// Decompiled with JetBrains decompiler
// Type: PX.Data.PXDBGuidNotNullAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

public class PXDBGuidNotNullAttribute : 
  PXDBGuidAttribute,
  IPXRowPersistingSubscriber,
  IPXRowInsertingSubscriber
{
  public PXDBGuidNotNullAttribute()
    : base(true)
  {
  }

  public virtual void RowInserting(PXCache sender, PXRowInsertingEventArgs e)
  {
    sender.SetValue(e.Row, this._FieldOrdinal, (object) SequentialGuid.Generate());
  }

  public virtual void RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    if ((e.Operation & PXDBOperation.Delete) != PXDBOperation.Insert || sender.GetValue(e.Row, this._FieldOrdinal) != null)
      return;
    sender.SetValue(e.Row, this._FieldOrdinal, (object) SequentialGuid.Generate());
  }
}
