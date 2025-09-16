// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.Matrix.Graphs.MatrixItemsStatusInquiry
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.Common;
using PX.Objects.CS;
using PX.Objects.IN.Matrix.Attributes;
using PX.Objects.IN.Matrix.DAC.Unbound;
using PX.Objects.IN.Matrix.GraphExtensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.IN.Matrix.Graphs;

public class MatrixItemsStatusInquiry : PXGraph<MatrixItemsStatusInquiry, EntryHeader>
{
  public MatrixItemsStatusInquiry()
  {
    ((PXAction) this.Save).SetVisible(false);
    ((PXAction) this.Insert).SetVisible(false);
    ((PXAction) this.Delete).SetVisible(false);
    ((PXAction) this.CopyPaste).SetVisible(false);
    ((PXAction) this.Next).SetVisible(false);
    ((PXAction) this.Previous).SetVisible(false);
    ((PXAction) this.First).SetVisible(false);
    ((PXAction) this.Last).SetVisible(false);
    ((PXAction) this.Cancel).SetVisible(false);
  }

  public virtual bool CanClipboardCopyPaste() => false;

  public class MatrixItemsStatusInquiryImpl : MatrixGridExt<MatrixItemsStatusInquiry, EntryHeader>
  {
    public PXAction<EntryHeader> viewAllocationDetails;

    public override bool AddTotals => true;

    public override void Initialize()
    {
      base.Initialize();
      PXUIFieldAttribute.SetRequired<EntryHeader.templateItemID>(((PXSelectBase) this.Header).Cache, true);
      PXUIFieldAttribute.SetRequired<EntryHeader.colAttributeID>(((PXSelectBase) this.Header).Cache, true);
      PXUIFieldAttribute.SetRequired<EntryHeader.rowAttributeID>(((PXSelectBase) this.Header).Cache, true);
      PXUIFieldAttribute.SetVisible<EntryHeader.showAvailable>(((PXSelectBase) this.Header).Cache, (object) null, false);
    }

    [PXMergeAttributes]
    [PXRestrictor(typeof (Where<InventoryItem.stkItem, Equal<True>>), "The inventory item is not a stock item.", new Type[] {})]
    protected virtual void _(Events.CacheAttached<EntryHeader.templateItemID> e)
    {
    }

    protected virtual void _(
      Events.FieldUpdated<EntryHeader, EntryHeader.locationID> eventArgs)
    {
      this.RecalcMatrixGrid();
    }

    protected virtual void _(
      Events.FieldUpdated<EntryHeader, EntryHeader.displayPlanType> eventArgs)
    {
      this.RecalcMatrixGrid();
    }

