// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INPIClassMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Api.Models;
using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

#nullable disable
namespace PX.Objects.IN;

public class INPIClassMaint : PXGraph<INPIClassMaint, INPIClass>
{
  public PXSelect<INPIClass> Classes;
  public PXSelect<INPIClass, Where<INPIClass.pIClassID, Equal<Current<INPIClass.pIClassID>>>> CurrentClass;
  public PXSelectJoin<INPIClassItem, LeftJoin<InventoryItem, On<INPIClassItem.FK.InventoryItem>>, Where<INPIClassItem.pIClassID, Equal<Current<INPIClass.pIClassID>>>> Items;
  public PXSelectJoin<INPIClassItemClass, LeftJoin<INItemClass, On<INPIClassItemClass.FK.ItemClass>>, Where<INPIClassItemClass.pIClassID, Equal<Current<INPIClass.pIClassID>>>> ItemClasses;
  public PXSelectJoin<INPIClassLocation, LeftJoin<INLocation, On<INPIClassLocation.FK.Location>>, Where<INPIClassLocation.pIClassID, Equal<Current<INPIClass.pIClassID>>>> Locations;
  public PXFilter<INPILocationFilter> LocationFilter;
  public PXFilter<INPIInventoryFilter> InventoryFilter;
  public PXAction<INPIClass> AddLocation;
  public PXAction<INPIClass> AddItem;

