// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.Discount.DiscountEngine`2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Common.Collection;
using PX.Data;
using PX.Objects.AR;
using PX.Objects.CM.TemporaryHelpers;
using PX.Objects.Common.Discount.Mappers;
using PX.Objects.Common.Extensions;
using PX.Objects.CS;
using PX.Objects.IN;
using PX.Objects.TX;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

#nullable disable
namespace PX.Objects.Common.Discount;

public class DiscountEngine<TLine, TDiscountDetail> : DiscountEngine
  where TLine : class, IBqlTable, new()
  where TDiscountDetail : class, IBqlTable, IDiscountDetail, new()
{
  private const string DiscountID = "DiscountID";
  private const string DiscountSequenceID = "DiscountSequenceID";
  private const string TypeFieldName = "Type";
  private const string LineNbrFieldName = "LineNbr";

  protected virtual bool IsDiscountFeatureEnabled(
    PXGraph graph,
    DiscountEngine.DiscountCalculationOptions calculationOptions = DiscountEngine.DiscountCalculationOptions.CalculateAll)
  {
    if (calculationOptions.HasFlag((Enum) DiscountEngine.DiscountCalculationOptions.DisableAPDiscountsCalculation) && calculationOptions.HasFlag((Enum) DiscountEngine.DiscountCalculationOptions.DisableARDiscountsCalculation))
      return false;
    if (calculationOptions.HasFlag((Enum) DiscountEngine.DiscountCalculationOptions.DisableAPDiscountsCalculation))
      return DiscountEngine.FeatureInstalled<FeaturesSet.customerDiscounts>(graph);
    return !calculationOptions.HasFlag((Enum) DiscountEngine.DiscountCalculationOptions.DisableARDiscountsCalculation) || DiscountEngine.FeatureInstalled<FeaturesSet.vendorDiscounts>(graph);
  }

  protected bool IsDiscountCalculationEnabled(
    PXGraph graph,
    DiscountEngine.DiscountCalculationOptions options)
  {
    return !graph.IsImport || options.HasFlag((Enum) DiscountEngine.DiscountCalculationOptions.CalculateDiscountsFromImport);
  }

  public virtual void SetDiscounts(
    PXCache cache,
    PXSelectBase<TLine> lines,
    TLine line,
    PXSelectBase<TDiscountDetail> discountDetails,
    int? branchID,
    int? locationID,
    string curyID,
    DateTime? date,
    RecalcDiscountsParamFilter recalcFilter = null,
    DiscountEngine.DiscountCalculationOptions discountCalculationOptions = DiscountEngine.DiscountCalculationOptions.CalculateAll)
  {
    if (!branchID.HasValue || !locationID.HasValue || !date.HasValue || !this.IsDiscountFeatureEnabled(cache.Graph, discountCalculationOptions))
      return;
    DiscountEngine.UpdateEntityCache();
    DiscountLineFields mapFor = DiscountLineFields.GetMapFor<TLine>(line, cache);
    bool? nullable = mapFor.AutomaticDiscountsDisabled;
    if ((!nullable.GetValueOrDefault() || discountCalculationOptions.HasFlag((Enum) DiscountEngine.DiscountCalculationOptions.ExplicitlyAllowToCalculateAutomaticLineDiscounts)) && this.IsDiscountCalculationNeeded(cache, line, "L", discountDetails) && (this.IsDiscountCalculationEnabled(cache.Graph, discountCalculationOptions) || cache.Graph.IsMobile))
    {
      int num;
      if (!cache.Fields.Contains("SkipLineDiscounts"))
      {
        num = this.GetUnitPrice(cache, line, locationID, curyID, date.Value).skipLineDiscount ? 1 : 0;
      }
      else
      {
        nullable = mapFor.SkipLineDiscounts;
        num = nullable.GetValueOrDefault() ? 1 : 0;
      }
      if (num == 0)
        this.SetLineDiscount(cache, this.GetDiscountEntitiesDiscounts(cache, line, locationID, true), line, date.Value, discountCalculationOptions, recalcFilter);
      else if (!mapFor.ManualDisc)
        this.ClearLineDiscount(cache, line, mapFor);
    }
    nullable = mapFor.AutomaticDiscountsDisabled;
    if (nullable.GetValueOrDefault() && discountCalculationOptions.HasFlag((Enum) DiscountEngine.DiscountCalculationOptions.DisableOrigAutomaticDiscounts))
      this.UpdateOrigGroupAndDocumentDiscounts(cache, lines, discountDetails, discountCalculationOptions);
    if (discountCalculationOptions.HasFlag((Enum) DiscountEngine.DiscountCalculationOptions.DisableGroupAndDocumentDiscounts) || !this.IsDiscountCalculationEnabled(cache.Graph, discountCalculationOptions) && !cache.Graph.IsMobile || mapFor.SkipDisc)
      return;
    nullable = mapFor.AutomaticDiscountsDisabled;
    if (!nullable.GetValueOrDefault())
    {
      this.RecalculateGroupAndDocumentDiscounts(cache, lines, line, discountDetails, branchID, locationID, date, discountCalculationOptions, recalcFilter);
    }
    else
    {
      this.RemoveOrphanDiscountLines(cache, lines, discountDetails, discountCalculationOptions);
      this.UpdateExternalDiscounts(cache, lines, line, discountDetails, discountCalculationOptions);
    }
  }

  protected override void SetLineDiscountOnlyImpl(
    PXCache cache,
    object line,
    DiscountLineFields dLine,
    string discountID,
    Decimal? unitPrice,
    Decimal? extPrice,
    Decimal? qty,
    int? locationID,
    int? customerID,
    string curyID,
    DateTime? date,
    int? branchID,
    int? inventoryID,
    bool needDiscountID)
  {
    if (!locationID.HasValue || !date.HasValue || !this.IsDiscountFeatureEnabled(cache.Graph))
      return;
    DiscountEngine.UpdateEntityCache();
    TLine line1 = (TLine) line;
    if (!this.IsDiscountCalculationNeeded(cache, line1, "L"))
      return;
    if (this.GetUnitPrice(cache, line1, locationID, curyID, date.Value).skipLineDiscount)
    {
      if (dLine.ManualDisc)
        return;
      this.ClearLineDiscount(cache, line1, dLine);
    }
    else
    {
      HashSet<KeyValuePair<object, string>> entitiesDiscounts = this.GetDiscountEntitiesDiscounts(cache, line1, locationID, true, branchID, inventoryID, customerID);
      DiscountEngine.GetDiscountTypes();
      if (!extPrice.HasValue || !qty.HasValue)
        return;
      HashSet<DiscountSequenceKey> other = this.SelectDiscountSequences(discountID);
      HashSet<DiscountSequenceKey> discountSequences = this.SelectApplicableEntityDiscounts(cache.Graph, entitiesDiscounts, "L", !needDiscountID);
      if (needDiscountID)
        discountSequences.IntersectWith((IEnumerable<DiscountSequenceKey>) other);
      Decimal num = !(this.GetLineDiscountTarget(cache, line1) == "S") ? extPrice.Value : unitPrice.Value;
      DiscountDetailLine discount1 = this.SelectApplicableDiscount(cache, dLine, discountSequences, num, qty.Value, "L", date.Value);
      if (discount1.DiscountID != null)
      {
        Decimal discount2 = this.CalculateDiscount(cache, discount1, dLine, num, qty.Value, date.Value, "L");
        DiscountResult discountResult = new DiscountResult(discount1.DiscountedFor == "A" ? new Decimal?(discount2) : discount1.Discount, discount1.DiscountedFor == "A");
        this.ApplyDiscountToLine(cache, line1, new Decimal?(qty.Value), new Decimal?(unitPrice.Value), new Decimal?(extPrice.Value), dLine, discountResult, 1);
        dLine.DiscountID = discount1.DiscountID;
        dLine.DiscountSequenceID = discount1.DiscountSequenceID;
        dLine.RaiseFieldUpdated<DiscountLineFields.discountSequenceID>((object) null);
      }
      else
        this.ClearLineDiscount(cache, line1, dLine);
    }
  }

  protected override Decimal GetLineDiscountOnlyImpl(
    PXCache cache,
    object line,
    DiscountLineFields dLine,
    Decimal? unitPrice,
    Decimal? extPrice,
    Decimal? qty,
    int? locationID,
    int? customerID,
    string curyID,
    DateTime? date,
    int? branchID,
    int? inventoryID,
    bool needDiscountID)
  {
    if (!locationID.HasValue || !date.HasValue || !this.IsDiscountFeatureEnabled(cache.Graph))
      return 0M;
    DiscountEngine.UpdateEntityCache();
    TLine line1 = (TLine) line;
    HashSet<KeyValuePair<object, string>> entitiesDiscounts = this.GetDiscountEntitiesDiscounts(cache, line1, locationID, true, branchID, inventoryID, customerID);
    DiscountEngine.GetDiscountTypes();
    if (!extPrice.HasValue || !qty.HasValue)
      return 0M;
    HashSet<DiscountSequenceKey> discountSequences = this.SelectApplicableEntityDiscounts(cache.Graph, entitiesDiscounts, "L", !needDiscountID, true);
    Decimal num = !(this.GetLineDiscountTarget(cache, line1) == "S") ? extPrice.Value : unitPrice.Value;
    DiscountDetailLine discount = this.SelectApplicableDiscount(cache, dLine, discountSequences, num, qty.Value, "L", date.Value);
    return discount.DiscountID != null ? this.CalculateDiscount(cache, discount, dLine, num, qty.Value, date.Value, "L") : 0M;
  }

  public virtual void RecalculatePricesAndDiscounts(
    PXCache cache,
    PXSelectBase<TLine> lines,
    TLine currentLine,
    PXSelectBase<TDiscountDetail> discountDetails,
    int? locationID,
    DateTime? date,
    RecalcDiscountsParamFilter recalcFilter,
    DiscountEngine.DiscountCalculationOptions discountCalculationOptions)
  {
    try
    {
      DiscountEngine.UpdateEntityCache();
      recalcFilter.UseRecalcFilter = new bool?(true);
      if (recalcFilter.RecalcDiscounts.GetValueOrDefault() && (recalcFilter.OverrideManualDocGroupDiscounts.GetValueOrDefault() || recalcFilter.CalcDiscountsOnLinesWithDisabledAutomaticDiscounts.GetValueOrDefault()))
      {
        foreach (TDiscountDetail discountDetail in this.GetDiscountDetailsByType(cache, discountDetails, (string) null))
        {
          bool? nullable = recalcFilter.CalcDiscountsOnLinesWithDisabledAutomaticDiscounts;
          if (nullable.GetValueOrDefault())
          {
            nullable = discountDetail.IsOrigDocDiscount;
            if (nullable.GetValueOrDefault())
              discountDetails.Delete(discountDetail);
          }
          nullable = recalcFilter.OverrideManualDocGroupDiscounts;
          if (nullable.GetValueOrDefault() && discountDetail.Type != "B")
            discountDetail.IsManual = new bool?(false);
        }
      }
      if (recalcFilter.RecalcTarget == "ALL" && (recalcFilter.RecalcDiscounts.GetValueOrDefault() || recalcFilter.RecalcUnitPrices.GetValueOrDefault()))
      {
        List<TLine> documentDetails = this.GetDocumentDetails(cache, lines);
        for (int index = 0; index < documentDetails.Count<TLine>(); ++index)
        {
          DiscountLineFields mapFor = DiscountLineFields.GetMapFor<TLine>(documentDetails[index], cache);
          try
          {
            if (index != documentDetails.Count<TLine>() - 1)
              mapFor.SkipDisc = true;
            this.RecalculatePricesAndDiscountsOnLine(cache, documentDetails[index], recalcFilter, discountCalculationOptions);
          }
          finally
          {
            mapFor.SkipDisc = false;
          }
        }
      }
      else
      {
        if ((object) currentLine == null || !recalcFilter.RecalcDiscounts.GetValueOrDefault() && !recalcFilter.RecalcUnitPrices.GetValueOrDefault())
          return;
        this.RecalculatePricesAndDiscountsOnLine(cache, currentLine, recalcFilter, discountCalculationOptions);
      }
    }
    finally
    {
      recalcFilter.UseRecalcFilter = new bool?(false);
    }
  }

  public virtual void AutoRecalculatePricesAndDiscounts(
    PXCache cache,
    PXSelectBase<TLine> lines,
    TLine currentLine,
    PXSelectBase<TDiscountDetail> discountDetails,
    int? locationID,
    DateTime? date,
    DiscountEngine.DiscountCalculationOptions discountCalculationOptions)
  {
    if (!locationID.HasValue || !date.HasValue)
      return;
    RecalcDiscountsParamFilter recalcFilter = new RecalcDiscountsParamFilter()
    {
      RecalcTarget = "ALL",
      OverrideManualDiscounts = new bool?(false),
      OverrideManualDocGroupDiscounts = new bool?(false),
      OverrideManualPrices = new bool?(false),
      RecalcDiscounts = new bool?(this.IsDiscountFeatureEnabled(cache.Graph, discountCalculationOptions)),
      RecalcUnitPrices = new bool?(!discountCalculationOptions.HasFlag((Enum) DiscountEngine.DiscountCalculationOptions.DisablePriceCalculation))
    };
    try
    {
      this.IsInternalDiscountEngineCall = true;
      this.RecalculatePricesAndDiscounts(cache, lines, currentLine, discountDetails, locationID, date, recalcFilter, discountCalculationOptions);
    }
    finally
    {
      this.IsInternalDiscountEngineCall = false;
    }
  }

  protected virtual void RecalculatePricesAndDiscountsOnLine(
    PXCache cache,
    TLine line,
    RecalcDiscountsParamFilter recalcFilter,
    DiscountEngine.DiscountCalculationOptions discountCalculationOptions)
  {
    TLine copy = (TLine) cache.CreateCopy((object) line);
    DiscountLineFields mapFor1 = DiscountLineFields.GetMapFor<TLine>(line, cache);
    DiscountLineFields mapFor2 = DiscountLineFields.GetMapFor<TLine>(copy, cache);
    AmountLineFields mapFor3 = AmountLineFields.GetMapFor<TLine>(line, cache);
    LineEntitiesFields mapFor4 = LineEntitiesFields.GetMapFor<TLine>(line, cache);
    if (recalcFilter.OverrideManualPrices.Value)
      mapFor1.ManualPrice = false;
    bool flag1 = false;
    bool? nullable1;
    if (mapFor4.InventoryID.HasValue && !mapFor1.ManualPrice)
    {
      if (!recalcFilter.RecalcUnitPrices.GetValueOrDefault())
      {
        Decimal? nullable2 = mapFor3.CuryUnitPrice;
        Decimal num1 = 0M;
        if (nullable2.GetValueOrDefault() == num1 & nullable2.HasValue)
        {
          nullable2 = mapFor3.CuryExtPrice;
          Decimal num2 = 0M;
          if (nullable2.GetValueOrDefault() == num2 & nullable2.HasValue)
          {
            nullable1 = recalcFilter.OverrideManualPrices;
            bool flag2 = false;
            if (!(nullable1.GetValueOrDefault() == flag2 & nullable1.HasValue))
              goto label_8;
          }
          else
            goto label_8;
        }
        else
          goto label_8;
      }
      cache.RaiseFieldUpdated(mapFor3.GetField<AmountLineFields.curyUnitPrice>().Name, (object) line, (object) 0M);
      cache.SetDefaultExt((object) line, mapFor3.GetField<AmountLineFields.curyUnitPrice>().Name, (object) null);
      flag1 = true;
    }
label_8:
    nullable1 = recalcFilter.RecalcDiscounts;
    if (nullable1.Value)
    {
      nullable1 = mapFor1.AutomaticDiscountsDisabled;
      if (nullable1.GetValueOrDefault())
      {
        nullable1 = recalcFilter.CalcDiscountsOnLinesWithDisabledAutomaticDiscounts;
        if (!nullable1.GetValueOrDefault() && !discountCalculationOptions.HasFlag((Enum) DiscountEngine.DiscountCalculationOptions.ExplicitlyAllowToCalculateAutomaticLineDiscounts))
          goto label_20;
      }
      nullable1 = recalcFilter.OverrideManualDiscounts;
      if (nullable1.GetValueOrDefault())
        mapFor1.ManualDisc = false;
      if (!mapFor1.ManualDisc)
        mapFor1.DiscountID = (string) null;
      nullable1 = recalcFilter.CalcDiscountsOnLinesWithDisabledAutomaticDiscounts;
      if (nullable1.GetValueOrDefault())
        mapFor1.AutomaticDiscountsDisabled = new bool?(false);
      mapFor2.DiscountID = string.Empty;
      if (discountCalculationOptions.HasFlag((Enum) DiscountEngine.DiscountCalculationOptions.CalculateDiscountsFromImport))
        mapFor1.CalculateDiscountsOnImport = new bool?(true);
      flag1 = true;
    }
label_20:
    if (!flag1)
      return;
    cache.IsDirty = true;
    cache.RaiseRowUpdated((object) line, (object) copy);
    GraphHelper.MarkUpdated(cache, (object) line, true);
  }

  public virtual void RecalculateGroupAndDocumentDiscounts(
    PXCache cache,
    PXSelectBase<TLine> lines,
    TLine currentLine,
    PXSelectBase<TDiscountDetail> discountDetails,
    int? branchID,
    int? locationID,
    DateTime? date,
    DiscountEngine.DiscountCalculationOptions discountCalculationOptions,
    RecalcDiscountsParamFilter recalcFilter = null)
  {
    DiscountLineFields mapFor = DiscountLineFields.GetMapFor<TLine>(currentLine, cache);
    if (!branchID.HasValue || !locationID.HasValue || !date.HasValue || mapFor.AutomaticDiscountsDisabled.GetValueOrDefault() || !this.IsDiscountFeatureEnabled(cache.Graph, discountCalculationOptions))
    {
      if (this.IsDiscountFeatureEnabled(cache.Graph, discountCalculationOptions))
        return;
      this.RemoveOrphanDiscountLines(cache, lines, discountDetails, discountCalculationOptions);
      this.CalculateDocumentDiscountRate(cache, lines, currentLine, discountDetails, discountCalculationOptions);
    }
    else
    {
      DiscountEngine.UpdateEntityCache();
      this.RemoveOrphanDiscountLines(cache, lines, discountDetails, discountCalculationOptions);
      this.SetGroupDiscounts(cache, lines, currentLine, discountDetails, locationID, date.Value, discountCalculationOptions, recalcFilter);
      this.SetDocumentDiscount(cache, lines, discountDetails, currentLine, branchID, locationID, date.Value, discountCalculationOptions, recalcFilter);
      if (!discountCalculationOptions.HasFlag((Enum) DiscountEngine.DiscountCalculationOptions.DisableOrigAutomaticDiscounts))
        return;
      this.UpdateOrigGroupAndDocumentDiscounts(cache, lines, discountDetails, discountCalculationOptions);
    }
  }

  public virtual void RemoveOrphanDiscountLines(
    PXCache cache,
    PXSelectBase<TLine> lines,
    PXSelectBase<TDiscountDetail> discountDetails,
    DiscountEngine.DiscountCalculationOptions discountCalculationOptions)
  {
    TwoWayLookup<TDiscountDetail, TLine> andDocumentLines = this.GetListOfLinksBetweenDiscountsAndDocumentLines(cache, lines, discountDetails, discountCalculationOptions);
    foreach (TDiscountDetail leftValue in andDocumentLines.LeftValues)
    {
      if (andDocumentLines.RightsFor(leftValue).Count<TLine>() == 0 && (!discountCalculationOptions.HasFlag((Enum) DiscountEngine.DiscountCalculationOptions.DisableOrigAutomaticDiscounts) || !leftValue.IsOrigDocDiscount.GetValueOrDefault()))
        this.DeleteDiscountDetail(cache, discountDetails, leftValue);
    }
  }

  public virtual void UpdateManualLineDiscount(
    PXCache cache,
    PXSelectBase<TLine> lines,
    TLine line,
    PXSelectBase<TDiscountDetail> discountDetails,
    int? branchID,
    int? locationID,
    DateTime? date,
    DiscountEngine.DiscountCalculationOptions discountCalculationOptions)
  {
    if (!branchID.HasValue || !locationID.HasValue || !date.HasValue || !this.IsDiscountFeatureEnabled(cache.Graph, discountCalculationOptions))
      return;
    if (!this.IsDiscountFeatureEnabled(cache.Graph, discountCalculationOptions))
    {
      this.CalculateDocumentDiscountRate(cache, this.GetDocumentDetails(cache, lines), default (TLine), discountDetails, discountCalculationOptions);
    }
    else
    {
      DiscountEngine.UpdateEntityCache();
      this.SetManualLineDiscount(cache, this.GetDiscountEntitiesDiscounts(cache, line, locationID, true), line, date.Value);
      this.RecalculateGroupAndDocumentDiscounts(cache, lines, line, discountDetails, branchID, locationID, new DateTime?(date.Value), discountCalculationOptions);
    }
  }

  public virtual void UpdateDocumentDiscount(
    PXCache cache,
    PXSelectBase<TLine> lines,
    PXSelectBase<TDiscountDetail> discountDetails,
    int? branchID,
    int? locationID,
    DateTime? date,
    bool recalcDocDiscount,
    DiscountEngine.DiscountCalculationOptions discountCalculationOptions)
  {
    if (!branchID.HasValue || !locationID.HasValue || !date.HasValue || !this.IsDiscountFeatureEnabled(cache.Graph, discountCalculationOptions))
      return;
    DiscountEngine.UpdateEntityCache();
    if (recalcDocDiscount)
    {
      this.AdjustGroupDiscountRates(cache, lines, discountDetails, locationID, date.Value);
      this.SetDocumentDiscount(cache, lines, discountDetails, default (TLine), branchID, locationID, date.Value, discountCalculationOptions);
      ((PXSelectBase) discountDetails).View.RequestRefresh();
    }
    else
      this.CalculateDocumentDiscountRate(cache, this.GetDocumentDetails(cache, lines), default (TLine), discountDetails, discountCalculationOptions);
  }

  public virtual void InsertManualDocGroupDiscount(
    PXCache cache,
    PXSelectBase<TLine> lines,
    PXSelectBase<TDiscountDetail> discountDetails,
    TDiscountDetail currentDiscountDetailLine,
    string discountID,
    string discountSequenceID,
    int? branchID,
    int? locationID,
    DateTime? date,
    DiscountEngine.DiscountCalculationOptions discountCalculationOptions)
  {
    if (!branchID.HasValue || !locationID.HasValue || !date.HasValue || !this.IsDiscountFeatureEnabled(cache.Graph, discountCalculationOptions))
      return;
    DiscountEngine.UpdateEntityCache();
    currentDiscountDetailLine.IsManual = new bool?(true);
    this.SetManualGroupDocDiscount(cache, lines, default (TLine), discountDetails, currentDiscountDetailLine, discountID, discountSequenceID, branchID, locationID, date.Value, discountCalculationOptions);
    if (!(currentDiscountDetailLine.Type != "D"))
      return;
    this.UpdateDocumentDiscount(cache, lines, discountDetails, branchID, locationID, new DateTime?(date.Value), currentDiscountDetailLine.Type != null && currentDiscountDetailLine.Type != "D", discountCalculationOptions);
  }

  public virtual void UpdateManualDocGroupDiscount(
    PXCache cache,
    PXSelectBase<TLine> lines,
    PXSelectBase<TDiscountDetail> discountDetails,
    TDiscountDetail currentDiscountDetailLine,
    string discountID,
    string discountSequenceID,
    int? branchID,
    int? locationID,
    DateTime? date,
    DiscountEngine.DiscountCalculationOptions discountCalculationOptions)
  {
    if (!branchID.HasValue || !locationID.HasValue || !date.HasValue || !this.IsDiscountFeatureEnabled(cache.Graph, discountCalculationOptions))
      return;
    DiscountEngine.UpdateEntityCache();
    currentDiscountDetailLine.IsManual = new bool?(true);
    currentDiscountDetailLine.CuryDiscountAmt = new Decimal?(0M);
    currentDiscountDetailLine.DiscountPct = new Decimal?(0M);
    currentDiscountDetailLine.CuryDiscountableAmt = new Decimal?(0M);
    currentDiscountDetailLine.DiscountableQty = new Decimal?(0M);
    bool recalcDocDiscount = currentDiscountDetailLine.Type != null && currentDiscountDetailLine.Type != "D";
    this.UpdateDocumentDiscount(cache, lines, discountDetails, branchID, locationID, new DateTime?(date.Value), recalcDocDiscount, discountCalculationOptions);
    this.SetManualGroupDocDiscount(cache, lines, default (TLine), discountDetails, currentDiscountDetailLine, discountID, discountSequenceID, branchID, locationID, date.Value, discountCalculationOptions);
    this.UpdateDocumentDiscount(cache, lines, discountDetails, branchID, locationID, new DateTime?(date.Value), recalcDocDiscount, discountCalculationOptions);
  }

  protected virtual void SetLineDiscount(
    PXCache cache,
    HashSet<KeyValuePair<object, string>> entities,
    TLine line,
    DateTime date,
    DiscountEngine.DiscountCalculationOptions discountCalculationOptions,
    RecalcDiscountsParamFilter recalcFilter = null)
  {
    AmountLineFields mapFor1 = AmountLineFields.GetMapFor<TLine>(line, cache);
    DiscountLineFields mapFor2 = DiscountLineFields.GetMapFor<TLine>(line, cache);
    int num;
    if (recalcFilter != null)
    {
      bool? nullable = recalcFilter.UseRecalcFilter;
      if (nullable.GetValueOrDefault())
      {
        nullable = recalcFilter.OverrideManualDiscounts;
        num = nullable.GetValueOrDefault() ? 1 : 0;
        goto label_4;
      }
    }
    num = 0;
label_4:
    bool flag = num != 0;
    if (discountCalculationOptions.HasFlag((Enum) DiscountEngine.DiscountCalculationOptions.DisableAllAutomaticDiscounts) && !flag)
    {
      mapFor2.ManualDisc = true;
    }
    else
    {
      object curyUnitPrice = (object) mapFor1.CuryUnitPrice;
      object curyExtPrice = (object) mapFor1.CuryExtPrice;
      object quantity = (object) mapFor1.Quantity;
      if (!(!mapFor2.ManualDisc | flag))
        return;
      DiscountEngine.GetDiscountTypes();
      if (curyExtPrice == null || quantity == null)
        return;
      Decimal curyAmount = !(this.GetLineDiscountTarget(cache, line) == "S") ? (Decimal) curyExtPrice : (Decimal) curyUnitPrice;
      if (flag)
      {
        mapFor2.ManualDisc = false;
        mapFor2.RaiseFieldUpdated<DiscountLineFields.manualDisc>((object) null);
      }
      DiscountDetailLine discount1 = this.SelectBestDiscount(cache, mapFor2, entities, "L", curyAmount, (Decimal) quantity, date);
      if (discount1.DiscountID != null)
      {
        Decimal discount2 = this.CalculateDiscount(cache, discount1, mapFor2, curyAmount, (Decimal) quantity, date, "L");
        DiscountResult discountResult = new DiscountResult(discount1.DiscountedFor == "A" ? new Decimal?(discount2) : discount1.Discount, discount1.DiscountedFor == "A");
        this.ApplyDiscountToLine(cache, line, new Decimal?((Decimal) quantity), new Decimal?((Decimal) curyUnitPrice), new Decimal?((Decimal) curyExtPrice), mapFor2, discountResult, 1);
        mapFor2.DiscountID = discount1.DiscountID;
        mapFor2.DiscountSequenceID = discount1.DiscountSequenceID;
        if (discountCalculationOptions.HasFlag((Enum) DiscountEngine.DiscountCalculationOptions.DisableAllAutomaticDiscounts))
          mapFor2.ManualDisc = true;
        mapFor2.RaiseFieldUpdated<DiscountLineFields.discountSequenceID>((object) null);
      }
      else
      {
        DiscountResult discountResult = new DiscountResult(new Decimal?(0M), true);
        this.ApplyDiscountToLine(cache, line, new Decimal?((Decimal) quantity), new Decimal?((Decimal) curyUnitPrice), new Decimal?((Decimal) curyExtPrice), mapFor2, discountResult, 1);
      }
    }
  }

  /// <summary>Sets manual line discount</summary>
  protected virtual void SetManualLineDiscount(
    PXCache cache,
    HashSet<KeyValuePair<object, string>> entities,
    TLine line,
    DateTime date)
  {
    if (!this.IsDiscountCalculationNeeded(cache, line, "L"))
      return;
    AmountLineFields mapFor1 = AmountLineFields.GetMapFor<TLine>(line, cache);
    DiscountLineFields mapFor2 = DiscountLineFields.GetMapFor<TLine>(line, cache);
    object curyUnitPrice = (object) mapFor1.CuryUnitPrice;
    object curyExtPrice = (object) mapFor1.CuryExtPrice;
    object quantity = (object) mapFor1.Quantity;
    string discountId = mapFor2.DiscountID;
    if (curyExtPrice == null || quantity == null)
      return;
    DiscountEngine.GetDiscountTypes();
    HashSet<DiscountSequenceKey> other = this.SelectDiscountSequences(discountId);
    HashSet<DiscountSequenceKey> discountSequences = this.SelectApplicableEntityDiscounts(cache.Graph, entities, "L", false);
    discountSequences.IntersectWith((IEnumerable<DiscountSequenceKey>) other);
    Decimal num = !(this.GetLineDiscountTarget(cache, line) == "S") ? (Decimal) curyExtPrice : (Decimal) curyUnitPrice;
    DiscountDetailLine discount1 = this.SelectApplicableDiscount(cache, mapFor2, discountSequences, num, (Decimal) quantity, "L", date);
    mapFor2.ManualDisc = true;
    mapFor2.RaiseFieldUpdated<DiscountLineFields.manualDisc>((object) null);
    if (discount1.DiscountID != null)
    {
      Decimal discount2 = this.CalculateDiscount(cache, discount1, mapFor2, num, (Decimal) quantity, date, "L");
      DiscountResult discountResult = new DiscountResult(discount1.DiscountedFor == "A" ? new Decimal?(discount2) : discount1.Discount, discount1.DiscountedFor == "A");
      this.ApplyDiscountToLine(cache, line, new Decimal?((Decimal) quantity), new Decimal?((Decimal) curyUnitPrice), new Decimal?((Decimal) curyExtPrice), mapFor2, discountResult, 1);
      mapFor2.DiscountSequenceID = discount1.DiscountSequenceID;
      mapFor2.RaiseFieldUpdated<DiscountLineFields.discountSequenceID>((object) null);
    }
    else
    {
      DiscountResult discountResult = new DiscountResult(new Decimal?(0M), true);
      this.ApplyDiscountToLine(cache, line, new Decimal?((Decimal) quantity), new Decimal?((Decimal) curyUnitPrice), new Decimal?((Decimal) curyExtPrice), mapFor2, discountResult, 1);
      if (discountId == null)
        return;
      PXUIFieldAttribute.SetWarning<DiscountLineFields.discountID>(cache, (object) line, PXMessages.LocalizeFormatNoPrefixNLA("The Discount Code {0} has no matching Discount Sequence to apply.", new object[1]
      {
        (object) discountId
      }));
      if (!cache.Graph.IsImport)
        return;
      mapFor2.DiscountID = (string) null;
    }
  }

  /// <summary>Applies line-level discount to a line</summary>
  /// <param name="qty">Quantity</param>
  /// <param name="curyUnitPrice">Cury Unit Price</param>
  /// <param name="curyExtPrice">Cury Ext. Price</param>
  /// <param name="dline">Discount will be applied to this line</param>
  /// <param name="discountResult">DiscountResult (discount percent/amount and isAmount flag)</param>
  /// <param name="multInv"></param>
  protected virtual void ApplyDiscountToLine(
    PXCache sender,
    TLine line,
    Decimal? qty,
    Decimal? curyUnitPrice,
    Decimal? curyExtPrice,
    DiscountLineFields dLine,
    DiscountResult discountResult,
    int multInv)
  {
    PXCache cache = dLine.Cache;
    object mappedLine = dLine.MappedLine;
    Decimal num1 = qty.HasValue ? Math.Abs(qty.Value) : 0M;
    if (!discountResult.IsEmpty)
    {
      string lineDiscountTarget = this.GetLineDiscountTarget(sender, line);
      if (discountResult.IsAmount)
      {
        Decimal valueOrDefault = discountResult.Discount.GetValueOrDefault();
        if (lineDiscountTarget == "S")
          valueOrDefault *= num1;
        Decimal curyval;
        MultiCurrencyCalculator.CuryConvCury(cache, mappedLine, valueOrDefault, out curyval, CommonSetupDecPl.PrcCst);
        Decimal? curyDiscAmt1 = dLine.CuryDiscAmt;
        dLine.CuryDiscAmt = new Decimal?((Decimal) multInv * MultiCurrencyCalculator.RoundCury(cache, mappedLine, curyval));
        Decimal? curyDiscAmt2 = dLine.CuryDiscAmt;
        Decimal num2 = Math.Abs(curyExtPrice.GetValueOrDefault());
        if (curyDiscAmt2.GetValueOrDefault() > num2 & curyDiscAmt2.HasValue)
        {
          dLine.CuryDiscAmt = curyExtPrice;
          PXUIFieldAttribute.SetWarning<DiscountLineFields.curyDiscAmt>(sender, (object) line, PXMessages.LocalizeFormatNoPrefix("The discount amount cannot be greater than the amount in the {0} column.", new object[1]
          {
            (object) AmountLineFields.GetMapFor<TLine>(line, cache).ExtPriceDisplayName
          }));
        }
        Decimal? nullable1 = dLine.CuryDiscAmt;
        Decimal? nullable2 = curyDiscAmt1;
        if (!(nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue))
          this.UpdateDiscAmt<DiscountLineFields.curyDiscAmt>(dLine, curyDiscAmt1, dLine.CuryDiscAmt);
        Decimal? nullable3 = dLine.CuryDiscAmt;
        Decimal num3 = 0M;
        if (nullable3.GetValueOrDefault() == num3 & nullable3.HasValue || !(curyExtPrice.Value != 0M))
          return;
        Decimal? discPct = dLine.DiscPct;
        nullable3 = dLine.CuryDiscAmt;
        Decimal d = nullable3.Value * 100M / curyExtPrice.Value;
        dLine.DiscPct = new Decimal?(Math.Round(d, 6, MidpointRounding.AwayFromZero));
        nullable3 = dLine.DiscPct;
        nullable1 = discPct;
        if (nullable3.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable3.HasValue == nullable1.HasValue)
          return;
        this.UpdateDiscPct<DiscountLineFields.discPct>(dLine, discPct, dLine.DiscPct);
      }
      else
      {
        Decimal? discPct1 = dLine.DiscPct;
        dLine.DiscPct = new Decimal?(Math.Round(discountResult.Discount.GetValueOrDefault(), 6, MidpointRounding.AwayFromZero));
        Decimal? discPct2 = dLine.DiscPct;
        Decimal? nullable4 = discPct1;
        if (!(discPct2.GetValueOrDefault() == nullable4.GetValueOrDefault() & discPct2.HasValue == nullable4.HasValue))
          this.UpdateDiscPct<DiscountLineFields.discPct>(dLine, discPct1, dLine.DiscPct);
        Decimal? curyDiscAmt = dLine.CuryDiscAmt;
        Decimal? nullable5 = curyExtPrice;
        Decimal num4 = 0M;
        Decimal? nullable6;
        int num5;
        if (nullable5.GetValueOrDefault() < num4 & nullable5.HasValue)
        {
          nullable6 = curyUnitPrice;
          Decimal num6 = 0M;
          if (nullable6.GetValueOrDefault() > num6 & nullable6.HasValue)
          {
            num5 = -1;
            goto label_17;
          }
        }
        num5 = 1;
label_17:
        Decimal num7 = (Decimal) num5;
        Decimal val1;
        if (lineDiscountTarget == "S")
        {
          Decimal num8 = num7;
          Decimal valueOrDefault1 = curyUnitPrice.GetValueOrDefault();
          Decimal num9 = curyUnitPrice.GetValueOrDefault() * 0.01M;
          nullable6 = dLine.DiscPct;
          Decimal valueOrDefault2 = nullable6.GetValueOrDefault();
          Decimal num10 = PXDBPriceCostAttribute.Round(num9 * valueOrDefault2);
          Decimal num11 = valueOrDefault1 - num10;
          Decimal num12 = num8 * num11;
          LineEntitiesFields mapFor = LineEntitiesFields.GetMapFor<TLine>(line, cache);
          bool flag = mapFor != null && mapFor.VendorID.HasValue && !mapFor.CustomerID.HasValue;
          if (flag && DiscountEngine.ApplyQuantityDiscountByBaseUOM(sender.Graph).ForAP || !flag && DiscountEngine.ApplyQuantityDiscountByBaseUOM(sender.Graph).ForAR)
            num1 = INUnitAttribute.ConvertFromBase<LineEntitiesFields.inventoryID, AmountLineFields.uOM>(cache, (object) line, num1, INPrecision.QUANTITY);
          Decimal val2 = num1 * PXDBPriceCostAttribute.Round(num12);
          val1 = num7 * num1 * curyUnitPrice.GetValueOrDefault() - MultiCurrencyCalculator.RoundCury(cache, mappedLine, val2);
          if (num7 * val1 < 0M && Math.Abs(val1) < 0.01M)
            val1 = 0M;
        }
        else
        {
          Decimal num13 = curyExtPrice.GetValueOrDefault() * 0.01M;
          nullable6 = dLine.DiscPct;
          Decimal valueOrDefault = nullable6.GetValueOrDefault();
          val1 = num13 * valueOrDefault;
        }
        dLine.CuryDiscAmt = new Decimal?((Decimal) multInv * MultiCurrencyCalculator.RoundCury(cache, mappedLine, val1));
        nullable6 = dLine.CuryDiscAmt;
        if (Math.Abs(nullable6.GetValueOrDefault()) > Math.Abs(curyExtPrice.GetValueOrDefault()))
        {
          dLine.CuryDiscAmt = curyExtPrice;
          PXUIFieldAttribute.SetWarning<DiscountLineFields.curyDiscAmt>(sender, (object) line, PXMessages.LocalizeFormatNoPrefix("The discount amount cannot be greater than the amount in the {0} column.", new object[1]
          {
            (object) AmountLineFields.GetMapFor<TLine>(line, cache).ExtPriceDisplayName
          }));
        }
        nullable6 = dLine.CuryDiscAmt;
        Decimal? nullable7 = curyDiscAmt;
        if (nullable6.GetValueOrDefault() == nullable7.GetValueOrDefault() & nullable6.HasValue == nullable7.HasValue)
          return;
        this.UpdateDiscAmt<DiscountLineFields.curyDiscAmt>(dLine, curyDiscAmt, dLine.CuryDiscAmt);
      }
    }
    else
    {
      if (sender.Graph.IsImport || sender.Graph.IsImportFromExcel)
        return;
      Decimal? discPct = dLine.DiscPct;
      Decimal? curyDiscAmt = dLine.CuryDiscAmt;
      string discountId = dLine.DiscountID;
      dLine.DiscPct = new Decimal?(0M);
      Decimal? nullable8 = discPct;
      Decimal num14 = 0M;
      if (!(nullable8.GetValueOrDefault() == num14 & nullable8.HasValue))
        dLine.RaiseFieldUpdated<DiscountLineFields.discPct>((object) discPct);
      dLine.CuryDiscAmt = new Decimal?(0M);
      Decimal? nullable9 = curyDiscAmt;
      Decimal num15 = 0M;
      if (!(nullable9.GetValueOrDefault() == num15 & nullable9.HasValue))
        dLine.RaiseFieldUpdated<DiscountLineFields.curyDiscAmt>((object) curyDiscAmt);
      nullable9 = dLine.DiscPct;
      Decimal num16 = 0M;
      if (!(nullable9.GetValueOrDefault() == num16 & nullable9.HasValue))
        return;
      nullable9 = dLine.CuryDiscAmt;
      Decimal num17 = 0M;
      if (!(nullable9.GetValueOrDefault() == num17 & nullable9.HasValue))
        return;
      dLine.DiscountID = (string) null;
      dLine.DiscountSequenceID = (string) null;
    }
  }

  /// <summary>
  /// Clears line discount and recalculates all dependent values
  /// </summary>
  /// <param name="cache">Cache</param>
  /// <param name="line">Document line</param>
  protected virtual void ClearLineDiscount(PXCache cache, TLine line, DiscountLineFields dLine)
  {
    this.ApplyDiscountToLine(cache, line, new Decimal?(), new Decimal?(), new Decimal?(), dLine ?? DiscountLineFields.GetMapFor<TLine>(line, cache), new DiscountResult(new Decimal?(0M), true), 1);
  }

  private void UpdateDiscPct<TField>(
    DiscountLineFields dLine,
    Decimal? oldValue,
    Decimal? newValue)
    where TField : IBqlField
  {
    object newValue1 = (object) newValue;
    dLine.RaiseFieldVerifying<TField>(ref newValue1);
    dLine.DiscPct = new Decimal?((Decimal) newValue1);
    dLine.RaiseFieldUpdated<TField>((object) oldValue);
  }

  private void UpdateDiscAmt<TField>(
    DiscountLineFields dLine,
    Decimal? oldValue,
    Decimal? newValue)
    where TField : IBqlField
  {
    object newValue1 = (object) newValue;
    dLine.RaiseFieldVerifying<TField>(ref newValue1);
    dLine.CuryDiscAmt = new Decimal?((Decimal) newValue1);
    dLine.RaiseFieldUpdated<TField>((object) oldValue);
  }

  protected virtual bool SetGroupDiscounts(
    PXCache cache,
    PXSelectBase<TLine> lines,
    TLine currentLine,
    PXSelectBase<TDiscountDetail> discountDetails,
    int? locationID,
    DateTime date,
    DiscountEngine.DiscountCalculationOptions discountCalculationOptions,
    RecalcDiscountsParamFilter recalcFilter = null)
  {
    if (!this.IsDiscountCalculationNeeded(cache, currentLine, "G", discountDetails))
      return false;
    Dictionary<DiscountSequenceKey, TDiscountDetail> outer = new Dictionary<DiscountSequenceKey, TDiscountDetail>();
    ConcurrentDictionary<string, DiscountCode> cachedDiscountCodes = DiscountEngine.GetCachedDiscountCodes();
    List<TLine> documentDetails = this.GetDocumentDetails(cache, lines);
    bool flag = false;
    if (documentDetails.Count != 0)
    {
      bool skipManual = recalcFilter != null && recalcFilter.UseRecalcFilter.GetValueOrDefault() && recalcFilter.OverrideManualDocGroupDiscounts.GetValueOrDefault();
      TwoWayLookup<DiscountSequenceKey, TLine> newGroupDiscountCodesWithApplicableLines = this.CollectAllGroupDiscountCodesWithApplicableLines(cache, documentDetails, discountDetails, locationID, date, skipManual);
      foreach (DiscountSequenceKey leftValue in newGroupDiscountCodesWithApplicableLines.LeftValues)
      {
        if (!discountCalculationOptions.HasFlag((Enum) DiscountEngine.DiscountCalculationOptions.DisableFreeItemDiscountsCalculation) || !(this.SelectDiscountSequence(cache, leftValue.DiscountID, leftValue.DiscountSequenceID).DiscountedFor == "F"))
        {
          DiscountLineFields mapFor = DiscountLineFields.GetMapFor<TLine>(documentDetails[0], cache);
          PXCache cache1 = cache;
          DiscountLineFields dLine = mapFor;
          HashSet<DiscountSequenceKey> discountSequences = new HashSet<DiscountSequenceKey>();
          discountSequences.Add(leftValue);
          Decimal? nullable = leftValue.CuryDiscountableAmount;
          Decimal curyDiscountableAmount = nullable.Value;
          nullable = leftValue.DiscountableQuantity;
          Decimal discountableQuantity = nullable.Value;
          DateTime date1 = date;
          List<DiscountDetailLine> discountDetailLineList = this.SelectApplicableDiscounts(cache1, dLine, discountSequences, curyDiscountableAmount, discountableQuantity, "G", date1);
          PXCache cache2 = cache;
          DiscountLineFields discountedLine = mapFor;
          List<DiscountDetailLine> discounts = discountDetailLineList;
          DateTime date2 = date;
          Dictionary<DiscountSequenceKey, TDiscountDetail> newDiscountDetails = outer;
          nullable = leftValue.CuryDiscountableAmount;
          Decimal discountedLineAmount = nullable.Value;
          nullable = leftValue.DiscountableQuantity;
          Decimal discountedLineQty = nullable.Value;
          this.CreateNewListOfDiscountDetails(cache2, discountedLine, discounts, date2, newDiscountDetails, discountedLineAmount, discountedLineQty);
        }
      }
      this.RemoveUnapplicableDiscountDetails(cache, discountDetails, outer.Keys.ToList<DiscountSequenceKey>(), "G", recalcFilter: recalcFilter);
      if (discountCalculationOptions.HasFlag((Enum) DiscountEngine.DiscountCalculationOptions.DisableAllAutomaticDiscounts))
      {
        if (skipManual)
        {
          foreach (KeyValuePair<DiscountSequenceKey, TDiscountDetail> keyValuePair in outer)
            keyValuePair.Value.IsManual = new bool?(true);
        }
        else
        {
          List<TDiscountDetail> list = this.GetDiscountDetailsByType(cache, discountDetails, "G").Where<TDiscountDetail>((Func<TDiscountDetail, bool>) (x => !x.IsOrigDocDiscount.GetValueOrDefault())).ToList<TDiscountDetail>();
          if (list.Count<TDiscountDetail>() == 0 && !discountCalculationOptions.HasFlag((Enum) DiscountEngine.DiscountCalculationOptions.DisableOrigAutomaticDiscounts))
            outer.Clear();
          if (list.Count > 0 && outer.Count > 0)
            outer = EnumerableExtensions.ToDictionary<DiscountSequenceKey, TDiscountDetail>(outer.Join((IEnumerable<TDiscountDetail>) list, n1 =>
            {
              KeyValuePair<DiscountSequenceKey, TDiscountDetail> keyValuePair = n1;
              string discountId = keyValuePair.Key.DiscountID;
              keyValuePair = n1;
              string discountSequenceId = keyValuePair.Key.DiscountSequenceID;
              return new
              {
                DiscountID = discountId,
                DiscountSequenceID = discountSequenceId
              };
            }, n2 => new
            {
              DiscountID = n2.DiscountID,
              DiscountSequenceID = n2.DiscountSequenceID
            }, (Func<KeyValuePair<DiscountSequenceKey, TDiscountDetail>, TDiscountDetail, KeyValuePair<DiscountSequenceKey, TDiscountDetail>>) ((n1, n2) => n1)));
        }
      }
      foreach (KeyValuePair<DiscountSequenceKey, TDiscountDetail> keyValuePair in outer)
      {
        TDiscountDetail discountDetail = this.UpdateInsertDiscountTrace(cache, discountDetails, keyValuePair.Value);
        if ((object) discountDetail != null && !discountDetail.SkipDiscount.GetValueOrDefault() && cachedDiscountCodes[discountDetail.DiscountID].SkipDocumentDiscounts)
          flag = true;
      }
      if (flag)
      {
        this.RemoveUnapplicableDiscountDetails(cache, discountDetails, (List<DiscountSequenceKey>) null, "D", recalcFilter: recalcFilter);
        this.CalculateDocumentDiscountRate(cache, documentDetails, default (TLine), discountDetails, discountCalculationOptions);
      }
      this.CalculateGroupDiscountRate(cache, documentDetails, currentLine, newGroupDiscountCodesWithApplicableLines, discountDetails, discountCalculationOptions, false, true);
    }
    else
      this.RemoveUnapplicableDiscountDetails(cache, discountDetails, (List<DiscountSequenceKey>) null, "G", recalcFilter: recalcFilter);
    return flag;
  }

  protected virtual TwoWayLookup<DiscountSequenceKey, TLine> AdjustGroupDiscountRates(
    PXCache cache,
    PXSelectBase<TLine> lines,
    PXSelectBase<TDiscountDetail> discountDetails,
    int? locationID,
    DateTime date)
  {
    List<TLine> documentDetails = this.GetDocumentDetails(cache, lines);
    DiscountEngine.GetCachedDiscountCodes();
    TwoWayLookup<DiscountSequenceKey, TLine> newGroupDiscountCodesWithApplicableLines = new TwoWayLookup<DiscountSequenceKey, TLine>((IEqualityComparer<DiscountSequenceKey>) null, (IEqualityComparer<TLine>) null, (Func<DiscountSequenceKey, TLine, bool>) null);
    if (documentDetails.Count != 0)
    {
      newGroupDiscountCodesWithApplicableLines = this.CollectAllGroupDiscountCodesWithApplicableLines(cache, documentDetails, discountDetails, locationID, date);
      this.CalculateGroupDiscountRate(cache, documentDetails, default (TLine), newGroupDiscountCodesWithApplicableLines, discountDetails, true);
    }
    return newGroupDiscountCodesWithApplicableLines;
  }

  public virtual TwoWayLookup<TDiscountDetail, TLine> GetListOfDiscountCodesWithApplicableLines(
    PXCache cache,
    List<TLine> documentDetails,
    PXSelectBase<TDiscountDetail> discountDetails,
    string discountType)
  {
    TwoWayLookup<TDiscountDetail, TLine> withApplicableLines = new TwoWayLookup<TDiscountDetail, TLine>((IEqualityComparer<TDiscountDetail>) new DiscountEngine<TLine, TDiscountDetail>.TDiscountDetailComparer(), (IEqualityComparer<TLine>) null, (Func<TDiscountDetail, TLine, bool>) null);
    List<TDiscountDetail> discountDetailsByType = this.GetDiscountDetailsByType(cache, discountDetails, discountType);
    foreach (TLine documentDetail in documentDetails)
    {
      ushort[] discountsAppliedToLine = DiscountLineFields.GetMapFor<TLine>(documentDetail, cache).DiscountsAppliedToLine;
      foreach (TDiscountDetail discountDetail in discountDetailsByType)
      {
        ushort? lineNbr = discountDetail.LineNbr;
        if (lineNbr.HasValue && discountsAppliedToLine != null)
        {
          ushort[] source = discountsAppliedToLine;
          lineNbr = discountDetail.LineNbr;
          int valueOrDefault = (int) lineNbr.GetValueOrDefault();
          if (((IEnumerable<ushort>) source).Contains<ushort>((ushort) valueOrDefault))
            withApplicableLines.Link(discountDetail, documentDetail);
        }
      }
    }
    return withApplicableLines;
  }

  protected HashSet<DiscountSequenceKey> FilterManualDiscountDetails(
    ConcurrentDictionary<string, DiscountCode> cachedDiscountTypes,
    IList<TDiscountDetail> typedDiscountDetails)
  {
    HashSet<DiscountSequenceKey> discountSequenceKeySet = new HashSet<DiscountSequenceKey>();
    foreach (TDiscountDetail typedDiscountDetail in (IEnumerable<TDiscountDetail>) typedDiscountDetails)
    {
      DiscountSequenceKey discountSequenceKey = new DiscountSequenceKey(typedDiscountDetail.DiscountID, typedDiscountDetail.DiscountSequenceID);
      if (typedDiscountDetail.DiscountID != null && cachedDiscountTypes[typedDiscountDetail.DiscountID].IsManual)
      {
        bool? isOrigDocDiscount = typedDiscountDetail.IsOrigDocDiscount;
        bool flag = false;
        if (isOrigDocDiscount.GetValueOrDefault() == flag & isOrigDocDiscount.HasValue)
          discountSequenceKeySet.Add(discountSequenceKey);
      }
    }
    return discountSequenceKeySet;
  }

  protected virtual TwoWayLookup<DiscountSequenceKey, TLine> CollectAllGroupDiscountCodesWithApplicableLines(
    PXCache cache,
    List<TLine> documentDetails,
    PXSelectBase<TDiscountDetail> discountDetails,
    int? locationID,
    DateTime date,
    bool skipManual = false)
  {
    ConcurrentDictionary<string, DiscountCode> cachedDiscountCodes = DiscountEngine.GetCachedDiscountCodes();
    TwoWayLookup<DiscountSequenceKey, TLine> discountCodesWithLines = new TwoWayLookup<DiscountSequenceKey, TLine>((IEqualityComparer<DiscountSequenceKey>) null, (IEqualityComparer<TLine>) null, (Func<DiscountSequenceKey, TLine, bool>) null);
    List<TDiscountDetail> discountDetailsByType = this.GetDiscountDetailsByType(cache, discountDetails, "G");
    HashSet<DiscountSequenceKey> manualDiscountDetails = this.FilterManualDiscountDetails(cachedDiscountCodes, (IList<TDiscountDetail>) discountDetailsByType);
    foreach (TLine documentDetail in documentDetails)
    {
      DiscountLineFields mapFor = DiscountLineFields.GetMapFor<TLine>(documentDetail, cache);
      if (!mapFor.AutomaticDiscountsDisabled.GetValueOrDefault() && (mapFor.DiscountID == null || !cachedDiscountCodes[mapFor.DiscountID].ExcludeFromDiscountableAmt))
      {
        HashSet<DiscountSequenceKey> allApplicableDiscounts = this.SelectApplicableEntityDiscounts(cache.Graph, this.GetDiscountEntitiesDiscounts(cache, documentDetail, locationID, true), "G", skipManual);
        HashSet<DiscountSequenceKey> applicableGroupDiscounts = this.RemoveManualDiscounts(cache, allApplicableDiscounts, manualDiscountDetails);
        this.CombineDiscountsAndDocumentLines(cache, documentDetail, applicableGroupDiscounts, ref discountCodesWithLines);
      }
    }
    return discountCodesWithLines;
  }

  protected virtual void CombineDiscountsAndDocumentLines(
    PXCache cache,
    TLine line,
    HashSet<DiscountSequenceKey> applicableGroupDiscounts,
    ref TwoWayLookup<DiscountSequenceKey, TLine> discountCodesWithLines)
  {
    if (applicableGroupDiscounts.Count == 0)
      return;
    AmountLineFields mapFor = AmountLineFields.GetMapFor<TLine>(line, cache);
    DiscountableValues discountableValues1 = new DiscountableValues();
    ref DiscountableValues local1 = ref discountableValues1;
    Decimal? nullable1 = mapFor.CuryLineAmount;
    Decimal? nullable2 = new Decimal?(nullable1.GetValueOrDefault());
    local1.CuryDiscountableAmount = nullable2;
    ref DiscountableValues local2 = ref discountableValues1;
    nullable1 = mapFor.Quantity;
    Decimal? nullable3 = new Decimal?(nullable1.GetValueOrDefault());
    local2.DiscountableQuantity = nullable3;
    DiscountableValues discountableValues2 = discountableValues1;
    foreach (DiscountSequenceKey applicableGroupDiscount in applicableGroupDiscounts)
    {
      DiscountSequenceKey dSequence = applicableGroupDiscount;
      if (discountCodesWithLines.Contains(dSequence))
      {
        DiscountSequenceKey discountSequenceKey1 = discountCodesWithLines.LeftValues.First<DiscountSequenceKey>((Func<DiscountSequenceKey, bool>) (x => x.Equals((object) dSequence)));
        DiscountSequenceKey discountSequenceKey2 = discountSequenceKey1;
        Decimal? nullable4 = discountSequenceKey2.CuryDiscountableAmount;
        Decimal? nullable5 = discountableValues2.CuryDiscountableAmount;
        discountSequenceKey2.CuryDiscountableAmount = nullable4.HasValue & nullable5.HasValue ? new Decimal?(nullable4.GetValueOrDefault() + nullable5.GetValueOrDefault()) : new Decimal?();
        DiscountSequenceKey discountSequenceKey3 = discountSequenceKey1;
        nullable5 = discountSequenceKey3.DiscountableQuantity;
        nullable4 = discountableValues2.DiscountableQuantity;
        discountSequenceKey3.DiscountableQuantity = nullable5.HasValue & nullable4.HasValue ? new Decimal?(nullable5.GetValueOrDefault() + nullable4.GetValueOrDefault()) : new Decimal?();
        discountCodesWithLines.Link(discountSequenceKey1, line);
      }
      else
      {
        dSequence.CuryDiscountableAmount = discountableValues2.CuryDiscountableAmount;
        dSequence.DiscountableQuantity = discountableValues2.DiscountableQuantity;
        discountCodesWithLines.Link(dSequence, line);
      }
    }
  }

  public virtual TwoWayLookup<TDiscountDetail, TLine> MatchLinesWithDiscounts(
    PXCache cache,
    TwoWayLookup<DiscountSequenceKey, TLine> newDiscountCodesWithApplicableLines,
    List<TLine> documentDetails,
    PXSelectBase<TDiscountDetail> discountDetails,
    bool ignoreExistingGroupDiscounts,
    bool ignoreExistingDocumentDiscounts)
  {
    List<TDiscountDetail> discountDetailsByType = this.GetDiscountDetailsByType(cache, discountDetails, (string) null);
    TwoWayLookup<TDiscountDetail, TLine> twoWayLookup = new TwoWayLookup<TDiscountDetail, TLine>((IEqualityComparer<TDiscountDetail>) new DiscountEngine<TLine, TDiscountDetail>.TDiscountDetailComparer(), (IEqualityComparer<TLine>) null, (Func<TDiscountDetail, TLine, bool>) null);
    foreach (TLine documentDetail in documentDetails)
    {
      ushort[] discountsAppliedToLine = DiscountLineFields.GetMapFor<TLine>(documentDetail, cache).DiscountsAppliedToLine;
      foreach (TDiscountDetail discountDetail in discountDetailsByType)
      {
        DiscountSequenceKey discountSequenceKey = new DiscountSequenceKey(discountDetail.DiscountID, discountDetail.DiscountSequenceID);
        bool? isOrigDocDiscount = discountDetail.IsOrigDocDiscount;
        if ((isOrigDocDiscount.GetValueOrDefault() || discountDetail.Type == "G" && !ignoreExistingGroupDiscounts || discountDetail.Type == "D" && !ignoreExistingDocumentDiscounts) && discountsAppliedToLine != null && ((IEnumerable<ushort>) discountsAppliedToLine).Contains<ushort>(discountDetail.LineNbr.GetValueOrDefault()))
        {
          twoWayLookup.Link(discountDetail, documentDetail);
        }
        else
        {
          isOrigDocDiscount = discountDetail.IsOrigDocDiscount;
          if (!isOrigDocDiscount.GetValueOrDefault() && newDiscountCodesWithApplicableLines.RightValues.Contains<TLine>(documentDetail) && newDiscountCodesWithApplicableLines.LeftsFor(documentDetail).Contains<DiscountSequenceKey>(discountSequenceKey))
            twoWayLookup.Link(discountDetail, documentDetail);
        }
      }
    }
    return twoWayLookup;
  }

  public virtual TwoWayLookup<TDiscountDetail, TLine> GetListOfLinksBetweenDiscountsAndDocumentLines(
    PXCache cache,
    PXSelectBase<TLine> documentDetailsSelect,
    PXSelectBase<TDiscountDetail> discountDetailsSelect,
    object[] documentDetailsSelectParams = null,
    object[] discountDetailsSelectParams = null)
  {
    return this.GetListOfLinksBetweenDiscountsAndDocumentLines(cache, documentDetailsSelect, discountDetailsSelect, DiscountEngine.DiscountCalculationOptions.CalculateAll, documentDetailsSelectParams, discountDetailsSelectParams);
  }

  public virtual TwoWayLookup<TDiscountDetail, TLine> GetListOfLinksBetweenDiscountsAndDocumentLines(
    PXCache cache,
    PXSelectBase<TLine> documentDetailsSelect,
    PXSelectBase<TDiscountDetail> discountDetailsSelect,
    DiscountEngine.DiscountCalculationOptions discountCalculationOptions,
    object[] documentDetailsSelectParams = null,
    object[] discountDetailsSelectParams = null)
  {
    List<TDiscountDetail> discountDetailsByType = this.GetDiscountDetailsByType(cache, discountDetailsSelect, (string) null, discountDetailsSelectParams);
    if (!discountDetailsByType.Any<TDiscountDetail>())
      return new TwoWayLookup<TDiscountDetail, TLine>((IEqualityComparer<TDiscountDetail>) new DiscountEngine<TLine, TDiscountDetail>.TDiscountDetailComparer(), (IEqualityComparer<TLine>) null, (Func<TDiscountDetail, TLine, bool>) null);
    List<TLine> documentDetails = this.GetDocumentDetails(cache, documentDetailsSelect, documentDetailsSelectParams);
    return this.GetListOfLinksBetweenDiscountsAndDocumentLines(cache, documentDetails, discountDetailsByType, discountCalculationOptions);
  }

  public virtual TwoWayLookup<TDiscountDetail, TLine> GetListOfLinksBetweenDiscountsAndDocumentLines(
    PXCache cache,
    List<TLine> documentDetails,
    List<TDiscountDetail> discountDetails)
  {
    return this.GetListOfLinksBetweenDiscountsAndDocumentLines(cache, documentDetails, discountDetails, DiscountEngine.DiscountCalculationOptions.CalculateAll);
  }

  /// <summary>
  /// Creates TwoWayLookup with all links between Document Details lines and Discount Details lines.
  /// </summary>
  public virtual TwoWayLookup<TDiscountDetail, TLine> GetListOfLinksBetweenDiscountsAndDocumentLines(
    PXCache cache,
    List<TLine> documentDetails,
    List<TDiscountDetail> discountDetails,
    DiscountEngine.DiscountCalculationOptions discountCalculationOptions)
  {
    TwoWayLookup<TDiscountDetail, TLine> andDocumentLines = new TwoWayLookup<TDiscountDetail, TLine>((IEqualityComparer<TDiscountDetail>) new DiscountEngine<TLine, TDiscountDetail>.TDiscountDetailComparer(), (IEqualityComparer<TLine>) null, (Func<TDiscountDetail, TLine, bool>) null);
    foreach (TLine documentDetail in documentDetails)
    {
      DiscountLineFields mapFor = DiscountLineFields.GetMapFor<TLine>(documentDetail, cache);
      if (!mapFor.AutomaticDiscountsDisabled.GetValueOrDefault() || discountCalculationOptions.HasFlag((Enum) DiscountEngine.DiscountCalculationOptions.DisableOrigAutomaticDiscounts))
      {
        ushort[] discountsAppliedToLine = mapFor.DiscountsAppliedToLine;
        foreach (TDiscountDetail discountDetail in discountDetails)
        {
          if (discountsAppliedToLine != null && ((IEnumerable<ushort>) discountsAppliedToLine).Contains<ushort>(discountDetail.LineNbr.GetValueOrDefault()) || !discountDetail.IsOrigDocDiscount.GetValueOrDefault() && EnumerableExtensions.IsIn<string>(discountDetail.Type, "D", "B"))
            andDocumentLines.Link(discountDetail, documentDetail);
        }
      }
    }
    if (andDocumentLines.LeftValues.Count<TDiscountDetail>() != discountDetails.Count)
    {
      foreach (TDiscountDetail discountDetail in discountDetails)
      {
        if (!andDocumentLines.LeftValues.Contains<TDiscountDetail>(discountDetail))
          andDocumentLines.Add(discountDetail);
      }
    }
    return andDocumentLines;
  }

  public virtual TwoWayLookup<TDiscountDetail, TLine> GetListOfDiscountsAppliedToLine(
    PXCache cache,
    TLine line,
    List<TDiscountDetail> discountDetails)
  {
    TwoWayLookup<TDiscountDetail, TLine> discountsAppliedToLine1 = new TwoWayLookup<TDiscountDetail, TLine>((IEqualityComparer<TDiscountDetail>) new DiscountEngine<TLine, TDiscountDetail>.TDiscountDetailComparer(), (IEqualityComparer<TLine>) null, (Func<TDiscountDetail, TLine, bool>) null);
    DiscountLineFields mapFor = DiscountLineFields.GetMapFor<TLine>(line, cache);
    ushort[] discountsAppliedToLine2 = mapFor.DiscountsAppliedToLine;
    if (!mapFor.AutomaticDiscountsDisabled.GetValueOrDefault() && discountsAppliedToLine2 != null)
    {
      foreach (TDiscountDetail discountDetail in discountDetails)
      {
        if (((IEnumerable<ushort>) discountsAppliedToLine2).Contains<ushort>(discountDetail.LineNbr.GetValueOrDefault()) || !discountDetail.IsOrigDocDiscount.GetValueOrDefault() && EnumerableExtensions.IsIn<string>(discountDetail.Type, "D", "B"))
          discountsAppliedToLine1.Link(discountDetail, line);
      }
    }
    return discountsAppliedToLine1;
  }

  public virtual void UpdateOrigGroupAndDocumentDiscounts(
    PXCache cache,
    PXSelectBase<TLine> lines,
    PXSelectBase<TDiscountDetail> discountDetails,
    DiscountEngine.DiscountCalculationOptions discountCalculationOptions)
  {
    TwoWayLookup<TDiscountDetail, TLine> andDocumentLines = this.GetListOfLinksBetweenDiscountsAndDocumentLines(cache, lines, discountDetails, discountCalculationOptions);
    foreach (TDiscountDetail leftValue in andDocumentLines.LeftValues)
    {
      if (leftValue.IsOrigDocDiscount.GetValueOrDefault())
      {
        Decimal val1 = 0M;
        Decimal val2 = 0M;
        Decimal num1 = 0M;
        Decimal? nullable1;
        foreach (TLine line in andDocumentLines.RightsFor(leftValue))
        {
          AmountLineFields mapFor = AmountLineFields.GetMapFor<TLine>(line, cache);
          Decimal num2 = val1;
          nullable1 = mapFor.CuryLineAmount;
          Decimal valueOrDefault1 = nullable1.GetValueOrDefault();
          val1 = num2 + valueOrDefault1;
          Decimal num3 = num1;
          nullable1 = mapFor.Quantity;
          Decimal valueOrDefault2 = nullable1.GetValueOrDefault();
          num1 = num3 + valueOrDefault2;
          if (leftValue.Type == "G")
          {
            Decimal num4 = val2;
            nullable1 = mapFor.CuryLineAmount;
            Decimal valueOrDefault3 = nullable1.GetValueOrDefault();
            Decimal num5 = (Decimal) 1;
            nullable1 = mapFor.OrigGroupDiscountRate;
            Decimal num6 = (nullable1.HasValue ? new Decimal?(num5 - nullable1.GetValueOrDefault()) : new Decimal?()) ?? 1M;
            Decimal num7 = valueOrDefault3 * num6;
            val2 = num4 + num7;
          }
          else if (leftValue.Type == "D")
          {
            Decimal num8 = val1;
            nullable1 = mapFor.CuryLineAmount;
            Decimal valueOrDefault4 = nullable1.GetValueOrDefault();
            Decimal num9 = (Decimal) 1;
            nullable1 = mapFor.OrigGroupDiscountRate;
            Decimal num10 = (nullable1.HasValue ? new Decimal?(num9 - nullable1.GetValueOrDefault()) : new Decimal?()) ?? 1M;
            Decimal num11 = valueOrDefault4 * num10;
            val1 = num8 - num11;
            Decimal num12 = val2;
            nullable1 = mapFor.CuryLineAmount;
            Decimal valueOrDefault5 = nullable1.GetValueOrDefault();
            nullable1 = mapFor.CuryLineAmount;
            Decimal valueOrDefault6 = nullable1.GetValueOrDefault();
            Decimal num13 = (Decimal) 1;
            nullable1 = mapFor.OrigGroupDiscountRate;
            Decimal num14 = (nullable1.HasValue ? new Decimal?(num13 - nullable1.GetValueOrDefault()) : new Decimal?()) ?? 1M;
            Decimal num15 = valueOrDefault6 * num14;
            Decimal num16 = valueOrDefault5 - num15;
            Decimal num17 = (Decimal) 1;
            Decimal? documentDiscountRate = mapFor.OrigDocumentDiscountRate;
            Decimal num18 = (documentDiscountRate.HasValue ? new Decimal?(num17 - documentDiscountRate.GetValueOrDefault()) : new Decimal?()) ?? 1M;
            Decimal num19 = num16 * num18;
            val2 = num12 + num19;
          }
        }
        leftValue.CuryDiscountableAmt = new Decimal?(MultiCurrencyCalculator.RoundCury(cache, (object) leftValue, val1));
        leftValue.CuryDiscountAmt = new Decimal?(MultiCurrencyCalculator.RoundCury(cache, (object) leftValue, val2));
        leftValue.DiscountableQty = new Decimal?(num1);
        if (leftValue.CuryDiscountAmt.IsNullOrZero() || leftValue.CuryDiscountableAmt.IsNullOrZero())
        {
          leftValue.DiscountPct = new Decimal?(0M);
        }
        else
        {
          // ISSUE: variable of a boxed type
          __Boxed<TDiscountDetail> local = (object) leftValue;
          Decimal valueOrDefault = leftValue.CuryDiscountAmt.GetValueOrDefault();
          nullable1 = leftValue.CuryDiscountableAmt;
          Decimal num20 = nullable1 ?? 1M;
          Decimal? nullable2 = new Decimal?(valueOrDefault / num20 * 100M);
          local.DiscountPct = nullable2;
        }
        this.UpdateDiscountDetail(cache, discountDetails, leftValue);
      }
    }
  }

  public virtual void UpdateGroupAndDocumentDiscountRatesOnly(
    PXCache cache,
    PXSelectBase<TLine> lines,
    TLine currentLine,
    PXSelectBase<TDiscountDetail> discountDetails,
    bool recalculateTaxes,
    bool calculateOrigRate,
    bool recalcAll = true)
  {
    List<TLine> documentDetails = this.GetDocumentDetails(cache, lines);
    TwoWayLookup<TDiscountDetail, TLine> withApplicableLines = this.GetListOfDiscountCodesWithApplicableLines(cache, documentDetails, discountDetails, (string) null);
    this.CalculateGroupDiscountRate(cache, documentDetails, currentLine, withApplicableLines, recalculateTaxes, recalcAll, calculateOrigRate: calculateOrigRate);
    this.CalculateDocumentDiscountRate(cache, withApplicableLines, currentLine, lines, calculateOrigRate: calculateOrigRate);
  }

  public virtual void CalculateGroupDiscountRate(
    PXCache cache,
    PXSelectBase<TLine> lines,
    TLine currentLine,
    TwoWayLookup<TDiscountDetail, TLine> discountCodesWithApplicableLines,
    bool recalculateTaxes,
    object[] documentLinesSelectParams = null,
    bool recalcAll = true,
    bool forceFormulaCalculation = false,
    bool calculateOrigRate = false)
  {
    this.CalculateGroupDiscountRate(cache, lines, currentLine, discountCodesWithApplicableLines, false, recalculateTaxes, documentLinesSelectParams, recalcAll, forceFormulaCalculation, calculateOrigRate);
  }

  public virtual void CalculateGroupDiscountRate(
    PXCache cache,
    PXSelectBase<TLine> lines,
    TLine currentLine,
    TwoWayLookup<TDiscountDetail, TLine> discountCodesWithApplicableLines,
    bool useLegacyDetailsSelectMethod,
    bool recalculateTaxes,
    object[] documentLinesSelectParams = null,
    bool recalcAll = true,
    bool forceFormulaCalculation = false,
    bool calculateOrigRate = false)
  {
    List<TLine> documentDetails = this.GetDocumentDetails(cache, lines, useLegacyDetailsSelectMethod, documentLinesSelectParams);
    this.CalculateGroupDiscountRate(cache, documentDetails, currentLine, discountCodesWithApplicableLines, recalculateTaxes, recalcAll, forceFormulaCalculation, calculateOrigRate);
  }

  public virtual void CalculateGroupDiscountRate(
    PXCache cache,
    List<TLine> documentDetails,
    TLine currentLine,
    TwoWayLookup<DiscountSequenceKey, TLine> newGroupDiscountCodesWithApplicableLines,
    PXSelectBase<TDiscountDetail> discountDetails,
    bool recalculateTaxes,
    bool recalcAll = true)
  {
    this.CalculateGroupDiscountRate(cache, documentDetails, currentLine, this.MatchLinesWithDiscounts(cache, newGroupDiscountCodesWithApplicableLines, documentDetails, discountDetails, true, false), recalculateTaxes, recalcAll);
  }

  [Obsolete("This method has been deprecated and will be removed in Acumatica ERP 2025 R1.")]
  public virtual void CalculateGroupDiscountRate(
    PXCache cache,
    List<TLine> documentDetails,
    TLine currentLine,
    TwoWayLookup<DiscountSequenceKey, TLine> newGroupDiscountCodesWithApplicableLines,
    PXSelectBase<TDiscountDetail> discountDetails,
    bool recalculateTaxes,
    bool recalcAll,
    bool forceFormulaCalculation)
  {
    this.CalculateGroupDiscountRate(cache, documentDetails, currentLine, this.MatchLinesWithDiscounts(cache, newGroupDiscountCodesWithApplicableLines, documentDetails, discountDetails, true, false), recalculateTaxes, recalcAll, forceFormulaCalculation);
  }

  public virtual void CalculateGroupDiscountRate(
    PXCache cache,
    List<TLine> documentDetails,
    TLine currentLine,
    TwoWayLookup<DiscountSequenceKey, TLine> newGroupDiscountCodesWithApplicableLines,
    PXSelectBase<TDiscountDetail> discountDetails,
    DiscountEngine.DiscountCalculationOptions discountCalculationOptions,
    bool recalculateTaxes,
    bool recalcAll)
  {
    this.CalculateGroupDiscountRate(cache, documentDetails, currentLine, this.MatchLinesWithDiscounts(cache, newGroupDiscountCodesWithApplicableLines, documentDetails, discountDetails, true, false), discountCalculationOptions, recalculateTaxes, recalcAll, discountCalculationOptions.HasFlag((Enum) DiscountEngine.DiscountCalculationOptions.ForceFormulaRecalculation));
  }

  public virtual void CalculateGroupDiscountRate(
    PXCache cache,
    List<TLine> documentDetails,
    TLine currentLine,
    TwoWayLookup<TDiscountDetail, TLine> discountCodesWithApplicableLines,
    bool recalculateTaxes,
    bool recalcAll = true,
    bool forceFormulaCalculation = false,
    bool calculateOrigRate = false)
  {
    this.CalculateGroupDiscountRate(cache, documentDetails, currentLine, discountCodesWithApplicableLines, DiscountEngine.DiscountCalculationOptions.CalculateAll, recalculateTaxes, recalcAll, forceFormulaCalculation, calculateOrigRate);
  }

  public virtual void CalculateGroupDiscountRate(
    PXCache cache,
    List<TLine> documentDetails,
    TLine currentLine,
    TwoWayLookup<TDiscountDetail, TLine> discountCodesWithApplicableLines,
    DiscountEngine.DiscountCalculationOptions discountCalculationOptions,
    bool recalculateTaxes,
    bool recalcAll = true,
    bool forceFormulaCalculation = false,
    bool calculateOrigRate = false)
  {
    DiscountEngine.ApplyQuantityDiscountByBaseUOMOption discountByBaseUomOption = DiscountEngine.ApplyQuantityDiscountByBaseUOM(cache.Graph);
    foreach (TLine documentDetail in documentDetails)
    {
      TLine copy1 = (TLine) cache.CreateCopy((object) currentLine);
      AmountLineFields mapFor = AmountLineFields.GetMapFor<TLine>(documentDetail, cache, new DiscountEngine.ApplyQuantityDiscountByBaseUOMOption?(discountByBaseUomOption));
      DiscountLineFields.GetMapFor<TLine>(documentDetail, cache);
      TLine copy2 = (TLine) cache.CreateCopy((object) documentDetail);
      if (!discountCalculationOptions.HasFlag((Enum) DiscountEngine.DiscountCalculationOptions.DisableOrigAutomaticDiscounts))
        mapFor.OrigGroupDiscountRate = this.CalculateGroupDiscountRateOnSpecificLine(cache, documentDetail, discountCodesWithApplicableLines, new bool?(true), recalcAll);
      mapFor.GroupDiscountRate = this.CalculateGroupDiscountRateOnSpecificLine(cache, documentDetail, discountCodesWithApplicableLines, new bool?(false), recalcAll);
      this.UpdateListOfDiscountsAppliedToLine(cache, discountCodesWithApplicableLines, documentDetail, discountCalculationOptions);
      Decimal? groupDiscountRate1 = AmountLineFields.GetMapFor<TLine>(copy2, cache, new DiscountEngine.ApplyQuantityDiscountByBaseUOMOption?(discountByBaseUomOption)).GroupDiscountRate;
      Decimal? groupDiscountRate2 = mapFor.GroupDiscountRate;
      if (groupDiscountRate1.GetValueOrDefault() == groupDiscountRate2.GetValueOrDefault() & groupDiscountRate1.HasValue == groupDiscountRate2.HasValue)
      {
        groupDiscountRate2 = AmountLineFields.GetMapFor<TLine>(copy2, cache, new DiscountEngine.ApplyQuantityDiscountByBaseUOMOption?(discountByBaseUomOption)).OrigGroupDiscountRate;
        groupDiscountRate1 = mapFor.OrigGroupDiscountRate;
        if (groupDiscountRate2.GetValueOrDefault() == groupDiscountRate1.GetValueOrDefault() & groupDiscountRate2.HasValue == groupDiscountRate1.HasValue)
          continue;
      }
      this.RecalcTaxes(cache, documentDetail, copy2, copy1);
      if (forceFormulaCalculation)
        cache.RaiseRowUpdated((object) documentDetail, (object) copy2);
    }
  }

  public virtual Decimal? CalculateGroupDiscountRateOnSpecificLine(
    PXCache cache,
    TLine line,
    TwoWayLookup<TDiscountDetail, TLine> discountCodesWithApplicableLines,
    bool? discountSelectionMode = null,
    bool recalcAll = true)
  {
    Decimal? rateOnSpecificLine = new Decimal?(1M);
    AmountLineFields lineAmt = AmountLineFields.GetMapFor<TLine>(line, cache);
    TLine copy = (TLine) cache.CreateCopy((object) line);
    Decimal? nullable1;
    Decimal num1;
    if (!recalcAll)
    {
      nullable1 = lineAmt.CuryLineAmount;
      Decimal num2 = nullable1.Value;
      nullable1 = lineAmt.GroupDiscountRate;
      Decimal num3 = nullable1.Value;
      num1 = num2 * num3;
    }
    else
    {
      nullable1 = lineAmt.CuryLineAmount;
      num1 = nullable1.Value;
    }
    Decimal num4 = num1;
    nullable1 = lineAmt.CuryLineAmount;
    if (nullable1.HasValue)
    {
      nullable1 = lineAmt.CuryLineAmount;
      Decimal num5 = 0M;
      if (!(nullable1.GetValueOrDefault() == num5 & nullable1.HasValue))
      {
        foreach (Decimal num6 in discountCodesWithApplicableLines.LeftsFor(line).Where<TDiscountDetail>((Func<TDiscountDetail, bool>) (d =>
        {
          if (d.SkipDiscount.GetValueOrDefault() || !(d.Type == "G"))
            return false;
          if (!discountSelectionMode.HasValue)
            return true;
          bool? isOrigDocDiscount = d.IsOrigDocDiscount;
          bool? nullable2 = discountSelectionMode;
          return isOrigDocDiscount.GetValueOrDefault() == nullable2.GetValueOrDefault() & isOrigDocDiscount.HasValue == nullable2.HasValue;
        })).Where<TDiscountDetail>((Func<TDiscountDetail, bool>) (dDetail =>
        {
          if (!dDetail.CuryDiscountableAmt.HasValue)
            return false;
          Decimal? curyDiscountableAmt = dDetail.CuryDiscountableAmt;
          Decimal num7 = 0M;
          return !(curyDiscountableAmt.GetValueOrDefault() == num7 & curyDiscountableAmt.HasValue);
        })).Select<TDiscountDetail, Decimal>((Func<TDiscountDetail, Decimal>) (dDetail =>
        {
          Decimal? nullable3 = lineAmt.CuryLineAmount;
          Decimal num8 = nullable3.Value;
          nullable3 = dDetail.CuryDiscountAmt;
          Decimal num9 = nullable3.Value;
          Decimal num10 = num8 * num9;
          nullable3 = dDetail.CuryDiscountableAmt;
          Decimal num11 = nullable3.Value;
          return num10 / num11;
        })))
        {
          if (num4 != 0M)
            num4 -= num6;
        }
        Decimal num12 = num4;
        Decimal? curyLineAmount = AmountLineFields.GetMapFor<TLine>(copy, cache).CuryLineAmount;
        return new Decimal?(DiscountEngine.RoundDiscountRate((curyLineAmount.HasValue ? new Decimal?(num12 / curyLineAmount.GetValueOrDefault()) : new Decimal?()) ?? 1M));
      }
    }
    return rateOnSpecificLine;
  }

  protected virtual bool UpdateGroupDiscountRate(AmountLineFields aLine, Decimal? discountRate)
  {
    Decimal? groupDiscountRate1 = aLine.GroupDiscountRate;
    Decimal? groupDiscountRate2 = aLine.GroupDiscountRate;
    Decimal? nullable = discountRate;
    if (groupDiscountRate2.GetValueOrDefault() == nullable.GetValueOrDefault() & groupDiscountRate2.HasValue == nullable.HasValue)
      return false;
    aLine.GroupDiscountRate = discountRate;
    aLine.RaiseFieldUpdated<AmountLineFields.groupDiscountRate>((object) groupDiscountRate1);
    return true;
  }

  public virtual void CalculateDocumentDiscountRate(
    PXCache cache,
    PXSelectBase<TLine> documentDetails,
    TLine currentLine,
    PXSelectBase<TDiscountDetail> discountDetails,
    DiscountEngine.DiscountCalculationOptions discountCalculationOptions,
    bool forceFormulaCalculation = false)
  {
    List<TLine> documentDetails1 = this.GetDocumentDetails(cache, documentDetails);
    this.CalculateDocumentDiscountRate(cache, documentDetails1, currentLine, discountDetails, discountCalculationOptions, forceFormulaCalculation);
  }

  public bool IsDocumentDiscountRateCalculationNeeded(
    PXCache cache,
    TLine currentLine,
    PXSelectBase<TDiscountDetail> discountDetails)
  {
    if (((IQueryable<PXResult<TDiscountDetail>>) discountDetails.Select(Array.Empty<object>())).Any<PXResult<TDiscountDetail>>() || NonGenericIEnumerableExtensions.Any_(((PXSelectBase) discountDetails).Cache.Cached))
      return true;
    AmountLineFields mapFor1 = AmountLineFields.GetMapFor<TLine>(currentLine, cache, false);
    DiscountLineFields mapFor2 = DiscountLineFields.GetMapFor<TLine>(currentLine, cache);
    mapFor1.OrigDocumentDiscountRate = new Decimal?((Decimal) 1);
    mapFor1.DocumentDiscountRate = new Decimal?((Decimal) 1);
    mapFor1.OrigGroupDiscountRate = new Decimal?((Decimal) 1);
    mapFor1.GroupDiscountRate = new Decimal?((Decimal) 1);
    mapFor2.DiscountsAppliedToLine = new ushort[0];
    if (!cache.GetAttributesReadonly<AmountLineFields.taxCategoryID>().OfType<TaxAttribute>().Any<TaxAttribute>())
      TaxBaseAttribute.InvokeRecalcTaxes(cache);
    return false;
  }

  public virtual void CalculateDocumentDiscountRate(
    PXCache cache,
    TwoWayLookup<TDiscountDetail, TLine> discountsLinkedToLine,
    TLine currentLine,
    PXSelectBase<TLine> documentDetails = null,
    bool forceFormulaCalculation = false,
    bool calculateOrigRate = false)
  {
    List<TLine> lineList = discountsLinkedToLine.RightValues.ToList<TLine>();
    if (lineList.Count<TLine>() == 0 && documentDetails != null)
      lineList = this.GetDocumentDetails(cache, documentDetails);
    this.ApplyDocumentDiscountRatesToLine(cache, discountsLinkedToLine, lineList, currentLine, DiscountEngine.DiscountCalculationOptions.CalculateAll, forceFormulaCalculation, calculateOrigRate);
  }

  private void CalculateDocumentDiscountRate(
    PXCache cache,
    List<TLine> documentDetails,
    TLine currentLine,
    PXSelectBase<TDiscountDetail> discountDetails,
    DiscountEngine.DiscountCalculationOptions discountCalculationOptions,
    bool forceFormulaCalculation = false)
  {
    List<TDiscountDetail> discountDetailsByType = this.GetDiscountDetailsByType(cache, discountDetails, (string) null);
    this.CalculateDocumentDiscountRate(cache, documentDetails, currentLine, discountDetailsByType, discountCalculationOptions, forceFormulaCalculation);
  }

  private void CalculateDocumentDiscountRate(
    PXCache cache,
    List<TLine> documentDetails,
    TLine currentLine,
    List<TDiscountDetail> discountDetails,
    DiscountEngine.DiscountCalculationOptions discountCalculationOptions,
    bool forceFormulaCalculation = false)
  {
    TwoWayLookup<TDiscountDetail, TLine> andDocumentLines = this.GetListOfLinksBetweenDiscountsAndDocumentLines(cache, documentDetails, discountDetails, discountCalculationOptions);
    this.ApplyDocumentDiscountRatesToLine(cache, andDocumentLines, documentDetails, currentLine, discountCalculationOptions, forceFormulaCalculation);
  }

  private void ApplyDocumentDiscountRatesToLine(
    PXCache cache,
    TwoWayLookup<TDiscountDetail, TLine> discountsLinkedToLine,
    List<TLine> documentDetails,
    TLine currentLine,
    DiscountEngine.DiscountCalculationOptions discountCalculationOptions,
    bool forceFormulaCalculation = false,
    bool calculateOrigRate = false)
  {
    ConcurrentDictionary<string, DiscountCode> cachedDiscountCodes = DiscountEngine.GetCachedDiscountCodes();
    Dictionary<TDiscountDetail, (Decimal, Decimal)> dictionary = new Dictionary<TDiscountDetail, (Decimal, Decimal)>();
    DiscountEngine.ApplyQuantityDiscountByBaseUOMOption discountByBaseUomOption = DiscountEngine.ApplyQuantityDiscountByBaseUOM(cache.Graph);
    foreach (TLine documentDetail in documentDetails)
    {
      AmountLineFields mapFor1 = AmountLineFields.GetMapFor<TLine>(documentDetail, cache, new DiscountEngine.ApplyQuantityDiscountByBaseUOMOption?(discountByBaseUomOption));
      DiscountLineFields mapFor2 = DiscountLineFields.GetMapFor<TLine>(documentDetail, cache);
      Decimal discountRate1 = 0M;
      Decimal discountRate2 = 0M;
      foreach (TDiscountDetail key in discountsLinkedToLine.LeftsFor(documentDetail).Where<TDiscountDetail>((Func<TDiscountDetail, bool>) (x => EnumerableExtensions.IsIn<string>(x.Type, "D", "B") && !x.SkipDiscount.GetValueOrDefault())))
      {
        Decimal num1 = 0M;
        Decimal num2 = 0M;
        Decimal num3 = 0M;
        Decimal num4 = 0M;
        if (key.IsOrigDocDiscount.GetValueOrDefault())
          num1 += key.CuryDiscountAmt.Value;
        else
          num3 += key.CuryDiscountAmt.Value;
        (Decimal, Decimal) valueTuple;
        if (dictionary.TryGetValue(key, out valueTuple))
        {
          num2 = valueTuple.Item1;
          num4 = valueTuple.Item2;
        }
        else
        {
          foreach (TLine line in discountsLinkedToLine.RightsFor(key))
          {
            DiscountLineFields mapFor3 = DiscountLineFields.GetMapFor<TLine>(line, cache);
            bool? nullable = mapFor3.AutomaticDiscountsDisabled;
            if (((!nullable.GetValueOrDefault() ? 1 : (key.Type == "B" ? 1 : 0)) | (calculateOrigRate ? 1 : 0)) != 0 && (mapFor3.DiscountID == null || cachedDiscountCodes.ContainsKey(mapFor3.DiscountID) && !cachedDiscountCodes[mapFor3.DiscountID].ExcludeFromDiscountableAmt))
            {
              AmountLineFields mapFor4 = AmountLineFields.GetMapFor<TLine>(line, cache, new DiscountEngine.ApplyQuantityDiscountByBaseUOMOption?(discountByBaseUomOption));
              nullable = key.IsOrigDocDiscount;
              if (nullable.GetValueOrDefault())
                num2 += mapFor4.CuryLineAmount.GetValueOrDefault() * mapFor4.OrigGroupDiscountRate.GetValueOrDefault();
              else
                num4 += mapFor4.CuryLineAmount.GetValueOrDefault() * mapFor4.GroupDiscountRate.GetValueOrDefault();
            }
          }
          dictionary.Add(key, (num2, num4));
        }
        if (((!mapFor2.AutomaticDiscountsDisabled.GetValueOrDefault() ? 1 : (key.Type == "B" ? 1 : 0)) | (calculateOrigRate ? 1 : 0)) != 0 && (mapFor2.DiscountID == null || cachedDiscountCodes.ContainsKey(mapFor2.DiscountID) && !cachedDiscountCodes[mapFor2.DiscountID].ExcludeFromDiscountableAmt))
        {
          if (num1 != 0M && num2 != 0M)
            discountRate1 += num1 / num2;
          if (num3 != 0M && num4 != 0M)
            discountRate2 += num3 / num4;
        }
      }
      TLine copy1 = (TLine) cache.CreateCopy((object) documentDetail);
      TLine copy2 = (TLine) cache.CreateCopy((object) currentLine);
      if (!discountCalculationOptions.HasFlag((Enum) DiscountEngine.DiscountCalculationOptions.DisableOrigAutomaticDiscounts))
        mapFor1.OrigDocumentDiscountRate = new Decimal?(1M - DiscountEngine.RoundDiscountRate(discountRate1));
      this.UpdateDocumentDiscountRate(mapFor1, new Decimal?(1M - DiscountEngine.RoundDiscountRate(discountRate2)));
      this.UpdateListOfDiscountsAppliedToLine(cache, discountsLinkedToLine, documentDetail, discountCalculationOptions);
      Decimal? documentDiscountRate1 = AmountLineFields.GetMapFor<TLine>(copy1, cache, new DiscountEngine.ApplyQuantityDiscountByBaseUOMOption?(discountByBaseUomOption)).DocumentDiscountRate;
      Decimal? documentDiscountRate2 = mapFor1.DocumentDiscountRate;
      if (documentDiscountRate1.GetValueOrDefault() == documentDiscountRate2.GetValueOrDefault() & documentDiscountRate1.HasValue == documentDiscountRate2.HasValue)
      {
        documentDiscountRate2 = AmountLineFields.GetMapFor<TLine>(copy1, cache, new DiscountEngine.ApplyQuantityDiscountByBaseUOMOption?(discountByBaseUomOption)).OrigDocumentDiscountRate;
        Decimal? documentDiscountRate3 = mapFor1.OrigDocumentDiscountRate;
        if (documentDiscountRate2.GetValueOrDefault() == documentDiscountRate3.GetValueOrDefault() & documentDiscountRate2.HasValue == documentDiscountRate3.HasValue)
          continue;
      }
      this.RecalcTaxes(cache, documentDetail, copy1, copy2);
      if (forceFormulaCalculation)
        cache.RaiseRowUpdated((object) documentDetail, (object) copy1);
    }
    List<PXEventSubscriberAttribute> attributes = TaxBaseAttribute.GetAttributes<AmountLineFields.taxCategoryID, TaxAttribute>(cache, (object) null);
    if (attributes != null && attributes.Count != 0)
      return;
    TaxBaseAttribute.InvokeRecalcTaxes(cache);
  }

  [Obsolete("This item has been deprecated and will be removed in Acumatica ERP 2023 R1.")]
  public virtual void CalculateDocumentDiscountRate(
    PXCache cache,
    PXSelectBase<TLine> lines,
    List<TLine> documentDetailsList,
    TLine currentLine,
    PXSelectBase<TDiscountDetail> discountDetails,
    Decimal lineTotal,
    Decimal discountTotal,
    string docType = null,
    string docNbr = null,
    bool alwaysRecalc = false,
    bool overrideDiscountTotal = false,
    bool skipParameters = false)
  {
    List<TLine> lineList = documentDetailsList;
    if (lineList == null)
    {
      PXCache cache1 = cache;
      PXSelectBase<TLine> documentDetails = lines;
      object[] parameters;
      if (docType == null || docNbr == null)
        parameters = (object[]) null;
      else
        parameters = new object[2]
        {
          (object) docType,
          (object) docNbr
        };
      lineList = this.GetDocumentDetails(cache1, documentDetails, parameters);
    }
    List<TLine> documentDetails1 = lineList;
    Func<string, List<TDiscountDetail>> func = (Func<string, List<TDiscountDetail>>) (discountType =>
    {
      PXCache cache2 = cache;
      PXSelectBase<TDiscountDetail> discountDetails1 = discountDetails;
      string type = discountType;
      object[] parameters;
      if (!skipParameters)
      {
        if (docType == null || docNbr == null)
          parameters = new object[1]
          {
            (object) discountType
          };
        else
          parameters = new object[3]
          {
            (object) docType,
            (object) docNbr,
            (object) discountType
          };
      }
      else
        parameters = (object[]) null;
      return this.GetDiscountDetailsByType(cache2, discountDetails1, type, parameters);
    });
    Decimal val1 = func("G").Where<TDiscountDetail>((Func<TDiscountDetail, bool>) (d => !d.SkipDiscount.GetValueOrDefault())).Sum<TDiscountDetail>((Func<TDiscountDetail, Decimal>) (d => d.CuryDiscountAmt.Value));
    Decimal val2 = func("D").Concat<TDiscountDetail>((IEnumerable<TDiscountDetail>) func("B")).Where<TDiscountDetail>((Func<TDiscountDetail, bool>) (d => !d.SkipDiscount.GetValueOrDefault())).Sum<TDiscountDetail>((Func<TDiscountDetail, Decimal>) (d => d.CuryDiscountAmt.Value));
    ConcurrentDictionary<string, DiscountCode> cachedDiscountCodes = DiscountEngine.GetCachedDiscountCodes();
    if (Math.Abs(MultiCurrencyCalculator.RoundCury(cache, (object) lines, val1) + MultiCurrencyCalculator.RoundCury(cache, (object) lines, val2) - MultiCurrencyCalculator.RoundCury(cache, (object) lines, discountTotal)) <= 0.000005M && (val1 != 0M || val2 != 0M || discountTotal != 0M) && !alwaysRecalc)
      return;
    if (overrideDiscountTotal)
    {
      val1 = 0M;
      val2 = discountTotal;
    }
    DiscountEngine.ApplyQuantityDiscountByBaseUOMOption discountByBaseUomOption = DiscountEngine.ApplyQuantityDiscountByBaseUOM(cache.Graph);
    Decimal num1 = 0M;
    foreach (TLine line in documentDetails1)
    {
      AmountLineFields mapFor1 = AmountLineFields.GetMapFor<TLine>(line, cache, new DiscountEngine.ApplyQuantityDiscountByBaseUOMOption?(discountByBaseUomOption));
      DiscountLineFields mapFor2 = DiscountLineFields.GetMapFor<TLine>(line, cache);
      if (!mapFor2.AutomaticDiscountsDisabled.GetValueOrDefault() && (mapFor2.DiscountID == null || cachedDiscountCodes.ContainsKey(mapFor2.DiscountID) && !cachedDiscountCodes[mapFor2.DiscountID].ExcludeFromDiscountableAmt))
        num1 += mapFor1.CuryLineAmount.Value;
    }
    Decimal num2 = num1 - val1;
    foreach (TLine line in documentDetails1)
    {
      AmountLineFields mapFor3 = AmountLineFields.GetMapFor<TLine>(line, cache, new DiscountEngine.ApplyQuantityDiscountByBaseUOMOption?(discountByBaseUomOption));
      DiscountLineFields mapFor4 = DiscountLineFields.GetMapFor<TLine>(line, cache);
      TLine copy1 = (TLine) cache.CreateCopy((object) line);
      TLine copy2 = (TLine) cache.CreateCopy((object) currentLine);
      Decimal? nullable1;
      Decimal? nullable2;
      if (num2 != 0M && val2 != 0M)
      {
        nullable1 = mapFor3.CuryLineAmount;
        Decimal num3 = 0M;
        if (!(nullable1.GetValueOrDefault() == num3 & nullable1.HasValue) && !mapFor4.AutomaticDiscountsDisabled.GetValueOrDefault() && (mapFor4.DiscountID == null || cachedDiscountCodes.ContainsKey(mapFor4.DiscountID) && !cachedDiscountCodes[mapFor4.DiscountID].ExcludeFromDiscountableAmt))
        {
          ref Decimal? local = ref nullable2;
          nullable1 = mapFor3.CuryLineAmount;
          Decimal num4 = nullable1.Value * val2 / num2;
          nullable1 = mapFor3.CuryLineAmount;
          Decimal num5 = nullable1.Value;
          Decimal num6 = 1M - num4 / num5;
          local = new Decimal?(num6);
          goto label_21;
        }
      }
      nullable2 = new Decimal?((Decimal) 1);
label_21:
      mapFor3.DocumentDiscountRate = new Decimal?(DiscountEngine.RoundDiscountRate(nullable2 ?? 1M));
      if (val1 == 0M)
        mapFor3.GroupDiscountRate = new Decimal?((Decimal) 1);
      TwoWayLookup<TDiscountDetail, TLine> andDocumentLines = this.GetListOfLinksBetweenDiscountsAndDocumentLines(cache, documentDetails1, this.GetDiscountDetailsByType(cache, discountDetails, (string) null));
      this.UpdateListOfDiscountsAppliedToLine(cache, andDocumentLines, line);
      nullable1 = AmountLineFields.GetMapFor<TLine>(copy1, cache, new DiscountEngine.ApplyQuantityDiscountByBaseUOMOption?(discountByBaseUomOption)).DocumentDiscountRate;
      Decimal? documentDiscountRate = mapFor3.DocumentDiscountRate;
      if (nullable1.GetValueOrDefault() == documentDiscountRate.GetValueOrDefault() & nullable1.HasValue == documentDiscountRate.HasValue)
      {
        documentDiscountRate = AmountLineFields.GetMapFor<TLine>(copy1, cache, new DiscountEngine.ApplyQuantityDiscountByBaseUOMOption?(discountByBaseUomOption)).OrigDocumentDiscountRate;
        nullable1 = mapFor3.OrigDocumentDiscountRate;
        if (documentDiscountRate.GetValueOrDefault() == nullable1.GetValueOrDefault() & documentDiscountRate.HasValue == nullable1.HasValue)
          continue;
      }
      this.RecalcTaxes(cache, line, copy1, copy2);
    }
  }

  protected virtual bool UpdateDocumentDiscountRate(AmountLineFields aLine, Decimal? discountRate)
  {
    Decimal? documentDiscountRate1 = aLine.DocumentDiscountRate;
    Decimal? documentDiscountRate2 = aLine.DocumentDiscountRate;
    Decimal? nullable = discountRate;
    if (documentDiscountRate2.GetValueOrDefault() == nullable.GetValueOrDefault() & documentDiscountRate2.HasValue == nullable.HasValue)
      return false;
    aLine.DocumentDiscountRate = discountRate;
    aLine.RaiseFieldUpdated<AmountLineFields.documentDiscountRate>((object) documentDiscountRate1);
    return true;
  }

  public virtual void UpdateListOfDiscountsAppliedToLines(
    PXCache cache,
    TwoWayLookup<TDiscountDetail, TLine> discountCodesWithApplicableLines,
    DiscountEngine.DiscountCalculationOptions discountCalculationOptions)
  {
    foreach (TLine rightValue in discountCodesWithApplicableLines.RightValues)
      this.UpdateListOfDiscountsAppliedToLine(cache, discountCodesWithApplicableLines, rightValue, discountCalculationOptions);
  }

  protected virtual void UpdateListOfDiscountsAppliedToLine(
    PXCache cache,
    TwoWayLookup<TDiscountDetail, TLine> discountCodesWithApplicableLines,
    TLine line)
  {
    this.UpdateListOfDiscountsAppliedToLine(cache, discountCodesWithApplicableLines, line, DiscountEngine.DiscountCalculationOptions.CalculateAll);
  }

  protected virtual void UpdateListOfDiscountsAppliedToLine(
    PXCache cache,
    TwoWayLookup<TDiscountDetail, TLine> discountCodesWithApplicableLines,
    TLine line,
    DiscountEngine.DiscountCalculationOptions discountCalculationOptions)
  {
    DiscountLineFields mapFor = DiscountLineFields.GetMapFor<TLine>(line, cache);
    ushort[] second1 = new ushort[0];
    if (!mapFor.AutomaticDiscountsDisabled.GetValueOrDefault() || mapFor.AutomaticDiscountsDisabled.GetValueOrDefault() && discountCalculationOptions.HasFlag((Enum) DiscountEngine.DiscountCalculationOptions.DisableOrigAutomaticDiscounts))
    {
      second1 = discountCodesWithApplicableLines.LeftsFor(line).Where<TDiscountDetail>((Func<TDiscountDetail, bool>) (dDetail =>
      {
        Decimal? nullable;
        if (dDetail.CuryDiscountableAmt.HasValue)
        {
          nullable = dDetail.CuryDiscountableAmt;
          Decimal num = 0M;
          if (nullable.GetValueOrDefault() > num & nullable.HasValue)
            return true;
        }
        nullable = dDetail.DiscountableQty;
        if (!nullable.HasValue)
          return false;
        nullable = dDetail.DiscountableQty;
        Decimal num1 = 0M;
        return nullable.GetValueOrDefault() > num1 & nullable.HasValue;
      })).Select<TDiscountDetail, ushort?>((Func<TDiscountDetail, ushort?>) (dDetail => dDetail.LineNbr)).Where<ushort?>((Func<ushort?, bool>) (x => x.HasValue)).Select<ushort?, ushort>((Func<ushort?, ushort>) (x => x.Value)).ToArray<ushort>();
      foreach (TDiscountDetail discountDetail in discountCodesWithApplicableLines.LeftValues.Where<TDiscountDetail>((Func<TDiscountDetail, bool>) (x =>
      {
        if (!x.LineNbr.HasValue)
          return false;
        return !x.IsOrigDocDiscount.GetValueOrDefault() && x.Type == "D" || x.Type == "B";
      })))
      {
        ushort[] source = second1;
        ushort? lineNbr = discountDetail.LineNbr;
        int valueOrDefault = (int) lineNbr.GetValueOrDefault();
        if (!((IEnumerable<ushort>) source).Contains<ushort>((ushort) valueOrDefault))
        {
          ushort[] first = second1;
          ushort[] second2 = new ushort[1];
          lineNbr = discountDetail.LineNbr;
          second2[0] = lineNbr.GetValueOrDefault();
          second1 = ((IEnumerable<ushort>) first).Concat<ushort>((IEnumerable<ushort>) second2).ToArray<ushort>();
        }
      }
    }
    if (mapFor.DiscountsAppliedToLine != null && ((IEnumerable<ushort>) mapFor.DiscountsAppliedToLine).SequenceEqual<ushort>((IEnumerable<ushort>) second1))
      return;
    mapFor.DiscountsAppliedToLine = second1;
    GraphHelper.MarkUpdated(cache, (object) line, true);
  }

  private void RecalcTaxes(PXCache cache, TLine line, TLine oldLine, TLine oldCurrentLine)
  {
    if (!this.CompareRows<TLine>(oldLine, oldCurrentLine))
      TaxBaseAttribute.Calculate<AmountLineFields.taxCategoryID>(cache, new PXRowUpdatedEventArgs((object) line, (object) oldLine, false));
    GraphHelper.MarkUpdated(cache, (object) line, true);
  }

  /// <summary>Returns new DiscountDetail line</summary>
  protected virtual TDiscountDetail CreateDiscountDetailsLine(
    PXCache cache,
    DiscountDetailLine discount,
    DiscountLineFields discountedLine,
    Decimal curyDiscountedLineAmount,
    Decimal discountedLineQty,
    DateTime date,
    string type)
  {
    TDiscountDetail discountDetail = new TDiscountDetail();
    discountDetail.DiscountID = discount.DiscountID;
    discountDetail.DiscountSequenceID = discount.DiscountSequenceID;
    discountDetail.Type = type;
    discountDetail.CuryDiscountableAmt = new Decimal?(curyDiscountedLineAmount);
    discountDetail.DiscountableQty = new Decimal?(discountedLineQty);
    TDiscountDetail row = discountDetail;
    Decimal discount1 = this.CalculateDiscount(cache, discount, discountedLine, curyDiscountedLineAmount, discountedLineQty, date, type);
    row.CuryDiscountAmt = new Decimal?(MultiCurrencyCalculator.RoundCury(cache, (object) row, discount1));
    if (discount.DiscountedFor == "P")
      row.DiscountPct = discount.Discount;
    else if (!row.CuryDiscountableAmt.IsNullOrZero())
    {
      // ISSUE: variable of a boxed type
      __Boxed<TDiscountDetail> local = (object) row;
      Decimal? curyDiscountAmt = row.CuryDiscountAmt;
      Decimal? curyDiscountableAmt = row.CuryDiscountableAmt;
      Decimal? nullable1 = curyDiscountAmt.HasValue & curyDiscountableAmt.HasValue ? new Decimal?(curyDiscountAmt.GetValueOrDefault() / curyDiscountableAmt.GetValueOrDefault()) : new Decimal?();
      Decimal num = (Decimal) 100;
      Decimal? nullable2 = nullable1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() * num) : new Decimal?();
      local.DiscountPct = nullable2;
    }
    row.FreeItemID = discount.freeItemID;
    row.FreeItemQty = new Decimal?(this.CalculateFreeItemQuantity(cache, discount, discountedLine, curyDiscountedLineAmount, discountedLineQty, date, type));
    row.ExtDiscCode = discount.ExtDiscCode;
    row.Description = discount.Description;
    return row;
  }

  /// <summary>Creates new list of Discount Details lines.</summary>
  protected virtual bool CreateNewListOfDiscountDetails(
    PXCache cache,
    DiscountLineFields discountedLine,
    List<DiscountDetailLine> discounts,
    DateTime date,
    Dictionary<DiscountSequenceKey, TDiscountDetail> newDiscountDetails,
    Decimal discountedLineAmount,
    Decimal discountedLineQty)
  {
    bool ofDiscountDetails = false;
    ConcurrentDictionary<string, DiscountCode> cachedDiscountCodes = DiscountEngine.GetCachedDiscountCodes();
    foreach (DiscountDetailLine discount in discounts.Where<DiscountDetailLine>((Func<DiscountDetailLine, bool>) (d => d.DiscountID != null)))
    {
      if (cachedDiscountCodes[discount.DiscountID].SkipDocumentDiscounts)
        ofDiscountDetails = true;
      TDiscountDetail discountDetailsLine = this.CreateDiscountDetailsLine(cache, discount, discountedLine, discountedLineAmount, discountedLineQty, date, "G");
      DiscountSequenceKey key = new DiscountSequenceKey(discountDetailsLine.DiscountID, discountDetailsLine.DiscountSequenceID);
      if (!newDiscountDetails.ContainsKey(key))
      {
        newDiscountDetails.Add(key, discountDetailsLine);
      }
      else
      {
        newDiscountDetails[key].CuryDiscountableAmt = new Decimal?();
        newDiscountDetails[key].DiscountableQty = new Decimal?();
        newDiscountDetails[key].DiscountPct = new Decimal?();
        TDiscountDetail newDiscountDetail = newDiscountDetails[key];
        ref TDiscountDetail local1 = ref newDiscountDetail;
        // ISSUE: variable of a boxed type
        __Boxed<TDiscountDetail> local2 = (object) local1;
        Decimal? curyDiscountAmt1 = local1.CuryDiscountAmt;
        Decimal? curyDiscountAmt2 = discountDetailsLine.CuryDiscountAmt;
        Decimal? nullable = curyDiscountAmt1.HasValue & curyDiscountAmt2.HasValue ? new Decimal?(curyDiscountAmt1.GetValueOrDefault() + curyDiscountAmt2.GetValueOrDefault()) : new Decimal?();
        local2.CuryDiscountAmt = nullable;
      }
    }
    return ofDiscountDetails;
  }

  protected virtual void SetDocumentDiscount(
    PXCache cache,
    PXSelectBase<TLine> lines,
    PXSelectBase<TDiscountDetail> discountDetails,
    TLine currentLine,
    int? branchID,
    int? locationID,
    DateTime date,
    DiscountEngine.DiscountCalculationOptions discountCalculationOptions,
    RecalcDiscountsParamFilter recalcFilter = null)
  {
    if (!this.IsDiscountCalculationNeeded(cache, currentLine, "D", discountDetails))
    {
      if (!this.GetDiscountDetailsByType(cache, discountDetails, "B").Any<TDiscountDetail>())
        return;
      discountCalculationOptions |= DiscountEngine.DiscountCalculationOptions.CalculateExternalDocumentDiscountsOnly;
    }
    List<TLine> documentDetails = this.GetDocumentDetails(cache, lines);
    if (documentDetails.Count != 0)
      this.SetDocumentDiscount(cache, documentDetails, this.GetDiscountEntitiesDiscounts(cache, documentDetails.First<TLine>(), locationID, false, branchID), discountDetails, date, discountCalculationOptions, recalcFilter);
    else
      this.RemoveUnapplicableDiscountDetails(cache, discountDetails, (List<DiscountSequenceKey>) null, "D", recalcFilter: recalcFilter);
    this.CalculateDocumentDiscountRate(cache, documentDetails, currentLine, discountDetails, discountCalculationOptions, discountCalculationOptions.HasFlag((Enum) DiscountEngine.DiscountCalculationOptions.ForceFormulaRecalculation));
  }

  protected virtual void SetDocumentDiscount(
    PXCache cache,
    List<TLine> documentDetails,
    HashSet<KeyValuePair<object, string>> entities,
    PXSelectBase<TDiscountDetail> discountDetails,
    DateTime date,
    DiscountEngine.DiscountCalculationOptions discountCalculationOptions,
    RecalcDiscountsParamFilter recalcFilter = null)
  {
    if (documentDetails.Count == 0)
      return;
    TLine line = documentDetails.First<TLine>();
    DiscountEngine.GetDiscountTypes();
    ConcurrentDictionary<string, DiscountCode> cachedDiscountTypes = DiscountEngine.GetCachedDiscountCodes();
    List<TDiscountDetail> discountDetailsByType = this.GetDiscountDetailsByType(cache, discountDetails, "G");
    bool flag1 = false;
    if (discountDetailsByType.Any<TDiscountDetail>((Func<TDiscountDetail, bool>) (d => !d.SkipDiscount.GetValueOrDefault() && !d.IsOrigDocDiscount.GetValueOrDefault() && cachedDiscountTypes[d.DiscountID].SkipDocumentDiscounts)))
    {
      this.RemoveUnapplicableDiscountDetails(cache, discountDetails, new List<DiscountSequenceKey>(), "D", recalcFilter: recalcFilter);
      flag1 = true;
    }
    Decimal curyTotalDiscountAmt;
    this.GetDiscountAmountByType(((PXSelectBase) discountDetails).Cache, discountDetailsByType, "G", out Decimal _, out curyTotalDiscountAmt);
    Decimal curyTotalLineAmt;
    this.SumAmounts(cache, documentDetails, out Decimal _, out curyTotalLineAmt);
    Decimal num1 = curyTotalLineAmt - curyTotalDiscountAmt;
    LineEntitiesFields mapFor = LineEntitiesFields.GetMapFor<TLine>(line, cache);
    Decimal discountLimit = this.GetDiscountLimit(cache, mapFor.CustomerID, mapFor.VendorID);
    List<DiscountDetailLine> discountDetailLineList = new List<DiscountDetailLine>();
    if (curyTotalLineAmt / 100M * discountLimit > curyTotalDiscountAmt || discountLimit == 0M)
    {
      if (curyTotalLineAmt == 0M)
        return;
      List<TDiscountDetail> newDiscountDetails = new List<TDiscountDetail>();
      Decimal num2 = 0M;
      if (!flag1 && !discountCalculationOptions.HasFlag((Enum) DiscountEngine.DiscountCalculationOptions.CalculateExternalDocumentDiscountsOnly))
      {
        discountDetailLineList.Add(this.SelectBestDiscount(cache, DiscountLineFields.GetMapFor<TLine>(line, cache), entities, "D", num1, 0M, date));
        bool flag2 = recalcFilter != null && recalcFilter.UseRecalcFilter.GetValueOrDefault() && recalcFilter.OverrideManualDocGroupDiscounts.GetValueOrDefault();
        if (!flag2)
        {
          foreach (TDiscountDetail collectManualDiscount in this.CollectManualDiscounts(cache, discountDetails, "D"))
          {
            if (!collectManualDiscount.IsOrigDocDiscount.GetValueOrDefault())
            {
              HashSet<DiscountSequenceKey> source = this.SelectApplicableEntityDiscounts(cache.Graph, entities, "D", false);
              DiscountDetailLine newManualDiscount = this.SelectApplicableDiscount(cache, DiscountLineFields.GetMapFor<TLine>(line, cache), new DiscountSequenceKey(collectManualDiscount.DiscountID, collectManualDiscount.DiscountSequenceID), num1, 0M, "D", date);
              Func<DiscountSequenceKey, bool> predicate = (Func<DiscountSequenceKey, bool>) (x => x.DiscountID == newManualDiscount.DiscountID && x.DiscountSequenceID == newManualDiscount.DiscountSequenceID);
              if (source.Count<DiscountSequenceKey>(predicate) != 0 && newManualDiscount.DiscountID == collectManualDiscount.DiscountID && newManualDiscount.DiscountSequenceID == collectManualDiscount.DiscountSequenceID)
              {
                newManualDiscount.Discount = collectManualDiscount.CuryDiscountAmt;
                newManualDiscount.DiscountedFor = "A";
                discountDetailLineList.Add(newManualDiscount);
              }
            }
          }
        }
        List<DiscountSequenceKey> list1 = discountDetailLineList.Where<DiscountDetailLine>((Func<DiscountDetailLine, bool>) (ds => ds.DiscountID != null && ds.DiscountSequenceID != null)).Select<DiscountDetailLine, DiscountSequenceKey>((Func<DiscountDetailLine, DiscountSequenceKey>) (ds => new DiscountSequenceKey(ds.DiscountID, ds.DiscountSequenceID))).ToList<DiscountSequenceKey>();
        this.RemoveUnapplicableDiscountDetails(cache, discountDetails, list1, "D", recalcFilter: recalcFilter);
        if (discountCalculationOptions.HasFlag((Enum) DiscountEngine.DiscountCalculationOptions.DisableAllAutomaticDiscounts) && !flag2)
        {
          List<TDiscountDetail> list2 = this.GetDiscountDetailsByType(cache, discountDetails, "D").Where<TDiscountDetail>((Func<TDiscountDetail, bool>) (x => !x.IsOrigDocDiscount.GetValueOrDefault())).ToList<TDiscountDetail>();
          if (list2.Count<TDiscountDetail>() == 0 && !discountCalculationOptions.HasFlag((Enum) DiscountEngine.DiscountCalculationOptions.DisableOrigAutomaticDiscounts))
            discountDetailLineList.Clear();
          if (list2.Count > 0 && discountDetailLineList.Count > 0)
            discountDetailLineList = discountDetailLineList.Join((IEnumerable<TDiscountDetail>) list2, n1 => new
            {
              DiscountID = n1.DiscountID,
              DiscountSequenceID = n1.DiscountSequenceID
            }, n2 => new
            {
              DiscountID = n2.DiscountID,
              DiscountSequenceID = n2.DiscountSequenceID
            }, (Func<DiscountDetailLine, TDiscountDetail, DiscountDetailLine>) ((n1, n2) => n1)).ToList<DiscountDetailLine>();
        }
        foreach (DiscountDetailLine discount in discountDetailLineList)
        {
          if (discount.DiscountID != null)
          {
            TDiscountDetail discountDetailsLine = this.CreateDiscountDetailsLine(cache, discount, DiscountLineFields.GetMapFor<TLine>(line, cache), num1, 0M, date, "D");
            num2 += discountDetailsLine.CuryDiscountAmt.GetValueOrDefault();
            if (discountCalculationOptions.HasFlag((Enum) DiscountEngine.DiscountCalculationOptions.DisableAllAutomaticDiscounts) & flag2)
              discountDetailsLine.IsManual = new bool?(true);
            newDiscountDetails.Add(discountDetailsLine);
          }
        }
      }
      this.UpdateExternalDiscountsDiscountableAmounts(cache, documentDetails, discountDetails, curyTotalDiscountAmt, newDiscountDetails, discountCalculationOptions);
      foreach (TDiscountDetail newTrace in newDiscountDetails)
      {
        this.UpdateInsertDiscountTrace(cache, discountDetails, newTrace);
        if (curyTotalLineAmt / 100M * discountLimit < num2 + curyTotalDiscountAmt)
          this.SetDiscountLimitException(cache, discountDetails, "D", PXMessages.LocalizeFormatNoPrefix("The total amount of group and document discounts cannot exceed the limit specified for the customer class ({0:F2}%) on the Customer Classes (AR201000) form.", new object[1]
          {
            (object) discountLimit
          }), discountCalculationOptions);
      }
    }
    else if (curyTotalLineAmt != 0M && curyTotalDiscountAmt != 0M)
      this.SetDiscountLimitException(cache, discountDetails, "G", "Total group discount exceeds limit configured for this customer class. Document Discount was not calculated.", discountCalculationOptions);
    else
      this.RemoveUnapplicableDiscountDetails(cache, discountDetails, (List<DiscountSequenceKey>) null, "D", recalcFilter: recalcFilter);
  }

  public virtual void UpdateExternalDiscounts(
    PXCache cache,
    PXSelectBase<TLine> lines,
    TLine line,
    PXSelectBase<TDiscountDetail> discountDetails,
    DiscountEngine.DiscountCalculationOptions discountCalculationOptions)
  {
    List<TDiscountDetail> newDiscountDetails = new List<TDiscountDetail>();
    List<TLine> documentDetails = this.GetDocumentDetails(cache, lines);
    this.UpdateExternalDiscountsDiscountableAmounts(cache, documentDetails, discountDetails, 0M, newDiscountDetails, discountCalculationOptions);
    foreach (TDiscountDetail newTrace in newDiscountDetails)
      this.UpdateInsertDiscountTrace(cache, discountDetails, newTrace);
    this.CalculateDocumentDiscountRate(cache, lines, line, discountDetails, discountCalculationOptions);
  }

  public virtual void UpdateExternalDiscountsDiscountableAmounts(
    PXCache cache,
    List<TLine> documentDetails,
    PXSelectBase<TDiscountDetail> discountDetails,
    Decimal curyTotalGroupDiscountAmount,
    List<TDiscountDetail> newDiscountDetails,
    DiscountEngine.DiscountCalculationOptions discountCalculationOptions)
  {
    List<TDiscountDetail> discountDetailsByType = this.GetDiscountDetailsByType(cache, discountDetails, "B");
    if (!EnumerableExtensions.HasAtLeast<TDiscountDetail>((IEnumerable<TDiscountDetail>) discountDetailsByType, 1))
      return;
    Decimal curyTotalLineAmt;
    this.SumAmounts(cache, documentDetails, true, out Decimal _, out curyTotalLineAmt);
    Decimal num1 = curyTotalLineAmt - curyTotalGroupDiscountAmount;
    foreach (TDiscountDetail discountDetail in discountDetailsByType)
    {
      Decimal num2 = num1;
      if (!discountDetail.IsOrigDocDiscount.GetValueOrDefault())
      {
        discountDetail.CuryDiscountableAmt = new Decimal?(num2);
        if (num2 != 0M)
        {
          // ISSUE: variable of a boxed type
          __Boxed<TDiscountDetail> local = (object) discountDetail;
          Decimal? nullable1 = discountDetail.CuryDiscountAmt;
          Decimal num3 = (Decimal) 100;
          Decimal? nullable2 = nullable1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() * num3) : new Decimal?();
          Decimal num4 = num2;
          Decimal? nullable3;
          if (!nullable2.HasValue)
          {
            nullable1 = new Decimal?();
            nullable3 = nullable1;
          }
          else
            nullable3 = new Decimal?(nullable2.GetValueOrDefault() / num4);
          local.DiscountPct = nullable3;
        }
        else
          discountDetail.DiscountPct = new Decimal?(0M);
        newDiscountDetails.Add(discountDetail);
      }
    }
  }

  private void SetDiscountLimitException(
    PXCache cache,
    PXSelectBase<TDiscountDetail> discountDetails,
    string discountType,
    string message,
    DiscountEngine.DiscountCalculationOptions discountCalculationOptions,
    TDiscountDetail currentDiscountLine = null)
  {
    List<TDiscountDetail> discountDetailsByType = this.GetDiscountDetailsByType(cache, discountDetails, discountType);
    if (discountDetailsByType.Count != 0)
    {
      ((PXSelectBase) discountDetails).Cache.RaiseExceptionHandling("DiscountID", (object) discountDetailsByType[0], (object) null, (Exception) new PXSetPropertyException(message, (PXErrorLevel) 2));
    }
    else
    {
      if ((object) currentDiscountLine == null)
        return;
      ((PXSelectBase) discountDetails).Cache.RaiseExceptionHandling("DiscountID", (object) currentDiscountLine, (object) null, (Exception) new PXSetPropertyException(message, (PXErrorLevel) 2));
    }
  }

  protected virtual void SetManualGroupDocDiscount(
    PXCache cache,
    PXSelectBase<TLine> lines,
    TLine currentLine,
    PXSelectBase<TDiscountDetail> discountDetails,
    TDiscountDetail currentDiscountDetailLine,
    string manualDiscountID,
    string manualDiscountSequenceID,
    int? branchID,
    int? locationID,
    DateTime date,
    DiscountEngine.DiscountCalculationOptions discountCalculationOptions)
  {
    List<TLine> documentDetails = this.GetDocumentDetails(cache, lines);
    ConcurrentDictionary<string, DiscountCode> cachedDiscountTypes = DiscountEngine.GetCachedDiscountCodes();
    if (!cachedDiscountTypes.ContainsKey(manualDiscountID) || documentDetails.Count == 0)
      return;
    bool flag1 = false;
    bool flag2 = false;
    List<TDiscountDetail> discountDetailsByType = this.GetDiscountDetailsByType(cache, discountDetails, "G");
    Decimal totalDiscountAmount;
    Decimal curyTotalDiscountAmt;
    this.GetDiscountAmountByType(((PXSelectBase) discountDetails).Cache, discountDetailsByType, "G", out totalDiscountAmount, out curyTotalDiscountAmt);
    Decimal totalLineAmt;
    Decimal curyTotalLineAmt;
    this.SumAmounts(cache, documentDetails, out totalLineAmt, out curyTotalLineAmt);
    LineEntitiesFields mapFor1 = LineEntitiesFields.GetMapFor<TLine>(documentDetails.First<TLine>(), cache);
    Decimal discountLimit = this.GetDiscountLimit(cache, mapFor1.CustomerID, mapFor1.VendorID);
    if (cachedDiscountTypes[manualDiscountID].Type == "G")
    {
      TwoWayLookup<DiscountSequenceKey, TLine> discountCodesWithLines = new TwoWayLookup<DiscountSequenceKey, TLine>((IEqualityComparer<DiscountSequenceKey>) null, (IEqualityComparer<TLine>) null, (Func<DiscountSequenceKey, TLine, bool>) null);
      foreach (TLine line in documentDetails)
      {
        DiscountLineFields mapFor2 = DiscountLineFields.GetMapFor<TLine>(line, cache);
        if (!mapFor2.AutomaticDiscountsDisabled.GetValueOrDefault())
        {
          HashSet<DiscountSequenceKey> hashSet = this.SelectApplicableEntityDiscounts(cache.Graph, this.GetDiscountEntitiesDiscounts(cache, line, locationID, true), cachedDiscountTypes[manualDiscountID].Type, false).Where<DiscountSequenceKey>((Func<DiscountSequenceKey, bool>) (sk => manualDiscountID == sk.DiscountID && EnumerableExtensions.IsIn<string>(manualDiscountSequenceID, (string) null, sk.DiscountSequenceID))).ToHashSet<DiscountSequenceKey>();
          if (!flag1)
            flag1 = hashSet.Any<DiscountSequenceKey>();
          if (hashSet.Any<DiscountSequenceKey>())
          {
            bool flag3 = false;
            if (mapFor2.DiscountID != null)
              flag3 = cachedDiscountTypes[mapFor2.DiscountID].ExcludeFromDiscountableAmt || mapFor2.AutomaticDiscountsDisabled.GetValueOrDefault();
            if (!flag3)
              this.CombineDiscountsAndDocumentLines(cache, line, hashSet, ref discountCodesWithLines);
          }
        }
      }
      foreach (DiscountSequenceKey leftValue in discountCodesWithLines.LeftValues)
      {
        DiscountLineFields mapFor3 = DiscountLineFields.GetMapFor<TLine>(documentDetails[0], cache);
        PXCache cache1 = cache;
        DiscountLineFields dLine = mapFor3;
        HashSet<DiscountSequenceKey> discountSequences = new HashSet<DiscountSequenceKey>();
        discountSequences.Add(leftValue);
        Decimal? nullable = leftValue.CuryDiscountableAmount;
        Decimal curyDiscountableAmount = nullable.Value;
        nullable = leftValue.DiscountableQuantity;
        Decimal discountableQuantity = nullable.Value;
        DateTime date1 = date;
        List<DiscountDetailLine> source = this.SelectApplicableDiscounts(cache1, dLine, discountSequences, curyDiscountableAmount, discountableQuantity, "G", date1);
        if (source.Count != 0)
        {
          PXCache cache2 = cache;
          DiscountDetailLine discount = source.First<DiscountDetailLine>();
          DiscountLineFields discountedLine = mapFor3;
          nullable = leftValue.CuryDiscountableAmount;
          Decimal curyDiscountedLineAmount = nullable.Value;
          nullable = leftValue.DiscountableQuantity;
          Decimal discountedLineQty = nullable.Value;
          DateTime date2 = date;
          TDiscountDetail discountDetailsLine = this.CreateDiscountDetailsLine(cache2, discount, discountedLine, curyDiscountedLineAmount, discountedLineQty, date2, "G");
          discountDetailsLine.CuryDiscountableAmt = leftValue.CuryDiscountableAmount;
          discountDetailsLine.DiscountableQty = leftValue.DiscountableQuantity;
          currentDiscountDetailLine.DiscountSequenceID = discountDetailsLine.DiscountSequenceID;
          currentDiscountDetailLine.Type = discountDetailsLine.Type;
          currentDiscountDetailLine.CuryDiscountableAmt = discountDetailsLine.CuryDiscountableAmt;
          currentDiscountDetailLine.DiscountableQty = discountDetailsLine.DiscountableQty;
          currentDiscountDetailLine.FreeItemID = discountDetailsLine.FreeItemID;
          currentDiscountDetailLine.FreeItemQty = discountDetailsLine.FreeItemQty;
          if (cache.Graph.IsImport && !currentDiscountDetailLine.FreeItemID.HasValue && (!currentDiscountDetailLine.CuryDiscountAmt.IsNullOrZero() || !currentDiscountDetailLine.DiscountPct.IsNullOrZero()))
          {
            this.CalculateManualDiscountPercentOrAmount(cache, currentDiscountDetailLine, default (TDiscountDetail));
          }
          else
          {
            currentDiscountDetailLine.DiscountPct = discountDetailsLine.DiscountPct;
            currentDiscountDetailLine.CuryDiscountAmt = discountDetailsLine.CuryDiscountAmt;
          }
          flag2 = true;
          Decimal num1 = curyTotalLineAmt / 100M * discountLimit;
          Decimal? curyDiscountAmt = discountDetailsLine.CuryDiscountAmt;
          Decimal num2 = curyTotalDiscountAmt;
          nullable = curyDiscountAmt.HasValue ? new Decimal?(curyDiscountAmt.GetValueOrDefault() + num2) : new Decimal?();
          Decimal valueOrDefault = nullable.GetValueOrDefault();
          if (num1 < valueOrDefault & nullable.HasValue)
            this.SetDiscountLimitException(cache, discountDetails, "G", PXMessages.LocalizeFormatNoPrefix("Total group discount exceeds limit configured for this customer class.", new object[1]
            {
              (object) discountLimit
            }), discountCalculationOptions, currentDiscountDetailLine);
        }
        if (cachedDiscountTypes[manualDiscountID].SkipDocumentDiscounts)
          this.RemoveUnapplicableDiscountDetails(cache, discountDetails, (List<DiscountSequenceKey>) null, "D");
      }
      TwoWayLookup<TDiscountDetail, TLine> discountCodesWithApplicableLines = this.MatchLinesWithDiscounts(cache, discountCodesWithLines, documentDetails, discountDetails, false, false);
      this.CalculateGroupDiscountRate(cache, documentDetails, currentLine, discountCodesWithApplicableLines, true, false);
    }
    if (cachedDiscountTypes[manualDiscountID].Type == "D")
    {
      HashSet<DiscountSequenceKey> hashSet = this.SelectApplicableEntityDiscounts(cache.Graph, this.GetDiscountEntitiesDiscounts(cache, documentDetails.First<TLine>(), locationID, false, branchID), cachedDiscountTypes[manualDiscountID].Type, false).Where<DiscountSequenceKey>((Func<DiscountSequenceKey, bool>) (sk => manualDiscountID == sk.DiscountID && EnumerableExtensions.IsIn<string>(manualDiscountSequenceID, (string) null, sk.DiscountSequenceID))).ToHashSet<DiscountSequenceKey>();
      flag1 = hashSet.Any<DiscountSequenceKey>();
      flag2 = false;
      if (flag1)
      {
        if (discountDetailsByType.Any<TDiscountDetail>((Func<TDiscountDetail, bool>) (detail => !detail.SkipDiscount.GetValueOrDefault() && detail.DiscountID != null && cachedDiscountTypes[detail.DiscountID].SkipDocumentDiscounts)))
        {
          ((PXSelectBase) discountDetails).Cache.RaiseExceptionHandling("DiscountID", (object) currentDiscountDetailLine, (object) manualDiscountID, (Exception) new PXSetPropertyException("Skip Document Discounts option is set for one or more group discounts. Document discount can not be added.", (PXErrorLevel) 4));
          return;
        }
        if (curyTotalLineAmt / 100M * discountLimit > curyTotalDiscountAmt || discountLimit == 0M)
        {
          if (curyTotalLineAmt != 0M)
          {
            DiscountDetailLine discount = this.SelectApplicableDiscount(cache, DiscountLineFields.GetMapFor<TLine>(documentDetails.First<TLine>(), cache), hashSet, totalLineAmt - totalDiscountAmount, 0M, "D", date);
            if (discount.DiscountID != null)
            {
              TDiscountDetail discountDetailsLine = this.CreateDiscountDetailsLine(cache, discount, DiscountLineFields.GetMapFor<TLine>(documentDetails.First<TLine>(), cache), curyTotalLineAmt - curyTotalDiscountAmt, 0M, date, "D");
              discountDetailsLine.CuryDiscountableAmt = new Decimal?(curyTotalLineAmt - curyTotalDiscountAmt);
              currentDiscountDetailLine.DiscountSequenceID = discountDetailsLine.DiscountSequenceID;
              currentDiscountDetailLine.Type = discountDetailsLine.Type;
              currentDiscountDetailLine.CuryDiscountableAmt = discountDetailsLine.CuryDiscountableAmt;
              currentDiscountDetailLine.DiscountableQty = new Decimal?(0M);
              currentDiscountDetailLine.FreeItemID = new int?();
              currentDiscountDetailLine.FreeItemQty = new Decimal?(0M);
              if (cache.Graph.IsImport && (!currentDiscountDetailLine.CuryDiscountAmt.IsNullOrZero() || !currentDiscountDetailLine.DiscountPct.IsNullOrZero()))
              {
                this.CalculateManualDiscountPercentOrAmount(cache, currentDiscountDetailLine, default (TDiscountDetail));
              }
              else
              {
                currentDiscountDetailLine.DiscountPct = discountDetailsLine.DiscountPct;
                currentDiscountDetailLine.CuryDiscountAmt = discountDetailsLine.CuryDiscountAmt;
              }
              flag2 = true;
              Decimal num3 = curyTotalLineAmt / 100M * discountLimit;
              Decimal? curyDiscountAmt = discountDetailsLine.CuryDiscountAmt;
              Decimal num4 = curyTotalDiscountAmt;
              Decimal? nullable = curyDiscountAmt.HasValue ? new Decimal?(curyDiscountAmt.GetValueOrDefault() + num4) : new Decimal?();
              Decimal valueOrDefault = nullable.GetValueOrDefault();
              if (num3 < valueOrDefault & nullable.HasValue)
                this.SetDiscountLimitException(cache, discountDetails, "D", PXMessages.LocalizeFormatNoPrefix("The total amount of group and document discounts cannot exceed the limit specified for the customer class ({0:F2}%) on the Customer Classes (AR201000) form.", new object[1]
                {
                  (object) discountLimit
                }), discountCalculationOptions, currentDiscountDetailLine);
            }
            else
              this.RemoveUnapplicableDiscountDetails(cache, discountDetails, (List<DiscountSequenceKey>) null, "D");
          }
        }
        else
          this.SetDiscountLimitException(cache, discountDetails, "G", "Total group discount exceeds limit configured for this customer class. Document Discount was not calculated.", discountCalculationOptions);
        this.CalculateDocumentDiscountRate(cache, documentDetails, currentLine, discountDetails, discountCalculationOptions);
      }
    }
    if (flag1 && flag2)
      return;
    if (manualDiscountID != null && manualDiscountSequenceID == null)
      ((PXSelectBase) discountDetails).Cache.RaiseExceptionHandling("DiscountID", (object) currentDiscountDetailLine, (object) manualDiscountID, (Exception) new PXSetPropertyException("No applicable discount sequence found.", (PXErrorLevel) 4));
    if (manualDiscountID == null || manualDiscountSequenceID == null)
      return;
    currentDiscountDetailLine.DiscountSequenceID = (string) null;
    ((PXSelectBase) discountDetails).Cache.RaiseExceptionHandling("DiscountSequenceID", (object) currentDiscountDetailLine, (object) null, (Exception) new PXSetPropertyException("Discount Sequence {0} cannot be applied to this document.", (PXErrorLevel) 4, new object[1]
    {
      (object) manualDiscountSequenceID
    }));
  }

  public virtual void SetTotalDocDiscount(
    PXCache cache,
    PXSelectBase<TLine> lines,
    PXSelectBase<TDiscountDetail> discountDetails,
    Decimal? curyDiscountTotal,
    DiscountEngine.DiscountCalculationOptions discountCalculationOptions)
  {
    if (!curyDiscountTotal.HasValue || this.IsDiscountFeatureEnabled(cache.Graph, discountCalculationOptions))
      return;
    this.GetDocumentDetails(cache, lines);
    foreach (TDiscountDetail traceToDelete in this.GetDiscountDetailsByType(cache, discountDetails, (string) null))
      this.DeleteDiscountDetail(cache, discountDetails, traceToDelete);
    TDiscountDetail discountDetail = new TDiscountDetail();
    discountDetail.CuryDiscountAmt = curyDiscountTotal;
    discountDetail.Type = "B";
    discountDetail.Description = "Discount Total Adjustment";
    TDiscountDetail newTrace = discountDetail;
    this.InsertDiscountDetail(cache, discountDetails, newTrace, false);
  }

  public virtual bool SetExternalManualDocDiscount(
    PXCache cache,
    PXSelectBase<TLine> lines,
    PXSelectBase<TDiscountDetail> discountDetails,
    TDiscountDetail currentDiscountDetailLine,
    TDiscountDetail oldDiscountDetailLine,
    DiscountEngine.DiscountCalculationOptions discountCalculationOptions)
  {
    List<TLine> documentDetails = this.GetDocumentDetails(cache, lines);
    if (documentDetails.Count == 0)
      return false;
    if ((object) oldDiscountDetailLine == null)
      oldDiscountDetailLine = new TDiscountDetail();
    Decimal? nullable1 = currentDiscountDetailLine.DiscountPct;
    Decimal? nullable2 = oldDiscountDetailLine.DiscountPct;
    if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
    {
      nullable2 = currentDiscountDetailLine.CuryDiscountAmt;
      nullable1 = oldDiscountDetailLine.CuryDiscountAmt;
      if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue)
        goto label_7;
    }
    if ((currentDiscountDetailLine.Type == "B" || currentDiscountDetailLine.Type == "D" || currentDiscountDetailLine.Type == null) && !currentDiscountDetailLine.IsOrigDocDiscount.GetValueOrDefault())
    {
      Decimal curyTotalDiscountAmt1;
      this.GetDiscountAmountByType(((PXSelectBase) discountDetails).Cache, this.GetDiscountDetailsByType(cache, discountDetails, "G"), "G", out Decimal _, out curyTotalDiscountAmt1);
      Decimal curyTotalDiscountAmt2;
      this.GetDiscountAmountByType(((PXSelectBase) discountDetails).Cache, this.GetDiscountDetailsByType(cache, discountDetails, (string) null), (string) null, out Decimal _, out curyTotalDiscountAmt2);
      Decimal curyTotalLineAmt;
      this.SumAmounts(cache, documentDetails, true, out Decimal _, out curyTotalLineAmt);
      if (currentDiscountDetailLine.DiscountID == null)
      {
        currentDiscountDetailLine.Type = "B";
        currentDiscountDetailLine.CuryDiscountableAmt = new Decimal?(curyTotalLineAmt - curyTotalDiscountAmt1);
      }
      currentDiscountDetailLine.IsManual = new bool?(true);
      this.CalculateManualDiscountPercentOrAmount(cache, currentDiscountDetailLine, oldDiscountDetailLine);
      this.CalculateDocumentDiscountRate(cache, documentDetails, default (TLine), discountDetails, discountCalculationOptions);
      LineEntitiesFields mapFor = LineEntitiesFields.GetMapFor<TLine>(documentDetails.First<TLine>(), cache);
      Decimal discountLimit = this.GetDiscountLimit(cache, mapFor.CustomerID, mapFor.VendorID);
      Decimal num1 = curyTotalLineAmt / 100M * discountLimit;
      nullable2 = currentDiscountDetailLine.CuryDiscountAmt;
      Decimal num2 = curyTotalDiscountAmt2;
      nullable1 = nullable2.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + num2) : new Decimal?();
      Decimal valueOrDefault = nullable1.GetValueOrDefault();
      if (num1 < valueOrDefault & nullable1.HasValue)
        this.SetDiscountLimitException(cache, discountDetails, "G", PXMessages.LocalizeFormatNoPrefix("Total group discount exceeds limit configured for this customer class.", new object[1]
        {
          (object) discountLimit
        }), discountCalculationOptions, currentDiscountDetailLine);
      return true;
    }
