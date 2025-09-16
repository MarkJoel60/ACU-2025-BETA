// Decompiled with JetBrains decompiler
// Type: PX.Objects.Extensions.ProcessAffectedEntitiesInPrimaryGraphBase`4
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

#nullable enable
namespace PX.Objects.Extensions;

public abstract class ProcessAffectedEntitiesInPrimaryGraphBase<TSelf, TGraph, TEntity, TPrimaryGraphOfEntity> : 
  PXGraphExtension<
  #nullable disable
  TGraph>
  where TSelf : ProcessAffectedEntitiesInPrimaryGraphBase<TSelf, TGraph, TEntity, TPrimaryGraphOfEntity>
  where TGraph : PXGraph
  where TEntity : class, IBqlTable, new()
  where TPrimaryGraphOfEntity : PXGraph, new()
{
  [PXOverride]
  public virtual void Persist(System.Action basePersist)
  {
    IEnumerable<TEntity> affectedEntities = this.GetAffectedEntities();
    IEnumerable<TEntity> lateAffectedEntities = this.GetLatelyAffectedEntities();
    if (lateAffectedEntities != null || affectedEntities.Any<TEntity>())
    {
      if (this.PersistInSameTransaction)
      {
        using (PXTransactionScope transactionScope = new PXTransactionScope())
        {
          basePersist();
          IEnumerable<System.Type> typesOfDirtyCaches = this.ProcessAffectedEntities(lateAffectedEntities == null ? affectedEntities : lateAffectedEntities.Union<TEntity>(affectedEntities, this.Base.Caches<TEntity>().GetComparer<TEntity>()));
          transactionScope.Complete();
          this.ClearCaches((PXGraph) this.Base, typesOfDirtyCaches);
        }
      }
      else
      {
        this.Base.OnAfterPersist += new System.Action<PXGraph>(OnAfterPersistHandler);
        basePersist();
      }
    }
    else
      basePersist();

    void OnAfterPersistHandler(PXGraph graph)
    {
      graph.OnAfterPersist -= new System.Action<PXGraph>(OnAfterPersistHandler);
      IEnumerable<System.Type> typesOfDirtyCaches = graph.FindImplementation<TSelf>().ProcessAffectedEntities(lateAffectedEntities == null ? affectedEntities : lateAffectedEntities.Union<TEntity>(affectedEntities, this.Base.Caches<TEntity>().GetComparer<TEntity>()));
      this.ClearCaches(graph, typesOfDirtyCaches);
    }
  }

  protected virtual IEnumerable<TEntity> GetAffectedEntities()
  {
    return (IEnumerable<TEntity>) this.Base.Caches<TEntity>().Updated.Cast<TEntity>().Where<TEntity>(new Func<TEntity, bool>(this.EntityIsAffected)).ToArray<TEntity>();
  }

  protected virtual IEnumerable<TEntity> GetLatelyAffectedEntities() => (IEnumerable<TEntity>) null;

  protected IEnumerable<System.Type> ProcessAffectedEntities(IEnumerable<TEntity> affectedEntities)
  {
    HashSet<System.Type> typeSet = new HashSet<System.Type>();
    List<TEntity> list = affectedEntities.ToList<TEntity>();
    if (list.Count != 0)
    {
      TPrimaryGraphOfEntity instance = PXGraph.CreateInstance<TPrimaryGraphOfEntity>();
      foreach (TEntity entity1 in list)
      {
        TEntity entity2 = this.ActualizeEntity(instance, entity1);
        instance.Caches<TEntity>().Current = (object) entity2;
        this.ProcessAffectedEntity(instance, entity2);
        if (instance.IsDirty)
        {
          foreach (KeyValuePair<System.Type, PXCache> keyValuePair in instance.Caches.Where<KeyValuePair<System.Type, PXCache>>((Func<KeyValuePair<System.Type, PXCache>, bool>) (ch =>
          {
            PXCache pxCache = ch.Value;
            return pxCache != null && pxCache.IsDirty;
          })))
            typeSet.Add(keyValuePair.Key);
          PXAction pxAction = (PXAction) instance.Actions.Values.OfType<PXSave<TEntity>>().FirstOrDefault<PXSave<TEntity>>();
          if (pxAction != null)
            pxAction.Press();
          else
            instance.Persist();
          instance.Clear();
        }
      }
      this.OnProcessed(instance);
    }
    return (IEnumerable<System.Type>) typeSet;
  }

  protected virtual void OnProcessed(TPrimaryGraphOfEntity foreignGraph)
  {
  }

  public void ClearCaches(PXGraph graph, IEnumerable<System.Type> typesOfDirtyCaches)
  {
    if (this.ClearAffectedCaches)
    {
      foreach (System.Type typesOfDirtyCach in typesOfDirtyCaches)
      {
        if (graph.Caches.Keys.Contains<System.Type>(typesOfDirtyCach))
          graph.Caches[typesOfDirtyCach].Clear();
      }
    }
    this.ClearCaches(graph);
    graph.SelectTimeStamp();
  }

  protected bool WhenAnyFieldIsAffected(
    TEntity entity,
    params Expression<Func<TEntity, object>>[] fields)
  {
    PXCache<TEntity> entityCache = this.Base.Caches<TEntity>();
    TEntity origin = entityCache.GetOriginal(entity);
    return ((IEnumerable<Expression<Func<TEntity, object>>>) fields).Select<Expression<Func<TEntity, object>>, string>((Func<Expression<Func<TEntity, object>>, string>) (f => ExtractFieldName(f.Body))).Select<string, (object, object)>((Func<string, (object, object)>) (fn => (entityCache.GetValue((object) origin, fn), entityCache.GetValue((object) entity, fn)))).Any<(object, object)>((Func<(object, object), bool>) (p => !object.Equals(p.OriginValue, p.CurrentValue)));

    static string ExtractFieldName(Expression exp)
    {
      return !(exp is MemberExpression memberExpression) ? ExtractFieldName(((UnaryExpression) exp).Operand) : memberExpression.Member.Name;
    }
  }

  protected virtual void ClearCaches(PXGraph graph)
  {
  }

  protected virtual bool ClearAffectedCaches => true;

  protected abstract bool PersistInSameTransaction { get; }

  protected abstract bool EntityIsAffected(TEntity entity);

  protected abstract void ProcessAffectedEntity(TPrimaryGraphOfEntity primaryGraph, TEntity entity);

  protected virtual TEntity ActualizeEntity(TPrimaryGraphOfEntity primaryGraph, TEntity entity)
  {
    return entity;
  }
}
