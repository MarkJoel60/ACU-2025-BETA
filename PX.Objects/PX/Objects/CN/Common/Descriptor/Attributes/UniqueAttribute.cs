// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Common.Descriptor.Attributes.UniqueAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.CN.Common.Extensions;
using PX.Objects.Common.Extensions;
using System;

#nullable disable
namespace PX.Objects.CN.Common.Descriptor.Attributes;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property)]
public class UniqueAttribute : PXEventSubscriberAttribute, IPXRowPersistingSubscriber
{
  public string ErrorMessage { get; set; } = "The value of this field must be unique among all records.";

  public Type WhereCondition { get; set; }

  public void RowPersisting(PXCache cache, PXRowPersistingEventArgs args)
  {
    if (!EnumerableExtensions.IsIn<PXDBOperation>(args.Operation, (PXDBOperation) 2, (PXDBOperation) 1))
      return;
    this.ValidateEntity(cache, args.Row);
  }

  private void ValidateEntity(PXCache cache, object entity)
  {
    object newValue = cache.GetValue(entity, this._FieldName);
    if (newValue == null || !this.GetView(cache).SelectMulti(newValue.SingleToArray<object>()).HasAtLeastTwoItems<object>())
      return;
    cache.RaiseException(this._FieldName, entity, this.ErrorMessage, newValue);
  }

  private PXView GetView(PXCache cache)
  {
    BqlCommand bqlCommand = this.GetBqlCommand(cache);
    if (this.WhereCondition != (Type) null)
      bqlCommand = bqlCommand.WhereAnd(this.WhereCondition);
    return new PXView(cache.Graph, false, bqlCommand);
  }

  private BqlCommand GetBqlCommand(PXCache cache)
  {
    Type bqlField = cache.GetBqlField(this._FieldName);
    return BqlCommand.CreateInstance(new Type[7]
    {
      typeof (Select<,>),
      this.BqlTable,
      typeof (Where<,>),
      bqlField,
      typeof (Equal<>),
      typeof (Required<>),
      bqlField
    });
  }
}