    protected override void FillInventoryMatrixItem(
      EntryMatrix newRow,
      int colAttributeIndex,
      MatrixGridExt<MatrixItemsStatusInquiry, EntryHeader>.InventoryMapValue inventoryValue)
    {
      newRow.InventoryIDs[colAttributeIndex] = (int?) inventoryValue?.InventoryID;
      string displayPlanType = ((PXSelectBase<EntryHeader>) this.Header).Current.DisplayPlanType;
      if (displayPlanType != null && displayPlanType.Length == 2)
      {
        switch (displayPlanType[1])
        {
          case 'A':
            switch (displayPlanType)
            {
              case "AA":
                newRow.Quantities[colAttributeIndex] = new Decimal?(this.GetQty<INSiteStatus.qtyAvail>(inventoryValue));
                return;
              case "NA":
                newRow.Quantities[colAttributeIndex] = new Decimal?(this.GetQty<INSiteStatus.qtyNotAvail>(inventoryValue));
                return;
              case "IA":
                newRow.Quantities[colAttributeIndex] = new Decimal?(this.GetQty<INSiteStatus.qtyINAssemblyDemand>(inventoryValue));
                return;
              case "FA":
                newRow.Quantities[colAttributeIndex] = new Decimal?(this.GetQty<INSiteStatus.qtyFSSrvOrdAllocated>(inventoryValue));
                return;
              case "PA":
                newRow.Quantities[colAttributeIndex] = new Decimal?(this.GetQty<INSiteStatus.qtyProductionAllocated>(inventoryValue));
                return;
            }
            break;
          case 'B':
            switch (displayPlanType)
            {
              case "SB":
                newRow.Quantities[colAttributeIndex] = new Decimal?(this.GetQty<INSiteStatus.qtySOBooked>(inventoryValue));
                return;
              case "FB":
                newRow.Quantities[colAttributeIndex] = new Decimal?(this.GetQty<INSiteStatus.qtyFSSrvOrdBooked>(inventoryValue));
                return;
            }
            break;
          case 'C':
            if (displayPlanType == "PC")
            {
              newRow.Quantities[colAttributeIndex] = new Decimal?(this.GetQty<INSiteStatus.qtyPOFixedReceipts>(inventoryValue));
              return;
            }
            break;
          case 'D':
            switch (displayPlanType)
            {
              case "SD":
                newRow.Quantities[colAttributeIndex] = new Decimal?(this.GetQty<INSiteStatus.qtySOShipped>(inventoryValue));
                return;
              case "PD":
                newRow.Quantities[colAttributeIndex] = new Decimal?(this.GetQty<INSiteStatus.qtyProductionDemand>(inventoryValue));
                return;
            }
            break;
          case 'E':
            if (displayPlanType == "PE")
            {
              newRow.Quantities[colAttributeIndex] = new Decimal?(this.GetQty<INSiteStatus.qtySOFixedProduction>(inventoryValue));
              return;
            }
            break;
          case 'F':
            switch (displayPlanType)
            {
              case "FF":
                newRow.Quantities[colAttributeIndex] = new Decimal?(this.GetQty<INSiteStatus.qtyFixedFSSrvOrd>(inventoryValue));
                return;
              case "PF":
                newRow.Quantities[colAttributeIndex] = new Decimal?(this.GetQty<INSiteStatus.qtyPOFixedProductionPrepared>(inventoryValue));
                return;
            }
            break;
          case 'G':
            if (displayPlanType == "PG")
            {
              newRow.Quantities[colAttributeIndex] = new Decimal?(this.GetQty<INSiteStatus.qtyProdFixedSalesOrdersPrepared>(inventoryValue));
              return;
            }
            break;
          case 'H':
            switch (displayPlanType)
            {
              case "DH":
                newRow.Quantities[colAttributeIndex] = new Decimal?(this.GetQty<INSiteStatus.qtyPODropShipOrders>(inventoryValue));
                return;
              case "OH":
                newRow.Quantities[colAttributeIndex] = new Decimal?(this.GetQty<INSiteStatus.qtyOnHand>(inventoryValue));
                return;
              case "PH":
                newRow.Quantities[colAttributeIndex] = new Decimal?(this.GetQty<INSiteStatus.qtyProdFixedSalesOrders>(inventoryValue));
                return;
            }
            break;
          case 'I':
            switch (displayPlanType)
            {
              case "II":
                newRow.Quantities[colAttributeIndex] = new Decimal?(this.GetQty<INSiteStatus.qtyINIssues>(inventoryValue));
                return;
              case "DI":
                newRow.Quantities[colAttributeIndex] = new Decimal?(this.GetQty<INSiteStatus.qtyPODropShipPrepared>(inventoryValue));
                return;
            }
            break;
          case 'J':
            if (displayPlanType == "PJ")
            {
              newRow.Quantities[colAttributeIndex] = new Decimal?(this.GetQty<INSiteStatus.qtyProdFixedPurchase>(inventoryValue));
              return;
            }
            break;
          case 'L':
            if (displayPlanType == "PL")
            {
              newRow.Quantities[colAttributeIndex] = new Decimal?(this.GetQty<INSiteStatus.qtyProdFixedProdOrders>(inventoryValue));
              return;
            }
            break;
          case 'N':
            if (displayPlanType == "PN")
            {
              newRow.Quantities[colAttributeIndex] = new Decimal?(this.GetQty<INSiteStatus.qtyProdFixedProdOrdersPrepared>(inventoryValue));
              return;
            }
            break;
          case 'O':
            switch (displayPlanType)
            {
              case "SO":
                newRow.Quantities[colAttributeIndex] = new Decimal?(this.GetQty<INSiteStatus.qtySOBackOrdered>(inventoryValue));
                return;
              case "PO":
                newRow.Quantities[colAttributeIndex] = new Decimal?(this.GetQty<INSiteStatus.qtyPOOrders>(inventoryValue));
                return;
              case "FO":
                newRow.Quantities[colAttributeIndex] = new Decimal?(this.GetQty<INSiteStatus.qtyPOFixedFSSrvOrd>(inventoryValue));
                return;
            }
            break;
          case 'P':
            switch (displayPlanType)
            {
              case "PP":
                newRow.Quantities[colAttributeIndex] = new Decimal?(this.GetQty<INSiteStatus.qtyPOPrepared>(inventoryValue));
                return;
              case "SP":
                newRow.Quantities[colAttributeIndex] = new Decimal?(this.GetQty<INSiteStatus.qtySOFixed>(inventoryValue));
                return;
              case "DP":
                newRow.Quantities[colAttributeIndex] = new Decimal?(this.GetQty<INSiteStatus.qtyPODropShipReceipts>(inventoryValue));
                return;
              case "FP":
                newRow.Quantities[colAttributeIndex] = new Decimal?(this.GetQty<INSiteStatus.qtyFSSrvOrdPrepared>(inventoryValue));
                return;
            }
            break;
          case 'Q':
            if (displayPlanType == "PQ")
            {
              newRow.Quantities[colAttributeIndex] = new Decimal?(this.GetQty<INSiteStatus.qtyPOFixedProductionOrders>(inventoryValue));
              return;
            }
            break;
          case 'R':
            switch (displayPlanType)
            {
              case "IR":
                newRow.Quantities[colAttributeIndex] = new Decimal?(this.GetQty<INSiteStatus.qtyINReceipts>(inventoryValue));
                return;
              case "PR":
                newRow.Quantities[colAttributeIndex] = new Decimal?(this.GetQty<INSiteStatus.qtyPOReceipts>(inventoryValue));
                return;
              case "FR":
                newRow.Quantities[colAttributeIndex] = new Decimal?(this.GetQty<INSiteStatus.qtyPOFixedFSSrvOrdReceipts>(inventoryValue));
                return;
            }
            break;
          case 'S':
            switch (displayPlanType)
            {
              case "AS":
                newRow.Quantities[colAttributeIndex] = new Decimal?(this.GetQty<INSiteStatus.qtyHardAvail>(inventoryValue));
                return;
              case "SS":
                newRow.Quantities[colAttributeIndex] = new Decimal?(this.GetQty<INSiteStatus.qtySOShipping>(inventoryValue));
                return;
              case "PS":
                newRow.Quantities[colAttributeIndex] = new Decimal?(this.GetQty<INSiteStatus.qtyPOFixedOrders>(inventoryValue));
                return;
              case "DS":
                newRow.Quantities[colAttributeIndex] = new Decimal?(this.GetQty<INSiteStatus.qtySODropShip>(inventoryValue));
                return;
              case "IS":
                newRow.Quantities[colAttributeIndex] = new Decimal?(this.GetQty<INSiteStatus.qtyINAssemblySupply>(inventoryValue));
                return;
            }
            break;
          case 'T':
            switch (displayPlanType)
            {
              case "IT":
                newRow.Quantities[colAttributeIndex] = new Decimal?(this.GetQty<INSiteStatus.qtyInTransit>(inventoryValue));
                return;
              case "PT":
                newRow.Quantities[colAttributeIndex] = new Decimal?(this.GetQty<INSiteStatus.qtyInTransitToProduction>(inventoryValue));
                return;
            }
            break;
          case 'U':
            if (displayPlanType == "PU")
            {
              newRow.Quantities[colAttributeIndex] = new Decimal?(this.GetQty<INSiteStatus.qtyPOFixedPrepared>(inventoryValue));
              return;
            }
            break;
          case 'W':
            switch (displayPlanType)
            {
              case "FW":
                newRow.Quantities[colAttributeIndex] = new Decimal?(this.GetQty<INSiteStatus.qtyPOFixedFSSrvOrdPrepared>(inventoryValue));
                return;
              case "PW":
                newRow.Quantities[colAttributeIndex] = new Decimal?(this.GetQty<INSiteStatus.qtyProductionDemandPrepared>(inventoryValue));
                return;
            }
            break;
          case 'X':
            switch (displayPlanType)
            {
              case "EX":
                newRow.Quantities[colAttributeIndex] = new Decimal?(this.GetQty<INSiteStatus.qtyExpired>(inventoryValue));
                return;
              case "PX":
                newRow.Quantities[colAttributeIndex] = new Decimal?(this.GetQty<INSiteStatus.qtyProductionSupply>(inventoryValue));
                return;
            }
            break;
          case 'Y':
            if (displayPlanType == "PY")
            {
              newRow.Quantities[colAttributeIndex] = new Decimal?(this.GetQty<INSiteStatus.qtyProdFixedProduction>(inventoryValue));
              return;
            }
            break;
          case 'Z':
            if (displayPlanType == "PZ")
            {
              newRow.Quantities[colAttributeIndex] = new Decimal?(this.GetQty<INSiteStatus.qtyProductionSupplyPrepared>(inventoryValue));
              return;
            }
            break;
        }
      }
      throw new PXArgumentException("DisplayPlanType");
    }

