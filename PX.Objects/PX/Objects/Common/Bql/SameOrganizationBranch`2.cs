// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.Bql.SameOrganizationBranch`2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.SQLTree;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.Common.Bql;

public sealed class SameOrganizationBranch<Field, Parameter> : IBqlUnary, IBqlCreator, IBqlVerifier
  where Field : IBqlOperand
  where Parameter : IBqlParameter, new()
{
  private IBqlParameter _parameter;

  public void Verify(
    PXCache cache,
    object item,
    List<object> pars,
    ref bool? result,
    ref object value)
  {
    object branchID = (object) null;
    if (this._parameter == null)
      this._parameter = (IBqlParameter) new Parameter();
    if (this._parameter.HasDefault)
    {
      Type referencedType = this._parameter.GetReferencedType();
      if (referencedType.IsNested)
      {
        Type itemType = BqlCommand.GetItemType(referencedType);
        PXCache cach = cache.Graph.Caches[itemType];
        if (cach.Current != null)
          branchID = cach.GetValue(cach.Current, referencedType.Name);
      }
    }
    if (!typeof (IBqlField).IsAssignableFrom(typeof (Field)))
      throw new PXArgumentException("Operand", "'{0}' either has to be a class field or has to expose the IBqlCreator interface.");
    value = cache.GetItemType() == BqlCommand.GetItemType(typeof (Field)) || BqlCommand.GetItemType(typeof (Field)).IsAssignableFrom(cache.GetItemType()) ? cache.GetValue(item, typeof (Field).Name) : (object) null;
    List<int> intList = (List<int>) null;
    if (branchID != null)
      intList = this.GetSameOrganizationBranches((int?) branchID);
    if (intList == null || intList.Count <= 0 || value == null)
      return;
    result = new bool?(intList.Contains((int) value));
  }

  public bool AppendExpression(
    ref SQLExpression exp,
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    bool flag = true;
    object obj = (object) null;
    if (this._parameter == null)
      this._parameter = (IBqlParameter) new Parameter();
    if (graph != null && info.BuildExpression)
    {
      if (this._parameter.HasDefault)
      {
        Type referencedType = this._parameter.GetReferencedType();
        if (referencedType.IsNested)
        {
          Type itemType = BqlCommand.GetItemType(referencedType);
          PXCache cach = graph.Caches[itemType];
          if (cach.Current != null)
            obj = cach.GetValue(cach.Current, referencedType.Name);
        }
      }
      SQLExpression singleExpression = BqlCommand.GetSingleExpression(typeof (Field), graph, info.Tables, selection, (BqlCommand.FieldPlace) 0);
      exp = singleExpression.IsNull();
      List<int> intList = (List<int>) null;
      if (obj != null)
        intList = this.GetSameOrganizationBranches((int?) obj);
      if (intList != null && intList.Count > 0)
      {
        SQLExpression sqlExpression = (SQLExpression) null;
        foreach (int num in intList)
          sqlExpression = sqlExpression != null ? sqlExpression.Seq((object) num) : (SQLExpression) new SQLConst((object) num);
        exp = exp.Or(singleExpression.In(sqlExpression)).Embrace();
      }
    }
    List<Type> fields = info.Fields;
    // ISSUE: explicit non-virtual call
    if ((fields != null ? (!__nonvirtual (fields.Contains(typeof (Field))) ? 1 : 0) : 0) != 0)
      info.Fields.Add(typeof (Field));
    if (!selection.FromProjection)
      exp = this.AddParameterExpression(exp, info, selection, obj);
    return flag;
  }

  private SQLExpression AddParameterExpression(
    SQLExpression currentExpression,
    BqlCommandInfo info,
    BqlCommand.Selection selection,
    object parameterValue)
  {
    SQLExpression sqlExpression1 = (SQLExpression) Literal.NewParameter(selection.ParamCounter++);
    SQLExpression sqlExpression2 = parameterValue != null ? sqlExpression1.IsNotNull() : sqlExpression1.IsNull();
    info.Parameters?.Add(this._parameter);
    SQLExpression sqlExpression3 = currentExpression;
    return sqlExpression2.And(sqlExpression3).Embrace();
  }

  private List<int> GetSameOrganizationBranches(int? branchID)
  {
    return ((IEnumerable<int>) PXAccess.GetChildBranchIDs(PXAccess.GetParentOrganizationID(branchID), false)).ToList<int>();
  }
}
