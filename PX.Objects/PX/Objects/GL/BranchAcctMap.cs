// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.BranchAcctMap
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

/// <summary>The rules for balancing inter-branch transactions.</summary>
[PXCacheName("Branch Account Map")]
[PXHidden]
[Serializable]
public class BranchAcctMap : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBInt(IsKey = true)]
  [PXDBDefault(typeof (Branch.branchID))]
  public virtual int? BranchID { get; set; }

  [PXDBInt(IsKey = true)]
  [PXLineNbr(typeof (Branch.acctMapNbr))]
  public virtual int? LineNbr { get; set; }

  [PXDBInt]
  public virtual int? FromBranchID { get; set; }

  [PXDBInt]
  public virtual int? ToBranchID { get; set; }

  [PXDefault]
  [AccountRaw]
  public virtual 
  #nullable disable
  string FromAccountCD { get; set; }

  [PXDefault]
  [AccountRaw]
  public virtual string ToAccountCD { get; set; }

  [Account(DescriptionField = typeof (Account.description))]
  [PXDefault]
  [PXForeignReference(typeof (Field<BranchAcctMap.mapAccountID>.IsRelatedTo<Account.accountID>))]
  public virtual int? MapAccountID { get; set; }

  [SubAccount(typeof (BranchAcctMap.mapAccountID), DescriptionField = typeof (Account.description))]
  [PXDefault]
  [PXForeignReference(typeof (Field<BranchAcctMap.mapSubID>.IsRelatedTo<Sub.subID>))]
  public virtual int? MapSubID { get; set; }

  public class PK : PrimaryKeyOf<BranchAcctMap>.By<BranchAcctMap.branchID, BranchAcctMap.lineNbr>
  {
    public static BranchAcctMap Find(
      PXGraph graph,
      int? branchID,
      int? lineNbr,
      PKFindOptions options = 0)
    {
      return (BranchAcctMap) PrimaryKeyOf<BranchAcctMap>.By<BranchAcctMap.branchID, BranchAcctMap.lineNbr>.FindBy(graph, (object) branchID, (object) lineNbr, options);
    }
  }

  public static class FK
  {
    public class Branch : 
      PrimaryKeyOf<Branch>.By<Branch.branchID>.ForeignKeyOf<BranchAcctMap>.By<BranchAcctMap.branchID>
    {
    }

    public class FromBranch : 
      PrimaryKeyOf<Branch>.By<Branch.branchID>.ForeignKeyOf<BranchAcctMap>.By<BranchAcctMap.fromBranchID>
    {
    }

    public class ToBranch : 
      PrimaryKeyOf<Branch>.By<Branch.branchID>.ForeignKeyOf<BranchAcctMap>.By<BranchAcctMap.toBranchID>
    {
    }

    public class FromAccount : 
      PrimaryKeyOf<Account>.By<Account.accountCD>.ForeignKeyOf<BranchAcctMap>.By<BranchAcctMap.fromAccountCD>
    {
    }

    public class ToAccount : 
      PrimaryKeyOf<Account>.By<Account.accountCD>.ForeignKeyOf<BranchAcctMap>.By<BranchAcctMap.toAccountCD>
    {
    }

    public class MapAccount : 
      PrimaryKeyOf<Account>.By<Account.accountID>.ForeignKeyOf<BranchAcctMap>.By<BranchAcctMap.mapAccountID>
    {
    }

    public class MapSubaccount : 
      PrimaryKeyOf<Sub>.By<Sub.subID>.ForeignKeyOf<BranchAcctMap>.By<BranchAcctMap.mapSubID>
    {
    }
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  BranchAcctMap.branchID>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  BranchAcctMap.lineNbr>
  {
  }

  public abstract class fromBranchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  BranchAcctMap.fromBranchID>
  {
  }

  public abstract class toBranchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  BranchAcctMap.toBranchID>
  {
  }

  public abstract class fromAccountCD : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    BranchAcctMap.fromAccountCD>
  {
  }

  public abstract class toAccountCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BranchAcctMap.toAccountCD>
  {
  }

  public abstract class mapAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  BranchAcctMap.mapAccountID>
  {
  }

  public abstract class mapSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  BranchAcctMap.mapSubID>
  {
  }
}
