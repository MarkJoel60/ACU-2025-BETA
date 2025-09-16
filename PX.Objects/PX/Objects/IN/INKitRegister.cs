// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INKitRegister
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CM;
using PX.Objects.Common;
using PX.Objects.Common.Attributes;
using PX.Objects.Common.Bql;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.PM;
using System;

#nullable enable
namespace PX.Objects.IN;

[PXPrimaryGraph(typeof (KitAssemblyEntry))]
[PXCacheName("IN Kit")]
[PXProjection(typeof (Select2<INRegister, InnerJoin<INTran, On<INRegister.FK.KitTran>>>), Persistent = true)]
public class INKitRegister : 
  PXBqlTable,
  IBqlTable,
  IBqlTableSystemDataStorage,
  ILSPrimary,
  ILSMaster,
  IItemPlanMaster,
  IItemPlanRegister
{
  [Branch(typeof (Search<INSite.branchID, Where<INSite.siteID, Equal<Current<INKitRegister.siteID>>>>), null, true, true, true, IsDetail = false, BqlField = typeof (INRegister.branchID), Enabled = false)]
  [PXDependsOnFields(new Type[] {typeof (INKitRegister.tranBranchID)})]
  public virtual int? BranchID { get; set; }

  [PXString(5, IsUnicode = true)]
  [PXFormula(typeof (Selector<INKitRegister.branchID, PX.Objects.GL.Branch.baseCuryID>))]
  [PXUIField(DisplayName = "Currency", Enabled = false, FieldClass = "MultipleBaseCurrencies")]
  public virtual 
  #nullable disable
  string BranchBaseCuryID { get; set; }

  [PXDBString(1, IsKey = true, IsFixed = true, BqlField = typeof (INRegister.docType))]
  [PXDefault("P")]
  [INDocType.KitList]
  [PXUIField]
  [PXDependsOnFields(new Type[] {typeof (INKitRegister.tranDocType)})]
  public virtual string DocType { get; set; }

  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = ">CCCCCCCCCCCCCCC", BqlField = typeof (INRegister.refNbr))]
  [PXDefault]
  [PXUIField]
  [PXSelector(typeof (Search<INKitRegister.refNbr, Where<INKitRegister.docType, Equal<Optional<INKitRegister.docType>>>, OrderBy<Desc<INKitRegister.refNbr>>>), Filterable = true)]
  [AutoNumber(typeof (INKitRegister.docType), typeof (INKitRegister.tranDate), new string[] {"P", "C", "D"}, new Type[] {typeof (INSetup.kitAssemblyNumberingID), typeof (INSetup.kitAssemblyNumberingID), typeof (INSetup.kitAssemblyNumberingID)})]
  [PXDependsOnFields(new Type[] {typeof (INKitRegister.tranRefNbr)})]
  public virtual string RefNbr { get; set; }

  [PXDBString(2, IsFixed = true, BqlField = typeof (INRegister.origModule))]
  [PXDefault("IN")]
  [PXDependsOnFields(new Type[] {typeof (INKitRegister.tranOrigModule)})]
  public virtual string OrigModule { get; set; }

  [PXDBString(256 /*0x0100*/, IsUnicode = true, BqlField = typeof (INRegister.tranDesc))]
  [PXUIField(DisplayName = "Description")]
  [PXFormula(typeof (INKitRegister.tranTranDesc))]
  public virtual string TranDesc { get; set; }

  [PXDBBool(BqlField = typeof (INRegister.released))]
  [PXDefault(false)]
  [NoUpdateDBField(NoInsert = true)]
  public virtual bool? Released { get; set; }

  [PXDBBool(BqlField = typeof (INRegister.hold))]
  [PXDefault(typeof (INSetup.holdEntry))]
  [PXUIField(DisplayName = "Hold", Enabled = false)]
  public virtual bool? Hold { get; set; }

  [PXDBString(1, IsFixed = true, BqlField = typeof (INRegister.status))]
  [PXDefault]
  [PXUIField]
  [INDocStatus.List]
  public virtual string Status { get; set; }

  [PXDBDate(BqlField = typeof (INRegister.tranDate))]
  [PXDefault(typeof (AccessInfo.businessDate))]
  [PXUIField]
  [PXDependsOnFields(new Type[] {typeof (INKitRegister.tranTranDate)})]
  public virtual DateTime? TranDate { get; set; }

  [PXDBString(1, IsFixed = true, BqlField = typeof (INRegister.transferType))]
  [PXDefault("1")]
  [INTransferType.List]
  [PXUIField(DisplayName = "Transfer Type")]
  public virtual string TransferType { get; set; }

  [INOpenPeriod(typeof (INKitRegister.tranDate), typeof (INKitRegister.siteID), typeof (Selector<INKitRegister.siteID, INSite.branchID>), null, null, null, true, FinPeriodSelectorAttribute.SelectionModesWithRestrictions.Undefined, typeof (INKitRegister.tranPeriodID), IsHeader = true, BqlField = typeof (INRegister.finPeriodID))]
  [PXDefault]
  [PXUIField]
  [PXDependsOnFields(new Type[] {typeof (INKitRegister.tranFinPeriodID)})]
  public virtual string FinPeriodID { get; set; }

  [PeriodID(null, null, null, true, BqlField = typeof (INRegister.tranPeriodID))]
  public virtual string TranPeriodID { get; set; }

  [PXDBInt(BqlField = typeof (INRegister.lineCntr))]
  [PXDefault(1)]
  public virtual int? LineCntr { get; set; }

  [PXDBQuantity(BqlField = typeof (INRegister.totalQty))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  [PXFormula(typeof (INKitRegister.qty))]
  public virtual Decimal? TotalQty { get; set; }

  [PXDBBaseCury(null, null, BqlField = typeof (INRegister.totalAmount))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  [PXFormula(typeof (INKitRegister.tranAmt))]
  public virtual Decimal? TotalAmount { get; set; }

  [PXDBBaseCury(null, null, BqlField = typeof (INRegister.totalCost))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  [PXFormula(typeof (INKitRegister.tranCost))]
  public virtual Decimal? TotalCost { get; set; }

  [PXBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? TotalCostStock { get; set; }

  [PXBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? TotalCostNonStock { get; set; }

  [PXDBQuantity(BqlField = typeof (INRegister.controlQty))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Control Qty.")]
  public virtual Decimal? ControlQty { get; set; }

  [PXDBBaseCury(null, null, BqlField = typeof (INRegister.controlAmount))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Control Amount")]
  public virtual Decimal? ControlAmount { get; set; }

  [PXDBBaseCury(null, null, BqlField = typeof (INRegister.controlCost))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Control Cost")]
  public virtual Decimal? ControlCost { get; set; }

  [PXDefault]
  [PXDBInt(BqlField = typeof (INRegister.kitInventoryID))]
  [PXUIField]
  [PXDimensionSelector("INVENTORY", typeof (Search<InventoryItem.inventoryID, Where<InventoryItem.stkItem, Equal<True>, And2<Match<Current<AccessInfo.userName>>, And<Exists<Select<INKitSpecHdr, Where<INKitSpecHdr.kitInventoryID, Equal<InventoryItem.inventoryID>, And<INKitSpecHdr.isActive, Equal<True>>>>>>>>>), typeof (InventoryItem.inventoryCD), DescriptionField = typeof (InventoryItem.descr))]
  [PXDependsOnFields(new Type[] {typeof (INKitRegister.inventoryID)})]
  public virtual int? KitInventoryID { get; set; }

  [PXDefault]
  [PXDBString(10, IsUnicode = true, InputMask = ">aaaaaaaaaa", BqlField = typeof (INRegister.kitRevisionID))]
  [PXUIField]
  [PXRestrictor(typeof (Where<INKitSpecHdr.isActive, Equal<True>>), "Revision '{0}' is inactive", new Type[] {typeof (INKitSpecHdr.revisionID)})]
  [PXSelector(typeof (Search<INKitSpecHdr.revisionID, Where<INKitSpecHdr.kitInventoryID, Equal<Current<INKitRegister.kitInventoryID>>>>), new Type[] {typeof (INKitSpecHdr.kitInventoryID), typeof (INKitSpecHdr.descr)})]
  public virtual string KitRevisionID { get; set; }

  [PXDBInt(BqlField = typeof (INRegister.kitLineNbr))]
  [PXDefault(0)]
  [PXDependsOnFields(new Type[] {typeof (INKitRegister.lineNbr)})]
  public virtual int? KitLineNbr { get; set; }

  /// <inheritdoc cref="P:PX.Objects.IN.INRegister.KitRequestDate" />
  [PXDBDate(BqlField = typeof (INRegister.kitRequestDate))]
  [PXDefault(typeof (AccessInfo.businessDate))]
  [PXUIField(DisplayName = "Requested On")]
  public virtual DateTime? KitRequestDate { get; set; }

  [PXDBString(15, IsUnicode = true, BqlField = typeof (INRegister.batchNbr))]
  [PXUIField]
  [PXSelector(typeof (Search<Batch.batchNbr, Where<Batch.module, Equal<BatchModule.moduleIN>>>))]
  public virtual string BatchNbr { get; set; }

  [PXString(1)]
  [PXFormula(typeof (Selector<INKitRegister.kitInventoryID, Selector<InventoryItem.lotSerClassID, INLotSerClass.lotSerTrack>>))]
  public virtual string LotSerTrack { get; set; }

  [PXDBCreatedByID(BqlField = typeof (INRegister.createdByID))]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID(BqlField = typeof (INRegister.createdByScreenID))]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime(BqlField = typeof (INRegister.createdDateTime))]
  [PXUIField(DisplayName = "Created On", Enabled = false)]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID(BqlField = typeof (INRegister.lastModifiedByID))]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID(BqlField = typeof (INRegister.lastModifiedByScreenID))]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime(BqlField = typeof (INRegister.lastModifiedDateTime))]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  [PXSearchable(32 /*0x20*/, "Kit: {0} {1}", new Type[] {typeof (INKitRegister.docType), typeof (INKitRegister.refNbr), typeof (INKitRegister.tranDocType), typeof (INKitRegister.tranType), typeof (INKitRegister.tranRefNbr)}, new Type[] {typeof (INKitRegister.tranDesc), typeof (INKitRegister.tranTranDesc)}, NumberFields = new Type[] {typeof (INKitRegister.refNbr)}, Line1Format = "{1}{2}{3:d}{4}", Line1Fields = new Type[] {typeof (INKitRegister.kitInventoryID), typeof (InventoryItem.inventoryCD), typeof (INKitRegister.kitRevisionID), typeof (INKitRegister.tranDate), typeof (INKitRegister.status)}, Line2Format = "{0}", Line2Fields = new Type[] {typeof (INKitRegister.tranTranDesc)}, SelectForFastIndexing = typeof (Select<INKitRegister, Where<INKitRegister.docType, Equal<INDocType.production>, And<INKitRegister.docType, Equal<INDocType.disassembly>>>>))]
  [PXNote(BqlField = typeof (INRegister.noteID))]
  public virtual Guid? NoteID { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  [PXDBString(1, IsFixed = true, BqlField = typeof (INTran.docType))]
  [PXDefault]
  [PXRestriction]
  public virtual string TranDocType
  {
    get => this.DocType;
    set => this.DocType = value;
  }

  [PXDBString(2, IsFixed = true, BqlField = typeof (INTran.origModule))]
  [PXDefault]
  public virtual string TranOrigModule
  {
    get => this.OrigModule;
    set => this.OrigModule = value;
  }

  [PXDBString(3, IsFixed = true, BqlField = typeof (INTran.tranType))]
  [PXDefault]
  [PXFormula(typeof (Switch<Case<Where<INKitRegister.tranDocType, Equal<INDocType.disassembly>>, INTranType.disassembly>, INTranType.assembly>))]
  [INTranType.List]
  [PXUIField(DisplayName = "Tran. Type")]
  public virtual string TranType { get; set; }

  [PXDBString(15, IsUnicode = true, BqlField = typeof (INTran.refNbr))]
  [PXDefault]
  [PXRestriction]
  public virtual string TranRefNbr
  {
    get => this.RefNbr;
    set => this.RefNbr = value;
  }

  [PXDBInt(BqlField = typeof (INTran.branchID))]
  public virtual int? TranBranchID
  {
    get => this.BranchID;
    set => this.BranchID = value;
  }

  [PXDBInt(BqlField = typeof (INTran.lineNbr))]
  [PXDefault]
  [PXRestriction]
  public virtual int? LineNbr
  {
    get => this.KitLineNbr;
    set => this.KitLineNbr = value;
  }

  [PXDefault("K")]
  [PXDBString(1, IsFixed = true, BqlField = typeof (INTran.assyType))]
  public virtual string AssyType { get; set; }

  [ProjectDefault]
  [PXDBInt(BqlField = typeof (INTran.projectID))]
  public virtual int? ProjectID { get; set; }

  [PXDBInt(BqlField = typeof (INTran.taskID))]
  public virtual int? TaskID { get; set; }

  [PXDBDate(BqlField = typeof (INTran.tranDate))]
  public virtual DateTime? TranTranDate
  {
    get => this.TranDate;
    set => this.TranDate = value;
  }

  [PXDBShort(BqlField = typeof (INTran.invtMult))]
  [PXDefault(1)]
  public virtual short? InvtMult { get; set; }

  [PXDBBool(BqlField = typeof (INTran.isStockItem))]
  public virtual bool? IsStockItem { get; set; }

  /// <summary>
  /// This field declaration is for population of <see cref="P:PX.Objects.IN.INTran.InventoryID" /> within projection
  /// </summary>
  [PXDBInt(BqlField = typeof (INTran.inventoryID))]
  public virtual int? InventoryID
  {
    get => this.KitInventoryID;
    set => this.KitInventoryID = value;
  }

  [SubItem(typeof (INKitRegister.kitInventoryID), BqlField = typeof (INTran.subItemID))]
  public virtual int? SubItemID { get; set; }

  [PXDefault]
  [SiteAvail(typeof (INKitRegister.kitInventoryID), typeof (INKitRegister.subItemID), typeof (CostCenter.freeStock), BqlField = typeof (INTran.siteID))]
  [InterBranchRestrictor(typeof (Where<SameOrganizationBranch<INSite.branchID, Current<AccessInfo.branchID>>>))]
  public virtual int? SiteID { get; set; }

  [LocationAvail(typeof (INKitRegister.kitInventoryID), typeof (INKitRegister.subItemID), typeof (CostCenter.freeStock), typeof (INKitRegister.siteID), typeof (Where<False>), typeof (Where2<Where<INKitRegister.tranType, Equal<INTranType.assembly>, Or<INKitRegister.tranType, Equal<INTranType.disassembly>>>, And<INKitRegister.invtMult, Equal<short1>>>), typeof (Where<False, Equal<True>>), BqlField = typeof (INTran.locationID))]
  public virtual int? LocationID { get; set; }

  [PXDefault(typeof (Search<InventoryItem.baseUnit, Where<InventoryItem.inventoryID, Equal<Current<INKitRegister.kitInventoryID>>>>))]
  [INUnit(typeof (INKitRegister.kitInventoryID), BqlField = typeof (INTran.uOM))]
  public virtual string UOM { get; set; }

  [PXDBQuantity(typeof (INKitRegister.uOM), typeof (INKitRegister.baseQty), InventoryUnitType.BaseUnit, BqlField = typeof (INTran.qty))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Quantity")]
  public virtual Decimal? Qty { get; set; }

  [PXDBQuantity(BqlField = typeof (INTran.baseQty))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? BaseQty { get; set; }

  [PXDBDecimal(6, BqlField = typeof (INTran.unassignedQty))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? UnassignedQty { get; set; }

  [PXDBBool(BqlField = typeof (INTran.released))]
  [PXDefault(false)]
  public virtual bool? TranReleased { get; set; }

  [PXDefault]
  [PX.Objects.GL.FinPeriodID(typeof (INKitRegister.tranTranDate), typeof (INKitRegister.tranBranchID), null, null, null, null, true, false, null, typeof (INKitRegister.tranTranPeriodID), null, true, true, IsHeader = true, BqlField = typeof (INTran.finPeriodID))]
  public virtual string TranFinPeriodID
  {
    get => this.FinPeriodID;
    set => this.FinPeriodID = value;
  }

  [PX.Objects.GL.FinPeriodID(null, null, null, null, null, null, true, false, null, null, null, true, true, BqlField = typeof (INTran.tranPeriodID))]
  public virtual string TranTranPeriodID
  {
    get => this.TranPeriodID;
    set => this.TranPeriodID = value;
  }

  [PXDBPriceCost(BqlField = typeof (INTran.unitPrice))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Unit Price")]
  public virtual Decimal? UnitPrice { get; set; }

  [PXDBBaseCury(null, null, BqlField = typeof (INTran.tranAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Ext. Price")]
  public virtual Decimal? TranAmt { get; set; }

  [PXDBPriceCost(BqlField = typeof (INTran.unitCost))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Unit Cost")]
  public virtual Decimal? UnitCost { get; set; }

  [PXDBBaseCury(null, null, BqlField = typeof (INTran.tranCost))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Ext. Cost")]
  [PXFormula(typeof (Add<INKitRegister.totalCostStock, INKitRegister.totalCostNonStock>))]
  public virtual Decimal? TranCost { get; set; }

  [PXDBString(256 /*0x0100*/, IsUnicode = true, BqlField = typeof (INTran.tranDesc))]
  [PXUIField]
  [PXFormula(typeof (Selector<INKitRegister.kitInventoryID, InventoryItem.descr>))]
  public virtual string TranTranDesc { get; set; }

  [PXDBString(20, IsUnicode = true, BqlField = typeof (INTran.reasonCode))]
  [PXSelector(typeof (Search<PX.Objects.CS.ReasonCode.reasonCodeID, Where<PX.Objects.CS.ReasonCode.usage, Equal<ReasonCodeUsages.assemblyDisassembly>>>))]
  [PXUIField(DisplayName = "Reason Code")]
  [PXDefault(typeof (INSetup.assemblyDisassemblyReasonCode))]
  public virtual string ReasonCode { get; set; }

  [INLotSerialNbr(typeof (INKitRegister.kitInventoryID), typeof (INKitRegister.subItemID), typeof (INKitRegister.locationID), typeof (CostCenter.freeStock))]
  public virtual string LotSerialNbr { get; set; }

  [INExpireDate(typeof (INKitRegister.kitInventoryID))]
  public virtual DateTime? ExpireDate { get; set; }

  [PXDBBool(BqlField = typeof (INTran.updateShippedNotInvoiced))]
  [PXDefault(false)]
  public virtual bool? UpdateShippedNotInvoiced { get; set; }

  [PXDBBool(BqlField = typeof (INTran.isIntercompany))]
  [PXDefault(false)]
  public virtual bool? IsIntercompany { get; set; }

  [PXDBInt(BqlField = typeof (INTran.costCenterID))]
  [PXDefault(typeof (CostCenter.freeStock))]
  public virtual int? CostCenterID { get; set; }

  [PXDBInt(BqlField = typeof (INTran.toCostCenterID))]
  [PXDefault(typeof (CostCenter.freeStock))]
  public virtual int? ToCostCenterID { get; set; }

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

  [PXDBCreatedByID(BqlField = typeof (INTran.createdByID))]
  public virtual Guid? TranCreatedByID { get; set; }

  [PXDBCreatedByScreenID(BqlField = typeof (INTran.createdByScreenID))]
  public virtual string TranCreatedByScreenID { get; set; }

  [PXDBCreatedDateTime(BqlField = typeof (INTran.createdDateTime))]
  public virtual DateTime? TranCreatedDateTime { get; set; }

  [PXDBLastModifiedByID(BqlField = typeof (INTran.lastModifiedByID))]
  public virtual Guid? TranLastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID(BqlField = typeof (INTran.lastModifiedByScreenID))]
  public virtual string TranLastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime(BqlField = typeof (INTran.lastModifiedDateTime))]
  public virtual DateTime? TranLastModifiedDateTime { get; set; }

  [PXDBTimestamp]
  public virtual byte[] Trantstamp { get; set; }

  public int? ToSiteID => new int?();

  public class PK : PrimaryKeyOf<INKitRegister>.By<INKitRegister.docType, INKitRegister.refNbr>
  {
    public static INKitRegister Find(
      PXGraph graph,
      string docType,
      string refNbr,
      PKFindOptions options = 0)
    {
      return (INKitRegister) PrimaryKeyOf<INKitRegister>.By<INKitRegister.docType, INKitRegister.refNbr>.FindBy(graph, (object) docType, (object) refNbr, options);
    }
  }

  public static class FK
  {
    public class Register : 
      PrimaryKeyOf<INRegister>.By<INRegister.docType, INRegister.refNbr>.ForeignKeyOf<INRegister>.By<INKitRegister.docType, INKitRegister.refNbr>
    {
    }

    public class Tran : 
      PrimaryKeyOf<INTran>.By<INTran.docType, INTran.refNbr, INTran.lineNbr>.ForeignKeyOf<INTran>.By<INKitRegister.tranDocType, INKitRegister.tranRefNbr, INKitRegister.lineNbr>
    {
    }

    public class Site : 
      PrimaryKeyOf<INSite>.By<INSite.siteID>.ForeignKeyOf<INKitRegister>.By<INKitRegister.siteID>
    {
    }

    public class Location : 
      PrimaryKeyOf<INLocation>.By<INLocation.locationID>.ForeignKeyOf<INKitRegister>.By<INKitRegister.locationID>
    {
    }

    public class Branch : 
      PrimaryKeyOf<PX.Objects.GL.Branch>.By<PX.Objects.GL.Branch.branchID>.ForeignKeyOf<INKitRegister>.By<INKitRegister.branchID>
    {
    }

    public class KitInventoryItem : 
      PrimaryKeyOf<InventoryItem>.By<InventoryItem.inventoryID>.ForeignKeyOf<INKitRegister>.By<INKitRegister.kitInventoryID>
    {
    }

    public class KitTran : 
      PrimaryKeyOf<INTran>.By<INTran.docType, INTran.refNbr, INTran.lineNbr>.ForeignKeyOf<INKitRegister>.By<INKitRegister.docType, INKitRegister.refNbr, INKitRegister.kitLineNbr>
    {
    }

    public class KitSpecification : 
      PrimaryKeyOf<INKitSpecHdr>.By<INKitSpecHdr.kitInventoryID, INKitSpecHdr.revisionID>.ForeignKeyOf<INKitRegister>.By<INKitRegister.kitInventoryID, INKitRegister.kitRevisionID>
    {
    }

    public class Project : 
      PrimaryKeyOf<PMProject>.By<PMProject.contractID>.ForeignKeyOf<INKitRegister>.By<INKitRegister.projectID>
    {
    }

    public class Task : 
      PrimaryKeyOf<PMTask>.By<PMTask.projectID, PMTask.taskID>.ForeignKeyOf<INKitRegister>.By<INKitRegister.projectID, INKitRegister.taskID>
    {
    }

    public class InventoryItem : 
      PrimaryKeyOf<InventoryItem>.By<InventoryItem.inventoryID>.ForeignKeyOf<INKitRegister>.By<INKitRegister.inventoryID>
    {
    }

    public class SubItem : 
      PrimaryKeyOf<INSubItem>.By<INSubItem.subItemID>.ForeignKeyOf<INKitRegister>.By<INKitRegister.subItemID>
    {
    }

    public class ReasonCode : 
      PrimaryKeyOf<PX.Objects.CS.ReasonCode>.By<PX.Objects.CS.ReasonCode.reasonCodeID>.ForeignKeyOf<INKitRegister>.By<INKitRegister.reasonCode>
    {
    }
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INKitRegister.branchID>
  {
  }

  public abstract class branchBaseCuryID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INKitRegister.branchBaseCuryID>
  {
  }

  public abstract class docType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INKitRegister.docType>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INKitRegister.refNbr>
  {
  }

  public abstract class origModule : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INKitRegister.origModule>
  {
  }

  public abstract class tranDesc : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INKitRegister.tranDesc>
  {
  }

  public abstract class released : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  INKitRegister.released>
  {
  }

  public abstract class hold : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  INKitRegister.hold>
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INKitRegister.status>
  {
  }

  public abstract class tranDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  INKitRegister.tranDate>
  {
  }

  public abstract class transferType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INKitRegister.transferType>
  {
  }

  public abstract class finPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INKitRegister.finPeriodID>
  {
  }

  public abstract class tranPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INKitRegister.tranPeriodID>
  {
  }

  public abstract class lineCntr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INKitRegister.lineCntr>
  {
  }

  public abstract class totalQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INKitRegister.totalQty>
  {
  }

  public abstract class totalAmount : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INKitRegister.totalAmount>
  {
  }

  public abstract class totalCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INKitRegister.totalCost>
  {
  }

  public abstract class totalCostStock : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INKitRegister.totalCostStock>
  {
  }

  public abstract class totalCostNonStock : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INKitRegister.totalCostNonStock>
  {
  }

  public abstract class controlQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INKitRegister.controlQty>
  {
  }

  public abstract class controlAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INKitRegister.controlAmount>
  {
  }

  public abstract class controlCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INKitRegister.controlCost>
  {
  }

  public abstract class kitInventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INKitRegister.kitInventoryID>
  {
  }

  public abstract class kitRevisionID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INKitRegister.kitRevisionID>
  {
  }

  public abstract class kitLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INKitRegister.kitLineNbr>
  {
  }

  public abstract class kitRequestDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INKitRegister.kitRequestDate>
  {
  }

  public abstract class batchNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INKitRegister.batchNbr>
  {
  }

  public abstract class lotSerTrack : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INKitRegister.lotSerTrack>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  INKitRegister.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INKitRegister.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INKitRegister.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    INKitRegister.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INKitRegister.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INKitRegister.lastModifiedDateTime>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  INKitRegister.noteID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  INKitRegister.Tstamp>
  {
  }

  public abstract class tranDocType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INKitRegister.tranDocType>
  {
  }

  public abstract class tranOrigModule : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INKitRegister.tranOrigModule>
  {
  }

  public abstract class tranType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INKitRegister.tranType>
  {
  }

  public abstract class tranRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INKitRegister.tranRefNbr>
  {
  }

  public abstract class tranBranchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INKitRegister.tranBranchID>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INKitRegister.lineNbr>
  {
  }

  public abstract class assyType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INKitRegister.assyType>
  {
  }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INKitRegister.projectID>
  {
  }

  public abstract class taskID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INKitRegister.taskID>
  {
  }

  public abstract class tranTranDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INKitRegister.tranTranDate>
  {
  }

  public abstract class invtMult : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  INKitRegister.invtMult>
  {
  }

  public abstract class isStockItem : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  INKitRegister.isStockItem>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INKitRegister.inventoryID>
  {
  }

  public abstract class subItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INKitRegister.subItemID>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INKitRegister.siteID>
  {
  }

  public abstract class locationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INKitRegister.locationID>
  {
  }

  public abstract class uOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INKitRegister.uOM>
  {
  }

  public abstract class qty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INKitRegister.qty>
  {
  }

  public abstract class baseQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INKitRegister.baseQty>
  {
  }

  public abstract class unassignedQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INKitRegister.unassignedQty>
  {
  }

  public abstract class tranReleased : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  INKitRegister.tranReleased>
  {
  }

  public abstract class tranFinPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INKitRegister.tranFinPeriodID>
  {
  }

  public abstract class tranTranPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INKitRegister.tranTranPeriodID>
  {
  }

  public abstract class unitPrice : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INKitRegister.unitPrice>
  {
  }

  public abstract class tranAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INKitRegister.tranAmt>
  {
  }

  public abstract class unitCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INKitRegister.unitCost>
  {
  }

  public abstract class tranCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INKitRegister.tranCost>
  {
  }

  public abstract class tranTranDesc : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INKitRegister.tranTranDesc>
  {
  }

  public abstract class reasonCode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INKitRegister.reasonCode>
  {
  }

  public abstract class lotSerialNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INKitRegister.lotSerialNbr>
  {
  }

  public abstract class expireDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  INKitRegister.expireDate>
  {
  }

  public abstract class updateShippedNotInvoiced : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INKitRegister.updateShippedNotInvoiced>
  {
  }

  public abstract class isIntercompany : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  INKitRegister.isIntercompany>
  {
  }

  public abstract class costCenterID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INKitRegister.costCenterID>
  {
  }

  public abstract class toCostCenterID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INKitRegister.toCostCenterID>
  {
  }

  public abstract class costLayerType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INKitRegister.costLayerType>
  {
  }

  public abstract class toCostLayerType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INKitRegister.toCostLayerType>
  {
  }

  public abstract class inventorySource : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INKitRegister.inventorySource>
  {
  }

  public abstract class toInventorySource : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INKitRegister.toInventorySource>
  {
  }

  public abstract class trancreatedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    INKitRegister.trancreatedByID>
  {
  }

  public abstract class trancreatedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INKitRegister.trancreatedByScreenID>
  {
  }

  public abstract class trancreatedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INKitRegister.trancreatedDateTime>
  {
  }

  public abstract class tranlastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    INKitRegister.tranlastModifiedByID>
  {
  }

  public abstract class tranlastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INKitRegister.tranlastModifiedByScreenID>
  {
  }

  public abstract class tranlastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INKitRegister.tranlastModifiedDateTime>
  {
  }

  public abstract class tranTstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  INKitRegister.tranTstamp>
  {
  }
}
