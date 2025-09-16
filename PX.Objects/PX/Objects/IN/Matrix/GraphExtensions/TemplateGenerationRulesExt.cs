// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.Matrix.GraphExtensions.TemplateGenerationRulesExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.Common.Attributes;
using PX.Objects.CS;
using PX.Objects.IN.Matrix.Attributes;
using PX.Objects.IN.Matrix.DAC;
using PX.Objects.IN.Matrix.DAC.Projections;
using PX.Objects.IN.Matrix.Graphs;
using PX.Objects.IN.Matrix.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.IN.Matrix.GraphExtensions;

public class TemplateGenerationRulesExt : 
  GenerationRulesExt<TemplateInventoryItemMaint, InventoryItem, InventoryItem.inventoryID, INMatrixGenerationRule.parentType.templateItem, InventoryItem.sampleID, InventoryItem.sampleDescription>
{
  protected virtual void _(Events.RowUpdated<CSAnswers> e)
  {
    if (e.Row.IsActive.GetValueOrDefault() || !e.OldRow.IsActive.GetValueOrDefault())
      return;
    foreach (PXResult<IDGenerationRule> pxResult in ((PXSelectBase<IDGenerationRule>) this.IDGenerationRules).Select(Array.Empty<object>()))
    {
      IDGenerationRule idGenerationRule = PXResult<IDGenerationRule>.op_Implicit(pxResult);
      if (idGenerationRule.AttributeID == e.Row.AttributeID)
      {
        idGenerationRule.AttributeID = (string) null;
        ((PXSelectBase<IDGenerationRule>) this.IDGenerationRules).Update(idGenerationRule);
      }
    }
    foreach (PXResult<DescriptionGenerationRule> pxResult in ((PXSelectBase<DescriptionGenerationRule>) this.DescriptionGenerationRules).Select(Array.Empty<object>()))
    {
      DescriptionGenerationRule descriptionGenerationRule = PXResult<DescriptionGenerationRule>.op_Implicit(pxResult);
      if (descriptionGenerationRule.AttributeID == e.Row.AttributeID)
      {
        descriptionGenerationRule.AttributeID = (string) null;
        ((PXSelectBase<DescriptionGenerationRule>) this.DescriptionGenerationRules).Update(descriptionGenerationRule);
      }
    }
  }

  [PXMergeAttributes]
  [PXRemoveBaseAttribute(typeof (PXDefaultAttribute))]
  [DefaultConditional(typeof (Search<INItemClass.defaultRowMatrixAttributeID, Where<INItemClass.itemClassID, Equal<Current<InventoryItem.parentItemClassID>>>>), typeof (InventoryItem.isTemplate), new object[] {true})]
  [PXUIRequired(typeof (Where<InventoryItem.defaultRowMatrixAttributeID, NotEqual<MatrixAttributeSelectorAttribute.dummyAttributeName>>))]
  protected virtual void _(
    Events.CacheAttached<InventoryItem.defaultRowMatrixAttributeID> eventArgs)
  {
  }

  [PXMergeAttributes]
  [PXRemoveBaseAttribute(typeof (PXDefaultAttribute))]
  [DefaultConditional(typeof (Search<INItemClass.defaultColumnMatrixAttributeID, Where<INItemClass.itemClassID, Equal<Current<InventoryItem.parentItemClassID>>>>), typeof (InventoryItem.isTemplate), new object[] {true})]
  [PXUIRequired(typeof (Where<InventoryItem.defaultColumnMatrixAttributeID, NotEqual<MatrixAttributeSelectorAttribute.dummyAttributeName>>))]
  protected virtual void _(
    Events.CacheAttached<InventoryItem.defaultColumnMatrixAttributeID> eventArgs)
  {
  }

  protected override string[] GetAttributeIDs()
  {
    return ((IEnumerable<CSAnswers>) this.Base.Answers.SelectMain(Array.Empty<object>())).Select<CSAnswers, string>((Func<CSAnswers, string>) (a => a.AttributeID)).ToArray<string>();
  }

  protected override InventoryItem GetTemplate()
  {
    return ((PXSelectBase<InventoryItem>) this.Base.ItemSettings).Current;
  }

  /// Overrides <see cref="M:PX.Objects.IN.Matrix.Graphs.TemplateInventoryItemMaint.ResetDefaultsOnItemClassChange(PX.Objects.IN.InventoryItem)" />
  [PXOverride]
  public virtual void ResetDefaultsOnItemClassChange(
    InventoryItem row,
    Action<InventoryItem> baseMethod)
  {
    if (baseMethod != null)
      baseMethod(row);
    PXCache cache = ((PXSelectBase) this.Base.Item).Cache;
    this.InsertDefaultAnswers();
    cache.SetDefaultExt<InventoryItem.defaultColumnMatrixAttributeID>((object) row);
    cache.SetDefaultExt<InventoryItem.defaultRowMatrixAttributeID>((object) row);
    IEnumerable<IDGenerationRule> idGenerationRules = GraphHelper.RowCast<IDGenerationRule>((IEnumerable) PXSelectBase<IDGenerationRule, PXViewOf<IDGenerationRule>.BasedOn<SelectFromBase<IDGenerationRule, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<IDGenerationRule.parentType, Equal<INMatrixGenerationRule.parentType.itemClass>>>>>.And<BqlOperand<IDGenerationRule.parentID, IBqlInt>.IsEqual<BqlField<InventoryItem.itemClassID, IBqlInt>.FromCurrent>>>.Order<By<Asc<IDGenerationRule.sortOrder>>>>.Config>.Select((PXGraph) this.Base, Array.Empty<object>()));
    EnumerableExtensions.ForEach<IDGenerationRule>((IEnumerable<IDGenerationRule>) ((PXSelectBase<IDGenerationRule>) this.IDGenerationRules).SelectMain(Array.Empty<object>()), (Action<IDGenerationRule>) (rule => ((PXSelectBase<IDGenerationRule>) this.IDGenerationRules).Delete(rule)));
    Action<IDGenerationRule> action1 = (Action<IDGenerationRule>) (classIdRule =>
    {
      IDGenerationRule idGenerationRule = PropertyTransfer.Transfer<IDGenerationRule, IDGenerationRule>(classIdRule, new IDGenerationRule());
      idGenerationRule.ParentID = row.InventoryID;
      idGenerationRule.ParentType = "T";
      idGenerationRule.LineNbr = new int?();
      ((PXSelectBase<IDGenerationRule>) this.IDGenerationRules).Insert(idGenerationRule);
    });
    EnumerableExtensions.ForEach<IDGenerationRule>(idGenerationRules, action1);
    IEnumerable<DescriptionGenerationRule> descriptionGenerationRules = GraphHelper.RowCast<DescriptionGenerationRule>((IEnumerable) PXSelectBase<DescriptionGenerationRule, PXViewOf<DescriptionGenerationRule>.BasedOn<SelectFromBase<DescriptionGenerationRule, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<DescriptionGenerationRule.parentType, Equal<INMatrixGenerationRule.parentType.itemClass>>>>>.And<BqlOperand<DescriptionGenerationRule.parentID, IBqlInt>.IsEqual<BqlField<InventoryItem.itemClassID, IBqlInt>.FromCurrent>>>.Order<By<Asc<DescriptionGenerationRule.sortOrder>>>>.Config>.Select((PXGraph) this.Base, Array.Empty<object>()));
    EnumerableExtensions.ForEach<DescriptionGenerationRule>((IEnumerable<DescriptionGenerationRule>) ((PXSelectBase<DescriptionGenerationRule>) this.DescriptionGenerationRules).SelectMain(Array.Empty<object>()), (Action<DescriptionGenerationRule>) (rule => ((PXSelectBase<DescriptionGenerationRule>) this.DescriptionGenerationRules).Delete(rule)));
    Action<DescriptionGenerationRule> action2 = (Action<DescriptionGenerationRule>) (classIdRule =>
    {
      DescriptionGenerationRule descriptionGenerationRule = PropertyTransfer.Transfer<DescriptionGenerationRule, DescriptionGenerationRule>(classIdRule, new DescriptionGenerationRule());
      descriptionGenerationRule.ParentID = row.InventoryID;
      descriptionGenerationRule.ParentType = "T";
      descriptionGenerationRule.LineNbr = new int?();
      ((PXSelectBase<DescriptionGenerationRule>) this.DescriptionGenerationRules).Insert(descriptionGenerationRule);
    });
    EnumerableExtensions.ForEach<DescriptionGenerationRule>(descriptionGenerationRules, action2);
  }

  protected virtual void InsertDefaultAnswers()
  {
    this.Base.Answers.SelectMain(Array.Empty<object>());
  }

  protected override int? GetAttributeLength(INMatrixGenerationRule row)
  {
    CreateMatrixItemsHelper matrixItemsHelper = this.Base.GetCreateMatrixItemsHelper();
    return new int?(row.SegmentType == "AV" ? matrixItemsHelper.GetAttributeValue(row, ((PXSelectBase<InventoryItem>) this.Base.Item).Current, (PX.Objects.IN.Matrix.DAC.Unbound.MatrixInventoryItem) null).Length : matrixItemsHelper.GetAttributeCaption(row, ((PXSelectBase<InventoryItem>) this.Base.Item).Current, (PX.Objects.IN.Matrix.DAC.Unbound.MatrixInventoryItem) null, (string) null).Length);
  }
}
