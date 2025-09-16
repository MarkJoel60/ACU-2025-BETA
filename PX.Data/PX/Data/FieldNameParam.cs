// Decompiled with JetBrains decompiler
// Type: PX.Data.FieldNameParam
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.SQLTree;
using System.Collections.Generic;

#nullable disable
namespace PX.Data;

/// <exclude />
internal sealed class FieldNameParam : IBqlParameter, IBqlOperand, IBqlCreator, IBqlVerifier
{
  /// <exclude />
  public void Verify(
    PXCache cache,
    object item,
    List<object> pars,
    ref bool? result,
    ref object value)
  {
    if (pars.Count <= 0)
      return;
    pars.RemoveAt(0);
  }

  /// <exclude />
  public bool AppendExpression(
    ref SQLExpression exp,
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    if (info.BuildExpression)
      exp = (SQLExpression) Literal.NewParameter(selection.ParamCounter);
    ++selection.ParamCounter;
    info.Parameters?.Add((IBqlParameter) this);
    return true;
  }

  public System.Type GetReferencedType() => (System.Type) null;

  public bool TryDefault => false;

  public bool HasDefault => false;

  public bool IsVisible => false;

  public bool IsArgument => false;

  public System.Type MaskedType
  {
    get => (System.Type) null;
    set
    {
    }
  }

  public bool NullAllowed
  {
    get => false;
    set
    {
    }
  }

  /// <exclude />
  internal class PXRequiredField : IBqlField, IBqlOperand
  {
  }
}
