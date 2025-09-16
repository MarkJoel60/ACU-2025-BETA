// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APGenerateIntercompanyBills
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.AP.MigrationMode;
using PX.Objects.AR;
using PX.Objects.AR.GraphExtensions;
using PX.Objects.GL;
using PX.SM;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.AP;

[TableAndChartDashboardType]
public class APGenerateIntercompanyBills : PXGraph<APGenerateIntercompanyBills>
{
  public PXFilter<APGenerateIntercompanyBillsFilter> Filter;
  public PXCancel<APGenerateIntercompanyBillsFilter> Cancel;
  public APSetupNoMigrationMode apsetup;
  [PXFilterable(new System.Type[] {})]
  public PXFilteredProcessing<ARDocumentForAPDocument, APGenerateIntercompanyBillsFilter> Documents;
  public PXAction<APGenerateIntercompanyBillsFilter> viewARDocument;
  public PXAction<APGenerateIntercompanyBillsFilter> viewAPDocument;

  public static bool IsActive() => PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.interBranch>();

  public APGenerateIntercompanyBills()
  {
    APSetup current = this.apsetup.Current;
    PXUIFieldAttribute.SetEnabled(this.Documents.Cache, (string) null, false);
    PXUIFieldAttribute.SetEnabled<ARDocumentForAPDocument.selected>(this.Documents.Cache, (object) null, true);
    this.Documents.Cache.AllowInsert = false;
    this.Documents.Cache.AllowDelete = false;
    this.Documents.SetSelected<ARDocumentForAPDocument.selected>();
    this.Documents.SetProcessCaption("Process");
    this.Documents.SetProcessAllCaption("Process All");
    bool isVisible = PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.projectAccounting>();
    PXUIFieldAttribute.SetVisible<APGenerateIntercompanyBillsFilter.copyProjectInformationto>(this.Filter.Cache, (object) null, isVisible);
    PXUIFieldAttribute.SetVisible<APGenerateIntercompanyBillsFilter.projectID>(this.Filter.Cache, (object) null, isVisible);
  }

  protected void GenerateIntercompanyBill(
    ARInvoiceEntry arInvoiceEntryGraph,
    ARDocumentForAPDocument arInvoice,
    APGenerateIntercompanyBillsFilter filter)
  {
    GenerateIntercompanyBillExtension extension = arInvoiceEntryGraph.GetExtension<GenerateIntercompanyBillExtension>();
    APInvoiceEntry instance = PXGraph.CreateInstance<APInvoiceEntry>();
    GenerateIntercompanyBillExtension.GenerateBillParameters current = extension.generateBillParameters.Current;
    current.CreateOnHold = new bool?(filter.CreateOnHold.GetValueOrDefault());
    current.FinPeriodID = filter.CreateInSpecificPeriod.GetValueOrDefault() ? this.Filter.Current.FinPeriodID : (string) null;
    current.CopyProjectInformation = new bool?(filter.CopyProjectInformation.GetValueOrDefault());
    current.MassProcess = new bool?(true);
    PXProcessing<ARDocumentForAPDocument>.SetCurrentItem((object) arInvoice);
    arInvoiceEntryGraph.Document.Current = (PX.Objects.AR.ARInvoice) arInvoiceEntryGraph.Document.Search<PX.Objects.AR.ARInvoice.refNbr>((object) arInvoice.RefNbr, (object) arInvoice.DocType);
    PXSelect<PX.Objects.AR.ARInvoice, Where<PX.Objects.AR.ARInvoice.docType, Equal<Current<PX.Objects.AR.ARInvoice.docType>>, And<PX.Objects.AR.ARInvoice.refNbr, Equal<Current<PX.Objects.AR.ARInvoice.refNbr>>>>> currentDocument = arInvoiceEntryGraph.CurrentDocument;
    if ((currentDocument != null ? (((Guid?) currentDocument.Current?.NoteID).HasValue ? 1 : 0) : 0) == 0)
      return;
    APInvoice intercompanyBill = extension.GenerateIntercompanyBill(instance, arInvoiceEntryGraph.CurrentDocument?.Current, current);
    instance.Save.Press();
    if (intercompanyBill != null)
      arInvoice.IntercompanyAPDocumentNoteID = intercompanyBill.NoteID;
    PX.Objects.Common.ProcessingResult processingResult = extension.CheckGeneratedAPDocument(arInvoiceEntryGraph.CurrentDocument?.Current, intercompanyBill);
    if (!string.IsNullOrEmpty(PXProcessing<ARDocumentForAPDocument>.GetItemMessage()?.Message))
      return;
    if (!processingResult.IsSuccess)
      PXProcessing<ARDocumentForAPDocument>.SetError(processingResult.GeneralMessage);
    else if (processingResult.HasWarning)
      PXProcessing<ARDocumentForAPDocument>.SetWarning(processingResult.GeneralMessage);
    else
      PXProcessing<ARDocumentForAPDocument>.SetProcessed();
  }

