// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.Matrix.GraphExtensions.ItemsGridExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Common.Collection;
using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Objects.Common;
using PX.Objects.Common.Exceptions;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.IN.Matrix.Attributes;
using PX.Objects.IN.Matrix.DAC.Unbound;
using PX.Objects.IN.Matrix.Graphs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace PX.Objects.IN.Matrix.GraphExtensions;

public class ItemsGridExt : PXGraphExtension<
#nullable disable
TemplateInventoryItemMaint>
{
  public PXFilter<TemplateAttributes> ItemAttributes;
  [PXVirtualDAC]
  public PXSelect<ItemsGridExt.MatrixInventoryItem> MatrixItems;
  public PXAction<InventoryItem> viewMatrixItem;
  public PXAction<InventoryItem> deleteItems;

  [PXUIField]
  [PXLookupButton]
  public virtual IEnumerable ViewMatrixItem(PXAdapter adapter)
  {
    if (((PXSelectBase<ItemsGridExt.MatrixInventoryItem>) this.MatrixItems).Current != null)
    {
      InventoryItem inventoryItem = InventoryItem.PK.Find((PXGraph) this.Base, ((PXSelectBase<ItemsGridExt.MatrixInventoryItem>) this.MatrixItems).Current.InventoryID);
      if (inventoryItem != null)
        PXRedirectHelper.TryRedirect(((PXGraph) this.Base).Caches[typeof (InventoryItem)], (object) inventoryItem, "View Item", (PXRedirectHelper.WindowMode) 3);
    }
    return adapter.Get();
  }

  public virtual void Initialize()
  {
    ((PXGraphExtension) this).Initialize();
    HashSet<string> enabledFields = new HashSet<string>(this.GetEditableChildFields(), (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    EnumerableExtensions.AddRange<string>((ISet<string>) enabledFields, this.GetEditableCurrencyChildFields());
    enabledFields.Add("Selected");
    EnumerableExtensions.ForEach<PXUIFieldAttribute>(((PXSelectBase) this.MatrixItems).Cache.GetAttributesOfType<PXUIFieldAttribute>((object) null, (string) null), (Action<PXUIFieldAttribute>) (a => a.Enabled = enabledFields.Contains(((PXEventSubscriberAttribute) a).FieldName)));
    int attributeNumber = 0;
    while (true)
    {
      int num = attributeNumber;
      int? length = ((PXSelectBase<TemplateAttributes>) this.ItemAttributes).Current.AttributeIdentifiers?.Length;
      int valueOrDefault = length.GetValueOrDefault();
      if (num < valueOrDefault & length.HasValue)
      {
        this.AddAttributeColumn(attributeNumber);
        ++attributeNumber;
      }
      else
        break;
    }
    ((PXGraph) this.Base).Views.Caches.Remove(typeof (TemplateAttributes));
    ((PXGraph) this.Base).Views.Caches.Remove(typeof (ItemsGridExt.MatrixInventoryItem));
  }

  protected virtual void _(PX.Data.Events.RowSelected<InventoryItem> eventArgs)
  {
    int? templateItemId = ((PXSelectBase<TemplateAttributes>) this.ItemAttributes).Current.TemplateItemID;
    int? inventoryId = (int?) eventArgs.Row?.InventoryID;
    if (templateItemId.GetValueOrDefault() == inventoryId.GetValueOrDefault() & templateItemId.HasValue == inventoryId.HasValue)
      return;
    ((PXSelectBase<TemplateAttributes>) this.ItemAttributes).Current.TemplateItemID = (int?) eventArgs.Row?.InventoryID;
    this.RecalcAttributeColumns();
  }

  protected virtual void _(PX.Data.Events.RowUpdated<CSAnswers> e)
  {
    bool? isActive1 = e.Row.IsActive;
    bool? isActive2 = e.OldRow.IsActive;
    if (isActive1.GetValueOrDefault() == isActive2.GetValueOrDefault() & isActive1.HasValue == isActive2.HasValue)
      return;
    this.RecalcAttributeColumns();
  }

  protected virtual void RecalcAttributeColumns()
  {
    CSAttribute[] source = ((PXSelectBase<CSAttribute>) new PXSelectReadonly2<CSAttribute, InnerJoin<CSAttributeGroup, On<CSAttributeGroup.attributeID, Equal<CSAttribute.attributeID>>>, Where<CSAttributeGroup.isActive, Equal<True>, And<CSAttributeGroup.entityClassID, Equal<Required<InventoryItem.itemClassID>>, And<CSAttributeGroup.entityType, Equal<Constants.DACName<InventoryItem>>, And<CSAttributeGroup.attributeCategory, Equal<CSAttributeGroup.attributeCategory.variant>>>>>, OrderBy<Asc<CSAttributeGroup.sortOrder>>>((PXGraph) this.Base)).SelectMain(new object[1]
    {
      (object) (int?) ((PXSelectBase<InventoryItem>) this.Base.ItemSettings).Current?.ParentItemClassID
    });
    Dictionary<string, CSAnswers> answers = ((IEnumerable<CSAnswers>) this.Base.Answers.SelectMain(Array.Empty<object>())).ToDictionary<CSAnswers, string>((Func<CSAnswers, string>) (a => a.AttributeID));
    CSAttribute[] array = ((IEnumerable<CSAttribute>) source).Where<CSAttribute>((Func<CSAttribute, bool>) (a =>
    {
      CSAnswers csAnswers;
      if (!answers.TryGetValue(a.AttributeID, out csAnswers))
        return true;
      bool? isActive = csAnswers.IsActive;
      bool flag = false;
      return !(isActive.GetValueOrDefault() == flag & isActive.HasValue);
    })).ToArray<CSAttribute>();
    ((PXSelectBase<TemplateAttributes>) this.ItemAttributes).Current.AttributeIdentifiers = new string[array.Length];
    for (int attributeNumber = 0; attributeNumber < array.Length; ++attributeNumber)
    {
      ((PXSelectBase<TemplateAttributes>) this.ItemAttributes).Current.AttributeIdentifiers[attributeNumber] = array[attributeNumber].AttributeID;
      this.AddAttributeColumn(attributeNumber);
    }
    ((PXSelectBase) this.MatrixItems).View.RequestRefresh();
  }

  protected virtual void AddAttributeColumn(int attributeNumber)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    ItemsGridExt.\u003C\u003Ec__DisplayClass9_0 cDisplayClass90 = new ItemsGridExt.\u003C\u003Ec__DisplayClass9_0()
    {
      \u003C\u003E4__this = this,
      attributeNumber = attributeNumber
    };
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    cDisplayClass90.fieldName = $"AttributeValue{cDisplayClass90.attributeNumber}";
    // ISSUE: reference to a compiler-generated field
    if (((PXSelectBase) this.MatrixItems).Cache.Fields.Contains(cDisplayClass90.fieldName))
      return;
    // ISSUE: reference to a compiler-generated field
    ((PXSelectBase) this.MatrixItems).Cache.Fields.Add(cDisplayClass90.fieldName);
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    ((PXGraph) this.Base).FieldSelecting.AddHandler(((PXSelectBase) this.MatrixItems).Cache.GetItemType(), cDisplayClass90.fieldName, new PXFieldSelecting((object) cDisplayClass90, __methodptr(\u003CAddAttributeColumn\u003Eb__0)));
  }

  protected virtual void AttributeValueFieldSelecting(
    int attributeNumber,
    PXFieldSelectingEventArgs e,
    string fieldName)
  {
    ItemsGridExt.MatrixInventoryItem row = e.Row as ItemsGridExt.MatrixInventoryItem;
    PXStringState instance = (PXStringState) PXStringState.CreateInstance(e.ReturnState, new int?(), new bool?(true), fieldName, new bool?(false), new int?(), (string) null, (string[]) null, (string[]) null, new bool?(), (string) null, (string[]) null);
    ((PXFieldState) instance).Enabled = false;
    e.ReturnState = (object) instance;
    int num1 = attributeNumber;
    int? length = ((PXSelectBase<TemplateAttributes>) this.ItemAttributes).Current.AttributeIdentifiers?.Length;
    int valueOrDefault1 = length.GetValueOrDefault();
    if (num1 < valueOrDefault1 & length.HasValue)
    {
      PXFieldSelectingEventArgs selectingEventArgs = e;
      string str;
      if (0 <= attributeNumber)
      {
        int num2 = attributeNumber;
        length = row?.AttributeValues?.Length;
        int valueOrDefault2 = length.GetValueOrDefault();
        if (num2 < valueOrDefault2 & length.HasValue)
        {
          str = row.AttributeValues[attributeNumber];
          goto label_5;
        }
      }
      str = (string) null;
label_5:
      selectingEventArgs.ReturnValue = (object) str;
      ((PXFieldState) instance).Visible = true;
      ((PXFieldState) instance).Visibility = (PXUIVisibility) 3;
      CRAttribute.Attribute attribute;
      if (!((KList<string, CRAttribute.Attribute>) CRAttribute.Attributes).TryGetValue(((PXSelectBase<TemplateAttributes>) this.ItemAttributes).Current.AttributeIdentifiers[attributeNumber], ref attribute))
        return;
      ((PXFieldState) instance).DisplayName = attribute.Description;
      instance.AllowedValues = attribute.Values.Select<CRAttribute.AttributeValue, string>((Func<CRAttribute.AttributeValue, string>) (v => v.ValueID)).ToArray<string>();
      instance.AllowedLabels = attribute.Values.Select<CRAttribute.AttributeValue, string>((Func<CRAttribute.AttributeValue, string>) (v => v.Description)).ToArray<string>();
    }
    else
    {
      ((PXFieldState) instance).Value = (object) null;
      e.ReturnValue = (object) null;
      ((PXFieldState) instance).DisplayName = (string) null;
      ((PXFieldState) instance).Visible = false;
      ((PXFieldState) instance).Visibility = (PXUIVisibility) 1;
    }
  }

  public virtual IEnumerable matrixItems()
  {
    List<ItemsGridExt.MatrixInventoryItem> matrixInventoryItemList = new List<ItemsGridExt.MatrixInventoryItem>();
    PXResultset<CSAnswers> pxResultset = ((PXSelectBase<CSAnswers>) new PXSelectReadonly2<CSAnswers, InnerJoin<InventoryItem, On<CSAnswers.refNoteID, Equal<InventoryItem.noteID>>, InnerJoin<CSAttributeGroup, On<CSAttributeGroup.attributeID, Equal<CSAnswers.attributeID>>, LeftJoin<CSAttributeDetail, On<CSAttributeDetail.attributeID, Equal<CSAttributeGroup.attributeID>, And<CSAttributeDetail.valueID, Equal<CSAnswers.value>>>, LeftJoin<InventoryItemCurySettings, On<InventoryItemCurySettings.curyID, Equal<Current<AccessInfo.baseCuryID>>, And<InventoryItemCurySettings.FK.Inventory>>, LeftJoin<INItemCost, On<INItemCost.curyID, Equal<Current<AccessInfo.baseCuryID>>, And<INItemCost.FK.InventoryItem>>>>>>>, Where<CSAttributeGroup.isActive, Equal<True>, And<CSAttributeGroup.entityClassID, Equal<Required<InventoryItem.itemClassID>>, And<CSAttributeGroup.entityType, Equal<Constants.DACName<InventoryItem>>, And<CSAttributeGroup.attributeCategory, Equal<CSAttributeGroup.attributeCategory.variant>, And<InventoryItem.templateItemID, Equal<Current<InventoryItem.inventoryID>>, And<CSAnswers.attributeID, In<Required<CSAnswers.attributeID>>>>>>>>, OrderBy<Asc<InventoryItem.inventoryCD, Asc<CSAnswers.attributeID>>>>((PXGraph) this.Base)).Select(new object[2]
    {
      (object) (int?) ((PXSelectBase<InventoryItem>) this.Base.ItemSettings).Current?.ItemClassID,
      (object) ((PXSelectBase<TemplateAttributes>) this.ItemAttributes).Current.AttributeIdentifiers
    });
    string str = (string) null;
    ItemsGridExt.MatrixInventoryItem matrixInventoryItem1 = (ItemsGridExt.MatrixInventoryItem) null;
    string[] attributeIdentifiers = ((PXSelectBase<TemplateAttributes>) this.ItemAttributes).Current.AttributeIdentifiers;
    List<string> list = attributeIdentifiers != null ? ((IEnumerable<string>) attributeIdentifiers).ToList<string>() : (List<string>) null;
    foreach (PXResult<CSAnswers, InventoryItem, CSAttributeGroup, CSAttributeDetail, InventoryItemCurySettings, INItemCost> pxResult in pxResultset)
    {
      CSAnswers csAnswers = PXResult<CSAnswers, InventoryItem, CSAttributeGroup, CSAttributeDetail, InventoryItemCurySettings, INItemCost>.op_Implicit(pxResult);
      InventoryItem source = PXResult<CSAnswers, InventoryItem, CSAttributeGroup, CSAttributeDetail, InventoryItemCurySettings, INItemCost>.op_Implicit(pxResult);
      PXResult<CSAnswers, InventoryItem, CSAttributeGroup, CSAttributeDetail, InventoryItemCurySettings, INItemCost>.op_Implicit(pxResult);
      InventoryItemCurySettings itemCurySettings = PXResult<CSAnswers, InventoryItem, CSAttributeGroup, CSAttributeDetail, InventoryItemCurySettings, INItemCost>.op_Implicit(pxResult);
      INItemCost inItemCost = PXResult<CSAnswers, InventoryItem, CSAttributeGroup, CSAttributeDetail, InventoryItemCurySettings, INItemCost>.op_Implicit(pxResult);
      if (str != source.InventoryCD)
      {
        if (matrixInventoryItem1 != null)
          matrixInventoryItemList.Add(matrixInventoryItem1);
        str = source.InventoryCD;
        matrixInventoryItem1 = PropertyTransfer.Transfer<InventoryItem, ItemsGridExt.MatrixInventoryItem>(source, new ItemsGridExt.MatrixInventoryItem());
        matrixInventoryItem1.AttributeValues = new string[((PXSelectBase<TemplateAttributes>) this.ItemAttributes).Current.AttributeIdentifiers.Length];
        matrixInventoryItem1.AttributeIDs = list.ToArray();
        ItemsGridExt.MatrixInventoryItem matrixInventoryItem2 = matrixInventoryItem1;
        Decimal? nullable1;
        Decimal? nullable2;
        if (itemCurySettings == null)
        {
          nullable1 = new Decimal?();
          nullable2 = nullable1;
        }
        else
          nullable2 = itemCurySettings.BasePrice;
        nullable1 = nullable2;
        Decimal? nullable3 = new Decimal?(nullable1.GetValueOrDefault());
        matrixInventoryItem2.BasePrice = nullable3;
        matrixInventoryItem1.DfltSiteID = (int?) itemCurySettings?.DfltSiteID;
        ItemsGridExt.MatrixInventoryItem matrixInventoryItem3 = matrixInventoryItem1;
        Decimal? nullable4;
        if (inItemCost == null)
        {
          nullable1 = new Decimal?();
          nullable4 = nullable1;
        }
        else
          nullable4 = inItemCost.LastCost;
        nullable1 = nullable4;
        Decimal? nullable5 = new Decimal?(nullable1.GetValueOrDefault());
        matrixInventoryItem3.LastCost = nullable5;
      }
      // ISSUE: explicit non-virtual call
      int index = list != null ? __nonvirtual (list.IndexOf(csAnswers.AttributeID)) : -1;
      if (index >= 0)
        matrixInventoryItem1.AttributeValues[index] = csAnswers.Value;
    }
    if (matrixInventoryItem1 != null)
      matrixInventoryItemList.Add(matrixInventoryItem1);
    foreach (ItemsGridExt.MatrixInventoryItem rowFromDb in matrixInventoryItemList)
    {
      ItemsGridExt.MatrixInventoryItem rowFromCache = (ItemsGridExt.MatrixInventoryItem) ((PXSelectBase) this.MatrixItems).Cache.Locate((object) rowFromDb);
      if (rowFromCache == null || EnumerableExtensions.IsNotIn<PXEntryStatus>(((PXSelectBase) this.MatrixItems).Cache.GetStatus((object) rowFromCache), (PXEntryStatus) 1, (PXEntryStatus) 2))
        GraphHelper.MarkUpdated(((PXSelectBase) this.MatrixItems).Cache, (object) rowFromDb, true);
      this.UpdateNonEditableFields(rowFromCache, rowFromDb);
    }
    ((PXSelectBase) this.MatrixItems).View.RequestRefresh();
    return ((PXSelectBase) this.MatrixItems).Cache.Cached;
  }

  private void UpdateNonEditableFields(
    ItemsGridExt.MatrixInventoryItem rowFromCache,
    ItemsGridExt.MatrixInventoryItem rowFromDb)
  {
    if (rowFromCache == null || rowFromDb == null)
      return;
    foreach (string str in ((PXSelectBase) this.MatrixItems).Cache.BqlFields.Select<System.Type, string>((Func<System.Type, string>) (f => f.Name)).ToHashSet<string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase).Except<string>(this.GetEditableChildFields().Concat<string>(this.GetEditableCurrencyChildFields()).Concat<string>((IEnumerable<string>) new string[1]
    {
      "Selected"
    }), (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase))
      ((PXSelectBase) this.MatrixItems).Cache.SetValue((object) rowFromCache, str, ((PXSelectBase) this.MatrixItems).Cache.GetValue((object) rowFromDb, str));
  }

  [PXUIField]
  [PXProcessButton]
  public virtual IEnumerable DeleteItems(PXAdapter adapter)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    ItemsGridExt.\u003C\u003Ec__DisplayClass14_0 cDisplayClass140 = new ItemsGridExt.\u003C\u003Ec__DisplayClass14_0();
    ((PXAction) this.Base.Save).Press();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass140.selectedItems = ((IEnumerable<ItemsGridExt.MatrixInventoryItem>) ((PXSelectBase<ItemsGridExt.MatrixInventoryItem>) this.MatrixItems).SelectMain(Array.Empty<object>())).Where<ItemsGridExt.MatrixInventoryItem>((Func<ItemsGridExt.MatrixInventoryItem, bool>) (i => i.Selected.GetValueOrDefault())).ToArray<ItemsGridExt.MatrixInventoryItem>();
    // ISSUE: method pointer
    PXLongOperation.StartOperation((PXGraph) this.Base, new PXToggleAsyncDelegate((object) cDisplayClass140, __methodptr(\u003CDeleteItems\u003Eb__1)));
    return adapter.Get();
  }

  protected virtual IEnumerable<string> GetEditableChildFields()
  {
    return (IEnumerable<string>) new string[1]{ "descr" };
  }

  protected virtual IEnumerable<string> GetEditableCurrencyChildFields()
  {
    return (IEnumerable<string>) new string[2]
    {
      "recPrice",
      "basePrice"
    };
  }

  protected virtual void ChildItemUpdated(
    PX.Data.Events.RowUpdated<ItemsGridExt.MatrixInventoryItem> eventArgs)
  {
    if (eventArgs.Row == null || eventArgs.OldRow == null)
      return;
    this.UpdateInventoryItemByChildItem(((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<ItemsGridExt.MatrixInventoryItem>>) eventArgs).Cache, eventArgs.OldRow, eventArgs.Row);
    this.UpdateInventoryItemCurySettingsByChildItem(((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<ItemsGridExt.MatrixInventoryItem>>) eventArgs).Cache, eventArgs.OldRow, eventArgs.Row);
  }

  protected virtual void UpdateInventoryItemByChildItem(
    PXCache cache,
    ItemsGridExt.MatrixInventoryItem oldRow,
    ItemsGridExt.MatrixInventoryItem row)
  {
    bool flag = false;
    InventoryItem inventoryItem = (InventoryItem) null;
    foreach (string editableChildField in this.GetEditableChildFields())
    {
      object objA = cache.GetValue((object) oldRow, editableChildField);
      object obj = cache.GetValue((object) row, editableChildField);
      object objB = obj;
      if (!object.Equals(objA, objB))
      {
        if (inventoryItem == null)
        {
          inventoryItem = InventoryItem.PK.FindDirty((PXGraph) this.Base, row.InventoryID);
          if (inventoryItem != null)
          {
            int? templateItemId = inventoryItem.TemplateItemID;
            int? inventoryId = (int?) ((PXSelectBase<InventoryItem>) this.Base.Item).Current?.InventoryID;
            if (templateItemId.GetValueOrDefault() == inventoryId.GetValueOrDefault() & templateItemId.HasValue == inventoryId.HasValue)
              goto label_7;
          }
          throw new RowNotFoundException(((PXSelectBase) this.Base.Item).Cache, new object[1]
          {
            (object) row.InventoryID
          });
        }
label_7:
        ((PXSelectBase) this.Base.Item).Cache.SetValue((object) inventoryItem, editableChildField, obj);
        flag = true;
      }
    }
    if (!flag)
      return;
    PXDBLocalizableStringAttribute.CopyTranslations<ItemsGridExt.MatrixInventoryItem.descr, InventoryItem.descr>((PXGraph) this.Base, (object) row, (object) inventoryItem);
    this.Base.UpdateChild(inventoryItem);
  }

  protected virtual void UpdateInventoryItemCurySettingsByChildItem(
    PXCache cache,
    ItemsGridExt.MatrixInventoryItem oldRow,
    ItemsGridExt.MatrixInventoryItem row)
  {
    bool flag = false;
    InventoryItemCurySettings curySettings = this.Base.GetCurySettings(row.InventoryID);
    foreach (string currencyChildField in this.GetEditableCurrencyChildFields())
    {
      object objA = cache.GetValue((object) oldRow, currencyChildField);
      object obj = cache.GetValue((object) row, currencyChildField);
      object objB = obj;
      if (!object.Equals(objA, objB))
      {
        ((PXSelectBase) this.Base.ItemCurySettings).Cache.SetValue((object) curySettings, currencyChildField, obj);
        flag = true;
      }
    }
    if (!flag)
      return;
    this.Base.UpdateChildCurySettings(curySettings);
  }

  [PXVirtual]
  [PXCacheName("Inventory Item with Attribute Values")]
  [PXBreakInheritance]
  public class MatrixInventoryItem : PX.Objects.IN.Matrix.DAC.Unbound.MatrixInventoryItem
  {
    [PXDefault]
    [InventoryRaw(DisplayName = "Inventory ID", IsKey = true)]
    public override string InventoryCD
    {
      get => base.InventoryCD;
      set => base.InventoryCD = value;
    }

    [AnyInventory(Visible = true, DisplayName = "Inventory ID")]
    public override int? InventoryID
    {
      get => base.InventoryID;
      set => base.InventoryID = value;
    }

    /// <summary>The description of the Inventory Item.</summary>
    [DBMatrixLocalizableDescription(256 /*0x0100*/, IsUnicode = true, CopyTranslationsToInventoryItem = true)]
    [PXUIField]
    [PXFieldDescription]
    public override string Descr
    {
      get => base.Descr;
      set => base.Descr = value;
    }

    /// <summary>
    /// The cost assigned to the item before the current cost was set.
    /// </summary>
    [PXPriceCost]
    [PXUIField(DisplayName = "Last Cost", Enabled = false)]
    public virtual Decimal? LastCost { get; set; }

    public new abstract class inventoryCD : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ItemsGridExt.MatrixInventoryItem.inventoryCD>
    {
    }

    public new abstract class inventoryID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ItemsGridExt.MatrixInventoryItem.inventoryID>
    {
    }

    public new abstract class descr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ItemsGridExt.MatrixInventoryItem.descr>
    {
    }

    public abstract class lastCost : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ItemsGridExt.MatrixInventoryItem.lastCost>
    {
    }
  }
}
