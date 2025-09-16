// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.Matrix.Utility.AttributeGroupHelper
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Common.Collection;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.Common;
using PX.Objects.Common.Exceptions;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.IN.Matrix.Attributes;
using PX.Objects.IN.Matrix.DAC;
using PX.Objects.IN.Matrix.Graphs;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.IN.Matrix.Utility;

public class AttributeGroupHelper
{
  private const string Separator = "; ";
  protected PXGraph _graph;
  protected TemplateInventoryItemMaint _templateInventoryItemMaint;
  protected InventoryItem _template;
  protected Dictionary<string, AttributeGroupHelper.Attribute> _templateAttributes;
  protected Dictionary<string[], int> _combinations;
  protected int _numberOfCombination;
  protected int? _lastInventoryID;
  protected string[] _attributeValues;
  protected string _columnAttributeValue;
  protected string _rowAttributeValue;

  public AttributeGroupHelper(PXGraph graph)
  {
    this._graph = graph;
    this._templateInventoryItemMaint = graph as TemplateInventoryItemMaint;
  }

  public virtual void Recalculate(InventoryItem template)
  {
    if (template == null)
      throw new PXArgumentException(nameof (template));
    if (!template.InventoryID.HasValue)
      throw new PXArgumentException("InventoryID");
    this._template = template;
    this.DeleteOldRows();
    this.GetAttributes();
    this._combinations = new Dictionary<string[], int>((IEqualityComparer<string[]>) new AttributeGroupHelper.StringArrayComparer());
    this._numberOfCombination = 0;
    this._lastInventoryID = new int?();
    this._attributeValues = new string[this._templateAttributes.Keys.Count];
    this._columnAttributeValue = (string) null;
    this._rowAttributeValue = (string) null;
    PXSelectReadonly2<CSAnswers, InnerJoin<InventoryItem, On<CSAnswers.refNoteID, Equal<InventoryItem.noteID>>, LeftJoin<CSAttributeGroup, On<CSAnswers.attributeID, Equal<CSAttributeGroup.attributeID>, And<CSAttributeGroup.entityClassID, Equal<InventoryItem.itemClassID>, And<CSAttributeGroup.entityType, Equal<Constants.DACName<InventoryItem>>, And<CSAttributeGroup.attributeCategory, Equal<CSAttributeGroup.attributeCategory.variant>>>>>>>, Where<InventoryItem.templateItemID, Equal<Required<InventoryItem.inventoryID>>, And2<Where<CSAnswers.attributeID, Equal<MatrixAttributeSelectorAttribute.dummyAttributeName>, Or<CSAttributeGroup.isActive, Equal<True>>>, And<NotExists<Select<CSAnswers, Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<CSAnswers.isActive, Equal<False>>>>, And<BqlOperand<CSAnswers.attributeID, IBqlString>.IsEqual<CSAttributeGroup.attributeID>>>>.And<BqlOperand<CSAnswers.refNoteID, IBqlGuid>.IsEqual<InventoryItem.noteID>>>>>>>>, OrderBy<Asc<InventoryItem.inventoryID, Asc<CSAttributeGroup.sortOrder, Asc<CSAttributeGroup.attributeID>>>>> pxSelectReadonly2 = new PXSelectReadonly2<CSAnswers, InnerJoin<InventoryItem, On<CSAnswers.refNoteID, Equal<InventoryItem.noteID>>, LeftJoin<CSAttributeGroup, On<CSAnswers.attributeID, Equal<CSAttributeGroup.attributeID>, And<CSAttributeGroup.entityClassID, Equal<InventoryItem.itemClassID>, And<CSAttributeGroup.entityType, Equal<Constants.DACName<InventoryItem>>, And<CSAttributeGroup.attributeCategory, Equal<CSAttributeGroup.attributeCategory.variant>>>>>>>, Where<InventoryItem.templateItemID, Equal<Required<InventoryItem.inventoryID>>, And2<Where<CSAnswers.attributeID, Equal<MatrixAttributeSelectorAttribute.dummyAttributeName>, Or<CSAttributeGroup.isActive, Equal<True>>>, And<NotExists<Select<CSAnswers, Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<CSAnswers.isActive, Equal<False>>>>, And<BqlOperand<CSAnswers.attributeID, IBqlString>.IsEqual<CSAttributeGroup.attributeID>>>>.And<BqlOperand<CSAnswers.refNoteID, IBqlGuid>.IsEqual<InventoryItem.noteID>>>>>>>>, OrderBy<Asc<InventoryItem.inventoryID, Asc<CSAttributeGroup.sortOrder, Asc<CSAttributeGroup.attributeID>>>>>(this._graph);
    int attributesAreFilled = 0;
    using (new PXFieldScope(((PXSelectBase) pxSelectReadonly2).View, new System.Type[3]
    {
      typeof (InventoryItem.inventoryID),
      typeof (CSAnswers.attributeID),
      typeof (CSAnswers.value)
    }))
    {
      foreach (PXResult<CSAnswers, InventoryItem> pxResult in ((PXSelectBase<CSAnswers>) pxSelectReadonly2).Select(new object[1]
      {
        (object) this._template.InventoryID
      }))
        this.RecalculateItem(PXResult<CSAnswers, InventoryItem>.op_Implicit(pxResult), PXResult<CSAnswers, InventoryItem>.op_Implicit(pxResult), ref attributesAreFilled);
    }
  }

