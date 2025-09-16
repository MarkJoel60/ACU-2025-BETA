// Decompiled with JetBrains decompiler
// Type: PX.Objects.EndpointAdapters.DefaultEndpointImpl20
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Api;
using PX.Api.ContractBased;
using PX.Api.ContractBased.Models;
using PX.Common;
using PX.Data;
using PX.Objects.CS;
using PX.Objects.PO;
using PX.Objects.SO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

#nullable disable
namespace PX.Objects.EndpointAdapters;

[PXInternalUseOnly]
[PXVersion("20.200.001", "Default")]
public class DefaultEndpointImpl20 : DefaultEndpointImpl
{
  [FieldsProcessed(new string[] {"AttributeID", "AttributeDescription", "RefNoteID", "Value", "ValueDescription"})]
  protected new void AttributeValue_Insert(
    PXGraph graph,
    EntityImpl entity,
    EntityImpl targetEntity)
  {
    string attributeID = targetEntity.Fields.OfType<EntityValueField>().FirstOrDefault<EntityValueField>((Func<EntityValueField, bool>) (f => ((EntityField) f).Name == "AttributeID"))?.Value;
    if (attributeID == null)
      return;
    this.ProcessAttribute(graph, targetEntity, attributeID);
  }

  [FieldsProcessed(new string[] {"AttributeID", "AttributeDescription", "RefNoteID", "Value", "ValueDescription"})]
  protected void AttributeValue_Update(PXGraph graph, EntityImpl entity, EntityImpl targetEntity)
  {
    Dictionary<string, string> dictionary;
    string attributeID;
    if (!targetEntity.InternalKeys.TryGetValue("Answers", out dictionary) || !dictionary.TryGetValue("AttributeID", out attributeID))
      return;
    this.ProcessAttribute(graph, targetEntity, attributeID);
  }

  private void ProcessAttribute(PXGraph graph, EntityImpl targetEntity, string attributeID)
  {
    EntityValueField entityValueField1 = targetEntity.Fields.OfType<EntityValueField>().FirstOrDefault<EntityValueField>((Func<EntityValueField, bool>) (f => ((EntityField) f).Name == "Value"));
    EntityValueField entityValueField2 = targetEntity.Fields.OfType<EntityValueField>().FirstOrDefault<EntityValueField>((Func<EntityValueField, bool>) (f => ((EntityField) f).Name == "IsActive"));
    if (entityValueField1 == null && entityValueField2 == null)
      return;
    PXView view = graph.Views["Answers"];
    PXCache cache = view.Cache;
    object[] array = view.SelectMulti(Array.Empty<object>()).OrderBy<object, object>((Func<object, object>) (row => (cache.GetStateExt<CSAnswers.order>(row) as PXFieldState).Value)).ToArray<object>();
    OrderedDictionary orderedDictionary = new OrderedDictionary();
    foreach (CSAnswers csAnswers in array)
    {
      string str = cache.GetStateExt<CSAnswers.attributeID>((object) csAnswers) is PXFieldState stateExt1 ? stateExt1.Value?.ToString() : (string) null;
      if (StringExtensions.OrdinalEquals(attributeID, csAnswers.AttributeID) || StringExtensions.OrdinalEquals(attributeID, str))
      {
        bool flag = false;
        if (entityValueField1 != null)
        {
          if (cache.GetStateExt<CSAnswers.value>((object) csAnswers) is PXStringState stateExt2)
          {
            if (!((PXFieldState) stateExt2).Enabled)
              flag = true;
            else if (stateExt2.ValueLabelDic != null)
            {
              foreach (KeyValuePair<string, string> keyValuePair in stateExt2.ValueLabelDic)
              {
                if (keyValuePair.Value == entityValueField1.Value)
                {
                  entityValueField1.Value = keyValuePair.Key;
                  break;
                }
              }
            }
          }
          cache.SetValueExt<CSAnswers.value>((object) csAnswers, (object) entityValueField1.Value);
          cache.Update((object) csAnswers);
          orderedDictionary.Add((object) csAnswers.AttributeID, (object) entityValueField1.Value);
        }
        if (entityValueField2 != null && cache.GetStateExt<CSAnswers.isActive>((object) csAnswers) is PXFieldState stateExt3 && stateExt3.Enabled)
        {
          cache.SetValueExt<CSAnswers.isActive>((object) csAnswers, (object) entityValueField2.Value);
          cache.Update((object) csAnswers);
        }
        if (!flag)
          break;
      }
    }
    graph.ExecuteUpdate("Answers", (IDictionary) orderedDictionary, (IDictionary) orderedDictionary, Array.Empty<object>());
  }

