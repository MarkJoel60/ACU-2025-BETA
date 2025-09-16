// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.InvoiceRecognition.APTranRecognizedInventoryItemAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.AP.InvoiceRecognition.DAC;
using PX.Objects.IN;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.AP.InvoiceRecognition;

[PXInt]
internal class APTranRecognizedInventoryItemAttribute : APTranInventoryItemAttribute
{
  private readonly System.Type[] _inventoryRestrictingConditions;

  public APTranRecognizedInventoryItemAttribute()
  {
    this._inventoryRestrictingConditions = this.GetInventoryRestrictingConditions();
    this.IsDBField = false;
  }

  private System.Type[] GetInventoryRestrictingConditions()
  {
    return this.GetAttributes().OfType<PXRestrictorAttribute>().Select<PXRestrictorAttribute, System.Type>((Func<PXRestrictorAttribute, System.Type>) (r => r.RestrictingCondition)).Where<System.Type>((Func<System.Type, bool>) (r => ((IEnumerable<System.Type>) BqlCommand.Decompose(r)).All<System.Type>((Func<System.Type, bool>) (c => !typeof (IBqlField).IsAssignableFrom(c) || c.DeclaringType == typeof (PX.Objects.IN.InventoryItem))))).ToArray<System.Type>();
  }

  public override void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    sender.Graph.FieldUpdating.RemoveHandler(sender.GetItemType(), this._FieldName, new PXFieldUpdating(this.SelectorAttribute.FieldUpdating));
    sender.Graph.FieldUpdating.AddHandler(sender.GetItemType(), this._FieldName, new PXFieldUpdating(this.FieldUpdating));
  }

  public virtual void FieldUpdating(PXCache sender, PXFieldUpdatingEventArgs e)
  {
    if (!(e.Row is APRecognizedTran row))
      return;
    string newValue = e.NewValue as string;
    try
    {
      this.SelectorAttribute.FieldUpdating(sender, e);
      return;
    }
    catch (PXSetPropertyException ex)
    {
    }
    List<(string CD, string Description)> alternateInventory = this.FindAlternateInventory(sender.Graph, newValue);
    row.NumOfFoundIDByAlternate = new int?(alternateInventory.Count);
    if (alternateInventory.Count == 0)
    {
      e.NewValue = (object) null;
    }
    else
    {
      e.NewValue = (object) alternateInventory.First<(string, string)>().CD;
      this.SelectorAttribute.FieldUpdating(sender, e);
    }
  }

  private List<(string CD, string Description)> FindAlternateInventory(
    PXGraph graph,
    string alternateID)
  {
    BqlCommand select1 = (BqlCommand) new SelectFromBase<INItemXRef, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.IN.InventoryItem>.On<INItemXRef.FK.InventoryItem>>>.Where<BqlChainableConditionLite<PX.Data.Match<PX.Data.BQL.BqlField<AccessInfo.userName, IBqlString>.FromCurrent>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<PX.Data.And<Compare<INItemXRef.alternateType, Equal<P.AsString>>>>>.And<BqlOperand<INItemXRef.alternateID, IBqlString>.IsEqual<P.AsString>>>>();
    foreach (System.Type restrictingCondition in this._inventoryRestrictingConditions)
      select1 = select1.WhereAnd(restrictingCondition);
    BqlCommand select2 = select1.WhereAnd<PX.Data.Where<BqlOperand<INItemXRef.bAccountID, IBqlInt>.IsEqual<PX.Data.BQL.BqlField<APRecognizedInvoice.vendorID, IBqlInt>.FromCurrent>>>();
    List<(string, string)> list = new PXView(graph, true, select2).SelectMultiBound(new object[1]
    {
      graph.Caches[typeof (APRecognizedInvoice)].Current
    }, (object) "0VPN", (object) alternateID).Select<object, PX.Objects.IN.InventoryItem>((Func<object, PX.Objects.IN.InventoryItem>) (r => ((PXResult) r).GetItem<PX.Objects.IN.InventoryItem>())).Select<PX.Objects.IN.InventoryItem, (string, string)>((Func<PX.Objects.IN.InventoryItem, (string, string)>) (r => (r.InventoryCD, r.Descr))).ToList<(string, string)>();
    if (list.Count != 0)
      return list;
    return new PXView(graph, true, select1).SelectMulti((object) "GLBL", (object) alternateID).Select<object, PX.Objects.IN.InventoryItem>((Func<object, PX.Objects.IN.InventoryItem>) (r => ((PXResult) r).GetItem<PX.Objects.IN.InventoryItem>())).Select<PX.Objects.IN.InventoryItem, (string, string)>((Func<PX.Objects.IN.InventoryItem, (string, string)>) (r => (r.InventoryCD, r.Descr))).ToList<(string, string)>();
  }
}
