// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INInventoryByItemClassEnq
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Common.Extensions;
using PX.Data;
using PX.Data.BQL;
using PX.Data.Maintenance;
using PX.Objects.CS;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

#nullable enable
namespace PX.Objects.IN;

public class INInventoryByItemClassEnq : PXGraph<
#nullable disable
INInventoryByItemClassEnq>
{
  private bool _allowToSyncTreeCurrentWithPrimaryViewCurrent;
  private bool _forbidToSyncTreeCurrentWithPrimaryViewCurrent;
  private bool _keepInventoryFilter;
  private readonly Lazy<bool> _timestampSelected = new Lazy<bool>((Func<bool>) (() =>
  {
    PXDatabase.SelectTimeStamp();
    return true;
  }));
  public PXSelectReadonly<INItemClass> ItemClassFilter;
  public PXSelect<INItemClass, Where<INItemClass.itemClassID, Equal<Current<INItemClass.itemClassID>>>> TreeViewAndPrimaryViewSynchronizationHelper;
  public PXFilter<INInventoryByItemClassEnq.InventoryByClassFilter> InventoryFilter;
  public PXFilter<INInventoryByItemClassEnq.ItemBuffer> CutBuffer;
  public PXSelectReadonly3<ItemClassTree.INItemClass, OrderBy<BqlField<
  #nullable enable
  ItemClassTree.INItemClass.itemClassCD, IBqlString>.Asc>> ItemClasses;
  public 
  #nullable disable
  PXSelectJoin<InventoryItem, InnerJoin<INItemClass, On<InventoryItem.FK.ItemClass>>> Inventories;
  public PXSave<INItemClass> Save;
  public PXCancel<INItemClass> Cancel;
  public PXFirst<INItemClass> First;
  public PXPrevious<INItemClass> Previous;
  public PXNext<INItemClass> Next;
  public PXLast<INItemClass> Last;
  public PXAction<INItemClass> Cut;
  public PXAction<INItemClass> Paste;
  public PXAction<INItemClass> GoToNodeSelectedInTree;

  protected virtual IEnumerable itemClasses([PXInt] int? itemClassID)
  {
    return (IEnumerable) DimensionTree<ItemClassTree, ItemClassTree.INItemClass, INItemClass.dimension, ItemClassTree.INItemClass.itemClassCD, ItemClassTree.INItemClass.itemClassID>.EnrollNodes(itemClassID);
  }

  protected virtual IEnumerable inventories()
  {
    return this.GetRelevantInventories(((PXSelectBase<INInventoryByItemClassEnq.InventoryByClassFilter>) this.InventoryFilter).Current.ShowItems);
  }

  [PXButton(ImageKey = "Cut", Tooltip = "Cut Selected Records")]
  [PXUIField(DisplayName = "Cut", Enabled = false)]
  internal IEnumerable cut(PXAdapter adapter) => this.PerformCut(adapter);

  [PXButton(ImageKey = "Paste", Tooltip = "Paste Records")]
  [PXUIField(DisplayName = "Paste", Enabled = false)]
  internal IEnumerable paste(PXAdapter adapter) => this.PerformPaste(adapter);

  [PXButton]
  [PXUIField]
  protected virtual IEnumerable goToNodeSelectedInTree(PXAdapter adapter)
  {
    INInventoryByItemClassEnq inventoryByItemClassEnq = this;
    inventoryByItemClassEnq._forbidToSyncTreeCurrentWithPrimaryViewCurrent = true;
    ((PXSelectBase<INItemClass>) inventoryByItemClassEnq.ItemClassFilter).Current = PXResultset<INItemClass>.op_Implicit(PXSelectBase<INItemClass, PXSelect<INItemClass>.Config>.Search<INItemClass.itemClassID>((PXGraph) inventoryByItemClassEnq, (object) (int?) ((PXSelectBase<ItemClassTree.INItemClass>) inventoryByItemClassEnq.ItemClasses).Current?.ItemClassID, Array.Empty<object>()));
    yield return (object) ((PXSelectBase<INItemClass>) inventoryByItemClassEnq.ItemClassFilter).Current;
  }

  /// <summary><see cref="P:PX.Objects.IN.InventoryItem.BaseItemWeight" /> CacheAttached</summary>
  [PXDBQuantity]
  [PXMergeAttributes]
  protected void InventoryItem_BaseItemWeight_CacheAttached(PXCache sender)
  {
  }

  /// <summary><see cref="P:PX.Objects.IN.InventoryItem.BaseItemVolume" /> CacheAttached</summary>
  [PXDBQuantity]
  [PXMergeAttributes]
  protected void InventoryItem_BaseItemVolume_CacheAttached(PXCache sender)
  {
  }

  /// <summary><see cref="T:PX.Objects.IN.INItemClass" /> Selected</summary>
  protected virtual void INItemClass_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    this._timestampSelected.Init<bool>();
    this.SyncTreeCurrentWithPrimaryViewCurrent((INItemClass) e.Row);
  }

  /// <summary><see cref="T:PX.Objects.IN.INItemClass" /> Updating</summary>
  protected virtual void INItemClass_RowUpdating(PXCache sender, PXRowUpdatingEventArgs e)
  {
    ((CancelEventArgs) e).Cancel = true;
  }

  /// <summary><see cref="P:PX.Objects.IN.INInventoryByItemClassEnq.InventoryByClassFilter.InventoryID" /> Updated</summary>
  protected virtual void InventoryByClassFilter_InventoryID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    INInventoryByItemClassEnq.InventoryByClassFilter row = (INInventoryByItemClassEnq.InventoryByClassFilter) e.Row;
    if (row == null || !row.InventoryID.HasValue)
      return;
    INItemClass inItemClass = PXResultset<INItemClass>.op_Implicit(PXSelectBase<INItemClass, PXSelectReadonly2<INItemClass, InnerJoin<InventoryItem, On<InventoryItem.FK.ItemClass>>, Where<InventoryItem.inventoryID, Equal<Required<InventoryItem.inventoryID>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[1]
    {
      (object) row.InventoryID
    }));
    this._allowToSyncTreeCurrentWithPrimaryViewCurrent = true;
    this._forbidToSyncTreeCurrentWithPrimaryViewCurrent = false;
    this._keepInventoryFilter = true;
    ((PXSelectBase<INItemClass>) this.ItemClassFilter).Current = inItemClass;
  }

  public virtual IEnumerable ExecuteSelect(
    string viewName,
    object[] parameters,
    object[] searches,
    string[] sortcolumns,
    bool[] descendings,
    PXFilterRow[] filters,
    ref int startRow,
    int maximumRows,
    ref int totalRows)
  {
    if (viewName == "TreeViewAndPrimaryViewSynchronizationHelper")
      this._allowToSyncTreeCurrentWithPrimaryViewCurrent = true;
    return ((PXGraph) this).ExecuteSelect(viewName, parameters, searches, sortcolumns, descendings, filters, ref startRow, maximumRows, ref totalRows);
  }

  private void SyncTreeCurrentWithPrimaryViewCurrent(INItemClass primaryViewCurrent)
  {
    if (this._allowToSyncTreeCurrentWithPrimaryViewCurrent && !this._forbidToSyncTreeCurrentWithPrimaryViewCurrent && primaryViewCurrent != null)
    {
      if (((PXSelectBase<ItemClassTree.INItemClass>) this.ItemClasses).Current != null)
      {
        int? itemClassId1 = ((PXSelectBase<ItemClassTree.INItemClass>) this.ItemClasses).Current.ItemClassID;
        int? itemClassId2 = primaryViewCurrent.ItemClassID;
        if (itemClassId1.GetValueOrDefault() == itemClassId2.GetValueOrDefault() & itemClassId1.HasValue == itemClassId2.HasValue)
          goto label_4;
      }
      ItemClassTree.INItemClass nodeById = DimensionTree<ItemClassTree, ItemClassTree.INItemClass, INItemClass.dimension, ItemClassTree.INItemClass.itemClassCD, ItemClassTree.INItemClass.itemClassID>.Instance.GetNodeByID(primaryViewCurrent.ItemClassID.GetValueOrDefault());
      ((PXSelectBase<ItemClassTree.INItemClass>) this.ItemClasses).Current = nodeById;
      ((PXSelectBase) this.ItemClasses).Cache.ActiveRow = (IBqlTable) nodeById;
    }
label_4:
    if (this._keepInventoryFilter)
      return;
    ((PXSelectBase) this.InventoryFilter).Cache.SetValueExt<INInventoryByItemClassEnq.InventoryByClassFilter.inventoryID>((object) ((PXSelectBase<INInventoryByItemClassEnq.InventoryByClassFilter>) this.InventoryFilter).Current, (object) null);
  }

  private IEnumerable GetRelevantInventories(string showItemsMode)
  {
    INInventoryByItemClassEnq inventoryByItemClassEnq = this;
    if (((PXSelectBase<ItemClassTree.INItemClass>) inventoryByItemClassEnq.ItemClasses).Current != null)
    {
      PXSelectJoin<InventoryItem, InnerJoin<INItemClass, On<InventoryItem.FK.ItemClass>>, Where2<Where<InventoryItem.inventoryID, Equal<Current<INInventoryByItemClassEnq.InventoryByClassFilter.inventoryID>>, Or<Current<INInventoryByItemClassEnq.InventoryByClassFilter.inventoryID>, IsNull>>, And<Match<Current<AccessInfo.userName>>>>> pxSelectJoin1 = new PXSelectJoin<InventoryItem, InnerJoin<INItemClass, On<InventoryItem.FK.ItemClass>>, Where2<Where<InventoryItem.inventoryID, Equal<Current<INInventoryByItemClassEnq.InventoryByClassFilter.inventoryID>>, Or<Current<INInventoryByItemClassEnq.InventoryByClassFilter.inventoryID>, IsNull>>, And<Match<Current<AccessInfo.userName>>>>>((PXGraph) inventoryByItemClassEnq);
      switch (showItemsMode)
      {
        case "C":
          ((PXSelectBase<InventoryItem>) pxSelectJoin1).WhereAnd<Where<INItemClass.itemClassID, Equal<Required<INItemClass.itemClassID>>>>();
          PXSelectJoin<InventoryItem, InnerJoin<INItemClass, On<InventoryItem.FK.ItemClass>>, Where2<Where<InventoryItem.inventoryID, Equal<Current<INInventoryByItemClassEnq.InventoryByClassFilter.inventoryID>>, Or<Current<INInventoryByItemClassEnq.InventoryByClassFilter.inventoryID>, IsNull>>, And<Match<Current<AccessInfo.userName>>>>> pxSelectJoin2 = pxSelectJoin1;
          object[] objArray1 = new object[1]
          {
            (object) ((PXSelectBase<ItemClassTree.INItemClass>) inventoryByItemClassEnq.ItemClasses).Current.ItemClassID
          };
          foreach (object relevantInventory in ((PXSelectBase<InventoryItem>) pxSelectJoin2).Select(objArray1))
            yield return relevantInventory;
          break;
        case "A":
          ((PXSelectBase<InventoryItem>) pxSelectJoin1).WhereAnd<Where<INItemClass.itemClassCD, Like<Required<INItemClass.itemClassCD>>>>();
          PXSelectJoin<InventoryItem, InnerJoin<INItemClass, On<InventoryItem.FK.ItemClass>>, Where2<Where<InventoryItem.inventoryID, Equal<Current<INInventoryByItemClassEnq.InventoryByClassFilter.inventoryID>>, Or<Current<INInventoryByItemClassEnq.InventoryByClassFilter.inventoryID>, IsNull>>, And<Match<Current<AccessInfo.userName>>>>> pxSelectJoin3 = pxSelectJoin1;
          object[] objArray2 = new object[1]
          {
            (object) ((PXSelectBase<ItemClassTree.INItemClass>) inventoryByItemClassEnq.ItemClasses).Current.ItemClassCDWildcard
          };
          foreach (object relevantInventory in ((PXSelectBase<InventoryItem>) pxSelectJoin3).Select(objArray2))
            yield return relevantInventory;
          break;
        default:
          throw new PXInvalidOperationException();
      }
    }
  }

  private IEnumerable PerformCut(PXAdapter adapter)
  {
    ((PXSelectBase) this.CutBuffer).Cache.Clear();
    bool flag1 = true;
    bool flag2 = true;
    foreach (InventoryItem inventoryItem in GraphHelper.RowCast<InventoryItem>(this.GetRelevantInventories(((PXSelectBase<INInventoryByItemClassEnq.InventoryByClassFilter>) this.InventoryFilter).Current.ShowItems)).Where<InventoryItem>((Func<InventoryItem, bool>) (i => i.Selected.GetValueOrDefault())))
    {
      INInventoryByItemClassEnq.ItemBuffer instance = (INInventoryByItemClassEnq.ItemBuffer) ((PXSelectBase) this.CutBuffer).Cache.CreateInstance();
      instance.InventoryID = inventoryItem.InventoryID;
      ((PXSelectBase) this.CutBuffer).Cache.Insert((object) instance);
      if (inventoryItem.StkItem.GetValueOrDefault())
        flag2 = false;
      else
        flag1 = false;
    }
    if (((PXSelectBase) this.CutBuffer).Cache.Cached.Count() != 0L && !flag1 && !flag2)
      throw new PXInvalidOperationException("You have selected both stock and non-stock items. They could not be moved to one item class.");
    return adapter.Get();
  }

  private IEnumerable PerformPaste(PXAdapter adapter)
  {
    INInventoryByItemClassEnq.ItemBuffer[] array1 = ((PXSelectBase) this.CutBuffer).Cache.Cached.Cast<INInventoryByItemClassEnq.ItemBuffer>().ToArray<INInventoryByItemClassEnq.ItemBuffer>();
    if (!((IEnumerable<INInventoryByItemClassEnq.ItemBuffer>) array1).Any<INInventoryByItemClassEnq.ItemBuffer>())
      return adapter.Get();
    int num = ((PXSelectBase<ItemClassTree.INItemClass>) this.ItemClasses).Current.ItemClassID.Value;
    InventoryItem[] array2 = GraphHelper.RowCast<InventoryItem>((IEnumerable) PXSelectBase<InventoryItem, PXSelectReadonly<InventoryItem, Where<InventoryItem.inventoryID, In<Required<InventoryItem.inventoryID>>>>.Config>.Select((PXGraph) this, (object[]) new object[1][]
    {
      ((IEnumerable<INInventoryByItemClassEnq.ItemBuffer>) array1).Select<INInventoryByItemClassEnq.ItemBuffer, int?>((Func<INInventoryByItemClassEnq.ItemBuffer, int?>) (b => b.InventoryID)).Cast<object>().ToArray<object>()
    })).ToArray<InventoryItem>();
    InventoryItem[] array3 = ((IEnumerable<InventoryItem>) array2).Where<InventoryItem>((Func<InventoryItem, bool>) (i =>
    {
      bool? stkItem1 = i.StkItem;
      bool? stkItem2 = ((PXSelectBase<ItemClassTree.INItemClass>) this.ItemClasses).Current.StkItem;
      return !(stkItem1.GetValueOrDefault() == stkItem2.GetValueOrDefault() & stkItem1.HasValue == stkItem2.HasValue);
    })).ToArray<InventoryItem>();
    if (((IEnumerable<InventoryItem>) array3).Any<InventoryItem>())
    {
      string str = StringExtensions.FirstSegment(((PXSelectBase<ItemClassTree.INItemClass>) this.ItemClasses).Current.SegmentedClassCD, ' ');
      if (array3.Length == 1)
        throw new PXInvalidOperationException("Inventory item {0} has not been moved to the {1} item class because moved item and the target item class should both be configured either as stock or as non-stock entities.", new object[2]
        {
          (object) array3[0].InventoryCD.TrimEnd(),
          (object) str
        });
      PXTrace.WriteInformation(((IEnumerable<InventoryItem>) array3).Aggregate<InventoryItem, StringBuilder, string>(new StringBuilder().AppendLine(PXMessages.LocalizeFormatNoPrefix("Inventory items that cannot be moved to the {0} item class:", new object[1]
      {
        (object) str
      })), (Func<StringBuilder, InventoryItem, StringBuilder>) ((sb, item) => sb.AppendLine(item.InventoryCD.TrimEnd())), (Func<StringBuilder, string>) (sb => sb.ToString())));
      throw new PXInvalidOperationException("Inventory items have not been moved to the {0} item class because all moved items and the target item class should both be configured either as stock or as non-stock entities. See trace for details.", new object[1]
      {
        (object) str
      });
    }
    bool flag = ((PXSelectBase<InventoryItem>) this.Inventories).Ask("Warning", "Please confirm if you want to update current Item settings with the Inventory Class defaults. Original settings will be preserved otherwise.", (MessageButtons) 4) == 6;
    Lazy<InventoryItemMaint> lazy1 = new Lazy<InventoryItemMaint>(new Func<InventoryItemMaint>(PXGraph.CreateInstance<InventoryItemMaint>));
    Lazy<NonStockItemMaint> lazy2 = new Lazy<NonStockItemMaint>(new Func<NonStockItemMaint>(PXGraph.CreateInstance<NonStockItemMaint>));
    foreach (InventoryItem inventoryItem in array2)
    {
      if (flag)
      {
        InventoryItemMaintBase inventoryItemMaintBase = inventoryItem.StkItem.GetValueOrDefault() ? (InventoryItemMaintBase) lazy1.Value : (InventoryItemMaintBase) lazy2.Value;
        ((PXGraph) inventoryItemMaintBase).TimeStamp = ((PXGraph) this).TimeStamp;
        ((PXSelectBase<InventoryItem>) inventoryItemMaintBase.Item).Current = inventoryItem;
        using (FieldEditPreventerExt.MakeRuleWeakeningScopeFor<InventoryItem.lotSerClassID>((PXGraph) inventoryItemMaintBase, (RuleWeakenLevel) 1))
        {
          using (FieldEditPreventerExt.MakeRuleWeakeningScopeFor<InventoryItem.baseUnit>((PXGraph) inventoryItemMaintBase, (RuleWeakenLevel) 1))
          {
            using (FieldEditPreventerExt.MakeRuleWeakeningScopeFor<InventoryItem.decimalBaseUnit>((PXGraph) lazy1.Value, (RuleWeakenLevel) 1))
            {
              InventoryItem copy = (InventoryItem) ((PXSelectBase) inventoryItemMaintBase.Item).Cache.CreateCopy((object) inventoryItem);
              copy.ItemClassID = new int?(num);
              ((PXSelectBase<InventoryItem>) inventoryItemMaintBase.Item).Update(copy);
            }
          }
        }
        ((PXGraph) inventoryItemMaintBase).Actions.PressSave();
      }
      else
      {
        inventoryItem.ItemClassID = new int?(num);
        ((PXSelectBase) this.Inventories).Cache.Update((object) inventoryItem);
      }
      ((PXSelectBase<InventoryItem>) this.Inventories).SetValueExt<InventoryItem.selected>(((PXSelectBase<InventoryItem>) this.Inventories).Locate(inventoryItem), (object) false);
    }
    ((PXSelectBase) this.CutBuffer).Cache.Clear();
    if (flag)
      ((PXGraph) this).Actions.PressCancel();
    else
      ((PXGraph) this).Actions.PressSave();
    return adapter.Get();
  }

  public static class ShowItemsMode
  {
    public const string ChildrenOfCurrentClass = "C";
    public const string AllChildren = "A";

    public class ListAttribute : PXStringListAttribute
    {
      public ListAttribute()
        : base(new string[2]{ "C", "A" }, new string[2]
        {
          "Related to Only the Current Item Class",
          "Related to the Current and Child Item Classes"
        })
      {
      }
    }
  }

  [Serializable]
  public class InventoryByClassFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [Inventory]
    public virtual int? InventoryID { get; set; }

    [PXString(1, IsFixed = true)]
    [PXUIField(DisplayName = "Show Items")]
    [INInventoryByItemClassEnq.ShowItemsMode.List]
    [PXDefault("C")]
    public virtual string ShowItems { get; set; }

    public abstract class inventoryID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      INInventoryByItemClassEnq.InventoryByClassFilter.inventoryID>
    {
    }

    public abstract class showItems : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      INInventoryByItemClassEnq.InventoryByClassFilter.showItems>
    {
    }
  }

  [Serializable]
  public class INItemClassFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXInt]
    [PXUIField]
    [PXDimensionSelector("INITEMCLASS", typeof (Search<INItemClass.itemClassID, Where<INItemClass.stkItem, Equal<False>, Or<Where<INItemClass.stkItem, Equal<True>, And<FeatureInstalled<FeaturesSet.distributionModule>>>>>>), typeof (INItemClass.itemClassCD), DescriptionField = typeof (INItemClass.descr), ValidComboRequired = true)]
    public virtual int? ItemClassID { get; set; }

    public abstract class itemClassID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      INInventoryByItemClassEnq.INItemClassFilter.itemClassID>
    {
    }
  }

  [PXHidden]
  [Serializable]
  public class ItemBuffer : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXInt(IsKey = true)]
    [PXDefault]
    public int? InventoryID { get; set; }

    public abstract class inventoryID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      INInventoryByItemClassEnq.ItemBuffer.inventoryID>
    {
    }
  }
}
