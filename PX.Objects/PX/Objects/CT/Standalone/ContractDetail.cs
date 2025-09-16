// Decompiled with JetBrains decompiler
// Type: PX.Objects.CT.Standalone.ContractDetail
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using PX.Objects.IN;
using System;

#nullable enable
namespace PX.Objects.CT.Standalone;

public class ContractDetail : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBIdentity(IsKey = true)]
  [PXUIField(DisplayName = "Contract Detail ID")]
  public virtual int? ContractDetailID { get; set; }

  [PXDBInt(IsKey = true)]
  [PXUIField(DisplayName = "Contract ID")]
  public virtual int? ContractID { get; set; }

  [PXDBInt]
  public virtual int? LineNbr { get; set; }

  [PXDBInt(MinValue = 1, IsKey = true)]
  [PXUIField(DisplayName = "Revision Number")]
  public virtual int? RevID { get; set; }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Quantity")]
  public virtual Decimal? Qty { get; set; }

  [PXUIField(DisplayName = "Price/Percent")]
  [PXDBDecimal(6)]
  public Decimal? FixedRecurringPrice { get; set; }

  [PXDBInt]
  [PXDefault]
  [PXDimensionSelector("CONTRACTITEM", typeof (Search<ContractItem.contractItemID>), typeof (ContractItem.contractItemCD), new Type[] {typeof (ContractItem.contractItemCD), typeof (ContractItem.descr)}, DescriptionField = typeof (ContractItem.descr))]
  [PXUIField(DisplayName = "Item Code")]
  public virtual int? ContractItemID { get; set; }

  [RecurringOption.List]
  [PXUIField(DisplayName = "Fixed Recurring")]
  [PXDBString(1, IsFixed = true)]
  [PXFormula(typeof (Selector<ContractDetail.contractItemID, ContractItem.fixedRecurringPriceOption>))]
  public 
  #nullable disable
  string FixedRecurringPriceOption { [PXDependsOnFields(new Type[] {typeof (ContractDetail.contractItemID)})] get; set; }

  [PXUIField(DisplayName = "Price/Percent")]
  [PXDBDecimal(6)]
  [PXFormula(typeof (Switch<Case<Where<Selector<ContractDetail.contractItemID, ContractItem.baseItemID>, IsNull>, decimal0>, Selector<ContractDetail.contractItemID, ContractItem.basePrice>>))]
  public Decimal? BasePrice { [PXDependsOnFields(new Type[] {typeof (ContractDetail.contractItemID)})] get; set; }

  [PriceOption.List]
  [PXUIField(DisplayName = "Setup Pricing")]
  [PXDBString(1, IsFixed = true)]
  [PXFormula(typeof (Switch<Case<Where<Selector<ContractDetail.contractItemID, ContractItem.baseItemID>, IsNull>, PriceOption.manually>, Selector<ContractDetail.contractItemID, ContractItem.basePriceOption>>))]
  public string BasePriceOption { [PXDependsOnFields(new Type[] {typeof (ContractDetail.contractItemID)})] get; set; }

  [PXDecimal(6)]
  [PXFormula(typeof (GetItemPriceValue<ContractDetail.contractID, ContractDetail.contractItemID, ContractDetailType.ContractDetailSetup, ContractDetail.basePriceOption, Selector<ContractDetail.contractItemID, ContractItem.baseItemID>, ContractDetail.basePrice, ContractDetail.basePriceVal, ContractDetail.qty, ContractDetail.historyStartDate>))]
  [PXUIField(DisplayName = "Setup Price")]
  public Decimal? BasePriceVal { [PXDependsOnFields(new Type[] {typeof (ContractDetail.contractID), typeof (ContractDetail.contractItemID), typeof (ContractDetail.basePriceOption), typeof (ContractDetail.basePrice), typeof (ContractDetail.basePriceVal), typeof (ContractDetail.qty), typeof (ContractDetail.historyStartDate)})] get; set; }

  [PXString(1, IsFixed = true)]
  [PXDBScalar(typeof (Search<ContractRenewalHistory.status, Where<ContractRenewalHistory.contractID, Equal<ContractDetail.contractID>, And<ContractRenewalHistory.revID, Equal<ContractDetail.revID>>>>))]
  [Contract.status.List]
  [PXUIField]
  public virtual string HistoryStatus { [PXDependsOnFields(new Type[] {typeof (ContractDetail.contractID), typeof (ContractDetail.revID)})] get; set; }

  [PXDate]
  [PXDBScalar(typeof (Search<ContractRenewalHistory.nextDate, Where<ContractRenewalHistory.contractID, Equal<ContractDetail.contractID>, And<ContractRenewalHistory.revID, Equal<ContractDetail.revID>>>>))]
  [PXUIField]
  public virtual DateTime? HistoryNextDate { [PXDependsOnFields(new Type[] {typeof (ContractDetail.contractID), typeof (ContractDetail.revID)})] get; set; }

  [PXDate]
  [PXDBScalar(typeof (Search<ContractRenewalHistory.activationDate, Where<ContractRenewalHistory.contractID, Equal<ContractDetail.contractID>, And<ContractRenewalHistory.revID, Equal<ContractDetail.revID>>>>))]
  public virtual DateTime? HistoryActivationDate { [PXDependsOnFields(new Type[] {typeof (ContractDetail.contractID), typeof (ContractDetail.revID)})] get; set; }

  [PXDate]
  [PXDBScalar(typeof (Search<ContractRenewalHistory.startDate, Where<ContractRenewalHistory.contractID, Equal<ContractDetail.contractID>, And<ContractRenewalHistory.revID, Equal<ContractDetail.revID>>>>))]
  public virtual DateTime? HistoryStartDate { [PXDependsOnFields(new Type[] {typeof (ContractDetail.contractID), typeof (ContractDetail.revID)})] get; set; }

  [PXDate]
  [PXDBScalar(typeof (Search<ContractRenewalHistory.expireDate, Where<ContractRenewalHistory.contractID, Equal<ContractDetail.contractID>, And<ContractRenewalHistory.revID, Equal<ContractDetail.revID>>>>))]
  public virtual DateTime? HistoryExpireDate { [PXDependsOnFields(new Type[] {typeof (ContractDetail.contractID), typeof (ContractDetail.revID)})] get; set; }

  [PXDate]
  [PXDBScalar(typeof (Search<ContractRenewalHistory.terminationDate, Where<ContractRenewalHistory.contractID, Equal<ContractDetail.contractID>, And<ContractRenewalHistory.revID, Equal<ContractDetail.revID>>>>))]
  public virtual DateTime? HistoryTerminationDate { [PXDependsOnFields(new Type[] {typeof (ContractDetail.contractID), typeof (ContractDetail.revID)})] get; set; }

  [PXDate]
  [PXDBScalar(typeof (Search<ContractBillingSchedule.nextDate, Where<ContractBillingSchedule.contractID, Equal<ContractDetail.contractID>>>))]
  [PXUIField]
  public virtual DateTime? BillingScheduleNextDate { [PXDependsOnFields(new Type[] {typeof (ContractDetail.contractID), typeof (ContractDetail.revID)})] get; set; }

  [PXDecimal(6)]
  [PXFormula(typeof (GetItemPriceValue<ContractDetail.contractID, ContractDetail.contractItemID, ContractDetailType.ContractDetail, ContractDetail.fixedRecurringPriceOption, Selector<ContractDetail.contractItemID, ContractItem.recurringItemID>, ContractDetail.fixedRecurringPrice, ContractDetail.basePriceVal, ContractDetail.qty, Switch<Case<Where<ContractDetail.historyStatus, Equal<Contract.status.draft>, Or<ContractDetail.historyStatus, Equal<Contract.status.pendingActivation>>>, IsNull<ContractDetail.historyActivationDate, ContractDetail.historyStartDate>, Case<Where<ContractDetail.historyStatus, Equal<Contract.status.active>, Or<ContractDetail.historyStatus, Equal<Contract.status.inUpgrade>>>, IsNull<ContractDetail.historyNextDate, Current<AccessInfo.businessDate>>, Case<Where<ContractDetail.historyStatus, Equal<Contract.status.expired>>, IsNull<ContractDetail.billingScheduleNextDate, ContractDetail.historyExpireDate>, Case<Where<ContractDetail.historyStatus, Equal<Contract.status.canceled>>, IsNull<ContractDetail.historyTerminationDate, Current<AccessInfo.businessDate>>>>>>, Current<AccessInfo.businessDate>>>))]
  [PXUIField(DisplayName = "Recurring Price")]
  public Decimal? FixedRecurringPriceVal { [PXDependsOnFields(new Type[] {typeof (ContractDetail.contractID), typeof (ContractDetail.contractItemID), typeof (ContractDetail.fixedRecurringPriceOption), typeof (ContractDetail.fixedRecurringPrice), typeof (ContractDetail.basePriceVal), typeof (ContractDetail.qty), typeof (ContractDetail.historyStatus), typeof (ContractDetail.historyNextDate), typeof (ContractDetail.historyActivationDate), typeof (ContractDetail.historyStartDate), typeof (ContractDetail.historyExpireDate), typeof (ContractDetail.historyTerminationDate), typeof (ContractDetail.billingScheduleNextDate)})] get; set; }

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

  public abstract class qty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ContractDetail.qty>
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

  public abstract class contractItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ContractDetail.contractItemID>
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

  public abstract class basePriceVal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ContractDetail.basePriceVal>
  {
  }

  public abstract class historyStatus : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ContractDetail.historyStatus>
  {
  }

  public abstract class historyNextDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ContractDetail.historyNextDate>
  {
  }

  public abstract class historyActivationDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ContractDetail.historyActivationDate>
  {
  }

  public abstract class historyStartDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ContractDetail.historyStartDate>
  {
  }

  public abstract class historyExpireDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ContractDetail.historyExpireDate>
  {
  }

  public abstract class historyTerminationDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ContractDetail.historyTerminationDate>
  {
  }

  public abstract class billingScheduleNextDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ContractDetail.billingScheduleNextDate>
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
}
