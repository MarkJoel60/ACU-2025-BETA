// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.Matrix.GraphExtensions.HeaderAndAttributesExt`2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.Common;
using PX.Objects.Common.Exceptions;
using PX.Objects.CS;
using PX.Objects.IN.Matrix.Attributes;
using PX.Objects.IN.Matrix.DAC.Unbound;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.IN.Matrix.GraphExtensions;

public abstract class HeaderAndAttributesExt<Graph, MainItemType> : PXGraphExtension<Graph>
  where Graph : PXGraph
  where MainItemType : class, IBqlTable, new()
{
  public PXFilter<EntryHeader> Header;
  public PXFilter<PX.Objects.IN.Matrix.DAC.Unbound.AdditionalAttributes> AdditionalAttributes;

  private event Action ResetState;

  public virtual bool ShowDisabledValue => true;

  public virtual void Initialize()
  {
    ((PXGraphExtension) this).Initialize();
    this.AddNeededFields();
    this.Base.Views.Caches.Remove(typeof (EntryHeader));
    this.Base.Views.Caches.Remove(typeof (PX.Objects.IN.Matrix.DAC.Unbound.AdditionalAttributes));
  }

  protected virtual void AddNeededFields()
  {
    int attributeNumber = 0;
    while (true)
    {
      int num = attributeNumber;
      int? length = ((PXSelectBase<PX.Objects.IN.Matrix.DAC.Unbound.AdditionalAttributes>) this.AdditionalAttributes).Current.Values?.Length;
      int valueOrDefault = length.GetValueOrDefault();
      if (num < valueOrDefault & length.HasValue)
      {
        this.AddFieldToAttributeGrid(((PXSelectBase) this.AdditionalAttributes).Cache, attributeNumber);
        ++attributeNumber;
      }
      else
        break;
    }
  }

  protected virtual void _(
    Events.FieldUpdated<EntryHeader, EntryHeader.templateItemID> eventArgs)
  {
    this.RecalcAttributesGrid();
  }

  protected virtual void _(
    Events.FieldUpdated<EntryHeader, EntryHeader.colAttributeID> eventArgs)
  {
    this.RecalcAttributesGrid();
  }

  protected virtual void _(
    Events.FieldUpdated<EntryHeader, EntryHeader.rowAttributeID> eventArgs)
  {
    this.RecalcAttributesGrid();
  }

  protected virtual void _(Events.RowSelected<EntryHeader> e)
  {
    Action resetState = this.ResetState;
    if (resetState != null)
      resetState();
    this.ResetState = (Action) null;
    this.AddNeededFields();
    this.Base.Views.Caches.Remove(typeof (EntryHeader));
    this.Base.Views.Caches.Remove(typeof (PX.Objects.IN.Matrix.DAC.Unbound.AdditionalAttributes));
  }

  protected virtual void _(Events.RowUpdated<CSAnswers> e)
  {
    if (!e.Row.IsActive.GetValueOrDefault() && e.OldRow.IsActive.GetValueOrDefault())
    {
      if (((PXSelectBase<EntryHeader>) this.Header).Current.ColAttributeID == e.Row.AttributeID)
      {
        ((PXSelectBase<EntryHeader>) this.Header).Current.ColAttributeID = (string) null;
        ((PXSelectBase<EntryHeader>) this.Header).UpdateCurrent();
      }
      if (((PXSelectBase<EntryHeader>) this.Header).Current.RowAttributeID == e.Row.AttributeID)
      {
        ((PXSelectBase<EntryHeader>) this.Header).Current.RowAttributeID = (string) null;
        ((PXSelectBase<EntryHeader>) this.Header).UpdateCurrent();
      }
    }
    bool? isActive1 = e.Row.IsActive;
    bool? isActive2 = e.OldRow.IsActive;
    if (isActive1.GetValueOrDefault() == isActive2.GetValueOrDefault() & isActive1.HasValue == isActive2.HasValue)
      return;
    PXCacheEx.AdjustReadonly<MatrixAttributeSelectorAttribute>(((PXSelectBase) this.Header).Cache, (object) null).ForAllFields((Action<MatrixAttributeSelectorAttribute>) (a => a.RefreshDummyValue(((PXSelectBase) this.Header).Cache, (object) ((PXSelectBase<EntryHeader>) this.Header).Current)));
    this.RecalcAttributesGrid();
  }

  protected virtual void RecalcAttributesGrid()
  {
    CSAttribute[] additionalAttributes = this.GetAdditionalAttributes();
    ((PXSelectBase<PX.Objects.IN.Matrix.DAC.Unbound.AdditionalAttributes>) this.AdditionalAttributes).Current.Values = new string[additionalAttributes.Length];
    ((PXSelectBase<PX.Objects.IN.Matrix.DAC.Unbound.AdditionalAttributes>) this.AdditionalAttributes).Current.Descriptions = new string[additionalAttributes.Length];
    ((PXSelectBase<PX.Objects.IN.Matrix.DAC.Unbound.AdditionalAttributes>) this.AdditionalAttributes).Current.AttributeIdentifiers = new string[additionalAttributes.Length];
    ((PXSelectBase<PX.Objects.IN.Matrix.DAC.Unbound.AdditionalAttributes>) this.AdditionalAttributes).Current.AttributeDisplayNames = new string[additionalAttributes.Length];
    ((PXSelectBase<PX.Objects.IN.Matrix.DAC.Unbound.AdditionalAttributes>) this.AdditionalAttributes).Current.ViewNames = new string[additionalAttributes.Length];
    if (additionalAttributes.Length != 0)
    {
      for (int index = 0; index < additionalAttributes.Length; ++index)
      {
        CSAttribute csAttribute = additionalAttributes[index];
        ((PXSelectBase<PX.Objects.IN.Matrix.DAC.Unbound.AdditionalAttributes>) this.AdditionalAttributes).Current.AttributeIdentifiers[index] = csAttribute.AttributeID;
        ((PXSelectBase<PX.Objects.IN.Matrix.DAC.Unbound.AdditionalAttributes>) this.AdditionalAttributes).Current.AttributeDisplayNames[index] = csAttribute.Description;
      }
      for (int attributeNumber = 0; attributeNumber < ((PXSelectBase<PX.Objects.IN.Matrix.DAC.Unbound.AdditionalAttributes>) this.AdditionalAttributes).Current.Values.Length; ++attributeNumber)
        this.AddFieldToAttributeGrid(((PXSelectBase) this.AdditionalAttributes).Cache, attributeNumber);
    }
    ((PXSelectBase) this.AdditionalAttributes).View.RequestRefresh();
  }

  protected virtual InventoryItem GetTemplateItem()
  {
    return InventoryItem.PK.Find((PXGraph) this.Base, ((PXSelectBase<EntryHeader>) this.Header).Current.TemplateItemID);
  }

  protected virtual CSAttribute[] GetAdditionalAttributes()
  {
    InventoryItem templateItem = this.GetTemplateItem();
    return ((PXSelectBase<CSAttribute>) new PXSelectReadonly2<CSAttribute, InnerJoin<CSAttributeGroup, On<CSAttributeGroup.attributeID, Equal<CSAttribute.attributeID>>>, Where<CSAttributeGroup.isActive, Equal<True>, And<CSAttributeGroup.entityClassID, Equal<Required<InventoryItem.itemClassID>>, And<CSAttributeGroup.entityType, Equal<Constants.DACName<InventoryItem>>, And<CSAttributeGroup.attributeCategory, Equal<CSAttributeGroup.attributeCategory.variant>, And<CSAttribute.attributeID, NotEqual<Current2<EntryHeader.colAttributeID>>, And<CSAttribute.attributeID, NotEqual<Current2<EntryHeader.rowAttributeID>>, And<NotExists<Select<CSAnswers, Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<CSAnswers.isActive, Equal<False>>>>, And<BqlOperand<CSAnswers.attributeID, IBqlString>.IsEqual<CSAttribute.attributeID>>>>.And<BqlOperand<CSAnswers.refNoteID, IBqlGuid>.IsEqual<P.AsGuid>>>>>>>>>>>>, OrderBy<Asc<CSAttributeGroup.sortOrder>>>((PXGraph) this.Base)).SelectMain(new object[2]
    {
      (object) (int?) templateItem?.ParentItemClassID,
      (object) (Guid?) templateItem?.NoteID
    });
  }

  protected virtual void AddFieldToAttributeGrid(PXCache cache, int attributeNumber)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    HeaderAndAttributesExt<Graph, MainItemType>.\u003C\u003Ec__DisplayClass17_0 cDisplayClass170 = new HeaderAndAttributesExt<Graph, MainItemType>.\u003C\u003Ec__DisplayClass17_0()
    {
      \u003C\u003E4__this = this,
      attributeNumber = attributeNumber,
      cache = cache
    };
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    cDisplayClass170.fieldName = $"AttributeValue{cDisplayClass170.attributeNumber}";
    // ISSUE: reference to a compiler-generated field
    this.FillAttributeViewName(cDisplayClass170.attributeNumber);
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    if (cDisplayClass170.cache.Fields.Contains(cDisplayClass170.fieldName))
      return;
    Type itemType = cache.GetItemType();
    // ISSUE: method pointer
    PXFieldSelecting selectingDel = new PXFieldSelecting((object) cDisplayClass170, __methodptr(\u003CAddFieldToAttributeGrid\u003Eb__0));
    // ISSUE: method pointer
    PXFieldUpdating updatingDel = new PXFieldUpdating((object) cDisplayClass170, __methodptr(\u003CAddFieldToAttributeGrid\u003Eb__1));
    cache.Fields.Add(fieldName);
    this.Base.FieldSelecting.AddHandler(itemType, fieldName, selectingDel);
    this.Base.FieldUpdating.AddHandler(itemType, fieldName, updatingDel);
    this.ResetState += (Action) (() =>
    {
      cache.Fields.Remove(fieldName);
      this.Base.FieldSelecting.RemoveHandler(itemType, fieldName, selectingDel);
      this.Base.FieldUpdating.RemoveHandler(itemType, fieldName, updatingDel);
    });
  }

  protected virtual void FillAttributeViewName(int attributeNumber)
  {
    int num = attributeNumber;
    int? length = ((PXSelectBase<PX.Objects.IN.Matrix.DAC.Unbound.AdditionalAttributes>) this.AdditionalAttributes).Current.ViewNames?.Length;
    int valueOrDefault = length.GetValueOrDefault();
    if (!(num < valueOrDefault & length.HasValue))
      return;
    string str = "attr" + ((PXSelectBase<PX.Objects.IN.Matrix.DAC.Unbound.AdditionalAttributes>) this.AdditionalAttributes).Current.AttributeIdentifiers[attributeNumber];
    MatrixAttributeValueSelectorAttribute selectorAttribute = new MatrixAttributeValueSelectorAttribute(attributeNumber, this.ShowDisabledValue);
    ((PXEventSubscriberAttribute) selectorAttribute).FieldName = str;
    ((PXEventSubscriberAttribute) selectorAttribute).CacheAttached(((PXSelectBase) this.AdditionalAttributes).Cache);
    PXFieldSelectingEventArgs selectingEventArgs = new PXFieldSelectingEventArgs((object) null, (object) null, true, false);
    ((PXSelectorAttribute) selectorAttribute).FieldSelecting(((PXSelectBase) this.AdditionalAttributes).Cache, selectingEventArgs);
    PXFieldState returnState = (PXFieldState) selectingEventArgs.ReturnState;
    ((PXSelectBase<PX.Objects.IN.Matrix.DAC.Unbound.AdditionalAttributes>) this.AdditionalAttributes).Current.ViewNames[attributeNumber] = returnState.ViewName;
  }

  protected virtual void AttributeValueFieldSelecting(
    int attributeNumber,
    PXFieldSelectingEventArgs e,
    string fieldName)
  {
    PXFieldState instance = PXFieldState.CreateInstance(e.ReturnState, typeof (string), new bool?(false), new bool?(true), new int?(1), new int?(), new int?(), (object) null, fieldName, (string) null, (string) null, (string) null, (PXErrorLevel) 0, new bool?(), new bool?(), new bool?(), (PXUIVisibility) 0, (string) null, (string[]) null, (string[]) null);
    e.ReturnState = (object) instance;
    int num = attributeNumber;
    int? length = ((PXSelectBase<PX.Objects.IN.Matrix.DAC.Unbound.AdditionalAttributes>) this.AdditionalAttributes).Current.ViewNames?.Length;
    int valueOrDefault = length.GetValueOrDefault();
    if (num < valueOrDefault & length.HasValue)
    {
      e.ReturnValue = (object) this.GetAttributeValue(e.Row, attributeNumber);
      instance.DisplayName = ((PXSelectBase<PX.Objects.IN.Matrix.DAC.Unbound.AdditionalAttributes>) this.AdditionalAttributes).Current.AttributeDisplayNames[attributeNumber];
      instance.Visible = true;
      instance.Visibility = (PXUIVisibility) 3;
      instance.Enabled = true;
      instance.ValueField = "valueID";
      instance.ViewName = ((PXSelectBase<PX.Objects.IN.Matrix.DAC.Unbound.AdditionalAttributes>) this.AdditionalAttributes).Current.ViewNames[attributeNumber];
      instance.DescriptionName = "description";
      instance.FieldList = new string[2]
      {
        "valueID",
        "description"
      };
      PXCache<CSAttributeDetail> pxCache = GraphHelper.Caches<CSAttributeDetail>((PXGraph) this.Base);
      instance.HeaderList = new string[2]
      {
        PXUIFieldAttribute.GetDisplayName<CSAttributeDetail.valueID>((PXCache) pxCache),
        PXUIFieldAttribute.GetDisplayName<CSAttributeDetail.description>((PXCache) pxCache)
      };
    }
    else
    {
      instance.Value = (object) null;
      e.ReturnValue = (object) null;
      instance.DisplayName = (string) null;
      instance.Visible = false;
      instance.Visibility = (PXUIVisibility) 1;
      instance.Enabled = false;
      instance.ViewName = (string) null;
    }
  }

  protected virtual string GetAttributeValue(object row, int attributeNumber)
  {
    string str = ((PXSelectBase<PX.Objects.IN.Matrix.DAC.Unbound.AdditionalAttributes>) this.AdditionalAttributes).Current.Values[attributeNumber];
    if (string.IsNullOrEmpty(str))
      return (string) null;
    string description = ((PXSelectBase<PX.Objects.IN.Matrix.DAC.Unbound.AdditionalAttributes>) this.AdditionalAttributes).Current.Descriptions[attributeNumber];
    return string.IsNullOrEmpty(description) ? str : $"{str} - {description}";
  }

  protected virtual void AttributeValueFieldUpdating(
    int attributeNumber,
    PXFieldUpdatingEventArgs e)
  {
    if (!(e.Row is PX.Objects.IN.Matrix.DAC.Unbound.AdditionalAttributes row))
      return;
    string newValue = e.NewValue as string;
    int num = attributeNumber;
    int? length = row.Values?.Length;
    int valueOrDefault = length.GetValueOrDefault();
    if (!(num < valueOrDefault & length.HasValue) || !(this.GetAttributeValue((object) row, attributeNumber) != newValue))
      return;
    PXSelect<CSAttributeDetail, Where<CSAttributeDetail.attributeID, Equal<Required<CSAttributeDetail.attributeID>>, And<CSAttributeDetail.valueID, Equal<Required<CSAttributeDetail.valueID>>>>> pxSelect = new PXSelect<CSAttributeDetail, Where<CSAttributeDetail.attributeID, Equal<Required<CSAttributeDetail.attributeID>>, And<CSAttributeDetail.valueID, Equal<Required<CSAttributeDetail.valueID>>>>>((PXGraph) this.Base);
    if (!this.ShowDisabledValue)
      ((PXSelectBase<CSAttributeDetail>) pxSelect).WhereAnd<Where<CSAttributeDetail.disabled, NotEqual<True>>>();
    CSAttributeDetail csAttributeDetail = PXResultset<CSAttributeDetail>.op_Implicit(((PXSelectBase<CSAttributeDetail>) pxSelect).Select(new object[2]
    {
      (object) ((PXSelectBase<PX.Objects.IN.Matrix.DAC.Unbound.AdditionalAttributes>) this.AdditionalAttributes).Current.AttributeIdentifiers[attributeNumber],
      (object) newValue
    }));
    row.Values[attributeNumber] = csAttributeDetail != null ? csAttributeDetail.ValueID : throw new RowNotFoundException((PXCache) GraphHelper.Caches<CSAttributeDetail>((PXGraph) this.Base), new object[2]
    {
      (object) ((PXSelectBase<PX.Objects.IN.Matrix.DAC.Unbound.AdditionalAttributes>) this.AdditionalAttributes).Current.AttributeIdentifiers[attributeNumber],
      (object) newValue
    });
    row.Descriptions[attributeNumber] = csAttributeDetail.Description;
  }

  protected virtual bool AllAdditionalAttributesArePopulated()
  {
    PX.Objects.IN.Matrix.DAC.Unbound.AdditionalAttributes current = ((PXSelectBase<PX.Objects.IN.Matrix.DAC.Unbound.AdditionalAttributes>) this.AdditionalAttributes).Current;
    if (current == null)
      return false;
    string[] values = current.Values;
    bool? nullable = values != null ? new bool?(((IEnumerable<string>) values).Any<string>((Func<string, bool>) (v => string.IsNullOrEmpty(v)))) : new bool?();
    bool flag = false;
    return nullable.GetValueOrDefault() == flag & nullable.HasValue;
  }
}
