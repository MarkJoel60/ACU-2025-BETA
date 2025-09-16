// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.PXRounder
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Objects.CM;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.Common;

/// <exclude />
public static class PXRounder
{
  private const int TolerancePrecision = 12;
  public const int DefaultPrecision = 2;

  private static IEnumerable<int> GetAdjustedIndexes(
    PXRounder.SpreadType spreadType,
    int length,
    int countOfAdjusted,
    bool invertSpread)
  {
    if (countOfAdjusted > length / 2)
      throw new ArgumentOutOfRangeException(nameof (countOfAdjusted));
    if (invertSpread)
    {
      switch (spreadType)
      {
        case PXRounder.SpreadType.Top:
          spreadType = PXRounder.SpreadType.Bottom;
          break;
        case PXRounder.SpreadType.Bottom:
          spreadType = PXRounder.SpreadType.Top;
          break;
        case PXRounder.SpreadType.First:
          spreadType = PXRounder.SpreadType.Last;
          break;
        case PXRounder.SpreadType.Last:
          spreadType = PXRounder.SpreadType.First;
          break;
      }
    }
    Random randomizer = new Random();
    HashSet<int> usedRandomIndexes = new HashSet<int>();
    for (int i = 0; i < countOfAdjusted; ++i)
    {
      switch (spreadType)
      {
        case PXRounder.SpreadType.Top:
          yield return i;
          break;
        case PXRounder.SpreadType.Bottom:
          yield return length - i - 1;
          break;
        case PXRounder.SpreadType.Evenly:
          yield return (int) PXRounder.Round((Decimal) length / (Decimal) countOfAdjusted * (Decimal) i, 0);
          break;
        case PXRounder.SpreadType.Random:
          int adjustedIndex;
          do
            ;
          while (usedRandomIndexes.Contains(adjustedIndex = randomizer.Next(length)));
          usedRandomIndexes.Add(adjustedIndex);
          yield return adjustedIndex;
          break;
        case PXRounder.SpreadType.First:
          yield return 0;
          break;
        case PXRounder.SpreadType.Last:
          yield return length - 1;
          break;
        default:
          throw new NotImplementedException();
      }
    }
  }

  [Obsolete("This item has been deprecated and will be removed in Acumatica ERP 2025 R2.")]
  public static int GetBaseCuryPrecision()
  {
    return (int) ((short?) CurrencyCollection.GetBaseCurrency()?.DecimalPlaces ?? (short) 2);
  }

  public static int GetCuryPrecision(string curyID)
  {
    return (int) ((short?) CurrencyCollection.GetCurrency(curyID)?.DecimalPlaces ?? (short) 2);
  }

  public static Decimal Round(Decimal sourceValue, int precision)
  {
    return Math.Round(sourceValue, precision, MidpointRounding.AwayFromZero);
  }

  [Obsolete("This item has been deprecated and will be removed in Acumatica ERP 2025 R2.")]
  public static Decimal RoundBaseCury(Decimal sourceValue)
  {
    return PXRounder.Round(sourceValue, PXRounder.GetBaseCuryPrecision());
  }

  public static Decimal RoundCury(Decimal sourceValue, string curyID)
  {
    return PXRounder.Round(sourceValue, PXRounder.GetCuryPrecision(curyID));
  }

  [Obsolete("This item has been deprecated and will be removed in Acumatica ERP 2025 R2.")]
  public static IEnumerable<Decimal> RoundBaseCury(
    this IEnumerable<Decimal> sourceSequence,
    PXRounder.SpreadType spreadType)
  {
    return sourceSequence.Round(PXRounder.GetBaseCuryPrecision(), spreadType);
  }

  public static IEnumerable<Decimal> RoundCury(
    this IEnumerable<Decimal> sourceSequence,
    string curyID,
    PXRounder.SpreadType spreadType)
  {
    return sourceSequence.Round(PXRounder.GetCuryPrecision(curyID), spreadType);
  }

