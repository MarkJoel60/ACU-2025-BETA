// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.BankFeedTransaction
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.CA;

[PXCacheName("BankFeedTransaction")]
public class BankFeedTransaction : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXString(IsKey = true)]
  [PXUIField(DisplayName = "Transaction ID", Visible = false)]
  public 
  #nullable disable
  string TransactionID { get; set; }

  [PXString]
  [PXUIField(DisplayName = "Type", Visible = false)]
  public string Type { get; set; }

  [PXString]
  [PXUIField(DisplayName = "Account Owner", Visible = false)]
  public string AccountOwner { get; set; }

  [PXString]
  [PXUIField(DisplayName = "Pending Transaction ID", Visible = false)]
  public string PendingTransactionID { get; set; }

  [PXBool]
  [PXUIField(DisplayName = "Pending", Visible = false)]
  public bool? Pending { get; set; }

  [PXDate]
  [PXUIField(DisplayName = "Date", Visible = false)]
  public DateTime? Date { get; set; }

  [PXString]
  [PXUIField(DisplayName = "Currency Code", Visible = false)]
  public string IsoCurrencyCode { get; set; }

  [PXDecimal]
  [PXUIField(DisplayName = "Amount", Visible = false)]
  public Decimal? Amount { get; set; }

  [PXString]
  [PXUIField(DisplayName = "Account ID", Visible = false)]
  public string AccountID { get; set; }

  [PXString]
  [PXUIField(DisplayName = "Name", Visible = false)]
  public string Name { get; set; }

  [PXString]
  [PXUIField(DisplayName = "Category", Visible = false)]
  public string Category { get; set; }

  [PXString]
  [PXUIField(DisplayName = "Check Number", Visible = false)]
  public string CheckNumber { get; set; }

  [PXString]
  [PXUIField(DisplayName = "Memo", Visible = false)]
  public string Memo { get; set; }

  /// <summary>Contains data from service's field created_at</summary>
  [PXDateAndTime]
  [PXUIField(DisplayName = "Created At", Visible = false)]
  public DateTime? CreatedAt { get; set; }

  /// <summary>Contains data from service's field posted_at</summary>
  [PXDateAndTime]
  [PXUIField(DisplayName = "Posted At", Visible = false)]
  public DateTime? PostedAt { get; set; }

  /// <summary>Contains data from service's field transacted_at</summary>
  [PXDateAndTime]
  [PXUIField(DisplayName = "Transacted At", Visible = false)]
  public DateTime? TransactedAt { get; set; }

  /// <summary>Contains data from service's field updated_at</summary>
  [PXDateAndTime]
  [PXUIField(DisplayName = "Updated At", Visible = false)]
  public DateTime? UpdatedAt { get; set; }

  /// <summary>Contains data from service's field account_id</summary>
  [PXString]
  [PXUIField(DisplayName = "Account String ID", Visible = false)]
  public string AccountStringId { get; set; }

  /// <summary>Contains data from service's field category_guid</summary>
  [PXString]
  [PXUIField(DisplayName = "Category GUID", Visible = false)]
  public string CategoryGuid { get; set; }

  /// <summary>
  /// Contains data from service's field extended_transaction_type
  /// </summary>
  [PXString]
  [PXUIField(DisplayName = "Extended Transaction Type", Visible = false)]
  public string ExtendedTransactionType { get; set; }

  /// <summary>Contains data from service's field id</summary>
  [PXString]
  [PXUIField(DisplayName = "ID", Visible = false)]
  public string Id { get; set; }

  /// <summary>Contains data from service's field is_bill_pay</summary>
  [PXBool]
  [PXUIField(DisplayName = "Is Bill Pay", Visible = false)]
  public bool? IsBillPay { get; set; }

  /// <summary>Contains data from service's field is_direct_deposit</summary>
  [PXBool]
  [PXUIField(DisplayName = "Is Direct Deposit", Visible = false)]
  public bool? IsDirectDeposit { get; set; }

  /// <summary>Contains data from service's field is_expense</summary>
  [PXBool]
  [PXUIField(DisplayName = "Is Expense", Visible = false)]
  public bool? IsExpense { get; set; }

  /// <summary>Contains data from service's field is_fee</summary>
  [PXBool]
  [PXUIField(DisplayName = "Is Fee", Visible = false)]
  public bool? IsFee { get; set; }

  /// <summary>Contains data from service's field is_income</summary>
  [PXBool]
  [PXUIField(DisplayName = "Is Income", Visible = false)]
  public bool? IsIncome { get; set; }

  /// <summary>Contains data from service's field is_international</summary>
  [PXBool]
  [PXUIField(DisplayName = "Is International", Visible = false)]
  public bool? IsInternational { get; set; }

  /// <summary>Contains data from service's field is_overdraft_fee</summary>
  [PXBool]
  [PXUIField(DisplayName = "Is Overdraft Fee", Visible = false)]
  public bool? IsOverdraftFee { get; set; }

  /// <summary>Contains data from service's field is_payroll_advance</summary>
  [PXBool]
  [PXUIField(DisplayName = "Is Payroll Advance", Visible = false)]
  public bool? IsPayrollAdvance { get; set; }

  /// <summary>Contains data from service's field is_recurring</summary>
  [PXBool]
  [PXUIField(DisplayName = "Is Recurring", Visible = false)]
  public bool? IsRecurring { get; set; }

  /// <summary>Contains data from service's field is_subcription</summary>
  [PXBool]
  [PXUIField(DisplayName = "Is Subscription", Visible = false)]
  public bool? IsSubscription { get; set; }

  /// <summary>Contains data from service's field latitude</summary>
  [PXDecimal]
  [PXUIField(DisplayName = "Latitude", Visible = false)]
  public Decimal? Latitude { get; set; }

  /// <summary>
  /// Contains data from service's field localized_description
  /// </summary>
  [PXString]
  [PXUIField(DisplayName = "Localized Description", Visible = false)]
  public string LocalizedDescription { get; set; }

  /// <summary>Contains data from service's field localized_memo</summary>
  [PXString]
  [PXUIField(DisplayName = "Localized Memo", Visible = false)]
  public string LocalizedMemo { get; set; }

  /// <summary>Contains data from service's field longitude</summary>
  [PXDecimal]
  [PXUIField(DisplayName = "Longitude", Visible = false)]
  public Decimal? Longitude { get; set; }

  /// <summary>
  /// Contains data from service's field member_is_managed_by_user
  /// </summary>
  [PXBool]
  [PXUIField(DisplayName = "Member Is Managed By User", Visible = false)]
  public bool? MemberIsManagedByUser { get; set; }

  /// <summary>
  /// Contains data from service's field merchant_category_code
  /// </summary>
  [PXInt]
  [PXUIField(DisplayName = "Merchant Category Code", Visible = false)]
  public int? MerchantCategoryCode { get; set; }

  /// <summary>Contains data from service's field merchant_guid</summary>
  [PXString]
  [PXUIField(DisplayName = "Merchant GUID", Visible = false)]
  public string MerchantGuid { get; set; }

  /// <summary>
  /// Contains data from service's field merchant_location_guid
  /// </summary>
  [PXString]
  [PXUIField(DisplayName = "Merchant Location GUID", Visible = false)]
  public string MerchantLocationGuid { get; set; }

  /// <summary>Contains data from service's field metadata</summary>
  [PXString]
  [PXUIField(DisplayName = "Metadata", Visible = false)]
  public string Metadata { get; set; }

  /// <summary>
  /// Contains data from service's field original_description
  /// </summary>
  [PXString]
  [PXUIField(DisplayName = "Original Description", Visible = false)]
  public string OriginalDescription { get; set; }

  /// <summary>Contains data from service's field user_id</summary>
  [PXString]
  [PXUIField(DisplayName = "User ID", Visible = false)]
  public string UserId { get; set; }

  /// <summary>Contains data from service's field authorized_date</summary>
  [PXDate]
  [PXUIField(DisplayName = "Authorized Date", Visible = false)]
  public DateTime? AuthorizedDate { get; set; }

  /// <summary>
  /// Contains data from service's field authorized_datetime
  /// </summary>
  [PXDate]
  [PXUIField(DisplayName = "Authorized Datetime", Visible = false)]
  public DateTime? AuthorizedDatetime { get; set; }

  /// <summary>Contains data from service's field datetime</summary>
  [PXDateAndTime]
  [PXUIField(DisplayName = "Datetime Value", Visible = false)]
  public DateTime? DatetimeValue { get; set; }

  /// <summary>Contains data from service's field address</summary>
  [PXString]
  [PXUIField(DisplayName = "Address", Visible = false)]
  public string Address { get; set; }

  /// <summary>Contains data from service's field city</summary>
  [PXString]
  [PXUIField(DisplayName = "City", Visible = false)]
  public string City { get; set; }

  /// <summary>Contains data from service's field country</summary>
  [PXString]
  [PXUIField(DisplayName = "Country", Visible = false)]
  public string Country { get; set; }

  /// <summary>Contains data from service's field postal_code</summary>
  [PXString]
  [PXUIField(DisplayName = "Postal Code", Visible = false)]
  public string PostalCode { get; set; }

  /// <summary>Contains data from service's field region</summary>
  [PXString]
  [PXUIField(DisplayName = "Region", Visible = false)]
  public string Region { get; set; }

  /// <summary>Contains data from service's field store_number</summary>
  [PXString]
  [PXUIField(DisplayName = "Store Number", Visible = false)]
  public string StoreNumber { get; set; }

  /// <summary>Contains data from service's field merchant_name</summary>
  [PXString]
  [PXUIField(DisplayName = "Merchant Name", Visible = false)]
  public string MerchantName { get; set; }

  /// <summary>Contains data from service's field payment_channel</summary>
  [PXString]
  [PXUIField(DisplayName = "Payment Channel", Visible = false)]
  public string PaymentChannel { get; set; }

  /// <summary>Contains data from service's field by_order_of</summary>
  [PXString]
  [PXUIField(DisplayName = "By Order Of", Visible = false)]
  public string ByOrderOf { get; set; }

  /// <summary>Contains data from service's field payee</summary>
  [PXString]
  [PXUIField(DisplayName = "Payee", Visible = false)]
  public string Payee { get; set; }

  /// <summary>Contains data from service's field payer</summary>
  [PXString]
  [PXUIField(DisplayName = "Payer", Visible = false)]
  public string Payer { get; set; }

  /// <summary>Contains data from service's field payment_method</summary>
  [PXString]
  [PXUIField(DisplayName = "Payment Method", Visible = false)]
  public string PaymentMethod { get; set; }

  /// <summary>Contains data from service's field payment_processor</summary>
  [PXString]
  [PXUIField(DisplayName = "Payment Processor", Visible = false)]
  public string PaymentProcessor { get; set; }

  /// <summary>Contains data from service's field ppd_id</summary>
  [PXString]
  [PXUIField(DisplayName = "PPD ID", Visible = false)]
  public string PpdId { get; set; }

  /// <summary>Contains data from service's field reason</summary>
  [PXString]
  [PXUIField(DisplayName = "Reason", Visible = false)]
  public string Reason { get; set; }

  /// <summary>Contains data from service's field reference_number</summary>
  [PXString]
  [PXUIField(DisplayName = "Reference Number", Visible = false)]
  public string ReferenceNumber { get; set; }

  /// <summary>
  /// Contains data from service's field personal_finance_category
  /// </summary>
  [PXString]
  [PXUIField(DisplayName = "Personal Finance Category", Visible = false)]
  public string PersonalFinanceCategory { get; set; }

  /// <summary>Contains data from service's field transaction_code</summary>
  [PXString]
  [PXUIField(DisplayName = "Transaction Code", Visible = false)]
  public string TransactionCode { get; set; }

  /// <summary>
  /// Contains data from service's field unofficial_currency_code
  /// </summary>
  [PXString]
  [PXUIField(DisplayName = "Unofficial Currency Code", Visible = false)]
  public string UnofficialCurrencyCode { get; set; }

  [PXString]
  [PXUIField(DisplayName = "Partner Account ID", Visible = false)]
  public string PartnerAccountId { get; set; }

  /// <summary>Contains data about the card number from the file.</summary>
  [PXString]
  [PXUIField(DisplayName = "Card Number", Visible = false)]
  public string CardNumber { get; set; }

  /// <summary>
  /// Contains data about the external ref. number from the file.
  /// </summary>
  [PXString]
  [PXUIField(DisplayName = "Ext. Ref. Nbr.", Visible = false)]
  public virtual string ExtRefNbr { get; set; }

  /// <summary>
  /// Contains data about the custom transaction description from the file.
  /// </summary>
  [PXString]
  [PXUIField(DisplayName = "Custom Tran. Desc.", Visible = false)]
  public virtual string UserDesc { get; set; }

  /// <summary>Contains data about the invoice number from the file.</summary>
  [PXString]
  [PXUIField(DisplayName = "Invoice Nbr.", Visible = false)]
  public virtual string InvoiceInfo { get; set; }

  /// <summary>Contains data about Payee/Payer from the file.</summary>
  [PXString]
  [PXUIField(DisplayName = "Payee/Payer", Visible = false)]
  public virtual string PayeeName { get; set; }

  /// <summary>
  /// Contains data about the transaction code from the file.
  /// </summary>
  [PXString]
  [PXUIField(DisplayName = "Tran. Code", Visible = false)]
  public virtual string TranCode { get; set; }

  /// <summary>Contains data about the credit amount from the file.</summary>
  [PXDecimal]
  [PXUIField(DisplayName = "Receipt Amount", Visible = false)]
  public Decimal? CreditAmount { get; set; }

  /// <summary>Contains data about the debit amount from the file.</summary>
  [PXDecimal]
  [PXUIField(DisplayName = "Disbursement Amount", Visible = false)]
  public Decimal? DebitAmount { get; set; }

  /// <summary>
  /// Contains data about the debit/credit parameter from the file.
  /// </summary>
  [PXString]
  [PXUIField(DisplayName = "Debit/Credit Property in Separate Column", Visible = false)]
  public virtual string DebitCreditParameter { get; set; }

  /// <summary>Contains data about the account name from the file.</summary>
  [PXString]
  [PXUIField(DisplayName = "Bank Account Name", Visible = false)]
  public virtual string AccountName { get; set; }

  public abstract class transactionID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    BankFeedTransaction.transactionID>
  {
  }

  public abstract class type : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BankFeedTransaction.type>
  {
  }

  public abstract class accountOwner : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    BankFeedTransaction.accountOwner>
  {
  }

  public abstract class pendingTransactionID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    BankFeedTransaction.pendingTransactionID>
  {
  }

  public abstract class pending : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  BankFeedTransaction.pending>
  {
  }

  public abstract class date : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  BankFeedTransaction.date>
  {
  }

  public abstract class isoCurrencyCode : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    BankFeedTransaction.isoCurrencyCode>
  {
  }

  public abstract class amount : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  BankFeedTransaction.amount>
  {
  }

  public abstract class accountID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BankFeedTransaction.accountID>
  {
  }

  public abstract class name : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BankFeedTransaction.name>
  {
  }

  public abstract class category : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BankFeedTransaction.category>
  {
  }

  public abstract class checkNumber : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    BankFeedTransaction.checkNumber>
  {
  }

  public abstract class memo : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BankFeedTransaction.memo>
  {
  }

  public abstract class createdAt : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    BankFeedTransaction.createdAt>
  {
  }

  public abstract class postedAt : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    BankFeedTransaction.postedAt>
  {
  }

  public abstract class transactedAt : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    BankFeedTransaction.transactedAt>
  {
  }

  public abstract class updatedAt : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    BankFeedTransaction.updatedAt>
  {
  }

  public abstract class accountStringId : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    BankFeedTransaction.accountStringId>
  {
  }

  public abstract class categoryGuid : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    BankFeedTransaction.categoryGuid>
  {
  }

  public abstract class extendedTransactionType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    BankFeedTransaction.extendedTransactionType>
  {
  }

  public abstract class id : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BankFeedTransaction.id>
  {
  }

  public abstract class isBillPay : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  BankFeedTransaction.isBillPay>
  {
  }

  public abstract class isDirectDeposit : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    BankFeedTransaction.isDirectDeposit>
  {
  }

  public abstract class isExpense : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  BankFeedTransaction.isExpense>
  {
  }

  public abstract class isFee : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  BankFeedTransaction.isFee>
  {
  }

  public abstract class isIncome : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  BankFeedTransaction.isIncome>
  {
  }

  public abstract class isInternational : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    BankFeedTransaction.isInternational>
  {
  }

  public abstract class isOverdraftFee : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    BankFeedTransaction.isOverdraftFee>
  {
  }

  public abstract class isPayrollAdvance : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    BankFeedTransaction.isPayrollAdvance>
  {
  }

  public abstract class isRecurring : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  BankFeedTransaction.isRecurring>
  {
  }

  public abstract class isSubscription : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    BankFeedTransaction.isSubscription>
  {
  }

  public abstract class latitude : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  BankFeedTransaction.latitude>
  {
  }

  public abstract class localizedDescription : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    BankFeedTransaction.localizedDescription>
  {
  }

  public abstract class localizedMemo : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    BankFeedTransaction.localizedMemo>
  {
  }

  public abstract class longitude : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    BankFeedTransaction.longitude>
  {
  }

  public abstract class memberIsManagedByUser : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    BankFeedTransaction.memberIsManagedByUser>
  {
  }

  public abstract class merchantCategoryCode : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    BankFeedTransaction.merchantCategoryCode>
  {
  }

  public abstract class merchantGuid : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    BankFeedTransaction.merchantGuid>
  {
  }

  public abstract class merchantLocationGuid : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    BankFeedTransaction.merchantLocationGuid>
  {
  }

  public abstract class metadata : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BankFeedTransaction.metadata>
  {
  }

  public abstract class originalDescription : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    BankFeedTransaction.originalDescription>
  {
  }

  public abstract class userId : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BankFeedTransaction.userId>
  {
  }

  public abstract class authorizedDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    BankFeedTransaction.authorizedDate>
  {
  }

  public abstract class authorizedDatetime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    BankFeedTransaction.authorizedDatetime>
  {
  }

  public abstract class datetimeValue : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    BankFeedTransaction.datetimeValue>
  {
  }

  public abstract class address : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BankFeedTransaction.address>
  {
  }

  public abstract class city : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BankFeedTransaction.city>
  {
  }

  public abstract class country : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BankFeedTransaction.country>
  {
  }

  public abstract class postalCode : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    BankFeedTransaction.postalCode>
  {
  }

  public abstract class region : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BankFeedTransaction.region>
  {
  }

  public abstract class storeNumber : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    BankFeedTransaction.storeNumber>
  {
  }

  public abstract class merchantName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    BankFeedTransaction.merchantName>
  {
  }

  public abstract class paymentChannel : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    BankFeedTransaction.paymentChannel>
  {
  }

  public abstract class byOrderOf : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BankFeedTransaction.byOrderOf>
  {
  }

  public abstract class payee : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BankFeedTransaction.payee>
  {
  }

  public abstract class payer : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BankFeedTransaction.payer>
  {
  }

  public abstract class paymentMethod : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    BankFeedTransaction.paymentMethod>
  {
  }

  public abstract class paymentProcessor : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    BankFeedTransaction.paymentProcessor>
  {
  }

  public abstract class ppdId : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BankFeedTransaction.ppdId>
  {
  }

  public abstract class reason : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BankFeedTransaction.reason>
  {
  }

  public abstract class referenceNumber : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    BankFeedTransaction.referenceNumber>
  {
  }

  public abstract class personalFinanceCategory : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    BankFeedTransaction.personalFinanceCategory>
  {
  }

  public abstract class transactionCode : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    BankFeedTransaction.transactionCode>
  {
  }

  public abstract class unofficialCurrencyCode : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    BankFeedTransaction.unofficialCurrencyCode>
  {
  }

  public abstract class partnerAccountID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    BankFeedTransaction.partnerAccountID>
  {
  }

  public abstract class cardNumber : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    BankFeedTransaction.cardNumber>
  {
  }

  public abstract class extRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BankFeedTransaction.extRefNbr>
  {
  }

  public abstract class userDesc : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BankFeedTransaction.userDesc>
  {
  }

  public abstract class invoiceInfo : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    BankFeedTransaction.invoiceInfo>
  {
  }

  public abstract class payeeName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BankFeedTransaction.payeeName>
  {
  }

  public abstract class tranCode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BankFeedTransaction.tranCode>
  {
  }

  public abstract class creditAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    BankFeedTransaction.creditAmount>
  {
  }

  public abstract class debitAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    BankFeedTransaction.debitAmount>
  {
  }

  public abstract class debitCreditParameter : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    BankFeedTransaction.debitCreditParameter>
  {
  }

  public abstract class accountName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    BankFeedTransaction.accountName>
  {
  }
}
