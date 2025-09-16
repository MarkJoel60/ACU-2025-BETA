// Decompiled with JetBrains decompiler
// Type: PX.Data.PXAggregateAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Collections.Generic;

#nullable disable
namespace PX.Data;

public class PXAggregateAttribute : PXEventSubscriberAttribute
{
  /// <summary>The collection of the attributes combined in the current attribute.</summary>
  protected PXAggregateAttribute.AggregatedAttributesCollection _Attributes;

  internal List<PXEventSubscriberAttribute> InternalAttributesAccessor
  {
    get => (List<PXEventSubscriberAttribute>) this._Attributes;
  }

  /// <summary>
  /// Initializes a new instance of the attribute; pulls the
  /// <tt>PXEventSubscriberAttribute</tt>-derived attributes placed on the
  /// current attribute and adds them to the collection of aggregated attributes.
  /// </summary>
  public PXAggregateAttribute()
  {
    this._Attributes = new PXAggregateAttribute.AggregatedAttributesCollection();
    foreach (PXEventSubscriberAttribute customAttribute in this.GetType().GetCustomAttributes(typeof (PXEventSubscriberAttribute), true))
      this._Attributes.Add(customAttribute);
  }

  /// <exclude />
  public PXEventSubscriberAttribute[] GetAttributes() => this._Attributes.ToArray();

  /// <exclude />
  public PXEventSubscriberAttribute[] GetAggregatedAttributes()
  {
    return this._Attributes.AggregatedAttributes;
  }

  /// <exclude />
  public T GetAttribute<T>() where T : class
  {
    foreach (PXEventSubscriberAttribute attribute in (List<PXEventSubscriberAttribute>) this._Attributes)
    {
      if (attribute is T)
        return attribute as T;
    }
    return default (T);
  }

  /// <exclude />
  public override PXEventSubscriberAttribute Clone(PXAttributeLevel attributeLevel)
  {
    PXAggregateAttribute aggregateAttribute = (PXAggregateAttribute) base.Clone(attributeLevel);
    if (attributeLevel == PXAttributeLevel.Item)
    {
      this._Attributes = new PXAggregateAttribute.AggregatedAttributesCollection((IEnumerable<PXEventSubscriberAttribute>) this._Attributes);
      return (PXEventSubscriberAttribute) aggregateAttribute;
    }
    aggregateAttribute._Attributes = new PXAggregateAttribute.AggregatedAttributesCollection(this._Attributes.Count);
    foreach (PXEventSubscriberAttribute attribute in (List<PXEventSubscriberAttribute>) this._Attributes)
      aggregateAttribute._Attributes.Add(attribute.Clone(attributeLevel));
    return (PXEventSubscriberAttribute) aggregateAttribute;
  }

  /// <exclude />
  internal override void InjectAttributeDependencies(PXCache cache)
  {
    base.InjectAttributeDependencies(cache);
    foreach (PXEventSubscriberAttribute attribute in (List<PXEventSubscriberAttribute>) this._Attributes)
      attribute.InjectAttributeDependencies(cache);
  }

