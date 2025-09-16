// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.GraphExtensions.ARReleaseProcessExt.Correction
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.WorkflowAPI;
using PX.Objects.AR;
using PX.Objects.AR.Standalone;
using PX.Objects.GL;
using PX.Objects.IN;
using PX.Objects.IN.GraphExtensions.ARReleaseProcessExt;
using PX.Objects.PM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

#nullable disable
namespace PX.Objects.SO.GraphExtensions.ARReleaseProcessExt;

public class Correction : PXGraphExtension<ProcessInventory, ARReleaseProcess>
{
  /// <summary>
  /// Overrides <see cref="M:PX.Objects.AR.ARReleaseProcess.CloseInvoiceAndClearBalances(PX.Objects.AR.ARRegister)" />
  /// </summary>
  [PXOverride]
  public virtual void CloseInvoiceAndClearBalances(PX.Objects.AR.ARRegister ardoc, Action<PX.Objects.AR.ARRegister> baseMethod)
  {
    if (ardoc.IsUnderCorrection.GetValueOrDefault() && !((PXGraphExtension<ARReleaseProcess>) this).Base.IsIntegrityCheck)
      this.MarkInvoiceCanceled((PX.Objects.AR.ARInvoice) ardoc);
    baseMethod(ardoc);
  }

  /// <summary>
  /// Overrides <see cref="M:PX.Objects.AR.ARReleaseProcess.OpenInvoiceAndRecoverBalances(PX.Objects.AR.ARRegister)" />
  /// </summary>
  [PXOverride]
  public virtual void OpenInvoiceAndRecoverBalances(PX.Objects.AR.ARRegister ardoc, Action<PX.Objects.AR.ARRegister> baseMethod)
  {
    if (ardoc.IsUnderCorrection.GetValueOrDefault() && !((PXGraphExtension<ARReleaseProcess>) this).Base.IsIntegrityCheck)
      throw new PXException("The cancellation credit memo can be applied to the invoice {0} only in full.", new object[1]
      {
        (object) ardoc.RefNbr
      });
    baseMethod(ardoc);
  }

  /// <summary>
  /// Overrides <see cref="M:PX.Objects.IN.GraphExtensions.ARReleaseProcessExt.ProcessInventory.ProcessARTranInventory(PX.Objects.AR.ARTran,PX.Objects.AR.ARInvoice,PX.Objects.GL.JournalEntry)" />
  /// </summary>
  [PXOverride]
  public virtual void ProcessARTranInventory(
    PX.Objects.AR.ARTran n,
    PX.Objects.AR.ARInvoice ardoc,
    JournalEntry je,
    Action<PX.Objects.AR.ARTran, PX.Objects.AR.ARInvoice, JournalEntry> baseMethod)
  {
    if (ardoc.IsCancellation.GetValueOrDefault())
    {
      if (((PXGraphExtension<ARReleaseProcess>) this).Base.IsIntegrityCheck || n?.LineType == "DS")
        return;
      foreach (INTran intran in this.Base1.GetINTransBoundToARTran(n))
      {
        intran.ARDocType = (string) null;
        intran.ARRefNbr = (string) null;
        intran.ARLineNbr = new int?();
        GraphHelper.MarkUpdated(((PXSelectBase) this.Base1.intranselect).Cache, (object) intran, true);
        this.Base1.PostShippedNotInvoiced(intran, n, ardoc, je);
      }
      if (n.OrigInvoiceType == null)
        return;
      PX.Objects.AR.ARTran arTran = PX.Objects.AR.ARTran.PK.Find((PXGraph) ((PXGraphExtension<ARReleaseProcess>) this).Base, n.OrigInvoiceType, n.OrigInvoiceNbr, n.OrigInvoiceLineNbr, (PKFindOptions) 1);
      if (arTran == null || !arTran.InvtReleased.GetValueOrDefault())
        return;
      arTran.InvtReleased = new bool?(false);
      GraphHelper.MarkUpdated(((PXSelectBase) ((PXGraphExtension<ARReleaseProcess>) this).Base.ARTran_TranType_RefNbr).Cache, (object) arTran);
    }
    else
      baseMethod(n, ardoc, je);
  }

