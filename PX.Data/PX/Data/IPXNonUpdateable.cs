// Decompiled with JetBrains decompiler
// Type: PX.Data.IPXNonUpdateable
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <summary>
/// If a data view is marked with this interface, its primary table will not be persisted
/// by a <see cref="T:PX.Data.PXGraph" /> instance where the data view is declared.
/// </summary>
/// <remarks>
/// If a graph includes multiple data views with the same primary table,
/// and at least one data view is not marked with this interface, the primary table is still persisted by the <see cref="T:PX.Data.PXGraph" /> instance.
/// </remarks>
/// <example>
/// 	<code lang="CS">
/// public class PXSetup&lt;Table&gt; : PXSelectReadonly&lt;Table&gt;, IPXNonUpdateable
/// 	</code>
/// </example>
public interface IPXNonUpdateable
{
}
