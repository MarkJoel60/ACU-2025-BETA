// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.PaymentMethodAccount
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.Common;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.CA;

/// <summary>
/// A link between <see cref="T:PX.Objects.CA.CashAccount" /> and <see cref="T:PX.Objects.CA.PaymentMethod" />.
/// The record of this type stores additional settings for a combination of a cash account and a payment method.
/// </summary>
[PXProjection(typeof (Select2<PaymentMethodAccount, InnerJoin<CashAccount, On<CashAccount.cashAccountID, Equal<PaymentMethodAccount.cashAccountID>>>>), new Type[] {typeof (PaymentMethodAccount)})]
[PXCacheName("Payment Method for Cash Account")]
[Serializable]
public class PaymentMethodAccount : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected bool? _APLastRefNbrIsNull = new bool?(false);
  protected bool? _ARLastRefNbrIsNull = new bool?(false);

  [PXDBString(10, IsUnicode = true, IsKey = true, InputMask = ">aaaaaaaaaa")]
  [PXDefault(typeof (PaymentMethod.paymentMethodID))]
  [PXParent(typeof (Select<PaymentMethod, Where<PaymentMethod.paymentMethodID, Equal<Current<PaymentMethodAccount.paymentMethodID>>>>))]
  [PXSelector(typeof (Search<PaymentMethod.paymentMethodID>))]
  [PXUIField(DisplayName = "Payment Method")]
  public virtual 
  #nullable disable
  string PaymentMethodID { get; set; }

  [CashAccount]
  [PXDefault]
  [PXRestrictor(typeof (Where<CashAccount.active, Equal<True>>), "The cash account {0} is deactivated on the Cash Accounts (CA202000) form.", new Type[] {typeof (CashAccount.cashAccountCD)})]
  public virtual int? CashAccountID { get; set; }

  [Branch(null, null, true, true, true, BqlField = typeof (CashAccount.branchID))]
  public virtual int? BranchID { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Use in AP")]
  public virtual bool? UseForAP { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "AP Default")]
  [UniqueBool(typeof (PaymentMethodAccount.paymentMethodID), typeof (PaymentMethodAccount.branchID))]
  public virtual bool? APIsDefault { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "AP - Suggest Next Number")]
  public virtual bool? APAutoNextNbr { get; set; }

  [PXDBString(40, IsUnicode = true)]
  [PXDefault]
  [PXUIField(DisplayName = "AP Last Reference Number")]
  public virtual string APLastRefNbr { get; set; }

  public virtual bool? APLastRefNbrIsNull
  {
    get => this._APLastRefNbrIsNull;
    set => this._APLastRefNbrIsNull = value;
  }

  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "Batch Last Reference Number")]
  public virtual string APBatchLastRefNbr { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Quick Batch Generation")]
  public virtual bool? APQuickBatchGeneration { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Use in AR")]
  public virtual bool? UseForAR { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "AR Default")]
  [UniqueBool(typeof (PaymentMethodAccount.paymentMethodID), typeof (PaymentMethodAccount.branchID))]
  public virtual bool? ARIsDefault { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "AR Default For Refund")]
  [UniqueBool(typeof (PaymentMethodAccount.paymentMethodID), typeof (PaymentMethodAccount.branchID))]
  public virtual bool? ARIsDefaultForRefund { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "AR - Suggest Next Number")]
  public virtual bool? ARAutoNextNbr { get; set; }

  [PXDBString(40, IsUnicode = true)]
  [PXUIField(DisplayName = "AR Last Reference Number")]
  public virtual string ARLastRefNbr { get; set; }

  public virtual bool? ARLastRefNbrIsNull
  {
    get => this._ARLastRefNbrIsNull;
    set => this._ARLastRefNbrIsNull = value;
  }

  [PXDBLastModifiedDateTime(BqlField = typeof (CashAccount.lastModifiedDateTime))]
  [PXUIField]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  public class PK : 
    PrimaryKeyOf<PaymentMethodAccount>.By<PaymentMethodAccount.paymentMethodID, PaymentMethodAccount.cashAccountID>
  {
    public static PaymentMethodAccount Find(
      PXGraph graph,
      string paymentMethodID,
      int? cashAccountID,
      PKFindOptions options = 0)
    {
      return (PaymentMethodAccount) PrimaryKeyOf<PaymentMethodAccount>.By<PaymentMethodAccount.paymentMethodID, PaymentMethodAccount.cashAccountID>.FindBy(graph, (object) paymentMethodID, (object) cashAccountID, options);
    }
  }

  public static class FK
  {
    public class PaymentMethod : 
      PrimaryKeyOf<PaymentMethod>.By<PaymentMethod.paymentMethodID>.ForeignKeyOf<PaymentMethodAccount>.By<PaymentMethodAccount.paymentMethodID>
    {
    }

    public class CashAccount : 
      PrimaryKeyOf<CashAccount>.By<CashAccount.cashAccountID>.ForeignKeyOf<PaymentMethodAccount>.By<PaymentMethodAccount.cashAccountID>
    {
    }

    public class Branch : 
      PrimaryKeyOf<PX.Objects.GL.Branch>.By<PX.Objects.GL.Branch.branchID>.ForeignKeyOf<PaymentMethodAccount>.By<PaymentMethodAccount.branchID>
    {
    }
  }

  public abstract class paymentMethodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PaymentMethodAccount.paymentMethodID>
  {
  }

  public abstract class cashAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PaymentMethodAccount.cashAccountID>
  {
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PaymentMethodAccount.branchID>
  {
  }

  public abstract class useForAP : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PaymentMethodAccount.useForAP>
  {
  }

  public abstract class aPIsDefault : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PaymentMethodAccount.aPIsDefault>
  {
  }

  public abstract class aPAutoNextNbr : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    PaymentMethodAccount.aPAutoNextNbr>
  {
  }

  public abstract class aPLastRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PaymentMethodAccount.aPLastRefNbr>
  {
  }

  public abstract class aPBatchLastRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PaymentMethodAccount.aPBatchLastRefNbr>
  {
  }

  public abstract class aPQuickBatchGeneration : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    PaymentMethodAccount.aPQuickBatchGeneration>
  {
  }

  public abstract class useForAR : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PaymentMethodAccount.useForAR>
  {
  }

  public abstract class aRIsDefault : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PaymentMethodAccount.aRIsDefault>
  {
  }

  public abstract class aRIsDefaultForRefund : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    PaymentMethodAccount.aRIsDefaultForRefund>
  {
  }

  public abstract class aRAutoNextNbr : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    PaymentMethodAccount.aRAutoNextNbr>
  {
  }

  public abstract class aRLastRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PaymentMethodAccount.aRLastRefNbr>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PaymentMethodAccount.lastModifiedDateTime>
  {
  }
}
