// Decompiled with JetBrains decompiler
// Type: PX.Data.PXDBCreatedByScreenIDAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <summary>Maps a DAC field to the database column and automatically
/// sets the field value to the string ID of the application screen that
/// created the data record.</summary>
/// <remarks>The attribute is added to the value declaration of a DAC field.
/// The field data type should be <tt>string</tt>.</remarks>
/// <example>
/// <code>
/// [PXDBCreatedByScreenID()]
/// public virtual string CreatedByScreenID { get; set; }
/// </code>
/// </example>
public class PXDBCreatedByScreenIDAttribute : 
  PXDBBaseScreenIDAttribute,
  IPXRowInsertingSubscriber,
  IPXCommandPreparingSubscriber
{
  protected virtual bool ExcludeFromUpdate => true;

  void IPXRowInsertingSubscriber.RowInserting(PXCache sender, PXRowInsertingEventArgs e)
  {
    sender.SetValue(e.Row, this._FieldOrdinal, (object) this.GetScreenID(sender));
  }

  void IPXCommandPreparingSubscriber.CommandPreparing(PXCache sender, PXCommandPreparingEventArgs e)
  {
    this.CommandPreparing(sender, e);
    if (!this.ExcludeFromUpdate || (e.Operation & PXDBOperation.Delete) != PXDBOperation.Update)
      return;
    e.ExcludeFromInsertUpdate();
  }
}
