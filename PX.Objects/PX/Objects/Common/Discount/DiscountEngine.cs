// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.Discount.DiscountEngine
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.CM.TemporaryHelpers;
using PX.Objects.Common.Discount.Mappers;
using PX.Objects.Common.Extensions;
using PX.Objects.CS;
using PX.Objects.IN;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel;
using System.Linq;

#nullable enable
namespace PX.Objects.Common.Discount;

public abstract class DiscountEngine : PXGraph<
#nullable disable
DiscountEngine>
{
  private const string InventoryPriceClassesSlotName = "CachedDEInventoryPriceClasses";
  private const string CustomerPriceClassIDSlotName = "CachedDECustomerPriceClassID";
  private static readonly IReadOnlyDictionary<string, ApplicableToCombination> DiscountTargetToApplicableMap = (IReadOnlyDictionary<string, ApplicableToCombination>) new Dictionary<string, ApplicableToCombination>()
  {
    ["CU"] = ApplicableToCombination.Customer,
    ["IN"] = ApplicableToCombination.InventoryItem,
    ["CE"] = ApplicableToCombination.CustomerPriceClass,
    ["IE"] = ApplicableToCombination.InventoryPriceClass,
    ["CI"] = (ApplicableToCombination.Customer | ApplicableToCombination.InventoryItem),
    ["CP"] = (ApplicableToCombination.Customer | ApplicableToCombination.InventoryPriceClass),
    ["PI"] = (ApplicableToCombination.InventoryItem | ApplicableToCombination.CustomerPriceClass),
    ["PB"] = (ApplicableToCombination.CustomerPriceClass | ApplicableToCombination.Branch),
    ["PP"] = (ApplicableToCombination.CustomerPriceClass | ApplicableToCombination.InventoryPriceClass),
    ["CB"] = (ApplicableToCombination.Customer | ApplicableToCombination.Branch),
    ["WH"] = ApplicableToCombination.Warehouse,
    ["WC"] = (ApplicableToCombination.Customer | ApplicableToCombination.Warehouse),
    ["WE"] = (ApplicableToCombination.CustomerPriceClass | ApplicableToCombination.Warehouse),
    ["WI"] = (ApplicableToCombination.InventoryItem | ApplicableToCombination.Warehouse),
    ["WP"] = (ApplicableToCombination.InventoryPriceClass | ApplicableToCombination.Warehouse),
    ["BR"] = ApplicableToCombination.Branch,
    ["VE"] = ApplicableToCombination.Vendor,
    ["VI"] = (ApplicableToCombination.InventoryItem | ApplicableToCombination.Vendor),
    ["VP"] = (ApplicableToCombination.InventoryPriceClass | ApplicableToCombination.Vendor),
    ["VL"] = ApplicableToCombination.Location,
    ["LI"] = (ApplicableToCombination.InventoryItem | ApplicableToCombination.Location),
    ["UN"] = ApplicableToCombination.Unconditional
  };
  protected Lazy<ARDiscountSequenceMaint> ARDiscountSequence = Lazy.By<ARDiscountSequenceMaint>(new Func<ARDiscountSequenceMaint>(PXGraph.CreateInstance<ARDiscountSequenceMaint>));
  protected Lazy<APDiscountSequenceMaint> APDiscountSequence = Lazy.By<APDiscountSequenceMaint>(new Func<APDiscountSequenceMaint>(PXGraph.CreateInstance<APDiscountSequenceMaint>));
  public const DiscountEngine.DiscountCalculationOptions DefaultDiscountCalculationParameters = DiscountEngine.DiscountCalculationOptions.CalculateAll;
  /// <summary>Default option for AR. AP discounts disabled.</summary>
  public const DiscountEngine.DiscountCalculationOptions DefaultARDiscountCalculationParameters = DiscountEngine.DiscountCalculationOptions.DisableAPDiscountsCalculation;
  /// <summary>
  /// Default option for AP. AR discounts and Free-item discounts disabled.
  /// </summary>
  public const DiscountEngine.DiscountCalculationOptions DefaultAPDiscountCalculationParameters = DiscountEngine.DiscountCalculationOptions.DisableFreeItemDiscountsCalculation | DiscountEngine.DiscountCalculationOptions.DisableARDiscountsCalculation;

  /// <summary>
  /// Returns single best discount details line for a given Entities set
  /// </summary>
  /// <param name="cache">Cache</param>
  /// <param name="dline">Discount-related fields</param>
  /// <param name="discountType">Line or Document</param>
  protected virtual DiscountDetailLine SelectBestDiscount(
    PXCache cache,
    DiscountLineFields dLine,
    HashSet<KeyValuePair<object, string>> entities,
    string discountType,
    Decimal curyAmount,
    Decimal quantity,
    DateTime date)
  {
    DiscountEngine.GetDiscountTypes();
    return entities == null ? new DiscountDetailLine() : this.SelectApplicableDiscounts(cache, dLine, this.SelectApplicableEntityDiscounts(cache.Graph, entities, discountType, true), curyAmount, quantity, discountType, date).FirstOrDefault<DiscountDetailLine>();
  }

  /// <summary>
  /// Returns single DiscountDetails line on a given DiscountSequenceKey
  /// </summary>
  /// <param name="discountSequence">Applicable Discount Sequence</param>
  /// <param name="curyDiscountableAmount">Discountable amount</param>
  /// <param name="discountableQuantity">Discountable quantity</param>
  /// <param name="discountType">Discount type: line, group or document</param>
  protected virtual DiscountDetailLine SelectApplicableDiscount(
    PXCache cache,
    DiscountLineFields dLine,
    DiscountSequenceKey discountSequence,
    Decimal curyDiscountableAmount,
    Decimal discountableQuantity,
    string discountType,
    DateTime date)
  {
    PXCache cache1 = cache;
    DiscountLineFields dLine1 = dLine;
    HashSet<DiscountSequenceKey> discountSequences = new HashSet<DiscountSequenceKey>();
    discountSequences.Add(discountSequence);
    Decimal curyDiscountableAmount1 = curyDiscountableAmount;
    Decimal discountableQuantity1 = discountableQuantity;
    string discountType1 = discountType;
    DateTime date1 = date;
    return this.SelectApplicableDiscounts(cache1, dLine1, discountSequences, curyDiscountableAmount1, discountableQuantity1, discountType1, date1).FirstOrDefault<DiscountDetailLine>();
  }

  /// <summary>
  /// Returns single DiscountDetails line. Accepts HashSet of DiscountSequenceKey
  /// </summary>
  /// <param name="discountSequences">Applicable Discount Sequences</param>
  /// <param name="curyDiscountableAmount">Discountable amount</param>
  /// <param name="discountableQuantity">Discountable quantity</param>
  /// <param name="discountType">Discount type: line, group or document</param>
  protected virtual DiscountDetailLine SelectApplicableDiscount(
    PXCache cache,
    DiscountLineFields dLine,
    HashSet<DiscountSequenceKey> discountSequences,
    Decimal curyDiscountableAmount,
    Decimal discountableQuantity,
    string discountType,
    DateTime date)
  {
    return this.SelectApplicableDiscounts(cache, dLine, discountSequences, curyDiscountableAmount, discountableQuantity, discountType, date).FirstOrDefault<DiscountDetailLine>();
  }

  /// <summary>
  /// Returns all Discount Sequences applicable to a given entities set.
  /// </summary>
  /// <param name="entities">Entities dictionary</param>
  /// <param name="discountType">Line, Group or Document</param>
  /// <returns></returns>
  protected virtual HashSet<DiscountSequenceKey> SelectApplicableEntityDiscounts(
    PXGraph graph,
    HashSet<KeyValuePair<object, string>> entities,
    string discountType,
    bool skipManual,
    bool appliedToDR = false)
  {
    ConcurrentDictionary<string, DiscountCode> cachedDiscountTypes = DiscountEngine.GetCachedDiscountCodes();
    HashSet<DiscountSequenceKey> discountSequenceKeySet1 = new HashSet<DiscountSequenceKey>();
    HashSet<DiscountSequenceKey> discountSequenceKeySet2 = new HashSet<DiscountSequenceKey>();
    Dictionary<ApplicableToCombination, ImmutableHashSet<DiscountSequenceKey>> dictionary = new Dictionary<ApplicableToCombination, ImmutableHashSet<DiscountSequenceKey>>();
    int? key = (int?) entities.LastOrDefault<KeyValuePair<object, string>>((Func<KeyValuePair<object, string>, bool>) (e => e.Value == "VE")).Key;
    bool flag1 = !key.HasValue;
    bool flag2 = DiscountEngine.FeatureInstalled<FeaturesSet.customerDiscounts>(graph);
    bool flag3 = DiscountEngine.FeatureInstalled<FeaturesSet.vendorDiscounts>(graph);
    foreach (KeyValuePair<object, string> entity in entities)
    {
      ImmutableHashSet<DiscountSequenceKey> second = ImmutableHashSet.Create<DiscountSequenceKey>();
      if (flag1 & flag2)
        second = this.GetApplicableEntityARDiscounts(entity);
      else if (flag3)
        second = this.GetApplicableEntityAPDiscounts(entity, key);
      if (second.Count != 0)
      {
        ApplicableToCombination combination = DiscountEngine.SetApplicableToCombination(entity.Value);
        if (dictionary.ContainsKey(combination))
          dictionary[combination] = ImmutableHashSet.ToImmutableHashSet<DiscountSequenceKey>(((IEnumerable<DiscountSequenceKey>) dictionary[combination]).Concat<DiscountSequenceKey>((IEnumerable<DiscountSequenceKey>) second));
        else
          dictionary.Add(combination, second);
        EnumerableExtensions.AddRange<DiscountSequenceKey>((ISet<DiscountSequenceKey>) discountSequenceKeySet2, (IEnumerable<DiscountSequenceKey>) second);
      }
    }
    if (flag1)
      EnumerableExtensions.AddRange<DiscountSequenceKey>((ISet<DiscountSequenceKey>) discountSequenceKeySet1, this.GetUnconditionalDiscountsByType(discountType).Where<DiscountSequenceKey>((Func<DiscountSequenceKey, bool>) (ud => !skipManual || !cachedDiscountTypes[ud.DiscountID].IsManual)));
    foreach (DiscountSequenceKey discountSequenceKey in discountSequenceKeySet2)
    {
      if (cachedDiscountTypes.ContainsKey(discountSequenceKey.DiscountID) && cachedDiscountTypes[discountSequenceKey.DiscountID].Type == discountType && (!skipManual || !cachedDiscountTypes[discountSequenceKey.DiscountID].IsManual) && (!appliedToDR || cachedDiscountTypes[discountSequenceKey.DiscountID].IsAppliedToDR) && (flag1 && !cachedDiscountTypes[discountSequenceKey.DiscountID].IsVendorDiscount || !flag1 && cachedDiscountTypes[discountSequenceKey.DiscountID].IsVendorDiscount))
      {
        ApplicableToCombination applicableToCombination = ApplicableToCombination.None;
        bool flag4 = true;
        foreach (KeyValuePair<ApplicableToCombination, ImmutableHashSet<DiscountSequenceKey>> keyValuePair in dictionary)
        {
          if ((keyValuePair.Key & cachedDiscountTypes[discountSequenceKey.DiscountID].ApplicableToEnum) != ApplicableToCombination.None)
          {
            if (!keyValuePair.Value.Contains(discountSequenceKey))
            {
              flag4 = false;
              break;
            }
            applicableToCombination |= keyValuePair.Key;
          }
        }
        if (applicableToCombination == cachedDiscountTypes[discountSequenceKey.DiscountID].ApplicableToEnum & flag4)
          discountSequenceKeySet1.Add(discountSequenceKey);
      }
    }
    return discountSequenceKeySet1;
  }

  protected static bool FeatureInstalled<FeatureType>(PXGraph graph) where FeatureType : IBqlField
  {
    PXCache cach = graph.Caches[typeof (DiscountEngine.DiscountFeatures)];
    if (cach.Current == null)
      cach.Current = cach.CreateInstance();
    bool? nullable;
    if (!(nullable = (bool?) cach.GetValue<FeatureType>(cach.Current)).HasValue)
    {
      nullable = new bool?(PXAccess.FeatureInstalled<FeatureType>());
      cach.SetValue<FeatureType>(cach.Current, (object) nullable);
    }
    return nullable.Value;
  }

  /// <summary>
  /// Returns best available discount for Line and Document discount types. Returns list of all applicable discounts for Group discount type.
  /// </summary>
  /// <param name="discountSequences">Applicable Discount Sequences</param>
  /// <param name="curyDiscountableAmount">Discountable amount</param>
  /// <param name="discountableQuantity">Discountable quantity</param>
  /// <param name="discountType">Discount type: line, group or document</param>
  protected virtual List<DiscountDetailLine> SelectApplicableDiscounts(
    PXCache cache,
    DiscountLineFields dLine,
    HashSet<DiscountSequenceKey> discountSequences,
    Decimal curyDiscountableAmount,
    Decimal discountableQuantity,
    string discountType,
    DateTime date,
    bool ignoreCurrency = false)
  {
    if (cache == null)
      throw new ArgumentNullException(nameof (cache));
    if (!ignoreCurrency && dLine?.MappedLine != null && dLine.Cache.GetValue(dLine.MappedLine, "curyInfoID") != null)
      MultiCurrencyCalculator.CuryConvBase(cache, dLine.MappedLine, curyDiscountableAmount, out curyDiscountableAmount);
    List<DiscountDetailLine> col = new List<DiscountDetailLine>();
    if (discountType != "G")
    {
      Func<string, DiscountDetailLine> func = (Func<string, DiscountDetailLine>) (option => this.SelectSingleBestDiscount(cache, dLine, discountSequences.ToList<DiscountSequenceKey>(), curyDiscountableAmount, discountableQuantity, option, date));
      DiscountDetailLine discountDetailLine1 = func("A");
      DiscountDetailLine discountDetailLine2 = func("P");
      if (discountDetailLine1.DiscountID != null && discountDetailLine2.DiscountID != null)
      {
        List<DiscountDetailLine> discountDetailLineList = col;
        Decimal? discount1 = discountDetailLine1.Discount;
        Decimal num = curyDiscountableAmount / 100M;
        Decimal? discount2 = discountDetailLine2.Discount;
        Decimal? nullable = discount2.HasValue ? new Decimal?(num * discount2.GetValueOrDefault()) : new Decimal?();
        DiscountDetailLine discountDetailLine3 = discount1.GetValueOrDefault() < nullable.GetValueOrDefault() & discount1.HasValue & nullable.HasValue ? discountDetailLine2 : discountDetailLine1;
        discountDetailLineList.Add(discountDetailLine3);
      }
      else
      {
        if (discountDetailLine1.DiscountID != null && discountDetailLine2.DiscountID == null)
          col.Add(discountDetailLine1);
        if (discountDetailLine1.DiscountID == null && discountDetailLine2.DiscountID != null)
          col.Add(discountDetailLine2);
      }
    }
    else
      col.Add<DiscountDetailLine>((IEnumerable<DiscountDetailLine>) this.SelectAllApplicableDiscounts(cache, dLine, discountSequences.ToList<DiscountSequenceKey>(), curyDiscountableAmount, discountableQuantity, date));
    return col;
  }

  protected virtual DiscountDetailLine CreateDiscountDetails(
    PXCache cache,
    DiscountLineFields dLine,
    PXResult<PX.Objects.AR.DiscountSequence, DiscountSequenceDetail> discountResult,
    DateTime date)
  {
    DiscountSequenceDetail discountSequenceDetail = PXResult<PX.Objects.AR.DiscountSequence, DiscountSequenceDetail>.op_Implicit(discountResult);
    PX.Objects.AR.DiscountSequence discountSequence = PXResult<PX.Objects.AR.DiscountSequence, DiscountSequenceDetail>.op_Implicit(discountResult);
    if (discountSequenceDetail == null || discountSequence == null)
      return new DiscountDetailLine();
    ConcurrentDictionary<string, DiscountCode> cachedDiscountCodes = DiscountEngine.GetCachedDiscountCodes();
    DiscountDetailLine discountDetails = new DiscountDetailLine()
    {
      DiscountID = discountSequenceDetail.DiscountID,
      DiscountSequenceID = discountSequenceDetail.DiscountSequenceID,
      Type = cachedDiscountCodes[discountSequenceDetail.DiscountID].Type,
      DiscountedFor = discountSequence.DiscountedFor,
      BreakBy = discountSequence.BreakBy
    };
    if (discountDetails.DiscountedFor == "A" && discountDetails.Type != "L")
    {
      Decimal curyval;
      MultiCurrencyCalculator.CuryConvCury(cache, dLine.MappedLine, discountSequenceDetail.Discount.Value, out curyval, CommonSetupDecPl.PrcCst);
      discountDetails.Discount = new Decimal?(curyval);
    }
    else
      discountDetails.Discount = discountSequenceDetail.Discount;
    if (discountSequence.BreakBy == "Q")
    {
      discountDetails.AmountFrom = discountSequenceDetail.Quantity;
      discountDetails.AmountTo = discountSequenceDetail.QuantityTo;
    }
    else if (dLine != null)
    {
      Decimal curyval;
      MultiCurrencyCalculator.CuryConvCury(cache, dLine.MappedLine, discountSequenceDetail.Amount.GetValueOrDefault(), out curyval, CommonSetupDecPl.PrcCst);
      discountDetails.AmountFrom = new Decimal?(curyval);
      Decimal? amountTo = discountSequenceDetail.AmountTo;
      if (amountTo.HasValue)
      {
        PXCache sender = cache;
        object mappedLine = dLine.MappedLine;
        amountTo = discountSequenceDetail.AmountTo;
        Decimal valueOrDefault = amountTo.GetValueOrDefault();
        Decimal num;
        ref Decimal local = ref num;
        int prcCst = CommonSetupDecPl.PrcCst;
        MultiCurrencyCalculator.CuryConvCury(sender, mappedLine, valueOrDefault, out local, prcCst);
        discountDetails.AmountTo = new Decimal?(num);
      }
      else
        discountDetails.AmountTo = new Decimal?();
    }
    else
    {
      discountDetails.AmountFrom = discountSequenceDetail.Amount;
      discountDetails.AmountTo = discountSequenceDetail.AmountTo;
    }
    if (discountSequence.DiscountedFor == "F")
    {
      discountDetails.freeItemQty = discountSequenceDetail.FreeItemQty;
      ref DiscountDetailLine local = ref discountDetails;
      int? nullable;
      if (!discountSequence.IsPromotion.GetValueOrDefault())
      {
        DateTime? lastDate = discountSequenceDetail.LastDate;
        DateTime dateTime = date;
        if ((lastDate.HasValue ? (lastDate.GetValueOrDefault() <= dateTime ? 1 : 0) : 0) == 0)
        {
          nullable = discountSequence.LastFreeItemID;
          goto label_17;
        }
      }
      nullable = discountSequence.FreeItemID;
label_17:
      local.freeItemID = nullable;
    }
    discountDetails.Prorate = discountSequence.Prorate;
    return discountDetails;
  }

  protected static DiscountEngine.DCCache DiscountCodesCache
  {
    get
    {
      DiscountEngine.DCCache slot = PXContext.GetSlot<DiscountEngine.DCCache>();
      if (slot != null)
        return slot;
      return PXContext.SetSlot<DiscountEngine.DCCache>(PXDatabase.GetSlot<DiscountEngine.DCCache>(typeof (DiscountCode).Name, new System.Type[3]
      {
        typeof (ConcurrentDictionary<string, DiscountCode>),
        typeof (ARDiscount),
        typeof (APDiscount)
      }));
    }
  }

  public static void UpdateEntityCache()
  {
    PXContext.SetSlot<DiscountEngine.DECache>(PXDatabase.GetSlot<DiscountEngine.DECache>(typeof (PX.Objects.AR.DiscountSequence).Name, new System.Type[1]
    {
      typeof (PX.Objects.AR.DiscountSequence)
    }));
  }

  public static void PutEntityDiscountsToSlot<TTable, TKeyType>(
    ICollection<KeyValuePair<TKeyType, HashSet<DiscountSequenceKey>>> items)
    where TTable : IBqlTable
  {
    ConcurrentDictionary<TKeyType, ImmutableHashSet<DiscountSequenceKey>> entityDiscountsSlot = DiscountEngine.GetEntityDiscountsSlot<TTable, TKeyType>();
    foreach (KeyValuePair<TKeyType, HashSet<DiscountSequenceKey>> keyValuePair in (IEnumerable<KeyValuePair<TKeyType, HashSet<DiscountSequenceKey>>>) items)
      entityDiscountsSlot[keyValuePair.Key] = ImmutableHashSet.ToImmutableHashSet<DiscountSequenceKey>((IEnumerable<DiscountSequenceKey>) keyValuePair.Value);
  }

  private static TSlot GetSlot<TSlot>(string key) where TSlot : class, new()
  {
    TSlot slot1 = PXContext.GetSlot<TSlot>(key);
    if ((object) slot1 == null)
    {
      string key1 = key;
      string str = key;
      System.Type[] typeArray = new System.Type[1]
      {
        typeof (DummyTable)
      };
      TSlot slot2;
      slot1 = slot2 = PXDatabase.GetSlot<TSlot>(str, typeArray);
      PXContext.SetSlot<TSlot>(key1, slot2);
    }
    return slot1;
  }

  protected static ConcurrentDictionary<TKeyType, ImmutableHashSet<DiscountSequenceKey>> GetEntityDiscountsSlot<TTable, TKeyType>() where TTable : IBqlTable
  {
    return DiscountEngine.GetSlot<ConcurrentDictionary<TKeyType, ImmutableHashSet<DiscountSequenceKey>>>(typeof (TTable).Name);
  }

  protected static ConcurrentDictionary<int, string> GetInventoryPriceClassesSlot()
  {
    return DiscountEngine.GetSlot<ConcurrentDictionary<int, string>>("CachedDEInventoryPriceClasses");
  }

  protected static ConcurrentDictionary<Tuple<int, int>, string> GetCustomerPriceClassesSlot()
  {
    return DiscountEngine.GetSlot<ConcurrentDictionary<Tuple<int, int>, string>>("CachedDECustomerPriceClassID");
  }

  /// <summary>
  /// Returns dictionary of cached discount codes. Dictionary key is DiscountID
  /// </summary>
  protected static ConcurrentDictionary<string, DiscountCode> GetCachedDiscountCodes()
  {
    ConcurrentDictionary<string, DiscountCode> cachedDiscountCodes = DiscountEngine.DiscountCodesCache.cachedDiscountCodes;
    if (cachedDiscountCodes.Count != 0)
      return cachedDiscountCodes;
    DiscountEngine.GetDiscountTypes();
    return cachedDiscountCodes;
  }

  /// <summary>
  /// Collects all discount types and unconditional AR discounts
  /// </summary>
  /// <param name="clearCache">Set to true to clear discount types cache and recreate it.</param>
  public static void GetDiscountTypes(bool clearCache = false)
  {
    DiscountEngine.SelectUnconditionalDiscounts(clearCache);
  }

  private HashSet<DiscountSequenceKey> GetUnconditionalDiscountsByType(string type)
  {
    ConcurrentDictionary<object, ImmutableHashSet<DiscountSequenceKey>> unconditionalDiscounts = DiscountEngine.CachedUnconditionalDiscounts;
    if (!unconditionalDiscounts.ContainsKey((object) "Unconditional"))
      return new HashSet<DiscountSequenceKey>();
    HashSet<DiscountSequenceKey> unconditionalDiscountsByType = new HashSet<DiscountSequenceKey>();
    foreach (DiscountSequenceKey discountSequenceKey in unconditionalDiscounts[(object) "Unconditional"])
    {
      if (DiscountEngine.GetCachedDiscountCodes().ContainsKey(discountSequenceKey.DiscountID))
      {
        if (DiscountEngine.GetCachedDiscountCodes()[discountSequenceKey.DiscountID].Type == type)
          unconditionalDiscountsByType.Add(discountSequenceKey);
      }
      else
      {
        DiscountEngine.SelectUnconditionalDiscounts(true);
        break;
      }
    }
    return unconditionalDiscountsByType;
  }

  private static HashSet<DiscountSequenceKey> SelectUnconditionalDiscounts(bool clearCache)
  {
    ConcurrentDictionary<object, ImmutableHashSet<DiscountSequenceKey>> unconditionalDiscounts = DiscountEngine.CachedUnconditionalDiscounts;
    if (clearCache)
      unconditionalDiscounts.Clear();
    if (!unconditionalDiscounts.ContainsKey((object) "Unconditional"))
    {
      unconditionalDiscounts.GetOrAdd<object, ImmutableHashSet<DiscountSequenceKey>>((object) "Unconditional", new Func<ImmutableHashSet<DiscountSequenceKey>>(ImmutableHashSet.Create<DiscountSequenceKey>));
      IEnumerable<DiscountSequenceKey> second = GraphHelper.RowCast<PX.Objects.AR.DiscountSequence>((IEnumerable) PXSelectBase<ARDiscount, PXSelectJoin<ARDiscount, InnerJoin<PX.Objects.AR.DiscountSequence, On<PX.Objects.AR.DiscountSequence.discountID, Equal<ARDiscount.discountID>>>, Where<ARDiscount.applicableTo, Equal<DiscountTarget.unconditional>>>.Config>.Select(PXGraph.CreateInstance<PXGraph>(), Array.Empty<object>())).Select<PX.Objects.AR.DiscountSequence, DiscountSequenceKey>((Func<PX.Objects.AR.DiscountSequence, DiscountSequenceKey>) (d => new DiscountSequenceKey(d.DiscountID, d.DiscountSequenceID)));
      unconditionalDiscounts[(object) "Unconditional"] = ImmutableHashSet.ToImmutableHashSet<DiscountSequenceKey>(((IEnumerable<DiscountSequenceKey>) unconditionalDiscounts[(object) "Unconditional"]).Concat<DiscountSequenceKey>(second));
    }
    return new HashSet<DiscountSequenceKey>((IEnumerable<DiscountSequenceKey>) unconditionalDiscounts[(object) "Unconditional"]);
  }

  private static ConcurrentDictionary<object, ImmutableHashSet<DiscountSequenceKey>> CachedUnconditionalDiscounts
  {
    get
    {
      return DiscountEngine.GetSlot<ConcurrentDictionary<object, ImmutableHashSet<DiscountSequenceKey>>>("Unconditional");
    }
  }

  private static ApplicableToCombination SetApplicableToCombination(string applicableTo)
  {
    ApplicableToCombination combination;
    DiscountEngine.DiscountTargetToApplicableMap.TryGetValue(applicableTo, out combination);
    return combination;
  }

  private ImmutableHashSet<DiscountSequenceKey> GetApplicableEntityARDiscounts(
    KeyValuePair<object, string> entity)
  {
    switch (entity.Value)
    {
      case "CU":
        return this.SelectEntityDiscounts<DiscountCustomer, DiscountCustomer.discountID, DiscountCustomer.discountSequenceID, DiscountCustomer.customerID, int>((int) entity.Key);
      case "IN":
        return this.SelectEntityDiscounts<DiscountItem, DiscountItem.discountID, DiscountItem.discountSequenceID, DiscountItem.inventoryID, int>((int) entity.Key);
      case "CE":
        return this.SelectEntityDiscounts<DiscountCustomerPriceClass, DiscountCustomerPriceClass.discountID, DiscountCustomerPriceClass.discountSequenceID, DiscountCustomerPriceClass.customerPriceClassID, string>((string) entity.Key);
      case "IE":
        return this.SelectEntityDiscounts<DiscountInventoryPriceClass, DiscountInventoryPriceClass.discountID, DiscountInventoryPriceClass.discountSequenceID, DiscountInventoryPriceClass.inventoryPriceClassID, string>((string) entity.Key);
      case "BR":
        return this.SelectEntityDiscounts<DiscountBranch, DiscountBranch.discountID, DiscountBranch.discountSequenceID, DiscountBranch.branchID, int>((int) entity.Key);
      case "WH":
        return this.SelectEntityDiscounts<DiscountSite, DiscountSite.discountID, DiscountSite.discountSequenceID, DiscountSite.siteID, int>((int) entity.Key);
      default:
        return ImmutableHashSet.Create<DiscountSequenceKey>();
    }
  }

  private ImmutableHashSet<DiscountSequenceKey> GetApplicableEntityAPDiscounts(
    KeyValuePair<object, string> entity,
    int? vendorID)
  {
    switch (entity.Value)
    {
      case "IN":
        return this.SelectEntityDiscounts<DiscountItem, DiscountItem.discountID, DiscountItem.discountSequenceID, DiscountItem.inventoryID, int>((int) entity.Key);
      case "VE":
        return this.SelectEntityDiscounts<APDiscountVendor, APDiscountVendor.discountID, APDiscountVendor.discountSequenceID, APDiscountVendor.vendorID, int>((int) entity.Key);
      case "IE":
        return this.SelectEntityDiscounts<DiscountInventoryPriceClass, DiscountInventoryPriceClass.discountID, DiscountInventoryPriceClass.discountSequenceID, DiscountInventoryPriceClass.inventoryPriceClassID, string>((string) entity.Key);
      case "VL":
        return vendorID.HasValue ? this.SelectEntityDiscounts<APDiscountLocation, APDiscountLocation.discountID, APDiscountLocation.discountSequenceID, APDiscountLocation.vendorID, int, APDiscountLocation.locationID, int>(vendorID.Value, (int) entity.Key) : ImmutableHashSet.Create<DiscountSequenceKey>();
      default:
        return ImmutableHashSet.Create<DiscountSequenceKey>();
    }
  }

  /// <summary>
  /// Removes entity from the list of cached Inventory ID to Inventory Price Class correlations
  /// </summary>
  public static void RemoveFromCachedInventoryPriceClasses(int? inventoryID)
  {
    int valueOrDefault = inventoryID.GetValueOrDefault();
    ConcurrentDictionary<int, string> priceClassesSlot = DiscountEngine.GetInventoryPriceClassesSlot();
    if (!priceClassesSlot.ContainsKey(valueOrDefault))
      return;
    priceClassesSlot.Remove<int, string>(valueOrDefault);
  }

  protected static string GetInventoryPriceClassID<TLine>(
    PXCache cache,
    TLine line,
    int? inventoryID)
    where TLine : class, IBqlTable, new()
  {
    if (!inventoryID.HasValue)
      return (string) null;
    int key = inventoryID.Value;
    ConcurrentDictionary<int, string> priceClassesSlot = DiscountEngine.GetInventoryPriceClassesSlot();
    if (priceClassesSlot.ContainsKey(key))
      return priceClassesSlot[key];
    if (priceClassesSlot.Count >= 5000)
      priceClassesSlot.Clear();
    PX.Objects.IN.InventoryItem inventoryItem1 = (PX.Objects.IN.InventoryItem) PXSelectorAttribute.Select<PX.Objects.IN.InventoryItem.inventoryID>(cache, (object) line);
    if (inventoryItem1 == null)
      inventoryItem1 = PXResultset<PX.Objects.IN.InventoryItem>.op_Implicit(PXSelectBase<PX.Objects.IN.InventoryItem, PXSelectReadonly<PX.Objects.IN.InventoryItem, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Required<PX.Objects.IN.InventoryItem.inventoryID>>>>.Config>.Select(cache.Graph, new object[1]
      {
        (object) inventoryID
      }));
    PX.Objects.IN.InventoryItem inventoryItem2 = inventoryItem1;
    return inventoryItem2 != null ? priceClassesSlot.GetOrAdd(key, inventoryItem2.PriceClassID) : (string) null;
  }

  /// <summary>
  /// Removes entity from the list of cached Customer ID to Customer Price Class correlations
  /// </summary>
  public static void RemoveFromCachedCustomerPriceClasses(int? bAccountID)
  {
    int bAcctID = bAccountID.GetValueOrDefault();
    ConcurrentDictionary<Tuple<int, int>, string> priceClassesSlot = DiscountEngine.GetCustomerPriceClassesSlot();
    foreach (Tuple<int, int> key in priceClassesSlot.Keys.Where<Tuple<int, int>>((Func<Tuple<int, int>, bool>) (k => k.Item1 == bAcctID)))
      priceClassesSlot.Remove<Tuple<int, int>, string>(key);
  }

  protected static string GetCustomerPriceClassID(PXCache cache, int? bAccountID, int? locationID)
  {
    if (!bAccountID.HasValue || !locationID.HasValue)
      return (string) null;
    Tuple<int, int> key = Tuple.Create<int, int>(bAccountID.Value, locationID.Value);
    ConcurrentDictionary<Tuple<int, int>, string> priceClassesSlot = DiscountEngine.GetCustomerPriceClassesSlot();
    if (priceClassesSlot.ContainsKey(key))
      return priceClassesSlot[key];
    if (priceClassesSlot.Count >= 5000)
      priceClassesSlot.Clear();
    PX.Objects.CR.Location location = PXResultset<PX.Objects.CR.Location>.op_Implicit(PXSelectBase<PX.Objects.CR.Location, PXSelectReadonly<PX.Objects.CR.Location, Where<PX.Objects.CR.Location.bAccountID, Equal<Required<PX.Objects.CR.Location.bAccountID>>, And<PX.Objects.CR.Location.locationID, Equal<Required<PX.Objects.CR.Location.locationID>>>>>.Config>.Select(cache.Graph, new object[2]
    {
      (object) bAccountID,
      (object) locationID
    }));
    return location != null ? priceClassesSlot.GetOrAdd(key, location.CPriceClassID) : (string) null;
  }

  /// <summary>Returns single best available discount</summary>
  /// <param name="discountSequences">Applicable Discount Sequences</param>
  /// <param name="amount">Discountable amount</param>
  /// <param name="quantity">Discountable quantity</param>
  /// <param name="discountFor">Discounted for: amount, percent or free item</param>
  protected virtual DiscountDetailLine SelectSingleBestDiscount(
    PXCache cache,
    DiscountLineFields dLine,
    List<DiscountSequenceKey> discountSequences,
    Decimal amount,
    Decimal quantity,
    string discountFor,
    DateTime date)
  {
    if ((discountSequences != null ? (!discountSequences.Any<DiscountSequenceKey>() ? 1 : 0) : 1) != 0)
      return new DiscountDetailLine();
    PXResult<PX.Objects.AR.DiscountSequence, DiscountSequenceDetail> discountResult = this.GetDiscounts(discountSequences, amount, quantity, discountFor, date, true).SingleOrDefault<PXResult<PX.Objects.AR.DiscountSequence, DiscountSequenceDetail>>();
    PX.Objects.AR.DiscountSequence discountSequence = PXResult<PX.Objects.AR.DiscountSequence, DiscountSequenceDetail>.op_Implicit(discountResult);
    return discountSequence != null && (!(discountSequence.BreakBy != "A") && !(amount != 0M) || !(discountSequence.BreakBy != "Q") && !(quantity != 0M)) ? new DiscountDetailLine() : this.CreateDiscountDetails(cache, dLine, discountResult, date);
  }

  /// <summary>Returns list of all applicable discounts</summary>
  /// <param name="discountSequences">Applicable Discount Sequences</param>
  /// <param name="amount">Discountable amount</param>
  /// <param name="quantity">Discountable quantity</param>
  protected virtual List<DiscountDetailLine> SelectAllApplicableDiscounts(
    PXCache cache,
    DiscountLineFields dLine,
    List<DiscountSequenceKey> discountSequences,
    Decimal amount,
    Decimal quantity,
    DateTime date)
  {
    List<DiscountDetailLine> discountDetailLineList = new List<DiscountDetailLine>();
    if ((discountSequences != null ? (!discountSequences.Any<DiscountSequenceKey>() ? 1 : 0) : 1) != 0)
      return discountDetailLineList;
    foreach (PXResult<PX.Objects.AR.DiscountSequence, DiscountSequenceDetail> discount1 in this.GetDiscounts(discountSequences, amount, quantity, string.Empty, date, false))
    {
      PX.Objects.AR.DiscountSequence sequence = PXResult<PX.Objects.AR.DiscountSequence, DiscountSequenceDetail>.op_Implicit(discount1);
      if (sequence != null && (!(sequence.BreakBy == "A") || !(amount == 0M)) && (!(sequence.BreakBy == "Q") || !(quantity == 0M)))
      {
        int index = discountDetailLineList.FindIndex((Predicate<DiscountDetailLine>) (x => x.DiscountID == sequence.DiscountID && x.DiscountSequenceID == sequence.DiscountSequenceID && x.DiscountedFor == sequence.DiscountedFor));
        DiscountDetailLine discountDetails = this.CreateDiscountDetails(cache, dLine, discount1, date);
        if (index < 0)
        {
          discountDetailLineList.Add(discountDetails);
        }
        else
        {
          Decimal? discount2 = discountDetailLineList[index].Discount;
          Decimal? discount3 = discountDetails.Discount;
          if (discount2.GetValueOrDefault() < discount3.GetValueOrDefault() & discount2.HasValue & discount3.HasValue)
            discountDetailLineList[index] = discountDetails;
        }
      }
    }
    return discountDetailLineList;
  }

  protected virtual List<PXResult<PX.Objects.AR.DiscountSequence, DiscountSequenceDetail>> GetDiscounts(
    List<DiscountSequenceKey> discountSequences,
    Decimal amount,
    Decimal quantity,
    string discountedFor,
    DateTime date,
    bool single)
  {
    if (discountSequences.Count == 0)
      return (List<PXResult<PX.Objects.AR.DiscountSequence, DiscountSequenceDetail>>) null;
    List<object> objectList = new List<object>()
    {
      (object) "A",
      (object) Math.Abs(amount),
      (object) Math.Abs(amount),
      (object) "Q",
      (object) Math.Abs(quantity),
      (object) Math.Abs(quantity),
      (object) "A",
      (object) Math.Abs(amount),
      (object) "Q",
      (object) Math.Abs(quantity),
      (object) date,
      (object) date,
      (object) date,
      (object) date
    };
    List<System.Type> typeList = new List<System.Type>();
    if (discountedFor != string.Empty)
    {
      typeList.Add(typeof (And<,,>));
      typeList.Add(typeof (PX.Objects.AR.DiscountSequence.discountedFor));
      typeList.Add(typeof (Equal<Required<PX.Objects.AR.DiscountSequence.discountedFor>>));
      objectList.Add((object) discountedFor);
    }
    typeList.Add(typeof (And<>));
    typeList.Add(typeof (Where<,,>));
    for (int index = 0; index < discountSequences.Count; ++index)
    {
      bool flag = index == discountSequences.Count - 1;
      typeList.Add(typeof (DiscountSequenceDetail.discountID));
      typeList.Add(typeof (Equal<Required<DiscountSequenceDetail.discountID>>));
      typeList.Add(!flag ? typeof (And<,,>) : typeof (And<,>));
      typeList.Add(typeof (DiscountSequenceDetail.discountSequenceID));
      typeList.Add(typeof (Equal<Required<DiscountSequenceDetail.discountSequenceID>>));
      objectList.Add((object) discountSequences[index].DiscountID);
      objectList.Add((object) discountSequences[index].DiscountSequenceID);
      if (!flag)
        typeList.Add(typeof (Or<,,>));
    }
    BqlCommand command = BqlTemplate.OfCommand<Select2<PX.Objects.AR.DiscountSequence, LeftJoin<DiscountSequenceDetail, On<DiscountSequenceDetail.discountID, Equal<PX.Objects.AR.DiscountSequence.discountID>, And<DiscountSequenceDetail.discountSequenceID, Equal<PX.Objects.AR.DiscountSequence.discountSequenceID>>>>, Where2<Where<Where2<Where<PX.Objects.AR.DiscountSequence.breakBy, Equal<Required<PX.Objects.AR.DiscountSequence.breakBy>>, And<DiscountSequenceDetail.amount, LessEqual<Required<DiscountSequenceDetail.amount>>, And<DiscountSequenceDetail.amountTo, Greater<Required<DiscountSequenceDetail.amountTo>>, Or<PX.Objects.AR.DiscountSequence.breakBy, Equal<Required<PX.Objects.AR.DiscountSequence.breakBy>>, And<DiscountSequenceDetail.quantity, LessEqual<Required<DiscountSequenceDetail.quantity>>, And<DiscountSequenceDetail.quantityTo, Greater<Required<DiscountSequenceDetail.quantityTo>>, Or<Where2<Where<PX.Objects.AR.DiscountSequence.breakBy, Equal<Required<PX.Objects.AR.DiscountSequence.breakBy>>, And<Where2<Where<DiscountSequenceDetail.amountTo, IsNull, Or<DiscountSequenceDetail.amountTo, Equal<decimal0>>>, And<DiscountSequenceDetail.amount, LessEqual<Required<DiscountSequenceDetail.amount>>>>>>, Or<Where<PX.Objects.AR.DiscountSequence.breakBy, Equal<Required<PX.Objects.AR.DiscountSequence.breakBy>>, And<Where2<Where<DiscountSequenceDetail.quantityTo, IsNull, Or<DiscountSequenceDetail.quantityTo, Equal<decimal0>>>, And<DiscountSequenceDetail.quantity, LessEqual<Required<DiscountSequenceDetail.quantity>>>>>>>>>>>>>>>, And<Where<PX.Objects.AR.DiscountSequence.isActive, Equal<True>, And<DiscountSequenceDetail.isActive, Equal<True>, And<Where2<Where<PX.Objects.AR.DiscountSequence.isPromotion, Equal<False>, And<Where2<Where<DiscountSequenceDetail.lastDate, LessEqual<Required<DiscountSequenceDetail.lastDate>>, And<DiscountSequenceDetail.isLast, Equal<False>>>, Or<Where<DiscountSequenceDetail.lastDate, Greater<Required<DiscountSequenceDetail.lastDate>>, And<DiscountSequenceDetail.isLast, Equal<True>>>>>>>, Or<Where<PX.Objects.AR.DiscountSequence.isPromotion, Equal<True>, And<DiscountSequenceDetail.isLast, Equal<False>, And<PX.Objects.AR.DiscountSequence.startDate, LessEqual<Required<PX.Objects.AR.DiscountSequence.startDate>>, And<PX.Objects.AR.DiscountSequence.endDate, GreaterEqual<Required<PX.Objects.AR.DiscountSequence.endDate>>>>>>>>>>>>>>, BqlPlaceholder.A>, OrderBy<Asc<DiscountSequenceDetail.isLast, Desc<DiscountSequenceDetail.discount>>>>>.Replace<BqlPlaceholder.A>(BqlCommand.Compose(typeList.ToArray())).ToCommand();
    List<PXResult<PX.Objects.AR.DiscountSequence, DiscountSequenceDetail>> list1 = new PXView(PXGraph.CreateInstance<PXGraph>(), false, command).SelectMulti(objectList.ToArray()).Cast<PXResult<PX.Objects.AR.DiscountSequence, DiscountSequenceDetail>>().ToList<PXResult<PX.Objects.AR.DiscountSequence, DiscountSequenceDetail>>();
    List<PXResult<PX.Objects.AR.DiscountSequence, DiscountSequenceDetail>> discounts = new List<PXResult<PX.Objects.AR.DiscountSequence, DiscountSequenceDetail>>();
    if (list1.Count<PXResult<PX.Objects.AR.DiscountSequence, DiscountSequenceDetail>>() > 0)
    {
      List<PXResult<PX.Objects.AR.DiscountSequence, DiscountSequenceDetail>> list2 = list1.GroupBy<PXResult<PX.Objects.AR.DiscountSequence, DiscountSequenceDetail>, bool?, IOrderedEnumerable<PXResult<PX.Objects.AR.DiscountSequence, DiscountSequenceDetail>>>((Func<PXResult<PX.Objects.AR.DiscountSequence, DiscountSequenceDetail>, bool?>) (_ => PXResult<PX.Objects.AR.DiscountSequence, DiscountSequenceDetail>.op_Implicit(_).IsLast), (Func<bool?, IEnumerable<PXResult<PX.Objects.AR.DiscountSequence, DiscountSequenceDetail>>, IOrderedEnumerable<PXResult<PX.Objects.AR.DiscountSequence, DiscountSequenceDetail>>>) ((keyIsLast, groupIsLast) => groupIsLast.OrderBy<PXResult<PX.Objects.AR.DiscountSequence, DiscountSequenceDetail>, bool?>((Func<PXResult<PX.Objects.AR.DiscountSequence, DiscountSequenceDetail>, bool?>) (_ => keyIsLast)).GroupBy(_ => new
      {
        DiscountID = PXResult<PX.Objects.AR.DiscountSequence, DiscountSequenceDetail>.op_Implicit(_).DiscountID,
        DiscountSequenceID = PXResult<PX.Objects.AR.DiscountSequence, DiscountSequenceDetail>.op_Implicit(_).DiscountSequenceID
      }, (keyDiscountID, groupDiscountID) =>
      {
        DiscountSequenceDetail discountSequenceDetail = PXResult<PX.Objects.AR.DiscountSequence, DiscountSequenceDetail>.op_Implicit(groupDiscountID.FirstOrDefault<PXResult<PX.Objects.AR.DiscountSequence, DiscountSequenceDetail>>());
        return ((discountSequenceDetail != null ? (!discountSequenceDetail.IsLast.GetValueOrDefault() ? 1 : 0) : 1) != 0 ? (IEnumerable<PXResult<PX.Objects.AR.DiscountSequence, DiscountSequenceDetail>>) groupDiscountID.OrderByDescending<PXResult<PX.Objects.AR.DiscountSequence, DiscountSequenceDetail>, DateTime?>((Func<PXResult<PX.Objects.AR.DiscountSequence, DiscountSequenceDetail>, DateTime?>) (_ => PXResult<PX.Objects.AR.DiscountSequence, DiscountSequenceDetail>.op_Implicit(_).LastDate)) : (IEnumerable<PXResult<PX.Objects.AR.DiscountSequence, DiscountSequenceDetail>>) groupDiscountID.OrderBy<PXResult<PX.Objects.AR.DiscountSequence, DiscountSequenceDetail>, DateTime?>((Func<PXResult<PX.Objects.AR.DiscountSequence, DiscountSequenceDetail>, DateTime?>) (_ => PXResult<PX.Objects.AR.DiscountSequence, DiscountSequenceDetail>.op_Implicit(_).LastDate))).FirstOrDefault<PXResult<PX.Objects.AR.DiscountSequence, DiscountSequenceDetail>>();
      }).OrderByDescending<PXResult<PX.Objects.AR.DiscountSequence, DiscountSequenceDetail>, Decimal?>((Func<PXResult<PX.Objects.AR.DiscountSequence, DiscountSequenceDetail>, Decimal?>) (_ => PXResult<PX.Objects.AR.DiscountSequence, DiscountSequenceDetail>.op_Implicit(_).Discount)))).SelectMany<IOrderedEnumerable<PXResult<PX.Objects.AR.DiscountSequence, DiscountSequenceDetail>>, PXResult<PX.Objects.AR.DiscountSequence, DiscountSequenceDetail>>((Func<IOrderedEnumerable<PXResult<PX.Objects.AR.DiscountSequence, DiscountSequenceDetail>>, IEnumerable<PXResult<PX.Objects.AR.DiscountSequence, DiscountSequenceDetail>>>) (_ => _.ToList<PXResult<PX.Objects.AR.DiscountSequence, DiscountSequenceDetail>>().Where<PXResult<PX.Objects.AR.DiscountSequence, DiscountSequenceDetail>>((Func<PXResult<PX.Objects.AR.DiscountSequence, DiscountSequenceDetail>, bool>) (_ =>
      {
        if (!PXResult<PX.Objects.AR.DiscountSequence, DiscountSequenceDetail>.op_Implicit(_).IsLast.GetValueOrDefault())
          return true;
        if (!PXResult<PX.Objects.AR.DiscountSequence, DiscountSequenceDetail>.op_Implicit(_).Discount.HasValue)
          return false;
        Decimal? discount = PXResult<PX.Objects.AR.DiscountSequence, DiscountSequenceDetail>.op_Implicit(_).Discount;
        Decimal num = 0M;
        return !(discount.GetValueOrDefault() == num & discount.HasValue);
      })))).ToList<PXResult<PX.Objects.AR.DiscountSequence, DiscountSequenceDetail>>();
      if (single)
        discounts.Add(list2.FirstOrDefault<PXResult<PX.Objects.AR.DiscountSequence, DiscountSequenceDetail>>());
      else
        discounts.AddRange((IEnumerable<PXResult<PX.Objects.AR.DiscountSequence, DiscountSequenceDetail>>) list2);
    }
    return discounts;
  }

  protected virtual ImmutableHashSet<DiscountSequenceKey> SelectEntityDiscounts<TTable, TDiscountID, TDiscountSequenceID, TKeyField, TKeyType>(
    TKeyType entityID)
    where TTable : IBqlTable
    where TDiscountID : IBqlField
    where TDiscountSequenceID : IBqlField
    where TKeyField : IBqlField
  {
    ConcurrentDictionary<TKeyType, ImmutableHashSet<DiscountSequenceKey>> entityDiscountsSlot = DiscountEngine.GetEntityDiscountsSlot<TTable, TKeyType>();
    if (!entityDiscountsSlot.ContainsKey(entityID))
    {
      PXView pxView = new PXView((PXGraph) this.ARDiscountSequence.Value, true, (BqlCommand) new Select2<PX.Objects.AR.DiscountSequence, InnerJoin<TTable, On<TDiscountID, Equal<PX.Objects.AR.DiscountSequence.discountID>, And<TDiscountSequenceID, Equal<PX.Objects.AR.DiscountSequence.discountSequenceID>>>>, Where<PX.Objects.AR.DiscountSequence.isActive, Equal<True>, And<TKeyField, Equal<Required<TKeyField>>>>>());
      pxView.Cache.ClearQueryCache();
      entityDiscountsSlot[entityID] = ImmutableHashSet.ToImmutableHashSet<DiscountSequenceKey>(GraphHelper.RowCast<PX.Objects.AR.DiscountSequence>((IEnumerable) pxView.SelectMulti(new object[1]
      {
        (object) entityID
      })).Select<PX.Objects.AR.DiscountSequence, DiscountSequenceKey>((Func<PX.Objects.AR.DiscountSequence, DiscountSequenceKey>) (seq => new DiscountSequenceKey(seq.DiscountID, seq.DiscountSequenceID))));
    }
    return entityDiscountsSlot[entityID];
  }

  protected virtual ImmutableHashSet<DiscountSequenceKey> SelectEntityDiscounts<TTable, TDiscountID, TDiscountSequenceID, TKeyField1, TKeyType1, TKeyField2, TKeyType2>(
    TKeyType1 entityID1,
    TKeyType2 entityID2)
    where TTable : IBqlTable
    where TDiscountID : IBqlField
    where TDiscountSequenceID : IBqlField
    where TKeyField1 : IBqlField
    where TKeyField2 : IBqlField
  {
    ConcurrentDictionary<Tuple<TKeyType1, TKeyType2>, ImmutableHashSet<DiscountSequenceKey>> slot = PXDatabase.GetSlot<ConcurrentDictionary<Tuple<TKeyType1, TKeyType2>, ImmutableHashSet<DiscountSequenceKey>>>(typeof (TTable).Name, new System.Type[1]
    {
      typeof (DummyTable)
    });
    Tuple<TKeyType1, TKeyType2> key = Tuple.Create<TKeyType1, TKeyType2>(entityID1, entityID2);
    if (!slot.ContainsKey(key))
    {
      PXView pxView = new PXView((PXGraph) this.APDiscountSequence.Value, true, (BqlCommand) new Select2<PX.Objects.AR.DiscountSequence, InnerJoin<TTable, On<TDiscountID, Equal<PX.Objects.AR.DiscountSequence.discountID>, And<TDiscountSequenceID, Equal<PX.Objects.AR.DiscountSequence.discountSequenceID>>>>, Where<PX.Objects.AR.DiscountSequence.isActive, Equal<True>, And<TKeyField1, Equal<Required<TKeyField1>>, And<TKeyField2, Equal<Required<TKeyField2>>>>>>());
      pxView.Cache.ClearQueryCache();
      slot[key] = ImmutableHashSet.ToImmutableHashSet<DiscountSequenceKey>(GraphHelper.RowCast<PX.Objects.AR.DiscountSequence>((IEnumerable) pxView.SelectMulti(new object[2]
      {
        (object) key.Item1,
        (object) key.Item2
      })).Select<PX.Objects.AR.DiscountSequence, DiscountSequenceKey>((Func<PX.Objects.AR.DiscountSequence, DiscountSequenceKey>) (seq => new DiscountSequenceKey(seq.DiscountID, seq.DiscountSequenceID))));
    }
    return slot[key];
  }

  protected virtual HashSet<DiscountSequenceKey> SelectDiscountSequences(string discountID)
  {
    return PXDatabase.SelectMulti<PX.Objects.AR.DiscountSequence>(new PXDataField[2]
    {
      (PXDataField) new PXDataField<PX.Objects.AR.DiscountSequence.discountSequenceID>(),
      (PXDataField) new PXDataFieldValue("DiscountID", (PXDbType) 12, (object) discountID)
    }).Select<PXDataRecord, DiscountSequenceKey>((Func<PXDataRecord, DiscountSequenceKey>) (ds => new DiscountSequenceKey(discountID, ds.GetString(0)))).ToHashSet<DiscountSequenceKey>();
  }

  protected virtual PX.Objects.AR.DiscountSequence SelectDiscountSequence(
    PXCache cache,
    string discountID,
    string discountSequenceID)
  {
    PXResult<PX.Objects.AR.DiscountSequence> pxResult = ((IQueryable<PXResult<PX.Objects.AR.DiscountSequence>>) PXSelectBase<PX.Objects.AR.DiscountSequence, PXSelect<PX.Objects.AR.DiscountSequence, Where<PX.Objects.AR.DiscountSequence.discountID, Equal<Required<PX.Objects.AR.DiscountSequence.discountID>>, And<PX.Objects.AR.DiscountSequence.discountSequenceID, Equal<Required<PX.Objects.AR.DiscountSequence.discountSequenceID>>>>>.Config>.Select(cache.Graph, new object[2]
    {
      (object) discountID,
      (object) discountSequenceID
    })).FirstOrDefault<PXResult<PX.Objects.AR.DiscountSequence>>();
    return pxResult == null ? new PX.Objects.AR.DiscountSequence() : PXResult<PX.Objects.AR.DiscountSequence>.op_Implicit(pxResult);
  }

  public virtual Decimal GetDiscountLimit(PXCache cache, int? customerID, int? vendorID = null)
  {
    Decimal x = 100M;
    if (customerID.HasValue)
    {
      CustomerClass customerClass = PXResultset<CustomerClass>.op_Implicit(PXSelectBase<CustomerClass, PXSelectJoin<CustomerClass, LeftJoinSingleTable<Customer, On<Customer.customerClassID, Equal<CustomerClass.customerClassID>>>, Where<Customer.bAccountID, Equal<Required<Customer.bAccountID>>>>.Config>.SelectWindowed(cache.Graph, 0, 1, new object[1]
      {
        (object) customerID
      }));
      if (customerClass != null)
        x = customerClass.DiscountLimit ?? 100M;
    }
    else if (vendorID.HasValue)
      x = (Decimal) Math.Pow((double) x, 3.0);
    return x;
  }

  public virtual Decimal GetTotalGroupAndDocumentDiscount<TDiscountDetail>(
    PXSelectBase<TDiscountDetail> discountDetails,
    bool docOnly = false)
    where TDiscountDetail : class, IBqlTable, IDiscountDetail, new()
  {
    return GraphHelper.RowCast<TDiscountDetail>((IEnumerable) discountDetails.Select(Array.Empty<object>())).Where<TDiscountDetail>((Func<TDiscountDetail, bool>) (r =>
    {
      if (r.SkipDiscount.GetValueOrDefault())
        return false;
      return docOnly && r.Type == "D" || !docOnly;
    })).Select<TDiscountDetail, Decimal>((Func<TDiscountDetail, Decimal>) (r => r.CuryDiscountAmt.GetValueOrDefault())).Sum();
  }

  public virtual (Decimal groupDiscountTotal, Decimal documentDiscountTotal, Decimal discountTotal) GetDiscountTotals<TDiscountDetail>(
    PXSelectBase<TDiscountDetail> discountDetails)
    where TDiscountDetail : class, IBqlTable, IDiscountDetail, new()
  {
    (Decimal, Decimal, Decimal) discountTotals = (0M, 0M, 0M);
    foreach (PXResult<TDiscountDetail> pxResult in discountDetails.Select(Array.Empty<object>()))
    {
      TDiscountDetail discountDetail = PXResult<TDiscountDetail>.op_Implicit(pxResult);
      if (!discountDetail.SkipDiscount.GetValueOrDefault())
      {
        switch (discountDetail.Type)
        {
          case "G":
            discountTotals.Item1 += discountDetail.CuryDiscountAmt.GetValueOrDefault();
            break;
          case "D":
          case "B":
            discountTotals.Item2 += discountDetail.CuryDiscountAmt.GetValueOrDefault();
            break;
        }
        discountTotals.Item3 += discountDetail.CuryDiscountAmt.GetValueOrDefault();
      }
    }
    return discountTotals;
  }

  protected virtual ARSalesPriceMaint.SalesPriceItem GetSalesPrice(
    PXCache cache,
    int? inventoryID,
    int? siteID,
    int? customerID,
    string customerPriceClassID,
    string curyID,
    string UOM,
    Decimal? quantity,
    DateTime date,
    bool isBaseQty)
  {
    if (isBaseQty)
      quantity = new Decimal?(INUnitAttribute.ConvertFromBase(cache, inventoryID, UOM, (Decimal) (int) quantity.Value, INPrecision.QUANTITY));
    return ARSalesPriceMaint.SingleARSalesPriceMaint.FindSalesPrice(cache, customerPriceClassID, customerID, inventoryID, siteID, curyID, curyID, quantity, UOM, date, false);
  }

  public static void SetLineDiscountOnly<TLine>(
    PXCache cache,
    TLine line,
    DiscountLineFields dLine,
    string discountID,
    Decimal? unitPrice,
    Decimal? extPrice,
    Decimal? qty,
    int? locationID,
    int? customerID,
    string curyID,
    DateTime? date,
    int? branchID = null,
    int? inventoryID = null,
    bool needDiscountID = true)
    where TLine : class, IBqlTable, new()
  {
    DiscountEngineProvider.GetEngineFor<TLine, DiscountEngine.NoDiscountDetail>().SetLineDiscountOnlyImpl(cache, (object) line, dLine, discountID, unitPrice, extPrice, qty, locationID, customerID, curyID, date, branchID, inventoryID, needDiscountID);
  }

  public static Decimal GetLineDiscountOnly<TLine>(
    PXCache cache,
    TLine line,
    DiscountLineFields dLine,
    Decimal? unitPrice,
    Decimal? extPrice,
    Decimal? qty,
    int? locationID,
    int? customerID,
    string curyID,
    DateTime? date,
    int? branchID = null,
    int? inventoryID = null,
    bool needDiscountID = true)
    where TLine : class, IBqlTable, new()
  {
    return DiscountEngineProvider.GetEngineFor<TLine, DiscountEngine.NoDiscountDetail>().GetLineDiscountOnlyImpl(cache, (object) line, dLine, unitPrice, extPrice, qty, locationID, customerID, curyID, date, branchID, inventoryID, needDiscountID);
  }

  protected abstract void SetLineDiscountOnlyImpl(
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
    bool needDiscountID);

  protected abstract Decimal GetLineDiscountOnlyImpl(
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
    bool needDiscountID);

  public static bool ApplyQuantityDiscountByBaseUOMForAP(PXGraph graph)
  {
    return DiscountEngine.ApplyQuantityDiscountByBaseUOM(graph).ForAP;
  }

  public static bool ApplyQuantityDiscountByBaseUOMForAR(PXGraph graph)
  {
    return DiscountEngine.ApplyQuantityDiscountByBaseUOM(graph).ForAR;
  }

  internal static DiscountEngine.ApplyQuantityDiscountByBaseUOMOption ApplyQuantityDiscountByBaseUOM(
    PXGraph graph)
  {
    return new DiscountEngine.ApplyQuantityDiscountByBaseUOMOption(DiscountEngine.FeatureInstalled<FeaturesSet.vendorDiscounts>(graph) && CurrentConfiguration.ActualAPSetup.ApplyQuantityDiscountBy == "B", DiscountEngine.FeatureInstalled<FeaturesSet.customerDiscounts>(graph) && CurrentConfiguration.ActualARSetup.ApplyQuantityDiscountBy == "B");
  }

  protected static Decimal RoundDiscountRate(Decimal discountRate)
  {
    return Math.Round(discountRate, 18, MidpointRounding.AwayFromZero);
  }

  [PXHidden]
  public class DiscountFeatures : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXDBBool]
    public virtual bool? CustomerDiscounts { get; set; }

    [PXDBBool]
    public virtual bool? VendorDiscounts { get; set; }

    [PXDBBool]
    public virtual bool? MatrixItem { get; set; }

    public abstract class customerDiscounts : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      DiscountEngine.DiscountFeatures.customerDiscounts>
    {
    }

    public abstract class vendorDiscounts : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      DiscountEngine.DiscountFeatures.vendorDiscounts>
    {
    }

    public abstract class matrixItem : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      DiscountEngine.DiscountFeatures.matrixItem>
    {
    }
  }

  protected class DCCache : IPrefetchable, IPXCompanyDependent
  {
    public ConcurrentDictionary<string, DiscountCode> cachedDiscountCodes { get; private set; }

    public void Prefetch()
    {
      this.cachedDiscountCodes = new ConcurrentDictionary<string, DiscountCode>();
      this.StoreARDiscounts();
      this.StoreAPDiscounts();
    }

    private void StoreARDiscounts()
    {
      foreach (PXDataRecord pxDataRecord in PXDatabase.SelectMulti<ARDiscount>(new PXDataField[7]
      {
        (PXDataField) new PXDataField<ARDiscount.discountID>(),
        (PXDataField) new PXDataField<ARDiscount.type>(),
        (PXDataField) new PXDataField<ARDiscount.isManual>(),
        (PXDataField) new PXDataField<ARDiscount.isAppliedToDR>(),
        (PXDataField) new PXDataField<ARDiscount.excludeFromDiscountableAmt>(),
        (PXDataField) new PXDataField<ARDiscount.skipDocumentDiscounts>(),
        (PXDataField) new PXDataField<ARDiscount.applicableTo>()
      }))
      {
        DiscountCode discountCode = new DiscountCode()
        {
          IsVendorDiscount = false,
          Type = pxDataRecord.GetString(1),
          IsManual = pxDataRecord.GetBoolean(2).Value,
          IsAppliedToDR = pxDataRecord.GetBoolean(3).Value,
          ExcludeFromDiscountableAmt = pxDataRecord.GetBoolean(4).Value,
          SkipDocumentDiscounts = pxDataRecord.GetBoolean(5).Value,
          ApplicableToEnum = DiscountEngine.SetApplicableToCombination(pxDataRecord.GetString(6))
        };
        this.cachedDiscountCodes.GetOrAdd(pxDataRecord.GetString(0), discountCode);
      }
    }

    private void StoreAPDiscounts()
    {
      foreach (PXDataRecord pxDataRecord in PXDatabase.SelectMulti<APDiscount>(new PXDataField[7]
      {
        (PXDataField) new PXDataField<APDiscount.bAccountID>(),
        (PXDataField) new PXDataField<APDiscount.discountID>(),
        (PXDataField) new PXDataField<APDiscount.type>(),
        (PXDataField) new PXDataField<APDiscount.isManual>(),
        (PXDataField) new PXDataField<APDiscount.excludeFromDiscountableAmt>(),
        (PXDataField) new PXDataField<APDiscount.skipDocumentDiscounts>(),
        (PXDataField) new PXDataField<APDiscount.applicableTo>()
      }))
      {
        DiscountCode discountCode = new DiscountCode()
        {
          IsVendorDiscount = true,
          VendorID = pxDataRecord.GetInt32(0),
          Type = pxDataRecord.GetString(2),
          IsManual = pxDataRecord.GetBoolean(3).Value,
          ExcludeFromDiscountableAmt = pxDataRecord.GetBoolean(4).Value,
          SkipDocumentDiscounts = pxDataRecord.GetBoolean(5).Value,
          ApplicableToEnum = DiscountEngine.SetApplicableToCombination(pxDataRecord.GetString(6))
        };
        this.cachedDiscountCodes.GetOrAdd(pxDataRecord.GetString(1), discountCode);
      }
    }
  }

  protected class DECache : IPrefetchable, IPXCompanyDependent
  {
    public void Prefetch() => DiscountEngine.DECache.ClearAllEntityCaches();

    private static void ClearAllEntityCaches()
    {
      DiscountEngine.SelectUnconditionalDiscounts(true);
      DiscountEngine.DECache.ClearAllEntityDiscounts<DiscountCustomer, int>();
      DiscountEngine.DECache.ClearAllEntityDiscounts<DiscountItem, int>();
      DiscountEngine.DECache.ClearAllEntityDiscounts<DiscountCustomerPriceClass, string>();
      DiscountEngine.DECache.ClearAllEntityDiscounts<DiscountInventoryPriceClass, string>();
      DiscountEngine.DECache.ClearAllEntityDiscounts<DiscountBranch, int>();
      DiscountEngine.DECache.ClearAllEntityDiscounts<DiscountSite, int>();
      DiscountEngine.DECache.ClearAllEntityDiscounts<APDiscountVendor, int>();
      DiscountEngine.DECache.ClearAllEntityDiscountsTwoKeys<APDiscountLocation, int, int>();
    }

    private static void ClearEntityCaches(
      PXCache cache,
      string discountID,
      string discountSequenceID,
      string applicableTo,
      int? vendorID = null)
    {
      DiscountEngine.SelectUnconditionalDiscounts(true);
      ApplicableToCombination combination = DiscountEngine.SetApplicableToCombination(applicableTo);
      if (combination.HasFlag((Enum) ApplicableToCombination.Customer))
        DiscountEngine.DECache.ClearDiscountCustomers(cache.Graph, discountID, discountSequenceID);
      if (combination.HasFlag((Enum) ApplicableToCombination.InventoryItem))
        DiscountEngine.DECache.ClearDiscountItems(cache.Graph, discountID, discountSequenceID);
      if (combination.HasFlag((Enum) ApplicableToCombination.CustomerPriceClass))
        DiscountEngine.DECache.ClearDiscountCustomerPriceClasses(cache.Graph, discountID, discountSequenceID);
      if (combination.HasFlag((Enum) ApplicableToCombination.InventoryPriceClass))
        DiscountEngine.DECache.ClearDiscountInventoryPriceClasses(cache.Graph, discountID, discountSequenceID);
      if (combination.HasFlag((Enum) ApplicableToCombination.Branch))
        DiscountEngine.DECache.ClearDiscountBranches(cache.Graph, discountID, discountSequenceID);
      if (combination.HasFlag((Enum) ApplicableToCombination.Warehouse))
        DiscountEngine.DECache.ClearDiscountSites(cache.Graph, discountID, discountSequenceID);
      if (vendorID.HasValue && combination.HasFlag((Enum) ApplicableToCombination.Vendor))
        DiscountEngine.DECache.ClearDiscountVendors(cache.Graph, discountID, discountSequenceID);
      if (!vendorID.HasValue || !combination.HasFlag((Enum) ApplicableToCombination.Location))
        return;
      DiscountEngine.DECache.ClearDiscountLocations(cache.Graph, discountID, discountSequenceID);
    }

    private static void ClearDiscountLocations(
      PXGraph graph,
      string discountID,
      string discountSequenceID)
    {
      foreach (PXResult<APDiscountLocation> pxResult in PXSelectBase<APDiscountLocation, PXSelect<APDiscountLocation, Where<APDiscountLocation.discountID, Equal<Required<APDiscountLocation.discountID>>, And<APDiscountLocation.discountSequenceID, Equal<Required<APDiscountLocation.discountSequenceID>>>>>.Config>.Select(graph, new object[2]
      {
        (object) discountID,
        (object) discountSequenceID
      }))
      {
        APDiscountLocation discountLocation = PXResult<APDiscountLocation>.op_Implicit(pxResult);
        DiscountEngine.DECache.ClearEntityDiscountsTwoKeys<APDiscountLocation, int, int>(discountLocation.VendorID.Value, discountLocation.LocationID.Value);
      }
    }

    private static void ClearDiscountVendors(
      PXGraph graph,
      string discountID,
      string discountSequenceID)
    {
      foreach (PXResult<APDiscountVendor> pxResult in PXSelectBase<APDiscountVendor, PXSelect<APDiscountVendor, Where<APDiscountVendor.discountID, Equal<Required<APDiscountVendor.discountID>>, And<APDiscountVendor.discountSequenceID, Equal<Required<APDiscountVendor.discountSequenceID>>>>>.Config>.Select(graph, new object[2]
      {
        (object) discountID,
        (object) discountSequenceID
      }))
        DiscountEngine.DECache.ClearEntityDiscounts<APDiscountVendor, int>(PXResult<APDiscountVendor>.op_Implicit(pxResult).VendorID.Value);
    }

    private static void ClearDiscountSites(
      PXGraph graph,
      string discountID,
      string discountSequenceID)
    {
      foreach (PXResult<DiscountSite> pxResult in PXSelectBase<DiscountSite, PXSelect<DiscountSite, Where<DiscountSite.discountID, Equal<Required<DiscountSite.discountID>>, And<DiscountSite.discountSequenceID, Equal<Required<DiscountSite.discountSequenceID>>>>>.Config>.Select(graph, new object[2]
      {
        (object) discountID,
        (object) discountSequenceID
      }))
        DiscountEngine.DECache.ClearEntityDiscounts<DiscountSite, int>(PXResult<DiscountSite>.op_Implicit(pxResult).SiteID.Value);
    }

    private static void ClearDiscountBranches(
      PXGraph graph,
      string discountID,
      string discountSequenceID)
    {
      foreach (PXResult<DiscountBranch> pxResult in PXSelectBase<DiscountBranch, PXSelect<DiscountBranch, Where<DiscountBranch.discountID, Equal<Required<DiscountBranch.discountID>>, And<DiscountBranch.discountSequenceID, Equal<Required<DiscountBranch.discountSequenceID>>>>>.Config>.Select(graph, new object[2]
      {
        (object) discountID,
        (object) discountSequenceID
      }))
        DiscountEngine.DECache.ClearEntityDiscounts<DiscountBranch, int>(PXResult<DiscountBranch>.op_Implicit(pxResult).BranchID.Value);
    }

    private static void ClearDiscountInventoryPriceClasses(
      PXGraph graph,
      string discountID,
      string discountSequenceID)
    {
      foreach (PXResult<DiscountInventoryPriceClass> pxResult in PXSelectBase<DiscountInventoryPriceClass, PXSelect<DiscountInventoryPriceClass, Where<DiscountInventoryPriceClass.discountID, Equal<Required<DiscountInventoryPriceClass.discountID>>, And<DiscountInventoryPriceClass.discountSequenceID, Equal<Required<DiscountInventoryPriceClass.discountSequenceID>>>>>.Config>.Select(graph, new object[2]
      {
        (object) discountID,
        (object) discountSequenceID
      }))
        DiscountEngine.DECache.ClearEntityDiscounts<DiscountInventoryPriceClass, string>(PXResult<DiscountInventoryPriceClass>.op_Implicit(pxResult).InventoryPriceClassID);
    }

    private static void ClearDiscountCustomerPriceClasses(
      PXGraph graph,
      string discountID,
      string discountSequenceID)
    {
      foreach (PXResult<DiscountCustomerPriceClass> pxResult in PXSelectBase<DiscountCustomerPriceClass, PXSelect<DiscountCustomerPriceClass, Where<DiscountCustomerPriceClass.discountID, Equal<Required<DiscountCustomerPriceClass.discountID>>, And<DiscountCustomerPriceClass.discountSequenceID, Equal<Required<DiscountCustomerPriceClass.discountSequenceID>>>>>.Config>.Select(graph, new object[2]
      {
        (object) discountID,
        (object) discountSequenceID
      }))
        DiscountEngine.DECache.ClearEntityDiscounts<DiscountCustomerPriceClass, string>(PXResult<DiscountCustomerPriceClass>.op_Implicit(pxResult).CustomerPriceClassID);
    }

    private static void ClearDiscountItems(
      PXGraph graph,
      string discountID,
      string discountSequenceID)
    {
      foreach (PXResult<DiscountItem> pxResult in PXSelectBase<DiscountItem, PXSelect<DiscountItem, Where<DiscountItem.discountID, Equal<Required<DiscountItem.discountID>>, And<DiscountItem.discountSequenceID, Equal<Required<DiscountItem.discountSequenceID>>>>>.Config>.Select(graph, new object[2]
      {
        (object) discountID,
        (object) discountSequenceID
      }))
        DiscountEngine.DECache.ClearEntityDiscounts<DiscountItem, int>(PXResult<DiscountItem>.op_Implicit(pxResult).InventoryID.Value);
    }

    private static void ClearDiscountCustomers(
      PXGraph graph,
      string discountID,
      string discountSequenceID)
    {
      foreach (PXResult<DiscountCustomer> pxResult in PXSelectBase<DiscountCustomer, PXSelect<DiscountCustomer, Where<DiscountCustomer.discountID, Equal<Required<DiscountCustomer.discountID>>, And<DiscountCustomer.discountSequenceID, Equal<Required<DiscountCustomer.discountSequenceID>>>>>.Config>.Select(graph, new object[2]
      {
        (object) discountID,
        (object) discountSequenceID
      }))
        DiscountEngine.DECache.ClearEntityDiscounts<DiscountCustomer, int>(PXResult<DiscountCustomer>.op_Implicit(pxResult).CustomerID.Value);
    }

    private static void ClearAllEntityDiscounts<TTable, TEntityType>() where TTable : IBqlTable
    {
      DiscountEngine.GetEntityDiscountsSlot<TTable, TEntityType>().Clear();
    }

    private static void ClearAllEntityDiscountsTwoKeys<TTable, TEntityType1, TEntityType2>() where TTable : IBqlTable
    {
      PXDatabase.GetSlot<ConcurrentDictionary<Tuple<TEntityType1, TEntityType2>, ImmutableHashSet<DiscountSequenceKey>>>(typeof (TTable).Name, new System.Type[1]
      {
        typeof (DummyTable)
      }).Clear();
    }

    private static void ClearEntityDiscounts<TTable, TEntityType>(TEntityType entityID) where TTable : IBqlTable
    {
      ConcurrentDictionary<TEntityType, ImmutableHashSet<DiscountSequenceKey>> entityDiscountsSlot = DiscountEngine.GetEntityDiscountsSlot<TTable, TEntityType>();
      if (!entityDiscountsSlot.ContainsKey(entityID))
        return;
      entityDiscountsSlot.Remove<TEntityType, ImmutableHashSet<DiscountSequenceKey>>(entityID);
    }

    private static void ClearEntityDiscountsTwoKeys<TTable, TEntityType1, TEntityType2>(
      TEntityType1 entityID1,
      TEntityType2 entityID2)
      where TTable : IBqlTable
    {
      ConcurrentDictionary<Tuple<TEntityType1, TEntityType2>, ImmutableHashSet<DiscountSequenceKey>> slot = PXDatabase.GetSlot<ConcurrentDictionary<Tuple<TEntityType1, TEntityType2>, ImmutableHashSet<DiscountSequenceKey>>>(typeof (TTable).Name, new System.Type[1]
      {
        typeof (DummyTable)
      });
      Tuple<TEntityType1, TEntityType2> key = Tuple.Create<TEntityType1, TEntityType2>(entityID1, entityID2);
      if (!slot.ContainsKey(key))
        return;
      slot.Remove<Tuple<TEntityType1, TEntityType2>, ImmutableHashSet<DiscountSequenceKey>>(key);
    }
  }

  /// <summary>
  /// Fake discount detail table, used for calling DiscountEngine methods that do not need DiscountDetail
  /// </summary>
  [PXHidden]
  private class NoDiscountDetail : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage, IDiscountDetail
  {
    public int? RecordID { get; set; }

    public ushort? LineNbr { get; set; }

    public bool? SkipDiscount { get; set; }

    public string DiscountID { get; set; }

    public string DiscountSequenceID { get; set; }

    public string Type { get; set; }

    public Decimal? CuryDiscountableAmt { get; set; }

    public Decimal? DiscountableQty { get; set; }

    public Decimal? CuryDiscountAmt { get; set; }

    public Decimal? DiscountPct { get; set; }

    public int? FreeItemID { get; set; }

    public Decimal? FreeItemQty { get; set; }

    public bool? IsManual { get; set; }

    public bool? IsOrigDocDiscount { get; set; }

    public string ExtDiscCode { get; set; }

    public string Description { get; set; }
  }

  internal struct ApplyQuantityDiscountByBaseUOMOption(bool forAP, bool forAR)
  {
    public readonly bool ForAP = forAP;
    public readonly bool ForAR = forAR;
  }

  [Flags]
  public enum DiscountCalculationOptions
  {
    [Description("Calculate all Automatic discounts. Default state: set.")] CalculateAll = 0,
    [Description("Set this flag to disable free-item discounts. Default state: not set.")] DisableFreeItemDiscountsCalculation = 1,
    [Description("Set this flag to disable automatic Group and Document discounts calculation. Default state: not set.")] DisableGroupAndDocumentDiscounts = 2,
    [Description("Set this flag to disable automatic discount calculation. Discounts, that already present in the document and marked as Manual, will be kept in a valid state. Overrides DisableGroupAndDocumentDiscounts option. Default state: not set.")] DisableAllAutomaticDiscounts = 4,
    [Description("Set this flag to disable AR discount calculation. Default state: not set.")] DisableARDiscountsCalculation = 8,
    [Description("Set this flag to disable AP discount calculation. Default state: not set.")] DisableAPDiscountsCalculation = 16, // 0x00000010
    [Description("Set this flag to disable price/cost calculation. Default state: set for AP")] DisablePriceCalculation = 32, // 0x00000020
    [Description("Set this flag to enable automatic discount calculation on import. Default state: not set")] CalculateDiscountsFromImport = 64, // 0x00000040
    [Description("Set this flag to enable optimization of Group and Document discount calculation procedure. Default state: not set"), Obsolete("Flag will be removed in future versions")] EnableOptimizationOfGroupAndDocumentDiscountsCalculation = 128, // 0x00000080
    [Description("Set this flag to disable all document discount recalculation except for External Document Discounts")] CalculateExternalDocumentDiscountsOnly = 256, // 0x00000100
    [Description("Set this flag to enable calculation of automatic Line discounts when AutomaticDiscountsDisabled is enabled. Default state: not set")] ExplicitlyAllowToCalculateAutomaticLineDiscounts = 512, // 0x00000200
    [Description("Set this flag to disable automatic discount calculation. Discounts, that came from SOOrder, will be kept in a valid state. Default state: not set.")] DisableOrigAutomaticDiscounts = 1024, // 0x00000400
    [Description("Set this flag to force formula recalculation when updating group or document discount rates. Default state: not set")] ForceFormulaRecalculation = 2048, // 0x00000800
  }
}
