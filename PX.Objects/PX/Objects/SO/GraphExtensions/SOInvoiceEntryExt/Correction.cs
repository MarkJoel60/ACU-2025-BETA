// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.GraphExtensions.SOInvoiceEntryExt.Correction
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AR;
using PX.Objects.AR.Standalone;
using PX.Objects.Common.Exceptions;
using PX.Objects.CS;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.SO.GraphExtensions.SOInvoiceEntryExt;

public class Correction : PXGraphExtension<SOInvoiceEntry>
{
  private bool CancellationInvoiceCreationOnRelease;
  public PXAction<PX.Objects.AR.ARInvoice> cancelInvoice;
  public PXAction<PX.Objects.AR.ARInvoice> reverseDirectInvoice;
  public PXAction<PX.Objects.AR.ARInvoice> correctInvoice;

  [PXUIField]
  [PXButton]
  [ARMigrationModeDependentActionRestriction(true, true, true)]
  public virtual IEnumerable CancelInvoice(PXAdapter adapter)
  {
    return this.CancelSOInvoice(adapter, false);
  }

  [PXUIField]
  [PXButton]
  [ARMigrationModeDependentActionRestriction(true, true, true)]
  public virtual IEnumerable ReverseDirectInvoice(PXAdapter adapter)
  {
    return this.CancelSOInvoice(adapter, true);
  }

