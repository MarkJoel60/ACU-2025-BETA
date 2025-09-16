// Decompiled with JetBrains decompiler
// Type: PX.Objects.RQ.RQRequest
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Data.WorkflowAPI;
using PX.Objects.AP;
using PX.Objects.CM;
using PX.Objects.Common;
using PX.Objects.Common.Bql;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.EP;
using PX.Objects.GL;
using PX.Objects.IN;
using PX.Objects.PO;
using PX.TM;
using System;

#nullable enable
namespace PX.Objects.RQ;

[PXPrimaryGraph(typeof (RQRequestEntry))]
[PXCacheName("Request")]
[PXGroupMask(typeof (LeftJoinSingleTable<PX.Objects.AP.Vendor, On<PX.Objects.AP.Vendor.bAccountID, Equal<RQRequest.vendorID>, And<Match<PX.Objects.AP.Vendor, Current<AccessInfo.userName>>>>>), WhereRestriction = typeof (Where<PX.Objects.AP.Vendor.bAccountID, IsNotNull, Or<RQRequest.vendorID, IsNull>>))]
public class RQRequest : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage, IAssign, INotable
{
  protected bool? _Selected = new bool?(false);
  protected int? _BranchID;
  protected 
  #nullable disable
  string _OrderNbr;
  protected DateTime? _OrderDate;
  protected string _ReqClassID;
  protected int? _Priority;
  protected string _Status;
  protected string _Description;
  protected string _FinPeriodID;
  protected bool? _Hold;
  protected bool? _Approved;
  protected bool? _Rejected = new bool?(false);
  protected bool? _Cancelled;
  protected Guid? _NoteID;
  protected bool? _VendorHidden;
  protected bool? _CustomerRequest;
  protected bool? _BudgetValidation;
  protected int? _VendorID;
  protected int? _VendorLocationID;
  protected int? _WorkgroupID;
  protected int? _OwnerID;
  protected int? _ApprovalWorkgroupID;
  protected int? _ApprovalOwnerID;
  protected bool? _CheckBudget;
  protected string _ShipDestType;
  protected int? _SiteID;
  protected int? _ShipToBAccountID;
  protected int? _ShipToLocationID;
  protected int? _ShipAddressID;
  protected int? _ShipContactID;
  protected int? _RemitAddressID;
  protected int? _RemitContactID;
  protected int? _EmployeeID;
  protected string _DepartmentID;
  protected int? _LocationID;
  protected string _CuryID;
  protected long? _CuryInfoID;
  protected Decimal? _OpenOrderQty;
  protected Decimal? _CuryEstExtCostTotal;
  protected Decimal? _EstExtCostTotal;
  protected string _Purpose;
  protected int? _LineCntr;
  protected bool? _IsOverbudget;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Selected")]
  public virtual bool? Selected
  {
    get => this._Selected;
    set => this._Selected = value;
  }

