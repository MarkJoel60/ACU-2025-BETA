// Decompiled with JetBrains decompiler
// Type: PX.Data.FullTextMatchAgainst`3
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.SQLTree;
using System.Collections.Generic;

#nullable disable
namespace PX.Data;

/// <exclude />
internal class FullTextMatchAgainst<Text, Pattern, TopCount> : IBqlUnary, IBqlCreator, IBqlVerifier
  where Text : IBqlOperand
  where Pattern : IBqlOperand
  where TopCount : IBqlOperand
{
  private IBqlCreator _text;
  private IBqlCreator _pattern;

  protected IBqlCreator ensureText()
  {
    return this._text ?? (this._text = this._text.createOperand<Text>());
  }

  protected IBqlCreator ensurePattern()
  {
    return this._pattern ?? (this._pattern = this._pattern.createOperand<Pattern>());
  }

  protected bool? verifyCore(object val, object value)
  {
    return Like<Pattern>.CheckLike(val as string, value as string);
  }

  public void Verify(
    PXCache cache,
    object item,
    List<object> pars,
    ref bool? result,
    ref object value)
  {
    object val = value;
    BqlFunction.getValue<Text>(ref this._text, cache, item, pars, ref result, out value);
    object obj;
    BqlFunction.getValue<Pattern>(ref this._pattern, cache, item, pars, ref result, out obj);
    result = this.verifyCore(val, obj);
  }

  public bool AppendExpression(
    ref SQLExpression exp,
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    throw new PXException("FullTextMatchAgainst<Text, Pattern, TopCount>.AppendExpression() called. ");
  }
}