  protected virtual void RecalculateItem(
    InventoryItem inventoryItem,
    CSAnswers answer,
    ref int attributesAreFilled)
  {
    int? lastInventoryId = this._lastInventoryID;
    int? inventoryId = inventoryItem.InventoryID;
    if (!(lastInventoryId.GetValueOrDefault() == inventoryId.GetValueOrDefault() & lastInventoryId.HasValue == inventoryId.HasValue))
    {
      this._lastInventoryID = inventoryItem.InventoryID;
      this._attributeValues = new string[this._templateAttributes.Keys.Count];
      attributesAreFilled = 0;
    }
    if (string.Equals(answer.AttributeID, this._template.DefaultColumnMatrixAttributeID, StringComparison.OrdinalIgnoreCase))
      this._columnAttributeValue = answer.Value;
    else if (string.Equals(answer.AttributeID, this._template.DefaultRowMatrixAttributeID, StringComparison.OrdinalIgnoreCase))
    {
      this._rowAttributeValue = answer.Value;
    }
    else
    {
      AttributeGroupHelper.Attribute attribute;
      if (this._templateAttributes.TryGetValue(answer.AttributeID, out attribute) && this._attributeValues[attribute.AttributeIndex] == null)
      {
        ++attributesAreFilled;
        this._attributeValues[attribute.AttributeIndex] = answer.Value;
      }
    }
    if (attributesAreFilled != this._templateAttributes.Keys.Count || this._columnAttributeValue == null || this._rowAttributeValue == null)
      return;
    int combinationNumber;
    if (this._combinations.TryGetValue(this._attributeValues, out combinationNumber))
    {
      this.SetInventoryCombinationNumber(inventoryItem.InventoryID, combinationNumber);
    }
    else
    {
      this.SetInventoryCombinationNumber(inventoryItem.InventoryID, this._numberOfCombination);
      this._combinations.Add(this._attributeValues, this._numberOfCombination);
      this.OnNewCombination();
      ++this._numberOfCombination;
    }
    this._lastInventoryID = new int?();
  }

  protected virtual void DeleteOldRows()
  {
    PXSelect<INAttributeDescriptionGroup, Where<INAttributeDescriptionGroup.templateID, Equal<Required<InventoryItem.inventoryID>>>> groupSelect = new PXSelect<INAttributeDescriptionGroup, Where<INAttributeDescriptionGroup.templateID, Equal<Required<InventoryItem.inventoryID>>>>(this._graph);
    EnumerableExtensions.ForEach<INAttributeDescriptionGroup>((IEnumerable<INAttributeDescriptionGroup>) ((PXSelectBase<INAttributeDescriptionGroup>) groupSelect).SelectMain(new object[1]
    {
      (object) this._template.InventoryID
    }), (Action<INAttributeDescriptionGroup>) (r => ((PXSelectBase) groupSelect).Cache.Delete((object) r)));
    PXSelect<INAttributeDescriptionItem, Where<INAttributeDescriptionItem.templateID, Equal<Required<InventoryItem.inventoryID>>>> itemSelect = new PXSelect<INAttributeDescriptionItem, Where<INAttributeDescriptionItem.templateID, Equal<Required<InventoryItem.inventoryID>>>>(this._graph);
    EnumerableExtensions.ForEach<INAttributeDescriptionItem>((IEnumerable<INAttributeDescriptionItem>) ((PXSelectBase<INAttributeDescriptionItem>) itemSelect).SelectMain(new object[1]
    {
      (object) this._template.InventoryID
    }), (Action<INAttributeDescriptionItem>) (r => ((PXSelectBase) itemSelect).Cache.Delete((object) r)));
  }

