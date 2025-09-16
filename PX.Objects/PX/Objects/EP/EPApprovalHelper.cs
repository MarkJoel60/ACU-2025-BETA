// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EPApprovalHelper
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.Common.Interfaces;
using PX.Objects.CR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

#nullable enable
namespace PX.Objects.EP;

/// <summary>A helper for the approval mechanism.</summary>
public static class EPApprovalHelper
{
  public static 
  #nullable disable
  string BuildEPApprovalDetailsString(PXCache sender, IApprovalDescription currentDocument)
  {
    PX.Objects.CA.CashAccount cashAccount = PXResult<PX.Objects.CA.CashAccount>.op_Implicit(((IQueryable<PXResult<PX.Objects.CA.CashAccount>>) PXSelectBase<PX.Objects.CA.CashAccount, PXSelect<PX.Objects.CA.CashAccount>.Config>.Search<PX.Objects.CA.CashAccount.cashAccountID>(sender.Graph, (object) currentDocument.CashAccountID, Array.Empty<object>())).First<PXResult<PX.Objects.CA.CashAccount>>());
    PX.Objects.CA.PaymentMethod paymentMethod = PXResult<PX.Objects.CA.PaymentMethod>.op_Implicit(((IQueryable<PXResult<PX.Objects.CA.PaymentMethod>>) PXSelectBase<PX.Objects.CA.PaymentMethod, PXSelect<PX.Objects.CA.PaymentMethod>.Config>.Search<PX.Objects.CA.PaymentMethod.paymentMethodID>(sender.Graph, (object) currentDocument.PaymentMethodID, Array.Empty<object>())).First<PXResult<PX.Objects.CA.PaymentMethod>>());
    PX.Objects.CM.Extensions.CurrencyInfo ci = PXResult<PX.Objects.CM.Extensions.CurrencyInfo>.op_Implicit(((IQueryable<PXResult<PX.Objects.CM.Extensions.CurrencyInfo>>) PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo, PXSelect<PX.Objects.CM.Extensions.CurrencyInfo>.Config>.Search<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID>(sender.Graph, (object) currentDocument.CuryInfoID, Array.Empty<object>())).First<PXResult<PX.Objects.CM.Extensions.CurrencyInfo>>());
    return $"{cashAccount?.Descr} ({paymentMethod?.Descr}; {EPApprovalHelper.GetChargeString(currentDocument, ci)})";
  }

  private static string GetChargeString(IApprovalDescription currentDocument, PX.Objects.CM.Extensions.CurrencyInfo ci)
  {
    if (currentDocument.CuryChargeAmt.HasValue)
    {
      Decimal? curyChargeAmt = currentDocument.CuryChargeAmt;
      Decimal num = 0.0M;
      if (!(curyChargeAmt.GetValueOrDefault() == num & curyChargeAmt.HasValue))
      {
        int decimals = (int) (ci.BasePrecision ?? (short) 4);
        return string.Join("=", PXLocalizer.Localize("Charges"), Math.Round(currentDocument.CuryChargeAmt.Value, decimals, MidpointRounding.AwayFromZero).ToString("N" + decimals.ToString()));
      }
    }
    return PXLocalizer.Localize("No charges");
  }

  /// <summary>
  /// Find today delegate for approver, if no delegate found same contactID returned or exception thrown
  /// </summary>
  public static int? GetTodayApproverContactID(
    PXGraph graph,
    int? contactID,
    ref int? delegationRecordID,
    bool throwExceptionIfNoApproveFound = false,
    List<int?> list = null)
  {
    if (list == null)
      list = new List<int?>();
    else if (list.Contains(contactID))
    {
      PXTrace.WriteInformation("The delegate or their delegates are not available: {0}. The approval has not been delegated.", new object[1]
      {
        (object) list[0]
      });
      delegationRecordID = new int?();
      if (throwExceptionIfNoApproveFound)
        throw new EPApprovalHelper.PXReassignmentApproverNotAvailableException((PXErrorLevel) 3);
      return list[0];
    }
    PXResultset<EPWingmanForApprovals> source = PXSelectBase<EPWingmanForApprovals, PXViewOf<EPWingmanForApprovals>.BasedOn<SelectFromBase<EPWingmanForApprovals, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<BAccountR>.On<BqlOperand<BAccountR.bAccountID, IBqlInt>.IsEqual<EPWingman.employeeID>>>, FbqlJoins.Inner<BAccount2>.On<BqlOperand<BAccount2.bAccountID, IBqlInt>.IsEqual<EPWingman.wingmanID>>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<BAccountR.defContactID, Equal<P.AsInt>>>>, And<BqlOperand<EPWingman.startsOn, IBqlDateTime>.IsLessEqual<EPApprovalHelper.PXTimeZoneInfoToday.dayEnd>>>>.And<Brackets<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<EPWingman.expiresOn, IsNull>>>>.Or<BqlOperand<EPWingman.expiresOn, IBqlDateTime>.IsGreaterEqual<EPApprovalHelper.PXTimeZoneInfoToday.dayBegin>>>>>>.Config>.SelectSingleBound(graph, (object[]) null, new object[1]
    {
      (object) contactID
    });
    if (!((IQueryable<PXResult<EPWingmanForApprovals>>) source).Any<PXResult<EPWingmanForApprovals>>())
      return contactID;
    list.Add(contactID);
    contactID = GraphHelper.RowCast<BAccount2>((IEnumerable) source).First<BAccount2>().DefContactID;
    delegationRecordID = GraphHelper.RowCast<EPWingmanForApprovals>((IEnumerable) source).First<EPWingmanForApprovals>().RecordID;
    return EPApprovalHelper.GetTodayApproverContactID(graph, contactID, ref delegationRecordID, throwExceptionIfNoApproveFound, list);
  }

