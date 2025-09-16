// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.DuplicatesSearchEngineBase`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.Common;

[PXInternalUseOnly]
public abstract class DuplicatesSearchEngineBase<TEntity> where TEntity : class, IBqlTable, new()
{
  protected readonly PXCache _cache;
  protected readonly Type[] _keyFields;
  protected readonly KeyValuesComparer<TEntity> _comparator;
  protected readonly TEntity _template;

  public DuplicatesSearchEngineBase(PXCache cache, IEnumerable<Type> keyFields)
  {
    this._cache = cache;
    this._keyFields = keyFields.ToArray<Type>();
    this._comparator = new KeyValuesComparer<TEntity>(cache, keyFields);
    this._template = (TEntity) this._cache.CreateInstance();
  }

  public virtual TEntity CreateEntity(IDictionary itemValues, params Type[] additionalFields)
  {
    foreach (Type type in NonGenericIEnumerableExtensions.Concat_((IEnumerable) this._keyFields, (IEnumerable) additionalFields))
    {
      string field = this._cache.GetField(type);
      object obj;
      if (itemValues.Contains((object) field))
        obj = itemValues[(object) field];
      else
        this._cache.RaiseFieldDefaulting(field, (object) this._template, ref obj);
      if (obj != null)
      {
        try
        {
          if (!this._cache.RaiseFieldUpdating(field, (object) this._template, ref obj))
            obj = (object) null;
        }
        catch (PXSetPropertyException ex)
        {
          obj = (object) null;
        }
      }
      this._cache.SetValue((object) this._template, type.Name, obj);
    }
    return this._template;
  }

  public virtual TEntity Find(IDictionary itemValues) => this.Find(this.CreateEntity(itemValues));

  public abstract TEntity Find(TEntity item);

  public abstract void AddItem(TEntity item);
}
