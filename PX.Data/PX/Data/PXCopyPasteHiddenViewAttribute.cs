// Decompiled with JetBrains decompiler
// Type: PX.Data.PXCopyPasteHiddenViewAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;
using System.Reflection;

#nullable disable
namespace PX.Data;

/// <summary>Indicates that the cache corresponding to the primary DAC of
/// the data view is not copied when the copy-paste feature is utilized on
/// the webpage.</summary>
/// <remarks>
/// <para>The attribute is placed on the definition of a data view in a
/// graph to prevent the cache of the first DAC type referenced by the
/// data view to be copied and pasted. The copy-paste feature allows a
/// user to copy all caches related to the graph of the current webpage,
/// add a new data record, and paste all copied caches to the new data
/// record. The <tt>PXCopyPasteHiddenView</tt> attribute hides a cache
/// from this feature.</para>
/// <para>To hide only a specific field from the copy-paste feature, use
/// the <see cref="T:PX.Data.PXCopyPasteHiddenFieldsAttribute">PXCopyPasteHiddenFields</see>
/// attribute.</para>
/// </remarks>
/// <example>
/// The code below shows the use of the attribute on the definition of a
/// data view in a graph. As a result, the <tt>APAdjust</tt> cache is not
/// copied when the user clicks <b>Copy</b> on the webpage bound to the
/// graph where the data view is defined.
/// <code>
/// [PXCopyPasteHiddenView()]
/// public PXSelectJoin&lt;APAdjust,
///     InnerJoin&lt;APPayment, On&lt;APPayment.docType, Equal&lt;APAdjust.adjgDocType&gt;,
///         And&lt;APPayment.refNbr, Equal&lt;APAdjust.adjgRefNbr&gt;&gt;&gt;&gt;&gt; Adjustments;</code>
/// </example>
[AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
public class PXCopyPasteHiddenViewAttribute : Attribute
{
  /// <exclude />
  public bool ShowInSimpleImport;
  /// <exclude />
  public bool IsHidden = true;

  /// <summary>Returns the value indicating whether the attribute is
  /// attached to the specified data view in the graph.</summary>
  /// <exclude />
  /// <param name="g">The graph where the data view is defined.</param>
  /// <param name="viewName">The name of the data view.</param>
  internal static bool IsHiddenView(PXGraph g, string viewName, bool isImportSimple)
  {
    List<System.Type> typeList = new List<System.Type>()
    {
      g.GetType()
    };
    if (g.Extensions != null)
    {
      foreach (PXGraphExtension extension in g.Extensions)
        typeList.Add(extension.GetType());
    }
    bool flag = false;
    foreach (System.Type type in typeList)
    {
      System.Reflection.FieldInfo field = type.GetField(viewName, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public);
      if (!(field == (System.Reflection.FieldInfo) null))
      {
        PXCopyPasteHiddenViewAttribute customAttribute = (PXCopyPasteHiddenViewAttribute) Attribute.GetCustomAttribute((MemberInfo) field, typeof (PXCopyPasteHiddenViewAttribute));
        if (customAttribute != null)
          flag = customAttribute.IsHidden && (!isImportSimple || !customAttribute.ShowInSimpleImport);
      }
    }
    return flag;
  }
}
