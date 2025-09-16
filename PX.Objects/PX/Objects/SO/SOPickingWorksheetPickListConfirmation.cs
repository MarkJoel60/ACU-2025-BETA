// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOPickingWorksheetPickListConfirmation
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Data.WorkflowAPI;
using PX.Objects.Common;
using PX.Objects.Common.Extensions;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.IN;
using PX.SM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;

#nullable enable
namespace PX.Objects.SO;

public class SOPickingWorksheetPickListConfirmation : PXGraphExtension<
#nullable disable
SOPickingWorksheetReview>
{
  private Func<int?, bool> isEnterableDate;
  private Func<int?, bool> isWhenUsed;
  private Func<int?, bool> isEnterableOnIssue;
  public FbqlSelect<SelectFromBase<SOPickingWorksheetPickListConfirmation.SOPickingWorksheetLineDelta, TypeArrayOf<IFbqlJoin>.Empty>, SOPickingWorksheetPickListConfirmation.SOPickingWorksheetLineDelta>.View WSLineDelta;
  public FbqlSelect<SelectFromBase<SOPickingWorksheetPickListConfirmation.SOPickingWorksheetLineSplitDelta, TypeArrayOf<IFbqlJoin>.Empty>, SOPickingWorksheetPickListConfirmation.SOPickingWorksheetLineSplitDelta>.View WSSplitDelta;
  public FbqlSelect<SelectFromBase<INCartSplit, TypeArrayOf<IFbqlJoin>.Empty>, INCartSplit>.View CartSplits;
  public FbqlSelect<SelectFromBase<SOPickListEntryToCartSplitLink, TypeArrayOf<IFbqlJoin>.Empty>, SOPickListEntryToCartSplitLink>.View CartLinks;
  public PXAction<SOPickingWorksheet> FulfillShipments;

  public virtual void Initialize()
  {
    Func<int?, bool> secondFunc = InventoryLotSerialClassPredicate((Func<INLotSerClass, bool>) (lsc => lsc.LotSerIssueMethod == "U"));
    this.isEnterableDate = InventoryLotSerialClassPredicate((Func<INLotSerClass, bool>) (lsc => lsc.LotSerTrackExpiration.GetValueOrDefault()));
    this.isWhenUsed = InventoryLotSerialClassPredicate((Func<INLotSerClass, bool>) (lsc => lsc.LotSerAssign == "U"));
    this.isEnterableOnIssue = Func.Disjoin<int?>(this.isWhenUsed, secondFunc);

    Func<int?, bool> InventoryLotSerialClassPredicate(Func<INLotSerClass, bool> predicate)
    {
      return Func.Memorize<int?, bool>((Func<int?, bool>) (inventoryID => PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this.Base, inventoryID).With<PX.Objects.IN.InventoryItem, INLotSerClass>((Func<PX.Objects.IN.InventoryItem, INLotSerClass>) (ii => (INLotSerClass) PrimaryKeyOf<INLotSerClass>.By<INLotSerClass.lotSerClassID>.ForeignKeyOf<PX.Objects.IN.InventoryItem>.By<PX.Objects.IN.InventoryItem.lotSerClassID>.FindParent((PXGraph) this.Base, (PX.Objects.IN.InventoryItem.lotSerClassID) ii, (PKFindOptions) 0))).With<INLotSerClass, bool>(predicate)));
    }
  }

  [PXButton]
  [PXUIField(DisplayName = "Pick All Shipments")]
  protected virtual void fulfillShipments()
  {
    ((PXGraph) this.Base).LongOperationManager.StartAsyncOperation((Func<CancellationToken, System.Threading.Tasks.Task>) (ct => this.FulfillShipmentsAndConfirmWorksheet(((PXSelectBase<SOPickingWorksheet>) this.Base.worksheet).Current, ct)));
  }

  protected virtual void _(PX.Data.Events.RowSelected<SOPickingWorksheet> e)
  {
    ((PXAction) this.FulfillShipments).SetEnabled(((PXSelectBase<SOPickingWorksheet>) this.Base.worksheet).Current != null && EnumerableExtensions.IsIn<string>(((PXSelectBase<SOPickingWorksheet>) this.Base.worksheet).Current.Status, "I", "P") && ((IEnumerable<SOPickingWorksheetShipment>) ((PXSelectBase<SOPickingWorksheetShipment>) this.Base.shipmentLinks).SelectMain(Array.Empty<object>())).Any<SOPickingWorksheetShipment>((Func<SOPickingWorksheetShipment, bool>) (sh =>
    {
      bool? picked = sh.Picked;
      bool flag1 = false;
      if (!(picked.GetValueOrDefault() == flag1 & picked.HasValue))
        return false;
      bool? unlinked = sh.Unlinked;
      bool flag2 = false;
      return unlinked.GetValueOrDefault() == flag2 & unlinked.HasValue;
    })) && ((IEnumerable<SOPicker>) ((PXSelectBase<SOPicker>) this.Base.pickers).SelectMain(Array.Empty<object>())).Any<SOPicker>((Func<SOPicker, bool>) (p => p.Confirmed.GetValueOrDefault())));
  }

  public virtual void ConfirmPickList(SOPicker pickList, int? sortingLocationID)
  {
    if (pickList == null)
      throw new PXArgumentException(nameof (pickList), "The argument cannot be null.");
    ((PXGraph) this.Base).Clear();
    ((PXSelectBase<SOPickingWorksheet>) this.Base.worksheet).Current = SOPickingWorksheet.PK.Find((PXGraph) this.Base, pickList.WorksheetNbr, (PKFindOptions) 1).With<SOPickingWorksheet, SOPickingWorksheet>(new Func<SOPickingWorksheet, SOPickingWorksheet>(this.ValidateMutability));
    ((PXSelectBase<SOPicker>) this.Base.pickers).Current = SOPicker.PK.Find((PXGraph) this.Base, pickList.WorksheetNbr, pickList.PickerNbr, (PKFindOptions) 1).With<SOPicker, SOPicker>(new Func<SOPicker, SOPicker>(this.ValidateMutability));
    ((PXSelectBase<SOPickingJob>) this.Base.pickerJob).Current = PXResultset<SOPickingJob>.op_Implicit(((PXSelectBase<SOPickingJob>) this.Base.pickerJob).Select(Array.Empty<object>()));
    (SOPickingWorksheetLine, SOPickingWorksheetLineSplit)[] array1 = ((IEnumerable<PXResult<SOPickingWorksheetLine>>) PXSelectBase<SOPickingWorksheetLine, PXViewOf<SOPickingWorksheetLine>.BasedOn<SelectFromBase<SOPickingWorksheetLine, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<SOPickingWorksheetLineSplit>.On<SOPickingWorksheetLineSplit.FK.WorksheetLine>>>.Where<KeysRelation<Field<SOPickingWorksheetLine.worksheetNbr>.IsRelatedTo<SOPickingWorksheet.worksheetNbr>.AsSimpleKey.WithTablesOf<SOPickingWorksheet, SOPickingWorksheetLine>, SOPickingWorksheet, SOPickingWorksheetLine>.SameAsCurrent>>.ReadOnly.Config>.Select((PXGraph) this.Base, Array.Empty<object>())).AsEnumerable<PXResult<SOPickingWorksheetLine>>().Select<PXResult<SOPickingWorksheetLine>, (SOPickingWorksheetLine, SOPickingWorksheetLineSplit)>((Func<PXResult<SOPickingWorksheetLine>, (SOPickingWorksheetLine, SOPickingWorksheetLineSplit)>) (r => (((PXResult) r).GetItem<SOPickingWorksheetLine>(), ((PXResult) r).GetItem<SOPickingWorksheetLineSplit>()))).ToArray<(SOPickingWorksheetLine, SOPickingWorksheetLineSplit)>();
    ((int?, int?, string, int?, string), Decimal, DateTime?)[] array2 = ((IEnumerable<SOPickerListEntry>) ((PXSelectBase<SOPickerListEntry>) new FbqlSelect<SelectFromBase<SOPickerListEntry, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<INLocation>.On<SOPickerListEntry.FK.Location>>, FbqlJoins.Inner<PX.Objects.IN.InventoryItem>.On<SOPickerListEntry.FK.InventoryItem>>>.Where<KeysRelation<CompositeKey<Field<SOPickerListEntry.worksheetNbr>.IsRelatedTo<SOPicker.worksheetNbr>, Field<SOPickerListEntry.pickerNbr>.IsRelatedTo<SOPicker.pickerNbr>>.WithTablesOf<SOPicker, SOPickerListEntry>, SOPicker, SOPickerListEntry>.SameAsCurrent>.Order<By<BqlField<INLocation.pathPriority, IBqlInt>.Asc, BqlField<INLocation.locationCD, IBqlString>.Asc, BqlField<PX.Objects.IN.InventoryItem.inventoryCD, IBqlString>.Asc, BqlField<SOPickerListEntry.lotSerialNbr, IBqlString>.Asc>>, SOPickerListEntry>.View((PXGraph) this.Base)).SelectMain(Array.Empty<object>())).GroupBy<SOPickerListEntry, (int?, int?, string, int?, string)>((Func<SOPickerListEntry, (int?, int?, string, int?, string)>) (e => (e.InventoryID, e.SubItemID, e.OrderLineUOM, e.LocationID, e.LotSerialNbr))).Select<IGrouping<(int?, int?, string, int?, string), SOPickerListEntry>, ((int?, int?, string, int?, string), Decimal, DateTime?)>((Func<IGrouping<(int?, int?, string, int?, string), SOPickerListEntry>, ((int?, int?, string, int?, string), Decimal, DateTime?)>) (g => (g.Key, g.Sum<SOPickerListEntry>((Func<SOPickerListEntry, Decimal>) (e => e.PickedQty.GetValueOrDefault())), g.Min<SOPickerListEntry, DateTime?>((Func<SOPickerListEntry, DateTime?>) (e => e.ExpireDate))))).Where<((int?, int?, string, int?, string), Decimal, DateTime?)>((Func<((int?, int?, string, int?, string), Decimal, DateTime?), bool>) (e => e.PickedQty > 0M)).ToArray<((int?, int?, string, int?, string), Decimal, DateTime?)>();
    HashSet<int?> pickListEnterableItems = ((IEnumerable<((int?, int?, string, int?, string), Decimal, DateTime?)>) array2).Select<((int?, int?, string, int?, string), Decimal, DateTime?), int?>((Func<((int?, int?, string, int?, string), Decimal, DateTime?), int?>) (e => e.Key.InventoryID)).Distinct<int?>().Where<int?>((Func<int?, bool>) (itemID => this.isEnterableOnIssue(itemID))).ToHashSet<int?>();
    (IEnumerable<((int?, int?, string, int?, string), Decimal, DateTime?)> entries1, IEnumerable<((int?, int?, string, int?, string), Decimal, DateTime?)> entries2) = EnumerableExtensions.DisuniteBy<((int?, int?, string, int?, string), Decimal, DateTime?)>((IEnumerable<((int?, int?, string, int?, string), Decimal, DateTime?)>) array2, (Func<((int?, int?, string, int?, string), Decimal, DateTime?), bool>) (e => pickListEnterableItems.Contains(new int?(e.Key.InventoryID.Value))));
    Func<(SOPickingWorksheetLine, SOPickingWorksheetLineSplit), bool> func = (Func<(SOPickingWorksheetLine, SOPickingWorksheetLineSplit), bool>) (e => pickListEnterableItems.Contains(new int?(e.Split.InventoryID.Value)));
    (IEnumerable<(SOPickingWorksheetLine, SOPickingWorksheetLineSplit)>, IEnumerable<(SOPickingWorksheetLine, SOPickingWorksheetLineSplit)>) tuple = EnumerableExtensions.DisuniteBy<(SOPickingWorksheetLine, SOPickingWorksheetLineSplit)>((IEnumerable<(SOPickingWorksheetLine, SOPickingWorksheetLineSplit)>) array1, func);
    IEnumerable<(SOPickingWorksheetLine, SOPickingWorksheetLineSplit)> wsSplits = tuple.Item1;
    this.UpdateFixedWorksheetSplits(tuple.Item2, entries2, sortingLocationID);
    this.UpdateEnterableWorksheetSplits(wsSplits, entries1, sortingLocationID);
    ((PXSelectBase<SOPicker>) this.Base.pickers).Current.Confirmed = new bool?(true);
    if (sortingLocationID.HasValue)
      ((PXSelectBase<SOPicker>) this.Base.pickers).Current.SortingLocationID = sortingLocationID;
    ((PXSelectBase<SOPicker>) this.Base.pickers).UpdateCurrent();
    if (((PXSelectBase<SOPickingJob>) this.Base.pickerJob).Current != null)
    {
      ((PXSelectBase<SOPickingJob>) this.Base.pickerJob).Current.Status = "PED";
      ((PXSelectBase<SOPickingJob>) this.Base.pickerJob).UpdateCurrent();
    }
    if (sortingLocationID.HasValue)
      this.RemoveItemsFromPickerCart(((PXSelectBase<SOPicker>) this.Base.pickers).Current);
    if (((PXSelectBase<SOPickingWorksheet>) this.Base.worksheet).Current.SingleShipmentNbr != null)
      ((PXGraph) this.Base).GetExtension<SOPickingWorksheetPickListConfirmation.ShipmentWorkLog>().CloseFor(((PXSelectBase<SOPickingWorksheet>) this.Base.worksheet).Current.SingleShipmentNbr);
    else
      ((PXGraph) this.Base).GetExtension<SOPickingWorksheetPickListConfirmation.ShipmentWorkLog>().CloseFor(((PXSelectBase<SOPicker>) this.Base.pickers).Current.WorksheetNbr, ((PXSelectBase<SOPicker>) this.Base.pickers).Current.PickerNbr.Value);
    ((PXAction) this.Base.Save).Press();
  }

  protected virtual void RemoveItemsFromPickerCart(SOPicker picker)
  {
    foreach (SOPickListEntryToCartSplitLink selectChild in KeysRelation<CompositeKey<Field<SOPickListEntryToCartSplitLink.worksheetNbr>.IsRelatedTo<SOPicker.worksheetNbr>, Field<SOPickListEntryToCartSplitLink.pickerNbr>.IsRelatedTo<SOPicker.pickerNbr>>.WithTablesOf<SOPicker, SOPickListEntryToCartSplitLink>, SOPicker, SOPickListEntryToCartSplitLink>.SelectChildren((PXGraph) this.Base, picker))
    {
      Decimal? qty = selectChild.Qty;
      Decimal num1 = qty.Value;
      ((PXSelectBase<SOPickListEntryToCartSplitLink>) this.CartLinks).Delete(selectChild);
      INCartSplit inCartSplit = INCartSplit.PK.Find((PXGraph) this.Base, selectChild.SiteID, selectChild.CartID, selectChild.CartSplitLineNbr);
      if (inCartSplit != null)
      {
        qty = inCartSplit.Qty;
        Decimal num2 = qty.Value - num1;
        if (num2 <= 0M)
        {
          ((PXSelectBase<INCartSplit>) this.CartSplits).Delete(inCartSplit);
        }
        else
        {
          ((PXSelectBase) this.CartSplits).Cache.SetValueExt<INCartSplit.qty>((object) inCartSplit, (object) num2);
          ((PXSelectBase<INCartSplit>) this.CartSplits).Update(inCartSplit);
        }
      }
    }
  }

  protected virtual void UpdateFixedWorksheetSplits(
    IEnumerable<(SOPickingWorksheetLine Line, SOPickingWorksheetLineSplit Split)> wsSplits,
    IEnumerable<((int? InventoryID, int? SubItemID, string OrderLineUOM, int? LocationID, string LotSerialNbr) Key, Decimal PickedQty, DateTime? ExpireDate)> entries,
    int? sortingLocationID)
  {
    foreach (IGrouping<int?, (SOPickingWorksheetLine, SOPickingWorksheetLineSplit, ((int?, int?, string, int?, string), Decimal, DateTime?))> source1 in wsSplits.Join<(SOPickingWorksheetLine, SOPickingWorksheetLineSplit), ((int?, int?, string, int?, string), Decimal, DateTime?), (int?, int?, string, int?, string), (SOPickingWorksheetLine, SOPickingWorksheetLineSplit, ((int?, int?, string, int?, string), Decimal, DateTime?))>(entries, (Func<(SOPickingWorksheetLine, SOPickingWorksheetLineSplit), (int?, int?, string, int?, string)>) (s => (s.Split.InventoryID, s.Split.SubItemID, s.Line.UOM, s.Split.LocationID, s.Split.LotSerialNbr)), (Func<((int?, int?, string, int?, string), Decimal, DateTime?), (int?, int?, string, int?, string)>) (e => (e.Key.InventoryID, e.Key.SubItemID, e.Key.OrderLineUOM, e.Key.LocationID, e.Key.LotSerialNbr)), (Func<(SOPickingWorksheetLine, SOPickingWorksheetLineSplit), ((int?, int?, string, int?, string), Decimal, DateTime?), (SOPickingWorksheetLine, SOPickingWorksheetLineSplit, ((int?, int?, string, int?, string), Decimal, DateTime?))>) ((s, e) => (s.Line, s.Split, e))).GroupBy<(SOPickingWorksheetLine, SOPickingWorksheetLineSplit, ((int?, int?, string, int?, string), Decimal, DateTime?)), int?>((Func<(SOPickingWorksheetLine, SOPickingWorksheetLineSplit, ((int?, int?, string, int?, string), Decimal, DateTime?)), int?>) (t => t.Line.LineNbr)).ToArray<IGrouping<int?, (SOPickingWorksheetLine, SOPickingWorksheetLineSplit, ((int?, int?, string, int?, string), Decimal, DateTime?))>>())
    {
      SOPickingWorksheetLine source2 = source1.First<(SOPickingWorksheetLine, SOPickingWorksheetLineSplit, ((int?, int?, string, int?, string), Decimal, DateTime?))>().Item1;
      Decimal num = source1.Sum<(SOPickingWorksheetLine, SOPickingWorksheetLineSplit, ((int?, int?, string, int?, string), Decimal, DateTime?))>((Func<(SOPickingWorksheetLine, SOPickingWorksheetLineSplit, ((int?, int?, string, int?, string), Decimal, DateTime?)), Decimal>) (s => s.Entry.PickedQty));
      SOPickingWorksheetPickListConfirmation.SOPickingWorksheetLineDelta worksheetLineDelta = PropertyTransfer.Transfer<SOPickingWorksheetLine, SOPickingWorksheetPickListConfirmation.SOPickingWorksheetLineDelta>(source2, new SOPickingWorksheetPickListConfirmation.SOPickingWorksheetLineDelta());
      worksheetLineDelta.BasePickedQty = new Decimal?(num);
      worksheetLineDelta.PickedQty = new Decimal?(INUnitAttribute.ConvertFromBase(((PXSelectBase) this.WSLineDelta).Cache, source2.InventoryID, source2.UOM, num, INPrecision.NOROUND));
      ((PXSelectBase<SOPickingWorksheetPickListConfirmation.SOPickingWorksheetLineDelta>) this.WSLineDelta).Insert(worksheetLineDelta);
      foreach ((SOPickingWorksheetLine _, SOPickingWorksheetLineSplit source3, ((int?, int?, string, int?, string), Decimal, DateTime?) tuple) in (IEnumerable<(SOPickingWorksheetLine, SOPickingWorksheetLineSplit, ((int?, int?, string, int?, string), Decimal, DateTime?))>) source1)
      {
        SOPickingWorksheetPickListConfirmation.SOPickingWorksheetLineSplitDelta worksheetLineSplitDelta = PropertyTransfer.Transfer<SOPickingWorksheetLineSplit, SOPickingWorksheetPickListConfirmation.SOPickingWorksheetLineSplitDelta>(source3, new SOPickingWorksheetPickListConfirmation.SOPickingWorksheetLineSplitDelta());
        worksheetLineSplitDelta.BasePickedQty = new Decimal?(tuple.Item2);
        worksheetLineSplitDelta.PickedQty = new Decimal?(tuple.Item2);
        worksheetLineSplitDelta.BaseQty = new Decimal?(0M);
        worksheetLineSplitDelta.Qty = new Decimal?(0M);
        worksheetLineSplitDelta.SortingLocationID = sortingLocationID;
        ((PXSelectBase<SOPickingWorksheetPickListConfirmation.SOPickingWorksheetLineSplitDelta>) this.WSSplitDelta).Insert(worksheetLineSplitDelta);
      }
    }
  }

  protected virtual void UpdateEnterableWorksheetSplits(
    IEnumerable<(SOPickingWorksheetLine Line, SOPickingWorksheetLineSplit Split)> wsSplits,
    IEnumerable<((int? InventoryID, int? SubItemID, string OrderLineUOM, int? LocationID, string LotSerialNbr) Key, Decimal PickedQty, DateTime? ExpireDate)> entries,
    int? sortingLocationID)
  {
    this.PopulateWorksheetSplitsFromPickListEntries(wsSplits.Where<(SOPickingWorksheetLine, SOPickingWorksheetLineSplit)>((Func<(SOPickingWorksheetLine, SOPickingWorksheetLineSplit), bool>) (r => r.Split.IsUnassigned.GetValueOrDefault() || this.isEnterableOnIssue(r.Split.InventoryID))), entries, sortingLocationID);
  }

  protected virtual void PopulateWorksheetSplitsFromPickListEntries(
    IEnumerable<(SOPickingWorksheetLine Line, SOPickingWorksheetLineSplit Split)> wsSplits,
    IEnumerable<((int? InventoryID, int? SubItemID, string OrderLineUOM, int? LocationID, string LotSerialNbr) Key, Decimal PickedQty, DateTime? ExpireDate)> entries,
    int? sortingLocationID)
  {
    Queue<(SOPickingWorksheetLine, SOPickingWorksheetLineSplit)> queue1 = EnumerableExtensions.ToQueue<(SOPickingWorksheetLine, SOPickingWorksheetLineSplit)>((IEnumerable<(SOPickingWorksheetLine, SOPickingWorksheetLineSplit)>) wsSplits.OrderBy<(SOPickingWorksheetLine, SOPickingWorksheetLineSplit), int?>((Func<(SOPickingWorksheetLine, SOPickingWorksheetLineSplit), int?>) (r => r.Split.InventoryID)).ThenBy<(SOPickingWorksheetLine, SOPickingWorksheetLineSplit), int?>((Func<(SOPickingWorksheetLine, SOPickingWorksheetLineSplit), int?>) (r => r.Split.SubItemID)).ThenBy<(SOPickingWorksheetLine, SOPickingWorksheetLineSplit), string>((Func<(SOPickingWorksheetLine, SOPickingWorksheetLineSplit), string>) (r => r.Line.UOM)).ThenBy<(SOPickingWorksheetLine, SOPickingWorksheetLineSplit), int?>((Func<(SOPickingWorksheetLine, SOPickingWorksheetLineSplit), int?>) (r => r.Split.LocationID)).ThenByDescending<(SOPickingWorksheetLine, SOPickingWorksheetLineSplit), bool>((Func<(SOPickingWorksheetLine, SOPickingWorksheetLineSplit), bool>) (r => r.Split.HasGeneratedLotSerialNbr.GetValueOrDefault())).ThenByDescending<(SOPickingWorksheetLine, SOPickingWorksheetLineSplit), Decimal?>((Func<(SOPickingWorksheetLine, SOPickingWorksheetLineSplit), Decimal?>) (r => r.Split.Qty)));
    Queue<((int?, int?, string, int?, string), Decimal, DateTime?)> queue2 = EnumerableExtensions.ToQueue<((int?, int?, string, int?, string), Decimal, DateTime?)>((IEnumerable<((int?, int?, string, int?, string), Decimal, DateTime?)>) entries.OrderBy<((int?, int?, string, int?, string), Decimal, DateTime?), int?>((Func<((int?, int?, string, int?, string), Decimal, DateTime?), int?>) (r => r.Key.InventoryID)).ThenBy<((int?, int?, string, int?, string), Decimal, DateTime?), int?>((Func<((int?, int?, string, int?, string), Decimal, DateTime?), int?>) (r => r.Key.SubItemID)).ThenBy<((int?, int?, string, int?, string), Decimal, DateTime?), string>((Func<((int?, int?, string, int?, string), Decimal, DateTime?), string>) (r => r.Key.OrderLineUOM)).ThenBy<((int?, int?, string, int?, string), Decimal, DateTime?), int?>((Func<((int?, int?, string, int?, string), Decimal, DateTime?), int?>) (r => r.Key.LocationID)).ThenByDescending<((int?, int?, string, int?, string), Decimal, DateTime?), Decimal>((Func<((int?, int?, string, int?, string), Decimal, DateTime?), Decimal>) (r => r.PickedQty)));
    while (queue1.Count > 0 && queue2.Count > 0)
    {
      (SOPickingWorksheetLine source1, SOPickingWorksheetLineSplit source2) = queue1.Peek();
      ((int?, int?, string, int?, string), Decimal, DateTime?) tuple1 = queue2.Peek();
      (int?, int?, string, int?, string) entry = tuple1.Item1;
      Decimal val2 = tuple1.Item2;
      DateTime? nullable1 = tuple1.Item3;
      (int?, int?, string, int?) valueTuple = (entry.Item1, entry.Item2, entry.Item3, entry.Item4);
      (int?, int?, string, int?) other = (source2.InventoryID, source2.SubItemID, source1.UOM, source2.LocationID);
      int? nullable2;
      if (!valueTuple.Equals(other))
      {
        nullable2 = valueTuple.Item1;
        int? nullable3 = other.Item1;
        bool bypassSubItemID = (!(nullable2.GetValueOrDefault() == nullable3.GetValueOrDefault() & nullable2.HasValue == nullable3.HasValue) ? 1 : 0) != 0;
        int num1;
        if (!bypassSubItemID)
        {
          nullable3 = valueTuple.Item2;
          nullable2 = other.Item2;
          num1 = !(nullable3.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable3.HasValue == nullable2.HasValue) ? 1 : 0;
        }
        else
          num1 = 1;
        bool bypassUOM = num1 != 0;
        bool bypassLocationID = bypassUOM || valueTuple.Item3 != other.Item3;
        if (valueTuple.CompareTo(other) > 0)
        {
          int num2 = queue1.TakeWhile<(SOPickingWorksheetLine, SOPickingWorksheetLineSplit)>((Func<(SOPickingWorksheetLine, SOPickingWorksheetLineSplit), bool>) (r =>
          {
            int? inventoryId1 = r.Split.InventoryID;
            int? inventoryId2 = source2.InventoryID;
            if (inventoryId1.GetValueOrDefault() == inventoryId2.GetValueOrDefault() & inventoryId1.HasValue == inventoryId2.HasValue)
            {
              if (!bypassSubItemID)
              {
                int? subItemId1 = r.Split.SubItemID;
                int? subItemId2 = source2.SubItemID;
                if (!(subItemId1.GetValueOrDefault() == subItemId2.GetValueOrDefault() & subItemId1.HasValue == subItemId2.HasValue))
                  goto label_7;
              }
              if (bypassUOM || r.Line.UOM == source1.UOM)
              {
                if (bypassLocationID)
                  return true;
                int? locationId1 = r.Split.LocationID;
                int? locationId2 = source2.LocationID;
                return locationId1.GetValueOrDefault() == locationId2.GetValueOrDefault() & locationId1.HasValue == locationId2.HasValue;
              }
            }
label_7:
            return false;
          })).Count<(SOPickingWorksheetLine, SOPickingWorksheetLineSplit)>();
          EnumerableExtensions.Consume<(SOPickingWorksheetLine, SOPickingWorksheetLineSplit)>(EnumerableExtensions.Dequeue<(SOPickingWorksheetLine, SOPickingWorksheetLineSplit)>(queue1, num2));
        }
        else
        {
          int num3 = queue2.TakeWhile<((int?, int?, string, int?, string), Decimal, DateTime?)>((Func<((int?, int?, string, int?, string), Decimal, DateTime?), bool>) (e =>
          {
            int? inventoryId = e.Key.InventoryID;
            int? nullable4 = entry.Item1;
            if (inventoryId.GetValueOrDefault() == nullable4.GetValueOrDefault() & inventoryId.HasValue == nullable4.HasValue)
            {
              if (!bypassSubItemID)
              {
                int? subItemId = e.Key.SubItemID;
                int? nullable5 = entry.Item2;
                if (!(subItemId.GetValueOrDefault() == nullable5.GetValueOrDefault() & subItemId.HasValue == nullable5.HasValue))
                  goto label_7;
              }
              if (bypassUOM || e.Key.OrderLineUOM == entry.Item3)
              {
                if (bypassLocationID)
                  return true;
                int? locationId = e.Key.LocationID;
                int? nullable6 = entry.Item4;
                return locationId.GetValueOrDefault() == nullable6.GetValueOrDefault() & locationId.HasValue == nullable6.HasValue;
              }
            }
label_7:
            return false;
          })).Count<((int?, int?, string, int?, string), Decimal, DateTime?)>();
          EnumerableExtensions.Consume<((int?, int?, string, int?, string), Decimal, DateTime?)>(EnumerableExtensions.Dequeue<((int?, int?, string, int?, string), Decimal, DateTime?)>(queue2, num3));
        }
      }
      else
      {
        Decimal? nullable7 = source2.Qty;
        Decimal val1 = nullable7.Value;
        do
        {
          Decimal num4 = Math.Min(val1, val2);
          val2 -= num4;
          val1 -= num4;
          SOPickingWorksheetPickListConfirmation.SOPickingWorksheetLineDelta worksheetLineDelta1 = PropertyTransfer.Transfer<SOPickingWorksheetLine, SOPickingWorksheetPickListConfirmation.SOPickingWorksheetLineDelta>(source1, new SOPickingWorksheetPickListConfirmation.SOPickingWorksheetLineDelta());
          worksheetLineDelta1.BasePickedQty = new Decimal?(num4);
          worksheetLineDelta1.PickedQty = new Decimal?(INUnitAttribute.ConvertFromBase(((PXSelectBase) this.WSLineDelta).Cache, source1.InventoryID, source1.UOM, num4, INPrecision.NOROUND));
          SOPickingWorksheetPickListConfirmation.SOPickingWorksheetLineDelta worksheetLineDelta2 = ((PXSelectBase<SOPickingWorksheetPickListConfirmation.SOPickingWorksheetLineDelta>) this.WSLineDelta).Locate(worksheetLineDelta1);
          Decimal? nullable8;
          if (worksheetLineDelta2 == null)
          {
            ((PXSelectBase<SOPickingWorksheetPickListConfirmation.SOPickingWorksheetLineDelta>) this.WSLineDelta).Insert(worksheetLineDelta1);
          }
          else
          {
            SOPickingWorksheetPickListConfirmation.SOPickingWorksheetLineDelta worksheetLineDelta3 = worksheetLineDelta2;
            nullable7 = worksheetLineDelta3.BasePickedQty;
            nullable8 = worksheetLineDelta1.BasePickedQty;
            worksheetLineDelta3.BasePickedQty = nullable7.HasValue & nullable8.HasValue ? new Decimal?(nullable7.GetValueOrDefault() + nullable8.GetValueOrDefault()) : new Decimal?();
            SOPickingWorksheetPickListConfirmation.SOPickingWorksheetLineDelta worksheetLineDelta4 = worksheetLineDelta2;
            nullable8 = worksheetLineDelta4.PickedQty;
            nullable7 = worksheetLineDelta1.PickedQty;
            worksheetLineDelta4.PickedQty = nullable8.HasValue & nullable7.HasValue ? new Decimal?(nullable8.GetValueOrDefault() + nullable7.GetValueOrDefault()) : new Decimal?();
          }
          SOPickingWorksheetPickListConfirmation.SOPickingWorksheetLineSplitDelta worksheetLineSplitDelta1 = PropertyTransfer.Transfer<SOPickingWorksheetLineSplit, SOPickingWorksheetPickListConfirmation.SOPickingWorksheetLineSplitDelta>(source2, new SOPickingWorksheetPickListConfirmation.SOPickingWorksheetLineSplitDelta());
          worksheetLineSplitDelta1.BaseQty = new Decimal?(-num4);
          worksheetLineSplitDelta1.Qty = new Decimal?(-num4);
          SOPickingWorksheetPickListConfirmation.SOPickingWorksheetLineSplitDelta worksheetLineSplitDelta2 = ((PXSelectBase<SOPickingWorksheetPickListConfirmation.SOPickingWorksheetLineSplitDelta>) this.WSSplitDelta).Locate(worksheetLineSplitDelta1);
          if (worksheetLineSplitDelta2 == null)
          {
            nullable2 = source2.SortingLocationID;
            if (!nullable2.HasValue)
              worksheetLineSplitDelta1.SortingLocationID = sortingLocationID;
            ((PXSelectBase<SOPickingWorksheetPickListConfirmation.SOPickingWorksheetLineSplitDelta>) this.WSSplitDelta).Insert(worksheetLineSplitDelta1);
          }
          else
          {
            SOPickingWorksheetPickListConfirmation.SOPickingWorksheetLineSplitDelta worksheetLineSplitDelta3 = worksheetLineSplitDelta2;
            nullable7 = worksheetLineSplitDelta3.BaseQty;
            Decimal num5 = num4;
            Decimal? nullable9;
            if (!nullable7.HasValue)
            {
              nullable8 = new Decimal?();
              nullable9 = nullable8;
            }
            else
              nullable9 = new Decimal?(nullable7.GetValueOrDefault() - num5);
            worksheetLineSplitDelta3.BaseQty = nullable9;
            SOPickingWorksheetPickListConfirmation.SOPickingWorksheetLineSplitDelta worksheetLineSplitDelta4 = worksheetLineSplitDelta2;
            nullable7 = worksheetLineSplitDelta4.Qty;
            Decimal num6 = num4;
            Decimal? nullable10;
            if (!nullable7.HasValue)
            {
              nullable8 = new Decimal?();
              nullable10 = nullable8;
            }
            else
              nullable10 = new Decimal?(nullable7.GetValueOrDefault() - num6);
            worksheetLineSplitDelta4.Qty = nullable10;
          }
          ((PXSelectBase<SOPickingWorksheetLine>) this.Base.worksheetLines).Current = source1;
          SOPickingWorksheetLineSplit worksheetLineSplit1 = PropertyTransfer.Transfer<SOPickingWorksheetLineSplit, SOPickingWorksheetLineSplit>(source2, new SOPickingWorksheetLineSplit());
          SOPickingWorksheetLineSplit worksheetLineSplit2 = worksheetLineSplit1;
          nullable2 = new int?();
          int? nullable11 = nullable2;
          worksheetLineSplit2.SplitNbr = nullable11;
          worksheetLineSplit1.LotSerialNbr = entry.Item5;
          worksheetLineSplit1.ExpireDate = nullable1;
          worksheetLineSplit1.PickedQty = new Decimal?(num4);
          worksheetLineSplit1.Qty = new Decimal?(num4);
          worksheetLineSplit1.BasePickedQty = new Decimal?(num4);
          worksheetLineSplit1.BaseQty = new Decimal?(num4);
          worksheetLineSplit1.SortingLocationID = sortingLocationID;
          worksheetLineSplit1.IsUnassigned = new bool?(false);
          worksheetLineSplit1.HasGeneratedLotSerialNbr = new bool?(false);
          ((PXSelectBase<SOPickingWorksheetLineSplit>) this.Base.worksheetLineSplits).Insert(worksheetLineSplit1);
          if (val2 == 0M)
          {
            queue2.Dequeue();
            if (queue2.Count != 0)
            {
              ((int?, int?, string, int?, string), Decimal, DateTime?) tuple2 = queue2.Peek();
              entry = tuple2.Item1;
              val2 = tuple2.Item2;
              nullable1 = tuple2.Item3;
              valueTuple = (entry.Item1, entry.Item2, entry.Item3, entry.Item4);
            }
            else
              break;
          }
          if (val1 == 0M)
          {
            queue1.Dequeue();
            if (queue1.Count != 0)
            {
              (source1, source2) = queue1.Peek();
              nullable7 = source2.Qty;
              val1 = nullable7.Value;
              other = (source2.InventoryID, source2.SubItemID, source1.UOM, source2.LocationID);
            }
            else
              break;
          }
        }
        while (valueTuple.Equals(other));
      }
    }
  }

  public virtual IEnumerable<string> TryFulfillShipments(SOPickingWorksheet worksheet)
  {
    if (worksheet == null)
      throw new PXArgumentException(nameof (worksheet), "The argument cannot be null.");
    if (worksheet.WorksheetType == "SS")
      return this.FulfillShipmentsSingle(worksheet);
    if (worksheet.WorksheetType == "WV")
      return this.FulfillShipmentsWave(worksheet);
    return worksheet.WorksheetType == "BT" ? this.FulfillShipmentsBatch(worksheet) : throw new NotSupportedException();
  }

  protected virtual IEnumerable<string> FulfillShipmentsBatch(SOPickingWorksheet worksheet)
  {
    ((PXGraph) this.Base).Clear();
    ((PXSelectBase<SOPickingWorksheet>) this.Base.worksheet).Current = SOPickingWorksheet.PK.Find((PXGraph) this.Base, worksheet.WorksheetNbr, (PKFindOptions) 1).With<SOPickingWorksheet, SOPickingWorksheet>(new Func<SOPickingWorksheet, SOPickingWorksheet>(this.ValidateMutability));
    (SOPickingWorksheetLine, SOPickingWorksheetLineSplit)[] array1 = ((IEnumerable<PXResult<SOPickingWorksheetLine>>) PXSelectBase<SOPickingWorksheetLine, PXViewOf<SOPickingWorksheetLine>.BasedOn<SelectFromBase<SOPickingWorksheetLine, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<SOPickingWorksheetLineSplit>.On<SOPickingWorksheetLineSplit.FK.WorksheetLine>>>.Where<KeysRelation<Field<SOPickingWorksheetLine.worksheetNbr>.IsRelatedTo<SOPickingWorksheet.worksheetNbr>.AsSimpleKey.WithTablesOf<SOPickingWorksheet, SOPickingWorksheetLine>, SOPickingWorksheet, SOPickingWorksheetLine>.SameAsCurrent.And<BqlOperand<SOPickingWorksheetLineSplit.isUnassigned, IBqlBool>.IsEqual<False>>>>.Config>.Select((PXGraph) this.Base, Array.Empty<object>())).AsEnumerable<PXResult<SOPickingWorksheetLine>>().Select<PXResult<SOPickingWorksheetLine>, (SOPickingWorksheetLine, SOPickingWorksheetLineSplit)>((Func<PXResult<SOPickingWorksheetLine>, (SOPickingWorksheetLine, SOPickingWorksheetLineSplit)>) (r => (((PXResult) r).GetItem<SOPickingWorksheetLine>(), ((PXResult) r).GetItem<SOPickingWorksheetLineSplit>()))).ToArray<(SOPickingWorksheetLine, SOPickingWorksheetLineSplit)>();
    ReadOnlyDictionary<(int?, string), DateTime> expirationDates = EnumerableExtensions.AsReadOnly<(int?, string), DateTime>((IDictionary<(int?, string), DateTime>) EnumerableExtensions.Distinct<(SOPickingWorksheetLine, SOPickingWorksheetLineSplit), (int?, string)>(((IEnumerable<(SOPickingWorksheetLine, SOPickingWorksheetLineSplit)>) array1).Where<(SOPickingWorksheetLine, SOPickingWorksheetLineSplit)>((Func<(SOPickingWorksheetLine, SOPickingWorksheetLineSplit), bool>) (s => this.isEnterableDate(s.Split.InventoryID))), (Func<(SOPickingWorksheetLine, SOPickingWorksheetLineSplit), (int?, string)>) (s => (s.Split.InventoryID, s.Split.LotSerialNbr))).ToDictionary<(SOPickingWorksheetLine, SOPickingWorksheetLineSplit), (int?, string), DateTime>((Func<(SOPickingWorksheetLine, SOPickingWorksheetLineSplit), (int?, string)>) (s => (s.Split.InventoryID, s.Split.LotSerialNbr)), (Func<(SOPickingWorksheetLine, SOPickingWorksheetLineSplit), DateTime>) (s => s.Split.ExpireDate.Value)));
    Dictionary<(int?, int?, string, int?, int?, string), Decimal> dictionary1 = ((IEnumerable<(SOPickingWorksheetLine, SOPickingWorksheetLineSplit)>) array1).GroupBy<(SOPickingWorksheetLine, SOPickingWorksheetLineSplit), (int?, int?, string, int?, int?, string)>((Func<(SOPickingWorksheetLine, SOPickingWorksheetLineSplit), (int?, int?, string, int?, int?, string)>) (s => (s.Split.InventoryID, s.Split.SubItemID, s.Line.UOM, s.Split.LocationID, s.Split.SortingLocationID, s.Split.LotSerialNbr))).ToDictionary<IGrouping<(int?, int?, string, int?, int?, string), (SOPickingWorksheetLine, SOPickingWorksheetLineSplit)>, (int?, int?, string, int?, int?, string), Decimal>((Func<IGrouping<(int?, int?, string, int?, int?, string), (SOPickingWorksheetLine, SOPickingWorksheetLineSplit)>, (int?, int?, string, int?, int?, string)>) (g => g.Key), (Func<IGrouping<(int?, int?, string, int?, int?, string), (SOPickingWorksheetLine, SOPickingWorksheetLineSplit)>, Decimal>) (g => g.Sum<(SOPickingWorksheetLine, SOPickingWorksheetLineSplit)>((Func<(SOPickingWorksheetLine, SOPickingWorksheetLineSplit), Decimal>) (s => s.Split.PickedQty.GetValueOrDefault()))));
    (SOShipment, SOShipLine, SOShipLineSplit, INTranSplit)[] array2 = ((IEnumerable<(SOShipment, SOShipLine, SOShipLineSplit, INTranSplit)>) ((IEnumerable<PXResult<SOShipment>>) PXSelectBase<SOShipment, PXViewOf<SOShipment>.BasedOn<SelectFromBase<SOShipment, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<SOShipLine>.On<SOShipLine.FK.Shipment>>, FbqlJoins.Inner<SOShipLineSplit>.On<SOShipLineSplit.FK.ShipmentLine>>, FbqlJoins.Left<INTranSplit>.On<KeysRelation<CompositeKey<Field<INTranSplit.shipmentNbr>.IsRelatedTo<SOShipLineSplit.shipmentNbr>, Field<INTranSplit.shipmentLineNbr>.IsRelatedTo<SOShipLineSplit.lineNbr>, Field<INTranSplit.shipmentLineSplitNbr>.IsRelatedTo<SOShipLineSplit.splitLineNbr>>.WithTablesOf<SOShipLineSplit, INTranSplit>, SOShipLineSplit, INTranSplit>.And<BqlOperand<INTranSplit.locationID, IBqlInt>.IsNotEqual<INTranSplit.toLocationID>>>>>.Where<KeysRelation<Field<SOShipment.currentWorksheetNbr>.IsRelatedTo<SOPickingWorksheet.worksheetNbr>.AsSimpleKey.WithTablesOf<SOPickingWorksheet, SOShipment>, SOPickingWorksheet, SOShipment>.SameAsCurrent>>.Config>.Select((PXGraph) this.Base, Array.Empty<object>())).AsEnumerable<PXResult<SOShipment>>().Select<PXResult<SOShipment>, (SOShipment, SOShipLine, SOShipLineSplit, INTranSplit)>((Func<PXResult<SOShipment>, (SOShipment, SOShipLine, SOShipLineSplit, INTranSplit)>) (r => (((PXResult) r).GetItem<SOShipment>(), ((PXResult) r).GetItem<SOShipLine>(), ((PXResult) r).GetItem<SOShipLineSplit>(), ((PXResult) r).GetItem<INTranSplit>()))).ToArray<(SOShipment, SOShipLine, SOShipLineSplit, INTranSplit)>()).Concat<(SOShipment, SOShipLine, SOShipLineSplit, INTranSplit)>((IEnumerable<(SOShipment, SOShipLine, SOShipLineSplit, INTranSplit)>) ((IEnumerable<PXResult<SOShipment>>) PXSelectBase<SOShipment, PXViewOf<SOShipment>.BasedOn<SelectFromBase<SOShipment, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<SOShipLine>.On<SOShipLine.FK.Shipment>>, FbqlJoins.Inner<PX.Objects.SO.Unassigned.SOShipLineSplit>.On<PX.Objects.SO.Unassigned.SOShipLineSplit.FK.ShipmentLine>>, FbqlJoins.Left<INTranSplit>.On<BqlOperand<True, IBqlBool>.IsEqual<False>>>>.Where<KeysRelation<Field<SOShipment.currentWorksheetNbr>.IsRelatedTo<SOPickingWorksheet.worksheetNbr>.AsSimpleKey.WithTablesOf<SOPickingWorksheet, SOShipment>, SOPickingWorksheet, SOShipment>.SameAsCurrent>>.Config>.Select((PXGraph) this.Base, Array.Empty<object>())).AsEnumerable<PXResult<SOShipment>>().Select<PXResult<SOShipment>, (SOShipment, SOShipLine, SOShipLineSplit, INTranSplit)>((Func<PXResult<SOShipment>, (SOShipment, SOShipLine, SOShipLineSplit, INTranSplit)>) (r => (((PXResult) r).GetItem<SOShipment>(), ((PXResult) r).GetItem<SOShipLine>(), PropertyTransfer.Transfer<PX.Objects.SO.Unassigned.SOShipLineSplit, SOShipLineSplit>(((PXResult) r).GetItem<PX.Objects.SO.Unassigned.SOShipLineSplit>(), new SOShipLineSplit()), ((PXResult) r).GetItem<INTranSplit>()))).ToArray<(SOShipment, SOShipLine, SOShipLineSplit, INTranSplit)>()).OrderBy<(SOShipment, SOShipLine, SOShipLineSplit, INTranSplit), DateTime?>((Func<(SOShipment, SOShipLine, SOShipLineSplit, INTranSplit), DateTime?>) (r => r.Shipment.ShipDate)).ThenBy<(SOShipment, SOShipLine, SOShipLineSplit, INTranSplit), Decimal?>((Func<(SOShipment, SOShipLine, SOShipLineSplit, INTranSplit), Decimal?>) (r => r.Shipment.LineTotal)).ThenBy<(SOShipment, SOShipLine, SOShipLineSplit, INTranSplit), string>((Func<(SOShipment, SOShipLine, SOShipLineSplit, INTranSplit), string>) (r => r.Shipment.ShipmentNbr)).ToArray<(SOShipment, SOShipLine, SOShipLineSplit, INTranSplit)>();
    ((SOShipment, SOShipLine, SOShipLineSplit, INTranSplit)[] source1, (SOShipment, SOShipLine, SOShipLineSplit, INTranSplit)[] source2) = EnumerableExtensions.DisuniteBy<(SOShipment, SOShipLine, SOShipLineSplit, INTranSplit)>((IEnumerable<(SOShipment, SOShipLine, SOShipLineSplit, INTranSplit)>) array2, (Func<(SOShipment, SOShipLine, SOShipLineSplit, INTranSplit), bool>) (s => s.Shipment.Picked.GetValueOrDefault())).With<(IEnumerable<(SOShipment, SOShipLine, SOShipLineSplit, INTranSplit)>, IEnumerable<(SOShipment, SOShipLine, SOShipLineSplit, INTranSplit)>), ((SOShipment, SOShipLine, SOShipLineSplit, INTranSplit)[], (SOShipment, SOShipLine, SOShipLineSplit, INTranSplit)[])>((Func<(IEnumerable<(SOShipment, SOShipLine, SOShipLineSplit, INTranSplit)>, IEnumerable<(SOShipment, SOShipLine, SOShipLineSplit, INTranSplit)>), ((SOShipment, SOShipLine, SOShipLineSplit, INTranSplit)[], (SOShipment, SOShipLine, SOShipLineSplit, INTranSplit)[])>) (pair => (pair.Affirmatives.ToArray<(SOShipment, SOShipLine, SOShipLineSplit, INTranSplit)>(), pair.Negatives.ToArray<(SOShipment, SOShipLine, SOShipLineSplit, INTranSplit)>())));
    if (source1.Length + source2.Length != array2.Length || EnumerableExtensions.IntersectBy<(SOShipment, SOShipLine, SOShipLineSplit, INTranSplit), string>((IEnumerable<(SOShipment, SOShipLine, SOShipLineSplit, INTranSplit)>) source1, (IEnumerable<(SOShipment, SOShipLine, SOShipLineSplit, INTranSplit)>) source2, (Func<(SOShipment, SOShipLine, SOShipLineSplit, INTranSplit), string>) (s => s.Shipment.ShipmentNbr), (IEqualityComparer<string>) null).Count<(SOShipment, SOShipLine, SOShipLineSplit, INTranSplit)>() != 0)
      throw new PXInvalidOperationException();
    Dictionary<(string, int?), INTran> transferLinesForUnassignedSplits = EnumerableExtensions.Distinct<SOShipLine, (string, int?)>(((IEnumerable<(SOShipment, SOShipLine, SOShipLineSplit, INTranSplit)>) source1).Where<(SOShipment, SOShipLine, SOShipLineSplit, INTranSplit)>((Func<(SOShipment, SOShipLine, SOShipLineSplit, INTranSplit), bool>) (s => !s.TransferSplit.LocationID.HasValue)).Select<(SOShipment, SOShipLine, SOShipLineSplit, INTranSplit), SOShipLine>((Func<(SOShipment, SOShipLine, SOShipLineSplit, INTranSplit), SOShipLine>) (s => s.Line)), (Func<SOShipLine, (string, int?)>) (l => (l.ShipmentNbr, l.LineNbr))).ToDictionary<SOShipLine, (string, int?), INTran>((Func<SOShipLine, (string, int?)>) (l => (l.ShipmentNbr, l.LineNbr)), (Func<SOShipLine, INTran>) (l => PXResultset<INTran>.op_Implicit(PXSelectBase<INTran, PXViewOf<INTran>.BasedOn<SelectFromBase<INTran, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<INTranSplit>.On<INTranSplit.FK.Tran>>>.Where<KeysRelation<CompositeKey<Field<INTranSplit.shipmentNbr>.IsRelatedTo<SOShipLine.shipmentNbr>, Field<INTranSplit.shipmentLineNbr>.IsRelatedTo<SOShipLine.lineNbr>>.WithTablesOf<SOShipLine, INTranSplit>, SOShipLine, INTranSplit>.SameAsCurrent.And<BqlOperand<INTranSplit.locationID, IBqlInt>.IsNotEqual<INTranSplit.toLocationID>>>>.Config>.SelectSingleBound((PXGraph) this.Base, (object[]) new SOShipLine[1]
    {
      l
    }, Array.Empty<object>()))));
    Dictionary<(int?, int?, string, int?, int?, string), Decimal> dictionary2 = ((IEnumerable<(SOShipment, SOShipLine, SOShipLineSplit, INTranSplit)>) source1).GroupBy<(SOShipment, SOShipLine, SOShipLineSplit, INTranSplit), (int?, int?, string, int?, int?, string)>((Func<(SOShipment, SOShipLine, SOShipLineSplit, INTranSplit), (int?, int?, string, int?, int?, string)>) (s => (s.Split.InventoryID, s.Split.SubItemID, s.Line.UOM, s.TransferSplit.LocationID ?? transferLinesForUnassignedSplits[(s.Line.ShipmentNbr, s.Line.LineNbr)].LocationID, s.Split.LocationID, s.Split.LotSerialNbr))).ToDictionary<IGrouping<(int?, int?, string, int?, int?, string), (SOShipment, SOShipLine, SOShipLineSplit, INTranSplit)>, (int?, int?, string, int?, int?, string), Decimal>((Func<IGrouping<(int?, int?, string, int?, int?, string), (SOShipment, SOShipLine, SOShipLineSplit, INTranSplit)>, (int?, int?, string, int?, int?, string)>) (g => g.Key), (Func<IGrouping<(int?, int?, string, int?, int?, string), (SOShipment, SOShipLine, SOShipLineSplit, INTranSplit)>, Decimal>) (g => g.Sum<(SOShipment, SOShipLine, SOShipLineSplit, INTranSplit)>((Func<(SOShipment, SOShipLine, SOShipLineSplit, INTranSplit), Decimal>) (s => s.Split.PickedQty.GetValueOrDefault()))));
    if (dictionary2.Any<KeyValuePair<(int?, int?, string, int?, int?, string), Decimal>>())
    {
      foreach (KeyValuePair<(int?, int?, string, int?, int?, string), Decimal> keyValuePair in dictionary2)
        dictionary1[keyValuePair.Key] -= keyValuePair.Value;
      EnumerableExtensions.RemoveRange<(int?, int?, string, int?, int?, string), Decimal>((IDictionary<(int?, int?, string, int?, int?, string), Decimal>) dictionary1, (IEnumerable<(int?, int?, string, int?, int?, string)>) dictionary1.Where<KeyValuePair<(int?, int?, string, int?, int?, string), Decimal>>((Func<KeyValuePair<(int?, int?, string, int?, int?, string), Decimal>, bool>) (kvp => kvp.Value == 0M)).Select<KeyValuePair<(int?, int?, string, int?, int?, string), Decimal>, (int?, int?, string, int?, int?, string)>((Func<KeyValuePair<(int?, int?, string, int?, int?, string), Decimal>, (int?, int?, string, int?, int?, string)>) (kvp => kvp.Key)).ToArray<(int?, int?, string, int?, int?, string)>());
    }
    Dictionary<(int?, int?, string, int?, string), List<(int?, Decimal)>> availability = dictionary1.GroupBy<KeyValuePair<(int?, int?, string, int?, int?, string), Decimal>, (int?, int?, string, int?, string)>((Func<KeyValuePair<(int?, int?, string, int?, int?, string), Decimal>, (int?, int?, string, int?, string)>) (kvp => (kvp.Key.InventoryID, kvp.Key.SubItemID, kvp.Key.UOM, kvp.Key.LocationID, kvp.Key.LotSerialNbr))).Where<IGrouping<(int?, int?, string, int?, string), KeyValuePair<(int?, int?, string, int?, int?, string), Decimal>>>((Func<IGrouping<(int?, int?, string, int?, string), KeyValuePair<(int?, int?, string, int?, int?, string), Decimal>>, bool>) (g => g.Any<KeyValuePair<(int?, int?, string, int?, int?, string), Decimal>>((Func<KeyValuePair<(int?, int?, string, int?, int?, string), Decimal>, bool>) (e => e.Value > 0M)))).ToDictionary<IGrouping<(int?, int?, string, int?, string), KeyValuePair<(int?, int?, string, int?, int?, string), Decimal>>, (int?, int?, string, int?, string), List<(int?, Decimal)>>((Func<IGrouping<(int?, int?, string, int?, string), KeyValuePair<(int?, int?, string, int?, int?, string), Decimal>>, (int?, int?, string, int?, string)>) (g => g.Key), (Func<IGrouping<(int?, int?, string, int?, string), KeyValuePair<(int?, int?, string, int?, int?, string), Decimal>>, List<(int?, Decimal)>>) (g => g.Select<KeyValuePair<(int?, int?, string, int?, int?, string), Decimal>, (int?, Decimal)>((Func<KeyValuePair<(int?, int?, string, int?, int?, string), Decimal>, (int?, Decimal)>) (e => (e.Key.SortLocationID, e.Value))).OrderByDescending<(int?, Decimal), Decimal>((Func<(int?, Decimal), Decimal>) (e => e.PickedQty)).ToList<(int?, Decimal)>()));
    HashSet<int?> itemVariety = availability.Select<KeyValuePair<(int?, int?, string, int?, string), List<(int?, Decimal)>>, int?>((Func<KeyValuePair<(int?, int?, string, int?, string), List<(int?, Decimal)>>, int?>) (kvp => kvp.Key.InventoryID)).ToHashSet<int?>();
    HashSet<int?> locationVariety = availability.Select<KeyValuePair<(int?, int?, string, int?, string), List<(int?, Decimal)>>, int?>((Func<KeyValuePair<(int?, int?, string, int?, string), List<(int?, Decimal)>>, int?>) (kvp => kvp.Key.LocationID)).ToHashSet<int?>();
    HashSet<string> uomVariety = availability.Select<KeyValuePair<(int?, int?, string, int?, string), List<(int?, Decimal)>>, string>((Func<KeyValuePair<(int?, int?, string, int?, string), List<(int?, Decimal)>>, string>) (kvp => kvp.Key.UOM)).ToHashSet<string>();
    (string, int?, Dictionary<(int?, int?, string, int?, string), Decimal>, (SOShipLine, SOShipLineSplit)[])[] array3 = ((IEnumerable<(SOShipment, SOShipLine, SOShipLineSplit, INTranSplit)>) source2).GroupBy<(SOShipment, SOShipLine, SOShipLineSplit, INTranSplit), string>((Func<(SOShipment, SOShipLine, SOShipLineSplit, INTranSplit), string>) (s => s.Shipment.ShipmentNbr)).Where<IGrouping<string, (SOShipment, SOShipLine, SOShipLineSplit, INTranSplit)>>((Func<IGrouping<string, (SOShipment, SOShipLine, SOShipLineSplit, INTranSplit)>, bool>) (g => g.All<(SOShipment, SOShipLine, SOShipLineSplit, INTranSplit)>((Func<(SOShipment, SOShipLine, SOShipLineSplit, INTranSplit), bool>) (s => itemVariety.Contains(s.Split.InventoryID) && locationVariety.Contains(s.Split.LocationID) && uomVariety.Contains(s.Line.UOM))))).Select<IGrouping<string, (SOShipment, SOShipLine, SOShipLineSplit, INTranSplit)>, (string, int?, Dictionary<(int?, int?, string, int?, string), Decimal>, (SOShipLine, SOShipLineSplit)[])>((Func<IGrouping<string, (SOShipment, SOShipLine, SOShipLineSplit, INTranSplit)>, (string, int?, Dictionary<(int?, int?, string, int?, string), Decimal>, (SOShipLine, SOShipLineSplit)[])>) (splitsByShipment => (splitsByShipment.Key, splitsByShipment.First<(SOShipment, SOShipLine, SOShipLineSplit, INTranSplit)>().Item1.SiteID, splitsByShipment.GroupBy<(SOShipment, SOShipLine, SOShipLineSplit, INTranSplit), (int?, int?, string, int?, string)>((Func<(SOShipment, SOShipLine, SOShipLineSplit, INTranSplit), (int?, int?, string, int?, string)>) (s => (s.Split.InventoryID, s.Split.SubItemID, s.Line.UOM, s.Split.LocationID, s.Split.LotSerialNbr))).ToDictionary<IGrouping<(int?, int?, string, int?, string), (SOShipment, SOShipLine, SOShipLineSplit, INTranSplit)>, (int?, int?, string, int?, string), Decimal>((Func<IGrouping<(int?, int?, string, int?, string), (SOShipment, SOShipLine, SOShipLineSplit, INTranSplit)>, (int?, int?, string, int?, string)>) (g => g.Key), (Func<IGrouping<(int?, int?, string, int?, string), (SOShipment, SOShipLine, SOShipLineSplit, INTranSplit)>, Decimal>) (g => g.Sum<(SOShipment, SOShipLine, SOShipLineSplit, INTranSplit)>((Func<(SOShipment, SOShipLine, SOShipLineSplit, INTranSplit), Decimal>) (s => s.Split.Qty.GetValueOrDefault())))), splitsByShipment.Select<(SOShipment, SOShipLine, SOShipLineSplit, INTranSplit), (SOShipLine, SOShipLineSplit)>((Func<(SOShipment, SOShipLine, SOShipLineSplit, INTranSplit), (SOShipLine, SOShipLineSplit)>) (d => (d.Line, d.Split))).ToArray<(SOShipLine, SOShipLineSplit)>()))).ToArray<(string, int?, Dictionary<(int?, int?, string, int?, string), Decimal>, (SOShipLine, SOShipLineSplit)[])>();
    Lazy<INTransferEntry> lazy1 = Lazy.By<INTransferEntry>((Func<INTransferEntry>) (() => PXGraph.CreateInstance<INTransferEntry>().Apply<INTransferEntry>((Action<INTransferEntry>) (graph => graph.SuppressLocationDefaultingForWMS = true))));
    Lazy<SOShipmentEntry> lazy2 = Lazy.By<SOShipmentEntry>((Func<SOShipmentEntry>) (() => PXGraph.CreateInstance<SOShipmentEntry>()));
    List<string> stringList = new List<string>();
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      foreach ((string, int?, Dictionary<(int?, int?, string, int?, string), Decimal>, (SOShipLine, SOShipLineSplit)[]) tuple in array3)
      {
        if (tuple.Item3.All<KeyValuePair<(int?, int?, string, int?, string), Decimal>>((Func<KeyValuePair<(int?, int?, string, int?, string), Decimal>, bool>) (demand =>
        {
          if (this.isEnterableOnIssue(demand.Key.InventoryID))
            return availability.Where<KeyValuePair<(int?, int?, string, int?, string), List<(int?, Decimal)>>>((Func<KeyValuePair<(int?, int?, string, int?, string), List<(int?, Decimal)>>, bool>) (kvp =>
            {
              int? inventoryId3 = kvp.Key.InventoryID;
              int? inventoryId4 = demand.Key.InventoryID;
              if (inventoryId3.GetValueOrDefault() == inventoryId4.GetValueOrDefault() & inventoryId3.HasValue == inventoryId4.HasValue)
              {
                int? subItemId3 = kvp.Key.SubItemID;
                int? subItemId4 = demand.Key.SubItemID;
                if (subItemId3.GetValueOrDefault() == subItemId4.GetValueOrDefault() & subItemId3.HasValue == subItemId4.HasValue && kvp.Key.UOM == demand.Key.UOM)
                {
                  int? locationId3 = kvp.Key.LocationID;
                  int? locationId4 = demand.Key.LocationID;
                  return locationId3.GetValueOrDefault() == locationId4.GetValueOrDefault() & locationId3.HasValue == locationId4.HasValue;
                }
              }
              return false;
            })).Sum<KeyValuePair<(int?, int?, string, int?, string), List<(int?, Decimal)>>>((Func<KeyValuePair<(int?, int?, string, int?, string), List<(int?, Decimal)>>, Decimal>) (kvp => kvp.Value.Sum<(int?, Decimal)>((Func<(int?, Decimal), Decimal>) (s => s.PickedQty)))) >= demand.Value;
          return availability.ContainsKey(demand.Key) && availability[demand.Key].Sum<(int?, Decimal)>((Func<(int?, Decimal), Decimal>) (s => s.PickedQty)) >= demand.Value;
        })))
        {
          this.HoldShipment(lazy2.Value, tuple.Item1);
          IEnumerable<(SOShipLineSplit, INTranSplit)> transferSplits = this.CreateTransferSplits((IReadOnlyDictionary<(int?, int?, string, int?, string), List<(int?, Decimal)>>) availability, (IEnumerable<(SOShipLine, SOShipLineSplit)>) tuple.Item4);
          IEnumerable<(SOShipLine Line, SOShipLineSplit Split)> outer = this.MakeAllSplitsAssigned(lazy2.Value, tuple.Item1, (IReadOnlyDictionary<(int?, int?, string, int?, string), List<(int?, Decimal)>>) availability, (IEnumerable<(SOShipLine, SOShipLineSplit)>) tuple.Item4, (IReadOnlyDictionary<(int?, string), DateTime>) expirationDates);
          foreach ((SOShipLine Line, SOShipLineSplit Split) detail in outer)
            this.DecreaseAvailability((IReadOnlyDictionary<(int?, int?, string, int?, string), List<(int?, Decimal)>>) availability, detail);
          this.CreateTransferToStorageLocations(lazy1.Value, tuple.Item2, transferSplits.Select<(SOShipLineSplit, INTranSplit), INTranSplit>((Func<(SOShipLineSplit, INTranSplit), INTranSplit>) (s => s.tranSplit)));
          this.PutShipmentOnStorageLocations(lazy2.Value, tuple.Item1, EnumerableExtensions.Distinct<(SOShipLineSplit, int?), int?>(outer.Join<(SOShipLine, SOShipLineSplit), (SOShipLineSplit, INTranSplit), int?, (SOShipLineSplit, int?)>(transferSplits, (Func<(SOShipLine, SOShipLineSplit), int?>) (asp => asp.Split.LineNbr), (Func<(SOShipLineSplit, INTranSplit), int?>) (tsp => tsp.soSplit.LineNbr), (Func<(SOShipLine, SOShipLineSplit), (SOShipLineSplit, INTranSplit), (SOShipLineSplit, int?)>) ((asp, tsp) => (asp.Split, tsp.tranSplit.ToLocationID))), (Func<(SOShipLineSplit, int?), int?>) (r => r.Split.SplitLineNbr)));
          stringList.Add(tuple.Item1);
        }
      }
      transactionScope.Complete();
    }
    return (IEnumerable<string>) stringList;
  }

  protected virtual IEnumerable<(SOShipLine Line, SOShipLineSplit Split)> MakeAllSplitsAssigned(
    SOShipmentEntry shipmentEntry,
    string shipmentNbr,
    IReadOnlyDictionary<(int? InventoryID, int? SubItemID, string UOM, int? LocationID, string LotSerialNbr), List<(int? SortLocationID, Decimal PickedQty)>> availability,
    IEnumerable<(SOShipLine Line, SOShipLineSplit Split)> details,
    IReadOnlyDictionary<(int? InventoryID, string LotSerialNbr), DateTime> expirationDates)
  {
    (IEnumerable<(SOShipLine, SOShipLineSplit)> source, IEnumerable<(SOShipLine, SOShipLineSplit)> first) = EnumerableExtensions.DisuniteBy<(SOShipLine, SOShipLineSplit)>(details, (Func<(SOShipLine, SOShipLineSplit), bool>) (r => r.Split.IsUnassigned.GetValueOrDefault() || r.Split.HasGeneratedLotSerialNbr.GetValueOrDefault() || this.isEnterableOnIssue(r.Split.InventoryID)));
    if (!source.Any<(SOShipLine, SOShipLineSplit)>())
      return first;
    ((PXGraph) shipmentEntry).Clear();
    ((PXSelectBase<SOShipment>) shipmentEntry.Document).Current = PXResultset<SOShipment>.op_Implicit(((PXSelectBase<SOShipment>) shipmentEntry.Document).Search<SOShipment.shipmentNbr>((object) shipmentNbr, Array.Empty<object>()));
    PXSelectBase<SOShipLine> transactions = (PXSelectBase<SOShipLine>) shipmentEntry.Transactions;
    PXSelectBase<SOShipLineSplit> splits = (PXSelectBase<SOShipLineSplit>) shipmentEntry.splits;
    HashSet<int?> enterableItems = source.Select<(SOShipLine, SOShipLineSplit), int?>((Func<(SOShipLine, SOShipLineSplit), int?>) (s => s.Split.InventoryID)).ToHashSet<int?>();
    Queue<((int?, int?, string, int?), int?, string, Decimal)> queue = EnumerableExtensions.ToQueue<((int?, int?, string, int?), int?, string, Decimal)>((IEnumerable<((int?, int?, string, int?), int?, string, Decimal)>) availability.Where<KeyValuePair<(int?, int?, string, int?, string), List<(int?, Decimal)>>>((Func<KeyValuePair<(int?, int?, string, int?, string), List<(int?, Decimal)>>, bool>) (a => enterableItems.Contains(a.Key.InventoryID))).SelectMany<KeyValuePair<(int?, int?, string, int?, string), List<(int?, Decimal)>>, ((int?, int?, string, int?), int?, string, Decimal)>((Func<KeyValuePair<(int?, int?, string, int?, string), List<(int?, Decimal)>>, IEnumerable<((int?, int?, string, int?), int?, string, Decimal)>>) (a => a.Value.Select<(int?, Decimal), ((int?, int?, string, int?), int?, string, Decimal)>((Func<(int?, Decimal), ((int?, int?, string, int?), int?, string, Decimal)>) (e => ((a.Key.InventoryID, a.Key.SubItemID, a.Key.UOM, a.Key.LocationID), e.SortLocationID, a.Key.LotSerialNbr, e.PickedQty))))).OrderBy<((int?, int?, string, int?), int?, string, Decimal), int?>((Func<((int?, int?, string, int?), int?, string, Decimal), int?>) (e => e.Key.InventoryID)).ThenBy<((int?, int?, string, int?), int?, string, Decimal), int?>((Func<((int?, int?, string, int?), int?, string, Decimal), int?>) (e => e.Key.SubItemID)).ThenBy<((int?, int?, string, int?), int?, string, Decimal), string>((Func<((int?, int?, string, int?), int?, string, Decimal), string>) (e => e.Key.UOM)).ThenBy<((int?, int?, string, int?), int?, string, Decimal), int?>((Func<((int?, int?, string, int?), int?, string, Decimal), int?>) (e => e.Key.LocationID)).ThenBy<((int?, int?, string, int?), int?, string, Decimal), string>((Func<((int?, int?, string, int?), int?, string, Decimal), string>) (e => e.LotSerialNbr)));
    IEnumerable<(SOShipLine, SOShipLineSplit)> array = (IEnumerable<(SOShipLine, SOShipLineSplit)>) source.OrderBy<(SOShipLine, SOShipLineSplit), int?>((Func<(SOShipLine, SOShipLineSplit), int?>) (e => e.Split.InventoryID)).ThenBy<(SOShipLine, SOShipLineSplit), int?>((Func<(SOShipLine, SOShipLineSplit), int?>) (e => e.Split.SubItemID)).ThenBy<(SOShipLine, SOShipLineSplit), string>((Func<(SOShipLine, SOShipLineSplit), string>) (e => e.Line.UOM)).ThenBy<(SOShipLine, SOShipLineSplit), int?>((Func<(SOShipLine, SOShipLineSplit), int?>) (e => e.Split.LocationID)).ThenBy<(SOShipLine, SOShipLineSplit), int?>((Func<(SOShipLine, SOShipLineSplit), int?>) (e => e.Line.LineNbr)).ToArray<(SOShipLine, SOShipLineSplit)>();
    ((int?, int?, string, int?), int?, string, Decimal) valueTuple1 = queue.Dequeue();
    Decimal val2 = valueTuple1.Item4;
    List<(SOShipLine, SOShipLineSplit)> second = new List<(SOShipLine, SOShipLineSplit)>();
    using (IEnumerator<(SOShipLine, SOShipLineSplit)> enumerator = array.GetEnumerator())
    {
label_20:
      while (enumerator.MoveNext())
      {
        (SOShipLine, SOShipLineSplit) current = enumerator.Current;
        transactions.Current = PXResultset<SOShipLine>.op_Implicit(transactions.Search<SOShipLine.shipmentNbr, SOShipLine.lineNbr>((object) shipmentNbr, (object) current.Item1.LineNbr, Array.Empty<object>()));
        Decimal val1 = current.Item2.Qty.Value;
        (int?, int?, string, int?) valueTuple2 = (current.Item2.InventoryID, current.Item2.SubItemID, current.Item1.UOM, current.Item2.LocationID);
        while (!valueTuple2.Equals(valueTuple1.Item1))
        {
          if (queue.Count == 0)
          {
            valueTuple1 = ();
            val2 = 0M;
            break;
          }
          valueTuple1 = queue.Dequeue();
          val2 = valueTuple1.Item4;
        }
        while (true)
        {
          do
          {
            if (val1 > 0M && val2 > 0M && valueTuple2.Equals(valueTuple1.Item1))
            {
              Decimal num1 = Math.Min(val1, val2);
              val1 -= num1;
              val2 -= num1;
              if (!current.Item2.HasGeneratedLotSerialNbr.GetValueOrDefault())
              {
                bool? isUnassigned = current.Item2.IsUnassigned;
                bool flag = false;
                if (!(isUnassigned.GetValueOrDefault() == flag & isUnassigned.HasValue) || !this.isEnterableOnIssue(current.Item2.InventoryID))
                  goto label_14;
              }
              Decimal? qty1 = current.Item2.Qty;
              Decimal num2 = num1;
              if (qty1.GetValueOrDefault() == num2 & qty1.HasValue)
              {
                splits.Delete(current.Item2);
              }
              else
              {
                SOShipLineSplit soShipLineSplit = current.Item2;
                Decimal? qty2 = soShipLineSplit.Qty;
                Decimal num3 = num1;
                soShipLineSplit.Qty = qty2.HasValue ? new Decimal?(qty2.GetValueOrDefault() - num3) : new Decimal?();
                splits.Update(current.Item2);
              }
label_14:
              SOShipLineSplit soShipLineSplit1 = splits.Insert();
              soShipLineSplit1.Qty = new Decimal?(num1);
              soShipLineSplit1.BaseQty = new Decimal?(num1);
              soShipLineSplit1.LocationID = valueTuple1.Item1.Item4;
              soShipLineSplit1.LotSerialNbr = valueTuple1.Item3;
              DateTime dateTime;
              if (expirationDates.TryGetValue((valueTuple1.Item1.Item1, valueTuple1.Item3), out dateTime))
                soShipLineSplit1.ExpireDate = new DateTime?(dateTime);
              SOShipLineSplit soShipLineSplit2 = splits.Update(soShipLineSplit1);
              second.Add((transactions.Current, soShipLineSplit2));
            }
            else
              goto label_20;
          }
          while (!(val2 == 0M));
          if (queue.Count != 0)
          {
            valueTuple1 = queue.Dequeue();
            val2 = valueTuple1.Item4;
          }
          else
            goto label_20;
        }
      }
    }
    ((PXAction) shipmentEntry.Save).Press();
    return (IEnumerable<(SOShipLine, SOShipLineSplit)>) first.Concat<(SOShipLine, SOShipLineSplit)>((IEnumerable<(SOShipLine, SOShipLineSplit)>) second).OrderBy<(SOShipLine, SOShipLineSplit), int?>((Func<(SOShipLine, SOShipLineSplit), int?>) (r => r.Line.LineNbr)).ThenBy<(SOShipLine, SOShipLineSplit), int?>((Func<(SOShipLine, SOShipLineSplit), int?>) (r => r.Split.SplitLineNbr)).ToArray<(SOShipLine, SOShipLineSplit)>();
  }

  protected virtual IEnumerable<(SOShipLineSplit soSplit, INTranSplit tranSplit)> CreateTransferSplits(
    IReadOnlyDictionary<(int? InventoryID, int? SubItemID, string UOM, int? LocationID, string LotSerialNbr), List<(int? SortLocationID, Decimal PickedQty)>> availability,
    IEnumerable<(SOShipLine Line, SOShipLineSplit Split)> details)
  {
    Dictionary<(int?, int?, string, int?), List<(int?, string, Decimal)>> dictionary = availability.Select<KeyValuePair<(int?, int?, string, int?, string), List<(int?, Decimal)>>, ((int?, int?, string, int?), List<(int?, string, Decimal)>)>((Func<KeyValuePair<(int?, int?, string, int?, string), List<(int?, Decimal)>>, ((int?, int?, string, int?), List<(int?, string, Decimal)>)>) (kvp => ((kvp.Key.InventoryID, kvp.Key.SubItemID, kvp.Key.UOM, kvp.Key.LocationID), kvp.Value.Select<(int?, Decimal), (int?, string, Decimal)>((Func<(int?, Decimal), (int?, string, Decimal)>) (e => (e.SortLocationID, this.isWhenUsed(kvp.Key.InventoryID) ? "" : kvp.Key.LotSerialNbr, e.PickedQty))).ToList<(int?, string, Decimal)>()))).GroupBy<((int?, int?, string, int?), List<(int?, string, Decimal)>), (int?, int?, string, int?)>((Func<((int?, int?, string, int?), List<(int?, string, Decimal)>), (int?, int?, string, int?)>) (r => r.Key)).ToDictionary<IGrouping<(int?, int?, string, int?), ((int?, int?, string, int?), List<(int?, string, Decimal)>)>, (int?, int?, string, int?), List<(int?, string, Decimal)>>((Func<IGrouping<(int?, int?, string, int?), ((int?, int?, string, int?), List<(int?, string, Decimal)>)>, (int?, int?, string, int?)>) (r => r.Key), (Func<IGrouping<(int?, int?, string, int?), ((int?, int?, string, int?), List<(int?, string, Decimal)>)>, List<(int?, string, Decimal)>>) (r => r.Aggregate<((int?, int?, string, int?), List<(int?, string, Decimal)>), IEnumerable<(int?, string, Decimal)>>(Enumerable.Empty<(int?, string, Decimal)>(), (Func<IEnumerable<(int?, string, Decimal)>, ((int?, int?, string, int?), List<(int?, string, Decimal)>), IEnumerable<(int?, string, Decimal)>>) ((acc, elem) => acc.Concat<(int?, string, Decimal)>((IEnumerable<(int?, string, Decimal)>) elem.Value))).GroupBy<(int?, string, Decimal), (int?, string), Decimal>((Func<(int?, string, Decimal), (int?, string)>) (t => (t.SortLocationID, t.LotSerialNbr)), (Func<(int?, string, Decimal), Decimal>) (t => t.PickedQty)).Select<IGrouping<(int?, string), Decimal>, (int?, string, Decimal)>((Func<IGrouping<(int?, string), Decimal>, (int?, string, Decimal)>) (t => (t.Key.SortLocationID, t.Key.LotSerialNbr, t.Sum()))).ToList<(int?, string, Decimal)>()));
    List<(SOShipLineSplit, INTranSplit)> transferSplits = new List<(SOShipLineSplit, INTranSplit)>();
    foreach ((_, _) in details)
    {
      (SOShipLine, SOShipLineSplit) detail;
      (int?, int?, string, int?) key = (detail.Item2.InventoryID, detail.Item2.SubItemID, detail.Item1.UOM, detail.Item2.LocationID);
      Decimal val2 = detail.Item2.BaseQty.Value;
      foreach ((int?, string, Decimal) tuple in (IEnumerable<(int?, string, Decimal)>) dictionary[key].OrderByDescending<(int?, string, Decimal), bool>((Func<(int?, string, Decimal), bool>) (a => string.Equals(a.LotSerialNbr, detail.Item2.LotSerialNbr, StringComparison.OrdinalIgnoreCase))).OrderBy<(int?, string, Decimal), Decimal>((Func<(int?, string, Decimal), Decimal>) (a => a.PickedQty)))
      {
        Decimal num = Math.Min(tuple.Item3, val2);
        if (tuple.Item3 > num)
        {
          Decimal restPickedQty = tuple.Item3 - num;
          dictionary[key].Insert(dictionary[key].TakeWhile<(int?, string, Decimal)>((Func<(int?, string, Decimal), bool>) (a => a.PickedQty < restPickedQty)).Count<(int?, string, Decimal)>(), (tuple.Item1, tuple.Item2, restPickedQty));
        }
        dictionary[key].Remove(tuple);
        INTranSplit inTranSplit = new INTranSplit()
        {
          SiteID = detail.Item2.SiteID,
          ToSiteID = detail.Item2.SiteID,
          LocationID = detail.Item2.LocationID,
          ToLocationID = tuple.Item1,
          InventoryID = detail.Item2.InventoryID,
          SubItemID = detail.Item2.SubItemID,
          LotSerialNbr = tuple.Item2,
          ExpireDate = detail.Item2.ExpireDate,
          Qty = new Decimal?(num),
          UOM = detail.Item2.UOM,
          BaseQty = new Decimal?(num),
          ShipmentNbr = detail.Item2.ShipmentNbr,
          ShipmentLineNbr = detail.Item2.LineNbr,
          ShipmentLineSplitNbr = detail.Item2.SplitLineNbr
        };
        transferSplits.Add((detail.Item2, inTranSplit));
        val2 -= num;
        if (val2 <= 0M)
          break;
      }
    }
    return (IEnumerable<(SOShipLineSplit, INTranSplit)>) transferSplits;
  }

  protected virtual (int? SortLocationID, Decimal PickedQty) DecreaseAvailability(
    IReadOnlyDictionary<(int? InventoryID, int? SubItemID, string UOM, int? LocationID, string LotSerialNbr), List<(int? SortLocationID, Decimal PickedQty)>> availability,
    (SOShipLine Line, SOShipLineSplit Split) detail)
  {
    (int?, int?, string, int?, string) key = (detail.Split.InventoryID, detail.Split.SubItemID, detail.Line.UOM, detail.Split.LocationID, detail.Split.LotSerialNbr);
    (int?, Decimal) tuple = availability[key].OrderBy<(int?, Decimal), Decimal>((Func<(int?, Decimal), Decimal>) (a => a.PickedQty)).First<(int?, Decimal)>((Func<(int?, Decimal), bool>) (a =>
    {
      Decimal pickedQty = a.PickedQty;
      Decimal? baseQty = detail.Split.BaseQty;
      Decimal valueOrDefault = baseQty.GetValueOrDefault();
      return pickedQty >= valueOrDefault & baseQty.HasValue;
    }));
    Decimal num1 = tuple.Item2;
    Decimal? baseQty1 = detail.Split.BaseQty;
    Decimal valueOrDefault1 = baseQty1.GetValueOrDefault();
    if (num1 > valueOrDefault1 & baseQty1.HasValue)
    {
      Decimal num2 = tuple.Item2;
      baseQty1 = detail.Split.BaseQty;
      Decimal num3 = baseQty1.Value;
      Decimal restQty = num2 - num3;
      availability[key].Insert(availability[key].TakeWhile<(int?, Decimal)>((Func<(int?, Decimal), bool>) (a => a.PickedQty < restQty)).Count<(int?, Decimal)>(), (tuple.Item1, restQty));
    }
    availability[key].Remove(tuple);
    return tuple;
  }

  protected virtual void HoldShipment(SOShipmentEntry shipmentEntry, string shipmentNbr)
  {
    ((PXGraph) shipmentEntry).Clear();
    ((PXSelectBase<SOShipment>) shipmentEntry.Document).Current = PXResultset<SOShipment>.op_Implicit(((PXSelectBase<SOShipment>) shipmentEntry.Document).Search<SOShipment.shipmentNbr>((object) shipmentNbr, Array.Empty<object>()));
    ((PXAction) shipmentEntry.putOnHold).Press();
  }

  protected virtual void CreateTransferToStorageLocations(
    INTransferEntry transferEntry,
    int? siteID,
    IEnumerable<INTranSplit> tranSplits)
  {
    ((PXGraph) transferEntry).Clear();
    ((PXSelectBase<INSetup>) transferEntry.insetup).Current.RequireControlTotal = new bool?(false);
    transferEntry.transfer.With<PXSelectJoin<PX.Objects.IN.INRegister, LeftJoin<PX.Objects.IN.INSite, On<PX.Objects.IN.INSite.siteID, Equal<PX.Objects.IN.INRegister.siteID>>>, Where<PX.Objects.IN.INRegister.docType, Equal<INDocType.transfer>, And<Where<PX.Objects.IN.INSite.siteID, IsNull, Or<Match<PX.Objects.IN.INSite, Current<AccessInfo.userName>>>>>>>, PX.Objects.IN.INRegister>((Func<PXSelectJoin<PX.Objects.IN.INRegister, LeftJoin<PX.Objects.IN.INSite, On<PX.Objects.IN.INSite.siteID, Equal<PX.Objects.IN.INRegister.siteID>>>, Where<PX.Objects.IN.INRegister.docType, Equal<INDocType.transfer>, And<Where<PX.Objects.IN.INSite.siteID, IsNull, Or<Match<PX.Objects.IN.INSite, Current<AccessInfo.userName>>>>>>>, PX.Objects.IN.INRegister>) (_ => ((PXSelectBase<PX.Objects.IN.INRegister>) _).Insert() ?? ((PXSelectBase<PX.Objects.IN.INRegister>) _).Insert()));
    ((PXSelectBase<PX.Objects.IN.INRegister>) transferEntry.transfer).SetValueExt<PX.Objects.IN.INRegister.siteID>(((PXSelectBase<PX.Objects.IN.INRegister>) transferEntry.transfer).Current, (object) siteID);
    ((PXSelectBase<PX.Objects.IN.INRegister>) transferEntry.transfer).SetValueExt<PX.Objects.IN.INRegister.toSiteID>(((PXSelectBase<PX.Objects.IN.INRegister>) transferEntry.transfer).Current, (object) siteID);
    ((PXSelectBase<PX.Objects.IN.INRegister>) transferEntry.transfer).UpdateCurrent();
    foreach (INTranSplit tranSplit in tranSplits)
    {
      if (PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this.Base, tranSplit.InventoryID).StkItem.GetValueOrDefault())
      {
        INTran inTran1 = transferEntry.transactions.With<PXSelect<INTran, Where<INTran.docType, Equal<INDocType.transfer>, And<INTran.refNbr, Equal<Current<PX.Objects.IN.INRegister.refNbr>>, And<INTran.invtMult, In3<InventoryMultiplicator.decrease, InventoryMultiplicator.noUpdate>>>>>, INTran>((Func<PXSelect<INTran, Where<INTran.docType, Equal<INDocType.transfer>, And<INTran.refNbr, Equal<Current<PX.Objects.IN.INRegister.refNbr>>, And<INTran.invtMult, In3<InventoryMultiplicator.decrease, InventoryMultiplicator.noUpdate>>>>>, INTran>) (_ => ((PXSelectBase<INTran>) _).Insert() ?? ((PXSelectBase<INTran>) _).Insert()));
        inTran1.InventoryID = tranSplit.InventoryID;
        inTran1.SubItemID = tranSplit.SubItemID;
        inTran1.LotSerialNbr = tranSplit.LotSerialNbr;
        inTran1.ExpireDate = tranSplit.ExpireDate;
        inTran1.UOM = tranSplit.UOM;
        INTran inTran2 = ((PXSelectBase<INTran>) transferEntry.transactions).Update(inTran1);
        inTran2.SiteID = tranSplit.SiteID;
        inTran2.LocationID = tranSplit.LocationID;
        inTran2.ToSiteID = tranSplit.SiteID;
        inTran2.ToLocationID = tranSplit.ToLocationID;
        INTran inTran3 = ((PXSelectBase<INTran>) transferEntry.transactions).Update(inTran2);
        INTranSplit inTranSplit = PXResultset<INTranSplit>.op_Implicit(((PXSelectBase<INTranSplit>) transferEntry.splits).Search<INTranSplit.lineNbr>((object) inTran3.LineNbr, Array.Empty<object>()));
        if (inTranSplit == null)
        {
          inTranSplit = transferEntry.splits.With<PXSelect<INTranSplit, Where<INTranSplit.docType, Equal<INDocType.transfer>, And<INTranSplit.refNbr, Equal<Current<INTran.refNbr>>, And<INTranSplit.lineNbr, Equal<Current<INTran.lineNbr>>>>>>, INTranSplit>((Func<PXSelect<INTranSplit, Where<INTranSplit.docType, Equal<INDocType.transfer>, And<INTranSplit.refNbr, Equal<Current<INTran.refNbr>>, And<INTranSplit.lineNbr, Equal<Current<INTran.lineNbr>>>>>>, INTranSplit>) (_ => ((PXSelectBase<INTranSplit>) _).Insert() ?? ((PXSelectBase<INTranSplit>) _).Insert()));
          inTranSplit.LotSerialNbr = tranSplit.LotSerialNbr;
          inTranSplit.ExpireDate = tranSplit.ExpireDate;
          inTranSplit.ToSiteID = tranSplit.SiteID;
          inTranSplit.ToLocationID = tranSplit.ToLocationID;
        }
        inTranSplit.ShipmentNbr = tranSplit.ShipmentNbr;
        inTranSplit.ShipmentLineNbr = tranSplit.ShipmentLineNbr;
        inTranSplit.ShipmentLineSplitNbr = tranSplit.ShipmentLineSplitNbr;
        inTranSplit.Qty = tranSplit.Qty;
        ((PXSelectBase<INTranSplit>) transferEntry.splits).Update(inTranSplit);
      }
    }
    ((PXSelectBase<PX.Objects.IN.INRegister>) transferEntry.transfer).SetValueExt<PX.Objects.IN.INRegister.hold>(((PXSelectBase<PX.Objects.IN.INRegister>) transferEntry.transfer).Current, (object) false);
    ((PXAction) transferEntry.release).Press();
  }

  protected virtual void PutShipmentOnStorageLocations(
    SOShipmentEntry shipmentEntry,
    string shipmentNbr,
    IEnumerable<(SOShipLineSplit Split, int? SortLocationID)> soSplitToSortLocation)
  {
    ((PXGraph) shipmentEntry).Clear();
    NonStockKitSpecHelper stockKitSpecHelper = new NonStockKitSpecHelper((PXGraph) shipmentEntry);
    ((PXSelectBase<SOShipment>) shipmentEntry.Document).Current = PXResultset<SOShipment>.op_Implicit(((PXSelectBase<SOShipment>) shipmentEntry.Document).Search<SOShipment.shipmentNbr>((object) shipmentNbr, Array.Empty<object>()));
    PXSelectBase<SOShipLine> transactions = (PXSelectBase<SOShipLine>) shipmentEntry.Transactions;
    PXSelectBase<SOShipLineSplit> splits = (PXSelectBase<SOShipLineSplit>) shipmentEntry.splits;
    Func<int, bool> RequireShipping = Func.Memorize<int, bool>((Func<int, bool>) (inventoryID => PX.Objects.IN.InventoryItem.PK.Find((PXGraph) shipmentEntry, new int?(inventoryID)).With<PX.Objects.IN.InventoryItem, bool>((Func<PX.Objects.IN.InventoryItem, bool>) (item => item.StkItem.GetValueOrDefault() || item.NonStockShip.GetValueOrDefault()))));
    Decimal num1 = 0M;
    foreach (IGrouping<int?, (SOShipLineSplit, int?)> grouping in soSplitToSortLocation.GroupBy<(SOShipLineSplit, int?), int?>((Func<(SOShipLineSplit, int?), int?>) (d => d.Split.LineNbr)))
    {
      transactions.Current = PXResultset<SOShipLine>.op_Implicit(transactions.Search<SOShipLine.shipmentNbr, SOShipLine.lineNbr>((object) shipmentNbr, (object) grouping.Key, Array.Empty<object>()));
      Decimal num2 = 0M;
      Decimal? nullable;
      foreach ((SOShipLineSplit, int?) tuple in (IEnumerable<(SOShipLineSplit, int?)>) grouping)
      {
        splits.Current = PXResultset<SOShipLineSplit>.op_Implicit(splits.Search<SOShipLineSplit.shipmentNbr, SOShipLineSplit.lineNbr, SOShipLineSplit.splitLineNbr>((object) shipmentNbr, (object) grouping.Key, (object) tuple.Item1.SplitLineNbr, Array.Empty<object>()));
        splits.SetValueExt<SOShipLineSplit.locationID>(splits.Current, (object) tuple.Item2);
        splits.SetValueExt<SOShipLineSplit.pickedQty>(splits.Current, (object) tuple.Item1.Qty);
        splits.SetValueExt<SOShipLineSplit.basePickedQty>(splits.Current, (object) tuple.Item1.BaseQty);
        splits.UpdateCurrent();
        Decimal num3 = num2;
        nullable = splits.Current.BaseQty;
        Decimal num4 = nullable.Value;
        num2 = num3 + num4;
      }
      if (stockKitSpecHelper.IsNonStockKit(transactions.Current.InventoryID))
      {
        Dictionary<int, Decimal> dictionary1 = EnumerableExtensions.ToDictionary<int, Decimal>(stockKitSpecHelper.GetNonStockKitSpec(transactions.Current.InventoryID.Value).Where<KeyValuePair<int, Decimal>>((Func<KeyValuePair<int, Decimal>, bool>) (pair => pair.Value != 0M && RequireShipping(pair.Key))));
        Dictionary<int, Decimal> dictionary2 = ((IEnumerable<SOShipLineSplit>) splits.SelectMain(Array.Empty<object>())).GroupBy<SOShipLineSplit, int>((Func<SOShipLineSplit, int>) (r => r.InventoryID.Value)).ToDictionary<IGrouping<int, SOShipLineSplit>, int, Decimal>((Func<IGrouping<int, SOShipLineSplit>, int>) (g => g.Key), (Func<IGrouping<int, SOShipLineSplit>, Decimal>) (g => g.Sum<SOShipLineSplit>((Func<SOShipLineSplit, Decimal>) (s => s.PickedQty.GetValueOrDefault()))));
        Decimal num5 = dictionary1.Keys.Count<int>() == 0 || dictionary1.Keys.Except<int>((IEnumerable<int>) dictionary2.Keys).Count<int>() > 0 ? 0M : dictionary2.Join<KeyValuePair<int, Decimal>, KeyValuePair<int, Decimal>, int, Decimal>((IEnumerable<KeyValuePair<int, Decimal>>) dictionary1, (Func<KeyValuePair<int, Decimal>, int>) (split => split.Key), (Func<KeyValuePair<int, Decimal>, int>) (spec => spec.Key), (Func<KeyValuePair<int, Decimal>, KeyValuePair<int, Decimal>, Decimal>) ((split, spec) =>
        {
          KeyValuePair<int, Decimal> keyValuePair = split;
          Decimal d1 = keyValuePair.Value;
          keyValuePair = spec;
          Decimal d2 = keyValuePair.Value;
          return Math.Floor(Decimal.Divide(d1, d2));
        })).Min();
        num2 = INUnitAttribute.ConvertToBase(((PXSelectBase) transactions).Cache, transactions.Current.InventoryID, transactions.Current.UOM, num5, INPrecision.NOROUND);
      }
      transactions.Current.BasePickedQty = new Decimal?(num2);
      transactions.Current.PickedQty = new Decimal?(INUnitAttribute.ConvertFromBase(((PXSelectBase) transactions).Cache, transactions.Current.InventoryID, transactions.Current.UOM, num2, INPrecision.NOROUND));
      GraphHelper.MarkUpdated(((PXSelectBase) transactions).Cache, (object) transactions.Current, true);
      Decimal num6 = num1;
      nullable = transactions.Current.PickedQty;
      Decimal num7 = nullable.Value;
      num1 = num6 + num7;
    }
    ((PXSelectBase<SOShipment>) shipmentEntry.Document).SetValueExt<SOShipment.picked>(((PXSelectBase<SOShipment>) shipmentEntry.Document).Current, (object) true);
    ((PXSelectBase<SOShipment>) shipmentEntry.Document).SetValueExt<SOShipment.pickedQty>(((PXSelectBase<SOShipment>) shipmentEntry.Document).Current, (object) num1);
    ((PXSelectBase<SOShipment>) shipmentEntry.Document).SetValueExt<SOShipment.pickedViaWorksheet>(((PXSelectBase<SOShipment>) shipmentEntry.Document).Current, (object) true);
    ((PXSelectBase<SOShipment>) shipmentEntry.Document).UpdateCurrent();
    ((PXAction) shipmentEntry.releaseFromHold).Press();
  }

  protected virtual IEnumerable<string> FulfillShipmentsSingle(SOPickingWorksheet worksheet)
  {
    return this.FulfillShipmentsGrouped(worksheet);
  }

  protected virtual IEnumerable<string> FulfillShipmentsWave(SOPickingWorksheet worksheet)
  {
    return this.FulfillShipmentsGrouped(worksheet);
  }

  protected virtual IEnumerable<string> FulfillShipmentsGrouped(SOPickingWorksheet worksheet)
  {
    ((PXGraph) this.Base).Clear();
    ((PXSelectBase<SOPickingWorksheet>) this.Base.worksheet).Current = SOPickingWorksheet.PK.Find((PXGraph) this.Base, worksheet.WorksheetNbr, (PKFindOptions) 1).With<SOPickingWorksheet, SOPickingWorksheet>(new Func<SOPickingWorksheet, SOPickingWorksheet>(this.ValidateMutability));
    SOShipment[] array = EnumerableExtensions.Distinct<PXResult<SOShipment, SOPickerToShipmentLink, SOPicker>, string>(((IEnumerable<PXResult<SOShipment>>) PXSelectBase<SOShipment, PXViewOf<SOShipment>.BasedOn<SelectFromBase<SOShipment, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<SOPickerToShipmentLink>.On<SOPickerToShipmentLink.FK.Shipment>>, FbqlJoins.Inner<SOPicker>.On<SOPickerToShipmentLink.FK.Picker>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOPicker.confirmed, Equal<True>>>>>.And<KeysRelation<Field<SOPicker.worksheetNbr>.IsRelatedTo<SOPickingWorksheet.worksheetNbr>.AsSimpleKey.WithTablesOf<SOPickingWorksheet, SOPicker>, SOPickingWorksheet, SOPicker>.SameAsCurrent>>>.Config>.Select((PXGraph) this.Base, Array.Empty<object>())).AsEnumerable<PXResult<SOShipment>>().Cast<PXResult<SOShipment, SOPickerToShipmentLink, SOPicker>>(), (Func<PXResult<SOShipment, SOPickerToShipmentLink, SOPicker>, string>) (row => ((PXResult) row).GetItem<SOShipment>().ShipmentNbr)).GroupBy<PXResult<SOShipment, SOPickerToShipmentLink, SOPicker>, object, SOShipment>((Func<PXResult<SOShipment, SOPickerToShipmentLink, SOPicker>, object>) (row => (object) PXResult<SOShipment, SOPickerToShipmentLink, SOPicker>.op_Implicit(row)), (Func<PXResult<SOShipment, SOPickerToShipmentLink, SOPicker>, SOShipment>) (row => PXResult<SOShipment, SOPickerToShipmentLink, SOPicker>.op_Implicit(row)), PXCacheEx.GetComparer(((PXSelectBase) this.Base.pickers).Cache)).Where<IGrouping<object, SOShipment>>((Func<IGrouping<object, SOShipment>, bool>) (g => g.All<SOShipment>((Func<SOShipment, bool>) (sh =>
    {
      bool? picked = sh.Picked;
      bool flag = false;
      return picked.GetValueOrDefault() == flag & picked.HasValue;
    })))).SelectMany<IGrouping<object, SOShipment>, SOShipment>((Func<IGrouping<object, SOShipment>, IEnumerable<SOShipment>>) (g => (IEnumerable<SOShipment>) g)).ToArray<SOShipment>();
    Lazy<SOShipmentEntry> lazy = Lazy.By<SOShipmentEntry>((Func<SOShipmentEntry>) (() => PXGraph.CreateInstance<SOShipmentEntry>()));
    List<string> stringList = new List<string>();
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      foreach (SOShipment soShipment in array)
      {
        this.MarkShipmentPicked(lazy.Value, soShipment.ShipmentNbr);
        stringList.Add(soShipment.ShipmentNbr);
      }
      transactionScope.Complete();
    }
    return (IEnumerable<string>) stringList;
  }

  protected virtual void MarkShipmentPicked(SOShipmentEntry shipmentEntry, string shipmentNbr)
  {
    ((PXGraph) shipmentEntry).Clear();
    NonStockKitSpecHelper stockKitSpecHelper = new NonStockKitSpecHelper((PXGraph) shipmentEntry);
    SOPicker topFirst = PXSelectBase<SOPicker, PXViewOf<SOPicker>.BasedOn<SelectFromBase<SOPicker, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<SOPickerToShipmentLink>.On<SOPickerToShipmentLink.FK.Picker>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOPicker.worksheetNbr, Equal<P.AsString>>>>>.And<BqlOperand<SOPickerToShipmentLink.shipmentNbr, IBqlString>.IsEqual<P.AsString>>>>.Config>.Select((PXGraph) shipmentEntry, new object[2]
    {
      (object) ((PXSelectBase<SOPickingWorksheet>) this.Base.worksheet).Current.WorksheetNbr,
      (object) shipmentNbr
    }).TopFirst;
    Dictionary<(int?, int?, string, int?, string), (Decimal, ReadOnlyCollection<SOPickListEntryToCartSplitLink>, DateTime?)> availability = ((IEnumerable<(SOPickerListEntry, SOPickListEntryToCartSplitLink)>) ((IEnumerable<PXResult<SOPickerListEntry>>) PXSelectBase<SOPickerListEntry, PXViewOf<SOPickerListEntry>.BasedOn<SelectFromBase<SOPickerListEntry, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<SOPickListEntryToCartSplitLink>.On<SOPickListEntryToCartSplitLink.FK.PickListEntry>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOPickerListEntry.worksheetNbr, Equal<P.AsString>>>>, And<BqlOperand<SOPickerListEntry.pickerNbr, IBqlInt>.IsEqual<P.AsInt>>>>.And<BqlOperand<SOPickerListEntry.shipmentNbr, IBqlString>.IsEqual<P.AsString>>>>.Config>.Select((PXGraph) shipmentEntry, new object[3]
    {
      (object) topFirst.WorksheetNbr,
      (object) topFirst.PickerNbr,
      (object) shipmentNbr
    })).AsEnumerable<PXResult<SOPickerListEntry>>().Select<PXResult<SOPickerListEntry>, (SOPickerListEntry, SOPickListEntryToCartSplitLink)>((Func<PXResult<SOPickerListEntry>, (SOPickerListEntry, SOPickListEntryToCartSplitLink)>) (r => (((PXResult) r).GetItem<SOPickerListEntry>(), ((PXResult) r).GetItem<SOPickListEntryToCartSplitLink>()))).ToArray<(SOPickerListEntry, SOPickListEntryToCartSplitLink)>()).GroupBy<(SOPickerListEntry, SOPickListEntryToCartSplitLink), (int?, int?, string, int?, string)>((Func<(SOPickerListEntry, SOPickListEntryToCartSplitLink), (int?, int?, string, int?, string)>) (e => (e.Entry.InventoryID, e.Entry.SubItemID, e.Entry.OrderLineUOM, e.Entry.LocationID, e.Entry.LotSerialNbr))).Select<IGrouping<(int?, int?, string, int?, string), (SOPickerListEntry, SOPickListEntryToCartSplitLink)>, ((int?, int?, string, int?, string), Decimal, ReadOnlyCollection<SOPickListEntryToCartSplitLink>, DateTime?)>((Func<IGrouping<(int?, int?, string, int?, string), (SOPickerListEntry, SOPickListEntryToCartSplitLink)>, ((int?, int?, string, int?, string), Decimal, ReadOnlyCollection<SOPickListEntryToCartSplitLink>, DateTime?)>) (g => (g.Key, g.Sum<(SOPickerListEntry, SOPickListEntryToCartSplitLink)>((Func<(SOPickerListEntry, SOPickListEntryToCartSplitLink), Decimal>) (e => e.Entry.PickedQty.GetValueOrDefault())), EnumerableExtensions.WhereNotNull<SOPickListEntryToCartSplitLink>(g.Select<(SOPickerListEntry, SOPickListEntryToCartSplitLink), SOPickListEntryToCartSplitLink>((Func<(SOPickerListEntry, SOPickListEntryToCartSplitLink), SOPickListEntryToCartSplitLink>) (e => e.Link))).ToList<SOPickListEntryToCartSplitLink>().AsReadOnly(), this.isEnterableDate(g.Key.InventoryID) ? g.Select<(SOPickerListEntry, SOPickListEntryToCartSplitLink), DateTime?>((Func<(SOPickerListEntry, SOPickListEntryToCartSplitLink), DateTime?>) (x => x.Entry.ExpireDate)).Where<DateTime?>((Func<DateTime?, bool>) (x => x.HasValue)).FirstOrDefault<DateTime?>() : new DateTime?()))).ToDictionary<((int?, int?, string, int?, string), Decimal, ReadOnlyCollection<SOPickListEntryToCartSplitLink>, DateTime?), (int?, int?, string, int?, string), (Decimal, ReadOnlyCollection<SOPickListEntryToCartSplitLink>, DateTime?)>((Func<((int?, int?, string, int?, string), Decimal, ReadOnlyCollection<SOPickListEntryToCartSplitLink>, DateTime?), (int?, int?, string, int?, string)>) (r => r.Key), (Func<((int?, int?, string, int?, string), Decimal, ReadOnlyCollection<SOPickListEntryToCartSplitLink>, DateTime?), (Decimal, ReadOnlyCollection<SOPickListEntryToCartSplitLink>, DateTime?)>) (r => (r.PickedQty, r.Links, r.ExpiredDate)));
    ((PXSelectBase<SOShipment>) shipmentEntry.Document).Current = PXResultset<SOShipment>.op_Implicit(((PXSelectBase<SOShipment>) shipmentEntry.Document).Search<SOShipment.shipmentNbr>((object) shipmentNbr, Array.Empty<object>()));
    PXSelectBase<SOShipLine> shLines = (PXSelectBase<SOShipLine>) shipmentEntry.Transactions;
    PXSelectBase<SOShipLineSplit> splits = (PXSelectBase<SOShipLineSplit>) shipmentEntry.splits;
    PXSelectBase<PX.Objects.SO.Unassigned.SOShipLineSplit> unassignedSplits = (PXSelectBase<PX.Objects.SO.Unassigned.SOShipLineSplit>) shipmentEntry.unassignedSplits;
    Func<int, bool> RequireShipping = Func.Memorize<int, bool>((Func<int, bool>) (inventoryID => PX.Objects.IN.InventoryItem.PK.Find((PXGraph) shipmentEntry, new int?(inventoryID)).With<PX.Objects.IN.InventoryItem, bool>((Func<PX.Objects.IN.InventoryItem, bool>) (item => item.StkItem.GetValueOrDefault() || item.NonStockShip.GetValueOrDefault()))));
    Decimal num1 = 0M;
    foreach (PXResult<SOShipLine> pxResult1 in shLines.Select(Array.Empty<object>()))
    {
      SOShipLine soShipLine = PXResult<SOShipLine>.op_Implicit(pxResult1);
      shLines.Current = PXResultset<SOShipLine>.op_Implicit(shLines.Search<SOShipLine.shipmentNbr, SOShipLine.lineNbr>((object) shipmentNbr, (object) soShipLine.LineNbr, Array.Empty<object>()));
      Decimal num2 = 0M;
      Decimal? nullable;
      foreach (PXResult<SOShipLineSplit> pxResult2 in splits.Select(Array.Empty<object>()))
      {
        SOShipLineSplit soShipLineSplit = PXResult<SOShipLineSplit>.op_Implicit(pxResult2);
        splits.Current = PXResultset<SOShipLineSplit>.op_Implicit(splits.Search<SOShipLineSplit.shipmentNbr, SOShipLineSplit.lineNbr, SOShipLineSplit.splitLineNbr>((object) shipmentNbr, (object) soShipLine.LineNbr, (object) soShipLineSplit.SplitLineNbr, Array.Empty<object>()));
        (int?, int?, string, int?, string) key = (splits.Current.InventoryID, splits.Current.SubItemID, shLines.Current.UOM, splits.Current.LocationID, splits.Current.LotSerialNbr);
        if (this.isEnterableOnIssue(splits.Current.InventoryID))
        {
          SOShipLineSplit protoSplit = splits.DeleteCurrent();
          num2 += CreateAssignedSplits(protoSplit);
        }
        else if (availability.ContainsKey(key))
        {
          Decimal val1 = availability[key].Item1;
          nullable = splits.Current.Qty;
          Decimal val2 = nullable.Value;
          Decimal num3 = Math.Min(val1, val2);
          nullable = splits.Current.PickedQty;
          Decimal num4 = num3;
          if (!(nullable.GetValueOrDefault() == num4 & nullable.HasValue))
          {
            splits.SetValueExt<SOShipLineSplit.pickedQty>(splits.Current, (object) num3);
            splits.UpdateCurrent();
          }
          num2 += num3;
          if (num3 > 0M)
          {
            availability[key] = (availability[key].Item1 - num3, availability[key].Item2, availability[key].Item3);
            shipmentEntry.CartSupportExt?.TransformCartLinks(splits.Current, (IReadOnlyCollection<SOPickListEntryToCartSplitLink>) availability[key].Item2);
          }
        }
      }
      foreach (PXResult<PX.Objects.SO.Unassigned.SOShipLineSplit> pxResult3 in unassignedSplits.Select(Array.Empty<object>()))
      {
        PX.Objects.SO.Unassigned.SOShipLineSplit source = PXResult<PX.Objects.SO.Unassigned.SOShipLineSplit>.op_Implicit(pxResult3);
        num2 += CreateAssignedSplits(PropertyTransfer.Transfer<PX.Objects.SO.Unassigned.SOShipLineSplit, SOShipLineSplit>(source, new SOShipLineSplit()));
      }
      if (stockKitSpecHelper.IsNonStockKit(soShipLine.InventoryID))
      {
        Dictionary<int, Decimal> dictionary1 = EnumerableExtensions.ToDictionary<int, Decimal>(stockKitSpecHelper.GetNonStockKitSpec(shLines.Current.InventoryID.Value).Where<KeyValuePair<int, Decimal>>((Func<KeyValuePair<int, Decimal>, bool>) (pair => pair.Value != 0M && RequireShipping(pair.Key))));
        Dictionary<int, Decimal> dictionary2 = ((IEnumerable<SOShipLineSplit>) splits.SelectMain(Array.Empty<object>())).GroupBy<SOShipLineSplit, int>((Func<SOShipLineSplit, int>) (r => r.InventoryID.Value)).ToDictionary<IGrouping<int, SOShipLineSplit>, int, Decimal>((Func<IGrouping<int, SOShipLineSplit>, int>) (g => g.Key), (Func<IGrouping<int, SOShipLineSplit>, Decimal>) (g => g.Sum<SOShipLineSplit>((Func<SOShipLineSplit, Decimal>) (s => s.PickedQty.GetValueOrDefault()))));
        Decimal num5 = dictionary1.Keys.Count<int>() == 0 || dictionary1.Keys.Except<int>((IEnumerable<int>) dictionary2.Keys).Count<int>() > 0 ? 0M : dictionary2.Join<KeyValuePair<int, Decimal>, KeyValuePair<int, Decimal>, int, Decimal>((IEnumerable<KeyValuePair<int, Decimal>>) dictionary1, (Func<KeyValuePair<int, Decimal>, int>) (split => split.Key), (Func<KeyValuePair<int, Decimal>, int>) (spec => spec.Key), (Func<KeyValuePair<int, Decimal>, KeyValuePair<int, Decimal>, Decimal>) ((split, spec) =>
        {
          KeyValuePair<int, Decimal> keyValuePair = split;
          Decimal d1 = keyValuePair.Value;
          keyValuePair = spec;
          Decimal d2 = keyValuePair.Value;
          return Math.Floor(Decimal.Divide(d1, d2));
        })).Min();
        num2 = INUnitAttribute.ConvertToBase(((PXSelectBase) shLines).Cache, shLines.Current.InventoryID, shLines.Current.UOM, num5, INPrecision.NOROUND);
      }
      nullable = shLines.Current.BasePickedQty;
      Decimal num6 = num2;
      if (!(nullable.GetValueOrDefault() == num6 & nullable.HasValue))
      {
        shLines.Current.BasePickedQty = new Decimal?(num2);
        shLines.Current.PickedQty = new Decimal?(INUnitAttribute.ConvertFromBase(((PXSelectBase) shLines).Cache, shLines.Current.InventoryID, shLines.Current.UOM, num2, INPrecision.NOROUND));
        GraphHelper.MarkUpdated(((PXSelectBase) shLines).Cache, (object) shLines.Current, true);
      }
      Decimal num7 = num1;
      nullable = shLines.Current.PickedQty;
      Decimal num8 = nullable.Value;
      num1 = num7 + num8;
    }
    if (num1 > 0M)
    {
      ((PXSelectBase<SOShipment>) shipmentEntry.Document).SetValueExt<SOShipment.picked>(((PXSelectBase<SOShipment>) shipmentEntry.Document).Current, (object) true);
      ((PXSelectBase<SOShipment>) shipmentEntry.Document).SetValueExt<SOShipment.pickedQty>(((PXSelectBase<SOShipment>) shipmentEntry.Document).Current, (object) num1);
      ((PXSelectBase<SOShipment>) shipmentEntry.Document).SetValueExt<SOShipment.pickedViaWorksheet>(((PXSelectBase<SOShipment>) shipmentEntry.Document).Current, (object) true);
      ((PXSelectBase<SOShipment>) shipmentEntry.Document).UpdateCurrent();
      if (((PXSelectBase<SOShipment>) shipmentEntry.Document).Current.Hold.GetValueOrDefault())
        ((PXAction) shipmentEntry.releaseFromHold).Press();
      else
        ((PXAction) shipmentEntry.Save).Press();
    }
    else
      ((PXAction) shipmentEntry.Save).Press();

    Decimal CreateAssignedSplits(SOShipLineSplit protoSplit)
    {
      (int?, int?, string, int?) usplitKey = (protoSplit.InventoryID, protoSplit.SubItemID, shLines.Current.UOM, protoSplit.LocationID);
      Decimal assignedSplits = 0M;
      Decimal val2 = protoSplit.Qty.Value;
      foreach (KeyValuePair<(int?, int?, string, int?, string), (Decimal, ReadOnlyCollection<SOPickListEntryToCartSplitLink>, DateTime?)> keyValuePair in availability.Where<KeyValuePair<(int?, int?, string, int?, string), (Decimal, ReadOnlyCollection<SOPickListEntryToCartSplitLink>, DateTime?)>>((Func<KeyValuePair<(int?, int?, string, int?, string), (Decimal, ReadOnlyCollection<SOPickListEntryToCartSplitLink>, DateTime?)>, bool>) (t => usplitKey.Equals((t.Key.InventoryID, t.Key.SubItemID, t.Key.OrderLineUOM, t.Key.LocationID)) && t.Value.PickedQty > 0M)).OrderByAccordanceTo<KeyValuePair<(int?, int?, string, int?, string), (Decimal, ReadOnlyCollection<SOPickListEntryToCartSplitLink>, DateTime?)>>((Func<KeyValuePair<(int?, int?, string, int?, string), (Decimal, ReadOnlyCollection<SOPickListEntryToCartSplitLink>, DateTime?)>, bool>) (t => t.Key.LotSerialNbr == protoSplit.LotSerialNbr)).ToArray<KeyValuePair<(int?, int?, string, int?, string), (Decimal, ReadOnlyCollection<SOPickListEntryToCartSplitLink>, DateTime?)>>())
      {
        if (!(val2 == 0M))
        {
          Decimal num = Math.Min(keyValuePair.Value.Item1, val2);
          if (num > 0M)
          {
            SOShipLineSplit soShipLineSplit = PropertyTransfer.Transfer<SOShipLineSplit, SOShipLineSplit>(protoSplit, new SOShipLineSplit());
            soShipLineSplit.SplitLineNbr = new int?();
            soShipLineSplit.LotSerialNbr = keyValuePair.Key.Item5;
            if (keyValuePair.Value.Item3.HasValue)
              soShipLineSplit.ExpireDate = keyValuePair.Value.Item3;
            soShipLineSplit.Qty = new Decimal?(num);
            soShipLineSplit.PickedQty = new Decimal?(num);
            soShipLineSplit.PackedQty = new Decimal?(0M);
            soShipLineSplit.IsUnassigned = new bool?(false);
            soShipLineSplit.HasGeneratedLotSerialNbr = new bool?(false);
            soShipLineSplit.PlanID = new long?();
            SOShipLineSplit shipSplit = ((PXSelectBase<SOShipLineSplit>) shipmentEntry.splits).Insert(soShipLineSplit);
            assignedSplits += num;
            availability[keyValuePair.Key] = (availability[keyValuePair.Key].Item1 - num, availability[keyValuePair.Key].Item2, availability[keyValuePair.Key].Item3);
            val2 -= num;
            shipmentEntry.CartSupportExt?.TransformCartLinks(shipSplit, (IReadOnlyCollection<SOPickListEntryToCartSplitLink>) availability[keyValuePair.Key].Item2);
          }
        }
        else
          break;
      }
      return assignedSplits;
    }
  }

  public virtual bool TryMarkWorksheetPicked(SOPickingWorksheet worksheet)
  {
    if (worksheet == null)
      throw new PXArgumentException(nameof (worksheet), "The argument cannot be null.");
    ((PXGraph) this.Base).Clear();
    ((PXSelectBase<SOPickingWorksheet>) this.Base.worksheet).Current = SOPickingWorksheet.PK.Find((PXGraph) this.Base, worksheet.WorksheetNbr, (PKFindOptions) 1).With<SOPickingWorksheet, SOPickingWorksheet>(new Func<SOPickingWorksheet, SOPickingWorksheet>(this.ValidateMutability));
    if (!((IEnumerable<SOPicker>) ((PXSelectBase<SOPicker>) this.Base.pickers).SelectMain(Array.Empty<object>())).All<SOPicker>((Func<SOPicker, bool>) (p => p.Confirmed.GetValueOrDefault())))
      return false;
    foreach (PXResult<SOPickingWorksheetLine> pxResult1 in ((PXSelectBase<SOPickingWorksheetLine>) this.Base.worksheetLines).Select(Array.Empty<object>()))
    {
      SOPickingWorksheetLine pickingWorksheetLine = PXResult<SOPickingWorksheetLine>.op_Implicit(pxResult1);
      Decimal? qty = pickingWorksheetLine.Qty;
      Decimal num1 = 0M;
      if (qty.GetValueOrDefault() == num1 & qty.HasValue)
      {
        ((PXSelectBase<SOPickingWorksheetLine>) this.Base.worksheetLines).Delete(pickingWorksheetLine);
      }
      else
      {
        ((PXSelectBase<SOPickingWorksheetLine>) this.Base.worksheetLines).Current = pickingWorksheetLine;
        HashSet<string> source = new HashSet<string>();
        foreach (PXResult<SOPickingWorksheetLineSplit> pxResult2 in ((PXSelectBase<SOPickingWorksheetLineSplit>) this.Base.worksheetLineSplits).Select(Array.Empty<object>()))
        {
          SOPickingWorksheetLineSplit worksheetLineSplit = PXResult<SOPickingWorksheetLineSplit>.op_Implicit(pxResult2);
          qty = worksheetLineSplit.Qty;
          Decimal num2 = 0M;
          if (qty.GetValueOrDefault() == num2 & qty.HasValue)
            ((PXSelectBase<SOPickingWorksheetLineSplit>) this.Base.worksheetLineSplits).Delete(worksheetLineSplit);
          else if (!string.IsNullOrEmpty(worksheetLineSplit.LotSerialNbr))
            source.Add(worksheetLineSplit.LotSerialNbr);
        }
        if (source.Count == 1)
        {
          string b = source.First<string>();
          if (b != null && !string.Equals(pickingWorksheetLine.LotSerialNbr, b, StringComparison.OrdinalIgnoreCase))
          {
            pickingWorksheetLine.LotSerialNbr = b;
            ((PXSelectBase<SOPickingWorksheetLine>) this.Base.worksheetLines).Update(pickingWorksheetLine);
            continue;
          }
        }
        if (pickingWorksheetLine.LotSerialNbr != null)
        {
          pickingWorksheetLine.LotSerialNbr = (string) null;
          ((PXSelectBase<SOPickingWorksheetLine>) this.Base.worksheetLines).Update(pickingWorksheetLine);
        }
      }
    }
    ((PXSelectBase<SOPickingWorksheet>) this.Base.worksheet).Current.Status = "P";
    ((PXSelectBase<SOPickingWorksheet>) this.Base.worksheet).UpdateCurrent();
    ((PXAction) this.Base.Save).Press();
    return true;
  }

  protected virtual SOPickingWorksheet ValidateMutability(SOPickingWorksheet worksheet)
  {
    return !EnumerableExtensions.IsIn<string>(worksheet.Status, "H", "C", "L") ? worksheet : throw new PXInvalidOperationException("The {0} picking worksheet cannot be modified because it has the {1} status.", new object[2]
    {
      (object) worksheet.WorksheetNbr,
      ((PXSelectBase) this.Base.worksheet).Cache.GetStateExt<SOPickingWorksheet.status>((object) worksheet)
    });
  }

  protected virtual SOPicker ValidateMutability(SOPicker pickList)
  {
    return !pickList.Confirmed.GetValueOrDefault() ? pickList : throw new PXInvalidOperationException("The {0} pick list is already confirmed.", new object[1]
    {
      (object) pickList.PickListNbr
    });
  }

  public virtual async System.Threading.Tasks.Task FulfillShipmentsAndConfirmWorksheet(
    SOPickingWorksheet worksheet,
    CancellationToken cancellationToken)
  {
    SOPickingWorksheetPickListConfirmation listConfirmation = this;
    PXReportRequiredException report = (PXReportRequiredException) null;
    using (PXTransactionScope ts = new PXTransactionScope())
    {
      IEnumerable<string> source = listConfirmation.TryFulfillShipments(worksheet);
      if (source.Any<string>() && worksheet.WorksheetType == "BT" && PXAccess.FeatureInstalled<FeaturesSet.deviceHub>())
      {
        foreach (string str in source)
        {
          Dictionary<string, string> dictionary = new Dictionary<string, string>()
          {
            ["WorksheetNbr"] = worksheet.WorksheetNbr,
            ["ShipmentNbr"] = str
          };
          if (report == null)
            report = new PXReportRequiredException(dictionary, "SO644005", (CurrentLocalization) null);
          else
            report.AddSibling("SO644005", dictionary);
        }
        if (report != null)
        {
          int num = await SMPrintJobMaint.CreatePrintJobGroup(new PrintSettings()
          {
            PrintWithDeviceHub = new bool?(true),
            DefinePrinterManually = new bool?(true),
            PrinterID = new NotificationUtility((PXGraph) listConfirmation.Base).SearchPrinter("Customer", "SO644005", ((PXGraph) listConfirmation.Base).Accessinfo.BranchID),
            NumberOfCopies = new int?(1)
          }, report, (string) null, cancellationToken) ? 1 : 0;
        }
      }
      listConfirmation.TryMarkWorksheetPicked(worksheet);
      ts.Complete();
    }
    report = report == null ? (PXReportRequiredException) null : throw report;
  }

  [PXHidden]
  [SOPickingWorksheetPickListConfirmation.SOPickingWorksheetLineDelta.Accumulator(BqlTable = typeof (SOPickingWorksheetLine))]
  public class SOPickingWorksheetLineDelta : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = "")]
    public virtual string WorksheetNbr { get; set; }

    [PXDBInt(IsKey = true)]
    public virtual int? LineNbr { get; set; }

    [PXDBDecimal(6)]
    public virtual Decimal? PickedQty { get; set; }

    [PXDBDecimal(6)]
    public virtual Decimal? BasePickedQty { get; set; }

    public abstract class worksheetNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      SOPickingWorksheetPickListConfirmation.SOPickingWorksheetLineDelta.worksheetNbr>
    {
    }

    public abstract class lineNbr : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      SOPickingWorksheetPickListConfirmation.SOPickingWorksheetLineDelta.lineNbr>
    {
    }

    public abstract class pickedQty : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      SOPickingWorksheetPickListConfirmation.SOPickingWorksheetLineDelta.pickedQty>
    {
    }

    public abstract class basePickedQty : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      SOPickingWorksheetPickListConfirmation.SOPickingWorksheetLineDelta.basePickedQty>
    {
    }

    public class AccumulatorAttribute : PXAccumulatorAttribute
    {
      public AccumulatorAttribute() => this._SingleRecord = true;

      protected virtual bool PrepareInsert(
        PXCache sender,
        object row,
        PXAccumulatorCollection columns)
      {
        if (!base.PrepareInsert(sender, row, columns))
          return false;
        SOPickingWorksheetPickListConfirmation.SOPickingWorksheetLineDelta worksheetLineDelta = (SOPickingWorksheetPickListConfirmation.SOPickingWorksheetLineDelta) row;
        columns.Update<SOPickingWorksheetPickListConfirmation.SOPickingWorksheetLineDelta.pickedQty>((object) worksheetLineDelta.PickedQty, (PXDataFieldAssign.AssignBehavior) 1);
        columns.Update<SOPickingWorksheetPickListConfirmation.SOPickingWorksheetLineDelta.basePickedQty>((object) worksheetLineDelta.BasePickedQty, (PXDataFieldAssign.AssignBehavior) 1);
        return true;
      }
    }
  }

  [PXHidden]
  [SOPickingWorksheetPickListConfirmation.SOPickingWorksheetLineSplitDelta.Accumulator(BqlTable = typeof (SOPickingWorksheetLineSplit))]
  public class SOPickingWorksheetLineSplitDelta : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = "")]
    public virtual string WorksheetNbr { get; set; }

    [PXDBInt(IsKey = true)]
    public virtual int? LineNbr { get; set; }

    [PXDBInt(IsKey = true)]
    public virtual int? SplitNbr { get; set; }

    [PXDBDecimal(6)]
    public virtual Decimal? Qty { get; set; }

    [PXDBDecimal(6)]
    public virtual Decimal? BaseQty { get; set; }

    [PXDBDecimal(6)]
    public virtual Decimal? PickedQty { get; set; }

    [PXDBDecimal(6)]
    public virtual Decimal? BasePickedQty { get; set; }

    [PXDBInt]
    public virtual int? SortingLocationID { get; set; }

    public abstract class worksheetNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      SOPickingWorksheetPickListConfirmation.SOPickingWorksheetLineSplitDelta.worksheetNbr>
    {
    }

    public abstract class lineNbr : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      SOPickingWorksheetPickListConfirmation.SOPickingWorksheetLineSplitDelta.lineNbr>
    {
    }

    public abstract class splitNbr : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      SOPickingWorksheetPickListConfirmation.SOPickingWorksheetLineSplitDelta.splitNbr>
    {
    }

    public abstract class qty : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      SOPickingWorksheetPickListConfirmation.SOPickingWorksheetLineSplitDelta.qty>
    {
    }

    public abstract class baseQty : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      SOPickingWorksheetPickListConfirmation.SOPickingWorksheetLineSplitDelta.baseQty>
    {
    }

    public abstract class pickedQty : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      SOPickingWorksheetPickListConfirmation.SOPickingWorksheetLineSplitDelta.pickedQty>
    {
    }

    public abstract class basePickedQty : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      SOPickingWorksheetPickListConfirmation.SOPickingWorksheetLineSplitDelta.basePickedQty>
    {
    }

    public abstract class sortingLocationID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      SOPickingWorksheetPickListConfirmation.SOPickingWorksheetLineSplitDelta.sortingLocationID>
    {
    }

    public class AccumulatorAttribute : PXAccumulatorAttribute
    {
      public AccumulatorAttribute() => this._SingleRecord = true;

      protected virtual bool PrepareInsert(
        PXCache sender,
        object row,
        PXAccumulatorCollection columns)
      {
        if (!base.PrepareInsert(sender, row, columns))
          return false;
        SOPickingWorksheetPickListConfirmation.SOPickingWorksheetLineSplitDelta worksheetLineSplitDelta = (SOPickingWorksheetPickListConfirmation.SOPickingWorksheetLineSplitDelta) row;
        columns.Update<SOPickingWorksheetPickListConfirmation.SOPickingWorksheetLineSplitDelta.qty>((object) worksheetLineSplitDelta.Qty, (PXDataFieldAssign.AssignBehavior) 1);
        columns.Update<SOPickingWorksheetPickListConfirmation.SOPickingWorksheetLineSplitDelta.baseQty>((object) worksheetLineSplitDelta.BaseQty, (PXDataFieldAssign.AssignBehavior) 1);
        columns.Update<SOPickingWorksheetPickListConfirmation.SOPickingWorksheetLineSplitDelta.pickedQty>((object) worksheetLineSplitDelta.PickedQty, (PXDataFieldAssign.AssignBehavior) 1);
        columns.Update<SOPickingWorksheetPickListConfirmation.SOPickingWorksheetLineSplitDelta.basePickedQty>((object) worksheetLineSplitDelta.BasePickedQty, (PXDataFieldAssign.AssignBehavior) 1);
        columns.Update<SOPickingWorksheetPickListConfirmation.SOPickingWorksheetLineSplitDelta.sortingLocationID>((object) worksheetLineSplitDelta.SortingLocationID, (PXDataFieldAssign.AssignBehavior) 4);
        return true;
      }
    }
  }

  public class ShipmentWorkLog : PX.Objects.SO.GraphExtensions.ShipmentWorkLog<SOPickingWorksheetReview>
  {
  }

  [PXLocalizable]
  public static class Msg
  {
    public const string PickListIsAlreadyConfirmed = "The {0} pick list is already confirmed.";
    public const string WorksheetCannotBeUpdatedInCurrentStatus = "The {0} picking worksheet cannot be modified because it has the {1} status.";
    [Obsolete]
    public const string PickListConfirmedButShipmentDoesNot = "The pick list has been confirmed but an error has occurred on the {0} shipment confirmation. Contact your manager.";
  }

  public class WorkflowChanges : 
    PXGraphExtension<SOPickingWorksheetReview.Workflow, SOPickingWorksheetReview>
  {
    public virtual void Configure(PXScreenConfiguration config)
    {
      SOPickingWorksheetPickListConfirmation.WorkflowChanges.Configure(config.GetScreenConfigurationContext<SOPickingWorksheetReview, SOPickingWorksheet>());
    }

    protected static void Configure(
      WorkflowContext<SOPickingWorksheetReview, SOPickingWorksheet> context)
    {
      context.UpdateScreenConfigurationFor((Func<BoundedTo<SOPickingWorksheetReview, SOPickingWorksheet>.ScreenConfiguration.ConfiguratorScreen, BoundedTo<SOPickingWorksheetReview, SOPickingWorksheet>.ScreenConfiguration.ConfiguratorScreen>) (screen => screen.WithActions((Action<BoundedTo<SOPickingWorksheetReview, SOPickingWorksheet>.ActionDefinition.ContainerAdjusterActions>) (actions => actions.Add<SOPickingWorksheetPickListConfirmation>((Expression<Func<SOPickingWorksheetPickListConfirmation, PXAction<SOPickingWorksheet>>>) (e => e.FulfillShipments), (Func<BoundedTo<SOPickingWorksheetReview, SOPickingWorksheet>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<SOPickingWorksheetReview, SOPickingWorksheet>.ActionDefinition.IConfigured>) (a => (BoundedTo<SOPickingWorksheetReview, SOPickingWorksheet>.ActionDefinition.IConfigured) a.WithCategory(CommonActionCategories.Get<SOPickingWorksheetReview, SOPickingWorksheet>(context).Processing).PlaceAfterInCategory((Expression<Func<SOPickingWorksheetReview, PXAction<SOPickingWorksheet>>>) (g => g.PrintPackSlips))))))));
    }
  }
}
