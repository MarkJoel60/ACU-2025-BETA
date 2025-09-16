// Decompiled with JetBrains decompiler
// Type: PX.Data.PXEventSubscriberAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

#nullable disable
namespace PX.Data;

[DebuggerDisplay("{ToString()}")]
public abstract class PXEventSubscriberAttribute : Attribute
{
  internal PXGraphExtension[] Extensions;
  protected System.Type _BqlTable;
  protected string _FieldName;
  protected int _FieldOrdinal = -1;
  internal int IndexInClonesArray = -1;
  public bool IsDirty;
  internal PXEventSubscriberAttribute Prototype;
  protected PXAttributeLevel _AttributeLevel;
  protected System.Type prepCacheExtensionType;
  private System.Type cacheExtensionType;

  protected PXGraphExtension[] GraphExtensions => this.Extensions;

  public override int GetHashCode() => this._FieldOrdinal;

  public override bool Equals(object obj) => this == obj;

  protected PXEventSubscriberAttribute() => PXExtensionManager.InitExtensions((object) this);

  public PXAttributeLevel AttributeLevel => this._AttributeLevel;

  public virtual PXEventSubscriberAttribute Clone(PXAttributeLevel attributeLevel)
  {
    PXEventSubscriberAttribute subscriberAttribute = (PXEventSubscriberAttribute) this.MemberwiseClone();
    subscriberAttribute._AttributeLevel = attributeLevel;
    return subscriberAttribute;
  }

  public virtual System.Type BqlTable
  {
    get => this._BqlTable;
    set
    {
      this._BqlTable = !(this._BqlTable != (System.Type) null) || this._AttributeLevel == PXAttributeLevel.Type ? value : throw new PXException("An attempt to override the base attribute state has been detected.");
    }
  }

  public virtual System.Type CacheExtensionType
  {
    get => this.cacheExtensionType;
    set
    {
      this.cacheExtensionType = value;
      this.prepCacheExtensionType = PXEventSubscriberAttribute.GetBaseCacheWithTableAttr(value);
    }
  }

  /// <summary>
  /// Returns the first cache extension in <see cref="T:PX.Data.PXCacheExtension" /> extension chain, that
  /// has <see cref="T:PX.Data.PXTableAttribute" /> or <see cref="T:PX.Data.PXTableNameAttribute" /> applied.
  /// </summary>
  /// <param name="cache">Cache to start the attribute search from.</param>
  internal static System.Type GetBaseCacheWithTableAttr(System.Type cache)
  {
    if (cache == (System.Type) null)
      return (System.Type) null;
    System.Type cacheWithTableAttr = cache.BaseType;
    while (cacheWithTableAttr != (System.Type) null && (!cacheWithTableAttr.IsDefined(typeof (PXTableAttribute), false) && (!cacheWithTableAttr.IsDefined(typeof (PXTableNameAttribute), false) || !((PXTableNameAttribute) cacheWithTableAttr.GetCustomAttributes(typeof (PXTableNameAttribute), false)[0]).IsActive) || !typeof (PXCacheExtension).IsAssignableFrom(cacheWithTableAttr.BaseType) || !(typeof (PXCacheExtension) != cacheWithTableAttr.BaseType)))
      cacheWithTableAttr = !cacheWithTableAttr.FullName.StartsWith(typeof (PXCacheExtension).FullName) || cacheWithTableAttr.GenericTypeArguments.Length == 0 ? cacheWithTableAttr.BaseType : cacheWithTableAttr.GenericTypeArguments[0];
    return cacheWithTableAttr;
  }

  protected internal virtual void SetBqlTable(System.Type bqlTable)
  {
    if (!typeof (PXCacheExtension).IsAssignableFrom(bqlTable))
    {
      this._BqlTable = bqlTable;
      System.Type baseType;
      while ((typeof (IBqlTable).IsAssignableFrom(baseType = this._BqlTable.BaseType) || baseType.IsDefined(typeof (PXTableAttribute), false) || baseType.IsDefined(typeof (PXTableNameAttribute), false) && ((PXTableNameAttribute) baseType.GetCustomAttributes(typeof (PXTableNameAttribute), false)[0]).IsActive) && (this._FieldName != null && baseType.GetProperty(this._FieldName) != (PropertyInfo) null || !this._BqlTable.IsDefined(typeof (PXTableAttribute), false)) && (!this._BqlTable.IsDefined(typeof (PXTableNameAttribute), false) || !((PXTableNameAttribute) this._BqlTable.GetCustomAttributes(typeof (PXTableNameAttribute), false)[0]).IsActive))
        this._BqlTable = baseType;
    }
    else
      this._BqlTable = bqlTable;
  }

  public virtual string FieldName
  {
    get => this._FieldName;
    set
    {
      if (this._AttributeLevel != PXAttributeLevel.Type)
        throw new PXException("An attempt to override the base attribute state has been detected.");
      this._FieldName = value;
    }
  }

  public virtual int FieldOrdinal
  {
    get => this._FieldOrdinal;
    set
    {
      if (this._AttributeLevel != PXAttributeLevel.Type)
        throw new PXException("An attempt to override the base attribute state has been detected.");
      this._FieldOrdinal = value;
    }
  }

  /// <summary>
  /// Injects dependencies of a certain level inside the attribute's instance using registered <see cref="!:IDependencyInjector">dependency injector</see>.
  /// </summary>
  /// <param name="cache"><see cref="T:PX.Data.PXCache" /> instance that is used to resolve
  /// <see cref="T:PX.Data.PXCache" />- and <see cref="T:PX.Data.PXGraph" />-bound parameters during the injection into cache-level attributes;
  /// <see langword="null" /> for dependencies that are injected into type-level attributes.</param>
  internal virtual void InjectAttributeDependencies(PXCache cache)
  {
    InjectMethods.InjectDependencies(this, cache);
  }

  [MethodImpl(MethodImplOptions.NoInlining)]
  public void InvokeCacheAttached(PXCache cache)
  {
    this.InjectAttributeDependencies(cache);
    if (this is PXAggregateAttribute)
      this.CacheAttached(cache);
    else
      this.CacheAttached(cache);
  }

  public virtual void CacheAttached(PXCache sender)
  {
  }

  public virtual void GetSubscriber<ISubscriber>(List<ISubscriber> subscribers) where ISubscriber : class
  {
    if (!(this is ISubscriber))
      return;
    subscribers.Add(this as ISubscriber);
  }

  public static T CreateInstance<T>(params object[] constructorArgs) where T : PXEventSubscriberAttribute
  {
    return (T) PXEventSubscriberAttribute.CreateInstance(typeof (T), constructorArgs);
  }

  public static PXEventSubscriberAttribute CreateInstance(System.Type t, params object[] constructorArgs)
  {
    t = PXExtensionManager.GetWrapperType(t);
    return constructorArgs == null || constructorArgs.Length == 0 ? (PXEventSubscriberAttribute) Activator.CreateInstance(t) : (PXEventSubscriberAttribute) t.GetConstructor(((IEnumerable<object>) constructorArgs).Select<object, System.Type>((Func<object, System.Type>) (_ => _.GetType())).ToArray<System.Type>()).Invoke(constructorArgs);
  }

  public override string ToString()
  {
    if (this.BqlTable == (System.Type) null || this.FieldName == null)
      return base.ToString();
    return $"{base.ToString()} on {this.BqlTable.FullName}+{this.FieldName}";
  }

  protected internal class ObjectRef<T>
  {
    public T Value;

    public ObjectRef()
    {
    }

    public ObjectRef(T defaultValue) => this.Value = defaultValue;
  }
}
