// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.GLAllocationAccountHistory
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

[PXCacheName("GL Allocation History for Account")]
[Serializable]
public class GLAllocationAccountHistory : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _Module;
  protected string _BatchNbr;
  protected int? _BranchID;
  protected int? _AccountID;
  protected int? _SubID;
  protected Decimal? _AllocatedAmount;
  protected Decimal? _PriorPeriodsAllocAmount;
  protected int? _ContrAccontID;
  protected int? _ContrSubID;
  protected int? _SourceLedgerID;

  [PXDBString(2, IsKey = true, IsFixed = true)]
  [PXDBDefault(typeof (Batch))]
  public virtual string Module
  {
    get => this._Module;
    set => this._Module = value;
  }

  [PXDBString(15, IsUnicode = true, IsKey = true)]
  [PXDBDefault(typeof (Batch))]
  [PXParent(typeof (Select<Batch, Where<Batch.batchNbr, Equal<Current<GLAllocationAccountHistory.batchNbr>>, And<Batch.module, Equal<Current<GLAllocationAccountHistory.module>>>>>))]
  public virtual string BatchNbr
  {
    get => this._BatchNbr;
    set => this._BatchNbr = value;
  }

  [Branch(null, null, true, true, true, IsKey = true)]
  public virtual int? BranchID
  {
    get => this._BranchID;
    set => this._BranchID = value;
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

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? AllocatedAmount
  {
    get => this._AllocatedAmount;
    set => this._AllocatedAmount = value;
  }

  [PXDBDecimal(4)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? PriorPeriodsAllocAmount
  {
    get => this._PriorPeriodsAllocAmount;
    set => this._PriorPeriodsAllocAmount = value;
  }

  [PXDBInt]
  public virtual int? ContrAccontID
  {
    get => this._ContrAccontID;
    set => this._ContrAccontID = value;
  }

  [PXDBInt]
  public virtual int? ContrSubID
  {
    get => this._ContrSubID;
    set => this._ContrSubID = value;
  }

  [PXDBInt]
  public virtual int? SourceLedgerID
  {
    get => this._SourceLedgerID;
    set => this._SourceLedgerID = value;
  }

  public class PK : 
    PrimaryKeyOf<GLAllocationAccountHistory>.By<GLAllocationAccountHistory.module, GLAllocationAccountHistory.batchNbr, GLAllocationAccountHistory.branchID, GLAllocationAccountHistory.accountID, GLAllocationAccountHistory.subID>
  {
    public static GLAllocationAccountHistory Find(
      PXGraph graph,
      string module,
      string batchNbr,
      int? branchID,
      int? accountID,
      int? subID,
      PKFindOptions options = 0)
    {
      return (GLAllocationAccountHistory) PrimaryKeyOf<GLAllocationAccountHistory>.By<GLAllocationAccountHistory.module, GLAllocationAccountHistory.batchNbr, GLAllocationAccountHistory.branchID, GLAllocationAccountHistory.accountID, GLAllocationAccountHistory.subID>.FindBy(graph, (object) module, (object) batchNbr, (object) branchID, (object) accountID, (object) subID, options);
    }
  }

  public static class FK
  {
    public class Branch : 
      PrimaryKeyOf<Branch>.By<Branch.branchID>.ForeignKeyOf<GLAllocationAccountHistory>.By<GLAllocationAccountHistory.branchID>
    {
    }

    public class Batch : 
      PrimaryKeyOf<Batch>.By<Batch.module, Batch.batchNbr>.ForeignKeyOf<GLAllocationAccountHistory>.By<GLAllocationAccountHistory.module, GLAllocationAccountHistory.batchNbr>
    {
    }

    public class Account : 
      PrimaryKeyOf<Account>.By<Account.accountID>.ForeignKeyOf<GLAllocationAccountHistory>.By<GLAllocationAccountHistory.accountID>
    {
    }

    public class Subaccount : 
      PrimaryKeyOf<Sub>.By<Sub.subID>.ForeignKeyOf<GLAllocationAccountHistory>.By<GLAllocationAccountHistory.subID>
    {
    }

    public class ContraAccount : 
      PrimaryKeyOf<Account>.By<Account.accountID>.ForeignKeyOf<GLAllocationAccountHistory>.By<GLAllocationAccountHistory.contrAccontID>
    {
    }

    public class ContraSubaccount : 
      PrimaryKeyOf<Sub>.By<Sub.subID>.ForeignKeyOf<GLAllocationAccountHistory>.By<GLAllocationAccountHistory.contrSubID>
    {
    }

    public class SourceLedger : 
      PrimaryKeyOf<Ledger>.By<Ledger.ledgerID>.ForeignKeyOf<GLAllocationAccountHistory>.By<GLAllocationAccountHistory.sourceLedgerID>
    {
    }
  }

  public abstract class module : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLAllocationAccountHistory.module>
  {
  }

  public abstract class batchNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GLAllocationAccountHistory.batchNbr>
  {
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLAllocationAccountHistory.branchID>
  {
  }

  public abstract class accountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLAllocationAccountHistory.accountID>
  {
  }

  public abstract class subID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLAllocationAccountHistory.subID>
  {
  }

  public abstract class allocatedAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    GLAllocationAccountHistory.allocatedAmount>
  {
  }

  public abstract class priorPeriodsAllocAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    GLAllocationAccountHistory.priorPeriodsAllocAmount>
  {
  }

  public abstract class contrAccontID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    GLAllocationAccountHistory.contrAccontID>
  {
  }

  public abstract class contrSubID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    GLAllocationAccountHistory.contrSubID>
  {
  }

  public abstract class sourceLedgerID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    GLAllocationAccountHistory.sourceLedgerID>
  {
  }
}
