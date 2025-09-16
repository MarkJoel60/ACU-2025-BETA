// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.POReceipt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.EP;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Data.WorkflowAPI;
using PX.Objects.AP;
using PX.Objects.AP.Standalone;
using PX.Objects.AR;
using PX.Objects.CM.Extensions;
using PX.Objects.Common;
using PX.Objects.Common.Bql;
using PX.Objects.Common.GraphExtensions.Abstract;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.IN;
using PX.Objects.PM;
using PX.TM;
using System;

#nullable enable
namespace PX.Objects.PO;

[PXPrimaryGraph(typeof (POReceiptEntry))]
[PXCacheName("Purchase Receipt")]
[PXGroupMask(typeof (LeftJoinSingleTable<PX.Objects.AP.Vendor, On<PX.Objects.AP.Vendor.bAccountID, Equal<POReceipt.vendorID>>>), WhereRestriction = typeof (Where<PX.Objects.AP.Vendor.bAccountID, IsNull, Or<Match<PX.Objects.AP.Vendor, Current<AccessInfo.userName>>>>))]
[Serializable]
public class POReceipt : 
  PXBqlTable,
  IBqlTable,
  IBqlTableSystemDataStorage,
  IAssign,
  INotable,
  ILotSerialTrackableDocument
{
  protected bool? _Selected = new bool?(false);
  protected 
  #nullable disable
  string _ReceiptType;
  protected string _ReceiptNbr;
  protected int? _VendorID;
  protected int? _VendorLocationID;
  protected int? _BranchID;
  protected DateTime? _ReceiptDate;
  protected DateTime? _InvoiceDate;
  protected string _FinPeriodID;
  protected bool? _Hold;
  protected bool? _Released;
  protected string _Status;
  protected Guid? _NoteID;
  protected string _CuryID;
  protected long? _CuryInfoID;
  protected int? _LineCntr;
  protected Decimal? _OrderQty;
  protected Decimal? _ControlQty;
  protected string _APDocType;
  protected string _APRefNbr;
  protected string _InvtDocType;
  protected string _InvtRefNbr;
  protected string _InvoiceNbr;
  protected bool? _AutoCreateInvoice;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;
  protected int? _WorkgroupID;
  protected int? _OwnerID;
  protected Decimal? _UnbilledQty;
  protected Decimal? _ReceiptWeight;
  protected Decimal? _ReceiptVolume;
  protected string _POType;
  protected int? _ShipToBAccountID;
  protected int? _ShipToLocationID;
  protected Decimal? _CuryOrderTotal;
  protected Decimal? _OrderTotal;

  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Selected")]
  public virtual bool? Selected
  {
    get => this._Selected;
    set => this._Selected = value;
  }

  [PXDBString(2, IsFixed = true, IsKey = true, InputMask = "")]
  [PXDefault("RT")]
  [POReceiptType.List]
  [PXUIField(DisplayName = "Type")]
  [PXFieldDescription]
  public virtual string ReceiptType
  {
    get => this._ReceiptType;
    set => this._ReceiptType = value;
  }

  [PXDBString(15, IsKey = true, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXDefault]
  [POReceiptType.Numbering]
  [POReceiptType.RefNbr(typeof (Search2<POReceipt.receiptNbr, LeftJoinSingleTable<PX.Objects.AP.Vendor, On<PX.Objects.AP.Vendor.bAccountID, Equal<POReceipt.vendorID>>>, Where<POReceipt.receiptType, Equal<Optional<POReceipt.receiptType>>, And<Where<PX.Objects.AP.Vendor.bAccountID, IsNull, Or<Match<PX.Objects.AP.Vendor, Current<AccessInfo.userName>>>>>>, OrderBy<Desc<POReceipt.receiptNbr>>>), Filterable = true)]
  [PXUIField]
  [PXFieldDescription]
  public virtual string ReceiptNbr
  {
    get => this._ReceiptNbr;
    set => this._ReceiptNbr = value;
  }

  [Site(DisplayName = "Warehouse", Required = true)]
  [PXForeignReference(typeof (POReceipt.FK.Site))]
  [InterBranchRestrictor(typeof (Where<SameOrganizationBranch<PX.Objects.IN.INSite.branchID, Current<AccessInfo.branchID>>>))]
  public int? SiteID { get; set; }

  [Vendor(typeof (Search<BAccountR.bAccountID, Where<True, Equal<True>>>))]
  [VerndorNonEmployeeOrOrganizationRestrictor(typeof (POReceipt.receiptType))]
  [PXRestrictor(typeof (Where<PX.Objects.AP.Vendor.vStatus, IsNull, Or<PX.Objects.AP.Vendor.vStatus, In3<VendorStatus.active, VendorStatus.oneTime, VendorStatus.holdPayments>>>), "The vendor status is '{0}'.", new System.Type[] {typeof (PX.Objects.AP.Vendor.vStatus)})]
  [PXDefault]
  [PXFormula(typeof (Switch<Case<Where<POReceipt.receiptType, Equal<POReceiptType.transferreceipt>>, Selector<POReceipt.siteID, Selector<PX.Objects.IN.INSite.branchID, PX.Objects.GL.Branch.branchCD>>>, POReceipt.vendorID>))]
  [PXForeignReference(typeof (POReceipt.FK.Vendor))]
  public virtual int? VendorID
  {
    get => this._VendorID;
    set => this._VendorID = value;
  }

  [LocationActive(typeof (Where<PX.Objects.CR.Location.bAccountID, Equal<Current<POReceipt.vendorID>>, And<MatchWithBranch<PX.Objects.CR.Location.vBranchID>>>))]
  [PXDefault(typeof (Coalesce<Search2<BAccountR.defLocationID, InnerJoin<PX.Objects.CR.Standalone.Location, On<PX.Objects.CR.Standalone.Location.bAccountID, Equal<BAccountR.bAccountID>, And<PX.Objects.CR.Standalone.Location.locationID, Equal<BAccountR.defLocationID>>>>, Where<BAccountR.bAccountID, Equal<Current<POReceipt.vendorID>>, And<PX.Objects.CR.Standalone.Location.isActive, Equal<True>, And<MatchWithBranch<PX.Objects.CR.Standalone.Location.vBranchID>>>>>, Search<PX.Objects.CR.Standalone.Location.locationID, Where<PX.Objects.CR.Standalone.Location.bAccountID, Equal<Current<POReceipt.vendorID>>, And<PX.Objects.CR.Standalone.Location.isActive, Equal<True>, And<MatchWithBranch<PX.Objects.CR.Standalone.Location.vBranchID>>>>>>))]
  [PXForeignReference(typeof (Field<POReceipt.vendorLocationID>.IsRelatedTo<PX.Objects.CR.Location.locationID>))]
  public virtual int? VendorLocationID
  {
    get => this._VendorLocationID;
    set => this._VendorLocationID = value;
  }

  [Branch(typeof (AccessInfo.branchID), null, true, true, true, IsDetail = false, TabOrder = 0)]
  [PXFormula(typeof (Switch<Case<Where<POReceipt.receiptType, Equal<POReceiptType.transferreceipt>>, Selector<POReceipt.siteID, PX.Objects.IN.INSite.branchID>, Case<Where<POReceipt.vendorLocationID, IsNotNull, And<Selector<POReceipt.vendorLocationID, PX.Objects.CR.Location.vBranchID>, IsNotNull>>, Selector<POReceipt.vendorLocationID, PX.Objects.CR.Location.vBranchID>, Case<Where<Current2<POReceipt.branchID>, IsNotNull>, Current2<POReceipt.branchID>>>>, Current<AccessInfo.branchID>>))]
  public virtual int? BranchID
  {
    get => this._BranchID;
    set => this._BranchID = value;
  }

  [PXDBDate]
  [PXDefault(typeof (AccessInfo.businessDate))]
  [PXUIField]
  public virtual DateTime? ReceiptDate
  {
    get => this._ReceiptDate;
    set => this._ReceiptDate = value;
  }

  [PXDBDate]
  [PXUIField]
  [PXFormula(typeof (POReceipt.receiptDate))]
  [PXDefault(typeof (POReceipt.receiptDate))]
  public virtual DateTime? InvoiceDate
  {
    get => this._InvoiceDate;
    set => this._InvoiceDate = value;
  }

  [POOpenPeriod(typeof (POReceipt.receiptDate), typeof (POReceipt.branchID), null, null, null, null, true, false, typeof (POReceipt.tranPeriodID), IsHeader = true)]
  [PXDefault]
  [PXUIField]
  public virtual string FinPeriodID
  {
    get => this._FinPeriodID;
    set => this._FinPeriodID = value;
  }

  [PeriodID(null, null, null, true)]
  public virtual string TranPeriodID { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "Hold", Enabled = false)]
  [PXDefault(true, typeof (Search<POSetup.holdReceipts>))]
  public virtual bool? Hold
  {
    get => this._Hold;
    set => this._Hold = value;
  }

  [PXDBBool]
  [PXUIField(DisplayName = "Released")]
  [PXDefault(false)]
  public virtual bool? Released
  {
    get => this._Released;
    set => this._Released = value;
  }

  /// <summary>
  /// Indicates that the receipt was counted by the warehouse worker and it is under manager verification.
  /// </summary>
  [PXDBBool]
  [PXUIField(DisplayName = "Received")]
  [PXDefault(false)]
  public virtual bool? Received { get; set; }

  [PXDBString(1, IsFixed = true)]
  [PXDefault("H")]
  [PXUIField]
  [POReceiptStatus.List]
  public virtual string Status
  {
    get => this._Status;
    set => this._Status = value;
  }

  [PXSearchable(128 /*0x80*/, "{0}: {1} - {3}", new System.Type[] {typeof (POReceipt.receiptType), typeof (POReceipt.receiptNbr), typeof (POReceipt.vendorID), typeof (PX.Objects.AP.Vendor.acctName)}, new System.Type[] {typeof (POReceipt.invoiceNbr)}, NumberFields = new System.Type[] {typeof (POReceipt.receiptNbr)}, Line1Format = "{0:d}{1}{2}", Line1Fields = new System.Type[] {typeof (POReceipt.receiptDate), typeof (POReceipt.status), typeof (POReceipt.invoiceNbr)}, Line2Format = "{0}", Line2Fields = new System.Type[] {typeof (POReceipt.orderQty)}, MatchWithJoin = typeof (InnerJoin<BAccountR, On<BAccountR.bAccountID, Equal<POReceipt.vendorID>>>), SelectForFastIndexing = typeof (Select2<POReceipt, InnerJoin<PX.Objects.AP.Vendor, On<POReceipt.vendorID, Equal<PX.Objects.AP.Vendor.bAccountID>>>>))]
  [PXNote(ShowInReferenceSelector = true, Selector = typeof (Search2<POReceipt.receiptNbr, LeftJoinSingleTable<PX.Objects.AP.Vendor, On<PX.Objects.AP.Vendor.bAccountID, Equal<POReceipt.vendorID>>>, Where<PX.Objects.AP.Vendor.bAccountID, IsNull, Or<Match<PX.Objects.AP.Vendor, Current<AccessInfo.userName>>>>, OrderBy<Desc<POReceipt.receiptNbr>>>))]
  public virtual Guid? NoteID
  {
    get => this._NoteID;
    set => this._NoteID = value;
  }

  [PXDBString(5, IsUnicode = true, InputMask = ">LLLLL")]
  [PXUIField]
  [PXDefault(typeof (Current<AccessInfo.baseCuryID>))]
  [PXSelector(typeof (PX.Objects.CM.Extensions.Currency.curyID))]
  public virtual string CuryID
  {
    get => this._CuryID;
    set => this._CuryID = value;
  }

  [PXDBLong]
  [CurrencyInfo]
  public virtual long? CuryInfoID
  {
    get => this._CuryInfoID;
    set => this._CuryInfoID = value;
  }

  [PXDBInt]
  [PXDefault(0)]
  public virtual int? LineCntr
  {
    get => this._LineCntr;
    set => this._LineCntr = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Total Qty.", Enabled = false)]
  public virtual Decimal? OrderQty
  {
    get => this._OrderQty;
    set => this._OrderQty = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Control Qty.")]
  public virtual Decimal? ControlQty
  {
    get => this._ControlQty;
    set => this._ControlQty = value;
  }

  [PXDBString(3, IsFixed = true)]
  [PXDefault]
  [PX.Objects.AP.APDocType.List]
  [PXUIField]
  public virtual string APDocType
  {
    get => this._APDocType;
    set => this._APDocType = value;
  }

  [PXDBString(15, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXUIField]
  [APInvoiceType.RefNbr(typeof (Search2<APRegisterAlias.refNbr, InnerJoinSingleTable<PX.Objects.AP.APInvoice, On<PX.Objects.AP.APInvoice.docType, Equal<APRegisterAlias.docType>, And<PX.Objects.AP.APInvoice.refNbr, Equal<APRegisterAlias.refNbr>>>, InnerJoin<PX.Objects.AP.Vendor, On<APRegisterAlias.vendorID, Equal<PX.Objects.AP.Vendor.bAccountID>>>>, Where<APRegisterAlias.docType, Equal<Current<POReceipt.aPDocType>>, And<PX.Objects.AP.Vendor.bAccountID, Equal<Current<POReceipt.vendorID>>>>>), Filterable = true)]
  public virtual string APRefNbr
  {
    get => this._APRefNbr;
    set => this._APRefNbr = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXUIField(DisplayName = "Inventory Doc. Type", Enabled = false)]
  [INDocType.List]
  public virtual string InvtDocType
  {
    get => this._InvtDocType;
    set => this._InvtDocType = value;
  }

  [PXDBString(15, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Inventory Ref. Nbr.", Enabled = false)]
  [PXSelector(typeof (Search<PX.Objects.IN.INRegister.refNbr, Where<PX.Objects.IN.INRegister.docType, Equal<Current<POReceipt.invtDocType>>>>))]
  public virtual string InvtRefNbr
  {
    get => this._InvtRefNbr;
    set => this._InvtRefNbr = value;
  }

  [PXDBString(40, IsUnicode = true)]
  [PXDefault]
  [PXUIField]
  [POVendorRefNbr]
  public virtual string InvoiceNbr
  {
    get => this._InvoiceNbr;
    set => this._InvoiceNbr = value;
  }

  [PXDBBool]
  [PXUIField]
  [PXFormula(typeof (Switch<Case<Where<POReceipt.receiptType, Equal<POReceiptType.transferreceipt>, Or<POReceipt.isIntercompany, Equal<True>>>, False>, Current<POSetup.autoCreateInvoiceOnReceipt>>))]
  [PXDefault]
  public virtual bool? AutoCreateInvoice
  {
    get => this._AutoCreateInvoice;
    set => this._AutoCreateInvoice = value;
  }

  /// <summary>
  /// The way in which the cost of items is determined in a return document.
  /// </summary>
  [PXDBString(1, IsFixed = true)]
  [PXDefault(typeof (Switch<Case<Where<POReceipt.receiptType, Equal<POReceiptType.poreturn>, And<Current<POSetup.returnOrigCost>, Equal<True>>>, ReturnCostMode.originalCost, Case<Where<POReceipt.receiptType, Equal<POReceiptType.poreturn>, And<Current<POSetup.returnOrigCost>, NotEqual<True>>>, ReturnCostMode.costByIssue>>, ReturnCostMode.notApplicable>))]
  [PXUIField]
  [PXUIEnabled(typeof (Where<POReceipt.receiptType, Equal<POReceiptType.poreturn>, And<POReceipt.released, Equal<False>>>))]
  [ReturnCostMode.List]
  public virtual string ReturnInventoryCostMode { get; set; }

  [PXDBBool]
  [PXDefault(typeof (Switch<Case<Where<POReceipt.receiptType, Equal<POReceiptType.poreturn>>, Current<POSetup.returnOrigCost>>, False>))]
  [PXUIField(DisplayName = "Process Return with Original Cost")]
  [PXUIEnabled(typeof (Where<POReceipt.receiptType, Equal<POReceiptType.poreturn>, And<POReceipt.released, Equal<False>>>))]
  [Obsolete("This item has been deprecated and will be removed in Acumatica ERP 2023 R1. Use ReturnInventoryCostMode field instead.")]
  public virtual bool? ReturnOrigCost { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID
  {
    get => this._CreatedByID;
    set => this._CreatedByID = value;
  }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID
  {
    get => this._CreatedByScreenID;
    set => this._CreatedByScreenID = value;
  }

  [PXDBCreatedDateTime]
  [PXUIField(DisplayName = "Created On", Enabled = false, IsReadOnly = true)]
  public virtual DateTime? CreatedDateTime
  {
    get => this._CreatedDateTime;
    set => this._CreatedDateTime = value;
  }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID
  {
    get => this._LastModifiedByID;
    set => this._LastModifiedByID = value;
  }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID
  {
    get => this._LastModifiedByScreenID;
    set => this._LastModifiedByScreenID = value;
  }

  [PXDBLastModifiedDateTime]
  [PXUIField(DisplayName = "Last Modified On", Enabled = false, IsReadOnly = true)]
  public virtual DateTime? LastModifiedDateTime
  {
    get => this._LastModifiedDateTime;
    set => this._LastModifiedDateTime = value;
  }

  [PXDBInt]
  [PXDefault(typeof (PX.Objects.CR.BAccount.workgroupID))]
  [PXCompanyTreeSelector]
  [PXUIField]
  public virtual int? WorkgroupID
  {
    get => this._WorkgroupID;
    set => this._WorkgroupID = value;
  }

  [PXDefault(typeof (PX.Objects.AP.Vendor.ownerID))]
  [Owner(typeof (POReceipt.workgroupID))]
  public virtual int? OwnerID
  {
    get => this._OwnerID;
    set => this._OwnerID = value;
  }

  [PXDBQuantity]
  [PXUIField(DisplayName = "Unbilled Quantity", Enabled = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? UnbilledQty
  {
    get => this._UnbilledQty;
    set => this._UnbilledQty = value;
  }

  [PXDBDecimal(6)]
  [PXUIField(DisplayName = "Weight", Visible = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? ReceiptWeight
  {
    get => this._ReceiptWeight;
    set => this._ReceiptWeight = value;
  }

  [PXDBDecimal(6)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Volume", Visible = false)]
  public virtual Decimal? ReceiptVolume
  {
    get => this._ReceiptVolume;
    set => this._ReceiptVolume = value;
  }

  [PXDBString(2, IsFixed = true)]
  [POOrderType.List]
  [PXUIField(DisplayName = "Order Type")]
  public virtual string POType
  {
    get => this._POType;
    set => this._POType = value;
  }

  [PXDBInt]
  [PXUIField(DisplayName = "Ship To")]
  public virtual int? ShipToBAccountID
  {
    get => this._ShipToBAccountID;
    set => this._ShipToBAccountID = value;
  }

  [LocationActive(typeof (Where<PX.Objects.CR.Location.bAccountID, Equal<Current<POReceipt.shipToBAccountID>>>), DescriptionField = typeof (PX.Objects.CR.Location.descr))]
  [PXUIField(DisplayName = "Shipping Location")]
  [PXForeignReference(typeof (Field<POReceipt.shipToLocationID>.IsRelatedTo<PX.Objects.CR.Location.locationID>))]
  public virtual int? ShipToLocationID
  {
    get => this._ShipToLocationID;
    set => this._ShipToLocationID = value;
  }

  [ProjectDefault]
  [PXRestrictor(typeof (Where<PMProject.isActive, Equal<True>>), "The {0} project or contract is inactive.", new System.Type[] {typeof (PMProject.contractCD)})]
  [PXRestrictor(typeof (Where<PMProject.visibleInPO, Equal<True>, Or<PMProject.nonProject, Equal<True>>>), "The '{0}' project is invisible in the module.", new System.Type[] {typeof (PMProject.contractCD)})]
  [ProjectBase]
  public virtual int? ProjectID { get; set; }

  public virtual string GetAPDocType() => !(this.ReceiptType == "RT") ? "ADR" : "INV";

  int? IAssign.WorkgroupID
  {
    get => this.WorkgroupID;
    set => this.WorkgroupID = value;
  }

  int? IAssign.OwnerID
  {
    get => this.OwnerID;
    set => this.OwnerID = value;
  }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBCurrency(typeof (POReceipt.curyInfoID), typeof (POReceipt.orderTotal))]
  [PXUIField]
  public virtual Decimal? CuryOrderTotal
  {
    get => this._CuryOrderTotal;
    set => this._CuryOrderTotal = value;
  }

  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? OrderTotal
  {
    get => this._OrderTotal;
    set => this._OrderTotal = value;
  }

  [PXDecimal(4)]
  [Obsolete("The field is preserved for the support of the legacy Default Endpoints only. Last endpoint that uses this field: 20.200.001. This field will be deleted once the 20.200.001 endpoint become obsolete.")]
  public virtual Decimal? CuryControlTotal
  {
    get => new Decimal?();
    set
    {
    }
  }

  [PXString(1, IsFixed = true)]
  [PXUIField(DisplayName = "IN Doc. Type", Enabled = false)]
  [INDocType.List]
  public virtual string InventoryDocType { get; set; }

  [PXString(15, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "IN Ref. Nbr.", Enabled = false)]
  [PXSelector(typeof (Search<PX.Objects.IN.INRegister.refNbr, Where<PX.Objects.IN.INRegister.docType, Equal<Current<POReceipt.inventoryDocType>>>>))]
  public virtual string InventoryRefNbr { [PXDependsOnFields(new System.Type[] {typeof (POReceipt.released), typeof (POReceipt.invtDocType), typeof (POReceipt.invtRefNbr)})] get; set; }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? WMSSingleOrder { get; set; }

  [PXDBString(15, IsUnicode = true)]
  public virtual string OrigPONbr { get; set; }

  [PXBool]
  [PXUIField(Visible = false)]
  [PXFormula(typeof (Switch<Case<Where<POReceipt.receiptType, NotEqual<POReceiptType.transferreceipt>>, True>, False>))]
  public virtual bool? ShowPurchaseOrdersTab { get; set; }

  [PXBool]
  [PXUIField(Visible = false)]
  [PXFormula(typeof (Switch<Case<Where<FeatureInstalled<FeaturesSet.inventory>>, True>, False>))]
  public virtual bool? ShowPutAwayHistoryTab { get; set; }

  [PXBool]
  [PXUIField(Visible = false)]
  [PXFormula(typeof (Switch<Case<Where<POReceipt.receiptType, NotEqual<POReceiptType.poreturn>, And<FeatureInstalled<FeaturesSet.inventory>>>, True>, False>))]
  public virtual bool? ShowLandedCostsTab { get; set; }

  [PXDBBool]
  [PXFormula(typeof (Where<Selector<POReceipt.vendorID, PX.Objects.CR.BAccount.isBranch>, Equal<True>, And<POReceipt.receiptType, In3<POReceiptType.poreceipt, POReceiptType.poreturn>>>))]
  [PXDefault]
  public virtual bool? IsIntercompany { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? IsIntercompanySOCreated { get; set; }

  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "Related Shipment Nbr.", Enabled = false, FieldClass = "InterBranch")]
  [PXSelector(typeof (Search<PX.Objects.SO.SOShipment.shipmentNbr, Where<PX.Objects.SO.SOShipment.shipmentType, Equal<INDocType.issue>>>))]
  public virtual string IntercompanyShipmentNbr { get; set; }

  [PXString(2, IsFixed = true)]
  [PXUIField(DisplayName = "Related Order Type", Enabled = false, FieldClass = "InterBranch")]
  [PXSelector(typeof (Search<PX.Objects.SO.SOOrderType.orderType>), DescriptionField = typeof (PX.Objects.SO.SOOrderType.descr))]
  public virtual string IntercompanySOType { get; set; }

  [PXString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "Related Order Nbr.", Enabled = false, FieldClass = "InterBranch")]
  [PXSelector(typeof (Search<PX.Objects.SO.SOOrder.orderNbr, Where<PX.Objects.SO.SOOrder.orderType, Equal<Current<POReceipt.intercompanySOType>>>>))]
  public virtual string IntercompanySONbr { get; set; }

  [PXBool]
  public virtual bool? IntercompanySOCancelled { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Exclude from Intercompany Processing", FieldClass = "InterBranch")]
  public virtual bool? ExcludeFromIntercompanyProc { get; set; }

  [PXDBString(2, IsFixed = true)]
  [PXSelector(typeof (Search<PX.Objects.SO.SOOrderType.orderType>), CacheGlobal = true)]
  public virtual string SOOrderType { get; set; }

  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "SO Return", Enabled = false)]
  [PXSelector(typeof (Search<PX.Objects.SO.SOOrder.orderNbr, Where<PX.Objects.SO.SOOrder.orderType, Equal<Current<POReceipt.sOOrderType>>>>))]
  public virtual string SOOrderNbr { get; set; }

  [PXBool]
  public virtual bool? DropshipFieldsSet { get; set; }

  [Customer]
  public virtual int? DropshipCustomerID { get; set; }

  [LocationActive(typeof (Where<PX.Objects.CR.Location.bAccountID, Equal<Current<POReceipt.dropshipCustomerID>>>))]
  public virtual int? DropshipCustomerLocationID { get; set; }

  [PXDBString(40, IsUnicode = true)]
  public virtual string DropshipCustomerOrderNbr { get; set; }

  [PXDBString(15, IsUnicode = true)]
  [PXSelector(typeof (Search<PX.Objects.CS.Carrier.carrierID>), CacheGlobal = true)]
  public virtual string DropshipShipVia { get; set; }

  /// <summary>
  /// The original PO receipt for which the current correction receipt was created
  /// </summary>
  [PXDBString]
  [PXUIField(DisplayName = "Original Doc. Ref. Nbr.", Enabled = false)]
  [PXSelector(typeof (Search<POReceipt.receiptNbr, Where<POReceipt.receiptType, Equal<Current<POReceipt.receiptType>>>>))]
  public virtual string OrigReceiptNbr { get; set; }

  /// <summary>
  /// The correction PO receipt that was created for the current correction receipt
  /// </summary>
  [PXString]
  [PXUIField(DisplayName = "Correction Doc. Ref. Nbr.", Enabled = false)]
  [PXSelector(typeof (Search<POReceipt.receiptNbr, Where<POReceipt.receiptType, Equal<Current<POReceipt.receiptType>>>>))]
  public virtual string CorrectionReceiptNbr { get; set; }

  /// <summary>Reversal inventory document type</summary>
  [PXString]
  [PXUIField(DisplayName = "Reversal IN Doc. Type", Enabled = false)]
  public virtual string ReversalInvtDocType { get; set; }

  /// <summary>Reversal inventory document number</summary>
  [PXString]
  [PXUIField(DisplayName = "Reversal IN Ref. Nbr.", Enabled = false)]
  [PXSelector(typeof (Search<PX.Objects.IN.INRegister.refNbr, Where<PX.Objects.IN.INRegister.docType, Equal<Current<POReceipt.reversalInvtDocType>>>>))]
  public virtual string ReversalInvtRefNbr { get; set; }

  /// <summary>
  /// When set to <c>true</c>, indicates that the receipt has an unreleased Correction receipt.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(Enabled = false)]
  public virtual bool? IsUnderCorrection { get; set; }

  /// <summary>
  /// When set to <c>true</c>, indicates that the receipt was canceled using the Cancel or Correct action.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(Enabled = false)]
  public virtual bool? Canceled { get; set; }

  public class PK : PrimaryKeyOf<POReceipt>.By<POReceipt.receiptType, POReceipt.receiptNbr>
  {
    public static POReceipt Find(
      PXGraph graph,
      string receiptType,
      string receiptNbr,
      PKFindOptions options = 0)
    {
      return (POReceipt) PrimaryKeyOf<POReceipt>.By<POReceipt.receiptType, POReceipt.receiptNbr>.FindBy(graph, (object) receiptType, (object) receiptNbr, options);
    }
  }

  public static class FK
  {
    public class Site : 
      PrimaryKeyOf<PX.Objects.IN.INSite>.By<PX.Objects.IN.INSite.siteID>.ForeignKeyOf<POReceipt>.By<POReceipt.siteID>
    {
    }

    public class Vendor : 
      PrimaryKeyOf<PX.Objects.AP.Vendor>.By<PX.Objects.AP.Vendor.bAccountID>.ForeignKeyOf<POReceipt>.By<POReceipt.vendorID>
    {
    }

    public class VendorLocation : 
      PrimaryKeyOf<PX.Objects.CR.Location>.By<PX.Objects.CR.Location.bAccountID, PX.Objects.CR.Location.locationID>.ForeignKeyOf<POReceipt>.By<POReceipt.vendorID, POReceipt.vendorLocationID>
    {
    }

    public class Branch : 
      PrimaryKeyOf<PX.Objects.GL.Branch>.By<PX.Objects.GL.Branch.branchID>.ForeignKeyOf<POReceipt>.By<POReceipt.branchID>
    {
    }

    public class Currency : 
      PrimaryKeyOf<PX.Objects.CM.Currency>.By<PX.Objects.CM.Currency.curyID>.ForeignKeyOf<POReceipt>.By<POReceipt.curyID>
    {
    }

    public class Workgroup : 
      PrimaryKeyOf<EPCompanyTree>.By<EPCompanyTree.workGroupID>.ForeignKeyOf<POReceipt>.By<POReceipt.workgroupID>
    {
    }

    public class Owner : 
      PrimaryKeyOf<PX.Objects.CR.Contact>.By<PX.Objects.CR.Contact.contactID>.ForeignKeyOf<POReceipt>.By<POReceipt.ownerID>
    {
    }

    public class ShipToAccount : 
      PrimaryKeyOf<PX.Objects.CR.BAccount>.By<PX.Objects.CR.BAccount.bAccountID>.ForeignKeyOf<POReceipt>.By<POReceipt.shipToBAccountID>
    {
    }

    public class ShipLocation : 
      PrimaryKeyOf<PX.Objects.CR.Location>.By<PX.Objects.CR.Location.bAccountID, PX.Objects.CR.Location.locationID>.ForeignKeyOf<POReceipt>.By<POReceipt.shipToBAccountID, POReceipt.shipToLocationID>
    {
    }

    public class Project : 
      PrimaryKeyOf<PMProject>.By<PMProject.contractID>.ForeignKeyOf<POReceipt>.By<POReceipt.projectID>
    {
    }

    public class IntercompanyShipment : 
      PrimaryKeyOf<PX.Objects.SO.SOShipment>.By<PX.Objects.SO.SOShipment.shipmentNbr>.ForeignKeyOf<POReceipt>.By<POReceipt.intercompanyShipmentNbr>
    {
    }
  }

  public class Events : PXEntityEventBase<POReceipt>.Container<POReceipt.Events>
  {
    public PXEntityEvent<POReceipt> InventoryReceiptCreated;
    public PXEntityEvent<POReceipt> InventoryIssueCreated;
    public PXEntityEvent<POReceipt> ReceiveConfirmed;
    public PXEntityEvent<POReceipt> CorrectionReceiptCreated;
    public PXEntityEvent<POReceipt> CorrectionReceiptDeleted;
    public PXEntityEvent<POReceipt> CorrectionReceiptReleased;
    public PXEntityEvent<POReceipt> ReceiptCanceled;
  }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POReceipt.selected>
  {
  }

  public abstract class receiptType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POReceipt.receiptType>
  {
    public const int Length = 2;
    public const string DisplayName = "Type";
  }

  public abstract class receiptNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POReceipt.receiptNbr>
  {
    public const int Length = 15;
    public const string DisplayName = "Receipt Nbr.";
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POReceipt.siteID>
  {
  }

  public abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POReceipt.vendorID>
  {
    public class PreventEditBAccountVOrgBAccountID<TGraph> : 
      PreventEditBAccountRestrictToBase<PX.Objects.CR.BAccount.vOrgBAccountID, TGraph, POReceipt, SelectFromBase<POReceipt, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
      #nullable enable
      POReceipt.receiptType, 
      #nullable disable
      NotEqual<POReceiptType.transferreceipt>>>>>.And<BqlOperand<
      #nullable enable
      POReceipt.vendorID, IBqlInt>.IsEqual<
      #nullable disable
      BqlField<
      #nullable enable
      PX.Objects.CR.BAccount.bAccountID, IBqlInt>.FromCurrent>>>>
      where TGraph : 
      #nullable disable
      PXGraph
    {
      protected override string GetErrorMessage(
        PX.Objects.CR.BAccount baccount,
        POReceipt document,
        string documentBaseCurrency)
      {
        return PXMessages.LocalizeFormatNoPrefix("A branch with the base currency other than {0} cannot be associated with the {1} vendor because {1} is selected in the {2} purchase receipt.", new object[3]
        {
          (object) documentBaseCurrency,
          (object) baccount.AcctCD,
          (object) document.ReceiptNbr
        });
      }
    }

    public class PreventEditBAccountVOrgBAccountIDOnVendorMaint : 
      POReceipt.vendorID.PreventEditBAccountVOrgBAccountID<VendorMaint>
    {
      public static bool IsActive()
      {
        return PXAccess.FeatureInstalled<FeaturesSet.multipleBaseCurrencies>();
      }
    }

    public class PreventEditBAccountVOrgBAccountIDOnCustomerMaint : 
      POReceipt.vendorID.PreventEditBAccountVOrgBAccountID<CustomerMaint>
    {
      public static bool IsActive()
      {
        return PXAccess.FeatureInstalled<FeaturesSet.multipleBaseCurrencies>();
      }
    }
  }

  public abstract class vendorLocationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POReceipt.vendorLocationID>
  {
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POReceipt.branchID>
  {
  }

  public abstract class receiptDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  POReceipt.receiptDate>
  {
    public const string DisplayName = "Date";
  }

  public abstract class invoiceDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  POReceipt.invoiceDate>
  {
  }

  public abstract class finPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POReceipt.finPeriodID>
  {
  }

  public abstract class tranPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POReceipt.tranPeriodID>
  {
  }

  public abstract class hold : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POReceipt.hold>
  {
  }

  public abstract class released : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POReceipt.released>
  {
  }

  public abstract class received : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POReceipt.received>
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POReceipt.status>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  POReceipt.noteID>
  {
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POReceipt.curyID>
  {
  }

  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  POReceipt.curyInfoID>
  {
  }

  public abstract class lineCntr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POReceipt.lineCntr>
  {
  }

  public abstract class orderQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POReceipt.orderQty>
  {
  }

  public abstract class controlQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POReceipt.controlQty>
  {
  }

  public abstract class aPDocType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POReceipt.aPDocType>
  {
  }

  public abstract class aPRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POReceipt.aPRefNbr>
  {
  }

  public abstract class invtDocType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POReceipt.invtDocType>
  {
  }

  public abstract class invtRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POReceipt.invtRefNbr>
  {
  }

  public abstract class invoiceNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POReceipt.invoiceNbr>
  {
  }

  public abstract class autoCreateInvoice : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    POReceipt.autoCreateInvoice>
  {
  }

  public abstract class returnInventoryCostMode : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POReceipt.returnInventoryCostMode>
  {
  }

  [Obsolete("This item has been deprecated and will be removed in Acumatica ERP 2023 R1. Use ReturnInventoryCostMode field instead.")]
  public abstract class returnOrigCost : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POReceipt.returnOrigCost>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  POReceipt.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  POReceipt.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POReceipt.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    POReceipt.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  POReceipt.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POReceipt.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    POReceipt.lastModifiedDateTime>
  {
  }

  public abstract class vendorID_Vendor_acctName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POReceipt.vendorID_Vendor_acctName>
  {
  }

  public abstract class workgroupID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POReceipt.workgroupID>
  {
  }

  public abstract class ownerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POReceipt.ownerID>
  {
  }

  public abstract class unbilledQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POReceipt.unbilledQty>
  {
  }

  public abstract class receiptWeight : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POReceipt.receiptWeight>
  {
  }

  public abstract class receiptVolume : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  POReceipt.receiptVolume>
  {
  }

  public abstract class pOType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POReceipt.pOType>
  {
  }

  public abstract class shipToBAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POReceipt.shipToBAccountID>
  {
  }

  public abstract class shipToLocationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POReceipt.shipToLocationID>
  {
  }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  POReceipt.projectID>
  {
  }

  public abstract class curyOrderTotal : IBqlField, IBqlOperand
  {
  }

  public abstract class orderTotal : IBqlField, IBqlOperand
  {
  }

  [Obsolete("The field is preserved for the support of the legacy Default Endpoints only. Last endpoint that uses this field: 20.200.001. This field will be deleted once the 20.200.001 endpoint become obsolete.")]
  public abstract class curyControlTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    POReceipt.curyControlTotal>
  {
  }

  public abstract class inventoryDocType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POReceipt.inventoryDocType>
  {
  }

  public abstract class inventoryRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POReceipt.inventoryRefNbr>
  {
  }

  public abstract class wMSSingleOrder : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POReceipt.wMSSingleOrder>
  {
  }

  public abstract class origPONbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POReceipt.origPONbr>
  {
  }

  public abstract class showPurchaseOrdersTab : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    POReceipt.showPurchaseOrdersTab>
  {
  }

  public abstract class showPutAwayHistoryTab : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    POReceipt.showPutAwayHistoryTab>
  {
  }

  public abstract class showLandedCostsTab : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    POReceipt.showLandedCostsTab>
  {
  }

  public abstract class isIntercompany : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POReceipt.isIntercompany>
  {
  }

  public abstract class isIntercompanySOCreated : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    POReceipt.isIntercompanySOCreated>
  {
  }

  public abstract class intercompanyShipmentNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POReceipt.intercompanyShipmentNbr>
  {
  }

  public abstract class intercompanySOType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POReceipt.intercompanySOType>
  {
  }

  public abstract class intercompanySONbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POReceipt.intercompanySONbr>
  {
  }

  public abstract class intercompanySOCancelled : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    POReceipt.intercompanySOCancelled>
  {
  }

  public abstract class excludeFromIntercompanyProc : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    POReceipt.excludeFromIntercompanyProc>
  {
  }

  public abstract class sOOrderType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POReceipt.sOOrderType>
  {
  }

  public abstract class sOOrderNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POReceipt.sOOrderNbr>
  {
  }

  public abstract class dropshipFieldsSet : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    POReceipt.dropshipFieldsSet>
  {
  }

  public abstract class dropshipCustomerID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    POReceipt.dropshipCustomerID>
  {
  }

  public abstract class dropshipCustomerLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    POReceipt.dropshipCustomerLocationID>
  {
  }

  public abstract class dropshipCustomerOrderNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POReceipt.dropshipCustomerOrderNbr>
  {
  }

  public abstract class dropshipShipVia : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POReceipt.dropshipShipVia>
  {
  }

  public abstract class origReceiptNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  POReceipt.origReceiptNbr>
  {
  }

  public abstract class correctionReceiptNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POReceipt.correctionReceiptNbr>
  {
  }

  public abstract class reversalInvtDocType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POReceipt.reversalInvtDocType>
  {
  }

  public abstract class reversalInvtRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POReceipt.reversalInvtRefNbr>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PO.POReceipt.IsUnderCorrection" />
  public abstract class isUnderCorrection : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    POReceipt.isUnderCorrection>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.PO.POReceipt.Canceled" />
  public abstract class canceled : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POReceipt.canceled>
  {
  }
}
