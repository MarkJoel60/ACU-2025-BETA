// Decompiled with JetBrains decompiler
// Type: PX.Data.Process.DummyRequest
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Web;

#nullable disable
namespace PX.Data.Process;

public class DummyRequest : HttpWorkerRequest
{
  private string _url;
  private string _query;

  public override void FlushResponse(bool finalFlush)
  {
  }

  public override void EndOfRequest()
  {
  }

  public DummyRequest(string url)
  {
    int length1;
    if (url != null && (length1 = url.IndexOf('#')) != -1 && length1 + 1 < url.Length)
      url = url.Substring(0, length1);
    int length2;
    if (url != null && (length2 = url.IndexOf('?')) != -1 && length2 + 1 < url.Length)
    {
      this._url = url.Substring(0, length2);
      this._query = url.Substring(length2 + 1);
    }
    else
    {
      this._url = url;
      this._query = "";
    }
  }

  public override string GetHttpVerbName() => "GET";

  public override string GetHttpVersion() => "HTTP/1.1";

  public override string GetLocalAddress() => "127.0.0.1";

  public override int GetLocalPort() => 80 /*0x50*/;

  public override string GetQueryString() => this._query;

  public override string GetRawUrl() => this.GetAppPath() + this._url;

  public override string GetRemoteAddress() => "127.0.0.1";

  public override int GetRemotePort() => 2136;

  public override string GetUriPath() => this.GetAppPath() + this._url;

  public override void SendKnownResponseHeader(int index, string value)
  {
  }

  public override void SendResponseFromMemory(byte[] data, int length)
  {
  }

  public override void SendStatus(int statusCode, string statusDescription)
  {
  }

  public override void SendUnknownResponseHeader(string name, string value)
  {
  }

  public override void SendResponseFromFile(IntPtr handle, long offset, long length)
  {
  }

  public override void SendResponseFromFile(string filename, long offset, long length)
  {
  }

  public override void CloseConnection()
  {
  }

  public override string GetAppPath() => HttpRuntime.AppDomainAppVirtualPath;

  public override string GetAppPathTranslated()
  {
    return HttpRuntime.AppDomainAppPath.Substring(0, HttpRuntime.AppDomainAppPath.Length - 1);
  }

  public override int ReadEntityBody(byte[] buffer, int size) => 0;

  public override string GetUnknownRequestHeader(string name)
  {
    return string.Compare("Host", name, true) == 0 ? "dummyhost/" : (string) null;
  }

  public override string[][] GetUnknownRequestHeaders()
  {
    return new string[1][]
    {
      new string[2]{ "Host", "dummyhost/" }
    };
  }

  public override string GetKnownRequestHeader(int index)
  {
    return index == 39 ? this.GetServerVariable("HTTP_USER_AGENT") : (string) null;
  }

  public override string GetServerVariable(string name)
  {
    switch (name)
    {
      case "HTTPS":
        return "off";
      case "HTTP_USER_AGENT":
        return "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; .NET CLR 1.1.4322; .NET CLR 2.0.50727; .NET CLR 3.0.04506.30; .NET CLR 3.0.04506.648; .NET CLR 3.5.21022)";
      default:
        return (string) null;
    }
  }

  public override string GetFilePath()
  {
    string filePath = this.GetUriPath();
    if (filePath.IndexOf(".aspx") != -1)
      filePath = filePath.Substring(0, filePath.IndexOf(".aspx") + 5);
    else if (filePath.IndexOf(".asmx") != -1)
      filePath = filePath.Substring(0, filePath.IndexOf(".asmx") + 5);
    return filePath;
  }

  public override string GetFilePathTranslated()
  {
    string str = this.GetFilePath().Substring(this.GetAppPath().Length).Replace('/', '\\');
    return this.GetAppPathTranslated() + str;
  }

  public override string GetPathInfo()
  {
    string filePath = this.GetFilePath();
    string uriPath = this.GetUriPath();
    return filePath.Length == uriPath.Length ? "" : uriPath.Substring(filePath.Length);
  }
}
