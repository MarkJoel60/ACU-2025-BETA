// Decompiled with JetBrains decompiler
// Type: PX.Data.PXDBLiteDefaultAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data;

/// <summary>Provides the default value for a property or parameter. The attribute is used for
/// defaulting from an auto-generated key field.</summary>
/// <remarks>This attribute is obsolete and will be removed in future versions. Use <see cref="T:PX.Data.PXDBDefaultAttribute" /> instead.</remarks>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property, AllowMultiple = true)]
[Obsolete("This attribute is obsolete and will be removed in future versions. Use PXDBDefaultAttribute instead.")]
/// <summary>
/// Defines the default from the item of the <tt>sourceType</tt> type.
/// </summary>
/// <param name="sourceType">The type to get the default value from. If the type implements IBqlField and is nested, the parameter defines the source field as well.</param>
public class PXDBLiteDefaultAttribute(System.Type sourceType) : PXDBDefaultAttribute(sourceType)
{
}
