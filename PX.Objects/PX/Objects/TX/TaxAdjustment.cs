// Decompiled with JetBrains decompiler
// Type: PX.Objects.TX.TaxAdjustment
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AP;
using PX.Objects.CM.Extensions;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.TX.Descriptor;
using System;

#nullable enable
namespace PX.Objects.TX;

[PXCacheName("Tax Adjustment")]
[Serializable]
public class TaxAdjustment : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected DateTime? _DocDate;
  protected 
  #nullable disable
  string _CuryID;
  protected int? _AdjAccountID;
  protected int? _AdjSubID;
  protected int? _LineCntr;
  protected Decimal? _CuryOrigDocAmt;
  protected Decimal? _OrigDocAmt;
  protected Decimal? _CuryDocBal;
  protected Decimal? _DocBal;
  protected string _DocDesc;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;
  protected byte[] _tstamp;
  protected string _BatchNbr;
  protected string _Status;
  protected bool? _Released;
  protected bool? _Hold;
  protected Guid? _NoteID;

  [PXDBString(3, IsKey = true, IsFixed = true)]
  [PXDefault("INT")]
  [TaxAdjustmentType.List]
  [PXUIField]
  public virtual string DocType { get; set; }

  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXDefault]
  [PXUIField]
  [PXSelector(typeof (Search<TaxAdjustment.refNbr, Where<TaxAdjustment.docType, Equal<Optional<TaxAdjustment.docType>>>>))]
  [AutoNumber(typeof (TXSetup.taxAdjustmentNumberingID), typeof (TaxAdjustment.docDate))]
  [PXFieldDescription]
  public virtual string RefNbr { get; set; }

  /// <summary>Reference number of the original (source) document.</summary>
  [PXDBString(15, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Orig. Ref. Nbr.")]
  public virtual string OrigRefNbr { get; set; }

  [PXDBDate]
  [PXDefault]
  [PXUIField]
  public virtual DateTime? DocDate
  {
    get => this._DocDate;
    set => this._DocDate = value;
  }

  [Branch(null, null, true, true, true)]
  public virtual int? BranchID { get; set; }

  [OpenPeriod(null, typeof (TaxAdjustment.docDate), typeof (TaxAdjustment.branchID), null, null, null, null, true, false, false, false, true, FinPeriodSelectorAttribute.SelectionModesWithRestrictions.Undefined, null, typeof (TaxAdjustment.tranPeriodID), true, IsHeader = true)]
  [PXDefault]
  [PXUIField]
  public virtual string FinPeriodID { get; set; }

  [PeriodID(null, null, null, true)]
  [PXUIField(DisplayName = "Master Period")]
  public virtual string TranPeriodID { get; set; }

  [TaxAgencyActive]
  [PXDefault]
  public virtual int? VendorID { get; set; }

  [LocationActive(typeof (Where<PX.Objects.CR.Location.bAccountID, Equal<Current<TaxAdjustment.vendorID>>, And<MatchWithBranch<PX.Objects.CR.Location.vBranchID>>>))]
  [PXDefault(typeof (Search2<BAccountR.defLocationID, InnerJoin<PX.Objects.CR.Location, On<PX.Objects.CR.Location.bAccountID, Equal<BAccountR.bAccountID>, And<PX.Objects.CR.Location.locationID, Equal<BAccountR.defLocationID>>>>, Where<BAccountR.bAccountID, Equal<Current<TaxAdjustment.vendorID>>, And<PX.Objects.CR.Location.isActive, Equal<True>, And<MatchWithBranch<PX.Objects.CR.Location.vBranchID>>>>>))]
  public virtual int? VendorLocationID { get; set; }

  [TaxAdjsutmentTaxPeriodSelector]
  public virtual string TaxPeriod { get; set; }

  [PXDBString(5, IsUnicode = true, InputMask = ">LLLLL")]
  [PXUIField]
  [PXDefault(typeof (Current<AccessInfo.baseCuryID>))]
  [PXSelector(typeof (PX.Objects.CM.Extensions.Currency.curyID))]
  public virtual string CuryID
  {
    get => this._CuryID;
    set => this._CuryID = value;
  }

  [Account]
  [PXDefault(typeof (Search<PX.Objects.CR.Location.vExpenseAcctID, Where<PX.Objects.CR.Location.bAccountID, Equal<Current<TaxAdjustment.vendorID>>, And<PX.Objects.CR.Location.locationID, Equal<Current<TaxAdjustment.vendorLocationID>>>>>))]
  public virtual int? AdjAccountID
  {
    get => this._AdjAccountID;
    set => this._AdjAccountID = value;
  }

  [SubAccount(typeof (TaxAdjustment.adjAccountID))]
  [PXDefault(typeof (Search<PX.Objects.CR.Location.vExpenseSubID, Where<PX.Objects.CR.Location.bAccountID, Equal<Current<TaxAdjustment.vendorID>>, And<PX.Objects.CR.Location.locationID, Equal<Current<TaxAdjustment.vendorLocationID>>>>>))]
  public virtual int? AdjSubID
  {
    get => this._AdjSubID;
    set => this._AdjSubID = value;
  }

  [PXDBInt]
  [PXDefault(0)]
  public virtual int? LineCntr
  {
    get => this._LineCntr;
    set => this._LineCntr = value;
  }

  [PXDBLong]
  [CurrencyInfo]
  public virtual long? CuryInfoID { get; set; }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBCurrency(typeof (TaxAdjustment.curyInfoID), typeof (TaxAdjustment.origDocAmt))]
  [PXUIField]
  public virtual Decimal? CuryOrigDocAmt
  {
    get => this._CuryOrigDocAmt;
    set => this._CuryOrigDocAmt = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? OrigDocAmt
  {
    get => this._OrigDocAmt;
    set => this._OrigDocAmt = value;
  }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBCurrency(typeof (TaxAdjustment.curyInfoID), typeof (TaxAdjustment.docBal), BaseCalc = false)]
  [PXUIField]
  public virtual Decimal? CuryDocBal
  {
    get => this._CuryDocBal;
    set => this._CuryDocBal = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? DocBal
  {
    get => this._DocBal;
    set => this._DocBal = value;
  }

  [PXDBString(60, IsUnicode = true)]
  [PXUIField]
  public virtual string DocDesc
  {
    get => this._DocDesc;
    set => this._DocDesc = value;
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
  [PXUIField(DisplayName = "Created On", Enabled = false, IsReadOnly = true)]
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
  [PXUIField(DisplayName = "Last Modified On", Enabled = false, IsReadOnly = true)]
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

  [PXDBString(15, IsUnicode = true)]
  [PXUIField]
  [PXSelector(typeof (Search<Batch.batchNbr, Where<Batch.module, Equal<BatchModule.moduleGL>>>))]
  public virtual string BatchNbr
  {
    get => this._BatchNbr;
    set => this._BatchNbr = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXDefault("H")]
  [PXUIField]
  [TaxAdjustmentStatus.List]
  public virtual string Status
  {
    [PXDependsOnFields(new System.Type[] {typeof (TaxAdjustment.released), typeof (TaxAdjustment.hold)})] get
    {
      return this._Status;
    }
    set
    {
    }
  }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? Released
  {
    get => this._Released;
    set
    {
      this._Released = value;
      this.SetStatus();
    }
  }

  [PXDBBool]
  [PXUIField]
  [PXDefault(true, typeof (Search<APSetup.holdEntry>))]
  public virtual bool? Hold
  {
    get => this._Hold;
    set
    {
      this._Hold = value;
      this.SetStatus();
    }
  }

  [PXNote(DescriptionField = typeof (TaxAdjustment.refNbr))]
  public virtual Guid? NoteID
  {
    get => this._NoteID;
    set => this._NoteID = value;
  }

  protected virtual void SetStatus()
  {
    if (this._Hold.HasValue && this._Hold.Value)
      this._Status = "H";
    else if (this._Released.HasValue && !this._Released.Value)
      this._Status = "B";
    else
      this._Status = "C";
  }

  public class PK : PrimaryKeyOf<TaxAdjustment>.By<TaxAdjustment.docType, TaxAdjustment.refNbr>
  {
    public static TaxAdjustment Find(
      PXGraph graph,
      string docType,
      string refNbr,
      PKFindOptions options = 0)
    {
      return (TaxAdjustment) PrimaryKeyOf<TaxAdjustment>.By<TaxAdjustment.docType, TaxAdjustment.refNbr>.FindBy(graph, (object) docType, (object) refNbr, options);
    }
  }

  public static class FK
  {
    public class Branch : 
      PrimaryKeyOf<PX.Objects.GL.Branch>.By<PX.Objects.GL.Branch.branchID>.ForeignKeyOf<TaxAdjustment>.By<TaxAdjustment.branchID>
    {
    }

    public class Vendor : 
      PrimaryKeyOf<PX.Objects.AP.Vendor>.By<PX.Objects.AP.Vendor.bAccountID>.ForeignKeyOf<TaxAdjustment>.By<TaxAdjustment.vendorID>
    {
    }

    public class Location : 
      PrimaryKeyOf<PX.Objects.CR.Location>.By<PX.Objects.CR.Location.bAccountID, PX.Objects.CR.Location.locationID>.ForeignKeyOf<TaxAdjustment>.By<TaxAdjustment.vendorID, TaxAdjustment.vendorLocationID>
    {
    }

    public class AdjustmentAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<TaxAdjustment>.By<TaxAdjustment.adjAccountID>
    {
    }

    public class AdjustmentSubaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<TaxAdjustment>.By<TaxAdjustment.adjSubID>
    {
    }

    public class CurrencyInfo : 
      PrimaryKeyOf<PX.Objects.CM.CurrencyInfo>.By<PX.Objects.CM.CurrencyInfo.curyInfoID>.ForeignKeyOf<TaxAdjustment>.By<TaxAdjustment.curyInfoID>
    {
    }

    public class Currency : 
      PrimaryKeyOf<PX.Objects.CM.Currency>.By<PX.Objects.CM.Currency.curyID>.ForeignKeyOf<TaxAdjustment>.By<TaxAdjustment.curyID>
    {
    }
  }

  public abstract class docType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TaxAdjustment.docType>
  {
  }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TaxAdjustment.refNbr>
  {
  }

  public abstract class origRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TaxAdjustment.origRefNbr>
  {
  }

  public abstract class docDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  TaxAdjustment.docDate>
  {
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  TaxAdjustment.branchID>
  {
  }

  public abstract class finPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TaxAdjustment.finPeriodID>
  {
  }

  public abstract class tranPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TaxAdjustment.tranPeriodID>
  {
  }

  public abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  TaxAdjustment.vendorID>
  {
  }

  public abstract class vendorLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    TaxAdjustment.vendorLocationID>
  {
  }

  public abstract class taxPeriod : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TaxAdjustment.taxPeriod>
  {
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TaxAdjustment.curyID>
  {
  }

  public abstract class adjAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  TaxAdjustment.adjAccountID>
  {
  }

  public abstract class adjSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  TaxAdjustment.adjSubID>
  {
  }

  public abstract class lineCntr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  TaxAdjustment.lineCntr>
  {
  }

  public abstract class curyInfoID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  TaxAdjustment.curyInfoID>
  {
  }

  public abstract class curyOrigDocAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    TaxAdjustment.curyOrigDocAmt>
  {
  }

  public abstract class origDocAmt : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  TaxAdjustment.origDocAmt>
  {
  }

  public abstract class curyDocBal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  TaxAdjustment.curyDocBal>
  {
  }

  public abstract class docBal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  TaxAdjustment.docBal>
  {
  }

  public abstract class docDesc : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TaxAdjustment.docDesc>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  TaxAdjustment.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    TaxAdjustment.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    TaxAdjustment.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    TaxAdjustment.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    TaxAdjustment.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    TaxAdjustment.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  TaxAdjustment.Tstamp>
  {
  }

  public abstract class batchNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TaxAdjustment.batchNbr>
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TaxAdjustment.status>
  {
  }

  public abstract class released : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  TaxAdjustment.released>
  {
  }

  public abstract class hold : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  TaxAdjustment.hold>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  TaxAdjustment.noteID>
  {
  }
}
