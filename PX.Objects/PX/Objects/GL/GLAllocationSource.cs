// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.GLAllocationSource
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CM;
using PX.Objects.GL.DAC;
using System;

#nullable enable
namespace PX.Objects.GL;

[PXCacheName("GL Allocation Source")]
[Serializable]
public class GLAllocationSource : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _BranchID;
  protected 
  #nullable disable
  string _GLAllocationID;
  protected int? _LineID;
  protected string _AccountCD;
  protected string _SubCD;
  protected int? _ContrAccountID;
  protected int? _ContrSubID;
  protected string _PercentLimitType;
  protected Decimal? _LimitAmount;
  protected Decimal? _LimitPercent;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  [PXDefault(typeof (Search2<Branch.branchID, InnerJoin<OrganizationLedgerLink, On<OrganizationLedgerLink.organizationID, Equal<Branch.organizationID>, And<OrganizationLedgerLink.ledgerID, Equal<Current<GLAllocation.sourceLedgerID>>, And<Branch.branchID, Equal<Current<GLAllocation.branchID>>>>>>, Where<Match<Current<AccessInfo.userName>>>>))]
  [Branch(null, typeof (Search2<Branch.branchID, InnerJoin<PX.Objects.GL.DAC.Organization, On<PX.Objects.GL.DAC.Organization.organizationID, Equal<Branch.organizationID>>, InnerJoin<OrganizationLedgerLink, On<Branch.organizationID, Equal<OrganizationLedgerLink.organizationID>, And<OrganizationLedgerLink.ledgerID, Equal<Current<GLAllocation.sourceLedgerID>>>>>>, Where2<Match<Current<AccessInfo.userName>>, And<MatchWithBranch<Branch.branchID>>>>), true, true, false)]
  public virtual int? BranchID
  {
    get => this._BranchID;
    set => this._BranchID = value;
  }

  [PXDBString(15, IsUnicode = true, IsKey = true)]
  [PXDBDefault(typeof (GLAllocation.gLAllocationID))]
  [PXUIField(DisplayName = "Allocation ID", Visible = false)]
  [PXParent(typeof (Select<GLAllocation, Where<GLAllocation.gLAllocationID, Equal<Current<GLAllocationSource.gLAllocationID>>>>))]
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

  [AccountCDWildcard(typeof (Search<Account.accountCD, Where<Account.active, Equal<True>, And<Account.accountingType, Equal<AccountEntityType.gLAccount>>>>), DescriptionField = typeof (Account.description))]
  [PXDefault]
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

  [Account(null, typeof (Search2<Account.accountID, LeftJoin<GLSetup, On<GLSetup.ytdNetIncAccountID, Equal<Account.accountID>>, LeftJoin<Ledger, On<Ledger.ledgerID, Equal<Current<GLAllocation.allocLedgerID>>>>>, Where2<Match<Current<AccessInfo.userName>>, And<Account.active, Equal<True>, And<Where<Account.curyID, IsNull, Or<Account.curyID, Equal<Ledger.baseCuryID>, And<GLSetup.ytdNetIncAccountID, IsNull>>>>>>>))]
  public virtual int? ContrAccountID
  {
    get => this._ContrAccountID;
    set => this._ContrAccountID = value;
  }

  [SubAccount(typeof (GLAllocationSource.contrAccountID))]
  public virtual int? ContrSubID
  {
    get => this._ContrSubID;
    set => this._ContrSubID = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXUIField(DisplayName = "Percent Limit Type", Visible = false)]
  [PX.Objects.GL.Constants.PercentLimitType.List]
  public virtual string PercentLimitType
  {
    get => this._PercentLimitType;
    set => this._PercentLimitType = value;
  }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBBaseCury(typeof (GLAllocation.sourceLedgerID), MinValue = 0.0)]
  [PXUIField(DisplayName = "Amount Limit")]
  public virtual Decimal? LimitAmount
  {
    get => this._LimitAmount;
    set => this._LimitAmount = value;
  }

  [PXDBDecimal(2, MinValue = 0.0, MaxValue = 100.0)]
  [PXDefault(TypeCode.Decimal, "100.0")]
  [PXUIField(DisplayName = "Percentage Limit")]
  public virtual Decimal? LimitPercent
  {
    get => this._LimitPercent;
    set => this._LimitPercent = value;
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
    PrimaryKeyOf<GLAllocationSource>.By<GLAllocationSource.gLAllocationID, GLAllocationSource.lineID>
  {
    public static GLAllocationSource Find(
      PXGraph graph,
      string gLAllocationID,
      int? lineID,
      PKFindOptions options = 0)
    {
      return (GLAllocationSource) PrimaryKeyOf<GLAllocationSource>.By<GLAllocationSource.gLAllocationID, GLAllocationSource.lineID>.FindBy(graph, (object) gLAllocationID, (object) lineID, options);
    }
  }

  public static class FK
  {
    public class Branch : 
      PrimaryKeyOf<Branch>.By<Branch.branchID>.ForeignKeyOf<GLAllocationSource>.By<GLAllocationSource.branchID>
    {
    }

    public class Allocation : 
      PrimaryKeyOf<GLAllocation>.By<GLAllocation.gLAllocationID>.ForeignKeyOf<GLAllocationSource>.By<GLAllocationSource.gLAllocationID>
    {
    }

    public class Account : 
      PrimaryKeyOf<Account>.By<Account.accountCD>.ForeignKeyOf<GLAllocationSource>.By<GLAllocationSource.accountCD>
    {
    }

    public class Subaccount : 
      PrimaryKeyOf<Sub>.By<Sub.subCD>.ForeignKeyOf<GLAllocationSource>.By<GLAllocationSource.subCD>
    {
    }

    public class ContraAccount : 
      PrimaryKeyOf<Account>.By<Account.accountID>.ForeignKeyOf<GLAllocationSource>.By<GLAllocationSource.contrAccountID>
    {
    }

    public class ContraSubaccount : 
      PrimaryKeyOf<Sub>.By<Sub.subID>.ForeignKeyOf<GLAllocationSource>.By<GLAllocationSource.contrSubID>
    {
    }
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLAllocationSource.branchID>
  {
  }

  public abstract class gLAllocationID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GLAllocationSource.gLAllocationID>
  {
  }

  public abstract class lineID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLAllocationSource.lineID>
  {
  }

  public abstract class accountCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLAllocationSource.accountCD>
  {
  }

  public abstract class subCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLAllocationSource.subCD>
  {
  }

  public abstract class contrAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    GLAllocationSource.contrAccountID>
  {
  }

  public abstract class contrSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  GLAllocationSource.contrSubID>
  {
  }

  public abstract class percentLimitType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GLAllocationSource.percentLimitType>
  {
  }

  public abstract class limitAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    GLAllocationSource.limitAmount>
  {
  }

  public abstract class limitPercent : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    GLAllocationSource.limitPercent>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  GLAllocationSource.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GLAllocationSource.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    GLAllocationSource.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    GLAllocationSource.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    GLAllocationSource.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    GLAllocationSource.lastModifiedDateTime>
  {
  }
}
