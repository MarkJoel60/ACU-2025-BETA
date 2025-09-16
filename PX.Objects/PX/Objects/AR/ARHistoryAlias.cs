// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARHistoryAlias
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.AR;

[PXHidden]
[Serializable]
public class ARHistoryAlias : ARHistory
{
  public new class PK : 
    PrimaryKeyOf<
    #nullable disable
    ARHistoryAlias>.By<ARHistoryAlias.branchID, ARHistoryAlias.accountID, ARHistoryAlias.subID, ARHistoryAlias.customerID, ARHistoryAlias.finPeriodID>
  {
    public static ARHistoryAlias Find(
      PXGraph graph,
      int? branchID,
      int? accountID,
      int? subID,
      int? customerID,
      string finPeriodID,
      PKFindOptions options = 0)
    {
      return (ARHistoryAlias) PrimaryKeyOf<ARHistoryAlias>.By<ARHistoryAlias.branchID, ARHistoryAlias.accountID, ARHistoryAlias.subID, ARHistoryAlias.customerID, ARHistoryAlias.finPeriodID>.FindBy(graph, (object) branchID, (object) accountID, (object) subID, (object) customerID, (object) finPeriodID, options);
    }
  }

  public new static class FK
  {
    public class Branch : 
      PrimaryKeyOf<PX.Objects.GL.Branch>.By<PX.Objects.GL.Branch.branchID>.ForeignKeyOf<ARHistoryAlias>.By<ARHistoryAlias.branchID>
    {
    }

    public class Account : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<ARHistoryAlias>.By<ARHistoryAlias.accountID>
    {
    }

    public class Subaccount : 
      PrimaryKeyOf<Sub>.By<Sub.subID>.ForeignKeyOf<ARHistoryAlias>.By<ARHistoryAlias.subID>
    {
    }

    public class Customer : 
      PrimaryKeyOf<Customer>.By<Customer.bAccountID>.ForeignKeyOf<ARHistoryAlias>.By<ARHistoryAlias.customerID>
    {
    }
  }

  public new abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARHistoryAlias.branchID>
  {
  }

  public new abstract class accountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARHistoryAlias.accountID>
  {
  }

  public new abstract class subID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARHistoryAlias.subID>
  {
  }

  public new abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARHistoryAlias.customerID>
  {
  }

  public new abstract class finPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARHistoryAlias.finPeriodID>
  {
  }
}
