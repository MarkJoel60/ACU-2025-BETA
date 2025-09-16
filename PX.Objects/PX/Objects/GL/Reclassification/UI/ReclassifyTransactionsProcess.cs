// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.Reclassification.UI.ReclassifyTransactionsProcess
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Api;
using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.Common;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.GL.Attributes;
using PX.Objects.GL.FinPeriods;
using PX.Objects.GL.FinPeriods.TableDefinition;
using PX.Objects.GL.GraphBaseExtensions;
using PX.Objects.GL.Reclassification.Common;
using PX.Objects.PM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

#nullable enable
namespace PX.Objects.GL.Reclassification.UI;

public class ReclassifyTransactionsProcess : 
  ReclassifyTransactionsBase<
  #nullable disable
  ReclassifyTransactionsProcess>
{
  public PXCancel<GLTranForReclassification> Cancel;
  public PXDelete<GLTranForReclassification> Delete;
  public PXAction<GLTranForReclassification> showLoadTransPopup;
  public PXAction<GLTranForReclassification> reloadTrans;
  public PXAction<GLTranForReclassification> loadTrans;
  public PXAction<GLTranForReclassification> replace;
  public PXAction<GLTranForReclassification> split;
  public PXAction<GLTranForReclassification> validateAndProcess;
  [PXFilterable(new System.Type[] {})]
  public PXProcessing<GLTranForReclassification, Where<True, Equal<True>>, OrderBy<Asc<GLTranForReclassification.sortOrder>>> GLTranForReclass;
  public PXSelectJoin<GLTranForReclassification, InnerJoin<PX.Objects.CM.CurrencyInfo, On<GLTran.curyInfoID, Equal<PX.Objects.CM.CurrencyInfo.curyInfoID>>>, Where<GLTran.module, Equal<Required<GLTran.module>>, And<GLTran.batchNbr, Equal<Required<GLTran.batchNbr>>, And<GLTran.lineNbr, Equal<Required<GLTran.lineNbr>>, And<GLTran.reclassBatchNbr, IsNull>>>>> GLTranForReclassWithCuryInfo;
  public PXSelect<GLTranForReclassification, Where<GLTran.module, Equal<Required<GLTran.module>>, And<GLTran.batchNbr, Equal<Required<GLTran.batchNbr>>, And<GLTran.lineNbr, Equal<Required<GLTran.lineNbr>>>>>> GLTranForReclass_Module_BatchNbr_LineNbr;
  public PXSelect<GLTranForReclassification, Where<GLTran.module, Equal<Required<GLTran.module>>, And<GLTran.batchNbr, Equal<Required<GLTran.batchNbr>>, And<GLTran.isReclassReverse, Equal<False>, And<GLTran.isInterCompany, Equal<False>>>>>> GLTransForReclassForReverseView;
  public PXSelect<GLTran, Where<GLTran.module, Equal<Required<GLTran.module>>, And<GLTran.batchNbr, Equal<Required<GLTran.batchNbr>>, And<GLTran.isReclassReverse, Equal<True>>>>> ReclassReverseGLTransView;
  public PXFilter<ReclassifyTransactionsProcess.LoadOptions> LoadOptionsView;
  public PXFilter<ReclassifyTransactionsProcess.ReplaceOptions> ReplaceOptionsView;
  public PXSelect<BAccountR> BAccountRView;
  public PXSelect<PX.Objects.GL.Batch> Batch;
  public PXSelect<PX.Objects.CM.CurrencyInfo> currencyInfo;
  private const string ScheduleActionKey = "Schedule";
  private static string[] _emptyStringReplaceWildCards = new string[2]
  {
    "\"\"",
    "''"
  };
  protected static System.Type[] EditableFields = new System.Type[8]
  {
    typeof (GLTranForReclassification.newBranchID),
    typeof (GLTranForReclassification.newAccountID),
    typeof (GLTranForReclassification.newSubID),
    typeof (GLTranForReclassification.newTranDate),
    typeof (GLTranForReclassification.newProjectID),
    typeof (GLTranForReclassification.newTaskID),
    typeof (GLTranForReclassification.newCostCodeID),
    typeof (GLTranForReclassification.curyNewAmt)
  };
  public PXAction<GLTranForReclassification> ViewReclassBatch;
  public PXAction<GLTranForReclassification> viewDocument;

  [InjectDependency]
  public IFinPeriodRepository FinPeriodRepository { get; set; }

  protected IEnumerable<GLTranForReclassification> GetUpdatedTranForReclass()
  {
    return ((PXSelectBase) this.GLTranForReclass).Cache.Updated.OfType<GLTranForReclassification>();
  }

  public ReclassifyTransactionsProcess()
  {
    ((PXGraph) this).Actions["Schedule"].SetVisible(false);
    ((PXProcessingBase<GLTranForReclassification>) this.GLTranForReclass).SetSelected<GLTran.selected>();
    ((PXProcessing<GLTranForReclassification>) this.GLTranForReclass).SetProcessCaption("Process");
    ((PXProcessing<GLTranForReclassification>) this.GLTranForReclass).SetProcessVisible(false);
    ((PXProcessing<GLTranForReclassification>) this.GLTranForReclass).SetProcessAllVisible(false);
    PXUIFieldAttribute.SetVisible<GLTran.refNbr>(((PXSelectBase) this.GLTranForReclass).Cache, (object) null, false);
    PXUIFieldAttribute.SetVisible<GLTran.selected>(((PXSelectBase) this.GLTranForReclass).Cache, (object) null, true);
    bool flag = PXAccess.FeatureInstalled<FeaturesSet.projectAccounting>();
    PXUIFieldAttribute.SetVisibility<GLTran.projectID>(((PXSelectBase) this.GLTranForReclass).Cache, (object) null, flag ? (PXUIVisibility) 3 : (PXUIVisibility) 1);
    PXUIFieldAttribute.SetVisible<GLTran.projectID>(((PXSelectBase) this.GLTranForReclass).Cache, (object) null, flag);
    PXUIFieldAttribute.SetVisibility<PX.Objects.CR.BAccount.acctReferenceNbr>(((PXSelectBase) this.BAccountRView).Cache, (object) null, (PXUIVisibility) 3);
    PXUIFieldAttribute.SetVisibility<BAccountR.parentBAccountID>(((PXSelectBase) this.BAccountRView).Cache, (object) null, (PXUIVisibility) 3);
    PXUIFieldAttribute.SetVisibility<PX.Objects.CR.BAccount.ownerID>(((PXSelectBase) this.BAccountRView).Cache, (object) null, (PXUIVisibility) 3);
    ReclassifyTransactionsProcess.LoadOptions current = ((PXSelectBase<ReclassifyTransactionsProcess.LoadOptions>) this.LoadOptionsView).Current;
    // ISSUE: method pointer
    ((PXAction) this.showLoadTransPopup).StateSelectingEvents += new PXFieldSelecting((object) this, __methodptr(LoadOptionsButtonFieldSelectingHandler));
    // ISSUE: method pointer
    ((PXAction) this.validateAndProcess).StateSelectingEvents += new PXFieldSelecting((object) this, __methodptr(ProcessButtonFieldSelectingHandler));
    // ISSUE: method pointer
    ((PXAction) this.loadTrans).StateSelectingEvents += new PXFieldSelecting((object) this, __methodptr(ButtonsFieldSelectingHandlerForDisableAfterProcess));
    // ISSUE: method pointer
    ((PXAction) this.replace).StateSelectingEvents += new PXFieldSelecting((object) this, __methodptr(DependingOnRowExistanceButtonsSelectingHandler));
    // ISSUE: method pointer
    ((PXAction) this.split).StateSelectingEvents += new PXFieldSelecting((object) this, __methodptr(DependingOnRowExistanceButtonsSelectingHandler));
    // ISSUE: method pointer
    ((PXAction) this.Delete).StateSelectingEvents += new PXFieldSelecting((object) this, __methodptr(DependingOnRowExistanceButtonsSelectingHandler));
    // ISSUE: method pointer
    ((PXAction) this.reloadTrans).StateSelectingEvents += new PXFieldSelecting((object) this, __methodptr(ReloadTransButtonStateSelectingHandler));
  }

  protected virtual IEnumerable glTranForReclass()
  {
    int num = 0;
    IEnumerable<GLTranForReclassification> updatedTranForReclass = this.GetUpdatedTranForReclass();
    foreach (GLTranForReclassification tran in updatedTranForReclass)
    {
      if (!tran.IsSplitting)
      {
        tran.SortOrder = new int?(num++);
        GLTranKey key1 = new GLTranKey((GLTran) tran);
        if (this.State.SplittingGroups.Keys.Contains<GLTranKey>(key1))
        {
          foreach (GLTranKey key2 in this.State.SplittingGroups[key1])
            ((PXSelectBase<GLTranForReclassification>) this.GLTranForReclass_Module_BatchNbr_LineNbr).Locate(this.GetGLTranForReclassByKey(key2)).SortOrder = new int?(num++);
        }
      }
    }
    return (IEnumerable) updatedTranForReclass;
  }

  protected virtual void GLTranForReclassification_ReclassBatchNbr_FieldSelecting(
    PXCache sender,
    PXFieldSelectingEventArgs e)
  {
    TimeSpan timeSpan;
    Exception exception;
    PXUIFieldAttribute.SetVisible<GLTran.reclassBatchNbr>(((PXSelectBase) this.GLTranForReclass).Cache, (object) null, PXLongOperation.GetStatus(((PXGraph) this).UID, ref timeSpan, ref exception) == 2 || this.State.ReclassScreenMode == ReclassScreenMode.Editing);
    GLTranForReclassification row = (GLTranForReclassification) e.Row;
    if ((row != null ? (!row.IsSplitting ? 1 : 0) : 0) != 0)
      return;
    e.ReturnValue = (object) "";
  }

  protected virtual void ReclassGraphState_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: method pointer
    ((PXProcessingBase<GLTranForReclassification>) this.GLTranForReclass).SetProcessDelegate(new PXProcessingBase<GLTranForReclassification>.ProcessListDelegate((object) new ReclassifyTransactionsProcess.\u003C\u003Ec__DisplayClass30_0()
    {
      state = PXCache<ReclassGraphState>.CreateCopy(this.State)
    }, __methodptr(\u003CReclassGraphState_RowSelected\u003Eb__0)));
  }

  protected virtual void GLTranForReclassification_RowSelected(
    PXCache cache,
    PXRowSelectedEventArgs e)
  {
    if (!(e.Row is GLTranForReclassification row))
      return;
    bool screenIsEditable = !PXLongOperation.Exists(((PXGraph) this).UID);
    bool flag1 = this.IsTranDateAndDescEnabled(row);
    PXUIFieldAttribute.SetEnabled<GLTran.selected>(cache, (object) row, this.GetEnabledForSelectedCheckbox(row, screenIsEditable));
    PXUIFieldAttribute.SetEnabled<GLTranForReclassification.newBranchID>(cache, (object) row, screenIsEditable);
    PXUIFieldAttribute.SetEnabled<GLTranForReclassification.newAccountID>(cache, (object) row, screenIsEditable);
    PXUIFieldAttribute.SetEnabled<GLTranForReclassification.newSubID>(cache, (object) row, screenIsEditable);
    PXUIFieldAttribute.SetEnabled<GLTranForReclassification.newFinPeriodID>(cache, (object) row, screenIsEditable);
    bool isProjectOrNonProject = ProjectDefaultAttribute.IsProjectOrNonProject((PXGraph) this, row.ProjectID);
    PXUIFieldAttribute.SetEnabled<GLTranForReclassification.newProjectID>(cache, (object) row, screenIsEditable & isProjectOrNonProject);
    PXUIFieldAttribute.SetEnabled<GLTranForReclassification.newTaskID>(cache, (object) row, screenIsEditable);
    PXUIFieldAttribute.SetEnabled<GLTranForReclassification.newCostCodeID>(cache, (object) row, screenIsEditable);
    PXUIFieldAttribute.SetEnabled<GLTranForReclassification.newTranDate>(cache, (object) row, flag1);
    PXUIFieldAttribute.SetEnabled<GLTranForReclassification.newTranDesc>(cache, (object) row, flag1);
    PXUIFieldAttribute.SetEnabled<GLTranForReclassification.curyNewAmt>(cache, (object) row, row.IsSplitting);
    PXCacheEx.Adjust<ActiveProjectAttribute>(((PXSelectBase) this.GLTranForReclass).Cache, (object) row).For<GLTranForReclassification.newProjectID>((Action<ActiveProjectAttribute>) (selector => selector.ValidateValue = isProjectOrNonProject));
    this.ValidateNewFields(row);
    this.IsDateWithinPeriod(cache, row);
    if (!row.NewProjectID.HasValue && !isProjectOrNonProject)
      row.NewProjectID = row.ProjectID;
    bool flag2 = this.State.SplittingGroups.Any<KeyValuePair<GLTranKey, List<GLTranKey>>>();
    PXUIFieldAttribute.SetVisible<GLTranForReclassification.curyNewAmt>(cache, (object) null, flag2);
    PXUIFieldAttribute.SetVisible<GLTranForReclassification.splittedIcon>(cache, (object) null, flag2);
  }

  protected virtual void GLTranForReclassification_Selected_FieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
    GLTranForReclassification row = (GLTranForReclassification) e.Row;
    bool? selected;
    if (e.ExternalCall)
    {
      selected = row.Selected;
      bool flag = false;
      if (selected.GetValueOrDefault() == flag & selected.HasValue)
      {
        this.InitTranForReclassEditableFields(row);
        this.CleanupValidationMessages(row);
      }
    }
    if (row.ReclassRowType != ReclassRowTypes.Editing)
      return;
    selected = row.Selected;
    if (selected.GetValueOrDefault())
      this.State.GLTranForReclassToDelete.Remove(row);
    else
      this.State.GLTranForReclassToDelete.Add(row);
  }

  protected virtual void GLTranForReclassification_NewBranchID_FieldVerifying(
    PXCache cache,
    PXFieldVerifyingEventArgs e)
  {
    GLTranForReclassification row = (GLTranForReclassification) e.Row;
    if (!row.NewBranchID.HasValue)
      return;
    this.GetNewFinPeriod(row).RaiseIfHasError();
  }

  protected virtual void GLTranForReclassification_NewBranchID_FieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
    GLTranForReclassification row = (GLTranForReclassification) e.Row;
    row.NewFinPeriodID = this.GetNewFinPeriod(row).GetValueOrRaiseError().FinPeriodID;
  }

  protected virtual ProcessingResult<FinPeriod> GetNewFinPeriod(
    GLTranForReclassification tranForReclass)
  {
    return this.FinPeriodRepository.GetFinPeriodByMasterPeriodID(PXAccess.GetParentOrganizationID(tranForReclass.NewBranchID), tranForReclass.TranPeriodID);
  }

  protected virtual void GLTranForReclassification_NewSubID_FieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
    GLTranForReclassification row = (GLTranForReclassification) e.Row;
    PXFieldState stateExt = (PXFieldState) ((PXSelectBase) this.GLTranForReclass).Cache.GetStateExt<GLTranForReclassification.newSubID>((object) row);
    row.NewSubCD = (string) stateExt.Value;
  }

  protected virtual void GLTranForReclassification_NewBranchID_FieldUpdating(
    PXCache cache,
    PXFieldUpdatingEventArgs e)
  {
    ((PXSelectBase) this.GLTranForReclass).Cache.RaiseExceptionHandling<GLTranForReclassification.newBranchID>((object) (GLTranForReclassification) e.Row, e.NewValue, (Exception) null);
  }

  protected virtual void GLTranForReclassification_NewAccountID_FieldVerifying(
    PXCache cache,
    PXFieldVerifyingEventArgs e)
  {
    if (e.Row == null || e.NewValue == null)
      return;
    PX.Objects.GL.Account account = (PX.Objects.GL.Account) PXSelectorAttribute.Select<GLTranForReclassification.newAccountID>(cache, e.Row, e.NewValue);
    AccountAttribute.VerifyAccountIsNotControl<GLTranForReclassification.newAccountID>(cache, (EventArgs) e, account, true);
    ReclassifyTransactionsProcess.VerifyAccountGroup(cache, e, account);
  }

  private static void VerifyAccountGroup(
    PXCache cache,
    PXFieldVerifyingEventArgs e,
    PX.Objects.GL.Account account)
  {
    GLTranForReclassification row = (GLTranForReclassification) e.Row;
    if (!ProjectDefaultAttribute.IsProject(cache.Graph, row.NewProjectID))
      return;
    ActiveProjectAttribute.VerifyAccountIsInAccountGroup<GLTranForReclassification.newAccountID>(cache, (EventArgs) e, account, true);
  }

  protected virtual void GLTranForReclassification_NewAccountID_FieldUpdating(
    PXCache cache,
    PXFieldUpdatingEventArgs e)
  {
    ((PXSelectBase) this.GLTranForReclass).Cache.RaiseExceptionHandling<GLTranForReclassification.newAccountID>((object) (GLTranForReclassification) e.Row, e.NewValue, (Exception) null);
  }

  protected virtual void GLTranForReclassification_NewSubID_FieldUpdating(
    PXCache cache,
    PXFieldUpdatingEventArgs e)
  {
    ((PXSelectBase) this.GLTranForReclass).Cache.RaiseExceptionHandling<GLTranForReclassification.newSubID>((object) (GLTranForReclassification) e.Row, e.NewValue, (Exception) null);
  }

  protected virtual void GLTranForReclassification_NewProjectID_FieldUpdating(
    PXCache cache,
    PXFieldUpdatingEventArgs e)
  {
    ((PXSelectBase) this.GLTranForReclass).Cache.RaiseExceptionHandling<GLTranForReclassification.newProjectID>(e.Row, e.NewValue, (Exception) null);
  }

  protected virtual void GLTranForReclassification_NewTaskID_FieldUpdating(
    PXCache cache,
    PXFieldUpdatingEventArgs e)
  {
    ((PXSelectBase) this.GLTranForReclass).Cache.RaiseExceptionHandling<GLTranForReclassification.newTaskID>(e.Row, e.NewValue, (Exception) null);
  }

  protected virtual void GLTranForReclassification_NewCostCodeID_FieldUpdating(
    PXCache cache,
    PXFieldUpdatingEventArgs e)
  {
    ((PXSelectBase) this.GLTranForReclass).Cache.RaiseExceptionHandling<GLTranForReclassification.newCostCodeID>(e.Row, e.NewValue, (Exception) null);
  }

  protected virtual void GLTranForReclassification_NewTranDate_FieldUpdating(
    PXCache cache,
    PXFieldUpdatingEventArgs e)
  {
    ((PXSelectBase) this.GLTranForReclass).Cache.RaiseExceptionHandling<GLTranForReclassification.newTranDate>((object) (GLTranForReclassification) e.Row, e.NewValue, (Exception) null);
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<GLTranForReclassification, GLTranForReclassification.newFinPeriodID> e)
  {
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<GLTranForReclassification, GLTranForReclassification.newFinPeriodID>>) e).Cancel = true;
  }

  protected virtual void GLTranForReclassification_NewFinPeriodID_ExceptionHandling(
    PXCache cache,
    PXExceptionHandlingEventArgs e)
  {
    GLTranForReclassification row = (GLTranForReclassification) e.Row;
    if (this.FieldHasError(row, typeof (GLTranForReclassification.newTranDate).Name))
      return;
    if (e.Exception is PXSetPropertyException exception)
      exception.ErrorValue = (object) row.NewTranDate;
    cache.RaiseExceptionHandling<GLTranForReclassification.newTranDate>((object) row, (object) row.NewTranDate, e.Exception);
  }

  protected virtual void GLTranForReclassification_RowUpdated(
    PXCache cache,
    PXRowUpdatedEventArgs e)
  {
    GLTranForReclassification row = (GLTranForReclassification) e.Row;
    this.CalcSelectedFieldValue(row);
    if (!row.NewBranchID.HasValue)
      cache.RaiseExceptionHandling<GLTranForReclassification.newBranchID>(e.Row, (object) null, (Exception) new PXSetPropertyException("'{0}' cannot be empty.", (PXErrorLevel) 4, new object[1]
      {
        (object) "[newBranchID]"
      }));
    int? nullable = row.NewAccountID;
    if (!nullable.HasValue)
      cache.RaiseExceptionHandling<GLTranForReclassification.newAccountID>(e.Row, (object) null, (Exception) new PXSetPropertyException("'{0}' cannot be empty.", (PXErrorLevel) 4, new object[1]
      {
        (object) "[newAccountID]"
      }));
    nullable = row.NewSubID;
    if (!nullable.HasValue)
      cache.RaiseExceptionHandling<GLTranForReclassification.newSubID>(e.Row, (object) null, (Exception) new PXSetPropertyException("'{0}' cannot be empty.", (PXErrorLevel) 4, new object[1]
      {
        (object) "[newSubID]"
      }));
    if (!row.NewTranDate.HasValue)
      cache.RaiseExceptionHandling<GLTranForReclassification.newTranDate>(e.Row, (object) null, (Exception) new PXSetPropertyException("'{0}' cannot be empty.", (PXErrorLevel) 4, new object[1]
      {
        (object) "[newTranDate]"
      }));
    this.VerifyProjectFields(cache, e);
    if (row.IsSplitted && ReclassifyTransactionsBase<ReclassifyTransactionsProcess>.IsReclassAttrChanged(row) && row.ReclassRowType == ReclassRowTypes.EditingVirtualParentTran)
      row.ReclassRowType = ReclassRowTypes.AddingNew;
    bool screenIsEditable = !PXLongOperation.Exists(((PXGraph) this).UID);
    PXUIFieldAttribute.SetEnabled<GLTran.selected>(cache, (object) row, this.GetEnabledForSelectedCheckbox(row, screenIsEditable));
  }

  private bool GetEnabledForSelectedCheckbox(GLTranForReclassification tran, bool screenIsEditable)
  {
    return screenIsEditable && tran.Selected.GetValueOrDefault() && (!tran.IsSplitted || ReclassifyTransactionsBase<ReclassifyTransactionsProcess>.IsReclassAttrChanged(tran));
  }

  private void VerifyProjectFields(PXCache cache, PXRowUpdatedEventArgs e)
  {
    GLTranForReclassification row = (GLTranForReclassification) e.Row;
    if (!PXAccess.FeatureInstalled<FeaturesSet.projectAccounting>())
      return;
    int? nullable1 = row.NewProjectID;
    if (!nullable1.HasValue)
    {
      nullable1 = row.ProjectID;
      if (nullable1.HasValue)
        cache.RaiseExceptionHandling<GLTranForReclassification.newProjectID>(e.Row, (object) null, (Exception) new PXSetPropertyException("'{0}' cannot be empty.", (PXErrorLevel) 4, new object[1]
        {
          (object) "[newProjectID]"
        }));
    }
    nullable1 = row.NewTaskID;
    int? nullable2;
    if (!nullable1.HasValue)
    {
      nullable1 = row.TaskID;
      if (nullable1.HasValue)
      {
        nullable1 = row.NewProjectID;
        nullable2 = ProjectDefaultAttribute.NonProject();
        if (!(nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue) && string.IsNullOrEmpty(((PXFieldState) cache.GetStateExt<GLTranForReclassification.newTaskID>(e.Row))?.Error))
          cache.RaiseExceptionHandling<GLTranForReclassification.newTaskID>(e.Row, (object) null, (Exception) new PXSetPropertyException("'{0}' cannot be empty.", (PXErrorLevel) 4, new object[1]
          {
            (object) "[newTaskID]"
          }));
      }
    }
    if (!PXAccess.FeatureInstalled<FeaturesSet.costCodes>())
      return;
    nullable2 = row.NewCostCodeID;
    if (nullable2.HasValue)
      return;
    nullable2 = row.CostCodeID;
    if (!nullable2.HasValue)
      return;
    cache.RaiseExceptionHandling<GLTranForReclassification.newCostCodeID>(e.Row, (object) null, (Exception) new PXSetPropertyException("'{0}' cannot be empty.", (PXErrorLevel) 4, new object[1]
    {
      (object) "[newCostCodeID]"
    }));
  }

  private void IsDateWithinPeriod(PXCache cache, GLTranForReclassification tran)
  {
    DateTime? newTranDate;
    int num;
    if (tran == null)
    {
      num = 1;
    }
    else
    {
      newTranDate = tran.NewTranDate;
      num = !newTranDate.HasValue ? 1 : 0;
    }
    if (num != 0)
      return;
    if (this.FieldHasError(tran, "newTranDate"))
      return;
    try
    {
      IFinPeriodRepository periodRepository = this.FinPeriodRepository;
      string newFinPeriodId = tran.NewFinPeriodID;
      int? parentOrganizationId = PXAccess.GetParentOrganizationID(tran.NewBranchID);
      newTranDate = tran.NewTranDate;
      DateTime date = newTranDate.Value;
      periodRepository.CheckIsDateWithinPeriod(newFinPeriodId, parentOrganizationId, date, "The date is outside the range of the selected financial period.", (PXErrorLevel) 2);
      cache.RaiseExceptionHandling<GLTranForReclassification.newBranchID>((object) tran, (object) tran.NewBranchID, (Exception) null);
    }
    catch (PXSetPropertyException ex)
    {
      cache.RaiseExceptionHandling<GLTranForReclassification.newTranDate>((object) tran, (object) tran.NewTranDate, (Exception) ex);
    }
  }

  private void ValidateNewFields(GLTranForReclassification tran)
  {
    this.ValidateField<GLTranForReclassification.newBranchID>((object) tran.NewBranchID, tran);
    this.ValidateField<GLTranForReclassification.newAccountID>((object) tran.NewAccountID, tran);
    this.ValidateField<GLTranForReclassification.newSubID>((object) tran.NewSubID, tran);
    this.ValidateField<GLTranForReclassification.newTranDate>((object) tran.NewTranDate, tran);
    this.ValidateField<GLTranForReclassification.newFinPeriodID>((object) tran.NewFinPeriodID, tran);
    this.ValidateField<GLTranForReclassification.curyNewAmt>((object) tran.CuryNewAmt, tran);
    this.ValidateField<GLTranForReclassification.newProjectID>((object) tran.NewProjectID, tran);
    this.ValidateField<GLTranForReclassification.newTaskID>((object) tran.NewTaskID, tran);
    this.ValidateField<GLTranForReclassification.newCostCodeID>((object) tran.NewCostCodeID, tran);
  }

  private void ValidateField<TField>(object newValue, GLTranForReclassification tran) where TField : IBqlField
  {
    if (!string.IsNullOrEmpty(PXUIFieldAttribute.GetErrorOnly<TField>(((PXSelectBase) this.GLTranForReclass).Cache, (object) tran)))
      return;
    try
    {
      ((PXSelectBase) this.GLTranForReclass).Cache.RaiseFieldVerifying<TField>((object) tran, ref newValue);
    }
    catch (PXSetPropertyException ex)
    {
      ((PXSelectBase) this.GLTranForReclass).Cache.RaiseExceptionHandling<TField>((object) tran, newValue, (Exception) ex);
    }
  }

  private void CleanupValidationMessages(GLTranForReclassification tran)
  {
    ((PXSelectBase) this.GLTranForReclass).Cache.RaiseExceptionHandling<GLTranForReclassification.newBranchID>((object) tran, (object) tran.NewBranchID, (Exception) null);
    ((PXSelectBase) this.GLTranForReclass).Cache.RaiseExceptionHandling<GLTranForReclassification.newAccountID>((object) tran, (object) tran.NewAccountID, (Exception) null);
    ((PXSelectBase) this.GLTranForReclass).Cache.RaiseExceptionHandling<GLTranForReclassification.newSubID>((object) tran, (object) tran.NewSubID, (Exception) null);
    ((PXSelectBase) this.GLTranForReclass).Cache.RaiseExceptionHandling<GLTranForReclassification.newTranDate>((object) tran, (object) tran.NewTranDate, (Exception) null);
    ((PXSelectBase) this.GLTranForReclass).Cache.RaiseExceptionHandling<GLTranForReclassification.newFinPeriodID>((object) tran, (object) tran.NewFinPeriodID, (Exception) null);
    ((PXSelectBase) this.GLTranForReclass).Cache.RaiseExceptionHandling<GLTranForReclassification.curyNewAmt>((object) tran, (object) tran.CuryNewAmt, (Exception) null);
    ((PXSelectBase) this.GLTranForReclass).Cache.RaiseExceptionHandling<GLTranForReclassification.newProjectID>((object) tran, (object) tran.NewProjectID, (Exception) null);
    ((PXSelectBase) this.GLTranForReclass).Cache.RaiseExceptionHandling<GLTranForReclassification.newTaskID>((object) tran, (object) tran.NewTaskID, (Exception) null);
    ((PXSelectBase) this.GLTranForReclass).Cache.RaiseExceptionHandling<GLTranForReclassification.newCostCodeID>((object) tran, (object) tran.NewCostCodeID, (Exception) null);
  }

  protected virtual void GLTranForReclassification_CuryNewAmt_FieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
    GLTranForReclassification row = (GLTranForReclassification) e.Row;
    if (!row.IsSplitting)
      return;
    GLTranForReclassification reclassification1 = ((PXSelectBase<GLTranForReclassification>) this.GLTranForReclass_Module_BatchNbr_LineNbr).Locate(this.GetGLTranForReclassByKey(row.ParentKey));
    Decimal? curyDebitAmt = reclassification1.CuryDebitAmt;
    Decimal? curyCreditAmt = reclassification1.CuryCreditAmt;
    Decimal? nullable1 = curyDebitAmt.HasValue & curyCreditAmt.HasValue ? new Decimal?(curyDebitAmt.GetValueOrDefault() + curyCreditAmt.GetValueOrDefault()) : new Decimal?();
    Decimal num1 = 0M;
    int num2 = nullable1.GetValueOrDefault() > num1 & nullable1.HasValue ? 1 : -1;
    Decimal num3 = (Decimal) num2;
    nullable1 = reclassification1.CuryNewAmt;
    Decimal num4 = (Decimal?) e.OldValue ?? 0.0M;
    Decimal num5 = (nullable1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + num4) : new Decimal?()) ?? 0.0M;
    Decimal num6 = num3 * num5;
    Decimal num7 = (Decimal) num2;
    Decimal? nullable2 = row.CuryNewAmt;
    Decimal valueOrDefault = nullable2.GetValueOrDefault();
    Decimal num8 = num7 * valueOrDefault;
    if (num6 < num8)
      this.SetExhaustedAmountError(row);
    GLTranForReclassification reclassification2 = reclassification1;
    nullable1 = reclassification2.CuryNewAmt;
    nullable2 = (Decimal?) e.OldValue;
    Decimal num9 = nullable2 ?? 0.0M;
    nullable2 = row.CuryNewAmt;
    Decimal num10 = nullable2 ?? 0.0M;
    Decimal num11 = num9 - num10;
    Decimal? nullable3;
    if (!nullable1.HasValue)
    {
      nullable2 = new Decimal?();
      nullable3 = nullable2;
    }
    else
      nullable3 = new Decimal?(nullable1.GetValueOrDefault() + num11);
    reclassification2.CuryNewAmt = nullable3;
    cache.Update((object) reclassification1);
    ((PXSelectBase) this.GLTranForReclass).View.RequestRefresh();
  }

  protected virtual void GLTranForReclassification_CuryNewAmt_FieldSelecting(
    PXCache cache,
    PXFieldSelectingEventArgs e)
  {
    GLTranForReclassification row = (GLTranForReclassification) e.Row;
    if (row == null)
      return;
    if (!row.IsSplitted && !row.IsSplitting)
      e.ReturnValue = (object) null;
    if (!row.IsSplitted && !row.IsSplitting || e.ReturnValue != null)
      return;
    e.ReturnValue = (object) 0M;
  }

  protected virtual void GLTranForReclassification_SplittedIcon_FieldSelecting(
    PXCache cache,
    PXFieldSelectingEventArgs e)
  {
    GLTranForReclassification row = (GLTranForReclassification) e.Row;
    if (row == null)
      return;
    if (this.State.SplittingGroups.ContainsKey(new GLTranKey((GLTran) row)))
      e.ReturnValue = (object) "~/Icons/parent_cc.svg";
    if (!row.IsSplitting)
      return;
    e.ReturnValue = (object) "~/Icons/subdirectory_arrow_right_cc.svg";
  }

  protected virtual void LoadOptions_FromAccountID_FieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
    ReclassifyTransactionsProcess.LoadOptions row = (ReclassifyTransactionsProcess.LoadOptions) e.Row;
    PXFieldState stateExt1 = (PXFieldState) cache.GetStateExt<ReclassifyTransactionsProcess.LoadOptions.fromAccountID>((object) row);
    PXFieldState stateExt2 = (PXFieldState) cache.GetStateExt<ReclassifyTransactionsProcess.LoadOptions.toAccountID>((object) row);
    string strA = (string) stateExt1.Value;
    string strB = (string) stateExt2.Value;
    if (string.CompareOrdinal(strA, strB) <= 0)
      return;
    cache.SetValueExt<ReclassifyTransactionsProcess.LoadOptions.toAccountID>(e.Row, (object) strA);
  }

  protected virtual void LoadOptions_ToAccountID_FieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
    ReclassifyTransactionsProcess.LoadOptions row = (ReclassifyTransactionsProcess.LoadOptions) e.Row;
    PXFieldState stateExt1 = (PXFieldState) cache.GetStateExt<ReclassifyTransactionsProcess.LoadOptions.fromAccountID>((object) row);
    PXFieldState stateExt2 = (PXFieldState) cache.GetStateExt<ReclassifyTransactionsProcess.LoadOptions.toAccountID>((object) row);
    string strA = (string) stateExt1.Value;
    string strB = (string) stateExt2.Value;
    if (strB == null || string.CompareOrdinal(strA, strB) <= 0)
      return;
    cache.SetValueExt<ReclassifyTransactionsProcess.LoadOptions.fromAccountID>(e.Row, (object) strB);
  }

  protected virtual void LoadOptions_FromTaskID_FieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
    ReclassifyTransactionsProcess.LoadOptions row = (ReclassifyTransactionsProcess.LoadOptions) e.Row;
    PXFieldState stateExt1 = (PXFieldState) cache.GetStateExt<ReclassifyTransactionsProcess.LoadOptions.fromTaskID>((object) row);
    PXFieldState stateExt2 = (PXFieldState) cache.GetStateExt<ReclassifyTransactionsProcess.LoadOptions.toTaskID>((object) row);
    string strA = (string) stateExt1.Value;
    string strB = (string) stateExt2.Value;
    if (string.CompareOrdinal(strA, strB) <= 0)
      return;
    cache.SetValueExt<ReclassifyTransactionsProcess.LoadOptions.toTaskID>(e.Row, (object) strA);
  }

  protected virtual void LoadOptions_ToTaskID_FieldUpdated(PXCache cache, PXFieldUpdatedEventArgs e)
  {
    ReclassifyTransactionsProcess.LoadOptions row = (ReclassifyTransactionsProcess.LoadOptions) e.Row;
    PXFieldState stateExt1 = (PXFieldState) cache.GetStateExt<ReclassifyTransactionsProcess.LoadOptions.fromTaskID>((object) row);
    PXFieldState stateExt2 = (PXFieldState) cache.GetStateExt<ReclassifyTransactionsProcess.LoadOptions.toTaskID>((object) row);
    string strA = (string) stateExt1.Value;
    string strB = (string) stateExt2.Value;
    if (strB == null || string.CompareOrdinal(strA, strB) <= 0)
      return;
    cache.SetValueExt<ReclassifyTransactionsProcess.LoadOptions.fromTaskID>(e.Row, (object) strB);
  }

  protected virtual void LoadOptions_FromCostCodeID_FieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
    ReclassifyTransactionsProcess.LoadOptions row = (ReclassifyTransactionsProcess.LoadOptions) e.Row;
    PXFieldState stateExt1 = (PXFieldState) cache.GetStateExt<ReclassifyTransactionsProcess.LoadOptions.fromCostCodeID>((object) row);
    PXFieldState stateExt2 = (PXFieldState) cache.GetStateExt<ReclassifyTransactionsProcess.LoadOptions.toCostCodeID>((object) row);
    string strA = (string) stateExt1.Value;
    string strB = (string) stateExt2.Value;
    if (string.CompareOrdinal(strA, strB) <= 0)
      return;
    cache.SetValueExt<ReclassifyTransactionsProcess.LoadOptions.toCostCodeID>(e.Row, (object) strA);
  }

  protected virtual void LoadOptions_ToCostCodeID_FieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
    ReclassifyTransactionsProcess.LoadOptions row = (ReclassifyTransactionsProcess.LoadOptions) e.Row;
    PXFieldState stateExt1 = (PXFieldState) cache.GetStateExt<ReclassifyTransactionsProcess.LoadOptions.fromCostCodeID>((object) row);
    PXFieldState stateExt2 = (PXFieldState) cache.GetStateExt<ReclassifyTransactionsProcess.LoadOptions.toCostCodeID>((object) row);
    string strA = (string) stateExt1.Value;
    string strB = (string) stateExt2.Value;
    if (strB == null || string.CompareOrdinal(strA, strB) <= 0)
      return;
    cache.SetValueExt<ReclassifyTransactionsProcess.LoadOptions.fromCostCodeID>(e.Row, (object) strB);
  }

  protected virtual void LoadOptions_FromSubID_FieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
    ReclassifyTransactionsProcess.LoadOptions row = (ReclassifyTransactionsProcess.LoadOptions) e.Row;
    if (string.CompareOrdinal((string) ((PXFieldState) cache.GetStateExt<ReclassifyTransactionsProcess.LoadOptions.fromSubID>((object) row)).Value, (string) ((PXFieldState) cache.GetStateExt<ReclassifyTransactionsProcess.LoadOptions.toSubID>((object) row)).Value) <= 0)
      return;
    cache.SetValue<ReclassifyTransactionsProcess.LoadOptions.toSubID>(e.Row, (object) row.FromSubID);
  }

  protected virtual void LoadOptions_ToSubID_FieldUpdated(PXCache cache, PXFieldUpdatedEventArgs e)
  {
    ReclassifyTransactionsProcess.LoadOptions row = (ReclassifyTransactionsProcess.LoadOptions) e.Row;
    PXFieldState stateExt1 = (PXFieldState) cache.GetStateExt<ReclassifyTransactionsProcess.LoadOptions.fromSubID>((object) row);
    PXFieldState stateExt2 = (PXFieldState) cache.GetStateExt<ReclassifyTransactionsProcess.LoadOptions.toSubID>((object) row);
    string strA = (string) stateExt1.Value;
    string strB = (string) stateExt2.Value;
    if (strB == null || string.CompareOrdinal(strA, strB) <= 0)
      return;
    cache.SetValue<ReclassifyTransactionsProcess.LoadOptions.fromSubID>(e.Row, (object) row.ToSubID);
  }

  protected virtual void LoadOptions_FromFinPeriodID_FieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
    ReclassifyTransactionsProcess.LoadOptions row = (ReclassifyTransactionsProcess.LoadOptions) e.Row;
    if (string.CompareOrdinal(row.FromFinPeriodID, row.ToFinPeriodID) > 0)
      cache.SetValue<ReclassifyTransactionsProcess.LoadOptions.toFinPeriodID>(e.Row, (object) row.FromFinPeriodID);
    this.SetPeriodDates(row);
  }

  protected virtual void LoadOptions_ToFinPeriodID_FieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
    ReclassifyTransactionsProcess.LoadOptions row = (ReclassifyTransactionsProcess.LoadOptions) e.Row;
    if (row.ToFinPeriodID != null && string.CompareOrdinal(row.FromFinPeriodID, row.ToFinPeriodID) > 0)
      cache.SetValue<ReclassifyTransactionsProcess.LoadOptions.fromFinPeriodID>(e.Row, (object) row.ToFinPeriodID);
    this.SetPeriodDates(row);
  }

  protected virtual void LoadOptions_FromDate_FieldUpdated(PXCache cache, PXFieldUpdatedEventArgs e)
  {
    ReclassifyTransactionsProcess.LoadOptions row = (ReclassifyTransactionsProcess.LoadOptions) e.Row;
    DateTime? fromDate = row.FromDate;
    DateTime? toDate = row.ToDate;
    if ((fromDate.HasValue & toDate.HasValue ? (fromDate.GetValueOrDefault() > toDate.GetValueOrDefault() ? 1 : 0) : 0) == 0 && row.ToDate.HasValue)
      return;
    cache.SetValue<ReclassifyTransactionsProcess.LoadOptions.toDate>(e.Row, (object) row.FromDate);
  }

  protected virtual void LoadOptions_ToDate_FieldUpdated(PXCache cache, PXFieldUpdatedEventArgs e)
  {
    ReclassifyTransactionsProcess.LoadOptions row = (ReclassifyTransactionsProcess.LoadOptions) e.Row;
    if (!row.ToDate.HasValue)
      return;
    DateTime? fromDate = row.FromDate;
    DateTime? toDate = row.ToDate;
    if ((fromDate.HasValue & toDate.HasValue ? (fromDate.GetValueOrDefault() > toDate.GetValueOrDefault() ? 1 : 0) : 0) == 0)
      return;
    cache.SetValue<ReclassifyTransactionsProcess.LoadOptions.fromDate>(e.Row, (object) row.ToDate);
  }

  protected virtual void LoadOptions_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is ReclassifyTransactionsProcess.LoadOptions row))
      return;
    DateTime? nullable1 = row.ToDate;
    DateTime? nullable2;
    if (nullable1.HasValue)
    {
      nullable1 = row.PeriodEndDate;
      if (nullable1.HasValue)
      {
        nullable1 = row.ToDate;
        DateTime? periodEndDate = row.PeriodEndDate;
        if ((nullable1.HasValue & periodEndDate.HasValue ? (nullable1.GetValueOrDefault() > periodEndDate.GetValueOrDefault() ? 1 : 0) : 0) != 0)
          goto label_6;
      }
      nullable2 = row.PeriodStartDate;
      if (nullable2.HasValue)
      {
        nullable2 = row.ToDate;
        nullable1 = row.PeriodStartDate;
        if ((nullable2.HasValue & nullable1.HasValue ? (nullable2.GetValueOrDefault() < nullable1.GetValueOrDefault() ? 1 : 0) : 0) == 0)
          goto label_7;
      }
      else
        goto label_7;
label_6:
      cache.RaiseExceptionHandling<ReclassifyTransactionsProcess.LoadOptions.toDate>(e.Row, (object) row.ToDate, (Exception) new PXSetPropertyException("The date is outside of the specified period.", (PXErrorLevel) 2));
      goto label_8;
    }
