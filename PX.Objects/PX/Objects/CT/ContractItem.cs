// Decompiled with JetBrains decompiler
// Type: PX.Objects.CT.ContractItem
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AR;
using PX.Objects.CS;
using PX.Objects.IN;
using System;

#nullable enable
namespace PX.Objects.CT;

[PXPrimaryGraph(typeof (ContractItemMaint))]
[PXCacheName("Contract Item")]
[Serializable]
public class ContractItem : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _Descr;
  protected string _CuryID;
  protected string _RecurringTypeForDeposits;
  protected string _UOMForDeposits;
  protected Decimal? _BasePriceVal;
  protected Decimal? _RenewalPriceVal;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;
  protected byte[] _tstamp;

  [PXDBIdentity]
  [PXUIField]
  public int? ContractItemID { get; set; }

  [ContractItem]
  public string ContractItemCD { get; set; }

  [PXDBLocalizableString(IsUnicode = true)]
  [PXDefault]
  [PXUIField]
  public virtual string Descr
  {
    get => this._Descr;
    set => this._Descr = value;
  }

  [PXDBQuantity(MinValue = 0.0)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXFormula(typeof (Validate<ContractItem.maxQty, ContractItem.minQty>))]
  [PXUIField(DisplayName = "Default Quantity")]
  public Decimal? DefaultQty { get; set; }

  [PXDBQuantity(MinValue = 0.0)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Minimum Allowed Quantity")]
  public Decimal? MinQty { get; set; }

  [PXDBQuantity(MinValue = 0.0)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Maximum Allowed Quantity")]
  public Decimal? MaxQty { get; set; }

  [PXDefault]
  [PXDBString(5, IsUnicode = true)]
  [PXSelector(typeof (Search<PX.Objects.CM.Extensions.Currency.curyID>))]
  [PXUIField(DisplayName = "Currency ID")]
  public virtual string CuryID
  {
    get => this._CuryID;
    set => this._CuryID = value;
  }

  [ContractInventoryItem(DisplayName = "Setup Item")]
  [PXForeignReference(typeof (Field<ContractItem.baseItemID>.IsRelatedTo<PX.Objects.IN.InventoryItem.inventoryID>))]
  public int? BaseItemID { get; set; }

  [PXDBString(1, IsFixed = true)]
  [PriceOption.List]
  [PXUIField(DisplayName = "Setup Pricing")]
  [PXDefault("I")]
  public string BasePriceOption { get; set; }

  [PXDBDecimal(6)]
  [PXUIField(DisplayName = "Item Price/Percent")]
  [PXDefault(TypeCode.Decimal, "100.0")]
  public Decimal? BasePrice { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Prorate Setup")]
  public bool? ProrateSetup { get; set; }

  [PXDBDecimal(2)]
  [PXUIField(DisplayName = "Retain Rate")]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXFormula(typeof (Switch<Case<Where<ContractItem.deposit, NotEqual<True>>, decimal0>, ContractItem.retainRate>))]
  [PXUIEnabled(typeof (Where<ContractItem.deposit, Equal<True>>))]
  public Decimal? RetainRate { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIEnabled(typeof (Where<ContractItem.baseItemID, IsNotNull, Or<ContractItem.renewalItemID, IsNotNull>>))]
  [PXUIField(DisplayName = "Refundable")]
  public bool? Refundable { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Deposit")]
  public bool? Deposit { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Collect Renewal Fee on Activation")]
  public bool? CollectRenewFeeOnActivation { get; set; }

  [ContractInventoryItem(DisplayName = "Renewal Item")]
  [PXForeignReference(typeof (Field<ContractItem.renewalItemID>.IsRelatedTo<PX.Objects.IN.InventoryItem.inventoryID>))]
  public int? RenewalItemID { get; set; }

  [PXDBString(1, IsFixed = true)]
  [RenewalOption.List]
  [PXDefault("B")]
  [PXUIField(DisplayName = "Renewal Pricing")]
  public string RenewalPriceOption { get; set; }

  [PXDBDecimal(6)]
  [PXUIField(DisplayName = "Item Price/Percent")]
  [PXDefault(TypeCode.Decimal, "100.0")]
  public Decimal? RenewalPrice { get; set; }

  [PXDBString(1, IsFixed = true)]
  [RecurringOption.List]
  [PXDefault("N")]
  [PXUIField(DisplayName = "Billing Type")]
  public string RecurringType { get; set; }

  [PXString(1, IsFixed = true)]
  [RecurringOption.ListForDeposits]
  [PXUIField(DisplayName = "Billing Type")]
  public string RecurringTypeForDeposits
  {
    get => this._RecurringTypeForDeposits;
    set => this._RecurringTypeForDeposits = value;
  }

  [PXUIField(DisplayName = "UOM")]
  [PXString(10, IsFixed = true)]
  public virtual string UOMForDeposits
  {
    get => this._UOMForDeposits;
    set => this._UOMForDeposits = value;
  }

  [PXDefault]
  [ContractInventoryItem(DisplayName = "Recurring Item")]
  [PXForeignReference(typeof (Field<ContractItem.recurringItemID>.IsRelatedTo<PX.Objects.IN.InventoryItem.inventoryID>))]
  public int? RecurringItemID { get; set; }

  [PXDBBool]
  [PXUIField(DisplayName = "Reset Usage on Billing")]
  [PXDefault(false)]
  public bool? ResetUsageOnBilling { get; set; }

  [PXDBString(1, IsFixed = true)]
  [FixedRecurringOption.List]
  [PXDefault("P")]
  [PXUIField(DisplayName = "Recurring Pricing")]
  public string FixedRecurringPriceOption { get; set; }

  [PXDBDecimal(6)]
  [PXUIField(DisplayName = "Item Price/Percent")]
  [PXDefault(TypeCode.Decimal, "100.0")]
  public Decimal? FixedRecurringPrice { get; set; }

  [PXDBString(1, IsFixed = true)]
  [UsageOption.List]
  [PXDefault("I")]
  [PXUIField(DisplayName = "Extra Usage Pricing")]
  public string UsagePriceOption { get; set; }

  [PXDBDecimal(6)]
  [PXUIField(DisplayName = "Item Price/Percent")]
  [PXDefault(TypeCode.Decimal, "100.0")]
  public Decimal? UsagePrice { get; set; }

  [PXDBDate]
  [PXUIField(DisplayName = "Discontinue After")]
  public DateTime? DiscontinueAfter { get; set; }

  [PXDBInt]
  [PXDimensionSelector("CONTRACTITEM", typeof (Search<ContractItem.contractItemID, Where<ContractItem.contractItemID, NotEqual<Current<ContractItem.contractItemID>>>>), typeof (ContractItem.contractItemCD))]
  [PXRestrictor(typeof (Where<ContractItem.deposit, Equal<Current<ContractItem.deposit>>>), "Contract Item does not match with Current Item on Deposit", new Type[] {})]
  [PXUIField(DisplayName = "Replacement Item")]
  public int? ReplacementItemID { get; set; }

  [PXDBInt]
  [PXDimensionSelector("CONTRACTITEM", typeof (Search<ContractItem.contractItemID, Where<ContractItem.contractItemID, NotEqual<Current<ContractItem.contractItemID>>>>), typeof (ContractItem.contractItemCD))]
  [PXRestrictor(typeof (Where<ContractItem.deposit, Equal<True>>), "Contract Item is not Deposit", new Type[] {})]
  [PXUIField(DisplayName = "Deposit Item")]
  public int? DepositItemID { get; set; }

  [PXInt]
  [PXSelector(typeof (Search<InventoryItemCurySettings.inventoryID, Where<InventoryItemCurySettings.inventoryID, Equal<BqlField<ContractItem.baseItemID, IBqlInt>.FromCurrent>, And<InventoryItemCurySettings.curyID, Equal<Current<AccessInfo.baseCuryID>>>>>), ValidateValue = false)]
  [PXFormula(typeof (ContractItem.baseItemID))]
  public int? BaseItemCurySettingsID { get; set; }

  [PXDecimal(6)]
  [PXUIField(DisplayName = "Setup Price", Enabled = false)]
  [PXFormula(typeof (IsNull<Switch<Case<Where<ContractItem.baseItemID, IsNotNull>, Switch<Case<Where<ContractItem.basePriceOption, Equal<PriceOption.itemPrice>>, IsNull<NullIf<Selector<ContractItem.baseItemID, ARSalesPrice.salesPrice>, decimal0>, Switch<Case<Where<ContractItem.curyID, Equal<CurrentValue<AccessInfo.baseCuryID>>>, NullIf<Selector<ContractItem.baseItemCurySettingsID, InventoryItemCurySettings.basePrice>, decimal0>>, Null>>, Case<Where<ContractItem.basePriceOption, Equal<PriceOption.itemPercent>>, Div<Mult<ContractItem.basePrice, IsNull<NullIf<Selector<ContractItem.baseItemID, ARSalesPrice.salesPrice>, decimal0>, Switch<Case<Where<ContractItem.curyID, Equal<CurrentValue<AccessInfo.baseCuryID>>>, NullIf<Selector<ContractItem.baseItemCurySettingsID, InventoryItemCurySettings.basePrice>, decimal0>>, Null>>>, decimal100>>>, ContractItem.basePrice>>, decimal0>, decimal0>))]
  public Decimal? BasePriceVal
  {
    get => this._BasePriceVal;
    set => this._BasePriceVal = value;
  }

  [PXInt]
  [PXSelector(typeof (Search<InventoryItemCurySettings.inventoryID, Where<InventoryItemCurySettings.inventoryID, Equal<BqlField<ContractItem.renewalItemID, IBqlInt>.FromCurrent>, And<InventoryItemCurySettings.curyID, Equal<Current<AccessInfo.baseCuryID>>>>>), ValidateValue = false)]
  [PXFormula(typeof (ContractItem.renewalItemID))]
  public int? RenewalItemCurySettingsID { get; set; }

  [PXDecimal(6)]
  [PXUIField(DisplayName = "Renewal Price", Enabled = false)]
  [PXFormula(typeof (IsNull<Switch<Case<Where<ContractItem.renewalItemID, IsNotNull>, Switch<Case<Where<ContractItem.renewalPriceOption, Equal<PriceOption.basePercent>>, Div<Mult<ContractItem.renewalPrice, ContractItem.basePriceVal>, decimal100>, Case<Where<ContractItem.renewalPriceOption, Equal<PriceOption.itemPrice>>, IsNull<NullIf<Selector<ContractItem.renewalItemID, ARSalesPrice.salesPrice>, decimal0>, Switch<Case<Where<ContractItem.curyID, Equal<CurrentValue<AccessInfo.baseCuryID>>>, NullIf<Selector<ContractItem.renewalItemCurySettingsID, InventoryItemCurySettings.basePrice>, decimal0>>, Null>>, Case<Where<ContractItem.renewalPriceOption, Equal<PriceOption.itemPercent>>, Div<Mult<ContractItem.renewalPrice, IsNull<NullIf<Selector<ContractItem.renewalItemID, ARSalesPrice.salesPrice>, decimal0>, Switch<Case<Where<ContractItem.curyID, Equal<CurrentValue<AccessInfo.baseCuryID>>>, NullIf<Selector<ContractItem.renewalItemCurySettingsID, InventoryItemCurySettings.basePrice>, decimal0>>, Null>>>, decimal100>>>>, ContractItem.renewalPrice>>, decimal0>, decimal0>))]
  public Decimal? RenewalPriceVal
  {
    get => this._RenewalPriceVal;
    set => this._RenewalPriceVal = value;
  }

  [PXInt]
  [PXSelector(typeof (Search<InventoryItemCurySettings.inventoryID, Where<InventoryItemCurySettings.inventoryID, Equal<BqlField<ContractItem.recurringItemID, IBqlInt>.FromCurrent>, And<InventoryItemCurySettings.curyID, Equal<Current<AccessInfo.baseCuryID>>>>>), ValidateValue = false)]
  [PXFormula(typeof (ContractItem.recurringItemID))]
  public int? RecurringItemCurySettingsID { get; set; }

  [PXDecimal(6)]
  [PXUIField(DisplayName = "Recurring Price", Enabled = false)]
  [PXFormula(typeof (IsNull<Switch<Case<Where<ContractItem.recurringItemID, IsNotNull>, Switch<Case<Where<ContractItem.fixedRecurringPriceOption, Equal<PriceOption.basePercent>>, Div<Mult<ContractItem.fixedRecurringPrice, ContractItem.basePriceVal>, decimal100>, Case<Where<ContractItem.fixedRecurringPriceOption, Equal<PriceOption.itemPrice>>, IsNull<NullIf<Selector<ContractItem.recurringItemID, ARSalesPrice.salesPrice>, decimal0>, Switch<Case<Where<ContractItem.curyID, Equal<CurrentValue<AccessInfo.baseCuryID>>>, NullIf<Selector<ContractItem.recurringItemCurySettingsID, InventoryItemCurySettings.basePrice>, decimal0>>, Null>>, Case<Where<ContractItem.fixedRecurringPriceOption, Equal<PriceOption.itemPercent>>, Div<Mult<ContractItem.fixedRecurringPrice, IsNull<NullIf<Selector<ContractItem.recurringItemID, ARSalesPrice.salesPrice>, decimal0>, Switch<Case<Where<ContractItem.curyID, Equal<CurrentValue<AccessInfo.baseCuryID>>>, NullIf<Selector<ContractItem.recurringItemCurySettingsID, InventoryItemCurySettings.basePrice>, decimal0>>, Null>>>, decimal100>>>>, ContractItem.fixedRecurringPrice>>, decimal0>, decimal0>))]
  public Decimal? FixedRecurringPriceVal { get; set; }

  [PXDecimal(6)]
  [PXUIField(DisplayName = "Extra Usage Price", Enabled = false)]
  [PXFormula(typeof (IsNull<Switch<Case<Where<ContractItem.recurringItemID, IsNotNull>, Switch<Case<Where<ContractItem.usagePriceOption, Equal<PriceOption.basePercent>>, Div<Mult<ContractItem.usagePrice, ContractItem.basePriceVal>, decimal100>, Case<Where<ContractItem.usagePriceOption, Equal<PriceOption.itemPrice>>, IsNull<NullIf<Selector<ContractItem.recurringItemID, ARSalesPrice.salesPrice>, decimal0>, Switch<Case<Where<ContractItem.curyID, Equal<CurrentValue<AccessInfo.baseCuryID>>>, NullIf<Selector<ContractItem.recurringItemCurySettingsID, InventoryItemCurySettings.basePrice>, decimal0>>, Null>>, Case<Where<ContractItem.usagePriceOption, Equal<PriceOption.itemPercent>>, Div<Mult<ContractItem.usagePrice, IsNull<NullIf<Selector<ContractItem.recurringItemID, ARSalesPrice.salesPrice>, decimal0>, Switch<Case<Where<ContractItem.curyID, Equal<CurrentValue<AccessInfo.baseCuryID>>>, NullIf<Selector<ContractItem.recurringItemCurySettingsID, InventoryItemCurySettings.basePrice>, decimal0>>, Null>>>, decimal100>>>>, ContractItem.usagePrice>>, decimal0>, decimal0>))]
  public Decimal? UsagePriceVal { get; set; }

  [PXBool]
  [PXDBCalced(typeof (Switch<Case<Where<ContractItem.baseItemID, IsNotNull, And<ContractItem.basePriceOption, Equal<PriceOption.manually>, And<ContractItem.basePrice, Equal<decimal0>>>>, False>, True>), typeof (bool))]
  public virtual bool? IsBaseValid { get; set; }

  [PXBool]
  [PXDBCalced(typeof (Switch<Case<Where<ContractItem.renewalItemID, IsNotNull, And<ContractItem.renewalPriceOption, Equal<PriceOption.manually>, And<ContractItem.renewalPrice, Equal<decimal0>>>>, False>, True>), typeof (bool))]
  public virtual bool? IsRenewalValid { get; set; }

  [PXBool]
  [PXDBCalced(typeof (Switch<Case<Where<ContractItem.recurringItemID, IsNotNull, And<ContractItem.fixedRecurringPriceOption, Equal<PriceOption.manually>, And<ContractItem.fixedRecurringPrice, Equal<decimal0>>>>, False>, True>), typeof (bool))]
  public virtual bool? IsFixedRecurringValid { get; set; }

  [PXBool]
  [PXDBCalced(typeof (Switch<Case<Where<ContractItem.recurringItemID, IsNotNull, And<ContractItem.usagePriceOption, Equal<PriceOption.manually>, And<ContractItem.usagePrice, Equal<decimal0>>>>, False>, True>), typeof (bool))]
  public virtual bool? IsUsageValid { get; set; }

  [PXNote(DescriptionField = typeof (ContractItem.contractItemCD))]
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

  public class PK : PrimaryKeyOf<ContractItem>.By<ContractItem.contractItemID>
  {
    public static ContractItem Find(PXGraph graph, int? contractItemID, PKFindOptions options = 0)
    {
      return (ContractItem) PrimaryKeyOf<ContractItem>.By<ContractItem.contractItemID>.FindBy(graph, (object) contractItemID, options);
    }
  }

  public class UK : PrimaryKeyOf<ContractItem>.By<ContractItem.contractItemCD>
  {
    public static ContractItem Find(PXGraph graph, string contractItemCD, PKFindOptions options = 0)
    {
      return (ContractItem) PrimaryKeyOf<ContractItem>.By<ContractItem.contractItemCD>.FindBy(graph, (object) contractItemCD, options);
    }
  }

  public static class FK
  {
    public class Currency : 
      PrimaryKeyOf<PX.Objects.CM.Currency>.By<PX.Objects.CM.Currency.curyID>.ForeignKeyOf<ContractItem>.By<ContractItem.curyID>
    {
    }

    public class SetupItem : 
      PrimaryKeyOf<PX.Objects.IN.InventoryItem>.By<PX.Objects.IN.InventoryItem.inventoryID>.ForeignKeyOf<ContractItem>.By<ContractItem.baseItemID>
    {
    }

    public class RenewalItem : 
      PrimaryKeyOf<PX.Objects.IN.InventoryItem>.By<PX.Objects.IN.InventoryItem.inventoryID>.ForeignKeyOf<ContractItem>.By<ContractItem.renewalItemID>
    {
    }
  }

  public abstract class contractItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ContractItem.contractItemID>
  {
  }

  public abstract class contractItemCD : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ContractItem.contractItemCD>
  {
  }

  public abstract class descr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ContractItem.descr>
  {
  }

  public abstract class defaultQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ContractItem.defaultQty>
  {
  }

  public abstract class minQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ContractItem.minQty>
  {
  }

  public abstract class maxQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ContractItem.maxQty>
  {
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ContractItem.curyID>
  {
  }

  public abstract class baseItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ContractItem.baseItemID>
  {
  }

  public abstract class basePriceOption : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ContractItem.basePriceOption>
  {
  }

  public abstract class basePrice : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ContractItem.basePrice>
  {
  }

  public abstract class prorateSetup : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ContractItem.prorateSetup>
  {
  }

  public abstract class retainRate : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ContractItem.retainRate>
  {
  }

  public abstract class refundable : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ContractItem.refundable>
  {
  }

  public abstract class deposit : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ContractItem.deposit>
  {
  }

  public abstract class collectRenewFeeOnActivation : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ContractItem.collectRenewFeeOnActivation>
  {
  }

  public abstract class renewalItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ContractItem.renewalItemID>
  {
  }

  public abstract class renewalPriceOption : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ContractItem.renewalPriceOption>
  {
  }

  public abstract class renewalPrice : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ContractItem.renewalPrice>
  {
  }

  public abstract class recurringType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ContractItem.recurringType>
  {
  }

  public abstract class recurringTypeForDeposits : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ContractItem.recurringTypeForDeposits>
  {
  }

  public abstract class uOMForDeposits : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ContractItem.uOMForDeposits>
  {
  }

  public abstract class recurringItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ContractItem.recurringItemID>
  {
  }

  public abstract class resetUsageOnBilling : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ContractItem.resetUsageOnBilling>
  {
  }

  public abstract class fixedRecurringPriceOption : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ContractItem.fixedRecurringPriceOption>
  {
  }

  public abstract class fixedRecurringPrice : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ContractItem.fixedRecurringPrice>
  {
  }

  public abstract class usagePriceOption : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ContractItem.usagePriceOption>
  {
  }

  public abstract class usagePrice : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ContractItem.usagePrice>
  {
  }

  public abstract class discontinueAfter : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ContractItem.discontinueAfter>
  {
  }

  public abstract class replacementItemID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ContractItem.replacementItemID>
  {
  }

  public abstract class depositItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ContractItem.depositItemID>
  {
  }

  public abstract class baseItemCurySettingsID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ContractItem.baseItemCurySettingsID>
  {
  }

  public abstract class basePriceVal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ContractItem.basePriceVal>
  {
  }

  public abstract class renewalItemCurySettingsID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ContractItem.renewalItemCurySettingsID>
  {
  }

  public abstract class renewalPriceVal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ContractItem.renewalPriceVal>
  {
  }

  public abstract class recurringItemCurySettingsID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ContractItem.recurringItemCurySettingsID>
  {
  }

  public abstract class fixedRecurringPriceVal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ContractItem.fixedRecurringPriceVal>
  {
  }

  public abstract class usagePriceVal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ContractItem.usagePriceVal>
  {
  }

  public abstract class isBaseValid : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ContractItem.isBaseValid>
  {
  }

  public abstract class isRenewalValid : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ContractItem.isRenewalValid>
  {
  }

  public abstract class isFixedRecurringValid : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ContractItem.isFixedRecurringValid>
  {
  }

  public abstract class isUsageValid : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ContractItem.isUsageValid>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  ContractItem.noteID>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  ContractItem.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ContractItem.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ContractItem.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    ContractItem.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ContractItem.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ContractItem.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  ContractItem.Tstamp>
  {
  }
}
