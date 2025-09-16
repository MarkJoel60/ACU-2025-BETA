// Decompiled with JetBrains decompiler
// Type: PX.Objects.RQ.RQBiddingVendor
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AP;
using PX.Objects.CM;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.CS.Attributes;
using PX.Objects.GL;
using PX.Objects.IN;
using PX.Objects.PO;
using System;

#nullable enable
namespace PX.Objects.RQ;

[PXCacheName("Bidding Vendor")]
[Serializable]
public class RQBiddingVendor : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _LineID;
  protected 
  #nullable disable
  string _ReqNbr;
  protected int? _VendorID;
  protected int? _VendorLocationID;
  protected int? _RemitAddressID;
  protected int? _RemitContactID;
  protected string _CuryID;
  protected long? _CuryInfoID;
  protected DateTime? _EntryDate;
  protected DateTime? _ExpireDate;
  protected DateTime? _PromisedDate;
  protected string _FOBPoint;
  protected string _ShipVia;
  protected Decimal? _TotalQuoteQty;
  protected Decimal? _CuryTotalQuoteExtCost;
  protected Decimal? _TotalQuoteExtCost;
  protected bool? _Status = new bool?(false);
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  [PXDBIdentity(IsKey = true)]
  public virtual int? LineID
  {
    get => this._LineID;
    set => this._LineID = value;
  }

  [PXDBString(15, IsUnicode = true, InputMask = "")]
  [PXDBDefault(typeof (RQRequisition.reqNbr))]
  [PXParent(typeof (RQBiddingVendor.FK.Requisition))]
  public virtual string ReqNbr
  {
    get => this._ReqNbr;
    set => this._ReqNbr = value;
  }

  [PXDefault]
  [VendorNonEmployeeActive]
  public virtual int? VendorID
  {
    get => this._VendorID;
    set => this._VendorID = value;
  }

  [LocationID(typeof (Where<PX.Objects.CR.Location.bAccountID, Equal<Current<RQBiddingVendor.vendorID>>>))]
  [PXDefaultValidate(typeof (Search<PX.Objects.AP.Vendor.defLocationID, Where<PX.Objects.AP.Vendor.bAccountID, Equal<Current<RQBiddingVendor.vendorID>>>>), typeof (Search<RQBiddingVendor.reqNbr, Where<RQBiddingVendor.reqNbr, Equal<Current<RQBiddingVendor.reqNbr>>, And<RQBiddingVendor.vendorID, Equal<Current<RQBiddingVendor.vendorID>>, And<RQBiddingVendor.vendorLocationID, Equal<Required<RQBiddingVendor.vendorLocationID>>>>>>))]
  [PXFormula(typeof (Default<RQBiddingVendor.vendorID>))]
  public virtual int? VendorLocationID
  {
    get => this._VendorLocationID;
    set => this._VendorLocationID = value;
  }

  [PXDBInt]
  [PORemitAddress(typeof (Select2<BAccount2, InnerJoin<PX.Objects.CR.Location, On<PX.Objects.CR.Location.bAccountID, Equal<BAccount2.bAccountID>>, InnerJoin<PX.Objects.CR.Address, On<PX.Objects.CR.Address.bAccountID, Equal<PX.Objects.CR.Location.bAccountID>, And<PX.Objects.CR.Address.addressID, Equal<PX.Objects.CR.Location.defAddressID>>>, LeftJoin<PX.Objects.PO.PORemitAddress, On<PX.Objects.PO.PORemitAddress.bAccountID, Equal<PX.Objects.CR.Address.bAccountID>, And<PX.Objects.PO.PORemitAddress.bAccountAddressID, Equal<PX.Objects.CR.Address.addressID>, And<PX.Objects.PO.PORemitAddress.revisionID, Equal<PX.Objects.CR.Address.revisionID>, And<PX.Objects.PO.PORemitAddress.isDefaultAddress, Equal<boolTrue>>>>>>>>, Where<PX.Objects.CR.Location.bAccountID, Equal<Current<RQBiddingVendor.vendorID>>, And<PX.Objects.CR.Location.locationID, Equal<Current<RQBiddingVendor.vendorLocationID>>>>>), Required = false)]
  [PXUIField]
  public virtual int? RemitAddressID
  {
    get => this._RemitAddressID;
    set => this._RemitAddressID = value;
  }

  [PXDBInt]
  [PORemitContact(typeof (Select2<PX.Objects.CR.Location, InnerJoin<PX.Objects.CR.Contact, On<PX.Objects.CR.Contact.bAccountID, Equal<PX.Objects.CR.Location.bAccountID>, And<PX.Objects.CR.Contact.contactID, Equal<PX.Objects.CR.Location.defContactID>>>, LeftJoin<PX.Objects.PO.PORemitContact, On<PX.Objects.PO.PORemitContact.bAccountID, Equal<PX.Objects.CR.Contact.bAccountID>, And<PX.Objects.PO.PORemitContact.bAccountContactID, Equal<PX.Objects.CR.Contact.contactID>, And<PX.Objects.PO.PORemitContact.revisionID, Equal<PX.Objects.CR.Contact.revisionID>, And<PX.Objects.PO.PORemitContact.isDefaultContact, Equal<boolTrue>>>>>>>, Where<PX.Objects.CR.Location.bAccountID, Equal<Current<RQBiddingVendor.vendorID>>, And<PX.Objects.CR.Location.locationID, Equal<Current<RQBiddingVendor.vendorLocationID>>>>>), Required = false)]
  public virtual int? RemitContactID
  {
    get => this._RemitContactID;
    set => this._RemitContactID = value;
  }

  [PXString(5)]
  [PXDefault(typeof (Coalesce<Search<PX.Objects.AP.Vendor.curyID, Where<PX.Objects.AP.Vendor.bAccountID, Equal<Current<RQBiddingVendor.vendorID>>>>, Search<Company.baseCuryID>>))]
  [PXUIField]
  [PXSelector(typeof (PX.Objects.CM.Currency.curyID))]
  public virtual string CuryID
  {
    get => this._CuryID;
    set => this._CuryID = value;
  }

  [PXDBLong]
  [CurrencyInfo(ModuleCode = "PO")]
  [PXUIField(DisplayName = "Currency")]
  public virtual long? CuryInfoID
  {
    get => this._CuryInfoID;
    set => this._CuryInfoID = value;
  }

  [PXDefault]
  [PXDBDate]
  [PXUIField]
  public virtual DateTime? EntryDate
  {
    get => this._EntryDate;
    set => this._EntryDate = value;
  }

  [PXDBDate]
  [PXUIField]
  public virtual DateTime? ExpireDate
  {
    get => this._ExpireDate;
    set => this._ExpireDate = value;
  }

  [PXDBDate]
  [PXUIField]
  public virtual DateTime? PromisedDate
  {
    get => this._PromisedDate;
    set => this._PromisedDate = value;
  }

  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "FOB Point")]
  [PXSelector(typeof (Search<PX.Objects.CS.FOBPoint.fOBPointID>), DescriptionField = typeof (PX.Objects.CS.FOBPoint.description), CacheGlobal = true)]
  [PXDefault(typeof (Search<PX.Objects.CR.Location.vFOBPointID, Where<PX.Objects.CR.Location.bAccountID, Equal<Current<RQBiddingVendor.vendorID>>>>))]
  public virtual string FOBPoint
  {
    get => this._FOBPoint;
    set => this._FOBPoint = value;
  }

  [PXUIField(DisplayName = "Ship Via")]
  [PXActiveCarrierSelector(typeof (Search<PX.Objects.CS.Carrier.carrierID>), CacheGlobal = true)]
  [PXDefault(typeof (Search<PX.Objects.CR.Location.vCarrierID, Where<PX.Objects.CR.Location.bAccountID, Equal<Current<RQBiddingVendor.vendorID>>, And<PX.Objects.CR.Location.locationID, Equal<Current<RQBiddingVendor.vendorLocationID>>>>>))]
  public virtual string ShipVia
  {
    get => this._ShipVia;
    set => this._ShipVia = value;
  }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? TotalQuoteQty
  {
    get => this._TotalQuoteQty;
    set => this._TotalQuoteQty = value;
  }

  [PXDBCurrency(typeof (RQBiddingVendor.curyInfoID), typeof (RQBiddingVendor.totalQuoteExtCost))]
  [PXUIField(DisplayName = "Total Extended Cost", Enabled = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryTotalQuoteExtCost
  {
    get => this._CuryTotalQuoteExtCost;
    set => this._CuryTotalQuoteExtCost = value;
  }

  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? TotalQuoteExtCost
  {
    get => this._TotalQuoteExtCost;
    set => this._TotalQuoteExtCost = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Request Sent")]
  public virtual bool? Status
  {
    get => this._Status;
    set => this._Status = value;
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

  public class PK : PrimaryKeyOf<RQBiddingVendor>.By<RQBiddingVendor.lineID>
  {
    public static RQBiddingVendor Find(PXGraph graph, int? lineNbr, PKFindOptions options = 0)
    {
      return (RQBiddingVendor) PrimaryKeyOf<RQBiddingVendor>.By<RQBiddingVendor.lineID>.FindBy(graph, (object) lineNbr, options);
    }
  }

  public static class FK
  {
    public class Requisition : 
      PrimaryKeyOf<RQRequisition>.By<RQRequisition.reqNbr>.ForeignKeyOf<RQBiddingVendor>.By<RQBiddingVendor.reqNbr>
    {
    }

    public class PORemitContact : 
      PrimaryKeyOf<PX.Objects.PO.PORemitContact>.By<PX.Objects.PO.PORemitContact.contactID>.ForeignKeyOf<RQBiddingVendor>.By<RQBiddingVendor.remitContactID>
    {
    }

    public class PORemitAddress : 
      PrimaryKeyOf<PX.Objects.PO.PORemitAddress>.By<PX.Objects.PO.PORemitAddress.addressID>.ForeignKeyOf<RQBiddingVendor>.By<RQBiddingVendor.remitAddressID>
    {
    }

    public class FOBPoint : 
      PrimaryKeyOf<PX.Objects.CS.FOBPoint>.By<PX.Objects.CS.FOBPoint.fOBPointID>.ForeignKeyOf<RQBiddingVendor>.By<RQBiddingVendor.fOBPoint>
    {
    }
  }

  public abstract class lineID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RQBiddingVendor.lineID>
  {
  }

  public abstract class reqNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RQBiddingVendor.reqNbr>
  {
  }

  public abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RQBiddingVendor.vendorID>
  {
  }

  public abstract class vendorLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    RQBiddingVendor.vendorLocationID>
  {
  }

  public abstract class remitAddressID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RQBiddingVendor.remitAddressID>
  {
  }

  public abstract class remitContactID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  RQBiddingVendor.remitContactID>
  {
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RQBiddingVendor.curyID>
  {
  }

  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  RQBiddingVendor.curyInfoID>
  {
  }

  public abstract class entryDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  RQBiddingVendor.entryDate>
  {
  }

  public abstract class expireDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    RQBiddingVendor.expireDate>
  {
  }

  public abstract class promisedDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    RQBiddingVendor.promisedDate>
  {
  }

  public abstract class fOBPoint : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RQBiddingVendor.fOBPoint>
  {
  }

  public abstract class shipVia : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RQBiddingVendor.shipVia>
  {
  }

  public abstract class totalQuoteQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    RQBiddingVendor.totalQuoteQty>
  {
  }

  public abstract class curyTotalQuoteExtCost : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    RQBiddingVendor.curyTotalQuoteExtCost>
  {
  }

  public abstract class totalQuoteExtCost : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    RQBiddingVendor.totalQuoteExtCost>
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  RQBiddingVendor.status>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  RQBiddingVendor.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  RQBiddingVendor.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RQBiddingVendor.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    RQBiddingVendor.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    RQBiddingVendor.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RQBiddingVendor.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    RQBiddingVendor.lastModifiedDateTime>
  {
  }
}
