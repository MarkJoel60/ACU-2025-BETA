// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOOrderTypeOperation
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CS;
using PX.Objects.IN;
using System;

#nullable enable
namespace PX.Objects.SO;

[PXCacheName]
[Serializable]
public class SOOrderTypeOperation : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _OrderType;
  protected string _Operation;
  protected string _INDocType;
  protected string _OrderPlanType;
  protected string _ShipmentPlanType;
  protected bool? _AutoCreateIssueLine;
  protected bool? _Active;
  protected bool? _RequireReasonCode;
  protected short? _InvtMult;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;
  protected byte[] _tstamp;

  [PXDBString(2, IsKey = true, IsFixed = true, InputMask = ">aa")]
  [PXUIField(DisplayName = "Order Type", Visible = false)]
  [PXDefault(typeof (SOOrderType.orderType))]
  [PXParent(typeof (SOOrderTypeOperation.FK.OrderType))]
  public virtual string OrderType
  {
    get => this._OrderType;
    set => this._OrderType = value;
  }

  [PXDBString(1, IsKey = true, IsFixed = true, InputMask = "")]
  [PXUIField(DisplayName = "Operation", Enabled = false)]
  [PXDefault(typeof (SOOrderType.defaultOperation))]
  [SOOperation.List]
  public virtual string Operation
  {
    get => this._Operation;
    set => this._Operation = value;
  }

  [PXDBString(3, IsFixed = true)]
  [PXDefault]
  [INTranType.SOList]
  [PXUIField(DisplayName = "Inventory Transaction Type")]
  public virtual string INDocType
  {
    get => this._INDocType;
    set => this._INDocType = value;
  }

  [PXDBString(2, IsFixed = true, InputMask = ">aa")]
  [PXUIField(DisplayName = "Order Plan Type")]
  [PXDefault]
  [PXSelector(typeof (Search<INPlanType.planType, Where<INPlanType.isDemand, Equal<True>>>), DescriptionField = typeof (INPlanType.localizedDescr))]
  [PXRestrictor(typeof (Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<SOOrderType.behavior>, In3<SOBehavior.iN, SOBehavior.cM, SOBehavior.mO>>>>>.And<BqlOperand<INPlanType.inclQtySOShipped, IBqlShort>.IsEqual<short1>>>>, Or<Brackets<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<SOOrderType.behavior>, In3<SOBehavior.rM, SOBehavior.sO, SOBehavior.tR, SOBehavior.bL>>>>, And<BqlOperand<Current<SOOrderType.requireAllocation>, IBqlBool>.IsEqual<False>>>>.And<BqlOperand<INPlanType.inclQtySOBooked, IBqlShort>.IsEqual<short1>>>>>, Or<Brackets<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<SOOrderType.behavior>, In3<SOBehavior.rM, SOBehavior.sO, SOBehavior.tR, SOBehavior.bL>>>>, And<BqlOperand<Current<SOOrderType.requireAllocation>, IBqlBool>.IsEqual<True>>>>.And<BqlOperand<INPlanType.inclQtySOShipping, IBqlShort>.IsEqual<short1>>>>>>.Or<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<SOOrderType.behavior>, Equal<SOBehavior.qT>>>>>.And<BqlOperand<INPlanType.planType, IBqlString>.IsNull>>>), "The selected order plan type cannot be used with the current state of the Require Stock Allocation check box. Select another order plan type.", new Type[] {typeof (INPlanType.planType)}, ShowWarning = true)]
  public virtual string OrderPlanType
  {
    get => this._OrderPlanType;
    set => this._OrderPlanType = value;
  }

  [PXDBString(2, IsFixed = true, InputMask = ">aa")]
  [PXUIField(DisplayName = "Shipment Plan Type")]
  [PXDefault]
  [PXSelector(typeof (Search<INPlanType.planType, Where<INPlanType.isDemand, Equal<True>>>), DescriptionField = typeof (INPlanType.localizedDescr))]
  public virtual string ShipmentPlanType
  {
    get => this._ShipmentPlanType;
    set => this._ShipmentPlanType = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Auto Create Issue Line")]
  public virtual bool? AutoCreateIssueLine
  {
    get => this._AutoCreateIssueLine;
    set => this._AutoCreateIssueLine = value;
  }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Active")]
  [PXUnboundFormula(typeof (IIf<Where<SOOrderTypeOperation.active, Equal<True>>, int1, int0>), typeof (SumCalc<SOOrderType.activeOperationsCntr>))]
  public virtual bool? Active
  {
    get => this._Active;
    set => this._Active = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Require Reason Code")]
  public virtual bool? RequireReasonCode
  {
    get => this._RequireReasonCode;
    set => this._RequireReasonCode = value;
  }

  [PXDBShort]
  [PXDefault]
  [PXFormula(typeof (Default<SOOrderTypeOperation.iNDocType>))]
  public virtual short? InvtMult
  {
    get => this._InvtMult;
    set => this._InvtMult = value;
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
  public virtual DateTime? LastModifiedDateTime
  {
    get => this._LastModifiedDateTime;
    set => this._LastModifiedDateTime = value;
  }

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  public class PK : 
    PrimaryKeyOf<SOOrderTypeOperation>.By<SOOrderTypeOperation.orderType, SOOrderTypeOperation.operation>
  {
    public static SOOrderTypeOperation Find(
      PXGraph graph,
      string orderType,
      string operation,
      PKFindOptions options = 0)
    {
      return (SOOrderTypeOperation) PrimaryKeyOf<SOOrderTypeOperation>.By<SOOrderTypeOperation.orderType, SOOrderTypeOperation.operation>.FindBy(graph, (object) orderType, (object) operation, options);
    }
  }

  public static class FK
  {
    public class OrderType : 
      PrimaryKeyOf<SOOrderType>.By<SOOrderType.orderType>.ForeignKeyOf<SOOrderTypeOperation>.By<SOOrderTypeOperation.orderType>
    {
    }

    public class OrderPlanType : 
      PrimaryKeyOf<INPlanType>.By<INPlanType.planType>.ForeignKeyOf<SOOrderTypeOperation>.By<SOOrderTypeOperation.orderPlanType>
    {
    }

    public class ShipmentPlanType : 
      PrimaryKeyOf<INPlanType>.By<INPlanType.planType>.ForeignKeyOf<SOOrderTypeOperation>.By<SOOrderTypeOperation.shipmentPlanType>
    {
    }
  }

  public abstract class orderType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOOrderTypeOperation.orderType>
  {
  }

  public abstract class operation : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOOrderTypeOperation.operation>
  {
  }

  public abstract class iNDocType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOOrderTypeOperation.iNDocType>
  {
    public const int Length = 3;
  }

  public abstract class orderPlanType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOOrderTypeOperation.orderPlanType>
  {
    public const int Length = 2;
  }

  public abstract class shipmentPlanType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOOrderTypeOperation.shipmentPlanType>
  {
  }

  public abstract class autoCreateIssueLine : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOOrderTypeOperation.autoCreateIssueLine>
  {
  }

  public abstract class active : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOOrderTypeOperation.active>
  {
  }

  public abstract class requireReasonCode : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOOrderTypeOperation.requireReasonCode>
  {
  }

  public abstract class invtMult : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  SOOrderTypeOperation.invtMult>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SOOrderTypeOperation.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOOrderTypeOperation.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SOOrderTypeOperation.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    SOOrderTypeOperation.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOOrderTypeOperation.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SOOrderTypeOperation.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  SOOrderTypeOperation.Tstamp>
  {
  }
}
