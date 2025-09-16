// Decompiled with JetBrains decompiler
// Type: PX.Data.PXDisableCloneAttributesAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data;

/// <summary>
/// Disables cloning of the cache-level attributes for a DAC.
/// </summary>
/// <remarks>
/// <para>The attribute is placed on a DAC to prevent creation of
/// item-level attributes of a cache. The cache creates item-level
/// attributes by copying cache-level attributes, for example,
/// when an attribute is modified for a specific data record.</para>
/// <para>The attribute is not used with DACs whose instances
/// (data records) can be modified in the UI. Typically, you place
/// the attribute on DACs representing history and status tables
/// used in processing operations and accumulator attributes.</para>
/// </remarks>
/// <example>
/// <para>The code below shows the usage of the <tt>PXDisableCloneAttributes</tt>
/// attribute on a DAC.</para>
/// <code>
/// [ItemStatsAccumulator()]
/// [PXDisableCloneAttributes()]
/// public partial class ItemStats : INItemStats
/// { ... }
/// </code>
/// </example>
[AttributeUsage(AttributeTargets.Class)]
public class PXDisableCloneAttributesAttribute : PXClassAttribute
{
  /// <exclude />
  public override void CacheAttached(PXCache sender) => sender.DisableCloneAttributes = true;
}