  protected virtual void GetAttributes()
  {
    CSAttributeGroup[] csAttributeGroupArray = ((PXSelectBase<CSAttributeGroup>) new PXSelectReadonly2<CSAttributeGroup, InnerJoin<InventoryItem, On<CSAttributeGroup.entityClassID, Equal<InventoryItem.itemClassID>>>, Where<CSAttributeGroup.isActive, Equal<True>, And<CSAttributeGroup.entityType, Equal<Constants.DACName<InventoryItem>>, And<CSAttributeGroup.attributeCategory, Equal<CSAttributeGroup.attributeCategory.variant>, And<InventoryItem.inventoryID, Equal<Required<InventoryItem.inventoryID>>, And<CSAttributeGroup.attributeID, NotIn3<Required<InventoryItem.defaultColumnMatrixAttributeID>, Required<InventoryItem.defaultRowMatrixAttributeID>>, And<NotExists<Select<CSAnswers, Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<CSAnswers.isActive, Equal<False>>>>, And<BqlOperand<CSAnswers.attributeID, IBqlString>.IsEqual<CSAttributeGroup.attributeID>>>>.And<BqlOperand<CSAnswers.refNoteID, IBqlGuid>.IsEqual<InventoryItem.noteID>>>>>>>>>>>, OrderBy<Asc<CSAttributeGroup.sortOrder, Asc<CSAttributeGroup.attributeID>>>>(this._graph)).SelectMain(new object[3]
    {
      (object) this._template.InventoryID,
      (object) this._template.DefaultColumnMatrixAttributeID,
      (object) this._template.DefaultRowMatrixAttributeID
    });
    this._templateAttributes = new Dictionary<string, AttributeGroupHelper.Attribute>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    for (int index = 0; index < csAttributeGroupArray.Length; ++index)
      this._templateAttributes.Add(csAttributeGroupArray[index].AttributeID, new AttributeGroupHelper.Attribute()
      {
        AttributeIndex = index
      });
  }

  protected virtual void SetInventoryCombinationNumber(int? inventoryID, int combinationNumber)
  {
    PXCache<InventoryItem> cache = GraphHelper.Caches<InventoryItem>(this._graph);
    InventoryItem dirty = InventoryItem.PK.FindDirty(this._graph, inventoryID);
    if (dirty == null)
      throw new RowNotFoundException((PXCache) cache, new object[1]
      {
        (object) inventoryID
      });
    dirty.AttributeDescriptionGroupID = new int?(combinationNumber);
    dirty.ColumnAttributeValue = this._columnAttributeValue;
    dirty.RowAttributeValue = this._rowAttributeValue;
    InventoryItem inventoryItem;
    if (this._templateInventoryItemMaint != null)
      inventoryItem = this._templateInventoryItemMaint.UpdateChild(dirty);
    else
      inventoryItem = cache.Update(dirty);
  }

