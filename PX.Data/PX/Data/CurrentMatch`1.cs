// Decompiled with JetBrains decompiler
// Type: PX.Data.CurrentMatch`1
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
/// Matches only the data records the specified user has access rights for.
/// Equivalent to <tt>Match&lt;Field&gt;</tt>, but is used in the
/// <tt>PXProjection</tt> attribute.
/// </summary>
/// <typeparam name="Parameter">The field holding the user name.</typeparam>
public sealed class CurrentMatch<Field> : 
  BqlChainableConditionLite<CurrentMatch<Field>>,
  IBqlUnary,
  IBqlCreator,
  IBqlVerifier
  where Field : IBqlField
{
  /// <exclude />
  public void Verify(
    PXCache cache,
    object item,
    List<object> pars,
    ref bool? result,
    ref object value)
  {
    result = new bool?(true);
  }

  /// <exclude />
  public bool AppendExpression(
    ref SQLExpression exp,
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    if (graph == null || !info.BuildExpression)
      return true;
    PXMutableCollection.AddMutableItem((IBqlCreator) this);
    List<System.Type> tables = info.Tables;
    if (GroupHelper.Count == 0 || !graph.Caches[tables[0]].Fields.Contains("GroupMask"))
    {
      exp = new SQLConst((object) 1).EQ((object) 1);
      return true;
    }
    if (tables == null)
    {
      exp = new SQLConst((object) 1).EQ((object) 0);
      return true;
    }
    PXCache cach = graph.Caches[BqlCommand.GetItemType(typeof (Field))];
    object obj = cach.GetValue<Field>(cach.Current);
    object referencedValue = GroupHelper.GetReferencedValue(cach, cach.Current, typeof (Field).Name, obj, graph._ForceUnattended);
    PXCommandPreparingEventArgs.FieldDescription description;
    cach.RaiseCommandPreparing(typeof (Field).Name, (object) null, referencedValue, PXDBOperation.Select, (System.Type) null, out description);
    if (description == null || description.Expr == null || description.DataValue == null)
    {
      exp = new SQLConst((object) 1).EQ((object) 0);
      return true;
    }
    byte[] dataValue = description.DataValue as byte[];
    uint num = 0;
    exp = SQLExpression.None();
    SQLExpression r1 = SQLExpression.None();
    foreach (GroupHelper.ParamsPair paramsPair in GroupHelper.GetParams(GroupHelper.GetReferencedType(cach, typeof (Field).Name), tables[0], dataValue))
    {
      Column column = new Column("GroupMask", (Table) new SimpleTable(tables[0]));
      SQLExpression r2 = SQLExpressionExt.EQ(new SQLConst((object) 0), column.ConvertBinToInt((uint) ((int) num * 4 + 1), 4U).BitAnd((SQLExpression) new SQLConst((object) paramsPair.First)));
      SQLExpression r3 = SQLExpressionExt.NE(new SQLConst((object) 0), column.ConvertBinToInt((uint) ((int) num * 4 + 1), 4U).BitAnd((SQLExpression) new SQLConst((object) paramsPair.Second)));
      ++num;
      exp = exp.And(r2);
      r1 = r1.Or(r3);
    }
    exp = exp.Or(r1).Embrace();
    return true;
  }
}
