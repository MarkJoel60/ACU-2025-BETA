// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APReleaseProcessPerUnitTaxPoster
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.Extensions.PerUnitTax;
using PX.Objects.GL;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.AP;

/// <summary>A per unit taxes post on AP release helper.</summary>
public class APReleaseProcessPerUnitTaxPoster : 
  PerUnitTaxesPostOnReleaseExt<APReleaseProcess, APInvoice, APTran, APTax, APTaxTran>
{
  private APReleaseProcessPerUnitTaxPoster.PostTaxDelegate _postGeneralTax;
  private APReleaseProcessPerUnitTaxPoster.PostTaxDelegate _postReverseTax;

  public static bool IsActive()
  {
    return PerUnitTaxesPostOnReleaseExt<APReleaseProcess, APInvoice, APTran, APTax, APTaxTran>.IsActiveBase();
  }

  public override void Initialize()
  {
    base.Initialize();
    this.FillProtectedMethodDelegate<APReleaseProcessPerUnitTaxPoster.PostTaxDelegate>("PostGeneralTax", out this._postGeneralTax);
    this.FillProtectedMethodDelegate<APReleaseProcessPerUnitTaxPoster.PostTaxDelegate>("PostReverseTax", out this._postReverseTax);
  }

  protected override bool CheckInputDocument(APInvoice apInvoice)
  {
    if (!base.CheckInputDocument(apInvoice))
      return false;
    if (!apInvoice.IsOriginalRetainageDocument() || !this.Base.apsetup.RetainTaxes.GetValueOrDefault())
      return true;
    throw new PXNotSupportedException("Per-unit taxes are not supported for documents with retainage.");
  }

  protected override void CreateAndPostGLTransactionsOnTaxAccount(
    JournalEntry journalEntry,
    APInvoice document,
    PX.Objects.CM.Extensions.CurrencyInfo newCurrencyInfo,
    APTaxTran perUnitAggregatedTax,
    PX.Objects.TX.Tax perUnitTax,
    bool isDebitTaxTran)
  {
    if (!perUnitTax.ReverseTax.GetValueOrDefault())
    {
      APReleaseProcessPerUnitTaxPoster.PostTaxDelegate postGeneralTax = this._postGeneralTax;
      if (postGeneralTax == null)
        return;
      postGeneralTax(journalEntry, document, newCurrencyInfo, perUnitAggregatedTax, perUnitTax);
    }
    else
    {
      APReleaseProcessPerUnitTaxPoster.PostTaxDelegate postReverseTax = this._postReverseTax;
      if (postReverseTax == null)
        return;
      postReverseTax(journalEntry, document, newCurrencyInfo, perUnitAggregatedTax, perUnitTax);
    }
  }

  protected override IEnumerable<(APTax Tax, APTran Line)> GetTaxWithLines(
    PX.Objects.TX.Tax perUnitTax,
    APTaxTran perUnitAggregatedTax)
  {
    return PXSelectBase<APTax, PXSelectJoin<APTax, InnerJoin<APTran, On<APTax.tranType, Equal<APTran.tranType>, And<APTax.refNbr, Equal<APTran.refNbr>, And<APTax.lineNbr, Equal<APTran.lineNbr>>>>>, Where<APTax.taxID, Equal<Required<APTax.taxID>>, And<APTran.tranType, Equal<Required<APTran.tranType>>, And<APTran.refNbr, Equal<Required<APTran.refNbr>>>>>, OrderBy<Desc<APTax.curyTaxAmt>>>.Config>.Select((PXGraph) this.Base, (object) perUnitTax.TaxID, (object) perUnitAggregatedTax.TranType, (object) perUnitAggregatedTax.RefNbr).AsEnumerable<PXResult<APTax>>().Select<PXResult<APTax>, (APTax, APTran)>((Func<PXResult<APTax>, (APTax, APTran)>) (res => (res.GetItem<APTax>(), res.GetItem<APTran>())));
  }

  protected override PX.Objects.GL.GLTran CreateGLTranForPerUnitLineTax(
    APInvoice apInvoice,
    PX.Objects.CM.Extensions.CurrencyInfo newCurrencyInfo,
    APTaxTran perUnitAggregatedTax,
    APTran apTran,
    APTax perUnitLineTax,
    bool isDebitTaxTran)
  {
    PX.Objects.GL.GLTran forPerUnitLineTax = base.CreateGLTranForPerUnitLineTax(apInvoice, newCurrencyInfo, perUnitAggregatedTax, apTran, perUnitLineTax, isDebitTaxTran);
    forPerUnitLineTax.SummPost = new bool?(this.Base.SummPost);
    forPerUnitLineTax.BranchID = apTran.BranchID;
    forPerUnitLineTax.AccountID = apTran.AccountID;
    forPerUnitLineTax.SubID = apTran.SubID;
    forPerUnitLineTax.TranLineNbr = apTran.LineNbr;
    forPerUnitLineTax.ReferenceID = apInvoice.VendorID;
    forPerUnitLineTax.ProjectID = apTran.ProjectID;
    forPerUnitLineTax.TaskID = apTran.TaskID;
    forPerUnitLineTax.CostCodeID = apTran.CostCodeID;
    if (isDebitTaxTran)
    {
      forPerUnitLineTax.CuryDebitAmt = perUnitLineTax.CuryTaxAmt;
      forPerUnitLineTax.DebitAmt = perUnitLineTax.TaxAmt;
    }
    else
    {
      forPerUnitLineTax.CuryCreditAmt = perUnitLineTax.CuryTaxAmt;
      forPerUnitLineTax.CreditAmt = perUnitLineTax.TaxAmt;
    }
    return forPerUnitLineTax;
  }

  protected override PX.Objects.GL.GLTran InsertNewGLTran(
    JournalEntry journalEntry,
    APInvoice apInvoice,
    APTran apTran,
    APTaxTran perUnitAggregatedTax,
    APTax perUnitLineTax,
    PX.Objects.GL.GLTran newGlTran)
  {
    APReleaseProcess.GLTranInsertionContext context = new APReleaseProcess.GLTranInsertionContext()
    {
      APRegisterRecord = (APRegister) apInvoice,
      APTranRecord = apTran,
      APTaxTranRecord = perUnitAggregatedTax
    };
    return this.Base.InsertInvoicePerUnitTaxAmountsToItemAccountsTransaction(journalEntry, newGlTran, context);
  }

  protected delegate void PostTaxDelegate(
    JournalEntry journalEntry,
    APInvoice document,
    PX.Objects.CM.Extensions.CurrencyInfo newCurrencyInfo,
    APTaxTran perUnitAggregatedTax,
    PX.Objects.TX.Tax perUnitTax);
}
