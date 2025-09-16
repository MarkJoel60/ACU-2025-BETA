// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOPackageEngine
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using PX.Objects.IN;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

#nullable disable
namespace PX.Objects.SO;

public class SOPackageEngine
{
  protected PXGraph graph;

  public SOPackageEngine(PXGraph graph)
  {
    this.graph = graph != null ? graph : throw new ArgumentNullException(nameof (graph));
  }

  public virtual IList<SOPackageEngine.PackSet> Pack(SOPackageEngine.OrderInfo orderInfo)
  {
    Stopwatch stopwatch = new Stopwatch();
    stopwatch.Start();
    Dictionary<int, List<INItemBoxEx>> boxesByInventoryLookup = new Dictionary<int, List<INItemBoxEx>>();
    foreach (SOPackageEngine.ItemInfo line in (IEnumerable<SOPackageEngine.ItemInfo>) orderInfo.Lines)
      boxesByInventoryLookup.Add(line.InventoryID, this.GetBoxesByInventoryID(line.InventoryID, orderInfo.CarrierID));
    Dictionary<int, SOPackageEngine.PackSet> dictionary1 = new Dictionary<int, SOPackageEngine.PackSet>();
    foreach (SOPackageEngine.ItemStats stat in orderInfo.Stats)
    {
      Dictionary<int, SOPackageEngine.PackSet> dictionary2 = dictionary1;
      int? nullable = stat.SiteID;
      int key1 = nullable.Value;
      SOPackageEngine.PackSet packSet;
      if (dictionary2.ContainsKey(key1))
      {
        Dictionary<int, SOPackageEngine.PackSet> dictionary3 = dictionary1;
        nullable = stat.SiteID;
        int key2 = nullable.Value;
        packSet = dictionary3[key2];
      }
      else
      {
        nullable = stat.SiteID;
        packSet = new SOPackageEngine.PackSet(nullable.Value);
        dictionary1.Add(packSet.SiteID, packSet);
      }
      if (stat.PackOption == "Q")
      {
        nullable = stat.InventoryID;
        int num = 0;
        if (nullable.GetValueOrDefault() == num & nullable.HasValue)
          throw new PXException("The {0} item has By Quantity selected in the Packaging Option box and the Pack Separately check box cleared. Contact your system administrator.", new object[1]
          {
            (object) PX.Objects.IN.InventoryItem.PK.Find(this.graph, new int?(stat.Lines.FirstOrDefault<SOPackageEngine.ItemInfo>().InventoryID)).InventoryCD
          });
        Dictionary<int, List<INItemBoxEx>> dictionary4 = boxesByInventoryLookup;
        nullable = stat.InventoryID;
        int key3 = nullable.Value;
        List<INItemBoxEx> boxes = dictionary4[key3];
        packSet.Packages.AddRange((IEnumerable<SOPackageInfoEx>) this.PackByQty(boxes, stat));
      }
      if (stat.PackOption == "W" || stat.PackOption == "V")
      {
        nullable = stat.InventoryID;
        int num = 0;
        if (nullable.GetValueOrDefault() == num & nullable.HasValue)
        {
          Dictionary<string, INItemBoxEx> dictionary5 = new Dictionary<string, INItemBoxEx>();
          Dictionary<string, List<int>> boxItemsLookup = new Dictionary<string, List<int>>();
          foreach (SOPackageEngine.ItemInfo line in (IEnumerable<SOPackageEngine.ItemInfo>) stat.Lines)
          {
            foreach (INItemBoxEx inItemBoxEx in boxesByInventoryLookup[line.InventoryID])
            {
              if (!dictionary5.ContainsKey(inItemBoxEx.BoxID))
                dictionary5.Add(inItemBoxEx.BoxID, inItemBoxEx);
              if (!boxItemsLookup.ContainsKey(inItemBoxEx.BoxID))
                boxItemsLookup.Add(inItemBoxEx.BoxID, new List<int>());
              boxItemsLookup[inItemBoxEx.BoxID].Add(line.InventoryID);
            }
          }
          packSet.Packages.AddRange((IEnumerable<SOPackageInfoEx>) this.PackByWeightMixedItems(new List<INItemBoxEx>((IEnumerable<INItemBoxEx>) dictionary5.Values), new List<SOPackageEngine.ItemInfo>((IEnumerable<SOPackageEngine.ItemInfo>) stat.Lines), boxesByInventoryLookup, boxItemsLookup, stat.PackOption == "V", stat.SiteID, new Decimal?(stat.DeclaredValue)));
        }
        else
        {
          Dictionary<int, List<INItemBoxEx>> dictionary6 = boxesByInventoryLookup;
          nullable = stat.InventoryID;
          int key4 = nullable.Value;
          List<INItemBoxEx> boxes = dictionary6[key4];
          packSet.Packages.AddRange((IEnumerable<SOPackageInfoEx>) this.PackByWeight(boxes, stat));
        }
      }
    }
    stopwatch.Stop();
    return (IList<SOPackageEngine.PackSet>) dictionary1.Values.ToList<SOPackageEngine.PackSet>();
  }

  public virtual List<SOPackageInfoEx> PackByQty(
    List<INItemBoxEx> boxes,
    SOPackageEngine.ItemStats stats)
  {
    List<SOPackageInfoEx> soPackageInfoExList = new List<SOPackageInfoEx>();
    foreach (SOPackageEngine.BoxInfo boxInfo in this.PackByQty(boxes, stats.BaseQty))
    {
      SOPackageInfoEx soPackageInfoEx1 = new SOPackageInfoEx();
      soPackageInfoEx1.SiteID = stats.SiteID;
      soPackageInfoEx1.BoxID = boxInfo.Box.BoxID;
      soPackageInfoEx1.InventoryID = stats.InventoryID;
      soPackageInfoEx1.Operation = stats.Operation;
      soPackageInfoEx1.DeclaredValue = new Decimal?(stats.DeclaredValue / stats.BaseQty * boxInfo.Value);
      soPackageInfoEx1.Weight = new Decimal?(Decimal.Round(stats.BaseWeight / stats.BaseQty * boxInfo.Value, 4));
      soPackageInfoEx1.Qty = new Decimal?(boxInfo.Value);
      soPackageInfoEx1.Length = boxInfo.Box.Length;
      soPackageInfoEx1.Width = boxInfo.Box.Width;
      soPackageInfoEx1.Height = boxInfo.Box.Height;
      soPackageInfoEx1.BoxWeight = boxInfo.Box.BoxWeight;
      SOPackageInfoEx soPackageInfoEx2 = soPackageInfoEx1;
      Decimal? weight = soPackageInfoEx1.Weight;
      Decimal? boxWeight = soPackageInfoEx1.BoxWeight;
      Decimal? nullable = weight.HasValue & boxWeight.HasValue ? new Decimal?(weight.GetValueOrDefault() + boxWeight.GetValueOrDefault()) : new Decimal?();
      soPackageInfoEx2.GrossWeight = nullable;
      soPackageInfoExList.Add(soPackageInfoEx1);
    }
    return soPackageInfoExList;
  }

