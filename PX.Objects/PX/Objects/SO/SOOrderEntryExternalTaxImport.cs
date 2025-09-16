// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOOrderEntryExternalTaxImport
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.MassProcess;
using PX.Objects.Common.Extensions;
using PX.Objects.TX;
using PX.TaxProvider;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.SO;

public class SOOrderEntryExternalTaxImport : PXGraphExtension<SOOrderEntryExternalTax, SOOrderEntry>
{
  [PXVirtualDAC]
  public PXFilter<SOTaxTranImported> ImportedTaxes;

  public virtual void Initialize()
  {
    ((PXGraphExtension) this).Initialize();
    typeof (FieldValue).GetCustomAttributes(typeof (PXVirtualAttribute), false);
  }

  protected virtual void _(Events.RowSelected<SOOrder> e, PXRowSelected baseMethod)
  {
    baseMethod.Invoke(((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<SOOrder>>) e).Cache, ((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<SOOrder>>) e).Args);
    if (e.Row == null || !this.ExternalTaxImportAllowed(e.Row))
      return;
    ((PXSelectBase) ((PXGraphExtension<SOOrderEntry>) this).Base.Taxes).Cache.AllowInsert = true;
    ((PXSelectBase) ((PXGraphExtension<SOOrderEntry>) this).Base.Taxes).Cache.AllowUpdate = true;
  }

  protected virtual void _(Events.RowInserting<SOTaxTran> e)
  {
    if (e.Row == null)
      return;
    SOTaxTran row = e.Row;
    SOOrder current1 = ((PXSelectBase<SOOrder>) ((PXGraphExtension<SOOrderEntry>) this).Base.Document).Current;
    if (!e.ExternalCall || current1 == null || !current1.ExternalTaxesImportInProgress.GetValueOrDefault() || !((Events.Event<PXRowInsertingEventArgs, Events.RowInserting<SOTaxTran>>) e).Cache.Graph.IsContractBasedAPI)
      return;
    SOTaxTranImported instance = (SOTaxTranImported) ((PXSelectBase) this.ImportedTaxes).Cache.CreateInstance();
    ((PXSelectBase) ((PXGraphExtension<SOOrderEntry>) this).Base.Taxes).Cache.RestoreCopy((object) instance, (object) row);
    instance.RecordID = new int?();
    ((PXSelectBase<SOTaxTranImported>) this.ImportedTaxes).Insert(instance);
    foreach (PXResult<SOTaxTran> pxResult in ((PXSelectBase<SOTaxTran>) ((PXGraphExtension<SOOrderEntry>) this).Base.Taxes).Select(Array.Empty<object>()))
    {
      SOTaxTran soTaxTran = PXResult<SOTaxTran>.op_Implicit(pxResult);
      if ((((PXSelectBase) ((PXGraphExtension<SOOrderEntry>) this).Base.Taxes).Cache.GetStatus((object) soTaxTran) == null || ((PXSelectBase) ((PXGraphExtension<SOOrderEntry>) this).Base.Taxes).Cache.GetStatus((object) soTaxTran) == 1) && string.Equals(soTaxTran.TaxID, row.TaxID, StringComparison.OrdinalIgnoreCase))
        ((PXSelectBase<SOTaxTran>) ((PXGraphExtension<SOOrderEntry>) this).Base.Taxes).Delete(soTaxTran);
    }
    PX.Objects.TX.TaxZone current2 = ((PXSelectBase<PX.Objects.TX.TaxZone>) ((PXGraphExtension<SOOrderEntry>) this).Base.taxzone).Current;
    if ((current2 != null ? (!current2.IsExternal.GetValueOrDefault() ? 1 : 0) : 1) == 0)
      return;
    foreach (TaxDetail taxDetail in ((PXSelectBase) ((PXGraphExtension<SOOrderEntry>) this).Base.Taxes).Cache.Inserted)
    {
      if (string.Equals(taxDetail.TaxID, row.TaxID, StringComparison.OrdinalIgnoreCase))
      {
        e.Cancel = true;
        break;
      }
    }
  }

