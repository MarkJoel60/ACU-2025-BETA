// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.Services.AmountDistribution.DistributionParameter`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.PO.Services.AmountDistribution;

public class DistributionParameter<ItemType> where ItemType : class, IAmountItem
{
  public IEnumerable<ItemType> Items;
  public Decimal? Amount;
  public Decimal? CuryAmount;
  public object CuryRow;
  public PXCache CacheOfCuryRow;
  public Func<ItemType, Decimal?, Decimal?, ItemType> OnValueCalculated;
  public Action<ItemType, Decimal?, Decimal?, Decimal?, Decimal?> OnRoundingDifferenceApplied;
  public Func<ItemType, Decimal?, Decimal?, Tuple<Decimal?, Decimal?>> ReplaceAmount;
}
