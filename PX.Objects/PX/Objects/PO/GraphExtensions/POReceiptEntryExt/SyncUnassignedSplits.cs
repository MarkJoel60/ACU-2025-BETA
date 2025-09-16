// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.GraphExtensions.POReceiptEntryExt.SyncUnassignedSplits
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.IN;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace PX.Objects.PO.GraphExtensions.POReceiptEntryExt;

public class SyncUnassignedSplits : PXGraphExtension<
#nullable disable
POReceiptEntry>
{
  public FbqlSelect<SelectFromBase<PX.Objects.PO.Unassigned.POReceiptLineSplit, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  PX.Objects.PO.Unassigned.POReceiptLineSplit.receiptType, 
  #nullable disable
  Equal<BqlField<
  #nullable enable
  PX.Objects.PO.POReceiptLine.receiptType, IBqlString>.FromCurrent>>>>, 
  #nullable disable
  And<BqlOperand<
  #nullable enable
  PX.Objects.PO.Unassigned.POReceiptLineSplit.receiptNbr, IBqlString>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  PX.Objects.PO.POReceiptLine.receiptNbr, IBqlString>.FromCurrent>>>>.And<
  #nullable disable
  BqlOperand<
  #nullable enable
  PX.Objects.PO.Unassigned.POReceiptLineSplit.lineNbr, IBqlInt>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  PX.Objects.PO.POReceiptLine.lineNbr, IBqlInt>.FromCurrent>>>, 
  #nullable disable
  PX.Objects.PO.Unassigned.POReceiptLineSplit>.View unassignedSplits;

  /// Overrides <see cref="M:PX.Objects.PO.POReceiptEntry.SyncUnassigned" />
  [PXOverride]
  public virtual void SyncUnassigned(Action baseImpl)
  {
    baseImpl();
    PXCache cache = ((PXSelectBase) this.Base.transactions).Cache;
    foreach (PX.Objects.PO.POReceiptLine line in NonGenericIEnumerableExtensions.Concat_(cache.Updated, cache.Inserted))
      this.SyncUnassigned(line);
  }

  protected virtual bool SupportsUnassignedSplits(PX.Objects.PO.POReceiptLine line)
  {
    if (!this.Base.LineSplittingExt.IsLSEntryEnabled(line))
      return false;
    INLotSerClass inLotSerClass = INLotSerClass.PK.Find((PXGraph) this.Base, PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this.Base, line.InventoryID)?.LotSerClassID);
    return inLotSerClass != null && EnumerableExtensions.IsIn<string>(inLotSerClass.LotSerTrack, "S", "L") && inLotSerClass.LotSerAssign == "R" && !inLotSerClass.AutoNextNbr.GetValueOrDefault();
  }

  protected virtual void SyncUnassigned(PX.Objects.PO.POReceiptLine line)
  {
    if (line.UnassignedQty.GetValueOrDefault() == 0M || !this.SupportsUnassignedSplits(line))
    {
      if (!line.IsUnassigned.GetValueOrDefault())
        return;
      this.DeleteUnassignedSplits(line, (IEnumerable<PX.Objects.PO.Unassigned.POReceiptLineSplit>) null);
      line.IsUnassigned = new bool?(false);
    }
    else
    {
      ((PXSelectBase<PX.Objects.PO.POReceiptLine>) this.Base.transactions).Current = line;
      PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this.Base, line.InventoryID);
      bool flag1 = INLotSerClass.PK.Find((PXGraph) this.Base, inventoryItem.LotSerClassID)?.LotSerTrack == "S";
      Decimal valueOrDefault = line.UnassignedQty.GetValueOrDefault();
      PXCache cache = ((PXSelectBase) this.unassignedSplits).Cache;
      Queue<PX.Objects.PO.Unassigned.POReceiptLineSplit> receiptLineSplitQueue = line.IsUnassigned.GetValueOrDefault() ? new Queue<PX.Objects.PO.Unassigned.POReceiptLineSplit>((IEnumerable<PX.Objects.PO.Unassigned.POReceiptLineSplit>) this.SelectUnassignedSplits(line)) : new Queue<PX.Objects.PO.Unassigned.POReceiptLineSplit>();
      line.IsUnassigned = new bool?(true);
      while (valueOrDefault > 0M)
      {
        Decimal num1 = flag1 ? 1M : valueOrDefault;
        if (receiptLineSplitQueue.Count > 0)
        {
          PX.Objects.PO.Unassigned.POReceiptLineSplit split = receiptLineSplitQueue.Dequeue();
          bool flag2 = this.AnyChange(split, line, inventoryItem.BaseUnit, new Decimal?(num1));
          this.InitUnassignedSplit(split, line, inventoryItem.BaseUnit, new Decimal?(num1));
          int? locationId = split.LocationID;
          if (!locationId.HasValue)
          {
            cache.SetDefaultExt<PX.Objects.PO.Unassigned.POReceiptLineSplit.locationID>((object) split);
            int num2 = flag2 ? 1 : 0;
            locationId = split.LocationID;
            int num3 = locationId.HasValue ? 1 : 0;
            flag2 = (num2 | num3) != 0;
          }
          if (flag2)
            cache.Update((object) split);
        }
        else
        {
          PX.Objects.PO.Unassigned.POReceiptLineSplit split = new PX.Objects.PO.Unassigned.POReceiptLineSplit()
          {
            ReceiptType = line.ReceiptType,
            ReceiptNbr = line.ReceiptNbr,
            LineNbr = line.LineNbr
          };
          this.InitUnassignedSplit(split, line, inventoryItem.BaseUnit, new Decimal?(num1));
          PXParentAttribute.SetParent(cache, (object) split, typeof (PX.Objects.PO.POReceiptLine), (object) line);
          cache.Insert((object) split);
        }
        valueOrDefault -= num1;
      }
      foreach (PX.Objects.PO.Unassigned.POReceiptLineSplit receiptLineSplit in receiptLineSplitQueue)
        cache.Delete((object) receiptLineSplit);
    }
  }

  protected virtual void DeleteUnassignedSplits(
    PX.Objects.PO.POReceiptLine line,
    IEnumerable<PX.Objects.PO.Unassigned.POReceiptLineSplit> unassignedSplitRows)
  {
    if (unassignedSplitRows == null)
      unassignedSplitRows = (IEnumerable<PX.Objects.PO.Unassigned.POReceiptLineSplit>) this.SelectUnassignedSplits(line);
    foreach (object unassignedSplitRow in unassignedSplitRows)
      ((PXSelectBase) this.unassignedSplits).Cache.Delete(unassignedSplitRow);
  }

  protected virtual List<PX.Objects.PO.Unassigned.POReceiptLineSplit> SelectUnassignedSplits(
    PX.Objects.PO.POReceiptLine line)
  {
    return GraphHelper.RowCast<PX.Objects.PO.Unassigned.POReceiptLineSplit>((IEnumerable) ((PXSelectBase) this.unassignedSplits).View.SelectMultiBound((object[]) new PX.Objects.PO.POReceiptLine[1]
    {
      line
    }, Array.Empty<object>())).ToList<PX.Objects.PO.Unassigned.POReceiptLineSplit>();
  }

  protected virtual void InitUnassignedSplit(
    PX.Objects.PO.Unassigned.POReceiptLineSplit split,
    PX.Objects.PO.POReceiptLine line,
    string uom,
    Decimal? qty)
  {
    split.LineType = line.LineType;
    split.InventoryID = line.InventoryID;
    split.SubItemID = line.SubItemID;
    split.SiteID = line.SiteID;
    split.LocationID = line.LocationID;
    split.LotSerialNbr = string.Empty;
    split.ExpireDate = line.ExpireDate;
    split.UOM = uom;
    split.Qty = qty;
    split.BaseQty = qty;
    split.InvtMult = line.InvtMult;
    split.OrigPlanType = line.OrigPlanType;
    split.ProjectID = line.ProjectID;
    split.TaskID = line.TaskID;
  }

  protected virtual bool AnyChange(
    PX.Objects.PO.Unassigned.POReceiptLineSplit split,
    PX.Objects.PO.POReceiptLine line,
    string uom,
    Decimal? qty)
  {
    int? nullable1;
    int? nullable2;
    int num;
    if (!(split.LineType != line.LineType))
    {
      nullable1 = split.InventoryID;
      nullable2 = line.InventoryID;
      if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
      {
        nullable2 = split.SubItemID;
        nullable1 = line.SubItemID;
        if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue)
        {
          nullable1 = split.SiteID;
          nullable2 = line.SiteID;
          if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
          {
            nullable2 = split.LocationID;
            nullable1 = line.LocationID;
            if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue && !(split.LotSerialNbr != string.Empty))
            {
              DateTime? expireDate1 = split.ExpireDate;
              DateTime? expireDate2 = line.ExpireDate;
              if ((expireDate1.HasValue == expireDate2.HasValue ? (expireDate1.HasValue ? (expireDate1.GetValueOrDefault() != expireDate2.GetValueOrDefault() ? 1 : 0) : 0) : 1) == 0 && !(split.UOM != uom))
              {
                Decimal? nullable3 = split.Qty;
                Decimal? nullable4 = qty;
                if (nullable3.GetValueOrDefault() == nullable4.GetValueOrDefault() & nullable3.HasValue == nullable4.HasValue)
                {
                  nullable4 = split.BaseQty;
                  nullable3 = qty;
                  if (nullable4.GetValueOrDefault() == nullable3.GetValueOrDefault() & nullable4.HasValue == nullable3.HasValue)
                  {
                    short? invtMult = split.InvtMult;
                    nullable1 = invtMult.HasValue ? new int?((int) invtMult.GetValueOrDefault()) : new int?();
                    invtMult = line.InvtMult;
                    nullable2 = invtMult.HasValue ? new int?((int) invtMult.GetValueOrDefault()) : new int?();
                    if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
                    {
                      num = split.OrigPlanType != line.OrigPlanType ? 1 : 0;
                      goto label_11;
                    }
                  }
                }
              }
            }
          }
        }
      }
    }
    num = 1;
label_11:
    PXCache<PX.Objects.PO.POReceiptLine> pxCache = GraphHelper.Caches<PX.Objects.PO.POReceiptLine>((PXGraph) this.Base);
    if (num == 0)
    {
      nullable2 = (int?) ((PXCache) pxCache).GetValueOriginal<PX.Objects.PO.POReceiptLine.projectID>((object) line);
      nullable1 = line.ProjectID;
      if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue)
      {
        nullable1 = (int?) ((PXCache) pxCache).GetValueOriginal<PX.Objects.PO.POReceiptLine.taskID>((object) line);
        nullable2 = line.TaskID;
        return !(nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue);
      }
    }
    return true;
  }
}
