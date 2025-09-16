// Decompiled with JetBrains decompiler
// Type: PX.Data.PXCustomizeBaseAttributeAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;
using System.Reflection;

#nullable disable
namespace PX.Data;

/// <summary>The <tt>PXCustomizeBaseAttribute</tt> attribute is added to the field in a DAC extension for each modified parameter.</summary>
/// <example><para>For example, to set the Enabled parameter of the PXUIField attribute of a field to false, add the following attribute to the field region of the DAC extension.</para>
/// <code title="Example" lang="CS">
/// [PXCustomizeBaseAttribute(typeof(PXUIFieldAttribute), "Enabled", false)]</code>
/// <code title="Example2" description="The following example shows how to change the Required parameter of the PXUIField attribute for the MyField field of the MyDAC data access class in the MyGraph graph extension by using the CacheAttached() event handler." groupname="Example" lang="CS">
/// public class MyGraph_Extension:PXGraphExtension&lt;MyGraph&gt;
/// {
/// ...
///  [PXCustomizeBaseAttribute(typeof(PXUIFieldAttribute), "Required", true)]
///   protected void MyDAC_MyField_CacheAttached(PXCache cache)
///   {
///   }
/// ...
/// }</code>
/// </example>
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, AllowMultiple = true)]
public class PXCustomizeBaseAttributeAttribute : Attribute
{
  /// <summary>Gets the target attribute.</summary>
  public System.Type TargetAttribute { get; private set; }

  /// <summary>Gets a property name.</summary>
  public string PropertyName { get; private set; }

  /// <summary>Gets the value. </summary>
  public object Value { get; private set; }

  public PXCustomizeBaseAttributeAttribute(System.Type targetAttribute, string propertyName, object value)
  {
    this.TargetAttribute = targetAttribute;
    this.PropertyName = propertyName;
    this.Value = value;
  }

  /// <exclude />
  public virtual void Apply(List<PXEventSubscriberAttribute> result)
  {
    PropertyInfo property = this.TargetAttribute.GetProperty(this.PropertyName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
    if (property == (PropertyInfo) null)
      throw new PXException($"Cannot find the property {this.PropertyName} in the attribute {this.TargetAttribute.FullName}.");
    foreach (PXEventSubscriberAttribute o in result)
    {
      if (this.TargetAttribute.IsInstanceOfType((object) o))
      {
        property.SetValue((object) o, this.Value);
        break;
      }
    }
  }
}