    protected virtual Decimal GetQty<TField>(
      MatrixGridExt<MatrixItemsStatusInquiry, EntryHeader>.InventoryMapValue inventoryValue)
      where TField : IBqlField
    {
      INLocationStatus locationStatus = inventoryValue?.LocationStatus;
      if (locationStatus != null)
        return ((Decimal?) ((PXCache) GraphHelper.Caches<INLocationStatus>((PXGraph) this.Base)).GetValue<TField>((object) locationStatus)).GetValueOrDefault();
      INSiteStatus siteStatus = inventoryValue?.SiteStatus;
      return ((Decimal?) ((PXCache) GraphHelper.Caches<INSiteStatus>((PXGraph) this.Base)).GetValue<TField>((object) siteStatus)).GetValueOrDefault();
    }

    protected override void FieldSelectingImpl(
      int attributeNumber,
      PXCache s,
      PXFieldSelectingEventArgs e,
      string fieldName)
    {
      EntryMatrix row = (EntryMatrix) e.Row;
      int? valueFromArray1 = MatrixGridExt<MatrixItemsStatusInquiry, EntryHeader>.GetValueFromArray<int?>(row?.InventoryIDs, attributeNumber);
      Decimal? valueFromArray2 = MatrixGridExt<MatrixItemsStatusInquiry, EntryHeader>.GetValueFromArray<Decimal?>(row?.Quantities, attributeNumber);
      string valueFromArray3 = MatrixGridExt<MatrixItemsStatusInquiry, EntryHeader>.GetValueFromArray<string>(row?.Errors, attributeNumber);
      PXFieldState instance = PXDecimalState.CreateInstance(e.ReturnState, this._precision.Value, fieldName, new bool?(false), new int?(0), new Decimal?(0M), new Decimal?());
      instance.Enabled = false;
      instance.Error = valueFromArray3;
      instance.ErrorLevel = string.IsNullOrEmpty(valueFromArray3) ? (PXErrorLevel) 0 : (PXErrorLevel) 2;
      e.ReturnState = (object) instance;
      e.ReturnValue = (object) (valueFromArray1.HasValue || row != null && row.IsTotal.GetValueOrDefault() ? valueFromArray2 : new Decimal?());
      EntryMatrix entryMatrix = row ?? this.GetFirstMatrixRow();
      int num = attributeNumber;
      int? length = entryMatrix?.ColAttributeValueDescrs?.Length;
      int valueOrDefault = length.GetValueOrDefault();
      if (num < valueOrDefault & length.HasValue)
      {
        instance.DisplayName = entryMatrix.ColAttributeValueDescrs[attributeNumber] ?? entryMatrix.ColAttributeValues[attributeNumber];
        instance.Visibility = (PXUIVisibility) 3;
        instance.Visible = true;
      }
      else
      {
        instance.DisplayName = (string) null;
        instance.Visibility = (PXUIVisibility) 1;
        instance.Visible = false;
      }
    }

