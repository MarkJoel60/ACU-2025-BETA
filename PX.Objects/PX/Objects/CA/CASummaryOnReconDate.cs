// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CASummaryOnReconDate
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.CA;

[PXProjection(typeof (Select5<CARecon, InnerJoin<CADailySummary, On<CARecon.cashAccountID, Equal<CADailySummary.cashAccountID>, And<CARecon.reconDate, GreaterEqual<CADailySummary.tranDate>>>>, Aggregate<GroupBy<CARecon.cashAccountID, GroupBy<CARecon.reconNbr, GroupBy<CARecon.reconDate, Sum<CADailySummary.amtReleasedClearedDr, Sum<CADailySummary.amtReleasedClearedCr, Sum<CADailySummary.amtReleasedUnclearedDr, Sum<CADailySummary.amtReleasedUnclearedCr>>>>>>>>>))]
[PXCacheName("Aggregated CA Daily Summary until Reconciliation Date")]
[Serializable]
public class CASummaryOnReconDate : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  /// <summary>
  /// The <see cref="T:PX.Objects.CA.CashAccount">cash account</see> under reconciliation.
  /// </summary>
  [CashAccount(IsKey = true, BqlField = typeof (CARecon.cashAccountID))]
  public virtual int? CashAccountID { get; set; }

  /// <summary>
  /// The identification number of the reconciliation statement,
  /// which the system assigns when the user saves the statement.
  /// </summary>
  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = ">CCCCCCCCCCCCCCC", BqlField = typeof (CARecon.reconNbr))]
  [PXSelector(typeof (Search<CARecon.reconNbr, Where<CARecon.cashAccountID, Equal<Optional<CARecon.cashAccountID>>>>))]
  public virtual 
  #nullable disable
  string ReconNbr { get; set; }

  /// <summary>
  /// The date when the reconciliation statement was released and closed. A user can change the date up to the release.
  /// </summary>
  [PXDBDate(BqlField = typeof (CARecon.reconDate))]
  [PXUIField(DisplayName = "Reconciliation Date")]
  public virtual DateTime? ReconDate { get; set; }

  [PXDBDecimal(2, BqlField = typeof (CADailySummary.amtReleasedClearedDr))]
  public virtual Decimal? AmtReleasedClearedDr { get; set; }

  [PXDBDecimal(2, BqlField = typeof (CADailySummary.amtReleasedClearedCr))]
  public virtual Decimal? AmtReleasedClearedCr { get; set; }

  [PXDBDecimal(2, BqlField = typeof (CADailySummary.amtReleasedUnclearedDr))]
  public virtual Decimal? AmtReleasedUnclearedDr { get; set; }

  [PXDBDecimal(2, BqlField = typeof (CADailySummary.amtReleasedUnclearedCr))]
  public virtual Decimal? AmtReleasedUnclearedCr { get; set; }

  /// <summary>
  /// The <see cref="T:PX.Objects.CA.CashAccount">cash account</see> under reconciliation.
  /// </summary>
  public abstract class cashAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CASummaryOnReconDate.cashAccountID>
  {
  }

  /// <summary>
  /// The identification number of the reconciliation statement,
  /// which the system assigns when the user saves the statement.
  /// </summary>
  public abstract class reconNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CASummaryOnReconDate.reconNbr>
  {
  }

  /// <summary>
  /// The date when the reconciliation statement was released and closed. A user can change the date up to the release.
  /// </summary>
  public abstract class reconDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CASummaryOnReconDate.reconDate>
  {
  }

  public abstract class amtReleasedClearedDr : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CASummaryOnReconDate.amtReleasedClearedDr>
  {
  }

  public abstract class amtReleasedClearedCr : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CASummaryOnReconDate.amtReleasedClearedCr>
  {
  }

  public abstract class amtReleasedUnclearedDr : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CASummaryOnReconDate.amtReleasedUnclearedDr>
  {
  }

  public abstract class amtReleasedUnclearedCr : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CASummaryOnReconDate.amtReleasedUnclearedCr>
  {
  }
}
