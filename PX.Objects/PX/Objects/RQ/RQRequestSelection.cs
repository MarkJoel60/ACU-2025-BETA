// Decompiled with JetBrains decompiler
// Type: PX.Objects.RQ.RQRequestSelection
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.AP;
using PX.Objects.CR;
using PX.Objects.EP;
using PX.TM;
using System;

#nullable enable
namespace PX.Objects.RQ;

[Serializable]
public class RQRequestSelection : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _OwnerID;
  protected bool? _MyOwner;
  protected int? _WorkGroupID;
  protected bool? _MyWorkGroup;
  protected bool? _MyEscalated;
  protected 
  #nullable disable
  string _ReqClassID;
  protected int? _SelectedPriority;
  protected int? _VendorID;
  protected int? _EmployeeID;
  protected string _DepartmentID;
  protected int? _LocationID;
  protected string _Description;
  protected string _DescriptionWildcard;
  protected bool? _AddExists;

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

  [PXDBString(10, IsUnicode = true)]
  [PXUIField]
  [PXSelector(typeof (RQRequestClass.reqClassID), DescriptionField = typeof (RQRequestClass.descr))]
  public virtual string ReqClassID
  {
    get => this._ReqClassID;
    set => this._ReqClassID = value;
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

  [VendorNonEmployeeActive]
  public virtual int? VendorID
  {
    get => this._VendorID;
    set => this._VendorID = value;
  }

  [PXDBInt]
  [RQRequesterSelector(typeof (RQRequestSelection.reqClassID))]
  [PXUIField]
  public virtual int? EmployeeID
  {
    get => this._EmployeeID;
    set => this._EmployeeID = value;
  }

  [PXDBString(10, IsUnicode = true)]
  [PXSelector(typeof (EPDepartment.departmentID), DescriptionField = typeof (EPDepartment.description))]
  [PXUIField]
  public virtual string DepartmentID
  {
    get => this._DepartmentID;
    set => this._DepartmentID = value;
  }

  [PXDefault(typeof (Search<BAccountR.defLocationID, Where<BAccountR.bAccountID, Equal<Current<RQRequestSelection.employeeID>>>>))]
  [PX.Objects.CS.LocationID(typeof (Where<PX.Objects.CR.Location.bAccountID, Equal<Current<RQRequestSelection.employeeID>>>))]
  [PXFormula(typeof (Default<RQRequestSelection.employeeID>))]
  public int? LocationID
  {
    get => this._LocationID;
    set => this._LocationID = value;
  }

  [PXDBString(60, IsUnicode = true)]
  [PXUIField]
  public virtual string Description
  {
    get => this._Description;
    set => this._Description = value;
  }

  [PXDBString(60, IsUnicode = true)]
  public virtual string DescriptionWildcard
  {
    get => this._Description == null ? (string) null : $"%{this._Description}%";
  }

  [PXDBBool]
  [PXUIField]
  [PXDefault(false, typeof (Search<RQSetup.requisitionMergeLines>))]
  public virtual bool? AddExists
  {
    get => this._AddExists;
    set => this._AddExists = value;
  }

  public abstract class currentOwnerID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    RQRequestSelection.currentOwnerID>
  {
  }

  public abstract class ownerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RQRequestSelection.ownerID>
  {
  }

  public abstract class myOwner : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  RQRequestSelection.myOwner>
  {
  }

  public abstract class workGroupID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RQRequestSelection.workGroupID>
  {
  }

  public abstract class myWorkGroup : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  RQRequestSelection.myWorkGroup>
  {
  }

  public abstract class myEscalated : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  RQRequestSelection.myEscalated>
  {
  }

  public abstract class filterSet : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  RQRequestSelection.filterSet>
  {
  }

  public abstract class reqClassID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RQRequestSelection.reqClassID>
  {
  }

  public abstract class selectedPriority : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    RQRequestSelection.selectedPriority>
  {
  }

  public abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RQRequestSelection.vendorID>
  {
  }

  public abstract class employeeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RQRequestSelection.employeeID>
  {
  }

  public abstract class departmentID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RQRequestSelection.departmentID>
  {
  }

  public abstract class locationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RQRequestSelection.locationID>
  {
  }

  public abstract class description : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RQRequestSelection.description>
  {
  }

  public abstract class descriptionWildcard : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RQRequestSelection.descriptionWildcard>
  {
  }

  public abstract class addExists : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  RQRequestSelection.addExists>
  {
  }
}
