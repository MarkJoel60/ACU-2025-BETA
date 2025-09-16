// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CSAttributeGroupList`2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.CS;
using PX.Data;
using PX.Metadata;
using PX.Objects.CS;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Compilation;

#nullable disable
namespace PX.Objects.CR;

public class CSAttributeGroupList<TClass, TEntity> : PXSelectBase<CSAttributeGroup> where TClass : class
{
  private readonly string _classIdFieldName;
  private readonly System.Type _class;

  [InjectDependency]
  protected IScreenInfoCacheControl ScreenInfoCacheControl { get; set; }

  public CSAttributeGroupList(PXGraph graph)
  {
    ((PXSelectBase) this)._Graph = graph;
    Select3<CSAttributeGroup, InnerJoin<CSAttribute, On<CSAttributeGroup.attributeID, Equal<CSAttribute.attributeID>>>, OrderBy<Asc<CSAttributeGroup.entityClassID, Asc<CSAttributeGroup.entityType, Asc<CSAttributeGroup.sortOrder>>>>> select3_1 = new Select3<CSAttributeGroup, InnerJoin<CSAttribute, On<CSAttributeGroup.attributeID, Equal<CSAttribute.attributeID>>>, OrderBy<Asc<CSAttributeGroup.entityClassID, Asc<CSAttributeGroup.entityType, Asc<CSAttributeGroup.sortOrder>>>>>();
    PXGraph pxGraph = graph;
    Select3<CSAttributeGroup, InnerJoin<CSAttribute, On<CSAttributeGroup.attributeID, Equal<CSAttribute.attributeID>>>, OrderBy<Asc<CSAttributeGroup.entityClassID, Asc<CSAttributeGroup.entityType, Asc<CSAttributeGroup.sortOrder>>>>> select3_2 = select3_1;
    CSAttributeGroupList<TClass, TEntity> attributeGroupList = this;
    // ISSUE: virtual method pointer
    PXSelectDelegate pxSelectDelegate = new PXSelectDelegate((object) attributeGroupList, __vmethodptr(attributeGroupList, SelectDelegate));
    ((PXSelectBase) this).View = new PXView(pxGraph, false, (BqlCommand) select3_2, (Delegate) pxSelectDelegate);
    if (typeof (IBqlTable).IsAssignableFrom(typeof (TClass)))
    {
      this._class = typeof (TClass);
      this._classIdFieldName = ((PXSelectBase) this)._Graph.Caches[this._class].BqlKeys.Single<System.Type>().Name;
    }
    else
    {
      if (!typeof (IBqlField).IsAssignableFrom(typeof (TClass)))
        throw new PXArgumentException(typeof (TClass).Name);
      this._classIdFieldName = typeof (TClass).Name;
      this._class = typeof (TClass).DeclaringType;
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    ((PXSelectBase) this)._Graph.FieldDefaulting.AddHandler<CSAttributeGroup.entityType>(CSAttributeGroupList<TClass, TEntity>.\u003C\u003Ec.\u003C\u003E9__6_0 ?? (CSAttributeGroupList<TClass, TEntity>.\u003C\u003Ec.\u003C\u003E9__6_0 = new PXFieldDefaulting((object) CSAttributeGroupList<TClass, TEntity>.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003C\u002Ector\u003Eb__6_0))));
    // ISSUE: method pointer
    ((PXSelectBase) this)._Graph.FieldDefaulting.AddHandler<CSAttributeGroup.entityClassID>(new PXFieldDefaulting((object) this, __methodptr(\u003C\u002Ector\u003Eb__6_1)));
    // ISSUE: method pointer
    ((PXSelectBase) this)._Graph.RowDeleted.AddHandler(this._class, new PXRowDeleted((object) this, __methodptr(\u003C\u002Ector\u003Eb__6_2)));
    // ISSUE: method pointer
    ((PXSelectBase) this)._Graph.RowPersisted.AddHandler<CSAttributeGroup>(new PXRowPersisted((object) this, __methodptr(\u003C\u002Ector\u003Eb__6_3)));
    // ISSUE: method pointer
    ((PXSelectBase) this)._Graph.FieldSelecting.AddHandler<CSAttributeGroup.defaultValue>(new PXFieldSelecting((object) this, __methodptr(CSAttributeGroup_DefaultValue_FieldSelecting)));
    // ISSUE: method pointer
    ((PXSelectBase) this)._Graph.RowDeleting.AddHandler<CSAttributeGroup>(new PXRowDeleting((object) this, __methodptr(OnRowDeleting)));
    if (graph.Views.Caches.Contains(typeof (CSAnswers)))
      return;
    graph.Views.Caches.Add(typeof (CSAnswers));
  }

