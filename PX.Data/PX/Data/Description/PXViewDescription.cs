// Decompiled with JetBrains decompiler
// Type: PX.Data.Description.PXViewDescription
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#nullable disable
namespace PX.Data.Description;

[System.Diagnostics.DebuggerDisplay("{DebuggerDisplay(),nq}")]
public class PXViewDescription
{
  private FieldInfo[] _allFields;
  public readonly ParsInfo[] Parameters;
  public readonly ParsInfo[] AllParameters;
  public bool HasLineNumber;
  public bool HasFileIndicator;
  public bool ForceNoteFiles;
  public bool AppendEveryField;
  public bool HasNoteID;
  public bool IsInSmartPanel;
  public bool IsGrid;
  public bool IsTree;
  public bool SyncAlways;
  public bool DefinedExplicitly;
  public PX.Data.Description.FieldGenerationMode? FieldGenerationMode;
  public bool? PXGridAllowAddNew;
  public bool? PXGridAllowDelete;
  public bool? PXGridAllowUpdate;
  public string PXGridDefaultAction;
  public bool? MergeToolbar;
  public string GenerateColumns;
  public string StatusField;
  public string SelectorDescriptionField;
  public string SelectorKeyField;
  public string BlankFilterHeader;

  public string DisplayName { get; set; }

  public string ViewName { get; set; }

  public bool CreatedFromOtherViewDescription { get; set; }

  public FieldInfo[] Fields { get; private set; }

  public FieldInfo[] AllFields
  {
    get => this._allFields;
    private set
    {
      this._allFields = value ?? new FieldInfo[0];
      this.Fields = ((IEnumerable<FieldInfo>) this._allFields).Where<FieldInfo>((Func<FieldInfo, bool>) (f => !f.Invisible)).ToArray<FieldInfo>();
    }
  }

  public PXViewDescription(
    string dispName,
    string viewName,
    FieldInfo[] fields,
    ParsInfo[] pars,
    bool appendEveryField)
  {
    this.DisplayName = dispName;
    this.ViewName = viewName;
    this.AllFields = fields;
    this.AllParameters = pars;
    this.Parameters = pars != null ? ((IEnumerable<ParsInfo>) pars).Where<ParsInfo>((Func<ParsInfo, bool>) (p => !p.Invisible)).ToArray<ParsInfo>() : new ParsInfo[0];
    this.AppendEveryField = appendEveryField;
  }

  public PXViewDescription(string viewName)
    : this((string) null, viewName, (FieldInfo[]) null, (ParsInfo[]) null, false)
  {
  }

  public string[] GetFieldNames()
  {
    if (this.Fields == null)
      return (string[]) null;
    string[] fieldNames = new string[this.Fields.Length];
    for (int index = 0; index < this.Fields.Length; ++index)
      fieldNames[index] = this.Fields[index].FieldName;
    return fieldNames;
  }

  public FieldInfo this[string fieldName]
  {
    get
    {
      if (this.Fields == null)
        return (FieldInfo) null;
      foreach (FieldInfo field in this.Fields)
      {
        if (field.FieldName == fieldName)
          return field;
      }
      return (FieldInfo) null;
    }
  }

  public bool HasSearchesByKey
  {
    get
    {
      if (this.Parameters == null)
        return false;
      foreach (ParsInfo parameter in this.Parameters)
      {
        if (parameter.Type == ParType.Searches)
        {
          foreach (FieldInfo field in this.Fields)
          {
            if (field.IsKey && field.FieldName == parameter.Field)
              return true;
          }
        }
      }
      return false;
    }
  }

  public void AddField(FieldInfo field)
  {
    FieldInfo[] fieldInfoArray = new FieldInfo[this.AllFields.Length + 1];
    this.AllFields.CopyTo((Array) fieldInfoArray, 0);
    fieldInfoArray[fieldInfoArray.Length - 1] = field;
    this.AllFields = fieldInfoArray;
  }

  public void InsertField(int insertIndex, FieldInfo field)
  {
    if (insertIndex < 0 || insertIndex > this.AllFields.Length)
      throw new ArgumentOutOfRangeException(nameof (insertIndex), "insertIndex must be > 0 and < AllFields.Length + 1 ");
    FieldInfo[] destinationArray = new FieldInfo[this.AllFields.Length + 1];
    Array.Copy((Array) this.AllFields, 0, (Array) destinationArray, 0, insertIndex);
    destinationArray[insertIndex] = field;
    Array.Copy((Array) this.AllFields, insertIndex, (Array) destinationArray, insertIndex + 1, this.AllFields.Length - insertIndex);
    this.AllFields = destinationArray;
  }

  private string DebuggerDisplay()
  {
    StringBuilder stringBuilder = new StringBuilder(nameof (PXViewDescription));
    if (!string.IsNullOrWhiteSpace(this.ViewName))
    {
      stringBuilder.Append(": \"").Append(this.ViewName).Append('"');
      if (this.Fields != null && this.AllFields != null)
        stringBuilder.Append(", ").Append(this.Fields.Length).Append('/').Append(this.AllFields.Length).Append(' ').Append(this.AllFields.Length != 1 ? "fields" : "field");
      if (this.IsGrid)
        stringBuilder.Append(" [GRID]");
    }
    return stringBuilder.ToString();
  }
}
