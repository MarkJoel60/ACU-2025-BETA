// Decompiled with JetBrains decompiler
// Type: PX.Objects.RQ.RQRequestLineOwned
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using PX.Objects.IN;
using PX.Objects.PO;
using PX.TM;
using System;

#nullable enable
namespace PX.Objects.RQ;

[OwnedEscalatedFilter.Projection(typeof (RQRequestSelection), typeof (RQRequestLine), typeof (InnerJoin<RQRequest, On<RQRequest.orderNbr, Equal<RQRequestLine.orderNbr>, And<RQRequest.status, Equal<RQRequestStatus.open>, And<RQRequestLine.openQty, Greater<decimal0>>>>, InnerJoin<RQRequestClass, On<RQRequestClass.reqClassID, Equal<RQRequest.reqClassID>>>>), null, typeof (RQRequest.workgroupID), typeof (RQRequest.ownerID), typeof (RQRequest.orderDate))]
[PXGroupMask(typeof (InnerJoinSingleTable<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<RQRequestLineOwned.employeeID>, And<Match<PX.Objects.AR.Customer, Current<AccessInfo.userName>>>>>))]
[Serializable]
public class RQRequestLineOwned : RQRequestLine
{
  protected 
  #nullable disable
  string _ReqClassID;
  protected int? _Priority;
  protected DateTime? _OrderDate;
  protected bool? _CustomerRequest;
  protected int? _EmployeeID;
  protected int? _LocationID;
  protected string _ShipDestType;

  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = "")]
  [PXSelector(typeof (Search<RQRequest.orderNbr>))]
  [PXUIField(DisplayName = "Ref. Nbr.")]
  public override string OrderNbr
  {
    get => this._OrderNbr;
    set => this._OrderNbr = value;
  }

  [PXDBString(10, IsUnicode = true, BqlField = typeof (RQRequest.reqClassID))]
  [PXDefault(typeof (RQSetup.defaultReqClassID))]
  [PXUIField]
  [PXSelector(typeof (RQRequestClass.reqClassID), DescriptionField = typeof (RQRequestClass.descr))]
  public virtual string ReqClassID
  {
    get => this._ReqClassID;
    set => this._ReqClassID = value;
  }

  [PXDBInt(BqlField = typeof (RQRequest.priority))]
  [PXUIField]
  [PXDefault(1)]
  [PXIntList(new int[] {0, 1, 2}, new string[] {"Low", "Normal", "High"})]
  public virtual int? Priority
  {
    get => this._Priority;
    set => this._Priority = value;
  }

  [PXDBDate(BqlField = typeof (RQRequest.orderDate))]
  [PXDefault(typeof (AccessInfo.businessDate))]
  [PXUIField]
  public virtual DateTime? OrderDate
  {
    get => this._OrderDate;
    set => this._OrderDate = value;
  }

  [PXDBBool(BqlField = typeof (RQRequestClass.customerRequest))]
  [PXUIField]
  [PXDefault(false)]
  public virtual bool? CustomerRequest
  {
    get => this._CustomerRequest;
    set => this._CustomerRequest = value;
  }

  [PXUIField]
  [PXDBInt(BqlField = typeof (RQRequest.employeeID))]
  [PXDefault(typeof (Search<PX.Objects.EP.EPEmployee.bAccountID, Where<PX.Objects.EP.EPEmployee.userID, Equal<Current<AccessInfo.userID>>>>))]
  [RQRequesterSelector(typeof (RQRequestApprove.reqClassID))]
  public virtual int? EmployeeID
  {
    get => this._EmployeeID;
    set => this._EmployeeID = value;
  }

  [PXDBString(10, IsUnicode = true)]
  [PXUIField(DisplayName = "Department")]
  public override string DepartmentID
  {
    get => this._DepartmentID;
    set => this._DepartmentID = value;
  }

  [PX.Objects.CS.LocationID(typeof (Where<PX.Objects.CR.Location.bAccountID, Equal<PX.Objects.CR.Location.bAccountID>>))]
  public int? LocationID
  {
    get => this._LocationID;
    set => this._LocationID = value;
  }

  [Inventory(Filterable = true)]
  public override int? InventoryID
  {
    get => this._InventoryID;
    set => this._InventoryID = value;
  }

  [PXDBString(1, IsFixed = true, BqlField = typeof (RQRequest.shipDestType))]
  [POShippingDestination.List]
  [PXUIField(DisplayName = "Shipping Destination Type")]
  public virtual string ShipDestType
  {
    get => this._ShipDestType;
    set => this._ShipDestType = value;
  }

  public new abstract class orderNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RQRequestLineOwned.orderNbr>
  {
  }

  public new abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RQRequestLineOwned.lineNbr>
  {
  }

  public new abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RQRequestLineOwned.vendorID>
  {
  }

  public new abstract class description : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RQRequestLineOwned.description>
  {
  }

  public abstract class reqClassID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RQRequestLineOwned.reqClassID>
  {
  }

  public abstract class priority : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RQRequestLineOwned.priority>
  {
  }

  public abstract class orderDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    RQRequestLineOwned.orderDate>
  {
  }

  public abstract class customerRequest : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    RQRequestLineOwned.customerRequest>
  {
  }

  public abstract class employeeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RQRequestLineOwned.employeeID>
  {
  }

  public new abstract class departmentID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RQRequestLineOwned.departmentID>
  {
  }

  public abstract class locationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RQRequestLineOwned.locationID>
  {
  }

  public new abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RQRequestLineOwned.inventoryID>
  {
  }

  public abstract class shipDestType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RQRequestLineOwned.shipDestType>
  {
  }
}
