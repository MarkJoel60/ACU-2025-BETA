// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.TransactionInquiry
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.AR;
using PX.Objects.Common;
using PX.Objects.CT;
using PX.Objects.EP;
using PX.Objects.GL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;

#nullable enable
namespace PX.Objects.PM;

[TableDashboardType]
[Serializable]
public class TransactionInquiry : PXGraph<
#nullable disable
TransactionInquiry>
{
  public PXFilter<TransactionInquiry.TranFilter> Filter;
  public PXCancel<TransactionInquiry.TranFilter> Cancel;
  public PXSelect<PX.Objects.AR.ARTran> Dummy;
  [PXCopyPasteHiddenView]
  [PXHidden]
  public PXSelect<PMCostCode> dummyCostCode;
  public PXSetup<PMProject>.Where<BqlOperand<
  #nullable enable
  PMProject.contractID, IBqlInt>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  TransactionInquiry.TranFilter.projectID, IBqlInt>.FromCurrent>> Project;
  [PXFilterable(new System.Type[] {})]
  public 
  #nullable disable
  PXSelectJoin<PMTran, LeftJoin<PMRegister, On<PMTran.tranType, Equal<PMRegister.module>, And<PMTran.refNbr, Equal<PMRegister.refNbr>>>, LeftJoin<PX.Objects.CR.BAccount, On<PX.Objects.CR.BAccount.bAccountID, Equal<PMTran.resourceID>>, LeftJoin<PMAccountGroup, On<PMAccountGroup.groupID, Equal<PMTran.accountGroupID>>, LeftJoin<PMProformaLine, On<PMProformaLine.refNbr, Equal<PMTran.proformaRefNbr>, And<PMProformaLine.lineNbr, Equal<PMTran.proformaLineNbr>, And<PMProformaLine.corrected, Equal<False>>>>>>>>, Where<PMTran.projectID, Equal<Current<TransactionInquiry.TranFilter.projectID>>>> Transactions;
  [PXCopyPasteHiddenView]
  [PXHidden]
  public PXSelect<PMRegister> dummy;
  [PXCopyPasteHiddenView]
  [PXHidden]
  public PXSelect<PX.Objects.CR.BAccount> dummyBAccount;
  public PXAction<TransactionInquiry.TranFilter> viewDocument;
  public PXAction<TransactionInquiry.TranFilter> viewOrigDocument;
  public PXAction<TransactionInquiry.TranFilter> viewInventory;
  public PXAction<TransactionInquiry.TranFilter> viewCustomer;
  public PXAction<TransactionInquiry.TranFilter> viewProforma;
  public PXAction<TransactionInquiry.TranFilter> viewInvoice;

  [AccountGroup(DisplayName = "Debit Account Group")]
  protected virtual void _(PX.Data.Events.CacheAttached<PMTran.accountGroupID> e)
  {
  }

  [PXMergeAttributes]
  [PXUIField]
  protected virtual void _(PX.Data.Events.CacheAttached<PMTran.aRRefNbr> e)
  {
  }

  [PXBool]
  [PXDefault(false)]
  protected virtual void _(
    PX.Data.Events.CacheAttached<PMCostCode.isProjectOverride> e)
  {
  }

  [PXDBString]
  [PXUIField(DisplayName = "Old Orig. Doc. Nbr.", Visible = false, Enabled = false)]
  protected virtual void _(PX.Data.Events.CacheAttached<PMRegister.origDocNbr> _)
  {
  }

  [PXMergeAttributes]
  [PXDecimal]
  [PXUIField(DisplayName = "Total Quantity", Enabled = false)]
  protected virtual void PMRegister_QtyTotal_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXDecimal]
  [PXUIField(DisplayName = "Total Billable Quantity", Enabled = false)]
  protected virtual void PMRegister_BillableQtyTotal_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXDecimal]
  [PXUIField(DisplayName = "Total Amount", Enabled = false)]
  protected virtual void PMRegister_AmtTotal_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Employee Name", Visible = false)]
  protected virtual void _(PX.Data.Events.CacheAttached<PX.Objects.CR.BAccount.acctName> e)
  {
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Pro Forma Line Nbr.", Visible = false, Enabled = false)]
  protected virtual void _(PX.Data.Events.CacheAttached<PMProformaLine.lineNbr> e)
  {
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Pro Forma Line Status", Visible = false, Enabled = false)]
  [ProformaLineStatus]
  protected virtual void _(PX.Data.Events.CacheAttached<PMProformaLine.option> e)
  {
  }

  [PXMergeAttributes]
  [BaseProjectTask(typeof (TransactionInquiry.TranFilter.projectID))]
  protected virtual void _(PX.Data.Events.CacheAttached<PMTran.taskID> e)
  {
  }

  public virtual IEnumerable transactions()
  {
    TransactionInquiry.TranFilter current = ((PXSelectBase<TransactionInquiry.TranFilter>) this.Filter).Current;
    if (!TransactionInquiry.IsNotEmpty(current))
      return (IEnumerable) Enumerable.Empty<PMTran>();
    List<object> objectList = new List<object>();
    PXSelectBase<PMTran> pxSelectBase = (PXSelectBase<PMTran>) new PXSelectJoin<PMTran, LeftJoin<PMRegister, On<PMTran.tranType, Equal<PMRegister.module>, And<PMTran.refNbr, Equal<PMRegister.refNbr>>>, LeftJoin<PX.Objects.CR.BAccount, On<PX.Objects.CR.BAccount.bAccountID, Equal<PMTran.resourceID>>, LeftJoin<PMAccountGroup, On<PMAccountGroup.groupID, Equal<PMTran.accountGroupID>>, LeftJoin<RegisterReleaseProcess.OffsetPMAccountGroup, On<RegisterReleaseProcess.OffsetPMAccountGroup.groupID, Equal<PMTran.offsetAccountGroupID>>, LeftJoin<PMProformaLine, On<PMProformaLine.refNbr, Equal<PMTran.proformaRefNbr>, And<PMProformaLine.lineNbr, Equal<PMTran.proformaLineNbr>, And<PMProformaLine.corrected, Equal<False>>>>>>>>>, Where<True, Equal<True>, And2<Where<RegisterReleaseProcess.OffsetPMAccountGroup.groupID, IsNull, Or<Match<RegisterReleaseProcess.OffsetPMAccountGroup, Current<AccessInfo.userName>>>>, And<Where<PMAccountGroup.groupID, IsNull, Or<Match<PMAccountGroup, Current<AccessInfo.userName>>>>>>>>((PXGraph) this);
    if (current.ARRefNbr != null)
    {
      string[] array = ((IQueryable<PXResult<PX.Objects.AR.ARInvoice>>) PXSelectBase<PX.Objects.AR.ARInvoice, PXSelectJoin<PX.Objects.AR.ARInvoice, InnerJoin<PMRegister, On<PMRegister.origNoteID, Equal<PX.Objects.AR.ARInvoice.noteID>>>, Where<PX.Objects.AR.ARRegister.docType, Equal<Current<TransactionInquiry.TranFilter.aRDocType>>, And<PX.Objects.AR.ARRegister.refNbr, Equal<Current<TransactionInquiry.TranFilter.aRRefNbr>>>>>.Config>.Select((PXGraph) this, Array.Empty<object>())).Select<PXResult<PX.Objects.AR.ARInvoice>, string>((Expression<Func<PXResult<PX.Objects.AR.ARInvoice>, string>>) (r => ((PMRegister) (PXResult<PX.Objects.AR.ARInvoice, PMRegister>) r).RefNbr)).ToArray<string>();
      if (((IEnumerable<string>) array).Any<string>())
      {
        objectList.Add((object) array);
        pxSelectBase.WhereAnd<Where2<Where<PMTran.aRTranType, Equal<Current<TransactionInquiry.TranFilter.aRDocType>>, And<PMTran.aRRefNbr, Equal<Current<TransactionInquiry.TranFilter.aRRefNbr>>>>, Or<Where<PMTran.refNbr, In<Required<PMTran.refNbr>>>>>>();
      }
      else
        pxSelectBase.WhereAnd<Where<PMTran.aRTranType, Equal<Current<TransactionInquiry.TranFilter.aRDocType>>, And<PMTran.aRRefNbr, Equal<Current<TransactionInquiry.TranFilter.aRRefNbr>>>>>();
    }
    if (current.TranID.HasValue)
      pxSelectBase.WhereAnd<Where<PMTran.tranID, Equal<Current<TransactionInquiry.TranFilter.tranID>>>>();
    if (!string.IsNullOrWhiteSpace(current.RefNbr))
      pxSelectBase.WhereAnd<Where<PMTran.refNbr, Equal<Current<TransactionInquiry.TranFilter.refNbr>>>>();
    int? nullable1 = current.ProjectID;
    if (nullable1.HasValue)
      pxSelectBase.WhereAnd<Where<PMTran.projectID, Equal<Current<TransactionInquiry.TranFilter.projectID>>>>();
    if (!string.IsNullOrWhiteSpace(current.AccountGroupType))
      pxSelectBase.WhereAnd<Where<PMAccountGroup.type, Equal<Current<TransactionInquiry.TranFilter.accountGroupType>>>>();
    nullable1 = current.AccountGroupID;
    if (nullable1.HasValue)
      pxSelectBase.WhereAnd<Where<PMTran.accountGroupID, Equal<Current<TransactionInquiry.TranFilter.accountGroupID>>, Or<PMTran.offsetAccountGroupID, Equal<Current<TransactionInquiry.TranFilter.accountGroupID>>>>>();
    nullable1 = current.AccountID;
    if (nullable1.HasValue)
      pxSelectBase.WhereAnd<Where<PMTran.accountID, Equal<Current<TransactionInquiry.TranFilter.accountID>>, Or<PMTran.offsetAccountID, Equal<Current<TransactionInquiry.TranFilter.accountID>>>>>();
    nullable1 = current.ProjectTaskID;
    if (nullable1.HasValue)
      pxSelectBase.WhereAnd<Where<PMTran.taskID, Equal<Current<TransactionInquiry.TranFilter.projectTaskID>>>>();
    nullable1 = current.CostCode;
    if (nullable1.HasValue)
      pxSelectBase.WhereAnd<Where<PMTran.costCodeID, Equal<Current<TransactionInquiry.TranFilter.costCode>>>>();
    nullable1 = current.InventoryID;
    if (nullable1.HasValue)
      pxSelectBase.WhereAnd<Where<PMTran.inventoryID, Equal<Current<TransactionInquiry.TranFilter.inventoryID>>>>();
    nullable1 = current.ResourceID;
    if (nullable1.HasValue)
      pxSelectBase.WhereAnd<Where<PMTran.resourceID, Equal<Current<TransactionInquiry.TranFilter.resourceID>>>>();
    bool? nullable2 = current.OnlyAllocation;
    if (nullable2.GetValueOrDefault())
      pxSelectBase.WhereAnd<Where<PMRegister.isAllocation, Equal<True>>>();
    nullable2 = current.IncludeUnreleased;
    if (!nullable2.GetValueOrDefault())
      pxSelectBase.WhereAnd<Where<PMRegister.released, Equal<True>>>();
    DateTime? nullable3 = current.DateFrom;
    if (nullable3.HasValue)
    {
      nullable3 = current.DateTo;
      if (nullable3.HasValue)
      {
        nullable3 = current.DateFrom;
        DateTime? dateTo = current.DateTo;
        if ((nullable3.HasValue == dateTo.HasValue ? (nullable3.HasValue ? (nullable3.GetValueOrDefault() == dateTo.GetValueOrDefault() ? 1 : 0) : 1) : 0) != 0)
        {
          pxSelectBase.WhereAnd<Where<PMTran.date, Equal<Current<TransactionInquiry.TranFilter.dateFrom>>>>();
          goto label_38;
        }
        pxSelectBase.WhereAnd<Where<PMTran.date, Between<Current<TransactionInquiry.TranFilter.dateFrom>, Current<TransactionInquiry.TranFilter.dateTo>>>>();
        goto label_38;
      }
    }
    DateTime? nullable4 = current.DateFrom;
    if (nullable4.HasValue)
    {
      pxSelectBase.WhereAnd<Where<PMTran.date, GreaterEqual<Current<TransactionInquiry.TranFilter.dateFrom>>>>();
    }
    else
    {
      nullable4 = current.DateTo;
      if (nullable4.HasValue)
        pxSelectBase.WhereAnd<Where<PMTran.date, LessEqual<Current<TransactionInquiry.TranFilter.dateTo>>>>();
    }
label_38:
    return TimeCardMaint.QSelect((PXGraph) this, ((PXSelectBase) pxSelectBase).View.BqlSelect, objectList.ToArray());
  }

  private static bool IsNotEmpty(TransactionInquiry.TranFilter filter)
  {
    if (filter == null)
      return false;
    return filter.ProjectID.HasValue || filter.AccountID.HasValue || filter.AccountGroupID.HasValue || filter.ARRefNbr != null || filter.TranID.HasValue || !string.IsNullOrWhiteSpace(filter.RefNbr);
  }

  public TransactionInquiry()
  {
    ((PXSelectBase) this.Transactions).Cache.AllowInsert = false;
    ((PXSelectBase) this.Transactions).Cache.AllowUpdate = false;
    ((PXSelectBase) this.Transactions).Cache.AllowDelete = false;
  }

  protected virtual void _(PX.Data.Events.RowSelected<PMTran> e)
  {
    PXUIFieldAttribute.SetVisible<PMTran.earningType>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMTran>>) e).Cache, (object) null, false);
    PXUIFieldAttribute.SetVisible<PMTran.overtimeMultiplier>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PMTran>>) e).Cache, (object) null, false);
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable ViewDocument(PXAdapter adapter)
  {
    RegisterEntry instance = PXGraph.CreateInstance<RegisterEntry>();
    ((PXSelectBase<PMRegister>) instance.Document).Current = PXResultset<PMRegister>.op_Implicit(((PXSelectBase<PMRegister>) instance.Document).Search<PMRegister.refNbr>((object) ((PXSelectBase<PMTran>) this.Transactions).Current.RefNbr, new object[1]
    {
      (object) ((PXSelectBase<PMTran>) this.Transactions).Current.TranType
    }));
    ProjectAccountingService.NavigateToScreen((PXGraph) instance);
    return adapter.Get();
  }

  [PXButton]
  [PXUIField]
  public virtual void ViewOrigDocument()
  {
    PMRegister pmRegister = PXResultset<PMRegister>.op_Implicit(PXSelectBase<PMRegister, PXSelect<PMRegister, Where<PMRegister.refNbr, Equal<Current<PMTran.refNbr>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()));
    EntityHelper entityHelper = new EntityHelper((PXGraph) this);
    if (!pmRegister.OrigNoteID.HasValue)
      return;
    entityHelper.NavigateToRow(new Guid?(pmRegister.OrigNoteID.Value), (PXRedirectHelper.WindowMode) 1);
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable ViewInventory(PXAdapter adapter)
  {
    ProjectAccountingService.NavigateToInventoryItemScreen(PXResultset<PX.Objects.IN.InventoryItem>.op_Implicit(PXSelectBase<PX.Objects.IN.InventoryItem, PXSelect<PX.Objects.IN.InventoryItem, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Current<PMTran.inventoryID>>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
    {
      (object) ((PXSelectBase<PMTran>) this.Transactions).Current
    }, Array.Empty<object>())));
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable ViewCustomer(PXAdapter adapter)
  {
    ProjectAccountingService.NavigateToCustomerScreen(PXResultset<PX.Objects.CR.BAccount>.op_Implicit(PXSelectBase<PX.Objects.CR.BAccount, PXSelect<PX.Objects.CR.BAccount, Where<PX.Objects.CR.BAccount.bAccountID, Equal<Current<PMTran.bAccountID>>>>.Config>.Select((PXGraph) this, Array.Empty<object>())));
    return adapter.Get();
  }

  [PXUIField(DisplayName = "", Enabled = false)]
  [PXButton]
  public virtual IEnumerable ViewProforma(PXAdapter adapter)
  {
    ProformaEntry instance = PXGraph.CreateInstance<ProformaEntry>();
    ((PXGraph) instance).Clear();
    ((PXSelectBase<PMProforma>) instance.Document).Current = PXResultset<PMProforma>.op_Implicit(PXSelectBase<PMProforma, PXSelect<PMProforma, Where<PMProforma.refNbr, Equal<Current<PMTran.proformaRefNbr>>, And<PMProforma.corrected, NotEqual<True>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()));
    ProjectAccountingService.NavigateToScreen((PXGraph) instance, "Pro Forma Invoice");
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable ViewInvoice(PXAdapter adapter)
  {
    ARInvoiceEntry instance = PXGraph.CreateInstance<ARInvoiceEntry>();
    ((PXGraph) instance).Clear();
    ((PXSelectBase<PX.Objects.AR.ARInvoice>) instance.Document).Current = PXResultset<PX.Objects.AR.ARInvoice>.op_Implicit(PXSelectBase<PX.Objects.AR.ARInvoice, PXSelect<PX.Objects.AR.ARInvoice, Where<PX.Objects.AR.ARInvoice.docType, Equal<Current<PMTran.aRTranType>>, And<PX.Objects.AR.ARInvoice.refNbr, Equal<Current<PMTran.aRRefNbr>>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()));
    ProjectAccountingService.NavigateToScreen((PXGraph) instance, "AR Invoice/Memo");
    return adapter.Get();
  }

  protected virtual void _(
    PX.Data.Events.FieldSelecting<PMTran, PMTran.projectCuryID> e)
  {
    if (((PXSelectBase<PMProject>) this.Project).Current == null)
      return;
    ((PX.Data.Events.FieldSelectingBase<PX.Data.Events.FieldSelecting<PMTran, PMTran.projectCuryID>>) e).ReturnValue = (object) ((PXSelectBase<PMProject>) this.Project).Current.CuryID;
  }

  [PXHidden]
  [ExcludeFromCodeCoverage]
  [Serializable]
  public class TranFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected int? _ProjectID;
    protected int? _ProjectTaskID;
    protected int? _InventoryID;
    protected DateTime? _DateFrom;
    protected DateTime? _DateTo;
    protected int? _ResourceID;
    protected bool? _OnlyAllocation;

    [PX.Objects.PM.Project(typeof (Where<PMProject.baseType, Equal<CTPRType.project>, And<PMProject.nonProject, Equal<False>>>), WarnIfCompleted = false)]
    public virtual int? ProjectID
    {
      get => this._ProjectID;
      set => this._ProjectID = value;
    }

    /// <summary>The type of the account groups.</summary>
    [PXDBString(1)]
    [PMAccountType.List]
    public virtual string AccountGroupType { get; set; }

    [AccountGroup(typeof (Where<Match<PMAccountGroup, Current<AccessInfo.userName>>>))]
    public virtual int? AccountGroupID { get; set; }

    [ProjectTask(typeof (TransactionInquiry.TranFilter.projectID))]
    public virtual int? ProjectTaskID
    {
      get => this._ProjectTaskID;
      set => this._ProjectTaskID = value;
    }

    [CostCode(Filterable = false, SkipVerification = true)]
    public virtual int? CostCode { get; set; }

    [PXDBInt]
    [PXUIField]
    [PMInventorySelector(Filterable = true)]
    public virtual int? InventoryID
    {
      get => this._InventoryID;
      set => this._InventoryID = value;
    }

    [PXDBDate]
    [PXUIField]
    public virtual DateTime? DateFrom
    {
      get => this._DateFrom;
      set => this._DateFrom = value;
    }

    [PXDBDate]
    [PXUIField]
    public virtual DateTime? DateTo
    {
      get => this._DateTo;
      set => this._DateTo = value;
    }

    [PXEPEmployeeSelector]
    [PXDBInt]
    [PXUIField(DisplayName = "Employee")]
    public virtual int? ResourceID
    {
      get => this._ResourceID;
      set => this._ResourceID = value;
    }

    [PXDBBool]
    [PXDefault(false)]
    [PXUIField(DisplayName = "Show Only Allocation Transactions")]
    public virtual bool? OnlyAllocation
    {
      get => this._OnlyAllocation;
      set => this._OnlyAllocation = value;
    }

    [PXDBBool]
    [PXDefault(true)]
    [PXUIField(DisplayName = "Include Unreleased Transactions")]
    public virtual bool? IncludeUnreleased { get; set; }

    /// <summary>Account</summary>
    [Account(null, typeof (Search<PX.Objects.GL.Account.accountID, Where<PX.Objects.GL.Account.accountGroupID, IsNotNull>>))]
    public virtual int? AccountID { get; set; }

    /// <summary>AR Document Type</summary>
    [PXDBString(3, IsFixed = true)]
    [TransactionInquiry.ARDocTypeList]
    [PXUIField]
    public virtual string ARDocType { get; set; }

    /// <summary>AR Document Reference Number</summary>
    [PXDBString(15, IsUnicode = true, InputMask = "")]
    [PXUIField]
    [PXSelector(typeof (Search<PX.Objects.AR.ARRegister.refNbr, Where<PX.Objects.AR.ARRegister.docType, Equal<Current<TransactionInquiry.TranFilter.aRDocType>>>>))]
    public virtual string ARRefNbr { get; set; }

    /// <summary>Project transaction ID</summary>
    [PXDBLong]
    [PXUIField(DisplayName = "PM Tran.")]
    public virtual long? TranID { get; set; }

    /// <summary>The reference number of the PM Document.</summary>
    [PXDBString(15, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
    [PXSelector(typeof (Search<PMRegister.refNbr>), Filterable = true)]
    [PXUIField(DisplayName = "Ref. Number")]
    public virtual string RefNbr { get; set; }

    public abstract class projectID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      TransactionInquiry.TranFilter.projectID>
    {
    }

    public abstract class accountGroupType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      TransactionInquiry.TranFilter.accountGroupType>
    {
    }

    public abstract class accountGroupID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      TransactionInquiry.TranFilter.accountGroupID>
    {
    }

    public abstract class projectTaskID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      TransactionInquiry.TranFilter.projectTaskID>
    {
    }

    public abstract class costCode : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      TransactionInquiry.TranFilter.costCode>
    {
    }

    public abstract class inventoryID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      TransactionInquiry.TranFilter.inventoryID>
    {
    }

    public abstract class dateFrom : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      TransactionInquiry.TranFilter.dateFrom>
    {
    }

    public abstract class dateTo : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      TransactionInquiry.TranFilter.dateTo>
    {
    }

    public abstract class resourceID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      TransactionInquiry.TranFilter.resourceID>
    {
    }

    public abstract class onlyAllocation : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      TransactionInquiry.TranFilter.onlyAllocation>
    {
    }

    public abstract class includeUnreleased : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      TransactionInquiry.TranFilter.includeUnreleased>
    {
    }

    public abstract class accountID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      TransactionInquiry.TranFilter.accountID>
    {
    }

    public abstract class aRDocType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      TransactionInquiry.TranFilter.aRDocType>
    {
    }

    public abstract class aRRefNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      TransactionInquiry.TranFilter.aRRefNbr>
    {
    }

    public abstract class tranID : 
      BqlType<
      #nullable enable
      IBqlLong, long>.Field<
      #nullable disable
      TransactionInquiry.TranFilter.tranID>
    {
    }

    public abstract class refNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      TransactionInquiry.TranFilter.refNbr>
    {
    }
  }

  /// <summary>List of type of AR Documents</summary>
  public class ARDocTypeListAttribute : LabelListAttribute
  {
    private static readonly IEnumerable<ValueLabelPair> _valueLabelPairs = (IEnumerable<ValueLabelPair>) new ValueLabelList()
    {
      {
        "INV",
        "Invoice"
      },
      {
        "CRM",
        "Credit Memo"
      },
      {
        "DRM",
        "Debit Memo"
      }
    };

    public ARDocTypeListAttribute()
      : base(TransactionInquiry.ARDocTypeListAttribute._valueLabelPairs)
    {
    }
  }
}