  [PXUIField]
  [PXButton]
  [ARMigrationModeDependentActionRestriction(true, true, true)]
  public virtual IEnumerable CorrectInvoice(PXAdapter adapter)
  {
    if (((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Base.Document).Current == null)
      return adapter.Get();
    GraphHelper.MarkUpdated(((PXSelectBase) this.Base.Document).Cache, (object) ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Base.Document).Current);
    ((PXAction) this.Base.Save).Press();
    this.EnsureCanCancel(((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Base.Document).Current, true, false);
    return this.Base.ReverseDocumentAndApplyToReversalIfNeeded(adapter, new ReverseInvoiceArgs()
    {
      ApplyToOriginalDocument = false,
      PreserveOriginalDocumentSign = true,
      DateOption = ReverseInvoiceArgs.CopyOption.SetDefault,
      CurrencyRateOption = ReverseInvoiceArgs.CopyOption.SetDefault
    });
  }

  protected virtual void SetupCorrectActionsState(
    PX.Data.Events.RowSelected<PX.Objects.AR.ARInvoice> e,
    bool isVisible,
    bool isEnabled)
  {
    ((PXAction) this.cancelInvoice).SetEnabled(isEnabled);
    ((PXAction) this.correctInvoice).SetEnabled(isEnabled);
    ((PXAction) this.reverseDirectInvoice).SetEnabled(isEnabled);
    ((PXGraph) this.Base).Actions["Corrections Category"]?.SetVisible("CancelInvoice", isVisible);
    ((PXGraph) this.Base).Actions["Corrections Category"]?.SetVisible("CorrectInvoice", isVisible);
    ((PXGraph) this.Base).Actions["Corrections Category"]?.SetVisible("ReverseDirectInvoice", false);
  }

  protected virtual void _(PX.Data.Events.RowSelected<PX.Objects.AR.ARInvoice> e)
  {
    int num;
    if (e.Row?.DocType == "INV")
    {
      bool? nullable = e.Row.Released;
      if (nullable.GetValueOrDefault())
      {
        nullable = e.Row.IsUnderCorrection;
        bool flag = false;
        num = nullable.GetValueOrDefault() == flag & nullable.HasValue ? 1 : 0;
        goto label_4;
      }
    }
    num = 0;
label_4:
    bool isEnabled = num != 0;
    this.SetupCorrectActionsState(e, e.Row?.DocType == "INV", isEnabled);
  }

  [Obsolete("Event handler is kept to avoid breaking changes.")]
  protected virtual void _(PX.Data.Events.RowPersisted<PX.Objects.AR.ARInvoice> e)
  {
  }

  [PXOverride]
  public void Persist(Action basePersist)
  {
    foreach (PX.Objects.AR.ARInvoice arInvoice in NonGenericIEnumerableExtensions.Concat_(((PXSelectBase) this.Base.Document).Cache.Inserted, ((PXSelectBase) this.Base.Document).Cache.Deleted))
    {
      PXEntryStatus status = ((PXSelectBase) this.Base.Document).Cache.GetStatus((object) arInvoice);
      bool flag1 = status == 2;
      bool flag2 = status == 3;
      if (flag1 | flag2)
      {
        bool? nullable = arInvoice.IsCorrection;
        if (!nullable.GetValueOrDefault())
        {
          nullable = arInvoice.IsCancellation;
          if (!nullable.GetValueOrDefault())
            continue;
        }
        if (arInvoice.OrigDocType != null && arInvoice.OrigRefNbr != null && !this.CancellationInvoiceCreationOnRelease)
        {
          PXCache<ARInvoiceAdjusted> pxCache = GraphHelper.Caches<ARInvoiceAdjusted>((PXGraph) this.Base);
          ARInvoiceAdjusted arInvoiceAdjusted = PXResultset<ARInvoiceAdjusted>.op_Implicit(PXSelectBase<ARInvoiceAdjusted, PXViewOf<ARInvoiceAdjusted>.BasedOn<SelectFromBase<ARInvoiceAdjusted, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ARInvoiceAdjusted.docType, Equal<BqlField<PX.Objects.AR.ARInvoice.origDocType, IBqlString>.FromCurrent>>>>>.And<BqlOperand<ARInvoiceAdjusted.refNbr, IBqlString>.IsEqual<BqlField<PX.Objects.AR.ARInvoice.origRefNbr, IBqlString>.FromCurrent>>>>.Config>.SelectSingleBound((PXGraph) this.Base, new object[1]
          {
            (object) arInvoice
          }, Array.Empty<object>()));
          nullable = (bool?) ((PXCache) pxCache).GetValueOriginal<ARInvoiceAdjusted.isUnderCorrection>((object) arInvoiceAdjusted);
          bool flag3 = flag1;
          if (nullable.GetValueOrDefault() == flag3 & nullable.HasValue)
            throw new PXLockViolationException(typeof (PX.Objects.AR.ARInvoice), (PXDBOperation) 1, (object[]) new string[2]
            {
              arInvoice.OrigDocType,
              arInvoice.OrigRefNbr
            });
          arInvoiceAdjusted.IsUnderCorrection = new bool?(flag1);
          pxCache.Update(arInvoiceAdjusted);
        }
      }
    }
    basePersist();
  }

  protected virtual IEnumerable CancelSOInvoice(PXAdapter adapter, bool allowDirectSales)
  {
    if (((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Base.Document).Current == null)
      return adapter.Get();
    GraphHelper.MarkUpdated(((PXSelectBase) this.Base.Document).Cache, (object) ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Base.Document).Current);
    ((PXAction) this.Base.Save).Press();
    this.EnsureCanCancel(((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Base.Document).Current, false, allowDirectSales);
    ReverseInvoiceArgs reverseArgs = this.PrepareReverseInvoiceArgs(allowDirectSales);
    return this.Base.ReverseDocumentAndApplyToReversalIfNeeded(adapter, reverseArgs);
  }

  protected virtual ReverseInvoiceArgs PrepareReverseInvoiceArgs(bool reverseINTransaction)
  {
    ReverseInvoiceArgs reverseInvoiceArgs = new ReverseInvoiceArgs()
    {
      ApplyToOriginalDocument = true,
      ReverseINTransaction = new bool?(reverseINTransaction)
    };
    if (this.CancellationInvoiceCreationOnRelease)
    {
      PX.Objects.AR.ARInvoice arInvoice = PXResultset<PX.Objects.AR.ARInvoice>.op_Implicit(PXSelectBase<PX.Objects.AR.ARInvoice, PXViewOf<PX.Objects.AR.ARInvoice>.BasedOn<SelectFromBase<PX.Objects.AR.ARInvoice, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AR.ARInvoice.origDocType, Equal<BqlField<PX.Objects.AR.ARInvoice.docType, IBqlString>.FromCurrent>>>>, And<BqlOperand<PX.Objects.AR.ARInvoice.origRefNbr, IBqlString>.IsEqual<BqlField<PX.Objects.AR.ARInvoice.refNbr, IBqlString>.FromCurrent>>>>.And<BqlOperand<PX.Objects.AR.ARRegister.isCorrection, IBqlBool>.IsEqual<True>>>>.ReadOnly.Config>.Select((PXGraph) this.Base, Array.Empty<object>()));
      if (arInvoice == null)
        throw new RowNotFoundException(((PXSelectBase) this.Base.Document).Cache, new object[2]
        {
          (object) ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Base.Document).Current.DocType,
          (object) ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Base.Document).Current.RefNbr
        });
      if ((string.CompareOrdinal(((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Base.Document).Current.FinPeriodID, arInvoice.FinPeriodID) > 0 ? 1 : (DateTime.Compare(((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Base.Document).Current.DocDate.Value, arInvoice.DocDate.Value) > 0 ? 1 : 0)) != 0)
      {
        reverseInvoiceArgs.DateOption = ReverseInvoiceArgs.CopyOption.SetOriginal;
      }
      else
      {
        reverseInvoiceArgs.DateOption = ReverseInvoiceArgs.CopyOption.Override;
        reverseInvoiceArgs.DocumentDate = arInvoice.DocDate;
        reverseInvoiceArgs.DocumentFinPeriodID = arInvoice.FinPeriodID;
      }
      reverseInvoiceArgs.CurrencyRateOption = ReverseInvoiceArgs.CopyOption.SetDefault;
      reverseInvoiceArgs.OverrideDocumentHold = new bool?(false);
      using (new PXLocaleScope(((PXSelectBase<PX.Objects.AR.Customer>) this.Base.customer).Current.LocaleName))
        reverseInvoiceArgs.OverrideDocumentDescr = PXMessages.LocalizeFormatNoPrefixNLA("Correction of the {0} invoice.", new object[1]
        {
          (object) ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Base.Document).Current.RefNbr
        });
    }
    return reverseInvoiceArgs;
  }

  public virtual void EnsureCanCancel(PX.Objects.AR.ARInvoice doc, bool isCorrection, bool allowDirectSales)
  {
    short? nullable1 = !(doc.DocType != "INV") ? doc.InstallmentCntr : throw new PXException("The document of the {0} type cannot be canceled.", new object[1]
    {
      (object) doc.DocType
    });
    int? nullable2 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
    int num1 = 0;
    if (nullable2.GetValueOrDefault() > num1 & nullable2.HasValue)
      throw new PXException("The invoice cannot be canceled because credit terms with multiple installments is specified.");
    List<PX.Objects.AR.ARAdjust> list = GraphHelper.RowCast<PX.Objects.AR.ARAdjust>((IEnumerable) PXSelectBase<PX.Objects.AR.ARAdjust, PXSelectGroupBy<PX.Objects.AR.ARAdjust, Where<PX.Objects.AR.ARAdjust.adjdDocType, Equal<Current<PX.Objects.AR.ARInvoice.docType>>, And<PX.Objects.AR.ARAdjust.adjdRefNbr, Equal<Current<PX.Objects.AR.ARInvoice.refNbr>>, And<PX.Objects.AR.ARAdjust.voided, NotEqual<True>>>>, Aggregate<GroupBy<PX.Objects.AR.ARAdjust.adjgDocType, GroupBy<PX.Objects.AR.ARAdjust.adjgRefNbr, GroupBy<PX.Objects.AR.ARAdjust.released, Sum<PX.Objects.AR.ARAdjust.curyAdjdAmt>>>>>>.Config>.SelectMultiBound((PXGraph) this.Base, (object[]) new PX.Objects.AR.ARInvoice[1]
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
      Decimal num2 = 0M;
      return !(curyAdjdAmt.GetValueOrDefault() == num2 & curyAdjdAmt.HasValue) && a.AdjgDocType == "CRM";
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
      Decimal num3 = 0M;
      return !(curyAdjdAmt.GetValueOrDefault() == num3 & curyAdjdAmt.HasValue);
    }));
    if (arAdjust2 != null)
      throw new PXException(allowDirectSales ? "The {0} invoice cannot be reversed because the {1} payment has been applied. Reverse the payment application before reversing the invoice." : "The invoice {0} cannot be canceled because the payment {1} is applied. Reverse the payment application before you cancel the invoice.", new object[2]
      {
        (object) arAdjust2.AdjdRefNbr,
        (object) arAdjust2.AdjgRefNbr
      });
    if (!allowDirectSales)
    {
      if (PXResultset<ARTran>.op_Implicit(PXSelectBase<ARTran, PXSelectReadonly<ARTran, Where<ARTran.tranType, Equal<Current<PX.Objects.AR.ARInvoice.docType>>, And<ARTran.refNbr, Equal<Current<PX.Objects.AR.ARInvoice.refNbr>>, And<ARTran.invtMult, NotEqual<short0>, And<ARTran.lineType, Equal<SOLineType.inventory>>>>>>.Config>.SelectSingleBound((PXGraph) this.Base, (object[]) new PX.Objects.AR.ARInvoice[1]
      {
        doc
      }, Array.Empty<object>())) != null)
        throw new PXException("The invoice {0} cannot be canceled because it contains direct sale lines with stock items. Process a direct return to cancel the direct sale.", new object[1]
        {
          (object) doc.RefNbr
        });
    }
    PX.Objects.SO.SOOrder soOrder = PXResultset<PX.Objects.SO.SOOrderShipment>.op_Implicit(PXSelectBase<PX.Objects.SO.SOOrderShipment, PXSelectReadonly<PX.Objects.SO.SOOrderShipment, Where<PX.Objects.SO.SOOrderShipment.invoiceType, Equal<Current<PX.Objects.AR.ARInvoice.docType>>, And<PX.Objects.SO.SOOrderShipment.invoiceNbr, Equal<Current<PX.Objects.AR.ARInvoice.refNbr>>, And<PX.Objects.SO.SOOrderShipment.shipmentNbr, Equal<PX.Objects.SO.Constants.noShipmentNbr>>>>>.Config>.SelectSingleBound((PXGraph) this.Base, (object[]) new PX.Objects.AR.ARInvoice[1]
    {
      doc
    }, Array.Empty<object>())) == null ? PXResultset<PX.Objects.SO.SOOrder>.op_Implicit(PXSelectBase<PX.Objects.SO.SOOrder, PXViewOf<PX.Objects.SO.SOOrder>.BasedOn<SelectFromBase<PX.Objects.SO.SOOrder, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<SOLine>.On<SOLine.FK.Order>>>.Where<KeysRelation<CompositeKey<Field<SOLine.invoiceType>.IsRelatedTo<PX.Objects.SO.SOInvoice.docType>, Field<SOLine.invoiceNbr>.IsRelatedTo<PX.Objects.SO.SOInvoice.refNbr>>.WithTablesOf<PX.Objects.SO.SOInvoice, SOLine>, PX.Objects.SO.SOInvoice, SOLine>.SameAsCurrent.And<BqlOperand<PX.Objects.SO.SOOrder.behavior, IBqlString>.IsEqual<SOBehavior.rM>>>>.Config>.SelectSingleBound((PXGraph) this.Base, (object[]) new PX.Objects.AR.ARInvoice[1]
    {
      doc
    }, Array.Empty<object>())) : throw new PXException("The invoice {0} cannot be canceled because it includes line or lines linked to an order that does not require shipping.", new object[1]
    {
      (object) doc.RefNbr
    });
    if (soOrder != null)
      throw new PXException("The {0} invoice cannot be canceled or corrected because one or multiple lines from the {0} invoice have been added to the {1} sales order of the {2} type.", new object[3]
      {
        (object) doc.RefNbr,
        (object) soOrder.OrderNbr,
        (object) soOrder.OrderType
      });
  }

