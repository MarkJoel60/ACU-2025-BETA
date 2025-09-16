// Decompiled with JetBrains decompiler
// Type: PX.Data.PXGuidAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data.SQLTree;
using System;

#nullable disable
namespace PX.Data;

/// <summary>Indicates a DAC field of <tt>Guid?</tt> type that is not
/// mapped to a database column.</summary>
/// <remarks>The attribute is added to the value declaration of a DAC field.
/// The field is not bound to a table column.</remarks>
/// <example>
/// <code>
/// [PXGuid]
/// [PXSelector(typeof(EPEmployee.userID),
///             SubstituteKey = typeof(EPEmployee.acctCD),
///             DescriptionField = typeof(EPEmployee.acctName))]
/// [PXUIField(DisplayName = "Custodian")]
/// public virtual Guid? Custodian { get; set; }
/// </code>
/// </example>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Parameter)]
public class PXGuidAttribute : 
  PXEventSubscriberAttribute,
  IPXFieldUpdatingSubscriber,
  IPXFieldSelectingSubscriber,
  IPXCommandPreparingSubscriber
{
  protected bool _IsKey;

  /// <summary>Gets or sets the value that indicates whether the field is a
  /// key field.</summary>
  public virtual bool IsKey
  {
    get => this._IsKey;
    set => this._IsKey = value;
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
  public virtual void CommandPreparing(PXCache sender, PXCommandPreparingEventArgs e)
  {
    if ((e.Operation & PXDBOperation.Delete) != PXDBOperation.Select)
      return;
    if (e.Value != null)
    {
      e.BqlTable = this._BqlTable;
      if (e.Expr == null)
        e.Expr = (SQLExpression) new Column(this._FieldName, e.BqlTable);
      e.DataValue = e.Value;
      e.DataLength = new int?(16 /*0x10*/);
    }
    e.DataType = PXDbType.UniqueIdentifier;
  }

  /// <exclude />
  public override void CacheAttached(PXCache sender)
  {
    if (!this.IsKey)
      return;
    sender.Keys.Add(this._FieldName);
  }
}
