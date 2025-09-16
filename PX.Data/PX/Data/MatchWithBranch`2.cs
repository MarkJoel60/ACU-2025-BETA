// Decompiled with JetBrains decompiler
// Type: PX.Data.MatchWithBranch`2
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
/// Matches the data records whose field is null or holds the ID of a branch that
/// can be accessed from within the specified branch or its subsidiaries.
/// </summary>
/// <typeparam name="Field">A field where to look for the branch ID whose rights
/// should be checked.</typeparam>
/// <typeparam name="Parameter">The branch to check against the branch found in <tt>Field</tt>.</typeparam>
public sealed class MatchWithBranch<Field, Parameter> : 
  BqlChainableConditionLite<MatchWithBranch<Field, Parameter>>,
  IBqlUnary,
  IBqlCreator,
  IBqlVerifier
  where Field : IBqlOperand
  where Parameter : IBqlParameter, new()
{
  private IBqlParameter _parameter;

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
    bool flag = true;
    if (graph == null || !info.BuildExpression)
      return flag;
    if (PXDatabase.ReadBranchRestricted || !typeof (IBqlField).IsAssignableFrom(typeof (Field)))
    {
      exp = new SQLConst((object) 1).EQ((object) 1);
      return flag;
    }
    SQLExpression singleExpression = BqlCommand.GetSingleExpression(typeof (Field), graph, info.Tables, selection, BqlCommand.FieldPlace.Condition);
    exp = singleExpression.IsNull();
    if (this._parameter == null)
      this._parameter = (IBqlParameter) new Parameter();
    object obj = (object) null;
    if (this._parameter.HasDefault)
    {
      System.Type referencedType = this._parameter.GetReferencedType();
      if (referencedType.IsNested)
      {
        System.Type itemType = BqlCommand.GetItemType(referencedType);
        PXCache cach = graph.Caches[itemType];
        if (cach.Current != null)
          obj = cach.GetValue(cach.Current, referencedType.Name);
      }
    }
    List<int> intList = PXDatabase.BranchIDs;
    if (intList == null && obj != null)
      intList = new List<int>() { (int) obj };
    if (intList != null && intList.Count > 0)
    {
      SQLExpression exp1 = (SQLExpression) null;
      foreach (int num in intList)
        exp1 = exp1 != null ? exp1.Seq((object) num) : (SQLExpression) new SQLConst((object) num);
      exp = exp.Or(singleExpression.In(exp1)).Embrace();
    }
    return flag;
  }
}
