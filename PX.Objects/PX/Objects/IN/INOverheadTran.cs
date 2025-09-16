// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INOverheadTran
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

[PXProjection(typeof (Select<INTran, Where<INTran.assyType, Equal<INAssyType.overheadTran>>>), Persistent = true)]
[PXCacheName("IN Overhead")]
public class INOverheadTran : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [Branch(typeof (INKitRegister.branchID), null, true, true, true)]
  public virtual int? BranchID { get; set; }

  [PXDBString(1, IsFixed = true, IsKey = true, BqlField = typeof (INTran.docType))]
  [PXDefault(typeof (INKitRegister.docType))]
  public virtual 
  #nullable disable
  string DocType { get; set; }

  [PXDBString(2, IsFixed = true, BqlField = typeof (INTran.origModule))]
  [PXDBDefault(typeof (INKitRegister.origModule))]
  public virtual string OrigModule { get; set; }

  [PXDBString(3, IsFixed = true, BqlField = typeof (INTran.tranType))]
  [PXDefault]
  [PXFormula(typeof (Switch<Case<Where<INOverheadTran.docType, Equal<INDocType.disassembly>>, INTranType.disassembly>, INTranType.assembly>))]
  public virtual string TranType { get; set; }

  [PXDBString(15, IsUnicode = true, IsKey = true, BqlField = typeof (INTran.refNbr))]
  [PXDBDefault(typeof (INKitRegister.refNbr))]
  [PXParent(typeof (Select<INKitRegister, Where<INKitRegister.docType, Equal<Current<INOverheadTran.docType>>, And<INKitRegister.refNbr, Equal<Current<INOverheadTran.refNbr>>>>>))]
  public virtual string RefNbr { get; set; }

  [PXDBInt(IsKey = true, BqlField = typeof (INTran.lineNbr))]
  [PXDefault]
  [PXLineNbr(typeof (INKitRegister.lineCntr))]
  public virtual int? LineNbr { get; set; }

  [PXDefault("O")]
  [PXDBString(1, IsFixed = true, BqlField = typeof (INTran.assyType))]
  public virtual string AssyType { get; set; }

  [ProjectDefault]
  [PXDBInt(BqlField = typeof (INTran.projectID))]
  public virtual int? ProjectID { get; set; }

  [PXDBDate(BqlField = typeof (INTran.tranDate))]
  [PXDBDefault(typeof (INKitRegister.tranDate))]
  public virtual DateTime? TranDate { get; set; }

  [NonStockItem(BqlField = typeof (INTran.inventoryID), DisplayName = "Inventory ID")]
  public virtual int? InventoryID { get; set; }

  [SubItem(typeof (INOverheadTran.inventoryID), BqlField = typeof (INTran.subItemID))]
  public virtual int? SubItemID { get; set; }

  [PXDefault(typeof (INKitRegister.siteID))]
  [PXDBInt(BqlField = typeof (INTran.siteID))]
  public virtual int? SiteID { get; set; }

  [LocationAvail(typeof (INOverheadTran.inventoryID), typeof (INOverheadTran.subItemID), typeof (CostCenter.freeStock), typeof (INOverheadTran.siteID), typeof (INOverheadTran.tranType), typeof (INOverheadTran.invtMult), BqlField = typeof (INTran.locationID))]
  public virtual int? LocationID { get; set; }

  [PXDBShort(BqlField = typeof (INTran.invtMult))]
  [PXDefault]
  public virtual short? InvtMult { get; set; }

  [PXDefault(typeof (Search<InventoryItem.baseUnit, Where<InventoryItem.inventoryID, Equal<Current<INOverheadTran.inventoryID>>>>))]
  [INUnit(typeof (INOverheadTran.inventoryID), BqlField = typeof (INTran.uOM))]
  public virtual string UOM { get; set; }

  [PXDBQuantity(typeof (INOverheadTran.uOM), typeof (INOverheadTran.baseQty), InventoryUnitType.BaseUnit, BqlField = typeof (INTran.qty))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Quantity")]
  public virtual Decimal? Qty { get; set; }

  [PXDBDefault(typeof (INKitRegister.finPeriodID))]
  [PX.Objects.GL.FinPeriodID(null, typeof (INOverheadTran.branchID), null, null, null, null, true, false, null, typeof (INOverheadTran.tranPeriodID), typeof (INKitRegister.tranPeriodID), true, true, BqlField = typeof (INTran.finPeriodID))]
  public virtual string FinPeriodID { get; set; }

  [PXDBString(256 /*0x0100*/, IsUnicode = true, BqlField = typeof (INTran.tranDesc))]
  [PXUIField(DisplayName = "Description")]
  [PXDefault(typeof (Search<InventoryItem.descr, Where<InventoryItem.inventoryID, Equal<Current<INOverheadTran.inventoryID>>>>))]
  public virtual string TranDesc { get; set; }

  [PXDBString(20, IsUnicode = true, BqlField = typeof (INTran.reasonCode))]
  [PXSelector(typeof (Search<PX.Objects.CS.ReasonCode.reasonCodeID, Where<PX.Objects.CS.ReasonCode.usage, Equal<ReasonCodeUsages.assemblyDisassembly>>>))]
  [PXUIField(DisplayName = "Reason Code")]
  public virtual string ReasonCode { get; set; }

  [PXDBQuantity(BqlField = typeof (INTran.baseQty))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? BaseQty { get; set; }

  [PXDBDecimal(6, BqlField = typeof (INTran.unassignedQty))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? UnassignedQty { get; set; }

  [PXDBBool(BqlField = typeof (INTran.released))]
  [PXDefault(false)]
  public virtual bool? Released { get; set; }

  [PXDBDefault(typeof (INKitRegister.tranPeriodID))]
  [PeriodID(null, null, null, true, BqlField = typeof (INTran.tranPeriodID))]
  public virtual string TranPeriodID { get; set; }

  [PXDBPriceCost(BqlField = typeof (INTran.unitPrice))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Unit Price")]
  public virtual Decimal? UnitPrice { get; set; }

  [PXDBBaseCury(null, null, BqlField = typeof (INTran.tranAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Ext. Price")]
  public virtual Decimal? TranAmt { get; set; }

  [PXDBPriceCost(BqlField = typeof (INTran.unitCost))]
  [PXUIField(DisplayName = "Unit Cost")]
  [PXFormula(typeof (Default<INOverheadTran.uOM>))]
  public virtual Decimal? UnitCost { get; set; }

  [PXDBBaseCury(null, null, BqlField = typeof (INTran.tranCost))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Ext. Cost")]
  [PXFormula(typeof (Mult<INOverheadTran.qty, INOverheadTran.unitCost>), typeof (SumCalc<INKitRegister.totalCostNonStock>))]
  public virtual Decimal? TranCost { get; set; }

  [PXDBBool(BqlField = typeof (INTran.updateShippedNotInvoiced))]
  [PXDefault(false)]
  public virtual bool? UpdateShippedNotInvoiced { get; set; }

  [PXDBString(1, IsFixed = true, BqlField = typeof (INTran.costLayerType))]
  [PXDefault("N")]
  public virtual string CostLayerType { get; set; }

  [PXDBString(1, IsFixed = true, BqlField = typeof (INTran.toCostLayerType))]
  [PXDefault("N")]
  public virtual string ToCostLayerType { get; set; }

  /// <inheritdoc cref="P:PX.Objects.IN.INTran.InventorySource" />
  [PXDBString(1, IsFixed = true, BqlField = typeof (INTran.inventorySource))]
  [PXDefault("F")]
  public virtual string InventorySource { get; set; }

  /// <inheritdoc cref="P:PX.Objects.IN.INTran.ToInventorySource" />
  [PXDBString(1, IsFixed = true, BqlField = typeof (INTran.toInventorySource))]
  [PXDefault("F")]
  public virtual string ToInventorySource { get; set; }

  [PXDBInt(BqlField = typeof (INTran.costCenterID))]
  [PXDefault(typeof (CostCenter.freeStock))]
  public virtual int? CostCenterID { get; set; }

  [PXDBInt(BqlField = typeof (INTran.toCostCenterID))]
  [PXDefault(typeof (CostCenter.freeStock))]
  public virtual int? ToCostCenterID { get; set; }

  [PXDBCreatedByID(BqlField = typeof (INTran.createdByID))]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID(BqlField = typeof (INTran.createdByScreenID))]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime(BqlField = typeof (INTran.createdDateTime))]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID(BqlField = typeof (INTran.lastModifiedByID))]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID(BqlField = typeof (INTran.lastModifiedByScreenID))]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime(BqlField = typeof (INTran.lastModifiedDateTime))]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  [PXDBTimestamp]
  public virtual byte[] Tstamp { get; set; }

  public class PK : 
    PrimaryKeyOf<INOverheadTran>.By<INOverheadTran.docType, INOverheadTran.refNbr, INOverheadTran.lineNbr>
  {
    public static INOverheadTran Find(
      PXGraph graph,
      string docType,
      string refNbr,
      int? lineNbr,
      PKFindOptions options = 0)
    {
      return (INOverheadTran) PrimaryKeyOf<INOverheadTran>.By<INOverheadTran.docType, INOverheadTran.refNbr, INOverheadTran.lineNbr>.FindBy(graph, (object) docType, (object) refNbr, (object) lineNbr, options);
    }
  }

  public static class FK
  {
    public class Branch : 
      PrimaryKeyOf<PX.Objects.GL.Branch>.By<PX.Objects.GL.Branch.branchID>.ForeignKeyOf<INOverheadTran>.By<INOverheadTran.branchID>
    {
    }

    public class KitRegister : 
      PrimaryKeyOf<INKitRegister>.By<INKitRegister.docType, INKitRegister.refNbr>.ForeignKeyOf<INOverheadTran>.By<INOverheadTran.docType, INOverheadTran.refNbr>
    {
    }

    public class Project : 
      PrimaryKeyOf<PMProject>.By<PMProject.contractID>.ForeignKeyOf<INOverheadTran>.By<INOverheadTran.projectID>
    {
    }

    public class InventoryItem : 
      PrimaryKeyOf<InventoryItem>.By<InventoryItem.inventoryID>.ForeignKeyOf<INOverheadTran>.By<INOverheadTran.inventoryID>
    {
    }

    public class SubItem : 
      PrimaryKeyOf<INSubItem>.By<INSubItem.subItemID>.ForeignKeyOf<INOverheadTran>.By<INOverheadTran.subItemID>
    {
    }

    public class Site : 
      PrimaryKeyOf<INSite>.By<INSite.siteID>.ForeignKeyOf<INOverheadTran>.By<INOverheadTran.siteID>
    {
    }

    public class Location : 
      PrimaryKeyOf<INLocation>.By<INLocation.locationID>.ForeignKeyOf<INOverheadTran>.By<INOverheadTran.locationID>
    {
    }

    public class ReasonCode : 
      PrimaryKeyOf<PX.Objects.CS.ReasonCode>.By<PX.Objects.CS.ReasonCode.reasonCodeID>.ForeignKeyOf<INOverheadTran>.By<INOverheadTran.reasonCode>
    {
    }
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INOverheadTran.branchID>
  {
  }

  public abstract class docType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INOverheadTran.docType>
  {
  }

  public abstract class origModule : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INOverheadTran.origModule>
  {
  }

  public abstract class tranType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INOverheadTran.tranType>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INOverheadTran.refNbr>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INOverheadTran.lineNbr>
  {
  }

  public abstract class assyType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INOverheadTran.assyType>
  {
  }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INOverheadTran.projectID>
  {
  }

  public abstract class tranDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  INOverheadTran.tranDate>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INOverheadTran.inventoryID>
  {
  }

  public abstract class subItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INOverheadTran.subItemID>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INOverheadTran.siteID>
  {
  }

  public abstract class locationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INOverheadTran.locationID>
  {
  }

  public abstract class invtMult : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  INOverheadTran.invtMult>
  {
  }

  public abstract class uOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INOverheadTran.uOM>
  {
  }

  public abstract class qty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INOverheadTran.qty>
  {
  }

  public abstract class finPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INOverheadTran.finPeriodID>
  {
  }

  public abstract class tranDesc : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INOverheadTran.tranDesc>
  {
  }

  public abstract class reasonCode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INOverheadTran.reasonCode>
  {
  }

  public abstract class baseQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INOverheadTran.baseQty>
  {
  }

  public abstract class unassignedQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INOverheadTran.unassignedQty>
  {
  }

  public abstract class released : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  INOverheadTran.released>
  {
  }

  public abstract class tranPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INOverheadTran.tranPeriodID>
  {
  }

  public abstract class unitPrice : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INOverheadTran.unitPrice>
  {
  }

  public abstract class tranAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INOverheadTran.tranAmt>
  {
  }

  public abstract class unitCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INOverheadTran.unitCost>
  {
  }

  public abstract class tranCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INOverheadTran.tranCost>
  {
  }

  public abstract class updateShippedNotInvoiced : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INOverheadTran.updateShippedNotInvoiced>
  {
  }

  public abstract class costLayerType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INOverheadTran.costLayerType>
  {
  }

  public abstract class toCostLayerType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INOverheadTran.toCostLayerType>
  {
  }

  public abstract class inventorySource : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INOverheadTran.inventorySource>
  {
  }

  public abstract class toInventorySource : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INOverheadTran.toInventorySource>
  {
  }

  public abstract class costCenterID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INOverheadTran.costCenterID>
  {
  }

  public abstract class toCostCenterID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INOverheadTran.toCostCenterID>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  INOverheadTran.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INOverheadTran.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INOverheadTran.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    INOverheadTran.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INOverheadTran.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INOverheadTran.lastModifiedDateTime>
  {
  }

  public abstract class tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  INOverheadTran.tstamp>
  {
  }
}
