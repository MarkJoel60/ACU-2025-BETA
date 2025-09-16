// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CABankFeedFileAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CA.BankFeed;
using System;
using System.Collections;

#nullable disable
namespace PX.Objects.CA;

public class CABankFeedFileAttribute : PXCustomSelectorAttribute
{
  public CABankFeedFileAttribute()
    : base(typeof (Search<BankFeedFile.fileName>), new Type[2]
    {
      typeof (BankFeedFile.fileName),
      typeof (BankFeedFile.uploadDate)
    })
  {
  }

  protected virtual IEnumerable GetRecords()
  {
    CABankFeedFileAttribute feedFileAttribute = this;
    PXGraph graph = feedFileAttribute._Graph;
    PXCache pxCache = (PXCache) null;
    if (graph != null)
      pxCache = GraphHelper.GetPrimaryCache(feedFileAttribute._Graph);
    if (pxCache != null)
    {
      foreach (BankFeedFile record in graph.Caches[typeof (BankFeedFile)].Cached)
        yield return (object) record;
    }
  }
}
