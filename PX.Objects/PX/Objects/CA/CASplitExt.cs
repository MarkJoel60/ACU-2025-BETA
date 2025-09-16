// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CASplitExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CM;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.CA;

[Serializable]
public class CASplitExt : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage, ICADocSource
{
  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Selected")]
  public virtual bool? Selected { get; set; }

  [PXDBString(3, IsKey = true, IsFixed = true)]
  [CATranType.List]
  [PXUIField(DisplayName = "Type", Visible = true)]
  public virtual 
  #nullable disable
  string AdjTranType { get; set; }

  [PXDBString(15, IsUnicode = true, IsKey = true)]
  [PXSelector(typeof (CAAdj.adjRefNbr))]
  [PXUIField(DisplayName = "Ref. Nbr", Visible = true)]
  public virtual string AdjRefNbr { get; set; }

  [PXDBInt(IsKey = true)]
  [PXUIField]
  public virtual int? LineNbr { get; set; }

  [PXDBString(40, IsUnicode = true)]
  [PXDefault]
  [PXUIField]
  public virtual string ExtRefNbr { get; set; }

  [CashAccount]
  public virtual int? CashAccountID { get; set; }

  [PXDBDate]
  [PXUIField]
  public virtual DateTime? TranDate { get; set; }

  [PXDBString(5, IsUnicode = true, InputMask = ">LLLLL")]
  [PXUIField(DisplayName = "Currency", Enabled = false)]
  [PXSelector(typeof (PX.Objects.CM.Currency.curyID))]
  public virtual string CuryID { get; set; }

  [PXDefault("D")]
  [PXDBString(1, IsFixed = true)]
  [CADrCr.List]
  [PXUIField(DisplayName = "Disb. / Receipt", Enabled = false, Visible = false)]
  public virtual string DrCr { get; set; }

  [FinPeriodSelector(null, typeof (CASplitExt.tranDate), typeof (CASplitExt.cashAccountID), typeof (Selector<CASplitExt.cashAccountID, CashAccount.branchID>), null, null, null, true, false, false, false, true, FinPeriodSelectorAttribute.SelectionModesWithRestrictions.Undefined, null, null, typeof (CASplitExt.tranPeriodID), true)]
  [PXUIField(DisplayName = "Fin. Period", Visible = false)]
  public virtual string FinPeriodID { get; set; }

