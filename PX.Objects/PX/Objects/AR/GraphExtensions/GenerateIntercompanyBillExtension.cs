// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.GraphExtensions.GenerateIntercompanyBillExtension
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.WorkflowAPI;
using PX.Objects.AP;
using PX.Objects.Common;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.PM;
using System;
using System.Collections;
using System.Linq;
using System.Linq.Expressions;

#nullable enable
namespace PX.Objects.AR.GraphExtensions;

public class GenerateIntercompanyBillExtension : PXGraphExtension<
#nullable disable
ARInvoiceEntry>
{
  public PXFilter<GenerateIntercompanyBillExtension.GenerateBillParameters> generateBillParameters;
  public PXAction<PX.Objects.AR.ARInvoice> generateOrViewIntercompanyBill;

  [InjectDependency]
  private ICurrentUserInformationProvider _currentUserInformationProvider { get; set; }

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.interBranch>();

  [PXWorkflowDependsOnType(new Type[] {typeof (ARSetupApproval)})]
  public virtual void Configure(PXScreenConfiguration config)
  {
    GenerateIntercompanyBillExtension.Configure(config.GetScreenConfigurationContext<ARInvoiceEntry, PX.Objects.AR.ARInvoice>());
  }

  protected static void Configure(WorkflowContext<ARInvoiceEntry, PX.Objects.AR.ARInvoice> context)
  {
    BoundedTo<ARInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionCategory.IConfigured intercompanyCategory = context.Categories.Get("IntercompanyID");
    context.UpdateScreenConfigurationFor((Func<BoundedTo<ARInvoiceEntry, PX.Objects.AR.ARInvoice>.ScreenConfiguration.ConfiguratorScreen, BoundedTo<ARInvoiceEntry, PX.Objects.AR.ARInvoice>.ScreenConfiguration.ConfiguratorScreen>) (screen => screen.WithActions((Action<BoundedTo<ARInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionDefinition.ContainerAdjusterActions>) (actions => actions.Add<GenerateIntercompanyBillExtension>((Expression<Func<GenerateIntercompanyBillExtension, PXAction<PX.Objects.AR.ARInvoice>>>) (g => g.generateOrViewIntercompanyBill), (Func<BoundedTo<ARInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionDefinition.IAllowOptionalConfigAction, BoundedTo<ARInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionDefinition.IConfigured>) (c => (BoundedTo<ARInvoiceEntry, PX.Objects.AR.ARInvoice>.ActionDefinition.IConfigured) c.InFolder(intercompanyCategory, (Expression<Func<ARInvoiceEntry, PXAction<PX.Objects.AR.ARInvoice>>>) (g => g.writeOff)).IsDisabledWhen(context.Conditions.Get("IsPrepaymentInvoiceReversing")).IsHiddenWhen(context.Conditions.Get("IsMigrationMode")).IsHiddenWhen(context.Conditions.Get("IsPrepaymentInvoice")).PlaceAfter((Expression<Func<ARInvoiceEntry, PXAction<PX.Objects.AR.ARInvoice>>>) (g => g.writeOff))))))));
  }

  public virtual void Initialize() => ((PXGraphExtension) this).Initialize();

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = false)]
  [ARMigrationModeDependentActionRestriction(true, true, true)]
  public virtual IEnumerable GenerateOrViewIntercompanyBill(PXAdapter adapter)
  {
    if (!((PXAction) this.generateOrViewIntercompanyBill).GetEnabled())
      return adapter.Get();
    APSetup apSetup = PXResultset<APSetup>.op_Implicit(PXSelectBase<APSetup, PXViewOf<APSetup>.BasedOn<SelectFromBase<APSetup, TypeArrayOf<IFbqlJoin>.Empty>>.Config>.Select((PXGraph) this.Base, Array.Empty<object>()));
    if ((apSetup != null ? (apSetup.MigrationMode.GetValueOrDefault() ? 1 : 0) : 0) != 0)
      throw new PXException("Migration mode is activated in the Accounts Payable module.");
    ((PXAction) this.Base.Save).Press();
    GenerateIntercompanyBillExtension.GenerateBillParameters current1 = ((PXSelectBase<GenerateIntercompanyBillExtension.GenerateBillParameters>) this.generateBillParameters).Current;
    GenerateIntercompanyBillExtension.GenerateBillParameters generateBillParameters = current1;
    bool? massProcess = ((PXSelectBase<GenerateIntercompanyBillExtension.GenerateBillParameters>) this.generateBillParameters).Current.MassProcess;
    bool? nullable = new bool?(massProcess.GetValueOrDefault() || adapter.MassProcess);
    generateBillParameters.MassProcess = nullable;
    PX.Objects.AR.ARInvoice current2 = ((PXSelectBase<PX.Objects.AR.ARInvoice>) this.Base.CurrentDocument).Current;
    PX.Objects.AP.APInvoice apInvoice = PXResultset<PX.Objects.AP.APInvoice>.op_Implicit(PXSelectBase<PX.Objects.AP.APInvoice, PXSelect<PX.Objects.AP.APInvoice, Where<PX.Objects.AP.APInvoice.intercompanyInvoiceNoteID, Equal<Required<PX.Objects.AP.APInvoice.intercompanyInvoiceNoteID>>>>.Config>.SelectSingleBound((PXGraph) this.Base, (object[]) null, new object[1]
    {
      (object) current2.NoteID
    }));
    APInvoiceEntry instance = PXGraph.CreateInstance<APInvoiceEntry>();
    if (apInvoice != null)
    {
      ((PXSelectBase<PX.Objects.AP.APInvoice>) instance.Document).Current = apInvoice;
    }
    else
    {
      massProcess = current1.MassProcess;
      if (!massProcess.GetValueOrDefault() && !((PXGraph) this.Base).IsContractBasedAPI)
      {
        int? projectId = current2.ProjectID;
        int num = 0;
        if (projectId.GetValueOrDefault() > num & projectId.HasValue && ((PXSelectBase) this.generateBillParameters).View.Ask("Do you want to copy the project information to the AP document?", (MessageButtons) 4) == 6)
          current1.CopyProjectInformation = new bool?(true);
      }
      this.GenerateIntercompanyBill(instance, current2);
    }
    massProcess = current1.MassProcess;
    if (massProcess.GetValueOrDefault())
      ((PXAction) instance.Save).Press();
    else
      PXRedirectHelper.TryRedirect((PXGraph) instance, (PXRedirectHelper.WindowMode) 0);
    return adapter.Get();
  }

  public virtual PX.Objects.AP.APInvoice GenerateIntercompanyBill(
    APInvoiceEntry apInvoiceEntryGraph,
    PX.Objects.AR.ARInvoice arInvoice,
    GenerateIntercompanyBillExtension.GenerateBillParameters parameters = null)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    GenerateIntercompanyBillExtension.\u003C\u003Ec__DisplayClass11_0 cDisplayClass110 = new GenerateIntercompanyBillExtension.\u003C\u003Ec__DisplayClass11_0();
    if (parameters == null)
      parameters = ((PXSelectBase<GenerateIntercompanyBillExtension.GenerateBillParameters>) this.generateBillParameters).Current;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass110.branch = PXAccess.GetBranchByBAccountID(arInvoice.CustomerID);
    // ISSUE: reference to a compiler-generated method
    if (!this._currentUserInformationProvider.GetActiveBranches().Any<BranchInfo>(new Func<BranchInfo, bool>(cDisplayClass110.\u003CGenerateIntercompanyBill\u003Eb__0)))
    {
      // ISSUE: reference to a compiler-generated field
      throw new PXException("{0} '{1}' cannot be found in the system. Please verify whether you have proper access rights to this object.", new object[2]
      {
        (object) PXUIFieldAttribute.GetDisplayName<PX.Objects.AR.ARInvoice.branchID>((PXCache) GraphHelper.Caches<PX.Objects.AR.ARInvoice>((PXGraph) this.Base)),
        (object) cDisplayClass110.branch.BranchCD.Trim()
      });
    }
    ((PXGraph) apInvoiceEntryGraph).Clear();
    bool? requireControlTotal = ((PXSelectBase<APSetup>) apInvoiceEntryGraph.APSetup).Current.RequireControlTotal;
    ((PXSelectBase<APSetup>) apInvoiceEntryGraph.APSetup).Current.RequireControlTotal = new bool?(false);
    PX.Objects.AP.APInvoice apInvoice1 = new PX.Objects.AP.APInvoice();
    apInvoice1.DocDate = arInvoice.DocDate;
    // ISSUE: reference to a compiler-generated field
    apInvoice1.BranchID = new int?(cDisplayClass110.branch.BranchID);
    PX.Objects.AP.APInvoice row = apInvoice1;
    PXCache cache1 = ((PXSelectBase) apInvoiceEntryGraph.Document).Cache;
    bool? nullable = parameters.MassProcess;
    if (nullable.GetValueOrDefault() && parameters != null && parameters.FinPeriodID != null)
      cache1.SetValue<PX.Objects.AP.APInvoice.finPeriodID>((object) row, (object) parameters?.FinPeriodID);
    else
      FinPeriodIDAttribute.SetPeriodsByMaster<PX.Objects.AP.APInvoice.finPeriodID>(((PXSelectBase) apInvoiceEntryGraph.CurrentDocument).Cache, (object) row, arInvoice.TranPeriodID);
    switch (arInvoice.DocType)
    {
      case "INV":
        row.DocType = "INV";
        break;
      case "DRM":
        row.DocType = "ACR";
        break;
      case "CRM":
        row.DocType = "ADR";
        break;
      default:
        throw new NotImplementedException();
    }
    PX.Objects.AP.Vendor vendor = PXResultset<PX.Objects.AP.Vendor>.op_Implicit(PXSelectBase<PX.Objects.AP.Vendor, PXSelectJoin<PX.Objects.AP.Vendor, InnerJoin<PX.Objects.GL.Branch, On<PX.Objects.AP.Vendor.bAccountID, Equal<PX.Objects.GL.Branch.bAccountID>>>, Where<PX.Objects.GL.Branch.branchID, Equal<Required<PX.Objects.GL.Branch.branchID>>>>.Config>.SelectSingleBound((PXGraph) this.Base, (object[]) null, new object[1]
    {
      (object) arInvoice.BranchID
    }));
    if (vendor != null)
      row.VendorID = vendor.BAccountID;
    if (!row.VendorID.HasValue)
      throw new PXException("The {0} branch has not been extended to a vendor.", new object[1]
      {
        (object) PXResultset<PX.Objects.GL.Branch>.op_Implicit(PXSelectBase<PX.Objects.GL.Branch, PXViewOf<PX.Objects.GL.Branch>.BasedOn<SelectFromBase<PX.Objects.GL.Branch, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<PX.Objects.GL.Branch.branchID, IBqlInt>.IsEqual<P.AsInt>>>.Config>.Select((PXGraph) this.Base, new object[1]
        {
          (object) arInvoice.BranchID
        })).BranchCD.Trim()
      });
    PX.Objects.AP.APInvoice apInvoice2 = ((PXSelectBase<PX.Objects.AP.APInvoice>) apInvoiceEntryGraph.Document).Insert(row);
    nullable = parameters.MassProcess;
    if (nullable.GetValueOrDefault())
    {
      PXCache pxCache = cache1;
      PX.Objects.AP.APInvoice apInvoice3 = apInvoice2;
      int num;
      if (parameters == null)
      {
        num = 0;
      }
      else
      {
        nullable = parameters.CreateOnHold;
        num = nullable.GetValueOrDefault() ? 1 : 0;
      }
      // ISSUE: variable of a boxed type
      __Boxed<bool> local = (ValueType) (bool) num;
      pxCache.SetValueExt<PX.Objects.AP.APInvoice.hold>((object) apInvoice3, (object) local);
    }
    else
    {
      PXCache pxCache = cache1;
      PX.Objects.AP.APInvoice apInvoice4 = apInvoice2;
      nullable = ((PXSelectBase<APSetup>) apInvoiceEntryGraph.APSetup).Current.HoldEntry;
      // ISSUE: variable of a boxed type
      __Boxed<bool> valueOrDefault = (ValueType) nullable.GetValueOrDefault();
      pxCache.SetValueExt<PX.Objects.AP.APInvoice.hold>((object) apInvoice4, (object) valueOrDefault);
    }
    cache1.Update((object) apInvoice2);
    cache1.SetValueExt<PX.Objects.AP.APInvoice.invoiceNbr>((object) apInvoice2, (object) arInvoice.RefNbr);
    PX.Objects.CM.Extensions.CurrencyInfo currencyInfo = ((PXGraph) this.Base).GetExtension<ARInvoiceEntry.MultiCurrency>().GetCurrencyInfo(arInvoice.CuryInfoID);
    ((PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo>) apInvoiceEntryGraph.currencyinfo).Current = PXResultset<PX.Objects.CM.Extensions.CurrencyInfo>.op_Implicit(((PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo>) apInvoiceEntryGraph.currencyinfo).Select(Array.Empty<object>()));
    bool flag = true;
    if (PXAccess.FeatureInstalled<FeaturesSet.multipleBaseCurrencies>())
    {
      PX.Objects.AR.Customer customer = PXResultset<PX.Objects.AR.Customer>.op_Implicit(PXSelectBase<PX.Objects.AR.Customer, PXSelect<PX.Objects.AR.Customer, Where<PX.Objects.AR.Customer.bAccountID, Equal<Required<PX.Objects.AR.Customer.bAccountID>>>>.Config>.SelectSingleBound((PXGraph) this.Base, (object[]) null, new object[1]
      {
        (object) arInvoice.CustomerID
      }));
      string baseCuryId1 = customer.BaseCuryID;
      if (baseCuryId1 == null)
      {
        nullable = customer.IsBranch;
        baseCuryId1 = nullable.GetValueOrDefault() ? PXAccess.GetBranchByBAccountID(customer.BAccountID).BaseCuryID : (string) null;
      }
      string baseCuryId2 = vendor.BaseCuryID;
      if (baseCuryId2 == null)
      {
        nullable = vendor.IsBranch;
        baseCuryId2 = nullable.GetValueOrDefault() ? PXAccess.GetBranchByBAccountID(vendor.BAccountID).BaseCuryID : (string) null;
      }
      flag = baseCuryId1 == baseCuryId2;
    }
    if (flag)
    {
      PX.Objects.CM.Extensions.CurrencyInfo copy = PXCache<PX.Objects.CM.Extensions.CurrencyInfo>.CreateCopy(currencyInfo);
      copy.CuryInfoID = ((PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo>) apInvoiceEntryGraph.currencyinfo).Current.CuryInfoID;
      ((PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo>) apInvoiceEntryGraph.currencyinfo).Update(copy);
    }
    cache1.SetValueExt<PX.Objects.AP.APInvoice.curyID>((object) apInvoice2, (object) arInvoice.CuryID);
    cache1.SetValueExt<PX.Objects.AP.APInvoice.curyInfoID>((object) apInvoice2, (object) ((PXSelectBase<PX.Objects.CM.Extensions.CurrencyInfo>) apInvoiceEntryGraph.currencyinfo).Current.CuryInfoID);
    cache1.SetValue<PX.Objects.AP.APInvoice.termsID>((object) apInvoice2, (object) arInvoice.TermsID);
    PX.Objects.AP.APInvoice apInvoice5 = (PX.Objects.AP.APInvoice) cache1.Update((object) apInvoice2);
    cache1.SetValueExt<PX.Objects.AP.APInvoice.dueDate>((object) apInvoice5, (object) arInvoice.DueDate);
    cache1.SetValueExt<PX.Objects.AP.APInvoice.discDate>((object) apInvoice5, (object) arInvoice.DiscDate);
    cache1.SetValueExt<PX.Objects.AP.APInvoice.paymentsByLinesAllowed>((object) apInvoice5, (object) ((PXSelectBase<PX.Objects.AP.Vendor>) apInvoiceEntryGraph.vendor).Current.PaymentsByLinesAllowed);
    cache1.SetValueExt<PX.Objects.AP.APInvoice.docDesc>((object) apInvoice5, (object) arInvoice.DocDesc);
    cache1.SetValueExt<PX.Objects.AP.APInvoice.disableAutomaticDiscountCalculation>((object) apInvoice5, (object) true);
    PXNoteAttribute.CopyNoteAndFiles((PXCache) GraphHelper.Caches<PX.Objects.AR.ARInvoice>((PXGraph) this.Base), (object) arInvoice, cache1, (object) apInvoice5, (PXNoteAttribute.IPXCopySettings) null);
    cache1.SetValueExt<PX.Objects.AP.APInvoice.taxCalcMode>((object) apInvoice5, (object) arInvoice.TaxCalcMode);
    PMProject pmProject = PXResultset<PMProject>.op_Implicit(PXSelectBase<PMProject, PXSelect<PMProject, Where<PMProject.contractID, Equal<Required<PMProject.contractID>>>>.Config>.SelectSingleBound((PXGraph) this.Base, (object[]) null, new object[1]
    {
      (object) arInvoice.ProjectID
    }));
    int? billProjectId = this.GetBillProjectID(parameters, arInvoice);
    apInvoice5.ProjectID = billProjectId;
    PXResultset<PMSetup>.op_Implicit(PXSelectBase<PMSetup, PXSelect<PMSetup>.Config>.Select((PXGraph) this.Base, Array.Empty<object>()));
    PXCache cache2 = ((PXSelectBase) this.Base.Transactions).Cache;
    PXCache cache3 = ((PXSelectBase) apInvoiceEntryGraph.Transactions).Cache;
    foreach (PXResult<ARTran> pxResult in ((PXSelectBase<ARTran>) this.Base.Transactions).Select(Array.Empty<object>()))
    {
      ARTran arTran = PXResult<ARTran>.op_Implicit(pxResult);
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      GenerateIntercompanyBillExtension.\u003C\u003Ec__DisplayClass11_1 cDisplayClass111 = new GenerateIntercompanyBillExtension.\u003C\u003Ec__DisplayClass11_1();
      // ISSUE: reference to a compiler-generated field
      cDisplayClass111.projectID = this.GetAPTranProjectID(parameters, arInvoice, arTran);
      // ISSUE: reference to a compiler-generated field
      cDisplayClass111.taskID = this.GetAPTranTaskID(parameters, arInvoice, arTran);
      // ISSUE: reference to a compiler-generated field
      cDisplayClass111.costCodeID = this.GetAPTranCostCodeID(parameters, arInvoice, arTran);
      try
      {
        // ISSUE: method pointer
        ((PXGraph) apInvoiceEntryGraph).FieldDefaulting.AddHandler<APTran.projectID>(new PXFieldDefaulting((object) cDisplayClass111, __methodptr(\u003CGenerateIntercompanyBill\u003Eg__APTranProjectIDFieldDefaulting\u007C1)));
        // ISSUE: method pointer
        ((PXGraph) apInvoiceEntryGraph).FieldDefaulting.AddHandler<APTran.taskID>(new PXFieldDefaulting((object) cDisplayClass111, __methodptr(\u003CGenerateIntercompanyBill\u003Eg__APTranTaskIDFieldDefaulting\u007C2)));
        // ISSUE: method pointer
        ((PXGraph) apInvoiceEntryGraph).FieldDefaulting.AddHandler<APTran.costCodeID>(new PXFieldDefaulting((object) cDisplayClass111, __methodptr(\u003CGenerateIntercompanyBill\u003Eg__APTranCostCodeIDFieldDefaulting\u007C3)));
        APTran intercompanyApTran = this.GenerateIntercompanyAPTran(apInvoiceEntryGraph, arTran);
        if (parameters != null)
        {
          nullable = parameters.CopyProjectInformation;
          if (nullable.GetValueOrDefault())
          {
            nullable = pmProject.VisibleInAP;
            if (!nullable.GetValueOrDefault())
            {
              PXUIFieldAttribute.SetWarning<APTran.projectID>(cache3, (object) intercompanyApTran, PXMessages.LocalizeFormatNoPrefix("The {0} project cannot be copied because the AP subledger is not added in its visibility settings.", new object[1]
              {
                (object) pmProject.ContractCD
              }));
              PXUIFieldAttribute.SetWarning<ARTran.projectID>(cache2, (object) arTran, PXMessages.LocalizeFormatNoPrefix("The {0} project cannot be copied because the AP subledger is not added in its visibility settings.", new object[1]
              {
                (object) pmProject.ContractCD
              }));
            }
          }
        }
      }
      finally
      {
        // ISSUE: method pointer
        ((PXGraph) apInvoiceEntryGraph).FieldDefaulting.RemoveHandler<APTran.projectID>(new PXFieldDefaulting((object) cDisplayClass111, __methodptr(\u003CGenerateIntercompanyBill\u003Eg__APTranProjectIDFieldDefaulting\u007C1)));
        // ISSUE: method pointer
        ((PXGraph) apInvoiceEntryGraph).FieldDefaulting.RemoveHandler<APTran.taskID>(new PXFieldDefaulting((object) cDisplayClass111, __methodptr(\u003CGenerateIntercompanyBill\u003Eg__APTranTaskIDFieldDefaulting\u007C2)));
        // ISSUE: method pointer
        ((PXGraph) apInvoiceEntryGraph).FieldDefaulting.RemoveHandler<APTran.costCodeID>(new PXFieldDefaulting((object) cDisplayClass111, __methodptr(\u003CGenerateIntercompanyBill\u003Eg__APTranCostCodeIDFieldDefaulting\u007C3)));
      }
    }
    PXCache cache4 = ((PXSelectBase) apInvoiceEntryGraph.DiscountDetails).Cache;
    if (PXAccess.FeatureInstalled<FeaturesSet.customerDiscounts>())
    {
      foreach (PXResult<ARInvoiceDiscountDetail> pxResult in ((PXSelectBase<ARInvoiceDiscountDetail>) this.Base.ARDiscountDetails).Select(Array.Empty<object>()))
      {
        ARInvoiceDiscountDetail invoiceDiscountDetail1 = PXResult<ARInvoiceDiscountDetail>.op_Implicit(pxResult);
        APInvoiceDiscountDetail invoiceDiscountDetail2 = ((PXSelectBase<APInvoiceDiscountDetail>) apInvoiceEntryGraph.DiscountDetails).Insert(new APInvoiceDiscountDetail());
        cache4.SetValueExt<ARInvoiceDiscountDetail.isManual>((object) invoiceDiscountDetail2, (object) true);
        cache4.SetValueExt<ARInvoiceDiscountDetail.curyDiscountAmt>((object) invoiceDiscountDetail2, (object) invoiceDiscountDetail1.CuryDiscountAmt);
        cache4.SetValueExt<ARInvoiceDiscountDetail.description>((object) invoiceDiscountDetail2, (object) invoiceDiscountDetail1.Description);
        ((PXSelectBase<APInvoiceDiscountDetail>) apInvoiceEntryGraph.DiscountDetails).Update(invoiceDiscountDetail2);
      }
    }
    else
    {
      APInvoiceDiscountDetail invoiceDiscountDetail = ((PXSelectBase<APInvoiceDiscountDetail>) apInvoiceEntryGraph.DiscountDetails).Insert(new APInvoiceDiscountDetail());
      cache4.SetValueExt<ARInvoiceDiscountDetail.isManual>((object) invoiceDiscountDetail, (object) true);
      cache4.SetValueExt<ARInvoiceDiscountDetail.curyDiscountAmt>((object) invoiceDiscountDetail, (object) arInvoice.CuryDiscTot);
      ((PXSelectBase<APInvoiceDiscountDetail>) apInvoiceEntryGraph.DiscountDetails).Update(invoiceDiscountDetail);
    }
    cache1.SetValue<PX.Objects.AP.APInvoice.intercompanyInvoiceNoteID>((object) apInvoice5, (object) arInvoice.NoteID);
    Decimal? curyOrigDocAmt1 = arInvoice.CuryOrigDocAmt;
    Decimal? curyOrigDocAmt2 = apInvoice5.CuryOrigDocAmt;
    if (curyOrigDocAmt1.GetValueOrDefault() == curyOrigDocAmt2.GetValueOrDefault() & curyOrigDocAmt1.HasValue == curyOrigDocAmt2.HasValue)
      cache1.SetValue<PX.Objects.AP.APInvoice.curyOrigDiscAmt>((object) apInvoice5, (object) arInvoice.CuryOrigDiscAmt);
    ((PXSelectBase<APSetup>) apInvoiceEntryGraph.APSetup).Current.RequireControlTotal = requireControlTotal;
    PX.Objects.AP.APInvoice intercompanyBill = ((PXSelectBase<PX.Objects.AP.APInvoice>) apInvoiceEntryGraph.Document).Update(apInvoice5);
    apInvoiceEntryGraph.AttachPrepayment();
    return intercompanyBill;
  }

  public virtual APTran GenerateIntercompanyAPTran(
    APInvoiceEntry apInvoiceEntryGraph,
    ARTran arTran)
  {
    APTran apTran = ((PXSelectBase<APTran>) apInvoiceEntryGraph.Transactions).Insert(new APTran()
    {
      ManualPrice = arTran.ManualPrice
    });
    this.SetAPTranFields(((PXSelectBase) apInvoiceEntryGraph.Transactions).Cache, apTran, arTran);
    return ((PXSelectBase<APTran>) apInvoiceEntryGraph.Transactions).Update(apTran);
  }

  public virtual void SetAPTranFields(PXCache cacheAPTran, APTran apTran, ARTran arTran)
  {
    cacheAPTran.SetValueExt<APTran.inventoryID>((object) apTran, (object) arTran.InventoryID);
    cacheAPTran.SetValueExt<APTran.tranDesc>((object) apTran, (object) arTran.TranDesc);
    cacheAPTran.SetValueExt<APTran.qty>((object) apTran, (object) arTran.Qty);
    apTran.UOM = arTran.UOM;
    cacheAPTran.SetValueExt<APTran.curyUnitCost>((object) apTran, (object) arTran.CuryUnitPrice);
    cacheAPTran.SetValueExt<APTran.curyLineAmt>((object) apTran, (object) arTran.CuryExtPrice);
    cacheAPTran.SetValueExt<APTran.curyDiscAmt>((object) apTran, (object) arTran.CuryDiscAmt);
    cacheAPTran.SetValueExt<APTran.discPct>((object) apTran, (object) arTran.DiscPct);
    cacheAPTran.SetValueExt<APTran.manualDisc>((object) apTran, (object) true);
    cacheAPTran.SetValueExt<APTran.taxCategoryID>((object) apTran, (object) arTran.TaxCategoryID);
  }

  protected virtual int? GetBillProjectID(
    GenerateIntercompanyBillExtension.GenerateBillParameters parameters,
    PX.Objects.AR.ARInvoice arInvoice)
  {
    int num = ProjectDefaultAttribute.NonProject().Value;
    return parameters == null || !parameters.CopyProjectInformation.GetValueOrDefault() ? new int?(num) : arInvoice.ProjectID;
  }

  protected virtual int? GetAPTranProjectID(
    GenerateIntercompanyBillExtension.GenerateBillParameters parameters,
    PX.Objects.AR.ARInvoice arInvoice,
    ARTran arTran)
  {
    return this.GetBillProjectID(parameters, arInvoice);
  }

  protected virtual int? GetAPTranTaskID(
    GenerateIntercompanyBillExtension.GenerateBillParameters parameters,
    PX.Objects.AR.ARInvoice arInvoice,
    ARTran arTran)
  {
    return parameters == null || !parameters.CopyProjectInformation.GetValueOrDefault() ? new int?() : arTran.TaskID;
  }

  protected virtual int? GetAPTranCostCodeID(
    GenerateIntercompanyBillExtension.GenerateBillParameters parameters,
    PX.Objects.AR.ARInvoice arInvoice,
    ARTran arTran)
  {
    return parameters == null || !parameters.CopyProjectInformation.GetValueOrDefault() ? new int?() : arTran.CostCodeID;
  }

  public ProcessingResult CheckGeneratedAPDocument(PX.Objects.AR.ARInvoice arInvoice, PX.Objects.AP.APInvoice apInvoice)
  {
    ProcessingResult success = ProcessingResultBase<ProcessingResult, object, ProcessingResultMessage>.CreateSuccess((object) arInvoice);
    if (GraphHelper.RowCast<ARTran>((IEnumerable) ((PXSelectBase<ARTran>) this.Base.Transactions).Select(Array.Empty<object>())).Any<ARTran>((Func<ARTran, bool>) (arTran => !string.IsNullOrEmpty(arTran.DeferredCode))))
      success.AddMessage((PXErrorLevel) 3, "The deferral codes from the AR document cannot be copied.");
    Decimal? curyOrigDocAmt = apInvoice.CuryOrigDocAmt;
    Decimal? nullable = arInvoice.CuryOrigDocAmt;
    if (!(curyOrigDocAmt.GetValueOrDefault() == nullable.GetValueOrDefault() & curyOrigDocAmt.HasValue == nullable.HasValue))
      success.AddMessage((PXErrorLevel) 3, "The document amount differs from the document amount in the related AR document.");
    nullable = apInvoice.CuryTaxTotal;
    Decimal? curyTaxTotal = arInvoice.CuryTaxTotal;
    if (!(nullable.GetValueOrDefault() == curyTaxTotal.GetValueOrDefault() & nullable.HasValue == curyTaxTotal.HasValue))
      success.AddMessage((PXErrorLevel) 3, "The document's tax total differs from the tax total in the related AR document.");
    return success;
  }

  public virtual void ARInvoice_RowSelected(PXCache cache, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is PX.Objects.AR.ARInvoice row))
      return;
    ARInvoiceState arInvoiceState1 = new ARInvoiceState();
    bool? nullable = row.Released;
    arInvoiceState1.IsDocumentReleased = nullable.GetValueOrDefault();
    arInvoiceState1.IsDocumentInvoice = row.DocType == "INV";
    arInvoiceState1.IsDocumentPrepaymentInvoice = row.DocType == "PPI";
    arInvoiceState1.IsDocumentCreditMemo = row.DocType == "CRM";
    arInvoiceState1.IsDocumentDebitMemo = row.DocType == "DRM";
    nullable = row.IsMigratedRecord;
    arInvoiceState1.IsMigratedDocument = nullable.GetValueOrDefault();
    nullable = row.RetainageApply;
    arInvoiceState1.RetainageApply = nullable.GetValueOrDefault();
    nullable = row.IsRetainageDocument;
    arInvoiceState1.IsRetainageDocument = nullable.GetValueOrDefault();
    ARInvoiceState arInvoiceState2 = arInvoiceState1;
    PX.Objects.AR.Customer current = ((PXSelectBase<PX.Objects.AR.Customer>) this.Base.customer).Current;
    int num1;
    if (current == null)
    {
      num1 = 0;
    }
    else
    {
      nullable = current.IsBranch;
      num1 = nullable.GetValueOrDefault() ? 1 : 0;
    }
    int num2;
    if (num1 != 0 && (arInvoiceState2.IsDocumentInvoice || arInvoiceState2.IsDocumentCreditMemo || arInvoiceState2.IsDocumentDebitMemo) && !arInvoiceState2.IsMigratedDocument && !arInvoiceState2.IsRetainageDocument && !arInvoiceState2.RetainageApply && row.MasterRefNbr == null && !row.InstallmentNbr.HasValue)
    {
      if (arInvoiceState2.IsDocumentCreditMemo)
      {
        nullable = row.PendingPPD;
        num2 = !nullable.GetValueOrDefault() ? 1 : 0;
      }
      else
        num2 = 1;
    }
    else
      num2 = 0;
    bool flag1 = num2 != 0;
    int num3;
    if (flag1 && arInvoiceState2.IsDocumentReleased)
    {
      nullable = row.IsHiddenInIntercompanySales;
      num3 = !nullable.GetValueOrDefault() ? 1 : 0;
    }
    else
      num3 = 0;
    bool flag2 = num3 != 0;
    PXResultset<PX.Objects.AP.APInvoice> pxResultset;
    if (flag1)
      pxResultset = (PXResultset<PX.Objects.AP.APInvoice>) null;
    else
      pxResultset = PXSelectBase<PX.Objects.AP.APInvoice, PXViewOf<PX.Objects.AP.APInvoice>.BasedOn<SelectFromBase<PX.Objects.AP.APInvoice, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<PX.Objects.AP.APInvoice.intercompanyInvoiceNoteID, IBqlGuid>.IsEqual<P.AsGuid>>>.Config>.SelectSingleBound((PXGraph) this.Base, (object[]) null, new object[1]
      {
        (object) row.NoteID
      });
    PX.Objects.AP.APInvoice apInvoice = PXResultset<PX.Objects.AP.APInvoice>.op_Implicit(pxResultset);
    ((PXAction) this.generateOrViewIntercompanyBill).SetCaption(apInvoice != null ? "View AP Document" : "Generate AP Document");
    ((PXAction) this.generateOrViewIntercompanyBill).SetEnabled(flag2 || apInvoice != null);
    PXUIFieldAttribute.SetVisible<PX.Objects.AR.ARInvoice.isHiddenInIntercompanySales>(cache, (object) row, flag1);
  }

  /// <summary>
  /// The user can specify the following parameter to manage the process of generation:
  /// * If the Create AP Documents in Specific Period checkbox is activated, then new AP documents will be created with the specified Financial Period.
  /// * If the Create AP Documents on Hold checkbox is checked, new AP documents are created in On Hold status; otherwise - in Balance or Pending Approval status depending on approval configuration in AP.
  /// * If the Copy Project Information To AP Document checkbox is activated, then the project data (project code, cost code, task id) will be copied from the AR document to the AP document.
  /// </summary>
  [PXHidden]
  [Serializable]
  public class GenerateBillParameters : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXString(6)]
    public virtual string FinPeriodID { get; set; }

    [PXBool]
    [PXUnboundDefault(true)]
    public virtual bool? CreateOnHold { get; set; }

    [PXBool]
    [PXUnboundDefault(false)]
    public virtual bool? CopyProjectInformation { get; set; }

    [PXBool]
    [PXUnboundDefault(false)]
    public virtual bool? MassProcess { get; set; }

    /// <summary>
    /// If the Create AP Documents in Specific Period checkbox is activated, then new AP documents will be created with the specified Financial Period.
    /// </summary>
    public abstract class finPeriodID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      GenerateIntercompanyBillExtension.GenerateBillParameters.finPeriodID>
    {
    }

    /// <summary>
    /// If the Create AP Documents on Hold checkbox is checked, new AP documents are created in On Hold status; otherwise - in Balance or Pending Approval status depending on approval configuration in AP.
    /// </summary>
    public abstract class createOnHold : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      GenerateIntercompanyBillExtension.GenerateBillParameters.createOnHold>
    {
    }

    /// <summary>
    /// If the Copy Project Information To AP Document checkbox is activated, then the project data (project code, cost code, task id) will be copied from the AR document to the AP document.
    /// </summary>
    public abstract class copyProjectInformationto : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      GenerateIntercompanyBillExtension.GenerateBillParameters.copyProjectInformationto>
    {
    }

    /// <summary>
    /// If the Copy Project Information To AP Document checkbox is activated, then the project data (project code, cost code, task id) will be copied from the AR document to the AP document.
    /// </summary>
    public abstract class massProcess : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      GenerateIntercompanyBillExtension.GenerateBillParameters.massProcess>
    {
    }
  }
}
