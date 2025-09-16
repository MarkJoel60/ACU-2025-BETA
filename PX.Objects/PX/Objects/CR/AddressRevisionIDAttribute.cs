// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.AddressRevisionIDAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;

#nullable disable
namespace PX.Objects.CR;

public class AddressRevisionIDAttribute : 
  PXEventSubscriberAttribute,
  IPXRowPersistingSubscriber,
  IPXRowPersistedSubscriber
{
  private bool IsInsertOrUpdateOperation(PXDBOperation operation)
  {
    return EnumerableExtensions.IsIn<PXDBOperation>(PXDBOperationExt.Command(operation), (PXDBOperation) 2, (PXDBOperation) 1);
  }

  public virtual void RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    if (!this.IsInsertOrUpdateOperation(e.Operation))
      return;
    int? nullable1 = new int?(((int?) sender.GetValue(e.Row, this._FieldOrdinal)).GetValueOrDefault());
    int? nullable2 = nullable1.HasValue ? new int?(nullable1.GetValueOrDefault() + 1) : new int?();
    sender.SetValue(e.Row, this._FieldOrdinal, (object) nullable2);
  }

  public virtual void RowPersisted(PXCache sender, PXRowPersistedEventArgs e)
  {
    if (e.TranStatus != 2 || !this.IsInsertOrUpdateOperation(e.Operation))
      return;
    int? nullable1 = (int?) sender.GetValue(e.Row, this._FieldOrdinal);
    int? nullable2 = nullable1.HasValue ? new int?(nullable1.GetValueOrDefault() - 1) : new int?();
    sender.SetValue(e.Row, this._FieldOrdinal, (object) nullable2);
  }
}
