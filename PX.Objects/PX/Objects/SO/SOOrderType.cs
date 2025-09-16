// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOOrderType
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.Common;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.IN;
using System;

#nullable enable
namespace PX.Objects.SO;

[PXPrimaryGraph(typeof (SOOrderTypeMaint))]
[PXCacheName]
public class SOOrderType : 
  PXBqlTable,
  IBqlTable,
  IBqlTableSystemDataStorage,
  PXNoteAttribute.IPXCopySettings
{
  protected 
  #nullable disable
  string _OrderType;
  protected bool? _Active;
  protected short? _DaysToKeep;
  protected string _Descr;
  protected string _Template;
  protected bool? _IsSystem;
  protected string _Behavior;
  protected string _DefaultOperation;
  protected string _INDocType;
  protected string _ARDocType;
  protected string _OrderPlanType;
  protected string _ShipmentPlanType;
  protected string _OrderNumberingID;
  protected string _InvoiceNumberingID;
  protected bool? _UserInvoiceNumbering;
  protected bool? _MarkInvoicePrinted;
  protected bool? _MarkInvoiceEmailed;
  protected bool? _HoldEntry;
  protected bool? _CreditHoldEntry;
  protected bool? _RequireAllocation;
  protected bool? _RequireLotSerial;
  protected bool? _AllowQuickProcess;
  protected bool? _RequireControlTotal;
  protected bool? _RequireShipping;
  protected bool? _CopyLotSerialFromShipment;
  protected bool? _BillSeparately;
  protected bool? _ShipSeparately;
  protected string _SalesAcctDefault;
  protected string _MiscAcctDefault;
  protected string _FreightAcctDefault;
  protected string _DiscAcctDefault;
  protected string _COGSAcctDefault;
  protected string _SalesSubMask;
  protected string _MiscSubMask;
  protected string _FreightSubMask;
  protected string _DiscSubMask;
  protected int? _FreightAcctID;
  protected int? _FreightSubID;
  protected bool? _UseShippedNotInvoiced;
  protected int? _ShippedNotInvoicedAcctID;
  protected int? _ShippedNotInvoicedSubID;
  protected bool? _DisableAutomaticDiscountCalculation;
  protected bool? _RecalculateDiscOnPartialShipment;
  protected bool? _CalculateFreight;
  protected string _COGSSubMask;
  protected int? _DiscountAcctID;
  protected int? _DiscountSubID;
  protected short? _OrderPriority;
  protected bool? _CopyNotes;
  protected bool? _CopyFiles;
  protected bool? _CopyLineNotesToShipment;
  protected bool? _CopyLineFilesToShipment;
  protected bool? _CopyLineNotesToInvoice;
  protected bool? _CopyLineNotesToInvoiceOnlyNS;
  protected bool? _CopyLineFilesToInvoice;
  protected bool? _CopyLineFilesToInvoiceOnlyNS;
  protected bool? _PostLineDiscSeparately;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;
  protected byte[] _tstamp;

  [PXDBString(2, IsKey = true, IsFixed = true, InputMask = ">aa")]
  [PXUIField]
  [PXDefault]
  [PXSelector(typeof (Search2<SOOrderType.orderType, InnerJoin<SOOrderTypeOperation, On<SOOrderTypeOperation.orderType, Equal<SOOrderType.orderType>, And<SOOrderTypeOperation.operation, Equal<SOOrderType.defaultOperation>>>>, Where<SOOrderType.requireShipping, Equal<boolFalse>, And<SOOrderType.behavior, NotEqual<SOBehavior.bL>, Or<FeatureInstalled<FeaturesSet.inventory>>>>>))]
  [PXRestrictor(typeof (Where<SOOrderTypeOperation.iNDocType, NotEqual<INTranType.transfer>, Or<FeatureInstalled<FeaturesSet.warehouse>>>), null, new Type[] {})]
  [PXRestrictor(typeof (Where<SOOrderType.requireAllocation, NotEqual<True>, Or<AllocationAllowed>>), null, new Type[] {})]
  public virtual string OrderType
  {
    get => this._OrderType;
    set => this._OrderType = value;
  }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Active")]
  public virtual bool? Active
  {
    get => this._Active;
    set => this._Active = value;
  }

  [PXDBShort]
  [PXDefault(30)]
  [PXUIField(DisplayName = "Days To Keep")]
  public virtual short? DaysToKeep
  {
    get => this._DaysToKeep;
    set => this._DaysToKeep = value;
  }

  [PXDBLocalizableString(60, IsUnicode = true)]
  [PXUIField]
  public virtual string Descr
  {
    get => this._Descr;
    set => this._Descr = value;
  }

  [PXDBString(2, IsFixed = true, InputMask = ">aa")]
  [PXUIField]
  [PXDefault]
  [PXSelector(typeof (Search<SOOrderTypeT.orderType, Where2<Where<SOOrderTypeT.requireAllocation, NotEqual<True>, Or2<FeatureInstalled<FeaturesSet.warehouseLocation>, Or2<FeatureInstalled<FeaturesSet.lotSerialTracking>, Or2<FeatureInstalled<FeaturesSet.subItem>, Or2<FeatureInstalled<FeaturesSet.replenishment>, Or<FeatureInstalled<FeaturesSet.sOToPOLink>>>>>>>, And<Where<SOOrderTypeT.requireShipping, Equal<boolFalse>, And<SOOrderTypeT.behavior, NotEqual<SOBehavior.bL>, Or<FeatureInstalled<FeaturesSet.inventory>>>>>>>), DirtyRead = true, DescriptionField = typeof (SOOrderTypeT.descr))]
  public virtual string Template
  {
    get => this._Template;
    set => this._Template = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Is System Template", Enabled = false)]
  public virtual bool? IsSystem
  {
    get => this._IsSystem;
    set => this._IsSystem = value;
  }

  [PXDBString(2, IsFixed = true, InputMask = ">aa")]
  [PXUIField]
  [PXDefault]
  [SOBehavior.List]
  public virtual string Behavior
  {
    get => this._Behavior;
    set => this._Behavior = value;
  }

  [PXDBString(1, IsFixed = true, InputMask = ">a")]
  [PXUIField(DisplayName = "Default Operation")]
  [PXDefault(typeof (Search<SOOrderType.defaultOperation, Where<SOOrderType.orderType, Equal<Current<SOOrderType.behavior>>>>))]
  [SOOperation.List]
  public virtual string DefaultOperation
  {
    get => this._DefaultOperation;
    set => this._DefaultOperation = value;
  }

  [PXDBString(3, IsFixed = true)]
  [PXDefault]
  [INTranType.SOList]
  [PXUIEnabled(typeof (Where<BqlOperand<SOOrderType.behavior, IBqlString>.IsNotEqual<SOBehavior.tR>>))]
  [PXFormula(typeof (BqlOperand<INTranType.transfer, IBqlString>.When<BqlOperand<SOOrderType.behavior, IBqlString>.IsEqual<SOBehavior.tR>>.Else<SOOrderType.iNDocType>))]
  [PXUIField(DisplayName = "Inventory Transaction Type")]
  public virtual string INDocType
  {
    get => this._INDocType;
    set => this._INDocType = value;
  }

  [PXDBString(3, IsFixed = true)]
  [PXDefault]
  [PX.Objects.AR.ARDocType.SOFullList]
  [PXUIField(DisplayName = "AR Document Type")]
  public virtual string ARDocType
  {
    get => this._ARDocType;
    set => this._ARDocType = value;
  }

  [PXDBString(2, IsFixed = true, InputMask = ">aa")]
  [PXUIField(DisplayName = "Order Plan Type")]
  [PXSelector(typeof (Search<INPlanType.planType>), DescriptionField = typeof (INPlanType.localizedDescr))]
  public virtual string OrderPlanType
  {
    get => this._OrderPlanType;
    set => this._OrderPlanType = value;
  }

  [PXDBString(2, IsFixed = true, InputMask = ">aa")]
  [PXUIField(DisplayName = "Shipment Plan Type")]
  [PXSelector(typeof (Search<INPlanType.planType>), DescriptionField = typeof (INPlanType.localizedDescr))]
  public virtual string ShipmentPlanType
  {
    get => this._ShipmentPlanType;
    set => this._ShipmentPlanType = value;
  }

  [PXDBString(10, IsUnicode = true)]
  [PXDefault]
  [PXSelector(typeof (Search<Numbering.numberingID>))]
  [PXUIField(DisplayName = "Order Numbering Sequence")]
  public virtual string OrderNumberingID
  {
    get => this._OrderNumberingID;
    set => this._OrderNumberingID = value;
  }

  [PXDBString(10, IsUnicode = true)]
  [PXDefault("ARINVOICE")]
  [PXSelector(typeof (Search<Numbering.numberingID>))]
  [PXUIField(DisplayName = "Invoice Numbering Sequence")]
  public virtual string InvoiceNumberingID
  {
    get => this._InvoiceNumberingID;
    set => this._InvoiceNumberingID = value;
  }

  [PXBool]
  [PXFormula(typeof (Selector<SOOrderType.invoiceNumberingID, Numbering.userNumbering>))]
  [PXUIField(DisplayName = "Manual Invoice Numbering")]
  public virtual bool? UserInvoiceNumbering
  {
    get => this._UserInvoiceNumbering;
    set => this._UserInvoiceNumbering = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Mark as Printed")]
  public virtual bool? MarkInvoicePrinted
  {
    get => this._MarkInvoicePrinted;
    set => this._MarkInvoicePrinted = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Mark as Emailed")]
  public virtual bool? MarkInvoiceEmailed
  {
    get => this._MarkInvoiceEmailed;
    set => this._MarkInvoiceEmailed = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Hold Orders on Entry")]
  public virtual bool? HoldEntry
  {
    get => this._HoldEntry;
    set => this._HoldEntry = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIEnabled(typeof (Where<BqlOperand<SOOrderType.behavior, IBqlString>.IsNotEqual<SOBehavior.tR>>))]
  [PXFormula(typeof (BqlOperand<False, IBqlBool>.When<BqlOperand<SOOrderType.behavior, IBqlString>.IsEqual<SOBehavior.tR>>.Else<SOOrderType.invoiceHoldEntry>))]
  [PXUIField(DisplayName = "Hold Invoices on Entry")]
  public virtual bool? InvoiceHoldEntry { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIEnabled(typeof (Where<BqlOperand<SOOrderType.behavior, IBqlString>.IsIn<SOBehavior.sO, SOBehavior.iN, SOBehavior.rM, SOBehavior.mO>>))]
  [PXFormula(typeof (BqlOperand<False, IBqlBool>.When<BqlOperand<SOOrderType.behavior, IBqlString>.IsNotIn<SOBehavior.sO, SOBehavior.iN, SOBehavior.rM, SOBehavior.mO>>.Else<SOOrderType.creditHoldEntry>))]
  [PXUIField(DisplayName = "Hold Document on Failed Credit Check")]
  public virtual bool? CreditHoldEntry
  {
    get => this._CreditHoldEntry;
    set => this._CreditHoldEntry = value;
  }

  /// <summary>
  /// If true and a sales order is put on Credit Hold, on the payment application to a full sales order balance,
  /// sales order is automatically released from Credit Hold and goes to the Open status.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIEnabled(typeof (Where<BqlOperand<SOOrderType.creditHoldEntry, IBqlBool>.IsEqual<True>>))]
  [PXFormula(typeof (SOOrderType.creditHoldEntry))]
  [PXUIField(DisplayName = "Remove Credit Hold on Payment Application")]
  [PXUIVisible(typeof (Where<BqlOperand<SOOrderType.behavior, IBqlString>.IsNotEqual<SOBehavior.bL>>))]
  public virtual bool? RemoveCreditHoldByPayment { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Use Currency Rate from Sales Order", FieldClass = "Multicurrency")]
  public virtual bool? UseCuryRateFromSO { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Use Currency Rate from Blanket Sales Order", FieldClass = "Multicurrency")]
  [PXUIVisible(typeof (Where<BqlOperand<SOOrderType.behavior, IBqlString>.IsEqual<SOBehavior.bL>>))]
  public virtual bool? UseCuryRateFromBL { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Require Stock Allocation")]
  public virtual bool? RequireAllocation
  {
    get => this._RequireAllocation;
    set => this._RequireAllocation = value;
  }

  [PXBool]
  [PXDBCalced(typeof (IIf<Where<SOOrderType.requireShipping, NotEqual<True>, And<SOOrderType.iNDocType, NotEqual<INTranType.noUpdate>>>, True, False>), typeof (bool))]
  [PXUIField(DisplayName = "Require Location", Enabled = false)]
  public virtual bool? RequireLocation { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Require Lot/Serial Entry")]
  public virtual bool? RequireLotSerial
  {
    get => this._RequireLotSerial;
    set => this._RequireLotSerial = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Allow Quick Process")]
  [PXUIVisible(typeof (Where<SOOrderType.behavior, In3<SOBehavior.sO, SOBehavior.tR, SOBehavior.iN, SOBehavior.cM, SOBehavior.mO>>))]
  public virtual bool? AllowQuickProcess
  {
    get => this._AllowQuickProcess;
    set => this._AllowQuickProcess = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Require Control Total")]
  public virtual bool? RequireControlTotal
  {
    get => this._RequireControlTotal;
    set => this._RequireControlTotal = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Process Shipments")]
  public virtual bool? RequireShipping
  {
    get => SOBehavior.GetRequireShipmentValue(this.Behavior, this._RequireShipping);
    set => this._RequireShipping = SOBehavior.GetRequireShipmentValue(this.Behavior, value);
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Copy Lot/Serial numbers from Shipment back to Sales Order")]
  public virtual bool? CopyLotSerialFromShipment
  {
    get => this._CopyLotSerialFromShipment;
    set => this._CopyLotSerialFromShipment = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Bill Separately")]
  public virtual bool? BillSeparately
  {
    get => this._BillSeparately;
    set => this._BillSeparately = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Ship Separately")]
  public virtual bool? ShipSeparately
  {
    get => this._ShipSeparately;
    set => this._ShipSeparately = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXUIField(DisplayName = "Use Sales Account from")]
  [SOSalesAcctSubDefault.AcctList]
  [PXDefault("I")]
  public virtual string SalesAcctDefault
  {
    get => this._SalesAcctDefault;
    set => this._SalesAcctDefault = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXUIField(DisplayName = "Use Misc. Account from")]
  [SOMiscAcctSubDefault.AcctList]
  [PXDefault("I")]
  public virtual string MiscAcctDefault
  {
    get => this._MiscAcctDefault;
    set => this._MiscAcctDefault = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXUIField(DisplayName = "Use Freight Account from")]
  [SOFreightAcctSubDefault.AcctList]
  [PXDefault("V")]
  public virtual string FreightAcctDefault
  {
    get => this._FreightAcctDefault;
    set => this._FreightAcctDefault = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXUIField(DisplayName = "Use Discount Account from")]
  [SODiscAcctSubDefault.AcctList]
  [PXDefault("L")]
  public virtual string DiscAcctDefault
  {
    get => this._DiscAcctDefault;
    set => this._DiscAcctDefault = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXUIField(DisplayName = "Use COGS Account from", Visible = false, Enabled = false)]
  [SOCOGSAcctSubDefault.AcctList]
  public virtual string COGSAcctDefault
  {
    get => this._COGSAcctDefault;
    set => this._COGSAcctDefault = value;
  }

  [PXDefault]
  [PXUIRequired(typeof (Where<SOOrderType.active, Equal<True>>))]
  [SOSalesSubAccountMask(DisplayName = "Combine Sales Sub. From")]
  public virtual string SalesSubMask
  {
    get => this._SalesSubMask;
    set => this._SalesSubMask = value;
  }

  [PXDefault]
  [PXUIRequired(typeof (Where<SOOrderType.active, Equal<True>>))]
  [SOMiscSubAccountMask(DisplayName = "Combine Misc. Sub. from")]
  public virtual string MiscSubMask
  {
    get => this._MiscSubMask;
    set => this._MiscSubMask = value;
  }

  [PXDefault]
  [PXUIRequired(typeof (Where<SOOrderType.active, Equal<True>>))]
  [SOFreightSubAccountMask(DisplayName = "Combine Freight Sub. from")]
  public virtual string FreightSubMask
  {
    get => this._FreightSubMask;
    set => this._FreightSubMask = value;
  }

  [PXDefault]
  [PXUIRequired(typeof (Where<SOOrderType.active, Equal<True>, And<FeatureInstalled<FeaturesSet.customerDiscounts>>>))]
  [SODiscSubAccountMask(DisplayName = "Combine Discount Sub. from")]
  public virtual string DiscSubMask
  {
    get => this._DiscSubMask;
    set => this._DiscSubMask = value;
  }

  [PXDefault]
  [PXUIRequired(typeof (Where<SOOrderType.active, Equal<True>, And<SOOrderType.behavior, NotEqual<SOBehavior.bL>>>))]
  [Account]
  [PXForeignReference(typeof (Field<SOOrderType.freightAcctID>.IsRelatedTo<PX.Objects.GL.Account.accountID>))]
  public virtual int? FreightAcctID
  {
    get => this._FreightAcctID;
    set => this._FreightAcctID = value;
  }

  [PXDefault]
  [PXUIRequired(typeof (Where<SOOrderType.active, Equal<True>, And<SOOrderType.behavior, NotEqual<SOBehavior.bL>>>))]
  [SubAccount(typeof (SOOrderType.freightAcctID))]
  [PXForeignReference(typeof (Field<SOOrderType.freightSubID>.IsRelatedTo<PX.Objects.GL.Sub.subID>))]
  public virtual int? FreightSubID
  {
    get => this._FreightSubID;
    set => this._FreightSubID = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Use Shipped-Not-Invoiced Account")]
  public virtual bool? UseShippedNotInvoiced
  {
    get => this._UseShippedNotInvoiced;
    set => this._UseShippedNotInvoiced = value;
  }

  [Account]
  [PXForeignReference(typeof (SOOrderType.FK.ShippedNotInvoicedAccount))]
  public virtual int? ShippedNotInvoicedAcctID
  {
    get => this._ShippedNotInvoicedAcctID;
    set => this._ShippedNotInvoicedAcctID = value;
  }

  [SubAccount(typeof (SOOrderType.shippedNotInvoicedAcctID))]
  [PXForeignReference(typeof (Field<SOOrderType.shippedNotInvoicedSubID>.IsRelatedTo<PX.Objects.GL.Sub.subID>))]
  public virtual int? ShippedNotInvoicedSubID
  {
    get => this._ShippedNotInvoicedSubID;
    set => this._ShippedNotInvoicedSubID = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Disable Automatic Discount Update")]
  public virtual bool? DisableAutomaticDiscountCalculation
  {
    get => this._DisableAutomaticDiscountCalculation;
    set => this._DisableAutomaticDiscountCalculation = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Defer Discount Recalculation")]
  public virtual bool? DeferPriceDiscountRecalculation { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Recalculate Discount On Partial Shipment")]
  public virtual bool? RecalculateDiscOnPartialShipment
  {
    get => this._RecalculateDiscOnPartialShipment;
    set => this._RecalculateDiscOnPartialShipment = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Disable Automatic Tax Calculation")]
  public virtual bool? DisableAutomaticTaxCalculation { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Ship in Full if Negative Quantity Is Allowed")]
  public virtual bool? ShipFullIfNegQtyAllowed { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Calculate Freight")]
  public virtual bool? CalculateFreight
  {
    get => this._CalculateFreight;
    set => this._CalculateFreight = value;
  }

  [SOCOGSSubAccountMask(DisplayName = "Combine COGS Sub. From", Visible = false, Enabled = false)]
  public virtual string COGSSubMask
  {
    get => this._COGSSubMask;
    set => this._COGSSubMask = value;
  }

  [Account]
  [PXDefault]
  [PXUIRequired(typeof (Where<SOOrderType.active, Equal<True>, And<SOOrderType.behavior, NotEqual<SOBehavior.bL>, And<FeatureInstalled<FeaturesSet.customerDiscounts>>>>))]
  [PXForeignReference(typeof (Field<SOOrderType.discountAcctID>.IsRelatedTo<PX.Objects.GL.Account.accountID>))]
  public virtual int? DiscountAcctID
  {
    get => this._DiscountAcctID;
    set => this._DiscountAcctID = value;
  }

  [SubAccount(typeof (SOOrderType.discountAcctID))]
  [PXDefault]
  [PXUIRequired(typeof (Where<SOOrderType.active, Equal<True>, And<SOOrderType.behavior, NotEqual<SOBehavior.bL>, And2<FeatureInstalled<FeaturesSet.customerDiscounts>, And<FeatureInstalled<FeaturesSet.subAccount>>>>>))]
  [PXForeignReference(typeof (Field<SOOrderType.discountSubID>.IsRelatedTo<PX.Objects.GL.Sub.subID>))]
  public virtual int? DiscountSubID
  {
    get => this._DiscountSubID;
    set => this._DiscountSubID = value;
  }

  [PXDBShort]
  public virtual short? OrderPriority
  {
    get => this._OrderPriority;
    set => this._OrderPriority = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Copy Notes")]
  public virtual bool? CopyNotes
  {
    get => this._CopyNotes;
    set => this._CopyNotes = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Copy Attachments")]
  public virtual bool? CopyFiles
  {
    get => this._CopyFiles;
    set => this._CopyFiles = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Copy Header Notes to Shipment")]
  public virtual bool? CopyHeaderNotesToShipment { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Copy Header Attachments to Shipment")]
  public virtual bool? CopyHeaderFilesToShipment { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Copy Header Notes to Invoice")]
  public virtual bool? CopyHeaderNotesToInvoice { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Copy Header Attachments to Invoice")]
  public virtual bool? CopyHeaderFilesToInvoice { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Copy Line Notes To Shipment")]
  public virtual bool? CopyLineNotesToShipment
  {
    get => this._CopyLineNotesToShipment;
    set => this._CopyLineNotesToShipment = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Copy Line Attachments To Shipment")]
  public virtual bool? CopyLineFilesToShipment
  {
    get => this._CopyLineFilesToShipment;
    set => this._CopyLineFilesToShipment = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Copy Line Notes To Invoice")]
  public virtual bool? CopyLineNotesToInvoice
  {
    get => this._CopyLineNotesToInvoice;
    set => this._CopyLineNotesToInvoice = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Only Non-Stock")]
  public virtual bool? CopyLineNotesToInvoiceOnlyNS
  {
    get => this._CopyLineNotesToInvoiceOnlyNS;
    set => this._CopyLineNotesToInvoiceOnlyNS = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Copy Line Attachments To Invoice")]
  public virtual bool? CopyLineFilesToInvoice
  {
    get => this._CopyLineFilesToInvoice;
    set => this._CopyLineFilesToInvoice = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Only Non-Stock")]
  public virtual bool? CopyLineFilesToInvoiceOnlyNS
  {
    get => this._CopyLineFilesToInvoiceOnlyNS;
    set => this._CopyLineFilesToInvoiceOnlyNS = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Copy Line Notes To Child Order")]
  public virtual bool? CopyLineNotesToChildOrder { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Copy Line Attachments To Child Order")]
  public virtual bool? CopyLineFilesToChildOrder { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Require Customer Order Nbr.")]
  public virtual bool? CustomerOrderIsRequired { get; set; }

  [PXDBString(1, IsFixed = true)]
  [PXUIField(DisplayName = "Customer Order Nbr. Validation")]
  [CustomerOrderValidationType.List]
  [PXDefault("N")]
  public virtual string CustomerOrderValidation { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Post Line Discounts Separately")]
  public virtual bool? PostLineDiscSeparately
  {
    get => this._PostLineDiscSeparately;
    set => this._PostLineDiscSeparately = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Use Discount Sub. from Sales Sub.", FieldClass = "SUBACCOUNT")]
  public virtual bool? UseDiscountSubFromSalesSub { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Auto Write-Off")]
  public virtual bool? AutoWriteOff { get; set; }

  [PXDBString(1, IsFixed = true)]
  [PXUIField(DisplayName = "Use Sales Account from", FieldClass = "InterBranch")]
  [SOIntercompanyAcctDefault.AcctSalesList]
  [PXDefault("I")]
  public virtual string IntercompanySalesAcctDefault { get; set; }

  [PXDBString(1, IsFixed = true)]
  [PXUIField(DisplayName = "Use COGS Account from", FieldClass = "InterBranch")]
  [SOIntercompanyAcctDefault.AcctCOGSList]
  [PXDefault("I")]
  public virtual string IntercompanyCOGSAcctDefault { get; set; }

  [PXNote(DescriptionField = typeof (SOOrderType.orderType), Selector = typeof (SOOrderType.orderType), FieldList = new Type[] {typeof (SOOrderType.orderType), typeof (SOOrderType.descr)})]
  public virtual Guid? NoteID { get; set; }

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

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  [PXDBInt]
  [PXDefault(0)]
  public virtual int? ActiveOperationsCntr { get; set; }

  [PXDBBool]
  [PXDefault(typeof (IIf<Where<SOOrderType.behavior, In3<SOBehavior.rM, SOBehavior.cM>, And<SOOrderType.aRDocType, NotEqual<PX.Objects.AR.ARDocType.noUpdate>, And<SOOrderType.defaultOperation, Equal<SOOperation.receipt>, And<SOOrderType.activeOperationsCntr, Equal<int1>, Or<SOOrderType.behavior, Equal<SOBehavior.mO>>>>>>, True, False>))]
  [PXUIField(DisplayName = "Allow Refund Before Return")]
  public virtual bool? AllowRefundBeforeReturn { get; set; }

  [PXDBBool]
  [PXFormula(typeof (IIf<Where<SOOrderType.aRDocType, In3<PX.Objects.AR.ARDocType.invoice, PX.Objects.AR.ARDocType.debitMemo>, Or<SOOrderType.behavior, In3<SOBehavior.bL, SOBehavior.mO>>>, True, False>))]
  public virtual bool? CanHavePayments { get; set; }

  [PXDBBool]
  [PXFormula(typeof (IIf<Where<SOOrderType.aRDocType, Equal<PX.Objects.AR.ARDocType.creditMemo>, And<SOOrderType.defaultOperation, Equal<SOOperation.receipt>, And<SOOrderType.activeOperationsCntr, Equal<int1>, Or<SOOrderType.behavior, Equal<SOBehavior.mO>>>>>, True, False>))]
  public virtual bool? CanHaveRefunds { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIVisible(typeof (Where<SOOrderType.canHaveRefunds, Equal<True>>))]
  [PXUIField(DisplayName = "Validate Card Refunds Against Original Transactions")]
  public virtual bool? ValidateCCRefundsOrigTransactions { get; set; }

  [PXDefault]
  [PXDBString(2, IsFixed = true, InputMask = ">aa")]
  [PXUIField(DisplayName = "Default Child Order Type")]
  [PXUIRequired(typeof (Where<SOOrderType.active, Equal<True>, And<SOOrderType.behavior, Equal<SOBehavior.bL>>>))]
  [PXSelector(typeof (Search<SOOrderType.orderType, Where<SOOrderType.behavior, Equal<SOBehavior.sO>>>))]
  [PXRestrictor(typeof (Where<SOOrderType.active, Equal<True>>), "Order Type is inactive.", new Type[] {}, ShowWarning = true)]
  public virtual string DfltChildOrderType { get; set; }

  /// <summary>
  /// Indicates whether remainder should be authorized after partial capture
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Authorize Remainder After Partial Capture")]
  public virtual bool? AuthorizeRemainderAfterPartialCapture { get; set; }

  /// <summary>
  /// The order orchestration fulfillment strategy for the order type.
  /// </summary>
  [PXDefault("NA")]
  [OrchestrationStrategies.List]
  [PXDBString(2, IsFixed = true)]
  [PXUIField(DisplayName = "Fulfillment Strategy", FieldClass = "OrderOrchestration")]
  public string OrchestrationStrategy { get; set; }

  /// <summary>
  /// A Boolean value that indicates whether the <see cref="P:PX.Objects.SO.SOOrderType.NumberOfWarehouses" /> value can be specified.
  /// </summary>
  [PXDefault(false, typeof (Switch<Case<Where<SOOrderType.orchestrationStrategy, Equal<OrchestrationStrategies.doNotOrchestrate>>, False>, SOOrderType.limitWarehouse>))]
  [PXFormula(typeof (Default<SOOrderType.orchestrationStrategy>))]
  [PXDBBool]
  [PXUIField(DisplayName = "Limit Number of Fulfillment Warehouses", FieldClass = "OrderOrchestration")]
  public bool? LimitWarehouse { get; set; }

  /// <summary>
  /// The maximum number of fulfillment warehouses possible for the order orchestration.
  /// </summary>
  [PXDefault(0, typeof (Switch<Case<Where<SOOrderType.limitWarehouse, Equal<True>>, Maximum<SOOrderType.numberOfWarehouses, int1>>, int0>))]
  [PXFormula(typeof (Default<SOOrderType.limitWarehouse>))]
  [PXUIVerify]
  [PXDBInt]
  [PXUIField(DisplayName = "Number of Fulfillment Warehouses", FieldClass = "OrderOrchestration")]
  public int? NumberOfWarehouses { get; set; }

  public class PK : PrimaryKeyOf<SOOrderType>.By<SOOrderType.orderType>
  {
    public static SOOrderType Find(PXGraph graph, string orderType, PKFindOptions options = 0)
    {
      return (SOOrderType) PrimaryKeyOf<SOOrderType>.By<SOOrderType.orderType>.FindBy(graph, (object) orderType, options);
    }
  }

  public static class FK
  {
    public class Template : 
      PrimaryKeyOf<SOOrderType>.By<SOOrderType.orderType>.ForeignKeyOf<SOOrderType>.By<SOOrderType.template>
    {
    }

    public class OrderPlanType : 
      PrimaryKeyOf<INPlanType>.By<INPlanType.planType>.ForeignKeyOf<SOOrderType>.By<SOOrderType.orderPlanType>
    {
    }

    public class ShipmentPlanType : 
      PrimaryKeyOf<INPlanType>.By<INPlanType.planType>.ForeignKeyOf<SOOrderType>.By<SOOrderType.shipmentPlanType>
    {
    }

    public class OrderNumbering : 
      PrimaryKeyOf<Numbering>.By<Numbering.numberingID>.ForeignKeyOf<SOOrderType>.By<SOOrderType.orderNumberingID>
    {
    }

    public class InvoiceNumbering : 
      PrimaryKeyOf<Numbering>.By<Numbering.numberingID>.ForeignKeyOf<SOOrderType>.By<SOOrderType.invoiceNumberingID>
    {
    }

    public class ManualInvoiceNumbering : 
      PrimaryKeyOf<Numbering>.By<Numbering.numberingID>.ForeignKeyOf<SOOrderType>.By<SOOrderType.userInvoiceNumbering>
    {
    }

    public class FreightAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<SOOrderType>.By<SOOrderType.freightAcctID>
    {
    }

    public class FreightSubaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<SOOrderType>.By<SOOrderType.freightSubID>
    {
    }

    public class DiscountAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<SOOrderType>.By<SOOrderType.discountAcctID>
    {
    }

    public class DiscountSubaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<SOOrderType>.By<SOOrderType.discountSubID>
    {
    }

    public class ShippedNotInvoicedAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<SOOrderType>.By<SOOrderType.shippedNotInvoicedAcctID>
    {
    }

    public class ShippedNotInvoicedSubaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<SOOrderType>.By<SOOrderType.shippedNotInvoicedSubID>
    {
    }
  }

  public abstract class orderType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOOrderType.orderType>
  {
    public class PreventEditIfChildOrderExists : 
      EditPreventor<TypeArrayOf<IBqlField>.FilledWith<SOOrderType.active>>.On<SOOrderTypeMaint>.IfExists<Select<SOOrderType, Where<BqlOperand<
      #nullable enable
      SOOrderType.dfltChildOrderType, IBqlString>.IsEqual<
      #nullable disable
      BqlField<
      #nullable enable
      SOOrderType.orderType, IBqlString>.FromCurrent>>>>
    {
      protected virtual 
      #nullable disable
      string CreateEditPreventingReason(
        GetEditPreventingReasonArgs arg,
        object firstPreventingEntity,
        string fieldName,
        string currentTableName,
        string foreignTableName)
      {
        if (!(arg.Row is SOOrderType row) || (arg.NewValue as bool?).GetValueOrDefault())
          return (string) null;
        SOOrderType soOrderType = firstPreventingEntity as SOOrderType;
        return PXMessages.LocalizeFormatNoPrefix("The {0} order type is the default child order type for the {1} order type.", new object[2]
        {
          (object) row.OrderType,
          (object) soOrderType?.OrderType
        });
      }
    }
  }

  public abstract class active : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOOrderType.active>
  {
  }

  public abstract class daysToKeep : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  SOOrderType.daysToKeep>
  {
  }

  public abstract class descr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOOrderType.descr>
  {
  }

  public abstract class template : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOOrderType.template>
  {
  }

  public abstract class isSystem : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOOrderType.isSystem>
  {
  }

  public abstract class behavior : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOOrderType.behavior>
  {
  }

  public abstract class defaultOperation : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOOrderType.defaultOperation>
  {
    public const int Length = 1;
  }

  public abstract class iNDocType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOOrderType.iNDocType>
  {
  }

  public abstract class aRDocType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOOrderType.aRDocType>
  {
  }

  public abstract class orderPlanType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOOrderType.orderPlanType>
  {
  }

  public abstract class shipmentPlanType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOOrderType.shipmentPlanType>
  {
  }

  public abstract class orderNumberingID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOOrderType.orderNumberingID>
  {
  }

  public abstract class invoiceNumberingID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOOrderType.invoiceNumberingID>
  {
  }

  public abstract class userInvoiceNumbering : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOOrderType.userInvoiceNumbering>
  {
  }

  public abstract class markInvoicePrinted : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOOrderType.markInvoicePrinted>
  {
  }

  public abstract class markInvoiceEmailed : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOOrderType.markInvoiceEmailed>
  {
  }

  public abstract class holdEntry : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOOrderType.holdEntry>
  {
  }

  public abstract class invoiceHoldEntry : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOOrderType.invoiceHoldEntry>
  {
  }

  public abstract class creditHoldEntry : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOOrderType.creditHoldEntry>
  {
  }

  public abstract class removeCreditHoldByPayment : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOOrderType.removeCreditHoldByPayment>
  {
  }

  public abstract class useCuryRateFromSO : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOOrderType.useCuryRateFromSO>
  {
  }

  public abstract class useCuryRateFromBL : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOOrderType.useCuryRateFromBL>
  {
  }

  public abstract class requireAllocation : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOOrderType.requireAllocation>
  {
  }

  public abstract class requireLocation : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOOrderType.requireLocation>
  {
  }

  public abstract class requireLotSerial : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOOrderType.requireLotSerial>
  {
  }

  public abstract class allowQuickProcess : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOOrderType.allowQuickProcess>
  {
  }

  public abstract class requireControlTotal : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOOrderType.requireControlTotal>
  {
  }

  public abstract class requireShipping : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOOrderType.requireShipping>
  {
  }

  public abstract class copyLotSerialFromShipment : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOOrderType.copyLotSerialFromShipment>
  {
  }

  public abstract class billSeparately : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOOrderType.billSeparately>
  {
  }

  public abstract class shipSeparately : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOOrderType.shipSeparately>
  {
  }

  public abstract class salesAcctDefault : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOOrderType.salesAcctDefault>
  {
  }

  public abstract class miscAcctDefault : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOOrderType.miscAcctDefault>
  {
  }

  public abstract class freightAcctDefault : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOOrderType.freightAcctDefault>
  {
  }

  public abstract class discAcctDefault : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOOrderType.discAcctDefault>
  {
  }

  public abstract class cOGSAcctDefault : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOOrderType.cOGSAcctDefault>
  {
  }

  public abstract class salesSubMask : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOOrderType.salesSubMask>
  {
  }

  public abstract class miscSubMask : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOOrderType.miscSubMask>
  {
  }

  public abstract class freightSubMask : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOOrderType.freightSubMask>
  {
  }

  public abstract class discSubMask : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOOrderType.discSubMask>
  {
  }

  public abstract class freightAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOOrderType.freightAcctID>
  {
  }

  public abstract class freightSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOOrderType.freightSubID>
  {
  }

  public abstract class useShippedNotInvoiced : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOOrderType.useShippedNotInvoiced>
  {
  }

  public abstract class shippedNotInvoicedAcctID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SOOrderType.shippedNotInvoicedAcctID>
  {
  }

  public abstract class shippedNotInvoicedSubID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SOOrderType.shippedNotInvoicedSubID>
  {
  }

  public abstract class disableAutomaticDiscountCalculation : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOOrderType.disableAutomaticDiscountCalculation>
  {
  }

  public abstract class deferPriceDiscountRecalculation : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOOrderType.deferPriceDiscountRecalculation>
  {
  }

  public abstract class recalculateDiscOnPartialShipment : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOOrderType.recalculateDiscOnPartialShipment>
  {
  }

  public abstract class disableAutomaticTaxCalculation : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOOrderType.disableAutomaticTaxCalculation>
  {
  }

  public abstract class shipFullIfNegQtyAllowed : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOOrderType.shipFullIfNegQtyAllowed>
  {
  }

  public abstract class calculateFreight : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOOrderType.calculateFreight>
  {
  }

  public abstract class cOGSSubMask : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOOrderType.cOGSSubMask>
  {
  }

  public abstract class discountAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOOrderType.discountAcctID>
  {
  }

  public abstract class discountSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOOrderType.discountSubID>
  {
  }

  public abstract class orderPriority : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  SOOrderType.orderPriority>
  {
  }

  public abstract class copyNotes : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOOrderType.copyNotes>
  {
  }

  public abstract class copyFiles : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOOrderType.copyFiles>
  {
  }

  public abstract class copyHeaderNotesToShipment : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOOrderType.copyHeaderNotesToShipment>
  {
  }

  public abstract class copyHeaderFilesToShipment : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOOrderType.copyHeaderFilesToShipment>
  {
  }

  public abstract class copyHeaderNotesToInvoice : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOOrderType.copyHeaderNotesToInvoice>
  {
  }

  public abstract class copyHeaderFilesToInvoice : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOOrderType.copyHeaderFilesToInvoice>
  {
  }

  public abstract class copyLineNotesToShipment : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOOrderType.copyLineNotesToShipment>
  {
  }

  public abstract class copyLineFilesToShipment : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOOrderType.copyLineFilesToShipment>
  {
  }

  public abstract class copyLineNotesToInvoice : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOOrderType.copyLineNotesToInvoice>
  {
  }

  public abstract class copyLineNotesToInvoiceOnlyNS : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOOrderType.copyLineNotesToInvoiceOnlyNS>
  {
  }

  public abstract class copyLineFilesToInvoice : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOOrderType.copyLineFilesToInvoice>
  {
  }

  public abstract class copyLineFilesToInvoiceOnlyNS : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOOrderType.copyLineFilesToInvoiceOnlyNS>
  {
  }

  public abstract class copyLineNotesToChildOrder : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOOrderType.copyLineNotesToChildOrder>
  {
  }

  public abstract class copyLineFilesToChildOrder : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOOrderType.copyLineFilesToChildOrder>
  {
  }

  public abstract class customerOrderIsRequired : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOOrderType.customerOrderIsRequired>
  {
  }

  public abstract class customerOrderValidation : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOOrderType.customerOrderValidation>
  {
  }

  public abstract class postLineDiscSeparately : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOOrderType.postLineDiscSeparately>
  {
  }

  public abstract class useDiscountSubFromSalesSub : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOOrderType.useDiscountSubFromSalesSub>
  {
  }

  public abstract class autoWriteOff : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOOrderType.autoWriteOff>
  {
  }

  public abstract class intercompanySalesAcctDefault : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOOrderType.intercompanySalesAcctDefault>
  {
  }

  public abstract class intercompanyCOGSAcctDefault : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOOrderType.intercompanyCOGSAcctDefault>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SOOrderType.noteID>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SOOrderType.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOOrderType.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SOOrderType.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    SOOrderType.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOOrderType.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SOOrderType.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  SOOrderType.Tstamp>
  {
  }

  public abstract class activeOperationsCntr : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SOOrderType.activeOperationsCntr>
  {
  }

  public abstract class allowRefundBeforeReturn : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOOrderType.allowRefundBeforeReturn>
  {
  }

  public abstract class canHavePayments : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOOrderType.canHavePayments>
  {
  }

  public abstract class canHaveRefunds : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOOrderType.canHaveRefunds>
  {
  }

  public abstract class validateCCRefundsOrigTransactions : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOOrderType.validateCCRefundsOrigTransactions>
  {
  }

  public abstract class dfltChildOrderType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOOrderType.dfltChildOrderType>
  {
  }

  public abstract class authorizeRemainderAfterPartialCapture : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOOrderType.authorizeRemainderAfterPartialCapture>
  {
  }

  public abstract class orchestrationStrategy : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOOrderType.orchestrationStrategy>
  {
  }

  public abstract class limitWarehouse : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOOrderType.limitWarehouse>
  {
  }

  public abstract class numberOfWarehouses : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SOOrderType.numberOfWarehouses>
  {
  }
}