  [PXOverride]
  public virtual bool AskUserApprovalIfReversingDocumentAlreadyExists(
    PX.Objects.AR.ARInvoice origDoc,
    Func<PX.Objects.AR.ARInvoice, bool> baseImpl)
  {
    return this.CancellationInvoiceCreationOnRelease || baseImpl(origDoc);
  }

  [PXOverride]
  public virtual PX.Objects.AR.ARInvoice CreateReversalARInvoice(
    PX.Objects.AR.ARInvoice doc,
    ReverseInvoiceArgs reverseArgs,
    Func<PX.Objects.AR.ARInvoice, ReverseInvoiceArgs, PX.Objects.AR.ARInvoice> baseMethod)
  {
    PX.Objects.AR.ARInvoice reversalArInvoice = baseMethod(doc, reverseArgs);
    if (reverseArgs.PreserveOriginalDocumentSign)
    {
      reversalArInvoice.IsCorrection = new bool?(true);
    }
    else
    {
      reversalArInvoice.IsCancellation = new bool?(true);
      if (this.CancellationInvoiceCreationOnRelease)
      {
        reversalArInvoice.DontPrint = new bool?(true);
        reversalArInvoice.DontEmail = new bool?(true);
      }
      else
      {
        reversalArInvoice.DontPrint = new bool?();
        reversalArInvoice.DontEmail = new bool?();
      }
    }
    return reversalArInvoice;
  }