  [Branch(null, null, true, true, true)]
  public virtual int? BranchID
  {
    get => this._BranchID;
    set => this._BranchID = value;
  }

  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXDefault]
  [PXUIField]
  [AutoNumber(typeof (RQSetup.requestNumberingID), typeof (RQRequest.orderDate))]
  [PXSelector(typeof (Search2<RQRequest.orderNbr, LeftJoinSingleTable<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<RQRequest.employeeID>>>, Where<PX.Objects.AR.Customer.bAccountID, IsNull, Or<Match<PX.Objects.AR.Customer, Current<AccessInfo.userName>>>>>), new System.Type[] {typeof (RQRequest.orderNbr), typeof (RQRequest.orderDate), typeof (RQRequest.status), typeof (RQRequest.employeeID), typeof (RQRequest.departmentID), typeof (RQRequest.locationID)}, Filterable = true)]
  [PXFieldDescription]
  public virtual string OrderNbr
  {
    get => this._OrderNbr;
    set => this._OrderNbr = value;
  }

  [PXDBDate]
  [PXDefault(typeof (AccessInfo.businessDate))]
  [PXUIField]
  public virtual DateTime? OrderDate
  {
    get => this._OrderDate;
    set => this._OrderDate = value;
  }

  [PXDBString(10, IsUnicode = true)]
  [PXDefault(typeof (RQSetup.defaultReqClassID))]
  [PXUIField]
  [PXSelector(typeof (RQRequestClass.reqClassID), DescriptionField = typeof (RQRequestClass.descr))]
  public virtual string ReqClassID
  {
    get => this._ReqClassID;
    set => this._ReqClassID = value;
  }

  [PXDBInt]
  [PXUIField(DisplayName = "Priority")]
  [PXDefault(1)]
  [PXIntList(new int[] {0, 1, 2}, new string[] {"Low", "Normal", "High"})]
  public virtual int? Priority
  {
    get => this._Priority;
    set => this._Priority = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXDefault("H")]
  [PXUIField]
  [RQRequestStatus.List]
  public virtual string Status
  {
    get => this._Status;
    set => this._Status = value;
  }

  [PXDBString(60, IsUnicode = true)]
  [PXUIField]
  public virtual string Description
  {
    get => this._Description;
    set => this._Description = value;
  }

  [APOpenPeriod(typeof (RQRequest.orderDate), null, null, null, null, null, true, false, FinPeriodSelectorAttribute.SelectionModesWithRestrictions.Undefined, null, null, ValidatePeriod = PeriodValidation.Nothing)]
  [PXUIField]
  public virtual string FinPeriodID
  {
    get => this._FinPeriodID;
    set => this._FinPeriodID = value;
  }

  [PXDBBool]
  [PXUIField]
  [PXDefault(true)]
  public virtual bool? Hold
  {
    get => this._Hold;
    set => this._Hold = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? Approved
  {
    get => this._Approved;
    set => this._Approved = value;
  }

  [PXBool]
  [PXDefault(false)]
  [PXUIField]
  public bool? Rejected
  {
    get => this._Rejected;
    set => this._Rejected = value;
  }

  [PXDBBool]
  [PXUIField]
  [PXDefault(false)]
  public virtual bool? Cancelled
  {
    get => this._Cancelled;
    set => this._Cancelled = value;
  }

  [PXSearchable(512 /*0x0200*/, "Request: {0} - {2}", new System.Type[] {typeof (RQRequest.orderNbr), typeof (RQRequest.employeeID), typeof (PX.Objects.EP.EPEmployee.acctName)}, new System.Type[] {typeof (RQRequest.reqClassID), typeof (RQRequest.description), typeof (RQRequest.departmentID)}, NumberFields = new System.Type[] {typeof (RQRequest.orderNbr)}, Line1Format = "{0:d}{1}{2}{3}{4}", Line1Fields = new System.Type[] {typeof (RQRequest.orderDate), typeof (RQRequest.status), typeof (RQRequest.reqClassID), typeof (RQRequest.departmentID), typeof (RQRequest.priority)}, Line2Format = "{0}", Line2Fields = new System.Type[] {typeof (RQRequest.description)})]
  [PXNote]
  public virtual Guid? NoteID
  {
    get => this._NoteID;
    set => this._NoteID = value;
  }

  [PXBool]
  [PXUIField]
  [PXDefault(false)]
  public virtual bool? VendorHidden
  {
    get => this._VendorHidden;
    set => this._VendorHidden = value;
  }

  [PXBool]
  [PXUIField]
  [PXDefault(false)]
  public virtual bool? CustomerRequest
  {
    get => this._CustomerRequest;
    set => this._CustomerRequest = value;
  }

  [PXBool]
  [PXUIField]
  [PXDefault(false)]
  public virtual bool? BudgetValidation
  {
    get => this._BudgetValidation;
    set => this._BudgetValidation = value;
  }

  [PXDefault]
  [VendorNonEmployeeActive]
  public virtual int? VendorID
  {
    get => this._VendorID;
    set => this._VendorID = value;
  }

  [PX.Objects.CS.LocationID(typeof (Where<PX.Objects.CR.Location.bAccountID, Equal<Current<RQRequest.vendorID>>>))]
  [PXDefault(typeof (Search<PX.Objects.AP.Vendor.defLocationID, Where<PX.Objects.AP.Vendor.bAccountID, Equal<Current<RQRequest.vendorID>>>>))]
  public virtual int? VendorLocationID
  {
    get => this._VendorLocationID;
    set => this._VendorLocationID = value;
  }

  [PXDBInt]
  [PXUIField]
  [PXSubordinateGroupSelector]
  public virtual int? WorkgroupID
  {
    get => this._WorkgroupID;
    set => this._WorkgroupID = value;
  }

  [PXDefault(typeof (PX.Objects.AP.Vendor.ownerID))]
  [Owner(typeof (RQRequisition.workgroupID))]
  public virtual int? OwnerID
  {
    get => this._OwnerID;
    set => this._OwnerID = value;
  }

  [PXInt]
  [PXSelector(typeof (Search<EPCompanyTree.workGroupID>), SubstituteKey = typeof (EPCompanyTree.description))]
  [PXUIField(DisplayName = "Approval Workgroup ID", Enabled = false)]
  public virtual int? ApprovalWorkgroupID
  {
    get => this._ApprovalWorkgroupID;
    set => this._ApprovalWorkgroupID = value;
  }

  [Owner(IsDBField = false, DisplayName = "Approver", Enabled = false)]
  public virtual int? ApprovalOwnerID
  {
    get => this._ApprovalOwnerID;
    set => this._ApprovalOwnerID = value;
  }

  [PXDBBool]
  [PXUIField]
  [PXDefault(false)]
  public virtual bool? CheckBudget
  {
    get => this._CheckBudget;
    set => this._CheckBudget = value;
  }

  [PXDBString(1, IsFixed = true)]
  [POShippingDestination.List]
  [PXDefault("L")]
  [PXUIField(DisplayName = "Shipping Destination Type")]
  public virtual string ShipDestType
  {
    get => this._ShipDestType;
    set => this._ShipDestType = value;
  }

  [Site(DescriptionField = typeof (INSite.descr))]
  [PXDefault(null, typeof (Coalesce<Search<LocationBranchSettings.vSiteID, Where<Current<RQRequest.shipDestType>, Equal<POShippingDestination.site>, And<LocationBranchSettings.bAccountID, Equal<Current<RQRequest.vendorID>>, And<LocationBranchSettings.locationID, Equal<Current<RQRequest.vendorLocationID>>, And<LocationBranchSettings.branchID, Equal<Current<RQRequest.branchID>>>>>>>, Search<PX.Objects.CR.Location.vSiteID, Where<Current<RQRequest.shipDestType>, Equal<POShippingDestination.site>, And<PX.Objects.CR.Location.bAccountID, Equal<Current<RQRequest.vendorID>>, And<PX.Objects.CR.Location.locationID, Equal<Current<RQRequest.vendorLocationID>>>>>>, Search<INSite.siteID, Where<Current<RQRequest.shipDestType>, Equal<POShippingDestination.site>, And<INSite.active, Equal<True>, And<INSite.siteID, NotEqual<SiteAnyAttribute.transitSiteID>>>>>>))]
  [PXForeignReference(typeof (RQRequest.FK.Site))]
  [InterBranchRestrictor(typeof (Where<SameOrganizationBranch<INSite.branchID, Current<RQRequest.branchID>>>))]
  public virtual int? SiteID
  {
    get => this._SiteID;
    set => this._SiteID = value;
  }

  [PXString(150, IsUnicode = true)]
  public virtual string SiteIdErrorMessage { get; set; }

  [PXDBInt]
  [PXSelector(typeof (Search2<BAccount2.bAccountID, LeftJoin<PX.Objects.AP.Vendor, On<PX.Objects.AP.Vendor.bAccountID, Equal<BAccount2.bAccountID>, And<PX.Objects.AP.Vendor.type, NotEqual<BAccountType.employeeType>>>, LeftJoin<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<BAccount2.bAccountID>>, LeftJoin<PX.Objects.GL.Branch, On<PX.Objects.GL.Branch.bAccountID, Equal<BAccount2.bAccountID>>>>>, Where<PX.Objects.AP.Vendor.bAccountID, IsNotNull, And<Optional<RQRequest.shipDestType>, Equal<POShippingDestination.vendor>, Or<Where<PX.Objects.GL.Branch.bAccountID, IsNotNull, And<Optional<RQRequest.shipDestType>, Equal<POShippingDestination.company>, Or<Where<PX.Objects.AR.Customer.bAccountID, IsNotNull, And<Optional<RQRequest.shipDestType>, Equal<POShippingDestination.customer>>>>>>>>>>), new System.Type[] {typeof (PX.Objects.CR.BAccount.acctCD), typeof (PX.Objects.CR.BAccount.acctName), typeof (PX.Objects.CR.BAccount.type), typeof (PX.Objects.CR.BAccount.acctReferenceNbr), typeof (PX.Objects.CR.BAccount.parentBAccountID)}, SubstituteKey = typeof (PX.Objects.CR.BAccount.acctCD), DescriptionField = typeof (PX.Objects.CR.BAccount.acctName))]
  [PXUIField(DisplayName = "Ship To")]
  [PXDefault(null, typeof (Search<PX.Objects.GL.Branch.bAccountID, Where<PX.SM.Branch.branchID, Equal<Current<AccessInfo.branchID>>, And<Optional<RQRequest.shipDestType>, Equal<POShippingDestination.company>>>>))]
  [PXForeignReference(typeof (Field<RQRequest.shipToBAccountID>.IsRelatedTo<PX.Objects.CR.BAccount.bAccountID>))]
  public virtual int? ShipToBAccountID
  {
    get => this._ShipToBAccountID;
    set => this._ShipToBAccountID = value;
  }

  [PX.Objects.CS.LocationID(typeof (Where<PX.Objects.CR.Location.bAccountID, Equal<Current<RQRequest.shipToBAccountID>>>), DescriptionField = typeof (PX.Objects.CR.Location.descr))]
  [PXDefault(null, typeof (Search<BAccount2.defLocationID, Where<BAccount2.bAccountID, Equal<Current<RQRequest.shipToBAccountID>>>>))]
  [PXUIField(DisplayName = "Shipping Location")]
  public virtual int? ShipToLocationID
  {
    get => this._ShipToLocationID;
    set => this._ShipToLocationID = value;
  }

  [PXDBInt]
  [POShipAddress(typeof (Select2<PX.Objects.CR.Address, LeftJoin<PX.Objects.CR.Standalone.Location, On<PX.Objects.CR.Address.bAccountID, Equal<PX.Objects.CR.Standalone.Location.bAccountID>, And<PX.Objects.CR.Address.addressID, Equal<PX.Objects.CR.Standalone.Location.defAddressID>, And<Current<RQRequest.shipDestType>, NotEqual<POShippingDestination.site>, And<PX.Objects.CR.Standalone.Location.bAccountID, Equal<Current<RQRequest.shipToBAccountID>>, And<PX.Objects.CR.Standalone.Location.locationID, Equal<Current<RQRequest.shipToLocationID>>>>>>>, LeftJoin<POShipAddress, On<POShipAddress.bAccountID, Equal<PX.Objects.CR.Address.bAccountID>, And<POShipAddress.bAccountAddressID, Equal<PX.Objects.CR.Address.addressID>, And<POShipAddress.revisionID, Equal<PX.Objects.CR.Address.revisionID>, And<POShipAddress.isDefaultAddress, Equal<boolTrue>>>>>>>, Where<PX.Objects.CR.Standalone.Location.locationCD, IsNotNull>>))]
  [PXUIField]
  public virtual int? ShipAddressID
  {
    get => this._ShipAddressID;
    set => this._ShipAddressID = value;
  }

  [PXDBInt]
  [POShipContact(typeof (Select2<PX.Objects.CR.Contact, LeftJoin<PX.Objects.CR.Standalone.Location, On<PX.Objects.CR.Contact.bAccountID, Equal<PX.Objects.CR.Standalone.Location.bAccountID>, And<PX.Objects.CR.Contact.contactID, Equal<PX.Objects.CR.Standalone.Location.defContactID>, And<Current<RQRequest.shipDestType>, NotEqual<POShippingDestination.site>, And<PX.Objects.CR.Standalone.Location.bAccountID, Equal<Current<RQRequest.shipToBAccountID>>, And<PX.Objects.CR.Standalone.Location.locationID, Equal<Current<RQRequest.shipToLocationID>>>>>>>, LeftJoin<POShipContact, On<POShipContact.bAccountID, Equal<PX.Objects.CR.Contact.bAccountID>, And<POShipContact.bAccountContactID, Equal<PX.Objects.CR.Contact.contactID>, And<POShipContact.revisionID, Equal<PX.Objects.CR.Contact.revisionID>, And<POShipContact.isDefaultContact, Equal<boolTrue>>>>>>>, Where<PX.Objects.CR.Standalone.Location.locationCD, IsNotNull>>))]
  [PXUIField]
  public virtual int? ShipContactID
  {
    get => this._ShipContactID;
    set => this._ShipContactID = value;
  }

  [PXDBInt]
  [PORemitAddress(typeof (Select2<BAccount2, InnerJoin<PX.Objects.CR.Location, On<PX.Objects.CR.Location.bAccountID, Equal<BAccount2.bAccountID>>, InnerJoin<PX.Objects.CR.Address, On<PX.Objects.CR.Address.bAccountID, Equal<PX.Objects.CR.Location.bAccountID>, And<PX.Objects.CR.Address.addressID, Equal<PX.Objects.CR.Location.defAddressID>>>, LeftJoin<PX.Objects.PO.PORemitAddress, On<PX.Objects.PO.PORemitAddress.bAccountID, Equal<PX.Objects.CR.Address.bAccountID>, And<PX.Objects.PO.PORemitAddress.bAccountAddressID, Equal<PX.Objects.CR.Address.addressID>, And<PX.Objects.PO.PORemitAddress.revisionID, Equal<PX.Objects.CR.Address.revisionID>, And<PX.Objects.PO.PORemitAddress.isDefaultAddress, Equal<boolTrue>>>>>>>>, Where<PX.Objects.CR.Location.bAccountID, Equal<Current<RQRequest.vendorID>>, And<PX.Objects.CR.Location.locationID, Equal<Current<RQRequest.vendorLocationID>>>>>), Required = false)]
  [PXUIField]
  public virtual int? RemitAddressID
  {
    get => this._RemitAddressID;
    set => this._RemitAddressID = value;
  }

  [PXDBInt]
  [PORemitContact(typeof (Select2<PX.Objects.CR.Location, InnerJoin<PX.Objects.CR.Contact, On<PX.Objects.CR.Contact.bAccountID, Equal<PX.Objects.CR.Location.bAccountID>, And<PX.Objects.CR.Contact.contactID, Equal<PX.Objects.CR.Location.defContactID>>>, LeftJoin<PX.Objects.PO.PORemitContact, On<PX.Objects.PO.PORemitContact.bAccountID, Equal<PX.Objects.CR.Contact.bAccountID>, And<PX.Objects.PO.PORemitContact.bAccountContactID, Equal<PX.Objects.CR.Contact.contactID>, And<PX.Objects.PO.PORemitContact.revisionID, Equal<PX.Objects.CR.Contact.revisionID>, And<PX.Objects.PO.PORemitContact.isDefaultContact, Equal<boolTrue>>>>>>>, Where<PX.Objects.CR.Location.bAccountID, Equal<Current<RQRequest.vendorID>>, And<PX.Objects.CR.Location.locationID, Equal<Current<RQRequest.vendorLocationID>>>>>), Required = false)]
  public virtual int? RemitContactID
  {
    get => this._RemitContactID;
    set => this._RemitContactID = value;
  }

  [PXDBInt]
  [PXDefault(typeof (Search2<PX.Objects.EP.EPEmployee.bAccountID, InnerJoin<RQRequestClass, On<RQRequestClass.reqClassID, Equal<Current<RQRequest.reqClassID>>, And<RQRequestClass.customerRequest, NotEqual<boolTrue>>>>, Where<PX.Objects.EP.EPEmployee.userID, Equal<Current<AccessInfo.userID>>>>))]
  [RQRequesterSelector(typeof (RQRequest.reqClassID))]
  [PXUIField]
  [PXForeignReference(typeof (Field<RQRequest.employeeID>.IsRelatedTo<PX.Objects.CR.BAccount.bAccountID>))]
  public virtual int? EmployeeID
  {
    get => this._EmployeeID;
    set => this._EmployeeID = value;
  }

  [PXDBString(10, IsUnicode = true)]
  [PXDefault(typeof (Search<PX.Objects.EP.EPEmployee.departmentID, Where<PX.Objects.EP.EPEmployee.bAccountID, Equal<Current<RQRequest.employeeID>>>>))]
  [PXSelector(typeof (EPDepartment.departmentID), DescriptionField = typeof (EPDepartment.description))]
  [PXUIField]
  public virtual string DepartmentID
  {
    get => this._DepartmentID;
    set => this._DepartmentID = value;
  }

  [PXDefault(typeof (Search<BAccountR.defLocationID, Where<BAccountR.bAccountID, Equal<Current<RQRequest.employeeID>>>>))]
  [PX.Objects.CS.LocationID(typeof (Where<PX.Objects.CR.Location.bAccountID, Equal<Current<RQRequest.employeeID>>>))]
  public int? LocationID
  {
    get => this._LocationID;
    set => this._LocationID = value;
  }

  [PXDBString(5, IsUnicode = true, InputMask = ">LLLLL")]
  [PXUIField]
  [PXDefault(typeof (Current<AccessInfo.baseCuryID>))]
  [PXSelector(typeof (PX.Objects.CM.Currency.curyID))]
  public virtual string CuryID
  {
    get => this._CuryID;
    set => this._CuryID = value;
  }

  [PXDBLong]
  [CurrencyInfo(ModuleCode = "PO")]
  public virtual long? CuryInfoID
  {
    get => this._CuryInfoID;
    set => this._CuryInfoID = value;
  }

  [PXDBQuantity]
  [PXUIField(DisplayName = "Open Qty.", Enabled = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? OpenOrderQty
  {
    get => this._OpenOrderQty;
    set => this._OpenOrderQty = value;
  }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBCurrency(typeof (RQRequest.curyInfoID), typeof (RQRequest.estExtCostTotal))]
  [PXUIField]
  public virtual Decimal? CuryEstExtCostTotal
  {
    get => this._CuryEstExtCostTotal;
    set => this._CuryEstExtCostTotal = value;
  }

  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? EstExtCostTotal
  {
    get => this._EstExtCostTotal;
    set => this._EstExtCostTotal = value;
  }

  [PXDBString(100, IsUnicode = true)]
  [PXUIField(DisplayName = "Purpose")]
  public virtual string Purpose
  {
    get => this._Purpose;
    set => this._Purpose = value;
  }

  [PXDBInt]
  [PXDefault(0)]
  public virtual int? LineCntr
  {
    get => this._LineCntr;
    set => this._LineCntr = value;
  }

  [PXDBBool]
  [PXDefault(typeof (False))]
  [PXUIField(DisplayName = "Is Over Budget", IsReadOnly = true)]
  public virtual bool? IsOverbudget
  {
    get => this._IsOverbudget;
    set => this._IsOverbudget = value;
  }

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID
  {
    get => this._CreatedByID;
    set => this._CreatedByID = value;
  }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID
  {
    get => this._CreatedByScreenID;
    set => this._CreatedByScreenID = value;
  }

  [PXDBCreatedDateTime]
  [PXUIField(DisplayName = "Created On", Enabled = false, IsReadOnly = true)]
  public virtual DateTime? CreatedDateTime
  {
    get => this._CreatedDateTime;
    set => this._CreatedDateTime = value;
  }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID
  {
    get => this._LastModifiedByID;
    set => this._LastModifiedByID = value;
  }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID
  {
    get => this._LastModifiedByScreenID;
    set => this._LastModifiedByScreenID = value;
  }

  [PXDBLastModifiedDateTime]
  [PXUIField(DisplayName = "Last Modified On", Enabled = false, IsReadOnly = true)]
  public virtual DateTime? LastModifiedDateTime
  {
    get => this._LastModifiedDateTime;
    set => this._LastModifiedDateTime = value;
  }

  int? IAssign.WorkgroupID
  {
    get => this.ApprovalWorkgroupID;
    set => this.ApprovalWorkgroupID = value;
  }

  int? IAssign.OwnerID
  {
    get => this.ApprovalOwnerID;
    set => this.ApprovalOwnerID = value;
  }

  public class PK : PrimaryKeyOf<RQRequest>.By<RQRequest.orderNbr>
  {
    public static RQRequest Find(PXGraph graph, string orderNbr, PKFindOptions options = 0)
    {
      return (RQRequest) PrimaryKeyOf<RQRequest>.By<RQRequest.orderNbr>.FindBy(graph, (object) orderNbr, options);
    }
  }

  public static class FK
  {
    public class RequestClass : 
      PrimaryKeyOf<RQRequestClass>.By<RQRequestClass.reqClassID>.ForeignKeyOf<RQRequest>.By<RQRequest.reqClassID>
    {
    }

    public class Site : 
      PrimaryKeyOf<INSite>.By<INSite.siteID>.ForeignKeyOf<RQRequest>.By<RQRequest.siteID>
    {
    }
  }

  public class Events : PXEntityEventBase<RQRequest>.Container<RQRequest.Events>
  {
    public PXEntityEvent<RQRequest> OpenOrderQtyChanged;
  }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  RQRequest.selected>
  {
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RQRequest.branchID>
  {
  }

  public abstract class orderNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RQRequest.orderNbr>
  {
  }

  public abstract class orderDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  RQRequest.orderDate>
  {
  }

  public abstract class reqClassID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RQRequest.reqClassID>
  {
  }

  public abstract class priority : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RQRequest.priority>
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RQRequest.status>
  {
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RQRequest.description>
  {
  }

  public abstract class finPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RQRequest.finPeriodID>
  {
  }

  public abstract class hold : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  RQRequest.hold>
  {
  }

  public abstract class approved : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  RQRequest.approved>
  {
  }

  public abstract class rejected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  RQRequest.rejected>
  {
  }

  public abstract class cancelled : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  RQRequest.cancelled>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  RQRequest.noteID>
  {
  }

  public abstract class vendorHidden : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  RQRequest.vendorHidden>
  {
  }

  public abstract class customerRequest : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  RQRequest.customerRequest>
  {
  }

  public abstract class budgetValidation : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  RQRequest.budgetValidation>
  {
  }

  public abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RQRequest.vendorID>
  {
  }

  public abstract class vendorLocationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RQRequest.vendorLocationID>
  {
  }

  public abstract class workgroupID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RQRequest.workgroupID>
  {
  }

  public abstract class ownerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RQRequest.ownerID>
  {
  }

  public abstract class approvalWorkgroupID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    RQRequest.approvalWorkgroupID>
  {
  }

  public abstract class approvalOwnerID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  RQRequest.approvalOwnerID>
  {
  }

  public abstract class checkBudget : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  RQRequest.checkBudget>
  {
  }

  public abstract class shipDestType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RQRequest.shipDestType>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RQRequest.siteID>
  {
  }

  public abstract class siteIdErrorMessage : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RQRequest.siteIdErrorMessage>
  {
  }

  public abstract class shipToBAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RQRequest.shipToBAccountID>
  {
  }

  public abstract class shipToLocationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RQRequest.shipToLocationID>
  {
  }

  public abstract class shipAddressID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RQRequest.shipAddressID>
  {
  }

  public abstract class shipContactID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RQRequest.shipContactID>
  {
  }

  public abstract class remitAddressID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RQRequest.remitAddressID>
  {
  }

  public abstract class remitContactID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RQRequest.remitContactID>
  {
  }

  public abstract class employeeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RQRequest.employeeID>
  {
  }

  public abstract class departmentID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RQRequest.departmentID>
  {
  }

  public abstract class locationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RQRequest.locationID>
  {
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RQRequest.curyID>
  {
  }

  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  RQRequest.curyInfoID>
  {
  }

  public abstract class openOrderQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  RQRequest.openOrderQty>
  {
  }

  public abstract class curyEstExtCostTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    RQRequest.curyEstExtCostTotal>
  {
  }

  public abstract class estExtCostTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    RQRequest.estExtCostTotal>
  {
  }

  public abstract class purpose : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RQRequest.purpose>
  {
  }

  public abstract class lineCntr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RQRequest.lineCntr>
  {
  }

  public abstract class isOverbudget : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  RQRequest.isOverbudget>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  RQRequest.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  RQRequest.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RQRequest.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    RQRequest.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  RQRequest.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RQRequest.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    RQRequest.lastModifiedDateTime>
  {
  }
}
