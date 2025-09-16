// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMHistoryByPeriod
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.GL;
using PX.Objects.GL.FinPeriods;
using System;
using System.Diagnostics.CodeAnalysis;

#nullable enable
namespace PX.Objects.PM;

[PXProjection(typeof (Select5<PMHistory, InnerJoin<MasterFinPeriod, On<MasterFinPeriod.finPeriodID, GreaterEqual<PMHistory.periodID>>>, Aggregate<GroupBy<PMHistory.projectID, GroupBy<PMHistory.projectTaskID, GroupBy<PMHistory.accountGroupID, GroupBy<PMHistory.inventoryID, GroupBy<PMHistory.costCodeID, Max<PMHistory.periodID, GroupBy<MasterFinPeriod.finPeriodID>>>>>>>>>))]
[PXHidden]
[ExcludeFromCodeCoverage]
[Serializable]
public class PMHistoryByPeriod : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _ProjectID;
  protected int? _ProjectTaskID;
  protected int? _AccountGroupID;
  protected int? _InventoryID;
  protected int? _CostCodeID;
  protected 
  #nullable disable
  string _PeriodID;

  [PXDBInt(IsKey = true, BqlField = typeof (PMHistory.projectID))]
  [PXDefault]
  public virtual int? ProjectID
  {
    get => this._ProjectID;
    set => this._ProjectID = value;
  }

  [PXDBInt(IsKey = true, BqlField = typeof (PMHistory.projectTaskID))]
  [PXDefault]
  public virtual int? ProjectTaskID
  {
    get => this._ProjectTaskID;
    set => this._ProjectTaskID = value;
  }

  [PXDBInt(IsKey = true, BqlField = typeof (PMHistory.accountGroupID))]
  [PXDefault]
  public virtual int? AccountGroupID
  {
    get => this._AccountGroupID;
    set => this._AccountGroupID = value;
  }

  [PXDBInt(IsKey = true, BqlField = typeof (PMHistory.inventoryID))]
  [PXDefault]
  public virtual int? InventoryID
  {
    get => this._InventoryID;
    set => this._InventoryID = value;
  }

  [PXDBInt(IsKey = true, BqlField = typeof (PMHistory.costCodeID))]
  [PXDefault]
  public virtual int? CostCodeID
  {
    get => this._CostCodeID;
    set => this._CostCodeID = value;
  }

  [FinPeriodID(null, null, null, null, null, null, true, false, null, null, null, true, true, IsKey = true, BqlField = typeof (PMHistory.projectID))]
  [PXDefault]
  [PXUIField]
  [PXSelector(typeof (MasterFinPeriod.finPeriodID), DescriptionField = typeof (MasterFinPeriod.descr))]
  public virtual string PeriodID
  {
    get => this._PeriodID;
    set => this._PeriodID = value;
  }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMHistoryByPeriod.projectID>
  {
  }

  public abstract class projectTaskID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMHistoryByPeriod.projectTaskID>
  {
  }

  public abstract class accountGroupID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMHistoryByPeriod.accountGroupID>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMHistoryByPeriod.inventoryID>
  {
  }

  public abstract class costCodeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMHistoryByPeriod.costCodeID>
  {
  }

  public abstract class periodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMHistoryByPeriod.periodID>
  {
  }
}
