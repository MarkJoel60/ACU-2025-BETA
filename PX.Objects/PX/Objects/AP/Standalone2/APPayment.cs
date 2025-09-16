// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.Standalone2.APPayment
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CR;
using System;

#nullable enable
namespace PX.Objects.AP.Standalone2;

[PXHidden(ServiceVisible = false)]
[Serializable]
public class APPayment : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(3, IsKey = true, IsFixed = true)]
  [PXDefault]
  public virtual 
  #nullable disable
  string DocType { get; set; }

  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = "")]
  [PXDBDefault(typeof (APRegister.refNbr))]
  public virtual string RefNbr { get; set; }

  /// Copy defaulting from <see cref="P:PX.Objects.AP.APPayment.RemitAddressID"></see>
  ///  with current from APRegister.
  [PXDBInt]
  [APAddress(typeof (Select2<PX.Objects.CR.Location, InnerJoin<BAccountR, On<BAccountR.bAccountID, Equal<PX.Objects.CR.Location.bAccountID>>, InnerJoin<PX.Objects.CR.Address, On<PX.Objects.CR.Address.addressID, Equal<PX.Objects.CR.Location.vRemitAddressID>, PX.Data.And<Where<PX.Objects.CR.Address.bAccountID, Equal<PX.Objects.CR.Location.bAccountID>, Or<PX.Objects.CR.Address.bAccountID, Equal<BAccountR.parentBAccountID>>>>>, LeftJoin<APAddress, On<APAddress.vendorID, Equal<PX.Objects.CR.Address.bAccountID>, And<APAddress.vendorAddressID, Equal<PX.Objects.CR.Address.addressID>, And<APAddress.revisionID, Equal<PX.Objects.CR.Address.revisionID>, And<APAddress.isDefaultAddress, Equal<True>>>>>>>>, Where<PX.Objects.CR.Location.bAccountID, Equal<Current<APRegister.vendorID>>, And<PX.Objects.CR.Location.locationID, Equal<Current<APRegister.vendorLocationID>>>>>))]
  public virtual int? RemitAddressID { get; set; }

  /// Copy defaulting from <see cref="P:PX.Objects.AP.APPayment.RemitContactID"></see>
  ///  with current from APRegister.
  [PXDBInt]
  [APContact(typeof (Select2<PX.Objects.CR.Location, InnerJoin<BAccountR, On<BAccountR.bAccountID, Equal<PX.Objects.CR.Location.bAccountID>>, InnerJoin<PX.Objects.CR.Contact, On<PX.Objects.CR.Contact.contactID, Equal<PX.Objects.CR.Location.vRemitContactID>, PX.Data.And<Where<PX.Objects.CR.Contact.bAccountID, Equal<PX.Objects.CR.Location.bAccountID>, Or<PX.Objects.CR.Contact.bAccountID, Equal<BAccountR.parentBAccountID>>>>>, LeftJoin<APContact, On<APContact.vendorID, Equal<PX.Objects.CR.Contact.bAccountID>, And<APContact.vendorContactID, Equal<PX.Objects.CR.Contact.contactID>, And<APContact.revisionID, Equal<PX.Objects.CR.Contact.revisionID>, And<APContact.isDefaultContact, Equal<True>>>>>>>>, Where<PX.Objects.CR.Location.bAccountID, Equal<Current<APRegister.vendorID>>, And<PX.Objects.CR.Location.locationID, Equal<Current<APRegister.vendorLocationID>>>>>))]
  public virtual int? RemitContactID { get; set; }

  [PXDBString(10, IsUnicode = true)]
  public virtual string PaymentMethodID { get; set; }

  [PXDBInt]
  public virtual int? CashAccountID { get; set; }

  [PXDBString(40, IsUnicode = true)]
  public virtual string ExtRefNbr { get; set; }

  [PXDBDate]
  public virtual System.DateTime? AdjDate { get; set; }

  [PXDBString(6, IsFixed = true)]
  public virtual string AdjFinPeriodID { get; set; }

  [PXDBString(6, IsFixed = true)]
  public virtual string AdjTranPeriodID { get; set; }

  [PXDBInt]
  [PXDefault(0)]
  public virtual int? StubCntr { get; set; }

  [PXDBInt]
  [PXDefault(0)]
  public virtual int? BillCntr { get; set; }

  [PXDBInt]
  [PXDefault(0)]
  public virtual int? ChargeCntr { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? Cleared { get; set; }

  [PXDBDate]
  public virtual System.DateTime? ClearDate { get; set; }

  [PXDBLong]
  public virtual long? CATranID { get; set; }

  [PXDBBool]
  public virtual bool? PrintCheck { get; set; }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBDecimal]
  public virtual Decimal? CuryOrigTaxDiscAmt { get; set; }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBDecimal]
  public virtual Decimal? OrigTaxDiscAmt { get; set; }

  /// Copy defaulting from <see cref="P:PX.Objects.AP.APPayment.DepositAsBatch"></see>
  /// .
  [PXDBBool]
  [PXDefault(false, typeof (Search<PX.Objects.CA.CashAccount.clearingAccount, Where<PX.Objects.CA.CashAccount.cashAccountID, Equal<Current<APPayment.cashAccountID>>>>))]
  public virtual bool? DepositAsBatch { get; set; }

  [PXDBDate]
  public virtual System.DateTime? DepositAfter { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? Deposited { get; set; }

  [PXDBString(3, IsFixed = true)]
  public virtual string DepositType { get; set; }

  [PXDBString(15, IsUnicode = true)]
  public virtual string DepositNbr { get; set; }

  public class PK : PrimaryKeyOf<APPayment>.By<APPayment.docType, APPayment.refNbr>
  {
    public static APPayment Find(
      PXGraph graph,
      string docType,
      string refNbr,
      PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<APPayment>.By<APPayment.docType, APPayment.refNbr>.FindBy(graph, (object) docType, (object) refNbr, options);
    }
  }

  public abstract class docType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APPayment.docType>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APPayment.refNbr>
  {
  }

  public abstract class remitAddressID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APPayment.remitAddressID>
  {
  }

  public abstract class remitContactID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APPayment.remitContactID>
  {
  }

  public abstract class paymentMethodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APPayment.paymentMethodID>
  {
  }

  public abstract class cashAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APPayment.cashAccountID>
  {
  }

  public abstract class extRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APPayment.extRefNbr>
  {
  }

  public abstract class adjDate : BqlType<
  #nullable enable
  IBqlDateTime, System.DateTime>.Field<
  #nullable disable
  APPayment.adjDate>
  {
  }

  public abstract class adjFinPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APPayment.adjFinPeriodID>
  {
  }

  public abstract class adjTranPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APPayment.adjTranPeriodID>
  {
  }

  public abstract class stubCntr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APPayment.stubCntr>
  {
  }

  public abstract class billCntr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APPayment.billCntr>
  {
  }

  public abstract class chargeCntr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APPayment.chargeCntr>
  {
  }

  public abstract class cleared : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APPayment.cleared>
  {
  }

  public abstract class clearDate : BqlType<
  #nullable enable
  IBqlDateTime, System.DateTime>.Field<
  #nullable disable
  APPayment.clearDate>
  {
  }

  public abstract class cATranID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  APPayment.cATranID>
  {
  }

  public abstract class printCheck : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APPayment.printCheck>
  {
  }

  public abstract class curyOrigTaxDiscAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APPayment.curyOrigTaxDiscAmt>
  {
  }

  public abstract class origTaxDiscAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    APPayment.origTaxDiscAmt>
  {
  }

  public abstract class depositAsBatch : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APPayment.depositAsBatch>
  {
  }

  public abstract class depositAfter : BqlType<
  #nullable enable
  IBqlDateTime, System.DateTime>.Field<
  #nullable disable
  APPayment.depositAfter>
  {
  }

  public abstract class deposited : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APPayment.deposited>
  {
  }

  public abstract class depositType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APPayment.depositType>
  {
  }

  public abstract class depositNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  APPayment.depositNbr>
  {
  }
}
