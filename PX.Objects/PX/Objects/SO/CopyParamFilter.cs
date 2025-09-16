// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.CopyParamFilter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using PX.Objects.IN;
using System;

#nullable enable
namespace PX.Objects.SO;

[Serializable]
public class CopyParamFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _OrderType;
  protected bool? _RecalcUnitPrices;
  protected bool? _OverrideManualPrices;
  protected bool? _RecalcDiscounts;
  protected bool? _OverrideManualDiscounts;
  protected string _OrderNbr;

  [PXDBString(2, IsFixed = true, InputMask = ">aa")]
  [PXDefault(typeof (Search<SOSetup.defaultOrderType>))]
  [PXSelector(typeof (Search2<SOOrderType.orderType, InnerJoin<SOOrderTypeOperation, On2<SOOrderTypeOperation.FK.OrderType, And<SOOrderTypeOperation.operation, Equal<SOOrderType.defaultOperation>>>>>))]
  [PXRestrictor(typeof (Where<SOOrderTypeOperation.iNDocType, NotEqual<INTranType.transfer>, Or<FeatureInstalled<FeaturesSet.warehouse>>>), "'{0}' cannot be found in the system.", new Type[] {typeof (CopyParamFilter.orderType)})]
  [PXRestrictor(typeof (Where<SOOrderType.requireAllocation, NotEqual<True>, Or<AllocationAllowed>>), "'{0}' cannot be found in the system.", new Type[] {typeof (CopyParamFilter.orderType)})]
  [PXRestrictor(typeof (Where<SOOrderType.active, Equal<True>>), "'{0}' cannot be found in the system.", new Type[] {typeof (CopyParamFilter.orderType)})]
  [PXRestrictor(typeof (Where<SOOrderType.behavior, NotEqual<SOBehavior.bL>>), "'{0}' cannot be found in the system.", new Type[] {typeof (CopyParamFilter.orderType)})]
  [PXUIField(DisplayName = "Order Type")]
  public virtual string OrderType
  {
    get => this._OrderType;
    set => this._OrderType = value;
  }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Recalculate Unit Prices")]
  public virtual bool? RecalcUnitPrices
  {
    get => this._RecalcUnitPrices;
    set => this._RecalcUnitPrices = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Override Manual Prices")]
  [PXFormula(typeof (BqlOperand<False, IBqlBool>.When<BqlOperand<CopyParamFilter.recalcUnitPrices, IBqlBool>.IsEqual<False>>.Else<CopyParamFilter.overrideManualPrices>))]
  public virtual bool? OverrideManualPrices
  {
    get => this._OverrideManualPrices;
    set => this._OverrideManualPrices = value;
  }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Recalculate Discounts")]
  public virtual bool? RecalcDiscounts
  {
    get => this._RecalcDiscounts;
    set => this._RecalcDiscounts = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Override Manual Discounts")]
  [PXFormula(typeof (BqlOperand<False, IBqlBool>.When<BqlOperand<CopyParamFilter.recalcDiscounts, IBqlBool>.IsEqual<False>>.Else<CopyParamFilter.overrideManualDiscounts>))]
  public virtual bool? OverrideManualDiscounts
  {
    get => this._OverrideManualDiscounts;
    set => this._OverrideManualDiscounts = value;
  }

  [PXDBString(15, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXUIField]
  [PXDefault(typeof (Search2<Numbering.newSymbol, InnerJoin<SOOrderType, On<Numbering.numberingID, Equal<SOOrderType.orderNumberingID>>>, Where<SOOrderType.orderType, Equal<Current<CopyParamFilter.orderType>>, And<Numbering.userNumbering, Equal<False>>>>))]
  public virtual string OrderNbr
  {
    get => this._OrderNbr;
    set => this._OrderNbr = value;
  }

  public abstract class orderType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CopyParamFilter.orderType>
  {
  }

  public abstract class recalcUnitPrices : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CopyParamFilter.recalcUnitPrices>
  {
  }

  public abstract class overrideManualPrices : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CopyParamFilter.overrideManualPrices>
  {
  }

  public abstract class recalcDiscounts : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CopyParamFilter.recalcDiscounts>
  {
  }

  public abstract class overrideManualDiscounts : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CopyParamFilter.overrideManualDiscounts>
  {
  }

  public abstract class orderNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CopyParamFilter.orderNbr>
  {
  }
}
