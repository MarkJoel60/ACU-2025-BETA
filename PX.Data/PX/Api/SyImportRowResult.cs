// Decompiled with JetBrains decompiler
// Type: PX.Api.SyImportRowResult
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Common.Parser;
using PX.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

#nullable disable
namespace PX.Api;

internal class SyImportRowResult
{
  private const string errorsSeparator = " ";
  internal bool NoteChanged;
  internal bool IsFilled;

  internal string[] SourceFields { get; set; }

  internal Dictionary<string, object> ExternalKeys { get; set; }

  public Exception Error { get; internal set; }

  public bool IsPersisted { get; internal set; }

  public object PersistedRow { get; internal set; }

  public Exception PersistingError { get; internal set; }

  public Dictionary<string, string> Keys { get; internal set; }

  public List<string> FieldExceptions { get; internal set; }

  public Dictionary<string, SyImportRowResult.FieldError> FieldErrors { get; internal set; }

  public Dictionary<string, string> UnlinkedErrors { get; internal set; }

  public Dictionary<string, string> UnlinkedWarnings { get; internal set; }

  public SyImportRowResult(string[] sourceFields)
  {
    this.SourceFields = sourceFields;
    this.FieldExceptions = new List<string>();
    this.FieldErrors = new Dictionary<string, SyImportRowResult.FieldError>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    this.UnlinkedErrors = new Dictionary<string, string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    this.UnlinkedWarnings = new Dictionary<string, string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
  }

  internal void AddFieldExceptions(SyImportProcessor.SyStep step)
  {
    foreach (KeyValuePair<string, SyImportProcessor.SyStep.SyView> view in step.Views)
    {
      foreach (KeyValuePair<string, SyImportProcessor.SyStep.SyField> field in view.Value.Fields)
      {
        ExpressionException exception = field.Value.Exception;
        if (exception != null)
        {
          field.Value.ErrorLevel = PXErrorLevel.Error;
          field.Value.Error = ((Exception) ((object) ((Exception) exception).InnerException ?? (object) exception)).Message;
          this.AddFieldError(field.Value, true);
          this.AddFieldException(field, step, view.Key);
        }
      }
    }
  }

  internal void AddFieldException(
    KeyValuePair<string, SyImportProcessor.SyStep.SyField> field,
    SyImportProcessor.SyStep step,
    string viewName)
  {
    if (!(((Exception) field.Value.Exception)?.InnerException is PXSubstitutionException innerException))
      return;
    PXView pxView;
    if (step.Graph.Views.TryGetValue(viewName, out pxView))
    {
      innerException.Info.TableName = pxView.GetItemType().FullName;
      innerException.Info.FieldName = pxView.Cache.GetBqlField(field.Key)?.Name;
    }
    else
    {
      innerException.Info.TableName = (string) null;
      innerException.Info.FieldName = field.Key;
    }
    this.FieldExceptions.Add(innerException.Serialize());
  }

  internal void AddFieldError(SyImportProcessor.SyStep.SyField field, bool isError)
  {
    string externalName = field.GetExternalName(this.SourceFields);
    if (Str.IsNullOrEmpty(externalName))
      return;
    string str = PXLocalizer.Localize(isError ? "Error" : "Warning");
    string errorText = field.Error.StartsWith(str) ? field.Error : $"{str}: {field.Error}";
    if (this.FieldErrors.ContainsKey(externalName))
      this.FieldErrors[externalName].AddError(isError, errorText);
    else
      this.FieldErrors.Add(externalName, new SyImportRowResult.FieldError(isError, externalName, errorText));
  }

  internal void AddUnlinkedError(SyImportProcessor.SyStep.SyField field, bool isError)
  {
    if (Str.IsNullOrEmpty(field.Formula))
      return;
    string str1 = PXLocalizer.Localize(isError ? "Error" : "Warning");
    string str2 = field.Error.StartsWith(str1) ? field.Error.Substring(str1.Length + 2) : field.Error;
    string key = $"{field.Formula}|{str2}";
    string str3 = field.CommittedInternalValue != null ? string.Format(PXLocalizer.Localize(isError ? "Field: {0}, Value: {2}, Error: {1}" : "Field: {0}, Value: {2}, Warning: {1}"), (object) field.DisplayName, (object) str2, field.CommittedInternalValue) : string.Format(PXLocalizer.Localize(isError ? "Field: {0}, Error: {1}" : "Field: {0}, Warning: {1}"), (object) field.DisplayName, (object) str2);
    if (isError)
    {
      this.IsPersisted = false;
      this.UnlinkedErrors[key] = str3;
    }
    else
      this.UnlinkedWarnings[key] = str3;
  }

  internal string GetFieldExceptions()
  {
    return this.FieldExceptions.Any<string>() ? SYData.JoinErrors((IEnumerable<string>) this.FieldExceptions) : (string) null;
  }

