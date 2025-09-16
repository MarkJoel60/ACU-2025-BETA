// Decompiled with JetBrains decompiler
// Type: PX.Data.EqualBaseCuryID`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.SQLTree;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Data;

/// <summary>Builds a list of BranchID for a specific BAccountID</summary>
/// <typeparam name="Operand">BAccountID to look for the BranchID.</typeparam>
public class EqualBaseCuryID<Operand> : In<Operand> where Operand : IBqlParameter
{
  public override void Verify(
    PXCache cacheBase,
    object item,
    List<object> pars,
    ref bool? result,
    ref object value)
  {
    result = new bool?(false);
    object strA = (object) null;
    if (this._operand1 is IBqlParameter operand1_1)
    {
      System.Type referencedType = operand1_1.GetReferencedType();
      if (referencedType.IsNested && cacheBase.Graph != null)
      {
        System.Type itemType = BqlCommand.GetItemType(referencedType);
        PXCache cach = cacheBase.Graph.Caches[itemType];
        if (cach.InternalCurrent != null)
        {
          int? nullable = cach.GetValue(cach.Current, referencedType.Name) as int?;
          strA = !(cach?.GetStateExt((object) null, referencedType.Name) is PXBranchSelectorState) ? (object) (PXAccess.GetBranch(nullable)?.BaseCuryID ?? PXAccess.GetOrganizationByID(nullable)?.BaseCuryID) : (object) (PXAccess.GetBranchByBAccountID(nullable)?.BaseCuryID ?? PXAccess.GetOrganizationByID(nullable)?.BaseCuryID);
        }
        else
        {
          result = new bool?(true);
          return;
        }
      }
      if (pars.Count <= 0)
        return;
      pars.RemoveAt(0);
    }
    else
    {
      if (this._operand1 == null)
        this._operand1 = this._operand1.createOperand<Operand>();
      object obj = (object) null;
      this._operand1.Verify(cacheBase, item, pars, ref result, ref obj);
      if (this._operand1 is IBqlParameter operand1)
      {
        System.Type referencedType = operand1.GetReferencedType();
        System.Type itemType = BqlCommand.GetItemType(referencedType);
        PXCache cach = cacheBase.Graph.Caches[itemType];
        if (cach.InternalCurrent != null)
        {
          int? nullable = cach.GetValue(cach.Current, referencedType.Name) as int?;
          strA = !(cach?.GetStateExt((object) null, referencedType.Name) is PXBranchSelectorState) ? (object) (PXAccess.GetBranch(nullable)?.BaseCuryID ?? PXAccess.GetOrganizationByID(nullable)?.BaseCuryID) : (object) (PXAccess.GetBranchByBAccountID(nullable)?.BaseCuryID ?? PXAccess.GetOrganizationByID(nullable)?.BaseCuryID);
        }
      }
    }
    result = new bool?(string.Compare((string) strA, (string) value, StringComparison.InvariantCultureIgnoreCase) == 0);
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
      if (referencedType.IsNested)
      {
        System.Type itemType = BqlCommand.GetItemType(referencedType);
        PXCache cach = graph.Caches[itemType];
        if (cach.Current != null)
        {
          int? nullable = cach.GetValue(cach.Current, referencedType.Name) as int?;
          v = !(cach?.GetStateExt((object) null, referencedType.Name) is PXBranchSelectorState) ? (object) (PXAccess.GetBranch(nullable)?.BaseCuryID ?? PXAccess.GetOrganizationByID(nullable)?.BaseCuryID) : (object) (PXAccess.GetBranchByBAccountID(nullable)?.BaseCuryID ?? PXAccess.GetOrganizationByID(nullable)?.BaseCuryID);
        }
      }
      else
      {
        exp = new SQLConst((object) 1).EQ((object) 1);
        return flag2;
      }
    }
    exp = v != null ? exp.In((SQLExpression) new SQLConst(v)) : new SQLConst((object) 1).EQ((object) 0);
    return flag2;
  }
}
