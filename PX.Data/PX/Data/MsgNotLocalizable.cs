// Decompiled with JetBrains decompiler
// Type: PX.Data.MsgNotLocalizable
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

internal class MsgNotLocalizable
{
  public const string NotAuthenticated = "Not authenticated.";
  public const string ReportDesignerRoleRequiredSaveReports = "The ReportDesigner role is required to save or preview reports.";
  public const string CustomizerRoleRequiredPublishCustomization = "The Customizer role is required to publish customization.";
  public const string NewItemAttributeDirty = "NewItemAttribute is dirty.";
  public const string GetDirtyAttributeLevel = "GetDirtyAttribute level";
  public const string ReleaseItemStateRefCntLessZero = "ReleaseItemState RefCnt<0";
  public const string GetItemExtensionFailed = "GetItemExtension failed.";
  public const string UnknownSqlDbType = "Unknown SqlDbType";
  public const string InvalidOptionForWebConfig = "Invalid option {0} for web config QueryCacheLevel";
  public const string ClusterEnvironmentExpectedAllTablesChanged = "Cluster environment expected: AllTablesChanged";
  public const string ClusterEnvironmentExpectedTableChanged = "Cluster environment expected: TableChanged";
  public const string AgeGlobalNotInitialized = "AgeGlobal is not initialized.";
  public const string PXGraphPassedAsParameterToSelectmethod = "A PXGraph instance has been passed as a query parameter to the Select method.";
  public const string PXGraphExtensionPassedAsParameterToSelectMethod = "A PXGraphExtension instance has been passed as a query parameter to the Select method.";
  public const string NoNestedClassInDacForThisField = "There is no nested class in the DAC for this field.";
  public const string TypeNotImplementIBqlFieldInterface = "The type {0} doesn't implement the IBqlField interface.";
  public const string TypeNotBqlWhereCondition = "The type {0} is not a BQL Where condition.";
  public const string MethodOrOperationNotImplemented = "The method or operation is not implemented.";
  public const string SearchIndexCannotBeSaved = "SearchIndex cannot be saved. NoteID is required for an entity to be searchable but it has not been supplied.";
  public const string UnsupportedNodeType = "Unsupported node type '{0}'.";
  public const string UnsupportedFormulaOperator = "Unsupported formula operator '{0}.'";
  public const string InvalidArgumentForGraphCaches = "Invalid argument for graph.Caches[] : {0}";
  public const string CacheCrossReferencesNotPermitted = "When extending a cache, cross references are not permitted: {0}.";
  public const string UnknownProperty = "Unknown property {0}";
  public const string OriginalMethodNotBeenFound = "The {0} method in the {1} graph extension is marked as [PXOverride], but no original method with this name has been found in PXGraph.";
  public const string SignatureNotCompatibleWithOriginalMethod = "The {0} method in the {1} graph extension is marked as [PXOverride], but its signature is not compatible with the original method.";
  public const string InheritNotSupported = "Inherit not supported";
  public const string DependantExtensionDoesNotBelongToSameGraph = "Dependant extension does not belong to the same graph.";
  public const string MergeMethodReplaceIsInvalid = "MergeMethod.Replace is Invalid {0}";
  public const string GetAssemblyFromFilePathNotFound = "GetAssemblyFromFile: Path not found: {0}";
  public const string FunctionNotImplementedType = "The function {0} is not implemented in type {1} for {2}.";
  public const string InvalidNumberArgumentsInMethod = "Invalid number of arguments in the method {0} in type {1} for {2}.";
  public const string IncompatibleParameterInMethod = "Incompatible parameter #{3} in the method {0} in type {1} for {2}";
  public const string InvalidDelegateInMethod = "Invalid delegate in the method {0} in type {1} for {2}.";
  public const string RequiredReturnValueInMethod = "Required return value in the method {0} in type {1} for {2}.";
  public const string IncompatibleTypeOfReturnValueInMethod = "Incompatible type of return value in the method {0} in type {1} for {2}.";
  public const string UnexpectedReturnValueInMethod = "Unexpected return value in the method {0} in type {1} for {2}.";
  public const string GraphExtensionMustHaveDefaultConstructor = "Graph extension {0} must have a default constructor.";
  public const string CannotInstantiateAbstractGraphExtension = "Cannot instantiate graph extension {0} for use in {1} because class {0} is abstract.";
  public const string FailedToUnambiguouslyDetermineHandler = "Failed to determine a delegate for the {0} action of the {1} class (declared in the {2} class) in the {3} class because the following methods with the same name exist in the {4} class: {5}.";
  public const string GraphExtensionCannotReferToAbstractGraphExtensions = "Graph extension {0} cannot refer to the following abstract graph extensions that are not marked as [PXProtectedAccess]: {1}.";
  public const string CannotFindPropertyInAttribute = "Cannot find the property {0} in the attribute {1}.";
  public const string FieldNotFoundInCache = "The field '{0}' is not found in the cache '{1}'.";
  public const string NotProcessingGraph = "{0} is not processing graph";
  public const string OperationWithKeyAlreadyExists = "Operation with the key already exists.";
  public const string DelegateIsNull = "The delegate is null in context of the long operation.";
  public const string CannotStartLongOperation = "Long operation has been completed and cannot start a new operation.";
  public const string RemoteCall = "Remote call";
  public const string MethodIsNullInSecondCall = "The method is null in the second call.";
  public const string PendingTask = "Pending task";
  public const string MethodIsNullInLongOperation = "The method is null in the long operation.";
  public const string InvalidKeyForPXLongOperation = "Invalid key for PXLongOperation";
  public const string InfoLastPosLessZero = "info.LastPos < 0*";
  public const string Deprecated = "Deprecated !!!";
  public const string CannotInsertEventHandler = "Cannot insert the event handler.";
  public const string HandlerIsNotFound = "Handler is not found";
  public const string CurrentAPIVersion = "Current API version: ";
  public const string LoggedInSuccessfully = "Logged in successfully";
  public const string InvalidCredentials = "Invalid credentials";
  public const string CantPrepareParameters = "Cannot prepare parameters";
  public const string DetailEntityNotFound = "The {0} detail entity with the {1} ID cannot be found. Probably the ID from another session is used.";
  public const string NoMappingInExtension = "Can't find mapping {0} field in extension {1}";
  public const string AggConcatSeparatorIsTooLong = "The separator for the AggConcat function must not be longer than {0} characters.";
}
