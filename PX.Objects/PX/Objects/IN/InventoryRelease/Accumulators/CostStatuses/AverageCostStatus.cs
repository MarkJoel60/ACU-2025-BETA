// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.InventoryRelease.Accumulators.CostStatuses.AverageCostStatus
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.IN.InventoryRelease.Accumulators.CostStatuses.Abstraction;
using System;

#nullable enable
namespace PX.Objects.IN.InventoryRelease.Accumulators.CostStatuses;

[PXHidden]
[CostStatusAccumulator(typeof (AverageCostStatus.qtyOnHand), typeof (INCostStatus.totalCost), typeof (AverageCostStatus.inventoryID), typeof (AverageCostStatus.costSubItemID), typeof (AverageCostStatus.costSiteID), typeof (AverageCostStatus.layerType), typeof (AverageCostStatus.receiptNbr))]
public class AverageCostStatus : INCostStatus
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
  [PXDefault]
  public override 
  #nullable disable
  string LayerType
  {
    get => this._LayerType;
    set => this._LayerType = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXDefault("A")]
  public override string ValMethod
  {
    get => this._ValMethod;
    set => this._ValMethod = value;
  }

  [PXDBString(15, IsUnicode = true, IsKey = true)]
  [PXDefault("ZZZ")]
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

  public new abstract class costID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  AverageCostStatus.costID>
  {
  }

  public new abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  AverageCostStatus.inventoryID>
  {
  }

  public new abstract class costSubItemID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    AverageCostStatus.costSubItemID>
  {
  }

  public new abstract class costSiteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  AverageCostStatus.costSiteID>
  {
  }

  public new abstract class accountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  AverageCostStatus.accountID>
  {
  }

  public new abstract class subID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  AverageCostStatus.subID>
  {
  }

  public new abstract class layerType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AverageCostStatus.layerType>
  {
  }

  public new abstract class valMethod : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AverageCostStatus.valMethod>
  {
  }

  public new abstract class receiptNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AverageCostStatus.receiptNbr>
  {
  }

  public new abstract class receiptDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    AverageCostStatus.receiptDate>
  {
  }

  public new abstract class lotSerialNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AverageCostStatus.lotSerialNbr>
  {
  }

  public new abstract class origQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  AverageCostStatus.origQty>
  {
  }

  public new abstract class qtyOnHand : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    AverageCostStatus.qtyOnHand>
  {
  }
}
