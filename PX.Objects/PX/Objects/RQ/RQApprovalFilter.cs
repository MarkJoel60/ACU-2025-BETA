// Decompiled with JetBrains decompiler
// Type: PX.Objects.RQ.RQApprovalFilter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CR;
using PX.Objects.EP;
using PX.TM;
using System;

#nullable enable
namespace PX.Objects.RQ;

[PXHidden]
[Serializable]
public class RQApprovalFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _OwnerID;
  protected bool? _MyOwner;
  protected int? _WorkGroupID;
  protected bool? _MyWorkGroup;
  protected bool? _MyEscalated;
  protected int? _EmployeeID;
  protected int? _SelectedPriority;

  [PXDBInt]
  [CRCurrentOwnerID]
  public virtual int? CurrentOwnerID { get; set; }

  [SubordinateOwner(DisplayName = "Assigned To")]
  public virtual int? OwnerID
  {
    get => !this._MyOwner.GetValueOrDefault() ? this._OwnerID : this.CurrentOwnerID;
    set => this._OwnerID = value;
  }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Me")]
  public virtual bool? MyOwner
  {
    get => this._MyOwner;
    set => this._MyOwner = value;
  }

  [PXDBInt]
  [PXUIField(DisplayName = "Workgroup")]
  [PXSelector(typeof (Search<EPCompanyTree.workGroupID, Where<EPCompanyTree.workGroupID, IsWorkgroupOrSubgroupOfContact<Current<AccessInfo.contactID>>>>), SubstituteKey = typeof (EPCompanyTree.description))]
  public virtual int? WorkGroupID
  {
    get => !this._MyWorkGroup.GetValueOrDefault() ? this._WorkGroupID : new int?();
    set => this._WorkGroupID = value;
  }

  [PXDefault(false)]
  [PXDBBool]
  [PXUIField]
  public virtual bool? MyWorkGroup
  {
    get => this._MyWorkGroup;
    set => this._MyWorkGroup = value;
  }

  [PXDefault(true)]
  [PXDBBool]
  [PXUIField]
  public virtual bool? MyEscalated
  {
    get => this._MyEscalated;
    set => this._MyEscalated = value;
  }

  [PXDefault(false)]
  [PXDBBool]
  public virtual bool? FilterSet
  {
    get
    {
      return new bool?(this.OwnerID.HasValue || this.WorkGroupID.HasValue || this.MyWorkGroup.GetValueOrDefault() || this.MyEscalated.GetValueOrDefault());
    }
  }

  [PXInt]
  [PXUIField]
  [PXEPEmployeeSelector]
  public virtual int? EmployeeID
  {
    get => this._EmployeeID;
    set => this._EmployeeID = value;
  }

  [PXDBInt]
  [PXDefault(-1)]
  [PXIntList(new int[] {-1, 0, 1, 2}, new string[] {"All", "Low", "Normal", "High"})]
  [PXUIField(DisplayName = "Priority")]
  public virtual int? SelectedPriority
  {
    get => this._SelectedPriority;
    set => this._SelectedPriority = value;
  }

  public abstract class currentOwnerID : BqlType<IBqlInt, int>.Field<
  #nullable disable
  RQApprovalFilter.currentOwnerID>
  {
  }

  public abstract class ownerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RQApprovalFilter.ownerID>
  {
  }

  public abstract class myOwner : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  RQApprovalFilter.myOwner>
  {
  }

  public abstract class workGroupID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RQApprovalFilter.workGroupID>
  {
  }

  public abstract class myWorkGroup : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  RQApprovalFilter.myWorkGroup>
  {
  }

  public abstract class myEscalated : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  RQApprovalFilter.myEscalated>
  {
  }

  public abstract class filterSet : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  RQApprovalFilter.filterSet>
  {
  }

  public abstract class employeeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RQApprovalFilter.employeeID>
  {
  }

  public abstract class selectedPriority : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    RQApprovalFilter.selectedPriority>
  {
  }
}
