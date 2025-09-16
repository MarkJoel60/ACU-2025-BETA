// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.GraphExtensions.SOShipmentEntryExt.RecreateUnassignedSplitsExtension
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.IN;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.SO.GraphExtensions.SOShipmentEntryExt;

public class RecreateUnassignedSplitsExtension : 
  PXGraphExtension<CreateShipmentExtension, SOShipmentEntry>
{
  /// Overrides <see cref="M:PX.Data.PXGraph.Persist" />
  [PXOverride]
  public virtual void Persist(Action base_Persist)
  {
    foreach (SOShipLine line in NonGenericIEnumerableExtensions.Concat_(NonGenericIEnumerableExtensions.Concat_(((PXSelectBase) ((PXGraphExtension<SOShipmentEntry>) this).Base.Transactions).Cache.Deleted, ((PXSelectBase) ((PXGraphExtension<SOShipmentEntry>) this).Base.Transactions).Cache.Updated), ((PXSelectBase) ((PXGraphExtension<SOShipmentEntry>) this).Base.Transactions).Cache.Inserted))
      this.SyncUnassigned(line);
    base_Persist();
  }

  public virtual void SyncUnassigned(SOShipLine line)
  {
    if (!line.IsUnassigned.GetValueOrDefault())
    {
      Decimal? unassignedQty = line.UnassignedQty;
      Decimal num = 0M;
      if (unassignedQty.GetValueOrDefault() == num & unassignedQty.HasValue)
        return;
    }
    if (line.Operation != "I")
      return;
    PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) ((PXGraphExtension<SOShipmentEntry>) this).Base, new int?(line.InventoryID.Value));
    INLotSerClass lotSerClass = (INLotSerClass) null;
    if (inventoryItem != null && inventoryItem.StkItem.GetValueOrDefault())
      lotSerClass = INLotSerClass.PK.Find((PXGraph) ((PXGraphExtension<SOShipmentEntry>) this).Base, inventoryItem.LotSerClassID);
    if (lotSerClass == null || !lotSerClass.IsManualAssignRequired.GetValueOrDefault())
      return;
    bool flag1 = false;
    bool flag2 = false;
    int? nullable1 = new int?();
    Decimal? nullable2 = new Decimal?();
    List<PXResult<PX.Objects.SO.Unassigned.SOShipLineSplit>> pxResultList = (List<PXResult<PX.Objects.SO.Unassigned.SOShipLineSplit>>) null;
    PXCache cache = ((PXSelectBase) ((PXGraphExtension<SOShipmentEntry>) this).Base.Transactions).Cache;
    int? locationId;
    Decimal? nullable3;
    if (cache.GetStatus((object) line) != 3)
    {
      Decimal? unassignedQty1 = line.UnassignedQty;
      Decimal num1 = 0M;
      if (!(unassignedQty1.GetValueOrDefault() == num1 & unassignedQty1.HasValue))
      {
        if (GraphHelper.RowCast<PX.Objects.SO.Unassigned.SOShipLineSplit>(((PXSelectBase) ((PXGraphExtension<SOShipmentEntry>) this).Base.unassignedSplits).Cache.Updated).Any<PX.Objects.SO.Unassigned.SOShipLineSplit>((Func<PX.Objects.SO.Unassigned.SOShipLineSplit, bool>) (s =>
        {
          int? lineNbr1 = s.LineNbr;
          int? lineNbr2 = line.LineNbr;
          return lineNbr1.GetValueOrDefault() == lineNbr2.GetValueOrDefault() & lineNbr1.HasValue == lineNbr2.HasValue;
        })) || GraphHelper.RowCast<PX.Objects.SO.Unassigned.SOShipLineSplit>(((PXSelectBase) ((PXGraphExtension<SOShipmentEntry>) this).Base.unassignedSplits).Cache.Deleted).Any<PX.Objects.SO.Unassigned.SOShipLineSplit>((Func<PX.Objects.SO.Unassigned.SOShipLineSplit, bool>) (s =>
        {
          int? lineNbr3 = s.LineNbr;
          int? lineNbr4 = line.LineNbr;
          return lineNbr3.GetValueOrDefault() == lineNbr4.GetValueOrDefault() & lineNbr3.HasValue == lineNbr4.HasValue;
        })))
        {
          flag2 = true;
          goto label_34;
        }
        if (GraphHelper.RowCast<PX.Objects.SO.SOShipLineSplit>(((PXSelectBase) ((PXGraphExtension<SOShipmentEntry>) this).Base.splits).Cache.Deleted).Any<PX.Objects.SO.SOShipLineSplit>((Func<PX.Objects.SO.SOShipLineSplit, bool>) (s =>
        {
          int? lineNbr7 = s.LineNbr;
          int? lineNbr8 = line.LineNbr;
          return lineNbr7.GetValueOrDefault() == lineNbr8.GetValueOrDefault() & lineNbr7.HasValue == lineNbr8.HasValue;
        })) || GraphHelper.RowCast<PX.Objects.SO.SOShipLineSplit>(((PXSelectBase) ((PXGraphExtension<SOShipmentEntry>) this).Base.splits).Cache.Updated).Any<PX.Objects.SO.SOShipLineSplit>((Func<PX.Objects.SO.SOShipLineSplit, bool>) (s =>
        {
          int? lineNbr9 = s.LineNbr;
          int? lineNbr10 = line.LineNbr;
          return lineNbr9.GetValueOrDefault() == lineNbr10.GetValueOrDefault() & lineNbr9.HasValue == lineNbr10.HasValue && !((PXSelectBase) ((PXGraphExtension<SOShipmentEntry>) this).Base.splits).Cache.ObjectsEqual<PX.Objects.SO.SOShipLineSplit.locationID, PX.Objects.SO.SOShipLineSplit.lotSerialNbr, PX.Objects.SO.SOShipLineSplit.qty>((object) s, (object) (((PXSelectBase) ((PXGraphExtension<SOShipmentEntry>) this).Base.splits).Cache.GetOriginal((object) s) as PX.Objects.SO.SOShipLineSplit));
        })))
        {
          if (((PXGraphExtension<SOShipmentEntry>) this).Base.IsPPS && lotSerClass?.LotSerTrack == "S")
          {
            nullable2 = GraphHelper.RowCast<PX.Objects.SO.SOShipLineSplit>(((PXSelectBase) ((PXGraphExtension<SOShipmentEntry>) this).Base.splits).Cache.Deleted).Sum<PX.Objects.SO.SOShipLineSplit>((Func<PX.Objects.SO.SOShipLineSplit, Decimal?>) (s => s.BaseQty));
            Decimal? nullable4 = nullable2;
            Decimal num2 = 0M;
            if (nullable4.GetValueOrDefault() == num2 & nullable4.HasValue)
              nullable2 = new Decimal?();
            IEnumerable<int?> source = GraphHelper.RowCast<PX.Objects.SO.SOShipLineSplit>(((PXSelectBase) ((PXGraphExtension<SOShipmentEntry>) this).Base.splits).Cache.Deleted).Where<PX.Objects.SO.SOShipLineSplit>((Func<PX.Objects.SO.SOShipLineSplit, bool>) (s =>
            {
              int? lineNbr5 = s.LineNbr;
              int? lineNbr6 = line.LineNbr;
              return lineNbr5.GetValueOrDefault() == lineNbr6.GetValueOrDefault() & lineNbr5.HasValue == lineNbr6.HasValue;
            })).Select<PX.Objects.SO.SOShipLineSplit, int?>((Func<PX.Objects.SO.SOShipLineSplit, int?>) (s => s.LocationID)).Distinct<int?>();
            if (source.Count<int?>() == 1)
              nullable1 = source.First<int?>();
          }
          flag2 = true;
          goto label_34;
        }
        if (!object.Equals((object) line.LocationID, cache.GetValueOriginal<SOShipLine.locationID>((object) line)))
        {
          locationId = line.LocationID;
          if (locationId.HasValue)
          {
            flag2 = true;
            goto label_34;
          }
        }
        List<PX.Objects.SO.SOShipLineSplit> list = GraphHelper.RowCast<PX.Objects.SO.SOShipLineSplit>(((PXSelectBase) ((PXGraphExtension<SOShipmentEntry>) this).Base.splits).Cache.Inserted).ToList<PX.Objects.SO.SOShipLineSplit>();
        Decimal? nullable5 = new Decimal?(list.Sum<PX.Objects.SO.SOShipLineSplit>((Func<PX.Objects.SO.SOShipLineSplit, Decimal>) (s => s.BaseQty.GetValueOrDefault())));
        pxResultList = ((IEnumerable<PXResult<PX.Objects.SO.Unassigned.SOShipLineSplit>>) PXSelectBase<PX.Objects.SO.Unassigned.SOShipLineSplit, PXSelectJoin<PX.Objects.SO.Unassigned.SOShipLineSplit, LeftJoin<INLocation, On<INLocation.locationID, Equal<PX.Objects.SO.Unassigned.SOShipLineSplit.locationID>>>, Where<PX.Objects.SO.Unassigned.SOShipLineSplit.shipmentNbr, Equal<Required<PX.Objects.SO.Unassigned.SOShipLineSplit.shipmentNbr>>, And<PX.Objects.SO.Unassigned.SOShipLineSplit.lineNbr, Equal<Required<PX.Objects.SO.Unassigned.SOShipLineSplit.lineNbr>>>>, OrderBy<Asc<INLocation.pickPriority>>>.Config>.Select((PXGraph) ((PXGraphExtension<SOShipmentEntry>) this).Base, new object[2]
        {
          (object) line.ShipmentNbr,
          (object) line.LineNbr
        })).ToList<PXResult<PX.Objects.SO.Unassigned.SOShipLineSplit>>();
        Decimal? nullable6 = pxResultList.Sum<PXResult<PX.Objects.SO.Unassigned.SOShipLineSplit>>((Func<PXResult<PX.Objects.SO.Unassigned.SOShipLineSplit>, Decimal?>) (r => PXResult<PX.Objects.SO.Unassigned.SOShipLineSplit>.op_Implicit(r).BaseQty));
        Decimal? unassignedQty2 = line.UnassignedQty;
        Decimal? nullable7 = nullable6.HasValue & unassignedQty2.HasValue ? new Decimal?(nullable6.GetValueOrDefault() - unassignedQty2.GetValueOrDefault()) : new Decimal?();
        Decimal? nullable8 = nullable5;
        nullable3 = nullable7;
        if (nullable8.GetValueOrDefault() <= nullable3.GetValueOrDefault() & nullable8.HasValue & nullable3.HasValue)
        {
          List<int> locations = new List<int>();
          Dictionary<int, Decimal?> locationsAssignedQty = new Dictionary<int, Decimal?>();
          foreach (PX.Objects.SO.SOShipLineSplit soShipLineSplit in list)
          {
            locationId = soShipLineSplit.LocationID;
            int key1 = locationId ?? -1;
            if (!locationsAssignedQty.ContainsKey(key1))
            {
              locations.Add(key1);
              locationsAssignedQty.Add(key1, new Decimal?(0M));
            }
            Dictionary<int, Decimal?> dictionary1 = locationsAssignedQty;
            int key2 = key1;
            Dictionary<int, Decimal?> dictionary2 = dictionary1;
            int key3 = key2;
            nullable3 = dictionary1[key2];
            Decimal? baseQty = soShipLineSplit.BaseQty;
            Decimal? nullable9 = nullable3.HasValue & baseQty.HasValue ? new Decimal?(nullable3.GetValueOrDefault() + baseQty.GetValueOrDefault()) : new Decimal?();
            dictionary2[key3] = nullable9;
          }
          locations.Add(int.MinValue);
          Dictionary<int, Decimal?> dictionary = locationsAssignedQty;
          Decimal? nullable10 = nullable7;
          nullable3 = nullable5;
          Decimal? nullable11 = nullable10.HasValue & nullable3.HasValue ? new Decimal?(nullable10.GetValueOrDefault() - nullable3.GetValueOrDefault()) : new Decimal?();
          dictionary[int.MinValue] = nullable11;
          this.ApplyAssignedQty(locations, locationsAssignedQty, pxResultList, true);
          this.ApplyAssignedQty(locations, locationsAssignedQty, pxResultList, false);
          goto label_34;
        }
        flag2 = true;
        goto label_34;
      }
    }
    flag1 = true;
