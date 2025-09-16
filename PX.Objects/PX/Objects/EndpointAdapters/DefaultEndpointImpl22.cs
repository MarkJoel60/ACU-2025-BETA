// Decompiled with JetBrains decompiler
// Type: PX.Objects.EndpointAdapters.DefaultEndpointImpl22
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Api;
using PX.Api.ContractBased;
using PX.Api.ContractBased.Models;
using PX.Common;
using PX.Data;
using PX.Objects.IN;
using PX.Objects.SO;
using PX.Objects.SO.DAC.Projections;
using PX.Objects.SO.DAC.Unbound;
using PX.Objects.SO.GraphExtensions.SOInvoiceEntryExt;
using PX.Objects.SO.GraphExtensions.SOOrderEntryExt;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.EndpointAdapters;

[PXInternalUseOnly]
[PXVersion("22.200.001", "Default")]
public class DefaultEndpointImpl22 : DefaultEndpointImpl20
{
  [FieldsProcessed(new string[] {})]
  protected virtual void InventoryIssueDetail_Insert(
    PXGraph graph,
    EntityImpl entity,
    EntityImpl targetEntity)
  {
    INIssueEntry inIssueEntry = (INIssueEntry) graph;
    if (((PXSelectBase<PX.Objects.IN.INRegister>) inIssueEntry.issue).Current == null)
      return;
    PXCache cache = ((PXSelectBase) inIssueEntry.transactions).Cache;
    cache.Current = cache.Insert();
    INTran current = cache.Current as INTran;
    if (cache.Current == null)
      throw new InvalidOperationException("Cannot insert Inventory Issue detail.");
    if (!((IEnumerable<EntityImpl>) ((((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => string.Equals(f.Name, "Allocations"))) is EntityListField entityListField ? entityListField.Value : (EntityImpl[]) null) ?? new EntityImpl[0])).Any<EntityImpl>((Func<EntityImpl, bool>) (a => a.Fields != null && a.Fields.Length != 0)))
      return;
    EntityValueField entityValueField1 = ((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "InventoryID")) as EntityValueField;
    EntityValueField entityValueField2 = ((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "WarehouseID")) as EntityValueField;
    EntityValueField entityValueField3 = ((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "Location")) as EntityValueField;
    EntityValueField entityValueField4 = ((IEnumerable<EntityField>) targetEntity.Fields).FirstOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "Subitem")) as EntityValueField;
    if (entityValueField1 != null)
      ((PXSelectBase) inIssueEntry.transactions).Cache.SetValueExt((object) current, "InventoryID", (object) entityValueField1.Value);
    if (entityValueField2 != null)
      ((PXSelectBase) inIssueEntry.transactions).Cache.SetValueExt((object) current, "SiteID", (object) entityValueField2.Value);
    if (entityValueField3 != null)
      ((PXSelectBase) inIssueEntry.transactions).Cache.SetValueExt((object) current, "LocationID", (object) entityValueField3.Value);
    if (entityValueField4 != null)
      ((PXSelectBase) inIssueEntry.transactions).Cache.SetValueExt((object) current, "SubItemID", (object) entityValueField4.Value);
    if (((IEnumerable<EntityField>) targetEntity.Fields).FirstOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "Qty")) is EntityValueField entityValueField5)
    {
      current.Qty = new Decimal?(Decimal.Parse(entityValueField5.Value));
      ((PXSelectBase<INTran>) inIssueEntry.transactions).Update(current);
    }
    if (cache.Current == null)
      return;
    foreach (INTranSplit inTranSplit in ((PXSelectBase) inIssueEntry.splits).Cache.Inserted)
    {
      int? lineNbr1 = inTranSplit.LineNbr;
      int? lineNbr2 = (cache.Current as INTran).LineNbr;
      if (lineNbr1.GetValueOrDefault() == lineNbr2.GetValueOrDefault() & lineNbr1.HasValue == lineNbr2.HasValue)
        ((PXSelectBase<INTranSplit>) inIssueEntry.splits).Delete(inTranSplit);
    }
  }

  [FieldsProcessed(new string[] {"InvoiceType", "InvoiceNbr", "InvoiceLineNbr"})]
  protected override void SalesOrderDetail_Insert(
    PXGraph graph,
    EntityImpl entity,
    EntityImpl targetEntity)
  {
    SOOrderEntry soOrderEntry = (SOOrderEntry) graph;
    AddInvoiceExt extension = ((PXGraph) soOrderEntry).GetExtension<AddInvoiceExt>();
    EntityValueField entityValueField1 = ((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "InvoiceType")) as EntityValueField;
    EntityValueField entityValueField2 = ((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "InvoiceNbr")) as EntityValueField;
    EntityImpl[] entityImplArray = (((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => string.Equals(f.Name, "PurchasingDetails"))) is EntityListField entityListField ? entityListField.Value : (EntityImpl[]) null) ?? new EntityImpl[0];
    bool flag = ((IEnumerable<EntityImpl>) entityImplArray).Any<EntityImpl>((Func<EntityImpl, bool>) (a => a.Fields != null && a.Fields.Length != 0));
    if (entityValueField1 != null && entityValueField2 != null)
    {
      PXCache cache = ((PXSelectBase) extension.AddInvoiceFilter).Cache;
      AddInvoiceFilter current = (AddInvoiceFilter) cache.Current;
      current.OrderType = (string) null;
      current.StartDate = new DateTime?();
      current.EndDate = new DateTime?();
      string arRefNbr = current.ARRefNbr;
      cache.SetValueExt<AddInvoiceFilter.aRDocType>((object) current, (object) entityValueField1.Value);
      current.ARRefNbr = entityValueField2.Value;
      cache.RaiseFieldUpdated<AddSOFilter.orderNbr>((object) current, (object) arRefNbr);
      cache.Update((object) current);
      bool? autoCreateIssue = !string.IsNullOrEmpty(((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "AutoCreateIssue")) is EntityValueField entityValueField3 ? entityValueField3.Value : (string) null) ? new bool?(bool.Parse(entityValueField3.Value)) : new bool?();
      int? nullable1 = !string.IsNullOrEmpty(((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "InvoiceLineNbr")) is EntityValueField entityValueField4 ? entityValueField4.Value : (string) null) ? new int?(int.Parse(entityValueField4.Value)) : new int?();
      foreach (PXResult<InvoiceSplit> pxResult in ((PXSelectBase<InvoiceSplit>) extension.invoiceSplits).Select(Array.Empty<object>()))
      {
        InvoiceSplit invoiceSplit = PXResult<InvoiceSplit>.op_Implicit(pxResult);
        if (nullable1.HasValue)
        {
          int? arLineNbr = invoiceSplit.ARLineNbr;
          int? nullable2 = nullable1;
          if (arLineNbr.GetValueOrDefault() == nullable2.GetValueOrDefault() & arLineNbr.HasValue == nullable2.HasValue)
          {
            this.UpdateInvoiceSplit(extension, autoCreateIssue, invoiceSplit);
            break;
          }
        }
        else
          this.UpdateInvoiceSplit(extension, autoCreateIssue, invoiceSplit);
      }
      ((PXAction) extension.addInvoiceOK).Press();
    }
    else if (flag)
    {
      PXCache cache = ((PXSelectBase) soOrderEntry.Transactions).Cache;
      cache.Current = cache.Insert();
      PX.Objects.SO.SOLine current = cache.Current as PX.Objects.SO.SOLine;
      this.LinkSOandPO(soOrderEntry, current, targetEntity, entityImplArray);
    }
    else
      base.SalesOrderDetail_Insert(graph, entity, targetEntity);
  }

  protected virtual void UpdateInvoiceSplit(
    AddInvoiceExt ext,
    bool? autoCreateIssue,
    InvoiceSplit invoiceSplit)
  {
    invoiceSplit.QtyToReturn = invoiceSplit.QtyAvailForReturn;
    if (autoCreateIssue.HasValue)
      invoiceSplit.AutoCreateIssueLine = autoCreateIssue;
    ((PXSelectBase<InvoiceSplit>) ext.invoiceSplits).Update(invoiceSplit);
  }

  [FieldsProcessed(new string[] {})]
  protected virtual void SalesOrderDetail_Update(
    PXGraph graph,
    EntityImpl entity,
    EntityImpl targetEntity)
  {
    EntityImpl[] entityImplArray = (((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => string.Equals(f.Name, "PurchasingDetails"))) is EntityListField entityListField ? entityListField.Value : (EntityImpl[]) null) ?? new EntityImpl[0];
    if (!((IEnumerable<EntityImpl>) entityImplArray).Any<EntityImpl>((Func<EntityImpl, bool>) (a => a.Fields != null && a.Fields.Length != 0)))
      return;
    SOOrderEntry soOrderEntry = (SOOrderEntry) graph;
    PX.Objects.SO.SOLine current = ((PXSelectBase<PX.Objects.SO.SOLine>) soOrderEntry.Transactions).Current;
    this.LinkSOandPO(soOrderEntry, current, targetEntity, entityImplArray);
  }

  protected virtual void LinkSOandPO(
    SOOrderEntry soOrderEntry,
    PX.Objects.SO.SOLine transaction,
    EntityImpl targetEntity,
    EntityImpl[] purchasingDetailsEntities)
  {
    if (transaction == null)
      throw new InvalidOperationException("Cannot insert Sales Order detail.");
    this.SetFieldsNeedToInsertLinkPO(targetEntity, soOrderEntry, transaction);
    POLinkDialog extension = ((PXGraph) soOrderEntry).GetExtension<POLinkDialog>();
    foreach (EntityImpl purchasingDetailsEntity in purchasingDetailsEntities)
    {
      string str1 = ((IEnumerable<EntityField>) purchasingDetailsEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "POOrderType")) is EntityValueField entityValueField1 ? entityValueField1.Value : (string) null;
      string str2 = ((IEnumerable<EntityField>) purchasingDetailsEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "POOrderNbr")) is EntityValueField entityValueField2 ? entityValueField2.Value : (string) null;
      int? nullable1 = !string.IsNullOrEmpty(((IEnumerable<EntityField>) purchasingDetailsEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "POOrderLineNbr")) is EntityValueField entityValueField3 ? entityValueField3.Value : (string) null) ? new int?(int.Parse(entityValueField3.Value)) : new int?();
      bool? nullable2 = !string.IsNullOrEmpty(((IEnumerable<EntityField>) purchasingDetailsEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "Selected")) is EntityValueField entityValueField4 ? entityValueField4.Value : (string) null) ? new bool?(bool.Parse(entityValueField4.Value)) : new bool?();
      bool flag1 = false;
      ((PXSelectBase) extension.SOLineDemand).View.Cache.ClearQueryCache();
      foreach (PXResult<SupplyPOLine> pxResult in ((PXSelectBase<SupplyPOLine>) extension.SupplyPOLines).Select(Array.Empty<object>()))
      {
        SupplyPOLine supplyPoLine = PXResult<SupplyPOLine>.op_Implicit(pxResult);
        bool flag2 = supplyPoLine.OrderType == str1;
        if (!flag2 && ((PXSelectBase) extension.SupplyPOLines).Cache.GetStateExt<SupplyPOLine.orderType>((object) supplyPoLine) is PXStringState stateExt && stateExt.ValueLabelDic != null)
        {
          foreach (KeyValuePair<string, string> keyValuePair in stateExt.ValueLabelDic)
          {
            if (keyValuePair.Key == supplyPoLine.OrderType && keyValuePair.Value == str1)
            {
              flag2 = true;
              break;
            }
          }
        }
        if (flag2 && supplyPoLine.OrderNbr == str2)
        {
          int? lineNbr = supplyPoLine.LineNbr;
          int? nullable3 = nullable1;
          if (lineNbr.GetValueOrDefault() == nullable3.GetValueOrDefault() & lineNbr.HasValue == nullable3.HasValue)
          {
            supplyPoLine.Selected = nullable2;
            ((PXSelectBase<SupplyPOLine>) extension.SupplyPOLines).Update(supplyPoLine);
            flag1 = true;
          }
        }
      }
      if (!flag1)
        throw new Exception($"Purchase Order Line (Order Type = {str1}, OrderNbr = {str2}, LineNbr = {nullable1}) cannot be found in Purchasing Details for Sales Order line (LineNbr = {transaction.LineNbr}).");
    }
    ((PXSelectBase) extension.SOLineDemand).View.Answer = (WebDialogResult) 1;
    ((PXAction) extension.pOSupplyOK).Press();
  }

  protected virtual void SetFieldsNeedToInsertLinkPO(
    EntityImpl targetEntity,
    SOOrderEntry soOrderEntry,
    PX.Objects.SO.SOLine soLine)
  {
    bool? poCreate = soLine.POCreate;
    string poSource = soLine.POSource;
    EntityValueField entityValueField1 = ((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "InventoryID")) as EntityValueField;
    EntityValueField entityValueField2 = ((IEnumerable<EntityField>) targetEntity.Fields).FirstOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "Subitem")) as EntityValueField;
    EntityValueField entityValueField3 = ((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "WarehouseID")) as EntityValueField;
    Decimal? nullable1 = !string.IsNullOrEmpty(((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "OrderQty")) is EntityValueField entityValueField4 ? entityValueField4.Value : (string) null) ? new Decimal?(Decimal.Parse(entityValueField4.Value)) : new Decimal?();
    EntityValueField entityValueField5 = ((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "UOM")) as EntityValueField;
    bool? nullable2 = !string.IsNullOrEmpty(((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "MarkForPO")) is EntityValueField entityValueField6 ? entityValueField6.Value : (string) null) ? new bool?(bool.Parse(entityValueField6.Value)) : new bool?();
    bool? nullable3 = !nullable2.HasValue ? poCreate : nullable2;
    EntityValueField POSourceParam = ((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "POSource")) as EntityValueField;
    EntityValueField entityValueField7 = ((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "PurchaseWarehouse")) as EntityValueField;
    EntityValueField entityValueField8 = ((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "VendorID")) as EntityValueField;
    string POSourceValue;
    if (POSourceParam == null)
    {
      POSourceValue = poSource;
    }
    else
    {
      if (!(((PXSelectBase) soOrderEntry.Transactions).Cache.GetStateExt((object) soLine, "POSource") is PXStringState stateExt))
        throw new Exception($"Cannot get labels for PO Source for Sales Order line (LineNbr = {soLine.LineNbr}).");
      if (!((IEnumerable<string>) stateExt.AllowedLabels).Contains<string>(POSourceParam.Value))
        throw new Exception($"PO Source = {POSourceParam.Value} is not found in the system for Sales Order line (LineNbr = {soLine.LineNbr}).");
      POSourceValue = stateExt.ValueLabelDic.Single<KeyValuePair<string, string>>((Func<KeyValuePair<string, string>, bool>) (p => p.Value == POSourceParam.Value)).Key;
    }
    int num = this.IsDropShip(POSourceValue) ? 1 : 0;
    if (entityValueField1 != null)
      ((PXSelectBase) soOrderEntry.Transactions).Cache.SetValueExt((object) soLine, "InventoryID", (object) entityValueField1.Value);
    if (entityValueField2 != null)
      ((PXSelectBase) soOrderEntry.Transactions).Cache.SetValueExt((object) soLine, "SubItemID", (object) entityValueField2.Value);
    if (entityValueField3 != null)
      ((PXSelectBase) soOrderEntry.Transactions).Cache.SetValueExt((object) soLine, "SiteID", (object) entityValueField3.Value);
    if (entityValueField4 != null)
      ((PXSelectBase) soOrderEntry.Transactions).Cache.SetValueExt((object) soLine, "OrderQty", (object) nullable1);
    if (entityValueField5 != null)
      ((PXSelectBase) soOrderEntry.Transactions).Cache.SetValueExt((object) soLine, "UOM", (object) entityValueField5.Value);
    if (entityValueField6 != null)
      ((PXSelectBase) soOrderEntry.Transactions).Cache.SetValueExt((object) soLine, "POCreate", (object) nullable3);
    if (POSourceValue != null)
      ((PXSelectBase) soOrderEntry.Transactions).Cache.SetValueExt((object) soLine, "POSource", (object) POSourceValue);
    if (num == 0 && entityValueField7 != null)
      ((PXSelectBase) soOrderEntry.Transactions).Cache.SetValueExt((object) soLine, "POSiteID", (object) entityValueField7.Value);
    soLine = ((PXSelectBase<PX.Objects.SO.SOLine>) soOrderEntry.Transactions).Update(soLine);
    if (entityValueField8 == null)
      return;
    ((PXSelectBase) soOrderEntry.Transactions).Cache.SetValueExt((object) soLine, "VendorID", (object) entityValueField8?.Value);
  }

  protected virtual bool IsDropShip(string POSourceValue)
  {
    return POSourceValue != null && POSourceValue == "D";
  }

  [FieldsProcessed(new string[] {"ShipmentNbr", "OrderNbr", "OrderType", "OrigInvType", "OrigInvNbr", "OrigInvLineNbr"})]
  protected override void SalesInvoiceDetail_Insert(
    PXGraph graph,
    EntityImpl entity,
    EntityImpl targetEntity)
  {
    SOInvoiceEntry soInvoiceEntry = (SOInvoiceEntry) graph;
    EntityValueField OrigInvoiceTypeField = ((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "OrigInvType")) as EntityValueField;
    EntityValueField entityValueField1 = ((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "OrigInvNbr")) as EntityValueField;
    EntityValueField entityValueField2 = ((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "OrigInvLineNbr")) as EntityValueField;
    if (OrigInvoiceTypeField != null && entityValueField1 != null)
    {
      int? nullable1 = !string.IsNullOrEmpty(entityValueField2?.Value) ? new int?(int.Parse(entityValueField2.Value)) : new int?();
      AddReturnLineToDirectInvoice extension = ((PXGraph) soInvoiceEntry).GetExtension<AddReturnLineToDirectInvoice>();
      if (extension == null)
        return;
      string key = OrigInvoiceTypeField.Value;
      if (((PXSelectBase) extension.arTranList).Cache.GetStateExt<ARTranForDirectInvoice.tranType>((object) new ARTranForDirectInvoice()) is PXStringState stateExt && ((IEnumerable<string>) stateExt.AllowedLabels).Contains<string>(OrigInvoiceTypeField.Value))
        key = stateExt.ValueLabelDic.Single<KeyValuePair<string, string>>((Func<KeyValuePair<string, string>, bool>) (p => p.Value == OrigInvoiceTypeField.Value)).Key;
      foreach (PXResult<ARTranForDirectInvoice> pxResult in ((PXSelectBase<ARTranForDirectInvoice>) extension.arTranList).Select(Array.Empty<object>()))
      {
        ARTranForDirectInvoice forDirectInvoice = PXResult<ARTranForDirectInvoice>.op_Implicit(pxResult);
        if (forDirectInvoice.TranType == key && forDirectInvoice.RefNbr == entityValueField1.Value)
        {
          if (nullable1.HasValue)
          {
            int? lineNbr = forDirectInvoice.LineNbr;
            int? nullable2 = nullable1;
            if (lineNbr.GetValueOrDefault() == nullable2.GetValueOrDefault() & lineNbr.HasValue == nullable2.HasValue)
            {
              forDirectInvoice.Selected = new bool?(true);
              ((PXSelectBase<ARTranForDirectInvoice>) extension.arTranList).Update(forDirectInvoice);
              break;
            }
          }
          else
          {
            forDirectInvoice.Selected = new bool?(true);
            ((PXSelectBase<ARTranForDirectInvoice>) extension.arTranList).Update(forDirectInvoice);
          }
        }
      }
      ((PXAction) extension.addARTran).Press();
    }
    else
      base.SalesInvoiceDetail_Insert(graph, entity, targetEntity);
  }

  [FieldsProcessed(new string[] {"ShipmentSplitLineNbr", "OrigOrderType", "OrigOrderNbr", "InventoryID", "Subitem", "LotSerialNbr"})]
  protected virtual void ShipmentPackageDetail_Insert(
    PXGraph graph,
    EntityImpl entity,
    EntityImpl targetEntity)
  {
    SOShipmentEntry soShipmentEntry = (SOShipmentEntry) graph;
    SOShipmentEntry.PackageDetail extension = ((PXGraph) soShipmentEntry).GetExtension<SOShipmentEntry.PackageDetail>();
    if (extension == null)
      return;
    SOShipLineSplitPackage lineSplitPackage = ((PXSelectBase<SOShipLineSplitPackage>) extension.PackageDetailSplit).Insert(new SOShipLineSplitPackage());
    EntityValueField field = ((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "ShipmentSplitLineNbr")) as EntityValueField;
    int? nullableInt = DefaultEndpointImpl.ConvertToNullableInt(field);
    EntityValueField entityValueField1 = ((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "OrigOrderType")) as EntityValueField;
    EntityValueField entityValueField2 = ((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "OrigOrderNbr")) as EntityValueField;
    EntityValueField entityValueField3 = ((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "InventoryID")) as EntityValueField;
    EntityValueField entityValueField4 = ((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "Subitem")) as EntityValueField;
    EntityValueField entityValueField5 = ((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "LotSerialNbr")) as EntityValueField;
    Decimal? nullableDecimal = DefaultEndpointImpl.ConvertToNullableDecimal(((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "Quantity")) as EntityValueField);
    if (string.IsNullOrWhiteSpace(entityValueField3?.Value) && !nullableInt.HasValue)
      return;
    List<SOShipLineSplit> source = new List<SOShipLineSplit>();
    foreach (PXResult<SOShipLineSplit> pxResult in PXSelectBase<SOShipLineSplit, PXSelect<SOShipLineSplit, Where<SOShipLineSplit.shipmentNbr, Equal<Required<SOShipLineSplit.shipmentNbr>>>>.Config>.Select(graph, new object[1]
    {
      (object) ((PXSelectBase<PX.Objects.SO.SOShipment>) soShipmentEntry.Document).Current?.ShipmentNbr
    }))
    {
      SOShipLineSplit soShipLineSplit = PXResult<SOShipLineSplit>.op_Implicit(pxResult);
      if (EnumerableExtensions.IsNotIn<PXEntryStatus>(((PXSelectBase) soShipmentEntry.splits).Cache.GetStatus((object) soShipLineSplit), (PXEntryStatus) 3, (PXEntryStatus) 4))
      {
        if (nullableInt.HasValue)
        {
          int? nullable = nullableInt;
          int? splitLineNbr = soShipLineSplit.SplitLineNbr;
          if (nullable.GetValueOrDefault() == splitLineNbr.GetValueOrDefault() & nullable.HasValue == splitLineNbr.HasValue)
          {
            source.Add(soShipLineSplit);
            break;
          }
        }
        else
        {
          object obj1 = PXFieldState.UnwrapValue(((PXSelectBase) soShipmentEntry.splits).Cache.GetValueExt<SOShipLineSplit.inventoryID>((object) soShipLineSplit));
          if (obj1 != null && obj1.ToString().Trim() == entityValueField3.Value.Trim())
          {
            source.Add(soShipLineSplit);
            if (!string.IsNullOrWhiteSpace(entityValueField4?.Value))
            {
              object obj2 = PXFieldState.UnwrapValue(((PXSelectBase) soShipmentEntry.splits).Cache.GetValueExt<SOShipLineSplit.subItemID>((object) soShipLineSplit));
              if (obj2 != null && obj2.ToString().Trim() != entityValueField4.Value.Trim())
              {
                source.Remove(soShipLineSplit);
                continue;
              }
            }
            if (!string.Equals(entityValueField5?.Value, soShipLineSplit.LotSerialNbr, StringComparison.OrdinalIgnoreCase))
              source.Remove(soShipLineSplit);
            else if (entityValueField1?.Value != soShipLineSplit.OrigOrderType)
              source.Remove(soShipLineSplit);
            else if (entityValueField2?.Value != soShipLineSplit.OrigOrderNbr)
              source.Remove(soShipLineSplit);
            else if (nullableDecimal.HasValue)
            {
              Decimal? nullable = nullableDecimal;
              Decimal? qty = soShipLineSplit.Qty;
              if (nullable.GetValueOrDefault() > qty.GetValueOrDefault() & nullable.HasValue & qty.HasValue)
                source.Remove(soShipLineSplit);
            }
          }
        }
      }
    }
    SOShipLineSplit soShipLineSplit1 = source.Count > 0 ? source.First<SOShipLineSplit>() : throw new PXException($"No suitable items found to pack (ShipmentSplitLineNbr: {field?.Value} InventoryID: {entityValueField3?.Value} Subitem: {entityValueField4?.Value} LotSerialNbr: {entityValueField5?.Value} OrigOrderType: {entityValueField1?.Value} OrigOrderNbr: {entityValueField2?.Value}).");
    lineSplitPackage.ShipmentSplitLineNbr = soShipLineSplit1.SplitLineNbr;
    ((PXSelectBase) extension.PackageDetailSplit).Cache.Update((object) lineSplitPackage);
  }
}
