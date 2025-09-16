// Decompiled with JetBrains decompiler
// Type: PX.Data.PXRefNoteAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data;

/// <summary>
/// Marks a DAC field that holds a reference to a data record
/// of any DAC type through the note ID.
/// </summary>
/// <remarks>
/// <para>The field marked with the <tt>PXRefNote</tt> attribute is typically
/// represented by the <tt>PXRefNoteSelector</tt> control on the ASPX page.
/// Through this control, a user can select a data records from any table.
/// A data record is referenced through its <tt>NoteID</tt> field, whose
/// value is written to the field associated with the <tt>PXRefNoteSelector</tt> control
/// (and marked with the <tt>PXRefNote</tt> attribute). The field should
/// be of the <tt>Guid?</tt> data type.</para>
/// <para>In the UI, the <tt>PXRefNoteSelector</tt> control displays the
/// selected data record's description, which is composed of the fields
/// that are marked with the
/// <see cref="T:PX.Data.EP.PXFieldDescriptionAttribute">PXFieldDescription</see>
/// attribute.</para>
/// </remarks>
/// <seealso cref="T:PX.Data.PXNoteAttribute" />
/// <seealso cref="T:PX.Data.EP.PXFieldDescriptionAttribute" />
/// <example>
/// The code below shows the usage of the <tt>PXRefNote</tt> attribute in
/// the defition of a DAC field.
/// <code>
/// [PXRefNote]
/// [PXUIField(DisplayName = "References Nbr.")]
/// public virtual Guid? RefNoteID { get; set; }
/// </code>
/// </example>
public class PXRefNoteAttribute : PXDBGuidAttribute
{
  protected EntityHelper helper;

  /// <summary>Get, set.</summary>
  public bool FullDescription { get; set; }

  public bool LastKeyOnly { get; set; }

  /// <exclude />
  public override void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    this.helper = new EntityHelper(sender.Graph);
  }

  /// <exclude />
  public override void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    using (new PXReadBranchRestrictedScope())
    {
      Guid? nullable = (Guid?) sender.GetValue(e.Row, this._FieldOrdinal);
      string str = string.Empty;
      if (nullable.HasValue)
        str = this.FullDescription ? this.helper.GetEntityRowValues(new Guid?(nullable.Value)) : (this.LastKeyOnly ? this.helper.GetEntityRowID(new Guid?(nullable.Value), (string) null) : this.helper.GetEntityRowID(new Guid?(nullable.Value)));
      e.ReturnValue = (object) PXStringState.CreateInstance((object) str, new int?(), new bool?(), this._FieldName, new bool?(this._IsKey), new int?(), (string) null, (string[]) null, (string[]) null, new bool?(), (string) null);
    }
  }

  /// <exclude />
  public override void CommandPreparing(PXCache sender, PXCommandPreparingEventArgs e)
  {
    if ((e.Operation & PXDBOperation.External) == PXDBOperation.External)
      return;
    base.CommandPreparing(sender, e);
  }

  public PXRefNoteAttribute()
    : base()
  {
  }
}
