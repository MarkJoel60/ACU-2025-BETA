// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.InventoryRelease.Accumulators.CostStatuses.OversoldCostStatus
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.IN.InventoryRelease.Accumulators.CostStatuses.Abstraction;
using System;

#nullable enable
namespace PX.Objects.IN.InventoryRelease.Accumulators.CostStatuses;

[PXHidden]
[OversoldCostStatus.Accumulator]
public class OversoldCostStatus : INCostStatus
{
  [CostIdentity(new Type[] {typeof (INTranCost.costID)})]
  [PXDefault]
  public override long? CostID
  {
    get => this._CostID;
    set => this._CostID = value;
  }

  [PXDBInt(IsKey = true)]
  [PXForeignSelector(typeof (INTran.inventoryID))]
  [PXDefault]
  public override int? InventoryID
  {
    get => this._InventoryID;
    set => this._InventoryID = value;
  }

  [SubItem(IsKey = true)]
  [PXDefault]
  public override int? CostSubItemID
  {
    get => this._CostSubItemID;
    set => this._CostSubItemID = value;
  }

  [PXDBInt(IsKey = true)]
  [PX.Objects.IN.InventoryRelease.Accumulators.CostStatuses.Abstraction.CostSiteID]
  [PXDefault]
  public override int? CostSiteID
  {
    get => this._CostSiteID;
    set => this._CostSiteID = value;
  }

  [PXDBInt(IsKey = true)]
  [PXDefault]
  public override int? AccountID
  {
    get => this._AccountID;
    set => this._AccountID = value;
  }

  [SubAccount(IsKey = true)]
  [PXDefault]
  public override int? SubID
  {
    get => this._SubID;
    set => this._SubID = value;
  }

  [PXDBString(1, IsFixed = true, IsKey = true)]
  [PXDefault("O")]
  public override 
  #nullable disable
  string LayerType
  {
    get => this._LayerType;
    set => this._LayerType = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXDefault]
  public override string ValMethod
  {
    get => this._ValMethod;
    set => this._ValMethod = value;
  }

  [PXDBString(15, IsUnicode = true, IsKey = true)]
  [PXDefault("OVERSOLD")]
  public override string ReceiptNbr
  {
    get => this._ReceiptNbr;
    set => this._ReceiptNbr = value;
  }

  [PXDBDate]
  [PXDefault(TypeCode.DateTime, "01/01/1900")]
  public override DateTime? ReceiptDate
  {
    get => this._ReceiptDate;
    set => this._ReceiptDate = value;
  }

  [PXDBString(100, IsUnicode = true)]
  public override string LotSerialNbr
  {
    get => this._LotSerialNbr;
    set => this._LotSerialNbr = value;
  }

  [PXDBDecimal(6)]
  [PXDefault(TypeCode.Decimal, "0.0", typeof (Coalesce<SearchFor<INItemSiteSettings.negativeCost>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INItemSiteSettings.inventoryID, Equal<BqlField<OversoldCostStatus.inventoryID, IBqlInt>.FromCurrent>>>>>.And<BqlOperand<INItemSiteSettings.siteID, IBqlInt>.IsEqual<BqlField<INCostStatus.siteID, IBqlInt>.FromCurrent>>>, SearchFor<INItemCost.lastCost>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INItemCost.inventoryID, Equal<BqlField<OversoldCostStatus.inventoryID, IBqlInt>.FromCurrent>>>>>.And<BqlOperand<INItemCost.curyID, IBqlString>.IsEqual<BqlField<INRegister.branchBaseCuryID, IBqlString>.FromCurrent>>>>))]
  public override Decimal? UnitCost
  {
    get => this._UnitCost;
    set => this._UnitCost = value;
  }

  public new abstract class costID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  OversoldCostStatus.costID>
  {
  }

  public new abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  OversoldCostStatus.inventoryID>
  {
  }

  public new abstract class costSubItemID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    OversoldCostStatus.costSubItemID>
  {
  }

  public new abstract class costSiteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  OversoldCostStatus.costSiteID>
  {
  }

  public new abstract class accountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  OversoldCostStatus.accountID>
  {
  }

  public new abstract class subID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  OversoldCostStatus.subID>
  {
  }

  public new abstract class layerType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    OversoldCostStatus.layerType>
  {
  }

  public new abstract class valMethod : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    OversoldCostStatus.valMethod>
  {
  }

  public new abstract class receiptNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    OversoldCostStatus.receiptNbr>
  {
  }

  public new abstract class receiptDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    OversoldCostStatus.receiptDate>
  {
  }

  public new abstract class lotSerialNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    OversoldCostStatus.lotSerialNbr>
  {
  }

  public new abstract class origQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  OversoldCostStatus.origQty>
  {
  }

  public new abstract class qtyOnHand : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    OversoldCostStatus.qtyOnHand>
  {
  }

  public new abstract class unitCost : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    OversoldCostStatus.unitCost>
  {
  }

  public class AccumulatorAttribute : CostStatusAccumulatorAttribute
  {
    public AccumulatorAttribute()
      : base(typeof (OversoldCostStatus.qtyOnHand), typeof (INCostStatus.totalCost), typeof (OversoldCostStatus.inventoryID), typeof (OversoldCostStatus.costSubItemID), typeof (OversoldCostStatus.costSiteID), typeof (OversoldCostStatus.layerType), typeof (OversoldCostStatus.receiptNbr))
    {
    }

    protected override bool PrepareInsert(
      PXCache cache,
      object row,
      PXAccumulatorCollection columns)
    {
      if (!base.PrepareInsert(cache, row, columns))
        return false;
      columns.Update<INCostStatus.valMethod>((PXDataFieldAssign.AssignBehavior) 0);
      return true;
    }
  }
}