  protected virtual void INPIClass_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    INPIClass row = (INPIClass) e.Row;
    if (row == null)
      return;
    PXUIFieldAttribute.SetEnabled<INPIClass.includeZeroItems>(sender, (object) row, this.AllowIncludeZeroItems(row));
    PXUIFieldAttribute.SetVisible<INPIClass.lastCountPeriod>(sender, (object) row, row.SelectedMethod == "I");
    PXUIFieldAttribute.SetVisible<INPIClass.randomItemsLimit>(sender, (object) row, row.SelectedMethod == "R");
    PXUIFieldAttribute.SetEnabled<INPIClass.aBCCodeID>(sender, (object) row, !row.ByFrequency.GetValueOrDefault());
    PXUIFieldAttribute.SetEnabled<INPIClass.movementClassID>(sender, (object) row, !row.ByFrequency.GetValueOrDefault());
    PXUIFieldAttribute.SetEnabled<INPIClass.cycleID>(sender, (object) row, !row.ByFrequency.GetValueOrDefault());
    ((PXSelectBase) this.Items).Cache.AllowInsert = ((PXSelectBase) this.Items).Cache.AllowUpdate = ((PXSelectBase) this.Items).Cache.AllowDelete = row.SelectedMethod == "L";
    ((PXAction) this.AddItem).SetEnabled(row.SelectedMethod == "L");
    PXCache cache1 = ((PXSelectBase) this.Locations).Cache;
    PXCache cache2 = ((PXSelectBase) this.Locations).Cache;
    PXCache cache3 = ((PXSelectBase) this.Locations).Cache;
    int? siteId = row.SiteID;
    int num1;
    bool flag1 = (num1 = siteId.HasValue ? 1 : 0) != 0;
    cache3.AllowDelete = num1 != 0;
    int num2;
    bool flag2 = (num2 = flag1 ? 1 : 0) != 0;
    cache2.AllowUpdate = num2 != 0;
    int num3 = flag2 ? 1 : 0;
    cache1.AllowInsert = num3 != 0;
    PXUIFieldAttribute.SetWarning<INPIClass.unlockSiteOnCountingFinish>(sender, (object) row, row.UnlockSiteOnCountingFinish.GetValueOrDefault() ? "Unfreezing stock when a PI process is not completed may cause discrepancy in cost or quantity of stock items and inability to release PI adjustments." : (string) null);
  }

  protected virtual void INPIClass_SiteID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    foreach (PXResult<INPIClassLocation> pxResult in ((PXSelectBase) this.Locations).View.SelectMultiBound(new object[1]
    {
      e.Row
    }, Array.Empty<object>()))
      ((PXSelectBase<INPIClassLocation>) this.Locations).Delete(PXResult<INPIClassLocation>.op_Implicit(pxResult));
  }

  protected virtual void INPIClass_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    INPIClass row = (INPIClass) e.Row;
    INPIClass oldRow = (INPIClass) e.OldRow;
    if (row != null && oldRow != null)
    {
      bool? byFrequency = row.ByFrequency;
      if (byFrequency.GetValueOrDefault())
      {
        byFrequency = oldRow.ByFrequency;
        if (!byFrequency.GetValueOrDefault())
        {
          row.ABCCodeID = (string) null;
          row.MovementClassID = (string) null;
          row.CycleID = (string) null;
        }
      }
      if (!PIMethod.IsByFrequencyAllowed(row.Method))
        row.ByFrequency = new bool?(false);
    }
    if (this.AllowIncludeZeroItems(row))
      return;
    row.IncludeZeroItems = new bool?(false);
  }

  [PXUIField]
  [PXButton(Tooltip = "Add New Line", ImageKey = "DataEntry")]
  protected virtual IEnumerable addLocation(PXAdapter e)
  {
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    if (((PXSelectBase<INPILocationFilter>) this.LocationFilter).AskExt(INPIClassMaint.\u003C\u003Ec.\u003C\u003E9__11_0 ?? (INPIClassMaint.\u003C\u003Ec.\u003C\u003E9__11_0 = new PXView.InitializePanel((object) INPIClassMaint.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CaddLocation\u003Eb__11_0))), true) == 1)
    {
      PXResultset<INLocation> pxResultset1;
      if (((PXSelectBase<INPILocationFilter>) this.LocationFilter).Current.EndLocationID.HasValue)
        pxResultset1 = PXSelectBase<INLocation, PXViewOf<INLocation>.BasedOn<SelectFromBase<INLocation, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INLocation.siteID, Equal<BqlField<INPIClass.siteID, IBqlInt>.FromCurrent>>>>>.And<BqlOperand<INLocation.locationCD, IBqlString>.IsBetween<P.AsString, P.AsString>>>>.ReadOnly.Config>.Select((PXGraph) this, new object[2]
        {
          (object) INLocation.PK.Find((PXGraph) this, ((PXSelectBase<INPILocationFilter>) this.LocationFilter).Current.StartLocationID)?.LocationCD,
          (object) INLocation.PK.Find((PXGraph) this, ((PXSelectBase<INPILocationFilter>) this.LocationFilter).Current.EndLocationID)?.LocationCD
        });
      else
        pxResultset1 = PXSelectBase<INLocation, PXViewOf<INLocation>.BasedOn<SelectFromBase<INLocation, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INLocation.siteID, Equal<BqlField<INPIClass.siteID, IBqlInt>.FromCurrent>>>>>.And<BqlOperand<INLocation.locationCD, IBqlString>.IsGreaterEqual<P.AsString>>>>.ReadOnly.Config>.Select((PXGraph) this, new object[1]
        {
          (object) INLocation.PK.Find((PXGraph) this, ((PXSelectBase<INPILocationFilter>) this.LocationFilter).Current.StartLocationID)?.LocationCD
        });
      PXResultset<INLocation> pxResultset2 = pxResultset1;
      HashSet<int?> existingLocations = ((IQueryable<PXResult<INPIClassLocation>>) PXSelectBase<INPIClassLocation, PXViewOf<INPIClassLocation>.BasedOn<SelectFromBase<INPIClassLocation, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<INPIClassLocation.pIClassID, IBqlString>.IsEqual<BqlField<INPIClass.pIClassID, IBqlString>.FromCurrent>>>.Config>.Select((PXGraph) this, Array.Empty<object>())).Select<PXResult<INPIClassLocation>, int?>((Expression<Func<PXResult<INPIClassLocation>, int?>>) (l => ((INPIClassLocation) l).LocationID)).Where<int?>((Expression<Func<int?, bool>>) (l => l.HasValue)).ToHashSet<int?>();
      foreach (INLocation inLocation in GraphHelper.RowCast<INLocation>((IEnumerable) pxResultset2).Where<INLocation>((Func<INLocation, bool>) (s => !existingLocations.Contains(s.LocationID))))
      {
        INPIClassLocation instance = (INPIClassLocation) ((PXSelectBase) this.Locations).Cache.CreateInstance();
        instance.LocationID = inLocation.LocationID;
        ((PXSelectBase<INPIClassLocation>) this.Locations).Insert(instance);
      }
    }
    return e.Get();
  }

  [PXUIField]
  [PXButton(Tooltip = "Add New Line", ImageKey = "DataEntry")]
  protected virtual IEnumerable addItem(PXAdapter e)
  {
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    if (((PXSelectBase<INPIInventoryFilter>) this.InventoryFilter).AskExt(INPIClassMaint.\u003C\u003Ec.\u003C\u003E9__13_0 ?? (INPIClassMaint.\u003C\u003Ec.\u003C\u003E9__13_0 = new PXView.InitializePanel((object) INPIClassMaint.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CaddItem\u003Eb__13_0))), true) == 1)
    {
      PXResultset<InventoryItem> pxResultset;
      if (((PXSelectBase<INPIInventoryFilter>) this.InventoryFilter).Current.EndInventoryID.HasValue)
        pxResultset = PXSelectBase<InventoryItem, PXViewOf<InventoryItem>.BasedOn<SelectFromBase<InventoryItem, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<InventoryItem.stkItem, Equal<True>>>>, And<BqlOperand<InventoryItem.itemStatus, IBqlString>.IsNotIn<InventoryItemStatus.inactive, InventoryItemStatus.markedForDeletion>>>>.And<BqlOperand<InventoryItem.inventoryCD, IBqlString>.IsBetween<P.AsString, P.AsString>>>>.ReadOnly.Config>.Select((PXGraph) this, new object[2]
        {
          (object) InventoryItem.PK.Find((PXGraph) this, ((PXSelectBase<INPIInventoryFilter>) this.InventoryFilter).Current.StartInventoryID)?.InventoryCD,
          (object) InventoryItem.PK.Find((PXGraph) this, ((PXSelectBase<INPIInventoryFilter>) this.InventoryFilter).Current.EndInventoryID)?.InventoryCD
        });
      else
        pxResultset = PXSelectBase<InventoryItem, PXViewOf<InventoryItem>.BasedOn<SelectFromBase<InventoryItem, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<InventoryItem.stkItem, Equal<True>>>>, And<BqlOperand<InventoryItem.itemStatus, IBqlString>.IsNotIn<InventoryItemStatus.inactive, InventoryItemStatus.markedForDeletion>>>>.And<BqlOperand<InventoryItem.inventoryCD, IBqlString>.IsGreaterEqual<P.AsString>>>>.ReadOnly.Config>.Select((PXGraph) this, new object[1]
        {
          (object) InventoryItem.PK.Find((PXGraph) this, ((PXSelectBase<INPIInventoryFilter>) this.InventoryFilter).Current.StartInventoryID)?.InventoryCD
        });
      foreach (PXResult<InventoryItem> pxResult in pxResultset)
      {
        InventoryItem inventoryItem = PXResult<InventoryItem>.op_Implicit(pxResult);
        INPIClassItem instance = (INPIClassItem) ((PXSelectBase) this.Items).Cache.CreateInstance();
        if (((PXSelectBase<INPIClassItem>) this.Items).Locate(instance) == null)
        {
          instance.InventoryID = inventoryItem.InventoryID;
          ((PXSelectBase<INPIClassItem>) this.Items).Insert(instance);
        }
      }
    }
    return e.Get();
  }

  protected virtual bool AllowIncludeZeroItems(INPIClass row)
  {
    if (row?.Method == "F")
      return false;
    return !(row?.Method == "I") || !(row?.SelectedMethod == "N");
  }

  public virtual void CopyPasteGetScript(
    bool isImportSimple,
    List<Command> script,
    List<Container> containers)
  {
    int index = script.FindIndex((Predicate<Command>) (_ => _.FieldName == "IncludeZeroItems"));
    if (index == -1)
      return;
    Command command = script[index];
    Container container = containers[index];
    script.Remove(command);
    containers.Remove(container);
    script.Add(command);
    containers.Add(container);
  }
}
