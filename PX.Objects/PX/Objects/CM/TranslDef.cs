// Decompiled with JetBrains decompiler
// Type: PX.Objects.CM.TranslDef
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.CM;

/// <summary>
/// Represents the currency translation definition, which determines the parameters of translation, such as source and destination
/// ledgers and currencies. Records of this type are accompanied by details of the <see cref="T:PX.Objects.CM.TranslDefDet" /> type.
/// Translation definitions are edited on the Translation Definition (CM203000) form backed by the <see cref="T:PX.Objects.CM.TranslationDefinitionMaint" /> graph.
/// </summary>
[PXPrimaryGraph(typeof (TranslationDefinitionMaint))]
[PXCacheName("Translation Definition")]
[Serializable]
public class TranslDef : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _TranslDefId;
  protected int? _BranchID;
  protected bool? _Active;
  protected string _Description;
  protected int? _SourceLedgerId;
  protected int? _DestLedgerId;
  protected int? _LineCntr;
  protected Guid? _NoteID;
  protected string _SourceCuryID;
  protected string _DestCuryID;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  [PXDBString(10, IsUnicode = true, IsKey = true)]
  [PXDefault]
  [PXUIField]
  [PXSelector(typeof (TranslDef.translDefId))]
  [PXFieldDescription]
  public virtual string TranslDefId
  {
    get => this._TranslDefId;
    set => this._TranslDefId = value;
  }

  [Branch(null, null, true, true, true)]
  public virtual int? BranchID
  {
    get => this._BranchID;
    set => this._BranchID = value;
  }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Active")]
  public virtual bool? Active
  {
    get => this._Active;
    set => this._Active = value;
  }

  [PXDBString(60, IsUnicode = true)]
  [PXUIField]
  public virtual string Description
  {
    get => this._Description;
    set => this._Description = value;
  }

  [PXDBInt]
  [PXDefault(typeof (Search<PX.Objects.GL.Branch.ledgerID, Where<PX.Objects.GL.Branch.branchID, Equal<Current<AccessInfo.branchID>>>>))]
  [PXUIField]
  [PXSelector(typeof (Search<PX.Objects.GL.Ledger.ledgerID, Where<PX.Objects.GL.Ledger.balanceType, Equal<ActualLedger>, Or<PX.Objects.GL.Ledger.balanceType, Equal<ReportLedger>>>>), SubstituteKey = typeof (PX.Objects.GL.Ledger.ledgerCD), DescriptionField = typeof (PX.Objects.GL.Ledger.descr), CacheGlobal = true)]
  public virtual int? SourceLedgerId
  {
    get => this._SourceLedgerId;
    set => this._SourceLedgerId = value;
  }

  [PXDBInt]
  [PXDefault]
  [PXUIField]
  [PXSelector(typeof (Search<PX.Objects.GL.Ledger.ledgerID, Where<PX.Objects.GL.Ledger.balanceType, Equal<ReportLedger>>>), SubstituteKey = typeof (PX.Objects.GL.Ledger.ledgerCD), DescriptionField = typeof (PX.Objects.GL.Ledger.descr), CacheGlobal = true)]
  public virtual int? DestLedgerId
  {
    get => this._DestLedgerId;
    set => this._DestLedgerId = value;
  }

  [PXDBInt]
  [PXDefault(0)]
  public virtual int? LineCntr
  {
    get => this._LineCntr;
    set => this._LineCntr = value;
  }

  [PXNote(DescriptionField = typeof (TranslDef.translDefId))]
  public virtual Guid? NoteID
  {
    get => this._NoteID;
    set => this._NoteID = value;
  }

  [PXString(5, IsUnicode = true)]
  [PXUIField]
  public virtual string SourceCuryID
  {
    get => this._SourceCuryID;
    set => this._SourceCuryID = value;
  }

  [PXString(5, IsUnicode = true)]
  [PXUIField]
  public virtual string DestCuryID
  {
    get => this._DestCuryID;
    set => this._DestCuryID = value;
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

  public class PK : PrimaryKeyOf<TranslDef>.By<TranslDef.translDefId>
  {
    public static TranslDef Find(PXGraph graph, string translDefId, PKFindOptions options = 0)
    {
      return (TranslDef) PrimaryKeyOf<TranslDef>.By<TranslDef.translDefId>.FindBy(graph, (object) translDefId, options);
    }
  }

  public static class FK
  {
    public class Branch : 
      PrimaryKeyOf<PX.Objects.GL.Branch>.By<PX.Objects.GL.Branch.branchID>.ForeignKeyOf<TranslDef>.By<TranslDef.branchID>
    {
    }

    public class SourceLedger : 
      PrimaryKeyOf<PX.Objects.GL.Ledger>.By<PX.Objects.GL.Ledger.ledgerID>.ForeignKeyOf<TranslDef>.By<TranslDef.sourceLedgerId>
    {
    }

    public class DestinationLedger : 
      PrimaryKeyOf<PX.Objects.GL.Ledger>.By<PX.Objects.GL.Ledger.ledgerID>.ForeignKeyOf<TranslDef>.By<TranslDef.destLedgerId>
    {
    }

    public class SourceCurrency : 
      PrimaryKeyOf<Currency>.By<Currency.curyID>.ForeignKeyOf<TranslDef>.By<TranslDef.sourceCuryID>
    {
    }

    public class DestinationCurrency : 
      PrimaryKeyOf<Currency>.By<Currency.curyID>.ForeignKeyOf<TranslDef>.By<TranslDef.destCuryID>
    {
    }
  }

  public abstract class translDefId : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TranslDef.translDefId>
  {
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  TranslDef.branchID>
  {
  }

  public abstract class active : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  TranslDef.active>
  {
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TranslDef.description>
  {
  }

  public abstract class sourceLedgerId : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  TranslDef.sourceLedgerId>
  {
  }

  public abstract class destLedgerId : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  TranslDef.destLedgerId>
  {
  }

  public abstract class lineCntr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  TranslDef.lineCntr>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  TranslDef.noteID>
  {
  }

  public abstract class sourceCuryID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TranslDef.sourceCuryID>
  {
  }

  public abstract class destCuryID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TranslDef.destCuryID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  TranslDef.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  TranslDef.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    TranslDef.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    TranslDef.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  TranslDef.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    TranslDef.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    TranslDef.lastModifiedDateTime>
  {
  }
}
