// Decompiled with JetBrains decompiler
// Type: PX.Data.PXHttpWebRequestScope
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.IO;
using System.Net;

#nullable disable
namespace PX.Data;

public class PXHttpWebRequestScope
{
  protected HttpWebRequest objRequest;

  public PXHttpWebRequestScope(string aUrl)
  {
    this.objRequest = (HttpWebRequest) WebRequest.Create(aUrl);
  }

  public string DoRequest(string aRequest)
  {
    StreamWriter streamWriter = (StreamWriter) null;
    this.objRequest.Method = "POST";
    this.objRequest.ContentLength = (long) aRequest.Length;
    this.objRequest.ContentType = "application/x-www-form-urlencoded";
    try
    {
      streamWriter = new StreamWriter(this.objRequest.GetRequestStream());
      streamWriter.Write(aRequest.ToString());
    }
    finally
    {
      streamWriter?.Close();
    }
    string end;
    using (StreamReader streamReader = new StreamReader(this.objRequest.GetResponse().GetResponseStream()))
    {
      end = streamReader.ReadToEnd();
      streamReader.Close();
    }
    return end;
  }
}
