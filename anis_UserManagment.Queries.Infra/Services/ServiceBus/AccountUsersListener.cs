using anis_UserManagment.Queries.Domain.Events.DataTypes;
using anis_UserManagment.Queries.Domain.Events;
using anis_UserManagment.Queries.Infra.Constants;
using Azure.Messaging.ServiceBus;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anis_UserManagment.Queries.Infra.Services.ServiceBus
{
    public class AccountUsersListener : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<AccountUsersListener> _logger;
        private readonly IConfiguration _configuration;

        private readonly ServiceBusSessionProcessor _processor;
        private readonly ServiceBusProcessor _deadLetterProcessor;
        private readonly ServiceBusClient _serviceBusClient;

        public AccountUsersListener(
            IServiceProvider serviceProvider,
            ILogger<AccountUsersListener> logger,
            IConfiguration configuration,
            ServiceBusClient serviceBusClient)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
            _configuration = configuration;
            _serviceBusClient = serviceBusClient;

            var options = new ServiceBusSessionProcessorOptions
            {
                AutoCompleteMessages = false,
                PrefetchCount = 1,
                MaxConcurrentCallsPerSession = 1,
                MaxConcurrentSessions = 100,
                ReceiveMode = ServiceBusReceiveMode.PeekLock,
                MaxAutoLockRenewalDuration = TimeSpan.FromMinutes(10)
            };

            _processor = _serviceBusClient.CreateSessionProcessor(
                configuration["ServiceBus:Topic"],
                configuration["ServiceBus:Subscription"],
                options);

            _processor.ProcessMessageAsync += Processor_ProcessMessageAsync;
            _processor.ProcessErrorAsync += Processor_ProcessErrorAsync;

            _deadLetterProcessor = _serviceBusClient.CreateProcessor(configuration["ServiceBus:Topic"],
              configuration["ServiceBus:Subscription"], new ServiceBusProcessorOptions()
              {
                  PrefetchCount = 1,
                  AutoCompleteMessages = false,
                  MaxConcurrentCalls = 1,
                  SubQueue = SubQueue.DeadLetter,
              });

            _deadLetterProcessor.ProcessMessageAsync += DeadLetterProcessor_ProcessMessageAsync;
            _deadLetterProcessor.ProcessErrorAsync += DeadLetterProcessor_ProcessErrorAsync;

        }

        private async Task Processor_ProcessMessageAsync(ProcessSessionMessageEventArgs arg)
        {
            Task<bool> isHandledTask = HandelSubject(arg.Message.Subject, arg.Message);

            var isHandled = await isHandledTask;

            if (isHandled)
            {
                await arg.CompleteMessageAsync(arg.Message);
            }
            else
            {
                await arg.AbandonMessageAsync(arg.Message);
            }
        }

        private Task Processor_ProcessErrorAsync(ProcessErrorEventArgs arg)
        {
            _logger.LogCritical(arg.Exception, "AccountUsersListener => _processor => Processor_ProcessErrorAsync Message handler encountered an exception," +
                " Error Source:{ErrorSource}," +
                " Entity Path:{EntityPath}",
                arg.ErrorSource.ToString(),
                arg.EntityPath
            );

            return Task.CompletedTask;
        }

        private async Task DeadLetterProcessor_ProcessMessageAsync(ProcessMessageEventArgs arg)
        {
            Task<bool> isHandledTask = HandelSubject(arg.Message.Subject, arg.Message);

            var isHandled = await isHandledTask;

            if (isHandled)
                await arg.CompleteMessageAsync(arg.Message);
            else
                await arg.AbandonMessageAsync(arg.Message);
        }

        private Task DeadLetterProcessor_ProcessErrorAsync(ProcessErrorEventArgs arg)
        {
            _logger.LogCritical(arg.Exception, "DeadLetter Message handler encountered an exception," +
                                               " Error Source:{ErrorSource}," +
                                               " Entity Path:{EntityPath}",
                arg.ErrorSource.ToString(),
                arg.EntityPath
            );

            return Task.CompletedTask;
        }

        private async Task<bool> HandelSubject(string subject, ServiceBusReceivedMessage message)
        {
            return subject switch
            {
                EventType.UserAssignedToAccount => await HandleAsync<UserAssignedData>(message),
                EventType.UserDeletedFromAccount => await HandleAsync<UserUnAssignedData>(message),
                _ => false
            };
        }

        private async Task<bool> HandleAsync<T>(ServiceBusReceivedMessage message)
        {
            var json = Encoding.UTF8.GetString(message.Body);

            var body = JsonConvert.DeserializeObject<Event<T>>(json) ?? throw new InvalidOperationException("Deserialize object return null");

            using var scope = _serviceProvider.CreateScope();

            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

            return await SendAsync(mediator, body);
        }

        private async Task<bool> SendAsync<T>(IMediator mediator, Event<T> @event)
        {
            var result = await mediator.Send(@event);

            if (!result)
                _logger.LogWarning("event not handled with AggregateId {id} and Sequence {seqience}",
                    @event.AggregateId,
                    @event.Sequence);


            return result;
        }

        public virtual Task StartAsync(CancellationToken cancellationToken)
        {
            _processor.StartProcessingAsync(cancellationToken);

            _deadLetterProcessor.StartProcessingAsync(cancellationToken);

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _processor.CloseAsync(cancellationToken);

            return Task.CompletedTask;
        }

        public Task CloseProccessorAsync()
        {
            return _processor.CloseAsync();
        }
    }
}
