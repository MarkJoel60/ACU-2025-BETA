// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.Matrix.Utility.CreateMatrixItemsHelper
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Common.Collection;
using PX.Data;
using PX.Data.BQL.Fluent;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.Common;
using PX.Objects.Common.Extensions;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.IN.Matrix.Attributes;
using PX.Objects.IN.Matrix.DAC;
using PX.Objects.IN.Matrix.DAC.Projections;
using PX.Objects.IN.Matrix.DAC.Unbound;
using PX.Objects.PO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#nullable disable
namespace PX.Objects.IN.Matrix.Utility;

public class CreateMatrixItemsHelper
{
  protected const char MultiComboAttributeSeparator = ',';
  protected PXGraph _graph;

  public CreateMatrixItemsHelper(PXGraph graph) => this._graph = graph;

  public virtual MatrixInventoryItem CreateMatrixItemFromTemplate(
    EntryMatrix row,
    int attributeNumber,
    PX.Objects.IN.InventoryItem template,
    List<INMatrixGenerationRule> idGenRules,
    List<INMatrixGenerationRule> descrGenRules)
  {
    int? valueFromArray1 = CreateMatrixItemsHelper.GetValueFromArray<int?>(row?.InventoryIDs, attributeNumber);
    bool? valueFromArray2 = CreateMatrixItemsHelper.GetValueFromArray<bool?>(row?.Selected, attributeNumber);
    if (valueFromArray1.HasValue || !valueFromArray2.GetValueOrDefault())
      return (MatrixInventoryItem) null;
    EntryHeader current1 = (EntryHeader) this._graph.Caches[typeof (EntryHeader)].Current;
    AdditionalAttributes current2 = (AdditionalAttributes) this._graph.Caches[typeof (AdditionalAttributes)].Current;
    PXCache matrixCache = this._graph.Caches[typeof (MatrixInventoryItem)];
    MatrixInventoryItem newItem = PropertyTransfer.Transfer<PX.Objects.IN.InventoryItem, MatrixInventoryItem>(template, new MatrixInventoryItem());
    InventoryItemCurySettings itemCurySettings = InventoryItemCurySettings.PK.Find(this._graph, template.InventoryID, this._graph.Accessinfo.BaseCuryID);
    newItem.DfltSiteID = (int?) itemCurySettings?.DfltSiteID;
    List<string> stringList1 = new List<string>((IEnumerable<string>) current2?.AttributeIdentifiers);
    stringList1.Add(current1?.RowAttributeID);
    stringList1.Add(current1?.ColAttributeID);
    List<string> stringList2 = new List<string>((IEnumerable<string>) current2?.Values);
    stringList2.Add(row.RowAttributeValue);
    stringList2.Add(CreateMatrixItemsHelper.GetValueFromArray<string>(row?.ColAttributeValues, attributeNumber));
    List<string> stringList3 = new List<string>((IEnumerable<string>) current2?.Descriptions);
    stringList3.Add(row.RowAttributeValueDescr);
    stringList3.Add(CreateMatrixItemsHelper.GetValueFromArray<string>(row?.ColAttributeValueDescrs, attributeNumber));
    newItem.AttributeIDs = stringList1.ToArray();
    newItem.AttributeValues = stringList2.ToArray();
    newItem.AttributeValueDescrs = stringList3.ToArray();
    object matrixItemId1 = (object) this.GenerateMatrixItemID(template, idGenRules, newItem);
    matrixCache.RaiseFieldUpdating<MatrixInventoryItem.inventoryCD>((object) newItem, ref matrixItemId1);
    newItem.InventoryCD = (string) matrixItemId1;
    newItem.InventoryCD = matrixCache.GetFormatedMaskField<MatrixInventoryItem.inventoryCD>((IBqlTable) newItem);
    if (PXDBLocalizableStringAttribute.IsEnabled)
    {
      GraphHelper.Caches<PX.Objects.IN.InventoryItem>(this._graph);
      DBMatrixLocalizableDescriptionAttribute.SetTranslations<MatrixInventoryItem.descr>(matrixCache, (object) newItem, (Func<string, string>) (locale =>
      {
        object matrixItemId2 = (object) this.GenerateMatrixItemID(template, descrGenRules, newItem, locale: locale);
        matrixCache.RaiseFieldUpdating<MatrixInventoryItem.descr>((object) newItem, ref matrixItemId2);
        return (string) matrixItemId2;
      }));
    }
    else
    {
      object matrixItemId3 = (object) this.GenerateMatrixItemID(template, descrGenRules, newItem);
      matrixCache.RaiseFieldUpdating<MatrixInventoryItem.descr>((object) newItem, ref matrixItemId3);
      newItem.Descr = (string) matrixItemId3;
    }
    newItem.Exists = new bool?(PX.Objects.IN.InventoryItem.UK.Find(this._graph, newItem.InventoryCD) != null);
    newItem.Duplicate = new bool?(GraphHelper.RowCast<MatrixInventoryItem>(matrixCache.Cached).Any<MatrixInventoryItem>((Func<MatrixInventoryItem, bool>) (mi => mi.InventoryCD == newItem.InventoryCD)));
    MatrixInventoryItem matrixInventoryItem = newItem;
    bool? nullable1 = newItem.Exists;
    int num;
    if (!nullable1.GetValueOrDefault())
    {
      nullable1 = newItem.Duplicate;
      num = !nullable1.GetValueOrDefault() ? 1 : 0;
    }
    else
      num = 0;
    bool? nullable2 = new bool?(num != 0);
    matrixInventoryItem.Selected = nullable2;
    newItem.IsTemplate = new bool?(false);
    if (PXAccess.FeatureInstalled<FeaturesSet.relatedItemAssistant>())
    {
      INItemClass inItemClass = INItemClass.PK.Find(this._graph, newItem.ItemClassID);
      newItem.PreferredItemClasses = inItemClass.PreferredItemClasses;
      newItem.PriceOfSuggestedItems = inItemClass.PriceOfSuggestedItems;
    }
    return newItem;
  }

  public virtual void GetGenerationRules(
    int? templateItemID,
    out List<INMatrixGenerationRule> idGenerationRules,
    out List<INMatrixGenerationRule> descrGenerationRules)
  {
    List<INMatrixGenerationRule> list = GraphHelper.RowCast<INMatrixGenerationRule>((IEnumerable) PXSelectBase<INMatrixGenerationRule, PXSelectReadonly<INMatrixGenerationRule, Where<INMatrixGenerationRule.parentID, Equal<Required<INMatrixGenerationRule.parentID>>, And<INMatrixGenerationRule.parentType, Equal<INMatrixGenerationRule.parentType.templateItem>>>, OrderBy<Asc<INMatrixGenerationRule.sortOrder>>>.Config>.Select(this._graph, new object[1]
    {
      (object) templateItemID
    })).ToList<INMatrixGenerationRule>();
    idGenerationRules = list.Where<INMatrixGenerationRule>((Func<INMatrixGenerationRule, bool>) (r => r.Type == "I")).ToList<INMatrixGenerationRule>();
    descrGenerationRules = list.Where<INMatrixGenerationRule>((Func<INMatrixGenerationRule, bool>) (r => r.Type == "D")).ToList<INMatrixGenerationRule>();
  }

