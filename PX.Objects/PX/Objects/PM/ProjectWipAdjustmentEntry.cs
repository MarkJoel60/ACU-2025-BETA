// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.ProjectWipAdjustmentEntry
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CS;
using PX.Objects.CT;
using PX.Objects.EP;
using PX.Objects.Extensions.MultiCurrency;
using PX.Objects.GL;
using PX.Objects.PO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace PX.Objects.PM;

[Serializable]
public class ProjectWipAdjustmentEntry : PXGraph<
#nullable disable
ProjectWipAdjustmentEntry, PMWipAdjustment>
{
  public PXSetup<PMSetup> Setup;
  [PXViewName("Project WIP Adjustment")]
  public FbqlSelect<SelectFromBase<PMWipAdjustment, TypeArrayOf<IFbqlJoin>.Empty>, PMWipAdjustment>.View Document;
  public FbqlSelect<SelectFromBase<PMWipAdjustment, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<
  #nullable enable
  PMWipAdjustment.refNbr, IBqlString>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  PMWipAdjustment.refNbr, IBqlString>.FromCurrent>>, 
  #nullable disable
  PMWipAdjustment>.View CurrentDocument;
  [PXViewName("Cost Projection Detail")]
  public FbqlSelect<SelectFromBase<PMWipAdjustmentLine, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<PMProject>.On<PMWipAdjustmentLine.FK.Project>>, FbqlJoins.Left<PMCostProjectionByDate>.On<PMWipAdjustmentLine.FK.Projection>>, FbqlJoins.Left<PX.Objects.CR.Contact>.On<BqlOperand<
  #nullable enable
  PX.Objects.CR.Contact.contactID, IBqlInt>.IsEqual<
  #nullable disable
  PMProject.ownerID>>>>.Where<BqlOperand<
  #nullable enable
  PMWipAdjustmentLine.refNbr, IBqlString>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  PMWipAdjustment.refNbr, IBqlString>.FromCurrent>>.Order<
  #nullable disable
  By<BqlField<
  #nullable enable
  PMWipAdjustmentLine.lineNbr, IBqlInt>.Asc>>, 
  #nullable disable
  PMWipAdjustmentLine>.View Items;
  [PXHidden]
  public PXSetup<PX.Objects.GL.Company> Company;
  [PXCopyPasteHiddenView]
  [PXViewName("Approval")]
  public EPApprovalAutomation<PMWipAdjustment, PMWipAdjustment.approved, PMWipAdjustment.rejected, PMWipAdjustment.hold, PMSetupWipAdjustmentApproval> Approval;
  public PXAction<PMWipAdjustment> addProjects;
  public PXAction<PMWipAdjustment> refreshLines;
  public PXAction<PMWipAdjustment> deleteSelected;
  public PXAction<PMWipAdjustment> refreshSelected;
  public PXAction<PMWipAdjustment> removeHold;
  public PXAction<PMWipAdjustment> hold;
  public PXAction<PMWipAdjustment> release;
  public PXAction<PMWipAdjustment> reverse;
  public PXAction<PMWipAdjustment> printWipReport;
  public PXAction<PMWipAdjustment> printWipAdjustment;
  public PXAction<PMWipAdjustment> sendEmail;
  public PXAction<PMWipAdjustmentLine> viewProject;
  public PXAction<PMWipAdjustmentLine> viewCostProjection;
  private Dictionary<int?, PMProject> _projectsCache;
  private Dictionary<ProjectWipAdjustmentEntry.ProjectBudgetKey, Decimal?> _projectOriginalBudgetCache;
  private Dictionary<int?, Decimal?> _projectOriginalCommitmentsCache;
  private Dictionary<int?, Decimal?> _projectApprovedCommitmentsCache;
  private Dictionary<int?, Decimal?> _projectPendingCommitmentsCache;
  private Dictionary<int?, Decimal?> _projectActualCostsCache;
  private Dictionary<int?, Decimal?> _projectPeriodCostsCache;
  private Dictionary<int?, Decimal?> _projectPeriodBillingsCache;
  private Dictionary<int?, Decimal?> _projectBillingsToPeriodCache;
  private Dictionary<ProjectWipAdjustmentEntry.ProjectBudgetKey, Decimal?> _projectBudgetPendingAmountCache;
  private Dictionary<ProjectWipAdjustmentEntry.ProjectBudgetKey, Decimal?> _projectBudgetChangedAmountCache;

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Manager Phone", Visible = false)]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.CR.Contact.phone1> e)
  {
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Phone 2", Visible = false)]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.CR.Contact.phone2> e)
  {
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Phone 3", Visible = false)]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.CR.Contact.phone3> e)
  {
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Email", Visible = false)]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.CR.Contact.eMail> e)
  {
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Adjustment Date", Enabled = false)]
  protected virtual void _(
    PX.Data.Events.CacheAttached<PMCostProjectionByDate.projectionDate> e)
  {
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Project Status", Enabled = false)]
  protected virtual void _(PX.Data.Events.CacheAttached<PMProject.status> e)
  {
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Project Description", Enabled = false)]
  protected virtual void _(PX.Data.Events.CacheAttached<PMProject.description> e)
  {
  }

  public ProjectWipAdjustmentEntry()
  {
    ((PXAction) this.CopyPaste).SetVisible(false);
    ((PXSelectBase) this.Items).Cache.AllowDelete = false;
  }

  protected virtual bool DocumentIsOnHold
  {
    get => ((PXSelectBase<PMWipAdjustment>) this.Document).Current?.Status == "H";
  }

  protected virtual bool DocumentIsOpen
  {
    get => ((PXSelectBase<PMWipAdjustment>) this.Document).Current?.Status == "O";
  }

  private bool DocumentHasLines
  {
    get
    {
      return ((PXSelectBase<PMWipAdjustmentLine>) this.Items).Current != null || NonGenericIEnumerableExtensions.Any_((IEnumerable) GraphHelper.RowCast<PMWipAdjustmentLine>((IEnumerable) ((PXSelectBase<PMWipAdjustmentLine>) this.Items).Select(Array.Empty<object>())));
    }
  }

  public virtual bool UseBudgetForPlannedCostEstimation
  {
    get
    {
      PMSetup current = ((PXSelectBase<PMSetup>) this.Setup).Current;
      return current != null && current.UseBudgetForPlannedCostEstimationInWipAdjustments.GetValueOrDefault();
    }
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMWipAdjustment, PMWipAdjustment.includePendingChangeOrders> e)
  {
    foreach (PXResult<PMWipAdjustmentLine> pxResult in ((PXSelectBase<PMWipAdjustmentLine>) this.Items).Select(Array.Empty<object>()))
    {
      PMWipAdjustmentLine line = PXResult<PMWipAdjustmentLine>.op_Implicit(pxResult);
      if (line.ProjectionRefNbr == null)
      {
        line.IncludePendingChangeOrders = (bool?) e.Row?.IncludePendingChangeOrders;
        this.UpdateLineWithCostProjection(line, (string) null);
        ((PXSelectBase<PMWipAdjustmentLine>) this.Items).Update(line);
      }
    }
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<PMWipAdjustmentLine, PMWipAdjustmentLine.projectID> e)
  {
    if (this.BulkMode)
      return;
    int? newValue = ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMWipAdjustmentLine, PMWipAdjustmentLine.projectID>, PMWipAdjustmentLine, object>) e).NewValue as int?;
    if (PXResultset<PMWipAdjustmentLine>.op_Implicit(PXSelectBase<PMWipAdjustmentLine, PXViewOf<PMWipAdjustmentLine>.BasedOn<SelectFromBase<PMWipAdjustmentLine, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMWipAdjustmentLine.refNbr, Equal<BqlField<PMWipAdjustment.refNbr, IBqlString>.FromCurrent>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMWipAdjustmentLine.lineNbr, NotEqual<P.AsInt>>>>>.And<BqlOperand<PMWipAdjustmentLine.projectID, IBqlInt>.IsEqual<P.AsInt>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) e.Row.LineNbr,
      (object) newValue
    })) != null)
    {
      ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMWipAdjustmentLine, PMWipAdjustmentLine.projectID>, PMWipAdjustmentLine, object>) e).NewValue = (object) PMProject.PK.Find((PXGraph) this, newValue)?.ContractCD;
      throw new PXSetPropertyException<PMWipAdjustmentLine.projectID>("Another line with the same project has already been added. Delete this line or specify a different project.", (PXErrorLevel) 4);
    }
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMWipAdjustmentLine, PMWipAdjustmentLine.projectID> e)
  {
    if (this.BulkMode)
      return;
    this.UpdateLineWithProject(e.Row, e.NewValue as int?);
    this.DefaultLineAccounts(e.Row);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMWipAdjustmentLine, PMWipAdjustmentLine.projectID>>) e).Cache.SetDefaultExt<PMWipAdjustmentLine.projectionRefNbr>((object) e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMWipAdjustmentLine, PMWipAdjustmentLine.projectionRefNbr> e)
  {
    this.UpdateLineWithCostProjection(e.Row, e.NewValue as string);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMWipAdjustmentLine, PMWipAdjustmentLine.curyOverbillingAdjustmentAmount> e)
  {
    Decimal? newValue = e.NewValue as Decimal?;
    if (!newValue.HasValue)
      return;
    Decimal? nullable = newValue;
    Decimal num1 = 0M;
    if (nullable.GetValueOrDefault() == num1 & nullable.HasValue)
      return;
    nullable = e.Row.CuryUnderbillingAdjustmentAmount;
    if (!nullable.HasValue)
      return;
    nullable = e.Row.CuryUnderbillingAdjustmentAmount;
    Decimal num2 = 0M;
    if (nullable.GetValueOrDefault() == num2 & nullable.HasValue)
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMWipAdjustmentLine, PMWipAdjustmentLine.curyOverbillingAdjustmentAmount>>) e).Cache.SetValueExt<PMWipAdjustmentLine.curyUnderbillingAdjustmentAmount>((object) e.Row, (object) 0M);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMWipAdjustmentLine, PMWipAdjustmentLine.curyUnderbillingAdjustmentAmount> e)
  {
    Decimal? newValue = e.NewValue as Decimal?;
    if (!newValue.HasValue)
      return;
    Decimal? nullable = newValue;
    Decimal num1 = 0M;
    if (nullable.GetValueOrDefault() == num1 & nullable.HasValue)
      return;
    nullable = e.Row.CuryOverbillingAdjustmentAmount;
    if (!nullable.HasValue)
      return;
    nullable = e.Row.CuryOverbillingAdjustmentAmount;
    Decimal num2 = 0M;
    if (nullable.GetValueOrDefault() == num2 & nullable.HasValue)
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMWipAdjustmentLine, PMWipAdjustmentLine.curyUnderbillingAdjustmentAmount>>) e).Cache.SetValueExt<PMWipAdjustmentLine.curyOverbillingAdjustmentAmount>((object) e.Row, (object) 0M);
  }

  protected virtual void _(PX.Data.Events.RowUpdated<PMWipAdjustmentLine> e)
  {
    if (this.DocumentIsOnHold)
      return;
    PMWipAdjustmentLine oldRow = e.OldRow;
    bool? selected;
    bool? nullable1;
    if (oldRow == null)
    {
      nullable1 = new bool?();
    }
    else
    {
      selected = oldRow.Selected;
      nullable1 = new bool?(selected.GetValueOrDefault());
    }
    bool? nullable2 = nullable1;
    selected = e.Row.Selected;
    bool valueOrDefault = selected.GetValueOrDefault();
    if (nullable2.GetValueOrDefault() == valueOrDefault & nullable2.HasValue)
      return;
    ((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<PMWipAdjustmentLine>>) e).Cache.IsDirty = false;
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMWipAdjustmentLine, PMWipAdjustmentLine.includePendingChangeOrders> e)
  {
    this.UpdateLineWithCostProjection(e.Row, e.Row?.ProjectionRefNbr);
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<PMWipAdjustmentLine, PMWipAdjustmentLine.projectionRefNbr> e)
  {
    if (this.UseBudgetForPlannedCostEstimation)
      return;
    PMCostProjectionByDate projectionByDate = PXResultset<PMCostProjectionByDate>.op_Implicit(PXSelectBase<PMCostProjectionByDate, PXViewOf<PMCostProjectionByDate>.BasedOn<SelectFromBase<PMCostProjectionByDate, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PMProject>.On<PMCostProjectionByDate.FK.Project>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMCostProjectionByDate.released, Equal<True>>>>, And<BqlOperand<PMProject.contractID, IBqlInt>.IsEqual<BqlField<PMWipAdjustmentLine.projectID, IBqlInt>.FromCurrent>>>>.And<BqlOperand<PMCostProjectionByDate.projectionDate, IBqlDateTime>.IsLessEqual<BqlField<PMWipAdjustment.projectionDate, IBqlDateTime>.FromCurrent>>>.Order<By<BqlField<PMCostProjectionByDate.projectionDate, IBqlDateTime>.Desc>>>.ReadOnly.Config>.Select((PXGraph) this, Array.Empty<object>()));
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMWipAdjustmentLine, PMWipAdjustmentLine.projectionRefNbr>, PMWipAdjustmentLine, object>) e).NewValue = (object) projectionByDate?.RefNbr;
  }

  protected virtual void _(PX.Data.Events.RowSelected<PMWipAdjustment> e)
  {
    bool flag = ((PXSelectBase) this.Document).Cache.GetStatus((object) e.Row) == 2;
    bool documentIsOnHold = this.DocumentIsOnHold;
    bool documentIsOpen = this.DocumentIsOpen;
    bool plannedCostEstimation = this.UseBudgetForPlannedCostEstimation;
    ((PXSelectBase) this.Document).Cache.AllowDelete = documentIsOnHold;
    ((PXSelectBase) this.Items).Cache.AllowInsert = documentIsOnHold;
    PXUIFieldAttribute.SetEnabled<PMWipAdjustment.ownerID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMWipAdjustment>>) e).Cache, (object) e.Row, documentIsOnHold);
    PXUIFieldAttribute.SetRequired<PMWipAdjustment.ownerID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMWipAdjustment>>) e).Cache, true);
    PXUIFieldAttribute.SetEnabled<PMWipAdjustment.workgroupID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMWipAdjustment>>) e).Cache, (object) e.Row, documentIsOnHold);
    PXUIFieldAttribute.SetEnabled<PMWipAdjustment.date>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMWipAdjustment>>) e).Cache, (object) e.Row, documentIsOnHold);
    PXUIFieldAttribute.SetEnabled<PMWipAdjustment.projectionDate>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMWipAdjustment>>) e).Cache, (object) e.Row, documentIsOnHold);
    PXUIFieldAttribute.SetEnabled<PMWipAdjustment.finPeriodID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMWipAdjustment>>) e).Cache, (object) e.Row, documentIsOnHold);
    PXUIFieldAttribute.SetEnabled<PMWipAdjustment.branchID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMWipAdjustment>>) e).Cache, (object) e.Row, documentIsOnHold);
    PXUIFieldAttribute.SetEnabled<PMWipAdjustment.projectStatus>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMWipAdjustment>>) e).Cache, (object) e.Row, documentIsOnHold);
    PXUIFieldAttribute.SetEnabled<PMWipAdjustment.overbillingUnderbillingOption>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMWipAdjustment>>) e).Cache, (object) e.Row, documentIsOnHold);
    PXUIFieldAttribute.SetEnabled<PMWipAdjustment.revenueOption>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMWipAdjustment>>) e).Cache, (object) e.Row, documentIsOnHold);
    PXUIFieldAttribute.SetEnabled<PMWipAdjustment.description>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMWipAdjustment>>) e).Cache, (object) e.Row, documentIsOnHold | documentIsOpen);
    bool useLineProject1;
    bool useLineAccounts1;
    PostingOptions.ParsePostingOptions(e.Row?.OverbillingUnderbillingOption, out useLineProject1, out useLineAccounts1, out bool _);
    bool useLineProject2;
    bool useLineAccounts2;
    PostingOptions.ParsePostingOptions(e.Row?.RevenueOption, out useLineProject2, out useLineAccounts2);
    PXUIFieldAttribute.SetEnabled<PMWipAdjustment.overbillingAccountID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMWipAdjustment>>) e).Cache, (object) e.Row, documentIsOnHold);
    PXUIFieldAttribute.SetEnabled<PMWipAdjustment.overbillingSubID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMWipAdjustment>>) e).Cache, (object) e.Row, documentIsOnHold);
    PXUIFieldAttribute.SetRequired<PMWipAdjustment.overbillingAccountID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMWipAdjustment>>) e).Cache, !useLineAccounts1);
    PXUIFieldAttribute.SetRequired<PMWipAdjustment.overbillingSubID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMWipAdjustment>>) e).Cache, !useLineAccounts1);
    PXUIFieldAttribute.SetEnabled<PMWipAdjustment.underbillingAccountID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMWipAdjustment>>) e).Cache, (object) e.Row, documentIsOnHold);
    PXUIFieldAttribute.SetEnabled<PMWipAdjustment.underbillingSubID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMWipAdjustment>>) e).Cache, (object) e.Row, documentIsOnHold);
    PXUIFieldAttribute.SetRequired<PMWipAdjustment.underbillingAccountID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMWipAdjustment>>) e).Cache, !useLineAccounts1);
    PXUIFieldAttribute.SetRequired<PMWipAdjustment.underbillingSubID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMWipAdjustment>>) e).Cache, !useLineAccounts1);
    PXUIFieldAttribute.SetEnabled<PMWipAdjustment.revenueAccountID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMWipAdjustment>>) e).Cache, (object) e.Row, documentIsOnHold);
    PXUIFieldAttribute.SetEnabled<PMWipAdjustment.revenueSubID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMWipAdjustment>>) e).Cache, (object) e.Row, documentIsOnHold);
    PXUIFieldAttribute.SetRequired<PMWipAdjustment.revenueAccountID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMWipAdjustment>>) e).Cache, !useLineAccounts2);
    PXUIFieldAttribute.SetRequired<PMWipAdjustment.revenueSubID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMWipAdjustment>>) e).Cache, !useLineAccounts2);
    PXUIFieldAttribute.SetVisible<PMWipAdjustment.includePendingChangeOrders>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMWipAdjustment>>) e).Cache, (object) e.Row, plannedCostEstimation);
    PXUIFieldAttribute.SetEnabled<PMWipAdjustment.includePendingChangeOrders>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMWipAdjustment>>) e).Cache, (object) e.Row, plannedCostEstimation & documentIsOnHold);
    PXUIFieldAttribute.SetEnabled<PMWipAdjustmentLine.projectID>(((PXSelectBase) this.Items).Cache, (object) null, documentIsOnHold);
    PXUIFieldAttribute.SetVisible<PMWipAdjustmentLine.projectTaskID>(((PXSelectBase) this.Items).Cache, (object) null, useLineProject1 | useLineProject2);
    PXUIFieldAttribute.SetEnabled<PMWipAdjustmentLine.projectTaskID>(((PXSelectBase) this.Items).Cache, (object) null, documentIsOnHold);
    PXUIFieldAttribute.SetVisible<PMWipAdjustmentLine.projectionRefNbr>(((PXSelectBase) this.Items).Cache, (object) null, !plannedCostEstimation);
    PXUIFieldAttribute.SetEnabled<PMWipAdjustmentLine.projectionRefNbr>(((PXSelectBase) this.Items).Cache, (object) null, documentIsOnHold && !plannedCostEstimation);
    PXUIFieldAttribute.SetEnabled<PMWipAdjustmentLine.curyOverbillingAdjustmentAmount>(((PXSelectBase) this.Items).Cache, (object) null, documentIsOnHold);
    PXUIFieldAttribute.SetEnabled<PMWipAdjustmentLine.curyUnderbillingAdjustmentAmount>(((PXSelectBase) this.Items).Cache, (object) null, documentIsOnHold);
    PXUIFieldAttribute.SetEnabled<PMWipAdjustmentLine.overbillingAccountID>(((PXSelectBase) this.Items).Cache, (object) null, documentIsOnHold);
    PXUIFieldAttribute.SetEnabled<PMWipAdjustmentLine.overbillingSubID>(((PXSelectBase) this.Items).Cache, (object) null, documentIsOnHold);
    PXUIFieldAttribute.SetEnabled<PMWipAdjustmentLine.underbillingAccountID>(((PXSelectBase) this.Items).Cache, (object) null, documentIsOnHold);
    PXUIFieldAttribute.SetEnabled<PMWipAdjustmentLine.underbillingSubID>(((PXSelectBase) this.Items).Cache, (object) null, documentIsOnHold);
    PXUIFieldAttribute.SetEnabled<PMWipAdjustmentLine.revenueAccountID>(((PXSelectBase) this.Items).Cache, (object) null, documentIsOnHold);
    PXUIFieldAttribute.SetEnabled<PMWipAdjustmentLine.revenueSubID>(((PXSelectBase) this.Items).Cache, (object) null, documentIsOnHold);
    PXUIFieldAttribute.SetRequired<PMWipAdjustmentLine.projectTaskID>(((PXSelectBase) this.Items).Cache, useLineProject1 | useLineProject2);
    PXUIFieldAttribute.SetRequired<PMWipAdjustmentLine.overbillingAccountID>(((PXSelectBase) this.Items).Cache, useLineAccounts1);
    PXUIFieldAttribute.SetRequired<PMWipAdjustmentLine.overbillingSubID>(((PXSelectBase) this.Items).Cache, useLineAccounts1);
    PXUIFieldAttribute.SetRequired<PMWipAdjustmentLine.underbillingAccountID>(((PXSelectBase) this.Items).Cache, useLineAccounts1);
    PXUIFieldAttribute.SetRequired<PMWipAdjustmentLine.underbillingSubID>(((PXSelectBase) this.Items).Cache, useLineAccounts1);
    PXUIFieldAttribute.SetRequired<PMWipAdjustmentLine.revenueAccountID>(((PXSelectBase) this.Items).Cache, useLineAccounts2);
    PXUIFieldAttribute.SetRequired<PMWipAdjustmentLine.revenueSubID>(((PXSelectBase) this.Items).Cache, useLineAccounts2);
    PXUIFieldAttribute.SetVisible<PMCostProjectionByDate.projectionDate>(((PXGraph) this).Caches[typeof (PMCostProjectionByDate)], (object) null, !plannedCostEstimation);
    ((PXAction) this.printWipReport).SetEnabled(!flag);
    ((PXAction) this.printWipAdjustment).SetEnabled(!flag);
    this.UpdateLineActionStates();
    ((PXSelectBase) this.Approval).AllowSelect = PXAccess.FeatureInstalled<FeaturesSet.approvalWorkflow>();
  }

  protected virtual void _(PX.Data.Events.RowSelected<PMWipAdjustmentLine> e)
  {
    this.UpdateLineActionStates();
    if (e.Row == null)
      return;
    PXUIFieldAttribute.SetEnabled<PMWipAdjustmentLine.includePendingChangeOrders>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMWipAdjustmentLine>>) e).Cache, (object) e.Row, e.Row.ProjectionRefNbr == null && this.DocumentIsOnHold && this.UseBudgetForPlannedCostEstimation);
  }

  private void UpdateLineActionStates()
  {
    bool documentHasLines = this.DocumentHasLines;
    bool documentIsOnHold = this.DocumentIsOnHold;
    bool flag = this.SelectedItems.Any<PMWipAdjustmentLine>();
    ((PXAction) this.addProjects).SetEnabled(!documentHasLines & documentIsOnHold);
    ((PXAction) this.refreshLines).SetEnabled(documentHasLines & documentIsOnHold);
    ((PXAction) this.refreshSelected).SetEnabled(flag & documentHasLines & documentIsOnHold);
    ((PXAction) this.deleteSelected).SetEnabled(flag & documentHasLines & documentIsOnHold);
    ((PXAction) this.sendEmail).SetEnabled(flag & documentHasLines);
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<PMWipAdjustment, PMWipAdjustment.branchID> e)
  {
    this.AskAboutFieldUpdatingIfLinesExist("Change Calculation Parameters", "If you change any of the calculation parameters, all WIP adjustment lines will be cleared. Would you like to proceed with changing the parameters?", (System.Action) (() => ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMWipAdjustment, PMWipAdjustment.branchID>, PMWipAdjustment, object>) e).NewValue = e.OldValue));
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMWipAdjustment, PMWipAdjustment.branchID> e)
  {
    this.ClearAllLines();
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<PMWipAdjustment, PMWipAdjustment.projectStatus> e)
  {
    this.AskAboutFieldUpdatingIfLinesExist("Change Calculation Parameters", "If you change any of the calculation parameters, all WIP adjustment lines will be cleared. Would you like to proceed with changing the parameters?", (System.Action) (() => ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMWipAdjustment, PMWipAdjustment.projectStatus>, PMWipAdjustment, object>) e).NewValue = e.OldValue));
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMWipAdjustment, PMWipAdjustment.projectStatus> e)
  {
    this.ClearAllLines();
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<PMWipAdjustment, PMWipAdjustment.projectionDate> e)
  {
    this.AskAboutFieldUpdatingIfLinesExist("Recalculate All Lines", "All lines will be recalculated. Would you like to proceed with refreshing the WIP adjustment lines?", (System.Action) (() => ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMWipAdjustment, PMWipAdjustment.projectionDate>, PMWipAdjustment, object>) e).NewValue = e.OldValue));
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMWipAdjustment, PMWipAdjustment.projectionDate> e)
  {
    this.RefreshAllLines();
  }

  protected virtual void AskAboutFieldUpdatingIfLinesExist(
    string title,
    string message,
    System.Action ifNoAction)
  {
    if (!((IQueryable<PXResult<PMWipAdjustmentLine>>) ((PXSelectBase<PMWipAdjustmentLine>) this.Items).Select(Array.Empty<object>())).Any<PXResult<PMWipAdjustmentLine>>() || ((PXSelectBase<PMWipAdjustment>) this.Document).Ask(title, message, (MessageButtons) 4) != 7)
      return;
    ifNoAction();
  }

  protected virtual void Validate(bool throwIfError = false)
  {
    List<PXException> list = this.ValidateDocument().ToList<PXException>();
    foreach (PXResult<PMWipAdjustmentLine> pxResult in ((PXSelectBase<PMWipAdjustmentLine>) this.Items).Select(Array.Empty<object>()))
    {
      PMWipAdjustmentLine line = PXResult<PMWipAdjustmentLine>.op_Implicit(pxResult);
      list.AddRange(this.ValidateLine(line));
    }
    if (list.Any<PXException>() & throwIfError)
      throw list.First<PXException>();
  }

  protected virtual IEnumerable<PXException> ValidateDocument()
  {
    PMWipAdjustment document = ((PXSelectBase<PMWipAdjustment>) this.Document).Current;
    bool useOverbillingUnderbillingLineAccounts;
    bool flag;
    PostingOptions.ParsePostingOptions(document.OverbillingUnderbillingOption, out bool _, out useOverbillingUnderbillingLineAccounts, out flag);
    bool useRevenueLineAccounts;
    PostingOptions.ParsePostingOptions(document.RevenueOption, out flag, out useRevenueLineAccounts);
    int? nullable = document.OverbillingAccountID;
    if (!nullable.HasValue && !useOverbillingUnderbillingLineAccounts)
      yield return this.RaisePostingOptionFieldExceptionHandling<PMWipAdjustment.overbillingAccountID>(((PXSelectBase) this.Document).Cache, (object) document, document.OverbillingUnderbillingOption);
    nullable = document.OverbillingSubID;
    if (!nullable.HasValue && !useOverbillingUnderbillingLineAccounts)
      yield return this.RaisePostingOptionFieldExceptionHandling<PMWipAdjustment.overbillingSubID>(((PXSelectBase) this.Document).Cache, (object) document, document.OverbillingUnderbillingOption);
    nullable = document.UnderbillingAccountID;
    if (!nullable.HasValue && !useOverbillingUnderbillingLineAccounts)
      yield return this.RaisePostingOptionFieldExceptionHandling<PMWipAdjustment.underbillingAccountID>(((PXSelectBase) this.Document).Cache, (object) document, document.OverbillingUnderbillingOption);
    nullable = document.UnderbillingSubID;
    if (!nullable.HasValue && !useOverbillingUnderbillingLineAccounts)
      yield return this.RaisePostingOptionFieldExceptionHandling<PMWipAdjustment.underbillingSubID>(((PXSelectBase) this.Document).Cache, (object) document, document.OverbillingUnderbillingOption);
    nullable = document.RevenueAccountID;
    if (!nullable.HasValue && !useRevenueLineAccounts)
      yield return this.RaisePostingOptionFieldExceptionHandling<PMWipAdjustment.revenueAccountID>(((PXSelectBase) this.Document).Cache, (object) document, document.RevenueOption);
    nullable = document.RevenueSubID;
    if (!nullable.HasValue && !useRevenueLineAccounts)
      yield return this.RaisePostingOptionFieldExceptionHandling<PMWipAdjustment.revenueSubID>(((PXSelectBase) this.Document).Cache, (object) document, document.RevenueOption);
  }

  protected virtual IEnumerable<PXException> ValidateLine(PMWipAdjustmentLine line)
  {
    PMWipAdjustment document = ((PXSelectBase<PMWipAdjustment>) this.Document).Current;
    bool useOverbillingUnderbillingLineProjects;
    bool useOverbillingUnderbillingLineAccounts;
    PostingOptions.ParsePostingOptions(((PXSelectBase<PMWipAdjustment>) this.Document).Current.OverbillingUnderbillingOption, out useOverbillingUnderbillingLineProjects, out useOverbillingUnderbillingLineAccounts, out bool _);
    bool useRevenueLineProjects;
    bool useRevenueLineAccounts;
    PostingOptions.ParsePostingOptions(((PXSelectBase<PMWipAdjustment>) this.Document).Current.RevenueOption, out useRevenueLineProjects, out useRevenueLineAccounts);
    int? nullable = line.ProjectTaskID;
    if (!nullable.HasValue && useOverbillingUnderbillingLineProjects | useRevenueLineProjects)
      yield return this.RaisePostingOptionFieldExceptionHandling<PMWipAdjustmentLine.projectTaskID>(((PXSelectBase) this.Items).Cache, (object) line, useOverbillingUnderbillingLineProjects ? document.OverbillingUnderbillingOption : document.RevenueOption);
    nullable = line.OverbillingAccountID;
    if (!nullable.HasValue & useOverbillingUnderbillingLineAccounts)
      yield return this.RaisePostingOptionFieldExceptionHandling<PMWipAdjustmentLine.overbillingAccountID>(((PXSelectBase) this.Items).Cache, (object) line, document.OverbillingUnderbillingOption);
    nullable = line.OverbillingSubID;
    if (!nullable.HasValue & useOverbillingUnderbillingLineAccounts)
      yield return this.RaisePostingOptionFieldExceptionHandling<PMWipAdjustmentLine.overbillingSubID>(((PXSelectBase) this.Items).Cache, (object) line, document.OverbillingUnderbillingOption);
    nullable = line.UnderbillingAccountID;
    if (!nullable.HasValue & useOverbillingUnderbillingLineAccounts)
      yield return this.RaisePostingOptionFieldExceptionHandling<PMWipAdjustmentLine.underbillingAccountID>(((PXSelectBase) this.Items).Cache, (object) line, document.OverbillingUnderbillingOption);
    nullable = line.UnderbillingSubID;
    if (!nullable.HasValue & useOverbillingUnderbillingLineAccounts)
      yield return this.RaisePostingOptionFieldExceptionHandling<PMWipAdjustmentLine.underbillingSubID>(((PXSelectBase) this.Items).Cache, (object) line, document.OverbillingUnderbillingOption);
    nullable = line.RevenueAccountID;
    if (!nullable.HasValue & useRevenueLineAccounts)
      yield return this.RaisePostingOptionFieldExceptionHandling<PMWipAdjustmentLine.revenueAccountID>(((PXSelectBase) this.Items).Cache, (object) line, document.RevenueOption);
    nullable = line.RevenueSubID;
    if (!nullable.HasValue & useRevenueLineAccounts)
      yield return this.RaisePostingOptionFieldExceptionHandling<PMWipAdjustmentLine.revenueSubID>(((PXSelectBase) this.Items).Cache, (object) line, document.RevenueOption);
    nullable = line.OverbillingAccountID;
    if (nullable.HasValue & useOverbillingUnderbillingLineAccounts & useOverbillingUnderbillingLineProjects)
    {
      PXException ex;
      this.CheckIfAccountIsMappedToAccountGroup<PMWipAdjustmentLine.overbillingAccountID>(((PXSelectBase) this.Items).Cache, line, out ex);
      if (ex != null)
        yield return ex;
    }
    nullable = line.UnderbillingAccountID;
    if (nullable.HasValue & useOverbillingUnderbillingLineAccounts & useOverbillingUnderbillingLineProjects)
    {
      PXException ex;
      this.CheckIfAccountIsMappedToAccountGroup<PMWipAdjustmentLine.underbillingAccountID>(((PXSelectBase) this.Items).Cache, line, out ex);
      if (ex != null)
        yield return ex;
    }
    nullable = line.RevenueAccountID;
    if (nullable.HasValue & useRevenueLineAccounts & useRevenueLineProjects)
    {
      PXException ex;
      this.CheckIfAccountIsMappedToAccountGroup<PMWipAdjustmentLine.revenueAccountID>(((PXSelectBase) this.Items).Cache, line, out ex);
      if (ex != null)
        yield return ex;
    }
    nullable = line.ProjectID;
    if (nullable.HasValue && useOverbillingUnderbillingLineProjects | useRevenueLineProjects && document.ProjectStatus != "A")
    {
      PXException ex;
      this.CheckIfProjectCanBeAdjusted(((PXSelectBase) this.Items).Cache, line, out ex);
      if (ex != null)
        yield return ex;
    }
  }

  private void CheckIfAccountIsMappedToAccountGroup<TField>(
    PXCache cache,
    PMWipAdjustmentLine line,
    out PXException ex)
    where TField : IBqlField
  {
    ex = (PXException) null;
    int? accountID = cache.GetValue<TField>((object) line) as int?;
    if (!accountID.HasValue)
      return;
    PX.Objects.GL.Account account = PX.Objects.GL.Account.PK.Find((PXGraph) this, accountID, (PKFindOptions) 1);
    if (account.AccountGroupID.HasValue)
      return;
    ex = this.RaiseFieldExceptionHandling<TField>(cache, (object) line, (object) account.AccountCD, "The {0} account is not mapped to any account group.", (object) account.AccountCD);
  }

  private void CheckIfProjectCanBeAdjusted(
    PXCache cache,
    PMWipAdjustmentLine line,
    out PXException ex)
  {
    ex = (PXException) null;
    PMProject pmProject = PMProject.PK.Find((PXGraph) this, line.ProjectID, (PKFindOptions) 1);
    if (pmProject == null || pmProject.Status == "A")
      return;
    string localizedLabel = PXStringListAttribute.GetLocalizedLabel<PMProject.status>(((PXGraph) this).Caches[typeof (PMProject)], (object) pmProject);
    ex = this.RaiseFieldExceptionHandling<PMWipAdjustmentLine.projectID>(cache, (object) line, (object) pmProject.ContractCD, "Balances of projects with the {0} status can't be adjusted if the Detailed Projects posting option is specified on the Financial tab.", (object) localizedLabel);
  }

  private PXException RaisePostingOptionFieldExceptionHandling<TField>(
    PXCache cache,
    object row,
    string postingOption)
    where TField : IBqlField
  {
    return this.RaiseFieldExceptionHandling<TField>(cache, row, cache.GetValue<TField>(row), "The {0} cannot be empty if {1} is specified as the posting option on the Financial tab.", (object) PXUIFieldAttribute.GetDisplayName<TField>(cache), (object) PostingOptions.GetOptionDisplayName(postingOption));
  }

  private PXException RaiseFieldExceptionHandling<TField>(
    PXCache cache,
    object row,
    object value,
    string message,
    params object[] args)
    where TField : IBqlField
  {
    PXSetPropertyException<TField> propertyException = new PXSetPropertyException<TField>(message, (PXErrorLevel) 4, args);
    cache.RaiseExceptionHandling<TField>(row, value, (Exception) propertyException);
    return (PXException) propertyException;
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable AddProjects(PXAdapter adapter)
  {
    this.AppendAllProjects();
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable RefreshLines(PXAdapter adapter)
  {
    this.RefreshAllLines();
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable DeleteSelected(PXAdapter adapter)
  {
    this.DeleteSelectedLines();
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable RefreshSelected(PXAdapter adapter)
  {
    this.RefreshSelectedLines();
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Remove Hold", Enabled = false)]
  [PXButton]
  public virtual IEnumerable RemoveHold(PXAdapter adapter)
  {
    this.Validate(true);
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Hold", Enabled = false)]
  [PXButton]
  public virtual IEnumerable Hold(PXAdapter adapter) => adapter.Get();

  [PXUIField(DisplayName = "Release", Enabled = false)]
  [PXButton]
  public virtual IEnumerable Release(PXAdapter adapter)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: method pointer
    PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) new ProjectWipAdjustmentEntry.\u003C\u003Ec__DisplayClass62_0()
    {
      graph = PXGraph.CreateInstance<ProjectWipAdjustmentEntry>(),
      refNbr = ((PXSelectBase<PMWipAdjustment>) this.Document).Current.RefNbr
    }, __methodptr(\u003CRelease\u003Eb__0)));
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Void", Enabled = false)]
  [PXButton]
  public virtual IEnumerable Reverse(PXAdapter adapter)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: method pointer
    PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) new ProjectWipAdjustmentEntry.\u003C\u003Ec__DisplayClass64_0()
    {
      graph = PXGraph.CreateInstance<ProjectWipAdjustmentEntry>(),
      refNbr = ((PXSelectBase<PMWipAdjustment>) this.Document).Current.RefNbr
    }, __methodptr(\u003CReverse\u003Eb__0)));
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Print WIP Report", Enabled = false)]
  [PXButton]
  public virtual IEnumerable PrintWipReport(PXAdapter adapter)
  {
    ProjectAccountingService.NavigateToReport("PM651800", new Dictionary<string, string>()
    {
      ["DocumentID"] = ((PXSelectBase<PMWipAdjustment>) this.Document).Current?.RefNbr
    });
    return adapter.Get();
  }

  [PXUIField(DisplayName = "Print WIP Adjustment", Enabled = false)]
  [PXButton]
  public virtual IEnumerable PrintWipAdjustment(PXAdapter adapter)
  {
    ProjectAccountingService.NavigateToReport("PM651850", new Dictionary<string, string>()
    {
      ["DocumentID"] = ((PXSelectBase<PMWipAdjustment>) this.Document).Current?.RefNbr
    });
    return adapter.Get();
  }

  [PXButton]
  [PXUIField]
  protected virtual IEnumerable SendEmail(PXAdapter adapter)
  {
    return ((PXAction) ((PXGraph) this).GetExtension<PX.Objects.AR.ProjectWipAdjustmentEntry_ActivityDetailsExt>()?.NewMailActivity).Press(adapter);
  }

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = false)]
  public IEnumerable ViewProject(PXAdapter adapter)
  {
    PMWipAdjustmentLine current = ((PXSelectBase<PMWipAdjustmentLine>) this.Items).Current;
    if ((current != null ? (!current.ProjectID.HasValue ? 1 : 0) : 1) != 0)
      return adapter.Get();
    ProjectEntry instance = PXGraph.CreateInstance<ProjectEntry>();
    ((PXSelectBase<PMProject>) instance.Project).Current = PMProject.PK.Find((PXGraph) this, (int?) ((PXSelectBase<PMWipAdjustmentLine>) this.Items).Current?.ProjectID);
    ProjectAccountingService.NavigateToScreen((PXGraph) instance);
    return adapter.Get();
  }

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = false)]
  public IEnumerable ViewCostProjection(PXAdapter adapter)
  {
    if (((PXSelectBase<PMWipAdjustmentLine>) this.Items).Current?.ProjectionRefNbr == null)
      return adapter.Get();
    ProjectCostProjectionByDateEntry instance = PXGraph.CreateInstance<ProjectCostProjectionByDateEntry>();
    ((PXSelectBase<PMCostProjectionByDate>) instance.Document).Current = PMCostProjectionByDate.PK.Find((PXGraph) this, ((PXSelectBase<PMWipAdjustmentLine>) this.Items).Current?.ProjectionRefNbr);
    ProjectAccountingService.NavigateToScreen((PXGraph) instance);
    return adapter.Get();
  }

  protected virtual void ReleaseDocument(string refNbr)
  {
    PMWipAdjustment pmWipAdjustment = PMWipAdjustment.PK.Find((PXGraph) this, refNbr);
    if (pmWipAdjustment == null)
      return;
    ((PXSelectBase<PMWipAdjustment>) this.Document).Current = pmWipAdjustment;
    List<ProjectWipAdjustmentEntry.AdjustmentTransaction> transactions = new List<ProjectWipAdjustmentEntry.AdjustmentTransaction>();
    bool useLineProject1;
    bool useLineAccounts1;
    bool postTotalOnly;
    PostingOptions.ParsePostingOptions(pmWipAdjustment.OverbillingUnderbillingOption, out useLineProject1, out useLineAccounts1, out postTotalOnly);
    bool useLineProject2;
    bool useLineAccounts2;
    PostingOptions.ParsePostingOptions(pmWipAdjustment.RevenueOption, out useLineProject2, out useLineAccounts2);
    Decimal num = 0M;
    foreach (PXResult<PMWipAdjustmentLine> pxResult in ((PXSelectBase<PMWipAdjustmentLine>) this.Items).Select(Array.Empty<object>()))
    {
      PMWipAdjustmentLine wipAdjustmentLine = PXResult<PMWipAdjustmentLine>.op_Implicit(pxResult);
      Decimal? adjustmentAmount = wipAdjustmentLine.CuryOverbillingAdjustmentAmount;
      Decimal valueOrDefault1 = adjustmentAmount.GetValueOrDefault();
      adjustmentAmount = wipAdjustmentLine.CuryUnderbillingAdjustmentAmount;
      Decimal valueOrDefault2 = adjustmentAmount.GetValueOrDefault();
      Decimal amount = valueOrDefault2 - valueOrDefault1;
      int? overbillingSubId;
      int? accountId1;
      int? subId;
      int? accountId2;
      if (!useLineAccounts1)
      {
        int? underbillingAccountId = pmWipAdjustment.UnderbillingAccountID;
        int? underbillingSubId = pmWipAdjustment.UnderbillingSubID;
        int? overbillingAccountId = pmWipAdjustment.OverbillingAccountID;
        overbillingSubId = pmWipAdjustment.OverbillingSubID;
        accountId1 = overbillingAccountId;
        subId = underbillingSubId;
        accountId2 = underbillingAccountId;
      }
      else
      {
        int? underbillingAccountId = wipAdjustmentLine.UnderbillingAccountID;
        int? underbillingSubId = wipAdjustmentLine.UnderbillingSubID;
        int? overbillingAccountId = wipAdjustmentLine.OverbillingAccountID;
        overbillingSubId = wipAdjustmentLine.OverbillingSubID;
        accountId1 = overbillingAccountId;
        subId = underbillingSubId;
        accountId2 = underbillingAccountId;
      }
      int? revenueSubId;
      int? accountId3;
      if (!useLineAccounts2)
      {
        int? revenueAccountId = pmWipAdjustment.RevenueAccountID;
        revenueSubId = pmWipAdjustment.RevenueSubID;
        accountId3 = revenueAccountId;
      }
      else
      {
        int? revenueAccountId = wipAdjustmentLine.RevenueAccountID;
        revenueSubId = wipAdjustmentLine.RevenueSubID;
        accountId3 = revenueAccountId;
      }
      int? projectID1;
      int? taskId1;
      if (!useLineProject1)
      {
        projectID1 = new int?();
        taskId1 = new int?();
      }
      else
      {
        int? projectId = wipAdjustmentLine.ProjectID;
        taskId1 = wipAdjustmentLine.ProjectTaskID;
        projectID1 = projectId;
      }
      int? projectID2;
      int? taskId2;
      if (!useLineProject2)
      {
        projectID2 = new int?();
        taskId2 = new int?();
      }
      else
      {
        int? projectId = wipAdjustmentLine.ProjectID;
        taskId2 = wipAdjustmentLine.ProjectTaskID;
        projectID2 = projectId;
      }
      if (!postTotalOnly)
      {
        Add(ProjectWipAdjustmentEntry.AdjustmentTransaction.CreateTransaction(valueOrDefault1, accountId1, overbillingSubId, projectID1, taskId1));
        Add(ProjectWipAdjustmentEntry.AdjustmentTransaction.CreateTransaction(-valueOrDefault2, accountId2, subId, projectID1, taskId1));
      }
      Add(ProjectWipAdjustmentEntry.AdjustmentTransaction.CreateTransaction(amount, accountId3, revenueSubId, projectID2, taskId2));
      num += amount;
    }
    if (postTotalOnly)
    {
      if (num > 0M)
        Add(ProjectWipAdjustmentEntry.AdjustmentTransaction.CreateTransaction(-num, pmWipAdjustment.UnderbillingAccountID, pmWipAdjustment.UnderbillingSubID));
      else
        Add(ProjectWipAdjustmentEntry.AdjustmentTransaction.CreateTransaction(-num, pmWipAdjustment.OverbillingAccountID, pmWipAdjustment.OverbillingSubID));
    }
    transactions = transactions.GroupBy<ProjectWipAdjustmentEntry.AdjustmentTransaction, (int, int, int, int)>((Func<ProjectWipAdjustmentEntry.AdjustmentTransaction, (int, int, int, int)>) (key => (key.ProjectID, key.ProjectTaskID, key.AccountID, key.SubID))).Select<IGrouping<(int, int, int, int), ProjectWipAdjustmentEntry.AdjustmentTransaction>, ProjectWipAdjustmentEntry.AdjustmentTransaction>((Func<IGrouping<(int, int, int, int), ProjectWipAdjustmentEntry.AdjustmentTransaction>, ProjectWipAdjustmentEntry.AdjustmentTransaction>) (x => new ProjectWipAdjustmentEntry.AdjustmentTransaction()
    {
      ProjectID = x.Key.ProjectID,
      ProjectTaskID = x.Key.ProjectTaskID,
      AccountID = x.Key.AccountID,
      SubID = x.Key.SubID,
      DebitAmount = x.Sum<ProjectWipAdjustmentEntry.AdjustmentTransaction>((Func<ProjectWipAdjustmentEntry.AdjustmentTransaction, Decimal>) (_ => _.DebitAmount)),
      CreditAmount = x.Sum<ProjectWipAdjustmentEntry.AdjustmentTransaction>((Func<ProjectWipAdjustmentEntry.AdjustmentTransaction, Decimal>) (_ => _.CreditAmount))
    })).Where<ProjectWipAdjustmentEntry.AdjustmentTransaction>((Func<ProjectWipAdjustmentEntry.AdjustmentTransaction, bool>) (x => x.DebitAmount > 0M || x.CreditAmount > 0M)).ToList<ProjectWipAdjustmentEntry.AdjustmentTransaction>();
    if (!transactions.Any<ProjectWipAdjustmentEntry.AdjustmentTransaction>())
      return;
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      JournalEntry instance = PXGraph.CreateInstance<JournalEntry>();
      ((PXSelectBase<Batch>) instance.BatchModule).Insert(new Batch()
      {
        Module = "PM",
        BranchID = pmWipAdjustment.BranchID,
        CuryID = pmWipAdjustment.CuryID,
        CuryInfoID = pmWipAdjustment.CuryInfoID,
        DateEntered = pmWipAdjustment.ProjectionDate,
        FinPeriodID = pmWipAdjustment.FinPeriodID,
        AutoReverse = new bool?(true),
        Description = $"Project WIP Adjustment for the {pmWipAdjustment.ProjectionDate:d} date"
      });
      foreach (ProjectWipAdjustmentEntry.AdjustmentTransaction adjustmentTransaction in transactions)
      {
        adjustmentTransaction.Normalize();
        ((PXSelectBase<PX.Objects.GL.GLTran>) instance.GLTranModuleBatNbr).Insert(new PX.Objects.GL.GLTran()
        {
          AccountID = adjustmentTransaction.AccountID != 0 ? new int?(adjustmentTransaction.AccountID) : new int?(),
          SubID = adjustmentTransaction.SubID != 0 ? new int?(adjustmentTransaction.SubID) : new int?(),
          ProjectID = new int?(adjustmentTransaction.ProjectID),
          TaskID = adjustmentTransaction.ProjectTaskID != 0 ? new int?(adjustmentTransaction.ProjectTaskID) : new int?(),
          CuryInfoID = pmWipAdjustment.CuryInfoID,
          CuryDebitAmt = new Decimal?(adjustmentTransaction.DebitAmount),
          CuryCreditAmt = new Decimal?(adjustmentTransaction.CreditAmount),
          TranDesc = $"Project WIP Adjustment for the {pmWipAdjustment.ProjectionDate:d} date"
        });
      }
      if (((PXSelectBase<Batch>) instance.BatchModule).Current.Status == "H")
        ((PXAction) instance.releaseFromHold).Press();
      ((PXAction) instance.Save).Press();
      this.ReleaseGLBatch(((PXSelectBase<Batch>) instance.BatchModule).Current);
      pmWipAdjustment.BatchNbr = ((PXSelectBase<Batch>) instance.BatchModule).Current.BatchNbr;
      ((PXSelectBase) this.Document).Cache.Update((object) pmWipAdjustment);
      ((PXAction) this.Save).Press();
      transactionScope.Complete();
    }

    void Add(
      ProjectWipAdjustmentEntry.AdjustmentTransaction transaction)
    {
      transactions.Add(transaction);
    }
  }

  protected virtual void ReverseDocument(string refNbr)
  {
    PMWipAdjustment pmWipAdjustment = PMWipAdjustment.PK.Find((PXGraph) this, refNbr);
    if (pmWipAdjustment == null || pmWipAdjustment.BatchNbr == null)
      return;
    ((PXSelectBase<PMWipAdjustment>) this.Document).Current = pmWipAdjustment;
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      if (Batch.PK.Find((PXGraph) this, "PM", pmWipAdjustment.BatchNbr) != null)
      {
        JournalEntry instance = PXGraph.CreateInstance<JournalEntry>();
        instance.ReverseBatchProc(Batch.PK.Find((PXGraph) this, "PM", pmWipAdjustment.BatchNbr));
        if (((PXSelectBase<Batch>) instance.BatchModule).Current != null)
        {
          if (((PXSelectBase<Batch>) instance.BatchModule).Current.Status == "H")
            ((PXAction) instance.releaseFromHold).Press();
          ((PXAction) instance.Save).Press();
          this.ReleaseGLBatch(((PXSelectBase<Batch>) instance.BatchModule).Current);
        }
      }
      pmWipAdjustment.BatchNbr = (string) null;
      ((PXSelectBase) this.Document).Cache.Update((object) pmWipAdjustment);
      ((PXAction) this.Save).Press();
      transactionScope.Complete();
    }
  }

  private void ReleaseGLBatch(Batch batch)
  {
    JournalEntry.ReleaseBatch((IList<Batch>) new List<Batch>()
    {
      batch
    });
  }

  protected virtual bool BulkMode { get; set; }

  protected virtual int ChunkSize => 1000;

  protected virtual void AppendAllProjects()
  {
    PMProject[] array = GraphHelper.RowCast<PMProject>((IEnumerable) PXSelectBase<PMProject, PXViewOf<PMProject>.BasedOn<SelectFromBase<PMProject, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMProject.baseType, Equal<CTPRType.project>>>>>.And<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMProject.nonProject, Equal<False>>>>, And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMProject.defaultBranchID, Equal<BqlField<PMWipAdjustment.branchID, IBqlInt>.FromCurrent>>>>>.Or<BqlOperand<PMProject.defaultBranchID, IBqlInt>.IsNull>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMProject.curyID, Equal<BqlField<PMWipAdjustment.curyID, IBqlString>.FromCurrent>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<PMWipAdjustment.projectStatus>, Contains<PMProject.status>>>>>.And<Match<Current<AccessInfo.userName>>>>>>>>.ReadOnly.Config>.Select((PXGraph) this, Array.Empty<object>())).ToArray<PMProject>();
    bool flag = false;
    if (array.Length > this.ChunkSize)
    {
      if (((PXSelectBase<PMWipAdjustmentLine>) this.Items).Ask("Warning", "This operation may take a significant amount of time. Do you want to continue?", (MessageButtons) 4) == 7)
        return;
      flag = true;
    }
    if (flag)
      this.AppendProjectsInChunks((IEnumerable<PMProject>) array);
    else
      this.AppendProjects((IEnumerable<PMProject>) array);
  }

  public static void AppendProjectsInChunks(
    string refNbr,
    IEnumerable<PMProject> projects,
    int chunkSize)
  {
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      foreach (IEnumerable<PMProject> chunk in ProjectWipAdjustmentEntry.GetChunks<PMProject>(projects, chunkSize))
      {
        ProjectWipAdjustmentEntry instance = PXGraph.CreateInstance<ProjectWipAdjustmentEntry>();
        ((PXSelectBase<PMWipAdjustment>) instance.Document).Current = PMWipAdjustment.PK.Find((PXGraph) instance, refNbr);
        instance.AppendProjects(chunk);
        ((PXAction) instance.Save).Press();
      }
      transactionScope.Complete();
    }
  }

  public virtual void AppendProjectsInChunks(IEnumerable<PMProject> projects)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    ProjectWipAdjustmentEntry.\u003C\u003Ec__DisplayClass87_0 cDisplayClass870 = new ProjectWipAdjustmentEntry.\u003C\u003Ec__DisplayClass87_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass870.projects = projects;
    ((PXAction) this.Save).Press();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass870.refNbr = ((PXSelectBase<PMWipAdjustment>) this.Document).Current.RefNbr;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass870.chunkSize = this.ChunkSize;
    // ISSUE: method pointer
    PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) cDisplayClass870, __methodptr(\u003CAppendProjectsInChunks\u003Eb__0)));
  }

  public static IEnumerable<IEnumerable<T>> GetChunks<T>(IEnumerable<T> values, int chunkSize)
  {
    for (; values.Any<T>(); values = (IEnumerable<T>) values.Skip<T>(chunkSize).ToList<T>())
      yield return (IEnumerable<T>) values.Take<T>(chunkSize).ToList<T>();
  }

  private IEnumerable<PMProject> PrepareData(IEnumerable<PMProject> projects)
  {
    return (IEnumerable<PMProject>) (this._projectsCache = projects.ToDictionary<PMProject, int?>((Func<PMProject, int?>) (x => x.ProjectID))).Values.OrderBy<PMProject, string>((Func<PMProject, string>) (p => p.ContractCD));
  }

  public virtual void AppendProjects(IEnumerable<PMProject> projects)
  {
    this.BulkMode = true;
    foreach (PMProject project in this.PrepareData(projects))
      this.AppendProject(project);
    ((PXSelectBase) this.Items).View.RequestRefresh();
  }

  protected virtual void AppendProject(PMProject project)
  {
    PMWipAdjustmentLine line = new PMWipAdjustmentLine()
    {
      ProjectID = project.ContractID
    };
    this.UpdateLineWithProject(line, line.ProjectID);
    this.DefaultLineAccounts(line, project);
    if (this.UseBudgetForPlannedCostEstimation)
      this.UpdateLineWithCostProjection(line, (string) null);
    if (!(((PXSelectBase) this.Items).Cache.Insert((object) line) is PMWipAdjustmentLine wipAdjustmentLine) || this.UseBudgetForPlannedCostEstimation)
      return;
    ((PXSelectBase) this.Items).Cache.SetDefaultExt<PMWipAdjustmentLine.projectionRefNbr>((object) wipAdjustmentLine);
    ((PXSelectBase) this.Items).Cache.Update((object) wipAdjustmentLine);
  }

  protected virtual void ClearAllLines()
  {
    foreach (PXResult<PMWipAdjustmentLine> pxResult in ((PXSelectBase<PMWipAdjustmentLine>) this.Items).Select(Array.Empty<object>()))
      ((PXSelectBase<PMWipAdjustmentLine>) this.Items).Delete(PXResult<PMWipAdjustmentLine>.op_Implicit(pxResult));
  }

  protected virtual void RefreshAllLines()
  {
    this.BulkMode = true;
    foreach (PXResult<PMWipAdjustmentLine> pxResult in ((PXSelectBase<PMWipAdjustmentLine>) this.Items).Select(Array.Empty<object>()))
      this.RefreshLine(PXResult<PMWipAdjustmentLine>.op_Implicit(pxResult));
    ((PXSelectBase) this.Items).View.RequestRefresh();
  }

  private IEnumerable<PMWipAdjustmentLine> SelectedItems
  {
    get
    {
      return GraphHelper.RowCast<PMWipAdjustmentLine>(((PXSelectBase) this.Items).Cache.Cached).Where<PMWipAdjustmentLine>((Func<PMWipAdjustmentLine, bool>) (x => x.Selected.GetValueOrDefault() && ((PXSelectBase) this.Items).Cache.GetStatus((object) x) != 3 && ((PXSelectBase) this.Items).Cache.GetStatus((object) x) != 4));
    }
  }

  protected virtual void DeleteSelectedLines()
  {
    foreach (object obj in this.SelectedItems.ToArray<PMWipAdjustmentLine>())
      ((PXSelectBase) this.Items).Cache.Delete(obj);
  }

  protected virtual void RefreshSelectedLines()
  {
    this.BulkMode = true;
    foreach (PMWipAdjustmentLine selectedItem in this.SelectedItems)
      this.RefreshLine(selectedItem);
    ((PXSelectBase) this.Items).View.RequestRefresh();
  }

  protected virtual void RefreshLine(PMWipAdjustmentLine line)
  {
    if (line == null)
      return;
    this.UpdateLineWithProject(line, new int?());
    this.UpdateLineWithCostProjection(line, (string) null);
    line = ((PXSelectBase) this.Items).Cache.Update((object) line) as PMWipAdjustmentLine;
    this.UpdateLineWithProject(line, line.ProjectID);
    if (this.UseBudgetForPlannedCostEstimation)
      this.UpdateLineWithCostProjection(line, (string) null);
    ((PXSelectBase) this.Items).Cache.SetValue<PMWipAdjustmentLine.selected>((object) line, (object) false);
    line = ((PXSelectBase) this.Items).Cache.Update((object) line) as PMWipAdjustmentLine;
    if (this.UseBudgetForPlannedCostEstimation)
      return;
    ((PXSelectBase) this.Items).Cache.SetDefaultExt<PMWipAdjustmentLine.projectionRefNbr>((object) line);
    ((PXSelectBase) this.Items).Cache.Update((object) line);
  }

  protected virtual void DefaultLineAccounts(PMWipAdjustmentLine line)
  {
    if (line.ProjectID.HasValue)
    {
      PMProject project = (PMProject) null;
      if (this.BulkMode && this._projectsCache != null)
        this._projectsCache.TryGetValue(line.ProjectID, out project);
      if (project == null)
        project = PMProject.PK.Find((PXGraph) this, line.ProjectID);
      this.DefaultLineAccounts(line, project);
    }
    else
    {
      line.OverbillingAccountID = new int?();
      line.OverbillingSubID = new int?();
      line.UnderbillingAccountID = new int?();
      line.UnderbillingSubID = new int?();
      line.RevenueAccountID = new int?();
      line.RevenueSubID = new int?();
    }
  }

  protected virtual void DefaultLineAccounts(PMWipAdjustmentLine line, PMProject project)
  {
    line.OverbillingAccountID = (int?) project?.DefaultOverbillingAccountID;
    line.OverbillingSubID = (int?) project?.DefaultOverbillingSubID;
    line.UnderbillingAccountID = (int?) project?.DefaultUnderbillingAccountID;
    line.UnderbillingSubID = (int?) project?.DefaultUnderbillingSubID;
    line.RevenueAccountID = (int?) project?.DefaultSalesAccountID;
    line.RevenueSubID = (int?) project?.DefaultSalesSubID;
  }

  protected virtual void UpdateLineWithProject(PMWipAdjustmentLine line, int? projectID)
  {
    if (projectID.HasValue)
    {
      line.CuryOriginalRevenueAmount = new Decimal?(this.GetProjectBudgetOriginalAmount(projectID.Value, "I").GetValueOrDefault());
      line.CuryOriginalCostAmount = new Decimal?(this.GetProjectBudgetOriginalAmount(projectID.Value, "E").GetValueOrDefault());
      line.CuryOriginalCommitmentAmount = new Decimal?(this.GetProjectOriginalCommitmentsAmount(projectID.Value).GetValueOrDefault());
      line.CuryApprovedCommitmentAmount = new Decimal?(this.GetProjectApprovedCommitmentsAmount(projectID.Value).GetValueOrDefault());
      line.CuryPeriodCostAmount = new Decimal?(this.GetProjectPeriodCostsAmount(projectID.Value).GetValueOrDefault());
      PMWipAdjustmentLine wipAdjustmentLine = line;
      Decimal? periodBillingsAmount = this.GetProjectPeriodBillingsAmount(projectID.Value);
      Decimal? nullable = new Decimal?((periodBillingsAmount.HasValue ? new Decimal?(-periodBillingsAmount.GetValueOrDefault()) : new Decimal?()).GetValueOrDefault());
      wipAdjustmentLine.CuryPeriodBillingAmount = nullable;
    }
    else
    {
      line.CuryOriginalRevenueAmount = new Decimal?();
      line.CuryOriginalCostAmount = new Decimal?();
      line.CuryOriginalCommitmentAmount = new Decimal?();
      line.CuryApprovedCommitmentAmount = new Decimal?();
      line.CuryPeriodCostAmount = new Decimal?();
      line.CuryPeriodBillingAmount = new Decimal?();
    }
  }

  protected virtual void UpdateLineWithCostProjection(
    PMWipAdjustmentLine line,
    string projectionRefNbr)
  {
    Decimal? nullable1 = new Decimal?();
    Decimal? nullable2;
    Decimal? nullable3;
    if (!string.IsNullOrWhiteSpace(projectionRefNbr))
    {
      PMCostProjectionByDate projectionByDate = PMCostProjectionByDate.PK.Find((PXGraph) this, projectionRefNbr);
      line.IncludePendingChangeOrders = (bool?) projectionByDate?.IncludePendingChangeOrders;
      PMWipAdjustmentLine wipAdjustmentLine1 = line;
      bool? pendingChangeOrders = line.IncludePendingChangeOrders;
      Decimal? nullable4 = pendingChangeOrders.GetValueOrDefault() ? this.RoundAmount((Decimal?) projectionByDate?.CuryPendingRevenueChangeOrderAmountTotal) : new Decimal?();
      wipAdjustmentLine1.CuryPendingRevenueChangeOrderAmount = nullable4;
      line.CuryRevisedRevenueBudgetedAmount = this.RoundAmount((Decimal?) projectionByDate?.CuryRevisedRevenueBudgetedAmountTotal);
      PMWipAdjustmentLine wipAdjustmentLine2 = line;
      pendingChangeOrders = line.IncludePendingChangeOrders;
      Decimal? nullable5 = pendingChangeOrders.GetValueOrDefault() ? this.RoundAmount((Decimal?) projectionByDate?.CuryPendingChangeOrderAmountTotal) : new Decimal?();
      wipAdjustmentLine2.CuryPendingCostChangeOrderAmount = nullable5;
      line.CuryRevisedCostBudgetedAmount = this.RoundAmount((Decimal?) projectionByDate?.CuryBudgetedAmountTotal);
      PMWipAdjustmentLine wipAdjustmentLine3 = line;
      pendingChangeOrders = line.IncludePendingChangeOrders;
      Decimal? nullable6 = pendingChangeOrders.GetValueOrDefault() ? this.RoundAmount((Decimal?) projectionByDate?.CuryPendingCommitmentAmountTotal) : new Decimal?();
      wipAdjustmentLine3.CuryPendingCommitmentAmount = nullable6;
      line.CuryProjectedAmount = this.RoundAmount((Decimal?) projectionByDate?.CuryProjectedAmountTotal);
      line.CuryProjectedMarginAmount = this.RoundAmount((Decimal?) projectionByDate?.CuryProjectedMarginTotal);
      line.ProjectedMarginPct = (Decimal?) projectionByDate?.ProjectedMarginPctTotal;
      line.CuryBilledRevenueAmount = this.RoundAmount((Decimal?) projectionByDate?.CuryBilledRevenueAmountTotal);
      line.CuryActualAmount = this.RoundAmount((Decimal?) projectionByDate?.CuryActualAmountTotal);
      line.CompletedPct = this.RoundPercent((Decimal?) projectionByDate?.CompletedPctTotal);
      line.CuryRevenueExpectedAmount = this.RoundAmount((Decimal?) projectionByDate?.CuryRevenueExpectedAmountTotal);
      nullable1 = this.RoundAmount((Decimal?) projectionByDate?.CuryOverbillingAmountTotal);
    }
    else
    {
      int? projectId = line.ProjectID;
      if (projectId.HasValue && this.UseBudgetForPlannedCostEstimation)
      {
        PMWipAdjustmentLine wipAdjustmentLine4 = line;
        bool? pendingChangeOrders = line.IncludePendingChangeOrders;
        Decimal? nullable7 = pendingChangeOrders.GetValueOrDefault() ? new Decimal?(this.GetProjectBudgetPendingAmount(projectId.Value, "I").GetValueOrDefault()) : new Decimal?();
        wipAdjustmentLine4.CuryPendingRevenueChangeOrderAmount = nullable7;
        PMWipAdjustmentLine wipAdjustmentLine5 = line;
        Decimal num1 = this.GetProjectBudgetChangedAmount(projectId.Value, "I").GetValueOrDefault() + line.CuryOriginalRevenueAmount.GetValueOrDefault();
        nullable2 = line.CuryPendingRevenueChangeOrderAmount;
        Decimal valueOrDefault1 = nullable2.GetValueOrDefault();
        Decimal? nullable8 = new Decimal?(num1 + valueOrDefault1);
        wipAdjustmentLine5.CuryRevisedRevenueBudgetedAmount = nullable8;
        PMWipAdjustmentLine wipAdjustmentLine6 = line;
        pendingChangeOrders = line.IncludePendingChangeOrders;
        Decimal? nullable9;
        if (!pendingChangeOrders.GetValueOrDefault())
        {
          nullable2 = new Decimal?();
          nullable9 = nullable2;
        }
        else
        {
          nullable2 = this.GetProjectBudgetPendingAmount(projectId.Value, "E");
          nullable9 = new Decimal?(nullable2.GetValueOrDefault());
        }
        wipAdjustmentLine6.CuryPendingCostChangeOrderAmount = nullable9;
        PMWipAdjustmentLine wipAdjustmentLine7 = line;
        nullable2 = this.GetProjectBudgetChangedAmount(projectId.Value, "E");
        Decimal valueOrDefault2 = nullable2.GetValueOrDefault();
        nullable2 = line.CuryOriginalCostAmount;
        Decimal valueOrDefault3 = nullable2.GetValueOrDefault();
        Decimal num2 = valueOrDefault2 + valueOrDefault3;
        nullable2 = line.CuryPendingCostChangeOrderAmount;
        Decimal valueOrDefault4 = nullable2.GetValueOrDefault();
        Decimal? nullable10 = new Decimal?(num2 + valueOrDefault4);
        wipAdjustmentLine7.CuryRevisedCostBudgetedAmount = nullable10;
        PMWipAdjustmentLine wipAdjustmentLine8 = line;
        pendingChangeOrders = line.IncludePendingChangeOrders;
        Decimal? nullable11;
        if (!pendingChangeOrders.GetValueOrDefault())
        {
          nullable2 = new Decimal?();
          nullable11 = nullable2;
        }
        else
        {
          nullable2 = this.GetProjectPendingCommitmentsAmount(projectId.Value);
          nullable11 = new Decimal?(nullable2.GetValueOrDefault());
        }
        wipAdjustmentLine8.CuryPendingCommitmentAmount = nullable11;
        PMWipAdjustmentLine wipAdjustmentLine9 = line;
        nullable2 = line.CuryRevisedCostBudgetedAmount;
        Decimal valueOrDefault5 = nullable2.GetValueOrDefault();
        nullable2 = line.CuryPendingCostChangeOrderAmount;
        Decimal valueOrDefault6 = nullable2.GetValueOrDefault();
        Decimal? nullable12 = new Decimal?(valueOrDefault5 - valueOrDefault6);
        wipAdjustmentLine9.CuryProjectedAmount = nullable12;
        PMWipAdjustmentLine wipAdjustmentLine10 = line;
        nullable2 = line.CuryRevisedRevenueBudgetedAmount;
        Decimal valueOrDefault7 = nullable2.GetValueOrDefault();
        nullable2 = line.CuryRevisedCostBudgetedAmount;
        Decimal valueOrDefault8 = nullable2.GetValueOrDefault();
        Decimal? nullable13 = new Decimal?(valueOrDefault7 - valueOrDefault8);
        wipAdjustmentLine10.CuryProjectedMarginAmount = nullable13;
        PMWipAdjustmentLine wipAdjustmentLine11 = line;
        nullable2 = line.CuryRevisedRevenueBudgetedAmount;
        Decimal? nullable14;
        if (!(nullable2.GetValueOrDefault() != 0M))
        {
          nullable2 = new Decimal?();
          nullable14 = nullable2;
        }
        else
        {
          nullable3 = line.CuryProjectedMarginAmount;
          Decimal valueOrDefault9 = line.CuryRevisedRevenueBudgetedAmount.GetValueOrDefault();
          nullable2 = nullable3.HasValue ? new Decimal?(nullable3.GetValueOrDefault() / valueOrDefault9) : new Decimal?();
          Decimal num3 = (Decimal) 100;
          Decimal? percent;
          if (!nullable2.HasValue)
          {
            nullable3 = new Decimal?();
            percent = nullable3;
          }
          else
            percent = new Decimal?(nullable2.GetValueOrDefault() * num3);
          nullable14 = this.RoundPercent(percent);
        }
        wipAdjustmentLine11.ProjectedMarginPct = nullable14;
        PMWipAdjustmentLine wipAdjustmentLine12 = line;
        nullable2 = this.GetProjectBillingsToPeriodAmount(projectId.Value);
        Decimal? nullable15;
        if (!nullable2.HasValue)
        {
          nullable3 = new Decimal?();
          nullable15 = nullable3;
        }
        else
          nullable15 = new Decimal?(-nullable2.GetValueOrDefault());
        nullable3 = nullable15;
        Decimal? nullable16 = new Decimal?(nullable3.GetValueOrDefault());
        wipAdjustmentLine12.CuryBilledRevenueAmount = nullable16;
        PMWipAdjustmentLine wipAdjustmentLine13 = line;
        nullable2 = this.GetProjectActualCostsAmount(projectId.Value);
        Decimal? nullable17 = new Decimal?(nullable2.GetValueOrDefault());
        wipAdjustmentLine13.CuryActualAmount = nullable17;
        nullable2 = line.CuryRevisedCostBudgetedAmount;
        Decimal? nullable18;
        if (!(nullable2.GetValueOrDefault() != 0M))
        {
          nullable2 = new Decimal?();
          nullable18 = nullable2;
        }
        else
        {
          nullable2 = line.CuryActualAmount;
          nullable3 = line.CuryRevisedCostBudgetedAmount;
          Decimal valueOrDefault10 = nullable3.GetValueOrDefault();
          if (!nullable2.HasValue)
          {
            nullable3 = new Decimal?();
            nullable18 = nullable3;
          }
          else
            nullable18 = new Decimal?(nullable2.GetValueOrDefault() / valueOrDefault10);
        }
        Decimal? nullable19 = nullable18;
        PMWipAdjustmentLine wipAdjustmentLine14 = line;
        nullable2 = nullable19;
        Decimal num4 = (Decimal) 100;
        Decimal? percent1;
        if (!nullable2.HasValue)
        {
          nullable3 = new Decimal?();
          percent1 = nullable3;
        }
        else
          percent1 = new Decimal?(nullable2.GetValueOrDefault() * num4);
        Decimal? nullable20 = this.RoundPercent(percent1);
        wipAdjustmentLine14.CompletedPct = nullable20;
        PMWipAdjustmentLine wipAdjustmentLine15 = line;
        nullable2 = nullable19;
        nullable3 = line.CuryRevisedRevenueBudgetedAmount;
        Decimal? nullable21 = this.RoundAmount(nullable2.HasValue & nullable3.HasValue ? new Decimal?(nullable2.GetValueOrDefault() * nullable3.GetValueOrDefault()) : new Decimal?());
        wipAdjustmentLine15.CuryRevenueExpectedAmount = nullable21;
        nullable3 = line.CuryBilledRevenueAmount;
        nullable2 = line.CuryRevenueExpectedAmount;
        nullable1 = nullable3.HasValue & nullable2.HasValue ? new Decimal?(nullable3.GetValueOrDefault() - nullable2.GetValueOrDefault()) : new Decimal?();
      }
      else
      {
        line.CuryPendingRevenueChangeOrderAmount = new Decimal?();
        line.CuryRevisedRevenueBudgetedAmount = new Decimal?();
        line.CuryPendingCostChangeOrderAmount = new Decimal?();
        line.CuryRevisedCostBudgetedAmount = new Decimal?();
        line.CuryPendingCommitmentAmount = new Decimal?();
        line.CuryProjectedAmount = new Decimal?();
        line.CuryProjectedMarginAmount = new Decimal?();
        line.ProjectedMarginPct = new Decimal?();
        line.CompletedPct = new Decimal?();
        line.CuryRevenueExpectedAmount = new Decimal?();
        line.CuryBilledRevenueAmount = new Decimal?();
        line.CuryActualAmount = new Decimal?();
      }
    }
    PMWipAdjustmentLine wipAdjustmentLine16 = line;
    nullable2 = nullable1;
    Decimal num5 = 0M;
    Decimal? nullable22 = nullable2.GetValueOrDefault() >= num5 & nullable2.HasValue ? nullable1 : new Decimal?(0M);
    wipAdjustmentLine16.CuryOverbillingAmount = nullable22;
    PMWipAdjustmentLine wipAdjustmentLine17 = line;
    nullable2 = nullable1;
    Decimal num6 = 0M;
    Decimal? nullable23;
    if (!(nullable2.GetValueOrDefault() < num6 & nullable2.HasValue))
    {
      nullable23 = new Decimal?(0M);
    }
    else
    {
      nullable2 = nullable1;
      if (!nullable2.HasValue)
      {
        nullable3 = new Decimal?();
        nullable23 = nullable3;
      }
      else
        nullable23 = new Decimal?(-nullable2.GetValueOrDefault());
    }
    wipAdjustmentLine17.CuryUnderbillingAmount = nullable23;
    line.CuryOverbillingAdjustmentAmount = line.CuryOverbillingAmount;
    line.CuryUnderbillingAdjustmentAmount = line.CuryUnderbillingAmount;
  }

  private Decimal? RoundAmount(Decimal? amount)
  {
    return !amount.HasValue ? new Decimal?() : new Decimal?(Math.Round(amount.Value, 2, MidpointRounding.AwayFromZero));
  }

  private Decimal? RoundPercent(Decimal? percent)
  {
    return !percent.HasValue ? new Decimal?() : new Decimal?(Math.Round(percent.Value, 2, MidpointRounding.AwayFromZero));
  }

  private Decimal? GetProjectBudgetOriginalAmount(int projectID, string budgetType)
  {
    if (!this.BulkMode)
      return this._GetProjectBudgetOriginalAmount(projectID, budgetType);
    if (this._projectOriginalBudgetCache == null)
      this._projectOriginalBudgetCache = GraphHelper.RowCast<PMBudget>((IEnumerable) PXSelectBase<PMBudget, PXViewOf<PMBudget>.BasedOn<SelectFromBase<PMBudget, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PMProject>.On<BqlOperand<PMBudget.projectID, IBqlInt>.IsEqual<PMProject.contractID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMProject.baseType, Equal<CTPRType.project>>>>>.And<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMProject.nonProject, Equal<False>>>>, And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMProject.defaultBranchID, Equal<BqlField<PMWipAdjustment.branchID, IBqlInt>.FromCurrent>>>>>.Or<BqlOperand<PMProject.defaultBranchID, IBqlInt>.IsNull>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMProject.curyID, Equal<BqlField<PMWipAdjustment.curyID, IBqlString>.FromCurrent>>>>>.And<BqlOperand<Current<PMWipAdjustment.projectStatus>, IBqlString>.Contains<PMProject.status>>>>>.Aggregate<To<GroupBy<PMBudget.projectID>, GroupBy<PMBudget.type>, Sum<PMBudget.curyAmount>>>>.ReadOnly.Config>.Select((PXGraph) this, Array.Empty<object>())).ToDictionary<PMBudget, ProjectWipAdjustmentEntry.ProjectBudgetKey, Decimal?>((Func<PMBudget, ProjectWipAdjustmentEntry.ProjectBudgetKey>) (key => new ProjectWipAdjustmentEntry.ProjectBudgetKey(key.ProjectID.Value, key.Type)), (Func<PMBudget, Decimal?>) (value => value.CuryAmount));
    Decimal? nullable;
    return this._projectOriginalBudgetCache.TryGetValue(new ProjectWipAdjustmentEntry.ProjectBudgetKey(projectID, budgetType), out nullable) ? nullable : new Decimal?();
  }

  private Decimal? _GetProjectBudgetOriginalAmount(int projectID, string budgetType)
  {
    return PXResultset<PMBudget>.op_Implicit(PXSelectBase<PMBudget, PXViewOf<PMBudget>.BasedOn<SelectFromBase<PMBudget, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PMProject>.On<BqlOperand<PMBudget.projectID, IBqlInt>.IsEqual<PMProject.contractID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMProject.contractID, Equal<P.AsInt>>>>>.And<BqlOperand<PMBudget.type, IBqlString>.IsEqual<P.AsString>>>.Aggregate<To<GroupBy<PMBudget.projectID>, Sum<PMBudget.curyAmount>>>>.ReadOnly.Config>.Select((PXGraph) this, new object[2]
    {
      (object) projectID,
      (object) budgetType
    }))?.CuryAmount;
  }

  private Decimal? GetProjectOriginalCommitmentsAmount(int projectID)
  {
    if (!this.BulkMode)
      return this._GetProjectOriginalCommitmentsAmount(projectID);
    if (this._projectOriginalCommitmentsCache == null)
      this._projectOriginalCommitmentsCache = GraphHelper.RowCast<PMCommitment>((IEnumerable) PXSelectBase<PMCommitment, PXViewOf<PMCommitment>.BasedOn<SelectFromBase<PMCommitment, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<POLine>.On<BqlOperand<PMCommitment.commitmentID, IBqlGuid>.IsEqual<POLine.commitmentID>>>, FbqlJoins.Inner<POOrder>.On<POLine.FK.Order>>, FbqlJoins.Inner<PMProject>.On<POLine.FK.Project>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<POOrder.orderDate, LessEqual<BqlField<PMWipAdjustment.projectionDate, IBqlDateTime>.FromCurrent>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMProject.baseType, Equal<CTPRType.project>>>>>.And<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMProject.nonProject, Equal<False>>>>, And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMProject.defaultBranchID, Equal<BqlField<PMWipAdjustment.branchID, IBqlInt>.FromCurrent>>>>>.Or<BqlOperand<PMProject.defaultBranchID, IBqlInt>.IsNull>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMProject.curyID, Equal<BqlField<PMWipAdjustment.curyID, IBqlString>.FromCurrent>>>>>.And<BqlOperand<Current<PMWipAdjustment.projectStatus>, IBqlString>.Contains<PMProject.status>>>>>>.Aggregate<To<GroupBy<PMCommitment.projectID>, Sum<PMCommitment.origAmount>>>>.ReadOnly.Config>.Select((PXGraph) this, Array.Empty<object>())).ToDictionary<PMCommitment, int?, Decimal?>((Func<PMCommitment, int?>) (key => key.ProjectID), (Func<PMCommitment, Decimal?>) (value => value.OrigAmount));
    Decimal? nullable;
    return this._projectOriginalCommitmentsCache.TryGetValue(new int?(projectID), out nullable) ? nullable : new Decimal?();
  }

  private Decimal? _GetProjectOriginalCommitmentsAmount(int projectID)
  {
    return PXResultset<PMCommitment>.op_Implicit(PXSelectBase<PMCommitment, PXViewOf<PMCommitment>.BasedOn<SelectFromBase<PMCommitment, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<POLine>.On<BqlOperand<PMCommitment.commitmentID, IBqlGuid>.IsEqual<POLine.commitmentID>>>, FbqlJoins.Inner<POOrder>.On<POLine.FK.Order>>, FbqlJoins.Inner<PMProject>.On<POLine.FK.Project>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<POOrder.orderDate, LessEqual<BqlField<PMWipAdjustment.projectionDate, IBqlDateTime>.FromCurrent>>>>>.And<BqlOperand<PMProject.contractID, IBqlInt>.IsEqual<P.AsInt>>>.Aggregate<To<GroupBy<PMCommitment.projectID>, Sum<PMCommitment.origAmount>>>>.ReadOnly.Config>.Select((PXGraph) this, new object[1]
    {
      (object) projectID
    }))?.OrigAmount;
  }

  private Decimal? GetProjectApprovedCommitmentsAmount(int projectID)
  {
    if (!this.BulkMode)
      return this._GetProjectApprovedCommitmentsAmount(projectID);
    if (this._projectApprovedCommitmentsCache == null)
      this._projectApprovedCommitmentsCache = GraphHelper.RowCast<PMChangeOrder>((IEnumerable) PXSelectBase<PMChangeOrder, PXViewOf<PMChangeOrder>.BasedOn<SelectFromBase<PMChangeOrder, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PMProject>.On<BqlOperand<PMChangeOrder.projectID, IBqlInt>.IsEqual<PMProject.contractID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMChangeOrder.completionDate, LessEqual<BqlField<PMWipAdjustment.projectionDate, IBqlDateTime>.FromCurrent>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMChangeOrder.status, Equal<ChangeOrderStatus.closed>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMProject.baseType, Equal<CTPRType.project>>>>>.And<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMProject.nonProject, Equal<False>>>>, And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMProject.defaultBranchID, Equal<BqlField<PMWipAdjustment.branchID, IBqlInt>.FromCurrent>>>>>.Or<BqlOperand<PMProject.defaultBranchID, IBqlInt>.IsNull>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMProject.curyID, Equal<BqlField<PMWipAdjustment.curyID, IBqlString>.FromCurrent>>>>>.And<BqlOperand<Current<PMWipAdjustment.projectStatus>, IBqlString>.Contains<PMProject.status>>>>>>>.Aggregate<To<GroupBy<PMChangeOrder.projectID>, Sum<PMChangeOrder.commitmentTotal>>>>.ReadOnly.Config>.Select((PXGraph) this, Array.Empty<object>())).ToDictionary<PMChangeOrder, int?, Decimal?>((Func<PMChangeOrder, int?>) (key => key.ProjectID), (Func<PMChangeOrder, Decimal?>) (value => value.CommitmentTotal));
    Decimal? nullable;
    return this._projectApprovedCommitmentsCache.TryGetValue(new int?(projectID), out nullable) ? nullable : new Decimal?();
  }

  private Decimal? _GetProjectApprovedCommitmentsAmount(int projectID)
  {
    return PXResultset<PMChangeOrder>.op_Implicit(PXSelectBase<PMChangeOrder, PXViewOf<PMChangeOrder>.BasedOn<SelectFromBase<PMChangeOrder, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PMProject>.On<BqlOperand<PMChangeOrder.projectID, IBqlInt>.IsEqual<PMProject.contractID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMChangeOrder.completionDate, LessEqual<BqlField<PMWipAdjustment.projectionDate, IBqlDateTime>.FromCurrent>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMChangeOrder.status, Equal<ChangeOrderStatus.closed>>>>>.And<BqlOperand<PMProject.contractID, IBqlInt>.IsEqual<P.AsInt>>>>.Aggregate<To<GroupBy<PMChangeOrder.projectID>, Sum<PMChangeOrder.commitmentTotal>>>>.ReadOnly.Config>.Select((PXGraph) this, new object[1]
    {
      (object) projectID
    }))?.CommitmentTotal;
  }

  private Decimal? GetProjectPendingCommitmentsAmount(int projectID)
  {
    if (!this.BulkMode)
      return this._GetProjectPendingCommitmentsAmount(projectID);
    if (this._projectPendingCommitmentsCache == null)
      this._projectPendingCommitmentsCache = GraphHelper.RowCast<PMChangeOrder>((IEnumerable) PXSelectBase<PMChangeOrder, PXViewOf<PMChangeOrder>.BasedOn<SelectFromBase<PMChangeOrder, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PMProject>.On<BqlOperand<PMChangeOrder.projectID, IBqlInt>.IsEqual<PMProject.contractID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMChangeOrder.date, LessEqual<BqlField<PMWipAdjustment.projectionDate, IBqlDateTime>.FromCurrent>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMChangeOrder.status, In3<ChangeOrderStatus.open, ChangeOrderStatus.onHold, ChangeOrderStatus.pendingApproval>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMProject.baseType, Equal<CTPRType.project>>>>>.And<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMProject.nonProject, Equal<False>>>>, And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMProject.defaultBranchID, Equal<BqlField<PMWipAdjustment.branchID, IBqlInt>.FromCurrent>>>>>.Or<BqlOperand<PMProject.defaultBranchID, IBqlInt>.IsNull>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMProject.curyID, Equal<BqlField<PMWipAdjustment.curyID, IBqlString>.FromCurrent>>>>>.And<BqlOperand<Current<PMWipAdjustment.projectStatus>, IBqlString>.Contains<PMProject.status>>>>>>>.Aggregate<To<GroupBy<PMChangeOrder.projectID>, Sum<PMChangeOrder.commitmentTotal>>>>.ReadOnly.Config>.Select((PXGraph) this, Array.Empty<object>())).ToDictionary<PMChangeOrder, int?, Decimal?>((Func<PMChangeOrder, int?>) (key => key.ProjectID), (Func<PMChangeOrder, Decimal?>) (value => value.CommitmentTotal));
    Decimal? nullable;
    return this._projectPendingCommitmentsCache.TryGetValue(new int?(projectID), out nullable) ? nullable : new Decimal?();
  }

  private Decimal? _GetProjectPendingCommitmentsAmount(int projectID)
  {
    return PXResultset<PMChangeOrder>.op_Implicit(PXSelectBase<PMChangeOrder, PXViewOf<PMChangeOrder>.BasedOn<SelectFromBase<PMChangeOrder, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PMProject>.On<BqlOperand<PMChangeOrder.projectID, IBqlInt>.IsEqual<PMProject.contractID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMChangeOrder.date, LessEqual<BqlField<PMWipAdjustment.projectionDate, IBqlDateTime>.FromCurrent>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMChangeOrder.status, In3<ChangeOrderStatus.open, ChangeOrderStatus.onHold, ChangeOrderStatus.pendingApproval>>>>>.And<BqlOperand<PMProject.contractID, IBqlInt>.IsEqual<P.AsInt>>>>.Aggregate<To<GroupBy<PMChangeOrder.projectID>, Sum<PMChangeOrder.commitmentTotal>>>>.ReadOnly.Config>.Select((PXGraph) this, new object[1]
    {
      (object) projectID
    }))?.CommitmentTotal;
  }

  private Decimal? GetProjectActualCostsAmount(int projectID)
  {
    if (!this.BulkMode)
      return this._GetProjectActualCostsAmount(projectID);
    if (this._projectActualCostsCache == null)
      this._projectActualCostsCache = GraphHelper.RowCast<PMHistoryByDate>((IEnumerable) PXSelectBase<PMHistoryByDate, PXViewOf<PMHistoryByDate>.BasedOn<SelectFromBase<PMHistoryByDate, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PMAccountGroup>.On<BqlOperand<PMHistoryByDate.accountGroupID, IBqlInt>.IsEqual<PMAccountGroup.groupID>>>, FbqlJoins.Inner<PMProject>.On<BqlOperand<PMHistoryByDate.projectID, IBqlInt>.IsEqual<PMProject.contractID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMHistoryByDate.date, LessEqual<BqlField<PMWipAdjustment.projectionDate, IBqlDateTime>.FromCurrent>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMAccountGroup.type, Equal<AccountType.expense>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMProject.baseType, Equal<CTPRType.project>>>>>.And<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMProject.nonProject, Equal<False>>>>, And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMProject.defaultBranchID, Equal<BqlField<PMWipAdjustment.branchID, IBqlInt>.FromCurrent>>>>>.Or<BqlOperand<PMProject.defaultBranchID, IBqlInt>.IsNull>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMProject.curyID, Equal<BqlField<PMWipAdjustment.curyID, IBqlString>.FromCurrent>>>>>.And<BqlOperand<Current<PMWipAdjustment.projectStatus>, IBqlString>.Contains<PMProject.status>>>>>>>.Aggregate<To<GroupBy<PMHistoryByDate.projectID>, Sum<PMHistoryByDate.curyActualAmount>>>>.ReadOnly.Config>.Select((PXGraph) this, Array.Empty<object>())).ToDictionary<PMHistoryByDate, int?, Decimal?>((Func<PMHistoryByDate, int?>) (key => key.ProjectID), (Func<PMHistoryByDate, Decimal?>) (value => value.CuryActualAmount));
    Decimal? nullable;
    return this._projectActualCostsCache.TryGetValue(new int?(projectID), out nullable) ? nullable : new Decimal?();
  }

  private Decimal? _GetProjectActualCostsAmount(int projectID)
  {
    return PXResultset<PMHistoryByDate>.op_Implicit(PXSelectBase<PMHistoryByDate, PXViewOf<PMHistoryByDate>.BasedOn<SelectFromBase<PMHistoryByDate, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PMAccountGroup>.On<BqlOperand<PMHistoryByDate.accountGroupID, IBqlInt>.IsEqual<PMAccountGroup.groupID>>>, FbqlJoins.Inner<PMProject>.On<BqlOperand<PMHistoryByDate.projectID, IBqlInt>.IsEqual<PMProject.contractID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMHistoryByDate.date, LessEqual<BqlField<PMWipAdjustment.projectionDate, IBqlDateTime>.FromCurrent>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMAccountGroup.type, Equal<AccountType.expense>>>>>.And<BqlOperand<PMProject.contractID, IBqlInt>.IsEqual<P.AsInt>>>>.Aggregate<To<GroupBy<PMHistoryByDate.projectID>, Sum<PMHistoryByDate.curyActualAmount>>>>.ReadOnly.Config>.Select((PXGraph) this, new object[1]
    {
      (object) projectID
    }))?.CuryActualAmount;
  }

  private Decimal? GetProjectPeriodCostsAmount(int projectID)
  {
    if (!this.BulkMode)
      return this._GetProjectPeriodCostsAmount(projectID);
    if (this._projectPeriodCostsCache == null)
      this._projectPeriodCostsCache = GraphHelper.RowCast<PMTran>((IEnumerable) PXSelectBase<PMTran, PXViewOf<PMTran>.BasedOn<SelectFromBase<PMTran, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PMAccountGroup>.On<BqlOperand<PMTran.accountGroupID, IBqlInt>.IsEqual<PMAccountGroup.groupID>>>, FbqlJoins.Inner<PMProject>.On<BqlOperand<PMTran.projectID, IBqlInt>.IsEqual<PMProject.contractID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMTran.finPeriodID, Equal<BqlField<PMWipAdjustment.finPeriodID, IBqlString>.FromCurrent>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMAccountGroup.type, Equal<AccountType.expense>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMProject.baseType, Equal<CTPRType.project>>>>>.And<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMProject.nonProject, Equal<False>>>>, And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMProject.defaultBranchID, Equal<BqlField<PMWipAdjustment.branchID, IBqlInt>.FromCurrent>>>>>.Or<BqlOperand<PMProject.defaultBranchID, IBqlInt>.IsNull>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMProject.curyID, Equal<BqlField<PMWipAdjustment.curyID, IBqlString>.FromCurrent>>>>>.And<BqlOperand<Current<PMWipAdjustment.projectStatus>, IBqlString>.Contains<PMProject.status>>>>>>>.Aggregate<To<GroupBy<PMTran.projectID>, Sum<PMTran.projectCuryAmount>>>>.ReadOnly.Config>.Select((PXGraph) this, Array.Empty<object>())).ToDictionary<PMTran, int?, Decimal?>((Func<PMTran, int?>) (key => key.ProjectID), (Func<PMTran, Decimal?>) (value => value.ProjectCuryAmount));
    Decimal? nullable;
    return this._projectPeriodCostsCache.TryGetValue(new int?(projectID), out nullable) ? nullable : new Decimal?();
  }

  private Decimal? _GetProjectPeriodCostsAmount(int projectID)
  {
    return PXResultset<PMTran>.op_Implicit(PXSelectBase<PMTran, PXViewOf<PMTran>.BasedOn<SelectFromBase<PMTran, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PMAccountGroup>.On<BqlOperand<PMTran.accountGroupID, IBqlInt>.IsEqual<PMAccountGroup.groupID>>>, FbqlJoins.Inner<PMProject>.On<BqlOperand<PMTran.projectID, IBqlInt>.IsEqual<PMProject.contractID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMTran.finPeriodID, Equal<BqlField<PMWipAdjustment.finPeriodID, IBqlString>.FromCurrent>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMAccountGroup.type, Equal<AccountType.expense>>>>>.And<BqlOperand<PMProject.contractID, IBqlInt>.IsEqual<P.AsInt>>>>.Aggregate<To<GroupBy<PMTran.projectID>, Sum<PMTran.projectCuryAmount>>>>.ReadOnly.Config>.Select((PXGraph) this, new object[1]
    {
      (object) projectID
    }))?.ProjectCuryAmount;
  }

  private Decimal? GetProjectPeriodBillingsAmount(int projectID)
  {
    if (!this.BulkMode)
      return this._GetProjectPeriodBillingsAmount(projectID);
    if (this._projectPeriodBillingsCache == null)
      this._projectPeriodBillingsCache = GraphHelper.RowCast<PMTran>((IEnumerable) PXSelectBase<PMTran, PXViewOf<PMTran>.BasedOn<SelectFromBase<PMTran, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PMProject>.On<BqlOperand<PMTran.projectID, IBqlInt>.IsEqual<PMProject.contractID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMTran.finPeriodID, Equal<BqlField<PMWipAdjustment.finPeriodID, IBqlString>.FromCurrent>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMTran.tranType, Equal<BatchModule.moduleAR>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMProject.baseType, Equal<CTPRType.project>>>>>.And<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMProject.nonProject, Equal<False>>>>, And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMProject.defaultBranchID, Equal<BqlField<PMWipAdjustment.branchID, IBqlInt>.FromCurrent>>>>>.Or<BqlOperand<PMProject.defaultBranchID, IBqlInt>.IsNull>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMProject.curyID, Equal<BqlField<PMWipAdjustment.curyID, IBqlString>.FromCurrent>>>>>.And<BqlOperand<Current<PMWipAdjustment.projectStatus>, IBqlString>.Contains<PMProject.status>>>>>>>.Aggregate<To<GroupBy<PMTran.projectID>, Sum<PMTran.projectCuryAmount>>>>.ReadOnly.Config>.Select((PXGraph) this, Array.Empty<object>())).ToDictionary<PMTran, int?, Decimal?>((Func<PMTran, int?>) (key => key.ProjectID), (Func<PMTran, Decimal?>) (value => value.ProjectCuryAmount));
    Decimal? nullable;
    return this._projectPeriodBillingsCache.TryGetValue(new int?(projectID), out nullable) ? nullable : new Decimal?();
  }

  private Decimal? _GetProjectPeriodBillingsAmount(int projectID)
  {
    return PXResultset<PMTran>.op_Implicit(PXSelectBase<PMTran, PXViewOf<PMTran>.BasedOn<SelectFromBase<PMTran, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PMProject>.On<BqlOperand<PMTran.projectID, IBqlInt>.IsEqual<PMProject.contractID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMTran.finPeriodID, Equal<BqlField<PMWipAdjustment.finPeriodID, IBqlString>.FromCurrent>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMTran.tranType, Equal<BatchModule.moduleAR>>>>>.And<BqlOperand<PMProject.contractID, IBqlInt>.IsEqual<P.AsInt>>>>.Aggregate<To<GroupBy<PMTran.projectID>, Sum<PMTran.projectCuryAmount>>>>.ReadOnly.Config>.Select((PXGraph) this, new object[1]
    {
      (object) projectID
    }))?.ProjectCuryAmount;
  }

  private Decimal? GetProjectBillingsToPeriodAmount(int projectID)
  {
    if (!this.BulkMode)
      return this._GetProjectBillingsToPeriodAmount(projectID);
    if (this._projectBillingsToPeriodCache == null)
      this._projectBillingsToPeriodCache = GraphHelper.RowCast<PMTran>((IEnumerable) PXSelectBase<PMTran, PXViewOf<PMTran>.BasedOn<SelectFromBase<PMTran, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PMProject>.On<BqlOperand<PMTran.projectID, IBqlInt>.IsEqual<PMProject.contractID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMTran.date, LessEqual<BqlField<PMWipAdjustment.projectionDate, IBqlDateTime>.FromCurrent>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMTran.tranType, Equal<BatchModule.moduleAR>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMProject.baseType, Equal<CTPRType.project>>>>>.And<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMProject.nonProject, Equal<False>>>>, And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMProject.defaultBranchID, Equal<BqlField<PMWipAdjustment.branchID, IBqlInt>.FromCurrent>>>>>.Or<BqlOperand<PMProject.defaultBranchID, IBqlInt>.IsNull>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMProject.curyID, Equal<BqlField<PMWipAdjustment.curyID, IBqlString>.FromCurrent>>>>>.And<BqlOperand<Current<PMWipAdjustment.projectStatus>, IBqlString>.Contains<PMProject.status>>>>>>>.Aggregate<To<GroupBy<PMTran.projectID>, Sum<PMTran.projectCuryAmount>>>>.ReadOnly.Config>.Select((PXGraph) this, Array.Empty<object>())).ToDictionary<PMTran, int?, Decimal?>((Func<PMTran, int?>) (key => key.ProjectID), (Func<PMTran, Decimal?>) (value => value.ProjectCuryAmount));
    Decimal? nullable;
    return this._projectBillingsToPeriodCache.TryGetValue(new int?(projectID), out nullable) ? nullable : new Decimal?();
  }

  private Decimal? _GetProjectBillingsToPeriodAmount(int projectID)
  {
    return PXResultset<PMTran>.op_Implicit(PXSelectBase<PMTran, PXViewOf<PMTran>.BasedOn<SelectFromBase<PMTran, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PMProject>.On<BqlOperand<PMTran.projectID, IBqlInt>.IsEqual<PMProject.contractID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMTran.date, LessEqual<BqlField<PMWipAdjustment.projectionDate, IBqlDateTime>.FromCurrent>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMTran.tranType, Equal<BatchModule.moduleAR>>>>>.And<BqlOperand<PMProject.contractID, IBqlInt>.IsEqual<P.AsInt>>>>.Aggregate<To<GroupBy<PMTran.projectID>, Sum<PMTran.projectCuryAmount>>>>.ReadOnly.Config>.Select((PXGraph) this, new object[1]
    {
      (object) projectID
    }))?.ProjectCuryAmount;
  }

  private Decimal? GetProjectBudgetPendingAmount(int projectID, string budgetType)
  {
    if (!this.BulkMode)
      return this._GetProjectBudgetPendingAmount(projectID, budgetType);
    if (this._projectBudgetPendingAmountCache == null)
      this._projectBudgetPendingAmountCache = GraphHelper.RowCast<PMChangeOrderBudget>((IEnumerable) PXSelectBase<PMChangeOrderBudget, PXViewOf<PMChangeOrderBudget>.BasedOn<SelectFromBase<PMChangeOrderBudget, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PMChangeOrder>.On<BqlOperand<PMChangeOrderBudget.refNbr, IBqlString>.IsEqual<PMChangeOrder.refNbr>>>, FbqlJoins.Inner<PMProject>.On<PMChangeOrder.FK.Project>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMChangeOrder.date, LessEqual<BqlField<PMWipAdjustment.projectionDate, IBqlDateTime>.FromCurrent>>>>, And<BqlOperand<PMChangeOrder.released, IBqlBool>.IsEqual<False>>>, And<BqlOperand<PMChangeOrder.status, IBqlString>.IsIn<ChangeOrderStatus.open, ChangeOrderStatus.onHold, ChangeOrderStatus.pendingApproval>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMProject.baseType, Equal<CTPRType.project>>>>>.And<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMProject.nonProject, Equal<False>>>>, And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMProject.defaultBranchID, Equal<BqlField<PMWipAdjustment.branchID, IBqlInt>.FromCurrent>>>>>.Or<BqlOperand<PMProject.defaultBranchID, IBqlInt>.IsNull>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMProject.curyID, Equal<BqlField<PMWipAdjustment.curyID, IBqlString>.FromCurrent>>>>>.And<BqlOperand<Current<PMWipAdjustment.projectStatus>, IBqlString>.Contains<PMProject.status>>>>>>.Aggregate<To<GroupBy<PMChangeOrderBudget.projectID>, GroupBy<PMChangeOrderBudget.type>, Sum<PMChangeOrderBudget.amount>>>>.ReadOnly.Config>.Select((PXGraph) this, Array.Empty<object>())).ToDictionary<PMChangeOrderBudget, ProjectWipAdjustmentEntry.ProjectBudgetKey, Decimal?>((Func<PMChangeOrderBudget, ProjectWipAdjustmentEntry.ProjectBudgetKey>) (key => new ProjectWipAdjustmentEntry.ProjectBudgetKey(key.ProjectID.Value, key.Type)), (Func<PMChangeOrderBudget, Decimal?>) (value => value.Amount));
    Decimal? nullable;
    return this._projectBudgetPendingAmountCache.TryGetValue(new ProjectWipAdjustmentEntry.ProjectBudgetKey(projectID, budgetType), out nullable) ? nullable : new Decimal?();
  }

  private Decimal? _GetProjectBudgetPendingAmount(int projectID, string budgetType)
  {
    return PXResultset<PMChangeOrderBudget>.op_Implicit(PXSelectBase<PMChangeOrderBudget, PXViewOf<PMChangeOrderBudget>.BasedOn<SelectFromBase<PMChangeOrderBudget, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PMChangeOrder>.On<BqlOperand<PMChangeOrderBudget.refNbr, IBqlString>.IsEqual<PMChangeOrder.refNbr>>>, FbqlJoins.Inner<PMProject>.On<PMChangeOrder.FK.Project>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMChangeOrder.date, LessEqual<BqlField<PMWipAdjustment.projectionDate, IBqlDateTime>.FromCurrent>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMChangeOrder.released, Equal<False>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMChangeOrder.status, In3<ChangeOrderStatus.open, ChangeOrderStatus.onHold, ChangeOrderStatus.pendingApproval>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMChangeOrderBudget.type, Equal<P.AsString>>>>>.And<BqlOperand<PMProject.contractID, IBqlInt>.IsEqual<P.AsInt>>>>>>.Aggregate<To<GroupBy<PMChangeOrderBudget.projectID>, Sum<PMChangeOrderBudget.amount>>>>.ReadOnly.Config>.Select((PXGraph) this, new object[2]
    {
      (object) budgetType,
      (object) projectID
    }))?.Amount;
  }

  private Decimal? GetProjectBudgetChangedAmount(int projectID, string budgetType)
  {
    if (!this.BulkMode)
      return this._GetProjectBudgetChangedAmount(projectID, budgetType);
    if (this._projectBudgetChangedAmountCache == null)
      this._projectBudgetChangedAmountCache = GraphHelper.RowCast<PMChangeOrderBudget>((IEnumerable) PXSelectBase<PMChangeOrderBudget, PXViewOf<PMChangeOrderBudget>.BasedOn<SelectFromBase<PMChangeOrderBudget, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PMChangeOrder>.On<BqlOperand<PMChangeOrderBudget.refNbr, IBqlString>.IsEqual<PMChangeOrder.refNbr>>>, FbqlJoins.Inner<PMProject>.On<PMChangeOrder.FK.Project>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMChangeOrder.completionDate, LessEqual<BqlField<PMWipAdjustment.projectionDate, IBqlDateTime>.FromCurrent>>>>, And<BqlOperand<PMChangeOrder.released, IBqlBool>.IsEqual<True>>>, And<BqlOperand<PMChangeOrder.status, IBqlString>.IsEqual<ChangeOrderStatus.closed>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMProject.baseType, Equal<CTPRType.project>>>>>.And<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMProject.nonProject, Equal<False>>>>, And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMProject.defaultBranchID, Equal<BqlField<PMWipAdjustment.branchID, IBqlInt>.FromCurrent>>>>>.Or<BqlOperand<PMProject.defaultBranchID, IBqlInt>.IsNull>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMProject.curyID, Equal<BqlField<PMWipAdjustment.curyID, IBqlString>.FromCurrent>>>>>.And<BqlOperand<Current<PMWipAdjustment.projectStatus>, IBqlString>.Contains<PMProject.status>>>>>>.Aggregate<To<GroupBy<PMChangeOrderBudget.projectID>, GroupBy<PMChangeOrderBudget.type>, Sum<PMChangeOrderBudget.amount>>>>.ReadOnly.Config>.Select((PXGraph) this, Array.Empty<object>())).ToDictionary<PMChangeOrderBudget, ProjectWipAdjustmentEntry.ProjectBudgetKey, Decimal?>((Func<PMChangeOrderBudget, ProjectWipAdjustmentEntry.ProjectBudgetKey>) (key => new ProjectWipAdjustmentEntry.ProjectBudgetKey(key.ProjectID.Value, key.Type)), (Func<PMChangeOrderBudget, Decimal?>) (value => value.Amount));
    Decimal? nullable;
    return this._projectBudgetChangedAmountCache.TryGetValue(new ProjectWipAdjustmentEntry.ProjectBudgetKey(projectID, budgetType), out nullable) ? nullable : new Decimal?();
  }

  private Decimal? _GetProjectBudgetChangedAmount(int projectID, string budgetType)
  {
    return PXResultset<PMChangeOrderBudget>.op_Implicit(PXSelectBase<PMChangeOrderBudget, PXViewOf<PMChangeOrderBudget>.BasedOn<SelectFromBase<PMChangeOrderBudget, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PMChangeOrder>.On<BqlOperand<PMChangeOrderBudget.refNbr, IBqlString>.IsEqual<PMChangeOrder.refNbr>>>, FbqlJoins.Inner<PMProject>.On<PMChangeOrder.FK.Project>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMChangeOrder.completionDate, LessEqual<BqlField<PMWipAdjustment.projectionDate, IBqlDateTime>.FromCurrent>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMChangeOrder.released, Equal<True>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMChangeOrder.status, Equal<ChangeOrderStatus.closed>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMChangeOrderBudget.type, Equal<P.AsString>>>>>.And<BqlOperand<PMProject.contractID, IBqlInt>.IsEqual<P.AsInt>>>>>>.Aggregate<To<GroupBy<PMChangeOrderBudget.projectID>, Sum<PMChangeOrderBudget.amount>>>>.ReadOnly.Config>.Select((PXGraph) this, new object[2]
    {
      (object) budgetType,
      (object) projectID
    }))?.Amount;
  }

  public class MultiCurrency : MultiCurrencyGraph<ProjectWipAdjustmentEntry, PMWipAdjustment>
  {
    public static bool IsActive() => true;

    protected override string Module => "PM";

    protected override bool AllowOverrideCury() => this.Base.DocumentIsOnHold;

    protected override bool AllowOverrideRate(PXCache sender, PX.Objects.CM.Extensions.CurrencyInfo info, CurySource source)
    {
      return this.Base.DocumentIsOnHold;
    }

    protected override PXSelectBase[] GetChildren()
    {
      return new PXSelectBase[1]
      {
        (PXSelectBase) this.Base.Items
      };
    }

    protected override PXSelectBase[] GetTrackedExceptChildren()
    {
      return new PXSelectBase[1]
      {
        (PXSelectBase) this.Base.Document
      };
    }

    protected override MultiCurrencyGraph<ProjectWipAdjustmentEntry, PMWipAdjustment>.CurySourceMapping GetCurySourceMapping()
    {
      return new MultiCurrencyGraph<ProjectWipAdjustmentEntry, PMWipAdjustment>.CurySourceMapping(typeof (PX.Objects.GL.Company))
      {
        CuryID = typeof (PX.Objects.GL.Company.baseCuryID)
      };
    }

    protected override MultiCurrencyGraph<ProjectWipAdjustmentEntry, PMWipAdjustment>.DocumentMapping GetDocumentMapping()
    {
      return new MultiCurrencyGraph<ProjectWipAdjustmentEntry, PMWipAdjustment>.DocumentMapping(typeof (PMWipAdjustment))
      {
        BranchID = typeof (PMWipAdjustment.branchID),
        CuryInfoID = typeof (PMWipAdjustment.curyInfoID),
        CuryID = typeof (PMWipAdjustment.curyID),
        DocumentDate = typeof (PMWipAdjustment.projectionDate)
      };
    }

    protected override void _(PX.Data.Events.FieldVerifying<PX.Objects.Extensions.MultiCurrency.Document, PX.Objects.Extensions.MultiCurrency.Document.curyID> e)
    {
      this.Base.AskAboutFieldUpdatingIfLinesExist("Change Calculation Parameters", "If you change any of the calculation parameters, all WIP adjustment lines will be cleared. Would you like to proceed with changing the parameters?", (System.Action) (() =>
      {
        ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.Extensions.MultiCurrency.Document, PX.Objects.Extensions.MultiCurrency.Document.curyID>, PX.Objects.Extensions.MultiCurrency.Document, object>) e).NewValue = e.OldValue;
        ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.Extensions.MultiCurrency.Document, PX.Objects.Extensions.MultiCurrency.Document.curyID>>) e).Cancel = true;
      }));
      if (((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PX.Objects.Extensions.MultiCurrency.Document, PX.Objects.Extensions.MultiCurrency.Document.curyID>>) e).Cancel)
        return;
      base._(e);
    }

    protected virtual void _(PX.Data.Events.FieldUpdated<PX.Objects.Extensions.MultiCurrency.Document, PX.Objects.Extensions.MultiCurrency.Document.curyID> e)
    {
      this.Base.ClearAllLines();
    }
  }

  private class AdjustmentTransaction
  {
    public int ProjectID;
    public int ProjectTaskID;
    public Decimal DebitAmount;
    public Decimal CreditAmount;
    public int AccountID;
    public int SubID;

    public AdjustmentTransaction()
    {
    }

    private AdjustmentTransaction(
      Decimal amount,
      int? accountId,
      int? subId,
      int? projectID,
      int? taskId)
    {
      this.DebitAmount = amount >= 0M ? amount : 0M;
      this.CreditAmount = amount < 0M ? -amount : 0M;
      this.AccountID = accountId.GetValueOrDefault();
      this.SubID = subId.GetValueOrDefault();
      this.ProjectID = projectID ?? ProjectDefaultAttribute.NonProject().GetValueOrDefault();
      this.ProjectTaskID = taskId.GetValueOrDefault();
    }

    public void Normalize()
    {
      if (this.DebitAmount >= this.CreditAmount)
      {
        this.DebitAmount -= this.CreditAmount;
        this.CreditAmount = 0M;
      }
      else
      {
        this.CreditAmount -= this.DebitAmount;
        this.DebitAmount = 0M;
      }
    }

    public static ProjectWipAdjustmentEntry.AdjustmentTransaction CreateTransaction(
      Decimal amount,
      int? accountId,
      int? subId,
      int? projectID = null,
      int? taskId = null)
    {
      return new ProjectWipAdjustmentEntry.AdjustmentTransaction(amount, accountId, subId, projectID, taskId);
    }
  }

  private record ProjectBudgetKey
  {
    public int ProjectID { get; init; }

    public string BudgetType { get; init; }

    internal ProjectBudgetKey(int projectID, string budgetType)
    {
      this.ProjectID = projectID;
      this.BudgetType = budgetType;
    }
  }
}
