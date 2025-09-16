// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.TemplateMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.AR;
using PX.Objects.CA;
using PX.Objects.Common.Extensions;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.CT;
using PX.Objects.EP;
using PX.Objects.Extensions.MultiCurrency;
using PX.Objects.GL;
using PX.Objects.IN;
using PX.SM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

#nullable enable
namespace PX.Objects.PM;

public class TemplateMaint : PXGraph<
#nullable disable
TemplateMaint, PMProject>, PXImportAttribute.IPXPrepareItems
{
  public FbqlSelect<SelectFromBase<PMProject, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  PMProject.baseType, 
  #nullable disable
  Equal<CTPRType.projectTemplate>>>>>.And<MatchUser>>, PMProject>.View Project;
  public PXSelect<PMProject, Where<PMProject.contractID, Equal<Current<PMProject.contractID>>>> ProjectProperties;
  public PXSelect<ContractBillingSchedule, Where<ContractBillingSchedule.contractID, Equal<Current<PMProject.contractID>>>> Billing;
  [PXImport(typeof (PMProject))]
  [PXFilterable(new System.Type[] {})]
  public PXSelect<PMTask, Where<PMTask.projectID, Equal<Current<PMProject.contractID>>>> Tasks;
  [PXFilterable(new System.Type[] {})]
  public PXSelectJoin<EPEquipmentRate, InnerJoin<EPEquipment, On<EPEquipmentRate.equipmentID, Equal<EPEquipment.equipmentID>>>, Where<EPEquipmentRate.projectID, Equal<Current<PMProject.contractID>>>> EquipmentRates;
  public PXSelect<PMAccountTask, Where<PMAccountTask.projectID, Equal<Current<PMProject.contractID>>>> Accounts;
  public PXSetup<PMSetup> Setup;
  public PXSetup<PX.Objects.GL.Company> Company;
  [PXCopyPasteHiddenFields(new System.Type[] {typeof (PMRevenueBudget.completedPct), typeof (PMRevenueBudget.revisedQty), typeof (PMRevenueBudget.curyRevisedAmount), typeof (PMRevenueBudget.curyAmountToInvoice)})]
  [PXImport(typeof (PMProject))]
  [PXFilterable(new System.Type[] {})]
  public PXSelect<PMRevenueBudget, Where<PMRevenueBudget.projectID, Equal<Current<PMProject.contractID>>, And<PMRevenueBudget.type, Equal<AccountType.income>>>> RevenueBudget;
  [PXCopyPasteHiddenFields(new System.Type[] {typeof (PMCostBudget.revisedQty), typeof (PMCostBudget.curyRevisedAmount), typeof (PMBudget.curyCostToComplete), typeof (PMBudget.curyCostAtCompletion), typeof (PMBudget.completedPct)})]
  [PXImport(typeof (PMProject))]
  [PXFilterable(new System.Type[] {})]
  public PXSelect<PMCostBudget, Where<PMCostBudget.projectID, Equal<Current<PMProject.contractID>>, And<PMCostBudget.type, Equal<AccountType.expense>>>> CostBudget;
  public PXSelectJoin<EPEmployeeContract, InnerJoin<PX.Objects.EP.EPEmployee, On<PX.Objects.EP.EPEmployee.bAccountID, Equal<EPEmployeeContract.employeeID>>>, Where<EPEmployeeContract.contractID, Equal<Current<PMProject.contractID>>>> EmployeeContract;
  public PXSelectJoin<EPContractRate, LeftJoin<PX.Objects.IN.InventoryItem, On<PX.Objects.IN.InventoryItem.inventoryID, Equal<EPContractRate.labourItemID>>, LeftJoin<EPEarningType, On<EPEarningType.typeCD, Equal<EPContractRate.earningType>>>>, Where<EPContractRate.employeeID, Equal<Optional<EPEmployeeContract.employeeID>>, And<EPContractRate.contractID, Equal<Optional<PMProject.contractID>>>>, OrderBy<Asc<EPContractRate.contractID>>> ContractRates;
  public PXSelect<PMRetainageStep, Where<PMRetainageStep.projectID, Equal<Current<PMProject.contractID>>>, OrderBy<Asc<PMRetainageStep.thresholdPct>>> RetainageSteps;
  [PXHidden]
  public PXSelect<PMRecurringItem, Where<PMRecurringItem.projectID, Equal<Current<PMTask.projectID>>>> BillingItems;
  [PXViewName("Project Answers")]
  public TemplateAttributeList<PMProject> Answers;
  [PXHidden]
  public TemplateAttributeList<PMTask> TaskAnswers;
  public PXSelect<CSAnswers, Where<CSAnswers.refNoteID, Equal<Required<PMProject.noteID>>>> Answer;
  public CRNotificationSourceList<PMProject, PMProject.classID, PMNotificationSource.project> NotificationSources;
  public PXSelect<NotificationRecipient, Where<NotificationRecipient.sourceID, Equal<Optional<NotificationSource.sourceID>>>> NotificationRecipients;
  public PXSelectJoin<PMProjectContact, LeftJoin<PX.Objects.CR.Contact, On<PX.Objects.CR.Contact.contactID, Equal<PMProjectContact.contactID>>>, Where<PMProjectContact.projectID, Equal<Current<PMProject.contractID>>>, OrderBy<Asc<PX.Objects.CR.Contact.displayName>>> ProjectContacts;
  [PXCopyPasteHiddenView]
  [PXHidden]
  public PXSelect<PMCostCode> dummyCostCode;
  public PXFilter<TemplateMaint.CopyDialogInfo> CopyDialog;
  public ChangeProjectID ChangeID;
  public PXAction<PMProject> viewTask;
  public PXAction<PMProject> copyTemplate;
  public PXAction<PMProject> updateRetainage;
  private Dictionary<int?, int?> persistedTask = new Dictionary<int?, int?>();
  private int? negativeKey;

  [PXDimensionSelector("TMPROJECT", typeof (FbqlSelect<SelectFromBase<PMProject, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMProject.baseType, Equal<CTPRType.projectTemplate>>>>>.And<MatchUser>>, PMProject>.SearchFor<PMProject.contractCD>), typeof (PMProject.contractCD), new System.Type[] {typeof (PMProject.contractCD), typeof (PMProject.description), typeof (PMProject.status)}, DescriptionField = typeof (PMProject.description))]
  [PXDBString(30, IsUnicode = true, IsKey = true, InputMask = "")]
  [PXDefault]
  [PXUIField]
  [ProjectCDRestrictor]
  protected virtual void _(PX.Data.Events.CacheAttached<PMProject.contractCD> e)
  {
  }

  [PX.Objects.PM.Project]
  protected virtual void _(PX.Data.Events.CacheAttached<PMProject.templateID> e)
  {
  }

  [PXMergeAttributes]
  [PXDefault("R")]
  protected virtual void _(PX.Data.Events.CacheAttached<PMProject.baseType> e)
  {
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField]
  protected virtual void _(PX.Data.Events.CacheAttached<PMProject.nonProject> e)
  {
  }

  [PXDBString(1, IsFixed = true)]
  [ProjectStatus.TemplStatusList]
  [PXDefault("H")]
  [PXUIField]
  protected virtual void _(PX.Data.Events.CacheAttached<PMProject.status> e)
  {
  }

  [PXDBDate]
  protected virtual void _(PX.Data.Events.CacheAttached<PMProject.startDate> e)
  {
  }

  [PXDBLong]
  protected virtual void _(PX.Data.Events.CacheAttached<PMProject.curyInfoID> e)
  {
  }

  [PXDBLong]
  [PXMergeAttributes]
  protected virtual void PMRevenueBudget_CuryInfoID_CacheAttached(PXCache sender)
  {
    CuryField.SubscribeSimpleCopying(sender);
  }

  [PXMergeAttributes]
  [PXRestrictor(typeof (Where<PMTask.type, NotEqual<ProjectTaskType.cost>>), "Task Type is not valid", new System.Type[] {typeof (PMTask.type)})]
  protected virtual void _(
    PX.Data.Events.CacheAttached<PMRevenueBudget.projectTaskID> e)
  {
  }

  [PXDBLong]
  [PXMergeAttributes]
  protected virtual void PMCostBudget_CuryInfoID_CacheAttached(PXCache sender)
  {
    CuryField.SubscribeSimpleCopying(sender);
  }

  [PXMergeAttributes]
  [PXRestrictor(typeof (Where<PMTask.type, NotEqual<ProjectTaskType.revenue>>), "Task Type is not valid", new System.Type[] {typeof (PMTask.type)})]
  protected virtual void _(PX.Data.Events.CacheAttached<PMCostBudget.projectTaskID> e)
  {
  }

  [PXMergeAttributes]
  [PXDefault]
  protected virtual void _(PX.Data.Events.CacheAttached<PMProject.billingCuryID> e)
  {
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Project Currency", Required = true, FieldClass = "ProjectMultiCurrency")]
  protected virtual void _(PX.Data.Events.CacheAttached<PMProject.curyID> e)
  {
  }

  [PXMergeAttributes]
  [PXDefault]
  protected virtual void _(PX.Data.Events.CacheAttached<PMProject.baseCuryID> e)
  {
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Application Nbr. Format", FieldClass = "Construction")]
  protected virtual void _(
    PX.Data.Events.CacheAttached<PMProject.lastProformaNumber> e)
  {
  }

  [PXDBInt(IsKey = true)]
  [PXParent(typeof (Select<PMProject, Where<PMProject.contractID, Equal<Current<PMTask.projectID>>>>))]
  [PXDBDefault(typeof (PMProject.contractID))]
  protected virtual void _(PX.Data.Events.CacheAttached<PMTask.projectID> e)
  {
  }

  [PXDBString(1, IsFixed = true)]
  [PXDefault("A")]
  [PXUIField]
  protected virtual void _(PX.Data.Events.CacheAttached<PMTask.status> e)
  {
  }

  [Customer]
  protected virtual void _(PX.Data.Events.CacheAttached<PMTask.customerID> e)
  {
  }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Automatically Include in Project")]
  protected virtual void _(PX.Data.Events.CacheAttached<PMTask.autoIncludeInPrj> e)
  {
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Bill Separately", Visible = false)]
  protected virtual void _(PX.Data.Events.CacheAttached<PMTask.billSeparately> e)
  {
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Default Cost Code", Visible = false)]
  protected virtual void _(PX.Data.Events.CacheAttached<PMTask.defaultCostCodeID> e)
  {
  }

  [PXDBInt(IsKey = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Equipment ID")]
  [PXSelector(typeof (EPEquipment.equipmentID), DescriptionField = typeof (EPEquipment.description), SubstituteKey = typeof (EPEquipment.equipmentCD))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<EPEquipmentRate.equipmentID> e)
  {
  }

  [PXParent(typeof (Select<PMProject, Where<PMProject.contractID, Equal<Current<EPEquipmentRate.projectID>>>>))]
  [PXDBDefault(typeof (PMProject.contractID))]
  [PXDBInt(IsKey = true)]
  protected virtual void _(PX.Data.Events.CacheAttached<EPEquipmentRate.projectID> e)
  {
  }

  [PXDBInt(IsKey = true)]
  [PXDefault]
  [PXUIField]
  [PXEPEmployeeSelector]
  [PXCheckUnique(new System.Type[] {}, Where = typeof (Where<EPEmployeeContract.contractID, Equal<Current<EPEmployeeContract.contractID>>>))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<EPEmployeeContract.employeeID> e)
  {
  }

  [PXDBInt(IsKey = true)]
  [PXDBDefault(typeof (PMProject.contractID))]
  [PXParent(typeof (Select<PMProject, Where<PMProject.contractID, Equal<Current<EPEmployeeContract.contractID>>>>))]
  [PXCheckUnique(new System.Type[] {}, Where = typeof (Where<EPEmployeeContract.employeeID, Equal<Current<EPEmployeeContract.employeeID>>>))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<EPEmployeeContract.contractID> e)
  {
  }

  [PXDBString(1, IsFixed = true)]
  [BillingType.ListForProject]
  [PXUIField(DisplayName = "Billing Period")]
  protected virtual void _(
    PX.Data.Events.CacheAttached<ContractBillingSchedule.type> e)
  {
  }

  [PXMergeAttributes]
  [PXBool]
  [PXDefault(false)]
  protected virtual void _(
    PX.Data.Events.CacheAttached<PMCostCode.isProjectOverride> e)
  {
  }

  [PXDBGuid(false, IsKey = true)]
  [PXSelector(typeof (Search<NotificationSetup.setupID, Where<NotificationSetup.sourceCD, Equal<PMNotificationSource.project>>>))]
  [PXUIField(DisplayName = "Mailing ID")]
  [PXUIEnabled(typeof (Where<BqlOperand<NotificationSource.setupID, IBqlGuid>.IsNull>))]
  protected virtual void _(PX.Data.Events.CacheAttached<NotificationSource.setupID> e)
  {
  }

  [PXDBString(10, IsUnicode = true)]
  protected virtual void _(PX.Data.Events.CacheAttached<NotificationSource.classID> e)
  {
  }

  [PXMergeAttributes]
  [PXCheckUnique(new System.Type[] {typeof (NotificationSource.setupID)}, IgnoreNulls = false, Where = typeof (Where<NotificationSource.refNoteID, Equal<Current<NotificationSource.refNoteID>>>))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<NotificationSource.nBranchID> e)
  {
  }

  [PXDBString(8, InputMask = "CC.CC.CC.CC")]
  [PXUIField(DisplayName = "Report")]
  [PXDefault(typeof (Search<NotificationSetup.reportID, Where<NotificationSetup.setupID, Equal<Current<NotificationSource.setupID>>>>))]
  [PXSelector(typeof (Search<SiteMap.screenID, Where<SiteMap.url, Like<urlReports>, And<Where<SiteMap.screenID, Like<PXModule.pm_>>>>, OrderBy<Asc<SiteMap.screenID>>>), new System.Type[] {typeof (SiteMap.screenID), typeof (SiteMap.title)}, Headers = new string[] {"Report ID", "Report Name"}, DescriptionField = typeof (SiteMap.title))]
  [PXFormula(typeof (Default<NotificationSource.setupID>))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<NotificationSource.reportID> e)
  {
  }

  [PXDBInt]
  [PXDBDefault(typeof (NotificationSource.sourceID))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<NotificationRecipient.sourceID> e)
  {
  }

  [PXDBString(10)]
  [PXDefault]
  [NotificationContactType.ProjectTemplateList]
  [PXUIField(DisplayName = "Contact Type")]
  [PXCheckDistinct(new System.Type[] {typeof (NotificationRecipient.contactID)}, Where = typeof (Where<NotificationRecipient.sourceID, Equal<Current<NotificationRecipient.sourceID>>, And<NotificationRecipient.refNoteID, Equal<Current<PMProject.noteID>>>>))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<NotificationRecipient.contactType> e)
  {
  }

  [PXDBInt]
  [PXUIField(DisplayName = "Contact ID")]
  [PXNotificationContactSelector(typeof (NotificationRecipient.contactType), typeof (Search2<PX.Objects.CR.Contact.contactID, LeftJoin<PX.Objects.EP.EPEmployee, On<PX.Objects.EP.EPEmployee.parentBAccountID, Equal<PX.Objects.CR.Contact.bAccountID>, And<PX.Objects.EP.EPEmployee.defContactID, Equal<PX.Objects.CR.Contact.contactID>>>>, Where<Current<NotificationRecipient.contactType>, Equal<NotificationContactType.employee>, And<PX.Objects.EP.EPEmployee.acctCD, IsNotNull>>>), DirtyRead = true)]
  protected virtual void _(
    PX.Data.Events.CacheAttached<NotificationRecipient.contactID> e)
  {
  }

  [PXDBString(10, IsUnicode = true)]
  protected virtual void _(
    PX.Data.Events.CacheAttached<NotificationRecipient.classID> e)
  {
  }

  [PXString]
  [PXUIField(DisplayName = "Email", Enabled = false)]
  [PXFormula(typeof (Selector<NotificationRecipient.contactID, PX.Objects.CR.Contact.eMail>))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<NotificationRecipient.email> e)
  {
  }

  [PXMergeAttributes]
  [PXDBDefault(typeof (PMProject.contractID))]
  [PXDBInt(IsKey = true)]
  protected virtual void _(PX.Data.Events.CacheAttached<PMRetainageStep.projectID> e)
  {
  }

  [PXMergeAttributes]
  [PXDBDefault(typeof (PMProject.contractID))]
  [PXDBInt(IsKey = true)]
  protected virtual void _(PX.Data.Events.CacheAttached<PMProjectContact.projectID> e)
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

  [InjectDependency]
  public IUnitRateService RateService { get; set; }

  public IProjectGroupMaskHelper ProjectGroupMaskHelper
  {
    get
    {
      return (IProjectGroupMaskHelper) ((PXGraph) this).GetExtension<TemplateMaint.ProjectGroupMaskHelperExt>();
    }
  }

  [PXUIField]
  [PXButton(ImageKey = "DataEntry")]
  public IEnumerable ViewTask(PXAdapter adapter)
  {
    if (((PXSelectBase<PMTask>) this.Tasks).Current != null && ((PXSelectBase) this.Project).Cache.GetStatus((object) ((PXSelectBase<PMProject>) this.Project).Current) != 2)
    {
      TemplateTaskMaint instance = PXGraph.CreateInstance<TemplateTaskMaint>();
      ((PXSelectBase<PMTask>) instance.Task).Current = PMTask.PK.FindDirty((PXGraph) this, ((PXSelectBase<PMTask>) this.Tasks).Current.ProjectID, ((PXSelectBase<PMTask>) this.Tasks).Current.TaskID);
      ProjectAccountingService.NavigateToScreen((PXGraph) instance, "Project Task Entry - View Task");
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  protected virtual IEnumerable CopyTemplate(PXAdapter adapter)
  {
    if (((PXSelectBase<PMProject>) this.Project).Current != null)
    {
      ((PXAction) this.Save).Press();
      this.IsCopyPaste = true;
      try
      {
        this.Copy(((PXSelectBase<PMProject>) this.Project).Current);
      }
      finally
      {
        this.IsCopyPaste = false;
      }
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = false, VisibleOnProcessingResults = false)]
  public virtual IEnumerable UpdateRetainage(PXAdapter adapter)
  {
    if (((PXSelectBase<PMProject>) this.Project).Current != null)
    {
      bool flag = false;
      foreach (PXResult<PMRevenueBudget> pxResult in ((PXSelectBase<PMRevenueBudget>) this.RevenueBudget).Select(Array.Empty<object>()))
      {
        Decimal? retainagePct1 = PXResult<PMRevenueBudget>.op_Implicit(pxResult).RetainagePct;
        Decimal? retainagePct2 = ((PXSelectBase<PMProject>) this.Project).Current.RetainagePct;
        if (!(retainagePct1.GetValueOrDefault() == retainagePct2.GetValueOrDefault() & retainagePct1.HasValue == retainagePct2.HasValue))
          flag = true;
      }
      if (flag)
      {
        if (((PXSelectBase<PMProject>) this.Project).Current.RetainageMode == "C")
          this.SyncRetainage();
        else if (((PXSelectBase<PMProject>) this.Project).Ask("Default Retainage (%) Changed", "Update Retainage (%) in the revenue budget lines?", (MessageButtons) 4, (MessageIcon) 2) == 6)
          this.SyncRetainage();
      }
    }
    return adapter.Get();
  }

  public virtual void SyncRetainage()
  {
    List<PMRevenueBudget> pmRevenueBudgetList = new List<PMRevenueBudget>();
    foreach (PXResult<PMRevenueBudget> pxResult in ((PXSelectBase<PMRevenueBudget>) this.RevenueBudget).Select(Array.Empty<object>()))
    {
      PMRevenueBudget pmRevenueBudget = PXResult<PMRevenueBudget>.op_Implicit(pxResult);
      Decimal? retainagePct1 = pmRevenueBudget.RetainagePct;
      Decimal? retainagePct2 = ((PXSelectBase<PMProject>) this.Project).Current.RetainagePct;
      if (!(retainagePct1.GetValueOrDefault() == retainagePct2.GetValueOrDefault() & retainagePct1.HasValue == retainagePct2.HasValue))
        pmRevenueBudgetList.Add(pmRevenueBudget);
    }
    if (pmRevenueBudgetList.Count <= 0)
      return;
    foreach (PMRevenueBudget pmRevenueBudget in pmRevenueBudgetList)
    {
      pmRevenueBudget.RetainagePct = ((PXSelectBase<PMProject>) this.Project).Current.RetainagePct;
      ((PXSelectBase<PMRevenueBudget>) this.RevenueBudget).Update(pmRevenueBudget);
    }
  }

  public TemplateMaint()
  {
    if (((PXSelectBase<PMSetup>) this.Setup).Current == null)
      throw new PXException("Project Management Setup is not configured.");
    ((PXAction) this.CopyPaste).SetVisible(false);
  }

  protected virtual void _(PX.Data.Events.RowInserted<PMProject> e)
  {
    bool isDirty1 = ((PXSelectBase) this.Billing).Cache.IsDirty;
    ((PXSelectBase<ContractBillingSchedule>) this.Billing).Insert(new ContractBillingSchedule()
    {
      ContractID = e.Row.ContractID
    });
    ((PXSelectBase) this.Billing).Cache.IsDirty = isDirty1;
    PXSelect<NotificationSetup, Where<NotificationSetup.module, Equal<BatchModule.modulePM>>> pxSelect1 = new PXSelect<NotificationSetup, Where<NotificationSetup.module, Equal<BatchModule.modulePM>>>((PXGraph) this);
    PXSelect<NotificationSetupRecipient, Where<NotificationSetupRecipient.setupID, Equal<Required<NotificationSetupRecipient.setupID>>>> pxSelect2 = new PXSelect<NotificationSetupRecipient, Where<NotificationSetupRecipient.setupID, Equal<Required<NotificationSetupRecipient.setupID>>>>((PXGraph) this);
    bool isDirty2 = ((PXSelectBase) this.NotificationSources).Cache.IsDirty;
    bool isDirty3 = ((PXSelectBase) this.NotificationRecipients).Cache.IsDirty;
    object[] objArray = Array.Empty<object>();
    foreach (PXResult<NotificationSetup> pxResult1 in ((PXSelectBase<NotificationSetup>) pxSelect1).Select(objArray))
    {
      NotificationSetup notificationSetup = PXResult<NotificationSetup>.op_Implicit(pxResult1);
      ((PXSelectBase<NotificationSource>) this.NotificationSources).Insert(new NotificationSource()
      {
        SetupID = notificationSetup.SetupID,
        Active = notificationSetup.Active,
        EMailAccountID = notificationSetup.EMailAccountID,
        NotificationID = notificationSetup.NotificationID,
        ReportID = notificationSetup.ReportID,
        Format = notificationSetup.Format
      });
      foreach (PXResult<NotificationSetupRecipient> pxResult2 in ((PXSelectBase<NotificationSetupRecipient>) pxSelect2).Select(new object[1]
      {
        (object) notificationSetup.SetupID
      }))
      {
        NotificationSetupRecipient notificationSetupRecipient = PXResult<NotificationSetupRecipient>.op_Implicit(pxResult2);
        ((PXSelectBase<NotificationRecipient>) this.NotificationRecipients).Insert(new NotificationRecipient()
        {
          SetupID = notificationSetupRecipient.SetupID,
          Active = notificationSetupRecipient.Active,
          ContactID = notificationSetupRecipient.ContactID,
          AddTo = notificationSetupRecipient.AddTo,
          ContactType = notificationSetupRecipient.ContactType,
          Format = notificationSetup.Format
        });
      }
    }
    ((PXSelectBase) this.NotificationSources).Cache.IsDirty = isDirty2;
    ((PXSelectBase) this.NotificationRecipients).Cache.IsDirty = isDirty3;
  }

  protected virtual void PMProject_RowDeleting(PXCache sender, PXRowDeletingEventArgs e)
  {
    if (!(e.Row is PMProject row))
      return;
    if (PXResultset<PMProject>.op_Implicit(PXSelectBase<PMProject, PXSelect<PMProject, Where<PMProject.templateID, Equal<Required<PMProject.contractID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) row.ContractID
    })) != null)
    {
      ((CancelEventArgs) e).Cancel = true;
      throw new PXException("This record cannot be deleted. One or more projects are referencing this document.");
    }
  }

  protected virtual void PMProject_LocationID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is PMProject))
      return;
    sender.SetDefaultExt<PMProject.defaultSalesSubID>(e.Row);
  }

  protected void _(PX.Data.Events.RowPersisting<PMProject> e)
  {
    string contractCd = e.Row?.ContractCD;
    if (string.IsNullOrEmpty(contractCd))
      return;
    this.ValidateProjectCD(contractCd, e.Operation);
    object obj1 = ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<PMProject>>) e).Cache.GetValue<PMProject.ownerID>((object) e.Row);
    object valueOriginal1 = ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<PMProject>>) e).Cache.GetValueOriginal<PMProject.ownerID>((object) e.Row);
    if (obj1 != null && (e.Operation == 2 || !obj1.Equals(valueOriginal1)))
      ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<PMProject>>) e).Cache.VerifyFieldAndRaiseException<PMProject.ownerID>((object) e.Row, true);
    object obj2 = ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<PMProject>>) e).Cache.GetValue<PMProject.assistantID>((object) e.Row);
    object valueOriginal2 = ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<PMProject>>) e).Cache.GetValueOriginal<PMProject.assistantID>((object) e.Row);
    if (obj2 == null || e.Operation != 2 && obj2.Equals(valueOriginal2))
      return;
    ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<PMProject>>) e).Cache.VerifyFieldAndRaiseException<PMProject.assistantID>((object) e.Row, true);
  }

  protected void _(
    PX.Data.Events.FieldVerifying<PMProject, PMProject.assistantID> e)
  {
    if (this.IsCopyPaste)
      return;
    PX.Objects.EP.EPEmployee epEmployee = PX.Objects.EP.EPEmployee.PK.Find((PXGraph) this, (int?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMProject, PMProject.assistantID>, PMProject, object>) e).NewValue);
    if (epEmployee != null && epEmployee.VStatus != "A")
    {
      PXSetPropertyException<PMProject.assistantID> propertyException = new PXSetPropertyException<PMProject.assistantID>("The employee is not active.");
      ((PXSetPropertyException) propertyException).ErrorValue = (object) epEmployee.AcctCD;
      throw propertyException;
    }
  }

  public void _(
    PX.Data.Events.FieldSelecting<PMProject, PMProject.ownerID> e)
  {
    PMProject row = e.Row;
    if ((row != null ? (!row.OwnerID.HasValue ? 1 : 0) : 1) != 0 || !string.IsNullOrEmpty(PXUIFieldAttribute.GetError<PMProject.ownerID>(((PX.Data.Events.Event<PXFieldSelectingEventArgs, PX.Data.Events.FieldSelecting<PMProject, PMProject.ownerID>>) e).Cache, (object) e.Row)))
      return;
    PX.Objects.EP.EPEmployee epEmployee = ((PXSelectBase<PX.Objects.EP.EPEmployee>) new FbqlSelect<SelectFromBase<PX.Objects.EP.EPEmployee, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<PX.Objects.EP.EPEmployee.defContactID, IBqlInt>.IsEqual<P.AsInt>>, PX.Objects.EP.EPEmployee>.View((PXGraph) this)).SelectSingle(new object[1]
    {
      (object) e.Row.OwnerID
    });
    if (epEmployee == null || !(epEmployee.VStatus != "A"))
      return;
    PXUIFieldAttribute.SetWarning<PMProject.ownerID>(((PX.Data.Events.Event<PXFieldSelectingEventArgs, PX.Data.Events.FieldSelecting<PMProject, PMProject.ownerID>>) e).Cache, (object) e.Row, "The employee is not active.");
  }

  public void _(
    PX.Data.Events.FieldSelecting<PMProject, PMProject.assistantID> e)
  {
    PMProject row = e.Row;
    if ((row != null ? (!row.AssistantID.HasValue ? 1 : 0) : 1) != 0 || !string.IsNullOrEmpty(PXUIFieldAttribute.GetError<PMProject.assistantID>(((PX.Data.Events.Event<PXFieldSelectingEventArgs, PX.Data.Events.FieldSelecting<PMProject, PMProject.assistantID>>) e).Cache, (object) e.Row)))
      return;
    PX.Objects.EP.EPEmployee epEmployee = ((PXSelectBase<PX.Objects.EP.EPEmployee>) new FbqlSelect<SelectFromBase<PX.Objects.EP.EPEmployee, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<PX.Objects.EP.EPEmployee.bAccountID, IBqlInt>.IsEqual<P.AsInt>>, PX.Objects.EP.EPEmployee>.View((PXGraph) this)).SelectSingle(new object[1]
    {
      (object) e.Row.AssistantID
    });
    if (epEmployee == null || !(epEmployee.VStatus != "A"))
      return;
    PXUIFieldAttribute.SetWarning<PMProject.assistantID>(((PX.Data.Events.Event<PXFieldSelectingEventArgs, PX.Data.Events.FieldSelecting<PMProject, PMProject.assistantID>>) e).Cache, (object) e.Row, "The employee is not active.");
  }

  public void _(
    PX.Data.Events.FieldSelecting<PMProjectContact, PMProjectContact.contactID> e)
  {
    PMProjectContact row = e.Row;
    if ((row != null ? (!row.ContactID.HasValue ? 1 : 0) : 1) != 0 || !string.IsNullOrEmpty(PXUIFieldAttribute.GetError<PMProjectContact.contactID>(((PX.Data.Events.Event<PXFieldSelectingEventArgs, PX.Data.Events.FieldSelecting<PMProjectContact, PMProjectContact.contactID>>) e).Cache, (object) e.Row)))
      return;
    PX.Objects.CR.Contact contact = PX.Objects.CR.Contact.PK.Find((PXGraph) this, e.Row.ContactID);
    if (contact == null || contact.IsActive.GetValueOrDefault())
      return;
    PXUIFieldAttribute.SetWarning<PMProjectContact.contactID>(((PX.Data.Events.Event<PXFieldSelectingEventArgs, PX.Data.Events.FieldSelecting<PMProjectContact, PMProjectContact.contactID>>) e).Cache, (object) e.Row, contact.ContactType == "EP" ? "The employee is not active." : "The contact is not active.");
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMProject, PMProject.defaultBranchID> e)
  {
    if (e.Row == null || PXAccess.FeatureInstalled<FeaturesSet.projectMultiCurrency>())
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMProject, PMProject.defaultBranchID>>) e).Cache.SetDefaultExt<PMProject.curyID>((object) e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<PMProject, PMProject.curyID> e)
  {
    if (e.Row == null)
      return;
    if (!PXAccess.FeatureInstalled<FeaturesSet.projectMultiCurrency>() && e.Row.DefaultBranchID.HasValue)
    {
      PX.Objects.GL.Branch branch = PXResultset<PX.Objects.GL.Branch>.op_Implicit(PXSelectBase<PX.Objects.GL.Branch, PXViewOf<PX.Objects.GL.Branch>.BasedOn<SelectFromBase<PX.Objects.GL.Branch, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<PX.Objects.GL.Branch.branchID, IBqlInt>.IsEqual<BqlField<PMProject.defaultBranchID, IBqlInt>.FromCurrent>>>.ReadOnly.Config>.Select((PXGraph) this, Array.Empty<object>()));
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMProject, PMProject.curyID>, PMProject, object>) e).NewValue = (object) branch.BaseCuryID;
    }
    else
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMProject, PMProject.curyID>, PMProject, object>) e).NewValue = (object) ((PXGraph) this).Accessinfo.BaseCuryID;
  }

  protected virtual void EPEmployeeContract_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    ((PXSelectBase) this.ContractRates).View.Cache.AllowInsert = e.Row != null;
  }

  protected virtual void EPEmployeeContract_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    EPEmployeeContract oldRow = (EPEmployeeContract) e.OldRow;
    EPEmployeeContract row = (EPEmployeeContract) e.Row;
    if (oldRow == null)
      return;
    EPContractRate.UpdateKeyFields((PXGraph) this, oldRow.ContractID, oldRow.EmployeeID, row.ContractID, row.EmployeeID);
  }

  protected virtual void _(PX.Data.Events.RowSelected<PMProject> e)
  {
    if (e.Row == null)
      return;
    PXUIFieldAttribute.SetVisible<PMProject.curyID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMProject>>) e).Cache, (object) e.Row, PXAccess.FeatureInstalled<FeaturesSet.multicurrency>());
    PXUIFieldAttribute.SetEnabled<PMProject.visibleInGL>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMProject>>) e).Cache, (object) e.Row, ((PXSelectBase<PMSetup>) this.Setup).Current.VisibleInGL.GetValueOrDefault());
    PXUIFieldAttribute.SetEnabled<PMProject.visibleInAP>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMProject>>) e).Cache, (object) e.Row, ((PXSelectBase<PMSetup>) this.Setup).Current.VisibleInAP.GetValueOrDefault());
    PXUIFieldAttribute.SetEnabled<PMProject.visibleInAR>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMProject>>) e).Cache, (object) e.Row, ((PXSelectBase<PMSetup>) this.Setup).Current.VisibleInAR.GetValueOrDefault());
    PXUIFieldAttribute.SetEnabled<PMProject.visibleInSO>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMProject>>) e).Cache, (object) e.Row, ((PXSelectBase<PMSetup>) this.Setup).Current.VisibleInSO.GetValueOrDefault());
    PXUIFieldAttribute.SetEnabled<PMProject.visibleInPO>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMProject>>) e).Cache, (object) e.Row, ((PXSelectBase<PMSetup>) this.Setup).Current.VisibleInPO.GetValueOrDefault());
    PXUIFieldAttribute.SetEnabled<PMProject.visibleInTA>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMProject>>) e).Cache, (object) e.Row, ((PXSelectBase<PMSetup>) this.Setup).Current.VisibleInTA.GetValueOrDefault());
    PXUIFieldAttribute.SetEnabled<PMProject.visibleInEA>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMProject>>) e).Cache, (object) e.Row, ((PXSelectBase<PMSetup>) this.Setup).Current.VisibleInEA.GetValueOrDefault());
    PXUIFieldAttribute.SetEnabled<PMProject.visibleInIN>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMProject>>) e).Cache, (object) e.Row, ((PXSelectBase<PMSetup>) this.Setup).Current.VisibleInIN.GetValueOrDefault());
    PXUIFieldAttribute.SetEnabled<PMProject.visibleInCA>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMProject>>) e).Cache, (object) e.Row, ((PXSelectBase<PMSetup>) this.Setup).Current.VisibleInCA.GetValueOrDefault());
    PXUIFieldAttribute.SetEnabled<PMProject.visibleInCR>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMProject>>) e).Cache, (object) e.Row, ((PXSelectBase<PMSetup>) this.Setup).Current.VisibleInCR.GetValueOrDefault());
    PXUIFieldAttribute.SetEnabled<PMProject.templateID>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMProject>>) e).Cache, (object) e.Row, !e.Row.TemplateID.HasValue && ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMProject>>) e).Cache.GetStatus((object) e.Row) == 2);
    PXUIFieldAttribute.SetVisible<PMCostBudget.inventoryID>(((PXSelectBase) this.CostBudget).Cache, (object) null, e.Row.CostBudgetLevel == "I" || e.Row.CostBudgetLevel == "D");
    PXUIFieldAttribute.SetVisible<PMCostBudget.costCodeID>(((PXSelectBase) this.CostBudget).Cache, (object) null, e.Row.CostBudgetLevel == "C" || e.Row.CostBudgetLevel == "D");
    PXUIFieldAttribute.SetVisible<PMCostBudget.revenueInventoryID>(((PXSelectBase) this.CostBudget).Cache, (object) null, e.Row.BudgetLevel == "I" || e.Row.BudgetLevel == "D");
    PXUIFieldAttribute.SetVisibility<PMCostBudget.revenueInventoryID>(((PXSelectBase) this.CostBudget).Cache, (object) null, e.Row.BudgetLevel == "I" || e.Row.BudgetLevel == "D" ? (PXUIVisibility) 3 : (PXUIVisibility) 1);
    PXUIFieldAttribute.SetVisible<PMRevenueBudget.inventoryID>(((PXSelectBase) this.RevenueBudget).Cache, (object) null, e.Row.BudgetLevel == "I" || e.Row.BudgetLevel == "D");
    PXUIFieldAttribute.SetVisible<PMRevenueBudget.costCodeID>(((PXSelectBase) this.RevenueBudget).Cache, (object) null, e.Row.BudgetLevel == "C" || e.Row.BudgetLevel == "D");
    PXUIFieldAttribute.SetRequired<PMRevenueBudget.inventoryID>(((PXSelectBase) this.RevenueBudget).Cache, e.Row.BudgetLevel == "I" || e.Row.BudgetLevel == "D");
    PXUIFieldAttribute.SetVisible<PMRevenueBudget.curyPrepaymentAmount>(((PXSelectBase) this.RevenueBudget).Cache, (object) null, this.PrepaymentVisible());
    PXUIFieldAttribute.SetVisible<PMRevenueBudget.prepaymentPct>(((PXSelectBase) this.RevenueBudget).Cache, (object) null, this.PrepaymentVisible());
    PXUIFieldAttribute.SetVisible<PMRevenueBudget.limitQty>(((PXSelectBase) this.RevenueBudget).Cache, (object) null, false);
    PXUIFieldAttribute.SetVisible<PMRevenueBudget.maxQty>(((PXSelectBase) this.RevenueBudget).Cache, (object) null, false);
    PXUIFieldAttribute.SetVisible<PMRevenueBudget.limitAmount>(((PXSelectBase) this.RevenueBudget).Cache, (object) null, this.LimitsVisible());
    PXUIFieldAttribute.SetVisible<PMRevenueBudget.curyMaxAmount>(((PXSelectBase) this.RevenueBudget).Cache, (object) null, this.LimitsVisible());
    PXUIFieldAttribute.SetVisibility<PMRevenueBudget.curyPrepaymentAmount>(((PXSelectBase) this.RevenueBudget).Cache, (object) null, this.PrepaymentVisible() ? (PXUIVisibility) 3 : (PXUIVisibility) 1);
    PXUIFieldAttribute.SetVisibility<PMRevenueBudget.prepaymentPct>(((PXSelectBase) this.RevenueBudget).Cache, (object) null, this.PrepaymentVisible() ? (PXUIVisibility) 3 : (PXUIVisibility) 1);
    PXUIFieldAttribute.SetVisibility<PMRevenueBudget.limitQty>(((PXSelectBase) this.RevenueBudget).Cache, (object) null, (PXUIVisibility) 1);
    PXUIFieldAttribute.SetVisibility<PMRevenueBudget.maxQty>(((PXSelectBase) this.RevenueBudget).Cache, (object) null, (PXUIVisibility) 1);
    PXUIFieldAttribute.SetVisibility<PMRevenueBudget.limitAmount>(((PXSelectBase) this.RevenueBudget).Cache, (object) null, this.LimitsVisible() ? (PXUIVisibility) 3 : (PXUIVisibility) 1);
    PXUIFieldAttribute.SetVisibility<PMRevenueBudget.curyMaxAmount>(((PXSelectBase) this.RevenueBudget).Cache, (object) null, this.LimitsVisible() ? (PXUIVisibility) 3 : (PXUIVisibility) 1);
    PXUIFieldAttribute.SetVisibility<PMTask.defaultCostCodeID>(((PXSelectBase) this.Tasks).Cache, (object) null, CostCodeAttribute.UseCostCode() ? (PXUIVisibility) 3 : (PXUIVisibility) 1);
    bool flag = PXAccess.FeatureInstalled<FeaturesSet.paymentsByLines>();
    PXUIFieldAttribute.SetVisible<PX.Objects.CT.Contract.retainageMode>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMProject>>) e).Cache, (object) e.Row, flag);
    PXUIFieldAttribute.SetVisible<PMRevenueBudget.retainagePct>(((PXSelectBase) this.RevenueBudget).Cache, (object) null, e.Row.RetainageMode != "C");
    PXUIFieldAttribute.SetVisibility<PMRevenueBudget.retainagePct>(((PXSelectBase) this.RevenueBudget).Cache, (object) null, e.Row.RetainageMode != "C" ? (PXUIVisibility) 3 : (PXUIVisibility) 1);
    PXUIFieldAttribute.SetVisible<PX.Objects.CT.Contract.retainageMaxPct>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMProject>>) e).Cache, (object) e.Row, flag && e.Row.RetainageMode == "C");
    PXUIFieldAttribute.SetVisible<PMBudget.retainageMaxPct>(((PXSelectBase) this.RevenueBudget).Cache, (object) null, e.Row.RetainageMode == "L");
    PXUIFieldAttribute.SetVisible<PMProject.retainagePct>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMProject>>) e).Cache, (object) e.Row, !e.Row.SteppedRetainage.GetValueOrDefault());
    PXUIFieldAttribute.SetEnabled<PMProject.steppedRetainageOption>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMProject>>) e).Cache, (object) e.Row, e.Row.RetainageMode != "L");
    bool valueOrDefault = e.Row.SteppedRetainage.GetValueOrDefault();
    ((PXSelectBase) this.RetainageSteps).AllowSelect = valueOrDefault;
    ((PXSelectBase) this.RetainageSteps).AllowInsert = valueOrDefault;
    ((PXSelectBase) this.RetainageSteps).AllowUpdate = valueOrDefault;
    ((PXSelectBase) this.RetainageSteps).AllowDelete = valueOrDefault;
    PXUIFieldAttribute.SetEnabled<PMProject.accountingMode>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMProject>>) e).Cache, (object) e.Row, PXAccess.FeatureInstalled<FeaturesSet.materialManagement>());
    PXDefaultAttribute.SetPersistingCheck<PMProject.dropshipExpenseSubMask>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMProject>>) e).Cache, (object) e.Row, !PXAccess.FeatureInstalled<FeaturesSet.distributionModule>() || !PXAccess.FeatureInstalled<FeaturesSet.subAccount>() ? (PXPersistingCheck) 2 : (PXPersistingCheck) 1);
    if (PXAccess.FeatureInstalled<FeaturesSet.inventory>())
      return;
    PXStringListAttribute.SetList<PMProject.dropshipExpenseAccountSource>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMProject>>) e).Cache, (object) e.Row, new string[3]
    {
      "O",
      "P",
      "T"
    }, new string[3]{ "Item", "Project", "Task" });
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<PMProject, PMProject.accountingMode> e)
  {
    if (e.Row == null || PXAccess.FeatureInstalled<FeaturesSet.materialManagement>())
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMProject, PMProject.accountingMode>, PMProject, object>) e).NewValue = (object) "L";
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<PMProject, PMProject.retainagePct> e)
  {
    if (e.Row == null || ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMProject, PMProject.retainagePct>, PMProject, object>) e).NewValue == null)
      return;
    Decimal newValue = (Decimal) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMProject, PMProject.retainagePct>, PMProject, object>) e).NewValue;
    if (newValue < 0M || newValue > 100M)
      throw new PXSetPropertyException<PMProject.retainagePct>("Percentage value should be between 0 and 100");
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<PMProject, PMProject.costBudgetLevel> e)
  {
    if (CostCodeAttribute.UseCostCode())
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMProject, PMProject.costBudgetLevel>, PMProject, object>) e).NewValue = (object) "C";
    else
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMProject, PMProject.costBudgetLevel>, PMProject, object>) e).NewValue = (object) "I";
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMProject, PX.Objects.CT.Contract.steppedRetainage> e)
  {
    if (e.Row == null || !e.Row.SteppedRetainage.GetValueOrDefault() || ((PXSelectBase<PMRetainageStep>) this.RetainageSteps).Select(Array.Empty<object>()).Count != 0)
      return;
    PMRetainageStep pmRetainageStep = ((PXSelectBase<PMRetainageStep>) this.RetainageSteps).Insert();
    pmRetainageStep.ThresholdPct = new Decimal?(0M);
    pmRetainageStep.RetainagePct = e.Row.RetainagePct;
    ((PXSelectBase<PMRetainageStep>) this.RetainageSteps).Update(pmRetainageStep);
    this.SyncRetainage();
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<PMProject, PX.Objects.CT.Contract.createProforma> e)
  {
    if (!((bool?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMProject, PX.Objects.CT.Contract.createProforma>, PMProject, object>) e).NewValue).GetValueOrDefault() && e.Row != null && e.Row.RetainageMode == "C")
      throw new PXSetPropertyException<PX.Objects.CT.Contract.createProforma>("To enable the creation of pro forma invoices, the retainage mode must be changed first.");
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<PMProject, PX.Objects.CT.Contract.retainageMode> e)
  {
    if ((string) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMProject, PX.Objects.CT.Contract.retainageMode>, PMProject, object>) e).NewValue == "C" && !e.Row.CreateProforma.GetValueOrDefault())
      throw new PXSetPropertyException<PX.Objects.CT.Contract.retainageMode>("To select the Contract Cap mode, the creation of pro forma invoices must be enabled first.");
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMProject, PX.Objects.CT.Contract.retainageMode> e)
  {
    if (e.Row != null && e.Row.RetainageMode != "C")
      e.Row.SteppedRetainage = new bool?(false);
    if (!(e.Row.RetainageMode == "C"))
      return;
    this.SyncRetainage();
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMProject, PMProject.retainagePct> e)
  {
    if (!PXAccess.FeatureInstalled<FeaturesSet.retainage>())
      return;
    ((PXAction) this.updateRetainage).Press();
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMProject, PMProject.ownerID> e)
  {
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMProject, PMProject.ownerID>>) e).Cache.SetDefaultExt<PMProject.approverID>((object) e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMProject, PMProject.projectGroupID> e)
  {
    this.ProjectGroupMaskHelper.UpdateProjectMaskFromProjectGroup(e.Row, (string) e.NewValue, ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMProject, PMProject.projectGroupID>>) e).Cache);
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<PMProject, PMProject.approverID> e)
  {
    PX.Objects.CR.Standalone.EPEmployee epEmployee = PXResultset<PX.Objects.CR.Standalone.EPEmployee>.op_Implicit(PXSelectBase<PX.Objects.CR.Standalone.EPEmployee, PXSelect<PX.Objects.CR.Standalone.EPEmployee, Where<PX.Objects.CR.Standalone.EPEmployee.defContactID, Equal<Current<PMProject.ownerID>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()));
    if (epEmployee == null)
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMProject, PMProject.approverID>, PMProject, object>) e).NewValue = (object) epEmployee.BAccountID;
  }

  protected virtual void _(PX.Data.Events.RowSelected<PMRevenueBudget> e)
  {
    if (e.Row == null)
      return;
    PXUIFieldAttribute.SetEnabled<PMRevenueBudget.limitQty>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMRevenueBudget>>) e).Cache, (object) e.Row, !string.IsNullOrEmpty(e.Row.UOM));
    PXUIFieldAttribute.SetEnabled<PMRevenueBudget.maxQty>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMRevenueBudget>>) e).Cache, (object) e.Row, e.Row.LimitQty.GetValueOrDefault() && !string.IsNullOrEmpty(e.Row.UOM));
    PXUIFieldAttribute.SetEnabled<PMRevenueBudget.curyMaxAmount>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMRevenueBudget>>) e).Cache, (object) e.Row, e.Row.LimitAmount.GetValueOrDefault());
    Decimal? qty = e.Row.Qty;
    Decimal num1 = 0M;
    if (qty.GetValueOrDefault() == num1 & qty.HasValue)
    {
      Decimal? revisedQty = e.Row.RevisedQty;
      Decimal num2 = 0M;
      if (revisedQty.GetValueOrDefault() == num2 & revisedQty.HasValue)
        goto label_6;
    }
    if (string.IsNullOrEmpty(e.Row.UOM))
    {
      if (!string.IsNullOrEmpty(PXUIFieldAttribute.GetError<PMRevenueBudget.uOM>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMRevenueBudget>>) e).Cache, (object) e.Row)))
        return;
      PXUIFieldAttribute.SetWarning<PMRevenueBudget.uOM>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMRevenueBudget>>) e).Cache, (object) e.Row, "The value of the Actual Qty. will not be updated if no UOM is defined.");
      return;
    }
