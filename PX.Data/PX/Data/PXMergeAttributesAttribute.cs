// Decompiled with JetBrains decompiler
// Type: PX.Data.PXMergeAttributesAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Data;

/// <summary>
/// You use the <tt>PXMergeAttributes</tt> attribute to specify for each field how to apply custom attributes to the existing ones in a DAC extension.
/// </summary>
/// <example><para>For example, suppose that for the customized FieldName field, you have to add the PXDefault attribute and change the value in the PXUIFieldAttribute. Suppose the original code of the FieldName field is the following.</para>
/// <code title="Example" lang="CS">
/// #region FieldName
///   ...
///  [PXDBDecimal(2)]
///  [PXUIField(DisplayName = "Display Name")]
///  public virtual decimal? FieldName
///  {...}
/// #endregion</code>
/// <para>In the DAC extension code for the customized field, specify the Merge option, as shown below.</para>
/// <code title="Example2" groupname="Example" lang="CS">
/// public class DACName_Extension: PXCacheExtension&lt;DACName&gt;
/// {
///  #region FieldName
/// 
///   [PXMergeAttributes(Method = MergeMethod.Merge)]
/// 
///   [PXDefault(TypeCode.Decimal, "0.0")]
///   [PXUIField(DisplayName = "Name")]
/// 
///   public string FieldName{get;set;}
/// 
///  #endregion
/// }</code>
/// <para>The system will merge attributes for the FieldName field. In the result of the merge, the FieldName field will have the following collection of attributes.</para>
/// <code title="Example3" lang="CS">
/// ...
/// [PXDBDecimal(2)]
/// [PXDefault(TypeCode.Decimal, "0.0")]
/// [PXUIField(DisplayName = "Name")]
/// public virtual decimal? FieldName
/// {...}</code>
/// </example>
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, AllowMultiple = false)]
public class PXMergeAttributesAttribute : Attribute
{
  /// <summary>Gets or sets a merge method that was selected in the <tt>MergeMethod</tt> enumeration.</summary>
  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "DisplayName", 1)]
  public MergeMethod Method { get; set; }

  /// <exclude />
  public virtual void Apply(
    List<PXEventSubscriberAttribute> result,
    PXEventSubscriberAttribute[] current)
  {
    if (this.Method == MergeMethod.Replace)
    {
      result.Clear();
      result.AddRange((IEnumerable<PXEventSubscriberAttribute>) current);
    }
    else if (this.Method == MergeMethod.Append)
    {
      result.AddRange((IEnumerable<PXEventSubscriberAttribute>) current);
    }
    else
    {
      if (this.Method != MergeMethod.Merge)
        return;
      foreach (PXEventSubscriberAttribute subscriberAttribute in current)
      {
        System.Type type1 = subscriberAttribute.GetType();
        bool flag = false;
        for (int index = 0; index < result.Count; ++index)
        {
          System.Type type2 = result[index].GetType();
          flag = type2.IsAssignableFrom(type1) || type1.IsAssignableFrom(type2) || PXAttributeFamilyAttribute.IsSameFamily(type2, type1);
          if (flag)
          {
            result[index] = subscriberAttribute;
            break;
          }
        }
        if (!flag)
          result.Add(subscriberAttribute);
      }
    }
  }
}
