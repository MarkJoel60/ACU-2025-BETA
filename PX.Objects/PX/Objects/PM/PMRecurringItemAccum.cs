// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMRecurringItemAccum
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.IN;
using System;
using System.Diagnostics.CodeAnalysis;

#nullable enable
namespace PX.Objects.PM;

[PXBreakInheritance]
[PMRecurringItemAccum]
[PXHidden]
[ExcludeFromCodeCoverage]
public class PMRecurringItemAccum : PMRecurringItem
{
  [PXDBInt(IsKey = true)]
  public override int? ProjectID { get; set; }

  [PXDBInt(IsKey = true)]
  public override int? TaskID { get; set; }

  [PXDBInt(IsKey = true)]
  public override int? InventoryID { get; set; }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Used", Enabled = false)]
  public override Decimal? Used { get; set; }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Used Total", Enabled = false)]
  public override Decimal? UsedTotal { get; set; }

  public new abstract class projectID : BqlType<IBqlInt, int>.Field<
  #nullable disable
  PMRecurringItemAccum.projectID>
  {
  }

  public new abstract class taskID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMRecurringItemAccum.taskID>
  {
  }

  public new abstract class inventoryID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMRecurringItemAccum.inventoryID>
  {
  }

  public new abstract class used : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMRecurringItemAccum.used>
  {
  }

  public new abstract class usedTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMRecurringItemAccum.usedTotal>
  {
  }
}
