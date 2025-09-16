// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.POLineTypeList2Attribute
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

#nullable disable
namespace PX.Objects.PO;

public class POLineTypeList2Attribute : 
  PXStringListAttribute,
  IPXFieldDefaultingSubscriber,
  IPXFieldVerifyingSubscriber
{
  protected Type docTypeField;
  protected Type inventoryIDField;

  public POLineTypeList2Attribute(Type docType, Type inventoryID)
    : base(new Tuple<string, string>[14]
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
    this.docTypeField = docType;
    this.inventoryIDField = inventoryID;
  }

  public virtual void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    PXGraph.FieldUpdatedEvents fieldUpdated = sender.Graph.FieldUpdated;
    Type itemType = sender.GetItemType();
    string field = sender.GetField(this.inventoryIDField);
    POLineTypeList2Attribute typeList2Attribute = this;
    // ISSUE: virtual method pointer
    PXFieldUpdated pxFieldUpdated = new PXFieldUpdated((object) typeList2Attribute, __vmethodptr(typeList2Attribute, InventoryIDUpdated));
    fieldUpdated.AddHandler(itemType, field, pxFieldUpdated);
  }

  public virtual void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    if (this.ShowFilteredComboValues(sender, e.Row))
    {
      string returnValue = (string) e.ReturnValue;
      Tuple<List<string>, List<string>, string> tuple = this.PopulateValues(sender, e.Row);
      if (string.IsNullOrEmpty(returnValue) || tuple.Item1.Contains(returnValue))
      {
        e.ReturnState = (object) PXStringState.CreateInstance(e.ReturnState, new int?(1), new bool?(false), ((PXEventSubscriberAttribute) this)._FieldName, new bool?(false), new int?(1), (string) null, tuple.Item1.ToArray(), tuple.Item2.ToArray(), new bool?(true), tuple.Item3, (string[]) null);
        ((PXFieldState) e.ReturnState).Enabled = tuple.Item1.Count > 1;
      }
      else
      {
        e.ReturnState = (object) PXStringState.CreateInstance(e.ReturnState, new int?(1), new bool?(false), ((PXEventSubscriberAttribute) this)._FieldName, new bool?(false), new int?(1), (string) null, this._AllowedValues, this._AllowedLabels, new bool?(true), returnValue, (string[]) null);
        ((PXFieldState) e.ReturnState).Enabled = false;
      }
    }
    else
      base.FieldSelecting(sender, e);
  }

  public virtual void FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    if (!sender.Graph.IsImportFromExcel || !this.ShowFilteredComboValues(sender, e.Row))
      return;
    string str = sender.GetValue(e.Row, ((PXEventSubscriberAttribute) this).FieldName) as string;
    Tuple<List<string>, List<string>, string> tuple = this.PopulateValues(sender, e.Row);
    if (string.IsNullOrEmpty(str) || tuple.Item1.Contains(e.NewValue as string))
      return;
    e.NewValue = (object) str;
  }

  protected virtual bool ShowFilteredComboValues(PXCache sender, object row)
  {
    return row != null && !string.IsNullOrEmpty(sender.Graph.PrimaryView);
  }

  public virtual void FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
  {
    string docType = (string) null;
    if (this.docTypeField != (Type) null)
      docType = (string) sender.GetValue(e.Row, sender.GetField(this.docTypeField));
    int? inventoryID = (int?) sender.GetValue(e.Row, sender.GetField(this.inventoryIDField));
    Tuple<List<string>, List<string>, string> tuple = this.PopulateValues(sender, docType, inventoryID);
    e.NewValue = (object) tuple.Item3;
  }

  private string LocaleLabel(string value, string neutralLabel)
  {
    if (this._AllowedValues != null && this._AllowedLabels != null && this._AllowedValues.Length == this._AllowedLabels.Length)
    {
      for (int index = 0; index < this._AllowedValues.Length; ++index)
      {
        if (this._AllowedValues[index] == value)
          return this._AllowedLabels[index];
      }
    }
    return neutralLabel;
  }

  protected virtual Tuple<List<string>, List<string>, string> PopulateValues(
    PXCache cache,
    object row)
  {
    string docType = (string) null;
    if (this.docTypeField != (Type) null)
      docType = (string) cache.GetValue(row, cache.GetField(this.docTypeField));
    int? inventoryID = (int?) cache.GetValue(row, cache.GetField(this.inventoryIDField));
    return this.PopulateValues(cache, docType, inventoryID);
  }

  /// <summary>
  /// Populate list of available LineTypes based on current state of entity.
  /// </summary>
  /// <returns>
  /// Item1 - List of available values
  /// Item2 - List of available labels
  /// Item3 - default value.
  /// </returns>
  protected virtual Tuple<List<string>, List<string>, string> PopulateValues(
    PXCache sender,
    string docType,
    int? inventoryID)
  {
    string str = (string) null;
    List<string> stringList1 = new List<string>();
    List<string> stringList2 = new List<string>();
    PX.Objects.IN.InventoryItem inventoryItem = (PX.Objects.IN.InventoryItem) null;
    if (inventoryID.HasValue)
      inventoryItem = PX.Objects.IN.InventoryItem.PK.Find(sender.Graph, inventoryID);
    if (inventoryItem != null)
    {
      bool flag = false;
      bool? nullable = inventoryItem.StkItem;
      if (nullable.GetValueOrDefault())
      {
        flag = true;
      }
      else
      {
        nullable = inventoryItem.KitItem;
        if (nullable.GetValueOrDefault())
        {
          if (PXResultset<INKitSpecStkDet>.op_Implicit(PXSelectBase<INKitSpecStkDet, PXSelect<INKitSpecStkDet, Where<INKitSpecStkDet.kitInventoryID, Equal<Required<INKitSpecStkDet.kitInventoryID>>>>.Config>.SelectWindowed(sender.Graph, 0, 1, new object[1]
          {
            (object) inventoryItem.InventoryID
          })) != null)
            flag = true;
        }
      }
      if (flag)
      {
        switch (docType)
        {
          case "DP":
            stringList1.Add("GP");
            stringList2.Add(this.LocaleLabel("GP", "Goods for Drop-Ship"));
            break;
          case "PD":
            stringList1.Add("PG");
            stringList2.Add(this.LocaleLabel("PG", "Goods for Project"));
            break;
          default:
            stringList1.Add("GI");
            stringList2.Add(this.LocaleLabel("GI", "Goods for IN"));
            break;
        }
      }
      else if (!PXAccess.FeatureInstalled<FeaturesSet.inventory>())
      {
        stringList1.Add("SV");
        stringList2.Add(this.LocaleLabel("SV", "Service"));
      }
      else
      {
        switch (docType)
        {
          case "DP":
            nullable = inventoryItem.NonStockReceipt;
            if (nullable.GetValueOrDefault())
            {
              nullable = inventoryItem.NonStockShip;
              if (nullable.GetValueOrDefault())
              {
                stringList1.Add("NP");
                stringList2.Add(this.LocaleLabel("NP", "Non-Stock for Drop-Ship"));
                break;
              }
            }
            nullable = inventoryItem.NonStockReceipt;
            if (nullable.GetValueOrDefault())
            {
              stringList1.Add("NS");
              stringList2.Add(this.LocaleLabel("NS", "Non-Stock"));
              break;
            }
            stringList1.Add("SV");
            stringList2.Add("Service");
            break;
          case "PD":
            nullable = inventoryItem.NonStockReceipt;
            if (nullable.GetValueOrDefault())
            {
              stringList1.Add("PN");
              stringList2.Add(this.LocaleLabel("PN", "Non-Stock for Project"));
              break;
            }
            stringList1.Add("SV");
            stringList2.Add("Service");
            break;
          default:
            nullable = inventoryItem.NonStockReceipt;
            if (nullable.GetValueOrDefault())
            {
              stringList1.Add("NS");
              stringList2.Add(this.LocaleLabel("NS", "Non-Stock"));
              break;
            }
            stringList1.Add("SV");
            stringList2.Add(this.LocaleLabel("SV", "Service"));
            break;
        }
      }
    }
    else
    {
      stringList1.Add("SV");
      stringList2.Add(this.LocaleLabel("SV", "Service"));
      stringList1.Add("DN");
      stringList2.Add(this.LocaleLabel("DN", "Description"));
      stringList1.Add("FT");
      stringList2.Add(this.LocaleLabel("FT", "Freight"));
    }
    if (stringList1.Count > 0)
      str = stringList1[0];
    return new Tuple<List<string>, List<string>, string>(stringList1, stringList2, str);
  }

  protected virtual void InventoryIDUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    if (!EnumerableExtensions.IsIn<object>(sender.GetValuePending(e.Row, ((PXEventSubscriberAttribute) this)._FieldName), (object) null, PXCache.NotSetValue))
      return;
    sender.SetDefaultExt(e.Row, ((PXEventSubscriberAttribute) this)._FieldName, (object) null);
  }
}