  public virtual List<SOPackageInfoEx> PackByWeight(
    List<INItemBoxEx> boxes,
    SOPackageEngine.ItemStats stats)
  {
    List<SOPackageInfoEx> soPackageInfoExList = new List<SOPackageInfoEx>();
    foreach (SOPackageEngine.BoxInfo boxInfo in this.PackByWeight(boxes, stats.BaseWeight, stats.BaseQty, stats.InventoryID.Value))
    {
      SOPackageInfoEx soPackageInfoEx1 = new SOPackageInfoEx();
      soPackageInfoEx1.SiteID = stats.SiteID;
      soPackageInfoEx1.BoxID = boxInfo.Box.BoxID;
      soPackageInfoEx1.InventoryID = stats.InventoryID;
      soPackageInfoEx1.Operation = stats.Operation;
      soPackageInfoEx1.DeclaredValue = new Decimal?(stats.DeclaredValue / stats.BaseWeight * boxInfo.Value);
      soPackageInfoEx1.Weight = new Decimal?(Decimal.Round(boxInfo.Value, 4));
      soPackageInfoEx1.Length = boxInfo.Box.Length;
      soPackageInfoEx1.Width = boxInfo.Box.Width;
      soPackageInfoEx1.Height = boxInfo.Box.Height;
      soPackageInfoEx1.BoxWeight = boxInfo.Box.BoxWeight;
      SOPackageInfoEx soPackageInfoEx2 = soPackageInfoEx1;
      Decimal? weight = soPackageInfoEx1.Weight;
      Decimal? boxWeight = soPackageInfoEx1.BoxWeight;
      Decimal? nullable = weight.HasValue & boxWeight.HasValue ? new Decimal?(weight.GetValueOrDefault() + boxWeight.GetValueOrDefault()) : new Decimal?();
      soPackageInfoEx2.GrossWeight = nullable;
      soPackageInfoExList.Add(soPackageInfoEx1);
    }
    return soPackageInfoExList;
  }

