// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.APInvoiceEntryExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AP;
using PX.Objects.CM;
using PX.Objects.CS;
using PX.Objects.PO;
using System;
using System.Collections;

#nullable disable
namespace PX.Objects.PM;

/// <summary>
/// Extends AP Invoice Entry with Project related functionality. Requires Project Accounting feature.
/// </summary>
public class APInvoiceEntryExt : PXGraphExtension<APInvoiceEntry>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.projectAccounting>();

  [PXOverride]
  public virtual void CopyCustomizationFieldsToAPTran(
    PX.Objects.AP.APTran apTranToFill,
    IAPTranSource poSourceLine,
    bool areCurrenciesSame)
  {
    if (this.CopyProjectFromLine(poSourceLine))
    {
      apTranToFill.ProjectID = poSourceLine.ProjectID;
      apTranToFill.TaskID = poSourceLine.TaskID;
    }
    else
      apTranToFill.ProjectID = ProjectDefaultAttribute.NonProject();
    apTranToFill.CostCodeID = poSourceLine.CostCodeID;
    if (this.IsPartiallyBilledCompleteByAmountSubcontractLine(apTranToFill, poSourceLine))
    {
      this.RedefaultInvoiceAmountForSubcontract(apTranToFill, poSourceLine, areCurrenciesSame);
    }
    else
    {
      if (!this.IsDebitAdjSubcontractLine(apTranToFill, poSourceLine))
        return;
      this.RedefaultDebitAdjAmountForSubcontract(apTranToFill, poSourceLine, areCurrenciesSame);
    }
  }

  protected virtual bool CopyProjectFromLine(IAPTranSource poSourceLine) => true;

  private bool IsPartiallyBilledCompleteByAmountSubcontractLine(PX.Objects.AP.APTran tran, IAPTranSource line)
  {
    return line.OrderType == "RS" && tran.TranType != "ADR" && line.CompletePOLine == "A" && line.IsPartiallyBilled;
  }

  private void RedefaultInvoiceAmountForSubcontract(
    PX.Objects.AP.APTran tran,
    IAPTranSource line,
    bool areCurrenciesSame)
  {
    tran.CuryRetainageAmt = new Decimal?();
    if (areCurrenciesSame)
    {
      tran.CuryLineAmt = line.UnbilledAmt;
    }
    else
    {
      Decimal curyval;
      PXCurrencyAttribute.PXCurrencyHelper.CuryConvCury(((PXSelectBase) this.Base.Document).Cache, (object) ((PXSelectBase<PX.Objects.AP.APInvoice>) this.Base.Document).Current, line.UnbilledAmt.GetValueOrDefault(), out curyval);
      tran.CuryLineAmt = new Decimal?(curyval);
    }
  }

  private bool IsDebitAdjSubcontractLine(PX.Objects.AP.APTran tran, IAPTranSource line)
  {
    return line.OrderType == "RS" && tran.TranType == "ADR";
  }

  private void RedefaultDebitAdjAmountForSubcontract(
    PX.Objects.AP.APTran tran,
    IAPTranSource line,
    bool areCurrenciesSame)
  {
    tran.Qty = line.BilledQty;
    tran.RetainagePct = new Decimal?(0M);
    tran.RetainageAmt = new Decimal?(0M);
    tran.CuryRetainageAmt = new Decimal?(0M);
    if (areCurrenciesSame)
    {
      tran.CuryLineAmt = line.BilledAmt;
      tran.UnitCost = line.UnitCost;
    }
    else
    {
      Decimal curyval;
      PXCurrencyAttribute.PXCurrencyHelper.CuryConvCury(((PXSelectBase) this.Base.Document).Cache, (object) ((PXSelectBase<PX.Objects.AP.APInvoice>) this.Base.Document).Current, line.BilledAmt.GetValueOrDefault(), out curyval);
      tran.CuryLineAmt = new Decimal?(curyval);
      tran.CuryUnitCost = line.CuryUnitCost;
    }
  }

  [PXOverride]
  public virtual PX.Objects.AP.APTran InsertTranOnAddPOReceiptLine(
    IAPTranSource line,
    PX.Objects.AP.APTran tran,
    Func<IAPTranSource, PX.Objects.AP.APTran, PX.Objects.AP.APTran> baseMethod)
  {
    if (line.OrderType == "RS")
    {
      this.ValidateAndRaiseError(line);
      if (tran.TranType == "ADR")
      {
        tran.RetainagePct = new Decimal?(0M);
        tran.CuryRetainageAmt = new Decimal?(0M);
        tran.RetainageAmt = new Decimal?(0M);
      }
      else
        tran.CuryRetainageAmt = new Decimal?();
      tran.CuryDiscAmt = new Decimal?(0M);
      tran.DiscAmt = new Decimal?(0M);
      tran.DiscPct = new Decimal?(0M);
    }
    return baseMethod(line, tran);
  }

  private void ValidateAndRaiseError(IAPTranSource line)
  {
    if (!line.TaskID.HasValue || !line.ExpenseAcctID.HasValue)
      return;
    PX.Objects.GL.Account account = PX.Objects.GL.Account.PK.Find((PXGraph) this.Base, line.ExpenseAcctID);
    if (account != null && !account.AccountGroupID.HasValue)
      throw new PXSetPropertyException("The account specified in the project-related line must be mapped to an account group. Assign an account group to the {0} account or select the non-project code in the line.", (PXErrorLevel) 4, new object[1]
      {
        (object) account.AccountCD
      });
  }

  [PXOverride]
  public virtual IEnumerable Release(PXAdapter adapter, Func<PXAdapter, IEnumerable> baseHandler)
  {
    this.ValidatePOLines();
    return baseHandler(adapter);
  }

  private void ValidatePOLines()
  {
    bool flag = false;
    foreach (PXResult<PX.Objects.AP.APTran> pxResult in ((PXSelectBase<PX.Objects.AP.APTran>) this.Base.Transactions).Select(Array.Empty<object>()))
    {
      PX.Objects.AP.APTran apTran = PXResult<PX.Objects.AP.APTran>.op_Implicit(pxResult);
      PX.Objects.GL.Account account = PX.Objects.GL.Account.PK.Find((PXGraph) this.Base, apTran.AccountID);
      if ((account != null ? (!account.AccountGroupID.HasValue ? 1 : 0) : 1) != 0 && apTran.LineType == "SV" && ProjectDefaultAttribute.IsProject((PXGraph) this.Base, apTran.ProjectID))
      {
        PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this.Base, apTran.InventoryID);
        if (inventoryItem == null || !(inventoryItem.PostToExpenseAccount == "S"))
        {
          PXTrace.WriteError("The account specified in the project-related line must be mapped to an account group. Assign an account group to the {0} account or select the non-project code in the line.", new object[1]
          {
            (object) account.AccountCD
          });
          flag = true;
        }
      }
    }
    if (flag)
      throw new PXException("One purchase order line or multiple purchase order lines cannot be added to the bill. See Trace Log for details.");
  }

  protected virtual void _(PX.Data.Events.RowSelected<PX.Objects.AP.APTran> e)
  {
    if (e.Row == null || !this.IsItEditableReturnRow(e.Row))
      return;
    this.EnableReturnProperties(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<PX.Objects.AP.APTran>>) e).Cache, e.Row);
  }

  protected virtual bool IsItEditableReturnRow(PX.Objects.AP.APTran tran)
  {
    return this.IsInvoiceEditable() && this.IsItEditableReturnRowOfProjectDropShipPurchaseOrder(tran);
  }

  protected virtual bool IsInvoiceEditable()
  {
    return this.Invoice != null && this.Invoice.Status == "H" && this.Invoice.DocType == "ADR" && !this.Invoice.RetainageApply.GetValueOrDefault();
  }

  protected virtual bool IsItEditableReturnRowOfProjectDropShipPurchaseOrder(PX.Objects.AP.APTran tran)
  {
    return tran.POOrderType == "PD" && tran.PONbr != null && tran.POLineNbr.HasValue && tran.ReceiptNbr == null;
  }

  protected virtual void EnableReturnProperties(PXCache cache, PX.Objects.AP.APTran row)
  {
    PXUIFieldAttribute.SetEnabled<PX.Objects.AP.APTran.qty>(cache, (object) row, true);
    PXUIFieldAttribute.SetEnabled<PX.Objects.AP.APTran.curyUnitCost>(cache, (object) row, true);
    PXUIFieldAttribute.SetEnabled<PX.Objects.AP.APTran.curyLineAmt>(cache, (object) row, true);
  }

  private PX.Objects.AP.APInvoice Invoice => ((PXSelectBase<PX.Objects.AP.APInvoice>) this.Base.Document).Current;
}