  public virtual string GenerateMatrixItemID(
    PX.Objects.IN.InventoryItem template,
    List<INMatrixGenerationRule> genRules,
    MatrixInventoryItem newItem,
    bool useLastAutoNumberValue = false,
    string locale = null)
  {
    StringBuilder res = new StringBuilder();
    for (int index = 0; index < genRules.Count; ++index)
    {
      bool isLastSegment = index == genRules.Count - 1;
      this.AppendMatrixItemIDSegment(res, template, genRules[index], isLastSegment, newItem, useLastAutoNumberValue, locale);
    }
    return res.ToString();
  }

  protected virtual void AppendMatrixItemIDSegment(
    StringBuilder res,
    PX.Objects.IN.InventoryItem template,
    INMatrixGenerationRule genRule,
    bool isLastSegment,
    MatrixInventoryItem newItem,
    bool useLastAutoNumberValue,
    string locale)
  {
    string empty = string.Empty;
    string segmentType = genRule.SegmentType;
    if (segmentType != null && segmentType.Length == 2)
    {
      string str1;
      switch (segmentType[1])
      {
        case 'C':
          if (segmentType == "AC")
          {
            str1 = this.GetAttributeCaption(genRule, template, newItem, locale);
            break;
          }
          goto label_21;
        case 'D':
          if (segmentType == "TD")
          {
            str1 = this.GetTemplateDescription(template, locale);
            break;
          }
          goto label_21;
        case 'I':
          if (segmentType == "TI")
          {
            str1 = template.InventoryCD;
            break;
          }
          goto label_21;
        case 'N':
          if (segmentType == "AN")
          {
            if (!useLastAutoNumberValue)
            {
              str1 = AutoNumberAttribute.GetNextNumber(this._graph.Caches[typeof (PX.Objects.IN.InventoryItem)], (object) null, genRule.NumberingID, this._graph.Accessinfo.BusinessDate);
              break;
            }
            if (useLastAutoNumberValue)
            {
              str1 = AutoNumberAttribute.GetNumberingSequence(genRule.NumberingID, this._graph.Accessinfo.BranchID, this._graph.Accessinfo.BusinessDate)?.LastNbr;
              if (string.IsNullOrEmpty(str1))
              {
                str1 = AutoNumberAttribute.GetNextNumber(this._graph.Caches[typeof (PX.Objects.IN.InventoryItem)], (object) null, genRule.NumberingID, this._graph.Accessinfo.BusinessDate);
                break;
              }
              break;
            }
            goto label_21;
          }
          goto label_21;
        case 'O':
          if (segmentType == "CO")
          {
            str1 = genRule.Constant;
            break;
          }
          goto label_21;
        case 'P':
          if (segmentType == "SP")
          {
            str1 = " ";
            break;
          }
          goto label_21;
        case 'V':
          if (segmentType == "AV")
          {
            str1 = this.GetAttributeValue(genRule, template, newItem);
            break;
          }
          goto label_21;
        default:
          goto label_21;
      }
      string str2 = str1 ?? string.Empty;
      int length1 = str2.Length;
      int? numberOfCharacters = genRule.NumberOfCharacters;
      int valueOrDefault1 = numberOfCharacters.GetValueOrDefault();
      if (length1 > valueOrDefault1 & numberOfCharacters.HasValue)
      {
        string str3 = str2;
        numberOfCharacters = genRule.NumberOfCharacters;
        int length2 = numberOfCharacters.Value;
        str2 = str3.Substring(0, length2);
      }
      else
      {
        int length3 = str2.Length;
        numberOfCharacters = genRule.NumberOfCharacters;
        int valueOrDefault2 = numberOfCharacters.GetValueOrDefault();
        if (length3 < valueOrDefault2 & numberOfCharacters.HasValue && genRule.AddSpaces.GetValueOrDefault())
        {
          string str4 = str2;
          numberOfCharacters = genRule.NumberOfCharacters;
          int totalWidth = numberOfCharacters.Value;
          str2 = str4.PadRight(totalWidth);
        }
      }
      if (!genRule.AddSpaces.GetValueOrDefault())
        str2 = str2.TrimEnd();
      res.Append(str2);
      if (isLastSegment)
        return;
      if (genRule.UseSpaceAsSeparator.GetValueOrDefault())
      {
        res.Append(' ');
        return;
      }
      res.Append(genRule.Separator);
      return;
    }
label_21:
    throw new PXArgumentException("INMatrixGenerationRule");
  }

  protected virtual string GetTemplateDescription(PX.Objects.IN.InventoryItem template, string locale)
  {
    string templateDescription;
    if (!string.IsNullOrEmpty(locale))
    {
      templateDescription = PXDBLocalizableStringAttribute.GetTranslation<PX.Objects.IN.InventoryItem.descr>(this._graph.Caches[typeof (PX.Objects.IN.InventoryItem)], (object) template, locale);
      if (string.IsNullOrEmpty(templateDescription))
        templateDescription = template.Descr;
    }
    else
      templateDescription = template.Descr;
    return templateDescription;
  }

  public virtual string GetAttributeCaption(
    INMatrixGenerationRule genRule,
    PX.Objects.IN.InventoryItem template,
    MatrixInventoryItem newItem,
    string locale)
  {
    PXGraph graph = this._graph;
    string attributeId = genRule.AttributeID;
    int? parentItemClassId = template.ParentItemClassID;
    ref int? local = ref parentItemClassId;
    string entityClassID = local.HasValue ? local.GetValueOrDefault().ToString() : (string) null;
    string fullName = typeof (PX.Objects.IN.InventoryItem).FullName;
    if (!(CSAttributeGroup.PK.Find(graph, attributeId, entityClassID, fullName)?.AttributeCategory == "A"))
      return this.GetAttributeCaption(genRule.AttributeID, newItem, locale);
    CSAnswers dirty = CSAnswers.PK.FindDirty(this._graph, template.NoteID, genRule.AttributeID);
    return !string.IsNullOrEmpty(dirty?.Value) ? this.GetAttributeCaption(genRule, dirty.Value, locale) : string.Empty;
  }

