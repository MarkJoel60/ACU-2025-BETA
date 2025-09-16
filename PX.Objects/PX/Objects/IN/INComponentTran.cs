// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INComponentTran
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CM;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.PM;
using System;

#nullable enable
namespace PX.Objects.IN;

[PXProjection(typeof (Select<INTran, Where<INTran.assyType, Equal<INAssyType.compTran>>>), Persistent = true)]
[PXCacheName("IN Component")]
public class INComponentTran : INTran
{
  [Branch(typeof (INKitRegister.branchID), null, true, true, true)]
  public override int? BranchID
  {
    get => this._BranchID;
    set => this._BranchID = value;
  }

  [PXDBString(1, IsFixed = true, IsKey = true)]
  [PXDefault(typeof (INKitRegister.docType))]
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

  [PXDBString(3, IsFixed = true)]
  [PXDefault]
  [PXFormula(typeof (Switch<Case<Where<INComponentTran.docType, Equal<INDocType.disassembly>>, INTranType.disassembly>, INTranType.assembly>))]
  public override string TranType
  {
    get => this._TranType;
    set => this._TranType = value;
  }

  [PXDBString(15, IsUnicode = true, IsKey = true)]
  [PXDBDefault(typeof (INKitRegister.refNbr))]
  [PXParent(typeof (Select<INKitRegister, Where<INKitRegister.docType, Equal<Current<INComponentTran.docType>>, And<INKitRegister.refNbr, Equal<Current<INComponentTran.refNbr>>>>>))]
  public override string RefNbr
  {
    get => this._RefNbr;
    set => this._RefNbr = value;
  }

  [PXDBInt(IsKey = true)]
  [PXDefault]
  [PXLineNbr(typeof (INKitRegister.lineCntr))]
  public override int? LineNbr
  {
    get => this._LineNbr;
    set => this._LineNbr = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXDefault("C")]
  public override string AssyType
  {
    get => this._AssyType;
    set => this._AssyType = value;
  }

  [ProjectDefault]
  [PXDBInt(BqlField = typeof (INTran.projectID))]
  public override int? ProjectID
  {
    get => this._ProjectID;
    set => this._ProjectID = value;
  }

  [PXDBDate]
  [PXDBDefault(typeof (INKitRegister.tranDate))]
  public override DateTime? TranDate
  {
    get => this._TranDate;
    set => this._TranDate = value;
  }

  [PX.Objects.GL.FinPeriodID(null, typeof (INComponentTran.branchID), null, null, null, null, true, false, null, typeof (INComponentTran.tranPeriodID), typeof (INKitRegister.tranPeriodID), true, true)]
  public override string FinPeriodID
  {
    get => this._FinPeriodID;
    set => this._FinPeriodID = value;
  }

  [PeriodID(null, null, null, true, BqlField = typeof (INTran.tranPeriodID))]
  public override string TranPeriodID
  {
    get => this._TranPeriodID;
    set => this._TranPeriodID = value;
  }

  [PXDBShort]
  [PXDefault]
  public override short? InvtMult
  {
    get => this._InvtMult;
    set => this._InvtMult = value;
  }

  [PXDefault(typeof (Search<InventoryItem.baseUnit, Where<InventoryItem.inventoryID, Equal<Current<INComponentTran.inventoryID>>>>))]
  [INUnit(typeof (INComponentTran.inventoryID))]
  public override string UOM
  {
    get => this._UOM;
    set => this._UOM = value;
  }

