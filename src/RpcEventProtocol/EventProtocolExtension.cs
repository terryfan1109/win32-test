using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProtoBuf;

namespace Rpc.Event.Protocol
{
  public static class ProtocolExtension
  {
    public static byte[] toByteArray(this IExtensible value)
    {
      var stream = new MemoryStream();
      ProtoBuf.Serializer.Serialize(stream, value);
      var responseBuffer = new byte[stream.Length];
      stream.Seek(0, SeekOrigin.Begin);
      stream.Read(responseBuffer, 0, responseBuffer.Length);
      return responseBuffer;
    }
  }
}
