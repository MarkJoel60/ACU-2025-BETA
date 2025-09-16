// Decompiled with JetBrains decompiler
// Type: PX.TM.Escalated`4
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.SQLTree;
using System.Collections.Generic;

#nullable disable
namespace PX.TM;

/// <exclude />
public class Escalated<Owner, PendingWorkgroupID, PendingOwnerID, PendingDate> : 
  IBqlComparison,
  IBqlCreator,
  IBqlVerifier
  where Owner : IBqlOperand
  where PendingWorkgroupID : IBqlOperand
  where PendingOwnerID : IBqlOperand
  where PendingDate : IBqlOperand
{
  private IBqlCreator _owner;
  private IBqlCreator _pendingWorkgroupID;
  private IBqlCreator _pendingOwnerID;
  private IBqlCreator _pendingDate;

  /// <exclude />
  public void Verify(
    PXCache cache,
    object item,
    List<object> pars,
    ref bool? result,
    ref object value)
  {
    result = new bool?();
    value = (object) null;
  }

  /// <exclude />
  public virtual bool AppendExpression(
    ref SQLExpression exp,
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    bool flag1 = true;
    SQLExpression exp1 = (SQLExpression) null;
    bool flag2 = flag1 & BqlCommand.AppendExpression<Owner>(ref exp1, graph, info, selection, ref this._owner);
    SQLExpression exp2 = (SQLExpression) null;
    bool flag3 = flag2 & BqlCommand.AppendExpression<PendingDate>(ref exp2, graph, info, selection, ref this._pendingDate);
    SQLExpression exp3 = (SQLExpression) null;
    bool flag4 = flag3 & BqlCommand.AppendExpression<PendingWorkgroupID>(ref exp3, graph, info, selection, ref this._pendingWorkgroupID);
    SQLExpression exp4 = (SQLExpression) null;
    bool flag5 = flag4 & BqlCommand.AppendExpression<PendingOwnerID>(ref exp4, graph, info, selection, ref this._pendingOwnerID);
    if (!info.BuildExpression)
      return flag5;
    SimpleTable t = new SimpleTable(typeof (EPCompanyTreeMember), "EPCompanyTreeMemberA");
    Query q = new Query();
    q.Select<EPCompanyTreeH.workGroupID>().From<EPCompanyTreeH>().Join<EPCompanyTreeMember>().On(SQLExpressionExt.EQ(typeof (EPCompanyTreeH.parentWGID), typeof (EPCompanyTreeMember.workGroupID)).And(typeof (EPCompanyTreeMember.active).EQ((object) 1)).And(SQLExpressionExt.EQ(typeof (EPCompanyTreeMember.contactID), exp1))).LeftJoin((Table) t).On(SQLExpressionExt.EQ(new Column(typeof (EPCompanyTreeMember.workGroupID), (Table) t), (SQLExpression) new Column(typeof (EPCompanyTreeMember.workGroupID)))).Where(new Column(typeof (EPCompanyTreeH.parentWGLevel)).LT((SQLExpression) new Column(typeof (EPCompanyTreeH.workGroupLevel))).And(new SQLDateDiff((IConstant<string>) new DateDiff.minute(), exp2, SQLExpression.Now()).GT((SQLExpression) new Column(typeof (EPCompanyTreeH.waitTime)))).Embrace().Or(SQLExpressionExt.EQ(SQLExpressionExt.EQ(exp3, (SQLExpression) new Column(typeof (EPCompanyTreeMember.workGroupID), (Table) t)).And((SQLExpression) new Column(typeof (EPCompanyTreeMember.contactID), (Table) t)), exp4).And(new SQLDateDiff((IConstant<string>) new DateDiff.minute(), exp2, SQLExpression.NowUtc()).GT((SQLExpression) new Column(typeof (EPCompanyTreeMember.waitTime), (Table) t))).Embrace()));
    exp = exp.In(q);
    return flag5;
  }
}
