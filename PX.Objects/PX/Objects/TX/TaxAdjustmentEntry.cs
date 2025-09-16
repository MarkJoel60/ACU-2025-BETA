// Decompiled with JetBrains decompiler
// Type: PX.Objects.TX.TaxAdjustmentEntry
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.WorkflowAPI;
using PX.Objects.AP;
using PX.Objects.Common;
using PX.Objects.Common.Extensions;
using PX.Objects.Common.GraphExtensions.Abstract;
using PX.Objects.Common.GraphExtensions.Abstract.Mapping;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.Extensions.MultiCurrency;
using PX.Objects.Extensions.MultiCurrency.AP;
using PX.Objects.GL;
using PX.Objects.GL.FinPeriods;
using PX.Objects.GL.FinPeriods.TableDefinition;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

#nullable disable
namespace PX.Objects.TX;

public class TaxAdjustmentEntry : PXGraph<TaxAdjustmentEntry, TaxAdjustment>
{
  public PXSelect<TaxAdjustment, Where<TaxAdjustment.docType, Equal<Optional<TaxAdjustment.docType>>>> Document;
  public PXSelect<TaxAdjustment, Where<TaxAdjustment.docType, Equal<Current<TaxAdjustment.docType>>, And<TaxAdjustment.refNbr, Equal<Current<TaxAdjustment.refNbr>>>>> CurrentDocument;
  public PXSelect<TaxTran, Where<TaxTran.tranType, Equal<Current<TaxAdjustment.docType>>, And<TaxTran.refNbr, Equal<Current<TaxAdjustment.refNbr>>>>> Transactions;
  public PXSelect<PX.Objects.CM.Extensions.CurrencyInfo, Where<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<Current<TaxAdjustment.curyInfoID>>>> currencyinfo;
  public PXSetup<PX.Objects.AP.Vendor, Where<PX.Objects.AP.Vendor.bAccountID, Equal<Optional<TaxAdjustment.vendorID>>>> vendor;
  public PXSetup<PX.Objects.CR.Location, Where<PX.Objects.CR.Location.bAccountID, Equal<Current<TaxAdjustment.vendorID>>, And<PX.Objects.CR.Location.locationID, Equal<Optional<TaxAdjustment.vendorLocationID>>>>> location;
  public PXSelect<Tax, Where<Tax.taxID, Equal<Required<Tax.taxID>>>> SalesTax_Select;
  public PXSelect<TaxRev, Where<TaxRev.taxID, Equal<Required<TaxRev.taxID>>, And<TaxRev.taxType, Equal<Required<TaxRev.taxType>>, And<TaxRev.outdated, Equal<False>, And<Required<TaxRev.startDate>, Between<TaxRev.startDate, TaxRev.endDate>>>>>> SalesTaxRev_Select;
  public PXInitializeState<TaxAdjustment> initializeState;
  public PXAction<TaxAdjustment> putOnHold;
  public PXAction<TaxAdjustment> releaseFromHold;
  public PXAction<TaxAdjustment> newVendor;
  public PXAction<TaxAdjustment> editVendor;
  public PXAction<TaxAdjustment> viewBatch;
  public PXAction<TaxAdjustment> viewOriginalDocument;
  public PXAction<TaxAdjustment> release;
  public PXAction<TaxAdjustment> reverseAdjustment;
  public PXSetup<PX.Objects.AP.APSetup> APSetup;
  public PXSetup<PX.Objects.TX.TXSetup> TXSetup;

  protected bool IsReversingInProgress { get; set; }

  [InjectDependency]
  public IFinPeriodRepository FinPeriodRepository { get; set; }

  [PXButton(CommitChanges = true)]
  [PXUIField]
  protected virtual IEnumerable PutOnHold(PXAdapter adapter) => adapter.Get();

  [PXButton(CommitChanges = true)]
  [PXUIField]
  protected virtual IEnumerable ReleaseFromHold(PXAdapter adapter) => adapter.Get();

  [PXUIField]
  [PXLookupButton]
  public virtual IEnumerable NewVendor(PXAdapter adapter)
  {
    throw new PXRedirectRequiredException((PXGraph) PXGraph.CreateInstance<VendorMaint>(), "New Vendor");
  }

