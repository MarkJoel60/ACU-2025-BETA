// Decompiled with JetBrains decompiler
// Type: PX.Api.Messages
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;

#nullable disable
namespace PX.Api;

[PXLocalizable]
internal class Messages
{
  public const string HasTwoFactor = "Two-factor authentication is enabled for this user. You cannot log in with this user account.";
  public const string InvalidHdr = "Invalid hdr";
  public const string PXSYTableCantContainDublColumns = "PXSYTable can't contain duplicate columns '{0}'";
  public const string RowNotBelongsThisTable = "You should add a row that belongs to this table.";
  public const string InstanceMethodNotFound = "The public instance method {0} is not found in {1}";
  public const string FieldNotSupportedByProvider = "The field is not supported by the provider: {0}";
  public const string GetFieldGroupFailed = "GetFieldGroup failed";
  public const string AttemptCommitBypassedRow = "An attempt to commit a bypassed row";
  public const string FormulaRuntimeError = "Formula runtime error: {0}";
  public const string InvalidFieldExported = "Invalid field exported: {0}";
  public const string CannotFindProviderObject = "Cannot find the specified provider object.";
  public const string InvalidRowIndex = "Invalid row index";
  public const string HasErrorConflictsWithErrorMessage = "HasError conflicts with ErrorMessage.";
  public const string NoSourceFieldMappingSpecified = "No source field mapping is specified.";
  public const string ProviderNotFound = "The provider is not found.";
  public const string IncorrectSyncType = "Incorrect sync type";
  public const string TypeDeclaredOutOfAnyNamespace = "The type {0} is declared out of any namespace.";
  public const string CannotResolveTableName = "Cannot resolve the table name: [{0}]";
  public const string InvalidArgumentInGetWrapperTypeName = "Invalid argument in GetWrapperTypeName";
  public const string NoAssembliesDefined = "You must specify at least one assembly.";
  public const string RepeatingOptionPrimary = "Repeat Only Summary Fields";
  public const string RepeatingOptionAll = "Repeat All Fields";
  public const string RepeatingOptionNone = "Do Not Repeat Fields";
  public const string SubstitutionKeyNotFound = "The {0} substitution list cannot be found. On the Import Scenarios (SM206025) form, select a substitution list in the Formula Editor dialog box, or add a new list by clicking Add Substitution.";
  public const string SubstitutionKeyNotFoundOnExport = "The {0} substitution list cannot be found. On the Export Scenarios (SM207025) form, select a substitution list in the Formula Editor dialog box, or add a new list on the Substitution Lists (SM206026) form.";
  public const string SubstitutionValueNotFound = "The substitution value for {0} cannot be found. Add a new substitution value for {0} by clicking the Add Substitution button.";
  public const string SubstitutionValueNotFoundOnExport = "The substitution value for {0} cannot be found. Add a new substitution value for {0} on the Substitution Lists (SM206026) form.";
  public const string SubstitutionValueNotUnique = "An element with the {0} value already exists.";
  public const string TheNumberOfReplacements = "Values replaced: {0}";
  public const string PublicationTargetCompanyListIsEmpty = "The list of companies cannot be empty";
  public const string OperationIsNotAllowed = "The operation is not allowed.";
  public const string UseConvertToManualScenarioAction = "To modify the mapping on this form, you must convert the import scenario to a manual scenario by clicking Convert to Manual Scenario on the form toolbar.";
  public const string DuplicateSetBranchCommand = "The <Set: Branch> command has been already added.";
  public const string BranchNotFound = "The {0} branch cannot be found in the system.";
  public const string AddAllFieldsCommand = "<Add All Fields>";
  public const string SetBranchCommand = "<Set: Branch>";
}
