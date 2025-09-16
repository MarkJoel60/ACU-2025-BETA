// Decompiled with JetBrains decompiler
// Type: PX.Data.PXUniqueNoteAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data;

/// <exclude />
public class PXUniqueNoteAttribute : PXNoteAttribute, IPXRowInsertingSubscriber
{
  public PXUniqueNoteAttribute()
  {
  }

  [Obsolete("The constructor is obsolete. Use the parameterless constructor instead. The PXUniqueNoteAttribute constructor is exactly the same as the parameterless one. It does not provide any additional functionality and does not save values of provided fields in the note. The constructor will be removed in a future version of Acumatica ERP.")]
  public PXUniqueNoteAttribute(params System.Type[] searches)
    : base(searches)
  {
  }

  public virtual void RowInserting(PXCache sender, PXRowInsertingEventArgs e)
  {
    if (this.IsKey)
      return;
    sender.SetValue(e.Row, this._FieldOrdinal, (object) PXNoteAttribute.GenerateId());
  }
}
