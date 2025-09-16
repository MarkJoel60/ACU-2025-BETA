// Decompiled with JetBrains decompiler
// Type: PX.Data.InfoMessages
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;

#nullable disable
namespace PX.Data;

/// <exclude />
[PXLocalizable]
public static class InfoMessages
{
  public const string EqualsTo = "Equals";
  public const string NotEqualsTo = "Does Not Equal";
  public const string GreaterThan = "Is Greater Than";
  public const string GreateThanOrEqualsTo = "Is Greater Than or Equal To";
  public const string LessThan = "Is Less Than";
  public const string LessThanOrEqualsTo = "Is Less Than or Equal To";
  public const string Between = "Is Between";
  public const string Like = "Contains";
  public const string NotLike = "Does Not Contain";
  public const string LeftLike = "Ends With";
  public const string RightLike = "Starts With";
  public const string IsNull = "Is Empty";
  public const string IsNotNull = "Is Not Empty";
  public const string Today = "Today";
  public const string Overdue = "Overdue";
  public const string TodayOverdue = "Today+Overdue";
  public const string Tomorrow = "Tomorrow";
  public const string ThisWeek = "This Week";
  public const string NextWeek = "Next Week";
  public const string ThisMonth = "This Month";
  public const string NextMonth = "Next Month";
  public const string In = "Is In";
  public const string NotIn = "Is Not In";
  public const string ER = "External Reference";
  public const string NestedSelectorFilter = "Nested selector filter";
  public const string FiltersScreenID = "CS209010";
  public const string ChangedAutomatically = "'{0}' was changed automatically to {1}.";
  public const string NewRecord = "New Record";
  public const string SiteMaintetanceComplite = "Site maintenance has been completed.";
  public const string Explicit = "Explicit";
  public const string SearchIndexCacheName = "Search Index";
  public const string VisitedRecordCacheName = "Viewed Record";
  public const string FavoriteRecordCacheName = "Favorite Record";
  public const string FavoriteActionCacheName = "Favorite Action";
  public const string FileNameDefaultValue = "<EmptyFileName>";
  public const string ProviderNoteID = "ProviderNoteID";
  public const string ObjectKeyWarning = "You have not provided external keys for the target object {0}. As a result, every prepared source record will be treated as a separate object. Do you want to correct the scenario before saving?";
  public const string WrongScreenID = "An import scenario for this form cannot be created. The form doesn't have the Copy and Paste actions.";
  public const string ImportBaseName = "Import";
  public const string GIEntryScreenHasBeenAlreadyReplaced = "This entry screen has already been replaced with another list screen. Do you want to overwrite the screen?";
  public const string GIListScreenHasBeenAlreadyReplaced = "This inquiry has already been used as a list for another entry screen. Do you want to replace it?";
  public const string GIPrimaryScreenDoesNotContainThisRecord = "Cannot open this record for editing: The form {0} does not contain it.";
  public const string GIDefaultSorts = "No sort order was defined. Default sort order will be used.";
  public const string GIDefaultSortsWithComplex = "No sort order was defined. Default sort order will be used. We recommend that you define a custom sort order because the default order may slow down the inquiry.";
  public const string GIAscendingSortOrder = "ASC";
  public const string GenericResultName = "Generic Inquiry Result";
  public const string GITotalMax = "max";
  public const string GITotalMin = "min";
  public const string GITotalSum = "sum";
  public const string GITotalAvg = "avg";
  public const string GITotalCount = "count";
  public const string GIIsDeletedColumn = "Is Deleted";
  public const string GIIsRecordStatusColumn = "Status";
  public const string GIDacPreviewTitle = "Preview - {0}";
  public const string GIDacPreviewTitleWithDisplayName = "Preview - {0} ({1})";
  public const string GIDacPreviewFieldWithDisplayName = "{0} ({1})";
  public const string FormFilterPostfix = "(form filter)";
  public const string GISourceTypeTable = "Table";
  public const string GISourceTypeGI = "Generic Inquiry";
  public const string PropertyDisplayName = "Name";
  public const string PropertyValue = "Value";
  public const string AddToFavorites = "Add to Favorites";
  public const string RemoveFromFavorites = "Remove from Favorites";
  public const string UnsavedPreparedDataHeader = "Unsaved Prepared Data";
  public const string UnsavedPreparedDataMessage = "Prepared data was not saved. Do you want to save it?";
  public const string ScreenIDWillBeChangedInRelatedTables = "You are going to change the Screen ID of the existing form. All the related entities will be updated automatically. Do you want to proceed?";
  public const string NoScheduleHistoryHeader = "Processing Results";
  public const string NoScheduleHistoryRecordsFound = "No records are found. The detailed results of processing were deleted. On the Automation Schedules (SM205020) form, you can configure the number of results stored for a specific schedule by setting the Executions to Keep in History parameter on the Details tab.";
  public const string Periods = "Period(s)";
  public const string Days = "Day(s)";
  public const string Weeks = "Week(s)";
  public const string Months = "Month(s)";
  public const string ExposeToMobile = "Expose to the Mobile Application";
  public const string NotesAndFilesTable = "Attach Notes To";
  public const string HideNotesAndFiles = "Not Applicable";
  public const string ScreenIDLabel = "Screen ID";
  public const string Tools = "Tools";
  public const string ToolsTip = "View Tools";
  public const string DACBrowser = "DAC Schema Browser";
  public const string ShareColumnConfiguration = "Share Column Configuration";
  public const string Customization = "Customization";
  public const string SelectProject = "Select Project...";
  public const string EditProject = "Edit Project...";
  public const string ManageCustomizations = "Manage Customizations...";
  public const string ScreenConfiguration = "Screen Configuration";
  public const string ManageUserDefinedFields = "Manage User-Defined Fields";
  public const string EditGenericInquiry = "Edit Generic Inquiry";
  public const string OpenPivotTablesEditor = "Pivot Tables";
  public const string CopyCode = "Copy";
  public const string WikiReadDuration = "minutes to read";
  public const string WikiReadDurationOne = "minute to read";
  public const string ShowCode = "Show";
  public const string HideCode = "Hide";
  public const string RecordIsArchived = "The current record is archived.";
  public const string LineCounterValidationName = "Line Counter Validation: {0}";
  public const string LineCounterValidationDetails = "Line counter value: {0}, maximum line number value: {1}.";
}
