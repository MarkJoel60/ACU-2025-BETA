// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.NonStockNonKitCrossItemAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using System;
using System.Linq.Expressions;
using System.Reflection;

#nullable disable
namespace PX.Objects.IN;

public class NonStockNonKitCrossItemAttribute : CrossItemAttribute
{
  private NonStockNonKitCrossItemAttribute(Type search, INPrimaryAlternateType primaryAltType)
    : base(search, typeof (InventoryItem.inventoryCD), typeof (InventoryItem.descr), primaryAltType)
  {
  }

  public NonStockNonKitCrossItemAttribute(INPrimaryAlternateType primaryAltType)
    : this(NonStockItemAttribute.Search, primaryAltType)
  {
    ((PXAggregateAttribute) this)._Attributes.Add((PXEventSubscriberAttribute) NonStockItemAttribute.CreateRestrictor());
    ((PXAggregateAttribute) this)._Attributes.Add((PXEventSubscriberAttribute) NonStockNonKitItemAttribute.CreateStandardRestrictor());
  }

  public NonStockNonKitCrossItemAttribute(
    INPrimaryAlternateType primaryAltType,
    string aErrorMsg,
    Type origNbrField)
    : this(NonStockItemAttribute.Search, primaryAltType)
  {
    ((PXAggregateAttribute) this)._Attributes.Add((PXEventSubscriberAttribute) NonStockItemAttribute.CreateRestrictor());
    ((PXAggregateAttribute) this)._Attributes.Add((PXEventSubscriberAttribute) GenericCall.Of<PXRestrictorAttribute>((Expression<Func<PXRestrictorAttribute>>) (() => NonStockNonKitItemAttribute.CreateCustomRestrictor<IBqlField>(aErrorMsg))).ButWith(origNbrField, Array.Empty<Type>()));
  }

  public NonStockNonKitCrossItemAttribute(
    INPrimaryAlternateType primaryAltType,
    string aErrorMsg,
    Type origNbrField,
    Type allowStkFeature)
    : this(NonStockItemAttribute.Search, primaryAltType)
  {
    // ISSUE: method reference
    ((PXAggregateAttribute) this)._Attributes.Add((PXEventSubscriberAttribute) GenericCall.Of<PXRestrictorAttribute>(Expression.Lambda<Func<PXRestrictorAttribute>>((Expression) Expression.Call((Expression) null, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (NonStockItemAttribute.CreateRestrictorDependingOnFeature)), Array.Empty<Expression>()))).ButWith(allowStkFeature, Array.Empty<Type>()));
    ((PXAggregateAttribute) this)._Attributes.Add((PXEventSubscriberAttribute) GenericCall.Of<PXRestrictorAttribute>((Expression<Func<PXRestrictorAttribute>>) (() => NonStockNonKitItemAttribute.CreateCustomRestrictor<IBqlField>(aErrorMsg))).ButWith(origNbrField, Array.Empty<Type>()));
  }
}
