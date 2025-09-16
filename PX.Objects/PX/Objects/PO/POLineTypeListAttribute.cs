// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.POLineTypeListAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.CS;
using PX.Objects.IN;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

#nullable disable
namespace PX.Objects.PO;

/// <summary>
/// Specialized PXStringList attribute for PO Order Line types.<br />
/// Provides a list of possible values for line types depending upon InventoryID <br />
/// specified in the row. For stock- and not-stock inventory items the allowed values <br />
/// are different. If item is changed and old value is not compatible with inventory item <br />
/// - it will defaulted to the applicable value.<br />
/// <example>
/// [POLineTypeList(typeof(POLine.inventoryID))]
/// </example>
/// </summary>
public class POLineTypeListAttribute : 
  PXStringListExtAttribute,
  IPXFieldVerifyingSubscriber,
  IPXFieldDefaultingSubscriber
{
  protected Type _inventoryID;

  /// <summary>
  /// Ctor, short version. List of allowed values is defined as POLineType.GoodsForInventory, POLineType.NonStock, POLineType.Service, POLineType.Freight, POLineType.Description
  /// </summary>
  /// <param name="inventoryID">Must be IBqlField. Represents an InventoryID field in the row</param>
  public POLineTypeListAttribute(Type inventoryID)
    : this(inventoryID, new Tuple<string, string>[5]
    {
      PXStringListAttribute.Pair("GI", "Goods for IN"),
      PXStringListAttribute.Pair("NS", "Non-Stock"),
      PXStringListAttribute.Pair("SV", "Service"),
      PXStringListAttribute.Pair("FT", "Freight"),
      PXStringListAttribute.Pair("DN", "Description")
    })
  {
  }

  protected POLineTypeListAttribute(Type inventoryID, Tuple<string, string>[] allowedPairs)
    : this(inventoryID, ((IEnumerable<Tuple<string, string>>) allowedPairs).Select<Tuple<string, string>, string>((Func<Tuple<string, string>, string>) (t => t.Item1)).ToArray<string>(), ((IEnumerable<Tuple<string, string>>) allowedPairs).Select<Tuple<string, string>, string>((Func<Tuple<string, string>, string>) (t => t.Item2)).ToArray<string>())
  {
  }

  /// <summary>
  /// Ctor. Shorter version. User may define a list of allowed values and their descriptions
  /// List for hidden values is defined as POLineType.GoodsForInventory, POLineType.GoodsForSalesOrder,
  /// POLineType.GoodsForReplenishment, POLineType.GoodsForDropShip, POLineType.NonStockForDropShip,
  /// POLineType.NonStockForSalesOrder, POLineType.NonStock, POLineType.Service,
  /// POLineType.Freight, POLineType.Description - it includes all the values for the POLine types.
  /// </summary>
  /// <param name="inventoryID">Must be IBqlField. Represents an InventoryID field in the row</param>
  /// <param name="allowedValues">List of allowed values. </param>
  /// <param name="allowedLabels">List of allowed values labels. Will be shown in the combo-box in UI</param>
  public POLineTypeListAttribute(Type inventoryID, string[] allowedValues, string[] allowedLabels)
    : this(inventoryID, allowedValues, allowedLabels, new Tuple<string, string>[14]
    {
      PXStringListAttribute.Pair("GI", "Goods for IN"),
      PXStringListAttribute.Pair("GS", "Goods for SO"),
      PXStringListAttribute.Pair("GF", "Goods for FS"),
      PXStringListAttribute.Pair("GR", "Goods for RP"),
      PXStringListAttribute.Pair("GP", "Goods for Drop-Ship"),
      PXStringListAttribute.Pair("NP", "Non-Stock for Drop-Ship"),
      PXStringListAttribute.Pair("NO", "Non-Stock for SO"),
      PXStringListAttribute.Pair("NF", "Non-Stock for FS"),
      PXStringListAttribute.Pair("PG", "Goods for Project"),
      PXStringListAttribute.Pair("PN", "Non-Stock for Project"),
      PXStringListAttribute.Pair("NS", "Non-Stock"),
      PXStringListAttribute.Pair("SV", "Service"),
      PXStringListAttribute.Pair("FT", "Freight"),
      PXStringListAttribute.Pair("DN", "Description")
    })
  {
  }

  protected POLineTypeListAttribute(
    Type inventoryID,
    string[] allowedValues,
    string[] allowedLabels,
    Tuple<string, string>[] hiddenPairs)
    : this(inventoryID, allowedValues, allowedLabels, ((IEnumerable<Tuple<string, string>>) hiddenPairs).Select<Tuple<string, string>, string>((Func<Tuple<string, string>, string>) (t => t.Item1)).ToArray<string>(), ((IEnumerable<Tuple<string, string>>) hiddenPairs).Select<Tuple<string, string>, string>((Func<Tuple<string, string>, string>) (t => t.Item2)).ToArray<string>())
  {
  }

  protected POLineTypeListAttribute(
    Type inventoryID,
    Tuple<string, string>[] allowedPairs,
    Tuple<string, string>[] hiddenPairs)
    : this(inventoryID, ((IEnumerable<Tuple<string, string>>) allowedPairs).Select<Tuple<string, string>, string>((Func<Tuple<string, string>, string>) (t => t.Item1)).ToArray<string>(), ((IEnumerable<Tuple<string, string>>) allowedPairs).Select<Tuple<string, string>, string>((Func<Tuple<string, string>, string>) (t => t.Item2)).ToArray<string>(), ((IEnumerable<Tuple<string, string>>) hiddenPairs).Select<Tuple<string, string>, string>((Func<Tuple<string, string>, string>) (t => t.Item1)).ToArray<string>(), ((IEnumerable<Tuple<string, string>>) hiddenPairs).Select<Tuple<string, string>, string>((Func<Tuple<string, string>, string>) (t => t.Item2)).ToArray<string>())
  {
  }

  /// <summary>
  /// Ctor. Full version. User may define a list of allowed values and their descriptions, and a list of hidden values.
  /// </summary>
  /// <param name="inventoryID">Must be IBqlField. Represents an InventoryID field of the row</param>
  /// <param name="allowedValues">List of allowed values. </param>
  /// <param name="allowedLabels"> Labels for the allowed values. List should have the same size as the list of the values</param>
  /// <param name="hiddenValues"> List of possible values for the control. Must include all the values from the allowedValues list</param>
  /// <param name="hiddenLabels"> Labels for the possible values. List should have the same size as the list of the values</param>
  public POLineTypeListAttribute(
    Type inventoryID,
    string[] allowedValues,
    string[] allowedLabels,
    string[] hiddenValues,
    string[] hiddenLabels)
    : base(allowedValues, allowedLabels, hiddenValues, hiddenLabels)
  {
    this._inventoryID = inventoryID;
  }

  public override void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    PXGraph.FieldUpdatedEvents fieldUpdated = sender.Graph.FieldUpdated;
    Type itemType = sender.GetItemType();
    string name = this._inventoryID.Name;
    POLineTypeListAttribute typeListAttribute = this;
    // ISSUE: virtual method pointer
    PXFieldUpdated pxFieldUpdated = new PXFieldUpdated((object) typeListAttribute, __vmethodptr(typeListAttribute, InventoryIDUpdated));
    fieldUpdated.AddHandler(itemType, name, pxFieldUpdated);
  }

  public virtual void FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    int? inventoryID = (int?) sender.GetValue(e.Row, this._inventoryID.Name);
    if (e.Row == null || e.NewValue == null)
      return;
    if (inventoryID.HasValue && !POLineType.IsProjectDropShip((string) e.NewValue))
    {
      PX.Objects.IN.InventoryItem inventoryItem1 = PX.Objects.IN.InventoryItem.PK.Find(sender.Graph, inventoryID);
      PX.Objects.IN.InventoryItem inventoryItem2;
      if (inventoryItem1 != null)
      {
        bool? stkItem = inventoryItem1.StkItem;
        bool flag = false;
        if (stkItem.GetValueOrDefault() == flag & stkItem.HasValue)
        {
          inventoryItem2 = inventoryItem1;
          goto label_6;
        }
      }
      inventoryItem2 = (PX.Objects.IN.InventoryItem) null;
label_6:
      PX.Objects.IN.InventoryItem inventoryItem3 = inventoryItem2;
      if (inventoryItem3 != null && inventoryItem3.KitItem.GetValueOrDefault())
      {
        if (PXResultset<INKitSpecStkDet>.op_Implicit(PXSelectBase<INKitSpecStkDet, PXSelect<INKitSpecStkDet, Where<INKitSpecStkDet.kitInventoryID, Equal<Required<INKitSpecStkDet.kitInventoryID>>>>.Config>.SelectWindowed(sender.Graph, 0, 1, new object[1]
        {
          (object) inventoryItem3.InventoryID
        })) != null)
          inventoryItem3 = (PX.Objects.IN.InventoryItem) null;
      }
      if (inventoryItem3 != null && !POLineType.IsNonStock((string) e.NewValue) || inventoryItem3 == null && !POLineType.IsStock((string) e.NewValue))
        throw new PXSetPropertyException("Selected line type is not allowed for current inventory item.");
    }
    if (this.IndexValue((string) e.NewValue) == -1)
      throw new PXSetPropertyException("Selected line type is not allowed for current inventory item.");
  }

  public virtual void FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
  {
    int? inventoryID = (int?) sender.GetValue(e.Row, this._inventoryID.Name);
    if (!inventoryID.HasValue)
      return;
    PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find(sender.Graph, inventoryID);
    if (inventoryItem != null)
    {
      bool? nullable = inventoryItem.StkItem;
      if (!nullable.GetValueOrDefault())
      {
        if (!PXAccess.FeatureInstalled<FeaturesSet.inventory>())
        {
          e.NewValue = (object) "SV";
          goto label_12;
        }
        nullable = inventoryItem.KitItem;
        if (nullable.GetValueOrDefault())
        {
          if (PXResultset<INKitSpecStkDet>.op_Implicit(PXSelectBase<INKitSpecStkDet, PXSelect<INKitSpecStkDet, Where<INKitSpecStkDet.kitInventoryID, Equal<Required<INKitSpecStkDet.kitInventoryID>>>>.Config>.SelectWindowed(sender.Graph, 0, 1, new object[1]
          {
            (object) inventoryItem.InventoryID
          })) == null)
            return;
          e.NewValue = (object) "GI";
          return;
        }
        e.NewValue = (object) "GI";
        nullable = inventoryItem.NonStockReceipt;
        if (nullable.HasValue)
        {
          PXFieldDefaultingEventArgs defaultingEventArgs = e;
          nullable = inventoryItem.NonStockReceipt;
          string str = nullable.GetValueOrDefault() ? "NS" : "SV";
          defaultingEventArgs.NewValue = (object) str;
          goto label_12;
        }
        goto label_12;
      }
    }
    e.NewValue = (object) "GI";
label_12:
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void InventoryIDUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    if (!EnumerableExtensions.IsIn<object>(sender.GetValuePending(e.Row, ((PXEventSubscriberAttribute) this)._FieldName), (object) null, PXCache.NotSetValue))
      return;
    sender.SetDefaultExt(e.Row, ((PXEventSubscriberAttribute) this)._FieldName, (object) null);
  }
}