    protected override void FieldUpdatingImpl(
      int attributeNumber,
      PXCache s,
      PXFieldUpdatingEventArgs e,
      string fieldName)
    {
    }

    protected override EntryMatrix GenerateTotalRow(IEnumerable<EntryMatrix> rows)
    {
      bool flag = false;
      EntryMatrix instance = (EntryMatrix) ((PXSelectBase) this.Matrix).Cache.CreateInstance();
      foreach (EntryMatrix entryMatrix in ((PXSelectBase) this.Matrix).Cache.Cached)
      {
        flag = true;
        if (instance.Quantities == null)
          instance.Quantities = new Decimal?[entryMatrix.Quantities.Length];
        for (int index = 0; index < entryMatrix.Quantities.Length; ++index)
        {
          instance.Quantities[index] = new Decimal?(instance.Quantities[index].GetValueOrDefault());
          ref Decimal? local = ref instance.Quantities[index];
          Decimal? nullable = local;
          Decimal valueOrDefault = entryMatrix.Quantities[index].GetValueOrDefault();
          local = nullable.HasValue ? new Decimal?(nullable.GetValueOrDefault() + valueOrDefault) : new Decimal?();
        }
      }
      instance.RowAttributeValueDescr = PXLocalizer.Localize("Total Qty.");
      instance.IsTotal = new bool?(true);
      instance.LineNbr = new int?(int.MaxValue);
      return !flag ? (EntryMatrix) null : instance;
    }

