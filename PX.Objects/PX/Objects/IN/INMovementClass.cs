// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INMovementClass
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.Objects.IN;

[PXCacheName]
[PXPrimaryGraph(typeof (INMovementClassMaint))]
[Serializable]
public class INMovementClass : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _MovementClassID;
  protected string _Descr;
  protected short? _CountsPerYear;
  protected Decimal? _MaxTurnoverPct;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;
  protected byte[] _tstamp;

  [PXDefault]
  [PXDBString(1, IsKey = true)]
  [PXUIField]
  public virtual string MovementClassID
  {
    get => this._MovementClassID;
    set => this._MovementClassID = value;
  }

  [PXDBString(256 /*0x0100*/, IsUnicode = true)]
  [PXUIField]
  public virtual string Descr
  {
    get => this._Descr;
    set => this._Descr = value;
  }

  [PXDBShort(MinValue = 0)]
  [PXUIField]
  public virtual short? CountsPerYear
  {
    get => this._CountsPerYear;
    set => this._CountsPerYear = value;
  }

  [PXDBDecimal(2, MinValue = 0.0, MaxValue = 1000000000.0)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Max. Turnover %")]
  public virtual Decimal? MaxTurnoverPct
  {
    get => this._MaxTurnoverPct;
    set => this._MaxTurnoverPct = value;
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

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  public class PK : PrimaryKeyOf<INMovementClass>.By<INMovementClass.movementClassID>
  {
    public static INMovementClass Find(
      PXGraph graph,
      string movementClassID,
      PKFindOptions options = 0)
    {
      return (INMovementClass) PrimaryKeyOf<INMovementClass>.By<INMovementClass.movementClassID>.FindBy(graph, (object) movementClassID, options);
    }
  }

  public abstract class movementClassID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INMovementClass.movementClassID>
  {
  }

  public abstract class descr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INMovementClass.descr>
  {
  }

  public abstract class countsPerYear : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    INMovementClass.countsPerYear>
  {
  }

  public abstract class maxTurnoverPct : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INMovementClass.maxTurnoverPct>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  INMovementClass.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INMovementClass.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INMovementClass.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    INMovementClass.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INMovementClass.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INMovementClass.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  INMovementClass.Tstamp>
  {
  }
}