  public static IEnumerable<Decimal> Round(
    this IEnumerable<Decimal> sourceSequence,
    int precision,
    PXRounder.SpreadType spreadType)
  {
    List<Decimal> numList = new List<Decimal>();
    Decimal sourceValue1 = 0M;
    Decimal num1 = 0M;
    Decimal sourceValue2 = 0M;
    Decimal num2 = (Decimal) Math.Pow(10.0, (double) -precision);
    foreach (Decimal sourceValue3 in sourceSequence)
    {
      Decimal num3 = PXRounder.Round(sourceValue3, precision);
      sourceValue1 += sourceValue3;
      num1 += num3;
      if (spreadType == PXRounder.SpreadType.Flow && Math.Abs(PXRounder.Round(sourceValue2, 12)) >= num2)
      {
        Decimal num4 = (Decimal) Math.Sign(sourceValue2) * num2;
        num3 += num4;
        sourceValue2 -= num4;
      }
      numList.Add(num3);
    }
    if (spreadType == PXRounder.SpreadType.Flow)
      return (IEnumerable<Decimal>) numList;
    Decimal sourceValue4 = PXRounder.Round(sourceValue1, precision) - num1;
    if (Math.Abs(PXRounder.Round(sourceValue4, 12)) >= num2)
    {
      Decimal num5 = (Decimal) Math.Sign(sourceValue4) * num2;
      int countOfAdjusted = (int) PXRounder.Round(sourceValue4 / num5, 0);
      foreach (int adjustedIndex in PXRounder.GetAdjustedIndexes(spreadType, numList.Count, countOfAdjusted, sourceValue4 < 0M))
        numList[adjustedIndex] += num5;
    }
    return (IEnumerable<Decimal>) numList;
  }

  public static ICollection<TItem> Round<TItem>(
    this ICollection<TItem> items,
    Func<TItem, Decimal> getValue,
    Action<TItem, Decimal> setValue,
    int precision,
    PXRounder.SpreadType spreadType)
    where TItem : class
  {
    Decimal sourceValue1 = 0M;
    Decimal num1 = 0M;
    Decimal sourceValue2 = 0M;
    Decimal num2 = (Decimal) Math.Pow(10.0, (double) -precision);
    foreach (TItem obj in (IEnumerable<TItem>) items)
    {
      Decimal sourceValue3 = getValue(obj);
      Decimal num3 = PXRounder.Round(sourceValue3, precision);
      sourceValue2 += sourceValue3 - num3;
      sourceValue1 += sourceValue3;
      num1 += num3;
      if (spreadType == PXRounder.SpreadType.Flow && Math.Abs(PXRounder.Round(sourceValue2, 12)) >= num2)
      {
        Decimal num4 = (Decimal) Math.Sign(sourceValue2) * num2;
        num3 += num4;
        sourceValue2 -= num4;
      }
      setValue(obj, num3);
    }
    if (spreadType == PXRounder.SpreadType.Flow)
      return items;
    Decimal sourceValue4 = PXRounder.Round(sourceValue1, precision) - num1;
    if (Math.Abs(PXRounder.Round(sourceValue4, 12)) >= num2)
    {
      Decimal num5 = (Decimal) Math.Sign(sourceValue4) * num2;
      int countOfAdjusted = (int) PXRounder.Round(sourceValue4 / num5, 0);
      foreach (int adjustedIndex in PXRounder.GetAdjustedIndexes(spreadType, items.Count, countOfAdjusted, sourceValue4 < 0M))
      {
        TItem obj = items.ElementAt<TItem>(adjustedIndex);
        setValue(obj, getValue(obj) + num5);
      }
    }
    return items;
  }

  /// <exclude />
  public enum SpreadType
  {
    Top,
    Bottom,
    Evenly,
    Random,
    First,
    Last,
    Flow,
  }
}
