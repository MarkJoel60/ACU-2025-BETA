// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.BQL.IsFromExpenseClaims`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.SQLTree;
using System;
using System.Collections.Generic;

#nullable enable
namespace PX.Objects.AP.BQL;

/// <summary>
/// A BQL predicate checking whether its operand (returning a
/// screen ID) corresponds to one of the Expense Claims screens.
/// </summary>
[Obsolete("This predicate is not used anymore and will be removed in Acumatica ERP 8.0. Use APRegister.OrigModule == 'EP' comparison instead.", false)]
public class IsFromExpenseClaims<TScreenID> : IBqlUnary, IBqlCreator, IBqlVerifier where TScreenID : 
#nullable disable
IBqlOperand
{
  private IBqlUnary _where = (IBqlUnary) new Where<TScreenID, Equal<IsFromExpenseClaims<TScreenID>.screenExpenseClaim>, Or<TScreenID, Equal<IsFromExpenseClaims<TScreenID>.screenExpenseClaims>, Or<TScreenID, Equal<IsFromExpenseClaims<TScreenID>.screenReleaseExpenseClaims>>>>();

  public bool AppendExpression(
    ref SQLExpression exp,
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    return this._where.AppendExpression(ref exp, graph, info, selection);
  }

  public void Verify(
    PXCache cache,
    object item,
    List<object> pars,
    ref bool? result,
    ref object value)
  {
    this._where.Verify(cache, item, pars, ref result, ref value);
  }

  public class screenExpenseClaims : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    IsFromExpenseClaims<TScreenID>.screenExpenseClaims>
  {
    public screenExpenseClaims()
      : base("EP301030")
    {
    }
  }

  public class screenExpenseClaim : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    IsFromExpenseClaims<TScreenID>.screenExpenseClaim>
  {
    public screenExpenseClaim()
      : base("EP301000")
    {
    }
  }

  public class screenReleaseExpenseClaims : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    IsFromExpenseClaims<TScreenID>.screenReleaseExpenseClaims>
  {
    public screenReleaseExpenseClaims()
      : base("EP501000")
    {
    }
  }
}
