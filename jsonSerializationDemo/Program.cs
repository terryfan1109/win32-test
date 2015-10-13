using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;
using System.Text;
using System.IO;

[DataContract]
class MySubObject
{
  [DataMember]
  public String attr1 { get; set; }
  [DataMember]
  public String attr2 { get; set; }
  [DataMember]
  public Int32 attr3 { get; set; }
}

[DataContract]
class MyObject
{
  [DataMember]
  public String attr1 { get; set; }
  [DataMember]
  public MySubObject attr2 { get; set; }
  [DataMember]
  public Int32 attr3 { get; set; }
  [DataMember]
  public List<String> attr4 { get; set; }
}

namespace jsonSerializationDemo
{
  class Program
  {
    static void Main(string[] args)
    {
      var myValue = new MyObject()
      {
        attr1 = "ABC",
        attr2 = new MySubObject()
        {
          attr1 = "XYZ",
          attr2 = "ABC",
          attr3 = 567
        },
        attr3 = 123,
        attr4 = new List<String>(new String[] { "abc", "def" })
      };

      var outputBuffer = new MemoryStream();
      var serializerSettings = new DataContractJsonSerializerSettings() {
        UseSimpleDictionaryFormat = false
      };
      var serializer = new DataContractJsonSerializer(typeof(MyObject), serializerSettings);
      serializer.WriteObject(outputBuffer, myValue);

      System.Console.Out.WriteLine(Encoding.UTF8.GetString(outputBuffer.ToArray()));
    }
  }
}
