// Decompiled with JetBrains decompiler
// Type: PX.Data.FreeText`4
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <summary>This class is used to find all records for which the value of the specified field matches the meaning (not just the exact wording) of the text that is specified in the operand. The returned table contains the specified number of the highest-ranked matches. </summary>
/// <typeparam name="Operand">Specifies the text to be found.</typeparam>
/// <typeparam name="Field">Specifies the field in which the text specified by <em>Operand</em> should be found.</typeparam>
/// <typeparam name="FieldKey">Specifies the key field.</typeparam>
/// <remarks>The class can be used in the Where clause.</remarks>
/// <example><para>For example, the following statement illustrates the use of the FreeText function.</para>
///   <code title="Example" lang="CS">
/// PXSelect&lt;SearchIndex,
///   Where&lt;
///     Contains&lt;
///       SearchIndex.content,
///       Required&lt;SearchIndex.content&gt;,
///       SearchIndex.indexID,
///       Argument&lt;int?&gt;&gt;&gt;&gt;</code>
///   <code title="Example2" description="This statement is converted to the following SQL statement, where search_value is the value that is specified with the Required parameter, and number_of_records is the value that is specified with the Argument parameter." groupname="Example" lang="SQL">
/// SELECT * FROM SearchIndex
///   INNER JOIN FREETEXTTABLE(SearchIndex, SearchIndex.content, search_value, number_of_records)
///     AS ftt_SearchIndex ON ftt_SearchIndex.Key = SearchIndex.indexID</code>
/// </example>
public class FreeText<Field, Operand, FieldKey, TopCount> : 
  FreeTextBase<Field, Operand, FieldKey, TopCount>
  where Field : IBqlField
  where Operand : IBqlOperand
  where FieldKey : IBqlField
  where TopCount : IBqlOperand
{
  protected override string FreeTextOperator => "FREETEXTTABLE";
}
