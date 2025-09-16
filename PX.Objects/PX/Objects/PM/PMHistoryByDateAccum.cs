// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMHistoryByDateAccum
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.PM;

/// <exclude />
[PXBreakInheritance]
[PMHistoryByDateAccum]
[PXHidden]
[Serializable]
public class PMHistoryByDateAccum : PMHistoryByDate
{
  [PXDBInt(IsKey = true)]
  public override int? ProjectID { get; set; }

  [PXDBInt(IsKey = true)]
  public override int? ProjectTaskID { get; set; }

  [PXDBInt(IsKey = true)]
  public override int? AccountGroupID { get; set; }

  [PXDBInt(IsKey = true)]
  public override int? InventoryID { get; set; }

  [PXDBInt(IsKey = true)]
  public override int? CostCodeID { get; set; }

  [PXDBDate(IsKey = true)]
  [PXDBDefault]
  public override DateTime? Date { get; set; }

  [FinPeriodID(null, null, null, null, null, null, true, false, null, null, null, true, true, IsKey = true)]
  public override 
  #nullable disable
  string PeriodID { get; set; }

  public new abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMHistoryByDateAccum.projectID>
  {
  }

  public new abstract class projectTaskID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMHistoryByDateAccum.projectTaskID>
  {
  }

  public new abstract class accountGroupID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMHistoryByDateAccum.accountGroupID>
  {
  }

  public new abstract class inventoryID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMHistoryByDateAccum.inventoryID>
  {
  }

  public new abstract class costCodeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMHistoryByDateAccum.costCodeID>
  {
  }

  public new abstract class date : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  PMHistoryByDateAccum.date>
  {
  }

  public new abstract class periodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMHistoryByDateAccum.periodID>
  {
  }
}
