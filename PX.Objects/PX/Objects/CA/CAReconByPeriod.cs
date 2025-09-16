// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CAReconByPeriod
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.GL.FinPeriods;
using System;

#nullable enable
namespace PX.Objects.CA;

[PXProjection(typeof (Select5<CARecon, InnerJoin<CashAccount, On<CashAccount.cashAccountID, Equal<CARecon.cashAccountID>>, InnerJoin<PX.Objects.GL.Branch, On<PX.Objects.GL.Branch.branchID, Equal<CashAccount.branchID>>, InnerJoin<OrganizationFinPeriod, On<OrganizationFinPeriod.endDate, Greater<CARecon.reconDate>, And<OrganizationFinPeriod.organizationID, Equal<PX.Objects.GL.Branch.organizationID>, And<CARecon.reconciled, Equal<boolTrue>, And<CARecon.voided, Equal<boolFalse>>>>>>>>, Aggregate<GroupBy<CARecon.cashAccountID, Max<CARecon.reconDate, GroupBy<OrganizationFinPeriod.finPeriodID>>>>>))]
[PXCacheName("Reconciliation by Period")]
[Serializable]
public class CAReconByPeriod : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [CashAccount(IsKey = true, BqlField = typeof (CARecon.cashAccountID))]
  public virtual int? CashAccountID { get; set; }

  [PXDBDate(BqlField = typeof (CARecon.reconDate))]
  [PXUIField(DisplayName = "Last Reconciliation Date")]
  public virtual DateTime? LastReconDate { get; set; }

  [PX.Objects.GL.FinPeriodID(null, null, null, null, null, null, true, false, null, null, null, true, true, IsKey = true, BqlField = typeof (OrganizationFinPeriod.finPeriodID))]
  public virtual 
  #nullable disable
  string FinPeriodID { get; set; }

  public class PK : 
    PrimaryKeyOf<CAReconByPeriod>.By<CAReconByPeriod.cashAccountID, CAReconByPeriod.finPeriodID>
  {
    public static CAReconByPeriod Find(
      PXGraph graph,
      int? cashAccountID,
      string finPeriodID,
      PKFindOptions options = 0)
    {
      return (CAReconByPeriod) PrimaryKeyOf<CAReconByPeriod>.By<CAReconByPeriod.cashAccountID, CAReconByPeriod.finPeriodID>.FindBy(graph, (object) cashAccountID, (object) finPeriodID, options);
    }
  }

  public static class FK
  {
    public class CashAccount : 
      PrimaryKeyOf<CashAccount>.By<CashAccount.cashAccountID>.ForeignKeyOf<CAReconByPeriod>.By<CAReconByPeriod.cashAccountID>
    {
    }
  }

  public abstract class cashAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CAReconByPeriod.cashAccountID>
  {
  }

  public abstract class lastReconDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CAReconByPeriod.lastReconDate>
  {
  }

  public abstract class finPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CAReconByPeriod.finPeriodID>
  {
  }
}
