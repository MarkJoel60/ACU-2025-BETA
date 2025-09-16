// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.ActivityEntityIDSelectorAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Web.Compilation;

#nullable disable
namespace PX.Objects.CR;

public class ActivityEntityIDSelectorAttribute : 
  EntityIDSelectorAttribute,
  IPXFieldUpdatedSubscriber,
  IPXRowPersistingSubscriber
{
  protected readonly System.Type _contactIdBqlField;
  protected readonly System.Type _baccountIdBqlField;

  protected string _contactIdFieldName { get; set; }

  protected string _baccountIdFieldName { get; set; }

  protected PXView RelatedEntity { get; set; }

  protected EntityHelper EntityHelperInstance { get; set; }

  public bool SuppressFillingContactAndBAccount { get; set; }

  public ActivityEntityIDSelectorAttribute(
    System.Type typeBqlField,
    System.Type contactIdBqlField,
    System.Type baccountIdBqlField)
    : base(typeBqlField)
  {
    this._contactIdBqlField = contactIdBqlField;
    this._baccountIdBqlField = baccountIdBqlField;
  }

  public override void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    this.Graph = sender.Graph;
    this._contactIdFieldName = sender.GetField(this._contactIdBqlField);
    this._baccountIdFieldName = sender.GetField(this._baccountIdBqlField);
    PXGraph.FieldUpdatedEvents fieldUpdated = this.Graph.FieldUpdated;
    System.Type itemType = sender.GetItemType();
    string name = this._typeBqlField.Name;
    ActivityEntityIDSelectorAttribute selectorAttribute = this;
    // ISSUE: virtual method pointer
    PXFieldUpdated pxFieldUpdated = new PXFieldUpdated((object) selectorAttribute, __vmethodptr(selectorAttribute, _RelatedEntityType_FieldUpdated));
    fieldUpdated.AddHandler(itemType, name, pxFieldUpdated);
    this.EntityHelperInstance = new EntityHelper(this.Graph);
  }

  public virtual void _RelatedEntityType_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    object row = e.Row;
    if (row == null)
      return;
    sender.SetValue(row, this.FieldName, (object) null);
  }

  public virtual void RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    object row = e.Row;
    if (row == null || !(sender.GetValue(row, this._typeFieldName) is string typeName))
      return;
    System.Type relatedEntityType = this.GetRelatedEntityType(typeName);
    PXCache cach = this.Graph.Caches[relatedEntityType];
    object entityRow = this.EntityHelperInstance.GetEntityRow(relatedEntityType, sender.GetValue(row, this.FieldName) as Guid?);
    string noteField = EntityHelper.GetNoteField(relatedEntityType);
    GraphHelper.EnsureCachePersistence<Note>(this.Graph);
    object obj = entityRow;
    string str = noteField;
    PXNoteAttribute.GetNoteID(cach, obj, str);
    this.Graph.Caches[typeof (Note)].Persist((PXDBOperation) 2);
    this.Graph.Caches[typeof (Note)].Persisted(false);
  }

  public virtual void FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    if (e.Row == null || object.Equals(sender.GetValue(e.Row, this._FieldName), e.OldValue))
      return;
    this.FillRefNoteIDType(sender, e.Row);
    if (sender.Graph.UnattendedMode && (sender.GetValuePending(e.Row, this._contactIdFieldName) == PXCache.NotSetValue || sender.GetValuePending(e.Row, this._baccountIdFieldName) == PXCache.NotSetValue))
      return;
    this.FillContactAndBAccount(sender, e.Row);
  }

  public override void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    base.FieldSelecting(sender, e);
    if (!(e.ReturnState is PXFieldState returnState))
      return;
    int num = sender.Graph.IsMobile || sender.GetValue(e.Row, this._typeFieldName) == null ? (sender.Graph.IsContractBasedAPI ? 1 : 0) : 1;
    returnState.Enabled = num != 0;
  }

  protected System.Type GetRelatedEntityType(string typeName)
  {
    return PXBuildManager.GetType(typeName, false);
  }

  protected virtual System.Type GetRelatedEntity(object row)
  {
    if (row == null)
      return (System.Type) null;
    if (this.Graph.Caches[this.BqlTable].GetValue(row, this._typeFieldName) is string typeName)
      return this.GetRelatedEntityType(typeName);
    return this.Graph.Caches[this.BqlTable].GetValue(row, this.FieldName) is Guid guid ? this.EntityHelperInstance.GetEntityRowType(new Guid?(guid), true) : (System.Type) null;
  }

  public virtual void FillRefNoteIDType(PXCache sender, object row)
  {
    if (row == null || this.Graph.Caches[this.BqlTable].GetValue(row, this._typeFieldName) is string)
      return;
    string fullName = this.GetRelatedEntity(row)?.FullName;
    this.Graph.Caches[this.BqlTable].SetValue(row, this._typeFieldName, (object) fullName);
  }

  public virtual void FillContactAndBAccount(PXCache sender, object row)
  {
    if (row == null || this.SuppressFillingContactAndBAccount || !(this.Graph.Caches[this.BqlTable].GetValue(row, this._typeFieldName) is string typeName))
      return;
    System.Type type = PXBuildManager.GetType(typeName, false);
    Guid? nullable = this.Graph.Caches[this.BqlTable].GetValue(row, this.FieldName) as Guid?;
    EntityHelper entityHelper = new EntityHelper(this.Graph);
    object entityRow = entityHelper.GetEntityRow(type, nullable);
    if (entityRow == null)
      return;
    System.Type primaryGraphType = entityHelper.GetPrimaryGraphType(type, entityRow, true);
    if (primaryGraphType == (System.Type) null)
      return;
    object copy = sender.CreateCopy(row);
    PXGraph instance = PXGraph.CreateInstance(primaryGraphType);
    System.Type noteType = EntityHelper.GetNoteType(type);
    PXView pxView = new PXView(this.Graph, false, BqlCommand.CreateInstance(new System.Type[1]
    {
      BqlCommand.Compose(new System.Type[7]
      {
        typeof (Select<,>),
        type,
        typeof (Where<,>),
        noteType,
        typeof (Equal<>),
        typeof (Required<>),
        noteType
      })
    }));
    instance.Caches[type].Current = pxView.SelectSingle(new object[1]
    {
      (object) nullable
    });
    if (instance.Caches[type].Current == null)
      return;
    instance.Caches[type].SetStatus(instance.Caches[type].Current, (PXEntryStatus) 2);
    PXCache cach = instance.Caches[sender.GetItemType()];
    cach.SetDefaultExt(copy, this._contactIdFieldName, (object) null);
    cach.SetDefaultExt(copy, this._baccountIdFieldName, (object) null);
    sender.SetValue(row, this._contactIdFieldName, sender.GetValue(copy, this._contactIdFieldName));
    sender.SetValue(row, this._baccountIdFieldName, sender.GetValue(copy, this._baccountIdFieldName));
  }
}
