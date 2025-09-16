// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.POOrderRS
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CM.Extensions;
using PX.Objects.IN;
using System;

#nullable enable
namespace PX.Objects.PO;

/// <summary>POOrder + Unbilled Service Items Projection</summary>
[PXBreakInheritance]
[Serializable]
public class POOrderRS : POOrder
{
  [PXDBCurrency(typeof (POOrderRS.curyInfoID), typeof (POOrderRS.unbilledOrderTotal))]
  [PXUIField(DisplayName = "Unbilled Amt.", Enabled = false)]
  public override Decimal? CuryUnbilledOrderTotal { get; set; }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public override Decimal? UnbilledOrderTotal { get; set; }

  [PXDBQuantity]
  [PXUIField(DisplayName = "Unbilled Qty.", Enabled = false)]
  public override Decimal? UnbilledOrderQty { get; set; }

  public new abstract class selected : BqlType<IBqlBool, bool>.Field<
  #nullable disable
  POOrderRS.selected>
  {
  }

  public new abstract class orderNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POOrderRS.orderNbr>
  {
  }

  public new abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  POOrderRS.curyInfoID>
  {
  }

  public new abstract class curyUnbilledOrderTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POOrderRS.curyUnbilledOrderTotal>
  {
  }

  public new abstract class unbilledOrderTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POOrderRS.unbilledOrderTotal>
  {
  }

  public new abstract class unbilledOrderQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POOrderRS.unbilledOrderQty>
  {
  }

  public new abstract class hasMultipleProjects : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    POOrderRS.hasMultipleProjects>
  {
  }
}