  [PXOverride]
  public virtual List<PX.Objects.AR.ARRegister> ReleaseInvoice(
    JournalEntry je,
    PX.Objects.AR.ARRegister doc,
    PXResult<PX.Objects.AR.ARInvoice, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CS.Terms, PX.Objects.AR.Customer, PX.Objects.GL.Account> res,
    List<PMRegister> pmDocs,
    Correction.ReleaseInvoiceDelegate baseMethod)
  {
    this.EnsureCanReleaseOrThrow(doc);
    PX.Objects.AR.ARInvoice arInvoice1 = PXResult<PX.Objects.AR.ARInvoice, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CS.Terms, PX.Objects.AR.Customer, PX.Objects.GL.Account>.op_Implicit(res);
    int num1;
    if (arInvoice1.IsCancellation.GetValueOrDefault())
    {
      Decimal? curyOrigDocAmt = arInvoice1.CuryOrigDocAmt;
      Decimal num2 = 0M;
      if (curyOrigDocAmt.GetValueOrDefault() == num2 & curyOrigDocAmt.HasValue)
      {
        num1 = !string.IsNullOrEmpty(arInvoice1.OrigRefNbr) ? 1 : 0;
        goto label_4;
      }
    }
    num1 = 0;
label_4:
    List<PX.Objects.AR.ARRegister> arRegisterList = baseMethod(je, doc, res, pmDocs);
    if (num1 != 0)
    {
      PX.Objects.AR.ARInvoice arInvoice2 = PXResultset<PX.Objects.AR.ARInvoice>.op_Implicit(PXSelectBase<PX.Objects.AR.ARInvoice, PXSelect<PX.Objects.AR.ARInvoice, Where<PX.Objects.AR.ARInvoice.docType, Equal<Required<PX.Objects.AR.ARInvoice.docType>>, And<PX.Objects.AR.ARInvoice.refNbr, Equal<Required<PX.Objects.AR.ARInvoice.refNbr>>>>>.Config>.Select((PXGraph) ((PXGraphExtension<ARReleaseProcess>) this).Base, new object[2]
      {
        (object) arInvoice1.OrigDocType,
        (object) arInvoice1.OrigRefNbr
      }));
      if (arInvoice2 != null && arInvoice2.IsUnderCorrection.GetValueOrDefault())
      {
        bool? canceled = arInvoice2.Canceled;
        bool flag = false;
        if (canceled.GetValueOrDefault() == flag & canceled.HasValue)
          this.MarkInvoiceCanceled(arInvoice2);
      }
    }
    return arRegisterList;
  }

  protected virtual void MarkInvoiceCanceled(PX.Objects.AR.ARInvoice arInvoice)
  {
    arInvoice.Canceled = new bool?(true);
    ((SelectedEntityEvent<PX.Objects.AR.ARInvoice>) PXEntityEventBase<PX.Objects.AR.ARInvoice>.Container<PX.Objects.AR.ARInvoice.Events>.Select((Expression<Func<PX.Objects.AR.ARInvoice.Events, PXEntityEvent<PX.Objects.AR.ARInvoice.Events>>>) (ev => ev.CancelDocument))).FireOn((PXGraph) ((PXGraphExtension<ARReleaseProcess>) this).Base, arInvoice);
    ((PXSelectBase<PX.Objects.AR.ARRegister>) ((PXGraphExtension<ARReleaseProcess>) this).Base.ARDocument).Update((PX.Objects.AR.ARRegister) arInvoice);
    foreach (PXResult<PX.Objects.AR.ARTran> pxResult in PXSelectBase<PX.Objects.AR.ARTran, PXSelect<PX.Objects.AR.ARTran, Where<PX.Objects.AR.ARTran.tranType, Equal<Required<PX.Objects.AR.ARTran.tranType>>, And<PX.Objects.AR.ARTran.refNbr, Equal<Required<PX.Objects.AR.ARTran.refNbr>>, And<PX.Objects.AR.ARTran.canceled, Equal<False>>>>>.Config>.Select((PXGraph) ((PXGraphExtension<ARReleaseProcess>) this).Base, new object[2]
    {
      (object) arInvoice.DocType,
      (object) arInvoice.RefNbr
    }))
    {
      PX.Objects.AR.ARTran arTran = PXResult<PX.Objects.AR.ARTran>.op_Implicit(pxResult);
      arTran.Canceled = new bool?(true);
      GraphHelper.Caches<PX.Objects.AR.ARTran>((PXGraph) ((PXGraphExtension<ARReleaseProcess>) this).Base).Update(arTran);
    }
  }

