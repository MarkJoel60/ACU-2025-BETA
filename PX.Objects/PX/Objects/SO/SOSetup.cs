// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOSetup
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CS;
using PX.Objects.EP;
using PX.Objects.IN;
using System;

#nullable enable
namespace PX.Objects.SO;

/// <exclude />
[PXPrimaryGraph(typeof (SOSetupMaint))]
[PXCacheName("Sales Orders Preferences")]
[Serializable]
public class SOSetup : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _ShipmentNumberingID;
  protected bool? _HoldShipments;
  protected bool? _OrderRequestApproval;
  protected bool? _RequireShipmentTotal;
  protected bool? _AddAllToShipment;
  protected bool? _CreateZeroShipments;
  protected bool? _AutoReleaseIN;
  protected int? _DefaultOrderAssignmentMapID;
  protected int? _DefaultShipmentAssignmentMapID;
  protected bool? _ProrateDiscounts;
  protected string _FreeItemShipping;
  protected string _FreightAllocation;
  protected string _MinGrossProfitValidation;
  protected bool? _UsePriceAdjustmentMultiplier;
  protected string _DefaultOrderType;
  protected string _TransferOrderType;
  protected bool? _CreditCheckError;
  protected string _SalesProfitabilityForNSKits;
  protected bool? _DisableEditingPricesDiscountsForIntercompany;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  [PXDBString(10, IsUnicode = true)]
  [PXDefault("SOSHIPMENT")]
  [PXSelector(typeof (Numbering.numberingID), DescriptionField = typeof (Numbering.descr))]
  [PXUIField(DisplayName = "Shipment Numbering Sequence")]
  public virtual string ShipmentNumberingID
  {
    get => this._ShipmentNumberingID;
    set => this._ShipmentNumberingID = value;
  }

  [PXDBString(10, IsUnicode = true)]
  [PXDefault("PICKWORKSH")]
  [PXSelector(typeof (Numbering.numberingID), DescriptionField = typeof (Numbering.descr))]
  [PXUIField(DisplayName = "Picking Worksheet Numbering Sequence")]
  public virtual string PickingWorksheetNumberingID { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Hold Shipments on Entry")]
  public virtual bool? HoldShipments
  {
    get => this._HoldShipments;
    set => this._HoldShipments = value;
  }

  [Obsolete]
  [EPRequireApproval]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Require Approval")]
  public virtual bool? OrderRequestApproval
  {
    get => this._OrderRequestApproval;
    set => this._OrderRequestApproval = value;
  }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Validate Shipment Total on Confirmation")]
  public virtual bool? RequireShipmentTotal
  {
    get => this._RequireShipmentTotal;
    set => this._RequireShipmentTotal = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Add Zero Lines for Items Not in Stock")]
  public virtual bool? AddAllToShipment
  {
    get => this._AddAllToShipment;
    set => this._AddAllToShipment = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Create Zero Shipments")]
  [PXUIEnabled(typeof (SOSetup.addAllToShipment))]
  [PXFormula(typeof (BqlOperand<False, IBqlBool>.When<BqlOperand<SOSetup.addAllToShipment, IBqlBool>.IsEqual<False>>.Else<SOSetup.createZeroShipments>))]
  public virtual bool? CreateZeroShipments
  {
    get => this._CreateZeroShipments;
    set => this._CreateZeroShipments = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Automatically Release IN Documents")]
  public virtual bool? AutoReleaseIN
  {
    get => this._AutoReleaseIN;
    set => this._AutoReleaseIN = value;
  }

  [PXDBInt]
  [PXSelector(typeof (Search<EPAssignmentMap.assignmentMapID, Where<EPAssignmentMap.entityType, Equal<AssignmentMapType.AssignmentMapTypeSalesOrder>>>))]
  [PXUIField(DisplayName = "Default Sales Order Assignment Map")]
  public virtual int? DefaultOrderAssignmentMapID
  {
    get => this._DefaultOrderAssignmentMapID;
    set => this._DefaultOrderAssignmentMapID = value;
  }

  [PXDBInt]
  [PXSelector(typeof (Search<EPAssignmentMap.assignmentMapID, Where<EPAssignmentMap.entityType, Equal<AssignmentMapType.AssignmentMapTypeSalesOrderShipment>>>))]
  [PXUIField(DisplayName = "Default Sales Order Shipment Assignment Map")]
  public virtual int? DefaultShipmentAssignmentMapID
  {
    get => this._DefaultShipmentAssignmentMapID;
    set => this._DefaultShipmentAssignmentMapID = value;
  }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Prorate Discounts")]
  public virtual bool? ProrateDiscounts
  {
    get => this._ProrateDiscounts;
    set => this._ProrateDiscounts = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXDefault("P")]
  [FreeItemShipType.List]
  [PXUIField]
  public virtual string FreeItemShipping
  {
    get => this._FreeItemShipping;
    set => this._FreeItemShipping = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXDefault("A")]
  [FreightAllocationList.List]
  [PXUIField]
  public virtual string FreightAllocation
  {
    get => this._FreightAllocation;
    set => this._FreightAllocation = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXDefault("W")]
  [MinGrossProfitValidationType.List]
  [PXUIField]
  public virtual string MinGrossProfitValidation
  {
    get => this._MinGrossProfitValidation;
    set => this._MinGrossProfitValidation = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Use a Price Adjustment Multiplier")]
  public virtual bool? UsePriceAdjustmentMultiplier
  {
    get => this._UsePriceAdjustmentMultiplier;
    set => this._UsePriceAdjustmentMultiplier = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Specific to Customers")]
  public virtual bool? IgnoreMinGrossProfitCustomerPrice { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Specific to Customer Price Classes")]
  public virtual bool? IgnoreMinGrossProfitCustomerPriceClass { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Specific to Promotions")]
  public virtual bool? IgnoreMinGrossProfitPromotionalPrice { get; set; }

  [PXDBString(2, IsFixed = true, InputMask = ">aa")]
  [PXUIField(DisplayName = "Default Sales Order Type")]
  [PXDefault(typeof (BqlOperand<SOOrderTypeConstants.salesOrder, IBqlString>.When<Where<FeatureInstalled<FeaturesSet.inventory>>>.Else<SOOrderTypeConstants.invoiceOrder>))]
  [PXSelector(typeof (Search<SOOrderType.orderType>), DescriptionField = typeof (SOOrderType.descr))]
  [PXRestrictor(typeof (Where<SOOrderType.active, Equal<True>>), "Order Type is inactive.", new Type[] {typeof (SOOrderType.orderType)})]
  public virtual string DefaultOrderType
  {
    get => this._DefaultOrderType;
    set => this._DefaultOrderType = value;
  }

  [PXDBString(2, IsFixed = true, InputMask = ">aa")]
  [PXUIField(DisplayName = "Default Transfer Order Type")]
  [PXDefault(typeof (Switch<Case<Where<FeatureInstalled<FeaturesSet.warehouse>>, SOOrderTypeConstants.transferOrder>>))]
  [PXSelector(typeof (Search2<SOOrderType.orderType, InnerJoin<SOOrderTypeOperation, On<SOOrderTypeOperation.orderType, Equal<SOOrderType.orderType>, And<SOOrderTypeOperation.iNDocType, Equal<INTranType.transfer>>>>>), DescriptionField = typeof (SOOrderType.descr))]
  public virtual string TransferOrderType
  {
    get => this._TransferOrderType;
    set => this._TransferOrderType = value;
  }

  /// <summary>
  /// Gets or sets the data field from the drop-down that will be used as the default type for Return order.
  /// </summary>
  [PXDBString(2, IsFixed = true, InputMask = ">aa")]
  [PXUIField(DisplayName = "Default Return Order Type", FieldClass = "CASE")]
  [PXDefault(typeof (Switch<Case<Where<FeatureInstalled<FeaturesSet.caseManagement>>, SOOrderTypeConstants.rmaOrder>>))]
  [PXSelector(typeof (Search<SOOrderType.orderType, Where<SOOrderType.behavior, Equal<SOBehavior.rM>>>), DescriptionField = typeof (SOOrderType.descr))]
  [PXRestrictor(typeof (Where<SOOrderType.active, Equal<True>>), "Order Type is inactive.", new Type[] {typeof (SOOrderType.orderType)}, ShowWarning = true)]
  public virtual string DefaultReturnOrderType { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField]
  public virtual bool? CreditCheckError
  {
    get => this._CreditCheckError;
    set => this._CreditCheckError = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Use Shipment Date for Invoice Date")]
  public virtual bool? UseShipDateForInvoiceDate { get; set; }

  [PXDBString(1, IsFixed = true)]
  [PXDefault("C")]
  [SalesProfitabilityNSKitMethod.List]
  [PXUIField]
  public virtual string SalesProfitabilityForNSKits
  {
    get => this._SalesProfitabilityForNSKits;
    set => this._SalesProfitabilityForNSKits = value;
  }

  [PXDBString(2, IsFixed = true, InputMask = ">aa")]
  [PXUIField(DisplayName = "Default Type for Intercompany Sales", FieldClass = "InterBranch")]
  [PXDefault(typeof (Switch<Case<Where<FeatureInstalled<FeaturesSet.interBranch>>, SOOrderTypeConstants.salesOrder>>))]
  [PXSelector(typeof (Search2<SOOrderType.orderType, InnerJoin<SOOrderTypeOperation, On<SOOrderTypeOperation.orderType, Equal<SOOrderType.orderType>, And<SOOrderTypeOperation.operation, Equal<SOOrderType.defaultOperation>, And<SOOrderTypeOperation.iNDocType, NotEqual<INTranType.transfer>>>>>, Where<SOOrderType.behavior, In3<SOBehavior.sO, SOBehavior.iN>>>), DescriptionField = typeof (SOOrderType.descr))]
  [PXRestrictor(typeof (Where<SOOrderType.active, Equal<True>>), "Order Type is inactive.", new Type[] {typeof (SOOrderType.orderType)})]
  public virtual string DfltIntercompanyOrderType { get; set; }

  [PXDBString(2, IsFixed = true, InputMask = ">aa")]
  [PXUIField(DisplayName = "Default Type for Intercompany Returns", FieldClass = "InterBranch")]
  [PXDefault(typeof (Switch<Case<Where<FeatureInstalled<FeaturesSet.interBranch>>, SOOrderTypeConstants.rmaOrder>>))]
  [PXSelector(typeof (Search<SOOrderType.orderType, Where<SOOrderType.behavior, In3<SOBehavior.rM, SOBehavior.cM>>>), DescriptionField = typeof (SOOrderType.descr))]
  [PXRestrictor(typeof (Where<SOOrderType.active, Equal<True>>), "Order Type is inactive.", new Type[] {typeof (SOOrderType.orderType)})]
  public virtual string DfltIntercompanyRMAType { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Disable Editing Prices and Discounts", FieldClass = "InterBranch")]
  public virtual bool? DisableEditingPricesDiscountsForIntercompany
  {
    get => this._DisableEditingPricesDiscountsForIntercompany;
    set => this._DisableEditingPricesDiscountsForIntercompany = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Disable Adding Items to Orders", FieldClass = "InterBranch")]
  public virtual bool? DisableAddingItemsForIntercompany { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Defer Discount Recalculation")]
  public virtual bool? DeferPriceDiscountRecalculation { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Show Only Available Items")]
  public virtual bool? ShowOnlyAvailableRelatedItems { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [Obsolete("This item has been deprecated and will be removed in Acumatica ERP 2024 R1.")]
  public virtual bool? UseBaseUomTransferringAllocations { get; set; }

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

  public static class FK
  {
    public class ShipmentNumbering : 
      PrimaryKeyOf<Numbering>.By<Numbering.numberingID>.ForeignKeyOf<SOSetup>.By<SOSetup.shipmentNumberingID>
    {
    }

    public class PickingWorksheetNumbering : 
      PrimaryKeyOf<Numbering>.By<Numbering.numberingID>.ForeignKeyOf<SOSetup>.By<SOSetup.pickingWorksheetNumberingID>
    {
    }

    public class DefaultSalesOrderAssignmentMap : 
      PrimaryKeyOf<EPAssignmentMap>.By<EPAssignmentMap.assignmentMapID>.ForeignKeyOf<SOSetup>.By<SOSetup.defaultOrderAssignmentMapID>
    {
    }

    public class DefaultSalesOrderShipmentAssignmentMap : 
      PrimaryKeyOf<EPAssignmentMap>.By<EPAssignmentMap.assignmentMapID>.ForeignKeyOf<SOSetup>.By<SOSetup.defaultShipmentAssignmentMapID>
    {
    }

    public class DefaultOrderType : 
      PrimaryKeyOf<SOOrderType>.By<SOOrderType.orderType>.ForeignKeyOf<SOSetup>.By<SOSetup.defaultOrderType>
    {
    }

    public class TransferOrderType : 
      PrimaryKeyOf<SOOrderType>.By<SOOrderType.orderType>.ForeignKeyOf<SOSetup>.By<SOSetup.transferOrderType>
    {
    }
  }

  public abstract class shipmentNumberingID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOSetup.shipmentNumberingID>
  {
  }

  public abstract class pickingWorksheetNumberingID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOSetup.pickingWorksheetNumberingID>
  {
  }

  public abstract class holdShipments : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOSetup.holdShipments>
  {
  }

  [Obsolete]
  public abstract class orderRequestApproval : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOSetup.orderRequestApproval>
  {
  }

  public abstract class requireShipmentTotal : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOSetup.requireShipmentTotal>
  {
  }

  public abstract class addAllToShipment : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOSetup.addAllToShipment>
  {
  }

  public abstract class createZeroShipments : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOSetup.createZeroShipments>
  {
  }

  public abstract class autoReleaseIN : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOSetup.autoReleaseIN>
  {
  }

  public abstract class defaultOrderAssignmentMapID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SOSetup.defaultOrderAssignmentMapID>
  {
  }

  public abstract class defaultShipmentAssignmentMapID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SOSetup.defaultShipmentAssignmentMapID>
  {
  }

  public abstract class prorateDiscounts : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOSetup.prorateDiscounts>
  {
  }

  public abstract class freeItemShipping : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOSetup.freeItemShipping>
  {
  }

  public abstract class freightAllocation : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOSetup.freightAllocation>
  {
  }

  public abstract class minGrossProfitValidation : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOSetup.minGrossProfitValidation>
  {
  }

  public abstract class usePriceAdjustmentMultiplier : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOSetup.usePriceAdjustmentMultiplier>
  {
  }

  public abstract class ignoreMinGrossProfitCustomerPrice : IBqlField, IBqlOperand
  {
  }

  public abstract class ignoreMinGrossProfitCustomerPriceClass : IBqlField, IBqlOperand
  {
  }

  public abstract class ignoreMinGrossProfitPromotionalPrice : IBqlField, IBqlOperand
  {
  }

  public abstract class defaultOrderType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOSetup.defaultOrderType>
  {
  }

  public abstract class transferOrderType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOSetup.transferOrderType>
  {
  }

  public abstract class defaultReturnOrderType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOSetup.defaultReturnOrderType>
  {
  }

  public abstract class creditCheckError : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOSetup.creditCheckError>
  {
  }

  public abstract class useShipDateForInvoiceDate : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOSetup.useShipDateForInvoiceDate>
  {
  }

  public abstract class salesProfitabilityForNSKits : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOSetup.salesProfitabilityForNSKits>
  {
  }

  public abstract class dfltIntercompanyOrderType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOSetup.dfltIntercompanyOrderType>
  {
  }

  public abstract class dfltIntercompanyRMAType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOSetup.dfltIntercompanyRMAType>
  {
  }

  public abstract class disableEditingPricesDiscountsForIntercompany : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOSetup.disableEditingPricesDiscountsForIntercompany>
  {
  }

  public abstract class disableAddingItemsForIntercompany : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOSetup.disableAddingItemsForIntercompany>
  {
  }

  public abstract class deferPriceDiscountRecalculation : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOSetup.deferPriceDiscountRecalculation>
  {
  }

  public abstract class showOnlyAvailableRelatedItems : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOSetup.showOnlyAvailableRelatedItems>
  {
  }

  [Obsolete("This item has been deprecated and will be removed in Acumatica ERP 2024 R1.")]
  public abstract class useBaseUomTransferringAllocations : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOSetup.useBaseUomTransferringAllocations>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  SOSetup.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SOSetup.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOSetup.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SOSetup.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SOSetup.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOSetup.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SOSetup.lastModifiedDateTime>
  {
  }
}
