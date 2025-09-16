// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.GraphExtensions.INItemClassMaintExt.ItemClassGenerationRulesExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common.Collection;
using PX.Data;
using PX.Objects.Common;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.IN.Matrix.DAC;
using PX.Objects.IN.Matrix.DAC.Projections;
using PX.Objects.IN.Matrix.GraphExtensions;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.IN.GraphExtensions.INItemClassMaintExt;

public class ItemClassGenerationRulesExt : 
  GenerationRulesExt<INItemClassMaint, INItemClass, INItemClass.itemClassID, INMatrixGenerationRule.parentType.itemClass, INItemClass.sampleID, INItemClass.sampleDescription>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.matrixItem>();

  protected override InventoryItem GetTemplate()
  {
    INItemClass current = ((PXSelectBase<INItemClass>) this.Base.itemclasssettings).Current;
    return new InventoryItem()
    {
      InventoryCD = current?.ItemClassCD,
      Descr = current?.Descr,
      ItemClassID = current.ItemClassID,
      ParentItemClassID = current.ItemClassID
    };
  }

  protected override string[] GetAttributeIDs()
  {
    return ((IEnumerable<CSAttributeGroup>) this.Base.Mapping.SelectMain(Array.Empty<object>())).Select<CSAttributeGroup, string>((Func<CSAttributeGroup, string>) (a => a.AttributeID)).ToArray<string>();
  }

  protected virtual void VerifyAttributes()
  {
    this.VerifyAttribute<INItemClass.defaultColumnMatrixAttributeID>();
    this.VerifyAttribute<INItemClass.defaultRowMatrixAttributeID>();
    this.VerifyRuleAttributes(((PXSelectBase) this.DescriptionGenerationRules).Cache, (IEnumerable<INMatrixGenerationRule>) ((PXSelectBase<DescriptionGenerationRule>) this.DescriptionGenerationRules).SelectMain(Array.Empty<object>()));
    this.VerifyRuleAttributes(((PXSelectBase) this.IDGenerationRules).Cache, (IEnumerable<INMatrixGenerationRule>) ((PXSelectBase<IDGenerationRule>) this.IDGenerationRules).SelectMain(Array.Empty<object>()));
  }

  protected virtual void VerifyAttribute<Field>() where Field : IBqlField
  {
    object obj = ((PXSelectBase) this.Base.itemclasssettings).Cache.GetValue<Field>((object) ((PXSelectBase<INItemClass>) this.Base.itemclasssettings).Current);
    try
    {
      ((PXSelectBase) this.Base.itemclasssettings).Cache.RaiseFieldVerifying<Field>((object) ((PXSelectBase<INItemClass>) this.Base.itemclasssettings).Current, ref obj);
    }
    catch (PXSetPropertyException ex)
    {
      ((PXSelectBase) this.Base.itemclasssettings).Cache.RaiseExceptionHandling<Field>((object) ((PXSelectBase<INItemClass>) this.Base.itemclasssettings).Current, obj, (Exception) ex);
    }
  }

  protected virtual void VerifyRuleAttributes(
    PXCache cache,
    IEnumerable<INMatrixGenerationRule> items)
  {
    foreach (INMatrixGenerationRule matrixGenerationRule in items)
    {
      object attributeId = (object) matrixGenerationRule.AttributeID;
      if (attributeId != null)
      {
        try
        {
          cache.RaiseFieldVerifying<DescriptionGenerationRule.attributeID>((object) matrixGenerationRule, ref attributeId);
        }
        catch (PXSetPropertyException ex)
        {
          cache.RaiseExceptionHandling<DescriptionGenerationRule.attributeID>((object) matrixGenerationRule, attributeId, (Exception) ex);
        }
      }
    }
  }

  protected override int? GetAttributeLength(INMatrixGenerationRule row)
  {
    CRAttribute.Attribute attribute = ((KList<string, CRAttribute.Attribute>) CRAttribute.Attributes)[row.AttributeID];
    int? controlType = attribute.ControlType;
    if (controlType.HasValue)
    {
      switch (controlType.GetValueOrDefault())
      {
        case 2:
          return !(row.SegmentType == "AV") ? attribute.Values.Max<CRAttribute.AttributeValue>((Func<CRAttribute.AttributeValue, int?>) (v => v.Description?.Length)) : attribute.Values.Max<CRAttribute.AttributeValue>((Func<CRAttribute.AttributeValue, int?>) (v => v.ValueID?.Length));
        case 4:
          return new int?(1);
      }
    }
    return base.GetAttributeLength(row);
  }

  [PXMergeAttributes]
  [PXDefault("C")]
  protected virtual void _(
    PX.Data.Events.CacheAttached<IDGenerationRule.parentType> eventArgs)
  {
  }

  [PXMergeAttributes]
  [PXDefault("C")]
  protected virtual void _(
    PX.Data.Events.CacheAttached<DescriptionGenerationRule.parentType> eventArgs)
  {
  }

  [PXMergeAttributes]
  [PXDBDefault(typeof (INItemClass.itemClassID))]
  [PXParent(typeof (IDGenerationRule.FK.ItemClass))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<IDGenerationRule.parentID> eventArgs)
  {
  }

  [PXMergeAttributes]
  [PXDBDefault(typeof (INItemClass.itemClassID))]
  [PXParent(typeof (DescriptionGenerationRule.FK.ItemClass))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<DescriptionGenerationRule.parentID> eventArgs)
  {
  }

  [PXMergeAttributes]
  [PXLineNbr(typeof (INItemClass.generationRuleCntr))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<IDGenerationRule.lineNbr> eventArgs)
  {
  }

  [PXMergeAttributes]
  [PXLineNbr(typeof (INItemClass.generationRuleCntr))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<DescriptionGenerationRule.lineNbr> eventArgs)
  {
  }

  [PXMergeAttributes]
  [PXSelector(typeof (Search<CSAttributeGroup.attributeID, Where<CSAttributeGroup.entityClassID, Equal<RTrim<Current<INItemClass.itemClassID>>>, And<CSAttributeGroup.entityType, Equal<Constants.DACName<InventoryItem>>>>>), new System.Type[] {typeof (CSAttributeGroup.attributeID), typeof (CSAttributeGroup.description)}, DirtyRead = true)]
  protected virtual void _(
    PX.Data.Events.CacheAttached<IDGenerationRule.attributeID> eventArgs)
  {
  }

  [PXMergeAttributes]
  [PXSelector(typeof (Search<CSAttributeGroup.attributeID, Where<CSAttributeGroup.entityClassID, Equal<RTrim<Current<INItemClass.itemClassID>>>, And<CSAttributeGroup.entityType, Equal<Constants.DACName<InventoryItem>>>>>), new System.Type[] {typeof (CSAttributeGroup.attributeID), typeof (CSAttributeGroup.description)}, DirtyRead = true)]
  protected virtual void _(
    PX.Data.Events.CacheAttached<DescriptionGenerationRule.attributeID> eventArgs)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdated<CSAttributeGroup> eventArgs)
  {
    if (((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<CSAttributeGroup>>) eventArgs).Cache.ObjectsEqual<CSAttributeGroup.attributeCategory, CSAttributeGroup.attributeID, CSAttributeGroup.isActive>((object) eventArgs.Row, (object) eventArgs.OldRow))
      return;
    this.VerifyAttributes();
  }

  protected virtual void _(PX.Data.Events.RowDeleted<CSAttributeGroup> eventArgs)
  {
    if (!(eventArgs.Row?.AttributeCategory == "V"))
      return;
    this.VerifyAttributes();
  }

  protected virtual void _(PX.Data.Events.RowPersisting<INItemClass> eventArgs)
  {
    this.VerifyAttributes();
  }
}