  [PXDBQuantity(typeof (INComponentTran.uOM), typeof (INTran.baseQty), InventoryUnitType.BaseUnit)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Quantity")]
  public override Decimal? Qty
  {
    get => this._Qty;
    set => this._Qty = value;
  }

  [PXDefault]
  [Inventory(typeof (Search2<InventoryItem.inventoryID, InnerJoin<INLotSerClass, On<InventoryItem.FK.LotSerialClass>>, Where2<Match<Current<AccessInfo.userName>>, And<InventoryItem.stkItem, Equal<True>>>>), typeof (InventoryItem.inventoryCD), typeof (InventoryItem.descr), DisplayName = "Inventory ID")]
  [PXRestrictor(typeof (Where<INLotSerTrack.serialNumbered, Equal<Current<INKitRegister.lotSerTrack>>, Or<INLotSerClass.lotSerTrack, Equal<INLotSerTrack.lotNumbered>, Or<INLotSerClass.lotSerTrack, Equal<INLotSerTrack.notNumbered>>>>), "Serial-numbered components are allowed only in serial-numbered kits", new Type[] {})]
  public override int? InventoryID
  {
    get => this._InventoryID;
    set => this._InventoryID = value;
  }

  [PXDefault(typeof (Search<InventoryItem.defaultSubItemID, Where<InventoryItem.inventoryID, Equal<Current<INComponentTran.inventoryID>>, And<InventoryItem.defaultSubItemOnEntry, Equal<boolTrue>>>>))]
  [SubItem(typeof (INComponentTran.inventoryID), typeof (LeftJoin<INSiteStatusByCostCenter, On2<INSiteStatusByCostCenter.FK.SubItem, And<INSiteStatusByCostCenter.inventoryID, Equal<Optional<INComponentTran.inventoryID>>, And<INSiteStatusByCostCenter.siteID, Equal<Optional<INComponentTran.siteID>>, And<INSiteStatusByCostCenter.costCenterID, Equal<Optional<INComponentTran.costCenterID>>>>>>>))]
  [PXFormula(typeof (Default<INComponentTran.inventoryID>))]
  public override int? SubItemID
  {
    get => this._SubItemID;
    set => this._SubItemID = value;
  }

  [PXDefault(typeof (INKitRegister.siteID))]
  [PXDBInt]
  [PXSelector(typeof (Search<INSite.siteID>), SubstituteKey = typeof (INSite.siteCD))]
  public override int? SiteID
  {
    get => this._SiteID;
    set => this._SiteID = value;
  }

  [LocationAvail(typeof (INComponentTran.inventoryID), typeof (INComponentTran.subItemID), typeof (CostCenter.freeStock), typeof (INComponentTran.siteID), typeof (Where2<Where<INComponentTran.tranType, Equal<INTranType.assembly>, Or<INComponentTran.tranType, Equal<INTranType.disassembly>>>, And<INComponentTran.invtMult, Equal<shortMinus1>>>), typeof (Where2<Where<INComponentTran.tranType, Equal<INTranType.assembly>, Or<INComponentTran.tranType, Equal<INTranType.disassembly>>>, And<INComponentTran.invtMult, Equal<short1>>>), typeof (Where<False, Equal<True>>))]
  public override int? LocationID
  {
    get => this._LocationID;
    set => this._LocationID = value;
  }

  [PXDBPriceCost]
  [PXUIField(DisplayName = "Unit Cost")]
  [PXFormula(typeof (Default<INComponentTran.uOM>))]
  public override Decimal? UnitCost
  {
    get => this._UnitCost;
    set => this._UnitCost = value;
  }

  [PXDBBaseCury(null, null, BqlField = typeof (INTran.tranCost))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Ext. Cost")]
  [PXFormula(typeof (Mult<INComponentTran.qty, INComponentTran.unitCost>), typeof (SumCalc<INKitRegister.totalCostStock>))]
  public override Decimal? TranCost { get; set; }

  [PXDBString(20, IsUnicode = true, BqlField = typeof (INTran.reasonCode))]
  [PXSelector(typeof (Search<PX.Objects.CS.ReasonCode.reasonCodeID, Where<PX.Objects.CS.ReasonCode.usage, Equal<ReasonCodeUsages.assemblyDisassembly>>>))]
  [PXUIField(DisplayName = "Reason Code")]
  public override string ReasonCode
  {
    get => this._ReasonCode;
    set => this._ReasonCode = value;
  }

  [PXDBString(256 /*0x0100*/, IsUnicode = true, BqlField = typeof (INTran.tranDesc))]
  [PXUIField(DisplayName = "Description")]
  [PXDefault(typeof (Search<InventoryItem.descr, Where<InventoryItem.inventoryID, Equal<Current<INComponentTran.inventoryID>>>>))]
  public override string TranDesc
  {
    get => this._TranDesc;
    set => this._TranDesc = value;
  }

  /// <exclude />
  [PXDBInt(BqlField = typeof (INTran.costCenterID))]
  [PXDefault(typeof (CostCenter.freeStock))]
  public override int? CostCenterID { get; set; }

  public new class PK : 
    PrimaryKeyOf<INComponentTran>.By<INComponentTran.docType, INComponentTran.refNbr, INComponentTran.lineNbr>
  {
    public static INComponentTran Find(
      PXGraph graph,
      string docType,
      string refNbr,
      int? lineNbr,
      PKFindOptions options = 0)
    {
      return (INComponentTran) PrimaryKeyOf<INComponentTran>.By<INComponentTran.docType, INComponentTran.refNbr, INComponentTran.lineNbr>.FindBy(graph, (object) docType, (object) refNbr, (object) lineNbr, options);
    }
  }

  public new static class FK
  {
    public class Branch : 
      PrimaryKeyOf<PX.Objects.GL.Branch>.By<PX.Objects.GL.Branch.branchID>.ForeignKeyOf<INComponentTran>.By<INComponentTran.branchID>
    {
    }

    public class KitRegister : 
      PrimaryKeyOf<INKitRegister>.By<INKitRegister.docType, INKitRegister.refNbr>.ForeignKeyOf<INComponentTran>.By<INComponentTran.docType, INComponentTran.refNbr>
    {
    }

    public class Project : 
      PrimaryKeyOf<PMProject>.By<PMProject.contractID>.ForeignKeyOf<INComponentTran>.By<INComponentTran.projectID>
    {
    }

    public class InventoryItem : 
      PrimaryKeyOf<InventoryItem>.By<InventoryItem.inventoryID>.ForeignKeyOf<INComponentTran>.By<INComponentTran.inventoryID>
    {
    }

    public class SubItem : 
      PrimaryKeyOf<INSubItem>.By<INSubItem.subItemID>.ForeignKeyOf<INComponentTran>.By<INComponentTran.subItemID>
    {
    }

    public class Site : 
      PrimaryKeyOf<INSite>.By<INSite.siteID>.ForeignKeyOf<INComponentTran>.By<INComponentTran.siteID>
    {
    }

    public class Location : 
      PrimaryKeyOf<INLocation>.By<INLocation.locationID>.ForeignKeyOf<INComponentTran>.By<INComponentTran.locationID>
    {
    }

    public class ReasonCode : 
      PrimaryKeyOf<PX.Objects.CS.ReasonCode>.By<PX.Objects.CS.ReasonCode.reasonCodeID>.ForeignKeyOf<INComponentTran>.By<INComponentTran.reasonCode>
    {
    }
  }

  public new abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INComponentTran.branchID>
  {
  }

  public new abstract class docType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INComponentTran.docType>
  {
  }

  public new abstract class origModule : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INComponentTran.origModule>
  {
  }

  public new abstract class tranType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INComponentTran.tranType>
  {
  }

  public new abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INComponentTran.refNbr>
  {
  }

  public new abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INComponentTran.lineNbr>
  {
  }

  public new abstract class assyType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INComponentTran.assyType>
  {
  }

  public new abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INComponentTran.projectID>
  {
  }

  public new abstract class tranDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INComponentTran.tranDate>
  {
  }

  public new abstract class finPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INComponentTran.finPeriodID>
  {
  }

  public new abstract class tranPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INComponentTran.tranPeriodID>
  {
  }

  public new abstract class invtMult : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  INComponentTran.invtMult>
  {
  }

  public new abstract class uOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INComponentTran.uOM>
  {
  }

  public new abstract class qty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INComponentTran.qty>
  {
  }

  public new abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INComponentTran.inventoryID>
  {
  }

  public new abstract class subItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INComponentTran.subItemID>
  {
  }

  public new abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INComponentTran.siteID>
  {
  }

  public new abstract class locationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INComponentTran.locationID>
  {
  }

  public new abstract class unitCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INComponentTran.unitCost>
  {
  }

  public new abstract class tranCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INComponentTran.tranCost>
  {
  }

  public new abstract class reasonCode : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INComponentTran.reasonCode>
  {
  }

  public new abstract class tranDesc : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INComponentTran.tranDesc>
  {
  }

  public new abstract class costCenterID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INComponentTran.costCenterID>
  {
  }
}
