// Decompiled with JetBrains decompiler
// Type: PX.Data.ReferentialIntegrity.Attributes.Handlers.RestrictedOperationHandlerBase
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api;
using PX.Data.ReferentialIntegrity.Merging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data.ReferentialIntegrity.Attributes.Handlers;

internal abstract class RestrictedOperationHandlerBase
{
  protected static readonly ConcurrentDictionary<Reference, bool> BrokenReferences = new ConcurrentDictionary<Reference, bool>();
  protected readonly ITableMergedReferencesInspector TableMergedReferencesInspector;
  protected readonly PXCache Cache;
  protected readonly object Row;
  protected readonly List<RestrictedOperationHandlerBase.ConstraintViolation> ConstraintViolations = new List<RestrictedOperationHandlerBase.ConstraintViolation>();
  protected readonly PXGraph FKGraph = (PXGraph) PXGraph.CreateInstance<RestrictedOperationHandlerBase.ForeignKeyGraph>();
  private readonly RecordInfoProvider _recordInfoProvider;
  private readonly Dictionary<System.Type, object> _fieldsValuesCache = new Dictionary<System.Type, object>();

  protected RestrictedOperationHandlerBase(
    ITableMergedReferencesInspector tableMergedReferencesInspector,
    PXCache cache,
    object row)
  {
    this.TableMergedReferencesInspector = tableMergedReferencesInspector;
    this.Cache = cache;
    this.Row = row;
    this._recordInfoProvider = new RecordInfoProvider(cache);
  }

  protected RestrictedOperationHandlerBase.ConstraintViolation CreateConstraintViolation(
    Reference reference,
    object foundProblemChild,
    object parent = null)
  {
    return new RestrictedOperationHandlerBase.ConstraintViolation(reference, foundProblemChild, this._recordInfoProvider.GetRecordInfo(foundProblemChild), parent == null ? this._recordInfoProvider.GetParentInfo(reference) : this._recordInfoProvider.GetRecordInfo(parent));
  }

  public void Handle() => this.HandleImpl();

  protected abstract void HandleImpl();

  protected void HandleSingleReference(Reference reference, BqlCommand selectCommand)
  {
    switch (reference.ReferenceBehavior)
    {
      case ReferenceBehavior.NoAction:
        break;
      case ReferenceBehavior.Restrict:
        this.CheckRestriction(reference, selectCommand);
        break;
      case ReferenceBehavior.SetNull:
        this.SetKeyColumnsOfChildrenToNull(reference, selectCommand);
        break;
      case ReferenceBehavior.SetDefault:
        this.SetKeyColumnsOfChildrenToDefault(reference, selectCommand);
        break;
      case ReferenceBehavior.Cascade:
        this.CascadeDeleteChildren(reference, selectCommand);
        break;
      default:
        throw new PXInvalidOperationException("Unknown reference behavior");
    }
  }

  protected abstract void CascadeDeleteChildren(Reference reference, BqlCommand selectCommand);

  protected abstract void CheckRestriction(Reference reference, BqlCommand selectCommand);

  protected abstract void SetKeyColumnsOfChildrenToDefault(
    Reference reference,
    BqlCommand selectCommand);

  protected abstract void SetKeyColumnsOfChildrenToNull(
    Reference reference,
    BqlCommand selectCommand);

  protected bool ForeignKeyCanBeDefaulted(Reference reference, object child)
  {
    return reference.Child.KeyFields.All<System.Type>((Func<System.Type, bool>) (k => this.KeyComponentCanBeDefaulted(k, child)));
  }

  protected bool KeyComponentCanBeDefaulted(System.Type keyComponent, object child)
  {
    PXCache cach = this.FKGraph.Caches[child.GetType()];
    return cach.GetAttributesReadonly(child, keyComponent.Name).OfType<PXDefaultAttribute>().All<PXDefaultAttribute>((Func<PXDefaultAttribute, bool>) (a => a.CanDefault)) & cach.GetAttributesReadonly(child, keyComponent.Name).OfType<PXDBDefaultAttribute>().All<PXDBDefaultAttribute>((Func<PXDBDefaultAttribute, bool>) (a => a.CanDefault));
  }

  protected bool ForeignKeyCanBeNull(Reference reference, object child)
  {
    return reference.Child.KeyFields.All<System.Type>((Func<System.Type, bool>) (k => this.KeyComponentCanBeNull(k, child)));
  }

  protected bool KeyComponentCanBeNull(System.Type keyComponent, object child)
  {
    PXCache cach = this.FKGraph.Caches[child.GetType()];
    return cach.GetAttributesReadonly(child, keyComponent.Name).OfType<PXDefaultAttribute>().All<PXDefaultAttribute>((Func<PXDefaultAttribute, bool>) (a => a.PersistingCheck == PXPersistingCheck.Nothing)) & cach.GetAttributesReadonly(child, keyComponent.Name).OfType<PXDBDefaultAttribute>().All<PXDBDefaultAttribute>((Func<PXDBDefaultAttribute, bool>) (a => a.PersistingCheck == PXPersistingCheck.Nothing));
  }

  protected PXView CreateViewReadonly(BqlCommand selectCommand)
  {
    return this.FKGraph.TypedViews.GetView(selectCommand, true);
  }

  protected PXView CreateView(BqlCommand selectCommand)
  {
    return this.FKGraph.TypedViews.GetView(selectCommand, false);
  }

  protected object[] ExtractKeyValues(Reference reference)
  {
    return this.ChooseSide(reference).KeyFields.Select<System.Type, object>(new Func<System.Type, object>(this.GetAndCacheFieldValue)).ToArray<object>();
  }

  protected abstract TableWithKeys ChooseSide(Reference reference);

  private object GetAndCacheFieldValue(System.Type field)
  {
    return this._fieldsValuesCache.GetOrAdd<System.Type, object>(field, (Func<System.Type, object>) (f => this.Cache.GetValue(this.Row, f.Name)));
  }

  protected void ThrowConstraintViolationException(string msg)
  {
    PXGraph.ThrowWithoutRollback((Exception) new ReferentialIntegrityViolationException(this.ConstraintViolations.Select<RestrictedOperationHandlerBase.ConstraintViolation, Reference>((Func<RestrictedOperationHandlerBase.ConstraintViolation, Reference>) (r => r.Reference)), msg));
  }

  protected class ForeignKeyGraph : PXGraph<RestrictedOperationHandlerBase.ForeignKeyGraph>
  {
  }

  protected struct ConstraintViolation(
    Reference reference,
    object foundProblemEntity,
    string childRecordInfo,
    string parentRecordInfo)
  {
    public readonly Reference Reference = reference;
    public readonly string ChildRecordInfo = childRecordInfo;
    public readonly string ParentRecordInfo = parentRecordInfo;
    public readonly object FoundProblemEntity = foundProblemEntity;
  }
}