label_6:
    if (!(PXUIFieldAttribute.GetError<PMRevenueBudget.uOM>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMRevenueBudget>>) e).Cache, (object) e.Row) == PXLocalizer.Localize("The value of the Actual Qty. will not be updated if no UOM is defined.")))
      return;
    PXUIFieldAttribute.SetWarning<PMRevenueBudget.uOM>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMRevenueBudget>>) e).Cache, (object) e.Row, (string) null);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMRevenueBudget, PMRevenueBudget.costCodeID> e)
  {
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMRevenueBudget, PMRevenueBudget.costCodeID>>) e).Cache.SetDefaultExt<PMRevenueBudget.description>((object) e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMRevenueBudget, PMRevenueBudget.inventoryID> e)
  {
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMRevenueBudget, PMRevenueBudget.inventoryID>>) e).Cache.SetDefaultExt<PMRevenueBudget.description>((object) e.Row);
    if (!e.Row.AccountGroupID.HasValue)
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMRevenueBudget, PMRevenueBudget.inventoryID>>) e).Cache.SetDefaultExt<PMRevenueBudget.accountGroupID>((object) e.Row);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMRevenueBudget, PMRevenueBudget.inventoryID>>) e).Cache.SetDefaultExt<PMRevenueBudget.uOM>((object) e.Row);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMRevenueBudget, PMRevenueBudget.inventoryID>>) e).Cache.SetDefaultExt<PMRevenueBudget.curyUnitRate>((object) e.Row);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMRevenueBudget, PMRevenueBudget.inventoryID>>) e).Cache.SetDefaultExt<PMRevenueBudget.taxCategoryID>((object) e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMRevenueBudget, PMRevenueBudget.uOM> e)
  {
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMRevenueBudget, PMRevenueBudget.uOM>>) e).Cache.SetDefaultExt<PMRevenueBudget.curyUnitRate>((object) e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<PMRevenueBudget, PMRevenueBudget.description> e)
  {
    if (e.Row == null || ((PXSelectBase<PMProject>) this.Project).Current == null)
      return;
    if (((PXSelectBase<PMProject>) this.Project).Current.BudgetLevel == "C" || ((PXSelectBase<PMProject>) this.Project).Current.BudgetLevel == "D")
    {
      if (!e.Row.CostCodeID.HasValue || !(PXSelectorAttribute.Select<PMRevenueBudget.costCodeID>(((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<PMRevenueBudget, PMRevenueBudget.description>>) e).Cache, (object) e.Row) is PMCostCode pmCostCode))
        return;
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMRevenueBudget, PMRevenueBudget.description>, PMRevenueBudget, object>) e).NewValue = (object) pmCostCode.Description;
    }
    else if (((PXSelectBase<PMProject>) this.Project).Current.BudgetLevel == "T")
    {
      if (!e.Row.ProjectTaskID.HasValue || !(PXSelectorAttribute.Select<PMRevenueBudget.projectTaskID>(((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<PMRevenueBudget, PMRevenueBudget.description>>) e).Cache, (object) e.Row) is PMTask pmTask))
        return;
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMRevenueBudget, PMRevenueBudget.description>, PMRevenueBudget, object>) e).NewValue = (object) pmTask.Description;
    }
    else
    {
      if (!(((PXSelectBase<PMProject>) this.Project).Current.BudgetLevel == "I") || !e.Row.InventoryID.HasValue)
        return;
      int? inventoryId = e.Row.InventoryID;
      int emptyInventoryId = PMInventorySelectorAttribute.EmptyInventoryID;
      if (inventoryId.GetValueOrDefault() == emptyInventoryId & inventoryId.HasValue || !(PXSelectorAttribute.Select<PMRevenueBudget.inventoryID>(((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<PMRevenueBudget, PMRevenueBudget.description>>) e).Cache, (object) e.Row) is PX.Objects.IN.InventoryItem inventoryItem))
        return;
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMRevenueBudget, PMRevenueBudget.description>, PMRevenueBudget, object>) e).NewValue = (object) inventoryItem.Descr;
    }
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMRevenueBudget, PMRevenueBudget.projectTaskID> e)
  {
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMRevenueBudget, PMRevenueBudget.projectTaskID>>) e).Cache.SetDefaultExt<PMRevenueBudget.description>((object) e.Row);
    if (e.Row == null || e.NewValue == null || ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMRevenueBudget, PMRevenueBudget.projectTaskID>>) e).Cache.Graph.IsImportFromExcel && e.Row.ProgressBillingBase != null)
      return;
    PMTask dirty = PMTask.PK.FindDirty((PXGraph) this, e.Row.ProjectID, e.NewValue as int?);
    if (dirty == null)
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMRevenueBudget, PMRevenueBudget.projectTaskID>>) e).Cache.SetValueExt<PMRevenueBudget.progressBillingBase>((object) e.Row, (object) dirty.ProgressBillingBase);
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<PMRevenueBudget, PMRevenueBudget.inventoryID> e)
  {
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMRevenueBudget, PMRevenueBudget.inventoryID>, PMRevenueBudget, object>) e).NewValue = (object) PMInventorySelectorAttribute.EmptyInventoryID;
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<PMRevenueBudget, PMRevenueBudget.costCodeID> e)
  {
    if (((PXSelectBase<PMProject>) this.Project).Current == null || !(((PXSelectBase<PMProject>) this.Project).Current.BudgetLevel != "C"))
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMRevenueBudget, PMRevenueBudget.costCodeID>, PMRevenueBudget, object>) e).NewValue = (object) CostCodeAttribute.GetDefaultCostCode();
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<PMRevenueBudget, PMRevenueBudget.curyUnitRate> e)
  {
    if (((PXSelectBase<PMProject>) this.Project).Current == null)
      return;
    Decimal? unitPrice = this.RateService.CalculateUnitPrice(((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<PMRevenueBudget, PMRevenueBudget.curyUnitRate>>) e).Cache, e.Row.ProjectID, e.Row.ProjectTaskID, e.Row.InventoryID, e.Row.UOM, e.Row.Qty, ((PXSelectBase<PMProject>) this.Project).Current.StartDate, ((PXSelectBase<PMProject>) this.Project).Current.CuryInfoID);
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMRevenueBudget, PMRevenueBudget.curyUnitRate>, PMRevenueBudget, object>) e).NewValue = (object) unitPrice.GetValueOrDefault();
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<PMProject, PMProject.dropshipExpenseSubMask> e)
  {
    if (((PXSelectBase<PMSetup>) this.Setup).Current == null)
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMProject, PMProject.dropshipExpenseSubMask>, PMProject, object>) e).NewValue = (object) ((PXSelectBase<PMSetup>) this.Setup).Current.DropshipExpenseSubMask;
    if (((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMProject, PMProject.dropshipExpenseSubMask>, PMProject, object>) e).NewValue == null)
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMProject, PMProject.dropshipExpenseSubMask>>) e).Cancel = true;
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMProject, PMProject.dropshipReceiptProcessing> e)
  {
    if (e.Row == null || !((string) e.NewValue == "S"))
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMProject, PMProject.dropshipReceiptProcessing>>) e).Cache.SetValueExt<PMProject.dropshipExpenseRecording>((object) e.Row, (object) "B");
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<PMProject, PMProject.dropshipExpenseRecording> e)
  {
    if (e.Row == null || !((string) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMProject, PMProject.dropshipExpenseRecording>, PMProject, object>) e).NewValue == "R") || !PXAccess.FeatureInstalled<FeaturesSet.inventory>())
      return;
    INSetup inSetup = PXResultset<INSetup>.op_Implicit(PXSelectBase<INSetup, PXSelect<INSetup>.Config>.Select((PXGraph) this, Array.Empty<object>()));
    if ((inSetup != null ? (!inSetup.UpdateGL.GetValueOrDefault() ? 1 : 0) : 1) != 0)
      throw new PXSetPropertyException<PMProject.dropshipExpenseRecording>("The On Receipt Release option cannot be selected because the Update GL check box is cleared on the Inventory Preferences (IN101000) form.");
  }

  protected virtual void _(PX.Data.Events.RowPersisting<PMRevenueBudget> e)
  {
    if (e.Operation != 3 && ((PXSelectBase<PMProject>) this.Project).Current != null && ((PXSelectBase<PMProject>) this.Project).Current.BudgetLevel == "I" && string.IsNullOrEmpty(e.Row.Description))
    {
      ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<PMRevenueBudget>>) e).Cache.RaiseExceptionHandling<PMRevenueBudget.description>((object) e.Row, (object) null, (Exception) new PXSetPropertyException<PMRevenueBudget.description>("'{0}' cannot be empty.", new object[1]
      {
        (object) "[Description]"
      }));
      throw new PXRowPersistingException("Description", (object) null, "'{0}' cannot be empty.", new object[1]
      {
        (object) "Description"
      });
    }
    e.Row.CuryInfoID = new long?();
  }

  protected virtual void _(
    PX.Data.Events.FieldSelecting<PMRevenueBudget, PMRevenueBudget.prepaymentPct> e)
  {
    if (e.Row == null)
      return;
    Decimal valueOrDefault = e.Row.CuryAmount.GetValueOrDefault();
    Decimal d = 0M;
    if (valueOrDefault != 0M)
      d = e.Row.CuryPrepaymentAmount.GetValueOrDefault() * 100M / valueOrDefault;
    PXFieldState instance = PXDecimalState.CreateInstance((object) Math.Round(d, 2), new int?(2), "prepaymentPct", new bool?(false), new int?(0), new Decimal?(Decimal.MinValue), new Decimal?(Decimal.MaxValue));
    ((PX.Data.Events.FieldSelectingBase<PX.Data.Events.FieldSelecting<PMRevenueBudget, PMRevenueBudget.prepaymentPct>>) e).ReturnState = (object) instance;
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMRevenueBudget, PMRevenueBudget.prepaymentPct> e)
  {
    if (e.Row == null)
      return;
    Decimal num = Math.Max(0M, e.Row.CuryAmount.GetValueOrDefault() * e.Row.PrepaymentPct.GetValueOrDefault() / 100M);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMRevenueBudget, PMRevenueBudget.prepaymentPct>>) e).Cache.SetValueExt<PMRevenueBudget.curyPrepaymentAmount>((object) e.Row, (object) num);
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<PMRevenueBudget, PMRevenueBudget.curyPrepaymentAmount> e)
  {
    PMProject current = ((PXSelectBase<PMProject>) this.Project).Current;
    if ((current != null ? (current.PrepaymentEnabled.GetValueOrDefault() ? 1 : 0) : 0) == 0)
      return;
    Decimal valueOrDefault = e.Row.CuryAmount.GetValueOrDefault();
    Decimal? newValue = (Decimal?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMRevenueBudget, PMRevenueBudget.curyPrepaymentAmount>, PMRevenueBudget, object>) e).NewValue;
    Decimal num = valueOrDefault;
    if (!(newValue.GetValueOrDefault() > num & newValue.HasValue))
      return;
    ((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<PMRevenueBudget, PMRevenueBudget.curyPrepaymentAmount>>) e).Cache.RaiseExceptionHandling<PMRevenueBudget.curyPrepaymentAmount>((object) e.Row, ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMRevenueBudget, PMRevenueBudget.curyPrepaymentAmount>, PMRevenueBudget, object>) e).NewValue, (Exception) new PXSetPropertyException<PMRevenueBudget.curyPrepaymentAmount>("The Prepaid Amount exceeds the uninvoiced balance.", (PXErrorLevel) 2));
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMRevenueBudget, PMRevenueBudget.curyPrepaymentAmount> e)
  {
    if (e.Row == null)
      return;
    e.Row.CuryPrepaymentAvailable = new Decimal?(e.Row.CuryPrepaymentAmount.GetValueOrDefault() - e.Row.CuryPrepaymentInvoiced.GetValueOrDefault());
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<PMRevenueBudget, PMRevenueBudget.retainagePct> e)
  {
    if (e.Row == null || ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMRevenueBudget, PMRevenueBudget.retainagePct>, PMRevenueBudget, object>) e).NewValue == null)
      return;
    Decimal newValue = (Decimal) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<PMRevenueBudget, PMRevenueBudget.retainagePct>, PMRevenueBudget, object>) e).NewValue;
    if (newValue < 0M || newValue > 100M)
      throw new PXSetPropertyException<PMRevenueBudget.retainagePct>("Percentage value should be between 0 and 100");
  }

  protected virtual void _(PX.Data.Events.RowSelected<PMCostBudget> e)
  {
    if (e.Row == null)
      return;
    Decimal? nullable = e.Row.Qty;
    Decimal num1 = 0M;
    if (nullable.GetValueOrDefault() == num1 & nullable.HasValue)
    {
      nullable = e.Row.RevisedQty;
      Decimal num2 = 0M;
      if (nullable.GetValueOrDefault() == num2 & nullable.HasValue)
        goto label_6;
    }
    if (string.IsNullOrEmpty(e.Row.UOM))
    {
      if (!string.IsNullOrEmpty(PXUIFieldAttribute.GetError<PMCostBudget.uOM>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMCostBudget>>) e).Cache, (object) e.Row)))
        return;
      PXUIFieldAttribute.SetWarning<PMCostBudget.uOM>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMCostBudget>>) e).Cache, (object) e.Row, "The value of the Actual Qty. will not be updated if no UOM is defined.");
      return;
    }
