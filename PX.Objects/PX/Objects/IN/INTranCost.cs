// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INTranCost
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CM;
using PX.Objects.GL;
using PX.Objects.IN.InventoryRelease.Accumulators.CostStatuses;
using System;

#nullable enable
namespace PX.Objects.IN;

[PXCacheName("IN Transaction Cost")]
[Serializable]
public class INTranCost : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _DocType;
  protected string _TranType;
  protected string _RefNbr;
  protected int? _LineNbr;
  protected long? _CostID;
  protected string _CostDocType;
  protected string _CostRefNbr;
  protected short? _InvtMult;
  protected Decimal? _Qty;
  protected Decimal? _OversoldQty;
  protected Decimal? _TranCost;
  protected Decimal? _OversoldTranCost;
  protected Decimal? _VarCost;
  protected Decimal? _TranAmt;
  protected DateTime? _TranDate;
  protected string _FinPeriodID;
  protected string _TranPeriodID;
  protected int? _InventoryID;
  protected int? _CostSubItemID;
  protected int? _CostSiteID;
  protected string _LotSerialNbr;
  protected int? _InvtAcctID;
  protected int? _InvtSubID;
  protected int? _COGSAcctID;
  protected int? _COGSSubID;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;
  protected byte[] _tstamp;
  protected Decimal? _UnitCost;

  [PXDBString(1, IsKey = true, IsFixed = true)]
  [PXDefault]
  public virtual string DocType
  {
    get => this._DocType;
    set => this._DocType = value;
  }

  [PXDBString(3, IsFixed = true)]
  [PXDefault]
  [INTranType.List]
  [PXUIField(DisplayName = "Transaction Type")]
  public virtual string TranType
  {
    get => this._TranType;
    set => this._TranType = value;
  }

  [PXDBString(15, IsUnicode = true, IsKey = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Ref. Number")]
  public virtual string RefNbr
  {
    get => this._RefNbr;
    set => this._RefNbr = value;
  }

  [PXDBInt(IsKey = true)]
  [PXDefault]
  [PXParent(typeof (Select<INTran, Where<INTran.docType, Equal<Current<INTranCost.docType>>, And<INTran.refNbr, Equal<Current<INTranCost.refNbr>>, And<INTran.lineNbr, Equal<Current<INTranCost.lineNbr>>>>>>), LeaveChildren = true)]
  public virtual int? LineNbr
  {
    get => this._LineNbr;
    set => this._LineNbr = value;
  }

  [PXDBLong(IsKey = true)]
  [PXDefault]
  public virtual long? CostID
  {
    get => this._CostID;
    set => this._CostID = value;
  }

  [PXDBString(1, IsKey = true, IsFixed = true)]
  [PXDefault]
  public virtual string CostDocType
  {
    get => this._CostDocType;
    set => this._CostDocType = value;
  }

  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = "")]
  [PXDefault]
  public virtual string CostRefNbr
  {
    get => this._CostRefNbr;
    set => this._CostRefNbr = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? IsOversold { get; set; }

  [PXDBShort]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Inventory Multiplier")]
  public virtual short? InvtMult
  {
    get => this._InvtMult;
    set => this._InvtMult = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Qty.")]
  public virtual Decimal? Qty
  {
    get => this._Qty;
    set => this._Qty = value;
  }

  [PXDBDecimal(6)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? OversoldQty
  {
    get => this._OversoldQty;
    set => this._OversoldQty = value;
  }

  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Transaction Cost")]
  public virtual Decimal? TranCost
  {
    get => this._TranCost;
    set => this._TranCost = value;
  }

  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? OversoldTranCost
  {
    get => this._OversoldTranCost;
    set => this._OversoldTranCost = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? VarCost
  {
    get => this._VarCost;
    set => this._VarCost = value;
  }

  [PXDecimal(4)]
  public virtual Decimal? TranAmt
  {
    get => this._TranAmt;
    set => this._TranAmt = value;
  }

  [PXDBDate]
  [PXDefault(TypeCode.DateTime, "01/01/1900")]
  [PXUIField(DisplayName = "Transaction Date")]
  public virtual DateTime? TranDate
  {
    get => this._TranDate;
    set => this._TranDate = value;
  }

  [PXDBString(6, IsFixed = true)]
  [PXDefault("")]
  public virtual string FinPeriodID
  {
    get => this._FinPeriodID;
    set => this._FinPeriodID = value;
  }

  [PXDBString(6, IsFixed = true)]
  [PXDefault("")]
  public virtual string TranPeriodID
  {
    get => this._TranPeriodID;
    set => this._TranPeriodID = value;
  }

  [PXDBInt]
  [PXDefault]
  public virtual int? InventoryID
  {
    get => this._InventoryID;
    set => this._InventoryID = value;
  }

  /// <summary>
  /// The identifier of the <see cref="T:PX.Objects.IN.INSite">warehouse</see>
  /// </summary>
  [PXDBInt]
  [PXDefault]
  public virtual int? SiteID { get; set; }

  [SubItem]
  [PXDefault]
  public virtual int? CostSubItemID
  {
    get => this._CostSubItemID;
    set => this._CostSubItemID = value;
  }

  [PXDBInt]
  [PXDefault]
  public virtual int? CostSiteID
  {
    get => this._CostSiteID;
    set => this._CostSiteID = value;
  }

  [PXDBString(100, IsUnicode = true)]
  [PXDefault(typeof (Coalesce<Search<SpecificCostStatus.lotSerialNbr, Where<SpecificCostStatus.costID, Equal<Current<INTranCost.costID>>>>, Search<SpecificTransitCostStatus.lotSerialNbr, Where<SpecificTransitCostStatus.costID, Equal<Current<INTranCost.costID>>>>>))]
  public virtual string LotSerialNbr
  {
    get => this._LotSerialNbr;
    set => this._LotSerialNbr = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [Obsolete("This item has been deprecated and will be removed in Acumatica ERP 2023 R1. Use the CostType field instead.")]
  public virtual bool? IsVirtual { get; set; }

  [Account]
  [PXDefault]
  public virtual int? InvtAcctID
  {
    get => this._InvtAcctID;
    set => this._InvtAcctID = value;
  }

  [SubAccount(typeof (INTranCost.invtAcctID))]
  [PXDefault]
  public virtual int? InvtSubID
  {
    get => this._InvtSubID;
    set => this._InvtSubID = value;
  }

  [Account]
  public virtual int? COGSAcctID
  {
    get => this._COGSAcctID;
    set => this._COGSAcctID = value;
  }

  [SubAccount(typeof (INTranCost.cOGSAcctID))]
  public virtual int? COGSSubID
  {
    get => this._COGSSubID;
    set => this._COGSSubID = value;
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

  [PXUIField(DisplayName = "Created")]
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

  [PXDecimal(6)]
  public virtual Decimal? QtyOnHand
  {
    get => this._Qty;
    set => this._Qty = value;
  }

  [PXDecimal(6)]
  public virtual Decimal? UnitCost
  {
    get => this._UnitCost;
    set => this._UnitCost = value;
  }

  [PXDecimal(4)]
  public virtual Decimal? TotalCost
  {
    get => this._TranCost;
    set => this._TranCost = value;
  }

  [PXDBString(1, IsUnicode = false, IsFixed = true)]
  [PXDefault(typeof (INTranCost.costType.normal))]
  public virtual string CostType { get; set; }

  public class PK : 
    PrimaryKeyOf<INTranCost>.By<INTranCost.docType, INTranCost.refNbr, INTranCost.lineNbr, INTranCost.costID, INTranCost.costDocType, INTranCost.costRefNbr>
  {
    public static INTranCost Find(
      PXGraph graph,
      string docType,
      string refNbr,
      int? lineNbr,
      long? costID,
      string costDocType,
      string costRefNbr,
      PKFindOptions options = 0)
    {
      return (INTranCost) PrimaryKeyOf<INTranCost>.By<INTranCost.docType, INTranCost.refNbr, INTranCost.lineNbr, INTranCost.costID, INTranCost.costDocType, INTranCost.costRefNbr>.FindBy(graph, (object) docType, (object) refNbr, (object) lineNbr, (object) costID, (object) costDocType, (object) costRefNbr, options);
    }
  }

  public static class FK
  {
    public class Register : 
      PrimaryKeyOf<INRegister>.By<INRegister.docType, INRegister.refNbr>.ForeignKeyOf<INTranCost>.By<INTranCost.docType, INTranCost.refNbr>
    {
    }

    public class Tran : 
      PrimaryKeyOf<INTran>.By<INTran.docType, INTran.refNbr, INTran.lineNbr>.ForeignKeyOf<INTranCost>.By<INTranCost.docType, INTranCost.refNbr, INTranCost.lineNbr>
    {
    }

    public class CostRegister : 
      PrimaryKeyOf<INRegister>.By<INRegister.docType, INRegister.refNbr>.ForeignKeyOf<INTranCost>.By<INTranCost.costDocType, INTranCost.costRefNbr>
    {
    }

    public class InventoryItem : 
      PrimaryKeyOf<InventoryItem>.By<InventoryItem.inventoryID>.ForeignKeyOf<INTranCost>.By<INTranCost.inventoryID>
    {
    }

    public class CostSubItem : 
      PrimaryKeyOf<INSubItem>.By<INSubItem.subItemID>.ForeignKeyOf<INTranCost>.By<INTranCost.costSubItemID>
    {
    }

    public class CostSite : 
      PrimaryKeyOf<INSite>.By<INSite.siteID>.ForeignKeyOf<INTranCost>.By<INTranCost.costSiteID>
    {
    }

    public class COGSAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<INTranCost>.By<INTranCost.cOGSAcctID>
    {
    }

    public class COGSSubaccount : 
      PrimaryKeyOf<Sub>.By<Sub.subID>.ForeignKeyOf<INTranCost>.By<INTranCost.cOGSSubID>
    {
    }

    public class InventoryAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<INTranCost>.By<INTranCost.invtAcctID>
    {
    }

    public class InventorySubaccount : 
      PrimaryKeyOf<Sub>.By<Sub.subID>.ForeignKeyOf<INTranCost>.By<INTranCost.invtSubID>
    {
    }
  }

  public abstract class docType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INTranCost.docType>
  {
  }

  public abstract class tranType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INTranCost.tranType>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INTranCost.refNbr>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INTranCost.lineNbr>
  {
  }

  public abstract class costID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  INTranCost.costID>
  {
  }

  public abstract class costDocType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INTranCost.costDocType>
  {
  }

  public abstract class costRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INTranCost.costRefNbr>
  {
  }

  public abstract class isOversold : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  INTranCost.isOversold>
  {
  }

  public abstract class invtMult : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  INTranCost.invtMult>
  {
  }

  public abstract class qty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INTranCost.qty>
  {
  }

  public abstract class oversoldQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INTranCost.oversoldQty>
  {
  }

  public abstract class tranCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INTranCost.tranCost>
  {
  }

  public abstract class oversoldTranCost : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INTranCost.oversoldTranCost>
  {
  }

  public abstract class varCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INTranCost.varCost>
  {
  }

  public abstract class tranAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INTranCost.tranAmt>
  {
  }

  public abstract class tranDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  INTranCost.tranDate>
  {
  }

  public abstract class finPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INTranCost.finPeriodID>
  {
  }

  public abstract class tranPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INTranCost.tranPeriodID>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INTranCost.inventoryID>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INTranCost.siteID>
  {
  }

  public abstract class costSubItemID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INTranCost.costSubItemID>
  {
  }

  public abstract class costSiteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INTranCost.costSiteID>
  {
  }

  public abstract class lotSerialNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INTranCost.lotSerialNbr>
  {
  }

  [Obsolete("This item has been deprecated and will be removed in Acumatica ERP 2023 R1. Use the CostType field instead.")]
  public abstract class isVirtual : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  INTranCost.isVirtual>
  {
  }

  public abstract class invtAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INTranCost.invtAcctID>
  {
  }

  public abstract class invtSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INTranCost.invtSubID>
  {
  }

  public abstract class cOGSAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INTranCost.cOGSAcctID>
  {
  }

  public abstract class cOGSSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INTranCost.cOGSSubID>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  INTranCost.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INTranCost.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INTranCost.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  INTranCost.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INTranCost.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INTranCost.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  INTranCost.Tstamp>
  {
  }

  public abstract class qtyOnHand : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INTranCost.qtyOnHand>
  {
  }

  public abstract class unitCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INTranCost.unitCost>
  {
  }

  public abstract class totalCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INTranCost.totalCost>
  {
  }

  public abstract class costType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INTranCost.costType>
  {
    public const string Normal = "N";
    public const string DropShip = "D";
    public const string DropShipPPVorLC = "P";
    public const string TransitTransfer = "T";

    public class normal : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    INTranCost.costType.normal>
    {
      public normal()
        : base("N")
      {
      }
    }

    public class dropShip : BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    INTranCost.costType.dropShip>
    {
      public dropShip()
        : base("D")
      {
      }
    }

    public class dropShipPPVorLC : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      INTranCost.costType.dropShipPPVorLC>
    {
      public dropShipPPVorLC()
        : base("P")
      {
      }
    }

    public class transitTransfer : 
      BqlType<
      #nullable enable
      IBqlString, string>.Constant<
      #nullable disable
      INTranCost.costType.transitTransfer>
    {
      public transitTransfer()
        : base("T")
      {
      }
    }
  }
}
