// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INComponentTranSplit
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CS;
using System;
using System.Diagnostics;

#nullable enable
namespace PX.Objects.IN;

[DebuggerDisplay("SerialNbr={LotSerialNbr}")]
[PXCacheName("IN Component Split")]
public class INComponentTranSplit : INTranSplit
{
  [PXDBString(1, IsFixed = true, IsKey = true)]
  [PXDefault(typeof (INComponentTran.docType))]
  public override 
  #nullable disable
  string DocType
  {
    get => this._DocType;
    set => this._DocType = value;
  }

  [PXDBString(2, IsFixed = true)]
  [PXDBDefault(typeof (INKitRegister.origModule))]
  public override string OrigModule
  {
    get => this._OrigModule;
    set => this._OrigModule = value;
  }

  [PXDBString(3)]
  [PXDefault]
  [PXFormula(typeof (Switch<Case<Where<INComponentTranSplit.docType, Equal<INDocType.disassembly>>, INTranType.disassembly>, INTranType.assembly>))]
  public override string TranType
  {
    get => this._TranType;
    set => this._TranType = value;
  }

  [PXDBString(15, IsUnicode = true, IsKey = true)]
  [PXDBDefault(typeof (INKitRegister.refNbr))]
  [PXParent(typeof (INComponentTranSplit.FK.ComponentTran), typeof (Where<BqlOperand<INComponentTran.lineNbr, IBqlInt>.IsNotEqual<BqlField<INKitRegister.kitLineNbr, IBqlInt>.FromCurrent>>))]
  public override string RefNbr
  {
    get => this._RefNbr;
    set => this._RefNbr = value;
  }

  [PXDBInt(IsKey = true)]
  [PXDefault(typeof (INComponentTran.lineNbr))]
  public override int? LineNbr
  {
    get => this._LineNbr;
    set => this._LineNbr = value;
  }

  [PXDBInt(IsKey = true)]
  [PXLineNbr(typeof (INKitRegister.lineCntr))]
  public override int? SplitLineNbr
  {
    get => this._SplitLineNbr;
    set => this._SplitLineNbr = value;
  }

  [PXDBDate]
  [PXDBDefault(typeof (INKitRegister.tranDate))]
  public override DateTime? TranDate
  {
    get => this._TranDate;
    set => this._TranDate = value;
  }

  [PXDBShort]
  [PXDefault(typeof (INComponentTran.invtMult))]
  public override short? InvtMult
  {
    get => this._InvtMult;
    set => this._InvtMult = value;
  }

  [StockItem(Visible = false)]
  [PXDefault(typeof (INComponentTran.inventoryID))]
  public override int? InventoryID
  {
    get => this._InventoryID;
    set => this._InventoryID = value;
  }

  [SubItem(typeof (INComponentTranSplit.inventoryID))]
  [PXDefault]
  public override int? SubItemID
  {
    get => this._SubItemID;
    set => this._SubItemID = value;
  }

  [Site]
  [PXDefault(typeof (INComponentTran.siteID))]
  public override int? SiteID
  {
    get => this._SiteID;
    set => this._SiteID = value;
  }

  [LocationAvail(typeof (INComponentTranSplit.inventoryID), typeof (INComponentTranSplit.subItemID), typeof (CostCenter.freeStock), typeof (INComponentTranSplit.siteID), typeof (Where2<Where<INComponentTranSplit.tranType, Equal<INTranType.assembly>, Or<INComponentTranSplit.tranType, Equal<INTranType.disassembly>>>, And<INComponentTranSplit.invtMult, Equal<shortMinus1>>>), typeof (Where2<Where<INComponentTranSplit.tranType, Equal<INTranType.assembly>, Or<INComponentTranSplit.tranType, Equal<INTranType.disassembly>>>, And<INComponentTranSplit.invtMult, Equal<short1>>>), typeof (Where<False, Equal<True>>))]
  [PXRestrictor(typeof (Where<True, Equal<True>>), null, new Type[] {}, ReplaceInherited = true)]
  [PXDefault]
  public override int? LocationID
  {
    get => this._LocationID;
    set => this._LocationID = value;
  }

  [INLotSerialNbr(typeof (INComponentTranSplit.inventoryID), typeof (INComponentTranSplit.subItemID), typeof (INComponentTranSplit.locationID), typeof (INTran.lotSerialNbr), typeof (CostCenter.freeStock))]
  public override string LotSerialNbr
  {
    get => this._LotSerialNbr;
    set => this._LotSerialNbr = value;
  }

  [INUnit(typeof (INComponentTranSplit.inventoryID), DisplayName = "UOM", Enabled = false)]
  [PXDefault]
  public override string UOM
  {
    get => this._UOM;
    set => this._UOM = value;
  }

