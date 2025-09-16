// Decompiled with JetBrains decompiler
// Type: PX.Data.Reports.ReportCacheCollection
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.Reports.DAC;

#nullable disable
namespace PX.Data.Reports;

internal class ReportCacheCollection : PXCacheUniqueForTypeCollection
{
  public ReportCacheCollection(PXGraph parent)
    : base(parent)
  {
  }

  public ReportCacheCollection(PXGraph parent, int capacity)
    : base(parent, capacity)
  {
  }

  protected override PXCache CreateCacheInstance(System.Type itemType)
  {
    return !(itemType == typeof (ReportParameter)) ? base.CreateCacheInstance(itemType) : (PXCache) new ReportParameterCache(this._Parent);
  }
}
