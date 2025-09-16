// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARFinChargesApplyMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.AP;
using PX.Objects.AR.MigrationMode;
using PX.Objects.BQLConstants;
using PX.Objects.CM;
using PX.Objects.Common;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.GL.FinPeriods.TableDefinition;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace PX.Objects.AR;

[TableAndChartDashboardType]
[Serializable]
public class ARFinChargesApplyMaint : PXGraph<
#nullable disable
ARFinChargesApplyMaint>
{
  [PXReadOnlyView]
  public PXSelect<ARInvoice, Where<True, Equal<False>>> ARInvoices;
  public PXAction<ARFinChargesApplyMaint.ARFinChargesApplyParameters> cancel;
  public PXAction<ARFinChargesApplyMaint.ARFinChargesApplyParameters> calculate;
  public PXFilter<ARFinChargesApplyMaint.ARFinChargesApplyParameters> Filter;
  [PXFilterable(new System.Type[] {})]
  public PXFilteredProcessing<ARFinChargesApplyMaint.ARFinChargesDetails, ARFinChargesApplyMaint.ARFinChargesApplyParameters> ARFinChargeRecords;
  public PXSelectJoin<Customer, InnerJoin<PX.Objects.CR.Location, On<PX.Objects.CR.Location.bAccountID, Equal<Customer.bAccountID>, And<PX.Objects.CR.Location.locationID, Equal<Customer.defLocationID>>>, InnerJoin<CustomerClass, On<CustomerClass.customerClassID, Equal<Customer.customerClassID>>, InnerJoin<ARStatementCycle, On<ARStatementCycle.statementCycleId, Equal<Customer.statementCycleId>>>>>, Where<Customer.finChargeApply, Equal<True>, And<Customer.status, NotEqual<CustomerStatus.inactive>, And<Customer.status, NotEqual<CustomerStatus.hold>, And<Match<Current<AccessInfo.userName>>>>>>> CustomersInStatementCycle;
  public ARSetupNoMigrationMode ARSetup;
  public PXAction<ARFinChargesApplyMaint.ARFinChargesApplyParameters> viewDocument;
  public PXAction<ARFinChargesApplyMaint.ARFinChargesApplyParameters> viewLastFinCharge;

  public ARFinChargesApplyMaint()
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    ARFinChargesApplyMaint.\u003C\u003Ec__DisplayClass4_0 cDisplayClass40 = new ARFinChargesApplyMaint.\u003C\u003Ec__DisplayClass4_0();
    PX.Objects.AR.ARSetup current = ((PXSelectBase<PX.Objects.AR.ARSetup>) this.ARSetup).Current;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass40.filter = ((PXSelectBase<ARFinChargesApplyMaint.ARFinChargesApplyParameters>) this.Filter).Current;
    // ISSUE: method pointer
    ((PXProcessingBase<ARFinChargesApplyMaint.ARFinChargesDetails>) this.ARFinChargeRecords).SetProcessDelegate(new PXProcessingBase<ARFinChargesApplyMaint.ARFinChargesDetails>.ProcessListDelegate((object) cDisplayClass40, __methodptr(\u003C\u002Ector\u003Eb__0)));
  }

  public virtual bool IsProcessing
  {
    get => false;
    set
    {
    }
  }

  public virtual bool IsDirty => false;

  [PXUIField]
  [PXButton]
  public virtual IEnumerable ViewDocument(PXAdapter adapter)
  {
    ARFinChargesApplyMaint.ARFinChargesDetails current = ((PXSelectBase<ARFinChargesApplyMaint.ARFinChargesDetails>) this.ARFinChargeRecords).Current;
    if (current != null)
    {
      ARInvoice arInvoice = PXResultset<ARInvoice>.op_Implicit(PXSelectBase<ARInvoice, PXSelect<ARInvoice>.Config>.Search<ARInvoice.docType, ARInvoice.refNbr>((PXGraph) this, (object) current.DocType, (object) current.RefNbr, Array.Empty<object>()));
      if (arInvoice != null)
      {
        ARInvoiceEntry instance = PXGraph.CreateInstance<ARInvoiceEntry>();
        ((PXSelectBase<ARInvoice>) instance.Document).Current = arInvoice;
        PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, true, "Document");
        ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
        throw requiredException;
      }
    }
    return (IEnumerable) ((PXSelectBase<ARFinChargesApplyMaint.ARFinChargesApplyParameters>) this.Filter).Select(Array.Empty<object>());
  }

  [PXUIField]
  [PXButton(VisibleOnProcessingResults = true)]
  public virtual IEnumerable ViewLastFinCharge(PXAdapter adapter)
  {
    ARFinChargesApplyMaint.ARFinChargesDetails current = ((PXSelectBase<ARFinChargesApplyMaint.ARFinChargesDetails>) this.ARFinChargeRecords).Current;
    if (current != null && !string.IsNullOrEmpty(current.LastFCRefNbr))
    {
      ARInvoice arInvoice = PXResultset<ARInvoice>.op_Implicit(PXSelectBase<ARInvoice, PXSelect<ARInvoice>.Config>.Search<ARInvoice.docType, ARInvoice.refNbr>((PXGraph) this, (object) current.LastFCDocType, (object) current.LastFCRefNbr, Array.Empty<object>()));
      if (arInvoice != null)
      {
        ARInvoiceEntry instance = PXGraph.CreateInstance<ARInvoiceEntry>();
        ((PXSelectBase<ARInvoice>) instance.Document).Current = arInvoice;
        PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, true, "Document");
        ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 3;
        throw requiredException;
      }
    }
    return (IEnumerable) ((PXSelectBase<ARFinChargesApplyMaint.ARFinChargesApplyParameters>) this.Filter).Select(Array.Empty<object>());
  }

  [PXMergeAttributes]
  [PXUIField(DisplayName = "Branch", Visible = false)]
  [PXUIVisible(typeof (BqlChainableConditionLite<FeatureInstalled<FeaturesSet.branch>>.Or<FeatureInstalled<FeaturesSet.multiCompany>>))]
  protected virtual void _(
    PX.Data.Events.CacheAttached<ARFinChargesApplyMaint.ARFinChargesDetails.branchID> e)
  {
  }

  [PXUIField]
  [PXCancelButton]
  protected virtual IEnumerable Cancel(PXAdapter adapter)
  {
    ((PXSelectBase) this.ARFinChargeRecords).Cache.Clear();
    ((PXGraph) this).TimeStamp = (byte[]) null;
    PXLongOperation.ClearStatus(((PXGraph) this).UID);
    return adapter.Get();
  }

  protected virtual IEnumerable aRFinChargeRecords()
  {
    return ((PXSelectBase) this.ARFinChargeRecords).Cache.Inserted;
  }

  [PXUIField]
  [PXProcessButton]
  public virtual IEnumerable Calculate(PXAdapter adapter)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    ARFinChargesApplyMaint.\u003C\u003Ec__DisplayClass24_0 cDisplayClass240 = new ARFinChargesApplyMaint.\u003C\u003Ec__DisplayClass24_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass240.filter = ((PXSelectBase<ARFinChargesApplyMaint.ARFinChargesApplyParameters>) this.Filter).Current;
    // ISSUE: reference to a compiler-generated field
    if (!this.CheckRequiredFieldsFilled(((PXSelectBase) this.Filter).Cache, (IBqlTable) cDisplayClass240.filter))
      return adapter.Get();
    PXResultset<Company>.op_Implicit(PXSelectBase<Company, PXSelect<Company>.Config>.Select((PXGraph) this, Array.Empty<object>()));
    PXResultset<PX.Objects.AR.ARSetup>.op_Implicit(PXSelectBase<PX.Objects.AR.ARSetup, PXSelect<PX.Objects.AR.ARSetup>.Config>.Select((PXGraph) this, Array.Empty<object>()));
    ((PXSelectBase) this.ARFinChargeRecords).Cache.Clear();
    ((PXProcessing<ARFinChargesApplyMaint.ARFinChargesDetails>) this.ARFinChargeRecords).SetProcessAllEnabled(false);
    ((PXProcessing<ARFinChargesApplyMaint.ARFinChargesDetails>) this.ARFinChargeRecords).SetProcessEnabled(false);
    // ISSUE: method pointer
    PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) cDisplayClass240, __methodptr(\u003CCalculate\u003Eb__0)));
    return adapter.Get();
  }

  protected virtual void CalculateFinancialCharges(
    ARFinChargesApplyMaint.ARFinChargesApplyParameters filter)
  {
    ((PXSelectBase) this.Filter).Cache.Clear();
    ((PXSelectBase<ARFinChargesApplyMaint.ARFinChargesApplyParameters>) this.Filter).Insert(filter);
    PXResultset<Company>.op_Implicit(PXSelectBase<Company, PXSelect<Company>.Config>.Select((PXGraph) this, Array.Empty<object>()));
    PX.Objects.AR.ARSetup arSetup = PXResultset<PX.Objects.AR.ARSetup>.op_Implicit(PXSelectBase<PX.Objects.AR.ARSetup, PXSelect<PX.Objects.AR.ARSetup>.Config>.Select((PXGraph) this, Array.Empty<object>()));
    this.AppendFilterRestrictions((PXSelectBase<Customer>) this.CustomersInStatementCycle);
    ICollection<int?> chargeableInvoices = this.GetCustomerIDsWithoutChargeableInvoices();
    foreach (PXResult<Customer, PX.Objects.CR.Location, CustomerClass, ARStatementCycle> pxResult1 in ((PXSelectBase<Customer>) this.CustomersInStatementCycle).Select(Array.Empty<object>()))
    {
      Customer cust = PXResult<Customer, PX.Objects.CR.Location, CustomerClass, ARStatementCycle>.op_Implicit(pxResult1);
      PX.Objects.CR.Location location = PXResult<Customer, PX.Objects.CR.Location, CustomerClass, ARStatementCycle>.op_Implicit(pxResult1);
      CustomerClass customerClass = PXResult<Customer, PX.Objects.CR.Location, CustomerClass, ARStatementCycle>.op_Implicit(pxResult1);
      ARStatementCycle arStatementCycle = PXResult<Customer, PX.Objects.CR.Location, CustomerClass, ARStatementCycle>.op_Implicit(pxResult1);
      if (!chargeableInvoices.Contains(cust.BAccountID))
      {
        bool? nullable1 = arSetup.DefFinChargeFromCycle;
        ARFinCharge arFinCharge;
        if (nullable1.GetValueOrDefault())
        {
          nullable1 = arStatementCycle.FinChargeApply;
          if (nullable1.GetValueOrDefault())
            arFinCharge = PXResultset<ARFinCharge>.op_Implicit(PXSelectBase<ARFinCharge, PXSelect<ARFinCharge, Where<ARFinCharge.finChargeID, Equal<Required<ARFinCharge.finChargeID>>>>.Config>.Select((PXGraph) this, new object[1]
            {
              (object) arStatementCycle.FinChargeID
            }));
          else
            continue;
        }
        else
        {
          nullable1 = customerClass.FinChargeApply;
          if (nullable1.GetValueOrDefault())
            arFinCharge = PXResultset<ARFinCharge>.op_Implicit(PXSelectBase<ARFinCharge, PXSelect<ARFinCharge, Where<ARFinCharge.finChargeID, Equal<Required<ARFinCharge.finChargeID>>>>.Config>.Select((PXGraph) this, new object[1]
            {
              (object) customerClass.FinChargeID
            }));
          else
            continue;
        }
        if (arFinCharge != null && arFinCharge.FinChargeID != null)
        {
          bool flag1 = this.CheckForOpenPayments((PXGraph) this, cust.BAccountID);
          Dictionary<ARFinChargesApplyMaint.ARDocKey, ARInvoice> dictionary1 = new Dictionary<ARFinChargesApplyMaint.ARDocKey, ARInvoice>();
          Dictionary<ARFinChargesApplyMaint.ARDocKey, ARInvoice> dictionary2 = new Dictionary<ARFinChargesApplyMaint.ARDocKey, ARInvoice>();
          int? calculationMethod1 = arFinCharge.CalculationMethod;
          int num1 = 0;
          if (calculationMethod1.GetValueOrDefault() == num1 & calculationMethod1.HasValue)
          {
            foreach (PXResult<ARInvoice, ARAdjust> pxResult2 in this.GetReleasedCustomerInvoicesToApplyOverdueCharge(filter, cust))
            {
              ARInvoice arInvoice1 = PXResult<ARInvoice, ARAdjust>.op_Implicit(pxResult2);
              ARAdjust adjustment = PXResult<ARInvoice, ARAdjust>.op_Implicit(pxResult2);
              if (!(arInvoice1.DocType == "INV") && !(arInvoice1.DocType == "DRM"))
              {
                nullable1 = arSetup.FinChargeOnCharge;
                if (!nullable1.GetValueOrDefault() || !(arInvoice1.DocType == "FCH"))
                  continue;
              }
              ARFinChargesApplyMaint.ARDocKey key = new ARFinChargesApplyMaint.ARDocKey(arInvoice1.DocType, arInvoice1.RefNbr);
              DateTime? lastFinChargeDate = arInvoice1.LastFinChargeDate;
              DateTime? finChargeDate = filter.FinChargeDate;
              if ((lastFinChargeDate.HasValue & finChargeDate.HasValue ? (lastFinChargeDate.GetValueOrDefault() >= finChargeDate.GetValueOrDefault() ? 1 : 0) : 0) == 0 && !dictionary2.ContainsKey(key))
              {
                ARInvoice copy;
                if (dictionary1.ContainsKey(key))
                {
                  copy = dictionary1[key];
                  if (adjustment.AdjgDocType == "SMB")
                  {
                    dictionary2[key] = arInvoice1;
                    dictionary1.Remove(key);
                    continue;
                  }
                }
                else
                {
                  copy = PXCache<ARInvoice>.CreateCopy(arInvoice1);
                  copy.CuryDocBal = new Decimal?(0M);
                  copy.DocBal = new Decimal?(0M);
                  dictionary1[key] = copy;
                }
                ARInvoice arInvoice2 = copy;
                Decimal? nullable2 = arInvoice2.DocBal;
                Decimal? nullable3 = this.AdjustDocumentBalance(adjustment);
                arInvoice2.DocBal = nullable2.HasValue & nullable3.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + nullable3.GetValueOrDefault()) : new Decimal?();
                ARInvoice arInvoice3 = copy;
                Decimal? curyDocBal = arInvoice3.CuryDocBal;
                nullable2 = this.AdjustCuryDocumentBalance(adjustment);
                arInvoice3.CuryDocBal = curyDocBal.HasValue & nullable2.HasValue ? new Decimal?(curyDocBal.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?();
              }
            }
          }
          int? calculationMethod2 = arFinCharge.CalculationMethod;
          int num2 = 0;
          PXResultset<ARInvoice> pxResultset;
          if (calculationMethod2.GetValueOrDefault() == num2 & calculationMethod2.HasValue)
          {
            pxResultset = this.GetOpenCustomerInvoicesToApplyOverdueCharge(filter, cust);
          }
          else
          {
            calculationMethod2 = arFinCharge.CalculationMethod;
            pxResultset = calculationMethod2.GetValueOrDefault() != 1 ? this.GetCloseCustomerInvoicesToApplyOverdueCharge(filter, cust) : this.GetCustomerInvoicesToApplyOverdueCharge(filter, cust);
          }
          foreach (PXResult<ARInvoice> pxResult3 in pxResultset)
          {
            ARInvoice arInvoice4 = PXResult<ARInvoice>.op_Implicit(pxResult3);
            if (!(arInvoice4.DocType == "INV") && !(arInvoice4.DocType == "DRM"))
            {
              nullable1 = arSetup.FinChargeOnCharge;
              if (!nullable1.GetValueOrDefault() || !(arInvoice4.DocType == "FCH"))
                continue;
            }
            DateTime? lastFinChargeDate = arInvoice4.LastFinChargeDate;
            DateTime? finChargeDate = filter.FinChargeDate;
            if ((lastFinChargeDate.HasValue & finChargeDate.HasValue ? (lastFinChargeDate.GetValueOrDefault() >= finChargeDate.GetValueOrDefault() ? 1 : 0) : 0) == 0)
            {
              ARFinChargesApplyMaint.ARDocKey key = new ARFinChargesApplyMaint.ARDocKey(arInvoice4.DocType, arInvoice4.RefNbr);
              ARInvoice arInvoice5 = dictionary1.ContainsKey(key) ? dictionary1[key] : PXCache<ARInvoice>.CreateCopy(arInvoice4);
              dictionary1[key] = arInvoice5;
              foreach (PXResult<ARAdjust> pxResult4 in PXSelectBase<ARAdjust, PXSelect<ARAdjust, Where<ARAdjust.adjdDocType, Equal<Required<ARAdjust.adjdDocType>>, And<ARAdjust.adjdRefNbr, Equal<Required<ARAdjust.adjdRefNbr>>, And<ARAdjust.adjgDocDate, Greater<Required<ARAdjust.adjgDocDate>>, And<ARAdjust.released, Equal<True>>>>>>.Config>.Select((PXGraph) this, new object[3]
              {
                (object) arInvoice4.DocType,
                (object) arInvoice4.RefNbr,
                (object) filter.FinChargeDate
              }))
              {
                ARAdjust adjustment = PXResult<ARAdjust>.op_Implicit(pxResult4);
                ARInvoice arInvoice6 = arInvoice5;
                Decimal? docBal = arInvoice6.DocBal;
                Decimal? nullable4 = this.AdjustDocumentBalance(adjustment);
                arInvoice6.DocBal = docBal.HasValue & nullable4.HasValue ? new Decimal?(docBal.GetValueOrDefault() + nullable4.GetValueOrDefault()) : new Decimal?();
                ARInvoice arInvoice7 = arInvoice5;
                nullable4 = arInvoice7.CuryDocBal;
                Decimal? nullable5 = this.AdjustCuryDocumentBalance(adjustment);
                arInvoice7.CuryDocBal = nullable4.HasValue & nullable5.HasValue ? new Decimal?(nullable4.GetValueOrDefault() + nullable5.GetValueOrDefault()) : new Decimal?();
              }
            }
          }
          foreach (ARInvoice aSrc in dictionary1.Values)
          {
            Decimal num3 = aSrc.DocBal.Value;
            Decimal num4 = aSrc.CuryDocBal.Value;
            PX.Objects.CM.CurrencyInfo currencyInfo = PXResultset<PX.Objects.CM.CurrencyInfo>.op_Implicit(PXSelectBase<PX.Objects.CM.CurrencyInfo, PXSelect<PX.Objects.CM.CurrencyInfo, Where<PX.Objects.CM.CurrencyInfo.curyInfoID, Equal<Required<PX.Objects.CM.CurrencyInfo.curyInfoID>>>>.Config>.Select((PXGraph) this, new object[1]
            {
              (object) aSrc.CuryInfoID
            }));
            bool? baseCurFlag;
            CurrencyRate currencyRate;
            if (!(currencyInfo.CuryID == currencyInfo.BaseCuryID))
            {
              baseCurFlag = arFinCharge.BaseCurFlag;
              if (!baseCurFlag.GetValueOrDefault())
              {
                object[] objArray = new object[4]
                {
                  (object) currencyInfo.CuryID,
                  (object) currencyInfo.BaseCuryID,
                  (object) cust.CuryRateTypeID,
                  (object) filter.FinChargeDate
                };
                if ((currencyRate = PXResultset<CurrencyRate>.op_Implicit(PXSelectBase<CurrencyRate, PXSelect<CurrencyRate, Where<CurrencyRate.fromCuryID, Equal<Required<CurrencyRate.fromCuryID>>, And<CurrencyRate.toCuryID, Equal<Required<CurrencyRate.toCuryID>>, And<CurrencyRate.curyRateType, Equal<Required<CurrencyRate.curyRateType>>, And<CurrencyRate.curyEffDate, LessEqual<Required<CurrencyRate.curyEffDate>>>>>>, OrderBy<Desc<CurrencyRate.curyEffDate>>>.Config>.Select((PXGraph) this, objArray))) == null)
                  throw new PXException("Rate is not defined for rate type '{0}' for currency '{1}' for this date!", new object[2]
                  {
                    (object) cust.CuryRateTypeID,
                    (object) currencyInfo.CuryID
                  });
                goto label_50;
              }
            }
            currencyRate = new CurrencyRate();
            currencyRate.CuryRate = new Decimal?(1M);
            currencyRate.CuryMultDiv = "M";
label_50:
            int? calculationMethod3 = arFinCharge.CalculationMethod;
            int num5 = 0;
            Decimal? nullable6;
            if (calculationMethod3.GetValueOrDefault() == num5 & calculationMethod3.HasValue)
            {
              nullable6 = aSrc.CuryDocBal;
              Decimal num6 = 0M;
              if (nullable6.GetValueOrDefault() > num6 & nullable6.HasValue)
                goto label_55;
            }
            calculationMethod3 = arFinCharge.CalculationMethod;
            if (calculationMethod3.GetValueOrDefault() != 1)
            {
              calculationMethod3 = arFinCharge.CalculationMethod;
              if (calculationMethod3.GetValueOrDefault() == 2)
              {
                nullable6 = aSrc.CuryDocBal;
                Decimal num7 = 0M;
                if (!(nullable6.GetValueOrDefault() == num7 & nullable6.HasValue))
                  continue;
              }
              else
                continue;
            }
label_55:
            ARFinChargesApplyMaint.ARFinChargesDetails aDest1 = new ARFinChargesApplyMaint.ARFinChargesDetails();
            this.Copy(aDest1, aSrc);
            aDest1.Selected = new bool?(true);
            aDest1.FinChargeID = arFinCharge.FinChargeID;
            aDest1.TermsID = arFinCharge.TermsID;
            aDest1.ARAccountID = location.ARAccountID;
            aDest1.ARSubID = location.ARSubID;
            aDest1.LastPaymentDate = aSrc.LastPaymentDate;
            aDest1.LastChargeDate = aSrc.LastFinChargeDate;
            aDest1.DueDate = aSrc.DueDate;
            nullable6 = aSrc.CuryDocBal;
            Decimal num8 = 0M;
            if (nullable6.GetValueOrDefault() == num8 & nullable6.HasValue)
            {
              DateTime? nullable7 = aSrc.LastFinChargeDate;
              if (nullable7.HasValue)
              {
                nullable7 = aDest1.LastPaymentDate;
                if (nullable7.HasValue)
                {
                  nullable7 = aSrc.LastFinChargeDate;
                  DateTime? lastPaymentDate = aDest1.LastPaymentDate;
                  if ((nullable7.HasValue & lastPaymentDate.HasValue ? (nullable7.GetValueOrDefault() > lastPaymentDate.GetValueOrDefault() ? 1 : 0) : 0) != 0)
                    continue;
                }
              }
            }
            nullable6 = aSrc.CuryDocBal;
            Decimal num9 = 0M;
            DateTime? nullable8;
            TimeSpan timeSpan;
            if (nullable6.GetValueOrDefault() == num9 & nullable6.HasValue)
            {
              DateTime? lastPaymentDate = aSrc.LastPaymentDate;
              nullable8 = filter.FinChargeDate;
              if ((lastPaymentDate.HasValue & nullable8.HasValue ? (lastPaymentDate.GetValueOrDefault() <= nullable8.GetValueOrDefault() ? 1 : 0) : 0) != 0)
              {
                nullable8 = aSrc.LastPaymentDate;
                DateTime? nullable9 = aSrc.LastFinChargeDate ?? aSrc.DueDate;
                timeSpan = (nullable8.HasValue & nullable9.HasValue ? new TimeSpan?(nullable8.GetValueOrDefault() - nullable9.GetValueOrDefault()) : new TimeSpan?()).Value;
                goto label_63;
              }
            }
            DateTime? finChargeDate = filter.FinChargeDate;
            DateTime? nullable10 = aSrc.LastFinChargeDate ?? aSrc.DueDate;
            timeSpan = (finChargeDate.HasValue & nullable10.HasValue ? new TimeSpan?(finChargeDate.GetValueOrDefault() - nullable10.GetValueOrDefault()) : new TimeSpan?()).Value;
label_63:
            aDest1.OverdueDays = new short?(timeSpan.Days > 0 ? (short) timeSpan.Days : (short) 0);
            baseCurFlag = arFinCharge.BaseCurFlag;
            if (baseCurFlag.GetValueOrDefault())
            {
              aDest1.FinChargeCuryID = currencyInfo.BaseCuryID;
              aDest1.CuryRate = new Decimal?(1M);
              aDest1.CuryMultDiv = "M";
              aDest1.CuryRateTypeID = cust.CuryRateTypeID;
              ARFinChargesApplyMaint.ARFinChargesDetails aDest2 = aDest1;
              ARFinCharge aDef = arFinCharge;
              nullable6 = aSrc.DocBal;
              Decimal aDocBalance = nullable6.Value;
              nullable8 = filter.FinChargeDate;
              DateTime calculationDate = nullable8.Value;
              this.CalcFCAmount(aDest2, aDef, aDocBalance, (PXGraph) this, calculationDate);
              ARFinChargesApplyMaint.ARFinChargesDetails finChargesDetails = aDest1;
              PXCache cach = ((PXGraph) this).Caches[typeof (ARInvoice)];
              ARInvoice row = aSrc;
              nullable6 = aDest1.FinChargeAmt;
              Decimal val = nullable6.Value;
              Decimal? nullable11 = new Decimal?(PXCurrencyAttribute.Round(cach, (object) row, val, CMPrecision.BASECURY));
              finChargesDetails.FinChargeAmt = nullable11;
            }
            else
            {
              aDest1.FinChargeCuryID = currencyInfo.CuryID;
              aDest1.CuryRate = currencyRate.CuryRate;
              aDest1.CuryMultDiv = currencyRate.CuryMultDiv;
              aDest1.CuryRateTypeID = cust.CuryRateTypeID;
              ARFinChargesApplyMaint.ARFinChargesDetails aDest3 = aDest1;
              ARFinCharge aDef = arFinCharge;
              nullable6 = aSrc.CuryDocBal;
              Decimal aDocBalance = nullable6.Value;
              nullable8 = filter.FinChargeDate;
              DateTime calculationDate = nullable8.Value;
              this.CalcFCAmount(aDest3, aDef, aDocBalance, (PXGraph) this, calculationDate);
              ARFinChargesApplyMaint.ARFinChargesDetails finChargesDetails = aDest1;
              PXCache cach = ((PXGraph) this).Caches[typeof (ARInvoice)];
              ARInvoice row = aSrc;
              nullable6 = aDest1.FinChargeAmt;
              Decimal val = nullable6.Value;
              Decimal? nullable12 = new Decimal?(PXCurrencyAttribute.Round(cach, (object) row, val, CMPrecision.TRANCURY));
              finChargesDetails.FinChargeAmt = nullable12;
            }
            nullable6 = aDest1.FinChargeAmt;
            Decimal num10 = 0M;
            if (!(nullable6.GetValueOrDefault() <= num10 & nullable6.HasValue))
            {
              aDest1.LastFCRefNbr = this.FindUnreleasedChargeForDoc((PXGraph) this, aSrc.DocType, aSrc.RefNbr);
              aDest1.LastFCDocType = "FCH";
              aDest1.FinChargeDate = filter.FinChargeDate;
              aDest1.FinPeriodID = filter.FinPeriodID;
              bool flag2 = !string.IsNullOrEmpty(aDest1.LastFCRefNbr);
              aDest1.HasUnreleasedCharges = new bool?(flag2);
              aDest1.CustomerHasOpenPayments = new bool?(flag1);
              aDest1.Selected = new bool?(!(flag2 | flag1));
              ((PXSelectBase<ARFinChargesApplyMaint.ARFinChargesDetails>) this.ARFinChargeRecords).Insert(aDest1);
            }
          }
        }
      }
    }
  }

  protected virtual PXResultset<ARInvoice> GetCloseCustomerInvoicesToApplyOverdueCharge(
    ARFinChargesApplyMaint.ARFinChargesApplyParameters filter,
    Customer cust)
  {
    return PXSelectBase<ARInvoice, PXSelect<ARInvoice, Where<ARInvoice.released, Equal<True>, And<ARInvoice.customerID, Equal<Required<ARInvoice.customerID>>, And<ARInvoice.dueDate, LessEqual<Required<ARInvoice.dueDate>>, And<ARRegister.openDoc, Equal<False>, And<ARInvoice.applyOverdueCharge, Equal<True>>>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) cust.BAccountID,
      (object) filter.FinChargeDate
    });
  }

  protected virtual PXResultset<ARInvoice> GetCustomerInvoicesToApplyOverdueCharge(
    ARFinChargesApplyMaint.ARFinChargesApplyParameters filter,
    Customer cust)
  {
    return PXSelectBase<ARInvoice, PXSelect<ARInvoice, Where<ARInvoice.released, Equal<True>, And<ARInvoice.pendingPPD, NotEqual<True>, And<ARInvoice.customerID, Equal<Required<ARInvoice.customerID>>, And<ARInvoice.dueDate, LessEqual<Required<ARInvoice.dueDate>>, And<ARInvoice.applyOverdueCharge, Equal<True>>>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) cust.BAccountID,
      (object) filter.FinChargeDate
    });
  }

  protected virtual PXResultset<ARInvoice> GetOpenCustomerInvoicesToApplyOverdueCharge(
    ARFinChargesApplyMaint.ARFinChargesApplyParameters filter,
    Customer cust)
  {
    return PXSelectBase<ARInvoice, PXSelect<ARInvoice, Where<ARInvoice.released, Equal<True>, And<ARInvoice.pendingPPD, NotEqual<True>, And<ARInvoice.customerID, Equal<Required<ARInvoice.customerID>>, And<ARInvoice.dueDate, LessEqual<Required<ARInvoice.dueDate>>, And<ARRegister.openDoc, Equal<True>, And<ARInvoice.applyOverdueCharge, Equal<True>>>>>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) cust.BAccountID,
      (object) filter.FinChargeDate
    });
  }

  protected virtual PXResultset<ARInvoice> GetReleasedCustomerInvoicesToApplyOverdueCharge(
    ARFinChargesApplyMaint.ARFinChargesApplyParameters filter,
    Customer cust)
  {
    return PXSelectBase<ARInvoice, PXSelectJoin<ARInvoice, InnerJoin<ARAdjust, On<ARInvoice.docType, Equal<ARAdjust.adjdDocType>, And<ARInvoice.refNbr, Equal<ARAdjust.adjdRefNbr>>>>, Where<ARInvoice.customerID, Equal<Required<ARInvoice.customerID>>, And<ARInvoice.dueDate, Less<Required<ARInvoice.dueDate>>, And<ARAdjust.adjgDocDate, GreaterEqual<Required<ARAdjust.adjgDocDate>>, And<ARInvoice.released, Equal<True>, And<ARInvoice.openDoc, Equal<False>, And<ARInvoice.applyOverdueCharge, Equal<True>>>>>>>>.Config>.Select((PXGraph) this, new object[3]
    {
      (object) cust.BAccountID,
      (object) filter.FinChargeDate,
      (object) filter.FinChargeDate
    });
  }

  protected virtual Decimal? AdjustDocumentBalance(ARAdjust adjustment)
  {
    Decimal? adjAmt = adjustment.AdjAmt;
    Decimal? nullable1 = adjustment.AdjDiscAmt;
    Decimal? nullable2 = adjAmt.HasValue & nullable1.HasValue ? new Decimal?(adjAmt.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
    Decimal? nullable3 = adjustment.AdjWOAmt;
    Decimal? nullable4;
    if (!(nullable2.HasValue & nullable3.HasValue))
    {
      nullable1 = new Decimal?();
      nullable4 = nullable1;
    }
    else
      nullable4 = new Decimal?(nullable2.GetValueOrDefault() + nullable3.GetValueOrDefault());
    Decimal? nullable5 = nullable4;
    Decimal? nullable6;
    if (!adjustment.ReverseGainLoss.GetValueOrDefault())
    {
      nullable6 = adjustment.RGOLAmt;
    }
    else
    {
      nullable3 = adjustment.RGOLAmt;
      nullable6 = nullable3.HasValue ? new Decimal?(-nullable3.GetValueOrDefault()) : new Decimal?();
    }
    Decimal? nullable7 = nullable6;
    if (nullable5.HasValue & nullable7.HasValue)
      return new Decimal?(nullable5.GetValueOrDefault() + nullable7.GetValueOrDefault());
    nullable3 = new Decimal?();
    return nullable3;
  }

  protected virtual Decimal? AdjustCuryDocumentBalance(ARAdjust adjustment)
  {
    Decimal? curyAdjdAmt = adjustment.CuryAdjdAmt;
    Decimal? nullable1 = adjustment.CuryAdjdDiscAmt;
    Decimal? nullable2 = curyAdjdAmt.HasValue & nullable1.HasValue ? new Decimal?(curyAdjdAmt.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?();
    Decimal? curyAdjdWoAmt = adjustment.CuryAdjdWOAmt;
    if (nullable2.HasValue & curyAdjdWoAmt.HasValue)
      return new Decimal?(nullable2.GetValueOrDefault() + curyAdjdWoAmt.GetValueOrDefault());
    nullable1 = new Decimal?();
    return nullable1;
  }

  protected virtual void AppendFilterRestrictions(PXSelectBase<Customer> query)
  {
    ARFinChargesApplyMaint.ARFinChargesApplyParameters current = ((PXSelectBase<ARFinChargesApplyMaint.ARFinChargesApplyParameters>) this.Filter).Current;
    if ((current != null ? (current.CustomerID.HasValue ? 1 : 0) : 0) != 0)
      query.WhereAnd<Where<Customer.bAccountID, Equal<Current<ARFinChargesApplyMaint.ARFinChargesApplyParameters.customerID>>>>();
    if (((PXSelectBase<ARFinChargesApplyMaint.ARFinChargesApplyParameters>) this.Filter).Current?.CustomerClassID != null)
      query.WhereAnd<Where<Customer.customerClassID, Equal<Current<ARFinChargesApplyMaint.ARFinChargesApplyParameters.customerClassID>>>>();
    if (((PXSelectBase<ARFinChargesApplyMaint.ARFinChargesApplyParameters>) this.Filter).Current?.StatementCycle == null)
      return;
    query.WhereAnd<Where<Customer.statementCycleId, Equal<Current<ARFinChargesApplyMaint.ARFinChargesApplyParameters.statementCycle>>>>();
  }

  /// <remarks>
  /// Used for coarse filtration of customers in the inner loop of <see cref="M:PX.Objects.AR.ARFinChargesApplyMaint.Calculate(PX.Data.PXAdapter)" />
  /// so as to skip (and avoid redundant DB selects / processing) those customers who
  /// don't have any chargeable documents. Intentionally done using a separate query
  /// so as not to overload performance / memory consumption of the <see cref="M:PX.Objects.AR.ARFinChargesApplyMaint.Calculate(PX.Data.PXAdapter)" />'s
  /// main query.
  /// </remarks>
  /// <returns>
  /// IDs of the customers that do not have any released chargeable invoices and that
  /// satisfy the current filter values.
  /// </returns>
  protected virtual ICollection<int?> GetCustomerIDsWithoutChargeableInvoices()
  {
    PXSelectBase<Customer> query = (PXSelectBase<Customer>) new PXSelectJoin<Customer, LeftJoin<ARInvoice, On<ARInvoice.customerID, Equal<Customer.bAccountID>, And<ARInvoice.released, Equal<True>, And<ARInvoice.dueDate, LessEqual<Required<ARInvoice.dueDate>>, And<ARInvoice.applyOverdueCharge, Equal<True>>>>>>, Where<Customer.finChargeApply, Equal<True>, And<Customer.status, NotEqual<CustomerStatus.inactive>, And<Customer.status, NotEqual<CustomerStatus.hold>, And<ARInvoice.refNbr, IsNull>>>>>((PXGraph) this);
    this.AppendFilterRestrictions(query);
    IEnumerable<int?> collection;
    using (new PXFieldScope(((PXSelectBase) query).View, new System.Type[2]
    {
      typeof (Customer.bAccountID),
      typeof (Customer.acctCD)
    }))
      collection = GraphHelper.RowCast<Customer>((IEnumerable) query.Select(new object[1]
      {
        (object) (DateTime?) ((PXSelectBase<ARFinChargesApplyMaint.ARFinChargesApplyParameters>) this.Filter).Current?.FinChargeDate
      })).Select<Customer, int?>((Func<Customer, int?>) (customer => customer.BAccountID));
    return (ICollection<int?>) new HashSet<int?>(collection);
  }

  public static void CreateFCDoc(
    List<ARFinChargesApplyMaint.ARFinChargesDetails> list,
    ARFinChargesApplyMaint.ARFinChargesApplyParameters filter)
  {
    ARInvoiceEntry instance1 = PXGraph.CreateInstance<ARInvoiceEntry>();
    Numbering numbering = PXResultset<Numbering>.op_Implicit(PXSelectBase<Numbering, PXSelect<Numbering, Where<Numbering.numberingID, Equal<Required<Numbering.numberingID>>>>.Config>.Select((PXGraph) instance1, new object[1]
    {
      (object) ((PXSelectBase<PX.Objects.AR.ARSetup>) instance1.ARSetup).Current.FinChargeNumberingID
    }));
    bool? nullable1;
    if (numbering != null)
    {
      nullable1 = numbering.UserNumbering;
      if (nullable1.GetValueOrDefault())
        throw new PXException("Manual numbering cannot be used for documents of the Overdue Charge type. Clear the Manual Numbering check box for the {0} numbering sequence on the Numbering Sequences (CS201010) form.", new object[1]
        {
          (object) numbering.NumberingID
        });
    }
    FinPeriod byId = instance1.FinPeriodRepository.FindByID(PXAccess.GetParentOrganizationID(PXAccess.GetBranchID()), filter.FinPeriodID);
    instance1.FinPeriodUtils.CanPostToPeriod((IFinPeriod) byId).RaiseIfHasError();
    instance1.IsProcessingMode = true;
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    ((PXGraph) instance1).FieldVerifying.AddHandler<ARInvoice.customerID>(ARFinChargesApplyMaint.\u003C\u003Ec.\u003C\u003E9__34_0 ?? (ARFinChargesApplyMaint.\u003C\u003Ec.\u003C\u003E9__34_0 = new PXFieldVerifying((object) ARFinChargesApplyMaint.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CCreateFCDoc\u003Eb__34_0))));
    ((PXSelectBase<PX.Objects.AR.ARSetup>) instance1.ARSetup).Current.RequireControlTotal = new bool?(false);
    Customer customer = (Customer) null;
    ARFinCharge arFinCharge = (ARFinCharge) null;
    bool flag = false;
    Dictionary<string, ARFinChargesApplyMaint.MessageWithLevel> dictionary1 = new Dictionary<string, ARFinChargesApplyMaint.MessageWithLevel>();
    List<string> stringList = new List<string>();
    PXLongOperation.SetCustomInfo((object) dictionary1);
    list.Sort(new Comparison<ARFinChargesApplyMaint.ARFinChargesDetails>(ARFinChargesApplyMaint.Compare));
    Dictionary<string, DateTime> dictionary2 = new Dictionary<string, DateTime>();
    for (int index = 0; index < list.Count; ++index)
    {
      ARFinChargesApplyMaint.ARFinChargesDetails aT0 = list[index];
      DateTime? finChargeDate = aT0.FinChargeDate;
      if (finChargeDate.HasValue)
      {
        if (!string.IsNullOrEmpty(aT0.FinPeriodID))
        {
          try
          {
            nullable1 = aT0.HasUnreleasedCharges;
            if (nullable1.GetValueOrDefault())
              throw new PXException("At least one unreleased overdue charge document has been found for this document. Processing has been aborted.");
            if (arFinCharge == null || arFinCharge.FinChargeID != aT0.FinChargeID)
              arFinCharge = PXResultset<ARFinCharge>.op_Implicit(PXSelectBase<ARFinCharge, PXSelect<ARFinCharge, Where<ARFinCharge.finChargeID, Equal<Required<ARFinCharge.finChargeID>>>>.Config>.Select((PXGraph) instance1, new object[1]
              {
                (object) aT0.FinChargeID
              }));
            if (((PXSelectBase<ARInvoice>) instance1.Document).Current == null)
            {
              if (customer != null)
              {
                int? baccountId = customer.BAccountID;
                int? customerId = aT0.CustomerID;
                if (baccountId.GetValueOrDefault() == customerId.GetValueOrDefault() & baccountId.HasValue == customerId.HasValue)
                  goto label_15;
              }
              customer = PXResultset<Customer>.op_Implicit(PXSelectBase<Customer, PXSelect<Customer, Where<Customer.bAccountID, Equal<Required<Customer.bAccountID>>>>.Config>.Select((PXGraph) instance1, new object[1]
              {
                (object) aT0.CustomerID
              }));
label_15:
              ARInvoice arInvoice = new ARInvoice();
              arInvoice.DocType = "FCH";
              ARInvoice copy = PXCache<ARInvoice>.CreateCopy(((PXSelectBase<ARInvoice>) instance1.Document).Insert(arInvoice));
              copy.ARAccountID = aT0.ARAccountID;
              copy.ARSubID = aT0.ARSubID;
              copy.CustomerID = aT0.CustomerID;
              ARInvoice row = ((PXSelectBase<ARInvoice>) instance1.Document).Update(copy);
              ((PXSelectBase) instance1.Document).Cache.SetDefaultExt<ARInvoice.branchID>((object) row);
              row.CuryID = aT0.FinChargeCuryID;
              row.DocDate = filter.FinChargeDate;
              using (new PXLocaleScope(customer.LocaleName))
                row.DocDesc = PXMessages.LocalizeNoPrefix("Overdue Charge");
              FinPeriodIDAttribute.SetPeriodsByMaster<ARInvoice.finPeriodID>(((PXSelectBase) instance1.Document).Cache, (object) row, filter.FinPeriodID);
              row.TermsID = arFinCharge.TermsID;
              ((PXSelectBase<ARInvoice>) instance1.Document).Update(row);
            }
            Decimal? finChargeAmt = aT0.FinChargeAmt;
            Decimal? minFinChargeAmount = arFinCharge.MinFinChargeAmount;
            Decimal? nullable2;
            if (finChargeAmt.GetValueOrDefault() >= minFinChargeAmount.GetValueOrDefault() & finChargeAmt.HasValue & minFinChargeAmount.HasValue)
            {
              ARTran row = new ARTran();
              row.AccountID = arFinCharge.FinChargeAccountID;
              row.SubID = arFinCharge.FinChargeSubID;
              row.TranType = "FCH";
              row.TranDate = filter.FinChargeDate;
              row.CuryTranAmt = aT0.FinChargeAmt;
              row.CuryUnitPrice = aT0.FinChargeAmt;
              using (new PXLocaleScope(customer.LocaleName))
                row.TranDesc = $"{PXStringListAttribute.GetLocalizedLabel<ARFinChargesApplyMaint.ARFinChargesDetails.docType>(((PXGraph) instance1).Caches[typeof (ARFinChargesApplyMaint.ARFinChargesDetails)], (object) aT0)} {aT0.RefNbr}";
              row.LineType = "";
              FinPeriodIDAttribute.SetPeriodsByMaster<ARTran.finPeriodID>(((PXSelectBase) instance1.Transactions).Cache, (object) row, filter.FinPeriodID);
              row.Qty = new Decimal?(1M);
              row.Released = new bool?(false);
              row.DrCr = "C";
              row.TranClass = "";
              row.TaxCategoryID = arFinCharge.TaxCategoryID;
              row.Commissionable = new bool?(false);
              ARTran arTran = ((PXSelectBase<ARTran>) instance1.Transactions).Insert(row);
              if (string.IsNullOrEmpty(arFinCharge.TaxCategoryID))
              {
                ARTran copy = PXCache<ARTran>.CreateCopy(arTran);
                copy.TaxCategoryID = arFinCharge.TaxCategoryID;
                arTran = ((PXSelectBase<ARTran>) instance1.Transactions).Update(copy);
              }
              ((PXSelectBase<ARFinChargeTran>) instance1.finChargeTrans).Insert(new ARFinChargeTran()
              {
                TranType = arTran.TranType,
                RefNbr = arTran.RefNbr,
                LineNbr = arTran.LineNbr,
                OrigDocType = aT0.DocType,
                OrigRefNbr = aT0.RefNbr,
                FinChargeID = arFinCharge.FinChargeID
              });
              stringList.Add(aT0.RefNbr + aT0.DocType);
            }
            else
            {
              dictionary1.Add(aT0.RefNbr + aT0.DocType, new ARFinChargesApplyMaint.MessageWithLevel("This line will not be added to the Overdue Charge document because calculated charge amount is less than the threshold amount specified in the overdue charge code on the Overdue Charges (AR204500) form.", (PXErrorLevel) 3));
              nullable2 = aT0.CuryDocBal;
              Decimal num = 0M;
              if (nullable2.GetValueOrDefault() == num & nullable2.HasValue)
                PXDatabase.Update<ARInvoice>(new PXDataFieldParam[3]
                {
                  (PXDataFieldParam) new PXDataFieldAssign<ARInvoice.applyOverdueCharge>((object) false),
                  (PXDataFieldParam) new PXDataFieldRestrict<ARInvoice.docType>((PXDbType) 22, new int?(3), (object) aT0.DocType, (PXComp) 0),
                  (PXDataFieldParam) new PXDataFieldRestrict<ARInvoice.refNbr>((PXDbType) 12, new int?(15), (object) aT0.RefNbr, (PXComp) 0)
                });
            }
            if (index != list.Count - 1)
            {
              if (ARFinChargesApplyMaint.Compare(aT0, list[index + 1]) == 0)
                continue;
            }
            if (((PXSelectBase<ARFinChargeTran>) instance1.finChargeTrans).Any<ARFinChargeTran>())
            {
              ARInvoice current1 = ((PXSelectBase<ARInvoice>) instance1.Document).Current;
              nullable2 = arFinCharge.MinChargeDocumentAmt;
              Decimal num1 = 0M;
              if (nullable2.GetValueOrDefault() > num1 & nullable2.HasValue)
              {
                nullable2 = arFinCharge.MinChargeDocumentAmt;
                Decimal? curyDocBal = current1.CuryDocBal;
                if (nullable2.GetValueOrDefault() > curyDocBal.GetValueOrDefault() & nullable2.HasValue & curyDocBal.HasValue)
                  throw new PXException("The overdue charge document cannot be generated. The amount of overdue charges does not exceed the threshold amount required for generating the document.");
              }
              Decimal? feeAmount = arFinCharge.FeeAmount;
              if (feeAmount.HasValue)
              {
                feeAmount = arFinCharge.FeeAmount;
                Decimal num2 = 0M;
                if (!(feeAmount.GetValueOrDefault() == num2 & feeAmount.HasValue))
                {
                  ARTran row = new ARTran();
                  row.AccountID = arFinCharge.FeeAccountID;
                  row.SubID = arFinCharge.FeeSubID;
                  row.TranDate = filter.FinChargeDate;
                  row.CuryTranAmt = arFinCharge.FeeAmount;
                  row.CuryUnitPrice = arFinCharge.FeeAmount;
                  row.TranDesc = PXDBLocalizableStringAttribute.GetTranslation(((PXGraph) instance1).Caches[typeof (ARFinCharge)], (object) arFinCharge, typeof (ARFinCharge.feeDesc).Name, customer.LocaleName);
                  row.LineType = "";
                  FinPeriodIDAttribute.SetPeriodsByMaster<ARTran.finPeriodID>(((PXSelectBase) instance1.Transactions).Cache, (object) row, filter.FinPeriodID);
                  row.Qty = new Decimal?(1M);
                  row.Released = new bool?(false);
                  row.DrCr = "C";
                  row.TranClass = "";
                  row.TaxCategoryID = arFinCharge.TaxCategoryID;
                  row.Commissionable = new bool?(false);
                  ARTran arTran = ((PXSelectBase<ARTran>) instance1.Transactions).Insert(row);
                  if (string.IsNullOrEmpty(arFinCharge.TaxCategoryID))
                  {
                    ARTran copy = PXCache<ARTran>.CreateCopy(arTran);
                    copy.TaxCategoryID = arFinCharge.TaxCategoryID;
                    ((PXSelectBase<ARTran>) instance1.Transactions).Update(copy);
                  }
                }
              }
              ARInvoice current2 = ((PXSelectBase<ARInvoice>) instance1.Document).Current;
              current2.CuryOrigDocAmt = current2.CuryDocBal;
              PX.Objects.AR.ARSetup arSetup = PXResultset<PX.Objects.AR.ARSetup>.op_Implicit(PXSelectBase<PX.Objects.AR.ARSetup, PXSelect<PX.Objects.AR.ARSetup>.Config>.Select((PXGraph) instance1, Array.Empty<object>()));
              ARInvoice arInvoice = current2;
              nullable1 = current2.ApplyOverdueCharge;
              int num3;
              if (nullable1.Value)
              {
                nullable1 = arSetup.FinChargeOnCharge;
                num3 = nullable1.Value ? 1 : 0;
              }
              else
                num3 = 0;
              bool? nullable3 = new bool?(num3 != 0);
              arInvoice.ApplyOverdueCharge = nullable3;
              PXAccess.MasterCollection.Branch branch = PXAccess.GetBranch(((PXGraph) instance1).Accessinfo.BranchID);
              if (PXAccess.FeatureInstalled<FeaturesSet.multipleBaseCurrencies>() && !current2.BranchID.HasValue && customer.BaseCuryID != null && customer.BaseCuryID != branch.BaseCuryID)
                throw new PXException(PXMessages.LocalizeFormatNoPrefix("The document cannot be created because the base currency of the originating branch ({0}) differs from the base currency of the customer. To be able to process the document, select a current branch with the {1} base currency.", new object[2]
                {
                  (object) branch.BranchCD.Trim(),
                  (object) customer.BaseCuryID
                }));
              ((PXAction) instance1.Save).Press();
            }
            if (!dictionary2.ContainsKey(customer.StatementCycleId))
            {
              Dictionary<string, DateTime> dictionary3 = dictionary2;
              string statementCycleId = customer.StatementCycleId;
              finChargeDate = aT0.FinChargeDate;
              DateTime dateTime = finChargeDate.Value;
              dictionary3[statementCycleId] = dateTime;
            }
            ((PXGraph) instance1).Clear();
            foreach (string key in stringList)
            {
              if (dictionary1.ContainsKey(key))
              {
                dictionary1[key].Message = "The record has been processed successfully.";
                dictionary1[key].Level = (PXErrorLevel) 1;
              }
              else
                dictionary1.Add(key, new ARFinChargesApplyMaint.MessageWithLevel("The record has been processed successfully.", (PXErrorLevel) 1));
            }
            stringList.Clear();
            continue;
          }
          catch (Exception ex)
          {
            ((PXGraph) instance1).Clear();
            if (!stringList.Contains(aT0.RefNbr + aT0.DocType))
              stringList.Add(aT0.RefNbr + aT0.DocType);
            foreach (string key in stringList)
            {
              if (dictionary1.ContainsKey(key))
              {
                dictionary1[key].Message = ex.Message;
                dictionary1[key].Level = (PXErrorLevel) 5;
              }
              else
                dictionary1.Add(key, new ARFinChargesApplyMaint.MessageWithLevel(ex.Message, (PXErrorLevel) 5));
            }
            stringList.Clear();
            flag = true;
            continue;
          }
        }
      }
      throw new PXException("Overdue Charge Date and Fin. Period are required");
    }
    ARStatementMaint instance2 = PXGraph.CreateInstance<ARStatementMaint>();
    foreach (KeyValuePair<string, DateTime> keyValuePair in dictionary2)
    {
      ARStatementCycle arStatementCycle = PXResultset<ARStatementCycle>.op_Implicit(((PXSelectBase<ARStatementCycle>) instance2.ARStatementCycleRecord).Search<ARStatementCycle.statementCycleId>((object) keyValuePair.Key, Array.Empty<object>()));
      ((PXSelectBase<ARStatementCycle>) instance2.ARStatementCycleRecord).Current = arStatementCycle;
      DateTime? lastFinChrgDate = arStatementCycle.LastFinChrgDate;
      if (lastFinChrgDate.HasValue)
      {
        lastFinChrgDate = arStatementCycle.LastFinChrgDate;
        DateTime dateTime = keyValuePair.Value;
        if ((lastFinChrgDate.HasValue ? (lastFinChrgDate.GetValueOrDefault() >= dateTime ? 1 : 0) : 0) != 0)
          goto label_75;
      }
      arStatementCycle.LastFinChrgDate = new DateTime?(keyValuePair.Value);
label_75:
      ((PXSelectBase<ARStatementCycle>) instance2.ARStatementCycleRecord).Update(arStatementCycle);
      ((PXAction) instance2.Save).Press();
    }
    if (flag)
      throw new PXException("At least one item has not been processed.");
  }

  private static bool CheckForUnreleasedCharges(PXGraph aGraph, int aCustomerID)
  {
    return PXResultset<ARRegister>.op_Implicit(PXSelectBase<ARRegister, PXSelect<ARRegister, Where<ARRegister.docType, Equal<ARDocType.finCharge>, And<ARRegister.released, Equal<BitOff>, And<ARRegister.customerID, Equal<Required<ARRegister.customerID>>>>>>.Config>.SelectWindowed(aGraph, 0, 1, new object[1]
    {
      (object) aCustomerID
    })) != null;
  }

  protected virtual string FindUnreleasedChargeForDoc(
    PXGraph aGraph,
    string aDocType,
    string aRefNbr)
  {
    ARInvoice arInvoice = PXResultset<ARInvoice>.op_Implicit(PXSelectBase<ARInvoice, PXSelectJoin<ARInvoice, InnerJoin<ARFinChargeTran, On<ARInvoice.docType, Equal<ARFinChargeTran.tranType>, And<ARInvoice.refNbr, Equal<ARFinChargeTran.refNbr>>>>, Where<ARFinChargeTran.origDocType, Equal<Required<ARFinChargeTran.origDocType>>, And<ARFinChargeTran.origRefNbr, Equal<Required<ARFinChargeTran.origRefNbr>>, And<ARInvoice.released, Equal<False>>>>>.Config>.SelectWindowed(aGraph, 0, 1, new object[2]
    {
      (object) aDocType,
      (object) aRefNbr
    }));
    return arInvoice != null && !string.IsNullOrEmpty(arInvoice.RefNbr) ? arInvoice.RefNbr : (string) null;
  }

  protected virtual bool CheckForOpenPayments(PXGraph aGraph, int? aCustomerID)
  {
    return PXResultset<ARPayment>.op_Implicit(PXSelectBase<ARPayment, PXSelect<ARPayment, Where<ARPayment.customerID, Equal<Required<ARPayment.customerID>>, And<ARPayment.openDoc, Equal<True>>>>.Config>.SelectWindowed(aGraph, 0, 1, new object[1]
    {
      (object) aCustomerID
    })) != null;
  }

  protected virtual void CalcFCAmount(
    ARFinChargesApplyMaint.ARFinChargesDetails aDest,
    ARFinCharge aDef,
    Decimal aDocBalance,
    PXGraph graph,
    DateTime calculationDate)
  {
    Decimal num1 = 0M;
    Decimal num2 = aDest.SampleCuryRate.Value;
    DateTime dateTime1 = !aDest.LastChargeDate.HasValue || !(aDest.LastChargeDate.Value > aDest.DueDate.Value) ? aDest.DueDate.Value : aDest.LastChargeDate.Value;
    bool? nullable1 = aDef.PercentFlag;
    Decimal? nullable2;
    Decimal? nullable3;
    if (nullable1.Value)
    {
      List<ARFinChargePercent> finChargePercentList = new List<ARFinChargePercent>();
      ARFinChargePercent finChargePercent1 = PXResultset<ARFinChargePercent>.op_Implicit(PXSelectBase<ARFinChargePercent, PXSelect<ARFinChargePercent, Where<ARFinChargePercent.finChargeID, Equal<Required<ARFinCharge.finChargeID>>, And<ARFinChargePercent.beginDate, LessEqual<Required<ARFinChargePercent.beginDate>>>>, OrderBy<Desc<ARFinChargePercent.beginDate>>>.Config>.SelectWindowed(graph, 0, 1, new object[2]
      {
        (object) aDef.FinChargeID,
        (object) dateTime1
      }));
      if (finChargePercent1 != null)
        finChargePercentList.Add(finChargePercent1);
      foreach (PXResult<ARFinChargePercent> pxResult in PXSelectBase<ARFinChargePercent, PXSelect<ARFinChargePercent, Where<ARFinChargePercent.finChargeID, Equal<Required<ARFinCharge.finChargeID>>, And<ARFinChargePercent.beginDate, Greater<Required<ARFinChargesApplyMaint.ARFinChargesDetails.docDate>>, And<ARFinChargePercent.beginDate, LessEqual<Required<ARFinChargePercent.beginDate>>>>>>.Config>.Select(graph, new object[3]
      {
        (object) aDef.FinChargeID,
        (object) dateTime1,
        (object) calculationDate
      }))
      {
        ARFinChargePercent finChargePercent2 = PXResult<ARFinChargePercent>.op_Implicit(pxResult);
        finChargePercentList.Add(finChargePercent2);
      }
      if (finChargePercentList.Count < 1)
        throw new PXException("Effective percent rate is not found.");
      for (int index = 0; index < finChargePercentList.Count; ++index)
      {
        DateTime? nullable4 = finChargePercentList[index].BeginDate;
        DateTime dateTime2 = nullable4.Value;
        DateTime dateTime3;
        if (!(dateTime2.AddDays(-1.0) > dateTime1))
        {
          dateTime3 = dateTime1;
        }
        else
        {
          nullable4 = finChargePercentList[index].BeginDate;
          dateTime2 = nullable4.Value;
          dateTime3 = dateTime2.AddDays(-1.0);
        }
        DateTime dateTime4 = dateTime3;
        DateTime dateTime5;
        if (index + 1 < finChargePercentList.Count)
        {
          nullable4 = finChargePercentList[index + 1].BeginDate;
          dateTime2 = nullable4.Value;
          dateTime5 = dateTime2.AddDays(-1.0);
        }
        else
          dateTime5 = calculationDate;
        Decimal num3 = num1;
        Decimal num4 = aDocBalance * (Decimal) (dateTime5 - dateTime4).Days;
        nullable2 = finChargePercentList[index].FinChargePercent;
        nullable3 = nullable2.HasValue ? new Decimal?(num4 * nullable2.GetValueOrDefault()) : new Decimal?();
        Decimal num5 = (Decimal) 36500;
        Decimal? nullable5;
        if (!nullable3.HasValue)
        {
          nullable2 = new Decimal?();
          nullable5 = nullable2;
        }
        else
          nullable5 = new Decimal?(nullable3.GetValueOrDefault() / num5);
        nullable2 = nullable5;
        Decimal num6 = nullable2.Value;
        num1 = num3 + num6;
        int? calculationMethod = aDef.CalculationMethod;
        int num7 = 0;
        if (!(calculationMethod.GetValueOrDefault() == num7 & calculationMethod.HasValue))
        {
          foreach (PXResult<ARAdjust> pxResult in PXSelectBase<ARAdjust, PXSelect<ARAdjust, Where<ARAdjust.adjdDocType, Equal<Required<ARInvoice.docType>>, And<ARAdjust.adjdRefNbr, Equal<Required<ARInvoice.refNbr>>, And<ARAdjust.adjgDocDate, LessEqual<Required<ARInvoice.docDate>>, And<ARAdjust.adjgDocDate, GreaterEqual<Required<ARInvoice.lastFinChargeDate>>, And<ARAdjust.released, Equal<True>>>>>>>.Config>.Select(graph, new object[4]
          {
            (object) aDest.DocType,
            (object) aDest.RefNbr,
            (object) calculationDate,
            (object) dateTime4
          }))
          {
            ARAdjust arAdjust = PXResult<ARAdjust>.op_Implicit(pxResult);
            DateTime dateTime6 = dateTime5;
            nullable4 = arAdjust.AdjgDocDate;
            DateTime dateTime7 = nullable4.Value;
            DateTime dateTime8;
            if (!(dateTime6 > dateTime7))
            {
              dateTime8 = dateTime5;
            }
            else
            {
              nullable4 = arAdjust.AdjgDocDate;
              dateTime8 = nullable4.Value;
            }
            DateTime dateTime9 = dateTime4;
            int days = (dateTime8 - dateTime9).Days;
            Decimal num8 = num1;
            Decimal? adjAmt = arAdjust.AdjAmt;
            Decimal? adjDiscAmt = arAdjust.AdjDiscAmt;
            Decimal? nullable6 = adjAmt.HasValue & adjDiscAmt.HasValue ? new Decimal?(adjAmt.GetValueOrDefault() + adjDiscAmt.GetValueOrDefault()) : new Decimal?();
            Decimal? adjWoAmt = arAdjust.AdjWOAmt;
            Decimal? nullable7 = nullable6.HasValue & adjWoAmt.HasValue ? new Decimal?(nullable6.GetValueOrDefault() + adjWoAmt.GetValueOrDefault()) : new Decimal?();
            Decimal? nullable8;
            if (!arAdjust.ReverseGainLoss.GetValueOrDefault())
            {
              nullable8 = arAdjust.RGOLAmt;
            }
            else
            {
              Decimal? rgolAmt = arAdjust.RGOLAmt;
              if (!rgolAmt.HasValue)
              {
                nullable6 = new Decimal?();
                nullable8 = nullable6;
              }
              else
                nullable8 = new Decimal?(-rgolAmt.GetValueOrDefault());
            }
            Decimal? nullable9 = nullable8;
            Decimal? nullable10 = nullable7.HasValue & nullable9.HasValue ? new Decimal?(nullable7.GetValueOrDefault() + nullable9.GetValueOrDefault()) : new Decimal?();
            Decimal num9 = num2;
            Decimal? nullable11 = nullable10.HasValue ? new Decimal?(nullable10.GetValueOrDefault() / num9) : new Decimal?();
            Decimal num10 = (Decimal) days;
            nullable2 = nullable11.HasValue ? new Decimal?(nullable11.GetValueOrDefault() * num10) : new Decimal?();
            Decimal? finChargePercent3 = finChargePercentList[index].FinChargePercent;
            Decimal? nullable12;
            if (!(nullable2.HasValue & finChargePercent3.HasValue))
            {
              nullable11 = new Decimal?();
              nullable12 = nullable11;
            }
            else
              nullable12 = new Decimal?(nullable2.GetValueOrDefault() * finChargePercent3.GetValueOrDefault());
            nullable3 = nullable12;
            Decimal num11 = (Decimal) 36500;
            Decimal num12 = (nullable3.HasValue ? new Decimal?(nullable3.GetValueOrDefault() / num11) : new Decimal?()).Value;
            num1 = num8 + num12;
          }
        }
      }
    }
    Decimal num13 = num1;
    Decimal? minFinChargeAmount = aDef.MinFinChargeAmount;
    Decimal num14 = num2;
    Decimal? nullable13;
    if (!minFinChargeAmount.HasValue)
    {
      nullable2 = new Decimal?();
      nullable13 = nullable2;
    }
    else
      nullable13 = new Decimal?(minFinChargeAmount.GetValueOrDefault() * num14);
    nullable3 = nullable13;
    Decimal valueOrDefault = nullable3.GetValueOrDefault();
    if (num13 < valueOrDefault & nullable3.HasValue)
    {
      nullable1 = aDef.MinFinChargeFlag;
      if (nullable1.GetValueOrDefault() && (calculationDate - dateTime1).Days > 0)
      {
        nullable3 = aDef.MinFinChargeAmount;
        num1 = nullable3.Value;
      }
      else if (aDocBalance != 0M)
        num1 = 0M;
    }
    aDest.FinChargeAmt = new Decimal?(num1);
  }

  protected virtual void ARFinChargesDetails_RefNbr_FieldSelecting(
    PXCache sender,
    PXFieldSelectingEventArgs e)
  {
    ARFinChargesApplyMaint.ARFinChargesDetails row = (ARFinChargesApplyMaint.ARFinChargesDetails) e.Row;
    if (row == null)
      return;
    Dictionary<string, ARFinChargesApplyMaint.MessageWithLevel> customInfo = PXLongOperation.GetCustomInfo(((PXGraph) this).UID) as Dictionary<string, ARFinChargesApplyMaint.MessageWithLevel>;
    TimeSpan timeSpan;
    Exception exception;
    PXLongRunStatus status = PXLongOperation.GetStatus(((PXGraph) this).UID, ref timeSpan, ref exception);
    if (status != 3 && status != 2 || customInfo == null)
      return;
    ARFinChargesApplyMaint.MessageWithLevel messageWithLevel = (ARFinChargesApplyMaint.MessageWithLevel) null;
    if (customInfo.ContainsKey(row.RefNbr + row.DocType))
      messageWithLevel = customInfo[row.RefNbr + row.DocType];
    string name = typeof (GLTranDoc.refNbr).Name;
    if (messageWithLevel == null)
      return;
    e.ReturnState = (object) PXFieldState.CreateInstance(e.ReturnState, typeof (string), new bool?(false), new bool?(), new int?(), new int?(), new int?(), (object) null, name, (string) null, (string) null, messageWithLevel.Message, messageWithLevel.Level, new bool?(), new bool?(), new bool?(), (PXUIVisibility) 0, (string) null, (string[]) null, (string[]) null);
    e.IsAltered = true;
  }

  protected virtual void ARFinChargesDetails_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is ARFinChargesApplyMaint.ARFinChargesDetails row))
      return;
    bool? nullable = row.HasUnreleasedCharges;
    if (nullable.GetValueOrDefault())
    {
      sender.RaiseExceptionHandling<ARFinChargesApplyMaint.ARFinChargesDetails.refNbr>((object) row, (object) row.RefNbr, (Exception) new PXSetPropertyException("At least one unreleased overdue charge document has been found for this document. Processing has been aborted.", (PXErrorLevel) 5));
    }
    else
    {
      nullable = row.CustomerHasOpenPayments;
      if (!nullable.GetValueOrDefault())
        return;
      sender.RaiseExceptionHandling<ARFinChargesApplyMaint.ARFinChargesDetails.refNbr>((object) row, (object) row.RefNbr, (Exception) new PXSetPropertyException("One or more unapplied or unreleased payments has been found for this Customer. Calculation of Overdue Charges can be affected by these documents. It is recommended to release and apply these documents prior to the processing.", (PXErrorLevel) 3));
    }
  }

  protected virtual void ARFinChargesApplyParameters_RowSelected(
    PXCache cache,
    PXRowSelectedEventArgs e)
  {
    ARFinChargesApplyMaint.ARFinChargesApplyParameters row = (ARFinChargesApplyMaint.ARFinChargesApplyParameters) e.Row;
    bool valueOrDefault = ((PXSelectBase<PX.Objects.AR.ARSetup>) this.ARSetup).Current.DefFinChargeFromCycle.GetValueOrDefault();
    PXUIFieldAttribute.SetRequired<ARFinChargesApplyMaint.ARFinChargesApplyParameters.finPeriodID>(cache, true);
    PXUIFieldAttribute.SetRequired<ARFinChargesApplyMaint.ARFinChargesApplyParameters.statementCycle>(cache, valueOrDefault);
    PXDefaultAttribute.SetPersistingCheck<ARFinChargesApplyMaint.ARFinChargesApplyParameters.statementCycle>(cache, (object) row, valueOrDefault ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
    PXUIFieldAttribute.SetVisible<ARFinChargesApplyMaint.ARFinChargesApplyParameters.customerClassID>(cache, e.Row, !valueOrDefault);
    PXUIFieldAttribute.SetVisible<ARFinChargesApplyMaint.ARFinChargesApplyParameters.customerID>(cache, e.Row, !valueOrDefault);
    bool flag = ((PXSelectBase<ARFinChargesApplyMaint.ARFinChargesDetails>) this.ARFinChargeRecords).Any<ARFinChargesApplyMaint.ARFinChargesDetails>();
    ((PXProcessing<ARFinChargesApplyMaint.ARFinChargesDetails>) this.ARFinChargeRecords).SetProcessAllEnabled(flag);
    ((PXProcessing<ARFinChargesApplyMaint.ARFinChargesDetails>) this.ARFinChargeRecords).SetProcessEnabled(flag);
  }

  protected virtual void ARFinChargesApplyParameters_StatementCycle_FieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
    ARFinChargesApplyMaint.ARFinChargesApplyParameters row = (ARFinChargesApplyMaint.ARFinChargesApplyParameters) e.Row;
    if (!((PXSelectBase<PX.Objects.AR.ARSetup>) this.ARSetup).Current.DefFinChargeFromCycle.GetValueOrDefault())
      return;
    ARStatementCycle aCycle = PXResultset<ARStatementCycle>.op_Implicit(PXSelectBase<ARStatementCycle, PXSelect<ARStatementCycle>.Config>.Search<ARStatementCycle.statementCycleId>((PXGraph) this, (object) row.StatementCycle, Array.Empty<object>()));
    if (aCycle == null)
      return;
    row.FinChargeDate = new DateTime?(ARStatementProcess.FindNextStatementDate((PXGraph) this, ((PXGraph) this).Accessinfo.BusinessDate.Value, aCycle));
    cache.SetDefaultExt<ARFinChargesApplyMaint.ARFinChargesApplyParameters.finPeriodID>(e.Row);
  }

  protected virtual void ARFinChargesApplyParameters_FinChargeDate_FieldUpdated(
    PXCache cache,
    PXFieldUpdatedEventArgs e)
  {
    cache.SetDefaultExt<ARFinChargesApplyMaint.ARFinChargesApplyParameters.finPeriodID>(e.Row);
  }

  [PXMergeAttributes]
  [LastFinchargeDate]
  public virtual void ARInvoice_LastFinChargeDate_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [LastPaymentDate]
  public virtual void ARInvoice_LastPaymentDate_CacheAttached(PXCache sender)
  {
  }

  private static int Compare(
    ARFinChargesApplyMaint.ARFinChargesDetails aT0,
    ARFinChargesApplyMaint.ARFinChargesDetails aT1)
  {
    int? customerId1 = aT0.CustomerID;
    int? customerId2 = aT1.CustomerID;
    if (!(customerId1.GetValueOrDefault() == customerId2.GetValueOrDefault() & customerId1.HasValue == customerId2.HasValue))
      return aT0.CustomerID.Value.CompareTo(aT1.CustomerID.Value);
    int? arAccountId1 = aT0.ARAccountID;
    int? arAccountId2 = aT1.ARAccountID;
    if (!(arAccountId1.GetValueOrDefault() == arAccountId2.GetValueOrDefault() & arAccountId1.HasValue == arAccountId2.HasValue))
      return aT0.ARAccountID.Value.CompareTo(aT1.ARAccountID.Value);
    int? arSubId1 = aT0.ARSubID;
    int? arSubId2 = aT1.ARSubID;
    return !(arSubId1.GetValueOrDefault() == arSubId2.GetValueOrDefault() & arSubId1.HasValue == arSubId2.HasValue) ? aT0.ARSubID.Value.CompareTo(aT1.ARSubID.Value) : string.Compare(aT0.FinChargeCuryID, aT1.FinChargeCuryID);
  }

  protected virtual void Copy(ARFinChargesApplyMaint.ARFinChargesDetails aDest, ARInvoice aSrc)
  {
    aDest.CustomerID = aSrc.CustomerID;
    aDest.DocType = aSrc.DocType;
    aDest.RefNbr = aSrc.RefNbr;
    aDest.DocDate = aSrc.DocDate;
    aDest.CuryID = aSrc.CuryID;
    aDest.CuryOrigDocAmt = aSrc.CuryOrigDocAmt;
    aDest.CuryDocBal = aSrc.CuryDocBal;
    aDest.BranchID = aSrc.BranchID;
  }

  protected virtual bool CheckRequiredFieldsFilled(PXCache aCache, IBqlTable aFilter)
  {
    bool flag = true;
    foreach (System.Type bqlField in aCache.BqlFields)
    {
      if (aCache.GetStateExt((object) aFilter, bqlField.Name) is PXFieldState stateExt)
      {
        object obj = aCache.GetValue((object) aFilter, bqlField.Name);
        if (stateExt.Required.GetValueOrDefault() && obj == null)
        {
          aCache.RaiseExceptionHandling(bqlField.Name, (object) aFilter, (object) null, (Exception) new PXSetPropertyException("This field is required!"));
          flag = false;
        }
      }
    }
    return flag;
  }

  protected class MessageWithLevel
  {
    public string Message;
    public PXErrorLevel Level;

    public MessageWithLevel(string message, PXErrorLevel level)
    {
      this.Message = message;
      this.Level = level;
    }
  }

  [Serializable]
  public class ARFinChargesApplyParameters : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXDBString(10, IsUnicode = true)]
    [PXDefault(typeof (ARStatementCycle.statementCycleId))]
    [PXUIField]
    [PXSelector(typeof (ARStatementCycle.statementCycleId), DescriptionField = typeof (ARStatementCycle.descr))]
    public virtual string StatementCycle { get; set; }

    [PXDBString(10, IsUnicode = true)]
    [PXDefault]
    [PXSelector(typeof (CustomerClass.customerClassID), DescriptionField = typeof (CustomerClass.descr))]
    [PXUIField(DisplayName = "Customer Class")]
    public virtual string CustomerClassID { get; set; }

    [PXDBInt]
    [PXUIField]
    [CustomerActive(DescriptionField = typeof (Customer.acctName))]
    [PXRestrictor(typeof (Where<Customer.status, NotEqual<CustomerStatus.inactive>, And<Customer.status, NotEqual<CustomerStatus.hold>>>), "The customer status is '{0}'.", new System.Type[] {typeof (Customer.status)}, ReplaceInherited = true)]
    public virtual int? CustomerID { get; set; }

    [PXDate]
    [PXDefault(typeof (AccessInfo.businessDate))]
    [PXUIField]
    public virtual DateTime? FinChargeDate { get; set; }

    [FinPeriodSelector(typeof (ARFinChargesApplyMaint.ARFinChargesApplyParameters.finChargeDate))]
    [PXUIField]
    public virtual string FinPeriodID { get; set; }

    public abstract class statementCycle : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARFinChargesApplyMaint.ARFinChargesApplyParameters.statementCycle>
    {
    }

    public abstract class customerClassID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARFinChargesApplyMaint.ARFinChargesApplyParameters.customerClassID>
    {
    }

    public abstract class customerID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ARFinChargesApplyMaint.ARFinChargesApplyParameters.customerID>
    {
    }

    public abstract class finChargeDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      ARFinChargesApplyMaint.ARFinChargesApplyParameters.finChargeDate>
    {
    }

    public abstract class finPeriodID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARFinChargesApplyMaint.ARFinChargesApplyParameters.finPeriodID>
    {
    }
  }

  [Serializable]
  public class ARFinChargesDetails : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected bool? _Selected = new bool?(false);
    public string TermsID;
    protected int? _BranchID;

    [PXBool]
    [PXDefault(true)]
    [PXUIField(DisplayName = "Selected")]
    public bool? Selected
    {
      get => this._Selected;
      set => this._Selected = value;
    }

    [PXDBString(3, IsKey = true, IsFixed = true)]
    [PXDefault]
    [ARInvoiceType.List]
    [PXUIField(DisplayName = "Type", Enabled = false)]
    public virtual string DocType { get; set; }

    [PXDBString(15, IsUnicode = true, IsKey = true)]
    [PXDefault]
    [PXUIField(DisplayName = "Reference Nbr.", Enabled = false)]
    public virtual string RefNbr { get; set; }

    [PXDate]
    [PXUIField]
    public virtual DateTime? DocDate { get; set; }

    [PXDate]
    [PXUIField]
    public virtual DateTime? DueDate { get; set; }

    [PXDate]
    [PXUIField]
    public virtual DateTime? LastPaymentDate { get; set; }

    [PXDate]
    [PXUIField]
    public virtual DateTime? LastChargeDate { get; set; }

    [CustomerActive]
    [PXRestrictor(typeof (Where<Customer.status, NotEqual<CustomerStatus.inactive>, And<Customer.status, NotEqual<CustomerStatus.hold>>>), "The customer status is '{0}'.", new System.Type[] {typeof (Customer.status)}, ReplaceInherited = true)]
    [PXDefault]
    public virtual int? CustomerID { get; set; }

    [PXString(5, IsUnicode = true, InputMask = ">LLLLL")]
    [PXUIField]
    [PXSelector(typeof (PX.Objects.CM.Currency.curyID))]
    public virtual string CuryID { get; set; }

    [PXCury(typeof (ARFinChargesApplyMaint.ARFinChargesDetails.curyID))]
    [PXUIField]
    public virtual Decimal? CuryOrigDocAmt { get; set; }

    [PXCury(typeof (ARFinChargesApplyMaint.ARFinChargesDetails.curyID))]
    [PXUIField]
    public virtual Decimal? CuryDocBal { get; set; }

    [PXShort]
    [PXDefault(0)]
    [PXUIField(DisplayName = "Overdue Days", Enabled = false)]
    public virtual short? OverdueDays { get; set; }

    [PXString(10, IsUnicode = true)]
    [PXDefault]
    [PXUIField]
    public virtual string FinChargeID { get; set; }

    [PXString(5, IsUnicode = true, InputMask = ">LLLLL")]
    [PXUIField]
    [PXDefault(typeof (Current<AccessInfo.baseCuryID>))]
    [PXSelector(typeof (PX.Objects.CM.Currency.curyID))]
    public virtual string FinChargeCuryID { get; set; }

    [PXDBCury(typeof (ARFinChargesApplyMaint.ARFinChargesDetails.curyID))]
    [PXUIField]
    public virtual Decimal? FinChargeAmt { get; set; }

    [PXDefault]
    [Account]
    public virtual int? ARAccountID { get; set; }

    [PXDefault]
    [SubAccount]
    public virtual int? ARSubID { get; set; }

    [PXDBDecimal(6)]
    [PXDefault(TypeCode.Decimal, "1.0")]
    public virtual Decimal? CuryRate { get; set; }

    [PXDBString(1, IsFixed = true)]
    [PXDefault("M")]
    public virtual string CuryMultDiv { get; set; }

    [PXDBString(6, IsUnicode = true)]
    public virtual string CuryRateTypeID { get; set; }

    [PXDBDecimal(6)]
    [PXUIField(DisplayName = "Currency Rate")]
    public virtual Decimal? SampleCuryRate
    {
      [PXDependsOnFields(new System.Type[] {typeof (ARFinChargesApplyMaint.ARFinChargesDetails.curyMultDiv), typeof (ARFinChargesApplyMaint.ARFinChargesDetails.curyRate)})] get
      {
        if (this.CuryMultDiv == "M")
          return this.CuryRate;
        Decimal num = (Decimal) 1;
        Decimal? curyRate = this.CuryRate;
        return !curyRate.HasValue ? new Decimal?() : new Decimal?(num / curyRate.GetValueOrDefault());
      }
      set
      {
      }
    }

    [PXDBBool]
    [PXDefault(false)]
    public virtual bool? HasUnreleasedCharges { get; set; }

    [PXDBString(3, IsFixed = true)]
    [PXDefault]
    public virtual string LastFCDocType { get; set; }

    [PXDBString(15, IsUnicode = true)]
    [PXDefault]
    [PXUIField(DisplayName = "Last Fin. Charge", Enabled = false)]
    public virtual string LastFCRefNbr { get; set; }

    [PXDate]
    [PXUIField]
    public virtual DateTime? FinChargeDate { get; set; }

    [FinPeriodID(null, null, null, null, null, null, true, false, null, null, null, true, true)]
    [PXUIField]
    public virtual string FinPeriodID { get; set; }

    [PXBool]
    [PXDefault(false)]
    public virtual bool? CustomerHasOpenPayments { get; set; }

    /// <summary>
    /// Identifier of the <see cref="T:PX.Objects.GL.Branch" />, to which the batch belongs.
    /// </summary>
    /// <value>
    /// Corresponds to the <see cref="P:PX.Objects.GL.Branch.BranchID" /> field.
    /// </value>
    [Branch(null, null, true, true, true)]
    public virtual int? BranchID
    {
      get => this._BranchID;
      set => this._BranchID = value;
    }

    public abstract class selected : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      ARFinChargesApplyMaint.ARFinChargesDetails.selected>
    {
    }

    public abstract class docType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARFinChargesApplyMaint.ARFinChargesDetails.docType>
    {
    }

    public abstract class refNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARFinChargesApplyMaint.ARFinChargesDetails.refNbr>
    {
    }

    public abstract class docDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      ARFinChargesApplyMaint.ARFinChargesDetails.docDate>
    {
    }

    public abstract class dueDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      ARFinChargesApplyMaint.ARFinChargesDetails.dueDate>
    {
    }

    public abstract class lastPaymentDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      ARFinChargesApplyMaint.ARFinChargesDetails.lastPaymentDate>
    {
    }

    public abstract class lastChargeDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      ARFinChargesApplyMaint.ARFinChargesDetails.lastChargeDate>
    {
    }

    public abstract class customerID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ARFinChargesApplyMaint.ARFinChargesDetails.customerID>
    {
    }

    public abstract class curyID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARFinChargesApplyMaint.ARFinChargesDetails.curyID>
    {
    }

    public abstract class curyOrigDocAmt : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARFinChargesApplyMaint.ARFinChargesDetails.curyOrigDocAmt>
    {
    }

    public abstract class curyDocBal : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARFinChargesApplyMaint.ARFinChargesDetails.curyDocBal>
    {
    }

    public abstract class overdueDays : 
      BqlType<
      #nullable enable
      IBqlShort, short>.Field<
      #nullable disable
      ARFinChargesApplyMaint.ARFinChargesDetails.overdueDays>
    {
    }

    public abstract class finChargeID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARFinChargesApplyMaint.ARFinChargesDetails.finChargeID>
    {
    }

    public abstract class finChargeCuryID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARFinChargesApplyMaint.ARFinChargesDetails.finChargeCuryID>
    {
    }

    public abstract class finChargeAmt : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARFinChargesApplyMaint.ARFinChargesDetails.finChargeAmt>
    {
    }

    public abstract class aRAccountID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ARFinChargesApplyMaint.ARFinChargesDetails.aRAccountID>
    {
    }

    public abstract class aRSubID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ARFinChargesApplyMaint.ARFinChargesDetails.aRSubID>
    {
    }

    public abstract class curyRate : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARFinChargesApplyMaint.ARFinChargesDetails.curyRate>
    {
    }

    public abstract class curyMultDiv : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARFinChargesApplyMaint.ARFinChargesDetails.curyMultDiv>
    {
    }

    public abstract class curyRateTypeID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARFinChargesApplyMaint.ARFinChargesDetails.curyRateTypeID>
    {
    }

    public abstract class sampleCuryRate : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARFinChargesApplyMaint.ARFinChargesDetails.sampleCuryRate>
    {
    }

    public abstract class hasUnreleasedCharges : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      ARFinChargesApplyMaint.ARFinChargesDetails.hasUnreleasedCharges>
    {
    }

    public abstract class lastFCDocType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARFinChargesApplyMaint.ARFinChargesDetails.lastFCDocType>
    {
    }

    public abstract class lastFCRefNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARFinChargesApplyMaint.ARFinChargesDetails.lastFCRefNbr>
    {
    }

    public abstract class finChargeDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      ARFinChargesApplyMaint.ARFinChargesDetails.finChargeDate>
    {
    }

    public abstract class finPeriodID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARFinChargesApplyMaint.ARFinChargesDetails.finPeriodID>
    {
    }

    public abstract class customerHasOpenPayments : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      ARFinChargesApplyMaint.ARFinChargesDetails.customerHasOpenPayments>
    {
    }

    public abstract class branchID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ARFinChargesApplyMaint.ARFinChargesDetails.branchID>
    {
    }
  }

  protected class ARDocKey(string aFirst, string aSecond) : Pair<string, string>(aFirst, aSecond)
  {
  }
}
