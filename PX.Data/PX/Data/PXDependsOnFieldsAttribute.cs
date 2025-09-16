// Decompiled with JetBrains decompiler
// Type: PX.Data.PXDependsOnFieldsAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

#nullable disable
namespace PX.Data;

/// <summary>Used for calculated DAC fields that contain references to other fields in their property getters. The attribute allows such fields to work properly in reports
/// and Integration Services.</summary>
/// <example>
/// <code title="" description="" lang="CS">
/// //The code below shows definition of a calculated DAC field.
/// //The property getter involves two fields, DocBal and TaxWheld. These two fields
/// //should be specified as parameters of the PXDependsOnFields attribute.
/// [PXDefault(TypeCode.Decimal, "0.0")]
/// [PXUIField(DisplayName = "Balance")]
/// public virtual Decimal? ActualBalance
/// {
///     [PXDependsOnFields(typeof(docBal), typeof(taxWheld))]
///     get
///     {
///         return this.DocBal - this.TaxWheld;
///     }
/// }</code>
/// <code title="Example" lang="CS">
/// //The following code displays two values in one box in the UI:
/// //the number of rejected requests and the percent of this number in all requests.
/// [PXString]
/// [PXUIField(DisplayName = "Nbr. of Declined Web Services API Requests (% of All)")]
/// [PXDependsOnFields(typeof(UsrSMLicenseStatistic.rejectedAPICount), typeof(UsrSMLicenseStatistic.rejectedAPICountP))]
/// public virtual string RejectedAPICountPW
/// {
///     get
///     {
///         return (string.Format("{0} ({1}%)", RejectedAPICount, RejectedAPICountP));
/// }
/// }</code></example>
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, AllowMultiple = false)]
public sealed class PXDependsOnFieldsAttribute : Attribute
{
  private System.Type[] _fields;

  /// <summary>
  /// Initializes an instance of the attribute that makes the field the
  /// attribute is attached to depend on the provided DAC fields.
  /// </summary>
  /// <param name="fields">The fields to depend to.</param>
  public PXDependsOnFieldsAttribute(params System.Type[] fields)
  {
    foreach (System.Type field in fields)
    {
      if (!typeof (IBqlField).IsAssignableFrom(field))
        throw new PXArgumentException("field", "Invalid field: '{0}'", new object[1]
        {
          (object) field.Name
        });
    }
    this._fields = fields;
  }

  internal static HashSet<string> GetDependsRecursive(PXCache table, string field)
  {
    HashSet<string> result = new HashSet<string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    PXDependsOnFieldsAttribute.AddDependsRecursive(table, field, result);
    return result;
  }

  private static void AddDependsRecursive(PXCache table, string field, HashSet<string> result)
  {
    if (result.Contains(field))
      return;
    System.Type itemType = table.GetItemType();
    PropertyInfo element = itemType.GetProperty(field, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public);
    if (element == (PropertyInfo) null)
    {
      foreach (System.Type extensionType in table.GetExtensionTypes())
      {
        PropertyInfo propertyInfo = element;
        if ((object) propertyInfo == null)
          propertyInfo = extensionType.GetProperty(field, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public);
        element = propertyInfo;
      }
      if (element == (PropertyInfo) null)
        return;
    }
    result.Add(element.Name);
    PXDependsOnFieldsAttribute onFieldsAttribute = (PXDependsOnFieldsAttribute) Attribute.GetCustomAttribute((MemberInfo) element, typeof (PXDependsOnFieldsAttribute)) ?? (PXDependsOnFieldsAttribute) Attribute.GetCustomAttribute((MemberInfo) element.GetGetMethod(), typeof (PXDependsOnFieldsAttribute));
    List<System.Type> source = new List<System.Type>();
    if (onFieldsAttribute != null)
      source.AddRange((IEnumerable<System.Type>) onFieldsAttribute._fields);
    foreach (PXEventSubscriberAttribute subscriberAttribute in table.GetAttributesReadonly(element.Name, true))
    {
      if (subscriberAttribute is IPXDependsOnFields pxDependsOnFields)
        source.AddRange((IEnumerable<System.Type>) pxDependsOnFields.GetDependencies(table));
    }
    EnumerableExtensions.ForEach<System.Type>(source.Distinct<System.Type>().Where<System.Type>((Func<System.Type, bool>) (ft => BqlCommand.GetItemType(ft).IsAssignableFrom(itemType))), (System.Action<System.Type>) (ft => PXDependsOnFieldsAttribute.AddDependsRecursive(table, ft.Name, result)));
  }
}
