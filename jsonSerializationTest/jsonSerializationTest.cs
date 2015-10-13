using System;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;
using System.Text;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using System.Globalization;

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

[DataContract]
public class Account
{
  [DataMember]
  public String account { get; set; }
  [DataMember]
  public String countryCode { get; set; }
  [DataMember]
  public Boolean isVerified { get; set; }
  [DataMember]
  public Boolean legalDocsToSign { get; set; }
  [DataMember]
  public String provider { get; set; }
}

[DataContract]
public class SigninResponse
{
  [DataMember]
  public String accessToken { get; set; }
  [DataMember]
  public Account account { get; set; }
  [DataMember]
  public long expiresIn { get; set; }
  [DataMember]
  public String profileServiceUri { get; set; }
  [DataMember]
  public String refreshToken { get; set; }
  [DataMember]
  public List<String> scopes { get; set; }
  [DataMember]
  public String serviceUri { get; set; }
}

namespace jsonSerializationTest
{
    [TestClass]
    public class jsonSerializationTest
    {
        [TestMethod]
        public void TestJsonDeserializer()
        {
            var data = @"{""attr1"":""ABC"",""attr2"":{""attr1"":""XYZ"",""attr2"":""ABC"",""attr3"":567},""attr3"":123,""attr4"":[""abc"",""def""]}";
            var dataStream = new MemoryStream(Encoding.UTF8.GetBytes(data));
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(MyObject));
            var myValue = (MyObject)serializer.ReadObject(dataStream);
        }

        [TestMethod]
        public void TestJsonSerializer()
        {
          System.Diagnostics.Trace.WriteLine("...");

          var myValue = new MyObject()
          {
            attr1 = "ABC",
            attr2 = new MySubObject(){
              attr1 = "XYZ",
              attr2 = "ABC",
              attr3 = 567
            },
            attr3 = 123,
            attr4 = new List<String>(new String[]{"abc", "def"})
          };

          var outputBuffer = new MemoryStream();
          var serializer = new DataContractJsonSerializer(typeof(MyObject));
          serializer.WriteObject(outputBuffer, myValue);

          System.Diagnostics.Trace.WriteLine(Encoding.UTF8.GetString(outputBuffer.ToArray()));
        }

        [TestMethod]
        public void TestJsonDeserializer2()
        {
          Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-us");

          var data = @"{""account"":{""accountId"":""05df8612-bdd2-44ed-a7f8-38207eb72415"",""countryCode"":""TW"",""isVerified"":true,""legalDocsToSign"":false,""provider"":""htc""},""accessToken"":""hEZHfq7ja71dkIO5HLiBHpHVhjIBDlvLDfwtnZbAPxM0jIanQp+l0sebtIczrYvZ2TQowcIDMKOVzYEvfLeZJce2X9lIWPQnBHR3Kw71yqyANj02c5CfJpd2TmBkMKUU01IzU0kBmKS6JmWa6KUL0bmptCOapUdCdidExKA0SeEGYc4IjggY7mULzYgJmwBVjCzDMXUXgpH2/NOSt//AJi0uFoJPVEVDo8jezLkvQXtIZT+UD3QzTEwKQUVSHQW0gvwu1FhyZk/SaB+f56HnOxR6ey2mGm9UfaTiq15SrX5Sof4wWE7OvsuvYshCa/l6e59P5hC4pWQVxFd4VJBWJC/97Xs1hAx+XY7f7Rb/yaKiGT0T1d/hf0Rq4h5z+4OBFgsmBlg3eOdl4HhemFWqZZKjnGMbqs+91Sxy7tJtBokGCuwtwSK5YZrgc/82Uo/fmRLq86t6Xjww8Ypls9uBHLnape6bhQA153etzlnDOBlohdPP0AhdBqNSs9f7A+gi0PNnF+Fkqbx27126nTu1xRkShqb53ZBgj97OEO+cKVk="",""expiresIn"":86400,""refreshToken"":""AfBR8GOSPdFMGEyfK507p7wFJvbXX2S2LJoCmw1K3SKImo_fLa-fOiv5PgOAPCTCHf5PGCd_d80EW073iZvJboWlaTjO2ax6nwmQkFRVe8y08ZerFNLQvbG4hLcVCT24s6SakjK2fEVRp4a_9CygfpcLAzVdEN5p3MFROTKf0Y4"",""scopes"":[""issuetoken"",""email""],""serviceUri"":""https://www.htcsense.com/$RAM$/$SS$/"",""profileServiceUri"":""https://profile.htcsense.com/RAM/SS/"",""avatarServiceUri"":""https://avatar.htcsense.com/""}";
          var dataStream = new MemoryStream(Encoding.UTF8.GetBytes(data));
          DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(SigninResponse));
          var myValue = (SigninResponse)serializer.ReadObject(dataStream);

          var outputBuffer = new MemoryStream();
          serializer.WriteObject(outputBuffer, myValue);

          System.Diagnostics.Trace.WriteLine(Encoding.UTF8.GetString(outputBuffer.ToArray()));
        }        
    
    }
}