  /// <exclude />
  public override void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    foreach (PXEventSubscriberAttribute attribute in (List<PXEventSubscriberAttribute>) this._Attributes)
      attribute.CacheAttached(sender);
  }

  /// <summary>Gets or sets the DAC associated with the attribute. The
  /// setter also sets the provided value as <tt>BqlTable</tt> in all
  /// attributes combined in the current attribute.</summary>
  public override System.Type BqlTable
  {
    get => base.BqlTable;
    set
    {
      base.BqlTable = value;
      foreach (PXEventSubscriberAttribute attribute in (List<PXEventSubscriberAttribute>) this._Attributes)
        attribute.BqlTable = value;
    }
  }

  /// <exclude />
  protected internal override void SetBqlTable(System.Type bqlTable)
  {
    base.SetBqlTable(bqlTable);
    foreach (PXEventSubscriberAttribute attribute in (List<PXEventSubscriberAttribute>) this._Attributes)
      attribute.SetBqlTable(bqlTable);
  }

  /// <summary>Gets or sets the name of the field associated with the attribute. The setter also sets the provided value as <tt>FieldName</tt> in all attributes combined in
  /// the current attribute.</summary>
  public override string FieldName
  {
    get => base.FieldName;
    set
    {
      base.FieldName = value;
      foreach (PXEventSubscriberAttribute attribute in (List<PXEventSubscriberAttribute>) this._Attributes)
        attribute.FieldName = value;
    }
  }

  /// <summary>Gets or sets the index of the field associtated with the
  /// attribute. The setter also sets the provided value as
  /// <tt>FieldOrdinal</tt> in all attributes combined in the current
  /// attribute.</summary>
  public override int FieldOrdinal
  {
    get => base.FieldOrdinal;
    set
    {
      base.FieldOrdinal = value;
      foreach (PXEventSubscriberAttribute attribute in (List<PXEventSubscriberAttribute>) this._Attributes)
        attribute.FieldOrdinal = value;
    }
  }

  public override System.Type CacheExtensionType
  {
    get => base.CacheExtensionType;
    set
    {
      base.CacheExtensionType = value;
      foreach (PXEventSubscriberAttribute attribute in (List<PXEventSubscriberAttribute>) this._Attributes)
        attribute.CacheExtensionType = value;
    }
  }

  /// <exclude />
  public override void GetSubscriber<ISubscriber>(List<ISubscriber> subscribers)
  {
    bool flag = this.ChildrenAttributesComeFirstFor<ISubscriber>();
    if (!flag)
      base.GetSubscriber<ISubscriber>(subscribers);
    foreach (PXEventSubscriberAttribute attribute in (List<PXEventSubscriberAttribute>) this._Attributes)
      attribute.GetSubscriber<ISubscriber>(subscribers);
    if (!flag)
      return;
    base.GetSubscriber<ISubscriber>(subscribers);
  }

  protected virtual bool ChildrenAttributesComeFirstFor<ISubscriber>() => false;

  protected class AggregatedAttributesCollection : List<PXEventSubscriberAttribute>
  {
    private PXEventSubscriberAttribute[] _AggregatedAttributes;

    public AggregatedAttributesCollection()
    {
    }

    public AggregatedAttributesCollection(int capacity)
      : base(capacity)
    {
    }

    public AggregatedAttributesCollection(IEnumerable<PXEventSubscriberAttribute> collection)
      : base(collection)
    {
    }

    public new virtual PXEventSubscriberAttribute this[int index]
    {
      get => base[index];
      set
      {
        base[index] = value;
        this._AggregatedAttributes = (PXEventSubscriberAttribute[]) null;
      }
    }

    public new virtual void Add(PXEventSubscriberAttribute item)
    {
      base.Add(item);
      this._AggregatedAttributes = (PXEventSubscriberAttribute[]) null;
    }

    public new virtual void AddRange(IEnumerable<PXEventSubscriberAttribute> collection)
    {
      base.AddRange(collection);
      this._AggregatedAttributes = (PXEventSubscriberAttribute[]) null;
    }

    public virtual void Remove(PXEventSubscriberAttribute item)
    {
      base.Remove(item);
      this._AggregatedAttributes = (PXEventSubscriberAttribute[]) null;
    }

    public new virtual void RemoveAt(int index)
    {
      base.RemoveAt(index);
      this._AggregatedAttributes = (PXEventSubscriberAttribute[]) null;
    }

    public PXEventSubscriberAttribute[] AggregatedAttributes
    {
      get
      {
        if (this._AggregatedAttributes == null)
          this.BuildAggregatedAttributes();
        return this._AggregatedAttributes;
      }
    }

    internal void BuildAggregatedAttributes()
    {
      List<PXEventSubscriberAttribute> subscriberAttributeList = new List<PXEventSubscriberAttribute>();
      foreach (PXEventSubscriberAttribute subscriberAttribute in (List<PXEventSubscriberAttribute>) this)
      {
        subscriberAttributeList.Add(subscriberAttribute);
        if (subscriberAttribute is PXAggregateAttribute aggregateAttribute)
          subscriberAttributeList.AddRange((IEnumerable<PXEventSubscriberAttribute>) aggregateAttribute.GetAggregatedAttributes());
      }
      this._AggregatedAttributes = subscriberAttributeList.ToArray();
    }

    public new void Clear()
    {
      base.Clear();
      this._AggregatedAttributes = (PXEventSubscriberAttribute[]) null;
    }
  }
}
