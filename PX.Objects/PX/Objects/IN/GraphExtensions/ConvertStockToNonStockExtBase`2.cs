// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.GraphExtensions.ConvertStockToNonStockExtBase`2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.IN.DAC;
using PX.Objects.PO;
using PX.Objects.SO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.IN.GraphExtensions;

public abstract class ConvertStockToNonStockExtBase<TConvertFromGraph, TConvertToGraph> : 
  PXGraphExtension<TConvertFromGraph>
  where TConvertFromGraph : InventoryItemMaintBase, new()
  where TConvertToGraph : InventoryItemMaintBase, new()
{
  protected const int MaxNumberOfDocuments = 200;
  protected HashSet<PX.Objects.IN.InventoryItem> _persistingConvertedItems;
  public PXAction<PX.Objects.IN.InventoryItem> convert;

  public virtual void Initialize()
  {
    ((PXGraphExtension) this).Initialize();
    GraphHelper.EnsureCachePersistence<INConversionHistory>((PXGraph) (object) this.Base);
    ((PXGraph) (object) this.Base).OnBeforeCommit += (Action<PXGraph>) (graph =>
    {
      HashSet<PX.Objects.IN.InventoryItem> persistingConvertedItems = this._persistingConvertedItems;
      if (persistingConvertedItems == null)
        return;
      EnumerableExtensions.ForEach<PX.Objects.IN.InventoryItem>((IEnumerable<PX.Objects.IN.InventoryItem>) persistingConvertedItems, new Action<PX.Objects.IN.InventoryItem>(this.OnBeforeCommitVerifyNoTransactionsCreated));
    });
    // ISSUE: method pointer
    ((PXGraph) (object) this.Base).OnClear += new PXGraphClearDelegate((object) this, __methodptr(\u003CInitialize\u003Eb__2_1));
  }

  [PXButton(CommitChanges = true)]
  [PXUIField]
  protected virtual IEnumerable Convert(PXAdapter adapter)
  {
    int? inventoryId = ((PXSelectBase<PX.Objects.IN.InventoryItem>) this.Base.Item).Current.InventoryID;
    int num = 0;
    if (inventoryId.GetValueOrDefault() >= num & inventoryId.HasValue)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      ConvertStockToNonStockExtBase<TConvertFromGraph, TConvertToGraph>.\u003C\u003Ec__DisplayClass4_0 cDisplayClass40 = new ConvertStockToNonStockExtBase<TConvertFromGraph, TConvertToGraph>.\u003C\u003Ec__DisplayClass4_0();
      ((PXAction) this.Base.Save).Press();
      // ISSUE: reference to a compiler-generated field
      cDisplayClass40.inventoryID = ((PXSelectBase<PX.Objects.IN.InventoryItem>) this.Base.Item).Current.InventoryID;
      // ISSUE: method pointer
      PXLongOperation.StartOperation((PXGraph) (object) this.Base, new PXToggleAsyncDelegate((object) cDisplayClass40, __methodptr(\u003CConvert\u003Eb__0)));
      return adapter.Get();
    }
    TConvertToGraph instance = PXGraph.CreateInstance<TConvertToGraph>();
    ((PXSelectBase<PX.Objects.IN.InventoryItem>) instance.Item).Current = this.ConvertInventoryItem(instance, ((PXSelectBase<PX.Objects.IN.InventoryItem>) this.Base.Item).Current);
    throw new PXRedirectRequiredException((PXGraph) (object) instance, (string) null);
  }

  protected virtual void VerifyAndConvert(int? inventoryID)
  {
    DateTime utcNow = PXTimeZoneInfo.UtcNow;
    ((PXSelectBase<PX.Objects.IN.InventoryItem>) this.Base.Item).Current = PXResultset<PX.Objects.IN.InventoryItem>.op_Implicit(((PXSelectBase<PX.Objects.IN.InventoryItem>) this.Base.Item).Search<PX.Objects.IN.InventoryItem.inventoryID>((object) inventoryID, Array.Empty<object>()));
    List<string> stringList = new List<string>();
    int num1 = this.Verify(((PXSelectBase<PX.Objects.IN.InventoryItem>) this.Base.Item).Current, stringList);
    if (num1 == 1 && stringList.Count == 1)
      throw new PXException(stringList.First<string>());
    int num2 = num1 + this.VerifyINItemPlan(((PXSelectBase<PX.Objects.IN.InventoryItem>) this.Base.Item).Current, stringList);
    if (num2 == 1 && stringList.Count == 1)
      throw new PXException(stringList.First<string>());
    if (num2 != 0)
      throw new PXException("The item cannot be converted because it is included in documents that are not processed completely. For details, see the trace log: Click Tools > Trace on the form title bar.");
    TConvertToGraph instance = PXGraph.CreateInstance<TConvertToGraph>();
    ((PXSelectBase<PX.Objects.IN.InventoryItem>) instance.Item).Current = this.ConvertInventoryItem(instance, ((PXSelectBase<PX.Objects.IN.InventoryItem>) this.Base.Item).Current);
    GraphHelper.Caches<INConversionHistory>((PXGraph) (object) instance).Insert(new INConversionHistory()
    {
      InventoryID = inventoryID,
      IsStockItem = new bool?(!((PXSelectBase<PX.Objects.IN.InventoryItem>) this.Base.Item).Current.StkItem.Value),
      StartedDateTime = new DateTime?(utcNow)
    });
    throw new PXRedirectRequiredException((PXGraph) (object) instance, (string) null);
  }

  protected virtual int Verify(PX.Objects.IN.InventoryItem item, List<string> errors)
  {
    return 0 + this.VerifyInventoryItem(item, errors) + this.VerifyINRegister(item, errors) + this.VerifyPOOrder(item, errors) + this.VerifyPOReceipt(item, errors) + this.VerifyLandedCost(item, errors) + this.VerifyAPTran(item, errors) + this.VerifySOOrder(item, errors) + this.VerifySOShipment(item, errors) + this.VerifyARTran(item, errors);
  }

  protected virtual int VerifyInventoryItem(PX.Objects.IN.InventoryItem item, List<string> errors)
  {
    if (item.KitItem.GetValueOrDefault())
      throw new PXException("The {0} item cannot be converted because it is a kit. To convert the item, remove the specification related to this item on the Kit Specifications (IN209500) form, and clear the Is a Kit check box on the General tab of the current form.", new object[1]
      {
        (object) item.InventoryCD.Trim()
      });
    if (item.TemplateItemID.HasValue || item.IsTemplate.GetValueOrDefault())
      throw new PXException("Cannot convert the {0} item because it is a matrix item. Matrix item conversion is not supported.", new object[1]
      {
        (object) item.InventoryCD.Trim()
      });
    return 0;
  }

  protected virtual int VerifyINRegister(PX.Objects.IN.InventoryItem item, List<string> errors)
  {
    INTran[] array = GraphHelper.RowCast<INTran>((IEnumerable) PXSelectBase<INTran, PXViewOf<INTran>.BasedOn<SelectFromBase<INTran, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INTran.released, NotEqual<True>>>>>.And<BqlOperand<INTran.inventoryID, IBqlInt>.IsEqual<P.AsInt>>>.Aggregate<To<GroupBy<INTran.docType>, GroupBy<INTran.refNbr>>>>.ReadOnly.Config>.SelectWindowed((PXGraph) (object) this.Base, 0, 200, new object[1]
    {
      (object) item.InventoryID
    })).ToArray<INTran>();
    if (((IEnumerable<INTran>) array).Any<INTran>())
    {
      foreach (IGrouping<string, INTran> source in ((IEnumerable<INTran>) array).GroupBy<INTran, string>((Func<INTran, string>) (d => this.GetINRegisterMessage(d.DocType))))
      {
        string str = PXLocalizer.LocalizeFormat(source.Key, new object[2]
        {
          (object) item.InventoryCD.Trim(),
          (object) string.Join(", ", source.Select<INTran, string>((Func<INTran, string>) (d => d.RefNbr)))
        });
        PXTrace.WriteError(str);
        errors.Add(str);
      }
    }
    return array.Length;
  }

  protected virtual string GetINRegisterMessage(string docType)
  {
    switch (docType)
    {
      case "R":
        return "The {0} item cannot be converted because it is included in the following receipts that are not released: {1}";
      case "I":
        return "The {0} item cannot be converted because it is included in the following issues that are not released: {1}";
      case "T":
        return "The {0} item cannot be converted because it is included in the following transfers that are not released: {1}";
      case "A":
        return "The {0} item cannot be converted because it is included in the following adjustments that are not released: {1}";
      case "P":
      case "D":
        return "The {0} item cannot be converted because it is included in the following kit assembly documents that are not released: {1}";
      default:
        throw new NotImplementedException(docType);
    }
  }

  protected virtual int VerifyPOOrder(PX.Objects.IN.InventoryItem item, List<string> errors)
  {
    PX.Objects.PO.POOrder[] array = GraphHelper.RowCast<PX.Objects.PO.POOrder>((IEnumerable) PXSelectBase<PX.Objects.PO.POOrder, PXViewOf<PX.Objects.PO.POOrder>.BasedOn<SelectFromBase<PX.Objects.PO.POOrder, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.PO.POOrder.cancelled, NotEqual<True>>>>, And<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.PO.POOrder.hold, NotEqual<False>>>>, Or<BqlOperand<PX.Objects.PO.POOrder.approved, IBqlBool>.IsNotEqual<True>>>>.Or<BqlOperand<PX.Objects.PO.POOrder.linesToCompleteCntr, IBqlInt>.IsNotEqual<decimal0>>>>>.And<Exists<Select<PX.Objects.PO.POLine, Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.PO.POLine.inventoryID, Equal<P.AsInt>>>>>.And<PX.Objects.PO.POLine.FK.Order>>>>>>>.ReadOnly.Config>.SelectWindowed((PXGraph) (object) this.Base, 0, 200, new object[1]
    {
      (object) item.InventoryID
    })).ToArray<PX.Objects.PO.POOrder>();
    if (((IEnumerable<PX.Objects.PO.POOrder>) array).Any<PX.Objects.PO.POOrder>())
    {
      string str = PXLocalizer.LocalizeFormat("The {0} item cannot be converted because it is included in the following incomplete purchase orders: {1}", new object[2]
      {
        (object) item.InventoryCD.Trim(),
        (object) string.Join(", ", ((IEnumerable<PX.Objects.PO.POOrder>) array).Select<PX.Objects.PO.POOrder, string>((Func<PX.Objects.PO.POOrder, string>) (c => c.OrderNbr)))
      });
      PXTrace.WriteError(str);
      errors.Add(str);
    }
    return array.Length;
  }

  protected virtual int VerifyPOReceipt(PX.Objects.IN.InventoryItem item, List<string> errors)
  {
    PX.Objects.PO.POReceiptLine[] array = GraphHelper.RowCast<PX.Objects.PO.POReceiptLine>((IEnumerable) PXSelectBase<PX.Objects.PO.POReceiptLine, PXViewOf<PX.Objects.PO.POReceiptLine>.BasedOn<SelectFromBase<PX.Objects.PO.POReceiptLine, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.PO.POReceiptLine.receiptType, NotEqual<POReceiptType.transferreceipt>>>>, And<BqlOperand<PX.Objects.PO.POReceiptLine.inventoryID, IBqlInt>.IsEqual<P.AsInt>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.PO.POReceiptLine.unbilledQty, NotEqual<decimal0>>>>>.Or<BqlOperand<PX.Objects.PO.POReceiptLine.released, IBqlBool>.IsNotEqual<True>>>>.Aggregate<To<GroupBy<PX.Objects.PO.POReceiptLine.receiptType>, GroupBy<PX.Objects.PO.POReceiptLine.receiptNbr>>>>.ReadOnly.Config>.SelectWindowed((PXGraph) (object) this.Base, 0, 200, new object[1]
    {
      (object) item.InventoryID
    })).ToArray<PX.Objects.PO.POReceiptLine>();
    if (((IEnumerable<PX.Objects.PO.POReceiptLine>) array).Any<PX.Objects.PO.POReceiptLine>())
    {
      foreach (IGrouping<bool?, PX.Objects.PO.POReceiptLine> source in ((IEnumerable<PX.Objects.PO.POReceiptLine>) array).GroupBy<PX.Objects.PO.POReceiptLine, bool?>((Func<PX.Objects.PO.POReceiptLine, bool?>) (d => d.Released)))
      {
        string str = PXLocalizer.LocalizeFormat(!source.Key.GetValueOrDefault() ? "The {0} item cannot be converted because it is included in the following purchase receipts that are not released: {1}" : "The {0} item cannot be converted because it is included in the following unbilled purchase receipts or purchase returns: {1}", new object[2]
        {
          (object) item.InventoryCD.Trim(),
          (object) string.Join(", ", source.Select<PX.Objects.PO.POReceiptLine, string>((Func<PX.Objects.PO.POReceiptLine, string>) (d => d.ReceiptNbr)))
        });
        PXTrace.WriteError(str);
        errors.Add(str);
      }
    }
    return array.Length;
  }

  protected virtual int VerifyLandedCost(PX.Objects.IN.InventoryItem item, List<string> errors)
  {
    POLandedCostDoc[] array = GraphHelper.RowCast<POLandedCostDoc>((IEnumerable) PXSelectBase<POLandedCostDoc, PXViewOf<POLandedCostDoc>.BasedOn<SelectFromBase<POLandedCostDoc, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<POLandedCostDoc.released, NotEqual<True>>>>>.And<Exists<Select<POLandedCostReceiptLine, Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<POLandedCostReceiptLine.inventoryID, Equal<P.AsInt>>>>>.And<POLandedCostReceiptLine.FK.LandedCostDocument>>>>>>>.ReadOnly.Config>.SelectWindowed((PXGraph) (object) this.Base, 0, 200, new object[1]
    {
      (object) item.InventoryID
    })).ToArray<POLandedCostDoc>();
    if (((IEnumerable<POLandedCostDoc>) array).Any<POLandedCostDoc>())
    {
      string str = PXLocalizer.LocalizeFormat("The {0} item cannot be converted because it is included in the following landed cost documents that are not released: {1}", new object[2]
      {
        (object) item.InventoryCD.Trim(),
        (object) string.Join(", ", ((IEnumerable<POLandedCostDoc>) array).Select<POLandedCostDoc, string>((Func<POLandedCostDoc, string>) (c => c.RefNbr)))
      });
      PXTrace.WriteError(str);
      errors.Add(str);
    }
    return array.Length;
  }

  protected virtual int VerifyAPTran(PX.Objects.IN.InventoryItem item, List<string> errors)
  {
    PX.Objects.AP.APRegister[] array = GraphHelper.RowCast<PX.Objects.AP.APRegister>((IEnumerable) PXSelectBase<PX.Objects.AP.APRegister, PXViewOf<PX.Objects.AP.APRegister>.BasedOn<SelectFromBase<PX.Objects.AP.APRegister, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AP.APRegister.released, NotEqual<True>>>>, And<BqlOperand<PX.Objects.AP.APRegister.docType, IBqlString>.IsIn<P.AsString.ASCII>>>>.And<Exists<Select<PX.Objects.AP.APTran, Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<CompositeKey<Field<PX.Objects.AP.APTran.tranType>.IsRelatedTo<PX.Objects.AP.APRegister.docType>, Field<PX.Objects.AP.APTran.refNbr>.IsRelatedTo<PX.Objects.AP.APRegister.refNbr>>.WithTablesOf<PX.Objects.AP.APRegister, PX.Objects.AP.APTran>>, And<BqlOperand<PX.Objects.AP.APTran.released, IBqlBool>.IsNotEqual<True>>>>.And<BqlOperand<PX.Objects.AP.APTran.inventoryID, IBqlInt>.IsEqual<P.AsInt>>>>>>>>.ReadOnly.Config>.SelectWindowed((PXGraph) (object) this.Base, 0, 200, new object[2]
    {
      (object) this.GetAPTranTypes().ToArray(),
      (object) item.InventoryID
    })).ToArray<PX.Objects.AP.APRegister>();
    if (((IEnumerable<PX.Objects.AP.APRegister>) array).Any<PX.Objects.AP.APRegister>())
    {
      foreach (IGrouping<string, PX.Objects.AP.APRegister> source in ((IEnumerable<PX.Objects.AP.APRegister>) array).GroupBy<PX.Objects.AP.APRegister, string>((Func<PX.Objects.AP.APRegister, string>) (d => this.GetAPTranMessage(d.DocType))))
      {
        string str = PXLocalizer.LocalizeFormat(source.Key, new object[2]
        {
          (object) item.InventoryCD.Trim(),
          (object) string.Join(", ", source.Select<PX.Objects.AP.APRegister, string>((Func<PX.Objects.AP.APRegister, string>) (d => d.RefNbr)))
        });
        PXTrace.WriteError(str);
        errors.Add(str);
      }
    }
    return array.Length;
  }

  protected virtual List<string> GetAPTranTypes()
  {
    return new List<string>() { "INV", "ADR", "PPM" };
  }

  protected virtual string GetAPTranMessage(string apDocType)
  {
    switch (apDocType)
    {
      case "INV":
        return "The {0} item cannot be converted because it is included in the following AP bills that are not released: {1}";
      case "ADR":
        return "The {0} item cannot be converted because it is included in the following debit adjustments that are not released: {1}";
      case "PPM":
        return "The {0} item cannot be converted because it is included in the following prepayment requests that are not released: {1}";
      default:
        throw new NotImplementedException(apDocType);
    }
  }

  protected virtual int VerifySOOrder(PX.Objects.IN.InventoryItem item, List<string> errors)
  {
    PX.Objects.SO.SOOrder[] array = GraphHelper.RowCast<PX.Objects.SO.SOOrder>((IEnumerable) PXSelectBase<PX.Objects.SO.SOOrder, PXViewOf<PX.Objects.SO.SOOrder>.BasedOn<SelectFromBase<PX.Objects.SO.SOOrder, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.SO.SOOrder.cancelled, NotEqual<True>>>>, And<BqlOperand<PX.Objects.SO.SOOrder.completed, IBqlBool>.IsNotEqual<True>>>>.And<Exists<Select<PX.Objects.SO.SOLine, Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.SO.SOLine.inventoryID, Equal<P.AsInt>>>>>.And<PX.Objects.SO.SOLine.FK.Order>>>>>>>.ReadOnly.Config>.SelectWindowed((PXGraph) (object) this.Base, 0, 200, new object[1]
    {
      (object) item.InventoryID
    })).ToArray<PX.Objects.SO.SOOrder>();
    if (((IEnumerable<PX.Objects.SO.SOOrder>) array).Any<PX.Objects.SO.SOOrder>())
    {
      string str = PXLocalizer.LocalizeFormat("The {0} item cannot be converted because it is included in the following incomplete sales orders: {1}", new object[2]
      {
        (object) item.InventoryCD.Trim(),
        (object) string.Join(", ", ((IEnumerable<PX.Objects.SO.SOOrder>) array).Select<PX.Objects.SO.SOOrder, string>((Func<PX.Objects.SO.SOOrder, string>) (c => c.OrderNbr)))
      });
      PXTrace.WriteError(str);
      errors.Add(str);
    }
    return array.Length;
  }

  protected virtual int VerifySOShipment(PX.Objects.IN.InventoryItem item, List<string> errors)
  {
    PX.Objects.SO.SOShipment[] array = GraphHelper.RowCast<PX.Objects.SO.SOShipment>((IEnumerable) PXSelectBase<PX.Objects.SO.SOShipment, PXViewOf<PX.Objects.SO.SOShipment>.BasedOn<SelectFromBase<PX.Objects.SO.SOShipment, TypeArrayOf<IFbqlJoin>.Empty>.Where<Exists<Select2<PX.Objects.SO.SOOrderShipment, InnerJoin<SOShipLine, On<SOShipLine.FK.OrderShipment>>, Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOShipLine.inventoryID, Equal<P.AsInt>>>>, And<PX.Objects.SO.SOOrderShipment.FK.Shipment>>, And<BqlOperand<PX.Objects.SO.SOOrderShipment.createINDoc, IBqlBool>.IsEqual<True>>>>.And<BqlOperand<PX.Objects.SO.SOOrderShipment.invtRefNbr, IBqlString>.IsNull>>>>>>.ReadOnly.Config>.SelectWindowed((PXGraph) (object) this.Base, 0, 200, new object[1]
    {
      (object) item.InventoryID
    })).ToArray<PX.Objects.SO.SOShipment>();
    if (((IEnumerable<PX.Objects.SO.SOShipment>) array).Any<PX.Objects.SO.SOShipment>())
    {
      string str = PXLocalizer.LocalizeFormat("The {0} item cannot be converted because inventory has not been updated for it in the following shipments: {1}", new object[2]
      {
        (object) item.InventoryCD.Trim(),
        (object) string.Join(", ", ((IEnumerable<PX.Objects.SO.SOShipment>) array).Select<PX.Objects.SO.SOShipment, string>((Func<PX.Objects.SO.SOShipment, string>) (c => c.ShipmentNbr)))
      });
      PXTrace.WriteError(str);
      errors.Add(str);
    }
    return array.Length;
  }

  protected virtual int VerifyARTran(PX.Objects.IN.InventoryItem item, List<string> errors)
  {
    PX.Objects.AR.ARTran[] array = GraphHelper.RowCast<PX.Objects.AR.ARTran>((IEnumerable) PXSelectBase<PX.Objects.AR.ARTran, PXViewOf<PX.Objects.AR.ARTran>.BasedOn<SelectFromBase<PX.Objects.AR.ARTran, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AR.ARTran.tranType, In<P.AsString.ASCII>>>>, And<BqlOperand<PX.Objects.AR.ARTran.released, IBqlBool>.IsNotEqual<True>>>>.And<BqlOperand<PX.Objects.AR.ARTran.inventoryID, IBqlInt>.IsEqual<P.AsInt>>>.Aggregate<To<GroupBy<PX.Objects.AR.ARTran.tranType>, GroupBy<PX.Objects.AR.ARTran.refNbr>>>>.ReadOnly.Config>.SelectWindowed((PXGraph) (object) this.Base, 0, 200, new object[2]
    {
      (object) this.GetARTranTypes().ToArray(),
      (object) item.InventoryID
    })).ToArray<PX.Objects.AR.ARTran>();
    if (((IEnumerable<PX.Objects.AR.ARTran>) array).Any<PX.Objects.AR.ARTran>())
    {
      foreach (IGrouping<string, PX.Objects.AR.ARTran> source in ((IEnumerable<PX.Objects.AR.ARTran>) array).GroupBy<PX.Objects.AR.ARTran, string>((Func<PX.Objects.AR.ARTran, string>) (d => this.GetARTranMessage(d.TranType))))
      {
        string str = PXLocalizer.LocalizeFormat(source.Key, new object[2]
        {
          (object) item.InventoryCD.Trim(),
          (object) string.Join(", ", source.Select<PX.Objects.AR.ARTran, string>((Func<PX.Objects.AR.ARTran, string>) (c => c.RefNbr)))
        });
        PXTrace.WriteError(str);
        errors.Add(str);
      }
    }
    return array.Length;
  }

  protected virtual List<string> GetARTranTypes()
  {
    return new List<string>()
    {
      "INV",
      "DRM",
      "CRM",
      "CSL",
      "RCS"
    };
  }

  protected virtual string GetARTranMessage(string apDocType)
  {
    switch (apDocType)
    {
      case "INV":
      case "DRM":
      case "CRM":
        return "The {0} item cannot be converted because it is included in the following invoices that are not released: {1}";
      case "CSL":
      case "RCS":
        return "The {0} item cannot be converted because it is included in the following cash sale documents that are not released: {1} ";
      default:
        throw new NotImplementedException(apDocType);
    }
  }

  protected virtual int VerifyINItemPlan(PX.Objects.IN.InventoryItem item, List<string> errors)
  {
    INItemPlan[] array = GraphHelper.RowCast<INItemPlan>((IEnumerable) PXSelectBase<INItemPlan, PXViewOf<INItemPlan>.BasedOn<SelectFromBase<INItemPlan, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INItemPlan.inventoryID, Equal<P.AsInt>>>>>.And<BqlOperand<INItemPlan.planType, IBqlString>.IsNotEqual<INPlanConstants.plan90>>>.Aggregate<To<GroupBy<INItemPlan.planType>>>>.ReadOnly.Config>.SelectWindowed((PXGraph) (object) this.Base, 0, 200, new object[1]
    {
      (object) item.InventoryID
    })).ToArray<INItemPlan>();
    if (((IEnumerable<INItemPlan>) array).Any<INItemPlan>())
    {
      foreach (IGrouping<string, INItemPlan> source in ((IEnumerable<INItemPlan>) array).GroupBy<INItemPlan, string>((Func<INItemPlan, string>) (p => this.GetINItemPlanMessage(p))))
      {
        string str = PXLocalizer.LocalizeFormat(source.Key, new object[2]
        {
          (object) item.InventoryCD.Trim(),
          (object) string.Join(", ", source.Select<INItemPlan, string>((Func<INItemPlan, string>) (g => this.GetINItemPlanName(g.PlanType))).Distinct<string>())
        });
        PXTrace.WriteError(str);
        errors.Add(str);
      }
    }
    return array.Length;
  }

  protected virtual string GetINItemPlanMessage(INItemPlan itemPlan)
  {
    if (!(itemPlan.PlanType == "90"))
      return "The {0} item cannot be converted because it has the following item plans: {1}";
    string fixedSource = itemPlan.FixedSource;
    return (fixedSource != null ? (EnumerableExtensions.IsIn<string>(fixedSource, "X", "T") ? 1 : 0) : 0) != 0 ? "The {0} item cannot be converted because it is included in at least one transfer request. To process the transfer requests, open the Create Transfer Orders (SO509000) form." : "The {0} item cannot be converted because it is included in at least one purchase request. To process the purchase requests, open the Create Purchase Orders (PO505000) form.";
  }

  protected virtual string GetINItemPlanName(string planType)
  {
    System.Type inclQtyField = INPlanConstants.ToInclQtyField(planType);
    return inclQtyField != (System.Type) null && ((PXCache) GraphHelper.Caches<INPlanType>((PXGraph) (object) this.Base)).GetStateExt((object) null, inclQtyField.Name) is PXIntState stateExt && !string.IsNullOrEmpty(((PXFieldState) stateExt).DisplayName) ? ((PXFieldState) stateExt).DisplayName : INPlanType.PK.Find((PXGraph) (object) this.Base, planType)?.Descr ?? planType;
  }

  protected virtual void VerifySingleINTran(PX.Objects.IN.InventoryItem item)
  {
    INTran inTran = PXResultset<INTran>.op_Implicit(PXSelectBase<INTran, PXViewOf<INTran>.BasedOn<SelectFromBase<INTran, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INTran.released, NotEqual<True>>>>>.And<BqlOperand<INTran.inventoryID, IBqlInt>.IsEqual<P.AsInt>>>>.ReadOnly.Config>.Select((PXGraph) (object) this.Base, new object[1]
    {
      (object) item.InventoryID
    }));
    if (inTran != null)
      throw new PXException(this.GetINRegisterMessage(inTran.DocType), new object[2]
      {
        (object) item?.InventoryCD.Trim(),
        (object) inTran.RefNbr
      });
  }

  protected virtual void VerifySingleINItemPlan(PX.Objects.IN.InventoryItem item)
  {
    INItemPlan inItemPlan = PXResultset<INItemPlan>.op_Implicit(PXSelectBase<INItemPlan, PXViewOf<INItemPlan>.BasedOn<SelectFromBase<INItemPlan, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<INItemPlan.inventoryID, IBqlInt>.IsEqual<P.AsInt>>>.ReadOnly.Config>.Select((PXGraph) (object) this.Base, new object[1]
    {
      (object) item.InventoryID
    }));
    if (inItemPlan != null)
      throw new PXException("The {0} item cannot be converted because it has the following item plans: {1}", new object[2]
      {
        (object) item?.InventoryCD.Trim(),
        (object) this.GetINItemPlanName(inItemPlan.PlanType)
      });
  }

  protected virtual void OnBeforeCommitVerifyNoTransactionsCreated(PX.Objects.IN.InventoryItem item)
  {
    this.VerifySingleINTran(item);
    this.VerifySingleINItemPlan(item);
  }

  protected virtual PX.Objects.IN.InventoryItem ConvertInventoryItem(
    TConvertToGraph graph,
    PX.Objects.IN.InventoryItem source)
  {
    this.DeleteRelatedRecords(graph, source.InventoryID);
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    PXFieldDefaulting pxFieldDefaulting = ConvertStockToNonStockExtBase<TConvertFromGraph, TConvertToGraph>.\u003C\u003Ec.\u003C\u003E9__27_0 ?? (ConvertStockToNonStockExtBase<TConvertFromGraph, TConvertToGraph>.\u003C\u003Ec.\u003C\u003E9__27_0 = new PXFieldDefaulting((object) ConvertStockToNonStockExtBase<TConvertFromGraph, TConvertToGraph>.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CConvertInventoryItem\u003Eb__27_0)));
    ((PXGraph) (object) graph).FieldDefaulting.AddHandler<PX.Objects.IN.InventoryItem.itemClassID>(pxFieldDefaulting);
    ((PXGraph) (object) graph).FieldDefaulting.AddHandler<PX.Objects.IN.InventoryItem.parentItemClassID>(pxFieldDefaulting);
    ((PXGraph) (object) graph).FieldDefaulting.AddHandler<PX.Objects.IN.InventoryItem.postClassID>(pxFieldDefaulting);
    try
    {
      PX.Objects.IN.InventoryItem newItem1 = ((PXSelectBase<PX.Objects.IN.InventoryItem>) graph.Item).Insert(new PX.Objects.IN.InventoryItem()
      {
        IsConversionMode = new bool?(true)
      });
      ((PXSelectBase) graph.Item).Cache.Clear();
      PX.Objects.IN.InventoryItem newItem2 = this.ConvertMainFields(graph, source, newItem1);
      PX.Objects.IN.InventoryItem newItem3 = this.ConvertPriceManagement(graph, source, newItem2);
      PX.Objects.IN.InventoryItem newItem4 = this.ConvertPackaging(graph, source, newItem3);
      PX.Objects.IN.InventoryItem newItem5 = this.ConvertCommerceData(graph, source, newItem4);
      PX.Objects.IN.InventoryItem newItem6 = this.ConvertDeferral(graph, source, newItem5);
      PX.Objects.IN.InventoryItem inventoryItem = this.ConvertCurySettings(graph, source, newItem6);
      PX.Objects.IN.InventoryItem newItem7 = ((PXSelectBase<PX.Objects.IN.InventoryItem>) graph.Item).Update(inventoryItem);
      PXDBLocalizableStringAttribute.CopyTranslations<PX.Objects.IN.InventoryItem.descr, PX.Objects.IN.InventoryItem.descr>((PXGraph) (object) graph, (object) newItem7, (object) source);
      PXDBLocalizableStringAttribute.CopyTranslations<PX.Objects.IN.InventoryItem.body, PX.Objects.IN.InventoryItem.body>((PXGraph) (object) graph, (object) newItem7, (object) source);
      this.ConvertVendorInventory(graph, source, newItem7);
      return newItem7;
    }
    finally
    {
      ((PXGraph) (object) graph).FieldDefaulting.RemoveHandler<PX.Objects.IN.InventoryItem.itemClassID>(pxFieldDefaulting);
      ((PXGraph) (object) graph).FieldDefaulting.RemoveHandler<PX.Objects.IN.InventoryItem.parentItemClassID>(pxFieldDefaulting);
      ((PXGraph) (object) graph).FieldDefaulting.RemoveHandler<PX.Objects.IN.InventoryItem.postClassID>(pxFieldDefaulting);
    }
  }

  protected virtual PX.Objects.IN.InventoryItem ConvertMainFields(
    TConvertToGraph graph,
    PX.Objects.IN.InventoryItem source,
    PX.Objects.IN.InventoryItem newItem)
  {
    newItem.NoteID = source.NoteID;
    newItem.InventoryID = source.InventoryID;
    PX.Objects.IN.InventoryItem inventoryItem = newItem;
    int? inventoryId = source.InventoryID;
    int num = 0;
    bool? nullable = new bool?(inventoryId.GetValueOrDefault() >= num & inventoryId.HasValue);
    inventoryItem.IsConversionMode = nullable;
    newItem.IsConverted = newItem.IsConversionMode;
    newItem.StkItem = new bool?(!source.StkItem.Value);
    newItem.InventoryCD = source.InventoryCD;
    newItem.ItemStatus = source.ItemStatus;
    newItem.Descr = source.Descr;
    newItem.ProductWorkgroupID = source.ProductWorkgroupID;
    newItem.ProductManagerID = source.ProductManagerID;
    newItem.KitItem = source.KitItem;
    newItem.TaxCategoryID = source.TaxCategoryID;
    newItem.Body = source.Body;
    newItem.BaseUnit = source.BaseUnit;
    newItem.PurchaseUnit = source.PurchaseUnit;
    newItem.SalesUnit = source.SalesUnit;
    newItem.DecimalBaseUnit = source.DecimalBaseUnit;
    newItem.DecimalPurchaseUnit = source.DecimalPurchaseUnit;
    newItem.DecimalSalesUnit = source.DecimalSalesUnit;
    newItem.tstamp = source.tstamp;
    return newItem;
  }

  protected virtual void DeleteRelatedRecords(TConvertToGraph graph, int? inventoryID)
  {
  }

  protected virtual PX.Objects.IN.InventoryItem ConvertPriceManagement(
    TConvertToGraph graph,
    PX.Objects.IN.InventoryItem source,
    PX.Objects.IN.InventoryItem newItem)
  {
    newItem.PriceClassID = source.PriceClassID;
    newItem.PriceWorkgroupID = source.PriceWorkgroupID;
    newItem.PriceManagerID = source.PriceManagerID;
    newItem.Commisionable = source.Commisionable;
    newItem.MinGrossProfitPct = source.MinGrossProfitPct;
    newItem.MarkupPct = source.MarkupPct;
    return newItem;
  }

  protected virtual PX.Objects.IN.InventoryItem ConvertPackaging(
    TConvertToGraph graph,
    PX.Objects.IN.InventoryItem source,
    PX.Objects.IN.InventoryItem newItem)
  {
    newItem.BaseItemWeight = source.BaseItemWeight;
    newItem.WeightUOM = source.WeightUOM;
    newItem.BaseItemVolume = source.BaseItemVolume;
    newItem.VolumeUOM = source.VolumeUOM;
    newItem.UndershipThreshold = source.UndershipThreshold;
    newItem.OvershipThreshold = source.OvershipThreshold;
    return newItem;
  }

  protected virtual PX.Objects.IN.InventoryItem ConvertCommerceData(
    TConvertToGraph graph,
    PX.Objects.IN.InventoryItem source,
    PX.Objects.IN.InventoryItem newItem)
  {
    newItem.ExportToExternal = source.ExportToExternal;
    newItem.Visibility = source.Visibility;
    newItem.Availability = source.Availability;
    newItem.NotAvailMode = source.NotAvailMode;
    return newItem;
  }

  protected virtual PX.Objects.IN.InventoryItem ConvertDeferral(
    TConvertToGraph graph,
    PX.Objects.IN.InventoryItem source,
    PX.Objects.IN.InventoryItem newItem)
  {
    ((PXSelectBase<PX.Objects.IN.InventoryItem>) graph.Item).SetValueExt<PX.Objects.IN.InventoryItem.deferredCode>(newItem, (object) source.DeferredCode);
    newItem.UseParentSubID = source.UseParentSubID;
    return newItem;
  }

  protected virtual PX.Objects.IN.InventoryItem ConvertCurySettings(
    TConvertToGraph graph,
    PX.Objects.IN.InventoryItem source,
    PX.Objects.IN.InventoryItem newItem)
  {
    newItem.DfltSiteID = source.DfltSiteID;
    newItem.RecPrice = source.RecPrice;
    newItem.BasePrice = source.BasePrice;
    newItem.PendingStdCost = source.PendingStdCost;
    newItem.PendingStdCostDate = source.PendingStdCostDate;
    newItem.StdCost = source.StdCost;
    newItem.StdCostDate = source.StdCostDate;
    newItem.LastStdCost = source.LastStdCost;
    newItem = ((PXSelectBase<PX.Objects.IN.InventoryItem>) graph.Item).Update(newItem);
    foreach (PXResult<InventoryItemCurySettings> pxResult in ((PXSelectBase<InventoryItemCurySettings>) graph.AllItemCurySettings).Select(new object[1]
    {
      (object) newItem.InventoryID
    }))
    {
      InventoryItemCurySettings itemCurySettings = PXResult<InventoryItemCurySettings>.op_Implicit(pxResult);
      int? dfltSiteId = itemCurySettings.DfltSiteID;
      int? receiptLocationId = itemCurySettings.DfltReceiptLocationID;
      int? dfltShipLocationId = itemCurySettings.DfltShipLocationID;
      itemCurySettings.DfltSiteID = new int?();
      itemCurySettings.DfltReceiptLocationID = new int?();
      itemCurySettings.DfltShipLocationID = new int?();
      ((PXSelectBase<InventoryItemCurySettings>) graph.AllItemCurySettings).Update(itemCurySettings);
      itemCurySettings.DfltSiteID = dfltSiteId;
      ((PXSelectBase<InventoryItemCurySettings>) graph.AllItemCurySettings).Update(itemCurySettings);
    }
    return newItem;
  }

  protected virtual void ConvertVendorInventory(
    TConvertToGraph graph,
    PX.Objects.IN.InventoryItem source,
    PX.Objects.IN.InventoryItem newItem)
  {
    foreach (PXResult<POVendorInventory> pxResult in PXSelectBase<POVendorInventory, PXViewOf<POVendorInventory>.BasedOn<SelectFromBase<POVendorInventory, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<POVendorInventory.inventoryID, IBqlInt>.IsEqual<P.AsInt>>>.Config>.Select((PXGraph) (object) graph, new object[1]
    {
      (object) newItem.InventoryID
    }))
    {
      ((PXSelectBase) graph.VendorItems).Cache.SetDefaultExt<POVendorInventory.subItemID>((object) pxResult);
      ((PXSelectBase) graph.VendorItems).Cache.SetDefaultExt<POVendorInventory.addLeadTimeDays>((object) pxResult);
      ((PXSelectBase) graph.VendorItems).Cache.SetDefaultExt<POVendorInventory.minOrdFreq>((object) pxResult);
      ((PXSelectBase) graph.VendorItems).Cache.SetDefaultExt<POVendorInventory.minOrdFreq>((object) pxResult);
      ((PXSelectBase) graph.VendorItems).Cache.SetDefaultExt<POVendorInventory.minOrdQty>((object) pxResult);
      ((PXSelectBase) graph.VendorItems).Cache.SetDefaultExt<POVendorInventory.maxOrdQty>((object) pxResult);
      ((PXSelectBase) graph.VendorItems).Cache.SetDefaultExt<POVendorInventory.lotSize>((object) pxResult);
      ((PXSelectBase) graph.VendorItems).Cache.SetDefaultExt<POVendorInventory.eRQ>((object) pxResult);
      ((PXSelectBase<POVendorInventory>) graph.VendorItems).Update(PXResult<POVendorInventory>.op_Implicit(pxResult));
    }
  }

  protected virtual void _(PX.Data.Events.RowSelected<PX.Objects.IN.InventoryItem> e)
  {
    if (e.Row == null || PXUIFieldAttribute.GetErrorOnly<PX.Objects.IN.InventoryItem.itemClassID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.IN.InventoryItem>>) e).Cache, (object) e.Row) != null)
      return;
    PXUIFieldAttribute.SetWarning<PX.Objects.IN.InventoryItem.itemClassID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.IN.InventoryItem>>) e).Cache, (object) e.Row, !e.Row.IsConversionMode.GetValueOrDefault() || !NonGenericIEnumerableExtensions.Any_(((PXSelectBase) this.Base.Answers).Cache.Deleted) ? (string) null : "The attributes of the current item class do not match the attributes of the previous item class. Review the attributes on the Attributes tab before you save the item.");
  }

  protected virtual void _(PX.Data.Events.RowSelected<CSAnswers> e)
  {
    if (e.Row == null)
      return;
    int? parentItemClassId = (int?) ((PXSelectBase<PX.Objects.IN.InventoryItem>) this.Base.Item).Current?.ParentItemClassID;
    if (!parentItemClassId.HasValue || !(((IEnumerable<CRAttribute.AttributeExt>) CRAttribute.EntityAttributes(typeof (PX.Objects.IN.InventoryItem), parentItemClassId.ToString())).FirstOrDefault<CRAttribute.AttributeExt>()?.ID == e.Row.AttributeID))
      return;
    PX.Objects.IN.InventoryItem current = ((PXSelectBase<PX.Objects.IN.InventoryItem>) this.Base.Item).Current;
    string str = (current != null ? (current.IsConversionMode.GetValueOrDefault() ? 1 : 0) : 0) == 0 || !NonGenericIEnumerableExtensions.Any_(((PXSelectBase) this.Base.Answers).Cache.Deleted) ? (string) null : "The attributes of the current item class do not match the attributes of the previous item class. Review the attributes on the Attributes tab before you save the item.";
    PXUIFieldAttribute.SetError(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<CSAnswers>>) e).Cache, (object) e.Row, "attributeID", str, (string) null, false, (PXErrorLevel) 3);
  }

  protected virtual void _(PX.Data.Events.RowPersisting<PX.Objects.IN.InventoryItem> e)
  {
    if (!e.Row.IsConversionMode.GetValueOrDefault() || PXDBOperationExt.Command(e.Operation) != 1)
      return;
    if (this._persistingConvertedItems == null)
      this._persistingConvertedItems = new HashSet<PX.Objects.IN.InventoryItem>((IEqualityComparer<PX.Objects.IN.InventoryItem>) PXCacheEx.GetComparer(((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<PX.Objects.IN.InventoryItem>>) e).Cache));
    this._persistingConvertedItems.Add(e.Row);
  }

  protected virtual void _(PX.Data.Events.RowPersisted<PX.Objects.IN.InventoryItem> e)
  {
    if (e.TranStatus != 1)
      return;
    e.Row.IsConversionMode = new bool?(false);
  }
}
