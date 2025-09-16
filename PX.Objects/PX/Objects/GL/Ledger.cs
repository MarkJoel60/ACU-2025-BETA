// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.Ledger
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.Objects.GL;

/// <summary>
/// A financial ledger.
/// The records of this type are added and edited on the Ledger (GL201500) form
/// (which corresponds to the <see cref="T:PX.Objects.GL.GeneralLedgerMaint" /> graph).
/// </summary>
[PXPrimaryGraph(typeof (GeneralLedgerMaint))]
[PXCacheName("Ledger", CacheGlobal = true)]
[Serializable]
public class Ledger : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected bool? _Selected = new bool?(false);
  protected int? _LedgerID;
  protected 
  #nullable disable
  string _LedgerCD;
  protected string _BaseCuryID;
  protected string _Descr;
  protected string _BalanceType;
  protected int? _DefBranchID;
  protected bool? _PostInterCompany;
  protected bool? _ConsolAllowed;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  /// <summary>
  /// Indicates whether the Ledger is selected for processing.
  /// </summary>
  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Selected")]
  public bool? Selected
  {
    get => this._Selected;
    set => this._Selected = value;
  }

  /// <summary>
  /// Database identity.
  /// Unique identifier of the Ledger.
  /// </summary>
  [PXDBIdentity]
  [PXUIField]
  public virtual int? LedgerID
  {
    get => this._LedgerID;
    set => this._LedgerID = value;
  }

  /// <summary>
  /// Key field.
  /// Unique user-friendly identifier of the Ledger.
  /// </summary>
  [PXSelector(typeof (Search<Ledger.ledgerCD>))]
  [PXDBString(10, IsUnicode = true, IsKey = true, InputMask = ">CCCCCCCCCC")]
  [PXDefault]
  [PXUIField]
  public virtual string LedgerCD
  {
    get => this._LedgerCD;
    set => this._LedgerCD = value;
  }

  /// <summary>
  /// Reference to <see cref="!:Organization" /> record. The field is filled only for ledgers of actual type and is used for data denormalization.
  /// </summary>
  [PXDBInt]
  public virtual int? OrganizationID { get; set; }

  /// <summary>
  /// Base <see cref="T:PX.Objects.CM.Currency" /> of the Ledger.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="!:Currency.CuryID" /> field.
  /// Defaults to the <see cref="P:PX.Objects.GL.Company.BaseCuryID">base currency of the company</see>.
  /// </value>
  [PXDBString(5, IsUnicode = true, InputMask = ">LLLLL")]
  [PXDefault(typeof (Current<AccessInfo.baseCuryID>))]
  [PXUIField]
  [PXSelector(typeof (PX.Objects.CM.Currency.curyID))]
  public virtual string BaseCuryID
  {
    get => this._BaseCuryID;
    set => this._BaseCuryID = value;
  }

  /// <summary>The description of the Ledger.</summary>
  [PXDBLocalizableString(60, IsUnicode = true)]
  [PXDefault("")]
  [PXUIField]
  public virtual string Descr
  {
    get => this._Descr;
    set => this._Descr = value;
  }

  /// <summary>The type of the balance of the ledger.</summary>
  /// <value>
  /// Possible values are:
  /// <c>"A"</c> - Actual,
  /// <c>"R"</c> - Reporting,
  /// <c>"S"</c> - Statistical,
  /// <c>"B"</c> - Budget.
  /// </value>
  [PXDBString(1, IsFixed = true)]
  [PXDefault("A")]
  [LedgerBalanceType.List]
  [PXUIField]
  public virtual string BalanceType
  {
    get => this._BalanceType;
    set => this._BalanceType = value;
  }

  /// <summary>
  /// The identifier of the consolidating <see cref="T:PX.Objects.GL.Branch" /> of the Ledger.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.GL.Branch.BranchID" /> field.
  /// </value>
  [Obsolete("This field has been deprecated and will be removed in Acumatica ERP 2018R2.")]
  [PXDBInt]
  public virtual int? DefBranchID
  {
    get => this._DefBranchID;
    set => this._DefBranchID = value;
  }

  /// <summary>
  /// When set to <c>true</c>, indicates that the system must automatically generate inter-branch transactions
  /// for this Ledger to balance transactions for all branches involved.
  /// This field is relevant only if the <see cref="!:FeaturesSet.InterBranch">Inter-Branch Transactions</see> feature has been activated
  /// and the <see cref="P:PX.Objects.GL.Ledger.DefBranchID">Consolidating Branch</see> is specified.
  /// </summary>
  /// <value>
  /// Defaults to <c>false</c>.
  /// </value>
  [PXDBBool]
  [PXUIField(DisplayName = "Branch Accounting")]
  [PXDefault(true)]
  [Obsolete("This field has been deprecated and will be removed in Acumatica ERP 2018R2.")]
  public virtual bool? PostInterCompany
  {
    get => this._PostInterCompany;
    set => this._PostInterCompany = value;
  }

  /// <summary>
  /// When set to <c>true</c>, indicates that the system must use the Ledger as a source Ledger for consolidation.
  /// </summary>
  /// <value>
  /// Defaults to <c>false</c>.
  /// </value>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Consolidation Source")]
  public virtual bool? ConsolAllowed
  {
    get => this._ConsolAllowed;
    set => this._ConsolAllowed = value;
  }

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  [PXNote]
  public virtual Guid? NoteID { get; set; }

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

  public class PK : PrimaryKeyOf<Ledger>.By<Ledger.ledgerID>
  {
    public static Ledger Find(PXGraph graph, int? ledgerID)
    {
      return (Ledger) PrimaryKeyOf<Ledger>.By<Ledger.ledgerID>.FindBy(graph, (object) ledgerID, ledgerID.GetValueOrDefault() <= 0);
    }
  }

  public class UK : PrimaryKeyOf<Ledger>.By<Ledger.ledgerCD>
  {
    public static Ledger Find(PXGraph graph, string ledgerCD, PKFindOptions options = 0)
    {
      return (Ledger) PrimaryKeyOf<Ledger>.By<Ledger.ledgerCD>.FindBy(graph, (object) ledgerCD, options);
    }
  }

  public static class FK
  {
    public class Organization : 
      PrimaryKeyOf<PX.Objects.GL.DAC.Organization>.By<PX.Objects.GL.DAC.Organization.organizationID>.ForeignKeyOf<Ledger>.By<Ledger.organizationID>
    {
    }

    public class BaseCurrency : 
      PrimaryKeyOf<PX.Objects.CM.Currency>.By<PX.Objects.CM.Currency.curyID>.ForeignKeyOf<Ledger>.By<Ledger.baseCuryID>
    {
    }
  }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Ledger.selected>
  {
  }

  public abstract class ledgerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Ledger.ledgerID>
  {
  }

  public abstract class ledgerCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Ledger.ledgerCD>
  {
  }

  public abstract class organizationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Ledger.organizationID>
  {
  }

  public abstract class baseCuryID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Ledger.baseCuryID>
  {
  }

  public abstract class descr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Ledger.descr>
  {
  }

  public abstract class balanceType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Ledger.balanceType>
  {
  }

  public abstract class defBranchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Ledger.defBranchID>
  {
  }

  public abstract class postInterCompany : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Ledger.postInterCompany>
  {
  }

  public abstract class consolAllowed : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Ledger.consolAllowed>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  Ledger.Tstamp>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  Ledger.noteID>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  Ledger.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    Ledger.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    Ledger.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  Ledger.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    Ledger.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    Ledger.lastModifiedDateTime>
  {
  }
}