  protected virtual void OnNewCombination()
  {
    string[] array = this._templateAttributes.Keys.ToArray<string>();
    if (array == null)
      throw new PXArgumentException("attributeIdentifiers");
    if (this._attributeValues == null)
      throw new PXArgumentException("_attributeValues");
    if (this._attributeValues.Length != array.Length)
      throw new PXArgumentException("_attributeValues.Length");
    PXCache pxCache1 = (PXCache) GraphHelper.Caches<INAttributeDescriptionItem>(this._graph);
    List<string> values = new List<string>();
    for (int index = 0; index < array.Length; ++index)
    {
      INAttributeDescriptionItem newItem = (INAttributeDescriptionItem) pxCache1.CreateInstance();
      newItem.TemplateID = this._template.InventoryID;
      newItem.GroupID = new int?(this._numberOfCombination);
      newItem.AttributeID = array[index];
      newItem.ValueID = this._attributeValues[index];
      newItem = (INAttributeDescriptionItem) pxCache1.Insert((object) newItem);
      string description = ((KList<string, CRAttribute.Attribute>) CRAttribute.Attributes)[newItem.AttributeID].Values.Where<CRAttribute.AttributeValue>((Func<CRAttribute.AttributeValue, bool>) (v => v.ValueID == newItem.ValueID)).FirstOrDefault<CRAttribute.AttributeValue>()?.Description;
      values.Add(description);
    }
    PXCache<INAttributeDescriptionGroup> pxCache2 = GraphHelper.Caches<INAttributeDescriptionGroup>(this._graph);
    INAttributeDescriptionGroup instance = (INAttributeDescriptionGroup) ((PXCache) pxCache2).CreateInstance();
    instance.TemplateID = this._template.InventoryID;
    instance.GroupID = new int?(this._numberOfCombination);
    instance.Description = string.Join("; ", (IEnumerable<string>) values);
    INAttributeDescriptionGroup descriptionGroup = (INAttributeDescriptionGroup) ((PXCache) pxCache2).Insert((object) instance);
  }

  public virtual void OnNewItem(
    InventoryItem templateItem,
    InventoryItem newItem,
    string[] newItemAttributeIDs,
    string[] newItemAttributeValues)
  {
    if (templateItem == null)
      throw new PXArgumentException(nameof (templateItem));
    if (newItem == null)
      throw new PXArgumentException(nameof (newItem));
    if (newItemAttributeIDs == null)
      throw new PXArgumentException(nameof (newItemAttributeIDs));
    if (newItemAttributeValues == null)
      throw new PXArgumentException(nameof (newItemAttributeValues));
    if (newItemAttributeIDs.Length != newItemAttributeValues.Length)
      throw new PXArgumentException("newItemAttributeValues.Length");
    if (templateItem != this._template)
    {
      this._template = templateItem;
      this.DeleteOldCombinationsIfAllChildrenDeleted();
      this.GetAttributes();
      this.GetCombinations();
    }
    this._attributeValues = new string[this._templateAttributes.Keys.Count];
    this._columnAttributeValue = this._template.DefaultColumnMatrixAttributeID == "~MX~DUMMY~" ? "Value" : (string) null;
    this._rowAttributeValue = this._template.DefaultRowMatrixAttributeID == "~MX~DUMMY~" ? "Value" : (string) null;
    int num1 = 0;
    for (int index = 0; index < newItemAttributeIDs.Length; ++index)
    {
      if (newItemAttributeIDs[index] == this._template.DefaultColumnMatrixAttributeID)
        this._columnAttributeValue = newItemAttributeValues[index];
      else if (newItemAttributeIDs[index] == this._template.DefaultRowMatrixAttributeID)
      {
        this._rowAttributeValue = newItemAttributeValues[index];
      }
      else
      {
        AttributeGroupHelper.Attribute attribute;
        if (this._templateAttributes.TryGetValue(newItemAttributeIDs[index], out attribute) && this._attributeValues[attribute.AttributeIndex] == null)
        {
          ++num1;
          this._attributeValues[attribute.AttributeIndex] = newItemAttributeValues[index];
        }
      }
    }
    if (num1 != this._templateAttributes.Keys.Count)
      return;
    int num2;
    if (this._combinations.TryGetValue(this._attributeValues, out num2))
    {
      newItem.AttributeDescriptionGroupID = new int?(num2);
      newItem.ColumnAttributeValue = this._columnAttributeValue;
      newItem.RowAttributeValue = this._rowAttributeValue;
    }
    else
    {
      newItem.AttributeDescriptionGroupID = new int?(this._numberOfCombination);
      newItem.ColumnAttributeValue = this._columnAttributeValue;
      newItem.RowAttributeValue = this._rowAttributeValue;
      this._combinations.Add(this._attributeValues, this._numberOfCombination);
      this.OnNewCombination();
      ++this._numberOfCombination;
    }
  }