  [PXDBQuantity(typeof (INComponentTranSplit.uOM), typeof (INComponentTranSplit.baseQty), InventoryUnitType.BaseUnit)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Quantity")]
  public override Decimal? Qty
  {
    get => this._Qty;
    set => this._Qty = value;
  }

  [PXDBLong(IsImmutable = true)]
  public override long? PlanID
  {
    get => this._PlanID;
    set => this._PlanID = value;
  }

  public static INComponentTranSplit FromINComponentTran(INComponentTran item)
  {
    INComponentTranSplit componentTranSplit = new INComponentTranSplit();
    componentTranSplit.DocType = item.DocType;
    componentTranSplit.TranType = item.TranType;
    componentTranSplit.RefNbr = item.RefNbr;
    componentTranSplit.LineNbr = item.LineNbr;
    componentTranSplit.SplitLineNbr = new int?(1);
    componentTranSplit.InventoryID = item.InventoryID;
    componentTranSplit.SiteID = item.SiteID;
    componentTranSplit.SubItemID = item.SubItemID;
    componentTranSplit.LocationID = item.LocationID;
    componentTranSplit.LotSerialNbr = item.LotSerialNbr;
    componentTranSplit.ExpireDate = item.ExpireDate;
    componentTranSplit.Qty = item.Qty;
    componentTranSplit.UOM = item.UOM;
    componentTranSplit.TranDate = item.TranDate;
    componentTranSplit.BaseQty = item.BaseQty;
    componentTranSplit.InvtMult = item.InvtMult;
    componentTranSplit.Released = item.Released;
    return componentTranSplit;
  }

  public new class PK : 
    PrimaryKeyOf<INComponentTranSplit>.By<INComponentTranSplit.docType, INComponentTranSplit.refNbr, INComponentTranSplit.lineNbr, INComponentTranSplit.splitLineNbr>
  {
    public static INComponentTranSplit Find(
      PXGraph graph,
      string docType,
      string refNbr,
      int? lineNbr,
      int? splitLineNbr,
      PKFindOptions options = 0)
    {
      return (INComponentTranSplit) PrimaryKeyOf<INComponentTranSplit>.By<INComponentTranSplit.docType, INComponentTranSplit.refNbr, INComponentTranSplit.lineNbr, INComponentTranSplit.splitLineNbr>.FindBy(graph, (object) docType, (object) refNbr, (object) lineNbr, (object) splitLineNbr, options);
    }
  }

  public new static class FK
  {
    public class KitRegister : 
      PrimaryKeyOf<INKitRegister>.By<INKitRegister.docType, INKitRegister.refNbr>.ForeignKeyOf<INComponentTranSplit>.By<INComponentTranSplit.docType, INComponentTranSplit.refNbr>
    {
    }

    public class ComponentTran : 
      PrimaryKeyOf<INComponentTran>.By<INComponentTran.docType, INComponentTran.refNbr, INComponentTran.lineNbr>.ForeignKeyOf<INComponentTranSplit>.By<INComponentTranSplit.docType, INComponentTranSplit.refNbr, INComponentTranSplit.lineNbr>
    {
    }

    public class InventoryItem : 
      PrimaryKeyOf<InventoryItem>.By<InventoryItem.inventoryID>.ForeignKeyOf<INComponentTranSplit>.By<INComponentTranSplit.inventoryID>
    {
    }

    public class SubItem : 
      PrimaryKeyOf<INSubItem>.By<INSubItem.subItemID>.ForeignKeyOf<INComponentTranSplit>.By<INComponentTranSplit.subItemID>
    {
    }

    public class Site : 
      PrimaryKeyOf<INSite>.By<INSite.siteID>.ForeignKeyOf<INComponentTranSplit>.By<INComponentTranSplit.siteID>
    {
    }

    public class Location : 
      PrimaryKeyOf<INLocation>.By<INLocation.locationID>.ForeignKeyOf<INComponentTranSplit>.By<INComponentTranSplit.locationID>
    {
    }

    public class ItemPlan : 
      PrimaryKeyOf<INItemPlan>.By<INItemPlan.planID>.ForeignKeyOf<INComponentTranSplit>.By<INComponentTranSplit.planID>
    {
    }
  }

  public new abstract class docType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INComponentTranSplit.docType>
  {
  }

  public new abstract class origModule : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INComponentTranSplit.origModule>
  {
  }

  public new abstract class tranType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INComponentTranSplit.tranType>
  {
  }

  public new abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INComponentTranSplit.refNbr>
  {
  }

  public new abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INComponentTranSplit.lineNbr>
  {
  }

  public new abstract class splitLineNbr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    INComponentTranSplit.splitLineNbr>
  {
  }

  public new abstract class tranDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INComponentTranSplit.tranDate>
  {
  }

  public new abstract class invtMult : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  INComponentTranSplit.invtMult>
  {
  }

  public new abstract class inventoryID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    INComponentTranSplit.inventoryID>
  {
  }

  public new abstract class subItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INComponentTranSplit.subItemID>
  {
  }

  public new abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INComponentTranSplit.siteID>
  {
  }

  public new abstract class locationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INComponentTranSplit.locationID>
  {
  }

  public new abstract class lotSerialNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INComponentTranSplit.lotSerialNbr>
  {
  }

  public new abstract class uOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INComponentTranSplit.uOM>
  {
  }

  public new abstract class qty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INComponentTranSplit.qty>
  {
  }

  public new abstract class baseQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INComponentTranSplit.baseQty>
  {
  }

  public new abstract class planID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  INComponentTranSplit.planID>
  {
  }
}
