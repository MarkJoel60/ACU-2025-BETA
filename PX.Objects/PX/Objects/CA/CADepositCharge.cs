// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CADepositCharge
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CA.Descriptor;
using PX.Objects.CM.Extensions;
using PX.Objects.CS;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.CA;

/// <summary>
/// The main properties of deposit charges.
/// Deposit charges are created on the Bank Deposits (CA305000) form
/// (which corresponds to the <see cref="T:PX.Objects.CA.CADepositEntry" /> graph)
/// based on the settings of the CA Deposit and Cash Account clearing accounts.
/// </summary>
[PXCacheName("CA Deposit Charge")]
[Serializable]
public class CADepositCharge : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  /// <summary>
  /// The type of the parent document.
  /// This field is a part of the compound key of the document.
  /// </summary>
  /// <value>
  /// Corresponds to the value of the <see cref="P:PX.Objects.CA.CADeposit.TranType" /> field.
  /// </value>
  [PXDBString(3, IsFixed = true, IsKey = true)]
  [CATranType.DepositList]
  [PXDefault(typeof (CADeposit.tranType))]
  [PXUIField]
  public virtual 
  #nullable disable
  string TranType { get; set; }

  /// <summary>
  /// The reference number of the document.
  /// This field is a part of the compound key of the document.
  /// </summary>
  /// <value>
  /// Corresponds to the value of the <see cref="P:PX.Objects.CA.CADeposit.RefNbr" /> field.
  /// </value>
  [PXDBString(15, IsKey = true, InputMask = "", IsUnicode = true)]
  [PXDBDefault(typeof (CADeposit.refNbr))]
  [PXUIField]
  [PXParent(typeof (Select<CADeposit, Where<CADeposit.tranType, Equal<Current<CADepositCharge.tranType>>, And<CADeposit.refNbr, Equal<Current<CADepositCharge.refNbr>>>>>))]
  public virtual string RefNbr { get; set; }

  [PXDBInt(IsKey = true)]
  [PXLineNbr(typeof (CADeposit.lineCntrCharge), DecrementOnDelete = false)]
  public virtual int? LineNbr { get; set; }

  /// <summary>
  /// The entry type of the bank charges that apply to the deposit.
  /// The entry types can be configured on the Entry Types (CA203000) form.
  /// </summary>
  /// <value>
  /// Corresponds to the value of the <see cref="P:PX.Objects.CA.CAEntryType.EntryTypeId" /> field.
  /// </value>
  [PXDBDepositCharge(10, IsUnicode = true)]
  [PXDefault]
  [PXUIField]
  [PXSelector(typeof (Search2<CAEntryType.entryTypeId, InnerJoin<CashAccountETDetail, On<CashAccountETDetail.entryTypeID, Equal<CAEntryType.entryTypeId>, And<CashAccountETDetail.cashAccountID, Equal<Current<CADeposit.cashAccountID>>>>>, Where<CAEntryType.module, Equal<BatchModule.moduleCA>, And<CAEntryType.useToReclassifyPayments, Equal<False>>>>))]
  public virtual string EntryTypeID { get; set; }

  /// <summary>
  /// The cash account from which the charge is taken when the deposit is made.
  /// </summary>
  /// <value>
  /// Corresponds to the value of the <see cref="P:PX.Objects.CA.CADeposit.CashAccountID" /> field.
  /// </value>
  [PXDefault(typeof (CADeposit.cashAccountID))]
  [CashAccount(null, typeof (Search<CashAccount.cashAccountID>), DisplayName = "Clearing Account")]
  public virtual int? DepositAcctID { get; set; }

  /// <summary>
  /// The payment method of the deposited payment to which the charge rate should be applied.
  /// This field is a part of the primary key.
  /// The field is useful if a bank establishes different charge rates for different payment methods.
  /// In the case, when payments with different payment methods are mixed in one deposit, you can group multiple records with different rates by the payment method.
  /// </summary>
  /// <value>
  /// Corresponds to the value of the <see cref="P:PX.Objects.CA.PaymentMethod.PaymentMethodID" /> field.
  /// If the value of the field is an empty string (which is the default value), the charge rate is applied to deposited payments regardless of their payment method.
  /// </value>
  [PXDBDepositCharge(10, IsUnicode = true)]
  [PXDefault("")]
  [PXUIField(DisplayName = "Payment Method")]
  [PXSelectorAllowEmpty(typeof (FbqlSelect<SelectFromBase<PaymentMethod, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<PaymentMethod.isActive, IBqlBool>.IsEqual<True>>, PaymentMethod>.SearchFor<PaymentMethod.paymentMethodID>))]
  public virtual string PaymentMethodID { get; set; }

  /// <summary>
  /// The expense account to which the charges are recorded.
  /// </summary>
  /// <value>
  /// Corresponds to the value of the <see cref="P:PX.Objects.CA.CAEntryType.AccountID" /> field.
  /// </value>
  [PXDefault(typeof (Search<CAEntryType.accountID, Where<CAEntryType.entryTypeId, Equal<Current<CADepositCharge.entryTypeID>>>>))]
  [Account]
  [PXFormula(typeof (Default<CADepositCharge.entryTypeID>))]
  public virtual int? AccountID { get; set; }

  /// <summary>The subaccount to be used with the expense account.</summary>
  /// <value>
  /// Corresponds to the value of the <see cref="P:PX.Objects.CA.CAEntryType.SubID" /> field.
  /// </value>
  [PXDefault(typeof (Search<CAEntryType.subID, Where<CAEntryType.entryTypeId, Equal<Current<CADepositCharge.entryTypeID>>>>))]
  [SubAccount(typeof (CADepositCharge.accountID))]
  [PXFormula(typeof (Default<CADepositCharge.entryTypeID>))]
  public virtual int? SubID { get; set; }

  /// <summary>The balance type of the deposit.</summary>
  /// <value>
  /// The field can have one of the following values:
  /// <c>"D"</c>: Receipt,
  /// <c>"C"</c>: Disbursement.
  /// </value>
  [PXDefault("C")]
  [PXDBString(1, IsFixed = true)]
  [CADrCr.List]
  [PXUIField(DisplayName = "Disb. / Receipt")]
  public virtual string DrCr { get; set; }

  /// <summary>The rate of the bank charges.</summary>
  [PXDBDecimal(6)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? ChargeRate { get; set; }

  /// <summary>
  /// The identifier of the exchange rate record for the deposit.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.CM.Extensions.CurrencyInfo.CuryInfoID" /> field.
  /// </value>
  [PXDBLong]
  [CurrencyInfo(typeof (CADeposit.curyInfoID))]
  public virtual long? CuryInfoID { get; set; }

  /// <summary>
  /// The amount to be used as a base for the charges in the selected currency.
  /// </summary>
  [PXDBCurrency(typeof (CADepositCharge.curyInfoID), typeof (CADepositCharge.chargeableAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? CuryChargeableAmt { get; set; }

  /// <summary>
  /// The amount to be used as a base for the charges in the base currency.
  /// </summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? ChargeableAmt { get; set; }

  /// <summary>The amount of the charges in the selected currency.</summary>
  [PXDBCurrency(typeof (CADepositCharge.curyInfoID), typeof (CADepositCharge.chargeAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXFormula(typeof (Mult<CADepositCharge.curyChargeableAmt, Div<CADepositCharge.chargeRate, decimal100>>), typeof (SumCalc<CADeposit.curyChargeTotal>))]
  [PXUIField]
  public virtual Decimal? CuryChargeAmt { get; set; }

  /// <summary>The amount of the charges in the base currency.</summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Charge Amount")]
  public virtual Decimal? ChargeAmt { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  public class PK : 
    PrimaryKeyOf<CADepositCharge>.By<CADepositCharge.tranType, CADepositCharge.refNbr, CADepositCharge.lineNbr>
  {
    public static CADepositCharge Find(
      PXGraph graph,
      string tranType,
      string refNbr,
      int? lineNbr,
      PKFindOptions options = 0)
    {
      return (CADepositCharge) PrimaryKeyOf<CADepositCharge>.By<CADepositCharge.tranType, CADepositCharge.refNbr, CADepositCharge.lineNbr>.FindBy(graph, (object) tranType, (object) refNbr, (object) lineNbr, options);
    }
  }

  public static class FK
  {
    public class ChargeEntryType : 
      PrimaryKeyOf<CAEntryType>.By<CAEntryType.entryTypeId>.ForeignKeyOf<CADepositCharge>.By<CADepositCharge.entryTypeID>
    {
    }

    public class ClearingAccount : 
      PrimaryKeyOf<CashAccount>.By<CashAccount.cashAccountID>.ForeignKeyOf<CADepositCharge>.By<CADepositCharge.depositAcctID>
    {
    }

    public class DepositPaymentMethod : 
      PrimaryKeyOf<PaymentMethod>.By<PaymentMethod.paymentMethodID>.ForeignKeyOf<CADepositCharge>.By<CADepositCharge.paymentMethodID>
    {
    }

    public class ExpenseAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<CADepositCharge>.By<CADepositCharge.accountID>
    {
    }

    public class ExpenseSubaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<CADepositCharge>.By<CADepositCharge.subID>
    {
    }

    public class CurrencyInfo : 
      PrimaryKeyOf<PX.Objects.CM.CurrencyInfo>.By<PX.Objects.CM.CurrencyInfo.curyInfoID>.ForeignKeyOf<CADepositCharge>.By<CADepositCharge.curyInfoID>
    {
    }
  }

  public abstract class tranType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CADepositCharge.tranType>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CADepositCharge.refNbr>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CADepositCharge.lineNbr>
  {
  }

  public abstract class entryTypeID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CADepositCharge.entryTypeID>
  {
  }

  public abstract class depositAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CADepositCharge.depositAcctID>
  {
  }

  public abstract class paymentMethodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CADepositCharge.paymentMethodID>
  {
  }

  public abstract class accountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CADepositCharge.accountID>
  {
  }

  public abstract class subID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CADepositCharge.subID>
  {
  }

  public abstract class drCr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CADepositCharge.drCr>
  {
  }

  public abstract class chargeRate : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CADepositCharge.chargeRate>
  {
  }

  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  CADepositCharge.curyInfoID>
  {
  }

  public abstract class curyChargeableAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CADepositCharge.curyChargeableAmt>
  {
  }

  public abstract class chargeableAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CADepositCharge.chargeableAmt>
  {
  }

  public abstract class curyChargeAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CADepositCharge.curyChargeAmt>
  {
  }

  public abstract class chargeAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CADepositCharge.chargeAmt>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CADepositCharge.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CADepositCharge.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CADepositCharge.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    CADepositCharge.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CADepositCharge.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CADepositCharge.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  CADepositCharge.Tstamp>
  {
  }
}
