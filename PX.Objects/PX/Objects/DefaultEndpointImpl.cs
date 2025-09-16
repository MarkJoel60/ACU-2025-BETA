// Decompiled with JetBrains decompiler
// Type: PX.Objects.DefaultEndpointImpl
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Api;
using PX.Api.ContractBased;
using PX.Api.ContractBased.Models;
using PX.Common;
using PX.Data;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.CA;
using PX.Objects.CN.Subcontracts.AP.GraphExtensions;
using PX.Objects.CS;
using PX.Objects.EP;
using PX.Objects.IN;
using PX.Objects.PO;
using PX.Objects.PO.DAC.Projections;
using PX.Objects.PO.GraphExtensions.APInvoiceSmartPanel;
using PX.Objects.PO.GraphExtensions.POReceiptEntryExt;
using PX.Objects.SO;
using PX.Objects.SO.DAC.Projections;
using PX.Objects.SO.GraphExtensions.SOOrderEntryExt;
using PX.Objects.SO.GraphExtensions.SOShipmentEntryExt;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

#nullable disable
namespace PX.Objects;

[PXInternalUseOnly]
public abstract class DefaultEndpointImpl
{
  [FieldsProcessed(new string[] {"DetailID", "Value"})]
  protected void BusinessAccountPaymentInstructionDetail_Insert(
    PXGraph graph,
    EntityImpl entity,
    EntityImpl targetEntity)
  {
    IEnumerable<EntityValueField> source = targetEntity.Fields.OfType<EntityValueField>();
    string str1 = source.Single<EntityValueField>((Func<EntityValueField, bool>) (f => ((EntityField) f).Name.EndsWith("ID"))).Value;
    string str2 = source.Single<EntityValueField>((Func<EntityValueField, bool>) (f => ((EntityField) f).Name.EndsWith("Value"))).Value;
    switch (graph)
    {
      case VendorMaint vendorMaint:
        VendorMaint.DefLocationExt extension1 = ((PXGraph) vendorMaint).GetExtension<VendorMaint.DefLocationExt>();
        ((PXSelectBase) extension1.DefLocation).View.Clear();
        PX.Objects.CR.Standalone.Location location1 = ((PXSelectBase<PX.Objects.CR.Standalone.Location>) extension1.DefLocation).Current = ((PXSelectBase<PX.Objects.CR.Standalone.Location>) extension1.DefLocation).SelectSingle(Array.Empty<object>());
        VendorMaint.PaymentDetailsExt extension2 = ((PXGraph) vendorMaint).GetExtension<VendorMaint.PaymentDetailsExt>();
        extension2.FillPaymentDetails(location1);
        foreach (PXResult<VendorPaymentMethodDetail> pxResult in ((PXSelectBase<VendorPaymentMethodDetail>) extension2.PaymentDetails).Select(new object[3]
        {
          (object) location1.BAccountID,
          (object) location1.LocationID,
          (object) location1.VPaymentMethodID
        }))
        {
          VendorPaymentMethodDetail paymentMethodDetail = PXResult<VendorPaymentMethodDetail>.op_Implicit(pxResult);
          if (paymentMethodDetail.DetailID == str1)
          {
            ((PXSelectBase) extension2.PaymentDetails).Cache.SetValueExt<VendorPaymentMethodDetail.detailValue>((object) paymentMethodDetail, (object) str2);
            ((PXSelectBase) extension2.PaymentDetails).Cache.Update((object) paymentMethodDetail);
            return;
          }
        }
        throw new PXException("{0} with ID '{1}' does not exist", new object[2]
        {
          (object) EntityHelper.GetFriendlyEntityName<VendorPaymentMethodDetail>(),
          (object) str1
        });
      case EmployeeMaint employeeMaint:
        PX.Objects.CR.Location location2 = PXResultset<PX.Objects.CR.Location>.op_Implicit(((PXSelectBase<PX.Objects.CR.Location>) employeeMaint.DefLocation).Select(Array.Empty<object>()));
        foreach (PXResult<VendorPaymentMethodDetail> pxResult in ((PXSelectBase<VendorPaymentMethodDetail>) employeeMaint.PaymentDetails).Select(new object[3]
        {
          (object) location2.BAccountID,
          (object) location2.LocationID,
          (object) location2.VPaymentMethodID
        }))
        {
          VendorPaymentMethodDetail paymentMethodDetail = PXResult<VendorPaymentMethodDetail>.op_Implicit(pxResult);
          if (paymentMethodDetail.DetailID == str1)
          {
            ((PXSelectBase) employeeMaint.PaymentDetails).Cache.SetValueExt<VendorPaymentMethodDetail.detailValue>((object) paymentMethodDetail, (object) str2);
            ((PXSelectBase) employeeMaint.PaymentDetails).Cache.Update((object) paymentMethodDetail);
            return;
          }
        }
        throw new PXException("{0} with ID '{1}' does not exist", new object[2]
        {
          (object) EntityHelper.GetFriendlyEntityName<VendorPaymentMethodDetail>(),
          (object) str1
        });
      case CustomerMaint customerMaint:
        CustomerMaint.DefLocationExt extension3 = ((PXGraph) customerMaint).GetExtension<CustomerMaint.DefLocationExt>();
        PX.Objects.CR.Standalone.Location location3 = ((PXSelectBase<PX.Objects.CR.Standalone.Location>) extension3.DefLocation).Current = ((PXSelectBase<PX.Objects.CR.Standalone.Location>) extension3.DefLocation).SelectSingle(Array.Empty<object>());
        CustomerMaint.PaymentDetailsExt extension4 = ((PXGraph) customerMaint).GetExtension<CustomerMaint.PaymentDetailsExt>();
        foreach (PXResult<CustomerPaymentMethodDetail> pxResult in ((PXSelectBase<CustomerPaymentMethodDetail>) extension4.DefPaymentMethodInstanceDetails).Select(new object[3]
        {
          (object) location3.BAccountID,
          (object) location3.LocationID,
          (object) location3.VPaymentMethodID
        }))
        {
          CustomerPaymentMethodDetail paymentMethodDetail = PXResult<CustomerPaymentMethodDetail>.op_Implicit(pxResult);
          if (paymentMethodDetail.DetailID == str1)
          {
            ((PXSelectBase) extension4.DefPaymentMethodInstanceDetails).Cache.SetValueExt<CustomerPaymentMethodDetail.value>((object) paymentMethodDetail, (object) str2);
            ((PXSelectBase) extension4.DefPaymentMethodInstanceDetails).Cache.Update((object) paymentMethodDetail);
            return;
          }
        }
        throw new PXException("{0} with ID '{1}' does not exist", new object[2]
        {
          (object) EntityHelper.GetFriendlyEntityName<CustomerPaymentMethodDetail>(),
          (object) str1
        });
      default:
        throw new InvalidOperationException("Not applicable for " + graph.GetType()?.ToString());
    }
  }

