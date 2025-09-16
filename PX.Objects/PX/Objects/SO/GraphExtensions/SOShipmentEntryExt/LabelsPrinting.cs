// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.GraphExtensions.SOShipmentEntryExt.LabelsPrinting
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using PX.Api;
using PX.CarrierService;
using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.AR;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.SM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

#nullable enable
namespace PX.Objects.SO.GraphExtensions.SOShipmentEntryExt;

[PXProtectedAccess(null)]
public abstract class LabelsPrinting : PXGraphExtension<
#nullable disable
SOShipmentEntry>
{
  private const string ReturnLabel = "ReturnLabel";
  public PXAction<SOShipment> printLabels;
  public PXAction<SOShipment> printCommercialInvoices;
  public PXAction<SOShipment> getReturnLabelsAction;
  private readonly string[] mergeExtensions = new string[6]
  {
    "zpl",
    "epl",
    "dpl",
    "spl",
    "starpl",
    "pdf"
  };

  [PXButton(CommitChanges = true)]
  [PXUIField]
  protected virtual IEnumerable PrintLabels(PXAdapter adapter)
  {
    List<SOShipment> list = adapter.Get<SOShipment>().ToList<SOShipment>();
    if (adapter.MassProcess)
    {
      ((PXAction) this.Base.Save).Press();
      PrintPackageFilesArgs printArgs = new PrintPackageFilesArgs()
      {
        Shipments = list,
        Adapter = adapter,
        Category = PackageFileCategory.CarrierLabel
      };
      LabelsPrinting graph = ((PXGraph) PXGraph.CreateInstance<SOShipmentEntry>()).GetExtension<LabelsPrinting>();
      ((PXGraph) this.Base).LongOperationManager.StartAsyncOperation((Func<CancellationToken, System.Threading.Tasks.Task>) (ct => graph.PrintPackageFiles(printArgs, ct)));
    }
    else
      this.PrintCarrierLabels();
    return (IEnumerable) list;
  }

  [PXButton(CommitChanges = true)]
  [PXUIField]
  protected virtual IEnumerable PrintCommercialInvoices(PXAdapter adapter)
  {
    List<SOShipment> list = adapter.Get<SOShipment>().ToList<SOShipment>();
    if (adapter.MassProcess)
    {
      ((PXAction) this.Base.Save).Press();
      PrintPackageFilesArgs printArgs = new PrintPackageFilesArgs()
      {
        Shipments = list,
        Adapter = adapter,
        Category = PackageFileCategory.CommercialInvoice
      };
      LabelsPrinting graph = ((PXGraph) PXGraph.CreateInstance<SOShipmentEntry>()).GetExtension<LabelsPrinting>();
      ((PXGraph) this.Base).LongOperationManager.StartAsyncOperation((Func<CancellationToken, System.Threading.Tasks.Task>) (ct => graph.PrintPackageFiles(printArgs, ct)));
    }
    else
      ((PXGraph) this.Base).LongOperationManager.StartAsyncOperation((Func<CancellationToken, System.Threading.Tasks.Task>) (ct => this.PrintCommercInvoices(ct)));
    return (IEnumerable) list;
  }

  [PXButton(CommitChanges = true)]
  [PXUIField]
  protected virtual IEnumerable GetReturnLabelsAction(PXAdapter adapter)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    LabelsPrinting.\u003C\u003Ec__DisplayClass6_0 cDisplayClass60 = new LabelsPrinting.\u003C\u003Ec__DisplayClass6_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass60.list = adapter.Get<SOShipment>().ToList<SOShipment>();
    ((PXAction) this.Base.Save).Press();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass60.massProcess = adapter.MassProcess;
    // ISSUE: method pointer
    PXLongOperation.StartOperation((PXGraph) this.Base, new PXToggleAsyncDelegate((object) cDisplayClass60, __methodptr(\u003CGetReturnLabelsAction\u003Eb__0)));
    // ISSUE: reference to a compiler-generated field
    return (IEnumerable) cDisplayClass60.list;
  }

  public virtual void PrintCarrierLabels()
  {
    if (((PXSelectBase<SOShipment>) this.Base.Document).Current == null)
      return;
    if (((PXSelectBase<SOShipment>) this.Base.Document).Current.LabelsPrinted.GetValueOrDefault())
    {
      if (((PXSelectBase) this.Base.Document).View.Ask((object) ((PXSelectBase<SOShipment>) this.Base.Document).Current, "Confirmation", "Labels can be used only once. Are you sure you want to reprint labels?", (MessageButtons) 4, (MessageIcon) 2) != 6)
        return;
      PXTrace.WriteInformation("User Forced Labels Reprint for Shipment {0}", new object[1]
      {
        (object) ((PXSelectBase<SOShipment>) this.Base.Document).Current.ShipmentNbr
      });
    }
    ((PXGraph) this.Base).LongOperationManager.StartAsyncOperation((Func<CancellationToken, System.Threading.Tasks.Task>) (ct => this.PrintCarrierLabels(ct)));
  }

  public virtual async System.Threading.Tasks.Task PrintCarrierLabels(
    CancellationToken cancellationToken)
  {
    LabelsPrinting labelsPrinting = this;
    if (((PXSelectBase<SOShipment>) labelsPrinting.Base.Document).Current == null)
      return;
    PrintPackageFilesArgs printArg = new PrintPackageFilesArgs()
    {
      Shipments = new List<SOShipment>()
      {
        ((PXSelectBase<SOShipment>) labelsPrinting.Base.Document).Current
      },
      Adapter = labelsPrinting.CreateDummyAdapter(),
      Category = PackageFileCategory.CarrierLabel
    };
    await labelsPrinting.PrintPackageFiles(printArg, cancellationToken);
  }

  public virtual async System.Threading.Tasks.Task PrintCommercInvoices(
    CancellationToken cancellationToken)
  {
    LabelsPrinting labelsPrinting = this;
    if (((PXSelectBase<SOShipment>) labelsPrinting.Base.Document).Current == null)
      return;
    PrintPackageFilesArgs printArg = new PrintPackageFilesArgs()
    {
      Shipments = new List<SOShipment>()
      {
        ((PXSelectBase<SOShipment>) labelsPrinting.Base.Document).Current
      },
      Adapter = labelsPrinting.CreateDummyAdapter(),
      Category = PackageFileCategory.CommercialInvoice
    };
    await labelsPrinting.PrintPackageFiles(printArg, cancellationToken);
  }

  protected virtual void MarkPackageFilesPrinted(SOShipment shipment, PackageFileCategory category)
  {
    if (category.HasFlag((Enum) PackageFileCategory.CarrierLabel))
      shipment.LabelsPrinted = new bool?(true);
    if (!category.HasFlag((Enum) PackageFileCategory.CommercialInvoice))
      return;
    shipment.CommercialInvoicesPrinted = new bool?(true);
  }

  protected virtual string GetPrintFormID(PackageFileCategory category)
  {
    if (category.HasFlag((Enum) PackageFileCategory.CarrierLabel))
      return "SO645000";
    return category.HasFlag((Enum) PackageFileCategory.CommercialInvoice) ? "SO645010" : (string) null;
  }

  public virtual void LoadPackageFiles(PrintPackageFilesArgs printArg)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    LabelsPrinting.\u003C\u003Ec__DisplayClass12_0 cDisplayClass120 = new LabelsPrinting.\u003C\u003Ec__DisplayClass12_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass120.\u003C\u003E4__this = this;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass120.printArg = printArg;
    // ISSUE: reference to a compiler-generated field
    Guid? printerId = (Guid?) SMPrintJobMaint.GetPrintSettings(cDisplayClass120.printArg.Adapter)?.PrinterID;
    PXReportRequiredException requiredException = (PXReportRequiredException) null;
    Dictionary<Guid, ShipmentRelatedReports> dictionary1 = new Dictionary<Guid, ShipmentRelatedReports>();
    // ISSUE: reference to a compiler-generated method
    Func<string, Guid?> func1 = Func.Memorize<string, Guid?>(new Func<string, Guid?>(cDisplayClass120.\u003CLoadPackageFiles\u003Eb__0));
    // ISSUE: reference to a compiler-generated field
    cDisplayClass120.notificationUtility = new NotificationUtility((PXGraph) this.Base);
    // ISSUE: reference to a compiler-generated method
    Func<int?, string> func2 = Func.Memorize<int?, string>(new Func<int?, string>(cDisplayClass120.\u003CLoadPackageFiles\u003Eb__1));
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    LabelsPrinting.\u003C\u003Ec__DisplayClass12_1 cDisplayClass121 = new LabelsPrinting.\u003C\u003Ec__DisplayClass12_1();
    UploadFileMaintenance instance = PXGraph.CreateInstance<UploadFileMaintenance>();
    // ISSUE: reference to a compiler-generated field
    foreach (SOShipment shipment in cDisplayClass120.printArg.Shipments)
    {
      Guid guid = printerId ?? func1(shipment.ShipVia ?? string.Empty) ?? Guid.Empty;
      ShipmentRelatedReports shipmentRelatedReports = EnumerableEx.Ensure<Guid, ShipmentRelatedReports>((IDictionary<Guid, ShipmentRelatedReports>) dictionary1, guid, (Func<ShipmentRelatedReports>) (() => new ShipmentRelatedReports()));
      PXResultset<SOPackageDetailEx> packages = PXSelectBase<SOPackageDetailEx, PXSelect<SOPackageDetailEx, Where<SOPackageDetailEx.shipmentNbr, Equal<Required<SOShipment.shipmentNbr>>>>.Config>.Select((PXGraph) this.Base, new object[1]
      {
        (object) shipment.ShipmentNbr
      });
      if (packages.Count > 0)
      {
        // ISSUE: reference to a compiler-generated field
        FileInfo firstFile = this.GetFirstFile(packages, cDisplayClass120.printArg.Category);
        if (firstFile != null)
        {
          if (this.IsThermalPrinter(firstFile))
          {
            // ISSUE: reference to a compiler-generated field
            shipmentRelatedReports.LabelFiles.AddRange((IEnumerable<FileInfo>) this.LoadFiles(packages, cDisplayClass120.printArg.Category));
          }
          else
          {
            shipmentRelatedReports.LaserLabels.Add(shipment.ShipmentNbr);
            string str = func2(shipment.CustomerID);
            Dictionary<string, string> dictionary2 = new Dictionary<string, string>()
            {
              ["SOShipment.ShipmentNbr"] = shipment.ShipmentNbr
            };
            shipmentRelatedReports.ReportRedirect = PXReportRequiredException.CombineReport(shipmentRelatedReports.ReportRedirect, str, dictionary2, (CurrentLocalization) null);
            requiredException = PXReportRequiredException.CombineReport(requiredException, str, dictionary2, (CurrentLocalization) null);
            ((PXBaseRedirectException) requiredException).Mode = (PXBaseRedirectException.WindowMode) 2;
          }
        }
        else
          PXTrace.WriteWarning("No Label files to print for Shipment {0}", new object[1]
          {
            (object) shipment.ShipmentNbr
          });
      }
    }
    // ISSUE: reference to a compiler-generated field
    cDisplayClass121.screenID = ((PXGraph) this.Base).Accessinfo.ScreenID?.Replace(".", "") ?? "";
    // ISSUE: reference to a compiler-generated field
    if (!string.IsNullOrEmpty(cDisplayClass121.screenID))
    {
      // ISSUE: method pointer
      ((PXGraph) instance).FieldDefaulting.AddHandler<UploadFile.primaryScreenID>(new PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<UploadFile.primaryScreenID>>.EventDelegate((object) cDisplayClass121, __methodptr(\u003CLoadPackageFiles\u003Eb__2)));
    }
    foreach (KeyValuePair<Guid, ShipmentRelatedReports> keyValuePair in dictionary1)
    {
      ShipmentRelatedReports shipmentRelatedReports = keyValuePair.Value;
      if (shipmentRelatedReports.LabelFiles.Count > 1 && this.CanMerge((IList<FileInfo>) shipmentRelatedReports.LabelFiles))
      {
        FileInfo fileInfo = this.MergeFiles((IList<FileInfo>) shipmentRelatedReports.LabelFiles);
        shipmentRelatedReports.LabelFiles.Clear();
        if (!instance.SaveFile(fileInfo))
          throw new PXException("Failed to Save Merged file.");
        shipmentRelatedReports.LabelFiles.Add(fileInfo);
      }
    }
    // ISSUE: reference to a compiler-generated field
    cDisplayClass120.printArg.PrinterToReportsMap = dictionary1;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass120.printArg.RedirectToReport = requiredException;
  }

  public virtual async System.Threading.Tasks.Task PrintPackageFiles(
    PrintPackageFilesArgs printArg,
    CancellationToken cancellationToken)
  {
    LabelsPrinting labelsPrinting = this;
    if (printArg.Category == PackageFileCategory.None)
      return;
    printArg.PrintFormID = printArg.PrintFormID ?? labelsPrinting.GetPrintFormID(printArg.Category);
    using (PXTransactionScope transactionScope = new PXTransactionScope())
    {
      labelsPrinting.LoadPackageFiles(printArg);
      foreach (SOShipment shipment in printArg.Shipments)
      {
        labelsPrinting.MarkPackageFilesPrinted(shipment, printArg.Category);
        ((PXSelectBase<SOShipment>) labelsPrinting.Base.Document).Update(shipment);
      }
      ((PXAction) labelsPrinting.Base.Save).Press();
      transactionScope.Complete();
    }
    PXAdapter adapter = printArg.Adapter;
    if ((adapter != null ? (adapter.MassProcess ? 1 : 0) : 0) != 0)
    {
      for (int index = 0; index < printArg.Shipments.Count; ++index)
        PXProcessing<SOShipment>.SetInfo(index, "The record has been processed successfully.");
    }
    if (PXAccess.FeatureInstalled<FeaturesSet.deviceHub>())
      await labelsPrinting.CreatePrintJobs(printArg, cancellationToken);
    try
    {
      labelsPrinting.RedirectToFiles(printArg);
    }
    finally
    {
      printArg.PrinterToReportsMap = (Dictionary<Guid, ShipmentRelatedReports>) null;
      printArg.RedirectToReport = (PXReportRequiredException) null;
    }
  }

  public virtual async System.Threading.Tasks.Task CreatePrintJobs(
    PrintPackageFilesArgs printArg,
    CancellationToken cancellationToken)
  {
    LabelsPrinting labelsPrinting = this;
    string description;
    if (printArg.PrinterToReportsMap == null)
    {
      description = (string) null;
    }
    else
    {
      description = string.Empty;
      if (printArg.Category.HasFlag((Enum) PackageFileCategory.CarrierLabel))
        description = PXMessages.Localize("Print Labels");
      if (printArg.Category.HasFlag((Enum) PackageFileCategory.CommercialInvoice))
        description = PXMessages.Localize("Print Commercial Invoices");
      foreach (KeyValuePair<Guid, ShipmentRelatedReports> printerToReports in printArg.PrinterToReportsMap)
      {
        Guid guid;
        ShipmentRelatedReports shipmentRelatedReports;
        EnumerableExtensions.Deconstruct<Guid, ShipmentRelatedReports>(printerToReports, ref guid, ref shipmentRelatedReports);
        Guid printerID = guid;
        ShipmentRelatedReports reports = shipmentRelatedReports;
        if (!(printerID == Guid.Empty))
        {
          foreach (FileInfo labelFile in reports.LabelFiles)
          {
            PXAdapter adapter = printArg.Adapter;
            Func<string, string, int?, Guid?> func = (Func<string, string, int?, Guid?>) ((_param1, _param2, _param3) => new Guid?(printerID));
            string printFormId = printArg.PrintFormID;
            int? branchId = ((PXGraph) labelsPrinting.Base).Accessinfo.BranchID;
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary.Add("FILEID", labelFile.UID.ToString());
            string str = description;
            CancellationToken cancellationToken1 = cancellationToken;
            int num = await SMPrintJobMaint.CreatePrintJobForRawFile(adapter, func, "Customer", printFormId, branchId, dictionary, str, cancellationToken1) ? 1 : 0;
          }
          if (reports.LaserLabels.Count > 0 && reports.ReportRedirect != null)
          {
            int num1 = await SMPrintJobMaint.CreatePrintJobGroup(printArg.Adapter, (Func<string, string, int?, Guid?>) ((_param1, _param2, _param3) => new Guid?(printerID)), "Customer", printArg.PrintFormID, ((PXGraph) labelsPrinting.Base).Accessinfo.BranchID, reports.ReportRedirect, description, cancellationToken) ? 1 : 0;
          }
          reports = (ShipmentRelatedReports) null;
        }
      }
      description = (string) null;
    }
  }

  public virtual void RedirectToFiles(PrintPackageFilesArgs printArg)
  {
    if (printArg.PrinterToReportsMap == null)
      return;
    PXReportRequiredException redirectToReport = printArg.RedirectToReport;
    PXRedirectToFileException redirectToFileException = (PXRedirectToFileException) null;
    if (printArg.PrinterToReportsMap.Count == 1 && printArg.PrinterToReportsMap.First<KeyValuePair<Guid, ShipmentRelatedReports>>().Value.LabelFiles.Count == 1)
      redirectToFileException = new PXRedirectToFileException(printArg.PrinterToReportsMap.First<KeyValuePair<Guid, ShipmentRelatedReports>>().Value.LabelFiles.First<FileInfo>().UID, true);
    if (redirectToFileException != null && redirectToReport != null)
      return;
    if (redirectToFileException != null)
      throw redirectToFileException;
    if (redirectToReport != null)
      throw redirectToReport;
  }

  protected virtual Guid? SearchPrinter(
    string source,
    string reportID,
    int? branchID,
    string shipVia)
  {
    NotificationSetupUserOverride setupUserOverride = PXResultset<NotificationSetupUserOverride>.op_Implicit(PXSelectBase<NotificationSetupUserOverride, PXViewOf<NotificationSetupUserOverride>.BasedOn<SelectFromBase<NotificationSetupUserOverride, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<NotificationSetup>.On<NotificationSetupUserOverride.FK.DefaultSetup>>>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<NotificationSetupUserOverride.userID, Equal<BqlField<AccessInfo.userID, IBqlGuid>.FromCurrent>>>>, And<BqlOperand<NotificationSetupUserOverride.active, IBqlBool>.IsEqual<True>>>, And<BqlOperand<NotificationSetup.active, IBqlBool>.IsEqual<True>>>, And<BqlOperand<NotificationSetup.sourceCD, IBqlString>.IsEqual<P.AsString>>>, And<BqlOperand<NotificationSetup.reportID, IBqlString>.IsEqual<P.AsString>>>, And<BqlOperand<NotificationSetupUserOverride.shipVia, IBqlString>.IsEqual<P.AsString>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<NotificationSetup.nBranchID, Equal<P.AsInt>>>>>.Or<BqlOperand<NotificationSetup.nBranchID, IBqlInt>.IsNull>>>.Order<By<BqlField<NotificationSetup.nBranchID, IBqlInt>.Desc>>>.Config>.Select((PXGraph) this.Base, new object[4]
    {
      (object) source,
      (object) reportID,
      (object) shipVia,
      (object) branchID
    }));
    Guid? defaultPrinterId;
    if (setupUserOverride != null)
    {
      defaultPrinterId = setupUserOverride.DefaultPrinterID;
      if (defaultPrinterId.HasValue)
        return setupUserOverride.DefaultPrinterID;
    }
    if (source != null && reportID != null)
    {
      NotificationSetup notificationSetup = PXResultset<NotificationSetup>.op_Implicit(PXSelectBase<NotificationSetup, PXViewOf<NotificationSetup>.BasedOn<SelectFromBase<NotificationSetup, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<NotificationSetup.active, Equal<True>>>>, And<BqlOperand<NotificationSetup.sourceCD, IBqlString>.IsEqual<P.AsString>>>, And<BqlOperand<NotificationSetup.reportID, IBqlString>.IsEqual<P.AsString>>>, And<BqlOperand<NotificationSetup.shipVia, IBqlString>.IsEqual<P.AsString>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<NotificationSetup.nBranchID, Equal<P.AsInt>>>>>.Or<BqlOperand<NotificationSetup.nBranchID, IBqlInt>.IsNull>>>.Order<By<BqlField<NotificationSetup.nBranchID, IBqlInt>.Desc>>>.Config>.SelectWindowed((PXGraph) this.Base, 0, 1, new object[4]
      {
        (object) source,
        (object) reportID,
        (object) shipVia,
        (object) branchID
      }));
      if (notificationSetup != null)
      {
        defaultPrinterId = notificationSetup.DefaultPrinterID;
        if (defaultPrinterId.HasValue)
          return notificationSetup.DefaultPrinterID;
      }
    }
    return new NotificationUtility((PXGraph) this.Base).SearchPrinter(source, reportID, branchID);
  }

  protected virtual bool IsThermalPrinter(FileInfo fileInfo)
  {
    if (!Path.HasExtension(fileInfo.Name))
      return false;
    string lower = Path.GetExtension(fileInfo.Name).Substring(1).ToLower();
    if (lower.Length <= 2)
      return false;
    string str = lower.Substring(0, 3);
    return str == "zpl" || str == "epl" || str == "dpl" || str == "spl" || lower == "starpl" || str == "pdf";
  }

  protected virtual PackageFileCategory GetFileCategory(FileInfo fileInfo)
  {
    SOPackageNoteAttribute packageNoteAttribute = ((PXSelectBase) this.Base.Packages).Cache.GetAttributesReadonly((string) null).OfType<SOPackageNoteAttribute>().FirstOrDefault<SOPackageNoteAttribute>();
    return packageNoteAttribute == null ? PackageFileCategory.None : packageNoteAttribute.GetFileCategory(fileInfo.Name);
  }

  protected virtual FileInfo GetFirstFile(
    PXResultset<SOPackageDetailEx> packages,
    PackageFileCategory category)
  {
    UploadFileMaintenance instance = PXGraph.CreateInstance<UploadFileMaintenance>();
    foreach (PXResult<SOPackageDetailEx> package in packages)
    {
      foreach (Guid fileNote in PXNoteAttribute.GetFileNotes(((PXSelectBase) this.Base.Packages).Cache, (object) PXResult<SOPackageDetailEx>.op_Implicit(package)))
      {
        FileInfo fileWithNoData = instance.GetFileWithNoData(fileNote);
        if (category.HasFlag((Enum) this.GetFileCategory(fileWithNoData)))
          return fileWithNoData;
      }
    }
    return (FileInfo) null;
  }

  protected virtual IList<FileInfo> LoadFiles(
    PXResultset<SOPackageDetailEx> packages,
    PackageFileCategory category)
  {
    List<FileInfo> fileInfoList = new List<FileInfo>();
    UploadFileMaintenance instance = PXGraph.CreateInstance<UploadFileMaintenance>();
    foreach (PXResult<SOPackageDetailEx> package in packages)
    {
      SOPackageDetailEx soPackageDetailEx = PXResult<SOPackageDetailEx>.op_Implicit(package);
      FileInfo fileInfo = (FileInfo) null;
      foreach (Guid fileNote in PXNoteAttribute.GetFileNotes(((PXSelectBase) this.Base.Packages).Cache, (object) soPackageDetailEx))
      {
        FileInfo fileWithNoData = instance.GetFileWithNoData(fileNote);
        if (category.HasFlag((Enum) this.GetFileCategory(fileWithNoData)))
        {
          if (fileInfo == null)
          {
            ((PXSelectBase<UploadFileFilter>) instance.Filter).Current.FileRevisionID = new int?();
            fileInfo = instance.GetFile(fileNote);
            fileInfoList.Add(fileInfo);
          }
          else
            PXTrace.WriteWarning("There are more then one file attached to the package. But only first will be processed for Shipment {0}/{1}", new object[2]
            {
              (object) soPackageDetailEx.ShipmentNbr,
              (object) soPackageDetailEx.LineNbr
            });
        }
      }
    }
    return (IList<FileInfo>) fileInfoList;
  }

  protected virtual FileInfo GetReturnLabelFileForPackage(
    SOPackageDetail result,
    UploadFileMaintenance uploadFileMaintenance)
  {
    FileInfo labelFileForPackage = (FileInfo) null;
    if (result != null)
    {
      Guid[] fileNotes = PXNoteAttribute.GetFileNotes(((PXSelectBase) this.Base.Packages).Cache, (object) result);
      if (fileNotes.Length != 0)
      {
        for (int index = 0; index < fileNotes.Length; ++index)
        {
          FileInfo file = uploadFileMaintenance.GetFile(fileNotes[index]);
          if (file.Name.StartsWith("ReturnLabel"))
          {
            labelFileForPackage = file;
            break;
          }
        }
      }
    }
    return labelFileForPackage;
  }

  protected virtual bool CanMerge(IList<FileInfo> files)
  {
    string str1 = (string) null;
    foreach (FileInfo file in (IEnumerable<FileInfo>) files)
    {
      string str2 = Path.GetExtension(file.Name).ToLower();
      if (str2.StartsWith("."))
        str2 = str2.Substring(1);
      if (string.IsNullOrEmpty(str2))
        return false;
      str1 = str1 ?? str2;
      if (str1 != str2 || !((IEnumerable<string>) this.mergeExtensions).Contains<string>(str2) && (str2.Length <= 2 || !((IEnumerable<string>) this.mergeExtensions).Contains<string>(str2.Substring(0, 3))))
        return false;
    }
    return true;
  }

  protected virtual FileInfo MergeFiles(IList<FileInfo> files)
  {
    FileInfo fileInfo = (FileInfo) null;
    string str = (string) null;
    PdfDocument pdfDocument1 = (PdfDocument) null;
    try
    {
      using (MemoryStream memoryStream1 = new MemoryStream())
      {
        foreach (FileInfo file in (IEnumerable<FileInfo>) files)
        {
          string extension = Path.GetExtension(file.Name);
          if (str == null)
            str = extension;
          else if (extension.ToLowerInvariant() != str.ToLowerInvariant())
            throw new PXException("Files with different formats (extensions) cannot be merged.");
          if (extension.ToLowerInvariant() == ".pdf")
          {
            pdfDocument1 = pdfDocument1 ?? new PdfDocument();
            using (MemoryStream memoryStream2 = new MemoryStream(file.BinData))
            {
              using (PdfDocument pdfDocument2 = PdfReader.Open((Stream) memoryStream2, (PdfDocumentOpenMode) 1))
              {
                foreach (PdfPage page in pdfDocument2.Pages)
                  pdfDocument1.AddPage(page);
              }
            }
            pdfDocument1.Save((Stream) memoryStream1, false);
          }
          else
            memoryStream1.Write(file.BinData, 0, file.BinData.Length);
        }
        pdfDocument1?.Close();
        fileInfo = new FileInfo(Guid.NewGuid().ToString() + str, (string) null, memoryStream1.ToArray());
      }
      return fileInfo;
    }
    finally
    {
      pdfDocument1?.Dispose();
    }
  }

  public virtual void GetReturnLabels(SOShipment shiporder)
  {
    if (shiporder.UnlimitedPackages.GetValueOrDefault())
      throw new PXException("The shipment was imported from an external system without validation of the number of packages against the license restriction. You cannot get labels for this shipment in Acumatica ERP.");
    PX.Objects.CS.Carrier carrier1 = PX.Objects.CS.Carrier.PK.Find((PXGraph) this.Base, shiporder.ShipVia);
    int num;
    if (carrier1 == null)
    {
      num = 0;
    }
    else
    {
      bool? isActive = carrier1.IsActive;
      bool flag = false;
      num = isActive.GetValueOrDefault() == flag & isActive.HasValue ? 1 : 0;
    }
    if (num != 0)
      throw new PXException("The Ship Via code is not active.");
    if (!this.IsWithLabels(shiporder.ShipVia))
      return;
    ICarrierService cs = CarrierMaint.CreateCarrierService((PXGraph) this.Base, shiporder.ShipVia);
    CarrierRequest cr = this.Base.CarrierRatesExt.BuildRequest(shiporder);
    CarrierResult<ShipResult>[] carrierResultArray;
    if (!EnumerableExtensions.IsIn<string>(cs.GetType().FullName, "PX.FedExRestCarrier.FedExRestCarrier", "PX.UpsRestCarrier.UpsRestCarrier"))
      carrierResultArray = new CarrierResult<ShipResult>[1]
      {
        cs.Return(cr)
      };
    else
      carrierResultArray = ((IEnumerable<CarrierBox>) cr.Packages.ToArray<CarrierBox>()).Select<CarrierBox, CarrierResult<ShipResult>>((Func<CarrierBox, CarrierResult<ShipResult>>) (package =>
      {
        cr.Packages = (IList<CarrierBox>) new List<CarrierBox>()
        {
          package
        };
        return cs.Return(cr);
      })).ToArray<CarrierResult<ShipResult>>();
    StringBuilder stringBuilder1 = new StringBuilder();
    StringBuilder stringBuilder2 = new StringBuilder();
    foreach (CarrierResult<ShipResult> carrierResult in EnumerableExtensions.WhereNotNull<CarrierResult<ShipResult>>((IEnumerable<CarrierResult<ShipResult>>) carrierResultArray))
    {
      if (carrierResult.IsSuccess)
      {
        HashSet<int> hashSet = carrierResult.Result.Data.Select<PackageData, int>((Func<PackageData, int>) (t => t.RefNbr)).ToHashSet<int>();
        UploadFileMaintenance instance = PXGraph.CreateInstance<UploadFileMaintenance>();
        foreach (PXResult<SOPackageDetail> pxResult1 in PXSelectBase<SOPackageDetail, PXSelect<SOPackageDetail, Where<SOPackageDetail.shipmentNbr, Equal<Required<SOShipment.shipmentNbr>>>>.Config>.Select((PXGraph) this.Base, new object[1]
        {
          (object) shiporder.ShipmentNbr
        }))
        {
          SOPackageDetail result = PXResult<SOPackageDetail>.op_Implicit(pxResult1);
          if (hashSet.Contains(result.LineNbr.Value))
          {
            FileInfo labelFileForPackage = this.GetReturnLabelFileForPackage(result, instance);
            if (labelFileForPackage != null)
            {
              foreach (PXResult<NoteDoc> pxResult2 in PXSelectBase<NoteDoc, PXSelect<NoteDoc, Where<NoteDoc.noteID, Equal<Required<NoteDoc.noteID>>, And<NoteDoc.fileID, Equal<Required<NoteDoc.fileID>>>>>.Config>.Select((PXGraph) this.Base, new object[2]
              {
                (object) result.NoteID,
                (object) labelFileForPackage.UID
              }))
                UploadFileMaintenance.DeleteFile(PXResult<NoteDoc>.op_Implicit(pxResult2).FileID);
            }
          }
        }
        using (PXTransactionScope transactionScope = new PXTransactionScope())
        {
          PXTransactionScope.SetSuppressWorkflow(true);
          foreach (PackageData packageData in (IEnumerable<PackageData>) carrierResult.Result.Data)
          {
            SOPackageDetailEx soPackageDetailEx = PXResultset<SOPackageDetailEx>.op_Implicit(PXSelectBase<SOPackageDetailEx, PXSelect<SOPackageDetailEx, Where<SOPackageDetailEx.shipmentNbr, Equal<Required<SOShipment.shipmentNbr>>, And<SOPackageDetailEx.lineNbr, Equal<Required<SOPackageDetailEx.lineNbr>>>>>.Config>.Select((PXGraph) this.Base, new object[2]
            {
              (object) shiporder.ShipmentNbr,
              (object) packageData.RefNbr
            }));
            if (soPackageDetailEx != null)
            {
              FileInfo fileInfo = new FileInfo(string.Format("{2} #{0}.{1}", (object) packageData.TrackingNumber, (object) packageData.Format, (object) "ReturnLabel"), (string) null, packageData.Image);
              instance.SaveFile(fileInfo);
              soPackageDetailEx.ReturnTrackNumber = packageData.TrackingNumber;
              PXNoteAttribute.SetFileNotes(((PXSelectBase) this.Base.Packages).Cache, (object) soPackageDetailEx, new Guid[1]
              {
                fileInfo.UID.Value
              });
              ((PXSelectBase<SOPackageDetailEx>) this.Base.Packages).Update(soPackageDetailEx);
              PX.Objects.CS.Carrier carrier2 = PX.Objects.CS.Carrier.PK.Find((PXGraph) this.Base, shiporder.ShipVia);
              CarrierPlugin carrierPlugin = CarrierPlugin.PK.Find((PXGraph) this.Base, carrier2.CarrierPluginID);
              CarrierMethodSelectorAttribute.CarrierPluginMethod carrierPluginMethod = PXSelectorAttribute.Select<PX.Objects.CS.Carrier.pluginMethod>(((PXSelectBase) this.Base.carrier).Cache, (object) carrier2) as CarrierMethodSelectorAttribute.CarrierPluginMethod;
              string str = $"{carrier2.PluginMethod} - {carrierPluginMethod?.Description}";
              if (str.Length > (int) byte.MaxValue)
                str = str.Substring(0, (int) byte.MaxValue);
              Decimal baseCury = this.ConvertAmtToBaseCury(carrierResult.Result.Cost.Currency, ((PXSelectBase<ARSetup>) this.Base.arsetup).Current.DefaultRateTypeID, shiporder.ShipDate.Value, packageData.RateAmount);
              ((PXSelectBase<CarrierLabelHistory>) this.Base.LabelHistory).Insert(new CarrierLabelHistory()
              {
                ShipmentNbr = shiporder.ShipmentNbr,
                LineNbr = new int?(packageData.RefNbr),
                PluginTypeName = carrierPlugin?.PluginTypeName,
                ServiceMethod = str,
                RateAmount = new Decimal?(baseCury)
              });
            }
          }
          ((PXGraph) this.Base).Actions.PressSave();
          transactionScope.Complete();
        }
        foreach (Message message in (IEnumerable<Message>) carrierResult.Messages)
          stringBuilder1.AppendFormat("{0}:{1} ", (object) message.Code, (object) message.Description);
      }
      else
      {
        foreach (Message message in (IEnumerable<Message>) carrierResult.Messages)
          stringBuilder2.AppendFormat("{0}:{1} ", (object) message.Code, (object) message.Description);
      }
    }
    if (stringBuilder2.Length > 0 && stringBuilder1.Length > 0)
    {
      string str = string.Format($"Errors: {{0}}{Environment.NewLine}Warnings: {{1}}", (object) stringBuilder2.ToString(), (object) stringBuilder1.ToString());
      ((PXSelectBase) this.Base.Document).Cache.RaiseExceptionHandling<SOShipment.curyFreightCost>((object) shiporder, (object) shiporder.CuryFreightCost, (Exception) new PXSetPropertyException((IBqlTable) shiporder, "Carrier Service returned error. {0}", (PXErrorLevel) 4, new object[1]
      {
        (object) str
      }));
      throw new PXException("Carrier Service returned error. {0}", new object[1]
      {
        (object) str
      });
    }
    if (stringBuilder2.Length > 0)
    {
      ((PXSelectBase) this.Base.Document).Cache.RaiseExceptionHandling<SOShipment.curyFreightCost>((object) shiporder, (object) shiporder.CuryFreightCost, (Exception) new PXSetPropertyException((IBqlTable) shiporder, "Carrier Service returned error. {0}", (PXErrorLevel) 4, new object[1]
      {
        (object) stringBuilder2.ToString()
      }));
      throw new PXException("Carrier Service returned error. {0}", new object[1]
      {
        (object) stringBuilder2.ToString()
      });
    }
    if (stringBuilder1.Length <= 0)
      return;
    ((PXSelectBase) this.Base.Document).Cache.RaiseExceptionHandling<SOShipment.curyFreightCost>((object) shiporder, (object) shiporder.CuryFreightCost, (Exception) new PXSetPropertyException((IBqlTable) shiporder, stringBuilder1.ToString(), (PXErrorLevel) 2));
  }

  /// Uses <see cref="M:PX.Objects.SO.SOShipmentEntry.CreateDummyAdapter" />
  [PXProtectedAccess(null)]
  protected abstract PXAdapter CreateDummyAdapter();

  /// Uses <see cref="M:PX.Objects.SO.SOShipmentEntry.IsWithLabels(System.String)" />
  [PXProtectedAccess(null)]
  protected abstract bool IsWithLabels(string shipVia);

  /// Uses <see cref="M:PX.Objects.SO.SOShipmentEntry.ConvertAmtToBaseCury(System.String,System.String,System.DateTime,System.Decimal)" />
  [PXProtectedAccess(null)]
  protected abstract Decimal ConvertAmtToBaseCury(
    string from,
    string rateType,
    DateTime effectiveDate,
    Decimal amount);
}