  protected virtual void EnsureCanReleaseOrThrow(PX.Objects.AR.ARRegister doc)
  {
    if (doc.DocType == "CRM" && !doc.IsCancellation.GetValueOrDefault())
    {
      ARAdjust2 arAdjust2 = PXResultset<ARAdjust2>.op_Implicit(PXSelectBase<ARAdjust2, PXViewOf<ARAdjust2>.BasedOn<SelectFromBase<ARAdjust2, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.AR.ARAdjust>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AR.ARAdjust.adjdRefNbr, Equal<ARAdjust2.adjdRefNbr>>>>>.And<BqlOperand<PX.Objects.AR.ARAdjust.adjdDocType, IBqlString>.IsEqual<ARAdjust2.adjdDocType>>>>, FbqlJoins.Inner<PX.Objects.AR.ARRegister>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AR.ARRegister.refNbr, Equal<ARAdjust2.adjgRefNbr>>>>>.And<BqlOperand<PX.Objects.AR.ARRegister.docType, IBqlString>.IsEqual<ARAdjust2.adjgDocType>>>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AR.ARAdjust.adjgDocType, Equal<BqlField<PX.Objects.AR.ARRegister.docType, IBqlString>.FromCurrent>>>>, And<BqlOperand<PX.Objects.AR.ARAdjust.adjgRefNbr, IBqlString>.IsEqual<BqlField<PX.Objects.AR.ARRegister.refNbr, IBqlString>.FromCurrent>>>, And<BqlOperand<PX.Objects.AR.ARAdjust.voided, IBqlBool>.IsNotEqual<True>>>, And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARAdjust2.adjgRefNbr, NotEqual<BqlField<PX.Objects.AR.ARRegister.refNbr, IBqlString>.FromCurrent>>>>>.Or<BqlOperand<ARAdjust2.adjgDocType, IBqlString>.IsNotEqual<BqlField<PX.Objects.AR.ARRegister.docType, IBqlString>.FromCurrent>>>>, And<BqlOperand<ARAdjust2.voided, IBqlBool>.IsNotEqual<True>>>, And<BqlOperand<PX.Objects.AR.ARRegister.docType, IBqlString>.IsEqual<ARDocType.creditMemo>>>>.And<BqlOperand<PX.Objects.AR.ARRegister.isCancellation, IBqlBool>.IsEqual<True>>>>.ReadOnly.Config>.SelectSingleBound((PXGraph) ((PXGraphExtension<ARReleaseProcess>) this).Base, (object[]) new PX.Objects.AR.ARRegister[1]
      {
        doc
      }, Array.Empty<object>()));
      if (arAdjust2 != null)
        throw new PXException("The application cannot be created because another cancellation invoice or correction invoice already exists for the invoice {0}.", new object[1]
        {
          (object) arAdjust2.AdjdRefNbr
        });
    }
    else
    {
      if (!doc.IsCancellation.GetValueOrDefault() || doc.OrigRefNbr == null || doc.OrigDocType == null)
        return;
      List<PX.Objects.AR.ARAdjust> list = GraphHelper.RowCast<PX.Objects.AR.ARAdjust>((IEnumerable) PXSelectBase<PX.Objects.AR.ARAdjust, PXViewOf<PX.Objects.AR.ARAdjust>.BasedOn<SelectFromBase<PX.Objects.AR.ARAdjust, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AR.ARAdjust.adjdDocType, Equal<BqlField<PX.Objects.AR.ARRegister.origDocType, IBqlString>.FromCurrent>>>>, And<BqlOperand<PX.Objects.AR.ARAdjust.adjdRefNbr, IBqlString>.IsEqual<BqlField<PX.Objects.AR.ARRegister.origRefNbr, IBqlString>.FromCurrent>>>, And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AR.ARAdjust.adjgDocType, NotEqual<BqlField<PX.Objects.AR.ARRegister.docType, IBqlString>.FromCurrent>>>>>.Or<BqlOperand<PX.Objects.AR.ARAdjust.adjgRefNbr, IBqlString>.IsNotEqual<BqlField<PX.Objects.AR.ARRegister.refNbr, IBqlString>.FromCurrent>>>>>.And<BqlOperand<PX.Objects.AR.ARAdjust.voided, IBqlBool>.IsNotEqual<True>>>.Aggregate<To<GroupBy<PX.Objects.AR.ARAdjust.adjgDocType>, GroupBy<PX.Objects.AR.ARAdjust.adjgRefNbr>, GroupBy<PX.Objects.AR.ARAdjust.released>, Sum<PX.Objects.AR.ARAdjust.curyAdjdAmt>>>>.ReadOnly.Config>.SelectMultiBound((PXGraph) ((PXGraphExtension<ARReleaseProcess>) this).Base, (object[]) new PX.Objects.AR.ARRegister[1]
      {
        doc
      }, Array.Empty<object>())).ToList<PX.Objects.AR.ARAdjust>();
      if (list.Any<PX.Objects.AR.ARAdjust>((Func<PX.Objects.AR.ARAdjust, bool>) (a =>
      {
        bool? released = a.Released;
        bool flag = false;
        return released.GetValueOrDefault() == flag & released.HasValue;
      })))
        throw new PXException("The invoice cannot be canceled because it has unreleased applications.");
      PX.Objects.AR.ARAdjust arAdjust1 = list.FirstOrDefault<PX.Objects.AR.ARAdjust>((Func<PX.Objects.AR.ARAdjust, bool>) (a =>
      {
        Decimal? curyAdjdAmt = a.CuryAdjdAmt;
        Decimal num = 0M;
        return !(curyAdjdAmt.GetValueOrDefault() == num & curyAdjdAmt.HasValue) && a.AdjgDocType == "CRM";
      }));
      if (arAdjust1 != null)
        throw new PXException("The invoice {0} cannot be canceled because it has the credit memo {1} applied. Reverse the credit memo application before you cancel the invoice.", new object[2]
        {
          (object) arAdjust1.AdjdRefNbr,
          (object) arAdjust1.AdjgRefNbr
        });
      PX.Objects.AR.ARAdjust arAdjust2 = list.FirstOrDefault<PX.Objects.AR.ARAdjust>((Func<PX.Objects.AR.ARAdjust, bool>) (a =>
      {
        Decimal? curyAdjdAmt = a.CuryAdjdAmt;
        Decimal num = 0M;
        return !(curyAdjdAmt.GetValueOrDefault() == num & curyAdjdAmt.HasValue);
      }));
      if (arAdjust2 != null)
        throw new PXException("The invoice {0} cannot be canceled because the payment {1} is applied. Reverse the payment application before you cancel the invoice.", new object[2]
        {
          (object) arAdjust2.AdjdRefNbr,
          (object) arAdjust2.AdjgRefNbr
        });
      (PX.Objects.AR.ARInvoice arInvoice, ARRegisterAlias arRegisterAlias) = this.ReadOriginalInvoiceAndCorrectionNbr(doc.OrigDocType, doc.OrigRefNbr);
      Decimal? curyOrigDocAmt1 = arInvoice.CuryOrigDocAmt;
      Decimal? curyOrigDocAmt2 = doc.CuryOrigDocAmt;
      if (curyOrigDocAmt1.GetValueOrDefault() == curyOrigDocAmt2.GetValueOrDefault() & curyOrigDocAmt1.HasValue == curyOrigDocAmt2.HasValue)
        return;
      bool? isTaxSaved1 = arInvoice.IsTaxSaved;
      bool? isTaxSaved2 = doc.IsTaxSaved;
      if (!(isTaxSaved1.GetValueOrDefault() == isTaxSaved2.GetValueOrDefault() & isTaxSaved1.HasValue == isTaxSaved2.HasValue))
        throw new PXException("The cancellation credit memo cannot be released because its balance differs from the balance of the {0} original invoice due to the calculation of external taxes. Make sure that the External Tax Provider check box on the Tax Zones (TX206000) form is in the same state as it was when the original invoice was created and try to cancel the original invoice again.", new object[1]
        {
          (object) doc.OrigRefNbr
        });
      if (arRegisterAlias.RefNbr != null)
        throw new PXException("The correction invoice cannot be released because the balance of the generated cancellation credit memo will differ from the balance of the {0} original invoice. Delete the correction invoice and try to cancel the original invoice instead.", new object[1]
        {
          (object) doc.OrigRefNbr
        });
      throw new PXException("The {0} cancellation credit memo cannot be released because its balance differs from the balance of the {1} original invoice. The discrepancy may appear due to altered taxes, discounts, or freight recalculation. Delete the cancellation credit memo and create it again.", new object[2]
      {
        (object) doc.RefNbr,
        (object) doc.OrigRefNbr
      });
    }
  }