label_7:
    cache.RaiseExceptionHandling<ReclassifyTransactionsProcess.LoadOptions.toDate>(e.Row, (object) null, (Exception) null);
label_8:
    nullable1 = row.FromDate;
    if (nullable1.HasValue)
    {
      nullable1 = row.PeriodStartDate;
      if (nullable1.HasValue)
      {
        nullable1 = row.FromDate;
        nullable2 = row.PeriodStartDate;
        if ((nullable1.HasValue & nullable2.HasValue ? (nullable1.GetValueOrDefault() < nullable2.GetValueOrDefault() ? 1 : 0) : 0) != 0)
          goto label_13;
      }
      nullable2 = row.PeriodEndDate;
      if (nullable2.HasValue)
      {
        nullable2 = row.FromDate;
        nullable1 = row.PeriodEndDate;
        if ((nullable2.HasValue & nullable1.HasValue ? (nullable2.GetValueOrDefault() >= nullable1.GetValueOrDefault() ? 1 : 0) : 0) == 0)
          goto label_14;
      }
      else
        goto label_14;
label_13:
      cache.RaiseExceptionHandling<ReclassifyTransactionsProcess.LoadOptions.fromDate>(e.Row, (object) row.FromDate, (Exception) new PXSetPropertyException("The date is outside of the specified period.", (PXErrorLevel) 2));
      return;
    }
