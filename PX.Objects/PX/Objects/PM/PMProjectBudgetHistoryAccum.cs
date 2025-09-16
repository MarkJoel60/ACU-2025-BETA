// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMProjectBudgetHistoryAccum
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.PM;

[PXHidden]
[PMProjectBudgetHistoryAccum]
public class PMProjectBudgetHistoryAccum : PMProjectBudgetHistory
{
  [PXDBDate(IsKey = true)]
  public override DateTime? Date { get; set; }

  [PXDefault]
  [PXDBInt(IsKey = true)]
  public override int? ProjectID { get; set; }

  [PXDefault]
  [PXDBInt(IsKey = true)]
  public override int? TaskID { get; set; }

  [PXDefault]
  [PXDBInt(IsKey = true)]
  public override int? AccountGroupID { get; set; }

  [PXDBInt(IsKey = true)]
  [PXDefault]
  public override int? InventoryID { get; set; }

  [PXDBInt(IsKey = true)]
  [PXDefault]
  public override int? CostCodeID { get; set; }

  [PXDBString(15, IsKey = true)]
  public override string ChangeOrderRefNbr { get; set; }

  public new abstract class date : 
    BqlType<IBqlDateTime, DateTime>.Field<PMProjectBudgetHistoryAccum.date>
  {
  }

  public new abstract class projectID : 
    BqlType<IBqlInt, int>.Field<PMProjectBudgetHistoryAccum.projectID>
  {
  }

  public new abstract class taskID : BqlType<IBqlInt, int>.Field<PMProjectBudgetHistoryAccum.taskID>
  {
  }

  public new abstract class accountGroupID : 
    BqlType<IBqlInt, int>.Field<PMProjectBudgetHistoryAccum.accountGroupID>
  {
  }

  public new abstract class inventoryID : 
    BqlType<IBqlInt, int>.Field<PMProjectBudgetHistoryAccum.inventoryID>
  {
  }

  public new abstract class costCodeID : 
    BqlType<IBqlInt, int>.Field<PMProjectBudgetHistoryAccum.costCodeID>
  {
  }

  public new abstract class changeOrderRefNbr : 
    BqlType<IBqlString, string>.Field<PMProjectBudgetHistoryAccum.changeOrderRefNbr>
  {
  }
}
