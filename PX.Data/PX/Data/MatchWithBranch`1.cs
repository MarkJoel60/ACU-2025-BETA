// Decompiled with JetBrains decompiler
// Type: PX.Data.MatchWithBranch`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.BQL;
using PX.Data.SQLTree;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data;

/// <summary>
/// Matches the data records whose field is null or holds the ID of a branch that
/// can be accessed from within the current branch. The current branch is the
/// branch to which the user is logged in.
/// </summary>
/// <typeparam name="Field">A field where to look for the branch ID whose rights
/// should be checked.</typeparam>
public sealed class MatchWithBranch<Field> : 
  BqlChainableConditionLite<MatchWithBranch<Field>>,
  IBqlUnary,
  IBqlCreator,
  IBqlVerifier
  where Field : IBqlOperand
{
  /// <exclude />
  public void Verify(
    PXCache cache,
    object item,
    List<object> pars,
    ref bool? result,
    ref object value)
  {
    if (PXDatabase.ReadBranchRestricted)
    {
      result = new bool?(true);
    }
    else
    {
      if (!typeof (IBqlField).IsAssignableFrom(typeof (Field)))
        throw new PXArgumentException("Operand", "'{0}' either has to be a class field or has to expose the IBqlCreator interface.");
      object obj = (cache.GetItemType() == typeof (Field).DeclaringType ? 0 : (!typeof (Field).DeclaringType.IsAssignableFrom(cache.GetItemType()) ? 1 : 0)) != 0 ? (object) null : cache.GetValue(item, typeof (Field).Name);
      if (obj == null)
      {
        result = new bool?(true);
      }
      else
      {
        int[] array = PXDatabase.BranchIDs != null ? PXDatabase.BranchIDs.ToArray() : PXAccess.GetBranchIDs();
        if (array != null && array.Length != 0)
          result = new bool?(Array.IndexOf<int>(array, (int) obj) > -1);
        else
          result = new bool?(false);
      }
    }
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
    int[] source = PXDatabase.BranchIDs != null ? PXDatabase.BranchIDs.ToArray() : PXAccess.GetBranchIDs();
    if (source != null && source.Length != 0)
      exp = exp.Or(Query.CreateOptimizedListInclusionCheck(singleExpression, ((IEnumerable<int>) source).ToList<int>())).Embrace();
    return flag;
  }
}
