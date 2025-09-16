// Decompiled with JetBrains decompiler
// Type: PX.Data.Inside`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.SQLTree;
using System.Collections.Generic;

#nullable disable
namespace PX.Data;

public class Inside<Operand> : RestrictByOrganization<Operand> where Operand : IBqlParameter
{
  protected override HashSet<int> GetVisibilityList(object val)
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

  /// <exclude />
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
        if (cach.Current != null)
          val = cach.GetValue(cach.Current, referencedType.Name);
      }
    }
    else if (this._operand1 == null)
      this._operand1 = this._operand1.createOperand<Operand>();
    this._operand1.Verify(cacheBase, item, pars, ref result, ref val);
    if (val == null)
      return;
    if ((int) val == 0)
    {
      result = new bool?(true);
    }
    else
    {
      HashSet<int> visibilityList = this.GetVisibilityList(val);
      PXAccess.MasterCollection.Branch branch = PXAccess.GetBranch((int?) value);
      foreach (int num in visibilityList)
      {
        if (num.Equals((object) branch?.BAccountID))
        {
          result = new bool?(true);
          break;
        }
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
    object v = (object) null;
    if (this._operand1 is IBqlParameter operand1)
    {
      System.Type referencedType = operand1.GetReferencedType();
      if (referencedType.IsNested && graph != null)
      {
        System.Type itemType = BqlCommand.GetItemType(referencedType);
        PXCache cach = graph.Caches[itemType];
        if (cach.Current != null)
          v = cach.GetValue(cach.Current, referencedType.Name);
      }
    }
    if (v != null)
      exp1 = (SQLExpression) new SQLConst((object) (int) v);
    exp = exp.In(exp1.InsideBranch());
    return flag2;
  }
}
