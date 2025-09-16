// Decompiled with JetBrains decompiler
// Type: PX.Objects.DR.DRDraftScheduleProc
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.Common;
using PX.Objects.Common.Extensions;
using PX.Objects.CR;
using PX.Objects.GL;
using PX.Objects.IN;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

#nullable enable
namespace PX.Objects.DR;

public class DRDraftScheduleProc : PXGraph<
#nullable disable
DRRecognition>
{
  public PXCancel<DRDraftScheduleProc.SchedulesFilter> Cancel;
  public PXFilter<DRDraftScheduleProc.SchedulesFilter> Filter;
  public PXAction<DRDraftScheduleProc.SchedulesFilter> viewSchedule;
  public PXSetup<DRSetup> Setup;
  [PXFilterable(new System.Type[] {})]
  public PXFilteredProcessing<DRScheduleDetail, DRDraftScheduleProc.SchedulesFilter> Items;
  public PXSelectJoin<DRScheduleDetail, InnerJoin<DRSchedule, On<DRSchedule.scheduleID, Equal<DRScheduleDetail.scheduleID>>, InnerJoin<DRDeferredCode, On<DRDeferredCode.deferredCodeID, Equal<DRScheduleDetail.defCode>>>>, Where<DRDeferredCode.accountType, Equal<Current<DRDraftScheduleProc.SchedulesFilter.accountType>>, And<DRScheduleDetail.status, Equal<DRScheduleStatus.DraftStatus>, And<DRSchedule.isCustom, Equal<True>>>>, OrderBy<Asc<DRScheduleDetail.scheduleID, Asc<DRScheduleDetail.componentID, Asc<DRScheduleDetail.detailLineNbr>>>>> ItemsView;
  public PXAction<DRDraftScheduleProc.SchedulesFilter> viewDocument;

  public virtual IEnumerable items()
  {
    DRDraftScheduleProc.SchedulesFilter current = ((PXSelectBase<DRDraftScheduleProc.SchedulesFilter>) this.Filter).Current;
    ((PXSelectBase<DRScheduleDetail>) this.ItemsView).WhereNew<Where<DRDeferredCode.accountType, Equal<Current<DRDraftScheduleProc.SchedulesFilter.accountType>>, And<DRScheduleDetail.status, Equal<DRScheduleStatus.DraftStatus>, And<DRSchedule.isCustom, Equal<True>>>>>();
    if (current == null)
      return (IEnumerable) null;
    BqlCommand command = ((PXSelectBase) this.ItemsView).View.BqlSelect;
    if (!string.IsNullOrEmpty(current.DeferredCode))
      command = command.WhereAnd<Where<DRScheduleDetail.defCode, Equal<Current<DRDraftScheduleProc.SchedulesFilter.deferredCode>>>>();
    int? nullable = current.BranchID;
    if (nullable.HasValue)
      command = command.WhereAnd<Where<DRScheduleDetail.branchID, Equal<Current<DRDraftScheduleProc.SchedulesFilter.branchID>>>>();
    nullable = current.AccountID;
    if (nullable.HasValue)
      command = command.WhereAnd<Where<DRScheduleDetail.defAcctID, Equal<Current<DRDraftScheduleProc.SchedulesFilter.accountID>>>>();
    nullable = current.SubID;
    if (nullable.HasValue)
      command = command.WhereAnd<Where<DRScheduleDetail.defSubID, Equal<Current<DRDraftScheduleProc.SchedulesFilter.subID>>>>();
    nullable = current.BAccountID;
    if (nullable.HasValue)
      command = command.WhereAnd<Where<DRScheduleDetail.bAccountID, Equal<Current<DRDraftScheduleProc.SchedulesFilter.bAccountID>>>>();
    nullable = current.ComponentID;
    if (nullable.HasValue)
      command = command.WhereAnd<Where<DRScheduleDetail.componentID, Equal<Current<DRDraftScheduleProc.SchedulesFilter.componentID>>>>();
    return command.CreateView((PXGraph) this, mergeCache: !((PXSelectBase) this.ItemsView).View.IsReadOnly).SelectExternalWithPaging();
  }

  public DRDraftScheduleProc()
  {
    DRSetup current = ((PXSelectBase<DRSetup>) this.Setup).Current;
    ((PXProcessingBase<DRScheduleDetail>) this.Items).SetSelected<DRScheduleDetail.selected>();
  }

  [PXUIField(DisplayName = "", Visible = false)]
  [PXEditDetailButton]
  public virtual IEnumerable ViewSchedule(PXAdapter adapter)
  {
    if (((PXSelectBase<DRScheduleDetail>) this.Items).Current != null)
      DRRedirectHelper.NavigateToDeferralSchedule((PXGraph) this, ((PXSelectBase<DRScheduleDetail>) this.Items).Current.ScheduleID);
    return adapter.Get();
  }

  [PXUIField(DisplayName = "")]
  public virtual IEnumerable ViewDocument(PXAdapter adapter)
  {
    if (((PXSelectBase<DRScheduleDetail>) this.Items).Current != null)
      DRRedirectHelper.NavigateToOriginalDocument((PXGraph) this, ((PXSelectBase<DRScheduleDetail>) this.Items).Current);
    return adapter.Get();
  }

  [PXMergeAttributes]
  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "Visible", false)]
  protected virtual void DRScheduleDetail_DefAmt_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "Visible", false)]
  protected virtual void DRScheduleDetail_LineNbr_CacheAttached(PXCache sender)
  {
  }

  protected virtual void SchedulesFilter_AccountType_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is DRDraftScheduleProc.SchedulesFilter row))
      return;
    row.BAccountID = new int?();
    row.DeferredCode = (string) null;
    row.AccountID = new int?();
    row.SubID = new int?();
  }

  protected virtual void SchedulesFilter_RowUpdated(PXCache cache, PXRowUpdatedEventArgs e)
  {
    ((PXSelectBase) this.Items).Cache.Clear();
  }

  protected virtual void SchedulesFilter_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    // ISSUE: method pointer
    ((PXProcessingBase<DRScheduleDetail>) this.Items).SetProcessDelegate(new PXProcessingBase<DRScheduleDetail>.ProcessItemDelegate((object) null, __methodptr(ReleaseCustomSchedule)));
  }

  protected virtual void DRScheduleDetail_ComponentID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void DRScheduleDetail_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is DRScheduleDetail row))
      return;
    row.DocumentType = DRScheduleDocumentType.BuildDocumentType(row.Module, row.DocType);
  }

  public static void ReleaseCustomSchedule(DRScheduleDetail item)
  {
    ScheduleMaint instance = PXGraph.CreateInstance<ScheduleMaint>();
    IEnumerable<DRScheduleDetail> records = GraphHelper.RowCast<DRScheduleDetail>((IEnumerable) PXSelectBase<DRScheduleDetail, PXSelect<DRScheduleDetail, Where<DRScheduleDetail.scheduleID, Equal<Required<DRSchedule.scheduleID>>, And<DRScheduleDetail.isResidual, Equal<False>>>>.Config>.Select((PXGraph) instance, new object[1]
    {
      (object) item.ScheduleID
    }));
    instance.FinPeriodUtils.ValidateFinPeriod<DRScheduleDetail>(records, (Func<DRScheduleDetail, string>) (m => item.FinPeriodID), (Func<DRScheduleDetail, int?[]>) (m => m.BranchID.SingleToArray<int?>()));
    ((PXGraph) instance).Clear();
    ((PXSelectBase<DRScheduleDetail>) instance.Document).Current = PXResultset<DRScheduleDetail>.op_Implicit(PXSelectBase<DRScheduleDetail, PXSelect<DRScheduleDetail, Where<DRScheduleDetail.scheduleID, Equal<Required<DRScheduleDetail.scheduleID>>, And<DRScheduleDetail.componentID, Equal<Required<DRScheduleDetail.componentID>>, And<DRScheduleDetail.detailLineNbr, Equal<Required<DRScheduleDetail.detailLineNbr>>>>>>.Config>.Select((PXGraph) instance, new object[3]
    {
      (object) item.ScheduleID,
      (object) item.ComponentID,
      (object) item.DetailLineNbr
    }));
    instance.ReleaseCustomScheduleDetail();
  }

  [Serializable]
  public class SchedulesFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected string _AccountType;
    protected int? _AccountID;
    protected int? _SubID;
    protected string _DeferredCode;
    protected string _BAccountType;
    protected int? _ComponentID;

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

    [Account]
    public virtual int? AccountID
    {
      get => this._AccountID;
      set => this._AccountID = value;
    }

    [SubAccount(typeof (DRDraftScheduleProc.SchedulesFilter.accountID))]
    public virtual int? SubID
    {
      get => this._SubID;
      set => this._SubID = value;
    }

    [PXDBString(10, InputMask = ">aaaaaaaaaa")]
    [PXUIField(DisplayName = "Deferral Code")]
    [PXSelector(typeof (Search<DRDeferredCode.deferredCodeID, Where<DRDeferredCode.accountType, Equal<Current<DRDraftScheduleProc.SchedulesFilter.accountType>>>>))]
    [PXRestrictor(typeof (Where<DRDeferredCode.active, Equal<True>>), "The {0} deferral code is deactivated on the Deferral Codes (DR202000) form.", new System.Type[] {typeof (DRDeferredCode.deferredCodeID)})]
    public virtual string DeferredCode
    {
      get => this._DeferredCode;
      set => this._DeferredCode = value;
    }

    [Branch(null, null, true, true, true)]
    public virtual int? BranchID { get; set; }

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
    [PXSelector(typeof (Search<BAccountR.bAccountID, Where<BAccountR.type, Equal<Current<DRDraftScheduleProc.SchedulesFilter.bAccountType>>>>), new System.Type[] {typeof (BAccountR.acctCD), typeof (BAccountR.acctName), typeof (BAccountR.type)}, SubstituteKey = typeof (BAccountR.acctCD))]
    public virtual int? BAccountID { get; set; }

    [Inventory(DisplayName = "Component")]
    public virtual int? ComponentID
    {
      get => this._ComponentID;
      set => this._ComponentID = value;
    }

    public abstract class accountType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      DRDraftScheduleProc.SchedulesFilter.accountType>
    {
    }

    public abstract class accountID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      DRDraftScheduleProc.SchedulesFilter.accountID>
    {
    }

    public abstract class subID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      DRDraftScheduleProc.SchedulesFilter.subID>
    {
    }

    public abstract class deferredCode : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      DRDraftScheduleProc.SchedulesFilter.deferredCode>
    {
    }

    public abstract class branchID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      DRDraftScheduleProc.SchedulesFilter.branchID>
    {
    }

    public abstract class bAccountType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      DRDraftScheduleProc.SchedulesFilter.bAccountType>
    {
    }

    public abstract class bAccountID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      DRDraftScheduleProc.SchedulesFilter.bAccountID>
    {
    }

    public abstract class componentID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      DRDraftScheduleProc.SchedulesFilter.componentID>
    {
    }
  }
}
