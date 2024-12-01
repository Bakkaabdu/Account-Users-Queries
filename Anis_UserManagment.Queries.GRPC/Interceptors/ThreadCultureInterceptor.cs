using Grpc.Core;
using Grpc.Core.Interceptors;
using System.Globalization;

namespace Anis_UserManagment.Queries.GRPC.Interceptors
{
    public class ThreadCultureInterceptor : Interceptor
    {
        public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(
            TRequest request,
            ServerCallContext context,
            UnaryServerMethod<TRequest, TResponse> continuation)
        {
            var headerLanguage = context.RequestHeaders.FirstOrDefault(t => t.Key == "language");

            if (headerLanguage != null)
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo(headerLanguage.Value);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(headerLanguage.Value);
            }

            return await continuation(request, context);
        }
    }
}
