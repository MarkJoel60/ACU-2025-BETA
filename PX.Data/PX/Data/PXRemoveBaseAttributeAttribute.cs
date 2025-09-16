// Decompiled with JetBrains decompiler
// Type: PX.Data.PXRemoveBaseAttributeAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Data;

/// <summary>
/// The <tt>PXRemoveBaseAttribute</tt> attribute removes the specified attribute. You use this attribute in a DAC extension.
/// </summary>
/// <example><para>For example, to remove the PXUIField attribute of the field, add the following attribute to the field region of the DAC extension.</para>
/// <code title="Example" lang="CS">
/// [PXRemoveBaseAttribute(typeof(PXUIFieldAttribute))]</code>
/// </example>
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, AllowMultiple = true)]
public class PXRemoveBaseAttributeAttribute : Attribute
{
  /// <summary>The attribute to be removed.</summary>
  public System.Type TargetAttribute { get; private set; }

  /// <summary>
  /// If set to <c>false</c>, all the attributes derived from <see cref="P:PX.Data.PXRemoveBaseAttributeAttribute.TargetAttribute" />,
  /// including the one which is <see cref="P:PX.Data.PXRemoveBaseAttributeAttribute.TargetAttribute" /> itself, are removed.
  /// Otherwise only the attribute of exact type <see cref="P:PX.Data.PXRemoveBaseAttributeAttribute.TargetAttribute" /> is removed.
  /// </summary>
  public bool ExactType { get; set; }

  public PXRemoveBaseAttributeAttribute(System.Type targetAttribute)
  {
    this.TargetAttribute = targetAttribute;
  }

  /// <exclude />
  public virtual void Apply(List<PXEventSubscriberAttribute> result)
  {
    result.RemoveAll((Predicate<PXEventSubscriberAttribute>) (_ => !this.ExactType ? this.TargetAttribute.IsInstanceOfType((object) _) : this.TargetAttribute == _.GetType()));
  }
}
