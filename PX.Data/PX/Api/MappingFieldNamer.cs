// Decompiled with JetBrains decompiler
// Type: PX.Api.MappingFieldNamer
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;

#nullable disable
namespace PX.Api;

internal static class MappingFieldNamer
{
  public const string LINE_NUMBER_VALUE = "##";
  public const string DIALOG_ANSWER_VALUE = "??";
  public const string EXTERNAL_KEY_FIELD = "NoteID";
  public const string EXTERNAL_KEY_VALUE = "@@NoteID";
  public const string EXTERNAL_KEY_LABEL = "<Key: External>";
  public const string PARAMETER_VALUE = "@";
  public const string KEY_VALUE = "@@";
  public const string SET_BRANCH_COMMAND = "<Set: Branch>";
  public const string ADD_ALL_FIELDS = "<Add All Fields>";

  public static string CreateActionLabel(string actionDisplayName)
  {
    return $"<{PXMessages.Localize("Action")}: {actionDisplayName}>";
  }

  public static string CorrectActionLabel(string actionLabel, string actionName)
  {
    return $"{actionLabel} ({actionName})";
  }

  public static string CreateActionValue(string actionName) => $"<{actionName}>";

  public static string CreateLineNumberLabel() => PXMessages.Localize("<Line Number>");

  public static string CreateLineNumberValue() => "##";

  public static string CreateDialogAnswerLabel() => PXMessages.Localize("<Dialog Answer>");

  public static string CreateDialogAnswerValue() => "??";

  public static string CreateExternalKeyLabel() => "<Key: External>";

  public static string CreateExternalKeyValue() => "@@NoteID";

  public static string CreateParameterLabel(string parameterName)
  {
    return $"<{PXMessages.Localize("Parameter")}: {parameterName}>";
  }

  public static string CreateParameterValue(string parameterName) => "@" + parameterName;

  public static string CreateKeyOrParameterFormula(string fieldName, string viewName = null)
  {
    if (string.IsNullOrEmpty(viewName))
      return $"=[{fieldName}]";
    return $"=[{viewName}.{fieldName}]";
  }

  public static string CreateKeyLabel(string keyName)
  {
    return $"<{PXMessages.Localize("Key")}: {keyName}>";
  }

  public static string CreateKeyValue(string keyField, string keyName)
  {
    return !string.IsNullOrEmpty(keyField) ? "@@" + keyField : "@@" + keyName;
  }
}
