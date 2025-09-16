// Decompiled with JetBrains decompiler
// Type: PX.Data.Constant`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.SQLTree;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Data;

/// <summary>The base class for BQL constants.</summary>
/// <typeparam name="ConstType">The data type of the constant.</typeparam>
/// <remarks>
/// To define a custom constant in the application, derive a class from <tt>Constant&lt;&gt;</tt>.
/// Specify constant's type in the <tt>ConstType</tt> type parameter and implement the
/// constructor. The constructor should inherit base class constructor and provide
/// the constant's actual value in its argument.
/// </remarks>
/// <example><para>The predefined constant Zero represents integer 0 and is not suitable for comparison with decimal values. The application should define a custom constant for decimal zero, deriving it from Constant&lt;Decimal&gt; as the following code shows.</para>
/// <code title="Example" lang="CS">
/// public class decimal_0 : Constant&lt;Decimal&gt;
/// {
///     public decimal_0()
///         : base(0m)
///     {
///     }
/// }
/// ...
/// // Using the constant in BQL.
/// public PXSelect&lt;Table1,
///     Where&lt;Table.decimalField, Greater&lt;decimal_0&gt;&gt;&gt; records;</code>
/// <code title="Example2" description="This constant can be used in BQL statements in the following way:" groupname="Example" lang="CS">
/// PXSelect&lt;Table,
///     Where&lt;Table.decimalField, Greater&lt;decimal_0&gt;&gt;&gt;</code>
/// <code title="Example3" description="This BQL statement is translated into the following SQL query:" groupname="Example2" lang="SQL">
/// SELECT * FROM Table
/// WHERE Table.DecimalField &gt; .0</code>
/// </example>
[Obsolete("Use a corresponding PX.Data.BQL.Bql[Type].Constant<TSelf> instead")]
public abstract class Constant<ConstType> : 
  Constant,
  IBqlOperand,
  IBqlCreator,
  IBqlVerifier,
  IConstant<ConstType>,
  IConstant
{
  private readonly ConstType _Value;

  /// <exclude />
  public Constant(ConstType value) => this._Value = value;

  /// <exclude />
  public override object Value => (object) this._Value;

  ConstType IConstant<ConstType>.Value => (ConstType) this.Value;

  /// <exclude />
  public void Verify(
    PXCache cache,
    object item,
    List<object> pars,
    ref bool? result,
    ref object value)
  {
    value = this.Value;
  }

  /// <exclude />
  public bool AppendExpression(
    ref SQLExpression exp,
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    if (!info.BuildExpression)
      return true;
    SQLConst sqlConst = new SQLConst(this.Value);
    sqlConst.SetDBType(System.Type.GetTypeCode(this.Value.GetType()).GetDBType());
    exp = (SQLExpression) sqlConst;
    return true;
  }
}
