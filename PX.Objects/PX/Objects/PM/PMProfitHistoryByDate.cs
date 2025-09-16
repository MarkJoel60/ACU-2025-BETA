// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMProfitHistoryByDate
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CS;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.PM;

/// <summary>
/// A projection DAC with formulas for profit calculation.
/// </summary>
[PXCacheName("PMProfitHistoryByDate")]
[PXProjection(typeof (SelectFrom<PMHistoryByDate, TypeArrayOf<IFbqlJoin>.Empty>.InnerJoin<PMAccountGroup>.On<BqlOperand<PMHistoryByDate.accountGroupID, IBqlInt>.IsEqual<PMAccountGroup.groupID>>))]
[Serializable]
public class PMProfitHistoryByDate : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  /// <inheritdoc cref="P:PX.Objects.PM.PMHistoryByDate.ProjectID" />
  [PXDBInt(IsKey = true, BqlField = typeof (PMHistoryByDate.projectID))]
  public virtual int? ProjectID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PM.PMHistoryByDate.ProjectTaskID" />
  [PXDBInt(IsKey = true, BqlField = typeof (PMHistoryByDate.projectTaskID))]
  public virtual int? TaskID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PM.PMHistoryByDate.AccountGroupID" />
  [PXDBInt(IsKey = true, BqlField = typeof (PMHistoryByDate.accountGroupID))]
  public virtual int? AccountGroupID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PM.PMAccountGroup.Type" />
  [PXDBString(BqlField = typeof (PMAccountGroup.type))]
  public virtual 
  #nullable disable
  string AccountGroupType { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PM.PMAccountGroup.ReportGroup" />
  [PXDBString(BqlField = typeof (PMAccountGroup.reportGroup))]
  public virtual string ReportGroup { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PM.PMHistoryByDate.CuryActualAmount" />
  [PXDBDecimal(BqlField = typeof (PMHistoryByDate.curyActualAmount))]
  public virtual Decimal? CuryActualAmount { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PM.PMHistoryByDate.Date" />
  [PXDBDate(BqlField = typeof (PMHistoryByDate.date))]
  public virtual DateTime? Date { get; set; }

  /// <summary>The contract to date.</summary>
  [PXDecimal]
  [PXDBCalced(typeof (BqlOperand<PMHistoryByDate.curyActualAmount, IBqlDecimal>.When<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMAccountGroup.type, Equal<AccountType.income>>>>>.Or<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMAccountGroup.reportGroup, Equal<PX.Objects.PM.ReportGroup.revenue>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMAccountGroup.type, Equal<AccountType.liability>>>>>.Or<BqlOperand<PMAccountGroup.type, IBqlString>.IsEqual<AccountType.asset>>>>>.Else<decimal0>), typeof (Decimal))]
  [PXUIField(DisplayName = "Billed Amount to Date")]
  public virtual Decimal? CuryContractToDate { get; set; }

  /// <summary>The cost to date.</summary>
  [PXDecimal]
  [PXDBCalced(typeof (BqlOperand<PMHistoryByDate.curyActualAmount, IBqlDecimal>.When<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMAccountGroup.type, Equal<AccountType.expense>>>>>.Or<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMAccountGroup.reportGroup, NotEqual<PX.Objects.PM.ReportGroup.revenue>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMAccountGroup.type, Equal<AccountType.liability>>>>>.Or<BqlOperand<PMAccountGroup.type, IBqlString>.IsEqual<AccountType.asset>>>>>.Else<decimal0>), typeof (Decimal))]
  [PXUIField(DisplayName = "Actual Costs to Date")]
  public virtual Decimal? CuryCostToDate { get; set; }

  /// <summary>The profit to date.</summary>
  /// <value>
  /// The value is calculated by the formula: <see cref="P:PX.Objects.PM.PMProfitHistoryByDate.CuryContractToDate" /> - <see cref="P:PX.Objects.PM.PMProfitHistoryByDate.CuryCostToDate" />.
  /// </value>
  [PXDecimal]
  [PXDBCalced(typeof (Sub<PMProfitHistoryByDate.curyContractToDate, PMProfitHistoryByDate.curyCostToDate>), typeof (Decimal))]
  [PXUIField(DisplayName = "Profit to Date")]
  public virtual Decimal? CuryProfitToDate { get; set; }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMProfitHistoryByDate.projectID>
  {
  }

  public abstract class taskID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMProfitHistoryByDate.taskID>
  {
  }

  public abstract class accountGroupID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMProfitHistoryByDate.accountGroupID>
  {
  }

  public abstract class accountGroupType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMProfitHistoryByDate.accountGroupType>
  {
  }

  public abstract class reportGroup : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMProfitHistoryByDate.reportGroup>
  {
  }

  public abstract class curyActualAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProfitHistoryByDate.curyActualAmount>
  {
  }

  public abstract class date : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  PMProfitHistoryByDate.date>
  {
  }

  public abstract class curyContractToDate : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProfitHistoryByDate.curyContractToDate>
  {
  }

  public abstract class curyCostToDate : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProfitHistoryByDate.curyCostToDate>
  {
  }

  public abstract class curyProfitToDate : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMProfitHistoryByDate.curyProfitToDate>
  {
  }
}
