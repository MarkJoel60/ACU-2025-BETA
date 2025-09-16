// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.FeaturesMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.BankFeed.Common;
using PX.Caching;
using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Licensing;
using PX.Metadata;
using PX.ML.Common.CloudProcessing.DomainServices.Interfaces;
using PX.ML.GI.Anomalies.DomainModel;
using PX.Objects.AP.InvoiceRecognition;
using PX.Objects.AR;
using PX.Objects.CA;
using PX.Objects.Common.Extensions;
using PX.Objects.CR;
using PX.Objects.FA;
using PX.Objects.GL.DAC;
using PX.Objects.GL.FinPeriods;
using PX.Objects.GL.FinPeriods.TableDefinition;
using PX.Objects.PM;
using PX.Objects.SO;
using PX.Objects.WZ;
using PX.Web.UI.Frameset.Model.DAC;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

#nullable enable
namespace PX.Objects.CS;

public class FeaturesMaint : PXGraph<
#nullable disable
FeaturesMaint>
{
  public PXFilter<AfterActivation> ActivationBehaviour;
  public PXSelect<FeaturesSet> Features;
  public PXSave<FeaturesSet> Save;
  public PXSaveClose<FeaturesSet> SaveClose;
  public PXCancel<FeaturesSet> Cancel;
  public PXAction<FeaturesSet> Insert;
  public PXAction<FeaturesSet> RequestValidation;
  public PXAction<FeaturesSet> CancelRequest;
  public PXSelectJoin<MasterFinPeriod, InnerJoin<OrganizationFinPeriod, On<MasterFinPeriod.finPeriodID, Equal<OrganizationFinPeriod.masterFinPeriodID>, And<OrganizationFinPeriod.organizationID, Equal<Required<OrganizationFinPeriod.organizationID>>>>>> MasterFinPeriods;
  public PXSelect<MUIWorkspace, Where<MUIWorkspace.workspaceID, Equal<Required<MUIWorkspace.workspaceID>>>> Workspace;
  public FbqlSelect<SelectFromBase<CRValidation, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<
  #nullable enable
  CRValidation.gramValidationDateTime, IBqlDateTime>.IsEqual<
  #nullable disable
  CRValidation.defaultGramValidationDateTime>>, CRValidation>.View CRDefaultValidations;
  public const int MAX_FINPERIOD_DISCREPANCY_MESSAGE_COUNT = 20;
  public PXSetup<PX.Objects.GL.Company> Company;
  private PX.Objects.GL.DAC.Organization etalonOrganization;

  [InjectDependency]
  protected ICacheControl<PageCache> PageCacheControl { get; set; }

  [InjectDependency]
  protected IScreenInfoCacheControl ScreenInfoCacheControl { get; set; }

  [InjectDependency]
  internal IInvoiceRecognitionService InvoiceRecognitionService { get; set; }

  [InjectDependency]
  internal IMLCloudCalculationService<MLGIDataCollectionInfo, MLGIDataSetCollectionInfo, Prediction> MLGIAnomaliesService { get; set; }

  [InjectDependency]
  internal ILicensing Licensing { get; set; }

  [InjectDependency]
  internal IBankFeedCloudClient BankFeedClient { get; set; }

  protected IEnumerable features()
  {
    FeaturesMaint featuresMaint = this;
    FeaturesSet featuresSet = PXResultset<FeaturesSet>.op_Implicit(PXSelectBase<FeaturesSet, PXSelect<FeaturesSet, Where<True, Equal<True>>, OrderBy<Desc<FeaturesSet.status>>>.Config>.SelectWindowed((PXGraph) featuresMaint, 0, 1, Array.Empty<object>())) ?? ((PXSelectBase<FeaturesSet>) featuresMaint.Features).Insert();
    featuresSet.LicenseID = PXVersionInfo.InstallationID;
    yield return (object) featuresSet;
  }

  public FeaturesMaint()
  {
    ((PXAction) this.SaveClose).SetVisible(false);
    EnumerableExtensions.ForEach<string>(((IEnumerable<string>) ((PXSelectBase) this.Features).Cache.Fields).Where<string>((Func<string, bool>) (item => ((PXSelectBase) this.Features).Cache.GetAttributesOfType<FeatureAttribute>((object) null, item).Any<FeatureAttribute>())), (Action<string>) (item => PXUIFieldAttribute.SetVisibility(((PXSelectBase) this.Features).Cache, (object) null, item, (PXUIVisibility) 9)));
  }

  public virtual IEnumerable ExecuteSelect(
    string viewName,
    object[] parameters,
    object[] searches,
    string[] sortcolumns,
    bool[] descendings,
    PXFilterRow[] filters,
    ref int startRow,
    int maximumRows,
    ref int totalRows)
  {
    if (viewName == "Features")
      searches = (object[]) null;
    return ((PXGraph) this).ExecuteSelect(viewName, parameters, searches, sortcolumns, descendings, filters, ref startRow, maximumRows, ref totalRows);
  }

  [PXButton(IsLockedOnToolbar = true)]
  [PXUIField]
  public IEnumerable insert(PXAdapter adapter)
  {
    FeaturesMaint featuresMaint = this;
    AfterActivation activationMode = ((PXSelectBase<AfterActivation>) featuresMaint.ActivationBehaviour).Current;
    foreach (object obj in ((PXAction) new PXInsert<FeaturesSet>((PXGraph) featuresMaint, "Insert")).Press(adapter))
    {
      ((PXSelectBase) featuresMaint.ActivationBehaviour).Cache.SetValueExt<AfterActivation.refresh>((object) ((PXSelectBase<AfterActivation>) featuresMaint.ActivationBehaviour).Current, (object) activationMode.Refresh);
      yield return obj;
    }
  }

  [PXButton(IsLockedOnToolbar = true)]
  [PXUIField(DisplayName = "Enable")]
  public IEnumerable requestValidation(PXAdapter adapter)
  {
    FeaturesMaint featuresMaint1 = this;
    foreach (FeaturesSet featuresSet in adapter.Get())
    {
      if (featuresSet.Status.GetValueOrDefault() == 3)
      {
        bool? nullable1 = new bool?(PXAccess.FeatureInstalled<FeaturesSet.customerDiscounts>());
        bool? multiCompanyOld = new bool?(PXAccess.FeatureInstalled<FeaturesSet.multiCompany>());
        bool? multiBranchOld = new bool?(PXAccess.FeatureInstalled<FeaturesSet.branch>());
        bool? nullable2 = new bool?(PXAccess.FeatureInstalled<FeaturesSet.materialManagement>());
        bool? nullable3 = new bool?(PXAccess.FeatureInstalled<FeaturesSet.integratedCardProcessing>());
        PXCache pxCache = (PXCache) new PXCache<FeaturesSet>((PXGraph) featuresMaint1);
        FeaturesSet copy = PXCache<FeaturesSet>.CreateCopy(featuresSet);
        copy.Status = new int?(0);
        FeaturesSet updated = ((PXSelectBase<FeaturesSet>) featuresMaint1.Features).Update(copy);
        ((PXSelectBase<FeaturesSet>) featuresMaint1.Features).Delete(featuresSet);
        if (updated.Status.GetValueOrDefault() != 1)
          ((PXSelectBase<FeaturesSet>) featuresMaint1.Features).Delete(new FeaturesSet()
          {
            Status = new int?(1)
          });
        ((PXGraph) featuresMaint1).Persist();
        ++PXAccess.Version;
        PXResultset<WZTask> pxResultset = PXSelectBase<WZTask, PXSelect<WZTask>.Config>.Select((PXGraph) featuresMaint1, Array.Empty<object>());
        WZTaskEntry instance = PXGraph.CreateInstance<WZTaskEntry>();
        foreach (PXResult<WZTask> pxResult1 in pxResultset)
        {
          WZTask wzTask1 = PXResult<WZTask>.op_Implicit(pxResult1);
          bool flag1 = false;
          bool flag2 = false;
          foreach (PXResult<WZTaskFeature> pxResult2 in PXSelectBase<WZTaskFeature, PXSelectReadonly<WZTaskFeature, Where<WZTaskFeature.taskID, Equal<Required<WZTask.taskID>>>>.Config>.Select((PXGraph) featuresMaint1, new object[1]
          {
            (object) wzTask1.TaskID
          }))
          {
            WZTaskFeature wzTaskFeature = PXResult<WZTaskFeature>.op_Implicit(pxResult2);
            if (!((bool?) pxCache.GetValue((object) updated, wzTaskFeature.Feature)).GetValueOrDefault())
            {
              flag1 = true;
              flag2 = false;
              break;
            }
            flag2 = true;
          }
          if (flag1)
          {
            wzTask1.Status = "DS";
            ((PXSelectBase<WZTask>) instance.TaskInfo).Update(wzTask1);
            ((PXAction) instance.Save).Press();
          }
          if (flag2 && wzTask1.Status == "DS")
          {
            bool flag3 = false;
            WZScenario wzScenario = PXResultset<WZScenario>.op_Implicit(PXSelectBase<WZScenario, PXSelect<WZScenario, Where<WZScenario.scenarioID, Equal<Required<WZTask.scenarioID>>>>.Config>.Select((PXGraph) featuresMaint1, new object[1]
            {
              (object) wzTask1.ScenarioID
            }));
            if (wzScenario != null && wzScenario.Status == "AC")
            {
              WZTask wzTask2 = PXResultset<WZTask>.op_Implicit(PXSelectBase<WZTask, PXSelect<WZTask, Where<WZTask.taskID, Equal<Required<WZTask.parentTaskID>>>>.Config>.Select((PXGraph) featuresMaint1, new object[1]
              {
                (object) wzTask1.ParentTaskID
              }));
              if (wzTask2 != null && (wzTask2.Status == "OP" || wzTask2.Status == "AC"))
                flag3 = true;
              foreach (PXResult<WZTaskPredecessorRelation, WZTask> pxResult3 in PXSelectBase<WZTaskPredecessorRelation, PXSelectJoin<WZTaskPredecessorRelation, InnerJoin<WZTask, On<WZTask.taskID, Equal<WZTaskPredecessorRelation.predecessorID>>>, Where<WZTaskPredecessorRelation.taskID, Equal<Required<WZTask.taskID>>>>.Config>.Select((PXGraph) featuresMaint1, new object[1]
              {
                (object) wzTask1.TaskID
              }))
              {
                WZTask wzTask3 = PXResult<WZTaskPredecessorRelation, WZTask>.op_Implicit(pxResult3);
                if (wzTask3 != null)
                {
                  if (wzTask3.Status == "CP")
                  {
                    flag3 = true;
                  }
                  else
                  {
                    flag3 = false;
                    break;
                  }
                }
              }
            }
            wzTask1.Status = flag3 ? "OP" : "PN";
            ((PXSelectBase<WZTask>) instance.TaskInfo).Update(wzTask1);
            ((PXAction) instance.Save).Press();
          }
        }
        if (nullable1.GetValueOrDefault() && !updated.CustomerDiscounts.GetValueOrDefault())
          PXUpdate<Set<SOOrderType.recalculateDiscOnPartialShipment, False, Set<SOOrderType.postLineDiscSeparately, False>>, SOOrderType>.Update((PXGraph) featuresMaint1, Array.Empty<object>());
        bool? nullable4 = multiCompanyOld;
        bool? nullable5 = updated.MultiCompany;
        if (!(nullable4.GetValueOrDefault() == nullable5.GetValueOrDefault() & nullable4.HasValue == nullable5.HasValue))
        {
          FeaturesMaint featuresMaint2 = featuresMaint1;
          object[] objArray = new object[2];
          nullable5 = updated.MultiCompany;
          objArray[0] = (object) nullable5.GetValueOrDefault();
          objArray[1] = (object) "CS101500";
          PXUpdate<Set<ListEntryPoint.isActive, Required<ListEntryPoint.isActive>>, ListEntryPoint, Where<ListEntryPoint.entryScreenID, Equal<Required<ListEntryPoint.entryScreenID>>>>.Update((PXGraph) featuresMaint2, objArray);
        }
        featuresMaint1.UpdateARPrapreStatement(multiCompanyOld, multiBranchOld, updated);
        if (!nullable2.GetValueOrDefault())
        {
          nullable5 = updated.MaterialManagement;
          if (nullable5.GetValueOrDefault())
          {
            PXUpdate<Set<PMSetup.stockInitRequired, True>, PMSetup>.Update((PXGraph) featuresMaint1, Array.Empty<object>());
            goto label_49;
          }
        }
        if (nullable2.GetValueOrDefault())
        {
          nullable5 = updated.MaterialManagement;
          if (!nullable5.GetValueOrDefault())
            PXUpdate<Set<PMSetup.stockInitRequired, False>, PMSetup>.Update((PXGraph) featuresMaint1, Array.Empty<object>());
        }
label_49:
        if (!nullable3.GetValueOrDefault())
        {
          nullable5 = updated.IntegratedCardProcessing;
          if (nullable5.GetValueOrDefault())
            PXUpdate<Set<ARSetup.integratedCCProcessing, True>, ARSetup>.Update((PXGraph) featuresMaint1, Array.Empty<object>());
        }
        yield return (object) updated;
      }
      else
        yield return (object) featuresSet;
    }
    int num;
    if (((PXSelectBase<AfterActivation>) featuresMaint1.ActivationBehaviour).Current != null)
    {
      bool? refresh = ((PXSelectBase<AfterActivation>) featuresMaint1.ActivationBehaviour).Current.Refresh;
      bool flag = false;
      num = !(refresh.GetValueOrDefault() == flag & refresh.HasValue) ? 1 : 0;
    }
    else
      num = 1;
    PXDatabase.ResetSlots();
    ((ICacheControl) featuresMaint1.PageCacheControl).InvalidateCache();
    ((ICacheControl) featuresMaint1.ScreenInfoCacheControl).InvalidateCache();
    ((PXGraph) featuresMaint1).Clear();
    if (num != 0)
      throw new PXRefreshException();
  }

  private void UpdateARPrapreStatement(
    bool? multiCompanyOld,
    bool? multiBranchOld,
    FeaturesSet updated)
  {
    bool? nullable1 = multiCompanyOld;
    bool? multiCompany = updated.MultiCompany;
    if (nullable1.GetValueOrDefault() == multiCompany.GetValueOrDefault() & nullable1.HasValue == multiCompany.HasValue)
    {
      bool? nullable2 = multiBranchOld;
      bool? branch = updated.Branch;
      if (nullable2.GetValueOrDefault() == branch.GetValueOrDefault() & nullable2.HasValue == branch.HasValue)
        return;
    }
    bool? nullable3 = updated.MultiCompany;
    bool flag1 = false;
    if (nullable3.GetValueOrDefault() == flag1 & nullable3.HasValue)
    {
      nullable3 = updated.Branch;
      bool flag2 = false;
      if (nullable3.GetValueOrDefault() == flag2 & nullable3.HasValue)
      {
        PXUpdate<Set<ARSetup.prepareStatements, Required<ARSetup.prepareStatements>>, ARSetup>.Update((PXGraph) this, new object[1]
        {
          (object) "B"
        });
        return;
      }
    }
    nullable3 = updated.MultiCompany;
    bool flag3 = false;
    if (nullable3.GetValueOrDefault() == flag3 & nullable3.HasValue)
    {
      nullable3 = updated.Branch;
      if (nullable3.GetValueOrDefault())
      {
        PXUpdate<Set<ARSetup.prepareStatements, Required<ARSetup.prepareStatements>>, ARSetup, Where<ARSetup.prepareStatements, Equal<Required<ARSetup.prepareStatements>>>>.Update((PXGraph) this, new object[2]
        {
          (object) "B",
          (object) "A"
        });
        return;
      }
    }
    nullable3 = updated.MultiCompany;
    if (!nullable3.GetValueOrDefault())
      return;
    nullable3 = updated.Branch;
    bool flag4 = false;
    if (!(nullable3.GetValueOrDefault() == flag4 & nullable3.HasValue))
      return;
    PXUpdate<Set<ARSetup.prepareStatements, Required<ARSetup.prepareStatements>>, ARSetup, Where<ARSetup.prepareStatements, Equal<Required<ARSetup.prepareStatements>>>>.Update((PXGraph) this, new object[2]
    {
      (object) "C",
      (object) "B"
    });
  }

  [PXButton]
  [PXUIField(DisplayName = "Cancel Validation Request", Visible = false)]
  public IEnumerable cancelRequest(PXAdapter adapter)
  {
    FeaturesMaint featuresMaint = this;
    foreach (FeaturesSet featuresSet1 in adapter.Get())
    {
      if (featuresSet1.Status.GetValueOrDefault() == 2)
      {
        FeaturesSet copy = PXCache<FeaturesSet>.CreateCopy(featuresSet1);
        copy.Status = new int?(3);
        ((PXSelectBase<FeaturesSet>) featuresMaint.Features).Delete(featuresSet1);
        FeaturesSet featuresSet2 = ((PXSelectBase<FeaturesSet>) featuresMaint.Features).Update(copy);
        ((PXGraph) featuresMaint).Persist();
        yield return (object) featuresSet2;
      }
      else
        yield return (object) featuresSet1;
    }
  }

  protected virtual void FeaturesSet_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    ((PXAction) this.Save).SetVisible(false);
    ((PXSelectBase) this.Features).Cache.AllowInsert = true;
    FeaturesSet row = (FeaturesSet) e.Row;
    if (row == null)
      return;
    ((PXAction) this.RequestValidation).SetEnabled(row.Status.GetValueOrDefault() == 3);
    PXAction<FeaturesSet> cancelRequest = this.CancelRequest;
    int? status = row.Status;
    int num1 = status.GetValueOrDefault() == 2 ? 1 : 0;
    ((PXAction) cancelRequest).SetEnabled(num1 != 0);
    PXCache cache1 = ((PXSelectBase) this.Features).Cache;
    status = row.Status;
    int num2 = 2;
    int num3 = status.GetValueOrDefault() < num2 & status.HasValue ? 1 : 0;
    cache1.AllowInsert = num3 != 0;
    PXCache cache2 = ((PXSelectBase) this.Features).Cache;
    status = row.Status;
    int num4 = status.GetValueOrDefault() == 3 ? 1 : 0;
    cache2.AllowUpdate = num4 != 0;
    ((PXSelectBase) this.Features).Cache.AllowDelete = false;
    if ((((PXSelectBase<AfterActivation>) this.ActivationBehaviour).Current == null ? 1 : (!((PXSelectBase<AfterActivation>) this.ActivationBehaviour).Current.Refresh.GetValueOrDefault() ? 1 : 0)) != 0 && ((OrderedDictionary) ((PXGraph) this).Actions).Contains((object) "CancelClose"))
      ((PXGraph) this).Actions["CancelClose"].SetTooltip("Back to Scenario");
    if (sender.GetStateExt<FeaturesSet.sendGridIntegration>((object) row) is PXFieldState stateExt1 && stateExt1.ErrorLevel != 4)
      PXUIFieldAttribute.SetEnabled<FeaturesSet.importSendGridDesigns>(sender, (object) row, ((bool?) stateExt1.Value).GetValueOrDefault());
    bool? nullable;
    if (sender.GetStateExt<FeaturesSet.bankFeedIntegration>((object) row) is PXFieldState stateExt2 && stateExt2.ErrorLevel != 4)
    {
      nullable = row.BankFeedIntegration;
      bool valueOrDefault = nullable.GetValueOrDefault();
      PXUIFieldAttribute.SetEnabled<FeaturesSet.bankFeedAccountsMultipleMapping>(sender, (object) row, valueOrDefault);
    }
    PXFieldState stateExt3 = sender.GetStateExt<FeaturesSet.payrollModule>((object) row) as PXFieldState;
    nullable = row.PayrollModule;
    if (nullable.GetValueOrDefault())
    {
      nullable = row.PayrollUS;
      if (!nullable.GetValueOrDefault())
      {
        nullable = row.PayrollCAN;
        if (!nullable.GetValueOrDefault() && stateExt3 != null && stateExt3.ErrorLevel != 4)
          sender.RaiseExceptionHandling<FeaturesSet.payrollModule>((object) row, (object) row.PayrollModule, (Exception) new PXSetPropertyException("Please select either US Payroll or Canadian Payroll as a sub feature.", (PXErrorLevel) 2));
      }
    }
    nullable = row.AdvancedSOInvoices;
    bool flag = false;
    if (nullable.GetValueOrDefault() == flag & nullable.HasValue)
    {
      nullable = row.ServiceManagementModule;
      if (nullable.GetValueOrDefault())
        sender.RaiseExceptionHandling<FeaturesSet.advancedSOInvoices>((object) row, (object) row.AdvancedSOInvoices, (Exception) new PXSetPropertyException("If this feature is disabled, corrections to SO invoices created from field service documents and containing a stock item or items are not supported.", (PXErrorLevel) 2));
    }
    nullable = row.OutlookIntegration;
    if (!nullable.GetValueOrDefault())
      return;
    nullable = row.OpenIDConnect;
    if (nullable.GetValueOrDefault())
      return;
    sender.RaiseExceptionHandling<FeaturesSet.outlookIntegration>((object) row, (object) row.OutlookIntegration, (Exception) new PXSetPropertyException((IBqlTable) row, "The OpenID Connect feature must be enabled for Outlook Integration to work with Microsoft 365 accounts.", (PXErrorLevel) 2));
  }

  protected virtual void FeaturesSet_RowInserting(PXCache sender, PXRowInsertingEventArgs e)
  {
    if (((int?) sender.GetValue<FeaturesSet.status>(e.Row)).GetValueOrDefault() != 3)
      return;
    FeaturesSet featuresSet = PXResultset<FeaturesSet>.op_Implicit(PXSelectBase<FeaturesSet, PXSelect<FeaturesSet, Where<True, Equal<True>>, OrderBy<Desc<FeaturesSet.status>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, Array.Empty<object>()));
    if (featuresSet == null)
      return;
    sender.RestoreCopy(e.Row, (object) featuresSet);
    sender.SetValue<FeaturesSet.status>(e.Row, (object) 3);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FeaturesSet, FeaturesSet.manufacturingMRP> e)
  {
    if (!(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FeaturesSet, FeaturesSet.manufacturingMRP>>) e).Cache.GetStateExt<FeaturesSet.manufacturingMRP>((object) e.Row) is PXFieldState stateExt) || stateExt.ErrorLevel == 4 || !((bool?) stateExt.Value).GetValueOrDefault())
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FeaturesSet, FeaturesSet.manufacturingMRP>>) e).Cache.SetValueExt<FeaturesSet.distributionReqPlan>((object) e.Row, (object) false);
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<FeaturesSet, FeaturesSet.manufacturing> e)
  {
    FeaturesSet current = (FeaturesSet) ((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<FeaturesSet, FeaturesSet.manufacturing>>) e).Cache.Current;
    bool? nullable;
    int num;
    if (current == null)
    {
      num = 0;
    }
    else
    {
      nullable = current.DistributionReqPlan;
      num = nullable.GetValueOrDefault() ? 1 : 0;
    }
    if (num == 0)
      return;
    nullable = (bool?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<FeaturesSet, FeaturesSet.manufacturing>, FeaturesSet, object>) e).NewValue;
    if (!nullable.GetValueOrDefault())
      return;
    nullable = (bool?) e.OldValue;
    bool flag = false;
    if (nullable.GetValueOrDefault() == flag & nullable.HasValue)
    {
      ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<FeaturesSet, FeaturesSet.manufacturing>, FeaturesSet, object>) e).NewValue = e.OldValue;
      throw new PXSetPropertyException("To enable this feature, disable {0} first.", new object[1]
      {
        (object) PXUIFieldAttribute.GetDisplayName<FeaturesSet.distributionReqPlan>(((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<FeaturesSet, FeaturesSet.manufacturing>>) e).Cache)
      });
    }
  }

  protected virtual void CheckMasterOrganizationCalendarDiscrepancy()
  {
    int num = 0;
    bool flag = false;
    foreach (PXResult<PX.Objects.GL.DAC.Organization> pxResult1 in PXSelectBase<PX.Objects.GL.DAC.Organization, PXViewOf<PX.Objects.GL.DAC.Organization>.BasedOn<SelectFromBase<PX.Objects.GL.DAC.Organization, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.GL.DAC.Organization.organizationType, NotEqual<OrganizationTypes.group>>>>>.And<BqlOperand<PX.Objects.GL.DAC.Organization.status, IBqlString>.IsEqual<OrganizationStatus.active>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()))
    {
      PX.Objects.GL.DAC.Organization organization = PXResult<PX.Objects.GL.DAC.Organization>.op_Implicit(pxResult1);
      foreach (PXResult<MasterFinPeriod> pxResult2 in PXSelectBase<MasterFinPeriod, PXSelectJoin<MasterFinPeriod, LeftJoin<OrganizationFinPeriod, On<MasterFinPeriod.finPeriodID, Equal<OrganizationFinPeriod.masterFinPeriodID>, And<OrganizationFinPeriod.organizationID, Equal<Required<OrganizationFinPeriod.organizationID>>>>>, Where<OrganizationFinPeriod.finPeriodID, IsNull>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) organization.OrganizationID
      }))
      {
        MasterFinPeriod masterFinPeriod = PXResult<MasterFinPeriod>.op_Implicit(pxResult2);
        flag = true;
        if (num <= 20)
        {
          PXTrace.WriteError("A discrepancy exists between the master calendar and the calendar of the {0} company in the {1} period on the Master Financial Calendar (GL201000) and Company Financial Calendar (GL201100) forms, respectively.", new object[2]
          {
            (object) organization.OrganizationCD,
            (object) masterFinPeriod.FinPeriodID
          });
          ++num;
        }
        else
          break;
      }
    }
    if (flag)
      throw new PXSetPropertyException("A discrepancy exists between the master calendar and the calendar of the {0} company on the Master Financial Calendar (GL201000) and Company Financial Calendar (GL201100) forms, respectively. Please see Trace.");
  }

  protected PX.Objects.GL.DAC.Organization EtalonOrganization
  {
    get
    {
      return this.etalonOrganization ?? (this.etalonOrganization = PXResultset<PX.Objects.GL.DAC.Organization>.op_Implicit(PXSelectBase<PX.Objects.GL.DAC.Organization, PXViewOf<PX.Objects.GL.DAC.Organization>.BasedOn<SelectFromBase<PX.Objects.GL.DAC.Organization, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.GL.DAC.Organization.organizationType, NotEqual<OrganizationTypes.group>>>>>.And<BqlOperand<PX.Objects.GL.DAC.Organization.status, IBqlString>.IsEqual<OrganizationStatus.active>>>>.Config>.SelectSingleBound((PXGraph) this, new object[0], Array.Empty<object>())));
    }
  }

  protected virtual void CheckOrganizationCalendarFieldsDiscrepancy()
  {
    int num = 0;
    bool flag = false;
    if (this.EtalonOrganization == null)
      return;
    foreach (PXResult<PX.Objects.GL.DAC.Organization> pxResult1 in PXSelectBase<PX.Objects.GL.DAC.Organization, PXSelect<PX.Objects.GL.DAC.Organization, Where<PX.Objects.GL.DAC.Organization.organizationID, NotEqual<Required<PX.Objects.GL.DAC.Organization.organizationID>>, And<PX.Objects.GL.DAC.Organization.status, Equal<OrganizationStatus.active>, And<PX.Objects.GL.DAC.Organization.organizationType, NotEqual<OrganizationTypes.group>>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) this.EtalonOrganization.OrganizationID
    }))
    {
      PX.Objects.GL.DAC.Organization organization = PXResult<PX.Objects.GL.DAC.Organization>.op_Implicit(pxResult1);
      foreach (PXResult<OrganizationFinPeriod> pxResult2 in PXSelectBase<OrganizationFinPeriod, PXSelectJoin<OrganizationFinPeriod, LeftJoin<OrganizationFinPeriodStatus, On<OrganizationFinPeriodStatus.organizationID, Equal<Required<OrganizationFinPeriodStatus.organizationID>>, And<OrganizationFinPeriod.finPeriodID, Equal<OrganizationFinPeriodStatus.finPeriodID>, And<OrganizationFinPeriod.dateLocked, Equal<OrganizationFinPeriodStatus.dateLocked>, And<OrganizationFinPeriod.status, Equal<OrganizationFinPeriodStatus.status>, And<OrganizationFinPeriod.aPClosed, Equal<OrganizationFinPeriodStatus.aPClosed>, And<OrganizationFinPeriod.aRClosed, Equal<OrganizationFinPeriodStatus.aRClosed>, And<OrganizationFinPeriod.iNClosed, Equal<OrganizationFinPeriodStatus.iNClosed>, And<OrganizationFinPeriod.cAClosed, Equal<OrganizationFinPeriodStatus.cAClosed>, And<OrganizationFinPeriod.fAClosed, Equal<OrganizationFinPeriodStatus.fAClosed>>>>>>>>>>>, Where<OrganizationFinPeriodStatus.finPeriodID, IsNull, And<OrganizationFinPeriod.organizationID, Equal<Required<OrganizationFinPeriod.organizationID>>>>>.Config>.Select((PXGraph) this, new object[2]
      {
        (object) organization.OrganizationID,
        (object) this.EtalonOrganization.OrganizationID
      }))
      {
        OrganizationFinPeriod problemPeriod = PXResult<OrganizationFinPeriod>.op_Implicit(pxResult2);
        flag = true;
        if (num <= 20)
        {
          string problemFields = this.GetProblemFields(organization, problemPeriod);
          PXTrace.WriteError("A discrepancy exists between the calendars of the {0} and {1} companies in the {3} period in the following functional areas: {2}.", new object[4]
          {
            (object) this.EtalonOrganization.OrganizationCD,
            (object) organization.OrganizationCD,
            (object) problemFields,
            (object) problemPeriod.FinPeriodID
          });
          ++num;
        }
        else
          break;
      }
    }
    if (flag)
      throw new PXSetPropertyException("A discrepancy exists between the calendars of companies on the Company Financial Calendar (GL201100) form. Please see Trace.");
  }

  protected virtual void CheckInactiveOrganizationCalendarFieldsDiscrepancy()
  {
    int num = 0;
    bool flag = false;
    if (this.EtalonOrganization == null)
      return;
    foreach (PXResult<OrganizationFinPeriod> pxResult in PXSelectBase<OrganizationFinPeriod, PXSelectJoin<OrganizationFinPeriod, LeftJoin<PX.Objects.GL.DAC.Organization, On<PX.Objects.GL.DAC.Organization.organizationID, Equal<OrganizationFinPeriod.organizationID>>, LeftJoin<OrganizationFinPeriodStatus, On<OrganizationFinPeriodStatus.organizationID, Equal<Required<OrganizationFinPeriodStatus.organizationID>>, And<OrganizationFinPeriod.finPeriodID, Equal<OrganizationFinPeriodStatus.finPeriodID>>>>>, Where<PX.Objects.GL.DAC.Organization.status, Equal<OrganizationStatus.inactive>, And<Where<OrganizationFinPeriod.dateLocked, NotEqual<OrganizationFinPeriodStatus.dateLocked>, Or<OrganizationFinPeriod.status, NotEqual<OrganizationFinPeriodStatus.status>, Or<OrganizationFinPeriod.aPClosed, NotEqual<OrganizationFinPeriodStatus.aPClosed>, Or<OrganizationFinPeriod.aRClosed, NotEqual<OrganizationFinPeriodStatus.aRClosed>, Or<OrganizationFinPeriod.iNClosed, NotEqual<OrganizationFinPeriodStatus.iNClosed>, Or<OrganizationFinPeriod.cAClosed, NotEqual<OrganizationFinPeriodStatus.cAClosed>, Or<OrganizationFinPeriod.fAClosed, NotEqual<OrganizationFinPeriodStatus.fAClosed>>>>>>>>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) this.EtalonOrganization.OrganizationID
    }))
    {
      OrganizationFinPeriod problemPeriod = PXResult<OrganizationFinPeriod>.op_Implicit(pxResult);
      flag = true;
      if (num <= 20)
      {
        string problemFields = this.GetProblemFields(this.EtalonOrganization, problemPeriod);
        PXTrace.WriteError("A discrepancy exists between the calendars of the {0} and {1} companies in the {3} period in the following functional areas: {2}.", new object[4]
        {
          (object) this.EtalonOrganization.OrganizationCD,
          (object) PXAccess.GetOrganizationCD(problemPeriod.OrganizationID),
          (object) problemFields,
          (object) problemPeriod.FinPeriodID
        });
        ++num;
      }
      else
        break;
    }
    if (flag)
      throw new PXSetPropertyException("A discrepancy exists between the calendars of companies on the Company Financial Calendar (GL201100) form. Please see Trace.");
  }

  private void CheckFullLengthOrganizationCalendars()
  {
    List<string> list = GraphHelper.RowCast<PX.Objects.GL.DAC.Organization>((IEnumerable) PXSelectBase<MasterFinPeriod, PXSelectJoinGroupBy<MasterFinPeriod, CrossJoin<PX.Objects.GL.DAC.Organization, LeftJoin<OrganizationFinPeriod, On<MasterFinPeriod.finPeriodID, Equal<OrganizationFinPeriod.masterFinPeriodID>, And<PX.Objects.GL.DAC.Organization.organizationID, Equal<OrganizationFinPeriod.organizationID>>>>>, Where<OrganizationFinPeriod.finPeriodID, IsNull, And<PX.Objects.GL.DAC.Organization.status, Equal<OrganizationStatus.active>, And<PX.Objects.GL.DAC.Organization.organizationType, NotEqual<OrganizationTypes.group>>>>, Aggregate<GroupBy<PX.Objects.GL.DAC.Organization.organizationCD>>>.Config>.Select((PXGraph) this, Array.Empty<object>())).Select<PX.Objects.GL.DAC.Organization, string>((Func<PX.Objects.GL.DAC.Organization, string>) (org => org.OrganizationCD.Trim())).ToList<string>();
    if (list.Any<string>())
      throw new PXSetPropertyException("The following companies have calendars shorter than the master calendar: {0}.", new object[1]
      {
        (object) string.Join(", ", (IEnumerable<string>) list)
      });
  }

  private void CheckFullLengthOrganizationFACalendars()
  {
    PXResultset<FABookPeriod> source = PXSelectBase<FABookPeriod, PXSelectJoinGroupBy<FABookPeriod, CrossJoin<PX.Objects.GL.DAC.Organization, LeftJoin<FABook, On<FABook.bookID, Equal<FABookPeriod.bookID>>, LeftJoin<FABookPeriodAlias, On<FABookPeriod.finPeriodID, Equal<FABookPeriodAlias.finPeriodID>, And<PX.Objects.GL.DAC.Organization.organizationID, Equal<FABookPeriodAlias.organizationID>, And<FABookPeriod.bookID, Equal<FABookPeriodAlias.bookID>>>>>>>, Where<FABookPeriodAlias.finPeriodID, IsNull, And<FABook.updateGL, Equal<True>, And<PX.Objects.GL.DAC.Organization.status, Equal<OrganizationStatus.active>, And<FABookPeriod.organizationID, Equal<FinPeriod.organizationID.masterValue>>>>>, Aggregate<GroupBy<PX.Objects.GL.DAC.Organization.organizationCD>>>.Config>.Select((PXGraph) this, Array.Empty<object>());
    List<string> list = GraphHelper.RowCast<PX.Objects.GL.DAC.Organization>((IEnumerable) source).Select<PX.Objects.GL.DAC.Organization, string>((Func<PX.Objects.GL.DAC.Organization, string>) (org => org.OrganizationCD.Trim())).ToList<string>();
    FABookPeriod faBookPeriod = PXResult<FABookPeriod>.op_Implicit(((IQueryable<PXResult<FABookPeriod>>) source).FirstOrDefault<PXResult<FABookPeriod>>());
    ((PXGraph) this).GetService<IFABookPeriodRepository>();
    if (list.Any<string>())
      throw new PXSetPropertyException("In fixed assets, the posting book calendars are shorter than the master calendar for the following companies: {0}. On the Generate Book Calendars (FA501000) form, generate years from {1} to {2} in the posting book for these companies.", new object[3]
      {
        (object) string.Join(", ", (IEnumerable<string>) list),
        (object) ((PXGraph) this).GetService<IFABookPeriodRepository>().FindFirstFABookYear(faBookPeriod.BookID, new int?(0)).Year,
        (object) ((PXGraph) this).GetService<IFABookPeriodRepository>().FindLastFABookYear(faBookPeriod.BookID, new int?(0)).Year
      });
  }

  private void CheckUnshiftedOrganizationCalendars()
  {
    List<string> list = GraphHelper.RowCast<PX.Objects.GL.DAC.Organization>((IEnumerable) PXSelectBase<OrganizationFinPeriod, PXSelectJoinGroupBy<OrganizationFinPeriod, InnerJoin<PX.Objects.GL.DAC.Organization, On<OrganizationFinPeriod.organizationID, Equal<PX.Objects.GL.DAC.Organization.organizationID>>>, Where<OrganizationFinPeriod.finPeriodID, NotEqual<OrganizationFinPeriod.masterFinPeriodID>>, Aggregate<GroupBy<PX.Objects.GL.DAC.Organization.organizationCD>>>.Config>.Select((PXGraph) this, Array.Empty<object>())).Select<PX.Objects.GL.DAC.Organization, string>((Func<PX.Objects.GL.DAC.Organization, string>) (org => org.OrganizationCD.Trim())).ToList<string>();
    if (list.Any<string>())
      throw new PXSetPropertyException("The following companies have calendars shifted relative to the master calendar: {0}.", new object[1]
      {
        (object) string.Join(", ", (IEnumerable<string>) list)
      });
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdating<FeaturesSet, FeaturesSet.multipleCalendarsSupport> e)
  {
    if (e.Row == null)
      return;
    bool? nullable = e.Row.MultipleCalendarsSupport;
    if (!nullable.GetValueOrDefault())
      return;
    nullable = (bool?) ((PX.Data.Events.FieldUpdatingBase<PX.Data.Events.FieldUpdating<FeaturesSet, FeaturesSet.multipleCalendarsSupport>>) e).NewValue;
    if (nullable.GetValueOrDefault())
      return;
    this.CheckUnshiftedOrganizationCalendars();
    this.CheckFullLengthOrganizationCalendars();
    this.CheckFullLengthOrganizationFACalendars();
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdating<FeaturesSet, FeaturesSet.centralizedPeriodsManagement> e)
  {
    ((PX.Data.Events.FieldUpdatingBase<PX.Data.Events.FieldUpdating<FeaturesSet, FeaturesSet.centralizedPeriodsManagement>>) e).NewValue = PXBoolAttribute.ConvertValue(((PX.Data.Events.FieldUpdatingBase<PX.Data.Events.FieldUpdating<FeaturesSet, FeaturesSet.centralizedPeriodsManagement>>) e).NewValue);
    if (e.Row == null)
      return;
    bool? periodsManagement = e.Row.CentralizedPeriodsManagement;
    if (!periodsManagement.HasValue)
      return;
    periodsManagement = e.Row.CentralizedPeriodsManagement;
    bool newValue = (bool) ((PX.Data.Events.FieldUpdatingBase<PX.Data.Events.FieldUpdating<FeaturesSet, FeaturesSet.centralizedPeriodsManagement>>) e).NewValue;
    if (periodsManagement.GetValueOrDefault() == newValue & periodsManagement.HasValue || !(bool) ((PX.Data.Events.FieldUpdatingBase<PX.Data.Events.FieldUpdating<FeaturesSet, FeaturesSet.centralizedPeriodsManagement>>) e).NewValue)
      return;
    this.CheckOrganizationCalendarFieldsDiscrepancy();
    this.CheckInactiveOrganizationCalendarFieldsDiscrepancy();
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FeaturesSet, FeaturesSet.centralizedPeriodsManagement> e)
  {
    if (e.Row == null)
      return;
    bool? nullable = (bool?) ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<FeaturesSet, FeaturesSet.centralizedPeriodsManagement>, FeaturesSet, object>) e).OldValue;
    if (!nullable.GetValueOrDefault())
    {
      nullable = e.Row.CentralizedPeriodsManagement;
      if (nullable.GetValueOrDefault() && this.EtalonOrganization != null)
      {
        using (IEnumerator<PXResult<MasterFinPeriod>> enumerator = ((PXSelectBase<MasterFinPeriod>) this.MasterFinPeriods).Select(new object[1]
        {
          (object) this.EtalonOrganization.OrganizationID
        }).GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            PXResult<MasterFinPeriod, OrganizationFinPeriod> current = (PXResult<MasterFinPeriod, OrganizationFinPeriod>) enumerator.Current;
            MasterFinPeriod masterFinPeriod = PXResult<MasterFinPeriod, OrganizationFinPeriod>.op_Implicit(current);
            OrganizationFinPeriod organizationFinPeriod = PXResult<MasterFinPeriod, OrganizationFinPeriod>.op_Implicit(current);
            masterFinPeriod.DateLocked = organizationFinPeriod.DateLocked;
            masterFinPeriod.Status = organizationFinPeriod.Status;
            masterFinPeriod.APClosed = organizationFinPeriod.APClosed;
            masterFinPeriod.ARClosed = organizationFinPeriod.ARClosed;
            masterFinPeriod.INClosed = organizationFinPeriod.INClosed;
            masterFinPeriod.CAClosed = organizationFinPeriod.CAClosed;
            masterFinPeriod.FAClosed = organizationFinPeriod.FAClosed;
            ((PXSelectBase) this.MasterFinPeriods).Cache.Update((object) masterFinPeriod);
          }
          return;
        }
      }
    }
    ((PXSelectBase) this.MasterFinPeriods).Cache.Clear();
  }

  private string GetProblemFields(PX.Objects.GL.DAC.Organization organization, OrganizationFinPeriod problemPeriod)
  {
    OrganizationFinPeriod organizationFinPeriod = PXResultset<OrganizationFinPeriod>.op_Implicit(PXSelectBase<OrganizationFinPeriod, PXSelect<OrganizationFinPeriod, Where<OrganizationFinPeriod.organizationID, Equal<Required<OrganizationFinPeriod.organizationID>>, And<OrganizationFinPeriod.finPeriodID, Equal<Required<OrganizationFinPeriod.finPeriodID>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) organization.OrganizationID,
      (object) problemPeriod.FinPeriodID
    }));
    List<string> stringList = new List<string>();
    bool? dateLocked1 = problemPeriod.DateLocked;
    bool? dateLocked2 = organizationFinPeriod.DateLocked;
    if (!(dateLocked1.GetValueOrDefault() == dateLocked2.GetValueOrDefault() & dateLocked1.HasValue == dateLocked2.HasValue))
      stringList.Add("DateLocked");
    if (problemPeriod.Status != organizationFinPeriod.Status)
      stringList.Add("Status");
    bool? apClosed1 = problemPeriod.APClosed;
    bool? apClosed2 = organizationFinPeriod.APClosed;
    if (!(apClosed1.GetValueOrDefault() == apClosed2.GetValueOrDefault() & apClosed1.HasValue == apClosed2.HasValue))
      stringList.Add("APClosed");
    bool? arClosed1 = problemPeriod.ARClosed;
    bool? arClosed2 = organizationFinPeriod.ARClosed;
    if (!(arClosed1.GetValueOrDefault() == arClosed2.GetValueOrDefault() & arClosed1.HasValue == arClosed2.HasValue))
      stringList.Add("ARClosed");
    bool? inClosed1 = problemPeriod.INClosed;
    bool? inClosed2 = organizationFinPeriod.INClosed;
    if (!(inClosed1.GetValueOrDefault() == inClosed2.GetValueOrDefault() & inClosed1.HasValue == inClosed2.HasValue))
      stringList.Add("INClosed");
    bool? caClosed1 = problemPeriod.CAClosed;
    bool? caClosed2 = organizationFinPeriod.CAClosed;
    if (!(caClosed1.GetValueOrDefault() == caClosed2.GetValueOrDefault() & caClosed1.HasValue == caClosed2.HasValue))
      stringList.Add("CAClosed");
    bool? faClosed1 = problemPeriod.FAClosed;
    bool? faClosed2 = organizationFinPeriod.FAClosed;
    if (!(faClosed1.GetValueOrDefault() == faClosed2.GetValueOrDefault() & faClosed1.HasValue == faClosed2.HasValue))
      stringList.Add("FAClosed");
    return string.Join(", ", stringList.ToArray());
  }

  protected virtual void _(PX.Data.Events.FieldUpdating<FeaturesSet.aSC606> e)
  {
    ((PX.Data.Events.FieldUpdatingBase<PX.Data.Events.FieldUpdating<FeaturesSet.aSC606>>) e).NewValue = PXBoolAttribute.ConvertValue(((PX.Data.Events.FieldUpdatingBase<PX.Data.Events.FieldUpdating<FeaturesSet.aSC606>>) e).NewValue);
    FeaturesSet row = (FeaturesSet) e.Row;
    if (row == null)
      return;
    bool? asC606 = row.ASC606;
    if (!row.ASC606.HasValue)
      return;
    bool? nullable1 = asC606;
    bool newValue = (bool) ((PX.Data.Events.FieldUpdatingBase<PX.Data.Events.FieldUpdating<FeaturesSet.aSC606>>) e).NewValue;
    if (nullable1.GetValueOrDefault() == newValue & nullable1.HasValue)
      return;
    int? rowCount = PXSelectBase<ARTranAlias, PXSelectGroupBy<ARTranAlias, Aggregate<Count>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, Array.Empty<object>()).RowCount;
    int? nullable2 = rowCount;
    int num1 = 0;
    if (nullable2.GetValueOrDefault() > num1 & nullable2.HasValue)
    {
      if (((PXSelectBase<FeaturesSet>) this.Features).Ask(PXMessages.LocalizeFormatNoPrefixNLA("There are {0} unreleased AR or SO documents with DR codes. Switching to this mode before releasing them may lead to unwanted results. Do you want to continue?", new object[1]
      {
        (object) rowCount
      }), (MessageButtons) 4) != 6)
      {
        ((PX.Data.Events.FieldUpdatingBase<PX.Data.Events.FieldUpdating<FeaturesSet.aSC606>>) e).NewValue = (object) asC606;
        ((PX.Data.Events.FieldUpdatingBase<PX.Data.Events.FieldUpdating<FeaturesSet.aSC606>>) e).Cancel = true;
        return;
      }
    }
    int num2 = (bool) ((PX.Data.Events.FieldUpdatingBase<PX.Data.Events.FieldUpdating<FeaturesSet.aSC606>>) e).NewValue ? 1 : 0;
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<FeaturesSet, FeaturesSet.multipleCalendarsSupport> e)
  {
    FeaturesSet current = (FeaturesSet) ((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<FeaturesSet, FeaturesSet.multipleCalendarsSupport>>) e).Cache.Current;
    bool? nullable;
    int num;
    if (current == null)
    {
      num = 0;
    }
    else
    {
      nullable = current.CentralizedPeriodsManagement;
      num = nullable.GetValueOrDefault() ? 1 : 0;
    }
    if (num == 0)
      return;
    nullable = (bool?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<FeaturesSet, FeaturesSet.multipleCalendarsSupport>, FeaturesSet, object>) e).NewValue;
    if (!nullable.GetValueOrDefault())
      return;
    nullable = (bool?) e.OldValue;
    bool flag = false;
    if (nullable.GetValueOrDefault() == flag & nullable.HasValue)
    {
      ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<FeaturesSet, FeaturesSet.multipleCalendarsSupport>, FeaturesSet, object>) e).NewValue = e.OldValue;
      throw new PXSetPropertyException("To enable this feature, disable {0} first.", new object[1]
      {
        (object) PXUIFieldAttribute.GetDisplayName<FeaturesSet.centralizedPeriodsManagement>(((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<FeaturesSet, FeaturesSet.multipleCalendarsSupport>>) e).Cache)
      });
    }
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<FeaturesSet, FeaturesSet.centralizedPeriodsManagement> e)
  {
    FeaturesSet current1 = (FeaturesSet) ((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<FeaturesSet, FeaturesSet.centralizedPeriodsManagement>>) e).Cache.Current;
    bool? nullable;
    int num1;
    if (current1 == null)
    {
      num1 = 0;
    }
    else
    {
      nullable = current1.MultipleCalendarsSupport;
      num1 = nullable.GetValueOrDefault() ? 1 : 0;
    }
    if (num1 != 0)
    {
      nullable = (bool?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<FeaturesSet, FeaturesSet.centralizedPeriodsManagement>, FeaturesSet, object>) e).NewValue;
      if (nullable.GetValueOrDefault())
      {
        nullable = (bool?) e.OldValue;
        bool flag = false;
        if (nullable.GetValueOrDefault() == flag & nullable.HasValue)
        {
          ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<FeaturesSet, FeaturesSet.centralizedPeriodsManagement>, FeaturesSet, object>) e).NewValue = e.OldValue;
          throw new PXSetPropertyException("To enable this feature, disable {0} first.", new object[1]
          {
            (object) PXUIFieldAttribute.GetDisplayName<FeaturesSet.multipleCalendarsSupport>(((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<FeaturesSet, FeaturesSet.centralizedPeriodsManagement>>) e).Cache)
          });
        }
      }
    }
    FeaturesSet current2 = (FeaturesSet) ((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<FeaturesSet, FeaturesSet.centralizedPeriodsManagement>>) e).Cache.Current;
    int num2;
    if (current2 == null)
    {
      num2 = 0;
    }
    else
    {
      nullable = current2.MultiCompany;
      bool flag = false;
      num2 = nullable.GetValueOrDefault() == flag & nullable.HasValue ? 1 : 0;
    }
    if (num2 == 0)
      return;
    nullable = (bool?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<FeaturesSet, FeaturesSet.centralizedPeriodsManagement>, FeaturesSet, object>) e).NewValue;
    bool flag1 = false;
    if (!(nullable.GetValueOrDefault() == flag1 & nullable.HasValue))
      return;
    nullable = (bool?) e.OldValue;
    if (nullable.GetValueOrDefault())
    {
      ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<FeaturesSet, FeaturesSet.centralizedPeriodsManagement>, FeaturesSet, object>) e).NewValue = e.OldValue;
      throw new PXSetPropertyException("To disable this feature, enable {0} first.", new object[1]
      {
        (object) PXUIFieldAttribute.GetDisplayName<FeaturesSet.multiCompany>(((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<FeaturesSet, FeaturesSet.centralizedPeriodsManagement>>) e).Cache)
      });
    }
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<FeaturesSet, FeaturesSet.multiCompany> e)
  {
    FeaturesSet current = (FeaturesSet) ((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<FeaturesSet, FeaturesSet.multiCompany>>) e).Cache.Current;
    int num;
    if (current == null)
    {
      num = 0;
    }
    else
    {
      bool? periodsManagement = current.CentralizedPeriodsManagement;
      bool flag = false;
      num = periodsManagement.GetValueOrDefault() == flag & periodsManagement.HasValue ? 1 : 0;
    }
    if (num == 0)
      return;
    bool? nullable = (bool?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<FeaturesSet, FeaturesSet.multiCompany>, FeaturesSet, object>) e).NewValue;
    bool flag1 = false;
    if (!(nullable.GetValueOrDefault() == flag1 & nullable.HasValue))
      return;
    nullable = (bool?) e.OldValue;
    if (nullable.GetValueOrDefault())
    {
      ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<FeaturesSet, FeaturesSet.multiCompany>, FeaturesSet, object>) e).NewValue = e.OldValue;
      throw new PXSetPropertyException("To disable this feature, enable {0} first.", new object[1]
      {
        (object) PXUIFieldAttribute.GetDisplayName<FeaturesSet.centralizedPeriodsManagement>(((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<FeaturesSet, FeaturesSet.multiCompany>>) e).Cache)
      });
    }
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<FeaturesSet.bankFeedAccountsMultipleMapping> e)
  {
    bool? newValue = ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<FeaturesSet.bankFeedAccountsMultipleMapping>, object, object>) e).NewValue as bool?;
    bool? oldValue = e.OldValue as bool?;
    bool? nullable = newValue;
    bool flag = false;
    if (nullable.GetValueOrDefault() == flag & nullable.HasValue && oldValue.GetValueOrDefault() && this.CheckBankFeedsWithMultipleMappingExist())
    {
      ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<FeaturesSet.bankFeedAccountsMultipleMapping>, object, object>) e).NewValue = e.OldValue;
      throw new PXSetPropertyException("There are bank feeds that use the Mapping of Multiple Accounts for Bank Feeds feature. Clear the Map Multiple Bank Accounts to One Cash Account check box on the Bank Feeds (CA205500) form for all bank feeds and try again.");
    }
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<FeaturesSet.bankFeedIntegration> e)
  {
    bool flag1 = this.BankFeedClient.IsConfigured();
    FeaturesSet row = e.Row as FeaturesSet;
    bool? newValue = (bool?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<FeaturesSet.bankFeedIntegration>, object, object>) e).NewValue;
    bool? oldValue = (bool?) e.OldValue;
    if (newValue.GetValueOrDefault())
    {
      bool? nullable = oldValue;
      bool flag2 = false;
      if (nullable.GetValueOrDefault() == flag2 & nullable.HasValue && !flag1)
      {
        ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<FeaturesSet.bankFeedIntegration>, object, object>) e).NewValue = (object) oldValue;
        throw new PXSetPropertyException("The feature cannot be enabled because the current license does not include the support of cloud services.");
      }
    }
    bool? nullable1 = newValue;
    bool flag3 = false;
    if (nullable1.GetValueOrDefault() == flag3 & nullable1.HasValue && oldValue.GetValueOrDefault() && row.BankFeedAccountsMultipleMapping.GetValueOrDefault() && this.CheckBankFeedsWithMultipleMappingExist())
    {
      ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<FeaturesSet.bankFeedIntegration>, object, object>) e).NewValue = (object) oldValue;
      throw new PXSetPropertyException("There are bank feeds that use the Mapping of Multiple Accounts for Bank Feeds feature. Clear the Map Multiple Bank Accounts to One Cash Account check box on the Bank Feeds (CA205500) form for all bank feeds and try again.");
    }
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FeaturesSet.bankFeedIntegration> e)
  {
    FeaturesSet row = e.Row as FeaturesSet;
    bool? newValue = e.NewValue as bool?;
    bool? oldValue = ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<FeaturesSet.bankFeedIntegration>, object, object>) e).OldValue as bool?;
    PXCache cache = ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FeaturesSet.bankFeedIntegration>>) e).Cache;
    if (row == null)
      return;
    bool? nullable = newValue;
    bool flag = false;
    if (!(nullable.GetValueOrDefault() == flag & nullable.HasValue) || !oldValue.GetValueOrDefault())
      return;
    cache.SetValueExt<FeaturesSet.bankFeedAccountsMultipleMapping>((object) row, (object) false);
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<FeaturesSet.imageRecognition> e)
  {
    FeaturesMaint.VerifyRecognitionFeature<FeaturesSet.imageRecognition>(e, this.InvoiceRecognitionService.IsConfigured());
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<FeaturesSet.apDocumentRecognition> e)
  {
    FeaturesMaint.VerifyRecognitionFeature<FeaturesSet.apDocumentRecognition>(e, this.InvoiceRecognitionService.IsConfigured());
  }

  public static void VerifyRecognitionFeature<F>(PX.Data.Events.FieldVerifying<F> e, bool IsConfigured) where F : class, IBqlField
  {
    int num;
    if (((bool?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<F>, object, object>) e).NewValue).GetValueOrDefault())
    {
      bool? oldValue = (bool?) e.OldValue;
      bool flag = false;
      num = oldValue.GetValueOrDefault() == flag & oldValue.HasValue ? 1 : 0;
    }
    else
      num = 0;
    if (num != 0 && !IsConfigured)
    {
      ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<F>, object, object>) e).NewValue = (object) false;
      PXLicenseState state = PXLicenseHelper.License.State;
      throw (state == 4 ? 1 : (state == 6 ? 1 : 0)) != 0 ? new PXSetPropertyException("To enable this feature, activate the license that includes this feature on the Activate License (SM201510) form.") : new PXSetPropertyException("The feature cannot be enabled because the current license does not include the support of cloud services.");
    }
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FeaturesSet, FeaturesSet.construction> e)
  {
    if (e.Row == null)
      return;
    this.UpdateProjectsWorkspace();
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FeaturesSet, FeaturesSet.professionalServices> e)
  {
    if (e.Row == null)
      return;
    this.UpdateProjectsWorkspace();
  }

  private void UpdateProjectsWorkspace()
  {
    MUIWorkspace muiWorkspace = PXResultset<MUIWorkspace>.op_Implicit(((PXSelectBase<MUIWorkspace>) this.Workspace).Select(new object[1]
    {
      (object) Guid.Parse("6dbfa68e-79e9-420b-9f64-e1036a28998c")
    }));
    if (muiWorkspace == null)
      return;
    bool valueOrDefault = ((PXSelectBase<FeaturesSet>) this.Features).Current.Construction.GetValueOrDefault();
    if (((PXSelectBase<FeaturesSet>) this.Features).Current.ProfessionalServices.GetValueOrDefault())
    {
      muiWorkspace.Title = "Professional Services";
      muiWorkspace.Icon = "person_with_checkmarks";
    }
    else if (valueOrDefault)
    {
      muiWorkspace.Title = "Construction";
      muiWorkspace.Icon = "cran";
    }
    else
    {
      muiWorkspace.Title = "Projects";
      muiWorkspace.Icon = "project";
    }
    ((PXSelectBase<MUIWorkspace>) this.Workspace).Update(muiWorkspace);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdating<FeaturesSet.contactDuplicate> e)
  {
    if (!(((PX.Data.Events.FieldUpdatingBase<PX.Data.Events.FieldUpdating<FeaturesSet.contactDuplicate>>) e).NewValue is bool newValue) || !newValue || !(e.OldValue is bool oldValue) || oldValue)
      return;
    foreach (PXResult<CRValidation> pxResult in ((PXSelectBase<CRValidation>) this.CRDefaultValidations).Select(Array.Empty<object>()))
    {
      CRValidation crValidation = PXResult<CRValidation>.op_Implicit(pxResult);
      crValidation.GramValidationDateTime = new DateTime?(PXTimeZoneInfo.Now);
      ((PXSelectBase) this.CRDefaultValidations).Cache.Update((object) crValidation);
    }
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<FeaturesSet.contactDuplicate> e)
  {
    if (!(((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<FeaturesSet.contactDuplicate>, object, object>) e).NewValue is bool newValue) || !newValue || !(e.OldValue is bool oldValue) || oldValue || !((IQueryable<PXResult<PX.Objects.CR.Contact>>) PXSelectBase<PX.Objects.CR.Contact, PXViewOf<PX.Objects.CR.Contact>.BasedOn<SelectFromBase<PX.Objects.CR.Contact, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<CRGramValidationDateTime.ByLead>.On<BqlOperand<True, IBqlBool>.IsEqual<True>>>, FbqlJoins.Inner<CRGramValidationDateTime.ByContact>.On<BqlOperand<True, IBqlBool>.IsEqual<True>>>, FbqlJoins.Inner<CRGramValidationDateTime.ByBAccount>.On<BqlOperand<True, IBqlBool>.IsEqual<True>>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.CR.Contact.contactType, Equal<ContactTypesAttribute.lead>>>>>.And<BqlOperand<PX.Objects.CR.Contact.grammValidationDateTime, IBqlDateTime>.IsLess<CRGramValidationDateTime.ByLead.value>>>>, Or<Brackets<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.CR.Contact.contactType, Equal<ContactTypesAttribute.person>>>>>.And<BqlOperand<PX.Objects.CR.Contact.grammValidationDateTime, IBqlDateTime>.IsLess<CRGramValidationDateTime.ByContact.value>>>>>>.Or<Brackets<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.CR.Contact.contactType, Equal<ContactTypesAttribute.bAccountProperty>>>>>.And<BqlOperand<PX.Objects.CR.Contact.grammValidationDateTime, IBqlDateTime>.IsLess<CRGramValidationDateTime.ByBAccount.value>>>>>>.Config>.Select((PXGraph) this, Array.Empty<object>())).Any<PXResult<PX.Objects.CR.Contact>>())
      return;
    ((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<FeaturesSet.contactDuplicate>>) e).Cache.RaiseExceptionHandling<FeaturesSet.contactDuplicate>(e.Row, ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<FeaturesSet.contactDuplicate>, object, object>) e).NewValue, (Exception) new PXSetPropertyException<FeaturesSet.contactDuplicate>("Validation scores have not been calculated for the existing leads, contacts, or business accounts. Before you start validating records for duplicates, calculate validation scores on the Calculate Grams (CR503400) form.", (PXErrorLevel) 2));
  }

  private void CheckCompaniesOnDifferBaseCury()
  {
    if (GraphHelper.RowCast<PX.Objects.GL.DAC.Organization>((IEnumerable) PXSelectBase<PX.Objects.GL.DAC.Organization, PXSelectReadonly<PX.Objects.GL.DAC.Organization, Where<PX.Objects.GL.DAC.Organization.active, Equal<True>, And<PX.Objects.GL.DAC.Organization.baseCuryID, NotEqual<Optional2<PX.Objects.GL.DAC.Organization.baseCuryID>>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) ((PXSelectBase<PX.Objects.GL.Company>) this.Company).Current.BaseCuryID
    })).Select<PX.Objects.GL.DAC.Organization, string>((Func<PX.Objects.GL.DAC.Organization, string>) (org => org.OrganizationCD.Trim())).ToList<string>().Any<string>())
      throw new PXSetPropertyException("This feature cannot be disabled because there are companies with different base currencies in the system.");
  }

  private bool CheckBankFeedsWithMultipleMappingExist()
  {
    return ((IQueryable<PXResult<CABankFeed>>) PXSelectBase<CABankFeed, PXViewOf<CABankFeed>.BasedOn<SelectFromBase<CABankFeed, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<CABankFeed.multipleMapping, IBqlBool>.IsEqual<True>>>.Config>.Select((PXGraph) this, Array.Empty<object>())).Any<PXResult<CABankFeed>>();
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdating<FeaturesSet, FeaturesSet.multipleBaseCurrencies> e)
  {
    if (e.Row == null)
      return;
    bool? newValue = (bool?) ((PX.Data.Events.FieldUpdatingBase<PX.Data.Events.FieldUpdating<FeaturesSet, FeaturesSet.multipleBaseCurrencies>>) e).NewValue;
    if (!newValue.GetValueOrDefault())
    {
      this.CheckCompaniesOnDifferBaseCury();
    }
    else
    {
      newValue = (bool?) ((PX.Data.Events.FieldUpdatingBase<PX.Data.Events.FieldUpdating<FeaturesSet, FeaturesSet.multipleBaseCurrencies>>) e).NewValue;
      if (!newValue.GetValueOrDefault())
        return;
      ARSetup arSetup = PXResultset<ARSetup>.op_Implicit(PXSetup<ARSetup>.Select((PXGraph) this, Array.Empty<object>()));
      if (arSetup?.PrepareStatements == "A" && arSetup?.PrepareDunningLetters == "A")
        throw new PXSetPropertyException("Customer statements and dunning letters cannot be prepared, because it is not allowed to consolidate statements and dunning letters for all companies if the Multiple Base Currencies feature is enabled. Select different options in the Prepare Statements and Prepare Dunning Letters boxes on the Accounts Receivable Preferences (AR101000) form.");
      if (arSetup?.PrepareStatements == "A")
        throw new PXSetPropertyException("Customer statements cannot be prepared because it is not allowed to consolidate statements for all companies if the Multiple Base Currencies feature is enabled. Select a different option in the Prepare Statements box on the Accounts Receivable Preferences (AR101000) form.");
      if (arSetup?.PrepareDunningLetters == "A")
        throw new PXSetPropertyException("Dunning letters cannot be prepared because it is not allowed to consolidate dunning letters for all companies if the Multiple Base Currencies feature is enabled. Select a different option in the Prepare Dunning Letters box on the Accounts Receivable Preferences (AR101000) form.");
    }
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<FeaturesSet, FeaturesSet.multipleBaseCurrencies> e)
  {
    if (e.Row.ProjectAccounting.GetValueOrDefault() && !e.Row.ProjectMultiCurrency.GetValueOrDefault() && ((bool?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<FeaturesSet, FeaturesSet.multipleBaseCurrencies>, FeaturesSet, object>) e).NewValue).GetValueOrDefault())
    {
      string empty1 = string.Empty;
      string empty2 = string.Empty;
      FeaturesSet row1 = e.Row;
      int num1;
      if (row1 == null)
      {
        num1 = 0;
      }
      else
      {
        bool? multiCompany = row1.MultiCompany;
        bool flag = false;
        num1 = multiCompany.GetValueOrDefault() == flag & multiCompany.HasValue ? 1 : 0;
      }
      if (num1 != 0)
      {
        string displayName = PXUIFieldAttribute.GetDisplayName<FeaturesSet.multiCompany>(((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<FeaturesSet, FeaturesSet.multipleBaseCurrencies>>) e).Cache);
        empty1 += !string.IsNullOrEmpty(empty1) ? $", {displayName}" : displayName;
      }
      FeaturesSet row2 = e.Row;
      int num2;
      if (row2 == null)
      {
        num2 = 0;
      }
      else
      {
        bool? multicurrency = row2.Multicurrency;
        bool flag = false;
        num2 = multicurrency.GetValueOrDefault() == flag & multicurrency.HasValue ? 1 : 0;
      }
      if (num2 != 0)
      {
        string displayName = PXUIFieldAttribute.GetDisplayName<FeaturesSet.multicurrency>(((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<FeaturesSet, FeaturesSet.multipleBaseCurrencies>>) e).Cache);
        empty1 += !string.IsNullOrEmpty(empty1) ? $", {displayName}" : displayName;
      }
      FeaturesSet row3 = e.Row;
      int num3;
      if (row3 == null)
      {
        num3 = 0;
      }
      else
      {
        bool? visibilityRestriction = row3.VisibilityRestriction;
        bool flag = false;
        num3 = visibilityRestriction.GetValueOrDefault() == flag & visibilityRestriction.HasValue ? 1 : 0;
      }
      if (num3 != 0)
      {
        string displayName = PXUIFieldAttribute.GetDisplayName<FeaturesSet.visibilityRestriction>(((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<FeaturesSet, FeaturesSet.multipleBaseCurrencies>>) e).Cache);
        empty1 += !string.IsNullOrEmpty(empty1) ? $", {displayName}" : displayName;
      }
      string displayName1 = PXUIFieldAttribute.GetDisplayName<FeaturesSet.projectMultiCurrency>(((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<FeaturesSet, FeaturesSet.multipleBaseCurrencies>>) e).Cache);
      throw new PXSetPropertyException("To enable this feature, enable the following features first: {0}.", new object[1]
      {
        (object) (empty1 + (!string.IsNullOrEmpty(empty1) ? $", {displayName1}" : displayName1))
      });
    }
  }

  protected virtual void _(PX.Data.Events.RowPersisting<FeaturesSet> e)
  {
    FeaturesSet row = e.Row;
    if (row == null || !row.PayrollModule.GetValueOrDefault() || row.PayrollUS.GetValueOrDefault() || row.PayrollCAN.GetValueOrDefault())
      return;
    PXUIFieldAttribute.SetError<FeaturesSet.payrollModule>(((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<FeaturesSet>>) e).Cache, (object) e.Row, "US Payroll or Canadian Payroll should be enabled.");
  }

  protected virtual void _(PX.Data.Events.FieldUpdated<FeaturesSet.payrollUS> e)
  {
    if (!(e.Row is FeaturesSet row) || !row.PayrollUS.GetValueOrDefault())
      return;
    row.PayrollCAN = new bool?(false);
  }

  protected virtual void _(PX.Data.Events.FieldUpdated<FeaturesSet.payrollCAN> e)
  {
    if (!(e.Row is FeaturesSet row) || !row.PayrollCAN.GetValueOrDefault())
      return;
    row.PayrollUS = new bool?(false);
  }

  protected virtual void _(PX.Data.Events.RowUpdated<FeaturesSet> e)
  {
    if (e.Row == null)
      return;
    FeaturesSet row = e.Row;
    bool? nullable1 = e.Row.Construction;
    int num;
    if (nullable1.GetValueOrDefault())
    {
      nullable1 = e.Row.PayrollUS;
      num = nullable1.GetValueOrDefault() ? 1 : 0;
    }
    else
      num = 0;
    bool? nullable2 = new bool?(num != 0);
    row.PayrollConstruction = nullable2;
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdating<FeaturesSet, FeaturesSet.projectMultiCurrency> e)
  {
    if (e.Row == null || ((bool?) ((PX.Data.Events.FieldUpdatingBase<PX.Data.Events.FieldUpdating<FeaturesSet, FeaturesSet.projectMultiCurrency>>) e).NewValue).GetValueOrDefault())
      return;
    this.CheckCompaniesOnDifferBaseCury();
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FeaturesSet, FeaturesSet.projectMultiCurrency> e)
  {
    if (e.Row == null)
      return;
    bool? nullable = (bool?) e.NewValue;
    if (nullable.GetValueOrDefault())
      return;
    nullable = e.Row.ProjectAccounting;
    if (!nullable.GetValueOrDefault())
      return;
    e.Row.MultipleBaseCurrencies = new bool?(false);
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<FeaturesSet, FeaturesSet.projectMultiCurrency> e)
  {
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<FeaturesSet, FeaturesSet.projectMultiCurrency>, FeaturesSet, object>) e).NewValue = (object) (bool) (!e.Row.ProjectAccounting.GetValueOrDefault() ? 0 : (e.Row.MultipleBaseCurrencies.GetValueOrDefault() ? 1 : 0));
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FeaturesSet, FeaturesSet.projectAccounting> e)
  {
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FeaturesSet, FeaturesSet.projectAccounting>>) e).Cache.SetDefaultExt<FeaturesSet.projectMultiCurrency>((object) e.Row);
    this.UpdateProjectsWorkspace();
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FeaturesSet, FeaturesSet.glAnomalyDetection> e)
  {
    if (!(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FeaturesSet, FeaturesSet.glAnomalyDetection>>) e).Cache.GetStateExt<FeaturesSet.glAnomalyDetection>((object) e.Row) is PXFieldState stateExt) || stateExt.ErrorLevel == 4 || !((bool?) stateExt.Value).GetValueOrDefault())
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FeaturesSet, FeaturesSet.glAnomalyDetection>>) e).Cache.RaiseExceptionHandling<FeaturesSet.glAnomalyDetection>((object) e.Row, (object) e.Row.GLAnomalyDetection, (Exception) new PXSetPropertyException("This is an experimental feature. Please see the release notes for details. In the future, this item can be substantially changed requiring a reimplementation.", (PXErrorLevel) 2));
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FeaturesSet, FeaturesSet.relatedItemAssistant> e)
  {
    if (!(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FeaturesSet, FeaturesSet.relatedItemAssistant>>) e).Cache.GetStateExt<FeaturesSet.relatedItemAssistant>((object) e.Row) is PXFieldState stateExt) || stateExt.ErrorLevel == 4 || !((bool?) stateExt.Value).GetValueOrDefault())
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FeaturesSet, FeaturesSet.relatedItemAssistant>>) e).Cache.RaiseExceptionHandling<FeaturesSet.relatedItemAssistant>((object) e.Row, (object) e.Row.RelatedItemAssistant, (Exception) new PXSetPropertyException("This is an experimental feature. Please see the release notes for details. In the future, this item can be substantially changed requiring a reimplementation.", (PXErrorLevel) 2));
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FeaturesSet, FeaturesSet.importSendGridDesigns> e)
  {
    if (!(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FeaturesSet, FeaturesSet.importSendGridDesigns>>) e).Cache.GetStateExt<FeaturesSet.importSendGridDesigns>((object) e.Row) is PXFieldState stateExt) || stateExt.ErrorLevel == 4 || !((bool?) stateExt.Value).GetValueOrDefault())
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FeaturesSet, FeaturesSet.importSendGridDesigns>>) e).Cache.RaiseExceptionHandling<FeaturesSet.importSendGridDesigns>((object) e.Row, (object) e.Row.ImportSendGridDesigns, (Exception) new PXSetPropertyException("This is an experimental feature. Please see the release notes for details. In the future, this item can be discontinued.", (PXErrorLevel) 2));
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<FeaturesSet, FeaturesSet.sendGridIntegration> e)
  {
    if (!(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FeaturesSet, FeaturesSet.sendGridIntegration>>) e).Cache.GetStateExt<FeaturesSet.sendGridIntegration>((object) e.Row) is PXFieldState stateExt) || stateExt.ErrorLevel == 4 || ((bool?) stateExt.Value).GetValueOrDefault())
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FeaturesSet, FeaturesSet.sendGridIntegration>>) e).Cache.SetValueExt<FeaturesSet.importSendGridDesigns>((object) e.Row, (object) false);
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<FeaturesSet.giAnomalyDetection> e)
  {
    int num;
    if (((bool?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<FeaturesSet.giAnomalyDetection>, object, object>) e).NewValue).GetValueOrDefault())
    {
      bool? oldValue = (bool?) e.OldValue;
      bool flag = false;
      num = oldValue.GetValueOrDefault() == flag & oldValue.HasValue ? 1 : 0;
    }
    else
      num = 0;
    if (num == 0 || this.MLGIAnomaliesService.IsConfigured)
      return;
    ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<FeaturesSet.giAnomalyDetection>, object, object>) e).NewValue = (object) false;
    PXUIFieldAttribute.SetError<FeaturesSet.giAnomalyDetection>(((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<FeaturesSet.giAnomalyDetection>>) e).Cache, e.Row, "To enable this feature, activate a license that includes it.");
  }
}
