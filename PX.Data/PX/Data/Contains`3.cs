// Decompiled with JetBrains decompiler
// Type: PX.Data.Contains`3
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <summary>
/// This function is used to find all records for which the value of the specified field
/// contains the text that is specified in the operand.
/// </summary>
/// <typeparam name="Field">Specifies the field in which the text specified by <em>Operand</em> should be found.</typeparam>
/// <typeparam name="Operand">Specifies the text to be found.</typeparam>
/// <typeparam name="FieldKey">Specifies the key field.</typeparam>
/// <remarks>By using full-text search functions, you can perform a full-text search on full-text indexed columns containing character-based data types. The full-text
/// search functions can be used in the Where clause.</remarks>
/// <example><para>The following statement illustrates the use of the Contains function. This statement is converted to the following SQL statement, where search_value is the value that is specified with the Required parameter.</para>
/// <code title="Example" lang="CS">
/// PXSelect&lt;SearchIndex,
///   Where&lt;
///     Contains&lt;
///       SearchIndex.content,
///       Required&lt;SearchIndex.content&gt;,
///       SearchIndex.indexID&gt;&gt;&gt;</code>
/// <code title="Example2" description="" groupname="Example" lang="SQL">
/// SELECT * FROM SearchIndex
///   INNER JOIN CONTAINSTABLE(SearchIndex, SearchIndex.content, search_value)
///     AS ftt_SearchIndex ON ftt_SearchIndex.Key = SearchIndex.indexID</code>
/// </example>
public class Contains<Field, Operand, FieldKey> : FreeTextBase<Field, Operand, FieldKey, BqlNone>
  where Field : IBqlField
  where Operand : IBqlOperand
  where FieldKey : IBqlField
{
  protected override string FreeTextOperator => "CONTAINSTABLE";
}
