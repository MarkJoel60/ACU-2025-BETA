// Decompiled with JetBrains decompiler
// Type: PX.Objects.DR.DRDocumentRecord
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CM;
using System;

#nullable enable
namespace PX.Objects.DR;

[PXHidden]
[Serializable]
public class DRDocumentRecord : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _DocType;
  protected string _RefNbr;
  protected DateTime? _DocDate;
  protected string _FinPeriodID;
  protected int? _LocationID;
  protected string _CuryID;
  protected string _Status;
  protected Decimal? _CuryOrigDocAmt;

  [PXDBString(3, IsKey = true, IsFixed = true)]
  [PXUIField]
  public virtual string DocType
  {
    get => this._DocType;
    set => this._DocType = value;
  }

  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = "")]
  [PXUIField]
  public virtual string RefNbr
  {
    get => this._RefNbr;
    set => this._RefNbr = value;
  }

  [PXDBDate]
  [PXDefault(typeof (AccessInfo.businessDate))]
  [PXUIField]
  public virtual DateTime? DocDate
  {
    get => this._DocDate;
    set => this._DocDate = value;
  }

  [PX.Objects.GL.FinPeriodID(null, null, null, null, null, null, true, false, null, null, null, true, true)]
  [PXUIField]
  public virtual string FinPeriodID
  {
    get => this._FinPeriodID;
    set => this._FinPeriodID = value;
  }

  [PXDBString]
  [PXUIField]
  public virtual string BAccountCD { get; set; }

  [PX.Objects.CS.LocationID(typeof (Where<PX.Objects.CR.Location.bAccountID, Equal<Current<DRDocumentRecord.bAccountCD>>>))]
  public virtual int? LocationID
  {
    get => this._LocationID;
    set => this._LocationID = value;
  }

  [PXDBString(5, IsUnicode = true, InputMask = ">LLLLL")]
  [PXUIField]
  public virtual string CuryID
  {
    get => this._CuryID;
    set => this._CuryID = value;
  }

  [PXDBString]
  [PXUIField]
  public virtual string Status
  {
    get => this._Status;
    set => this._Status = value;
  }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBCury(typeof (DRDocumentRecord.curyID))]
  [PXUIField]
  public virtual Decimal? CuryOrigDocAmt
  {
    get => this._CuryOrigDocAmt;
    set => this._CuryOrigDocAmt = value;
  }

  public abstract class docType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  DRDocumentRecord.docType>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  DRDocumentRecord.refNbr>
  {
  }

  public abstract class docDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  DRDocumentRecord.docDate>
  {
  }

  public abstract class finPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  DRDocumentRecord.finPeriodID>
  {
  }

  public abstract class bAccountCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  DRDocumentRecord.bAccountCD>
  {
  }

  public abstract class locationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  DRDocumentRecord.locationID>
  {
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  DRDocumentRecord.curyID>
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  DRDocumentRecord.status>
  {
  }

  public abstract class curyOrigDocAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    DRDocumentRecord.curyOrigDocAmt>
  {
  }
}
