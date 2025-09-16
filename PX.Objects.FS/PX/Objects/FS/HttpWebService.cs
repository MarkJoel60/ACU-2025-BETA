// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.HttpWebService
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using System.IO;
using System.Net;

#nullable disable
namespace PX.Objects.FS;

internal static class HttpWebService
{
  /// <summary>Invokes the maps WebService.</summary>
  /// <param name="url">WebService URL.</param>
  internal static string MakeRequest(string url)
  {
    using (WebResponse response = WebRequest.Create(url).GetResponse())
    {
      using (Stream responseStream = response.GetResponseStream())
      {
        using (StreamReader streamReader = new StreamReader(responseStream))
          return streamReader.ReadToEnd();
      }
    }
  }
}