  protected virtual void _(
    Events.FieldVerifying<SOTaxTran, SOTaxTran.taxID> e)
  {
    if (e.Row == null)
      return;
    SOOrder current1 = ((PXSelectBase<SOOrder>) ((PXGraphExtension<SOOrderEntry>) this).Base.Document).Current;
    PX.Objects.TX.TaxZone current2 = ((PXSelectBase<PX.Objects.TX.TaxZone>) ((PXGraphExtension<SOOrderEntry>) this).Base.taxzone).Current;
    if (current1 == null || current2 == null)
      return;
    bool? nullable = current1.ExternalTaxesImportInProgress;
    if (!nullable.GetValueOrDefault())
      return;
    nullable = current2.IsExternal;
    if (!nullable.GetValueOrDefault())
      return;
    ((Events.FieldVerifyingBase<Events.FieldVerifying<SOTaxTran, SOTaxTran.taxID>>) e).Cancel = true;
  }

  [PXOverride]
  public virtual void InsertImportedTaxes()
  {
    SOOrder order = ((PXSelectBase<SOOrder>) ((PXGraphExtension<SOOrderEntry>) this).Base.Document).Current;
    if (order == null)
      return;
    if (!this.ExternalTaxImportAllowed(order))
      return;
    try
    {
      bool flag = true;
      if (((PXSelectBase) ((PXGraphExtension<SOOrderEntry>) this).Base.Taxes).Cache.AllowUpdate && ((PXSelectBase) ((PXGraphExtension<SOOrderEntry>) this).Base.Taxes).Cache.AllowDelete && (order.Hold.GetValueOrDefault() || order.DontApprove.GetValueOrDefault()))
      {
        if (order.DisableAutomaticTaxCalculation.GetValueOrDefault())
        {
          int? billedCntr = order.BilledCntr;
          int num1 = 0;
          if (!(billedCntr.GetValueOrDefault() > num1 & billedCntr.HasValue))
          {
            int? releasedCntr = order.ReleasedCntr;
            int num2 = 0;
            if (releasedCntr.GetValueOrDefault() > num2 & releasedCntr.HasValue)
              goto label_7;
          }
          else
            goto label_7;
        }
        if (!order.Completed.GetValueOrDefault() && !order.Cancelled.GetValueOrDefault())
          goto label_8;
      }
label_7:
      flag = false;
label_8:
      if (!NonGenericIEnumerableExtensions.Any_(((PXSelectBase) this.ImportedTaxes).Cache.Inserted))
      {
        TaxBaseAttribute.SetTaxCalc<SOLine.taxCategoryID>(((PXSelectBase) ((PXGraphExtension<SOOrderEntry>) this).Base.Transactions).Cache, (object) null, TaxCalc.ManualCalc);
        foreach (PXResult<SOTaxTran> pxResult in ((PXSelectBase<SOTaxTran>) ((PXGraphExtension<SOOrderEntry>) this).Base.Taxes).Select(Array.Empty<object>()))
        {
          SOTaxTran soTaxTran = PXResult<SOTaxTran>.op_Implicit(pxResult);
          if (!flag)
            throw new PXException("Taxes cannot be deleted in the current state of the {0} document.", new object[1]
            {
              (object) order.OrderNbr
            });
          ((PXSelectBase<SOTaxTran>) ((PXGraphExtension<SOOrderEntry>) this).Base.Taxes).Delete(soTaxTran);
        }
        if ((((PXSelectBase<PX.Objects.TX.TaxZone>) ((PXGraphExtension<SOOrderEntry>) this).Base.taxzone).Current == null || !((PXSelectBase<PX.Objects.TX.TaxZone>) ((PXGraphExtension<SOOrderEntry>) this).Base.taxzone).Current.IsExternal.GetValueOrDefault() ? 0 : (((PXSelectBase<PX.Objects.TX.TaxZone>) ((PXGraphExtension<SOOrderEntry>) this).Base.taxzone).Current.TaxPluginID != null ? 1 : 0)) == 0)
          return;
        order.IsTaxValid = new bool?(true);
        order.IsOpenTaxValid = new bool?(true);
        order.IsUnbilledTaxValid = new bool?(true);
        order = ((PXSelectBase<SOOrder>) ((PXGraphExtension<SOOrderEntry>) this).Base.Document).Update(order);
      }
      else
      {
        PX.Objects.TX.TaxZone current = ((PXSelectBase<PX.Objects.TX.TaxZone>) ((PXGraphExtension<SOOrderEntry>) this).Base.taxzone).Current;
        int num3 = ((PXSelectBase<PX.Objects.TX.TaxZone>) ((PXGraphExtension<SOOrderEntry>) this).Base.taxzone).Current == null ? 0 : (((PXSelectBase<PX.Objects.TX.TaxZone>) ((PXGraphExtension<SOOrderEntry>) this).Base.taxzone).Current.IsExternal.GetValueOrDefault() ? 1 : 0);
        GetTaxResult getTaxResult = new GetTaxResult();
        List<TaxLine> taxLineList = new List<TaxLine>();
        List<TaxDetail> taxDetailList = new List<TaxDetail>();
        Decimal num4 = 0M;
        if (num3 != 0)
        {
          if (!flag)
            throw new PXException("Taxes cannot be added in the current state of the {0} document.", new object[1]
            {
              (object) order.OrderNbr
            });
          Sign sign1 = order.DefaultOperation == "R" ? Sign.Minus : Sign.Plus;
          foreach (SOTaxTranImported soTaxTranImported in ((PXSelectBase) this.ImportedTaxes).Cache.Inserted)
          {
            Sign sign2 = sign1;
            Decimal? nullable = soTaxTranImported.CuryTaxableAmt;
            Decimal valueOrDefault1 = (nullable.HasValue ? new Decimal?(Sign.op_Multiply(sign2, nullable.GetValueOrDefault())) : new Decimal?()).GetValueOrDefault();
            Sign sign3 = sign1;
            nullable = soTaxTranImported.CuryTaxAmt;
            Decimal valueOrDefault2 = (nullable.HasValue ? new Decimal?(Sign.op_Multiply(sign3, nullable.GetValueOrDefault())) : new Decimal?()).GetValueOrDefault();
            Decimal num5;
            if (soTaxTranImported.TaxRate.IsNullOrZero())
            {
              if (!soTaxTranImported.CuryTaxableAmt.IsNullOrZero())
              {
                nullable = soTaxTranImported.CuryTaxAmt;
                Decimal valueOrDefault3 = nullable.GetValueOrDefault();
                nullable = soTaxTranImported.CuryTaxableAmt;
                Decimal num6 = nullable ?? 1M;
                num5 = Decimal.Round(valueOrDefault3 / num6, 6);
              }
              else
                num5 = 0M;
            }
            else
            {
              nullable = soTaxTranImported.TaxRate;
              num5 = nullable.GetValueOrDefault();
            }
            Decimal num7 = num5;
            TaxDetail taxDetail = new TaxDetail()
            {
              TaxName = soTaxTranImported.TaxID,
              TaxableAmount = valueOrDefault1,
              TaxAmount = valueOrDefault2,
              Rate = num7
            };
            if (soTaxTranImported.LineNbr.GetValueOrDefault() == 32000)
            {
              TaxLine taxLine = new TaxLine()
              {
                Index = (int) short.MinValue,
                TaxableAmount = valueOrDefault1,
                TaxAmount = valueOrDefault2,
                Rate = num7
              };
              taxLineList.Add(taxLine);
            }
            Decimal num8 = num4;
            nullable = soTaxTranImported.CuryTaxAmt;
            Decimal valueOrDefault4 = nullable.GetValueOrDefault();
            num4 = num8 + valueOrDefault4;
            taxDetailList.Add(taxDetail);
          }
          getTaxResult.TaxSummary = taxDetailList.ToArray();
          getTaxResult.TotalTaxAmount = Sign.op_Multiply(sign1, num4);
          ((PXSelectBase) this.ImportedTaxes).Cache.Clear();
          using (new PXTimeStampScope((byte[]) null))
            this.Base1.ApplyExternalTaxes(order, getTaxResult, getTaxResult, getTaxResult, false);
        }
        else
        {
          List<KeyValuePair<string, Dictionary<string, string>>> source = new List<KeyValuePair<string, Dictionary<string, string>>>();
          foreach (SOTaxTran soTaxTran in ((PXSelectBase) ((PXGraphExtension<SOOrderEntry>) this).Base.Taxes).Cache.Cached)
          {
            ((PXSelectBase) ((PXGraphExtension<SOOrderEntry>) this).Base.Taxes).Cache.GetStatus((object) soTaxTran);
            Dictionary<string, string> errors = PXUIFieldAttribute.GetErrors(((PXSelectBase) ((PXGraphExtension<SOOrderEntry>) this).Base.Taxes).Cache, (object) soTaxTran, Array.Empty<PXErrorLevel>());
            if (errors.Count != 0)
              source.Add(new KeyValuePair<string, Dictionary<string, string>>(soTaxTran.TaxID, errors));
          }
          if (source.Any<KeyValuePair<string, Dictionary<string, string>>>())
          {
            string str = string.Empty;
            foreach (KeyValuePair<string, Dictionary<string, string>> keyValuePair in source)
              str = $"{str}{$"Tax {keyValuePair.Key} was not imported. Error: {keyValuePair.Value.Select<KeyValuePair<string, string>, string>((Func<KeyValuePair<string, string>, string>) (x => x.Value)).Aggregate<string>((Func<string, string, string>) ((e1, e2) => $"{e1}; {e2}"))}"} ";
            throw new PXException(str);
          }
          TaxBaseAttribute.SetTaxCalc<SOLine.taxCategoryID>(((PXSelectBase) ((PXGraphExtension<SOOrderEntry>) this).Base.Transactions).Cache, (object) null, TaxCalc.ManualCalc);
          PXResultset<SOTaxTran> pxResultset = ((PXSelectBase<SOTaxTran>) ((PXGraphExtension<SOOrderEntry>) this).Base.Taxes).Select(Array.Empty<object>());
          foreach (PXResult<SOTaxTran> pxResult in pxResultset)
          {
            SOTaxTran taxTran = PXResult<SOTaxTran>.op_Implicit(pxResult);
            if (this.GetMatchingTax(taxTran) == null)
            {
              if (!flag)
                throw new PXException("Taxes cannot be deleted in the current state of the {0} document.", new object[1]
                {
                  (object) order.OrderNbr
                });
              ((PXSelectBase<SOTaxTran>) ((PXGraphExtension<SOOrderEntry>) this).Base.Taxes).Delete(taxTran);
            }
          }
          foreach (PXResult<SOTaxTran> pxResult in pxResultset)
          {
            SOTaxTran taxTran = PXResult<SOTaxTran>.op_Implicit(pxResult);
            SOTaxTranImported matchingTax = this.GetMatchingTax(taxTran);
            if (matchingTax != null && flag)
            {
              SOTaxTran copy = PXCache<SOTaxTran>.CreateCopy(taxTran);
              Decimal? nullable = matchingTax.CuryTaxableAmt;
              if (nullable.HasValue)
                copy.CuryTaxableAmt = matchingTax.CuryTaxableAmt;
              nullable = matchingTax.CuryTaxAmt;
              if (nullable.HasValue)
                copy.CuryTaxAmt = matchingTax.CuryTaxAmt;
              ((PXSelectBase<SOTaxTran>) ((PXGraphExtension<SOOrderEntry>) this).Base.Taxes).Update(copy);
            }
          }
        }
      }
    }
    finally
    {
      ((PXSelectBase) this.ImportedTaxes).Cache.Clear();
      order.ExternalTaxesImportInProgress = new bool?(false);
    }
  }

  private bool ExternalTaxImportAllowed(SOOrder order)
  {
    return order.ExternalTaxesImportInProgress.GetValueOrDefault() && ((PXGraph) ((PXGraphExtension<SOOrderEntry>) this).Base).IsContractBasedAPI && !this.Base1.skipExternalTaxCalcOnSave && ((PXSelectBase<SOOrder>) ((PXGraphExtension<SOOrderEntry>) this).Base.Document).Current != null && !order.IsTransferOrder.GetValueOrDefault() && !((PXGraphExtension<SOOrderEntry>) this).Base.RecalculateExternalTaxesSync;
  }

  public virtual SOTaxTranImported GetMatchingTax(SOTaxTran taxTran)
  {
    SOTaxTranImported matchingTax = (SOTaxTranImported) null;
    foreach (SOTaxTranImported soTaxTranImported in ((PXSelectBase) this.ImportedTaxes).Cache.Inserted)
    {
      if (string.Equals(soTaxTranImported.TaxID, taxTran.TaxID, StringComparison.OrdinalIgnoreCase))
        matchingTax = soTaxTranImported;
    }
    return matchingTax;
  }
}
