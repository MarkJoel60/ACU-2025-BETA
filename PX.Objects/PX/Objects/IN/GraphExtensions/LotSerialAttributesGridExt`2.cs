// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.GraphExtensions.LotSerialAttributesGridExt`2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.IN.GraphExtensions;

public abstract class LotSerialAttributesGridExt<TGraph, TAttributesHeader> : 
  LotSerialGraphExtBase<TGraph>
  where TGraph : PXGraph
  where TAttributesHeader : class, ILotSerialAttributesHeader, IBqlTable, new()
{
  public virtual void Initialize()
  {
    ((PXGraphExtension) this).Initialize();
    PXUIFieldAttribute.SetEnabled((PXCache) GraphHelper.Caches<INItemLotSerialAttribute>((PXGraph) this.Base), (string) null, false);
    ((PXCache) GraphHelper.Caches<INItemLotSerialAttribute>((PXGraph) this.Base)).AllowInsert = ((PXCache) GraphHelper.Caches<INItemLotSerialAttribute>((PXGraph) this.Base)).AllowDelete = false;
    if (this.Base.Views.Caches.Contains(typeof (INItemLotSerialAttribute)))
      this.Base.Views.Caches.Remove(typeof (INItemLotSerialAttribute));
    this.AddValueColumn();
  }

  protected virtual void AddValueColumn()
  {
    PXCache<INItemLotSerialAttribute> pxCache = GraphHelper.Caches<INItemLotSerialAttribute>((PXGraph) this.Base);
    if (((PXCache) pxCache).Fields.Contains("Value"))
      return;
    ((PXCache) pxCache).Fields.Add("Value");
    // ISSUE: method pointer
    this.Base.FieldSelecting.AddHandler(((PXCache) pxCache).GetItemType(), "Value", new PXFieldSelecting((object) this, __methodptr(\u003CAddValueColumn\u003Eb__1_0)));
    // ISSUE: method pointer
    this.Base.FieldUpdating.AddHandler(((PXCache) pxCache).GetItemType(), "Value", new PXFieldUpdating((object) this, __methodptr(\u003CAddValueColumn\u003Eb__1_1)));
  }

  protected virtual void AttributeValueFieldSelecting(string fieldName, PXFieldSelectingEventArgs e)
  {
    e.ReturnState = (object) PXFieldState.CreateInstance((object) null, typeof (string), new bool?(), new bool?(), new int?(), new int?(), new int?(), (object) null, (string) null, (string) null, "Value", (string) null, (PXErrorLevel) 0, new bool?(), new bool?(), new bool?(), (PXUIVisibility) 0, (string) null, (string[]) null, (string[]) null);
    INItemLotSerialAttribute row = (INItemLotSerialAttribute) e.Row;
    if (row == null)
      return;
    if (((PXSelectBase) this.GetAttributesHeaderView()).Cache.GetStateExt((object) this.GetAttributesHeader(row.InventoryID, row.LotSerialNbr, false), this.ConvertAttributeIDToFieldName(row.AttributeID)) is PXFieldState stateExt)
    {
      stateExt.SetFieldName(fieldName);
      stateExt.Enabled = this.GetAttributeEnabled(row);
      e.ReturnState = (object) stateExt;
      e.ReturnValue = stateExt.Value;
    }
    this.FieldSelecting(row.AttributeID, e);
  }

  protected virtual void AttributeValueFieldUpdating(string fieldName, PXFieldUpdatingEventArgs e)
  {
    INItemLotSerialAttribute row = (INItemLotSerialAttribute) e.Row;
    if (row == null)
      return;
    TAttributesHeader attributesHeader = this.GetAttributesHeader(row.InventoryID, row.LotSerialNbr, true);
    IEnumerable<string> source = this.GetAttributes(attributesHeader.InventoryID).Where<LotSerialGraphExtBase<TGraph>.AttributeInformation>((Func<LotSerialGraphExtBase<TGraph>.AttributeInformation, bool>) (a => a.Required && a.IsActive)).Select<LotSerialGraphExtBase<TGraph>.AttributeInformation, string>((Func<LotSerialGraphExtBase<TGraph>.AttributeInformation, string>) (a => a.AttributeID));
    if (e.NewValue == null && !this.AllowEmptyRequiredAttribute() && source != null && source.Contains<string>(row.AttributeID, (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase))
    {
      if (((PXSelectBase) this.GetAttributesHeaderView()).Cache.RaiseExceptionHandling(this.ConvertAttributeIDToFieldName(row.AttributeID), (object) attributesHeader, (object) null, (Exception) new PXSetPropertyException((IBqlTable) attributesHeader, "'{0}' cannot be empty.", (PXErrorLevel) 5)))
      {
        InventoryItem inventoryItem = InventoryItem.PK.Find((PXGraph) this.Base, row.InventoryID);
        throw new PXSetPropertyException((IBqlTable) row, "At least one required attribute has not been specified for the {0} lot or serial number of the {1} item in the Line Details dialog box. Specify all required attributes to save the document.", new object[2]
        {
          (object) attributesHeader.LotSerialNbr,
          (object) inventoryItem?.InventoryCD?.Trim()
        });
      }
    }
    else
      this.SetValue<TAttributesHeader>(attributesHeader, row.AttributeID, e.NewValue);
  }

  public abstract PXSelectBase<TAttributesHeader> GetAttributesHeaderView();

  protected abstract TAttributesHeader GetAttributesHeader(
    int? inventoryID,
    string lotSerialNbr,
    bool insert);

  protected virtual void VerifyRequiredAttributes(TAttributesHeader header)
  {
    foreach (string attributeID in this.GetRequiredAttributes(header.InventoryID).Select<LotSerialGraphExtBase<TGraph>.AttributeInformation, string>((Func<LotSerialGraphExtBase<TGraph>.AttributeInformation, string>) (a => a.AttributeID)))
    {
      if (this.GetValue<TAttributesHeader>(header, attributeID) == null)
      {
        if (this.GetKeyValueAttribute(attributeID).ControlType == 4)
          this.SetValue<TAttributesHeader>(header, attributeID, (object) false);
        else if (!this.AllowEmptyRequiredAttribute())
        {
          PXCache cache = ((PXSelectBase) this.GetAttributesHeaderView()).Cache;
          if (!this.ShowErrorOnAttributeField() || cache.RaiseExceptionHandling(this.ConvertAttributeIDToFieldName(attributeID), (object) header, (object) null, (Exception) new PXSetPropertyException((IBqlTable) header, "'{0}' cannot be empty.", (PXErrorLevel) 5)))
          {
            InventoryItem inventoryItem = InventoryItem.PK.Find((PXGraph) this.Base, header.InventoryID);
            throw new PXRowPersistingException("Value", (object) null, "At least one required attribute has not been specified for the {0} lot or serial number of the {1} item in the Line Details dialog box. Specify all required attributes to save the document.", new object[2]
            {
              (object) header.LotSerialNbr,
              (object) inventoryItem?.InventoryCD?.Trim()
            });
          }
        }
      }
    }
  }

  protected virtual bool AllowEmptyRequiredAttribute() => false;

  protected virtual bool ShowErrorOnAttributeField() => true;

  protected virtual bool GetAttributeEnabled(INItemLotSerialAttribute attribute) => true;
}