  /// Overrides <see cref="M:PX.Objects.AR.ARInvoiceEntry.setDontApproveValue(PX.Objects.AR.ARInvoice,PX.Data.PXCache)" />
  [PXOverride]
  public void setDontApproveValue(
    PX.Objects.AR.ARInvoice doc,
    PXCache cache,
    Action<PX.Objects.AR.ARInvoice, PXCache> base_setDontApproveValue)
  {
    if (this.CancellationInvoiceCreationOnRelease && doc.IsCancellation.GetValueOrDefault())
    {
      cache.SetValue<PX.Objects.AR.ARRegister.dontApprove>((object) doc, (object) true);
      cache.SetValue<PX.Objects.AR.ARRegister.approved>((object) doc, (object) true);
    }
    else
      base_setDontApproveValue(doc, cache);
  }

  [PXOverride]
  public virtual ARTran CreateReversalARTran(
    ARTran srcTran,
    ReverseInvoiceArgs reverseArgs,
    Func<ARTran, ReverseInvoiceArgs, ARTran> baseMethod)
  {
    if (srcTran.LineType == "FR")
      return (ARTran) null;
    ARTran reversalArTran = baseMethod(srcTran, reverseArgs);
    reversalArTran.OrigInvoiceType = srcTran.TranType;
    reversalArTran.OrigInvoiceNbr = srcTran.RefNbr;
    reversalArTran.OrigInvoiceLineNbr = srcTran.LineNbr;
    if (!reverseArgs.PreserveOriginalDocumentSign)
    {
      reversalArTran.TranCost = srcTran.TranCost;
      reversalArTran.TranCostOrig = srcTran.TranCostOrig;
      reversalArTran.IsTranCostFinal = srcTran.IsTranCostFinal;
    }
    return reversalArTran;
  }

