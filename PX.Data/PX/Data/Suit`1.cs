// Decompiled with JetBrains decompiler
// Type: PX.Data.Suit`1
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

public class Suit<Operand> : In<Operand> where Operand : IBqlParameter
{
  public override void Verify(
    PXCache cache,
    object item,
    List<object> pars,
    ref bool? result,
    ref object value)
  {
    object obj = (object) null;
    if (this._operand1 == null)
      this._operand1 = this._operand1.createOperand<Operand>();
    this._operand1.Verify(cache, item, pars, ref result, ref obj);
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
    object obj = (object) null;
    if (this._operand1 is IBqlParameter operand1)
    {
      System.Type referencedType = operand1.GetReferencedType();
      if (referencedType.IsNested && graph != null)
      {
        System.Type itemType = BqlCommand.GetItemType(referencedType);
        PXCache cach = graph.Caches[itemType];
        if (cach.Current != null)
          obj = cach.GetValue(cach.Current, referencedType.Name);
      }
    }
    if (obj != null)
    {
      int bAccountID = (int) obj;
      PXAccess.MasterCollection masterBranches = PXAccess.MasterBranches;
      PXAccess.MasterCollection.Organization organization = masterBranches.OrganizationsByID.Select<KeyValuePair<int, PXAccess.MasterCollection.Organization>, PXAccess.MasterCollection.Organization>((Func<KeyValuePair<int, PXAccess.MasterCollection.Organization>, PXAccess.MasterCollection.Organization>) (o => o.Value)).Where<PXAccess.MasterCollection.Organization>((Func<PXAccess.MasterCollection.Organization, bool>) (o =>
      {
        bool? deletedDatabaseRecord = o.DeletedDatabaseRecord;
        bool flag3 = true;
        if (deletedDatabaseRecord.GetValueOrDefault() == flag3 & deletedDatabaseRecord.HasValue)
          return false;
        int? baccountId = o.BAccountID;
        int num = bAccountID;
        return baccountId.GetValueOrDefault() == num & baccountId.HasValue;
      })).FirstOrDefault<PXAccess.MasterCollection.Organization>();
      if (organization != null)
      {
        exp = SQLExpressionExt.EQ(exp, (SQLExpression) new SQLConst((object) organization.OrganizationID));
        return flag2;
      }
      PXAccess.MasterCollection.Branch branch = masterBranches.AllBranchesByID.Select<KeyValuePair<int, PXAccess.MasterCollection.Branch>, PXAccess.MasterCollection.Branch>((Func<KeyValuePair<int, PXAccess.MasterCollection.Branch>, PXAccess.MasterCollection.Branch>) (b => b.Value)).Where<PXAccess.MasterCollection.Branch>((Func<PXAccess.MasterCollection.Branch, bool>) (b => !b.DeletedDatabaseRecord && b.BAccountID == bAccountID)).FirstOrDefault<PXAccess.MasterCollection.Branch>();
      exp = branch == null ? SQLExpressionExt.EQ(exp, (SQLExpression) new SQLConst((object) 0)) : SQLExpressionExt.EQ(exp, (SQLExpression) new SQLConst((object) branch.Organization.OrganizationID));
      return flag2;
    }
    Query q = new Query();
    SimpleTable t1 = new SimpleTable("Branch");
    SimpleTable t2 = new SimpleTable("Organization");
    q.Field((SQLExpression) new Column("OrganizationID", (Table) t2)).From((Table) t2).InnerJoin((Table) t1).On(SQLExpressionExt.EQ(new Column("OrganizationID", (Table) t2), (SQLExpression) new Column("OrganizationID", (Table) t1))).Where(SQLExpressionExt.EQ(new Column("BAccountID", (Table) t2), exp1).Or(SQLExpressionExt.EQ(new Column("BAccountID", (Table) t1), exp1))).Distinct();
    exp = exp.In(q).Or(exp1.IsNull().And(SQLExpressionExt.EQ(exp, (SQLExpression) new SQLConst((object) 0))));
    return flag2;
  }
}
