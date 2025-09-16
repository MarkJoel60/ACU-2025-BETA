// Decompiled with JetBrains decompiler
// Type: PX.Data.PXNoteState
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <exclude />
public class PXNoteState : PXFieldState
{
  protected string _NoteIDField;
  protected string _NoteDocsField;
  protected string _NoteTextField;
  protected string _NoteActivityField;
  protected bool _AddFileEnabled;
  protected string _NoteTextExistsField;
  protected string _NoteFilesExistsField;

  protected PXNoteState(object value)
    : base(value)
  {
    this._DataType = typeof (string);
    this._Nullable = false;
  }

  public virtual string NoteIDField => this._NoteIDField;

  public virtual string NoteDocsField => this._NoteDocsField;

  public virtual string NoteTextField => this._NoteTextField;

  public virtual string NoteActivityField => this._NoteActivityField;

  public virtual string NoteTextExistsField => this._NoteTextExistsField;

  public virtual string NoteFilesExistsField => this._NoteFilesExistsField;

  public static PXFieldState CreateInstance(
    object value,
    string fieldName,
    string noteIDField,
    string noteTextField,
    string noteDocsField,
    string noteActivityField,
    string noteTextExistsField = null,
    string noteFilesExistsField = null,
    string noteTextDisplayName = null)
  {
    switch (value)
    {
      case PXNoteState instance1:
label_4:
        if (noteTextDisplayName != null)
          instance1._DisplayName = noteTextDisplayName;
        if (fieldName != null)
          instance1._FieldName = fieldName;
        if (noteIDField != null)
          instance1._NoteIDField = noteIDField;
        if (noteTextField != null)
          instance1._NoteTextField = noteTextField;
        if (noteDocsField != null)
          instance1._NoteDocsField = noteDocsField;
        if (noteActivityField != null)
          instance1._NoteActivityField = noteActivityField;
        if (noteTextExistsField != null)
          instance1._NoteTextExistsField = noteTextExistsField;
        if (noteFilesExistsField != null)
          instance1._NoteFilesExistsField = noteFilesExistsField;
        return (PXFieldState) instance1;
      case PXFieldState instance2:
        if (instance2.DataType != typeof (object) && instance2.DataType != typeof (string))
          return instance2;
        goto default;
      default:
        instance1 = new PXNoteState(value);
        goto label_4;
    }
  }
}
