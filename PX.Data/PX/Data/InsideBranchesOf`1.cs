// Decompiled with JetBrains decompiler
// Type: PX.Data.InsideBranchesOf`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.SQLTree;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data;

/// <summary>Builds a list of BranchID for a specific BAccountID</summary>
/// <typeparam name="Operand">BAccountID to look for the BranchID.</typeparam>
public class InsideBranchesOf<Operand> : In<Operand> where Operand : IBqlParameter
{
  public override void Verify(
    PXCache cacheBase,
    object item,
    List<object> pars,
    ref bool? result,
    ref object value)
  {
    result = new bool?(false);
    object val = (object) null;
    if (this._operand1 is IBqlParameter operand1)
    {
      System.Type referencedType = operand1.GetReferencedType();
      if (referencedType.IsNested && cacheBase.Graph != null)
      {
        System.Type itemType = BqlCommand.GetItemType(referencedType);
        PXCache cach = cacheBase.Graph.Caches[itemType];
        if (cach.InternalCurrent != null)
          val = cach.GetValue(cach.Current, referencedType.Name);
      }
      if (pars.Count <= 0)
        return;
      pars.RemoveAt(0);
    }
    else
    {
      if (this._operand1 == null)
        this._operand1 = this._operand1.createOperand<Operand>();
      this._operand1.Verify(cacheBase, item, pars, ref result, ref val);
    }
    if (val != null && value != null && !this.GetVisibilityList(val).Contains((int) value))
      return;
    result = new bool?(true);
  }

  public override bool AppendExpression(
    ref SQLExpression exp,
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    bool flag1 = true;
    if (this._operand1 == null)
      this._operand1 = this._operand1.createOperand<Operand>();
    SQLExpression exp1 = (SQLExpression) null;
    bool flag2 = flag1 & this._operand1.AppendExpression(ref exp1, graph, info, selection);
    if (graph == null || !info.BuildExpression)
      return flag2;
    object val = (object) null;
    if (this._operand1 is IBqlParameter operand1)
    {
      System.Type referencedType = operand1.GetReferencedType();
      if (referencedType.IsNested && graph != null)
      {
        System.Type itemType = BqlCommand.GetItemType(referencedType);
        PXCache cach = graph.Caches[itemType];
        if (cach.Current != null)
          val = cach.GetValue(cach.Current, referencedType.Name);
      }
    }
    SQLExpression exp2 = SQLExpression.None();
    if (val != null)
    {
      foreach (int visibility in this.GetVisibilityList(val))
        exp2 = exp2.Seq((SQLExpression) new SQLConst((object) visibility));
      exp = exp.In(exp2);
    }
    else
      exp = new SQLConst((object) 1).EQ((object) 0);
    return flag2;
  }

  protected virtual HashSet<int> GetVisibilityList(object val)
  {
    int num = (int) val;
    HashSet<int> visibilityList = new HashSet<int>();
    PXAccess.MasterCollection.Branch branchByBaccountId = PXAccess.GetBranchByBAccountID(new int?(num));
    if (branchByBaccountId != null && !branchByBaccountId.DeletedDatabaseRecord)
    {
      visibilityList.Add(branchByBaccountId.BranchID);
    }
    else
    {
      PXAccess.MasterCollection.Organization organizationByBaccountId = PXAccess.GetOrganizationByBAccountID(new int?(num));
      if (organizationByBaccountId != null)
      {
        bool? deletedDatabaseRecord = organizationByBaccountId.DeletedDatabaseRecord;
        bool flag = true;
        if (!(deletedDatabaseRecord.GetValueOrDefault() == flag & deletedDatabaseRecord.HasValue))
        {
          foreach (PXAccess.MasterCollection.Branch branch in organizationByBaccountId.ChildBranches.Where<PXAccess.MasterCollection.Branch>((Func<PXAccess.MasterCollection.Branch, bool>) (x => !x.DeletedDatabaseRecord)))
            visibilityList.Add(branch.BranchID);
        }
      }
    }
    return visibilityList;
  }
}
