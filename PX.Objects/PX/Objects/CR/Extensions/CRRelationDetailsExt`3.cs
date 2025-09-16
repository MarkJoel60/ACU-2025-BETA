// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Extensions.CRRelationDetailsExt`3
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CR.Extensions.Cache;
using PX.SM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Compilation;

#nullable enable
namespace PX.Objects.CR.Extensions;

/// <summary>Represents the Relations grid</summary>
public abstract class CRRelationDetailsExt<TGraph, TMaster, TNoteField> : 
  CRRelationDetailDeclarationExt<
  #nullable disable
  TGraph>
  where TGraph : PXGraph, new()
  where TMaster : class, IBqlTable, new()
  where TNoteField : IBqlField
{
  public EntityHelper EntityHelperInstance;
  [PXCopyPasteHiddenView]
  [PXHidden]
  public FbqlSelect<SelectFromBase<Contact, TypeArrayOf<IFbqlJoin>.Empty>, Contact>.View Contact_Dummy;
  [PXCopyPasteHiddenView]
  [PXHidden]
  public FbqlSelect<SelectFromBase<BAccount, TypeArrayOf<IFbqlJoin>.Empty>, BAccount>.View BAccount_Dummy;
  [PXCopyPasteHiddenView]
  [PXViewName("Relations")]
  [PXFilterable(new System.Type[] {})]
  public FbqlSelect<SelectFromBase<CRRelation, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<Contact>.On<BqlOperand<
  #nullable enable
  True, IBqlBool>.IsEqual<
  #nullable disable
  False>>>, FbqlJoins.Left<BAccount>.On<BqlOperand<
  #nullable enable
  True, IBqlBool>.IsEqual<
  #nullable disable
  False>>>, FbqlJoins.Left<Users>.On<BqlOperand<
  #nullable enable
  True, IBqlBool>.IsEqual<
  #nullable disable
  False>>>>.Order<By<BqlField<
  #nullable enable
  CRRelation.createdDateTime, IBqlDateTime>.Asc>>, 
  #nullable disable
  CRRelation>.View Relations;
  protected PXView dbView;
  public PXAction<TMaster> RelationsViewTargetDetails;
  public PXAction<TMaster> RelationsViewEntityDetails;
  public PXAction<TMaster> RelationsViewContactDetails;

  public PXCache MasterCache => this.Base.Caches[BqlCommand.GetItemType(typeof (TNoteField))];

  public virtual void Initialize()
  {
    ((PXGraphExtension) this).Initialize();
    this.dbView = new PXView((PXGraph) this.Base, false, (BqlCommand) new Select2<CRRelation, LeftJoin<Contact, On<Contact.contactID, Equal<CRRelation.contactID>>, LeftJoin<BAccount, On<BAccount.bAccountID, Equal<CRRelation.entityID>>, LeftJoin<Users, On<Users.pKID, Equal<Contact.userID>>>>>, Where<CRRelation.refNoteID, Equal<Current<TNoteField>>>>());
    this.EntityHelperInstance = new EntityHelper((PXGraph) this.Base);
  }

  public virtual IEnumerable relations()
  {
    PXDelegateResult pxDelegateResult = new PXDelegateResult()
    {
      IsResultSorted = false,
      IsResultTruncated = false,
      IsResultFiltered = true
    };
    foreach (PXResult<CRRelation, Contact, BAccount, Users> pxResult in this.dbView.SelectMulti(Array.Empty<object>()))
    {
      CRRelation relation = ((PXResult) pxResult).GetItem<CRRelation>();
      Contact contact = ((PXResult) pxResult).GetItem<Contact>();
      BAccount businessAccount = ((PXResult) pxResult).GetItem<BAccount>();
      Users user = ((PXResult) pxResult).GetItem<Users>();
      (object, System.Type) relatedEntityObject = this.GetRelatedEntityObject(relation);
      this.FillUnboundDataForRelation(relation, contact, businessAccount, user, relatedEntityObject);
      if (this.MeetFilter(relation, relatedEntityObject, PXView.Filters))
        ((List<object>) pxDelegateResult).Add((object) pxResult);
    }
    return (IEnumerable) pxDelegateResult;
  }

  protected System.Type GetRelatedEntityType(string typeName)
  {
    return PXBuildManager.GetType(typeName, false);
  }

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = false)]
  public virtual IEnumerable relationsViewTargetDetails(PXAdapter adapter)
  {
    CRRelation current = ((PXSelectBase<CRRelation>) this.Relations).Current;
    if (current == null)
      return adapter.Get();
    new EntityHelper((PXGraph) this.Base).NavigateToRow(current.TargetType, current.TargetNoteID, (PXRedirectHelper.WindowMode) 3);
    return adapter.Get();
  }

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = false)]
  public virtual IEnumerable relationsViewEntityDetails(PXAdapter adapter)
  {
    BAccount baccount = BAccount.PK.Find((PXGraph) this.Base, (int?) ((PXSelectBase<CRRelation>) this.Relations).Current?.EntityID);
    if (baccount == null)
      return adapter.Get();
    PXRedirectHelper.TryRedirect((PXGraph) this.Base, (object) baccount, (PXRedirectHelper.WindowMode) 3);
    return adapter.Get();
  }

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = false)]
  public virtual IEnumerable relationsViewContactDetails(PXAdapter adapter)
  {
    Contact contact = Contact.PK.Find((PXGraph) this.Base, (int?) ((PXSelectBase<CRRelation>) this.Relations).Current?.ContactID);
    if (contact == null)
      return adapter.Get();
    PXRedirectHelper.TryRedirect((PXGraph) this.Base, (object) contact, (PXRedirectHelper.WindowMode) 3);
    return adapter.Get();
  }

  [StatusField(typeof (CRRelation.targetType))]
  [PXMergeAttributes]
  protected virtual void _(Events.CacheAttached<CRRelation.status> e)
  {
  }

  protected virtual void _(
    Events.FieldDefaulting<CRRelation, CRRelation.refEntityType> e)
  {
    ((Events.FieldDefaultingBase<Events.FieldDefaulting<CRRelation, CRRelation.refEntityType>, CRRelation, object>) e).NewValue = (object) this.MasterCache.GetItemType().FullName;
  }

  protected virtual void _(
    Events.FieldDefaulting<CRRelation, CRRelation.refNoteID> e)
  {
    ((Events.FieldDefaultingBase<Events.FieldDefaulting<CRRelation, CRRelation.refNoteID>, CRRelation, object>) e).NewValue = this.MasterCache.GetValue(this.MasterCache.Current, typeof (TNoteField).Name);
  }

  protected virtual void _(
    Events.FieldUpdated<CRRelation, CRRelation.isPrimary> e)
  {
    if (!e.Row.IsPrimary.GetValueOrDefault() || string.IsNullOrEmpty(e.Row.TargetType))
      return;
    this.ClearOtherPrimaryRelations(e.Row);
  }

  protected virtual void _(
    Events.FieldUpdated<CRRelation, CRRelation.targetType> e)
  {
    if (!e.Row.IsPrimary.GetValueOrDefault() || string.IsNullOrEmpty(e.Row.TargetType))
      return;
    this.ClearOtherPrimaryRelations(e.Row);
  }

  protected virtual void _(Events.RowDeleted<TMaster> e)
  {
    if ((object) e.Row == null)
      return;
    EnumerableExtensions.ForEach<CRRelation>((IEnumerable<CRRelation>) ((PXSelectBase<CRRelation>) this.Relations).SelectMain(Array.Empty<object>()), (Action<CRRelation>) (relation => ((PXSelectBase<CRRelation>) this.Relations).Delete(relation)));
  }

  protected virtual void _(Events.RowInserted<CRRelation> e)
  {
    this.FillUnboundDataForRelation(((Events.Event<PXRowInsertedEventArgs, Events.RowInserted<CRRelation>>) e).Cache.Graph, e.Row);
  }

  protected virtual void _(Events.RowUpdated<CRRelation> e)
  {
    if (!((Events.Event<PXRowUpdatedEventArgs, Events.RowUpdated<CRRelation>>) e).Cache.ObjectsEqual<CRRelation.targetNoteID>((object) e.Row, (object) e.OldRow))
    {
      System.Type type = e.Row.TargetType != null ? GraphHelper.GetType(e.Row.TargetType) : (System.Type) null;
      e.Row.DocNoteID = !(type != (System.Type) null) || typeof (BAccount).IsAssignableFrom(type) || typeof (Contact).IsAssignableFrom(type) ? new Guid?() : e.Row.TargetNoteID;
    }
    if (((Events.Event<PXRowUpdatedEventArgs, Events.RowUpdated<CRRelation>>) e).Cache.ObjectsEqual<CRRelation.contactID>((object) e.Row, (object) e.OldRow) && !((Events.Event<PXRowUpdatedEventArgs, Events.RowUpdated<CRRelation>>) e).Cache.ObjectsEqual<CRRelation.entityID>((object) e.Row, (object) e.OldRow))
      e.Row.ContactID = new int?();
    this.FillUnboundDataForRelation(((Events.Event<PXRowUpdatedEventArgs, Events.RowUpdated<CRRelation>>) e).Cache.Graph, e.Row);
  }

  protected virtual void _(Events.RowSelected<CRRelation> e)
  {
    if (e.Row == null)
      return;
    bool flag = BAccount.PK.Find((PXGraph) this.Base, e.Row.EntityID)?.Type != "EP";
    PXUIFieldAttribute.SetEnabled(((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<CRRelation>>) e).Cache, (object) e.Row, "contactID", flag);
  }

  protected virtual void _(Events.RowPersisting<CRRelation> e)
  {
    if (!e.Row.ContactID.HasValue && e.Row.TargetType == "PX.Objects.CR.Contact")
      ((Events.Event<PXRowPersistingEventArgs, Events.RowPersisting<CRRelation>>) e).Cache.RaiseExceptionHandling<CRRelation.contactID>((object) e.Row, (object) null, (Exception) new PXSetPropertyException("'{0}' cannot be empty.", new object[1]
      {
        (object) "ContactID"
      }));
    else if (!e.Row.EntityID.HasValue && EnumerableExtensions.IsIn<string>(e.Row.TargetType, "PX.Objects.CR.BAccount", "PX.Objects.AR.Customer", "PX.Objects.AP.Vendor"))
    {
      ((Events.Event<PXRowPersistingEventArgs, Events.RowPersisting<CRRelation>>) e).Cache.RaiseExceptionHandling<CRRelation.entityID>((object) e.Row, (object) null, (Exception) new PXSetPropertyException("'{0}' cannot be empty.", new object[1]
      {
        (object) "EntityID"
      }));
    }
    else
    {
      if (e.Row.TargetNoteID.HasValue || !EnumerableExtensions.IsNotIn<string>(e.Row.TargetType, "PX.Objects.CR.Contact", "PX.Objects.CR.BAccount", "PX.Objects.AR.Customer", "PX.Objects.AP.Vendor"))
        return;
      ((Events.Event<PXRowPersistingEventArgs, Events.RowPersisting<CRRelation>>) e).Cache.RaiseExceptionHandling<CRRelation.targetNoteID>((object) e.Row, (object) null, (Exception) new PXSetPropertyException("'{0}' cannot be empty.", new object[1]
      {
        (object) "TargetNoteID"
      }));
    }
  }

  public virtual BAccount GetBusinessAccount(CRRelation relation)
  {
    return BAccount.PK.Find((PXGraph) this.Base, (int?) relation?.EntityID);
  }

  public virtual Contact GetContact(CRRelation relation)
  {
    return Contact.PK.Find((PXGraph) this.Base, (int?) relation?.ContactID);
  }

  public virtual Users GetUser(Contact contact)
  {
    return Users.PK.Find((PXGraph) this.Base, (Guid?) contact?.UserID, (PKFindOptions) 0);
  }

  public virtual void FillUnboundDataForRelation(PXGraph graph, CRRelation relation)
  {
    Contact contact = this.GetContact(relation);
    BAccount businessAccount = this.GetBusinessAccount(relation);
    Users user = this.GetUser(contact);
    (object, System.Type) relatedEntityObject = this.GetRelatedEntityObject(relation);
    this.FillUnboundDataForRelation(relation, contact, businessAccount, user, relatedEntityObject);
  }

  public virtual void FillUnboundDataForRelation(
    CRRelation relation,
    Contact contact,
    BAccount businessAccount,
    Users user,
    (object Entity, System.Type EntityType) relatedEntity)
  {
    CRRelation.FillUnboundData(relation, contact, businessAccount, user);
    if (relatedEntity.Entity == null)
      return;
    (string FieldName, CRRelationDetail Attribute) fieldWithAttribute = this.Base.Caches[relatedEntity.EntityType].GetField_WithAttribute<CRRelationDetail>();
    if (fieldWithAttribute.Attribute == null)
      return;
    CRRelation crRelation1 = relation;
    CRRelation crRelation2 = relation;
    CRRelation crRelation3 = relation;
    CRRelation crRelation4 = relation;
    (string, string, int?, DateTime?) values = fieldWithAttribute.Attribute.GetValues(this.Base.Caches[relatedEntity.EntityType], relatedEntity.Entity);
    string str = values.Item1;
    crRelation1.Status = str;
    crRelation2.Description = values.Item2;
    crRelation3.OwnerID = values.Item3;
    crRelation4.DocumentDate = values.Item4;
  }

  public virtual bool MeetFilter(
    CRRelation relation,
    (object Entity, System.Type EntityType) relatedEntity,
    PXView.PXFilterRowCollection filters)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    CRRelationDetailsExt<TGraph, TMaster, TNoteField>.\u003C\u003Ec__DisplayClass31_0 cDisplayClass310 = new CRRelationDetailsExt<TGraph, TMaster, TNoteField>.\u003C\u003Ec__DisplayClass31_0()
    {
      relation = relation,
      relatedEntity = relatedEntity
    };
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    cDisplayClass310.viewForResultMapping = new PXView((PXGraph) this.Base, false, BqlCommand.CreateInstance(new System.Type[1]
    {
      this.GetFilterView(cDisplayClass310.relatedEntity.EntityType)
    }));
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    PXView pxView = new PXView((PXGraph) this.Base, false, BqlCommand.CreateInstance(new System.Type[1]
    {
      this.GetFilterView(cDisplayClass310.relatedEntity.EntityType)
    }), (Delegate) new PXSelectDelegate((object) cDisplayClass310, __methodptr(\u003CMeetFilter\u003Eg__getItemRecord\u007C0)));
    int num1 = 0;
    int num2 = 0;
    PXFilterRow[] pxFilterRowArray = PXView.PXFilterRowCollection.op_Implicit(filters);
    ref int local1 = ref num1;
    ref int local2 = ref num2;
    return pxView.Select((object[]) null, (object[]) null, (object[]) null, (string[]) null, (bool[]) null, pxFilterRowArray, ref local1, 0, ref local2).Count > 0;
  }

  public virtual System.Type GetFilterView(System.Type relatedEntityType)
  {
    System.Type[] typeArray = new System.Type[5]
    {
      typeof (Select2<,>),
      typeof (CRRelation),
      typeof (LeftJoin<,>),
      null,
      null
    };
    System.Type type = relatedEntityType;
    if ((object) type == null)
      type = typeof (CRRelation);
    typeArray[3] = type;
    typeArray[4] = typeof (On<True, Equal<False>>);
    return BqlCommand.Compose(typeArray);
  }

  public virtual (object, System.Type) GetRelatedEntityObject(CRRelation relation)
  {
    if (relation.TargetType == null)
      return ((object) null, (System.Type) null);
    System.Type relatedEntityType = this.GetRelatedEntityType(relation.TargetType);
    return (this.EntityHelperInstance.GetEntityRow(relatedEntityType, relation.TargetNoteID), relatedEntityType);
  }

  public virtual void ClearOtherPrimaryRelations(CRRelation relation)
  {
    foreach (CRRelation crRelation in ((IEnumerable<CRRelation>) ((PXSelectBase<CRRelation>) this.Relations).SelectMain(Array.Empty<object>())).ToList<CRRelation>().Where<CRRelation>((Func<CRRelation, bool>) (item =>
    {
      if (!item.IsPrimary.GetValueOrDefault())
        return false;
      int? relationId1 = item.RelationID;
      int? relationId2 = relation.RelationID;
      return !(relationId1.GetValueOrDefault() == relationId2.GetValueOrDefault() & relationId1.HasValue == relationId2.HasValue);
    })))
    {
      crRelation.IsPrimary = new bool?(false);
      ((PXSelectBase<CRRelation>) this.Relations).Update(crRelation);
    }
    ((PXSelectBase) this.Relations).View.RequestRefresh();
  }
}
