// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOShipmentEntry
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.CarrierService;
using PX.Common;
using PX.CS.Contracts.Interfaces;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.DependencyInjection;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Data.WorkflowAPI;
using PX.LicensePolicy;
using PX.Objects.AR;
using PX.Objects.AR.MigrationMode;
using PX.Objects.CM;
using PX.Objects.Common;
using PX.Objects.Common.Extensions;
using PX.Objects.Common.Scopes;
using PX.Objects.CR;
using PX.Objects.CR.Extensions;
using PX.Objects.CS;
using PX.Objects.EP;
using PX.Objects.GL.FinPeriods;
using PX.Objects.IN;
using PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated;
using PX.Objects.PO;
using PX.Objects.SO.GraphExtensions;
using PX.Objects.SO.GraphExtensions.CarrierRates;
using PX.Objects.SO.GraphExtensions.SOOrderEntryExt;
using PX.Objects.SO.GraphExtensions.SOShipmentEntryExt;
using PX.Objects.SO.Models;
using PX.Objects.SO.Services;
using PX.Objects.SO.WMS;
using PX.SM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;

#nullable enable
namespace PX.Objects.SO;

public class SOShipmentEntry : PXGraph<
#nullable disable
SOShipmentEntry, SOShipment>, IGraphWithInitialization
{
  public ToggleCurrency<SOShipment> CurrencyView;
  [PXViewName("Shipment")]
  public PXSelectJoin<SOShipment, LeftJoinSingleTable<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<SOShipment.customerID>>, LeftJoin<PX.Objects.IN.INSite, On<SOShipment.FK.Site>>>, Where2<Where<PX.Objects.AR.Customer.bAccountID, IsNull, Or<Match<PX.Objects.AR.Customer, Current<AccessInfo.userName>>>>, And<Where<SOShipment.siteID, IsNull, Or<PX.Objects.IN.INSite.branchID, IsNotNull, And<Match<PX.Objects.IN.INSite, Current<AccessInfo.userName>>>>>>>> Document;
  public PXSelect<SOShipment, Where<SOShipment.shipmentNbr, Equal<Current<SOShipment.shipmentNbr>>>> CurrentDocument;
  public PXSelect<SOShipLine, Where<SOShipLine.shipmentNbr, Equal<Current<SOShipment.shipmentNbr>>>, OrderBy<Asc<SOShipLine.shipmentNbr, Asc<SOShipLine.sortOrder>>>> Transactions;
  public PXSelect<SOShipLineSplit, Where<SOShipLineSplit.shipmentNbr, Equal<Current<SOShipLine.shipmentNbr>>, And<SOShipLineSplit.lineNbr, Equal<Current<SOShipLine.lineNbr>>>>> splits;
  public PXSelect<PX.Objects.SO.Unassigned.SOShipLineSplit, Where<PX.Objects.SO.Unassigned.SOShipLineSplit.shipmentNbr, Equal<Current<SOShipLine.shipmentNbr>>, And<PX.Objects.SO.Unassigned.SOShipLineSplit.lineNbr, Equal<Current<SOShipLine.lineNbr>>>>> unassignedSplits;
  [PXViewName("Shipping Address")]
  public PXSelect<SOShipmentAddress, Where<SOShipmentAddress.addressID, Equal<Current<SOShipment.shipAddressID>>>> Shipping_Address;
  [PXViewName("Shipping Contact")]
  public PXSelect<SOShipmentContact, Where<SOShipmentContact.contactID, Equal<Current<SOShipment.shipContactID>>>> Shipping_Contact;
  public PXSelect<SOSetupApproval> sosetupapproval;
  public PXSelect<SOShipLine, Where<SOShipLine.shipmentNbr, Equal<Current<SOShipment.shipmentNbr>>, And<SOShipLine.isFree, Equal<boolTrue>>>, OrderBy<Asc<SOShipLine.shipmentNbr, Asc<SOShipLine.lineNbr>>>> FreeItems;
  [PXViewName("SO Package Detail")]
  public PXSelect<SOPackageDetailEx, Where<SOPackageDetailEx.shipmentNbr, Equal<Current<SOShipment.shipmentNbr>>>> Packages;
  [PXHidden]
  public PXSelect<SOPackageDetailEx, Where<SOPackageDetailEx.shipmentNbr, Equal<Current<SOShipment.shipmentNbr>>>> PackagesForRates;
  [PXHidden]
  [PXCopyPasteHiddenView]
  public PXSelect<CarrierLabelHistory> LabelHistory;
  public PXSetup<PX.Objects.CS.Carrier, Where<PX.Objects.CS.Carrier.carrierID, Equal<Current<SOShipment.shipVia>>>> carrier;
  public PXSelect<PX.Objects.CM.CurrencyInfo, Where<PX.Objects.CM.CurrencyInfo.curyInfoID, Equal<Current<SOShipment.curyInfoID>>>> currencyinfo;
  public PXSelect<PX.Objects.CM.CurrencyInfo> DummyCuryInfo;
  public PXSetup<INSetup> insetup;
  public PXSetup<SOSetup> sosetup;
  public PXSetup<ARSetup> arsetup;
  public PXSetupOptional<CommonSetup> commonsetup;
  public PXSetup<PX.Objects.GL.Branch, InnerJoin<PX.Objects.IN.INSite, On<PX.Objects.IN.INSite.branchID, Equal<PX.Objects.GL.Branch.branchID>>>, Where<PX.Objects.IN.INSite.siteID, Equal<Optional<SOShipment.destinationSiteID>>, And<Current<SOShipment.shipmentType>, Equal<INDocType.transfer>, Or<PX.Objects.IN.INSite.siteID, Equal<Optional<SOShipment.siteID>>, And<Current<SOShipment.shipmentType>, NotEqual<INDocType.transfer>>>>>> Company;
  public PXSetup<PX.Objects.AR.Customer, Where<PX.Objects.AR.Customer.bAccountID, Equal<Optional<SOShipment.customerID>>>> customer;
  public PXSetup<PX.Objects.CR.Location, Where<PX.Objects.CR.Location.bAccountID, Equal<Current<SOShipment.customerID>>, And<PX.Objects.CR.Location.locationID, Equal<Optional<SOShipment.customerLocationID>>>>> location;
  [PXViewName("Main Contact")]
  public PXSelect<PX.Objects.CR.Contact> DefaultCompanyContact;
  public PXAction<SOShipment> putOnHold;
  public PXAction<SOShipment> releaseFromHold;
  public PXInitializeState<SOShipment> initializeState;
  public PXAction<SOShipment> notification;
  public PXAction<SOShipment> emailShipment;
  public PXAction<SOShipment> applyAssignmentRules;
  public PXAction<SOShipment> correctShipmentAction;
  public PXAction<SOShipment> printPickListAction;
  public PXAction<SOShipment> inquiry;
  public PXAction<SOShipment> report;
  public PXAction<SOShipment> printShipmentConfirmation;
  public PXAction<SOShipment> calculateFreight;
  public PXAction<SOShipment> inventorySummary;
  public PXWorkflowEventHandler<SOShipment> OnShipmentCorrected;
  public PXWorkflowEventHandler<SOShipment, SOInvoice> OnInvoiceReleased;
  public PXWorkflowEventHandler<SOShipment, SOInvoice> OnInvoiceCancelled;
  public PXAction<SOShipment> validateAddresses;
  private const string UpsCarrierPlugin = "PX.UpsRestCarrier.UpsRestCarrier";
  private const int MaxNameLengthForUps = 35;

  public SOShipmentLineSplittingExtension LineSplittingExt
  {
    get => ((PXGraph) this).FindImplementation<SOShipmentLineSplittingExtension>();
  }

  public SOShipmentItemAvailabilityExtension ItemAvailabilityExt
  {
    get => ((PXGraph) this).FindImplementation<SOShipmentItemAvailabilityExtension>();
  }

  protected virtual IEnumerable defaultCompanyContact()
  {
    return (IEnumerable) OrganizationMaint.GetDefaultContactForCurrentOrganization((PXGraph) this);
  }

  [PXButton(CommitChanges = true)]
  [PXUIField(DisplayName = "Hold")]
  protected virtual IEnumerable PutOnHold(PXAdapter adapter) => adapter.Get();

  [PXButton(CommitChanges = true)]
  [PXUIField(DisplayName = "Remove Hold")]
  protected virtual IEnumerable ReleaseFromHold(PXAdapter adapter) => adapter.Get();

  [PXUIField(DisplayName = "Notifications", Visible = false)]
  [PXButton(ImageKey = "DataEntryF")]
  protected virtual IEnumerable Notification(PXAdapter adapter, [PXString] string notificationCD)
  {
    List<SOShipment> list = adapter.Get<SOShipment>().ToList<SOShipment>();
    PXProcessing<SOShipment>.ProcessRecords((IEnumerable<SOShipment>) list, adapter.MassProcess, (Action<SOShipment>) (shipment =>
    {
      ((PXSelectBase<SOShipment>) this.Document).Current = shipment;
      Dictionary<string, string> parameters = new Dictionary<string, string>();
      parameters["SOShipment.ShipmentNbr"] = shipment.ShipmentNbr;
      PX.Objects.GL.Branch branch = PXResultset<PX.Objects.GL.Branch>.op_Implicit(PXSelectBase<PX.Objects.GL.Branch, PXSelectReadonly2<PX.Objects.GL.Branch, InnerJoin<PX.Objects.IN.INSite, On<PX.Objects.IN.INSite.branchID, Equal<PX.Objects.GL.Branch.branchID>>>, Where<PX.Objects.IN.INSite.siteID, Equal<Optional<SOShipment.destinationSiteID>>, And<Current<SOShipment.shipmentType>, Equal<INDocType.transfer>, Or<PX.Objects.IN.INSite.siteID, Equal<Optional<SOShipment.siteID>>, And<Current<SOShipment.shipmentType>, NotEqual<INDocType.transfer>>>>>>.Config>.SelectSingleBound((PXGraph) this, new object[1]
      {
        (object) shipment
      }, Array.Empty<object>()));
      ((PXGraph) this).GetExtension<SOShipmentEntry_ActivityDetailsExt>().SendNotification("Customer", notificationCD, branch == null || !branch.BranchID.HasValue ? ((PXGraph) this).Accessinfo.BranchID : branch.BranchID, (IDictionary<string, string>) parameters, adapter.MassProcess, (IList<Guid?>) null);
    }), (Action<SOShipment>) null, (Func<SOShipment, Exception, bool, bool?>) null, (Action<SOShipment>) null, (Action<SOShipment>) null);
    return (IEnumerable) list;
  }

  [PXButton(CommitChanges = true)]
  [PXUIField]
  public virtual IEnumerable EmailShipment(PXAdapter adapter, [PXString] string notificationCD = null)
  {
    return this.Notification(adapter, notificationCD ?? "SHIPMENT");
  }

  [PXButton(CommitChanges = true)]
  [PXUIField(DisplayName = "Apply Assignment Rules", Visible = false)]
  protected virtual IEnumerable ApplyAssignmentRules(PXAdapter adapter)
  {
    if (!((PXSelectBase<SOSetup>) this.sosetup).Current.DefaultShipmentAssignmentMapID.HasValue)
      throw new PXSetPropertyException("Default Sales Order Assignment Map is not entered in Sales Orders Preferences", new object[1]
      {
        (object) "Sales Orders Preferences"
      });
    List<SOShipment> list = adapter.Get<SOShipment>().ToList<SOShipment>();
    PXGraph.CreateInstance<EPAssignmentProcessor<SOShipment>>().Assign(((PXSelectBase<SOShipment>) this.Document).Current, ((PXSelectBase<SOSetup>) this.sosetup).Current.DefaultShipmentAssignmentMapID);
    ((PXSelectBase<SOShipment>) this.Document).Update(((PXSelectBase<SOShipment>) this.Document).Current);
    return (IEnumerable) list;
  }

  [PXButton(CommitChanges = true)]
  [PXUIField]
  protected virtual IEnumerable CorrectShipmentAction(PXAdapter adapter)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    SOShipmentEntry.\u003C\u003Ec__DisplayClass42_0 cDisplayClass420 = new SOShipmentEntry.\u003C\u003Ec__DisplayClass42_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass420.list = adapter.Get<SOShipment>().ToList<SOShipment>();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass420.massProcess = adapter.MassProcess;
    ((PXAction) this.Save).Press();
    // ISSUE: method pointer
    PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) cDisplayClass420, __methodptr(\u003CCorrectShipmentAction\u003Eb__0)));
    // ISSUE: reference to a compiler-generated field
    return (IEnumerable) cDisplayClass420.list;
  }

  [PXButton(CommitChanges = true)]
  [PXUIField]
  protected virtual IEnumerable PrintPickListAction(PXAdapter adapter)
  {
    if (!adapter.MassProcess && ((PXGraph) this).IsDirty)
      ((PXAction) this.Save).Press();
    List<SOShipment> list = adapter.Get<SOShipment>().ToList<SOShipment>();
    ((PXGraph) this).LongOperationManager.StartAsyncOperation((Func<CancellationToken, System.Threading.Tasks.Task>) (ct => SOShipmentEntry.PrintPickListOperation(list, adapter, ct)));
    return (IEnumerable) list;
  }

  [PXUIField]
  [PXButton]
  protected virtual IEnumerable Inquiry(PXAdapter adapter, [PXInt, PXIntList(new int[] {}, new string[] {})] int? inquiryID, [PXString] string ActionName)
  {
    if (!string.IsNullOrEmpty(ActionName))
    {
      PXAction action = ((PXGraph) this).Actions[ActionName];
      if (action != null)
      {
        ((PXAction) this.Save).Press();
        foreach (object obj in action.Press(adapter))
          ;
      }
    }
    return adapter.Get();
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable Report(PXAdapter adapter, [PXString(8, InputMask = "CC.CC.CC.CC")] string reportID)
  {
    ImmutableList<SOShipment> immutableList = ImmutableList.ToImmutableList<SOShipment>(adapter.Get<SOShipment>());
    if (!string.IsNullOrEmpty(reportID) && ((IEnumerable<SOShipment>) immutableList).Any<SOShipment>())
    {
      ((PXAction) this.Save).Press();
      PXReportRequiredException combinedReport = ((IEnumerable<SOShipment>) immutableList).Select<SOShipment, (string, Dictionary<string, string>)>((Func<SOShipment, (string, Dictionary<string, string>)>) (sh => (GetActualReportID(sh), new Dictionary<string, string>()
      {
        ["SOShipment.ShipmentNbr"] = sh.ShipmentNbr
      }))).Aggregate<(string, Dictionary<string, string>), PXReportRequiredException>((PXReportRequiredException) null, (Func<PXReportRequiredException, (string, Dictionary<string, string>), PXReportRequiredException>) ((acc, elem) =>
      {
        CurrentLocalization currentLocalization = (CurrentLocalization) null;
        PX.Objects.IN.INSite inSite = PX.Objects.IN.INSite.PK.Find((PXGraph) this, ((PXSelectBase<SOShipment>) this.Document).Current.SiteID);
        if (inSite != null)
          currentLocalization = new CurrentLocalization(OrganizationLocalizationHelper.GetCurrentLocalizationCodeForBranch(inSite.BranchID));
        PXReportRequiredException requiredException = PXReportRequiredException.CombineReport(acc, elem.ActualReportID, elem.Parameters, currentLocalization);
        if (adapter.MassProcess)
          PXProcessing<SOShipment>.SetProcessed();
        ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 2;
        return requiredException;
      }));
      if (combinedReport != null)
      {
        if (PXAccess.FeatureInstalled<FeaturesSet.deviceHub>())
        {
          int num;
          ((PXGraph) this).LongOperationManager.StartAsyncOperation((Func<CancellationToken, System.Threading.Tasks.Task>) (async ct => num = await SMPrintJobMaint.CreatePrintJobGroup(adapter, new Func<string, string, int?, Guid?>(new NotificationUtility((PXGraph) this).SearchPrinter), "Customer", reportID, ((PXGraph) this).Accessinfo.BranchID, combinedReport, (string) null, ct) ? 1 : 0));
        }
        throw combinedReport;
      }
    }
    return (IEnumerable) immutableList;

    string GetActualReportID(SOShipment shipment)
    {
      if (adapter.MassProcess)
        PXProcessing<SOShipment>.SetCurrentItem((object) shipment);
      ((PXSelectBase<SOShipment>) this.Document).Current = shipment;
      PX.Objects.GL.Branch branch = (PX.Objects.GL.Branch) null;
      using (new PXReadBranchRestrictedScope())
        branch = PXResultset<PX.Objects.GL.Branch>.op_Implicit(((PXSelectBase<PX.Objects.GL.Branch>) this.Company).Select(Array.Empty<object>()));
      return new NotificationUtility((PXGraph) this).SearchCustomerReport(reportID, shipment.CustomerID, branch.BranchID);
    }
  }

  [PXButton(CommitChanges = true)]
  [PXUIField]
  public virtual IEnumerable PrintShipmentConfirmation(PXAdapter adapter)
  {
    return this.Report(adapter.Apply<PXAdapter>((Action<PXAdapter>) (it => it.Menu = "Print Shipment Confirmation")), "SO642000");
  }

  [PXUIField]
  [PXButton]
  public virtual IEnumerable CalculateFreight(PXAdapter adapter)
  {
    this.CalculateFreightCost(false);
    return adapter.Get();
  }

  public virtual void PrintConfirmation()
  {
    this.PrintShipmentConfirmation(this.CreateDummyAdapter());
  }

  [PXUIField]
  [PXLookupButton]
  public virtual IEnumerable InventorySummary(PXAdapter adapter)
  {
    PXCache cache = ((PXSelectBase) this.Transactions).Cache;
    SOShipLine current = ((PXSelectBase<SOShipLine>) this.Transactions).Current;
    if (current == null)
      return adapter.Get();
    PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this, current.InventoryID);
    if (inventoryItem != null && inventoryItem.StkItem.GetValueOrDefault())
    {
      INSubItem inSubItem = (INSubItem) PXSelectorAttribute.Select<SOShipLine.subItemID>(cache, (object) current);
      InventorySummaryEnq.Redirect(inventoryItem.InventoryID, inSubItem?.SubItemCD, current.SiteID, current.LocationID);
    }
    return adapter.Get();
  }

  public SOShipmentEntry()
  {
    if (PXAccess.FeatureInstalled<FeaturesSet.inventory>())
    {
      INSetup current1 = ((PXSelectBase<INSetup>) this.insetup).Current;
    }
    CommonSetup current2 = ((PXSelectBase<CommonSetup>) this.commonsetup).Current;
    SOSetup current3 = ((PXSelectBase<SOSetup>) this.sosetup).Current;
    ARSetupNoMigrationMode.EnsureMigrationModeDisabled((PXGraph) this);
    ((PXAction) this.CopyPaste).SetVisible(false);
    PXUIFieldAttribute.SetDisplayName<PX.Objects.CR.Contact.salutation>(((PXGraph) this).Caches[typeof (PX.Objects.CR.Contact)], "Attention");
    ((PXGraph) this).Views.Caches.Add(typeof (SOLineSplit));
    ((PXGraph) this).Views.Caches.Add(typeof (NoteDoc));
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    ((PXGraph) this).FieldDefaulting.AddHandler<BAccountR.type>(SOShipmentEntry.\u003C\u003Ec.\u003C\u003E9__56_0 ?? (SOShipmentEntry.\u003C\u003Ec.\u003C\u003E9__56_0 = new PXFieldDefaulting((object) SOShipmentEntry.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003C\u002Ector\u003Eb__56_0))));
    if (PXAccess.FeatureInstalled<FeaturesSet.carrierIntegration>())
      return;
    ((PXAction) this.CarrierRatesExt.shopRates).SetCaption(PXMessages.LocalizeNoPrefix(nameof (Packages)));
  }

  [InjectDependency]
  protected ILicenseLimitsService _licenseLimits { get; set; }

  void IGraphWithInitialization.Initialize()
  {
    if (this._licenseLimits == null)
      return;
    ((PXGraph) this).OnBeforeCommit += new Action<PXGraph>(this.CheckLicenseLimitsBeforeCommitHandler);
  }

  private void CheckLicenseLimitsBeforeCommitHandler(PXGraph e)
  {
    Action<PXGraph> checkerDelegate1 = this._licenseLimits.GetCheckerDelegate<SOShipment>(new TableQuery[1]
    {
      new TableQuery((TransactionTypes) 108, typeof (SOShipLine), (Func<PXGraph, PXDataFieldValue[]>) (graph => new PXDataFieldValue[1]
      {
        (PXDataFieldValue) new PXDataFieldValue<SOShipLine.shipmentNbr>((object) ((PXSelectBase<SOShipment>) ((SOShipmentEntry) graph).Document).Current?.ShipmentNbr)
      }))
    });
    try
    {
      checkerDelegate1(e);
    }
    catch (PXException ex)
    {
      throw new PXException("The number of the lines in this document has exceeded the limit set for the current license. Please reduce the number of lines to be able to save the document");
    }
    Action<PXGraph> checkerDelegate2 = this._licenseLimits.GetCheckerDelegate<SOShipment>(new TableQuery[1]
    {
      new TableQuery((TransactionTypes) 115, typeof (SOShipLineSplit), (Func<PXGraph, PXDataFieldValue[]>) (graph => new PXDataFieldValue[1]
      {
        (PXDataFieldValue) new PXDataFieldValue<SOShipLineSplit.shipmentNbr>((object) ((PXSelectBase<SOShipment>) ((SOShipmentEntry) graph).Document).Current?.ShipmentNbr)
      }))
    });
    try
    {
      checkerDelegate2(e);
    }
    catch (PXException ex)
    {
      throw new PXException("The number of the splits in this document has exceeded the limit set for the current license. Please reduce the number of splits to be able to save the document");
    }
    SOShipment current = ((PXSelectBase<SOShipment>) this.Document).Current;
    if ((current != null ? (!current.UnlimitedPackages.GetValueOrDefault() ? 1 : 0) : 1) == 0)
      return;
    Action<PXGraph> checkerDelegate3 = this._licenseLimits.GetCheckerDelegate<SOShipment>(new TableQuery[1]
    {
      new TableQuery((TransactionTypes) 108, typeof (SOPackageDetail), (Func<PXGraph, PXDataFieldValue[]>) (graph => new PXDataFieldValue[1]
      {
        (PXDataFieldValue) new PXDataFieldValue<SOPackageDetail.shipmentNbr>((object) ((PXSelectBase<SOShipment>) ((SOShipmentEntry) graph).Document).Current?.ShipmentNbr)
      }))
    });
    try
    {
      checkerDelegate3(e);
    }
    catch (PXException ex)
    {
      throw new PXException("The number of the packages in this document has exceeded the limit set for the current license. Please reduce the number of packages to be able to save the document");
    }
  }

  [PXMergeAttributes]
  [PXRestrictor(typeof (Where<Current<SOShipment.orderCntr>, Equal<int0>, And<Current<SOShipment.overrideFreightAmount>, Equal<False>, Or<PX.Objects.CS.ShipTerms.freightAmountSource, Equal<Current<SOShipment.freightAmountSource>>>>>), "Cannot select shipping terms with Invoice Freight Price Based On set to {0}.", new System.Type[] {typeof (PX.Objects.CS.ShipTerms.freightAmountSource)})]
  protected virtual void SOShipment_ShipTermsID_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXRemoveBaseAttribute(typeof (INUnitAttribute))]
  [SOShipLineUnit(DisplayName = "UOM")]
  protected virtual void SOShipLine_UOM_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXFormula(null, typeof (SumCalc<SOShipment.shipmentQty>))]
  protected virtual void SOShipLine_ShippedQty_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXFormula(typeof (Mult<SOShipLine.shippedQty, Mult<SOShipLine.unitPrice, Sub<decimal1, Div<SOShipLine.discPct, decimal100>>>>), typeof (SumCalc<SOShipment.lineTotal>))]
  protected virtual void SOShipLine_LineAmt_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXFormula(typeof (Mult<Row<SOShipLine.baseShippedQty>.WithDependencies<SOShipLine.shippedQty, SOShipLine.uOM>, SOShipLine.unitWeigth>), typeof (SumCalc<SOShipment.shipmentWeight>))]
  protected virtual void SOShipLine_ExtWeight_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXFormula(typeof (Mult<Row<SOShipLine.baseShippedQty>.WithDependencies<SOShipLine.shippedQty, SOShipLine.uOM>, SOShipLine.unitVolume>), typeof (SumCalc<SOShipment.shipmentVolume>))]
  protected virtual void SOShipLine_ExtVolume_CacheAttached(PXCache sender)
  {
  }

  [PXMergeAttributes]
  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "Visible", true)]
  public virtual void _(PX.Data.Events.CacheAttached<SOShipmentAddress.latitude> e)
  {
  }

  [PXMergeAttributes]
  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "Visible", true)]
  public virtual void _(
    PX.Data.Events.CacheAttached<SOShipmentAddress.longitude> e)
  {
  }

  protected virtual void _(PX.Data.Events.RowUpdated<SOShipmentAddress> e)
  {
    SOShipmentAddress row = e.Row;
    if (row == null || ((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<SOShipmentAddress>>) e).Cache.ObjectsEqual<SOShipmentAddress.countryID, SOShipmentAddress.postalCode, SOShipmentAddress.state>((object) row, (object) e.OldRow))
      return;
    this.ResetFreightCostIsValid(((PXSelectBase<SOShipment>) this.Document).Current);
  }

  [PXButton(CommitChanges = true)]
  [PXUIField]
  public virtual IEnumerable ValidateAddresses(PXAdapter adapter)
  {
    SOShipmentEntry soShipmentEntry = this;
    foreach (SOShipment soShipment in adapter.Get<SOShipment>())
    {
      if (soShipment != null)
        ((PXGraph) soShipmentEntry).FindAllImplementations<IAddressValidationHelper>().ValidateAddresses();
      yield return (object) soShipment;
    }
  }

  protected virtual void CurrencyInfo_CuryEffDate_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (((PXSelectBase<SOShipment>) this.Document).Current == null)
      return;
    e.NewValue = (object) ((PXSelectBase<SOShipment>) this.Document).Current.ShipDate;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void CurrencyInfo_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is PX.Objects.CM.CurrencyInfo row))
      return;
    bool flag = row.AllowUpdate(((PXSelectBase) this.Transactions).Cache);
    PXUIFieldAttribute.SetEnabled<PX.Objects.CM.CurrencyInfo.curyRateTypeID>(sender, (object) row, flag);
    PXUIFieldAttribute.SetEnabled<PX.Objects.CM.CurrencyInfo.curyEffDate>(sender, (object) row, flag);
    PXUIFieldAttribute.SetEnabled<PX.Objects.CM.CurrencyInfo.sampleCuryRate>(sender, (object) row, flag);
    PXUIFieldAttribute.SetEnabled<PX.Objects.CM.CurrencyInfo.sampleRecipRate>(sender, (object) row, flag);
  }

  protected virtual void SOShipment_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    SOShipment row = (SOShipment) e.Row;
    SOShipment oldRow = (SOShipment) e.OldRow;
    this.EnsureControlQty(row);
    if (!sender.ObjectsEqual<SOShipment.resedential, SOShipment.saturdayDelivery, SOShipment.groundCollect, SOShipment.insurance, SOShipment.useCustomerAccount, SOShipment.shipAddressID, SOShipment.shipVia>(e.Row, e.OldRow))
      this.ResetFreightCostIsValid(row);
    if (!this.IsFreightRecalculationNeeded(row, oldRow))
      return;
    this.RecalculateFreight(row, oldRow);
  }

  protected void ResetFreightCostIsValid(SOShipment row)
  {
    if (row == null)
      return;
    PX.Objects.CS.Carrier carrier = PX.Objects.CS.Carrier.PK.Find((PXGraph) this, row.ShipVia);
    if ((carrier != null ? (carrier.IsExternal.GetValueOrDefault() ? 1 : 0) : 0) != 0)
      row.FreightCostIsValid = new bool?(false);
    else
      row.FreightCostIsValid = new bool?(true);
  }

  protected virtual void EnsureControlQty(SOShipment shipment)
  {
    PXCache cache = ((PXSelectBase) this.Document).Cache;
    if (!((PXSelectBase<SOSetup>) this.sosetup).Current.RequireShipmentTotal.GetValueOrDefault())
    {
      cache.SetValue<SOShipment.controlQty>((object) shipment, (object) shipment.ShipmentQty.GetValueOrDefault());
    }
    else
    {
      bool? hold = shipment.Hold;
      bool flag1 = false;
      if (!(hold.GetValueOrDefault() == flag1 & hold.HasValue))
        return;
      bool? confirmed = shipment.Confirmed;
      bool flag2 = false;
      if (!(confirmed.GetValueOrDefault() == flag2 & confirmed.HasValue))
        return;
      Decimal? shipmentQty = shipment.ShipmentQty;
      Decimal? controlQty = shipment.ControlQty;
      PXSetPropertyException propertyException1;
      if (!(shipmentQty.GetValueOrDefault() == controlQty.GetValueOrDefault() & shipmentQty.HasValue == controlQty.HasValue))
      {
        controlQty = shipment.ControlQty;
        Decimal num = 0M;
        if (!(controlQty.GetValueOrDefault() == num & controlQty.HasValue))
        {
          propertyException1 = new PXSetPropertyException("The document is out of the balance.");
          goto label_8;
        }
      }
      propertyException1 = (PXSetPropertyException) null;
label_8:
      PXSetPropertyException propertyException2 = propertyException1;
      cache.RaiseExceptionHandling<SOShipment.controlQty>((object) shipment, (object) shipment.ControlQty, (Exception) propertyException2);
    }
  }

  protected virtual bool IsFreightRecalculationNeeded(SOShipment row, SOShipment oldRow)
  {
    return !((PXSelectBase) this.Document).Cache.ObjectsEqualBy<TypeArrayOf<IBqlField>.FilledWith<SOShipment.lineTotal, SOShipment.shipmentWeight, SOShipment.packageWeight, SOShipment.shipmentVolume, SOShipment.shipTermsID, SOShipment.shipZoneID, SOShipment.shipVia, SOShipment.curyFreightCost, SOShipment.overrideFreightAmount>>((object) oldRow, (object) row);
  }

  protected virtual void RecalculateFreight(SOShipment row, SOShipment oldRow)
  {
    PXResultset<SOShipLine> pxResultset = ((PXSelectBase<SOShipLine>) this.Transactions).Select(Array.Empty<object>());
    if (pxResultset == null)
      return;
    PX.Objects.CS.Carrier carrier = PX.Objects.CS.Carrier.PK.Find((PXGraph) this, row.ShipVia);
    if (!((PXSelectBase) this.Document).Cache.ObjectsEqual<SOShipment.shipVia>((object) oldRow, (object) row) && carrier?.CalcMethod == "M")
      row.FreightCost = new Decimal?(0M);
    if (this.UseFreightCalculator(row, carrier))
    {
      FreightCalculator freightCalculator = this.CreateFreightCalculator();
      freightCalculator.CalcFreightCost<SOShipment, SOShipment.curyFreightCost>(((PXSelectBase) this.Document).Cache, row);
      if (row.OverrideFreightAmount.GetValueOrDefault())
        return;
      freightCalculator.ApplyFreightTerms<SOShipment, SOShipment.curyFreightAmt>(((PXSelectBase) this.Document).Cache, row, new int?(pxResultset.Count));
    }
    else
    {
      if (!this.UseCarrierService(row, carrier) || row.OverrideFreightAmount.GetValueOrDefault())
        return;
      FreightCalculator freightCalculator = this.CreateFreightCalculator();
      if (!freightCalculator.IsFlatRate<SOShipment>(((PXSelectBase) this.Document).Cache, row))
        return;
      freightCalculator.ApplyFreightTerms<SOShipment, SOShipment.curyFreightAmt>(((PXSelectBase) this.Document).Cache, row, new int?(pxResultset.Count));
    }
  }

  private void CalculateFreightCost(bool supressErrors)
  {
    if (((PXSelectBase<SOShipment>) this.Document).Current.ShipVia == null)
      return;
    PX.Objects.CS.Carrier carrier = PX.Objects.CS.Carrier.PK.Find((PXGraph) this, ((PXSelectBase<SOShipment>) this.Document).Current.ShipVia);
    if (carrier == null || !carrier.IsExternal.GetValueOrDefault())
      return;
    bool? isActive = carrier.IsActive;
    bool flag = false;
    if (isActive.GetValueOrDefault() == flag & isActive.HasValue)
      throw new PXException("The Ship Via code is not active.");
    ICarrierService result = CarrierPluginMaint.CreateCarrierService((PXGraph) this, CarrierPlugin.PK.Find((PXGraph) this, carrier.CarrierPluginID), true).Result;
    result.Method = carrier.PluginMethod;
    CarrierResult<RateQuote> rateQuote = result.GetRateQuote(this.CarrierRatesExt.BuildRateRequest(((PXSelectBase<SOShipment>) this.Document).Current));
    if (rateQuote == null)
      return;
    StringBuilder stringBuilder = new StringBuilder();
    foreach (Message message in (IEnumerable<Message>) rateQuote.Messages)
      stringBuilder.AppendFormat("{0}:{1} ", (object) message.Code, (object) message.Description);
    if (rateQuote.IsSuccess)
    {
      ((PXSelectBase<SOShipment>) this.Document).Current.CuryFreightCost = new Decimal?(this.ConvertAmtToBaseCury(rateQuote.Result.Currency, ((PXSelectBase<ARSetup>) this.arsetup).Current.DefaultRateTypeID, ((PXSelectBase<SOShipment>) this.Document).Current.ShipDate.Value, rateQuote.Result.Amount));
      ((PXSelectBase<SOShipment>) this.Document).Current.FreightCostIsValid = new bool?(true);
      ((PXSelectBase<SOShipment>) this.Document).Update(((PXSelectBase<SOShipment>) this.Document).Current);
      if (rateQuote.Messages.Count <= 0)
        return;
      if (!supressErrors)
        ((PXSelectBase) this.Document).Cache.RaiseExceptionHandling<SOShipment.curyFreightCost>((object) ((PXSelectBase<SOShipment>) this.Document).Current, (object) ((PXSelectBase<SOShipment>) this.Document).Current.CuryFreightCost, (Exception) new PXSetPropertyException((IBqlTable) ((PXSelectBase<SOShipment>) this.Document).Current, stringBuilder.ToString(), (PXErrorLevel) 2));
      else
        PXTrace.WriteWarning(stringBuilder.ToString());
    }
    else
    {
      ((PXSelectBase<SOShipment>) this.Document).Current.FreightCostIsValid = new bool?(false);
      ((PXSelectBase<SOShipment>) this.Document).Update(((PXSelectBase<SOShipment>) this.Document).Current);
      if (!supressErrors)
      {
        ((PXSelectBase) this.Document).Cache.RaiseExceptionHandling<SOShipment.curyFreightCost>((object) ((PXSelectBase<SOShipment>) this.Document).Current, (object) ((PXSelectBase<SOShipment>) this.Document).Current.CuryFreightCost, (Exception) new PXSetPropertyException((IBqlTable) ((PXSelectBase<SOShipment>) this.Document).Current, "Carrier Service returned error. {0}", (PXErrorLevel) 4, new object[1]
        {
          (object) stringBuilder.ToString()
        }));
        throw new PXException("Carrier Service returned error. {0}", new object[1]
        {
          (object) stringBuilder.ToString()
        });
      }
      PXTrace.WriteError($"Carrier Service returned error. {stringBuilder.ToString()}");
    }
  }

  protected virtual void SOShipment_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (e.Row == null)
      return;
    SOShipment row1 = (SOShipment) e.Row;
    bool flag1 = row1.ShipmentType == "T";
    bool? confirmed = row1.Confirmed;
    bool flag2 = false;
    bool flag3 = confirmed.GetValueOrDefault() == flag2 & confirmed.HasValue;
    bool flag4 = row1.CurrentWorksheetNbr == null;
    bool? nullable;
    int num1;
    if (!flag4)
    {
      nullable = row1.Picked;
      num1 = nullable.GetValueOrDefault() ? 1 : 0;
    }
    else
      num1 = 1;
    bool flag5 = num1 != 0;
    bool flag6 = flag3;
    PXUIFieldAttribute.SetVisible<SOShipment.curyID>(sender, e.Row, PXAccess.FeatureInstalled<FeaturesSet.multicurrency>() && !flag1);
    bool flag7 = true;
    PXUIFieldAttribute.SetEnabled<SOShipment.curyID>(sender, e.Row, flag6 & flag7);
    PXCache pxCache1 = sender;
    object row2 = e.Row;
    int num2;
    if (flag6)
    {
      nullable = row1.OverrideFreightAmount;
      num2 = nullable.GetValueOrDefault() ? 1 : 0;
    }
    else
      num2 = 0;
    PXUIFieldAttribute.SetEnabled<SOShipment.curyFreightAmt>(pxCache1, row2, num2 != 0);
    PXUIFieldAttribute.SetEnabled<SOShipment.overrideFreightAmount>(sender, e.Row, this.AllowChangingOverrideFreightAmount(row1));
    sender.AllowInsert = true;
    sender.AllowUpdate = flag3;
    sender.AllowDelete = flag6 & flag4;
    ((PXSelectBase) this.Transactions).Cache.AllowInsert = false;
    ((PXSelectBase) this.Transactions).Cache.AllowUpdate = flag6 & flag4;
    ((PXSelectBase) this.Transactions).Cache.AllowDelete = flag6 & flag4;
    ((PXSelectBase) this.splits).Cache.AllowInsert = flag6 & flag4;
    ((PXSelectBase) this.splits).Cache.AllowUpdate = flag6 & flag4;
    ((PXSelectBase) this.splits).Cache.AllowDelete = flag6 & flag4;
    ((PXSelectBase) this.Packages).Cache.AllowInsert = flag3 & flag5;
    ((PXSelectBase) this.Packages).Cache.AllowUpdate = flag3 & flag5;
    ((PXSelectBase) this.Packages).Cache.AllowDelete = flag3 & flag5;
    PXCache pxCache2 = sender;
    object row3 = e.Row;
    nullable = ((PXSelectBase<SOSetup>) this.sosetup).Current.RequireShipmentTotal;
    int num3 = nullable.Value ? 1 : 0;
    PXUIFieldAttribute.SetVisible<SOShipment.controlQty>(pxCache2, row3, num3 != 0);
    bool flag8 = sender.AllowUpdate && ((PXSelectBase<SOShipLine>) this.Transactions).Select(Array.Empty<object>()).Count == 0;
    PXUIFieldAttribute.SetEnabled<SOShipment.shipmentType>(sender, e.Row, flag8 && sender.GetStatus(e.Row) == 2);
    PXUIFieldAttribute.SetEnabled<SOShipment.operation>(sender, e.Row, flag8);
    PXUIFieldAttribute.SetEnabled<SOShipment.customerID>(sender, e.Row, flag8);
    PXUIFieldAttribute.SetEnabled<SOShipment.customerLocationID>(sender, e.Row, flag8);
    PXUIFieldAttribute.SetEnabled<SOShipment.siteID>(sender, e.Row, flag8);
    PXUIFieldAttribute.SetEnabled<SOShipment.destinationSiteID>(sender, e.Row, flag8 & flag1);
    ((PXAction) this.validateAddresses).SetEnabled(flag6 && ((PXGraph) this).FindAllImplementations<IAddressValidationHelper>().RequiresValidation());
    if (((SOShipment) e.Row).ShipVia != null)
    {
      PX.Objects.CS.Carrier carrier = PX.Objects.CS.Carrier.PK.Find((PXGraph) this, row1.ShipVia);
      if (carrier != null)
        PXUIFieldAttribute.SetEnabled<SOShipment.curyFreightCost>(sender, e.Row, carrier.CalcMethod == "M" & flag6);
      string errorOnly = PXUIFieldAttribute.GetErrorOnly<SOShipment.curyFreightCost>(sender, (object) row1);
      if (carrier != null)
      {
        nullable = carrier.IsExternal;
        if (nullable.GetValueOrDefault() && string.IsNullOrEmpty(errorOnly))
        {
          PXCache pxCache3 = sender;
          object row4 = e.Row;
          nullable = row1.FreightCostIsValid;
          bool flag9 = false;
          string str = nullable.GetValueOrDefault() == flag9 & nullable.HasValue & flag6 ? "The freight cost is not up to date." : (string) null;
          PXUIFieldAttribute.SetWarning<SOShipment.curyFreightCost>(pxCache3, row4, str);
        }
      }
    }
    PXUIFieldAttribute.SetVisible<SOShipment.groundCollect>(sender, e.Row, this.CanUseGroundCollect(row1));
    PXUIFieldAttribute.SetVisible<SOShipment.customerID>(sender, e.Row, !flag1);
    PXUIFieldAttribute.SetVisible<SOShipment.customerLocationID>(sender, e.Row, !flag1);
    PXUIFieldAttribute.SetVisible<SOShipment.destinationSiteID>(sender, e.Row, flag1);
    PXUIFieldAttribute.SetVisible<SOShipLine.isFree>(((PXSelectBase) this.Transactions).Cache, (object) null, !flag1);
    PXUIFieldAttribute.SetRequired<SOShipment.destinationSiteID>(sender, true);
    PXUIFieldAttribute.SetVisible<SOShipment.curyFreightAmt>(sender, e.Row, EnumerableExtensions.IsIn<string>(row1.FreightAmountSource, (string) null, "S"));
    PXUIFieldAttribute.SetVisible<SOShipment.overrideFreightAmount>(sender, e.Row, EnumerableExtensions.IsIn<string>(row1.FreightAmountSource, (string) null, "S"));
  }

  protected virtual bool AllowChangingOverrideFreightAmount(SOShipment doc)
  {
    bool? confirmed = doc.Confirmed;
    bool flag = false;
    return confirmed.GetValueOrDefault() == flag & confirmed.HasValue && EnumerableExtensions.IsIn<string>(doc.FreightAmountSource, (string) null, "S");
  }

  protected virtual bool UseFreightCalculator(SOShipment row, PX.Objects.CS.Carrier carrier)
  {
    if (carrier == null)
      return true;
    return !carrier.IsExternal.GetValueOrDefault() && this.AllowCalculateFreight(row, carrier);
  }

  protected virtual bool UseCarrierService(SOShipment row, PX.Objects.CS.Carrier carrier)
  {
    return carrier != null && carrier.IsExternal.GetValueOrDefault() && this.AllowCalculateFreight(row, carrier);
  }

  protected virtual bool AllowCalculateFreight(SOShipment row, PX.Objects.CS.Carrier carrier)
  {
    return !(row.Operation == "R") || carrier.CalcFreightOnReturn.GetValueOrDefault();
  }

  protected virtual void SOShipment_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    if ((e.Operation & 3) == 3)
      return;
    SOShipment row = (SOShipment) e.Row;
    if (row.ShipmentType == "T" && !row.DestinationSiteID.HasValue)
      throw new PXRowPersistingException(typeof (SOOrder.destinationSiteID).Name, (object) null, "'{0}' cannot be empty.", new object[1]
      {
        (object) typeof (SOOrder.destinationSiteID).Name
      });
    if (FlaggedModeScopeBase<SOShipmentEntry.SkipShipCompleteValidationScope>.IsActive)
      return;
    this.ValidateShipComplete(row);
  }

  protected virtual void SOShipment_CustomerID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    sender.SetDefaultExt<SOShipment.customerLocationID>(e.Row);
  }

  protected virtual void SOShipment_CustomerLocationID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (((SOShipment) e.Row).ShipmentType != "T" && (!((SOShipment) e.Row).SiteID.HasValue || e.ExternalCall))
      sender.SetDefaultExt<SOShipment.siteID>(e.Row);
    SharedRecordAttribute.DefaultRecord<SOShipment.shipAddressID>(sender, e.Row);
    SharedRecordAttribute.DefaultRecord<SOShipment.shipContactID>(sender, e.Row);
  }

  protected virtual void SOShipment_DestinationSiteID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (e.Row is SOShipment row && !(row.ShipmentType != "T"))
      return;
    e.NewValue = (object) null;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void SOShipment_DestinationSiteID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    ((PXSetup<PX.Objects.GL.Branch, Where<PX.Objects.IN.INSite.siteID, Equal<Optional<SOShipment.destinationSiteID>>, And<Current<SOShipment.shipmentType>, Equal<INDocType.transfer>, Or<PX.Objects.IN.INSite.siteID, Equal<Optional<SOShipment.siteID>>, And<Current<SOShipment.shipmentType>, NotEqual<INDocType.transfer>>>>>>) this.Company).RaiseFieldUpdated(sender, e.Row);
    PX.Objects.GL.Branch branch = (PX.Objects.GL.Branch) null;
    using (new PXReadBranchRestrictedScope())
      branch = PXResultset<PX.Objects.GL.Branch>.op_Implicit(((PXSelectBase<PX.Objects.GL.Branch>) this.Company).Select(Array.Empty<object>()));
    if (((SOShipment) e.Row).ShipmentType == "T" && branch != null)
      sender.SetValueExt<SOShipment.customerID>(e.Row, (object) branch.BranchCD);
    SharedRecordAttribute.DefaultRecord<SOShipment.shipAddressID>(sender, e.Row);
    SharedRecordAttribute.DefaultRecord<SOShipment.shipContactID>(sender, e.Row);
  }

  protected virtual void SOShipment_ShipVia_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    this.RedefaultCurrencyInfo(sender, e);
    sender.SetDefaultExt<SOShipment.taxCategoryID>(e.Row);
    if (!(e.Row is SOShipment row))
      return;
    object valuePending = sender.GetValuePending<SOShipment.useCustomerAccount>(e.Row);
    if (valuePending != PXCache.NotSetValue)
    {
      bool? nullable1 = row.ShipViaUpdateFromShopForRate;
      if (!nullable1.GetValueOrDefault())
      {
        SOShipment soShipment = row;
        int num;
        if (this.CanUseCustomerAccount(row))
        {
          nullable1 = (bool?) valuePending;
          num = nullable1.GetValueOrDefault() ? 1 : 0;
        }
        else
          num = 0;
        bool? nullable2 = new bool?(num != 0);
        soShipment.UseCustomerAccount = nullable2;
        goto label_8;
      }
    }
    row.UseCustomerAccount = new bool?(this.CanUseCustomerAccount(row));
