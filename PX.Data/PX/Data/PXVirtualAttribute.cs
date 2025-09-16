// Decompiled with JetBrains decompiler
// Type: PX.Data.PXVirtualAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Reflection;

#nullable disable
namespace PX.Data;

/// <summary>
/// You add this attribute to a DAC class definition to prevent specific DAC data records from being saved to the database.
/// </summary>
/// <remarks>
/// It is mandatory that you invoke the <tt>PXVirtual</tt> statuc constructor by adding the following line of code into either a BLC constuctor or
/// the Initialize method within a BLC extension:
/// <tt>typeof(PX.Data.MassProcess.FieldValue).GetCustomAttributes(typeof(PXVirtualAttribute), false);</tt>
/// </remarks>
/// <example><para>The example below shows a general PXVirtual attribute declaration.</para>
/// <code title="Example" lang="CS">
/// [PXVirtual]
/// [PXCacheName(Messages.TimeCardDetail)]
/// [Serializable]
/// public partial class EPTimeCardSummary : PXBqlTable, IBqlTable
/// { ... }</code>
/// <para>In the following code you invoke a static constructor for the PXVirtual attribute within a BLC constructor.</para>
/// <code title="Example2" lang="CS">
/// public PXGenericInqGrph()
/// {
///     ...
///     typeof(FieldValue).GetCustomAttributes(typeof(PXVirtualAttribute), false);
///     ...
/// }</code>
/// <para>In the following code you invoke a static constructor for the PXVirtual attribute within a BLC initialize method.</para>
/// <code title="Example3" lang="CS">
/// public override void Initialize()
/// {
///     typeof(FieldValue).GetCustomAttributes(typeof(PXVirtualAttribute), false);
///     ...
/// }</code>
/// </example>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public sealed class PXVirtualAttribute : Attribute
{
  static PXVirtualAttribute()
  {
    PXCacheCollection.OnCacheCreated += new Action<PXGraph, PXCache>(PXVirtualAttribute.CacheAttached);
    PXCacheCollection.OnCacheChanged += new Action<PXGraph, PXCache>(PXVirtualAttribute.CacheAttached);
  }

  private static void CacheAttached(PXGraph graph, PXCache cache)
  {
    if (cache == null || !Attribute.IsDefined((MemberInfo) cache.GetItemType(), typeof (PXVirtualAttribute), true))
      return;
    new PXVirtualDACAttribute().CacheAttached(graph, cache);
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    graph.RowPersisting.AddHandler(cache.GetItemType(), PXVirtualAttribute.\u003C\u003EO.\u003C0\u003E__RowPersisting ?? (PXVirtualAttribute.\u003C\u003EO.\u003C0\u003E__RowPersisting = new PXRowPersisting(PXVirtualAttribute.RowPersisting)));
  }

  private static void RowPersisting(PXCache sender, PXRowPersistingEventArgs e) => e.Cancel = true;
}
