// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Extensions.CRUpdateRelatedContactInfoGraphExt`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.CR.Extensions.Cache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

#nullable disable
namespace PX.Objects.CR.Extensions;

/// <exclude />
public abstract class CRUpdateRelatedContactInfoGraphExt<TGraph> : PXGraphExtension<TGraph> where TGraph : PXGraph
{
  private readonly string SLOT_KEY = "CRUpdateRelatedContactInfoGraphExt+" + typeof (TGraph).Name;

  public bool? UpdateRelatedInfo
  {
    get => PXContext.GetSlot<bool?>(this.SLOT_KEY);
    set => PXContext.SetSlot<bool?>(this.SLOT_KEY, value);
  }

  [PXOverride]
  public virtual int Persist(
    System.Type cacheType,
    PXDBOperation operation,
    Func<System.Type, PXDBOperation, int> del)
  {
    this.UpdateRelatedInfo = new bool?(false);
    return del(cacheType, operation);
  }

  /// <summary>
  /// Same as <see cref="M:PX.Objects.CR.Extensions.Cache.CacheExtensions.GetFields_ContactInfo(PX.Data.PXCache)" /> plus validate <see cref="T:PX.Objects.CR.Contact.overrideSalesTerritory" />
  /// and if it is <see langword="false" /> sync <see cref="T:PX.Objects.CR.Contact.salesTerritoryID" />. It could be overridden for customize sync logic.
  /// </summary>
  /// <param name="cache"></param>
  /// <param name="row"></param>
  /// <returns></returns>
  public virtual IEnumerable<string> GetFields_ContactInfoExt(PXCache cache, object row = null)
  {
    if (row != null)
    {
      int num;
      if (!(row is Contact contact))
      {
        num = 0;
      }
      else
      {
        bool? overrideSalesTerritory = contact.OverrideSalesTerritory;
        bool flag = false;
        num = overrideSalesTerritory.GetValueOrDefault() == flag & overrideSalesTerritory.HasValue ? 1 : 0;
      }
      if (num != 0)
        return cache.GetFields_ContactInfo().Union<string>((IEnumerable<string>) new string[1]
        {
          "salesTerritoryID"
        });
    }
    return cache.GetFields_ContactInfo();
  }

  public virtual void UpdateFieldsValuesWithoutPersist(PXCache cache, object source, object target)
  {
    foreach (string str in this.GetFields_ContactInfoExt(cache))
      cache.SetValue(target, str, cache.GetValue(source, str));
  }

  public virtual void UpdateFieldsValuesWithoutPersist(PXResult source, PXResult target)
  {
    foreach (System.Type table in source.Tables)
    {
      object source1 = source[table];
      object target1 = target[table];
      if (source1 != null && target1 != null)
        this.UpdateFieldsValuesWithoutPersist(this.Base.Caches[table], source1, target1);
    }
  }

  public virtual void SetUpdateRelatedInfo<TEntity>(
    Events.RowPersisting<TEntity> eventArgs,
    IEnumerable<string> fieldNames)
    where TEntity : class, IBqlTable, new()
  {
    TEntity original = ((Events.Event<PXRowPersistingEventArgs, Events.RowPersisting<TEntity>>) eventArgs).Cache.GetOriginal((object) eventArgs.Row) as TEntity;
    this.UpdateRelatedInfo = new bool?(this.UpdateRelatedInfo.GetValueOrDefault() || (object) (TEntity) original == null || fieldNames.Any<string>((Func<string, bool>) (fieldName => !object.Equals(((Events.Event<PXRowPersistingEventArgs, Events.RowPersisting<TEntity>>) eventArgs).Cache.GetValue((object) eventArgs.Row, fieldName), ((Events.Event<PXRowPersistingEventArgs, Events.RowPersisting<TEntity>>) eventArgs).Cache.GetValue((object) original, fieldName)))));
  }

  public virtual void SetUpdateRelatedInfo<TEntity, TField>(
    Events.RowPersisting<TEntity> eventArgs,
    bool _ = true)
    where TEntity : class, IBqlTable, new()
    where TField : class, IBqlField
  {
    TEntity original = ((Events.Event<PXRowPersistingEventArgs, Events.RowPersisting<TEntity>>) eventArgs).Cache.GetOriginal((object) eventArgs.Row) as TEntity;
    this.UpdateRelatedInfo = new bool?(this.UpdateRelatedInfo.GetValueOrDefault() || (object) original == null || !object.Equals(((Events.Event<PXRowPersistingEventArgs, Events.RowPersisting<TEntity>>) eventArgs).Cache.GetValue<TField>((object) eventArgs.Row), ((Events.Event<PXRowPersistingEventArgs, Events.RowPersisting<TEntity>>) eventArgs).Cache.GetValue<TField>((object) original)));
  }

