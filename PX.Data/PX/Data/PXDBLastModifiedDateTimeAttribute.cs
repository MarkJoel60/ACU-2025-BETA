// Decompiled with JetBrains decompiler
// Type: PX.Data.PXDBLastModifiedDateTimeAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <summary>Maps a DAC field to the database column and automatically sets the field value to the data record's last modification date and time in UTC.</summary>
/// <remarks>The attribute is added to the value declaration of a DAC field. The field data type should be <tt>DateTime?</tt>.</remarks>
/// <example>
///   <code title="Example" description="" lang="CS">
/// [PXDBLastModifiedDateTime]
/// public virtual DateTime? LastModifiedDateTime { get; set; }</code>
/// </example>
public class PXDBLastModifiedDateTimeAttribute : 
  PXDBCreatedDateTimeAttribute,
  IPXCommandPreparingSubscriber,
  IPXRowUpdatingSubscriber
{
  void IPXRowUpdatingSubscriber.RowUpdating(PXCache sender, PXRowUpdatingEventArgs e)
  {
    if (sender.GetValue(e.NewRow, this._FieldOrdinal) != null)
      return;
    sender.SetValue(e.NewRow, this._FieldOrdinal, (object) this.GetDate());
  }

  void IPXCommandPreparingSubscriber.CommandPreparing(PXCache sender, PXCommandPreparingEventArgs e)
  {
    if ((e.Operation & PXDBOperation.Insert) == PXDBOperation.Insert || (e.Operation & PXDBOperation.Update) == PXDBOperation.Update)
    {
      e.DataLength = new int?(8);
      e.IsRestriction = e.IsRestriction || this._IsKey;
      this.PrepareFieldName(this._DatabaseFieldName, e);
      e.DataType = PXDbType.DirectExpression;
      e.DataValue = this.UseTimeZone ? (object) e.SqlDialect.GetUtcDate : (object) e.SqlDialect.GetDate;
      sender.SetValue(e.Row, this._FieldOrdinal, (object) this.GetDate());
    }
    else
      this.CommandPreparing(sender, e);
  }
}
