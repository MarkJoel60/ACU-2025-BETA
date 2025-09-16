// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.GLConsolHistory
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CM;
using System;

#nullable enable
namespace PX.Objects.GL;

[PXHidden]
[Serializable]
public class GLConsolHistory : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _SetupID;
  protected int? _BranchID;
  protected int? _LedgerID;
  protected int? _AccountID;
  protected int? _SubID;
  protected 
  #nullable disable
  string _FinPeriodID;
  protected Decimal? _PtdCredit;
  protected Decimal? _PtdDebit;
  protected byte[] _tstamp;

  [PXDBInt(IsKey = true)]
  [PXDefault]
  public virtual int? SetupID
  {
    get => this._SetupID;
    set => this._SetupID = value;
  }

  [PXDBInt(IsKey = true)]
  [PXDefault]
  public virtual int? BranchID
  {
    get => this._BranchID;
    set => this._BranchID = value;
  }

  [PXDBInt(IsKey = true)]
  [PXDefault]
  public virtual int? LedgerID
  {
    get => this._LedgerID;
    set => this._LedgerID = value;
  }

  [PXDBInt(IsKey = true)]
  [PXDefault]
  public virtual int? AccountID
  {
    get => this._AccountID;
    set => this._AccountID = value;
  }

  [PXDBInt(IsKey = true)]
  [PXDefault]
  public virtual int? SubID
  {
    get => this._SubID;
    set => this._SubID = value;
  }

  [PXDBString(6, IsKey = true)]
  [PXDefault]
  public virtual string FinPeriodID
  {
    get => this._FinPeriodID;
    set => this._FinPeriodID = value;
  }

  [PXDBBaseCury(typeof (GLHistory.ledgerID))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? PtdCredit
  {
    get => this._PtdCredit;
    set => this._PtdCredit = value;
  }

  [PXDBBaseCury(typeof (GLHistory.ledgerID))]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? PtdDebit
  {
    get => this._PtdDebit;
    set => this._PtdDebit = value;
  }

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  public class PK : 
    PrimaryKeyOf<GLConsolHistory>.By<GLConsolHistory.setupID, GLConsolHistory.ledgerID, GLConsolHistory.branchID, GLConsolHistory.accountID, GLConsolHistory.subID, GLConsolHistory.finPeriodID>
  {
    public static GLConsolHistory Find(
      PXGraph graph,
      int? setupID,
      int? ledgerID,
      int? branchID,
      int? accountID,
      int? subID,
      string finPeriodID,
      PKFindOptions options = 0)
    {
      return (GLConsolHistory) PrimaryKeyOf<GLConsolHistory>.By<GLConsolHistory.setupID, GLConsolHistory.ledgerID, GLConsolHistory.branchID, GLConsolHistory.accountID, GLConsolHistory.subID, GLConsolHistory.finPeriodID>.FindBy(graph, (object) setupID, (object) ledgerID, (object) branchID, (object) accountID, (object) subID, (object) finPeriodID, options);
    }
  }

  public static class FK
  {
    public class Branch : 
      PrimaryKeyOf<Branch>.By<Branch.branchID>.ForeignKeyOf<GLConsolHistory>.By<GLConsolHistory.branchID>
    {
    }

    public class Ledger : 
      PrimaryKeyOf<Ledger>.By<Ledger.ledgerID>.ForeignKeyOf<GLConsolHistory>.By<GLConsolHistory.ledgerID>
    {
    }

    public class Account : 
      PrimaryKeyOf<Account>.By<Account.accountID>.ForeignKeyOf<GLConsolHistory>.By<GLConsolHistory.accountID>
    {
    }

    public class Subaccount : 
      PrimaryKeyOf<Sub>.By<Sub.subID>.ForeignKeyOf<GLConsolHistory>.By<GLConsolHistory.subID>
    {
    }
  }

  public abstract class setupID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLConsolHistory.setupID>
  {
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLConsolHistory.branchID>
  {
  }

  public abstract class ledgerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLConsolHistory.ledgerID>
  {
  }

  public abstract class accountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLConsolHistory.accountID>
  {
  }

  public abstract class subID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLConsolHistory.subID>
  {
  }

  public abstract class finPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLConsolHistory.finPeriodID>
  {
  }

  public abstract class ptdCredit : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  GLConsolHistory.ptdCredit>
  {
  }

  public abstract class ptdDebit : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  GLConsolHistory.ptdDebit>
  {
  }
}