label_7:
    return false;
  }

  protected virtual void CalculateManualDiscountPercentOrAmount(
    PXCache cache,
    TDiscountDetail discountDetailLine,
    TDiscountDetail oldDiscountDetailLine)
  {
    if ((object) oldDiscountDetailLine == null)
      oldDiscountDetailLine = new TDiscountDetail();
    Decimal? nullable1 = discountDetailLine.CuryDiscountAmt;
    Decimal? curyDiscountAmt = oldDiscountDetailLine.CuryDiscountAmt;
    bool flag1 = !(nullable1.GetValueOrDefault() == curyDiscountAmt.GetValueOrDefault() & nullable1.HasValue == curyDiscountAmt.HasValue);
    Decimal? nullable2 = discountDetailLine.CuryDiscountAmt;
    int num1;
    if (nullable2.HasValue)
    {
      nullable2 = discountDetailLine.CuryDiscountableAmt;
      Decimal num2 = 0M;
      num1 = !(nullable2.GetValueOrDefault() == num2 & nullable2.HasValue) ? 1 : 0;
    }
    else
      num1 = 0;
    bool flag2 = num1 != 0;
    if ((cache.Graph.IsCopyPasteContext | flag1) & flag2)
    {
      nullable2 = oldDiscountDetailLine.CuryDiscountAmt;
      if (nullable2.HasValue || !discountDetailLine.CuryDiscountAmt.IsNullOrZero())
      {
        // ISSUE: variable of a boxed type
        __Boxed<TDiscountDetail> local = (object) discountDetailLine;
        nullable1 = discountDetailLine.CuryDiscountAmt;
        Decimal num3 = nullable1.GetValueOrDefault() * 100M;
        nullable2 = discountDetailLine.CuryDiscountableAmt;
        Decimal? nullable3;
        if (!nullable2.HasValue)
        {
          nullable1 = new Decimal?();
          nullable3 = nullable1;
        }
        else
          nullable3 = new Decimal?(num3 / nullable2.GetValueOrDefault());
        nullable1 = nullable3;
        Decimal? nullable4 = new Decimal?(Math.Round(nullable1.GetValueOrDefault(), 6, MidpointRounding.AwayFromZero));
        local.DiscountPct = nullable4;
        return;
      }
    }
    nullable2 = discountDetailLine.DiscountPct;
    nullable1 = oldDiscountDetailLine.DiscountPct;
    if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue)
      return;
    nullable1 = discountDetailLine.DiscountPct;
    if (!nullable1.HasValue)
      return;
    // ISSUE: variable of a boxed type
    __Boxed<TDiscountDetail> local1 = (object) discountDetailLine;
    nullable1 = discountDetailLine.CuryDiscountableAmt;
    Decimal num4 = nullable1.GetValueOrDefault() / 100M;
    nullable1 = discountDetailLine.DiscountPct;
    Decimal valueOrDefault = nullable1.GetValueOrDefault();
    Decimal? nullable5 = new Decimal?(num4 * valueOrDefault);
    local1.CuryDiscountAmt = nullable5;
  }

  protected virtual UnitPriceVal GetUnitPrice(
    PXCache cache,
    TLine line,
    int? locationID,
    string curyID,
    DateTime date)
  {
    AmountLineFields mapFor1 = AmountLineFields.GetMapFor<TLine>(line, cache);
    LineEntitiesFields mapFor2 = LineEntitiesFields.GetMapFor<TLine>(line, cache);
    UnitPriceVal unitPrice = new UnitPriceVal();
    if (mapFor2.CustomerID.HasValue)
    {
      int? nullable = mapFor2.InventoryID;
      if (nullable.HasValue)
      {
        string customerPriceClassId = DiscountEngine.GetCustomerPriceClassID(cache, mapFor2.CustomerID, locationID);
        PXCache cache1 = cache;
        nullable = mapFor2.InventoryID;
        int? inventoryID = new int?(nullable.Value);
        int? siteId = mapFor2.SiteID;
        nullable = mapFor2.CustomerID;
        int? customerID = new int?(nullable.Value);
        string customerPriceClassID = customerPriceClassId;
        string curyID1 = curyID;
        string uom = mapFor1.UOM;
        Decimal? quantity = new Decimal?(mapFor1.Quantity.Value);
        DateTime date1 = date;
        int num = mapFor1.HaveBaseQuantity ? 1 : 0;
        ARSalesPriceMaint.SalesPriceItem salesPrice = this.GetSalesPrice(cache1, inventoryID, siteId, customerID, customerPriceClassID, curyID1, uom, quantity, date1, num != 0);
        if (salesPrice == null)
          return unitPrice;
        unitPrice.CuryUnitPrice = new Decimal?(salesPrice.Price);
        unitPrice.isBAccountSpecific = salesPrice.PriceType == "C";
        unitPrice.isPriceClassSpecific = salesPrice.PriceType == "P";
        unitPrice.isPromotional = salesPrice.IsPromotionalPrice;
        unitPrice.skipLineDiscount = salesPrice.SkipLineDiscounts;
      }
    }
    return unitPrice;
  }

  protected virtual void SetUnitPrice(PXCache cache, TLine line, UnitPriceVal unitPriceVal)
  {
    AmountLineFields mapFor = AmountLineFields.GetMapFor<TLine>(line, cache);
    if (!unitPriceVal.CuryUnitPrice.HasValue)
      return;
    Decimal? curyUnitPrice = mapFor.CuryUnitPrice;
    mapFor.CuryUnitPrice = unitPriceVal.CuryUnitPrice;
    mapFor.RaiseFieldUpdated<AmountLineFields.curyUnitPrice>((object) curyUnitPrice);
  }

  /// <summary>
  /// Calculates total discount amount. Prorates Amount discounts if needed.
  /// </summary>
  /// <returns>Returns total CuryDiscountAmt</returns>
  protected virtual Decimal CalculateDiscount(
    PXCache cache,
    DiscountDetailLine discount,
    DiscountLineFields dLine,
    Decimal curyAmount,
    Decimal quantity,
    DateTime date,
    string type)
  {
    Decimal discount1 = 0M;
    if (discount.DiscountedFor == "A")
    {
      if (discount.Prorate.Value && discount.AmountFrom.HasValue)
      {
        Decimal? amountFrom1 = discount.AmountFrom;
        Decimal num1 = 0M;
        if (!(amountFrom1.GetValueOrDefault() == num1 & amountFrom1.HasValue))
        {
          DiscountDetailLine discountDetailLine = discount;
          Decimal curyDiscountableAmount = curyAmount;
          Decimal discountableQuantity = quantity;
          discount1 = 0M;
          DiscountSequenceKey discountSequence = new DiscountSequenceKey(discount.DiscountID, discount.DiscountSequenceID);
          do
          {
            if (discount.BreakBy == "A")
            {
              if (curyDiscountableAmount < discountDetailLine.AmountFrom.GetValueOrDefault())
              {
                discountDetailLine = this.SelectApplicableDiscount(cache, dLine, discountSequence, curyDiscountableAmount, discountableQuantity, type, date);
                if (discountDetailLine.DiscountID != null)
                {
                  discount1 += discountDetailLine.Discount.GetValueOrDefault();
                  curyDiscountableAmount -= discountDetailLine.AmountFrom.GetValueOrDefault();
                }
                else
                  discountDetailLine = new DiscountDetailLine();
              }
              else
              {
                discount1 += discountDetailLine.Discount.GetValueOrDefault();
                curyDiscountableAmount -= discountDetailLine.AmountFrom.GetValueOrDefault();
              }
            }
            else if (discountableQuantity < discountDetailLine.AmountFrom.GetValueOrDefault())
            {
              discountDetailLine = this.SelectApplicableDiscount(cache, dLine, discountSequence, curyDiscountableAmount, discountableQuantity, type, date);
              if (discountDetailLine.DiscountID != null)
              {
                discount1 += discountDetailLine.Discount.GetValueOrDefault();
                discountableQuantity -= discountDetailLine.AmountFrom.GetValueOrDefault();
              }
              else
                discountDetailLine = new DiscountDetailLine();
            }
            else
            {
              discount1 += discountDetailLine.Discount.GetValueOrDefault();
              discountableQuantity -= discountDetailLine.AmountFrom.GetValueOrDefault();
            }
            Decimal? amountFrom2 = discountDetailLine.AmountFrom;
            Decimal num2 = 0M;
            if (amountFrom2.GetValueOrDefault() == num2 & amountFrom2.HasValue)
              discountDetailLine = new DiscountDetailLine();
          }
          while (discountDetailLine.DiscountID != null);
          goto label_21;
        }
      }
      discount1 = discount.Discount.GetValueOrDefault();
    }
    else if (discount.DiscountedFor == "P")
      discount1 = curyAmount / 100M * discount.Discount.GetValueOrDefault();
label_21:
    return discount1;
  }

  /// <summary>
  /// Calculates total free item quantity. Prorates Free-Item discounts if needed.
  /// </summary>
  /// <returns>Returns total FreeItemQty</returns>
  protected virtual Decimal CalculateFreeItemQuantity(
    PXCache cache,
    DiscountDetailLine discount,
    DiscountLineFields dLine,
    Decimal curyAmount,
    Decimal quantity,
    DateTime date,
    string type)
  {
    Decimal freeItemQuantity = 0M;
    if (discount.DiscountedFor == "F")
    {
      if (discount.Prorate.Value && discount.AmountFrom.HasValue)
      {
        Decimal? amountFrom1 = discount.AmountFrom;
        Decimal num1 = 0M;
        if (!(amountFrom1.GetValueOrDefault() == num1 & amountFrom1.HasValue))
        {
          DiscountDetailLine discountDetailLine = discount;
          Decimal curyDiscountableAmount = curyAmount;
          Decimal discountableQuantity = quantity;
          freeItemQuantity = 0M;
          DiscountSequenceKey discountSequence = new DiscountSequenceKey(discount.DiscountID, discount.DiscountSequenceID);
          do
          {
            if (discount.BreakBy == "A")
            {
              if (curyDiscountableAmount < discountDetailLine.AmountFrom.GetValueOrDefault())
              {
                discountDetailLine = this.SelectApplicableDiscount(cache, dLine, discountSequence, curyDiscountableAmount, discountableQuantity, type, date);
                if (discountDetailLine.DiscountID != null)
                {
                  freeItemQuantity += discountDetailLine.freeItemQty.GetValueOrDefault();
                  curyDiscountableAmount -= discountDetailLine.AmountFrom.GetValueOrDefault();
                }
                else
                  discountDetailLine = new DiscountDetailLine();
              }
              else
              {
                freeItemQuantity += discountDetailLine.freeItemQty.GetValueOrDefault();
                curyDiscountableAmount -= discountDetailLine.AmountFrom.GetValueOrDefault();
              }
            }
            else if (discountableQuantity < discountDetailLine.AmountFrom.GetValueOrDefault())
            {
              discountDetailLine = this.SelectApplicableDiscount(cache, dLine, discountSequence, curyDiscountableAmount, discountableQuantity, type, date);
              if (discountDetailLine.DiscountID != null)
              {
                freeItemQuantity += discountDetailLine.freeItemQty.GetValueOrDefault();
                discountableQuantity -= discountDetailLine.AmountFrom.GetValueOrDefault();
              }
              else
                discountDetailLine = new DiscountDetailLine();
            }
            else
            {
              freeItemQuantity += discountDetailLine.freeItemQty.GetValueOrDefault();
              discountableQuantity -= discountDetailLine.AmountFrom.GetValueOrDefault();
            }
            Decimal? amountFrom2 = discountDetailLine.AmountFrom;
            Decimal num2 = 0M;
            if (amountFrom2.GetValueOrDefault() == num2 & amountFrom2.HasValue)
              discountDetailLine = new DiscountDetailLine();
          }
          while (discountDetailLine.DiscountID != null);
          goto label_19;
        }
      }
      freeItemQuantity = discount.freeItemQty.GetValueOrDefault();
    }
label_19:
    return freeItemQuantity;
  }

  /// <summary>
  /// Checks if discount calculation needed for the given combination of entity line and discount type
  /// </summary>
  protected virtual bool IsDiscountCalculationNeeded(
    PXCache cache,
    TLine line,
    string discountType)
  {
    return this.IsDiscountCalculationNeeded(cache, line, discountType, (PXSelectBase<TDiscountDetail>) null);
  }

  protected virtual bool IsDiscountCalculationNeeded(
    PXCache cache,
    TLine line,
    string discountType,
    PXSelectBase<TDiscountDetail> discountDetails)
  {
    ConcurrentDictionary<string, DiscountCode> cachedDiscountCodes = DiscountEngine.GetCachedDiscountCodes();
    if (cachedDiscountCodes.Count == 0)
      return false;
    LineEntitiesFields mapFor = LineEntitiesFields.GetMapFor<TLine>(line, cache);
    switch (discountType)
    {
      case "L":
        if (DiscountLineFields.GetMapFor<TLine>(line, cache).DiscountID != null)
          return true;
        break;
      case "G":
        Decimal? groupDiscountRate = AmountLineFields.GetMapFor<TLine>(line, cache).GroupDiscountRate;
        Decimal num1 = 1M;
        if (!(groupDiscountRate.GetValueOrDefault() == num1 & groupDiscountRate.HasValue))
          return true;
        break;
      case "D":
        Decimal? documentDiscountRate = AmountLineFields.GetMapFor<TLine>(line, cache).DocumentDiscountRate;
        Decimal num2 = 1M;
        if (!(documentDiscountRate.GetValueOrDefault() == num2 & documentDiscountRate.HasValue) && (discountDetails == null || this.GetDiscountDetailsByType(cache, discountDetails, discountType).Any<TDiscountDetail>()))
          return true;
        break;
    }
    foreach (DiscountCode discountCode in ConcurrentDictionaryExtensions.ValuesExt<string, DiscountCode>(cachedDiscountCodes))
    {
      int? nullable = mapFor.VendorID;
      if (nullable.HasValue)
      {
        nullable = mapFor.CustomerID;
        if (!nullable.HasValue)
        {
          if (discountCode.IsVendorDiscount)
          {
            nullable = discountCode.VendorID;
            int? vendorId = mapFor.VendorID;
            if (nullable.GetValueOrDefault() == vendorId.GetValueOrDefault() & nullable.HasValue == vendorId.HasValue && discountCode.Type == discountType)
              return true;
            continue;
          }
          continue;
        }
      }
      if (!discountCode.IsVendorDiscount && discountCode.Type == discountType)
        return true;
    }
    return false;
  }

  /// <summary>Returns total discount amount by discount type</summary>
  /// <param name="cache"></param>
  /// <param name="discountDetails"></param>
  /// <param name="type">Set specific discount tyoe or set to null to get Discount Total</param>
  /// <param name="totalDiscountAmount"></param>
  /// <param name="curyTotalDiscountAmt"></param>
  /// <typeparam name="TDiscountDetail"></typeparam>
  protected virtual void GetDiscountAmountByType(
    PXCache cache,
    List<TDiscountDetail> discountDetails,
    string type,
    out Decimal totalDiscountAmount,
    out Decimal curyTotalDiscountAmt)
  {
    totalDiscountAmount = 0M;
    curyTotalDiscountAmt = 0M;
    foreach (TDiscountDetail discountDetail in discountDetails)
    {
      if ((discountDetail.Type == type || type == null) && !discountDetail.SkipDiscount.GetValueOrDefault() && !discountDetail.IsOrigDocDiscount.GetValueOrDefault())
      {
        ref Decimal local1 = ref curyTotalDiscountAmt;
        Decimal num1 = curyTotalDiscountAmt;
        Decimal? curyDiscountAmt = discountDetail.CuryDiscountAmt;
        Decimal valueOrDefault1 = curyDiscountAmt.GetValueOrDefault();
        Decimal num2 = num1 + valueOrDefault1;
        local1 = num2;
        PXCache sender = cache;
        // ISSUE: variable of a boxed type
        __Boxed<TDiscountDetail> row = (object) discountDetail;
        curyDiscountAmt = discountDetail.CuryDiscountAmt;
        Decimal valueOrDefault2 = curyDiscountAmt.GetValueOrDefault();
        Decimal num3;
        ref Decimal local2 = ref num3;
        MultiCurrencyCalculator.CuryConvBaseSkipRounding(sender, (object) row, valueOrDefault2, out local2);
        totalDiscountAmount += num3;
      }
    }
  }

  protected virtual List<TDiscountDetail> CollectManualDiscounts(
    PXCache cache,
    PXSelectBase<TDiscountDetail> discountDetails,
    string type)
  {
    return this.GetDiscountDetailsByType(cache, discountDetails, type).Where<TDiscountDetail>((Func<TDiscountDetail, bool>) (x => x.IsManual.GetValueOrDefault())).ToList<TDiscountDetail>();
  }

  protected virtual HashSet<DiscountSequenceKey> RemoveManualDiscounts(
    PXCache cache,
    PXSelectBase<TDiscountDetail> discountDetails,
    HashSet<DiscountSequenceKey> allApplicableDiscounts,
    string type)
  {
    if (allApplicableDiscounts.Count == 0)
      return new HashSet<DiscountSequenceKey>();
    List<TDiscountDetail> discountDetailsByType = this.GetDiscountDetailsByType(cache, discountDetails, type);
    return this.RemoveManualDiscounts(cache, allApplicableDiscounts, discountDetailsByType);
  }

  /// <summary>
  /// Removes manual discounts from allApplicableDiscounts. Manual discounts that already applied to the document will be retained.
  /// </summary>
  protected virtual HashSet<DiscountSequenceKey> RemoveManualDiscounts(
    PXCache cache,
    HashSet<DiscountSequenceKey> allApplicableDiscounts,
    List<TDiscountDetail> typedDiscountDetails)
  {
    if (allApplicableDiscounts.Count == 0)
      return new HashSet<DiscountSequenceKey>();
    ConcurrentDictionary<string, DiscountCode> cachedDiscountCodes = DiscountEngine.GetCachedDiscountCodes();
    HashSet<DiscountSequenceKey> discountSequenceKeySet = new HashSet<DiscountSequenceKey>();
    foreach (DiscountSequenceKey applicableDiscount in allApplicableDiscounts)
    {
      if (cachedDiscountCodes[applicableDiscount.DiscountID].IsManual)
      {
        foreach (TDiscountDetail typedDiscountDetail in typedDiscountDetails)
        {
          DiscountSequenceKey discountSequenceKey = new DiscountSequenceKey(typedDiscountDetail.DiscountID, typedDiscountDetail.DiscountSequenceID);
          if (applicableDiscount.Equals((object) discountSequenceKey) && cachedDiscountCodes[typedDiscountDetail.DiscountID].IsManual)
          {
            bool? isOrigDocDiscount = typedDiscountDetail.IsOrigDocDiscount;
            bool flag = false;
            if (isOrigDocDiscount.GetValueOrDefault() == flag & isOrigDocDiscount.HasValue)
              discountSequenceKeySet.Add(applicableDiscount);
          }
        }
      }
      else
        discountSequenceKeySet.Add(applicableDiscount);
    }
    return discountSequenceKeySet;
  }

  /// <summary>
  /// Removes manual discounts from allApplicableDiscounts. Manual discounts that already applied to the document will be retained.
  /// </summary>
  protected virtual HashSet<DiscountSequenceKey> RemoveManualDiscounts(
    PXCache cache,
    HashSet<DiscountSequenceKey> allApplicableDiscounts,
    HashSet<DiscountSequenceKey> manualDiscountDetails)
  {
    if (allApplicableDiscounts.Count == 0)
      return new HashSet<DiscountSequenceKey>();
    ConcurrentDictionary<string, DiscountCode> cachedDiscountCodes = DiscountEngine.GetCachedDiscountCodes();
    HashSet<DiscountSequenceKey> discountSequenceKeySet = new HashSet<DiscountSequenceKey>();
    foreach (DiscountSequenceKey applicableDiscount in allApplicableDiscounts)
    {
      if (cachedDiscountCodes[applicableDiscount.DiscountID].IsManual)
      {
        if (manualDiscountDetails.Contains(applicableDiscount))
          discountSequenceKeySet.Add(applicableDiscount);
      }
      else
        discountSequenceKeySet.Add(applicableDiscount);
    }
    return discountSequenceKeySet;
  }

  protected virtual void RemoveUnapplicableDiscountDetails(
    PXCache cache,
    PXSelectBase<TDiscountDetail> discountDetails,
    List<DiscountSequenceKey> newDiscountDetails,
    string type,
    bool removeManual = true,
    RecalcDiscountsParamFilter recalcFilter = null)
  {
    List<TDiscountDetail> discountDetailsByType = this.GetDiscountDetailsByType(cache, discountDetails, type);
    bool flag1 = recalcFilter != null && recalcFilter.UseRecalcFilter.GetValueOrDefault() && recalcFilter.OverrideManualDocGroupDiscounts.GetValueOrDefault();
    foreach (TDiscountDetail discountDetail in discountDetailsByType)
    {
      bool? nullable = discountDetail.IsManual;
      bool flag2 = false;
      if (!(nullable.GetValueOrDefault() == flag2 & nullable.HasValue))
      {
        nullable = discountDetail.IsManual;
        if (!nullable.GetValueOrDefault() || !removeManual)
          continue;
      }
      if (!flag1)
      {
        nullable = discountDetail.IsOrigDocDiscount;
        if (nullable.GetValueOrDefault())
          continue;
      }
      if (newDiscountDetails != null)
      {
        DiscountSequenceKey discountSequenceKey = new DiscountSequenceKey(discountDetail.DiscountID, discountDetail.DiscountSequenceID);
        if (!newDiscountDetails.Contains(discountSequenceKey))
          this.UpdateUnapplicableDiscountLine(cache, discountDetails, discountDetail);
      }
      else
        this.UpdateUnapplicableDiscountLine(cache, discountDetails, discountDetail);
    }
  }

  private void UpdateUnapplicableDiscountLine(
    PXCache cache,
    PXSelectBase<TDiscountDetail> discountDetails,
    TDiscountDetail discountDetail)
  {
    if (!discountDetail.SkipDiscount.GetValueOrDefault())
    {
      this.DeleteDiscountDetail(cache, discountDetails, discountDetail);
    }
    else
    {
      discountDetail.CuryDiscountableAmt = new Decimal?(0M);
      discountDetail.CuryDiscountAmt = new Decimal?(0M);
      discountDetail.DiscountableQty = new Decimal?(0M);
      discountDetail.DiscountPct = new Decimal?(0M);
      this.UpdateDiscountDetail(cache, discountDetails, discountDetail);
    }
  }

  protected virtual TDiscountDetail UpdateInsertOneDiscountTraceLine(
    PXCache cache,
    PXSelectBase<TDiscountDetail> discountDetails,
    TDiscountDetail trace,
    TDiscountDetail newTrace)
  {
    if ((object) trace == null)
      return this.InsertDiscountDetail(cache, discountDetails, newTrace);
    trace.CuryDiscountableAmt = newTrace.CuryDiscountableAmt;
    trace.DiscountableQty = newTrace.DiscountableQty;
    trace.CuryDiscountAmt = new Decimal?(newTrace.CuryDiscountAmt.GetValueOrDefault());
    trace.DiscountPct = newTrace.DiscountPct;
    trace.FreeItemID = newTrace.FreeItemID;
    trace.FreeItemQty = newTrace.FreeItemQty;
    return this.UpdateDiscountDetail(cache, discountDetails, trace);
  }

  protected virtual TDiscountDetail UpdateInsertDiscountTrace(
    PXCache cache,
    PXSelectBase<TDiscountDetail> discountDetails,
    TDiscountDetail newTrace)
  {
    TDiscountDetail trace = this.SearchForExistingDiscountDetail(cache, discountDetails, newTrace.DiscountID, newTrace.DiscountSequenceID, newTrace.Type, newTrace.Type == "B" ? newTrace.RecordID : new int?());
    if ((object) trace == null)
      return this.InsertDiscountDetail(cache, discountDetails, newTrace);
    trace.CuryDiscountableAmt = newTrace.CuryDiscountableAmt;
    trace.DiscountableQty = newTrace.DiscountableQty;
    trace.CuryDiscountAmt = new Decimal?(newTrace.CuryDiscountAmt.GetValueOrDefault());
    trace.DiscountPct = newTrace.DiscountPct;
    trace.FreeItemID = newTrace.FreeItemID;
    trace.FreeItemQty = newTrace.FreeItemQty;
    if (newTrace.IsManual.HasValue)
      trace.IsManual = newTrace.IsManual;
    return this.UpdateDiscountDetail(cache, discountDetails, trace);
  }

  public HashSet<KeyValuePair<object, string>> GetDiscountEntitiesDiscounts(
    PXCache cache,
    TLine line,
    int? locationID,
    bool isLineOrGroupDiscount,
    int? branchID = null,
    int? inventoryID = null,
    int? customerID = null)
  {
    LineEntitiesFields mapFor = LineEntitiesFields.GetMapFor<TLine>(line, cache);
    HashSet<KeyValuePair<object, string>> entities = new HashSet<KeyValuePair<object, string>>();
    int? nullable1;
    if (mapFor.VendorID.HasValue)
    {
      nullable1 = mapFor.CustomerID;
      if (!nullable1.HasValue)
      {
        entities.Add(new KeyValuePair<object, string>((object) mapFor.VendorID, "VE"));
        if (isLineOrGroupDiscount)
        {
          if (locationID.HasValue)
            entities.Add(new KeyValuePair<object, string>((object) locationID, "VL"));
          nullable1 = mapFor.InventoryID;
          int? nullable2 = nullable1 ?? inventoryID;
          if (nullable2.HasValue)
          {
            entities.Add(new KeyValuePair<object, string>((object) nullable2, "IN"));
            this.AddTemplateInventoryID(cache, line, nullable2, ref entities);
            this.AddInventoryPriceClassesToDiscountEntities(cache, line, nullable2, entities);
            goto label_17;
          }
          goto label_17;
        }
        goto label_17;
      }
    }
    nullable1 = mapFor.CustomerID;
    int? nullable3 = nullable1 ?? customerID;
    if (nullable3.HasValue)
    {
      entities.Add(new KeyValuePair<object, string>((object) nullable3, "CU"));
      if (locationID.HasValue)
        this.AddCustomerPriceClassesToDiscountEntities(cache, nullable3, locationID, entities);
    }
    nullable1 = mapFor.BranchID;
    int? key = nullable1 ?? branchID;
    if (key.HasValue)
      entities.Add(new KeyValuePair<object, string>((object) key, "BR"));
    if (isLineOrGroupDiscount)
    {
      nullable1 = mapFor.InventoryID;
      int? nullable4 = nullable1 ?? inventoryID;
      if (nullable4.HasValue)
      {
        entities.Add(new KeyValuePair<object, string>((object) nullable4, "IN"));
        this.AddTemplateInventoryID(cache, line, nullable4, ref entities);
        this.AddInventoryPriceClassesToDiscountEntities(cache, line, nullable4, entities);
      }
      nullable1 = mapFor.SiteID;
      if (nullable1.HasValue)
        entities.Add(new KeyValuePair<object, string>((object) mapFor.SiteID, "WH"));
    }
label_17:
    return entities;
  }

  /// <summary>
  /// Adds customer location price classes to the dictionary of discountable entities
  /// </summary>
  public virtual void AddCustomerPriceClassesToDiscountEntities(
    PXCache cache,
    int? customerID,
    int? locationID,
    HashSet<KeyValuePair<object, string>> entities)
  {
    string customerPriceClassId = DiscountEngine.GetCustomerPriceClassID(cache, customerID, locationID);
    if (customerPriceClassId == null)
      return;
    entities.Add(new KeyValuePair<object, string>((object) customerPriceClassId, "CE"));
  }

  /// <summary>
  /// Adds inventory price classes to the dictionary of discountable entities
  /// </summary>
  public virtual void AddInventoryPriceClassesToDiscountEntities(
    PXCache cache,
    TLine Line,
    int? inventoryID,
    HashSet<KeyValuePair<object, string>> entities)
  {
    string inventoryPriceClassId = DiscountEngine.GetInventoryPriceClassID<TLine>(cache, Line, new int?(inventoryID.GetValueOrDefault()));
    if (inventoryPriceClassId == null)
      return;
    entities.Add(new KeyValuePair<object, string>((object) inventoryPriceClassId, "IE"));
  }

  private void AddTemplateInventoryID(
    PXCache cache,
    TLine line,
    int? entityInventoryID,
    ref HashSet<KeyValuePair<object, string>> entities)
  {
    if (!DiscountEngine.FeatureInstalled<FeaturesSet.matrixItem>(cache.Graph))
      return;
    PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find(cache.Graph, entityInventoryID);
    if (inventoryItem == null || !inventoryItem.TemplateItemID.HasValue)
      return;
    entities.Add(new KeyValuePair<object, string>((object) inventoryItem.TemplateItemID, "IN"));
  }

  protected virtual List<TDiscountDetail> GetDiscountDetailsByType(
    PXCache cache,
    PXSelectBase<TDiscountDetail> discountDetails,
    string type,
    object[] parameters = null)
  {
    List<TDiscountDetail> discountDetailsByType = new List<TDiscountDetail>();
    if (parameters == null)
    {
      foreach (PXResult<TDiscountDetail> pxResult in discountDetails.Select(Array.Empty<object>()))
      {
        TDiscountDetail discountDetail = PXResult<TDiscountDetail>.op_Implicit(pxResult);
        if (discountDetail.Type == type || type == null)
          discountDetailsByType.Add(discountDetail);
      }
    }
    else
    {
      foreach (PXResult<TDiscountDetail> pxResult in discountDetails.Select(parameters))
      {
        TDiscountDetail discountDetail = PXResult<TDiscountDetail>.op_Implicit(pxResult);
        if (discountDetail.Type == type || type == null)
          discountDetailsByType.Add(discountDetail);
      }
    }
    return discountDetailsByType;
  }

  private TDiscountDetail SearchForExistingDiscountDetail(
    PXCache sender,
    PXSelectBase<TDiscountDetail> discountDetails,
    string discountID,
    string discountSequenceID,
    string type,
    int? recordID)
  {
    foreach (PXResult<TDiscountDetail> pxResult in discountDetails.Select(Array.Empty<object>()))
    {
      TDiscountDetail discountDetail = PXResult<TDiscountDetail>.op_Implicit(pxResult);
      if (!discountDetail.IsOrigDocDiscount.GetValueOrDefault())
      {
        if (recordID.HasValue)
        {
          int? recordId = discountDetail.RecordID;
          int? nullable = recordID;
          if (recordId.GetValueOrDefault() == nullable.GetValueOrDefault() & recordId.HasValue == nullable.HasValue)
            return discountDetail;
        }
        else if (discountDetail.DiscountID == discountID && discountDetail.DiscountSequenceID == discountSequenceID && discountDetail.Type == type)
          return discountDetail;
      }
    }
    return default (TDiscountDetail);
  }

  private List<TLine> GetDocumentDetails(
    PXCache cache,
    PXSelectBase<TLine> documentDetails,
    object[] parameters = null)
  {
    return this.GetDocumentDetails(cache, documentDetails, false, parameters);
  }

  private List<TLine> GetDocumentDetails(
    PXCache cache,
    PXSelectBase<TLine> documentDetails,
    bool useLegacyDetailsSelectMethod,
    object[] parameters = null)
  {
    Func<TLine, bool> predicate = (Func<TLine, bool>) (detail =>
    {
      DiscountLineFields mapFor = DiscountLineFields.GetMapFor<TLine>(detail, cache);
      return !mapFor.IsFree.GetValueOrDefault() && mapFor.LineType != "DS" && mapFor.LineType != "FR" && mapFor.LineType != "LA";
    });
    if (documentDetails.GetType().GetGenericTypeDefinition() == typeof (PXSelectExtension<>) | useLegacyDetailsSelectMethod)
      return GraphHelper.RowCast<TLine>((IEnumerable) documentDetails.Select(parameters)).Where<TLine>(predicate).ToList<TLine>();
    PXGraph graph = cache.Graph;
    Type type = ((object) this).GetType();
    PXView pxView;
    if (!graph.TypedViews.TryGetValue(type, ref pxView))
      pxView = graph.TypedViews[type] = (PXView) new DiscountEngine<TLine, TDiscountDetail>.UnsortedView(cache.Graph, false, ((PXSelectBase) documentDetails).View.BqlSelect);
    int num1 = 0;
    int num2 = 0;
    return GraphHelper.RowCast<TLine>((IEnumerable) pxView.Select((object[]) null, parameters, (object[]) null, (string[]) null, (bool[]) null, (PXFilterRow[]) null, ref num1, 0, ref num2)).Where<TLine>(predicate).ToList<TLine>();
  }

  /// <summary>
  /// Sums line amounts. Returns modified totalLineAmt and curyTotalLineAmt
  /// </summary>
  protected virtual void SumAmounts(
    PXCache cache,
    List<TLine> lines,
    out Decimal totalLineAmt,
    out Decimal curyTotalLineAmt)
  {
    totalLineAmt = 0M;
    curyTotalLineAmt = 0M;
    DiscountEngine.ApplyQuantityDiscountByBaseUOMOption discountByBaseUomOption = DiscountEngine.ApplyQuantityDiscountByBaseUOM(cache.Graph);
    ConcurrentDictionary<string, DiscountCode> cachedDiscountCodes = DiscountEngine.GetCachedDiscountCodes();
    foreach (TLine line in lines)
    {
      bool flag = false;
      AmountLineFields mapFor1 = AmountLineFields.GetMapFor<TLine>(line, cache, new DiscountEngine.ApplyQuantityDiscountByBaseUOMOption?(discountByBaseUomOption));
      DiscountLineFields mapFor2 = DiscountLineFields.GetMapFor<TLine>(line, cache);
      if (mapFor2.LineType == null || mapFor2.LineType != "DS" || mapFor2.LineType != "LA")
      {
        if (mapFor2.AutomaticDiscountsDisabled.GetValueOrDefault() || mapFor2.DiscountID != null && cachedDiscountCodes[mapFor2.DiscountID].ExcludeFromDiscountableAmt)
          flag = true;
        Decimal valueOrDefault = !flag ? mapFor1.CuryLineAmount.GetValueOrDefault() : 0M;
        Decimal baseval;
        MultiCurrencyCalculator.CuryConvBaseSkipRounding(cache, (object) mapFor1, valueOrDefault, out baseval);
        totalLineAmt += baseval;
        curyTotalLineAmt += valueOrDefault;
      }
    }
  }

  /// <summary>
  /// Sums line amounts. Returns modified totalLineAmt and curyTotalLineAmt
  /// </summary>
  protected virtual void SumAmounts(
    PXCache cache,
    List<TLine> lines,
    bool isExternalDiscount,
    out Decimal totalLineAmt,
    out Decimal curyTotalLineAmt)
  {
    totalLineAmt = 0M;
    curyTotalLineAmt = 0M;
    DiscountEngine.ApplyQuantityDiscountByBaseUOMOption discountByBaseUomOption = DiscountEngine.ApplyQuantityDiscountByBaseUOM(cache.Graph);
    ConcurrentDictionary<string, DiscountCode> cachedDiscountCodes = DiscountEngine.GetCachedDiscountCodes();
    foreach (TLine line in lines)
    {
      bool flag = false;
      AmountLineFields mapFor1 = AmountLineFields.GetMapFor<TLine>(line, cache, new DiscountEngine.ApplyQuantityDiscountByBaseUOMOption?(discountByBaseUomOption));
      DiscountLineFields mapFor2 = DiscountLineFields.GetMapFor<TLine>(line, cache);
      if (mapFor2.LineType == null || mapFor2.LineType != "DS" || mapFor2.LineType != "LA")
      {
        if (mapFor2.AutomaticDiscountsDisabled.GetValueOrDefault() && !isExternalDiscount || mapFor2.DiscountID != null && cachedDiscountCodes[mapFor2.DiscountID].ExcludeFromDiscountableAmt)
          flag = true;
        Decimal valueOrDefault = !flag ? mapFor1.CuryLineAmount.GetValueOrDefault() : 0M;
        Decimal baseval;
        MultiCurrencyCalculator.CuryConvBaseSkipRounding(cache, (object) mapFor1, valueOrDefault, out baseval);
        totalLineAmt += baseval;
        curyTotalLineAmt += valueOrDefault;
      }
    }
  }

  public virtual void ClearDiscount(PXCache cache, TLine line)
  {
    DiscountLineFields mapFor = DiscountLineFields.GetMapFor<TLine>(line, cache);
    mapFor.DiscountID = (string) null;
    mapFor.DiscountSequenceID = (string) null;
  }

  protected bool CompareRows<TRow>(TRow row1, TRow row2, params string[] ignore) where TRow : class
  {
    if ((object) row1 == null || (object) row2 == null)
      return (object) row1 == (object) row2;
    Type type = typeof (TRow);
    List<string> stringList = new List<string>((IEnumerable<string>) ignore);
    foreach (PropertyInfo property in type.GetProperties(BindingFlags.Instance | BindingFlags.Public))
    {
      if (!stringList.Contains(property.Name))
      {
        object obj1 = type.GetProperty(property.Name).GetValue((object) row1, (object[]) null);
        object obj2 = type.GetProperty(property.Name).GetValue((object) row2, (object[]) null);
        if (obj1 != obj2 && (obj1 == null || !obj1.Equals(obj2)))
          return false;
      }
    }
    return true;
  }

  protected virtual string GetLineDiscountTarget(PXCache cache, TLine line)
  {
    LineEntitiesFields mapFor = LineEntitiesFields.GetMapFor<TLine>(line, cache);
    string lineDiscountTarget = "E";
    if (mapFor != null)
    {
      int? nullable = mapFor.VendorID;
      if (nullable.HasValue)
      {
        nullable = mapFor.CustomerID;
        if (!nullable.HasValue)
        {
          PX.Objects.AP.Vendor vendor = PXResultset<PX.Objects.AP.Vendor>.op_Implicit(PXSelectBase<PX.Objects.AP.Vendor, PXSelect<PX.Objects.AP.Vendor, Where<PX.Objects.AP.Vendor.bAccountID, Equal<Required<PX.Objects.AP.Vendor.bAccountID>>>>.Config>.SelectSingleBound(cache.Graph, (object[]) null, new object[1]
          {
            (object) mapFor.VendorID
          }));
          if (vendor != null)
          {
            lineDiscountTarget = vendor.LineDiscountTarget;
            goto label_7;
          }
          goto label_7;
        }
      }
    }
    ARSetup arSetup = PXResultset<ARSetup>.op_Implicit(PXSelectBase<ARSetup, PXSelect<ARSetup>.Config>.Select(cache.Graph, Array.Empty<object>()));
    if (arSetup != null)
      lineDiscountTarget = arSetup.LineDiscountTarget;
label_7:
    return lineDiscountTarget;
  }

  public virtual void ValidateDiscountDetails(PXSelectBase<TDiscountDetail> discountDetails)
  {
    List<TDiscountDetail> discountDetailsByType1 = this.GetDiscountDetailsByType(((PXSelectBase) discountDetails).Cache, discountDetails, "D");
    if (discountDetailsByType1.Count > 1)
    {
      ((PXSelectBase) discountDetails).Cache.RaiseExceptionHandling("DiscountID", (object) discountDetailsByType1.First<TDiscountDetail>(), (object) discountDetailsByType1.First<TDiscountDetail>().DiscountID, (Exception) new PXSetPropertyException("Only one Document Discount allowed.", (PXErrorLevel) 4));
      throw new PXSetPropertyException("Only one Document Discount allowed.", (PXErrorLevel) 5);
    }
    List<TDiscountDetail> discountDetailsByType2 = this.GetDiscountDetailsByType(((PXSelectBase) discountDetails).Cache, discountDetails, "G");
    foreach (TDiscountDetail discountDetail1 in discountDetailsByType2)
    {
      foreach (TDiscountDetail discountDetail2 in discountDetailsByType2)
      {
        ushort? lineNbr = discountDetail1.LineNbr;
        int? nullable1 = lineNbr.HasValue ? new int?((int) lineNbr.GetValueOrDefault()) : new int?();
        lineNbr = discountDetail2.LineNbr;
        int? nullable2 = lineNbr.HasValue ? new int?((int) lineNbr.GetValueOrDefault()) : new int?();
        if (!(nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue) && discountDetail1.DiscountID == discountDetail2.DiscountID && discountDetail1.DiscountSequenceID == discountDetail2.DiscountSequenceID)
        {
          ((PXSelectBase) discountDetails).Cache.RaiseExceptionHandling("DiscountID", (object) discountDetail2, (object) discountDetail2.DiscountID, (Exception) new PXSetPropertyException("Duplicate Group Discount.", (PXErrorLevel) 5));
          throw new PXSetPropertyException("Duplicate Group Discount.", (PXErrorLevel) 5);
        }
      }
    }
  }

  public virtual TDiscountDetail InsertDiscountDetail(
    PXCache cache,
    PXSelectBase<TDiscountDetail> discountDetails,
    TDiscountDetail newTrace,
    bool setInternalDiscountEngineCall = true)
  {
    try
    {
      this.IsInternalDiscountEngineCall = setInternalDiscountEngineCall;
      return discountDetails.Insert(newTrace);
    }
    finally
    {
      this.IsInternalDiscountEngineCall = false;
    }
  }

  public virtual TDiscountDetail UpdateDiscountDetail(
    PXCache cache,
    PXSelectBase<TDiscountDetail> discountDetails,
    TDiscountDetail trace,
    bool setInternalDiscountEngineCall = true)
  {
    try
    {
      this.IsInternalDiscountEngineCall = setInternalDiscountEngineCall;
      return discountDetails.Update(trace);
    }
    finally
    {
      this.IsInternalDiscountEngineCall = false;
    }
  }

  public virtual TDiscountDetail DeleteDiscountDetail(
    PXCache cache,
    PXSelectBase<TDiscountDetail> discountDetails,
    TDiscountDetail traceToDelete,
    bool setInternalDiscountEngineCall = true)
  {
    try
    {
      this.IsInternalDiscountEngineCall = setInternalDiscountEngineCall;
      return discountDetails.Delete(traceToDelete);
    }
    finally
    {
      this.IsInternalDiscountEngineCall = false;
    }
  }

  /// <summary>
  /// Flag that indicates that the operation is called by the internal logic of the Discount Engine. Replaces 'e.ExternalCall == true' check in DiscountDetail_RowInserted/Updated/Deleted event handlers.
  /// </summary>
  public virtual bool IsInternalDiscountEngineCall
  {
    get => PXContext.GetSlot<bool>("InternalDEOperation");
    set => PXContext.SetSlot<bool>("InternalDEOperation", value);
  }

  /// <summary>Stores discounted document lines</summary>
  public struct DiscountedLines
  {
    public List<TLine> DiscountableLines { get; set; }

    public DiscountableValues DiscountableValues { get; set; }

    internal DiscountedLines(List<TLine> discountableLines, DiscountableValues discountableValues)
    {
      this.DiscountableLines = discountableLines;
      this.DiscountableValues = discountableValues;
    }
  }

  public class TDiscountDetailComparer : IEqualityComparer<TDiscountDetail>
  {
    public bool Equals(TDiscountDetail discountDetail1, TDiscountDetail discountDetail2)
    {
      if (!(discountDetail1.DiscountID == discountDetail2.DiscountID) || !(discountDetail1.DiscountSequenceID == discountDetail2.DiscountSequenceID) || !(discountDetail1.Type == discountDetail2.Type))
        return false;
      ushort? lineNbr = discountDetail1.LineNbr;
      int? nullable1 = lineNbr.HasValue ? new int?((int) lineNbr.GetValueOrDefault()) : new int?();
      lineNbr = discountDetail2.LineNbr;
      int? nullable2 = lineNbr.HasValue ? new int?((int) lineNbr.GetValueOrDefault()) : new int?();
      return nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue;
    }

    public int GetHashCode(TDiscountDetail discountDetail)
    {
      int num1 = 17 * 11;
      int? hashCode1 = discountDetail.DiscountID?.GetHashCode();
      int num2 = (hashCode1.HasValue ? new int?(num1 + hashCode1.GetValueOrDefault()) : new int?()).GetValueOrDefault() * 11;
      int? hashCode2 = discountDetail.DiscountSequenceID?.GetHashCode();
      int num3 = (hashCode2.HasValue ? new int?(num2 + hashCode2.GetValueOrDefault()) : new int?()).GetValueOrDefault() * 11;
      int? hashCode3 = discountDetail.Type?.GetHashCode();
      int num4 = (hashCode3.HasValue ? new int?(num3 + hashCode3.GetValueOrDefault()) : new int?()).GetValueOrDefault() * 11;
      ushort? lineNbr = discountDetail.LineNbr;
      ref ushort? local = ref lineNbr;
      int? nullable = local.HasValue ? new int?(local.GetValueOrDefault().GetHashCode()) : new int?();
      return (nullable.HasValue ? new int?(num4 + nullable.GetValueOrDefault()) : new int?()).GetValueOrDefault();
    }
  }

  public class UnsortedView(PXGraph graph, bool IsReadOnly, BqlCommand select) : PXView(graph, IsReadOnly, select)
  {
    protected virtual void SortResult(
      List<object> list,
      PXView.PXSearchColumn[] sorts,
      bool reverseOrder)
    {
    }
  }
}
