// Decompiled with JetBrains decompiler
// Type: PX.Data.PXDBIntAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Globalization;

#nullable disable
namespace PX.Data;

/// <summary>Maps a DAC field of <tt>int?</tt> type to the database column of <tt>int</tt> type.</summary>
/// <remarks>The attribute is added to the value declaration of a DAC field. The field becomes bound to the database column with the same name.</remarks>
/// <example><para>The attribute below maps a field to the database column and explicitly sets the minimum and maximum values for the field.</para>
///   <code title="Example" lang="CS">
/// [PXDBInt(MinValue = 0, MaxValue = 365)]
/// public virtual int? ReceiptTranDaysBefore { get; set; }</code>
/// <code title="Example2" description="The attribute below maps a field to the database column and sets the properties inherited from the PXDBField attribute." groupname="Example" lang="CS">
/// [PXDBInt(IsKey = true, BqlField = typeof(CuryARHistory.branchID))]
/// [PXSelector(typeof(Branch.branchID),
///             SubstituteKey = typeof(Branch.branchCD))]
/// public virtual int? BranchID { get; set; }</code>
/// </example>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Parameter)]
public class PXDBIntAttribute : 
  PXDBFieldAttribute,
  IPXRowSelectingSubscriber,
  IPXCommandPreparingSubscriber,
  IPXFieldUpdatingSubscriber,
  IPXFieldSelectingSubscriber,
  IPXFieldVerifyingSubscriber
{
  protected int _MinValue = int.MinValue;
  protected int _MaxValue = int.MaxValue;

  /// <summary>Gets or sets the minimum value for the field.</summary>
  /// <example>
  /// The attribute below maps a field to the database column and
  /// explicitly sets the minimum and maximum values for the field.
  /// <code>
  /// [PXDBInt(MinValue = 0, MaxValue = 365)]
  /// public virtual int? ReceiptTranDaysBefore { get; set; }
  /// </code>
  /// </example>
  public int MinValue
  {
    get => this._MinValue;
    set => this._MinValue = value;
  }

  /// <summary>Gets or sets the maximum value for the field.</summary>
  public int MaxValue
  {
    get => this._MaxValue;
    set => this._MaxValue = value;
  }

  /// <exclude />
  protected override void PrepareCommandImpl(string dbFieldName, PXCommandPreparingEventArgs e)
  {
    base.PrepareCommandImpl(dbFieldName, e);
    e.DataType = PXDbType.Int;
    e.DataLength = new int?(4);
  }

  /// <exclude />
  public override void RowSelecting(PXCache sender, PXRowSelectingEventArgs e)
  {
    if (e.Row != null)
    {
      if (this._IsKey && !e.IsReadOnly)
      {
        int? int32;
        sender.SetValue(e.Row, this._FieldOrdinal, (object) (int32 = e.Record.GetInt32(e.Position)));
        if ((!int32.HasValue || sender.IsPresent(e.Row)) && sender.Graph.GetType() != typeof (PXGraph))
          e.Row = (object) null;
      }
      else
        sender.SetValue(e.Row, this._FieldOrdinal, (object) e.Record.GetInt32(e.Position));
    }
    ++e.Position;
  }

  /// <exclude />
  public virtual void FieldUpdating(PXCache sender, PXFieldUpdatingEventArgs e)
  {
    if (!(e.NewValue is string))
      return;
    int result;
    if (int.TryParse((string) e.NewValue, NumberStyles.Any, (IFormatProvider) sender.Graph.Culture, out result))
      e.NewValue = (object) result;
    else
      e.NewValue = (object) null;
  }

  /// <exclude />
  public virtual void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    if (this._AttributeLevel != PXAttributeLevel.Item && !e.IsAltered)
      return;
    e.ReturnState = (object) PXIntState.CreateInstance(e.ReturnState, this._FieldName, new bool?(this._IsKey), new int?(-1), new int?(this._MinValue), new int?(this._MaxValue), (int[]) null, (string[]) null, typeof (int), new int?());
  }

  public virtual void FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    if (!(e.NewValue is int newValue))
      return;
    if (newValue < this._MinValue)
      throw new PXSetPropertyException(e.Row as IBqlTable, "The value must be greater than or equal to {0}.", new object[1]
      {
        (object) this._MinValue
      });
    if (newValue > this._MaxValue)
      throw new PXSetPropertyException(e.Row as IBqlTable, "The value must be less than or equal to {0}.", new object[1]
      {
        (object) this._MaxValue
      });
  }
}
