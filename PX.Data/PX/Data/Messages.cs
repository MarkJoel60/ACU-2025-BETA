// Decompiled with JetBrains decompiler
// Type: PX.Data.Messages
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;

#nullable disable
namespace PX.Data;

[PXLocalizable]
public class Messages
{
  public const string NoEmailToSendConfirmation = "There is no email address for this user.";
  public const string ConfirmationEmailSentTemplate = "Enter code which was sent to your email {0}:";
  public const string ConfirmationAccessCode = "Enter code generated in Acumatica mobile app (use Generate Access Code command of Edit Account menu) or from the list of access codes";
  public const string MultifactorRegistrationEmailSent = "To verify your identity by approving push requests on your mobile device: ~ ~1. Install Acumatica mobile app on your device. ~ ~2. Sign in to the account of this Acumatica instance using access code sent to your email: {0}. ~ ";
  public const string UnableUpdateFailureCount = "Unable to update the failure count and to start a window.";
  public const string SessionObjectWaitTimeout = "Session object wait timeout: {0}";
  public const string ProcessWithSessionContextFailed = "ProcessWithSessionContext failed.";
  public const string InvalidSessionKey = "Invalid session key";
  public const string PathNotFound = "Path not found {0}";
  public const string DoubleWrap = "Double wrap";
  public const string ArgumentNullError = "The argument cannot be null.";
  public const string UnrecognizedAttribute = "Unrecognized attribute '{0}'";
  public const string NodesWithIdenticalKeyDetected = "Multiple nodes with identical key {0} have been detected.";
  public const string ExtensionNotBelongSameCache = "The dependent extension does not belong to the same cache.";
  public const string InfiniteLoopOneCompanies = "The infinite loop 1 of companies has been detected.~ {0}";
  public const string InfiniteLoopTwoCompanies = "The infinite loop 2 of companies has been detected.~ {0}";
  public const string CompanyWithParent = "Tenant {0} with parent {1}";
  public const string CompanyWithParentNull = "Tenant {0} with parent null";
  public const string UnsupportedFormulaOperator = "Unsupported formula operator '{0}.'";
  public const string IndexOutsideOfArray = "Index is outside bounds of the array";
  public const string InvalidScreenId = "Invalid screenId";
  public const string CurrentRowNotSelected = "The current row is not selected.";
  public const string NotInquiryScreen = "Screen '{0}' is not an inquiry screen.";
  public const string IdentityProviderNotSpecified = "Identity provider is not specified.";
  public const string IdentityProviderNotEnabled = "{0} identity provider is not enabled.";
  public const string IdentityProviderConfigurationNotFound = "{0} identity provider configuration is not found.";
  public const string CompanyNotSpecified = "Tenant is not specified";
  public const string SystemCannotExecuteOAuthWebRequest = "The system cannot execute an OAuth web request.";
  public const string CannotFindCollectionItem = "Cannot find a collection item with the index {0} in {1}.~A possible reason: The page was modified after the upgrade.";
  public const string TableNotFoundInDatabase = "Cannot create '{0}' column in table '{1}' because that table is not found in the database. Please update the customization to match the current data schema.";
  public const string TableNotFoundInDatabaseAlter = "The {0} column of the {1} table cannot be altered because the table is not found in the database. Update the customization to match the current data schema.";
  public const string ColumnNotFoundInTable = "The {0} column of the {1} table cannot be altered because the column is not found in the database. Update the customization to match the current data schema.";
  public const string ErrorOnPageWithMessage = "error on page {0}~with message : {1}";
  public const string CannotFindControlThatMatchesCustomization = "Cannot find the control that matches the customization ID {0} on the page.~There is a customization for this page that updates the properties of an item~that was moved or deleted in this version of the product.~You should manually fix the customization document.";
  public const string ErrorProcessingVirtualControl = "An error while processing the virtual control {0}~with the message: {1}";
  public const string PageTypeFailed = "Page type failed";
  public const string PageCompilationFailed = "Page compilation failed with error: {0}";
  public const string FieldNameMustBeSet = "PXGenerateAfterAttribute fieldName must be set.";
  public const string NoSuchDacFieldHasBeenFound = "Invalid PXGenerateAfterAttribute parameter: No such DAC field has been found.";
  public const string CannotBeNullOrEmpty = "Cannot be null or empty.";
  public const string ValidationFailedHeader = "Validation failed";
  public const string FileContainsIncorrectData = "The file, which is being uploaded, contains incorrect data.";
  public const string MultipleFeaturesExist = "Multiple features with the {0} name exist in the system. You should rename the custom features so that their names differ from the name of the in-built feature.";
  public const string YouAreNotAllowedToEditSharedFilters = "You are not allowed to edit shared filters. If you need to edit this filter contact your system administrator.";
  public const string NotEnoughDeclaredParametersInDelegate = "The number of declared parameters of the {0} delegate does not match the number of parameters of the overridden method.";
  public const string IncompatibleReturnTypeInDelegate = "Return type of the {0} delegate does not match the return type of the overridden method.";
  public const string GenericPopupText = "Multiple notes have been found for the inserted records.";
  public const string TheRecordHasBeenErased = "The record has been erased due to personal data protection reasons and the related personal data is being masked.";
  public const string TheRecordHasBeenRestricted = "The processing of the record is restricted and the related personal data is being masked.";
  public const string NotPseudonymized = "Not Pseudonymized";
  public const string Pseudonymized = "Pseudonymized";
  public const string Erased = "Erased";
  public const string EmailButton = "Email";
  public const string EmailButtonToolTip = "Receive code by email";
  public const string AccessCodeToolTip = "Enter code generated in mobile app or from the list";
  public const string AccessCodeButton = "Access Code";
  public const string CannotDecodeBase64ContentExplicit = "An HTML element with the \"img\" tag cannot be parsed because it has the \"src\" attribute with invalid base64 content.";
  public const string CanFocus = "This cell can be focused by using the Tab button";
  public const string CannotFocus = "This cell cannot be focused by using the Tab button";
  public const string NotEnoughRightsToAccessScreen = "You have insufficient rights to access the {0} ({1}) form.";
  public const string AddToFavoritesFailed = "Failed to add to favorites";
  public const string GetUserTimeZoneMethodDoesntExist = "The {0} type does not have the {1} method.";
  public const string DCIssue = "A data corruption state has been detected. You cannot save the changes. Copy the data you have entered and reload the page. Date and Time: {0}; IncidentID: {1}; Name: {2}. You can view detailed information about the issue on the System Events tab of the System Monitor (SM201530) form.";
  public const string InvalidRowCode = "An RMRow record with RowSetCode set to {0} contains an invalid RowCode value: {1}.";
  public const string InvalidLinkedRowCode = "An RMRow record with RowSetCode set to {0} and RowCode set to {1} contains an invalid LinkedRowCode value: {2}.";
  public const string InvalidBaseRowCode = "An RMRow record with RowSetCode set to {0} and RowCode set to {1} contains an invalid BaseRowCode value: {2}.";
  public const string ViewCannotBeFound = "The value of at least one field in the selected email template is not recognized.";
  public const string WrongDataField = "The value of at least one field in the selected email template is not recognized.";
}
