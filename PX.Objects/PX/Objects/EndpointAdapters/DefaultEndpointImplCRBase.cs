// Decompiled with JetBrains decompiler
// Type: PX.Objects.EndpointAdapters.DefaultEndpointImplCRBase
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Api.ContractBased;
using PX.Api.ContractBased.Adapters;
using PX.Api.ContractBased.Models;
using PX.Data;
using PX.Objects.EP;
using System;
using System.Linq;

#nullable disable
namespace PX.Objects.EndpointAdapters;

internal class DefaultEndpointImplCRBase : IAdapterWithMetadata
{
  public IEntityMetadataProvider MetadataProvider { protected get; set; }

  protected CbApiWorkflowApplicator.CaseApplicator CaseApplicator { get; }

  protected CbApiWorkflowApplicator.OpportunityApplicator OpportunityApplicator { get; }

  protected CbApiWorkflowApplicator.LeadApplicator LeadApplicator { get; }

  public DefaultEndpointImplCRBase(
    CbApiWorkflowApplicator.CaseApplicator caseApplicator,
    CbApiWorkflowApplicator.OpportunityApplicator opportunityApplicator,
    CbApiWorkflowApplicator.LeadApplicator leadApplicator)
  {
    this.CaseApplicator = caseApplicator;
    this.OpportunityApplicator = opportunityApplicator;
    this.LeadApplicator = leadApplicator;
  }

  protected T EnsureAndGetCurrentForInsert<T>(PXGraph graph, Func<T, T> initializer = null) where T : class, IBqlTable, new()
  {
    PXCache cach = graph.Caches[typeof (T)];
    T currentForInsert = initializer == null ? cach.Insert() as T : cach.Insert((object) initializer(cach.CreateInstance() as T)) as T;
    if ((object) currentForInsert == null)
    {
      graph.Clear();
      currentForInsert = initializer == null ? cach.Insert() as T : cach.Insert((object) initializer(cach.CreateInstance() as T)) as T;
    }
    return currentForInsert;
  }

  protected T EnsureAndGetCurrentForUpdate<T>(PXGraph graph) where T : class, IBqlTable, new()
  {
    PXCache cach = graph.Caches[typeof (T)];
    if ((object) (cach.Current as T) == null)
    {
      PXTrace.WriteWarning("No entity in cache for update. Create new entity instead.");
      graph.Clear();
      return cach.Insert() as T;
    }
    cach.Current = (object) (cach.Update(cach.Current) as T);
    return cach.Current as T;
  }

  protected T GetOrCreateInstance<T>(PXGraph graph) where T : class, IBqlTable, new()
  {
    PXCache<T> pxCache = GraphHelper.Caches<T>(graph);
    if ((object) (((PXCache) pxCache).Current as T) != null)
      return ((PXCache) pxCache).Current as T;
    graph.Clear();
    return ((PXCache) GraphHelper.Caches<T>(graph)).CreateInstance() as T;
  }

  protected T Insert<T>(PXGraph graph, T entity) where T : class, IBqlTable, new()
  {
    return GraphHelper.Caches<T>(graph).Insert(entity);
  }

  protected T Update<T>(PXGraph graph, T entity) where T : class, IBqlTable, new()
  {
    return GraphHelper.Caches<T>(graph).Update(entity);
  }

  protected T GetField<T>(EntityImpl impl, string fieldName) where T : EntityField
  {
    return impl.Fields.OfType<T>().FirstOrDefault<T>((Func<T, bool>) (f => string.Equals(f.Name, fieldName, StringComparison.InvariantCultureIgnoreCase)));
  }

  protected EntityValueField GetField(EntityImpl impl, string fieldName)
  {
    return this.GetField<EntityValueField>(impl, fieldName);
  }

  [FieldsProcessed(new string[] {"Type", "Key"})]
  protected virtual void EventAttendee_Insert(
    PXGraph graph,
    EntityImpl entity,
    EntityImpl targetEntity)
  {
    EPAttendee currentForInsert = this.EnsureAndGetCurrentForInsert<EPAttendee>(graph);
    this.EventAttendeeImpl(graph, targetEntity, currentForInsert);
  }

  [FieldsProcessed(new string[] {"Type", "Key"})]
  protected virtual void EventAttendee_Update(
    PXGraph graph,
    EntityImpl entity,
    EntityImpl targetEntity)
  {
    EPAttendee currentForUpdate = this.EnsureAndGetCurrentForUpdate<EPAttendee>(graph);
    this.EventAttendeeImpl(graph, targetEntity, currentForUpdate);
  }

  private void EventAttendeeImpl(PXGraph graph, EntityImpl targetEntity, EPAttendee current)
  {
    EntityValueField field1 = this.GetField(targetEntity, "Type");
    int result1;
    if (field1 != null && int.TryParse(field1.Value, out result1) && result1 == 0)
      return;
    EntityValueField field2 = this.GetField(targetEntity, "Key");
    int result2;
    if (field2 == null || field2.Value == null || !int.TryParse(field2.Value, out result2))
      return;
    PX.Objects.CR.Contact contact = PX.Objects.CR.Contact.PK.Find(graph, new int?(result2));
    if (contact == null)
      return;
    current.ContactID = contact.ContactID;
    GraphHelper.Caches<EPAttendee>(graph).Update(current);
  }
}