  public virtual void UpdateContact<TSelect>(
    PXCache cache,
    object row,
    TSelect select,
    params object[] pars)
    where TSelect : PXSelectBase<Contact>
  {
    ParameterExpression parameterExpression;
    // ISSUE: method reference
    // ISSUE: method reference
    List<int?> list = ((IQueryable<PXResult<Contact>>) select.Select(pars)).Select<PXResult<Contact>, int?>(Expression.Lambda<Func<PXResult<Contact>, int?>>((Expression) Expression.Property((Expression) Expression.Call(l, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (PXResult.GetItem)), Array.Empty<Expression>()), (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Contact.get_ContactID))), parameterExpression)).ToList<int?>();
    if (list.Count == 0)
      return;
    this.Update<TSelect, Contact, Contact.contactID>(cache, row, select, list.OfType<object>(), (IEnumerable<PXDataFieldParam>) new PXDataFieldAssign<Contact.grammValidationDateTime>[1]
    {
      new PXDataFieldAssign<Contact.grammValidationDateTime>((object) new DateTime(1900, 1, 1))
    }, (IEnumerable<PXCache>) new PXCache[1]
    {
      this.Base.Caches[typeof (CRLead)]
    });
  }

  public virtual void UpdateAddress<TSelect>(
    PXCache cache,
    object row,
    TSelect select,
    params object[] pars)
    where TSelect : PXSelectBase<Address>
  {
    ParameterExpression parameterExpression;
    // ISSUE: method reference
    // ISSUE: method reference
    List<int?> list = ((IQueryable<PXResult<Address>>) select.Select(pars)).Select<PXResult<Address>, int?>(Expression.Lambda<Func<PXResult<Address>, int?>>((Expression) Expression.Property((Expression) Expression.Call(l, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (PXResult.GetItem)), Array.Empty<Expression>()), (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Address.get_AddressID))), parameterExpression)).ToList<int?>();
    if (list.Count == 0)
      return;
    this.Update<TSelect, Address, Address.addressID>(cache, row, select, list.OfType<object>());
  }

  protected virtual void Update<TSelect, TTable, TIdField>(
    PXCache cache,
    object row,
    TSelect select,
    IEnumerable<object> ids,
    IEnumerable<PXDataFieldParam> appendants = null,
    IEnumerable<PXCache> additionalCachesToCheckUpdatedEntitiesById = null)
    where TIdField : IBqlField
  {
    IEnumerable<PXDataFieldParam> pxDataFieldParams = this.GetFields_ContactInfoExt(cache, row).Select<string, PXDataFieldAssign>((Func<string, PXDataFieldAssign>) (f => new PXDataFieldAssign(f, cache.GetValue(row, f)))).OfType<PXDataFieldParam>();
    if (appendants != null)
      pxDataFieldParams = pxDataFieldParams.Concat<PXDataFieldParam>(appendants);
    List<PXCache> list = (additionalCachesToCheckUpdatedEntitiesById ?? Enumerable.Empty<PXCache>()).Prepend<PXCache>(cache.Graph.Caches[typeof (TTable)]).ToList<PXCache>();
    foreach (object id in ids)
    {
      bool flag = false;
      foreach (PXCache pxCache in list)
      {
        PXCache relatedCache = pxCache;
        if (!flag)
          flag = relatedCache.Cached.OfType<object>().Where<object>((Func<object, bool>) (o => EnumerableExtensions.IsNotIn<PXEntryStatus>(relatedCache.GetStatus(o), (PXEntryStatus) 0, (PXEntryStatus) 5))).Select<object, object>((Func<object, object>) (o => relatedCache.GetValue<TIdField>(o))).Any<object>(new Func<object, bool>(id.Equals));
        else
          break;
      }
      if (!flag)
      {
        IEnumerable<PXDataFieldParam> source = pxDataFieldParams;
        PXDataFieldRestrict<TIdField> element = new PXDataFieldRestrict<TIdField>(id);
        ((PXDataFieldRestrict) element).OrOperator = true;
        pxDataFieldParams = source.Append<PXDataFieldParam>((PXDataFieldParam) element);
      }
    }
    PXDataFieldParam[] array = pxDataFieldParams.ToArray<PXDataFieldParam>();
    if (!array.OfType<PXDataFieldRestrict>().Any<PXDataFieldRestrict>())
      return;
    PXDatabase.Update(typeof (TTable), array);
  }
}
