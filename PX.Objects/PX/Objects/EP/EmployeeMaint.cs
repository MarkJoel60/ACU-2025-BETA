// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EmployeeMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.EP;
using PX.Objects.AP;
using PX.Objects.CA;
using PX.Objects.Common;
using PX.Objects.CR;
using PX.Objects.CR.Extensions;
using PX.Objects.CR.GraphExtensions;
using PX.Objects.CS;
using PX.Objects.EP.DAC;
using PX.Objects.GL;
using PX.Objects.PM;
using PX.SM;
using PX.TM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.EP;

public class EmployeeMaint : PXGraph<EmployeeMaint>
{
  [PXViewName("Employee")]
  public PXSelectJoin<EPEmployee, LeftJoin<PX.Objects.GL.Branch, On<PX.Objects.GL.Branch.bAccountID, Equal<EPEmployee.parentBAccountID>>>, Where<EPEmployee.parentBAccountID, IsNull, Or<MatchWithBranch<PX.Objects.GL.Branch.branchID>>>> Employee;
  public PXSelect<BAccount2> BAccountParent;
  public PXSelect<EPEmployee, Where<EPEmployee.bAccountID, Equal<Current<EPEmployee.bAccountID>>>> CurrentEmployee;
  public PXSetup<EPEmployeeClass, Where<EPEmployeeClass.vendorClassID, Equal<Optional<EPEmployee.vendorClassID>>>> EmployeeClass;
  [PXViewName("Financial Settings")]
  [PXCopyPasteHiddenFields(new System.Type[] {typeof (PX.Objects.CR.Location.vPaymentMethodID)})]
  public PXSelect<PX.Objects.CR.Location, Where<PX.Objects.CR.Location.bAccountID, Equal<Current<EPEmployee.bAccountID>>, And<PX.Objects.CR.Location.locationID, Equal<Current<EPEmployee.defLocationID>>>>> DefLocation;
  [PXViewName("Address")]
  public PXSelect<PX.Objects.CR.Address, Where<PX.Objects.CR.Address.bAccountID, Equal<Optional<EPEmployee.parentBAccountID>>, And<PX.Objects.CR.Address.addressID, Equal<Optional<EPEmployee.defAddressID>>>>> Address;
  [PXViewName("Contact")]
  public SelectContactEmailSync<Where<PX.Objects.CR.Contact.bAccountID, Equal<Optional<EPEmployee.parentBAccountID>>, And<PX.Objects.CR.Contact.contactID, Equal<Optional<EPEmployee.defContactID>>>>> Contact;
  public PXSelectJoin<EMailSyncAccount, InnerJoin<PX.Objects.CR.BAccount, On<PX.Objects.CR.BAccount.bAccountID, Equal<EMailSyncAccount.employeeID>>>, Where<PX.Objects.CR.BAccount.defContactID, Equal<Optional<PX.Objects.CR.Contact.contactID>>>> SyncAccount;
  public PXSelect<EMailAccount, Where<EMailAccount.emailAccountID, Equal<Optional<EMailSyncAccount.emailAccountID>>>> EMailAccounts;
  [PXCopyPasteHiddenView]
  public PXSelectJoin<VendorPaymentMethodDetail, InnerJoin<PaymentMethodDetail, On<PaymentMethodDetail.paymentMethodID, Equal<VendorPaymentMethodDetail.paymentMethodID>, And<PaymentMethodDetail.detailID, Equal<VendorPaymentMethodDetail.detailID>, And<Where<PaymentMethodDetail.useFor, Equal<PaymentMethodDetailUsage.useForVendor>, Or<PaymentMethodDetail.useFor, Equal<PaymentMethodDetailUsage.useForAll>>>>>>>, Where<VendorPaymentMethodDetail.bAccountID, Equal<Optional<PX.Objects.CR.Location.bAccountID>>, And<VendorPaymentMethodDetail.locationID, Equal<Optional<PX.Objects.CR.Location.locationID>>, And<VendorPaymentMethodDetail.paymentMethodID, Equal<Optional<PX.Objects.CR.Location.vPaymentMethodID>>>>>, OrderBy<Asc<PaymentMethodDetail.orderIndex>>> PaymentDetails;
  public PXSelect<PaymentMethodDetail, Where<PaymentMethodDetail.paymentMethodID, Equal<Current<PX.Objects.CR.Location.vPaymentMethodID>>, And<Where<PaymentMethodDetail.useFor, Equal<PaymentMethodDetailUsage.useForVendor>, Or<PaymentMethodDetail.useFor, Equal<PaymentMethodDetailUsage.useForAll>>>>>> PaymentTypeDetails;
  public PXSelect<BAccount2, Where<BAccount2.acctCD, Equal<Required<BAccount2.acctCD>>>> BAccount;
  public PXSelectJoin<ContactNotification, InnerJoin<NotificationSetup, On<NotificationSetup.setupID, Equal<ContactNotification.setupID>>>, Where<ContactNotification.contactID, Equal<Optional<EPEmployee.defContactID>>>> NWatchers;
  public PXSelect<EPCompanyTreeMember, Where<EPCompanyTreeMember.contactID, Equal<Current<EPEmployee.defContactID>>>> CompanyTree;
  public PXSelect<PMLaborCostRate, Where<PMLaborCostRate.employeeID, Equal<Current<EPEmployee.bAccountID>>, And<PMLaborCostRate.type, Equal<PMLaborCostRateType.employee>>>, OrderBy<Asc<PMLaborCostRate.effectiveDate>>> EmployeeRates;
  [PXCopyPasteHiddenView]
  public PXSelect<EPEmployeePosition, Where<EPEmployeePosition.employeeID, Equal<Current<EPEmployee.bAccountID>>>, OrderBy<Desc<EPEmployeePosition.startDate>>> EmployeePositions;
  public PXSelect<EPEmployeePosition, Where<EPEmployeePosition.employeeID, Equal<Current<EPEmployee.bAccountID>>, And<EPEmployeePosition.isActive, Equal<True>>>> ActivePosition;
  public PXSelectJoin<EPEmployeeClassLaborMatrix, LeftJoin<PX.Objects.IN.InventoryItem, On<PX.Objects.IN.InventoryItem.inventoryID, Equal<EPEmployeeClassLaborMatrix.labourItemID>>, LeftJoin<EPEarningType, On<EPEarningType.typeCD, Equal<EPEmployeeClassLaborMatrix.earningType>>>>, Where<EPEmployeeClassLaborMatrix.employeeID, Equal<Current<EPEmployee.bAccountID>>>> LaborMatrix;
  [PXCopyPasteHiddenView]
  public PXSelectUsers<EPEmployee, Where<Users.pKID, Equal<Current<EPEmployee.userID>>>> User;
  [PXCopyPasteHiddenView]
  public PXSelectUsersInRoles UserRoles;
  [PXViewName("Answers")]
  public CRAttributeList<EPEmployee> Answers;
  public PXSetup<PX.Objects.GL.Branch, Where<PX.Objects.GL.Branch.bAccountID, Equal<Optional<EPEmployee.parentBAccountID>>>> company;
  public PXSetup<Company> companySetup;
  [PXHidden]
  public PXFilter<PX.Objects.EP.GenTimeCardFilter> GenTimeCardFilter;
  public PXSelectJoin<EPRule, InnerJoin<EPAssignmentMap, On<EPAssignmentMap.assignmentMapID, Equal<EPRule.assignmentMapID>>, LeftJoin<EPRuleApprover, On<EPRuleApprover.ruleID, Equal<EPRule.ruleID>>>>, Where<EPRule.ownerID, Equal<Current<EPEmployee.defContactID>>, Or<Current<EPEmployee.defContactID>, Equal<EPRuleApprover.ownerID>>>> AssigmentAndApprovalMaps;
  public PXSelect<PX.Objects.GL.Branch> BranchView;
  [PXCopyPasteHiddenView]
  public PXSelectJoin<EPEmployeeCorpCardLink, LeftJoin<CACorpCard, On<CACorpCard.corpCardID, Equal<EPEmployeeCorpCardLink.corpCardID>>>, Where<EPEmployeeCorpCardLink.employeeID, Equal<Current<EPEmployee.bAccountID>>>> EmployeeCorpCards;
  private bool isPaymentMergedFlag;
  public PXAction<EPEmployee> generateTimeCards;
  public PXSave<EPEmployee> Save;
  public PXInsert<EPEmployee> Insert;
  public PXCopyPasteAction<EPEmployee> Edit;
  public PXDelete<EPEmployee> Delete;
  public PXFirst<EPEmployee> First;
  public PXPrevious<EPEmployee> Prev;
  public PXNext<EPEmployee> Next;
  public PXLast<EPEmployee> Last;
  public PXMenuAction<EPEmployee> Action;
  public PXAction<EPEmployee> Cancel;
  public PXAction<EPEmployee> detachUser;
  public PXAction<EPEmployee> viewMap;
  public PXChangeBAccountID<EPEmployee, EPEmployee.acctCD> ChangeID;
  public PXMenuAction<EPEmployee> inquiry;
  public PXAction<EPEmployee> laborCostRates;
  private object _KeyToAbort;
  private bool doCopyClassSettings;

  public EmployeeMaint()
  {
    PXUIFieldAttribute.SetDisplayName<ContactNotification.classID>(this.NWatchers.Cache, "Customer/Vendor Class");
    PXUIFieldAttribute.SetDisplayName<EPRule.name>(this.AssigmentAndApprovalMaps.Cache, "Rule");
    PXUIFieldAttribute.SetDisplayName<EPRule.stepID>(this.AssigmentAndApprovalMaps.Cache, "Step");
    PXUIFieldAttribute.SetDisplayName<EPAssignmentMap.name>(this.Caches[typeof (EPAssignmentMap)], "Map");
    this.Action.AddMenuAction((PXAction) this.detachUser);
    this.Action.AddMenuAction((PXAction) this.ChangeID);
    this.Action.AddMenuAction((PXAction) this.generateTimeCards);
    this.inquiry.AddMenuAction((PXAction) this.laborCostRates);
    this.Caches<RedirectEmployeeParameters>();
    this.EmployeeCorpCards.AllowSelect = PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.expenseManagement>();
    this.AssigmentAndApprovalMaps.AllowSelect = PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.approvalWorkflow>();
  }

  public override void Clear()
  {
    RedirectEmployeeParameters current = this.Caches<RedirectEmployeeParameters>().Current as RedirectEmployeeParameters;
    base.Clear();
    if (current != null)
      this.Caches<RedirectEmployeeParameters>().Insert(current);
    this.isPaymentMergedFlag = false;
  }