  [FieldsProcessed(new string[] {"POLineNbr", "POOrderType", "POOrderNbr", "POReceiptLineNbr", "POReceiptNbr", "TransferOrderType", "TransferOrderNbr", "TransferShipmentNbr"})]
  protected override void PurchaseReceiptDetail_Insert(
    PXGraph graph,
    EntityImpl entity,
    EntityImpl targetEntity)
  {
    POReceiptEntry receiptEntry = (POReceiptEntry) graph;
    if (((PXSelectBase<PX.Objects.PO.POReceipt>) receiptEntry.Document).Current == null || ((PXSelectBase<PX.Objects.PO.POReceipt>) receiptEntry.Document).Current.Released.GetValueOrDefault())
      return;
    EntityValueField receiptNbr = ((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "POReceiptNbr")) as EntityValueField;
    PXCache cache = ((PXSelectBase) receiptEntry.transactions).Cache;
    if (((PXSelectBase<PX.Objects.PO.POReceipt>) receiptEntry.Document).Current.ReceiptType == "RN" && receiptNbr != null)
    {
      EntityValueField receiptLineNbr = ((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "POReceiptLineNbr")) as EntityValueField;
      int num = receiptLineNbr == null ? 0 : (receiptNbr != null ? 1 : 0);
      bool flag = receiptNbr != null;
      if (num != 0)
      {
        this.FillInAddPRFilter(receiptEntry, receiptNbr);
        ParameterExpression parameterExpression;
        // ISSUE: method reference
        POReceiptLineReturn receiptLineReturn = ((IQueryable<PXResult<POReceiptLineReturn>>) ((PXSelectBase<POReceiptLineReturn>) receiptEntry.poReceiptLineReturn).Select(Array.Empty<object>())).Select<PXResult<POReceiptLineReturn>, POReceiptLineReturn>(Expression.Lambda<Func<PXResult<POReceiptLineReturn>, POReceiptLineReturn>>((Expression) Expression.Call(r, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (PXResult.GetItem)), Array.Empty<Expression>()), parameterExpression)).FirstOrDefault<POReceiptLineReturn>((Expression<Func<POReceiptLineReturn, bool>>) (o => o.LineNbr == (int?) int.Parse(receiptLineNbr.Value)));
        if (receiptLineReturn == null)
          throw new PXException("The purchase receipt line was not found.");
        receiptLineReturn.Selected = new bool?(true);
        ((PXSelectBase<POReceiptLineReturn>) receiptEntry.poReceiptLineReturn).Update(receiptLineReturn);
        ((PXGraph) receiptEntry).Actions["AddPOReceiptLineReturn2"].Press();
        return;
      }
      if (flag)
      {
        this.FillInAddPRFilter(receiptEntry, receiptNbr);
        ParameterExpression parameterExpression;
        // ISSUE: method reference
        POReceiptReturn poReceiptReturn = ((IQueryable<PXResult<POReceiptReturn>>) ((PXSelectBase<POReceiptReturn>) receiptEntry.poReceiptReturn).Select(Array.Empty<object>())).Select<PXResult<POReceiptReturn>, POReceiptReturn>(Expression.Lambda<Func<PXResult<POReceiptReturn>, POReceiptReturn>>((Expression) Expression.Call(r, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (PXResult.GetItem)), Array.Empty<Expression>()), parameterExpression)).FirstOrDefault<POReceiptReturn>();
        poReceiptReturn.Selected = new bool?(true);
        ((PXSelectBase<POReceiptReturn>) receiptEntry.poReceiptReturn).Update(poReceiptReturn);
        ((PXGraph) receiptEntry).Actions["AddPOReceiptReturn2"].Press();
        return;
      }
    }
    base.PurchaseReceiptDetail_Insert(graph, entity, targetEntity);
    if (!(((PXSelectBase<PX.Objects.PO.POReceipt>) receiptEntry.Document).Current.ReceiptType == "RN") || receiptNbr != null || cache.Current == null || ((POReceiptLine) cache.Current).InventoryID.HasValue)
      return;
    DefaultEndpointImpl.SetFieldsNeedToInsertAllocations(targetEntity, receiptEntry, (POReceiptLine) cache.Current);
  }

  protected virtual void FillInAddPRFilter(POReceiptEntry receiptEntry, EntityValueField receiptNbr)
  {
    ((PXSelectBase) receiptEntry.returnFilter).Cache.Remove((object) ((PXSelectBase<POReceiptReturnFilter>) receiptEntry.returnFilter).Current);
    ((PXSelectBase) receiptEntry.returnFilter).Cache.Insert((object) new POReceiptReturnFilter());
    POReceiptReturnFilter current = ((PXSelectBase<POReceiptReturnFilter>) receiptEntry.returnFilter).Current;
    ((PXSelectBase) receiptEntry.returnFilter).Cache.SetValueExt((object) current, "ReceiptNbr", (object) receiptNbr.Value);
    POReceiptReturnFilter receiptReturnFilter = ((PXSelectBase<POReceiptReturnFilter>) receiptEntry.returnFilter).Update(current);
    Dictionary<string, string> errors = PXUIFieldAttribute.GetErrors(((PXSelectBase) receiptEntry.returnFilter).Cache, (object) receiptReturnFilter, Array.Empty<PXErrorLevel>());
    if (errors.Count<KeyValuePair<string, string>>() > 0)
      throw new PXException(string.Join(";", errors.Select<KeyValuePair<string, string>, string>((Func<KeyValuePair<string, string>, string>) (x => $"{x.Key}={x.Value}"))));
  }

  [FieldsProcessed(new string[] {})]
  protected virtual void SalesOrderDetail_Insert(
    PXGraph graph,
    EntityImpl entity,
    EntityImpl targetEntity)
  {
    SOOrderEntry soOrderEntry = (SOOrderEntry) graph;
    if (((PXSelectBase<PX.Objects.SO.SOOrder>) soOrderEntry.Document).Current == null)
      return;
    PXCache cache = ((PXSelectBase) soOrderEntry.Transactions).Cache;
    cache.Current = cache.Insert();
    PX.Objects.SO.SOLine soLine = cache.Current as PX.Objects.SO.SOLine;
    if (cache.Current == null)
      throw new InvalidOperationException("Cannot insert Sales Order detail.");
    if (((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "InventoryID")) is EntityValueField entityValueField1)
    {
      ((PXSelectBase) soOrderEntry.Transactions).Cache.SetValueExt((object) soLine, "InventoryID", (object) entityValueField1.Value);
      if (((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "WarehouseID")) is EntityValueField entityValueField)
        ((PXSelectBase) soOrderEntry.Transactions).Cache.SetValueExt((object) soLine, "SiteID", (object) entityValueField.Value);
      soLine = ((PXSelectBase<PX.Objects.SO.SOLine>) soOrderEntry.Transactions).Update(soLine);
    }
    if (!(((PXSelectBase<PX.Objects.SO.SOOrder>) soOrderEntry.Document).Current.Behavior == "BL") || !((IEnumerable<EntityImpl>) ((((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => string.Equals(f.Name, "Allocations"))) is EntityListField entityListField ? entityListField.Value : (EntityImpl[]) null) ?? new EntityImpl[0])).Any<EntityImpl>((Func<EntityImpl, bool>) (a => a.Fields != null && a.Fields.Length != 0)))
      return;
    EntityValueField entityValueField2 = ((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "WarehouseID")) as EntityValueField;
    EntityValueField entityValueField3 = ((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "Location")) as EntityValueField;
    EntityValueField entityValueField4 = ((IEnumerable<EntityField>) targetEntity.Fields).FirstOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "Subitem")) as EntityValueField;
    if (entityValueField2 != null)
      ((PXSelectBase) soOrderEntry.Transactions).Cache.SetValueExt((object) soLine, "SiteID", (object) entityValueField2.Value);
    if (entityValueField3 != null)
      ((PXSelectBase) soOrderEntry.Transactions).Cache.SetValueExt((object) soLine, "LocationID", (object) entityValueField3.Value);
    if (entityValueField4 != null)
      ((PXSelectBase) soOrderEntry.Transactions).Cache.SetValueExt((object) soLine, "SubItemID", (object) entityValueField4.Value);
    if (((IEnumerable<EntityField>) targetEntity.Fields).FirstOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "OrderQty")) is EntityValueField entityValueField5)
    {
      soLine.OrderQty = new Decimal?(Decimal.Parse(entityValueField5.Value));
      ((PXSelectBase<PX.Objects.SO.SOLine>) soOrderEntry.Transactions).Update(soLine);
    }
    if (cache.Current == null)
      return;
    foreach (SOLineSplit soLineSplit in ((PXSelectBase) soOrderEntry.splits).Cache.Inserted)
    {
      int? lineNbr1 = soLineSplit.LineNbr;
      int? lineNbr2 = (cache.Current as PX.Objects.SO.SOLine).LineNbr;
      if (lineNbr1.GetValueOrDefault() == lineNbr2.GetValueOrDefault() & lineNbr1.HasValue == lineNbr2.HasValue)
        ((PXSelectBase<SOLineSplit>) soOrderEntry.splits).Delete(soLineSplit);
    }
  }
}