    protected override void TotalFieldSelecting(
      PXCache s,
      PXFieldSelectingEventArgs e,
      string fieldName)
    {
      EntryMatrix row = (EntryMatrix) e.Row;
      PXFieldState instance = PXDecimalState.CreateInstance(e.ReturnState, this._precision.Value, fieldName, new bool?(false), new int?(0), new Decimal?(0M), new Decimal?());
      e.ReturnState = (object) instance;
      instance.Enabled = false;
      instance.DisplayName = PXLocalizer.Localize("Total Qty.");
      EntryMatrix entryMatrix = row ?? this.GetFirstMatrixRow();
      int? length;
      int num1;
      if (entryMatrix == null)
      {
        num1 = 0;
      }
      else
      {
        length = entryMatrix.ColAttributeValueDescrs?.Length;
        int num2 = 0;
        num1 = length.GetValueOrDefault() > num2 & length.HasValue ? 1 : 0;
      }
      if (num1 != 0)
      {
        instance.Visibility = (PXUIVisibility) 3;
        instance.Visible = true;
      }
      else
      {
        instance.Visibility = (PXUIVisibility) 1;
        instance.Visible = false;
      }
      Decimal num3 = 0M;
      int index = 0;
      while (true)
      {
        int num4 = index;
        length = row?.Quantities?.Length;
        int valueOrDefault = length.GetValueOrDefault();
        if (num4 < valueOrDefault & length.HasValue)
        {
          num3 += row.Quantities[index].GetValueOrDefault();
          ++index;
        }
        else
          break;
      }
      e.ReturnValue = (object) num3;
    }

    protected override List<PXResult<CSAnswers, InventoryItem>> SelectInventoryWithAttributes()
    {
      PXView pxView = new PXView((PXGraph) this.Base, true, BqlCommand.CreateInstance(new Type[1]
      {
        ((IBqlTemplate) BqlTemplate.OfCommand<Select5<CSAnswers, InnerJoin<InventoryItem, On<CSAnswers.refNoteID, Equal<InventoryItem.noteID>>, LeftJoin<CSAttributeGroup, On<CSAnswers.attributeID, Equal<CSAttributeGroup.attributeID>, And<CSAttributeGroup.entityClassID, Equal<InventoryItem.itemClassID>, And<CSAttributeGroup.entityType, Equal<Constants.DACName<InventoryItem>>, And<CSAttributeGroup.attributeCategory, Equal<CSAttributeGroup.attributeCategory.variant>>>>>, LeftJoin<INSiteStatus, On<INSiteStatus.inventoryID, Equal<InventoryItem.inventoryID>, And<INSiteStatus.subItemID, Equal<InventoryItem.defaultSubItemID>, And<Where<INSiteStatus.siteID, Equal<Current<EntryHeader.siteID>>, Or<Current<EntryHeader.siteID>, IsNull>>>>>, LeftJoin<INLocationStatus, On<INLocationStatus.inventoryID, Equal<InventoryItem.inventoryID>, And<INLocationStatus.subItemID, Equal<InventoryItem.defaultSubItemID>, And<INLocationStatus.siteID, Equal<Current<EntryHeader.siteID>>, And<INLocationStatus.locationID, Equal<Current<EntryHeader.locationID>>>>>>>>>>, Where<InventoryItem.templateItemID, Equal<Current<EntryHeader.templateItemID>>, And<Where<CSAnswers.attributeID, Equal<MatrixAttributeSelectorAttribute.dummyAttributeName>, Or<CSAttributeGroup.isActive, Equal<True>>>>>, Aggregate<GroupBy<InventoryItem.inventoryID, GroupBy<CSAnswers.refNoteID, GroupBy<CSAnswers.attributeID, BqlPlaceholder.A>>>>, OrderBy<Asc<InventoryItem.inventoryID, Asc<CSAnswers.refNoteID, Asc<CSAnswers.attributeID>>>>>>.Replace<BqlPlaceholder.A>(BqlCommand.Compose(BqlHelper.GetDecimalFieldsAggregate<INSiteStatus>((PXGraph) this.Base, true).ToArray<Type>()))).ToType()
      }));
      using (new PXFieldScope(pxView, new Type[5]
      {
        typeof (InventoryItem.inventoryID),
        typeof (CSAnswers.attributeID),
        typeof (CSAnswers.value),
        typeof (INSiteStatus),
        typeof (INLocationStatus)
      }))
        return pxView.SelectMulti(Array.Empty<object>()).Cast<PXResult<CSAnswers, InventoryItem>>().ToList<PXResult<CSAnswers, InventoryItem>>();
    }

