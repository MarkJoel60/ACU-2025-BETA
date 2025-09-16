// Decompiled with JetBrains decompiler
// Type: PX.Metadata.XmlCommentsExtensions
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Namotion.Reflection;
using PX.Common;
using System;
using System.Reflection;

#nullable disable
namespace PX.Metadata;

/// <summary>
/// An extension helping to retrieve XML comments from code.
/// </summary>
[PXInternalUseOnly]
public static class XmlCommentsExtensions
{
  /// <summary>
  /// Returns the content of the <see langword="&lt;value&gt;" /> XML documentation tag.
  /// </summary>
  /// <remarks>In general, the <see langword="&lt;value&gt;" /> tag is not applicable to <see cref="T:System.Type" />
  /// but there are some exceptions like BQL fields.</remarks>
  /// <seealso href="https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/xmldoc/value" />
  public static string GetXmlDocsValue(this Type type)
  {
    return XmlDocsExtensions.GetXmlDocsTag((Type) type.GetTypeInfo(), "value", true);
  }

  /// <summary>
  /// Returns the content of the <see langword="&lt;value&gt;" /> XML documentation tag.
  /// </summary>
  /// <seealso href="https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/xmldoc/value" />
  public static string GetXmlDocsValue(this MemberInfo member)
  {
    return XmlDocsExtensions.GetXmlDocsTag(member, "value", true);
  }
}
