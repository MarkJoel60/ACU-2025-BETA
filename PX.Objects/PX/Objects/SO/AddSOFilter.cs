// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.AddSOFilter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.AR;
using PX.Objects.CS;
using PX.Objects.IN;
using System;

#nullable enable
namespace PX.Objects.SO;

[Serializable]
public class AddSOFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _Operation;
  protected string _OrderType;
  protected string _OrderNbr;
  protected bool? _AddAllLines;

  [PXDBString(1, IsFixed = true, InputMask = ">a")]
  [PXUIField(DisplayName = "Operation")]
  [PXDefault("I", typeof (SOShipment.operation))]
  [SOOperation.List]
  public virtual string Operation
  {
    get => this._Operation;
    set => this._Operation = value;
  }

  [PXDBString(2, IsFixed = true, InputMask = ">aa")]
  [PXSelector(typeof (Search2<SOOrderType.orderType, InnerJoin<SOOrderTypeOperation, On<SOOrderTypeOperation.FK.OrderType>>, Where<SOOrderType.active, Equal<True>, And<SOOrderType.requireShipping, Equal<True>, And<SOOrderTypeOperation.active, Equal<True>, And<SOOrderTypeOperation.operation, Equal<Current<AddSOFilter.operation>>, And<Where<SOOrderTypeOperation.iNDocType, Equal<INTranType.transfer>, And<Current<SOShipment.shipmentType>, Equal<INDocType.transfer>, Or<SOOrderTypeOperation.iNDocType, NotEqual<INTranType.transfer>, And<Current<SOShipment.shipmentType>, Equal<INDocType.issue>>>>>>>>>>>))]
  [PXRestrictor(typeof (Where<Current<SOShipment.createARDoc>, IsNull, Or<Current<SOShipment.createARDoc>, Equal<IIf<Where<SOOrderType.aRDocType, Equal<ARDocType.noUpdate>>, False, True>>>>), "The {0} order type cannot be selected. The value of the AR Document Type setting for this order type differs from the value of the same setting for the order types in the lines that have already been added to the shipment.", new Type[] {typeof (SOOrderType.orderType)})]
  [PXDefault(typeof (Search2<SOOrderType.orderType, InnerJoin<SOOrderTypeOperation, On<SOOrderTypeOperation.FK.OrderType>, LeftJoin<SOSetup, On<SOSetup.defaultOrderType, Equal<SOOrderType.orderType>>>>, Where<SOOrderType.active, Equal<True>, And<SOOrderType.requireShipping, Equal<True>, And<SOOrderTypeOperation.active, Equal<True>, And<SOOrderTypeOperation.operation, Equal<Current<AddSOFilter.operation>>, And2<Where<SOOrderTypeOperation.iNDocType, Equal<INTranType.transfer>, And<Current<SOShipment.shipmentType>, Equal<INDocType.transfer>, Or<SOOrderTypeOperation.iNDocType, NotEqual<INTranType.transfer>, And<Current<SOShipment.shipmentType>, Equal<INDocType.issue>>>>>, And<Where<Current<SOShipment.createARDoc>, IsNull, Or<Current<SOShipment.createARDoc>, Equal<IIf<Where<SOOrderType.aRDocType, Equal<ARDocType.noUpdate>>, False, True>>>>>>>>>>, OrderBy<Desc<SOSetup.defaultOrderType, Asc<SOOrderType.orderType>>>>))]
  [PXUIField(DisplayName = "Order Type")]
  [PXFormula(typeof (Default<AddSOFilter.operation>))]
  public virtual string OrderType
  {
    get => this._OrderType;
    set => this._OrderType = value;
  }

  [PXDBString(15, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Order Nbr.")]
  [PXDefault]
  [PX.Objects.SO.SO.RefNbr(typeof (Search<SOOrder.orderNbr, Where<SOOrder.orderType, Equal<Current<AddSOFilter.orderType>>, And<SOOrder.customerID, Equal<Current<SOShipment.customerID>>, And2<Where<SOOrder.customerLocationID, Equal<Current<SOShipment.customerLocationID>>, Or<Current<SOShipment.orderCntr>, Equal<int0>>>, And<SOOrder.cancelled, Equal<False>, And<SOOrder.completed, Equal<False>, And<SOOrder.hold, Equal<False>, And<SOOrder.creditHold, Equal<False>, And<SOOrder.approved, Equal<True>>>>>>>>>>), Filterable = true)]
  [PXFormula(typeof (Default<AddSOFilter.orderType>))]
  public virtual string OrderNbr
  {
    get => this._OrderNbr;
    set => this._OrderNbr = value;
  }

  /// <exclude />
  [PXDBInt]
  [PXUIField(DisplayName = "Line Nbr.", Visible = false, Enabled = false)]
  public virtual int? OrderLineNbr { get; set; }

  [PXDBString(1, IsFixed = true)]
  [PX.Objects.CS.FreightAmountSource]
  [PXFormula(typeof (Selector<AddSOFilter.orderNbr, SOOrder.freightAmountSource>))]
  public virtual string FreightAmountSource { get; set; }

  [PXDefault(false)]
  public virtual bool? AddAllLines
  {
    get => this._AddAllLines;
    set => this._AddAllLines = value;
  }

  public abstract class operation : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AddSOFilter.operation>
  {
  }

  public abstract class orderType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AddSOFilter.orderType>
  {
  }

  public abstract class orderNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AddSOFilter.orderNbr>
  {
  }

  /// <exclude />
  public abstract class orderLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  AddSOFilter.orderLineNbr>
  {
  }

  public abstract class freightAmountSource : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AddSOFilter.freightAmountSource>
  {
  }

  public abstract class addAllLines : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  AddSOFilter.addAllLines>
  {
  }
}