  protected virtual string GetAttributeCaption(
    string attributeID,
    MatrixInventoryItem newItem,
    string locale)
  {
    string attributeCaption = string.Empty;
    for (int index = 0; index < newItem.AttributeIDs.Length; ++index)
    {
      if (newItem.AttributeIDs[index].Equals(attributeID, StringComparison.OrdinalIgnoreCase))
      {
        if (!string.IsNullOrEmpty(locale))
        {
          string attributeValue = newItem.AttributeValues[index];
          attributeCaption = this.GetComboAttributeCaption(attributeID, attributeValue, locale);
          break;
        }
        attributeCaption = newItem.AttributeValueDescrs[index];
        break;
      }
    }
    return attributeCaption;
  }

  protected virtual string GetAttributeCaption(
    INMatrixGenerationRule genRule,
    string valueID,
    string locale)
  {
    int? controlType = ((KList<string, CRAttribute.Attribute>) CRAttribute.Attributes)[genRule.AttributeID].ControlType;
    if (controlType.HasValue)
    {
      switch (controlType.GetValueOrDefault())
      {
        case 1:
        case 4:
        case 5:
        case 8:
          return valueID;
        case 2:
          return this.GetComboAttributeCaption(genRule.AttributeID, valueID, locale);
        case 6:
          return this.GetMultiComboAttributeCaption(genRule, valueID, locale);
      }
    }
    throw new NotImplementedException();
  }

  protected virtual string GetComboAttributeCaption(
    string attributeID,
    string valueID,
    string locale)
  {
    CSAttributeDetail csAttributeDetail = CSAttributeDetail.PK.Find(this._graph, attributeID, valueID);
    string attributeCaption = (string) null;
    if (!string.IsNullOrEmpty(locale))
      attributeCaption = PXDBLocalizableStringAttribute.GetTranslation<CSAttributeDetail.description>(this._graph.Caches[typeof (CSAttributeDetail)], (object) csAttributeDetail, locale);
    if (string.IsNullOrEmpty(attributeCaption))
      attributeCaption = csAttributeDetail.Description;
    return attributeCaption;
  }

  protected virtual string GetMultiComboAttributeCaption(
    INMatrixGenerationRule genRule,
    string valueID,
    string locale)
  {
    if (string.IsNullOrEmpty(valueID))
      return string.Empty;
    IEnumerable<string> values = ((IEnumerable<string>) valueID.Split(',')).Select<string, string>((Func<string, string>) (v => this.GetComboAttributeCaption(genRule.AttributeID, v, locale)));
    return string.Join(genRule.UseSpaceAsSeparator.GetValueOrDefault() ? " " : genRule.Separator, values);
  }

  public virtual string GetAttributeValue(
    INMatrixGenerationRule genRule,
    PX.Objects.IN.InventoryItem template,
    MatrixInventoryItem newItem)
  {
    string attributeValue = string.Empty;
    PXGraph graph = this._graph;
    string attributeId = genRule.AttributeID;
    int? parentItemClassId = template.ParentItemClassID;
    ref int? local = ref parentItemClassId;
    string entityClassID = local.HasValue ? local.GetValueOrDefault().ToString() : (string) null;
    string fullName = typeof (PX.Objects.IN.InventoryItem).FullName;
    if (CSAttributeGroup.PK.Find(graph, attributeId, entityClassID, fullName)?.AttributeCategory == "A")
    {
      attributeValue = CSAnswers.PK.FindDirty(this._graph, template.NoteID, genRule.AttributeID)?.Value ?? string.Empty;
      if (((KList<string, CRAttribute.Attribute>) CRAttribute.Attributes)[genRule.AttributeID].ControlType.GetValueOrDefault() == 6)
      {
        string[] strArray = attributeValue.Split(',');
        attributeValue = string.Join(genRule.UseSpaceAsSeparator.GetValueOrDefault() ? " " : genRule.Separator, strArray);
      }
    }
    else
    {
      for (int index = 0; index < newItem.AttributeIDs.Length; ++index)
      {
        if (newItem.AttributeIDs[index].Equals(genRule.AttributeID, StringComparison.OrdinalIgnoreCase))
        {
          attributeValue = newItem.AttributeValues[index];
          break;
        }
      }
    }
    return attributeValue;
  }