label_34:
    if (flag1 || flag2 && !nullable2.HasValue)
      this.DeleteUnassignedSplits(line, (IEnumerable<PXResult<PX.Objects.SO.Unassigned.SOShipLineSplit>>) pxResultList);
    SOShipLine soShipLine = line;
    nullable3 = line.UnassignedQty;
    Decimal num3 = 0M;
    bool? nullable12 = new bool?(!(nullable3.GetValueOrDefault() == num3 & nullable3.HasValue));
    soShipLine.IsUnassigned = nullable12;
    if (!flag2 || !line.IsUnassigned.GetValueOrDefault())
      return;
    Decimal? nullable13 = new Decimal?();
    CreateShipmentExtension base1 = this.Base1;
    locationId = line.LocationID;
    int? locationID = locationId ?? nullable1;
    Decimal? quantity = nullable2;
    using (new CreateShipmentExtension.SyncUnassignedScope(base1, locationID, quantity))
      nullable13 = this.RecreateUnassignedSplits(line, lotSerClass);
    Decimal? nullable14 = nullable13;
    Decimal num4 = 0M;
    if (nullable14.GetValueOrDefault() > num4 & nullable14.HasValue && line.LocationID.HasValue)
    {
      object[] objArray = new object[4]
      {
        cache.GetStateExt<SOShipLine.inventoryID>((object) line),
        cache.GetStateExt<SOShipLine.subItemID>((object) line),
        cache.GetStateExt<SOShipLine.siteID>((object) line),
        cache.GetStateExt<SOShipLine.locationID>((object) line)
      };
      cache.RaiseExceptionHandling<SOShipLine.locationID>((object) line, cache.GetStateExt<SOShipLine.locationID>((object) line), (Exception) new PXSetPropertyException((IBqlTable) line, "Updating data for item '{0} {1}' on warehouse '{2} {3}' will result in negative available quantity.", (PXErrorLevel) 4, objArray));
      throw new PXException("Updating data for item '{0} {1}' on warehouse '{2} {3}' will result in negative available quantity.", objArray);
    }
  }

  private void ApplyAssignedQty(
    List<int> locations,
    Dictionary<int, Decimal?> locationsAssignedQty,
    List<PXResult<PX.Objects.SO.Unassigned.SOShipLineSplit>> unassignedSplitRows,
    bool onlyCoincidentLocation)
  {
    foreach (int location in locations)
    {
      int locationID = location;
      Decimal? nullable1 = locationsAssignedQty[locationID];
      while (true)
      {
        Decimal? nullable2 = nullable1;
        Decimal num1 = 0M;
        if (nullable2.GetValueOrDefault() > num1 & nullable2.HasValue && unassignedSplitRows.Count > 0)
        {
          IEnumerable<int> source = EnumerableExtensions.SelectIndexesWhere<PXResult<PX.Objects.SO.Unassigned.SOShipLineSplit>>((IEnumerable<PXResult<PX.Objects.SO.Unassigned.SOShipLineSplit>>) unassignedSplitRows, (Func<PXResult<PX.Objects.SO.Unassigned.SOShipLineSplit>, bool>) (r =>
          {
            int? locationId = PXResult<PX.Objects.SO.Unassigned.SOShipLineSplit>.op_Implicit(r).LocationID;
            int num2 = locationID;
            return locationId.GetValueOrDefault() == num2 & locationId.HasValue;
          }));
          int? nullable3 = source.Any<int>() ? new int?(source.First<int>()) : (!onlyCoincidentLocation ? new int?(unassignedSplitRows.Count - 1) : new int?());
          if (nullable3.HasValue)
          {
            PX.Objects.SO.Unassigned.SOShipLineSplit soShipLineSplit1 = PXResult<PX.Objects.SO.Unassigned.SOShipLineSplit>.op_Implicit(unassignedSplitRows[nullable3.Value]);
            Decimal? nullable4 = nullable1;
            nullable2 = soShipLineSplit1.BaseQty;
            if (nullable4.GetValueOrDefault() >= nullable2.GetValueOrDefault() & nullable4.HasValue & nullable2.HasValue)
            {
              nullable2 = nullable1;
              nullable4 = soShipLineSplit1.BaseQty;
              nullable1 = nullable2.HasValue & nullable4.HasValue ? new Decimal?(nullable2.GetValueOrDefault() - nullable4.GetValueOrDefault()) : new Decimal?();
              ((PXSelectBase<PX.Objects.SO.Unassigned.SOShipLineSplit>) ((PXGraphExtension<SOShipmentEntry>) this).Base.unassignedSplits).Delete(soShipLineSplit1);
              unassignedSplitRows.RemoveAt(nullable3.Value);
            }
            else
            {
              PX.Objects.SO.Unassigned.SOShipLineSplit soShipLineSplit2 = soShipLineSplit1;
              nullable4 = soShipLineSplit2.BaseQty;
              nullable2 = nullable1;
              soShipLineSplit2.BaseQty = nullable4.HasValue & nullable2.HasValue ? new Decimal?(nullable4.GetValueOrDefault() - nullable2.GetValueOrDefault()) : new Decimal?();
              PX.Objects.SO.Unassigned.SOShipLineSplit soShipLineSplit3 = soShipLineSplit1;
              PXCache cache = ((PXSelectBase) ((PXGraphExtension<SOShipmentEntry>) this).Base.unassignedSplits).Cache;
              int? inventoryId = soShipLineSplit1.InventoryID;
              string uom = soShipLineSplit1.UOM;
              nullable2 = soShipLineSplit1.BaseQty;
              Decimal num3 = nullable2.Value;
              Decimal? nullable5 = new Decimal?(INUnitAttribute.ConvertFromBase(cache, inventoryId, uom, num3, INPrecision.QUANTITY));
              soShipLineSplit3.Qty = nullable5;
              nullable1 = new Decimal?(0M);
              ((PXSelectBase<PX.Objects.SO.Unassigned.SOShipLineSplit>) ((PXGraphExtension<SOShipmentEntry>) this).Base.unassignedSplits).Update(soShipLineSplit1);
            }
          }
          else
            break;
        }
        else
          break;
      }
      locationsAssignedQty[locationID] = nullable1;
    }
  }

  public virtual void DeleteUnassignedSplits(
    SOShipLine line,
    IEnumerable<PXResult<PX.Objects.SO.Unassigned.SOShipLineSplit>> unassignedSplitRows)
  {
    if (unassignedSplitRows == null)
      unassignedSplitRows = ((IEnumerable<PXResult<PX.Objects.SO.Unassigned.SOShipLineSplit>>) PXSelectBase<PX.Objects.SO.Unassigned.SOShipLineSplit, PXSelect<PX.Objects.SO.Unassigned.SOShipLineSplit, Where<PX.Objects.SO.Unassigned.SOShipLineSplit.shipmentNbr, Equal<Required<PX.Objects.SO.Unassigned.SOShipLineSplit.shipmentNbr>>, And<PX.Objects.SO.Unassigned.SOShipLineSplit.lineNbr, Equal<Required<PX.Objects.SO.Unassigned.SOShipLineSplit.lineNbr>>>>>.Config>.Select((PXGraph) ((PXGraphExtension<SOShipmentEntry>) this).Base, new object[2]
      {
        (object) line.ShipmentNbr,
        (object) line.LineNbr
      })).AsEnumerable<PXResult<PX.Objects.SO.Unassigned.SOShipLineSplit>>();
    foreach (PXResult<PX.Objects.SO.Unassigned.SOShipLineSplit> unassignedSplitRow in unassignedSplitRows)
      ((PXSelectBase) ((PXGraphExtension<SOShipmentEntry>) this).Base.unassignedSplits).Cache.Delete((object) PXResult<PX.Objects.SO.Unassigned.SOShipLineSplit>.op_Implicit(unassignedSplitRow));
  }

  public virtual Decimal? RecreateUnassignedSplits(SOShipLine line, INLotSerClass lotSerClass)
  {
    ((PXSelectBase<SOShipLine>) ((PXGraphExtension<SOShipmentEntry>) this).Base.Transactions).Current = line;
    if (lotSerClass.LotSerAssign == "R")
    {
      PX.Objects.SO.SOLineSplit soLineSplit = PXResultset<PX.Objects.SO.SOLineSplit>.op_Implicit(PXSelectBase<PX.Objects.SO.SOLineSplit, PXSelectReadonly<PX.Objects.SO.SOLineSplit, Where<PX.Objects.SO.SOLineSplit.orderType, Equal<Required<PX.Objects.SO.SOLineSplit.orderType>>, And<PX.Objects.SO.SOLineSplit.orderNbr, Equal<Required<PX.Objects.SO.SOLineSplit.orderNbr>>, And<PX.Objects.SO.SOLineSplit.lineNbr, Equal<Required<PX.Objects.SO.SOLineSplit.lineNbr>>, And<PX.Objects.SO.SOLineSplit.splitLineNbr, Equal<Required<PX.Objects.SO.SOLineSplit.splitLineNbr>>>>>>>.Config>.Select((PXGraph) ((PXGraphExtension<SOShipmentEntry>) this).Base, new object[4]
      {
        (object) line.OrigOrderType,
        (object) line.OrigOrderNbr,
        (object) line.OrigLineNbr,
        (object) line.OrigSplitLineNbr
      }));
      if (!string.IsNullOrEmpty(soLineSplit?.LotSerialNbr))
        return this.Base1.CreateSplitsForAvailableLots(this.Base1.QuantityToCreate ?? line.UnassignedQty, line.OrigPlanType, soLineSplit?.LotSerialNbr, line, lotSerClass);
    }
    return this.Base1.CreateSplitsForAvailableNonLots(this.Base1.QuantityToCreate ?? line.UnassignedQty, line.OrigPlanType, line, lotSerClass);
  }
}
