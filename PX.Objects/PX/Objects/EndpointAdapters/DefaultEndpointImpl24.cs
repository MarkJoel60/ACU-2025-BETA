// Decompiled with JetBrains decompiler
// Type: PX.Objects.EndpointAdapters.DefaultEndpointImpl24
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
using PX.Objects.IN;
using PX.Objects.PO;
using PX.Objects.PO.GraphExtensions.APInvoiceSmartPanel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Linq.Expressions;

#nullable disable
namespace PX.Objects.EndpointAdapters;

[PXInternalUseOnly]
[PXVersion("24.200.001", "Default")]
public class DefaultEndpointImpl24 : DefaultEndpointImpl23
{
  [FieldsProcessed(new string[] {})]
  protected virtual void InventoryReceiptDetail_Insert(
    PXGraph graph,
    EntityImpl entity,
    EntityImpl targetEntity)
  {
    INReceiptEntry inReceiptEntry = (INReceiptEntry) graph;
    if (((PXSelectBase<PX.Objects.IN.INRegister>) inReceiptEntry.receipt).Current == null)
      return;
    PXCache cache = ((PXSelectBase) inReceiptEntry.transactions).Cache;
    cache.Current = cache.Insert();
    INTran current = cache.Current as INTran;
    if (cache.Current == null)
      throw new InvalidOperationException("Cannot insert Inventory Receipt detail.");
    if (!((IEnumerable<EntityImpl>) ((((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => string.Equals(f.Name, "Allocations"))) is EntityListField entityListField ? entityListField.Value : (EntityImpl[]) null) ?? new EntityImpl[0])).Any<EntityImpl>((Func<EntityImpl, bool>) (a => a.Fields != null && a.Fields.Length != 0)))
      return;
    EntityValueField entityValueField1 = ((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "InventoryID")) as EntityValueField;
    EntityValueField entityValueField2 = ((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "WarehouseID")) as EntityValueField;
    EntityValueField entityValueField3 = ((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "Location")) as EntityValueField;
    EntityValueField entityValueField4 = ((IEnumerable<EntityField>) targetEntity.Fields).FirstOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "Subitem")) as EntityValueField;
    if (entityValueField1 != null)
      ((PXSelectBase) inReceiptEntry.transactions).Cache.SetValueExt((object) current, "InventoryID", (object) entityValueField1.Value);
    if (entityValueField2 != null)
      ((PXSelectBase) inReceiptEntry.transactions).Cache.SetValueExt((object) current, "SiteID", (object) entityValueField2.Value);
    if (entityValueField3 != null)
      ((PXSelectBase) inReceiptEntry.transactions).Cache.SetValueExt((object) current, "LocationID", (object) entityValueField3.Value);
    if (entityValueField4 != null)
      ((PXSelectBase) inReceiptEntry.transactions).Cache.SetValueExt((object) current, "SubItemID", (object) entityValueField4.Value);
    if (((IEnumerable<EntityField>) targetEntity.Fields).FirstOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "Qty")) is EntityValueField entityValueField5)
    {
      current.Qty = new Decimal?(Decimal.Parse(entityValueField5.Value));
      ((PXSelectBase<INTran>) inReceiptEntry.transactions).Update(current);
    }
    if (cache.Current == null)
      return;
    foreach (INTranSplit inTranSplit in ((PXSelectBase) inReceiptEntry.splits).Cache.Inserted)
    {
      int? lineNbr1 = inTranSplit.LineNbr;
      int? lineNbr2 = (cache.Current as INTran).LineNbr;
      if (lineNbr1.GetValueOrDefault() == lineNbr2.GetValueOrDefault() & lineNbr1.HasValue == lineNbr2.HasValue)
        ((PXSelectBase<INTranSplit>) inReceiptEntry.splits).Delete(inTranSplit);
    }
  }

  /// <summary>
  /// Handles creation of document details in the Bills and Adjustments (AP301000) screen
  /// for cases when landed cost entities are specified
  /// using the <see cref="F:PX.Objects.PO.GraphExtensions.APInvoiceSmartPanel.AddLandedCostExtension.addLandedCost">Add LC action</see>
  /// </summary>
  [FieldsProcessed(new string[] {"LCType", "LCNbr", "LCLineNbr"})]
  protected override void BillDetail_Insert(
    PXGraph graph,
    EntityImpl entity,
    EntityImpl targetEntity)
  {
    APInvoiceEntry invoiceEntry = (APInvoiceEntry) graph;
    EntityValueField lCType = ((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "LCType")) as EntityValueField;
    EntityValueField lCNbr = ((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "LCNbr")) as EntityValueField;
    EntityValueField lCLineNbr = ((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name == "LCLineNbr")) as EntityValueField;
    if (lCType != null && lCNbr != null)
    {
      DefaultEndpointImpl24.AddLCOrderToBill(invoiceEntry, lCType, lCNbr, lCLineNbr);
    }
    else
    {
      if (lCType != null || lCNbr != null)
        throw new PXException("Both LCType and LCNbr must be provided to add Landed Costs to details.");
      base.BillDetail_Insert(graph, entity, targetEntity);
    }
  }

  private static void AddLCOrderToBill(
    APInvoiceEntry invoiceEntry,
    EntityValueField lCType,
    EntityValueField lCNbr,
    EntityValueField lCLineNbr)
  {
    AddLandedCostExtension extension = ((PXGraph) invoiceEntry).GetExtension<AddLandedCostExtension>();
    if (((PXSelectBase) invoiceEntry.Transactions).Cache.GetStateExt<PX.Objects.AP.APTran.lCDocType>((object) new PX.Objects.AP.APTran()) is PXStringState stateExt && ((IEnumerable<string>) stateExt.AllowedLabels).Contains<string>(lCType.Value))
      lCType.Value = stateExt.ValueLabelDic.Single<KeyValuePair<string, string>>((Func<KeyValuePair<string, string>, bool>) (p => p.Value == lCType.Value)).Key;
    List<PXResult<POLandedCostDetailS>> list = ((IQueryable<PXResult<POLandedCostDetailS>>) ((PXSelectBase<POLandedCostDetailS>) extension.LandedCostDetailsAdd).Select(Array.Empty<object>())).Where<PXResult<POLandedCostDetailS>>((Expression<Func<PXResult<POLandedCostDetailS>, bool>>) (x => ((POLandedCostDetailS) x).DocType == lCType.Value && ((POLandedCostDetailS) x).RefNbr == lCNbr.Value)).ToList<PXResult<POLandedCostDetailS>>();
    if (lCLineNbr?.Value != null)
    {
      list = list.Where<PXResult<POLandedCostDetailS>>((Func<PXResult<POLandedCostDetailS>, bool>) (x =>
      {
        int? lineNbr = PXResult<POLandedCostDetailS>.op_Implicit(x).LineNbr;
        int num = int.Parse(lCLineNbr.Value);
        return lineNbr.GetValueOrDefault() == num & lineNbr.HasValue;
      })).ToList<PXResult<POLandedCostDetailS>>();
      if (!list.Any<PXResult<POLandedCostDetailS>>())
        throw new PXException("Landed Cost Line {0} - {1} not found.", new object[2]
        {
          (object) lCNbr.Value,
          (object) lCLineNbr.Value
        });
    }
    if (!list.Any<PXResult<POLandedCostDetailS>>())
      throw new PXException("Landed Cost {0} was not found.", new object[1]
      {
        (object) lCNbr.Value
      });
    foreach (PXResult<POLandedCostDetailS> pxResult in list)
    {
      POLandedCostDetailS landedCostDetailS = PXResult<POLandedCostDetailS>.op_Implicit(pxResult);
      landedCostDetailS.Selected = new bool?(true);
      ((PXSelectBase<POLandedCostDetailS>) extension.LandedCostDetailsAdd).Update(landedCostDetailS);
    }
    ((PXAction) extension.addLandedCost2).Press();
  }

  [FieldsProcessed(new string[] {"VendorOrCustomer", "AlternateID"})]
  protected void InventoryItemCrossReference_Update(
    PXGraph graph,
    EntityImpl entity,
    EntityImpl targetEntity)
  {
    InventoryItemMaint inventoryItemMaint = (InventoryItemMaint) graph;
    EntityImpl entityImpl = targetEntity;
    if ((entityImpl != null ? (((Entity) entityImpl).ID.HasValue ? 1 : 0) : 0) == 0)
      return;
    INItemXRef inItemXref = PXResult<INItemXRef>.op_Implicit(((IQueryable<PXResult<INItemXRef>>) ((PXSelectBase<INItemXRef>) inventoryItemMaint.itemxrefrecords).Select(Array.Empty<object>())).FirstOrDefault<PXResult<INItemXRef>>((Expression<Func<PXResult<INItemXRef>, bool>>) (x => ((INItemXRef) x).NoteID == targetEntity.ID)));
    if (inItemXref == null)
      return;
    OrderedDictionary orderedDictionary1 = new OrderedDictionary((IEqualityComparer) StringComparer.OrdinalIgnoreCase);
    foreach (string key in (IEnumerable<string>) ((PXSelectBase) inventoryItemMaint.itemxrefrecords).Cache.Keys)
      orderedDictionary1.Add((object) key, ((PXSelectBase) inventoryItemMaint.itemxrefrecords).Cache.GetValue((object) inItemXref, key));
    OrderedDictionary orderedDictionary2 = new OrderedDictionary((IEqualityComparer) StringComparer.OrdinalIgnoreCase);
    if (EnumerableExtensions.IsIn<string>(inItemXref.AlternateType, "0VPN", "0CPN") && ((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name.Equals("VendorOrCustomer", StringComparison.OrdinalIgnoreCase))) is EntityValueField entityValueField1 && !string.IsNullOrEmpty(entityValueField1.Value))
      orderedDictionary2.Add((object) typeof (INItemXRef.bAccountID).Name, (object) entityValueField1.Value);
    if (((IEnumerable<EntityField>) targetEntity.Fields).SingleOrDefault<EntityField>((Func<EntityField, bool>) (f => f.Name.Equals("AlternateID", StringComparison.OrdinalIgnoreCase))) is EntityValueField entityValueField2 && !string.IsNullOrEmpty(entityValueField2.Value))
      orderedDictionary2.Add((object) typeof (INItemXRef.alternateID).Name, (object) entityValueField2.Value);
    if (!NonGenericIEnumerableExtensions.Any_((IEnumerable) orderedDictionary2))
      return;
    ((PXSelectBase) inventoryItemMaint.itemxrefrecords).Cache.Update((IDictionary) orderedDictionary1, (IDictionary) orderedDictionary2);
  }
}
