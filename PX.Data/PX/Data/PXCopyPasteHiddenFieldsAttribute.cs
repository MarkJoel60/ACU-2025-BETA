// Decompiled with JetBrains decompiler
// Type: PX.Data.PXCopyPasteHiddenFieldsAttribute
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

/// <summary>Indicates that the specific fields of the primary DAC of the data view are not copied when the copy-paste feature is utilized on the webpage.</summary>
/// <remarks>See the <see cref="T:PX.Data.PXCopyPasteHiddenViewAttribute">PXCopyPasteHiddenView</see>
/// attribute for more detail.</remarks>
/// <example><para>The code below prevents only the InvoiceNbr field of the APInvoice DAC from copying when a user clicks Copy on the webpage.</para>
///   <code title="Example" lang="CS">
/// [PXCopyPasteHiddenFields(typeof(APInvoice.invoiceNbr))]
/// public PXSelectJoin&lt;APInvoice,
///     LeftJoin&lt;Vendor, On&lt;Vendor.bAccountID, Equal&lt;APInvoice.vendorID&gt;&gt;&gt;&gt;
/// Document;</code>
///   <code title="Example2" description="Multiple fields can be listed, as the following code shows." lang="CS">
/// [PXCopyPasteHiddenFields(typeof(GLTranDoc.parentLineNbr),
///                          typeof(GLTranDoc.curyDiscAmt),
///                          typeof(GLTranDoc.extRefNbr))]
/// public PXSelect&lt;GLTranDoc,
///     Where&lt;GLTranDoc.module, Equal&lt;Current&lt;GLDocBatch.module&gt;&gt;,
///         And&lt;GLTranDoc.batchNbr, Equal&lt;Current&lt;GLDocBatch.batchNbr&gt;&gt;&gt;&gt;,
///     OrderBy&lt;Asc&lt;GLTranDoc.groupTranID, Asc&lt;GLTranDoc.lineNbr&gt;&gt;&gt;&gt;
///         GLTranModuleBatNbr;</code>
/// </example>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Field, AllowMultiple = false)]
public class PXCopyPasteHiddenFieldsAttribute : Attribute
{
  /// <exclude />
  private readonly System.Type[] Fields;
  /// <exclude />
  public System.Type[] FieldsToShowInSimpleImport;

  /// <summary>
  /// Initializes a new instance of the <tt>PXCopyPasteHiddenFields</tt>
  /// attribute.
  /// </summary>
  /// <param name="fields">The DAC fields that are not copied
  /// when the user utilize the copy-paste feature.</param>
  /// <example>
  /// The code below shows the usage of the attribute on the <tt>CurrentDocument</tt>
  /// data view defined in a graph.
  /// <code>
  /// [PXCopyPasteHiddenFields(typeof(APInvoice.paySel), typeof(APInvoice.payDate))]
  /// public PXSelect&lt;APInvoice,
  ///     Where&lt;APInvoice.docType, Equal&lt;Current&lt;APInvoice.docType&gt;&gt;,
  ///         And&lt;APInvoice.refNbr, Equal&lt;Current&lt;APInvoice.refNbr&gt;&gt;&gt;&gt;&gt; CurrentDocument;
  /// </code>
  /// </example>
  public PXCopyPasteHiddenFieldsAttribute(params System.Type[] fields) => this.Fields = fields;

  /// <exclude />
  /// <param name="g">The graph type where the data view is defined.</param>
  /// <param name="viewName">The data view that is checked for the <tt>PXCopyPasteHiddenFields</tt> attribute.</param>
  /// <param name="fieldName"></param>
  /// <param name="isImportSimple"></param>
  /// <returns></returns>
  internal static bool IsHiddenField(
    PXGraph g,
    string viewName,
    string fieldName,
    bool isImportSimple)
  {
    List<PXCopyPasteHiddenFieldsAttribute> hiddenFieldsAttributeList = new List<PXCopyPasteHiddenFieldsAttribute>();
    List<System.Type> typeList = new List<System.Type>()
    {
      g.GetType()
    };
    foreach (PXGraphExtension pxGraphExtension in g.Extensions ?? new PXGraphExtension[0])
      typeList.Add(pxGraphExtension.GetType());
    foreach (System.Type type in typeList)
    {
      System.Reflection.FieldInfo field = type.GetField(viewName, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public);
      if (!(field == (System.Reflection.FieldInfo) null))
      {
        PXCopyPasteHiddenFieldsAttribute customAttribute = (PXCopyPasteHiddenFieldsAttribute) Attribute.GetCustomAttribute((MemberInfo) field, typeof (PXCopyPasteHiddenFieldsAttribute));
        if (customAttribute != null)
          hiddenFieldsAttributeList.Add(customAttribute);
      }
    }
    if (g.Views.ContainsKey(viewName))
    {
      foreach (MemberInfo element in g.Views[viewName].Cache.GetExtensionTypes() ?? new System.Type[0])
      {
        PXCopyPasteHiddenFieldsAttribute customAttribute = (PXCopyPasteHiddenFieldsAttribute) Attribute.GetCustomAttribute(element, typeof (PXCopyPasteHiddenFieldsAttribute));
        if (customAttribute != null)
          hiddenFieldsAttributeList.Add(customAttribute);
      }
    }
    foreach (PXCopyPasteHiddenFieldsAttribute hiddenFieldsAttribute in hiddenFieldsAttributeList)
    {
      if (isImportSimple && hiddenFieldsAttribute.FieldsToShowInSimpleImport != null && ((IEnumerable<System.Type>) hiddenFieldsAttribute.FieldsToShowInSimpleImport).Any<System.Type>((Func<System.Type, bool>) (_ => string.Equals(_.Name, fieldName, StringComparison.OrdinalIgnoreCase))))
        return false;
      if (hiddenFieldsAttribute.Fields != null && ((IEnumerable<System.Type>) hiddenFieldsAttribute.Fields).Any<System.Type>((Func<System.Type, bool>) (_ => string.Equals(_.Name, fieldName, StringComparison.OrdinalIgnoreCase))))
        return true;
    }
    return false;
  }
}
