// Decompiled with JetBrains decompiler
// Type: PX.Data.PXSequentialSelfRefNoteAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;

#nullable disable
namespace PX.Data;

/// <exclude />
public class PXSequentialSelfRefNoteAttribute : PXSequentialNoteAttribute
{
  public PXSequentialSelfRefNoteAttribute()
  {
  }

  [Obsolete("The constructor is obsolete. Use the parameterless constructor instead. The PXSequentialSelfRefNoteAttribute constructor is exactly the same as the parameterless one. It does not provide any additional functionality and does not save values of provided fields in the note. The constructor will be removed in a future version of Acumatica ERP.")]
  public PXSequentialSelfRefNoteAttribute(params System.Type[] searches)
    : base(searches)
  {
  }

  public System.Type NoteField { get; set; }

  public bool Persistent { get; set; }

  public override void FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
  {
    if (this.NoteField != (System.Type) null && e.NewValue == null)
      e.NewValue = sender.GetValue(e.Row, this.NoteField.Name);
    else
      base.FieldDefaulting(sender, e);
  }

  public override void RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    if (!(sender.GetValue(e.Row, this._FieldOrdinal) is Guid id))
      return;
    if (e.Operation.Command() == PXDBOperation.Delete)
    {
      PXCache cach1 = sender.Graph.Caches[typeof (Note)];
      PXCache cach2 = sender.Graph.Caches[typeof (NoteDoc)];
      if (this._ForceRetain || cach2.Deleted.Count() <= 0L && cach1.Deleted.Count() <= 0L)
        return;
      this.ExecuteDeleteOnRowPersisting(sender, e, cach1, cach2, id);
    }
    else
    {
      if (!this.Persistent)
        return;
      base.RowPersisting(sender, e);
    }
  }
}