  public void _(
    PX.Data.Events.RowSelected<APGenerateIntercompanyBillsFilter> e)
  {
    APGenerateIntercompanyBillsFilter filter = e.Row;
    PXUIFieldAttribute.SetEnabled<APGenerateIntercompanyBillsFilter.finPeriodID>(e.Cache, (object) filter, filter.CreateInSpecificPeriod.GetValueOrDefault());
    this.Documents.SetProcessDelegate<ARInvoiceEntry>((PXProcessingBase<ARDocumentForAPDocument>.ProcessItemDelegate<ARInvoiceEntry>) ((arInvoiceEntry, doc) => this.GenerateIntercompanyBill(arInvoiceEntry, doc, filter)));
    this.Documents.SetParametersDelegate((PXProcessingBase<ARDocumentForAPDocument>.ParametersDelegate) (list =>
    {
      bool flag = true;
      if (PXContext.GetSlot<AUSchedule>() == null && list.Count > 1000)
        flag = this.Documents.Ask("The process will process more than 1000 items. It will take time.", MessageButtons.OKCancel) == WebDialogResult.OK;
      return flag;
    }));
  }

  public void _(
    PX.Data.Events.FieldUpdating<APGenerateIntercompanyBillsFilter, APGenerateIntercompanyBillsFilter.createInSpecificPeriod> e)
  {
    if (!((bool?) e.NewValue).GetValueOrDefault() || e.NewValue == e.OldValue || e.Row.CustomerID.HasValue || PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.centralizedPeriodsManagement>())
      return;
    e.NewValue = (object) false;
    PXUIFieldAttribute.SetError<APGenerateIntercompanyBillsFilter.customerID>(e.Cache, (object) e.Row, "Fill in the Purchaser box to retrieve financial periods.");
  }

  public void _(
    PX.Data.Events.FieldUpdated<APGenerateIntercompanyBillsFilter, APGenerateIntercompanyBillsFilter.createInSpecificPeriod> e)
  {
    bool? newValue = (bool?) e.NewValue;
    bool flag = false;
    if (!(newValue.GetValueOrDefault() == flag & newValue.HasValue) || e.NewValue == e.OldValue)
      return;
    e.Row.FinPeriodID = (string) null;
  }

  public void _(
    PX.Data.Events.FieldUpdated<APGenerateIntercompanyBillsFilter, APGenerateIntercompanyBillsFilter.customerID> e)
  {
    if (e.NewValue != null || e.NewValue == e.OldValue || PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.centralizedPeriodsManagement>())
      return;
    e.Row.CreateInSpecificPeriod = new bool?(false);
    e.Row.FinPeriodID = (string) null;
  }