  public virtual List<SOPackageInfoEx> PackByWeightMixedItems(
    List<INItemBoxEx> boxes,
    List<SOPackageEngine.ItemInfo> items,
    Dictionary<int, List<INItemBoxEx>> boxesByInventoryLookup,
    Dictionary<string, List<int>> boxItemsLookup,
    bool restrictByVolume,
    int? siteID,
    Decimal? declaredValue)
  {
    HashSet<int> intSet = new HashSet<int>();
    foreach (SOPackageEngine.ItemInfo itemInfo in items)
    {
      if (itemInfo.Qty % 1M != 0M)
        intSet.Add(itemInfo.InventoryID);
    }
    List<SOPackageEngine.advancedINItemBoxEx> advancedInItemBoxExList = new List<SOPackageEngine.advancedINItemBoxEx>();
    foreach (KeyValuePair<int, List<INItemBoxEx>> keyValuePair in boxesByInventoryLookup)
      keyValuePair.Value.Sort((Comparison<INItemBoxEx>) ((x, y) =>
      {
        int num = -1 * Decimal.Compare(x.MaxNetWeight, y.MaxNetWeight);
        return num == 0 ? -1 * Decimal.Compare(x.MaxVolume.GetValueOrDefault(), y.MaxVolume.GetValueOrDefault()) : num;
      }));
    items.Sort((Comparison<SOPackageEngine.ItemInfo>) ((x, y) =>
    {
      Decimal? unitWeight = x.UnitWeight;
      Decimal valueOrDefault1 = unitWeight.GetValueOrDefault();
      unitWeight = y.UnitWeight;
      Decimal valueOrDefault2 = unitWeight.GetValueOrDefault();
      return -1 * Decimal.Compare(valueOrDefault1, valueOrDefault2);
    }));
    boxes.Sort((Comparison<INItemBoxEx>) ((x, y) =>
    {
      int num = -1 * Decimal.Compare(x.MaxNetWeight, y.MaxNetWeight);
      return num == 0 ? -1 * Decimal.Compare(x.MaxVolume.GetValueOrDefault(), y.MaxVolume.GetValueOrDefault()) : num;
    }));
    if (restrictByVolume)
    {
      foreach (SOPackageEngine.ItemInfo itemInfo in items)
      {
        INItemBoxEx inItemBoxEx1 = boxesByInventoryLookup[itemInfo.InventoryID].ToArray()[0];
        Decimal totalWeight = itemInfo.TotalWeight;
        Decimal totalVolume = itemInfo.TotalVolume;
        Decimal? nullable1 = itemInfo.UnitWeight;
        Decimal maxNetWeight1 = inItemBoxEx1.MaxNetWeight;
        if (!(nullable1.GetValueOrDefault() > maxNetWeight1 & nullable1.HasValue))
        {
          nullable1 = itemInfo.UnitVolume;
          Decimal? nullable2 = inItemBoxEx1.MaxVolume;
          if (nullable1.GetValueOrDefault() > nullable2.GetValueOrDefault() & nullable1.HasValue & nullable2.HasValue)
          {
            nullable2 = inItemBoxEx1.MaxVolume;
            Decimal num = 0M;
            if (nullable2.GetValueOrDefault() > num & nullable2.HasValue)
              goto label_17;
          }
          foreach (SOPackageEngine.advancedINItemBoxEx advancedInItemBoxEx1 in advancedInItemBoxExList)
          {
            foreach (INItemBoxEx inItemBoxEx2 in boxesByInventoryLookup[itemInfo.InventoryID].ToArray())
            {
              if (advancedInItemBoxEx1.BoxID == inItemBoxEx2.BoxID && totalWeight > 0M)
              {
                Decimal emptyBoxWeight1 = advancedInItemBoxEx1.EmptyBoxWeight;
                nullable2 = itemInfo.UnitWeight;
                Decimal valueOrDefault3 = nullable2.GetValueOrDefault();
                if (emptyBoxWeight1 >= valueOrDefault3 & nullable2.HasValue)
                {
                  Decimal emptyBoxVolume1 = advancedInItemBoxEx1.EmptyBoxVolume;
                  nullable2 = itemInfo.UnitVolume;
                  Decimal valueOrDefault4 = nullable2.GetValueOrDefault();
                  if (emptyBoxVolume1 >= valueOrDefault4 & nullable2.HasValue)
                  {
                    Decimal num1;
                    if (advancedInItemBoxEx1.EmptyBoxWeight > totalWeight)
                    {
                      nullable2 = itemInfo.UnitWeight;
                      Decimal num2 = 0M;
                      if (nullable2.GetValueOrDefault() > num2 & nullable2.HasValue)
                      {
                        Decimal num3;
                        if (!intSet.Contains(itemInfo.InventoryID))
                        {
                          Decimal num4 = totalWeight;
                          nullable2 = itemInfo.UnitWeight;
                          Decimal num5 = nullable2.Value;
                          num3 = Math.Floor(num4 / num5);
                        }
                        else
                        {
                          Decimal num6 = totalWeight;
                          nullable2 = itemInfo.UnitWeight;
                          Decimal num7 = nullable2.Value;
                          num3 = num6 / num7;
                        }
                        num1 = num3;
                      }
                      else
                        num1 = intSet.Contains(itemInfo.InventoryID) ? itemInfo.Qty : Math.Floor(itemInfo.Qty);
                    }
                    else
                    {
                      nullable2 = itemInfo.UnitWeight;
                      Decimal num8 = 0M;
                      if (nullable2.GetValueOrDefault() > num8 & nullable2.HasValue)
                      {
                        Decimal num9;
                        if (!intSet.Contains(itemInfo.InventoryID))
                        {
                          Decimal emptyBoxWeight2 = advancedInItemBoxEx1.EmptyBoxWeight;
                          nullable2 = itemInfo.UnitWeight;
                          Decimal num10 = nullable2.Value;
                          num9 = Math.Floor(emptyBoxWeight2 / num10);
                        }
                        else
                        {
                          Decimal emptyBoxWeight3 = advancedInItemBoxEx1.EmptyBoxWeight;
                          nullable2 = itemInfo.UnitWeight;
                          Decimal num11 = nullable2.Value;
                          num9 = emptyBoxWeight3 / num11;
                        }
                        num1 = num9;
                      }
                      else
                        num1 = intSet.Contains(itemInfo.InventoryID) ? itemInfo.Qty : Math.Floor(itemInfo.Qty);
                    }
                    Decimal num12;
                    if (advancedInItemBoxEx1.EmptyBoxVolume > totalVolume)
                    {
                      nullable2 = itemInfo.UnitVolume;
                      Decimal num13 = 0M;
                      if (nullable2.GetValueOrDefault() > num13 & nullable2.HasValue)
                      {
                        Decimal num14;
                        if (!intSet.Contains(itemInfo.InventoryID))
                        {
                          Decimal num15 = totalVolume;
                          nullable2 = itemInfo.UnitVolume;
                          Decimal num16 = nullable2.Value;
                          num14 = Math.Floor(num15 / num16);
                        }
                        else
                        {
                          Decimal num17 = totalVolume;
                          nullable2 = itemInfo.UnitVolume;
                          Decimal num18 = nullable2.Value;
                          num14 = num17 / num18;
                        }
                        num12 = num14;
                      }
                      else
                        num12 = intSet.Contains(itemInfo.InventoryID) ? itemInfo.Qty : Math.Floor(itemInfo.Qty);
                    }
                    else
                    {
                      nullable2 = itemInfo.UnitVolume;
                      Decimal num19 = 0M;
                      if (nullable2.GetValueOrDefault() > num19 & nullable2.HasValue)
                      {
                        Decimal num20;
                        if (!intSet.Contains(itemInfo.InventoryID))
                        {
                          Decimal emptyBoxVolume2 = advancedInItemBoxEx1.EmptyBoxVolume;
                          nullable2 = itemInfo.UnitVolume;
                          Decimal num21 = nullable2.Value;
                          num20 = Math.Floor(emptyBoxVolume2 / num21);
                        }
                        else
                        {
                          Decimal emptyBoxVolume3 = advancedInItemBoxEx1.EmptyBoxVolume;
                          nullable2 = itemInfo.UnitVolume;
                          Decimal num22 = nullable2.Value;
                          num20 = emptyBoxVolume3 / num22;
                        }
                        num12 = num20;
                      }
                      else
                        num12 = intSet.Contains(itemInfo.InventoryID) ? itemInfo.Qty : Math.Floor(itemInfo.Qty);
                    }
                    Decimal num23 = num12 < num1 ? num12 : num1;
                    nullable2 = itemInfo.UnitWeight;
                    Decimal num24 = num23 * nullable2.GetValueOrDefault();
                    nullable2 = itemInfo.UnitVolume;
                    Decimal num25 = num23 * nullable2.GetValueOrDefault();
                    totalWeight -= num24;
                    advancedInItemBoxEx1.InvenoryList.Add(itemInfo.InventoryID);
                    advancedInItemBoxEx1.CurrentWeight += num24;
                    advancedInItemBoxEx1.EmptyBoxWeight = advancedInItemBoxEx1.MaxNetWeight - advancedInItemBoxEx1.CurrentWeight;
                    advancedInItemBoxEx1.CurrentVolume += num25;
                    SOPackageEngine.advancedINItemBoxEx advancedInItemBoxEx2 = advancedInItemBoxEx1;
                    nullable2 = advancedInItemBoxEx1.MaxVolume;
                    Decimal num26 = nullable2.GetValueOrDefault() - advancedInItemBoxEx1.CurrentVolume;
                    advancedInItemBoxEx2.EmptyBoxVolume = num26;
                  }
                }
              }
            }
          }
          if (totalWeight > 0M)
          {
            while (totalWeight > inItemBoxEx1.MaxNetWeight)
            {
              nullable2 = itemInfo.UnitWeight;
              Decimal num27 = 0M;
              Decimal num28;
              if (nullable2.GetValueOrDefault() > num27 & nullable2.HasValue)
              {
                Decimal num29;
                if (!intSet.Contains(itemInfo.InventoryID))
                {
                  Decimal maxNetWeight2 = inItemBoxEx1.MaxNetWeight;
                  nullable2 = itemInfo.UnitWeight;
                  Decimal num30 = nullable2.Value;
                  num29 = Math.Floor(maxNetWeight2 / num30);
                }
                else
                {
                  Decimal maxNetWeight3 = inItemBoxEx1.MaxNetWeight;
                  nullable2 = itemInfo.UnitWeight;
                  Decimal num31 = nullable2.Value;
                  num29 = maxNetWeight3 / num31;
                }
                num28 = num29;
              }
              else
                num28 = intSet.Contains(itemInfo.InventoryID) ? itemInfo.Qty : Math.Floor(itemInfo.Qty);
              nullable2 = itemInfo.UnitVolume;
              Decimal num32 = 0M;
              Decimal num33;
              if (nullable2.GetValueOrDefault() > num32 & nullable2.HasValue)
              {
                nullable2 = inItemBoxEx1.MaxVolume;
                Decimal num34 = 0M;
                if (nullable2.GetValueOrDefault() > num34 & nullable2.HasValue)
                {
                  Decimal num35;
                  if (!intSet.Contains(itemInfo.InventoryID))
                  {
                    nullable2 = inItemBoxEx1.MaxVolume;
                    Decimal num36 = nullable2.Value;
                    nullable2 = itemInfo.UnitVolume;
                    Decimal num37 = nullable2.Value;
                    num35 = Math.Floor(num36 / num37);
                  }
                  else
                  {
                    nullable2 = inItemBoxEx1.MaxVolume;
                    Decimal num38 = nullable2.Value;
                    nullable2 = itemInfo.UnitVolume;
                    Decimal num39 = nullable2.Value;
                    num35 = num38 / num39;
                  }
                  num33 = num35;
                  goto label_69;
                }
              }
              num33 = intSet.Contains(itemInfo.InventoryID) ? itemInfo.Qty : Math.Floor(itemInfo.Qty);
label_69:
              Decimal num40 = num33 < num28 ? num33 : num28;
              nullable2 = itemInfo.UnitWeight;
              Decimal num41 = num40 * nullable2.GetValueOrDefault();
              nullable2 = itemInfo.UnitVolume;
              Decimal num42 = num40 * nullable2.GetValueOrDefault();
              totalWeight -= num41;
              totalVolume -= num42;
              SOPackageEngine.advancedINItemBoxEx advancedInItemBoxEx3 = new SOPackageEngine.advancedINItemBoxEx();
              advancedInItemBoxEx3.InvenoryList = new List<int>();
              advancedInItemBoxEx3.BoxID = inItemBoxEx1.BoxID;
              advancedInItemBoxEx3.InvenoryList.Add(itemInfo.InventoryID);
              advancedInItemBoxEx3.BoxWeight = inItemBoxEx1.BoxWeight;
              advancedInItemBoxEx3.MaxWeight = inItemBoxEx1.MaxWeight;
              advancedInItemBoxEx3.MaxVolume = inItemBoxEx1.MaxVolume;
              advancedInItemBoxEx3.EmptyBoxWeight = inItemBoxEx1.MaxNetWeight - num41;
              SOPackageEngine.advancedINItemBoxEx advancedInItemBoxEx4 = advancedInItemBoxEx3;
              nullable2 = inItemBoxEx1.MaxVolume;
              Decimal num43 = nullable2.GetValueOrDefault() - num42;
              advancedInItemBoxEx4.EmptyBoxVolume = num43;
              advancedInItemBoxEx3.CurrentWeight = num41;
              advancedInItemBoxEx3.CurrentVolume = num42;
              advancedInItemBoxEx3.Length = inItemBoxEx1.Length;
              advancedInItemBoxEx3.Width = inItemBoxEx1.Width;
              advancedInItemBoxEx3.Height = inItemBoxEx1.Height;
              advancedInItemBoxExList.Add(advancedInItemBoxEx3);
            }
            while (totalWeight > 0M)
            {
              Decimal num44;
              if (inItemBoxEx1.MaxNetWeight > totalWeight)
              {
                nullable2 = itemInfo.UnitWeight;
                Decimal num45 = 0M;
                if (nullable2.GetValueOrDefault() > num45 & nullable2.HasValue)
                {
                  Decimal num46;
                  if (!intSet.Contains(itemInfo.InventoryID))
                  {
                    Decimal num47 = totalWeight;
                    nullable2 = itemInfo.UnitWeight;
                    Decimal num48 = nullable2.Value;
                    num46 = Math.Floor(num47 / num48);
                  }
                  else
                  {
                    Decimal num49 = totalWeight;
                    nullable2 = itemInfo.UnitWeight;
                    Decimal num50 = nullable2.Value;
                    num46 = num49 / num50;
                  }
                  num44 = num46;
                }
                else
                  num44 = intSet.Contains(itemInfo.InventoryID) ? itemInfo.Qty : Math.Floor(itemInfo.Qty);
              }
              else
              {
                nullable2 = itemInfo.UnitWeight;
                Decimal num51 = 0M;
                if (nullable2.GetValueOrDefault() > num51 & nullable2.HasValue)
                {
                  Decimal num52;
                  if (!intSet.Contains(itemInfo.InventoryID))
                  {
                    Decimal maxNetWeight4 = inItemBoxEx1.MaxNetWeight;
                    nullable2 = itemInfo.UnitWeight;
                    Decimal num53 = nullable2.Value;
                    num52 = Math.Floor(maxNetWeight4 / num53);
                  }
                  else
                  {
                    Decimal maxNetWeight5 = inItemBoxEx1.MaxNetWeight;
                    nullable2 = itemInfo.UnitWeight;
                    Decimal num54 = nullable2.Value;
                    num52 = maxNetWeight5 / num54;
                  }
                  num44 = num52;
                }
                else
                  num44 = intSet.Contains(itemInfo.InventoryID) ? itemInfo.Qty : Math.Floor(itemInfo.Qty);
              }
              nullable2 = inItemBoxEx1.MaxVolume;
              Decimal num55 = totalVolume;
              Decimal num56;
              if (nullable2.GetValueOrDefault() > num55 & nullable2.HasValue)
              {
                nullable2 = itemInfo.UnitVolume;
                Decimal num57 = 0M;
                if (nullable2.GetValueOrDefault() > num57 & nullable2.HasValue)
                {
                  Decimal num58;
                  if (!intSet.Contains(itemInfo.InventoryID))
                  {
                    Decimal num59 = totalVolume;
                    nullable2 = itemInfo.UnitVolume;
                    Decimal num60 = nullable2.Value;
                    num58 = Math.Floor(num59 / num60);
                  }
                  else
                  {
                    Decimal num61 = totalVolume;
                    nullable2 = itemInfo.UnitVolume;
                    Decimal num62 = nullable2.Value;
                    num58 = num61 / num62;
                  }
                  num56 = num58;
                }
                else
                  num56 = intSet.Contains(itemInfo.InventoryID) ? itemInfo.Qty : Math.Floor(itemInfo.Qty);
              }
              else
              {
                nullable2 = itemInfo.UnitVolume;
                Decimal num63 = 0M;
                if (nullable2.GetValueOrDefault() > num63 & nullable2.HasValue)
                {
                  nullable2 = inItemBoxEx1.MaxVolume;
                  Decimal num64 = 0M;
                  if (nullable2.GetValueOrDefault() > num64 & nullable2.HasValue)
                  {
                    Decimal num65;
                    if (!intSet.Contains(itemInfo.InventoryID))
                    {
                      nullable2 = inItemBoxEx1.MaxVolume;
                      Decimal num66 = nullable2.Value;
                      nullable2 = itemInfo.UnitVolume;
                      Decimal num67 = nullable2.Value;
                      num65 = Math.Floor(num66 / num67);
                    }
                    else
                    {
                      nullable2 = inItemBoxEx1.MaxVolume;
                      Decimal num68 = nullable2.Value;
                      nullable2 = itemInfo.UnitVolume;
                      Decimal num69 = nullable2.Value;
                      num65 = num68 / num69;
                    }
                    num56 = num65;
                    goto label_98;
                  }
                }
                num56 = intSet.Contains(itemInfo.InventoryID) ? itemInfo.Qty : Math.Floor(itemInfo.Qty);
              }
label_98:
              Decimal num70 = num56 < num44 ? num56 : num44;
              nullable2 = itemInfo.UnitWeight;
              Decimal num71 = num70 * nullable2.GetValueOrDefault();
              nullable2 = itemInfo.UnitVolume;
              Decimal num72 = num70 * nullable2.GetValueOrDefault();
              totalWeight -= num71;
              totalVolume -= num72;
              SOPackageEngine.advancedINItemBoxEx advancedInItemBoxEx5 = new SOPackageEngine.advancedINItemBoxEx();
              advancedInItemBoxEx5.InvenoryList = new List<int>();
              advancedInItemBoxEx5.BoxID = inItemBoxEx1.BoxID;
              advancedInItemBoxEx5.InvenoryList.Add(itemInfo.InventoryID);
              advancedInItemBoxEx5.BoxWeight = inItemBoxEx1.BoxWeight;
              advancedInItemBoxEx5.MaxWeight = inItemBoxEx1.MaxWeight;
              advancedInItemBoxEx5.MaxVolume = inItemBoxEx1.MaxVolume;
              advancedInItemBoxEx5.EmptyBoxWeight = inItemBoxEx1.MaxNetWeight - num71;
              SOPackageEngine.advancedINItemBoxEx advancedInItemBoxEx6 = advancedInItemBoxEx5;
              nullable2 = inItemBoxEx1.MaxVolume;
              Decimal num73 = nullable2.GetValueOrDefault() - num72;
              advancedInItemBoxEx6.EmptyBoxVolume = num73;
              advancedInItemBoxEx5.CurrentWeight = num71;
              advancedInItemBoxEx5.CurrentVolume = num72;
              advancedInItemBoxEx5.Length = inItemBoxEx1.Length;
              advancedInItemBoxEx5.Width = inItemBoxEx1.Width;
              advancedInItemBoxEx5.Height = inItemBoxEx1.Height;
              advancedInItemBoxExList.Add(advancedInItemBoxEx5);
            }
            continue;
          }
          continue;
        }
label_17:
        throw new PXException("Packages is not configured properly for item {0}. Please correct in on Stock Items screen and try again. Given item do not fit in any of the box configured for it.", new object[1]
        {
          (object) PX.Objects.IN.InventoryItem.PK.Find(this.graph, new int?(itemInfo.InventoryID)).InventoryCD.Trim()
        });
      }
      this.PackIntoSmallerBoxes(boxes, boxItemsLookup, advancedInItemBoxExList, true);
    }
    else
    {
      foreach (SOPackageEngine.ItemInfo itemInfo in items)
      {
        INItemBoxEx inItemBoxEx3 = boxesByInventoryLookup[itemInfo.InventoryID].ToArray()[0];
        Decimal totalWeight = itemInfo.TotalWeight;
        Decimal? unitWeight = itemInfo.UnitWeight;
        Decimal maxNetWeight6 = inItemBoxEx3.MaxNetWeight;
        if (unitWeight.GetValueOrDefault() > maxNetWeight6 & unitWeight.HasValue)
          throw new PXException("Packages is not configured properly for item {0}. Please correct in on Stock Items screen and try again. Given item do not fit in any of the box configured for it.", new object[1]
          {
            (object) PX.Objects.IN.InventoryItem.PK.Find(this.graph, new int?(itemInfo.InventoryID)).InventoryCD.Trim()
          });
        foreach (SOPackageEngine.advancedINItemBoxEx advancedInItemBoxEx in advancedInItemBoxExList)
        {
          foreach (INItemBoxEx inItemBoxEx4 in boxesByInventoryLookup[itemInfo.InventoryID].ToArray())
          {
            if (advancedInItemBoxEx.BoxID == inItemBoxEx4.BoxID && totalWeight > 0M)
            {
              Decimal emptyBoxWeight4 = advancedInItemBoxEx.EmptyBoxWeight;
              unitWeight = itemInfo.UnitWeight;
              Decimal valueOrDefault5 = unitWeight.GetValueOrDefault();
              if (emptyBoxWeight4 >= valueOrDefault5 & unitWeight.HasValue)
              {
                Decimal num74;
                if (advancedInItemBoxEx.EmptyBoxWeight > totalWeight)
                {
                  unitWeight = itemInfo.UnitWeight;
                  Decimal num75 = 0M;
                  if (unitWeight.GetValueOrDefault() > num75 & unitWeight.HasValue)
                  {
                    Decimal num76;
                    if (!intSet.Contains(itemInfo.InventoryID))
                    {
                      Decimal num77 = totalWeight;
                      unitWeight = itemInfo.UnitWeight;
                      Decimal num78 = unitWeight.Value;
                      num76 = Math.Floor(num77 / num78);
                    }
                    else
                    {
                      Decimal num79 = totalWeight;
                      unitWeight = itemInfo.UnitWeight;
                      Decimal num80 = unitWeight.Value;
                      num76 = num79 / num80;
                    }
                    num74 = num76;
                  }
                  else
                    num74 = intSet.Contains(itemInfo.InventoryID) ? itemInfo.Qty : Math.Ceiling(itemInfo.Qty);
                }
                else
                {
                  unitWeight = itemInfo.UnitWeight;
                  Decimal num81 = 0M;
                  if (unitWeight.GetValueOrDefault() > num81 & unitWeight.HasValue)
                  {
                    Decimal num82;
                    if (!intSet.Contains(itemInfo.InventoryID))
                    {
                      Decimal emptyBoxWeight5 = advancedInItemBoxEx.EmptyBoxWeight;
                      unitWeight = itemInfo.UnitWeight;
                      Decimal num83 = unitWeight.Value;
                      num82 = Math.Floor(emptyBoxWeight5 / num83);
                    }
                    else
                    {
                      Decimal emptyBoxWeight6 = advancedInItemBoxEx.EmptyBoxWeight;
                      unitWeight = itemInfo.UnitWeight;
                      Decimal num84 = unitWeight.Value;
                      num82 = emptyBoxWeight6 / num84;
                    }
                    num74 = num82;
                  }
                  else
                    num74 = intSet.Contains(itemInfo.InventoryID) ? itemInfo.Qty : Math.Ceiling(itemInfo.Qty);
                }
                Decimal num85 = num74;
                unitWeight = itemInfo.UnitWeight;
                Decimal valueOrDefault6 = unitWeight.GetValueOrDefault();
                Decimal num86 = num85 * valueOrDefault6;
                totalWeight -= num86;
                advancedInItemBoxEx.InvenoryList.Add(itemInfo.InventoryID);
                advancedInItemBoxEx.CurrentWeight += num86;
                advancedInItemBoxEx.EmptyBoxWeight = advancedInItemBoxEx.MaxNetWeight - advancedInItemBoxEx.CurrentWeight;
              }
            }
          }
        }
        if (totalWeight > 0M && inItemBoxEx3.MaxNetWeight > 0M)
        {
          while (totalWeight > inItemBoxEx3.MaxNetWeight)
          {
            unitWeight = itemInfo.UnitWeight;
            Decimal num87 = 0M;
            Decimal num88;
            if (unitWeight.GetValueOrDefault() > num87 & unitWeight.HasValue)
            {
              Decimal num89;
              if (!intSet.Contains(itemInfo.InventoryID))
              {
                Decimal maxNetWeight7 = inItemBoxEx3.MaxNetWeight;
                unitWeight = itemInfo.UnitWeight;
                Decimal num90 = unitWeight.Value;
                num89 = Math.Floor(maxNetWeight7 / num90);
              }
              else
              {
                Decimal maxNetWeight8 = inItemBoxEx3.MaxNetWeight;
                unitWeight = itemInfo.UnitWeight;
                Decimal num91 = unitWeight.Value;
                num89 = maxNetWeight8 / num91;
              }
              num88 = num89;
            }
            else
              num88 = intSet.Contains(itemInfo.InventoryID) ? itemInfo.Qty : Math.Ceiling(itemInfo.Qty);
            Decimal num92 = num88;
            unitWeight = itemInfo.UnitWeight;
            Decimal valueOrDefault = unitWeight.GetValueOrDefault();
            Decimal num93 = num92 * valueOrDefault;
            totalWeight -= num93;
            SOPackageEngine.advancedINItemBoxEx advancedInItemBoxEx = new SOPackageEngine.advancedINItemBoxEx();
            advancedInItemBoxEx.InvenoryList = new List<int>();
            advancedInItemBoxEx.BoxID = inItemBoxEx3.BoxID;
            advancedInItemBoxEx.InvenoryList.Add(itemInfo.InventoryID);
            advancedInItemBoxEx.BoxWeight = inItemBoxEx3.BoxWeight;
            advancedInItemBoxEx.MaxWeight = inItemBoxEx3.MaxWeight;
            advancedInItemBoxEx.MaxVolume = inItemBoxEx3.MaxVolume;
            advancedInItemBoxEx.EmptyBoxWeight = inItemBoxEx3.MaxNetWeight - num93;
            advancedInItemBoxEx.CurrentWeight = num93;
            advancedInItemBoxEx.Length = inItemBoxEx3.Length;
            advancedInItemBoxEx.Width = inItemBoxEx3.Width;
            advancedInItemBoxEx.Height = inItemBoxEx3.Height;
            advancedInItemBoxExList.Add(advancedInItemBoxEx);
          }
          SOPackageEngine.advancedINItemBoxEx advancedInItemBoxEx7 = new SOPackageEngine.advancedINItemBoxEx();
          advancedInItemBoxEx7.InvenoryList = new List<int>();
          advancedInItemBoxEx7.BoxID = inItemBoxEx3.BoxID;
          advancedInItemBoxEx7.InvenoryList.Add(itemInfo.InventoryID);
          advancedInItemBoxEx7.BoxWeight = inItemBoxEx3.BoxWeight;
          advancedInItemBoxEx7.MaxWeight = inItemBoxEx3.MaxWeight;
          advancedInItemBoxEx7.MaxVolume = inItemBoxEx3.MaxVolume;
          advancedInItemBoxEx7.EmptyBoxWeight = inItemBoxEx3.MaxNetWeight - totalWeight;
          advancedInItemBoxEx7.CurrentWeight = totalWeight;
          advancedInItemBoxEx7.Length = inItemBoxEx3.Length;
          advancedInItemBoxEx7.Width = inItemBoxEx3.Width;
          advancedInItemBoxEx7.Height = inItemBoxEx3.Height;
          advancedInItemBoxExList.Add(advancedInItemBoxEx7);
        }
      }
      this.PackIntoSmallerBoxes(boxes, boxItemsLookup, advancedInItemBoxExList, false);
    }
    Decimal num94 = advancedInItemBoxExList.Sum<SOPackageEngine.advancedINItemBoxEx>((Func<SOPackageEngine.advancedINItemBoxEx, Decimal>) (x => x.CurrentWeight));
    Decimal num95 = num94 == 0M ? declaredValue.GetValueOrDefault() : declaredValue.GetValueOrDefault() / num94;
    List<SOPackageInfoEx> soPackageInfoExList = new List<SOPackageInfoEx>();
    foreach (SOPackageEngine.advancedINItemBoxEx advancedInItemBoxEx in advancedInItemBoxExList)
    {
      SOPackageInfoEx soPackageInfoEx1 = new SOPackageInfoEx();
      soPackageInfoEx1.BoxID = advancedInItemBoxEx.BoxID;
      soPackageInfoEx1.SiteID = siteID;
      soPackageInfoEx1.DeclaredValue = new Decimal?(num95 * advancedInItemBoxEx.CurrentWeight);
      soPackageInfoEx1.Length = advancedInItemBoxEx.Length;
      soPackageInfoEx1.Width = advancedInItemBoxEx.Width;
      soPackageInfoEx1.Height = advancedInItemBoxEx.Height;
      foreach (INItemBox box in boxes)
      {
        if (box.BoxID == soPackageInfoEx1.BoxID)
        {
          soPackageInfoEx1.Weight = new Decimal?(Decimal.Round(advancedInItemBoxEx.CurrentWeight, 4));
          soPackageInfoEx1.BoxWeight = new Decimal?(Decimal.Round(advancedInItemBoxEx.BoxWeight.GetValueOrDefault(), 4));
          SOPackageInfoEx soPackageInfoEx2 = soPackageInfoEx1;
          Decimal? weight = soPackageInfoEx1.Weight;
          Decimal? boxWeight = soPackageInfoEx1.BoxWeight;
          Decimal? nullable = weight.HasValue & boxWeight.HasValue ? new Decimal?(weight.GetValueOrDefault() + boxWeight.GetValueOrDefault()) : new Decimal?();
          soPackageInfoEx2.GrossWeight = nullable;
        }
      }
      soPackageInfoExList.Add(soPackageInfoEx1);
    }
    return soPackageInfoExList;
  }