label_14:
    cache.RaiseExceptionHandling<ReclassifyTransactionsProcess.LoadOptions.fromDate>(e.Row, (object) null, (Exception) null);
  }

  private void ButtonsFieldSelectingHandlerForDisableAfterProcess(
    PXCache sender,
    PXFieldSelectingEventArgs e)
  {
    e.ReturnState = (object) this.CreateReturnState(e.ReturnState);
    ((PXFieldState) e.ReturnState).Enabled = PXLongOperation.GetStatus(((PXGraph) this).UID) == 0;
  }

  private void LoadOptionsButtonFieldSelectingHandler(PXCache sender, PXFieldSelectingEventArgs e)
  {
    e.ReturnState = (object) this.CreateReturnState(e.ReturnState);
    ReclassifyTransactionsProcess graph = sender.Graph as ReclassifyTransactionsProcess;
    ((PXFieldState) e.ReturnState).Enabled = PXLongOperation.GetStatus(((PXGraph) this).UID) == null && (graph == null || graph.State.ReclassScreenMode != ReclassScreenMode.Editing);
  }

  private void ProcessButtonFieldSelectingHandler(PXCache sender, PXFieldSelectingEventArgs e)
  {
    e.ReturnState = (object) this.CreateReturnState(e.ReturnState);
    ((PXFieldState) e.ReturnState).Enabled = PXLongOperation.GetStatus(((PXGraph) this).UID) == null && NonGenericIEnumerableExtensions.Any_(sender.Cached);
  }

  private void DependingOnRowExistanceButtonsSelectingHandler(
    PXCache sender,
    PXFieldSelectingEventArgs e)
  {
    e.ReturnState = (object) this.CreateReturnState(e.ReturnState);
    this.ButtonsFieldSelectingHandlerForDisableAfterProcess(sender, e);
    ((PXFieldState) e.ReturnState).Enabled = ((PXFieldState) e.ReturnState).Enabled && NonGenericIEnumerableExtensions.Any_(sender.Cached);
  }

  private void ReloadTransButtonStateSelectingHandler(PXCache sender, PXFieldSelectingEventArgs e)
  {
    e.ReturnState = (object) this.CreateReturnState(e.ReturnState);
    ((PXFieldState) e.ReturnState).Enabled = PXLongOperation.GetStatus(((PXGraph) this).UID) == null && this.State.ReclassScreenMode != ReclassScreenMode.Editing;
  }

  protected virtual void ReplaceOptions_NewFinPeriodID_ExceptionHandling(
    PXCache cache,
    PXExceptionHandlingEventArgs e)
  {
    ReclassifyTransactionsProcess.ReplaceOptions row = (ReclassifyTransactionsProcess.ReplaceOptions) e.Row;
    cache.RaiseExceptionHandling<ReclassifyTransactionsProcess.ReplaceOptions.newDate>((object) row, (object) row.NewDate, e.Exception);
  }

  protected virtual void ReplaceOptions_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    ReclassifyTransactionsProcess.ReplaceOptions row = (ReclassifyTransactionsProcess.ReplaceOptions) e.Row;
    if (!row.Showed)
    {
      row.Showed = true;
      if (row.NewFinPeriodID != null)
      {
        object newFinPeriodId = (object) row.NewFinPeriodID;
        cache.RaiseFieldVerifying<ReclassifyTransactionsProcess.ReplaceOptions.newFinPeriodID>((object) row, ref newFinPeriodId);
      }
      else if (row.NewDate.HasValue)
        cache.RaiseFieldUpdated<ReclassifyTransactionsProcess.ReplaceOptions.newDate>((object) row, (object) row.NewDate);
    }
    bool flag = this.State.SplittingGroups.Any<KeyValuePair<GLTranKey, List<GLTranKey>>>();
    string str = "The Replace action will affect any split transactions that match the selected criteria.";
    row.Warning = str;
    PXUIFieldAttribute.SetVisible<ReclassifyTransactionsProcess.ReplaceOptions.warning>(cache, (object) row, flag);
    cache.RaiseExceptionHandling<ReclassifyTransactionsProcess.ReplaceOptions.warning>((object) row, (object) str, (Exception) new PXSetPropertyException(str, (PXErrorLevel) 2));
  }

  protected virtual void ReplaceOptions_WithAccountID_FieldVerifying(
    PXCache cache,
    PXFieldVerifyingEventArgs e)
  {
    if (e.Row == null || e.NewValue == null)
      return;
    PX.Objects.GL.Account account = (PX.Objects.GL.Account) PXSelectorAttribute.Select<ReclassifyTransactionsProcess.ReplaceOptions.withAccountID>(cache, e.Row, e.NewValue);
    AccountAttribute.VerifyAccountIsNotControl<ReclassifyTransactionsProcess.ReplaceOptions.withAccountID>(cache, (EventArgs) e, account, true);
  }

  protected virtual void ReplaceOptions_NewAccountID_FieldVerifying(
    PXCache cache,
    PXFieldVerifyingEventArgs e)
  {
    if (e.Row == null || e.NewValue == null)
      return;
    PX.Objects.GL.Account account = (PX.Objects.GL.Account) PXSelectorAttribute.Select<ReclassifyTransactionsProcess.ReplaceOptions.newAccountID>(cache, e.Row, e.NewValue);
    AccountAttribute.VerifyAccountIsNotControl<ReclassifyTransactionsProcess.ReplaceOptions.newAccountID>(cache, (EventArgs) e, account, true);
  }

  [PXButton(ImageKey = "RecordDel")]
  [PXUIField]
  protected virtual IEnumerable delete(PXAdapter adapter)
  {
    GLTranForReclassification current = ((PXSelectBase<GLTranForReclassification>) this.GLTranForReclass).Current;
    string str = current.IsSplitted ? "The current record and its child records will be deleted." : "The current record will be deleted.";
    if (adapter.View.Ask(str, (MessageButtons) 1) == 2)
      adapter.Get();
    GLTranKey tranKey = new GLTranKey((GLTran) current);
    if (this.State.SplittingGroups.ContainsKey(tranKey))
    {
      foreach (GLTranKey key in this.State.SplittingGroups[tranKey])
        this.RemoveTran(((PXSelectBase<GLTranForReclassification>) this.GLTranForReclass_Module_BatchNbr_LineNbr).Locate(this.GetGLTranForReclassByKey(key)));
      this.State.SplittingGroups.Remove(tranKey);
    }
    this.RemoveTran(current);
    ((PXSelectBase) this.GLTranForReclass).Cache.Remove((object) current);
    if (current.IsSplitting)
    {
      if (this.State.SplittingGroups[current.ParentKey].Any<GLTranKey>((Func<GLTranKey, bool>) (m => m.Equals(tranKey))))
        this.State.SplittingGroups[current.ParentKey].Remove(tranKey);
      GLTranForReclassification reclassification1 = ((PXSelectBase<GLTranForReclassification>) this.GLTranForReclass_Module_BatchNbr_LineNbr).Locate(this.GetGLTranForReclassByKey(current.ParentKey));
      if (!this.State.SplittingGroups[current.ParentKey].Any<GLTranKey>())
      {
        reclassification1.IsSplitted = false;
        reclassification1.CuryNewAmt = new Decimal?(0M);
        this.State.SplittingGroups.Remove(current.ParentKey);
      }
      GLTranForReclassification reclassification2 = reclassification1;
      Decimal? curyNewAmt1 = reclassification2.CuryNewAmt;
      Decimal? curyNewAmt2 = current.CuryNewAmt;
      reclassification2.CuryNewAmt = curyNewAmt1.HasValue & curyNewAmt2.HasValue ? new Decimal?(curyNewAmt1.GetValueOrDefault() + curyNewAmt2.GetValueOrDefault()) : new Decimal?();
      ((PXSelectBase) this.GLTranForReclass).Cache.Update((object) reclassification1);
    }
    return adapter.Get();
  }

  private void RemoveTran(GLTranForReclassification splitRow)
  {
    if (splitRow.ReclassRowType == ReclassRowTypes.Editing)
      this.State.GLTranForReclassToDelete.Add(splitRow);
    ((PXSelectBase) this.GLTranForReclass).Cache.Remove((object) splitRow);
  }

  [PXCancelButton]
  [PXUIField]
  protected virtual IEnumerable cancel(PXAdapter adapter)
  {
    IEnumerable<GLTranForReclassification> trans = this.GetUpdatedTranForReclass().Where<GLTranForReclassification>((Func<GLTranForReclassification, bool>) (tran => tran.ReclassRowType == ReclassRowTypes.AddingNew));
    ReclassGraphState state = this.State;
    state.GLTranForReclassToDelete.Clear();
    ((PXGraph) this).Clear();
    this.State = state;
    this.State.ClearSplittingGroups();
    this.PutTransForReclassToCacheByKey((IEnumerable<GLTran>) trans);
    this.PutReclassificationBatchTransForEditingToCache(this.State.EditingBatchModule, this.State.EditingBatchNbr);
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable viewReclassBatch(PXAdapter adapter)
  {
    GLTranForReclassification current = ((PXSelectBase<GLTranForReclassification>) this.GLTranForReclass).Current;
    if (current != null)
      JournalEntry.RedirectToBatch((PXGraph) this, current.ReclassBatchModule, current.ReclassBatchNbr);
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable ShowLoadTransPopup(PXAdapter adapter)
  {
    ((PXSelectBase<ReclassifyTransactionsProcess.LoadOptions>) this.LoadOptionsView).AskExt();
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable ReloadTrans(PXAdapter adapter)
  {
    if (((PXSelectBase<GLTranForReclassification>) this.GLTranForReclass).Ask("Transactions listed on the form (if any) will be removed. New transactions that match the selection criteria will be loaded. Do you want to continue?", (MessageButtons) 1) != 1)
      return adapter.Get();
    ((PXSelectBase) this.GLTranForReclass).Cache.Clear();
    ((PXSelectBase) this.GLTranForReclass).Cache.ClearQueryCacheObsolete();
    this.State.ClearSplittingGroups();
    this.LoadTransactionsProc(true);
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable LoadTrans(PXAdapter adapter)
  {
    this.LoadTransactionsProc();
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable Replace(PXAdapter adapter)
  {
    ReclassifyTransactionsProcess.ReplaceOptions options = ((PXSelectBase<ReclassifyTransactionsProcess.ReplaceOptions>) this.ReplaceOptionsView).Current;
    options.Showed = false;
    if (((PXSelectBase<ReclassifyTransactionsProcess.ReplaceOptions>) this.ReplaceOptionsView).AskExt() == 1 && (options.NewBranchID.HasValue || options.NewAccountID.HasValue || options.NewSubID.HasValue || options.NewFinPeriodID != null || options.NewDate.HasValue || options.NewTranDesc != null || options.NewProjectID.HasValue || options.NewTaskID.HasValue || options.NewCostCodeID.HasValue))
    {
      IEnumerable<GLTranForReclassification> reclassifications = this.GetUpdatedTranForReclass();
      int? nullable = options.WithBranchID;
      if (nullable.HasValue)
        reclassifications = reclassifications.Where<GLTranForReclassification>((Func<GLTranForReclassification, bool>) (tran =>
        {
          int? newBranchId = tran.NewBranchID;
          int? withBranchId = options.WithBranchID;
          return newBranchId.GetValueOrDefault() == withBranchId.GetValueOrDefault() & newBranchId.HasValue == withBranchId.HasValue;
        }));
      nullable = options.WithAccountID;
      if (nullable.HasValue)
        reclassifications = reclassifications.Where<GLTranForReclassification>((Func<GLTranForReclassification, bool>) (tran =>
        {
          int? newAccountId = tran.NewAccountID;
          int? withAccountId = options.WithAccountID;
          return newAccountId.GetValueOrDefault() == withAccountId.GetValueOrDefault() & newAccountId.HasValue == withAccountId.HasValue;
        }));
      nullable = options.WithSubID;
      if (nullable.HasValue)
        reclassifications = reclassifications.Where<GLTranForReclassification>((Func<GLTranForReclassification, bool>) (tran =>
        {
          int? newSubId = tran.NewSubID;
          int? withSubId = options.WithSubID;
          return newSubId.GetValueOrDefault() == withSubId.GetValueOrDefault() & newSubId.HasValue == withSubId.HasValue;
        }));
      if (options.WithFinPeriodID != null)
      {
        string masterPeriodID = FinPeriodIDAttribute.CalcMasterPeriodID<ReclassifyTransactionsProcess.ReplaceOptions.withFinPeriodID>(((PXSelectBase) this.ReplaceOptionsView).Cache, (object) options);
        reclassifications = reclassifications.Where<GLTranForReclassification>((Func<GLTranForReclassification, bool>) (tran => tran.TranPeriodID == masterPeriodID));
      }
      if (options.WithDate.HasValue)
        reclassifications = reclassifications.Where<GLTranForReclassification>((Func<GLTranForReclassification, bool>) (tran =>
        {
          DateTime? newTranDate = tran.NewTranDate;
          DateTime? withDate = options.WithDate;
          if (newTranDate.HasValue != withDate.HasValue)
            return false;
          return !newTranDate.HasValue || newTranDate.GetValueOrDefault() == withDate.GetValueOrDefault();
        }));
      if (options.WithTranDescFilteringValue != null)
        reclassifications = this.AddTransDescWhereConditionForReplace(reclassifications, options.WithTranDescFilteringValue);
      foreach (GLTranForReclassification tran in ReclassifyTransactionsProcess.OptionallySetUpProjectOptionsToReplace(options, reclassifications))
      {
        GLTranForReclassification copy = PXCache<GLTranForReclassification>.CreateCopy(tran);
        nullable = options.NewBranchID;
        if (nullable.HasValue)
        {
          nullable = options.NewBranchID;
          int? newBranchId = tran.NewBranchID;
          if (!(nullable.GetValueOrDefault() == newBranchId.GetValueOrDefault() & nullable.HasValue == newBranchId.HasValue))
            tran.NewBranchID = options.NewBranchID;
        }
        if (options.NewAccountID.HasValue)
        {
          int? newAccountId = options.NewAccountID;
          nullable = tran.NewAccountID;
          if (!(newAccountId.GetValueOrDefault() == nullable.GetValueOrDefault() & newAccountId.HasValue == nullable.HasValue))
            tran.NewAccountID = options.NewAccountID;
        }
        nullable = options.NewSubID;
        if (nullable.HasValue)
        {
          nullable = options.NewSubID;
          int? newSubId = tran.NewSubID;
          if (!(nullable.GetValueOrDefault() == newSubId.GetValueOrDefault() & nullable.HasValue == newSubId.HasValue))
          {
            PXFieldState stateExt = (PXFieldState) ((PXSelectBase) this.ReplaceOptionsView).Cache.GetStateExt<ReclassifyTransactionsProcess.ReplaceOptions.newSubID>((object) options);
            ((PXSelectBase) this.GLTranForReclass).Cache.SetValueExt<GLTranForReclassification.newSubID>((object) tran, stateExt.Value);
          }
        }
        if (options.NewDate.HasValue)
        {
          DateTime? newDate = options.NewDate;
          DateTime? newTranDate1 = tran.NewTranDate;
          if ((newDate.HasValue == newTranDate1.HasValue ? (newDate.HasValue ? (newDate.GetValueOrDefault() != newTranDate1.GetValueOrDefault() ? 1 : 0) : 0) : 1) != 0)
          {
            tran.NewTranDate = options.NewDate;
            object newTranDate2 = (object) tran.NewTranDate;
            try
            {
              ((PXSelectBase) this.GLTranForReclass).Cache.RaiseFieldVerifying<GLTranForReclassification.newTranDate>((object) tran, ref newTranDate2);
            }
            catch (PXSetPropertyException ex)
            {
              ((PXSelectBase) this.GLTranForReclass).Cache.RaiseExceptionHandling<GLTranForReclassification.newTranDate>((object) tran, newTranDate2, (Exception) ex);
            }
          }
        }
        if (options.NewTranDesc != null)
          tran.NewTranDesc = ((IEnumerable<string>) ReclassifyTransactionsProcess._emptyStringReplaceWildCards).Contains<string>(options.NewTranDesc) ? (string) null : options.NewTranDesc;
        this.ReplaceProjectAttributes(options, tran);
        ((PXSelectBase) this.GLTranForReclass).Cache.RaiseRowUpdated((object) tran, (object) copy);
        ((PXSelectBase) this.GLTranForReclass).Cache.RaiseRowSelected((object) tran);
      }
    }
    return adapter.Get();
  }

  private static IEnumerable<GLTranForReclassification> OptionallySetUpProjectOptionsToReplace(
    ReclassifyTransactionsProcess.ReplaceOptions options,
    IEnumerable<GLTranForReclassification> trans)
  {
    if (options.WithProjectID.HasValue)
      trans = trans.Where<GLTranForReclassification>((Func<GLTranForReclassification, bool>) (tran =>
      {
        int? newProjectId = tran.NewProjectID;
        int? withProjectId = options.WithProjectID;
        return newProjectId.GetValueOrDefault() == withProjectId.GetValueOrDefault() & newProjectId.HasValue == withProjectId.HasValue;
      }));
    int? nullable = options.WithTaskID;
    if (nullable.HasValue)
      trans = trans.Where<GLTranForReclassification>((Func<GLTranForReclassification, bool>) (tran =>
      {
        int? newTaskId = tran.NewTaskID;
        int? withTaskId = options.WithTaskID;
        return newTaskId.GetValueOrDefault() == withTaskId.GetValueOrDefault() & newTaskId.HasValue == withTaskId.HasValue;
      }));
    nullable = options.WithCostCodeID;
    if (nullable.HasValue)
      trans = trans.Where<GLTranForReclassification>((Func<GLTranForReclassification, bool>) (tran =>
      {
        int? newCostCodeId = tran.NewCostCodeID;
        int? withCostCodeId = options.WithCostCodeID;
        return newCostCodeId.GetValueOrDefault() == withCostCodeId.GetValueOrDefault() & newCostCodeId.HasValue == withCostCodeId.HasValue;
      }));
    return trans;
  }

  private void ReplaceProjectAttributes(
    ReclassifyTransactionsProcess.ReplaceOptions options,
    GLTranForReclassification tran)
  {
    int? nullable1;
    int? nullable2;
    if (options.NewProjectID.HasValue)
    {
      nullable1 = options.NewProjectID;
      nullable2 = tran.NewProjectID;
      if (!(nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue))
      {
        tran.NewProjectID = options.NewProjectID;
        nullable2 = options.NewTaskID;
        if (!nullable2.HasValue)
          tran.NewTaskID = this.GetNewTaskIdAfterProjectReplaceWithoutTaskReplace(tran.NewProjectID, tran.ProjectID, tran.TaskID);
      }
    }
    nullable2 = options.NewTaskID;
    if (nullable2.HasValue)
    {
      nullable2 = options.NewTaskID;
      nullable1 = tran.NewTaskID;
      if (!(nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue))
        tran.NewTaskID = options.NewTaskID;
    }
    nullable1 = options.NewCostCodeID;
    if (!nullable1.HasValue)
      return;
    nullable1 = options.NewCostCodeID;
    nullable2 = tran.NewCostCodeID;
    if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
      return;
    tran.NewCostCodeID = options.NewCostCodeID;
  }

  private int? GetNewTaskIdAfterProjectReplaceWithoutTaskReplace(
    int? newProjectId,
    int? oldProjectId,
    int? oldTaskId)
  {
    if (!newProjectId.HasValue || !oldProjectId.HasValue || !oldTaskId.HasValue)
      return new int?();
    if (ProjectDefaultAttribute.IsNonProject(newProjectId))
      return new int?();
    PMTask pmTask = PMTask.PK.Find((PXGraph) this, oldProjectId, oldTaskId);
    return PMTask.UK.Find((PXGraph) this, newProjectId, pmTask.TaskCD)?.TaskID;
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable Split(PXAdapter adapter)
  {
    GLTranForReclassification reclassification1 = !((PXSelectBase<GLTranForReclassification>) this.GLTranForReclass).Current.IsSplitting ? ((PXSelectBase<GLTranForReclassification>) this.GLTranForReclass).Current : ((PXSelectBase<GLTranForReclassification>) this.GLTranForReclass).Locate(this.GetGLTranForReclassByKey(((PXSelectBase<GLTranForReclassification>) this.GLTranForReclass).Current.ParentKey));
    GLTranForReclassification tran = this.SplitTransaction(((PXSelectBase) this.GLTranForReclass).Cache, reclassification1);
    GLTranKey key = new GLTranKey((GLTran) reclassification1);
    GLTranKey glTranKey = new GLTranKey((GLTran) tran);
    if (!this.State.SplittingGroups.ContainsKey(key))
    {
      this.State.SplittingGroups[key] = new List<GLTranKey>()
      {
        new GLTranKey((GLTran) tran)
      };
      GLTranForReclassification reclassification2 = reclassification1;
      Decimal? nullable1 = reclassification1.CuryReclassRemainingAmt;
      Decimal num = 0M;
      Decimal? nullable2;
      if (nullable1.GetValueOrDefault() == num & nullable1.HasValue)
      {
        nullable1 = reclassification1.CuryDebitAmt;
        Decimal? curyCreditAmt = reclassification1.CuryCreditAmt;
        nullable2 = nullable1.HasValue & curyCreditAmt.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + curyCreditAmt.GetValueOrDefault()) : new Decimal?();
      }
      else
        nullable2 = reclassification1.CuryReclassRemainingAmt;
      reclassification2.CuryNewAmt = nullable2;
      reclassification1.IsSplitted = true;
      if (!ReclassifyTransactionsBase<ReclassifyTransactionsProcess>.IsReclassAttrChanged(reclassification1))
      {
        reclassification1.NewTranDate = reclassification1.TranDate;
        reclassification1.NewTranDesc = reclassification1.TranDesc;
      }
      ((PXSelectBase<GLTranForReclassification>) this.GLTranForReclass).Update(reclassification1);
    }
    else
      this.State.SplittingGroups[key].Add(glTranKey);
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable ValidateAndProcess(PXAdapter adapter)
  {
    this.ValidateSplitGroups();
    return ((PXGraph) this).Actions["Process"].Press(adapter);
  }

  private GLTranForReclassification SplitTransaction(
    PXCache cache,
    GLTranForReclassification originalTran)
  {
    ((PXSelectBase<PX.Objects.CM.CurrencyInfo>) this.currencyInfo).Current = PXResultset<PX.Objects.CM.CurrencyInfo>.op_Implicit(PXSelectBase<PX.Objects.CM.CurrencyInfo, PXSelect<PX.Objects.CM.CurrencyInfo, Where<PX.Objects.CM.CurrencyInfo.curyInfoID, Equal<Required<GLTran.curyInfoID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) originalTran.CuryInfoID
    }));
    GLTranKey glTranKey = new GLTranKey((GLTran) originalTran);
    GLTranForReclassification copy = (GLTranForReclassification) ((PXSelectBase) this.GLTranForReclass).Cache.CreateCopy((object) originalTran);
    copy.LineNbr = new int?(this.State.CurrentSplitLineNbr);
    copy.ParentKey = glTranKey;
    copy.CuryNewAmt = new Decimal?(0.0M);
    copy.SourceCuryDebitAmt = originalTran.CuryDebitAmt;
    copy.SourceCuryCreditAmt = originalTran.CuryCreditAmt;
    copy.CuryDebitAmt = new Decimal?(0M);
    copy.CuryCreditAmt = new Decimal?(0M);
    copy.NewBranchID = originalTran.BranchID;
    copy.NewAccountID = originalTran.AccountID;
    copy.NewSubID = originalTran.SubID;
    copy.NewTranDesc = originalTran.TranDesc;
    copy.NewTranDate = originalTran.TranDate;
    ReclassifyTransactionsProcess.SetUpProjectFieldsForSplitTransaction(originalTran, copy);
    copy.ReclassRowType = ReclassRowTypes.AddingNew;
    copy.IsSplitted = false;
    copy.Selected = new bool?(false);
    this.State.IncSplitLineNbr();
    ((PXSelectBase<PX.Objects.GL.Batch>) this.Batch).Current = PXParentAttribute.SelectParent<PX.Objects.GL.Batch>(cache, (object) originalTran);
    GLTranForReclassification splittedTran = ((PXSelectBase<GLTranForReclassification>) this.GLTranForReclass).Insert(copy);
    int? nullable = splittedTran != null ? splittedTran.ProjectID : throw new PXException("The splitting record has not been added because the unique key was duplicated. Please contact support service for assistance.");
    int? projectId = originalTran.ProjectID;
    if (!(nullable.GetValueOrDefault() == projectId.GetValueOrDefault() & nullable.HasValue == projectId.HasValue))
      ReclassifyTransactionsProcess.SetUpProjectFieldsForSplitTransaction(originalTran, splittedTran);
    ((PXSelectBase) this.GLTranForReclass).Cache.SetStatus((object) splittedTran, (PXEntryStatus) 1);
    return splittedTran;
  }

  private static void SetUpProjectFieldsForSplitTransaction(
    GLTranForReclassification originalTran,
    GLTranForReclassification splittedTran)
  {
    splittedTran.ProjectID = originalTran.ProjectID;
    splittedTran.TaskID = originalTran.TaskID;
    splittedTran.CostCodeID = originalTran.CostCodeID;
    splittedTran.NewProjectID = originalTran.ProjectID;
    splittedTran.NewTaskID = originalTran.TaskID;
    splittedTran.NewCostCodeID = originalTran.CostCodeID;
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable ViewDocument(PXAdapter adapter)
  {
    GLTranForReclassification current = ((PXSelectBase<GLTranForReclassification>) this.GLTranForReclass).Current;
    if (current != null)
    {
      PX.Objects.GL.Batch batch = JournalEntry.FindBatch((PXGraph) this, (GLTran) current);
      ((PXGraph) this).GetExtension<ReclassifyTransactionsProcess.RedirectToSourceDocumentFromReclassifyTransactionsProcessExtension>().RedirectToSourceDocument((GLTran) current, batch);
    }
    return adapter.Get();
  }

  private PXButtonState CreateReturnState(object returnState)
  {
    return PXButtonState.CreateInstance(returnState, (string) null, (string) null, (string) null, (string) null, (string) null, new bool?(false), (PXConfirmationType) 2, (string) null, new char?(), new bool?(), new bool?(), (ButtonMenu[]) null, new bool?(), new bool?(), new bool?(), (string) null, (string) null, (PXShortCutAttribute) null, typeof (GLTranForReclassification));
  }

  protected void LoadTransactionsProc(bool isReload = false)
  {
    IEnumerable<PXResult<GLTranForReclassification, PX.Objects.GL.Account, PX.Objects.GL.Sub, PX.Objects.GL.Batch, PX.Objects.CM.CurrencyInfo, PX.Objects.GL.Ledger>> reclassByLoadOptions = this.GetTransForReclassByLoadOptions(((PXSelectBase<ReclassifyTransactionsProcess.LoadOptions>) this.LoadOptionsView).Current);
    bool flag1 = false;
    bool flag2 = false;
    foreach (PXResult<GLTranForReclassification, PX.Objects.GL.Account, PX.Objects.GL.Sub, PX.Objects.GL.Batch, PX.Objects.CM.CurrencyInfo, PX.Objects.GL.Ledger> pxResult in reclassByLoadOptions)
    {
      GLTranForReclassification reclassification = PXResult<GLTranForReclassification, PX.Objects.GL.Account, PX.Objects.GL.Sub, PX.Objects.GL.Batch, PX.Objects.CM.CurrencyInfo, PX.Objects.GL.Ledger>.op_Implicit(pxResult);
      PX.Objects.CM.CurrencyInfo curyInfo = PXResult<GLTranForReclassification, PX.Objects.GL.Account, PX.Objects.GL.Sub, PX.Objects.GL.Batch, PX.Objects.CM.CurrencyInfo, PX.Objects.GL.Ledger>.op_Implicit(pxResult);
      PX.Objects.GL.Batch batch = PXResult<GLTranForReclassification, PX.Objects.GL.Account, PX.Objects.GL.Sub, PX.Objects.GL.Batch, PX.Objects.CM.CurrencyInfo, PX.Objects.GL.Ledger>.op_Implicit(pxResult);
      PX.Objects.GL.Ledger ledger = PXResult<GLTranForReclassification, PX.Objects.GL.Account, PX.Objects.GL.Sub, PX.Objects.GL.Batch, PX.Objects.CM.CurrencyInfo, PX.Objects.GL.Ledger>.op_Implicit(pxResult);
      if (JournalEntry.IsTransactionReclassifiable((GLTran) reclassification, batch.BatchType, ledger.BalanceType, ProjectDefaultAttribute.NonProject()))
      {
        if (!ReclassifyTransactionsProcess.IsReclassAttrChangedAndNotNull(reclassification) && !(reclassification.CuryNewAmt.GetValueOrDefault() != 0M) || isReload)
        {
          this.InitTranForReclassAdditionalFields(reclassification, curyInfo);
          this.InitOriginalAmountIfRepeatedReclassification(reclassification);
          if (isReload && reclassification.IsSplitted)
            reclassification.IsSplitted = false;
          reclassification.Selected = new bool?(false);
          ((PXSelectBase) this.GLTranForReclass).Cache.SetStatus((object) reclassification, (PXEntryStatus) 1);
          flag2 = true;
        }
      }
      else if (reclassification.ReclassRowType != ReclassRowTypes.Editing && reclassification.ReclassRowType != ReclassRowTypes.EditingVirtualParentTran && !reclassification.IsSplitted && !reclassification.IsSplitting)
      {
        flag1 = true;
        ((PXSelectBase) this.GLTranForReclass).Cache.Remove((object) reclassification);
      }
    }
    if (!flag2)
      throw new PXException("No transactions, for which the reclassification can be performed, have been found to match the specified criteria.");
    if (!flag1)
      return;
    ((PXSelectBase<GLTranForReclassification>) this.GLTranForReclass).Ask("Some transactions that match the specified selection criteria cannot be reclassified. These transactions will not be loaded.", (MessageButtons) 0);
  }

  protected virtual void SetPeriodDates(
    ReclassifyTransactionsProcess.LoadOptions loadOptions)
  {
    int? calendarOrganizationId = this.FinPeriodRepository.GetCalendarOrganizationID(loadOptions.BranchID, loadOptions.UseMasterCalendar);
    FinPeriod byId1 = this.FinPeriodRepository.FindByID(calendarOrganizationId, loadOptions.FromFinPeriodID);
    FinPeriod byId2 = this.FinPeriodRepository.FindByID(calendarOrganizationId, loadOptions.ToFinPeriodID);
    if (byId1 != null && byId2 != null)
    {
      ReclassifyTransactionsProcess.LoadOptions loadOptions1 = loadOptions;
      DateTime? nullable1 = byId1.StartDate;
      DateTime? nullable2 = byId2.StartDate;
      DateTime? nullable3 = (nullable1.HasValue & nullable2.HasValue ? (nullable1.GetValueOrDefault() <= nullable2.GetValueOrDefault() ? 1 : 0) : 0) != 0 ? byId1.StartDate : byId2.StartDate;
      loadOptions1.PeriodStartDate = nullable3;
      ReclassifyTransactionsProcess.LoadOptions loadOptions2 = loadOptions;
      nullable2 = byId2.EndDate;
      nullable1 = byId1.EndDate;
      DateTime? nullable4 = (nullable2.HasValue & nullable1.HasValue ? (nullable2.GetValueOrDefault() >= nullable1.GetValueOrDefault() ? 1 : 0) : 0) != 0 ? byId2.EndDate : byId1.EndDate;
      loadOptions2.PeriodEndDate = nullable4;
    }
    else if (byId1 != null || byId2 != null)
    {
      FinPeriod finPeriod = byId1 ?? byId2;
      loadOptions.PeriodStartDate = finPeriod.StartDate;
      loadOptions.PeriodEndDate = finPeriod.EndDate;
    }
    else
    {
      loadOptions.PeriodStartDate = new DateTime?();
      loadOptions.PeriodEndDate = new DateTime?();
    }
  }

  private bool IsAllowProcess(GLTranForReclassification tranForReclass)
  {
    bool flag1 = ReclassifyTransactionsBase<ReclassifyTransactionsProcess>.IsReclassAttrChanged(tranForReclass) || tranForReclass.IsSplitted;
    int? nullable1 = tranForReclass.NewProjectID;
    int? nullable2 = tranForReclass.ProjectID;
    if (!(nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue))
    {
      nullable2 = tranForReclass.NewProjectID;
      if (!nullable2.HasValue)
      {
        nullable2 = tranForReclass.NewProjectID;
        nullable1 = ProjectDefaultAttribute.NonProject();
        if (!(nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue))
          goto label_8;
      }
    }
    nullable1 = tranForReclass.NewTaskID;
    nullable2 = tranForReclass.TaskID;
    if (!(nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue))
    {
      nullable2 = tranForReclass.NewTaskID;
      if (!nullable2.HasValue)
      {
        nullable2 = tranForReclass.NewProjectID;
        nullable1 = ProjectDefaultAttribute.NonProject();
        if (!(nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue))
          goto label_8;
      }
    }
    nullable1 = tranForReclass.NewCostCodeID;
    nullable2 = tranForReclass.CostCodeID;
    if (!(nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue))
    {
      nullable2 = tranForReclass.NewCostCodeID;
      if (!nullable2.HasValue && PXAccess.FeatureInstalled<FeaturesSet.costCodes>())
        goto label_8;
    }
    int num1 = 1;
    goto label_10;
label_8:
    num1 = !PXAccess.FeatureInstalled<FeaturesSet.projectAccounting>() ? 1 : 0;
label_10:
    bool flag2 = num1 != 0;
    nullable2 = tranForReclass.NewBranchID;
    int num2;
    if (nullable2.HasValue)
    {
      nullable2 = tranForReclass.NewAccountID;
      if (nullable2.HasValue)
      {
        nullable2 = tranForReclass.NewSubID;
        if (nullable2.HasValue)
        {
          num2 = tranForReclass.NewTranDate.HasValue ? 1 : 0;
          goto label_15;
        }
      }
    }
    num2 = 0;
label_15:
    int num3 = flag2 ? 1 : 0;
    bool flag3 = (num2 & num3) != 0;
    bool flag4 = false;
    this.IsDateWithinPeriod(((PXSelectBase) this.GLTranForReclass).Cache, tranForReclass);
    foreach (System.Type editableField in ReclassifyTransactionsProcess.EditableFields)
    {
      flag4 = this.FieldHasError(tranForReclass, editableField.Name);
      if (flag4)
        break;
    }
    return flag1 & flag3 && !flag4;
  }

  public static bool IsReclassAttrChangedAndNotNull(GLTranForReclassification tranForReclass)
  {
    int? newBranchId = tranForReclass.NewBranchID;
    int? branchId = tranForReclass.BranchID;
    if (newBranchId.GetValueOrDefault() == branchId.GetValueOrDefault() & newBranchId.HasValue == branchId.HasValue || !tranForReclass.NewBranchID.HasValue)
    {
      int? newAccountId = tranForReclass.NewAccountID;
      int? accountId = tranForReclass.AccountID;
      if (newAccountId.GetValueOrDefault() == accountId.GetValueOrDefault() & newAccountId.HasValue == accountId.HasValue || !tranForReclass.NewBranchID.HasValue)
      {
        int? newSubId = tranForReclass.NewSubID;
        int? subId = tranForReclass.SubID;
        return !(newSubId.GetValueOrDefault() == subId.GetValueOrDefault() & newSubId.HasValue == subId.HasValue) && tranForReclass.NewBranchID.HasValue;
      }
    }
    return true;
  }

  private void CalcSelectedFieldValue(GLTranForReclassification tranForReclass)
  {
    if (tranForReclass.IsSplitted || tranForReclass.IsSplitting)
    {
      this.CalcGroupSelectedValues(tranForReclass);
    }
    else
    {
      bool flag = this.IsAllowProcess(tranForReclass);
      tranForReclass.Selected = new bool?(flag);
    }
  }

  private void CalcGroupSelectedValues(GLTranForReclassification tran)
  {
    GLTranKey key1 = tran.IsSplitting ? tran.ParentKey : new GLTranKey((GLTran) tran);
    GLTranForReclassification parent = ((PXSelectBase<GLTranForReclassification>) this.GLTranForReclass).Locate(this.GetGLTranForReclassByKey(key1));
    Decimal? nullable = parent.CuryDebitAmt;
    Decimal num1 = nullable.Value;
    nullable = parent.CuryCreditAmt;
    Decimal num2 = nullable.Value;
    Decimal num3 = num1 + num2;
    Decimal? curyDebitAmt = parent.CuryDebitAmt;
    Decimal? curyCreditAmt = parent.CuryCreditAmt;
    nullable = curyDebitAmt.HasValue & curyCreditAmt.HasValue ? new Decimal?(curyDebitAmt.GetValueOrDefault() + curyCreditAmt.GetValueOrDefault()) : new Decimal?();
    Decimal num4 = 0M;
    int num5 = nullable.GetValueOrDefault() > num4 & nullable.HasValue ? 1 : -1;
    bool flag1 = this.IsAllowProcess(parent);
    bool flag2 = ReclassifyTransactionsBase<ReclassifyTransactionsProcess>.IsReclassAttrChanged(parent);
    bool flag3 = true;
    bool flag4 = true;
    IReadOnlyCollection<GLTranKey> splittingGroup = (IReadOnlyCollection<GLTranKey>) this.State.SplittingGroups[key1];
    IReadOnlyCollection<GLTranKey> glTranKeys;
    if (tran.IsSplitting)
    {
      GLTranKey glTranKey = new GLTranKey((GLTran) tran);
      glTranKeys = (IReadOnlyCollection<GLTranKey>) EnumerableExtensions.Except<GLTranKey>((IEnumerable<GLTranKey>) splittingGroup, glTranKey).Union<GLTranKey>((IEnumerable<GLTranKey>) new List<GLTranKey>()
      {
        glTranKey
      }).ToList<GLTranKey>();
    }
    else
      glTranKeys = (IReadOnlyCollection<GLTranKey>) this.State.SplittingGroups[key1];
    Decimal num6 = 0M;
    bool flag5 = false;
    bool flag6 = true;
    List<GLTranForReclassification> source = new List<GLTranForReclassification>();
    foreach (GLTranKey key2 in (IEnumerable<GLTranKey>) glTranKeys)
    {
      GLTranForReclassification reclassification = ((PXSelectBase<GLTranForReclassification>) this.GLTranForReclass).Locate(this.GetGLTranForReclassByKey(key2));
      source.Add(reclassification);
    }
    foreach (GLTranForReclassification reclassification in source)
    {
      GLTranForReclassification split = reclassification;
      int num7 = this.IsAllowProcess(split) ? 1 : 0;
      bool flag7 = ReclassifyTransactionsBase<ReclassifyTransactionsProcess>.IsReclassAttrChanged(split);
      bool flag8 = this.IsDuplicate(split, parent) || source.Any<GLTranForReclassification>((Func<GLTranForReclassification, bool>) (m =>
      {
        int? lineNbr1 = m.LineNbr;
        int? lineNbr2 = split.LineNbr;
        return !(lineNbr1.GetValueOrDefault() == lineNbr2.GetValueOrDefault() & lineNbr1.HasValue == lineNbr2.HasValue) && this.IsDuplicate(m, split);
      }));
      Decimal num8 = num3;
      Decimal? curyNewAmt = split.CuryNewAmt;
      Decimal valueOrDefault1 = curyNewAmt.GetValueOrDefault();
      num3 = num8 - valueOrDefault1;
      Decimal num9 = num6;
      curyNewAmt = split.CuryNewAmt;
      Decimal valueOrDefault2 = curyNewAmt.GetValueOrDefault();
      num6 = num9 + valueOrDefault2;
      if ((Decimal) num5 * num3 < 0M && !flag5)
      {
        this.SetExhaustedAmountError(split);
        flag5 = true;
        flag6 = false;
      }
      else
        ((PXSelectBase) this.GLTranForReclass).Cache.RaiseExceptionHandling<GLTranForReclassification.curyNewAmt>((object) split, (object) split.CuryNewAmt, (Exception) null);
      int num10;
      if (((num7 == 0 ? 0 : (flag2 ? 1 : (!flag8 ? 1 : 0))) & (flag7 ? 1 : 0)) != 0)
      {
        curyNewAmt = split.CuryNewAmt;
        num10 = curyNewAmt.GetValueOrDefault() != 0M ? 1 : 0;
      }
      else
        num10 = 0;
      bool flag9 = num10 != 0;
      bool flag10 = flag9 & flag6;
      split.Selected = new bool?(flag10);
      flag4 &= flag9;
      flag3 &= flag10;
    }
    if (flag4 && (Decimal) num5 * (parent.CuryDebitAmt.Value + parent.CuryCreditAmt.Value - num6) >= 0M)
    {
      foreach (GLTranForReclassification reclassification in source)
      {
        ((PXSelectBase) this.GLTranForReclass).Cache.RaiseExceptionHandling<GLTranForReclassification.curyNewAmt>((object) reclassification, (object) reclassification.CuryNewAmt, (Exception) null);
        reclassification.Selected = new bool?(true);
      }
      flag3 = true;
    }
    bool flag11 = source.Any<GLTranForReclassification>((Func<GLTranForReclassification, bool>) (m => this.IsDuplicate(m, parent)));
    bool flag12 = ((!(flag3 | flag2) ? 0 : (!flag11 ? 1 : 0)) & (flag1 ? 1 : 0)) != 0;
    parent.Selected = new bool?(flag12);
    ((PXSelectBase) this.GLTranForReclass).View.RequestRefresh();
  }

  private bool IsDuplicate(GLTranForReclassification ltran, GLTranForReclassification rtran)
  {
    int? newBranchId1 = ltran.NewBranchID;
    int? newBranchId2 = rtran.NewBranchID;
    if (newBranchId1.GetValueOrDefault() == newBranchId2.GetValueOrDefault() & newBranchId1.HasValue == newBranchId2.HasValue)
    {
      int? newAccountId1 = ltran.NewAccountID;
      int? newAccountId2 = rtran.NewAccountID;
      if (newAccountId1.GetValueOrDefault() == newAccountId2.GetValueOrDefault() & newAccountId1.HasValue == newAccountId2.HasValue)
      {
        int? newSubId1 = ltran.NewSubID;
        int? newSubId2 = rtran.NewSubID;
        if (newSubId1.GetValueOrDefault() == newSubId2.GetValueOrDefault() & newSubId1.HasValue == newSubId2.HasValue)
          return this.IsDuplicatedProjectAttributes(ltran, rtran);
      }
    }
    return false;
  }

  private bool IsDuplicatedProjectAttributes(
    GLTranForReclassification ltran,
    GLTranForReclassification rtran)
  {
    int? newProjectId1 = ltran.NewProjectID;
    int? newProjectId2 = rtran.NewProjectID;
    if (newProjectId1.GetValueOrDefault() == newProjectId2.GetValueOrDefault() & newProjectId1.HasValue == newProjectId2.HasValue)
    {
      int? newTaskId1 = ltran.NewTaskID;
      int? newTaskId2 = rtran.NewTaskID;
      if (newTaskId1.GetValueOrDefault() == newTaskId2.GetValueOrDefault() & newTaskId1.HasValue == newTaskId2.HasValue)
      {
        int? newCostCodeId1 = ltran.NewCostCodeID;
        int? newCostCodeId2 = rtran.NewCostCodeID;
        return newCostCodeId1.GetValueOrDefault() == newCostCodeId2.GetValueOrDefault() & newCostCodeId1.HasValue == newCostCodeId2.HasValue;
      }
    }
    return false;
  }

  private bool FieldHasError(GLTranForReclassification tranForReclass, string fieldName)
  {
    return ((PXSelectBase) this.GLTranForReclass).Cache.GetAttributesReadonly((object) tranForReclass, fieldName).OfType<IPXInterfaceField>().Any<IPXInterfaceField>((Func<IPXInterfaceField, bool>) (attr => attr.ErrorText != null && attr.ErrorLevel == 4));
  }

  private bool IsTranDateAndDescEnabled(GLTranForReclassification tran)
  {
    return tran.IsSplitted && ReclassifyTransactionsBase<ReclassifyTransactionsProcess>.IsReclassAttrChanged(tran) || !tran.IsSplitted;
  }

  private IEnumerable<GLTranForReclassification> AddTransDescWhereConditionForReplace(
    IEnumerable<GLTranForReclassification> trans,
    string filteringValue)
  {
    if (((IEnumerable<string>) ReclassifyTransactionsProcess._emptyStringReplaceWildCards).Contains<string>(filteringValue))
      return trans.Where<GLTranForReclassification>((Func<GLTranForReclassification, bool>) (tran => string.IsNullOrEmpty(tran.NewTranDesc)));
    Regex regex = new Regex($"^{Regex.Escape(filteringValue).Replace("\\*", ".*").Replace("\\?", ".")}$", RegexOptions.IgnoreCase);
    return trans.Where<GLTranForReclassification>((Func<GLTranForReclassification, bool>) (tran => regex.IsMatch(tran.TranDesc)));
  }

  private IList<PXResult<GLTranForReclassification, PX.Objects.CM.CurrencyInfo>> RegetAndKeepInCacheOnlyReclassifiableTransByKey(
    IEnumerable<GLTran> trans)
  {
    List<PXResult<GLTranForReclassification, PX.Objects.CM.CurrencyInfo>> pxResultList = new List<PXResult<GLTranForReclassification, PX.Objects.CM.CurrencyInfo>>();
    foreach (GLTran tran1 in trans)
    {
      if (tran1.ReclassBatchNbr == null || tran1.ReclassBatchNbr != null && tran1.Reclassified.GetValueOrDefault() && tran1.CuryReclassRemainingAmt.GetValueOrDefault() != 0M)
      {
        PXResult<GLTranForReclassification, PX.Objects.CM.CurrencyInfo> pxResult = (PXResult<GLTranForReclassification, PX.Objects.CM.CurrencyInfo>) PXResultset<GLTranForReclassification>.op_Implicit(PXSelectBase<GLTranForReclassification, PXSelectJoin<GLTranForReclassification, InnerJoin<PX.Objects.CM.CurrencyInfo, On<GLTran.curyInfoID, Equal<PX.Objects.CM.CurrencyInfo.curyInfoID>>>, Where<GLTran.module, Equal<Required<GLTran.module>>, And<GLTran.batchNbr, Equal<Required<GLTran.batchNbr>>, And<GLTran.lineNbr, Equal<Required<GLTran.lineNbr>>>>>>.Config>.Select((PXGraph) this, new object[3]
        {
          (object) tran1.Module,
          (object) tran1.BatchNbr,
          (object) tran1.LineNbr
        }));
        GLTranForReclassification tran2 = PXResult<GLTranForReclassification, PX.Objects.CM.CurrencyInfo>.op_Implicit(pxResult);
        if (pxResult != null && (tran2.ReclassBatchNbr == null || tran2.ReclassBatchNbr != null && tran2.Reclassified.GetValueOrDefault() && tran2.CuryReclassRemainingAmt.GetValueOrDefault() != 0M) && !JournalEntry.HasUnreleasedReclassTran((GLTran) tran2))
          pxResultList.Add(pxResult);
        else
          this.TryRemoveTranForReclassFromCacheByKey(tran1);
      }
      else
        this.TryRemoveTranForReclassFromCacheByKey(tran1);
    }
    return (IList<PXResult<GLTranForReclassification, PX.Objects.CM.CurrencyInfo>>) pxResultList;
  }

  private void TryRemoveTranForReclassFromCacheByKey(GLTran tran)
  {
    GLTranForReclassification reclassification1 = new GLTranForReclassification();
    reclassification1.Module = tran.Module;
    reclassification1.BatchNbr = tran.BatchNbr;
    reclassification1.LineNbr = tran.LineNbr;
    GLTranForReclassification reclassification2 = ((PXSelectBase<GLTranForReclassification>) this.GLTranForReclass).Locate(reclassification1);
    if (reclassification2 == null)
      return;
    ((PXSelectBase) this.GLTranForReclass).Cache.Remove((object) reclassification2);
  }

  private void PutTransForReclassToCacheByKey(IEnumerable<GLTran> trans)
  {
    foreach (PXResult<GLTranForReclassification, PX.Objects.CM.CurrencyInfo> pxResult in (IEnumerable<PXResult<GLTranForReclassification, PX.Objects.CM.CurrencyInfo>>) this.RegetAndKeepInCacheOnlyReclassifiableTransByKey(trans))
    {
      GLTranForReclassification reclassification = PXResult<GLTranForReclassification, PX.Objects.CM.CurrencyInfo>.op_Implicit(pxResult);
      PX.Objects.CM.CurrencyInfo curyInfo = PXResult<GLTranForReclassification, PX.Objects.CM.CurrencyInfo>.op_Implicit(pxResult);
      this.InitOriginalAmountIfRepeatedReclassification(reclassification);
      this.InitTranForReclassAdditionalFields(reclassification, curyInfo);
      ((PXSelectBase) this.GLTranForReclass).Cache.SetStatus((object) reclassification, (PXEntryStatus) 1);
      this.CalcSelectedFieldValue(reclassification);
      this.IsDateWithinPeriod(((PXSelectBase) this.GLTranForReclass).Cache, reclassification);
    }
  }

  private void PutReclassificationBatchTransForEditingToCache(string module, string batchNbr)
  {
    IEnumerable<GLTran> reclassTypeSorted = this.GetTransReclassTypeSorted((PXGraph) this, module, batchNbr);
    Dictionary<GLTranKey, GLTranForReclassification> parentsList = new Dictionary<GLTranKey, GLTranForReclassification>();
    foreach (GLTran tranToEdit in reclassTypeSorted)
    {
      if (!tranToEdit.IsReclassReverse.GetValueOrDefault())
      {
        if (tranToEdit.ReclassType == "S")
          this.PutSplitAmountTransactions(parentsList, tranToEdit);
        if (tranToEdit.ReclassType == "C")
          this.PutCommonReclassifiedTransaction(parentsList, tranToEdit);
      }
    }
    foreach (KeyValuePair<GLTranKey, GLTranForReclassification> keyValuePair in parentsList)
    {
      this.CalcSelectedFieldValue(keyValuePair.Value);
      ((PXSelectBase) this.GLTranForReclass).Cache.RaiseRowSelected((object) keyValuePair.Value);
    }
  }

  private void PutCommonReclassifiedTransaction(
    Dictionary<GLTranKey, GLTranForReclassification> parentsList,
    GLTran tranToEdit)
  {
    GLTranForReclassification reclassification = PXResultset<GLTranForReclassification>.op_Implicit(((PXSelectBase<GLTranForReclassification>) this.GLTranForReclass_Module_BatchNbr_LineNbr).Select(new object[3]
    {
      (object) tranToEdit.OrigModule,
      (object) tranToEdit.OrigBatchNbr,
      (object) tranToEdit.OrigLineNbr
    }));
    ((PXSelectBase) this.GLTranForReclass).Cache.SetStatus((object) reclassification, (PXEntryStatus) 1);
    this.InitTranForReclassAdditionalFieldsForEditing(reclassification, tranToEdit);
    reclassification.EditingPairReclassifyingLineNbr = tranToEdit.LineNbr;
    GLTranKey key = new GLTranKey((GLTran) reclassification);
    if (parentsList.ContainsKey(key))
    {
      reclassification.IsSplitted = true;
      this.InitTranForReclassAdditionalFieldsForEditing(reclassification, tranToEdit);
      this.InitOriginalAmountIfRepeatedReclassification(reclassification);
    }
    else
      this.CalcSelectedFieldValue(reclassification);
    reclassification.ReclassRowType = ReclassRowTypes.Editing;
    ((PXSelectBase) this.GLTranForReclass).Cache.RaiseRowSelected((object) reclassification);
  }

  private void PutSplitAmountTransactions(
    Dictionary<GLTranKey, GLTranForReclassification> parentsList,
    GLTran tranToEdit)
  {
    GLTranKey glTranKey = new GLTranKey(tranToEdit.OrigModule, tranToEdit.OrigBatchNbr, tranToEdit.OrigLineNbr);
    GLTranForReclassification parent = (parentsList.ContainsKey(glTranKey) ? parentsList[glTranKey] : (GLTranForReclassification) null) ?? this.RegenerateParentOfGroup(parentsList, glTranKey);
    GLTranForReclassification reclassification = PXResultset<GLTranForReclassification>.op_Implicit(((PXSelectBase<GLTranForReclassification>) this.GLTranForReclass_Module_BatchNbr_LineNbr).Select(new object[3]
    {
      (object) tranToEdit.Module,
      (object) tranToEdit.BatchNbr,
      (object) tranToEdit.LineNbr
    }));
    ((PXSelectBase) this.GLTranForReclass).Cache.SetStatus((object) reclassification, (PXEntryStatus) 1);
    this.InitSplitTranForEditing(tranToEdit, glTranKey, parent, reclassification);
    this.State.SplittingGroups[glTranKey].Add(new GLTranKey((GLTran) reclassification));
    ((PXSelectBase) this.GLTranForReclass).Cache.RaiseFieldUpdated<GLTranForReclassification.curyNewAmt>((object) reclassification, (object) 0M);
    this.CalcSelectedFieldValue(reclassification);
    ((PXSelectBase) this.GLTranForReclass).Cache.RaiseRowSelected((object) reclassification);
  }

  private void InitSplitTranForEditing(
    GLTran tranToEdit,
    GLTranKey parentKey,
    GLTranForReclassification parent,
    GLTranForReclassification tranForReclass)
  {
    this.InitSplitTranForReclassAdditionalFieldsForEditing(tranForReclass, parent, tranToEdit);
    tranForReclass.EditingPairReclassifyingLineNbr = tranToEdit.LineNbr;
    GLTranForReclassification reclassification = tranForReclass;
    Decimal? curyDebitAmt = tranToEdit.CuryDebitAmt;
    Decimal? curyCreditAmt = tranToEdit.CuryCreditAmt;
    Decimal? nullable = curyDebitAmt.HasValue & curyCreditAmt.HasValue ? new Decimal?(curyDebitAmt.GetValueOrDefault() + curyCreditAmt.GetValueOrDefault()) : new Decimal?();
    reclassification.CuryNewAmt = nullable;
    tranForReclass.ParentKey = parentKey;
    tranForReclass.SourceCuryDebitAmt = tranForReclass.CuryDebitAmt;
    tranForReclass.SourceCuryCreditAmt = tranForReclass.CuryCreditAmt;
    tranForReclass.CuryDebitAmt = new Decimal?(0M);
    tranForReclass.CuryCreditAmt = new Decimal?(0M);
    tranForReclass.ReclassRowType = ReclassRowTypes.Editing;
  }

  private GLTranForReclassification RegenerateParentOfGroup(
    Dictionary<GLTranKey, GLTranForReclassification> parentsList,
    GLTranKey parentKey)
  {
    GLTranForReclassification reclassification1 = PXResultset<GLTranForReclassification>.op_Implicit(((PXSelectBase<GLTranForReclassification>) this.GLTranForReclass_Module_BatchNbr_LineNbr).Select(new object[3]
    {
      (object) parentKey.Module,
      (object) parentKey.BatchNbr,
      (object) parentKey.LineNbr
    }));
    this.State.SplittingGroups[parentKey] = new List<GLTranKey>();
    ((PXSelectBase) this.GLTranForReclass).Cache.SetStatus((object) reclassification1, (PXEntryStatus) 1);
    this.InitTranForReclassAdditionalFieldsForEditing(reclassification1, (GLTran) reclassification1);
    reclassification1.EditingPairReclassifyingLineNbr = reclassification1.LineNbr;
    GLTranForReclassification reclassification2 = reclassification1;
    Decimal? nullable;
    if (!(reclassification1.CuryReclassRemainingAmt.GetValueOrDefault() == 0M))
    {
      nullable = reclassification1.CuryReclassRemainingAmt;
    }
    else
    {
      Decimal? curyDebitAmt = reclassification1.CuryDebitAmt;
      Decimal? curyCreditAmt = reclassification1.CuryCreditAmt;
      nullable = curyDebitAmt.HasValue & curyCreditAmt.HasValue ? new Decimal?(curyDebitAmt.GetValueOrDefault() + curyCreditAmt.GetValueOrDefault()) : new Decimal?();
    }
    reclassification2.CuryNewAmt = nullable;
    this.InitOriginalAmountIfRepeatedReclassification(reclassification1);
    reclassification1.IsSplitted = true;
    reclassification1.ReclassRowType = ReclassRowTypes.EditingVirtualParentTran;
    this.State.SplittingGroups[parentKey] = new List<GLTranKey>();
    parentsList[parentKey] = reclassification1;
    this.CalcSelectedFieldValue(reclassification1);
    return reclassification1;
  }

  private void PutReclassificationBatchTransForReversingToCache(
    string module,
    string batchNbr,
    string curyID)
  {
    IEnumerable<GLTranForReclassification> reclassifications = GraphHelper.RowCast<GLTranForReclassification>((IEnumerable) ((PXSelectBase<GLTranForReclassification>) this.GLTransForReclassForReverseView).Select(new object[2]
    {
      (object) module,
      (object) batchNbr
    }));
    Dictionary<int?, GLTran> dictionary = GraphHelper.RowCast<GLTran>((IEnumerable) ((PXSelectBase<GLTran>) this.ReclassReverseGLTransView).Select(new object[2]
    {
      (object) module,
      (object) batchNbr
    })).ToDictionary<GLTran, int?, GLTran>((Func<GLTran, int?>) (tran => tran.LineNbr), (Func<GLTran, GLTran>) (tran => tran));
    foreach (GLTranForReclassification tranForReclass in reclassifications)
    {
      GLTran tran = dictionary[new int?(tranForReclass.LineNbr.Value - 1)];
      this.InitTranForReclassEditableFieldsFromTran(tranForReclass, tran);
      tranForReclass.CuryID = curyID;
      ((PXSelectBase) this.GLTranForReclass).Cache.SetStatus((object) tranForReclass, (PXEntryStatus) 1);
      this.CalcSelectedFieldValue(tranForReclass);
    }
  }

  public static void TryOpenForReclassificationOfDocument(
    PXView askView,
    string module,
    string batchNbr,
    string docType,
    string refNbr)
  {
    ReclassifyTransactionsProcess instance = PXGraph.CreateInstance<ReclassifyTransactionsProcess>();
    IEnumerable<PXResult<GLTranForReclassification, PX.Objects.CM.CurrencyInfo>> pxResults = EnumerableEx.Select<PXResult<GLTranForReclassification, PX.Objects.CM.CurrencyInfo>>((IEnumerable) PXSelectBase<GLTranForReclassification, PXSelectJoin<GLTranForReclassification, InnerJoin<PX.Objects.CM.CurrencyInfo, On<GLTran.curyInfoID, Equal<PX.Objects.CM.CurrencyInfo.curyInfoID>>>, Where<GLTran.module, Equal<Required<GLTran.module>>, And<GLTran.batchNbr, Equal<Required<GLTran.batchNbr>>, And<GLTran.tranType, Equal<Required<GLTran.tranType>>, And<GLTran.refNbr, Equal<GLTran.refNbr>>>>>>.Config>.Select((PXGraph) instance, new object[4]
    {
      (object) module,
      (object) batchNbr,
      (object) docType,
      (object) refNbr
    }));
    bool flag1 = false;
    bool flag2 = false;
    foreach (PXResult<GLTranForReclassification, PX.Objects.CM.CurrencyInfo> pxResult in pxResults)
    {
      GLTranForReclassification tran = PXResult<GLTranForReclassification, PX.Objects.CM.CurrencyInfo>.op_Implicit(pxResult);
      PX.Objects.CM.CurrencyInfo curyInfo = PXResult<GLTranForReclassification, PX.Objects.CM.CurrencyInfo>.op_Implicit(pxResult);
      if (JournalEntry.IsTransactionReclassifiable((GLTran) tran, (string) null, (string) null, ProjectDefaultAttribute.NonProject()) && ReclassifyTransactionsProcess.IsTransactionReclassifiableByProjectSettings((PXGraph) instance, (GLTran) tran, ProjectDefaultAttribute.NonProject()))
      {
        instance.InitTranForReclassAdditionalFields(tran, curyInfo);
        ((PXSelectBase) instance.GLTranForReclass).Cache.SetStatus((object) tran, (PXEntryStatus) 1);
        flag2 = true;
      }
      else
      {
        flag1 = true;
        ((PXSelectBase) instance.GLTranForReclass).Cache.Remove((object) tran);
      }
    }
    if (!flag2)
      throw new PXException("No transactions, for which the reclassification can be performed, have been found in the batch.");
    if (flag1)
      askView.Ask("Some transactions of the batch cannot be reclassified. These transactions will not be loaded.", (MessageButtons) 0);
    PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, true, string.Empty);
    ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 2;
    throw requiredException;
  }

  public static void TryOpenForReclassification<TTran>(
    PXGraph graph,
    IEnumerable<TTran> trans,
    PX.Objects.GL.Ledger ledger,
    Func<TTran, string> getBatchTypeDelegate,
    PXView askView,
    string someTransactionsCannotBeReclassifiedMessage,
    string noTransactionsForWhichTheReclassificationCanBePerformed,
    PXBaseRedirectException.WindowMode redirectMode = 2)
    where TTran : GLTran
  {
    bool flag = false;
    List<TTran> trans1 = new List<TTran>();
    foreach (TTran tran in trans)
    {
      if ((!JournalEntry.IsTransactionReclassifiable((GLTran) tran, getBatchTypeDelegate(tran), ledger?.BalanceType, ProjectDefaultAttribute.NonProject()) ? 0 : (ReclassifyTransactionsProcess.IsTransactionReclassifiableByProjectSettings(graph, (GLTran) tran, ProjectDefaultAttribute.NonProject()) ? 1 : 0)) != 0)
        trans1.Add(tran);
      else
        flag = true;
    }
    if (trans1.Count == 0)
      throw new PXException(noTransactionsForWhichTheReclassificationCanBePerformed);
    if (flag)
      askView.Ask(someTransactionsCannotBeReclassifiedMessage, (MessageButtons) 0);
    ReclassifyTransactionsProcess.OpenForReclassification((IReadOnlyCollection<GLTran>) trans1, redirectMode);
  }

  public static bool IsTransactionReclassifiableByProjectSettings(
    PXGraph graph,
    GLTran tran,
    int? nonProjectID)
  {
    int? projectId = tran.ProjectID;
    int? nullable = nonProjectID;
    if (projectId.GetValueOrDefault() == nullable.GetValueOrDefault() & projectId.HasValue == nullable.HasValue || !tran.ProjectID.HasValue || PMProject.PK.Find(graph, tran.ProjectID)?.BaseType == "C")
      return true;
    if (!PXAccess.FeatureInstalled<FeaturesSet.projectAccounting>())
      return false;
    return ((IQueryable<PXResult<PMProject>>) PXSelectBase<PMProject, PXViewOf<PMProject>.BasedOn<SelectFromBase<PMProject, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<PMTran>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMTran.tranID, Equal<P.AsInt>>>>>.And<BqlOperand<PMTran.projectID, IBqlInt>.IsEqual<PMProject.contractID>>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMProject.contractID, Equal<P.AsInt>>>>, And<BqlOperand<PMProject.isActive, IBqlBool>.IsEqual<True>>>, And<BqlOperand<PMProject.status, IBqlString>.IsEqual<ProjectStatus.active>>>>.And<Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMTran.tranID, IsNull>>>, Or<BqlOperand<PMTran.allocated, IBqlBool>.IsEqual<False>>>>.And<BqlOperand<PMTran.billed, IBqlBool>.IsEqual<False>>>>>>.Config>.Select(graph, new object[2]
    {
      (object) tran.PMTranID,
      (object) tran.ProjectID
    })).Any<PXResult<PMProject>>();
  }

  public static void TryOpenForReclassification<TTran>(
    PXGraph graph,
    IEnumerable<TTran> trans,
    string batchType,
    PXView askView,
    string someTransactionsCannotBeReclassifiedMessage,
    string noTransactionsForWhichTheReclassificationCanBePerformed,
    PXBaseRedirectException.WindowMode redirectMode = 2)
    where TTran : GLTran
  {
    ReclassifyTransactionsProcess.TryOpenForReclassification<TTran>(graph, trans, (PX.Objects.GL.Ledger) null, (Func<TTran, string>) (tran => batchType), askView, someTransactionsCannotBeReclassifiedMessage, noTransactionsForWhichTheReclassificationCanBePerformed, redirectMode);
  }

  public static void OpenForReclassification(
    IReadOnlyCollection<GLTran> trans,
    PXBaseRedirectException.WindowMode redirectMode = 2)
  {
    if (trans == null)
      throw new ArgumentNullException(nameof (trans));
    ReclassifyTransactionsProcess instance = PXGraph.CreateInstance<ReclassifyTransactionsProcess>();
    instance.State.ReclassScreenMode = ReclassScreenMode.Reclassification;
    instance.PutTransForReclassToCacheByKey((IEnumerable<GLTran>) trans);
    PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, true, string.Empty);
    ((PXBaseRedirectException) requiredException).Mode = redirectMode;
    throw requiredException;
  }

  public static void OpenForReclassBatchEditing(PX.Objects.GL.Batch batch)
  {
    ReclassifyTransactionsProcess instance = PXGraph.CreateInstance<ReclassifyTransactionsProcess>();
    instance.State.ReclassScreenMode = ReclassScreenMode.Editing;
    instance.State.EditingBatchModule = batch.Module;
    instance.State.EditingBatchNbr = batch.BatchNbr;
    instance.State.EditingBatchMasterPeriodID = batch.TranPeriodID;
    instance.State.EditingBatchCuryID = batch.CuryID;
    instance.PutReclassificationBatchTransForEditingToCache(batch.Module, batch.BatchNbr);
    PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, true, string.Empty);
    ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 1;
    throw requiredException;
  }

  public static void OpenForReclassBatchReversing(PX.Objects.GL.Batch batch)
  {
    ReclassifyTransactionsProcess instance = PXGraph.CreateInstance<ReclassifyTransactionsProcess>();
    instance.State.ReclassScreenMode = ReclassScreenMode.Reversing;
    instance.State.OrigBatchModuleToReverse = batch.Module;
    instance.State.OrigBatchNbrToReverse = batch.BatchNbr;
    instance.PutReclassificationBatchTransForReversingToCache(batch.Module, batch.BatchNbr, batch.CuryID);
    PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, true, string.Empty);
    ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 1;
    throw requiredException;
  }

  private void InitTranForReclassAdditionalFieldsForEditing(
    GLTranForReclassification tranForReclass,
    GLTran tran)
  {
    this.InitTranForReclassEditableFieldsFromTran(tranForReclass, tran);
    tranForReclass.CuryID = this.State.EditingBatchCuryID;
  }

  private void InitSplitTranForReclassAdditionalFieldsForEditing(
    GLTranForReclassification splitForReclass,
    GLTranForReclassification parentForReclass,
    GLTran tran)
  {
    splitForReclass.BranchID = parentForReclass.BranchID;
    splitForReclass.AccountID = parentForReclass.AccountID;
    splitForReclass.SubID = parentForReclass.SubID;
    splitForReclass.TranDate = parentForReclass.TranDate;
    splitForReclass.FinPeriodID = parentForReclass.FinPeriodID;
    splitForReclass.TranPeriodID = parentForReclass.TranPeriodID;
    splitForReclass.TranDesc = parentForReclass.TranDesc;
    splitForReclass.CuryDebitAmt = parentForReclass.CuryDebitAmt;
    splitForReclass.CuryCreditAmt = parentForReclass.CuryCreditAmt;
    this.InitTranForReclassEditableFieldsFromTran(splitForReclass, tran);
    splitForReclass.CuryID = this.State.EditingBatchCuryID;
  }

  private void InitTranForReclassEditableFieldsFromTran(
    GLTranForReclassification tranForReclass,
    GLTran tran)
  {
    tranForReclass.NewBranchID = tran.BranchID;
    tranForReclass.NewAccountID = tran.AccountID;
    tranForReclass.NewSubID = tran.SubID;
    tranForReclass.NewSubCD = (string) null;
    tranForReclass.NewTranDate = tran.TranDate;
    tranForReclass.NewFinPeriodID = tran.FinPeriodID;
    tranForReclass.NewTranDesc = tran.TranDesc;
    this.InitTranForReclassEditableProjectFieldsFromTran(tranForReclass, tran);
  }

  private void InitTranForReclassEditableProjectFieldsFromTran(
    GLTranForReclassification tranForReclass,
    GLTran tran)
  {
    tranForReclass.NewProjectID = tran.ProjectID;
    tranForReclass.NewTaskID = tran.TaskID;
    tranForReclass.NewCostCodeID = tran.CostCodeID;
  }

  private void InitTranForReclassEditableFields(GLTranForReclassification tranForReclass)
  {
    tranForReclass.NewFinPeriodID = tranForReclass.FinPeriodID;
    tranForReclass.NewTranDate = tranForReclass.TranDate;
    tranForReclass.NewBranchID = tranForReclass.BranchID;
    tranForReclass.NewAccountID = tranForReclass.AccountID;
    tranForReclass.NewSubID = tranForReclass.SubID;
    tranForReclass.NewSubCD = (string) null;
    tranForReclass.NewTranDesc = tranForReclass.TranDesc;
    tranForReclass.NewProjectID = ProjectDefaultAttribute.IsProjectOrNonProject((PXGraph) this, tranForReclass.ProjectID) ? tranForReclass.ProjectID : new int?();
    tranForReclass.NewTaskID = tranForReclass.TaskID;
    tranForReclass.NewCostCodeID = tranForReclass.CostCodeID;
    if (this.State.SplittingGroups.ContainsKey(new GLTranKey((GLTran) tranForReclass)))
      return;
    Decimal? curyNewAmt = tranForReclass.CuryNewAmt;
    tranForReclass.CuryNewAmt = new Decimal?(0M);
    ((PXSelectBase) this.GLTranForReclass).Cache.RaiseFieldUpdated<GLTranForReclassification.curyNewAmt>((object) tranForReclass, (object) curyNewAmt);
  }

  private void InitTranForReclassAdditionalFields(
    GLTranForReclassification tran,
    PX.Objects.CM.CurrencyInfo curyInfo)
  {
    this.InitTranForReclassEditableFields(tran);
    tran.CuryID = curyInfo.CuryID;
  }

  private void InitOriginalAmountIfRepeatedReclassification(GLTranForReclassification tran)
  {
    if (tran.CuryReclassRemainingAmt.GetValueOrDefault() == 0M)
      return;
    GLTranForReclassification reclassification1 = tran;
    Decimal? curyDebitAmt = tran.CuryDebitAmt;
    Decimal num1 = 0M;
    Decimal? nullable1 = !(curyDebitAmt.GetValueOrDefault() == num1 & curyDebitAmt.HasValue) ? tran.CuryReclassRemainingAmt : new Decimal?(0M);
    reclassification1.CuryDebitAmt = nullable1;
    GLTranForReclassification reclassification2 = tran;
    Decimal? curyCreditAmt = tran.CuryCreditAmt;
    Decimal num2 = 0M;
    Decimal? nullable2 = !(curyCreditAmt.GetValueOrDefault() == num2 & curyCreditAmt.HasValue) ? tran.CuryReclassRemainingAmt : new Decimal?(0M);
    reclassification2.CuryCreditAmt = nullable2;
  }

  private IEnumerable<PXResult<GLTranForReclassification, PX.Objects.GL.Account, PX.Objects.GL.Sub, PX.Objects.GL.Batch, PX.Objects.CM.CurrencyInfo, PX.Objects.GL.Ledger>> GetTransForReclassByLoadOptions(
    ReclassifyTransactionsProcess.LoadOptions loadOptions)
  {
    PXSelectBase<GLTranForReclassification> query = (PXSelectBase<GLTranForReclassification>) new PXSelectJoin<GLTranForReclassification, InnerJoin<PX.Objects.GL.Account, On<GLTran.accountID, Equal<PX.Objects.GL.Account.accountID>>, InnerJoin<PX.Objects.GL.Sub, On<GLTran.subID, Equal<PX.Objects.GL.Sub.subID>>, InnerJoin<PX.Objects.GL.Batch, On<PX.Objects.GL.Batch.module, Equal<GLTran.module>, And<PX.Objects.GL.Batch.batchNbr, Equal<GLTran.batchNbr>>>, InnerJoin<PX.Objects.CM.CurrencyInfo, On<GLTran.curyInfoID, Equal<PX.Objects.CM.CurrencyInfo.curyInfoID>>, InnerJoin<PX.Objects.GL.Ledger, On<GLTran.ledgerID, Equal<PX.Objects.GL.Ledger.ledgerID>>, LeftJoin<PMProject, On<PMProject.contractID, Equal<GLTran.projectID>>, LeftJoin<PMTask, On<PMTask.taskID, Equal<GLTran.taskID>>, LeftJoin<PMCostCode, On<PMCostCode.costCodeID, Equal<GLTran.costCodeID>>, LeftJoin<PMTran, On<PMTran.tranID, Equal<GLTran.pMTranID>>>>>>>>>>>, Where<PMProject.contractID, IsNull, Or<PMProject.nonProject, Equal<True>, Or<PMProject.isActive, Equal<True>, And<PMProject.status, Equal<ProjectStatus.active>, And<Where<PMTran.tranID, IsNull, Or<PMTran.allocated, NotEqual<True>, And<PMTran.billed, NotEqual<True>>>>>>>>>, OrderBy<Asc<GLTran.module, Asc<GLTran.batchNbr, Asc<GLTran.lineNbr>>>>>((PXGraph) this);
    List<object> pars = new List<object>();
    if (loadOptions.BranchID.HasValue)
    {
      query.WhereAnd<Where<GLTranForReclassification.branchID, Equal<Required<GLTranForReclassification.branchID>>>>();
      pars.Add((object) loadOptions.BranchID);
    }
    if (loadOptions.FromAccountID.HasValue)
    {
      PX.Objects.GL.Account account = AccountAttribute.GetAccount((PXGraph) this, loadOptions.FromAccountID);
      query.WhereAnd<Where<PX.Objects.GL.Account.accountCD, GreaterEqual<Required<PX.Objects.GL.Account.accountCD>>>>();
      pars.Add((object) account.AccountCD);
    }
    if (loadOptions.ToAccountID.HasValue)
    {
      PX.Objects.GL.Account account = AccountAttribute.GetAccount((PXGraph) this, loadOptions.ToAccountID);
      query.WhereAnd<Where<PX.Objects.GL.Account.accountCD, LessEqual<Required<PX.Objects.GL.Account.accountCD>>>>();
      pars.Add((object) account.AccountCD);
    }
    if (loadOptions.FromSubID.HasValue)
    {
      PX.Objects.GL.Sub subaccount = SubAccountAttribute.GetSubaccount((PXGraph) this, loadOptions.FromSubID);
      query.WhereAnd<Where<PX.Objects.GL.Sub.subCD, GreaterEqual<Required<PX.Objects.GL.Sub.subCD>>>>();
      pars.Add((object) subaccount.SubCD);
    }
    if (loadOptions.ToSubID.HasValue)
    {
      PX.Objects.GL.Sub subaccount = SubAccountAttribute.GetSubaccount((PXGraph) this, loadOptions.ToSubID);
      query.WhereAnd<Where<PX.Objects.GL.Sub.subCD, LessEqual<Required<PX.Objects.GL.Sub.subCD>>>>();
      pars.Add((object) subaccount.SubCD);
    }
    if (loadOptions.FromDate.HasValue)
      query.WhereAnd<Where<GLTran.tranDate, GreaterEqual<Current<ReclassifyTransactionsProcess.LoadOptions.fromDate>>>>();
    if (loadOptions.ToDate.HasValue)
      query.WhereAnd<Where<GLTran.tranDate, LessEqual<Current<ReclassifyTransactionsProcess.LoadOptions.toDate>>>>();
    if (loadOptions.FromFinPeriodID != null)
      query.WhereAnd<Where<GLTran.finPeriodID, GreaterEqual<Current<ReclassifyTransactionsProcess.LoadOptions.fromFinPeriodID>>>>();
    if (loadOptions.ToFinPeriodID != null)
      query.WhereAnd<Where<GLTran.finPeriodID, LessEqual<Current<ReclassifyTransactionsProcess.LoadOptions.toFinPeriodID>>>>();
    if (loadOptions.Module != null)
      query.WhereAnd<Where<GLTran.module, Equal<Current<ReclassifyTransactionsProcess.LoadOptions.module>>>>();
    if (loadOptions.BatchNbr != null)
      query.WhereAnd<Where<GLTran.batchNbr, Equal<Current<ReclassifyTransactionsProcess.LoadOptions.batchNbr>>>>();
    if (loadOptions.RefNbr != null)
      query.WhereAnd<Where<GLTran.refNbr, Equal<Current<ReclassifyTransactionsProcess.LoadOptions.refNbr>>>>();
    if (loadOptions.ReferenceID.HasValue)
      query.WhereAnd<Where<GLTranForReclassification.referenceID, Equal<Current<ReclassifyTransactionsProcess.LoadOptions.referenceID>>>>();
    this.OptionallyApplyProjectOptions(loadOptions, query, pars);
    return EnumerableEx.Select<PXResult<GLTranForReclassification, PX.Objects.GL.Account, PX.Objects.GL.Sub, PX.Objects.GL.Batch, PX.Objects.CM.CurrencyInfo, PX.Objects.GL.Ledger>>(!loadOptions.MaxTrans.HasValue ? (IEnumerable) query.Select(pars.ToArray()) : (IEnumerable) query.SelectWindowed(0, loadOptions.MaxTrans.Value, pars.ToArray()));
  }

  private void OptionallyApplyProjectOptions(
    ReclassifyTransactionsProcess.LoadOptions loadOptions,
    PXSelectBase<GLTranForReclassification> query,
    List<object> pars)
  {
    if (loadOptions.ProjectID.HasValue)
    {
      query.WhereAnd<Where<PMProject.contractID, Equal<Required<PMProject.contractID>>>>();
      pars.Add((object) loadOptions.ProjectID);
    }
    if (loadOptions.FromTaskID.HasValue)
    {
      PMTask dirty = PMTask.PK.FindDirty((PXGraph) this, loadOptions.ProjectID, loadOptions.FromTaskID);
      query.WhereAnd<Where<PMTask.taskCD, GreaterEqual<Required<PMTask.taskCD>>>>();
      pars.Add((object) dirty.TaskCD);
    }
    if (loadOptions.ToTaskID.HasValue)
    {
      PMTask dirty = PMTask.PK.FindDirty((PXGraph) this, loadOptions.ProjectID, loadOptions.ToTaskID);
      query.WhereAnd<Where<PMTask.taskCD, LessEqual<Required<PMTask.taskCD>>>>();
      pars.Add((object) dirty.TaskCD);
    }
    if (loadOptions.FromCostCodeID.HasValue)
    {
      PMCostCode pmCostCode = PMCostCode.PK.Find((PXGraph) this, loadOptions.FromCostCodeID);
      query.WhereAnd<Where<PMCostCode.costCodeCD, GreaterEqual<Required<PMCostCode.costCodeCD>>>>();
      pars.Add((object) pmCostCode.CostCodeCD);
    }
    if (!loadOptions.ToCostCodeID.HasValue)
      return;
    PMCostCode pmCostCode1 = PMCostCode.PK.Find((PXGraph) this, loadOptions.ToCostCodeID);
    query.WhereAnd<Where<PMCostCode.costCodeCD, LessEqual<Required<PMCostCode.costCodeCD>>>>();
    pars.Add((object) pmCostCode1.CostCodeCD);
  }

  private void ValidateSplitGroups()
  {
    string str = string.Empty;
    foreach (KeyValuePair<GLTranKey, List<GLTranKey>> splittingGroup in this.State.SplittingGroups)
    {
      GLTranForReclassification row = ((PXSelectBase<GLTranForReclassification>) this.GLTranForReclass).Locate(this.GetGLTranForReclassByKey(splittingGroup.Key));
      Decimal num1 = row.CuryDebitAmt.Value + row.CuryCreditAmt.Value;
      str = this.ValidateSplitRecord(row) ?? str;
      Decimal? curyDebitAmt = row.CuryDebitAmt;
      Decimal? curyCreditAmt = row.CuryCreditAmt;
      Decimal? nullable = curyDebitAmt.HasValue & curyCreditAmt.HasValue ? new Decimal?(curyDebitAmt.GetValueOrDefault() + curyCreditAmt.GetValueOrDefault()) : new Decimal?();
      Decimal num2 = 0M;
      int num3 = nullable.GetValueOrDefault() > num2 & nullable.HasValue ? 1 : -1;
      foreach (GLTranKey key in splittingGroup.Value)
      {
        GLTranForReclassification reclassification = ((PXSelectBase<GLTranForReclassification>) this.GLTranForReclass).Locate(this.GetGLTranForReclassByKey(key));
        str = this.ValidateSplitRecord(reclassification) ?? str;
        Decimal num4 = num1;
        nullable = reclassification.CuryNewAmt;
        Decimal valueOrDefault = nullable.GetValueOrDefault();
        num1 = num4 - valueOrDefault;
        if ((Decimal) num3 * num1 < 0M)
        {
          str = "The total amount of split lines cannot be more than the original amount.";
          this.SetExhaustedAmountError(reclassification);
        }
      }
      if (!string.IsNullOrEmpty(str))
        throw new PXException(str);
    }
  }

  private void SetExhaustedAmountError(GLTranForReclassification tran)
  {
    PXSetPropertyException propertyException = new PXSetPropertyException("The total amount of split lines cannot be more than the original amount.", (PXErrorLevel) 4);
    ((PXSelectBase) this.GLTranForReclass).Cache.RaiseExceptionHandling<GLTranForReclassification.curyNewAmt>((object) tran, (object) tran.CuryNewAmt, (Exception) propertyException);
  }

  private string ValidateSplitRecord(GLTranForReclassification row)
  {
    if (!row.IsSplitting && !row.IsSplitted)
      return (string) null;
    bool? selected = row.Selected;
    bool flag = false;
    if (selected.GetValueOrDefault() == flag & selected.HasValue)
    {
      PXSetPropertyException propertyException = new PXSetPropertyException("Because not all lines of the splitting group have been selected, processing is impossible.", (PXErrorLevel) 5);
      ((PXSelectBase) this.GLTranForReclass).Cache.RaiseExceptionHandling(typeof (GLTran.selected).Name, (object) row, (object) row.Selected, (Exception) propertyException);
      return "Because not all lines of the splitting group have been selected, processing is impossible.";
    }
    ((PXSelectBase) this.GLTranForReclass).Cache.RaiseExceptionHandling(typeof (GLTran.selected).Name, (object) row, (object) row.Selected, (Exception) null);
    return (string) null;
  }

  public class RedirectToSourceDocumentFromReclassifyTransactionsProcessExtension : 
    RedirectToSourceDocumentExtensionBase<ReclassifyTransactionsProcess>
  {
    public static bool IsActive() => true;
  }

  public class LoadOptions : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected int? _BranchID;
    protected int? _FromAccountID;
    protected int? _ToAccountID;
    protected int? _FromSubID;
    protected int? _ToSubID;
    protected DateTime? _FromDate;
    protected DateTime? _ToDate;
    protected string _FromFinPeriodID;
    protected string _ToFinPeriodID;
    protected string _Module;
    protected string _BatchNbr;
    protected string _RefNbr;
    protected int? _ReferenceID;
    protected int? _MaxTrans;
    protected DateTime? _PeriodStartDate;
    protected DateTime? _PeriodEndDate;

    [Branch(null, null, true, true, true, IsDBField = false, Required = false)]
    public virtual int? BranchID
    {
      get => this._BranchID;
      set => this._BranchID = value;
    }

    [AccountAny(DisplayName = "From Account")]
    public virtual int? FromAccountID
    {
      get => this._FromAccountID;
      set => this._FromAccountID = value;
    }

    [AccountAny(DisplayName = "To Account")]
    public virtual int? ToAccountID
    {
      get => this._ToAccountID;
      set => this._ToAccountID = value;
    }

    [SubAccount(DisplayName = "From Subaccount")]
    public virtual int? FromSubID
    {
      get => this._FromSubID;
      set => this._FromSubID = value;
    }

    [SubAccount(DisplayName = "To Subaccount")]
    public virtual int? ToSubID
    {
      get => this._ToSubID;
      set => this._ToSubID = value;
    }

    [PXDBDate]
    [PXUIField(DisplayName = "From Date")]
    public virtual DateTime? FromDate
    {
      get => this._FromDate;
      set => this._FromDate = value;
    }

    [PXDBDate]
    [PXUIField(DisplayName = "To Date")]
    public virtual DateTime? ToDate
    {
      get => this._ToDate;
      set => this._ToDate = value;
    }

    [FinPeriodSelector(null, typeof (AccessInfo.businessDate), typeof (ReclassifyTransactionsProcess.LoadOptions.branchID), null, null, typeof (ReclassifyTransactionsProcess.LoadOptions.useMasterCalendar), null, false, false, false, false, true, FinPeriodSelectorAttribute.SelectionModesWithRestrictions.Undefined, null, null, null, true)]
    [PXUIField(DisplayName = "From Period", Required = false)]
    public virtual string FromFinPeriodID
    {
      get => this._FromFinPeriodID;
      set => this._FromFinPeriodID = value;
    }

    [FinPeriodSelector(null, typeof (AccessInfo.businessDate), typeof (ReclassifyTransactionsProcess.LoadOptions.branchID), null, null, typeof (ReclassifyTransactionsProcess.LoadOptions.useMasterCalendar), null, false, false, false, false, true, FinPeriodSelectorAttribute.SelectionModesWithRestrictions.Undefined, null, null, null, true)]
    [PXUIField(DisplayName = "To Period", Required = false)]
    public virtual string ToFinPeriodID
    {
      get => this._ToFinPeriodID;
      set => this._ToFinPeriodID = value;
    }

    [PXDBString(2, IsFixed = true)]
    [PXUIField]
    [ReclassifyTransactionsProcess.LoadOptions.ModuleList]
    public virtual string Module
    {
      get => this._Module;
      set => this._Module = value;
    }

    [PXDBString(15, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
    [PXSelector(typeof (Search<PX.Objects.GL.Batch.batchNbr, Where<PX.Objects.GL.Batch.module, Equal<Current<ReclassifyTransactionsProcess.LoadOptions.module>>, And<PX.Objects.GL.Batch.draft, Equal<False>>>, OrderBy<Desc<PX.Objects.GL.Batch.batchNbr>>>), Filterable = true)]
    [PXUIField]
    public virtual string BatchNbr
    {
      get => this._BatchNbr;
      set => this._BatchNbr = value;
    }

    [PXDBString(15, IsUnicode = true)]
    [PXUIField(DisplayName = "Ref. Number")]
    public virtual string RefNbr
    {
      get => this._RefNbr;
      set => this._RefNbr = value;
    }

    [PXDBInt]
    [PXSelector(typeof (Search<BAccountR.bAccountID>), SubstituteKey = typeof (BAccountR.acctCD))]
    [PXUIField(DisplayName = "Customer/Vendor")]
    [CustomerVendorRestrictor]
    public virtual int? ReferenceID
    {
      get => this._ReferenceID;
      set => this._ReferenceID = value;
    }

    [ActiveProject(DisplayName = "Project", IsDBField = false)]
    public virtual int? ProjectID { get; set; }

    [BaseProjectTask(typeof (ReclassifyTransactionsProcess.LoadOptions.projectID), "GL", IsDBField = false, DisplayName = "From Project Task", AllowInactive = false)]
    public virtual int? FromTaskID { get; set; }

    [BaseProjectTask(typeof (ReclassifyTransactionsProcess.LoadOptions.projectID), "GL", IsDBField = false, DisplayName = "To Project Task", AllowInactive = false)]
    public virtual int? ToTaskID { get; set; }

    [CostCode(DisplayName = "From Cost Code", IsDBField = false)]
    public virtual int? FromCostCodeID { get; set; }

    [CostCode(DisplayName = "To Cost Code", IsDBField = false)]
    public virtual int? ToCostCodeID { get; set; }

    [PXDBInt]
    [PXUIField(DisplayName = "Max. Number of Transactions")]
    [PXDefault(999)]
    public virtual int? MaxTrans
    {
      get => this._MaxTrans;
      set => this._MaxTrans = value;
    }

    [PXDBDate]
    public virtual DateTime? PeriodStartDate
    {
      get => this._PeriodStartDate;
      set => this._PeriodStartDate = value;
    }

    [PXDBDate]
    public virtual DateTime? PeriodEndDate
    {
      get => this._PeriodEndDate;
      set => this._PeriodEndDate = value;
    }

    [PXBool]
    public bool? UseMasterCalendar { get; set; }

    public abstract class branchID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ReclassifyTransactionsProcess.LoadOptions.branchID>
    {
    }

    public abstract class fromAccountID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ReclassifyTransactionsProcess.LoadOptions.fromAccountID>
    {
    }

    public abstract class toAccountID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ReclassifyTransactionsProcess.LoadOptions.toAccountID>
    {
    }

    public abstract class fromSubID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ReclassifyTransactionsProcess.LoadOptions.fromSubID>
    {
    }

    public abstract class toSubID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ReclassifyTransactionsProcess.LoadOptions.toSubID>
    {
    }

    public abstract class fromDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      ReclassifyTransactionsProcess.LoadOptions.fromDate>
    {
    }

    public abstract class toDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      ReclassifyTransactionsProcess.LoadOptions.toDate>
    {
    }

    public abstract class fromFinPeriodID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ReclassifyTransactionsProcess.LoadOptions.fromFinPeriodID>
    {
    }

    public abstract class toFinPeriodID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ReclassifyTransactionsProcess.LoadOptions.toFinPeriodID>
    {
    }

    public abstract class module : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ReclassifyTransactionsProcess.LoadOptions.module>
    {
    }

    public abstract class batchNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ReclassifyTransactionsProcess.LoadOptions.batchNbr>
    {
    }

    public abstract class refNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ReclassifyTransactionsProcess.LoadOptions.refNbr>
    {
    }

    public abstract class referenceID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ReclassifyTransactionsProcess.LoadOptions.referenceID>
    {
    }

    public abstract class projectID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ReclassifyTransactionsProcess.LoadOptions.projectID>
    {
    }

    public abstract class fromTaskID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ReclassifyTransactionsProcess.LoadOptions.fromTaskID>
    {
    }

    public abstract class toTaskID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ReclassifyTransactionsProcess.LoadOptions.toTaskID>
    {
    }

    public abstract class fromCostCodeID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ReclassifyTransactionsProcess.LoadOptions.fromCostCodeID>
    {
    }

    public abstract class toCostCodeID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ReclassifyTransactionsProcess.LoadOptions.toCostCodeID>
    {
    }

    public abstract class maxTrans : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ReclassifyTransactionsProcess.LoadOptions.maxTrans>
    {
    }

    public abstract class periodStartDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      ReclassifyTransactionsProcess.LoadOptions.periodStartDate>
    {
    }

    public abstract class periodEndDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      ReclassifyTransactionsProcess.LoadOptions.periodEndDate>
    {
    }

    public abstract class useMasterCalendar : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      ReclassifyTransactionsProcess.LoadOptions.useMasterCalendar>
    {
    }

    public class ModuleListAttribute : PXStringListAttribute
    {
      public ModuleListAttribute()
        : base(new string[9]
        {
          "GL",
          "AP",
          "AR",
          "CA",
          "IN",
          "DR",
          "FA",
          "PM",
          "PR"
        }, new string[9]
        {
          "GL",
          "AP",
          "AR",
          "CA",
          "IN",
          "DR",
          "FA",
          "PM",
          "PR"
        })
      {
      }
    }
  }

  public class ReplaceOptions : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected int? _WithBranchID;
    protected int? _WithAccountID;
    protected int? _WithSubID;
    protected DateTime? _WithDate;
    protected string _WithFinPeriodID;
    protected string _WithTranDescFilteringValue;
    protected int? _NewBranchID;
    protected int? _NewAccountID;
    protected int? _NewSubID;
    protected DateTime? _NewDate;
    protected string _NewFinPeriodID;
    protected string _NewTranDesc;

    public virtual bool Showed { get; set; }

    [PXString]
    [PXUIField(DisplayName = "", IsReadOnly = true, Visible = false)]
    public virtual string Warning { get; set; }

    [Branch(null, null, true, true, false, IsDBField = false, Required = false)]
    public virtual int? WithBranchID
    {
      get => this._WithBranchID;
      set => this._WithBranchID = value;
    }

    [AccountAny(DisplayName = "Account", AvoidControlAccounts = true)]
    public virtual int? WithAccountID
    {
      get => this._WithAccountID;
      set => this._WithAccountID = value;
    }

    [SubAccount(DisplayName = "Subaccount")]
    public virtual int? WithSubID
    {
      get => this._WithSubID;
      set => this._WithSubID = value;
    }

    [ActiveProject(DisplayName = "Project", IsDBField = false)]
    public virtual int? WithProjectID { get; set; }

    [BaseProjectTask(typeof (ReclassifyTransactionsProcess.ReplaceOptions.withProjectID), "GL", IsDBField = false, DisplayName = "Project Task", AllowInactive = false)]
    public virtual int? WithTaskID { get; set; }

    [CostCode(typeof (ReclassifyTransactionsProcess.ReplaceOptions.withAccountID), typeof (ReclassifyTransactionsProcess.ReplaceOptions.withTaskID), IsDBField = false, DisplayName = "Cost Code")]
    public virtual int? WithCostCodeID { get; set; }

    [PXDBDate]
    [PXUIField(DisplayName = "Date")]
    public virtual DateTime? WithDate
    {
      get => this._WithDate;
      set => this._WithDate = value;
    }

    [OpenPeriod(null, typeof (ReclassifyTransactionsProcess.ReplaceOptions.withDate), typeof (ReclassifyTransactionsProcess.ReplaceOptions.withBranchID), null, null, null, null, false, false, false, true, true, FinPeriodSelectorAttribute.SelectionModesWithRestrictions.Undefined, null, null, true, IsDBField = false)]
    [PXUIField]
    public virtual string WithFinPeriodID
    {
      get => this._WithFinPeriodID;
      set => this._WithFinPeriodID = value;
    }

    [PXString(256 /*0x0100*/, IsUnicode = true)]
    [PXUIField(DisplayName = "Transaction Description")]
    public virtual string WithTranDescFilteringValue
    {
      get => this._WithTranDescFilteringValue;
      set => this._WithTranDescFilteringValue = value;
    }

    [Branch(null, typeof (Search2<PX.Objects.GL.Branch.branchID, InnerJoin<PX.Objects.GL.DAC.Organization, On<PX.Objects.GL.Branch.organizationID, Equal<PX.Objects.GL.DAC.Organization.organizationID>>>, Where<PX.Objects.GL.Branch.baseCuryID, EqualBaseCuryID<Current<ReclassifyTransactionsProcess.ReplaceOptions.withBranchID>>, And<MatchWithBranch<PX.Objects.GL.Branch.branchID>>>>), true, true, false, DisplayName = "Branch", IsDBField = false, Required = false)]
    public virtual int? NewBranchID
    {
      get => this._NewBranchID;
      set => this._NewBranchID = value;
    }

    [AccountAny(DisplayName = "Account", AvoidControlAccounts = true)]
    public virtual int? NewAccountID
    {
      get => this._NewAccountID;
      set => this._NewAccountID = value;
    }

    [SubAccount(DisplayName = "Subaccount")]
    public virtual int? NewSubID
    {
      get => this._NewSubID;
      set => this._NewSubID = value;
    }

    [ActiveProject(DisplayName = "Project", IsDBField = false)]
    public virtual int? NewProjectID { get; set; }

    [BaseProjectTask(typeof (ReclassifyTransactionsProcess.ReplaceOptions.newProjectID), "GL", IsDBField = false, DisplayName = "Project Task", AllowInactive = false)]
    public virtual int? NewTaskID { get; set; }

    [CostCode(typeof (ReclassifyTransactionsProcess.ReplaceOptions.newAccountID), typeof (ReclassifyTransactionsProcess.ReplaceOptions.newTaskID), IsDBField = false, DisplayName = "Cost Code")]
    public virtual int? NewCostCodeID { get; set; }

    [PXDBDate]
    [PXUIField(DisplayName = "Date")]
    public virtual DateTime? NewDate
    {
      get => this._NewDate;
      set => this._NewDate = value;
    }

    [OpenPeriod(null, typeof (ReclassifyTransactionsProcess.ReplaceOptions.newDate), typeof (ReclassifyTransactionsProcess.ReplaceOptions.newBranchID), null, null, null, null, true, false, false, false, true, FinPeriodSelectorAttribute.SelectionModesWithRestrictions.Undefined, null, null, true, IsDBField = false)]
    public virtual string NewFinPeriodID
    {
      get => this._NewFinPeriodID;
      set => this._NewFinPeriodID = value;
    }

    [PXString(256 /*0x0100*/, IsUnicode = true)]
    [PXUIField(DisplayName = "Transaction Description")]
    public virtual string NewTranDesc
    {
      get => this._NewTranDesc;
      set => this._NewTranDesc = value;
    }

    public abstract class warning : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ReclassifyTransactionsProcess.ReplaceOptions.warning>
    {
    }

    public abstract class withBranchID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ReclassifyTransactionsProcess.ReplaceOptions.withBranchID>
    {
    }

    public abstract class withAccountID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ReclassifyTransactionsProcess.ReplaceOptions.withAccountID>
    {
    }

    public abstract class withSubID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ReclassifyTransactionsProcess.ReplaceOptions.withSubID>
    {
    }

    public abstract class withProjectID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ReclassifyTransactionsProcess.ReplaceOptions.withProjectID>
    {
    }

    public abstract class withTaskID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ReclassifyTransactionsProcess.ReplaceOptions.withTaskID>
    {
    }

    public abstract class withCostCodeID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ReclassifyTransactionsProcess.ReplaceOptions.withCostCodeID>
    {
    }

    public abstract class withDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      ReclassifyTransactionsProcess.ReplaceOptions.withDate>
    {
    }

    public abstract class withFinPeriodID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ReclassifyTransactionsProcess.ReplaceOptions.withFinPeriodID>
    {
    }

    public abstract class withTranDescFilteringValue : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ReclassifyTransactionsProcess.ReplaceOptions.withTranDescFilteringValue>
    {
    }

    public abstract class newBranchID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ReclassifyTransactionsProcess.ReplaceOptions.newBranchID>
    {
    }

    public abstract class newAccountID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ReclassifyTransactionsProcess.ReplaceOptions.newAccountID>
    {
    }

    public abstract class newSubID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ReclassifyTransactionsProcess.ReplaceOptions.newSubID>
    {
    }

    public abstract class newProjectID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ReclassifyTransactionsProcess.ReplaceOptions.newProjectID>
    {
    }

    public abstract class newTaskID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ReclassifyTransactionsProcess.ReplaceOptions.newTaskID>
    {
    }

    public abstract class newCostCodeID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ReclassifyTransactionsProcess.ReplaceOptions.newCostCodeID>
    {
    }

    public abstract class newDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      ReclassifyTransactionsProcess.ReplaceOptions.newDate>
    {
    }

    public abstract class newFinPeriodID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ReclassifyTransactionsProcess.ReplaceOptions.newFinPeriodID>
    {
    }

    public abstract class newTranDesc : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ReclassifyTransactionsProcess.ReplaceOptions.newTranDesc>
    {
    }
  }
}
