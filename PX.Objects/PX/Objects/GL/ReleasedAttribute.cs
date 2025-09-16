// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.ReleasedAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.GL;

[PXDBBool]
public class ReleasedAttribute : PXAggregateAttribute, IPXRowDeletingSubscriber
{
  public bool PreventDeletingReleased { get; set; }

  public void RowDeleting(PXCache sender, PXRowDeletingEventArgs e)
  {
    if (this.PreventDeletingReleased && sender.GetValue(e.Row, ((PXEventSubscriberAttribute) this)._FieldName) != null && ((bool?) sender.GetValue(e.Row, ((PXEventSubscriberAttribute) this)._FieldName)).GetValueOrDefault())
      throw new PXException("Released documents cannot be deleted.");
  }
}
