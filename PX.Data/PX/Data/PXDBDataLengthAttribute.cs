// Decompiled with JetBrains decompiler
// Type: PX.Data.PXDBDataLengthAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.SQLTree;
using System;

#nullable disable
namespace PX.Data;

/// <exclude />
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Parameter)]
public class PXDBDataLengthAttribute : 
  PXEventSubscriberAttribute,
  IPXCommandPreparingSubscriber,
  IPXRowSelectingSubscriber
{
  private string _TargetFieldName;

  public PXDBDataLengthAttribute(System.Type targetField)
  {
    this._TargetFieldName = targetField.Name;
  }

  public PXDBDataLengthAttribute(string targetFieldName) => this._TargetFieldName = targetFieldName;

  public virtual void CommandPreparing(PXCache sender, PXCommandPreparingEventArgs e)
  {
    System.Type type = e.Table;
    if ((object) type == null)
      type = this._BqlTable;
    System.Type dac = type;
    if ((e.Operation & PXDBOperation.Delete) != PXDBOperation.Select)
      return;
    e.BqlTable = this._BqlTable;
    e.Expr = new Column(this._TargetFieldName, dac, PXDbType.BigInt).BinaryLength();
  }

  public void RowSelecting(PXCache sender, PXRowSelectingEventArgs e)
  {
    if (e.Row != null)
      sender.SetValue(e.Row, this._FieldOrdinal, e.Record.GetValue(e.Position));
    ++e.Position;
  }
}
