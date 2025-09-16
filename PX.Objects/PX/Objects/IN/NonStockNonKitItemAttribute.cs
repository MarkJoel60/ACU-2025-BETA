// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.NonStockNonKitItemAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using System;
using System.Linq.Expressions;

#nullable disable
namespace PX.Objects.IN;

public class NonStockNonKitItemAttribute : NonStockItemAttribute
{
  public static PXRestrictorAttribute CreateStandardRestrictor()
  {
    return new PXRestrictorAttribute(typeof (Where<InventoryItem.stkItem, NotEqual<False>, Or<InventoryItem.kitItem, NotEqual<True>>>), "A non-stock kit cannot be added to a cash transaction.", Array.Empty<Type>());
  }

  public static PXRestrictorAttribute CreateCustomRestrictor<TOrigNbrField>(string aErrorMsg) where TOrigNbrField : IBqlField
  {
    return new PXRestrictorAttribute(typeof (Where<Current<TOrigNbrField>, IsNotNull, Or<InventoryItem.stkItem, NotEqual<False>, Or<InventoryItem.kitItem, NotEqual<True>>>>), aErrorMsg, Array.Empty<Type>());
  }

  public NonStockNonKitItemAttribute()
  {
    ((PXAggregateAttribute) this)._Attributes.Add((PXEventSubscriberAttribute) NonStockNonKitItemAttribute.CreateStandardRestrictor());
  }

  public NonStockNonKitItemAttribute(string aErrorMsg, Type origNbrField)
  {
    ((PXAggregateAttribute) this)._Attributes.Add((PXEventSubscriberAttribute) GenericCall.Of<PXRestrictorAttribute>((Expression<Func<PXRestrictorAttribute>>) (() => NonStockNonKitItemAttribute.CreateCustomRestrictor<IBqlField>(aErrorMsg))).ButWith(origNbrField, Array.Empty<Type>()));
  }
}
