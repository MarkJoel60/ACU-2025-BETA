// Decompiled with JetBrains decompiler
// Type: PX.Data.RestrictByOrganization`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.SQLTree;
using System.Collections.Generic;

#nullable disable
namespace PX.Data;

public class RestrictByOrganization<Operand> : In<Operand> where Operand : IBqlParameter
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
    foreach (int num in val != null ? (IEnumerable<int>) this.GetVisibilityList(val) : PXAccess.GetActiveBranchesWithParents())
    {
      if (num.Equals(value))
      {
        result = new bool?(true);
        break;
      }
    }
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
      if ((object) referencedType != null && referencedType.IsNested)
      {
        System.Type itemType = BqlCommand.GetItemType(referencedType);
        PXCache cach = graph.Caches[itemType];
        if (cach.Current != null)
          val = cach.GetValue(cach.Current, referencedType.Name);
      }
    }
    SQLExpression exp2 = SQLExpression.None();
    foreach (int v in val != null ? (IEnumerable<int>) this.GetVisibilityList(val) : PXAccess.GetActiveBranchesWithParents())
      exp2 = exp2.Seq((SQLExpression) new SQLConst((object) v));
    exp = exp.In(exp2);
    return flag2;
  }

  protected virtual HashSet<int> GetVisibilityList(object val)
  {
    int bAccountID = (int) val;
    HashSet<int> parents = RestrictByOrganization<Operand>.GetParents(bAccountID);
    PXAccess.MasterCollection.Organization organizationByBaccountId = PXAccess.GetOrganizationByBAccountID(new int?(bAccountID));
    if (organizationByBaccountId != null)
    {
      foreach (PXAccess.MasterCollection.Branch childBranch in organizationByBaccountId.ChildBranches)
        parents.Add(childBranch.BAccountID);
    }
    return parents;
  }

  public static HashSet<int> GetParents(int bAccountID)
  {
    HashSet<int> parents = new HashSet<int>() { 0 };
    PXAccess.MasterCollection.Branch branchByBaccountId = PXAccess.GetBranchByBAccountID(new int?(bAccountID));
    if (branchByBaccountId != null)
    {
      parents.Add(branchByBaccountId.BAccountID);
      int baccountId1 = branchByBaccountId.BAccountID;
      int? baccountId2 = branchByBaccountId.Organization.BAccountID;
      int valueOrDefault = baccountId2.GetValueOrDefault();
      if (!(baccountId1 == valueOrDefault & baccountId2.HasValue))
      {
        HashSet<int> intSet = parents;
        baccountId2 = branchByBaccountId.Organization.BAccountID;
        int num = baccountId2.Value;
        intSet.Add(num);
      }
      if (branchByBaccountId.Organization.Parents.Count > 0)
        branchByBaccountId.Organization.Parents.ForEach((System.Action<PXAccess.MasterCollection.Organization>) (p => parents.Add(p.BAccountID.Value)));
    }
    else
    {
      PXAccess.MasterCollection.Organization organizationByBaccountId = PXAccess.GetOrganizationByBAccountID(new int?(bAccountID));
      if (organizationByBaccountId != null)
      {
        parents.Add(organizationByBaccountId.BAccountID.Value);
        if (organizationByBaccountId.Parents.Count > 0)
          organizationByBaccountId.Parents.ForEach((System.Action<PXAccess.MasterCollection.Organization>) (p => parents.Add(p.BAccountID.Value)));
      }
    }
    return parents;
  }
}
