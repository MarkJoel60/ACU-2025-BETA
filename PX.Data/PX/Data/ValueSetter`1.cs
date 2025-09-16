// Decompiled with JetBrains decompiler
// Type: PX.Data.ValueSetter`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.ComponentModel;
using System.Linq.Expressions;

#nullable disable
namespace PX.Data;

[ImmutableObject(true)]
public readonly struct ValueSetter<TEntity> where TEntity : class, IBqlTable, new()
{
  private readonly PXCache _cache;
  private readonly TEntity _entity;
  private readonly bool _fireFieldEvents;

  internal ValueSetter(PXCache cache, TEntity entity, bool fireFieldEvents)
  {
    this._cache = cache;
    this._entity = entity;
    this._fireFieldEvents = fireFieldEvents;
  }

  public void Set<TValue>(Expression<Func<TEntity, TValue>> fieldSelector, TValue value)
  {
    this.Set(((MemberExpression) fieldSelector.Body).Member.Name, (object) value);
  }

  private void Set(string fieldName, object value)
  {
    if (this._fireFieldEvents)
      this._cache.SetValueExt((object) this._entity, fieldName, value);
    else
      this._cache.SetValue((object) this._entity, fieldName, value);
  }

  public ValueSetter<TEntity> WithEventFiring
  {
    get => new ValueSetter<TEntity>(this._cache, this._entity, true);
  }

  public ValueSetter<TEntity>.Ext<TExt> With<TExt>() where TExt : PXCacheExtension<TEntity>
  {
    return new ValueSetter<TEntity>.Ext<TExt>(this);
  }

  [ImmutableObject(true)]
  public readonly struct Ext<TExt> where TExt : PXCacheExtension<TEntity>
  {
    private readonly ValueSetter<TEntity> _parent;

    internal Ext(ValueSetter<TEntity> parent) => this._parent = parent;

    public void Set<TValue>(Expression<Func<TExt, TValue>> fieldSelector, TValue value)
    {
      this._parent.Set(((MemberExpression) fieldSelector.Body).Member.Name, (object) value);
    }

    public void Set<TValue>(Expression<Func<TEntity, TValue>> fieldSelector, TValue value)
    {
      this._parent.Set(((MemberExpression) fieldSelector.Body).Member.Name, (object) value);
    }

    public ValueSetter<TEntity>.Ext<TExt> WithEventFiring
    {
      get => new ValueSetter<TEntity>.Ext<TExt>(this._parent.WithEventFiring);
    }
  }
}