label_6:
    if (!(PXUIFieldAttribute.GetError<PMCostBudget.uOM>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMCostBudget>>) e).Cache, (object) e.Row) == PXLocalizer.Localize("The value of the Actual Qty. will not be updated if no UOM is defined.")))
      return;
    PXUIFieldAttribute.SetWarning<PMCostBudget.uOM>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMCostBudget>>) e).Cache, (object) e.Row, (string) null);
  }

  protected virtual void _(PX.Data.Events.RowPersisting<PMCostBudget> e)
  {
    if (e.Row == null)
      return;
    e.Row.CuryInfoID = new long?();
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<PMCostBudget, PMCostBudget.curyUnitRate> e)
  {
    if (((PXSelectBase<PMProject>) this.Project).Current == null)
      return;
    Decimal? unitCost = this.RateService.CalculateUnitCost(((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<PMCostBudget, PMCostBudget.curyUnitRate>>) e).Cache, e.Row.ProjectID, e.Row.ProjectTaskID, e.Row.InventoryID, e.Row.UOM, new int?(), ((PXSelectBase<PMProject>) this.Project).Current.StartDate, ((PXSelectBase<PMProject>) this.Project).Current.CuryInfoID);
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMCostBudget, PMCostBudget.curyUnitRate>, PMCostBudget, object>) e).NewValue = (object) unitCost.GetValueOrDefault();
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<PMCostBudget, PMCostBudget.inventoryID> e)
  {
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMCostBudget, PMCostBudget.inventoryID>, PMCostBudget, object>) e).NewValue = (object) PMInventorySelectorAttribute.EmptyInventoryID;
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<PMCostBudget, PMCostBudget.costCodeID> e)
  {
    if (((PXSelectBase<PMProject>) this.Project).Current == null || !(((PXSelectBase<PMProject>) this.Project).Current.BudgetLevel != "C"))
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMCostBudget, PMCostBudget.costCodeID>, PMCostBudget, object>) e).NewValue = (object) CostCodeAttribute.GetDefaultCostCode();
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMCostBudget, PMCostBudget.projectTaskID> e)
  {
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMCostBudget, PMCostBudget.projectTaskID>>) e).Cache.SetDefaultExt<PMCostBudget.description>((object) e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMCostBudget, PMCostBudget.inventoryID> e)
  {
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMCostBudget, PMCostBudget.inventoryID>>) e).Cache.SetDefaultExt<PMCostBudget.description>((object) e.Row);
    if (!e.Row.AccountGroupID.HasValue)
      ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMCostBudget, PMCostBudget.inventoryID>>) e).Cache.SetDefaultExt<PMCostBudget.accountGroupID>((object) e.Row);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMCostBudget, PMCostBudget.inventoryID>>) e).Cache.SetDefaultExt<PMCostBudget.uOM>((object) e.Row);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMCostBudget, PMCostBudget.inventoryID>>) e).Cache.SetDefaultExt<PMCostBudget.curyUnitRate>((object) e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMCostBudget, PMCostBudget.uOM> e)
  {
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMCostBudget, PMCostBudget.uOM>>) e).Cache.SetDefaultExt<PMCostBudget.curyUnitRate>((object) e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMCostBudget, PMCostBudget.revenueTaskID> e)
  {
    if (PXResultset<PMRevenueBudget>.op_Implicit(((PXSelectBase<PMRevenueBudget>) new PXSelect<PMRevenueBudget, Where<PMRevenueBudget.projectID, Equal<Current<PMCostBudget.projectID>>, And<PMRevenueBudget.projectTaskID, Equal<Current<PMCostBudget.revenueTaskID>>, And<PMRevenueBudget.inventoryID, Equal<Current<PMCostBudget.inventoryID>>>>>>((PXGraph) this)).Select(Array.Empty<object>())) != null)
      return;
    e.Row.RevenueInventoryID = new int?();
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMCostBudget, PMCostBudget.costCodeID> e)
  {
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<PMCostBudget, PMCostBudget.costCodeID>>) e).Cache.SetDefaultExt<PMCostBudget.description>((object) e.Row);
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<PMCostBudget, PMCostBudget.description> e)
  {
    if (e.Row == null || ((PXSelectBase<PMProject>) this.Project).Current == null)
      return;
    if (((PXSelectBase<PMProject>) this.Project).Current.CostBudgetLevel == "C" || ((PXSelectBase<PMProject>) this.Project).Current.CostBudgetLevel == "D")
    {
      if (!e.Row.CostCodeID.HasValue || !(PXSelectorAttribute.Select<PMCostBudget.costCodeID>(((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<PMCostBudget, PMCostBudget.description>>) e).Cache, (object) e.Row) is PMCostCode pmCostCode))
        return;
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMCostBudget, PMCostBudget.description>, PMCostBudget, object>) e).NewValue = (object) pmCostCode.Description;
    }
    else if (((PXSelectBase<PMProject>) this.Project).Current.CostBudgetLevel == "T")
    {
      if (!e.Row.ProjectTaskID.HasValue || !(PXSelectorAttribute.Select<PMCostBudget.projectTaskID>(((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<PMCostBudget, PMCostBudget.description>>) e).Cache, (object) e.Row) is PMTask pmTask))
        return;
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMCostBudget, PMCostBudget.description>, PMCostBudget, object>) e).NewValue = (object) pmTask.Description;
    }
    else
    {
      if (!(((PXSelectBase<PMProject>) this.Project).Current.CostBudgetLevel == "I") || !e.Row.InventoryID.HasValue)
        return;
      int? inventoryId = e.Row.InventoryID;
      int emptyInventoryId = PMInventorySelectorAttribute.EmptyInventoryID;
      if (inventoryId.GetValueOrDefault() == emptyInventoryId & inventoryId.HasValue || !(PXSelectorAttribute.Select<PMCostBudget.inventoryID>(((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<PMCostBudget, PMCostBudget.description>>) e).Cache, (object) e.Row) is PX.Objects.IN.InventoryItem inventoryItem))
        return;
      ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<PMCostBudget, PMCostBudget.description>, PMCostBudget, object>) e).NewValue = (object) inventoryItem.Descr;
    }
  }

  protected virtual void _(PX.Data.Events.RowPersisting<PMTask> e)
  {
    if (e.Operation != 2)
      return;
    this.negativeKey = e.Row.TaskID;
  }

  protected virtual void _(PX.Data.Events.RowPersisted<PMTask> e)
  {
    if (e.Operation == 2 && e.TranStatus == null && this.negativeKey.HasValue)
    {
      int? taskId = e.Row.TaskID;
      foreach (PMCostBudget pmCostBudget in ((PXSelectBase) this.CostBudget).Cache.Inserted)
      {
        int? revenueTaskId = pmCostBudget.RevenueTaskID;
        if (revenueTaskId.HasValue)
        {
          revenueTaskId = pmCostBudget.RevenueTaskID;
          int? negativeKey = this.negativeKey;
          if (revenueTaskId.GetValueOrDefault() == negativeKey.GetValueOrDefault() & revenueTaskId.HasValue == negativeKey.HasValue)
          {
            ((PXSelectBase) this.CostBudget).Cache.SetValue<PMCostBudget.revenueTaskID>((object) pmCostBudget, (object) taskId);
            if (!this.persistedTask.ContainsKey(taskId))
              this.persistedTask.Add(taskId, this.negativeKey);
          }
        }
      }
      this.negativeKey = new int?();
    }
    if (e.Operation != 2 || e.TranStatus != 2)
      return;
    foreach (PMCostBudget pmCostBudget in ((PXSelectBase) this.CostBudget).Cache.Inserted)
    {
      if (pmCostBudget.RevenueTaskID.HasValue && this.persistedTask.TryGetValue(e.Row.TaskID, out this.negativeKey))
        ((PXSelectBase) this.CostBudget).Cache.SetValue<PMCostBudget.revenueTaskID>((object) pmCostBudget, (object) this.negativeKey);
    }
    foreach (PMCostBudget pmCostBudget in ((PXSelectBase) this.CostBudget).Cache.Updated)
    {
      if (pmCostBudget.RevenueTaskID.HasValue && this.persistedTask.TryGetValue(e.Row.TaskID, out this.negativeKey))
        ((PXSelectBase) this.CostBudget).Cache.SetValue<PMCostBudget.revenueTaskID>((object) pmCostBudget, (object) this.negativeKey);
    }
  }

  protected virtual void _(PX.Data.Events.FieldUpdated<PMTask, PMTask.isDefault> e)
  {
    if (!e.Row.IsDefault.GetValueOrDefault())
      return;
    bool flag = false;
    foreach (PXResult<PMTask> pxResult in ((PXSelectBase<PMTask>) this.Tasks).Select(Array.Empty<object>()))
    {
      PMTask pmTask = PXResult<PMTask>.op_Implicit(pxResult);
      if (pmTask.IsDefault.GetValueOrDefault())
      {
        int? taskId1 = pmTask.TaskID;
        int? taskId2 = e.Row.TaskID;
        if (!(taskId1.GetValueOrDefault() == taskId2.GetValueOrDefault() & taskId1.HasValue == taskId2.HasValue))
        {
          ((PXSelectBase) this.Tasks).Cache.SetValue<PMTask.isDefault>((object) pmTask, (object) false);
          GraphHelper.SmartSetStatus(((PXSelectBase) this.Tasks).Cache, (object) pmTask, (PXEntryStatus) 1, (PXEntryStatus) 0);
          flag = true;
        }
      }
    }
    if (!flag)
      return;
    ((PXSelectBase) this.Tasks).View.RequestRefresh();
  }

  protected virtual void _(PX.Data.Events.RowSelected<NotificationSource> e)
  {
    if (e.Row == null)
      return;
    NotificationSetup notificationSetup = PXResultset<NotificationSetup>.op_Implicit(PXSelectBase<NotificationSetup, PXSelect<NotificationSetup, Where<NotificationSetup.setupID, Equal<Required<NotificationSetup.setupID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) e.Row.SetupID
    }));
    if (notificationSetup != null && notificationSetup.NotificationCD == "PROFORMA")
      PXUIFieldAttribute.SetEnabled<NotificationSource.active>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<NotificationSource>>) e).Cache, (object) e.Row, false);
    else
      PXUIFieldAttribute.SetEnabled<NotificationSource.active>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<NotificationSource>>) e).Cache, (object) e.Row, true);
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<NotificationSource.reportID> e)
  {
    if ((string) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<NotificationSource.reportID>, object, object>) e).NewValue == "PM644000" || (string) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<NotificationSource.reportID>, object, object>) e).NewValue == "PM644500")
      throw new PXSetPropertyException("The AIA Report ({0}) and AIA Report with Quantity ({1}) reports cannot be used in mailing settings. These reports are to be generated only by clicking the AIA Report button on the form toolbar of the Projects (PM301000) and Pro Forma Invoices (PM307000) forms.", new object[2]
      {
        (object) "PM644000",
        (object) "PM644500"
      });
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<TemplateMaint.CopyDialogInfo, TemplateMaint.CopyDialogInfo.templateID> e)
  {
    PMProject pmProject = PXResultset<PMProject>.op_Implicit(((PXSelectBase<PMProject>) new PXSelect<PMProject, Where<PMProject.contractCD, Equal<Required<PMProject.contractCD>>, And<PMProject.baseType, Equal<CTPRType.projectTemplate>>>>((PXGraph) this)).Select(new object[1]
    {
      ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<TemplateMaint.CopyDialogInfo, TemplateMaint.CopyDialogInfo.templateID>, TemplateMaint.CopyDialogInfo, object>) e).NewValue
    }));
    if (pmProject != null)
      throw new PXSetPropertyException<TemplateMaint.CopyDialogInfo.templateID>("The project template with the {0} identifier already exists. Specify another project template ID.", new object[1]
      {
        (object) pmProject.ContractCD.Trim()
      });
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PMProjectContact, PMProjectContact.contactID> e)
  {
    e.Row.RoleID = (string) null;
  }

  protected virtual void _(PX.Data.Events.RowPersisting<PMProjectContact> e)
  {
    if (e.Operation == 3 || !e.Row.ContactID.HasValue)
      return;
    PX.Objects.CR.Contact contact = PX.Objects.CR.Contact.PK.Find((PXGraph) this, e.Row.ContactID);
    if (contact == null || contact.IsActive.GetValueOrDefault())
      return;
    ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<PMProjectContact>>) e).Cache.RaiseExceptionHandling<PMProjectContact.contactID>((object) e.Row, (object) contact.DisplayName, (Exception) new PXSetPropertyException<PMProjectContact.contactID>(contact.ContactType == "EP" ? "The employee is not active." : "The contact is not active."));
  }

  /// <summary>
  /// Creates a new instance of ProjectEntry graph and inserts copies of entities from current graph.
  /// Redirects to target graph on completion.
  /// </summary>
  public virtual void Copy(PMProject project)
  {
    int num = DimensionMaint.IsAutonumbered((PXGraph) this, "TMPROJECT") ? 1 : 0;
    string str = (string) null;
    if (num == 0)
    {
      if (!WebDialogResultExtension.IsPositive(((PXSelectBase<TemplateMaint.CopyDialogInfo>) this.CopyDialog).AskExt()) || string.IsNullOrEmpty(((PXSelectBase<TemplateMaint.CopyDialogInfo>) this.CopyDialog).Current.TemplateID))
        return;
      str = ((PXSelectBase<TemplateMaint.CopyDialogInfo>) this.CopyDialog).Current.TemplateID;
    }
    TemplateMaint instance = PXGraph.CreateInstance<TemplateMaint>();
    ((PXGraph) instance).SelectTimeStamp();
    instance.IsCopyPaste = true;
    instance.CopySource = this;
    PMProject copy1 = (PMProject) ((PXSelectBase) this.Project).Cache.CreateCopy((object) project);
    copy1.ContractID = new int?();
    copy1.ContractCD = str;
    copy1.Status = (string) null;
    copy1.Hold = new bool?();
    copy1.StartDate = new DateTime?();
    copy1.ExpireDate = new DateTime?();
    copy1.BudgetFinalized = new bool?();
    copy1.LastChangeOrderNumber = (string) null;
    copy1.IsActive = new bool?();
    copy1.IsCompleted = new bool?();
    copy1.IsCancelled = new bool?();
    copy1.NoteID = new Guid?();
    PMProject pmProject = ((PXSelectBase<PMProject>) instance.Project).Insert(copy1);
    ((PXSelectBase) instance.Billing).Cache.Clear();
    ContractBillingSchedule copy2 = (ContractBillingSchedule) ((PXSelectBase) this.Billing).Cache.CreateCopy((object) ((PXSelectBase<ContractBillingSchedule>) this.Billing).SelectSingle(Array.Empty<object>()));
    copy2.ContractID = pmProject.ContractID;
    copy2.LastDate = new DateTime?();
    ((PXSelectBase<ContractBillingSchedule>) instance.Billing).Insert(copy2);
    Dictionary<int, int> taskMap = new Dictionary<int, int>();
    foreach (PXResult<PMTask> pxResult in ((PXSelectBase<PMTask>) this.Tasks).Select(Array.Empty<object>()))
    {
      PMTask pmTask1 = PXResult<PMTask>.op_Implicit(pxResult);
      PMTask copy3 = (PMTask) ((PXSelectBase) this.Tasks).Cache.CreateCopy((object) pmTask1);
      copy3.TaskID = new int?();
      copy3.ProjectID = pmProject.ContractID;
      copy3.IsActive = new bool?();
      copy3.IsCompleted = new bool?();
      copy3.IsCancelled = new bool?();
      copy3.Status = (string) null;
      PMTask pmTask2 = copy3;
      DateTime? nullable1 = new DateTime?();
      DateTime? nullable2 = nullable1;
      pmTask2.StartDate = nullable2;
      PMTask pmTask3 = copy3;
      nullable1 = new DateTime?();
      DateTime? nullable3 = nullable1;
      pmTask3.EndDate = nullable3;
      PMTask pmTask4 = copy3;
      nullable1 = new DateTime?();
      DateTime? nullable4 = nullable1;
      pmTask4.PlannedStartDate = nullable4;
      PMTask pmTask5 = copy3;
      nullable1 = new DateTime?();
      DateTime? nullable5 = nullable1;
      pmTask5.PlannedEndDate = nullable5;
      copy3.CompletedPercent = new Decimal?();
      copy3.NoteID = new Guid?();
      nullable1 = pmTask1.PlannedStartDate;
      if (nullable1.HasValue)
      {
        nullable1 = ((PXGraph) this).Accessinfo.BusinessDate;
        if (nullable1.HasValue)
        {
          nullable1 = project.StartDate;
          if (nullable1.HasValue)
          {
            PMTask pmTask6 = copy3;
            nullable1 = ((PXGraph) this).Accessinfo.BusinessDate;
            DateTime dateTime1 = nullable1.Value;
            ref DateTime local1 = ref dateTime1;
            nullable1 = pmTask1.PlannedStartDate;
            DateTime dateTime2 = nullable1.Value;
            ref DateTime local2 = ref dateTime2;
            nullable1 = project.StartDate;
            DateTime dateTime3 = nullable1.Value;
            TimeSpan timeSpan = local2.Subtract(dateTime3);
            double totalDays1 = timeSpan.TotalDays;
            DateTime? nullable6 = new DateTime?(local1.AddDays(totalDays1));
            pmTask6.PlannedStartDate = nullable6;
            PMTask pmTask7 = copy3;
            nullable1 = copy3.PlannedStartDate;
            DateTime dateTime4 = nullable1.Value;
            ref DateTime local3 = ref dateTime4;
            nullable1 = pmTask1.PlannedEndDate;
            DateTime dateTime5 = nullable1.Value;
            ref DateTime local4 = ref dateTime5;
            nullable1 = pmTask1.PlannedStartDate;
            DateTime dateTime6 = nullable1.Value;
            timeSpan = local4.Subtract(dateTime6);
            double totalDays2 = timeSpan.TotalDays;
            DateTime? nullable7 = new DateTime?(local3.AddDays(totalDays2));
            pmTask7.PlannedEndDate = nullable7;
          }
        }
      }
      PMTask pmTask8 = ((PXSelectBase<PMTask>) instance.Tasks).Insert(copy3);
      taskMap.Add(pmTask1.TaskID.Value, pmTask8.TaskID.Value);
      instance.TaskAnswers.CopyAllAttributes((object) pmTask8, (object) pmTask1);
      this.OnCopyPasteTaskInserted(pmProject, pmTask8, pmTask1);
    }
    this.OnCopyPasteTasksInserted(instance, taskMap);
    foreach (PXResult<PMRevenueBudget> pxResult in ((PXSelectBase<PMRevenueBudget>) this.RevenueBudget).Select(Array.Empty<object>()))
    {
      PMRevenueBudget sourceItem = PXResult<PMRevenueBudget>.op_Implicit(pxResult);
      PMRevenueBudget copy4 = (PMRevenueBudget) ((PXSelectBase) this.RevenueBudget).Cache.CreateCopy((object) sourceItem);
      copy4.ProjectID = pmProject.ContractID;
      copy4.ProjectTaskID = new int?(taskMap[sourceItem.TaskID.Value]);
      copy4.ActualAmount = new Decimal?(0M);
      copy4.ActualQty = new Decimal?(0M);
      copy4.AmountToInvoice = new Decimal?(0M);
      copy4.ChangeOrderAmount = new Decimal?(0M);
      copy4.ChangeOrderQty = new Decimal?(0M);
      copy4.CuryCommittedOrigAmount = new Decimal?(0M);
      copy4.CommittedOrigAmount = new Decimal?(0M);
      copy4.CommittedOrigQty = new Decimal?(0M);
      copy4.CuryCommittedAmount = new Decimal?(0M);
      copy4.CommittedAmount = new Decimal?(0M);
      copy4.CuryCommittedInvoicedAmount = new Decimal?(0M);
      copy4.CuryCommittedOpenAmount = new Decimal?(0M);
      copy4.CommittedOpenQty = new Decimal?(0M);
      copy4.CommittedQty = new Decimal?(0M);
      copy4.CommittedReceivedQty = new Decimal?(0M);
      copy4.CompletedPct = new Decimal?(0M);
      copy4.CostAtCompletion = new Decimal?(0M);
      copy4.CostToComplete = new Decimal?(0M);
      copy4.InvoicedAmount = new Decimal?(0M);
      copy4.LastCostAtCompletion = new Decimal?(0M);
      copy4.LastCostToComplete = new Decimal?(0M);
      copy4.LastPercentCompleted = new Decimal?(0M);
      copy4.PercentCompleted = new Decimal?(0M);
      copy4.PrepaymentInvoiced = new Decimal?(0M);
      copy4.LineCntr = new int?();
      copy4.NoteID = new Guid?();
      PMRevenueBudget newItem = ((PXSelectBase<PMRevenueBudget>) instance.RevenueBudget).Insert(copy4);
      this.OnCopyPasteRevenueBudgetInserted(pmProject, newItem, sourceItem);
    }
    foreach (PXResult<PMCostBudget> pxResult in ((PXSelectBase<PMCostBudget>) this.CostBudget).Select(Array.Empty<object>()))
    {
      PMCostBudget sourceItem = PXResult<PMCostBudget>.op_Implicit(pxResult);
      PMCostBudget copy5 = (PMCostBudget) ((PXSelectBase) this.CostBudget).Cache.CreateCopy((object) sourceItem);
      copy5.ProjectID = pmProject.ContractID;
      copy5.ProjectTaskID = new int?(taskMap[sourceItem.TaskID.Value]);
      copy5.ActualAmount = new Decimal?(0M);
      copy5.ActualQty = new Decimal?(0M);
      copy5.AmountToInvoice = new Decimal?(0M);
      copy5.ChangeOrderAmount = new Decimal?(0M);
      copy5.ChangeOrderQty = new Decimal?(0M);
      copy5.CuryCommittedOrigAmount = new Decimal?(0M);
      copy5.CommittedOrigAmount = new Decimal?(0M);
      copy5.CommittedOrigQty = new Decimal?(0M);
      copy5.CuryCommittedAmount = new Decimal?(0M);
      copy5.CommittedAmount = new Decimal?(0M);
      copy5.CuryCommittedInvoicedAmount = new Decimal?(0M);
      copy5.CuryCommittedOpenAmount = new Decimal?(0M);
      copy5.CommittedOpenQty = new Decimal?(0M);
      copy5.CommittedQty = new Decimal?(0M);
      copy5.CommittedReceivedQty = new Decimal?(0M);
      copy5.CompletedPct = new Decimal?(0M);
      copy5.CostAtCompletion = new Decimal?(0M);
      copy5.CostToComplete = new Decimal?(0M);
      copy5.InvoicedAmount = new Decimal?(0M);
      copy5.LastCostAtCompletion = new Decimal?(0M);
      copy5.LastCostToComplete = new Decimal?(0M);
      copy5.LastPercentCompleted = new Decimal?(0M);
      copy5.PercentCompleted = new Decimal?(0M);
      copy5.PrepaymentInvoiced = new Decimal?(0M);
      copy5.LineCntr = new int?();
      copy5.NoteID = new Guid?();
      copy5.RevenueTaskID = !sourceItem.RevenueTaskID.HasValue ? new int?() : new int?(taskMap[sourceItem.RevenueTaskID.Value]);
      PMCostBudget newItem = ((PXSelectBase<PMCostBudget>) instance.CostBudget).Insert(copy5);
      this.OnCopyPasteCostBudgetInserted(pmProject, newItem, sourceItem);
    }
    foreach (PXResult<EPEmployeeContract> pxResult in ((PXSelectBase<EPEmployeeContract>) this.EmployeeContract).Select(Array.Empty<object>()))
    {
      EPEmployeeContract sourceItem = PXResult<EPEmployeeContract>.op_Implicit(pxResult);
      EPEmployeeContract copy6 = (EPEmployeeContract) ((PXSelectBase) this.EmployeeContract).Cache.CreateCopy((object) sourceItem);
      copy6.ContractID = pmProject.ContractID;
      EPEmployeeContract newItem = ((PXSelectBase<EPEmployeeContract>) instance.EmployeeContract).Insert(copy6);
      this.OnCopyPasteEmployeeContractInserted(pmProject, newItem, sourceItem);
    }
    foreach (PXResult<EPContractRate> pxResult in ((PXSelectBase<EPContractRate>) this.ContractRates).Select(Array.Empty<object>()))
    {
      EPContractRate sourceItem = PXResult<EPContractRate>.op_Implicit(pxResult);
      EPContractRate copy7 = (EPContractRate) ((PXSelectBase) this.ContractRates).Cache.CreateCopy((object) sourceItem);
      copy7.ContractID = pmProject.ContractID;
      EPContractRate newItem = ((PXSelectBase<EPContractRate>) instance.ContractRates).Insert(copy7);
      this.OnCopyPasteContractRateInserted(pmProject, newItem, sourceItem);
    }
    foreach (PXResult<EPEquipmentRate> pxResult in ((PXSelectBase<EPEquipmentRate>) this.EquipmentRates).Select(Array.Empty<object>()))
    {
      EPEquipmentRate sourceItem = PXResult<EPEquipmentRate>.op_Implicit(pxResult);
      EPEquipmentRate copy8 = (EPEquipmentRate) ((PXSelectBase) this.EquipmentRates).Cache.CreateCopy((object) sourceItem);
      copy8.ProjectID = pmProject.ContractID;
      copy8.NoteID = new Guid?();
      EPEquipmentRate newItem = ((PXSelectBase<EPEquipmentRate>) instance.EquipmentRates).Insert(copy8);
      this.OnCopyPasteEquipmentRateInserted(pmProject, newItem, sourceItem);
    }
    foreach (PXResult<PMProjectContact> pxResult in ((PXSelectBase<PMProjectContact>) this.ProjectContacts).Select(Array.Empty<object>()))
    {
      PMProjectContact sourceItem = PXResult<PMProjectContact>.op_Implicit(pxResult);
      int? contactId = sourceItem.ContactID;
      sourceItem.ContactID = new int?();
      PMProjectContact copy9 = (PMProjectContact) ((PXSelectBase) this.ProjectContacts).Cache.CreateCopy((object) sourceItem);
      copy9.ProjectID = new int?();
      copy9.NoteID = new Guid?();
      copy9.IsActive = new bool?();
      PMProjectContact newItem = ((PXSelectBase<PMProjectContact>) instance.ProjectContacts).Insert(copy9);
      newItem.ContactID = contactId;
      this.OnCopyPasteProjectContactInserted(pmProject, newItem, sourceItem);
    }
    foreach (PXResult<PMAccountTask> pxResult in ((PXSelectBase<PMAccountTask>) this.Accounts).Select(Array.Empty<object>()))
    {
      PMAccountTask sourceItem = PXResult<PMAccountTask>.op_Implicit(pxResult);
      PMAccountTask copy10 = (PMAccountTask) ((PXSelectBase) this.Accounts).Cache.CreateCopy((object) sourceItem);
      copy10.ProjectID = pmProject.ContractID;
      copy10.TaskID = new int?(taskMap[sourceItem.TaskID.Value]);
      copy10.NoteID = new Guid?();
      PMAccountTask newItem = ((PXSelectBase<PMAccountTask>) instance.Accounts).Insert(copy10);
      this.OnCopyPasteAccountTaskInserted(pmProject, newItem, sourceItem);
    }
    ((PXSelectBase) instance.NotificationSources).Cache.Clear();
    ((PXSelectBase) instance.NotificationRecipients).Cache.Clear();
    foreach (PXResult<NotificationSource> pxResult1 in ((PXSelectBase<NotificationSource>) this.NotificationSources).Select(Array.Empty<object>()))
    {
      NotificationSource sourceItem = PXResult<NotificationSource>.op_Implicit(pxResult1);
      int? sourceId = sourceItem.SourceID;
      sourceItem.SourceID = new int?();
      sourceItem.RefNoteID = new Guid?();
      NotificationSource newItem = ((PXSelectBase<NotificationSource>) instance.NotificationSources).Insert(sourceItem);
      foreach (PXResult<NotificationRecipient> pxResult2 in ((PXSelectBase<NotificationRecipient>) this.NotificationRecipients).Select(new object[1]
      {
        (object) sourceId
      }))
      {
        NotificationRecipient notificationRecipient = PXResult<NotificationRecipient>.op_Implicit(pxResult2);
        if (notificationRecipient.ContactType == "P" || notificationRecipient.ContactType == "E")
        {
          notificationRecipient.NotificationID = new int?();
          notificationRecipient.SourceID = newItem.SourceID;
          notificationRecipient.RefNoteID = new Guid?();
          ((PXSelectBase<NotificationRecipient>) instance.NotificationRecipients).Insert(notificationRecipient);
        }
      }
      this.OnCopyPasteNotificationSourceInserted(pmProject, newItem, sourceItem);
    }
    ((PXGraph) instance).Views.Caches.Add(typeof (PMRecurringItem));
    foreach (PXResult<PMRecurringItem> pxResult in PXSelectBase<PMRecurringItem, PXSelect<PMRecurringItem, Where<PMRecurringItem.projectID, Equal<Required<PMRecurringItem.projectID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) project.ContractID
    }))
    {
      PMRecurringItem sourceItem = PXResult<PMRecurringItem>.op_Implicit(pxResult);
      PMRecurringItem copy11 = (PMRecurringItem) ((PXGraph) this).Caches[typeof (PMRecurringItem)].CreateCopy((object) sourceItem);
      copy11.ProjectID = pmProject.ContractID;
      copy11.TaskID = new int?(taskMap[sourceItem.TaskID.Value]);
      copy11.Used = new Decimal?();
      copy11.LastBilledDate = new DateTime?();
      copy11.LastBilledQty = new Decimal?();
      copy11.NoteID = new Guid?();
      PMRecurringItem newItem = (PMRecurringItem) ((PXGraph) instance).Caches[typeof (PMRecurringItem)].Insert((object) copy11);
      this.OnCopyPasteRecurringItemInserted(pmProject, newItem, sourceItem);
    }
    ((PXSelectBase) instance.RetainageSteps).Cache.Clear();
    foreach (PXResult<PMRetainageStep> pxResult in ((PXSelectBase<PMRetainageStep>) this.RetainageSteps).Select(Array.Empty<object>()))
    {
      PMRetainageStep copy12 = (PMRetainageStep) ((PXSelectBase) this.RetainageSteps).Cache.CreateCopy((object) PXResult<PMRetainageStep>.op_Implicit(pxResult));
      copy12.ProjectID = pmProject.ContractID;
      copy12.NoteID = new Guid?();
      ((PXSelectBase<PMRetainageStep>) instance.RetainageSteps).Insert(copy12);
    }
    instance.Answers.CopyAllAttributes((object) pmProject, (object) project);
    this.OnCopyPasteCompleted(pmProject, project);
    ProjectAccountingService.OpenInTheSameWindow((PXGraph) instance);
  }

  protected virtual void OnCopyPasteTasksInserted(
    TemplateMaint target,
    Dictionary<int, int> taskMap)
  {
  }

  protected virtual void OnCopyPasteTaskInserted(
    PMProject target,
    PMTask newItem,
    PMTask sourceItem)
  {
  }

  protected virtual void OnCopyPasteRevenueBudgetInserted(
    PMProject target,
    PMRevenueBudget newItem,
    PMRevenueBudget sourceItem)
  {
  }

  protected virtual void OnCopyPasteCostBudgetInserted(
    PMProject target,
    PMCostBudget newItem,
    PMCostBudget sourceItem)
  {
  }

  protected virtual void OnCopyPasteEmployeeContractInserted(
    PMProject target,
    EPEmployeeContract newItem,
    EPEmployeeContract sourceItem)
  {
  }

  protected virtual void OnCopyPasteContractRateInserted(
    PMProject target,
    EPContractRate newItem,
    EPContractRate sourceItem)
  {
  }

  protected virtual void OnCopyPasteEquipmentRateInserted(
    PMProject target,
    EPEquipmentRate newItem,
    EPEquipmentRate sourceItem)
  {
  }

  protected virtual void OnCopyPasteProjectContactInserted(
    PMProject target,
    PMProjectContact newItem,
    PMProjectContact sourceItem)
  {
  }

  protected virtual void OnCopyPasteAccountTaskInserted(
    PMProject target,
    PMAccountTask newItem,
    PMAccountTask sourceItem)
  {
  }

  protected virtual void OnCopyPasteNotificationSourceInserted(
    PMProject target,
    NotificationSource newItem,
    NotificationSource sourceItem)
  {
  }

  protected virtual void OnCopyPasteRecurringItemInserted(
    PMProject target,
    PMRecurringItem newItem,
    PMRecurringItem sourceItem)
  {
  }

  protected virtual void OnCopyPasteCompleted(PMProject target, PMProject source)
  {
  }

  /// <summary>
  /// Returns true both for source as well as target graph during copy-paste procedure.
  /// </summary>
  public bool IsCopyPaste { get; private set; }

  /// <summary>
  /// During Paste of Copied Template this propert holds the reference to the Graph with source data.
  /// </summary>
  public TemplateMaint CopySource { get; private set; }

  public virtual bool PrepaymentVisible()
  {
    return ((PXSelectBase<PMProject>) this.Project).Current != null && ((PXSelectBase<PMProject>) this.Project).Current.PrepaymentEnabled.GetValueOrDefault();
  }

  public virtual bool LimitsVisible()
  {
    return ((PXSelectBase<PMProject>) this.Project).Current != null && ((PXSelectBase<PMProject>) this.Project).Current.LimitsEnabled.GetValueOrDefault();
  }

  public virtual void ValidateProjectCD(string projectCD, PXDBOperation operation)
  {
    if (PXResultset<PMProject>.op_Implicit(PXSelectBase<PMProject, PXViewOf<PMProject>.BasedOn<SelectFromBase<PMProject, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMProject.contractCD, Equal<P.AsString>>>>, And<Not<MatchUser>>>>.And<BqlOperand<PMProject.baseType, IBqlString>.IsEqual<CTPRType.projectTemplate>>>>.ReadOnly.Config>.Select((PXGraph) this, new object[1]
    {
      (object) projectCD
    })) != null)
      throw new PXException("The project template cannot be created because the specified project template ID ({0}) already exists in the system but your user has no sufficient access rights for it. Specify another Template ID.", new object[1]
      {
        (object) projectCD.Trim()
      });
    if (operation != 2)
      return;
    if (PXResultset<PMProject>.op_Implicit(PXSelectBase<PMProject, PXViewOf<PMProject>.BasedOn<SelectFromBase<PMProject, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<PMProject.contractCD, IBqlString>.IsEqual<P.AsString>>>.ReadOnly.Config>.Select((PXGraph) this, new object[1]
    {
      (object) projectCD
    })) != null)
      throw new PXException("The project template cannot be created because the specified project template ID ({0}) already exists in the system. Specify another Template ID.", new object[1]
      {
        (object) projectCD.Trim()
      });
  }

  public bool PrepareImportRow(string viewName, IDictionary keys, IDictionary values)
  {
    switch (viewName)
    {
      case "RevenueBudget":
        string str1 = (string) null;
        if (keys.Contains((object) "AccountGroupID"))
        {
          object key = keys[(object) "AccountGroupID"];
          if (key is int)
          {
            PMAccountGroup pmAccountGroup = PMAccountGroup.PK.Find((PXGraph) this, (int?) key);
            if (pmAccountGroup != null)
              return pmAccountGroup.Type == "I";
          }
          else
            str1 = (string) keys[(object) "AccountGroupID"];
        }
        if (string.IsNullOrEmpty(str1))
          return true;
        PMAccountGroup pmAccountGroup1 = PXResultset<PMAccountGroup>.op_Implicit(PXSelectBase<PMAccountGroup, PXSelect<PMAccountGroup, Where<PMAccountGroup.groupCD, Equal<Required<PMAccountGroup.groupCD>>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) str1
        }));
        return pmAccountGroup1 != null && pmAccountGroup1.Type == "I";
      case "CostBudget":
        string str2 = (string) null;
        if (keys.Contains((object) "AccountGroupID"))
          str2 = (string) keys[(object) "AccountGroupID"];
        if (string.IsNullOrEmpty(str2))
          return true;
        PMAccountGroup pmAccountGroup2 = PXResultset<PMAccountGroup>.op_Implicit(PXSelectBase<PMAccountGroup, PXSelect<PMAccountGroup, Where<PMAccountGroup.groupCD, Equal<Required<PMAccountGroup.groupCD>>>>.Config>.Select((PXGraph) this, new object[1]
        {
          (object) str2
        }));
        return pmAccountGroup2 != null && pmAccountGroup2.IsExpense.GetValueOrDefault();
      default:
        return true;
    }
  }

  public bool RowImporting(string viewName, object row) => true;

  public bool RowImported(string viewName, object row, object oldRow) => oldRow == null;

  public void PrepareItems(string viewName, IEnumerable items)
  {
  }

  public sealed class ProjectGroupMaskHelperExt : PX.Objects.PM.ProjectGroupMaskHelper<TemplateMaint>
  {
    public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.projectAccounting>();
  }

  [PXHidden]
  [ExcludeFromCodeCoverage]
  [Serializable]
  public class CopyDialogInfo : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXString]
    [PXUIField(DisplayName = "Template ID", Required = true)]
    [PXDimension("TMPROJECT")]
    public virtual string TemplateID { get; set; }

    public abstract class templateID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      TemplateMaint.CopyDialogInfo.templateID>
    {
    }
  }
}