  [FieldsProcessed(new string[] {"OrderType", "OrderNbr", "OrderLineNbr", "InventoryID", "LotSerialNbr", "ShippedQty"})]
  protected virtual void ShipmentDetail_Insert(
    PXGraph graph,
    EntityImpl entity,
    EntityImpl targetEntity)
  {
    SOShipmentEntry soShipmentEntry = (SOShipmentEntry) graph;
    SOOrderExtension extension = graph.GetExtension<SOOrderExtension>();
    PXCache cache1 = ((PXSelectBase) extension.addsofilter).Cache;
    AddSOFilter current1 = (AddSOFilter) cache1.Current;
    EntityValueField entityValueField1 = ((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "OrderLineNbr")) as EntityValueField;
    EntityValueField entityValueField2 = ((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "ShippedQty")) as EntityValueField;
    string str1 = ((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "OrderType")) is EntityValueField entityValueField3 ? entityValueField3.Value : (string) null;
    string str2 = ((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "OrderNbr")) is EntityValueField entityValueField4 ? entityValueField4.Value : (string) null;
    int? nullable1 = !string.IsNullOrEmpty(entityValueField1?.Value) ? new int?(int.Parse(entityValueField1.Value)) : new int?();
    string str3 = ((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "InventoryID")) is EntityValueField entityValueField5 ? entityValueField5.Value : (string) null;
    string lotSerialNbr = ((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "LotSerialNbr")) is EntityValueField entityValueField6 ? entityValueField6.Value : (string) null;
    Decimal? shippedQty = !string.IsNullOrEmpty(entityValueField2?.Value) ? new Decimal?(Decimal.Parse(entityValueField2.Value)) : new Decimal?();
    string str4 = ((IEnumerable<EntityField>) entity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "CreateNewShipmentForEveryOrder")) is EntityValueField entityValueField7 ? entityValueField7.Value : (string) null;
    bool result = false;
    if (str4 != null)
      bool.TryParse(str4, out result);
    string orderNbr = current1.OrderNbr;
    current1.OrderType = str1;
    current1.OrderNbr = str2;
    cache1.RaiseFieldUpdated<AddSOFilter.orderNbr>((object) current1, (object) orderNbr);
    cache1.Update((object) current1);
    SOShipmentPlan soShipmentPlan1 = new SOShipmentPlan();
    int? nullable2;
    if (nullable1.HasValue)
    {
      nullable2 = nullable1;
      int num = 0;
      if (nullable2.GetValueOrDefault() >= num & nullable2.HasValue)
      {
        current1.OrderLineNbr = nullable1;
        soShipmentPlan1 = DefaultEndpointImpl.FindSalesOrderLine(extension, (Func<PXResult<SOShipmentPlan>, bool>) (_ => true));
        goto label_14;
      }
    }
    if (!string.IsNullOrWhiteSpace(str3))
    {
      PXResultset<PX.Objects.IN.InventoryItem> source = PXSelectBase<PX.Objects.IN.InventoryItem, PXSelect<PX.Objects.IN.InventoryItem, Where<PX.Objects.IN.InventoryItem.inventoryCD, Equal<Required<PX.Objects.IN.InventoryItem.inventoryCD>>>>.Config>.Select(graph, new object[1]
      {
        (object) str3
      });
      Func<PXResult<SOShipmentPlan>, bool> func = (Func<PXResult<SOShipmentPlan>, bool>) (r =>
      {
        int? inventoryId1 = ((PXResult) r).GetItem<PX.Objects.SO.SOLineSplit>().InventoryID;
        int? inventoryId2 = ((source != null ? ((PXResult) ((IQueryable<PXResult<PX.Objects.IN.InventoryItem>>) source).FirstOrDefault<PXResult<PX.Objects.IN.InventoryItem>>())?.GetItem<PX.Objects.IN.InventoryItem>() : (PX.Objects.IN.InventoryItem) null) ?? throw new PXException("Inventory Item '{0}' was not found.", new object[1]
        {
          (object) str3
        })).InventoryID;
        return inventoryId1.GetValueOrDefault() == inventoryId2.GetValueOrDefault() & inventoryId1.HasValue == inventoryId2.HasValue;
      });
      if (!string.IsNullOrWhiteSpace(lotSerialNbr))
        func = Func.Conjoin<PXResult<SOShipmentPlan>>(func, (Func<PXResult<SOShipmentPlan>, bool>) (t => string.Equals(((PXResult) t).GetItem<PX.Objects.SO.SOLineSplit>().LotSerialNbr, lotSerialNbr, StringComparison.OrdinalIgnoreCase)));
      if (shippedQty.HasValue)
      {
        Decimal? nullable3 = shippedQty;
        Decimal num = 0M;
        if (nullable3.GetValueOrDefault() > num & nullable3.HasValue)
          func = Func.Conjoin<PXResult<SOShipmentPlan>>(func, (Func<PXResult<SOShipmentPlan>, bool>) (t =>
          {
            Decimal? openQty = ((PXResult) t).GetItem<PX.Objects.SO.SOLineSplit>().OpenQty;
            Decimal? nullable4 = shippedQty;
            return openQty.GetValueOrDefault() >= nullable4.GetValueOrDefault() & openQty.HasValue & nullable4.HasValue;
          }));
      }
      soShipmentPlan1 = DefaultEndpointImpl.FindSalesOrderLine(extension, func);
    }
label_14:
    bool flag = false;
    nullable2 = soShipmentPlan1.InventoryID;
    if (nullable2.HasValue)
    {
      this.SelectLine(extension, soShipmentPlan1);
      flag = true;
      ((PXAction) extension.addSO).Press();
    }
    else if (result)
    {
      current1.AddAllLines = new bool?(true);
      ((PXAction) extension.addSO).Press();
      if (((PXGraph) soShipmentEntry).Caches[typeof (SOShipLine)].Current is SOShipLine current2)
      {
        PX.Objects.SO.SOShipment copy = (PX.Objects.SO.SOShipment) ((PXSelectBase) soShipmentEntry.Document).Cache.CreateCopy((object) ((PXSelectBase<PX.Objects.SO.SOShipment>) soShipmentEntry.Document).Current);
        ((PXSelectBase<PX.Objects.SO.SOShipment>) soShipmentEntry.Document).Current.ShipmentNbr = current2.ShipmentNbr;
      }
    }
    else
    {
      foreach (PXResult<SOShipmentPlan> pxResult in ((PXSelectBase<SOShipmentPlan>) extension.soshipmentplan).Select(Array.Empty<object>()))
      {
        SOShipmentPlan soShipmentPlan2 = PXResult<SOShipmentPlan>.op_Implicit(pxResult);
        this.SelectLine(extension, soShipmentPlan2);
      }
      ((PXAction) extension.addSO).Press();
    }
    if (!(((PXGraph) soShipmentEntry).Caches[typeof (SOShipLine)].Current is SOShipLine current3))
      throw new InvalidOperationException("The system cannot add the shipment details.");
    if (((IEnumerable<EntityImpl>) ((((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => string.Equals(f.Name, "Allocations"))) is EntityListField entityListField ? entityListField.Value : (EntityImpl[]) null) ?? new EntityImpl[0])).Any<EntityImpl>((Func<EntityImpl, bool>) (a => a.Fields != null && a.Fields.Length != 0)))
    {
      if (!flag)
        throw new InvalidOperationException("Allocations can be specified for unambiguously selected Sales Order line only.");
      PXCache cache2 = ((PXSelectBase) soShipmentEntry.splits).Cache;
      foreach (SOShipLineSplit soShipLineSplit in cache2.Inserted)
      {
        if (cache2.GetStatus((object) soShipLineSplit) == 2 && !(current3.ShipmentNbr != soShipLineSplit.ShipmentNbr))
        {
          int? lineNbr1 = current3.LineNbr;
          int? lineNbr2 = soShipLineSplit.LineNbr;
          if (lineNbr1.GetValueOrDefault() == lineNbr2.GetValueOrDefault() & lineNbr1.HasValue == lineNbr2.HasValue)
            ((PXSelectBase<SOShipLineSplit>) soShipmentEntry.splits).Delete(soShipLineSplit);
        }
      }
      SOShipLine copy = (SOShipLine) ((PXGraph) soShipmentEntry).Caches[typeof (SOShipLine)].CreateCopy((object) current3);
      copy.ShippedQty = new Decimal?(0M);
      ((PXGraph) soShipmentEntry).Caches[typeof (SOShipLine)].Update((object) copy);
    }
    else
    {
      if (!shippedQty.HasValue)
        return;
      current3.ShippedQty = shippedQty;
      ((PXGraph) soShipmentEntry).Caches[typeof (SOShipLine)].Update((object) current3);
    }
  }

  protected static SOShipmentPlan FindSalesOrderLine(
    SOOrderExtension shipmentEntry,
    Func<PXResult<SOShipmentPlan>, bool> lineCriteria)
  {
    try
    {
      return PXResult<SOShipmentPlan>.op_Implicit(((IEnumerable<PXResult<SOShipmentPlan>>) ((PXSelectBase<SOShipmentPlan>) shipmentEntry.soshipmentplan).Select(Array.Empty<object>())).First<PXResult<SOShipmentPlan>>(lineCriteria));
    }
    catch (InvalidOperationException ex)
    {
      throw new InvalidOperationException("No sales order line can be found using the specified parameters.", (Exception) ex);
    }
  }

  protected void SelectLine(SOOrderExtension shipmentEntry, SOShipmentPlan item)
  {
    item.Selected = new bool?(true);
    item = ((PXSelectBase<SOShipmentPlan>) shipmentEntry.soshipmentplan).Update(item);
    ((PXSelectBase) shipmentEntry.soshipmentplan).Cache.SetStatus((object) item, (PXEntryStatus) 0);
    this.AssertNoErrors(((PXSelectBase) shipmentEntry.soshipmentplan).Cache, (object) item);
  }

  protected void AssertNoErrors(PXCache cache, object current)
  {
    Dictionary<string, string> errors = PXUIFieldAttribute.GetErrors(cache, current, Array.Empty<PXErrorLevel>());
    if (errors.Count != 0)
      throw new InvalidOperationException(string.Join("\n", errors.Select<KeyValuePair<string, string>, string>((Func<KeyValuePair<string, string>, string>) (p => $"{p.Key}: {p.Value}"))));
  }

  [FieldsProcessed(new string[] {"ShipmentNbr", "OrderNbr", "OrderType"})]
  protected virtual void SalesInvoiceDetail_Insert(
    PXGraph graph,
    EntityImpl entity,
    EntityImpl targetEntity)
  {
    SOInvoiceEntry graph1 = (SOInvoiceEntry) graph;
    EntityValueField entityValueField1 = ((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "ShipmentNbr")) as EntityValueField;
    EntityValueField entityValueField2 = ((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "OrderNbr")) as EntityValueField;
    EntityValueField entityValueField3 = ((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "OrderType")) as EntityValueField;
    EntityValueField field = ((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "CalculateDiscountsOnImport")) as EntityValueField;
    string shipment = entityValueField1?.Value;
    string number = entityValueField2?.Value;
    string type = entityValueField3?.Value;
    bool? nullableBool = DefaultEndpointImpl.ConvertToNullableBool(field);
    if (shipment != null && number != null && type != null)
    {
      IEnumerable<PX.Objects.SO.SOOrderShipment> source = ((IEnumerable<PXResult<PX.Objects.SO.SOOrderShipment>>) ((PXSelectBase<PX.Objects.SO.SOOrderShipment>) graph1.shipmentlist).Select(Array.Empty<object>())).AsEnumerable<PXResult<PX.Objects.SO.SOOrderShipment>>().Select<PXResult<PX.Objects.SO.SOOrderShipment>, PX.Objects.SO.SOOrderShipment>((Func<PXResult<PX.Objects.SO.SOOrderShipment>, PX.Objects.SO.SOOrderShipment>) (s => ((PXResult) s).GetItem<PX.Objects.SO.SOOrderShipment>())).Where<PX.Objects.SO.SOOrderShipment>((Func<PX.Objects.SO.SOOrderShipment, bool>) (s =>
      {
        if (shipment != null && !StringExtensions.OrdinalEquals(s.ShipmentNbr, shipment) || number != null && !StringExtensions.OrdinalEquals(s.OrderNbr, number))
          return false;
        return type == null || StringExtensions.OrdinalEquals(s.OrderType, type);
      }));
      if (!source.Any<PX.Objects.SO.SOOrderShipment>())
        throw new PXException("Shipments were not found.");
      foreach (PX.Objects.SO.SOOrderShipment soOrderShipment in source)
      {
        foreach (PXResult<PX.Objects.SO.SOOrderShipment> pxResult in ((PXSelectBase<PX.Objects.SO.SOOrderShipment>) graph1.shipmentlist).Select(Array.Empty<object>()))
          PXResult<PX.Objects.SO.SOOrderShipment>.op_Implicit(pxResult).Selected = new bool?(false);
        soOrderShipment.Selected = new bool?(true);
        ((PXSelectBase<PX.Objects.SO.SOOrderShipment>) graph1.shipmentlist).Update(soOrderShipment);
        ((PXGraph) graph1).Actions["AddShipment"].Press();
      }
    }
    else
    {
      if (shipment != null)
        return;
      PX.Objects.AR.ARTran instance = (PX.Objects.AR.ARTran) ((PXSelectBase) graph1.Transactions).Cache.CreateInstance();
      instance.SOShipmentNbr = shipment;
      instance.SOOrderType = type;
      instance.SOOrderNbr = number;
      instance.CalculateDiscountsOnImport = nullableBool;
      DefaultEndpointImpl.FillInvoiceRowFromEntiry(graph1, targetEntity, instance);
      graph1.InsertInvoiceDirectLine(instance);
    }
  }

  [FieldsProcessed(new string[] {"Value", "Active", "SegmentID"})]
  protected void SubItemStockItem_Insert(PXGraph graph, EntityImpl entity, EntityImpl targetEntity)
  {
    EntityValueField entityValueField1 = ((IEnumerable<EntityField>) targetEntity.Fields).Single<EntityField>((Func<EntityField, bool>) (f => f.Name == "Value")) as EntityValueField;
    EntityValueField entityValueField2 = ((IEnumerable<EntityField>) targetEntity.Fields).Single<EntityField>((Func<EntityField, bool>) (f => f.Name == "Active")) as EntityValueField;
    EntityValueField entityValueField3 = ((IEnumerable<EntityField>) targetEntity.Fields).Single<EntityField>((Func<EntityField, bool>) (f => f.Name == "SegmentID")) as EntityValueField;
    PXView view = graph.Views["SubItem_" + entityValueField3.Value];
    PXCache cache = view.Cache;
    foreach (INSubItemSegmentValueList.SValue svalue in view.SelectMulti(new object[1]
    {
      (object) entityValueField3.Value
    }))
    {
      if (svalue.Value == entityValueField1.Value)
      {
        if (entityValueField2.Value == "true")
        {
          svalue.Active = new bool?(true);
          cache.Update((object) svalue);
        }
        else
          cache.Delete((object) svalue);
      }
    }
  }

  [FieldsProcessed(new string[] {"Value", "Active", "SegmentID"})]
  protected void SubItemStockItem_Update(PXGraph graph, EntityImpl entity, EntityImpl targetEntity)
  {
    EntityValueField entityValueField1 = ((IEnumerable<EntityField>) targetEntity.Fields).Single<EntityField>((Func<EntityField, bool>) (f => f.Name == "Value")) as EntityValueField;
    EntityValueField entityValueField2 = ((IEnumerable<EntityField>) targetEntity.Fields).Single<EntityField>((Func<EntityField, bool>) (f => f.Name == "Active")) as EntityValueField;
    EntityValueField entityValueField3 = ((IEnumerable<EntityField>) targetEntity.Fields).Single<EntityField>((Func<EntityField, bool>) (f => f.Name == "SegmentID")) as EntityValueField;
    PXView view = graph.Views["SubItem_" + entityValueField3.Value];
    PXCache cache = view.Cache;
    foreach (INSubItemSegmentValueList.SValue svalue in view.SelectMulti(new object[1]
    {
      (object) entityValueField3.Value
    }))
    {
      if (svalue.Value == entityValueField1.Value)
      {
        if (entityValueField2.Value == "true")
        {
          svalue.Active = new bool?(true);
          cache.Update((object) svalue);
        }
        else
          cache.Delete((object) svalue);
      }
    }
  }

  [FieldsProcessed(new string[] {"WarehouseID"})]
  protected void StockItemWarehouseDetail_Insert(
    PXGraph graph,
    EntityImpl entity,
    EntityImpl targetEntity)
  {
    EntityValueField entityValueField1 = ((IEnumerable<EntityField>) targetEntity.Fields).FirstOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "WarehouseID")) as EntityValueField;
    EntityValueField entityValueField2 = ((IEnumerable<EntityField>) targetEntity.Fields).FirstOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "DefaultIssueLocationID")) as EntityValueField;
    EntityValueField entityValueField3 = ((IEnumerable<EntityField>) targetEntity.Fields).FirstOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "DefaultReceiptLocationID")) as EntityValueField;
    PXView view = graph.Views["itemsiterecords"];
    PXCache cache = view.Cache;
    PX.Objects.IN.INSite inSite = PXResultset<PX.Objects.IN.INSite>.op_Implicit(PXSelectBase<PX.Objects.IN.INSite, PXSelect<PX.Objects.IN.INSite, Where<PX.Objects.IN.INSite.siteCD, Equal<Required<PX.Objects.IN.INSite.siteCD>>>>.Config>.SelectSingleBound(graph, (object[]) null, new object[1]
    {
      (object) entityValueField1.Value
    }));
    if (inSite == null)
      throw new PXException("Site '{0}' is missing.", new object[1]
      {
        (object) entityValueField1.Value
      });
    foreach (PXResult<INItemSite, PX.Objects.IN.INSite, INSiteStatusSummary> pxResult in view.SelectMulti(Array.Empty<object>()).Cast<PXResult<INItemSite, PX.Objects.IN.INSite, INSiteStatusSummary>>().ToArray<PXResult<INItemSite, PX.Objects.IN.INSite, INSiteStatusSummary>>())
    {
      int? siteId1 = PXResult<INItemSite, PX.Objects.IN.INSite, INSiteStatusSummary>.op_Implicit(pxResult).SiteID;
      int? siteId2 = inSite.SiteID;
      if (siteId1.GetValueOrDefault() == siteId2.GetValueOrDefault() & siteId1.HasValue == siteId2.HasValue)
        return;
    }
    INItemSite instance = (INItemSite) cache.CreateInstance();
    PXCache cach = graph.Caches[typeof (PX.Objects.IN.InventoryItem)];
    if (cach != null && cach.Current != null)
      instance.InventoryID = ((PX.Objects.IN.InventoryItem) cach.Current).InventoryID;
    instance.SiteID = inSite.SiteID;
    if (entityValueField2 != null)
      cache.SetValueExt((object) instance, "DfltShipLocationID", (object) entityValueField2.Value);
    if (entityValueField3 != null)
      cache.SetValueExt((object) instance, "DfltReceiptLocationID", (object) entityValueField3.Value);
    cache.Insert((object) instance);
  }

  [FieldsProcessed(new string[] {"InventoryID", "Location", "LotSerialNbr", "Subitem"})]
  protected void PhysicalInventoryCountDetail_Insert(
    PXGraph graph,
    EntityImpl entity,
    EntityImpl targetEntity)
  {
    EntityValueField entityValueField1 = ((IEnumerable<EntityField>) targetEntity.Fields).Single<EntityField>((Func<EntityField, bool>) (f => f.Name == "InventoryID")) as EntityValueField;
    EntityValueField entityValueField2 = ((IEnumerable<EntityField>) targetEntity.Fields).Single<EntityField>((Func<EntityField, bool>) (f => f.Name == "Location")) as EntityValueField;
    EntityValueField entityValueField3 = ((IEnumerable<EntityField>) targetEntity.Fields).FirstOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "LotSerialNbr")) as EntityValueField;
    EntityValueField entityValueField4 = ((IEnumerable<EntityField>) targetEntity.Fields).FirstOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "Subitem")) as EntityValueField;
    INPICountEntry inpiCountEntry = (INPICountEntry) graph;
    INBarCodeItemLookup<INBarCodeItem> addByBarCode = inpiCountEntry.AddByBarCode;
    PXCache cache = ((PXSelectBase) addByBarCode).Cache;
    cache.Remove((object) ((PXSelectBase<INBarCodeItem>) addByBarCode).Current);
    cache.Insert((object) new INBarCodeItem());
    INBarCodeItem current = (INBarCodeItem) cache.Current;
    cache.SetValueExt((object) current, "InventoryID", (object) entityValueField1.Value);
    cache.SetValueExt((object) current, "LocationID", (object) entityValueField2.Value);
    if (entityValueField3 != null)
      cache.SetValueExt((object) current, "LotSerialNbr", (object) entityValueField3.Value);
    if (entityValueField4 != null)
      cache.SetValueExt((object) current, "SubItemID", (object) entityValueField4.Value);
    cache.Update((object) current);
    ((PXGraph) inpiCountEntry).Actions["AddLine2"].Press();
  }

  private static EntityValueField GetEntityField(EntityImpl targetEntity, string fieldName)
  {
    return ((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == fieldName)) as EntityValueField;
  }

  protected static Decimal? ConvertToNullableDecimal(EntityValueField field)
  {
    return field == null ? new Decimal?() : new Decimal?(Convert.ToDecimal(field.Value));
  }

  protected static int? ConvertToNullableInt(EntityValueField field)
  {
    return field == null ? new int?() : new int?(Convert.ToInt32(field.Value));
  }

  protected static bool? ConvertToNullableBool(EntityValueField field)
  {
    return field == null ? new bool?() : new bool?(Convert.ToBoolean(field.Value));
  }

  private static void FillInvoiceRowFromEntiry(
    SOInvoiceEntry graph,
    EntityImpl targetEntity,
    PX.Objects.AR.ARTran row)
  {
    row.TranDesc = DefaultEndpointImpl.GetEntityField(targetEntity, "TransactionDescr")?.Value;
    row.UnitPrice = DefaultEndpointImpl.ConvertToNullableDecimal(DefaultEndpointImpl.GetEntityField(targetEntity, "UnitPrice"));
    row.LineNbr = DefaultEndpointImpl.ConvertToNullableInt(DefaultEndpointImpl.GetEntityField(targetEntity, "LineNbr"));
    row.Qty = DefaultEndpointImpl.ConvertToNullableDecimal(DefaultEndpointImpl.GetEntityField(targetEntity, "Qty"));
    row.CuryTranAmt = DefaultEndpointImpl.ConvertToNullableDecimal(DefaultEndpointImpl.GetEntityField(targetEntity, "Amount"));
    row.UOM = DefaultEndpointImpl.GetEntityField(targetEntity, "UOM")?.Value;
    row.DiscAmt = DefaultEndpointImpl.ConvertToNullableDecimal(DefaultEndpointImpl.GetEntityField(targetEntity, "DiscountAmount"));
    row.DiscPct = DefaultEndpointImpl.ConvertToNullableDecimal(DefaultEndpointImpl.GetEntityField(targetEntity, "DiscountPercent"));
    row.LotSerialNbr = DefaultEndpointImpl.GetEntityField(targetEntity, "LotSerialNbr")?.Value;
    row.SOOrderLineNbr = DefaultEndpointImpl.ConvertToNullableInt(DefaultEndpointImpl.GetEntityField(targetEntity, "OrderLineNbr"));
    row.TaxCategoryID = DefaultEndpointImpl.GetEntityField(targetEntity, "TaxCategory")?.Value;
    row.OrigInvoiceType = DefaultEndpointImpl.GetEntityField(targetEntity, "OrigInvType")?.Value;
    row.OrigInvoiceNbr = DefaultEndpointImpl.GetEntityField(targetEntity, "OrigInvNbr")?.Value;
    row.OrigInvoiceLineNbr = DefaultEndpointImpl.ConvertToNullableInt(DefaultEndpointImpl.GetEntityField(targetEntity, "OrigInvLineNbr"));
    string str1 = DefaultEndpointImpl.GetEntityField(targetEntity, "InventoryID")?.Value;
    string str2 = DefaultEndpointImpl.GetEntityField(targetEntity, "BranchID")?.Value;
    EntityValueField entityField = DefaultEndpointImpl.GetEntityField(targetEntity, "ExpirationDate");
    DateTime dateTime = entityField != null ? Convert.ToDateTime(entityField.Value) : new DateTime();
    string str3 = DefaultEndpointImpl.GetEntityField(targetEntity, "Location")?.Value;
    string str4 = DefaultEndpointImpl.GetEntityField(targetEntity, "WarehouseID")?.Value;
    if (str1 != null)
      row.InventoryID = (int?) ((PXResult) ((IQueryable<PXResult<PX.Objects.IN.InventoryItem>>) PXSelectBase<PX.Objects.IN.InventoryItem, PXSelect<PX.Objects.IN.InventoryItem, Where<PX.Objects.IN.InventoryItem.inventoryCD, Equal<Required<PX.Objects.IN.InventoryItem.inventoryCD>>>>.Config>.Select((PXGraph) graph, new object[1]
      {
        (object) str1
      })).FirstOrDefault<PXResult<PX.Objects.IN.InventoryItem>>())?.GetItem<PX.Objects.IN.InventoryItem>().InventoryID;
    if (str2 != null)
      row.BranchID = (int?) ((PXResult) ((IQueryable<PXResult<PX.Objects.GL.Branch>>) PXSelectBase<PX.Objects.GL.Branch, PXSelect<PX.Objects.GL.Branch, Where<PX.Objects.GL.Branch.branchCD, Equal<Required<PX.Objects.GL.Branch.branchCD>>>>.Config>.Select((PXGraph) graph, new object[1]
      {
        (object) str2
      })).FirstOrDefault<PXResult<PX.Objects.GL.Branch>>())?.GetItem<PX.Objects.GL.Branch>().BranchID;
    if (dateTime != new DateTime())
      row.ExpireDate = new DateTime?(dateTime);
    if (str4 == null)
      return;
    row.SiteID = (int?) ((PXResult) ((IQueryable<PXResult<PX.Objects.IN.INSite>>) PXSelectBase<PX.Objects.IN.INSite, PXSelect<PX.Objects.IN.INSite, Where<PX.Objects.IN.INSite.siteCD, Equal<Required<PX.Objects.IN.INSite.siteCD>>>>.Config>.Select((PXGraph) graph, new object[1]
    {
      (object) str4
    })).FirstOrDefault<PXResult<PX.Objects.IN.INSite>>())?.GetItem<PX.Objects.IN.INSite>().SiteID;
    if (str3 == null)
      return;
    row.LocationID = (int?) ((PXResult) ((IQueryable<PXResult<INLocation>>) PXSelectBase<INLocation, PXSelect<INLocation, Where<INLocation.siteID, Equal<Required<INLocation.siteID>>, And<INLocation.locationCD, Equal<Required<INLocation.locationCD>>>>>.Config>.Select((PXGraph) graph, new object[2]
    {
      (object) row.SiteID,
      (object) str3
    })).FirstOrDefault<PXResult<INLocation>>())?.GetItem<INLocation>().LocationID;
  }

  private protected void AttributeBase_Insert(
    PXGraph graph,
    EntityImpl entity,
    EntityImpl targetEntity,
    string attributeIdFieldName)
  {
    EntityValueField entityValueField1 = ((IEnumerable<EntityField>) targetEntity.Fields).Single<EntityField>((Func<EntityField, bool>) (f => f.Name == attributeIdFieldName)) as EntityValueField;
    EntityValueField entityValueField2 = ((IEnumerable<EntityField>) targetEntity.Fields).Single<EntityField>((Func<EntityField, bool>) (f => f.Name == "Value")) as EntityValueField;
    PXView view = graph.Views["Answers"];
    PXCache cache = view.Cache;
    foreach (object obj in view.SelectMulti(Array.Empty<object>()).OrderBy<object, object>((Func<object, object>) (row => (cache.GetStateExt(row, "Order") as PXFieldState).Value)).ToArray<object>())
    {
      string str = (cache.GetStateExt(obj, "AttributeID") as PXFieldState).Value.ToString();
      if (StringExtensions.OrdinalEquals(entityValueField1.Value, str))
      {
        if (cache.GetStateExt<CSAnswers.value>(obj) is PXStringState stateExt)
        {
          if (((PXFieldState) stateExt).Enabled)
          {
            if (stateExt.ValueLabelDic != null)
            {
              foreach (KeyValuePair<string, string> keyValuePair in stateExt.ValueLabelDic)
              {
                if (keyValuePair.Value == entityValueField2.Value)
                {
                  entityValueField2.Value = keyValuePair.Key;
                  break;
                }
              }
            }
          }
          else
            continue;
        }
        cache.SetValueExt(obj, "Value", (object) entityValueField2.Value);
        cache.Update(obj);
        break;
      }
    }
  }

  [FieldsProcessed(new string[] {"POLineNbr", "POOrderType", "POOrderNbr", "TransferOrderType", "TransferOrderNbr", "TransferShipmentNbr"})]
  protected virtual void PurchaseReceiptDetail_Insert(
    PXGraph graph,
    EntityImpl entity,
    EntityImpl targetEntity)
  {
    POReceiptEntry receiptEntry = (POReceiptEntry) graph;
    AddTransferDialog extension = ((PXGraph) receiptEntry).GetExtension<AddTransferDialog>();
    if (((PXSelectBase<PX.Objects.PO.POReceipt>) receiptEntry.Document).Current == null || ((PXSelectBase<PX.Objects.PO.POReceipt>) receiptEntry.Document).Current.Released.GetValueOrDefault())
      return;
    PXCache cache = ((PXSelectBase) receiptEntry.transactions).Cache;
    bool flag1 = ((IEnumerable<EntityImpl>) ((((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => string.Equals(f.Name, "Allocations"))) is EntityListField entityListField ? entityListField.Value : (EntityImpl[]) null) ?? new EntityImpl[0])).Any<EntityImpl>((Func<EntityImpl, bool>) (a => a.Fields != null && a.Fields.Length != 0));
    if (((PXSelectBase<PX.Objects.PO.POReceipt>) receiptEntry.Document).Current.ReceiptType == "RX")
    {
      EntityValueField sOOrderType = ((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "TransferOrderType")) as EntityValueField;
      EntityValueField sOOrderNbr = ((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "TransferOrderNbr")) as EntityValueField;
      EntityValueField entityValueField = ((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "TransferOrderLineNbr")) as EntityValueField;
      EntityValueField sOShipmentNbr = ((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "TransferShipmentNbr")) as EntityValueField;
      if (sOOrderType != null && sOOrderNbr != null && sOShipmentNbr != null)
      {
        if (entityValueField != null)
        {
          ((PXSelectBase) receiptEntry.filter).Cache.Remove((object) ((PXSelectBase<POReceiptEntry.POOrderFilter>) receiptEntry.filter).Current);
          ((PXSelectBase) receiptEntry.filter).Cache.Insert((object) new POReceiptEntry.POOrderFilter());
          POReceiptEntry.POOrderFilter current = ((PXSelectBase<POReceiptEntry.POOrderFilter>) receiptEntry.filter).Current;
          ((PXSelectBase) receiptEntry.filter).Cache.SetValueExt((object) current, "SOOrderType", (object) sOOrderType.Value);
          ((PXSelectBase) receiptEntry.filter).Cache.SetValueExt((object) current, "SOOrderNbr", (object) sOOrderNbr.Value);
          POReceiptEntry.POOrderFilter poOrderFilter = ((PXSelectBase<POReceiptEntry.POOrderFilter>) receiptEntry.filter).Update(current);
          Dictionary<string, string> errors = PXUIFieldAttribute.GetErrors(((PXSelectBase) receiptEntry.filter).Cache, (object) poOrderFilter, Array.Empty<PXErrorLevel>());
          if (errors.Count<KeyValuePair<string, string>>() > 0)
            throw new PXException(string.Join(";", errors.Select<KeyValuePair<string, string>, string>((Func<KeyValuePair<string, string>, string>) (x => $"{x.Key}={x.Value}"))));
          INTran inTran1 = (INTran) null;
          foreach (PXResult<INTran> pxResult in ((PXSelectBase<INTran>) receiptEntry.intranSelection).Select(Array.Empty<object>()))
          {
            INTran inTran2 = PXResult<INTran>.op_Implicit(pxResult);
            if (inTran2.SOOrderType == sOOrderType.Value && inTran2.SOOrderNbr == sOOrderNbr.Value && inTran2.SOOrderLineNbr.ToString() == entityValueField.Value && inTran2.SOShipmentNbr == sOShipmentNbr.Value)
            {
              inTran1 = inTran2;
              break;
            }
          }
          if (inTran1 == null)
            throw new PXException("The transfer order line was not found.");
          if (PXResultset<PX.Objects.PO.POReceiptLine>.op_Implicit(PXSelectBase<PX.Objects.PO.POReceiptLine, PXSelect<PX.Objects.PO.POReceiptLine, Where<PX.Objects.PO.POReceiptLine.receiptType, Equal<Required<PX.Objects.PO.POReceipt.receiptType>>, And<PX.Objects.PO.POReceiptLine.receiptNbr, Equal<Required<PX.Objects.PO.POReceipt.receiptNbr>>, And<PX.Objects.PO.POReceiptLine.origDocType, Equal<Required<PX.Objects.PO.POReceiptLine.origDocType>>, And<PX.Objects.PO.POReceiptLine.origRefNbr, Equal<Required<PX.Objects.PO.POReceiptLine.origRefNbr>>, And<PX.Objects.PO.POReceiptLine.origLineNbr, Equal<Required<PX.Objects.PO.POReceiptLine.origLineNbr>>>>>>>>.Config>.Select((PXGraph) receiptEntry, new object[5]
          {
            (object) ((PXSelectBase<PX.Objects.PO.POReceipt>) receiptEntry.Document).Current.ReceiptType,
            (object) ((PXSelectBase<PX.Objects.PO.POReceipt>) receiptEntry.Document).Current.ReceiptNbr,
            (object) inTran1.DocType,
            (object) inTran1.RefNbr,
            (object) inTran1.LineNbr
          })) == null)
            extension.AddTransferLine(inTran1);
        }
        else
        {
          ((PXSelectBase) receiptEntry.filter).Cache.Remove((object) ((PXSelectBase<POReceiptEntry.POOrderFilter>) receiptEntry.filter).Current);
          ((PXSelectBase) receiptEntry.filter).Cache.Insert((object) new POReceiptEntry.POOrderFilter());
          ParameterExpression parameterExpression;
          // ISSUE: method reference
          PX.Objects.SO.SOOrderShipment soOrderShipment = ((IQueryable<PXResult<PX.Objects.SO.SOOrderShipment>>) ((PXSelectBase<PX.Objects.SO.SOOrderShipment>) extension.openTransfers).Select(Array.Empty<object>())).Select<PXResult<PX.Objects.SO.SOOrderShipment>, PX.Objects.SO.SOOrderShipment>(Expression.Lambda<Func<PXResult<PX.Objects.SO.SOOrderShipment>, PX.Objects.SO.SOOrderShipment>>((Expression) Expression.Call(r, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (PXResult.GetItem)), Array.Empty<Expression>()), parameterExpression)).FirstOrDefault<PX.Objects.SO.SOOrderShipment>((Expression<Func<PX.Objects.SO.SOOrderShipment, bool>>) (o => o.OrderType == sOOrderType.Value && o.OrderNbr == sOOrderNbr.Value && o.ShipmentNbr == sOShipmentNbr.Value));
          if (soOrderShipment == null)
            throw new PXException("The transfer order was not found.");
          soOrderShipment.Selected = new bool?(true);
          ((PXSelectBase<PX.Objects.SO.SOOrderShipment>) extension.openTransfers).Update(soOrderShipment);
          ((PXGraph) receiptEntry).Actions["AddTransfer2"].Press();
          return;
        }
      }
    }
    else
    {
      EntityValueField lineNbr = ((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "POLineNbr")) as EntityValueField;
      EntityValueField orderType = ((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "POOrderType")) as EntityValueField;
      EntityValueField orderNbr = ((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "POOrderNbr")) as EntityValueField;
      EntityValueField entityValueField1 = ((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "UnitCost")) as EntityValueField;
      EntityValueField entityValueField2 = ((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "ExtendedCost")) as EntityValueField;
      bool flag2 = ((PXSelectBase<PX.Objects.PO.POReceipt>) receiptEntry.Document).Current.ReceiptType == "RT";
      bool flag3 = ((orderNbr == null ? 0 : (orderType != null ? 1 : 0)) & (flag2 ? 1 : 0)) != 0;
      int num = !flag3 ? 0 : (lineNbr != null ? 1 : 0);
      bool flag4 = entityValueField1 != null || entityValueField2 != null;
      if (flag2 && !flag3 && (orderType != null || orderNbr != null))
        throw new PXException("PO type and PO number must be filled in for the system to add the purchase order to the details.");
      if (num != 0)
      {
        this.FillInAddPOFilter(receiptEntry, orderType, orderNbr);
        ParameterExpression parameterExpression;
        // ISSUE: method reference
        POReceiptEntry.POLineS poLineS = ((IQueryable<PXResult<POReceiptEntry.POLineS>>) ((PXSelectBase<POReceiptEntry.POLineS>) receiptEntry.poLinesSelection).Select(Array.Empty<object>())).Select<PXResult<POReceiptEntry.POLineS>, POReceiptEntry.POLineS>(Expression.Lambda<Func<PXResult<POReceiptEntry.POLineS>, POReceiptEntry.POLineS>>((Expression) Expression.Call(r, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (PXResult.GetItem)), Array.Empty<Expression>()), parameterExpression)).FirstOrDefault<POReceiptEntry.POLineS>((Expression<Func<POReceiptEntry.POLineS, bool>>) (o => o.LineNbr == (int?) int.Parse(lineNbr.Value)));
        if (poLineS == null)
          throw new PXException("The purchase order line was not found.");
        poLineS.Selected = new bool?(true);
        ((PXSelectBase<POReceiptEntry.POLineS>) receiptEntry.poLinesSelection).Update(poLineS);
        ((PXGraph) receiptEntry).Actions["AddPOOrderLine2"].Press();
        if (flag4 && cache.Current != null)
          cache.SetValueExt<PX.Objects.PO.POReceiptLine.manualPrice>(cache.Current, (object) true);
      }
      else if (flag3)
      {
        this.FillInAddPOFilter(receiptEntry, orderType, orderNbr);
        ParameterExpression parameterExpression;
        // ISSUE: method reference
        POReceiptEntry.POOrderS poOrderS = ((IQueryable<PXResult<POReceiptEntry.POOrderS>>) ((PXSelectBase<POReceiptEntry.POOrderS>) receiptEntry.openOrders).Select(Array.Empty<object>())).Select<PXResult<POReceiptEntry.POOrderS>, POReceiptEntry.POOrderS>(Expression.Lambda<Func<PXResult<POReceiptEntry.POOrderS>, POReceiptEntry.POOrderS>>((Expression) Expression.Call(r, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (PXResult.GetItem)), Array.Empty<Expression>()), parameterExpression)).FirstOrDefault<POReceiptEntry.POOrderS>((Expression<Func<POReceiptEntry.POOrderS, bool>>) (o => o.OrderNbr == orderNbr.Value));
        if (poOrderS == null)
          throw new PXException("The {0} {1} purchase order was not found.", new object[2]
          {
            (object) orderType.Value,
            (object) orderNbr.Value
          });
        poOrderS.Selected = new bool?(true);
        ((PXSelectBase<POReceiptEntry.POOrderS>) receiptEntry.openOrders).Update(poOrderS);
        ((PXGraph) receiptEntry).Actions["AddPOOrder2"].Press();
        if (!(((IEnumerable<EntityField>) targetEntity.Fields).FirstOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "ExpirationDate")) is EntityValueField entityValueField3) || entityValueField3.Value == null)
          return;
        IEnumerator enumerator = ((PXSelectBase) receiptEntry.transactions).Cache.Inserted.GetEnumerator();
        try
        {
          while (enumerator.MoveNext())
          {
            PX.Objects.PO.POReceiptLine current = (PX.Objects.PO.POReceiptLine) enumerator.Current;
            if (((PXSelectBase) receiptEntry.transactions).Cache.GetStateExt((object) current, "ExpireDate") is PXDateState stateExt && ((PXFieldState) stateExt).Enabled && current.PONbr == orderNbr.Value)
              ((PXSelectBase) receiptEntry.transactions).Cache.SetValueExt((object) current, "ExpireDate", (object) entityValueField3.Value);
          }
          return;
        }
        finally
        {
          if (enumerator is IDisposable disposable)
            disposable.Dispose();
        }
      }
      else
      {
        cache.Current = cache.Insert();
        PX.Objects.PO.POReceiptLine receiptLineCurrent = cache.Current as PX.Objects.PO.POReceiptLine;
        if (flag4 && receiptLineCurrent != null)
          cache.SetValueExt<PX.Objects.PO.POReceiptLine.manualPrice>((object) receiptLineCurrent, (object) true);
        if (flag1)
        {
          if (cache.Current == null)
            throw new InvalidOperationException("Cannot insert Purchase Receipt detail.");
          DefaultEndpointImpl.SetFieldsNeedToInsertAllocations(targetEntity, receiptEntry, receiptLineCurrent);
          EntityValueField entityValueField4 = ((IEnumerable<EntityField>) targetEntity.Fields).FirstOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "ReceiptQty")) as EntityValueField;
          EntityValueField entityValueField5 = ((IEnumerable<EntityField>) targetEntity.Fields).FirstOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "ExpirationDate")) as EntityValueField;
          if (entityValueField4 != null)
          {
            receiptLineCurrent.ReceiptQty = new Decimal?(Decimal.Parse(entityValueField4.Value));
            receiptLineCurrent = ((PXSelectBase<PX.Objects.PO.POReceiptLine>) receiptEntry.transactions).Update(receiptLineCurrent);
          }
          if (entityValueField5 != null)
            ((PXSelectBase) receiptEntry.transactions).Cache.SetValueExt((object) receiptLineCurrent, "ExpireDate", (object) entityValueField5.Value);
        }
      }
    }
    if (!flag1 || cache.Current == null)
      return;
    foreach (POReceiptLineSplit receiptLineSplit in ((PXSelectBase) receiptEntry.splits).Cache.Inserted)
    {
      int? lineNbr1 = receiptLineSplit.LineNbr;
      int? lineNbr2 = (cache.Current as PX.Objects.PO.POReceiptLine).LineNbr;
      if (lineNbr1.GetValueOrDefault() == lineNbr2.GetValueOrDefault() & lineNbr1.HasValue == lineNbr2.HasValue)
        ((PXSelectBase<POReceiptLineSplit>) receiptEntry.splits).Delete(receiptLineSplit);
    }
  }

  public static void SetFieldsNeedToInsertAllocations(
    EntityImpl targetEntity,
    POReceiptEntry receiptEntry,
    PX.Objects.PO.POReceiptLine receiptLineCurrent)
  {
    EntityValueField entityValueField1 = ((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "InventoryID")) as EntityValueField;
    EntityValueField entityValueField2 = ((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "Warehouse")) as EntityValueField;
    EntityValueField entityValueField3 = ((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "Location")) as EntityValueField;
    EntityValueField entityValueField4 = ((IEnumerable<EntityField>) targetEntity.Fields).FirstOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "LotSerialNbr")) as EntityValueField;
    EntityValueField entityValueField5 = ((IEnumerable<EntityField>) targetEntity.Fields).FirstOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "Subitem")) as EntityValueField;
    if (entityValueField1 != null)
      ((PXSelectBase) receiptEntry.transactions).Cache.SetValueExt((object) receiptLineCurrent, "InventoryID", (object) entityValueField1.Value);
    if (entityValueField2 != null)
      ((PXSelectBase) receiptEntry.transactions).Cache.SetValueExt((object) receiptLineCurrent, "SiteID", (object) entityValueField2.Value);
    if (entityValueField3 != null)
      ((PXSelectBase) receiptEntry.transactions).Cache.SetValueExt((object) receiptLineCurrent, "LocationID", (object) entityValueField3.Value);
    if (entityValueField4 != null)
      ((PXSelectBase) receiptEntry.transactions).Cache.SetValueExt((object) receiptLineCurrent, "LotSerialNbr", (object) entityValueField4.Value);
    if (entityValueField5 == null)
      return;
    ((PXSelectBase) receiptEntry.transactions).Cache.SetValueExt((object) receiptLineCurrent, "SubItemID", (object) entityValueField5.Value);
  }

  protected virtual void FillInAddPOFilter(
    POReceiptEntry receiptEntry,
    EntityValueField orderType,
    EntityValueField orderNbr)
  {
    ((PXSelectBase) receiptEntry.filter).Cache.Remove((object) ((PXSelectBase<POReceiptEntry.POOrderFilter>) receiptEntry.filter).Current);
    ((PXSelectBase) receiptEntry.filter).Cache.Insert((object) new POReceiptEntry.POOrderFilter());
    POReceiptEntry.POOrderFilter current = ((PXSelectBase<POReceiptEntry.POOrderFilter>) receiptEntry.filter).Current;
    if (((PXSelectBase) receiptEntry.filter).Cache.GetStateExt((object) current, "OrderType") is PXStringState stateExt && ((IEnumerable<string>) stateExt.AllowedLabels).Contains<string>(orderType.Value))
      orderType.Value = stateExt.ValueLabelDic.Single<KeyValuePair<string, string>>((Func<KeyValuePair<string, string>, bool>) (p => p.Value == orderType.Value)).Key;
    ((PXSelectBase) receiptEntry.filter).Cache.SetValueExt((object) current, "OrderType", (object) orderType.Value);
    ((PXSelectBase) receiptEntry.filter).Cache.SetValueExt((object) current, "OrderNbr", (object) orderNbr.Value);
    POReceiptEntry.POOrderFilter poOrderFilter = ((PXSelectBase<POReceiptEntry.POOrderFilter>) receiptEntry.filter).Update(current);
    Dictionary<string, string> errors = PXUIFieldAttribute.GetErrors(((PXSelectBase) receiptEntry.filter).Cache, (object) poOrderFilter, Array.Empty<PXErrorLevel>());
    if (errors.Count<KeyValuePair<string, string>>() > 0)
      throw new PXException(string.Join(";", errors.Select<KeyValuePair<string, string>, string>((Func<KeyValuePair<string, string>, string>) (x => $"{x.Key}={x.Value}"))));
  }

  /// <summary>Adds all lines of a given order to the shipment.</summary>
  protected void Action_AddOrder(PXGraph graph, ActionImpl action)
  {
    SOOrderExtension extension = graph.GetExtension<SOOrderExtension>();
    ((PXSelectBase<AddSOFilter>) extension.addsofilter).Current.OrderType = ((EntityValueField) ((IEnumerable<EntityField>) action.Fields).Single<EntityField>((Func<EntityField, bool>) (f => f.Name == "OrderType"))).Value;
    ((PXSelectBase<AddSOFilter>) extension.addsofilter).Current.OrderNbr = ((EntityValueField) ((IEnumerable<EntityField>) action.Fields).Single<EntityField>((Func<EntityField, bool>) (f => f.Name == "OrderNbr"))).Value;
    ((PXSelectBase<AddSOFilter>) extension.addsofilter).Update(((PXSelectBase<AddSOFilter>) extension.addsofilter).Current);
    foreach (PXResult<SOShipmentPlan> pxResult in ((PXSelectBase<SOShipmentPlan>) extension.soshipmentplan).Select(Array.Empty<object>()))
    {
      SOShipmentPlan soShipmentPlan = PXResult<SOShipmentPlan>.op_Implicit(pxResult);
      this.SelectLine(extension, soShipmentPlan);
    }
    ((PXAction) extension.addSO).Press();
  }

  /// <summary>
  /// Handles creation of document details in the Bills and Adjustments (AP301000) screen
  /// for cases when po entities are specified
  /// using the <see cref="F:PX.Objects.PO.GraphExtensions.APInvoiceSmartPanel.AddPOOrderExtension.addPOOrder2">Add PO action</see>,
  /// the <see cref="F:PX.Objects.PO.GraphExtensions.APInvoiceSmartPanel.AddPOReceiptExtension.addPOReceipt2">Add PO Receipt action</see>.
  /// </summary>
  [FieldsProcessed(new string[] {"POOrderType", "POOrderNbr", "POLine", "POReceiptType", "POReceiptNbr", "POReceiptLine"})]
  protected virtual void BillDetail_Insert(
    PXGraph graph,
    EntityImpl entity,
    EntityImpl targetEntity)
  {
    APInvoiceEntry invoiceEntry = (APInvoiceEntry) graph;
    EntityValueField orderType = ((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "POOrderType")) as EntityValueField;
    EntityValueField orderNbr = ((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "POOrderNbr")) as EntityValueField;
    EntityValueField orderLineNbr = ((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "POLine")) as EntityValueField;
    EntityValueField receiptType = ((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "POReceiptType")) as EntityValueField;
    EntityValueField receiptNbr = ((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "POReceiptNbr")) as EntityValueField;
    EntityValueField receiptLineNbr = ((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "POReceiptLine")) as EntityValueField;
    if (orderType != null && orderNbr != null)
    {
      if (orderLineNbr != null)
        DefaultEndpointImpl.AddPOOrderLineToBill(invoiceEntry, orderType, orderNbr, orderLineNbr);
      else
        DefaultEndpointImpl.AddPOOrderToBill(invoiceEntry, orderType, orderNbr);
    }
    else if (receiptNbr != null)
    {
      if (receiptType == null)
        receiptType = new EntityValueField()
        {
          Value = ((PXSelectBase<PX.Objects.AP.APInvoice>) invoiceEntry.Document).Current?.DocType == "INV" ? "RT" : "RN"
        };
      else if (((PXSelectBase) invoiceEntry.Transactions).Cache.GetStateExt<PX.Objects.AP.APTran.receiptType>((object) new PX.Objects.AP.APTran()) is PXStringState stateExt && ((IEnumerable<string>) stateExt.AllowedLabels).Contains<string>(receiptType.Value))
        receiptType.Value = stateExt.ValueLabelDic.Single<KeyValuePair<string, string>>((Func<KeyValuePair<string, string>, bool>) (p => p.Value == receiptType.Value)).Key;
      if (receiptLineNbr != null)
        DefaultEndpointImpl.AddPOReceiptLineToBill(invoiceEntry, receiptType, receiptNbr, receiptLineNbr);
      else
        DefaultEndpointImpl.AddPOReceiptToBill(invoiceEntry, receiptType, receiptNbr);
    }
    else
    {
      if (orderNbr != null || orderType != null)
        throw new PXException("Both POOrderType and POOrderNumber must be provided to add a Purchase Order to details.");
      PXCache cache = ((PXSelectBase) invoiceEntry.Transactions).Cache;
      cache.Current = cache.Insert(cache.CreateInstance());
    }
  }

  [FieldsProcessed(new string[] {"Name", "Description", "Value"})]
  protected void CustomerPaymentMethodDetail_Insert(
    PXGraph graph,
    EntityImpl entity,
    EntityImpl targetEntity)
  {
    CustomerPaymentMethodMaint paymentMethodMaint = (CustomerPaymentMethodMaint) graph;
    EntityValueField entityValueField1 = (EntityValueField) ((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => StringExtensions.OrdinalEquals(f.Name, "Name")));
    EntityValueField entityValueField2 = (EntityValueField) ((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => StringExtensions.OrdinalEquals(f.Name, "Description")));
    EntityValueField entityValueField3 = (EntityValueField) ((IEnumerable<EntityField>) targetEntity.Fields).Single<EntityField>((Func<EntityField, bool>) (f => StringExtensions.OrdinalEquals(f.Name, "Value")));
    PXCache cache = ((PXSelectBase) paymentMethodMaint.Details).Cache;
    foreach (PXResult<CustomerPaymentMethodDetail> pxResult in ((PXSelectBase<CustomerPaymentMethodDetail>) paymentMethodMaint.Details).Select(Array.Empty<object>()))
    {
      CustomerPaymentMethodDetail paymentMethodDetail1 = PXResult<CustomerPaymentMethodDetail>.op_Implicit(pxResult);
      PaymentMethodDetail paymentMethodDetail2 = PXSelectorAttribute.Select(cache, (object) paymentMethodDetail1, "DetailID") as PaymentMethodDetail;
      if (entityValueField1 != null && (paymentMethodDetail2.Descr == entityValueField1.Value || paymentMethodDetail1.DetailID == entityValueField1.Value) || entityValueField2 != null && (paymentMethodDetail2.Descr == entityValueField2.Value || paymentMethodDetail2.DetailID == entityValueField2.Value))
      {
        cache.SetValueExt((object) paymentMethodDetail1, "Value", (object) entityValueField3.Value);
        ((PXSelectBase<CustomerPaymentMethodDetail>) paymentMethodMaint.Details).Update(paymentMethodDetail1);
        break;
      }
    }
  }

  [FieldsProcessed(new string[] {"ParentCategoryID", "Description"})]
  protected void ItemSalesCategory_Insert(
    PXGraph graph,
    EntityImpl entity,
    EntityImpl targetEntity)
  {
    INCategoryMaint inCategoryMaint = (INCategoryMaint) graph;
    ((PXSelectBase<INCategory>) inCategoryMaint.Folders).Current = ((PXSelectBase<INCategory>) inCategoryMaint.Folders).SelectSingle(Array.Empty<object>());
    ((PXGraph) inCategoryMaint).Actions["AddCategory"].Press();
    EntityValueField entityValueField1 = ((IEnumerable<EntityField>) entity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name.Equals("ParentCategoryID", StringComparison.OrdinalIgnoreCase))) as EntityValueField;
    EntityValueField entityValueField2 = ((IEnumerable<EntityField>) entity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name.Equals("Description", StringComparison.OrdinalIgnoreCase))) as EntityValueField;
    INCategory activeRow = ((PXSelectBase) inCategoryMaint.Folders).Cache.ActiveRow as INCategory;
    PXCache cache = ((PXSelectBase) inCategoryMaint.Folders).Cache;
    if (entityValueField1 != null && !string.IsNullOrEmpty(entityValueField1.Value))
      cache.SetValueExt((object) activeRow, "ParentID", (object) int.Parse(entityValueField1.Value));
    if (entityValueField2 != null && !string.IsNullOrEmpty(entityValueField2.Value))
      cache.SetValueExt((object) activeRow, "Description", (object) entityValueField2.Value);
    ((PXSelectBase) inCategoryMaint.Folders).Cache.Current = (object) activeRow;
  }

  [FieldsProcessed(new string[] {"TypeID", "Description", "WarehouseID"})]
  protected void PhysicalInventoryReview_Insert(
    PXGraph graph,
    EntityImpl entity,
    EntityImpl targetEntity)
  {
    INPIReview inpiReview = (INPIReview) graph;
    EntityValueField entityValueField1 = ((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "TypeID")) as EntityValueField;
    EntityValueField entityValueField2 = ((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "Description")) as EntityValueField;
    EntityValueField entityValueField3 = ((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "WarehouseID")) as EntityValueField;
    PXCache cache = ((PXSelectBase) inpiReview.GeneratorSettings).Cache;
    cache.Clear();
    cache.Insert((object) new PIGeneratorSettings());
    Dictionary<string, object> dictionary1 = new Dictionary<string, object>();
    if (entityValueField1 != null)
      dictionary1.Add("PIClassID", (object) entityValueField1.Value);
    ((PXGraph) inpiReview).ExecuteUpdate(((PXSelectBase) inpiReview.GeneratorSettings).View.Name, (IDictionary) new Dictionary<string, object>(), (IDictionary) dictionary1, Array.Empty<object>());
    Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
    if (entityValueField2 != null)
      dictionary2.Add("Descr", (object) entityValueField2.Value);
    if (entityValueField3 != null)
      dictionary2.Add("SiteID", (object) entityValueField3.Value);
    ((PXGraph) inpiReview).ExecuteUpdate(((PXSelectBase) inpiReview.GeneratorSettings).View.Name, (IDictionary) new Dictionary<string, object>(), (IDictionary) dictionary2, Array.Empty<object>());
    ((PXSelectBase) inpiReview.GeneratorSettings).View.SetAnswer((string) null, (WebDialogResult) 1);
    ((PXAction) inpiReview.Insert).Press();
  }

  protected void ItemSalesCategory_Delete(
    PXGraph graph,
    EntityImpl entity,
    EntityImpl targetEntity)
  {
    INCategoryMaint inCategoryMaint = (INCategoryMaint) graph;
    ((PXGraph) inCategoryMaint).Actions["DeleteCategory"].Press();
    ((PXGraph) inCategoryMaint).Actions["Save"].Press();
  }

  [FieldsProcessed(new string[] {"Description", "ParentCategoryID"})]
  protected void ItemSalesCategory_Update(
    PXGraph graph,
    EntityImpl entity,
    EntityImpl targetEntity)
  {
    INCategoryMaint inCategoryMaint = (INCategoryMaint) graph;
    INCategory current = ((PXSelectBase) inCategoryMaint.Folders).Cache.Current as INCategory;
    if (((IEnumerable<EntityField>) entity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name.Equals("Description", StringComparison.OrdinalIgnoreCase))) is EntityValueField entityValueField1 && !string.IsNullOrEmpty(entityValueField1.Value))
      ((PXSelectBase) inCategoryMaint.Folders).Cache.SetValueExt<INCategory.description>((object) current, (object) entityValueField1.Value);
    if (((IEnumerable<EntityField>) entity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name.Equals("ParentCategoryID", StringComparison.OrdinalIgnoreCase))) is EntityValueField entityValueField2 && !string.IsNullOrEmpty(entityValueField2.Value))
      current.ParentID = new int?(int.Parse(entityValueField2.Value));
    ((PXSelectBase<INCategory>) inCategoryMaint.Folders).Update(current);
  }

  protected INUnit GetINUnit(
    InventoryItemMaint maint,
    EntityValueField fromUnit,
    EntityValueField toUnit)
  {
    return ((IEnumerable<PXResult<INUnit>>) ((PXSelectBase<INUnit>) maint.UnitsOfMeasureExt.itemunits).Select(Array.Empty<object>())).AsEnumerable<PXResult<INUnit>>().Select<PXResult<INUnit>, INUnit>((Func<PXResult<INUnit>, INUnit>) (c => ((PXResult) c)[typeof (INUnit)] as INUnit)).FirstOrDefault<INUnit>((Func<INUnit, bool>) (c => c != null && (toUnit == null || string.IsNullOrEmpty(toUnit.Value) || string.Equals(c.ToUnit, toUnit.Value)) && string.Equals(c.FromUnit, fromUnit.Value)));
  }

  [FieldsProcessed(new string[] {"ToUOM", "FromUOM"})]
  protected void InventoryItemUOMConversion_Insert(
    PXGraph graph,
    EntityImpl entity,
    EntityImpl targetEntity)
  {
    InventoryItemMaint maint = (InventoryItemMaint) graph;
    EntityValueField fromUnit = ((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "FromUOM")) as EntityValueField;
    EntityValueField toUnit = ((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "ToUOM")) as EntityValueField;
    INUnit inUnit = this.GetINUnit(maint, fromUnit, toUnit);
    if (inUnit == null)
      inUnit = ((PXSelectBase<INUnit>) maint.UnitsOfMeasureExt.itemunits).Insert(new INUnit()
      {
        ToUnit = toUnit == null || string.IsNullOrEmpty(toUnit.Value) ? (string) null : toUnit.Value,
        FromUnit = fromUnit.Value
      });
    ((PXSelectBase<INUnit>) maint.UnitsOfMeasureExt.itemunits).Current = inUnit;
  }

  protected void InventoryItemUOMConversion_Delete(
    PXGraph graph,
    EntityImpl entity,
    EntityImpl targetEntity)
  {
    InventoryItemMaint maint = (InventoryItemMaint) graph;
    EntityValueField fromUnit = ((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "FromUOM")) as EntityValueField;
    EntityValueField toUnit = ((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "ToUOM")) as EntityValueField;
    INUnit inUnit = this.GetINUnit(maint, fromUnit, toUnit);
    if (inUnit == null)
      return;
    ((PXSelectBase<INUnit>) maint.UnitsOfMeasureExt.itemunits).Delete(inUnit);
    ((PXAction) maint.Save).Press();
  }

  [FieldsProcessed(new string[] {})]
  protected void FinancialSettings_Insert(
    PXGraph graph,
    EntityImpl entity,
    EntityImpl targetEntity)
  {
    SOOrderEntry soOrderEntry = (SOOrderEntry) graph;
    if (((IEnumerable<EntityField>) targetEntity.Fields).Count<EntityField>() <= 0)
      return;
    EntityValueField entityValueField = ((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "CustomerTaxZone")) as EntityValueField;
    if (((PXSelectBase<PX.Objects.SO.SOOrder>) soOrderEntry.Document).Current == null || entityValueField == null || !(((PXSelectBase<PX.Objects.SO.SOOrder>) soOrderEntry.Document).Current.TaxZoneID != entityValueField.Value))
      return;
    ((PXSelectBase<PX.Objects.SO.SOOrder>) soOrderEntry.Document).Current.OverrideTaxZone = new bool?(true);
  }

  [FieldsProcessed(new string[] {})]
  protected void FinancialSettings_Update(
    PXGraph graph,
    EntityImpl entity,
    EntityImpl targetEntity)
  {
    SOOrderEntry soOrderEntry = (SOOrderEntry) graph;
    if (((IEnumerable<EntityField>) targetEntity.Fields).Count<EntityField>() <= 0)
      return;
    EntityValueField entityValueField = ((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "CustomerTaxZone")) as EntityValueField;
    if (((PXSelectBase<PX.Objects.SO.SOOrder>) soOrderEntry.Document).Current == null || entityValueField == null || !(((PXSelectBase<PX.Objects.SO.SOOrder>) soOrderEntry.Document).Current.TaxZoneID != entityValueField.Value))
      return;
    ((PXSelectBase<PX.Objects.SO.SOOrder>) soOrderEntry.Document).Current.OverrideTaxZone = new bool?(true);
  }

  /// <summary>
  /// 
  /// </summary>
  protected void Action_SalesOrderAddInvoice(PXGraph graph, ActionImpl action)
  {
    AddInvoiceExt extension = graph.GetExtension<AddInvoiceExt>();
    foreach (PXResult<InvoiceSplit> pxResult in ((PXSelectBase<InvoiceSplit>) extension.invoiceSplits).Select(Array.Empty<object>()))
    {
      InvoiceSplit invoiceSplit = PXResult<InvoiceSplit>.op_Implicit(pxResult);
      invoiceSplit.QtyToReturn = invoiceSplit.QtyAvailForReturn;
      ((PXSelectBase<InvoiceSplit>) extension.invoiceSplits).Update(invoiceSplit);
    }
    ((PXAction) extension.addInvoiceOK).Press();
  }

  /// <summary>
  /// 
  /// </summary>
  protected void Action_SalesInvoiceAddOrder(PXGraph graph, ActionImpl action)
  {
    SOInvoiceEntry soInvoiceEntry = (SOInvoiceEntry) graph;
    string orderNbr = ((EntityValueField) ((IEnumerable<EntityField>) action.Fields).Single<EntityField>((Func<EntityField, bool>) (f => f.Name == "OrderNbr"))).Value;
    string orderType = ((EntityValueField) ((IEnumerable<EntityField>) action.Fields).Single<EntityField>((Func<EntityField, bool>) (f => f.Name == "OrderType"))).Value;
    string shipmentNbr = ((EntityValueField) ((IEnumerable<EntityField>) action.Fields).Single<EntityField>((Func<EntityField, bool>) (f => f.Name == "ShipmentNbr"))).Value;
    foreach (PX.Objects.SO.SOOrderShipment soOrderShipment in EnumerableEx.Select<PX.Objects.SO.SOOrderShipment>((IEnumerable) ((PXSelectBase<PX.Objects.SO.SOOrderShipment>) soInvoiceEntry.shipmentlist).Select(Array.Empty<object>())).Where<PX.Objects.SO.SOOrderShipment>((Func<PX.Objects.SO.SOOrderShipment, bool>) (_ => _.OrderType == orderType && _.OrderNbr == orderNbr && _.ShipmentNbr == shipmentNbr)))
    {
      soOrderShipment.Selected = new bool?(true);
      ((PXSelectBase<PX.Objects.SO.SOOrderShipment>) soInvoiceEntry.shipmentlist).Update(soOrderShipment);
    }
    ((PXAction) soInvoiceEntry.addShipment).Press();
  }

  /// <summary>
  /// 
  /// </summary>
  protected void Action_SalesOrderAddStockItem(PXGraph graph, ActionImpl action)
  {
    SOOrderSiteStatusLookupExt extension = graph.GetExtension<SOOrderSiteStatusLookupExt>();
    foreach (PXResult<SOOrderSiteStatusSelected> pxResult in ((PXSelectBase<SOOrderSiteStatusSelected>) extension.ItemInfo).Select(Array.Empty<object>()))
    {
      SOOrderSiteStatusSelected siteStatusSelected = PXResult<SOOrderSiteStatusSelected>.op_Implicit(pxResult);
      siteStatusSelected.Selected = new bool?(true);
      ((PXSelectBase<SOOrderSiteStatusSelected>) extension.ItemInfo).Update(siteStatusSelected);
    }
    ((PXAction) extension.addSelectedItems).Press();
    ((PXSelectBase) extension.ItemFilter).Cache.Clear();
  }

  /// <summary>
  /// 
  /// </summary>
  protected void Action_ShipmentAddOrder(PXGraph graph, ActionImpl action)
  {
    SOOrderExtension extension = graph.GetExtension<SOOrderExtension>();
    foreach (PXResult<SOShipmentPlan> pxResult in ((PXSelectBase<SOShipmentPlan>) extension.soshipmentplan).Select(Array.Empty<object>()))
    {
      SOShipmentPlan soShipmentPlan = PXResult<SOShipmentPlan>.op_Implicit(pxResult);
      this.SelectLine(extension, soShipmentPlan);
    }
    ((PXAction) extension.addSO).Press();
  }

  /// <summary>
  /// 
  /// </summary>
  protected void Action_PaymentLoadDocuments(PXGraph graph, ActionImpl action)
  {
    ARPaymentEntry arPaymentEntry = (ARPaymentEntry) graph;
    arPaymentEntry.LoadInvoicesProc(false, ((PXSelectBase<ARPaymentEntry.LoadOptions>) arPaymentEntry.loadOpts).Current);
  }

  /// <summary>
  /// 
  /// </summary>
  protected void Action_PaymentLoadOrders(PXGraph graph, ActionImpl action)
  {
    ARPaymentEntry arPaymentEntry = (ARPaymentEntry) graph;
    arPaymentEntry.GetOrdersToApplyTabExtension(true).LoadOrdersProc(false, ((PXSelectBase<ARPaymentEntry.LoadOptions>) arPaymentEntry.loadOpts).Current);
  }

  private static void AddPOOrderToBill(
    APInvoiceEntry invoiceEntry,
    EntityValueField orderType,
    EntityValueField orderNbr)
  {
    if (((PXSelectBase) invoiceEntry.Transactions).Cache.GetStateExt<PX.Objects.AP.APTran.pOOrderType>((object) new PX.Objects.AP.APTran()) is PXStringState stateExt && ((IEnumerable<string>) stateExt.AllowedLabels).Contains<string>(orderType.Value))
      orderType.Value = stateExt.ValueLabelDic.Single<KeyValuePair<string, string>>((Func<KeyValuePair<string, string>, bool>) (p => p.Value == orderType.Value)).Key;
    if (orderType.Value == "RS")
    {
      ApInvoiceEntryAddSubcontractsExtension extension = ((PXGraph) invoiceEntry).GetExtension<ApInvoiceEntryAddSubcontractsExtension>();
      POOrderRS poOrderRs = PXResult<POOrderRS>.op_Implicit(((IQueryable<PXResult<POOrderRS>>) ((PXSelectBase<POOrderRS>) extension.Subcontracts).Select(Array.Empty<object>())).Where<PXResult<POOrderRS>>((Expression<Func<PXResult<POOrderRS>, bool>>) (x => ((POOrderRS) x).OrderType == orderType.Value && ((POOrderRS) x).OrderNbr == orderNbr.Value)).FirstOrDefault<PXResult<POOrderRS>>());
      if (poOrderRs == null)
        throw new PXException("Subcontract {0} was not found.", new object[1]
        {
          (object) orderNbr.Value
        });
      poOrderRs.Selected = new bool?(true);
      ((PXSelectBase<POOrderRS>) extension.Subcontracts).Update(poOrderRs);
      ((PXAction) extension.AddSubcontract).Press();
    }
    else
    {
      AddPOOrderExtension extension = ((PXGraph) invoiceEntry).GetExtension<AddPOOrderExtension>();
      POOrderRS poOrderRs = PXResult<POOrderRS>.op_Implicit(((IQueryable<PXResult<POOrderRS>>) ((PXSelectBase<POOrderRS>) extension.poorderslist).Select(Array.Empty<object>())).Where<PXResult<POOrderRS>>((Expression<Func<PXResult<POOrderRS>, bool>>) (x => ((POOrderRS) x).OrderType == orderType.Value && ((POOrderRS) x).OrderNbr == orderNbr.Value)).FirstOrDefault<PXResult<POOrderRS>>());
      if (poOrderRs == null)
        throw new PXException("Purchase order {0} - {1} was not found.", new object[2]
        {
          (object) orderType.Value,
          (object) orderNbr.Value
        });
      poOrderRs.Selected = new bool?(true);
      ((PXSelectBase<POOrderRS>) extension.poorderslist).Update(poOrderRs);
      ((PXAction) extension.addPOOrder2).Press();
    }
  }

  private static void AddPOOrderLineToBill(
    APInvoiceEntry invoiceEntry,
    EntityValueField orderType,
    EntityValueField orderNbr,
    EntityValueField orderLineNbr)
  {
    if (((PXSelectBase) invoiceEntry.Transactions).Cache.GetStateExt<PX.Objects.AP.APTran.pOOrderType>((object) new PX.Objects.AP.APTran()) is PXStringState stateExt && ((IEnumerable<string>) stateExt.AllowedLabels).Contains<string>(orderType.Value))
      orderType.Value = stateExt.ValueLabelDic.Single<KeyValuePair<string, string>>((Func<KeyValuePair<string, string>, bool>) (p => p.Value == orderType.Value)).Key;
    if (orderType.Value == "RS")
    {
      ApInvoiceEntryAddSubcontractsExtension extension = ((PXGraph) invoiceEntry).GetExtension<ApInvoiceEntryAddSubcontractsExtension>();
      int poLineNbr = int.Parse(orderLineNbr.Value);
      POLineRS poLineRs = PXResult<POLineRS>.op_Implicit(((IQueryable<PXResult<POLineRS>>) ((PXSelectBase<POLineRS>) extension.SubcontractLines).Select(Array.Empty<object>())).Where<PXResult<POLineRS>>((Expression<Func<PXResult<POLineRS>, bool>>) (x => ((POLineRS) x).OrderType == orderType.Value && ((POLineRS) x).OrderNbr == orderNbr.Value && ((POLineRS) x).LineNbr == (int?) poLineNbr)).FirstOrDefault<PXResult<POLineRS>>());
      if (poLineRs == null)
        throw new PXException("Subcontract {0}, Line Nbr.: {1} was not found.", new object[2]
        {
          (object) orderNbr.Value,
          (object) orderLineNbr.Value
        });
      poLineRs.Selected = new bool?(true);
      ((PXSelectBase<POLineRS>) extension.SubcontractLines).Update(poLineRs);
      ((PXAction) extension.AddSubcontractLine).Press();
    }
    else
    {
      AddPOOrderLineExtension extension = ((PXGraph) invoiceEntry).GetExtension<AddPOOrderLineExtension>();
      int poLineNbr = int.Parse(orderLineNbr.Value);
      POLineRS poLineRs = PXResult<POLineRS>.op_Implicit(((IQueryable<PXResult<POLineRS>>) ((PXSelectBase<POLineRS>) extension.poorderlineslist).Select(Array.Empty<object>())).Where<PXResult<POLineRS>>((Expression<Func<PXResult<POLineRS>, bool>>) (x => ((POLineRS) x).OrderType == orderType.Value && ((POLineRS) x).OrderNbr == orderNbr.Value && ((POLineRS) x).LineNbr == (int?) poLineNbr)).FirstOrDefault<PXResult<POLineRS>>());
      if (poLineRs == null)
        throw new PXException("Order Line: {0} {1}, Line Nbr.: {2} not found.", new object[3]
        {
          (object) orderType.Value,
          (object) orderNbr.Value,
          (object) orderLineNbr.Value
        });
      poLineRs.Selected = new bool?(true);
      ((PXSelectBase<POLineRS>) extension.poorderlineslist).Update(poLineRs);
      ((PXAction) extension.addPOOrderLine2).Press();
    }
  }

  private static void AddPOReceiptToBill(
    APInvoiceEntry invoiceEntry,
    EntityValueField receiptType,
    EntityValueField receiptNbr)
  {
    AddPOReceiptExtension extension = ((PXGraph) invoiceEntry).GetExtension<AddPOReceiptExtension>();
    PX.Objects.PO.POReceipt poReceipt = PXResult<PX.Objects.PO.POReceipt>.op_Implicit(((IQueryable<PXResult<PX.Objects.PO.POReceipt>>) ((PXSelectBase<PX.Objects.PO.POReceipt>) extension.poreceiptslist).Select(Array.Empty<object>())).Where<PXResult<PX.Objects.PO.POReceipt>>((Expression<Func<PXResult<PX.Objects.PO.POReceipt>, bool>>) (x => ((PX.Objects.PO.POReceipt) x).ReceiptType == receiptType.Value && ((PX.Objects.PO.POReceipt) x).ReceiptNbr == receiptNbr.Value)).FirstOrDefault<PXResult<PX.Objects.PO.POReceipt>>());
    if (poReceipt == null)
      throw new PXException("Purchase Receipt {0} was not found.", new object[1]
      {
        (object) receiptNbr.Value
      });
    poReceipt.Selected = new bool?(true);
    ((PXSelectBase<PX.Objects.PO.POReceipt>) extension.poreceiptslist).Update(poReceipt);
    ((PXAction) extension.addPOReceipt2).Press();
  }

  private static void AddPOReceiptLineToBill(
    APInvoiceEntry invoiceEntry,
    EntityValueField receiptType,
    EntityValueField receiptNbr,
    EntityValueField receiptLineNbr)
  {
    AddPOReceiptLineExtension extension = ((PXGraph) invoiceEntry).GetExtension<AddPOReceiptLineExtension>();
    POReceiptLineAdd poReceiptLineAdd = PXResultset<POReceiptLineAdd>.op_Implicit(((PXSelectBase<POReceiptLineAdd>) extension.ReceipLineAdd).Select(new object[3]
    {
      (object) receiptType.Value,
      (object) receiptNbr.Value,
      (object) receiptLineNbr.Value
    }));
    if (poReceiptLineAdd == null)
      throw new PXException("Receipt Line {0} - {1} not found.", new object[2]
      {
        (object) receiptNbr.Value,
        (object) receiptLineNbr.Value
      });
    poReceiptLineAdd.Selected = new bool?(true);
    ((PXSelectBase<POReceiptLineAdd>) extension.poReceiptLinesSelection).Update(poReceiptLineAdd);
    ((PXAction) extension.addReceiptLine2).Press();
  }

  [FieldsProcessed(new string[] {})]
  protected void Payments_Insert(PXGraph graph, EntityImpl entity, EntityImpl targetEntity)
  {
    if (((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "ReferenceNbr")) is EntityValueField entityValueField)
    {
      SOAdjust instance = (SOAdjust) ((PXSelectBase) ((SOOrderEntry) graph).Adjustments).Cache.CreateInstance();
      instance.AdjgRefNbr = entityValueField.Value;
      ((PXSelectBase<SOAdjust>) ((SOOrderEntry) graph).Adjustments).Insert(instance);
    }
    else
    {
      EntityValueField entityValueField1 = ((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "DocType")) as EntityValueField;
      if (!(((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "AppliedToOrder")) is EntityValueField entityValueField2) || entityValueField2.Value == null)
        return;
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      DefaultEndpointImpl.\u003C\u003Ec__DisplayClass41_0 cDisplayClass410 = new DefaultEndpointImpl.\u003C\u003Ec__DisplayClass41_0();
      Decimal num1 = Decimal.Parse(entityValueField2.Value);
      SOOrderEntry soOrderEntry = (SOOrderEntry) graph;
      ((PXAction) soOrderEntry.Save).Press();
      CreatePaymentExt extension = graph.GetExtension<CreatePaymentExt>();
      extension.CheckTermsInstallmentType();
      if (entityValueField1 != null && graph.Caches[typeof (PX.Objects.AR.ARRegister)].GetStateExt((object) new PX.Objects.AR.ARRegister(), "DocType") is PXStringState stateExt && stateExt.ValueLabelDic != null)
      {
        bool flag = false;
        foreach (KeyValuePair<string, string> keyValuePair in stateExt.ValueLabelDic)
        {
          if (keyValuePair.Value == entityValueField1.Value || keyValuePair.Key == entityValueField1.Value)
          {
            flag = true;
            entityValueField1.Value = keyValuePair.Key;
            break;
          }
        }
        if (!flag)
          entityValueField1 = (EntityValueField) null;
      }
      PX.Objects.SO.SOOrder current1 = ((PXSelectBase<PX.Objects.SO.SOOrder>) soOrderEntry.Document).Current;
      SOQuickPayment current2 = ((PXSelectBase<SOQuickPayment>) extension.QuickPayment).Current;
      extension.SetDefaultValues(current2, current1);
      PXCache cache = ((PXSelectBase) extension.QuickPayment).Cache;
      if (((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "PaymentMethod")) is EntityValueField entityValueField3 && entityValueField3.Value != null)
        cache.SetValueExt<SOQuickPayment.paymentMethodID>((object) current2, (object) entityValueField3.Value);
      if (((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "CashAccount")) is EntityValueField entityValueField4 && entityValueField4.Value != null)
        cache.SetValueExt<SOQuickPayment.cashAccountID>((object) current2, (object) entityValueField4.Value);
      if (((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "PaymentAmount")) is EntityValueField entityValueField5 && entityValueField5.Value != null)
      {
        Decimal num2 = Decimal.Parse(entityValueField5.Value);
        cache.SetValueExt<SOQuickPayment.curyOrigDocAmt>((object) current2, (object) num2);
      }
      else
        cache.SetValueExt<SOQuickPayment.curyOrigDocAmt>((object) current2, (object) num1);
      // ISSUE: reference to a compiler-generated field
      cDisplayClass410.paymentEntry = extension.CreatePayment(current2, current1, entityValueField1 != null ? entityValueField1.Value : "PMT");
      if (((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "PaymentRef")) is EntityValueField entityValueField6 && entityValueField6.Value != null)
      {
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        ((PXSelectBase) cDisplayClass410.paymentEntry.Document).Cache.SetValueExt<PX.Objects.AR.ARPayment.extRefNbr>((object) ((PXSelectBase<PX.Objects.AR.ARPayment>) cDisplayClass410.paymentEntry.Document).Current, (object) entityValueField6.Value);
      }
      // ISSUE: reference to a compiler-generated field
      ((PXAction) cDisplayClass410.paymentEntry.Save).Press();
      ((PXAction) soOrderEntry.Cancel).Press();
      ((PXSelectBase<PX.Objects.SO.SOOrder>) soOrderEntry.Document).Current = PXResultset<PX.Objects.SO.SOOrder>.op_Implicit(((PXSelectBase<PX.Objects.SO.SOOrder>) soOrderEntry.Document).Search<PX.Objects.SO.SOOrder.orderNbr>((object) ((PXSelectBase<PX.Objects.SO.SOOrder>) soOrderEntry.Document).Current.OrderNbr, new object[1]
      {
        (object) ((PXSelectBase<PX.Objects.SO.SOOrder>) soOrderEntry.Document).Current.OrderType
      }));
      try
      {
        ParameterExpression parameterExpression;
        // ISSUE: field reference
        // ISSUE: field reference
        // ISSUE: method reference
        // ISSUE: type reference
        // ISSUE: method reference
        // ISSUE: method reference
        // ISSUE: type reference
        // ISSUE: method reference
        // ISSUE: field reference
        // ISSUE: field reference
        // ISSUE: method reference
        // ISSUE: type reference
        // ISSUE: method reference
        ((PXSelectBase<SOAdjust>) soOrderEntry.Adjustments).Current = PXResult<SOAdjust>.op_Implicit(((IQueryable<PXResult<SOAdjust>>) ((PXSelectBase<SOAdjust>) soOrderEntry.Adjustments).Select(Array.Empty<object>())).Where<PXResult<SOAdjust>>(Expression.Lambda<Func<PXResult<SOAdjust>, bool>>((Expression) Expression.AndAlso((Expression) Expression.Equal(((SOAdjust) x).AdjgDocType, (Expression) Expression.Property((Expression) Expression.Property((Expression) Expression.Field((Expression) Expression.Field((Expression) Expression.Constant((object) cDisplayClass410, typeof (DefaultEndpointImpl.\u003C\u003Ec__DisplayClass41_0)), FieldInfo.GetFieldFromHandle(__fieldref (DefaultEndpointImpl.\u003C\u003Ec__DisplayClass41_0.paymentEntry))), FieldInfo.GetFieldFromHandle(__fieldref (ARPaymentEntry.Document))), (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (PXSelectBase<PX.Objects.AR.ARPayment>.get_Current), __typeref (PXSelectBase<PX.Objects.AR.ARPayment>))), (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (PX.Objects.AR.ARRegister.get_DocType)))), (Expression) Expression.Equal((Expression) Expression.Property((Expression) Expression.Convert((Expression) parameterExpression, typeof (SOAdjust), (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (PXResult<SOAdjust>.op_Implicit), __typeref (PXResult<SOAdjust>))), (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (SOAdjust.get_AdjgRefNbr))), (Expression) Expression.Property((Expression) Expression.Property((Expression) Expression.Field((Expression) Expression.Field((Expression) Expression.Constant((object) cDisplayClass410, typeof (DefaultEndpointImpl.\u003C\u003Ec__DisplayClass41_0)), FieldInfo.GetFieldFromHandle(__fieldref (DefaultEndpointImpl.\u003C\u003Ec__DisplayClass41_0.paymentEntry))), FieldInfo.GetFieldFromHandle(__fieldref (ARPaymentEntry.Document))), (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (PXSelectBase<PX.Objects.AR.ARPayment>.get_Current), __typeref (PXSelectBase<PX.Objects.AR.ARPayment>))), (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (PX.Objects.AR.ARRegister.get_RefNbr))))), parameterExpression)).First<PXResult<SOAdjust>>());
      }
      catch
      {
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        throw new PXException("Payment {0} {1} was not found in the list of payments applied to order.", new object[2]
        {
          (object) ((PXSelectBase<PX.Objects.AR.ARPayment>) cDisplayClass410.paymentEntry.Document).Current.DocType,
          (object) ((PXSelectBase<PX.Objects.AR.ARPayment>) cDisplayClass410.paymentEntry.Document).Current.RefNbr
        });
      }
    }
  }

  [FieldsProcessed(new string[] {"AttributeID", "Value"})]
  protected void AttributeValue_Insert(PXGraph graph, EntityImpl entity, EntityImpl targetEntity)
  {
    this.AttributeBase_Insert(graph, entity, targetEntity, "AttributeID");
  }

  [FieldsProcessed(new string[] {"Attribute", "Value"})]
  protected void AttributeDetail_Insert(PXGraph graph, EntityImpl entity, EntityImpl targetEntity)
  {
    this.AttributeBase_Insert(graph, entity, targetEntity, "Attribute");
  }
}
