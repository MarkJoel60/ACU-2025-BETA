// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CABankTran
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AP;
using PX.Objects.CM;
using PX.Objects.CM.Extensions;
using PX.Objects.Common.Attributes;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.TX;
using System;

#nullable enable
namespace PX.Objects.CA;

/// <summary>
/// The main properties of CA bank transactions and their classes.
/// CA bank transactions are edited on the Process Bank Transactions (CA306000) form
/// (which corresponds to the <see cref="T:PX.Objects.CA.CABankTransactionsMaint" /> graph).
/// </summary>
[PXCacheName("Bank Transaction")]
[Serializable]
public class CABankTran : 
  PXBqlTable,
  IBqlTable,
  IBqlTableSystemDataStorage,
  ICADocWithTaxesSource,
  ICADocSource
{
  /// <summary>
  /// The cash account specified on the bank statement for which you want to upload bank transactions.
  /// This field is a part of the compound key of the document.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.CA.CashAccount.CashAccountID" /> field.
  /// </value>
  [CashAccount]
  [PXDefault(typeof (CABankTranHeader.cashAccountID))]
  public virtual int? CashAccountID { get; set; }

  /// <summary>
  /// The unique identifier of the CA bank transaction.
  /// This field is the key field.
  /// </summary>
  [PXUIField(DisplayName = "ID", Visible = false)]
  [PXDBIdentity(IsKey = true)]
  public virtual int? TranID { get; set; }

  /// <summary>
  /// The type of the bank tansaction.
  ///  The field is linked to the <see cref="P:PX.Objects.CA.CABankTranHeader.TranType" /> field.
  /// </summary>
  /// <value>
  /// The field can have one of the following values:
  /// <c>"S"</c>: Bank Statement Import,
  /// <c>"I"</c>: Payments Import
  /// </value>
  [PXDBString(1, IsFixed = true)]
  [PXDefault(typeof (CABankTranHeader.tranType))]
  [CABankTranType.List]
  [PXUIField]
  public virtual 
  #nullable disable
  string TranType { get; set; }

  /// <summary>
  /// The reference number of the imported bank statement (<see cref="T:PX.Objects.CA.CABankTranHeader">CABankTranHeader</see>),
  /// which the system generates automatically in accordance with the numbering sequence assigned to statements on the Cash Management Preferences (CA101000) form.
  /// </summary>
  [PXDBString(15, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXDBDefault(typeof (CABankTranHeader.refNbr))]
  [PXUIField(DisplayName = "Statement Nbr.")]
  [PXParent(typeof (Select<CABankTranHeader, Where<CABankTranHeader.refNbr, Equal<Current<CABankTran.headerRefNbr>>, And<CABankTranHeader.tranType, Equal<Current<CABankTran.tranType>>>>>))]
  public virtual string HeaderRefNbr { get; set; }

  /// <summary>The external identifier of the transaction.</summary>
  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Ext. Tran. ID", Visible = false)]
  public virtual string ExtTranID { get; set; }

  /// <summary>The balance type of the bank transaction.</summary>
  /// <value>
  /// The field can have one of the following values:
  /// <c>"D"</c>: Receipt,
  /// <c>"C"</c>: Disbursement
  /// </value>
  [PXDBString(1, IsFixed = true)]
  [PXDefault("C")]
  [CADrCr.List]
  [PXUIField(DisplayName = "DrCr")]
  public virtual string DrCr { get; set; }

  /// <summary>The identifier of currency of the bank transaction.</summary>
  [PXDBString(5, IsUnicode = true)]
  [PXDefault]
  [PXSelector(typeof (PX.Objects.CM.Extensions.Currency.curyID), CacheGlobal = true)]
  [PXUIField(DisplayName = "Currency")]
  public virtual string CuryID { get; set; }

  /// <summary>
  /// The identifier of the exchange rate record for the bank transaction amount.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.CM.Extensions.CurrencyInfo.CuryInfoID" /> field.
  /// </value>
  [PXDBLong]
  public virtual long? CuryInfoID { get; set; }

  /// <summary>The transaction date.</summary>
  [PXDBDate]
  [PXDefault]
  [PXUIField(DisplayName = "Tran. Date")]
  public virtual DateTime? TranDate { get; set; }

  [PXDBDate]
  [PXDefault(typeof (CABankTran.tranDate))]
  [PXUIField(DisplayName = "Payment Date")]
  public virtual DateTime? MatchingPaymentDate { get; set; }

  [CAAPAROpenPeriod(typeof (CABankTran.origModule), typeof (CABankTran.matchingPaymentDate), typeof (CABankTran.cashAccountID), typeof (Selector<CABankTran.cashAccountID, CashAccount.branchID>), null, null, null, true, typeof (CABankTran.tranPeriodID), true, RedefaultOnDateChanged = true, IsHeader = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Fin. Period")]
  public virtual string MatchingFinPeriodID { get; set; }

  [PeriodID(null, null, null, true)]
  public virtual string TranPeriodID { get; set; }

  /// <summary>The bank transaction entry date.</summary>
  [PXDBDate]
  [PXUIField(DisplayName = "Tran. Entry Date", Visible = false)]
  public virtual DateTime? TranEntryDate { get; set; }

  /// <summary>
  /// The amount of the bank transaction in the selected currency.
  /// </summary>
  [PXDBCury(typeof (CABankTran.curyID))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "CuryTranAmt")]
  public virtual Decimal? CuryTranAmt { get; set; }

  /// <summary>The currency of the matching document.</summary>
  [PXDBString(5, IsUnicode = true)]
  [PXSelector(typeof (PX.Objects.CM.Extensions.Currency.curyID), CacheGlobal = true)]
  [PXUIField(DisplayName = "Orig. Currency", Visible = false)]
  public virtual string OrigCuryID { get; set; }

  /// <summary>The external reference number of the transaction.</summary>
  [PXDBString(40, IsUnicode = true)]
  [PXUIField(DisplayName = "Ext. Ref. Nbr.")]
  public virtual string ExtRefNbr { get; set; }

  /// <summary>The description of the bank transaction.</summary>
  [PXDBString(512 /*0x0200*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Tran. Desc")]
  public virtual string TranDesc { get; set; }

  /// <summary>
  /// The description of the bank transaction.
  /// You can use this field to specify a user description of the bank transaction while keeping the original bank description (<see cref="P:PX.Objects.CA.CABankTran.TranDesc" />) untouched.
  /// </summary>
  [PXDBString(512 /*0x0200*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Custom Tran. Desc.", Enabled = true, Visible = false)]
  public virtual string UserDesc { get; set; }

  /// <summary>The payee name, if any, specified for a transaction.</summary>
  [PXDBString(256 /*0x0100*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Payee/Payer", Visible = false)]
  public virtual string PayeeName { get; set; }

  /// <summary>
  /// The payee address, if any, specified for a transaction.
  /// </summary>
  [PXDBString(256 /*0x0100*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Payee Address1", Visible = false)]
  public virtual string PayeeAddress1 { get; set; }

  /// <summary>The payee city, if any, specified for a transaction.</summary>
  [PXDBString(256 /*0x0100*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Payee City", Visible = false)]
  public virtual string PayeeCity { get; set; }

  /// <summary>The payee state, if any, specified for a transaction.</summary>
  [PXDBString(256 /*0x0100*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Payee State", Visible = false)]
  public virtual string PayeeState { get; set; }

  /// <summary>
  /// The payee postal code, if any, specified for a transaction.
  /// </summary>
  [PXDBString(256 /*0x0100*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Payee Postal Code", Visible = false)]
  public virtual string PayeePostalCode { get; set; }

  /// <summary>The payee phone, if any, specified for a transaction.</summary>
  [PXDBString(256 /*0x0100*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Payee Phone", Visible = false)]
  public virtual string PayeePhone { get; set; }

  /// <summary>The external code from the bank.</summary>
  [PXDBString(35, IsUnicode = true)]
  [PXUIField(DisplayName = "Tran. Code", Visible = false)]
  public virtual string TranCode { get; set; }

  /// <summary>
  /// The original module of the matching document.
  /// This field is displayed on the Create Payment tab of on the Process Bank Transactions (CA306000) form.
  /// </summary>
  /// <value>
  /// The field can have one of the following values:
  /// <c>"AP"</c>: Accounts Payable,
  /// <c>"AR"</c>: Accounts Receivable,
  /// <c>"CA"</c>: Cash Management.
  /// </value>
  [PXDBString(2, IsFixed = true)]
  [PXStringList(new string[] {"AP", "AR", "CA"}, new string[] {"AP", "AR", "CA"})]
  [PXUIField(DisplayName = "Module", Enabled = false)]
  [PXDefault]
  public virtual string OrigModule { get; set; }

  /// <summary>
  /// The vendor or customer associated with the document, by its business account ID.
  /// This field is displayed if the <c>"AP"</c> or <c>"AR"</c> option is selected in the <see cref="P:PX.Objects.CA.CABankTran.OrigModule" /> field.
  /// This field is displayed on the Create Payment tab of on the Process Bank Transactions (CA306000) form.
  /// </summary>
  [PXDBInt]
  [PXDefault]
  [PXVendorCustomerSelector(typeof (CABankTran.origModule), true)]
  [PXUIField(DisplayName = "Business Account", Visible = false)]
  public virtual int? PayeeBAccountID { get; set; }

  /// <summary>
  /// The location of the vendor or customer.
  /// This field is displayed if the <c>"AP"</c> or <c>"AR"</c> option is selected in the <see cref="P:PX.Objects.CA.CABankTran.OrigModule" /> field.
  /// This field is displayed on the Create Payment tab of on the Process Bank Transactions (CA306000) form.
  /// </summary>
  [PX.Objects.CS.LocationID(typeof (Where<PX.Objects.CR.Location.bAccountID, Equal<Current<CABankTran.payeeBAccountID>>>), DisplayName = "Location", DescriptionField = typeof (PX.Objects.CR.Location.descr), Visible = false)]
  [PXDefault(typeof (Search<BAccountR.defLocationID, Where<BAccountR.bAccountID, Equal<Current<CABankTran.payeeBAccountID>>>>))]
  public virtual int? PayeeLocationID { get; set; }

  /// <summary>
  /// The payment method used by a customer or vendor for the document.
  /// This field is displayed if the <c>"AP"</c> or <c>"AR"</c> option is selected in the <see cref="P:PX.Objects.CA.CABankTran.OrigModule" /> field.
  /// This field is displayed on the Create Payment tab of on the Process Bank Transactions (CA306000) form.
  /// </summary>
  [PXDBString(10, IsUnicode = true)]
  [PXDefault(typeof (Coalesce<Coalesce<Search2<PX.Objects.AR.Customer.defPaymentMethodID, InnerJoin<PaymentMethod, On<PaymentMethod.paymentMethodID, Equal<PX.Objects.AR.Customer.defPaymentMethodID>, And<PaymentMethod.useForAR, Equal<True>>>, InnerJoin<PaymentMethodAccount, On<PaymentMethodAccount.paymentMethodID, Equal<PX.Objects.AR.Customer.defPaymentMethodID>, And<PaymentMethodAccount.useForAR, Equal<True>, And<PaymentMethodAccount.cashAccountID, Equal<Current<CABankTran.cashAccountID>>>>>>>, Where<Current<CABankTran.origModule>, Equal<BatchModule.moduleAR>, And<PX.Objects.AR.Customer.bAccountID, Equal<Current<CABankTran.payeeBAccountID>>>>>, Search2<PaymentMethodAccount.paymentMethodID, InnerJoin<PaymentMethod, On<PaymentMethodAccount.paymentMethodID, Equal<PaymentMethod.paymentMethodID>, And<PaymentMethodAccount.cashAccountID, Equal<Current<CABankTran.cashAccountID>>, And<PaymentMethodAccount.useForAR, Equal<True>>>>>, Where<Current<CABankTran.origModule>, Equal<BatchModule.moduleAR>, And<PaymentMethod.useForAR, Equal<True>, And<PaymentMethod.isActive, Equal<boolTrue>>>>, OrderBy<Asc<PaymentMethodAccount.aRIsDefault, Desc<PaymentMethodAccount.paymentMethodID>>>>>, Coalesce<Search2<PX.Objects.CR.Location.vPaymentMethodID, InnerJoin<PX.Objects.AP.Vendor, On<PX.Objects.CR.Location.bAccountID, Equal<PX.Objects.AP.Vendor.bAccountID>, And<PX.Objects.CR.Location.locationID, Equal<PX.Objects.AP.Vendor.defLocationID>>>, InnerJoin<PaymentMethodAccount, On<PaymentMethodAccount.paymentMethodID, Equal<PX.Objects.CR.Location.vPaymentMethodID>, And<PaymentMethodAccount.useForAP, Equal<True>, And<PaymentMethodAccount.cashAccountID, Equal<Current<CABankTran.cashAccountID>>>>>>>, Where<Current<CABankTran.origModule>, Equal<BatchModule.moduleAP>, And<PX.Objects.AP.Vendor.bAccountID, Equal<Current<CABankTran.payeeBAccountID>>>>>, Search2<PaymentMethodAccount.paymentMethodID, InnerJoin<PaymentMethod, On<PaymentMethodAccount.paymentMethodID, Equal<PaymentMethod.paymentMethodID>, And<PaymentMethodAccount.cashAccountID, Equal<Current<CABankTran.cashAccountID>>, And<PaymentMethodAccount.useForAP, Equal<True>>>>>, Where<Current<CABankTran.origModule>, Equal<BatchModule.moduleAP>, And<PaymentMethod.useForAP, Equal<True>, And<PaymentMethod.isActive, Equal<boolTrue>>>>, OrderBy<Asc<PaymentMethodAccount.aPIsDefault, Desc<PaymentMethodAccount.paymentMethodID>>>>>>))]
  [PXSelector(typeof (Search2<PaymentMethod.paymentMethodID, InnerJoin<PaymentMethodAccount, On<PaymentMethodAccount.paymentMethodID, Equal<PaymentMethod.paymentMethodID>, And<PaymentMethodAccount.cashAccountID, Equal<Current<CABankTran.cashAccountID>>, And<Where2<Where<Current<CABankTran.origModule>, Equal<BatchModule.moduleAP>, And<PaymentMethodAccount.useForAP, Equal<True>>>, Or<Where<Current<CABankTran.origModule>, Equal<BatchModule.moduleAR>, And<PaymentMethodAccount.useForAR, Equal<True>>>>>>>>>, Where<PaymentMethod.isActive, Equal<boolTrue>, And<Where2<Where<Current<CABankTran.origModule>, Equal<BatchModule.moduleAP>, And<PaymentMethod.useForAP, Equal<True>>>, Or<Where<Current<CABankTran.origModule>, Equal<BatchModule.moduleAR>, And<PaymentMethod.useForAR, Equal<True>>>>>>>>), DescriptionField = typeof (PaymentMethod.descr))]
  [PXUIField(DisplayName = "Payment Method", Visible = false)]
  public virtual string PaymentMethodID { get; set; }

  /// <summary>
  /// The identifier of the credit card or account that is used by a customer or vendor for the document.
  /// This field is displayed on the Create Payment tab of on the Process Bank Transactions (CA306000) form.
  /// </summary>
  [PXDBInt]
  [PXUIField(DisplayName = "Card/Account Nbr.", Visible = false)]
  [PXDefault(typeof (Coalesce<Search2<PX.Objects.AR.Customer.defPMInstanceID, InnerJoin<PX.Objects.AR.CustomerPaymentMethod, On<PX.Objects.AR.CustomerPaymentMethod.pMInstanceID, Equal<PX.Objects.AR.Customer.defPMInstanceID>, And<PX.Objects.AR.CustomerPaymentMethod.bAccountID, Equal<PX.Objects.AR.Customer.bAccountID>>>>, Where<Current<CABankTran.origModule>, Equal<BatchModule.moduleAR>, And<PX.Objects.AR.Customer.bAccountID, Equal<Current<CABankTran.payeeBAccountID>>, And<PX.Objects.AR.CustomerPaymentMethod.isActive, Equal<True>, And<PX.Objects.AR.CustomerPaymentMethod.paymentMethodID, Equal<Current<CABankTran.paymentMethodID>>>>>>>, Search<PX.Objects.AR.CustomerPaymentMethod.pMInstanceID, Where<Current<CABankTran.origModule>, Equal<BatchModule.moduleAR>, And<PX.Objects.AR.CustomerPaymentMethod.bAccountID, Equal<Current<CABankTran.payeeBAccountID>>, And<PX.Objects.AR.CustomerPaymentMethod.paymentMethodID, Equal<Current<CABankTran.paymentMethodID>>, And<PX.Objects.AR.CustomerPaymentMethod.isActive, Equal<True>>>>>, OrderBy<Desc<PX.Objects.AR.CustomerPaymentMethod.expirationDate, Desc<PX.Objects.AR.CustomerPaymentMethod.pMInstanceID>>>>>))]
  [PXSelector(typeof (Search<PX.Objects.AR.CustomerPaymentMethod.pMInstanceID, Where<PX.Objects.AR.CustomerPaymentMethod.bAccountID, Equal<Current<CABankTran.payeeBAccountID>>, And<PX.Objects.AR.CustomerPaymentMethod.paymentMethodID, Equal<Current<CABankTran.paymentMethodID>>, And<PX.Objects.AR.CustomerPaymentMethod.isActive, Equal<boolTrue>>>>>), DescriptionField = typeof (PX.Objects.AR.CustomerPaymentMethod.descr))]
  [DeprecatedProcessing]
  [DisabledProcCenter]
  public virtual int? PMInstanceID { get; set; }

  /// <summary>
  /// The reference number of the document (invoice or bill) generated to match a payment.
  /// This field is displayed if the <c>"AP"</c> or <c>"AR"</c> option is selected in the <see cref="P:PX.Objects.CA.CABankTran.OrigModule" /> field.
  /// This field is displayed on the Create Payment tab of on the Process Bank Transactions (CA306000) form.
  /// </summary>
  [PXDBString(256 /*0x0100*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Invoice Nbr.", Visible = false)]
  public virtual string InvoiceInfo { get; set; }

  /// <summary>
  /// Specifies (if set to <c>true</c>) that this bank transaction is matched to the payment and ready to be processed.
  /// That is, the bank transaction has been matched to an existing transaction in the system, or details of a new document that matches this transaction have been specified.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Matched", Visible = true, Enabled = false)]
  public virtual bool? DocumentMatched { get; set; }

  /// <summary>
  /// Specifies (if set to <c>true</c>) that the rule was applied to clear the transaction on the Process Bank Transactions (CA306000) form.
  /// </summary>
  [PXBool]
  [PXDefault(false)]
  [PXUIField]
  public virtual bool? RuleApplied
  {
    [PXDependsOnFields(new System.Type[] {typeof (CABankTran.ruleID), typeof (CABankTran.createDocument)})] get
    {
      return new bool?(this.CreateDocument.GetValueOrDefault() && this.RuleID.HasValue && this.OrigModule == "CA");
    }
  }

  /// <summary>
  /// Specifies (if set to <c>true</c>) that the button <c>Create Rule</c> is enabled.
  /// This is a virtual field and it has no representation in the database.
  /// </summary>
  [PXBool]
  [PXDefault(false)]
  [PXUIField]
  public virtual bool? ApplyRuleEnabled
  {
    [PXDependsOnFields(new System.Type[] {typeof (CABankTran.ruleID), typeof (CABankTran.createDocument)})] get
    {
      return new bool?(this.CreateDocument.GetValueOrDefault() && !this.RuleID.HasValue && this.OrigModule == "CA");
    }
  }

  /// <summary>
  /// Specifies (if set to <c>true</c>) that this bank transaction is matched to the transaction in the system.
  /// This is a virtual field and it has no representation in the database.
  /// </summary>
  [PXBool]
  [PXUIField(DisplayName = "Matched", Visible = true, Enabled = false)]
  public virtual bool? MatchedToExisting { get; set; }

  /// <summary>
  /// Specifies (if set to <c>true</c>) that this bank transaction is matched to the invoice.
  /// This is a virtual field and it has no representation in the database.
  /// </summary>
  [PXBool]
  [PXUIField(DisplayName = "Matched to Invoice", Visible = false, Enabled = false)]
  public virtual bool? MatchedToInvoice { get; set; }

  /// <summary>
  /// Specifies (if set to <c>true</c>) that this bank transaction is matched to the invoice.
  /// </summary>
  [PXDBBool]
  [PXUIField(DisplayName = "Matched to Invoice", Visible = false, Enabled = false)]
  public virtual bool? HistMatchedToInvoice { get; set; }

  /// <summary>
  /// Specifies (if set to <c>true</c>) that this bank transaction is matched to the Expense Receipt.
  /// This is a virtual field and it has no representation in the database.
  /// </summary>
  [PXBool]
  [PXUIField(DisplayName = "Matched To Expense Receipt", Visible = false, Enabled = false)]
  public virtual bool? MatchedToExpenseReceipt { get; set; }

  /// <summary>
  /// Specifies (if set to <c>true</c>) that a new payment will be created for the selected bank transactions.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Create")]
  public virtual bool? CreateDocument { get; set; }

  /// <summary>
  /// Specifies (if set to <c>true</c>) that the selected bank transaction can be matched to multiple invoices.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Match to Multiple Documents")]
  public virtual bool? MultipleMatching { get; set; }

  /// <summary>
  /// Specifies (if set to <c>true</c>) that the selected bank transaction can be matched to multiple payments.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Match to Multiple Payments")]
  public virtual bool? MultipleMatchingToPayments { get; set; }

  /// <summary>
  /// Specifies (if set to <c>true</c>) that the selected bank transaction can be matched to multiple documents with any amount and any direction.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Match to Receipts and Disbursements")]
  public virtual bool? MatchReceiptsAndDisbursements { get; set; }

  /// <summary>
  /// The status of the bank transaction.
  /// This is a virtual field and it has no representation in the database.
  /// </summary>
  /// <value>
  /// The field can have one of the following values:
  /// <c>"M"</c>: The bank transaction is matched to the payment and ready to be processed.
  /// <c>"I"</c>: The bank transaction is matched to the invoice.
  /// <c>"C"</c>: The bank transactions will be matched to a new payment.
  /// <c>"H"</c>: The bank transaction is hidden from the statement on the Process Bank Transactions (CA306000) form.
  /// <c>string.Empty</c>: The <see cref="P:PX.Objects.CA.CABankTran.DocumentMatched" />, <see cref="P:PX.Objects.CA.CABankTran.MatchedToInvoice" />, <see cref="P:PX.Objects.CA.CABankTran.CreateDocument" />, and <see cref="P:PX.Objects.CA.CABankTran.Hidden" /> flags are set to <c>false</c>.
  /// </value>
  [PXString(1, IsFixed = true)]
  [CABankTranStatus.List]
  [PXUIField]
  public virtual string Status
  {
    [PXDependsOnFields(new System.Type[] {typeof (CABankTran.hidden), typeof (CABankTran.createDocument), typeof (CABankTran.matchedToInvoice), typeof (CABankTran.documentMatched), typeof (CABankTran.matchedToExpenseReceipt)})] get
    {
      if (this.Hidden.GetValueOrDefault())
        return "H";
      if (this.MatchedToExpenseReceipt.GetValueOrDefault())
        return "R";
      if (this.CreateDocument.GetValueOrDefault())
        return "C";
      if (this.MatchedToInvoice.GetValueOrDefault())
        return "I";
      return this.DocumentMatched.GetValueOrDefault() ? "M" : string.Empty;
    }
  }

  /// <summary>
  /// Specifies (if set to <c>true</c>) that this bank transaction is processed.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Processed")]
  public virtual bool? Processed { get; set; }

  /// <summary>
  /// The identifier of an entry type that is used as a template for a new cash transaction to be created to match the selected bank transaction.
  /// The field is displayed if the <c>CA</c> option is selected in the <see cref="P:PX.Objects.CA.CABankTran.OrigModule" /> field.
  /// This field is displayed on the Create Payment tab of on the Process Bank Transactions (CA306000) form.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.CA.CAEntryType.EntryTypeId" /> field.
  /// </value>
  [PXDBString(10, IsUnicode = true)]
  [PXDefault]
  [PXSelector(typeof (Search2<CAEntryType.entryTypeId, InnerJoin<CashAccountETDetail, On<CashAccountETDetail.entryTypeID, Equal<CAEntryType.entryTypeId>>>, Where<CashAccountETDetail.cashAccountID, Equal<Current<CABankTran.cashAccountID>>, And<CAEntryType.module, Equal<BatchModule.moduleCA>, And<Where<CAEntryType.drCr, Equal<Current<CABankTran.drCr>>>>>>>), DescriptionField = typeof (CAEntryType.descr))]
  [PXUIField]
  public virtual string EntryTypeID { get; set; }

  /// <summary>The tax zone that applies to the bank transaction.</summary>
  /// <value>
  /// Corresponds to the value of the <see cref="P:PX.Objects.TX.TaxZone.TaxZoneID" /> field.
  /// </value>
  [PXDBString(10, IsUnicode = true)]
  [PXUIField]
  [PXSelector(typeof (PX.Objects.TX.TaxZone.taxZoneID), DescriptionField = typeof (PX.Objects.TX.TaxZone.descr), Filterable = true)]
  [PXDefault(typeof (Search<CashAccountETDetail.taxZoneID, Where<CashAccountETDetail.cashAccountID, Equal<Current<CABankTran.cashAccountID>>, And<CashAccountETDetail.entryTypeID, Equal<Current<CABankTran.entryTypeID>>>>>))]
  public virtual string TaxZoneID { get; set; }

  /// <summary>
  /// The tax calculation mode, which defines which amounts (tax-inclusive or tax-exclusive)
  /// should be entered in the detail lines of a document.
  /// This field is displayed only if the <see cref="P:PX.Objects.CS.FeaturesSet.NetGrossEntryMode" /> field is set to <c>true</c>.
  /// </summary>
  /// <value>
  /// The field can have one of the following values:
  /// <c>"T"</c> (Tax Settings): The tax amount for the document is calculated according to the settings of the applicable tax or taxes.
  /// <c>"G"</c> (Gross): The amount in the document detail line includes a tax or taxes.
  /// <c>"N"</c> (Net): The amount in the document detail line does not include taxes.
  /// </value>
  [PXDBString(1, IsFixed = true)]
  [PXDefault("T", typeof (Search<CashAccountETDetail.taxCalcMode, Where<CashAccountETDetail.cashAccountID, Equal<Current<CABankTran.cashAccountID>>, And<CashAccountETDetail.entryTypeID, Equal<Current<CABankTran.entryTypeID>>>>>))]
  [TaxCalculationMode.List]
  [PXUIField(DisplayName = "Tax Calculation Mode")]
  public virtual string TaxCalcMode { get; set; }

  /// <summary>
  /// The identifier of an entry type that is used as a template for a new cash transaction to be created to match the selected bank transaction.
  /// The field is displayed if the <c>Multiple Documents</c> option is selected.
  /// This field is displayed on the Match to Invoices tab of on the Process Bank Transactions (CA306000) form.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.CA.CAEntryType.EntryTypeId" /> field.
  /// </value>
  [PXDBString(10, IsUnicode = true)]
  [PXDefault]
  [PXSelector(typeof (Search2<CAEntryType.entryTypeId, InnerJoin<CashAccountETDetail, On<CashAccountETDetail.entryTypeID, Equal<CAEntryType.entryTypeId>>>, Where<CashAccountETDetail.cashAccountID, Equal<Current<CABankTran.cashAccountID>>, And<CAEntryType.module, Equal<BatchModule.moduleCA>>>>), DescriptionField = typeof (CAEntryType.descr))]
  [PXUIField]
  public virtual string ChargeTypeID { get; set; }

  /// <summary>The tax zone that applies to the bank transaction.</summary>
  /// <value>
  /// Corresponds to the value of the <see cref="P:PX.Objects.TX.TaxZone.TaxZoneID" /> field.
  /// </value>
  [PXDBString(10, IsUnicode = true)]
  [PXUIField]
  [PXSelector(typeof (PX.Objects.TX.TaxZone.taxZoneID), DescriptionField = typeof (PX.Objects.TX.TaxZone.descr), Filterable = true)]
  [PXDefault(typeof (Search<CashAccountETDetail.taxZoneID, Where<CashAccountETDetail.cashAccountID, Equal<Current<CABankTran.cashAccountID>>, And<CashAccountETDetail.entryTypeID, Equal<Current<CABankTran.chargeTypeID>>>>>))]
  [PXFormula(typeof (Default<CABankTran.chargeTypeID>))]
  public virtual string ChargeTaxZoneID { get; set; }

  /// <summary>
  /// The tax calculation mode, which defines which amounts (tax-inclusive or tax-exclusive)
  /// should be entered in the detail lines of a document.
  /// This field is displayed only if the <see cref="P:PX.Objects.CS.FeaturesSet.NetGrossEntryMode" /> field is set to <c>true</c>.
  /// </summary>
  /// <value>
  /// The field can have one of the following values:
  /// <c>"T"</c> (Tax Settings): The tax amount for the document is calculated according to the settings of the applicable tax or taxes.
  /// <c>"G"</c> (Gross): The amount in the document detail line includes a tax or taxes.
  /// <c>"N"</c> (Net): The amount in the document detail line does not include taxes.
  /// </value>
  [PXDBString(1, IsFixed = true)]
  [PXDefault("T", typeof (Search<CashAccountETDetail.taxCalcMode, Where<CashAccountETDetail.cashAccountID, Equal<Current<CABankTran.cashAccountID>>, And<CashAccountETDetail.entryTypeID, Equal<Current<CABankTran.chargeTypeID>>>>>))]
  [TaxCalculationMode.List]
  [PXUIField(DisplayName = "Charge Tax Calculation Mode")]
  [PXFormula(typeof (Default<CABankTran.chargeTypeID>))]
  public virtual string ChargeTaxCalcMode { get; set; }

  [PXDefault(typeof (Search<CAEntryType.drCr, Where<CAEntryType.entryTypeId, Equal<Current<CABankTran.chargeTypeID>>>>))]
  [PXDBString(1, IsFixed = true)]
  [CADrCr.List]
  public virtual string ChargeDrCr { [PXDependsOnFields(new System.Type[] {typeof (CABankTran.chargeTypeID)})] get; set; }

  /// <summary>
  /// The amount of the receipt in the selected currency.
  /// This is a virtual field and it has no representation in the database.
  /// </summary>
  [PXCury(typeof (CABankTran.curyID))]
  [PXUIField(DisplayName = "Receipt")]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXFormula(null, typeof (SumCalc<CABankTranHeader.curyDebitsTotal>))]
  public virtual Decimal? CuryDebitAmt
  {
    [PXDependsOnFields(new System.Type[] {typeof (CABankTran.drCr), typeof (CABankTran.curyTranAmt)})] get
    {
      return !(this.DrCr == "D") ? new Decimal?(0M) : this.CuryTranAmt;
    }
    set
    {
      Decimal? nullable = value;
      Decimal num = 0M;
      if (!(nullable.GetValueOrDefault() == num & nullable.HasValue))
      {
        this.CuryTranAmt = value;
        this.DrCr = "D";
      }
      else
      {
        if (!(this.DrCr == "D"))
          return;
        this.CuryTranAmt = new Decimal?(0M);
      }
    }
  }

  /// <summary>
  /// The amount of the disbursement in the selected currency.
  /// This is a virtual field and it has no representation in the database.
  /// </summary>
  [PXCury(typeof (CABankTran.curyID))]
  [PXUIField(DisplayName = "Disbursement")]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXFormula(null, typeof (SumCalc<CABankTranHeader.curyCreditsTotal>))]
  public virtual Decimal? CuryCreditAmt
  {
    [PXDependsOnFields(new System.Type[] {typeof (CABankTran.drCr), typeof (CABankTran.curyTranAmt)})] get
    {
      if (!(this.DrCr == "C"))
        return new Decimal?(0M);
      Decimal? curyTranAmt = this.CuryTranAmt;
      return !curyTranAmt.HasValue ? new Decimal?() : new Decimal?(-curyTranAmt.GetValueOrDefault());
    }
    set
    {
      Decimal? nullable1 = value;
      Decimal num = 0M;
      if (!(nullable1.GetValueOrDefault() == num & nullable1.HasValue))
      {
        Decimal? nullable2 = value;
        this.CuryTranAmt = nullable2.HasValue ? new Decimal?(-nullable2.GetValueOrDefault()) : new Decimal?();
        this.DrCr = "C";
      }
      else
      {
        if (!(this.DrCr == "C"))
          return;
        this.CuryTranAmt = new Decimal?(0M);
      }
    }
  }

  /// <summary>
  /// The total amount of the created document in the selected currency.
  /// This field is displayed if the <c>"AP"</c> or <c>"AR"</c> option is selected in the <see cref="P:PX.Objects.CA.CABankTran.OrigModule" /> field.
  /// This is a virtual field and it has no representation in the database.
  /// </summary>
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Total Amount")]
  [PXCury(typeof (CABankTran.curyID))]
  public virtual Decimal? CuryTotalAmt
  {
    get
    {
      if (!(this.DrCr == "C"))
        return this.CuryTranAmt;
      Decimal num = (Decimal) -1;
      Decimal? curyTranAmt = this.CuryTranAmt;
      return !curyTranAmt.HasValue ? new Decimal?() : new Decimal?(num * curyTranAmt.GetValueOrDefault());
    }
    set
    {
    }
  }

  /// <summary>
  /// The copy of the <see cref="P:PX.Objects.CA.CABankTran.CuryTotalAmt" /> field.
  /// The total amount of the created document in the selected currency.
  /// This is a virtual field and it has no representation in the database.
  /// </summary>
  [PXUnboundDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Transaction Amount")]
  [PXCury(typeof (CABankTran.curyID))]
  public virtual Decimal? CuryTotalAmtCopy
  {
    get => this.CuryTotalAmt;
    set
    {
    }
  }

  /// <summary>
  /// The sum of all details and exclusive taxes in the selected currency.
  /// </summary>
  [PXDBCurrency(typeof (CABankTran.curyInfoID), typeof (CABankTran.detailsWithTaxesTotal))]
  [PXUIField]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryDetailsWithTaxesTotal { get; set; }

  /// <summary>
  /// The sum of all details and exclusive taxes in the base currency.
  /// </summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? DetailsWithTaxesTotal { get; set; }

  /// <summary>
  /// The total amount of tax paid on the document in the selected currency.
  /// </summary>
  [PXDBCurrency(typeof (CABankTran.curyInfoID), typeof (CABankTran.taxTotal))]
  [PXUIField]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryTaxTotal { get; set; }

  /// <summary>
  /// The total amount of tax paid on the document in the base currency.
  /// </summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TaxTotal { get; set; }

  /// <summary>
  /// The copy of the <see cref="P:PX.Objects.CA.CABankTran.CuryTotalAmt" /> field.
  /// The total amount of the created document in the selected currency.
  /// This is a virtual field and it has no representation in the database.
  /// </summary>
  [PXUnboundDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Transaction Amount")]
  [PXCury(typeof (CABankTran.curyID))]
  public virtual Decimal? CuryTotalAmtDisplay => this.CuryTotalAmt;

  /// <summary>
  /// The amount of the transaction for which the documents (to match the bank transaction) are added.
  /// Represented in the selected currency.
  /// This field is displayed if the <c>CA</c> option is selected in the <see cref="P:PX.Objects.CA.CABankTran.OrigModule" /> field.
  /// </summary>
  [PXDBCury(typeof (CABankTran.curyID))]
  [PXUIField]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryApplAmtCA { get; set; }

  /// <summary>
  /// The balance of the transaction for which you can add the documents.
  /// This field is displayed if the <c>CA</c> option is selected in the <see cref="P:PX.Objects.CA.CABankTran.OrigModule" /> field.
  /// This is a virtual field and it has no representation in the database.
  /// </summary>
  [PXDBCurrency(typeof (CABankTran.curyInfoID), typeof (CABankTran.unappliedBalCA))]
  [PXUIField]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryUnappliedBalCA { get; set; }

  /// <summary>The amount of the transaction in the base currency.</summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? UnappliedBalCA { get; set; }

  /// <summary>
  /// The amount of the application for this payment.
  /// This field is displayed if the <c>"AP"</c> or <c>"AR"</c> option is selected in the <see cref="P:PX.Objects.CA.CABankTran.OrigModule" /> field.
  /// </summary>
  [PXDBCury(typeof (CABankTran.curyID))]
  [PXUIField]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryApplAmt { get; set; }

  /// <summary>
  /// The unapplied balance of the document in the selected currency.
  /// This field is displayed if the <c>"AP"</c> or <c>"AR"</c> option is selected in the <see cref="P:PX.Objects.CA.CABankTran.OrigModule" /> field.
  /// This is a virtual field and it has no representation in the database.
  /// </summary>
  [PXCury(typeof (CABankTran.curyID))]
  [PXUIField]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryUnappliedBal
  {
    [PXDependsOnFields(new System.Type[] {typeof (CABankTran.curyTotalAmt), typeof (CABankTran.curyApplAmt)})] get
    {
      Decimal? nullable = this.CuryTotalAmt;
      Decimal valueOrDefault1 = nullable.GetValueOrDefault();
      nullable = this.CuryApplAmt;
      Decimal valueOrDefault2 = nullable.GetValueOrDefault();
      return new Decimal?(valueOrDefault1 - valueOrDefault2);
    }
    set
    {
    }
  }

  /// <summary>
  /// The amount of the application for this payment.
  /// This field is displayed if the <c>"AP"</c> or <c>"AR"</c> option is selected in the <see cref="P:PX.Objects.CA.CABankTran.OrigModule" /> field.
  /// </summary>
  [PXDBCury(typeof (CABankTran.curyID))]
  [PXUIField]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryApplAmtMatch { get; set; }

  /// <summary>
  /// The amount of the application for this payment.
  /// This field is displayed if the <c>"AP"</c> or <c>"AR"</c> option is selected in the <see cref="P:PX.Objects.CA.CABankTran.OrigModule" /> field.
  /// </summary>
  [PXCury(typeof (CABankTran.curyID))]
  [PXUIField]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryApplAmtMatchToInvoice
  {
    [PXDependsOnFields(new System.Type[] {typeof (CABankTran.matchedToInvoice), typeof (CABankTran.multipleMatching), typeof (CABankTran.matchedToExisting), typeof (CABankTran.drCr), typeof (CABankTran.chargeDrCr), typeof (CABankTran.curyApplAmtMatch), typeof (CABankTran.curyChargeAmt)})] get
    {
      if (!this.MatchedToInvoice.GetValueOrDefault())
      {
        if (this.MultipleMatching.GetValueOrDefault())
        {
          bool? matchedToExisting = this.MatchedToExisting;
          bool flag = false;
          if (matchedToExisting.GetValueOrDefault() == flag & matchedToExisting.HasValue)
            goto label_4;
        }
        return new Decimal?(0M);
      }
label_4:
      Decimal? curyApplAmtMatch = this.CuryApplAmtMatch;
      Decimal num = (Decimal) (this.ChargeDrCr == this.DrCr ? 1 : -1);
      Decimal? curyChargeAmt = this.CuryChargeAmt;
      Decimal valueOrDefault = (curyChargeAmt.HasValue ? new Decimal?(num * curyChargeAmt.GetValueOrDefault()) : new Decimal?()).GetValueOrDefault();
      return !curyApplAmtMatch.HasValue ? new Decimal?() : new Decimal?(curyApplAmtMatch.GetValueOrDefault() + valueOrDefault);
    }
  }

  /// <summary>
  /// The amount of the application for this payment.
  /// This field is displayed if the <c>"AP"</c> or <c>"AR"</c> option is selected in the <see cref="P:PX.Objects.CA.CABankTran.OrigModule" /> field.
  /// </summary>
  [PXCury(typeof (CABankTran.curyID))]
  [PXUIField]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryApplAmtMatchToPayment
  {
    [PXDependsOnFields(new System.Type[] {typeof (CABankTran.matchedToInvoice), typeof (CABankTran.multipleMatchingToPayments), typeof (CABankTran.matchedToExisting), typeof (CABankTran.matchedToExpenseReceipt), typeof (CABankTran.curyApplAmtMatch)})] get
    {
      return !this.MatchedToExisting.GetValueOrDefault() && !this.MultipleMatchingToPayments.GetValueOrDefault() || this.MatchedToInvoice.GetValueOrDefault() || this.MatchedToExpenseReceipt.GetValueOrDefault() ? new Decimal?(0M) : this.CuryApplAmtMatch;
    }
  }

  /// <summary>
  /// The unapplied balance of the document in the selected currency.
  /// This is a virtual field and it has no representation in the database.
  /// </summary>
  [PXCury(typeof (CABankTran.curyID))]
  [PXUIField]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryUnappliedBalMatch
  {
    [PXDependsOnFields(new System.Type[] {typeof (CABankTran.curyTotalAmt), typeof (CABankTran.curyApplAmtMatch), typeof (CABankTran.chargeDrCr), typeof (CABankTran.curyChargeAmt)})] get
    {
      Decimal? nullable = this.CuryTotalAmt;
      Decimal valueOrDefault1 = nullable.GetValueOrDefault();
      nullable = this.CuryApplAmtMatch;
      Decimal valueOrDefault2 = nullable.GetValueOrDefault();
      Decimal num1 = valueOrDefault1 - valueOrDefault2;
      Decimal num2 = (Decimal) (this.ChargeDrCr == this.DrCr ? 1 : -1);
      nullable = this.CuryChargeAmt;
      Decimal valueOrDefault3 = (nullable.HasValue ? new Decimal?(num2 * nullable.GetValueOrDefault()) : new Decimal?()).GetValueOrDefault();
      return new Decimal?(num1 - valueOrDefault3);
    }
    set
    {
    }
  }

  /// <summary>
  /// The document total that is exempt from VAT in the selected currency.
  /// This total is calculated as the taxable amount for the tax
  /// with the <see cref="P:PX.Objects.TX.Tax.ExemptTax" /> field set to <c>true</c> (that is, the Include in VAT Exempt Total check box selected on the Taxes (TX205000) form).
  /// </summary>
  [PXDBCurrency(typeof (CABankTran.curyInfoID), typeof (CABankTran.vatExemptTotal))]
  [PXUIField]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryVatExemptTotal { get; set; }

  /// <summary>
  /// The document total that is exempt from VAT in the base currency.
  /// </summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? VatExemptTotal { get; set; }

  /// <summary>
  /// The document total that is subjected to VAT in the selected currency.
  /// The field is displayed only if
  /// the <see cref="P:PX.Objects.TX.Tax.IncludeInTaxable" /> field is set to <c>true</c> (that is, the Include in VAT Exempt Total check box is selected on the Taxes (TX205000) form).
  /// </summary>
  [PXDBCurrency(typeof (CABankTran.curyInfoID), typeof (CABankTran.vatTaxableTotal))]
  [PXUIField]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryVatTaxableTotal { get; set; }

  /// <summary>
  /// The document total that is subjected to VAT in the base currency.
  /// </summary>
  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? VatTaxableTotal { get; set; }

  /// <summary>
  /// The difference between the original document amount and the rounded amount in the selected currency.
  /// </summary>
  [PXDBCurrency(typeof (CABankTran.curyInfoID), typeof (CABankTran.taxRoundDiff), BaseCalc = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public Decimal? CuryTaxRoundDiff { get; set; }

  /// <summary>
  /// The difference between the original document amount and the rounded amount in the base currency.
  /// </summary>
  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public Decimal? TaxRoundDiff { get; set; }

  /// <summary>
  /// The amount of the charge including tax (if applicable).
  /// </summary>
  [PXDBCury(typeof (CABankTran.curyID))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Charge Amount")]
  public virtual Decimal? CuryChargeAmt { get; set; }

  /// <summary>
  /// The amount of the charge including tax (if applicable).
  /// </summary>
  [PXDBCury(typeof (CABankTran.curyID))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Charge Tax Amount")]
  public virtual Decimal? CuryChargeTaxAmt { get; set; }

  /// <summary>
  /// The unapplied balance of the document in the selected currency.
  /// This is a virtual field and it has no representation in the database.
  /// </summary>
  [PXCury(typeof (CABankTran.curyID))]
  [PXUIField]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryUnappliedBalMatchToInvoice
  {
    [PXDependsOnFields(new System.Type[] {typeof (CABankTran.matchedToInvoice), typeof (CABankTran.multipleMatching), typeof (CABankTran.matchedToExisting), typeof (CABankTran.curyUnappliedBalMatch)})] get
    {
      if (!this.MatchedToInvoice.GetValueOrDefault())
      {
        if (this.MultipleMatching.GetValueOrDefault())
        {
          bool? matchedToExisting = this.MatchedToExisting;
          bool flag = false;
          if (matchedToExisting.GetValueOrDefault() == flag & matchedToExisting.HasValue)
            goto label_4;
        }
        return new Decimal?(0M);
      }
label_4:
      return this.CuryUnappliedBalMatch;
    }
  }

  /// <summary>
  /// The unapplied balance of the document in the selected currency.
  /// This is a virtual field and it has no representation in the database.
  /// </summary>
  [PXCury(typeof (CABankTran.curyID))]
  [PXUIField]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryUnappliedBalMatchToPayment
  {
    [PXDependsOnFields(new System.Type[] {typeof (CABankTran.matchedToInvoice), typeof (CABankTran.multipleMatchingToPayments), typeof (CABankTran.matchedToExisting), typeof (CABankTran.matchedToExpenseReceipt), typeof (CABankTran.curyUnappliedBalMatch)})] get
    {
      return !this.MatchedToExisting.GetValueOrDefault() && !this.MultipleMatchingToPayments.GetValueOrDefault() || this.MatchedToInvoice.GetValueOrDefault() || this.MatchedToExpenseReceipt.GetValueOrDefault() ? new Decimal?(0M) : this.CuryUnappliedBalMatch;
    }
  }

  [PXString(3, IsFixed = true)]
  [PXDefault]
  [APPaymentType.List]
  [PXFieldDescription]
  public string DocType
  {
    get
    {
      return this.OrigModule == "AP" ? (this.DrCr == "C" ? "CHK" : "REF") : (this.DrCr == "C" ? "REF" : "PMT");
    }
    set
    {
    }
  }

  /// <summary>
  /// The counter of related adjustments.
  /// The <c>PXParentAttribute</c> from the <see cref="P:PX.Objects.CA.CABankTranAdjustment.AdjNbr" /> field links on this field.
  /// </summary>
  [PXDBInt]
  [PXDefault(0)]
  public virtual int? LineCntr { get; set; }

  /// <summary>
  /// The counter of related details.
  /// The <c>PXParentAttribute</c> from the <see cref="P:PX.Objects.CA.CABankTranDetail.LineNbr" /> field links on this field.
  /// </summary>
  [PXDBInt]
  [PXDefault(0)]
  public virtual int? LineCntrCA { get; set; }

  /// <summary>
  /// The counter of related details.
  /// The <c>PXParentAttribute</c> from the <see cref="P:PX.Objects.CA.CABankTranMatch.LineNbr" /> field links on this field.
  /// </summary>
  [PXDBInt]
  [PXDefault(0)]
  public virtual int? LineCntrMatch { get; set; }

  /// <summary>Number of records with errors in the details table.</summary>
  [PXDBInt]
  [PXDefault(0)]
  public int? DetailErrorCount { get; set; }

  /// <summary>
  /// The copy of the <see cref="P:PX.Objects.CA.CABankTran.PayeeBAccountID" /> field.
  /// This field is displayed on the Match to Invoices tab of on the Process Bank Transactions (CA306000) form.
  /// </summary>
  [PXInt]
  [PXSelector(typeof (Search<BAccountR.bAccountID>), SubstituteKey = typeof (BAccountR.acctCD), DescriptionField = typeof (BAccountR.acctName))]
  [PXUIField(DisplayName = "Business Account")]
  public virtual int? PayeeBAccountIDCopy
  {
    get => this.PayeeBAccountID;
    set => this.PayeeBAccountID = value;
  }

  /// <summary>
  /// The copy of the <see cref="P:PX.Objects.CA.CABankTran.PayeeLocationID" /> field .
  /// This field is displayed on the Match to Invoices tab of on the Process Bank Transactions (CA306000) form.
  /// </summary>
  [PX.Objects.CS.LocationID(typeof (Where<PX.Objects.CR.Location.bAccountID, Equal<Current<CABankTran.payeeBAccountID>>>), DisplayName = "Location", DescriptionField = typeof (PX.Objects.CR.Location.descr), IsDBField = false)]
  public virtual int? PayeeLocationIDCopy
  {
    get => this.PayeeLocationID;
    set => this.PayeeLocationID = value;
  }

  /// <summary>
  /// The copy of the <see cref="P:PX.Objects.CA.CABankTran.PaymentMethodID" /> field.
  /// This field is displayed on the Match to Invoices tab of on the Process Bank Transactions (CA306000) form.
  /// </summary>
  [PXString(10, IsUnicode = true)]
  [PXSelector(typeof (Search2<PaymentMethod.paymentMethodID, InnerJoin<PaymentMethodAccount, On<PaymentMethodAccount.paymentMethodID, Equal<PaymentMethod.paymentMethodID>, And<PaymentMethodAccount.cashAccountID, Equal<Current<CABankTran.cashAccountID>>, And<Where2<Where<Current<CABankTran.origModule>, Equal<BatchModule.moduleAP>, And<PaymentMethodAccount.useForAP, Equal<True>>>, Or<Where<Current<CABankTran.origModule>, Equal<BatchModule.moduleAR>, And<PaymentMethodAccount.useForAR, Equal<True>>>>>>>>>, Where<PaymentMethod.isActive, Equal<boolTrue>, And<Where2<Where<Current<CABankTran.origModule>, Equal<BatchModule.moduleAP>, And<PaymentMethod.useForAP, Equal<True>>>, Or<Where<Current<CABankTran.origModule>, Equal<BatchModule.moduleAR>, And<PaymentMethod.useForAR, Equal<True>>>>>>>>), DescriptionField = typeof (PaymentMethod.descr))]
  [PXUIField(DisplayName = "Payment Method", Visible = true)]
  public virtual string PaymentMethodIDCopy
  {
    get => this.PaymentMethodID;
    set => this.PaymentMethodID = value;
  }

  /// <summary>
  /// The copy of the <see cref="P:PX.Objects.CA.CABankTran.PMInstanceID" /> field.
  /// This field is displayed on the Match to Invoices tab of on the Process Bank Transactions (CA306000) form.
  /// </summary>
  [PXInt]
  [PXUIField(DisplayName = "Card/Account Nbr.")]
  [PXSelector(typeof (Search<PX.Objects.AR.CustomerPaymentMethod.pMInstanceID, Where<PX.Objects.AR.CustomerPaymentMethod.bAccountID, Equal<Current<CABankTran.payeeBAccountID>>, And<PX.Objects.AR.CustomerPaymentMethod.paymentMethodID, Equal<Current<CABankTran.paymentMethodID>>, And<PX.Objects.AR.CustomerPaymentMethod.isActive, Equal<boolTrue>>>>>), DescriptionField = typeof (PX.Objects.AR.CustomerPaymentMethod.descr))]
  public virtual int? PMInstanceIDCopy
  {
    get => this.PMInstanceID;
    set => this.PMInstanceID = value;
  }

  /// <summary>
  /// The identifier of the rule that was applied to the bank transaction to create a document.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.CA.CABankTranRule.RuleID" /> field.
  /// </value>
  [PXDBInt]
  [PXSelector(typeof (CABankTranRule.ruleID), DescriptionField = typeof (CABankTranRule.description))]
  [PXUIField(DisplayName = "Applied Rule", Enabled = false)]
  public int? RuleID { get; set; }

  /// <summary>
  /// Specifies (if set to <c>true</c>) that this bank transaction has been hidden from the statement on the Process Bank Transactions (CA306000) form.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Hidden", Enabled = false)]
  public virtual bool? Hidden { get; set; }

  /// <summary>
  /// Specifies (if set to <c>true</c>) that the invoice for matching to this bank transaction wasn't found.
  /// </summary>
  [PXDBBool]
  public bool? InvoiceNotFound { get; set; }

  /// <summary>
  /// The count of matched payments.
  /// This is a virtual field and it has no representation in the database.
  /// </summary>
  [PXInt]
  [PXUIField(Visible = false, Enabled = false)]
  public virtual int? CountMatches { get; set; }

  /// <summary>
  /// The count of matched invoices.
  /// This is a virtual field and it has no representation in the database.
  /// </summary>
  [PXInt]
  [PXUIField(Visible = false, Enabled = false)]
  public virtual int? CountInvoiceMatches { get; set; }

  /// <summary>
  /// The count of matched expense receipts.
  /// This is a virtual field and it has no representation in the database.
  /// </summary>
  [PXInt]
  [PXUIField(Visible = false, Enabled = false)]
  public virtual int? CountExpenseReceiptDetailMatches { get; set; }

  /// <summary>
  /// The user-friendly brief description of the status of the selected transaction.
  /// The field is displayed in the bottom of the table with bank transactions on the Process Bank Transactions (CA306000) form.
  /// This is a virtual field and it has no representation in the database.
  /// </summary>
  [PXString]
  [PXUIField]
  public virtual string MatchStatsInfo { get; set; }

  /// <summary>
  /// The name of the vendor or customer associated with the document.
  /// </summary>
  [PXInt]
  [PXSelector(typeof (Search<BAccountR.bAccountID>), SubstituteKey = typeof (BAccountR.acctName))]
  [PXUIField]
  public virtual int? AcctName
  {
    get => this.PayeeBAccountID;
    set => this.PayeeBAccountID = value;
  }

  /// <summary>
  /// The copy of the <see cref="P:PX.Objects.CA.CABankTran.PayeeBAccountID" /> field.
  /// This field is displayed on the Match to Payments tab of on the Process Bank Transactions (CA306000) form.
  /// </summary>
  [PXInt]
  [PXDefault]
  [PXSelector(typeof (Search<BAccountR.bAccountID>), SubstituteKey = typeof (BAccountR.acctCD), DescriptionField = typeof (BAccountR.acctName))]
  [PXUIField]
  public virtual int? PayeeBAccountID1
  {
    get => this.PayeeBAccountID;
    set => this.PayeeBAccountID = value;
  }

  /// <summary>
  /// The copy of the <see cref="P:PX.Objects.CA.CABankTran.PayeeLocationID" /> field.
  /// This field is displayed on the Match to Payments tab of on the Process Bank Transactions (CA306000) form.
  /// </summary>
  [PXInt]
  [PXSelector(typeof (Search<PX.Objects.CR.Location.locationID, Where<PX.Objects.CR.Location.bAccountID, Equal<Current<CABankTran.payeeBAccountID>>>>), SubstituteKey = typeof (PX.Objects.CR.Location.locationCD), DescriptionField = typeof (PX.Objects.CR.Location.descr))]
  [PXUIField]
  [PXDefault(typeof (Search<BAccountR.defLocationID, Where<BAccountR.bAccountID, Equal<Current<CABankTran.payeeBAccountID>>>>))]
  public virtual int? PayeeLocationID1
  {
    get => this.PayeeLocationID;
    set => this.PayeeLocationID = value;
  }

  /// <summary>
  /// The copy of the <see cref="P:PX.Objects.CA.CABankTran.PaymentMethodID" /> field.
  /// This field is displayed on the Match to Payments tab of on the Process Bank Transactions (CA306000) form.
  /// </summary>
  [PXString(10, IsUnicode = true)]
  [PXDefault(typeof (Coalesce<Coalesce<Search2<PX.Objects.AR.Customer.defPaymentMethodID, InnerJoin<PaymentMethod, On<PaymentMethod.paymentMethodID, Equal<PX.Objects.AR.Customer.defPaymentMethodID>, And<PaymentMethod.useForAR, Equal<True>>>, InnerJoin<PaymentMethodAccount, On<PaymentMethodAccount.paymentMethodID, Equal<PX.Objects.AR.Customer.defPaymentMethodID>, And<PaymentMethodAccount.useForAR, Equal<True>, And<PaymentMethodAccount.cashAccountID, Equal<Current<CABankTran.cashAccountID>>>>>>>, Where<Current<CABankTran.origModule>, Equal<BatchModule.moduleAR>, And<PX.Objects.AR.Customer.bAccountID, Equal<Current<CABankTran.payeeBAccountID>>>>>, Search2<PaymentMethodAccount.paymentMethodID, InnerJoin<PaymentMethod, On<PaymentMethodAccount.paymentMethodID, Equal<PaymentMethod.paymentMethodID>, And<PaymentMethodAccount.cashAccountID, Equal<Current<CABankTran.cashAccountID>>, And<PaymentMethodAccount.useForAR, Equal<True>>>>>, Where<Current<CABankTran.origModule>, Equal<BatchModule.moduleAR>, And<PaymentMethod.useForAR, Equal<True>, And<PaymentMethod.isActive, Equal<boolTrue>>>>, OrderBy<Asc<PaymentMethodAccount.aRIsDefault, Desc<PaymentMethodAccount.paymentMethodID>>>>>, Coalesce<Search2<PX.Objects.CR.Location.vPaymentMethodID, InnerJoin<PX.Objects.AP.Vendor, On<PX.Objects.CR.Location.bAccountID, Equal<PX.Objects.AP.Vendor.bAccountID>, And<PX.Objects.CR.Location.locationID, Equal<PX.Objects.AP.Vendor.defLocationID>>>, InnerJoin<PaymentMethodAccount, On<PaymentMethodAccount.paymentMethodID, Equal<PX.Objects.CR.Location.vPaymentMethodID>, And<PaymentMethodAccount.useForAP, Equal<True>, And<PaymentMethodAccount.cashAccountID, Equal<Current<CABankTran.cashAccountID>>>>>>>, Where<Current<CABankTran.origModule>, Equal<BatchModule.moduleAP>, And<PX.Objects.AP.Vendor.bAccountID, Equal<Current<CABankTran.payeeBAccountID>>>>>, Search2<PaymentMethodAccount.paymentMethodID, InnerJoin<PaymentMethod, On<PaymentMethodAccount.paymentMethodID, Equal<PaymentMethod.paymentMethodID>, And<PaymentMethodAccount.cashAccountID, Equal<Current<CABankTran.cashAccountID>>, And<PaymentMethodAccount.useForAP, Equal<True>>>>>, Where<Current<CABankTran.origModule>, Equal<BatchModule.moduleAP>, And<PaymentMethod.useForAP, Equal<True>, And<PaymentMethod.isActive, Equal<boolTrue>>>>, OrderBy<Asc<PaymentMethodAccount.aPIsDefault, Desc<PaymentMethodAccount.paymentMethodID>>>>>>))]
  [PXSelector(typeof (Search2<PaymentMethod.paymentMethodID, InnerJoin<PaymentMethodAccount, On<PaymentMethodAccount.paymentMethodID, Equal<PaymentMethod.paymentMethodID>, And<PaymentMethodAccount.cashAccountID, Equal<Current<CABankTran.cashAccountID>>, And<Where2<Where<Current<CABankTran.origModule>, Equal<BatchModule.moduleAP>, And<PaymentMethodAccount.useForAP, Equal<True>>>, Or<Where<Current<CABankTran.origModule>, Equal<BatchModule.moduleAR>, And<PaymentMethodAccount.useForAR, Equal<True>>>>>>>>>, Where<PaymentMethod.isActive, Equal<boolTrue>, And<Where2<Where<Current<CABankTran.origModule>, Equal<BatchModule.moduleAP>, And<PaymentMethod.useForAP, Equal<True>>>, Or<Where<Current<CABankTran.origModule>, Equal<BatchModule.moduleAR>, And<PaymentMethod.useForAR, Equal<True>>>>>>>>), DescriptionField = typeof (PaymentMethod.descr))]
  [PXUIField(DisplayName = "Payment Method", Visible = false)]
  public virtual string PaymentMethodID1
  {
    get => this.PaymentMethodID;
    set => this.PaymentMethodID = value;
  }

  /// <summary>
  /// The copy of the <see cref="P:PX.Objects.CA.CABankTran.InvoiceInfo" /> field.
  /// This field is displayed on the Match to Payments tab of on the Process Bank Transactions (CA306000) form.
  /// </summary>
  [PXString(256 /*0x0100*/, IsUnicode = true)]
  [PXUIField]
  public virtual string InvoiceInfo1
  {
    get => this.InvoiceInfo;
    set => this.InvoiceInfo = value;
  }

  /// <summary>
  /// The copy of the <see cref="P:PX.Objects.CA.CABankTran.EntryTypeID" /> field.
  /// This field is displayed on the Match to Payments tab of on the Process Bank Transactions (CA306000) form.
  /// </summary>
  [PXString(10, IsUnicode = true)]
  [PXDefault]
  [PXSelector(typeof (Search2<CAEntryType.entryTypeId, InnerJoin<CashAccountETDetail, On<CashAccountETDetail.entryTypeID, Equal<CAEntryType.entryTypeId>>>, Where<CashAccountETDetail.cashAccountID, Equal<Current<CABankTran.cashAccountID>>, And<CAEntryType.module, Equal<BatchModule.moduleCA>, And<Where<CAEntryType.drCr, Equal<Current<CABankTran.drCr>>>>>>>), DescriptionField = typeof (CAEntryType.descr))]
  [PXUIField]
  public virtual string EntryTypeID1
  {
    get => this.EntryTypeID;
    set => this.EntryTypeID = value;
  }

  /// <summary>
  /// The copy of the <see cref="P:PX.Objects.CA.CABankTran.OrigModule" /> field.
  /// This field is displayed on the Match to Payments tab of on the Process Bank Transactions (CA306000) form.
  /// </summary>
  [PXString(2, IsFixed = true)]
  [PXStringList(new string[] {"AP", "AR", "CA"}, new string[] {"AP", "AR", "CA"})]
  [PXUIField]
  [PXDefault]
  public virtual string OrigModule1
  {
    get => this.OrigModule;
    set => this.OrigModule = value;
  }

  /// <summary>
  /// The total amount of write-offs specified for documents to be applied in the selected currency.
  /// This field is displayed if the <c>"AR"</c> option is selected in the <see cref="P:PX.Objects.CA.CABankTran.OrigModule" /> field.
  /// </summary>
  [PXCurrency(typeof (CABankTran.curyInfoID), typeof (CABankTran.wOAmt))]
  [PXUIField]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryWOAmt { get; set; }

  /// <summary>
  /// The total amount of write-offs specified for documents to be applied in the base currency.
  /// </summary>
  [PXDecimal(4)]
  public virtual Decimal? WOAmt { get; set; }

  [PXUIField(DisplayName = "Card Number")]
  [PXDBString(25)]
  public string CardNumber { get; set; }

  [Obsolete("The field is obsoleted, use CountAdjustments instead")]
  [PXBool]
  [PXDBCalced(typeof (Switch<Case<Where<Exists<Select<CABankTranAdjustment, Where<CABankTranAdjustment.tranID, Equal<CABankTran.tranID>>>>>, True>, False>), typeof (bool))]
  public virtual bool? HasAdjustments { get; set; }

  [PXDBInt]
  public virtual int? CountAdjustments { get; set; }

  [PXInt]
  public int? SortOrder { get; set; }

  /// <exclude />
  [PXDBString(2)]
  [CABankTranOperations.List]
  [PXUIField]
  [PXDefault("AA")]
  public virtual string AllowedOperations { get; set; }

  [PXDBString(1)]
  [CABankTran.matchReason.List]
  [PXUIField(DisplayName = "Match Reason")]
  public virtual string MatchReason { get; set; }

  [PXDBDate(PreserveTime = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Last Auto-Match Date")]
  public virtual DateTime? LastAutoMatchDate { get; set; }

  [PXNote]
  public virtual Guid? NoteID { get; set; }

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

  public int? BAccountID
  {
    get => this.PayeeBAccountID;
    set => this.PayeeBAccountID = value;
  }

  public int? LocationID
  {
    get => this.PayeeLocationID;
    set => this.PayeeLocationID = value;
  }

  [PXBool]
  public bool? Cleared => new bool?(true);

  [PXDate]
  public virtual DateTime? ClearDate => this.TranDate;

  public int? CARefTranAccountID => new int?();

  public long? CARefTranID => new long?();

  public int? CARefSplitLineNbr => new int?();

  public Decimal? CuryOrigDocAmt
  {
    [PXDependsOnFields(new System.Type[] {typeof (CABankTran.curyTranAmt)})] get
    {
      if (!this.CuryTranAmt.HasValue)
        return new Decimal?();
      if (!(this.CuryTranAmt.Value != 0M))
        return new Decimal?(0M);
      Decimal? curyTranAmt = this.CuryTranAmt;
      Decimal num = (Decimal) Math.Sign(this.CuryTranAmt.Value);
      return !curyTranAmt.HasValue ? new Decimal?() : new Decimal?(curyTranAmt.GetValueOrDefault() * num);
    }
  }

  long? ICADocSource.CuryInfoID => new long?();

  string ICADocSource.FinPeriodID => this.MatchingFinPeriodID;

  string ICADocSource.InvoiceNbr => this.InvoiceInfo;

  string ICADocSource.TranDesc => this.UserDesc;

  public virtual string GetFriendlyKeyImage(PXCache cache)
  {
    return $"{PXUIFieldAttribute.GetDisplayName<CABankTran.headerRefNbr>(cache)}: {this.HeaderRefNbr}, {PXUIFieldAttribute.GetDisplayName<CABankTran.tranID>(cache)}: {this.TranID}";
  }

  public class PK : PrimaryKeyOf<CABankTran>.By<CABankTran.tranID>
  {
    public static CABankTran Find(PXGraph graph, int? tranID, PKFindOptions options = 0)
    {
      return (CABankTran) PrimaryKeyOf<CABankTran>.By<CABankTran.tranID>.FindBy(graph, (object) tranID, options);
    }
  }

  public static class FK
  {
    public class CashAcccount : 
      PrimaryKeyOf<CashAccount>.By<CashAccount.cashAccountID>.ForeignKeyOf<CABankTran>.By<CABankTran.cashAccountID>
    {
    }

    public class Currency : 
      PrimaryKeyOf<PX.Objects.CM.Currency>.By<PX.Objects.CM.Currency.curyID>.ForeignKeyOf<CABankTran>.By<CABankTran.curyID>
    {
    }

    public class CurrencyInfo : 
      PrimaryKeyOf<PX.Objects.CM.CurrencyInfo>.By<PX.Objects.CM.CurrencyInfo.curyInfoID>.ForeignKeyOf<CABankTran>.By<CABankTran.curyInfoID>
    {
    }

    public class MatchingCurrencyInfo : 
      PrimaryKeyOf<PX.Objects.CM.CurrencyInfo>.By<PX.Objects.CM.CurrencyInfo.curyInfoID>.ForeignKeyOf<CABankTran>.By<CABankTran.origCuryID>
    {
    }

    public class PayeeBusinessAccount : 
      PrimaryKeyOf<PX.Objects.CR.BAccount>.By<PX.Objects.CR.BAccount.bAccountID>.ForeignKeyOf<CABankTran>.By<CABankTran.payeeBAccountID>
    {
    }

    public class PayeeLocation : 
      PrimaryKeyOf<PX.Objects.CR.Location>.By<PX.Objects.CR.Location.bAccountID, PX.Objects.CR.Location.locationID>.ForeignKeyOf<CABankTran>.By<CABankTran.payeeBAccountID, CABankTran.payeeLocationID>
    {
    }

    public class PaymentMethod : 
      PrimaryKeyOf<PaymentMethod>.By<PaymentMethod.paymentMethodID>.ForeignKeyOf<CABankTran>.By<CABankTran.paymentMethodID>
    {
    }

    public class CustomerPaymentMethod : 
      PrimaryKeyOf<PX.Objects.AR.CustomerPaymentMethod>.By<PX.Objects.AR.CustomerPaymentMethod.pMInstanceID>.ForeignKeyOf<CABankTran>.By<CABankTran.pMInstanceID>
    {
    }

    public class MatchingEntryType : 
      PrimaryKeyOf<CAEntryType>.By<CAEntryType.entryTypeId>.ForeignKeyOf<CABankTran>.By<CABankTran.entryTypeID>
    {
    }

    public class TransactionRule : 
      PrimaryKeyOf<CABankTranRule>.By<CABankTranRule.ruleID>.ForeignKeyOf<CABankTran>.By<CABankTran.ruleID>
    {
    }

    public class BankStatement : 
      PrimaryKeyOf<CABankTranHeader>.By<CABankTranHeader.cashAccountID, CABankTranHeader.refNbr, CABankTranHeader.tranType>.ForeignKeyOf<CABankTran>.By<CABankTran.cashAccountID, CABankTran.headerRefNbr, CABankTran.tranType>
    {
    }
  }

  public abstract class cashAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CABankTran.cashAccountID>
  {
  }

  public abstract class tranID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CABankTran.tranID>
  {
  }

  public abstract class tranType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CABankTran.tranType>
  {
  }

  public abstract class headerRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CABankTran.headerRefNbr>
  {
  }

  public abstract class extTranID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CABankTran.extTranID>
  {
  }

  public abstract class drCr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CABankTran.drCr>
  {
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CABankTran.curyID>
  {
  }

  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  CABankTran.curyInfoID>
  {
  }

  public abstract class tranDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  CABankTran.tranDate>
  {
  }

  public abstract class matchingPaymentDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CABankTran.matchingPaymentDate>
  {
  }

  public abstract class matchingfinPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CABankTran.matchingfinPeriodID>
  {
  }

  public abstract class tranPeriodID : IBqlField, IBqlOperand
  {
  }

  public abstract class tranEntryDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CABankTran.tranEntryDate>
  {
  }

  public abstract class curyTranAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CABankTran.curyTranAmt>
  {
  }

  public abstract class origCuryID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CABankTran.origCuryID>
  {
  }

  public abstract class extRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CABankTran.extRefNbr>
  {
  }

  public abstract class tranDesc : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CABankTran.tranDesc>
  {
  }

  public abstract class userDesc : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CABankTran.userDesc>
  {
  }

  public abstract class payeeName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CABankTran.payeeName>
  {
  }

  public abstract class payeeAddress1 : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CABankTran.payeeAddress1>
  {
  }

  public abstract class payeeCity : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CABankTran.payeeCity>
  {
  }

  public abstract class payeeState : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CABankTran.payeeState>
  {
  }

  public abstract class payeePostalCode : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CABankTran.payeePostalCode>
  {
  }

  public abstract class payeePhone : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CABankTran.payeePhone>
  {
  }

  public abstract class tranCode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CABankTran.tranCode>
  {
  }

  public abstract class origModule : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CABankTran.origModule>
  {
  }

  public abstract class payeeBAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CABankTran.payeeBAccountID>
  {
  }

  public abstract class payeeLocationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CABankTran.payeeLocationID>
  {
  }

  public abstract class paymentMethodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CABankTran.paymentMethodID>
  {
  }

  public abstract class pMInstanceID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CABankTran.pMInstanceID>
  {
  }

  public abstract class invoiceInfo : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CABankTran.invoiceInfo>
  {
  }

  public abstract class documentMatched : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CABankTran.documentMatched>
  {
  }

  public abstract class ruleApplied : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CABankTran.ruleApplied>
  {
  }

  public abstract class applyRuleEnabled : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CABankTran.applyRuleEnabled>
  {
  }

  public abstract class matchedToExisting : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CABankTran.matchedToExisting>
  {
  }

  public abstract class matchedToInvoice : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CABankTran.matchedToInvoice>
  {
  }

  public abstract class histMatchedToInvoice : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CABankTran.histMatchedToInvoice>
  {
  }

  public abstract class matchedToExpenseReceipt : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CABankTran.matchedToExpenseReceipt>
  {
  }

  public abstract class createDocument : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CABankTran.createDocument>
  {
  }

  public abstract class multipleMatching : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CABankTran.multipleMatching>
  {
  }

  public abstract class multipleMatchingToPayments : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CABankTran.multipleMatchingToPayments>
  {
  }

  public abstract class matchReceiptsAndDisbursements : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CABankTran.matchReceiptsAndDisbursements>
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CABankTran.status>
  {
  }

  public abstract class processed : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CABankTran.processed>
  {
  }

  public abstract class entryTypeID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CABankTran.entryTypeID>
  {
  }

  public abstract class taxZoneID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CABankTran.taxZoneID>
  {
  }

  public abstract class taxCalcMode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CABankTran.taxCalcMode>
  {
  }

  public abstract class chargeTypeID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CABankTran.chargeTypeID>
  {
  }

  public abstract class chargeTaxZoneID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CABankTran.chargeTaxZoneID>
  {
  }

  public abstract class chargeTaxCalcMode : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CABankTran.chargeTaxCalcMode>
  {
  }

  public abstract class chargeDrCr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CABankTran.chargeDrCr>
  {
  }

  public abstract class curyDebitAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CABankTran.curyDebitAmt>
  {
  }

  public abstract class curyCreditAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CABankTran.curyCreditAmt>
  {
  }

  public abstract class curyTotalAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CABankTran.curyTotalAmt>
  {
  }

  public abstract class curyTotalAmtCopy : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CABankTran.curyTotalAmtCopy>
  {
  }

  public abstract class curyDetailsWithTaxesTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CABankTran.curyDetailsWithTaxesTotal>
  {
  }

  public abstract class detailsWithTaxesTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CABankTran.detailsWithTaxesTotal>
  {
  }

  public abstract class curyTaxTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CABankTran.curyTaxTotal>
  {
  }

  public abstract class taxTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CABankTran.taxTotal>
  {
  }

  public abstract class curyTotalAmtDisplay : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CABankTran.curyTotalAmtDisplay>
  {
  }

  public abstract class curyApplAmtCA : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CABankTran.curyApplAmtCA>
  {
  }

  public abstract class curyUnappliedBalCA : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CABankTran.curyUnappliedBalCA>
  {
  }

  public abstract class unappliedBalCA : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CABankTran.unappliedBalCA>
  {
  }

  public abstract class curyApplAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CABankTran.curyApplAmt>
  {
  }

  public abstract class curyUnappliedBal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CABankTran.curyUnappliedBal>
  {
  }

  public abstract class curyApplAmtMatch : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CABankTran.curyApplAmtMatch>
  {
  }

  public abstract class curyApplAmtMatchToInvoice : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CABankTran.curyApplAmtMatchToInvoice>
  {
  }

  public abstract class curyApplAmtMatchToPayment : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CABankTran.curyApplAmtMatchToPayment>
  {
  }

  public abstract class curyUnappliedBalMatch : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CABankTran.curyUnappliedBalMatch>
  {
  }

  public abstract class curyVatExemptTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CABankTran.curyVatExemptTotal>
  {
  }

  public abstract class vatExemptTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CABankTran.vatExemptTotal>
  {
  }

  public abstract class curyVatTaxableTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CABankTran.curyVatTaxableTotal>
  {
  }

  public abstract class vatTaxableTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CABankTran.vatTaxableTotal>
  {
  }

  public abstract class curyTaxRoundDiff : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CABankTran.curyTaxRoundDiff>
  {
  }

  public abstract class taxRoundDiff : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CABankTran.taxRoundDiff>
  {
  }

  public abstract class curyChargeAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CABankTran.curyChargeAmt>
  {
  }

  public abstract class curyChargeTaxAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CABankTran.curyChargeTaxAmt>
  {
  }

  public abstract class curyUnappliedBalMatchToInvoice : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CABankTran.curyUnappliedBalMatchToInvoice>
  {
  }

  public abstract class curyUnappliedBalMatchToPayment : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CABankTran.curyUnappliedBalMatchToPayment>
  {
  }

  public abstract class docType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CABankTran.docType>
  {
  }

  public abstract class lineCntr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CABankTran.lineCntr>
  {
  }

  public abstract class lineCntrCA : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CABankTran.lineCntrCA>
  {
  }

  public abstract class lineCntrMatch : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CABankTran.lineCntrMatch>
  {
  }

  public abstract class detailErrorCount : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CABankTran.detailErrorCount>
  {
  }

  public abstract class payeeBAccountIDCopy : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CABankTran.payeeBAccountIDCopy>
  {
  }

  public abstract class payeeLocationIDCopy : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CABankTran.payeeLocationIDCopy>
  {
  }

  public abstract class paymentMethodIDCopy : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CABankTran.paymentMethodIDCopy>
  {
  }

  public abstract class pMInstanceIDCopy : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CABankTran.pMInstanceIDCopy>
  {
  }

  public abstract class ruleID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CABankTran.ruleID>
  {
  }

  public abstract class hidden : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CABankTran.hidden>
  {
  }

  public abstract class invoiceNotFound : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CABankTran.invoiceNotFound>
  {
  }

  public abstract class countMatches : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CABankTran.countMatches>
  {
  }

  public abstract class countInvoiceMatches : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CABankTran.countInvoiceMatches>
  {
  }

  public abstract class countExpenseReceiptDetailMatches : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CABankTran.countExpenseReceiptDetailMatches>
  {
  }

  public abstract class matchStatsInfo : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CABankTran.matchStatsInfo>
  {
  }

  public abstract class acctName : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CABankTran.acctName>
  {
  }

  public abstract class payeeBAccountID1 : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CABankTran.payeeBAccountID1>
  {
  }

  public abstract class payeeLocationID1 : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CABankTran.payeeLocationID1>
  {
  }

  public abstract class paymentMethodID1 : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CABankTran.paymentMethodID1>
  {
  }

  public abstract class invoiceInfo1 : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CABankTran.invoiceInfo1>
  {
  }

  public abstract class entryTypeID1 : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CABankTran.entryTypeID1>
  {
  }

  public abstract class origModule1 : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CABankTran.origModule1>
  {
  }

  public abstract class curyWOAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CABankTran.curyWOAmt>
  {
  }

  public abstract class wOAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  CABankTran.wOAmt>
  {
  }

  public abstract class cardNumber : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CABankTran.cardNumber>
  {
  }

  [Obsolete("The field is obsoleted, use CountAdjustments instead")]
  public abstract class hasAdjustments : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CABankTran.hasAdjustments>
  {
  }

  public abstract class countAdjustments : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CABankTran.countAdjustments>
  {
  }

  public abstract class sortOrder : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CABankTran.sortOrder>
  {
  }

  public abstract class allowedOperations : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CABankTran.allowedOperations>
  {
  }

  public abstract class matchReason : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CABankTran.matchReason>
  {
    public const string AutoMatch = "A";
    public const string Manual = "M";

    public class ListAttribute : PXStringListAttribute
    {
      public ListAttribute()
        : base(new string[2]{ "A", "M" }, new string[2]
        {
          "Auto-Match",
          "Matched Manually"
        })
      {
      }
    }

    public class autoMatch : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    CABankTran.matchReason.autoMatch>
    {
      public autoMatch()
        : base("A")
      {
      }
    }

    public class manual : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    CABankTran.matchReason.manual>
    {
      public manual()
        : base("M")
      {
      }
    }
  }

  public abstract class lastAutoMatchDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CABankTran.lastAutoMatchDate>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CABankTran.noteID>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CABankTran.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CABankTran.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CABankTran.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CABankTran.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CABankTran.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CABankTran.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  CABankTran.Tstamp>
  {
  }

  public abstract class cleared : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CABankTran.cleared>
  {
  }

  public abstract class clearDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  CABankTran.clearDate>
  {
  }
}
