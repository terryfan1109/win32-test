//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from: EventProtocol.proto
namespace Rpc.Event.Protocol
{
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"Request")]
  public partial class Request : global::ProtoBuf.IExtensible
  {
    public Request() {}
    
    private int _version;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"version", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int version
    {
      get { return _version; }
      set { _version = value; }
    }
    private int _value;
    [global::ProtoBuf.ProtoMember(101, IsRequired = true, Name=@"value", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int value
    {
      get { return _value; }
      set { _value = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"Response")]
  public partial class Response : global::ProtoBuf.IExtensible
  {
    public Response() {}
    
    private int _version;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"version", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int version
    {
      get { return _version; }
      set { _version = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"EventRequest")]
  public partial class EventRequest : global::ProtoBuf.IExtensible
  {
    public EventRequest() {}
    
    private int _version;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"version", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int version
    {
      get { return _version; }
      set { _version = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"EventResponse")]
  public partial class EventResponse : global::ProtoBuf.IExtensible
  {
    public EventResponse() {}
    
    private int _version;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"version", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int version
    {
      get { return _version; }
      set { _version = value; }
    }
    private int _eventId;
    [global::ProtoBuf.ProtoMember(101, IsRequired = true, Name=@"eventId", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int eventId
    {
      get { return _eventId; }
      set { _eventId = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
}