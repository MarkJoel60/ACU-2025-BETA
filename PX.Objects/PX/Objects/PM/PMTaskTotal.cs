// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMTaskTotal
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CM.Extensions;
using System;
using System.Diagnostics.CodeAnalysis;

#nullable enable
namespace PX.Objects.PM;

[PXCacheName("Task Total")]
[TaskTotalAccum]
[ExcludeFromCodeCoverage]
[Serializable]
public class PMTaskTotal : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _ProjectID;
  protected int? _TaskID;
  protected 
  #nullable disable
  byte[] _tstamp;

  [PXDBInt(IsKey = true)]
  public virtual int? ProjectID
  {
    get => this._ProjectID;
    set => this._ProjectID = value;
  }

  [PXDBInt(IsKey = true)]
  public virtual int? TaskID
  {
    get => this._TaskID;
    set => this._TaskID = value;
  }

  [PXDBCurrency(typeof (PMProject.curyInfoID), typeof (PMTaskTotal.asset), BaseCalc = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Asset", Enabled = false)]
  public virtual Decimal? CuryAsset { get; set; }

  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? Asset { get; set; }

  [PXDBCurrency(typeof (PMProject.curyInfoID), typeof (PMTaskTotal.liability), BaseCalc = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Liability", Enabled = false)]
  public virtual Decimal? CuryLiability { get; set; }

  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? Liability { get; set; }

  [PXDBCurrency(typeof (PMProject.curyInfoID), typeof (PMTaskTotal.income), BaseCalc = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Income", Enabled = false)]
  public virtual Decimal? CuryIncome { get; set; }

  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? Income { get; set; }

  [PXDBCurrency(typeof (PMProject.curyInfoID), typeof (PMTaskTotal.expense), BaseCalc = false)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Expense", Enabled = false)]
  public virtual Decimal? CuryExpense { get; set; }

  [PXDBBaseCury]
  [PXDefault(TypeCode.Decimal, "0.0")]
  public virtual Decimal? Expense { get; set; }

  /// <summary>Margin in Project Currency</summary>
  [PXCurrency(typeof (PMProject.curyInfoID), typeof (PMTaskTotal.margin))]
  [PXUIField(DisplayName = "Margin Amount", Enabled = false)]
  [PXFormula(typeof (Sub<PMTaskTotal.curyIncome, PMTaskTotal.curyExpense>))]
  public virtual Decimal? CuryMargin { get; set; }

  /// <summary>Margin in Base Currency</summary>
  [PXBaseCury]
  public virtual Decimal? Margin { get; set; }

  /// <summary>Margin in percents</summary>
  [PXDecimal]
  [PXUIField(DisplayName = "Margin (%)", Enabled = false)]
  public virtual Decimal? MarginPct
  {
    [PXDependsOnFields(new Type[] {typeof (PMTaskTotal.curyIncome), typeof (PMTaskTotal.curyExpense)})] get
    {
      Decimal? curyIncome1 = this.CuryIncome;
      Decimal num1 = 0M;
      if (curyIncome1.GetValueOrDefault() == num1 & curyIncome1.HasValue)
        return new Decimal?(0M);
      Decimal num2 = (Decimal) 100;
      Decimal? curyIncome2 = this.CuryIncome;
      Decimal? nullable1 = this.CuryExpense;
      Decimal? nullable2 = curyIncome2.HasValue & nullable1.HasValue ? new Decimal?(curyIncome2.GetValueOrDefault() - nullable1.GetValueOrDefault()) : new Decimal?();
      Decimal? nullable3;
      if (!nullable2.HasValue)
      {
        nullable1 = new Decimal?();
        nullable3 = nullable1;
      }
      else
        nullable3 = new Decimal?(num2 * nullable2.GetValueOrDefault());
      Decimal? nullable4 = nullable3;
      Decimal? curyIncome3 = this.CuryIncome;
      return !(nullable4.HasValue & curyIncome3.HasValue) ? new Decimal?() : new Decimal?(nullable4.GetValueOrDefault() / curyIncome3.GetValueOrDefault());
    }
  }

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMTaskTotal.projectID>
  {
  }

  public abstract class taskID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMTaskTotal.taskID>
  {
  }

  public abstract class curyAsset : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMTaskTotal.curyAsset>
  {
  }

  public abstract class asset : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMTaskTotal.asset>
  {
  }

  public abstract class curyLiability : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMTaskTotal.curyLiability>
  {
  }

  public abstract class liability : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMTaskTotal.liability>
  {
  }

  public abstract class curyIncome : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMTaskTotal.curyIncome>
  {
  }

  public abstract class income : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMTaskTotal.income>
  {
  }

  public abstract class curyExpense : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMTaskTotal.curyExpense>
  {
  }

  public abstract class expense : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMTaskTotal.expense>
  {
  }

  public abstract class curyMargin : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMTaskTotal.curyMargin>
  {
  }

  public abstract class margin : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMTaskTotal.margin>
  {
  }

  public abstract class marginPct : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMTaskTotal.marginPct>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  PMTaskTotal.Tstamp>
  {
  }
}