  public virtual IEnumerable documents(PXAdapter adapter)
  {
    using (new PXReadBranchRestrictedScope()
    {
      SpecificBranchTable = typeof (PX.Objects.AR.ARInvoice).Name
    })
    {
      PX.Data.PXView view = this.GetInvoicesSelectCommand().View;
      int startRow = PX.Data.PXView.StartRow;
      int num = 0;
      object[] parameters = new object[1]
      {
        (object) PXAccess.GetBranchIDs()
      };
      object[] searches = PX.Data.PXView.Searches;
      string[] sortColumns = PX.Data.PXView.SortColumns;
      bool[] descendings = PX.Data.PXView.Descendings;
      PXFilterRow[] filters = (PXFilterRow[]) PX.Data.PXView.Filters;
      ref int local1 = ref startRow;
      int maximumRows = PX.Data.PXView.MaximumRows;
      ref int local2 = ref num;
      List<object> objectList = view.Select((object[]) null, parameters, searches, sortColumns, descendings, filters, ref local1, maximumRows, ref local2);
      PX.Data.PXView.StartRow = 0;
      return (IEnumerable) objectList;
    }
  }

  protected virtual PXSelectBase<ARDocumentForAPDocument> GetInvoicesSelectCommand()
  {
    APGenerateIntercompanyBillsFilter current = this.Filter.Current;
    PXSelectBase<ARDocumentForAPDocument> invoicesSelectCommand = (PXSelectBase<ARDocumentForAPDocument>) new PXSelect<ARDocumentForAPDocument, Where2<Match<Current<AccessInfo.userName>>, And2<Where<ARDocumentForAPDocument.branchID, IsNull>, Or<ARDocumentForAPDocument.branchID, In<Required<ARDocumentForAPDocument.branchID>>>>>>((PXGraph) this);
    if (current.Date.HasValue)
      invoicesSelectCommand.WhereAnd<Where<ARDocumentForAPDocument.docDate, LessEqual<Current<APGenerateIntercompanyBillsFilter.date>>>>();
    if (current.CustomerID.HasValue)
      invoicesSelectCommand.WhereAnd<Where<ARDocumentForAPDocument.customerID, Equal<Current<APGenerateIntercompanyBillsFilter.customerID>>>>();
    if (current.VendorID.HasValue)
      invoicesSelectCommand.WhereAnd<Where<ARDocumentForAPDocument.vendorID, Equal<Current<APGenerateIntercompanyBillsFilter.vendorID>>>>();
    if (current.ProjectID.HasValue)
      invoicesSelectCommand.WhereAnd<Where<ARDocumentForAPDocument.projectID, Equal<Current<APGenerateIntercompanyBillsFilter.projectID>>>>();
    return invoicesSelectCommand;
  }

  [PXUIField(DisplayName = "", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select, Visible = false)]
  [PXEditDetailButton(ImageKey = "DataEntry")]
  public virtual IEnumerable ViewARDocument(PXAdapter adapter)
  {
    ARDocumentForAPDocument current = this.Documents.Current;
    if (current != null)
    {
      ARInvoiceEntry instance = PXGraph.CreateInstance<ARInvoiceEntry>();
      instance.Document.Current = (PX.Objects.AR.ARInvoice) instance.Document.Search<PX.Objects.AR.ARInvoice.refNbr>((object) current.RefNbr, (object) current.DocType);
      PXRedirectHelper.TryRedirect((PXGraph) instance, PXRedirectHelper.WindowMode.NewWindow);
    }
    return (IEnumerable) this.Filter.Select();
  }

  [PXUIField(DisplayName = "", MapEnableRights = PXCacheRights.Select, MapViewRights = PXCacheRights.Select, Visible = false)]
  [PXEditDetailButton(ImageKey = "DataEntry")]
  public virtual IEnumerable ViewAPDocument(PXAdapter adapter)
  {
    ARDocumentForAPDocument current = this.Documents.Current;
    if (current != null && current.IntercompanyAPDocumentNoteID.HasValue)
      new EntityHelper((PXGraph) this).NavigateToRow(current.IntercompanyAPDocumentNoteID, PXRedirectHelper.WindowMode.NewWindow);
    return adapter.Get();
  }

  public override bool IsDirty => false;
}
