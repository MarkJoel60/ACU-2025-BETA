// Decompiled with JetBrains decompiler
// Type: PX.Data.PXBreakInheritanceAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data;

/// <summary>When placed on a derived data access class (DAC), indicates
/// that the cache objects corresponding to the base DACs should not be
/// instantiated.</summary>
/// <example>
/// In the example below, the attribute prevents instantiation of the
/// <tt>INItemStats</tt> cache during instantiation of the
/// <tt>INItemStatsTotal</tt> cache.
/// <code>
/// [PXBreakInheritance]
/// [Serializable]
/// public partial class INItemStatsTotal : INItemStats
/// {
/// ...
/// }</code>
/// </example>
public sealed class PXBreakInheritanceAttribute : Attribute
{
}
