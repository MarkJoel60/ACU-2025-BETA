// Decompiled with JetBrains decompiler
// Type: PX.Objects.RQ.RQRequestApprove
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CM;
using PX.Objects.EP;
using PX.Objects.IN;
using PX.Objects.PO;
using System;

#nullable enable
namespace PX.Objects.RQ;

[PXHidden]
[Serializable]
public class RQRequestApprove : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected bool? _Selected = new bool?(false);
  protected 
  #nullable disable
  string _ReqClassID;
  protected string _OrderNbr;
  protected DateTime? _OrderDate;
  protected int? _Priority;
  protected string _Description;
  protected bool? _CheckBudget;
  protected int? _EmployeeID;
  protected string _DepartmentID;
  protected int? _LocationID;
  protected string _CuryID;
  protected long? _CuryInfoID;
  protected string _Purpose;
  protected int? _LineNbr;
  protected string _LineType;
  protected string _InventoryID;
  protected int? _SubItemID;
  protected string _UOM;
  protected Decimal? _OrderQty;
  protected Decimal? _BaseOrderQty;
  protected Decimal? _CuryEstUnitCost;
  protected Decimal? _CuryEstExtCost;
  protected Decimal? _EstExtCost;
  protected DateTime? _RequestedDate;
  protected DateTime? _PromisedDate;
  protected bool? _Escalated;

  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Selected")]
  public virtual bool? Selected
  {
    get => this._Selected;
    set => this._Selected = value;
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

  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = "")]
  [PXDefault]
  [PXUIField]
  [PXSelector(typeof (RQRequest.orderNbr), new System.Type[] {typeof (RQRequest.orderNbr), typeof (RQRequest.orderDate), typeof (RQRequest.status), typeof (RQRequest.employeeID), typeof (RQRequest.departmentID), typeof (RQRequest.locationID)}, Filterable = true)]
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

  [PXDBInt]
  [PXUIField]
  [PXDefault(1)]
  [PXIntList(new int[] {0, 1, 2}, new string[] {"Low", "Normal", "High"})]
  public virtual int? Priority
  {
    get => this._Priority;
    set => this._Priority = value;
  }

  [PXDBString(60, IsUnicode = true)]
  [PXUIField]
  public virtual string Description
  {
    get => this._Description;
    set => this._Description = value;
  }

  [PXDBBool]
  [PXUIField]
  [PXDefault(false)]
  public virtual bool? CheckBudget
  {
    get => this._CheckBudget;
    set => this._CheckBudget = value;
  }

  [PXDBInt]
  [PXDefault(typeof (Search<EPEmployee.bAccountID, Where<EPEmployee.userID, Equal<Current<AccessInfo.userID>>>>))]
  [RQRequesterSelector(typeof (RQRequestApprove.reqClassID))]
  [PXUIField]
  public virtual int? EmployeeID
  {
    get => this._EmployeeID;
    set => this._EmployeeID = value;
  }

  [PXDBString(10, IsUnicode = true)]
  [PXDefault(typeof (Search<EPEmployee.departmentID, Where<EPEmployee.bAccountID, Equal<Current<RQRequestApprove.employeeID>>>>))]
  [PXSelector(typeof (EPDepartment.departmentID), DescriptionField = typeof (EPDepartment.description))]
  [PXUIField]
  public virtual string DepartmentID
  {
    get => this._DepartmentID;
    set => this._DepartmentID = value;
  }

  [PXDefault(typeof (Search<EPEmployee.defLocationID, Where<EPEmployee.bAccountID, Equal<Current<RQRequestApprove.employeeID>>>>))]
  [PX.Objects.CS.LocationID(typeof (Where<PX.Objects.CR.Location.bAccountID, Equal<Current<RQRequestApprove.employeeID>>>))]
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

  [PXDBString(100, IsUnicode = true)]
  [PXUIField(DisplayName = "Purpose")]
  public virtual string Purpose
  {
    get => this._Purpose;
    set => this._Purpose = value;
  }

  [PXDBInt(IsKey = true)]
  [PXUIField]
  public virtual int? LineNbr
  {
    get => this._LineNbr;
    set => this._LineNbr = value;
  }

  [PXDBString(2, IsFixed = true)]
  [POLineType.DefaultList]
  [PXUIField(DisplayName = "Line Type")]
  public virtual string LineType
  {
    get => this._LineType;
    set => this._LineType = value;
  }

  [InventoryRaw(Filterable = true)]
  public virtual string InventoryID
  {
    get => this._InventoryID;
    set => this._InventoryID = value;
  }

  [SubItem(typeof (RQRequestApprove.inventoryID))]
  public virtual int? SubItemID
  {
    get => this._SubItemID;
    set => this._SubItemID = value;
  }

  [PXDefault(typeof (Search<PX.Objects.IN.InventoryItem.purchaseUnit, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Current<RQRequestApprove.inventoryID>>>>))]
  [INUnit(typeof (RQRequestApprove.inventoryID), DisplayName = "UOM")]
  public virtual string UOM
  {
    get => this._UOM;
    set => this._UOM = value;
  }

  [PXDBQuantity(typeof (RQRequestApprove.uOM), typeof (RQRequestApprove.baseOrderQty), HandleEmptyKey = true)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? OrderQty
  {
    get => this._OrderQty;
    set => this._OrderQty = value;
  }

  [PXDBDecimal(6)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? BaseOrderQty
  {
    get => this._BaseOrderQty;
    set => this._BaseOrderQty = value;
  }

  [PXDBDecimal]
  [PXUIField]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryEstUnitCost
  {
    get => this._CuryEstUnitCost;
    set => this._CuryEstUnitCost = value;
  }

  [PXDBCurrency(typeof (RQRequestApprove.curyInfoID), typeof (RQRequestApprove.estExtCost))]
  [PXUIField]
  [PXFormula(typeof (Mult<RQRequestApprove.orderQty, RQRequestApprove.curyEstUnitCost>))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryEstExtCost
  {
    get => this._CuryEstExtCost;
    set => this._CuryEstExtCost = value;
  }

  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? EstExtCost
  {
    get => this._EstExtCost;
    set => this._EstExtCost = value;
  }

  [PXDBDate]
  [PXUIField(DisplayName = "Required Date")]
  public virtual DateTime? RequestedDate
  {
    get => this._RequestedDate;
    set => this._RequestedDate = value;
  }

  [PXDBDate]
  [PXUIField(DisplayName = "Promised Date")]
  public virtual DateTime? PromisedDate
  {
    get => this._PromisedDate;
    set => this._PromisedDate = value;
  }

  [PXBool]
  public virtual bool? Escalated
  {
    get => this._Escalated;
    set => this._Escalated = value;
  }

  public class PK : 
    PrimaryKeyOf<RQRequestApprove>.By<RQRequestApprove.orderNbr, RQRequestApprove.lineNbr>
  {
    public static RQRequestApprove Find(
      PXGraph graph,
      string orderNbr,
      int? lineNbr,
      PKFindOptions options = 0)
    {
      return (RQRequestApprove) PrimaryKeyOf<RQRequestApprove>.By<RQRequestApprove.orderNbr, RQRequestApprove.lineNbr>.FindBy(graph, (object) orderNbr, (object) lineNbr, options);
    }
  }

  public static class FK
  {
    public class RequestClass : 
      PrimaryKeyOf<RQRequestClass>.By<RQRequestClass.reqClassID>.ForeignKeyOf<RQRequestApprove>.By<RQRequestApprove.reqClassID>
    {
    }
  }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  RQRequestApprove.selected>
  {
  }

  public abstract class reqClassID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RQRequestApprove.reqClassID>
  {
  }

  public abstract class orderNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RQRequestApprove.orderNbr>
  {
  }

  public abstract class orderDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  RQRequestApprove.orderDate>
  {
  }

  public abstract class priority : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RQRequestApprove.priority>
  {
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RQRequestApprove.description>
  {
  }

  public abstract class checkBudget : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  RQRequestApprove.checkBudget>
  {
  }

  public abstract class employeeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RQRequestApprove.employeeID>
  {
  }

  public abstract class departmentID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RQRequestApprove.departmentID>
  {
  }

  public abstract class locationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RQRequestApprove.locationID>
  {
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RQRequestApprove.curyID>
  {
  }

  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  RQRequestApprove.curyInfoID>
  {
  }

  public abstract class purpose : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RQRequestApprove.purpose>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RQRequestApprove.lineNbr>
  {
  }

  public abstract class lineType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RQRequestApprove.lineType>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RQRequestApprove.inventoryID>
  {
  }

  public abstract class subItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RQRequestApprove.subItemID>
  {
  }

  public abstract class uOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RQRequestApprove.uOM>
  {
  }

  public abstract class orderQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  RQRequestApprove.orderQty>
  {
  }

  public abstract class baseOrderQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    RQRequestApprove.baseOrderQty>
  {
  }

  public abstract class curyEstUnitCost : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    RQRequestApprove.curyEstUnitCost>
  {
  }

  public abstract class curyEstExtCost : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    RQRequestApprove.curyEstExtCost>
  {
  }

  public abstract class estExtCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  RQRequestApprove.estExtCost>
  {
  }

  public abstract class requestedDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    RQRequestApprove.requestedDate>
  {
  }

  public abstract class promisedDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    RQRequestApprove.promisedDate>
  {
  }

  public abstract class escalated : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  RQRequestApprove.escalated>
  {
  }
}
