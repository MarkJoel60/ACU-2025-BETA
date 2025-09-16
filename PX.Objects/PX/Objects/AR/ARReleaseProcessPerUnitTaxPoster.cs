// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARReleaseProcessPerUnitTaxPoster
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.Extensions.PerUnitTax;
using PX.Objects.GL;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.AR;

/// <summary>A per unit taxes post on AR release helper.</summary>
public class ARReleaseProcessPerUnitTaxPoster : 
  PerUnitTaxesPostOnReleaseExt<ARReleaseProcess, ARInvoice, ARTran, ARTax, ARTaxTran>
{
  private ARReleaseProcessPerUnitTaxPoster.PostTaxDelegate _postGeneralTax;

  public static bool IsActive()
  {
    return PerUnitTaxesPostOnReleaseExt<ARReleaseProcess, ARInvoice, ARTran, ARTax, ARTaxTran>.IsActiveBase();
  }

  public virtual void Initialize()
  {
    ((PXGraphExtension) this).Initialize();
    this.FillProtectedMethodDelegate<ARReleaseProcessPerUnitTaxPoster.PostTaxDelegate>("PostGeneralTax", out this._postGeneralTax);
  }

  protected override bool CheckInputDocument(ARInvoice arInvoice)
  {
    if (!base.CheckInputDocument(arInvoice))
      return false;
    if (!arInvoice.IsOriginalRetainageDocument() || !this.Base.arsetup.RetainTaxes.GetValueOrDefault())
      return true;
    throw new PXNotSupportedException("Per-unit taxes are not supported for documents with retainage.");
  }

  protected override void CreateAndPostGLTransactionsOnTaxAccount(
    JournalEntry journalEntry,
    ARInvoice document,
    PX.Objects.CM.Extensions.CurrencyInfo newCurrencyInfo,
    ARTaxTran perUnitAggregatedTax,
    PX.Objects.TX.Tax perUnitTax,
    bool isDebitTaxTran)
  {
    PX.Objects.GL.Account taxAccount = PXResultset<PX.Objects.GL.Account>.op_Implicit(PXSelectBase<PX.Objects.GL.Account, PXViewOf<PX.Objects.GL.Account>.BasedOn<SelectFromBase<PX.Objects.GL.Account, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<PX.Objects.GL.Account.accountID, IBqlInt>.IsEqual<P.AsInt>>>.Config>.SelectSingleBound((PXGraph) this.Base, (object[]) null, new object[1]
    {
      (object) perUnitAggregatedTax.AccountID
    }));
    ARReleaseProcessPerUnitTaxPoster.PostTaxDelegate postGeneralTax = this._postGeneralTax;
    if (postGeneralTax == null)
      return;
    postGeneralTax(journalEntry, document, (ARRegister) document, perUnitTax, perUnitAggregatedTax, taxAccount, newCurrencyInfo, isDebitTaxTran);
  }

  protected override IEnumerable<(ARTax Tax, ARTran Line)> GetTaxWithLines(
    PX.Objects.TX.Tax perUnitTax,
    ARTaxTran perUnitAggregatedTax)
  {
    return ((IEnumerable<PXResult<ARTax>>) PXSelectBase<ARTax, PXSelectJoin<ARTax, InnerJoin<ARTran, On<ARTax.tranType, Equal<ARTran.tranType>, And<ARTax.refNbr, Equal<ARTran.refNbr>, And<ARTax.lineNbr, Equal<ARTran.lineNbr>>>>>, Where<ARTax.taxID, Equal<Required<ARTax.taxID>>, And<ARTran.tranType, Equal<Required<ARTran.tranType>>, And<ARTran.refNbr, Equal<Required<ARTran.refNbr>>>>>, OrderBy<Desc<ARTax.curyTaxAmt>>>.Config>.Select((PXGraph) this.Base, new object[3]
    {
      (object) perUnitTax.TaxID,
      (object) perUnitAggregatedTax.TranType,
      (object) perUnitAggregatedTax.RefNbr
    })).AsEnumerable<PXResult<ARTax>>().Select<PXResult<ARTax>, (ARTax, ARTran)>((Func<PXResult<ARTax>, (ARTax, ARTran)>) (res => (((PXResult) res).GetItem<ARTax>(), ((PXResult) res).GetItem<ARTran>())));
  }

  protected override PX.Objects.GL.GLTran CreateGLTranForPerUnitLineTax(
    ARInvoice arInvoice,
    PX.Objects.CM.Extensions.CurrencyInfo newCurrencyInfo,
    ARTaxTran perUnitAggregatedTax,
    ARTran arTran,
    ARTax perUnitLineTax,
    bool isDebitTaxTran)
  {
    PX.Objects.GL.GLTran forPerUnitLineTax = base.CreateGLTranForPerUnitLineTax(arInvoice, newCurrencyInfo, perUnitAggregatedTax, arTran, perUnitLineTax, isDebitTaxTran);
    forPerUnitLineTax.SummPost = new bool?(this.Base.SummPost);
    forPerUnitLineTax.BranchID = arTran.BranchID;
    forPerUnitLineTax.AccountID = arTran.AccountID;
    forPerUnitLineTax.SubID = arTran.SubID;
    forPerUnitLineTax.TranLineNbr = arTran.LineNbr;
    forPerUnitLineTax.ReferenceID = arInvoice.CustomerID;
    forPerUnitLineTax.ProjectID = arTran.ProjectID;
    forPerUnitLineTax.TaskID = arTran.TaskID;
    forPerUnitLineTax.CostCodeID = arTran.CostCodeID;
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
    ARInvoice arInvoice,
    ARTran arTran,
    ARTaxTran perUnitAggregatedTax,
    ARTax perUnitLineTax,
    PX.Objects.GL.GLTran newGlTran)
  {
    ARReleaseProcess.GLTranInsertionContext context = new ARReleaseProcess.GLTranInsertionContext()
    {
      ARRegisterRecord = (ARRegister) arInvoice,
      ARTranRecord = arTran,
      ARTaxTranRecord = perUnitAggregatedTax
    };
    return this.Base.InsertInvoicePerUnitTaxAmountsToItemAccountsTransaction(journalEntry, newGlTran, context);
  }

  protected delegate void PostTaxDelegate(
    JournalEntry journalEntry,
    ARInvoice document,
    ARRegister doc,
    PX.Objects.TX.Tax perUnitTax,
    ARTaxTran perUnitAggregatedTax,
    PX.Objects.GL.Account taxAccount,
    PX.Objects.CM.Extensions.CurrencyInfo newCurrencyInfo,
    bool isDebit);
}