  protected virtual void GetCombinations()
  {
    int? nullable1 = new int?();
    this._attributeValues = new string[this._templateAttributes.Keys.Count];
    this._combinations = new Dictionary<string[], int>((IEqualityComparer<string[]>) new AttributeGroupHelper.StringArrayComparer());
    this._numberOfCombination = 0;
    int num = 0;
    if (this._templateAttributes.Keys.Count == 0)
    {
      INAttributeDescriptionGroup descriptionGroup = PXResultset<INAttributeDescriptionGroup>.op_Implicit(((PXSelectBase<INAttributeDescriptionGroup>) new PXSelect<INAttributeDescriptionGroup, Where<INAttributeDescriptionGroup.templateID, Equal<Required<InventoryItem.inventoryID>>>>(this._graph)).Select(new object[1]
      {
        (object) this._template.InventoryID
      }));
      if (descriptionGroup == null)
        return;
      this._combinations.Add(this._attributeValues, descriptionGroup.GroupID.Value);
      this._numberOfCombination = descriptionGroup.GroupID.Value + 1;
    }
    else
    {
      PXSelect<INAttributeDescriptionItem, Where<INAttributeDescriptionItem.templateID, Equal<Required<InventoryItem.inventoryID>>>> pxSelect = new PXSelect<INAttributeDescriptionItem, Where<INAttributeDescriptionItem.templateID, Equal<Required<InventoryItem.inventoryID>>>>(this._graph);
      object[] objArray = new object[1]
      {
        (object) this._template.InventoryID
      };
      foreach (INAttributeDescriptionItem attributeDescriptionItem in ((PXSelectBase<INAttributeDescriptionItem>) pxSelect).SelectMain(objArray))
      {
        int? nullable2 = nullable1;
        int? groupId = attributeDescriptionItem.GroupID;
        if (!(nullable2.GetValueOrDefault() == groupId.GetValueOrDefault() & nullable2.HasValue == groupId.HasValue))
        {
          nullable1 = attributeDescriptionItem.GroupID;
          this._attributeValues = new string[this._templateAttributes.Keys.Count];
          num = 0;
        }
        AttributeGroupHelper.Attribute attribute;
        if (this._templateAttributes.TryGetValue(attributeDescriptionItem.AttributeID, out attribute) && this._attributeValues[attribute.AttributeIndex] == null)
        {
          ++num;
          this._attributeValues[attribute.AttributeIndex] = attributeDescriptionItem.ValueID;
        }
        if (num == this._templateAttributes.Keys.Count)
        {
          if (!this._combinations.TryGetValue(this._attributeValues, out int _))
          {
            this._combinations.Add(this._attributeValues, attributeDescriptionItem.GroupID.Value);
            this._numberOfCombination = Math.Max(this._numberOfCombination, attributeDescriptionItem.GroupID.Value + 1);
          }
          nullable1 = new int?();
        }
      }
    }
  }

  protected virtual void DeleteOldCombinationsIfAllChildrenDeleted()
  {
    if (PXResultset<InventoryItem>.op_Implicit(PXSelectBase<InventoryItem, PXSelectReadonly<InventoryItem, Where<InventoryItem.templateItemID, Equal<Required<InventoryItem.templateItemID>>>>.Config>.Select(this._graph, new object[1]
    {
      (object) this._template.InventoryID
    })) != null)
      return;
    this.DeleteOldRows();
  }

  protected class Attribute
  {
    public int AttributeIndex { get; set; }
  }

  protected class StringArrayComparer : IEqualityComparer<string[]>
  {
    public bool Equals(string[] x, string[] y)
    {
      if (x == y)
        return true;
      return x != null && y != null && ((IEnumerable<string>) x).SequenceEqual<string>((IEnumerable<string>) y);
    }

    public int GetHashCode(string[] list)
    {
      return list == null ? 0 : string.Join(string.Empty, list).GetHashCode();
    }
  }
}
