// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INABCCode
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
[PXPrimaryGraph(typeof (INABCCodeMaint))]
[Serializable]
public class INABCCode : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _ABCCodeID;
  protected string _Descr;
  protected short? _CountsPerYear;
  protected Decimal? _MaxCountInaccuracyPct;
  protected Decimal? _ABCPct;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;
  protected byte[] _tstamp;

  [PXDefault]
  [PXDBString(1, IsKey = true, IsFixed = true)]
  [PXUIField]
  public virtual string ABCCodeID
  {
    get => this._ABCCodeID;
    set => this._ABCCodeID = value;
  }

  [PXDBString(60, IsUnicode = true)]
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

  [PXDBDecimal(2, MinValue = 0.0, MaxValue = 100.0)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Max. Count Inaccuracy %")]
  public virtual Decimal? MaxCountInaccuracyPct
  {
    get => this._MaxCountInaccuracyPct;
    set => this._MaxCountInaccuracyPct = value;
  }

  [PXDBDecimal(2, MinValue = 0.0, MaxValue = 100.0)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "ABC Code %")]
  public virtual Decimal? ABCPct
  {
    get => this._ABCPct;
    set => this._ABCPct = value;
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

  public class PK : PrimaryKeyOf<INABCCode>.By<INABCCode.aBCCodeID>
  {
    public static INABCCode Find(PXGraph graph, string aBCCodeID, PKFindOptions options = 0)
    {
      return (INABCCode) PrimaryKeyOf<INABCCode>.By<INABCCode.aBCCodeID>.FindBy(graph, (object) aBCCodeID, options);
    }
  }

  public abstract class aBCCodeID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INABCCode.aBCCodeID>
  {
  }

  public abstract class descr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INABCCode.descr>
  {
  }

  public abstract class countsPerYear : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  INABCCode.countsPerYear>
  {
  }

  public abstract class maxCountInaccuracyPct : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INABCCode.maxCountInaccuracyPct>
  {
  }

  public abstract class aBCPct : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INABCCode.aBCPct>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  INABCCode.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INABCCode.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INABCCode.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  INABCCode.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INABCCode.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INABCCode.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  INABCCode.Tstamp>
  {
  }
}
