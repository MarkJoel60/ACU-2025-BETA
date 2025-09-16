// Decompiled with JetBrains decompiler
// Type: PX.Objects.CM.TranslationHistory
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CS;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.CM;

/// <summary>
/// Represents currency translation history. The records of this type are created together with details (<see cref="T:PX.Objects.CM.TranslationHistoryDetails" />)
/// by the currency translation process (<see cref="T:PX.Objects.CM.TranslationProcess" />).
/// The header records store general results of the currency translation, including the dates, status, link to the resulting batch, and totals.
/// </summary>
[PXPrimaryGraph(typeof (TranslationHistoryMaint))]
[PXCacheName("Translation History")]
[Serializable]
public class TranslationHistory : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _ReferenceNbr;
  protected string _Description;
  protected string _TranslDefId;
  protected int? _BranchID;
  protected int? _LedgerID;
  protected string _DestCuryID;
  protected DateTime? _DateEntered;
  protected string _FinPeriodID;
  protected string _Status;
  protected string _BatchNbr;
  protected DateTime? _CuryEffDate;
  protected Decimal? _DebitTot;
  protected Decimal? _CreditTot;
  protected Decimal? _ControlTot;
  protected bool? _Released;
  protected Guid? _NoteID;
  protected bool? _Selected;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXDefault]
  [PXUIField]
  [PXSelector(typeof (TranslationHistory.referenceNbr))]
  [AutoNumber(typeof (CMSetup.translNumberingID), typeof (TranslationHistory.dateEntered))]
  [PXFieldDescription]
  public virtual string ReferenceNbr
  {
    get => this._ReferenceNbr;
    set => this._ReferenceNbr = value;
  }

  [PXDBString(60, IsUnicode = true)]
  [PXDefault]
  [PXUIField]
  public virtual string Description
  {
    get => this._Description;
    set => this._Description = value;
  }

  [PXDBString(10, IsUnicode = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Translation ID", Enabled = false)]
  [PXSelector(typeof (TranslDef.translDefId))]
  public virtual string TranslDefId
  {
    get => this._TranslDefId;
    set => this._TranslDefId = value;
  }

  [Branch(null, null, false, true, true, Enabled = false, Required = false)]
  public virtual int? BranchID
  {
    get => this._BranchID;
    set => this._BranchID = value;
  }

  [PXDBInt]
  [PXUIField]
  [PXSelector(typeof (Search<PX.Objects.GL.Ledger.ledgerID, Where<PX.Objects.GL.Ledger.balanceType, NotEqual<BudgetLedger>>>), SubstituteKey = typeof (PX.Objects.GL.Ledger.ledgerCD), DescriptionField = typeof (PX.Objects.GL.Ledger.descr), CacheGlobal = true)]
  public virtual int? LedgerID
  {
    get => this._LedgerID;
    set => this._LedgerID = value;
  }

  [PXDBString(5, IsUnicode = true)]
  [PXUIField]
  public virtual string DestCuryID
  {
    get => this._DestCuryID;
    set => this._DestCuryID = value;
  }

  [PXDBDate]
  [PXDefault(typeof (AccessInfo.businessDate))]
  [PXUIField]
  public virtual DateTime? DateEntered
  {
    get => this._DateEntered;
    set => this._DateEntered = value;
  }

  [ClosedPeriod(typeof (AccessInfo.businessDate))]
  [PXUIField]
  public virtual string FinPeriodID
  {
    get => this._FinPeriodID;
    set => this._FinPeriodID = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXDefault("U")]
  [PXUIField]
  [TranslationStatus.List]
  public virtual string Status
  {
    get => this._Status;
    set => this._Status = value;
  }

  [PXDBString(15, IsUnicode = true)]
  [PXSelector(typeof (Search<Batch.batchNbr, Where<Batch.module, Equal<BatchModule.moduleCM>>>))]
  [PXUIField]
  public virtual string BatchNbr
  {
    get => this._BatchNbr;
    set => this._BatchNbr = value;
  }

  [PXDBDate]
  [PXDefault(typeof (AccessInfo.businessDate))]
  [PXUIField(DisplayName = "Currency Effective Date", Enabled = false)]
  public virtual DateTime? CuryEffDate
  {
    get => this._CuryEffDate;
    set => this._CuryEffDate = value;
  }

  [PXUIField(DisplayName = "Debit Total", Enabled = false)]
  [PXDBBaseCury(typeof (TranslationHistory.ledgerID))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? DebitTot
  {
    get => this._DebitTot;
    set => this._DebitTot = value;
  }

  [PXUIField(DisplayName = "Credit Total", Enabled = false)]
  [PXDBBaseCury(typeof (TranslationHistory.ledgerID))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? CreditTot
  {
    get => this._CreditTot;
    set => this._CreditTot = value;
  }

  [PXUIField(DisplayName = "Control Total", Enabled = true, Required = true)]
  [PXDBBaseCury(typeof (TranslationHistory.ledgerID))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? ControlTot
  {
    get => this._ControlTot;
    set => this._ControlTot = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? Released
  {
    get => this._Released;
    set => this._Released = value;
  }

  [PXNote(DescriptionField = typeof (TranslationHistory.referenceNbr))]
  public virtual Guid? NoteID
  {
    get => this._NoteID;
    set => this._NoteID = value;
  }

  [PXBool]
  [PXUIField(DisplayName = "Selected")]
  public virtual bool? Selected
  {
    get => this._Selected;
    set => this._Selected = value;
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

  public class PK : PrimaryKeyOf<TranslationHistory>.By<TranslationHistory.referenceNbr>
  {
    public static TranslationHistory Find(
      PXGraph graph,
      string referenceNbr,
      PKFindOptions options = 0)
    {
      return (TranslationHistory) PrimaryKeyOf<TranslationHistory>.By<TranslationHistory.referenceNbr>.FindBy(graph, (object) referenceNbr, options);
    }
  }

  public static class FK
  {
    public class Translation : 
      PrimaryKeyOf<TranslDef>.By<TranslDef.translDefId>.ForeignKeyOf<TranslationHistory>.By<TranslationHistory.translDefId>
    {
    }

    public class Branch : 
      PrimaryKeyOf<PX.Objects.GL.Branch>.By<PX.Objects.GL.Branch.branchID>.ForeignKeyOf<TranslationHistory>.By<TranslationHistory.branchID>
    {
    }

    public class Ledger : 
      PrimaryKeyOf<PX.Objects.GL.Ledger>.By<PX.Objects.GL.Ledger.ledgerID>.ForeignKeyOf<TranslationHistory>.By<TranslationHistory.ledgerID>
    {
    }

    public class DestinationCurrency : 
      PrimaryKeyOf<Currency>.By<Currency.curyID>.ForeignKeyOf<TranslationHistory>.By<TranslationHistory.destCuryID>
    {
    }
  }

  public abstract class referenceNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    TranslationHistory.referenceNbr>
  {
  }

  public abstract class description : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    TranslationHistory.description>
  {
  }

  public abstract class translDefId : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    TranslationHistory.translDefId>
  {
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  TranslationHistory.branchID>
  {
  }

  public abstract class ledgerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  TranslationHistory.ledgerID>
  {
  }

  public abstract class destCuryID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TranslationHistory.destCuryID>
  {
  }

  public abstract class dateEntered : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    TranslationHistory.dateEntered>
  {
  }

  public abstract class finPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    TranslationHistory.finPeriodID>
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TranslationHistory.status>
  {
  }

  public abstract class batchNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TranslationHistory.batchNbr>
  {
  }

  public abstract class curyEffDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    TranslationHistory.curyEffDate>
  {
  }

  public abstract class debitTot : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  TranslationHistory.debitTot>
  {
  }

  public abstract class creditTot : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  TranslationHistory.creditTot>
  {
  }

  public abstract class controlTot : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    TranslationHistory.controlTot>
  {
  }

  public abstract class released : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  TranslationHistory.released>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  TranslationHistory.noteID>
  {
  }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  TranslationHistory.selected>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  TranslationHistory.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  TranslationHistory.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    TranslationHistory.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    TranslationHistory.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    TranslationHistory.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    TranslationHistory.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    TranslationHistory.lastModifiedDateTime>
  {
  }
}
