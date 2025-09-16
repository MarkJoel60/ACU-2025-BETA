// Decompiled with JetBrains decompiler
// Type: PX.Data.PXCheckDistinct
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <summary>Ensures that a DAC field has distinct values in all data records in a given context. It's similar to PXCheckUnique.</summary>
/// <remarks>
///   <para>The attribute is placed on the declaration of a DAC field, and ensures that this field has a unique value within the current context.</para>
/// </remarks>
/// <example>
///   <code title="" description="" lang="CS">
/// [PXDBString(30, IsKey = true)]
/// [PXUIField(DisplayName = "Mailing ID")]
/// [PXCheckDistinct]
/// public override string NotificationCD { get; set; }</code>
/// </example>
public class PXCheckDistinct : PXCheckUnique
{
  /// <summary>Initializes a new instance of the attribute.</summary>
  /// <param name="fields">Fields. The parameter is optional.</param>
  public PXCheckDistinct(params System.Type[] fields)
    : base(fields)
  {
    this.IgnoreNulls = false;
  }

  /// <exclude />
  public override void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    this.CreateViewOnCacheAttached(sender);
  }

  /// <exclude />
  public override void RowInserting(PXCache sender, PXRowInsertingEventArgs e)
  {
    if (e.Row == null)
      return;
    this.ValidateDuplicates(sender, e.Row, (object) null);
  }

  public override void RowUpdating(PXCache sender, PXRowUpdatingEventArgs e)
  {
    this.ClearErrors(sender, e.NewRow);
    if (e.Row != null && e.NewRow != null && this.CheckUpdated(sender, e.Row, e.NewRow))
      this.ValidateDuplicates(sender, e.NewRow, e.Row);
    if (!this.ClearOnDuplicate || !PXCheckUnique.CheckEquals(sender.GetValue(e.Row, this._FieldName), sender.GetValue(e.NewRow, this._FieldName)) || !e.Cancel)
      return;
    this.ClearErrors(sender, e.NewRow);
    sender.SetValue(e.NewRow, this._FieldName, (object) null);
    this.ValidateDuplicates(sender, e.NewRow, e.Row);
  }

  /// <exclude />
  public override void RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    if (e.Row == null || e.Operation == PXDBOperation.Delete)
      return;
    this.ValidateDuplicates(sender, e.Row, (object) null);
  }

  protected override bool CheckDefaults(PXCache sender, object row) => true;
}
