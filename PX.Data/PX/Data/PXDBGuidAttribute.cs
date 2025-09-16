// Decompiled with JetBrains decompiler
// Type: PX.Data.PXDBGuidAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;

#nullable disable
namespace PX.Data;

/// <summary>Map a DAC field of <tt>Guid?</tt> type to the database column
/// of <tt>uniqueidentifier</tt> type.</summary>
/// <remarks>The attribute is added to the value declaration of a DAC field.
/// The field becomes bound to the database column with the same
/// name.</remarks>
/// <example><para>The attribute below binds the field to the unique identifier column and assigns a default value to the field.</para>
/// <code title="Example" lang="CS">
/// [PXDBGuid(true)]
/// public virtual Guid? SetupID { get; set; }</code>
/// <code title="Example1" description="The attribute below binds the field to the unique identifier column. The field becomes a key field." groupname="Example" lang="CS">
/// [PXDBGuid(IsKey = true)]
/// public virtual Guid? SetupID { get; set; }</code>
/// </example>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Parameter)]
public class PXDBGuidAttribute : 
  PXDBFieldAttribute,
  IPXRowSelectingSubscriber,
  IPXCommandPreparingSubscriber,
  IPXFieldUpdatingSubscriber,
  IPXFieldSelectingSubscriber,
  IPXFieldDefaultingSubscriber
{
  private readonly bool _withDefaulting;

  /// <summary>Initializes a new instance of the attribute.</summary>
  /// <param name="withDefaulting">Indicates whether the default
  /// value should be set for the field.</param>
  public PXDBGuidAttribute(bool withDefaulting = false) => this._withDefaulting = withDefaulting;

  /// <exclude />
  protected override void PrepareCommandImpl(string dbFieldName, PXCommandPreparingEventArgs e)
  {
    base.PrepareCommandImpl(dbFieldName, e);
    e.DataType = PXDbType.UniqueIdentifier;
    e.DataLength = new int?(16 /*0x10*/);
  }

  /// <exclude />
  public override void RowSelecting(PXCache sender, PXRowSelectingEventArgs e)
  {
    if (e.Row != null)
      sender.SetValue(e.Row, this._FieldOrdinal, (object) e.Record.GetGuid(e.Position));
    ++e.Position;
  }

  /// <exclude />
  public virtual void FieldUpdating(PXCache sender, PXFieldUpdatingEventArgs e)
  {
    if (!(e.NewValue is string))
      return;
    Guid guid;
    if (GUID.TryParse((string) e.NewValue, ref guid))
      e.NewValue = (object) guid;
    else
      e.NewValue = (object) null;
  }

  /// <exclude />
  public virtual void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    if (this._AttributeLevel != PXAttributeLevel.Item && !e.IsAltered)
      return;
    e.ReturnState = (object) PXGuidState.CreateInstance(e.ReturnState, this._FieldName, new bool?(this._IsKey), new int?(-1));
  }

  /// <exclude />
  public virtual void FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
  {
    if (!this._withDefaulting)
      return;
    e.NewValue = (object) this.newGuid();
  }

  protected virtual Guid newGuid() => Guid.NewGuid();
}
