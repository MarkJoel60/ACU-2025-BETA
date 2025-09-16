// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.Matrix.GraphExtensions.GenerationRulesExt`6
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Common.Collection;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.IN.Matrix.DAC;
using PX.Objects.IN.Matrix.DAC.Projections;
using PX.Objects.IN.Matrix.Interfaces;
using PX.Objects.IN.Matrix.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.IN.Matrix.GraphExtensions;

public abstract class GenerationRulesExt<TGraph, TParent, TParentID, TParentType, TSampleIDField, TSampleDescriptionField> : 
  PXGraphExtension<TGraph>
  where TGraph : PXGraph, ICreateMatrixHelperFactory
  where TParent : class, IBqlTable, new()
  where TParentID : IBqlField
  where TParentType : class, IBqlOperand
  where TSampleIDField : IBqlField
  where TSampleDescriptionField : IBqlField
{
  private const string SampleField = "Sample";
  public PXSelect<IDGenerationRule, Where<IDGenerationRule.parentID, Equal<Current<TParentID>>, And<IDGenerationRule.parentType, Equal<TParentType>>>, OrderBy<Asc<IDGenerationRule.sortOrder>>> IDGenerationRules;
  public PXSelect<DescriptionGenerationRule, Where<DescriptionGenerationRule.parentID, Equal<Current<TParentID>>, And<DescriptionGenerationRule.parentType, Equal<TParentType>>>, OrderBy<Asc<DescriptionGenerationRule.sortOrder>>> DescriptionGenerationRules;
  public PXAction<InventoryItem> IdRowUp;
  public PXAction<InventoryItem> IdRowDown;
  public PXAction<InventoryItem> DescriptionRowUp;
  public PXAction<InventoryItem> DescriptionRowDown;

  public virtual void Initialize()
  {
    ((PXGraphExtension) this).Initialize();
    ((PXSelectBase) this.IDGenerationRules).Cache.Fields.Add("Sample");
    ((PXSelectBase) this.DescriptionGenerationRules).Cache.Fields.Add("Sample");
    // ISSUE: method pointer
    this.Base.FieldSelecting.AddHandler(typeof (IDGenerationRule), "Sample", new PXFieldSelecting((object) this, __methodptr(SampleIDFieldSelecting)));
    // ISSUE: method pointer
    this.Base.FieldSelecting.AddHandler(typeof (DescriptionGenerationRule), "Sample", new PXFieldSelecting((object) this, __methodptr(SampleDescriptionFieldSelecting)));
  }

  [PXMergeAttributes]
  [PXDefault(true)]
  protected virtual void _(
    PX.Data.Events.CacheAttached<IDGenerationRule.addSpaces> eventArgs)
  {
  }

  [PXMergeAttributes]
  [PXDefault(false)]
  protected virtual void _(
    PX.Data.Events.CacheAttached<DescriptionGenerationRule.addSpaces> eventArgs)
  {
  }

  protected virtual void _(PX.Data.Events.RowInserted<IDGenerationRule> eventArgs)
  {
    this.GenerationRuleRowInserted(((PX.Data.Events.Event<PXRowInsertedEventArgs, PX.Data.Events.RowInserted<IDGenerationRule>>) eventArgs).Cache, (INMatrixGenerationRule) eventArgs.Row);
    this.ResetValue<TSampleIDField>();
  }

  protected virtual void _(
    PX.Data.Events.RowInserted<DescriptionGenerationRule> eventArgs)
  {
    this.GenerationRuleRowInserted(((PX.Data.Events.Event<PXRowInsertedEventArgs, PX.Data.Events.RowInserted<DescriptionGenerationRule>>) eventArgs).Cache, (INMatrixGenerationRule) eventArgs.Row);
    this.ResetValue<TSampleDescriptionField>();
  }

  protected virtual void GenerationRuleRowInserted(PXCache cache, INMatrixGenerationRule row)
  {
    row.SortOrder = row.LineNbr;
  }

  protected virtual void _(PX.Data.Events.RowUpdated<IDGenerationRule> eventArgs)
  {
    this.ResetValue<TSampleIDField>();
  }

  protected virtual void _(
    PX.Data.Events.RowUpdated<DescriptionGenerationRule> eventArgs)
  {
    this.ResetValue<TSampleDescriptionField>();
  }

  protected virtual void _(PX.Data.Events.RowDeleted<IDGenerationRule> eventArgs)
  {
    this.ResetValue<TSampleIDField>();
  }

  protected virtual void _(
    PX.Data.Events.RowDeleted<DescriptionGenerationRule> eventArgs)
  {
    this.ResetValue<TSampleDescriptionField>();
  }

  public void SampleIDFieldSelecting(PXCache sender, PXFieldSelectingEventArgs args)
  {
    args.ReturnState = (object) PXStringState.CreateInstance(args.ReturnState, new int?(), new bool?(true), "Sample", new bool?(false), new int?(0), (string) null, (string[]) null, (string[]) null, new bool?(), (string) null, (string[]) null);
    args.ReturnValue = (object) this.GetGenerationRuleSample<TSampleIDField, IDGenerationRule>((PXSelectBase<IDGenerationRule>) this.IDGenerationRules, "Inventory ID Example: {0}");
  }

  public void SampleDescriptionFieldSelecting(PXCache sender, PXFieldSelectingEventArgs args)
  {
    args.ReturnState = (object) PXStringState.CreateInstance(args.ReturnState, new int?(), new bool?(true), "Sample", new bool?(false), new int?(0), (string) null, (string[]) null, (string[]) null, new bool?(), (string) null, (string[]) null);
    args.ReturnValue = (object) this.GetGenerationRuleSample<TSampleDescriptionField, DescriptionGenerationRule>((PXSelectBase<DescriptionGenerationRule>) this.DescriptionGenerationRules, "Description Example: {0}");
  }

  [PXUIField]
  [PXButton(ImageKey = "ArrowUp", Tooltip = "Move Row Up")]
  public virtual IEnumerable idRowUp(PXAdapter adapter)
  {
    this.MoveCurrentRow(((PXSelectBase) this.IDGenerationRules).Cache, (IEnumerable<INMatrixGenerationRule>) ((PXSelectBase<IDGenerationRule>) this.IDGenerationRules).SelectMain(Array.Empty<object>()), true);
    ((PXSelectBase) this.IDGenerationRules).View.RequestRefresh();
    return adapter.Get();
  }

  [PXUIField]
  [PXButton(ImageKey = "ArrowDown", Tooltip = "Move Row Down")]
  public virtual IEnumerable idRowDown(PXAdapter adapter)
  {
    this.MoveCurrentRow(((PXSelectBase) this.IDGenerationRules).Cache, (IEnumerable<INMatrixGenerationRule>) ((PXSelectBase<IDGenerationRule>) this.IDGenerationRules).SelectMain(Array.Empty<object>()), false);
    ((PXSelectBase) this.IDGenerationRules).View.RequestRefresh();
    return adapter.Get();
  }

  [PXUIField]
  [PXButton(ImageKey = "ArrowUp", Tooltip = "Move Row Up")]
  public virtual IEnumerable descriptionRowUp(PXAdapter adapter)
  {
    this.MoveCurrentRow(((PXSelectBase) this.DescriptionGenerationRules).Cache, (IEnumerable<INMatrixGenerationRule>) ((PXSelectBase<DescriptionGenerationRule>) this.DescriptionGenerationRules).SelectMain(Array.Empty<object>()), true);
    ((PXSelectBase) this.DescriptionGenerationRules).View.RequestRefresh();
    return adapter.Get();
  }

  [PXUIField]
  [PXButton(ImageKey = "ArrowDown", Tooltip = "Move Row Down")]
  public virtual IEnumerable descriptionRowDown(PXAdapter adapter)
  {
    this.MoveCurrentRow(((PXSelectBase) this.DescriptionGenerationRules).Cache, (IEnumerable<INMatrixGenerationRule>) ((PXSelectBase<DescriptionGenerationRule>) this.DescriptionGenerationRules).SelectMain(Array.Empty<object>()), false);
    ((PXSelectBase) this.DescriptionGenerationRules).View.RequestRefresh();
    return adapter.Get();
  }

  public void MoveCurrentRow(PXCache cache, IEnumerable<INMatrixGenerationRule> allRows, bool up)
  {
    INMatrixGenerationRule currentLine = (INMatrixGenerationRule) cache.Current;
    if (currentLine == null)
      return;
    INMatrixGenerationRule matrixGenerationRule1 = !up ? allRows.Where<INMatrixGenerationRule>((Func<INMatrixGenerationRule, bool>) (r =>
    {
      int? sortOrder1 = r.SortOrder;
      int? sortOrder2 = currentLine.SortOrder;
      return sortOrder1.GetValueOrDefault() > sortOrder2.GetValueOrDefault() & sortOrder1.HasValue & sortOrder2.HasValue;
    })).OrderBy<INMatrixGenerationRule, int?>((Func<INMatrixGenerationRule, int?>) (r => r.SortOrder)).FirstOrDefault<INMatrixGenerationRule>() : allRows.Where<INMatrixGenerationRule>((Func<INMatrixGenerationRule, bool>) (r =>
    {
      int? sortOrder3 = r.SortOrder;
      int? sortOrder4 = currentLine.SortOrder;
      return sortOrder3.GetValueOrDefault() < sortOrder4.GetValueOrDefault() & sortOrder3.HasValue & sortOrder4.HasValue;
    })).OrderByDescending<INMatrixGenerationRule, int?>((Func<INMatrixGenerationRule, int?>) (r => r.SortOrder)).FirstOrDefault<INMatrixGenerationRule>();
    if (matrixGenerationRule1 == null)
      return;
    int? sortOrder5 = currentLine.SortOrder;
    int? sortOrder6 = matrixGenerationRule1.SortOrder;
    matrixGenerationRule1.SortOrder = sortOrder5;
    currentLine.SortOrder = sortOrder6;
    INMatrixGenerationRule matrixGenerationRule2 = (INMatrixGenerationRule) cache.Update((object) matrixGenerationRule1);
    currentLine = (INMatrixGenerationRule) cache.Update((object) currentLine);
    cache.Current = (object) currentLine;
  }

  protected virtual void _(PX.Data.Events.RowSelected<IDGenerationRule> eventArgs)
  {
    this.GenerationRuleRowSelected(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<IDGenerationRule>>) eventArgs).Cache, (INMatrixGenerationRule) eventArgs.Row);
  }

  protected virtual void _(
    PX.Data.Events.RowSelected<DescriptionGenerationRule> eventArgs)
  {
    this.GenerationRuleRowSelected(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<DescriptionGenerationRule>>) eventArgs).Cache, (INMatrixGenerationRule) eventArgs.Row);
  }

  protected virtual void GenerationRuleRowSelected(PXCache cache, INMatrixGenerationRule row)
  {
    if (row == null)
      return;
    bool isAttribute = EnumerableExtensions.IsIn<string>(row.SegmentType, "AC", "AV");
    bool isConstant = row.SegmentType == "CO";
    bool isAutonumber = row.SegmentType == "AN";
    PXCacheEx.Adjust<PXUIFieldAttribute>(cache, (object) row).For<INMatrixGenerationRule.attributeID>((Action<PXUIFieldAttribute>) (a => a.Enabled = isAttribute)).For<INMatrixGenerationRule.constant>((Action<PXUIFieldAttribute>) (a => a.Enabled = isConstant)).For<INMatrixGenerationRule.numberingID>((Action<PXUIFieldAttribute>) (a => a.Enabled = isAutonumber)).For<INMatrixGenerationRule.separator>((Action<PXUIFieldAttribute>) (a => a.Enabled = !row.UseSpaceAsSeparator.GetValueOrDefault()));
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<IDGenerationRule, INMatrixGenerationRule.segmentType> eventArgs)
  {
    this.GenerationRuleSegmentUpdated(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<IDGenerationRule, INMatrixGenerationRule.segmentType>>) eventArgs).Cache, (INMatrixGenerationRule) eventArgs.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<DescriptionGenerationRule, INMatrixGenerationRule.segmentType> eventArgs)
  {
    this.GenerationRuleSegmentUpdated(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<DescriptionGenerationRule, INMatrixGenerationRule.segmentType>>) eventArgs).Cache, (INMatrixGenerationRule) eventArgs.Row);
  }

  protected virtual void GenerationRuleSegmentUpdated(PXCache cache, INMatrixGenerationRule row)
  {
    if (row == null)
      return;
    string segmentType = row.SegmentType;
    if (segmentType != null && segmentType.Length == 2)
    {
      switch (segmentType[1])
      {
        case 'C':
          if (segmentType == "AC")
            break;
          goto label_17;
        case 'D':
          if (segmentType == "TD")
          {
            row.NumberOfCharacters = this.GetTemplate()?.Descr?.Length;
            row.Constant = (string) null;
            row.NumberingID = (string) null;
            row.AttributeID = (string) null;
            return;
          }
          goto label_17;
        case 'I':
          if (segmentType == "TI")
          {
            row.NumberOfCharacters = this.GetTemplate()?.InventoryCD?.Trim().Length;
            row.Constant = (string) null;
            row.NumberingID = (string) null;
            row.AttributeID = (string) null;
            return;
          }
          goto label_17;
        case 'N':
          if (segmentType == "AN")
          {
            row.Constant = (string) null;
            row.AttributeID = (string) null;
            return;
          }
          goto label_17;
        case 'O':
          if (segmentType == "CO")
          {
            row.AttributeID = (string) null;
            row.NumberingID = (string) null;
            return;
          }
          goto label_17;
        case 'P':
          if (segmentType == "SP")
          {
            row.NumberOfCharacters = new int?(1);
            row.Constant = (string) null;
            row.NumberingID = (string) null;
            row.AttributeID = (string) null;
            return;
          }
          goto label_17;
        case 'V':
          if (segmentType == "AV")
            break;
          goto label_17;
        default:
          goto label_17;
      }
      row.Constant = (string) null;
      row.NumberingID = (string) null;
      this.GenerationRuleAttributeUpdated(cache, row);
      return;
    }
label_17:
    row.Constant = (string) null;
    row.NumberingID = (string) null;
    row.AttributeID = (string) null;
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<IDGenerationRule, IDGenerationRule.attributeID> eventArgs)
  {
    this.GenerationRuleAttributeUpdated(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<IDGenerationRule, IDGenerationRule.attributeID>>) eventArgs).Cache, (INMatrixGenerationRule) eventArgs.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<DescriptionGenerationRule, IDGenerationRule.attributeID> eventArgs)
  {
    this.GenerationRuleAttributeUpdated(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<DescriptionGenerationRule, IDGenerationRule.attributeID>>) eventArgs).Cache, (INMatrixGenerationRule) eventArgs.Row);
  }

  protected virtual void GenerationRuleAttributeUpdated(PXCache cache, INMatrixGenerationRule row)
  {
    if (row == null || row.AttributeID == null)
      return;
    CRAttribute.Attribute attribute = ((KList<string, CRAttribute.Attribute>) CRAttribute.Attributes)[row.AttributeID];
    int? nullable = new int?(0);
    int? parentItemClassId = this.GetTemplate().ParentItemClassID;
    ref int? local = ref parentItemClassId;
    string str = local.HasValue ? local.GetValueOrDefault().ToString() : (string) null;
    nullable = !(((PXSelectBase<CSAttributeGroup>) new FbqlSelect<SelectFromBase<CSAttributeGroup, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<CSAttributeGroup.attributeID, Equal<P.AsString>>>>, And<BqlOperand<CSAttributeGroup.entityClassID, IBqlString>.IsEqual<P.AsString>>>>.And<BqlOperand<CSAttributeGroup.entityType, IBqlString>.IsEqual<P.AsString>>>, CSAttributeGroup>.View((PXGraph) this.Base)).SelectSingle(new object[3]
    {
      (object) row.AttributeID,
      (object) str,
      (object) typeof (InventoryItem).FullName
    })?.AttributeCategory == "A") ? (row.SegmentType == "AV" ? attribute.Values.Max<CRAttribute.AttributeValue>((Func<CRAttribute.AttributeValue, int?>) (v => v.ValueID?.Length)) : attribute.Values.Max<CRAttribute.AttributeValue>((Func<CRAttribute.AttributeValue, int?>) (v => v.Description?.Length))) : this.GetAttributeLength(row);
    row.NumberOfCharacters = nullable;
  }

  protected virtual int? GetAttributeLength(INMatrixGenerationRule row) => new int?(0);

  protected virtual void _(
    PX.Data.Events.FieldUpdated<IDGenerationRule, INMatrixGenerationRule.constant> eventArgs)
  {
    this.GenerationRuleConstantUpdated(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<IDGenerationRule, INMatrixGenerationRule.constant>>) eventArgs).Cache, (INMatrixGenerationRule) eventArgs.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<DescriptionGenerationRule, INMatrixGenerationRule.constant> eventArgs)
  {
    this.GenerationRuleConstantUpdated(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<DescriptionGenerationRule, INMatrixGenerationRule.constant>>) eventArgs).Cache, (INMatrixGenerationRule) eventArgs.Row);
  }

  protected virtual void GenerationRuleConstantUpdated(PXCache cache, INMatrixGenerationRule row)
  {
    row.NumberOfCharacters = row?.Constant?.Length;
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<IDGenerationRule, INMatrixGenerationRule.numberingID> eventArgs)
  {
    this.GenerationRuleNumberingUpdated(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<IDGenerationRule, INMatrixGenerationRule.numberingID>>) eventArgs).Cache, (INMatrixGenerationRule) eventArgs.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<DescriptionGenerationRule, INMatrixGenerationRule.numberingID> eventArgs)
  {
    this.GenerationRuleNumberingUpdated(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<DescriptionGenerationRule, INMatrixGenerationRule.numberingID>>) eventArgs).Cache, (INMatrixGenerationRule) eventArgs.Row);
  }

  protected virtual void GenerationRuleNumberingUpdated(PXCache cache, INMatrixGenerationRule row)
  {
    if (row == null || row.NumberingID == null)
      return;
    row.NumberOfCharacters = ((IEnumerable<NumberingSequence>) ((PXSelectBase<NumberingSequence>) new PXSelect<NumberingSequence, Where<NumberingSequence.numberingID, Equal<Required<PX.Objects.CS.Numbering.numberingID>>>>((PXGraph) this.Base)).SelectMain(new object[1]
    {
      (object) row.NumberingID
    })).Max<NumberingSequence>((Func<NumberingSequence, int?>) (s => s.EndNbr?.Length));
  }

  protected virtual void ResetValue<Field>() where Field : IBqlField
  {
    ((PXCache) GraphHelper.Caches<TParent>((PXGraph) this.Base)).SetValue<Field>(((PXCache) GraphHelper.Caches<TParent>((PXGraph) this.Base)).Current, (object) null);
  }

  protected virtual string GetGenerationRuleSample<Field, Rule>(
    PXSelectBase<Rule> view,
    string userMessage)
    where Field : IBqlField
    where Rule : INMatrixGenerationRule, new()
  {
    PXCache<TParent> pxCache = GraphHelper.Caches<TParent>((PXGraph) this.Base);
    object current = ((PXCache) pxCache).Current;
    if (current == null)
      return (string) null;
    string generationRuleSample1 = (string) ((PXCache) pxCache).GetValue<Field>(current);
    if (generationRuleSample1 != null)
      return generationRuleSample1;
    string generationRuleSample2 = this.GetGenerationRuleSample((IEnumerable<INMatrixGenerationRule>) view.SelectMain(Array.Empty<object>()));
    string generationRuleSample3 = PXLocalizer.LocalizeFormat(userMessage, new object[1]
    {
      (object) generationRuleSample2
    });
    ((PXCache) pxCache).SetValue<Field>(current, (object) generationRuleSample3);
    ((PXSelectBase) view).View.RequestRefresh();
    return generationRuleSample3;
  }

  protected virtual string GetGenerationRuleSample(IEnumerable<INMatrixGenerationRule> rules)
  {
    try
    {
      CreateMatrixItemsHelper matrixItemsHelper = this.Base.GetCreateMatrixItemsHelper();
      PX.Objects.IN.Matrix.DAC.Unbound.MatrixInventoryItem newItem = new PX.Objects.IN.Matrix.DAC.Unbound.MatrixInventoryItem()
      {
        AttributeIDs = this.GetAttributeIDs()
      };
      newItem.AttributeValues = new string[newItem.AttributeIDs.Length];
      newItem.AttributeValueDescrs = new string[newItem.AttributeIDs.Length];
      for (int index = 0; index < newItem.AttributeIDs.Length; ++index)
      {
        CRAttribute.AttributeValue attributeValue = ((KList<string, CRAttribute.Attribute>) CRAttribute.Attributes)[newItem.AttributeIDs[index]].Values.Where<CRAttribute.AttributeValue>((Func<CRAttribute.AttributeValue, bool>) (v => !v.Disabled)).FirstOrDefault<CRAttribute.AttributeValue>();
        newItem.AttributeValues[index] = attributeValue?.ValueID;
        newItem.AttributeValueDescrs[index] = attributeValue?.Description;
      }
      return matrixItemsHelper.GenerateMatrixItemID(this.GetTemplate(), rules.ToList<INMatrixGenerationRule>(), newItem, true);
    }
    catch (Exception ex)
    {
      return ex.Message;
    }
  }

  protected abstract string[] GetAttributeIDs();

  protected abstract InventoryItem GetTemplate();
}
