// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.ShipTermsDetail
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CM;
using System;

#nullable enable
namespace PX.Objects.CS;

[PXCacheName]
[Serializable]
public class ShipTermsDetail : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _ShipTermsID;
  protected int? _LineNbr;
  protected Decimal? _BreakAmount;
  protected Decimal? _FreightCostPercent;
  protected Decimal? _InvoiceAmountPercent;
  protected Decimal? _ShippingHandling;
  protected Decimal? _LineHandling;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  [PXDBString(10, IsUnicode = true, IsKey = true, InputMask = ">aaaaaaaaaaaaaaa")]
  [PXDefault(typeof (ShipTerms.shipTermsID))]
  public virtual string ShipTermsID
  {
    get => this._ShipTermsID;
    set => this._ShipTermsID = value;
  }

  [PXDBInt(IsKey = true)]
  [PXDefault]
  [PXLineNbr(typeof (ShipTerms))]
  [PXParent(typeof (ShipTermsDetail.FK.ShipTerms), LeaveChildren = true)]
  public virtual int? LineNbr
  {
    get => this._LineNbr;
    set => this._LineNbr = value;
  }

  [PXDBDecimal(2)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Break Amount")]
  public virtual Decimal? BreakAmount
  {
    get => this._BreakAmount;
    set => this._BreakAmount = value;
  }

  [PXDBDecimal(2)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Freight Cost %")]
  public virtual Decimal? FreightCostPercent
  {
    get => this._FreightCostPercent;
    set => this._FreightCostPercent = value;
  }

  [PXDBDecimal(2)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Invoice Amount %")]
  public virtual Decimal? InvoiceAmountPercent
  {
    get => this._InvoiceAmountPercent;
    set => this._InvoiceAmountPercent = value;
  }

  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Shipping and Handling")]
  public virtual Decimal? ShippingHandling
  {
    get => this._ShippingHandling;
    set => this._ShippingHandling = value;
  }

  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Line Handling")]
  public virtual Decimal? LineHandling
  {
    get => this._LineHandling;
    set => this._LineHandling = value;
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

  public class PK : 
    PrimaryKeyOf<ShipTermsDetail>.By<ShipTermsDetail.shipTermsID, ShipTermsDetail.lineNbr>
  {
    public static ShipTermsDetail Find(
      PXGraph graph,
      string shipTermsID,
      int? lineNbr,
      PKFindOptions options = 0)
    {
      return (ShipTermsDetail) PrimaryKeyOf<ShipTermsDetail>.By<ShipTermsDetail.shipTermsID, ShipTermsDetail.lineNbr>.FindBy(graph, (object) shipTermsID, (object) lineNbr, options);
    }
  }

  public static class FK
  {
    public class ShipTerms : 
      PrimaryKeyOf<ShipTerms>.By<ShipTerms.shipTermsID>.ForeignKeyOf<ShipTermsDetail>.By<ShipTermsDetail.shipTermsID>
    {
    }
  }

  public abstract class shipTermsID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ShipTermsDetail.shipTermsID>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ShipTermsDetail.lineNbr>
  {
  }

  public abstract class breakAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ShipTermsDetail.breakAmount>
  {
  }

  public abstract class freightCostPercent : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ShipTermsDetail.freightCostPercent>
  {
  }

  public abstract class invoiceAmountPercent : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ShipTermsDetail.invoiceAmountPercent>
  {
  }

  public abstract class shippingHandling : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ShipTermsDetail.shippingHandling>
  {
  }

  public abstract class lineHandling : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ShipTermsDetail.lineHandling>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  ShipTermsDetail.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  ShipTermsDetail.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ShipTermsDetail.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ShipTermsDetail.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    ShipTermsDetail.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ShipTermsDetail.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ShipTermsDetail.lastModifiedDateTime>
  {
  }
}