  [PXUIField(DisplayName = "Generate Time Cards")]
  [PXButton]
  public virtual IEnumerable GenerateTimeCards(PXAdapter adapter)
  {
    if (this.Employee.Current != null && this.Employee.Current.BAccountID.HasValue && this.GenTimeCardFilter.AskExt((PXView.InitializePanel) ((gr, view) =>
    {
      if (!PXSelectBase<CREmployee, PXSelect<CREmployee, Where<CREmployee.bAccountID, Equal<Required<CREmployee.bAccountID>>, And<Where<CREmployee.defContactID, Equal<Current<AccessInfo.contactID>>, Or<CREmployee.defContactID, IsSubordinateOfContact<Current<AccessInfo.contactID>>, Or<CREmployee.bAccountID, WingmanUser<Current<AccessInfo.userID>, EPDelegationOf.timeEntries>>>>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, (object) this.Employee.Current.BAccountID).Any<PXResult<CREmployee>>())
        throw new PXException("No time cards have been generated. You can generate time cards for any of the following: your employee account, your subordinating employees according to the organization hierarchy on the Company Tree (EP204061) form, or for an employee for which you are specified as an appointed delegate on the Delegates tab of the Employees (EP203000) form.");
      this.GenTimeCardFilter.Cache.SetValueExt<PX.Objects.EP.GenTimeCardFilter.lastDateGenerated>((object) this.GenTimeCardFilter.Current, (object) (((EPTimeCard) PXSelectBase<EPTimeCard, PXSelect<EPTimeCard, Where<EPTimeCard.employeeID, Equal<Required<EPTimeCard.employeeID>>>, OrderBy<Desc<EPTimeCard.weekId>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, (object) this.Employee.Current.BAccountID) ?? throw new PXException("There are no time cards to generate.")).WeekStartDate ?? this.Accessinfo.BusinessDate).Value.AddDays(7.0));
      using (IEnumerator<PXResult<EPEmployeePosition>> enumerator = PXSelectBase<EPEmployeePosition, PXSelectGroupBy<EPEmployeePosition, Where<EPEmployeePosition.employeeID, Equal<Required<EPEmployee.bAccountID>>>, Aggregate<Max<EPEmployeePosition.startDate>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, (object) this.Employee.Current.BAccountID).GetEnumerator())
      {
        if (!enumerator.MoveNext())
          return;
        EPEmployeePosition current = (EPEmployeePosition) enumerator.Current;
        DateTime? startDate = current.StartDate;
        DateTime? lastDateGenerated = this.GenTimeCardFilter.Current.LastDateGenerated;
        if ((startDate.HasValue & lastDateGenerated.HasValue ? (startDate.GetValueOrDefault() < lastDateGenerated.GetValueOrDefault() ? 1 : 0) : 0) != 0)
          this.GenTimeCardFilter.Cache.SetValueExt<PX.Objects.EP.GenTimeCardFilter.generateUntil>((object) this.GenTimeCardFilter.Current, (object) this.GenTimeCardFilter.Current.LastDateGenerated);
        else
          this.GenTimeCardFilter.Cache.SetValueExt<PX.Objects.EP.GenTimeCardFilter.generateUntil>((object) this.GenTimeCardFilter.Current, (object) current.StartDate);
      }
    })) == WebDialogResult.OK)
      PXLongOperation.StartOperation((PXGraph) this, (PXToggleAsyncDelegate) (() =>
      {
        if (this.GenTimeCardFilter.Current.LastDateGenerated.HasValue && this.GenTimeCardFilter.Current.GenerateUntil.HasValue)
        {
          DateTime? generateUntil1 = this.GenTimeCardFilter.Current.GenerateUntil;
          DateTime? nullable = this.GenTimeCardFilter.Current.LastDateGenerated;
          if ((generateUntil1.HasValue & nullable.HasValue ? (generateUntil1.GetValueOrDefault() <= nullable.GetValueOrDefault() ? 1 : 0) : 0) == 0)
          {
            TimeCardMaint instance = PXGraph.CreateInstance<TimeCardMaint>();
            EPTimeCard data1 = (EPTimeCard) null;
            DateTime? generateUntil2;
            do
            {
              if (data1 != null)
              {
                instance.Document.Cache.SetValueExt<EPTimeCard.employeeID>((object) data1, (object) this.Employee.Current.BAccountID);
                instance.Document.Cache.SetValueExt<EPTimeCard.isReleased>((object) data1, (object) true);
                instance.Document.Cache.SetValueExt<EPTimeCard.isApproved>((object) data1, (object) true);
                EPTimeCard data2 = instance.Document.Insert(data1);
                instance.Document.Cache.SetValueExt<EPTimeCard.status>((object) data2, (object) "R");
                instance.Document.Cache.SetValueExt<EPTimeCard.isHold>((object) data2, (object) false);
                instance.Save.Press();
              }
              data1 = (EPTimeCard) instance.Document.Cache.CreateInstance();
              instance.Document.Cache.SetValueExt<EPTimeCard.weekId>((object) data1, (object) instance.GetNextWeekID(this.Employee.Current.BAccountID));
              nullable = data1.WeekStartDate;
              generateUntil2 = this.GenTimeCardFilter.Current.GenerateUntil;
            }
            while ((nullable.HasValue & generateUntil2.HasValue ? (nullable.GetValueOrDefault() < generateUntil2.GetValueOrDefault() ? 1 : 0) : 0) != 0);
            return;
          }
        }
        throw new PXOperationCompletedWithErrorException("Wrong dates have been specified.");
      }));
    return adapter.Get();
  }

  [PXButton]
  [PXUIField(DisplayName = "Detach Employee Login")]
  protected virtual IEnumerable DetachUser(PXAdapter a)
  {
    EPEmployee current = this.Employee.Current;
    PX.Objects.CR.Contact contact = (PX.Objects.CR.Contact) this.Contact.Select();
    if (current != null && contact != null)
    {
      if (this.AssigmentAndApprovalMaps.Select() != null)
      {
        if (this.AssigmentAndApprovalMaps.Ask("This employee is used in at least one assignment map, approval map, or workgroup. (View the Assignment and Approval Maps tab for the list of the relevant maps or the Company Tree tab for the list of the workgroups.) Detaching the login from the employee may affect the assignment process and cause delays in document processing. Are you sure you want to detach the login?", MessageButtons.OKCancel) != WebDialogResult.OK)
          return a.Get();
        PX.Objects.CR.Contact copy1 = PXCache<PX.Objects.CR.Contact>.CreateCopy(contact);
        copy1.UserID = new Guid?();
        this.Contact.Update(copy1);
        EPEmployee copy2 = PXCache<EPEmployee>.CreateCopy(current);
        copy2.UserID = new Guid?();
        this.Employee.Update(copy2);
        return a.Get();
      }
      if (this.CompanyTree.Select() != null)
      {
        if (this.AssigmentAndApprovalMaps.Ask("This employee is used in at least one workgroup in the company tree. (View the list of relevant workgroups on the Company Tree Info tab.) Detaching the login from the employee may affect the assignment process and cause delays in document processing. Are you sure you want to detach the login?", MessageButtons.OKCancel) != WebDialogResult.OK)
          return a.Get();
        PX.Objects.CR.Contact copy3 = PXCache<PX.Objects.CR.Contact>.CreateCopy(contact);
        copy3.UserID = new Guid?();
        this.Contact.Update(copy3);
        EPEmployee copy4 = PXCache<EPEmployee>.CreateCopy(current);
        copy4.UserID = new Guid?();
        this.Employee.Update(copy4);
        return a.Get();
      }
      PX.Objects.CR.Contact copy5 = PXCache<PX.Objects.CR.Contact>.CreateCopy(contact);
      copy5.UserID = new Guid?();
      this.Contact.Update(copy5);
      EPEmployee copy6 = PXCache<EPEmployee>.CreateCopy(current);
      copy6.UserID = new Guid?();
      this.Employee.Update(copy6);
    }
    return a.Get();
  }

  [PXUIField(DisplayName = "View Assignment Map", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select)]
  [PXButton]
  public virtual IEnumerable ViewMap(PXAdapter adapter)
  {
    EPRule current = this.AssigmentAndApprovalMaps.Current;
    if (current != null)
    {
      EPAssignmentMapMaint instance1 = PXGraph.CreateInstance<EPAssignmentMapMaint>();
      EPApprovalMapMaint instance2 = PXGraph.CreateInstance<EPApprovalMapMaint>();
      EPAssignmentMap epAssignmentMap1 = (EPAssignmentMap) instance1.AssigmentMap.Search<EPAssignmentMap.assignmentMapID>((object) current.AssignmentMapID);
      if (epAssignmentMap1 != null)
      {
        instance1.AssigmentMap.Current = epAssignmentMap1;
        PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance1, (string) null);
        requiredException.Mode = PXBaseRedirectException.WindowMode.NewWindow;
        throw requiredException;
      }
      EPAssignmentMap epAssignmentMap2 = (EPAssignmentMap) instance2.AssigmentMap.Search<EPAssignmentMap.assignmentMapID>((object) current.AssignmentMapID);
      if (epAssignmentMap2 != null)
      {
        instance2.AssigmentMap.Current = epAssignmentMap2;
        PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance2, (string) null);
        requiredException.Mode = PXBaseRedirectException.WindowMode.NewWindow;
        throw requiredException;
      }
    }
    return adapter.Get();
  }

  [PXButton(MenuAutoOpen = true, SpecialType = PXSpecialButtonType.InquiriesFolder)]
  [PXUIField(DisplayName = "Inquiries")]
  public virtual IEnumerable Inquiry(PXAdapter adapter) => adapter.Get();

  [PXUIField(DisplayName = "Labor Cost Rates", MapEnableRights = PXCacheRights.Select)]
  [PXButton]
  protected virtual IEnumerable LaborCostRates(PXAdapter adapter)
  {
    if (this.Employee.Current != null)
    {
      LaborCostRateMaint instance = PXGraph.CreateInstance<LaborCostRateMaint>();
      instance.Filter.Current.EmployeeID = this.Employee.Current.BAccountID;
      instance.Filter.Select();
      throw new PXRedirectRequiredException((PXGraph) instance, "Labor Cost Rates");
    }
    return adapter.Get();
  }

  [PXDBInt]
  [PXUIField(DisplayName = "Owner", Visibility = PXUIVisibility.Invisible)]
  protected virtual void BAccount2_OwnerID_CacheAttached(PXCache sender)
  {
  }

  [PXSelector(typeof (Users.pKID), SubstituteKey = typeof (Users.username), DescriptionField = typeof (Users.fullName), CacheGlobal = true, DirtyRead = true)]
  [PXUIField(DisplayName = "Employee Login", Visibility = PXUIVisibility.Visible)]
  [PXDBGuid(false)]
  protected virtual void EPEmployee_UserID_CacheAttached(PXCache cache)
  {
  }

  [PXMergeAttributes(Method = MergeMethod.Append)]
  [PXRemoveBaseAttribute(typeof (PXSelectorAttribute))]
  protected virtual void _(PX.Data.Events.CacheAttached<EPEmployee.baseCuryID> e)
  {
  }

  protected void ApplyUserAccessRights(params PXSelectBase[] datamembers)
  {
    string screenID = "SM201010";
    string screenId = PXContext.GetScreenID();
    AccessUsers instance;
    using (new PXPreserveScope())
    {
      try
      {
        PXContext.SetScreenID(screenID);
        instance = PXGraph.CreateInstance<AccessUsers>();
      }
      finally
      {
        PXContext.SetScreenID(screenId);
      }
    }
    instance.UserList.Current = (Users) PXSelectBase<Users, PXSelect<Users, Where<Users.pKID, Equal<Required<EPEmployee.userID>>>>.Config>.Select((PXGraph) instance, (object) this.Accessinfo.UserID);
    foreach (PXView pxView in ((IEnumerable<PXSelectBase>) datamembers).Select<PXSelectBase, PXView>((Func<PXSelectBase, PXView>) (sb => sb.View)))
    {
      PXCache cach = instance.Caches[pxView.CacheGetItemType()];
      pxView.AllowSelect = cach.AllowSelect;
      pxView.AllowInsert = cach.AllowInsert;
      pxView.AllowUpdate = cach.AllowUpdate;
      pxView.AllowDelete = cach.AllowUpdate;
    }
  }

  protected virtual void EPEmployee_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    EPEmployee row = (EPEmployee) e.Row;
    bool isVisible = PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.multicurrency>();
    PXUIFieldAttribute.SetVisible<EPEmployee.curyID>(cache, (object) null, isVisible);
    PXUIFieldAttribute.SetVisible<PX.Objects.AP.Vendor.allowOverrideCury>(cache, (object) null, isVisible);
    PXUIFieldAttribute.SetVisible<PX.Objects.AP.Vendor.allowOverrideRate>(cache, (object) null, isVisible);
    PXUIFieldAttribute.SetRequired<VendorClass.termsID>(cache, false);
    PXUIFieldAttribute.SetDisplayName<PX.Objects.CR.Contact.displayName>(this.Contact.Cache, "Employee Contact");
    bool isEnabled1 = e.Row != null && cache.GetStatus(e.Row) != PXEntryStatus.Inserted;
    if (isEnabled1)
      this.FillPaymentDetails();
    this.CompanyTree.Cache.AllowInsert = this.CompanyTree.Cache.AllowUpdate = this.CompanyTree.Cache.AllowDelete = row != null;
    PXAction<EPEmployee> generateTimeCards = this.generateTimeCards;
    Guid? userId;
    int num1;
    if (row != null)
    {
      userId = row.UserID;
      num1 = userId.HasValue ? 1 : 0;
    }
    else
      num1 = 0;
    generateTimeCards.SetEnabled(num1 != 0);
    this.laborCostRates.SetEnabled(isEnabled1);
    if (row == null)
      return;
    PXAction<EPEmployee> detachUser = this.detachUser;
    userId = row.UserID;
    int num2 = userId.HasValue ? 1 : 0;
    detachUser.SetEnabled(num2 != 0);
    userId = row.UserID;
    bool isEnabled2 = !userId.HasValue || this.User.Cache.GetStatus((object) this.User.Current) == PXEntryStatus.Inserted;
    bool flag = isEnabled2 && this.User.Current != null && this.User.Current.LoginTypeID.HasValue;
    PXUIFieldAttribute.SetEnabled<Users.loginTypeID>(this.User.Cache, (object) this.User.Current, isEnabled2);
    PXUIFieldAttribute.SetEnabled<Users.username>(this.User.Cache, (object) this.User.Current, flag);
    PXUIFieldAttribute.SetEnabled<Users.password>(this.User.Cache, (object) this.User.Current, flag);
    PXDefaultAttribute.SetPersistingCheck<Users.username>(this.User.Cache, (object) this.User.Current, flag ? PXPersistingCheck.NullOrBlank : PXPersistingCheck.Nothing);
    PXUIFieldAttribute.SetRequired<Users.username>(this.User.Cache, flag);
    PXUIFieldAttribute.SetRequired<Users.password>(this.User.Cache, flag);
    PXDefaultAttribute.SetPersistingCheck<PX.Objects.CR.Contact.eMail>(this.Contact.Cache, (object) this.Contact.Current, flag ? PXPersistingCheck.NullOrBlank : PXPersistingCheck.Nothing);
    PXUIFieldAttribute.SetRequired<PX.Objects.CR.Contact.eMail>(this.Contact.Cache, flag);
    this.User.Current = (Users) this.User.View.SelectSingleBound(new object[1]
    {
      e.Row
    });
    this.User.ToggleActions(this.User.Cache, this.User.Current);
  }

  protected virtual void Users_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    Users row = (Users) e.Row;
    if (row == null)
      return;
    bool isEnabled = this.User.Cache.GetStatus((object) this.User.Current) == PXEntryStatus.Inserted;
    bool flag1 = isEnabled && this.User.Current != null && this.User.Current.LoginTypeID.HasValue;
    PXUIFieldAttribute.SetEnabled<Users.loginTypeID>(this.User.Cache, (object) this.User.Current, isEnabled);
    PXUIFieldAttribute.SetEnabled<Users.username>(this.User.Cache, (object) this.User.Current, flag1);
    PXUIFieldAttribute.SetEnabled<Users.password>(this.User.Cache, (object) this.User.Current, flag1);
    PXDefaultAttribute.SetPersistingCheck<Users.username>(this.User.Cache, (object) this.User.Current, flag1 ? PXPersistingCheck.NullOrBlank : PXPersistingCheck.Nothing);
    PXUIFieldAttribute.SetRequired<Users.username>(this.User.Cache, flag1);
    bool? generatePassword = row.GeneratePassword;
    bool flag2 = generatePassword.HasValue && !generatePassword.GetValueOrDefault();
    PXUIFieldAttribute.SetEnabled<Users.password>(this.User.Cache, (object) row, flag2);
    PXUIFieldAttribute.SetRequired<Users.password>(this.User.Cache, flag2);
  }

  protected virtual void EPEmployee_DefLocationID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
  }

  protected virtual void EPEmployee_CuryID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.multicurrency>())
      return;
    if (this.companySetup.Current == null || string.IsNullOrEmpty(this.companySetup.Current.BaseCuryID))
      throw new PXException();
    e.NewValue = (object) this.companySetup.Current.BaseCuryID;
    e.Cancel = true;
  }

  protected virtual void EPEmployee_RowInserted(PXCache cache, PXRowInsertedEventArgs e)
  {
    EPEmployee row1 = (EPEmployee) e.Row;
    using (new ReadOnlyScope(new PXCache[3]
    {
      this.Address.Cache,
      this.Contact.Cache,
      this.DefLocation.Cache
    }))
    {
      PX.Objects.CR.Address address = (PX.Objects.CR.Address) this.Address.Cache.Insert((object) new PX.Objects.CR.Address()
      {
        BAccountID = this.Employee.Current.ParentBAccountID
      });
      PX.Objects.CR.Contact data = (PX.Objects.CR.Contact) this.Contact.Cache.Insert((object) new PX.Objects.CR.Contact()
      {
        BAccountID = this.Employee.Current.ParentBAccountID
      });
      data.Phone1Type = "H1";
      data.Phone2Type = "C";
      data.Phone3Type = "B1";
      data.FaxType = "HF";
      data.DefAddressID = address.AddressID;
      this.Employee.Current.DefContactID = data.ContactID;
      this.Employee.Current.DefAddressID = address.AddressID;
      this.Employee.Current.AcctName = data.DisplayName;
      foreach (PXResult<PX.Objects.CR.Standalone.Location, PX.Objects.GL.Branch, BAccount2> pxResult in PXSelectBase<PX.Objects.CR.Standalone.Location, PXSelectJoin<PX.Objects.CR.Standalone.Location, InnerJoin<PX.Objects.GL.Branch, On<PX.Objects.GL.Branch.bAccountID, Equal<PX.Objects.CR.Standalone.Location.bAccountID>>, InnerJoin<BAccount2, On<BAccount2.bAccountID, Equal<PX.Objects.CR.Standalone.Location.bAccountID>, And<BAccount2.defLocationID, Equal<PX.Objects.CR.Standalone.Location.locationID>>>>>, Where<BAccount2.bAccountID, Equal<Current<EPEmployee.parentBAccountID>>>>.Config>.Select((PXGraph) this))
      {
        this.Contact.Cache.SetValueExt<PX.Objects.CR.Contact.fullName>((object) data, (object) ((BAccount2) pxResult).AcctName);
        PX.Objects.CR.Location row2 = new PX.Objects.CR.Location()
        {
          BAccountID = ((PX.Objects.CR.BAccount) e.Row).BAccountID,
          LocType = "EP",
          Descr = PXMessages.LocalizeNoPrefix("Primary Location"),
          VTaxZoneID = ((PX.Objects.CR.Standalone.Location) pxResult).VTaxZoneID,
          VBranchID = ((PX.Objects.GL.Branch) pxResult).BranchID
        };
        object newValue = (object) PXMessages.LocalizeNoPrefix("MAIN");
        this.DefLocation.Cache.RaiseFieldUpdating<PX.Objects.CR.Location.locationCD>((object) row2, ref newValue);
        row2.LocationCD = (string) newValue;
        PX.Objects.CR.Location location = this.DefLocation.Insert(row2);
        if (location != null)
        {
          location.VAPAccountLocationID = location.LocationID;
          location.VPaymentInfoLocationID = location.LocationID;
          location.CARAccountLocationID = location.LocationID;
          location.VDefAddressID = address.AddressID;
          location.VDefContactID = data.ContactID;
          location.DefAddressID = address.AddressID;
          location.DefContactID = data.ContactID;
          location.VRemitAddressID = address.AddressID;
          location.VRemitContactID = data.ContactID;
          cache.SetValue<EPEmployee.defLocationID>(e.Row, (object) location.LocationID);
        }
      }
      this.FillPaymentDetails();
    }
  }

  protected virtual void EPEmployee_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    if (!(e.Row is EPEmployee row) || sender.ObjectsEqual<EPEmployee.parentBAccountID>(e.Row, e.OldRow))
      return;
    PX.Objects.GL.Branch branch = (PX.Objects.GL.Branch) PXSelectorAttribute.Select<EPEmployee.parentBAccountID>(sender, e.Row);
    if (row.ParentBAccountID.HasValue)
      this.BranchView.Cache.SetStatus((object) branch, PXEntryStatus.Updated);
    bool flag1 = false;
    foreach (PX.Objects.CR.Address address in this.Address.Cache.Inserted)
    {
      address.BAccountID = row.ParentBAccountID;
      address.CountryID = this.company.Current.CountryID;
      flag1 = true;
    }
    int? nullable1;
    if (!flag1)
    {
      PX.Objects.CR.Address address1 = (PX.Objects.CR.Address) this.Address.View.SelectSingleBound(new object[1]
      {
        e.OldRow
      }) ?? new PX.Objects.CR.Address();
      PX.Objects.CR.Address address2 = address1;
      nullable1 = row.ParentBAccountID;
      int? nullable2 = nullable1 ?? address1.BAccountID;
      address2.BAccountID = nullable2;
      address1.CountryID = this.company.Current.CountryID;
      this.Address.Cache.Update((object) address1);
    }
    bool flag2 = false;
    foreach (PX.Objects.CR.Contact contact1 in this.Contact.Cache.Inserted)
    {
      contact1.FullName = branch?.AcctName;
      contact1.BAccountID = row.ParentBAccountID;
      PX.Objects.CR.Contact contact2 = contact1;
      nullable1 = new int?();
      int? nullable3 = nullable1;
      contact2.DefAddressID = nullable3;
      foreach (PX.Objects.CR.Address address in this.Address.Cache.Inserted)
        contact1.DefAddressID = address.AddressID;
      flag2 = true;
    }
    if (!flag2)
    {
      PX.Objects.CR.Contact contact3 = (PX.Objects.CR.Contact) this.Contact.View.SelectSingleBound(new object[1]
      {
        e.OldRow
      }) ?? new PX.Objects.CR.Contact();
      contact3.FullName = branch?.AcctName;
      PX.Objects.CR.Contact contact4 = contact3;
      int? nullable4 = row.ParentBAccountID;
      int? nullable5 = nullable4 ?? contact3.BAccountID;
      contact4.BAccountID = nullable5;
      PX.Objects.CR.Contact contact5 = contact3;
      nullable4 = new int?();
      int? nullable6 = nullable4;
      contact5.DefAddressID = nullable6;
      foreach (PX.Objects.CR.Address address in this.Address.Cache.Inserted)
        contact3.DefAddressID = address.AddressID;
      this.Contact.Cache.Update((object) contact3);
    }
    bool flag3 = false;
    foreach (PX.Objects.CR.Location location in this.DefLocation.Cache.Inserted)
    {
      location.VBranchID = this.company.Current.BranchID;
      foreach (PX.Objects.CR.Address address in this.Address.Cache.Inserted)
        location.DefAddressID = address.AddressID;
      foreach (PX.Objects.CR.Contact contact in this.Contact.Cache.Inserted)
        location.DefContactID = contact.ContactID;
      flag3 = true;
    }
    if (flag3)
      return;
    PX.Objects.CR.Location location1 = (PX.Objects.CR.Location) this.DefLocation.View.SelectSingleBound(new object[1]
    {
      e.Row
    });
    location1.VBranchID = this.company.Current.BranchID;
    foreach (PX.Objects.CR.Address address in this.Address.Cache.Inserted)
    {
      location1.DefAddressID = address.AddressID;
      location1.VRemitAddressID = address.AddressID;
    }
    foreach (PX.Objects.CR.Contact contact in this.Contact.Cache.Inserted)
    {
      location1.DefContactID = contact.ContactID;
      location1.VRemitContactID = contact.ContactID;
    }
    this.DefLocation.Cache.Update((object) location1);
  }

  protected virtual void EPEmployee_VendorClassID_FieldVerifying(
    PXCache cache,
    PXFieldVerifyingEventArgs e)
  {
    EPEmployee row = (EPEmployee) e.Row;
    VendorClass vendorClass = (VendorClass) PXSelectorAttribute.Select<VendorClass.vendorClassID>(cache, (object) row, e.NewValue);
    this.doCopyClassSettings = false;
    if (vendorClass == null)
      return;
    this.doCopyClassSettings = true;
    if (cache.GetStatus((object) row) == PXEntryStatus.Inserted || this.Employee.Ask("Warning", "Please confirm if you want to update current Employee settings with the Employee Class defaults. Original settings will be preserved otherwise.", MessageButtons.YesNo) != WebDialogResult.No)
      return;
    this.doCopyClassSettings = false;
  }

  protected virtual void EPEmployee_VendorClassID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    this.EmployeeClass.RaiseFieldUpdated(sender, e.Row);
    if (((PX.Objects.AP.Vendor) e.Row).VendorClassID == null || !this.doCopyClassSettings)
      return;
    PX.Objects.CR.Location data = this.DefLocation.Current ?? (PX.Objects.CR.Location) this.DefLocation.Select();
    PXSelect<PX.Objects.CR.Location, Where<PX.Objects.CR.Location.bAccountID, Equal<Current<EPEmployee.bAccountID>>, And<PX.Objects.CR.Location.locationID, Equal<Current<EPEmployee.defLocationID>>>>> defLocation = this.DefLocation;
    if (defLocation.Current == null)
    {
      PX.Objects.CR.Location location;
      defLocation.Current = location = data;
    }
    sender.SetDefaultExt<EPEmployee.curyID>(e.Row);
    sender.SetDefaultExt<EPEmployee.curyRateTypeID>(e.Row);
    sender.SetDefaultExt<PX.Objects.AP.Vendor.allowOverrideCury>(e.Row);
    sender.SetDefaultExt<PX.Objects.AP.Vendor.allowOverrideRate>(e.Row);
    sender.SetDefaultExt<EPEmployee.calendarID>(e.Row);
    sender.SetDefaultExt<PX.Objects.AP.Vendor.termsID>(e.Row);
    sender.SetDefaultExt<EPEmployee.salesAcctID>(e.Row);
    sender.SetDefaultExt<EPEmployee.salesSubID>(e.Row);
    sender.SetDefaultExt<EPEmployee.expenseAcctID>(e.Row);
    sender.SetDefaultExt<EPEmployee.expenseSubID>(e.Row);
    sender.SetDefaultExt<EPEmployee.prepaymentAcctID>(e.Row);
    sender.SetDefaultExt<EPEmployee.prepaymentSubID>(e.Row);
    sender.SetDefaultExt<PX.Objects.AP.Vendor.discTakenAcctID>(e.Row);
    sender.SetDefaultExt<PX.Objects.AP.Vendor.discTakenSubID>(e.Row);
    sender.SetDefaultExt<EPEmployee.hoursValidation>(e.Row);
    this.DefLocation.Cache.SetDefaultExt<PX.Objects.CR.Location.vAPAccountID>((object) data);
    this.DefLocation.Cache.SetDefaultExt<PX.Objects.CR.Location.vAPSubID>((object) data);
    this.DefLocation.Cache.SetDefaultExt<PX.Objects.CR.Location.vTaxZoneID>((object) data);
    this.DefLocation.Cache.SetDefaultExt<PX.Objects.CR.Location.vCashAccountID>((object) data);
    this.DefLocation.Cache.SetDefaultExt<PX.Objects.CR.Location.vPaymentMethodID>((object) data);
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<EPEmployee, EPEmployee.vStatus> e)
  {
    EPEmployee row = e.Row;
    Guid userId1 = this.Accessinfo.UserID;
    Guid? userId2 = row.UserID;
    if ((userId2.HasValue ? (userId1 == userId2.GetValueOrDefault() ? 1 : 0) : 0) == 0)
      return;
    this.Employee.Cache.RaiseExceptionHandling<EPEmployee.vStatus>((object) e.Row, (object) row.VStatus, (Exception) new PXSetPropertyException("You cannot change the status of your own employee record."));
    e.NewValue = e.OldValue;
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<EPEmployee, EPEmployee.vStatus> e)
  {
    EPEmployee row = e.Row;
    Users users = (Users) PXSelectBase<Users, PXSelect<Users, Where<Users.pKID, Equal<Current<PX.Objects.CR.Contact.userID>>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
    {
      (object) this.Contact.Current
    });
    if (users != null)
    {
      users.IsApproved = new bool?(row.VStatus != "H" && row.VStatus != "I");
      this.User.Update(users);
    }
    PX.Objects.CR.Contact contact = this.Contact.SelectSingle();
    if (contact == null)
      return;
    contact.IsActive = new bool?(row.VStatus != "H" && row.VStatus != "I");
    this.Contact.Update(contact);
  }

  protected virtual void EPEmployee_UserID_FieldVerifying(
    PXCache cache,
    PXFieldVerifyingEventArgs e)
  {
    if (e.NewValue == null)
      return;
    EPEmployee row = (EPEmployee) e.Row;
    EPEmployee epEmployee = (EPEmployee) PXSelectBase<EPEmployee, PXSelect<EPEmployee, Where<EPEmployee.userID, Equal<Required<EPEmployee.userID>>>>.Config>.Select((PXGraph) this, e.NewValue);
    if (epEmployee != null && epEmployee.AcctCD != row.AcctCD)
    {
      Users users = (Users) PXSelectBase<Users, PXSelect<Users, Where<Users.pKID, Equal<Required<Users.pKID>>>>.Config>.Select((PXGraph) this, e.NewValue);
      this.Employee.Cache.RaiseExceptionHandling<EPEmployee.userID>(e.Row, (object) users.Username, (Exception) new PXSetPropertyException("This Login ID is assigned to Employee {0}: {1}. It cannot be associated with another Employee.", new object[2]
      {
        (object) epEmployee.AcctCD,
        (object) epEmployee.AcctName
      }));
      e.NewValue = (object) null;
    }
    else
      this.CompanyTree.Cache.Clear();
  }

  protected virtual void EPEmployee_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    EPEmployee row = (EPEmployee) e.Row;
    if (string.IsNullOrEmpty(row.TermsID) || e.Operation == PXDBOperation.Delete)
      return;
    PX.Objects.CS.Terms terms = (PX.Objects.CS.Terms) PXSelectorAttribute.Select<PX.Objects.AP.Vendor.termsID>(this.Employee.Cache, (object) row);
    if (terms != null)
    {
      Decimal? discPercent = terms.DiscPercent;
      if (discPercent.HasValue)
      {
        discPercent = terms.DiscPercent;
        if (discPercent.Value != 0M)
        {
          if (sender.RaiseExceptionHandling<PX.Objects.AP.Vendor.termsID>(e.Row, (object) row.TermsID, (Exception) new PXSetPropertyException("You cannot use Terms with configured Cash Discount for Employees.", new object[1]
          {
            (object) "[termsID]"
          })))
            throw new PXRowPersistingException(typeof (PX.Objects.AP.Vendor.termsID).Name, (object) row.TermsID, "You cannot use Terms with configured Cash Discount for Employees.", new object[1]
            {
              (object) "termsID"
            });
        }
      }
    }
    if (terms == null || !(terms.InstallmentType == "M"))
      return;
    if (sender.RaiseExceptionHandling<PX.Objects.AP.Vendor.termsID>(e.Row, (object) row.TermsID, (Exception) new PXSetPropertyException("You cannot use Terms with configured Multiple Installments for Employees.", new object[1]
    {
      (object) "[termsID]"
    })))
      throw new PXRowPersistingException(typeof (PX.Objects.AP.Vendor.termsID).Name, (object) row.TermsID, "You cannot use Terms with configured Multiple Installments for Employees.", new object[1]
      {
        (object) "termsID"
      });
  }

  protected virtual void EPEmployee_RowDeleting(PXCache sender, PXRowDeletingEventArgs e)
  {
    if (!string.IsNullOrEmpty(e.Row is EPEmployee row ? row.AcctCD : (string) null) && row.VStatus != "I" && sender.GetStatus(e.Row) != PXEntryStatus.InsertedDeleted)
    {
      e.Cancel = true;
      throw new PXException("Make this employee inactive before deleting");
    }
  }

  protected virtual void EPEmployee_RowDeleted(PXCache sender, PXRowDeletedEventArgs e)
  {
    Users users = (Users) PXSelectBase<Users, PXSelect<Users, Where<Users.pKID, Equal<Required<EPEmployee.userID>>>>.Config>.Select((PXGraph) this, (object) (e.Row as EPEmployee).UserID);
    if (users == null)
      return;
    users.LoginTypeID = new int?();
    users.IsApproved = new bool?(false);
    this.User.Update(users);
  }

  protected virtual void Location_BAccountID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    e.NewValue = this.Employee.Cache.GetValue<EPEmployee.bAccountID>((object) this.Employee.Current);
    e.Cancel = true;
  }

  protected virtual void Location_LocType_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if ((PX.Objects.CR.Location) PXSelectBase<PX.Objects.CR.Location, PXSelect<PX.Objects.CR.Location, Where<PX.Objects.CR.Location.bAccountID, Equal<Current<EPEmployee.parentBAccountID>>, And<PX.Objects.CR.Location.locationCD, Equal<Current<PX.Objects.CR.Location.locationCD>>>>>.Config>.SelectMultiBound((PXGraph) this, new object[1]
    {
      e.Row
    }) == null)
      return;
    e.NewValue = (object) "EP";
    e.Cancel = true;
  }

  protected virtual void Location_CBranchID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    e.NewValue = (object) null;
    e.Cancel = true;
  }

  protected virtual void Location_CARAccountLocationID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    e.NewValue = sender.GetValue<PX.Objects.CR.Location.locationID>(e.Row);
  }

  protected virtual void Location_VAPAccountLocationID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    e.NewValue = sender.GetValue<PX.Objects.CR.Location.locationID>(e.Row);
  }

  protected virtual void Location_VPaymentInfoLocationID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    e.NewValue = sender.GetValue<PX.Objects.CR.Location.locationID>(e.Row);
  }

  protected virtual void Location_VDefAddressID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    e.NewValue = this.Employee.Cache.GetValue<EPEmployee.defAddressID>((object) this.Employee.Current);
    e.Cancel = true;
  }

  protected virtual void Location_VDefContactID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    e.NewValue = this.Employee.Cache.GetValue<EPEmployee.defContactID>((object) this.Employee.Current);
    e.Cancel = true;
  }

  protected virtual void Location_VRemitAddressID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    e.NewValue = this.Employee.Cache.GetValue<EPEmployee.defAddressID>((object) this.Employee.Current);
    e.Cancel = true;
  }

  protected virtual void Location_VRemitContactID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    e.NewValue = this.Employee.Cache.GetValue<EPEmployee.defContactID>((object) this.Employee.Current);
    e.Cancel = true;
  }

  protected virtual void Location_VAPAccountID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    e.NewValue = this.EmployeeClass.Cache.GetValue<VendorClass.aPAcctID>((object) this.EmployeeClass.Current);
    e.Cancel = this.EmployeeClass.Current != null;
  }

  protected virtual void Location_VAPSubID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    e.NewValue = this.EmployeeClass.Cache.GetValue<VendorClass.aPSubID>((object) this.EmployeeClass.Current);
    e.Cancel = this.EmployeeClass.Current != null;
  }

  protected virtual void Location_VTaxZoneID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    e.NewValue = this.EmployeeClass.Cache.GetValue<EPEmployeeClass.taxZoneID>((object) this.EmployeeClass.Current);
    e.Cancel = this.EmployeeClass.Current != null;
  }

  protected virtual void Location_VCashAccountID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    PX.Objects.CR.Location row = (PX.Objects.CR.Location) e.Row;
    if (row == null)
      return;
    EPEmployeeClass current = this.EmployeeClass.Current;
    if (current != null && current.CashAcctID.HasValue && row.VPaymentMethodID == current.PaymentMethodID)
    {
      e.NewValue = (object) current.CashAcctID;
      e.Cancel = true;
    }
    else
    {
      e.NewValue = (object) null;
      e.Cancel = true;
    }
  }

  protected virtual void Location_VPaymentMethodID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    e.NewValue = this.EmployeeClass.Cache.GetValue<EPEmployeeClass.paymentMethodID>((object) this.EmployeeClass.Current);
    e.Cancel = this.EmployeeClass.Current != null;
  }

  protected virtual void Location_VPaymentMethodID_FieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
  }

  protected virtual void Location_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    PX.Objects.CR.Location row = (PX.Objects.CR.Location) e.Row;
    if (row == null)
      return;
    this.FillPaymentDetails(row);
    PXUIFieldAttribute.SetEnabled<PX.Objects.CR.Location.vCashAccountID>(sender, e.Row, !string.IsNullOrEmpty(row.VPaymentMethodID));
    PXUIFieldAttribute.SetRequired<PX.Objects.CR.Location.vAPAccountID>(sender, false);
    PXUIFieldAttribute.SetRequired<PX.Objects.CR.Location.vAPSubID>(sender, false);
  }

  protected virtual void Location_RowPersisted(PXCache sender, PXRowPersistedEventArgs e)
  {
    if (e.Operation != PXDBOperation.Insert)
      return;
    if (e.TranStatus == PXTranStatus.Open)
    {
      int? nullable = (int?) sender.GetValue<PX.Objects.CR.Location.vAPAccountLocationID>(e.Row);
      int num1 = 0;
      if (nullable.GetValueOrDefault() < num1 & nullable.HasValue)
      {
        this._KeyToAbort = sender.GetValue<PX.Objects.CR.Location.locationID>(e.Row);
        PXDatabase.Update<PX.Objects.CR.Location>((PXDataFieldParam) new PXDataFieldAssign("VAPAccountLocationID", this._KeyToAbort), (PXDataFieldParam) new PXDataFieldRestrict("LocationID", this._KeyToAbort), (PXDataFieldParam) PXDataFieldRestrict.OperationSwitchAllowed);
        sender.SetValue<PX.Objects.CR.Location.vAPAccountLocationID>(e.Row, this._KeyToAbort);
      }
      nullable = (int?) sender.GetValue<PX.Objects.CR.Location.vPaymentInfoLocationID>(e.Row);
      int num2 = 0;
      if (nullable.GetValueOrDefault() < num2 & nullable.HasValue)
      {
        this._KeyToAbort = sender.GetValue<PX.Objects.CR.Location.locationID>(e.Row);
        PXDatabase.Update<PX.Objects.CR.Location>((PXDataFieldParam) new PXDataFieldAssign("VPaymentInfoLocationID", this._KeyToAbort), (PXDataFieldParam) new PXDataFieldRestrict("LocationID", this._KeyToAbort), (PXDataFieldParam) PXDataFieldRestrict.OperationSwitchAllowed);
        sender.SetValue<PX.Objects.CR.Location.vPaymentInfoLocationID>(e.Row, this._KeyToAbort);
      }
      nullable = (int?) sender.GetValue<PX.Objects.CR.Location.cARAccountLocationID>(e.Row);
      int num3 = 0;
      if (!(nullable.GetValueOrDefault() < num3 & nullable.HasValue))
        return;
      this._KeyToAbort = sender.GetValue<PX.Objects.CR.Location.locationID>(e.Row);
      PXDatabase.Update<PX.Objects.CR.Location>((PXDataFieldParam) new PXDataFieldAssign("CARAccountLocationID", this._KeyToAbort), (PXDataFieldParam) new PXDataFieldRestrict("LocationID", this._KeyToAbort), (PXDataFieldParam) PXDataFieldRestrict.OperationSwitchAllowed);
      sender.SetValue<PX.Objects.CR.Location.cARAccountLocationID>(e.Row, this._KeyToAbort);
    }
    else
    {
      if (e.TranStatus == PXTranStatus.Aborted)
      {
        if (object.Equals(this._KeyToAbort, sender.GetValue<PX.Objects.CR.Location.vAPAccountLocationID>(e.Row)))
        {
          object obj = sender.GetValue<PX.Objects.CR.Location.locationID>(e.Row);
          sender.SetValue<PX.Objects.CR.Location.vAPAccountLocationID>(e.Row, obj);
        }
        if (object.Equals(this._KeyToAbort, sender.GetValue<PX.Objects.CR.Location.vPaymentInfoLocationID>(e.Row)))
        {
          object obj = sender.GetValue<PX.Objects.CR.Location.locationID>(e.Row);
          sender.SetValue<PX.Objects.CR.Location.vPaymentInfoLocationID>(e.Row, obj);
        }
        if (object.Equals(this._KeyToAbort, sender.GetValue<PX.Objects.CR.Location.cARAccountLocationID>(e.Row)))
        {
          object obj = sender.GetValue<PX.Objects.CR.Location.locationID>(e.Row);
          sender.SetValue<PX.Objects.CR.Location.cARAccountLocationID>(e.Row, obj);
        }
      }
      this._KeyToAbort = (object) null;
    }
  }

  [PXDBInt(IsKey = true)]
  [PXDBDefault(typeof (PX.Objects.CR.Location.bAccountID))]
  [PXUIField(DisplayName = "BAccountID", Visible = false, Enabled = false)]
  [PXParent(typeof (PX.Data.Select<EPEmployee, Where<EPEmployee.bAccountID, Equal<Current<VendorPaymentMethodDetail.bAccountID>>>>))]
  protected virtual void VendorPaymentMethodDetail_BAccountID_CacheAttached(PXCache sender)
  {
  }

  [PXDBInt(IsKey = true)]
  [PXDBDefault(typeof (PX.Objects.CR.Location.locationID))]
  [PXUIField(Visible = false, Enabled = false, Visibility = PXUIVisibility.Invisible)]
  [PXParent(typeof (PX.Data.Select<PX.Objects.CR.Location, Where<PX.Objects.CR.Location.bAccountID, Equal<Current<VendorPaymentMethodDetail.bAccountID>>, And<PX.Objects.CR.Location.locationID, Equal<Current<VendorPaymentMethodDetail.locationID>>>>>))]
  protected virtual void VendorPaymentMethodDetail_LocationID_CacheAttached(PXCache sender)
  {
  }

  [PXDBString(10, IsUnicode = true, IsKey = true, InputMask = ">aaaaaaaaaa")]
  [PXDefault(typeof (Search<PX.Objects.CR.Location.vPaymentMethodID, Where<PX.Objects.CR.Location.bAccountID, Equal<Current<VendorPaymentMethodDetail.bAccountID>>, And<PX.Objects.CR.Location.locationID, Equal<Current<VendorPaymentMethodDetail.locationID>>>>>))]
  [PXUIField(DisplayName = "Payment Method", Visible = false)]
  [PXSelector(typeof (Search<PX.Objects.CA.PaymentMethod.paymentMethodID>))]
  protected virtual void VendorPaymentMethodDetail_PaymentMethodID_CacheAttached(PXCache sender)
  {
  }

  [PXDBString(10, IsUnicode = true, IsKey = true)]
  [PXDefault]
  [PXUIField(DisplayName = "ID", Visible = false, Enabled = false)]
  [PXSelector(typeof (Search<PaymentMethodDetail.detailID, Where<PaymentMethodDetail.paymentMethodID, Equal<Current<VendorPaymentMethodDetail.paymentMethodID>>, And<Where<PaymentMethodDetail.useFor, Equal<PaymentMethodDetailUsage.useForVendor>, Or<PaymentMethodDetail.useFor, Equal<PaymentMethodDetailUsage.useForAll>>>>>>), DescriptionField = typeof (PaymentMethodDetail.descr))]
  protected virtual void VendorPaymentMethodDetail_DetailID_CacheAttached(PXCache sender)
  {
  }

  [PXDBInt(IsKey = true)]
  [PXParent(typeof (PX.Data.Select<EPEmployee, Where<EPEmployee.defContactID, Equal<Current<EPCompanyTreeMember.contactID>>>>))]
  [PXSelector(typeof (EPCompanyTree.workGroupID), SubstituteKey = typeof (EPCompanyTree.description))]
  [PXUIField(DisplayName = "Workgroup")]
  protected virtual void _(
    PX.Data.Events.CacheAttached<EPCompanyTreeMember.workGroupID> e)
  {
  }

  [PXDBInt(IsKey = true)]
  [PXDefault(typeof (EPEmployee.defContactID))]
  [PXUIField(DisplayName = "Member", Visible = false)]
  protected virtual void _(
    PX.Data.Events.CacheAttached<EPCompanyTreeMember.contactID> e)
  {
  }

  protected virtual void EPCompanyTreeMember_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    EPCompanyTreeMember row = e.Row as EPCompanyTreeMember;
    EPCompanyTreeMember oldRow = e.OldRow as EPCompanyTreeMember;
    if (row == null || oldRow == null)
      return;
    bool? isOwner = row.IsOwner;
    if (!isOwner.GetValueOrDefault())
      return;
    isOwner = oldRow.IsOwner;
    if (isOwner.GetValueOrDefault())
      return;
    foreach (PXResult<EPCompanyTreeMember> pxResult in PXSelectBase<EPCompanyTreeMember, PXSelect<EPCompanyTreeMember, Where<EPCompanyTreeMember.workGroupID, Equal<Required<EPCompanyTreeMember.workGroupID>>, And<EPCompanyTreeMember.isOwner, Equal<boolTrue>>>>.Config>.Select((PXGraph) this, (object) row.WorkGroupID))
    {
      EPCompanyTreeMember companyTreeMember = (EPCompanyTreeMember) pxResult;
      int? contactId1 = companyTreeMember.ContactID;
      int? contactId2 = row.ContactID;
      if (!(contactId1.GetValueOrDefault() == contactId2.GetValueOrDefault() & contactId1.HasValue == contactId2.HasValue))
      {
        EPCompanyTreeMember copy = PXCache<EPCompanyTreeMember>.CreateCopy(companyTreeMember);
        copy.IsOwner = new bool?(false);
        this.CompanyTree.Update(copy);
      }
    }
  }

  [PXDBIdentity(IsKey = true)]
  [PXUIField(DisplayName = "Contact ID", Visibility = PXUIVisibility.Invisible)]
  [PXParent(typeof (PX.Data.Select<PX.Objects.CR.BAccount, Where<PX.Objects.CR.BAccount.defContactID, Equal<Current<PX.Objects.CR.Contact.contactID>>>>))]
  protected virtual void Contact_ContactID_CacheAttached(PXCache sender)
  {
  }

  [PXDBInt]
  [PXUIField(DisplayName = "Owner", Visibility = PXUIVisibility.Invisible)]
  protected virtual void Contact_OwnerID_CacheAttached(PXCache sender)
  {
  }

  [PXDBString(2, IsFixed = true)]
  [PXDefault("EP")]
  protected virtual void Contact_ContactType_CacheAttached(PXCache sender)
  {
  }

  [PXDBString(100, IsUnicode = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Last Name", Visibility = PXUIVisibility.SelectorVisible)]
  [PXPersonalDataField]
  protected virtual void Contact_LastName_CacheAttached(PXCache sender)
  {
  }

  [Titles(ExclusiveValues = false)]
  [PXMergeAttributes(Method = MergeMethod.Merge)]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.CR.Contact.title> e)
  {
  }

  protected virtual void Contact_BAccountID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    e.Cancel = true;
  }

  protected virtual void Contact_BAccountID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (this.Employee.Current == null)
      return;
    e.NewValue = (object) this.Employee.Current.ParentBAccountID;
    e.Cancel = true;
  }

  protected virtual void Contact_Salutation_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    EPEmployeePosition data = (EPEmployeePosition) PXSelectBase<EPEmployeePosition, PXSelect<EPEmployeePosition, Where<EPEmployeePosition.employeeID, Equal<Current<EPEmployee.bAccountID>>, And<EPEmployeePosition.isActive, Equal<True>>>>.Config>.Select((PXGraph) this);
    if (data == null)
      return;
    e.NewValue = (object) ((EPPosition) PXSelectorAttribute.Select<EPEmployeePosition.positionID>(this.Employee.Cache, (object) data)).With<EPPosition, string>((Func<EPPosition, string>) (_ => _.Description));
  }

  protected virtual void EPEmployee_PositionID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    this.Contact.Cache.SetDefaultExt<PX.Objects.CR.Contact.salutation>((object) this.Contact.Current);
  }

  protected virtual void Contact_RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    if (!(e.Row is PX.Objects.CR.Contact row))
      return;
    this.Employee.Current.DefContactID = row.ContactID;
  }

  protected virtual void Contact_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    PX.Objects.CR.Contact row = (PX.Objects.CR.Contact) e.Row;
    if (row == null)
      return;
    bool flag = (!row.UserID.HasValue ? 1 : (this.User.Cache.GetStatus((object) this.User.Current) == PXEntryStatus.Inserted ? 1 : 0)) != 0 && this.User.Current != null && this.User.Current.LoginTypeID.HasValue;
    PXDefaultAttribute.SetPersistingCheck<PX.Objects.CR.Contact.eMail>(cache, (object) row, !flag || this.User.Current.Username == null ? PXPersistingCheck.Nothing : PXPersistingCheck.NullOrBlank);
    PXUIFieldAttribute.SetRequired<PX.Objects.CR.Contact.eMail>(cache, flag && this.User.Current.Username != null);
  }

  protected virtual void Contact_RowUpdated(PXCache cache, PXRowUpdatedEventArgs e)
  {
    this.Employee.SetValueExt<EPEmployee.acctName>(this.Employee.Current, (object) ((PX.Objects.CR.Contact) e.Row).DisplayName);
  }

  protected virtual void Contact_Email_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    PX.Objects.CR.Contact row = (PX.Objects.CR.Contact) e.Row;
    foreach (EMailSyncAccount emailSyncAccount in this.SyncAccount.Select((object) row.ContactID).RowCast<EMailSyncAccount>().Select<EMailSyncAccount, EMailSyncAccount>((Func<EMailSyncAccount, EMailSyncAccount>) (account => (EMailSyncAccount) this.SyncAccount.Cache.CreateCopy((object) account))))
    {
      emailSyncAccount.Address = row.EMail;
      emailSyncAccount.ContactsExportDate = new DateTime?();
      emailSyncAccount.ContactsImportDate = new DateTime?();
      emailSyncAccount.EmailsExportDate = new DateTime?();
      emailSyncAccount.EmailsImportDate = new DateTime?();
      emailSyncAccount.TasksExportDate = new DateTime?();
      emailSyncAccount.TasksImportDate = new DateTime?();
      emailSyncAccount.EventsExportDate = new DateTime?();
      emailSyncAccount.EventsImportDate = new DateTime?();
      EMailAccount emailAccount = (EMailAccount) this.EMailAccounts.Select((object) emailSyncAccount.EmailAccountID);
      if (emailAccount != null)
      {
        emailAccount.Address = emailSyncAccount.Address;
        this.EMailAccounts.Update(emailAccount);
      }
      this.SyncAccount.Update(emailSyncAccount);
    }
  }

  protected virtual void Contact_DefAddressID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    e.Cancel = true;
  }

  [PXDefault(typeof (Search<PX.Objects.GL.Branch.countryID, Where<PX.Objects.GL.Branch.bAccountID, Equal<Current<EPEmployee.parentBAccountID>>>>))]
  [PXMergeAttributes(Method = MergeMethod.Merge)]
  protected virtual void Address_CountryID_CacheAttached(PXCache sender)
  {
  }

  [PXDefault(typeof (IsNull<Current<RedirectEmployeeParameters.parentBAccountID>, CurrentBranchBAccountID>))]
  [PXDBInt]
  [PXUIField(DisplayName = "Branch")]
  [PXDimensionSelector("BIZACCT", typeof (Search<PX.Objects.GL.Branch.bAccountID, Where<PX.Objects.GL.Branch.active, Equal<True>, And<MatchWithBranch<PX.Objects.GL.Branch.branchID>>>>), typeof (PX.SM.Branch.branchCD), DescriptionField = typeof (PX.Objects.GL.Branch.acctName))]
  protected virtual void EPEmployee_ParentBAccountID_CacheAttached(PXCache sender)
  {
  }

  [PXDefault(true, typeof (RedirectEmployeeParameters.routeEmails))]
  [PXDBBool]
  [PXUIField(DisplayName = "Route Emails")]
  protected virtual void EPEmployee_RouteEmails_CacheAttached(PXCache sender)
  {
  }

  protected virtual void Address_BAccountID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    e.Cancel = true;
  }

  protected virtual void Address_BAccountID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (this.Employee.Current == null)
      return;
    e.NewValue = (object) this.Employee.Current.ParentBAccountID;
    e.Cancel = true;
  }

  protected virtual void Address_RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    if (!(e.Row is PX.Objects.CR.Address row))
      return;
    this.Employee.Current.DefAddressID = row.AddressID;
  }

  protected virtual void Address_CountryID_FieldUpdated(PXCache cache, PXFieldUpdatedEventArgs e)
  {
    PX.Objects.CR.Address row = (PX.Objects.CR.Address) e.Row;
    if (!((string) e.OldValue != row.CountryID))
      return;
    row.State = (string) null;
  }

  [PXDBInt]
  [PXDBChildIdentity(typeof (PX.Objects.CR.Address.addressID))]
  protected virtual void Location_DefAddressID_CacheAttached(PXCache sender)
  {
  }

  [PXDBInt]
  [PXDBChildIdentity(typeof (PX.Objects.CR.Contact.contactID))]
  protected virtual void Location_DefContactID_CacheAttached(PXCache sender)
  {
  }

  [PXInt]
  [PXUIField(DisplayName = "Contact")]
  [PXSelector(typeof (Search2<PX.Objects.CR.Contact.contactID, LeftJoin<Users, On<PX.Objects.CR.Contact.userID, Equal<Users.pKID>>>, Where<Current<Users.guest>, Equal<True>, And<PX.Objects.CR.Contact.contactType, Equal<ContactTypesAttribute.person>, Or<Current<Users.guest>, NotEqual<True>, And<PX.Objects.CR.Contact.contactType, Equal<ContactTypesAttribute.employee>>>>>>), new System.Type[] {typeof (PX.Objects.CR.Contact.displayName), typeof (PX.Objects.CR.Contact.salutation), typeof (PX.Objects.CR.Contact.fullName), typeof (PX.Objects.CR.Contact.eMail), typeof (Users.username)}, SubstituteKey = typeof (PX.Objects.CR.Contact.displayName))]
  [PXRestrictor(typeof (Where<PX.Objects.CR.Contact.eMail, IsNotNull, Or<Current<Users.source>, Equal<PXUsersSourceListAttribute.activeDirectory>>>), "Contact '{0}' does not have an email address.", new System.Type[] {typeof (PX.Objects.CR.Contact.displayName)})]
  [PXDBScalar(typeof (Search<PX.Objects.CR.Contact.contactID, Where<PX.Objects.CR.Contact.userID, Equal<Users.pKID>>>))]
  protected virtual void Users_ContactID_CacheAttached(PXCache sender)
  {
  }

  [PXDBGuid(false, IsKey = true)]
  [PXDefault]
  [PXUIField(Visibility = PXUIVisibility.Invisible)]
  public virtual void Users_PKID_CacheAttached(PXCache sender)
  {
  }

  [PXDBString(1)]
  [PXDefault("E")]
  [PXUIField(DisplayName = "Labor Rate Type")]
  public virtual void PMLaborCostRate_Type_CacheAttached(PXCache sender)
  {
  }

  [PXDBDefault(typeof (EPEmployee.bAccountID))]
  [PXDBInt]
  [PXUIField(DisplayName = "Employee")]
  public virtual void PMLaborCostRate_EmployeeID_CacheAttached(PXCache sender)
  {
  }

  [PXDBString(64 /*0x40*/, IsUnicode = true, InputMask = "")]
  [PXDefault]
  [PXUIField(DisplayName = "Login")]
  [PXUIRequired(typeof (Where<Users.loginTypeID, IsNotNull, And<EntryStatus, Equal<EntryStatus.inserted>>>))]
  public virtual void Users_Username_CacheAttached(PXCache sender)
  {
  }

  [PXDBInt]
  [PXUIField(DisplayName = "User Type")]
  [PXRestrictor(typeof (Where<EPLoginType.entity, Equal<EPLoginType.entity.employee>>), "Incorrect User Type '{0}', linked entity must be 'Employee'", new System.Type[] {typeof (EPLoginType.loginTypeName)})]
  [PXSelector(typeof (Search5<EPLoginType.loginTypeID, LeftJoin<EPManagedLoginType, On<EPLoginType.loginTypeID, Equal<EPManagedLoginType.loginTypeID>>, LeftJoin<Users, On<EPManagedLoginType.parentLoginTypeID, Equal<Users.loginTypeID>>, LeftJoin<ContactMaint.CurrentUser, On<ContactMaint.CurrentUser.pKID, Equal<Current<AccessInfo.userID>>>>>>, Where<EPLoginType.allowThisTypeForEmployees, Equal<True>, And<Users.pKID, Equal<ContactMaint.CurrentUser.pKID>, And<ContactMaint.CurrentUser.guest, Equal<True>, Or<ContactMaint.CurrentUser.guest, NotEqual<True>, And<EPLoginType.allowThisTypeForEmployees, Equal<True>>>>>>, Aggregate<GroupBy<EPLoginType.loginTypeID, GroupBy<EPLoginType.loginTypeName, GroupBy<EPLoginType.requireLoginActivation, GroupBy<EPLoginType.resetPasswordOnLogin>>>>>>), SubstituteKey = typeof (EPLoginType.loginTypeName))]
  [PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
  protected virtual void Users_LoginTypeID_CacheAttached(PXCache sender)
  {
  }

  [PXDBBool]
  [PXUIField(DisplayName = "Guest Account")]
  [PXFormula(typeof (Switch<Case<Where<Selector<Users.loginTypeID, EPLoginType.entity>, Equal<EPLoginType.entity.contact>>, True>, False>))]
  protected virtual void Users_Guest_CacheAttached(PXCache sender)
  {
  }

  [PXDBBool]
  [PXFormula(typeof (Selector<Users.loginTypeID, EPLoginType.requireLoginActivation>))]
  protected virtual void Users_IsPendingActivation_CacheAttached(PXCache sender)
  {
  }

  [PXDBBool]
  [PXUIField(DisplayName = "Force User to Change Password on Next Login")]
  [PXFormula(typeof (Switch<Case<Where<Selector<Users.loginTypeID, EPLoginType.resetPasswordOnLogin>, Equal<True>>, True>, False>))]
  protected virtual void Users_PasswordChangeOnNextLogin_CacheAttached(PXCache sender)
  {
  }

  [PXBool]
  [PXDefault(true, PersistingCheck = PXPersistingCheck.Nothing)]
  [PXUIField(DisplayName = "Generate Password")]
  protected virtual void Users_GeneratePassword_CacheAttached(PXCache sender)
  {
  }

  [PXDBString(256 /*0x0100*/, IsKey = true, IsUnicode = true, InputMask = "")]
  [PXDefault(typeof (Users.username))]
  [PXParent(typeof (PX.Data.Select<Users, Where<Users.username, Equal<Current<UsersInRoles.username>>>>))]
  protected virtual void UsersInRoles_Username_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes(Method = MergeMethod.Merge)]
  [PXRemoveBaseAttribute(typeof (PXSelectorAttribute))]
  protected virtual void _(PX.Data.Events.CacheAttached<UsersInRoles.rolename> e)
  {
  }

  [PXMergeAttributes(Method = MergeMethod.Append)]
  [PXDBDefault(typeof (EPEmployee.bAccountID))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<EPEmployeeCorpCardLink.employeeID> e)
  {
  }

  [PXMergeAttributes(Method = MergeMethod.Merge)]
  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "DisplayName", "Map")]
  protected virtual void _(PX.Data.Events.CacheAttached<EPAssignmentMap.name> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowSelected<EPEmployeePosition> e)
  {
    EPEmployeePosition row = e.Row;
    if (row == null || this.IsContractBasedAPI)
      return;
    PXUIFieldAttribute.SetEnabled<EPEmployeePosition.termReason>(this.EmployeePositions.Cache, (object) row, row.IsTerminated.GetValueOrDefault());
  }

  protected virtual void EPEmployeePosition_IsTerminated_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is EPEmployeePosition row))
      return;
    if (row.IsTerminated.GetValueOrDefault())
    {
      row.IsActive = new bool?(false);
    }
    else
    {
      row.IsRehirable = new bool?(false);
      row.TermReason = (string) null;
    }
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<EPEmployeePosition, EPEmployeePosition.startDate> e)
  {
    this.UpdateIfRequiredProbationPeriodEndDate(e.Row, e.Cache);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<EPEmployeePosition, EPEmployeePosition.startReason> e)
  {
    this.UpdateIfRequiredProbationPeriodEndDate(e.Row, e.Cache);
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<EPEmployeePosition, EPEmployeePosition.positionID> e)
  {
    this.UpdateIfRequiredProbationPeriodEndDate(e.Row, e.Cache);
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<EPEmployeePosition, EPEmployeePosition.startDate> e)
  {
    EPEmployeePosition row = e.Row;
    int valueOrDefault = this.EmployeeClass.Current.ProbationPeriodMonths.GetValueOrDefault();
    if (row == null || e.NewValue == null || e.OldValue == e.NewValue || row.StartReason != "NEW" && row.StartReason != "REH" || valueOrDefault == 0)
      return;
    e.Cache.RaiseExceptionHandling<EPEmployeePosition.probationPeriodEndDate>((object) row, (object) null, (Exception) new PXSetPropertyException("The probation period end date was updated based on the start date and the probation period specified on the Employee Classes (EP202000) form. You can adjust it manually, if needed.", PXErrorLevel.Warning));
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<EPEmployeePosition, EPEmployeePosition.startReason> e)
  {
    EPEmployeePosition row = e.Row;
    int valueOrDefault = this.EmployeeClass.Current.ProbationPeriodMonths.GetValueOrDefault();
    if (row == null || !row.StartDate.HasValue || e.OldValue == e.NewValue || (string) e.NewValue != "NEW" && (string) e.NewValue != "REH" || valueOrDefault == 0)
      return;
    e.Cache.RaiseExceptionHandling<EPEmployeePosition.probationPeriodEndDate>((object) row, (object) null, (Exception) new PXSetPropertyException("The probation period end date was updated based on the start date and the probation period specified on the Employee Classes (EP202000) form. You can adjust it manually, if needed.", PXErrorLevel.Warning));
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<EPEmployeePosition, EPEmployeePosition.probationPeriodEndDate> e)
  {
    EPEmployeePosition row = e.Row;
    if (row == null)
      return;
    DateTime? newValue = (DateTime?) e.NewValue;
    if (!newValue.HasValue)
      return;
    newValue = (DateTime?) e.NewValue;
    DateTime? startDate = row.StartDate;
    if ((newValue.HasValue & startDate.HasValue ? (newValue.GetValueOrDefault() < startDate.GetValueOrDefault() ? 1 : 0) : 0) != 0)
      throw new PXSetPropertyException("The end date of the probation period cannot be earlier than its start date.");
  }

  protected void UncheckEmployeePositionsIsActive()
  {
    bool flag = false;
    foreach (PXResult<EPEmployeePosition> pxResult in new PXSelect<EPEmployeePosition, Where<EPEmployeePosition.employeeID, Equal<Current<EPEmployeePosition.employeeID>>, And<EPEmployeePosition.lineNbr, NotEqual<Current<EPEmployeePosition.lineNbr>>, And<EPEmployeePosition.isActive, Equal<True>>>>>((PXGraph) this).Select())
    {
      EPEmployeePosition copy = PXCache<EPEmployeePosition>.CreateCopy((EPEmployeePosition) pxResult);
      copy.IsActive = new bool?(false);
      this.EmployeePositions.Update(copy);
      flag = true;
    }
    if (!flag)
      return;
    this.EmployeePositions.View.RequestRefresh();
  }

  protected void SetDefContactJobTitle()
  {
    PX.Objects.CR.Contact contact = this.Contact.SelectSingle();
    EPEmployeePosition employeePosition = (EPEmployeePosition) this.ActivePosition.Select();
    if (employeePosition == null || employeePosition.PositionID == null)
      return;
    EPPosition epPosition = (EPPosition) PXSelectBase<EPPosition, PXSelect<EPPosition, Where<EPPosition.positionID, Equal<Required<EPPosition.positionID>>>>.Config>.Select((PXGraph) this, (object) employeePosition.PositionID);
    contact.Salutation = epPosition.Description;
    this.Contact.Update(contact);
  }

  protected virtual void EPEmployeePosition_IsActive_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is EPEmployeePosition row) || !row.IsActive.GetValueOrDefault())
      return;
    row.IsTerminated = new bool?(false);
    row.TermReason = (string) null;
    row.IsRehirable = new bool?(false);
    this.UncheckEmployeePositionsIsActive();
    this.SetDefContactJobTitle();
  }

  protected virtual void EPEmployeePosition_RowInserting(PXCache sender, PXRowInsertingEventArgs e)
  {
    if (!(e.Row is EPEmployeePosition row))
      return;
    PXSelectBase<EPEmployeePosition> pxSelectBase = (PXSelectBase<EPEmployeePosition>) new PXSelect<EPEmployeePosition, Where<EPEmployeePosition.employeeID, Equal<Current<EPEmployeePosition.employeeID>>, And<EPEmployeePosition.lineNbr, NotEqual<Current<EPEmployeePosition.lineNbr>>, And<EPEmployeePosition.isActive, Equal<True>>>>>((PXGraph) this);
    if (this.IsExport || this.IsImport)
    {
      if (row.IsActive.GetValueOrDefault())
      {
        this.UncheckEmployeePositionsIsActive();
        this.SetDefContactJobTitle();
      }
    }
    else
    {
      PXView view = pxSelectBase.View;
      object[] currents = new object[1]{ (object) row };
      object[] objArray = Array.Empty<object>();
      row.IsActive = view.SelectMultiBound(currents, objArray).Count != 0 ? new bool?(false) : new bool?(true);
    }
    this.UpdateIfRequiredProbationPeriodEndDate(row, sender);
  }

  protected virtual void GenTimeCardFilter_GenerateUntil_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is PX.Objects.EP.GenTimeCardFilter row))
      return;
    DateTime? nullable = row.GenerateUntil;
    DateTime? lastDateGenerated = row.LastDateGenerated;
    if ((nullable.HasValue & lastDateGenerated.HasValue ? (nullable.GetValueOrDefault() < lastDateGenerated.GetValueOrDefault() ? 1 : 0) : 0) != 0)
      this.GenTimeCardFilter.Cache.RaiseExceptionHandling<PX.Objects.EP.GenTimeCardFilter.generateUntil>((object) this.GenTimeCardFilter.Current, (object) this.GenTimeCardFilter.Current.GenerateUntil, (Exception) new PXSetPropertyException("The Until date cannot be earlier than the From date.", PXErrorLevel.Error));
    DateTime? generateUntil = row.GenerateUntil;
    nullable = this.Accessinfo.BusinessDate;
    if ((generateUntil.HasValue & nullable.HasValue ? (generateUntil.GetValueOrDefault() > nullable.GetValueOrDefault() ? 1 : 0) : 0) == 0)
      return;
    this.GenTimeCardFilter.Cache.RaiseExceptionHandling<PX.Objects.EP.GenTimeCardFilter.generateUntil>((object) this.GenTimeCardFilter.Current, (object) this.GenTimeCardFilter.Current.GenerateUntil, (Exception) new PXSetPropertyException("The Until date is later than the current business date.", PXErrorLevel.Warning));
  }

  protected virtual void EPEmployeePosition_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    EPEmployeePosition row = (EPEmployeePosition) e.Row;
    if (row == null)
      return;
    DateTime? endDate = row.EndDate;
    DateTime? startDate = row.StartDate;
    if ((endDate.HasValue & startDate.HasValue ? (endDate.GetValueOrDefault() < startDate.GetValueOrDefault() ? 1 : 0) : 0) != 0)
      sender.RaiseExceptionHandling<EPEmployeePosition.endDate>(e.Row, (object) ((EPEmployeePosition) e.Row).EndDate, (Exception) new PXSetPropertyException("End Date must be greater than or equal to Start Date.", PXErrorLevel.Error));
    this.SetDefContactJobTitle();
  }

  public static Guid? GetCurrentEmployeeID(PXGraph graph)
  {
    return ((PX.Objects.EP.Simple.EPEmployee) PXSelectBase<PX.Objects.EP.Simple.EPEmployee, PXSelect<PX.Objects.EP.Simple.EPEmployee, Where<PX.Objects.EP.Simple.EPEmployee.userID, Equal<Current<AccessInfo.userID>>>>.Config>.SelectSingleBound(graph, (object[]) null))?.UserID;
  }

  public static int? GetCurrentOwnerID(PXGraph graph)
  {
    return ((PX.Objects.CR.Contact) PXSelectBase<PX.Objects.CR.Contact, PXSelect<PX.Objects.CR.Contact, Where<PX.Objects.CR.Contact.contactID, Equal<Current<AccessInfo.contactID>>, And<PX.Objects.CR.Contact.contactType, Equal<ContactTypesAttribute.employee>>>>.Config>.SelectSingleBound(graph, (object[]) null))?.ContactID;
  }

  public static EPEmployee GetCurrentEmployee(PXGraph graph)
  {
    PXSelectBase<EPEmployee, PXSelectReadonly<EPEmployee, Where<EPEmployee.userID, Equal<Current<AccessInfo.userID>>>>.Config>.Clear(graph);
    PXResultset<EPEmployee> pxResultset = PXSelectBase<EPEmployee, PXSelectReadonly<EPEmployee, Where<EPEmployee.userID, Equal<Current<AccessInfo.userID>>>>.Config>.Select(graph);
    return pxResultset != null && pxResultset.Count != 0 ? (EPEmployee) pxResultset[0][typeof (EPEmployee)] : (EPEmployee) null;
  }

  protected virtual void _(
    PX.Data.Events.FieldUpdated<PX.Objects.CR.Location, PX.Objects.CR.Location.vPaymentMethodID> e)
  {
    PX.Objects.CR.Location row = e.Row;
    string oldValue = (string) e.OldValue;
    if (!string.IsNullOrEmpty(oldValue))
      this.ClearPaymentDetails(row, oldValue, true);
    e.Cache.SetDefaultExt<PX.Objects.CR.Location.vCashAccountID>((object) e.Row);
    this.isPaymentMergedFlag = false;
    this.FillPaymentDetails(row);
    this.PaymentDetails.View.RequestRefresh();
  }

  protected virtual void _(PX.Data.Events.RowInserted<PX.Objects.CR.Location> e)
  {
    this.FillPaymentDetails(e.Row);
  }

  protected virtual void _(PX.Data.Events.RowSelected<VendorPaymentMethodDetail> e)
  {
    if (e.Row == null)
      return;
    VendorPaymentMethodDetail row = e.Row;
    PaymentMethodDetail template = this.FindTemplate(row);
    bool flag = template != null && template.IsRequired.GetValueOrDefault();
    PXDefaultAttribute.SetPersistingCheck<VendorPaymentMethodDetail.detailValue>(e.Cache, (object) row, flag ? PXPersistingCheck.NullOrBlank : PXPersistingCheck.Nothing);
  }

  protected virtual void _(
    PX.Data.Events.FieldSelecting<VendorPaymentMethodDetail, VendorPaymentMethodDetail.detailValue> e)
  {
    PaymentMethodDetailHelper.VendorDetailValueFieldSelecting((PXGraph) this, e);
  }

  protected virtual void _(PX.Data.Events.RowPersisting<VendorPaymentMethodDetail> e)
  {
    if (!e.Cancel && e.Row != null && !this.IsLocationPaymentMethodValid(e.Cache, (PX.Objects.CR.Location) null))
      throw new PXException("The record cannot be saved because at least one error has occurred. Please review the errors.");
  }

  public virtual PaymentMethodDetail FindTemplate(
    VendorPaymentMethodDetail vendorPaymentMethodDetail)
  {
    return (PaymentMethodDetail) PXSelectBase<PaymentMethodDetail, PXSelect<PaymentMethodDetail, Where<PaymentMethodDetail.paymentMethodID, Equal<Required<PaymentMethodDetail.paymentMethodID>>, And<PaymentMethodDetail.detailID, Equal<Required<PaymentMethodDetail.detailID>>, And<Where<PaymentMethodDetail.useFor, Equal<PaymentMethodDetailUsage.useForVendor>, Or<PaymentMethodDetail.useFor, Equal<PaymentMethodDetailUsage.useForAll>>>>>>>.Config>.Select((PXGraph) this, (object) vendorPaymentMethodDetail.PaymentMethodID, (object) vendorPaymentMethodDetail.DetailID);
  }

  public virtual bool IsLocationPaymentMethodValid(PXCache cache, PX.Objects.CR.Location location)
  {
    bool flag = true;
    foreach (PXResult<VendorPaymentMethodDetail> pxResult in this.PaymentDetails.Select())
    {
      if (!this.IsPaymentDetailValid((VendorPaymentMethodDetail) pxResult))
        flag = false;
    }
    return flag;
  }

  public virtual bool IsPaymentDetailValid(
    VendorPaymentMethodDetail vendorPaymentMethodDetail)
  {
    PaymentMethodDetail template = this.FindTemplate(vendorPaymentMethodDetail);
    if (template == null || !template.IsRequired.GetValueOrDefault() || !string.IsNullOrEmpty(vendorPaymentMethodDetail.DetailValue))
      return true;
    this.PaymentDetails.Cache.RaiseExceptionHandling<VendorPaymentMethodDetail.detailValue>((object) vendorPaymentMethodDetail, (object) vendorPaymentMethodDetail.DetailValue, (Exception) new PXSetPropertyException("This field is required."));
    return false;
  }

  [Obsolete("This item has been deprecated and will be removed in Acumatica ERP 2024 R2.")]
  public virtual void ClearPaymentDetails(bool clearNewOnly)
  {
    foreach (PXResult<VendorPaymentMethodDetail> pxResult in this.PaymentDetails.Select())
    {
      VendorPaymentMethodDetail paymentMethodDetail = (VendorPaymentMethodDetail) pxResult;
      bool flag = true;
      if (clearNewOnly)
        flag = this.PaymentDetails.Cache.GetStatus((object) paymentMethodDetail) == PXEntryStatus.Inserted;
      if (flag)
        this.PaymentDetails.Delete(paymentMethodDetail);
    }
  }

  public virtual void ClearPaymentDetails(
    PX.Objects.CR.Location location,
    string paymentTypeID,
    bool clearNewOnly)
  {
    foreach (PXResult<VendorPaymentMethodDetail> pxResult in this.PaymentDetails.Select((object) location.BAccountID, (object) location.LocationID, (object) paymentTypeID))
    {
      VendorPaymentMethodDetail paymentMethodDetail = (VendorPaymentMethodDetail) pxResult;
      bool flag = true;
      if (clearNewOnly)
        flag = this.PaymentDetails.Cache.GetStatus((object) paymentMethodDetail) == PXEntryStatus.Inserted;
      if (flag)
        this.PaymentDetails.Delete(paymentMethodDetail);
    }
  }

  protected virtual void FillPaymentDetails()
  {
    if (this.DefLocation.Current != null)
      return;
    PX.Objects.CR.Location location = (PX.Objects.CR.Location) this.DefLocation.Select();
  }

  protected virtual void FillPaymentDetails(PX.Objects.CR.Location account)
  {
    if (account == null || this.isPaymentMergedFlag)
      return;
    if (!string.IsNullOrEmpty(account.VPaymentMethodID))
    {
      List<PaymentMethodDetail> paymentMethodDetailList = new List<PaymentMethodDetail>();
      foreach (PXResult<PaymentMethodDetail> pxResult1 in this.PaymentTypeDetails.Select())
      {
        PaymentMethodDetail paymentMethodDetail1 = (PaymentMethodDetail) pxResult1;
        VendorPaymentMethodDetail paymentMethodDetail2 = (VendorPaymentMethodDetail) null;
        foreach (PXResult<VendorPaymentMethodDetail> pxResult2 in this.PaymentDetails.Select())
        {
          VendorPaymentMethodDetail paymentMethodDetail3 = (VendorPaymentMethodDetail) pxResult2;
          if (paymentMethodDetail3.DetailID == paymentMethodDetail1.DetailID)
          {
            paymentMethodDetail2 = paymentMethodDetail3;
            break;
          }
        }
        if (paymentMethodDetail2 == null)
          paymentMethodDetailList.Add(paymentMethodDetail1);
      }
      using (new ReadOnlyScope(new PXCache[1]
      {
        this.PaymentDetails.Cache
      }))
      {
        foreach (PaymentMethodDetail paymentMethodDetail in paymentMethodDetailList)
          this.PaymentDetails.Insert(new VendorPaymentMethodDetail()
          {
            BAccountID = account.BAccountID,
            LocationID = account.LocationID,
            DetailID = paymentMethodDetail.DetailID
          });
        if (paymentMethodDetailList.Count > 0)
          this.PaymentDetails.View.RequestRefresh();
      }
    }
    this.isPaymentMergedFlag = true;
  }

  public virtual DateTime? GetProbationPeriodEndDate(DateTime startDate, string startReason)
  {
    if (startReason == "NEW" || startReason == "REH")
    {
      int valueOrDefault = this.EmployeeClass.Current.ProbationPeriodMonths.GetValueOrDefault();
      if (valueOrDefault > 0)
        return new DateTime?(startDate.AddMonths(valueOrDefault).AddDays(-1.0));
    }
    return new DateTime?();
  }

  protected virtual void UpdateIfRequiredProbationPeriodEndDate(
    EPEmployeePosition employeePosition,
    PXCache cache)
  {
    if (employeePosition == null || !employeePosition.StartDate.HasValue)
      return;
    DateTime? nullable1 = employeePosition.StartDate;
    DateTime? probationPeriodEndDate = this.GetProbationPeriodEndDate(nullable1.GetValueOrDefault(), employeePosition.StartReason);
    nullable1 = employeePosition.ProbationPeriodEndDate;
    DateTime? nullable2 = probationPeriodEndDate;
    if ((nullable1.HasValue == nullable2.HasValue ? (nullable1.HasValue ? (nullable1.GetValueOrDefault() != nullable2.GetValueOrDefault() ? 1 : 0) : 0) : 1) == 0)
      return;
    cache.SetValue<EPEmployeePosition.probationPeriodEndDate>((object) employeePosition, (object) probationPeriodEndDate);
  }

  public override void Persist()
  {
    if (this.User.Current != null && this.Contact.Current != null && this.User.Cache.GetStatus((object) this.User.Current) == PXEntryStatus.Inserted)
    {
      Users copy = PXCache<Users>.CreateCopy(this.User.Current);
      copy.OldPassword = this.User.Current.Password;
      copy.NewPassword = this.User.Current.Password;
      copy.ConfirmPassword = this.User.Current.Password;
      copy.FirstName = this.Contact.Current.FirstName;
      copy.LastName = this.Contact.Current.LastName;
      copy.Email = this.Contact.Current.EMail;
      copy.IsAssigned = new bool?(true);
      this.User.Update(copy);
    }
    base.Persist();
    if (this.User.Current == null || this.User.Current.ContactID.HasValue || this.Contact.Current == null)
      return;
    this.User.Current.ContactID = this.Contact.Current.ContactID;
  }

  protected virtual void Users_State_FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    e.ReturnValue = e.ReturnValue ?? (object) "N";
  }

  protected virtual void Users_LoginTypeID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    Users row = (Users) e.Row;
    this.UserRoles.Cache.Clear();
    if (!row.LoginTypeID.HasValue)
    {
      this.User.Cache.Clear();
      this.Employee.Current.UserID = new Guid?();
    }
    else
    {
      int? source = row.Source;
      int num = 0;
      if (!(source.GetValueOrDefault() == num & source.HasValue) && !row.OverrideADRoles.GetValueOrDefault())
        return;
      foreach (PXResult<EPLoginTypeAllowsRole> pxResult in PXSelectBase<EPLoginTypeAllowsRole, PXViewOf<EPLoginTypeAllowsRole>.BasedOn<SelectFromBase<EPLoginTypeAllowsRole, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<EPLoginTypeAllowsRole.loginTypeID, Equal<P.AsInt>>>>>.And<BqlOperand<EPLoginTypeAllowsRole.isDefault, IBqlBool>.IsEqual<True>>>>.Config>.Select((PXGraph) this, (object) row.LoginTypeID))
      {
        EPLoginTypeAllowsRole loginTypeAllowsRole = (EPLoginTypeAllowsRole) pxResult;
        this.UserRoles.Cache.Insert((object) new UsersInRoles()
        {
          Rolename = loginTypeAllowsRole.Rolename
        });
      }
    }
  }

  protected virtual void Users_Username_FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    Guid? guidFromDeletedUser = Access.GetGuidFromDeletedUser((string) e.NewValue);
    if (!guidFromDeletedUser.HasValue)
      return;
    ((Users) e.Row).PKID = guidFromDeletedUser;
  }

  protected virtual void Users_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    Users row = (Users) e.Row;
    if (row == null || this.Contact.Current == null || !((Users) e.Row).LoginTypeID.HasValue)
      return;
    this.Employee.Current.UserID = row.PKID;
    if (this.Contact.Current == null)
      this.Contact.Current = (PX.Objects.CR.Contact) this.Contact.Select();
    this.Contact.Current.UserID = row.PKID;
    this.Contact.Cache.MarkUpdated((object) this.Contact.Current);
  }

  protected virtual void Users_RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    Users row = (Users) e.Row;
    if (!this.Employee.Current.UserID.HasValue)
    {
      this.Employee.Current.UserID = row.PKID;
      if (this.Contact.Current == null)
        this.Contact.Current = (PX.Objects.CR.Contact) this.Contact.Select();
      this.Contact.Current.UserID = row.PKID;
      this.Contact.Cache.MarkUpdated((object) this.Contact.Current);
    }
    else
    {
      this.User.Cache.Clear();
      this.UserRoles.Cache.Clear();
    }
    EPLoginType epLoginType = (EPLoginType) PXSelectBase<EPLoginType, PXSelect<EPLoginType, Where<EPLoginType.loginTypeID, Equal<Current<Users.loginTypeID>>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
    {
      (object) row
    });
    row.Username = this.Contact.Current == null || epLoginType == null || !epLoginType.EmailAsLogin.GetValueOrDefault() ? (string) null : this.Contact.Current.EMail;
  }

  protected virtual void EPRule_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    EPRule row = (EPRule) e.Row;
    if (row == null)
      return;
    EPRule epRule = (EPRule) PXSelectBase<EPRule, PXSelect<EPRule, Where<EPRule.ruleID, Equal<Required<EPRule.stepID>>, And<EPRule.stepID, IsNull>>>.Config>.Select((PXGraph) this, (object) row.StepID);
    if (epRule == null)
      return;
    row.StepName = epRule.Name;
  }

  /// <exclude />
  public class EmployeeMaintAddressLookupExtension : 
    AddressLookupExtension<EmployeeMaint, EPEmployee, PX.Objects.CR.Address>
  {
    protected override string AddressView => "Address";
  }

  public class EmployeeMaint_ActivityDetailsExt_Actions : 
    ActivityDetailsExt_Actions<EmployeeMaint.EmployeeMaint_ActivityDetailsExt, EmployeeMaint, EPEmployee, EPEmployee.noteID>
  {
  }

  public class EmployeeMaint_ActivityDetailsExt : 
    ActivityDetailsExt<EmployeeMaint, EPEmployee, EPEmployee.noteID>
  {
    public override System.Type GetBAccountIDCommand() => typeof (EPEmployee.bAccountID);
  }

  public class EmployeeMaint_CRDuplicateBAccountIdentifier : 
    CRDuplicateBAccountIdentifier<EmployeeMaint, EPEmployee>
  {
  }
}
