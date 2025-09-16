// Decompiled with JetBrains decompiler
// Type: PX.Data.ParameterBase`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.SQLTree;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Data;

/// <exclude />
public abstract class ParameterBase<Field> : IBqlParameter, IBqlOperand, IBqlCreator, IBqlVerifier
{
  /// <exclude />
  public virtual void Verify(
    PXCache cache,
    object item,
    List<object> pars,
    ref bool? result,
    ref object value)
  {
    if (pars.Count <= 0)
      return;
    value = pars[0];
    pars.RemoveAt(0);
  }

  /// <exclude />
  public virtual bool AppendExpression(
    ref SQLExpression exp,
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    if (selection != null)
    {
      if (info.BuildExpression)
        exp = (SQLExpression) Literal.NewParameter(selection.ParamCounter);
      ++selection.ParamCounter;
    }
    else if (info.Parameters != null && info.BuildExpression)
      exp = (SQLExpression) Literal.NewParameter(info.Parameters.Count);
    bool flag = selection == null || !selection.BqlMode.HasFlag((Enum) BqlCommand.Selection.BqlParsingMode.DontAllocateParameters);
    if (info.Parameters != null & flag)
      info.Parameters.Add((IBqlParameter) this);
    return true;
  }

  public System.Type GetReferencedType() => typeof (Field);

  public abstract bool TryDefault { get; }

  public abstract bool HasDefault { get; }

  public abstract bool IsVisible { get; }

  public abstract bool IsArgument { get; }

  public System.Type MaskedType { get; set; }

  public bool NullAllowed { get; set; }

  public override bool Equals(object obj) => obj != null && obj.GetType() == this.GetType();

  public override int GetHashCode() => this.GetType().GetHashCode();
}
