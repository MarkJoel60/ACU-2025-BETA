// Decompiled with JetBrains decompiler
// Type: PX.Data.PXDBBoolAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data;

/// <summary>Maps a DAC field of <tt>bool?</tt> type to the database
/// column of <tt>bit</tt> type.</summary>
/// <remarks>The attribute is added to the value declaration of a DAC field.
/// The field becomes bound to the database column with the same
/// name.</remarks>
/// <example>
/// <code>
/// [PXDBBool()]
/// [PXDefault(false)]
/// public virtual Boolean? Scheduled { get; set; }
/// </code>
/// </example>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Parameter)]
public class PXDBBoolAttribute : 
  PXDBFieldAttribute,
  IPXRowSelectingSubscriber,
  IPXCommandPreparingSubscriber,
  IPXFieldUpdatingSubscriber,
  IPXFieldSelectingSubscriber
{
  /// <exclude />
  protected override void PrepareCommandImpl(string dbFieldName, PXCommandPreparingEventArgs e)
  {
    base.PrepareCommandImpl(dbFieldName, e);
    e.DataType = PXDbType.Bit;
    e.DataLength = new int?(1);
  }

  /// <exclude />
  public override void RowSelecting(PXCache sender, PXRowSelectingEventArgs e)
  {
    if (e.Row != null)
      sender.SetValue(e.Row, this._FieldOrdinal, (object) e.Record.GetBoolean(e.Position));
    ++e.Position;
  }

  /// <exclude />
  public virtual void FieldUpdating(PXCache sender, PXFieldUpdatingEventArgs e)
  {
    PXBoolAttribute.ConvertValue(e);
  }

  /// <exclude />
  public virtual void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    if (this._AttributeLevel != PXAttributeLevel.Item && !e.IsAltered)
      return;
    e.ReturnState = (object) PXFieldState.CreateInstance(e.ReturnState, typeof (bool), new bool?(this._IsKey), required: new int?(-1), fieldName: this._FieldName);
  }
}
