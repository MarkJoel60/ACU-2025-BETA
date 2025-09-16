// Decompiled with JetBrains decompiler
// Type: PX.Objects.RQ.RQRequisitionProcess
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.Automation;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.AP;
using PX.Objects.CR;
using PX.Objects.EP;
using PX.Objects.GL;
using PX.SM;
using PX.TM;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable enable
namespace PX.Objects.RQ;

[TableAndChartDashboardType]
public class RQRequisitionProcess : PXGraph<
#nullable disable
RQRequisitionProcess>
{
  public PXFilter<RQRequisitionProcess.RQRequisitionSelection> Filter;
  public PXFilter<PX.Objects.AP.Vendor> Vendor;
  [PXFilterable(new System.Type[] {})]
  public RQRequisitionProcess.RQRequisitionProcessing Records;
  public PXCancel<RQRequisitionProcess.RQRequisitionSelection> Cancel;
  public PXAction<RQRequisitionProcess.RQRequisitionSelection> details;

  public RQRequisitionProcess()
  {
    ((PXProcessingBase<RQRequisitionProcess.RQRequisitionOwned>) this.Records).SetSelected<RQRequisitionLine.selected>();
    ((PXProcessing<RQRequisitionProcess.RQRequisitionOwned>) this.Records).SetProcessCaption("Process");
    ((PXProcessing<RQRequisitionProcess.RQRequisitionOwned>) this.Records).SetProcessAllCaption("Process All");
  }

  [PXEditDetailButton]
  [PXUIField]
  public virtual IEnumerable Details(PXAdapter adapter)
  {
    if (((PXSelectBase<RQRequisitionProcess.RQRequisitionOwned>) this.Records).Current != null && ((PXSelectBase<RQRequisitionProcess.RQRequisitionSelection>) this.Filter).Current != null)
    {
      RQRequisitionEntry instance = PXGraph.CreateInstance<RQRequisitionEntry>();
      ((PXSelectBase<RQRequisition>) instance.Document).Current = PXResultset<RQRequisition>.op_Implicit(((PXSelectBase<RQRequisition>) instance.Document).Search<RQRequisition.reqNbr>((object) ((PXSelectBase<RQRequisitionProcess.RQRequisitionOwned>) this.Records).Current.ReqNbr, Array.Empty<object>()));
      PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, true, "View Document");
      ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
      throw requiredException;
    }
    return adapter.Get();
  }

  protected virtual void _(
    PX.Data.Events.RowSelected<RQRequisitionProcess.RQRequisitionSelection> e)
  {
    if (string.IsNullOrEmpty(e.Row?.Action))
      return;
    Dictionary<string, object> dictionary = ((PXSelectBase) this.Filter).Cache.ToDictionary((object) e.Row);
    ((PXProcessingBase<RQRequisitionProcess.RQRequisitionOwned>) this.Records).SetProcessWorkflowAction(e.Row.Action, dictionary);
  }

  public class RQRequisitionSelection : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected int? _OwnerID;
    protected int? _WorkGroupID;

    [PXDBInt]
    [CRCurrentOwnerID]
    public virtual int? CurrentOwnerID { get; set; }

    [SubordinateOwner(DisplayName = "Assigned To")]
    public virtual int? OwnerID
    {
      get => !this.MyOwner.GetValueOrDefault() ? this._OwnerID : this.CurrentOwnerID;
      set => this._OwnerID = value;
    }

    [PXDBBool]
    [PXDefault(true)]
    [PXUIField(DisplayName = "Me")]
    public virtual bool? MyOwner { get; set; }

    [PXDBInt]
    [PXUIField(DisplayName = "Workgroup")]
    [PXSelector(typeof (Search<EPCompanyTree.workGroupID, Where<EPCompanyTree.workGroupID, IsWorkgroupOrSubgroupOfContact<Current<AccessInfo.contactID>>>>), SubstituteKey = typeof (EPCompanyTree.description))]
    public virtual int? WorkGroupID
    {
      get => !this.MyWorkGroup.GetValueOrDefault() ? this._WorkGroupID : new int?();
      set => this._WorkGroupID = value;
    }

    [PXDBBool]
    [PXDefault(false)]
    [PXUIField]
    public virtual bool? MyWorkGroup { get; set; }

    [PXDBBool]
    [PXDefault(true)]
    [PXUIField]
    public virtual bool? MyEscalated { get; set; }

    [PXDBBool]
    [PXDefault(false)]
    public virtual bool? FilterSet
    {
      get
      {
        return new bool?(this.OwnerID.HasValue || this.WorkGroupID.HasValue || this.MyWorkGroup.GetValueOrDefault() || this.MyEscalated.GetValueOrDefault());
      }
    }

    [PXWorkflowMassProcessing]
    public virtual string Action { get; set; }

    [PXDBInt]
    [PXDefault(-1)]
    [PXIntList(new int[] {-1, 0, 1, 2}, new string[] {"All", "Low", "Normal", "High"})]
    [PXUIField(DisplayName = "Priority")]
    public virtual int? SelectedPriority { get; set; }

    [VendorNonEmployeeActive]
    public virtual int? VendorID { get; set; }

    [PXDBInt]
    [PXSubordinateSelector]
    [PXUIField]
    public virtual int? EmployeeID { get; set; }

    [PXDBString(60, IsUnicode = true)]
    [PXUIField]
    public virtual string Description { get; set; }

    [PXDBString(60, IsUnicode = true)]
    public virtual string DescriptionWildcard
    {
      get => this.Description == null ? (string) null : $"%{this.Description}%";
    }

    public abstract class currentOwnerID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      RQRequisitionProcess.RQRequisitionSelection.currentOwnerID>
    {
    }

    public abstract class ownerID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      RQRequisitionProcess.RQRequisitionSelection.ownerID>
    {
    }

    public abstract class myOwner : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      RQRequisitionProcess.RQRequisitionSelection.myOwner>
    {
    }

    public abstract class workGroupID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      RQRequisitionProcess.RQRequisitionSelection.workGroupID>
    {
    }

    public abstract class myWorkGroup : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      RQRequisitionProcess.RQRequisitionSelection.myWorkGroup>
    {
    }

    public abstract class myEscalated : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      RQRequisitionProcess.RQRequisitionSelection.myEscalated>
    {
    }

    public abstract class filterSet : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      RQRequisitionProcess.RQRequisitionSelection.filterSet>
    {
    }

    public abstract class action : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      RQRequisitionProcess.RQRequisitionSelection.action>
    {
    }

    public abstract class selectedPriority : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      RQRequisitionProcess.RQRequisitionSelection.selectedPriority>
    {
    }

    public abstract class vendorID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      RQRequisitionProcess.RQRequisitionSelection.vendorID>
    {
    }

    public abstract class employeeID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      RQRequisitionProcess.RQRequisitionSelection.employeeID>
    {
    }

    public abstract class description : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      RQRequisitionProcess.RQRequisitionSelection.description>
    {
    }

    public abstract class descriptionWildcard : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      RQRequisitionProcess.RQRequisitionSelection.descriptionWildcard>
    {
    }
  }

  [PXCacheName("Requisition")]
  [OwnedEscalatedFilter.Projection(typeof (RQRequisitionProcess.RQRequisitionSelection), typeof (RQRequisition.workgroupID), typeof (RQRequisition.ownerID), typeof (RQRequisition.orderDate))]
  public class RQRequisitionOwned : RQRequisition
  {
    public new abstract class reqNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      RQRequisitionProcess.RQRequisitionOwned.reqNbr>
    {
    }

    public new abstract class orderDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      RQRequisitionProcess.RQRequisitionOwned.orderDate>
    {
    }

    public new abstract class priority : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      RQRequisitionProcess.RQRequisitionOwned.priority>
    {
    }

    public new abstract class status : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      RQRequisitionProcess.RQRequisitionOwned.status>
    {
    }

    public new abstract class description : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      RQRequisitionProcess.RQRequisitionOwned.description>
    {
    }

    public new abstract class workgroupID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      RQRequisitionProcess.RQRequisitionOwned.workgroupID>
    {
    }

    public new abstract class ownerID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      RQRequisitionProcess.RQRequisitionOwned.ownerID>
    {
    }

    public new abstract class customerID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      RQRequisitionProcess.RQRequisitionOwned.customerID>
    {
    }

    public new abstract class vendorID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      RQRequisitionProcess.RQRequisitionOwned.vendorID>
    {
    }

    public new abstract class vendorLocationID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      RQRequisitionProcess.RQRequisitionOwned.vendorLocationID>
    {
    }

    public new abstract class vendorRefNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      RQRequisitionProcess.RQRequisitionOwned.vendorRefNbr>
    {
    }
  }

  public class RQRequisitionProcessing : 
    PXProcessingViewOf<RQRequisitionProcess.RQRequisitionOwned>.BasedOn<SelectFromBase<RQRequisitionProcess.RQRequisitionOwned, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<PX.Objects.AR.Customer>.On<BqlOperand<
    #nullable enable
    PX.Objects.AR.Customer.bAccountID, IBqlInt>.IsEqual<
    #nullable disable
    RQRequisitionProcess.RQRequisitionOwned.customerID>>.SingleTableOnly>, FbqlJoins.Left<PX.Objects.AP.Vendor>.On<BqlOperand<
    #nullable enable
    PX.Objects.AP.Vendor.bAccountID, IBqlInt>.IsEqual<
    #nullable disable
    RQRequisitionProcess.RQRequisitionOwned.vendorID>>.SingleTableOnly>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<
    #nullable enable
    RQRequisitionProcess.RQRequisitionSelection.selectedPriority>, 
    #nullable disable
    Equal<AllPriority>>>>>.Or<BqlOperand<Current<
    #nullable enable
    RQRequisitionProcess.RQRequisitionSelection.selectedPriority>, IBqlInt>.IsEqual<
    #nullable disable
    RQRequisitionProcess.RQRequisitionOwned.priority>>>>, And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
    #nullable enable
    PX.Objects.AR.Customer.bAccountID, 
    #nullable disable
    IsNull>>>>.Or<Match<PX.Objects.AR.Customer, BqlField<
    #nullable enable
    AccessInfo.userName, IBqlString>.FromCurrent>>>>, 
    #nullable disable
    And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
    #nullable enable
    PX.Objects.AP.Vendor.bAccountID, 
    #nullable disable
    IsNull>>>>.Or<Match<PX.Objects.AP.Vendor, BqlField<
    #nullable enable
    AccessInfo.userName, IBqlString>.FromCurrent>>>>>.And<
    #nullable disable
    WhereWorkflowActionEnabled<RQRequisitionProcess.RQRequisitionOwned, RQRequisitionProcess.RQRequisitionSelection.action>>>>.FilteredBy<RQRequisitionProcess.RQRequisitionSelection>
  {
    public RQRequisitionProcessing(PXGraph graph)
      : base(graph)
    {
      this.InitView();
    }

    public RQRequisitionProcessing(PXGraph graph, Delegate handler)
      : base(graph, handler)
    {
      this.InitView();
    }

    protected virtual void InitView()
    {
      ((PXProcessingBase<RQRequisitionProcess.RQRequisitionOwned>) this)._OuterView.WhereAndCurrent<RQRequisitionProcess.RQRequisitionSelection>(typeof (RQRequisitionProcess.RQRequisitionSelection.ownerID).Name, typeof (RQRequisitionProcess.RQRequisitionSelection.workGroupID).Name);
    }
  }
}
