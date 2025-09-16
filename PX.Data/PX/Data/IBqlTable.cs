// Decompiled with JetBrains decompiler
// Type: PX.Data.IBqlTable
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <summary>Represents a database table. The data access class (DAC) corresponding to the table must be derived from this interface.</summary>
/// <example><para>A DAC representing the &lt;tt&gt;Product&lt;/tt&gt; database table is defined in the following way.</para>
///   <code title="Example" lang="CS">
/// [System.SerializableAttribute()]
/// public class Product : PXBqlTable, PX.Data.IBqlTable
/// {
///     //Declarations of fields
/// }</code>
/// </example>
public interface IBqlTable : IBqlTableSystemDataStorage
{
}
