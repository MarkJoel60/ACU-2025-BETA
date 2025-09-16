// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.Turnover.TurnoverCalcMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.Common.Exceptions;
using PX.Objects.CS;
using PX.Objects.GL.FinPeriods;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

#nullable enable
namespace PX.Objects.IN.Turnover;

public class TurnoverCalcMaint : PXGraph<
#nullable disable
TurnoverCalcMaint>
{
  public FbqlSelect<SelectFromBase<INTurnoverCalc, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  INTurnoverCalc.branchID, 
  #nullable disable
  Equal<P.AsInt>>>>, And<BqlOperand<
  #nullable enable
  INTurnoverCalc.fromPeriodID, IBqlString>.IsEqual<
  #nullable disable
  P.AsString.ASCII>>>>.And<BqlOperand<
  #nullable enable
  INTurnoverCalc.toPeriodID, IBqlString>.IsEqual<
  #nullable disable
  P.AsString.ASCII>>>, INTurnoverCalc>.View Calc;
  public FbqlSelect<SelectFromBase<INTurnoverCalcItem, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  INTurnoverCalcItem.branchID, 
  #nullable disable
  Equal<BqlField<
  #nullable enable
  INTurnoverCalc.branchID, IBqlInt>.FromCurrent>>>>, 
  #nullable disable
  And<BqlOperand<
  #nullable enable
  INTurnoverCalcItem.fromPeriodID, IBqlString>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  INTurnoverCalc.fromPeriodID, IBqlString>.FromCurrent>>>>.And<
  #nullable disable
  BqlOperand<
  #nullable enable
  INTurnoverCalcItem.toPeriodID, IBqlString>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  INTurnoverCalc.toPeriodID, IBqlString>.FromCurrent>>>, 
  #nullable disable
  INTurnoverCalcItem>.View CalcItems;
  public PXSetup<INSetup> insetup;

  public bool MassProcessing { get; set; }

  public virtual void Delete(INTurnoverCalc calc)
  {
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      this.DeleteImpl(PXResultset<INTurnoverCalc>.op_Implicit(((PXSelectBase<INTurnoverCalc>) this.Calc).Select(new object[3]
      {
        (object) calc.BranchID,
        (object) calc.FromPeriodID,
        (object) calc.ToPeriodID
      })) ?? throw new RowNotFoundException(((PXSelectBase) this.Calc).Cache, new object[3]
      {
        (object) calc.BranchID,
        (object) calc.FromPeriodID,
        (object) calc.ToPeriodID
      }));
      transactionScope.Complete();
    }
  }

  protected virtual void DeleteImpl(INTurnoverCalc calc)
  {
    PXDatabase.Delete<INTurnoverCalcItem>(new PXDataFieldRestrict[3]
    {
      (PXDataFieldRestrict) new PXDataFieldRestrict<INTurnoverCalcItem.branchID>((object) calc.BranchID),
      (PXDataFieldRestrict) new PXDataFieldRestrict<INTurnoverCalcItem.fromPeriodID>((object) calc.FromPeriodID),
      (PXDataFieldRestrict) new PXDataFieldRestrict<INTurnoverCalcItem.toPeriodID>((object) calc.ToPeriodID)
    });
    PXDatabase.Delete<INTurnoverCalc>(new PXDataFieldRestrict[3]
    {
      (PXDataFieldRestrict) new PXDataFieldRestrict<INTurnoverCalc.branchID>((object) calc.BranchID),
      (PXDataFieldRestrict) new PXDataFieldRestrict<INTurnoverCalc.fromPeriodID>((object) calc.FromPeriodID),
      (PXDataFieldRestrict) new PXDataFieldRestrict<INTurnoverCalc.toPeriodID>((object) calc.ToPeriodID)
    });
  }

  public virtual void Calculate(INTurnoverCalc calc)
  {
    TurnoverCalculationArgs calculationArgs = new TurnoverCalculationArgs()
    {
      NoteID = calc.NoteID,
      BranchID = calc.BranchID,
      FromPeriodID = calc.FromPeriodID,
      ToPeriodID = calc.ToPeriodID,
      SiteID = calc.SiteID,
      ItemClassID = calc.ItemClassID
    };
    if (calc.InventoryID.HasValue)
      calculationArgs.Inventories = new int?[1]
      {
        calc.InventoryID
      };
    this.Calculate(calculationArgs);
  }

  public virtual void Calculate(TurnoverCalculationArgs calculationArgs)
  {
    if (PXTransactionScope.IsScoped)
    {
      this.CalculateImpl(calculationArgs);
    }
    else
    {
      using (PXTransactionScope transactionScope = new PXTransactionScope())
      {
        this.CalculateImpl(calculationArgs);
        transactionScope.Complete();
      }
    }
  }

  protected virtual void CalculateImpl(TurnoverCalculationArgs calculationArgs)
  {
    if (!calculationArgs.BranchID.HasValue || calculationArgs.FromPeriodID == null || calculationArgs.ToPeriodID == null)
      return;
    calculationArgs.NumberOfDays = this.CaclulateNumberOfDays(calculationArgs.FromPeriodID, calculationArgs.ToPeriodID);
    if (calculationArgs.NumberOfDays == 0)
      return;
    INTurnoverCalc calc = PXResultset<INTurnoverCalc>.op_Implicit(((PXSelectBase<INTurnoverCalc>) this.Calc).Select(new object[3]
    {
      (object) calculationArgs.BranchID,
      (object) calculationArgs.FromPeriodID,
      (object) calculationArgs.ToPeriodID
    }));
    if (calc != null)
    {
      this.DeleteImpl(calc);
      ((PXSelectBase) this.Calc).Cache.Clear();
      ((PXSelectBase) this.Calc).Cache.ClearQueryCache();
    }
    ((PXSelectBase<INTurnoverCalc>) this.Calc).Insert(this.CreateTurnoverCalc(calculationArgs));
    this.CreateCalcItems(calculationArgs);
    ((PXGraph) this).Actions.PressSave();
  }

  protected virtual INTurnoverCalc CreateTurnoverCalc(TurnoverCalculationArgs calculationArgs)
  {
    INTurnoverCalc inTurnoverCalc = new INTurnoverCalc();
    inTurnoverCalc.NoteID = calculationArgs.NoteID;
    inTurnoverCalc.BranchID = calculationArgs.BranchID;
    inTurnoverCalc.FromPeriodID = calculationArgs.FromPeriodID;
    inTurnoverCalc.ToPeriodID = calculationArgs.ToPeriodID;
    inTurnoverCalc.IsFullCalc = new bool?(calculationArgs.IsFullCalc);
    int?[] inventories1 = calculationArgs.Inventories;
    inTurnoverCalc.IsInventoryListCalc = new bool?(inventories1 != null && inventories1.Length > 1);
    int?[] inventories2 = calculationArgs.Inventories;
    inTurnoverCalc.InventoryID = (inventories2 != null ? (inventories2.Length == 1 ? 1 : 0) : 0) != 0 ? calculationArgs.Inventories[0] : new int?();
    inTurnoverCalc.SiteID = calculationArgs.SiteID;
    inTurnoverCalc.ItemClassID = calculationArgs.ItemClassID;
    INTurnoverCalc turnoverCalc = inTurnoverCalc;
    if (!PXAccess.FeatureInstalled<FeaturesSet.kitAssemblies>())
      turnoverCalc.IncludedAssembly = new bool?(false);
    if (!PXAccess.FeatureInstalled<FeaturesSet.manufacturing>())
      turnoverCalc.IncludedProduction = new bool?(false);
    return turnoverCalc;
  }

  protected virtual INTurnoverCalcItem CreateTurnoverCalcItem(
    TurnoverCalculationArgs calculationArgs,
    int? inventoryID,
    int? siteID)
  {
    return new INTurnoverCalcItem()
    {
      BranchID = calculationArgs.BranchID,
      FromPeriodID = calculationArgs.FromPeriodID,
      ToPeriodID = calculationArgs.ToPeriodID,
      InventoryID = inventoryID,
      SiteID = siteID
    };
  }

  protected virtual void CreateCalcItems(TurnoverCalculationArgs calculationArgs)
  {
    foreach (int? loadStockItem in this.LoadStockItems(calculationArgs))
    {
      INTurnoverCalcItemHist[] array = ((IEnumerable<INTurnoverCalcItemHist>) this.LoadTurnoverCalcItemHist(calculationArgs, loadStockItem)).OrderBy<INTurnoverCalcItemHist, int?>((Func<INTurnoverCalcItemHist, int?>) (x => x.CostSubItemID)).ThenBy<INTurnoverCalcItemHist, int?>((Func<INTurnoverCalcItemHist, int?>) (x => x.CostSiteID)).ThenBy<INTurnoverCalcItemHist, int?>((Func<INTurnoverCalcItemHist, int?>) (x => x.AccountID)).ThenBy<INTurnoverCalcItemHist, int?>((Func<INTurnoverCalcItemHist, int?>) (x => x.SubID)).ToArray<INTurnoverCalcItemHist>();
      this.NormalizeInventoryHist(array);
      this.CalcInventoryTurnover(calculationArgs, loadStockItem, array);
    }
  }

  protected virtual void NormalizeInventoryHist(INTurnoverCalcItemHist[] inventoryHist)
  {
    Dictionary<(int?, int?, int?, int?), (Decimal?, Decimal?)> dictionary = new Dictionary<(int?, int?, int?, int?), (Decimal?, Decimal?)>();
    foreach (INTurnoverCalcItemHist h in inventoryHist)
    {
      if (h.CostHistFinPeriodID == null)
      {
        if (h.LastActiveFinPeriodID != null)
        {
          h.FinBegQty = h.FinYtdQty = h.LastFinYtdQty;
          h.FinBegCost = h.FinYtdCost = h.LastFinYtdCost;
        }
        else
        {
          (Decimal?, Decimal?) valueTuple;
          if (!dictionary.TryGetValue(GetTuple(h), out valueTuple))
            valueTuple = (new Decimal?(0M), new Decimal?(0M));
          h.FinBegQty = h.FinYtdQty = valueTuple.Item1;
          h.FinBegCost = h.FinYtdCost = valueTuple.Item2;
        }
      }
      dictionary[GetTuple(h)] = (h.FinYtdQty, h.FinYtdCost);
    }

    static (int? CostSubItemID, int? CostSiteID, int? AccountID, int? SubID) GetTuple(
      INTurnoverCalcItemHist h)
    {
      return (h.CostSubItemID, h.CostSiteID, h.AccountID, h.SubID);
    }
  }

  protected virtual void CalcInventoryTurnover(
    TurnoverCalculationArgs calculationArgs,
    int? inventoryID,
    INTurnoverCalcItemHist[] inventoryHist)
  {
    IEnumerable<IGrouping<int?, INTurnoverCalcItemHist>> groupings = ((IEnumerable<INTurnoverCalcItemHist>) inventoryHist).GroupBy<INTurnoverCalcItemHist, int?>((Func<INTurnoverCalcItemHist, int?>) (b => b.SiteID));
    bool flag = false;
    foreach (IGrouping<int?, INTurnoverCalcItemHist> histRows in groupings)
    {
      TurnoverCalcMaint.InventorySiteBalance siteBalance = new TurnoverCalcMaint.InventorySiteBalance(histRows.Key);
      siteBalance.AddRange((IEnumerable<INTurnoverCalcItemHist>) histRows);
      flag |= this.CalcInventoryTurnover(calculationArgs, inventoryID, siteBalance);
    }
    if (flag)
      return;
    int?[] inventories = calculationArgs.Inventories;
    if ((inventories != null ? (inventories.Length > 1 ? 1 : 0) : 0) == 0)
      return;
    this.AddVirtualInventoryTurnover(calculationArgs, inventoryID);
  }

  protected virtual bool CalcInventoryTurnover(
    TurnoverCalculationArgs calculationArgs,
    int? inventoryID,
    TurnoverCalcMaint.InventorySiteBalance siteBalance)
  {
    if (!siteBalance.Any())
      return false;
    INTurnoverCalc current = ((PXSelectBase<INTurnoverCalc>) this.Calc).Current;
    INTurnoverCalcItem turnoverCalcItem1 = this.CreateTurnoverCalcItem(calculationArgs, inventoryID, siteBalance.SiteID);
    TurnoverCalcMaint.InventorySitePeriodBalance sitePeriodBalance1 = siteBalance[calculationArgs.FromPeriodID];
    TurnoverCalcMaint.InventorySitePeriodBalance sitePeriodBalance2 = siteBalance[calculationArgs.ToPeriodID];
    turnoverCalcItem1.BegQty = sitePeriodBalance1.Hist.FinBegQty;
    turnoverCalcItem1.YtdQty = sitePeriodBalance2.Hist.FinYtdQty;
    turnoverCalcItem1.BegCost = sitePeriodBalance1.Hist.FinBegCost;
    turnoverCalcItem1.YtdCost = sitePeriodBalance2.Hist.FinYtdCost;
    turnoverCalcItem1.SoldQty = this.CalcSoldQty(current, siteBalance);
    turnoverCalcItem1.SoldCost = this.CalcSoldCost(current, siteBalance);
    ((PXSelectBase) this.CalcItems).Cache.SetValueExt<INTurnoverCalcItem.avgQty>((object) turnoverCalcItem1, (object) this.CalculateAverage((IEnumerable<TurnoverCalcMaint.InventorySitePeriodBalance>) siteBalance, (Func<TurnoverCalcMaint.InventorySitePeriodBalance, Decimal?>) (b => b.Hist.FinBegQty), (Func<TurnoverCalcMaint.InventorySitePeriodBalance, Decimal?>) (b => b.Hist.FinYtdQty)));
    ((PXSelectBase) this.CalcItems).Cache.SetValueExt<INTurnoverCalcItem.avgCost>((object) turnoverCalcItem1, (object) this.CalculateAverage((IEnumerable<TurnoverCalcMaint.InventorySitePeriodBalance>) siteBalance, (Func<TurnoverCalcMaint.InventorySitePeriodBalance, Decimal?>) (b => b.Hist.FinBegCost), (Func<TurnoverCalcMaint.InventorySitePeriodBalance, Decimal?>) (b => b.Hist.FinYtdCost)));
    Decimal? nullable1 = turnoverCalcItem1.AvgQty;
    Decimal num1 = 0M;
    if (nullable1.GetValueOrDefault() == num1 & nullable1.HasValue)
    {
      nullable1 = turnoverCalcItem1.SoldQty;
      Decimal num2 = 0M;
      if (nullable1.GetValueOrDefault() == num2 & nullable1.HasValue)
      {
        nullable1 = turnoverCalcItem1.AvgCost;
        Decimal num3 = 0M;
        if (nullable1.GetValueOrDefault() == num3 & nullable1.HasValue)
        {
          nullable1 = turnoverCalcItem1.SoldCost;
          Decimal num4 = 0M;
          if (nullable1.GetValueOrDefault() == num4 & nullable1.HasValue)
            return false;
        }
      }
    }
    nullable1 = turnoverCalcItem1.AvgQty;
    Decimal num5 = 0M;
    Decimal? nullable2;
    if (!(nullable1.GetValueOrDefault() == num5 & nullable1.HasValue))
    {
      PXCache cache = ((PXSelectBase) this.CalcItems).Cache;
      INTurnoverCalcItem turnoverCalcItem2 = turnoverCalcItem1;
      nullable1 = turnoverCalcItem1.SoldQty;
      nullable2 = turnoverCalcItem1.AvgQty;
      // ISSUE: variable of a boxed type
      __Boxed<Decimal?> local = (ValueType) (nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() / nullable2.GetValueOrDefault()) : new Decimal?());
      cache.SetValueExt<INTurnoverCalcItem.qtyRatio>((object) turnoverCalcItem2, (object) local);
    }
    nullable2 = turnoverCalcItem1.AvgCost;
    Decimal num6 = 0M;
    if (!(nullable2.GetValueOrDefault() == num6 & nullable2.HasValue))
    {
      PXCache cache = ((PXSelectBase) this.CalcItems).Cache;
      INTurnoverCalcItem turnoverCalcItem3 = turnoverCalcItem1;
      nullable2 = turnoverCalcItem1.SoldCost;
      nullable1 = turnoverCalcItem1.AvgCost;
      // ISSUE: variable of a boxed type
      __Boxed<Decimal?> local = (ValueType) (nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() / nullable1.GetValueOrDefault()) : new Decimal?());
      cache.SetValueExt<INTurnoverCalcItem.costRatio>((object) turnoverCalcItem3, (object) local);
    }
    nullable1 = turnoverCalcItem1.SoldQty;
    Decimal num7 = 0M;
    Decimal? nullable3;
    if (!(nullable1.GetValueOrDefault() == num7 & nullable1.HasValue))
    {
      PXCache cache = ((PXSelectBase) this.CalcItems).Cache;
      INTurnoverCalcItem turnoverCalcItem4 = turnoverCalcItem1;
      nullable3 = turnoverCalcItem1.AvgQty;
      Decimal numberOfDays = (Decimal) calculationArgs.NumberOfDays;
      nullable1 = nullable3.HasValue ? new Decimal?(nullable3.GetValueOrDefault() * numberOfDays) : new Decimal?();
      nullable2 = turnoverCalcItem1.SoldQty;
      Decimal? nullable4;
      if (!(nullable1.HasValue & nullable2.HasValue))
      {
        nullable3 = new Decimal?();
        nullable4 = nullable3;
      }
      else
        nullable4 = new Decimal?(nullable1.GetValueOrDefault() / nullable2.GetValueOrDefault());
      // ISSUE: variable of a boxed type
      __Boxed<Decimal?> local = (ValueType) nullable4;
      cache.SetValueExt<INTurnoverCalcItem.qtySellDays>((object) turnoverCalcItem4, (object) local);
    }
    nullable2 = turnoverCalcItem1.SoldCost;
    Decimal num8 = 0M;
    if (!(nullable2.GetValueOrDefault() == num8 & nullable2.HasValue))
    {
      PXCache cache = ((PXSelectBase) this.CalcItems).Cache;
      INTurnoverCalcItem turnoverCalcItem5 = turnoverCalcItem1;
      nullable3 = turnoverCalcItem1.AvgCost;
      Decimal numberOfDays = (Decimal) calculationArgs.NumberOfDays;
      nullable2 = nullable3.HasValue ? new Decimal?(nullable3.GetValueOrDefault() * numberOfDays) : new Decimal?();
      nullable1 = turnoverCalcItem1.SoldCost;
      Decimal? nullable5;
      if (!(nullable2.HasValue & nullable1.HasValue))
      {
        nullable3 = new Decimal?();
        nullable5 = nullable3;
      }
      else
        nullable5 = new Decimal?(nullable2.GetValueOrDefault() / nullable1.GetValueOrDefault());
      // ISSUE: variable of a boxed type
      __Boxed<Decimal?> local = (ValueType) nullable5;
      cache.SetValueExt<INTurnoverCalcItem.costSellDays>((object) turnoverCalcItem5, (object) local);
    }
    ((PXSelectBase<INTurnoverCalcItem>) this.CalcItems).Insert(turnoverCalcItem1);
    return true;
  }

  protected virtual Decimal? CalcSoldQty(
    INTurnoverCalc calc,
    TurnoverCalcMaint.InventorySiteBalance siteBalance)
  {
    Decimal? nullable1 = siteBalance.GetSoldQty();
    Decimal? nullable2;
    Decimal? nullable3;
    Decimal? nullable4;
    if (calc.IncludedProduction.GetValueOrDefault() && calc.IncludedAssembly.GetValueOrDefault())
    {
      nullable2 = nullable1;
      nullable3 = siteBalance.GetAssemblyQty();
      nullable1 = nullable2.HasValue & nullable3.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + nullable3.GetValueOrDefault()) : new Decimal?();
    }
    else if (calc.IncludedProduction.GetValueOrDefault())
    {
      nullable3 = nullable1;
      nullable2 = siteBalance.GetProductionQty();
      nullable1 = nullable3.HasValue & nullable2.HasValue ? new Decimal?(nullable3.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
    }
    else if (calc.IncludedAssembly.GetValueOrDefault())
    {
      Decimal? nullable5 = nullable1;
      Decimal? assemblyQty = siteBalance.GetAssemblyQty();
      nullable4 = siteBalance.GetProductionQty();
      nullable3 = assemblyQty.HasValue & nullable4.HasValue ? new Decimal?(assemblyQty.GetValueOrDefault() - nullable4.GetValueOrDefault()) : new Decimal?();
      Decimal? nullable6;
      if (!(nullable5.HasValue & nullable3.HasValue))
      {
        nullable4 = new Decimal?();
        nullable6 = nullable4;
      }
      else
        nullable6 = new Decimal?(nullable5.GetValueOrDefault() + nullable3.GetValueOrDefault());
      nullable1 = nullable6;
    }
    if (calc.IncludedIssue.GetValueOrDefault())
    {
      nullable3 = nullable1;
      nullable2 = siteBalance.GetIssuedAdjustedQty();
      Decimal? nullable7;
      if (!(nullable3.HasValue & nullable2.HasValue))
      {
        nullable4 = new Decimal?();
        nullable7 = nullable4;
      }
      else
        nullable7 = new Decimal?(nullable3.GetValueOrDefault() + nullable2.GetValueOrDefault());
      nullable1 = nullable7;
    }
    if (calc.IncludedTransfer.GetValueOrDefault())
    {
      nullable2 = nullable1;
      nullable3 = siteBalance.GetTransferedQty();
      Decimal? nullable8;
      if (!(nullable2.HasValue & nullable3.HasValue))
      {
        nullable4 = new Decimal?();
        nullable8 = nullable4;
      }
      else
        nullable8 = new Decimal?(nullable2.GetValueOrDefault() + nullable3.GetValueOrDefault());
      nullable1 = nullable8;
    }
    return nullable1;
  }

  protected virtual Decimal? CalcSoldCost(
    INTurnoverCalc calc,
    TurnoverCalcMaint.InventorySiteBalance siteBalance)
  {
    Decimal? nullable1 = siteBalance.GetSoldCost();
    Decimal? nullable2;
    Decimal? nullable3;
    Decimal? nullable4;
    if (calc.IncludedProduction.GetValueOrDefault() && calc.IncludedAssembly.GetValueOrDefault())
    {
      nullable2 = nullable1;
      nullable3 = siteBalance.GetAssemblyCost();
      nullable1 = nullable2.HasValue & nullable3.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + nullable3.GetValueOrDefault()) : new Decimal?();
    }
    else if (calc.IncludedProduction.GetValueOrDefault())
    {
      nullable3 = nullable1;
      nullable2 = siteBalance.GetProductionCost();
      nullable1 = nullable3.HasValue & nullable2.HasValue ? new Decimal?(nullable3.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
    }
    else if (calc.IncludedAssembly.GetValueOrDefault())
    {
      Decimal? nullable5 = nullable1;
      Decimal? assemblyCost = siteBalance.GetAssemblyCost();
      nullable4 = siteBalance.GetProductionCost();
      nullable3 = assemblyCost.HasValue & nullable4.HasValue ? new Decimal?(assemblyCost.GetValueOrDefault() - nullable4.GetValueOrDefault()) : new Decimal?();
      Decimal? nullable6;
      if (!(nullable5.HasValue & nullable3.HasValue))
      {
        nullable4 = new Decimal?();
        nullable6 = nullable4;
      }
      else
        nullable6 = new Decimal?(nullable5.GetValueOrDefault() + nullable3.GetValueOrDefault());
      nullable1 = nullable6;
    }
    if (calc.IncludedIssue.GetValueOrDefault())
    {
      nullable3 = nullable1;
      nullable2 = siteBalance.GetIssuedAdjustedCost();
      Decimal? nullable7;
      if (!(nullable3.HasValue & nullable2.HasValue))
      {
        nullable4 = new Decimal?();
        nullable7 = nullable4;
      }
      else
        nullable7 = new Decimal?(nullable3.GetValueOrDefault() + nullable2.GetValueOrDefault());
      nullable1 = nullable7;
    }
    if (calc.IncludedTransfer.GetValueOrDefault())
    {
      nullable2 = nullable1;
      nullable3 = siteBalance.GetTransferedCost();
      Decimal? nullable8;
      if (!(nullable2.HasValue & nullable3.HasValue))
      {
        nullable4 = new Decimal?();
        nullable8 = nullable4;
      }
      else
        nullable8 = new Decimal?(nullable2.GetValueOrDefault() + nullable3.GetValueOrDefault());
      nullable1 = nullable8;
    }
    return nullable1;
  }

  protected virtual void AddVirtualInventoryTurnover(
    TurnoverCalculationArgs calculationArgs,
    int? inventoryID)
  {
    int? siteID = new int?();
    if (calculationArgs.SiteID.HasValue)
    {
      siteID = calculationArgs.SiteID;
    }
    else
    {
      PXViewOf<INSite>.BasedOn<SelectFromBase<INSite, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INSite.active, Equal<True>>>>>.And<BqlOperand<INSite.branchID, IBqlInt>.IsEqual<P.AsInt>>>>.ReadOnly readOnly = new PXViewOf<INSite>.BasedOn<SelectFromBase<INSite, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INSite.active, Equal<True>>>>>.And<BqlOperand<INSite.branchID, IBqlInt>.IsEqual<P.AsInt>>>>.ReadOnly((PXGraph) this);
      using (new PXFieldScope(((PXSelectBase) readOnly).View, new Type[1]
      {
        typeof (INSite.siteID)
      }))
        siteID = (int?) PXResultset<INSite>.op_Implicit(((PXSelectBase<INSite>) readOnly).SelectWindowed(0, 1, new object[1]
        {
          (object) calculationArgs.BranchID
        }))?.SiteID;
    }
    if (!siteID.HasValue)
      return;
    INTurnoverCalcItem turnoverCalcItem = this.CreateTurnoverCalcItem(calculationArgs, inventoryID, siteID);
    turnoverCalcItem.IsVirtual = new bool?(true);
    ((PXSelectBase<INTurnoverCalcItem>) this.CalcItems).Insert(turnoverCalcItem);
  }

  protected virtual int?[] LoadStockItems(TurnoverCalculationArgs calculationArgs)
  {
    PXViewOf<PX.Objects.IN.InventoryItem>.BasedOn<SelectFromBase<PX.Objects.IN.InventoryItem, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.IN.InventoryItem.stkItem, Equal<True>>>>, And<BqlOperand<PX.Objects.IN.InventoryItem.isTemplate, IBqlBool>.IsEqual<False>>>, And<BqlOperand<PX.Objects.IN.InventoryItem.itemStatus, IBqlString>.IsNotIn<InventoryItemStatus.unknown, InventoryItemStatus.markedForDeletion>>>>.And<Match<PX.Objects.IN.InventoryItem, Current<AccessInfo.userName>>>>>.ReadOnly readOnly = new PXViewOf<PX.Objects.IN.InventoryItem>.BasedOn<SelectFromBase<PX.Objects.IN.InventoryItem, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.IN.InventoryItem.stkItem, Equal<True>>>>, And<BqlOperand<PX.Objects.IN.InventoryItem.isTemplate, IBqlBool>.IsEqual<False>>>, And<BqlOperand<PX.Objects.IN.InventoryItem.itemStatus, IBqlString>.IsNotIn<InventoryItemStatus.unknown, InventoryItemStatus.markedForDeletion>>>>.And<Match<PX.Objects.IN.InventoryItem, Current<AccessInfo.userName>>>>>.ReadOnly((PXGraph) this);
    List<object> objectList = new List<object>();
    if (calculationArgs.ItemClassID.HasValue)
    {
      ((PXSelectBase<PX.Objects.IN.InventoryItem>) readOnly).WhereAnd<Where<BqlOperand<PX.Objects.IN.InventoryItem.itemClassID, IBqlInt>.IsEqual<P.AsInt>>>();
      objectList.Add((object) calculationArgs.ItemClassID);
    }
    int?[] inventories = calculationArgs.Inventories;
    if ((inventories != null ? (inventories.Length != 0 ? 1 : 0) : 0) != 0)
    {
      ((PXSelectBase<PX.Objects.IN.InventoryItem>) readOnly).WhereAnd<Where<BqlOperand<PX.Objects.IN.InventoryItem.inventoryID, IBqlInt>.IsIn<P.AsInt>>>();
      objectList.Add((object) calculationArgs.Inventories);
    }
    PX.Objects.IN.InventoryItem[] source;
    using (new PXFieldScope(((PXSelectBase) readOnly).View, new Type[1]
    {
      typeof (PX.Objects.IN.InventoryItem.inventoryID)
    }))
      source = ((PXSelectBase<PX.Objects.IN.InventoryItem>) readOnly).SelectMain(objectList.ToArray());
    return ((IEnumerable<PX.Objects.IN.InventoryItem>) source).Select<PX.Objects.IN.InventoryItem, int?>((Func<PX.Objects.IN.InventoryItem, int?>) (x => x.InventoryID)).ToArray<int?>();
  }

  protected virtual INTurnoverCalcItemHist[] LoadTurnoverCalcItemHist(
    TurnoverCalculationArgs calculationArgs,
    int? inventoryID)
  {
    PXViewOf<INTurnoverCalcItemHist>.BasedOn<SelectFromBase<INTurnoverCalcItemHist, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INTurnoverCalcItemHist.inventoryID, Equal<P.AsInt>>>>, And<BqlOperand<INTurnoverCalcItemHist.branchID, IBqlInt>.IsEqual<P.AsInt>>>, And<BqlOperand<INTurnoverCalcItemHist.finPeriodID, IBqlString>.IsGreaterEqual<P.AsString.ASCII>>>>.And<BqlOperand<INTurnoverCalcItemHist.finPeriodID, IBqlString>.IsLessEqual<P.AsString.ASCII>>>>.ReadOnly readOnly = new PXViewOf<INTurnoverCalcItemHist>.BasedOn<SelectFromBase<INTurnoverCalcItemHist, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INTurnoverCalcItemHist.inventoryID, Equal<P.AsInt>>>>, And<BqlOperand<INTurnoverCalcItemHist.branchID, IBqlInt>.IsEqual<P.AsInt>>>, And<BqlOperand<INTurnoverCalcItemHist.finPeriodID, IBqlString>.IsGreaterEqual<P.AsString.ASCII>>>>.And<BqlOperand<INTurnoverCalcItemHist.finPeriodID, IBqlString>.IsLessEqual<P.AsString.ASCII>>>>.ReadOnly((PXGraph) this);
    List<object> objectList = new List<object>()
    {
      (object) inventoryID,
      (object) calculationArgs.BranchID,
      (object) calculationArgs.FromPeriodID,
      (object) calculationArgs.ToPeriodID
    };
    if (calculationArgs.SiteID.HasValue)
    {
      ((PXSelectBase<INTurnoverCalcItemHist>) readOnly).WhereAnd<Where<BqlOperand<INTurnoverCalcItemHist.siteID, IBqlInt>.IsEqual<P.AsInt>>>();
      objectList.Add((object) calculationArgs.SiteID);
    }
    return ((PXSelectBase<INTurnoverCalcItemHist>) readOnly).SelectMain(objectList.ToArray());
  }

  protected virtual Decimal? CalculateAverage(
    IEnumerable<TurnoverCalcMaint.InventorySitePeriodBalance> balances,
    Func<TurnoverCalcMaint.InventorySitePeriodBalance, Decimal?> begValue,
    Func<TurnoverCalcMaint.InventorySitePeriodBalance, Decimal?> ytdValue)
  {
    Decimal? nullable1 = new Decimal?(0M);
    TurnoverCalcMaint.InventorySitePeriodBalance sitePeriodBalance1 = balances.First<TurnoverCalcMaint.InventorySitePeriodBalance>();
    TurnoverCalcMaint.InventorySitePeriodBalance sitePeriodBalance2 = balances.Last<TurnoverCalcMaint.InventorySitePeriodBalance>();
    foreach (TurnoverCalcMaint.InventorySitePeriodBalance balance in balances)
    {
      if (balance == sitePeriodBalance1)
      {
        Decimal? nullable2 = nullable1;
        Decimal? nullable3 = begValue(balance);
        Decimal num = (Decimal) 2;
        Decimal? nullable4 = nullable3.HasValue ? new Decimal?(nullable3.GetValueOrDefault() / num) : new Decimal?();
        nullable1 = nullable2.HasValue & nullable4.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + nullable4.GetValueOrDefault()) : new Decimal?();
      }
      else
      {
        Decimal? nullable5 = nullable1;
        Decimal? nullable6 = begValue(balance);
        nullable1 = nullable5.HasValue & nullable6.HasValue ? new Decimal?(nullable5.GetValueOrDefault() + nullable6.GetValueOrDefault()) : new Decimal?();
      }
    }
    Decimal? nullable7 = nullable1;
    Decimal? nullable8 = ytdValue(sitePeriodBalance2);
    Decimal num1 = (Decimal) 2;
    Decimal? nullable9 = nullable8.HasValue ? new Decimal?(nullable8.GetValueOrDefault() / num1) : new Decimal?();
    Decimal? nullable10 = nullable7.HasValue & nullable9.HasValue ? new Decimal?(nullable7.GetValueOrDefault() + nullable9.GetValueOrDefault()) : new Decimal?();
    Decimal num2 = (Decimal) balances.Count<TurnoverCalcMaint.InventorySitePeriodBalance>();
    return nullable10.HasValue ? new Decimal?(nullable10.GetValueOrDefault() / num2) : new Decimal?();
  }

  protected virtual int CaclulateNumberOfDays(string fromPeriodID, string toPeriodID)
  {
    return ((IEnumerable<MasterFinPeriod>) ((PXSelectBase<MasterFinPeriod>) new PXViewOf<MasterFinPeriod>.BasedOn<SelectFromBase<MasterFinPeriod, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<MasterFinPeriod.finPeriodID, GreaterEqual<P.AsString.ASCII>>>>>.And<BqlOperand<MasterFinPeriod.finPeriodID, IBqlString>.IsLessEqual<P.AsString.ASCII>>>>.ReadOnly((PXGraph) this)).SelectMain(new object[2]
    {
      (object) fromPeriodID,
      (object) toPeriodID
    })).Sum<MasterFinPeriod>((Func<MasterFinPeriod, int>) (p =>
    {
      DateTime? nullable = p.EndDate;
      DateTime dateTime1 = nullable.Value;
      nullable = p.StartDate;
      DateTime dateTime2 = nullable.Value;
      return (int) (dateTime1 - dateTime2).TotalDays;
    }));
  }

  [DebuggerDisplay("Inventory {InventoryID}")]
  public class InventoryBalance
  {
    private Dictionary<int?, TurnoverCalcMaint.InventorySiteBalance> _siteBalances = new Dictionary<int?, TurnoverCalcMaint.InventorySiteBalance>();

    public int? InventoryID { get; }

    public InventoryBalance(int? inventoryID) => this.InventoryID = inventoryID;

    public void AddRange(INTurnoverCalcItemHist[] histRows)
    {
      foreach (INTurnoverCalcItemHist histRow in histRows)
        this.Add(histRow);
    }

    public void Add(INTurnoverCalcItemHist hist)
    {
      TurnoverCalcMaint.InventorySiteBalance inventorySiteBalance;
      if (!this._siteBalances.TryGetValue(hist.SiteID, out inventorySiteBalance))
        this._siteBalances.Add(hist.SiteID, inventorySiteBalance = new TurnoverCalcMaint.InventorySiteBalance(hist.SiteID));
      inventorySiteBalance.Add(hist);
    }
  }

  [DebuggerDisplay("Site {SiteID}")]
  public class InventorySiteBalance : 
    IEnumerable<TurnoverCalcMaint.InventorySitePeriodBalance>,
    IEnumerable
  {
    private Dictionary<string, TurnoverCalcMaint.InventorySitePeriodBalance> _periodBalances = new Dictionary<string, TurnoverCalcMaint.InventorySitePeriodBalance>();

    public int? SiteID { get; }

    public InventorySiteBalance(int? siteID) => this.SiteID = siteID;

    public bool Any()
    {
      return this._periodBalances.Any<KeyValuePair<string, TurnoverCalcMaint.InventorySitePeriodBalance>>();
    }

    public void AddRange(IEnumerable<INTurnoverCalcItemHist> histRows)
    {
      foreach (INTurnoverCalcItemHist histRow in histRows)
        this.Add(histRow);
    }

    public void Add(INTurnoverCalcItemHist hist)
    {
      TurnoverCalcMaint.InventorySitePeriodBalance sitePeriodBalance;
      if (!this._periodBalances.TryGetValue(hist.FinPeriodID, out sitePeriodBalance))
        this._periodBalances.Add(hist.FinPeriodID, sitePeriodBalance = new TurnoverCalcMaint.InventorySitePeriodBalance(hist.FinPeriodID));
      sitePeriodBalance.Add(hist);
    }

    public Decimal? GetSoldQty()
    {
      return new Decimal?(this._periodBalances.Values.Sum<TurnoverCalcMaint.InventorySitePeriodBalance>((Func<TurnoverCalcMaint.InventorySitePeriodBalance, Decimal>) (b =>
      {
        Decimal? nullable = b.Hist.FinPtdQtySales;
        Decimal valueOrDefault1 = nullable.GetValueOrDefault();
        nullable = b.Hist.FinPtdQtyCreditMemos;
        Decimal valueOrDefault2 = nullable.GetValueOrDefault();
        return valueOrDefault1 - valueOrDefault2;
      })));
    }

    public Decimal? GetSoldCost()
    {
      return new Decimal?(this._periodBalances.Values.Sum<TurnoverCalcMaint.InventorySitePeriodBalance>((Func<TurnoverCalcMaint.InventorySitePeriodBalance, Decimal>) (b =>
      {
        Decimal? nullable = b.Hist.FinPtdCOGS;
        Decimal valueOrDefault1 = nullable.GetValueOrDefault();
        nullable = b.Hist.FinPtdCOGSCredits;
        Decimal valueOrDefault2 = nullable.GetValueOrDefault();
        return valueOrDefault1 - valueOrDefault2;
      })));
    }

    public Decimal? GetProductionQty()
    {
      return new Decimal?(this._periodBalances.Values.Sum<TurnoverCalcMaint.InventorySitePeriodBalance>((Func<TurnoverCalcMaint.InventorySitePeriodBalance, Decimal>) (b => b.Hist.FinPtdQtyAMAssemblyOut.GetValueOrDefault())));
    }

    public Decimal? GetProductionCost()
    {
      return new Decimal?(this._periodBalances.Values.Sum<TurnoverCalcMaint.InventorySitePeriodBalance>((Func<TurnoverCalcMaint.InventorySitePeriodBalance, Decimal>) (b => b.Hist.FinPtdCostAMAssemblyOut.GetValueOrDefault())));
    }

    public Decimal? GetAssemblyQty()
    {
      return new Decimal?(this._periodBalances.Values.Sum<TurnoverCalcMaint.InventorySitePeriodBalance>((Func<TurnoverCalcMaint.InventorySitePeriodBalance, Decimal>) (b => b.Hist.FinPtdQtyAssemblyOut.GetValueOrDefault())));
    }

    public Decimal? GetAssemblyCost()
    {
      return new Decimal?(this._periodBalances.Values.Sum<TurnoverCalcMaint.InventorySitePeriodBalance>((Func<TurnoverCalcMaint.InventorySitePeriodBalance, Decimal>) (b => b.Hist.FinPtdCostAssemblyOut.GetValueOrDefault())));
    }

    public Decimal? GetIssuedAdjustedQty()
    {
      return new Decimal?(this._periodBalances.Values.Sum<TurnoverCalcMaint.InventorySitePeriodBalance>((Func<TurnoverCalcMaint.InventorySitePeriodBalance, Decimal>) (b => b.Hist.FinPtdQtyIssued.GetValueOrDefault())));
    }

    public Decimal? GetIssuedAdjustedCost()
    {
      return new Decimal?(this._periodBalances.Values.Sum<TurnoverCalcMaint.InventorySitePeriodBalance>((Func<TurnoverCalcMaint.InventorySitePeriodBalance, Decimal>) (b => b.Hist.FinPtdCostIssued.GetValueOrDefault())));
    }

    public Decimal? GetTransferedQty()
    {
      return new Decimal?(this._periodBalances.Values.Sum<TurnoverCalcMaint.InventorySitePeriodBalance>((Func<TurnoverCalcMaint.InventorySitePeriodBalance, Decimal>) (b => b.Hist.FinPtdQtyTransferOut.GetValueOrDefault())));
    }

    public Decimal? GetTransferedCost()
    {
      return new Decimal?(this._periodBalances.Values.Sum<TurnoverCalcMaint.InventorySitePeriodBalance>((Func<TurnoverCalcMaint.InventorySitePeriodBalance, Decimal>) (b => b.Hist.FinPtdCostTransferOut.GetValueOrDefault())));
    }

    public IEnumerator<TurnoverCalcMaint.InventorySitePeriodBalance> GetEnumerator()
    {
      return (IEnumerator<TurnoverCalcMaint.InventorySitePeriodBalance>) this._periodBalances.Values.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return ((IEnumerable) this._periodBalances.Values).GetEnumerator();
    }

    public TurnoverCalcMaint.InventorySitePeriodBalance this[string finPeriod]
    {
      get => this._periodBalances[finPeriod];
    }
  }

  [DebuggerDisplay("Period {FinPeriodID}: Qty = {_hist.FinBegQty} => {_hist.FinYtdQty}, Cost = {_hist.FinBegCost} => {_hist.FinYtdCost}, Sold = {_hist.FinPtdQtySales} items on {_hist.FinPtdCOGS}")]
  public class InventorySitePeriodBalance
  {
    private INTurnoverCalcItemHist _hist;

    public string FinPeriodID { get; }

    public INTurnoverCalcItemHist Hist => this._hist;

    public InventorySitePeriodBalance(string finPeriodID) => this.FinPeriodID = finPeriodID;

    public void Add(INTurnoverCalcItemHist hist)
    {
      INTurnoverCalcItemHist turnoverCalcItemHist = this._hist;
      if (turnoverCalcItemHist == null)
        turnoverCalcItemHist = new INTurnoverCalcItemHist()
        {
          InventoryID = hist.InventoryID,
          SiteID = hist.SiteID
        };
      this._hist = turnoverCalcItemHist;
      this._hist.FinBegQty = new Decimal?(this._hist.FinBegQty.GetValueOrDefault() + hist.FinBegQty.GetValueOrDefault());
      INTurnoverCalcItemHist hist1 = this._hist;
      Decimal? finYtdQty = this._hist.FinYtdQty;
      Decimal valueOrDefault1 = finYtdQty.GetValueOrDefault();
      finYtdQty = hist.FinYtdQty;
      Decimal valueOrDefault2 = finYtdQty.GetValueOrDefault();
      Decimal? nullable1 = new Decimal?(valueOrDefault1 + valueOrDefault2);
      hist1.FinYtdQty = nullable1;
      INTurnoverCalcItemHist hist2 = this._hist;
      Decimal? finBegCost = this._hist.FinBegCost;
      Decimal valueOrDefault3 = finBegCost.GetValueOrDefault();
      finBegCost = hist.FinBegCost;
      Decimal valueOrDefault4 = finBegCost.GetValueOrDefault();
      Decimal? nullable2 = new Decimal?(valueOrDefault3 + valueOrDefault4);
      hist2.FinBegCost = nullable2;
      INTurnoverCalcItemHist hist3 = this._hist;
      Decimal? finYtdCost = this._hist.FinYtdCost;
      Decimal valueOrDefault5 = finYtdCost.GetValueOrDefault();
      finYtdCost = hist.FinYtdCost;
      Decimal valueOrDefault6 = finYtdCost.GetValueOrDefault();
      Decimal? nullable3 = new Decimal?(valueOrDefault5 + valueOrDefault6);
      hist3.FinYtdCost = nullable3;
      INTurnoverCalcItemHist hist4 = this._hist;
      Decimal? finPtdQtySales = this._hist.FinPtdQtySales;
      Decimal valueOrDefault7 = finPtdQtySales.GetValueOrDefault();
      finPtdQtySales = hist.FinPtdQtySales;
      Decimal valueOrDefault8 = finPtdQtySales.GetValueOrDefault();
      Decimal? nullable4 = new Decimal?(valueOrDefault7 + valueOrDefault8);
      hist4.FinPtdQtySales = nullable4;
      INTurnoverCalcItemHist hist5 = this._hist;
      Decimal? finPtdCogs = this._hist.FinPtdCOGS;
      Decimal valueOrDefault9 = finPtdCogs.GetValueOrDefault();
      finPtdCogs = hist.FinPtdCOGS;
      Decimal valueOrDefault10 = finPtdCogs.GetValueOrDefault();
      Decimal? nullable5 = new Decimal?(valueOrDefault9 + valueOrDefault10);
      hist5.FinPtdCOGS = nullable5;
      INTurnoverCalcItemHist hist6 = this._hist;
      Decimal? ptdQtyCreditMemos = this._hist.FinPtdQtyCreditMemos;
      Decimal valueOrDefault11 = ptdQtyCreditMemos.GetValueOrDefault();
      ptdQtyCreditMemos = hist.FinPtdQtyCreditMemos;
      Decimal valueOrDefault12 = ptdQtyCreditMemos.GetValueOrDefault();
      Decimal? nullable6 = new Decimal?(valueOrDefault11 + valueOrDefault12);
      hist6.FinPtdQtyCreditMemos = nullable6;
      INTurnoverCalcItemHist hist7 = this._hist;
      Decimal? finPtdCogsCredits = this._hist.FinPtdCOGSCredits;
      Decimal valueOrDefault13 = finPtdCogsCredits.GetValueOrDefault();
      finPtdCogsCredits = hist.FinPtdCOGSCredits;
      Decimal valueOrDefault14 = finPtdCogsCredits.GetValueOrDefault();
      Decimal? nullable7 = new Decimal?(valueOrDefault13 + valueOrDefault14);
      hist7.FinPtdCOGSCredits = nullable7;
      INTurnoverCalcItemHist hist8 = this._hist;
      Decimal? qtyAmAssemblyOut = this._hist.FinPtdQtyAMAssemblyOut;
      Decimal valueOrDefault15 = qtyAmAssemblyOut.GetValueOrDefault();
      qtyAmAssemblyOut = hist.FinPtdQtyAMAssemblyOut;
      Decimal valueOrDefault16 = qtyAmAssemblyOut.GetValueOrDefault();
      Decimal? nullable8 = new Decimal?(valueOrDefault15 + valueOrDefault16);
      hist8.FinPtdQtyAMAssemblyOut = nullable8;
      INTurnoverCalcItemHist hist9 = this._hist;
      Decimal? costAmAssemblyOut = this._hist.FinPtdCostAMAssemblyOut;
      Decimal valueOrDefault17 = costAmAssemblyOut.GetValueOrDefault();
      costAmAssemblyOut = hist.FinPtdCostAMAssemblyOut;
      Decimal valueOrDefault18 = costAmAssemblyOut.GetValueOrDefault();
      Decimal? nullable9 = new Decimal?(valueOrDefault17 + valueOrDefault18);
      hist9.FinPtdCostAMAssemblyOut = nullable9;
      INTurnoverCalcItemHist hist10 = this._hist;
      Decimal? ptdQtyAssemblyOut = this._hist.FinPtdQtyAssemblyOut;
      Decimal valueOrDefault19 = ptdQtyAssemblyOut.GetValueOrDefault();
      ptdQtyAssemblyOut = hist.FinPtdQtyAssemblyOut;
      Decimal valueOrDefault20 = ptdQtyAssemblyOut.GetValueOrDefault();
      Decimal? nullable10 = new Decimal?(valueOrDefault19 + valueOrDefault20);
      hist10.FinPtdQtyAssemblyOut = nullable10;
      INTurnoverCalcItemHist hist11 = this._hist;
      Decimal? ptdCostAssemblyOut = this._hist.FinPtdCostAssemblyOut;
      Decimal valueOrDefault21 = ptdCostAssemblyOut.GetValueOrDefault();
      ptdCostAssemblyOut = hist.FinPtdCostAssemblyOut;
      Decimal valueOrDefault22 = ptdCostAssemblyOut.GetValueOrDefault();
      Decimal? nullable11 = new Decimal?(valueOrDefault21 + valueOrDefault22);
      hist11.FinPtdCostAssemblyOut = nullable11;
      INTurnoverCalcItemHist hist12 = this._hist;
      Decimal? finPtdQtyIssued = this._hist.FinPtdQtyIssued;
      Decimal valueOrDefault23 = finPtdQtyIssued.GetValueOrDefault();
      finPtdQtyIssued = hist.FinPtdQtyIssued;
      Decimal valueOrDefault24 = finPtdQtyIssued.GetValueOrDefault();
      Decimal? nullable12 = new Decimal?(valueOrDefault23 + valueOrDefault24);
      hist12.FinPtdQtyIssued = nullable12;
      INTurnoverCalcItemHist hist13 = this._hist;
      Decimal? finPtdCostIssued = this._hist.FinPtdCostIssued;
      Decimal valueOrDefault25 = finPtdCostIssued.GetValueOrDefault();
      finPtdCostIssued = hist.FinPtdCostIssued;
      Decimal valueOrDefault26 = finPtdCostIssued.GetValueOrDefault();
      Decimal? nullable13 = new Decimal?(valueOrDefault25 + valueOrDefault26);
      hist13.FinPtdCostIssued = nullable13;
      INTurnoverCalcItemHist hist14 = this._hist;
      Decimal? ptdQtyTransferOut = this._hist.FinPtdQtyTransferOut;
      Decimal valueOrDefault27 = ptdQtyTransferOut.GetValueOrDefault();
      ptdQtyTransferOut = hist.FinPtdQtyTransferOut;
      Decimal valueOrDefault28 = ptdQtyTransferOut.GetValueOrDefault();
      Decimal? nullable14 = new Decimal?(valueOrDefault27 + valueOrDefault28);
      hist14.FinPtdQtyTransferOut = nullable14;
      INTurnoverCalcItemHist hist15 = this._hist;
      Decimal? ptdCostTransferOut = this._hist.FinPtdCostTransferOut;
      Decimal valueOrDefault29 = ptdCostTransferOut.GetValueOrDefault();
      ptdCostTransferOut = hist.FinPtdCostTransferOut;
      Decimal valueOrDefault30 = ptdCostTransferOut.GetValueOrDefault();
      Decimal? nullable15 = new Decimal?(valueOrDefault29 + valueOrDefault30);
      hist15.FinPtdCostTransferOut = nullable15;
    }
  }
}
