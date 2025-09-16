// Decompiled with JetBrains decompiler
// Type: PX.Data.PXDBLastChangeDateTimeAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;

#nullable disable
namespace PX.Data;

/// <summary>Maps a DAC field to the database column and automatically sets the field value to the last modification date and time (in UTC) of the specified field.</summary>
/// <remarks>The attribute is added to the value declaration of a DAC field. The field data type should be <tt>DateTime?</tt>.</remarks>
/// <example><para>In the following code, the modification date and time of the &lt;tt&gt;StatusDate&lt;/tt&gt; field is updated each time &lt;tt&gt;CRCase.status&lt;/tt&gt; is updated.</para>
///   <code title="Example" lang="CS">
/// [PXDBLastChangeDateTime(typeof(CRCase.status))]
/// public virtual DateTime? StatusDate { get; set; }</code>
/// </example>
public class PXDBLastChangeDateTimeAttribute : PXDBDateAttribute, IPXRowUpdatingSubscriber
{
  private readonly System.Type _MonitoredField;

  /// <summary>Initializes a new instance of the attribute that monitors the specified field. On each modification of the monitored field, the attribute updates the
  /// modification date and time of the field that is marked with the attribute.</summary>
  /// <param name="monitoredField">The field to monitor. You specify a type that implements IBqlField.</param>
  public PXDBLastChangeDateTimeAttribute(System.Type monitoredField)
  {
    this.UseSmallDateTime = false;
    this.PreserveTime = true;
    base.UseTimeZone = true;
    if (monitoredField == (System.Type) null)
      throw new PXArgumentException(nameof (monitoredField), "The argument cannot be null.");
    this._MonitoredField = typeof (IBqlField).IsAssignableFrom(monitoredField) ? monitoredField : throw new PXArgumentException(nameof (monitoredField), "An invalid argument has been specified.");
  }

  /// <exclude />
  public System.DateTime GetDate()
  {
    System.DateTime? serverDateTime = PXTransactionScope.GetServerDateTime(true);
    return !serverDateTime.HasValue ? PXTimeZoneInfo.Now : PXTimeZoneInfo.ConvertTimeFromUtc(serverDateTime.Value, this.GetTimeZone());
  }

  /// <exclude />
  public override bool UseTimeZone
  {
    get => base.UseTimeZone;
    set
    {
    }
  }

  void IPXRowUpdatingSubscriber.RowUpdating(PXCache sender, PXRowUpdatingEventArgs e)
  {
    if (sender.GetValue(e.NewRow, this._FieldOrdinal) != null)
      return;
    sender.SetValue(e.NewRow, this._FieldOrdinal, (object) this.GetDate());
  }

  /// <exclude />
  public override void CommandPreparing(PXCache sender, PXCommandPreparingEventArgs e)
  {
    base.CommandPreparing(sender, e);
    if ((e.Operation & PXDBOperation.Update) != PXDBOperation.Update && (e.Operation & PXDBOperation.Insert) != PXDBOperation.Insert)
      return;
    e.DataLength = new int?(8);
    e.IsRestriction = e.IsRestriction || this._IsKey;
    this.PrepareFieldName(this._DatabaseFieldName, e);
    if (object.Equals(sender.GetValueOriginal(e.Row, this._MonitoredField.Name), sender.GetValue(e.Row, this._MonitoredField.Name)))
      return;
    e.DataValue = this.UseTimeZone ? (object) e.SqlDialect.GetUtcDate : (object) e.SqlDialect.GetDate;
    e.DataType = PXDbType.DirectExpression;
    sender.SetValue(e.Row, this._FieldOrdinal, (object) this.GetDate());
  }

  /// <exclude />
  [Serializable]
  public class SelectedValue : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXString(IsKey = true)]
    public virtual string FieldName { get; set; }

    public virtual object Value { get; set; }
  }
}
