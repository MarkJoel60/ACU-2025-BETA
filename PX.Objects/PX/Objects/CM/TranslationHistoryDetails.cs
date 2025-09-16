// Decompiled with JetBrains decompiler
// Type: PX.Objects.CM.TranslationHistoryDetails
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.GL;
using PX.Objects.GL.DAC.Abstract;
using System;

#nullable enable
namespace PX.Objects.CM;

/// <summary>
/// Represents the details of the currency translation history. The records of this type are created together with the header (<see cref="T:PX.Objects.CM.TranslationHistory" />)
/// by the currency translation process (<see cref="T:PX.Objects.CM.TranslationProcess" />).
/// The detail records store the results of currency translation for a particular account and subaccount, including the currency and rate information,
/// link to the resulting <see cref="T:PX.Objects.GL.GLTran">GL transaction</see>, status, and amounts.
/// </summary>
[PXCacheName("Translation History Detail")]
[Serializable]
public class TranslationHistoryDetails : 
  PXBqlTable,
  IBqlTable,
  IBqlTableSystemDataStorage,
  IAccountable
{
  protected 
  #nullable disable
  string _ReferenceNbr;
  protected string _TranslDefId;
  protected int? _LedgerID;
  protected int? _BranchID;
  protected int? _AccountID;
  protected int? _SubID;
  protected short? _CalcMode;
  protected Decimal? _SourceAmt;
  protected Decimal? _TranslatedAmt;
  protected Decimal? _OrigTranslatedAmt;
  protected string _FinPeriodID;
  protected string _CuryID;
  protected string _RateTypeID;
  protected string _CuryMultDiv;
  protected DateTime? _CuryEffDate;
  protected Decimal? _CuryRate;
  protected string _LineType;
  protected string _BatchNbr;
  protected int? _LineNbr;
  protected Decimal? _DebitAmt;
  protected Decimal? _CreditAmt;
  protected Guid? _NoteID;
  protected bool? _Released;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  [PXDBString(15, IsUnicode = true, IsKey = true)]
  [PXDBDefault(typeof (TranslationHistory))]
  [PXParent(typeof (Select<TranslationHistory, Where<TranslationHistory.referenceNbr, Equal<Current<TranslationHistoryDetails.referenceNbr>>>>))]
  [PXUIField]
  public virtual string ReferenceNbr
  {
    get => this._ReferenceNbr;
    set => this._ReferenceNbr = value;
  }

  [PXDBString(10, IsUnicode = true)]
  [PXDBDefault(typeof (TranslationHistory.translDefId))]
  [PXSelector(typeof (TranslDef.translDefId))]
  public virtual string TranslDefId
  {
    get => this._TranslDefId;
    set => this._TranslDefId = value;
  }

  [PXDBInt]
  [PXDBDefault(typeof (TranslationHistory.ledgerID))]
  [PXSelector(typeof (PX.Objects.GL.Ledger.ledgerID), SubstituteKey = typeof (PX.Objects.GL.Ledger.ledgerCD), CacheGlobal = true)]
  public virtual int? LedgerID
  {
    get => this._LedgerID;
    set => this._LedgerID = value;
  }

  [Branch(null, null, true, true, true, IsKey = true, IsDetail = true)]
  public virtual int? BranchID
  {
    get => this._BranchID;
    set => this._BranchID = value;
  }

  [Account(IsKey = true, DescriptionField = typeof (PX.Objects.GL.Account.description))]
  [PXDefault]
  public virtual int? AccountID
  {
    get => this._AccountID;
    set => this._AccountID = value;
  }

  [SubAccount(IsKey = true)]
  [PXDefault]
  public virtual int? SubID
  {
    get => this._SubID;
    set => this._SubID = value;
  }

  [PXUIField(DisplayName = "Translation Method")]
  [PXDBShort]
  [PXDefault(1)]
  [PXIntList("0;Gain or Loss,1;YTD Balance,2;PTD Balance")]
  public virtual short? CalcMode
  {
    get => this._CalcMode;
    set => this._CalcMode = value;
  }

  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Source Amount", Required = false)]
  public virtual Decimal? SourceAmt
  {
    get => this._SourceAmt;
    set => this._SourceAmt = value;
  }

  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Translated Amount", Required = false)]
  public virtual Decimal? TranslatedAmt
  {
    get => this._TranslatedAmt;
    set => this._TranslatedAmt = value;
  }

  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Orig. Translated Amount", Required = false)]
  public virtual Decimal? OrigTranslatedAmt
  {
    get => this._OrigTranslatedAmt;
    set => this._OrigTranslatedAmt = value;
  }

  [PX.Objects.GL.FinPeriodID(null, null, null, null, null, null, true, false, null, null, null, true, true)]
  [PXDBDefault(typeof (TranslationHistory.finPeriodID))]
  public virtual string FinPeriodID
  {
    get => this._FinPeriodID;
    set => this._FinPeriodID = value;
  }

  [PXDBString(5, IsUnicode = true)]
  [PXUIField]
  [PXDefault]
  [PXSelector(typeof (Currency.curyID))]
  public virtual string CuryID
  {
    get => this._CuryID;
    set => this._CuryID = value;
  }

  [PXDBString(6, IsUnicode = true)]
  [PXSelector(typeof (CurrencyRateType.curyRateTypeID))]
  [PXUIField(DisplayName = "Rate Type")]
  public virtual string RateTypeID
  {
    get => this._RateTypeID;
    set => this._RateTypeID = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXDefault("M")]
  [PXUIField(DisplayName = "Mult/Div")]
  [PXStringList("M;Multiply,D;Divide")]
  public virtual string CuryMultDiv
  {
    get => this._CuryMultDiv;
    set => this._CuryMultDiv = value;
  }

  [PXDBDate]
  [PXDefault(typeof (AccessInfo.businessDate))]
  [PXUIField(DisplayName = "Currency Effective Date")]
  public virtual DateTime? CuryEffDate
  {
    get => this._CuryEffDate;
    set => this._CuryEffDate = value;
  }

  [PXDBDecimal(8)]
  [PXDefault(TypeCode.Decimal, "1.0")]
  [PXUIField(DisplayName = "Currency Rate")]
  public virtual Decimal? CuryRate
  {
    get => this._CuryRate;
    set => this._CuryRate = value;
  }

  [PXDBString(1, IsFixed = true, IsKey = true)]
  [PXDefault]
  [TranslationLineType.List]
  [PXUIField(DisplayName = "Line Type", Enabled = false)]
  public virtual string LineType
  {
    get => this._LineType;
    set => this._LineType = value;
  }

  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "Translation Batch Number", Visible = false)]
  public virtual string BatchNbr
  {
    get => this._BatchNbr;
    set => this._BatchNbr = value;
  }

  [PXDBInt]
  [PXDefault]
  [PXUIField]
  public virtual int? LineNbr
  {
    get => this._LineNbr;
    set => this._LineNbr = value;
  }

  [PXDBBaseCury(typeof (TranslationHistoryDetails.ledgerID))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Transaction Debit Amount")]
  [PXFormula(null, typeof (SumCalc<TranslationHistory.debitTot>))]
  public virtual Decimal? DebitAmt
  {
    get => this._DebitAmt;
    set => this._DebitAmt = value;
  }

  [PXDBBaseCury(typeof (TranslationHistoryDetails.ledgerID))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Transaction Credit Amount")]
  [PXFormula(null, typeof (SumCalc<TranslationHistory.creditTot>))]
  public virtual Decimal? CreditAmt
  {
    get => this._CreditAmt;
    set => this._CreditAmt = value;
  }

  [PXNote]
  public virtual Guid? NoteID
  {
    get => this._NoteID;
    set => this._NoteID = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Released", Enabled = false, Visible = false)]
  public virtual bool? Released
  {
    get => this._Released;
    set => this._Released = value;
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
    PrimaryKeyOf<TranslationHistoryDetails>.By<TranslationHistoryDetails.referenceNbr, TranslationHistoryDetails.branchID, TranslationHistoryDetails.accountID, TranslationHistoryDetails.subID, TranslationHistoryDetails.lineType>
  {
    public static TranslationHistoryDetails Find(
      PXGraph graph,
      string referenceNbr,
      int? branchID,
      int? accountID,
      int? subID,
      string lineType,
      PKFindOptions options = 0)
    {
      return (TranslationHistoryDetails) PrimaryKeyOf<TranslationHistoryDetails>.By<TranslationHistoryDetails.referenceNbr, TranslationHistoryDetails.branchID, TranslationHistoryDetails.accountID, TranslationHistoryDetails.subID, TranslationHistoryDetails.lineType>.FindBy(graph, (object) referenceNbr, (object) branchID, (object) accountID, (object) subID, (object) lineType, options);
    }
  }

  public static class FK
  {
    public class TranslationNumber : 
      PrimaryKeyOf<TranslationHistory>.By<TranslationHistory.referenceNbr>.ForeignKeyOf<TranslationHistoryDetails>.By<TranslationHistoryDetails.referenceNbr>
    {
    }

    public class Translation : 
      PrimaryKeyOf<TranslDef>.By<TranslDef.translDefId>.ForeignKeyOf<TranslationHistoryDetails>.By<TranslationHistoryDetails.translDefId>
    {
    }

    public class Ledger : 
      PrimaryKeyOf<PX.Objects.GL.Ledger>.By<PX.Objects.GL.Ledger.ledgerID>.ForeignKeyOf<TranslationHistoryDetails>.By<TranslationHistoryDetails.ledgerID>
    {
    }

    public class Branch : 
      PrimaryKeyOf<PX.Objects.GL.Branch>.By<PX.Objects.GL.Branch.branchID>.ForeignKeyOf<TranslationHistoryDetails>.By<TranslationHistoryDetails.branchID>
    {
    }

    public class Account : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<TranslationHistoryDetails>.By<TranslationHistoryDetails.accountID>
    {
    }

    public class Subaccount : 
      PrimaryKeyOf<Sub>.By<Sub.subID>.ForeignKeyOf<TranslationHistoryDetails>.By<TranslationHistoryDetails.accountID>
    {
    }

    public class Currency : 
      PrimaryKeyOf<Currency>.By<Currency.curyID>.ForeignKeyOf<TranslationHistoryDetails>.By<TranslationHistoryDetails.curyID>
    {
    }

    public class RateType : 
      PrimaryKeyOf<CurrencyRateType>.By<CurrencyRateType.curyRateTypeID>.ForeignKeyOf<TranslationHistoryDetails>.By<TranslationHistoryDetails.rateTypeID>
    {
    }
  }

  public abstract class referenceNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    TranslationHistoryDetails.referenceNbr>
  {
  }

  public abstract class translDefId : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    TranslationHistoryDetails.translDefId>
  {
  }

  public abstract class ledgerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  TranslationHistoryDetails.ledgerID>
  {
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  TranslationHistoryDetails.branchID>
  {
  }

  public abstract class accountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  TranslationHistoryDetails.accountID>
  {
  }

  public abstract class subID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  TranslationHistoryDetails.subID>
  {
  }

  public abstract class calcMode : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    TranslationHistoryDetails.calcMode>
  {
  }

  public abstract class sourceAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    TranslationHistoryDetails.sourceAmt>
  {
  }

  public abstract class translatedAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    TranslationHistoryDetails.translatedAmt>
  {
  }

  public abstract class origTranslatedAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    TranslationHistoryDetails.origTranslatedAmt>
  {
  }

  public abstract class finPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    TranslationHistoryDetails.finPeriodID>
  {
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TranslationHistoryDetails.curyID>
  {
  }

  public abstract class rateTypeID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    TranslationHistoryDetails.rateTypeID>
  {
  }

  public abstract class curyMultDiv : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    TranslationHistoryDetails.curyMultDiv>
  {
  }

  public abstract class curyEffDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    TranslationHistoryDetails.curyEffDate>
  {
  }

  public abstract class curyRate : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    TranslationHistoryDetails.curyRate>
  {
  }

  public abstract class lineType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    TranslationHistoryDetails.lineType>
  {
  }

  public abstract class batchNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    TranslationHistoryDetails.batchNbr>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  TranslationHistoryDetails.lineNbr>
  {
  }

  public abstract class debitAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    TranslationHistoryDetails.debitAmt>
  {
  }

  public abstract class creditAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    TranslationHistoryDetails.creditAmt>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  TranslationHistoryDetails.noteID>
  {
  }

  public abstract class released : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  TranslationHistoryDetails.released>
  {
  }

  public abstract class Tstamp : 
    BqlType<
    #nullable enable
    IBqlByteArray, byte[]>.Field<
    #nullable disable
    TranslationHistoryDetails.Tstamp>
  {
  }

  public abstract class createdByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    TranslationHistoryDetails.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    TranslationHistoryDetails.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    TranslationHistoryDetails.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    TranslationHistoryDetails.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    TranslationHistoryDetails.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    TranslationHistoryDetails.lastModifiedDateTime>
  {
  }
}
