// Decompiled with JetBrains decompiler
// Type: PX.Data.PXDBCreatedDateTimeAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <summary>Maps a DAC field to the database column and automatically sets the field value to the data record's creation date and time in UTC.</summary>
/// <remarks>The attribute is added to the value declaration of a DAC field.
/// The field data type should be <tt>DateTime?</tt>.</remarks>
/// <example>
///   <code title="" description="" lang="CS">
/// [PXDBCreatedDateTime()]
/// public virtual DateTime? CreatedDateTime { get; set; }</code>
/// </example>
public class PXDBCreatedDateTimeAttribute : 
  PXDBBaseDateTimeAttribute,
  IPXCommandPreparingSubscriber,
  IPXRowInsertingSubscriber
{
  void IPXRowInsertingSubscriber.RowInserting(PXCache sender, PXRowInsertingEventArgs e)
  {
    if (sender.GetValue(e.Row, this._FieldOrdinal) != null)
      return;
    sender.SetValue(e.Row, this._FieldOrdinal, (object) this.GetDate());
  }

  void IPXCommandPreparingSubscriber.CommandPreparing(PXCache sender, PXCommandPreparingEventArgs e)
  {
    if ((e.Operation & PXDBOperation.Insert) == PXDBOperation.Insert)
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
    if ((e.Operation & PXDBOperation.Delete) != PXDBOperation.Update)
      return;
    e.ExcludeFromInsertUpdate();
  }
}
