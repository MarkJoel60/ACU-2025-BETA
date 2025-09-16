// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.PaymentProcessor.Messages
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;

#nullable disable
namespace PX.Objects.AP.PaymentProcessor;

[PXLocalizable]
public class Messages
{
  public const string Onboarded = "Active";
  public const string OnboardRequired = "Pending Onboarding";
  public const string Deactivated = "Inactive";
  public const string Pending = "Pending";
  public const string Invalid = "Invalid";
  public const string Expired = "Expired";
  public const string Disabled = "Disabled";
  public const string Blocked = "Blocked";
  public const string Active = "Active";
  public const string Unverified = "Unverified";
  public const string Nominated = "Nominated";
  public const string Verified = "Verified";
  public const string Undefined = "Undefined";
  public const string Open = "Open";
  public const string Successful = "Successful";
  public const string Error = "Error";
  public const string Unknown = "Unknown";
  public const string CardNumber = "Card Number";
  public const string BillCom = "Bill.com";
  public const string Disable = "Disable";
  public const string BillcomDisclaimerMessage = "Money transmission services provided by Bill.com, LLC (NMLS #: 1007645)";
  public const string ExternalPaymentProcessorDoesNotExist = "The operation failed because the external payment processor does not exist.";
  public const string UserDoesNotExist = "The operation failed because the user does not exist in the external payment processor.";
  public const string ExternalUserDoesNotExist = "The operation failed because the {0} user does not exist in the external payment processor.";
  public const string OrganizationDoesNotExist = "The operation failed because the company does not exist in the external payment processor.";
  public const string ExternalOrganizationDoesNotExist = "The operation failed because the {0} company does not exist in the external payment processor.";
  public const string UserNotOnboarded = "The logged-in user is not active in {0}. You cannot send requests to {0} until your user account is activated.";
  public const string UserCannotBeDeleted = "An onboarded user cannot be deleted.";
  public const string CashAccountAlreadyMapped = "The selected cash account has already been mapped to the {0} funding account of the {1} company.";
  public const string ErrorAfterSessionUpdating = "The user session has been updated. {0}";
  public const string NotMappedFundingAccount = "Payments cannot be processed because the {0} cash account is not mapped to an active funding account in the external payment processor. To process payments, make sure that the cash account is mapped on the Funding Accounts tab of the External Payment Processor (AP205500) form.";
  public const string CashAccountMappedToPendingAccount = "Payments cannot be processed because the {0} cash account is mapped to a funding account pending activation in the external payment processor. To process payments, make sure that the funding account is active on the Funding Accounts tab of the External Payment Processor (AP205500) form.";
  public const string CashAccountMappedToDeletedAccount = "Payments cannot be processed because the cash account {0} is mapped to a funding account that is not active in the external payment processor. To process payments, make sure that the funding account is active on the Funding Accounts tab of the External Payment Processor (AP205500) form.";
  public const string NotMappedOrgUser = "Payments cannot be processed because your user is not created for the {0} company in the external payment processor. To process payments, make sure that the {1} user is added and onboarded on the External Payment Processor (AP205500) form.";
  public const string NotMappedFundingAccountUser = "Payments cannot be processed because your user is not nominated for the funding account in the {0} company in the external payment processor. To process payments, select a cash account that is mapped to a funding account you are allowed to use.";
  public const string DisabledPaymentProcessor = "The external payment processor is inactive. To process payments, activate the payment processor on the External Payment Processor (AP205500) form.";
  public const string DisableFundingAccount = "Payments cannot be processed because the {0} cash account is mapped to a funding account that is not active in the external payment processor. To process payments, select a cash account that is mapped to an active funding account on the Funding Accounts tab of the External Payment Processor (AP205500) form.";
  public const string UserNotOnBoardedForPayment = "Payments cannot be processed because your user is not active in the {0} company in the external payment processor. To process payments, make sure that the {1} user is activated on the External Payment Processor (AP205500) form.";
  public const string DisableFundingAccountUser = "Payments cannot be processed because your user was disabled for the funding account in the {0} company in the external payment processor. To process payments, select a cash account that is mapped to a funding account you are allowed to use.";
  public const string NotVerifiedFundingAccount = "You cannot process payments because the Cash Account {0} is mapped to an unverified Funding Account of the {1} company of the {2}. You can access the External Payment Processor form to verify it on the Funding Accounts tab.";
  public const string NotVerifiedFundingAccountUser = "Payments cannot be processed because your user is not verified for the funding account in the {0} company in the external payment processor. To process payments, click Verify Funding Account and verify yourself for the funding account.";
  public const string ForeignCurrencyNotSupported = "Payments for non-USD documents are not supported.";
  public const string OnlyCurrencySupported = "Only USD funding accounts are supported by the external payment processor.";
  public const string DisableAccountConfirmation = "The selected funding account will be permanently disabled in the {0} company. Once disabled, it cannot be restored, and the related unprocessed payments will be canceled. To proceed, click Disable.";
  public const string DisableAccountUserConfirmation = "Access to the selected funding account will be permanently disabled for the {0} user. The operation is irreversible. To proceed, click Disable.";
  public const string DisableAccountUserCaption = "Disable Access to Funding Account";
  public const string DisableAccountCaption = "Disable Funding Account";
  public const string VendorCreditMoreThanBill = "The {0} payment cannot be processed because the total amount of debit adjustments applied to the payment is greater than the highest amount applied to the bill.";
  public const string WebhooksWorksOnlyWithHTTPS = "Webhooks work only if your Acumatica ERP instance is hosted over HTTPS.";
  public const string PaymentShouldBeVoided = "Disbursement has failed in the external payment processor. Please void this payment.";
  public const string VendorNotBasedInUSA = "Payments to vendors located outside the USA are not supported.";
  public const string CompanyAlreadyAdded = "The company has already been added to the payment processor.";
  public const string UserAlreadyAdded = "The user has already been added for the company.";
  public const string ExistsAnotherActiveEPP = "The record cannot be saved because another active payment processor with the {0} plug-in exists.";
  public const string CannotAddUser = "Cannot add a user before the company is onboarded.";
  public const string ExternalProcessorNotSupportedForDocType = "External Payment Processing is not available for {0} Documents";
  public const string CompanyDoesNotSupportCountry = "Cannot add companies that are not based in {0}";
  public const string PaymentInWrongStatus = "The payment was not processed successfully by the external payment processor. Click Synchronize Payment on the Checks and Payments (AP302000) form and try to process it again.";
  public const string PaymentAlreadyCreated = "The payment has already been created in the external payment processor.";
  public const string PaymentNotFound = "The payment does not exist in the external payment processor.";
  public const string FieldCannotBeEmpty = "{0} cannot be empty.";
}
