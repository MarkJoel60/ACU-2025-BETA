// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.Discount.DiscountEngineProvider
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;

#nullable disable
namespace PX.Objects.Common.Discount;

/// <summary>
/// Provides instances of discount engines for a specific discounted line type
/// </summary>
public class DiscountEngineProvider
{
  /// <summary>
  /// Get instance of discount engines for a specific discounted line type
  /// </summary>
  /// <typeparam name="TLine">The type of discounted line</typeparam>
  /// <returns></returns>
  public static DiscountEngine<TLine, TDiscountDetail> GetEngineFor<TLine, TDiscountDetail>()
    where TLine : class, IBqlTable, new()
    where TDiscountDetail : class, IBqlTable, IDiscountDetail, new()
  {
    return DiscountEngineProvider.EnginesCache<TLine, TDiscountDetail>.Engine;
  }

  /// <summary>
  /// Caches discount engines for a specific discounted line type
  /// </summary>
  /// <typeparam name="TLine">The type of discounted line</typeparam>
  private static class EnginesCache<TLine, TDiscountDetail>
    where TLine : class, IBqlTable, new()
    where TDiscountDetail : class, IBqlTable, IDiscountDetail, new()
  {
    public static DiscountEngine<TLine, TDiscountDetail> Engine
    {
      get
      {
        return PXContext.GetSlot<DiscountEngine<TLine, TDiscountDetail>>() ?? PXContext.SetSlot<DiscountEngine<TLine, TDiscountDetail>>(PXGraph.CreateInstance<DiscountEngine<TLine, TDiscountDetail>>());
      }
    }
  }
}