  /// <summary>Reassign approval to contactID</summary>
  public static void ReassignToContact(
    PXGraph graph,
    EPApproval approval,
    int? contactID,
    bool? ignoreApproversDelegations)
  {
    bool? nullable = ((approval != null ? (EPRule) PrimaryKeyOf<EPRule>.By<EPRule.ruleID>.ForeignKeyOf<EPApproval>.By<EPApproval.ruleID>.FindParent(graph, (EPApproval.ruleID) approval, (PKFindOptions) 0) : throw new PXSetPropertyException("Record for approving not found.", (PXErrorLevel) 5)) ?? throw new PXSetPropertyException("Reassignment of approvals is supported only for maps of the Approval Map type.", (PXErrorLevel) 5)).AllowReassignment;
    bool flag1 = false;
    if (nullable.GetValueOrDefault() == flag1 & nullable.HasValue)
      throw new PXSetPropertyException("The approval request cannot be reassigned because reassignment of approvals is not allowed in the approval map rule.", (PXErrorLevel) 5);
    nullable = ignoreApproversDelegations;
    bool flag2 = false;
    if (nullable.GetValueOrDefault() == flag2 & nullable.HasValue)
    {
      int? delegationRecordId = approval.DelegationRecordID;
      approval.OwnerID = EPApprovalHelper.GetTodayApproverContactID(graph, contactID, ref delegationRecordId, true);
      approval.DelegationRecordID = delegationRecordId;
    }
    else
    {
      approval.OwnerID = contactID;
      approval.IgnoreDelegations = new bool?(true);
    }
  }

  /// <summary>Reassign approval to today's delegate of contactID</summary>
  public static void ReassignToDelegate(PXGraph graph, EPApproval approval, int? contactID)
  {
    if (approval == null)
      throw new PXSetPropertyException("Record for approving not found.", (PXErrorLevel) 5);
    int? delegationRecordID = PrimaryKeyOf<EPRule>.By<EPRule.ruleID>.ForeignKeyOf<EPApproval>.By<EPApproval.ruleID>.FindParent(graph, (EPApproval.ruleID) approval, (PKFindOptions) 0) != null ? approval.DelegationRecordID : throw new PXSetPropertyException("Reassignment of approvals is supported only for maps of the Approval Map type.", (PXErrorLevel) 5);
    approval.OwnerID = EPApprovalHelper.GetTodayApproverContactID(graph, contactID, ref delegationRecordID, true);
    approval.DelegationRecordID = delegationRecordID;
  }

  public static class PXTimeZoneInfoToday
  {
    public class dayBegin : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Constant<
      #nullable disable
      EPApprovalHelper.PXTimeZoneInfoToday.dayBegin>
    {
      public dayBegin()
        : base(PXTimeZoneInfo.Today)
      {
      }
    }

    public class dayEnd : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Constant<
      #nullable disable
      EPApprovalHelper.PXTimeZoneInfoToday.dayEnd>
    {
      public dayEnd()
      {
        DateTime dateTime = PXTimeZoneInfo.Today;
        dateTime = dateTime.AddDays(1.0);
        // ISSUE: explicit constructor call
        base.\u002Ector(dateTime.AddSeconds(-1.0));
      }
    }
  }

  [Serializable]
  internal class PXReassignmentApproverNotAvailableException : PXSetPropertyException
  {
    public PXReassignmentApproverNotAvailableException(PXErrorLevel errorLevel)
      : base("The selected approver or their delegates are not available for the specified period. Select another approver.", errorLevel)
    {
    }

    public PXReassignmentApproverNotAvailableException(
      SerializationInfo info,
      StreamingContext context)
      : base(info, context)
    {
    }
  }
}
