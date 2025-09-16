// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOBlanketOrderLink
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CM;
using PX.Objects.IN;
using System;

#nullable enable
namespace PX.Objects.SO;

[PXCacheName("Blanket Order Link")]
public class SOBlanketOrderLink : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected Guid? _CreatedByID;
  protected 
  #nullable disable
  string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;
  protected byte[] _tstamp;

  [PXDefault]
  [PXDBString(2, IsFixed = true, IsKey = true)]
  public virtual string BlanketType { get; set; }

  [PXDBString(15, IsUnicode = true, IsKey = true)]
  [PXDefault]
  [PXParent(typeof (SOBlanketOrderLink.FK.BlanketOrder))]
  [PXUIField(DisplayName = "Blanket Order Nbr.", Enabled = false)]
  [PXSelector(typeof (Search<SOOrder.orderNbr, Where<SOOrder.orderType, Equal<Current<SOBlanketOrderLink.blanketType>>>>), ValidateValue = false)]
  public virtual string BlanketNbr { get; set; }

  [PXDBString(2, IsKey = true, IsFixed = true)]
  [PXDefault(typeof (SOOrder.orderType))]
  [PXUIField(DisplayName = "Order Type", Visible = true, Enabled = false)]
  [PXSelector(typeof (Search<SOOrderType.orderType>), CacheGlobal = true)]
  public virtual string OrderType { get; set; }

  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = "")]
  [PXSelector(typeof (Search<SOOrder.orderNbr, Where<SOOrder.orderType, Equal<Current<SOBlanketOrderLink.orderType>>>>))]
  [PXDBDefault(typeof (SOOrder.orderNbr))]
  [PXParent(typeof (SOBlanketOrderLink.FK.ChildOrder))]
  [PXUIField(DisplayName = "Order Nbr.", Visible = true, Enabled = false)]
  public virtual string OrderNbr { get; set; }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Ordered Qty.", Enabled = false)]
  public virtual Decimal? OrderedQty { get; set; }

  [PXDBLong]
  [CurrencyInfo(typeof (SOOrder.curyInfoID))]
  public virtual long? CuryInfoID { get; set; }

  [PXDBCurrency(typeof (SOBlanketOrderLink.curyInfoID), typeof (SOBlanketOrderLink.orderedAmt))]
  [PXUIField(DisplayName = "Ordered Amount", Enabled = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CuryOrderedAmt { get; set; }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? OrderedAmt { get; set; }

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

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  public class PK : 
    PrimaryKeyOf<SOBlanketOrderLink>.By<SOBlanketOrderLink.blanketType, SOBlanketOrderLink.blanketNbr, SOBlanketOrderLink.orderType, SOBlanketOrderLink.orderNbr>
  {
    public static SOBlanketOrderLink Find(
      PXGraph graph,
      string blanketType,
      string blanketNbr,
      string orderType,
      string orderNbr,
      PKFindOptions options = 0)
    {
      return (SOBlanketOrderLink) PrimaryKeyOf<SOBlanketOrderLink>.By<SOBlanketOrderLink.blanketType, SOBlanketOrderLink.blanketNbr, SOBlanketOrderLink.orderType, SOBlanketOrderLink.orderNbr>.FindBy(graph, (object) blanketType, (object) blanketNbr, (object) orderType, (object) orderNbr, options);
    }
  }

  public static class FK
  {
    public class BlanketOrder : 
      PrimaryKeyOf<SOOrder>.By<SOOrder.orderType, SOOrder.orderNbr>.ForeignKeyOf<SOBlanketOrderLink>.By<SOBlanketOrderLink.blanketType, SOBlanketOrderLink.blanketNbr>
    {
    }

    public class ChildOrder : 
      PrimaryKeyOf<SOOrder>.By<SOOrder.orderType, SOOrder.orderNbr>.ForeignKeyOf<SOBlanketOrderLink>.By<SOBlanketOrderLink.orderType, SOBlanketOrderLink.orderNbr>
    {
    }
  }

  public abstract class blanketType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOBlanketOrderLink.blanketType>
  {
  }

  public abstract class blanketNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOBlanketOrderLink.blanketNbr>
  {
  }

  public abstract class orderType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOBlanketOrderLink.orderType>
  {
  }

  public abstract class orderNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SOBlanketOrderLink.orderNbr>
  {
  }

  public abstract class orderedQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOBlanketOrderLink.orderedQty>
  {
  }

  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  SOBlanketOrderLink.curyInfoID>
  {
  }

  public abstract class curyOrderedAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOBlanketOrderLink.curyOrderedAmt>
  {
  }

  public abstract class orderedAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    SOBlanketOrderLink.orderedAmt>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SOBlanketOrderLink.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOBlanketOrderLink.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SOBlanketOrderLink.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    SOBlanketOrderLink.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOBlanketOrderLink.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SOBlanketOrderLink.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  SOBlanketOrderLink.Tstamp>
  {
  }
}
