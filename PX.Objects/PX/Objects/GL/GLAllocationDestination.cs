// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.GLAllocationDestination
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.GL.DAC;
using System;

#nullable enable
namespace PX.Objects.GL;

[PXCacheName("GL Allocation Destination")]
[Serializable]
public class GLAllocationDestination : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _BranchID;
  protected 
  #nullable disable
  string _GLAllocationID;
  protected int? _LineID;
  protected string _AccountCD;
  protected string _SubCD;
  protected int? _BasisBranchID;
  protected string _BasisAccountCD;
  protected string _BasisSubCD;
  protected Decimal? _Weight;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  [PXDefault(typeof (Search<Branch.branchID, Where<Branch.branchID, Equal<Current<GLAllocation.branchID>>>>))]
  [Branch(null, typeof (Search2<Branch.branchID, InnerJoin<PX.Objects.GL.DAC.Organization, On<PX.Objects.GL.DAC.Organization.organizationID, Equal<Branch.organizationID>>, LeftJoin<OrganizationLedgerLink, On<Branch.organizationID, Equal<OrganizationLedgerLink.organizationID>, And<CurrentValue<GLAllocation.allocLedgerBalanceType>, NotEqual<LedgerBalanceType.actual>>>>>, Where2<Match<Current<AccessInfo.userName>>, And2<MatchWithBranch<Branch.branchID>, And<Where<CurrentValue<GLAllocation.allocLedgerBalanceType>, NotEqual<LedgerBalanceType.actual>, And<OrganizationLedgerLink.ledgerID, Equal<Current<GLAllocation.allocLedgerID>>, Or<CurrentValue<GLAllocation.allocLedgerBalanceType>, Equal<LedgerBalanceType.actual>, And<Branch.baseCuryID, Equal<CurrentValue<GLAllocation.allocLedgerBaseCuryID>>>>>>>>>>), true, true, false)]
  public virtual int? BranchID
  {
    get => this._BranchID;
    set => this._BranchID = value;
  }

  [PXDBString(15, IsUnicode = true, IsKey = true)]
  [PXDBDefault(typeof (GLAllocation.gLAllocationID))]
  [PXUIField(DisplayName = "Allocation ID", Visible = false)]
  [PXParent(typeof (Select<GLAllocation, Where<GLAllocation.gLAllocationID, Equal<Current<GLAllocationDestination.gLAllocationID>>>>))]
  public virtual string GLAllocationID
  {
    get => this._GLAllocationID;
    set => this._GLAllocationID = value;
  }

  [PXDBIdentity(IsKey = true)]
  [PXUIField(DisplayName = "Line Nbr.", Visible = false)]
  public virtual int? LineID
  {
    get => this._LineID;
    set => this._LineID = value;
  }

  [PXDefault]
  [AccountCDWildcard(typeof (Search2<Account.accountCD, LeftJoin<GLSetup, On<GLSetup.ytdNetIncAccountID, Equal<Account.accountID>>, LeftJoin<Ledger, On<Ledger.ledgerID, Equal<Current<GLAllocation.allocLedgerID>>>>>, Where2<Match<Current<AccessInfo.userName>>, And<Account.active, Equal<True>, And<Account.accountingType, Equal<AccountEntityType.gLAccount>, And<Where<Account.curyID, IsNull, Or<Account.curyID, Equal<Ledger.baseCuryID>, And<GLSetup.ytdNetIncAccountID, IsNull>>>>>>>>), DescriptionField = typeof (Account.description))]
  public virtual string AccountCD
  {
    get => this._AccountCD;
    set => this._AccountCD = value;
  }

  [SubCDWildcard]
  [PXDefault]
  public virtual string SubCD
  {
    get => this._SubCD;
    set => this._SubCD = value;
  }

  [Branch(null, typeof (Search2<Branch.branchID, InnerJoin<PX.Objects.GL.DAC.Organization, On<PX.Objects.GL.DAC.Organization.organizationID, Equal<Branch.organizationID>>, InnerJoin<OrganizationLedgerLink, On<Branch.organizationID, Equal<OrganizationLedgerLink.organizationID>, And<OrganizationLedgerLink.ledgerID, Equal<Current<GLAllocation.basisLederID>>>>>>, Where<Match<Current<AccessInfo.userName>>>>), true, true, false)]
  public virtual int? BasisBranchID
  {
    get => this._BasisBranchID;
    set => this._BasisBranchID = value;
  }

  [AccountCDWildcard(typeof (Search<Account.accountCD, Where<Account.active, Equal<True>, And<Account.accountingType, Equal<AccountEntityType.gLAccount>>>>), DescriptionField = typeof (Account.description), DisplayName = "Base Account")]
  public virtual string BasisAccountCD
  {
    get => this._BasisAccountCD;
    set => this._BasisAccountCD = value;
  }

  [SubCDWildcard(DisplayName = "Base Subaccount")]
  public virtual string BasisSubCD
  {
    get => this._BasisSubCD;
    set => this._BasisSubCD = value;
  }

  [PXDBDecimal(6)]
  [PXUIField(DisplayName = "Weight/Percent")]
  public virtual Decimal? Weight
  {
    get => this._Weight;
    set => this._Weight = value;
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
    PrimaryKeyOf<GLAllocationDestination>.By<GLAllocationDestination.gLAllocationID, GLAllocationDestination.lineID>
  {
    public static GLAllocationDestination Find(
      PXGraph graph,
      string gLAllocationID,
      int? lineID,
      PKFindOptions options = 0)
    {
      return (GLAllocationDestination) PrimaryKeyOf<GLAllocationDestination>.By<GLAllocationDestination.gLAllocationID, GLAllocationDestination.lineID>.FindBy(graph, (object) gLAllocationID, (object) lineID, options);
    }
  }

  public static class FK
  {
    public class Branch : 
      PrimaryKeyOf<Branch>.By<Branch.branchID>.ForeignKeyOf<GLAllocationDestination>.By<GLAllocationDestination.branchID>
    {
    }

    public class Allocation : 
      PrimaryKeyOf<GLAllocation>.By<GLAllocation.gLAllocationID>.ForeignKeyOf<GLAllocationDestination>.By<GLAllocationDestination.gLAllocationID>
    {
    }

    public class Account : 
      PrimaryKeyOf<Account>.By<Account.accountCD>.ForeignKeyOf<GLAllocationDestination>.By<GLAllocationDestination.accountCD>
    {
    }

    public class Subaccount : 
      PrimaryKeyOf<Sub>.By<Sub.subCD>.ForeignKeyOf<GLAllocationDestination>.By<GLAllocationDestination.subCD>
    {
    }

    public class BaseBranch : 
      PrimaryKeyOf<Branch>.By<Branch.branchID>.ForeignKeyOf<GLAllocationDestination>.By<GLAllocationDestination.basisBranchID>
    {
    }

    public class BaseAccount : 
      PrimaryKeyOf<Account>.By<Account.accountCD>.ForeignKeyOf<GLAllocationDestination>.By<GLAllocationDestination.basisAccountCD>
    {
    }

    public class BaseSubaccount : 
      PrimaryKeyOf<Sub>.By<Sub.subCD>.ForeignKeyOf<GLAllocationDestination>.By<GLAllocationDestination.basisSubCD>
    {
    }
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLAllocationDestination.branchID>
  {
  }

  public abstract class gLAllocationID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GLAllocationDestination.gLAllocationID>
  {
  }

  public abstract class lineID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLAllocationDestination.lineID>
  {
  }

  public abstract class accountCD : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GLAllocationDestination.accountCD>
  {
  }

  public abstract class subCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLAllocationDestination.subCD>
  {
  }

  public abstract class basisBranchID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    GLAllocationDestination.basisBranchID>
  {
  }

  public abstract class basisAccountCD : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GLAllocationDestination.basisAccountCD>
  {
  }

  public abstract class basisSubCD : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GLAllocationDestination.basisSubCD>
  {
  }

  public abstract class weight : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  GLAllocationDestination.weight>
  {
  }

  public abstract class createdByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    GLAllocationDestination.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GLAllocationDestination.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    GLAllocationDestination.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    GLAllocationDestination.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GLAllocationDestination.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    GLAllocationDestination.lastModifiedDateTime>
  {
  }
}
