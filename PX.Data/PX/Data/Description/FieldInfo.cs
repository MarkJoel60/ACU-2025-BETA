// Decompiled with JetBrains decompiler
// Type: PX.Data.Description.FieldInfo
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Text;

#nullable disable
namespace PX.Data.Description;

/// <summary>Describes fields in any single View.</summary>
[System.Diagnostics.DebuggerDisplay("{DebuggerDisplay(),nq}")]
public class FieldInfo
{
  public readonly string FieldName;
  public string DisplayName;
  public readonly CallbackDescr Callback;
  public readonly bool IsKey;
  public bool IsCommit;
  public readonly System.Type FieldType;
  public readonly bool IsEnabled;
  public readonly bool IsRequired;
  public readonly bool IsUnicode;
  public readonly bool IsSelector;
  public bool IsTimeList;
  public bool? PreserveTime;
  public readonly string TextField;
  public readonly string InputMask;
  public readonly string DisplayMask;
  public readonly object MinValue;
  public readonly object MaxValue;
  public readonly int Length;
  public readonly int Precision;
  public readonly string[] AllowedLabels;
  public bool IsEditorControlDisabled;
  public PXViewDescription SelectorViewDescription;
  public string LinkCommand;
  public bool Invisible;
  public bool HasChangingUpdatability;

  public FieldInfo(
    string name,
    string dispName,
    CallbackDescr callback,
    bool isKey,
    System.Type fieldType,
    bool isEnabled,
    bool isRequired,
    bool isUnicode,
    bool isSelector,
    string textField,
    string inputMask,
    string displayMask,
    object minValue,
    object maxValue,
    int length,
    int precision,
    string[] allowedLabels,
    bool isCommit)
  {
    this.FieldName = name;
    this.DisplayName = dispName;
    this.Callback = callback;
    this.IsKey = isKey;
    this.FieldType = fieldType;
    this.IsEnabled = isEnabled;
    this.IsRequired = isRequired;
    this.IsUnicode = isUnicode;
    this.IsSelector = isSelector;
    this.TextField = textField;
    this.InputMask = inputMask;
    this.DisplayMask = displayMask;
    this.MinValue = minValue;
    this.MaxValue = maxValue;
    this.Length = length;
    this.Precision = precision;
    this.AllowedLabels = allowedLabels;
    this.IsCommit = isCommit;
  }

  private string DebuggerDisplay()
  {
    StringBuilder stringBuilder = new StringBuilder(nameof (FieldInfo));
    if (!string.IsNullOrWhiteSpace(this.FieldName))
    {
      stringBuilder.Append(": \"").Append(this.FieldName).Append('"');
      if (!string.IsNullOrWhiteSpace(this.DisplayName))
        stringBuilder.Append(" (\"").Append(this.DisplayName).Append("\")");
      if (this.IsKey)
        stringBuilder.Append(" [KEY]");
    }
    return stringBuilder.ToString();
  }
}
