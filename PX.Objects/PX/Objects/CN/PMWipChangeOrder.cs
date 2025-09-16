// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.PMWipChangeOrder
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.PM;
using System;

#nullable enable
namespace PX.Objects.CN;

/// <summary>
/// A projection over the <see cref="T:PX.Objects.PM.PMChangeOrder" /> class. The projection is used in WIP reports.
/// </summary>
[PXCacheName("PM WIP Change Order")]
[PXProjection(typeof (SelectFrom<PMChangeOrder>))]
public class PMWipChangeOrder : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  /// <inheritdoc cref="P:PX.Objects.PM.PMChangeOrder.RefNbr" />
  [PXDBString(15, IsKey = true, BqlField = typeof (PMChangeOrder.refNbr))]
  public virtual 
  #nullable disable
  string RefNbr { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PM.PMChangeOrder.ProjectID" />
  [PXDBInt(BqlField = typeof (PMChangeOrder.projectID))]
  public virtual int? ProjectID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PM.PMChangeOrder.Approved" />
  [PXDBBool(BqlField = typeof (PMChangeOrder.approved))]
  public virtual bool? Approved { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PM.PMChangeOrder.Released" />
  [PXDBBool(BqlField = typeof (PMChangeOrder.released))]
  public virtual bool? Released { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PM.PMChangeOrder.Date" />
  [PXDBDate(BqlField = typeof (PMChangeOrder.date))]
  public virtual DateTime? Date { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PM.PMChangeOrder.CompletionDate" />
  [PXDBDate(BqlField = typeof (PMChangeOrder.completionDate))]
  public virtual DateTime? CompletionDate { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PM.PMChangeOrder.CostTotal" />
  [PXDBDecimal(BqlField = typeof (PMChangeOrder.costTotal))]
  public virtual Decimal? CostTotal { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PM.PMChangeOrder.RevenueTotal" />
  [PXDBDecimal(BqlField = typeof (PMChangeOrder.revenueTotal))]
  public virtual Decimal? RevenueTotal { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PM.PMChangeOrder.CommitmentTotal" />
  [PXDBDecimal(BqlField = typeof (PMChangeOrder.commitmentTotal))]
  public virtual Decimal? CommitmentTotal { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PM.PMChangeOrder.Status" />
  [PXDBString(BqlField = typeof (PMChangeOrder.status))]
  public virtual string Status { get; set; }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMWipChangeOrder.refNbr>
  {
  }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMWipChangeOrder.projectID>
  {
  }

  public abstract class approved : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMWipChangeOrder.approved>
  {
  }

  public abstract class released : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMWipChangeOrder.released>
  {
  }

  public abstract class date : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  PMWipChangeOrder.date>
  {
  }

  public abstract class completionDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PMWipChangeOrder.completionDate>
  {
  }

  public abstract class costTotal : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMWipChangeOrder.costTotal>
  {
  }

  public abstract class revenueTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMWipChangeOrder.revenueTotal>
  {
  }

  public abstract class commitmentTotal : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMWipChangeOrder.commitmentTotal>
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMWipChangeOrder.status>
  {
  }
}
