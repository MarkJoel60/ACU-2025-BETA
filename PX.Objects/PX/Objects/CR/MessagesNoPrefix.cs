// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.MessagesNoPrefix
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;

#nullable disable
namespace PX.Objects.CR;

[PXLocalizable]
public static class MessagesNoPrefix
{
  public const string CbApi_Attributes_ComboboxAttributeDoesntSupportSpecifiedValue = "The specified value is not valid for the {0} attribute that has the Combo type.";
  public const string CbApi_Attributes_CheckboxAttributeDoesntSupportSpecifiedValue = "The specified value is not valid for the {0} attribute that has the Checkbox type because this value cannot be converted to a boolean value.";
  public const string CbApi_Attributes_NumberAttributeDoesntSupportSpecifiedValue = "The specified value is not valid for the {0} attribute that has the Number type because this value cannot be converted to a decimal value.";
  public const string CbApi_Attributes_DatetimeAttributeDoesntSupportSpecifiedValue = "The specified value is not valid for the {0} attribute that has the Datetime type because this value cannot be converted to a DateTime value.";
  public const string CbApi_Attributes_MultiComboboxAttributeDoesntSupportSpecifiedValue = "One of the specified values is not valid for the {0} attribute that has the Multi Select Combo type. Note that the Multi Select Combo type supports identifiers and does not support descriptions.";
  public const string CbApi_Activities_ActivityCannotBeInsertUpdatedDeleted = "An activity can be inserted, updated, or deleted from a contract-based API only through the Activity, Task, Event, or Email top-level entity.";
  public const string ErrorOccurred = "Error occurred: {0}";
  public const string OpportunityStagesSlotWasNotInitialized = "The opportunity stages slot was not initialized.";
  public const string AttributeNotFoundInClass = "The {1} class does not have the {0} attribute. Make sure the attribute name is correct or consider adding the attribute to the class.";
  public const string CurrentContactIsNull = "The linked contact on the Contact tab cannot be found.";
  public const string CurrentBillToContactIsNull = "The linked contact on the Financial tab cannot be found.";
  public const string CurrentShipToContactIsNull = "The linked contact on the Shipping tab cannot be found.";
  public const string CurrentAddressIsNull = "The address of the linked contact on the Contact tab cannot be found.";
  public const string CurrentBillToAddressIsNull = "The linked address on the Financial tab cannot be found.";
  public const string CurrentShipToAddressIsNull = "The linked address on the Shipping tab cannot be found.";
  public const string ContactNotFound = "The contact with the ID {0} cannot be found.";
  public const string AddressNotFound = "The contact's address with the ID {0} cannot be found.";
  public const string LeadNotFound = "The lead with the ID {0} cannot be found.";
  public const string BAccountNotFound = "The business account with the ID {0} cannot be found.";
  public const string CannotParseContactId = "The contact ID {0} is invalid and cannot be parsed to the integer.";
  public const string CannotParseBAccountId = "The business account ID {0} is invalid and cannot be parsed to the integer.";
  public const string EmailValidationFailed = "Email validation failed. The following fields contain invalid email addresses: {0}";
  public const string CannotValidateItemForDuplicates = "Duplicate validation for the record cannot be performed. Contact your system administrator.";
  public const string WouldYouLikeToRecalculateRecords = "Duplicate validation rules have been changed. To apply new settings, you need to recalculate validation scores. Would you like to open the Calculate Grams (CR503400) form?";
  public const string TargetRecordIsInactive = "The target record has the Inactive status.";
  public const string CannotInitializeSelectForView = "Cannot initialize select {0} for view {1}.";
  public const string CannotInitializeSelectForView_AbstractSelect = "Cannot initialize the following select for the {1} view because the select must be non-abstract, PXSelectBase, or PXSelectBase<Table>: {0}.";
  public const string MarketingListNotFound = "The marketing list {0} is not found.";
  public const string MarketingListIsAlreadyDynamic = "The marketing list {0} is already dynamic.";
  public const string MarketingListIsAlreadyStatic = "The marketing list {0} is already static.";
  public const string CannotAddMembersToDynamicList = "The members cannot be added to the marketing list {0} because it is dynamic.";
  public const string CannotRemoveMembersFromDynamicList = "The members cannot be removed from the marketing list {0} because it is dynamic.";
  public const string MarketingListIDIsNull = "The marketing list ID cannot be empty.";
  public const string MarketingListAlreadyExists = "The marketing list with the {0} ID already exists.";
  public const string CampaignUpdateListMembersButtonTooltip = "Replace campaign members added from marketing lists with those in the lists on the Marketing Lists tab";
  public const string CampaignClearMembersButtonTooltip = "Remove members currently shown on all pages from the list";
  public const string MarketingListClearMemberButtonTooltip = "Remove members currently shown on all pages from the list";
  public const string MarketingListCopyMembersButtonTooltip = "Copy all members of the selected marketing list";
  public const string CannotCreateCustomerFromCreateSalesOrder = "You need to extend the business account to be a customer. Click Create Customer, and fill in the required settings of the class on the Customers (AR303000) form. Then you can create the sales order.";
  public const string CannotCreateSalesOrderWhenAccountIsEmpty = "You must specify a business account to create a sales order.";
  public const string CannotConvertBusinessAccountToCustomer = "Cannot convert the business account to a customer. The conversion extension is not found.";
  public const string NoAccessToCreateCustomerToCreateSaleOrder = "To create a sales order, you need to create a customer first. You do not have access rights to create a customer. Please contact your system administrator.";
  public const string IfCaseBecomesInactive = "If Case Becomes Inactive";
  public const string IfCaseSolutionIsProvidedInActivity = "If Case Solution Is Provided in Activity";
  public const string ConsiderTurnOnTrackSolutionsInActivities = "At least one check box is selected for time tracking on the Commitments tab. Consider selecting the Track Solutions in Activities check box to track the resolution time in activities marked as a solution.";
  public const string MidnightCalendarOverlapping = "Calendars with time periods that pass over midnight are not supported for cases.";
  public const string CustomerHasReturnOrderWarning = "This customer already has at least one open return order.";
  public const string LinkEntitiesCaption = "Select the field values that you want to use for the contact.";
  public const string LinkEntitiesProcess = "Process";
  public const string MergeEntitiesLeftColumnDefaultCaption = "Left record";
  public const string MergeEntitiesRightColumnDefaultCaption = "Right record";
  public const string MergeEntitiesTargetRecordLabel = "Target Record";
  public const string MergeEntitiesCaption = "Select the field values that you want to keep in the target record.";
  public const string MergeEntitiesLeftValueDescriptionCaption = "Current Record";
  public const string MergeEntitiesRightValueDescriptionCaption = "Duplicate Record";
  public const string DeleteClassNotification = "The row cannot be deleted because it is inherited from the Mailing & Printing settings of the class.";
  public const string NoRecipientMassEmail = "At least one recipient must be specified for the mass email.";
  public const string MarketingMemberImportResult_Successfull = "{0} has been added to the list.";
  public const string MarketingMemberImportResult_Warning = "{0} has not been found in the system and the mapping file does not contain the required field values to create a new record.";
  public const string MarketingMemberImportResult_Error = "{0} has not been added to the list because of the following error: {1}";
  public const string CannotCreateReturnOrderWhenAccountIsEmptyOrProspect = "To create a return order, you must specify a business account that has the Customer or Customer & Vendor type.";
  public const string NoAccessToCreateReturnOrder = "You do not have access rights to create a return order. Contact your system administrator if you need your permissions to be updated.";
  public const string CashSalesDesc = "Cash Sales";
  public const string ChecksAndPaymentsDesc = "Checks and Payments";
  public const string InvoicesAndMemoDesc = "Invoices and Memos";
  public const string InvoicesDesc = "SO Invoices";
  public const string OpportunitiesDesc = "Opportunities";
  public const string ProjectsDesc = "Projects";
  public const string ProjectsQuoteDesc = "Project Quotes";
  public const string ProFormaInvoiceDesc = "Pro Forma Invoices";
  public const string PurchaseOrderDesc = "Purchase Orders";
  public const string SalesOrderDesc = "Sales Orders";
  public const string SalesQuoteDesc = "Sales Quotes";
  public const string ShipmentsDesc = "Shipments";
  public const string CannotFindViewOnParse = "The value of at least one field in the selected email template is not recognized.";
  public const string AtLeastOneDataFieldError = "The value of at least one field in the selected email template is not recognized.";
  public const string OnlyErpCanCreateActivityOriginatedByErp = "The '{0}' activity type is originated by ERP. Only ERP can create activities of this type.";
  public const string OnlyPortalCanCreateActivityOriginatedByPortal = "The '{0}' activity type is originated by Portal. Only Portal can create activities of this type.";
  public const string OnlySystemCanCreateActivityOriginatedBySystem = "The '{0}' activity type is originated by System. Only System can create activities of this type.\r\n ";
  public const string CannotCreateInitialActivityRefNoteIdIsNull = "Cannot create initial activity. Current primary entity is null or RefNoteID is null.";
  public const string NoOpenIdProviderForOutlook = "The OpenID provider is not specified for the Acumatica add-in for Outlook.";
  public const string AccessTokenIsMissing = "The access token is missing. The on-behalf-of flow of the OAuth 2.0 authentication cannot be initialized.";
  public const string CreateNew = "New";
  public const string AddActivityTooltip = "Create Activity";
  public const string SingleEmailAttachmentName_Default = "Attachment {0}.pdf";
  public const string SingleEmailAttachmentName_ARInvoice = "Invoices and Memos {0}.pdf";
  public const string SingleEmailAttachmentName_SOOrder = "Orders {0}.pdf";
  public const string SingleEmailAttachmentName_SOQuote = "Quotes {0}.pdf";
  public const string SingleEmailAttachmentName_CRQuote = "Sales Quotes {0}.pdf";
  public const string SingleEmailAttachmentName_PMQuote = "Project Quotes {0}.pdf";
}
