// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARAutoApplyPayments
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Api;
using PX.Common;
using PX.Data;
using PX.Objects.AR.BQL;
using PX.Objects.AR.MigrationMode;
using PX.Objects.GL;
using PX.Objects.GL.FinPeriods;
using PX.Objects.GL.FinPeriods.TableDefinition;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.AR;

[TableAndChartDashboardType]
public class ARAutoApplyPayments : PXGraph<ARAutoApplyPayments>
{
  public PXCancel<ARAutoApplyParameters> Cancel;
  public PXFilter<ARAutoApplyParameters> Filter;
  [PXFilterable(new Type[] {})]
  public PXFilteredProcessing<ARStatementCycle, ARAutoApplyParameters> ARStatementCycleList;
  public ARSetupNoMigrationMode arsetup;

  public ARAutoApplyPayments()
  {
    ARSetupNoMigrationMode.EnsureMigrationModeDisabled((PXGraph) this);
    OpenPeriodAttribute.SetValidatePeriod<ARAutoApplyParameters.finPeriodID>(((PXSelectBase) this.Filter).Cache, (object) null, ((PXGraph) this).IsContractBasedAPI || ((PXGraph) this).IsImport || ((PXGraph) this).IsExport || ((PXGraph) this).UnattendedMode ? PeriodValidation.DefaultUpdate : PeriodValidation.DefaultSelectUpdate);
  }

