// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.Abstraction.AvailabilityFlagAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using System;

#nullable disable
namespace PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.Abstraction;

[PXBool]
public class AvailabilityFlagAttribute : PXAggregateAttribute
{
  public AvailabilityFlagAttribute(Type referenceField, Type flagField)
  {
    this._Attributes.Add((PXEventSubscriberAttribute) new PXDefaultAttribute(((IBqlTemplate) BqlTemplate.OfCommand<SelectFromBase<INAvailabilityScheme, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<INAvailabilityScheme.availabilitySchemeID, IBqlString>.IsEqual<BqlField<BqlPlaceholder.A, BqlPlaceholder.IBqlAny>.FromCurrent>>>.Replace<BqlPlaceholder.A>(referenceField)).ToType())
    {
      SourceField = flagField,
      CacheGlobal = true,
      PersistingCheck = (PXPersistingCheck) 2
    });
  }

  public AvailabilityFlagAttribute(bool @default)
  {
    this._Attributes.Add((PXEventSubscriberAttribute) new PXUnboundDefaultAttribute((object) @default));
  }
}
