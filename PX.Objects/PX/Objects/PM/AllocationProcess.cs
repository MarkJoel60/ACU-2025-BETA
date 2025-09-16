// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.AllocationProcess
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.AR;
using PX.Objects.CT;
using PX.Objects.GL;
using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;

#nullable enable
namespace PX.Objects.PM;

[TableDashboardType]
public class AllocationProcess : PXGraph<
#nullable disable
AllocationProcess>
{
  public PXCancel<AllocationProcess.AllocationFilter> Cancel;
  public PXFilter<AllocationProcess.AllocationFilter> Filter;
  [PXFilterable(new Type[] {})]
  public PXFilteredProcessingJoin<PMTask, AllocationProcess.AllocationFilter, InnerJoin<PMProject, On<PMTask.projectID, Equal<PMProject.contractID>>, LeftJoin<PX.Objects.AR.Customer, On<PMTask.customerID, Equal<PX.Objects.AR.Customer.bAccountID>>>>, Where<PMTask.isActive, Equal<True>, And<PMProject.baseType, Equal<CTPRType.project>, And<PMTask.allocationID, IsNotNull, And2<Where<Current<AllocationProcess.AllocationFilter.allocationID>, IsNull, Or<PMTask.allocationID, Equal<Current<AllocationProcess.AllocationFilter.allocationID>>>>, And2<Where<Current<AllocationProcess.AllocationFilter.projectID>, IsNull, Or<PMTask.projectID, Equal<Current<AllocationProcess.AllocationFilter.projectID>>>>, And2<Where<Current<AllocationProcess.AllocationFilter.taskID>, IsNull, Or<PMTask.taskID, Equal<Current<AllocationProcess.AllocationFilter.taskID>>>>, And2<Where<Current<AllocationProcess.AllocationFilter.customerID>, IsNull, Or<PMTask.customerID, Equal<Current<AllocationProcess.AllocationFilter.customerID>>>>, And2<Where<Current<AllocationProcess.AllocationFilter.customerClassID>, IsNull, Or<PX.Objects.AR.Customer.customerClassID, Equal<Current<AllocationProcess.AllocationFilter.customerClassID>>>>, And2<CurrentMatch<PMProject, AccessInfo.userName>, And<PMProject.status, NotEqual<ProjectStatus.closed>>>>>>>>>>>> Items;
  public PXAction<AllocationProcess.AllocationFilter> viewProject;
  public PXAction<AllocationProcess.AllocationFilter> viewTask;
  private PMSetup setup;

  [PXSelector(typeof (Search<PMAllocation.allocationID, Where<PMAllocation.isActive, Equal<True>>>), DescriptionField = typeof (PMAllocation.description))]
  [PXUIField(DisplayName = "Allocation Rule")]
  [PXDBString(15, IsUnicode = true)]
  protected virtual void _(PX.Data.Events.CacheAttached<PMTask.allocationID> e)
  {
  }

  [Project(IsKey = true)]
  protected virtual void _(PX.Data.Events.CacheAttached<PMTask.projectID> _)
  {
  }

  public AllocationProcess()
  {
    ((PXProcessing<PMTask>) this.Items).SetProcessCaption("Allocate");
    ((PXProcessing<PMTask>) this.Items).SetProcessAllCaption("Allocate All");
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable ViewProject(PXAdapter adapter)
  {
    ProjectEntry instance = PXGraph.CreateInstance<ProjectEntry>();
    ((PXSelectBase<PMProject>) instance.Project).Current = PXResultset<PMProject>.op_Implicit(PXSelectBase<PMProject, PXSelect<PMProject, Where<PMProject.contractID, Equal<Current<PMTask.projectID>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()));
    ProjectAccountingService.NavigateToScreen((PXGraph) instance, "View Project");
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable ViewTask(PXAdapter adapter)
  {
    ProjectTaskEntry instance = PXGraph.CreateInstance<ProjectTaskEntry>();
    ((PXSelectBase<PMTask>) instance.Task).Current = PXResultset<PMTask>.op_Implicit(PXSelectBase<PMTask, PXSelect<PMTask, Where<PMTask.projectID, Equal<Current<PMTask.projectID>>, And<PMTask.taskID, Equal<Current<PMTask.taskID>>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()));
    ProjectAccountingService.NavigateToScreen((PXGraph) instance, "View Project");
    return adapter.Get();
  }

  protected virtual void AllocationFilter_RowUpdated(PXCache cache, PXRowUpdatedEventArgs e)
  {
    if (cache.ObjectsEqual<AllocationProcess.AllocationFilter.date, AllocationProcess.AllocationFilter.allocationID, AllocationProcess.AllocationFilter.customerClassID, AllocationProcess.AllocationFilter.customerID, AllocationProcess.AllocationFilter.projectID, AllocationProcess.AllocationFilter.taskID>(e.Row, e.OldRow))
      return;
    ((PXSelectBase) this.Items).Cache.Clear();
  }

  protected virtual void AllocationFilter_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: method pointer
    ((PXProcessingBase<PMTask>) this.Items).SetProcessDelegate<PMAllocator>(new PXProcessingBase<PMTask>.ProcessItemDelegate<PMAllocator>((object) new AllocationProcess.\u003C\u003Ec__DisplayClass11_0()
    {
      filter = ((PXSelectBase<AllocationProcess.AllocationFilter>) this.Filter).Current
    }, __methodptr(\u003CAllocationFilter_RowSelected\u003Eb__0)));
  }

  public bool AutoReleaseAllocation
  {
    get
    {
      if (this.setup == null)
        this.setup = PXResultset<PMSetup>.op_Implicit(PXSelectBase<PMSetup, PXSelect<PMSetup>.Config>.Select((PXGraph) this, Array.Empty<object>()));
      return this.setup.AutoReleaseAllocation.GetValueOrDefault();
    }
  }

  public static void Run(
    PMAllocator graph,
    PMTask item,
    DateTime? date,
    DateTime? fromDate,
    DateTime? toDate)
  {
    ((PXGraph) graph).Clear();
    graph.FilterStartDate = fromDate;
    graph.FilterEndDate = toDate;
    graph.PostingDate = date;
    graph.Execute(item);
    if (((PXSelectBase<PMRegister>) graph.Document).Current == null)
      throw new PXSetPropertyException("Transactions were not created during the allocation.", (PXErrorLevel) 3);
    ((PXGraph) graph).Actions.PressSave();
    PMSetup pmSetup = PXResultset<PMSetup>.op_Implicit(PXSelectBase<PMSetup, PXSelect<PMSetup>.Config>.Select((PXGraph) graph, Array.Empty<object>()));
    if (!(((PXGraph) graph).Caches[typeof (PMRegister)].Current is PMRegister current) || !pmSetup.AutoReleaseAllocation.GetValueOrDefault())
      return;
    RegisterRelease.Release(current);
  }

  [PXHidden]
  [ExcludeFromCodeCoverage]
  [Serializable]
  public class AllocationFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected DateTime? _Date;
    protected string _AllocationID;
    protected string _CustomerClassID;
    protected int? _CustomerID;
    protected int? _ProjectID;
    protected int? _TaskID;
    protected DateTime? _DateFrom;
    protected DateTime? _DateTo;

    [PXDBDate]
    [PXDefault(typeof (AccessInfo.businessDate))]
    [PXUIField]
    public virtual DateTime? Date
    {
      get => this._Date;
      set => this._Date = value;
    }

    [PXSelector(typeof (PMAllocation.allocationID), DescriptionField = typeof (PMAllocation.description))]
    [PXDBString(15, IsUnicode = true)]
    [PXUIField(DisplayName = "Allocation Rule")]
    public virtual string AllocationID
    {
      get => this._AllocationID;
      set => this._AllocationID = value;
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

    [ProjectTask(typeof (AllocationProcess.AllocationFilter.projectID))]
    public virtual int? TaskID
    {
      get => this._TaskID;
      set => this._TaskID = value;
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

    public abstract class date : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      AllocationProcess.AllocationFilter.date>
    {
    }

    public abstract class allocationID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      AllocationProcess.AllocationFilter.allocationID>
    {
    }

    public abstract class customerClassID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      AllocationProcess.AllocationFilter.customerClassID>
    {
    }

    public abstract class customerID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      AllocationProcess.AllocationFilter.customerID>
    {
    }

    public abstract class projectID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      AllocationProcess.AllocationFilter.projectID>
    {
    }

    public abstract class taskID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      AllocationProcess.AllocationFilter.taskID>
    {
    }

    public abstract class dateFrom : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      AllocationProcess.AllocationFilter.dateFrom>
    {
    }

    public abstract class dateTo : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      AllocationProcess.AllocationFilter.dateTo>
    {
    }
  }
}
