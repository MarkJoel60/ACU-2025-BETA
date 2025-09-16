// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.DAC.Projections.BlanketSOOrderSite
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CS;
using System;

#nullable enable
namespace PX.Objects.SO.DAC.Projections;

/// <exclude />
[PXCacheName("Blanket SO Order Site")]
[PXProjection(typeof (Select<SOOrderSite>), Persistent = true)]
public class BlanketSOOrderSite : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _OrderType;
  protected string _OrderNbr;
  protected int? _SiteID;
  protected int? _OpenLineCntr;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;
  protected byte[] _tstamp;

  [PXDBString(2, IsKey = true, IsFixed = true, BqlField = typeof (SOOrderSite.orderType))]
  public virtual string OrderType
  {
    get => this._OrderType;
    set => this._OrderType = value;
  }

  [PXDBString(15, IsKey = true, InputMask = "", IsUnicode = true, BqlField = typeof (SOOrderSite.orderNbr))]
  [PXParent(typeof (BlanketSOOrderSite.FK.Order))]
  public virtual string OrderNbr
  {
    get => this._OrderNbr;
    set => this._OrderNbr = value;
  }

  [PXDBInt(IsKey = true, BqlField = typeof (SOOrderSite.siteID))]
  public virtual int? SiteID
  {
    get => this._SiteID;
    set => this._SiteID = value;
  }

  [PXDBInt(BqlField = typeof (SOOrderSite.openLineCntr))]
  [PXUnboundFormula(typeof (IIf<BqlOperand<BlanketSOOrderSite.openLineCntr, IBqlInt>.IsGreater<int0>, int1, int0>), typeof (SumCalc<BlanketSOOrder.openSiteCntr>), ValidateAggregateCalculation = true)]
  public virtual int? OpenLineCntr
  {
    get => this._OpenLineCntr;
    set => this._OpenLineCntr = value;
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

  [PXDBTimestamp(BqlField = typeof (SOOrderSite.Tstamp), RecordComesFirst = true)]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  public class PK : 
    PrimaryKeyOf<BlanketSOOrderSite>.By<BlanketSOOrderSite.orderType, BlanketSOOrderSite.orderNbr, BlanketSOOrderSite.siteID>
  {
    public static BlanketSOOrderSite Find(
      PXGraph graph,
      string orderType,
      string orderNbr,
      int? siteID)
    {
      return (BlanketSOOrderSite) PrimaryKeyOf<BlanketSOOrderSite>.By<BlanketSOOrderSite.orderType, BlanketSOOrderSite.orderNbr, BlanketSOOrderSite.siteID>.FindBy(graph, (object) orderType, (object) orderNbr, (object) siteID, (PKFindOptions) 0);
    }
  }

  public static class FK
  {
    public class OrderType : 
      PrimaryKeyOf<PX.Objects.SO.SOOrderType>.By<PX.Objects.SO.SOOrderType.orderType>.ForeignKeyOf<BlanketSOOrderSite>.By<BlanketSOOrderSite.orderType>
    {
    }

    public class Order : 
      PrimaryKeyOf<BlanketSOOrder>.By<BlanketSOOrder.orderType, BlanketSOOrder.orderNbr>.ForeignKeyOf<BlanketSOOrderSite>.By<BlanketSOOrderSite.orderType, BlanketSOOrderSite.orderNbr>
    {
    }

    public class Site : 
      PrimaryKeyOf<PX.Objects.IN.INSite>.By<PX.Objects.IN.INSite.siteID>.ForeignKeyOf<BlanketSOOrderSite>.By<BlanketSOOrderSite.siteID>
    {
    }
  }

  public abstract class orderType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BlanketSOOrderSite.orderType>
  {
  }

  public abstract class orderNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BlanketSOOrderSite.orderNbr>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  BlanketSOOrderSite.siteID>
  {
  }

  public abstract class openLineCntr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  BlanketSOOrderSite.openLineCntr>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    BlanketSOOrderSite.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    BlanketSOOrderSite.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    BlanketSOOrderSite.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  BlanketSOOrderSite.Tstamp>
  {
  }
}
