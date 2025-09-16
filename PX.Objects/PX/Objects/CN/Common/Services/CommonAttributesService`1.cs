// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Common.Services.CommonAttributesService`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CR;
using PX.Objects.CS;

#nullable disable
namespace PX.Objects.CN.Common.Services;

/// <summary>
/// Provides generic functionality for integration with common attibutes.
/// Also is used to repeat create, update and delete operations on attribute groups related to TCache entity.
/// When a user adds a new attribute to a class, it is created only for TPrimaryCache entity.
/// The same attribute group is created for related TCache entity from code.
/// </summary>
public class CommonAttributesService<TAttributeGroup> where TAttributeGroup : CSAttributeGroup, new()
{
  private readonly PXSelectBase<TAttributeGroup> attributes;
  private readonly AttributeGroupDataProvider dataProvider;
  private readonly PXGraph graph;

  public CommonAttributesService(PXGraph graph, PXSelectBase<TAttributeGroup> attributes)
  {
    this.attributes = attributes;
    this.graph = graph;
    this.dataProvider = new AttributeGroupDataProvider(graph);
  }

  /// <summary>
  /// Fills required fields for attribute group. Should be used on RowInserting event.
  /// </summary>
  public void InitializeInsertedAttribute<TPrimaryCache>(
    TAttributeGroup attributeGroup,
    string classId)
    where TPrimaryCache : IBqlTable
  {
    attributeGroup.EntityClassID = classId;
    attributeGroup.EntityType = typeof (TPrimaryCache).FullName;
  }

  public void DeleteAnswersIfRequired<TPrimaryCache>(Events.RowDeleting<TAttributeGroup> args) where TPrimaryCache : IBqlTable
  {
    TAttributeGroup row = args.Row;
    if (row.IsActive.GetValueOrDefault())
      throw new PXSetPropertyException("The attribute cannot be removed because it is active. Make it inactive before removing.");
    if (this.IsDeleteConfirmed())
      CSAttributeGroupList<IBqlTable, TPrimaryCache>.DeleteAttributesForGroup(this.graph, (CSAttributeGroup) row);
    else
      args.Cancel = true;
  }

  /// <summary>
  /// Configures the type of control for default value field on adding an attribute. Should be used on
  /// FieldSelecting event for <see cref="!:TAttributeGroup.defaultValue" />.
  /// </summary>
  public PXFieldState GetNewReturnState(object returnState, TAttributeGroup attributeGroup)
  {
    return this.dataProvider.GetNewReturnState(returnState, (CSAttributeGroup) attributeGroup);
  }

  public void DeleteRelatedEntityAnswer<TCache>(TAttributeGroup attributeGroup) where TCache : IBqlTable
  {
    TAttributeGroup relatedEntityAttribute = this.GetRelatedEntityAttribute<TCache>(attributeGroup.AttributeID, attributeGroup.EntityClassID);
    if ((object) relatedEntityAttribute == null)
      return;
    CSAttributeGroupList<IBqlTable, TCache>.DeleteAttributesForGroup(this.graph, (CSAttributeGroup) relatedEntityAttribute);
  }

  public void DeleteRelatedEntityAttribute<TCache>(TAttributeGroup attribute) where TCache : IBqlTable
  {
    TAttributeGroup relatedEntityAttribute = this.GetRelatedEntityAttribute<TCache>(attribute.AttributeID, attribute.EntityClassID);
    if ((object) relatedEntityAttribute == null)
      return;
    ((PXCache) GraphHelper.Caches<TAttributeGroup>(this.graph)).Delete((object) relatedEntityAttribute);
  }

  public void UpdateRelatedEntityAttribute<TCache>(
    TAttributeGroup oldAttribute,
    TAttributeGroup newAttribute)
    where TCache : IBqlTable
  {
    TAttributeGroup relatedEntityAttribute = this.GetRelatedEntityAttribute<TCache>(oldAttribute.AttributeID, oldAttribute.EntityClassID);
    if ((object) relatedEntityAttribute == null)
      return;
    PXCache<TAttributeGroup> pxCache = GraphHelper.Caches<TAttributeGroup>(this.graph);
    GraphHelper.MarkUpdated((PXCache) pxCache, (object) relatedEntityAttribute);
    ((PXCache) pxCache).RestoreCopy((object) relatedEntityAttribute, (object) newAttribute);
    relatedEntityAttribute.EntityType = typeof (TCache).FullName;
  }

  public void CreateRelatedEntityAttribute<TCache>(TAttributeGroup attribute) where TCache : IBqlTable
  {
    PXCache<TAttributeGroup> pxCache = GraphHelper.Caches<TAttributeGroup>(this.graph);
    TAttributeGroup copy = (TAttributeGroup) ((PXCache) pxCache).CreateCopy((object) attribute);
    copy.EntityType = typeof (TCache).FullName;
    ((PXCache) pxCache).Insert((object) copy);
  }

  private bool IsDeleteConfirmed()
  {
    return this.attributes.Ask("Warning", "This action will delete the attribute from the class and all attribute values from corresponding entities", (MessageButtons) 1) == 1;
  }

  private TAttributeGroup GetRelatedEntityAttribute<TCache>(string attributeId, string classId) where TCache : IBqlTable
  {
    return ((PXSelectBase<TAttributeGroup>) new PXSelect<TAttributeGroup, Where<CSAttributeGroup.attributeID, Equal<Required<CSAttributeGroup.attributeID>>, And<CSAttributeGroup.entityClassID, Equal<Required<CSAttributeGroup.entityClassID>>, And<CSAttributeGroup.entityType, Equal<Required<CSAttributeGroup.entityType>>>>>>(this.graph)).SelectSingle(new object[3]
    {
      (object) attributeId,
      (object) classId,
      (object) typeof (TCache).FullName
    });
  }
}