label_8:
    sender.SetValue<SOShipment.isPackageValid>((object) row, (object) false);
    ((PXSelectBase<SOShipment>) this.Document).Current.RecalcPackagesReason = new int?(((PXSelectBase<SOShipment>) this.Document).Current.RecalcPackagesReason.GetValueOrDefault() | 1);
  }

  public virtual void RedefaultCurrencyInfo(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    if (!PXAccess.FeatureInstalled<FeaturesSet.multicurrency>())
      return;
    PX.Objects.CM.CurrencyInfo currencyInfo = CurrencyInfoAttribute.SetDefaults<SOShipment.curyInfoID>(sender, e.Row);
    string error = PXUIFieldAttribute.GetError<PX.Objects.CM.CurrencyInfo.curyEffDate>(((PXSelectBase) this.currencyinfo).Cache, (object) currencyInfo);
    if (!string.IsNullOrEmpty(error))
      sender.RaiseExceptionHandling<SOShipment.shipDate>(e.Row, (object) ((SOShipment) e.Row).ShipDate, (Exception) new PXSetPropertyException(error, (PXErrorLevel) 2));
    if (currencyInfo == null)
      return;
    sender.SetValue<SOShipment.curyID>(e.Row, (object) currencyInfo.CuryID);
  }

  protected virtual bool CanUseCustomerAccount(SOShipment row)
  {
    PX.Objects.CS.Carrier carrier = PX.Objects.CS.Carrier.PK.Find((PXGraph) this, row.ShipVia);
    if (carrier != null && !string.IsNullOrEmpty(carrier.CarrierPluginID))
    {
      foreach (PXResult<CarrierPluginCustomer> pxResult in PXSelectBase<CarrierPluginCustomer, PXSelect<CarrierPluginCustomer, Where<CarrierPluginCustomer.carrierPluginID, Equal<Required<CarrierPluginCustomer.carrierPluginID>>, And<CarrierPluginCustomer.customerID, Equal<Required<CarrierPluginCustomer.customerID>>, And<CarrierPluginCustomer.isActive, Equal<True>>>>>.Config>.Select((PXGraph) this, new object[2]
      {
        (object) carrier.CarrierPluginID,
        (object) row.CustomerID
      }))
      {
        CarrierPluginCustomer carrierPluginCustomer = PXResult<CarrierPluginCustomer>.op_Implicit(pxResult);
        if (!string.IsNullOrEmpty(carrierPluginCustomer.CarrierAccount))
        {
          int? customerLocationId1 = carrierPluginCustomer.CustomerLocationID;
          int? customerLocationId2 = row.CustomerLocationID;
          if (!(customerLocationId1.GetValueOrDefault() == customerLocationId2.GetValueOrDefault() & customerLocationId1.HasValue == customerLocationId2.HasValue))
          {
            customerLocationId2 = carrierPluginCustomer.CustomerLocationID;
            if (customerLocationId2.HasValue)
              continue;
          }
          return true;
        }
      }
    }
    return false;
  }

  protected virtual bool CanUseGroundCollect(SOShipment row)
  {
    if (string.IsNullOrEmpty(row.ShipVia))
      return false;
    PX.Objects.CS.Carrier carrier = PX.Objects.CS.Carrier.PK.Find((PXGraph) this, row.ShipVia);
    return (carrier != null ? (!carrier.IsExternal.GetValueOrDefault() ? 1 : 0) : 1) == 0 && !string.IsNullOrEmpty(carrier?.CarrierPluginID) && CarrierPluginMaint.GetCarrierPluginAttributes((PXGraph) this, carrier.CarrierPluginID).Contains("COLLECT");
  }

  protected virtual void SOShipment_UseCustomerAccount_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    if (!(e.Row is SOShipment row))
      return;
    bool flag = this.CanUseCustomerAccount(row);
    if (e.NewValue != null && (bool) e.NewValue && !flag)
    {
      e.NewValue = (object) false;
      throw new PXSetPropertyException("Customer Account is not configured. Please setup the Carrier Account on the Carrier Plug-in screen.");
    }
  }

  protected virtual void SOShipment_ShipTermsID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    SOShipment row = (SOShipment) e.Row;
    if (row == null)
      return;
    int? orderCntr = row.OrderCntr;
    int num = 0;
    if (!(orderCntr.GetValueOrDefault() > num & orderCntr.HasValue) || !(row.FreightAmountSource == "O"))
      return;
    PXUIFieldAttribute.SetWarning<SOShipment.shipTermsID>(sender, e.Row, "Freight price has not been recalculated in the sales order.");
  }

  [InjectDependency]
  public IFinPeriodRepository FinPeriodRepository { get; set; }

  protected virtual void SOShipment_ShipDate_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    SOShipment row = (SOShipment) e.Row;
    if (e.NewValue != null && this.FinPeriodRepository.FindFinPeriodByDate((DateTime?) e.NewValue, new int?(0)) == null)
      throw new PXSetPropertyException<SOShipment.shipDate>("The financial period that corresponds to the {0} date does not exist in the {1} company.", new object[2]
      {
        e.NewValue,
        (object) PXAccess.GetOrganizationCD(new int?(0))
      });
  }

  protected virtual void SOShipLine_InventoryID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    object obj = sender.GetValue<SOShipLine.inventoryID>(e.Row);
    if (obj == null)
      return;
    e.NewValue = obj;
  }

  protected virtual void SOShipLine_SubItemID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    object obj = sender.GetValue<SOShipLine.subItemID>(e.Row);
    if (obj == null || e.NewValue == null || !e.ExternalCall)
      return;
    e.NewValue = obj;
  }

  protected virtual void SOShipLine_SiteID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    object obj = sender.GetValue<SOShipLine.siteID>(e.Row);
    if (obj == null || !e.ExternalCall)
      return;
    e.NewValue = obj;
  }

  protected virtual void _(
    PX.Data.Events.FieldVerifying<SOShipLine, SOShipLine.shippedQty> e)
  {
    if (((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<SOShipLine, SOShipLine.shippedQty>, SOShipLine, object>) e).NewValue == null)
      return;
    Decimal? newValue = (Decimal?) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<SOShipLine, SOShipLine.shippedQty>, SOShipLine, object>) e).NewValue;
    Decimal num = 0M;
    if (newValue.GetValueOrDefault() < num & newValue.HasValue && EnumerableExtensions.IsIn<string>(e.Row.LineType, "GI", "GN"))
      throw new PXSetPropertyException("'{0}' should not be negative.", new object[1]
      {
        (object) PXUIFieldAttribute.GetDisplayName<SOShipLine.shippedQty>(((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<SOShipLine, SOShipLine.shippedQty>>) e).Cache)
      });
  }

  protected virtual void SOShipLine_InventoryID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    sender.SetDefaultExt<SOShipLine.uOM>(e.Row);
    sender.SetDefaultExt<SOShipLine.unitWeigth>(e.Row);
    sender.SetDefaultExt<SOShipLine.unitVolume>(e.Row);
    PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this, e.Row is SOShipLine row ? row.InventoryID : new int?());
    if (inventoryItem == null || row == null)
      return;
    row.TranDesc = PXDBLocalizableStringAttribute.GetTranslation(((PXGraph) this).Caches[typeof (PX.Objects.IN.InventoryItem)], (object) inventoryItem, "Descr", ((PXSelectBase<PX.Objects.AR.Customer>) this.customer).Current?.LocaleName);
  }

  protected virtual void SOShipLine_LocationID_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    if (!PXAccess.FeatureInstalled<FeaturesSet.warehouseLocation>())
      return;
    e.NewValue = (object) null;
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void DefaultUnitPrice(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    object obj;
    sender.RaiseFieldDefaulting<SOShipLine.unitPrice>(e.Row, ref obj);
    if (obj == null || !((Decimal) obj != 0M))
      return;
    Decimal? nullable = new Decimal?(INUnitAttribute.ConvertFromTo<SOShipLine.inventoryID>(sender, e.Row, ((SOShipLine) e.Row).UOM, ((SOShipLine) e.Row).OrderUOM, (Decimal) obj, INPrecision.NOROUND));
    sender.SetValueExt<SOShipLine.unitPrice>(e.Row, (object) nullable);
  }

  protected virtual void DefaultUnitCost(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    object obj;
    sender.RaiseFieldDefaulting<SOShipLine.unitCost>(e.Row, ref obj);
    if (obj == null || !((Decimal) obj != 0M))
      return;
    Decimal? nullable = new Decimal?(INUnitAttribute.ConvertFromTo<SOShipLine.inventoryID>(sender, e.Row, ((SOShipLine) e.Row).UOM, ((SOShipLine) e.Row).OrderUOM, (Decimal) obj, INPrecision.UNITCOST));
    sender.SetValueExt<SOShipLine.unitCost>(e.Row, (object) nullable);
  }

  protected virtual void SOShipLine_UOM_FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is SOShipLine row))
      return;
    this.DefaultUnitPrice(sender, e);
    this.DefaultUnitCost(sender, e);
    ((PXSelectBase) this.Transactions).Cache.RaiseFieldUpdated<SOShipLine.origOrderQty>((object) row, (object) null);
  }

  protected virtual void SOShipLine_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is SOShipLine row))
      return;
    bool flag = row.LineType == "GI";
    PXUIFieldAttribute.SetEnabled<SOShipLine.subItemID>(sender, (object) row, flag);
    PXUIFieldAttribute.SetEnabled<SOShipLine.locationID>(sender, (object) row, flag);
    PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this, row.InventoryID);
    if (inventoryItem == null)
      return;
    PXCache cache = ((PXSelectBase) this.splits).Cache;
    bool? nullable = inventoryItem.KitItem;
    int num;
    if (nullable.GetValueOrDefault())
    {
      nullable = inventoryItem.StkItem;
      num = !nullable.GetValueOrDefault() ? 1 : 0;
    }
    else
      num = 0;
    PXUIFieldAttribute.SetEnabled<SOShipLineSplit.inventoryID>(cache, (object) null, num != 0);
  }

  protected virtual void SOShipLine_RowInserted(PXCache sender, PXRowInsertedEventArgs e)
  {
    ((PXSelectBase<SOShipment>) this.Document).SetValueExt<SOShipment.isPackageValid>(((PXSelectBase<SOShipment>) this.Document).Current, (object) false);
    ((PXSelectBase<SOShipment>) this.Document).Current.RecalcPackagesReason = new int?(((PXSelectBase<SOShipment>) this.Document).Current.RecalcPackagesReason.GetValueOrDefault() | 2);
    if (!(e.Row is SOShipLine row))
      return;
    row.SortOrder = row.LineNbr;
  }

  protected virtual void SOShipLine_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    SOShipLine row = e.Row as SOShipLine;
    SOShipLine oldRow = e.OldRow as SOShipLine;
    if (row != null && sender.GetStatus((object) row) == 2)
    {
      row.OriginalShippedQty = row.ShippedQty;
      row.BaseOriginalShippedQty = row.BaseShippedQty;
    }
    if (row == null || oldRow == null)
      return;
    Decimal? baseQty1 = row.BaseQty;
    Decimal? baseQty2 = oldRow.BaseQty;
    if (baseQty1.GetValueOrDefault() == baseQty2.GetValueOrDefault() & baseQty1.HasValue == baseQty2.HasValue)
      return;
    ((PXSelectBase<SOShipment>) this.Document).SetValueExt<SOShipment.isPackageValid>(((PXSelectBase<SOShipment>) this.Document).Current, (object) false);
    ((PXSelectBase<SOShipment>) this.Document).Current.RecalcPackagesReason = new int?(((PXSelectBase<SOShipment>) this.Document).Current.RecalcPackagesReason.GetValueOrDefault() | 2);
  }

  protected virtual void SOShipLine_RowDeleted(PXCache sender, PXRowDeletedEventArgs e)
  {
    if ((SOShipLine) e.Row == null || ((PXSelectBase) this.Document).Cache.GetStatus((object) ((PXSelectBase<SOShipment>) this.Document).Current) == 3)
      return;
    ((PXSelectBase<SOShipment>) this.Document).SetValueExt<SOShipment.isPackageValid>(((PXSelectBase<SOShipment>) this.Document).Current, (object) false);
    ((PXSelectBase<SOShipment>) this.Document).Current.RecalcPackagesReason = new int?(((PXSelectBase<SOShipment>) this.Document).Current.RecalcPackagesReason.GetValueOrDefault() | 2);
  }

  protected virtual void SOShipLine_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    SOShipLine row = (SOShipLine) e.Row;
    if ((e.Operation & 3) != 2 && (e.Operation & 3) != 1)
      return;
    this.CheckSplitsForSameTask(sender, row);
    this.CheckLocationTaskRule(sender, row);
    Decimal? shippedQty = row.ShippedQty;
    Decimal num1 = 0M;
    if (!(shippedQty.GetValueOrDefault() == num1 & shippedQty.HasValue))
      return;
    Decimal? baseShippedQty = row.BaseShippedQty;
    Decimal num2 = 0M;
    if (!(baseShippedQty.GetValueOrDefault() == num2 & baseShippedQty.HasValue))
      throw new PXRowPersistingException(typeof (SOShipLine.shippedQty).Name, (object) row.ShippedQty, "Due to the insufficient decimal precision, the quantity in this line is calculated as 0 in the {0} UOM. To confirm the shipment, either change the UOM in line {1} of the related sales order, or change the decimal precision on the Companies (CS101500) form.", new object[2]
      {
        (object) row.UOM,
        (object) row.OrigLineNbr
      });
  }

  protected virtual void SOShipLineSplit_InventoryID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    SOShipLine soShipLine = PXParentAttribute.SelectParent<SOShipLine>(sender, e.Row);
    if (soShipLine == null)
      return;
    PX.Objects.IN.InventoryItem inventoryItem1 = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this, soShipLine.InventoryID);
    if (inventoryItem1 == null || !inventoryItem1.KitItem.GetValueOrDefault() || inventoryItem1.StkItem.GetValueOrDefault())
      return;
    if (PXResultset<INKitSpecHdr>.op_Implicit(PXSelectBase<INKitSpecHdr, PXSelectJoin<INKitSpecHdr, LeftJoin<INKitSpecStkDet, On<INKitSpecStkDet.kitInventoryID, Equal<INKitSpecHdr.kitInventoryID>>, LeftJoin<INKitSpecNonStkDet, On<INKitSpecNonStkDet.kitInventoryID, Equal<INKitSpecHdr.kitInventoryID>>>>, Where<INKitSpecHdr.kitInventoryID, Equal<Required<INKitSpecStkDet.kitInventoryID>>, And<Where<INKitSpecStkDet.compInventoryID, Equal<Required<INKitSpecStkDet.compInventoryID>>, Or<INKitSpecNonStkDet.compInventoryID, Equal<Required<INKitSpecNonStkDet.compInventoryID>>>>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[3]
    {
      (object) soShipLine.InventoryID,
      e.NewValue,
      e.NewValue
    })) == null)
    {
      PX.Objects.IN.InventoryItem inventoryItem2 = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this, (int?) e.NewValue);
      PXSetPropertyException<SOShipLineSplit.inventoryID> propertyException = new PXSetPropertyException<SOShipLineSplit.inventoryID>("The item cannot be added to the shipment. It is not a component of the kit.");
      ((PXSetPropertyException) propertyException).ErrorValue = (object) inventoryItem2?.InventoryCD;
      throw propertyException;
    }
  }

  protected virtual void SOPackageDetailEx_Weight_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is SOPackageDetail row))
      return;
    row.Confirmed = new bool?(true);
  }

  protected virtual void SOPackageDetailEx_Weight_FieldVerifying(
    PXCache cache,
    PXFieldVerifyingEventArgs e)
  {
    SOPackageDetail row = (SOPackageDetail) e.Row;
    if (row == null)
      return;
    PX.Objects.CS.CSBox parent = (PX.Objects.CS.CSBox) PrimaryKeyOf<PX.Objects.CS.CSBox>.By<PX.Objects.CS.CSBox.boxID>.ForeignKeyOf<SOPackageDetail>.By<SOPackageDetail.boxID>.FindParent(cache.Graph, (SOPackageDetail.boxID) row, (PKFindOptions) 0);
    if (parent == null)
      return;
    Decimal? maxWeight = parent.MaxWeight;
    Decimal? newValue = (Decimal?) e.NewValue;
    if (maxWeight.GetValueOrDefault() < newValue.GetValueOrDefault() & maxWeight.HasValue & newValue.HasValue)
      throw new PXSetPropertyException("The weight specified exceeds the max. weight of the box. Choose a bigger box or use multiple boxes.");
  }

  protected virtual void SOPackageDetailEx_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is SOPackageDetail row))
      return;
    row.WeightUOM = ((PXSelectBase<CommonSetup>) this.commonsetup).Current.WeightUOM;
  }

  protected virtual bool BusinessNameLengthIsExceeded(SOShipment doc, SOShipmentContact contact)
  {
    if (doc == null || doc.ShipVia == null || doc.Confirmed.GetValueOrDefault() || contact == null || (contact.FullName ?? string.Empty).Length <= 35)
      return false;
    PX.Objects.CS.Carrier carrier = PX.Objects.CS.Carrier.PK.Find((PXGraph) this, ((PXSelectBase<SOShipment>) this.Document).Current.ShipVia);
    if (carrier == null || !carrier.IsExternal.GetValueOrDefault())
      return false;
    CarrierPlugin carrierPlugin = CarrierPlugin.PK.Find((PXGraph) this, carrier.CarrierPluginID);
    return carrierPlugin != null && !(carrierPlugin.PluginTypeName != "PX.UpsRestCarrier.UpsRestCarrier");
  }

  protected virtual void SOShipmentContact_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    SOShipmentContact row = (SOShipmentContact) e.Row;
    if (row == null)
      return;
    PXSetPropertyException propertyException1;
    if (this.BusinessNameLengthIsExceeded(((PXSelectBase<SOShipment>) this.Document).Current, row))
      propertyException1 = new PXSetPropertyException("The value in the {0} box is too long. Only first {1} characters will be sent to UPS.", (PXErrorLevel) 2, new object[2]
      {
        (object) PXUIFieldAttribute.GetDisplayName<SOContact.fullName>(sender),
        (object) 35
      });
    else
      propertyException1 = (PXSetPropertyException) null;
    PXSetPropertyException propertyException2 = propertyException1;
    sender.RaiseExceptionHandling<SOContact.fullName>(e.Row, (object) row.FullName, (Exception) propertyException2);
  }

  /// <summary>
  /// Gets graph instance required to process original document.
  /// </summary>
  public virtual PXGraph GetOrigDocumentGraph(string origDocumentType)
  {
    PXGraph origDocumentGraph = this.CreateOrigDocumentGraph(origDocumentType);
    this.MergeStatusCachesBetweenGraphs((PXGraph) this, origDocumentGraph);
    this.InitOrigDocumentGraph(origDocumentGraph);
    return origDocumentGraph;
  }

  protected virtual PXGraph CreateOrigDocumentGraph(string origDocumentType)
  {
    throw new PXNotSupportedException();
  }

  protected virtual void InitOrigDocumentGraph(PXGraph graph)
  {
  }

  protected virtual Tuple<SOAddress, SOContact> GetBillToAddressContact()
  {
    return new Tuple<SOAddress, SOContact>((SOAddress) null, (SOContact) null);
  }

  public virtual void CorrectShipment(CorrectShipmentArgs args)
  {
    ((PXGraph) this).Clear();
    SOShipment shipment = args.Shipment;
    ((PXSelectBase<SOShipment>) this.Document).Current = PXResultset<SOShipment>.op_Implicit(((PXSelectBase<SOShipment>) this.Document).Search<SOShipment.shipmentNbr>((object) shipment.ShipmentNbr, Array.Empty<object>()));
    bool? nullable = WorkflowAction.HasWorkflowActionEnabled<SOShipmentEntry, SOShipment>(this, (Expression<Func<SOShipmentEntry, PXAction<SOShipment>>>) (g => g.correctShipmentAction), ((PXSelectBase<SOShipment>) this.Document).Current);
    bool flag = false;
    if (nullable.GetValueOrDefault() == flag & nullable.HasValue)
      throw new PXInvalidOperationException("The {0} action is not available in the {1} document at the moment. The document is being used by another process.", new object[2]
      {
        (object) ((PXAction) this.correctShipmentAction).GetCaption(),
        (object) ((PXSelectBase) this.Document).Cache.GetRowDescription((object) ((PXSelectBase<SOShipment>) this.Document).Current)
      });
    this.MarkOpen(((PXSelectBase<SOShipment>) this.Document).Current);
    GraphHelper.MarkUpdated(((PXSelectBase) this.Document).Cache, (object) ((PXSelectBase<SOShipment>) this.Document).Current, true);
    ((PXSelectBase) this.Document).Cache.IsDirty = true;
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      this.UpdateOrigDocumentOnCorrectShipment(args);
      PXView pxView = new PXView((PXGraph) this, false, this.GetShipLineSplitsToCorrectCommand(args));
      object[] objArray1 = new object[1]
      {
        (object) ((PXSelectBase<SOShipment>) this.Document).Current
      };
      object[] objArray2 = Array.Empty<object>();
      foreach (PXResult<INItemPlan> pxResult in pxView.SelectMultiBound(objArray1, objArray2))
        ((PXGraph) this).Caches[typeof (INItemPlan)].Update((object) this.GetUpdatedPlanByShipLineSplit(args, pxResult));
      this.UpdateShipLinesOnCorrectShipment(args);
      this.AfterCorrectShipment();
      ((SelectedEntityEvent<SOShipment>) PXEntityEventBase<SOShipment>.Container<SOShipment.Events>.Select((Expression<Func<SOShipment.Events, PXEntityEvent<SOShipment.Events>>>) (e => e.ShipmentCorrected))).FireOn((PXGraph) this, ((PXSelectBase<SOShipment>) this.Document).Current);
      ((PXAction) this.Save).Press();
      transactionScope.Complete();
      ((PXSelectBase) this.Document).Cache.RestoreCopy((object) shipment, (object) ((PXSelectBase<SOShipment>) this.Document).Current);
    }
  }

  protected virtual void UpdateOrigDocumentOnCorrectShipment(CorrectShipmentArgs args)
  {
  }

  protected virtual BqlCommand GetShipLineSplitsToCorrectCommand(CorrectShipmentArgs args)
  {
    return BqlCommand.CreateInstance(new System.Type[1]
    {
      typeof (Select2<INItemPlan, InnerJoin<SOShipLineSplit, On<SOShipLineSplit.planID, Equal<INItemPlan.planID>>>, Where<SOShipLineSplit.shipmentNbr, Equal<Current<SOShipment.shipmentNbr>>>>)
    });
  }

  protected virtual INItemPlan GetUpdatedPlanByShipLineSplit(
    CorrectShipmentArgs args,
    PXResult<INItemPlan> pxResult)
  {
    SOShipLineSplit soShipLineSplit = PXResult.Unwrap<SOShipLineSplit>((object) pxResult);
    soShipLineSplit.Confirmed = new bool?(false);
    if (args.ShipLinesClearedSOAllocation.Contains(soShipLineSplit.LineNbr))
      soShipLineSplit.OrigPlanType = "60";
    GraphHelper.MarkUpdated(((PXGraph) this).Caches[typeof (SOShipLineSplit)], (object) soShipLineSplit, true);
    ((PXGraph) this).Caches[typeof (SOShipLineSplit)].IsDirty = true;
    INItemPlan copy = PXCache<INItemPlan>.CreateCopy(PXResult<INItemPlan>.op_Implicit(pxResult));
    copy.PlanType = soShipLineSplit.PlanType;
    copy.OrigPlanType = soShipLineSplit.OrigPlanType;
    return copy;
  }

  protected virtual void UpdateShipLinesOnCorrectShipment(CorrectShipmentArgs args)
  {
    foreach (PXResult<SOShipLine> pxResult in ((PXSelectBase<SOShipLine>) this.Transactions).Select(Array.Empty<object>()))
    {
      SOShipLine copy = PXCache<SOShipLine>.CreateCopy(PXResult<SOShipLine>.op_Implicit(pxResult));
      this.CorrectShipLine(args.ShipLinesClearedSOAllocation, copy);
      ((PXGraph) this).Caches[typeof (SOShipLine)].Update((object) copy);
    }
  }

  public virtual void CorrectShipLine(
    HashSet<int?> shipLinesClearedSOAllocation,
    SOShipLine shiplinecopy)
  {
    shiplinecopy.Confirmed = new bool?(false);
    shiplinecopy.InvoiceGroupNbr = new int?();
    if (!shipLinesClearedSOAllocation.Contains(shiplinecopy.LineNbr))
      return;
    shiplinecopy.OrigPlanType = "60";
  }

  protected virtual void AfterCorrectShipment()
  {
  }

  public virtual bool TryCompleteWorksheet(SOPickingWorksheet worksheet)
  {
    return this.TryCompleteWorksheet((PXGraph) this, worksheet);
  }

  public virtual bool TryCompleteWorksheet(PXGraph graph, SOPickingWorksheet worksheet)
  {
    int num;
    if (EnumerableExtensions.IsNotIn<string>(worksheet.Status, "C", "L"))
      num = GraphHelper.RowCast<SOShipment>((IEnumerable) PXSelectBase<SOShipment, PXViewOf<SOShipment>.BasedOn<SelectFromBase<SOShipment, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<SOShipment.currentWorksheetNbr, IBqlString>.IsEqual<P.AsString>>>.Config>.Select(graph, new object[1]
      {
        (object) worksheet.WorksheetNbr
      })).AsEnumerable<SOShipment>().All<SOShipment>((Func<SOShipment, bool>) (sh => sh.Confirmed.GetValueOrDefault())) ? 1 : 0;
    else
      num = 0;
    if (num == 0)
      return false;
    worksheet.Status = "C";
    GraphHelper.Caches<SOPickingWorksheet>(graph).Update(worksheet);
    GraphHelper.EnsureCachePersistence<SOPickingWorksheet>(graph);
    foreach (PXResult<SOPickingJob> pxResult in PXSelectBase<SOPickingJob, PXViewOf<SOPickingJob>.BasedOn<SelectFromBase<SOPickingJob, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<SOPickingJob.worksheetNbr, IBqlString>.IsEqual<P.AsString>>>.Config>.Select(graph, new object[1]
    {
      (object) worksheet.WorksheetNbr
    }))
    {
      SOPickingJob soPickingJob = PXResult<SOPickingJob>.op_Implicit(pxResult);
      soPickingJob.Status = "CMP";
      GraphHelper.Caches<SOPickingJob>(graph).Update(soPickingJob);
      GraphHelper.EnsureCachePersistence<SOPickingJob>(graph);
    }
    return true;
  }

  public virtual void UpdateOrigValues(SOShipLine shipline, SOLine soline, Decimal? baseOrigQty)
  {
  }

  public virtual IEnumerable<PXResult<SOShipLine, SOLine>> CollectDropshipDetails(
    SOOrderShipment shipment)
  {
    object[] objArray1 = new object[1]{ (object) shipment };
    object[] objArray2 = new object[2]
    {
      shipment.Operation == "R" ? (object) "RN" : (object) "RT",
      (object) shipment.ShipmentNbr
    };
    foreach (PXResult<PX.Objects.PO.POReceiptLine, SOLineSplit, SOLine> pxResult in ((IEnumerable<PXResult<PX.Objects.PO.POReceiptLine>>) PXSelectBase<PX.Objects.PO.POReceiptLine, PXSelectJoin<PX.Objects.PO.POReceiptLine, InnerJoin<SOLineSplit, On<SOLineSplit.pOType, Equal<PX.Objects.PO.POReceiptLine.pOType>, And<SOLineSplit.pONbr, Equal<PX.Objects.PO.POReceiptLine.pONbr>, And<SOLineSplit.pOLineNbr, Equal<PX.Objects.PO.POReceiptLine.pOLineNbr>>>>, InnerJoin<SOLine, On<SOLineSplit.FK.OrderLine>>>, Where<PX.Objects.PO.POReceiptLine.lineType, In3<POLineType.goodsForDropShip, POLineType.nonStockForDropShip>, And<PX.Objects.PO.POReceiptLine.receiptType, Equal<Required<PX.Objects.PO.POReceiptLine.receiptType>>, And<PX.Objects.PO.POReceiptLine.receiptNbr, Equal<Required<PX.Objects.PO.POReceiptLine.receiptNbr>>, And<SOLine.orderType, Equal<Current<SOOrderShipment.orderType>>, And<SOLine.orderNbr, Equal<Current<SOOrderShipment.orderNbr>>>>>>>>.Config>.SelectMultiBound((PXGraph) this, objArray1, objArray2)).AsEnumerable<PXResult<PX.Objects.PO.POReceiptLine>>().Cast<PXResult<PX.Objects.PO.POReceiptLine, SOLineSplit, SOLine>>())
      yield return new PXResult<SOShipLine, SOLine>(SOShipLine.FromDropShip(PXResult<PX.Objects.PO.POReceiptLine, SOLineSplit, SOLine>.op_Implicit(pxResult), PXResult<PX.Objects.PO.POReceiptLine, SOLineSplit, SOLine>.op_Implicit(pxResult)), PXResult<PX.Objects.PO.POReceiptLine, SOLineSplit, SOLine>.op_Implicit(pxResult));
  }

  public virtual INRegisterEntryFactory CreateINRegisterFactory()
  {
    return new INRegisterEntryFactory(this);
  }

  public void MergeCachesWithINRegisterEntry(INRegisterEntryBase graph)
  {
    this.MergeStatusCachesBetweenGraphs((PXGraph) this, (PXGraph) graph);
  }

  protected virtual void MergeStatusCachesBetweenGraphs(PXGraph source, PXGraph target)
  {
    target.Caches[typeof (PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter)] = source.Caches[typeof (PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter)];
    target.Caches[typeof (PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.LocationStatusByCostCenter)] = source.Caches[typeof (PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.LocationStatusByCostCenter)];
    target.Caches[typeof (PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.LotSerialStatusByCostCenter)] = source.Caches[typeof (PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.LotSerialStatusByCostCenter)];
    target.Caches[typeof (SiteLotSerial)] = source.Caches[typeof (SiteLotSerial)];
    target.Caches[typeof (PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.ItemLotSerial)] = source.Caches[typeof (PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.ItemLotSerial)];
    target.Views.Caches.Remove(typeof (PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.SiteStatusByCostCenter));
    target.Views.Caches.Remove(typeof (PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.LocationStatusByCostCenter));
    target.Views.Caches.Remove(typeof (PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.LotSerialStatusByCostCenter));
    target.Views.Caches.Remove(typeof (SiteLotSerial));
    target.Views.Caches.Remove(typeof (PX.Objects.IN.InventoryRelease.Accumulators.QtyAllocated.ItemLotSerial));
  }

  protected virtual SOPackageEngine CreatePackageEngine() => new SOPackageEngine((PXGraph) this);

  protected virtual void CheckLocationTaskRule(PXCache sender, SOShipLine row)
  {
    if (!row.TaskID.HasValue)
      return;
    INLocation inLocation = INLocation.PK.Find((PXGraph) this, row.LocationID);
    if (inLocation == null)
      return;
    int? taskId1 = inLocation.TaskID;
    int? taskId2 = row.TaskID;
    if (taskId1.GetValueOrDefault() == taskId2.GetValueOrDefault() & taskId1.HasValue == taskId2.HasValue)
      return;
    taskId2 = inLocation.TaskID;
    if (!taskId2.HasValue)
      return;
    sender.RaiseExceptionHandling<SOShipLine.locationID>((object) row, (object) inLocation.LocationCD, (Exception) new PXSetPropertyException("The Project Task specified for the given Location do not match the selected Project Task.", (PXErrorLevel) 2));
  }

  [Obsolete]
  protected virtual void CheckSplitsForSameTask(PXCache sender, SOShipLine row)
  {
  }

  public virtual void ShipPackages(SOShipment shiporder)
  {
    PX.Objects.CS.Carrier carrier = PX.Objects.CS.Carrier.PK.Find((PXGraph) this, shiporder.ShipVia);
    bool? nullable1;
    if (carrier != null)
    {
      nullable1 = carrier.IsActive;
      bool flag = false;
      if (nullable1.GetValueOrDefault() == flag & nullable1.HasValue)
        throw new PXException("The Ship Via code is not active.");
    }
    if (!this.UseCarrierService(shiporder, carrier))
      return;
    CarrierPlugin carrierPlugin = (CarrierPlugin) null;
    nullable1 = carrier.IsExternal;
    if (nullable1.GetValueOrDefault())
    {
      carrierPlugin = CarrierPlugin.PK.Find((PXGraph) this, carrier.CarrierPluginID);
      if (carrierPlugin != null && carrierPlugin.SiteID.HasValue)
      {
        int? siteId1 = carrierPlugin.SiteID;
        int? siteId2 = shiporder.SiteID;
        if (!(siteId1.GetValueOrDefault() == siteId2.GetValueOrDefault() & siteId1.HasValue == siteId2.HasValue))
          throw new PXException("The ship via code selected in the shipment is not applicable to the {0} warehouse. Select a ship via code that is associated with the {0} warehouse.", new object[1]
          {
            ((PXSelectBase) this.Document).Cache.GetValueExt<SOShipment.siteID>((object) shiporder)
          });
      }
    }
    nullable1 = shiporder.ShippedViaCarrier;
    if (nullable1.GetValueOrDefault())
      return;
    ICarrierService carrierService = CarrierMaint.CreateCarrierService((PXGraph) this, shiporder.ShipVia);
    CarrierRequest carrierRequest = this.CarrierRatesExt.BuildRequest(shiporder);
    if (carrierRequest.Packages.Count <= 0)
      return;
    CarrierResult<ShipResult> carrierResult = carrierService.Ship(carrierRequest);
    if (carrierResult == null)
      return;
    StringBuilder stringBuilder = new StringBuilder();
    foreach (Message message in (IEnumerable<Message>) carrierResult.Messages)
      stringBuilder.AppendFormat("{0}:{1} ", (object) message.Code, (object) message.Description);
    if (carrierResult.IsSuccess)
    {
      using (PXTransactionScope transactionScope = new PXTransactionScope())
      {
        PXTransactionScope.SetSuppressWorkflow(true);
        ((PXSelectBase<SOShipment>) this.Document).Current = PXResultset<SOShipment>.op_Implicit(((PXSelectBase<SOShipment>) this.Document).Search<SOShipment.shipmentNbr>((object) shiporder.ShipmentNbr, Array.Empty<object>()));
        Decimal num = 0M;
        DateTime? shipDate;
        if (!shiporder.UseCustomerAccount.GetValueOrDefault() && (!shiporder.GroundCollect.GetValueOrDefault() || !this.CanUseGroundCollect(shiporder)))
        {
          string currency = carrierResult.Result.Cost.Currency;
          string defaultRateTypeId = ((PXSelectBase<ARSetup>) this.arsetup).Current.DefaultRateTypeID;
          shipDate = shiporder.ShipDate;
          DateTime effectiveDate = shipDate.Value;
          Decimal amount = carrierResult.Result.Cost.Amount;
          num = this.ConvertAmtToBaseCury(currency, defaultRateTypeId, effectiveDate, amount);
        }
        ((PXSelectBase<SOShipment>) this.Document).Current.FreightCost = new Decimal?(num);
        PXCurrencyAttribute.CuryConvCury<SOShipment.curyFreightCost>(((PXSelectBase) this.Document).Cache, (object) ((PXSelectBase<SOShipment>) this.Document).Current);
        if (!((PXSelectBase<SOShipment>) this.Document).Current.OverrideFreightAmount.GetValueOrDefault())
        {
          if (carrierResult.Result.Price == null)
          {
            PXResultset<SOShipLine> pxResultset = ((PXSelectBase<SOShipLine>) this.Transactions).Select(Array.Empty<object>());
            this.CreateFreightCalculator().ApplyFreightTerms<SOShipment, SOShipment.curyFreightAmt>(((PXSelectBase) this.Document).Cache, ((PXSelectBase<SOShipment>) this.Document).Current, new int?(pxResultset.Count));
          }
          else
          {
            SOShipment current = ((PXSelectBase<SOShipment>) this.Document).Current;
            string currency = carrierResult.Result.Price.Currency;
            string defaultRateTypeId = ((PXSelectBase<ARSetup>) this.arsetup).Current.DefaultRateTypeID;
            shipDate = ((PXSelectBase<SOShipment>) this.Document).Current.ShipDate;
            DateTime effectiveDate = shipDate.Value;
            Decimal amount = carrierResult.Result.Price.Amount;
            Decimal? nullable2 = new Decimal?(this.ConvertAmtToBaseCury(currency, defaultRateTypeId, effectiveDate, amount));
            current.FreightAmt = nullable2;
            PXCurrencyAttribute.CuryConvCury<SOShipment.curyFreightAmt>(((PXSelectBase) this.Document).Cache, (object) ((PXSelectBase<SOShipment>) this.Document).Current);
          }
        }
        ((PXSelectBase<SOShipment>) this.Document).Current.ShippedViaCarrier = new bool?(true);
        ((PXSelectBase<SOShipment>) this.Document).Current.FreightCostIsValid = new bool?(true);
        UploadFileMaintenance instance = PXGraph.CreateInstance<UploadFileMaintenance>();
        Guid? uid;
        if (carrierResult.Result.Image != null)
        {
          FileInfo fileInfo = new FileInfo($"High Value Report.{carrierResult.Result.Format}", (string) null, carrierResult.Result.Image);
          try
          {
            instance.SaveFile(fileInfo, (FileExistsAction) 1);
          }
          catch (PXNotSupportedFileTypeException ex)
          {
            object[] objArray = new object[1]
            {
              (object) carrierResult.Result.Format
            };
            throw new PXException((Exception) ex, "The carrier service has returned the file in the {0} format, which is not allowed for upload. Add the {0} type to the list of allowed file types on the File Upload Preferences form.", objArray);
          }
          PXCache cache = ((PXSelectBase) this.Document).Cache;
          SOShipment current = ((PXSelectBase<SOShipment>) this.Document).Current;
          Guid[] guidArray = new Guid[1];
          uid = fileInfo.UID;
          guidArray[0] = uid.Value;
          PXNoteAttribute.SetFileNotes(cache, (object) current, guidArray);
        }
        if (carrierResult.Result.AttachedFiles != null)
        {
          foreach (CarrierFileInfo attachedFile in (IEnumerable<CarrierFileInfo>) carrierResult.Result.AttachedFiles)
          {
            FileInfo fileInfo = new FileInfo($"{attachedFile.Name}.{attachedFile.Format}", (string) null, attachedFile.Data);
            try
            {
              instance.SaveFile(fileInfo, (FileExistsAction) 1);
            }
            catch (PXNotSupportedFileTypeException ex)
            {
              object[] objArray = new object[1]
              {
                (object) attachedFile.Format
              };
              throw new PXException((Exception) ex, "The carrier service has returned the file in the {0} format, which is not allowed for upload. Add the {0} type to the list of allowed file types on the File Upload Preferences form.", objArray);
            }
            PXCache cache = ((PXSelectBase) this.Document).Cache;
            SOShipment current = ((PXSelectBase<SOShipment>) this.Document).Current;
            Guid[] guidArray = new Guid[1];
            uid = fileInfo.UID;
            guidArray[0] = uid.Value;
            PXNoteAttribute.SetFileNotes(cache, (object) current, guidArray);
          }
        }
        ((PXSelectBase<SOShipment>) this.Document).Update(((PXSelectBase<SOShipment>) this.Document).Current);
        foreach (PackageData packageData in (IEnumerable<PackageData>) carrierResult.Result.Data)
        {
          SOPackageDetailEx soPackageDetailEx1 = PXResultset<SOPackageDetailEx>.op_Implicit(PXSelectBase<SOPackageDetailEx, PXSelect<SOPackageDetailEx, Where<SOPackageDetailEx.shipmentNbr, Equal<Required<SOShipment.shipmentNbr>>, And<SOPackageDetailEx.lineNbr, Equal<Required<SOPackageDetailEx.lineNbr>>>>>.Config>.Select((PXGraph) this, new object[2]
          {
            (object) shiporder.ShipmentNbr,
            (object) packageData.RefNbr
          }));
          if (soPackageDetailEx1 != null)
          {
            if (packageData.Image != null)
            {
              FileInfo fileInfo = new FileInfo($"Label #{packageData.TrackingNumber}.{packageData.Format}", (string) null, packageData.Image);
              try
              {
                instance.SaveFile(fileInfo);
              }
              catch (PXNotSupportedFileTypeException ex)
              {
                object[] objArray = new object[1]
                {
                  (object) packageData.Format
                };
                throw new PXException((Exception) ex, "The carrier service has returned the file in the {0} format, which is not allowed for upload. Add the {0} type to the list of allowed file types on the File Upload Preferences form.", objArray);
              }
              PXCache cache = ((PXSelectBase) this.Packages).Cache;
              SOPackageDetailEx soPackageDetailEx2 = soPackageDetailEx1;
              Guid[] guidArray = new Guid[1];
              uid = fileInfo.UID;
              guidArray[0] = uid.Value;
              PXNoteAttribute.SetFileNotes(cache, (object) soPackageDetailEx2, guidArray);
              CarrierMethodSelectorAttribute.CarrierPluginMethod carrierPluginMethod = PXSelectorAttribute.Select<PX.Objects.CS.Carrier.pluginMethod>(((PXSelectBase) this.carrier).Cache, (object) carrier) as CarrierMethodSelectorAttribute.CarrierPluginMethod;
              string str = $"{carrier.PluginMethod} - {carrierPluginMethod?.Description}";
              if (str.Length > (int) byte.MaxValue)
                str = str.Substring(0, (int) byte.MaxValue);
              string currency = carrierResult.Result.Cost.Currency;
              string defaultRateTypeId = ((PXSelectBase<ARSetup>) this.arsetup).Current.DefaultRateTypeID;
              shipDate = shiporder.ShipDate;
              DateTime effectiveDate = shipDate.Value;
              Decimal rateAmount = packageData.RateAmount;
              Decimal baseCury = this.ConvertAmtToBaseCury(currency, defaultRateTypeId, effectiveDate, rateAmount);
              ((PXSelectBase<CarrierLabelHistory>) this.LabelHistory).Insert(new CarrierLabelHistory()
              {
                ShipmentNbr = shiporder.ShipmentNbr,
                LineNbr = new int?(packageData.RefNbr),
                PluginTypeName = carrierPlugin?.PluginTypeName,
                ServiceMethod = str,
                RateAmount = new Decimal?(baseCury)
              });
            }
            soPackageDetailEx1.TrackNumber = packageData.TrackingNumber;
            soPackageDetailEx1.TrackUrl = packageData.TrackingUrl;
            soPackageDetailEx1.TrackData = packageData.TrackingData;
            ((PXSelectBase<SOPackageDetailEx>) this.Packages).Update(soPackageDetailEx1);
          }
        }
        ((PXAction) this.Save).Press();
        transactionScope.Complete();
      }
      ((PXSelectBase) this.Document).Cache.RestoreCopy((object) shiporder, (object) ((PXSelectBase<SOShipment>) this.Document).Current);
      if (carrierResult.Messages.Count <= 0)
        return;
      ((PXSelectBase) this.Document).Cache.RaiseExceptionHandling<SOShipment.curyFreightCost>((object) shiporder, (object) shiporder.CuryFreightCost, (Exception) new PXSetPropertyException(stringBuilder.ToString(), (PXErrorLevel) 2));
      PXTrace.WriteWarning(stringBuilder.ToString());
    }
    else
    {
      if (!string.IsNullOrEmpty(carrierResult.RequestData))
        PXTrace.WriteError(carrierResult.RequestData);
      ((PXSelectBase) this.Document).Cache.RaiseExceptionHandling<SOShipment.curyFreightCost>((object) shiporder, (object) shiporder.CuryFreightCost, (Exception) new PXSetPropertyException("Carrier Service returned error. {0}", (PXErrorLevel) 4, new object[1]
      {
        (object) stringBuilder.ToString()
      }));
      throw new PXException("Carrier Service returned error. {0}", new object[1]
      {
        (object) stringBuilder.ToString()
      });
    }
  }

  protected virtual FreightCalculator CreateFreightCalculator()
  {
    return new FreightCalculator((PXGraph) this);
  }

  public virtual void CancelPackages(SOShipment shiporder, bool isReturn = false)
  {
    if (!shiporder.ShippedViaCarrier.GetValueOrDefault() || !this.IsWithLabels(shiporder.ShipVia))
      return;
    SOShipment soShipment = PXResultset<SOShipment>.op_Implicit(((PXSelectBase<SOShipment>) this.Document).Search<SOShipment.shipmentNbr>((object) shiporder.ShipmentNbr, Array.Empty<object>()));
    ICarrierService carrierService = CarrierMaint.CreateCarrierService((PXGraph) this, soShipment.ShipVia);
    SOPackageDetailEx soPackageDetailEx1 = PXResultset<SOPackageDetailEx>.op_Implicit(PXSelectBase<SOPackageDetailEx, PXSelect<SOPackageDetailEx, Where<SOPackageDetailEx.shipmentNbr, Equal<Required<SOShipment.shipmentNbr>>>>.Config>.SelectWindowed((PXGraph) this, 0, 1, new object[1]
    {
      (object) soShipment.ShipmentNbr
    }));
    string str = isReturn ? soPackageDetailEx1.ReturnTrackNumber : soPackageDetailEx1.TrackNumber;
    if (soPackageDetailEx1 == null || string.IsNullOrEmpty(str))
      return;
    CarrierResult<string> carrierResult = carrierService.Cancel(str, soPackageDetailEx1.TrackData);
    if (carrierResult == null)
      return;
    StringBuilder stringBuilder = new StringBuilder();
    foreach (Message message in (IEnumerable<Message>) carrierResult.Messages)
      stringBuilder.AppendFormat("{0}:{1} ", (object) message.Code, (object) message.Description);
    foreach (PXResult<SOPackageDetailEx> pxResult1 in PXSelectBase<SOPackageDetailEx, PXSelect<SOPackageDetailEx, Where<SOPackageDetailEx.shipmentNbr, Equal<Required<SOShipment.shipmentNbr>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) soShipment.ShipmentNbr
    }))
    {
      SOPackageDetailEx soPackageDetailEx2 = PXResult<SOPackageDetailEx>.op_Implicit(pxResult1);
      soPackageDetailEx2.Confirmed = new bool?(false);
      if (!isReturn)
        soPackageDetailEx2.TrackNumber = (string) null;
      soPackageDetailEx2.ReturnTrackNumber = (string) null;
      soPackageDetailEx2.TrackUrl = (string) null;
      ((PXSelectBase<SOPackageDetailEx>) this.Packages).Update(soPackageDetailEx2);
      foreach (PXResult<NoteDoc> pxResult2 in PXSelectBase<NoteDoc, PXSelect<NoteDoc, Where<NoteDoc.noteID, Equal<Required<NoteDoc.noteID>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) soPackageDetailEx2.NoteID
      }))
        UploadFileMaintenance.DeleteFile(PXResult<NoteDoc>.op_Implicit(pxResult2).FileID);
    }
    soShipment.CuryFreightCost = new Decimal?(0M);
    if (!soShipment.OverrideFreightAmount.GetValueOrDefault())
      soShipment.CuryFreightAmt = new Decimal?(0M);
    soShipment.ShippedViaCarrier = new bool?(false);
    ((PXSelectBase<SOShipment>) this.Document).Update(soShipment);
    ((PXSelectBase) this.Document).Cache.RestoreCopy((object) shiporder, (object) ((PXSelectBase<SOShipment>) this.Document).Current);
    ((PXAction) this.Save).Press();
    if (!carrierResult.IsSuccess)
    {
      PXTrace.WriteWarning("Tracking Numbers and Labels for the shipment was succesfully cleared but Carrier Void Service Returned Error: " + stringBuilder.ToString());
    }
    else
    {
      if (carrierResult.Messages.Count <= 0)
        return;
      PXTrace.WriteWarning("Tracking Numbers and Labels for the shipment was succesfully cleared but Carrier Void Service Returned Warnings: " + stringBuilder.ToString());
    }
  }

  protected static System.Threading.Tasks.Task PrintPickListOperation(
    List<SOShipment> list,
    PXAdapter uiAdapter,
    CancellationToken cancellationToken)
  {
    SOShipmentEntry instance = PXGraph.CreateInstance<SOShipmentEntry>();
    return instance.PrintPickList(list, uiAdapter.CloneTo((PXGraph) instance), cancellationToken);
  }

  [Obsolete]
  protected virtual System.Threading.Tasks.Task PrintPickList(
    List<SOShipment> list,
    CancellationToken cancellationToken)
  {
    return this.PrintPickList(list, (PXAdapter) null, cancellationToken);
  }

  protected virtual async System.Threading.Tasks.Task PrintPickList(
    List<SOShipment> list,
    PXAdapter adapter,
    CancellationToken cancellationToken)
  {
    SOShipmentEntry graph = this;
    if (list.Count == 0)
      ;
    else
    {
      PXProcessing<SOShipment>.ProcessRecords((IEnumerable<SOShipment>) list, adapter.MassProcess, (Action<SOShipment>) (sh =>
      {
        SOShipment soShipment = PXResultset<SOShipment>.op_Implicit(((PXSelectBase<SOShipment>) this.Document).Search<SOShipment.shipmentNbr>((object) sh.ShipmentNbr, Array.Empty<object>()));
        soShipment.PickListPrinted = new bool?(true);
        if (!((PXSelectBase<SOShipment>) this.Document).Update(soShipment).Hold.GetValueOrDefault())
          return;
        PXActionExtensionSuppressWorkflowAutoPersistScope.PressWithSuppressedWorkflowPersist((PXAction) this.releaseFromHold);
      }), (Action<SOShipment>) null, (Func<SOShipment, Exception, bool, bool?>) null, (Action<SOShipment>) null, (Action<SOShipment>) null);
      PXReportRequiredException ex = (PXReportRequiredException) null;
      // ISSUE: method pointer
      // ISSUE: method pointer
      using (new SimpleScope((System.Action) (() => ((PXGraph) this).RowPersisted.AddHandler<SOShipment>(new PXRowPersisted((object) this, __methodptr(\u003CPrintPickList\u003Eg__shipmentPersisted\u007C1)))), (System.Action) (() => ((PXGraph) this).RowPersisted.RemoveHandler<SOShipment>(new PXRowPersisted((object) this, __methodptr(\u003CPrintPickList\u003Eg__shipmentPersisted\u007C1))))))
        ((PXAction) graph.Save).Press();
      PXAdapter pxAdapter = adapter;
      if ((pxAdapter != null ? (pxAdapter.MassProcess ? 1 : 0) : 0) != 0)
        list.ForEach((Action<SOShipment>) (sh => ((PXSelectBase) this.Document).Cache.RestoreCopy((object) sh, (object) SOShipment.PK.Find((PXGraph) this, sh.ShipmentNbr))));
      if (ex == null)
        ;
      else
      {
        if (PXAccess.FeatureInstalled<FeaturesSet.deviceHub>())
        {
          int num = await SMPrintJobMaint.CreatePrintJobGroup(adapter, new Func<string, string, int?, Guid?>(new NotificationUtility((PXGraph) graph).SearchPrinter), "Customer", "SO644000", ((PXGraph) graph).Accessinfo.BranchID, ex, "Print Pick List", cancellationToken) ? 1 : 0;
        }
        throw ex;
      }
    }
  }

  protected PXAdapter CreateDummyAdapter()
  {
    return new PXAdapter((PXView) PXView.Dummy.For<SOShipment>((PXGraph) this))
    {
      MassProcess = true,
      Arguments = {
        ["PrintWithDeviceHub"] = (object) true,
        ["DefinePrinterManually"] = (object) false
      }
    };
  }

  protected virtual bool IsWithLabels(string shipVia)
  {
    PX.Objects.CS.Carrier carrier = PX.Objects.CS.Carrier.PK.Find((PXGraph) this, shipVia);
    return carrier != null && carrier.IsExternal.GetValueOrDefault();
  }

  protected virtual bool ValidateAvailablePackages()
  {
    if (string.IsNullOrEmpty(((PXSelectBase<SOShipment>) this.Document).Current.ShipVia))
      return false;
    HashSet<string> hashSet = this.CreatePackageEngine().GetBoxesByCarrierID(((PXSelectBase<SOShipment>) this.Document).Current.ShipVia).Select<PX.Objects.CS.CSBox, string>((Func<PX.Objects.CS.CSBox, string>) (b => b.BoxID)).ToHashSet<string>();
    foreach (PXResult<SOPackageDetailEx> pxResult in ((PXSelectBase<SOPackageDetailEx>) this.Packages).Select(Array.Empty<object>()))
    {
      SOPackageDetail soPackageDetail = (SOPackageDetail) PXResult<SOPackageDetailEx>.op_Implicit(pxResult);
      if (!hashSet.Contains(soPackageDetail.BoxID))
        return false;
    }
    return true;
  }

  public bool IsPPS => ((PXGraph) this).FindImplementation<PickPackShip>() != null;

  protected virtual void ValidateShipComplete(SOShipment shipment)
  {
  }

  protected Decimal ConvertAmtToBaseCury(
    string from,
    string rateType,
    DateTime effectiveDate,
    Decimal amount)
  {
    Decimal baseval = amount;
    using (new ReadOnlyScope(new PXCache[1]
    {
      ((PXSelectBase) this.DummyCuryInfo).Cache
    }))
    {
      PX.Objects.CM.CurrencyInfo info = (PX.Objects.CM.CurrencyInfo) ((PXSelectBase) this.DummyCuryInfo).Cache.Insert((object) new PX.Objects.CM.CurrencyInfo()
      {
        CuryRateTypeID = rateType,
        CuryID = from
      });
      info.SetCuryEffDate(((PXSelectBase) this.DummyCuryInfo).Cache, (object) effectiveDate);
      ((PXSelectBase) this.DummyCuryInfo).Cache.Update((object) info);
      PXCurrencyAttribute.CuryConvBase(((PXSelectBase) this.DummyCuryInfo).Cache, info, amount, out baseval);
      ((PXSelectBase) this.DummyCuryInfo).Cache.Delete((object) info);
    }
    return baseval;
  }

  public virtual Decimal GetQtyThreshold(SOShipLineSplit sosplit)
  {
    return ((Decimal?) PXSelectBase<SOLine, PXViewOf<SOLine>.BasedOn<SelectFromBase<SOLine, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<SOShipLine>.On<SOShipLine.FK.OrderLine>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOShipLine.shipmentNbr, Equal<P.AsString>>>>>.And<BqlOperand<SOShipLine.lineNbr, IBqlInt>.IsEqual<P.AsInt>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) sosplit.ShipmentNbr,
      (object) sosplit.LineNbr
    }).TopFirst?.CompleteQtyMax ?? 100M) / 100M;
  }

  public virtual Decimal GetMinQtyThreshold(SOShipLineSplit sosplit)
  {
    return ((Decimal?) PXSelectBase<SOLine, PXViewOf<SOLine>.BasedOn<SelectFromBase<SOLine, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<SOShipLine>.On<SOShipLine.FK.OrderLine>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<SOShipLine.shipmentNbr, Equal<P.AsString>>>>>.And<BqlOperand<SOShipLine.lineNbr, IBqlInt>.IsEqual<P.AsInt>>>>.Config>.Select((PXGraph) this, new object[2]
    {
      (object) sosplit.ShipmentNbr,
      (object) sosplit.LineNbr
    }).TopFirst?.CompleteQtyMin ?? 100M) / 100M;
  }

  protected virtual void MarkOpen(SOShipment shipment)
  {
    shipment.Confirmed = new bool?(false);
    shipment.ConfirmedToVerify = new bool?(true);
    shipment.Status = "N";
    shipment.LabelsPrinted = new bool?(false);
    shipment.CommercialInvoicesPrinted = new bool?(false);
  }

  [PXInternalUseOnly]
  protected virtual void SetSuppressWorkflowOnCorrectShipment()
  {
    PXTransactionScope.SetSuppressWorkflow(true);
  }

  public SOShipmentEntry.PackageDetail PackageDetailExt
  {
    get => ((PXGraph) this).FindImplementation<SOShipmentEntry.PackageDetail>();
  }

  public SOShipmentEntry.CarrierRates CarrierRatesExt
  {
    get => ((PXGraph) this).FindImplementation<SOShipmentEntry.CarrierRates>();
  }

  public SOShipmentEntry.CartSupport CartSupportExt
  {
    get => ((PXGraph) this).FindImplementation<SOShipmentEntry.CartSupport>();
  }

  public SOShipmentEntry.WorkLog WorkLogExt
  {
    get => ((PXGraph) this).FindImplementation<SOShipmentEntry.WorkLog>();
  }

  public class SkipShipCompleteValidationScope : 
    FlaggedModeScopeBase<SOShipmentEntry.SkipShipCompleteValidationScope>
  {
  }

  public class LineShipment : IEnumerable<SOShipLine>, IEnumerable, ICollection<SOShipLine>
  {
    private List<SOShipLine> _List = new List<SOShipLine>();
    public bool AnyDeleted;

    public int Count => this._List.Count;

    public bool IsReadOnly => ((ICollection<SOShipLine>) this._List).IsReadOnly;

    public IEnumerator<SOShipLine> GetEnumerator()
    {
      return ((IEnumerable<SOShipLine>) this._List).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return (IEnumerator) ((IEnumerable<SOShipLine>) this._List).GetEnumerator();
    }

    public void Clear() => this._List.Clear();

    public bool Contains(SOShipLine item) => this._List.Contains(item);

    public void CopyTo(SOShipLine[] array, int arrayIndex) => this._List.CopyTo(array, arrayIndex);

    public bool Remove(SOShipLine item) => this._List.Remove(item);

    public void Add(SOShipLine item) => this._List.Add(item);
  }

  public class PackageDetail : PXGraphExtension<LabelsPrinting, SOShipmentEntry>
  {
    public PXSelect<SOShipLineSplitPackage, Where<SOShipLineSplitPackage.shipmentNbr, Equal<Optional<SOPackageDetail.shipmentNbr>>, And<SOShipLineSplitPackage.packageLineNbr, Equal<Optional<SOPackageDetail.lineNbr>>>>> PackageDetailSplit;

    protected virtual void _(PX.Data.Events.RowSelected<SOPackageDetail> e)
    {
      ((PXSelectBase) this.PackageDetailSplit).Cache.AllowInsert = ((PXSelectBase) ((PXGraphExtension<SOShipmentEntry>) this).Base.Packages).AllowInsert && e.Row != null;
      ((PXSelectBase) this.PackageDetailSplit).AllowDelete = ((PXSelectBase) ((PXGraphExtension<SOShipmentEntry>) this).Base.Packages).AllowDelete;
      ((PXSelectBase) this.PackageDetailSplit).AllowSelect = ((PXSelectBase) ((PXGraphExtension<SOShipmentEntry>) this).Base.Packages).AllowSelect;
      ((PXSelectBase) this.PackageDetailSplit).AllowUpdate = ((PXSelectBase) ((PXGraphExtension<SOShipmentEntry>) this).Base.Packages).AllowUpdate;
    }

    protected virtual void _(PX.Data.Events.RowSelected<SOShipment> e)
    {
      if (e.Row == null)
        return;
      PXUIFieldAttribute.SetEnabled<SOShipment.unlimitedPackages>(((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<SOShipment>>) e).Cache, (object) e.Row, ((PXGraph) ((PXGraphExtension<SOShipmentEntry>) this).Base).IsImport);
      Exception exception = (Exception) null;
      if (e.Row.IsPackageContentDeleted.GetValueOrDefault())
        exception = (Exception) new PXSetPropertyException((IBqlTable) e.Row, "The content of the packages has been cleared because the system has recalculated the packages. Specify the content of new packages, if needed.", (PXErrorLevel) 2);
      ((PX.Data.Events.Event<PXRowSelectedEventArgs, PX.Data.Events.RowSelected<SOShipment>>) e).Cache.RaiseExceptionHandling<SOShipment.packageCount>((object) e.Row, (object) null, exception);
    }

    protected virtual void _(PX.Data.Events.RowInserted<SOShipLineSplitPackage> e)
    {
      ((PXSelectBase<SOShipment>) ((PXGraphExtension<SOShipmentEntry>) this).Base.Document).Current.IsPackageContentDeleted = new bool?(false);
      this.UpdateParentShipmentLine(((PX.Data.Events.Event<PXRowInsertedEventArgs, PX.Data.Events.RowInserted<SOShipLineSplitPackage>>) e).Cache, e.Row, (SOShipLineSplitPackage) null);
    }

    protected virtual void _(PX.Data.Events.RowUpdated<SOShipLineSplitPackage> e)
    {
      this.UpdateParentShipmentLine(((PX.Data.Events.Event<PXRowUpdatedEventArgs, PX.Data.Events.RowUpdated<SOShipLineSplitPackage>>) e).Cache, e.Row, e.OldRow);
    }

    protected virtual void _(PX.Data.Events.RowDeleted<SOShipLineSplitPackage> e)
    {
      this.UpdateParentShipmentLine(((PX.Data.Events.Event<PXRowDeletedEventArgs, PX.Data.Events.RowDeleted<SOShipLineSplitPackage>>) e).Cache, (SOShipLineSplitPackage) null, e.Row);
    }

    protected virtual void _(
      PX.Data.Events.FieldUpdated<SOShipLineSplitPackage, SOShipLineSplitPackage.shipmentLineNbr> e)
    {
      if (e.Row == null)
        return;
      SOShipLineSplit soShipLineSplit = PXParentAttribute.SelectParent<SOShipLineSplit>(((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<SOShipLineSplitPackage, SOShipLineSplitPackage.shipmentLineNbr>>) e).Cache, (object) e.Row);
      e.Row.InventoryID = (int?) soShipLineSplit?.InventoryID;
      e.Row.UOM = soShipLineSplit?.UOM;
      SOShipLineSplitPackage row = e.Row;
      Decimal? qty = (Decimal?) soShipLineSplit?.Qty;
      Decimal? packedQty = (Decimal?) soShipLineSplit?.PackedQty;
      Decimal? nullable = qty.HasValue & packedQty.HasValue ? new Decimal?(qty.GetValueOrDefault() - packedQty.GetValueOrDefault()) : new Decimal?();
      row.PackedQty = nullable;
    }

    protected virtual void _(
      PX.Data.Events.FieldVerifying<SOShipLineSplitPackage, SOShipLineSplitPackage.packedQty> e)
    {
      if (e.Row == null || ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<SOShipLineSplitPackage, SOShipLineSplitPackage.packedQty>, SOShipLineSplitPackage, object>) e).NewValue == null || ((PXGraph) ((PXGraphExtension<SOShipmentEntry>) this).Base).IsContractBasedAPI && !FlaggedModeScopeBase<SOShipmentEntry.PackageDetail.RowPersistingScope>.IsActive)
        return;
      Decimal num1 = (Decimal) ((PX.Data.Events.FieldVerifyingBase<PX.Data.Events.FieldVerifying<SOShipLineSplitPackage, SOShipLineSplitPackage.packedQty>, SOShipLineSplitPackage, object>) e).NewValue - e.Row.PackedQty.GetValueOrDefault();
      SOShipLineSplit sosplit = PXParentAttribute.SelectParent<SOShipLineSplit>(((PX.Data.Events.Event<PXFieldVerifyingEventArgs, PX.Data.Events.FieldVerifying<SOShipLineSplitPackage, SOShipLineSplitPackage.packedQty>>) e).Cache, (object) e.Row);
      if (sosplit == null)
        return;
      Decimal? nullable1 = sosplit.PackedQty;
      Decimal num2 = num1;
      Decimal? nullable2 = nullable1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + num2) : new Decimal?();
      nullable1 = sosplit.Qty;
      Decimal qtyThreshold = ((PXGraphExtension<SOShipmentEntry>) this).Base.GetQtyThreshold(sosplit);
      Decimal? nullable3 = nullable1.HasValue ? new Decimal?(nullable1.GetValueOrDefault() * qtyThreshold) : new Decimal?();
      if (nullable2.GetValueOrDefault() > nullable3.GetValueOrDefault() & nullable2.HasValue & nullable3.HasValue)
        throw new PXSetPropertyException((IBqlTable) e.Row, "The quantity packed exceeds the quantity shipped for this line.");
    }

    protected virtual void _(PX.Data.Events.RowPersisting<SOShipLineSplitPackage> e)
    {
      if (!EnumerableExtensions.IsIn<PXDBOperation>(PXDBOperationExt.Command(e.Operation), (PXDBOperation) 2, (PXDBOperation) 1))
        return;
      using (new SOShipmentEntry.PackageDetail.RowPersistingScope())
        ((PX.Data.Events.Event<PXRowPersistingEventArgs, PX.Data.Events.RowPersisting<SOShipLineSplitPackage>>) e).Cache.VerifyFieldAndRaiseException<SOShipLineSplitPackage.packedQty>((object) e.Row);
    }

    protected void UpdateParentShipmentLine(
      PXCache sender,
      SOShipLineSplitPackage row,
      SOShipLineSplitPackage oldRow)
    {
      if (row != null && oldRow != null)
      {
        int? nullable = row.ShipmentLineNbr;
        int? shipmentLineNbr = oldRow.ShipmentLineNbr;
        if (nullable.GetValueOrDefault() == shipmentLineNbr.GetValueOrDefault() & nullable.HasValue == shipmentLineNbr.HasValue)
        {
          SOShipLineSplit shipmentLineSplit1 = PXParentAttribute.SelectParent<SOShipLineSplit>(sender, (object) row);
          if (shipmentLineSplit1 != null)
          {
            int? shipmentSplitLineNbr = row.ShipmentSplitLineNbr;
            nullable = oldRow.ShipmentSplitLineNbr;
            if (shipmentSplitLineNbr.GetValueOrDefault() == nullable.GetValueOrDefault() & shipmentSplitLineNbr.HasValue == nullable.HasValue)
            {
              this.UpdateShipmentLine(sender, shipmentLineSplit1, row, row.PackedQty.GetValueOrDefault() - oldRow.PackedQty.GetValueOrDefault());
              goto label_17;
            }
            this.UpdateShipmentLine(sender, shipmentLineSplit1, row, row.PackedQty.GetValueOrDefault());
            SOShipLineSplit shipmentLineSplit2 = PXParentAttribute.SelectParent<SOShipLineSplit>(sender, (object) oldRow);
            if (shipmentLineSplit2 != null)
            {
              this.UpdateShipmentLine(sender, shipmentLineSplit2, oldRow, -oldRow.PackedQty.GetValueOrDefault());
              goto label_17;
            }
            goto label_17;
          }
          goto label_17;
        }
      }
      Decimal? nullable1;
      if (row != null)
      {
        SOShipLineSplit soShipLineSplit = PXParentAttribute.SelectParent<SOShipLineSplit>(sender, (object) row);
        if (soShipLineSplit != null)
        {
          SOShipLine soShipLine = PXParentAttribute.SelectParent<SOShipLine>(((PXSelectBase) ((PXGraphExtension<SOShipmentEntry>) this).Base.splits).Cache, (object) soShipLineSplit);
          PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) ((PXGraphExtension<SOShipmentEntry>) this).Base, soShipLine.InventoryID);
          Decimal valueOrDefault1 = soShipLine.UnitPrice.GetValueOrDefault();
          Decimal num1 = 1M;
          bool? nullable2 = inventoryItem.StkItem;
          Decimal num2;
          if (!nullable2.GetValueOrDefault())
          {
            nullable2 = inventoryItem.KitItem;
            if (nullable2.GetValueOrDefault())
            {
              Decimal kitComponentsCount = this.GetNonStockKitComponentsCount(soShipLine, inventoryItem);
              num1 = kitComponentsCount != 0M ? kitComponentsCount : 1M;
              num2 = INUnitAttribute.ConvertFromBase<SOShipLine.inventoryID>(((PXSelectBase) ((PXGraphExtension<SOShipmentEntry>) this).Base.Transactions).Cache, (object) soShipLine, soShipLine.UOM, valueOrDefault1, INPrecision.NOROUND);
              goto label_13;
            }
          }
          num2 = INUnitAttribute.ConvertFromTo<SOShipLine.inventoryID>(((PXSelectBase) ((PXGraphExtension<SOShipmentEntry>) this).Base.Transactions).Cache, (object) soShipLine, soShipLineSplit.UOM, soShipLine.UOM, valueOrDefault1, INPrecision.NOROUND);
label_13:
          SOShipLineSplitPackage lineSplitPackage = row;
          Decimal num3 = num2;
          nullable1 = soShipLine.DiscPct;
          Decimal num4 = 1M - nullable1.GetValueOrDefault() / 100M;
          Decimal? nullable3 = new Decimal?(PXDBPriceCostAttribute.Round(num3 * num4 / num1));
          lineSplitPackage.UnitPriceFactor = nullable3;
          row.WeightFactor = new Decimal?(num1);
          PXCache sender1 = sender;
          SOShipLineSplit shipmentLineSplit = soShipLineSplit;
          SOShipLineSplitPackage packageDetailSplit = row;
          nullable1 = row.PackedQty;
          Decimal valueOrDefault2 = nullable1.GetValueOrDefault();
          this.UpdateShipmentLine(sender1, shipmentLineSplit, packageDetailSplit, valueOrDefault2);
        }
      }
      if (oldRow != null)
      {
        SOShipLineSplit soShipLineSplit = PXParentAttribute.SelectParent<SOShipLineSplit>(sender, (object) oldRow);
        if (soShipLineSplit != null)
        {
          PXCache sender2 = sender;
          SOShipLineSplit shipmentLineSplit = soShipLineSplit;
          SOShipLineSplitPackage packageDetailSplit = row;
          nullable1 = oldRow.PackedQty;
          Decimal adjustment = -nullable1.GetValueOrDefault();
          this.UpdateShipmentLine(sender2, shipmentLineSplit, packageDetailSplit, adjustment);
        }
      }
