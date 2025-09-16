// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CombinedDBStringListsAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.CR;

/// <summary>
/// Combines all <see cref="T:PX.Data.PXStringListAttribute" /> from specified fields and append SQL query during select.
/// </summary>
public abstract class CombinedDBStringListsAttribute : 
  CombinedStringListsAttribute,
  IPXCommandPreparingSubscriber,
  IPXRowSelectingSubscriber
{
  public CombinedDBStringListsAttribute(System.Type table, params System.Type[] fields)
    : base(fields)
  {
    this.Table = table;
  }

  protected System.Type Table { get; }

  public virtual void CommandPreparing(PXCache sender, PXCommandPreparingEventArgs e)
  {
    if (!e.IsSelect())
      return;
    e.BqlTable = this.Table;
    this.PrepareExpression(sender, e);
  }

  protected virtual void PrepareExpression(PXCache cache, PXCommandPreparingEventArgs e)
  {
    for (int fieldIndex = 0; fieldIndex < this.Fields.Count; ++fieldIndex)
      this.PrepareFieldExpression(cache, e, fieldIndex);
  }

  protected abstract void PrepareFieldExpression(
    PXCache cache,
    PXCommandPreparingEventArgs e,
    int fieldIndex);

  public virtual void RowSelecting(PXCache sender, PXRowSelectingEventArgs e)
  {
    if (e.Row != null)
      sender.SetValue(e.Row, ((PXEventSubscriberAttribute) this)._FieldOrdinal, Extensions.GetValue(e.Record, e.Position, typeof (string)));
    ++e.Position;
  }
}
