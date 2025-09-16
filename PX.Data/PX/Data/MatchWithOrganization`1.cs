// Decompiled with JetBrains decompiler
// Type: PX.Data.MatchWithOrganization`1
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

public sealed class MatchWithOrganization<Field> : 
  BqlChainableConditionLite<MatchWithOrganization<Field>>,
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
      int? organizationID = (cache.GetItemType() == typeof (Field).DeclaringType ? 0 : (!typeof (Field).DeclaringType.IsAssignableFrom(cache.GetItemType()) ? 1 : 0)) != 0 ? new int?() : (int?) cache.GetValue(item, typeof (Field).Name);
      if (!organizationID.HasValue)
      {
        result = new bool?(true);
      }
      else
      {
        int[] source = PXDatabase.BranchIDs != null ? PXDatabase.BranchIDs.ToArray() : PXAccess.GetBranchIDs();
        if (source != null && source.Length != 0)
          result = new bool?(((IEnumerable<int>) source).Any<int>((Func<int, bool>) (branchID =>
          {
            int? parentOrganizationId = PXAccess.GetParentOrganizationID(new int?(branchID));
            int? nullable = organizationID;
            return parentOrganizationId.GetValueOrDefault() == nullable.GetValueOrDefault() & parentOrganizationId.HasValue == nullable.HasValue;
          })));
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
    int[] source1 = PXDatabase.BranchIDs != null ? PXDatabase.BranchIDs.ToArray() : PXAccess.GetBranchIDs();
    int?[] nullableArray1;
    if (source1 == null)
    {
      nullableArray1 = (int?[]) null;
    }
    else
    {
      IEnumerable<int?> source2 = ((IEnumerable<int>) source1).Select<int, int?>((Func<int, int?>) (branchID => PXAccess.GetParentOrganizationID(new int?(branchID)))).Distinct<int?>();
      nullableArray1 = source2 != null ? source2.ToArray<int?>() : (int?[]) null;
    }
    int?[] nullableArray2 = nullableArray1;
    if (nullableArray2 != null && nullableArray2.Length != 0)
    {
      SQLExpression exp1 = (SQLExpression) null;
      foreach (int? nullable in nullableArray2)
        exp1 = exp1 != null ? exp1.Seq((object) nullable) : (SQLExpression) new SQLConst((object) nullable);
      exp = exp.Or(singleExpression.In(exp1)).Embrace();
    }
    return flag;
  }
}
