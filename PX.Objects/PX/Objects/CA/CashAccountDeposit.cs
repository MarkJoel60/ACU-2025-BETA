// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CashAccountDeposit
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CS;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.CA;

/// <summary>
/// The main properties of clearing accounts.
/// The records of this type define the settings for deposit to the cash account from the clearing accounts.
/// The presence of this record for a particular pair of cash account and deposit account
/// defines the possibility to post to the cash account from the specific clearing account.
/// Clearing accounts are edited on the Cash Accounts (CA202000) form (which corresponds to the <see cref="T:PX.Objects.CA.CashAccountMaint" /> graph) on the tab Clearing Accounts.
/// </summary>
[PXCacheName("Clearing Account")]
[Serializable]
public class CashAccountDeposit : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  /// <summary>
  /// The unique identifier of the parent cash account.
  /// This field is the key field.
  /// </summary>
  [PXDBInt(IsKey = true)]
  [PXDBDefault(typeof (CashAccount.cashAccountID))]
  [PXUIField(DisplayName = "Cash Account ID", Visible = false)]
  [PXParent(typeof (Select<CashAccount, Where<CashAccount.cashAccountID, Equal<Current<CashAccountDeposit.cashAccountID>>>>))]
  public virtual int? CashAccountID { get; set; }

  /// <summary>
  /// The cash account used to record customer payments that will later be deposited to the bank.
  /// Corresponds to the value of the <see cref="P:PX.Objects.CA.CashAccount.CashAccountID" /> field.
  /// </summary>
  [PXDefault]
  [CashAccount(typeof (CashAccount.branchID), typeof (Search<CashAccount.cashAccountID, Where<CashAccount.curyID, Equal<Current<CashAccount.curyID>>, And<CashAccount.cashAccountID, NotEqual<Current<CashAccount.cashAccountID>>, And<Where<CashAccount.clearingAccount, Equal<boolTrue>, Or<CashAccount.cashAccountID, Equal<Current<CashAccountDeposit.depositAcctID>>>>>>>>), IsKey = true, DisplayName = "Clearing Account")]
  public virtual int? DepositAcctID { get; set; }

  /// <summary>
  /// The payment method of the deposited payment to which this charge rate should be applied.
  /// If the field is filled by empty string (default), the charge rate is applied to deposited payments, regardless of their payment method.
  /// Corresponds to the value of the <see cref="P:PX.Objects.CA.PaymentMethod.PaymentMethodID" /> field.
  /// </summary>
  [PXDBString(10, IsKey = true, IsUnicode = true)]
  [PXDefault("")]
  [PXUIField(DisplayName = "Payment Method", Required = false)]
  [PXSelector(typeof (PaymentMethod.paymentMethodID))]
  public virtual 
  #nullable disable
  string PaymentMethodID { get; set; }

  /// <summary>
  /// The entry type of the bank charges that apply to the deposit.
  /// Corresponds to the value of the <see cref="P:PX.Objects.CA.CAEntryType.EntryTypeId" /> field.
  /// </summary>
  [PXDBString(10, IsUnicode = true)]
  [PXUIField(DisplayName = "Charge Type")]
  [PXSelector(typeof (Search2<CAEntryType.entryTypeId, InnerJoin<CashAccountETDetail, On<CashAccountETDetail.entryTypeID, Equal<CAEntryType.entryTypeId>>>, Where<CashAccountETDetail.cashAccountID, Equal<Current<CashAccountDeposit.depositAcctID>>, And<CAEntryType.module, Equal<BatchModule.moduleCA>>>>), DescriptionField = typeof (CAEntryType.descr), DirtyRead = false)]
  [PXDefault(typeof (Search2<CAEntryType.entryTypeId, InnerJoin<CashAccountETDetail, On<CashAccountETDetail.entryTypeID, Equal<CAEntryType.entryTypeId>>>, Where<CashAccountETDetail.cashAccountID, Equal<Current<CashAccountDeposit.depositAcctID>>, And<CAEntryType.module, Equal<BatchModule.moduleCA>, And<CAEntryType.useToReclassifyPayments, Equal<False>, And<CashAccountETDetail.isDefault, Equal<True>>>>>>))]
  [PXFormula(typeof (Default<CashAccountDeposit.depositAcctID>))]
  public virtual string ChargeEntryTypeID { get; set; }

  /// <summary>
  /// The rate of the specified charges (expressed as a percent of the deposit total).
  /// </summary>
  [PXDBDecimal(6)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? ChargeRate { get; set; }

  public class PK : 
    PrimaryKeyOf<CashAccountDeposit>.By<CashAccountDeposit.cashAccountID, CashAccountDeposit.depositAcctID, CashAccountDeposit.paymentMethodID>
  {
    public static CashAccountDeposit Find(
      PXGraph graph,
      int? cashAccountID,
      int? depositAcctID,
      string paymentMethodID,
      PKFindOptions options = 0)
    {
      return (CashAccountDeposit) PrimaryKeyOf<CashAccountDeposit>.By<CashAccountDeposit.cashAccountID, CashAccountDeposit.depositAcctID, CashAccountDeposit.paymentMethodID>.FindBy(graph, (object) cashAccountID, (object) depositAcctID, (object) paymentMethodID, options);
    }
  }

  public static class FK
  {
    public class ParentCashAccount : 
      PrimaryKeyOf<CashAccount>.By<CashAccount.cashAccountID>.ForeignKeyOf<CashAccountDeposit>.By<CashAccountDeposit.cashAccountID>
    {
    }

    public class DepositeCashAccount : 
      PrimaryKeyOf<CashAccount>.By<CashAccount.cashAccountID>.ForeignKeyOf<CashAccountDeposit>.By<CashAccountDeposit.depositAcctID>
    {
    }

    public class ChargeEntryType : 
      PrimaryKeyOf<CAEntryType>.By<CAEntryType.entryTypeId>.ForeignKeyOf<CashAccountDeposit>.By<CashAccountDeposit.chargeEntryTypeID>
    {
    }

    public class PaymentMethod : 
      PrimaryKeyOf<PaymentMethod>.By<PaymentMethod.paymentMethodID>.ForeignKeyOf<CashAccountDeposit>.By<CashAccountDeposit.paymentMethodID>
    {
    }
  }

  public abstract class cashAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CashAccountDeposit.cashAccountID>
  {
  }

  public abstract class depositAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CashAccountDeposit.depositAcctID>
  {
  }

  public abstract class paymentMethodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CashAccountDeposit.paymentMethodID>
  {
  }

  public abstract class chargeEntryTypeID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CashAccountDeposit.chargeEntryTypeID>
  {
  }

  public abstract class chargeRate : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CashAccountDeposit.chargeRate>
  {
  }
}
