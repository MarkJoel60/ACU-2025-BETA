// Decompiled with JetBrains decompiler
// Type: PX.Data.PXDBRevision
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data;

/// <exclude />
public class PXDBRevision : PXDBIntAttribute, IPXRowUpdatingSubscriber
{
  private readonly System.Type _MonitoredField;

  public PXDBRevision(System.Type monitoredField)
  {
    if (monitoredField == (System.Type) null)
      throw new PXArgumentException(nameof (monitoredField), "The argument cannot be null.");
    this._MonitoredField = typeof (IBqlField).IsAssignableFrom(monitoredField) ? monitoredField : throw new PXArgumentException(nameof (monitoredField), "An invalid argument has been specified.");
  }

  void IPXRowUpdatingSubscriber.RowUpdating(PXCache sender, PXRowUpdatingEventArgs e)
  {
    if (sender.GetValue(e.NewRow, this._FieldOrdinal) != null)
      return;
    sender.SetValue(e.NewRow, this._FieldOrdinal, (object) 0);
  }

  public override void CommandPreparing(PXCache sender, PXCommandPreparingEventArgs e)
  {
    base.CommandPreparing(sender, e);
    if ((e.Operation & PXDBOperation.Update) != PXDBOperation.Update && (e.Operation & PXDBOperation.Insert) != PXDBOperation.Insert)
      return;
    e.DataLength = new int?(4);
    e.IsRestriction = e.IsRestriction || this._IsKey;
    this.PrepareFieldName(this._DatabaseFieldName, e);
    e.DataType = PXDbType.Int;
    int? nullable1 = (int?) sender.GetValue(e.Row, this._FieldOrdinal);
    object valueOriginal = sender.GetValueOriginal(e.Row, this._MonitoredField.Name);
    if (valueOriginal != null && !object.Equals(valueOriginal, sender.GetValue(e.Row, this._MonitoredField.Name)))
    {
      PXCommandPreparingEventArgs preparingEventArgs = e;
      int? nullable2 = nullable1;
      // ISSUE: variable of a boxed type
      __Boxed<int?> local1 = (ValueType) (nullable2.HasValue ? new int?(nullable2.GetValueOrDefault() + 1) : new int?());
      preparingEventArgs.DataValue = (object) local1;
      PXCache pxCache = sender;
      object row = e.Row;
      int fieldOrdinal = this._FieldOrdinal;
      nullable2 = nullable1;
      // ISSUE: variable of a boxed type
      __Boxed<int?> local2 = (ValueType) (nullable2.HasValue ? new int?(nullable2.GetValueOrDefault() + 1) : new int?());
      pxCache.SetValue(row, fieldOrdinal, (object) local2);
    }
    else
      e.DataValue = (object) nullable1;
  }
}