    protected override MatrixGridExt<MatrixItemsStatusInquiry, EntryHeader>.InventoryMapValue CreateInventoryMapValue(
      int? inventoryID,
      PXResult<CSAnswers, InventoryItem> result)
    {
      MatrixGridExt<MatrixItemsStatusInquiry, EntryHeader>.InventoryMapValue inventoryMapValue = base.CreateInventoryMapValue(inventoryID, result);
      inventoryMapValue.SiteStatus = ((PXResult) result).GetItem<INSiteStatus>();
      inventoryMapValue.LocationStatus = ((PXSelectBase<EntryHeader>) this.Header).Current.LocationID.HasValue ? ((PXResult) result).GetItem<INLocationStatus>() : (INLocationStatus) null;
      return inventoryMapValue;
    }

    [PXUIField]
    [PXLookupButton]
    public virtual IEnumerable ViewAllocationDetails(PXAdapter adapter)
    {
      if (((PXSelectBase<EntryMatrix>) this.Matrix).Current != null)
      {
        int? selectedColumn = ((PXSelectBase<EntryMatrix>) this.Matrix).Current.SelectedColumn;
        int? nullable = ((PXSelectBase<EntryMatrix>) this.Matrix).Current.InventoryIDs?.Length;
        if (selectedColumn.GetValueOrDefault() < nullable.GetValueOrDefault() & selectedColumn.HasValue & nullable.HasValue)
        {
          MatrixItemsStatusInquiry graph = this.Base;
          int?[] inventoryIds = ((PXSelectBase<EntryMatrix>) this.Matrix).Current.InventoryIDs;
          nullable = ((PXSelectBase<EntryMatrix>) this.Matrix).Current.SelectedColumn;
          int index = nullable.Value;
          int? inventoryID = inventoryIds[index];
          InventoryItem inventoryItem = InventoryItem.PK.Find((PXGraph) graph, inventoryID);
          if (inventoryItem != null)
          {
            InventoryAllocDetEnq instance = PXGraph.CreateInstance<InventoryAllocDetEnq>();
            ((PXSelectBase<InventoryAllocDetEnqFilter>) instance.Filter).Current = new InventoryAllocDetEnqFilter()
            {
              InventoryID = inventoryItem.InventoryID,
              SiteID = ((PXSelectBase<EntryHeader>) this.Header).Current.SiteID,
              LocationID = ((PXSelectBase<EntryHeader>) this.Header).Current.LocationID
            };
            ((PXSelectBase<InventoryAllocDetEnqFilter>) instance.Filter).Select(Array.Empty<object>());
            PXRedirectHelper.TryRedirect((PXGraph) instance, (PXRedirectHelper.WindowMode) 3);
          }
        }
      }
      return adapter.Get();
    }
  }
}
