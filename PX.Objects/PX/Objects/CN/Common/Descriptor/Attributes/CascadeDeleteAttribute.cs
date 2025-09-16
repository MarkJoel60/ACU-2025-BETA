// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Common.Descriptor.Attributes.CascadeDeleteAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.CN.Common.Descriptor.Attributes;

/// <summary>
/// Attribute used for cascade deleting child entities.
/// Unlike <see cref="T:PX.Data.PXParentAttribute" /> not require DataView of the child entity in related Graph.
/// Should be set on key property as use FieldName parameter.
/// </summary>
[AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
public class CascadeDeleteAttribute : PXEventSubscriberAttribute, IPXRowPersistingSubscriber
{
  private readonly Type childReferenceType;
  private readonly Type childReferenceKeyType;

  public CascadeDeleteAttribute(Type childReferenceType, Type childReferenceKeyType)
  {
    this.childReferenceType = childReferenceType;
    this.childReferenceKeyType = childReferenceKeyType;
  }

  public void RowPersisting(PXCache cache, PXRowPersistingEventArgs args)
  {
    if (args.Operation != 3)
      return;
    PXView view = this.GetChildView(cache);
    view.SelectMulti(new object[1]
    {
      cache.GetValue(args.Row, this.FieldName)
    }).ForEach((Action<object>) (e => view.Cache.Delete(e)));
    view.Cache.Persist((PXDBOperation) 3);
    view.Cache.Clear();
  }

  private PXView GetChildView(PXCache cache)
  {
    BqlCommand instance = BqlCommand.CreateInstance(new Type[7]
    {
      typeof (Select<,>),
      this.childReferenceType,
      typeof (Where<,>),
      this.childReferenceKeyType,
      typeof (Equal<>),
      typeof (Required<>),
      this.childReferenceKeyType
    });
    return new PXView(cache.Graph, false, instance);
  }
}
