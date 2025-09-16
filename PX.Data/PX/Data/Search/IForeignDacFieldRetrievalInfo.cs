// Decompiled with JetBrains decompiler
// Type: PX.Data.Search.IForeignDacFieldRetrievalInfo
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;

#nullable disable
namespace PX.Data.Search;

/// <summary>
/// An interface for the information that is used for the retrieval of foreign DAC fields during creation of search results.
/// See <see cref="T:PX.Data.Maintenance.GI.GIDesign" /> for examples.
/// </summary>
[PXInternalUseOnly]
public interface IForeignDacFieldRetrievalInfo
{
  /// <summary>
  /// Gets the type of the foreign DAC whose fields should be retrieved.
  /// </summary>
  /// <value>The type of the foreign DAC.</value>
  System.Type ForeignDac { get; }

  /// <summary>
  /// Gets the types of foreign DAC fields whose values should be retrieved for creation of search results.
  /// </summary>
  /// <value>The foreign DAC field types.</value>
  System.Type[] ForeignDacFields { get; }

  /// <summary>
  /// Gets the required DAC field types from the DAC that declares the <see cref="T:PX.Data.PXSearchableAttribute" /> attribute
  /// The fields will be used as <see cref="P:PX.Data.Search.IForeignDacFieldRetrievalInfo.Query" /> parameters.
  /// The fields must be declared in the same order as the parameters in the <see cref="P:PX.Data.Search.IForeignDacFieldRetrievalInfo.Query" />
  /// </summary>
  /// <value>
  /// The required DAC field types from the DAC that declares the <see cref="T:PX.Data.PXSearchableAttribute" /> attribute.
  /// </value>
  System.Type[] RequiredDacFields { get; }

  /// <summary>
  /// Gets the BQL query to obtain a foreign DAC. The query should use the foreign DAC as the first DAC.
  /// </summary>
  /// <example>
  /// <para>An example of the query is shown below.</para>
  /// <code>
  /// Select2&lt;ForeignDac,
  ///   InnerJoin&lt;DeclaringDac,
  /// 		On&lt;ForeignDac.dacID, Equal&lt;DeclaringDac.foreignDacId&gt;&gt;&gt;,
  ///   Where&lt;DeclaringDac.dacKey, Equal&lt;Required&lt;DeclaringDac.dacKey&gt;&gt;&gt;&gt;
  /// </code>
  /// </example>
  /// <value>The query to get the foreign DAC.</value>
  System.Type Query { get; }
}