  [PeriodID(null, null, null, true)]
  public virtual string TranPeriodID { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Released")]
  public virtual bool? Released { get; set; }

  [Account]
  public virtual int? AccountID { get; set; }

  [SubAccount(typeof (CASplitExt.accountID))]
  public virtual int? SubID { get; set; }

  [CashAccount]
  public virtual int? ReclassCashAccountID { get; set; }

  [PXDBString(256 /*0x0100*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Description")]
  public virtual string TranDesc { get; set; }

  [PXDBString(256 /*0x0100*/, IsUnicode = true)]
  public virtual string TranDescAdj { get; set; }

  [PXDBString(256 /*0x0100*/, IsUnicode = true)]
  public virtual string TranDescSplit { get; set; }

  [PXDBLong]
  public virtual long? CuryInfoID { get; set; }

  [PXDBCurrency(typeof (CASplitExt.curyInfoID), typeof (CASplitExt.tranAmt))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Amount")]
  public virtual Decimal? CuryTranAmt { get; set; }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Tran. Amount", Visible = false)]
  [PXFormula(null, typeof (SumCalc<CAAdj.tranAmt>))]
  public virtual Decimal? TranAmt { get; set; }

  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Cleared", Visible = false)]
  public virtual bool? Cleared { get; set; }

  [PXDate]
  public virtual DateTime? ClearDate { get; set; }

  [PXDBString(2, IsFixed = true)]
  [PXStringList(new string[] {"AP", "AR"}, new string[] {"AP", "AR"})]
  [PXUIField(DisplayName = "Module", Enabled = false)]
  [PXDefault("AR")]
  public virtual string OrigModule { get; set; }

  [PXDBInt]
  [PXDefault]
  [PXVendorCustomerSelector(typeof (CASplitExt.origModule))]
  [PXUIField]
  public virtual int? ReferenceID { get; set; }

  [LocationActive(typeof (Where<PX.Objects.CR.Location.bAccountID, Equal<Current<CASplitExt.referenceID>>>), DescriptionField = typeof (PX.Objects.CR.Location.descr))]
  [PXUIField(DisplayName = "Location ID")]
  [PXDefault(typeof (Search<BAccountR.defLocationID, Where<BAccountR.bAccountID, Equal<Current<CASplitExt.referenceID>>>>))]
  public virtual int? LocationID { get; set; }

  [PXString(10, IsUnicode = true)]
  [PXDefault(typeof (Coalesce<Coalesce<Search2<PX.Objects.AR.Customer.defPaymentMethodID, InnerJoin<PaymentMethodAccount, On<PaymentMethodAccount.paymentMethodID, Equal<PX.Objects.AR.Customer.defPaymentMethodID>, And<PaymentMethodAccount.useForAR, Equal<True>>>, InnerJoin<CashAccount, On<CashAccount.cashAccountID, Equal<PaymentMethodAccount.cashAccountID>, And<CashAccount.accountID, Equal<Current<CASplitExt.accountID>>, And<CashAccount.subID, Equal<Current<CASplitExt.subID>>>>>>>, Where<Current<CASplitExt.origModule>, Equal<BatchModule.moduleAR>, And<PX.Objects.AR.Customer.bAccountID, Equal<Current<CASplitExt.referenceID>>>>>, Search2<PaymentMethodAccount.paymentMethodID, InnerJoin<CashAccount, On<CashAccount.cashAccountID, Equal<PaymentMethodAccount.cashAccountID>>>, Where<Current<CASplitExt.origModule>, Equal<BatchModule.moduleAR>, And<PaymentMethodAccount.useForAR, Equal<True>, And<CashAccount.accountID, Equal<Current<CASplitExt.accountID>>, And<CashAccount.subID, Equal<Current<CASplitExt.subID>>>>>>, OrderBy<Desc<PaymentMethodAccount.aRIsDefault>>>>, Search2<PaymentMethod.paymentMethodID, InnerJoin<PaymentMethodAccount, On<PaymentMethodAccount.paymentMethodID, Equal<PaymentMethod.paymentMethodID>, And<PaymentMethodAccount.useForAP, Equal<True>>>, InnerJoin<CashAccount, On<CashAccount.cashAccountID, Equal<PaymentMethodAccount.cashAccountID>, And<CashAccount.accountID, Equal<Current<CASplitExt.accountID>>, And<CashAccount.subID, Equal<Current<CASplitExt.subID>>>>>>>, Where<Current<CASplitExt.origModule>, Equal<BatchModule.moduleAP>, And<PaymentMethod.useForAP, Equal<True>, And<PaymentMethod.isActive, Equal<boolTrue>>>>>>))]
  [PXSelector(typeof (Search2<PaymentMethod.paymentMethodID, InnerJoin<PaymentMethodAccount, On<PaymentMethodAccount.paymentMethodID, Equal<PaymentMethod.paymentMethodID>, And<Where2<Where<Current<CASplitExt.origModule>, Equal<BatchModule.moduleAP>, And<PaymentMethodAccount.useForAP, Equal<True>>>, Or<Where<Current<CASplitExt.origModule>, Equal<BatchModule.moduleAR>, And<PaymentMethodAccount.useForAR, Equal<True>>>>>>>, InnerJoin<CashAccount, On<CashAccount.cashAccountID, Equal<PaymentMethodAccount.cashAccountID>, And<CashAccount.accountID, Equal<Current<CASplitExt.accountID>>, And<CashAccount.subID, Equal<Current<CASplitExt.subID>>>>>>>, Where<PaymentMethod.isActive, Equal<boolTrue>, And<Where2<Where<Current<CASplitExt.origModule>, Equal<BatchModule.moduleAP>, And<PaymentMethod.useForAP, Equal<True>>>, Or<Where<Current<CASplitExt.origModule>, Equal<BatchModule.moduleAR>, And<PaymentMethod.useForAR, Equal<True>>>>>>>>), DescriptionField = typeof (PaymentMethod.descr))]
  [PXUIField(DisplayName = "Payment Method", Visible = true)]
  public virtual string PaymentMethodID { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Card/Account Nbr.")]
  [PXDefault(typeof (Coalesce<Search2<PX.Objects.AR.Customer.defPMInstanceID, InnerJoin<PX.Objects.AR.CustomerPaymentMethod, On<PX.Objects.AR.CustomerPaymentMethod.pMInstanceID, Equal<PX.Objects.AR.Customer.defPMInstanceID>, And<PX.Objects.AR.CustomerPaymentMethod.bAccountID, Equal<PX.Objects.AR.Customer.bAccountID>>>>, Where<Current<CASplitExt.origModule>, Equal<BatchModule.moduleAR>, And<PX.Objects.AR.Customer.bAccountID, Equal<Current<CASplitExt.referenceID>>, And<PX.Objects.AR.CustomerPaymentMethod.isActive, Equal<True>, And<PX.Objects.AR.CustomerPaymentMethod.paymentMethodID, Equal<Current<CASplitExt.paymentMethodID>>>>>>>, Search<PX.Objects.AR.CustomerPaymentMethod.pMInstanceID, Where<Current<CASplitExt.origModule>, Equal<BatchModule.moduleAR>, And<PX.Objects.AR.CustomerPaymentMethod.bAccountID, Equal<Current<CASplitExt.referenceID>>, And<PX.Objects.AR.CustomerPaymentMethod.paymentMethodID, Equal<Current<CASplitExt.paymentMethodID>>, And<PX.Objects.AR.CustomerPaymentMethod.isActive, Equal<True>>>>>, OrderBy<Desc<PX.Objects.AR.CustomerPaymentMethod.expirationDate, Desc<PX.Objects.AR.CustomerPaymentMethod.pMInstanceID>>>>>))]
  [PXSelector(typeof (Search2<PX.Objects.AR.CustomerPaymentMethod.pMInstanceID, InnerJoin<PaymentMethodAccount, On<PaymentMethodAccount.paymentMethodID, Equal<PX.Objects.AR.CustomerPaymentMethod.paymentMethodID>, And<PaymentMethodAccount.useForAR, Equal<True>>>, InnerJoin<CashAccount, On<CashAccount.cashAccountID, Equal<PaymentMethodAccount.cashAccountID>>>>, Where<PX.Objects.AR.CustomerPaymentMethod.bAccountID, Equal<Current<CASplitExt.referenceID>>, And<PX.Objects.AR.CustomerPaymentMethod.paymentMethodID, Equal<Current<CASplitExt.paymentMethodID>>, And<PX.Objects.AR.CustomerPaymentMethod.isActive, Equal<boolTrue>, And<CashAccount.accountID, Equal<Current<CASplitExt.accountID>>, And<CashAccount.subID, Equal<Current<CASplitExt.subID>>>>>>>>), DescriptionField = typeof (PX.Objects.AR.CustomerPaymentMethod.descr))]
  public virtual int? PMInstanceID { get; set; }

  [PXDBLong]
  public virtual long? TranID { get; set; }

  [PXInt]
  public virtual int? RefAccountID { get; set; }

  [PXDBLong]
  public virtual long? ChildTranID { get; set; }

  [PXInt]
  public virtual int? ChildAccountID { get; set; }

  [PXDBString(2, IsFixed = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Module")]
  public virtual string ChildOrigModule { get; set; }

  [PXDBString(3, IsFixed = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Reclassified Doc. Type")]
  [CAAPARTranType.ListByModule(typeof (CASplitExt.childOrigModule))]
  public virtual string ChildOrigTranType { get; set; }

  [PXDBString(15, IsUnicode = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Reclassified Ref. Nbr.")]
  public virtual string ChildOrigRefNbr { get; set; }

  public virtual void CopyFrom(CAAdj aAdj)
  {
    this.AdjTranType = aAdj.AdjTranType;
    this.AdjRefNbr = aAdj.AdjRefNbr;
    this.ExtRefNbr = aAdj.ExtRefNbr;
    this.TranDate = aAdj.TranDate;
    this.DrCr = aAdj.DrCr;
    this.TranDesc = aAdj.TranDesc;
    this.CashAccountID = aAdj.CashAccountID;
    this.CuryID = aAdj.CuryID;
    this.FinPeriodID = aAdj.FinPeriodID;
    this.TranPeriodID = aAdj.TranPeriodID;
    this.Cleared = aAdj.Cleared;
    if (!string.IsNullOrEmpty(this.TranDesc))
      return;
    this.TranPeriodID = aAdj.TranDesc;
  }

  public virtual void CopyFrom(CASplit aSrc)
  {
    this.LineNbr = aSrc.LineNbr;
    this.AccountID = aSrc.AccountID;
    this.SubID = aSrc.SubID;
    this.CuryInfoID = aSrc.CuryInfoID;
    this.CuryTranAmt = aSrc.CuryTranAmt;
    this.TranAmt = aSrc.TranAmt;
    this.TranDesc = aSrc.TranDesc;
  }

  public virtual void CopyFrom(CATran aSrc)
  {
    this.TranID = aSrc.TranID;
    this.RefAccountID = aSrc.CashAccountID;
  }

  public virtual void CopyFrom(PaymentReclassifyProcess.CATranRef aSrc)
  {
    this.ChildTranID = aSrc.TranID;
    this.ChildAccountID = aSrc.CashAccountID;
    this.ChildOrigModule = aSrc.OrigModule;
    this.ChildOrigTranType = aSrc.OrigTranType;
    this.ChildOrigRefNbr = aSrc.OrigRefNbr;
    this.OrigModule = aSrc.OrigModule;
  }

  public virtual void CopyFrom(PX.Objects.AR.ARPayment aSrc)
  {
    this.ReferenceID = aSrc.CustomerID;
    this.LocationID = aSrc.CustomerLocationID;
    this.PMInstanceID = aSrc.PMInstanceID;
    this.PaymentMethodID = aSrc.PaymentMethodID;
  }

  public virtual void CopyFrom(PX.Objects.AP.APPayment aSrc)
  {
    this.ReferenceID = aSrc.VendorID;
    this.LocationID = aSrc.VendorLocationID;
    this.PaymentMethodID = aSrc.PaymentMethodID;
  }

  public virtual void CopyFrom(CashAccount aSrc)
  {
    if (aSrc != null)
      this.ReclassCashAccountID = aSrc.CashAccountID;
    else
      this.ReclassCashAccountID = new int?();
  }

  int? ICADocSource.BAccountID => this.ReferenceID;

  int? ICADocSource.CARefTranAccountID => this.RefAccountID;

  long? ICADocSource.CARefTranID => this.TranID;

  int? ICADocSource.CARefSplitLineNbr => this.LineNbr;

  Decimal? ICADocSource.CuryOrigDocAmt => this.CuryTranAmt;

  Decimal? ICADocSource.CuryChargeAmt => new Decimal?();

  string ICADocSource.EntryTypeID => (string) null;

  string ICADocSource.ChargeTypeID => (string) null;

  string ICADocSource.ChargeDrCr => (string) null;

  int? ICADocSource.CashAccountID => this.ReclassCashAccountID;

  string ICADocSource.InvoiceNbr => (string) null;

  public virtual Guid? NoteID => new Guid?();

  public DateTime? MatchingPaymentDate => this.TranDate;

  public string ChargeTaxZoneID => (string) null;

  public string ChargeTaxCalcMode => (string) null;

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CASplitExt.selected>
  {
  }

  public abstract class adjTranType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CASplitExt.adjTranType>
  {
  }

  public abstract class adjRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CASplitExt.adjRefNbr>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CASplitExt.lineNbr>
  {
  }

  public abstract class extRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CASplitExt.extRefNbr>
  {
  }

  public abstract class cashAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CASplitExt.cashAccountID>
  {
  }

  public abstract class tranDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  CASplitExt.tranDate>
  {
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CASplitExt.curyID>
  {
  }

  public abstract class drCr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CASplitExt.drCr>
  {
  }

  public abstract class finPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CASplitExt.finPeriodID>
  {
  }

  public abstract class tranPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CASplitExt.tranPeriodID>
  {
  }

  public abstract class released : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CASplitExt.released>
  {
  }

  public abstract class accountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CASplitExt.accountID>
  {
  }

  public abstract class subID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CASplitExt.subID>
  {
  }

  public abstract class reclassCashAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CASplitExt.reclassCashAccountID>
  {
  }

  public abstract class tranDesc : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CASplitExt.tranDesc>
  {
  }

  public abstract class tranDescAdj : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CASplitExt.tranDescAdj>
  {
  }

  public abstract class tranDescSplit : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CASplitExt.tranDescSplit>
  {
  }

  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  CASplitExt.curyInfoID>
  {
  }

  public abstract class curyTranAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CASplitExt.curyTranAmt>
  {
  }

  public abstract class tranAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CASplitExt.tranAmt>
  {
  }

  public abstract class cleared : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CASplitExt.cleared>
  {
  }

  public abstract class clearDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  CASplitExt.clearDate>
  {
  }

  public abstract class origModule : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CASplitExt.origModule>
  {
  }

  public abstract class referenceID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CASplitExt.referenceID>
  {
  }

  public abstract class locationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CASplitExt.locationID>
  {
  }

  public abstract class paymentMethodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CASplitExt.paymentMethodID>
  {
  }

  public abstract class pMInstanceID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CASplitExt.pMInstanceID>
  {
  }

  public abstract class tranID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  CASplitExt.tranID>
  {
  }

  public abstract class refAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CASplitExt.refAccountID>
  {
  }

  public abstract class childTranID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  CASplitExt.childTranID>
  {
  }

  public abstract class childAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CASplitExt.childAccountID>
  {
  }

  public abstract class childOrigModule : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CASplitExt.childOrigModule>
  {
  }

  public abstract class childOrigTranType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CASplitExt.childOrigTranType>
  {
  }

  public abstract class childOrigRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CASplitExt.childOrigRefNbr>
  {
  }
}
