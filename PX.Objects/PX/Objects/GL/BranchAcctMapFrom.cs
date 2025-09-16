// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.BranchAcctMapFrom
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

[PXProjection(typeof (Select<BranchAcctMap, Where<BranchAcctMap.branchID, Equal<BranchAcctMap.fromBranchID>>>), Persistent = true)]
[PXCacheName("Branch Account Map From")]
[Serializable]
public class BranchAcctMapFrom : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBInt(IsKey = true, BqlTable = typeof (BranchAcctMap))]
  [PXDBDefault(typeof (Branch.branchID))]
  public virtual int? BranchID { get; set; }

  [PXDBInt(IsKey = true, BqlTable = typeof (BranchAcctMap))]
  [PXLineNbr(typeof (Branch.acctMapNbr))]
  public virtual int? LineNbr { get; set; }

  [PXDBInt(BqlTable = typeof (BranchAcctMap))]
  [PXDBDefault(typeof (Branch.branchID))]
  public virtual int? FromBranchID { get; set; }

  [PXDBInt(BqlTable = typeof (BranchAcctMap))]
  [PXSelector(typeof (Search<Branch.branchID, Where<Branch.branchID, NotEqual<Current<BranchAcctMapFrom.branchID>>>>), SubstituteKey = typeof (Branch.branchCD))]
  [PXUIField(DisplayName = "Destination Branch")]
  [PXRestrictor(typeof (Where<Branch.active, Equal<True>>), "Branch is inactive.", new Type[] {})]
  public virtual int? ToBranchID { get; set; }

  [PXDBString(10, IsUnicode = true, InputMask = "", BqlTable = typeof (BranchAcctMap))]
  [PXDefault]
  [PXDimensionSelector("ACCOUNT", typeof (Search<Account.accountCD, Where<Account.accountingType, Equal<AccountEntityType.gLAccount>>>), DescriptionField = typeof (Account.description))]
  [PXUIField(DisplayName = "Account From")]
  public virtual 
  #nullable disable
  string FromAccountCD { get; set; }

  [PXDBString(10, IsUnicode = true, InputMask = "", BqlTable = typeof (BranchAcctMap))]
  [PXDefault]
  [PXDimensionSelector("ACCOUNT", typeof (Search<Account.accountCD, Where<Account.accountingType, Equal<AccountEntityType.gLAccount>>>), DescriptionField = typeof (Account.description))]
  [PXUIField(DisplayName = "Account To")]
  public virtual string ToAccountCD { get; set; }

  [Account(null, typeof (Search<Account.accountID, Where<Account.isCashAccount, Equal<False>, And<Account.curyID, IsNull>>>), DescriptionField = typeof (Account.description), BqlTable = typeof (BranchAcctMap), DisplayName = "Offset Account", AvoidControlAccounts = true)]
  [PXDefault]
  [PXForeignReference(typeof (Field<BranchAcctMapFrom.mapAccountID>.IsRelatedTo<Account.accountID>))]
  public virtual int? MapAccountID { get; set; }

  [SubAccount(typeof (BranchAcctMapFrom.mapAccountID), DisplayName = "Offset Subaccount", DescriptionField = typeof (Account.description), BqlTable = typeof (BranchAcctMap))]
  [PXDefault]
  [PXForeignReference(typeof (Field<BranchAcctMapFrom.mapSubID>.IsRelatedTo<Sub.subID>))]
  public virtual int? MapSubID { get; set; }

  public class PK : 
    PrimaryKeyOf<BranchAcctMapFrom>.By<BranchAcctMapFrom.branchID, BranchAcctMapFrom.lineNbr>
  {
    public static BranchAcctMapFrom Find(
      PXGraph graph,
      int? branchID,
      int? lineNbr,
      PKFindOptions options = 0)
    {
      return (BranchAcctMapFrom) PrimaryKeyOf<BranchAcctMapFrom>.By<BranchAcctMapFrom.branchID, BranchAcctMapFrom.lineNbr>.FindBy(graph, (object) branchID, (object) lineNbr, options);
    }
  }

  public static class FK
  {
    public class Branch : 
      PrimaryKeyOf<Branch>.By<Branch.branchID>.ForeignKeyOf<BranchAcctMapFrom>.By<BranchAcctMapFrom.branchID>
    {
    }

    public class FromBranch : 
      PrimaryKeyOf<Branch>.By<Branch.branchID>.ForeignKeyOf<BranchAcctMapFrom>.By<BranchAcctMapFrom.fromBranchID>
    {
    }

    public class ToBranch : 
      PrimaryKeyOf<Branch>.By<Branch.branchID>.ForeignKeyOf<BranchAcctMapFrom>.By<BranchAcctMapFrom.toBranchID>
    {
    }

    public class FromAccount : 
      PrimaryKeyOf<Account>.By<Account.accountCD>.ForeignKeyOf<BranchAcctMapFrom>.By<BranchAcctMapFrom.fromAccountCD>
    {
    }

    public class ToAccount : 
      PrimaryKeyOf<Account>.By<Account.accountCD>.ForeignKeyOf<BranchAcctMapFrom>.By<BranchAcctMapFrom.toAccountCD>
    {
    }

    public class MapAccount : 
      PrimaryKeyOf<Account>.By<Account.accountID>.ForeignKeyOf<BranchAcctMapFrom>.By<BranchAcctMapFrom.mapAccountID>
    {
    }

    public class MapSubaccount : 
      PrimaryKeyOf<Sub>.By<Sub.subID>.ForeignKeyOf<BranchAcctMapFrom>.By<BranchAcctMapFrom.mapSubID>
    {
    }
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  BranchAcctMapFrom.branchID>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  BranchAcctMapFrom.lineNbr>
  {
  }

  public abstract class fromBranchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  BranchAcctMapFrom.fromBranchID>
  {
  }

  public abstract class toBranchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  BranchAcctMapFrom.toBranchID>
  {
  }

  public abstract class fromAccountCD : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    BranchAcctMapFrom.fromAccountCD>
  {
  }

  public abstract class toAccountCD : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    BranchAcctMapFrom.toAccountCD>
  {
  }

  public abstract class mapAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  BranchAcctMapFrom.mapAccountID>
  {
  }

  public abstract class mapSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  BranchAcctMapFrom.mapSubID>
  {
  }
}
