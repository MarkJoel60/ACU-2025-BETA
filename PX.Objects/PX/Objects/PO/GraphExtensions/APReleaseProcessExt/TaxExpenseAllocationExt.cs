// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.GraphExtensions.APReleaseProcessExt.TaxExpenseAllocationExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.Common.Exceptions;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.IN;
using PX.Objects.IN.InventoryRelease;
using PX.Objects.IN.Services;
using PX.Objects.PM;
using PX.Objects.PO.LandedCosts;
using PX.Objects.PO.Services.AmountDistribution;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.PO.GraphExtensions.APReleaseProcessExt;

public class TaxExpenseAllocationExt : 
  PXGraphExtension<UpdatePOOnRelease, APReleaseProcess.MultiCurrency, APReleaseProcess>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.distributionModule>();

  [InjectDependency]
  public AmountDistributionFactory AmountDistributionFactory { get; set; }

  [PXOverride]
  public virtual void GetItemCostTaxAccount(
    PX.Objects.AP.APRegister apdoc,
    PX.Objects.TX.Tax tax,
    PX.Objects.AP.APTran apTran,
    APTaxTran taxTran,
    PX.Objects.PM.Lite.PMProject project,
    PX.Objects.PM.Lite.PMTask task,
    out int? accountID,
    out int? subID,
    TaxExpenseAllocationExt.getItemCostTaxAccountDelegate baseMethod)
  {
    if (!this.IsExtensionEnabled(apdoc) || !this.IsItemCostTax(tax))
    {
      baseMethod(apdoc, tax, apTran, taxTran, project, task, out accountID, out subID);
    }
    else
    {
      PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) ((PXGraphExtension<APReleaseProcess>) this).Base, apTran.InventoryID);
      if (POLineType.IsStock(apTran.LineType) && !POLineType.IsProjectDropShip(apTran.LineType) && (inventoryItem?.ValMethod == "T" || apTran.ReceiptType == "RN"))
        this.GetTaxReasonCodeAccount(apTran, inventoryItem, project, task, out accountID, out subID);
      else if ((POLineType.IsNonStock(apTran.LineType) || POLineType.IsProjectDropShip(apTran.LineType)) && !POLineType.IsService(apTran.LineType) && apTran.POAccrualRefNoteID.HasValue)
        this.GetCOGSAccount(apTran, inventoryItem, out accountID, out subID);
      else
        baseMethod(apdoc, tax, apTran, taxTran, project, task, out accountID, out subID);
    }
  }

  [PXOverride]
  public virtual List<PX.Objects.AP.APRegister> ReleaseInvoice(
    JournalEntry je,
    ref PX.Objects.AP.APRegister doc,
    PXResult<PX.Objects.AP.APInvoice, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CS.Terms, PX.Objects.AP.Vendor> res,
    bool isPrebooking,
    out List<PX.Objects.IN.INRegister> inDocs,
    TaxExpenseAllocationExt.releaseInvoiceDelegate baseMethod)
  {
    List<PX.Objects.AP.APRegister> apRegisterList = baseMethod(je, ref doc, res, isPrebooking, out inDocs);
    this.CalculateTaxExpenseAllocation(doc, inDocs);
    return apRegisterList;
  }

  protected virtual bool IsExtensionEnabled(PX.Objects.AP.APRegister apdoc)
  {
    return !apdoc.Released.GetValueOrDefault() && EnumerableExtensions.IsIn<string>(apdoc.DocType, "ADR", "INV") && apdoc.Status != "X";
  }

  protected virtual bool IsItemCostTax(PX.Objects.TX.Tax tax)
  {
    if (tax.ReportExpenseToSingleAccount.GetValueOrDefault())
      return false;
    if (EnumerableExtensions.IsIn<string>(tax.TaxType, "S", "P"))
      return true;
    return tax.TaxType == "V" && tax.DeductibleVAT.GetValueOrDefault();
  }

  protected virtual void GetTaxReasonCodeAccount(
    PX.Objects.AP.APTran apTran,
    PX.Objects.IN.InventoryItem item,
    PX.Objects.PM.Lite.PMProject project,
    PX.Objects.PM.Lite.PMTask task,
    out int? accountID,
    out int? subID)
  {
    if (((PXGraphExtension<APReleaseProcess>) this).Base.posetup?.TaxReasonCodeID == null)
      throw new FieldIsEmptyException((PXCache) GraphHelper.Caches<POSetup>((PXGraph) ((PXGraphExtension<APReleaseProcess>) this).Base), (object) ((PXGraphExtension<APReleaseProcess>) this).Base.posetup, typeof (POSetup.taxReasonCodeID), Array.Empty<object>());
    PX.Objects.CS.ReasonCode reasonCode = PX.Objects.CS.ReasonCode.PK.Find((PXGraph) ((PXGraphExtension<APReleaseProcess>) this).Base, ((PXGraphExtension<APReleaseProcess>) this).Base.posetup?.TaxReasonCodeID);
    if (reasonCode == null)
      throw new PXException("Reason Code '{0}' cannot be found", new object[1]
      {
        (object) ((PXGraphExtension<APReleaseProcess>) this).Base.posetup?.TaxReasonCodeID
      });
    INPostClass postclass = INPostClass.PK.Find((PXGraph) ((PXGraphExtension<APReleaseProcess>) this).Base, item.PostClassID);
    if (postclass == null)
      throw new RowNotFoundException((PXCache) GraphHelper.Caches<INPostClass>((PXGraph) ((PXGraphExtension<APReleaseProcess>) this).Base), new object[1]
      {
        (object) item.PostClassID
      });
    INSite site = INSite.PK.Find((PXGraph) ((PXGraphExtension<APReleaseProcess>) this).Base, apTran.SiteID);
    if (site == null)
      throw new RowNotFoundException((PXCache) GraphHelper.Caches<INSite>((PXGraph) ((PXGraphExtension<APReleaseProcess>) this).Base), new object[1]
      {
        (object) apTran.SiteID
      });
    accountID = reasonCode.AccountID;
    if (!accountID.HasValue)
      throw new FieldIsEmptyException((PXCache) GraphHelper.Caches<PX.Objects.CS.ReasonCode>((PXGraph) ((PXGraphExtension<APReleaseProcess>) this).Base), (object) reasonCode, typeof (PX.Objects.CS.ReasonCode.accountID), new object[1]
      {
        (object) reasonCode.ReasonCodeID
      });
    if (!reasonCode.SubID.HasValue)
      throw new FieldIsEmptyException((PXCache) GraphHelper.Caches<PX.Objects.CS.ReasonCode>((PXGraph) ((PXGraphExtension<APReleaseProcess>) this).Base), (object) reasonCode, typeof (PX.Objects.CS.ReasonCode.subID), new object[1]
      {
        (object) reasonCode.ReasonCodeID
      });
    if (reasonCode.SubMaskInventory == null)
      throw new FieldIsEmptyException((PXCache) GraphHelper.Caches<PX.Objects.CS.ReasonCode>((PXGraph) ((PXGraphExtension<APReleaseProcess>) this).Base), (object) reasonCode, typeof (PX.Objects.CS.ReasonCode.subMaskInventory), new object[1]
      {
        (object) reasonCode.ReasonCodeID
      });
    subID = INReleaseProcess.GetReasonCodeSubID((PXGraph) ((PXGraphExtension<APReleaseProcess>) this).Base, reasonCode, InventoryAccountServiceHelper.Params(item, site, postclass, (IProjectAccountsSource) project, (IProjectTaskAccountsSource) task));
    if (!subID.HasValue)
      throw new FieldIsEmptyException((PXCache) GraphHelper.Caches<PX.Objects.CS.ReasonCode>((PXGraph) ((PXGraphExtension<APReleaseProcess>) this).Base), (object) reasonCode, typeof (PX.Objects.CS.ReasonCode.subID), new object[1]
      {
        (object) reasonCode.ReasonCodeID
      });
  }

  protected virtual void GetCOGSAccount(
    PX.Objects.AP.APTran apTran,
    PX.Objects.IN.InventoryItem item,
    out int? accountID,
    out int? subID)
  {
    if (apTran.POAccrualType == "O")
    {
      PX.Objects.PO.POLine poLine = PXResultset<PX.Objects.PO.POLine>.op_Implicit(((PXSelectBase<PX.Objects.PO.POLine>) this.Base2.poOrderLineUPD).Select(new object[3]
      {
        (object) apTran.POOrderType,
        (object) apTran.PONbr,
        (object) apTran.POLineNbr
      }));
      accountID = poLine != null ? poLine.ExpenseAcctID : throw new RowNotFoundException(((PXSelectBase) this.Base2.poAccrualUpdate).Cache, new object[3]
      {
        (object) apTran.POAccrualRefNoteID,
        (object) apTran.POAccrualLineNbr,
        (object) apTran.POAccrualType
      });
      subID = poLine.ExpenseSubID;
    }
    else
    {
      PX.Objects.PO.POReceiptLine poReceiptLine = PXResultset<PX.Objects.PO.POReceiptLine>.op_Implicit(((PXSelectBase<PX.Objects.PO.POReceiptLine>) this.Base2.poReceiptLineUPD).Select(new object[3]
      {
        (object) apTran.ReceiptType,
        (object) apTran.ReceiptNbr,
        (object) apTran.ReceiptLineNbr
      }));
      accountID = poReceiptLine != null ? poReceiptLine.ExpenseAcctID : throw new RowNotFoundException(((PXSelectBase) this.Base2.poAccrualUpdate).Cache, new object[3]
      {
        (object) apTran.POAccrualRefNoteID,
        (object) apTran.POAccrualLineNbr,
        (object) apTran.POAccrualType
      });
      subID = poReceiptLine.ExpenseSubID;
    }
  }

  protected virtual void CalculateTaxExpenseAllocation(PX.Objects.AP.APRegister apdoc, List<PX.Objects.IN.INRegister> inDocs)
  {
    if (!this.IsExtensionEnabled(apdoc) || ((PXGraphExtension<APReleaseProcess>) this).Base.IsIntegrityCheck)
      return;
    IDictionary<int, TaxExpenseAllocationExt.TaxByLine> taxByLines = this.CollectTaxes(apdoc);
    this.ApplyTaxAmtToPOAccrualStatuses(taxByLines, apdoc);
    this.CollectPOReceiptLines(taxByLines);
    this.CreateINAdjustment(taxByLines, apdoc, inDocs);
  }

  protected virtual IDictionary<int, TaxExpenseAllocationExt.TaxByLine> CollectTaxes(
    PX.Objects.AP.APRegister apdoc)
  {
    PXResultset<PX.Objects.AP.APTran> source = ((PXSelectBase<PX.Objects.AP.APTran>) ((PXGraphExtension<APReleaseProcess>) this).Base.APTran_TranType_RefNbr).Select(new object[2]
    {
      (object) apdoc.DocType,
      (object) apdoc.RefNbr
    });
    PXCache apTaxCache = (PXCache) GraphHelper.Caches<APTax>((PXGraph) ((PXGraphExtension<APReleaseProcess>) this).Base);
    return (IDictionary<int, TaxExpenseAllocationExt.TaxByLine>) ((IEnumerable<PXResult<PX.Objects.AP.APTran>>) source).AsEnumerable<PXResult<PX.Objects.AP.APTran>>().Select(t => new
    {
      APTran = PXResult<PX.Objects.AP.APTran>.op_Implicit(t),
      InventoryItem = ((PXResult) t).GetItem<PX.Objects.IN.InventoryItem>(),
      Tax = ((PXResult) t).GetItem<PX.Objects.TX.Tax>(),
      APTax = (APTax) apTaxCache.Locate((object) ((PXResult) t).GetItem<APTax>()) ?? ((PXResult) t).GetItem<APTax>()
    }).Where(t => this.IsItemCostTax(t.Tax) && POLineType.IsStock(t.APTran.LineType) && t.InventoryItem.ValMethod != "T" && t.APTran.ReceiptType != "RN").GroupBy(t => t.APTran.LineNbr).ToDictionary<IGrouping<int?, \u003C\u003Ef__AnonymousType78<PX.Objects.AP.APTran, PX.Objects.IN.InventoryItem, PX.Objects.TX.Tax, APTax>>, int, TaxExpenseAllocationExt.TaxByLine>(r => r.Key.Value, r => new TaxExpenseAllocationExt.TaxByLine(r.First().APTran, r.First().InventoryItem, new ARReleaseProcess.Amount(r.Sum(a => this.GetTaxAmount(apdoc, a.Tax, a.APTax, true)), r.Sum(a => this.GetTaxAmount(apdoc, a.Tax, a.APTax, false)))));
  }

  protected virtual Decimal? GetTaxAmount(PX.Objects.AP.APRegister apdoc, PX.Objects.TX.Tax tax, APTax aptax, bool cury)
  {
    Decimal? nullable1 = cury ? aptax.CuryExpenseAmt : aptax.ExpenseAmt;
    Decimal? nullable2 = cury ? aptax.CuryTaxAmt : aptax.TaxAmt;
    Decimal? nullable3 = cury ? aptax.CuryRetainedTaxAmt : aptax.RetainedTaxAmt;
    Decimal? nullable4 = tax.TaxType == "V" ? nullable1 : nullable2;
    Decimal valueOrDefault = apdoc.RetainageApply.GetValueOrDefault() ? nullable3.GetValueOrDefault() : 0M;
    return !nullable4.HasValue ? new Decimal?() : new Decimal?(nullable4.GetValueOrDefault() + valueOrDefault);
  }

  protected virtual void ApplyTaxAmtToPOAccrualStatuses(
    IDictionary<int, TaxExpenseAllocationExt.TaxByLine> taxByLines,
    PX.Objects.AP.APRegister apdoc)
  {
    TaxExpenseAllocationExt.TaxByPOReceiptLine.CalculationParameter calcParameter = new TaxExpenseAllocationExt.TaxByPOReceiptLine.CalculationParameter();
    Dictionary<Tuple<Guid?, int?, string, int?>, \u003C\u003Ef__AnonymousType79<Decimal?, Decimal?, TaxExpenseAllocationExt.TaxByPOReceiptLine.CalculationParameter, List<TaxExpenseAllocationExt.TaxByPOReceiptLine>>> dictionary = GraphHelper.RowCast<POAccrualSplit>(((PXSelectBase) this.Base2.poAccrualSplitUpdate).Cache.Inserted).Where<POAccrualSplit>((Func<POAccrualSplit, bool>) (s => s.APRefNbr == apdoc.RefNbr && s.APDocType == apdoc.DocType && taxByLines.ContainsKey(s.APLineNbr.Value))).GroupBy<POAccrualSplit, Tuple<Guid?, int?, string, int?>>((Func<POAccrualSplit, Tuple<Guid?, int?, string, int?>>) (s => new Tuple<Guid?, int?, string, int?>(s.RefNoteID, s.LineNbr, s.Type, s.APLineNbr))).ToDictionary((Func<IGrouping<Tuple<Guid?, int?, string, int?>, POAccrualSplit>, Tuple<Guid?, int?, string, int?>>) (s => s.Key), s =>
    {
      Decimal? nullable1 = s.Sum<POAccrualSplit>((Func<POAccrualSplit, Decimal?>) (split => split.AccruedQty));
      Decimal? nullable2 = s.Sum<POAccrualSplit>((Func<POAccrualSplit, Decimal?>) (split => split.BaseAccruedQty));
      TaxExpenseAllocationExt.TaxByPOReceiptLine.CalculationParameter calculationParameter1 = new TaxExpenseAllocationExt.TaxByPOReceiptLine.CalculationParameter();
      calculationParameter1.UseBaseUom = s.Any<POAccrualSplit>((Func<POAccrualSplit, bool>) (split => !split.AccruedQty.HasValue));
      TaxExpenseAllocationExt.TaxByPOReceiptLine.CalculationParameter calculationParameter2 = calculationParameter1;
      calcParameter = calculationParameter1;
      TaxExpenseAllocationExt.TaxByPOReceiptLine.CalculationParameter calculationParameter3 = calculationParameter2;
      List<TaxExpenseAllocationExt.TaxByPOReceiptLine> list = s.Select<POAccrualSplit, TaxExpenseAllocationExt.TaxByPOReceiptLine>((Func<POAccrualSplit, TaxExpenseAllocationExt.TaxByPOReceiptLine>) (split => new TaxExpenseAllocationExt.TaxByPOReceiptLine()
      {
        POAccrualSplit = split,
        CalcParameter = calcParameter
      })).ToList<TaxExpenseAllocationExt.TaxByPOReceiptLine>();
      return new
      {
        SumOfAccruedQty = nullable1,
        SumOfBaseAccruedQty = nullable2,
        CalcParameter = calculationParameter3,
        Splits = list
      };
    });
    foreach (TaxExpenseAllocationExt.TaxByLine taxByLine in (IEnumerable<TaxExpenseAllocationExt.TaxByLine>) taxByLines.Values)
    {
      Decimal? nullable3 = taxByLine.APTran.Qty;
      Decimal num1 = 0M;
      if (!(nullable3.GetValueOrDefault() == num1 & nullable3.HasValue) && taxByLine.APTran.POAccrualRefNoteID.HasValue && !EnumerableExtensions.IsIn<string>(taxByLine.APTran.LineType, "PG", "PN"))
      {
        POAccrualStatus poAccrualStatus = ((PXSelectBase<POAccrualStatus>) this.Base2.poAccrualUpdate).Locate(new POAccrualStatus()
        {
          RefNoteID = taxByLine.APTran.POAccrualRefNoteID,
          LineNbr = taxByLine.APTran.POAccrualLineNbr,
          Type = taxByLine.APTran.POAccrualType
        });
        POAccrualStatus poAccrualStatus1 = poAccrualStatus != null ? poAccrualStatus : throw new RowNotFoundException(((PXSelectBase) this.Base2.poAccrualUpdate).Cache, new object[3]
        {
          (object) taxByLine.APTran.POAccrualRefNoteID,
          (object) taxByLine.APTran.POAccrualLineNbr,
          (object) taxByLine.APTran.POAccrualType
        });
        nullable3 = poAccrualStatus1.BilledTaxAdjCost;
        Decimal? nullable4 = taxByLine.TaxAmt.Base;
        Decimal sign1 = taxByLine.APTran.Sign;
        Decimal? nullable5;
        Decimal? nullable6;
        if (!nullable4.HasValue)
        {
          nullable5 = new Decimal?();
          nullable6 = nullable5;
        }
        else
          nullable6 = new Decimal?(nullable4.GetValueOrDefault() * sign1);
        nullable5 = nullable6;
        Decimal valueOrDefault1 = nullable5.GetValueOrDefault();
        Decimal? nullable7;
        if (!nullable3.HasValue)
        {
          nullable5 = new Decimal?();
          nullable7 = nullable5;
        }
        else
          nullable7 = new Decimal?(nullable3.GetValueOrDefault() + valueOrDefault1);
        poAccrualStatus1.BilledTaxAdjCost = nullable7;
        if (poAccrualStatus.BillCuryID == apdoc.CuryID)
        {
          POAccrualStatus poAccrualStatus2 = poAccrualStatus;
          nullable4 = poAccrualStatus2.CuryBilledTaxAdjCost;
          nullable3 = taxByLine.TaxAmt.Cury;
          Decimal sign2 = taxByLine.APTran.Sign;
          Decimal? nullable8;
          if (!nullable3.HasValue)
          {
            nullable5 = new Decimal?();
            nullable8 = nullable5;
          }
          else
            nullable8 = new Decimal?(nullable3.GetValueOrDefault() * sign2);
          nullable5 = nullable8;
          Decimal valueOrDefault2 = nullable5.GetValueOrDefault();
          Decimal? nullable9;
          if (!nullable4.HasValue)
          {
            nullable5 = new Decimal?();
            nullable9 = nullable5;
          }
          else
            nullable9 = new Decimal?(nullable4.GetValueOrDefault() + valueOrDefault2);
          poAccrualStatus2.CuryBilledTaxAdjCost = nullable9;
        }
        else
        {
          POAccrualStatus poAccrualStatus3 = poAccrualStatus;
          nullable3 = new Decimal?();
          Decimal? nullable10 = nullable3;
          poAccrualStatus3.CuryBilledTaxAdjCost = nullable10;
        }
        POAccrualDetail poAccrualDetail1 = this.Base2.PrepareAPTranAccrualDetail(taxByLine.APTran, apdoc);
        if (poAccrualDetail1 != null)
        {
          POAccrualDetail poAccrualDetail2 = poAccrualDetail1;
          nullable3 = poAccrualDetail2.TaxAccruedCost;
          nullable4 = taxByLine.TaxAmt.Base;
          Decimal sign3 = taxByLine.APTran.Sign;
          Decimal? nullable11;
          if (!nullable4.HasValue)
          {
            nullable5 = new Decimal?();
            nullable11 = nullable5;
          }
          else
            nullable11 = new Decimal?(nullable4.GetValueOrDefault() * sign3);
          nullable5 = nullable11;
          Decimal valueOrDefault3 = nullable5.GetValueOrDefault();
          Decimal? nullable12;
          if (!nullable3.HasValue)
          {
            nullable5 = new Decimal?();
            nullable12 = nullable5;
          }
          else
            nullable12 = new Decimal?(nullable3.GetValueOrDefault() + valueOrDefault3);
          poAccrualDetail2.TaxAccruedCost = nullable12;
          ((PXSelectBase<POAccrualDetail>) this.Base2.poAccrualDetailUpdate).Update(poAccrualDetail1);
        }
        Tuple<Guid?, int?, string, int?> key = new Tuple<Guid?, int?, string, int?>(taxByLine.APTran.POAccrualRefNoteID, taxByLine.APTran.POAccrualLineNbr, taxByLine.APTran.POAccrualType, taxByLine.APTran.LineNbr);
        if (dictionary.ContainsKey(key))
        {
          var data = dictionary[key];
          taxByLine.Splits = data.Splits;
          ARReleaseProcess.Amount taxAmt = taxByLine.TaxAmt;
          ARReleaseProcess.Amount amount1;
          if (!data.CalcParameter.UseBaseUom)
          {
            ARReleaseProcess.Amount amount2 = taxAmt * data.SumOfAccruedQty;
            nullable4 = taxByLine.APTran.Qty;
            Decimal num2 = nullable4.Value;
            amount1 = amount2 / num2;
          }
          else
          {
            ARReleaseProcess.Amount amount3 = taxAmt * data.SumOfBaseAccruedQty;
            nullable4 = taxByLine.APTran.BaseQty;
            Decimal num3 = nullable4.Value;
            amount1 = amount3 / num3;
          }
          this.AmountDistributionFactory.CreateService<TaxExpenseAllocationExt.TaxByPOReceiptLine>(DistributionMethod.RemainderToBiggestLine, new DistributionParameter<TaxExpenseAllocationExt.TaxByPOReceiptLine>()
          {
            Items = (IEnumerable<TaxExpenseAllocationExt.TaxByPOReceiptLine>) data.Splits,
            Amount = amount1.Base,
            CuryAmount = amount1.Cury,
            CuryRow = (object) apdoc,
            CacheOfCuryRow = ((PXSelectBase) ((PXGraphExtension<APReleaseProcess>) this).Base.APDocument).Cache,
            OnValueCalculated = (Func<TaxExpenseAllocationExt.TaxByPOReceiptLine, Decimal?, Decimal?, TaxExpenseAllocationExt.TaxByPOReceiptLine>) ((item, amount, curyAmount) => this.AddAmountToPOAccrual(poAccrualStatus, item, amount)),
            OnRoundingDifferenceApplied = (Action<TaxExpenseAllocationExt.TaxByPOReceiptLine, Decimal?, Decimal?, Decimal?, Decimal?>) ((item, newAmount, curyNewAmount, oldAmount, curyOldAmount) =>
            {
              POAccrualStatus poAccrualStatus4 = poAccrualStatus;
              TaxExpenseAllocationExt.TaxByPOReceiptLine split = item;
              Decimal? nullable13 = newAmount;
              Decimal? nullable14 = oldAmount;
              Decimal? taxAmtBase = nullable13.HasValue & nullable14.HasValue ? new Decimal?(nullable13.GetValueOrDefault() - nullable14.GetValueOrDefault()) : new Decimal?();
              this.AddAmountToPOAccrual(poAccrualStatus4, split, taxAmtBase);
            })
          }).Distribute();
          GraphHelper.MarkUpdated(((PXSelectBase) this.Base2.poAccrualUpdate).Cache, (object) poAccrualStatus, true);
        }
      }
    }
  }

  protected virtual TaxExpenseAllocationExt.TaxByPOReceiptLine AddAmountToPOAccrual(
    POAccrualStatus poAccrualStatus,
    TaxExpenseAllocationExt.TaxByPOReceiptLine split,
    Decimal? taxAmtBase)
  {
    POAccrualStatus poAccrualStatus1 = poAccrualStatus;
    Decimal? receivedTaxAdjCost = poAccrualStatus1.ReceivedTaxAdjCost;
    Decimal? nullable1 = taxAmtBase;
    poAccrualStatus1.ReceivedTaxAdjCost = receivedTaxAdjCost.HasValue & nullable1.HasValue ? new Decimal?(receivedTaxAdjCost.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
    split.POAccrualSplit = ((PXSelectBase<POAccrualSplit>) this.Base2.poAccrualSplitUpdate).Update(split.POAccrualSplit);
    POAccrualDetail lineAccrualDetail = poAccrualStatus.IsAccountAffected.Value ? this.Base2.FindPOReceiptLineAccrualDetail(split.POAccrualSplit) : (POAccrualDetail) null;
    Decimal? nullable2;
    if (lineAccrualDetail != null)
    {
      POAccrualDetail poAccrualDetail = lineAccrualDetail;
      Decimal? taxAccruedCost = poAccrualDetail.TaxAccruedCost;
      nullable2 = taxAmtBase;
      poAccrualDetail.TaxAccruedCost = taxAccruedCost.HasValue & nullable2.HasValue ? new Decimal?(taxAccruedCost.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
      ((PXSelectBase<POAccrualDetail>) this.Base2.poAccrualDetailUpdate).Update(lineAccrualDetail);
    }
    POAccrualDetail poAccrualDetail1 = ((PXSelectBase) this.Base2.poAccrualDetailUpdate).Cache.Inserted.OfType<POAccrualDetail>().FirstOrDefault<POAccrualDetail>((Func<POAccrualDetail, bool>) (x =>
    {
      int? lineNbr = x.LineNbr;
      int? apLineNbr = split.POAccrualSplit.APLineNbr;
      return lineNbr.GetValueOrDefault() == apLineNbr.GetValueOrDefault() & lineNbr.HasValue == apLineNbr.HasValue;
    }));
    if (poAccrualDetail1 != null)
    {
      POAccrualDetail poAccrualDetail2 = poAccrualDetail1;
      nullable2 = poAccrualDetail2.TaxAdjAmt;
      Decimal? nullable3 = taxAmtBase;
      poAccrualDetail2.TaxAdjAmt = nullable2.HasValue & nullable3.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + nullable3.GetValueOrDefault()) : new Decimal?();
      ((PXSelectBase<POAccrualDetail>) this.Base2.poAccrualDetailUpdate).Update(poAccrualDetail1);
    }
    return split;
  }

  protected virtual void CollectPOReceiptLines(
    IDictionary<int, TaxExpenseAllocationExt.TaxByLine> taxByLines)
  {
    List<AllocationServiceBase.POReceiptLineAdjustment> receiptLineAdjustmentList = new List<AllocationServiceBase.POReceiptLineAdjustment>();
    foreach (TaxExpenseAllocationExt.TaxByLine taxByLine in taxByLines.Values.Where<TaxExpenseAllocationExt.TaxByLine>((Func<TaxExpenseAllocationExt.TaxByLine, bool>) (line => line.Splits != null)))
    {
      foreach (TaxExpenseAllocationExt.TaxByPOReceiptLine split in taxByLine.Splits)
        split.POReceiptLine = PropertyTransfer.Transfer<PX.Objects.PO.POReceiptLine, PX.Objects.PO.POReceiptLine>(PXResultset<PX.Objects.PO.POReceiptLine>.op_Implicit(((PXSelectBase<PX.Objects.PO.POReceiptLine>) this.Base2.poReceiptLineUPD).Select(new object[3]
        {
          (object) split.POAccrualSplit.POReceiptType,
          (object) split.POAccrualSplit.POReceiptNbr,
          (object) split.POAccrualSplit.POReceiptLineNbr
        })) ?? throw new RowNotFoundException(((PXSelectBase) this.Base2.poReceiptLineUPD).Cache, new object[3]
        {
          (object) split.POAccrualSplit.POReceiptType,
          (object) split.POAccrualSplit.POReceiptNbr,
          (object) split.POAccrualSplit.POReceiptLineNbr
        }), new PX.Objects.PO.POReceiptLine());
    }
  }

  protected virtual void CreateINAdjustment(
    IDictionary<int, TaxExpenseAllocationExt.TaxByLine> taxByLines,
    PX.Objects.AP.APRegister apdoc,
    List<PX.Objects.IN.INRegister> inDocs)
  {
    List<AllocationServiceBase.POReceiptLineAdjustment> receiptLineAdjustmentList = this.CollectINAdjustmentLines(taxByLines, apdoc);
    if (!this.VerifyAdjustments(apdoc, (IEnumerable<AllocationServiceBase.POReceiptLineAdjustment>) receiptLineAdjustmentList))
      return;
    PX.Objects.IN.INRegister inAdjustment = this.CreateINAdjustment(apdoc, receiptLineAdjustmentList);
    this.OnTaxAdjustmentCreated(inAdjustment, apdoc, receiptLineAdjustmentList);
    inDocs.Add(inAdjustment);
  }

  protected virtual List<AllocationServiceBase.POReceiptLineAdjustment> CollectINAdjustmentLines(
    IDictionary<int, TaxExpenseAllocationExt.TaxByLine> taxByLines,
    PX.Objects.AP.APRegister apdoc)
  {
    List<AllocationServiceBase.POReceiptLineAdjustment> receiptLineAdjustmentList = new List<AllocationServiceBase.POReceiptLineAdjustment>();
    foreach (TaxExpenseAllocationExt.TaxByLine taxByLine in taxByLines.Values.Where<TaxExpenseAllocationExt.TaxByLine>((Func<TaxExpenseAllocationExt.TaxByLine, bool>) (s => s.Splits != null)))
    {
      foreach (TaxExpenseAllocationExt.TaxByPOReceiptLine taxByPoReceiptLine in taxByLine.Splits.Where<TaxExpenseAllocationExt.TaxByPOReceiptLine>((Func<TaxExpenseAllocationExt.TaxByPOReceiptLine, bool>) (s => s.POReceiptLine != null)))
      {
        Decimal valueOrDefault = taxByPoReceiptLine.POAccrualSplit.TaxAccruedCost.GetValueOrDefault();
        Decimal rest = valueOrDefault - PurchasePriceVarianceAllocationService.Instance.AllocateOverRCTLine((PXGraph) ((PXGraphExtension<APReleaseProcess>) this).Base, receiptLineAdjustmentList, taxByPoReceiptLine.POReceiptLine, valueOrDefault, taxByLine.APTran.BranchID);
        PurchasePriceVarianceAllocationService.Instance.AllocateRestOverRCTLines((IList<AllocationServiceBase.POReceiptLineAdjustment>) receiptLineAdjustmentList, rest);
      }
    }
    return receiptLineAdjustmentList;
  }

  protected virtual bool VerifyAdjustments(
    PX.Objects.AP.APRegister apdoc,
    IEnumerable<AllocationServiceBase.POReceiptLineAdjustment> adjustments)
  {
    if (apdoc.DocType == "ADR")
    {
      PX.Objects.AP.APInvoice apInvoice = PX.Objects.AP.APInvoice.PK.Find((PXGraph) ((PXGraphExtension<APReleaseProcess>) this).Base, apdoc.OrigDocType, apdoc.OrigRefNbr);
      if (apInvoice != null && apInvoice.TaxCostINAdjRefNbr != null)
      {
        PX.Objects.IN.INRegister inRegister = PX.Objects.IN.INRegister.PK.Find((PXGraph) ((PXGraphExtension<APReleaseProcess>) this).Base, "A", apInvoice.TaxCostINAdjRefNbr);
        if (inRegister != null && !inRegister.Released.GetValueOrDefault())
          throw new PXException("Cannot reverse the bill because the {0} IN adjustment linked to the {1} bill is not released. Release the adjustment first to be able to reverse the bill.", new object[2]
          {
            (object) apInvoice.TaxCostINAdjRefNbr,
            (object) apdoc.OrigRefNbr
          });
      }
    }
    return adjustments.Any<AllocationServiceBase.POReceiptLineAdjustment>();
  }

  protected virtual PX.Objects.IN.INRegister CreateINAdjustment(
    PX.Objects.AP.APRegister apdoc,
    List<AllocationServiceBase.POReceiptLineAdjustment> adjustmentLines)
  {
    if (((PXGraphExtension<APReleaseProcess>) this).Base.posetup?.TaxReasonCodeID == null)
      throw new FieldIsEmptyException((PXCache) GraphHelper.Caches<POSetup>((PXGraph) ((PXGraphExtension<APReleaseProcess>) this).Base), (object) ((PXGraphExtension<APReleaseProcess>) this).Base.posetup, typeof (POSetup.taxReasonCodeID), Array.Empty<object>());
    if (PX.Objects.CS.ReasonCode.PK.Find((PXGraph) ((PXGraphExtension<APReleaseProcess>) this).Base, ((PXGraphExtension<APReleaseProcess>) this).Base.posetup?.TaxReasonCodeID) == null)
      throw new PXException("Reason Code '{0}' cannot be found", new object[1]
      {
        (object) ((PXGraphExtension<APReleaseProcess>) this).Base.posetup?.TaxReasonCodeID
      });
    INAdjustmentEntry instance = PXGraph.CreateInstance<INAdjustmentEntry>();
    ((PXGraph) instance).Clear();
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    ((PXGraph) instance).FieldVerifying.AddHandler<INTran.inventoryID>(TaxExpenseAllocationExt.\u003C\u003Ec.\u003C\u003E9__24_0 ?? (TaxExpenseAllocationExt.\u003C\u003Ec.\u003C\u003E9__24_0 = new PXFieldVerifying((object) TaxExpenseAllocationExt.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CCreateINAdjustment\u003Eb__24_0))));
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    ((PXGraph) instance).FieldVerifying.AddHandler<INTran.origRefNbr>(TaxExpenseAllocationExt.\u003C\u003Ec.\u003C\u003E9__24_1 ?? (TaxExpenseAllocationExt.\u003C\u003Ec.\u003C\u003E9__24_1 = new PXFieldVerifying((object) TaxExpenseAllocationExt.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CCreateINAdjustment\u003Eb__24_1))));
    ((PXSelectBase<INSetup>) instance.insetup).Current.RequireControlTotal = new bool?(false);
    ((PXSelectBase<INSetup>) instance.insetup).Current.HoldEntry = new bool?(false);
    ((PXSelectBase<PX.Objects.IN.INRegister>) instance.adjustment).Insert(new PX.Objects.IN.INRegister()
    {
      DocType = "A",
      OrigModule = "AP",
      OrigRefNbr = apdoc.RefNbr,
      SiteID = new int?(),
      TranDate = apdoc.DocDate,
      FinPeriodID = apdoc.FinPeriodID,
      BranchID = apdoc.BranchID,
      IsTaxAdjustmentTran = new bool?(true)
    });
    this.GetINAdjustmentFactory(instance).CreateAdjustmentTran(adjustmentLines, ((PXGraphExtension<APReleaseProcess>) this).Base.posetup.TaxReasonCodeID);
    ((PXAction) instance.Save).Press();
    return ((PXSelectBase<PX.Objects.IN.INRegister>) instance.adjustment).Current;
  }

  protected virtual void OnTaxAdjustmentCreated(
    PX.Objects.IN.INRegister adjustment,
    PX.Objects.AP.APRegister apdoc,
    List<AllocationServiceBase.POReceiptLineAdjustment> adjustmentLines)
  {
    apdoc.TaxCostINAdjRefNbr = adjustment.RefNbr;
    foreach (POAccrualDetail poAccrualDetail in ((PXSelectBase) this.Base2.poAccrualDetailUpdate).Cache.Inserted.OfType<POAccrualDetail>().Where<POAccrualDetail>((Func<POAccrualDetail, bool>) (x =>
    {
      if (!(x.APDocType == apdoc.DocType) || !(x.APRefNbr == apdoc.RefNbr))
        return false;
      Decimal? taxAdjAmt = x.TaxAdjAmt;
      Decimal num = 0M;
      return !(taxAdjAmt.GetValueOrDefault() == num & taxAdjAmt.HasValue);
    })).ToList<POAccrualDetail>())
    {
      poAccrualDetail.TaxAdjRefNbr = adjustment.RefNbr;
      poAccrualDetail.TaxAdjPosted = new bool?(false);
      ((PXSelectBase<POAccrualDetail>) this.Base2.poAccrualDetailUpdate).Update(poAccrualDetail);
      POAccrualStatus poAccrualStatus1 = ((PXSelectBase<POAccrualStatus>) this.Base2.poAccrualUpdate).Locate(new POAccrualStatus()
      {
        RefNoteID = poAccrualDetail.POAccrualRefNoteID,
        LineNbr = poAccrualDetail.POAccrualLineNbr,
        Type = poAccrualDetail.POAccrualType
      });
      if (poAccrualStatus1 != null)
      {
        POAccrualStatus poAccrualStatus2 = poAccrualStatus1;
        int? unreleasedTaxAdjCntr = poAccrualStatus2.UnreleasedTaxAdjCntr;
        poAccrualStatus2.UnreleasedTaxAdjCntr = unreleasedTaxAdjCntr.HasValue ? new int?(unreleasedTaxAdjCntr.GetValueOrDefault() + 1) : new int?();
        ((PXSelectBase<POAccrualStatus>) this.Base2.poAccrualUpdate).Update(poAccrualStatus1);
      }
    }
  }

  protected virtual PurchasePriceVarianceINAdjustmentFactory GetINAdjustmentFactory(
    INAdjustmentEntry inGraph)
  {
    return new PurchasePriceVarianceINAdjustmentFactory(inGraph);
  }

  public delegate List<PX.Objects.AP.APRegister> releaseInvoiceDelegate(
    JournalEntry je,
    ref PX.Objects.AP.APRegister doc,
    PXResult<PX.Objects.AP.APInvoice, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CS.Terms, PX.Objects.AP.Vendor> res,
    bool isPrebooking,
    out List<PX.Objects.IN.INRegister> inDocs);

  public delegate void getItemCostTaxAccountDelegate(
    PX.Objects.AP.APRegister apdoc,
    PX.Objects.TX.Tax tax,
    PX.Objects.AP.APTran apTran,
    APTaxTran apTaxTran,
    PX.Objects.PM.Lite.PMProject project,
    PX.Objects.PM.Lite.PMTask task,
    out int? accountID,
    out int? subID);

  protected class TaxByPOReceiptLine : IAmountItem
  {
    public PX.Objects.PO.POReceiptLine POReceiptLine { get; set; }

    public POAccrualSplit POAccrualSplit { get; set; }

    public TaxExpenseAllocationExt.TaxByPOReceiptLine.CalculationParameter CalcParameter { get; set; }

    public Decimal Weight
    {
      get
      {
        return (this.CalcParameter.UseBaseUom ? this.POAccrualSplit.BaseAccruedQty : this.POAccrualSplit.AccruedQty).GetValueOrDefault();
      }
    }

    public Decimal? Amount
    {
      get => this.POAccrualSplit.TaxAccruedCost;
      set => this.POAccrualSplit.TaxAccruedCost = value;
    }

    public Decimal? CuryAmount { get; set; }

    public class CalculationParameter
    {
      public bool UseBaseUom { get; set; }
    }
  }

  protected class TaxByLine
  {
    public TaxByLine(PX.Objects.AP.APTran apTran, PX.Objects.IN.InventoryItem item, ARReleaseProcess.Amount taxAmt)
    {
      this.APTran = apTran;
      this.Item = item;
      this.TaxAmt = taxAmt;
    }

    public PX.Objects.AP.APTran APTran { get; }

    public PX.Objects.IN.InventoryItem Item { get; }

    public ARReleaseProcess.Amount TaxAmt { get; set; }

    public List<TaxExpenseAllocationExt.TaxByPOReceiptLine> Splits { get; set; }
  }
}
