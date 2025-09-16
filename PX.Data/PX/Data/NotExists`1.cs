// Decompiled with JetBrains decompiler
// Type: PX.Data.NotExists`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.BQL;
using PX.Data.SQLTree;
using System.Collections.Generic;

#nullable disable
namespace PX.Data;

/// <exclude />
public sealed class NotExists<Select> : 
  BqlChainableConditionLite<NotExists<Select>>,
  IBqlUnary,
  IBqlCreator,
  IBqlVerifier
  where Select : BqlCommand, IBqlSelect, new()
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
      exp = queryInternal.NotExists();
    }
    return true;
  }
}