  internal string GetFieldErrors()
  {
    return this.FieldErrors.Any<KeyValuePair<string, SyImportRowResult.FieldError>>() ? SYData.JoinErrors(this.FieldErrors.Select<KeyValuePair<string, SyImportRowResult.FieldError>, string>((Func<KeyValuePair<string, SyImportRowResult.FieldError>, string>) (error => error.Value.Serialize()))) : (string) null;
  }

  internal string GetUnlinkedErrors(bool addWarnings)
  {
    string unlinkedErrors = string.Empty;
    if (this.UnlinkedErrors.Any<KeyValuePair<string, string>>())
      unlinkedErrors = string.Join(" ", this.UnlinkedErrors.Select<KeyValuePair<string, string>, string>((Func<KeyValuePair<string, string>, string>) (e => e.Value)));
    if (addWarnings && this.UnlinkedWarnings.Any<KeyValuePair<string, string>>())
    {
      if (unlinkedErrors.Length > 0)
        unlinkedErrors += " ";
      unlinkedErrors += string.Join(" ", this.UnlinkedWarnings.Select<KeyValuePair<string, string>, string>((Func<KeyValuePair<string, string>, string>) (e => e.Value)));
    }
    return unlinkedErrors;
  }

  internal string GetErrorMessage(bool addWarnings, bool ignoreBypassed)
  {
    string text = (string) null;
    if (this.Error != null)
    {
      bool addErrors;
      text = SyImportRowResult.ExtractMessage(this.Error, ignoreBypassed, out addErrors);
      if (((this.PersistingError == null ? 0 : (this.PersistingError != this.Error ? 1 : 0)) & (addErrors ? 1 : 0)) != 0)
        SyImportRowResult.AddMessage(ref text, " ", SyImportRowResult.ExtractMessage(this.PersistingError, ignoreBypassed, out addErrors));
      if (addErrors)
        SyImportRowResult.AddMessage(ref text, " ", this.GetUnlinkedErrors(addWarnings));
    }
    else if (this.PersistingError != null)
    {
      bool addErrors;
      text = SyImportRowResult.ExtractMessage(this.PersistingError, ignoreBypassed, out addErrors);
      if (addErrors)
        SyImportRowResult.AddMessage(ref text, " ", this.GetUnlinkedErrors(addWarnings));
    }
    else if (!this.IsPersisted)
      text = this.GetUnlinkedErrors(addWarnings);
    return text;
  }

  private static string ExtractMessage(Exception e, bool ignoreBypassed, out bool addErrors)
  {
    switch (e)
    {
      case SyImportProcessor.DetailBypassedException bypassedException:
        addErrors = false;
        return !ignoreBypassed ? bypassedException.MessageNoPrefix : (string) null;
      case SyImportProcessor.PXFieldErrorException _:
        addErrors = true;
        return string.Empty;
      case PXOuterException pxOuterException:
        addErrors = !((IEnumerable<string>) pxOuterException.InnerMessages).Any<string>();
        return string.Join(" ", pxOuterException.InnerMessages);
      case PXException pxException:
        addErrors = true;
        return pxException.MessageNoPrefix;
      case TargetInvocationException _:
        addErrors = true;
        return e.InnerException?.Message ?? e.Message;
      default:
        addErrors = true;
        return e.Message;
    }
  }

  private static void AddMessage(ref string text, string separator, string message)
  {
    if (Str.IsNullOrEmpty(message))
      return;
    if (!Str.IsNullOrEmpty(text))
      text += separator;
    text += message;
  }

  internal class FieldError
  {
    public bool IsError { get; internal set; }

    public string FieldName { get; internal set; }

    public string ErrorText { get; internal set; }

    public HashSet<string> ErrorList { get; internal set; }

    public FieldError(bool isError, string fieldName, string errorText)
    {
      this.IsError = isError;
      this.FieldName = fieldName;
      this.ErrorText = errorText;
      this.ErrorList = new HashSet<string>();
      this.ErrorList.Add(errorText);
    }

    public void AddError(bool isError, string errorText)
    {
      if (this.ErrorList.Contains(errorText))
        return;
      this.ErrorList.Add(errorText);
      this.ErrorText = $"{this.ErrorText}\r\n{errorText}";
      this.IsError |= isError;
    }

    public string Serialize()
    {
      string str = this.IsError ? "Error" : "Warning";
      return string.Join(SYData.PARAM_SEPARATOR.ToString(), str, this.FieldName, this.ErrorText);
    }

    public override string ToString() => this.Serialize();

    public static SyImportRowResult.FieldError Deserialize(string serialized)
    {
      string[] strArray = serialized.Split(SYData.PARAM_SEPARATOR);
      return new SyImportRowResult.FieldError(strArray[0].OrdinalEquals("Error"), strArray[1], strArray[2]);
    }
  }
}
