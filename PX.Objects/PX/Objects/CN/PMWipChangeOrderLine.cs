// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.PMWipChangeOrderLine
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.PM;
using System;

#nullable enable
namespace PX.Objects.CN;

/// <summary>
/// A projection over the <see cref="T:PX.Objects.PM.PMChangeOrderLine" /> class joined with the <see cref="T:PX.Objects.PM.PMChangeOrder" /> class.
/// The projection is used in WIP reports.
/// </summary>
[PXCacheName("PM WIP Change Order Line")]
[PXProjection(typeof (SelectFrom<PMChangeOrderLine, TypeArrayOf<IFbqlJoin>.Empty>.InnerJoin<PMChangeOrder>.On<PMChangeOrderLine.FK.ChangeOrder>))]
public class PMWipChangeOrderLine : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  /// <inheritdoc cref="P:PX.Objects.PM.PMChangeOrder.RefNbr" />
  [PXDBString(15, IsKey = true, BqlField = typeof (PMChangeOrder.refNbr))]
  public virtual 
  #nullable disable
  string RefNbr { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PM.PMChangeOrderLine.LineNbr" />
  [PXDBInt(IsKey = true, BqlField = typeof (PMChangeOrderLine.lineNbr))]
  public virtual int? LineNbr { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PM.PMChangeOrder.Status" />
  [PXDBString(BqlField = typeof (PMChangeOrder.status))]
  public virtual string Status { get; set; }

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

  /// <inheritdoc cref="P:PX.Objects.PM.PMChangeOrder.ProjectID" />
  [PXDBInt(BqlField = typeof (PMChangeOrder.projectID))]
  public virtual int? ProjectID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PM.PMChangeOrderLine.TaskID" />
  [PXDBInt(BqlField = typeof (PMChangeOrderLine.taskID))]
  public virtual int? TaskID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PM.PMChangeOrderLine.CostCodeID" />
  [PXDBInt(BqlField = typeof (PMChangeOrderLine.costCodeID))]
  public virtual int? CostCodeID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PM.PMChangeOrderLine.AmountInProjectCury" />
  [PXDBDecimal(BqlField = typeof (PMChangeOrderLine.amountInProjectCury))]
  public virtual Decimal? AmountInProjectCury { get; set; }

  public abstract class refNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMWipChangeOrderLine.refNbr>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMWipChangeOrderLine.lineNbr>
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMWipChangeOrderLine.status>
  {
  }

  public abstract class approved : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMWipChangeOrderLine.approved>
  {
  }

  public abstract class released : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMWipChangeOrderLine.released>
  {
  }

  public abstract class date : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  PMWipChangeOrderLine.date>
  {
  }

  public abstract class completionDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PMWipChangeOrderLine.completionDate>
  {
  }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMWipChangeOrderLine.projectID>
  {
  }

  public abstract class taskID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMWipChangeOrderLine.taskID>
  {
  }

  public abstract class costCodeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMWipChangeOrderLine.costCodeID>
  {
  }

  public abstract class amountInProjectCury : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMWipChangeOrderLine.amountInProjectCury>
  {
  }
}
