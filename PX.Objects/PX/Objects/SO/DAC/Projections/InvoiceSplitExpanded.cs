// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.DAC.Projections.InvoiceSplitExpanded
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.IN;
using PX.Objects.SO.Attributes;
using System;

#nullable enable
namespace PX.Objects.SO.DAC.Projections;

/// <exclude />
[PXHidden]
[InvoiceSplitProjection(false)]
[PXBreakInheritance]
public class InvoiceSplitExpanded : InvoiceSplit
{
  [PXBool]
  [PXDBCalced(typeof (IIf<Where<PX.Objects.AR.ARTran.sOShipmentType, Equal<PX.Objects.IN.INDocType.dropShip>>, True, False>), typeof (bool), BqlTable = typeof (InvoiceSplitExpanded))]
  [PXUIField(DisplayName = "Drop Ship")]
  public override bool? DropShip
  {
    get => base.DropShip;
    set => base.DropShip = value;
  }

  [PXDBString(1, IsKey = true, IsFixed = true, BqlField = typeof (INTran.docType))]
  public override 
  #nullable disable
  string INDocType
  {
    get => base.INDocType;
    set => base.INDocType = value;
  }

  [PXDBString(15, IsKey = true, IsUnicode = true, BqlField = typeof (INTran.refNbr))]
  public override string INRefNbr
  {
    get => base.INRefNbr;
    set => base.INRefNbr = value;
  }

  [PXDBInt(IsKey = true, BqlField = typeof (INTran.lineNbr))]
  public override int? INLineNbr
  {
    get => base.INLineNbr;
    set => base.INLineNbr = value;
  }

  [PXDBInt(IsKey = true, BqlField = typeof (INTranSplit.splitLineNbr))]
  public override int? INSplitLineNbr
  {
    get => base.INSplitLineNbr;
    set => base.INSplitLineNbr = value;
  }

  [Inventory(DisplayName = "Component ID", IsDBField = false)]
  [PXDBCalced(typeof (Switch<Case<Where<PX.Objects.AR.ARTran.inventoryID, NotEqual<INTran.inventoryID>>, INTran.inventoryID>>), typeof (int), BqlTable = typeof (InvoiceSplitExpanded))]
  public override int? ComponentID
  {
    get => base.ComponentID;
    set => base.ComponentID = value;
  }

  [PXDBCalced(typeof (Switch<Case<Where<PX.Objects.AR.ARTran.inventoryID, NotEqual<INTran.inventoryID>>, INTran.tranDesc>>), typeof (string), BqlTable = typeof (InvoiceSplitExpanded))]
  [PXString(256 /*0x0100*/, IsUnicode = true)]
  public override string ComponentDesc
  {
    get => base.ComponentDesc;
    set => base.ComponentDesc = value;
  }

  [Site(IsDBField = false)]
  [PXDBCalced(typeof (IsNull<INTranSplit.siteID, IsNull<INTran.siteID, PX.Objects.SO.SOLine.siteID>>), typeof (int), BqlTable = typeof (InvoiceSplitExpanded))]
  public override int? SiteID
  {
    get => base.SiteID;
    set => base.SiteID = value;
  }

  [Location(typeof (InvoiceSplitExpanded.siteID), IsDBField = false)]
  [PXDBCalced(typeof (IsNull<INTranSplit.locationID, IsNull<INTran.locationID, PX.Objects.SO.SOLine.locationID>>), typeof (int), BqlTable = typeof (InvoiceSplitExpanded))]
  public override int? LocationID
  {
    get => base.LocationID;
    set => base.LocationID = value;
  }

  [INUnit(DisplayName = "UOM", Enabled = false, IsDBField = false)]
  [PXDBCalced(typeof (IsNull<INTranSplit.uOM, IsNull<INTran.uOM, PX.Objects.AR.ARTran.uOM>>), typeof (string), BqlTable = typeof (InvoiceSplitExpanded))]
  public override string UOM
  {
    get => base.UOM;
    set => base.UOM = value;
  }

  [PXQuantity]
  [PXUIField(DisplayName = "Quantity")]
  [PXDBCalced(typeof (IsNull<INTranSplit.qty, IsNull<INTran.qty, IIf<Where<BqlOperand<PX.Objects.AR.ARTran.tranType, IBqlString>.IsIn<PX.Objects.AR.ARDocType.creditMemo, PX.Objects.AR.ARDocType.cashReturn>>, Minus<PX.Objects.AR.ARTran.qty>, PX.Objects.AR.ARTran.qty>>>), typeof (Decimal), BqlTable = typeof (InvoiceSplitExpanded))]
  public override Decimal? Qty
  {
    get => base.Qty;
    set => base.Qty = value;
  }

  [PXQuantity]
  [PXDBCalced(typeof (IsNull<INTranSplit.baseQty, IsNull<INTran.baseQty, PX.Objects.AR.ARTran.baseQty>>), typeof (Decimal), BqlTable = typeof (InvoiceSplitExpanded))]
  public override Decimal? BaseQty
  {
    get => base.BaseQty;
    set => base.BaseQty = value;
  }

  [PXBool]
  [PXDBCalced(typeof (IIf<Where<PX.Objects.AR.ARTran.inventoryID, Equal<INTran.inventoryID>>, False, True>), typeof (bool), BqlTable = typeof (InvoiceSplitExpanded))]
  public override bool? IsKit { get; set; }

  public new abstract class aRDocType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    InvoiceSplitExpanded.aRDocType>
  {
  }

  public new abstract class aRRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    InvoiceSplitExpanded.aRRefNbr>
  {
  }

  public new abstract class aRLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  InvoiceSplitExpanded.aRLineNbr>
  {
  }

  public new abstract class aRlineType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    InvoiceSplitExpanded.aRlineType>
  {
  }

  public new abstract class sOOrderNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    InvoiceSplitExpanded.sOOrderNbr>
  {
  }

  public new abstract class sOlineType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    InvoiceSplitExpanded.sOlineType>
  {
  }

  public new abstract class dropShip : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  InvoiceSplitExpanded.dropShip>
  {
  }

  public new abstract class iNDocType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    InvoiceSplitExpanded.iNDocType>
  {
  }

  public new abstract class iNRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    InvoiceSplitExpanded.iNRefNbr>
  {
  }

  public new abstract class iNLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  InvoiceSplitExpanded.iNLineNbr>
  {
  }

  public new abstract class iNSplitLineNbr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    InvoiceSplitExpanded.iNSplitLineNbr>
  {
  }

  public new abstract class componentID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    InvoiceSplitExpanded.componentID>
  {
  }

  public new abstract class componentDesc : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    InvoiceSplitExpanded.componentDesc>
  {
  }

  public new abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  InvoiceSplitExpanded.siteID>
  {
  }

  public new abstract class locationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  InvoiceSplitExpanded.locationID>
  {
  }

  public new abstract class uOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  InvoiceSplitExpanded.uOM>
  {
  }

  public new abstract class qty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  InvoiceSplitExpanded.qty>
  {
  }

  public new abstract class baseQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    InvoiceSplitExpanded.baseQty>
  {
  }

  public new abstract class isKit : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  InvoiceSplitExpanded.isKit>
  {
  }
}
