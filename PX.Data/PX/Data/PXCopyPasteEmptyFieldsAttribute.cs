// Decompiled with JetBrains decompiler
// Type: PX.Data.PXCopyPasteEmptyFieldsAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

#nullable disable
namespace PX.Data;

/// <summary>The attribute that specifies the DAC fields whose corresponding elements should remain empty and not be filled in with default values when these fields are empty in the document that is going to be copied and pasted.
/// This attribute is added to the appropriate data view in the corresponding graph.</summary>
/// <example>
/// <para>The fields of the <tt>ARShippingContact</tt> DAC listed below as parameters of the <tt>PXCopyPasteEmptyFields</tt> attribute will remain empty when a document of this type is copied and pasted.</para>
/// <code title="" lang="CS">
/// [PXCopyPasteEmptyFields(
///   typeof(ARShippingContact.fullName),
///   typeof(ARShippingContact.attention),
///   typeof(ARShippingContact.phone1),
///   typeof(ARShippingContact.email))]
/// public
///   PXSelect&lt;ARShippingContact,
///     Where&lt;ARShippingContact.contactID,
///     Equal&lt;Current&lt;ARInvoice.shipContactID&gt;&gt;&gt;&gt; Shipping_Contact;
/// </code>
/// </example>
[AttributeUsage(AttributeTargets.Field)]
public class PXCopyPasteEmptyFieldsAttribute : Attribute
{
  private readonly System.Type[] Fields;

  public PXCopyPasteEmptyFieldsAttribute(params System.Type[] fields) => this.Fields = fields;

  internal static Dictionary<string, string[]> GetRequiredFields(PXGraph graph)
  {
    Dictionary<string, string[]> requiredFields = new Dictionary<string, string[]>();
    List<System.Type> typeList = new List<System.Type>()
    {
      graph.GetType()
    };
    if (graph.Extensions != null)
      typeList.AddRange(((IEnumerable<PXGraphExtension>) graph.Extensions).Select<PXGraphExtension, System.Type>((Func<PXGraphExtension, System.Type>) (graphExtension => graphExtension.GetType())));
    foreach (System.Type type in typeList)
    {
      foreach (System.Reflection.FieldInfo element in ((IEnumerable<System.Reflection.FieldInfo>) type.GetFields(BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public)).Where<System.Reflection.FieldInfo>((Func<System.Reflection.FieldInfo, bool>) (fieldView => fieldView.FieldType.IsSubclassOf(typeof (PXSelectBase)))))
      {
        PXCopyPasteEmptyFieldsAttribute customAttribute = (PXCopyPasteEmptyFieldsAttribute) Attribute.GetCustomAttribute((MemberInfo) element, typeof (PXCopyPasteEmptyFieldsAttribute));
        if (customAttribute != null)
          requiredFields.Add(element.Name, ((IEnumerable<System.Type>) customAttribute.Fields).Select<System.Type, string>((Func<System.Type, string>) (requiredField => requiredField.Name)).ToArray<string>());
      }
    }
    return requiredFields;
  }
}
