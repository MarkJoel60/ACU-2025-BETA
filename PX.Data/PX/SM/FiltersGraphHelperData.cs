// Decompiled with JetBrains decompiler
// Type: PX.SM.FiltersGraphHelperData
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;

#nullable disable
namespace PX.SM;

[PXHidden]
internal class FiltersGraphHelperData : 
  PXBqlTable,
  IFilterHeader,
  IBqlTable,
  IBqlTableSystemDataStorage
{
  public string ScreenID { get; set; }

  public string ViewName { get; set; }
}
