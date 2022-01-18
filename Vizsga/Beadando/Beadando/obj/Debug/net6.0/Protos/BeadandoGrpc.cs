// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: Protos/beadando.proto
// </auto-generated>
#pragma warning disable 0414, 1591
#region Designer generated code

using grpc = global::Grpc.Core;

namespace Beadando {
  public static partial class Beadandopackage
  {
    static readonly string __ServiceName = "Beadandopackage.Beadandopackage";

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
    static readonly grpc::Marshaller<global::Beadando.User> __Marshaller_Beadandopackage_User = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::Beadando.User.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::Beadando.Session_Id> __Marshaller_Beadandopackage_Session_Id = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::Beadando.Session_Id.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::Beadando.Result> __Marshaller_Beadandopackage_Result = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::Beadando.Result.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::Beadando.Data> __Marshaller_Beadandopackage_Data = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::Beadando.Data.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::Beadando.Empty> __Marshaller_Beadandopackage_Empty = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::Beadando.Empty.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::Beadando.Product> __Marshaller_Beadandopackage_Product = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::Beadando.Product.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::Beadando.Product2> __Marshaller_Beadandopackage_Product2 = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::Beadando.Product2.Parser));

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::Beadando.User, global::Beadando.Session_Id> __Method_Login = new grpc::Method<global::Beadando.User, global::Beadando.Session_Id>(
        grpc::MethodType.Unary,
        __ServiceName,
        "Login",
        __Marshaller_Beadandopackage_User,
        __Marshaller_Beadandopackage_Session_Id);

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::Beadando.Session_Id, global::Beadando.Result> __Method_Logout = new grpc::Method<global::Beadando.Session_Id, global::Beadando.Result>(
        grpc::MethodType.Unary,
        __ServiceName,
        "Logout",
        __Marshaller_Beadandopackage_Session_Id,
        __Marshaller_Beadandopackage_Result);

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::Beadando.Data, global::Beadando.Result> __Method_Add = new grpc::Method<global::Beadando.Data, global::Beadando.Result>(
        grpc::MethodType.Unary,
        __ServiceName,
        "Add",
        __Marshaller_Beadandopackage_Data,
        __Marshaller_Beadandopackage_Result);

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::Beadando.Empty, global::Beadando.Product> __Method_List = new grpc::Method<global::Beadando.Empty, global::Beadando.Product>(
        grpc::MethodType.ServerStreaming,
        __ServiceName,
        "List",
        __Marshaller_Beadandopackage_Empty,
        __Marshaller_Beadandopackage_Product);

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::Beadando.Product2, global::Beadando.Result> __Method_Bid = new grpc::Method<global::Beadando.Product2, global::Beadando.Result>(
        grpc::MethodType.Unary,
        __ServiceName,
        "Bid",
        __Marshaller_Beadandopackage_Product2,
        __Marshaller_Beadandopackage_Result);

    /// <summary>Service descriptor</summary>
    public static global::Google.Protobuf.Reflection.ServiceDescriptor Descriptor
    {
      get { return global::Beadando.BeadandoReflection.Descriptor.Services[0]; }
    }

    /// <summary>Base class for server-side implementations of Beadandopackage</summary>
    [grpc::BindServiceMethod(typeof(Beadandopackage), "BindService")]
    public abstract partial class BeadandopackageBase
    {
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::System.Threading.Tasks.Task<global::Beadando.Session_Id> Login(global::Beadando.User request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::System.Threading.Tasks.Task<global::Beadando.Result> Logout(global::Beadando.Session_Id request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::System.Threading.Tasks.Task<global::Beadando.Result> Add(global::Beadando.Data request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::System.Threading.Tasks.Task List(global::Beadando.Empty request, grpc::IServerStreamWriter<global::Beadando.Product> responseStream, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::System.Threading.Tasks.Task<global::Beadando.Result> Bid(global::Beadando.Product2 request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

    }

    /// <summary>Creates service definition that can be registered with a server</summary>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public static grpc::ServerServiceDefinition BindService(BeadandopackageBase serviceImpl)
    {
      return grpc::ServerServiceDefinition.CreateBuilder()
          .AddMethod(__Method_Login, serviceImpl.Login)
          .AddMethod(__Method_Logout, serviceImpl.Logout)
          .AddMethod(__Method_Add, serviceImpl.Add)
          .AddMethod(__Method_List, serviceImpl.List)
          .AddMethod(__Method_Bid, serviceImpl.Bid).Build();
    }

    /// <summary>Register service method with a service binder with or without implementation. Useful when customizing the  service binding logic.
    /// Note: this method is part of an experimental API that can change or be removed without any prior notice.</summary>
    /// <param name="serviceBinder">Service methods will be bound by calling <c>AddMethod</c> on this object.</param>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public static void BindService(grpc::ServiceBinderBase serviceBinder, BeadandopackageBase serviceImpl)
    {
      serviceBinder.AddMethod(__Method_Login, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::Beadando.User, global::Beadando.Session_Id>(serviceImpl.Login));
      serviceBinder.AddMethod(__Method_Logout, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::Beadando.Session_Id, global::Beadando.Result>(serviceImpl.Logout));
      serviceBinder.AddMethod(__Method_Add, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::Beadando.Data, global::Beadando.Result>(serviceImpl.Add));
      serviceBinder.AddMethod(__Method_List, serviceImpl == null ? null : new grpc::ServerStreamingServerMethod<global::Beadando.Empty, global::Beadando.Product>(serviceImpl.List));
      serviceBinder.AddMethod(__Method_Bid, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::Beadando.Product2, global::Beadando.Result>(serviceImpl.Bid));
    }

  }
}
#endregion