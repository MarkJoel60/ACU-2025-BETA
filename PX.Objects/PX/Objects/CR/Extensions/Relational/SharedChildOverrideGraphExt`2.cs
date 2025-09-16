// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Extensions.Relational.SharedChildOverrideGraphExt`2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System.Reflection;

#nullable disable
namespace PX.Objects.CR.Extensions.Relational;

/// <summary>
/// Extension that is used for linking the single shared <see cref="!:Child" /> to two same-level entities: <see cref="!:Document" /> and <see cref="T:PX.Objects.CR.Extensions.Relational.SharedChildOverrideGraphExt`2.Related" />.
/// Inserts <see cref="!:Child" /> on <see cref="!:Document" /> inserting.
/// Deletes <see cref="!:Child" /> on <see cref="!:Document" /> deleting (if it wasn't shred with the <see cref="T:PX.Objects.CR.Extensions.Relational.SharedChildOverrideGraphExt`2.Related" />).
/// </summary>
public abstract class SharedChildOverrideGraphExt<TGraph, TThis> : CRParentChild<TGraph, TThis>
  where TGraph : PXGraph
  where TThis : SharedChildOverrideGraphExt<TGraph, TThis>
{
  public PXSelectExtension<SharedChildOverrideGraphExt<TGraph, TThis>.Related> RelatedDocument;

  public virtual bool ViewHasADelegate { get; set; }

  public virtual bool IsChildRequired(CRParentChild<TGraph, TThis>.Document document) => true;

  public override void Initialize()
  {
    base.Initialize();
    ((PXSelectBase) this.RelatedDocument).View = new PXView((PXGraph) this.Base, false, BqlCommand.CreateInstance(new System.Type[2]
    {
      typeof (Select<>),
      ((PXSelectBase) this.RelatedDocument).View.GetItemType()
    }));
    MethodInfo method = ((PXSelectBase) this.RelatedDocument).Cache.GetType().GetMethod("GetBaseBqlField", BindingFlags.Instance | BindingFlags.NonPublic);
    object obj;
    if ((object) method == null)
    {
      obj = (object) null;
    }
    else
    {
      // ISSUE: explicit non-virtual call
      obj = __nonvirtual (method.Invoke((object) ((PXSelectBase) this.RelatedDocument).Cache, new object[1]
      {
        (object) "relatedID"
      }));
    }
    System.Type type = obj as System.Type;
    if (type == (System.Type) null)
      return;
    ((PXSelectBase) this.RelatedDocument).View.WhereNew(BqlCommand.Compose(new System.Type[5]
    {
      typeof (Where<,>),
      type,
      typeof (Equal<>),
      typeof (Required<>),
      type
    }));
  }

  public virtual SharedChildOverrideGraphExt<TGraph, TThis>.Related GetRelatedByID(int? relatedID)
  {
    if (!relatedID.HasValue)
      return (SharedChildOverrideGraphExt<TGraph, TThis>.Related) null;
    if (((PXSelectBase<SharedChildOverrideGraphExt<TGraph, TThis>.Related>) this.RelatedDocument).Current != null)
    {
      int? nullable1 = ((PXSelectBase) this.RelatedDocument).Cache.GetValue((object) ((PXSelectBase<SharedChildOverrideGraphExt<TGraph, TThis>.Related>) this.RelatedDocument).Current, "RelatedID") as int?;
      int? nullable2 = relatedID;
      if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
        return ((PXSelectBase<SharedChildOverrideGraphExt<TGraph, TThis>.Related>) this.RelatedDocument).Current;
    }
    return ((PXSelectBase<SharedChildOverrideGraphExt<TGraph, TThis>.Related>) this.RelatedDocument).SelectSingle(new object[1]
    {
      (object) relatedID
    });
  }

  protected virtual void _(
    Events.RowSelected<CRParentChild<TGraph, TThis>.Document> e)
  {
    CRParentChild<TGraph, TThis>.Document row = e.Row;
    if (row == null)
      return;
    bool flag = true;
    SharedChildOverrideGraphExt<TGraph, TThis>.Related relatedById = this.GetRelatedByID(row.RelatedID);
    int? nullable = row.ChildID ?? ((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<CRParentChild<TGraph, TThis>.Document>>) e).Cache.GetValue((object) row, "ChildID") as int?;
    int? childId = (int?) relatedById?.ChildID;
    if (nullable.GetValueOrDefault() == childId.GetValueOrDefault() & nullable.HasValue == childId.HasValue)
      flag = false;
    ((PXSelectBase) this.PrimaryDocument).Cache.SetValue((object) row, "IsOverrideRelated", (object) flag);
    if (this.ViewHasADelegate)
      return;
    PXUIFieldAttribute.SetEnabled(((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<CRParentChild<TGraph, TThis>.Document>>) e).Cache, (object) e.Row, "IsOverrideRelated", relatedById != null && relatedById.ChildID.HasValue);
  }

  protected virtual void _(
    Events.RowSelected<CRParentChild<TGraph, TThis>.Child> e)
  {
    CRParentChild<TGraph, TThis>.Child row = e.Row;
    if (row == null)
      return;
    bool flag = true;
    SharedChildOverrideGraphExt<TGraph, TThis>.Related relatedById = this.GetRelatedByID(row.RelatedID);
    int? childId1 = row.ChildID;
    int? childId2 = (int?) relatedById?.ChildID;
    if (childId1.GetValueOrDefault() == childId2.GetValueOrDefault() & childId1.HasValue == childId2.HasValue)
      flag = false;
    if (this.ViewHasADelegate)
      return;
    PXUIFieldAttribute.SetEnabled(((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<CRParentChild<TGraph, TThis>.Child>>) e).Cache, (object) row, flag);
  }

  protected virtual void _(
    Events.RowDeleted<CRParentChild<TGraph, TThis>.Document> e)
  {
    CRParentChild<TGraph, TThis>.Document row = e.Row;
    if (row == null)
      return;
    bool? isOverrideRelated = row.IsOverrideRelated;
    bool flag = false;
    if (isOverrideRelated.GetValueOrDefault() == flag & isOverrideRelated.HasValue)
      return;
    CRParentChild<TGraph, TThis>.Child childById = this.GetChildByID(row.ChildID);
    if (childById == null)
      return;
    ((PXSelectBase<CRParentChild<TGraph, TThis>.Child>) this.ChildDocument).Delete(childById);
  }

  protected virtual void _(
    Events.RowInserting<CRParentChild<TGraph, TThis>.Document> e)
  {
    if (this.GetDocumentMapping().Table.IsAssignableFrom(this.GetRelatedMapping().Table))
      return;
    this.ProcessInsert(e.Row);
  }

  protected virtual void _(
    Events.RowInserted<CRParentChild<TGraph, TThis>.Document> e)
  {
    if (!this.GetDocumentMapping().Table.IsAssignableFrom(this.GetRelatedMapping().Table))
      return;
    this.ProcessInsert(e.Row);
  }

  protected virtual void ProcessInsert(CRParentChild<TGraph, TThis>.Document row)
  {
    if (row == null)
      return;
    bool? nullable1 = row.IsOverrideRelated;
    SharedChildOverrideGraphExt<TGraph, TThis>.Related relatedById = this.GetRelatedByID(row.RelatedID);
    CRParentChild<TGraph, TThis>.Child childById1 = this.GetChildByID((int?) relatedById?.ChildID);
    int? nullable2;
    if (childById1 == null)
      nullable1 = new bool?(true);
    else if (!nullable1.GetValueOrDefault())
    {
      if (row.ChildID.HasValue)
      {
        int? childId = row.ChildID;
        nullable2 = relatedById.ChildID;
        if (!(childId.GetValueOrDefault() == nullable2.GetValueOrDefault() & childId.HasValue == nullable2.HasValue))
        {
          CRParentChild<TGraph, TThis>.Child childById2 = this.GetChildByID(row.ChildID);
          using (new ReadOnlyScope(new PXCache[1]
          {
            ((PXSelectBase) this.ChildDocument).View.Cache
          }))
            ((PXSelectBase<CRParentChild<TGraph, TThis>.Child>) this.ChildDocument).Delete(childById2);
        }
      }
      ((PXSelectBase) this.PrimaryDocument).Cache.SetValue((object) row, "ChildID", (object) relatedById.ChildID);
    }
    if (!nullable1.GetValueOrDefault())
      return;
    CRParentChild<TGraph, TThis>.Child child1 = this.GetChildByID(row.ChildID);
    if (child1 == null)
    {
      child1 = childById1 == null ? (CRParentChild<TGraph, TThis>.Child) ((PXSelectBase) this.ChildDocument).Cache.CreateInstance() : (CRParentChild<TGraph, TThis>.Child) ((PXSelectBase) this.ChildDocument).Cache.CreateCopy((object) childById1);
      CRParentChild<TGraph, TThis>.Child child2 = child1;
      nullable2 = new int?();
      int? nullable3 = nullable2;
      child2.ChildID = nullable3;
      child1.RelatedID = row.RelatedID;
      using (new ReadOnlyScope(new PXCache[1]
      {
        ((PXSelectBase) this.ChildDocument).View.Cache
      }))
        child1 = ((PXSelectBase<CRParentChild<TGraph, TThis>.Child>) this.ChildDocument).Insert(child1);
      ((PXSelectBase) this.PrimaryDocument).Cache.SetValue((object) row, "ChildID", (object) child1.ChildID);
    }
    else
    {
      child1.RelatedID = row.RelatedID;
      using (new ReadOnlyScope(new PXCache[1]
      {
        ((PXSelectBase) this.ChildDocument).View.Cache
      }))
        child1 = ((PXSelectBase<CRParentChild<TGraph, TThis>.Child>) this.ChildDocument).Update(child1);
    }
    if (relatedById == null || childById1 != null)
      return;
    ((PXSelectBase) this.RelatedDocument).Cache.SetValue((object) relatedById, "ChildID", (object) child1.ChildID);
  }

  protected virtual void _(
    Events.RowUpdating<CRParentChild<TGraph, TThis>.Document> e)
  {
    this.UpdateRelated(e.NewRow, e.Row);
  }

  public virtual void UpdateRelated(
    CRParentChild<TGraph, TThis>.Document row,
    CRParentChild<TGraph, TThis>.Document oldRow)
  {
    if (row == null || oldRow == null)
      return;
    int? nullable1 = row.RelatedID;
    if (!nullable1.HasValue)
    {
      nullable1 = oldRow.RelatedID;
      if (!nullable1.HasValue)
        return;
    }
    nullable1 = row.RelatedID;
    int? nullable2 = oldRow.RelatedID;
    bool? isOverrideRelated1;
    if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
    {
      nullable2 = row.ChildID;
      nullable1 = oldRow.ChildID;
      if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue)
      {
        bool? isOverrideRelated2 = row.IsOverrideRelated;
        isOverrideRelated1 = oldRow.IsOverrideRelated;
        if (isOverrideRelated2.GetValueOrDefault() == isOverrideRelated1.GetValueOrDefault() & isOverrideRelated2.HasValue == isOverrideRelated1.HasValue)
          return;
      }
    }
    nullable1 = row.RelatedID;
    nullable2 = oldRow.RelatedID;
    if (!(nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue))
    {
      SharedChildOverrideGraphExt<TGraph, TThis>.Related relatedById1 = this.GetRelatedByID(row.RelatedID);
      SharedChildOverrideGraphExt<TGraph, TThis>.Related relatedById2 = this.GetRelatedByID(oldRow.RelatedID);
      nullable2 = row.RelatedID;
      if (!nullable2.HasValue)
      {
        CRParentChild<TGraph, TThis>.Child childById = this.GetChildByID(relatedById2.ChildID);
        if (childById != null)
        {
          nullable2 = relatedById2.ChildID;
          nullable1 = row.ChildID;
          if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue)
          {
            CRParentChild<TGraph, TThis>.Child child1 = (CRParentChild<TGraph, TThis>.Child) ((PXSelectBase) this.ChildDocument).Cache.CreateCopy((object) childById);
            CRParentChild<TGraph, TThis>.Child child2 = child1;
            nullable1 = new int?();
            int? nullable3 = nullable1;
            child2.ChildID = nullable3;
            child1.RelatedID = row.RelatedID;
            using (new ReadOnlyScope(new PXCache[1]
            {
              ((PXSelectBase) this.ChildDocument).View.Cache
            }))
              child1 = ((PXSelectBase<CRParentChild<TGraph, TThis>.Child>) this.ChildDocument).Insert(child1);
            ((PXSelectBase) this.PrimaryDocument).Cache.SetValueExt((object) row, "ChildID", (object) child1.ChildID);
          }
        }
      }
      if (relatedById1 != null)
      {
        isOverrideRelated1 = row.IsOverrideRelated;
        bool flag = false;
        if (isOverrideRelated1.GetValueOrDefault() == flag & isOverrideRelated1.HasValue)
        {
          CRParentChild<TGraph, TThis>.Child childById = this.GetChildByID(relatedById1.ChildID);
          ((PXSelectBase) this.PrimaryDocument).Cache.SetValueExt((object) row, "ChildID", (object) childById.ChildID);
        }
      }
      CRParentChild<TGraph, TThis>.Child childById1 = this.GetChildByID(row.ChildID);
      if (childById1 != null)
      {
        childById1.RelatedID = row.RelatedID;
        using (new ReadOnlyScope(new PXCache[1]
        {
          ((PXSelectBase) this.ChildDocument).View.Cache
        }))
          ((PXSelectBase<CRParentChild<TGraph, TThis>.Child>) this.ChildDocument).Update(childById1);
      }
    }
    isOverrideRelated1 = row.IsOverrideRelated;
    bool? isOverrideRelated3 = oldRow.IsOverrideRelated;
    if (!(isOverrideRelated1.GetValueOrDefault() == isOverrideRelated3.GetValueOrDefault() & isOverrideRelated1.HasValue == isOverrideRelated3.HasValue))
    {
      isOverrideRelated3 = row.IsOverrideRelated;
      if (isOverrideRelated3.GetValueOrDefault())
      {
        ((PXSelectBase) this.PrimaryDocument).Cache.SetValueExt((object) row, "ChildID", (object) null);
      }
      else
      {
        isOverrideRelated3 = row.IsOverrideRelated;
        bool flag = false;
        if (isOverrideRelated3.GetValueOrDefault() == flag & isOverrideRelated3.HasValue)
        {
          SharedChildOverrideGraphExt<TGraph, TThis>.Related relatedById = this.GetRelatedByID(row.RelatedID);
          int? childID;
          if (relatedById == null)
          {
            nullable1 = new int?();
            childID = nullable1;
          }
          else
            childID = relatedById.ChildID;
          CRParentChild<TGraph, TThis>.Child childById = this.GetChildByID(childID);
          if (childById != null)
          {
            ((PXSelectBase) this.PrimaryDocument).Cache.SetValueExt((object) row, "ChildID", (object) childById.ChildID);
            GraphHelper.MarkUpdated(((PXSelectBase) this.ChildDocument).Cache, (object) childById);
          }
        }
      }
    }
    nullable1 = row.ChildID;
    nullable2 = oldRow.ChildID;
    if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
      return;
    nullable2 = row.ChildID;
    if (!nullable2.HasValue && this.IsChildRequired(row))
    {
      SharedChildOverrideGraphExt<TGraph, TThis>.Related relatedById = this.GetRelatedByID(row.RelatedID);
      CRParentChild<TGraph, TThis>.Child childById2 = this.GetChildByID(relatedById.ChildID);
      CRParentChild<TGraph, TThis>.Child child3;
      if (childById2 != null)
      {
        nullable2 = relatedById.ChildID;
        nullable1 = oldRow.ChildID;
        if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue)
        {
          child3 = (CRParentChild<TGraph, TThis>.Child) ((PXSelectBase) this.ChildDocument).Cache.CreateCopy((object) childById2);
          goto label_43;
        }
      }
      CRParentChild<TGraph, TThis>.Child childById3 = this.GetChildByID(oldRow.ChildID);
      child3 = childById3 != null ? (CRParentChild<TGraph, TThis>.Child) ((PXSelectBase) this.ChildDocument).Cache.CreateCopy((object) childById3) : (CRParentChild<TGraph, TThis>.Child) ((PXSelectBase) this.ChildDocument).Cache.CreateInstance();