  private void OnRowDeleting(PXCache sender, PXRowDeletingEventArgs e)
  {
    CSAttributeGroup row = (CSAttributeGroup) e.Row;
    if (row == null || sender.GetStatus(e.Row) == 2 || sender.GetStatus(e.Row) == 4 || ((PXSelectBase) this)._Graph.Caches[this._class].Current != null && ((PXSelectBase) this)._Graph.Caches[this._class].GetStatus(((PXSelectBase) this)._Graph.Caches[this._class].Current) == 3)
      return;
    if (row.IsActive.GetValueOrDefault())
      throw new PXSetPropertyException("The attribute cannot be removed because it is active. Make it inactive before removing.");
    if (!((PXSelectBase) this)._Graph.IsContractBasedAPI && this.Ask("Warning", "This action will delete the attribute from the class and all attribute values from corresponding entities", (MessageButtons) 1) != 1)
      ((CancelEventArgs) e).Cancel = true;
    else
      CSAttributeGroupList<TClass, TEntity>.DeleteAttributesForGroup(((PXSelectBase) this)._Graph, row);
  }

  public static void DeleteAttributesForGroup(PXGraph graph, CSAttributeGroup attributeGroup)
  {
    System.Type type1 = PXBuildManager.GetType(attributeGroup.EntityType, false);
    System.Type type2 = !(type1 == (System.Type) null) ? EntityHelper.GetNoteType(type1) : throw new ArgumentNullException("entityType", "Could not locate entity type " + attributeGroup.EntityType);
    if (type2 == (System.Type) null)
      throw new ArgumentNullException("noteIdField", "Could not locate NoteId field for " + attributeGroup.EntityType);
    System.Type type3 = graph.Caches[type1].GetAttributes((string) null).OfType<CRAttributesFieldAttribute>().FirstOrDefault<CRAttributesFieldAttribute>()?.ClassIdField;
    if (type3 == (System.Type) null)
      throw new ArgumentNullException("classIdField", "Could not locate ClassId field for " + attributeGroup.EntityType);
    List<object> objectList1 = new List<object>()
    {
      (object) attributeGroup.EntityClassID,
      (object) attributeGroup.EntityClassID,
      (object) attributeGroup.AttributeID,
      (object) attributeGroup.EntityType
    };
    if (type3 != (System.Type) null)
      objectList1[0] = graph.Caches[type1].ValueFromString(type3.Name, attributeGroup.EntityClassID);
    if (!graph.Caches[type1].GetAttributes(type3.Name).Any<PXEventSubscriberAttribute>((Func<PXEventSubscriberAttribute, bool>) (x =>
    {
      System.Type type4 = x.GetType();
      return type4.IsSubclassOf(typeof (PXDBFieldAttribute)) || type4 == typeof (PXDBCalcedAttribute) || type4.IsSubclassOf(typeof (PXDBCalcedAttribute));
    })))
      type3 = typeof (CSAttributeGroup.entityClassID);
    PXGraph pxGraph = new PXGraph();
    System.Type type5 = BqlCommand.Compose(new System.Type[3]
    {
      typeof (Equal<>),
      typeof (Required<>),
      type3
    });
    List<object> objectList2 = new PXView(pxGraph, false, BqlCommand.CreateInstance(new System.Type[1]
    {
      BqlCommand.Compose(new System.Type[14]
      {
        typeof (Select2<,,>),
        typeof (CSAttributeGroup),
        typeof (InnerJoin<,,>),
        typeof (CSAnswers),
        typeof (On<CSAnswers.attributeID, Equal<CSAttributeGroup.attributeID>>),
        typeof (InnerJoin<,>),
        type1,
        typeof (On<,,>),
        type2,
        typeof (Equal<CSAnswers.refNoteID>),
        typeof (And<,>),
        type3,
        type5,
        typeof (Where<CSAttributeGroup.entityClassID, Equal<Required<CSAttributeGroup.entityClassID>>, And<CSAttributeGroup.attributeID, Equal<Required<CSAttributeGroup.attributeID>>, And<CSAttributeGroup.entityType, Equal<Required<CSAttributeGroup.entityType>>>>>)
      })
    })).SelectMultiBound((object[]) null, objectList1.ToArray());
    if (objectList2.Count == 0)
      return;
    foreach (object obj in objectList2)
    {
      CSAnswers csAnswers = PXResult.Unwrap<CSAnswers>(obj);
      GraphHelper.Caches<CSAnswers>(graph).Delete(csAnswers);
    }
  }

