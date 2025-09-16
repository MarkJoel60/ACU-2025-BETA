// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APHistoryAlias
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AR;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.AP;

[PXHidden]
[Serializable]
public class APHistoryAlias : APHistory
{
  public new class PK : 
    PrimaryKeyOf<
    #nullable disable
    APHistoryAlias>.By<APHistoryAlias.branchID, APHistoryAlias.accountID, APHistoryAlias.subID, APHistoryAlias.vendorID, APHistoryAlias.finPeriodID>
  {
    public static APHistoryAlias Find(
      PXGraph graph,
      int? branchID,
      int? accountID,
      int? subID,
      int? vendorID,
      string finPeriodID,
      PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<APHistoryAlias>.By<APHistoryAlias.branchID, APHistoryAlias.accountID, APHistoryAlias.subID, APHistoryAlias.vendorID, APHistoryAlias.finPeriodID>.FindBy(graph, (object) branchID, (object) accountID, (object) subID, (object) vendorID, (object) finPeriodID, options);
    }
  }

  public new static class FK
  {
    public class Branch : 
      PrimaryKeyOf<PX.Objects.GL.Branch>.By<PX.Objects.GL.Branch.branchID>.ForeignKeyOf<APHistoryAlias>.By<APHistoryAlias.branchID>
    {
    }

    public class Account : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<APHistoryAlias>.By<APHistoryAlias.accountID>
    {
    }

    public class Subaccount : 
      PrimaryKeyOf<Sub>.By<Sub.subID>.ForeignKeyOf<APHistoryAlias>.By<APHistoryAlias.subID>
    {
    }

    public class Vendor : 
      PrimaryKeyOf<Customer>.By<Customer.bAccountID>.ForeignKeyOf<APHistoryAlias>.By<APHistoryAlias.vendorID>
    {
    }
  }

  public new abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APHistoryAlias.branchID>
  {
  }

  public new abstract class accountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APHistoryAlias.accountID>
  {
  }

  public new abstract class subID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APHistoryAlias.subID>
  {
  }

  public new abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APHistoryAlias.vendorID>
  {
  }

  public new abstract class finPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APHistoryAlias.finPeriodID>
  {
  }
}