label_43:
      CRParentChild<TGraph, TThis>.Child child4 = child3;
      nullable1 = new int?();
      int? nullable4 = nullable1;
      child4.ChildID = nullable4;
      child3.RelatedID = row.RelatedID;
      using (new ReadOnlyScope(new PXCache[1]
      {
        ((PXSelectBase) this.ChildDocument).View.Cache
      }))
        child3 = ((PXSelectBase<CRParentChild<TGraph, TThis>.Child>) this.ChildDocument).Insert(child3);
      ((PXSelectBase) this.PrimaryDocument).Cache.SetValueExt((object) row, "ChildID", (object) child3.ChildID);
    }
    CRParentChild<TGraph, TThis>.Child childById4 = this.GetChildByID(oldRow.ChildID);
    if (childById4 == null)
      return;
    SharedChildOverrideGraphExt<TGraph, TThis>.Related relatedById3 = this.GetRelatedByID(childById4.RelatedID);
    if (relatedById3 != null)
    {
      nullable1 = relatedById3.ChildID;
      nullable2 = oldRow.ChildID;
      if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
        return;
    }
    using (new ReadOnlyScope(new PXCache[1]
    {
      ((PXSelectBase) this.ChildDocument).View.Cache
    }))
      ((PXSelectBase<CRParentChild<TGraph, TThis>.Child>) this.ChildDocument).Delete(childById4);
  }

  protected abstract SharedChildOverrideGraphExt<TGraph, TThis>.RelatedMapping GetRelatedMapping();

  [PXHidden]
  public class Related : PX.Objects.CR.Extensions.Relational.Related<TThis>
  {
  }

  protected class RelatedMapping : IBqlMapping
  {
    protected System.Type _table;
    public System.Type RelatedID = typeof (PX.Objects.CR.Extensions.Relational.Related<TThis>.relatedID);
    public System.Type ChildID = typeof (PX.Objects.CR.Extensions.Relational.Related<TThis>.childID);

    public System.Type Extension => typeof (SharedChildOverrideGraphExt<TGraph, TThis>.Related);

    public System.Type Table => this._table;

    public RelatedMapping(System.Type table) => this._table = table;
  }
}
