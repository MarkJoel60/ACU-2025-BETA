// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.Matrix.GraphExtensions.CreateMatrixItemsTabExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.Common;
using PX.Objects.CS;
using PX.Objects.IN.Matrix.Attributes;
using PX.Objects.IN.Matrix.DAC;
using PX.Objects.IN.Matrix.DAC.Projections;
using PX.Objects.IN.Matrix.DAC.Unbound;
using PX.Objects.IN.Matrix.Graphs;
using PX.Objects.IN.Matrix.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.IN.Matrix.GraphExtensions;

public class CreateMatrixItemsTabExt : 
  CreateMatrixItemsExt<TemplateInventoryItemMaint, InventoryItem>
{
  public new PXAction<InventoryItem> createUpdate;

  public override void Initialize()
  {
    ((PXGraph) this.Base).Views.Caches.Remove(typeof (PX.Objects.IN.Matrix.DAC.Unbound.AdditionalAttributes));
    ((PXGraph) this.Base).Views.Caches.Remove(typeof (EntryMatrix));
    ((PXGraph) this.Base).Views.Caches.Remove(typeof (PX.Objects.IN.Matrix.DAC.Unbound.MatrixInventoryItem));
    base.Initialize();
  }

  [PXUIField]
  [PXButton]
  public override IEnumerable CreateUpdate(PXAdapter adapter)
  {
    ((PXAction) this.Base.Save).Press();
    return base.CreateUpdate(adapter);
  }

  protected override int? GetTemplateID()
  {
    return (int?) ((PXSelectBase<InventoryItem>) this.Base.Item).Current?.InventoryID ?? base.GetTemplateID();
  }

  protected override InventoryItem GetTemplateItem()
  {
    return ((PXSelectBase<InventoryItem>) this.Base.Item).Current ?? base.GetTemplateItem();
  }

  protected override void GetGenerationRules(
    CreateMatrixItemsHelper helper,
    out List<INMatrixGenerationRule> idGenerationRules,
    out List<INMatrixGenerationRule> descrGenerationRules)
  {
    TemplateGenerationRulesExt extension = ((PXGraph) this.Base).GetExtension<TemplateGenerationRulesExt>();
    idGenerationRules = ((IEnumerable<IDGenerationRule>) ((PXSelectBase<IDGenerationRule>) extension.IDGenerationRules).SelectMain(Array.Empty<object>())).Select<IDGenerationRule, INMatrixGenerationRule>((Func<IDGenerationRule, INMatrixGenerationRule>) (s => (INMatrixGenerationRule) s)).ToList<INMatrixGenerationRule>();
    descrGenerationRules = ((IEnumerable<DescriptionGenerationRule>) ((PXSelectBase<DescriptionGenerationRule>) extension.DescriptionGenerationRules).SelectMain(Array.Empty<object>())).Select<DescriptionGenerationRule, INMatrixGenerationRule>((Func<DescriptionGenerationRule, INMatrixGenerationRule>) (s => (INMatrixGenerationRule) s)).ToList<INMatrixGenerationRule>();
  }

  protected override CSAttribute[] GetAdditionalAttributes()
  {
    InventoryItem templateItem = this.GetTemplateItem();
    CSAttribute[] source = ((PXSelectBase<CSAttribute>) new PXSelectReadonly2<CSAttribute, InnerJoin<CSAttributeGroup, On<CSAttributeGroup.attributeID, Equal<CSAttribute.attributeID>>>, Where<CSAttributeGroup.isActive, Equal<True>, And<CSAttributeGroup.entityClassID, Equal<Required<InventoryItem.itemClassID>>, And<CSAttributeGroup.entityType, Equal<Constants.DACName<InventoryItem>>, And<CSAttributeGroup.attributeCategory, Equal<CSAttributeGroup.attributeCategory.variant>, And<CSAttribute.attributeID, NotEqual<Current2<EntryHeader.colAttributeID>>, And<CSAttribute.attributeID, NotEqual<Current2<EntryHeader.rowAttributeID>>>>>>>>, OrderBy<Asc<CSAttributeGroup.sortOrder>>>((PXGraph) this.Base)).SelectMain(new object[2]
    {
      (object) (int?) templateItem?.ParentItemClassID,
      (object) (Guid?) templateItem?.NoteID
    });
    Dictionary<string, CSAnswers> answers = ((IEnumerable<CSAnswers>) this.Base.Answers.SelectMain(Array.Empty<object>())).ToDictionary<CSAnswers, string>((Func<CSAnswers, string>) (a => a.AttributeID));
    Func<CSAttribute, bool> predicate = (Func<CSAttribute, bool>) (a =>
    {
      CSAnswers csAnswers;
      if (!answers.TryGetValue(a.AttributeID, out csAnswers))
        return true;
      bool? isActive = csAnswers.IsActive;
      bool flag = false;
      return !(isActive.GetValueOrDefault() == flag & isActive.HasValue);
    });
    return ((IEnumerable<CSAttribute>) source).Where<CSAttribute>(predicate).ToArray<CSAttribute>();
  }

  protected virtual void _(
    Events.FieldUpdated<InventoryItem, InventoryItem.itemClassID> eventArgs)
  {
    this.RecalcAttributesGrid();
  }

  protected virtual void _(Events.RowPersisted<InventoryItem> eventArgs)
  {
    InventoryItem row = eventArgs.Row;
    if ((row != null ? (row.IsTemplate.GetValueOrDefault() ? 1 : 0) : 0) == 0 || eventArgs.TranStatus != 1)
      return;
    ((PXSelectBase<EntryHeader>) this.Header).Current.TemplateItemID = eventArgs.Row.InventoryID;
  }

  protected override void _(Events.RowSelected<EntryHeader> eventArgs)
  {
    base._(eventArgs);
    ((PXGraph) this.Base).Views.Caches.Remove(typeof (PX.Objects.IN.Matrix.DAC.Unbound.MatrixInventoryItem));
    PXUIFieldAttribute.SetEnabled<EntryHeader.rowAttributeID>(((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<EntryHeader>>) eventArgs).Cache, (object) eventArgs.Row, ((PXSelectBase<InventoryItem>) this.Base.Item).Current?.DefaultRowMatrixAttributeID != null);
    PXUIFieldAttribute.SetEnabled<EntryHeader.colAttributeID>(((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<EntryHeader>>) eventArgs).Cache, (object) eventArgs.Row, ((PXSelectBase<InventoryItem>) this.Base.Item).Current?.DefaultColumnMatrixAttributeID != null);
  }

  protected virtual void _(
    Events.FieldUpdated<InventoryItem, InventoryItem.defaultColumnMatrixAttributeID> eventArgs)
  {
    ((PXSelectBase<EntryHeader>) this.Header).SetValueExt<EntryHeader.colAttributeID>(((PXSelectBase<EntryHeader>) this.Header).Current, eventArgs.NewValue);
  }

  protected virtual void _(
    Events.FieldUpdated<InventoryItem, InventoryItem.defaultRowMatrixAttributeID> eventArgs)
  {
    ((PXSelectBase<EntryHeader>) this.Header).SetValueExt<EntryHeader.rowAttributeID>(((PXSelectBase<EntryHeader>) this.Header).Current, eventArgs.NewValue);
  }

  protected virtual void _(Events.RowSelected<InventoryItem> eventArgs)
  {
    InventoryItem row1 = eventArgs.Row;
    if ((row1 != null ? (row1.IsTemplate.GetValueOrDefault() ? 1 : 0) : 0) == 0 || ((PXSelectBase<EntryHeader>) this.Header).Current == null)
      return;
    int? templateItemId = ((PXSelectBase<EntryHeader>) this.Header).Current.TemplateItemID;
    int? nullable1 = eventArgs.Row.InventoryID;
    if (templateItemId.GetValueOrDefault() == nullable1.GetValueOrDefault() & templateItemId.HasValue == nullable1.HasValue)
      return;
    EntryHeader current = ((PXSelectBase<EntryHeader>) this.Header).Current;
    InventoryItem row2 = eventArgs.Row;
    int? nullable2;
    if (row2 == null)
    {
      nullable1 = new int?();
      nullable2 = nullable1;
    }
    else
      nullable2 = row2.InventoryID;
    current.TemplateItemID = nullable2;
    this.RecalcAttributesGrid();
  }

  [PXMergeAttributes]
  [PXRemoveBaseAttribute(typeof (PXDefaultAttribute))]
  [PXRemoveBaseAttribute(typeof (PXFormulaAttribute))]
  [PXRemoveBaseAttribute(typeof (PXSelectorAttribute))]
  [PXDefault(typeof (InventoryItem.defaultColumnMatrixAttributeID))]
  [MatrixAttributeSelector(typeof (Search2<CSAnswers.attributeID, InnerJoin<CSAttributeGroup, On<BqlOperand<CSAttributeGroup.attributeID, IBqlString>.IsEqual<CSAnswers.attributeID>>>, Where<CSAnswers.refNoteID, Equal<Current<InventoryItem.noteID>>, And<CSAnswers.isActive, Equal<True>, And<CSAttributeGroup.isActive, Equal<True>, And<CSAttributeGroup.entityClassID, Equal<Current<InventoryItem.parentItemClassID>>, And<CSAttributeGroup.entityType, Equal<Constants.DACName<InventoryItem>>, And<CSAttributeGroup.attributeCategory, Equal<CSAttributeGroup.attributeCategory.variant>>>>>>>>), typeof (EntryHeader.rowAttributeID), true, new Type[] {typeof (CSAttributeGroup.attributeID)}, DescriptionField = typeof (CSAttributeGroup.description), DirtyRead = true)]
  protected virtual void _(
    Events.CacheAttached<EntryHeader.colAttributeID> eventArgs)
  {
  }

  [PXMergeAttributes]
  [PXRemoveBaseAttribute(typeof (PXDefaultAttribute))]
  [PXRemoveBaseAttribute(typeof (PXFormulaAttribute))]
  [PXRemoveBaseAttribute(typeof (PXSelectorAttribute))]
  [PXDefault(typeof (InventoryItem.defaultRowMatrixAttributeID))]
  [MatrixAttributeSelector(typeof (Search2<CSAnswers.attributeID, InnerJoin<CSAttributeGroup, On<BqlOperand<CSAttributeGroup.attributeID, IBqlString>.IsEqual<CSAnswers.attributeID>>>, Where<CSAnswers.refNoteID, Equal<Current<InventoryItem.noteID>>, And<CSAnswers.isActive, Equal<True>, And<CSAttributeGroup.isActive, Equal<True>, And<CSAttributeGroup.entityClassID, Equal<Current<InventoryItem.parentItemClassID>>, And<CSAttributeGroup.entityType, Equal<Constants.DACName<InventoryItem>>, And<CSAttributeGroup.attributeCategory, Equal<CSAttributeGroup.attributeCategory.variant>>>>>>>>), typeof (EntryHeader.colAttributeID), true, new Type[] {typeof (CSAttributeGroup.attributeID)}, DescriptionField = typeof (CSAttributeGroup.description), DirtyRead = true)]
  protected virtual void _(
    Events.CacheAttached<EntryHeader.rowAttributeID> eventArgs)
  {
  }
}
