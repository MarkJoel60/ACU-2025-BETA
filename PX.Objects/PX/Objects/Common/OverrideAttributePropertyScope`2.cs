// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.OverrideAttributePropertyScope`2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.Common;

public abstract class OverrideAttributePropertyScope<TAttribute, TProperty> : IDisposable where TAttribute : PXEventSubscriberAttribute
{
  /// <summary>
  /// Delegate function used to assign the required property of the
  /// specified attribute.
  /// </summary>
  protected Action<TAttribute, TProperty> _setAttributeProperty;
  /// <summary>
  /// Delegate function used to retrieve the required property value from
  /// the specified attribute.
  /// </summary>
  protected Func<TAttribute, TProperty> _getAttributeProperty;
  /// <summary>
  /// Stores the old property values (to be restored upon scope disposal)
  /// for each attribute of type <typeparamref name="TAttribute" /> residing
  /// on a given field.
  /// </summary>
  protected Dictionary<string, TProperty> _oldAttributePropertyValuesByField;

  /// <summary>
  /// The cache object used to obtain attributes of <typeparamref name="TAttribute" /> type.
  /// </summary>
  public PXCache Cache { get; }

  /// <summary>The record fields on which this scope acts.</summary>
  public IEnumerable<Type> Fields { get; }

  protected OverrideAttributePropertyScope(
    PXCache cache,
    IEnumerable<Type> fields,
    Action<TAttribute, TProperty> setAttributeProperty,
    Func<TAttribute, TProperty> getAttributeProperty,
    TProperty overridePropertyValue)
  {
    OverrideAttributePropertyScope<TAttribute, TProperty> attributePropertyScope = this;
    if (cache == null)
      throw new ArgumentNullException(nameof (cache));
    if (setAttributeProperty == null)
      throw new ArgumentNullException(nameof (setAttributeProperty));
    if (getAttributeProperty == null)
      throw new ArgumentNullException(nameof (getAttributeProperty));
    bool flag = fields != null && fields.Any<Type>();
    if (!flag)
      fields = (IEnumerable<Type>) cache.BqlFields;
    this.Cache = cache;
    this.Fields = fields;
    this._setAttributeProperty = setAttributeProperty;
    this._getAttributeProperty = getAttributeProperty;
    this._oldAttributePropertyValuesByField = new Dictionary<string, TProperty>();
    foreach (Type field1 in fields)
    {
      Type field = field1;
      if (!typeof (IBqlField).IsAssignableFrom(field))
        throw new PXException("{0} is not a BqlField.", new object[1]
        {
          (object) field.FullName
        });
      IEnumerable<TAttribute> attributes = this.Cache.GetAttributesReadonly(field.Name).OfType<TAttribute>();
      if (!attributes.Any<TAttribute>())
      {
        if (flag)
          throw new PXException("The {0} field does not have an item-level or cache-level {1}.", new object[2]
          {
            (object) field.FullName,
            (object) typeof (TAttribute).FullName
          });
      }
      else
      {
        this.AssertAttributesCount(attributes, field.FullName);
        EnumerableExtensions.ForEach<TAttribute>(attributes, (Action<TAttribute>) (attribute =>
        {
          closure_0._oldAttributePropertyValuesByField[field.Name] = closure_0._getAttributeProperty(attribute);
          closure_0._setAttributeProperty(attribute, overridePropertyValue);
        }));
      }
    }
  }

  protected virtual void AssertAttributesCount(
    IEnumerable<TAttribute> attributesOfType,
    string fieldName)
  {
    try
    {
      attributesOfType.Single<TAttribute>();
    }
    catch (Exception ex)
    {
      throw new PXException(ex, "An error occurred during processing of the field {0}: {1}.", new object[2]
      {
        (object) fieldName,
        (object) ex.Message
      });
    }
  }

  void IDisposable.Dispose()
  {
    foreach (string key in this._oldAttributePropertyValuesByField.Keys)
    {
      TProperty property = this._oldAttributePropertyValuesByField[key];
      foreach (TAttribute attribute in this.Cache.GetAttributes(key).OfType<TAttribute>())
        this._setAttributeProperty(attribute, property);
    }
  }
}