  protected virtual void ARAutoApplyParameters_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    ARAutoApplyPayments.\u003C\u003Ec__DisplayClass5_0 cDisplayClass50 = new ARAutoApplyPayments.\u003C\u003Ec__DisplayClass5_0();
    if (e.Row == null)
      return;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass50.filter = ((PXSelectBase<ARAutoApplyParameters>) this.Filter).Current;
    // ISSUE: method pointer
    ((PXProcessingBase<ARStatementCycle>) this.ARStatementCycleList).SetProcessDelegate<ARPaymentEntry>(new PXProcessingBase<ARStatementCycle>.ProcessItemDelegate<ARPaymentEntry>((object) cDisplayClass50, __methodptr(\u003CARAutoApplyParameters_RowSelected\u003Eb__0)));
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    PXStringListAttribute.SetList<ARAutoApplyParameters.loadChildDocuments>(((PXSelectBase) this.Filter).Cache, (object) cDisplayClass50.filter, cDisplayClass50.filter.ApplyCreditMemos.GetValueOrDefault() ? (PXStringListAttribute) new ARPaymentEntry.LoadOptions.loadChildDocuments.ListAttribute() : (PXStringListAttribute) new ARPaymentEntry.LoadOptions.loadChildDocuments.NoCRMListAttribute());
  }

  protected virtual void ARAutoApplyParameters_ApplyCreditMemos_FieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
    ARAutoApplyParameters row = e.Row as ARAutoApplyParameters;
    bool? applyCreditMemos = row.ApplyCreditMemos;
    bool flag = false;
    if (!(applyCreditMemos.GetValueOrDefault() == flag & applyCreditMemos.HasValue) || !(row.LoadChildDocuments == "INCRM"))
      return;
    row.LoadChildDocuments = "EXCRM";
  }

  private static IEnumerable<Customer> GetCustomersForAutoApplication(
    PXGraph graph,
    ARStatementCycle cycle,
    ARAutoApplyParameters filter)
  {
    List<Customer> result = new List<Customer>();
    if (filter.LoadChildDocuments != "NOONE")
    {
      PXResultset<Customer> pxResultset = PXSelectBase<Customer, PXSelectJoin<Customer, InnerJoin<CustomerMaster, On<Customer.parentBAccountID, Equal<CustomerMaster.bAccountID>, And<Customer.consolidateToParent, Equal<True>>>, InnerJoin<CustomerWithOpenInvoices, On<CustomerWithOpenInvoices.customerID, Equal<Customer.bAccountID>>>>, Where<CustomerMaster.statementCycleId, Equal<Required<Customer.statementCycleId>>, And<Match<Current<AccessInfo.userName>>>>>.Config>.Select(graph, new object[1]
      {
        (object) cycle.StatementCycleId
      });
      result.AddRange(GraphHelper.RowCast<Customer>((IEnumerable) pxResultset));
    }
    List<Customer> list = GraphHelper.RowCast<Customer>((IEnumerable) PXSelectBase<Customer, PXSelectJoin<Customer, LeftJoin<CustomerMaster, On<Customer.parentBAccountID, Equal<CustomerMaster.bAccountID>, And<Customer.consolidateToParent, Equal<True>>>, InnerJoin<CustomerWithOpenPayments, On<CustomerWithOpenPayments.customerID, Equal<Customer.bAccountID>, And<CustomerWithOpenPayments.statementCycleId, Equal<Required<CustomerWithOpenPayments.statementCycleId>>>>>>, Where<Customer.statementCycleId, Equal<Required<Customer.statementCycleId>>, And2<Match<Current<AccessInfo.userName>>, And<Where<CustomerMaster.bAccountID, IsNull, Or<CustomerMaster.statementCycleId, NotEqual<Required<CustomerMaster.statementCycleId>>>>>>>>.Config>.Select(graph, new object[3]
    {
      (object) cycle.StatementCycleId,
      (object) cycle.StatementCycleId,
      (object) cycle.StatementCycleId
    })).Where<Customer>((Func<Customer, bool>) (c => result.FirstOrDefault<Customer>((Func<Customer, bool>) (alreadyPresent =>
    {
      int? baccountId1 = alreadyPresent.BAccountID;
      int? baccountId2 = c.BAccountID;
      return baccountId1.GetValueOrDefault() == baccountId2.GetValueOrDefault() & baccountId1.HasValue == baccountId2.HasValue;
    })) == null)).ToList<Customer>();
    result.AddRange((IEnumerable<Customer>) list);
    return (IEnumerable<Customer>) result;
  }

  public static void ProcessDoc(
    ARPaymentEntry graph,
    ARStatementCycle cycle,
    ARAutoApplyParameters filter)
  {
    List<ARRegister> arRegisterList = new List<ARRegister>();
    HashSet<string> source = new HashSet<string>();
    string warningRefNbr = string.Empty;
    int?[] array = graph.CurrentUserInformationProvider.GetAllBranches().Select<BranchInfo, int?>((Func<BranchInfo, int?>) (b => new int?(b.Id))).Distinct<int?>().ToArray<int?>();
    foreach (Customer customer1 in ARAutoApplyPayments.GetCustomersForAutoApplication((PXGraph) graph, cycle, filter))
    {
      Customer customer = customer1;
      List<ARInvoice> arInvoiceList = new List<ARInvoice>();
      PXSelectJoin<ARInvoice, InnerJoin<Customer, On<ARInvoice.customerID, Equal<Customer.bAccountID>>>, Where<ARInvoice.released, Equal<True>, And<ARInvoice.openDoc, Equal<True>, And<ARInvoice.pendingPPD, NotEqual<True>, And<Where<ARInvoice.docType, Equal<ARDocType.invoice>, Or<ARInvoice.docType, Equal<ARDocType.finCharge>, Or<ARInvoice.docType, Equal<ARDocType.debitMemo>>>>>>>>, OrderBy<Asc<ARInvoice.dueDate>>> pxSelectJoin = new PXSelectJoin<ARInvoice, InnerJoin<Customer, On<ARInvoice.customerID, Equal<Customer.bAccountID>>>, Where<ARInvoice.released, Equal<True>, And<ARInvoice.openDoc, Equal<True>, And<ARInvoice.pendingPPD, NotEqual<True>, And<Where<ARInvoice.docType, Equal<ARDocType.invoice>, Or<ARInvoice.docType, Equal<ARDocType.finCharge>, Or<ARInvoice.docType, Equal<ARDocType.debitMemo>>>>>>>>, OrderBy<Asc<ARInvoice.dueDate>>>((PXGraph) graph);
      List<object> objectList = new List<object>();
      if (filter.LoadChildDocuments == "NOONE" || customer.ParentBAccountID.HasValue)
        ((PXSelectBase<ARInvoice>) pxSelectJoin).WhereAnd<Where<Customer.bAccountID, Equal<Required<ARInvoice.customerID>>>>();
      else
        ((PXSelectBase<ARInvoice>) pxSelectJoin).WhereAnd<Where<Customer.consolidatingBAccountID, Equal<Required<ARInvoice.customerID>>>>();
      objectList.Add((object) customer.BAccountID);
      if (filter.ApplicationDate.HasValue)
      {
        ((PXSelectBase<ARInvoice>) pxSelectJoin).WhereAnd<Where<ARInvoice.docDate, LessEqual<Required<ARInvoice.docDate>>>>();
        objectList.Add((object) filter.ApplicationDate);
      }
      PXResultset<ARInvoice> pxResultset;
      using (new PXReadBranchRestrictedScope((int?[]) null, array, true, false))
        pxResultset = ((PXSelectBase<ARInvoice>) pxSelectJoin).Select(objectList.ToArray());
      foreach (PXResult<ARInvoice> pxResult in pxResultset)
      {
        ARInvoice arInvoice = PXResult<ARInvoice>.op_Implicit(pxResult);
        arInvoiceList.Add(arInvoice);
      }
      arInvoiceList.Sort((Comparison<ARInvoice>) ((a, b) =>
      {
        if (((PXSelectBase<ARSetup>) graph.arsetup).Current.FinChargeFirst.Value)
        {
          int num = ((IComparable) (!(a.DocType == "FCH") ? 1 : 0)).CompareTo((object) (!(b.DocType == "FCH") ? 1 : 0));
          if (num != 0)
            return num;
        }
        return ((IComparable) a.DueDate).CompareTo((object) b.DueDate);
      }));
      if (arInvoiceList.Count > 0)
      {
        PXSelectBase<ARPayment> pxSelectBase = (PXSelectBase<ARPayment>) new PXSelectJoin<ARPayment, InnerJoin<PX.Objects.GL.Branch, On<ARPayment.branchID, Equal<PX.Objects.GL.Branch.branchID>>, InnerJoin<OrganizationFinPeriod, On<PX.Objects.GL.Branch.organizationID, Equal<OrganizationFinPeriod.organizationID>, And<OrganizationFinPeriod.finPeriodID, Equal<Required<OrganizationFinPeriod.finPeriodID>>>>>>, Where<OrganizationFinPeriod.status, NotEqual<FinPeriod.status.locked>, And<OrganizationFinPeriod.status, NotEqual<FinPeriod.status.inactive>, And<ARPayment.released, Equal<True>, And<ARPayment.openDoc, Equal<True>, And<ARPayment.customerID, Equal<Required<ARPayment.customerID>>, And<ARPayment.adjTranPeriodID, LessEqual<Required<ARPayment.adjTranPeriodID>>, And2<Not<HasUnreleasedVoidPayment<ARPayment.docType, ARPayment.refNbr>>, And2<Not<HasUnreleasedIncomingApplication<ARPayment.docType, ARPayment.refNbr>>, And2<Not<HasApplicationToUnreleasedCM<ARPayment.docType, ARPayment.refNbr>>, And<Where<ARPayment.docType, Equal<ARDocType.payment>, Or<ARPayment.docType, Equal<ARDocType.prepayment>, Or<ARPayment.docType, Equal<Required<ARPayment.docType>>>>>>>>>>>>>>>, OrderBy<Asc<ARPayment.docDate>>>((PXGraph) graph);
        if (!graph.FinPeriodUtils.CanPostToClosedPeriod())
          pxSelectBase.WhereAnd<Where<OrganizationFinPeriod.status, NotEqual<FinPeriod.status.closed>>>();
        foreach (PXResult<ARPayment> pxResult in pxSelectBase.Select(new object[4]
        {
          (object) filter.FinPeriodID,
          (object) customer.BAccountID,
          (object) filter.FinPeriodID,
          filter.ApplyCreditMemos.GetValueOrDefault() ? (object) "CRM" : (object) "PMT"
        }))
        {
          ARPayment arPayment = PXResult<ARPayment>.op_Implicit(pxResult);
          if (graph.VerifyAdjTranPeriodID(arPayment, filter.FinPeriodID))
          {
            ARAutoApplyPayments.ApplyPayment(graph, filter, arPayment, arInvoiceList, arRegisterList, out warningRefNbr);
            source.Add(warningRefNbr);
          }
        }
      }
      List<ARInvoice> list = arInvoiceList.Where<ARInvoice>((Func<ARInvoice, bool>) (inv =>
      {
        int? customerId = inv.CustomerID;
        int? baccountId = customer.BAccountID;
        return customerId.GetValueOrDefault() == baccountId.GetValueOrDefault() & customerId.HasValue == baccountId.HasValue;
      })).ToList<ARInvoice>();
      if (list.Count > 0 && filter.ApplyCreditMemos.GetValueOrDefault() && filter.LoadChildDocuments == "INCRM")
      {
        PXSelectBase<ARPayment> pxSelectBase = (PXSelectBase<ARPayment>) new PXSelectJoin<ARPayment, InnerJoin<Customer, On<ARPayment.customerID, Equal<Customer.bAccountID>>, InnerJoin<PX.Objects.GL.Branch, On<ARPayment.branchID, Equal<PX.Objects.GL.Branch.branchID>>, InnerJoin<OrganizationFinPeriod, On<PX.Objects.GL.Branch.organizationID, Equal<OrganizationFinPeriod.organizationID>, And<OrganizationFinPeriod.finPeriodID, Equal<Required<OrganizationFinPeriod.finPeriodID>>>>>>>, Where<OrganizationFinPeriod.status, NotEqual<FinPeriod.status.locked>, And<OrganizationFinPeriod.status, NotEqual<FinPeriod.status.inactive>, And<ARPayment.released, Equal<True>, And<ARPayment.openDoc, Equal<True>, And<Customer.consolidatingBAccountID, Equal<Required<Customer.consolidatingBAccountID>>, And<ARPayment.docType, Equal<ARDocType.creditMemo>, And2<Not<HasUnreleasedVoidPayment<ARPayment.docType, ARPayment.refNbr>>, And2<Not<HasUnreleasedIncomingApplication<ARPayment.docType, ARPayment.refNbr>>, And<Not<HasApplicationToUnreleasedCM<ARPayment.docType, ARPayment.refNbr>>>>>>>>>>>, OrderBy<Asc<ARPayment.docDate>>>((PXGraph) graph);
        if (!graph.FinPeriodUtils.CanPostToClosedPeriod())
          pxSelectBase.WhereAnd<Where<OrganizationFinPeriod.status, NotEqual<FinPeriod.status.closed>>>();
        foreach (PXResult<ARPayment> pxResult in pxSelectBase.Select(new object[2]
        {
          (object) filter.FinPeriodID,
          (object) customer.BAccountID
        }))
        {
          ARPayment payment = PXResult<ARPayment>.op_Implicit(pxResult);
          ARAutoApplyPayments.ApplyPayment(graph, filter, payment, list, arRegisterList, out warningRefNbr);
          source.Add(warningRefNbr);
        }
      }
    }
    if (source != null && source.Count != 0)
    {
      PXTrace.WriteWarning($"The following payments have not been processed: {EnumerableEx.JoinToString<string>(source.Where<string>((Func<string, bool>) (x => !Str.IsNullOrEmpty(x))), "; ")}.");
      source.Clear();
    }
    if (arRegisterList.Count <= 0)
      return;
    using (new PXTimeStampScope((byte[]) null))
      ARDocumentRelease.ReleaseDoc(arRegisterList, false);
  }

  private static void ApplyPayment(
    ARPaymentEntry graph,
    ARAutoApplyParameters filter,
    ARPayment payment,
    List<ARInvoice> arInvoiceList,
    List<ARRegister> toRelease,
    out string warningRefNbr)
  {
    warningRefNbr = string.Empty;
    if (!arInvoiceList.Any<ARInvoice>())
      return;
    DateTime? docDate = payment.DocDate;
    DateTime? applicationDate = filter.ApplicationDate;
    if ((docDate.HasValue & applicationDate.HasValue ? (docDate.GetValueOrDefault() > applicationDate.GetValueOrDefault() ? 1 : 0) : 0) != 0)
    {
      warningRefNbr = payment.RefNbr;
      PXProcessing<ARStatementCycle>.SetWarning("Some payments in the system have not been processed because their payment dates are later than the application date selected for processing. See the trace log for more details.");
    }
    else
    {
      int index = 0;
      List<ARInvoice> arInvoiceList1 = new List<ARInvoice>((IEnumerable<ARInvoice>) arInvoiceList);
      ((PXGraph) graph).Clear();
      ((PXSelectBase<ARPayment>) graph.Document).Current = payment;
      bool flag = false;
      while (true)
      {
        Decimal? curyUnappliedBal = ((PXSelectBase<ARPayment>) graph.Document).Current.CuryUnappliedBal;
        Decimal num1 = 0M;
        if (curyUnappliedBal.GetValueOrDefault() > num1 & curyUnappliedBal.HasValue && index < arInvoiceList1.Count)
        {
          Decimal? nullable = ((PXSelectBase<ARPayment>) graph.Document).Current.CuryApplAmt;
          if (!nullable.HasValue)
          {
            object valueExt = ((PXSelectBase) graph.Document).Cache.GetValueExt<ARPayment.curyApplAmt>((object) ((PXSelectBase<ARPayment>) graph.Document).Current);
            if (valueExt is PXFieldState)
              valueExt = ((PXFieldState) valueExt).Value;
            ((PXSelectBase<ARPayment>) graph.Document).Current.CuryApplAmt = (Decimal?) valueExt;
          }
          ((PXSelectBase<ARPayment>) graph.Document).Current.AdjDate = filter.ApplicationDate;
          FinPeriodIDAttribute.SetPeriodsByMaster<ARPayment.adjFinPeriodID>(((PXSelectBase) graph.Document).Cache, (object) ((PXSelectBase<ARPayment>) graph.Document).Current, filter.FinPeriodID);
          PXCacheEx.Adjust<AROpenPeriodAttribute>(((PXSelectBase) graph.Document).Cache, (object) null).For<ARPayment.adjFinPeriodID>((Action<AROpenPeriodAttribute>) (atr => atr.RedefaultOnDateChanged = false));
          ((PXSelectBase) graph.Document).Cache.Update((object) ((PXSelectBase<ARPayment>) graph.Document).Current);
          ARInvoice arInvoice = arInvoiceList1[index];
          PXResultset<ARTran> pxResultset1;
          if (!arInvoice.PaymentsByLinesAllowed.GetValueOrDefault())
          {
            PXResultset<ARTran> pxResultset2 = new PXResultset<ARTran>();
            pxResultset2.Add((PXResult<ARTran>) null);
            pxResultset1 = pxResultset2;
          }
          else
            pxResultset1 = PXSelectBase<ARTran, PXSelect<ARTran, Where<ARTran.tranType, Equal<Required<ARTran.tranType>>, And<ARTran.refNbr, Equal<Required<ARTran.refNbr>>>>>.Config>.Select((PXGraph) graph, new object[2]
            {
              (object) arInvoice.DocType,
              (object) arInvoice.RefNbr
            });
          foreach (PXResult<ARTran> pxResult in pxResultset1)
          {
            ARTran arTran = PXResult<ARTran>.op_Implicit(pxResult);
            graph.AutoPaymentApp = true;
            ARAdjust arAdjust = ((PXSelectBase<ARAdjust>) graph.Adjustments).Insert(new ARAdjust()
            {
              AdjdDocType = arInvoice.DocType,
              AdjdRefNbr = arInvoice.RefNbr,
              AdjdLineNbr = new int?(((int?) arTran?.LineNbr).GetValueOrDefault())
            });
            if (arAdjust != null)
            {
              flag = true;
              nullable = arAdjust.CuryDocBal;
              Decimal num2 = 0M;
              if (nullable.GetValueOrDefault() <= num2 & nullable.HasValue)
                arInvoiceList.Remove(arInvoice);
            }
          }
          ++index;
        }
        else
          break;
      }
      if (!flag)
        return;
      ((PXAction) graph.Save).Press();
      if (!filter.ReleaseBatchWhenFinished.GetValueOrDefault())
        return;
      toRelease.Add((ARRegister) ((PXSelectBase<ARPayment>) graph.Document).Current);
    }
  }

  protected virtual void ARStatementCycle_RowSelecting(PXCache sender, PXRowSelectingEventArgs e)
  {
    if (e.Row == null)
      return;
    ARStatementCycle row = e.Row as ARStatementCycle;
    ARStatementCycle arStatementCycle1 = row;
    PXGraph graph1 = sender.Graph;
    DateTime aBeforeDate = ((PXGraph) this).Accessinfo.BusinessDate.Value;
    string prepareOn1 = row.PrepareOn;
    short? day00 = row.Day00;
    int? aDay00_1 = day00.HasValue ? new int?((int) day00.GetValueOrDefault()) : new int?();
    short? nullable1 = row.Day01;
    int? aDay01_1 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
    int? dayOfWeek1 = row.DayOfWeek;
    DateTime? nullable2 = new DateTime?(ARStatementProcess.CalcStatementDateBefore(graph1, aBeforeDate, prepareOn1, aDay00_1, aDay01_1, dayOfWeek1));
    arStatementCycle1.NextStmtDate = nullable2;
    DateTime? nullable3 = row.LastStmtDate;
    if (!nullable3.HasValue)
      return;
    nullable3 = row.NextStmtDate;
    DateTime? lastStmtDate = row.LastStmtDate;
    if ((nullable3.HasValue & lastStmtDate.HasValue ? (nullable3.GetValueOrDefault() <= lastStmtDate.GetValueOrDefault() ? 1 : 0) : 0) == 0)
      return;
    ARStatementCycle arStatementCycle2 = row;
    PXGraph graph2 = sender.Graph;
    lastStmtDate = row.LastStmtDate;
    DateTime aLastStmtDate = lastStmtDate.Value;
    string prepareOn2 = row.PrepareOn;
    nullable1 = row.Day00;
    int? aDay00_2 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
    nullable1 = row.Day01;
    int? aDay01_2 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
    int? dayOfWeek2 = row.DayOfWeek;
    DateTime? nullable4 = ARStatementProcess.CalcNextStatementDate(graph2, aLastStmtDate, prepareOn2, aDay00_2, aDay01_2, dayOfWeek2);
    arStatementCycle2.NextStmtDate = nullable4;
  }
}
