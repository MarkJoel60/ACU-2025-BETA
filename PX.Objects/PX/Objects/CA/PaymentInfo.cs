// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.PaymentInfo
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CM;
using PX.Objects.CS;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.CA;

[Serializable]
public class PaymentInfo : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected bool? _Selected = new bool?(false);

  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Selected")]
  public virtual bool? Selected
  {
    get => this._Selected;
    set => this._Selected = value;
  }

  [PXDBString(2, IsKey = true, IsFixed = true)]
  [PXUIField]
  public virtual 
  #nullable disable
  string Module { get; set; }

  [PXDBString(3, IsKey = true, IsFixed = true, BqlField = typeof (PX.Objects.AR.Standalone.ARPayment.docType))]
  [CAAPARTranType.ListByModule(typeof (PaymentInfo.module))]
  [PXUIField]
  public virtual string DocType { get; set; }

  [PXDBString(15, IsKey = true, InputMask = "", BqlField = typeof (PX.Objects.AR.Standalone.ARPayment.refNbr))]
  [PXDefault]
  [PXUIField]
  public virtual string RefNbr { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Customer/Vendor", Enabled = true, Visible = true)]
  public virtual int? BAccountID { get; set; }

  /// <exclude />
  [PXDBString(60, IsUnicode = true)]
  [PXUIField(DisplayName = "Account Name", Enabled = true, Visible = true)]
  public virtual string BAccountName { get; set; }

  /// <exclude />
  [PXDBString(30, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Customer/Vendor", Enabled = true, Visible = true)]
  public virtual string BAccountCD { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Location")]
  public virtual int? LocationID { get; set; }

  /// <exclude />
  [PXDBString(InputMask = "", IsUnicode = true)]
  [PXUIField]
  public virtual string LocationCD { get; set; }

  [PXDBString(10, IsUnicode = true)]
  [PXDefault]
  [PXSelector(typeof (Search<PaymentMethod.paymentMethodID>), DescriptionField = typeof (PaymentMethod.descr))]
  [PXUIField(DisplayName = "Payment Method", Visible = true)]
  public virtual string PaymentMethodID { get; set; }

  [PXDBString(40, IsUnicode = true)]
  [PXDefault]
  [PXUIField]
  public virtual string ExtRefNbr { get; set; }

  [PXDBString(1, IsFixed = true)]
  [PXDefault("H")]
  [PXUIField]
  [CADepositDetailsStatus.List]
  public virtual string Status { get; set; }

  [PXDBDate]
  [PXUIField]
  public virtual DateTime? DocDate { get; set; }

  [PXDBString(5, IsUnicode = true, InputMask = ">LLLLL")]
  [PXUIField(DisplayName = "Currency", Visible = true, Enabled = false)]
  [PXSelector(typeof (PX.Objects.CM.Currency.curyID))]
  public virtual string CuryID { get; set; }

  [PXDBLong]
  public virtual long? CuryInfoID { get; set; }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBCury(typeof (PaymentInfo.curyID))]
  [PXUIField]
  [PXParent(typeof (Select<CADepositEntry.PaymentFilter>), UseCurrent = true)]
  [PXUnboundFormula(typeof (Switch<Case<Where<PaymentInfo.selected, Equal<True>>, PaymentInfo.curyOrigAmtSigned>, decimal0>), typeof (SumCalc<CADepositEntry.PaymentFilter.selectionTotal>))]
  [PXUnboundFormula(typeof (Switch<Case<Where<PaymentInfo.selected, Equal<True>>, int1>, int0>), typeof (SumCalc<CADepositEntry.PaymentFilter.numberOfDocuments>))]
  public virtual Decimal? CuryOrigDocAmt { get; set; }

  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? OrigDocAmt { get; set; }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBCury(typeof (PaymentInfo.curyID))]
  [PXUIField]
  public virtual Decimal? CuryChargeTotal { get; set; }

  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? ChargeTotal { get; set; }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBCury(typeof (PaymentInfo.curyID))]
  [PXUIField]
  public virtual Decimal? CuryGrossPaymentAmount { get; set; }

  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? GrossPaymentAmount { get; set; }

  [PXDBDate]
  [PXDefault]
  [PXUIField(DisplayName = "Deposit After", Enabled = false, Visible = false)]
  public virtual DateTime? DepositAfter { get; set; }

  [PXDBString(3, IsFixed = true, BqlField = typeof (PX.Objects.AR.Standalone.ARPayment.depositType))]
  public virtual string DepositType { get; set; }

  [PXDBString(15, IsUnicode = true, BqlField = typeof (PX.Objects.AR.Standalone.ARPayment.depositNbr))]
  [PXUIField(DisplayName = "Batch Deposit Nbr.", Enabled = false)]
  public virtual string DepositNbr { get; set; }

  [PXDBBool(BqlField = typeof (PX.Objects.AR.Standalone.ARPayment.deposited))]
  [PXDefault(false)]
  public virtual bool? Deposited { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Card/Account Nbr.")]
  [PXSelector(typeof (Search<PX.Objects.AR.CustomerPaymentMethod.pMInstanceID>), DescriptionField = typeof (PX.Objects.AR.CustomerPaymentMethod.descr))]
  public virtual int? PMInstanceID { get; set; }

  [CashAccount]
  public virtual int? CashAccountID { get; set; }

  [PXDBString(1, IsFixed = true)]
  public virtual string DrCr { get; set; }

  /// <summary>The sign of the amount.</summary>
  public Decimal? OrigDocSign
  {
    [PXDependsOnFields(new System.Type[] {typeof (PaymentInfo.drCr)})] get
    {
      return new Decimal?(this.DrCr == "C" ? -1M : 1M);
    }
  }

  /// <summary>
  /// The signed amount of the original payment document in the selected currency.
  /// </summary>
  [PXDecimal(4)]
  public virtual Decimal? CuryOrigAmtSigned
  {
    [PXDependsOnFields(new System.Type[] {typeof (PaymentInfo.curyOrigDocAmt), typeof (PaymentInfo.origDocSign)})] get
    {
      Decimal? curyOrigDocAmt = this.CuryOrigDocAmt;
      Decimal? origDocSign = this.OrigDocSign;
      return !(curyOrigDocAmt.HasValue & origDocSign.HasValue) ? new Decimal?() : new Decimal?(curyOrigDocAmt.GetValueOrDefault() * origDocSign.GetValueOrDefault());
    }
  }

  public class PK : 
    PrimaryKeyOf<PaymentInfo>.By<PaymentInfo.module, PaymentInfo.docType, PaymentInfo.refNbr>
  {
    public static PaymentInfo Find(
      PXGraph graph,
      string module,
      string docType,
      string refNbr,
      PKFindOptions options = 0)
    {
      return (PaymentInfo) PrimaryKeyOf<PaymentInfo>.By<PaymentInfo.module, PaymentInfo.docType, PaymentInfo.refNbr>.FindBy(graph, (object) module, (object) docType, (object) refNbr, options);
    }
  }

  public static class FK
  {
    public class Customer : 
      PrimaryKeyOf<PX.Objects.AR.Customer>.By<PX.Objects.AR.Customer.bAccountID>.ForeignKeyOf<PaymentInfo>.By<PaymentInfo.bAccountID>
    {
    }

    public class Vendor : 
      PrimaryKeyOf<PX.Objects.AP.Vendor>.By<PX.Objects.AP.Vendor.bAccountID>.ForeignKeyOf<PaymentInfo>.By<PaymentInfo.bAccountID>
    {
    }

    public class Location : 
      PrimaryKeyOf<PX.Objects.CR.Location>.By<PX.Objects.CR.Location.bAccountID, PX.Objects.CR.Location.locationID>.ForeignKeyOf<PaymentInfo>.By<PaymentInfo.bAccountID, PaymentInfo.locationID>
    {
    }

    public class PaymentMethod : 
      PrimaryKeyOf<PaymentMethod>.By<PaymentMethod.paymentMethodID>.ForeignKeyOf<PaymentInfo>.By<PaymentInfo.paymentMethodID>
    {
    }

    public class Currency : 
      PrimaryKeyOf<PX.Objects.CM.Currency>.By<PX.Objects.CM.Currency.curyID>.ForeignKeyOf<PaymentInfo>.By<PaymentInfo.curyID>
    {
    }

    public class CurrencyInfo : 
      PrimaryKeyOf<PX.Objects.CM.CurrencyInfo>.By<PX.Objects.CM.CurrencyInfo.curyInfoID>.ForeignKeyOf<PaymentInfo>.By<PaymentInfo.curyInfoID>
    {
    }

    public class CashAccountDeposit : 
      PrimaryKeyOf<CADeposit>.By<CADeposit.tranType, CADeposit.refNbr>.ForeignKeyOf<PaymentInfo>.By<PaymentInfo.depositType, PaymentInfo.depositNbr>
    {
    }

    public class CustomerPaymentMethod : 
      PrimaryKeyOf<PX.Objects.AR.CustomerPaymentMethod>.By<PX.Objects.AR.CustomerPaymentMethod.pMInstanceID>.ForeignKeyOf<PaymentInfo>.By<PaymentInfo.pMInstanceID>
    {
    }

    public class CashAccount : 
      PrimaryKeyOf<CashAccount>.By<CashAccount.cashAccountID>.ForeignKeyOf<PaymentInfo>.By<PaymentInfo.cashAccountID>
    {
    }
  }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PaymentInfo.selected>
  {
  }

  public abstract class module : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PaymentInfo.module>
  {
  }

  public abstract class docType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PaymentInfo.docType>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PaymentInfo.refNbr>
  {
  }

  public abstract class bAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PaymentInfo.bAccountID>
  {
  }

  public abstract class bAccountName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PaymentInfo.bAccountName>
  {
  }

  public abstract class bAccountCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PaymentInfo.bAccountCD>
  {
  }

  public abstract class locationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PaymentInfo.locationID>
  {
  }

  public abstract class locationCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PaymentInfo.locationCD>
  {
  }

  public abstract class paymentMethodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PaymentInfo.paymentMethodID>
  {
  }

  public abstract class extRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PaymentInfo.extRefNbr>
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PaymentInfo.status>
  {
  }

  public abstract class docDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  PaymentInfo.docDate>
  {
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PaymentInfo.curyID>
  {
  }

  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  PaymentInfo.curyInfoID>
  {
  }

  public abstract class curyOrigDocAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PaymentInfo.curyOrigDocAmt>
  {
  }

  public abstract class origDocAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PaymentInfo.origDocAmt>
  {
  }

  public abstract class curyChargeTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PaymentInfo.curyChargeTotal>
  {
  }

  public abstract class chargeTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PaymentInfo.chargeTotal>
  {
  }

  public abstract class curyGrossPaymentAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PaymentInfo.curyGrossPaymentAmount>
  {
  }

  public abstract class grossPaymentAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PaymentInfo.grossPaymentAmount>
  {
  }

  public abstract class depositAfter : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PaymentInfo.depositAfter>
  {
  }

  public abstract class depositType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PaymentInfo.depositType>
  {
  }

  public abstract class depositNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PaymentInfo.depositNbr>
  {
  }

  public abstract class deposited : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PaymentInfo.deposited>
  {
  }

  public abstract class pMInstanceID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PaymentInfo.pMInstanceID>
  {
  }

  public abstract class cashAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PaymentInfo.cashAccountID>
  {
  }

  public abstract class drCr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PaymentInfo.drCr>
  {
  }

  public abstract class origDocSign : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PaymentInfo.origDocSign>
  {
  }

  public abstract class curyOrigAmtSigned : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PaymentInfo.curyOrigAmtSigned>
  {
  }
}
