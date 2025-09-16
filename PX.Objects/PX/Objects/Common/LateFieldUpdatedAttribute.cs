// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.LateFieldUpdatedAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.Common;

/// <summary>
/// Represents an event handler for the pseudo FieldUpdated event that subscribes as late as possible.
/// Though this handler looks like a field event handler,
/// in fact it uses the RowUpdated event in which it checks if the value of the target field is changed,
/// and if yes - calls the <see cref="M:PX.Objects.Common.LateFieldUpdatedAttribute.LateFieldUpdated(PX.Data.PXCache,PX.Data.PXFieldUpdatedEventArgs)" /> method.
/// </summary>
public abstract class LateFieldUpdatedAttribute : LateRowUpdatedAttribute
{
  protected override void LateRowUpdated(PXCache cache, PXRowUpdatedEventArgs args)
  {
    if (args.Row == null || args.OldRow == null)
      return;
    object objA = cache.GetValue(args.Row, this.FieldOrdinal);
    object objB = cache.GetValue(args.OldRow, this.FieldOrdinal);
    if (object.Equals(objA, objB))
      return;
    this.LateFieldUpdated(cache, new PXFieldUpdatedEventArgs(args.Row, objB, args.ExternalCall));
  }

  protected abstract void LateFieldUpdated(PXCache cache, PXFieldUpdatedEventArgs args);
}
