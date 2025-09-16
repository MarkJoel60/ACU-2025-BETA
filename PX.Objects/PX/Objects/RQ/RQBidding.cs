// Decompiled with JetBrains decompiler
// Type: PX.Objects.RQ.RQBidding
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AP;
using PX.Objects.CM;
using PX.Objects.CS;
using PX.Objects.IN;
using System;

#nullable enable
namespace PX.Objects.RQ;

[Serializable]
public class RQBidding : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected bool? _Selected;
  protected int? _LineID;
  protected 
  #nullable disable
  string _ReqNbr;
  protected int? _LineNbr;
  protected int? _VendorID;
  protected int? _VendorLocationID;
  protected string _QuoteNumber;
  protected Decimal? _QuoteQty;
  protected string _CuryID;
  protected long? _CuryInfoID;
  protected Decimal? _CuryQuoteUnitCost;
  protected Decimal? _QuoteUnitCost;
  protected Decimal? _OrderQty;
  protected Decimal? _CuryQuoteExtCost;
  protected Decimal? _QuoteExtCost;
  protected Decimal? _MinQty;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Selected")]
  public virtual bool? Selected
  {
    [PXDependsOnFields(new System.Type[] {typeof (RQBidding.orderQty)})] get
    {
      if (this._Selected.HasValue)
        return this._Selected;
      Decimal? orderQty = this._OrderQty;
      Decimal num = 0M;
      return new bool?(orderQty.GetValueOrDefault() > num & orderQty.HasValue);
    }
    set => this._Selected = value;
  }

  [PXDBIdentity(IsKey = true)]
  public virtual int? LineID
  {
    get => this._LineID;
    set => this._LineID = value;
  }

  [PXDBString(15, IsUnicode = true, InputMask = "")]
  [PXDefault(typeof (RQRequisitionLine.reqNbr))]
  [PXForeignReference(typeof (RQBidding.FK.Requisition))]
  public virtual string ReqNbr
  {
    get => this._ReqNbr;
    set => this._ReqNbr = value;
  }

  [PXDBInt]
  [PXUIField]
  [PXParent(typeof (RQBidding.FK.RequisitionLine))]
  [PXDefault(typeof (RQBiddingState.lineNbr))]
  public virtual int? LineNbr
  {
    get => this._LineNbr;
    set => this._LineNbr = value;
  }

  [PXDefault]
  [VendorNonEmployeeActive]
  public virtual int? VendorID
  {
    get => this._VendorID;
    set => this._VendorID = value;
  }

  [LocationID(typeof (Where<PX.Objects.CR.Location.bAccountID, Equal<Current<RQBidding.vendorID>>>))]
  [PXDefaultValidate(typeof (Search<PX.Objects.AP.Vendor.defLocationID, Where<PX.Objects.AP.Vendor.bAccountID, Equal<Current<RQBidding.vendorID>>>>), typeof (Search<RQBidding.reqNbr, Where<RQBidding.reqNbr, Equal<Current<RQBidding.reqNbr>>, And<RQBidding.lineNbr, Equal<Current<RQBidding.lineNbr>>, And<RQBidding.vendorID, Equal<Current<RQBidding.vendorID>>, And<RQBidding.vendorLocationID, Equal<Required<RQBidding.vendorLocationID>>>>>>>))]
  [PXParent(typeof (Select<RQBiddingVendor, Where<RQBiddingVendor.reqNbr, Equal<Current<RQBidding.reqNbr>>, And<RQBiddingVendor.vendorID, Equal<Current<RQBidding.vendorID>>, And<RQBiddingVendor.vendorLocationID, Equal<Current<RQBidding.vendorLocationID>>>>>>))]
  public virtual int? VendorLocationID
  {
    get => this._VendorLocationID;
    set => this._VendorLocationID = value;
  }

  [PXDBString(20, IsUnicode = true)]
  [PXUIField(DisplayName = "Bid Number")]
  public virtual string QuoteNumber
  {
    get => this._QuoteNumber;
    set => this._QuoteNumber = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  [PXFormula(null, typeof (SumCalc<RQBiddingVendor.totalQuoteQty>))]
  public virtual Decimal? QuoteQty
  {
    get => this._QuoteQty;
    set => this._QuoteQty = value;
  }

  [PXString(5)]
  [PXDefault]
  [PXFormula(typeof (Selector<RQBidding.vendorID, PX.Objects.AP.Vendor.curyID>))]
  [PXUIField]
  [PXSelector(typeof (PX.Objects.CM.Currency.curyID))]
  public virtual string CuryID
  {
    get => this._CuryID;
    set => this._CuryID = value;
  }

  [PXDBLong]
  [CurrencyInfo(typeof (RQBiddingVendor.curyInfoID), ModuleCode = "PO")]
  public virtual long? CuryInfoID
  {
    get => this._CuryInfoID;
    set => this._CuryInfoID = value;
  }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBCurrency(typeof (Search<CommonSetup.decPlPrcCst>), typeof (RQBidding.curyInfoID), typeof (RQBidding.quoteUnitCost))]
  [PXUIField]
  public virtual Decimal? CuryQuoteUnitCost
  {
    get => this._CuryQuoteUnitCost;
    set => this._CuryQuoteUnitCost = value;
  }

  [PXDBPriceCost]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QuoteUnitCost
  {
    get => this._QuoteUnitCost;
    set => this._QuoteUnitCost = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  [PXFormula(null, typeof (AddCalc<RQRequisitionLine.biddingQty>))]
  public virtual Decimal? OrderQty
  {
    get => this._OrderQty;
    set => this._OrderQty = value;
  }

  [PXDBCurrency(typeof (RQBidding.curyInfoID), typeof (RQBidding.quoteExtCost))]
  [PXUIField]
  [PXFormula(typeof (Mult<RQBidding.quoteQty, RQBidding.curyQuoteUnitCost>))]
  [PXFormula(null, typeof (SumCalc<RQBiddingVendor.curyTotalQuoteExtCost>))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryQuoteExtCost
  {
    get => this._CuryQuoteExtCost;
    set => this._CuryQuoteExtCost = value;
  }

  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? QuoteExtCost
  {
    get => this._QuoteExtCost;
    set => this._QuoteExtCost = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? MinQty
  {
    get => this._MinQty;
    set => this._MinQty = value;
  }

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

  public class PK : PrimaryKeyOf<RQBidding>.By<RQBidding.lineID>
  {
    public static RQBidding Find(PXGraph graph, int? lineID, PKFindOptions options = 0)
    {
      return (RQBidding) PrimaryKeyOf<RQBidding>.By<RQBidding.lineID>.FindBy(graph, (object) lineID, options);
    }
  }

  public static class FK
  {
    public class Requisition : 
      PrimaryKeyOf<RQRequisition>.By<RQRequisition.reqNbr>.ForeignKeyOf<RQBidding>.By<RQBidding.reqNbr>
    {
    }

    public class RequisitionLine : 
      PrimaryKeyOf<RQRequisitionLine>.By<RQRequisitionLine.reqNbr, RQRequisitionLine.lineNbr>.ForeignKeyOf<RQBidding>.By<RQBidding.reqNbr, RQBidding.lineNbr>
    {
    }
  }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  RQBidding.selected>
  {
  }

  public abstract class lineID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RQBidding.lineID>
  {
  }

  public abstract class reqNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RQBidding.reqNbr>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RQBidding.lineNbr>
  {
  }

  public abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RQBidding.vendorID>
  {
  }

  public abstract class vendorLocationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RQBidding.vendorLocationID>
  {
  }

  public abstract class quoteNumber : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RQBidding.quoteNumber>
  {
  }

  public abstract class quoteQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  RQBidding.quoteQty>
  {
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RQBidding.curyID>
  {
  }

  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  RQBidding.curyInfoID>
  {
  }

  public abstract class curyQuoteUnitCost : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    RQBidding.curyQuoteUnitCost>
  {
  }

  public abstract class quoteUnitCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  RQBidding.quoteUnitCost>
  {
  }

  public abstract class orderQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  RQBidding.orderQty>
  {
  }

  public abstract class curyQuoteExtCost : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    RQBidding.curyQuoteExtCost>
  {
  }

  public abstract class quoteExtCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  RQBidding.quoteExtCost>
  {
  }

  public abstract class minQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  RQBidding.minQty>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  RQBidding.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  RQBidding.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RQBidding.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    RQBidding.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  RQBidding.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RQBidding.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    RQBidding.lastModifiedDateTime>
  {
  }
}
