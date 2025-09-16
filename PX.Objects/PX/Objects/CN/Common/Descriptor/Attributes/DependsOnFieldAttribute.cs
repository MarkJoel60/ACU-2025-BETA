// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Common.Descriptor.Attributes.DependsOnFieldAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.CN.Common.Descriptor.Attributes;

public class DependsOnFieldAttribute : PXEventSubscriberAttribute, IPXRowSelectedSubscriber
{
  public bool ShouldDisable = true;
  private readonly Type fieldType;

  public DependsOnFieldAttribute(Type fieldType) => this.fieldType = fieldType;

  public virtual void CacheAttached(PXCache cache)
  {
    // ISSUE: method pointer
    cache.Graph.RowUpdated.AddHandler(this.BqlTable, new PXRowUpdated((object) this, __methodptr(RowUpdated)));
  }

  public void RowSelected(PXCache cache, PXRowSelectedEventArgs args)
  {
    if (args.Row == null || !this.ShouldDisable)
      return;
    string field = cache.GetField(this.fieldType);
    object obj = cache.GetValue(args.Row, field);
    PXUIFieldAttribute.SetEnabled(cache, args.Row, this.FieldName, obj != null);
  }

  private void RowUpdated(PXCache cache, PXRowUpdatedEventArgs args)
  {
    string field = cache.GetField(this.fieldType);
    if (object.Equals(cache.GetValue(args.OldRow, field), cache.GetValue(args.Row, field)))
      return;
    cache.SetValue(args.Row, this.FieldName, (object) null);
    cache.RaiseExceptionHandling(this.FieldName, args.Row, (object) null, (Exception) null);
  }
}
