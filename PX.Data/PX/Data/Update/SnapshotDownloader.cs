// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.SnapshotDownloader
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.Handlers;
using PX.Data.Update.Storage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;

#nullable disable
namespace PX.Data.Update;

internal class SnapshotDownloader : IStreamGetterSource, IDisposable
{
  private const int BLOCK = 1000;
  private const string ID = "id";
  private Stream source;

  public long Length => this.source.Length;

  public void Initialise(Dictionary<string, string> args)
  {
    Guid id = new Guid(args["id"]);
    this.source = PXStorageHelper.GetProvider().OpenRead(id);
  }

  public void Dispose()
  {
    if (this.source == null)
      return;
    this.source.Dispose();
  }

  public void Read(Stream stream, long start, long stop)
  {
    long num = stop - start + 1L;
    this.source.Position = start;
    byte[] buffer1 = new byte[1000];
    while (num > 0L)
    {
      Stream source = this.source;
      byte[] buffer2 = buffer1;
      int count1 = 1000L < num ? 1000 : (int) num;
      int count2;
      if ((count2 = source.Read(buffer2, 0, count1)) <= 0)
        break;
      num -= (long) count2;
      stream.Write(buffer1, 0, count2);
      stream.Flush();
    }
  }

  public static void Write(Stream stream, Guid id)
  {
    using (Stream stream1 = PXStorageHelper.GetProvider().OpenWrite(id))
    {
      byte[] buffer = new byte[1000];
      int count;
      do
      {
        count = stream.Read(buffer, 0, 1000);
        stream1.Write(buffer, 0, count);
      }
      while (count == 1000);
      stream1.Flush();
    }
  }

  public static void Redirect(Guid id, string name)
  {
    throw new PXRedirectToUrlException($"~/Frames/GetStream.ashx?type=PX.Data.Update.SnapshotDownloader&name={HttpUtility.UrlEncode(name.Replace("&", "%26"))}.zip&{nameof (id)}={id.ToString()}", PXBaseRedirectException.WindowMode.Same, true, "Download Data");
  }
}
