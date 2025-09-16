// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMHistoryAccum
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;
using System.Diagnostics.CodeAnalysis;

#nullable enable
namespace PX.Objects.PM;

[PXHidden]
[PMProjectAccum]
[ExcludeFromCodeCoverage]
[Serializable]
public class PMHistoryAccum : PMHistory
{
  /// <inheritdoc cref="P:PX.Objects.PM.PMHistory.CostCodeID" />
  [PXDefault]
  [PXDBInt(IsKey = true)]
  public override int? CostCodeID
  {
    get => this._CostCodeID;
    set => this._CostCodeID = value;
  }

  /// <inheritdoc cref="P:PX.Objects.PM.PMHistory.PeriodID" />
  [PXDBString(6, IsKey = true, IsFixed = true)]
  [PXDefault]
  public override 
  #nullable disable
  string PeriodID
  {
    get => this._PeriodID;
    set => this._PeriodID = value;
  }

  public new abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMHistoryAccum.projectID>
  {
  }

  public new abstract class projectTaskID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMHistoryAccum.projectTaskID>
  {
  }

  public new abstract class accountGroupID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMHistoryAccum.accountGroupID>
  {
  }

  public new abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMHistoryAccum.inventoryID>
  {
  }

  public new abstract class costCodeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMHistoryAccum.costCodeID>
  {
  }

  public new abstract class periodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMHistoryAccum.periodID>
  {
  }
}
