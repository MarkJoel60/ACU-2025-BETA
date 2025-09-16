// Decompiled with JetBrains decompiler
// Type: PX.Data.Exists`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.BQL;
using PX.Data.SQLTree;
using System.Collections.Generic;

#nullable disable
namespace PX.Data;

/// <summary>
///   <para>Is used to test for the existence of any record in a subquery. The class corresponds to the <tt>EXIST</tt> SQL statement.</para>
/// </summary>
/// <typeparam name="Select">An appropriate <tt>Select&lt;&gt;</tt> statement.</typeparam>
/// <example>
///   <code title="Example" description="" lang="CS">
/// Exists&lt;Select&lt;
///        APTran,
///        Where&lt;
///         APTran.tranType, Equal&lt;TDocTypeField&gt;,
///         And&lt;APTran.refNbr, Equal&lt;TRefNbrField&gt;,
///         And&lt;Where&lt;
///          APTran.pONbr, IsNotNull,
///          Or&lt;APTran.pOLineNbr, IsNotNull,
///          Or&lt;APTran.receiptNbr, IsNotNull,
///          Or&lt;APTran.receiptLineNbr, IsNotNull&gt;&gt;&gt;&gt;&gt;&gt;&gt;&gt;&gt;</code>
///   <code title="Example2" description="The BQL statement above will be translated into the following SQL query." groupname="Example" lang="SQL">
/// EXISTS (SELECT [list of columns] FROM APTran
///   WHERE
///   APTran.TranType = [value of TDocTypeField]
///   AND APTran.RefNbr = [value of TRefNbrField])
///   AND (APTran.PONbr IS NOT NULL
///        OR APTran.POLineNbr  IS NOT NULL
///        OR APTran.ReceiptNbr  IS NOT NULL
///        OR APTran.ReceiptLineNbr  IS NOT NULL)</code>
/// </example>
public sealed class Exists<Select> : 
  BqlChainableConditionLite<Exists<Select>>,
  IBqlUnary,
  IBqlCreator,
  IBqlVerifier
  where Select : BqlCommand, new()
{
  private BqlCommand _select;

  public void Verify(
    PXCache cache,
    object item,
    List<object> pars,
    ref bool? result,
    ref object value)
  {
    if (this._select == null)
      this._select = (BqlCommand) new Select();
    this._select.Verify(cache, item, pars, ref result, ref value);
    result = new bool?(true);
  }

  public bool AppendExpression(
    ref SQLExpression exp,
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    if (this._select == null)
      this._select = (BqlCommand) new Select();
    List<System.Type> typeList = new List<System.Type>();
    if (info.Tables != null && info.Tables.Count > 0)
      typeList.Add(info.Tables[0]);
    BqlCommand.Selection selection1 = new BqlCommand.Selection()
    {
      IsNestedView = true,
      ParamCounter = selection != null ? selection.ParamCounter : 0
    };
    BqlCommandInfo info1 = new BqlCommandInfo(false)
    {
      Tables = typeList,
      Parameters = info.Parameters
    };
    Query queryInternal = this._select.GetQueryInternal(graph, info1, selection1);
    if (selection != null)
      selection.ParamCounter = selection1.ParamCounter;
    if (info.BuildExpression)
    {
      queryInternal.ClearSelection().Field((SQLExpression) new Asterisk()).Limit(1);
      exp = queryInternal.Exists();
    }
    return true;
  }
}
