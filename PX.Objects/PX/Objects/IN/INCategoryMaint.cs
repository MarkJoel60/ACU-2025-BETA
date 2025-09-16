// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INCategoryMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.Api.Export;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace PX.Objects.IN;

[NonOptimizable(IgnoreOptimizationBehavior = true)]
public class INCategoryMaint : PXGraph<
#nullable disable
INCategoryMaint>
{
  public PXFilter<INCategoryMaint.ClassFilter> ClassInfo;
  public PXFilter<INCategoryMaint.SelectedNode> SelectedFolders;
  public PXSelectOrderBy<INCategory, OrderBy<Asc<INCategory.sortOrder>>> Folders;
  public PXSelect<INCategory, Where<INCategory.categoryID, Equal<Current<INCategory.categoryID>>>> CurrentCategory;
  public PXSelectJoin<INItemCategory, LeftJoin<InventoryItem, On<INItemCategory.FK.InventoryItem>>, Where<INItemCategory.categoryID, Equal<Current<INCategory.categoryID>>>> Members;
  public PXSelectOrderBy<INCategoryMaint.INFolderCategory, OrderBy<Asc<INCategory.sortOrder>>> ParentFolders;
  public PXFilter<INItemCategoryBuffer> Buffer;
  [PXHidden]
  public PXSelect<InventoryItem, Where<InventoryItem.inventoryID, Equal<Optional<INItemCategory.inventoryID>>>> RelatedInventoryItem;
  public PXSave<INCategoryMaint.SelectedNode> Save;
  public PXCancel<INCategoryMaint.SelectedNode> Cancel;
  public PXAction<INCategoryMaint.SelectedNode> AddCategory;
  public PXAction<INCategoryMaint.SelectedNode> DeleteCategory;
  public PXAction<INCategoryMaint.SelectedNode> down;
  public PXAction<INCategoryMaint.SelectedNode> up;
  public PXAction<INCategoryMaint.SelectedNode> Copy;
  public PXAction<INCategoryMaint.SelectedNode> Cut;
  public PXAction<INCategoryMaint.SelectedNode> Paste;
  public PXAction<INCategoryMaint.SelectedNode> AddItemsbyClass;
  public PXAction<INCategoryMaint.SelectedNode> ViewDetails;

  [PXMergeAttributes]
  [INCategoryMaint.INCategorySelector]
  protected virtual void _(Events.CacheAttached<INCategory.parentID> e)
  {
  }

  protected virtual IEnumerable folders([PXInt] int? categoryID)
  {
    return (IEnumerable) this.GetFolders(categoryID);
  }

  protected virtual IEnumerable currentCategory() => (IEnumerable) this.GetCurrentCategory();

  protected virtual IEnumerable members() => this.GetMembers();

  protected virtual IEnumerable parentFolders([PXInt] int? categoryID)
  {
    return (IEnumerable) this.GetParentFolders(categoryID);
  }

  private IEnumerable<INCategory> GetFolders(int? categoryID)
  {
    INCategoryMaint graph = this;
    if (!categoryID.HasValue)
      yield return new INCategory()
      {
        CategoryID = new int?(0),
        Description = PXSiteMap.RootNode.Title
      };
    foreach (INCategory child in INCategoryMaint.CategoryCache<INCategory>.GetChildren((PXGraph) graph, categoryID))
    {
      if (!string.IsNullOrEmpty(child.Description))
        yield return child;
    }
  }

  private IEnumerable<INCategory> GetCurrentCategory()
  {
    INCategoryMaint inCategoryMaint1 = this;
    if (((PXSelectBase<INCategory>) inCategoryMaint1.Folders).Current != null)
    {
      PXUIFieldAttribute.SetEnabled<INCategory.description>(((PXGraph) inCategoryMaint1).Caches[typeof (INCategory)], (object) null, ((PXSelectBase<INCategory>) inCategoryMaint1.Folders).Current.ParentID.HasValue);
      PXUIFieldAttribute.SetEnabled<INCategory.parentID>(((PXGraph) inCategoryMaint1).Caches[typeof (INCategory)], (object) null, ((PXSelectBase<INCategory>) inCategoryMaint1.Folders).Current.ParentID.HasValue);
      ((PXGraph) inCategoryMaint1).Caches[typeof (INItemCategory)].AllowInsert = ((PXSelectBase<INCategory>) inCategoryMaint1.Folders).Current.ParentID.HasValue;
      ((PXGraph) inCategoryMaint1).Caches[typeof (INItemCategory)].AllowDelete = ((PXSelectBase<INCategory>) inCategoryMaint1.Folders).Current.ParentID.HasValue;
      ((PXGraph) inCategoryMaint1).Caches[typeof (INItemCategory)].AllowUpdate = ((PXSelectBase<INCategory>) inCategoryMaint1.Folders).Current.ParentID.HasValue;
      PXAction action1 = ((PXGraph) inCategoryMaint1).Actions["Copy"];
      int? parentId = ((PXSelectBase<INCategory>) inCategoryMaint1.Folders).Current.ParentID;
      int num1 = parentId.HasValue ? 1 : 0;
      action1.SetEnabled(num1 != 0);
      PXAction action2 = ((PXGraph) inCategoryMaint1).Actions["Cut"];
      parentId = ((PXSelectBase<INCategory>) inCategoryMaint1.Folders).Current.ParentID;
      int num2 = parentId.HasValue ? 1 : 0;
      action2.SetEnabled(num2 != 0);
      PXAction action3 = ((PXGraph) inCategoryMaint1).Actions["Paste"];
      parentId = ((PXSelectBase<INCategory>) inCategoryMaint1.Folders).Current.ParentID;
      int num3 = parentId.HasValue ? 1 : 0;
      action3.SetEnabled(num3 != 0);
      PXAction action4 = ((PXGraph) inCategoryMaint1).Actions["AddItemsbyClass"];
      parentId = ((PXSelectBase<INCategory>) inCategoryMaint1.Folders).Current.ParentID;
      int num4 = parentId.HasValue ? 1 : 0;
      action4.SetEnabled(num4 != 0);
      INCategoryMaint inCategoryMaint2 = inCategoryMaint1;
      object[] objArray = new object[1]
      {
        (object) ((PXSelectBase<INCategory>) inCategoryMaint1.Folders).Current.CategoryID
      };
      foreach (PXResult<INCategory> pxResult in PXSelectBase<INCategory, PXSelect<INCategory, Where<INCategory.categoryID, Equal<Required<INCategory.categoryID>>>>.Config>.Select((PXGraph) inCategoryMaint2, objArray))
        yield return PXResult<INCategory>.op_Implicit(pxResult);
    }
  }

  private IEnumerable GetMembers()
  {
    INCategoryMaint inCategoryMaint = this;
    if (((PXSelectBase<INCategory>) inCategoryMaint.Folders).Current != null)
    {
      PXUIFieldAttribute.SetEnabled<INCategory.description>(((PXGraph) inCategoryMaint).Caches[typeof (INCategory)], (object) null, ((PXSelectBase<INCategory>) inCategoryMaint.Folders).Current.ParentID.HasValue);
      PXUIFieldAttribute.SetEnabled<INCategory.parentID>(((PXGraph) inCategoryMaint).Caches[typeof (INCategory)], (object) null, ((PXSelectBase<INCategory>) inCategoryMaint.Folders).Current.ParentID.HasValue);
      ((PXGraph) inCategoryMaint).Caches[typeof (INItemCategory)].AllowInsert = ((PXSelectBase<INCategory>) inCategoryMaint.Folders).Current.ParentID.HasValue;
      ((PXGraph) inCategoryMaint).Caches[typeof (INItemCategory)].AllowUpdate = ((PXSelectBase<INCategory>) inCategoryMaint.Folders).Current.ParentID.HasValue;
      PXAction action1 = ((PXGraph) inCategoryMaint).Actions["Copy"];
      int? parentId = ((PXSelectBase<INCategory>) inCategoryMaint.Folders).Current.ParentID;
      int num1 = parentId.HasValue ? 1 : 0;
      action1.SetEnabled(num1 != 0);
      PXAction action2 = ((PXGraph) inCategoryMaint).Actions["Cut"];
      parentId = ((PXSelectBase<INCategory>) inCategoryMaint.Folders).Current.ParentID;
      int num2 = parentId.HasValue ? 1 : 0;
      action2.SetEnabled(num2 != 0);
      PXAction action3 = ((PXGraph) inCategoryMaint).Actions["Paste"];
      parentId = ((PXSelectBase<INCategory>) inCategoryMaint.Folders).Current.ParentID;
      int num3 = parentId.HasValue ? 1 : 0;
      action3.SetEnabled(num3 != 0);
      PXAction action4 = ((PXGraph) inCategoryMaint).Actions["AddItemsbyClass"];
      parentId = ((PXSelectBase<INCategory>) inCategoryMaint.Folders).Current.ParentID;
      int num4 = parentId.HasValue ? 1 : 0;
      action4.SetEnabled(num4 != 0);
      PXSelectBase<INItemCategory> pxSelectBase = (PXSelectBase<INItemCategory>) new PXSelectJoin<INItemCategory, LeftJoin<InventoryItem, On<INItemCategory.FK.InventoryItem>>, Where<INItemCategory.categoryID, Equal<Required<INCategory.categoryID>>>>((PXGraph) inCategoryMaint);
      int startRow = PXView.StartRow;
      int num5 = 0;
      PXView view = ((PXSelectBase) pxSelectBase).View;
      object[] currents = PXView.Currents;
      object[] objArray = new object[1]
      {
        (object) ((PXSelectBase<INCategory>) inCategoryMaint.Folders).Current.CategoryID
      };
      object[] searches = PXView.Searches;
      string[] sortColumns = PXView.SortColumns;
      bool[] descendings = PXView.Descendings;
      PXFilterRow[] pxFilterRowArray = PXView.PXFilterRowCollection.op_Implicit(PXView.Filters);
      ref int local1 = ref startRow;
      int maximumRows = PXView.MaximumRows;
      ref int local2 = ref num5;
      foreach (object member in view.Select(currents, objArray, searches, sortColumns, descendings, pxFilterRowArray, ref local1, maximumRows, ref local2))
      {
        yield return member;
        PXView.StartRow = 0;
      }
    }
  }

  private IEnumerable<INCategoryMaint.INFolderCategory> GetParentFolders(int? categoryID)
  {
    INCategoryMaint graph = this;
    if (!categoryID.HasValue)
    {
      INCategoryMaint.INFolderCategory parentFolder = new INCategoryMaint.INFolderCategory();
      parentFolder.CategoryID = new int?(0);
      parentFolder.Description = PXSiteMap.RootNode.Title;
      yield return parentFolder;
    }
    foreach (INCategoryMaint.INFolderCategory child in INCategoryMaint.CategoryCache<INCategoryMaint.INFolderCategory>.GetChildren((PXGraph) graph, categoryID))
    {
      if (!string.IsNullOrEmpty(child.Description))
      {
        int? categoryId1 = child.CategoryID;
        int? categoryId2 = ((PXSelectBase<INCategory>) graph.Folders).Current.CategoryID;
        if (!(categoryId1.GetValueOrDefault() == categoryId2.GetValueOrDefault() & categoryId1.HasValue == categoryId2.HasValue))
          yield return child;
      }
    }
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable addCategory(PXAdapter adapter)
  {
    int num1 = ((PXSelectBase<INCategory>) this.Folders).Current.CategoryID.Value;
    INCategory inCategory1 = GraphHelper.Caches<INCategory>((PXGraph) this).Insert(new INCategory()
    {
      Description = PXMessages.LocalizeNoPrefix("<NEW>"),
      ParentID = new int?(num1)
    });
    inCategory1.TempChildID = inCategory1.CategoryID;
    inCategory1.TempParentID = new int?(num1);
    INCategory inCategory2 = PXResultset<INCategory>.op_Implicit(PXSelectBase<INCategory, PXSelect<INCategory, Where<INCategory.parentID, Equal<Required<INCategory.parentID>>>, OrderBy<Desc<INCategory.sortOrder>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[1]
    {
      (object) num1
    }));
    int num2 = inCategory2.SortOrder.Value + 1;
    inCategory1.SortOrder = new int?(inCategory2 != null ? num2 : 1);
    ((PXSelectBase) this.Folders).Cache.ActiveRow = (IBqlTable) inCategory1;
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable deleteCategory(PXAdapter adapter)
  {
    GraphHelper.Caches<INCategory>((PXGraph) this).Delete(((PXSelectBase<INCategory>) this.Folders).Current);
    return adapter.Get();
  }

  [PXUIField]
  [PXButton(ImageKey = "ArrowDown")]
  public virtual IEnumerable Down(PXAdapter adapter)
  {
    INCategory current = ((PXSelectBase<INCategory>) this.Folders).Current;
    INCategory inCategory = PXResultset<INCategory>.op_Implicit(PXSelectBase<INCategory, PXSelect<INCategory, Where<INCategory.parentID, Equal<Required<INCategory.parentID>>, And<INCategory.sortOrder, Greater<Required<INCategory.parentID>>>>, OrderBy<Asc<INCategory.sortOrder>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[2]
    {
      (object) ((PXSelectBase<INCategory>) this.Folders).Current.ParentID,
      (object) ((PXSelectBase<INCategory>) this.Folders).Current.SortOrder
    }));
    if (inCategory != null && current != null)
    {
      int num = current.SortOrder.Value;
      current.SortOrder = inCategory.SortOrder;
      inCategory.SortOrder = new int?(num);
      GraphHelper.Caches<INCategory>((PXGraph) this).Update(inCategory);
      GraphHelper.Caches<INCategory>((PXGraph) this).Update(current);
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton(ImageKey = "ArrowUp")]
  public virtual IEnumerable Up(PXAdapter adapter)
  {
    INCategory current = ((PXSelectBase<INCategory>) this.Folders).Current;
    INCategory inCategory = PXResultset<INCategory>.op_Implicit(PXSelectBase<INCategory, PXSelect<INCategory, Where<INCategory.parentID, Equal<Required<INCategory.parentID>>, And<INCategory.sortOrder, Less<Required<INCategory.parentID>>>>, OrderBy<Desc<INCategory.sortOrder>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[2]
    {
      (object) ((PXSelectBase<INCategory>) this.Folders).Current.ParentID,
      (object) ((PXSelectBase<INCategory>) this.Folders).Current.SortOrder
    }));
    if (inCategory != null && current != null)
    {
      int num = current.SortOrder.Value;
      current.SortOrder = inCategory.SortOrder;
      inCategory.SortOrder = new int?(num);
      GraphHelper.Caches<INCategory>((PXGraph) this).Update(inCategory);
      GraphHelper.Caches<INCategory>((PXGraph) this).Update(current);
    }
    return adapter.Get();
  }

  [PXButton(ImageKey = "Copy", Tooltip = "Copy selected records.")]
  [PXUIField(DisplayName = "Copy", Enabled = false)]
  public IEnumerable copy(PXAdapter adapter)
  {
    ((PXSelectBase) this.Buffer).Cache.Clear();
    foreach (PXResult<INItemCategory> pxResult in PXSelectBase<INItemCategory, PXSelect<INItemCategory, Where<INItemCategory.categoryID, Equal<Required<INItemCategory.categoryID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) ((PXSelectBase<INCategory>) this.Folders).Current.CategoryID
    }))
    {
      INItemCategory inItemCategory = PXResult<INItemCategory>.op_Implicit(pxResult);
      if (inItemCategory.CategorySelected.GetValueOrDefault())
      {
        INItemCategoryBuffer instance = (INItemCategoryBuffer) ((PXSelectBase) this.Buffer).Cache.CreateInstance();
        instance.InventoryID = inItemCategory.InventoryID;
        ((PXSelectBase<INItemCategoryBuffer>) this.Buffer).Insert(instance);
      }
    }
    return adapter.Get();
  }

  [PXButton(ImageKey = "Cut", Tooltip = "Cut selected records.")]
  [PXUIField(DisplayName = "Cut", Enabled = false)]
  internal IEnumerable cut(PXAdapter adapter)
  {
    ((PXSelectBase) this.Buffer).Cache.Clear();
    List<INItemCategory> inItemCategoryList = new List<INItemCategory>();
    foreach (PXResult<INItemCategory> pxResult in PXSelectBase<INItemCategory, PXSelect<INItemCategory, Where<INItemCategory.categoryID, Equal<Required<INItemCategory.categoryID>>>, OrderBy<Asc<InventoryItem.inventoryCD>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) ((PXSelectBase<INCategory>) this.Folders).Current.CategoryID
    }))
    {
      INItemCategory inItemCategory = PXResult<INItemCategory>.op_Implicit(pxResult);
      if (inItemCategory.CategorySelected.GetValueOrDefault())
      {
        INItemCategoryBuffer instance = (INItemCategoryBuffer) ((PXSelectBase) this.Buffer).Cache.CreateInstance();
        instance.InventoryID = inItemCategory.InventoryID;
        ((PXSelectBase<INItemCategoryBuffer>) this.Buffer).Insert(instance);
        inItemCategoryList.Add(inItemCategory);
      }
    }
    foreach (INItemCategory inItemCategory in inItemCategoryList)
      ((PXSelectBase<INItemCategory>) this.Members).Delete(inItemCategory);
    return adapter.Get();
  }

  [PXButton(ImageKey = "Paste", Tooltip = "Paste records.")]
  [PXUIField(DisplayName = "Paste", Enabled = false)]
  internal IEnumerable paste(PXAdapter adapter)
  {
    foreach (INItemCategoryBuffer itemCategoryBuffer in ((PXSelectBase) this.Buffer).Cache.Cached)
    {
      INItemCategory instance = (INItemCategory) ((PXSelectBase) this.Members).Cache.CreateInstance();
      instance.InventoryID = itemCategoryBuffer.InventoryID;
      ((PXSelectBase<INItemCategory>) this.Members).Insert(instance);
    }
    return adapter.Get();
  }

  [PXButton(Tooltip = "Add Items")]
  [PXUIField(DisplayName = "Add Items", Enabled = false)]
  internal IEnumerable addItemsbyClass(PXAdapter adapter)
  {
    if (((PXSelectBase<INCategoryMaint.ClassFilter>) this.ClassInfo).AskExt() == 1)
    {
      new PXView((PXGraph) this, false, ((PXSelectBase) this.Members).View.BqlSelect).SelectMulti(Array.Empty<object>());
      PXSelectReadonly<InventoryItem, Where<InventoryItem.itemStatus, NotEqual<InventoryItemStatus.unknown>, And<InventoryItem.isTemplate, Equal<False>>>> pxSelectReadonly = new PXSelectReadonly<InventoryItem, Where<InventoryItem.itemStatus, NotEqual<InventoryItemStatus.unknown>, And<InventoryItem.isTemplate, Equal<False>>>>((PXGraph) this);
      if (((PXSelectBase<INCategoryMaint.ClassFilter>) this.ClassInfo).Current.AddItemsTypes == "I")
        ((PXSelectBase<InventoryItem>) pxSelectReadonly).WhereAnd<Where<InventoryItem.itemClassID, Equal<Required<InventoryItem.itemClassID>>>>();
      foreach (PXResult<InventoryItem> pxResult in ((PXSelectBase<InventoryItem>) pxSelectReadonly).Select(new object[1]
      {
        (object) ((PXSelectBase<INCategoryMaint.ClassFilter>) this.ClassInfo).Current.ItemClassID
      }))
      {
        InventoryItem inventoryItem = PXResult<InventoryItem>.op_Implicit(pxResult);
        INItemCategory instance = (INItemCategory) ((PXSelectBase) this.Members).Cache.CreateInstance();
        instance.InventoryID = inventoryItem.InventoryID;
        instance.CategorySelected = new bool?(false);
        ((PXSelectBase<INItemCategory>) this.Members).Insert(instance);
      }
    }
    ((PXGraph) this).Actions.PressSave();
    return adapter.Get();
  }

  [PXButton]
  [PXUIField(DisplayName = "Inventory Details", Visible = false)]
  public virtual IEnumerable viewDetails(PXAdapter adapter)
  {
    if (((PXSelectBase<INItemCategory>) this.Members).Current != null)
    {
      InventoryItem inventoryItem = InventoryItem.PK.Find((PXGraph) this, ((PXSelectBase<INItemCategory>) this.Members).Current.InventoryID);
      if (inventoryItem != null)
      {
        InventoryItemMaintBase inventoryItemMaintBase = inventoryItem.StkItem.GetValueOrDefault() ? (InventoryItemMaintBase) PXGraph.CreateInstance<InventoryItemMaint>() : (InventoryItemMaintBase) PXGraph.CreateInstance<NonStockItemMaint>();
        ((PXSelectBase<InventoryItem>) inventoryItemMaintBase.Item).Current = PXResultset<InventoryItem>.op_Implicit(((PXSelectBase<InventoryItem>) inventoryItemMaintBase.Item).Search<InventoryItem.inventoryID>((object) inventoryItem.InventoryID, Array.Empty<object>()));
        PXRedirectHelper.TryRedirect((PXGraph) inventoryItemMaintBase, (PXRedirectHelper.WindowMode) 0);
      }
    }
    return adapter.Get();
  }

  protected virtual void _(Events.RowInserting<INCategory> e)
  {
    if (e.Row == null || !e.Row.CategoryID.HasValue || !e.Row.ParentID.HasValue || e.Row.Description == null || this.GetNamesakeInFolderFor(e.Row) == null)
      return;
    e.Cancel = true;
    ((Events.Event<PXRowInsertingEventArgs, Events.RowInserting<INCategory>>) e).Cache.RaiseExceptionHandling<INCategory.description>((object) e.Row, (object) e.Row.Description, (Exception) this.GetExistingNamesakeExceptionFor(e.Row));
  }

  protected virtual void _(Events.RowUpdating<INCategory> e)
  {
    if (e.NewRow == null || !e.NewRow.CategoryID.HasValue || !e.NewRow.ParentID.HasValue || e.NewRow.Description == null || this.GetNamesakeInFolderFor(e.NewRow) == null)
      return;
    e.Cancel = true;
    int? parentId1 = e.Row.ParentID;
    int? parentId2 = e.NewRow.ParentID;
    if (!(parentId1.GetValueOrDefault() == parentId2.GetValueOrDefault() & parentId1.HasValue == parentId2.HasValue))
      ((Events.Event<PXRowUpdatingEventArgs, Events.RowUpdating<INCategory>>) e).Cache.RaiseExceptionHandling<INCategory.parentID>((object) e.NewRow, (object) e.NewRow.ParentID, (Exception) this.GetExistingNamesakeExceptionFor(e.NewRow));
    else
      ((Events.Event<PXRowUpdatingEventArgs, Events.RowUpdating<INCategory>>) e).Cache.RaiseExceptionHandling<INCategory.description>((object) e.NewRow, (object) e.NewRow.Description, (Exception) this.GetExistingNamesakeExceptionFor(e.NewRow));
  }

  protected virtual void INCategory_RowDeleted(PXCache cache, PXRowDeletedEventArgs e)
  {
    if ((e.Row is INCategory row ? (!row.CategoryID.HasValue ? 1 : 0) : 1) != 0)
      return;
    this.deleteRecurring(row);
  }

  protected virtual void INItemCategory_RowInserted(PXCache cache, PXRowInsertedEventArgs e)
  {
    INItemCategory row = (INItemCategory) e.Row;
    if (row == null)
      return;
    PXDefaultAttribute.SetDefault<INItemCategory.categorySelected>(cache, (object) row, (object) false);
  }

  protected virtual void _(Events.RowInserted<INItemCategory> eventArgs)
  {
    INItemCategory row = eventArgs.Row;
    if ((row != null ? (!row.InventoryID.HasValue ? 1 : 0) : 1) != 0)
      return;
    GraphHelper.MarkUpdated(((PXSelectBase) this.RelatedInventoryItem).Cache, (object) ((PXSelectBase<InventoryItem>) this.RelatedInventoryItem).SelectSingle(Array.Empty<object>()), true);
  }

  protected virtual void _(Events.RowUpdated<INItemCategory> eventArgs)
  {
    int? inventoryId1 = (int?) eventArgs.OldRow?.InventoryID;
    if (eventArgs.Row == null)
      return;
    int? inventoryId2 = eventArgs.Row.InventoryID;
    int? nullable = inventoryId1;
    if (inventoryId2.GetValueOrDefault() == nullable.GetValueOrDefault() & inventoryId2.HasValue == nullable.HasValue)
      return;
    nullable = eventArgs.Row.InventoryID;
    if (nullable.HasValue)
      GraphHelper.MarkUpdated(((PXSelectBase) this.RelatedInventoryItem).Cache, (object) ((PXSelectBase<InventoryItem>) this.RelatedInventoryItem).SelectSingle(Array.Empty<object>()), true);
    if (!inventoryId1.HasValue)
      return;
    GraphHelper.MarkUpdated(((PXSelectBase) this.RelatedInventoryItem).Cache, (object) ((PXSelectBase<InventoryItem>) this.RelatedInventoryItem).SelectSingle(new object[1]
    {
      (object) inventoryId1
    }), true);
  }

  protected virtual void _(Events.RowDeleted<INItemCategory> eventArgs)
  {
    INItemCategory row = eventArgs.Row;
    if ((row != null ? (!row.InventoryID.HasValue ? 1 : 0) : 1) != 0)
      return;
    GraphHelper.MarkUpdated(((PXSelectBase) this.RelatedInventoryItem).Cache, (object) ((PXSelectBase<InventoryItem>) this.RelatedInventoryItem).SelectSingle(Array.Empty<object>()), true);
  }

  protected virtual void ClassFilter_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    INCategoryMaint.ClassFilter row = (INCategoryMaint.ClassFilter) e.Row;
    if (row == null)
      return;
    if (row.AddItemsTypes == "A")
      row.ItemClassID = new int?();
    PXUIFieldAttribute.SetEnabled<INCategoryMaint.ClassFilter.itemClassID>(cache, (object) row, row.AddItemsTypes != "A");
  }

  public virtual void Persist()
  {
    ((PXSelectBase) this.Buffer).Cache.Clear();
    ((PXGraph) this).Persist();
    foreach (INCategory inCategory1 in GraphHelper.Caches<INCategory>((PXGraph) this).Rows.Cached)
    {
      int? nullable = inCategory1.TempParentID;
      int num = 0;
      if (nullable.GetValueOrDefault() < num & nullable.HasValue)
      {
        foreach (INCategory inCategory2 in GraphHelper.Caches<INCategory>((PXGraph) this).Rows.Cached)
        {
          nullable = inCategory2.TempChildID;
          int? tempParentId = inCategory1.TempParentID;
          if (nullable.GetValueOrDefault() == tempParentId.GetValueOrDefault() & nullable.HasValue == tempParentId.HasValue)
          {
            inCategory1.ParentID = inCategory2.CategoryID;
            inCategory1.TempParentID = inCategory2.CategoryID;
            GraphHelper.MarkUpdated((PXCache) GraphHelper.Caches<INCategory>((PXGraph) this), (object) inCategory1, true);
          }
        }
      }
    }
    ((PXGraph) this).Persist();
    ((PXSelectBase) this.Members).View.RequestRefresh();
    INCategoryMaint.CategoryCache<INCategory>.Clear((PXGraph) this);
    INCategoryMaint.CategoryCache<INCategoryMaint.INFolderCategory>.Clear((PXGraph) this);
  }

  private void deleteRecurring(INCategory map)
  {
    if (map == null)
      return;
    foreach (PXResult<INCategory> pxResult in PXSelectBase<INCategory, PXSelect<INCategory, Where<INCategory.parentID, Equal<Required<INCategory.categoryID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) map.CategoryID
    }))
      this.deleteRecurring(PXResult<INCategory>.op_Implicit(pxResult));
    GraphHelper.Caches<INCategory>((PXGraph) this).Delete(map);
  }

  private INCategory GetNamesakeInFolderFor(INCategory category)
  {
    return PXResultset<INCategory>.op_Implicit(PXSelectBase<INCategory, PXViewOf<INCategory>.BasedOn<SelectFromBase<INCategory, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INCategory.parentID, Equal<BqlField<INCategory.parentID, IBqlInt>.FromCurrent>>>>, And<BqlOperand<INCategory.categoryID, IBqlInt>.IsNotEqual<BqlField<INCategory.categoryID, IBqlInt>.FromCurrent>>>>.And<BqlOperand<INCategory.description, IBqlString>.IsEqual<BqlField<INCategory.description, IBqlString>.FromCurrent>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) new INCategory[1]
    {
      category
    }, Array.Empty<object>()));
  }

  private PXException GetExistingNamesakeExceptionFor(INCategory category)
  {
    return (PXException) new PXSetPropertyException("The {0} category already contains the {1} subcategory.", (PXErrorLevel) 4, new object[2]
    {
      (object) this.GetParentDescriptionFor(category),
      (object) category.Description
    });
  }

  private string GetParentDescriptionFor(INCategory category)
  {
    int? parentId = category.ParentID;
    int num = 0;
    if (parentId.GetValueOrDefault() == num & parentId.HasValue)
      return PXSiteMap.RootNode.Title;
    return ((PXResult) ((IQueryable<PXResult<INCategory>>) PXSelectBase<INCategory, PXViewOf<INCategory>.BasedOn<SelectFromBase<INCategory, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<INCategory.categoryID, IBqlInt>.IsEqual<P.AsInt>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) category.ParentID
    })).Single<PXResult<INCategory>>()).GetItem<INCategory>().Description;
  }

  public static class AddItemsTypesList
  {
    public const string AddAllItems = "A";
    public const string AddItemsByClass = "I";

    public class ListAttribute : PXStringListAttribute
    {
      public ListAttribute()
        : base(new Tuple<string, string>[2]
        {
          PXStringListAttribute.Pair("A", "All Items"),
          PXStringListAttribute.Pair("I", "By Class")
        })
      {
      }
    }
  }

  public class INCategorySelectorAttribute : PXCustomSelectorAttribute
  {
    public INCategorySelectorAttribute()
      : base(typeof (INCategory.categoryID))
    {
      ((PXSelectorAttribute) this).DescriptionField = typeof (INCategory.description);
      ((PXSelectorAttribute) this).SelectorMode = (PXSelectorMode) 16 /*0x10*/;
    }

    public virtual IEnumerable GetRecords()
    {
      INCategoryMaint.INCategorySelectorAttribute selectorAttribute = this;
      yield return (object) new INCategory()
      {
        CategoryID = new int?(0),
        Description = PXSiteMap.RootNode.Title
      };
      foreach (PXResult<INCategory> pxResult in PXSelectBase<INCategory, PXSelect<INCategory>.Config>.Select(selectorAttribute._Graph, Array.Empty<object>()))
        yield return (object) PXResult<INCategory>.op_Implicit(pxResult);
    }
  }

  public class ClassFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXString(1, IsFixed = true)]
    [PXUIField(DisplayName = "Add Items")]
    [INCategoryMaint.AddItemsTypesList.List]
    [PXDefault("I")]
    public virtual string AddItemsTypes { get; set; }

    [PXInt]
    [PXUIField]
    [PXDimensionSelector("INITEMCLASS", typeof (INItemClass.itemClassID), typeof (INItemClass.itemClassCD), DescriptionField = typeof (INItemClass.descr), ValidComboRequired = true)]
    public virtual int? ItemClassID { get; set; }

    public abstract class addItemsTypes : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      INCategoryMaint.ClassFilter.addItemsTypes>
    {
    }

    public abstract class itemClassID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      INCategoryMaint.ClassFilter.itemClassID>
    {
    }
  }

  [PXHidden]
  public class SelectedNode : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXInt]
    [PXUIField(Visible = false)]
    public virtual int? FolderID { get; set; }

    [PXInt]
    [PXUIField(Visible = false)]
    public virtual int? CategoryID { get; set; }

    public abstract class folderID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      INCategoryMaint.SelectedNode.folderID>
    {
    }

    public abstract class categoryID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      INCategoryMaint.SelectedNode.categoryID>
    {
    }
  }

  public class INFolderCategory : INCategory
  {
  }

  public class INCategoryCurrent : INCategory
  {
  }

  protected class CategoryCache<TCategory> : IPrefetchable<PXGraph>, IPXCompanyDependent where TCategory : INCategory, new()
  {
    private ILookup<int?, TCategory> _lookup;

    public void Prefetch(PXGraph parameter)
    {
      this._lookup = GraphHelper.RowCast<TCategory>((IEnumerable) PXSelectBase<TCategory, PXSelectOrderBy<TCategory, OrderBy<Asc<INCategory.parentID, Asc<INCategory.sortOrder>>>>.Config>.Select(parameter, Array.Empty<object>())).ToLookup<TCategory, int?>((Func<TCategory, int?>) (r => r.ParentID));
    }

    private static INCategoryMaint.CategoryCache<TCategory> GetInstance(PXGraph graph)
    {
      return PXDatabase.GetLocalizableSlot<INCategoryMaint.CategoryCache<TCategory>, PXGraph>(typeof (INCategoryMaint.CategoryCache<TCategory>).FullName, graph, new Type[1]
      {
        typeof (INCategory)
      });
    }

    public static IEnumerable<TCategory> GetChildren(PXGraph graph, int? categoryID)
    {
      TCategory[] array1 = INCategoryMaint.CategoryCache<TCategory>.GetInstance(graph)._lookup[categoryID].ToArray<TCategory>();
      PXCache<TCategory> cache = GraphHelper.Caches<TCategory>(graph);
      TCategory[] array2 = cache.Rows.Cached.Where<TCategory>((Func<TCategory, bool>) (c =>
      {
        int? parentId = c.ParentID;
        int? nullable = categoryID;
        return parentId.GetValueOrDefault() == nullable.GetValueOrDefault() & parentId.HasValue == nullable.HasValue && EnumerableExtensions.IsIn<PXEntryStatus>(cache.GetStatus(c), (PXEntryStatus) 2, (PXEntryStatus) 1, (PXEntryStatus) 3, (PXEntryStatus) 6);
      })).ToArray<TCategory>();
      if (array2.Length != 0)
        array1 = ((IEnumerable<TCategory>) ((IEnumerable<TCategory>) array1).Except<TCategory>((IEnumerable<TCategory>) array2, PXCacheEx.GetComparer<TCategory>(cache)).ToArray<TCategory>()).Concat<TCategory>((IEnumerable<TCategory>) ((IEnumerable<TCategory>) array2).Where<TCategory>((Func<TCategory, bool>) (c => cache.GetStatus(c) != 3)).ToArray<TCategory>()).ToArray<TCategory>();
      for (int index = 0; index < array1.Length; ++index)
      {
        TCategory category = cache.Locate(array1[index]);
        if ((object) category != null)
          array1[index] = category;
      }
      return ((IEnumerable<TCategory>) array1).Where<TCategory>((Func<TCategory, bool>) (t =>
      {
        int? parentId = t.ParentID;
        int? nullable = categoryID;
        return parentId.GetValueOrDefault() == nullable.GetValueOrDefault() & parentId.HasValue == nullable.HasValue;
      }));
    }

    public static void Clear(PXGraph graph)
    {
      INCategoryMaint.CategoryCache<TCategory>.GetInstance(graph).Prefetch(graph);
    }
  }

  [PXLocalizable]
  public static class Msg
  {
    public const string ExistingNamesakeError = "The {0} category already contains the {1} subcategory.";
  }
}