label_17:
      if (row == null || oldRow == null)
        return;
      nullable1 = row.PackedQty;
      Decimal? packedQty = oldRow.PackedQty;
      if (nullable1.GetValueOrDefault() == packedQty.GetValueOrDefault() & nullable1.HasValue == packedQty.HasValue)
        return;
      ((PXGraphExtension<SOShipmentEntry>) this).Base.ResetFreightCostIsValid(((PXSelectBase<SOShipment>) ((PXGraphExtension<SOShipmentEntry>) this).Base.Document).Current);
    }

    protected virtual Decimal GetNonStockKitComponentsCount(
      SOShipLine shipmentLine,
      PX.Objects.IN.InventoryItem item)
    {
      return shipmentLine.BaseShippedQty.GetValueOrDefault() == 0M ? 0M : ((IEnumerable<object>) PXParentAttribute.SelectChildren(((PXSelectBase) ((PXGraphExtension<SOShipmentEntry>) this).Base.splits).Cache, (object) shipmentLine, typeof (SOShipLine))).Sum<object>((Func<object, Decimal>) (s => ((SOShipLineSplit) s).Qty.GetValueOrDefault())) / shipmentLine.BaseShippedQty.Value;
    }

    protected void UpdateShipmentLine(
      PXCache sender,
      SOShipLineSplit shipmentLineSplit,
      SOShipLineSplitPackage packageDetailSplit,
      Decimal adjustment)
    {
      if (!(adjustment != 0M))
        return;
      int num;
      if (!(adjustment > 0M) || !(shipmentLineSplit.PackedQty.GetValueOrDefault() + adjustment > shipmentLineSplit.PickedQty.GetValueOrDefault()))
      {
        if (adjustment < 0M)
        {
          if (PXAccess.FeatureInstalled<FeaturesSet.wMSFulfillment>())
          {
            SOPickPackShipSetup pickPackShipSetup = SOPickPackShipSetup.PK.Find((PXGraph) ((PXGraphExtension<SOShipmentEntry>) this).Base, ((PXGraph) ((PXGraphExtension<SOShipmentEntry>) this).Base).Accessinfo.BranchID);
            if (pickPackShipSetup == null)
            {
              num = 0;
            }
            else
            {
              bool? showPickTab = pickPackShipSetup.ShowPickTab;
              bool flag = false;
              num = showPickTab.GetValueOrDefault() == flag & showPickTab.HasValue ? 1 : 0;
            }
          }
          else
            num = 1;
        }
        else
          num = 0;
      }
      else
        num = 1;
      bool syncPickedWithPacked = num != 0;
      UpdatePickPackInfoOf<SOShipLineSplit, SOShipLineSplit.pickedQty, SOShipLineSplit.packedQty>(shipmentLineSplit, (Func<SOShipLineSplit, Decimal?>) (split => new Decimal?(split.PackedQty.GetValueOrDefault() + adjustment)), syncPickedWithPacked, true);
      UpdatePickPackInfoOf<SOShipLine, SOShipLine.pickedQty, SOShipLine.packedQty>(PXParentAttribute.SelectParent<SOShipLine>(((PXSelectBase) ((PXGraphExtension<SOShipmentEntry>) this).Base.splits).Cache, (object) shipmentLineSplit), (Func<SOShipLine, Decimal?>) (line =>
      {
        SOShipLineSplit[] source = PXParentAttribute.SelectChildren<SOShipLineSplit, SOShipLine>(((PXSelectBase) ((PXGraphExtension<SOShipmentEntry>) this).Base.splits).Cache, line);
        NonStockKitSpecHelper stockKitSpecHelper = new NonStockKitSpecHelper((PXGraph) ((PXGraphExtension<SOShipmentEntry>) this).Base);
        if (!stockKitSpecHelper.IsNonStockKit(line.InventoryID))
          return new Decimal?(INUnitAttribute.ConvertFromBase(((PXSelectBase) ((PXGraphExtension<SOShipmentEntry>) this).Base.Transactions).Cache, line.InventoryID, line.UOM, ((IEnumerable<SOShipLineSplit>) source).Sum<SOShipLineSplit>((Func<SOShipLineSplit, Decimal>) (s => s.PackedQty.GetValueOrDefault())), INPrecision.NOROUND));
        Func<int, bool> RequireShipping = Func.Memorize<int, bool>((Func<int, bool>) (inventoryID => PX.Objects.IN.InventoryItem.PK.Find((PXGraph) ((PXGraphExtension<SOShipmentEntry>) this).Base, new int?(inventoryID)).With<PX.Objects.IN.InventoryItem, bool>((Func<PX.Objects.IN.InventoryItem, bool>) (item => item.StkItem.GetValueOrDefault() || item.NonStockShip.GetValueOrDefault()))));
        Dictionary<int, Decimal> dictionary1 = EnumerableExtensions.ToDictionary<int, Decimal>(stockKitSpecHelper.GetNonStockKitSpec(line.InventoryID.Value).Where<KeyValuePair<int, Decimal>>((Func<KeyValuePair<int, Decimal>, bool>) (pair => RequireShipping(pair.Key))));
        Dictionary<int, Decimal> dictionary2 = ((IEnumerable<SOShipLineSplit>) source).GroupBy<SOShipLineSplit, int>((Func<SOShipLineSplit, int>) (r => r.InventoryID.Value)).ToDictionary<IGrouping<int, SOShipLineSplit>, int, Decimal>((Func<IGrouping<int, SOShipLineSplit>, int>) (g => g.Key), (Func<IGrouping<int, SOShipLineSplit>, Decimal>) (g => g.Sum<SOShipLineSplit>((Func<SOShipLineSplit, Decimal>) (s => s.PackedQty.GetValueOrDefault()))));
        return new Decimal?(dictionary1.Keys.Count<int>() == 0 || dictionary1.Keys.Except<int>((IEnumerable<int>) dictionary2.Keys).Count<int>() > 0 ? 0M : dictionary2.Join<KeyValuePair<int, Decimal>, KeyValuePair<int, Decimal>, int, Decimal>((IEnumerable<KeyValuePair<int, Decimal>>) dictionary1, (Func<KeyValuePair<int, Decimal>, int>) (split => split.Key), (Func<KeyValuePair<int, Decimal>, int>) (spec => spec.Key), (Func<KeyValuePair<int, Decimal>, KeyValuePair<int, Decimal>, Decimal>) ((split, spec) =>
        {
          KeyValuePair<int, Decimal> keyValuePair = split;
          Decimal d1 = keyValuePair.Value;
          keyValuePair = spec;
          Decimal d2 = keyValuePair.Value;
          return Math.Floor(Decimal.Divide(d1, d2));
        })).Min());
      }), syncPickedWithPacked);
      UpdatePickPackInfoOf<SOShipment, SOShipment.pickedQty, SOShipment.packedQty>(PXParentAttribute.SelectParent<SOShipment>(((PXSelectBase) ((PXGraphExtension<SOShipmentEntry>) this).Base.splits).Cache, (object) shipmentLineSplit), (Func<SOShipment, Decimal?>) (shipment => new Decimal?(((IEnumerable<SOShipLine>) PXParentAttribute.SelectChildren<SOShipLine, SOShipment>(((PXSelectBase) ((PXGraphExtension<SOShipmentEntry>) this).Base.Transactions).Cache, shipment)).Sum<SOShipLine>((Func<SOShipLine, Decimal>) (l => l.PackedQty.GetValueOrDefault())))), syncPickedWithPacked);

      void UpdatePickPackInfoOf<TEntity, TPickedQtyField, TPackedQtyField>(
        TEntity row,
        Func<TEntity, Decimal?> calculateNewPackedQty,
        bool syncPickedWithPacked,
        bool raiseRowUpdated = false)
        where TEntity : PXBqlTable, IBqlTable, new()
        where TPickedQtyField : IBqlField, IImplement<IBqlDecimal>
        where TPackedQtyField : IBqlField, IImplement<IBqlDecimal>
      {
        PXCache<TEntity> pxCache = GraphHelper.Caches<TEntity>((PXGraph) ((PXGraphExtension<SOShipmentEntry>) this).Base);
        TEntity copy = pxCache.Rows.CreateCopy(row);
        GraphHelper.MarkUpdated((PXCache) pxCache, (object) row, true);
        Decimal? nullable1 = (Decimal?) ((PXCache) pxCache).GetValue<TPackedQtyField>((object) row);
        ((PXCache) pxCache).SetValue<TPackedQtyField>((object) row, (object) calculateNewPackedQty(row));
        ((PXCache) pxCache).RaiseFieldUpdated<TPackedQtyField>((object) row, (object) nullable1);
        if (syncPickedWithPacked)
        {
          Decimal? nullable2 = (Decimal?) ((PXCache) pxCache).GetValue<TPickedQtyField>((object) row);
          ((PXCache) pxCache).SetValue<TPickedQtyField>((object) row, ((PXCache) pxCache).GetValue<TPackedQtyField>((object) row));
          ((PXCache) pxCache).RaiseFieldUpdated<TPickedQtyField>((object) row, (object) nullable2);
        }
        if (!raiseRowUpdated)
          return;
        pxCache.RaiseRowUpdated(row, copy);
      }
    }

    [PXOverride]
    public virtual void ShipPackages(SOShipment shiporder, Action<SOShipment> baseMethod)
    {
      PX.Objects.CS.Carrier carrier = PX.Objects.CS.Carrier.PK.Find((PXGraph) ((PXGraphExtension<SOShipmentEntry>) this).Base, shiporder.ShipVia);
      if (carrier != null)
      {
        if (carrier.ValidatePackedQty.GetValueOrDefault())
          this.ValidatePackagedQuantities(shiporder);
        if ((!carrier.IsExternal.GetValueOrDefault() || shiporder.ShippedViaCarrier.GetValueOrDefault() || !carrier.ReturnLabel.GetValueOrDefault() || !(shiporder.Operation == "I") ? 0 : (!shiporder.UnlimitedPackages.GetValueOrDefault() ? 1 : 0)) != 0)
          this.Base1.GetReturnLabels(shiporder);
      }
      baseMethod(shiporder);
    }

    protected virtual void ValidatePackagedQuantities(SOShipment shiporder)
    {
      ((PXSelectBase<SOShipment>) ((PXGraphExtension<SOShipmentEntry>) this).Base.Document).Current = PXResultset<SOShipment>.op_Implicit(((PXSelectBase<SOShipment>) ((PXGraphExtension<SOShipmentEntry>) this).Base.Document).Search<SOShipment.shipmentNbr>((object) shiporder.ShipmentNbr, Array.Empty<object>()));
      if (!(((PXSelectBase<SOShipment>) ((PXGraphExtension<SOShipmentEntry>) this).Base.Document).Current.ShipmentType == "I"))
        return;
      foreach (PXResult<SOShipLine> pxResult1 in ((PXSelectBase<SOShipLine>) ((PXGraphExtension<SOShipmentEntry>) this).Base.Transactions).Select(Array.Empty<object>()))
      {
        SOShipLine soShipLine = PXResult<SOShipLine>.op_Implicit(pxResult1);
        ((PXSelectBase<SOShipLine>) ((PXGraphExtension<SOShipmentEntry>) this).Base.Transactions).Current = PXResultset<SOShipLine>.op_Implicit(((PXSelectBase<SOShipLine>) ((PXGraphExtension<SOShipmentEntry>) this).Base.Transactions).Search<SOShipLine.shipmentNbr, SOShipLine.lineNbr>((object) soShipLine.ShipmentNbr, (object) soShipLine.LineNbr, Array.Empty<object>()));
        if (soShipLine.LineType == "GI")
        {
          Decimal? nullable1 = soShipLine.BaseShippedQty;
          Decimal? nullable2 = soShipLine.BasePackedQty;
          if (!(nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue))
          {
            PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) ((PXGraphExtension<SOShipmentEntry>) this).Base, soShipLine.InventoryID);
            if (inventoryItem.StkItem.GetValueOrDefault() || !inventoryItem.KitItem.GetValueOrDefault())
              throw new PXException("One or more lines for item '{0}' have quantities that have not been packed.", new object[1]
              {
                (object) inventoryItem?.InventoryCD.Trim()
              });
          }
          foreach (PXResult<SOShipLineSplit> pxResult2 in ((PXSelectBase<SOShipLineSplit>) ((PXGraphExtension<SOShipmentEntry>) this).Base.splits).Select(Array.Empty<object>()))
          {
            SOShipLineSplit soShipLineSplit = PXResult<SOShipLineSplit>.op_Implicit(pxResult2);
            nullable2 = soShipLineSplit.BaseQty;
            nullable1 = soShipLineSplit.BasePackedQty;
            if (!(nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue))
              throw new PXException("One or more lines for item '{0}' have quantities that have not been packed.", new object[1]
              {
                (object) PX.Objects.IN.InventoryItem.PK.Find((PXGraph) ((PXGraphExtension<SOShipmentEntry>) this).Base, soShipLine.InventoryID)?.InventoryCD.Trim()
              });
          }
        }
      }
    }

    public virtual void OnBeforeRecalculatePackages(PX.Objects.SO.GraphExtensions.CarrierRates.Document doc)
    {
      ((PXSelectBase<SOShipment>) ((PXGraphExtension<SOShipmentEntry>) this).Base.Document).Current.IsPackageContentDeleted = new bool?(false);
    }

    public virtual void OnAutoPackageContentDeleted(SOShipLineSplitPackage row)
    {
      ((PXSelectBase<SOShipment>) ((PXGraphExtension<SOShipmentEntry>) this).Base.Document).Current.IsPackageContentDeleted = new bool?(true);
    }

    private class RowPersistingScope : 
      FlaggedModeScopeBase<SOShipmentEntry.PackageDetail.RowPersistingScope>
    {
    }
  }

  public class CarrierRates : CarrierRatesExtension<SOShipmentEntry, SOShipment>
  {
    protected override CarrierRatesExtension<SOShipmentEntry, SOShipment>.DocumentMapping GetDocumentMapping()
    {
      return new CarrierRatesExtension<SOShipmentEntry, SOShipment>.DocumentMapping(typeof (SOShipment))
      {
        DocumentDate = typeof (SOShipment.shipDate)
      };
    }

    protected override CarrierRatesExtension<SOShipmentEntry, SOShipment>.DocumentPackageMapping GetDocumentPackageMapping()
    {
      return new CarrierRatesExtension<SOShipmentEntry, SOShipment>.DocumentPackageMapping(typeof (SOPackageDetailEx));
    }

    protected override void CalculateFreightCost(PX.Objects.SO.GraphExtensions.CarrierRates.Document doc)
    {
      this.Base.CalculateFreightCost(true);
    }

    protected override void UpdatePackageWeightFromScale(Decimal? weight)
    {
      ((PXSelectBase<SOPackageDetailEx>) this.Base.Packages).Current.Weight = weight;
      ((PXSelectBase<SOPackageDetailEx>) this.Base.Packages).Update(((PXSelectBase<SOPackageDetailEx>) this.Base.Packages).Current);
    }

    public virtual CarrierRequest BuildRateRequest(SOShipment order)
    {
      return this.BuildRateRequest(((PXSelectBase) this.Documents).Cache.GetExtension<PX.Objects.SO.GraphExtensions.CarrierRates.Document>((object) order));
    }

    protected override CarrierRequest GetCarrierRequest(
      PX.Objects.SO.GraphExtensions.CarrierRates.Document doc,
      UnitsType unit,
      List<string> methods,
      List<CarrierBoxEx> boxes)
    {
      SOShipment main = (SOShipment) ((PXSelectBase) this.Documents).Cache.GetMain<PX.Objects.SO.GraphExtensions.CarrierRates.Document>(doc);
      SOShipmentAddress soShipmentAddress = PXResultset<SOShipmentAddress>.op_Implicit(((PXSelectBase<SOShipmentAddress>) this.Base.Shipping_Address).Select(Array.Empty<object>()));
      PX.Objects.CR.BAccount baccount = (PX.Objects.CR.BAccount) PXResultset<BAccountR>.op_Implicit(PXSelectBase<BAccountR, PXSelectJoin<BAccountR, InnerJoin<PX.Objects.GL.Branch, On<PX.Objects.GL.Branch.bAccountID, Equal<BAccountR.bAccountID>>>, Where<PX.Objects.GL.Branch.branchID, Equal<Required<PX.Objects.GL.Branch.branchID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
      {
        (object) ((PXGraph) this.Base).Accessinfo.BranchID
      }));
      PX.Objects.CR.Address address = PXResultset<PX.Objects.CR.Address>.op_Implicit(PXSelectBase<PX.Objects.CR.Address, PXSelect<PX.Objects.CR.Address, Where<PX.Objects.CR.Address.addressID, Equal<Required<PX.Objects.CR.Address.addressID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
      {
        (object) baccount.DefAddressID
      }));
      SOShipmentContact soShipmentContact = PXResultset<SOShipmentContact>.op_Implicit(((PXSelectBase<SOShipmentContact>) this.Base.Shipping_Contact).Select(Array.Empty<object>()));
      PX.Objects.CR.Contact contact = PXResultset<PX.Objects.CR.Contact>.op_Implicit(PXSelectBase<PX.Objects.CR.Contact, PXSelect<PX.Objects.CR.Contact, Where<PX.Objects.CR.Contact.contactID, Equal<Required<PX.Objects.CR.Contact.contactID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
      {
        (object) baccount.DefContactID
      }));
      CarrierRequest carrierRequest = new CarrierRequest(unit, main.CuryID);
      carrierRequest.Shipper = (IAddressBase) address;
      carrierRequest.Origin = (IAddressBase) null;
      carrierRequest.Destination = (IAddressBase) soShipmentAddress;
      carrierRequest.PackagesEx = (IList<CarrierBoxEx>) boxes;
      carrierRequest.Resedential = main.Resedential.GetValueOrDefault();
      carrierRequest.SaturdayDelivery = main.SaturdayDelivery.GetValueOrDefault();
      carrierRequest.Insurance = main.Insurance.GetValueOrDefault();
      carrierRequest.ShipDate = Tools.Max<DateTime>(((PXGraph) this.Base).Accessinfo.BusinessDate.Value.Date, main.ShipDate.Value);
      carrierRequest.Methods = (IList<string>) methods;
      carrierRequest.Attributes = (IList<string>) new List<string>();
      carrierRequest.InvoiceLineTotal = ((PXSelectBase<SOShipment>) this.Base.Document).Current.LineTotal.GetValueOrDefault();
      carrierRequest.ShipperContact = (IContactBase) contact;
      carrierRequest.DestinationContact = (IContactBase) soShipmentContact;
      if (main.GroundCollect.GetValueOrDefault() && this.Base.CanUseGroundCollect(main))
        carrierRequest.Attributes.Add("COLLECT");
      return carrierRequest;
    }

    protected override IEnumerable<Tuple<CarrierRatesExtension<SOShipmentEntry, SOShipment>.ILineInfo, PX.Objects.IN.InventoryItem>> GetLines(
      PX.Objects.SO.GraphExtensions.CarrierRates.Document doc)
    {
      return ((IEnumerable<PXResult<SOShipLine>>) PXSelectBase<SOShipLine, PXSelectJoin<SOShipLine, InnerJoin<PX.Objects.IN.InventoryItem, On<SOShipLine.FK.InventoryItem>>, Where<KeysRelation<CompositeKey<Field<SOShipLine.shipmentType>.IsRelatedTo<SOShipment.shipmentType>, Field<SOShipLine.shipmentNbr>.IsRelatedTo<SOShipment.shipmentNbr>>.WithTablesOf<SOShipment, SOShipLine>, SOShipment, SOShipLine>.SameAsCurrent>, OrderBy<Asc<SOShipLine.shipmentType, Asc<SOShipLine.shipmentNbr, Asc<SOShipLine.lineNbr>>>>>.Config>.SelectMultiBound((PXGraph) this.Base, new object[1]
      {
        (object) (SOShipment) ((PXSelectBase) this.Documents).Cache.GetMain<PX.Objects.SO.GraphExtensions.CarrierRates.Document>(doc)
      }, Array.Empty<object>())).AsEnumerable<PXResult<SOShipLine>>().Cast<PXResult<SOShipLine, PX.Objects.IN.InventoryItem>>().Select<PXResult<SOShipLine, PX.Objects.IN.InventoryItem>, Tuple<CarrierRatesExtension<SOShipmentEntry, SOShipment>.ILineInfo, PX.Objects.IN.InventoryItem>>((Func<PXResult<SOShipLine, PX.Objects.IN.InventoryItem>, Tuple<CarrierRatesExtension<SOShipmentEntry, SOShipment>.ILineInfo, PX.Objects.IN.InventoryItem>>) (r => Tuple.Create<CarrierRatesExtension<SOShipmentEntry, SOShipment>.ILineInfo, PX.Objects.IN.InventoryItem>((CarrierRatesExtension<SOShipmentEntry, SOShipment>.ILineInfo) new SOShipmentEntry.CarrierRates.LineInfo(PXResult<SOShipLine, PX.Objects.IN.InventoryItem>.op_Implicit(r)), PXResult<SOShipLine, PX.Objects.IN.InventoryItem>.op_Implicit(r))));
    }

    protected override IList<SOPackageEngine.PackSet> GetPackages(PX.Objects.SO.GraphExtensions.CarrierRates.Document doc, bool suppressRecalc = false)
    {
      SOShipment main = (SOShipment) ((PXSelectBase) this.Documents).Cache.GetMain<PX.Objects.SO.GraphExtensions.CarrierRates.Document>(doc);
      SOPackageEngine.PackSet packSet = new SOPackageEngine.PackSet(main.SiteID.Value);
      PXView view = ((PXSelectBase) this.Base.Packages).View;
      object[] objArray1 = new object[1]{ (object) main };
      object[] objArray2 = Array.Empty<object>();
      foreach (SOPackageDetailEx soPackageDetailEx in view.SelectMultiBound(objArray1, objArray2))
        packSet.Packages.Add(soPackageDetailEx.ToPackageInfo(new int?(main.SiteID.Value)));
      return (IList<SOPackageEngine.PackSet>) EnumerableExtensions.AsSingleEnumerable<SOPackageEngine.PackSet>(packSet).ToList<SOPackageEngine.PackSet>();
    }

    protected override void ClearPackages(PX.Objects.SO.GraphExtensions.CarrierRates.Document doc)
    {
      foreach (SOPackageDetailEx soPackageDetailEx in ((PXSelectBase) this.Base.Packages).View.SelectMultiBound(new object[1]
      {
        ((PXSelectBase) this.Documents).Cache.GetMain<PX.Objects.SO.GraphExtensions.CarrierRates.Document>(doc)
      }, Array.Empty<object>()))
        ((PXSelectBase<SOPackageDetailEx>) this.Base.Packages).Delete(soPackageDetailEx);
    }

    protected override void InsertPackages(IEnumerable<SOPackageInfoEx> packages)
    {
      foreach (SOPackageInfoEx package in packages)
        ((PXSelectBase<SOPackageDetailEx>) this.Base.Packages).Insert(package.ToPackageDetail("A").Apply<SOPackageDetailEx>((Action<SOPackageDetailEx>) (d => d.ShipmentNbr = ((PXSelectBase<SOShipment>) this.Base.Document).Current.ShipmentNbr)));
    }

    protected override void RecalculatePackagesForOrder(PX.Objects.SO.GraphExtensions.CarrierRates.Document doc)
    {
      if (((PXSelectBase<SOShipment>) this.Base.Document).Current == null)
        return;
      bool? nullable = ((PXSelectBase<SOShipment>) this.Base.Document).Current.UnlimitedPackages;
      if (!nullable.GetValueOrDefault())
      {
        nullable = ((PXSelectBase<SOShipment>) this.Base.Document).Current.Released;
        if (!nullable.GetValueOrDefault())
        {
          nullable = ((PXSelectBase<SOShipment>) this.Base.Document).Current.Confirmed;
          if (!nullable.GetValueOrDefault())
          {
            if (!((PXSelectBase<SOShipment>) this.Base.Document).Current.SiteID.HasValue)
              throw new PXException("The Warehouse ID must be specified before packages can be recalculated.");
            this.Base.PackageDetailExt.OnBeforeRecalculatePackages(doc);
            // ISSUE: method pointer
            PXRowDeleted pxRowDeleted = new PXRowDeleted((object) this, __methodptr(\u003CRecalculatePackagesForOrder\u003Eb__10_0));
            int num1 = 0;
            Decimal num2 = 0M;
            SOPackageEngine.PackSet manualPackSet;
            IList<SOPackageEngine.PackSet> packages = this.CalculatePackages(((PXSelectBase<SOShipment>) this.Base.Document).Current, out manualPackSet);
            try
            {
              ((PXGraph) this.Base).RowDeleted.AddHandler<SOShipLineSplitPackage>(pxRowDeleted);
              foreach (PXResult<SOPackageDetailEx> pxResult in ((PXSelectBase<SOPackageDetailEx>) this.Base.Packages).Select(Array.Empty<object>()))
              {
                SOPackageDetailEx soPackageDetailEx = PXResult<SOPackageDetailEx>.op_Implicit(pxResult);
                if (manualPackSet.Packages.Count == 0 && soPackageDetailEx.PackageType != "A")
                {
                  num2 += soPackageDetailEx.Weight.GetValueOrDefault();
                  ++num1;
                }
                else
                  ((PXSelectBase<SOPackageDetailEx>) this.Base.Packages).Delete(soPackageDetailEx);
              }
            }
            finally
            {
              ((PXGraph) this.Base).RowDeleted.RemoveHandler<SOShipLineSplitPackage>(pxRowDeleted);
            }
            foreach (SOPackageEngine.PackSet packSet in (IEnumerable<SOPackageEngine.PackSet>) packages)
            {
              foreach (SOPackageInfoEx package in packSet.Packages)
              {
                num2 += package.GrossWeight.GetValueOrDefault();
                SOPackageDetailEx soPackageDetailEx = new SOPackageDetailEx();
                soPackageDetailEx.PackageType = "A";
                soPackageDetailEx.ShipmentNbr = ((PXSelectBase<SOShipment>) this.Base.Document).Current.ShipmentNbr;
                soPackageDetailEx.BoxID = package.BoxID;
                soPackageDetailEx.Weight = package.GrossWeight;
                soPackageDetailEx.WeightUOM = package.WeightUOM;
                soPackageDetailEx.Qty = package.Qty;
                soPackageDetailEx.QtyUOM = package.QtyUOM;
                soPackageDetailEx.InventoryID = package.InventoryID;
                soPackageDetailEx.DeclaredValue = package.DeclaredValue;
                ((PXSelectBase<SOPackageDetailEx>) this.Base.Packages).Insert(soPackageDetailEx).Confirmed = new bool?(false);
                ++num1;
              }
            }
            foreach (SOPackageInfoEx package in manualPackSet.Packages)
            {
              num2 += package.GrossWeight.GetValueOrDefault();
              SOPackageDetailEx soPackageDetailEx = new SOPackageDetailEx();
              soPackageDetailEx.PackageType = "M";
              soPackageDetailEx.ShipmentNbr = ((PXSelectBase<SOShipment>) this.Base.Document).Current.ShipmentNbr;
              soPackageDetailEx.BoxID = package.BoxID;
              soPackageDetailEx.Weight = package.GrossWeight;
              soPackageDetailEx.WeightUOM = package.WeightUOM;
              soPackageDetailEx.Qty = package.Qty;
              soPackageDetailEx.QtyUOM = package.QtyUOM;
              soPackageDetailEx.InventoryID = package.InventoryID;
              soPackageDetailEx.DeclaredValue = package.DeclaredValue;
              soPackageDetailEx.Height = package.Height;
              soPackageDetailEx.Width = package.Width;
              soPackageDetailEx.Length = package.Length;
              ((PXSelectBase<SOPackageDetailEx>) this.Base.Packages).Insert(soPackageDetailEx).Confirmed = new bool?(false);
              ++num1;
            }
            ((PXSelectBase<SOShipment>) this.Base.Document).Current.IsPackageValid = new bool?(true);
            ((PXSelectBase<SOShipment>) this.Base.Document).Current.RecalcPackagesReason = new int?(0);
            ((PXSelectBase<SOShipment>) this.Base.Document).Current.PackageWeight = new Decimal?(num2);
            ((PXSelectBase<SOShipment>) this.Base.Document).Current.PackageCount = new int?(num1);
            ((PXSelectBase<SOShipment>) this.Base.Document).Update(((PXSelectBase<SOShipment>) this.Base.Document).Current);
            return;
          }
        }
        throw new PXException("Packages cannot be recalculated on a confirmed or released document.");
      }
    }

    protected virtual IList<SOPackageEngine.PackSet> CalculatePackages(
      SOShipment shipment,
      out SOPackageEngine.PackSet manualPackSet)
    {
      SOOrderExtension extension = ((PXGraph) this.Base).GetExtension<SOOrderExtension>();
      Dictionary<string, SOPackageEngine.ItemStats> dictionary = new Dictionary<string, SOPackageEngine.ItemStats>();
      PXSelectBase<SOPackageInfoEx> pxSelectBase = (PXSelectBase<SOPackageInfoEx>) new PXSelect<SOPackageInfoEx, Where<SOPackageInfoEx.orderType, Equal<Required<SOOrder.orderType>>, And<SOPackageInfoEx.orderNbr, Equal<Required<SOOrder.orderNbr>>, And<SOPackageInfoEx.siteID, Equal<Required<SOPackageInfoEx.siteID>>>>>>((PXGraph) this.Base);
      SOPackageEngine.OrderInfo orderInfo = new SOPackageEngine.OrderInfo(shipment.ShipVia);
      manualPackSet = new SOPackageEngine.PackSet(shipment.SiteID.Value);
      List<string> stringList = new List<string>();
      PXView view = ((PXSelectBase) this.Base.Transactions).View;
      object[] objArray1 = new object[1]
      {
        (object) shipment
      };
      object[] objArray2 = Array.Empty<object>();
      foreach (SOShipLine soShipLine in view.SelectMultiBound(objArray1, objArray2))
      {
        SOOrder row = PXParentAttribute.SelectParent<SOOrder>(((PXSelectBase) this.Base.Transactions).Cache, (object) soShipLine);
        bool? nullable1;
        int num1;
        if (PXAccess.FeatureInstalled<FeaturesSet.autoPackaging>())
        {
          if (row != null)
          {
            nullable1 = row.IsManualPackage;
            if (nullable1.GetValueOrDefault())
              goto label_11;
          }
          PXSelectJoin<SOShipment, LeftJoinSingleTable<PX.Objects.AR.Customer, On<PX.Objects.AR.Customer.bAccountID, Equal<SOShipment.customerID>>, LeftJoin<PX.Objects.IN.INSite, On<SOShipment.FK.Site>>>, Where2<Where<PX.Objects.AR.Customer.bAccountID, IsNull, Or<Match<PX.Objects.AR.Customer, Current<AccessInfo.userName>>>>, And<Where<SOShipment.siteID, IsNull, Or<PX.Objects.IN.INSite.branchID, IsNotNull, And<Match<PX.Objects.IN.INSite, Current<AccessInfo.userName>>>>>>>> document = this.Base.Document;
          if (document == null)
          {
            num1 = 0;
            goto label_12;
          }
          SOShipment current = ((PXSelectBase<SOShipment>) document).Current;
          bool? nullable2;
          if (current == null)
          {
            nullable1 = new bool?();
            nullable2 = nullable1;
          }
          else
            nullable2 = current.UnlimitedPackages;
          nullable1 = nullable2;
          num1 = nullable1.GetValueOrDefault() ? 1 : 0;
          goto label_12;
        }
label_11:
        num1 = 1;
label_12:
        if (num1 != 0)
        {
          string str = $"{row.OrderType}.{row.OrderNbr}.{shipment.SiteID}";
          if (!stringList.Contains(str))
          {
            foreach (PXResult<SOPackageInfoEx> pxResult in pxSelectBase.Select(new object[3]
            {
              (object) row.OrderType,
              (object) row.OrderNbr,
              (object) shipment.SiteID
            }))
            {
              SOPackageInfoEx soPackageInfoEx = PXResult<SOPackageInfoEx>.op_Implicit(pxResult);
              Decimal baseval;
              PXDBCurrencyAttribute.CuryConvBase<SOOrder.curyInfoID>(((PXSelectBase) extension.soorder).Cache, (object) row, soPackageInfoEx.DeclaredValue.GetValueOrDefault(), out baseval);
              soPackageInfoEx.DeclaredValue = new Decimal?(baseval);
              manualPackSet.Packages.Add(soPackageInfoEx);
            }
            stringList.Add(str);
          }
        }
        else
        {
          PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this.Base, soShipLine.InventoryID);
          if (!(inventoryItem.PackageOption == "N"))
          {
            orderInfo.AddLine(inventoryItem, soShipLine.BaseQty);
            nullable1 = inventoryItem.PackSeparately;
            int num2 = nullable1.GetValueOrDefault() ? inventoryItem.InventoryID.Value : 0;
            string key = $"{soShipLine.SiteID}.{num2}.{inventoryItem.PackageOption}.{soShipLine.Operation}";
            if (dictionary.ContainsKey(key))
            {
              SOPackageEngine.ItemStats itemStats = dictionary[key];
              itemStats.BaseQty += soShipLine.BaseQty.GetValueOrDefault();
              itemStats.BaseWeight += soShipLine.ExtWeight.GetValueOrDefault();
              itemStats.DeclaredValue += soShipLine.LineAmt.GetValueOrDefault();
              itemStats.AddLine(inventoryItem, soShipLine.BaseQty);
            }
            else
            {
              SOPackageEngine.ItemStats itemStats1 = new SOPackageEngine.ItemStats();
              itemStats1.SiteID = soShipLine.SiteID;
              itemStats1.InventoryID = new int?(num2);
              itemStats1.Operation = soShipLine.Operation;
              itemStats1.PackOption = inventoryItem.PackageOption;
              SOPackageEngine.ItemStats itemStats2 = itemStats1;
              Decimal baseQty = itemStats2.BaseQty;
              Decimal? nullable3 = soShipLine.BaseQty;
              Decimal valueOrDefault1 = nullable3.GetValueOrDefault();
              itemStats2.BaseQty = baseQty + valueOrDefault1;
              SOPackageEngine.ItemStats itemStats3 = itemStats1;
              Decimal baseWeight = itemStats3.BaseWeight;
              nullable3 = soShipLine.ExtWeight;
              Decimal valueOrDefault2 = nullable3.GetValueOrDefault();
              itemStats3.BaseWeight = baseWeight + valueOrDefault2;
              itemStats1.DeclaredValue += soShipLine.LineAmt.GetValueOrDefault();
              itemStats1.AddLine(inventoryItem, soShipLine.BaseQty);
              dictionary.Add(key, itemStats1);
            }
          }
        }
      }
      orderInfo.Stats.AddRange((IEnumerable<SOPackageEngine.ItemStats>) dictionary.Values);
      return this.CreatePackageEngine().Pack(orderInfo);
    }

    protected virtual IList<CarrierBox> GetPackages(
      SOShipment shiporder,
      PX.Objects.CS.Carrier carrier,
      CarrierPlugin plugin)
    {
      List<CarrierBox> packages = new List<CarrierBox>();
      List<SOPackageDetailEx> list = GraphHelper.RowCast<SOPackageDetailEx>((IEnumerable) PXSelectBase<SOPackageDetailEx, PXSelect<SOPackageDetailEx, Where<SOPackageDetailEx.shipmentNbr, Equal<Required<SOShipment.shipmentNbr>>>>.Config>.Select((PXGraph) this.Base, new object[1]
      {
        (object) shiporder.ShipmentNbr
      })).ToList<SOPackageDetailEx>();
      bool flag = false;
      foreach (SOCarrierPackageDetailEx pkgDetail in this.GetCarrierPackageDetail(list, carrier.CarrierID))
      {
        SOPackageDetailEx package = pkgDetail.Package;
        bool? nullable = carrier.ConfirmationRequired;
        if (nullable.GetValueOrDefault())
        {
          nullable = package.Confirmed;
          if (!nullable.GetValueOrDefault())
          {
            flag = true;
            ((PXSelectBase) this.Base.Packages).Cache.RaiseExceptionHandling<SOPackageDetail.confirmed>((object) package, (object) package.Confirmed, (Exception) new PXSetPropertyException("Confirmation for each and every Package is required. Please confirm and retry.", (PXErrorLevel) 4));
          }
        }
        packages.Add(this.BuildCarrierPackage(pkgDetail, plugin));
      }
      if (flag)
        throw new PXException("Confirmation for each and every Package is required. Please confirm and retry.");
      return (IList<CarrierBox>) packages;
    }

    public virtual CarrierBox BuildCarrierPackage(
      SOCarrierPackageDetailEx pkgDetail,
      CarrierPlugin plugin)
    {
      SOPackageDetailEx package = pkgDetail.Package;
      return new CarrierBox(package.LineNbr.Value, this.ConvertWeightValue(package.Weight.GetValueOrDefault(), plugin))
      {
        Description = package.Description,
        DeclaredValue = package.DeclaredValue.GetValueOrDefault(),
        COD = package.COD.GetValueOrDefault(),
        Length = this.ConvertLinearValue(package.Length.GetValueOrDefault(), plugin),
        Width = this.ConvertLinearValue(package.Width.GetValueOrDefault(), plugin),
        Height = this.ConvertLinearValue(package.Height.GetValueOrDefault(), plugin),
        CarrierPackage = pkgDetail.CarrierBoxName,
        CustomRefNbr1 = package.CustomRefNbr1,
        CustomRefNbr2 = package.CustomRefNbr2
      };
    }

    private List<SOCarrierPackageDetailEx> GetCarrierPackageDetail(
      List<SOPackageDetailEx> packages,
      string carrierID)
    {
      List<SOCarrierPackageDetailEx> carrierPackageDetail = new List<SOCarrierPackageDetailEx>();
      IEnumerable<CarrierPackage> source = GraphHelper.RowCast<CarrierPackage>((IEnumerable) PXSelectBase<CarrierPackage, PXSelect<CarrierPackage, Where<CarrierPackage.carrierID, Equal<Required<CarrierPackage.carrierID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
      {
        (object) carrierID
      })).AsEnumerable<CarrierPackage>();
      foreach (SOPackageDetailEx package1 in packages)
      {
        SOPackageDetailEx package = package1;
        carrierPackageDetail.Add(new SOCarrierPackageDetailEx()
        {
          CarrierID = carrierID,
          CarrierBoxName = source.Where<CarrierPackage>((Func<CarrierPackage, bool>) (x => x.BoxID.Equals(package.BoxID))).Select<CarrierPackage, string>((Func<CarrierPackage, string>) (y => y.CarrierBox)).FirstOrDefault<string>(),
          Package = package
        });
      }
      return carrierPackageDetail;
    }

    public virtual CarrierRequest BuildRequest(SOShipment shiporder)
    {
      PX.Objects.IN.INSite inSite = PX.Objects.IN.INSite.PK.Find((PXGraph) this.Base, shiporder.SiteID);
      if (inSite == null)
      {
        ((PXSelectBase) this.Base.Document).Cache.RaiseExceptionHandling<SOShipment.siteID>((object) shiporder, (object) shiporder.SiteID, (Exception) new PXSetPropertyException("Warehouse is required. It's address is used as shipment origin.", (PXErrorLevel) 4));
        throw new PXException("Warehouse is required. It's address is used as shipment origin.");
      }
      SOShipmentAddress soShipmentAddress = PXResultset<SOShipmentAddress>.op_Implicit(PXSelectBase<SOShipmentAddress, PXSelect<SOShipmentAddress, Where<SOShipmentAddress.addressID, Equal<Required<SOShipment.shipAddressID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
      {
        (object) shiporder.ShipAddressID
      }));
      SOShipmentContact soShipmentContact = PXResultset<SOShipmentContact>.op_Implicit(PXSelectBase<SOShipmentContact, PXSelect<SOShipmentContact, Where<SOShipmentContact.contactID, Equal<Required<SOShipment.shipContactID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
      {
        (object) shiporder.ShipContactID
      }));
      PX.Objects.CR.Address address1 = PXResultset<PX.Objects.CR.Address>.op_Implicit(PXSelectBase<PX.Objects.CR.Address, PXSelect<PX.Objects.CR.Address, Where<PX.Objects.CR.Address.addressID, Equal<Required<PX.Objects.CR.Address.addressID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
      {
        (object) inSite.AddressID
      }));
      PX.Objects.CR.Contact contact1 = PXResultset<PX.Objects.CR.Contact>.op_Implicit(PXSelectBase<PX.Objects.CR.Contact, PXSelect<PX.Objects.CR.Contact, Where<PX.Objects.CR.Contact.contactID, Equal<Required<PX.Objects.CR.Contact.contactID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
      {
        (object) inSite.ContactID
      }));
      PXResult<BAccountR, PX.Objects.GL.Branch, PX.Objects.GL.DAC.Organization> pxResult = (PXResult<BAccountR, PX.Objects.GL.Branch, PX.Objects.GL.DAC.Organization>) PXResultset<BAccountR>.op_Implicit(PXSelectBase<BAccountR, PXSelectJoin<BAccountR, InnerJoin<PX.Objects.GL.Branch, On<PX.Objects.GL.Branch.bAccountID, Equal<BAccountR.bAccountID>>, InnerJoin<PX.Objects.GL.DAC.Organization, On<PX.Objects.GL.DAC.Organization.organizationID, Equal<PX.Objects.GL.Branch.organizationID>>>>, Where<PX.Objects.GL.Branch.branchID, Equal<Required<PX.Objects.GL.Branch.branchID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
      {
        (object) inSite.BranchID
      }));
      PX.Objects.CR.BAccount baccount = (PX.Objects.CR.BAccount) PXResult<BAccountR, PX.Objects.GL.Branch, PX.Objects.GL.DAC.Organization>.op_Implicit(pxResult);
      PX.Objects.GL.Branch branch = PXResult<BAccountR, PX.Objects.GL.Branch, PX.Objects.GL.DAC.Organization>.op_Implicit(pxResult);
      PX.Objects.GL.DAC.Organization organization = PXResult<BAccountR, PX.Objects.GL.Branch, PX.Objects.GL.DAC.Organization>.op_Implicit(pxResult);
      PX.Objects.CR.Address address2 = PXResultset<PX.Objects.CR.Address>.op_Implicit(PXSelectBase<PX.Objects.CR.Address, PXSelect<PX.Objects.CR.Address, Where<PX.Objects.CR.Address.addressID, Equal<Required<PX.Objects.CR.Address.addressID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
      {
        (object) baccount.DefAddressID
      }));
      PX.Objects.CR.Contact contact2 = PXResultset<PX.Objects.CR.Contact>.op_Implicit(PXSelectBase<PX.Objects.CR.Contact, PXSelect<PX.Objects.CR.Contact, Where<PX.Objects.CR.Contact.contactID, Equal<Required<PX.Objects.CR.Contact.contactID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
      {
        (object) baccount.DefContactID
      }));
      PX.Objects.CS.Carrier carrier = PX.Objects.CS.Carrier.PK.Find((PXGraph) this.Base, shiporder.ShipVia);
      CarrierPlugin plugin = CarrierPlugin.PK.Find((PXGraph) this.Base, carrier.CarrierPluginID);
      this.ValidatePlugin(plugin);
      CarrierRequest carrierRequest1 = new CarrierRequest(this.GetUnitsType(plugin), shiporder.CuryID);
      carrierRequest1.Attributes = (IList<string>) new List<string>();
      PX.Objects.CR.Location location = PXResultset<PX.Objects.CR.Location>.op_Implicit(PXSelectBase<PX.Objects.CR.Location, PXSelect<PX.Objects.CR.Location, Where<PX.Objects.CR.Location.bAccountID, Equal<Required<PX.Objects.CR.Location.bAccountID>>, And<PX.Objects.CR.Location.locationID, Equal<Required<PX.Objects.CR.Location.locationID>>>>>.Config>.Select((PXGraph) this.Base, new object[2]
      {
        (object) shiporder.CustomerID,
        (object) shiporder.CustomerLocationID
      }));
      bool flag = shiporder.GroundCollect.GetValueOrDefault() && this.Base.CanUseGroundCollect(shiporder);
      if (flag || shiporder.UseCustomerAccount.GetValueOrDefault())
      {
        CarrierPluginCustomer carrierPluginCustomer = PXResultset<CarrierPluginCustomer>.op_Implicit(PXSelectBase<CarrierPluginCustomer, PXSelect<CarrierPluginCustomer, Where<CarrierPluginCustomer.carrierPluginID, Equal<Required<CarrierPluginCustomer.carrierPluginID>>, And<CarrierPluginCustomer.customerID, Equal<Required<CarrierPluginCustomer.customerID>>, And<CarrierPluginCustomer.isActive, Equal<True>, And<Where<CarrierPluginCustomer.customerLocationID, Equal<Required<CarrierPluginCustomer.customerLocationID>>, Or<CarrierPluginCustomer.customerLocationID, IsNull>>>>>>, OrderBy<Desc<CarrierPluginCustomer.customerLocationID>>>.Config>.Select((PXGraph) this.Base, new object[3]
        {
          (object) plugin.CarrierPluginID,
          (object) shiporder.CustomerID,
          (object) shiporder.CustomerLocationID
        }));
        if (!string.IsNullOrEmpty(carrierPluginCustomer?.CarrierAccount))
        {
          carrierRequest1.ThirdPartyAccountID = carrierPluginCustomer.CarrierAccount;
          PX.Objects.CR.Address address3 = PXResultset<PX.Objects.CR.Address>.op_Implicit(PXSelectBase<PX.Objects.CR.Address, PXSelect<PX.Objects.CR.Address, Where<PX.Objects.CR.Address.addressID, Equal<Required<PX.Objects.CR.Address.addressID>>>>.Config>.Select((PXGraph) this.Base, new object[1]
          {
            (object) location.DefAddressID
          }));
          carrierRequest1.ThirdPartyPostalCode = carrierPluginCustomer.PostalCode ?? address3.PostalCode;
          carrierRequest1.ThirdPartyCountryCode = carrierPluginCustomer.CountryID ?? address3.CountryID;
        }
        else if (shiporder.UseCustomerAccount.GetValueOrDefault())
          throw new PXException("Customer Account is not configured. Please setup the Carrier Account on the Carrier Plug-in screen.");
        if (shiporder.UseCustomerAccount.GetValueOrDefault() && carrierPluginCustomer?.CarrierBillingType == "R")
          carrierRequest1.Attributes.Add("RECEIVER");
      }
      Decimal num = 0M;
      Decimal? nullable1;
      if (shiporder.FreightAmountSource == "O")
      {
        SOOrderExtension extension = ((PXGraph) this.Base).GetExtension<SOOrderExtension>();
        IEnumerable<SOOrderShipment> source = GraphHelper.RowCast<SOOrderShipment>((IEnumerable) ((PXSelectBase<SOOrderShipment>) extension.OrderListSimple).Select(Array.Empty<object>()));
        if (source.Count<SOOrderShipment>() == 1)
        {
          SOOrderShipment soOrderShipment = source.FirstOrDefault<SOOrderShipment>();
          SOOrder soOrder = PXResultset<SOOrder>.op_Implicit(((PXSelectBase<SOOrder>) extension.soorder).Select(new object[2]
          {
            (object) soOrderShipment?.OrderType,
            (object) soOrderShipment?.OrderNbr
          }));
          Decimal? orderQty = (Decimal?) soOrder?.OrderQty;
          nullable1 = (Decimal?) soOrderShipment?.ShipmentQty;
          if (orderQty.GetValueOrDefault() == nullable1.GetValueOrDefault() & orderQty.HasValue == nullable1.HasValue)
          {
            Decimal? nullable2;
            if (soOrder == null)
            {
              nullable1 = new Decimal?();
              nullable2 = nullable1;
            }
            else
              nullable2 = soOrder.FreightAmt;
            nullable1 = nullable2;
            Decimal valueOrDefault1 = nullable1.GetValueOrDefault();
            Decimal? nullable3;
            if (soOrder == null)
            {
              nullable1 = new Decimal?();
              nullable3 = nullable1;
            }
            else
              nullable3 = soOrder.PremiumFreightAmt;
            nullable1 = nullable3;
            Decimal valueOrDefault2 = nullable1.GetValueOrDefault();
            num = valueOrDefault1 + valueOrDefault2;
          }
          else
            carrierRequest1.Attributes.Add("SKIPFREIGHTCHARGE");
        }
        else
          carrierRequest1.Attributes.Add("SKIPFREIGHTCHARGE");
      }
      else
        num = shiporder.FreightAmt.GetValueOrDefault();
      carrierRequest1.FreightCharge = num;
      SOAddress soAddress1;
      SOContact soContact1;
      this.Base.GetBillToAddressContact().Deconstruct<SOAddress, SOContact>(out soAddress1, out soContact1);
      SOAddress soAddress2 = soAddress1;
      SOContact soContact2 = soContact1;
      if (soAddress2 != null)
        carrierRequest1.BillToAddress = (IAddressBase) soAddress2;
      if (soContact2 != null)
        carrierRequest1.BillToContact = (IContactBase) soContact2;
      carrierRequest1.Shipper = (IAddressBase) address2;
      carrierRequest1.ShipperContact = (IContactBase) contact2;
      carrierRequest1.Origin = (IAddressBase) address1;
      carrierRequest1.OriginContact = (IContactBase) contact1;
      carrierRequest1.Destination = (IAddressBase) soShipmentAddress;
      carrierRequest1.DestinationContact = (IContactBase) soShipmentContact;
      carrierRequest1.Packages = this.GetPackages(shiporder, carrier, plugin);
      carrierRequest1.Resedential = shiporder.Resedential.GetValueOrDefault();
      carrierRequest1.SaturdayDelivery = shiporder.SaturdayDelivery.GetValueOrDefault();
      carrierRequest1.Insurance = shiporder.Insurance.GetValueOrDefault();
      CarrierRequest carrierRequest2 = carrierRequest1;
      DateTime? nullable4 = ((PXGraph) this.Base).Accessinfo.BusinessDate;
      DateTime date1 = nullable4.Value.Date;
      nullable4 = shiporder.ShipDate;
      DateTime date2 = nullable4.Value.Date;
      DateTime dateTime = Tools.Max<DateTime>(date1, date2);
      carrierRequest2.ShipDate = dateTime;
      carrierRequest1.ReceiverTaxID = location?.TaxRegistrationID;
      carrierRequest1.ShipperTaxID = baccount.TaxRegistrationID;
      if (flag)
        carrierRequest1.Attributes.Add("COLLECT");
      CarrierRequest carrierRequest3 = carrierRequest1;
      nullable1 = shiporder.LineTotal;
      Decimal valueOrDefault = nullable1.GetValueOrDefault();
      carrierRequest3.InvoiceLineTotal = valueOrDefault;
      if (!string.IsNullOrWhiteSpace(inSite.CarrierFacility))
        carrierRequest1.Attributes.Add("CarrierFacilityW:" + inSite.CarrierFacility);
      if (!string.IsNullOrWhiteSpace(branch.CarrierFacility))
        carrierRequest1.Attributes.Add("CarrierFacilityB:" + branch.CarrierFacility);
      else if (!string.IsNullOrWhiteSpace(organization.CarrierFacility))
        carrierRequest1.Attributes.Add("CarrierFacilityB:" + organization.CarrierFacility);
      return carrierRequest1;
    }

    protected override WebDialogResult AskForRateSelection()
    {
      return ((PXSelectBase<SOShipment>) this.Base.CurrentDocument).AskExt();
    }

    protected virtual void _(
      PX.Data.Events.FieldUpdated<SOPackageDetailEx, SOPackageDetailEx.boxID> e)
    {
      if (e.Row == null)
        return;
      ((PXSelectBase) this.Base.Packages).Cache.SetDefaultExt<SOPackageDetailEx.boxDescription>((object) e.Row);
      ((PXSelectBase) this.Base.Packages).Cache.SetDefaultExt<SOPackageDetail.length>((object) e.Row);
      ((PXSelectBase) this.Base.Packages).Cache.SetDefaultExt<SOPackageDetail.width>((object) e.Row);
      ((PXSelectBase) this.Base.Packages).Cache.SetDefaultExt<SOPackageDetail.height>((object) e.Row);
      ((PXSelectBase) this.Base.Packages).Cache.SetDefaultExt<SOPackageDetailEx.boxWeight>((object) e.Row);
      ((PXSelectBase) this.Base.Packages).Cache.SetDefaultExt<SOPackageDetailEx.maxWeight>((object) e.Row);
    }

    protected virtual void _(PX.Data.Events.RowSelected<SOShipment> e)
    {
      SOShipment row = e.Row;
      if (row == null)
        return;
      if (row.UnlimitedPackages.GetValueOrDefault())
      {
        ((PXAction) this.shopRates).SetEnabled(false);
        ((PXAction) this.shopRates).SetTooltip("The shipment was imported from an external system without validation of the number of packages against the license restriction. You cannot shop for rates.");
      }
      else
      {
        ((PXAction) this.shopRates).SetEnabled(true);
        ((PXAction) this.shopRates).SetTooltip("Shop for Rates");
      }
    }

    [PXOverride]
    public virtual void Persist(System.Action baseMtd)
    {
      if (((PXSelectBase<SOShipment>) this.Base.Document).Current != null)
      {
        bool? nullable1 = ((PXSelectBase<SOShipment>) this.Base.Document).Current.IsPackageValid;
        if (!nullable1.GetValueOrDefault())
        {
          nullable1 = ((PXSelectBase<SOShipment>) this.Base.Document).Current.Released;
          if (!nullable1.GetValueOrDefault())
          {
            nullable1 = ((PXSelectBase<SOShipment>) this.Base.Document).Current.Confirmed;
            if (!nullable1.GetValueOrDefault())
            {
              int? nullable2 = ((PXSelectBase<SOShipment>) this.Base.Document).Current.SiteID;
              if (nullable2.HasValue)
              {
                nullable2 = ((PXSelectBase<SOShipment>) this.Base.Document).Current.RecalcPackagesReason;
                if (nullable2.GetValueOrDefault() == 1 && this.Base.ValidateAvailablePackages())
                {
                  foreach (PXResult<SOPackageDetailEx> pxResult in ((PXSelectBase<SOPackageDetailEx>) this.Base.Packages).Select(Array.Empty<object>()))
                  {
                    SOPackageDetail soPackageDetail = (SOPackageDetail) PXResult<SOPackageDetailEx>.op_Implicit(pxResult);
                    if (soPackageDetail.PackageType == "A")
                      soPackageDetail.Confirmed = new bool?(false);
                  }
                  ((PXSelectBase<SOShipment>) this.Base.Document).Current.IsPackageValid = new bool?(true);
                }
                else
                  ((PXAction) this.recalculatePackages).Press();
              }
            }
          }
        }
      }
      baseMtd();
    }

    protected override IEnumerable<CarrierPlugin> GetApplicableCarrierPlugins()
    {
      return GraphHelper.RowCast<CarrierPlugin>((IEnumerable) PXSelectBase<CarrierPlugin, PXSelectReadonly<CarrierPlugin, Where<CarrierPlugin.isActive, Equal<True>, And<CarrierPlugin.siteID, IsNull, Or<CarrierPlugin.siteID, Equal<Current<SOShipment.siteID>>>>>>.Config>.Select((PXGraph) this.Base, Array.Empty<object>()));
    }

    private class LineInfo : CarrierRatesExtension<SOShipmentEntry, SOShipment>.ILineInfo
    {
      private SOShipLine _line;

      public LineInfo(SOShipLine line) => this._line = line;

      public Decimal? BaseQty => this._line.BaseQty;

      public Decimal? CuryLineAmt => this._line.LineAmt;

      public Decimal? ExtWeight => this._line.ExtWeight;

      public int? SiteID => this._line.SiteID;

      public string Operation => this._line.Operation;
    }
  }

  public class CartSupport : PXGraphExtension<SOShipmentEntry>
  {
    public FbqlSelect<SelectFromBase<SOShipmentSplitToCartSplitLink, TypeArrayOf<IFbqlJoin>.Empty>.Where<KeysRelation<Field<SOShipmentSplitToCartSplitLink.shipmentNbr>.IsRelatedTo<SOShipment.shipmentNbr>.AsSimpleKey.WithTablesOf<SOShipment, SOShipmentSplitToCartSplitLink>, SOShipment, SOShipmentSplitToCartSplitLink>.SameAsCurrent>, SOShipmentSplitToCartSplitLink>.View ShipmentCartLinks;
    public FbqlSelect<SelectFromBase<SOPickListEntryToCartSplitLink, TypeArrayOf<IFbqlJoin>.Empty>, SOPickListEntryToCartSplitLink>.View PickListCartLinks;
    public FbqlSelect<SelectFromBase<INCartSplit, TypeArrayOf<IFbqlJoin>.Empty>, INCartSplit>.View CartLinks;

    public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.wMSCartTracking>();

    public virtual void RemoveItemsFromCart()
    {
      foreach (PXResult<SOShipLineSplit, SOShipmentSplitToCartSplitLink, INCartSplit> pxResult in ((IEnumerable<PXResult<SOShipLineSplit>>) PXSelectBase<SOShipLineSplit, PXViewOf<SOShipLineSplit>.BasedOn<SelectFromBase<SOShipLineSplit, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<SOShipmentSplitToCartSplitLink>.On<SOShipmentSplitToCartSplitLink.FK.ShipmentSplitLine>>, FbqlJoins.Inner<INCartSplit>.On<SOShipmentSplitToCartSplitLink.FK.CartSplit>>>.Where<KeysRelation<Field<SOShipLineSplit.shipmentNbr>.IsRelatedTo<SOShipment.shipmentNbr>.AsSimpleKey.WithTablesOf<SOShipment, SOShipLineSplit>, SOShipment, SOShipLineSplit>.SameAsCurrent>>.Config>.Select((PXGraph) this.Base, Array.Empty<object>())).AsEnumerable<PXResult<SOShipLineSplit>>().Cast<PXResult<SOShipLineSplit, SOShipmentSplitToCartSplitLink, INCartSplit>>().ToArray<PXResult<SOShipLineSplit, SOShipmentSplitToCartSplitLink, INCartSplit>>())
      {
        SOShipLineSplit soShipLineSplit1;
        SOShipmentSplitToCartSplitLink splitToCartSplitLink1;
        INCartSplit inCartSplit1;
        pxResult.Deconstruct(ref soShipLineSplit1, ref splitToCartSplitLink1, ref inCartSplit1);
        SOShipLineSplit soShipLineSplit2 = soShipLineSplit1;
        SOShipmentSplitToCartSplitLink splitToCartSplitLink2 = splitToCartSplitLink1;
        INCartSplit inCartSplit2 = inCartSplit1;
        Decimal? qty = soShipLineSplit2.Qty;
        Decimal val1 = qty.Value;
        qty = splitToCartSplitLink2.Qty;
        Decimal val2 = qty.Value;
        Decimal num1 = Math.Min(val1, val2);
        SOShipmentSplitToCartSplitLink splitToCartSplitLink3 = ((PXSelectBase<SOShipmentSplitToCartSplitLink>) this.ShipmentCartLinks).Locate(splitToCartSplitLink2) ?? splitToCartSplitLink2;
        SOShipmentSplitToCartSplitLink splitToCartSplitLink4 = splitToCartSplitLink3;
        qty = splitToCartSplitLink4.Qty;
        Decimal num2 = num1;
        splitToCartSplitLink4.Qty = qty.HasValue ? new Decimal?(qty.GetValueOrDefault() - num2) : new Decimal?();
        qty = splitToCartSplitLink3.Qty;
        Decimal num3 = 0M;
        if (qty.GetValueOrDefault() <= num3 & qty.HasValue)
          ((PXSelectBase<SOShipmentSplitToCartSplitLink>) this.ShipmentCartLinks).Delete(splitToCartSplitLink3);
        else
          ((PXSelectBase<SOShipmentSplitToCartSplitLink>) this.ShipmentCartLinks).Update(splitToCartSplitLink3);
        INCartSplit inCartSplit3 = ((PXSelectBase<INCartSplit>) this.CartLinks).Locate(inCartSplit2) ?? inCartSplit2;
        INCartSplit inCartSplit4 = inCartSplit3;
        qty = inCartSplit4.Qty;
        Decimal num4 = num1;
        inCartSplit4.Qty = qty.HasValue ? new Decimal?(qty.GetValueOrDefault() - num4) : new Decimal?();
        qty = inCartSplit3.Qty;
        Decimal num5 = 0M;
        if (qty.GetValueOrDefault() <= num5 & qty.HasValue)
          ((PXSelectBase<INCartSplit>) this.CartLinks).Delete(inCartSplit3);
        else
          ((PXSelectBase<INCartSplit>) this.CartLinks).Update(inCartSplit3);
      }
    }

    public virtual SOShipmentSplitToCartSplitLink TransformCartLinks(
      SOShipLineSplit shipSplit,
      IReadOnlyCollection<SOPickListEntryToCartSplitLink> pickerCartLinks)
    {
      if (!pickerCartLinks.Any<SOPickListEntryToCartSplitLink>((Func<SOPickListEntryToCartSplitLink, bool>) (link =>
      {
        Decimal? qty = link.Qty;
        Decimal num = 0M;
        return qty.GetValueOrDefault() > num & qty.HasValue;
      })))
        return (SOShipmentSplitToCartSplitLink) null;
      SOPickListEntryToCartSplitLink entryToCartSplitLink1 = pickerCartLinks.Where<SOPickListEntryToCartSplitLink>((Func<SOPickListEntryToCartSplitLink, bool>) (link =>
      {
        Decimal? qty = link.Qty;
        Decimal num = 0M;
        return qty.GetValueOrDefault() > num & qty.HasValue;
      })).First<SOPickListEntryToCartSplitLink>();
      SOShipmentSplitToCartSplitLink splitToCartSplitLink1 = PXResultset<SOShipmentSplitToCartSplitLink>.op_Implicit(((PXSelectBase<SOShipmentSplitToCartSplitLink>) this.ShipmentCartLinks).Search<SOShipmentSplitToCartSplitLink.shipmentNbr, SOShipmentSplitToCartSplitLink.shipmentLineNbr, SOShipmentSplitToCartSplitLink.shipmentSplitLineNbr, SOShipmentSplitToCartSplitLink.siteID, SOShipmentSplitToCartSplitLink.cartID, SOShipmentSplitToCartSplitLink.cartSplitLineNbr>((object) shipSplit.ShipmentNbr, (object) shipSplit.LineNbr, (object) shipSplit.SplitLineNbr, (object) entryToCartSplitLink1.SiteID, (object) entryToCartSplitLink1.CartID, (object) entryToCartSplitLink1.CartSplitLineNbr, Array.Empty<object>()));
      if (splitToCartSplitLink1 == null)
        splitToCartSplitLink1 = ((PXSelectBase<SOShipmentSplitToCartSplitLink>) this.ShipmentCartLinks).Insert(new SOShipmentSplitToCartSplitLink()
        {
          ShipmentNbr = shipSplit.ShipmentNbr,
          ShipmentLineNbr = shipSplit.LineNbr,
          ShipmentSplitLineNbr = shipSplit.SplitLineNbr,
          SiteID = entryToCartSplitLink1.SiteID,
          CartID = entryToCartSplitLink1.CartID,
          CartSplitLineNbr = entryToCartSplitLink1.CartSplitLineNbr,
          Qty = new Decimal?(0M)
        });
      Decimal val2 = shipSplit.Qty.Value;
      foreach (SOPickListEntryToCartSplitLink entryToCartSplitLink2 in pickerCartLinks.Where<SOPickListEntryToCartSplitLink>((Func<SOPickListEntryToCartSplitLink, bool>) (link =>
      {
        Decimal? qty = link.Qty;
        Decimal num = 0M;
        return qty.GetValueOrDefault() > num & qty.HasValue;
      })))
      {
        if (!(val2 == 0M))
        {
          Decimal? qty = entryToCartSplitLink2.Qty;
          Decimal num1 = Math.Min(qty.Value, val2);
          SOShipmentSplitToCartSplitLink splitToCartSplitLink2 = splitToCartSplitLink1;
          qty = splitToCartSplitLink2.Qty;
          Decimal num2 = num1;
          splitToCartSplitLink2.Qty = qty.HasValue ? new Decimal?(qty.GetValueOrDefault() + num2) : new Decimal?();
          SOPickListEntryToCartSplitLink entryToCartSplitLink3 = entryToCartSplitLink2;
          qty = entryToCartSplitLink3.Qty;
          Decimal num3 = num1;
          entryToCartSplitLink3.Qty = qty.HasValue ? new Decimal?(qty.GetValueOrDefault() - num3) : new Decimal?();
          qty = entryToCartSplitLink2.Qty;
          Decimal num4 = 0M;
          if (qty.GetValueOrDefault() > num4 & qty.HasValue)
            ((PXSelectBase<SOPickListEntryToCartSplitLink>) this.PickListCartLinks).Update(entryToCartSplitLink2);
          else
            ((PXSelectBase<SOPickListEntryToCartSplitLink>) this.PickListCartLinks).Delete(entryToCartSplitLink2);
          val2 -= num1;
        }
        else
          break;
      }
      return ((PXSelectBase<SOShipmentSplitToCartSplitLink>) this.ShipmentCartLinks).Update(splitToCartSplitLink1);
    }
  }

  public class WorkLog : ShipmentWorkLog<SOShipmentEntry>
  {
    public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.wMSFulfillment>();
  }

  /// <exclude />
  public class SOShipmentEntryAddressLookupExtension : 
    AddressLookupExtension<SOShipmentEntry, SOShipment, SOShipmentAddress>
  {
    protected override string AddressView => "Shipping_Address";
  }

  public class SOShipmentEntryShippingAddressCachingHelper : 
    AddressValidationExtension<SOShipmentEntry, SOShipmentAddress>
  {
    protected override IEnumerable<PXSelectBase<SOShipmentAddress>> AddressSelects()
    {
      // ISSUE: reference to a compiler-generated field
      int num = this.\u003C\u003E1__state;
      SOShipmentEntry.SOShipmentEntryShippingAddressCachingHelper addressCachingHelper = this;
      if (num != 0)
      {
        if (num != 1)
          return false;
        // ISSUE: reference to a compiler-generated field
        this.\u003C\u003E1__state = -1;
        return false;
      }
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E1__state = -1;
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E2__current = (PXSelectBase<SOShipmentAddress>) addressCachingHelper.Base.Shipping_Address;
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E1__state = 1;
      return true;
    }
  }
}
