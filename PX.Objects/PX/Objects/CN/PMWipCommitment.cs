// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.PMWipCommitment
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
/// A projection over the <see cref="T:PX.Objects.PM.PMCommitment" /> class. The projection is used in WIP reports.
/// </summary>
[PXCacheName("PM WIP Commitment")]
[PXProjection(typeof (SelectFrom<PMCommitment>))]
public class PMWipCommitment : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  /// <inheritdoc cref="P:PX.Objects.PM.PMCommitment.CommitmentID" />
  [PXDBGuid(false, IsKey = true, BqlField = typeof (PMCommitment.commitmentID))]
  public virtual Guid? CommitmentID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PM.PMCommitment.ProjectID" />
  [PXDBInt(BqlField = typeof (PMCommitment.projectID))]
  public virtual int? ProjectID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PM.PMCommitment.ProjectTaskID" />
  [PXDBInt(BqlField = typeof (PMCommitment.projectTaskID))]
  public virtual int? ProjectTaskID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PM.PMCommitment.CostCodeID" />
  [PXDBInt(BqlField = typeof (PMCommitment.costCodeID))]
  public virtual int? CostCodeID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PM.PMCommitment.OrigAmount" />
  [PXDBDecimal(BqlField = typeof (PMCommitment.origAmount))]
  public virtual Decimal? OrigAmount { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PM.PMCommitment.Amount" />
  [PXDBDecimal(BqlField = typeof (PMCommitment.amount))]
  public virtual Decimal? Amount { get; set; }

  public abstract class commitmentID : BqlType<IBqlGuid, Guid>.Field<
  #nullable disable
  PMWipCommitment.commitmentID>
  {
  }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMWipCommitment.projectID>
  {
  }

  public abstract class projectTaskID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMWipCommitment.projectTaskID>
  {
  }

  public abstract class costCodeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMWipCommitment.costCodeID>
  {
  }

  public abstract class origAmount : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMWipCommitment.origAmount>
  {
  }

  public abstract class amount : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMWipCommitment.amount>
  {
  }
}
