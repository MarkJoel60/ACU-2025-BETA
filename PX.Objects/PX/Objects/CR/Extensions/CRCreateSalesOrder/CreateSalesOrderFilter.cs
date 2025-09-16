// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Extensions.CRCreateSalesOrder.CreateSalesOrderFilter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using PX.Objects.SO;
using System;

#nullable enable
namespace PX.Objects.CR.Extensions.CRCreateSalesOrder;

[PXHidden]
[Serializable]
public class CreateSalesOrderFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXString(15, IsUnicode = true, InputMask = "")]
  [PXUnboundDefault]
  [PXUIField(DisplayName = "Order Nbr.", TabOrder = 1)]
  public virtual 
  #nullable disable
  string OrderNbr { get; set; }

  [PXBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Set Quote as Primary", Visible = false)]
  public virtual bool? MakeQuotePrimary { get; set; }

  [PXDBString(2, IsFixed = true, InputMask = ">aa")]
  [PXDefault(typeof (Search2<SOOrderType.orderType, InnerJoin<SOSetup, On<SOOrderType.orderType, Equal<SOSetup.defaultOrderType>>>, Where<SOOrderType.active, Equal<boolTrue>>>))]
  [PXSelector(typeof (Search<SOOrderType.orderType>), DescriptionField = typeof (SOOrderType.descr))]
  [PXRestrictor(typeof (Where<SOOrderType.active, Equal<boolTrue>>), "Order Type '{0}' is not active.", new System.Type[] {typeof (SOOrderType.descr)})]
  [PXRestrictor(typeof (Where<BqlOperand<SOOrderType.behavior, IBqlString>.IsIn<SOBehavior.sO, SOBehavior.iN, SOBehavior.bL, SOBehavior.qT>>), "The order type cannot be used.", new System.Type[] {})]
  [PXUIField(DisplayName = "Order Type")]
  public virtual string OrderType { get; set; }

  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Recalculate Prices and Discounts")]
  [Obsolete]
  public virtual bool? RecalcDiscounts { get; set; }

  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Set Current Unit Prices")]
  public virtual bool? RecalculatePrices { get; set; }

  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Override Manual Prices")]
  public virtual bool? OverrideManualPrices { get; set; }

  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Recalculate Discounts")]
  public virtual bool? RecalculateDiscounts { get; set; }

  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Override Manual Line Discounts")]
  public virtual bool? OverrideManualDiscounts { get; set; }

  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Override Manual Group and Document Discounts")]
  public virtual bool? OverrideManualDocGroupDiscounts { get; set; }

  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Create a Sales Order Regardless of the Specified Manual Amount")]
  public virtual bool? ConfirmManualAmount { get; set; }

  public abstract class orderNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CreateSalesOrderFilter.orderNbr>
  {
  }

  public abstract class makeQuotePrimary : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CreateSalesOrderFilter.makeQuotePrimary>
  {
  }

  public abstract class orderType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CreateSalesOrderFilter.orderType>
  {
  }

  public abstract class recalcDiscount : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CreateSalesOrderFilter.recalcDiscount>
  {
  }

  public abstract class recalculatePrices : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CreateSalesOrderFilter.recalculatePrices>
  {
  }

  public abstract class overrideManualPrices : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CreateSalesOrderFilter.overrideManualPrices>
  {
  }

  public abstract class recalculateDiscounts : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CreateSalesOrderFilter.recalculateDiscounts>
  {
  }

  public abstract class overrideManualDiscounts : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CreateSalesOrderFilter.overrideManualDiscounts>
  {
  }

  public abstract class overrideManualDocGroupDiscounts : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CreateSalesOrderFilter.overrideManualDocGroupDiscounts>
  {
  }

  public abstract class confirmManualAmount : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CreateSalesOrderFilter.confirmManualAmount>
  {
  }
}