  /// <summary>Second pass. Trying to pack items into smaller boxes.</summary>
  private void PackIntoSmallerBoxes(
    List<INItemBoxEx> boxes,
    Dictionary<string, List<int>> boxItemsLookup,
    List<SOPackageEngine.advancedINItemBoxEx> advancedboxes,
    bool packByWeightAndVolume)
  {
    foreach (SOPackageEngine.advancedINItemBoxEx advancedbox in advancedboxes)
    {
      foreach (INItemBoxEx box in boxes)
      {
        if (!(advancedbox.BoxID == box.BoxID))
        {
          bool flag1 = true;
          foreach (int invenory in advancedbox.InvenoryList)
          {
            if (!boxItemsLookup[box.BoxID].Contains(invenory))
            {
              flag1 = false;
              break;
            }
          }
          if (flag1)
          {
            bool flag2 = advancedbox.CurrentWeight < box.MaxNetWeight;
            Decimal currentVolume = advancedbox.CurrentVolume;
            Decimal? maxVolume = box.MaxVolume;
            Decimal valueOrDefault = maxVolume.GetValueOrDefault();
            bool flag3 = currentVolume <= valueOrDefault & maxVolume.HasValue;
            bool flag4 = false;
            if (packByWeightAndVolume)
            {
              if (flag2 & flag3)
              {
                Decimal? nullable1 = box.MaxWeight;
                Decimal? nullable2 = advancedbox.MaxWeight;
                if (!(nullable1.GetValueOrDefault() < nullable2.GetValueOrDefault() & nullable1.HasValue & nullable2.HasValue))
                {
                  nullable2 = box.MaxWeight;
                  nullable1 = advancedbox.MaxWeight;
                  if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue)
                  {
                    nullable1 = box.MaxVolume;
                    nullable2 = advancedbox.MaxVolume;
                    if (!(nullable1.GetValueOrDefault() < nullable2.GetValueOrDefault() & nullable1.HasValue & nullable2.HasValue))
                    {
                      nullable2 = box.MaxVolume;
                      nullable1 = advancedbox.MaxVolume;
                      if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue)
                      {
                        nullable1 = box.BoxWeight;
                        nullable2 = advancedbox.BoxWeight;
                        if (!(nullable1.GetValueOrDefault() < nullable2.GetValueOrDefault() & nullable1.HasValue & nullable2.HasValue))
                          goto label_25;
                      }
                      else
                        goto label_25;
                    }
                  }
                  else
                    goto label_25;
                }
                flag4 = true;
              }
            }
            else if (flag2)
            {
              Decimal? nullable3 = box.MaxWeight;
              Decimal? nullable4 = advancedbox.MaxWeight;
              if (!(nullable3.GetValueOrDefault() < nullable4.GetValueOrDefault() & nullable3.HasValue & nullable4.HasValue))
              {
                nullable4 = box.MaxWeight;
                nullable3 = advancedbox.MaxWeight;
                if (nullable4.GetValueOrDefault() == nullable3.GetValueOrDefault() & nullable4.HasValue == nullable3.HasValue)
                {
                  nullable3 = box.BoxWeight;
                  nullable4 = advancedbox.BoxWeight;
                  if (!(nullable3.GetValueOrDefault() < nullable4.GetValueOrDefault() & nullable3.HasValue & nullable4.HasValue))
                    goto label_25;
                }
                else
                  goto label_25;
              }
              flag4 = true;
            }
label_25:
            if (flag4)
            {
              advancedbox.BoxID = box.BoxID;
              advancedbox.Length = box.Length;
              advancedbox.Width = box.Width;
              advancedbox.Height = box.Height;
              advancedbox.MaxWeight = box.MaxWeight;
              advancedbox.MaxVolume = box.MaxVolume;
              advancedbox.BoxWeight = box.BoxWeight;
            }
          }
        }
      }
    }
  }

  public virtual List<SOPackageEngine.BoxInfo> PackByQty(List<INItemBoxEx> boxes, Decimal baseQty)
  {
    boxes.Sort((Comparison<INItemBoxEx>) ((x, y) =>
    {
      Decimal? baseQty1 = x.BaseQty;
      Decimal valueOrDefault1 = baseQty1.GetValueOrDefault();
      baseQty1 = y.BaseQty;
      Decimal valueOrDefault2 = baseQty1.GetValueOrDefault();
      return Decimal.Compare(valueOrDefault1, valueOrDefault2);
    }));
    List<SOPackageEngine.BoxInfo> boxInfoList = new List<SOPackageEngine.BoxInfo>();
    if (baseQty > 0M && boxes.Count > 0)
    {
      INItemBoxEx boxThatCanFitQty = this.GetBoxThatCanFitQty(boxes, baseQty);
      if (boxThatCanFitQty != null)
      {
        boxInfoList.Add(new SOPackageEngine.BoxInfo()
        {
          Box = boxThatCanFitQty,
          Value = baseQty
        });
      }
      else
      {
        INItemBoxEx box = boxes[boxes.Count - 1];
        Decimal? baseQty2 = box.BaseQty;
        if (baseQty2.Value > 0M)
        {
          Decimal num1 = baseQty;
          baseQty2 = box.BaseQty;
          Decimal num2 = baseQty2.Value;
          int num3 = (int) Math.Floor(num1 / num2);
          for (int index = 0; index < num3; ++index)
          {
            SOPackageEngine.BoxInfo boxInfo1 = new SOPackageEngine.BoxInfo();
            boxInfo1.Box = box;
            SOPackageEngine.BoxInfo boxInfo2 = boxInfo1;
            baseQty2 = box.BaseQty;
            Decimal num4 = baseQty2.Value;
            boxInfo2.Value = num4;
            boxInfoList.Add(boxInfo1);
            Decimal num5 = baseQty;
            baseQty2 = box.BaseQty;
            Decimal num6 = baseQty2.Value;
            baseQty = num5 - num6;
          }
          boxInfoList.AddRange((IEnumerable<SOPackageEngine.BoxInfo>) this.PackByQty(boxes, baseQty));
        }
      }
    }
    return boxInfoList;
  }

  public virtual List<SOPackageEngine.BoxInfo> PackByWeight(
    List<INItemBoxEx> boxes,
    Decimal baseWeight,
    Decimal baseQty,
    int inventoryID)
  {
    boxes.Sort((Comparison<INItemBoxEx>) ((x, y) =>
    {
      Decimal? maxWeight = x.MaxWeight;
      Decimal valueOrDefault1 = maxWeight.GetValueOrDefault();
      maxWeight = y.MaxWeight;
      Decimal valueOrDefault2 = maxWeight.GetValueOrDefault();
      return Decimal.Compare(valueOrDefault1, valueOrDefault2);
    }));
    List<SOPackageEngine.BoxInfo> boxInfoList = new List<SOPackageEngine.BoxInfo>();
    if (baseQty == 0M)
    {
      PXTrace.WriteWarning("Packing Engine failed to execute. BaseQty supplied is zero.");
      return boxInfoList;
    }
    Decimal num1 = baseWeight / baseQty;
    if (baseWeight > 0M && boxes.Count > 0)
    {
      INItemBoxEx thatCanFitWeight = this.GetBoxThatCanFitWeight(boxes, baseWeight);
      if (thatCanFitWeight != null)
      {
        boxInfoList.Add(new SOPackageEngine.BoxInfo()
        {
          Box = thatCanFitWeight,
          Value = baseWeight
        });
      }
      else
      {
        INItemBoxEx box = boxes[boxes.Count - 1];
        if (box.MaxNetWeight > 0M)
        {
          if (box.MaxNetWeight < num1)
            throw new PXException("No boxes that fit the item {0} by weight have been found. Create a new box on the Boxes (CS207600) form, or disable auto-packaging for the {0} item.", new object[1]
            {
              (object) PX.Objects.IN.InventoryItem.PK.Find(this.graph, new int?(inventoryID))?.InventoryCD
            });
          int num2 = (int) Math.Floor(baseWeight / box.MaxNetWeight);
          int num3 = (int) Math.Floor(box.MaxNetWeight / num1);
          for (int index = 0; index < num2; ++index)
          {
            boxInfoList.Add(new SOPackageEngine.BoxInfo()
            {
              Box = box,
              Value = (Decimal) num3 * num1
            });
            baseWeight -= (Decimal) num3 * num1;
            baseQty -= (Decimal) num3;
          }
          boxInfoList.AddRange((IEnumerable<SOPackageEngine.BoxInfo>) this.PackByWeight(boxes, baseWeight, baseQty, inventoryID));
        }
      }
    }
    return boxInfoList;
  }

  public virtual List<INItemBoxEx> GetBoxesByInventoryID(int inventoryID, string carrierID)
  {
    PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find(this.graph, new int?(inventoryID));
    PXSelectBase<INItemBoxEx> pxSelectBase = !string.IsNullOrEmpty(carrierID) ? (PXSelectBase<INItemBoxEx>) new PXSelectJoin<INItemBoxEx, InnerJoin<CarrierPackage, On<INItemBoxEx.boxID, Equal<CarrierPackage.boxID>>>, Where<INItemBoxEx.inventoryID, Equal<Required<INItemBox.inventoryID>>, And<CarrierPackage.carrierID, Equal<Required<CarrierPackage.carrierID>>>>>(this.graph) : (PXSelectBase<INItemBoxEx>) new PXSelect<INItemBoxEx, Where<INItemBoxEx.inventoryID, Equal<Required<INItemBoxEx.inventoryID>>>>(this.graph);
    List<INItemBoxEx> boxesByInventoryId = new List<INItemBoxEx>();
    foreach (PXResult<INItemBoxEx> pxResult in pxSelectBase.Select(new object[2]
    {
      (object) inventoryID,
      (object) carrierID
    }))
    {
      INItemBoxEx inItemBoxEx = PXResult<INItemBoxEx>.op_Implicit(pxResult);
      boxesByInventoryId.Add(inItemBoxEx);
    }
    if (boxesByInventoryId.Count == 0)
    {
      if (inventoryItem.PackageOption == "Q")
      {
        if (string.IsNullOrEmpty(carrierID))
          throw new PXException("\"{0}\" do not have any boxes", new object[1]
          {
            (object) inventoryItem.InventoryCD
          });
        throw new PXException("Carrier \"{0}\" do not have any boxes setup for \"{1}\"", new object[2]
        {
          (object) carrierID,
          (object) inventoryItem.InventoryCD
        });
      }
      List<PX.Objects.CS.CSBox> boxesByCarrierId = this.GetBoxesByCarrierID(carrierID);
      if (boxesByCarrierId.Count == 0)
        throw new PXException("Carrier {0} do not have any boxes setup for it. Please correct this and try again.", new object[1]
        {
          (object) carrierID
        });
      foreach (PX.Objects.CS.CSBox csBox in boxesByCarrierId)
      {
        INItemBoxEx inItemBoxEx = new INItemBoxEx();
        inItemBoxEx.BoxID = csBox.BoxID;
        inItemBoxEx.BaseQty = new Decimal?();
        inItemBoxEx.BoxWeight = csBox.BoxWeight;
        inItemBoxEx.Description = csBox.Description;
        inItemBoxEx.InventoryID = new int?(inventoryID);
        inItemBoxEx.MaxQty = new Decimal?();
        inItemBoxEx.MaxVolume = csBox.MaxVolume;
        inItemBoxEx.MaxWeight = csBox.MaxWeight;
        inItemBoxEx.Qty = new Decimal?();
        inItemBoxEx.UOM = (string) null;
        inItemBoxEx.Length = csBox.Length;
        inItemBoxEx.Width = csBox.Width;
        inItemBoxEx.Height = csBox.Height;
        boxesByInventoryId.Add(inItemBoxEx);
      }
    }
    return boxesByInventoryId;
  }

  public virtual List<PX.Objects.CS.CSBox> GetBoxesByCarrierID(string carrierID)
  {
    List<PX.Objects.CS.CSBox> boxesByCarrierId = new List<PX.Objects.CS.CSBox>();
    if (string.IsNullOrEmpty(carrierID))
    {
      foreach (PXResult<PX.Objects.CS.CSBox> pxResult in PXSelectBase<PX.Objects.CS.CSBox, PXSelect<PX.Objects.CS.CSBox, Where<PX.Objects.CS.CSBox.maxWeight, Greater<decimal0>>>.Config>.Select(this.graph, Array.Empty<object>()))
      {
        PX.Objects.CS.CSBox csBox = PXResult<PX.Objects.CS.CSBox>.op_Implicit(pxResult);
        boxesByCarrierId.Add(csBox);
      }
    }
    else
    {
      foreach (PXResult<PX.Objects.CS.CSBox> pxResult in ((PXSelectBase<PX.Objects.CS.CSBox>) new PXSelectJoin<PX.Objects.CS.CSBox, InnerJoin<CarrierPackage, On<PX.Objects.CS.CSBox.boxID, Equal<CarrierPackage.boxID>>>, Where<CarrierPackage.carrierID, Equal<Required<CarrierPackage.carrierID>>, And<PX.Objects.CS.CSBox.maxWeight, Greater<decimal0>>>, OrderBy<Asc<PX.Objects.CS.CSBox.maxWeight>>>(this.graph)).Select(new object[1]
      {
        (object) carrierID
      }))
      {
        PX.Objects.CS.CSBox csBox = PXResult<PX.Objects.CS.CSBox>.op_Implicit(pxResult);
        boxesByCarrierId.Add(csBox);
      }
    }
    return boxesByCarrierId;
  }

  protected INItemBoxEx GetBoxThatCanFitQty(List<INItemBoxEx> boxes, Decimal baseQty)
  {
    for (int index = 0; index < boxes.Count; ++index)
    {
      Decimal? baseQty1 = boxes[index].BaseQty;
      Decimal num = baseQty;
      if (baseQty1.GetValueOrDefault() >= num & baseQty1.HasValue)
        return boxes[index];
    }
    return (INItemBoxEx) null;
  }

  protected INItemBoxEx GetBoxThatCanFitWeight(List<INItemBoxEx> boxes, Decimal baseWeight)
  {
    for (int index = 0; index < boxes.Count; ++index)
    {
      Decimal? maxWeight = boxes[index].MaxWeight;
      Decimal num = baseWeight + boxes[index].BoxWeight.GetValueOrDefault();
      if (maxWeight.GetValueOrDefault() >= num & maxWeight.HasValue)
        return boxes[index];
    }
    return (INItemBoxEx) null;
  }

  public class BoxInfo
  {
    public INItemBoxEx Box { get; set; }

    public Decimal Value { get; set; }
  }

  public class ItemStats
  {
    public const int Mixed = 0;
    private Dictionary<int, SOPackageEngine.ItemInfo> items = new Dictionary<int, SOPackageEngine.ItemInfo>();

    public int? SiteID { get; set; }

    public int? InventoryID { get; set; }

    public string PackOption { get; set; }

    public string Operation { get; set; }

    public Decimal BaseQty { get; set; }

    public Decimal BaseWeight { get; set; }

    public Decimal DeclaredValue { get; set; }

    public void AddLine(PX.Objects.IN.InventoryItem item, Decimal? baseQty)
    {
      if (this.items.ContainsKey(item.InventoryID.Value))
      {
        this.items[item.InventoryID.Value].Qty += baseQty.GetValueOrDefault();
      }
      else
      {
        SOPackageEngine.ItemInfo itemInfo = new SOPackageEngine.ItemInfo(item.InventoryID.Value, item.BaseWeight, item.BaseVolume, baseQty.GetValueOrDefault());
        this.items.Add(itemInfo.InventoryID, itemInfo);
      }
    }

    public ICollection<SOPackageEngine.ItemInfo> Lines
    {
      get => (ICollection<SOPackageEngine.ItemInfo>) this.items.Values;
    }
  }

  public class OrderInfo
  {
    private Dictionary<int, SOPackageEngine.ItemInfo> items = new Dictionary<int, SOPackageEngine.ItemInfo>();

    public string CarrierID { get; set; }

    public List<SOPackageEngine.ItemStats> Stats { get; private set; }

    public OrderInfo(string carrierID)
    {
      this.Stats = new List<SOPackageEngine.ItemStats>();
      this.CarrierID = carrierID;
    }

    public void AddLine(PX.Objects.IN.InventoryItem item, Decimal? baseQty)
    {
      if (this.items.ContainsKey(item.InventoryID.Value))
      {
        this.items[item.InventoryID.Value].Qty += baseQty.GetValueOrDefault();
      }
      else
      {
        SOPackageEngine.ItemInfo itemInfo = new SOPackageEngine.ItemInfo(item.InventoryID.Value, item.BaseWeight, item.BaseVolume, baseQty.GetValueOrDefault());
        this.items.Add(itemInfo.InventoryID, itemInfo);
      }
    }

    public ICollection<SOPackageEngine.ItemInfo> Lines
    {
      get => (ICollection<SOPackageEngine.ItemInfo>) this.items.Values;
    }
  }

  public class ItemInfo
  {
    public int InventoryID { get; private set; }

    public Decimal? UnitWeight { get; private set; }

    public Decimal? UnitVolume { get; private set; }

    public Decimal TotalWeight => this.UnitWeight.GetValueOrDefault() * this.Qty;

    public Decimal TotalVolume => this.UnitVolume.GetValueOrDefault() * this.Qty;

    public Decimal Qty { get; set; }

    public ItemInfo(int inventoryID, Decimal? unitWeight, Decimal? unitVolume, Decimal qty)
    {
      this.InventoryID = inventoryID;
      this.UnitWeight = unitWeight;
      this.UnitVolume = unitVolume;
      this.Qty = qty;
    }
  }

  public class PackSet
  {
    public int SiteID { get; private set; }

    public List<SOPackageInfoEx> Packages { get; private set; }

    public PackSet(int siteID)
    {
      this.SiteID = siteID;
      this.Packages = new List<SOPackageInfoEx>();
    }
  }

  [PXHidden]
  private class advancedINItemBoxEx : INItemBoxEx
  {
    /// <summary>
    /// Amount of weight that can be added to this box. Empty space.
    /// </summary>
    public Decimal EmptyBoxWeight { get; set; }

    /// <summary>
    /// Amount of volume that can be added to this box. Empty space.
    /// </summary>
    public Decimal EmptyBoxVolume { get; set; }

    public Decimal CurrentWeight { get; set; }

    public Decimal CurrentVolume { get; set; }

    public List<int> InvenoryList { get; set; }
  }
}