  public virtual void CreateUpdateMatrixItems(
    InventoryItemMaintBase graph,
    PX.Objects.IN.InventoryItem templateItem,
    IEnumerable<MatrixInventoryItem> itemsToCreateUpdate,
    bool create,
    Action<MatrixInventoryItem, PX.Objects.IN.InventoryItem> beforeSave = null)
  {
    Dictionary<string, string> dictionary = GraphHelper.RowCast<CSAnswers>((IEnumerable) PXSelectBase<CSAnswers, PXSelectReadonly<CSAnswers, Where<CSAnswers.refNoteID, Equal<Required<PX.Objects.IN.InventoryItem.noteID>>>>.Config>.Select((PXGraph) graph, new object[1]
    {
      (object) templateItem.NoteID
    })).ToDictionary<CSAnswers, string, string>((Func<CSAnswers, string>) (a => a.AttributeID), (Func<CSAnswers, string>) (a => a.Value), (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    IEnumerable<POVendorInventory> array1 = (IEnumerable<POVendorInventory>) GraphHelper.RowCast<POVendorInventory>((IEnumerable) ((PXSelectBase) graph.VendorItems).View.SelectMultiBound((object[]) new PX.Objects.IN.InventoryItem[1]
    {
      templateItem
    }, Array.Empty<object>())).ToArray<POVendorInventory>();
    IEnumerable<INUnit> array2 = (IEnumerable<INUnit>) GraphHelper.RowCast<INUnit>((IEnumerable) ((PXSelectBase) graph.UnitsOfMeasureExt.itemunits).View.SelectMultiBound((object[]) new PX.Objects.IN.InventoryItem[1]
    {
      templateItem
    }, Array.Empty<object>())).ToArray<INUnit>();
    IEnumerable<INItemCategory> array3 = (IEnumerable<INItemCategory>) GraphHelper.RowCast<INItemCategory>((IEnumerable) ((PXSelectBase) graph.Category).View.SelectMultiBound((object[]) new PX.Objects.IN.InventoryItem[1]
    {
      templateItem
    }, Array.Empty<object>())).ToArray<INItemCategory>();
    IEnumerable<INItemBoxEx> templateItemBoxes = (IEnumerable<INItemBoxEx>) null;
    InventoryItemMaint graph1 = (InventoryItemMaint) null;
    if (templateItem.StkItem.GetValueOrDefault())
    {
      graph1 = (InventoryItemMaint) graph;
      templateItemBoxes = (IEnumerable<INItemBoxEx>) GraphHelper.RowCast<INItemBoxEx>((IEnumerable) ((PXSelectBase) graph1.Boxes).View.SelectMultiBound((object[]) new PX.Objects.IN.InventoryItem[1]
      {
        templateItem
      }, Array.Empty<object>())).ToArray<INItemBoxEx>();
    }
    IEnumerable<InventoryItemCurySettings> array4 = (IEnumerable<InventoryItemCurySettings>) GraphHelper.RowCast<InventoryItemCurySettings>((IEnumerable) ((PXSelectBase<InventoryItemCurySettings>) graph.ItemCurySettings).Select(new object[1]
    {
      (object) templateItem.InventoryID
    })).ToArray<InventoryItemCurySettings>();
    foreach (MatrixInventoryItem itemToCreateUpdate in itemsToCreateUpdate)
    {
      ((PXGraph) graph).Clear();
      PX.Objects.IN.InventoryItem inventoryItem1;
      if (create)
      {
        PXDimensionAttribute.SuppressAutoNumbering<PX.Objects.IN.InventoryItem.inventoryCD>(((PXSelectBase) graph.Item).Cache, true);
        PX.Objects.IN.InventoryItem inventoryItem2 = new PX.Objects.IN.InventoryItem()
        {
          InventoryCD = itemToCreateUpdate.InventoryCD
        };
        inventoryItem1 = ((PXSelectBase<PX.Objects.IN.InventoryItem>) graph.Item).Insert(inventoryItem2);
      }
      else
        inventoryItem1 = ((PXSelectBase<PX.Objects.IN.InventoryItem>) graph.Item).Current = PXResultset<PX.Objects.IN.InventoryItem>.op_Implicit(((PXSelectBase<PX.Objects.IN.InventoryItem>) graph.Item).Search<PX.Objects.IN.InventoryItem.inventoryCD>((object) itemToCreateUpdate.InventoryCD, Array.Empty<object>()));
      if (inventoryItem1 == null)
        throw new PXInvalidOperationException();
      PX.Objects.IN.InventoryItem inventoryItem3 = this.AssignInventoryFields(graph, templateItem, inventoryItem1, itemToCreateUpdate, create);
      this.AssignInventoryAttributes(graph, itemToCreateUpdate, templateItem, dictionary, create);
      this.AssignVendorInventory(graph, array1);
      this.AssignInventoryConversions(graph, array2);
      this.AssignInventoryCategories(graph, array3);
      if (templateItem.StkItem.GetValueOrDefault())
        this.AssignInventoryBoxes(graph1, templateItemBoxes);
      this.AssignCurySettings(graph, array4);
      if (beforeSave != null)
        beforeSave(itemToCreateUpdate, inventoryItem3);
      ((PXAction) graph.Save).Press();
      itemToCreateUpdate.InventoryID = inventoryItem3.InventoryID;
    }
  }

  protected virtual PX.Objects.IN.InventoryItem AssignInventoryFields(
    InventoryItemMaintBase graph,
    PX.Objects.IN.InventoryItem templateItem,
    PX.Objects.IN.InventoryItem item,
    MatrixInventoryItem itemToCreateUpdate,
    bool create)
  {
    HashSet<string> userExcludedFields = new HashSet<string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    if (create)
    {
      item = this.AssignInventoryField<PX.Objects.IN.InventoryItem.descr>(graph, item, (object) itemToCreateUpdate.Descr);
      PXDBLocalizableStringAttribute.CopyTranslations<MatrixInventoryItem.descr, PX.Objects.IN.InventoryItem.descr>((PXGraph) graph, (object) itemToCreateUpdate, (object) item);
    }
    else
      EnumerableExtensions.AddRange<string>((ISet<string>) userExcludedFields, this.GetExcludedFields(templateItem.InventoryID, typeof (PX.Objects.IN.InventoryItem)));
    item = this.AssignInventoryField<PX.Objects.IN.InventoryItem.itemClassID>(graph, item, (object) templateItem.ItemClassID);
    if (create || !userExcludedFields.Contains("postClassID"))
      item = this.AssignInventoryField<PX.Objects.IN.InventoryItem.postClassID>(graph, item, (object) templateItem.PostClassID);
    this.AssignConversionsSettings(graph, item, templateItem, userExcludedFields, create);
    item = this.AssignRestInventoryFields(graph, item, templateItem, userExcludedFields);
    item = this.AssignInventoryField<PX.Objects.IN.InventoryItem.templateItemID>(graph, item, (object) templateItem.InventoryID);
    return item;
  }

  protected virtual PX.Objects.IN.InventoryItem AssignInventoryField<TField>(
    InventoryItemMaintBase graph,
    PX.Objects.IN.InventoryItem item,
    object value)
    where TField : IBqlField
  {
    PX.Objects.IN.InventoryItem copy = (PX.Objects.IN.InventoryItem) ((PXSelectBase) graph.Item).Cache.CreateCopy((object) item);
    ((PXSelectBase) graph.Item).Cache.SetValue<TField>((object) copy, value);
    return ((PXSelectBase<PX.Objects.IN.InventoryItem>) graph.Item).Update(copy);
  }

  protected virtual void AssignConversionsSettings(
    InventoryItemMaintBase graph,
    PX.Objects.IN.InventoryItem item,
    PX.Objects.IN.InventoryItem templateItem,
    HashSet<string> userExcludedFields,
    bool create)
  {
    PXCache cache = ((PXSelectBase) graph.Item).Cache;
    if (create)
    {
      if (templateItem.BaseUnit != item.BaseUnit || templateItem.SalesUnit != item.SalesUnit || templateItem.PurchaseUnit != item.PurchaseUnit)
      {
        cache.SetValueExt<PX.Objects.IN.InventoryItem.baseUnit>((object) item, (object) null);
        cache.SetValue<PX.Objects.IN.InventoryItem.salesUnit>((object) item, (object) null);
        cache.SetValue<PX.Objects.IN.InventoryItem.purchaseUnit>((object) item, (object) null);
        cache.SetValueExt<PX.Objects.IN.InventoryItem.baseUnit>((object) item, (object) templateItem.BaseUnit);
        cache.SetValueExt<PX.Objects.IN.InventoryItem.salesUnit>((object) item, (object) templateItem.SalesUnit);
        cache.SetValueExt<PX.Objects.IN.InventoryItem.purchaseUnit>((object) item, (object) templateItem.PurchaseUnit);
      }
      cache.SetValueExt<PX.Objects.IN.InventoryItem.decimalBaseUnit>((object) item, (object) templateItem.DecimalBaseUnit);
      cache.SetValueExt<PX.Objects.IN.InventoryItem.decimalSalesUnit>((object) item, (object) templateItem.DecimalSalesUnit);
      cache.SetValueExt<PX.Objects.IN.InventoryItem.decimalPurchaseUnit>((object) item, (object) templateItem.DecimalPurchaseUnit);
    }
    else
    {
      if (!userExcludedFields.Contains("salesUnit"))
        cache.SetValueExt<PX.Objects.IN.InventoryItem.salesUnit>((object) item, (object) templateItem.SalesUnit);
      if (!userExcludedFields.Contains("purchaseUnit"))
        cache.SetValueExt<PX.Objects.IN.InventoryItem.purchaseUnit>((object) item, (object) templateItem.PurchaseUnit);
      if (!userExcludedFields.Contains("decimalSalesUnit"))
        cache.SetValueExt<PX.Objects.IN.InventoryItem.decimalSalesUnit>((object) item, (object) templateItem.DecimalSalesUnit);
      if (userExcludedFields.Contains("decimalPurchaseUnit"))
        return;
      cache.SetValueExt<PX.Objects.IN.InventoryItem.decimalPurchaseUnit>((object) item, (object) templateItem.DecimalPurchaseUnit);
    }
  }

  protected virtual PX.Objects.IN.InventoryItem AssignRestInventoryFields(
    InventoryItemMaintBase graph,
    PX.Objects.IN.InventoryItem item,
    PX.Objects.IN.InventoryItem templateItem,
    HashSet<string> userExcludedFields)
  {
    if (!userExcludedFields.Contains("body"))
      PXDBLocalizableStringAttribute.CopyTranslations<PX.Objects.IN.InventoryItem.body, PX.Objects.IN.InventoryItem.body>(this._graph, (object) templateItem, (object) item);
    IEnumerable<string> source = ((IEnumerable<string>) new string[34]
    {
      "origLotSerClassID",
      "inventoryID",
      "inventoryCD",
      "descr",
      "templateItemID",
      "isTemplate",
      "Tstamp",
      "createdByID",
      "createdByScreenID",
      "createdDateTime",
      "lastModifiedByID",
      "lastModifiedByScreenID",
      "lastModifiedDateTime",
      "noteID",
      "columnAttributeValue",
      "rowAttributeValue",
      "defaultColumnMatrixAttributeID",
      "defaultRowMatrixAttributeID",
      "attributeDescriptionGroupID",
      "basePrice",
      "dfltReceiptLocationID",
      "dfltShipLocationID",
      "dfltSiteID",
      "lastStdCost",
      "pendingStdCost",
      "pendingStdCostDate",
      "preferredVendorID",
      "preferredVendorLocationID",
      "recPrice",
      "stdCost",
      "stdCostDate",
      "planningMethod",
      "preferredItemClasses",
      "priceOfSuggestedItems"
    }).Concat<string>((IEnumerable<string>) userExcludedFields);
    PX.Objects.IN.InventoryItem inventoryItem = graph.CopyTemplateItem(item, templateItem, source.ToList<string>());
    return ((PXSelectBase<PX.Objects.IN.InventoryItem>) graph.Item).Update(inventoryItem);
  }

  protected virtual void AssignInventoryAttributes(
    InventoryItemMaintBase graph,
    MatrixInventoryItem itemToCreateUpdate,
    PX.Objects.IN.InventoryItem templateItem,
    Dictionary<string, string> templateAttrValues,
    bool create)
  {
    HashSet<string> hashSet = create ? (HashSet<string>) null : this.GetExcludedAttributes(itemToCreateUpdate.TemplateItemID).ToHashSet<string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    CSAnswers[] array = GraphHelper.RowCast<CSAnswers>((IEnumerable) graph.Answers.Select(Array.Empty<object>())).ToArray<CSAnswers>();
    foreach (CSAnswers csAnswers in array)
    {
      if (create || !hashSet.Contains(csAnswers.AttributeID))
      {
        string str = (string) null;
        for (int index = 0; index < itemToCreateUpdate.AttributeIDs.Length; ++index)
        {
          if (itemToCreateUpdate.AttributeIDs[index].Equals(csAnswers.AttributeID, StringComparison.OrdinalIgnoreCase))
          {
            str = itemToCreateUpdate.AttributeValues[index];
            break;
          }
        }
        if (str == null)
          templateAttrValues.TryGetValue(csAnswers.AttributeID, out str);
        csAnswers.Value = str;
        graph.Answers.Update(csAnswers);
      }
    }
    this.AssignDummyAttribute(graph, templateItem, array);
  }

  protected virtual void AssignDummyAttribute(
    InventoryItemMaintBase graph,
    PX.Objects.IN.InventoryItem template,
    CSAnswers[] answers)
  {
    this.AssignDummyAttribute(graph, template, ((PXSelectBase<PX.Objects.IN.InventoryItem>) graph.Item).Current.NoteID);
  }

  [PXInternalUseOnly]
  public virtual void AssignDummyAttribute(
    InventoryItemMaintBase graph,
    PX.Objects.IN.InventoryItem template,
    Guid? matrixItemNoteID)
  {
    if (!(template.DefaultColumnMatrixAttributeID == "~MX~DUMMY~") && !(template.DefaultRowMatrixAttributeID == "~MX~DUMMY~") || CSAnswers.PK.FindDirty((PXGraph) graph, matrixItemNoteID, "~MX~DUMMY~") != null)
      return;
    CSAnswers csAnswers = new CSAnswers()
    {
      AttributeCategory = "V",
      AttributeID = "~MX~DUMMY~",
      Value = "Value",
      RefNoteID = matrixItemNoteID
    };
    graph.Answers.Insert(csAnswers);
  }

  protected virtual void AssignVendorInventory(
    InventoryItemMaintBase graph,
    IEnumerable<POVendorInventory> templateVendorInvs)
  {
    string[] strArray1 = (string[]) null;
    string[] strArray2 = (string[]) null;
    bool flag1 = false;
    POVendorInventory[] array = GraphHelper.RowCast<POVendorInventory>((IEnumerable) ((PXSelectBase<POVendorInventory>) graph.VendorItems).Select(Array.Empty<object>())).ToArray<POVendorInventory>();
    foreach (POVendorInventory templateVendorInv1 in templateVendorInvs)
    {
      POVendorInventory templateVendorInv = templateVendorInv1;
      POVendorInventory poVendorInventory = ((IEnumerable<POVendorInventory>) array).FirstOrDefault<POVendorInventory>((Func<POVendorInventory, bool>) (vi =>
      {
        int? subItemId1 = vi.SubItemID;
        int? subItemId2 = templateVendorInv.SubItemID;
        if (subItemId1.GetValueOrDefault() == subItemId2.GetValueOrDefault() & subItemId1.HasValue == subItemId2.HasValue)
        {
          int? vendorId1 = vi.VendorID;
          int? vendorId2 = templateVendorInv.VendorID;
          if (vendorId1.GetValueOrDefault() == vendorId2.GetValueOrDefault() & vendorId1.HasValue == vendorId2.HasValue)
          {
            int? vendorLocationId1 = vi.VendorLocationID;
            int? vendorLocationId2 = templateVendorInv.VendorLocationID;
            if (vendorLocationId1.GetValueOrDefault() == vendorLocationId2.GetValueOrDefault() & vendorLocationId1.HasValue == vendorLocationId2.HasValue)
              return vi.PurchaseUnit == templateVendorInv.PurchaseUnit;
          }
        }
        return false;
      }));
      bool flag2 = poVendorInventory == null;
      string[] first = flag2 ? strArray1 : strArray2;
      if (first == null)
      {
        first = new string[10]
        {
          "recordID",
          "inventoryID",
          "Tstamp",
          "createdByID",
          "createdByScreenID",
          "createdDateTime",
          "lastModifiedByID",
          "lastModifiedByScreenID",
          "lastModifiedDateTime",
          "noteID"
        };
        IEnumerable<string> excludedFields = this.GetExcludedFields(templateVendorInv.InventoryID, typeof (POVendorInventory));
        if (flag2)
        {
          flag1 = excludedFields.Any<string>((Func<string, bool>) (f => ((IEnumerable<string>) new string[4]
          {
            "SubItemID",
            "VendorID",
            "VendorLocationID",
            "PurchaseUnit"
          }).Contains<string>(f, (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase)));
          strArray1 = first;
        }
        else
        {
          first = ((IEnumerable<string>) first).Concat<string>(excludedFields).ToArray<string>();
          strArray2 = first;
        }
      }
      if (flag2)
      {
        if (!flag1)
          poVendorInventory = ((PXSelectBase<POVendorInventory>) graph.VendorItems).Insert();
        else
          continue;
      }
      POVendorInventory copy = (POVendorInventory) ((PXSelectBase) graph.VendorItems).Cache.CreateCopy((object) poVendorInventory);
      ((PXSelectBase) graph.VendorItems).Cache.RestoreCopy((object) copy, (object) templateVendorInv);
      foreach (string str in first)
        ((PXSelectBase) graph.VendorItems).Cache.SetValue((object) copy, str, ((PXSelectBase) graph.VendorItems).Cache.GetValue((object) poVendorInventory, str));
      ((PXSelectBase<POVendorInventory>) graph.VendorItems).Update(copy);
    }
  }

  protected virtual void AssignInventoryConversions(
    InventoryItemMaintBase graph,
    IEnumerable<INUnit> templateItemConvs)
  {
    string[] strArray1 = (string[]) null;
    string[] strArray2 = (string[]) null;
    bool flag1 = false;
    INUnit[] array = GraphHelper.RowCast<INUnit>((IEnumerable) ((PXSelectBase<INUnit>) graph.UnitsOfMeasureExt.itemunits).Select(Array.Empty<object>())).ToArray<INUnit>();
    foreach (INUnit templateItemConv1 in templateItemConvs)
    {
      INUnit templateItemConv = templateItemConv1;
      INUnit inUnit = ((IEnumerable<INUnit>) array).FirstOrDefault<INUnit>((Func<INUnit, bool>) (ic => ic.FromUnit == templateItemConv.FromUnit && ic.ToUnit == templateItemConv.ToUnit));
      bool flag2 = inUnit == null;
      string[] first = flag2 ? strArray1 : strArray2;
      if (first == null)
      {
        first = new string[9]
        {
          "recordID",
          "inventoryID",
          "Tstamp",
          "createdByID",
          "createdByScreenID",
          "createdDateTime",
          "lastModifiedByID",
          "lastModifiedByScreenID",
          "lastModifiedDateTime"
        };
        IEnumerable<string> excludedFields = this.GetExcludedFields(templateItemConv.InventoryID, typeof (INUnit));
        if (flag2)
        {
          flag1 = excludedFields.Any<string>((Func<string, bool>) (f => ((IEnumerable<string>) new string[3]
          {
            "unitType",
            "fromUnit",
            "toUnit"
          }).Contains<string>(f, (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase)));
          strArray1 = first;
        }
        else
        {
          first = ((IEnumerable<string>) first).Concat<string>(excludedFields).ToArray<string>();
          strArray2 = first;
        }
      }
      if (flag2)
      {
        if (!flag1)
          inUnit = ((PXSelectBase<INUnit>) graph.UnitsOfMeasureExt.itemunits).Insert(new INUnit()
          {
            FromUnit = templateItemConv.FromUnit
          });
        else
          continue;
      }
      INUnit copy = (INUnit) ((PXSelectBase) graph.UnitsOfMeasureExt.itemunits).Cache.CreateCopy((object) inUnit);
      ((PXSelectBase) graph.UnitsOfMeasureExt.itemunits).Cache.RestoreCopy((object) copy, (object) templateItemConv);
      foreach (string str in first)
        ((PXSelectBase) graph.UnitsOfMeasureExt.itemunits).Cache.SetValue((object) copy, str, ((PXSelectBase) graph.UnitsOfMeasureExt.itemunits).Cache.GetValue((object) inUnit, str));
      ((PXSelectBase<INUnit>) graph.UnitsOfMeasureExt.itemunits).Update(copy);
    }
  }

  protected virtual void AssignInventoryCategories(
    InventoryItemMaintBase graph,
    IEnumerable<INItemCategory> templateItemCategs)
  {
    IEnumerable<string> source = (IEnumerable<string>) null;
    INItemCategory[] array = GraphHelper.RowCast<INItemCategory>((IEnumerable) ((PXSelectBase<INItemCategory>) graph.Category).Select(Array.Empty<object>())).ToArray<INItemCategory>();
    foreach (INItemCategory templateItemCateg1 in templateItemCategs)
    {
      INItemCategory templateItemCateg = templateItemCateg1;
      if (((IEnumerable<INItemCategory>) array).FirstOrDefault<INItemCategory>((Func<INItemCategory, bool>) (ic =>
      {
        int? categoryId1 = ic.CategoryID;
        int? categoryId2 = templateItemCateg.CategoryID;
        return categoryId1.GetValueOrDefault() == categoryId2.GetValueOrDefault() & categoryId1.HasValue == categoryId2.HasValue;
      })) == null)
      {
        if (source == null)
        {
          source = this.GetExcludedFields(templateItemCateg.InventoryID, typeof (INItemCategory));
          if (source.Contains<string>("categoryID", (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase))
            break;
        }
        ((PXSelectBase<INItemCategory>) graph.Category).Insert(new INItemCategory()
        {
          CategoryID = templateItemCateg.CategoryID
        });
      }
    }
  }

  protected virtual void AssignInventoryBoxes(
    InventoryItemMaint graph,
    IEnumerable<INItemBoxEx> templateItemBoxes)
  {
    string[] strArray1 = (string[]) null;
    string[] strArray2 = (string[]) null;
    bool flag1 = false;
    INItemBoxEx[] array = GraphHelper.RowCast<INItemBoxEx>((IEnumerable) ((PXSelectBase<INItemBoxEx>) graph.Boxes).Select(Array.Empty<object>())).ToArray<INItemBoxEx>();
    foreach (INItemBoxEx templateItemBox1 in templateItemBoxes)
    {
      INItemBoxEx templateItemBox = templateItemBox1;
      INItemBoxEx inItemBoxEx1 = ((IEnumerable<INItemBoxEx>) array).FirstOrDefault<INItemBoxEx>((Func<INItemBoxEx, bool>) (ic => ic.BoxID == templateItemBox.BoxID));
      bool flag2 = inItemBoxEx1 == null;
      string[] first = flag2 ? strArray1 : strArray2;
      if (first == null)
      {
        first = new string[9]
        {
          "inventoryID",
          "Tstamp",
          "createdByID",
          "createdByScreenID",
          "createdDateTime",
          "lastModifiedByID",
          "lastModifiedByScreenID",
          "lastModifiedDateTime",
          "noteID"
        };
        IEnumerable<string> excludedFields = this.GetExcludedFields(templateItemBox.InventoryID, typeof (INItemBox));
        if (flag2)
        {
          flag1 = excludedFields.Contains<string>("BoxID", (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
          strArray1 = first;
        }
        else
        {
          first = ((IEnumerable<string>) first).Concat<string>(excludedFields).ToArray<string>();
          strArray2 = first;
        }
      }
      if (flag2)
      {
        if (!flag1)
        {
          FbqlSelect<SelectFromBase<INItemBoxEx, TypeArrayOf<IFbqlJoin>.Empty>.Where<KeysRelation<Field<INItemBoxEx.inventoryID>.IsRelatedTo<PX.Objects.IN.InventoryItem.inventoryID>.AsSimpleKey.WithTablesOf<PX.Objects.IN.InventoryItem, INItemBoxEx>, PX.Objects.IN.InventoryItem, INItemBoxEx>.SameAsCurrent>, INItemBoxEx>.View boxes = graph.Boxes;
          INItemBoxEx inItemBoxEx2 = new INItemBoxEx();
          inItemBoxEx2.BoxID = templateItemBox.BoxID;
          inItemBoxEx1 = ((PXSelectBase<INItemBoxEx>) boxes).Insert(inItemBoxEx2);
        }
        else
          continue;
      }
      INItemBoxEx copy = (INItemBoxEx) ((PXSelectBase) graph.Boxes).Cache.CreateCopy((object) inItemBoxEx1);
      ((PXSelectBase) graph.Boxes).Cache.RestoreCopy((object) copy, (object) templateItemBox);
      foreach (string str in first)
        ((PXSelectBase) graph.Boxes).Cache.SetValue((object) copy, str, ((PXSelectBase) graph.Boxes).Cache.GetValue((object) inItemBoxEx1, str));
      ((PXSelectBase<INItemBoxEx>) graph.Boxes).Update(copy);
    }
  }

  protected virtual void AssignCurySettings(
    InventoryItemMaintBase graph,
    IEnumerable<InventoryItemCurySettings> templateCurySettingsRows)
  {
    string[] strArray1 = (string[]) null;
    string[] strArray2 = (string[]) null;
    bool flag1 = false;
    InventoryItemCurySettings[] array = GraphHelper.RowCast<InventoryItemCurySettings>((IEnumerable) ((PXSelectBase<InventoryItemCurySettings>) graph.ItemCurySettings).Select(new object[1]
    {
      (object) ((PXSelectBase<PX.Objects.IN.InventoryItem>) graph.Item).Current.InventoryID
    })).ToArray<InventoryItemCurySettings>();
    foreach (InventoryItemCurySettings templateCurySettingsRow in templateCurySettingsRows)
    {
      InventoryItemCurySettings templateCurySettings = templateCurySettingsRow;
      InventoryItemCurySettings itemCurySettings = ((IEnumerable<InventoryItemCurySettings>) array).FirstOrDefault<InventoryItemCurySettings>((Func<InventoryItemCurySettings, bool>) (cs => cs.CuryID == templateCurySettings.CuryID));
      bool flag2 = itemCurySettings == null;
      string[] first = flag2 ? strArray1 : strArray2;
      if (first == null)
      {
        first = new string[13]
        {
          "inventoryID",
          "lastStdCost",
          "stdCost",
          "stdCostDate",
          "preferredVendorID",
          "preferredVendorLocationID",
          "Tstamp",
          "createdByID",
          "createdByScreenID",
          "createdDateTime",
          "lastModifiedByID",
          "lastModifiedByScreenID",
          "lastModifiedDateTime"
        };
        IEnumerable<string> excludedFields = this.GetExcludedFields(templateCurySettings.InventoryID, typeof (InventoryItemCurySettings));
        if (flag2)
        {
          flag1 = excludedFields.Any<string>((Func<string, bool>) (f => ((IEnumerable<string>) new string[1]
          {
            "curyID"
          }).Contains<string>(f, (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase)));
          strArray1 = first;
        }
        else
        {
          first = ((IEnumerable<string>) first).Concat<string>(excludedFields).ToArray<string>();
          strArray2 = first;
        }
      }
      if (flag2)
      {
        if (!flag1)
          itemCurySettings = ((PXSelectBase<InventoryItemCurySettings>) graph.ItemCurySettings).Insert(new InventoryItemCurySettings()
          {
            CuryID = templateCurySettings.CuryID
          });
        else
          continue;
      }
      PXCache cache = ((PXSelectBase) graph.ItemCurySettings).Cache;
      InventoryItemCurySettings copy = (InventoryItemCurySettings) cache.CreateCopy((object) itemCurySettings);
      cache.RestoreCopy((object) copy, (object) templateCurySettings);
      foreach (string str in first)
        cache.SetValue((object) copy, str, cache.GetValue((object) itemCurySettings, str));
      ((PXSelectBase<InventoryItemCurySettings>) graph.ItemCurySettings).Update(copy);
    }
  }

  protected static TResult GetValueFromArray<TResult>(TResult[] array, int index)
  {
    if (index >= 0)
    {
      int num = index;
      int? length = array?.Length;
      int valueOrDefault = length.GetValueOrDefault();
      if (num < valueOrDefault & length.HasValue)
        return array[index];
    }
    return default (TResult);
  }

  public virtual (string FieldName, string DisplayName)[] GetAttributesToUpdateItem(
    PX.Objects.IN.InventoryItem item)
  {
    return ((IEnumerable<PXResult<CSAttributeGroup>>) ((PXSelectBase<CSAttributeGroup>) new PXSelectJoin<CSAttributeGroup, InnerJoin<CSAttribute, On<CSAttribute.attributeID, Equal<CSAttributeGroup.attributeID>>>, Where<CSAttributeGroup.entityClassID, Equal<Required<PX.Objects.IN.InventoryItem.itemClassID>>, And<CSAttributeGroup.entityType, Equal<Constants.DACName<PX.Objects.IN.InventoryItem>>, And<CSAttributeGroup.isActive, Equal<True>, And<CSAttributeGroup.attributeCategory, Equal<CSAttributeGroup.attributeCategory.attribute>>>>>, OrderBy<Asc<CSAttributeGroup.sortOrder, Asc<CSAttributeGroup.attributeID>>>>(this._graph)).Select(new object[1]
    {
      (object) item.ItemClassID
    })).AsEnumerable<PXResult<CSAttributeGroup>>().Select<PXResult<CSAttributeGroup>, (string, string)>((Func<PXResult<CSAttributeGroup>, (string, string)>) (r => (PXResult<CSAttributeGroup>.op_Implicit(r).AttributeID, PXResult.Unwrap<CSAttribute>((object) r).Description))).ToArray<(string, string)>();
  }

  protected virtual IEnumerable<string> GetExcludedAttributes(int? templateID)
  {
    return ((IEnumerable<ExcludedAttribute>) ((PXSelectBase<ExcludedAttribute>) new PXSelect<ExcludedAttribute, Where<ExcludedAttribute.templateID, Equal<Required<PX.Objects.IN.InventoryItem.templateItemID>>, And<INMatrixExcludedData.isActive, Equal<True>>>>(this._graph)).SelectMain(new object[1]
    {
      (object) templateID
    })).Select<ExcludedAttribute, string>((Func<ExcludedAttribute, string>) (excludeAttribute => excludeAttribute.FieldName));
  }

  public virtual (System.Type Dac, string DisplayName)[] GetTablesToUpdateItem()
  {
    return ((IEnumerable<System.Type>) new System.Type[6]
    {
      typeof (PX.Objects.IN.InventoryItem),
      typeof (InventoryItemCurySettings),
      typeof (POVendorInventory),
      typeof (INUnit),
      typeof (INItemCategory),
      typeof (INItemBox)
    }).Select<System.Type, (System.Type, string)>((Func<System.Type, (System.Type, string)>) (t => (t, this._graph.Caches[t].DisplayName))).ToArray<(System.Type, string)>();
  }

  public virtual (string FieldName, string DisplayName)[] GetFieldsToUpdateItem(System.Type table)
  {
    PXCache cache = this._graph.Caches[table];
    IEnumerable<string> strings = cache.BqlFields.Select<System.Type, string>((Func<System.Type, string>) (f => f.Name)).Where<string>((Func<string, bool>) (field => cache.GetAttributesOfType<PXDBFieldAttribute>((object) null, field).Any<PXDBFieldAttribute>() && cache.GetAttributesOfType<PXUIFieldAttribute>((object) null, field).Any<PXUIFieldAttribute>((Func<PXUIFieldAttribute, bool>) (a => a.Enabled)))).Except<string>((IEnumerable<string>) new string[8]
    {
      "Tstamp",
      "createdByID",
      "createdByScreenID",
      "createdDateTime",
      "lastModifiedByID",
      "lastModifiedByScreenID",
      "lastModifiedDateTime",
      "noteID"
    }, (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase).Except<string>((IEnumerable<string>) new string[3]
    {
      "inventoryID",
      "inventoryCD",
      "recordID"
    }, (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    if (table == typeof (PX.Objects.IN.InventoryItem))
    {
      string[] second = new string[19]
      {
        "itemClassID",
        "baseUnit",
        "decimalBaseUnit",
        "itemType",
        "taxCategoryID",
        "kitItem",
        "valMethod",
        "completePOLine",
        "nonStockReceipt",
        "nonStockShip",
        "preferredVendorID",
        "preferredVendorLocationID",
        "templateItemID",
        "isTemplate",
        "columnAttributeValue",
        "rowAttributeValue",
        "defaultColumnMatrixAttributeID",
        "defaultRowMatrixAttributeID",
        "attributeDescriptionGroupID"
      };
      strings = strings.Except<string>((IEnumerable<string>) second, (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    }
    return strings.Select<string, (string, string)>((Func<string, (string, string)>) (field => (field, PXUIFieldAttribute.GetDisplayName(cache, field) ?? field))).ToArray<(string, string)>();
  }

  protected virtual IEnumerable<string> GetExcludedFields(int? templateID, System.Type tableName)
  {
    PXSelect<ExcludedField, Where<ExcludedField.templateID, Equal<Required<PX.Objects.IN.InventoryItem.templateItemID>>, And<ExcludedField.tableName, Equal<Required<ExcludedField.tableName>>, And<INMatrixExcludedData.isActive, Equal<True>>>>> select = new PXSelect<ExcludedField, Where<ExcludedField.templateID, Equal<Required<PX.Objects.IN.InventoryItem.templateItemID>>, And<ExcludedField.tableName, Equal<Required<ExcludedField.tableName>>, And<INMatrixExcludedData.isActive, Equal<True>>>>>(this._graph);
    List<string> list = ((IEnumerable<ExcludedField>) ((PXSelectBase<ExcludedField>) select).SelectMain(new object[2]
    {
      (object) templateID,
      (object) tableName.FullName
    })).Select<ExcludedField, string>((Func<ExcludedField, string>) (excludeField => excludeField.FieldName)).ToList<string>();
    this.AddRelatedExcludedFields(list, templateID, tableName, (PXSelectBase<ExcludedField>) select);
    return (IEnumerable<string>) list;
  }

  protected virtual void AddRelatedExcludedFields(
    List<string> result,
    int? templateID,
    System.Type tableName,
    PXSelectBase<ExcludedField> select)
  {
    if (!(tableName == typeof (InventoryItemCurySettings)))
      return;
    result.AddRange(((IEnumerable<ExcludedField>) select.SelectMain(new object[2]
    {
      (object) templateID,
      (object) typeof (PX.Objects.IN.InventoryItem).FullName
    })).Where<ExcludedField>((Func<ExcludedField, bool>) (excludeField => this._graph.Caches[typeof (InventoryItemCurySettings)].Fields.Contains(excludeField.FieldName))).Select<ExcludedField, string>((Func<ExcludedField, string>) (excludeField => excludeField.FieldName)));
  }
}