  [PXUIField]
  [PXLookupButton]
  public virtual IEnumerable EditVendor(PXAdapter adapter)
  {
    if (((PXSelectBase<PX.Objects.AP.Vendor>) this.vendor).Current != null)
    {
      VendorMaint instance = PXGraph.CreateInstance<VendorMaint>();
      ((PXSelectBase<VendorR>) instance.BAccount).Current = (VendorR) ((PXSelectBase<PX.Objects.AP.Vendor>) this.vendor).Current;
      throw new PXRedirectRequiredException((PXGraph) instance, "Edit Vendor");
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXLookupButton]
  public virtual IEnumerable ViewBatch(PXAdapter adapter)
  {
    if (((PXSelectBase<TaxAdjustment>) this.Document).Current != null && !string.IsNullOrEmpty(((PXSelectBase<TaxAdjustment>) this.Document).Current.BatchNbr))
    {
      JournalEntry instance = PXGraph.CreateInstance<JournalEntry>();
      ((PXSelectBase<Batch>) instance.BatchModule).Current = PXResultset<Batch>.op_Implicit(((PXSelectBase<Batch>) instance.BatchModule).Search<Batch.batchNbr>((object) ((PXSelectBase<TaxAdjustment>) this.Document).Current.BatchNbr, new object[1]
      {
        (object) "AP"
      }));
      throw new PXRedirectRequiredException((PXGraph) instance, "Current batch record");
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXLookupButton]
  protected virtual IEnumerable ViewOriginalDocument(PXAdapter adapter)
  {
    TaxAdjustment current = ((PXSelectBase<TaxAdjustment>) this.Document).Current;
    if (current == null || current.OrigRefNbr == null)
      return adapter.Get();
    TaxAdjustment taxAdjustment = PXResultset<TaxAdjustment>.op_Implicit(PXSelectBase<TaxAdjustment, PXSelect<TaxAdjustment, Where<TaxAdjustment.docType, Equal<Required<TaxAdjustment.docType>>, And<TaxAdjustment.refNbr, Equal<Required<TaxAdjustment.refNbr>>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, (object[]) new string[2]
    {
      current.DocType,
      current.OrigRefNbr
    }));
    if (taxAdjustment != null)
      ((PXSelectBase) this.Document).Cache.Current = (object) taxAdjustment;
    return (IEnumerable) new List<TaxAdjustment>()
    {
      taxAdjustment
    };
  }

  [PXUIField]
  [PXProcessButton]
  public virtual IEnumerable Release(PXAdapter adapter)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    TaxAdjustmentEntry.\u003C\u003Ec__DisplayClass31_0 cDisplayClass310 = new TaxAdjustmentEntry.\u003C\u003Ec__DisplayClass31_0();
    PXCache cache = ((PXSelectBase) this.Document).Cache;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass310.list = new List<TaxAdjustment>();
    foreach (TaxAdjustment taxAdjustment in adapter.Get())
    {
      if (!taxAdjustment.Hold.GetValueOrDefault() && !taxAdjustment.Released.GetValueOrDefault())
      {
        cache.Update((object) taxAdjustment);
        // ISSUE: reference to a compiler-generated field
        cDisplayClass310.list.Add(taxAdjustment);
      }
    }
    // ISSUE: reference to a compiler-generated field
    if (cDisplayClass310.list.Count == 0)
      throw new PXException("Document Status is invalid for processing.");
    ((PXAction) this.Save).Press();
    // ISSUE: reference to a compiler-generated field
    if (cDisplayClass310.list.Count > 0)
    {
      // ISSUE: method pointer
      PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) cDisplayClass310, __methodptr(\u003CRelease\u003Eb__0)));
    }
    // ISSUE: reference to a compiler-generated field
    return (IEnumerable) cDisplayClass310.list;
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable ReverseAdjustment(PXAdapter adapter)
  {
    TaxAdjustment current = ((PXSelectBase<TaxAdjustment>) this.Document).Current;
    if ((current != null ? (!current.Released.GetValueOrDefault() ? 1 : 0) : 1) != 0 || !this.AskUserApprovalIfReversingDocumentAlreadyExists(current))
      return adapter.Get();
    ((PXAction) this.Save).Press();
    try
    {
      this.IsReversingInProgress = true;
      ((PXGraph) this).Clear((PXClearOption) 1);
      Tuple<TaxAdjustment, PX.Objects.CM.Extensions.CurrencyInfo> adjustmentWithCuryInfo = this.CreateReversingTaxAdjustmentWithCuryInfo(current);
      TaxAdjustment reversedTaxAdj = ((PXSelectBase<TaxAdjustment>) this.Document).Insert(adjustmentWithCuryInfo.Item1);
      this.UpdateCurrencyInfoForReversedTaxAdjustment(adjustmentWithCuryInfo.Item2);
      this.AddReversedTaxTransactionsToReversedTaxAdjustment(current, reversedTaxAdj);
      ((PXSelectBase) this.Document).Cache.RaiseExceptionHandling<TaxAdjustment.finPeriodID>((object) ((PXSelectBase<TaxAdjustment>) this.Document).Current, (object) ((PXSelectBase<TaxAdjustment>) this.Document).Current.FinPeriodID, (Exception) null);
      PXTrace.WriteVerbose("Reverse Tax Adjustment for Tax Adjustment \"{0}\" was created", new object[1]
      {
        (object) current.RefNbr
      });
      return (IEnumerable) new List<TaxAdjustment>()
      {
        ((PXSelectBase<TaxAdjustment>) this.Document).Current
      };
    }
    catch (PXException ex)
    {
      PXTrace.WriteError((Exception) ex);
      ((PXGraph) this).Clear((PXClearOption) 1);
      ((PXSelectBase<TaxAdjustment>) this.Document).Current = current;
      throw;
    }
    finally
    {
      this.IsReversingInProgress = false;
    }
  }

  /// <summary>
  /// Ask user for approval for creation of another reversal if reversing <see cref="T:PX.Objects.TX.TaxAdjustment" /> already exists.
  /// </summary>
  /// <param name="taxAdjToReverse">The tax adjustment to reverse.</param>
  /// <returns />
  private bool AskUserApprovalIfReversingDocumentAlreadyExists(TaxAdjustment taxAdjToReverse)
  {
    TaxAdjustment taxAdjustment = PXResultset<TaxAdjustment>.op_Implicit(PXSelectBase<TaxAdjustment, PXSelect<TaxAdjustment, Where<TaxAdjustment.docType, Equal<Required<TaxAdjustment.docType>>, And<TaxAdjustment.origRefNbr, Equal<Required<TaxAdjustment.origRefNbr>>>>, OrderBy<Desc<TaxAdjustment.createdDateTime>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, (object[]) new string[2]
    {
      taxAdjToReverse.DocType,
      taxAdjToReverse.RefNbr
    }));
    if (taxAdjustment == null)
      return true;
    string docType;
    if (!new TaxAdjustmentType.ListAttribute().ValueLabelDic.TryGetValue(taxAdjustment.DocType, out docType))
    {
      docType = taxAdjToReverse.DocType;
      PXTrace.WriteWarning("Failed to retrieve tax adjustment type {0} description from {1} attribute.", new object[2]
      {
        (object) taxAdjustment.DocType,
        (object) "ListAttribute"
      });
    }
    return WebDialogResultExtension.IsPositive(((PXSelectBase<TaxAdjustment>) this.Document).Ask("Reverse", PXMessages.LocalizeFormatNoPrefix("A reversing document ({0} {1}) already exists for the original document. Reverse the document?", new object[2]
    {
      (object) docType,
      (object) taxAdjustment.RefNbr
    }), (MessageButtons) 4));
  }

  private Tuple<TaxAdjustment, PX.Objects.CM.Extensions.CurrencyInfo> CreateReversingTaxAdjustmentWithCuryInfo(
    TaxAdjustment taxAdjToReverse)
  {
    PX.Objects.CM.Extensions.CurrencyInfo currencyInfo1 = PXResultset<PX.Objects.CM.Extensions.CurrencyInfo>.op_Implicit(PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo, PXSelect<PX.Objects.CM.Extensions.CurrencyInfo, Where<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<Required<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[1]
    {
      (object) taxAdjToReverse.CuryInfoID
    }));
    if (currencyInfo1 == null)
    {
      PXTrace.WriteError("The {0} object with the ID {1} is not found", new object[2]
      {
        (object) "CurrencyInfo",
        (object) taxAdjToReverse.CuryInfoID
      });
      throw new PXException("The {0} object with the ID {1} is not found. The reverse tax adjustment can't be created.", new object[2]
      {
        (object) "CurrencyInfo",
        (object) taxAdjToReverse.CuryInfoID
      });
    }
    PX.Objects.CM.Extensions.CurrencyInfo copy1 = PXCache<PX.Objects.CM.Extensions.CurrencyInfo>.CreateCopy(currencyInfo1);
    copy1.CuryInfoID = new long?();
    copy1.IsReadOnly = new bool?(false);
    PX.Objects.CM.Extensions.CurrencyInfo currencyInfo2 = ((PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo>) this.currencyinfo).Insert(copy1);
    TaxAdjustment copy2 = PXCache<TaxAdjustment>.CreateCopy(taxAdjToReverse);
    copy2.CuryInfoID = currencyInfo2.CuryInfoID;
    copy2.RefNbr = (string) null;
    copy2.OrigRefNbr = taxAdjToReverse.RefNbr;
    copy2.Released = new bool?(false);
    copy2.Hold = new bool?(true);
    copy2.BatchNbr = (string) null;
    copy2.NoteID = new Guid?();
    Decimal? nullable = copy2.OrigDocAmt;
    copy2.OrigDocAmt = nullable.HasValue ? new Decimal?(-nullable.GetValueOrDefault()) : new Decimal?();
    nullable = copy2.CuryOrigDocAmt;
    copy2.CuryOrigDocAmt = nullable.HasValue ? new Decimal?(-nullable.GetValueOrDefault()) : new Decimal?();
    nullable = new Decimal?();
    copy2.CuryDocBal = nullable;
    nullable = new Decimal?();
    copy2.DocBal = nullable;
    return Tuple.Create<TaxAdjustment, PX.Objects.CM.Extensions.CurrencyInfo>(copy2, currencyInfo2);
  }

  private void UpdateCurrencyInfoForReversedTaxAdjustment(PX.Objects.CM.Extensions.CurrencyInfo reversedAdjCuryInfo)
  {
    if (reversedAdjCuryInfo == null)
      return;
    PX.Objects.CM.Extensions.CurrencyInfo currencyInfo = PXResultset<PX.Objects.CM.Extensions.CurrencyInfo>.op_Implicit(PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo, PXSelect<PX.Objects.CM.Extensions.CurrencyInfo, Where<PX.Objects.CM.Extensions.CurrencyInfo.curyInfoID, Equal<Current<TaxAdjustment.curyInfoID>>>>.Config>.Select((PXGraph) this, (object[]) null));
    currencyInfo.CuryID = reversedAdjCuryInfo.CuryID;
    currencyInfo.CuryEffDate = reversedAdjCuryInfo.CuryEffDate;
    currencyInfo.CuryRateTypeID = reversedAdjCuryInfo.CuryRateTypeID;
    currencyInfo.CuryRate = reversedAdjCuryInfo.CuryRate;
    currencyInfo.RecipRate = reversedAdjCuryInfo.RecipRate;
    currencyInfo.CuryMultDiv = reversedAdjCuryInfo.CuryMultDiv;
    ((PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo>) this.currencyinfo).Update(currencyInfo);
  }

  private void AddReversedTaxTransactionsToReversedTaxAdjustment(
    TaxAdjustment originalTaxAdj,
    TaxAdjustment reversedTaxAdj)
  {
    if (reversedTaxAdj == null)
    {
      PXTrace.WriteError("The creation reversed Tax Adjustment for the Tax Adjustment with the ID {0} failed", new object[1]
      {
        (object) originalTaxAdj.RefNbr
      });
      throw new PXException("The creation reversed Tax Adjustment for the Tax Adjustment with the ID {0} failed", new object[1]
      {
        (object) originalTaxAdj.RefNbr
      });
    }
    foreach (PXResult<TaxTran> pxResult in PXSelectBase<TaxTran, PXSelect<TaxTran, Where<TaxTran.tranType, Equal<Required<TaxAdjustment.docType>>, And<TaxTran.refNbr, Equal<Required<TaxAdjustment.refNbr>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) originalTaxAdj.DocType,
      (object) originalTaxAdj.RefNbr
    }))
    {
      TaxTran taxTran1 = PXResult<TaxTran>.op_Implicit(pxResult);
      TaxTran copy = PXCache<TaxTran>.CreateCopy(taxTran1);
      copy.TranType = taxTran1.TranType;
      copy.RefNbr = reversedTaxAdj.RefNbr;
      copy.RecordID = new int?();
      copy.Released = new bool?();
      copy.CuryInfoID = new long?();
      TaxTran taxTran2 = copy;
      Decimal? nullable1 = taxTran1.ExpenseAmt;
      Decimal? nullable2 = nullable1.HasValue ? new Decimal?(-nullable1.GetValueOrDefault()) : new Decimal?();
      taxTran2.ExpenseAmt = nullable2;
      TaxTran taxTran3 = copy;
      nullable1 = taxTran1.CuryExpenseAmt;
      Decimal? nullable3 = nullable1.HasValue ? new Decimal?(-nullable1.GetValueOrDefault()) : new Decimal?();
      taxTran3.CuryExpenseAmt = nullable3;
      TaxTran taxTran4 = copy;
      nullable1 = taxTran1.OrigTaxableAmt;
      Decimal? nullable4 = nullable1.HasValue ? new Decimal?(-nullable1.GetValueOrDefault()) : new Decimal?();
      taxTran4.OrigTaxableAmt = nullable4;
      TaxTran taxTran5 = copy;
      nullable1 = taxTran1.CuryOrigTaxableAmt;
      Decimal? nullable5 = nullable1.HasValue ? new Decimal?(-nullable1.GetValueOrDefault()) : new Decimal?();
      taxTran5.CuryOrigTaxableAmt = nullable5;
      TaxTran taxTran6 = copy;
      nullable1 = taxTran1.TaxAmt;
      Decimal? nullable6 = nullable1.HasValue ? new Decimal?(-nullable1.GetValueOrDefault()) : new Decimal?();
      taxTran6.TaxAmt = nullable6;
      TaxTran taxTran7 = copy;
      nullable1 = taxTran1.CuryTaxAmt;
      Decimal? nullable7 = nullable1.HasValue ? new Decimal?(-nullable1.GetValueOrDefault()) : new Decimal?();
      taxTran7.CuryTaxAmt = nullable7;
      TaxTran taxTran8 = copy;
      nullable1 = taxTran1.TaxableAmt;
      Decimal? nullable8 = nullable1.HasValue ? new Decimal?(-nullable1.GetValueOrDefault()) : new Decimal?();
      taxTran8.TaxableAmt = nullable8;
      TaxTran taxTran9 = copy;
      nullable1 = taxTran1.CuryTaxableAmt;
      Decimal? nullable9 = nullable1.HasValue ? new Decimal?(-nullable1.GetValueOrDefault()) : new Decimal?();
      taxTran9.CuryTaxableAmt = nullable9;
      TaxTran taxTran10 = copy;
      nullable1 = taxTran1.ReportTaxAmt;
      Decimal? nullable10 = nullable1.HasValue ? new Decimal?(-nullable1.GetValueOrDefault()) : new Decimal?();
      taxTran10.ReportTaxAmt = nullable10;
      TaxTran taxTran11 = copy;
      nullable1 = taxTran1.ReportTaxableAmt;
      Decimal? nullable11 = nullable1.HasValue ? new Decimal?(-nullable1.GetValueOrDefault()) : new Decimal?();
      taxTran11.ReportTaxableAmt = nullable11;
      ((PXSelectBase<TaxTran>) this.Transactions).Insert(copy);
    }
    ((PXSelectBase) this.Transactions).View.RequestRefresh();
  }

  public TaxAdjustmentEntry()
  {
    PX.Objects.AP.APSetup current = ((PXSelectBase<PX.Objects.AP.APSetup>) this.APSetup).Current;
    PXUIFieldAttribute.SetEnabled<TaxAdjustment.curyID>(((PXSelectBase) this.Document).Cache, (object) null, false);
    PXUIFieldAttribute.SetVisible<TaxAdjustment.vendorLocationID>(((PXSelectBase) this.Document).Cache, (object) null, false);
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    ((PXGraph) this).FieldDefaulting.AddHandler<BAccountR.type>(TaxAdjustmentEntry.\u003C\u003Ec.\u003C\u003E9__40_0 ?? (TaxAdjustmentEntry.\u003C\u003Ec.\u003C\u003E9__40_0 = new PXFieldDefaulting((object) TaxAdjustmentEntry.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003C\u002Ector\u003Eb__40_0))));
  }

  [PXFormula(typeof (Mult<TaxTran.curyTaxableAmt, Div<TaxTran.taxRate, decimal100>>), typeof (SumCalc<TaxAdjustment.curyDocBal>), ForceAggregateRecalculation = true)]
  [PXMergeAttributes]
  protected virtual void _(PX.Data.Events.CacheAttached<TaxTran.curyTaxAmt> e)
  {
  }

  protected virtual void TaxAdjustment_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is TaxAdjustment row))
      return;
    bool valueOrDefault = row.Released.GetValueOrDefault();
    cache.AllowDelete = !valueOrDefault;
    cache.AllowUpdate = !valueOrDefault;
    ((PXSelectBase) this.Transactions).Cache.SetAllEditPermissions(!valueOrDefault);
    PXUIFieldAttribute.SetEnabled(cache, (object) row, !valueOrDefault);
    if (!valueOrDefault)
    {
      PXUIFieldAttribute.SetEnabled<TaxAdjustment.status>(cache, (object) row, false);
      PXUIFieldAttribute.SetEnabled<TaxAdjustment.curyDocBal>(cache, (object) row, false);
      PXUIFieldAttribute.SetEnabled<TaxAdjustment.batchNbr>(cache, (object) row, false);
      this.ValidateDocDate(cache, row);
    }
    PXUIFieldAttribute.SetEnabled<TaxAdjustment.docType>(cache, (object) row);
    PXUIFieldAttribute.SetEnabled<TaxAdjustment.refNbr>(cache, (object) row);
    PXUIFieldAttribute.SetEnabled<TaxAdjustment.curyID>(cache, (object) row, false);
    PXUIFieldAttribute.SetVisible<TaxAdjustment.curyID>(cache, (object) row, PXAccess.FeatureInstalled<FeaturesSet.multicurrency>());
    ((PXAction) this.editVendor).SetEnabled(((PXSelectBase<PX.Objects.AP.Vendor>) this.vendor).Current != null);
    bool flag = PXResultset<TaxTran>.op_Implicit(((PXSelectBase<TaxTran>) this.Transactions).SelectWindowed(0, 1, Array.Empty<object>())) != null;
    PXUIFieldAttribute.SetEnabled<TaxAdjustment.vendorID>(cache, (object) row, !flag);
    int? nullable;
    int num;
    if (row == null)
    {
      num = 1;
    }
    else
    {
      nullable = row.BranchID;
      num = !nullable.HasValue ? 1 : 0;
    }
    if (num != 0)
      return;
    nullable = row.VendorID;
    if (!nullable.HasValue || !row.DocDate.HasValue || !(cache.GetStateExt<TaxAdjustment.docDate>((object) row) is PXFieldState stateExt) || stateExt.ErrorLevel == 4)
      return;
    cache.RaiseExceptionHandling<TaxAdjustment.docDate>((object) row, (object) row.DocDate, (Exception) null);
    TaxPeriod forTaxAdjustment = this.GetTaxPeriodForTaxAdjustment(row);
    if (forTaxAdjustment == null)
    {
      cache.RaiseExceptionHandling<TaxAdjustment.docDate>((object) row, (object) row.DocDate, (Exception) new PXSetPropertyException("The date belongs to the nonexistent period.", (PXErrorLevel) 2));
    }
    else
    {
      if (!(forTaxAdjustment.Status == "C"))
        return;
      cache.RaiseExceptionHandling<TaxAdjustment.docDate>((object) row, (object) row.DocDate, (Exception) new PXSetPropertyException("The date belongs to the closed period.", (PXErrorLevel) 2));
    }
  }

  protected virtual void TaxAdjustment_VendorID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if ((TaxAdjustment) e.Row == null)
      return;
    if (!((PXGraph) this).IsCopyPasteContext)
    {
      sender.SetDefaultExt<TaxAdjustment.vendorLocationID>(e.Row);
      sender.SetDefaultExt<TaxAdjustment.taxPeriod>(e.Row);
      sender.SetDefaultExt<TaxAdjustment.adjAccountID>(e.Row);
      sender.SetDefaultExt<TaxAdjustment.adjSubID>(e.Row);
    }
    else
    {
      if (PXAccess.FeatureInstalled<FeaturesSet.accountLocations>())
        return;
      sender.SetDefaultExt<TaxAdjustment.vendorLocationID>(e.Row);
    }
  }

  protected virtual void TaxAdjustment_DocDate_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    foreach (PXResult<TaxTran> pxResult in ((PXSelectBase<TaxTran>) this.Transactions).Select(Array.Empty<object>()))
    {
      TaxTran tran = PXResult<TaxTran>.op_Implicit(pxResult);
      this.SetTaxTranTranDate(tran);
      ((PXSelectBase) this.Transactions).Cache.SetDefaultExt<TaxTran.taxBucketID>((object) tran);
      ((PXSelectBase) this.Transactions).Cache.SetDefaultExt<TaxTran.taxRate>((object) tran);
    }
    if (!(e.Row is TaxAdjustment row) || row.TaxPeriod != null || !row.DocDate.HasValue)
      return;
    TaxPeriod forTaxAdjustment = this.GetTaxPeriodForTaxAdjustment(row);
    if (forTaxAdjustment == null || !(forTaxAdjustment.Status == "P"))
      return;
    sender.SetValue<TaxAdjustment.taxPeriod>((object) row, (object) forTaxAdjustment.TaxPeriodID);
  }

  protected virtual void TaxAdjustment_BranchID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is TaxAdjustment row))
      return;
    if (!row.BranchID.HasValue)
      row.TaxPeriod = (string) null;
    else
      sender.SetDefaultExt<TaxAdjustment.taxPeriod>((object) row);
    this.MarkLinesUpdated();
  }

  protected virtual void TaxAdjustment_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    if (!(e.Row is TaxAdjustment row1))
      return;
    bool? nullable = row1.Hold;
    if (nullable.GetValueOrDefault())
      return;
    nullable = row1.Released;
    if (nullable.GetValueOrDefault())
      return;
    PXCache pxCache = sender;
    object row2 = e.Row;
    // ISSUE: variable of a boxed type
    __Boxed<Decimal?> curyOrigDocAmt1 = (ValueType) ((TaxAdjustment) e.Row).CuryOrigDocAmt;
    Decimal? curyDocBal = ((TaxAdjustment) e.Row).CuryDocBal;
    Decimal? curyOrigDocAmt2 = ((TaxAdjustment) e.Row).CuryOrigDocAmt;
    PXSetPropertyException propertyException = !(curyDocBal.GetValueOrDefault() == curyOrigDocAmt2.GetValueOrDefault() & curyDocBal.HasValue == curyOrigDocAmt2.HasValue) ? new PXSetPropertyException("Document is out of balance.") : (PXSetPropertyException) null;
    pxCache.RaiseExceptionHandling<TaxAdjustment.curyOrigDocAmt>(row2, (object) curyOrigDocAmt1, (Exception) propertyException);
  }

  protected virtual void TaxAdjustment_TaxPeriod_FieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
    TaxAdjustment row = (TaxAdjustment) e.Row;
    this.MarkLinesUpdated();
    this.SetDocDateByPeriods(cache, row);
    this.ValidateDocDate(cache, row);
  }

  private void SetDocDateByPeriods(PXCache cache, TaxAdjustment document)
  {
    if (document.TaxPeriod == null || !document.BranchID.HasValue)
      return;
    TaxPeriod taxPeriodByKey = TaxYearMaint.FindTaxPeriodByKey((PXGraph) this, PXAccess.GetParentOrganizationID(document.BranchID), document.VendorID, document.TaxPeriod);
    if (taxPeriodByKey == null)
      return;
    DateTime? nullable;
    if (((PXSelectBase<PX.Objects.AP.Vendor>) this.vendor).Current.TaxReportFinPeriod.GetValueOrDefault())
    {
      FinPeriod belongToInterval = this.FinPeriodRepository.FindMaxFinPeriodWithEndDataBelongToInterval(taxPeriodByKey.StartDate, taxPeriodByKey.EndDate, PXAccess.GetParentOrganizationID(document.BranchID));
      nullable = belongToInterval != null ? belongToInterval.FinDate : ((PXGraph) this).Accessinfo.BusinessDate;
    }
    else
      nullable = taxPeriodByKey.EndDateUI;
    cache.SetValueExt<TaxAdjustment.docDate>((object) document, (object) nullable);
  }

  private void ValidateDocDate(PXCache cache, TaxAdjustment doc)
  {
    if (!doc.DocDate.HasValue || doc.TaxPeriod == null || !doc.BranchID.HasValue || doc.FinPeriodID == null)
      return;
    TaxPeriod taxPeriodByKey = TaxYearMaint.FindTaxPeriodByKey((PXGraph) this, PXAccess.GetParentOrganizationID(doc.BranchID), doc.VendorID, doc.TaxPeriod);
    if (taxPeriodByKey == null)
      return;
    string str = (string) null;
    if (((PXSelectBase<PX.Objects.AP.Vendor>) this.vendor).Current.TaxReportFinPeriod.GetValueOrDefault())
    {
      DateTime? finDate = this.FinPeriodRepository.GetByID(doc.FinPeriodID, PXAccess.GetParentOrganizationID(doc.BranchID)).FinDate;
      DateTime? endDate = taxPeriodByKey.EndDate;
      if ((finDate.HasValue & endDate.HasValue ? (finDate.GetValueOrDefault() >= endDate.GetValueOrDefault() ? 1 : 0) : 0) != 0)
        str = "Selected date belongs to the tax period that is greater than the specified one.";
    }
    else
    {
      DateTime? docDate = doc.DocDate;
      DateTime? endDate = taxPeriodByKey.EndDate;
      if ((docDate.HasValue & endDate.HasValue ? (docDate.GetValueOrDefault() >= endDate.GetValueOrDefault() ? 1 : 0) : 0) != 0)
        str = "Selected date belongs to the tax period that is greater than the specified one.";
    }
    PXSetPropertyException propertyException = str != null ? new PXSetPropertyException(str, (PXErrorLevel) 2) : (PXSetPropertyException) null;
    cache.RaiseExceptionHandling<TaxAdjustment.docDate>((object) doc, (object) doc.DocDate, (Exception) propertyException);
  }

  protected virtual void TaxTranDefaulting(PXCache sender, TaxTran tran)
  {
    sender.SetDefaultExt<TaxTran.accountID>((object) tran);
    sender.SetDefaultExt<TaxTran.subID>((object) tran);
    sender.SetDefaultExt<TaxTran.taxType>((object) tran);
    this.SetTaxTranTranDate(tran);
    sender.SetDefaultExt<TaxTran.taxBucketID>((object) tran);
    sender.SetDefaultExt<TaxTran.taxRate>((object) tran);
  }

  [PXMergeAttributes]
  [PXDBDefault(typeof (TaxAdjustment.branchID))]
  protected virtual void TaxTran_BranchID_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXDBDefault(typeof (TaxAdjustment.taxPeriod))]
  protected virtual void TaxTran_TaxPeriodID_CacheAttached(PXCache sender)
  {
  }

  protected virtual void TaxTran_TaxID_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    if (object.Equals(e.OldValue, (object) ((TaxDetail) e.Row).TaxID))
      return;
    this.TaxTranDefaulting(sender, (TaxTran) e.Row);
  }

  protected virtual void TaxTran_TaxID_FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    TaxTran row = (TaxTran) e.Row;
    if (row == null)
      return;
    TaxTran copy = (TaxTran) sender.CreateCopy((object) row);
    copy.TaxID = (string) e.NewValue;
    this.TaxTranDefaulting(sender, copy);
    if (!copy.TaxBucketID.HasValue)
      throw new PXSetPropertyException("Can't find effective rate for '{0}' (type '{1}')", new object[2]
      {
        (object) copy.TaxID,
        (object) GetLabel.For<TaxType>(copy.TaxType)
      });
  }

  protected virtual void TaxTran_AccountID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    Tax tax = Tax.PK.Find((PXGraph) this, e.Row is TaxTran row ? row.TaxID : (string) null);
    if (tax == null || ((PXSelectBase<TaxAdjustment>) this.Document).Current == null)
      return;
    switch (((PXSelectBase<TaxAdjustment>) this.Document).Current.DocType)
    {
      case "INT":
        e.NewValue = ((PXSelectBase<Tax>) this.SalesTax_Select).GetValueExt<Tax.salesTaxAcctID>(tax);
        break;
      case "RET":
        e.NewValue = ((PXSelectBase<Tax>) this.SalesTax_Select).GetValueExt<Tax.purchTaxAcctID>(tax);
        break;
    }
  }

  protected virtual void TaxTran_RevisionID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (!((PXSelectBase<TaxAdjustment>) this.Document).Current.BranchID.HasValue)
    {
      e.NewValue = (object) null;
    }
    else
    {
      PX.Objects.GL.DAC.Organization organizationById = OrganizationMaint.FindOrganizationByID((PXGraph) this, PXAccess.GetParentOrganizationID(((PXSelectBase<TaxAdjustment>) this.Document).Current.BranchID));
      int?[] nullableArray1;
      if (!organizationById.FileTaxesByBranches.GetValueOrDefault())
        nullableArray1 = (int?[]) null;
      else
        nullableArray1 = new int?[1]
        {
          ((PXSelectBase<TaxAdjustment>) this.Document).Current.BranchID
        };
      int?[] nullableArray2 = nullableArray1;
      using (new PXReadBranchRestrictedScope(organizationById.OrganizationID.SingleToArray<int?>(), nullableArray2, true, true))
      {
        TaxHistory taxHistory = PXResultset<TaxHistory>.op_Implicit(PXSelectBase<TaxHistory, PXSelect<TaxHistory, Where<TaxHistory.vendorID, Equal<Current<TaxAdjustment.vendorID>>, And<TaxHistory.taxPeriodID, Equal<Current<TaxAdjustment.taxPeriod>>>>, OrderBy<Desc<TaxHistory.revisionID>>>.Config>.Select((PXGraph) this, Array.Empty<object>()));
        e.NewValue = (object) (taxHistory != null ? taxHistory.RevisionID : new int?(1));
      }
    }
  }

  protected virtual void TaxTran_SubID_FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
  {
    Tax tax = Tax.PK.Find((PXGraph) this, e.Row is TaxTran row ? row.TaxID : (string) null);
    if (tax == null || ((PXSelectBase<TaxAdjustment>) this.Document).Current == null)
      return;
    switch (((PXSelectBase<TaxAdjustment>) this.Document).Current.DocType)
    {
      case "INT":
        e.NewValue = ((PXSelectBase<Tax>) this.SalesTax_Select).GetValueExt<Tax.salesTaxSubID>(tax);
        break;
      case "RET":
        e.NewValue = ((PXSelectBase<Tax>) this.SalesTax_Select).GetValueExt<Tax.purchTaxSubID>(tax);
        break;
    }
  }

  protected virtual void TaxTran_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    if (!(e.Row is TaxTran row))
      return;
    row.ReportTaxAmt = row.CuryTaxAmt;
    row.ReportTaxableAmt = row.CuryTaxableAmt;
  }

  protected virtual void TaxTran_TaxType_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (((PXSelectBase<TaxAdjustment>) this.Document).Current == null)
      return;
    switch (((PXSelectBase<TaxAdjustment>) this.Document).Current.DocType)
    {
      case "INT":
        e.NewValue = (object) "S";
        break;
      case "RET":
        e.NewValue = (object) "P";
        break;
    }
  }

  protected virtual void SetTaxTranTranDate(TaxTran tran)
  {
    if (((PXSelectBase<TaxAdjustment>) this.Document).Current == null)
      return;
    tran.TranDate = ((PXSelectBase<TaxAdjustment>) this.Document).Current.DocDate;
  }

  protected virtual void TaxTran_TaxBucketID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    TaxTran row = (TaxTran) e.Row;
    if (row == null)
      return;
    TaxRev taxRev = PXResultset<TaxRev>.op_Implicit(((PXSelectBase<TaxRev>) this.SalesTaxRev_Select).Select(new object[3]
    {
      (object) row.TaxID,
      (object) row.TaxType,
      (object) row.TranDate
    }));
    if (taxRev == null)
      return;
    e.NewValue = (object) taxRev.TaxBucketID;
    sender.SetValue<TaxTran.taxRate>(e.Row, (object) taxRev.TaxRate);
  }

  protected virtual void TaxTran_TaxRate_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    TaxTran row = (TaxTran) e.Row;
    if (row == null)
      return;
    TaxRev taxRev = PXResultset<TaxRev>.op_Implicit(((PXSelectBase<TaxRev>) this.SalesTaxRev_Select).Select(new object[3]
    {
      (object) row.TaxID,
      (object) row.TaxType,
      (object) row.TranDate
    }));
    if (taxRev == null)
      return;
    e.NewValue = (object) taxRev.TaxRate;
    ((CancelEventArgs) e).Cancel = true;
  }

  private void MarkLinesUpdated()
  {
    foreach (PXResult<TaxTran> pxResult in ((PXSelectBase<TaxTran>) this.Transactions).Select(Array.Empty<object>()))
      GraphHelper.MarkUpdated(((PXSelectBase) this.Transactions).Cache, (object) PXResult<TaxTran>.op_Implicit(pxResult));
  }

  private TaxPeriod GetTaxPeriodForTaxAdjustment(TaxAdjustment taxAdjustment)
  {
    int? nullable;
    int num;
    if (taxAdjustment == null)
    {
      num = 1;
    }
    else
    {
      nullable = taxAdjustment.BranchID;
      num = !nullable.HasValue ? 1 : 0;
    }
    if (num == 0)
    {
      nullable = taxAdjustment.VendorID;
      if (nullable.HasValue && taxAdjustment.DocDate.HasValue)
        return PXResultset<TaxPeriod>.op_Implicit(PXSelectBase<TaxPeriod, PXSelectJoin<TaxPeriod, InnerJoin<PX.Objects.GL.Branch, On<PX.Objects.GL.Branch.organizationID, Equal<TaxPeriod.organizationID>>>, Where<PX.Objects.GL.Branch.branchID, Equal<Required<PX.Objects.GL.Branch.branchID>>, And<TaxPeriod.vendorID, Equal<Required<TaxPeriod.vendorID>>, And<TaxPeriod.startDate, LessEqual<Required<TaxPeriod.startDate>>, And<TaxPeriod.endDate, Greater<Required<TaxPeriod.endDate>>>>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[4]
        {
          (object) taxAdjustment.BranchID,
          (object) taxAdjustment.VendorID,
          (object) taxAdjustment.DocDate,
          (object) taxAdjustment.DocDate
        }));
    }
    return (TaxPeriod) null;
  }

  public class TaxAdjustmentEntryDocumentExtension : 
    DocumentWithLinesGraphExtension<TaxAdjustmentEntry>
  {
    public virtual void Initialize()
    {
      ((PXGraphExtension) this).Initialize();
      this.Documents = new PXSelectExtension<PX.Objects.Common.GraphExtensions.Abstract.DAC.Document>((PXSelectBase) this.Base.Document);
      this.Lines = new PXSelectExtension<PX.Objects.Common.GraphExtensions.Abstract.DAC.DocumentLine>((PXSelectBase) this.Base.Transactions);
    }

    protected override PX.Objects.Common.GraphExtensions.Abstract.Mapping.DocumentMapping GetDocumentMapping()
    {
      return new PX.Objects.Common.GraphExtensions.Abstract.Mapping.DocumentMapping(typeof (TaxAdjustment))
      {
        HeaderTranPeriodID = typeof (TaxAdjustment.tranPeriodID),
        HeaderDocDate = typeof (TaxAdjustment.docDate)
      };
    }

    protected override DocumentLineMapping GetDocumentLineMapping()
    {
      return new DocumentLineMapping(typeof (TaxTran));
    }

    protected override bool ShouldUpdateLinesOnDocumentUpdated(PX.Data.Events.RowUpdated<PX.Objects.Common.GraphExtensions.Abstract.DAC.Document> e)
    {
      return base.ShouldUpdateLinesOnDocumentUpdated(e) || !((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<PX.Objects.Common.GraphExtensions.Abstract.DAC.Document>>) e).Cache.ObjectsEqual<PX.Objects.Common.GraphExtensions.Abstract.DAC.Document.headerDocDate>((object) e.Row, (object) e.OldRow);
    }

    protected override void ProcessLineOnDocumentUpdated(
      PX.Data.Events.RowUpdated<PX.Objects.Common.GraphExtensions.Abstract.DAC.Document> e,
      PX.Objects.Common.GraphExtensions.Abstract.DAC.DocumentLine line)
    {
      base.ProcessLineOnDocumentUpdated(e, line);
      if (((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<PX.Objects.Common.GraphExtensions.Abstract.DAC.Document>>) e).Cache.ObjectsEqual<PX.Objects.Common.GraphExtensions.Abstract.DAC.Document.headerDocDate>((object) e.Row, (object) e.OldRow))
        return;
      ((PXSelectBase) this.Lines).Cache.SetDefaultExt<PX.Objects.Common.GraphExtensions.Abstract.DAC.DocumentLine.tranDate>((object) line);
    }
  }

  public class MultiCurrency : APMultiCurrencyGraph<TaxAdjustmentEntry, TaxAdjustment>
  {
    protected override string DocumentStatus
    {
      get => ((PXSelectBase<TaxAdjustment>) this.Base.Document).Current?.Status;
    }

    protected override MultiCurrencyGraph<TaxAdjustmentEntry, TaxAdjustment>.DocumentMapping GetDocumentMapping()
    {
      return new MultiCurrencyGraph<TaxAdjustmentEntry, TaxAdjustment>.DocumentMapping(typeof (TaxAdjustment))
      {
        DocumentDate = typeof (TaxAdjustment.docDate),
        BAccountID = typeof (TaxAdjustment.vendorID)
      };
    }

    protected override PXSelectBase[] GetChildren()
    {
      return new PXSelectBase[2]
      {
        (PXSelectBase) this.Base.Document,
        (PXSelectBase) this.Base.Transactions
      };
    }
  }
}
