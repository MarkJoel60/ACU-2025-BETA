// Decompiled with JetBrains decompiler
// Type: PX.Objects.Extensions.CostAccrual.NonStockAccrualGraph`2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using PX.Objects.IN;

#nullable disable
namespace PX.Objects.Extensions.CostAccrual;

/// <summary>This extension defines the accrual account/subaccount selection mechanism.</summary>
/// <typeparam name="TGraph">A <see cref="T:PX.Data.PXGraph" /> type.</typeparam>
/// <typeparam name="TPrimary">A DAC (a <see cref="T:PX.Data.IBqlTable" /> type).</typeparam>
public abstract class NonStockAccrualGraph<TGraph, TPrimary> : PXGraphExtension<TGraph>
  where TGraph : PXGraph
  where TPrimary : class, IBqlTable, new()
{
  public virtual object SetExpenseAccountSub(
    PXCache sender,
    PXFieldDefaultingEventArgs e,
    int? inventoryID,
    int? siteID,
    NonStockAccrualGraph<TGraph, TPrimary>.GetAccountSubUsingPostingClassDelegate GetAccountSubUsingPostingClass,
    NonStockAccrualGraph<TGraph, TPrimary>.GetAccountSubFromItemDelegate GetAccountSubFromItem)
  {
    if (inventoryID.HasValue)
    {
      InventoryItem inventoryItem = InventoryItem.PK.Find(sender.Graph, inventoryID);
      if (inventoryItem != null && !inventoryItem.StkItem.GetValueOrDefault())
        return this.SetExpenseAccountSub(sender, e, inventoryItem, siteID, GetAccountSubUsingPostingClass, GetAccountSubFromItem);
    }
    return (object) null;
  }

  public virtual object SetExpenseAccountSub(
    PXCache sender,
    PXFieldDefaultingEventArgs e,
    InventoryItem item,
    int? siteID,
    NonStockAccrualGraph<TGraph, TPrimary>.GetAccountSubUsingPostingClassDelegate GetAccountSubUsingPostingClass,
    NonStockAccrualGraph<TGraph, TPrimary>.GetAccountSubFromItemDelegate GetAccountSubFromItem)
  {
    object expenseAccountSub = this.GetExpenseAccountSub(sender, e, item, siteID, GetAccountSubUsingPostingClass, GetAccountSubFromItem);
    e.NewValue = expenseAccountSub;
    e.Cancel = true;
    return expenseAccountSub;
  }

  public virtual object GetExpenseAccountSub(
    PXCache sender,
    PXFieldDefaultingEventArgs e,
    int? inventoryID,
    int? siteID,
    NonStockAccrualGraph<TGraph, TPrimary>.GetAccountSubUsingPostingClassDelegate GetAccountSubUsingPostingClass,
    NonStockAccrualGraph<TGraph, TPrimary>.GetAccountSubFromItemDelegate GetAccountSubFromItem)
  {
    if (inventoryID.HasValue)
    {
      InventoryItem inventoryItem = InventoryItem.PK.Find(sender.Graph, inventoryID);
      if (inventoryItem != null && !inventoryItem.StkItem.GetValueOrDefault())
        return this.GetExpenseAccountSub(sender, e, inventoryItem, siteID, GetAccountSubUsingPostingClass, GetAccountSubFromItem);
    }
    return (object) null;
  }

  public virtual object GetExpenseAccountSub(
    PXCache sender,
    PXFieldDefaultingEventArgs e,
    InventoryItem item,
    int? siteID,
    NonStockAccrualGraph<TGraph, TPrimary>.GetAccountSubUsingPostingClassDelegate GetAccountSubUsingPostingClass,
    NonStockAccrualGraph<TGraph, TPrimary>.GetAccountSubFromItemDelegate GetAccountSubFromItem)
  {
    object expenseAccountSub = (object) null;
    if (item != null)
    {
      bool? nullable = item.StkItem;
      if (!nullable.GetValueOrDefault())
      {
        INSetup inSetup = (INSetup) PXSelectBase<INSetup, PXSelectReadonly<INSetup>.Config>.SelectWindowed(sender.Graph, 0, 1);
        nullable = item.NonStockReceipt;
        if (nullable.GetValueOrDefault() && PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.inventory>() && inSetup != null)
        {
          nullable = inSetup.UpdateGL;
          if (nullable.GetValueOrDefault())
          {
            INPostClass postClass = INPostClass.PK.Find(sender.Graph, item.PostClassID);
            if (postClass != null)
            {
              INSite site = INSite.PK.Find(sender.Graph, siteID);
              try
              {
                expenseAccountSub = GetAccountSubUsingPostingClass(item, site, postClass);
                goto label_10;
              }
              catch (PXMaskArgumentException ex)
              {
                goto label_10;
              }
            }
            else
            {
              expenseAccountSub = (object) null;
              goto label_10;
            }
          }
        }
        expenseAccountSub = GetAccountSubFromItem(item);
      }
    }
label_10:
    return expenseAccountSub;
  }

  public delegate object GetAccountSubUsingPostingClassDelegate(
    InventoryItem item,
    INSite site,
    INPostClass postClass)
    where TGraph : PXGraph
    where TPrimary : class, IBqlTable, new();

  public delegate object GetAccountSubFromItemDelegate(InventoryItem item)
    where TGraph : PXGraph
    where TPrimary : class, IBqlTable, new();
}
