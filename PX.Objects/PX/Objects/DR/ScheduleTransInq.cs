// Decompiled with JetBrains decompiler
// Type: PX.Objects.DR.ScheduleTransInq
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.Common;
using PX.Objects.CR;
using PX.Objects.GL;
using PX.Objects.GL.Attributes;
using PX.Objects.GL.FinPeriods;
using PX.Objects.GL.FinPeriods.TableDefinition;
using System;
using System.Collections;

#nullable enable
namespace PX.Objects.DR;

[TableAndChartDashboardType]
public class ScheduleTransInq : PXGraph<
#nullable disable
ScheduleTransInq>
{
  public PXCancel<ScheduleTransInq.ScheduleTransFilter> Cancel;
  public PXFilter<ScheduleTransInq.ScheduleTransFilter> Filter;
  [PXFilterable(new System.Type[] {})]
  public PXSelectJoin<DRScheduleTran, InnerJoin<DRSchedule, On<DRScheduleTran.scheduleID, Equal<DRSchedule.scheduleID>>, InnerJoin<DRScheduleDetail, On<DRScheduleTran.scheduleID, Equal<DRScheduleDetail.scheduleID>, And<DRScheduleTran.componentID, Equal<DRScheduleDetail.componentID>, And<DRScheduleTran.detailLineNbr, Equal<DRScheduleDetail.detailLineNbr>>>>, InnerJoin<DRDeferredCode, On<DRDeferredCode.deferredCodeID, Equal<DRScheduleDetail.defCode>>, LeftJoin<PX.Objects.IN.InventoryItem, On<PX.Objects.IN.InventoryItem.inventoryID, Equal<DRScheduleDetail.componentID>>>>>>, Where<DRDeferredCode.accountType, Equal<Current<ScheduleTransInq.ScheduleTransFilter.accountType>>, And<DRScheduleTran.status, Equal<DRScheduleTranStatus.PostedStatus>, And<DRScheduleTran.finPeriodID, Equal<Current<ScheduleTransInq.ScheduleTransFilter.finPeriodID>>>>>> Records;
  public PXAction<ScheduleTransInq.ScheduleTransFilter> previousPeriod;
  public PXAction<ScheduleTransInq.ScheduleTransFilter> nextPeriod;
  public PXSetup<DRSetup> Setup;
  public PXAction<ScheduleTransInq.ScheduleTransFilter> viewDoc;
  public PXAction<ScheduleTransInq.ScheduleTransFilter> viewBatch;

  public virtual IEnumerable records()
  {
    ScheduleTransInq scheduleTransInq = this;
    ScheduleTransInq.ScheduleTransFilter current = ((PXSelectBase<ScheduleTransInq.ScheduleTransFilter>) scheduleTransInq.Filter).Current;
    if (current != null)
    {
      PXSelectBase<DRScheduleTran> pxSelectBase = (PXSelectBase<DRScheduleTran>) new PXSelectJoin<DRScheduleTran, InnerJoin<DRSchedule, On<DRScheduleTran.scheduleID, Equal<DRSchedule.scheduleID>>, InnerJoin<DRScheduleDetail, On<DRScheduleTran.scheduleID, Equal<DRScheduleDetail.scheduleID>, And<DRScheduleTran.componentID, Equal<DRScheduleDetail.componentID>, And<DRScheduleTran.detailLineNbr, Equal<DRScheduleDetail.detailLineNbr>>>>, InnerJoin<DRDeferredCode, On<DRDeferredCode.deferredCodeID, Equal<DRScheduleDetail.defCode>>, LeftJoin<PX.Objects.IN.InventoryItem, On<PX.Objects.IN.InventoryItem.inventoryID, Equal<DRScheduleDetail.componentID>>>>>>, Where<DRDeferredCode.accountType, Equal<Current<ScheduleTransInq.ScheduleTransFilter.accountType>>, And<DRScheduleTran.status, Equal<DRScheduleTranStatus.PostedStatus>>>>((PXGraph) scheduleTransInq);
      if (!string.IsNullOrEmpty(current.DeferredCode))
        pxSelectBase.WhereAnd<Where<DRScheduleDetail.defCode, Equal<Current<ScheduleTransInq.ScheduleTransFilter.deferredCode>>>>();
      if (current.UseMasterCalendar.GetValueOrDefault())
        pxSelectBase.WhereAnd<Where<DRScheduleTran.tranPeriodID, Equal<Current<ScheduleTransInq.ScheduleTransFilter.finPeriodID>>>>();
      else
        pxSelectBase.WhereAnd<Where<DRScheduleTran.finPeriodID, Equal<Current<ScheduleTransInq.ScheduleTransFilter.finPeriodID>>>>();
      if (current.OrgBAccountID.HasValue)
        pxSelectBase.WhereAnd<Where<DRScheduleTran.branchID, Inside<Current<ScheduleTransInq.ScheduleTransFilter.orgBAccountID>>>>();
      if (current.AccountID.HasValue)
        pxSelectBase.WhereAnd<Where<DRScheduleTran.accountID, Equal<Current<ScheduleTransInq.ScheduleTransFilter.accountID>>>>();
      if (current.SubID.HasValue)
        pxSelectBase.WhereAnd<Where<DRScheduleTran.subID, Equal<Current<ScheduleTransInq.ScheduleTransFilter.subID>>>>();
      if (current.BAccountID.HasValue)
        pxSelectBase.WhereAnd<Where<DRScheduleDetail.bAccountID, Equal<Current<ScheduleTransInq.ScheduleTransFilter.bAccountID>>>>();
      foreach (object obj in GraphHelper.QuickSelect((PXGraph) scheduleTransInq, ((PXSelectBase) pxSelectBase).View.BqlSelect))
        yield return obj;
    }
  }

  [InjectDependency]
  public IFinPeriodRepository FinPeriodRepository { get; set; }

  public ScheduleTransInq()
  {
    DRSetup current = ((PXSelectBase<DRSetup>) this.Setup).Current;
  }

  [PXUIField(DisplayName = "")]
  [PXButton]
  public virtual IEnumerable ViewDoc(PXAdapter adapter)
  {
    if (((PXSelectBase<DRScheduleTran>) this.Records).Current != null)
      DRRedirectHelper.NavigateToOriginalDocument((PXGraph) this, ((PXSelectBase<DRScheduleTran>) this.Records).Current);
    return adapter.Get();
  }

  [PXUIField(DisplayName = "")]
  [PXButton]
  public virtual IEnumerable ViewBatch(PXAdapter adapter)
  {
    JournalEntry instance = PXGraph.CreateInstance<JournalEntry>();
    ((PXGraph) instance).Clear();
    Batch batch = PXResultset<Batch>.op_Implicit(PXSelectBase<Batch, PXSelect<Batch, Where<Batch.module, Equal<BatchModule.moduleDR>, And<Batch.batchNbr, Equal<Current<DRScheduleTran.batchNbr>>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()));
    if (batch != null)
    {
      ((PXSelectBase<Batch>) instance.BatchModule).Current = batch;
      PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, true, nameof (ViewBatch));
      ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
      throw requiredException;
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXPreviousButton]
  public virtual IEnumerable PreviousPeriod(PXAdapter adapter)
  {
    ScheduleTransInq.ScheduleTransFilter current = ((PXSelectBase<ScheduleTransInq.ScheduleTransFilter>) this.Filter).Current;
    FinPeriod prevPeriod = this.FinPeriodRepository.FindPrevPeriod(this.FinPeriodRepository.GetCalendarOrganizationID(current.OrganizationID, current.BranchID, current.UseMasterCalendar), current.FinPeriodID, true);
    current.FinPeriodID = prevPeriod?.FinPeriodID;
    return adapter.Get();
  }

  [PXUIField]
  [PXNextButton]
  public virtual IEnumerable NextPeriod(PXAdapter adapter)
  {
    ScheduleTransInq.ScheduleTransFilter current = ((PXSelectBase<ScheduleTransInq.ScheduleTransFilter>) this.Filter).Current;
    FinPeriod nextPeriod = this.FinPeriodRepository.FindNextPeriod(this.FinPeriodRepository.GetCalendarOrganizationID(current.OrganizationID, current.BranchID, current.UseMasterCalendar), current.FinPeriodID, true);
    current.FinPeriodID = nextPeriod?.FinPeriodID;
    return adapter.Get();
  }

  [PXCustomizeBaseAttribute]
  protected virtual void DRScheduleTran_RecDate_CacheAttached(PXCache sender)
  {
  }

  [PXCustomizeBaseAttribute]
  protected virtual void DRScheduleTran_AccountID_CacheAttached(PXCache sender)
  {
  }

  [PXCustomizeBaseAttribute]
  protected virtual void DRScheduleTran_SubID_CacheAttached(PXCache sender)
  {
  }

  protected virtual void ScheduleTransFilter_AccountType_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is ScheduleTransInq.ScheduleTransFilter row))
      return;
    row.BAccountID = new int?();
    row.DeferredCode = (string) null;
    row.AccountID = new int?();
    row.SubID = new int?();
  }

  [Serializable]
  public class ScheduleTransFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected string _AccountType;
    protected string _FinPeriodID;
    protected string _DeferredCode;
    protected int? _AccountID;
    protected int? _SubID;
    protected string _BAccountType;

    [Organization(false, Required = false)]
    public int? OrganizationID { get; set; }

    [BranchOfOrganization(typeof (ScheduleTransInq.ScheduleTransFilter.organizationID), false, null, null)]
    public virtual int? BranchID { get; set; }

    [OrganizationTree(typeof (ScheduleTransInq.ScheduleTransFilter.organizationID), typeof (ScheduleTransInq.ScheduleTransFilter.branchID), null, false)]
    public int? OrgBAccountID { get; set; }

    [PXDBString(1)]
    [PXDefault("I")]
    [LabelList(typeof (DeferredAccountType))]
    [PXUIField]
    public virtual string AccountType
    {
      get => this._AccountType;
      set
      {
        this._AccountType = value;
        if (value == "E")
          this._BAccountType = "VE";
        else
          this._BAccountType = "CU";
      }
    }

    [PXDefault]
    [FinPeriodSelector(null, null, typeof (ScheduleTransInq.ScheduleTransFilter.branchID), null, typeof (ScheduleTransInq.ScheduleTransFilter.organizationID), typeof (ScheduleTransInq.ScheduleTransFilter.useMasterCalendar), null, false, false, false, false, true, FinPeriodSelectorAttribute.SelectionModesWithRestrictions.Undefined, null, null, null, true)]
    [PXUIField(DisplayName = "Fin. Period")]
    public virtual string FinPeriodID
    {
      get => this._FinPeriodID;
      set => this._FinPeriodID = value;
    }

    [PXDBString(10, IsUnicode = true, InputMask = ">aaaaaaaaaa")]
    [PXUIField(DisplayName = "Deferral Code")]
    [PXSelector(typeof (Search<DRDeferredCode.deferredCodeID, Where<DRDeferredCode.accountType, Equal<Current<ScheduleTransInq.ScheduleTransFilter.accountType>>>>), new System.Type[] {typeof (DRDeferredCode.deferredCodeID), typeof (DRDeferredCode.description), typeof (DRDeferredCode.accountType), typeof (DRDeferredCode.accountID), typeof (DRDeferredCode.subID), typeof (DRDeferredCode.method), typeof (DRDeferredCode.active)})]
    public virtual string DeferredCode
    {
      get => this._DeferredCode;
      set => this._DeferredCode = value;
    }

    [Account]
    public virtual int? AccountID
    {
      get => this._AccountID;
      set => this._AccountID = value;
    }

    [SubAccount(typeof (ScheduleTransInq.ScheduleTransFilter.accountID))]
    public virtual int? SubID
    {
      get => this._SubID;
      set => this._SubID = value;
    }

    [PXDefault("CU")]
    [PXString(2, IsFixed = true)]
    [PXStringList(new string[] {"VE", "CU"}, new string[] {"Vendor", "Customer"})]
    public virtual string BAccountType
    {
      get => this._BAccountType;
      set => this._BAccountType = value;
    }

    [PXDBInt]
    [PXUIField(DisplayName = "Business Account")]
    [PXSelector(typeof (Search<BAccountR.bAccountID, Where<BAccountR.type, Equal<Current<ScheduleTransInq.ScheduleTransFilter.bAccountType>>, Or<BAccountR.type, Equal<BAccountType.combinedType>>>>), new System.Type[] {typeof (BAccountR.acctCD), typeof (BAccountR.acctName), typeof (BAccountR.type)}, SubstituteKey = typeof (BAccountR.acctCD))]
    public virtual int? BAccountID { get; set; }

    [PXDBBool]
    [PXUIField(DisplayName = "Use Master Calendar", FieldClass = "MultipleCalendarsSupport")]
    public bool? UseMasterCalendar { get; set; }

    public abstract class organizationID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ScheduleTransInq.ScheduleTransFilter.organizationID>
    {
    }

    public abstract class branchID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ScheduleTransInq.ScheduleTransFilter.branchID>
    {
    }

    public abstract class orgBAccountID : IBqlField, IBqlOperand
    {
    }

    public abstract class accountType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ScheduleTransInq.ScheduleTransFilter.accountType>
    {
    }

    public abstract class finPeriodID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ScheduleTransInq.ScheduleTransFilter.finPeriodID>
    {
    }

    public abstract class deferredCode : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ScheduleTransInq.ScheduleTransFilter.deferredCode>
    {
    }

    public abstract class accountID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ScheduleTransInq.ScheduleTransFilter.accountID>
    {
    }

    public abstract class subID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ScheduleTransInq.ScheduleTransFilter.subID>
    {
    }

    public abstract class bAccountType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ScheduleTransInq.ScheduleTransFilter.bAccountType>
    {
    }

    public abstract class bAccountID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ScheduleTransInq.ScheduleTransFilter.bAccountID>
    {
    }

    public abstract class useMasterCalendar : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      ScheduleTransInq.ScheduleTransFilter.useMasterCalendar>
    {
    }
  }
}
