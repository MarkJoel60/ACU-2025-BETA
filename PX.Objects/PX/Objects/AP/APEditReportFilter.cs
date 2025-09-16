// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APEditReportFilter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.AP;

public class APEditReportFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Include Pre-Released Transactions", FieldClass = "Prebooking")]
  public virtual bool? Prebooked { get; set; }

  public abstract class prebooked : BqlType<IBqlBool, bool>.Field<
  #nullable disable
  APEditReportFilter.prebooked>
  {
  }
}
