// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.Alias.CashAccount
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.GL;

#nullable enable
namespace PX.Objects.CA.Alias;

[PXHidden]
public class CashAccount : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBIdentity]
  [PXUIField(Enabled = false)]
  [PXReferentialIntegrityCheck]
  public virtual int? CashAccountID { get; set; }

  [CashAccountRaw]
  [PXDefault]
  public virtual 
  #nullable disable
  string CashAccountCD { get; set; }

  [Branch(null, null, true, true, true)]
  public virtual int? BranchID { get; set; }

  public class PK : PrimaryKeyOf<CashAccount>.By<CashAccount.cashAccountID>
  {
    public static CashAccount Find(PXGraph graph, int? cashAccountID, PKFindOptions options = 0)
    {
      return (CashAccount) PrimaryKeyOf<CashAccount>.By<CashAccount.cashAccountID>.FindBy(graph, (object) cashAccountID, options);
    }
  }

  public static class FK
  {
    public class Branch : 
      PrimaryKeyOf<PX.Objects.GL.Branch>.By<PX.Objects.GL.Branch.branchID>.ForeignKeyOf<CashAccount>.By<CashAccount.branchID>
    {
    }
  }

  public abstract class cashAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CashAccount.cashAccountID>
  {
  }

  public abstract class cashAccountCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CashAccount.cashAccountCD>
  {
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CashAccount.branchID>
  {
  }
}
