// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.GetLabel
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Linq;

#nullable disable
namespace PX.Objects.Common;

public static class GetLabel
{
  /// <summary>
  /// For the specified BQL constant declared inside an <see cref="T:PX.Objects.Common.ILabelProvider" />-implementing
  /// class, fetches its value and returns the corresponding label.
  /// </summary>
  /// <typeparam name="TConstant">
  /// A BQL constant type declared inside an <see cref="T:PX.Objects.Common.ILabelProvider" />-implementing class.
  /// </typeparam>
  /// <example><code>
  /// string value = GetLabel.For&lt;ARDocType.refund&gt;();
  /// </code></example>
  public static string For<TConstant>() where TConstant : IConstant<string>, IBqlOperand, new()
  {
    Type type = typeof (TConstant);
    Type declaringType = type.DeclaringType;
    if (!(declaringType == (Type) null))
    {
      if (typeof (ILabelProvider).IsAssignableFrom(declaringType))
      {
        try
        {
          return (Activator.CreateInstance(declaringType) as ILabelProvider).GetLabel(new TConstant().Value);
        }
        catch (MissingMethodException ex)
        {
          object[] objArray = new object[1]
          {
            (object) declaringType.Name
          };
          throw new PXException((Exception) ex, "The label provider class '{0}' must have a parameterless constructor.", objArray);
        }
      }
    }
    throw new PXException("The specified constant type '{0}' must be declared inside a class implementing the ILabelProvider interface.", new object[1]
    {
      (object) type.Name
    });
  }

  /// <summary>
  /// For the specified string value, returns the corresponding label as defined by
  /// the specified <see cref="T:PX.Objects.Common.ILabelProvider" />-implementing class.
  /// </summary>
  /// <typeparam name="TLabelProvider">The type of the label provider class, e.g. <see cref="!:ARDocType" />.</typeparam>
  /// <param name="value">The string value for which the label should be obtained.</param>
  /// <example><code>
  /// string value = GetLabel.For&lt;ARDocType&gt;("INV"); // returns "Invoice"
  /// </code></example>
  public static string For<TLabelProvider>(string value) where TLabelProvider : ILabelProvider, new()
  {
    return new TLabelProvider().GetLabel(value);
  }

  /// <summary>
  /// For the specified BQL field, returns the label defined for the field value
  /// by an item-level instance of <see cref="T:PX.Data.PXStringListAttribute" /> residing
  /// on the field.
  /// </summary>
  /// <remarks>
  /// This method can be used when the string list is changed dynamically
  /// for individual records.
  /// </remarks>
  public static string For<TField>(PXCache cache, IBqlTable record) where TField : IBqlField
  {
    if (cache == null)
      throw new ArgumentNullException(nameof (cache));
    if (record == null)
      throw new ArgumentNullException(nameof (record));
    if (!(cache.GetValue<TField>((object) record) is string key))
      throw new PXException("The {0} field is not of string type.", new object[1]
      {
        (object) typeof (TField).FullName
      });
    PXStringListAttribute stringListAttribute = cache.GetAttributesReadonly<TField>((object) record).OfType<PXStringListAttribute>().SingleOrDefault<PXStringListAttribute>();
    if (stringListAttribute == null)
      throw new PXException("The {0} field does not have an item-level or cache-level {1}.", new object[2]
      {
        (object) typeof (TField).FullName,
        (object) "PXStringListAttribute"
      });
    string str;
    if (!stringListAttribute.ValueLabelDic.TryGetValue(key, out str))
      throw new PXException("The string list attribute does not define a label for value '{0}'.", new object[1]
      {
        (object) key
      });
    return str;
  }

  /// <summary>
  /// For the specified BQL field and its value, returns the label defined for that
  /// value by a cache-level instance of <see cref="T:PX.Data.PXStringListAttribute" />
  /// residing on the field.
  /// </summary>
  /// <remarks>
  /// This method can be used when the string list is changed dynamically
  /// at the cache level.
  /// </remarks>
  public static string For<TField>(PXCache cache, string fieldValue) where TField : IBqlField
  {
    if (cache == null)
      throw new ArgumentNullException(nameof (cache));
    if (fieldValue == null)
      throw new ArgumentNullException(nameof (fieldValue));
    PXStringListAttribute stringListAttribute = cache.GetAttributesReadonly<TField>().OfType<PXStringListAttribute>().SingleOrDefault<PXStringListAttribute>();
    if (stringListAttribute == null)
      throw new PXException("The {0} field does not have a cache-level {1}.", new object[2]
      {
        (object) typeof (TField).FullName,
        (object) "PXStringListAttribute"
      });
    string str;
    if (!stringListAttribute.ValueLabelDic.TryGetValue(fieldValue, out str))
      throw new PXException("The string list attribute does not define a label for value '{0}'.", new object[1]
      {
        (object) fieldValue
      });
    return str;
  }
}
