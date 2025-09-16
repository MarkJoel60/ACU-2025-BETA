// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.GraphExtensions.Abstract.Mapping.InvoiceTranMapping
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.Common.GraphExtensions.Abstract.DAC;
using System;

#nullable disable
namespace PX.Objects.Common.GraphExtensions.Abstract.Mapping;

public class InvoiceTranMapping : IBqlMapping
{
  /// <exclude />
  protected Type _table;
  public Type BranchID = typeof (InvoiceTran.branchID);
  public Type TranDate = typeof (InvoiceTran.tranDate);
  public Type FinPeriodID = typeof (InvoiceTran.finPeriodID);
  public Type TranPeriodID = typeof (InvoiceTran.tranPeriodID);
  public Type LineNbr = typeof (InvoiceTran.lineNbr);
  public Type TaxCategoryID = typeof (InvoiceTran.taxCategoryID);
  public Type InventoryID = typeof (InvoiceTran.inventoryID);
  public Type TranDesc = typeof (InvoiceTran.tranDesc);
  public Type ManualPrice = typeof (InvoiceTran.manualPrice);
  public Type CuryUnitCost = typeof (InvoiceTran.curyUnitCost);
  public Type Qty = typeof (InvoiceTran.qty);
  public Type UOM = typeof (InvoiceTran.uOM);
  public Type NonBillable = typeof (InvoiceTran.nonBillable);
  public Type Date = typeof (InvoiceTran.date);
  public Type ProjectID = typeof (InvoiceTran.projectID);
  public Type TaskID = typeof (InvoiceTran.taskID);
  public Type CostCodeID = typeof (InvoiceTran.costCodeID);
  public Type AccountID = typeof (InvoiceTran.accountID);
  public Type SubID = typeof (InvoiceTran.subID);
  public Type CuryLineAmt = typeof (InvoiceTran.curyLineAmt);
  public Type CuryTaxAmt = typeof (InvoiceTran.curyTaxAmt);
  public Type CuryTaxableAmt = typeof (InvoiceTran.curyTaxableAmt);
  public Type CuryTranAmt = typeof (InvoiceTran.curyTranAmt);
  public Type TaxableAmt = typeof (InvoiceTran.taxableAmt);
  public Type TranAmt = typeof (InvoiceTran.tranAmt);

  /// <exclude />
  public Type Extension => typeof (InvoiceTran);

  /// <exclude />
  public Type Table => this._table;

  public InvoiceTranMapping(Type table) => this._table = table;
}
