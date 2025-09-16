// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.ReverseUnbilledProcess
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.AR;
using PX.Objects.EP;
using PX.Objects.IN;
using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;

#nullable enable
namespace PX.Objects.PM;

[Serializable]
public class ReverseUnbilledProcess : PXGraph<
#nullable disable
ReverseUnbilledProcess>
{
  public PXCancel<ReverseUnbilledProcess.TranFilter> Cancel;
  public PXFilter<ReverseUnbilledProcess.TranFilter> Filter;
  [PXFilterable(new Type[] {})]
  public PXFilteredProcessingJoin<PMTran, ReverseUnbilledProcess.TranFilter, InnerJoin<PMRegister, On<PMRegister.module, Equal<PMTran.tranType>, And<PMRegister.refNbr, Equal<PMTran.refNbr>, And<PMRegister.isAllocation, Equal<True>>>>, InnerJoin<PMProject, On<PMProject.contractID, Equal<PMTran.projectID>>, InnerJoin<PMTask, On<PMTask.projectID, Equal<PMTran.projectID>, And<PMTask.taskID, Equal<PMTran.taskID>>>, LeftJoin<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<PMProject.customerID>>>>>>, Where<PMTran.billed, Equal<False>, And<PMTran.excludedFromBilling, Equal<False>, And<PMTran.released, Equal<True>, And2<Where<PMTran.allocated, Equal<True>, Or<PMTran.excludedFromAllocation, Equal<True>>>, And2<Where<PMTask.billingID, Equal<Current<ReverseUnbilledProcess.TranFilter.billingID>>, Or<Current<ReverseUnbilledProcess.TranFilter.billingID>, IsNull>>, And2<Where<PMTran.projectID, Equal<Current<ReverseUnbilledProcess.TranFilter.projectID>>, Or<Current<ReverseUnbilledProcess.TranFilter.projectID>, IsNull>>, And2<Where<PMTran.taskID, Equal<Current<ReverseUnbilledProcess.TranFilter.projectTaskID>>, Or<Current<ReverseUnbilledProcess.TranFilter.projectTaskID>, IsNull>>, And2<Where<PMProject.customerID, Equal<Current<ReverseUnbilledProcess.TranFilter.customerID>>, Or<Current<ReverseUnbilledProcess.TranFilter.customerID>, IsNull>>, And2<Where<PX.Objects.AR.Customer.customerClassID, Equal<Current<ReverseUnbilledProcess.TranFilter.customerClassID>>, Or<Current<ReverseUnbilledProcess.TranFilter.customerClassID>, IsNull>>, And2<Where<PMTran.inventoryID, Equal<Current<ReverseUnbilledProcess.TranFilter.inventoryID>>, Or<Current<ReverseUnbilledProcess.TranFilter.inventoryID>, IsNull>>, And2<Where<PMTran.resourceID, Equal<Current<ReverseUnbilledProcess.TranFilter.resourceID>>, Or<Current<ReverseUnbilledProcess.TranFilter.resourceID>, IsNull>>, And2<Where2<Where<Current<ReverseUnbilledProcess.TranFilter.dateFrom>, IsNotNull, And<Current<ReverseUnbilledProcess.TranFilter.dateTo>, IsNotNull, And<Current<ReverseUnbilledProcess.TranFilter.dateFrom>, Equal<Current<ReverseUnbilledProcess.TranFilter.dateTo>>, And<PMTran.date, Equal<Current<ReverseUnbilledProcess.TranFilter.dateFrom>>>>>>, Or2<Where<Current<ReverseUnbilledProcess.TranFilter.dateFrom>, IsNotNull, And<Current<ReverseUnbilledProcess.TranFilter.dateTo>, IsNotNull, And<Current<ReverseUnbilledProcess.TranFilter.dateFrom>, NotEqual<Current<ReverseUnbilledProcess.TranFilter.dateTo>>, And<PMTran.date, Between<Current<ReverseUnbilledProcess.TranFilter.dateFrom>, Current<ReverseUnbilledProcess.TranFilter.dateTo>>>>>>, Or2<Where<Current<ReverseUnbilledProcess.TranFilter.dateFrom>, IsNotNull, And<Current<ReverseUnbilledProcess.TranFilter.dateTo>, IsNull, And<PMTran.date, GreaterEqual<Current<ReverseUnbilledProcess.TranFilter.dateFrom>>>>>, Or2<Where<Current<ReverseUnbilledProcess.TranFilter.dateFrom>, IsNull, And<Current<ReverseUnbilledProcess.TranFilter.dateTo>, IsNotNull, And<PMTran.date, LessEqual<Current<ReverseUnbilledProcess.TranFilter.dateTo>>>>>, Or<Where<Current<ReverseUnbilledProcess.TranFilter.dateFrom>, IsNull, And<Current<ReverseUnbilledProcess.TranFilter.dateTo>, IsNull>>>>>>>, And<CurrentMatch<PMProject, AccessInfo.userName>>>>>>>>>>>>>>> Items;
  [PXCopyPasteHiddenView]
  [PXHidden]
  public PXSelect<PMRegister> dummy;
  public PXAction<ReverseUnbilledProcess.TranFilter> viewDocument;

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

  [ProjectTask(typeof (PMTran.projectID))]
  protected virtual void _(PX.Data.Events.CacheAttached<PMTran.taskID> e)
  {
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable ViewDocument(PXAdapter adapter)
  {
    RegisterEntry instance = PXGraph.CreateInstance<RegisterEntry>();
    ((PXSelectBase<PMRegister>) instance.Document).Current = PXResultset<PMRegister>.op_Implicit(((PXSelectBase<PMRegister>) instance.Document).Search<PMRegister.refNbr>((object) ((PXSelectBase<PMTran>) this.Items).Current.RefNbr, new object[1]
    {
      (object) ((PXSelectBase<PMTran>) this.Items).Current.TranType
    }));
    ProjectAccountingService.NavigateToScreen((PXGraph) instance);
    return adapter.Get();
  }

  protected virtual void TranFilter_RowUpdated(PXCache cache, PXRowUpdatedEventArgs e)
  {
    ((PXSelectBase) this.Items).Cache.Clear();
  }

  protected virtual void TranFilter_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    ReverseUnbilledProcess.TranFilter current = ((PXSelectBase<ReverseUnbilledProcess.TranFilter>) this.Filter).Current;
    // ISSUE: method pointer
    ((PXProcessingBase<PMTran>) this.Items).SetProcessDelegate(new PXProcessingBase<PMTran>.ProcessItemDelegate((object) null, __methodptr(ReverseAllocatedTran)));
  }

  public static void ReverseAllocatedTran(PMTran tran)
  {
    RegisterEntry instance = PXGraph.CreateInstance<RegisterEntry>();
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    ((PXGraph) instance).FieldVerifying.AddHandler<PMTran.projectID>(ReverseUnbilledProcess.\u003C\u003Ec.\u003C\u003E9__13_0 ?? (ReverseUnbilledProcess.\u003C\u003Ec.\u003C\u003E9__13_0 = new PXFieldVerifying((object) ReverseUnbilledProcess.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CReverseAllocatedTran\u003Eb__13_0))));
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    ((PXGraph) instance).FieldVerifying.AddHandler<PMTran.taskID>(ReverseUnbilledProcess.\u003C\u003Ec.\u003C\u003E9__13_1 ?? (ReverseUnbilledProcess.\u003C\u003Ec.\u003C\u003E9__13_1 = new PXFieldVerifying((object) ReverseUnbilledProcess.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CReverseAllocatedTran\u003Eb__13_1))));
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    ((PXGraph) instance).FieldVerifying.AddHandler<PMTran.inventoryID>(ReverseUnbilledProcess.\u003C\u003Ec.\u003C\u003E9__13_2 ?? (ReverseUnbilledProcess.\u003C\u003Ec.\u003C\u003E9__13_2 = new PXFieldVerifying((object) ReverseUnbilledProcess.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CReverseAllocatedTran\u003Eb__13_2))));
    PMRegister pmRegister = PXResultset<PMRegister>.op_Implicit(PXSelectBase<PMRegister, PXSelectReadonly<PMRegister, Where<PMRegister.refNbr, Equal<Required<PMRegister.refNbr>>>>.Config>.Select((PXGraph) instance, new object[1]
    {
      (object) tran.RefNbr
    }));
    PMRegister doc = (PMRegister) ((PXSelectBase) instance.Document).Cache.Insert();
    doc.OrigDocType = "RV";
    doc.OrigNoteID = pmRegister.NoteID;
    doc.Description = PXMessages.LocalizeNoPrefix("Allocation Reversal");
    ((PXSelectBase<PMRegister>) instance.Document).Current = doc;
    instance.ReverseTransaction(tran, pmRegister.IsAllocation.GetValueOrDefault());
    ((PXAction) instance.Save).Press();
    if (!PXResultset<PMSetup>.op_Implicit(PXSelectBase<PMSetup, PXSelect<PMSetup>.Config>.Select((PXGraph) instance, Array.Empty<object>())).AutoReleaseAllocation.GetValueOrDefault())
      return;
    RegisterRelease.Release(doc);
  }

  [PXHidden]
  [ExcludeFromCodeCoverage]
  [Serializable]
  public class TranFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected string _BillingID;
    protected string _CustomerClassID;
    protected int? _CustomerID;
    protected int? _ProjectID;
    protected int? _ProjectTaskID;
    protected int? _InventoryID;
    protected int? _ResourceID;
    protected DateTime? _DateFrom;
    protected DateTime? _DateTo;

    [PXSelector(typeof (PMBilling.billingID), DescriptionField = typeof (PMBilling.description))]
    [PXDBString(15, IsUnicode = true)]
    [PXUIField(DisplayName = "Billing Rule")]
    public virtual string BillingID
    {
      get => this._BillingID;
      set => this._BillingID = value;
    }

    [PXDBString(10, IsUnicode = true)]
    [PXSelector(typeof (CustomerClass.customerClassID), DescriptionField = typeof (CustomerClass.descr), CacheGlobal = true)]
    [PXUIField(DisplayName = "Customer Class")]
    public virtual string CustomerClassID
    {
      get => this._CustomerClassID;
      set => this._CustomerClassID = value;
    }

    [Customer]
    public virtual int? CustomerID
    {
      get => this._CustomerID;
      set => this._CustomerID = value;
    }

    [Project]
    public virtual int? ProjectID
    {
      get => this._ProjectID;
      set => this._ProjectID = value;
    }

    [ProjectTask(typeof (ReverseUnbilledProcess.TranFilter.projectID))]
    public virtual int? ProjectTaskID
    {
      get => this._ProjectTaskID;
      set => this._ProjectTaskID = value;
    }

    [Inventory]
    public virtual int? InventoryID
    {
      get => this._InventoryID;
      set => this._InventoryID = value;
    }

    [PXEPEmployeeSelector]
    [PXDBInt]
    [PXUIField(DisplayName = "Employee")]
    public virtual int? ResourceID
    {
      get => this._ResourceID;
      set => this._ResourceID = value;
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

    public abstract class billingID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ReverseUnbilledProcess.TranFilter.billingID>
    {
    }

    public abstract class customerClassID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ReverseUnbilledProcess.TranFilter.customerClassID>
    {
    }

    public abstract class customerID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ReverseUnbilledProcess.TranFilter.customerID>
    {
    }

    public abstract class projectID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ReverseUnbilledProcess.TranFilter.projectID>
    {
    }

    public abstract class projectTaskID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ReverseUnbilledProcess.TranFilter.projectTaskID>
    {
    }

    public abstract class inventoryID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ReverseUnbilledProcess.TranFilter.inventoryID>
    {
    }

    public abstract class resourceID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ReverseUnbilledProcess.TranFilter.resourceID>
    {
    }

    public abstract class dateFrom : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      ReverseUnbilledProcess.TranFilter.dateFrom>
    {
    }

    public abstract class dateTo : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      ReverseUnbilledProcess.TranFilter.dateTo>
    {
    }
  }
}
