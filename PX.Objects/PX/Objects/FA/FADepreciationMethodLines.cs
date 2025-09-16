// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.FADepreciationMethodLines
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
namespace PX.Objects.FA;

[PXCacheName("FA Depreciation Method Lines")]
[Serializable]
public class FADepreciationMethodLines : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _MethodID;
  protected int? _Year;
  protected Decimal? _RatioPerYear;
  protected 
  #nullable disable
  byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;
  protected Guid? _NoteID;

  [PXDBInt(IsKey = true)]
  [PXDBDefault(typeof (FADepreciationMethod.methodID))]
  [PXParent(typeof (Select<FADepreciationMethod, Where<FADepreciationMethod.methodID, Equal<Current<FADepreciationMethodLines.methodID>>>>), UseCurrent = true, LeaveChildren = false)]
  [PXUIField]
  public virtual int? MethodID
  {
    get => this._MethodID;
    set => this._MethodID = value;
  }

  [PXDBInt(IsKey = true, MaxValue = 500, MinValue = 0)]
  [PXUIField(DisplayName = "Recovery Year", Enabled = false)]
  public virtual int? Year
  {
    get => this._Year;
    set => this._Year = value;
  }

  [PXDecimal(3, MinValue = 0.0, MaxValue = 100.0)]
  [PXFormula(typeof (Mult<Current<FADepreciationMethodLines.ratioPerYear>, decimal100>))]
  [PXUIField(DisplayName = "Percent per Year")]
  public virtual Decimal? DisplayRatioPerYear { get; set; }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBDecimal(5)]
  [PXFormula(typeof (Div<FADepreciationMethodLines.displayRatioPerYear, decimal100>), typeof (SumCalc<FADepreciationMethod.totalPercents>))]
  public virtual Decimal? RatioPerYear
  {
    get => this._RatioPerYear;
    set => this._RatioPerYear = value;
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

  [PXNote]
  public virtual Guid? NoteID
  {
    get => this._NoteID;
    set => this._NoteID = value;
  }

  public class PK : 
    PrimaryKeyOf<FADepreciationMethodLines>.By<FADepreciationMethodLines.methodID, FADepreciationMethodLines.year>
  {
    public static FADepreciationMethodLines Find(
      PXGraph graph,
      int? methodID,
      int? year,
      PKFindOptions options = 0)
    {
      return (FADepreciationMethodLines) PrimaryKeyOf<FADepreciationMethodLines>.By<FADepreciationMethodLines.methodID, FADepreciationMethodLines.year>.FindBy(graph, (object) methodID, (object) year, options);
    }
  }

  public static class FK
  {
    public class DepreciationMethod : 
      PrimaryKeyOf<FADepreciationMethod>.By<FADepreciationMethod.methodID>.ForeignKeyOf<FADepreciationMethodLines>.By<FADepreciationMethodLines.methodID>
    {
    }
  }

  public abstract class methodID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FADepreciationMethodLines.methodID>
  {
  }

  public abstract class year : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FADepreciationMethodLines.year>
  {
  }

  public abstract class displayRatioPerYear : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FADepreciationMethodLines.displayRatioPerYear>
  {
  }

  public abstract class ratioPerYear : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    FADepreciationMethodLines.ratioPerYear>
  {
  }

  public abstract class Tstamp : 
    BqlType<
    #nullable enable
    IBqlByteArray, byte[]>.Field<
    #nullable disable
    FADepreciationMethodLines.Tstamp>
  {
  }

  public abstract class createdByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    FADepreciationMethodLines.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FADepreciationMethodLines.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FADepreciationMethodLines.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    FADepreciationMethodLines.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FADepreciationMethodLines.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FADepreciationMethodLines.lastModifiedDateTime>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FADepreciationMethodLines.noteID>
  {
  }
}
