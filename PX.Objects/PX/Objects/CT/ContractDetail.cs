// Decompiled with JetBrains decompiler
// Type: PX.Objects.CT.ContractDetail
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CM;
using PX.Objects.CS;
using PX.Objects.IN;
using System;

#nullable enable
namespace PX.Objects.CT;

[PXCacheName("Contract Detail")]
[PXProjection(typeof (Select2<ContractDetail, InnerJoin<Contract, On<ContractDetail.contractID, Equal<Contract.contractID>, And<ContractDetail.revID, Equal<Contract.revID>>>, LeftJoin<ContractDetailExt, On<ContractDetailExt.contractID, Equal<ContractDetail.contractID>, And<ContractDetailExt.lineNbr, Equal<ContractDetail.lineNbr>, And<ContractDetailExt.revID, Equal<Contract.lastActiveRevID>>>>>>>), new Type[] {typeof (ContractDetail)}, Persistent = true)]
[Serializable]
public class ContractDetail : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected bool? _Selected = new bool?(false);
  protected int? _ContractDetailID;
  protected int? _ContractID;
  protected int? _LineNbr;
  protected int? _InventoryID;
  protected int? _ContractItemID;
  protected 
  #nullable disable
  string _Description;
  protected string _ResetUsage;
  protected Decimal? _Included;
  protected Decimal? _Used;
  protected Decimal? _UsedTotal;
  protected string _UOM;
  protected Decimal? _Qty;
  protected Decimal? _Change;
  protected DateTime? _LastBilledDate;
  protected Decimal? _LastBilledQty;
  protected bool? _WarningAmountForDeposit;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  [PXBool]
  [PXDefault(false)]
  [PXUIField]
  public bool? Selected
  {
    get => this._Selected;
    set => this._Selected = value;
  }

  [PXDBIdentity]
  public virtual int? ContractDetailID
  {
    get => this._ContractDetailID;
    set => this._ContractDetailID = value;
  }

  [PXDBInt(IsKey = true)]
  [PXDBDefault(typeof (Contract.contractID))]
  [PXParent(typeof (Select<Contract, Where<Contract.contractID, Equal<Current<ContractDetail.contractID>>>>))]
  [PXParent(typeof (Select<ContractBillingSchedule, Where<ContractBillingSchedule.contractID, Equal<Current<ContractDetail.contractID>>>>))]
  public virtual int? ContractID
  {
    get => this._ContractID;
    set => this._ContractID = value;
  }

  [PXDBInt(IsKey = true)]
  [ContractLineNbr(typeof (Contract.lineCtr))]
  [PXUIField(DisplayName = "Line Nbr.", Visible = false)]
  public virtual int? LineNbr
  {
    get => this._LineNbr;
    set => this._LineNbr = value;
  }

  [PXDBInt(MinValue = 1)]
  [PXDefault(typeof (Contract.revID))]
  public virtual int? RevID { get; set; }

  [PXDefault]
  [PXDBInt]
  [PXUIField(DisplayName = "Non-Stock Item")]
  [PXDimensionSelector("INVENTORY", typeof (Search<InventoryItem.inventoryID, Where<InventoryItem.stkItem, Equal<False>, And<InventoryItem.itemStatus, NotEqual<InventoryItemStatus.unknown>, And<InventoryItem.isTemplate, Equal<False>, And<Match<Current<AccessInfo.userName>>>>>>>), typeof (InventoryItem.inventoryCD))]
  [PXForeignReference(typeof (Field<ContractDetail.inventoryID>.IsRelatedTo<InventoryItem.inventoryID>))]
  public virtual int? InventoryID
  {
    get => this._InventoryID;
    set => this._InventoryID = value;
  }

  [PXDBInt]
  [PXDefault]
  [PXDimensionSelector("CONTRACTITEM", typeof (Search<ContractItem.contractItemID>), typeof (ContractItem.contractItemCD), new Type[] {typeof (ContractItem.contractItemCD), typeof (ContractItem.descr)}, DescriptionField = typeof (ContractItem.descr))]
  [PXUIField(DisplayName = "Item Code")]
  public virtual int? ContractItemID
  {
    get => this._ContractItemID;
    set => this._ContractItemID = value;
  }

  [PXDBLocalizableString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField]
  [PXFormula(typeof (Selector<ContractDetail.contractItemID, ContractItem.descr>))]
  public virtual string Description
  {
    get => this._Description;
    set => this._Description = value;
  }

  [PXDefault("N")]
  [PXUIField(DisplayName = "Reset Usage", Required = true)]
  [PXDBString(1, IsFixed = true)]
  [ResetUsageOption.List]
  public virtual string ResetUsage
  {
    get => this._ResetUsage;
    set => this._ResetUsage = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? Included
  {
    get => this._Included;
    set => this._Included = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? Used
  {
    get => this._Used;
    set => this._Used = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? UsedTotal
  {
    get => this._UsedTotal;
    set => this._UsedTotal = value;
  }

  [PXDefault(typeof (Search<InventoryItem.salesUnit, Where<InventoryItem.inventoryID, Equal<Current<ContractDetail.inventoryID>>>>))]
  [INUnit(typeof (ContractDetail.inventoryID))]
  public virtual string UOM
  {
    get => this._UOM;
    set => this._UOM = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Quantity")]
  public virtual Decimal? Qty
  {
    get => this._Qty;
    set => this._Qty = value;
  }

  [PXDBDecimal(BqlField = typeof (ContractDetailExt.qty))]
  public virtual Decimal? LastQty { get; set; }

  [PXDecimal]
  [PXFormula(typeof (Sub<ContractDetail.qty, Switch<Case<Where<ContractDetail.lastQty, IsNotNull>, ContractDetail.lastQty>, int0>>))]
  [PXUIField(DisplayName = "Difference", Enabled = false)]
  public virtual Decimal? Change
  {
    get => this._Change;
    set => this._Change = value;
  }

  [PXBool]
  [PXFormula(typeof (Selector<ContractDetail.contractItemID, ContractItem.deposit>))]
  public virtual bool? Deposit { get; set; }

  [PXDBCury(typeof (ContractDetail.curyID))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Deposit Amount", Enabled = false)]
  public virtual Decimal? DepositAmt { get; set; }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Deposit Used", Enabled = false)]
  public virtual Decimal? DepositUsed { get; set; }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Deposit Used Total", Enabled = false)]
  public virtual Decimal? DepositUsedTotal { get; set; }

  [PXUIField(DisplayName = "Included", Enabled = false)]
  [PXDecimal]
  [PXFormula(typeof (Switch<Case<Where<ContractDetail.deposit, Equal<True>>, ContractDetail.depositAmt>, ContractDetail.qty>))]
  public virtual Decimal? RecurringIncluded { get; set; }

  [PXUIField(DisplayName = "Unbilled", Enabled = false)]
  [PXDecimal]
  [PXFormula(typeof (Switch<Case<Where<ContractDetail.deposit, Equal<True>>, ContractDetail.depositUsed>, ContractDetail.used>))]
  public virtual Decimal? RecurringUsed { get; set; }

  [PXUIField(DisplayName = "Used Total", Enabled = false)]
  [PXDecimal]
  [PXFormula(typeof (Switch<Case<Where<ContractDetail.deposit, Equal<True>>, ContractDetail.depositUsedTotal>, ContractDetail.usedTotal>))]
  public virtual Decimal? RecurringUsedTotal { get; set; }

  [PXDBDate]
  [PXUIField(DisplayName = "Last Billed Date", Enabled = false)]
  public virtual DateTime? LastBilledDate
  {
    get => this._LastBilledDate;
    set => this._LastBilledDate = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Last Billed Qty.", Enabled = false)]
  public virtual Decimal? LastBilledQty
  {
    get => this._LastBilledQty;
    set => this._LastBilledQty = value;
  }

  [PXDBString(10, IsUnicode = true)]
  public virtual string BaseDiscountID { get; set; }

  [PXDBString(10, IsUnicode = true)]
  public virtual string BaseDiscountSeq { get; set; }

  [PXDBString(10, IsUnicode = true)]
  public virtual string RecurringDiscountID { get; set; }

  [PXDBString(10, IsUnicode = true)]
  public virtual string RecurringDiscountSeq { get; set; }

  [PXDBString(10, IsUnicode = true)]
  public virtual string RenewalDiscountID { get; set; }

  [PXDBString(10, IsUnicode = true)]
  public virtual string RenewalDiscountSeq { get; set; }

  [PXDBDecimal(6)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Setup Discount,%", Enabled = false)]
  public Decimal? BaseDiscountPct { get; set; }

  [PXDBDecimal(6)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Recurring Discount,%", Enabled = false)]
  public Decimal? RecurringDiscountPct { get; set; }

  [PXDBDecimal(6)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Renewal Discount,%", Enabled = false)]
  public Decimal? RenewalDiscountPct { get; set; }

  [PXDBDecimal(BqlField = typeof (ContractDetailExt.baseDiscountPct))]
  public virtual Decimal? LastBaseDiscountPct { get; set; }

  [PXDBDecimal(BqlField = typeof (ContractDetailExt.recurringDiscountPct))]
  public virtual Decimal? LastRecurringDiscountPct { get; set; }

  [PXDBDecimal(BqlField = typeof (ContractDetailExt.renewalDiscountPct))]
  public virtual Decimal? LastRenewalDiscountPct { get; set; }

  [PXDecimal(6)]
  [PXUIField(DisplayName = "Setup Discount", Enabled = false)]
  public Decimal? BaseDiscountAmt { get; set; }

  [PXDecimal(6)]
  [PXUIField(DisplayName = "Recurring Discount", Enabled = false)]
  public Decimal? RecurringDiscountAmt { get; set; }

  [PXDecimal(6)]
  [PXUIField(DisplayName = "Renewal Discount", Enabled = false)]
  public Decimal? RenewalDiscountAmt { get; set; }

  [PXUIField(DisplayName = "Price/Percent")]
  [PXDBDecimal(6)]
  [PXFormula(typeof (Switch<Case<Where<Selector<ContractDetail.contractItemID, ContractItem.baseItemID>, IsNull>, decimal0>, Selector<ContractDetail.contractItemID, ContractItem.basePrice>>))]
  public Decimal? BasePrice { get; set; }

  [PriceOption.List]
  [PXUIField(DisplayName = "Setup Pricing")]
  [PXDBString(1, IsFixed = true)]
  [PXFormula(typeof (Switch<Case<Where<Selector<ContractDetail.contractItemID, ContractItem.baseItemID>, IsNull>, PriceOption.manually>, Selector<ContractDetail.contractItemID, ContractItem.basePriceOption>>))]
  public string BasePriceOption { get; set; }

  [PXUIField(DisplayName = "Price/Percent")]
  [PXDBDecimal(6)]
  [PXFormula(typeof (Switch<Case<Where<Selector<ContractDetail.contractItemID, ContractItem.renewalItemID>, IsNull>, decimal0>, Selector<ContractDetail.contractItemID, ContractItem.renewalPrice>>))]
  public Decimal? RenewalPrice { get; set; }

  [RenewalOption.List]
  [PXUIField(DisplayName = "Renewal Pricing")]
  [PXDBString(1, IsFixed = true)]
  [PXFormula(typeof (Switch<Case<Where<Selector<ContractDetail.contractItemID, ContractItem.renewalItemID>, IsNull>, PriceOption.manually>, Selector<ContractDetail.contractItemID, ContractItem.renewalPriceOption>>))]
  public string RenewalPriceOption { get; set; }

  [PXUIField(DisplayName = "Price/Percent")]
  [PXDBDecimal(6)]
  [PXFormula(typeof (Selector<ContractDetail.contractItemID, ContractItem.fixedRecurringPrice>))]
  public Decimal? FixedRecurringPrice { get; set; }

  [RecurringOption.List]
  [PXUIField(DisplayName = "Fixed Recurring")]
  [PXDBString(1, IsFixed = true)]
  [PXFormula(typeof (Selector<ContractDetail.contractItemID, ContractItem.fixedRecurringPriceOption>))]
  public string FixedRecurringPriceOption { get; set; }

  [PXUIField(DisplayName = "Price/Percent")]
  [PXDBDecimal(6)]
  [PXFormula(typeof (Selector<ContractDetail.contractItemID, ContractItem.usagePrice>))]
  public Decimal? UsagePrice { get; set; }

  [PriceOption.List]
  [PXUIField(DisplayName = "Usage Price")]
  [PXDBString(1, IsFixed = true)]
  [PXFormula(typeof (Selector<ContractDetail.contractItemID, ContractItem.usagePriceOption>))]
  public string UsagePriceOption { get; set; }

  [PXDecimal(6)]
  [PXFormula(typeof (GetItemPriceValue<ContractDetail.contractID, ContractDetail.contractItemID, ContractDetailType.ContractDetailSetup, ContractDetail.basePriceOption, Selector<ContractDetail.contractItemID, ContractItem.baseItemID>, ContractDetail.basePrice, ContractDetail.basePriceVal, ContractDetail.qty, IsNull<Parent<Contract.startDate>, Current<AccessInfo.businessDate>>>))]
  [PXUIField(DisplayName = "Setup Price")]
  public Decimal? BasePriceVal { get; set; }

  [PXBool]
  [PXFormula(typeof (Switch<Case<Where<Selector<ContractDetail.contractItemID, ContractItem.baseItemID>, IsNull, Or<ContractDetail.basePriceOption, NotEqual<PriceOption.manually>>>, False>, True>))]
  public bool? BasePriceEditable { get; set; }

  [PXBool]
  [PXDBCalced(typeof (Switch<Case<Where<ContractDetail.basePriceOption, Equal<PriceOption.manually>, And<ContractDetail.basePrice, Equal<decimal0>>>, False>, True>), typeof (bool))]
  public virtual bool? IsBaseValid { get; set; }

  [PXDecimal(6)]
  [PXFormula(typeof (GetItemPriceValue<ContractDetail.contractID, ContractDetail.contractItemID, ContractDetailType.ContractDetailRenewal, ContractDetail.renewalPriceOption, Selector<ContractDetail.contractItemID, ContractItem.renewalItemID>, ContractDetail.renewalPrice, ContractDetail.basePriceVal, ContractDetail.qty, Switch<Case<Where<Parent<Contract.status>, Equal<Contract.status.draft>, Or<Parent<Contract.status>, Equal<Contract.status.pendingActivation>>>, IsNull<Parent<Contract.activationDate>, Parent<Contract.startDate>>, Case<Where<Parent<Contract.status>, Equal<Contract.status.active>, Or<Parent<Contract.status>, Equal<Contract.status.inUpgrade>, Or<Parent<Contract.status>, Equal<Contract.status.expired>>>>, IsNull<Add<Parent<Contract.expireDate>, int1>, Current<AccessInfo.businessDate>>, Case<Where<Parent<Contract.status>, Equal<Contract.status.canceled>>, IsNull<Parent<Contract.terminationDate>, Current<AccessInfo.businessDate>>>>>, Current<AccessInfo.businessDate>>>))]
  [PXUIField(DisplayName = "Renewal Price")]
  public Decimal? RenewalPriceVal { get; set; }

  [PXBool]
  [PXFormula(typeof (Switch<Case<Where<Selector<ContractDetail.contractItemID, ContractItem.renewalItemID>, IsNull, Or<ContractDetail.renewalPriceOption, NotEqual<PriceOption.manually>>>, False>, True>))]
  public bool? RenewalPriceEditable { get; set; }

  [PXBool]
  [PXDBCalced(typeof (Switch<Case<Where<ContractDetail.renewalPriceOption, Equal<PriceOption.manually>, And<ContractDetail.renewalPrice, Equal<decimal0>>>, False>, True>), typeof (bool))]
  public virtual bool? IsRenewalValid { get; set; }

  [PXDecimal(6)]
  [PXFormula(typeof (GetItemPriceValue<ContractDetail.contractID, ContractDetail.contractItemID, ContractDetailType.ContractDetail, ContractDetail.fixedRecurringPriceOption, Selector<ContractDetail.contractItemID, ContractItem.recurringItemID>, ContractDetail.fixedRecurringPrice, ContractDetail.basePriceVal, ContractDetail.qty, Switch<Case<Where<Parent<Contract.status>, Equal<Contract.status.draft>, Or<Parent<Contract.status>, Equal<Contract.status.pendingActivation>>>, IsNull<Parent<Contract.activationDate>, Parent<Contract.startDate>>, Case<Where<Parent<Contract.status>, Equal<Contract.status.active>, Or<Parent<Contract.status>, Equal<Contract.status.inUpgrade>>>, IsNull<Parent<ContractBillingSchedule.nextDate>, Current<AccessInfo.businessDate>>, Case<Where<Parent<Contract.status>, Equal<Contract.status.expired>>, IsNull<Parent<ContractBillingSchedule.nextDate>, Parent<Contract.expireDate>>, Case<Where<Parent<Contract.status>, Equal<Contract.status.canceled>>, IsNull<Parent<Contract.terminationDate>, Current<AccessInfo.businessDate>>>>>>, Current<AccessInfo.businessDate>>>))]
  [PXUIField(DisplayName = "Recurring Price")]
  public Decimal? FixedRecurringPriceVal { get; set; }

  [PXBool]
  [PXFormula(typeof (Switch<Case<Where<Selector<ContractDetail.contractItemID, ContractItem.recurringItemID>, IsNull, Or<ContractDetail.fixedRecurringPriceOption, NotEqual<PriceOption.manually>>>, False>, True>))]
  public bool? FixedRecurringPriceEditable { get; set; }

  [PXBool]
  [PXDBCalced(typeof (Switch<Case<Where<ContractDetail.fixedRecurringPriceOption, Equal<PriceOption.manually>, And<ContractDetail.fixedRecurringPrice, Equal<decimal0>>>, False>, True>), typeof (bool))]
  public virtual bool? IsFixedRecurringValid { get; set; }

  [PXDecimal(6)]
  [PXFormula(typeof (GetItemPriceValue<ContractDetail.contractID, ContractDetail.contractItemID, ContractDetailType.ContractDetailUsagePrice, ContractDetail.usagePriceOption, Selector<ContractDetail.contractItemID, ContractItem.recurringItemID>, ContractDetail.usagePrice, ContractDetail.basePriceVal, ContractDetail.qty, IsNull<Parent<Contract.activationDate>, Current<AccessInfo.businessDate>>>))]
  [PXUIField(DisplayName = "Extra Usage Price")]
  public Decimal? UsagePriceVal { get; set; }

  [PXBool]
  [PXFormula(typeof (Switch<Case<Where<Selector<ContractDetail.contractItemID, ContractItem.recurringItemID>, IsNull, Or<ContractDetail.usagePriceOption, NotEqual<PriceOption.manually>>>, False>, True>))]
  public bool? UsagePriceEditable { get; set; }

  [PXBool]
  [PXDBCalced(typeof (Switch<Case<Where<ContractDetail.usagePriceOption, Equal<PriceOption.manually>, And<ContractDetail.usagePrice, Equal<decimal0>>>, False>, True>), typeof (bool))]
  public virtual bool? IsUsageValid { get; set; }

  [PXNote]
  public virtual Guid? NoteID { get; set; }

  [PXBool]
  public virtual bool? WarningAmountForDeposit
  {
    get => this._WarningAmountForDeposit;
    set => this._WarningAmountForDeposit = value;
  }

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
  public virtual DateTime? LastModifiedDateTime
  {
    get => this._LastModifiedDateTime;
    set => this._LastModifiedDateTime = value;
  }

  public class PK : 
    PrimaryKeyOf<ContractDetail>.By<ContractDetail.contractID, ContractDetail.contractDetailID, ContractDetail.revID>
  {
    public static ContractDetail Find(
      PXGraph graph,
      int? contractID,
      int? contractDetailID,
      int? revID,
      PKFindOptions options = 0)
    {
      return (ContractDetail) PrimaryKeyOf<ContractDetail>.By<ContractDetail.contractID, ContractDetail.contractDetailID, ContractDetail.revID>.FindBy(graph, (object) contractID, (object) contractDetailID, (object) revID, options);
    }
  }

  public static class FK
  {
    public class Contract : 
      PrimaryKeyOf<Contract>.By<Contract.contractID>.ForeignKeyOf<ContractDetail>.By<ContractDetail.contractID>
    {
    }

    public class NonStockItem : 
      PrimaryKeyOf<InventoryItem>.By<InventoryItem.inventoryID>.ForeignKeyOf<ContractDetail>.By<ContractDetail.inventoryID>
    {
    }

    public class ContractItem : 
      PrimaryKeyOf<ContractItem>.By<ContractItem.contractItemID>.ForeignKeyOf<ContractDetail>.By<ContractDetail.contractItemID>
    {
    }

    public class Currency : 
      PrimaryKeyOf<PX.Objects.CM.Currency>.By<PX.Objects.CM.Currency.curyID>.ForeignKeyOf<ContractDetail>.By<ContractDetail.curyID>
    {
    }
  }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ContractDetail.selected>
  {
  }

  public abstract class contractDetailID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ContractDetail.contractDetailID>
  {
  }

  public abstract class contractID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ContractDetail.contractID>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ContractDetail.lineNbr>
  {
  }

  public abstract class revID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ContractDetail.revID>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ContractDetail.inventoryID>
  {
  }

  public abstract class contractItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ContractDetail.contractItemID>
  {
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ContractDetail.curyID>
  {
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ContractDetail.description>
  {
  }

  public abstract class resetUsage : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ContractDetail.resetUsage>
  {
  }

  public abstract class included : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ContractDetail.included>
  {
  }

  public abstract class used : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ContractDetail.used>
  {
  }

  public abstract class usedTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ContractDetail.usedTotal>
  {
  }

  public abstract class uOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ContractDetail.uOM>
  {
  }

  public abstract class qty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ContractDetail.qty>
  {
  }

  public abstract class lastQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ContractDetail.lastQty>
  {
  }

  public abstract class change : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ContractDetail.change>
  {
  }

  public abstract class deposit : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ContractDetail.deposit>
  {
  }

  public abstract class depositAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ContractDetail.depositAmt>
  {
  }

  public abstract class depositUsed : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ContractDetail.depositUsed>
  {
  }

  public abstract class depositUsedTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ContractDetail.depositUsedTotal>
  {
  }

  public abstract class recurringIncluded : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ContractDetail.recurringIncluded>
  {
  }

  public abstract class recurringUsed : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ContractDetail.recurringUsed>
  {
  }

  public abstract class recurringUsedTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ContractDetail.recurringUsedTotal>
  {
  }

  public abstract class lastBilledDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ContractDetail.lastBilledDate>
  {
  }

  public abstract class lastBilledQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ContractDetail.lastBilledQty>
  {
  }

  public abstract class baseDiscountID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ContractDetail.baseDiscountID>
  {
  }

  public abstract class baseDiscountSeq : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ContractDetail.baseDiscountSeq>
  {
  }

  public abstract class recurringDiscountID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ContractDetail.recurringDiscountID>
  {
  }

  public abstract class recurringDiscountSeq : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ContractDetail.recurringDiscountSeq>
  {
  }

  public abstract class renewalDiscountID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ContractDetail.renewalDiscountID>
  {
  }

  public abstract class renewalDiscountSeq : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ContractDetail.renewalDiscountSeq>
  {
  }

  public abstract class baseDiscountPct : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ContractDetail.baseDiscountPct>
  {
  }

  public abstract class recurringDiscountPct : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ContractDetail.recurringDiscountPct>
  {
  }

  public abstract class renewalDiscountPct : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ContractDetail.renewalDiscountPct>
  {
  }

  public abstract class lastBaseDiscountPct : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ContractDetail.lastBaseDiscountPct>
  {
  }

  public abstract class lastRecurringDiscountPct : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ContractDetail.lastRecurringDiscountPct>
  {
  }

  public abstract class lastRenewalDiscountPct : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ContractDetail.lastRenewalDiscountPct>
  {
  }

  public abstract class baseDiscountAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ContractDetail.baseDiscountAmt>
  {
  }

  public abstract class recurringDiscountAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ContractDetail.recurringDiscountAmt>
  {
  }

  public abstract class renewalDiscountAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ContractDetail.renewalDiscountAmt>
  {
  }

  public abstract class basePrice : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ContractDetail.basePrice>
  {
  }

  public abstract class basePriceOption : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ContractDetail.basePriceOption>
  {
  }

  public abstract class renewalPrice : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ContractDetail.renewalPrice>
  {
  }

  public abstract class renewalPriceOption : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ContractDetail.renewalPriceOption>
  {
  }

  public abstract class fixedRecurringPrice : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ContractDetail.fixedRecurringPrice>
  {
  }

  public abstract class fixedRecurringPriceOption : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ContractDetail.fixedRecurringPriceOption>
  {
  }

  public abstract class usagePrice : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ContractDetail.usagePrice>
  {
  }

  public abstract class usagePriceOption : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ContractDetail.usagePriceOption>
  {
  }

  public abstract class basePriceVal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ContractDetail.basePriceVal>
  {
  }

  public abstract class basePriceEditable : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ContractDetail.basePriceEditable>
  {
  }

  public abstract class isBaseValid : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ContractDetail.isBaseValid>
  {
  }

  public abstract class renewalPriceVal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ContractDetail.renewalPriceVal>
  {
  }

  public abstract class renewalPriceEditable : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ContractDetail.renewalPriceEditable>
  {
  }

  public abstract class isRenewalValid : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ContractDetail.isRenewalValid>
  {
  }

  public abstract class fixedRecurringPriceVal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ContractDetail.fixedRecurringPriceVal>
  {
  }

  public abstract class fixedRecurringPriceEditable : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ContractDetail.fixedRecurringPriceEditable>
  {
  }

  public abstract class isFixedRecurringValid : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ContractDetail.isFixedRecurringValid>
  {
  }

  public abstract class usagePriceVal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ContractDetail.usagePriceVal>
  {
  }

  public abstract class usagePriceEditable : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ContractDetail.usagePriceEditable>
  {
  }

  public abstract class isUsageValid : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ContractDetail.isUsageValid>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  ContractDetail.noteID>
  {
  }

  public abstract class warningAmountForDeposit : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ContractDetail.warningAmountForDeposit>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  ContractDetail.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  ContractDetail.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ContractDetail.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ContractDetail.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    ContractDetail.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ContractDetail.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ContractDetail.lastModifiedDateTime>
  {
  }
}
