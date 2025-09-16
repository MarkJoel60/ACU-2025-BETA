// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.DaylightShiftFilter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.CS;

[PXCacheName("Calendar Year")]
[Serializable]
public class DaylightShiftFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXInt]
  [PXUIField(DisplayName = "Year")]
  [DaylightSelector]
  [CurrentYearByDefault]
  public virtual int? Year { get; set; }

  public abstract class year : BqlType<IBqlInt, int>.Field<
  #nullable disable
  DaylightShiftFilter.year>
  {
  }
}
