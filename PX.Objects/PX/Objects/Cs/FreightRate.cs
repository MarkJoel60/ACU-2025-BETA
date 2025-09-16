// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.FreightRate
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.CS;

[PXCacheName("Freight Rate")]
[Serializable]
public class FreightRate : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _CarrierID;
  protected int? _LineNbr;
  protected Decimal? _Weight;
  protected Decimal? _Volume;
  protected string _ZoneID;
  protected Decimal? _Rate;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = ">aaaaaaaaaaaaaaa")]
  [PXDefault(typeof (Carrier.carrierID))]
  public virtual string CarrierID
  {
    get => this._CarrierID;
    set => this._CarrierID = value;
  }

  [PXDBInt(IsKey = true)]
  [PXDefault]
  [PXLineNbr(typeof (Carrier))]
  [PXParent(typeof (Select<Carrier, Where<Carrier.carrierID, Equal<Current<FreightRate.carrierID>>>>), LeaveChildren = true)]
  public virtual int? LineNbr
  {
    get => this._LineNbr;
    set => this._LineNbr = value;
  }

  [PXDBDecimal(2)]
  [PXDefault]
  [PXUIField(DisplayName = "Weight")]
  public virtual Decimal? Weight
  {
    get => this._Weight;
    set => this._Weight = value;
  }

  [PXDBDecimal(2)]
  [PXDefault]
  [PXUIField(DisplayName = "Volume")]
  public virtual Decimal? Volume
  {
    get => this._Volume;
    set => this._Volume = value;
  }

  [PXDBString(15, IsUnicode = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Zone ID")]
  [PXSelector(typeof (ShippingZone.zoneID))]
  public virtual string ZoneID
  {
    get => this._ZoneID;
    set => this._ZoneID = value;
  }

  [PXDBDecimal(typeof (Search<CommonSetup.decPlPrcCst>))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Rate")]
  public virtual Decimal? Rate
  {
    get => this._Rate;
    set => this._Rate = value;
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

  public abstract class carrierID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FreightRate.carrierID>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FreightRate.lineNbr>
  {
  }

  public abstract class weight : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FreightRate.weight>
  {
  }

  public abstract class volume : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FreightRate.volume>
  {
  }

  public abstract class zoneID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FreightRate.zoneID>
  {
  }

  public abstract class rate : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FreightRate.rate>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  FreightRate.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FreightRate.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FreightRate.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FreightRate.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    FreightRate.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FreightRate.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FreightRate.lastModifiedDateTime>
  {
  }
}
