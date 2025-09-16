// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.InventoryRelease.Accumulators.ReceiptStatus
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.IN.InventoryRelease.Accumulators;

[PXHidden]
[ReceiptStatus.Accumulator]
public class ReceiptStatus : INReceiptStatus
{
  [PXDBLongIdentity]
  [PXDefault]
  public override long? ReceiptID
  {
    get => this._ReceiptID;
    set => this._ReceiptID = value;
  }

  [PXDBInt(IsKey = true)]
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
  [PXDefault]
  public override int? CostSiteID
  {
    get => this._CostSiteID;
    set => this._CostSiteID = value;
  }

  [Account(IsKey = true)]
  [PXDefault]
  public override int? AccountID
  {
    get => this._AccountID;
    set => this._AccountID = value;
  }

  [SubAccount(typeof (ReceiptStatus.accountID), IsKey = true)]
  [PXDefault]
  public override int? SubID
  {
    get => this._SubID;
    set => this._SubID = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXDefault("N")]
  public override 
  #nullable disable
  string LayerType
  {
    get => this._LayerType;
    set => this._LayerType = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXDefault("F")]
  public override string ValMethod
  {
    get => this._ValMethod;
    set => this._ValMethod = value;
  }

  [PXDBString(1, IsKey = true, IsFixed = true)]
  [PXDefault]
  public override string DocType { get; set; }

  [PXDBString(15, IsUnicode = true, IsKey = true)]
  [PXDefault]
  public override string ReceiptNbr
  {
    get => this._ReceiptNbr;
    set => this._ReceiptNbr = value;
  }

  [PXDBString(100, IsUnicode = true, IsKey = true)]
  [PXDefault("")]
  public override string LotSerialNbr
  {
    get => this._LotSerialNbr;
    set => this._LotSerialNbr = value;
  }

  [PXDBDate]
  [PXDefault]
  public override DateTime? ReceiptDate
  {
    get => this._ReceiptDate;
    set => this._ReceiptDate = value;
  }

  /// <summary>
  /// The unbound field is used only during inventory documents release for indicating that <see cref="P:PX.Objects.IN.INReceiptStatus.OrigQty" /> should be overriden.
  /// Used for PO Corrections scenarios when there are both Issue and Receipt transactions and we need to separate Receipts only.
  /// </summary>
  [PXBool]
  [PXDefault(false)]
  public bool? OverrideOrigQty { get; set; }

  public new abstract class receiptID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  ReceiptStatus.receiptID>
  {
  }

  public new abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ReceiptStatus.inventoryID>
  {
  }

  public new abstract class costSubItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ReceiptStatus.costSubItemID>
  {
  }

  public new abstract class costSiteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ReceiptStatus.costSiteID>
  {
  }

  public new abstract class accountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ReceiptStatus.accountID>
  {
  }

  public new abstract class subID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ReceiptStatus.subID>
  {
  }

  public new abstract class layerType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ReceiptStatus.layerType>
  {
  }

  public new abstract class valMethod : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ReceiptStatus.valMethod>
  {
  }

  public new abstract class docType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ReceiptStatus.docType>
  {
  }

  public new abstract class receiptNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ReceiptStatus.receiptNbr>
  {
  }

  public new abstract class lotSerialNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ReceiptStatus.lotSerialNbr>
  {
  }

  public new abstract class receiptDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ReceiptStatus.receiptDate>
  {
  }

  public new abstract class origQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ReceiptStatus.origQty>
  {
  }

  public new abstract class qtyOnHand : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ReceiptStatus.qtyOnHand>
  {
  }

  public abstract class overrideOrigQty : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ReceiptStatus.overrideOrigQty>
  {
  }

  public class AccumulatorAttribute : PXAccumulatorAttribute
  {
    public AccumulatorAttribute() => this.SingleRecord = true;

    protected virtual bool PrepareInsert(
      PXCache cache,
      object row,
      PXAccumulatorCollection columns)
    {
      if (!base.PrepareInsert(cache, row, columns))
        return false;
      ReceiptStatus receiptStatus = (ReceiptStatus) row;
      columns.Update<ReceiptStatus.origQty>(receiptStatus.OverrideOrigQty.GetValueOrDefault() ? (PXDataFieldAssign.AssignBehavior) 0 : (PXDataFieldAssign.AssignBehavior) 4);
      columns.Update<ReceiptStatus.qtyOnHand>((PXDataFieldAssign.AssignBehavior) 1);
      PXAccumulatorCollection accumulatorCollection = columns;
      Decimal? qtyOnHand = ((INReceiptStatus) row).QtyOnHand;
      // ISSUE: variable of a boxed type
      __Boxed<Decimal?> local = (ValueType) (qtyOnHand.HasValue ? new Decimal?(-qtyOnHand.GetValueOrDefault()) : new Decimal?());
      accumulatorCollection.Restrict<ReceiptStatus.qtyOnHand>((PXComp) 3, (object) local);
      return true;
    }

    public virtual bool PersistInserted(PXCache cache, object row)
    {
      try
      {
        return base.PersistInserted(cache, row);
      }
      catch (PXLockViolationException ex)
      {
        ReceiptStatus receiptStatus = (ReceiptStatus) row;
        BqlCommand bqlCommand1 = (BqlCommand) new Select<ReceiptStatus, Where<ReceiptStatus.inventoryID, Equal<Current<ReceiptStatus.inventoryID>>, And<ReceiptStatus.costSiteID, Equal<Current<ReceiptStatus.costSiteID>>, And<ReceiptStatus.costSubItemID, Equal<Current<ReceiptStatus.costSubItemID>>, And<ReceiptStatus.accountID, Equal<Current<ReceiptStatus.accountID>>>>>>>();
        BqlCommand bqlCommand2 = !(receiptStatus.ValMethod == "S") ? bqlCommand1.WhereAnd<Where<ReceiptStatus.subID, Equal<Current<ReceiptStatus.subID>>>>() : bqlCommand1.WhereAnd<Where<ReceiptStatus.subID, Equal<Current<ReceiptStatus.subID>>, And<ReceiptStatus.lotSerialNbr, Equal<Current<ReceiptStatus.lotSerialNbr>>>>>();
        if (((Decimal?) ((INReceiptStatus) new PXView(cache.Graph, true, bqlCommand2).SelectSingleBound(new object[1]
        {
          (object) receiptStatus
        }, Array.Empty<object>()))?.QtyOnHand).GetValueOrDefault() + receiptStatus.QtyOnHand.GetValueOrDefault() < 0M)
          throw new PXRestartOperationException((Exception) ex);
        throw;
      }
    }
  }
}