  [PXOverride]
  public virtual void InsertReversedTransactionDetails(
    PX.Objects.AR.ARRegister doc,
    ReverseInvoiceArgs reverseArgs,
    Dictionary<int?, int?> origLineNbrsDict,
    Action<PX.Objects.AR.ARRegister, ReverseInvoiceArgs, Dictionary<int?, int?>> baseMethod)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    Correction.\u003C\u003Ec__DisplayClass18_0 cDisplayClass180 = new Correction.\u003C\u003Ec__DisplayClass18_0();
    baseMethod(doc, reverseArgs, origLineNbrsDict);
    // ISSUE: reference to a compiler-generated field
    cDisplayClass180.SetLinkToOrigFreightTran = (Action<ARTran, string, string, int?>) ((row, origInvoiceType, origInvoiceNbr, origInvoiceLineNbr) =>
    {
      row.OrigInvoiceType = origInvoiceType;
      row.OrigInvoiceNbr = origInvoiceNbr;
      row.OrigInvoiceLineNbr = origInvoiceLineNbr;
    });
    foreach (SOFreightDetail fd in ((PXSelectBase) this.Base.FreightDetails).View.SelectMultiBound((object[]) new PX.Objects.AR.ARInvoice[1]
    {
      (PX.Objects.AR.ARInvoice) doc
    }, Array.Empty<object>()))
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      Correction.\u003C\u003Ec__DisplayClass18_1 cDisplayClass181 = new Correction.\u003C\u003Ec__DisplayClass18_1();
      // ISSUE: reference to a compiler-generated field
      cDisplayClass181.CS\u0024\u003C\u003E8__locals1 = cDisplayClass180;
      SOFreightDetail copy = PXCache<SOFreightDetail>.CreateCopy(fd);
      ARTran freightTran = this.Base.GetFreightTran(doc.DocType, doc.RefNbr, fd);
      // ISSUE: reference to a compiler-generated field
      cDisplayClass181.origInvoiceType = freightTran?.TranType;
      // ISSUE: reference to a compiler-generated field
      cDisplayClass181.origInvoiceNbr = freightTran?.RefNbr;
      // ISSUE: reference to a compiler-generated field
      cDisplayClass181.origInvoiceLineNbr = (int?) freightTran?.LineNbr;
      copy.DocType = (string) null;
      copy.RefNbr = (string) null;
      copy.CuryInfoID = new long?();
      copy.NoteID = new Guid?();
      try
      {
        // ISSUE: method pointer
        ((PXGraph) this.Base).RowInserted.AddHandler<ARTran>(new PXRowInserted((object) cDisplayClass181, __methodptr(\u003CInsertReversedTransactionDetails\u003Eb__1)));
        ((PXSelectBase<SOFreightDetail>) this.Base.FreightDetails).Insert(copy);
      }
      finally
      {
        // ISSUE: method pointer
        ((PXGraph) this.Base).RowInserted.RemoveHandler<ARTran>(new PXRowInserted((object) cDisplayClass181, __methodptr(\u003CInsertReversedTransactionDetails\u003Eb__2)));
      }
    }
  }

  [PXOverride]
  public virtual void ReverseInvoiceProc(
    PX.Objects.AR.ARRegister doc,
    ReverseInvoiceArgs reverseArgs,
    Action<PX.Objects.AR.ARRegister, ReverseInvoiceArgs> baseMethod)
  {
    baseMethod(doc, reverseArgs);
  }

  [PXOverride]
  public virtual ARInvoiceState GetDocumentState(
    PXCache cache,
    PX.Objects.AR.ARInvoice doc,
    Func<PXCache, PX.Objects.AR.ARInvoice, ARInvoiceState> baseMethod)
  {
    ARInvoiceState documentState = baseMethod(cache, doc);
    documentState.IsCancellationDocument = doc.IsCancellation.GetValueOrDefault();
    documentState.IsCorrectionDocument = doc.IsCorrection.GetValueOrDefault();
    documentState.ShouldDisableHeader |= documentState.IsCancellationDocument;
    documentState.IsTaxZoneIDEnabled &= !documentState.IsCancellationDocument;
    documentState.IsAvalaraCustomerUsageTypeEnabled &= !documentState.IsCancellationDocument;
    documentState.IsAssignmentEnabled &= !documentState.IsCancellationDocument;
    ARInvoiceState arInvoiceState1 = documentState;
    arInvoiceState1.AllowDeleteDocument = ((arInvoiceState1.AllowDeleteDocument ? 1 : 0) | (!documentState.IsCancellationDocument ? 0 : (!documentState.IsDocumentReleased ? 1 : 0))) != 0;
    ARInvoiceState arInvoiceState2 = documentState;
    arInvoiceState2.DocumentHoldEnabled = ((arInvoiceState2.DocumentHoldEnabled ? 1 : 0) | (!documentState.IsCancellationDocument ? 0 : (!documentState.IsDocumentReleased ? 1 : 0))) != 0;
    ARInvoiceState arInvoiceState3 = documentState;
    arInvoiceState3.DocumentDateEnabled = ((arInvoiceState3.DocumentDateEnabled ? 1 : 0) | (!documentState.IsCancellationDocument ? 0 : (!documentState.IsDocumentReleased ? 1 : 0))) != 0;
    ARInvoiceState arInvoiceState4 = documentState;
    arInvoiceState4.DocumentDescrEnabled = ((arInvoiceState4.DocumentDescrEnabled ? 1 : 0) | (!documentState.IsCancellationDocument ? 0 : (!documentState.IsDocumentReleased ? 1 : 0))) != 0;
    ARInvoiceState arInvoiceState5 = documentState;
    arInvoiceState5.BalanceBaseCalc = ((arInvoiceState5.BalanceBaseCalc ? 1 : 0) | (!documentState.IsCancellationDocument ? 0 : (!documentState.IsDocumentReleased ? 1 : 0))) != 0;
    ARInvoiceState arInvoiceState6 = documentState;
    arInvoiceState6.AllowDeleteTransactions = ((arInvoiceState6.AllowDeleteTransactions ? 1 : 0) & (documentState.IsCancellationDocument ? 0 : (!documentState.IsCorrectionDocument ? 1 : 0))) != 0;
    documentState.AllowUpdateTransactions &= !documentState.IsCancellationDocument;
    ARInvoiceState arInvoiceState7 = documentState;
    arInvoiceState7.AllowInsertTransactions = ((arInvoiceState7.AllowInsertTransactions ? 1 : 0) & (documentState.IsCancellationDocument ? 0 : (!documentState.IsCorrectionDocument ? 1 : 0))) != 0;
    documentState.AllowDeleteTaxes &= !documentState.IsCancellationDocument;
    documentState.AllowUpdateTaxes &= !documentState.IsCancellationDocument;
    documentState.AllowInsertTaxes &= !documentState.IsCancellationDocument;
    documentState.AllowDeleteDiscounts &= !documentState.IsCancellationDocument;
    documentState.AllowUpdateDiscounts &= !documentState.IsCancellationDocument;
    documentState.AllowInsertDiscounts &= !documentState.IsCancellationDocument;
    documentState.AllowUpdateCMAdjustments &= !documentState.IsCancellationDocument;
    return documentState;
  }

  /// <summary>
  /// Overrides <see cref="M:PX.Objects.SO.SOInvoiceEntry.ReleaseInvoiceProc(System.Collections.Generic.List{PX.Objects.AR.ARRegister},System.Boolean)" />
  /// </summary>
  [PXOverride]
  public virtual void ReleaseInvoiceProc(
    List<PX.Objects.AR.ARRegister> list,
    bool isMassProcess,
    Action<List<PX.Objects.AR.ARRegister>, bool> baseMethod)
  {
    list = list.Select<PX.Objects.AR.ARRegister, PX.Objects.AR.ARRegister>((Func<PX.Objects.AR.ARRegister, int, PX.Objects.AR.ARRegister>) ((doc, index) => !this.CreateAndReleaseCancellationInvoice(doc, index, isMassProcess) ? (PX.Objects.AR.ARRegister) null : doc)).ToList<PX.Objects.AR.ARRegister>();
    list = list.Select<PX.Objects.AR.ARRegister, PX.Objects.AR.ARRegister>((Func<PX.Objects.AR.ARRegister, int, PX.Objects.AR.ARRegister>) ((doc, index) => !this.ReleaseOnSeparateTransaction(doc, index, isMassProcess) ? doc : (PX.Objects.AR.ARRegister) null)).ToList<PX.Objects.AR.ARRegister>();
    if (!list.Any<PX.Objects.AR.ARRegister>((Func<PX.Objects.AR.ARRegister, bool>) (x => x != null)))
      return;
    baseMethod(list, isMassProcess);
  }

  protected virtual bool CreateAndReleaseCancellationInvoice(
    PX.Objects.AR.ARRegister doc,
    int index,
    bool isMassProcess)
  {
    bool? nullable;
    int num;
    if (doc == null)
    {
      num = 1;
    }
    else
    {
      nullable = doc.IsCorrection;
      num = !nullable.GetValueOrDefault() ? 1 : 0;
    }
    if (num != 0)
      return true;
    try
    {
      SOInvoiceEntry instance = PXGraph.CreateInstance<SOInvoiceEntry>();
      PX.Objects.AR.ARInvoice arInvoice = PXResultset<PX.Objects.AR.ARInvoice>.op_Implicit(PXSelectBase<PX.Objects.AR.ARInvoice, PXSelect<PX.Objects.AR.ARInvoice, Where<PX.Objects.AR.ARInvoice.origDocType, Equal<Current<PX.Objects.AR.ARInvoice.origDocType>>, And<PX.Objects.AR.ARInvoice.origRefNbr, Equal<Current<PX.Objects.AR.ARInvoice.origRefNbr>>, And<PX.Objects.AR.ARRegister.isCancellation, Equal<True>>>>>.Config>.SelectSingleBound((PXGraph) instance, (object[]) new PX.Objects.AR.ARRegister[1]
      {
        doc
      }, Array.Empty<object>()));
      if (arInvoice != null)
      {
        nullable = arInvoice.Released;
        if (!nullable.GetValueOrDefault())
          throw new PXException("The cancellation invoice cannot be created because another cancellation invoice {0} already exists for the invoice {1}.", new object[2]
          {
            (object) arInvoice.RefNbr,
            (object) doc.RefNbr
          });
        return true;
      }
      ((PXGraph) instance).GetExtension<Correction>().CancellationInvoiceCreationOnRelease = true;
      ((PXSelectBase<PX.Objects.AR.ARInvoice>) instance.Document).Current = PXResultset<PX.Objects.AR.ARInvoice>.op_Implicit(((PXSelectBase<PX.Objects.AR.ARInvoice>) instance.Document).Search<PX.Objects.AR.ARInvoice.refNbr>((object) doc.OrigRefNbr, new object[1]
      {
        (object) doc.OrigDocType
      }));
      ((PXGraph) instance).Actions["cancelInvoice"].Press();
      using (PXTransactionScope transactionScope = new PXTransactionScope())
      {
        ((PXAction) instance.Save).Press();
        instance.ReleaseInvoiceProcImpl(new List<PX.Objects.AR.ARRegister>()
        {
          (PX.Objects.AR.ARRegister) ((PXSelectBase<PX.Objects.AR.ARInvoice>) instance.Document).Current
        }, false);
        transactionScope.Complete();
      }
      return true;
    }
    catch (PXException ex) when (isMassProcess)
    {
      PXProcessing<PX.Objects.AR.ARRegister>.SetError(index, (Exception) ex);
      return false;
    }
  }

  protected virtual bool ReleaseOnSeparateTransaction(
    PX.Objects.AR.ARRegister doc,
    int index,
    bool isMassProcess)
  {
    if ((doc != null ? (!doc.IsCancellation.GetValueOrDefault() ? 1 : 0) : 1) != 0)
      return false;
    try
    {
      using (PXTransactionScope transactionScope = new PXTransactionScope())
      {
        List<PX.Objects.AR.ARRegister> list = ((IEnumerable<PX.Objects.AR.ARRegister>) new PX.Objects.AR.ARRegister[index + 1]).ToList<PX.Objects.AR.ARRegister>();
        list[index] = doc;
        this.Base.ReleaseInvoiceProcImpl(list, isMassProcess);
        transactionScope.Complete();
      }
    }
    catch (PXOperationCompletedWithErrorException ex) when (isMassProcess)
    {
    }
    return true;
  }
}
