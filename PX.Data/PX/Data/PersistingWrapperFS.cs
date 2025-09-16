// Decompiled with JetBrains decompiler
// Type: PX.Data.PersistingWrapperFS
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.SM;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Web.SessionState;

#nullable disable
namespace PX.Data;

[Serializable]
public class PersistingWrapperFS
{
  public const string WRAPPER = "Wrapper";
  private byte[][] Buffer;

  internal int? GetBufferLength()
  {
    byte[][] buffer = this.Buffer;
    return buffer == null ? new int?() : new int?(((IEnumerable<byte[]>) buffer).Sum<byte[]>((Func<byte[], int>) (x => x.Length)));
  }

  public void Wrap(SessionStateStoreData d)
  {
    if (d.Items.Count == 0)
      return;
    if (d.Items["Wrapper"] != null)
      throw new PXException("Double wrap");
    List<PersistingWrapperFS.NVPair> nvPairList = new List<PersistingWrapperFS.NVPair>();
    for (int index = 0; index < d.Items.Count; ++index)
    {
      object v = d.Items[index];
      if (!PersistingWrapperFS.IsPrimitive(v))
      {
        nvPairList.Add(new PersistingWrapperFS.NVPair()
        {
          N = d.Items.Keys[index],
          V = v
        });
        d.Items[index] = (object) null;
      }
    }
    d.Items["Wrapper"] = (object) this;
    SparseMemoryStream sparseMemoryStream = new SparseMemoryStream(2048 /*0x0800*/);
    GZipStream gzipStream = new GZipStream((Stream) sparseMemoryStream, CompressionMode.Compress);
    PXReflectionSerializer.Serialize((Stream) gzipStream, (object) nvPairList.ToArray(), false);
    gzipStream.Close();
    ((Stream) sparseMemoryStream).Close();
    this.Buffer = sparseMemoryStream.GetBuf();
  }

  public static void UnWrap(SessionStateStoreData d)
  {
    PersistingWrapperFS persistingWrapperFs = (PersistingWrapperFS) d.Items["Wrapper"];
    if (persistingWrapperFs == null)
      return;
    d.Items.Remove("Wrapper");
    persistingWrapperFs.Restore(d);
    PXPerformanceInfo currentSample = PXPerformanceMonitor.GetCurrentSample();
    if (currentSample == null)
      return;
    currentSample.SessionLoadSize = (double) persistingWrapperFs.GetBufferLength().GetValueOrDefault();
  }

  private void Restore(SessionStateStoreData d)
  {
    using (GZipStream gzipStream = new GZipStream((Stream) new SparseMemoryStream(this.Buffer, 2048 /*0x0800*/), CompressionMode.Decompress))
    {
      try
      {
        PersistingWrapperFS.NVPair[] nvPairArray = (PersistingWrapperFS.NVPair[]) PXReflectionSerializer.Deserialize((Stream) gzipStream);
        for (int index = 0; index < nvPairArray.Length; ++index)
        {
          string n = nvPairArray[index].N;
          object v = nvPairArray[index].V;
          d.Items[n] = v;
        }
      }
      catch (ArgumentException ex)
      {
        if (this.Buffer != null)
          PXFirstChanceExceptionLogger.LogMessage("A session argument exception with the buffer " + Convert.ToBase64String(new SparseMemoryStream(this.Buffer, 2048 /*0x0800*/).ToArray()));
        throw;
      }
    }
  }

  public static bool IsPrimitive(object v)
  {
    switch (v)
    {
      case null:
        return true;
      case string _:
        return true;
      case ValueType _:
        return true;
      default:
        return false;
    }
  }

  private class NVPair
  {
    public string N;
    public object V;
  }
}
