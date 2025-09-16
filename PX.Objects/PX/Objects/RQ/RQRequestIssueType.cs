// Decompiled with JetBrains decompiler
// Type: PX.Objects.RQ.RQRequestIssueType
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.RQ;

public static class RQRequestIssueType
{
  public const string Open = "O";
  public const string PartiallyIssued = "P";
  public const string Closed = "I";
  public const string Canceled = "C";
  public const string Ordered = "B";
  public const string Requseted = "R";
  public const string Received = "E";

  public class ListAttribute : PXStringListAttribute, IPXRowSelectedSubscriber
  {
    public ListAttribute()
      : base(new Tuple<string, string>[7]
      {
        PXStringListAttribute.Pair("O", "Open"),
        PXStringListAttribute.Pair("P", "Partially Issued"),
        PXStringListAttribute.Pair("I", "Closed"),
        PXStringListAttribute.Pair("C", "Canceled"),
        PXStringListAttribute.Pair("B", "Ordered"),
        PXStringListAttribute.Pair("R", "Requested"),
        PXStringListAttribute.Pair("E", "Received")
      })
    {
    }

    public void RowSelected(PXCache sender, PXRowSelectedEventArgs e)
    {
      if (!(e.Row is RQRequestLine row))
        return;
      this.SetIssueStatus(sender, row);
    }

    protected virtual void SetIssueStatus(PXCache cache, RQRequestLine row)
    {
      if (row.Cancelled.GetValueOrDefault())
      {
        row.IssueStatus = "C";
      }
      else
      {
        Decimal? issuedQty = row.IssuedQty;
        Decimal? nullable = row.OrderQty;
        if (issuedQty.GetValueOrDefault() >= nullable.GetValueOrDefault() & issuedQty.HasValue & nullable.HasValue)
        {
          row.IssueStatus = "I";
        }
        else
        {
          nullable = row.OpenQty;
          Decimal num1 = 0M;
          if (nullable.GetValueOrDefault() == num1 & nullable.HasValue)
          {
            bool flag1 = true;
            bool flag2 = true;
            foreach (RQRequisitionLineReceived requisitionReceivedLine in this.GetRequisitionReceivedLines(cache.Graph, row))
            {
              if (requisitionReceivedLine.Status == "O")
              {
                flag1 = false;
                flag2 = false;
                break;
              }
              if (EnumerableExtensions.IsIn<string>(requisitionReceivedLine.Status, "P", "B"))
                flag2 = false;
            }
            row.IssueStatus = flag2 ? "I" : (flag1 ? "B" : "R");
            RQRequestClass requestClass = this.GetRequestClass(cache.Graph, row);
            if (requestClass == null || !requestClass.IssueRequestor.GetValueOrDefault() || !(row.IssueStatus == "I"))
              return;
            row.IssueStatus = "E";
          }
          else
          {
            nullable = row.IssuedQty;
            Decimal num2 = 0M;
            if (nullable.GetValueOrDefault() > num2 & nullable.HasValue)
              row.IssueStatus = "P";
            else
              row.IssueStatus = "O";
          }
        }
      }
    }

    protected virtual RQRequestClass GetRequestClass(PXGraph graph, RQRequestLine row)
    {
      return PXResult<RQRequest, RQRequestClass>.op_Implicit((PXResult<RQRequest, RQRequestClass>) PXResultset<RQRequest>.op_Implicit(PXSelectBase<RQRequest, PXViewOf<RQRequest>.BasedOn<SelectFromBase<RQRequest, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<RQRequestClass>.On<RQRequest.FK.RequestClass>>>.Where<BqlOperand<RQRequest.orderNbr, IBqlString>.IsEqual<P.AsString>>>.Config>.Select(graph, new object[1]
      {
        (object) row.OrderNbr
      })));
    }

    protected virtual IEnumerable<RQRequisitionLineReceived> GetRequisitionReceivedLines(
      PXGraph graph,
      RQRequestLine row)
    {
      return GraphHelper.RowCast<RQRequisitionLineReceived>((IEnumerable) PXSelectBase<RQRequisitionContent, PXViewOf<RQRequisitionContent>.BasedOn<SelectFromBase<RQRequisitionContent, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<RQRequisitionLineReceived>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<RQRequisitionLineReceived.reqNbr, Equal<RQRequisitionContent.reqNbr>>>>>.And<BqlOperand<RQRequisitionLineReceived.lineNbr, IBqlInt>.IsEqual<RQRequisitionContent.reqLineNbr>>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<RQRequisitionContent.orderNbr, Equal<P.AsString>>>>>.And<BqlOperand<RQRequisitionContent.lineNbr, IBqlInt>.IsEqual<P.AsInt>>>>.Config>.Select(graph, new object[2]
      {
        (object) row.OrderNbr,
        (object) row.LineNbr
      }));
    }
  }
}
