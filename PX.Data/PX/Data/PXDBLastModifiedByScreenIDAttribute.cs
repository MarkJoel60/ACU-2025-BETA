// Decompiled with JetBrains decompiler
// Type: PX.Data.PXDBLastModifiedByScreenIDAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <summary>Maps a DAC field to the database column and automatically
/// sets the field value to the string ID of the application screen on
/// which the data record was modified the last time.</summary>
/// <remarks>The attribute is added to the value declaration of a DAC field.
/// The field data type should be <tt>string</tt>.</remarks>
/// <example>
/// <code>
/// [PXDBLastModifiedByScreenID()]
/// public virtual string LastModifiedByScreenID { get; set; }
/// </code>
/// </example>
public class PXDBLastModifiedByScreenIDAttribute : 
  PXDBCreatedByScreenIDAttribute,
  IPXRowUpdatingSubscriber,
  IPXRowPersistingSubscriber
{
  protected override bool ExcludeFromUpdate => false;

  void IPXRowUpdatingSubscriber.RowUpdating(PXCache sender, PXRowUpdatingEventArgs e)
  {
    sender.SetValue(e.NewRow, this._FieldOrdinal, (object) this.GetScreenID(sender));
  }

  void IPXRowPersistingSubscriber.RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    if ((e.Operation & PXDBOperation.Delete) == PXDBOperation.Delete)
      return;
    sender.SetValue(e.Row, this._FieldOrdinal, (object) this.GetScreenID(sender));
  }
}
