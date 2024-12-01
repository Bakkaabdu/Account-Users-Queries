// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: Protos/Clients/anis_users_events_history.proto
// </auto-generated>
#pragma warning disable 0414, 1591, 8981, 0612
#region Designer generated code

using grpc = global::Grpc.Core;

namespace Anis_UserManagment.Queries.GRPC.Protos.Clients {
  public static partial class UsersEventsHistory
  {
    static readonly string __ServiceName = "manage_users_commands.UsersEventsHistory";

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static void __Helper_SerializeMessage(global::Google.Protobuf.IMessage message, grpc::SerializationContext context)
    {
      #if !GRPC_DISABLE_PROTOBUF_BUFFER_SERIALIZATION
      if (message is global::Google.Protobuf.IBufferMessage)
      {
        context.SetPayloadLength(message.CalculateSize());
        global::Google.Protobuf.MessageExtensions.WriteTo(message, context.GetBufferWriter());
        context.Complete();
        return;
      }
      #endif
      context.Complete(global::Google.Protobuf.MessageExtensions.ToByteArray(message));
    }

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static class __Helper_MessageCache<T>
    {
      public static readonly bool IsBufferMessage = global::System.Reflection.IntrospectionExtensions.GetTypeInfo(typeof(global::Google.Protobuf.IBufferMessage)).IsAssignableFrom(typeof(T));
    }

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static T __Helper_DeserializeMessage<T>(grpc::DeserializationContext context, global::Google.Protobuf.MessageParser<T> parser) where T : global::Google.Protobuf.IMessage<T>
    {
      #if !GRPC_DISABLE_PROTOBUF_BUFFER_SERIALIZATION
      if (__Helper_MessageCache<T>.IsBufferMessage)
      {
        return parser.ParseFrom(context.PayloadAsReadOnlySequence());
      }
      #endif
      return parser.ParseFrom(context.PayloadAsNewBuffer());
    }

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::Anis_UserManagment.Queries.GRPC.Protos.Clients.GetEventsRequest> __Marshaller_manage_users_commands_GetEventsRequest = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::Anis_UserManagment.Queries.GRPC.Protos.Clients.GetEventsRequest.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::Anis_UserManagment.Queries.GRPC.Protos.Clients.Response> __Marshaller_manage_users_commands_Response = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::Anis_UserManagment.Queries.GRPC.Protos.Clients.Response.Parser));

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::Anis_UserManagment.Queries.GRPC.Protos.Clients.GetEventsRequest, global::Anis_UserManagment.Queries.GRPC.Protos.Clients.Response> __Method_GetEvents = new grpc::Method<global::Anis_UserManagment.Queries.GRPC.Protos.Clients.GetEventsRequest, global::Anis_UserManagment.Queries.GRPC.Protos.Clients.Response>(
        grpc::MethodType.Unary,
        __ServiceName,
        "GetEvents",
        __Marshaller_manage_users_commands_GetEventsRequest,
        __Marshaller_manage_users_commands_Response);

    /// <summary>Service descriptor</summary>
    public static global::Google.Protobuf.Reflection.ServiceDescriptor Descriptor
    {
      get { return global::Anis_UserManagment.Queries.GRPC.Protos.Clients.AnisUsersEventsHistoryReflection.Descriptor.Services[0]; }
    }

    /// <summary>Client for UsersEventsHistory</summary>
    public partial class UsersEventsHistoryClient : grpc::ClientBase<UsersEventsHistoryClient>
    {
      /// <summary>Creates a new client for UsersEventsHistory</summary>
      /// <param name="channel">The channel to use to make remote calls.</param>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public UsersEventsHistoryClient(grpc::ChannelBase channel) : base(channel)
      {
      }
      /// <summary>Creates a new client for UsersEventsHistory that uses a custom <c>CallInvoker</c>.</summary>
      /// <param name="callInvoker">The callInvoker to use to make remote calls.</param>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public UsersEventsHistoryClient(grpc::CallInvoker callInvoker) : base(callInvoker)
      {
      }
      /// <summary>Protected parameterless constructor to allow creation of test doubles.</summary>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      protected UsersEventsHistoryClient() : base()
      {
      }
      /// <summary>Protected constructor to allow creation of configured clients.</summary>
      /// <param name="configuration">The client configuration.</param>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      protected UsersEventsHistoryClient(ClientBaseConfiguration configuration) : base(configuration)
      {
      }

      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::Anis_UserManagment.Queries.GRPC.Protos.Clients.Response GetEvents(global::Anis_UserManagment.Queries.GRPC.Protos.Clients.GetEventsRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return GetEvents(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::Anis_UserManagment.Queries.GRPC.Protos.Clients.Response GetEvents(global::Anis_UserManagment.Queries.GRPC.Protos.Clients.GetEventsRequest request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_GetEvents, null, options, request);
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::Anis_UserManagment.Queries.GRPC.Protos.Clients.Response> GetEventsAsync(global::Anis_UserManagment.Queries.GRPC.Protos.Clients.GetEventsRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return GetEventsAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::Anis_UserManagment.Queries.GRPC.Protos.Clients.Response> GetEventsAsync(global::Anis_UserManagment.Queries.GRPC.Protos.Clients.GetEventsRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_GetEvents, null, options, request);
      }
      /// <summary>Creates a new instance of client from given <c>ClientBaseConfiguration</c>.</summary>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      protected override UsersEventsHistoryClient NewInstance(ClientBaseConfiguration configuration)
      {
        return new UsersEventsHistoryClient(configuration);
      }
    }

  }
}
#endregion