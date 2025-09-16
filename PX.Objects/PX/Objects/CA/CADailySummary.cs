// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CADailySummary
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
namespace PX.Objects.CA;

[CADailySummaryAccumulator]
[PXPrimaryGraph(typeof (CATranEnq), Filter = typeof (CAEnqFilter))]
[PXCacheName("CA Daily Summary")]
[Serializable]
public class CADailySummary : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDefault]
  [CashAccount(true, null, null, DisplayName = "Cash Account", IsKey = true)]
  public virtual int? CashAccountID { get; set; }

  [PXDBDate(IsKey = true)]
  [PXDefault]
  public virtual DateTime? TranDate { get; set; }

  [PXDBDecimal(2)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? AmtReleasedClearedDr { get; set; }

  [PXDBDecimal(2)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? AmtUnreleasedClearedDr { get; set; }

  [PXDBDecimal(2)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? AmtReleasedUnclearedDr { get; set; }

  [PXDBDecimal(2)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? AmtUnreleasedUnclearedDr { get; set; }

  [PXDBDecimal(2)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? AmtReleasedClearedCr { get; set; }

  [PXDBDecimal(2)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? AmtUnreleasedClearedCr { get; set; }

  [PXDBDecimal(2)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? AmtReleasedUnclearedCr { get; set; }

  [PXDBDecimal(2)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? AmtUnreleasedUnclearedCr { get; set; }

  public class PK : 
    PrimaryKeyOf<
    #nullable disable
    CADailySummary>.By<CADailySummary.cashAccountID, CADailySummary.tranDate>
  {
    public static CADailySummary Find(
      PXGraph graph,
      int? cashAccountID,
      DateTime? tranDate,
      PKFindOptions options = 0)
    {
      return (CADailySummary) PrimaryKeyOf<CADailySummary>.By<CADailySummary.cashAccountID, CADailySummary.tranDate>.FindBy(graph, (object) cashAccountID, (object) tranDate, options);
    }
  }

  public static class FK
  {
    public class CashAccount : 
      PrimaryKeyOf<CashAccount>.By<CashAccount.cashAccountID>.ForeignKeyOf<CADailySummary>.By<CADailySummary.cashAccountID>
    {
    }
  }

  public abstract class cashAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CADailySummary.cashAccountID>
  {
  }

  public abstract class tranDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  CADailySummary.tranDate>
  {
  }

  public abstract class amtReleasedClearedDr : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CADailySummary.amtReleasedClearedDr>
  {
  }

  public abstract class amtUnreleasedClearedDr : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CADailySummary.amtUnreleasedClearedDr>
  {
  }

  public abstract class amtReleasedUnclearedDr : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CADailySummary.amtReleasedUnclearedDr>
  {
  }

  public abstract class amtUnreleasedUnclearedDr : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CADailySummary.amtUnreleasedUnclearedDr>
  {
  }

  public abstract class amtReleasedClearedCr : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CADailySummary.amtReleasedClearedCr>
  {
  }

  public abstract class amtUnreleasedClearedCr : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CADailySummary.amtUnreleasedClearedCr>
  {
  }

  public abstract class amtReleasedUnclearedCr : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CADailySummary.amtReleasedUnclearedCr>
  {
  }

  public abstract class amtUnreleasedUnclearedCr : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    CADailySummary.amtUnreleasedUnclearedCr>
  {
  }
}
