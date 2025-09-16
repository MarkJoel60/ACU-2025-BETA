// Decompiled with JetBrains decompiler
// Type: PX.Objects.EndpointAdapters.DefaultEndpointImplCR20
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Api;
using PX.Api.ContractBased;
using PX.Api.ContractBased.Models;
using PX.Data;
using PX.Objects.CR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Compilation;

#nullable disable
namespace PX.Objects.EndpointAdapters;

[PXVersion("20.200.001", "Default")]
internal class DefaultEndpointImplCR20(
  CbApiWorkflowApplicator.CaseApplicator caseApplicator,
  CbApiWorkflowApplicator.OpportunityApplicator opportunityApplicator,
  CbApiWorkflowApplicator.LeadApplicator leadApplicator) : DefaultEndpointImplCRBase(caseApplicator, opportunityApplicator, leadApplicator)
{
  /// <summary>Returns true when specifeid type is a BqlTable object</summary>
  private static bool IsTable(System.Type t)
  {
    return t != (System.Type) null && typeof (IBqlTable).IsAssignableFrom(t) && !typeof (PXMappedCacheExtension).IsAssignableFrom(t);
  }

  [FieldsProcessed(new string[] {"RelatedEntityNoteID", "RelatedEntityType", "RelatedEntityDescription"})]
  protected virtual void Activity_Insert(PXGraph graph, EntityImpl entity, EntityImpl targetEntity)
  {
    this.Activity_Insert_Impl<CRActivity>(graph, entity, targetEntity);
  }

  [FieldsProcessed(new string[] {"RelatedEntityNoteID", "RelatedEntityType", "RelatedEntityDescription"})]
  protected virtual void Activity_Update(PXGraph graph, EntityImpl entity, EntityImpl targetEntity)
  {
    this.Activity_Update_Impl<CRActivity>(graph, entity, targetEntity);
  }

  [FieldsProcessed(new string[] {"RelatedEntityNoteID", "RelatedEntityType", "RelatedEntityDescription"})]
  protected virtual void Email_Insert(PXGraph graph, EntityImpl entity, EntityImpl targetEntity)
  {
    this.Activity_Insert_Impl<CRSMEmail>(graph, entity, targetEntity);
  }

  [FieldsProcessed(new string[] {"RelatedEntityNoteID", "RelatedEntityType", "RelatedEntityDescription"})]
  protected virtual void Email_Update(PXGraph graph, EntityImpl entity, EntityImpl targetEntity)
  {
    this.Activity_Update_Impl<CRSMEmail>(graph, entity, targetEntity);
  }

  [FieldsProcessed(new string[] {"RelatedEntityNoteID", "RelatedEntityType", "RelatedEntityDescription"})]
  protected virtual void Task_Insert(PXGraph graph, EntityImpl entity, EntityImpl targetEntity)
  {
    this.Activity_Insert_Impl<CRActivity>(graph, entity, targetEntity);
  }

  [FieldsProcessed(new string[] {"RelatedEntityNoteID", "RelatedEntityType", "RelatedEntityDescription"})]
  protected virtual void Task_Update(PXGraph graph, EntityImpl entity, EntityImpl targetEntity)
  {
    this.Activity_Update_Impl<CRActivity>(graph, entity, targetEntity);
  }

  [FieldsProcessed(new string[] {"RelatedEntityNoteID", "RelatedEntityType", "RelatedEntityDescription"})]
  protected virtual void Event_Insert(PXGraph graph, EntityImpl entity, EntityImpl targetEntity)
  {
    this.Activity_Insert_Impl<CRActivity>(graph, entity, targetEntity);
  }

  [FieldsProcessed(new string[] {"RelatedEntityNoteID", "RelatedEntityType", "RelatedEntityDescription"})]
  protected virtual void Event_Update(PXGraph graph, EntityImpl entity, EntityImpl targetEntity)
  {
    this.Activity_Update_Impl<CRActivity>(graph, entity, targetEntity);
  }

  private void Activity_Insert_Impl<T>(PXGraph graph, EntityImpl entity, EntityImpl targetEntity) where T : CRActivity, new()
  {
    if (entity != targetEntity)
      return;
    T currentForInsert = this.EnsureAndGetCurrentForInsert<T>(graph);
    this.Activity_Insert_Update_Impl(graph, (PXCache) GraphHelper.Caches<T>(graph), (CRActivity) currentForInsert, targetEntity);
  }

  private void Activity_Update_Impl<T>(PXGraph graph, EntityImpl entity, EntityImpl targetEntity) where T : CRActivity, new()
  {
    if (entity != targetEntity)
      return;
    T currentForUpdate = this.EnsureAndGetCurrentForUpdate<T>(graph);
    this.Activity_Insert_Update_Impl(graph, (PXCache) GraphHelper.Caches<T>(graph), (CRActivity) currentForUpdate, targetEntity);
  }

  private void Activity_Insert_Update_Impl(
    PXGraph graph,
    PXCache cache,
    CRActivity activity,
    EntityImpl targetEntity)
  {
    string input = this.GetField(targetEntity, "RelatedEntityNoteID")?.Value;
    string typeName = this.GetField(targetEntity, "RelatedEntityType")?.Value;
    Guid refNoteID;
    if (input == null || !Guid.TryParse(input, out refNoteID))
      return;
    Note note = new EntityHelper(graph).SelectNote(new Guid?(refNoteID));
    System.Type type;
    if (note == null)
    {
      if (typeName == null)
      {
        PXTrace.WriteError("Note cannot be found and RelatedEntityType is not a specified");
        return;
      }
      type = PXBuildManager.GetType(typeName, false);
      if (!DefaultEndpointImplCR20.IsTable(type))
      {
        PXTrace.WriteError("Note cannot be found and RelatedEntityType is not a table: {type}", new object[1]
        {
          (object) typeName
        });
        return;
      }
      PXNoteAttribute noteAttribute = EntityHelper.GetNoteAttribute(type, true);
      if (noteAttribute == null || !noteAttribute.ShowInReferenceSelector)
      {
        PXTrace.WriteError("RelatedEntityType is not supported as Related Entity. Type: {type}", new object[1]
        {
          (object) typeName
        });
        return;
      }
      PXNoteAttribute.InsertNoteRecord(graph.Caches[type], refNoteID, "");
    }
    else
      type = PXBuildManager.GetType(note.EntityType, false);
    if (type == typeof (BAccount))
      activity.BAccountID = graph.Select<BAccount>().Where<BAccount>((Expression<Func<BAccount, bool>>) (b => b.NoteID == (Guid?) refNoteID)).Select<BAccount, int?>((Expression<Func<BAccount, int?>>) (b => b.BAccountID)).FirstOrDefault<int?>();
    else if (type == typeof (Contact))
    {
      var data = graph.Select<Contact>().Where<Contact>((Expression<Func<Contact, bool>>) (c => c.NoteID == (Guid?) refNoteID)).Select(c => new
      {
        BAccountID = c.BAccountID,
        ContactID = c.ContactID
      }).FirstOrDefault();
      activity.BAccountID = (int?) data?.BAccountID;
      activity.ContactID = (int?) data?.ContactID;
    }
    activity.RefNoteID = new Guid?(refNoteID);
    activity.RefNoteIDType = typeName;
    cache.Update((object) activity);
  }

  [FieldsProcessed(new string[] {"OpportunityID", "Status", "Subject"})]
  protected virtual void Opportunity_Insert(
    PXGraph graph,
    EntityImpl entity,
    EntityImpl targetEntity)
  {
    if (!(graph is OpportunityMaint))
      return;
    CROpportunity currentForInsert = this.EnsureAndGetCurrentForInsert<CROpportunity>(graph, (Func<CROpportunity, CROpportunity>) (o =>
    {
      o.OpportunityID = entity.Fields.OfType<EntityValueField>().FirstOrDefault<EntityValueField>((Func<EntityValueField, bool>) (f => ((EntityField) f).Name == "OpportunityID"))?.Value;
      o.Subject = entity.Fields.OfType<EntityValueField>().FirstOrDefault<EntityValueField>((Func<EntityValueField, bool>) (f => ((EntityField) f).Name == "Subject"))?.Value;
      return o;
    }));
    this.OpportunityApplicator.ApplyStatusChange(graph, this.MetadataProvider, entity, currentForInsert);
  }

  [FieldsProcessed(new string[] {"Status"})]
  protected virtual void Opportunity_Update(
    PXGraph graph,
    EntityImpl entity,
    EntityImpl targetEntity)
  {
    if (!(graph is OpportunityMaint))
      return;
    CROpportunity currentForUpdate = this.EnsureAndGetCurrentForUpdate<CROpportunity>(graph);
    this.OpportunityApplicator.ApplyStatusChange(graph, this.MetadataProvider, entity, currentForUpdate);
  }

  [FieldsProcessed(new string[] {"CaseID", "Status"})]
  protected virtual void Case_Insert(PXGraph graph, EntityImpl entity, EntityImpl targetEntity)
  {
    if (!(graph is CRCaseMaint))
      return;
    CRCase currentForInsert = this.EnsureAndGetCurrentForInsert<CRCase>(graph, (Func<CRCase, CRCase>) (o =>
    {
      o.CaseCD = entity.Fields.OfType<EntityValueField>().FirstOrDefault<EntityValueField>((Func<EntityValueField, bool>) (f => ((EntityField) f).Name == "CaseID"))?.Value;
      return o;
    }));
    this.CaseApplicator.ApplyStatusChange(graph, this.MetadataProvider, entity, currentForInsert);
  }

  [FieldsProcessed(new string[] {"Status"})]
  protected virtual void Case_Update(PXGraph graph, EntityImpl entity, EntityImpl targetEntity)
  {
    if (!(graph is CRCaseMaint))
      return;
    CRCase currentForUpdate = this.EnsureAndGetCurrentForUpdate<CRCase>(graph);
    this.CaseApplicator.ApplyStatusChange(graph, this.MetadataProvider, entity, currentForUpdate);
  }

  [FieldsProcessed(new string[] {"ContactID", "Status"})]
  protected virtual void Lead_Insert(PXGraph graph, EntityImpl entity, EntityImpl targetEntity)
  {
    if (!(graph is LeadMaint))
      return;
    CRLead currentForInsert = this.EnsureAndGetCurrentForInsert<CRLead>(graph, (Func<CRLead, CRLead>) (o =>
    {
      string s = entity.Fields.OfType<EntityValueField>().FirstOrDefault<EntityValueField>((Func<EntityValueField, bool>) (f => ((EntityField) f).Name == "ContactID"))?.Value;
      o.ContactID = s == null ? new int?() : new int?(int.Parse(s));
      return o;
    }));
    this.LeadApplicator.ApplyStatusChange(graph, this.MetadataProvider, entity, currentForInsert);
  }

  [FieldsProcessed(new string[] {"Status"})]
  protected virtual void Lead_Update(PXGraph graph, EntityImpl entity, EntityImpl targetEntity)
  {
    if (!(graph is LeadMaint))
      return;
    CRLead currentForUpdate = this.EnsureAndGetCurrentForUpdate<CRLead>(graph);
    this.LeadApplicator.ApplyStatusChange(graph, this.MetadataProvider, entity, currentForUpdate);
  }

  [FieldsProcessed(new string[] {})]
  protected virtual void ActivityDetail_Insert(
    PXGraph graph,
    EntityImpl entity,
    EntityImpl targetEntity)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    DefaultEndpointImplCR20.\u003C\u003Ec__DisplayClass19_0 cDisplayClass190 = new DefaultEndpointImplCR20.\u003C\u003Ec__DisplayClass19_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass190.graph = graph;
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    cDisplayClass190.graph.RowPersisting.AddHandler(this.GetActivitiesViewForError(cDisplayClass190.graph), new PXRowPersisting((object) cDisplayClass190, __methodptr(\u003CActivityDetail_Insert\u003Eb__0)));
  }

  [FieldsProcessed(new string[] {})]
  protected virtual void ActivityDetail_Update(
    PXGraph graph,
    EntityImpl entity,
    EntityImpl targetEntity)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    DefaultEndpointImplCR20.\u003C\u003Ec__DisplayClass20_0 cDisplayClass200 = new DefaultEndpointImplCR20.\u003C\u003Ec__DisplayClass20_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass200.graph = graph;
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    cDisplayClass200.graph.RowPersisting.AddHandler(this.GetActivitiesViewForError(cDisplayClass200.graph), new PXRowPersisting((object) cDisplayClass200, __methodptr(\u003CActivityDetail_Update\u003Eb__0)));
  }

  protected virtual void ActivityDetail_Delete(
    PXGraph graph,
    EntityImpl entity,
    EntityImpl targetEntity)
  {
    throw new PXOuterException(new Dictionary<string, string>(), graph.GetType(), graph.Views[this.GetActivitiesViewForError(graph)].Cache.Current, "An activity can be inserted, updated, or deleted from a contract-based API only through the Activity, Task, Event, or Email top-level entity.");
  }

  protected virtual string GetActivitiesViewForError(PXGraph graph)
  {
    if (graph is IRelatedActivitiesView relatedActivitiesView)
      return relatedActivitiesView.Activities.Name;
    if (((Dictionary<string, PXView>) graph.Views).ContainsKey("Activities"))
      return "Activities";
    PXView pxView;
    return !graph.TypedViews.TryGetValue(typeof (CRActivity), ref pxView) ? graph.PrimaryView : pxView.Name;
  }
}
