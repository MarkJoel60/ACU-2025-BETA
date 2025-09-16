// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.AllocationProcessByProject
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Objects.AR;
using PX.Objects.CT;
using PX.Objects.GL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

#nullable enable
namespace PX.Objects.PM;

[TableDashboardType]
public class AllocationProcessByProject : PXGraph<
#nullable disable
AllocationProcessByProject>
{
  public PXCancel<AllocationProcessByProject.AllocationFilter> Cancel;
  public PXFilter<AllocationProcessByProject.AllocationFilter> Filter;
  [PXFilterable(new Type[] {})]
  public PXFilteredProcessingJoin<PMProject, AllocationProcessByProject.AllocationFilter, LeftJoin<AllocationProcessByProject.Customer, On<AllocationProcessByProject.Customer.bAccountID, Equal<PMProject.customerID>>>, Where2<Where<Current<AllocationProcessByProject.AllocationFilter.allocationID>, IsNull, Or<PMProject.allocationID, Equal<Current<AllocationProcessByProject.AllocationFilter.allocationID>>>>, And2<Where<Current<AllocationProcessByProject.AllocationFilter.projectID>, IsNull, Or<PMProject.contractID, Equal<Current<AllocationProcessByProject.AllocationFilter.projectID>>>>, And2<Where<Current<AllocationProcessByProject.AllocationFilter.customerID>, IsNull, Or<PMProject.customerID, Equal<Current<AllocationProcessByProject.AllocationFilter.customerID>>>>, And2<Where<Current<AllocationProcessByProject.AllocationFilter.customerClassID>, IsNull, Or<AllocationProcessByProject.Customer.customerClassID, Equal<Current<AllocationProcessByProject.AllocationFilter.customerClassID>>>>, And2<Where<Current<AllocationProcessByProject.AllocationFilter.customerClassID>, IsNull, Or<AllocationProcessByProject.Customer.customerClassID, Equal<Current<AllocationProcessByProject.AllocationFilter.customerClassID>>>>, And2<Match<Current<AccessInfo.userName>>, And<PMProject.nonProject, Equal<False>, And<PMProject.baseType, Equal<CTPRType.project>, And<PMProject.status, NotEqual<ProjectStatus.closed>>>>>>>>>>> Items;
  public PXAction<AllocationProcessByProject.AllocationFilter> viewProject;
  private PMSetup setup;

  public AllocationProcessByProject()
  {
    ((PXProcessing<PMProject>) this.Items).SetProcessCaption("Allocate");
    ((PXProcessing<PMProject>) this.Items).SetProcessAllCaption("Allocate All");
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable ViewProject(PXAdapter adapter)
  {
    ProjectEntry instance = PXGraph.CreateInstance<ProjectEntry>();
    ((PXSelectBase<PMProject>) instance.Project).Current = PXResultset<PMProject>.op_Implicit(PXSelectBase<PMProject, PXSelect<PMProject, Where<PMProject.contractCD, Equal<Current<PMProject.contractCD>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()));
    ProjectAccountingService.NavigateToScreen((PXGraph) instance, "View Project");
    return adapter.Get();
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Allocation Rule")]
  protected virtual void _(PX.Data.Events.CacheAttached<PMProject.allocationID> e)
  {
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Customer Class")]
  protected virtual void _(
    PX.Data.Events.CacheAttached<AllocationProcessByProject.Customer.customerClassID> e)
  {
  }

  protected virtual void AllocationFilter_RowUpdated(PXCache cache, PXRowUpdatedEventArgs e)
  {
    if (cache.ObjectsEqual<AllocationProcessByProject.AllocationFilter.date, AllocationProcessByProject.AllocationFilter.allocationID, AllocationProcessByProject.AllocationFilter.customerClassID, AllocationProcessByProject.AllocationFilter.customerID, AllocationProcessByProject.AllocationFilter.projectID, AllocationProcessByProject.AllocationFilter.taskID>(e.Row, e.OldRow))
      return;
    ((PXSelectBase) this.Items).Cache.Clear();
  }

  protected virtual void AllocationFilter_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: method pointer
    ((PXProcessingBase<PMProject>) this.Items).SetProcessDelegate<PMAllocator>(new PXProcessingBase<PMProject>.ProcessItemDelegate<PMAllocator>((object) new AllocationProcessByProject.\u003C\u003Ec__DisplayClass9_0()
    {
      filter = ((PXSelectBase<AllocationProcessByProject.AllocationFilter>) this.Filter).Current
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
    PMProject item,
    DateTime? date,
    DateTime? fromDate,
    DateTime? toDate)
  {
    PXSelect<PMTask, Where<PMTask.projectID, Equal<Required<PMTask.projectID>>, And<PMTask.allocationID, IsNotNull, And<PMTask.isActive, Equal<True>>>>> pxSelect = new PXSelect<PMTask, Where<PMTask.projectID, Equal<Required<PMTask.projectID>>, And<PMTask.allocationID, IsNotNull, And<PMTask.isActive, Equal<True>>>>>((PXGraph) graph);
    List<PMTask> tasks = new List<PMTask>();
    object[] objArray = new object[1]
    {
      (object) item.ContractID
    };
    foreach (PXResult<PMTask> pxResult in ((PXSelectBase<PMTask>) pxSelect).Select(objArray))
    {
      PMTask pmTask = PXResult<PMTask>.op_Implicit(pxResult);
      tasks.Add(pmTask);
    }
    ((PXGraph) graph).Clear();
    graph.FilterStartDate = fromDate;
    graph.FilterEndDate = toDate;
    graph.PostingDate = date;
    graph.Execute(tasks);
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

    [PX.Objects.AR.Customer]
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

    [ProjectTask(typeof (AllocationProcessByProject.AllocationFilter.projectID))]
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
      AllocationProcessByProject.AllocationFilter.date>
    {
    }

    public abstract class allocationID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      AllocationProcessByProject.AllocationFilter.allocationID>
    {
    }

    public abstract class customerClassID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      AllocationProcessByProject.AllocationFilter.customerClassID>
    {
    }

    public abstract class customerID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      AllocationProcessByProject.AllocationFilter.customerID>
    {
    }

    public abstract class projectID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      AllocationProcessByProject.AllocationFilter.projectID>
    {
    }

    public abstract class taskID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      AllocationProcessByProject.AllocationFilter.taskID>
    {
    }

    public abstract class dateFrom : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      AllocationProcessByProject.AllocationFilter.dateFrom>
    {
    }

    public abstract class dateTo : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      AllocationProcessByProject.AllocationFilter.dateTo>
    {
    }
  }

  [PXHidden]
  [ExcludeFromCodeCoverage]
  public class PMAccountGroupFrom : PMAccountGroup
  {
    public new abstract class groupID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      AllocationProcessByProject.PMAccountGroupFrom.groupID>
    {
    }

    public new abstract class groupCD : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      AllocationProcessByProject.PMAccountGroupFrom.groupCD>
    {
    }
  }

  [PXHidden]
  [ExcludeFromCodeCoverage]
  public class PMAccountGroupTo : PMAccountGroup
  {
    public new abstract class groupID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      AllocationProcessByProject.PMAccountGroupTo.groupID>
    {
    }

    public new abstract class groupCD : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      AllocationProcessByProject.PMAccountGroupTo.groupCD>
    {
    }
  }

  [PXHidden]
  [ExcludeFromCodeCoverage]
  public class Customer : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected int? _BAccountID;
    protected string _CustomerClassID;

    [PXDBInt]
    public virtual int? BAccountID
    {
      get => this._BAccountID;
      set => this._BAccountID = value;
    }

    [PXDBString(10, IsUnicode = true)]
    public virtual string CustomerClassID
    {
      get => this._CustomerClassID;
      set => this._CustomerClassID = value;
    }

    public abstract class bAccountID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      AllocationProcessByProject.Customer.bAccountID>
    {
    }

    public abstract class customerClassID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      AllocationProcessByProject.Customer.customerClassID>
    {
    }
  }

  [PXHidden]
  [PXProjection(typeof (Select5<PMProject, InnerJoin<PMTask, On<PMProject.contractID, Equal<PMTask.projectID>, And<Where<PMTask.status, Equal<ProjectTaskStatus.active>, Or<PMTask.status, Equal<ProjectTaskStatus.completed>>>>>, InnerJoin<PMAllocation, On<PMAllocation.allocationID, Equal<PMTask.allocationID>, And<PMAllocation.isActive, Equal<True>>>, InnerJoin<PMAllocationDetail, On<PMAllocationDetail.allocationID, Equal<PMAllocation.allocationID>, And<PMAllocationDetail.selectOption, Equal<PMSelectOption.transaction>>>, LeftJoin<AllocationProcessByProject.PMAccountGroupFrom, On<AllocationProcessByProject.PMAccountGroupFrom.groupID, Equal<PMAllocationDetail.accountGroupFrom>>, LeftJoin<AllocationProcessByProject.PMAccountGroupTo, On<AllocationProcessByProject.PMAccountGroupTo.groupID, Equal<PMAllocationDetail.accountGroupTo>>, LeftJoin<PMAccountGroup, On<PMAccountGroup.groupCD, Equal<AllocationProcessByProject.PMAccountGroupFrom.groupCD>, Or<PMAccountGroup.groupCD, Equal<AllocationProcessByProject.PMAccountGroupTo.groupCD>, Or<PMAccountGroup.groupCD, Between<AllocationProcessByProject.PMAccountGroupFrom.groupCD, AllocationProcessByProject.PMAccountGroupTo.groupCD>>>>, LeftJoin<PMTran, On<PMTran.projectID, Equal<PMProject.contractID>, And<PMTran.taskID, Equal<PMTask.taskID>, And<PMTran.allocated, Equal<False>, And<PMTran.excludedFromAllocation, Equal<False>, And<PMTran.released, Equal<True>, And<PMTran.accountGroupID, Equal<PMAccountGroup.groupID>>>>>>>, LeftJoin<AllocationProcessByProject.Customer, On<PMProject.customerID, Equal<AllocationProcessByProject.Customer.bAccountID>>>>>>>>>>, Where<PMProject.baseType, Equal<CTPRType.project>, And<PMProject.isActive, Equal<True>, And<PMProject.nonProject, Equal<False>, And2<Where<PMProject.status, Equal<ProjectStatus.active>, Or<PMProject.status, Equal<ProjectStatus.completed>>>, And<PMAllocationDetail.method, Equal<PMMethod.budget>, Or<PMTran.tranID, IsNotNull>>>>>>, Aggregate<GroupBy<PMProject.contractID, GroupBy<PMProject.contractCD, GroupBy<PMProject.description, GroupBy<PMProject.allocationID, GroupBy<PMProject.customerID, GroupBy<AllocationProcessByProject.Customer.customerClassID>>>>>>>>), Persistent = false)]
  [ExcludeFromCodeCoverage]
  [Serializable]
  public class PMProjectForAllocation : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected bool? _Selected = new bool?(false);
    protected int? _ContractID;
    protected string _ContractCD;
    protected string _Description;
    private string _AllocationID;
    protected int? _CustomerID;
    protected string _CustomerClassID;

    [PXBool]
    [PXUnboundDefault(false)]
    [PXUIField(DisplayName = "Selected")]
    public virtual bool? Selected
    {
      get => this._Selected;
      set => this._Selected = value;
    }

    [PXDBInt(IsKey = true, BqlField = typeof (PMProject.contractID))]
    public virtual int? ContractID
    {
      get => this._ContractID;
      set => this._ContractID = value;
    }

    [PXDBString(IsUnicode = true, InputMask = "", BqlField = typeof (PMProject.contractCD))]
    [PXDefault]
    [PXUIField]
    [PXFieldDescription]
    public virtual string ContractCD
    {
      get => this._ContractCD;
      set => this._ContractCD = value;
    }

    [PXDBString(60, IsUnicode = true, BqlField = typeof (PMProject.description))]
    [PXUIField]
    public virtual string Description
    {
      get => this._Description;
      set => this._Description = value;
    }

    [PXSelector(typeof (Search<PMAllocation.allocationID, Where<PMAllocation.isActive, Equal<True>>>))]
    [PXUIField(DisplayName = "Allocation Rule")]
    [PXDBString(15, IsUnicode = true, BqlField = typeof (PMProject.allocationID))]
    public virtual string AllocationID
    {
      get => this._AllocationID;
      set => this._AllocationID = value;
    }

    [CustomerActive(DescriptionField = typeof (PX.Objects.AR.Customer.acctName), BqlField = typeof (PMProject.customerID))]
    public virtual int? CustomerID
    {
      get => this._CustomerID;
      set => this._CustomerID = value;
    }

    [PXDBString(10, IsUnicode = true, BqlField = typeof (AllocationProcessByProject.Customer.customerClassID))]
    [PXSelector(typeof (CustomerClass.customerClassID), DescriptionField = typeof (CustomerClass.descr), CacheGlobal = true)]
    [PXUIField(DisplayName = "Customer Class")]
    public virtual string CustomerClassID
    {
      get => this._CustomerClassID;
      set => this._CustomerClassID = value;
    }

    public abstract class selected : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      AllocationProcessByProject.PMProjectForAllocation.selected>
    {
    }

    public abstract class contractID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      AllocationProcessByProject.PMProjectForAllocation.contractID>
    {
    }

    public abstract class contractCD : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      AllocationProcessByProject.PMProjectForAllocation.contractCD>
    {
    }

    public abstract class description : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      AllocationProcessByProject.PMProjectForAllocation.description>
    {
    }

    public abstract class allocationID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      AllocationProcessByProject.PMProjectForAllocation.allocationID>
    {
    }

    public abstract class customerID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      AllocationProcessByProject.PMProjectForAllocation.customerID>
    {
    }

    public abstract class customerClassID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      AllocationProcessByProject.PMProjectForAllocation.customerClassID>
    {
    }
  }
}
