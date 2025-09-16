// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSAppointmentDet
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.CM;
using PX.Objects.CM.Extensions;
using PX.Objects.Common.Discount;
using PX.Objects.Common.Discount.Attributes;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.IN;
using PX.Objects.PM;
using PX.Objects.PO;
using PX.Objects.SO;
using System;
using System.Collections.Generic;

#nullable enable
namespace PX.Objects.FS;

[PXCacheName("Appointment Item Detail")]
[Serializable]
public class FSAppointmentDet : 
  PXBqlTable,
  IBqlTable,
  IBqlTableSystemDataStorage,
  IFSSODetBase,
  IDocLine,
  ISortOrder,
  ILSPrimary,
  ILSMaster,
  IItemPlanMaster
{
  protected 
  #nullable disable
  string _UOM;
  public string _Status;
  protected string _UIStatus;
  protected string _LotSerialNbr;
  protected Decimal? _BaseEstimatedQty;
  private int? _LogActualDuration;
  private int? _ActualDuration;
  protected Decimal? _BaseActualQty;
  protected Decimal? _EffTranQty;
  protected Decimal? _BaseEffTranQty;
  private Decimal? _CuryBillableExtPrice;
  private string _POSource;
  protected string _Operation;
  protected string _TranType;

  [PXBool]
  [PXUIField(DisplayName = "Selected")]
  public virtual bool? Selected { get; set; }

  [PXDBString(4, IsKey = true, IsFixed = true)]
  [PXUIField(DisplayName = "Service Order Type", Visible = false, Enabled = false)]
  [PXDefault(typeof (FSAppointment.srvOrdType))]
  [PXSelector(typeof (Search<FSSrvOrdType.srvOrdType>), CacheGlobal = true)]
  public virtual string SrvOrdType { get; set; }

  [PXDBString(20, IsKey = true, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Appointment Nbr.", Visible = false, Enabled = false)]
  [PXDBDefault(typeof (FSAppointment.refNbr), DefaultForUpdate = false)]
  [PXParent(typeof (Select<FSAppointment, Where<FSAppointment.srvOrdType, Equal<Current<FSAppointmentDet.srvOrdType>>, And<FSAppointment.refNbr, Equal<Current<FSAppointmentDet.refNbr>>>>>))]
  public virtual string RefNbr { get; set; }

  [PXDBInt]
  [PXDBDefault(typeof (FSAppointment.appointmentID))]
  [PXUIField(DisplayName = "Appointment Nbr.")]
  public virtual int? AppointmentID { get; set; }

  [PXDBIdentity]
  public virtual int? AppDetID { get; set; }

  [PXDBInt(IsKey = true)]
  [PXLineNbr(typeof (FSAppointment.lineCntr))]
  [PXCheckUnique(new System.Type[] {}, Where = typeof (Where<FSAppointmentDet.srvOrdType, Equal<Current<FSAppointment.srvOrdType>>, And<FSAppointmentDet.refNbr, Equal<Current<FSAppointment.refNbr>>>>), UniqueKeyIsPartOfPrimaryKey = true, ClearOnDuplicate = false)]
  [PXUIField(DisplayName = "Line Nbr.", Visible = false, Enabled = false)]
  [PXFormula(null, typeof (MaxCalc<FSAppointment.maxLineNbr>))]
  public virtual int? LineNbr { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Sort Order", Visible = false, Enabled = false)]
  public virtual int? SortOrder { get; set; }

  [PXDBInt]
  [PXCheckUnique(new System.Type[] {}, Where = typeof (Where<FSAppointmentDet.appointmentID, Equal<Current<FSAppointment.appointmentID>>>))]
  [PXUIField(DisplayName = "Service Order Detail Ref. Nbr.", Visible = false)]
  [FSSelectorSODetID(ValidateValue = false)]
  public virtual int? SODetID { get; set; }

  [PXDBString(4, IsFixed = true)]
  [PXUIField]
  public virtual string LineRef { get; set; }

  [Branch(typeof (FSAppointment.branchID), null, true, true, true)]
  public virtual int? BranchID { get; set; }

  [PXDBLong]
  [CurrencyInfo(typeof (FSAppointment.curyInfoID))]
  public virtual long? CuryInfoID { get; set; }

  [PXDBString(5, IsFixed = true)]
  [PXUIField(DisplayName = "Line Type")]
  [FSLineType.List]
  [PXDefault]
  public virtual string LineType { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Prepaid Item", Enabled = false, Visible = false)]
  public virtual bool? IsPrepaid { get; set; }

  [PXDBString(4, IsFixed = true)]
  [PXUIField(DisplayName = "Pickup/Delivery Ref. Nbr.", FieldClass = "ROUTEMANAGEMENT")]
  [FSSelectorServiceInAppointment]
  [PXFormula(typeof (Default<FSAppointmentDet.lineType>))]
  public virtual string PickupDeliveryAppLineRef { get; set; }

  [Service(null, Enabled = false)]
  [PXUIField(DisplayName = "Pickup/Delivery Service ID", Visible = false, FieldClass = "ROUTEMANAGEMENT")]
  [PXFormula(typeof (Selector<FSAppointmentDet.pickupDeliveryAppLineRef, FSAppointmentDet.inventoryID>))]
  public virtual int? PickupDeliveryServiceID { get; set; }

  [PXDefault]
  [PXFormula(typeof (Default<FSAppointmentDet.lineType>))]
  [InventoryIDByLineType(typeof (FSAppointmentDet.lineType), null, Filterable = true)]
  [PXRestrictor(typeof (Where<PX.Objects.IN.InventoryItem.itemType, NotEqual<INItemTypes.serviceItem>, Or<FSxServiceClass.requireRoute, Equal<True>, Or<Current<FSSrvOrdType.requireRoute>, Equal<False>>>>), "Non-route service cannot be handled with current route Service Order Type.", new System.Type[] {})]
  [PXRestrictor(typeof (Where<PX.Objects.IN.InventoryItem.itemType, NotEqual<INItemTypes.serviceItem>, Or<FSxServiceClass.requireRoute, Equal<False>, Or<Current<FSSrvOrdType.requireRoute>, Equal<True>>>>), "Route service cannot be handled with current non-route Service Order Type.", new System.Type[] {})]
  [PXRestrictor(typeof (Where<PX.Objects.IN.InventoryItem.stkItem, Equal<False>, Or<Current<FSSrvOrdType.postToSOSIPM>, Equal<True>, Or<Current<FSAppointmentDet.lineType>, Equal<ListField_LineType_Pickup_Delivery.Pickup_Delivery>>>>), "This stock item cannot be handled for current Service Order Type.", new System.Type[] {})]
  public virtual int? InventoryID { get; set; }

  [SubItem(typeof (FSAppointmentDet.inventoryID), DisplayName = "Subitem")]
  [PXDefault(typeof (Search<PX.Objects.IN.InventoryItem.defaultSubItemID, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Current<FSAppointmentDet.inventoryID>>, And<PX.Objects.IN.InventoryItem.defaultSubItemOnEntry, Equal<True>>>>))]
  [SubItemStatusVeryfier(typeof (FSAppointmentDet.inventoryID), typeof (FSAppointmentDet.siteID), new string[] {"IN", "NS"})]
  public virtual int? SubItemID { get; set; }

  [PXDefault(typeof (Search<PX.Objects.IN.InventoryItem.salesUnit, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Current<FSAppointmentDet.inventoryID>>>>))]
  [INUnit(typeof (FSAppointmentDet.inventoryID), DisplayName = "UOM")]
  public virtual string UOM
  {
    get => this._UOM;
    set => this._UOM = value;
  }

  [PXDBString(4, IsFixed = true)]
  [ListField_BillingRule.List]
  [PXDefault("FLRA")]
  [PXUIField(DisplayName = "Billing Rule", Enabled = false)]
  public virtual string BillingRule { get; set; }

  [PXUIVisible(typeof (FeatureInstalled<FeaturesSet.routeManagementModule>))]
  [PXDBString(1, IsFixed = true)]
  [ListField_Appointment_Service_Action_Type.List]
  [PXDefault("N")]
  [PXFormula(typeof (Selector<FSAppointmentDet.pickupDeliveryAppLineRef, FSAppointmentDet.serviceType>))]
  [PXUIField(DisplayName = "Pickup/Delivery Action", Enabled = false, Visible = false, FieldClass = "ROUTEMANAGEMENT")]
  public virtual string ServiceType { get; set; }

  [FSSiteAvail(typeof (FSAppointmentDet.inventoryID), typeof (FSAppointmentDet.subItemID), typeof (FSAppointmentDet.costCenterID), DisplayName = "Warehouse")]
  public virtual int? SiteID { get; set; }

  [PXFormula(typeof (Default<FSAppointmentDet.inventoryID, FSAppointmentDet.subItemID, FSAppointmentDet.siteID>))]
  [LocationAvail(typeof (FSAppointmentDet.inventoryID), typeof (FSAppointmentDet.subItemID), typeof (FSAppointmentDet.costCenterID), typeof (FSAppointmentDet.siteID), true, false, false)]
  public virtual int? SiteLocationID { get; set; }

  [PXDBBool]
  [PXUIField]
  [PXDefault(false, typeof (Search<FSxService.isTravelItem, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Current<FSAppointmentDet.inventoryID>>>>))]
  [PXFormula(typeof (Default<FSAppointmentDet.inventoryID>))]
  public virtual bool? IsTravelItem { get; set; }

  [PXString(2, IsFixed = true)]
  [PX.Objects.SO.SOLineType.List]
  [PXUIField(DisplayName = "SO Line Type", Visible = false, Enabled = false)]
  [PXFormula(typeof (Selector<FSAppointmentDet.inventoryID, Switch<Case<Where<PX.Objects.IN.InventoryItem.stkItem, Equal<True>, Or<PX.Objects.IN.InventoryItem.kitItem, Equal<True>>>, PX.Objects.SO.SOLineType.inventory, Case<Where<PX.Objects.IN.InventoryItem.nonStockShip, Equal<True>>, PX.Objects.SO.SOLineType.nonInventory>>, PX.Objects.SO.SOLineType.miscCharge>>))]
  public virtual string SOLineType { get; set; }

  [PXDBString(2, IsFixed = true)]
  [FSAppointmentDet.ListField_Status_AppointmentDet.List]
  [PXDefault(typeof (Switch<Case<Where<Current<FSAppointment.completed>, Equal<True>, And<FSAppointmentDet.isTravelItem, Equal<False>>>, FSAppointmentDet.ListField_Status_AppointmentDet.Completed, Case<Where<FSAppointmentDet.linkedEntityType, Equal<ListField_Linked_Entity_Type.apBill>, Or<FSAppointmentDet.linkedEntityType, Equal<ListField_Linked_Entity_Type.expenseReceipt>>>, FSAppointmentDet.ListField_Status_AppointmentDet.Completed>>, FSAppointmentDet.ListField_Status_AppointmentDet.NotStarted>))]
  [PXFormula(typeof (Default<FSAppointmentDet.isTravelItem, FSAppointmentDet.linkedEntityType>))]
  [PXUnboundFormula(typeof (Switch<Case<Where<FSAppointmentDet.status, Equal<FSAppointmentDet.ListField_Status_AppointmentDet.waitingForPO>>, int1>, int0>), typeof (SumCalc<FSAppointment.pendingPOLineCntr>))]
  [PXUnboundFormula(typeof (Switch<Case<Where<FSAppointmentDet.status, Equal<FSAppointmentDet.ListField_Status_AppointmentDet.waitingForPO>, And<FSAppointmentDet.pOSource, Equal<ListField_FSPOSource.purchaseToAppointment>>>, int1>, int0>), typeof (SumCalc<FSAppointment.pendingApptPOLineCntr>))]
  [PXUIField(DisplayName = "Line Status", Enabled = false)]
  public virtual string Status
  {
    get => this._Status;
    set
    {
      this._Status = value;
      this.UIStatus = value;
    }
  }

  [PXString(2, IsFixed = true)]
  [FSAppointmentDet.ListField_Status_AppointmentDet.List]
  [PXUIField(DisplayName = "Line Status")]
  public virtual string UIStatus
  {
    get => this._UIStatus;
    set => this._UIStatus = value;
  }

  [PXBool]
  [PXDBCalced(typeof (IIf<Where<FSAppointmentDet.status, Equal<FSAppointmentDet.ListField_Status_AppointmentDet.Canceled>, Or<FSAppointmentDet.status, Equal<FSAppointmentDet.ListField_Status_AppointmentDet.NotPerformed>, Or<FSAppointmentDet.status, Equal<FSAppointmentDet.ListField_Status_AppointmentDet.requestForPO>>>>, True, False>), typeof (bool))]
  [PXDefault(typeof (IIf<Where<FSAppointmentDet.status, Equal<FSAppointmentDet.ListField_Status_AppointmentDet.Canceled>, Or<FSAppointmentDet.status, Equal<FSAppointmentDet.ListField_Status_AppointmentDet.NotPerformed>, Or<FSAppointmentDet.status, Equal<FSAppointmentDet.ListField_Status_AppointmentDet.requestForPO>>>>, True, False>))]
  public virtual bool? IsCanceledNotPerformed { get; set; }

  [PXDBString(100, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Lot/Serial Nbr.", FieldClass = "LotSerial")]
  [PXDefault("")]
  [FSINLotSerialNbr(typeof (FSAppointmentDet.siteID), typeof (FSAppointmentDet.inventoryID), typeof (FSAppointmentDet.subItemID), typeof (FSAppointmentDet.siteLocationID), typeof (FSAppointmentDet.costCenterID), typeof (FSAppointmentDet.sODetID))]
  public virtual string LotSerialNbr
  {
    get => this._LotSerialNbr;
    set => this._LotSerialNbr = value;
  }

  [FSDBTimeSpanLong]
  [PXUIField(DisplayName = "Estimated Duration")]
  [PXUnboundFormula(typeof (Switch<Case<Where<FSAppointmentDet.lineType, Equal<FSLineType.Service>, And<FSAppointmentDet.status, NotEqual<FSAppointmentDet.ListField_Status_AppointmentDet.Canceled>, And<FSAppointmentDet.isTravelItem, Equal<False>>>>, FSAppointmentDet.estimatedDuration>, SharedClasses.int_0>), typeof (SumCalc<FSAppointment.estimatedDurationTotal>))]
  [PXDefault(0)]
  public virtual int? EstimatedDuration { get; set; }

  [FSDBQuantity(typeof (FSAppointmentDet.uOM), typeof (FSAppointmentDet.baseEstimatedQty))]
  [PXUIField(DisplayName = "Estimated Quantity")]
  [PXFormula(typeof (Default<FSAppointmentDet.sODetID>))]
  public virtual Decimal? EstimatedQty { get; set; }

  [PXDBDecimal(6)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Base Estimated Qty.", Visible = false, Enabled = false)]
  public virtual Decimal? BaseEstimatedQty
  {
    get => this._BaseEstimatedQty;
    set => this._BaseEstimatedQty = value;
  }

  [PXDBTimeSpanLong]
  [PXUIField(DisplayName = "Log Actual Duration", Enabled = false)]
  [PXDefault(0)]
  public virtual int? LogActualDuration
  {
    get => this._LogActualDuration;
    set => this._LogActualDuration = value;
  }

  [PXBool]
  [PXUnboundDefault(typeof (Switch<Case<Where<FSAppointmentDet.isCanceledNotPerformed, Equal<True>>, False, Case<Where<FSAppointmentDet.isTravelItem, Equal<True>, And<FSAppointmentDet.status, NotEqual<FSAppointmentDet.ListField_Status_AppointmentDet.NotStarted>>>, True, Case<Where<Current<FSAppointment.areActualFieldsActive>, Equal<True>>, True, Case<Where<Current<FSAppointment.closed>, Equal<True>>, True, Case<Where<Current<FSAppointment.closeActionRunning>, Equal<True>>, True>>>>>, False>))]
  [PXFormula(typeof (Default<FSAppointmentDet.isCanceledNotPerformed, FSAppointmentDet.isTravelItem, FSAppointmentDet.logActualDuration>))]
  public virtual bool? AreActualFieldsActive { get; set; }

  [FSDBTimeSpanLongAllowNegative]
  [PXUIField(DisplayName = "Actual Duration")]
  [PXFormula(typeof (Default<FSAppointmentDet.lineType, FSAppointmentDet.areActualFieldsActive, FSAppointmentDet.logActualDuration>))]
  [PXUnboundFormula(typeof (Switch<Case<Where<FSAppointmentDet.lineType, Equal<FSLineType.Service>, And<FSAppointmentDet.isTravelItem, Equal<False>>>, FSAppointmentDet.actualDuration>, int0>), typeof (SumCalc<FSAppointment.actualDurationTotal>))]
  [PXUIVerify]
  public virtual int? ActualDuration
  {
    get => this._ActualDuration;
    set => this._ActualDuration = value;
  }

  [FSDBQuantity(typeof (FSAppointmentDet.uOM), typeof (FSAppointmentDet.baseActualQty))]
  [PXDefault(typeof (Switch<Case<Where<FSAppointmentDet.lineType, NotEqual<ListField_LineType_ALL.Service>, And<FSAppointmentDet.lineType, NotEqual<ListField_LineType_ALL.NonStockItem>, And<FSAppointmentDet.lineType, NotEqual<ListField_LineType_ALL.Inventory_Item>>>>, decimal0, Case<Where<FSAppointmentDet.billingRule, Equal<ListField_BillingRule.Time>, Or<Current<FSAppointment.completeActionRunning>, Equal<True>>>, FSAppointmentDet.actualQty, Case<Where<FSAppointmentDet.areActualFieldsActive, Equal<True>, Or<FSAppointmentDet.linkedEntityType, Equal<ListField_Linked_Entity_Type.expenseReceipt>>>, FSAppointmentDet.estimatedQty>>>, decimal0>))]
  [PXFormula(typeof (Default<FSAppointmentDet.lineType, FSAppointmentDet.billingRule, FSAppointmentDet.areActualFieldsActive, FSAppointmentDet.linkedEntityType>))]
  [PXUIField(DisplayName = "Actual Quantity")]
  [PXUIVerify]
  public virtual Decimal? ActualQty { get; set; }

  [PXDBDecimal(6)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Base Actual Qty.", Visible = false, Enabled = false)]
  public virtual Decimal? BaseActualQty
  {
    get => this._BaseActualQty;
    set => this._BaseActualQty = value;
  }

  [FSDBQuantity(typeof (FSAppointmentDet.uOM), typeof (FSAppointmentDet.baseEffTranQty), InventoryUnitType.SalesUnit)]
  [PXUIField(DisplayName = "Transaction Qty.", Enabled = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXFormula(typeof (IIf<Where<FSAppointmentDet.areActualFieldsActive, Equal<True>>, FSAppointmentDet.actualQty, FSAppointmentDet.estimatedQty>))]
  public virtual Decimal? EffTranQty
  {
    get => this._EffTranQty;
    set => this._EffTranQty = value;
  }

  [PXUIField(DisplayName = "Qty.")]
  public virtual Decimal? Qty
  {
    [PXDependsOnFields(new System.Type[] {typeof (FSAppointmentDet.effTranQty)})] get
    {
      return this._EffTranQty;
    }
    set => this._EffTranQty = value;
  }

  [PXDBDecimal(6)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Base Transaction Qty.", Visible = false, Enabled = false)]
  public virtual Decimal? BaseEffTranQty
  {
    get => this._BaseEffTranQty;
    set => this._BaseEffTranQty = value;
  }

  public virtual Decimal? BaseQty
  {
    get => this._BaseEffTranQty;
    set => this._BaseEffTranQty = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Manual Price")]
  [PXUIVisible(typeof (Where<Current<FSSrvOrdType.postTo>, NotEqual<ListField_PostTo_CreateInvoice.PM>, Or<Current<FSSrvOrdType.postTo>, Equal<ListField_PostTo_CreateInvoice.PM>, And<Current<FSSrvOrdType.billingType>, NotEqual<ListField_SrvOrdType_BillingType.CostAsCost>>>>))]
  public virtual bool? ManualPrice { get; set; }

  [PXDBBool]
  [PXDefault(typeof (IIf<Where<FSAppointmentDet.lineType, Equal<FSLineType.Comment>, Or<FSAppointmentDet.lineType, Equal<FSLineType.Instruction>, Or<FSAppointmentDet.isCanceledNotPerformed, Equal<True>, Or<FSAppointmentDet.isPrepaid, Equal<True>>>>>, False, True>))]
  [PXUIField(DisplayName = "Billable")]
  [PXFormula(typeof (Default<FSAppointmentDet.isPrepaid, FSAppointmentDet.isCanceledNotPerformed>))]
  public virtual bool? IsBillable { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "Free Item")]
  public virtual bool? IsFree { get; set; }

  [FSDBQuantity(typeof (FSAppointmentDet.uOM), typeof (FSAppointmentDet.baseBillableQty))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXFormula(typeof (Switch<Case<Where<FSAppointmentDet.isPrepaid, Equal<True>, Or<FSAppointmentDet.isBillable, Equal<False>, Or<FSAppointmentDet.isCanceledNotPerformed, Equal<True>>>>, decimal0, Case<Where<FSAppointmentDet.contractRelated, Equal<True>>, FSAppointmentDet.extraUsageQty, Case<Where<FSAppointmentDet.areActualFieldsActive, Equal<True>>, FSAppointmentDet.actualQty>>>, FSAppointmentDet.estimatedQty>))]
  [PXUIField(DisplayName = "Billable Quantity", Enabled = false)]
  [PXUIVerify]
  public virtual Decimal? BillableQty { get; set; }

  [PXDBDecimal(6)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Base Billable Qty.", Visible = false, Enabled = false)]
  public virtual Decimal? BaseBillableQty { get; set; }

  [PXDBString(256 /*0x0100*/, IsUnicode = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Description")]
  public virtual string TranDesc { get; set; }

  [PXUIField(DisplayName = "NoteID")]
  [PXNote]
  public virtual Guid? NoteID { get; set; }

  [PXDBCreatedByID]
  [PXUIField(DisplayName = "CreatedByID")]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  [PXUIField(DisplayName = "CreatedByScreenID")]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  [PXUIField(DisplayName = "CreatedDateTime")]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  [PXUIField(DisplayName = "LastModifiedByID")]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  [PXUIField(DisplayName = "LastModifiedByScreenID")]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  [PXUIField(DisplayName = "LastModifiedDateTime")]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  [PXInt]
  [PXUIField(DisplayName = "Expense Staff Member ID")]
  [FSSelector_StaffMember_All(null)]
  public virtual int? ExpenseEmployeeID { get; set; }

  [PXDBDate]
  [PXDBDefault(typeof (FSAppointment.effDocDate))]
  [PXUIField(DisplayName = "Transaction Date")]
  public virtual DateTime? TranDate { get; set; }

  [PXDBCurrency(typeof (Search<CommonSetup.decPlPrcCst>), typeof (FSAppointmentDet.curyInfoID), typeof (FSAppointmentDet.unitCost))]
  [PXUIField]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryUnitCost { get; set; }

  [PXDBPriceCost]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? UnitCost { get; set; }

  [PXDBCurrency(typeof (FSAppointmentDet.curyInfoID), typeof (FSAppointmentDet.estimatedExtCost))]
  [PXUIField(Visible = false, Enabled = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXFormula(typeof (Switch<Case<Where<FSAppointmentDet.linkedEntityType, Equal<ListField_Linked_Entity_Type.expenseReceipt>>, FSAppointmentDet.curyExtCost, Case<Where<FSAppointmentDet.isCanceledNotPerformed, NotEqual<True>, And<Where<FSAppointmentDet.lineType, Equal<FSLineType.NonStockItem>, Or<FSAppointmentDet.lineType, Equal<FSLineType.Inventory_Item>, Or<Where<FSAppointmentDet.lineType, Equal<FSLineType.Service>, And<FSAppointmentDet.curyUnitCost, Greater<SharedClasses.decimal_0>>>>>>>>, Mult<FSAppointmentDet.curyUnitCost, FSAppointmentDet.estimatedQty>>>, SharedClasses.decimal_0>), typeof (SumCalc<FSAppointment.curyEstimatedCostTotal>))]
  public virtual Decimal? CuryEstimatedExtCost { get; set; }

  [PXDBPriceCost]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? EstimatedExtCost { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Manual Cost", Visible = false)]
  public virtual bool? ManualCost { get; set; }

  [PXDBCurrency(typeof (Search<CommonSetup.decPlPrcCst>), typeof (FSAppointmentDet.curyInfoID), typeof (FSAppointmentDet.unitPrice))]
  [PXUIField(DisplayName = "Unit Price")]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryUnitPrice { get; set; }

  [PXDBPriceCost]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Base Unit Price", Enabled = false)]
  public virtual Decimal? UnitPrice { get; set; }

  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Base Estimated Amount", Enabled = false, Visible = false)]
  [PXUnboundFormula(typeof (Switch<Case<Where<FSAppointmentDet.isFree, Equal<False>>, FSAppointmentDet.estimatedTranAmt>, SharedClasses.decimal_0>), typeof (SumCalc<FSAppointment.estimatedLineTotal>))]
  public virtual Decimal? EstimatedTranAmt { get; set; }

  [PXDBCurrency(typeof (FSAppointmentDet.curyInfoID), typeof (FSAppointmentDet.estimatedTranAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXFormula(typeof (Switch<Case<Where<FSAppointmentDet.isCanceledNotPerformed, Equal<True>>, decimal0, Case<Where2<Where<FSAppointmentDet.lineType, Equal<FSLineType.Service>, Or<FSAppointmentDet.lineType, Equal<FSLineType.Inventory_Item>>>, And<FSAppointmentDet.billingRule, Equal<ListField_BillingRule.None>>>, decimal0>>, Mult<FSAppointmentDet.curyUnitPrice, FSAppointmentDet.estimatedQty>>), typeof (SumCalc<FSAppointment.curyEstimatedLineTotal>))]
  [PXUIField(DisplayName = "Estimated Amount", Enabled = false)]
  public virtual Decimal? CuryEstimatedTranAmt { get; set; }

  [PXDBCurrency(typeof (FSAppointmentDet.curyInfoID), typeof (FSAppointmentDet.extCost))]
  [PXUIField(DisplayName = "Ext. Cost", Enabled = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXFormula(typeof (Switch<Case<Where<FSAppointmentDet.linkedEntityType, Equal<ListField_Linked_Entity_Type.expenseReceipt>, Or<FSAppointmentDet.linkedEntityType, Equal<ListField_Linked_Entity_Type.apBill>>>, IsNull<FSAppointmentDet.curyExtCost, decimal0>, Case<Where<FSAppointmentDet.isCanceledNotPerformed, NotEqual<True>, And<Where<FSAppointmentDet.lineType, Equal<FSLineType.NonStockItem>, Or<FSAppointmentDet.lineType, Equal<FSLineType.Inventory_Item>, Or<Where<FSAppointmentDet.lineType, Equal<FSLineType.Service>, And<FSAppointmentDet.curyUnitCost, Greater<SharedClasses.decimal_0>>>>>>>>, Mult<FSAppointmentDet.curyUnitCost, FSAppointmentDet.actualQty>>>, SharedClasses.decimal_0>), typeof (SumCalc<FSAppointment.curyCostTotal>))]
  public virtual Decimal? CuryExtCost { get; set; }

  [PXDBPriceCost]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? ExtCost { get; set; }

  [PXDBCurrency(typeof (FSAppointmentDet.curyInfoID), typeof (FSAppointmentDet.billableExtPrice))]
  [PXUIField(DisplayName = "Ext. Price")]
  [PXUIEnabled(typeof (Where<FSAppointmentDet.isCanceledNotPerformed, Equal<False>>))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXFormula(typeof (Switch<Case<Where<FSAppointmentDet.isFree, Equal<True>>, IsNull<FSAppointmentDet.curyBillableExtPrice, decimal0>, Case<Where<FSAppointmentDet.contractRelated, Equal<False>, And<FSAppointmentDet.billingRule, Equal<ListField_BillingRule.None>, And<Where<FSAppointmentDet.lineType, Equal<FSLineType.Service>, Or<FSAppointmentDet.lineType, Equal<FSLineType.Inventory_Item>, Or<FSAppointmentDet.lineType, Equal<ListField_LineType_Pickup_Delivery.Pickup_Delivery>>>>>>>, decimal0, Case<Where<FSAppointmentDet.contractRelated, Equal<True>>, Mult<FSAppointmentDet.curyExtraUsageUnitPrice, FSAppointmentDet.billableQty>>>>, Mult<FSAppointmentDet.curyUnitPrice, FSAppointmentDet.billableQty>>))]
  public virtual Decimal? CuryBillableExtPrice
  {
    get => this._CuryBillableExtPrice;
    set => this._CuryBillableExtPrice = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? BillableExtPrice { get; set; }

  [PXDBCurrency(typeof (FSAppointmentDet.curyInfoID), typeof (FSAppointmentDet.billableTranAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXFormula(typeof (Switch<Case<Where<FSAppointmentDet.isPrepaid, Equal<True>, Or<FSAppointmentDet.contractRelated, Equal<True>>>, FSAppointmentDet.curyBillableExtPrice>, Sub<FSAppointmentDet.curyBillableExtPrice, FSAppointmentDet.curyDiscAmt>>), typeof (SumCalc<FSAppointment.curyBillableLineTotal>))]
  [PXUIField(DisplayName = "Billable Amount", Enabled = false)]
  public virtual Decimal? CuryBillableTranAmt { get; set; }

  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Base Billable Amount", Enabled = false)]
  public virtual Decimal? BillableTranAmt { get; set; }

  [PXDecimal]
  [PXUIField(DisplayName = "Ext. Price")]
  public virtual Decimal? CuryExtPrice => this.CuryBillableExtPrice;

  [PXDecimal]
  [PXUIField(DisplayName = "Amount", Enabled = false)]
  public virtual Decimal? CuryLineAmt => this.CuryBillableTranAmt;

  [PXDBCurrency(typeof (FSAppointmentDet.curyInfoID), typeof (FSAppointmentDet.tranAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXFormula(typeof (Switch<Case<Where2<Where<FSAppointmentDet.lineType, Equal<FSLineType.Service>, Or<FSAppointmentDet.lineType, Equal<FSLineType.Inventory_Item>>>, And<FSAppointmentDet.billingRule, Equal<ListField_BillingRule.None>>>, SharedClasses.decimal_0>, Mult<FSAppointmentDet.curyUnitPrice, FSAppointmentDet.actualQty>>), typeof (SumCalc<FSAppointment.curyLineTotal>))]
  [PXUIField(DisplayName = "Actual Amount", Enabled = false)]
  public virtual Decimal? CuryTranAmt { get; set; }

  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Base Actual Amount", Enabled = false)]
  [PXUnboundFormula(typeof (Switch<Case<Where<FSAppointmentDet.isFree, Equal<False>, And<FSAppointmentDet.isCanceledNotPerformed, NotEqual<True>>>, FSAppointmentDet.tranAmt>, SharedClasses.decimal_0>), typeof (SumCalc<FSAppointment.lineTotal>))]
  public virtual Decimal? TranAmt { get; set; }

  [ManualDiscountMode(typeof (FSAppointmentDet.curyDiscAmt), typeof (FSAppointmentDet.discPct), DiscountFeatureType.CustomerDiscount)]
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField]
  public virtual bool? ManualDisc { get; set; }

  [PXUIEnabled(typeof (Where<FSAppointmentDet.status, NotEqual<FSAppointmentDet.ListField_Status_AppointmentDet.NotPerformed>>))]
  [PXDBDecimal(6, MinValue = 0.0, MaxValue = 100.0)]
  [PXUIField(DisplayName = "Discount Percent")]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXFormula(typeof (Default<FSAppointmentDet.inventoryID>))]
  public virtual Decimal? DiscPct { get; set; }

  [PXUIEnabled(typeof (Where<FSAppointmentDet.status, NotEqual<FSAppointmentDet.ListField_Status_AppointmentDet.NotPerformed>>))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBCurrencyPriceCost(typeof (FSAppointmentDet.curyInfoID), typeof (FSAppointmentDet.discAmt))]
  [PXFormula(typeof (Div<Mult<FSAppointmentDet.curyBillableExtPrice, FSAppointmentDet.discPct>, decimal100>))]
  [PXUIField(DisplayName = "Discount Amount")]
  public virtual Decimal? CuryDiscAmt { get; set; }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? DiscAmt { get; set; }

  [PXDBPackedIntegerArray]
  public virtual ushort[] DiscountsAppliedToLine { get; set; }

  [PXDBString(10, IsUnicode = true)]
  [PXSelector(typeof (Search<ARDiscount.discountID, Where<ARDiscount.type, Equal<DiscountType.LineDiscount>>>))]
  [PXUIField(DisplayName = "Discount Code", Visible = true, Enabled = true)]
  public virtual string DiscountID { get; set; }

  [PXDBString(10, IsUnicode = true)]
  [PXUIField(DisplayName = "Discount Sequence", Visible = false, Enabled = false)]
  public virtual string DiscountSequenceID { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Post ID")]
  public virtual int? PostID { get; set; }

  [PXDBInt]
  [PXDefault(typeof (FSAppointment.projectID))]
  [PXUIField(Visible = false)]
  [PXForeignReference(typeof (FSAppointmentDet.FK.Project))]
  public virtual int? ProjectID { get; set; }

  [PXDBInt]
  [PXDefault(typeof (Switch<Case<Where<FSAppointmentDet.lineType, Equal<FSLineType.Comment>, Or<FSAppointmentDet.lineType, Equal<FSLineType.Instruction>>>, Null>, Current<FSAppointment.dfltProjectTaskID>>))]
  [PXUIField(DisplayName = "Project Task", FieldClass = "PROJECT")]
  [FSSelectorActive_AR_SO_ProjectTask(typeof (Where<PMTask.projectID, Equal<Current<FSAppointmentDet.projectID>>>))]
  [PXForeignReference(typeof (FSAppointmentDet.FK.Task))]
  public virtual int? ProjectTaskID { get; set; }

  [PXDBInt]
  [PXUIField(Enabled = false, Visible = false)]
  public virtual int? ScheduleDetID { get; set; }

  /// <exclude />
  [PXDBInt]
  [PXDefault(typeof (PX.Objects.IN.CostCenter.freeStock))]
  public virtual int? CostCenterID { get; set; }

  [PXDBString(2, IsFixed = true)]
  [PXUIVisible(typeof (FeatureInstalled<FeaturesSet.inventory>))]
  [ListField_EquipmentAction.ListAtrribute]
  [PXDefault("NO")]
  [PXUIField(DisplayName = "Equipment Action", FieldClass = "EQUIPMENTMANAGEMENT")]
  public virtual string EquipmentAction { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Target Equipment ID", FieldClass = "EQUIPMENTMANAGEMENT")]
  [PXUIEnabled(typeof (Where<Current<FSAppointmentDet.isTravelItem>, NotEqual<True>>))]
  [PXDefault(typeof (FSAppointment.mem_SMequipmentID))]
  [FSSelectorMaintenanceEquipment(typeof (FSServiceOrder.srvOrdType), typeof (FSServiceOrder.billCustomerID), typeof (FSServiceOrder.customerID), typeof (FSServiceOrder.locationID), typeof (FSServiceOrder.branchID), typeof (FSServiceOrder.branchLocationID))]
  [PXRestrictor(typeof (Where<FSEquipment.status, Equal<ID.Equipment_Status.Equipment_StatusActive>>), "Equipment is {0}.", new System.Type[] {typeof (FSEquipment.status)})]
  [PXForeignReference(typeof (Field<FSAppointmentDet.SMequipmentID>.IsRelatedTo<FSEquipment.SMequipmentID>))]
  public virtual int? SMEquipmentID { get; set; }

  [PXDBString(4, IsFixed = true)]
  [PXUIVisible(typeof (FeatureInstalled<FeaturesSet.inventory>))]
  [PXUIField(DisplayName = "Model Equipment Ref. Nbr.", FieldClass = "EQUIPMENTMANAGEMENT")]
  [PXUIEnabled(typeof (Where<Current<FSAppointmentDet.isTravelItem>, NotEqual<True>>))]
  [PXDefault]
  [FSSelectorNewTargetEquipmentAppointment]
  public virtual string NewTargetEquipmentLineNbr { get; set; }

  [PXDBInt]
  [PXDefault]
  [PXUIField(DisplayName = "Component ID", FieldClass = "EQUIPMENTMANAGEMENT")]
  [PXUIEnabled(typeof (Where<Current<FSAppointmentDet.isTravelItem>, NotEqual<True>>))]
  [FSSelectorComponentIDAppointment(typeof (FSAppointmentDet), typeof (FSAppointmentDet))]
  public virtual int? ComponentID { get; set; }

  [PXDBInt]
  [PXDefault]
  [PXUIField(DisplayName = "Component Ref. Nbr.", FieldClass = "EQUIPMENTMANAGEMENT")]
  [PXUIEnabled(typeof (Where<Current<FSAppointmentDet.isTravelItem>, NotEqual<True>>))]
  [FSSelectorEquipmentLineRefServiceOrderAppointment(typeof (FSAppointmentDet.inventoryID), typeof (FSAppointmentDet.SMequipmentID), typeof (FSAppointmentDet.componentID), typeof (FSAppointmentDet.equipmentAction))]
  public virtual int? EquipmentLineRef { get; set; }

  [PXDBInt]
  [FSSelector_StaffMember_ServiceOrderProjectID]
  [PXUIField(DisplayName = "Staff Member ID")]
  public virtual int? StaffID { get; set; }

  [PXDBString(5, IsFixed = true)]
  [PXDefault("BASEP")]
  [PXUIField(DisplayName = "Price Type", Enabled = false)]
  [ListField_PriceType.ListAtrribute]
  public virtual string PriceType { get; set; }

  [PXDBString(30, InputMask = ">aaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")]
  [PXDefault]
  [PXUIField(DisplayName = "Price Code", Enabled = false)]
  public virtual string PriceCode { get; set; }

  [PXFormula(typeof (Default<FSAppointmentDet.inventoryID>))]
  [PXDefault]
  [Account]
  public virtual int? AcctID { get; set; }

  [PXFormula(typeof (Default<FSAppointmentDet.acctID>))]
  [PXDefault]
  [SubAccount(typeof (FSAppointmentDet.acctID))]
  public virtual int? SubID { get; set; }

  [PXDBInt]
  public virtual int? ScheduleID { get; set; }

  [SMCostCode(typeof (FSAppointmentDet.skipCostCodeValidation), typeof (FSAppointmentDet.acctID), typeof (FSAppointmentDet.projectTaskID))]
  [PXFormula(typeof (Default<FSAppointmentDet.inventoryID, FSAppointmentDet.isPrepaid>))]
  [PXForeignReference(typeof (FSAppointmentDet.FK.CostCode))]
  public virtual int? CostCodeID { get; set; }

  [PXBool]
  [PXFormula(typeof (IIf<Where<FSAppointmentDet.inventoryID, IsNotNull>, False, True>))]
  public virtual bool? SkipCostCodeValidation { get; set; }

  public virtual FSSODet FSSODetRow { get; set; }

  [PXInt]
  [PXSelector(typeof (Search<PX.Objects.IN.InventoryItem.inventoryID, Where<PX.Objects.IN.InventoryItem.itemStatus, NotEqual<InventoryItemStatus.inactive>, And<PX.Objects.IN.InventoryItem.itemStatus, NotEqual<InventoryItemStatus.markedForDeletion>, And<PX.Objects.IN.InventoryItem.itemStatus, NotEqual<InventoryItemStatus.noSales>, And<PX.Objects.IN.InventoryItem.itemType, Equal<INItemTypes.serviceItem>, And<Match<Current<AccessInfo.userName>>>>>>>>), SubstituteKey = typeof (PX.Objects.IN.InventoryItem.inventoryCD), DescriptionField = typeof (PX.Objects.IN.InventoryItem.descr))]
  [PXUIField(DisplayName = "Inventory ID", Enabled = false)]
  public virtual int? InventoryIDReport { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Warranty", Enabled = false, FieldClass = "EQUIPMENTMANAGEMENT")]
  public virtual bool? Warranty { get; set; }

  [PXInt]
  [PXUIField(DisplayName = "SO NewTargetEquipmentLineNbr", FieldClass = "EQUIPMENTMANAGEMENT")]
  public virtual int? SONewTargetEquipmentLineNbr { get; set; }

  [PXDBString(255 /*0xFF*/, IsUnicode = false)]
  [PXUIVisible(typeof (FeatureInstalled<FeaturesSet.inventory>))]
  [PXUIField(DisplayName = "Equipment Action Comment", FieldClass = "EQUIPMENTMANAGEMENT", Visible = false)]
  public virtual string Comment { get; set; }

  [PXDBString(2, IsFixed = true)]
  public virtual string EquipmentItemClass { get; set; }

  [PXDBString(15, IsUnicode = true)]
  [PXUIField]
  [PXSelector(typeof (PX.Objects.TX.TaxCategory.taxCategoryID), DescriptionField = typeof (PX.Objects.TX.TaxCategory.descr))]
  [PXDefault(typeof (Search<PX.Objects.IN.InventoryItem.taxCategoryID, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Current<FSAppointmentDet.inventoryID>>>>))]
  [PXFormula(typeof (Default<FSAppointmentDet.inventoryID>))]
  public virtual string TaxCategoryID { get; set; }

  [PXDBDecimal(6)]
  [PXDefault(TypeCode.Decimal, "1.0")]
  public virtual Decimal? GroupDiscountRate { get; set; }

  [PXDBDecimal(6)]
  [PXDefault(TypeCode.Decimal, "1.0")]
  public virtual Decimal? DocumentDiscountRate { get; set; }

  [PXDBBool]
  [PXFormula(typeof (Default<FSAppointmentDet.billingRule, FSAppointmentDet.SMequipmentID, FSAppointmentDet.actualQty, FSAppointmentDet.inventoryID>))]
  [PXFormula(typeof (Default<FSAppointmentDet.estimatedQty>))]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Service Contract Item", IsReadOnly = true, Visible = false, FieldClass = "FSCONTRACT")]
  public virtual bool? ContractRelated { get; set; }

  [PXDBQuantity]
  [PXFormula(typeof (Default<FSAppointmentDet.contractRelated, FSAppointmentDet.estimatedQty, FSAppointmentDet.actualQty>))]
  [PXUIField(DisplayName = "Covered Quantity", IsReadOnly = true, Visible = false, FieldClass = "FSCONTRACT")]
  public virtual Decimal? CoveredQty { get; set; }

  [PXDBQuantity]
  [PXFormula(typeof (Switch<Case<Where2<Where<Current<FSAppointment.notStarted>, Equal<True>>, And<FSAppointmentDet.contractRelated, Equal<True>>>, Sub<FSAppointmentDet.estimatedQty, FSAppointmentDet.coveredQty>, Case<Where<FSAppointmentDet.contractRelated, Equal<True>>, Sub<FSAppointmentDet.actualQty, FSAppointmentDet.coveredQty>>>, SharedClasses.decimal_0>))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Overage Quantity", IsReadOnly = true, Visible = false, FieldClass = "FSCONTRACT")]
  public virtual Decimal? ExtraUsageQty { get; set; }

  [PXDBPriceCost]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Base Overage Unit Price", Enabled = false, FieldClass = "FSCONTRACT")]
  public virtual Decimal? ExtraUsageUnitPrice { get; set; }

  [PXDBCurrency(typeof (Search<CommonSetup.decPlPrcCst>), typeof (FSAppointmentDet.curyInfoID), typeof (FSAppointmentDet.extraUsageUnitPrice))]
  [PXFormula(typeof (Default<FSAppointmentDet.contractRelated>))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Overage Unit Price", Enabled = false, Visible = false, FieldClass = "FSCONTRACT")]
  public virtual Decimal? CuryExtraUsageUnitPrice { get; set; }

  [PXDBBool]
  [PXDefault(false, typeof (Search<INItemSiteSettings.pOCreate, Where<INItemSiteSettings.inventoryID, Equal<Current<FSAppointmentDet.inventoryID>>, And<INItemSiteSettings.siteID, Equal<Current<FSAppointmentDet.siteID>>>>>))]
  [PXUIField(DisplayName = "Mark for PO", Visible = false, FieldClass = "DISTINV")]
  [PXUIEnabled(typeof (Where<FSAppointmentDet.lineType, NotEqual<ListField_LineType_ALL.Comment>, And<FSAppointmentDet.lineType, NotEqual<ListField_LineType_ALL.Instruction>>>))]
  public virtual bool? EnablePO { get; set; }

  [PXBool]
  [PXDefault(true)]
  public virtual bool? CanChangeMarkForPO { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "PO Completed", Visible = false, Enabled = false, FieldClass = "DISTINV")]
  public virtual bool? POCompleted { get; set; }

  [PXDBString(2)]
  [PXUIField(DisplayName = "Order Type", Enabled = false, FieldClass = "DISTINV")]
  public virtual string POType { get; set; }

  [PXDBString]
  [PXDefault(typeof (IIf<Where<FSAppointmentDet.enablePO, Equal<True>>, ListField_FSPOSource.purchaseToAppointment, Null>))]
  [PXFormula(typeof (Default<FSAppointmentDet.enablePO>))]
  [ListField_FSPOSource.List]
  [PXUIEnabled(typeof (Where<FSAppointmentDet.enablePO, Equal<True>>))]
  [PXUIField(DisplayName = "PO Source", FieldClass = "DISTINV")]
  public virtual string POSource
  {
    get => this._POSource;
    set => this._POSource = value;
  }

  [VendorNonEmployeeActive(DisplayName = "Vendor ID", DescriptionField = typeof (PX.Objects.AP.Vendor.acctName), CacheGlobal = true, Filterable = true, FieldClass = "DISTINV")]
  [PXDefault(typeof (Search<INItemSiteSettings.preferredVendorID, Where<INItemSiteSettings.inventoryID, Equal<Current<FSAppointmentDet.inventoryID>>, And<INItemSiteSettings.siteID, Equal<Current<FSAppointmentDet.siteID>>>>>))]
  [PXFormula(typeof (Default<FSAppointmentDet.enablePO>))]
  [PXUIEnabled(typeof (Where<FSAppointmentDet.enablePO, Equal<True>, And<FSAppointmentDet.canChangeMarkForPO, Equal<True>>>))]
  public virtual int? POVendorID { get; set; }

  [PXFormula(typeof (Default<FSAppointmentDet.poVendorID>))]
  [PXDefault(typeof (Coalesce<Search<INItemSiteSettings.preferredVendorLocationID, Where<INItemSiteSettings.inventoryID, Equal<Current<FSAppointmentDet.inventoryID>>, And<INItemSiteSettings.preferredVendorID, Equal<Current<FSAppointmentDet.poVendorID>>>>>, Search2<PX.Objects.AP.Vendor.defLocationID, InnerJoin<PX.Objects.CR.Standalone.Location, On<PX.Objects.CR.Standalone.Location.locationID, Equal<PX.Objects.AP.Vendor.defLocationID>, And<PX.Objects.CR.Standalone.Location.bAccountID, Equal<PX.Objects.AP.Vendor.bAccountID>>>>, Where<PX.Objects.AP.Vendor.bAccountID, Equal<Current<FSAppointmentDet.poVendorID>>, And<PX.Objects.CR.Standalone.Location.isActive, Equal<True>, And<MatchWithBranch<PX.Objects.CR.Standalone.Location.vBranchID>>>>>>))]
  [LocationActive(typeof (Where<PX.Objects.CR.Location.bAccountID, Equal<Current<FSAppointmentDet.poVendorID>>, And<MatchWithBranch<PX.Objects.CR.Location.vBranchID>>>))]
  [PXUIEnabled(typeof (Where<FSAppointmentDet.enablePO, Equal<True>, And<FSAppointmentDet.canChangeMarkForPO, Equal<True>>>))]
  public virtual int? POVendorLocationID { get; set; }

  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "PO Nbr.", Enabled = false, FieldClass = "DISTINV")]
  [PX.Objects.PO.PO.RefNbr(typeof (Search2<PX.Objects.PO.POOrder.orderNbr, LeftJoinSingleTable<PX.Objects.AP.Vendor, On<PX.Objects.PO.POOrder.vendorID, Equal<PX.Objects.AP.Vendor.bAccountID>, And<Match<PX.Objects.AP.Vendor, Current<AccessInfo.userName>>>>>, Where<PX.Objects.PO.POOrder.orderType, Equal<POOrderType.regularOrder>, And<PX.Objects.AP.Vendor.bAccountID, IsNotNull>>, OrderBy<Desc<PX.Objects.PO.POOrder.orderNbr>>>), Filterable = true)]
  public virtual string PONbr { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "PO Line Nbr.", FieldClass = "DISTINV")]
  public virtual int? POLineNbr { get; set; }

  [PXDBString]
  [POOrderStatus.List]
  [PXUIField(DisplayName = "PO Status", Enabled = false, FieldClass = "DISTINV")]
  public virtual string POStatus { get; set; }

  [FSDBQuantity(typeof (FSAppointmentDet.uOM), typeof (FSAppointmentDet.baseAllocatedFromSrvOrdPOQty), InventoryUnitType.BaseUnit)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Allocated from Service Order PO Qty.", Enabled = false)]
  public virtual Decimal? AllocatedFromSrvOrdPOQty { get; set; }

  [PXDBDecimal(6)]
  public virtual Decimal? BaseAllocatedFromSrvOrdPOQty { get; set; }

  [PXInt]
  public virtual int? TabOrigin => this.LineType == "SLPRO" ? new int?(1) : new int?(0);

  [PXString(1, IsFixed = true)]
  [PXDBScalar(typeof (Search2<INLotSerClass.lotSerTrack, InnerJoin<PX.Objects.IN.InventoryItem, On<PX.Objects.IN.InventoryItem.lotSerClassID, Equal<INLotSerClass.lotSerClassID>>>, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<FSAppointmentDet.inventoryID>>>))]
  [PXDefault(typeof (Search2<INLotSerClass.lotSerTrack, InnerJoin<PX.Objects.IN.InventoryItem, On<PX.Objects.IN.InventoryItem.lotSerClassID, Equal<INLotSerClass.lotSerClassID>>>, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Current<FSAppointmentDet.inventoryID>>>>))]
  [PXFormula(typeof (Default<FSAppointmentDet.inventoryID>))]
  public virtual string LotSerTrack { get; set; }

  [PXDBString(2, IsFixed = true)]
  [PXUIField(DisplayName = "Related Doc. Type", IsReadOnly = true)]
  [ListField_Linked_Entity_Type.List]
  [PXUnboundFormula(typeof (Switch<Case<Where<FSAppointmentDet.linkedEntityType, Equal<ListField_Linked_Entity_Type.apBill>>, int1>, int0>), typeof (SumCalc<FSAppointment.apBillLineCntr>))]
  public virtual string LinkedEntityType { get; set; }

  [PXDBString(4)]
  [PXUIField(Enabled = false, Visible = false)]
  public virtual string LinkedDocType { get; set; }

  [PXDBString(15, IsUnicode = true)]
  [PXUIField(Enabled = false, Visible = false)]
  public virtual string LinkedDocRefNbr { get; set; }

  [PXString]
  [PXUIField]
  public virtual string LinkedDisplayRefNbr
  {
    get
    {
      return (this.LinkedDocType != null ? this.LinkedDocType.Trim() + ", " : "") + (this.LinkedDocRefNbr != null ? this.LinkedDocRefNbr.Trim() : "");
    }
  }

  [PXDBInt]
  [PXUIField(DisplayName = "Related Doc. Line Nbr.", Visible = false, Enabled = false)]
  public virtual int? LinkedLineNbr { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(Visible = false, Enabled = false)]
  public virtual bool? SODetCreate { get; set; }

  [PXString(15, IsFixed = true)]
  [PXUIField(DisplayName = "Batch Nbr.", Enabled = false)]
  public virtual string Mem_BatchNbr { get; set; }

  public int? DocID => this.AppointmentID;

  public int? LineID => this.AppDetID;

  public int? PostAppointmentID => this.AppointmentID;

  public int? PostSODetID => this.SODetID;

  public int? PostAppDetID => this.AppDetID;

  public string BillingBy => "AP";

  public string SourceTable => nameof (FSAppointmentDet);

  public Decimal? OverageItemPrice
  {
    get => this.CuryExtraUsageUnitPrice;
    set => this.CuryExtraUsageUnitPrice = value;
  }

  public int? GetPrimaryDACDuration() => this.ActualDuration;

  public Decimal? GetPrimaryDACQty() => this.ActualQty;

  public Decimal? GetPrimaryDACTranAmt() => this.TranAmt;

  public int? GetDuration(FieldType fieldType)
  {
    if (fieldType == FieldType.EstimatedField)
      return this.EstimatedDuration;
    if (fieldType == FieldType.ActualField)
      return this.ActualDuration;
    throw new InvalidOperationException();
  }

  public int? GetApptDuration() => new int?(0);

  public Decimal? GetQty(FieldType fieldType)
  {
    if (fieldType == FieldType.EstimatedField)
      return this.EstimatedQty;
    if (fieldType == FieldType.ActualField)
      return this.ActualQty;
    if (fieldType == FieldType.BillableField)
      return this.BillableQty;
    throw new InvalidOperationException();
  }

  public Decimal? GetApptQty() => new Decimal?(0M);

  public Decimal? GetBaseQty(FieldType fieldType)
  {
    if (fieldType == FieldType.EstimatedField)
      throw new InvalidOperationException();
    if (fieldType == FieldType.ActualField)
      throw new InvalidOperationException();
    if (fieldType == FieldType.BillableField)
      return this.BaseBillableQty;
    throw new InvalidOperationException();
  }

  public Decimal? GetTranAmt(FieldType fieldType)
  {
    if (fieldType == FieldType.EstimatedField)
      return this.CuryEstimatedTranAmt;
    if (fieldType == FieldType.ActualField)
      return this.CuryTranAmt;
    if (fieldType == FieldType.BillableField)
      return this.CuryBillableTranAmt;
    throw new InvalidOperationException();
  }

  public void SetDuration(FieldType fieldType, int? duration, PXCache cache, bool raiseEvents)
  {
    switch (fieldType)
    {
      case FieldType.EstimatedField:
        if (raiseEvents)
        {
          cache.SetValueExt<FSAppointmentDet.estimatedDuration>((object) this, (object) duration);
          break;
        }
        this.EstimatedDuration = duration;
        break;
      case FieldType.ActualField:
        if (raiseEvents)
        {
          cache.SetValueExt<FSAppointmentDet.actualDuration>((object) this, (object) duration);
          break;
        }
        this.ActualDuration = duration;
        break;
      case FieldType.BillableField:
        if (raiseEvents)
        {
          cache.SetValueExt<FSAppointmentDet.actualDuration>((object) this, (object) duration);
          break;
        }
        this.ActualDuration = duration;
        break;
      default:
        throw new InvalidOperationException();
    }
  }

  public void SetQty(FieldType fieldType, Decimal? qty, PXCache cache, bool raiseEvents)
  {
    switch (fieldType)
    {
      case FieldType.EstimatedField:
        if (raiseEvents)
        {
          cache.SetValueExt<FSAppointmentDet.estimatedQty>((object) this, (object) qty);
          break;
        }
        this.EstimatedQty = qty;
        break;
      case FieldType.ActualField:
        if (raiseEvents)
        {
          cache.SetValueExt<FSAppointmentDet.actualQty>((object) this, (object) qty);
          break;
        }
        this.ActualQty = qty;
        break;
      case FieldType.BillableField:
        if (raiseEvents)
        {
          cache.SetValueExt<FSAppointmentDet.actualQty>((object) this, (object) qty);
          break;
        }
        this.ActualQty = qty;
        break;
      default:
        throw new InvalidOperationException();
    }
  }

  public virtual bool IsService => this.LineType == "SERVI" || this.LineType == "NSTKI";

  public virtual bool IsInventoryItem => this.LineType == "SLPRO";

  public virtual bool IsPickupDelivery => this.LineType == "PU_DL";

  public virtual bool needToBePosted()
  {
    if (this.LineType == "SERVI" || this.LineType == "NSTKI" || this.LineType == "SLPRO")
    {
      bool? isPrepaid = this.IsPrepaid;
      bool flag = false;
      if (isPrepaid.GetValueOrDefault() == flag & isPrepaid.HasValue && this.Status != "CC" && this.Status != "NP")
        return this.Status != "RP";
    }
    return false;
  }

  public virtual bool IsExpenseReceiptItem
  {
    get => this.LinkedEntityType == "ER" && !string.IsNullOrEmpty(this.LinkedDocRefNbr);
  }

  public virtual bool IsAPBillItem
  {
    get => this.LinkedEntityType == "AP" && !string.IsNullOrEmpty(this.LinkedDocRefNbr);
  }

  /// <summary>
  /// Linked items are lines entered from other modules with a particular behavior where UnitPrice = 0
  /// and several fields are disabled. Its status should never be changed regardless of the status of the document.
  /// </summary>
  public virtual bool IsLinkedItem => this.IsExpenseReceiptItem || this.IsAPBillItem;

  public virtual bool ShouldBeWaitingPO
  {
    get
    {
      if (this.EnablePO.GetValueOrDefault() && !this.POCompleted.GetValueOrDefault())
      {
        Decimal? baseEffTranQty = this.BaseEffTranQty;
        Decimal? allocatedFromSrvOrdPoQty = this.BaseAllocatedFromSrvOrdPOQty;
        Decimal? nullable = baseEffTranQty.HasValue & allocatedFromSrvOrdPoQty.HasValue ? new Decimal?(baseEffTranQty.GetValueOrDefault() - allocatedFromSrvOrdPoQty.GetValueOrDefault()) : new Decimal?();
        Decimal num = 0M;
        if (nullable.GetValueOrDefault() > num & nullable.HasValue)
          return !this.CanChangeMarkForPO.GetValueOrDefault() || this.POSource == "A";
      }
      return false;
    }
  }

  public virtual bool ShouldBeRequestPO
  {
    get
    {
      return this.EnablePO.GetValueOrDefault() && !this.POCompleted.GetValueOrDefault() && this.CanChangeMarkForPO.GetValueOrDefault() && this.POSource == "O";
    }
  }

  public bool IsCommentInstruction => this.LineType == "CM_LN" || this.LineType == "IT_LN";

  [PXString(InputMask = "", IsUnicode = true)]
  [PXUIField]
  [PXFormula(typeof (Selector<FSAppointmentDet.inventoryID, PX.Objects.IN.InventoryItem.inventoryCD>))]
  public virtual string InventoryCD { get; set; }

  [PXDBString(15, IsUnicode = true)]
  [PXDBDefault(typeof (FSAppointment.soRefNbr), DefaultForUpdate = false)]
  [PXUIField(DisplayName = "Service Order Nbr.", Enabled = false)]
  [PXParent(typeof (FSAppointmentDet.FK.ServiceOrder))]
  public virtual string OrigSrvOrdNbr { get; set; }

  [PXDBInt]
  [PXDefault]
  [PXUIField(DisplayName = "Service Order Line Nbr.", Visible = false, Enabled = false)]
  [PXParent(typeof (FSAppointmentDet.FK.SrvOrdLine))]
  public virtual int? OrigLineNbr { get; set; }

  [PXDBDecimal(6)]
  [PXFormula(typeof (decimal0))]
  [PXUIField(DisplayName = "Unassigned Qty.", Visible = false, Enabled = false)]
  public virtual Decimal? UnassignedQty { get; set; }

  [PXString(1, IsFixed = true, InputMask = ">a")]
  [PXUnboundDefault(typeof (SOOperation.issue))]
  public virtual string Operation
  {
    get => this._Operation;
    set => this._Operation = value;
  }

  [PXString(3, IsFixed = true)]
  [PXUnboundDefault(typeof (INTranType.issue))]
  public virtual string TranType
  {
    get => this._TranType;
    set => this._TranType = value;
  }

  [PXShort]
  [PXFormula(typeof (shortMinus1))]
  [PXUIField(DisplayName = "Inventory Multiplier")]
  public virtual short? InvtMult { get; set; }

  [PXInt]
  public virtual int? LocationID
  {
    get => this.SiteLocationID;
    set => this.SiteLocationID = value;
  }

  [PXDate(InputMask = "d", DisplayMask = "d")]
  [PXUIField(DisplayName = "Expiration Date", FieldClass = "LotSerial")]
  public virtual DateTime? ExpireDate { get; set; }

  [PXInt]
  public virtual int? TaskID
  {
    get => this.ProjectTaskID;
    set => this.ProjectTaskID = value;
  }

  [PXBool]
  public virtual bool? IsLotSerialRequired { get; set; }

  public static implicit operator FSApptLineSplit(FSAppointmentDet item)
  {
    return new FSApptLineSplit()
    {
      SrvOrdType = item.SrvOrdType,
      ApptNbr = item.RefNbr,
      LineNbr = item.LineNbr,
      Operation = item.Operation,
      SplitLineNbr = new int?(1),
      InventoryID = item.InventoryID,
      SiteID = item.SiteID,
      SubItemID = item.SubItemID,
      LocationID = item.LocationID,
      CostCenterID = item.CostCenterID,
      LotSerialNbr = item.LotSerialNbr,
      ExpireDate = item.ExpireDate,
      Qty = item.ActualQty,
      BaseQty = item.BaseQty,
      UOM = item.UOM,
      InvtMult = item.InvtMult
    };
  }

  public static implicit operator FSAppointmentDet(FSApptLineSplit item)
  {
    return new FSAppointmentDet()
    {
      SrvOrdType = item.SrvOrdType,
      RefNbr = item.ApptNbr,
      LineNbr = item.LineNbr,
      LineType = "GI",
      Operation = item.Operation,
      InventoryID = item.InventoryID,
      SiteID = item.SiteID,
      SubItemID = item.SubItemID,
      LocationID = item.LocationID,
      CostCenterID = item.CostCenterID,
      LotSerialNbr = item.LotSerialNbr,
      ActualQty = item.Qty,
      UOM = item.UOM,
      BaseQty = item.BaseQty,
      InvtMult = item.InvtMult
    };
  }

  [PXDecimal]
  [PXDefault(typeof (FSAppointmentDet.actualQty))]
  public virtual Decimal? INOpenQty { get; set; }

  bool? ILSMaster.IsIntercompany => new bool?(false);

  bool? ILSPrimary.IsStockItem
  {
    set
    {
    }
  }

  public class PK : 
    PrimaryKeyOf<FSAppointmentDet>.By<FSAppointmentDet.srvOrdType, FSAppointmentDet.refNbr, FSAppointmentDet.lineNbr>
  {
    public static FSAppointmentDet Find(
      PXGraph graph,
      string srvOrdType,
      string refNbr,
      int? lineNbr,
      PKFindOptions options = 0)
    {
      return (FSAppointmentDet) PrimaryKeyOf<FSAppointmentDet>.By<FSAppointmentDet.srvOrdType, FSAppointmentDet.refNbr, FSAppointmentDet.lineNbr>.FindBy(graph, (object) srvOrdType, (object) refNbr, (object) lineNbr, options);
    }
  }

  public static class FK
  {
    public class Branch : 
      PrimaryKeyOf<PX.Objects.GL.Branch>.By<PX.Objects.GL.Branch.branchID>.ForeignKeyOf<FSAppointmentDet>.By<FSAppointmentDet.branchID>
    {
    }

    public class ServiceOrderType : 
      PrimaryKeyOf<FSSrvOrdType>.By<FSSrvOrdType.srvOrdType>.ForeignKeyOf<FSAppointmentDet>.By<FSAppointmentDet.srvOrdType>
    {
    }

    public class Appointment : 
      PrimaryKeyOf<FSAppointment>.By<FSAppointment.srvOrdType, FSAppointment.refNbr>.ForeignKeyOf<FSAppointmentDet>.By<FSAppointmentDet.srvOrdType, FSAppointmentDet.refNbr>
    {
    }

    public class ServiceOrder : 
      PrimaryKeyOf<FSServiceOrder>.By<FSServiceOrder.srvOrdType, FSServiceOrder.refNbr>.ForeignKeyOf<FSAppointmentDet>.By<FSAppointmentDet.srvOrdType, FSAppointmentDet.origSrvOrdNbr>
    {
    }

    public class SrvOrdLine : 
      PrimaryKeyOf<FSSODet>.By<FSSODet.srvOrdType, FSSODet.refNbr, FSSODet.lineNbr>.ForeignKeyOf<FSAppointmentDet>.By<FSAppointmentDet.srvOrdType, FSAppointmentDet.origSrvOrdNbr, FSAppointmentDet.origLineNbr>
    {
    }

    public class InventoryItem : 
      PrimaryKeyOf<PX.Objects.IN.InventoryItem>.By<PX.Objects.IN.InventoryItem.inventoryID>.ForeignKeyOf<FSAppointmentDet>.By<FSAppointmentDet.inventoryID>
    {
    }

    public class SubItem : 
      PrimaryKeyOf<INSubItem>.By<INSubItem.subItemID>.ForeignKeyOf<FSAppointmentDet>.By<FSAppointmentDet.subItemID>
    {
    }

    public class Site : 
      PrimaryKeyOf<PX.Objects.IN.INSite>.By<PX.Objects.IN.INSite.siteID>.ForeignKeyOf<FSAppointmentDet>.By<FSAppointmentDet.siteID>
    {
    }

    public class SiteStatus : 
      PrimaryKeyOf<INSiteStatus>.By<INSiteStatus.inventoryID, INSiteStatus.subItemID, INSiteStatus.siteID>.ForeignKeyOf<FSAppointmentDet>.By<FSAppointmentDet.inventoryID, FSAppointmentDet.subItemID, FSAppointmentDet.siteID>
    {
    }

    public class Location : 
      PrimaryKeyOf<INLocation>.By<INLocation.locationID>.ForeignKeyOf<FSAppointmentDet>.By<FSAppointmentDet.locationID>
    {
    }

    public class CostCenter : 
      PrimaryKeyOf<INCostCenter>.By<INCostCenter.costCenterID>.ForeignKeyOf<FSAppointmentDet>.By<FSAppointmentDet.costCenterID>
    {
    }

    public class LocationStatus : 
      PrimaryKeyOf<INLocationStatus>.By<INLocationStatus.inventoryID, INLocationStatus.subItemID, INLocationStatus.siteID, INLocationStatus.locationID>.ForeignKeyOf<FSAppointmentDet>.By<FSAppointmentDet.inventoryID, FSAppointmentDet.subItemID, FSAppointmentDet.siteID, FSAppointmentDet.locationID>
    {
    }

    public class LotSerialStatus : 
      PrimaryKeyOf<INLotSerialStatus>.By<INLotSerialStatus.inventoryID, INLotSerialStatus.subItemID, INLotSerialStatus.siteID, INLotSerialStatus.locationID, INLotSerialStatus.lotSerialNbr>.ForeignKeyOf<FSAppointmentDet>.By<FSAppointmentDet.inventoryID, FSAppointmentDet.subItemID, FSAppointmentDet.siteID, FSAppointmentDet.locationID, FSAppointmentDet.lotSerialNbr>
    {
    }

    public class CurrencyInfo : 
      PrimaryKeyOf<PX.Objects.CM.CurrencyInfo>.By<PX.Objects.CM.CurrencyInfo.curyInfoID>.ForeignKeyOf<FSAppointmentDet>.By<FSAppointmentDet.curyInfoID>
    {
    }

    public class TaxCategory : 
      PrimaryKeyOf<PX.Objects.TX.TaxCategory>.By<PX.Objects.TX.TaxCategory.taxCategoryID>.ForeignKeyOf<FSAppointmentDet>.By<FSAppointmentDet.taxCategoryID>
    {
    }

    public class Project : 
      PrimaryKeyOf<PMProject>.By<PMProject.contractID>.ForeignKeyOf<FSAppointmentDet>.By<FSAppointmentDet.projectID>
    {
    }

    public class Task : 
      PrimaryKeyOf<PMTask>.By<PMTask.projectID, PMTask.taskID>.ForeignKeyOf<FSAppointmentDet>.By<FSAppointmentDet.projectID, FSAppointmentDet.projectTaskID>
    {
    }

    public class Account : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<FSAppointmentDet>.By<FSAppointmentDet.acctID>
    {
    }

    public class Subaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<FSAppointmentDet>.By<FSAppointmentDet.subID>
    {
    }

    public class CostCode : 
      PrimaryKeyOf<PMCostCode>.By<PMCostCode.costCodeID>.ForeignKeyOf<FSAppointmentDet>.By<FSAppointmentDet.costCodeID>
    {
    }

    public class Discount : 
      PrimaryKeyOf<ARDiscount>.By<ARDiscount.discountID>.ForeignKeyOf<FSAppointmentDet>.By<FSAppointmentDet.discountID>
    {
    }

    public class Vendor : 
      PrimaryKeyOf<PX.Objects.AP.Vendor>.By<PX.Objects.AP.Vendor.bAccountID>.ForeignKeyOf<FSAppointmentDet>.By<FSAppointmentDet.poVendorID>
    {
    }

    public class Equipment : 
      PrimaryKeyOf<FSEquipment>.By<FSEquipment.SMequipmentID>.ForeignKeyOf<FSAppointmentDet>.By<FSAppointmentDet.SMequipmentID>
    {
    }

    public class Component : 
      PrimaryKeyOf<FSModelTemplateComponent>.By<FSModelTemplateComponent.componentID>.ForeignKeyOf<FSAppointmentDet>.By<FSAppointmentDet.componentID>
    {
    }

    public class EquipmentComponent : 
      PrimaryKeyOf<FSEquipmentComponent>.By<FSEquipmentComponent.SMequipmentID, FSEquipmentComponent.lineNbr>.ForeignKeyOf<FSAppointmentDet>.By<FSAppointmentDet.SMequipmentID, FSAppointmentDet.equipmentLineRef>
    {
    }

    public class PostInfo : 
      PrimaryKeyOf<FSPostInfo>.By<FSPostInfo.postID>.ForeignKeyOf<FSAppointmentDet>.By<FSAppointmentDet.postID>
    {
    }

    public class PurchaseOrderType : 
      PrimaryKeyOf<PX.Objects.SO.SOOrderType>.By<PX.Objects.SO.SOOrderType.orderType>.ForeignKeyOf<FSAppointmentDet>.By<FSAppointmentDet.poType>
    {
    }

    public class PurchaseOrder : 
      PrimaryKeyOf<PX.Objects.PO.POOrder>.By<PX.Objects.PO.POOrder.orderType, PX.Objects.PO.POOrder.orderNbr>.ForeignKeyOf<FSAppointmentDet>.By<FSAppointmentDet.poType, FSAppointmentDet.poNbr>
    {
    }

    public class Schedule : 
      PrimaryKeyOf<FSSchedule>.By<FSSchedule.scheduleID>.ForeignKeyOf<FSAppointmentDet>.By<FSAppointmentDet.scheduleID>
    {
    }

    public class ScheduleDetail : 
      PrimaryKeyOf<FSScheduleDet>.By<FSScheduleDet.scheduleID, FSScheduleDet.scheduleDetID>.ForeignKeyOf<FSAppointmentDet>.By<FSAppointmentDet.scheduleID, FSAppointmentDet.scheduleDetID>
    {
    }

    public class Staff : 
      PrimaryKeyOf<PX.Objects.CR.BAccount>.By<PX.Objects.CR.BAccount.bAccountID>.ForeignKeyOf<FSAppointmentDet>.By<FSAppointmentDet.staffID>
    {
    }
  }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSAppointmentDet.selected>
  {
  }

  public abstract class srvOrdType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSAppointmentDet.srvOrdType>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSAppointmentDet.refNbr>
  {
  }

  public abstract class appointmentID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSAppointmentDet.appointmentID>
  {
  }

  public abstract class appDetID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSAppointmentDet.appDetID>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSAppointmentDet.lineNbr>
  {
  }

  public abstract class sortOrder : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSAppointmentDet.sortOrder>
  {
  }

  public abstract class sODetID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSAppointmentDet.sODetID>
  {
  }

  public abstract class lineRef : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSAppointmentDet.lineRef>
  {
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSAppointmentDet.branchID>
  {
  }

  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  FSAppointmentDet.curyInfoID>
  {
  }

  public abstract class lineType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSAppointmentDet.lineType>
  {
  }

  public abstract class isPrepaid : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSAppointmentDet.isPrepaid>
  {
  }

  public abstract class pickupDeliveryAppLineRef : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSAppointmentDet.pickupDeliveryAppLineRef>
  {
  }

  public abstract class pickupDeliveryServiceID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSAppointmentDet.pickupDeliveryServiceID>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSAppointmentDet.inventoryID>
  {
  }

  public abstract class subItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSAppointmentDet.subItemID>
  {
  }

  public abstract class uOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSAppointmentDet.uOM>
  {
  }

  public abstract class billingRule : ListField_BillingRule
  {
  }

  public abstract class serviceType : ListField_Appointment_Service_Action_Type
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSAppointmentDet.siteID>
  {
  }

  public abstract class siteLocationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSAppointmentDet.siteLocationID>
  {
  }

  public abstract class isTravelItem : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSAppointmentDet.isTravelItem>
  {
  }

  public abstract class sOLineType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSAppointmentDet.sOLineType>
  {
  }

  public abstract class ListField_Status_AppointmentDet : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSAppointmentDet.status>
  {
    public const string NOT_STARTED = "NS";
    public const string IN_PROCESS = "IP";
    public const string COMPLETED = "CP";
    public const string NOT_FINISHED = "NF";
    public const string NOT_PERFORMED = "NP";
    public const string CANCELED = "CC";
    public const string WaitingForPO = "WP";
    public const string RequestForPO = "RP";

    public class ListAttribute : PXStringListAttribute
    {
      public static List<Tuple<string, string>> FullList = new List<Tuple<string, string>>()
      {
        new Tuple<string, string>("NS", "Not Started"),
        new Tuple<string, string>("IP", "In Process"),
        new Tuple<string, string>("CP", "Completed"),
        new Tuple<string, string>("NF", "Not Finished"),
        new Tuple<string, string>("NP", "Not Performed"),
        new Tuple<string, string>("CC", "Canceled"),
        new Tuple<string, string>("WP", "Waiting for Purchased Items"),
        new Tuple<string, string>("RP", "Requiring Purchase")
      };

      public ListAttribute()
        : base(FSAppointmentDet.ListField_Status_AppointmentDet.ListAttribute.FullList.ToArray())
      {
      }
    }

    public class NotStarted : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      FSAppointmentDet.ListField_Status_AppointmentDet.NotStarted>
    {
      public NotStarted()
        : base("NS")
      {
      }
    }

    public class InProcess : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      FSAppointmentDet.ListField_Status_AppointmentDet.InProcess>
    {
      public InProcess()
        : base("IP")
      {
      }
    }

    public class NotFinished : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      FSAppointmentDet.ListField_Status_AppointmentDet.NotFinished>
    {
      public NotFinished()
        : base("NF")
      {
      }
    }

    public class NotPerformed : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      FSAppointmentDet.ListField_Status_AppointmentDet.NotPerformed>
    {
      public NotPerformed()
        : base("NP")
      {
      }
    }

    public class Completed : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      FSAppointmentDet.ListField_Status_AppointmentDet.Completed>
    {
      public Completed()
        : base("CP")
      {
      }
    }

    public class Canceled : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      FSAppointmentDet.ListField_Status_AppointmentDet.Canceled>
    {
      public Canceled()
        : base("CC")
      {
      }
    }

    public class waitingForPO : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      FSAppointmentDet.ListField_Status_AppointmentDet.waitingForPO>
    {
      public waitingForPO()
        : base("WP")
      {
      }
    }

    public class requestForPO : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      FSAppointmentDet.ListField_Status_AppointmentDet.requestForPO>
    {
      public requestForPO()
        : base("RP")
      {
      }
    }
  }

  public abstract class status : FSAppointmentDet.ListField_Status_AppointmentDet
  {
  }

  public abstract class uiStatus : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSAppointmentDet.uiStatus>
  {
    public abstract class Values : FSAppointmentDet.ListField_Status_AppointmentDet
    {
    }
  }

  public abstract class isCanceledNotPerformed : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSAppointmentDet.isCanceledNotPerformed>
  {
  }

  public abstract class lotSerialNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSAppointmentDet.lotSerialNbr>
  {
  }

  public abstract class estimatedDuration : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSAppointmentDet.estimatedDuration>
  {
  }

  public abstract class estimatedQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSAppointmentDet.estimatedQty>
  {
  }

  public abstract class baseEstimatedQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSAppointmentDet.baseEstimatedQty>
  {
  }

  public abstract class logActualDuration : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSAppointmentDet.logActualDuration>
  {
  }

  public abstract class areActualFieldsActive : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSAppointmentDet.areActualFieldsActive>
  {
  }

  public abstract class actualDuration : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSAppointmentDet.actualDuration>
  {
  }

  public abstract class actualQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FSAppointmentDet.actualQty>
  {
  }

  public abstract class baseActualQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSAppointmentDet.baseActualQty>
  {
  }

  public abstract class effTranQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FSAppointmentDet.effTranQty>
  {
  }

  public abstract class qty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FSAppointmentDet.qty>
  {
  }

  public abstract class baseEffTranQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSAppointmentDet.baseEffTranQty>
  {
  }

  public abstract class manualPrice : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSAppointmentDet.manualPrice>
  {
  }

  public abstract class isBillable : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSAppointmentDet.isBillable>
  {
  }

  public abstract class isFree : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSAppointmentDet.isFree>
  {
  }

  public abstract class billableQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSAppointmentDet.billableQty>
  {
  }

  public abstract class baseBillableQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSAppointmentDet.baseBillableQty>
  {
  }

  public abstract class tranDesc : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSAppointmentDet.tranDesc>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FSAppointmentDet.noteID>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FSAppointmentDet.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSAppointmentDet.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSAppointmentDet.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    FSAppointmentDet.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSAppointmentDet.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSAppointmentDet.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  FSAppointmentDet.Tstamp>
  {
  }

  public abstract class expenseEmployeeID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSAppointmentDet.expenseEmployeeID>
  {
  }

  public abstract class tranDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  FSAppointmentDet.tranDate>
  {
  }

  public abstract class curyUnitCost : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSAppointmentDet.curyUnitCost>
  {
  }

  public abstract class unitCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FSAppointmentDet.unitCost>
  {
  }

  public abstract class curyEstimatedExtCost : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSAppointmentDet.curyEstimatedExtCost>
  {
  }

  public abstract class estimatedExtCost : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSAppointmentDet.estimatedExtCost>
  {
  }

  public abstract class manualCost : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSAppointmentDet.manualCost>
  {
  }

  public abstract class curyUnitPrice : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSAppointmentDet.curyUnitPrice>
  {
  }

  public abstract class unitPrice : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FSAppointmentDet.unitPrice>
  {
  }

  public abstract class estimatedTranAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSAppointmentDet.estimatedTranAmt>
  {
  }

  public abstract class curyEstimatedTranAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSAppointmentDet.curyEstimatedTranAmt>
  {
  }

  public abstract class curyExtCost : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSAppointmentDet.curyExtCost>
  {
  }

  public abstract class extCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FSAppointmentDet.extCost>
  {
  }

  public abstract class curyBillableExtPrice : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSAppointmentDet.curyBillableExtPrice>
  {
  }

  public abstract class billableExtPrice : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSAppointmentDet.billableExtPrice>
  {
  }

  public abstract class curyBillableTranAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSAppointmentDet.curyBillableTranAmt>
  {
  }

  public abstract class billableTranAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSAppointmentDet.billableTranAmt>
  {
  }

  public abstract class curyExtPrice : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSAppointmentDet.curyExtPrice>
  {
  }

  public abstract class curyLineAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSAppointmentDet.curyLineAmt>
  {
  }

  public abstract class curyTranAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSAppointmentDet.curyTranAmt>
  {
  }

  public abstract class tranAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FSAppointmentDet.tranAmt>
  {
  }

  public abstract class manualDisc : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSAppointmentDet.manualDisc>
  {
  }

  public abstract class discPct : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FSAppointmentDet.discPct>
  {
  }

  public abstract class curyDiscAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSAppointmentDet.curyDiscAmt>
  {
  }

  public abstract class discAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FSAppointmentDet.discAmt>
  {
  }

  public abstract class discountsAppliedToLine : 
    BqlType<
    #nullable enable
    IBqlByteArray, byte[]>.Field<
    #nullable disable
    FSAppointmentDet.discountsAppliedToLine>
  {
  }

  public abstract class discountID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSAppointmentDet.discountID>
  {
  }

  public abstract class discountSequenceID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSAppointmentDet.discountSequenceID>
  {
  }

  public abstract class postID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSAppointmentDet.postID>
  {
  }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSAppointmentDet.projectID>
  {
  }

  public abstract class projectTaskID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSAppointmentDet.projectTaskID>
  {
  }

  public abstract class scheduleDetID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSAppointmentDet.scheduleDetID>
  {
  }

  /// <exclude />
  public abstract class costCenterID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSAppointmentDet.costCenterID>
  {
  }

  public abstract class equipmentAction : ListField_EquipmentAction
  {
  }

  public abstract class SMequipmentID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSAppointmentDet.SMequipmentID>
  {
  }

  public abstract class newTargetEquipmentLineNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSAppointmentDet.newTargetEquipmentLineNbr>
  {
  }

  public abstract class componentID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSAppointmentDet.componentID>
  {
  }

  public abstract class equipmentLineRef : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSAppointmentDet.equipmentLineRef>
  {
  }

  public abstract class staffID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSAppointmentDet.staffID>
  {
  }

  public abstract class priceType : ListField_PriceType
  {
  }

  public abstract class priceCode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSAppointmentDet.priceCode>
  {
  }

  public abstract class acctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSAppointmentDet.acctID>
  {
  }

  public abstract class subID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSAppointmentDet.subID>
  {
  }

  public abstract class scheduleID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSAppointmentDet.scheduleID>
  {
  }

  public abstract class costCodeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSAppointmentDet.costCodeID>
  {
  }

  public abstract class skipCostCodeValidation : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSAppointmentDet.skipCostCodeValidation>
  {
  }

  public abstract class inventoryIDReport : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSAppointmentDet.inventoryIDReport>
  {
  }

  public abstract class warranty : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSAppointmentDet.warranty>
  {
  }

  public abstract class comment : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSAppointmentDet.comment>
  {
  }

  public abstract class equipmentItemClass : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSAppointmentDet.equipmentItemClass>
  {
  }

  public abstract class taxCategoryID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSAppointmentDet.taxCategoryID>
  {
  }

  public abstract class groupDiscountRate : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSAppointmentDet.groupDiscountRate>
  {
  }

  public abstract class documentDiscountRate : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSAppointmentDet.documentDiscountRate>
  {
  }

  public abstract class contractRelated : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSAppointmentDet.contractRelated>
  {
  }

  public abstract class coveredQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FSAppointmentDet.coveredQty>
  {
  }

  public abstract class extraUsageQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSAppointmentDet.extraUsageQty>
  {
  }

  public abstract class extraUsageUnitPrice : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSAppointmentDet.extraUsageUnitPrice>
  {
  }

  public abstract class curyExtraUsageUnitPrice : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSAppointmentDet.curyExtraUsageUnitPrice>
  {
  }

  public abstract class enablePO : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSAppointmentDet.enablePO>
  {
  }

  public abstract class canChangeMarkForPO : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSAppointmentDet.canChangeMarkForPO>
  {
  }

  public abstract class poCompleted : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSAppointmentDet.poCompleted>
  {
  }

  public abstract class poType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSAppointmentDet.poType>
  {
  }

  public abstract class pOSource : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSAppointmentDet.pOSource>
  {
    public abstract class Values : ListField_FSPOSource
    {
    }
  }

  public abstract class poVendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSAppointmentDet.poVendorID>
  {
  }

  public abstract class poVendorLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSAppointmentDet.poVendorLocationID>
  {
  }

  public abstract class poNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSAppointmentDet.poNbr>
  {
  }

  public abstract class poLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSAppointmentDet.poLineNbr>
  {
  }

  public abstract class poStatus : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSAppointmentDet.poStatus>
  {
  }

  public abstract class allocatedFromSrvOrdPOQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSAppointmentDet.allocatedFromSrvOrdPOQty>
  {
  }

  public abstract class baseAllocatedFromSrvOrdPOQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSAppointmentDet.baseAllocatedFromSrvOrdPOQty>
  {
  }

  public abstract class tabOrigin : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSAppointmentDet.tabOrigin>
  {
  }

  public abstract class lotSerTrack : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSAppointmentDet.lotSerTrack>
  {
  }

  public abstract class linkedEntityType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSAppointmentDet.linkedEntityType>
  {
    public abstract class Values : ListField_Linked_Entity_Type
    {
    }
  }

  public abstract class linkedDocType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSAppointmentDet.linkedDocType>
  {
  }

  public abstract class linkedDocRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSAppointmentDet.linkedDocRefNbr>
  {
  }

  public abstract class linkedDisplayRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSAppointmentDet.linkedDisplayRefNbr>
  {
  }

  public abstract class linkedLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSAppointmentDet.linkedLineNbr>
  {
  }

  public abstract class sODetCreate : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSAppointmentDet.sODetCreate>
  {
  }

  public abstract class mem_BatchNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSAppointmentDet.mem_BatchNbr>
  {
  }

  public abstract class inventoryCD : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSAppointmentDet.inventoryCD>
  {
  }

  public abstract class origSrvOrdNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSAppointmentDet.origSrvOrdNbr>
  {
  }

  public abstract class origLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSAppointmentDet.origLineNbr>
  {
  }

  public abstract class unassignedQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FSAppointmentDet.unassignedQty>
  {
  }

  public abstract class operation : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSAppointmentDet.operation>
  {
  }

  public abstract class tranType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSAppointmentDet.tranType>
  {
  }

  public abstract class invtMult : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  FSAppointmentDet.invtMult>
  {
  }

  public abstract class locationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSAppointmentDet.locationID>
  {
  }

  public abstract class expireDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSAppointmentDet.expireDate>
  {
  }

  public abstract class taskID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSAppointmentDet.taskID>
  {
  }

  public abstract class isLotSerialRequired : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSAppointmentDet.isLotSerialRequired>
  {
  }

  public abstract class inOpenQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FSAppointmentDet.inOpenQty>
  {
  }
}
