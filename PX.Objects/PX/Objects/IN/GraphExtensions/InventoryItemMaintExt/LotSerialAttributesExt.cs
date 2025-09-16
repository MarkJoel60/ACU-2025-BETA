// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.GraphExtensions.InventoryItemMaintExt.LotSerialAttributesExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CS;
using PX.Objects.IN.DAC;
using PX.Objects.PO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace PX.Objects.IN.GraphExtensions.InventoryItemMaintExt;

public class LotSerialAttributesExt : LotSerialAttributeConfigurationBase<
#nullable disable
InventoryItemMaint>
{
  public FbqlSelect<SelectFromBase<INItemLotSerialAttribute, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<CSAttribute>.On<INItemLotSerialAttribute.FK.Attribute>>>.Where<BqlOperand<
  #nullable enable
  INItemLotSerialAttribute.inventoryID, IBqlInt>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  PX.Objects.IN.InventoryItem.inventoryID, IBqlInt>.FromCurrent>>.Order<
  #nullable disable
  By<BqlField<
  #nullable enable
  INItemLotSerialAttribute.sortOrder, IBqlShort>.Asc>>, 
  #nullable disable
  INItemLotSerialAttribute>.View LotSerialAttributes;
  public FbqlSelect<SelectFromBase<INRegisterItemLotSerialAttributesHeader, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<
  #nullable enable
  INRegisterItemLotSerialAttributesHeader.inventoryID, IBqlInt>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  INItemLotSerialAttribute.inventoryID, IBqlInt>.FromCurrent>>, 
  #nullable disable
  INRegisterItemLotSerialAttributesHeader>.View InventoryLotSerialAttributeHeader;
  public FbqlSelect<SelectFromBase<POReceiptItemLotSerialAttributesHeader, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<
  #nullable enable
  POReceiptItemLotSerialAttributesHeader.inventoryID, IBqlInt>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  INItemLotSerialAttribute.inventoryID, IBqlInt>.FromCurrent>>, 
  #nullable disable
  POReceiptItemLotSerialAttributesHeader>.View POReceiptLotSerialAttributeHeader;

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.lotSerialAttributes>();

  protected virtual void _(PX.Data.Events.RowSelected<PX.Objects.IN.InventoryItem> e)
  {
    if (e.Row == null)
      return;
    ((PXSelectBase) this.LotSerialAttributes).AllowSelect = ((PXSelectBase) this.LotSerialAttributes).AllowInsert = ((PXSelectBase) this.LotSerialAttributes).AllowDelete = ((PXSelectBase) this.LotSerialAttributes).AllowUpdate = this.IsLotSerialAttributesApplicable(e.Row.LotSerClassID);
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<PX.Objects.IN.InventoryItem, PX.Objects.IN.InventoryItem.lotSerClassID> e)
  {
    if (e.Row == null || e.OldValue == ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.IN.InventoryItem, PX.Objects.IN.InventoryItem.lotSerClassID>, PX.Objects.IN.InventoryItem, object>) e).NewValue || ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.IN.InventoryItem, PX.Objects.IN.InventoryItem.lotSerClassID>, PX.Objects.IN.InventoryItem, object>) e).NewValue == null || this.InitLotSerialAttributesFromClass(e.Row, true, ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.IN.InventoryItem, PX.Objects.IN.InventoryItem.lotSerClassID>, PX.Objects.IN.InventoryItem, object>) e).NewValue as string))
      return;
    ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.IN.InventoryItem, PX.Objects.IN.InventoryItem.lotSerClassID>, PX.Objects.IN.InventoryItem, object>) e).NewValue = e.OldValue;
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PX.Objects.IN.InventoryItem, PX.Objects.IN.InventoryItem.lotSerClassID> e)
  {
    if (e.Row == null || ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<PX.Objects.IN.InventoryItem, PX.Objects.IN.InventoryItem.lotSerClassID>, PX.Objects.IN.InventoryItem, object>) e).OldValue == e.NewValue || e.NewValue == null)
      return;
    this.InitLotSerialAttributesFromClass(e.Row, false);
  }

  protected virtual void _(PX.Data.Events.RowSelected<INItemLotSerialAttribute> e)
  {
    PXUIFieldAttribute.SetEnabled<INItemLotSerialAttribute.attributeID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<INItemLotSerialAttribute>>) e).Cache, (object) e.Row, e.Row?.AttributeID == null || ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<INItemLotSerialAttribute>>) e).Cache.GetStatus((object) e.Row) == 2);
  }

  protected virtual void _(PX.Data.Events.RowDeleting<INItemLotSerialAttribute> e)
  {
    if (e.Row == null || ((PX.Data.Events.Event<PXRowDeletingEventArgs, PX.Data.Events.RowDeleting<INItemLotSerialAttribute>>) e).Cache.GetStatus((object) e.Row) == 2 || ((PXSelectBase<INLotSerClass>) this.Base.lotserclass).Current == null || ((PXSelectBase) this.Base.lotserclass).Cache.GetStatus((object) ((PXSelectBase<INLotSerClass>) this.Base.lotserclass).Current) == 3)
      return;
    if (e.Row.IsActive.GetValueOrDefault())
      throw new PXException("The attribute cannot be removed because it is active. Make it inactive before removing.");
    if (this.FindReleasedAttributeValues(e.Row))
      throw new PXException("The attribute cannot be deleted because at least one of its values is assigned to an item that has already been received.");
    if (((PXGraph) this.Base).IsContractBasedAPI || !this.FindUnreleasedAttributeValues(e.Row) || ((PXSelectBase<INItemLotSerialAttribute>) this.LotSerialAttributes).Ask("Warning", "The attribute will be deleted from the settings of the item. Do you want to proceed?", (MessageButtons) 4) == 6)
      return;
    e.Cancel = true;
  }

  protected virtual void _(PX.Data.Events.RowDeleted<INItemLotSerialAttribute> e)
  {
    this.ClearAttributeValues(e.Row);
  }

  protected virtual bool IsLotSerialAttributesApplicable(string lotSerClassID)
  {
    if (lotSerClassID == null)
      return false;
    INLotSerClass inLotSerClass = INLotSerClass.PK.Find((PXGraph) this.Base, lotSerClassID);
    return inLotSerClass?.LotSerAssign == "R" && inLotSerClass.LotSerTrack != "N";
  }

  protected virtual bool InitLotSerialAttributesFromClass(
    PX.Objects.IN.InventoryItem item,
    bool onlyVerify,
    string newClassID = null)
  {
    using (new ReadOnlyScope(new PXCache[1]
    {
      ((PXSelectBase) this.LotSerialAttributes).Cache
    }))
    {
      Dictionary<string, INItemLotSerialAttribute> dictionary1 = GraphHelper.RowCast<INItemLotSerialAttribute>((IEnumerable) PXSelectBase<INItemLotSerialAttribute, PXViewOf<INItemLotSerialAttribute>.BasedOn<SelectFromBase<INItemLotSerialAttribute, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<INItemLotSerialAttribute.inventoryID, IBqlInt>.IsEqual<P.AsInt>>>.Config>.Select((PXGraph) this.Base, new object[1]
      {
        (object) item.InventoryID
      })).ToDictionary<INItemLotSerialAttribute, string>((Func<INItemLotSerialAttribute, string>) (x => x.AttributeID), (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
      Dictionary<string, INLotSerClassAttribute> dictionary2 = GraphHelper.RowCast<INLotSerClassAttribute>((IEnumerable) PXSelectBase<INLotSerClassAttribute, PXViewOf<INLotSerClassAttribute>.BasedOn<SelectFromBase<INLotSerClassAttribute, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<INLotSerClassAttribute.lotSerClassID, IBqlString>.IsEqual<P.AsString>>>.Config>.Select((PXGraph) this.Base, new object[1]
      {
        (object) (newClassID ?? item.LotSerClassID)
      })).ToDictionary<INLotSerClassAttribute, string>((Func<INLotSerClassAttribute, string>) (a => a.AttributeID), (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
      if (onlyVerify)
      {
        foreach (KeyValuePair<string, INItemLotSerialAttribute> keyValuePair in dictionary1)
        {
          if (!dictionary2.ContainsKey(keyValuePair.Key) && this.FindReleasedAttributeValues(keyValuePair.Value))
            throw new PXException("The attribute cannot be deleted because at least one of its values is assigned to an item that has already been received.");
        }
        foreach (KeyValuePair<string, INItemLotSerialAttribute> keyValuePair in dictionary1)
        {
          if (!dictionary2.ContainsKey(keyValuePair.Key) && this.FindUnreleasedAttributeValues(keyValuePair.Value) && ((PXSelectBase<INItemLotSerialAttribute>) this.LotSerialAttributes).Ask("Warning", "The attribute will be deleted from the settings of the item. Do you want to proceed?", (MessageButtons) 4) != 6)
            return false;
        }
        return true;
      }
      foreach (KeyValuePair<string, INItemLotSerialAttribute> keyValuePair in dictionary1)
      {
        if (!dictionary2.ContainsKey(keyValuePair.Key))
        {
          keyValuePair.Value.IsActive = new bool?(false);
          ((PXSelectBase<INItemLotSerialAttribute>) this.LotSerialAttributes).Delete(((PXSelectBase<INItemLotSerialAttribute>) this.LotSerialAttributes).Update(keyValuePair.Value));
        }
      }
      foreach (KeyValuePair<string, INLotSerClassAttribute> keyValuePair in dictionary2)
      {
        INItemLotSerialAttribute lotSerialAttribute;
        if (dictionary1.TryGetValue(keyValuePair.Key, out lotSerialAttribute))
        {
          lotSerialAttribute.IsActive = keyValuePair.Value.IsActive;
          lotSerialAttribute.Required = keyValuePair.Value.Required;
          lotSerialAttribute.SortOrder = keyValuePair.Value.SortOrder;
          ((PXSelectBase<INItemLotSerialAttribute>) this.LotSerialAttributes).Insert(lotSerialAttribute);
        }
        else
          ((PXSelectBase<INItemLotSerialAttribute>) this.LotSerialAttributes).Insert(new INItemLotSerialAttribute()
          {
            AttributeID = keyValuePair.Value.AttributeID,
            InventoryID = item.InventoryID,
            IsActive = keyValuePair.Value.IsActive,
            Required = keyValuePair.Value.Required,
            SortOrder = keyValuePair.Value.SortOrder
          });
      }
    }
    return true;
  }

  private void ClearAttributeValues(INItemLotSerialAttribute lotSerClassAttribute)
  {
    string attributeId = lotSerClassAttribute.AttributeID;
    if (this.GetKeyValueAttribute(attributeId, true) == null)
      return;
    int num1 = 0;
    int num2 = 0;
    foreach (INRegisterItemLotSerialAttributesHeader attributesHeader in GraphHelper.RowCast<INRegisterItemLotSerialAttributesHeader>((IEnumerable) ((PXSelectBase) this.InventoryLotSerialAttributeHeader).View.Select(new object[1]
    {
      (object) lotSerClassAttribute
    }, Array.Empty<object>(), Array.Empty<object>(), Array.Empty<string>(), Array.Empty<bool>(), new PXFilterRow[1]
    {
      new PXFilterRow(this.ConvertAttributeIDToFieldName(attributeId), (PXCondition) 12)
    }, ref num1, -1, ref num2)))
      this.SetValue<INRegisterItemLotSerialAttributesHeader>(attributesHeader, attributeId, (object) null);
    int num3 = 0;
    int num4 = 0;
    foreach (POReceiptItemLotSerialAttributesHeader attributesHeader in GraphHelper.RowCast<POReceiptItemLotSerialAttributesHeader>((IEnumerable) ((PXSelectBase) this.POReceiptLotSerialAttributeHeader).View.Select(new object[1]
    {
      (object) lotSerClassAttribute
    }, Array.Empty<object>(), Array.Empty<object>(), Array.Empty<string>(), Array.Empty<bool>(), new PXFilterRow[1]
    {
      new PXFilterRow(this.ConvertAttributeIDToFieldName(attributeId), (PXCondition) 12)
    }, ref num3, -1, ref num4)))
      this.SetValue<POReceiptItemLotSerialAttributesHeader>(attributesHeader, attributeId, (object) null);
  }

  private bool FindUnreleasedAttributeValues(INItemLotSerialAttribute lotSerClassAttribute)
  {
    string attributeId = lotSerClassAttribute.AttributeID;
    if (this.GetKeyValueAttribute(attributeId, true) == null)
      return false;
    int num1 = 0;
    int num2 = 0;
    ((PXSelectBase) this.InventoryLotSerialAttributeHeader).View.Select(new object[1]
    {
      (object) lotSerClassAttribute
    }, Array.Empty<object>(), Array.Empty<object>(), Array.Empty<string>(), Array.Empty<bool>(), new PXFilterRow[1]
    {
      new PXFilterRow(this.ConvertAttributeIDToFieldName(attributeId), (PXCondition) 12)
    }, ref num1, 1, ref num2);
    if (num2 > 0)
      return true;
    num1 = 0;
    num2 = 0;
    ((PXSelectBase) this.POReceiptLotSerialAttributeHeader).View.Select(new object[1]
    {
      (object) lotSerClassAttribute
    }, Array.Empty<object>(), Array.Empty<object>(), Array.Empty<string>(), Array.Empty<bool>(), new PXFilterRow[1]
    {
      new PXFilterRow(this.ConvertAttributeIDToFieldName(attributeId), (PXCondition) 12)
    }, ref num1, -1, ref num2);
    return num2 > 0;
  }

  private bool FindReleasedAttributeValues(INItemLotSerialAttribute lotSerClassAttribute)
  {
    if (this.GetKeyValueAttribute(lotSerClassAttribute.AttributeID, true) == null)
      return false;
    PXViewOf<PX.Objects.IN.DAC.INItemLotSerialAttributesHeader>.BasedOn<SelectFromBase<PX.Objects.IN.DAC.INItemLotSerialAttributesHeader, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<PX.Objects.IN.DAC.INItemLotSerialAttributesHeader.inventoryID, IBqlInt>.IsEqual<BqlField<INItemLotSerialAttribute.inventoryID, IBqlInt>.FromCurrent>>>.ReadOnly readOnly = new PXViewOf<PX.Objects.IN.DAC.INItemLotSerialAttributesHeader>.BasedOn<SelectFromBase<PX.Objects.IN.DAC.INItemLotSerialAttributesHeader, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<PX.Objects.IN.DAC.INItemLotSerialAttributesHeader.inventoryID, IBqlInt>.IsEqual<BqlField<INItemLotSerialAttribute.inventoryID, IBqlInt>.FromCurrent>>>.ReadOnly((PXGraph) this.Base);
    int num1 = 0;
    int num2 = 0;
    ((PXSelectBase) readOnly).View.Select(new object[1]
    {
      (object) lotSerClassAttribute
    }, Array.Empty<object>(), Array.Empty<object>(), Array.Empty<string>(), Array.Empty<bool>(), new PXFilterRow[1]
    {
      new PXFilterRow(this.ConvertAttributeIDToFieldName(lotSerClassAttribute.AttributeID), (PXCondition) 12)
    }, ref num1, 1, ref num2);
    return num2 > 0;
  }

  protected override bool IsAttributeCacheDirty()
  {
    return ((PXSelectBase) this.LotSerialAttributes).Cache.IsDirty || NonGenericIEnumerableExtensions.Any_(((PXSelectBase) this.LotSerialAttributes).Cache.Inserted);
  }
}