  private void CSAttributeGroup_DefaultValue_FieldSelecting(
    PXCache sender,
    PXFieldSelectingEventArgs e)
  {
    CSAttributeGroup row = (CSAttributeGroup) e.Row;
    if (row == null)
      return;
    CSAttribute source = PXResultset<CSAttribute>.op_Implicit(((PXSelectBase<CSAttribute>) new PXSelect<CSAttribute>(((PXSelectBase) this)._Graph)).Search<CSAttribute.attributeID>((object) row.AttributeID, Array.Empty<object>()));
    IEnumerable<CSAttributeDetail> firstTableItems = PXSelectBase<CSAttributeDetail, PXSelect<CSAttributeDetail, Where<CSAttributeDetail.attributeID, Equal<Required<CSAttributeGroup.attributeID>>>, OrderBy<Asc<CSAttributeDetail.sortOrder>>>.Config>.Select(((PXSelectBase) this)._Graph, new object[1]
    {
      (object) row.AttributeID
    }).FirstTableItems;
    KeyValueHelper.Attribute attribute = source == null ? (KeyValueHelper.Attribute) null : new KeyValueHelper.Attribute(new CSAttribute().PopulateFrom<CSAttribute, CSAttribute>(source), firstTableItems.Select<CSAttributeDetail, CSAttributeDetail>((Func<CSAttributeDetail, CSAttributeDetail>) (o => new CSAttributeDetail().PopulateFrom<CSAttributeDetail, CSAttributeDetail>(o))));
    e.ReturnState = (object) KeyValueHelper.MakeFieldState(attribute, "DefaultValue", e.ReturnState, new int?(row.Required.GetValueOrDefault() ? 1 : -1), (string) null, (string) null);
  }

  protected virtual IEnumerable SelectDelegate()
  {
    CSAttributeGroupList<TClass, TEntity> attributeGroupList = this;
    PXCache cach = ((PXSelectBase) attributeGroupList)._Graph.Caches[attributeGroupList._class];
    object current = cach.Current;
    if (current != null)
    {
      object obj = cach.GetValue(current, attributeGroupList._classIdFieldName);
      if (obj != null)
      {
        PXSelectJoin<CSAttributeGroup, InnerJoin<CSAttribute, On<CSAttributeGroup.attributeID, Equal<CSAttribute.attributeID>>>, Where<CSAttributeGroup.entityClassID, Equal<Required<CSAttributeGroup.entityClassID>>, And<CSAttributeGroup.entityType, Equal<Required<CSAttributeGroup.entityType>>>>> pxSelectJoin = new PXSelectJoin<CSAttributeGroup, InnerJoin<CSAttribute, On<CSAttributeGroup.attributeID, Equal<CSAttribute.attributeID>>>, Where<CSAttributeGroup.entityClassID, Equal<Required<CSAttributeGroup.entityClassID>>, And<CSAttributeGroup.entityType, Equal<Required<CSAttributeGroup.entityType>>>>>(((PXSelectBase) attributeGroupList)._Graph);
        object[] objArray = new object[2]
        {
          (object) obj.ToString(),
          (object) typeof (TEntity).FullName
        };
        foreach (PXResult<CSAttributeGroup> pxResult in ((PXSelectBase<CSAttributeGroup>) pxSelectJoin).Select(objArray))
        {
          if (PXResult.Unwrap<CSAttributeGroup>((object) pxResult) != null)
            yield return (object) pxResult;
        }
      }
    }
  }
}
