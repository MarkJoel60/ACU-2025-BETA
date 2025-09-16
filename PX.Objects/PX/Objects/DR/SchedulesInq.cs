// Decompiled with JetBrains decompiler
// Type: PX.Objects.DR.SchedulesInq
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.CM;
using PX.Objects.Common;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.GL.Attributes;
using PX.Objects.IN;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable enable
namespace PX.Objects.DR;

[TableAndChartDashboardType]
[Serializable]
public class SchedulesInq : PXGraph<
#nullable disable
SchedulesInq>
{
  public PXCancel<SchedulesInq.SchedulesFilter> Cancel;
  public PXFilter<SchedulesInq.SchedulesFilter> Filter;
  [PXFilterable(new System.Type[] {})]
  public PXSelectJoin<SchedulesInq.SchedulesInqResult, InnerJoin<DRSchedule, On<DRSchedule.scheduleID, Equal<SchedulesInq.SchedulesInqResult.scheduleID>>, InnerJoin<DRDeferredCode, On<DRDeferredCode.deferredCodeID, Equal<DRScheduleDetail.defCode>>, LeftJoin<PX.Objects.IN.InventoryItem, On<PX.Objects.IN.InventoryItem.inventoryID, Equal<SchedulesInq.SchedulesInqResult.componentID>>>>>, Where<DRDeferredCode.accountType, Equal<Current<SchedulesInq.SchedulesFilter.accountType>>>> Records;
  public PXSetup<DRSetup> Setup;
  /// <summary>
  /// Explicitly instantiate the business account cache to
  /// rename the <see cref="T:PX.Objects.CR.BAccountR.acctName" /> column in the
  /// <see cref="M:PX.Objects.DR.SchedulesInq.BAccountR_AcctName_CacheAttached(PX.Data.PXCache)" /> handler.
  /// </summary>
  public PXSelect<BAccountR> DummyBusinessAccount;
  public PXAction<SchedulesInq.SchedulesFilter> viewDocument;

  protected virtual int[] FilteringBranchIDs
  {
    get
    {
      int[] filteringBranchIds = (int[]) null;
      if (((PXSelectBase<SchedulesInq.SchedulesFilter>) this.Filter).Current.BranchID.HasValue)
        filteringBranchIds = new int[1]
        {
          ((PXSelectBase<SchedulesInq.SchedulesFilter>) this.Filter).Current.BranchID.Value
        };
      else if (((PXSelectBase<SchedulesInq.SchedulesFilter>) this.Filter).Current.OrganizationID.HasValue)
        filteringBranchIds = PXAccess.GetChildBranchIDs(((PXSelectBase<SchedulesInq.SchedulesFilter>) this.Filter).Current.OrganizationID, true);
      return filteringBranchIds;
    }
  }

  protected virtual IEnumerable filter()
  {
    SchedulesInq schedulesInq = this;
    PXCache cache = ((PXGraph) schedulesInq).Caches[typeof (SchedulesInq.SchedulesFilter)];
    if (cache != null)
    {
      if (cache.Current is SchedulesInq.SchedulesFilter filter)
      {
        int startRow = 0;
        int totalRows = 0;
        filter.TotalDeferred = new Decimal?(0M);
        filter.TotalScheduled = new Decimal?(0M);
        BqlCommand cmd = schedulesInq.ComposeBQLCommandForRecords(filter);
        if (cmd == null)
          yield return cache.Current;
        cmd = cmd.AggregateNew<Aggregate<Sum<SchedulesInq.SchedulesInqResult.signTotalAmt, Sum<SchedulesInq.SchedulesInqResult.signDefAmt>>>>();
        List<object> objectList = new PXView((PXGraph) schedulesInq, true, cmd).Select((object[]) new SchedulesInq.SchedulesFilter[1]
        {
          filter
        }, (object[]) null, (object[]) null, (string[]) null, (bool[]) null, ((PXSelectBase) schedulesInq.Records).View.GetExternalFilters(), ref startRow, 0, ref totalRows);
        if (objectList.Count > 0)
        {
          PXResult<SchedulesInq.SchedulesInqResult, DRSchedule, DRDeferredCode, PX.Objects.IN.InventoryItem> pxResult = (PXResult<SchedulesInq.SchedulesInqResult, DRSchedule, DRDeferredCode, PX.Objects.IN.InventoryItem>) objectList[0];
          filter.TotalScheduled = PXResult<SchedulesInq.SchedulesInqResult, DRSchedule, DRDeferredCode, PX.Objects.IN.InventoryItem>.op_Implicit(pxResult).SignTotalAmt;
          filter.TotalDeferred = PXResult<SchedulesInq.SchedulesInqResult, DRSchedule, DRDeferredCode, PX.Objects.IN.InventoryItem>.op_Implicit(pxResult).SignDefAmt;
        }
        cmd = (BqlCommand) null;
      }
      filter = (SchedulesInq.SchedulesFilter) null;
    }
    yield return cache.Current;
    cache.IsDirty = false;
  }

  public virtual IEnumerable records()
  {
    PXDelegateResult pxDelegateResult = new PXDelegateResult()
    {
      IsResultSorted = true,
      IsResultTruncated = true,
      IsResultFiltered = true
    };
    int startRow = PXView.StartRow;
    int num = 0;
    BqlCommand bqlCommand = this.ComposeBQLCommandForRecords(((PXSelectBase<SchedulesInq.SchedulesFilter>) this.Filter).Current);
    if (bqlCommand == null)
      return (IEnumerable) pxDelegateResult;
    foreach (PXResult<SchedulesInq.SchedulesInqResult, DRSchedule, DRDeferredCode, PX.Objects.IN.InventoryItem> pxResult in new PXView((PXGraph) this, true, bqlCommand).Select(PXView.Currents, (object[]) null, PXView.Searches, PXView.SortColumns, PXView.Descendings, PXView.PXFilterRowCollection.op_Implicit(PXView.Filters), ref startRow, PXView.MaximumRows, ref num))
    {
      SchedulesInq.SchedulesInqResult schedulesInqResult = PXResult<SchedulesInq.SchedulesInqResult, DRSchedule, DRDeferredCode, PX.Objects.IN.InventoryItem>.op_Implicit(pxResult);
      PX.Objects.IN.InventoryItem inventoryItem = PXResult<SchedulesInq.SchedulesInqResult, DRSchedule, DRDeferredCode, PX.Objects.IN.InventoryItem>.op_Implicit(pxResult);
      schedulesInqResult.ComponentCD = inventoryItem.InventoryCD;
      schedulesInqResult.DocumentType = DRScheduleDocumentType.BuildDocumentType(schedulesInqResult.Module, schedulesInqResult.DocType);
      ((List<object>) pxDelegateResult).Add((object) pxResult);
    }
    PXView.StartRow = 0;
    return (IEnumerable) pxDelegateResult;
  }

  public virtual BqlCommand ComposeBQLCommandForRecords(SchedulesInq.SchedulesFilter filter)
  {
    if (filter == null)
      return (BqlCommand) null;
    PXSelectBase<SchedulesInq.SchedulesInqResult> pxSelectBase = (PXSelectBase<SchedulesInq.SchedulesInqResult>) new PXSelectJoin<SchedulesInq.SchedulesInqResult, InnerJoin<DRSchedule, On<DRSchedule.scheduleID, Equal<SchedulesInq.SchedulesInqResult.scheduleID>>, InnerJoin<DRDeferredCode, On<DRDeferredCode.deferredCodeID, Equal<DRScheduleDetail.defCode>>, LeftJoin<PX.Objects.IN.InventoryItem, On<PX.Objects.IN.InventoryItem.inventoryID, Equal<SchedulesInq.SchedulesInqResult.componentID>>>>>, Where<DRDeferredCode.accountType, Equal<Current<SchedulesInq.SchedulesFilter.accountType>>>>((PXGraph) this);
    if (!string.IsNullOrEmpty(filter.DeferredCode))
      pxSelectBase.WhereAnd<Where<DRScheduleDetail.defCode, Equal<Current<SchedulesInq.SchedulesFilter.deferredCode>>>>();
    int? nullable = filter.OrgBAccountID;
    if (nullable.HasValue || PXAccess.FeatureInstalled<FeaturesSet.multipleBaseCurrencies>())
      pxSelectBase.WhereAnd<Where<DRScheduleDetail.branchID, Inside<Current2<SchedulesInq.SchedulesFilter.orgBAccountID>>>>();
    nullable = filter.AccountID;
    if (nullable.HasValue)
      pxSelectBase.WhereAnd<Where<DRScheduleDetail.defAcctID, Equal<Current<SchedulesInq.SchedulesFilter.accountID>>>>();
    nullable = filter.SubID;
    if (nullable.HasValue)
      pxSelectBase.WhereAnd<Where<DRScheduleDetail.defSubID, Equal<Current<SchedulesInq.SchedulesFilter.subID>>>>();
    nullable = filter.BAccountID;
    if (nullable.HasValue)
      pxSelectBase.WhereAnd<Where<DRScheduleDetail.bAccountID, Equal<Current<SchedulesInq.SchedulesFilter.bAccountID>>>>();
    nullable = filter.ComponentID;
    if (nullable.HasValue)
      pxSelectBase.WhereAnd<Where<SchedulesInq.SchedulesInqResult.componentID, Equal<Current<SchedulesInq.SchedulesFilter.componentID>>>>();
    return ((PXSelectBase) pxSelectBase).View.BqlSelect;
  }

  [PXUIField(DisplayName = "")]
  public virtual IEnumerable ViewDocument(PXAdapter adapter)
  {
    if (((PXSelectBase<SchedulesInq.SchedulesInqResult>) this.Records).Current != null)
      DRRedirectHelper.NavigateToOriginalDocument((PXGraph) this, (DRScheduleDetail) ((PXSelectBase<SchedulesInq.SchedulesInqResult>) this.Records).Current);
    return adapter.Get();
  }

  public SchedulesInq()
  {
    DRSetup current = ((PXSelectBase<DRSetup>) this.Setup).Current;
  }

  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "DisplayName", "Business Account Name")]
  protected virtual void BAccountR_AcctName_CacheAttached(PXCache sender)
  {
  }

  [PXCustomizeBaseAttribute]
  protected virtual void SchedulesInqResult_BranchID_CacheAttached(PXCache sender)
  {
  }

  [PXCustomizeBaseAttribute]
  protected virtual void SchedulesInqResult_DefCode_CacheAttached(PXCache sender)
  {
  }

  [PXCustomizeBaseAttribute]
  protected virtual void SchedulesInqResult_DefAcctID_CacheAttached(PXCache sender)
  {
  }

  [PXCustomizeBaseAttribute]
  protected virtual void SchedulesInqResult_DefSubID_CacheAttached(PXCache sender)
  {
  }

  [PXCustomizeBaseAttribute]
  protected virtual void SchedulesInqResult_AccountID_CacheAttached(PXCache sender)
  {
  }

  [PXCustomizeBaseAttribute]
  protected virtual void SchedulesInqResult_SubID_CacheAttached(PXCache sender)
  {
  }

  protected virtual void SchedulesFilter_AccountType_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is SchedulesInq.SchedulesFilter row))
      return;
    row.BAccountID = new int?();
    row.DeferredCode = (string) null;
    row.AccountID = new int?();
    row.SubID = new int?();
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

    [Organization(false, Required = false)]
    public virtual int? OrganizationID { get; set; }

    [BranchOfOrganization(typeof (SchedulesInq.SchedulesFilter.organizationID), false, null, null)]
    public virtual int? BranchID { get; set; }

    [OrganizationTree(typeof (SchedulesInq.SchedulesFilter.organizationID), typeof (SchedulesInq.SchedulesFilter.branchID), null, false)]
    [PXUIRequired(typeof (FeatureInstalled<FeaturesSet.multipleBaseCurrencies>))]
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

    [Account]
    public virtual int? AccountID
    {
      get => this._AccountID;
      set => this._AccountID = value;
    }

    [SubAccount(typeof (SchedulesInq.SchedulesFilter.accountID))]
    public virtual int? SubID
    {
      get => this._SubID;
      set => this._SubID = value;
    }

    [PXDBString(10, IsUnicode = true, InputMask = ">aaaaaaaaaa")]
    [PXUIField(DisplayName = "Deferral Code")]
    [PXSelector(typeof (Search<DRDeferredCode.deferredCodeID, Where<DRDeferredCode.accountType, Equal<Current<SchedulesInq.SchedulesFilter.accountType>>>>), new System.Type[] {typeof (DRDeferredCode.deferredCodeID), typeof (DRDeferredCode.description), typeof (DRDeferredCode.accountType), typeof (DRDeferredCode.accountID), typeof (DRDeferredCode.subID), typeof (DRDeferredCode.method), typeof (DRDeferredCode.active)})]
    public virtual string DeferredCode
    {
      get => this._DeferredCode;
      set => this._DeferredCode = value;
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
    [PXSelector(typeof (Search<BAccountR.bAccountID, Where<BAccountR.type, Equal<Current<SchedulesInq.SchedulesFilter.bAccountType>>, Or<BAccountR.type, Equal<BAccountType.combinedType>>>>), new System.Type[] {typeof (BAccountR.acctCD), typeof (BAccountR.acctName), typeof (BAccountR.type)}, SubstituteKey = typeof (BAccountR.acctCD))]
    public virtual int? BAccountID { get; set; }

    [AnyInventory(DisplayName = "Component")]
    public virtual int? ComponentID
    {
      get => this._ComponentID;
      set => this._ComponentID = value;
    }

    [PXDecimal(2)]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Total Scheduled", Enabled = false)]
    public virtual Decimal? TotalScheduled { get; set; }

    [PXDecimal(2)]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Total Deferred", Enabled = false)]
    public virtual Decimal? TotalDeferred { get; set; }

    public abstract class organizationID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      SchedulesInq.SchedulesFilter.organizationID>
    {
    }

    public abstract class branchID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      SchedulesInq.SchedulesFilter.branchID>
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
      SchedulesInq.SchedulesFilter.accountType>
    {
    }

    public abstract class accountID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      SchedulesInq.SchedulesFilter.accountID>
    {
    }

    public abstract class subID : BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SchedulesInq.SchedulesFilter.subID>
    {
    }

    public abstract class deferredCode : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      SchedulesInq.SchedulesFilter.deferredCode>
    {
    }

    public abstract class bAccountType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      SchedulesInq.SchedulesFilter.bAccountType>
    {
    }

    public abstract class bAccountID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      SchedulesInq.SchedulesFilter.bAccountID>
    {
    }

    public abstract class componentID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      SchedulesInq.SchedulesFilter.componentID>
    {
    }

    public abstract class totalScheduled : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      SchedulesInq.SchedulesFilter.totalScheduled>
    {
    }

    public abstract class totalDeferred : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      SchedulesInq.SchedulesFilter.totalDeferred>
    {
    }
  }

  [Serializable]
  public class SchedulesInqResult : DRScheduleDetail
  {
    protected string _ComponentCD;

    [PXDBDefault(typeof (DRSchedule.scheduleID))]
    [PXSelector(typeof (DRSchedule.scheduleID), DirtyRead = true)]
    [PXParent(typeof (Select<DRSchedule, Where<DRSchedule.scheduleID, Equal<Current<DRScheduleDetail.scheduleID>>>>))]
    [PXDBInt(IsKey = true)]
    [PXUIField(DisplayName = "Schedule Number", TabOrder = 1)]
    public override int? ScheduleID
    {
      get => this._ScheduleID;
      set => this._ScheduleID = value;
    }

    [PXDBInt(IsKey = true)]
    [PXUIField]
    public override int? ComponentID
    {
      get => this._ComponentID;
      set => this._ComponentID = value;
    }

    [PXString]
    [PXUIField]
    public virtual string ComponentCD
    {
      get => this._ComponentCD;
      set => this._ComponentCD = value;
    }

    [PXBaseCury]
    [PXDBCalced(typeof (Switch<Case<Where<CurrentValue<SchedulesInq.SchedulesFilter.accountType>, Equal<AccountType.income>, And2<Where<DRScheduleDetail.docType, Equal<ARDocType.creditMemo>, Or<DRScheduleDetail.docType, Equal<ARDocType.cashReturn>>>, Or<CurrentValue<SchedulesInq.SchedulesFilter.accountType>, Equal<AccountType.expense>, And<Where<DRScheduleDetail.docType, Equal<APDocType.debitAdj>, Or<DRScheduleDetail.docType, Equal<APDocType.voidQuickCheck>>>>>>>, Minus<DRScheduleDetail.totalAmt>>, DRScheduleDetail.totalAmt>), typeof (Decimal))]
    [PXUIField(DisplayName = "Total Amount")]
    public virtual Decimal? SignTotalAmt { get; set; }

    [PXBaseCury]
    [PXDBCalced(typeof (Switch<Case<Where<CurrentValue<SchedulesInq.SchedulesFilter.accountType>, Equal<AccountType.income>, And2<Where<DRScheduleDetail.docType, Equal<ARDocType.creditMemo>, Or<DRScheduleDetail.docType, Equal<ARDocType.cashReturn>>>, Or<CurrentValue<SchedulesInq.SchedulesFilter.accountType>, Equal<AccountType.expense>, And<Where<DRScheduleDetail.docType, Equal<APDocType.debitAdj>, Or<DRScheduleDetail.docType, Equal<APDocType.voidQuickCheck>>>>>>>, Minus<DRScheduleDetail.defAmt>>, DRScheduleDetail.defAmt>), typeof (Decimal))]
    [PXUIField]
    public virtual Decimal? SignDefAmt { get; set; }

    public new abstract class scheduleID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      SchedulesInq.SchedulesInqResult.scheduleID>
    {
    }

    public new abstract class componentID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      SchedulesInq.SchedulesInqResult.componentID>
    {
    }

    public abstract class componentCD : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      SchedulesInq.SchedulesInqResult.componentCD>
    {
    }

    public abstract class signTotalAmt : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      SchedulesInq.SchedulesInqResult.signTotalAmt>
    {
    }

    public abstract class signDefAmt : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      SchedulesInq.SchedulesInqResult.signDefAmt>
    {
    }
  }
}
