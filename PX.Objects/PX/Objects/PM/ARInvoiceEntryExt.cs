// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.ARInvoiceEntryExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.WorkflowAPI;
using PX.Objects.AR;
using PX.Objects.Common.Scopes;
using PX.Objects.CS;
using PX.Objects.CT;
using PX.Objects.Extensions.MultiCurrency;
using PX.Objects.IN;
using System;
using System.Collections;
using System.Linq.Expressions;
using System.Text;

#nullable disable
namespace PX.Objects.PM;

public class ARInvoiceEntryExt : PXGraphExtension<ARInvoiceEntry>
{
  [PXCopyPasteHiddenView]
  public PXSelect<PMBillingRecord> ProjectBillingRecord;
  [PXCopyPasteHiddenView]
  public PXSelect<PMProforma> ProjectProforma;
  [PXCopyPasteHiddenView]
  public PXSelect<PMRegister> ProjectRegister;
  public PXSelect<PMBudgetAccum> Budget;
  public PXAction<PX.Objects.AR.ARInvoice> viewProforma;
  public PXAction<PX.Objects.AR.ARInvoice> viewPMTrans;
  private bool isARInvoiceDeleting;

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.projectAccounting>();

  [InjectDependency]
  public IProjectMultiCurrency MultiCurrencyService { get; set; }

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = false)]
  public virtual IEnumerable ViewProforma(PXAdapter adapter)
  {
    if (((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Base.Document).Current != null && ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Base.Document).Current.ProformaExists.GetValueOrDefault())
    {
      ProformaEntry instance = PXGraph.CreateInstance<ProformaEntry>();
      ((PXSelectBase<PMProforma>) instance.Document).Current = PXResultset<PMProforma>.op_Implicit(PXSelectBase<PMProforma, PXSelect<PMProforma, Where<PMProforma.aRInvoiceDocType, Equal<Current<PX.Objects.AR.ARInvoice.docType>>, And<PMProforma.aRInvoiceRefNbr, Equal<Current<PX.Objects.AR.ARInvoice.refNbr>>, Or<PMProforma.aRInvoiceRefNbr, Equal<Current<PX.Objects.AR.ARInvoice.origRefNbr>>>>>>.Config>.Select((PXGraph) this.Base, Array.Empty<object>()));
      ProjectAccountingService.NavigateToScreen((PXGraph) instance);
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = false)]
  public virtual IEnumerable ViewPMTrans(PXAdapter adapter)
  {
    if (((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Base.Document).Current != null)
    {
      TransactionInquiry instance = PXGraph.CreateInstance<TransactionInquiry>();
      TransactionInquiry.TranFilter tranFilter = ((PXSelectBase<TransactionInquiry.TranFilter>) instance.Filter).Insert();
      tranFilter.ARDocType = ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Base.Document).Current.DocType;
      tranFilter.ARRefNbr = ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Base.Document).Current.RefNbr;
      ProjectAccountingService.NavigateToScreen((PXGraph) instance);
    }
    return adapter.Get();
  }

  protected virtual void _(PX.Data.Events.RowSelected<PX.Objects.AR.ARInvoice> e)
  {
    PX.Objects.AR.ARInvoice row = e.Row;
    bool flag = row != null && row.ProformaExists.GetValueOrDefault();
    ((PXAction) this.viewProforma).SetEnabled(flag);
    this.SetViewPMTransEnabled(row);
    ((PXSelectBase) this.Base.Taxes).Cache.AllowUpdate &= !flag;
    ((PXSelectBase) this.Base.Taxes).Cache.AllowInsert &= !flag;
    ((PXSelectBase) this.Base.Taxes).Cache.AllowDelete &= !flag;
    if (flag)
    {
      PXUIFieldAttribute.SetEnabled<PX.Objects.AR.ARInvoice.customerID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.AR.ARInvoice>>) e).Cache, (object) e.Row, false);
      PXUIFieldAttribute.SetEnabled<PX.Objects.AR.ARInvoice.projectID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.AR.ARInvoice>>) e).Cache, (object) e.Row, false);
      PXUIFieldAttribute.SetEnabled<PX.Objects.AR.ARInvoice.taxCalcMode>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.AR.ARInvoice>>) e).Cache, (object) e.Row, false);
    }
    if (row != null)
      this.CheckNoProFormaInvoiceForProFormaBilledProject(row);
    if (ProjectAttribute.IsPMVisible("AR"))
      return;
    PXUIFieldAttribute.SetVisible<PX.Objects.AR.ARTran.taskID>(((PXSelectBase) this.Base.Transactions).Cache, (object) null, ProjectDefaultAttribute.IsProject((PXGraph) this.Base, (int?) row?.ProjectID));
  }

  private void SetViewPMTransEnabled(PX.Objects.AR.ARInvoice doc)
  {
    bool flag = false;
    PMProject project;
    if (doc != null && ProjectDefaultAttribute.IsProject((PXGraph) this.Base, doc.ProjectID, out project))
      flag = project.BaseType == "P";
    ((PXAction) this.viewPMTrans).SetEnabled(flag);
  }

  protected virtual void _(PX.Data.Events.RowDeleting<PX.Objects.AR.ARInvoice> e)
  {
    if (e.Row != null && e.Row.ProjectID.HasValue)
    {
      int? projectId = e.Row.ProjectID;
      int? nullable = ProjectDefaultAttribute.NonProject();
      if (!(projectId.GetValueOrDefault() == nullable.GetValueOrDefault() & projectId.HasValue == nullable.HasValue))
      {
        PXResultset<PMBillingRecord> pxResultset = ((PXSelectBase<PMBillingRecord>) new PXSelectJoin<PMBillingRecord, InnerJoin<PMBillingRecordEx, On<PMBillingRecord.projectID, Equal<PMBillingRecordEx.projectID>, And<PMBillingRecord.billingTag, Equal<PMBillingRecordEx.billingTag>, And<PMBillingRecord.recordID, Less<PMBillingRecordEx.recordID>, And<PMBillingRecordEx.proformaRefNbr, IsNotNull, And<PMBillingRecordEx.aRRefNbr, IsNotNull>>>>>>, Where<PMBillingRecord.projectID, Equal<Required<PMBillingRecord.projectID>>, And<PMBillingRecord.aRDocType, Equal<Required<PMBillingRecord.aRDocType>>, And<PMBillingRecord.aRRefNbr, Equal<Required<PMBillingRecord.aRRefNbr>>>>>>((PXGraph) this.Base)).Select(new object[3]
        {
          (object) e.Row.ProjectID,
          (object) e.Row.DocType,
          (object) e.Row.RefNbr
        });
        if (pxResultset.Count > 0)
        {
          StringBuilder stringBuilder = new StringBuilder();
          int num = 0;
          foreach (PXResult<PMBillingRecord, PMBillingRecordEx> pxResult in pxResultset)
          {
            PMBillingRecordEx pmBillingRecordEx = PXResult<PMBillingRecord, PMBillingRecordEx>.op_Implicit(pxResult);
            stringBuilder.AppendFormat("{0}-{1},", (object) pmBillingRecordEx.ARDocType, (object) pmBillingRecordEx.ARRefNbr);
            if (++num == 10)
              break;
          }
          throw new PXException("You cannot delete the document that refers to a pro forma invoice because pro forma invoices can be reopened starting from the last one. To delete the document, at first delete the following documents: {0}.", new object[1]
          {
            (object) stringBuilder.ToString().TrimEnd(',')
          });
        }
      }
    }
    this.isARInvoiceDeleting = true;
  }

  protected virtual void _(PX.Data.Events.RowDeleted<PX.Objects.AR.ARInvoice> e)
  {
    PXResultset<PMBillingRecord> pxResultset = ((PXSelectBase<PMBillingRecord>) new PXSelectJoin<PMBillingRecord, LeftJoin<PMProforma, On<PMBillingRecord.proformaRefNbr, Equal<PMProforma.refNbr>, And<PMProforma.corrected, Equal<False>>>>, Where<PMBillingRecord.aRDocType, Equal<Required<PMBillingRecord.aRDocType>>, And<PMBillingRecord.aRRefNbr, Equal<Required<PMBillingRecord.aRRefNbr>>>>>((PXGraph) this.Base)).Select(new object[2]
    {
      (object) e.Row.DocType,
      (object) e.Row.RefNbr
    });
    if (pxResultset.Count > 0)
    {
      PMBillingRecord pmBillingRecord = PXResult.Unwrap<PMBillingRecord>((object) pxResultset[0]);
      if (pmBillingRecord != null)
      {
        if (pmBillingRecord.ProformaRefNbr != null)
        {
          pmBillingRecord.ARDocType = (string) null;
          pmBillingRecord.ARRefNbr = (string) null;
          ((PXSelectBase<PMBillingRecord>) this.ProjectBillingRecord).Update(pmBillingRecord);
          PMProforma proforma = PXResult.Unwrap<PMProforma>((object) pxResultset[0]);
          if (proforma != null && !string.IsNullOrEmpty(proforma.RefNbr))
          {
            this.ReopenProforma(proforma);
            ((PXSelectBase<PMProforma>) this.ProjectProforma).Update(proforma);
          }
        }
        else
          ((PXSelectBase<PMBillingRecord>) this.ProjectBillingRecord).Delete(pmBillingRecord);
      }
      PMRegister pmRegister = PXResultset<PMRegister>.op_Implicit(PXSelectBase<PMRegister, PXSelect<PMRegister, Where<PMRegister.origDocType, Equal<PMOrigDocType.allocationReversal>, And<PMRegister.origNoteID, Equal<Required<PX.Objects.AR.ARInvoice.noteID>>, And<PMRegister.released, Equal<False>>>>>.Config>.Select((PXGraph) this.Base, new object[1]
      {
        (object) e.Row.NoteID
      }));
      if (pmRegister != null)
        ((PXSelectBase<PMRegister>) this.ProjectRegister).Delete(pmRegister);
    }
    this.AddToUnbilledSummary(e.Row);
    foreach (PXResult<PMProforma> pxResult in PXSelectBase<PMProforma, PXViewOf<PMProforma>.BasedOn<SelectFromBase<PMProforma, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMProforma.aRInvoiceDocType, Equal<P.AsString>>>>>.And<BqlOperand<PMProforma.aRInvoiceRefNbr, IBqlString>.IsEqual<P.AsString>>>>.Config>.Select((PXGraph) this.Base, new object[2]
    {
      (object) e.Row.DocType,
      (object) e.Row.RefNbr
    }))
    {
      PMProforma proforma = PXResult<PMProforma>.op_Implicit(pxResult);
      this.ReopenProforma(proforma);
      ((PXSelectBase<PMProforma>) this.ProjectProforma).Update(proforma);
    }
  }

  protected virtual void ReopenProforma(PMProforma proforma)
  {
    proforma.ARInvoiceDocType = (string) null;
    proforma.ARInvoiceRefNbr = (string) null;
    if (!proforma.Released.GetValueOrDefault())
      return;
    proforma.Released = new bool?(false);
    proforma.Status = "O";
  }

  protected virtual void _(PX.Data.Events.RowInserted<PX.Objects.AR.ARTran> e)
  {
    if (((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Base.Document).Current.IsRetainageDocument.GetValueOrDefault() || !e.Row.TaskID.HasValue || ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Base.Document).Current.ProformaExists.GetValueOrDefault())
      return;
    PX.Objects.AR.ARTran row1 = e.Row;
    int? projectedAccountGroup1 = this.GetProjectedAccountGroup(e.Row);
    Decimal? nullable = ARDocType.SignAmount(e.Row.TranType);
    int mult1 = (int) (nullable ?? 1M);
    this.AddToInvoiced(row1, projectedAccountGroup1, mult1);
    PX.Objects.AR.ARTran row2 = e.Row;
    int? projectedAccountGroup2 = this.GetProjectedAccountGroup(e.Row);
    nullable = ARDocType.SignAmount(e.Row.TranType);
    int mult2 = (int) (nullable ?? 1M);
    this.AddToDraftRetained(row2, projectedAccountGroup2, mult2);
    this.RemoveObsoleteLines();
  }

  protected virtual void _(PX.Data.Events.RowUpdated<PX.Objects.AR.ARTran> e)
  {
    if (e.Row == null)
      return;
    this.SyncBudgets(e.Row, e.OldRow);
  }

  protected virtual void _(PX.Data.Events.RowUpdated<PX.Objects.CM.Extensions.CurrencyInfo> e)
  {
    if (e.Row == null)
      return;
    foreach (PXResult<PX.Objects.AR.ARTran> pxResult in ((PXSelectBase<PX.Objects.AR.ARTran>) this.Base.Transactions).Select(Array.Empty<object>()))
    {
      PX.Objects.AR.ARTran arTran = PXResult<PX.Objects.AR.ARTran>.op_Implicit(pxResult);
      Decimal num1 = 0M;
      Decimal? nullable = e.Row.CuryRate;
      if (nullable.HasValue)
      {
        PX.Objects.CM.Extensions.CurrencyInfo row = e.Row;
        nullable = arTran.CuryTranAmt;
        Decimal valueOrDefault = nullable.GetValueOrDefault();
        num1 = row.CuryConvBase(valueOrDefault);
      }
      PX.Objects.AR.ARTran copy1 = ((PXSelectBase) this.Base.Transactions).Cache.CreateCopy((object) arTran) as PX.Objects.AR.ARTran;
      copy1.TranAmt = new Decimal?(num1);
      Decimal num2 = 0M;
      nullable = e.OldRow.CuryRate;
      if (nullable.HasValue)
      {
        PX.Objects.CM.Extensions.CurrencyInfo oldRow = e.OldRow;
        nullable = arTran.CuryTranAmt;
        Decimal valueOrDefault = nullable.GetValueOrDefault();
        num2 = oldRow.CuryConvBase(valueOrDefault);
      }
      PX.Objects.AR.ARTran copy2 = ((PXSelectBase) this.Base.Transactions).Cache.CreateCopy((object) arTran) as PX.Objects.AR.ARTran;
      copy2.TranAmt = new Decimal?(num2);
      this.SyncBudgets(copy1, copy2);
    }
  }

  protected virtual void _(PX.Data.Events.RowDeleted<PX.Objects.AR.ARTran> e)
  {
    if (!((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Base.Document).Current.IsRetainageDocument.GetValueOrDefault() && e.Row.TaskID.HasValue && !((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Base.Document).Current.ProformaExists.GetValueOrDefault() && !((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Base.Document).Current.IsMigratedRecord.GetValueOrDefault())
    {
      this.AddToInvoiced(e.Row, this.GetProjectedAccountGroup(e.Row), -1 * (int) (ARDocType.SignAmount(e.Row.TranType) ?? 1M));
      this.AddToDraftRetained(e.Row, this.GetProjectedAccountGroup(e.Row), -1 * (int) (ARDocType.SignAmount(e.Row.TranType) ?? 1M));
      string tranType = e.Row.TranType;
      string refNbr = e.Row.RefNbr;
      int? lineNbr = e.Row.LineNbr;
      PMTran pmTran;
      if (tranType == "CRM" && !string.IsNullOrEmpty(((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Base.Document).Current.OrigRefNbr))
        pmTran = PXResultset<PMTran>.op_Implicit(((PXSelectBase<PMTran>) new PXSelect<PMTran, Where<PMTran.origTranType, Equal<Required<PMTran.origTranType>>, And<PMTran.origRefNbr, Equal<Required<PMTran.origRefNbr>>, And<PMTran.origLineNbr, Equal<Required<PMTran.origLineNbr>>>>>>((PXGraph) this.Base)).SelectWindowed(0, 1, new object[3]
        {
          (object) ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Base.Document).Current.OrigDocType,
          (object) ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Base.Document).Current.OrigRefNbr,
          (object) e.Row.OrigLineNbr
        }));
      else
        pmTran = PXResultset<PMTran>.op_Implicit(((PXSelectBase<PMTran>) new PXSelect<PMTran, Where<PMTran.aRTranType, Equal<Required<PMTran.aRTranType>>, And<PMTran.aRRefNbr, Equal<Required<PMTran.aRRefNbr>>, And<PMTran.refLineNbr, Equal<Required<PMTran.refLineNbr>>>>>>((PXGraph) this.Base)).SelectWindowed(0, 1, new object[3]
        {
          (object) tranType,
          (object) refNbr,
          (object) lineNbr
        }));
      if (pmTran == null)
      {
        if (PXResultset<PMBillingRecord>.op_Implicit(PXSelectBase<PMBillingRecord, PXViewOf<PMBillingRecord>.BasedOn<SelectFromBase<PMBillingRecord, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMBillingRecord.aRDocType, Equal<P.AsString>>>>>.And<BqlOperand<PMBillingRecord.aRRefNbr, IBqlString>.IsEqual<P.AsString>>>>.Config>.Select((PXGraph) this.Base, new object[2]
        {
          (object) e.Row.TranType,
          (object) e.Row.RefNbr
        })) != null)
          this.SubtractValuesToInvoice(e.Row, this.GetProjectedAccountGroup(e.Row), -1 * (int) (ARDocType.SignAmount(e.Row.TranType) ?? 1M));
      }
      this.RemoveObsoleteLines();
    }
    if (e.Row == null || this.isARInvoiceDeleting)
      return;
    foreach (PXResult<PMTran, PMProject> pxResult in ((PXSelectBase<PMTran>) this.Base.RefContractUsageTran).Select(new object[3]
    {
      (object) e.Row.TranType,
      (object) e.Row.RefNbr,
      (object) e.Row.LineNbr
    }))
    {
      PMTran tran = PXResult<PMTran, PMProject>.op_Implicit(pxResult);
      PMProject pmProject = PXResult<PMTran, PMProject>.op_Implicit(pxResult);
      if (tran != null)
      {
        tran.ARRefNbr = (string) null;
        tran.ARTranType = (string) null;
        tran.RefLineNbr = new int?();
        if (((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Base.Document).Current != null && !((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Base.Document).Current.ProformaExists.GetValueOrDefault() && (pmProject?.BaseType == "P" || FlaggedModeScopeBase<ARInvoiceEntry.UnlinkContractUsagesOnDeleteScope>.IsActive))
        {
          tran.Billed = new bool?(false);
          tran.BilledDate = new DateTime?();
          tran.InvoicedQty = new Decimal?(0M);
          tran.InvoicedAmount = new Decimal?(0M);
          RegisterReleaseProcess.AddToUnbilledSummary((PXGraph) this.Base, tran);
        }
        ((PXSelectBase<PMTran>) this.Base.RefContractUsageTran).Update(tran);
      }
    }
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<PX.Objects.AR.ARTran, PX.Objects.AR.ARTran.costCodeID> e)
  {
    PMProject project;
    if (!CostCodeAttribute.UseCostCode() || !ProjectDefaultAttribute.IsProject((PXGraph) this.Base, e.Row.ProjectID, out project) || !(project.BudgetLevel == "T"))
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PX.Objects.AR.ARTran, PX.Objects.AR.ARTran.costCodeID>, PX.Objects.AR.ARTran, object>) e).NewValue = (object) CostCodeAttribute.GetDefaultCostCode();
  }

  protected virtual void _(PX.Data.Events.RowPersisted<PX.Objects.AR.ARInvoice> e)
  {
    if ((e.Operation & 3) != 3)
      return;
    bool isActive = FlaggedModeScopeBase<ARInvoiceEntry.UnlinkContractUsagesOnDeleteScope>.IsActive;
    PXUpdateJoin<Set<PMTran.aRTranType, Null, Set<PMTran.aRRefNbr, Null, Set<PMTran.refLineNbr, Null, Set<PMTran.billed, IIf<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Required<Parameter.ofBool>, NotEqual<True>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMProject.baseType, Equal<CTPRType.project>>>>>.Or<BqlOperand<Required<Parameter.ofBool>, IBqlBool>.IsEqual<True>>>, False, PMTran.billed>, Set<PMTran.billedDate, IIf<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Required<Parameter.ofBool>, NotEqual<True>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMProject.baseType, Equal<CTPRType.project>>>>>.Or<BqlOperand<Required<Parameter.ofBool>, IBqlBool>.IsEqual<True>>>, Null, PMTran.billedDate>, Set<PMTran.invoicedQty, IIf<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Required<Parameter.ofBool>, NotEqual<True>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMProject.baseType, Equal<CTPRType.project>>>>>.Or<BqlOperand<Required<Parameter.ofBool>, IBqlBool>.IsEqual<True>>>, decimal0, PMTran.invoicedQty>, Set<PMTran.invoicedAmount, IIf<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Required<Parameter.ofBool>, NotEqual<True>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMProject.baseType, Equal<CTPRType.project>>>>>.Or<BqlOperand<Required<Parameter.ofBool>, IBqlBool>.IsEqual<True>>>, decimal0, PMTran.invoicedAmount>>>>>>>>, PMTran, InnerJoin<PMProject, On<BqlOperand<PMTran.projectID, IBqlInt>.IsEqual<PMProject.contractID>>>, Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMTran.aRTranType, Equal<P.AsString>>>>>.And<BqlOperand<PMTran.aRRefNbr, IBqlString>.IsEqual<P.AsString>>>>.Update((PXGraph) this.Base, new object[10]
    {
      (object) e.Row.ProformaExists,
      (object) isActive,
      (object) e.Row.ProformaExists,
      (object) isActive,
      (object) e.Row.ProformaExists,
      (object) isActive,
      (object) e.Row.ProformaExists,
      (object) isActive,
      (object) e.Row.DocType,
      (object) e.Row.RefNbr
    });
  }

  protected virtual void _(PX.Data.Events.FieldUpdated<PX.Objects.AR.ARInvoice.projectID> e)
  {
    if (!(e.Row is PX.Objects.AR.ARInvoice row))
      return;
    this.CheckNoProFormaInvoiceForProFormaBilledProject(row);
  }

  private void CheckNoProFormaInvoiceForProFormaBilledProject(PX.Objects.AR.ARInvoice arInvoice)
  {
    PMProject project;
    if (arInvoice.ProformaExists.GetValueOrDefault() || !ProjectDefaultAttribute.IsProject((PXGraph) this.Base, arInvoice.ProjectID, out project) || !project.CreateProforma.GetValueOrDefault())
      return;
    ((PXSelectBase) this.Base.Document).Cache.RaiseExceptionHandling<PX.Objects.AR.ARInvoice.projectID>((object) arInvoice, (object) arInvoice.ProjectID, (Exception) new PXSetPropertyException("The selected project is billed using pro forma invoices. Manually created AR document will not be tracked in the project budget on the Projects (PM301000) form and will not be shown in the project-related reports.", (PXErrorLevel) 2));
  }

  private void SyncBudgets(PX.Objects.AR.ARTran row, PX.Objects.AR.ARTran oldRow)
  {
    if (((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Base.Document).Current.IsRetainageDocument.GetValueOrDefault() || ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Base.Document).Current.ProformaExists.GetValueOrDefault())
      return;
    int? taskId = row.TaskID;
    int? nullable1 = oldRow.TaskID;
    Decimal? nullable2;
    if (taskId.GetValueOrDefault() == nullable1.GetValueOrDefault() & taskId.HasValue == nullable1.HasValue)
    {
      Decimal? tranAmt = row.TranAmt;
      Decimal? nullable3 = oldRow.TranAmt;
      if (tranAmt.GetValueOrDefault() == nullable3.GetValueOrDefault() & tranAmt.HasValue == nullable3.HasValue)
      {
        nullable1 = row.AccountID;
        int? nullable4 = oldRow.AccountID;
        if (nullable1.GetValueOrDefault() == nullable4.GetValueOrDefault() & nullable1.HasValue == nullable4.HasValue)
        {
          nullable4 = row.CostCodeID;
          nullable1 = oldRow.CostCodeID;
          if (nullable4.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable4.HasValue == nullable1.HasValue && !(row.UOM != oldRow.UOM))
          {
            nullable3 = row.Qty;
            nullable2 = oldRow.Qty;
            if (nullable3.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable3.HasValue == nullable2.HasValue)
              return;
          }
        }
      }
    }
    nullable1 = oldRow.TaskID;
    if (nullable1.HasValue)
    {
      PX.Objects.AR.ARTran line1 = oldRow;
      int? projectedAccountGroup1 = this.GetProjectedAccountGroup(oldRow);
      nullable2 = ARDocType.SignAmount(oldRow.TranType);
      int mult1 = -1 * (int) (nullable2 ?? 1M);
      this.AddToInvoiced(line1, projectedAccountGroup1, mult1);
      PX.Objects.AR.ARTran line2 = oldRow;
      int? projectedAccountGroup2 = this.GetProjectedAccountGroup(oldRow);
      nullable2 = ARDocType.SignAmount(oldRow.TranType);
      int mult2 = -1 * (int) (nullable2 ?? 1M);
      this.AddToDraftRetained(line2, projectedAccountGroup2, mult2);
    }
    nullable1 = row.TaskID;
    if (nullable1.HasValue)
    {
      PX.Objects.AR.ARTran line3 = row;
      int? projectedAccountGroup3 = this.GetProjectedAccountGroup(row);
      nullable2 = ARDocType.SignAmount(oldRow.TranType);
      int mult3 = (int) (nullable2 ?? 1M);
      this.AddToInvoiced(line3, projectedAccountGroup3, mult3);
      PX.Objects.AR.ARTran line4 = row;
      int? projectedAccountGroup4 = this.GetProjectedAccountGroup(row);
      nullable2 = ARDocType.SignAmount(oldRow.TranType);
      int mult4 = (int) (nullable2 ?? 1M);
      this.AddToDraftRetained(line4, projectedAccountGroup4, mult4);
    }
    this.RemoveObsoleteLines();
  }

  public virtual int? GetProjectedAccountGroup(PX.Objects.AR.ARTran line)
  {
    int? projectedAccountGroup = new int?();
    if (line.AccountID.HasValue && PXSelectorAttribute.Select<PX.Objects.AR.ARTran.accountID>(((PXSelectBase) this.Base.Transactions).Cache, (object) line, (object) line.AccountID) is PX.Objects.GL.Account account && account.AccountGroupID.HasValue)
      projectedAccountGroup = account.AccountGroupID;
    return projectedAccountGroup;
  }

  public virtual void AddToInvoiced(PX.Objects.AR.ARTran line, int? revenueAccountGroup, int mult)
  {
    if (((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Base.Document).Current.Scheduled.GetValueOrDefault() || !this.ModifyInvoiced(line))
      return;
    PMBudgetAccum targetBudget = this.GetTargetBudget(revenueAccountGroup, line);
    if (targetBudget == null)
      return;
    Decimal num1 = (Decimal) mult;
    int? projectId = targetBudget.ProjectID;
    Decimal? curyTranAmt = line.CuryTranAmt;
    Decimal? nullable1 = line.CuryRetainageAmt;
    Decimal? nullable2 = curyTranAmt.HasValue & nullable1.HasValue ? new Decimal?(curyTranAmt.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
    Decimal inProjectCurrency = this.GetValueInProjectCurrency(projectId, nullable2);
    Decimal curyValue = num1 * inProjectCurrency;
    PMBudgetAccum pmBudgetAccum1 = targetBudget;
    nullable1 = pmBudgetAccum1.CuryInvoicedAmount;
    Decimal num2 = curyValue;
    pmBudgetAccum1.CuryInvoicedAmount = nullable1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + num2) : new Decimal?();
    PMBudgetAccum pmBudgetAccum2 = targetBudget;
    nullable1 = pmBudgetAccum2.InvoicedAmount;
    Decimal baseValueForBudget = this.GetBaseValueForBudget(PMProject.PK.Find((PXGraph) this.Base, line.ProjectID), curyValue);
    pmBudgetAccum2.InvoicedAmount = nullable1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + baseValueForBudget) : new Decimal?();
    ARInvoiceEntry graph = this.Base;
    string uom1 = line.UOM;
    string uom2 = targetBudget.UOM;
    nullable1 = line.Qty;
    Decimal valueOrDefault = nullable1.GetValueOrDefault();
    Decimal num3;
    ref Decimal local = ref num3;
    INUnitAttribute.TryConvertGlobalUnits((PXGraph) graph, uom1, uom2, valueOrDefault, INPrecision.QUANTITY, out local);
    PMBudgetAccum pmBudgetAccum3 = targetBudget;
    nullable1 = pmBudgetAccum3.InvoicedQty;
    Decimal num4 = (Decimal) mult * num3;
    pmBudgetAccum3.InvoicedQty = nullable1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + num4) : new Decimal?();
  }

  protected virtual bool ModifyInvoiced(PX.Objects.AR.ARTran line)
  {
    return PMTask.PK.Find((PXGraph) this.Base, line.ProjectID, line.TaskID).Type != "Cost";
  }

  public virtual void SubtractValuesToInvoice(PX.Objects.AR.ARTran line, int? revenueAccountGroup, int mult)
  {
    PMBudgetAccum targetBudget = this.GetTargetBudget(revenueAccountGroup, line);
    if (targetBudget == null)
      return;
    Decimal num1 = (Decimal) mult;
    int? projectId = targetBudget.ProjectID;
    Decimal? nullable1 = line.CuryTranAmt;
    Decimal? curyRetainageAmt = line.CuryRetainageAmt;
    Decimal? nullable2 = nullable1.HasValue & curyRetainageAmt.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + curyRetainageAmt.GetValueOrDefault()) : new Decimal?();
    Decimal inProjectCurrency = this.GetValueInProjectCurrency(projectId, nullable2);
    Decimal curyValue = num1 * inProjectCurrency;
    PMBudgetAccum pmBudgetAccum1 = targetBudget;
    Decimal? nullable3 = pmBudgetAccum1.CuryAmountToInvoice;
    Decimal num2 = curyValue;
    Decimal? nullable4;
    if (!nullable3.HasValue)
    {
      nullable1 = new Decimal?();
      nullable4 = nullable1;
    }
    else
      nullable4 = new Decimal?(nullable3.GetValueOrDefault() - num2);
    pmBudgetAccum1.CuryAmountToInvoice = nullable4;
    PMBudgetAccum pmBudgetAccum2 = targetBudget;
    nullable3 = pmBudgetAccum2.AmountToInvoice;
    Decimal baseValueForBudget = this.GetBaseValueForBudget(PMProject.PK.Find((PXGraph) this.Base, line.ProjectID), curyValue);
    Decimal? nullable5;
    if (!nullable3.HasValue)
    {
      nullable1 = new Decimal?();
      nullable5 = nullable1;
    }
    else
      nullable5 = new Decimal?(nullable3.GetValueOrDefault() - baseValueForBudget);
    pmBudgetAccum2.AmountToInvoice = nullable5;
    if (!(targetBudget.ProgressBillingBase == "Q"))
      return;
    ARInvoiceEntry graph = this.Base;
    string uom1 = line.UOM;
    string uom2 = targetBudget.UOM;
    nullable3 = line.Qty;
    Decimal valueOrDefault = nullable3.GetValueOrDefault();
    Decimal num3;
    ref Decimal local = ref num3;
    INUnitAttribute.TryConvertGlobalUnits((PXGraph) graph, uom1, uom2, valueOrDefault, INPrecision.QUANTITY, out local);
    PMBudgetAccum pmBudgetAccum3 = targetBudget;
    nullable3 = pmBudgetAccum3.QtyToInvoice;
    Decimal num4 = (Decimal) mult * num3;
    Decimal? nullable6;
    if (!nullable3.HasValue)
    {
      nullable1 = new Decimal?();
      nullable6 = nullable1;
    }
    else
      nullable6 = new Decimal?(nullable3.GetValueOrDefault() - num4);
    pmBudgetAccum3.QtyToInvoice = nullable6;
  }

  protected virtual void AddToUnbilledSummary(PX.Objects.AR.ARInvoice row)
  {
    if (row.ProformaExists.GetValueOrDefault())
      return;
    foreach (PXResult<PMTran> pxResult in PXSelectBase<PMTran, PXViewOf<PMTran>.BasedOn<SelectFromBase<PMTran, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PMProject>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMTran.projectID, Equal<PMProject.contractID>>>>>.And<BqlOperand<PMProject.baseType, IBqlString>.IsNotEqual<CTPRType.contract>>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMTran.aRTranType, Equal<P.AsString>>>>>.And<BqlOperand<PMTran.aRRefNbr, IBqlString>.IsEqual<P.AsString>>>>.Config>.Select((PXGraph) this.Base, new object[2]
    {
      (object) row.DocType,
      (object) row.RefNbr
    }))
    {
      PMTran tran = PXResult<PMTran>.op_Implicit(pxResult);
      tran.Billed = new bool?(false);
      RegisterReleaseProcess.AddToUnbilledSummary((PXGraph) this.Base, tran);
    }
  }

  public virtual void AddToDraftRetained(PX.Objects.AR.ARTran line, int? revenueAccountGroup, int mult)
  {
    PMBudgetAccum targetBudget = this.GetTargetBudget(revenueAccountGroup, line);
    if (targetBudget == null)
      return;
    Decimal curyValue = (Decimal) mult * this.GetValueInProjectCurrency(targetBudget.ProjectID, line.CuryRetainageAmt);
    PMBudgetAccum pmBudgetAccum1 = targetBudget;
    Decimal? draftRetainedAmount = pmBudgetAccum1.CuryDraftRetainedAmount;
    Decimal num = curyValue;
    pmBudgetAccum1.CuryDraftRetainedAmount = draftRetainedAmount.HasValue ? new Decimal?(draftRetainedAmount.GetValueOrDefault() + num) : new Decimal?();
    PMBudgetAccum pmBudgetAccum2 = targetBudget;
    draftRetainedAmount = pmBudgetAccum2.DraftRetainedAmount;
    Decimal baseValueForBudget = this.GetBaseValueForBudget(PMProject.PK.Find((PXGraph) this.Base, line.ProjectID), curyValue);
    pmBudgetAccum2.DraftRetainedAmount = draftRetainedAmount.HasValue ? new Decimal?(draftRetainedAmount.GetValueOrDefault() + baseValueForBudget) : new Decimal?();
  }

  private PMBudgetAccum GetTargetBudget(int? accountGroupID, PX.Objects.AR.ARTran line)
  {
    if (!line.ProjectID.HasValue)
      return (PMBudgetAccum) null;
    if (!line.TaskID.HasValue)
      return (PMBudgetAccum) null;
    if (!accountGroupID.HasValue)
      return (PMBudgetAccum) null;
    PMProject project = PMProject.PK.Find((PXGraph) this.Base, line.ProjectID);
    if (project == null)
      return (PMBudgetAccum) null;
    if (project.NonProject.GetValueOrDefault())
      return (PMBudgetAccum) null;
    PX.Objects.PM.Lite.PMBudget pmBudget = new BudgetService((PXGraph) this.Base).SelectProjectBalance(PMAccountGroup.PK.Find((PXGraph) this.Base, accountGroupID), project, line.TaskID, line.InventoryID, line.CostCodeID, out bool _);
    string str = pmBudget.ProgressBillingBase ?? PMTask.PK.Find((PXGraph) this.Base, pmBudget.ProjectID, pmBudget.ProjectTaskID)?.ProgressBillingBase;
    PXSelect<PMBudgetAccum> budget = this.Budget;
    PMBudgetAccum pmBudgetAccum = new PMBudgetAccum();
    pmBudgetAccum.Type = pmBudget.Type;
    pmBudgetAccum.ProjectID = pmBudget.ProjectID;
    pmBudgetAccum.ProjectTaskID = pmBudget.TaskID;
    pmBudgetAccum.AccountGroupID = pmBudget.AccountGroupID;
    pmBudgetAccum.InventoryID = pmBudget.InventoryID;
    pmBudgetAccum.CostCodeID = pmBudget.CostCodeID;
    pmBudgetAccum.UOM = pmBudget.UOM;
    pmBudgetAccum.Description = pmBudget.Description;
    pmBudgetAccum.CuryInfoID = project.CuryInfoID;
    pmBudgetAccum.ProgressBillingBase = str;
    return ((PXSelectBase<PMBudgetAccum>) budget).Insert(pmBudgetAccum);
  }

  public virtual void Configure(PXScreenConfiguration config)
  {
    ARInvoiceEntryExt.Configure(config.GetScreenConfigurationContext<ARInvoiceEntry, PX.Objects.AR.ARInvoice>());
  }

  protected static void Configure(WorkflowContext<ARInvoiceEntry, PX.Objects.AR.ARInvoice> context)
  {
    BoundedTo<ARInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionCategory.IConfigured relatedDocumentsCategory = context.Categories.Get("Related DocumentsID");
    context.UpdateScreenConfigurationFor((Func<BoundedTo<ARInvoiceEntry, PX.Objects.AR.ARInvoice>.ScreenConfiguration.ConfiguratorScreen, BoundedTo<ARInvoiceEntry, PX.Objects.AR.ARInvoice>.ScreenConfiguration.ConfiguratorScreen>) (screen => screen.WithActions((Action<BoundedTo<ARInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionDefinition.ContainerAdjusterActions>) (actions =>
    {
      actions.Add<ARInvoiceEntryExt>((Expression<Func<ARInvoiceEntryExt, PXAction<PX.Objects.AR.ARInvoice>>>) (g => g.viewProforma), (Func<BoundedTo<ARInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ARInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionDefinition.IConfigured>) (c => (BoundedTo<ARInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionDefinition.IConfigured) c.WithCategory(relatedDocumentsCategory).IsDisabledWhen(context.Conditions.Get("IsPrepaymentInvoiceReversing")).IsHiddenWhen(context.Conditions.Get("IsFinCharge")).IsHiddenWhen(context.Conditions.Get("IsPrepaymentInvoice"))));
      actions.Add<ARInvoiceEntryExt>((Expression<Func<ARInvoiceEntryExt, PXAction<PX.Objects.AR.ARInvoice>>>) (g => g.viewPMTrans), (Func<BoundedTo<ARInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ARInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionDefinition.IConfigured>) (c => (BoundedTo<ARInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionDefinition.IConfigured) c.WithCategory((PredefinedCategory) 1).IsDisabledWhen(context.Conditions.Get("IsPrepaymentInvoiceReversing")).IsHiddenWhen(context.Conditions.Get("IsPrepaymentInvoice"))));
    }))));
  }

  protected virtual void RemoveObsoleteLines()
  {
    foreach (PMBudgetAccum pmBudgetAccum in ((PXSelectBase) this.Budget).Cache.Inserted)
    {
      if (pmBudgetAccum.CuryInvoicedAmount.GetValueOrDefault() == 0M && pmBudgetAccum.InvoicedAmount.GetValueOrDefault() == 0M && pmBudgetAccum.CuryAmountToInvoice.GetValueOrDefault() == 0M && pmBudgetAccum.AmountToInvoice.GetValueOrDefault() == 0M && pmBudgetAccum.InvoicedQty.GetValueOrDefault() == 0.0M)
        ((PXSelectBase) this.Budget).Cache.Remove((object) pmBudgetAccum);
    }
  }

  private Decimal GetValueInProjectCurrency(int? projectID, Decimal? value)
  {
    return this.MultiCurrencyService.GetValueInProjectCurrency((PXGraph) this.Base, PMProject.PK.Find((PXGraph) this.Base, projectID), ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Base.Document).Current.CuryID, ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Base.Document).Current.DocDate, value);
  }

  private Decimal GetBaseValueForBudget(PMProject project, Decimal curyValue)
  {
    return project.CuryID == project.BaseCuryID ? curyValue : ((PXGraph) this.Base).FindImplementation<IPXCurrencyHelper>().GetCurrencyInfo(project.CuryInfoID).CuryConvBase(curyValue);
  }

  [PXOverride]
  public string GetCustomerReportID(
    string reportID,
    PX.Objects.AR.ARInvoice doc,
    Func<string, PX.Objects.AR.ARInvoice, string> baseGetCustomerReportID)
  {
    ARInvoiceEntry_ActivityDetailsExt extension = ((PXGraph) this.Base).GetExtension<ARInvoiceEntry_ActivityDetailsExt>();
    if (!ProjectDefaultAttribute.IsProject((PXGraph) this.Base, doc.ProjectID) || extension.ProjectInvoiceReportActive(doc.ProjectID) == null || !(reportID == "AR641000"))
      return baseGetCustomerReportID(reportID, doc);
    PMProject project = PMProject.PK.Find((PXGraph) this.Base, doc.ProjectID);
    return this.GetProjectSpecificCustomerReportID(extension.ProjectInvoiceReportActive(doc.ProjectID), doc, project);
  }

  public virtual string GetProjectSpecificCustomerReportID(
    string reportID,
    PX.Objects.AR.ARInvoice doc,
    PMProject project)
  {
    NotificationSetup setup = this.GetSetup("Project", reportID, doc.BranchID);
    if (setup == null)
      return reportID;
    NotificationSource source = this.GetSource("Project", (object) project, setup.SetupID.Value, doc.BranchID);
    return source != null && source.ReportID != null ? source.ReportID : reportID;
  }

  private NotificationSetup GetSetup(string source, string reportID, int? branchID)
  {
    return PXResultset<NotificationSetup>.op_Implicit(PXSelectBase<NotificationSetup, PXViewOf<NotificationSetup>.BasedOn<SelectFromBase<NotificationSetup, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<NotificationSetup.active, Equal<True>>>>, And<BqlOperand<NotificationSetup.sourceCD, IBqlString>.IsEqual<P.AsString>>>, And<BqlOperand<NotificationSetup.reportID, IBqlString>.IsEqual<P.AsString>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<NotificationSetup.nBranchID, Equal<P.AsInt>>>>>.Or<BqlOperand<NotificationSetup.nBranchID, IBqlInt>.IsNull>>>.Order<By<BqlField<NotificationSetup.nBranchID, IBqlInt>.Desc>>>.Config>.SelectWindowed((PXGraph) this.Base, 0, 1, new object[3]
    {
      (object) source,
      (object) reportID,
      (object) branchID
    }));
  }

  public NotificationSource GetSource(string sourceType, object row, Guid setupID, int? branchID)
  {
    if (row == null)
      return (NotificationSource) null;
    ProjectEntry instance = PXGraph.CreateInstance<ProjectEntry>();
    NotificationSource source1 = (NotificationSource) null;
    foreach (NotificationSource source2 in GraphHelper.RowCast<NotificationSource>((IEnumerable) ((PXSelectBase) instance.NotificationSources).View.SelectMulti(Array.Empty<object>())))
    {
      Guid? setupId = source2.SetupID;
      Guid guid1 = setupID;
      int? nullable;
      if ((setupId.HasValue ? (setupId.GetValueOrDefault() == guid1 ? 1 : 0) : 0) != 0)
      {
        int? nbranchId = source2.NBranchID;
        nullable = branchID;
        if (nbranchId.GetValueOrDefault() == nullable.GetValueOrDefault() & nbranchId.HasValue == nullable.HasValue)
          return source2;
      }
      setupId = source2.SetupID;
      Guid guid2 = setupID;
      if ((setupId.HasValue ? (setupId.GetValueOrDefault() == guid2 ? 1 : 0) : 0) != 0)
      {
        nullable = source2.NBranchID;
        if (!nullable.HasValue)
          source1 = source2;
      }
    }
    return source1;
  }
}
