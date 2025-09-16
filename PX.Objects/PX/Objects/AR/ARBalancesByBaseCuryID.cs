// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARBalancesByBaseCuryID
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CM;
using System;

#nullable enable
namespace PX.Objects.AR;

[PXCacheName("AR Balance by Base Currency")]
[PXProjection(typeof (Select5<ARBalances, InnerJoin<PX.Objects.GL.Branch, On<PX.Objects.GL.Branch.branchID, Equal<ARBalances.branchID>>>, Aggregate<GroupBy<ARBalances.customerID, GroupBy<PX.Objects.GL.Branch.baseCuryID, Sum<ARBalances.currentBal, Sum<ARBalances.totalPrepayments, Sum<ARBalances.unreleasedBal>>>>>>>))]
[Serializable]
public class ARBalancesByBaseCuryID : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBInt(IsKey = true, BqlTable = typeof (ARBalances))]
  public virtual int? CustomerID { get; set; }

  [PXDBString(5, IsKey = true, IsUnicode = true, BqlTable = typeof (PX.Objects.GL.Branch))]
  [PXUIField(DisplayName = "Currency")]
  public virtual 
  #nullable disable
  string BaseCuryID { get; set; }

  [PXDBCury(typeof (ARBalancesByBaseCuryID.baseCuryID), BqlTable = typeof (ARBalances))]
  [PXUIField(DisplayName = "Balance")]
  public virtual Decimal? CurrentBal { get; set; }

  [PXDBCury(typeof (ARBalancesByBaseCuryID.baseCuryID), BqlTable = typeof (ARBalances))]
  [PXUIField(DisplayName = "Prepayment Balance")]
  public virtual Decimal? TotalPrepayments { get; set; }

  [PXDBCury(typeof (ARBalancesByBaseCuryID.baseCuryID), BqlTable = typeof (ARBalances))]
  [PXUIField(DisplayName = "Unreleased Balance")]
  public virtual Decimal? UnreleasedBal { get; set; }

  [PXDBCury(typeof (ARBalancesByBaseCuryID.baseCuryID), BqlTable = typeof (ARBalances))]
  [PXUIField(DisplayName = "Consolidated Balance")]
  public virtual Decimal? ConsolidatedBalance { get; set; }

  [PXDBCury(typeof (ARBalancesByBaseCuryID.baseCuryID), BqlTable = typeof (ARBalances))]
  [PXUIField]
  public virtual Decimal? RetainageBalance { get; set; }

  public abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARBalancesByBaseCuryID.customerID>
  {
  }

  public abstract class baseCuryID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARBalancesByBaseCuryID.baseCuryID>
  {
  }

  public abstract class currentBal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARBalancesByBaseCuryID.currentBal>
  {
  }

  public abstract class totalPrepayments : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARBalancesByBaseCuryID.totalPrepayments>
  {
  }

  public abstract class unreleasedBal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARBalancesByBaseCuryID.unreleasedBal>
  {
  }

  public abstract class consolidatedBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARBalancesByBaseCuryID.consolidatedBalance>
  {
  }

  public abstract class retainageBalance : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARBalancesByBaseCuryID.retainageBalance>
  {
  }
}
