// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.BranchAlias
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;

#nullable enable
namespace PX.Objects.GL;

[PXHidden]
public class BranchAlias : Branch
{
  public new class PK : PrimaryKeyOf<
  #nullable disable
  BranchAlias>.By<BranchAlias.branchID>
  {
    public static BranchAlias Find(PXGraph graph, int? branchID, PKFindOptions options = 0)
    {
      return (BranchAlias) PrimaryKeyOf<BranchAlias>.By<BranchAlias.branchID>.FindBy(graph, (object) branchID, options);
    }
  }

  public new class UK : PrimaryKeyOf<BranchAlias>.By<BranchAlias.branchCD>
  {
    public static BranchAlias Find(PXGraph graph, string branchCD, PKFindOptions options = 0)
    {
      return (BranchAlias) PrimaryKeyOf<BranchAlias>.By<BranchAlias.branchCD>.FindBy(graph, (object) branchCD, options);
    }
  }

  public new static class FK
  {
    public class BAccount : 
      PrimaryKeyOf<PX.Objects.CR.BAccount>.By<PX.Objects.CR.BAccount.bAccountID>.ForeignKeyOf<BranchAlias>.By<BranchAlias.bAccountID>
    {
    }

    public class Organization : 
      PrimaryKeyOf<PX.Objects.GL.DAC.Organization>.By<PX.Objects.GL.DAC.Organization.organizationID>.ForeignKeyOf<BranchAlias>.By<BranchAlias.organizationID>
    {
    }

    public class Ledger : 
      PrimaryKeyOf<Ledger>.By<Ledger.ledgerID>.ForeignKeyOf<BranchAlias>.By<BranchAlias.ledgerID>
    {
    }

    public class BaseCurrency : 
      PrimaryKeyOf<PX.Objects.CM.Currency>.By<PX.Objects.CM.Currency.curyID>.ForeignKeyOf<BranchAlias>.By<BranchAlias.baseCuryID>
    {
    }

    public class Country : 
      PrimaryKeyOf<PX.Objects.CS.Country>.By<PX.Objects.CS.Country.countryID>.ForeignKeyOf<BranchAlias>.By<BranchAlias.countryID>
    {
    }
  }

  public new abstract class organizationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  BranchAlias.organizationID>
  {
  }

  public new abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  BranchAlias.branchID>
  {
  }

  public new abstract class branchCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BranchAlias.branchCD>
  {
  }

  public new abstract class roleName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BranchAlias.roleName>
  {
  }

  public new abstract class logoName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BranchAlias.logoName>
  {
  }

  public new abstract class ledgerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  BranchAlias.ledgerID>
  {
  }

  public new abstract class ledgerCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BranchAlias.ledgerCD>
  {
  }

  public new abstract class bAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  BranchAlias.bAccountID>
  {
  }

  public new abstract class active : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  BranchAlias.active>
  {
  }

  public new abstract class phoneMask : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BranchAlias.phoneMask>
  {
  }

  public new abstract class countryID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BranchAlias.countryID>
  {
  }

  public new abstract class groupMask : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  BranchAlias.groupMask>
  {
  }

  public new abstract class acctMapNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  BranchAlias.acctMapNbr>
  {
  }

  public new abstract class acctName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BranchAlias.acctName>
  {
  }

  public new abstract class baseCuryID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BranchAlias.baseCuryID>
  {
  }

  public new abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  BranchAlias.Tstamp>
  {
  }

  public new abstract class included : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  BranchAlias.included>
  {
  }

  public new abstract class parentBranchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  BranchAlias.parentBranchID>
  {
  }
}
