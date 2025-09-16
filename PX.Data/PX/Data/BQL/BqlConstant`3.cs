// Decompiled with JetBrains decompiler
// Type: PX.Data.BQL.BqlConstant`3
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.SQLTree;
using System.Collections.Generic;

#nullable disable
namespace PX.Data.BQL;

/// <exclude />
public abstract class BqlConstant<TSelf, TBqlType, TCSharpType> : 
  BqlOperand<TSelf, TBqlType>,
  IConstant<TCSharpType>,
  IConstant,
  IBqlAggregatedOperand,
  IBqlCreator,
  IBqlVerifier
  where TSelf : BqlConstant<TSelf, TBqlType, TCSharpType>
  where TBqlType : class, IBqlDataType
{
  protected BqlConstant(TCSharpType value) => this.Value = value;

  public virtual TCSharpType Value { get; }

  private protected virtual PXDbType DbType { get; } = PXDbType.Unspecified;

  object IConstant.Value => (object) this.Value;

  void IBqlVerifier.Verify(
    PXCache cache,
    object item,
    List<object> pars,
    ref bool? result,
    ref object value)
  {
    value = (object) this.Value;
  }

  bool IBqlCreator.AppendExpression(
    ref SQLExpression exp,
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    if (graph == null || !info.BuildExpression)
      return true;
    exp = (SQLExpression) new SQLConst((object) this.Value);
    if (this.DbType == PXDbType.Unspecified)
      ((SQLConst) exp).SetDBType(this.EnsureDbType());
    else
      ((SQLConst) exp).SetDBType(this.DbType);
    return true;
  }

  private PXDbType EnsureDbType()
  {
    return PXDatabase.Provider.SqlDialect.GetConstDbType(typeof (TCSharpType));
  }
}
