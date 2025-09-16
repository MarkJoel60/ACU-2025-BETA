// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.Bql.EqualSiteBaseCuryID`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.SQLTree;
using PX.Objects.IN;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.Common.Bql;

/// <exclude />
public class EqualSiteBaseCuryID<Operand> : In<Operand> where Operand : IBqlParameter
{
  public virtual void Verify(
    PXCache cacheBase,
    object item,
    List<object> pars,
    ref bool? result,
    ref object value)
  {
    result = new bool?(false);
    object strA = (object) null;
    if (((InBase<Operand>) this)._operand1 is IBqlParameter operand1)
    {
      Type referencedType = operand1.GetReferencedType();
      if (referencedType.IsNested && cacheBase.Graph != null)
      {
        Type itemType = BqlCommand.GetItemType(referencedType);
        PXCache cach = cacheBase.Graph.Caches[itemType];
        if (cach.InternalCurrent != null)
        {
          int? siteID = cach.GetValue(cach.Current, referencedType.Name) as int?;
          if (siteID.HasValue)
            strA = (object) INSite.PK.Find(cacheBase.Graph, siteID)?.BaseCuryID;
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
      if (((InBase<Operand>) this)._operand1 == null)
        ((InBase<Operand>) this)._operand1 = BqlCommon.createOperand<Operand>(((InBase<Operand>) this)._operand1);
      ((IBqlVerifier) ((InBase<Operand>) this)._operand1).Verify(cacheBase, item, pars, ref result, ref strA);
    }
    result = new bool?(string.Compare((string) strA, (string) value, StringComparison.InvariantCultureIgnoreCase) == 0);
  }

  public virtual bool AppendExpression(
    ref SQLExpression exp,
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    bool flag1 = true;
    if (((InBase<Operand>) this)._operand1 == null)
      ((InBase<Operand>) this)._operand1 = BqlCommon.createOperand<Operand>(((InBase<Operand>) this)._operand1);
    SQLExpression sqlExpression = (SQLExpression) null;
    bool flag2 = flag1 & ((InBase<Operand>) this)._operand1.AppendExpression(ref sqlExpression, graph, info, selection);
    if (graph == null || !info.BuildExpression)
      return flag2;
    object obj = (object) null;
    if (((InBase<Operand>) this)._operand1 is IBqlParameter operand1)
    {
      Type referencedType = operand1.GetReferencedType();
      if (referencedType.IsNested)
      {
        Type itemType = BqlCommand.GetItemType(referencedType);
        PXCache cach = graph.Caches[itemType];
        if (cach.Current != null)
        {
          int? siteID = cach.GetValue(cach.Current, referencedType.Name) as int?;
          if (siteID.HasValue)
            obj = (object) INSite.PK.Find(graph, siteID)?.BaseCuryID;
        }
      }
      else
      {
        exp = SQLExpressionExt.EQ((SQLExpression) new SQLConst((object) 1), (object) 1);
        return flag2;
      }
    }
    exp = obj != null ? exp.In((SQLExpression) new SQLConst(obj)) : SQLExpressionExt.EQ((SQLExpression) new SQLConst((object) 1), (object) 0);
    return flag2;
  }
}