  protected virtual (PX.Objects.AR.ARInvoice, ARRegisterAlias) ReadOriginalInvoiceAndCorrectionNbr(
    string origDocType,
    string origRefNbr)
  {
    PXView view = ((PXSelectBase) new PXViewOf<PX.Objects.AR.ARInvoice>.BasedOn<SelectFromBase<PX.Objects.AR.ARInvoice, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Left<ARRegisterAlias>.On<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARRegisterAlias.origDocType, Equal<PX.Objects.AR.ARInvoice.docType>>>>, And<BqlOperand<ARRegisterAlias.origRefNbr, IBqlString>.IsEqual<PX.Objects.AR.ARInvoice.refNbr>>>>.And<BqlOperand<ARRegisterAlias.isCorrection, IBqlBool>.IsEqual<True>>>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AR.ARInvoice.docType, Equal<P.AsString.ASCII>>>>, And<BqlOperand<PX.Objects.AR.ARInvoice.refNbr, IBqlString>.IsEqual<P.AsString>>>>.And<BqlOperand<PX.Objects.AR.ARRegister.isUnderCorrection, IBqlBool>.IsEqual<True>>>>.ReadOnly((PXGraph) ((PXGraphExtension<ARReleaseProcess>) this).Base)).View;
    using (new PXFieldScope(view, new Type[3]
    {
      typeof (PX.Objects.AR.ARInvoice),
      typeof (ARRegisterAlias.docType),
      typeof (ARRegisterAlias.refNbr)
    }))
    {
      PXResult<PX.Objects.AR.ARInvoice> pxResult = (PXResult<PX.Objects.AR.ARInvoice>) view.SelectSingleBound(Array.Empty<object>(), new object[2]
      {
        (object) origDocType,
        (object) origRefNbr
      });
      return (PXResult<PX.Objects.AR.ARInvoice>.op_Implicit(pxResult), ((PXResult) pxResult).GetItem<ARRegisterAlias>());
    }
  }

  public delegate List<PX.Objects.AR.ARRegister> ReleaseInvoiceDelegate(
    JournalEntry je,
    PX.Objects.AR.ARRegister doc,
    PXResult<PX.Objects.AR.ARInvoice, PX.Objects.CM.Extensions.CurrencyInfo, PX.Objects.CS.Terms, PX.Objects.AR.Customer, PX.Objects.GL.Account> res,
    List<PMRegister> pmDocs);
}
