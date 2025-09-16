// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.ProjectAccounting.PM.Graphs.SubstantiatedBilling
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.AP;
using PX.Objects.CA;
using PX.Objects.CN.ProjectAccounting.PM.DAC;
using PX.Objects.CT;
using PX.Objects.GL;
using PX.Objects.GL.FinPeriods;
using PX.Objects.PM;
using PX.Reports;
using PX.Reports.Controls;
using PX.Reports.Data;
using PX.SM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

#nullable enable
namespace PX.Objects.CN.ProjectAccounting.PM.Graphs;

public class SubstantiatedBilling : PXGraph<
#nullable disable
SubstantiatedBilling>
{
  public static readonly HashSet<string> ValidImageExtensions = new HashSet<string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase)
  {
    ".bmp",
    ".gif",
    ".jpeg",
    ".jpg",
    ".png",
    ".tif",
    ".tiff"
  };
  public const string PdfExtension = ".pdf";
  protected int queryTimeout;
  protected IDictionary<string, string> OrigDocTypes = (IDictionary<string, string>) new PMOrigDocType.ListAttribute().ValueLabelDic;
  protected IDictionary<string, string> APOrigDocTypes = (IDictionary<string, string>) new APDocType.ListAttribute().ValueLabelDic;
  public PXCancel<SubstantiatedBilling.Substantial> Cancel;
  public PXFilter<SubstantiatedBilling.Substantial> MasterView;
  public FbqlSelect<SelectFromBase<PMTran, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PMAccountGroup>.On<BqlOperand<
  #nullable enable
  PMAccountGroup.groupID, IBqlInt>.IsEqual<
  #nullable disable
  PMTran.accountGroupID>>>, FbqlJoins.Left<PMRegister>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  PMRegister.module, 
  #nullable disable
  Equal<PMTran.tranType>>>>>.And<BqlOperand<
  #nullable enable
  PMRegister.refNbr, IBqlString>.IsEqual<
  #nullable disable
  PMTran.refNbr>>>>, FbqlJoins.Left<PX.Objects.AP.APRegister>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  PX.Objects.AP.APRegister.docType, 
  #nullable disable
  Equal<PMTran.origTranType>>>>>.And<BqlOperand<
  #nullable enable
  PX.Objects.AP.APRegister.refNbr, IBqlString>.IsEqual<
  #nullable disable
  PMTran.origRefNbr>>>>, FbqlJoins.Left<CAAdj>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  CAAdj.adjTranType, 
  #nullable disable
  Equal<PMTran.origTranType>>>>>.And<BqlOperand<
  #nullable enable
  CAAdj.adjRefNbr, IBqlString>.IsEqual<
  #nullable disable
  PMTran.origRefNbr>>>>, FbqlJoins.Left<CASplit>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  CASplit.projectID, 
  #nullable disable
  Equal<PMTran.projectID>>>>, And<BqlOperand<
  #nullable enable
  CASplit.adjTranType, IBqlString>.IsEqual<
  #nullable disable
  CAAdj.adjTranType>>>, And<BqlOperand<
  #nullable enable
  CASplit.adjRefNbr, IBqlString>.IsEqual<
  #nullable disable
  CAAdj.adjRefNbr>>>>.And<BqlOperand<
  #nullable enable
  CASplit.lineNbr, IBqlInt>.IsEqual<
  #nullable disable
  PMTran.origLineNbr>>>>, FbqlJoins.Left<PX.Objects.GL.Batch>.On<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  PMTran.origModule, 
  #nullable disable
  Equal<BatchModule.moduleGL>>>>, And<BqlOperand<
  #nullable enable
  PX.Objects.GL.Batch.module, IBqlString>.IsEqual<
  #nullable disable
  PMTran.origModule>>>>.And<BqlOperand<
  #nullable enable
  PX.Objects.GL.Batch.batchNbr, IBqlString>.IsEqual<
  #nullable disable
  PMTran.batchNbr>>>>, FbqlJoins.Left<PX.Objects.GL.GLTran>.On<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  PX.Objects.GL.GLTran.module, 
  #nullable disable
  Equal<PX.Objects.GL.Batch.module>>>>, And<BqlOperand<
  #nullable enable
  PX.Objects.GL.GLTran.batchNbr, IBqlString>.IsEqual<
  #nullable disable
  PX.Objects.GL.Batch.batchNbr>>>>.And<BqlOperand<
  #nullable enable
  PX.Objects.GL.GLTran.lineNbr, IBqlInt>.IsEqual<
  #nullable disable
  PMTran.origLineNbr>>>>, FbqlJoins.Left<NoteDoc>.On<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  PX.Objects.AP.APRegister.noteID, 
  #nullable disable
  Equal<NoteDoc.noteID>>>>, Or<BqlOperand<
  #nullable enable
  PMRegister.noteID, IBqlGuid>.IsEqual<
  #nullable disable
  NoteDoc.noteID>>>, Or<BqlOperand<
  #nullable enable
  CAAdj.noteID, IBqlGuid>.IsEqual<
  #nullable disable
  NoteDoc.noteID>>>, Or<BqlOperand<
  #nullable enable
  PX.Objects.GL.Batch.noteID, IBqlGuid>.IsEqual<
  #nullable disable
  NoteDoc.noteID>>>, Or<Brackets<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<
  #nullable enable
  SubstantiatedBilling.Substantial.includeLineAttachments>, 
  #nullable disable
  Equal<True>>>>>.And<BqlOperand<
  #nullable enable
  PMTran.noteID, IBqlGuid>.IsEqual<
  #nullable disable
  NoteDoc.noteID>>>>>, Or<Brackets<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<
  #nullable enable
  SubstantiatedBilling.Substantial.includeLineAttachments>, 
  #nullable disable
  Equal<True>>>>>.And<BqlOperand<
  #nullable enable
  PX.Objects.GL.GLTran.noteID, IBqlGuid>.IsEqual<
  #nullable disable
  NoteDoc.noteID>>>>>>.Or<Brackets<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<
  #nullable enable
  SubstantiatedBilling.Substantial.includeLineAttachments>, 
  #nullable disable
  Equal<True>>>>>.And<BqlOperand<
  #nullable enable
  CASplit.noteID, IBqlGuid>.IsEqual<
  #nullable disable
  NoteDoc.noteID>>>>>>, FbqlJoins.Inner<UploadFile>.On<BqlOperand<
  #nullable enable
  UploadFile.fileID, IBqlGuid>.IsEqual<
  #nullable disable
  NoteDoc.fileID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  PMTran.projectID, 
  #nullable disable
  Equal<BqlField<
  #nullable enable
  SubstantiatedBilling.Substantial.projectID, IBqlInt>.FromCurrent>>>>, 
  #nullable disable
  And<BqlOperand<
  #nullable enable
  PMAccountGroup.type, IBqlString>.IsEqual<
  #nullable disable
  AccountType.expense>>>, And<Brackets<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<
  #nullable enable
  SubstantiatedBilling.Substantial.mode>, 
  #nullable disable
  Equal<SubstantiatedBillingMode.dateOnly>>>>>.And<BqlOperand<
  #nullable enable
  PMTran.tranDate, IBqlDateTime>.IsBetween<
  #nullable disable
  BqlField<
  #nullable enable
  SubstantiatedBilling.Substantial.fromDate, IBqlDateTime>.FromCurrent, 
  #nullable disable
  BqlField<
  #nullable enable
  SubstantiatedBilling.Substantial.toDate, IBqlDateTime>.FromCurrent>>>>, 
  #nullable disable
  Or<Brackets<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<
  #nullable enable
  SubstantiatedBilling.Substantial.mode>, 
  #nullable disable
  Equal<SubstantiatedBillingMode.proFormaAndDate>>>>>.And<BqlOperand<
  #nullable enable
  PMTran.tranDate, IBqlDateTime>.IsBetween<
  #nullable disable
  BqlField<
  #nullable enable
  SubstantiatedBilling.Substantial.fromDate, IBqlDateTime>.FromCurrent, 
  #nullable disable
  BqlField<
  #nullable enable
  SubstantiatedBilling.Substantial.toDate, IBqlDateTime>.FromCurrent>>>>>, 
  #nullable disable
  Or<Brackets<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<
  #nullable enable
  SubstantiatedBilling.Substantial.mode>, 
  #nullable disable
  Equal<SubstantiatedBillingMode.proFormaOnly>>>>>.And<BqlOperand<
  #nullable enable
  PMTran.proformaRefNbr, IBqlString>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  SubstantiatedBilling.Substantial.proFormaRefNbr, IBqlString>.FromCurrent>>>>>>.Or<
  #nullable disable
  Brackets<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<
  #nullable enable
  SubstantiatedBilling.Substantial.mode>, 
  #nullable disable
  Equal<SubstantiatedBillingMode.proFormaAndDate>>>>>.And<BqlOperand<
  #nullable enable
  PMTran.proformaRefNbr, IBqlString>.IsEqual<
  #nullable disable
  BqlField<
  #nullable enable
  SubstantiatedBilling.Substantial.proFormaRefNbr, IBqlString>.FromCurrent>>>>>>>>.And<
  #nullable disable
  Brackets<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Current<
  #nullable enable
  SubstantiatedBilling.Substantial.includeNonBillable>, 
  #nullable disable
  Equal<True>>>>>.Or<BqlOperand<
  #nullable enable
  PMTran.billable, IBqlBool>.IsEqual<
  #nullable disable
  True>>>>>.Aggregate<To<GroupBy<PMTran.costCodeID>, GroupBy<PMTran.tranType>, GroupBy<PMTran.origRefNbr>, GroupBy<PMTran.refNbr>, GroupBy<UploadFile.fileID>>>.Order<By<BqlField<
  #nullable enable
  PMTran.projectID, IBqlInt>.Asc, 
  #nullable disable
  BqlField<
  #nullable enable
  PMTran.costCodeID, IBqlInt>.Asc, 
  #nullable disable
  BqlField<
  #nullable enable
  PMTran.tranType, IBqlString>.Asc, 
  #nullable disable
  BqlField<
  #nullable enable
  PMTran.origRefNbr, IBqlString>.Asc, 
  #nullable disable
  BqlField<
  #nullable enable
  PMTran.refNbr, IBqlString>.Asc>>, 
  #nullable disable
  PMTran>.View AttachmentsView;
  public PXAction<SubstantiatedBilling.Substantial> GetFile;

  [InjectDependency]
  protected IReportLoaderService ReportLoader { get; private set; }

  [InjectDependency]
  protected IReportRenderer ReportRenderer { get; private set; }

  [InjectDependency]
  public IFinPeriodRepository FinPeriodRepository { get; set; }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<SubstantiatedBilling.Substantial, SubstantiatedBilling.Substantial.fromDate> e)
  {
    if (e.Row == null)
      return;
    int? parentOrganizationId = PXAccess.GetParentOrganizationID(((PXGraph) this).Accessinfo.BranchID);
    string finPeriodId = this.FinPeriodRepository.FindFinPeriodByDate(((PXGraph) this).Accessinfo.BusinessDate, parentOrganizationId)?.FinPeriodID;
    if (string.IsNullOrEmpty(finPeriodId))
      return;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<SubstantiatedBilling.Substantial, SubstantiatedBilling.Substantial.fromDate>, SubstantiatedBilling.Substantial, object>) e).NewValue = (object) this.FinPeriodRepository.PeriodStartDate(finPeriodId, parentOrganizationId);
  }

  [PXUIField]
  [PXMergeAttributes]
  protected virtual void _(PX.Data.Events.CacheAttached<PMProforma.description> e)
  {
  }

  [PXButton]
  [PXUIField(DisplayName = "Download Report")]
  protected IEnumerable getFile(PXAdapter a)
  {
    this.Check();
    List<byte[]> reportBytes = new List<byte[]>();
    FileInfo fileInfo1 = (FileInfo) null;
    using (Report report = this.ReportLoader.LoadReport("PM650000", (IPXResultset) null))
    {
      if (report == null)
        throw new Exception("Unable to access Acumatica report writter for specified report : PM650000");
      Dictionary<string, string> dictionary = new Dictionary<string, string>();
      PMProject pmProject = PXResultset<PMProject>.op_Implicit(PXSelectBase<PMProject, PXSelect<PMProject, Where<PMProject.contractID, Equal<Current<SubstantiatedBilling.Substantial.projectID>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()));
      dictionary["ProjectID"] = pmProject.ContractCD;
      dictionary["TranMode"] = ((PXSelectBase<SubstantiatedBilling.Substantial>) this.MasterView).Current.Mode;
      dictionary["DateFrom"] = ((PXSelectBase<SubstantiatedBilling.Substantial>) this.MasterView).Current.FromDate.ToString();
      dictionary["DateTo"] = ((PXSelectBase<SubstantiatedBilling.Substantial>) this.MasterView).Current.ToDate.ToString();
      dictionary["ProFormaRefNbr"] = ((PXSelectBase<SubstantiatedBilling.Substantial>) this.MasterView).Current.ProFormaRefNbr;
      dictionary["IncludeNonBillable"] = ((PXSelectBase<SubstantiatedBilling.Substantial>) this.MasterView).Current.IncludeNonBillable.ToString();
      this.ReportLoader.InitDefaultReportParameters(report, (IDictionary<string, string>) dictionary);
      SubstantiatedBilling.ReflectionMethods.SetPrivatePropertyValue<PXDatabaseProvider>(PXDatabase.Provider, "queryTimeout", (object) 900);
      using (StreamManager streamManager = new StreamManager())
      {
        this.ReportRenderer.Render("PDF", report, (Hashtable) null, streamManager);
        FileInfo fileInfo2 = new FileInfo($"SubstantiatedBilling #{dictionary}.pdf", (string) null, streamManager.MainStream.GetBytes());
        reportBytes.Add(fileInfo2.BinData);
      }
    }
    fileInfo1 = (FileInfo) null;
    byte[] numArray = this.MergePDFs(reportBytes);
    FileInfo fileInfo3 = new FileInfo($"SubstantiatedBilling_{DateTime.Today.ToShortDateString().Replace("/", "-")}.pdf", "", numArray);
    if (fileInfo3 == null)
      return a.Get();
    throw new PXRedirectToFileException(fileInfo3, true);
  }

  /// <summary>Validates the form on submit button click</summary>
  public void Check()
  {
    SubstantiatedBilling.Substantial current = ((PXSelectBase<SubstantiatedBilling.Substantial>) this.MasterView).Current;
    if ((current != null ? (!current.ProjectID.HasValue ? 1 : 0) : 1) == 0)
    {
      DateTime? nullable;
      if (current.Mode == "D" || current.Mode == "B")
      {
        nullable = current.FromDate;
        if (nullable.HasValue)
        {
          nullable = current.ToDate;
          if (!nullable.HasValue)
            goto label_5;
        }
        else
          goto label_5;
      }
      if (!(current.Mode == "P") && !(current.Mode == "B") || current.ProFormaRefNbr != null)
      {
        nullable = current.FromDate;
        DateTime? toDate = current.ToDate;
        if ((nullable.HasValue & toDate.HasValue ? (nullable.GetValueOrDefault() > toDate.GetValueOrDefault() ? 1 : 0) : 0) == 0)
          return;
        throw new Exception("End Date cannot be earlier then Start Date");
      }
    }
label_5:
    throw new Exception("Provide value into Paramaters");
  }

  /// <summary>
  /// Creates pdf document based on the input list. PDF is generated with the list of invoice attachments listed in the report.
  /// </summary>
  /// <param name="reportBytes">The input List of type byte[] contains info related to the report PM650000 (Substantial Billing Report).</param>
  /// <returns>pdf document in byte array format</returns>
  public byte[] MergePDFs(List<byte[]> reportBytes)
  {
    HashSet<Guid> guidSet = new HashSet<Guid>();
    PdfDocument pdfDocument = new PdfDocument();
    PdfPage page1 = (PdfPage) null;
    foreach (PdfPage page2 in PdfReader.Open((Stream) new MemoryStream(reportBytes[0]), (PdfDocumentOpenMode) 1).Pages)
      page1 = pdfDocument.AddPage(page2);
    pdfDocument.Outlines.Add("Report Summary", pdfDocument.Pages[0], true, (PdfOutlineStyle) 2, XColors.DarkBlue);
    PreferencesEmail preferencesEmail = PXResultset<PreferencesEmail>.op_Implicit(PXSelectBase<PreferencesEmail, PXSelect<PreferencesEmail>.Config>.Select((PXGraph) this, Array.Empty<object>()));
    string str1;
    if (preferencesEmail == null)
    {
      str1 = (string) null;
    }
    else
    {
      string notificationSiteUrl = preferencesEmail.NotificationSiteUrl;
      if (notificationSiteUrl == null)
        str1 = (string) null;
      else
        str1 = notificationSiteUrl.TrimEnd('/');
    }
    string companyName = ((PXGraph) this).Accessinfo.CompanyName;
    string str2 = $"{str1}/(W(2))/Frames/GetFile.ashx?CompanyID={companyName}&fileID=";
    Dictionary<string, List<UploadFile>> dictionary = new Dictionary<string, List<UploadFile>>();
    foreach (PXResult<PMTran, PMAccountGroup, PMRegister, PX.Objects.AP.APRegister, CAAdj, CASplit, PX.Objects.GL.Batch, PX.Objects.GL.GLTran, NoteDoc, UploadFile> pxResult in ((PXSelectBase<PMTran>) this.AttachmentsView).Select(Array.Empty<object>()))
    {
      PMRegister pmRegister = PXResult<PMTran, PMAccountGroup, PMRegister, PX.Objects.AP.APRegister, CAAdj, CASplit, PX.Objects.GL.Batch, PX.Objects.GL.GLTran, NoteDoc, UploadFile>.op_Implicit(pxResult);
      PMTran pmTran = PXResult<PMTran, PMAccountGroup, PMRegister, PX.Objects.AP.APRegister, CAAdj, CASplit, PX.Objects.GL.Batch, PX.Objects.GL.GLTran, NoteDoc, UploadFile>.op_Implicit(pxResult);
      string tranType = pmTran.TranType;
      UploadFile uploadFile = PXResult<PMTran, PMAccountGroup, PMRegister, PX.Objects.AP.APRegister, CAAdj, CASplit, PX.Objects.GL.Batch, PX.Objects.GL.GLTran, NoteDoc, UploadFile>.op_Implicit(pxResult);
      if (!string.IsNullOrEmpty(pmTran.RefNbr) || !string.IsNullOrEmpty(pmTran.OrigRefNbr))
      {
        string extension = Path.GetExtension(uploadFile.Name);
        if ((string.Equals(extension, ".pdf", StringComparison.OrdinalIgnoreCase) || SubstantiatedBilling.ValidImageExtensions.Contains(extension)) && guidSet.Add(uploadFile.FileID.GetValueOrDefault()))
        {
          string documentName = this.GetDocumentName(pmTran, pmRegister);
          List<UploadFile> uploadFileList;
          if (!dictionary.TryGetValue(documentName, out uploadFileList))
          {
            uploadFileList = new List<UploadFile>();
            dictionary[documentName] = uploadFileList;
          }
          uploadFileList.Add(uploadFile);
        }
      }
    }
    foreach (string key in (IEnumerable<string>) dictionary.Keys.OrderBy<string, string>((Func<string, string>) (key => key), (IComparer<string>) StringComparer.OrdinalIgnoreCase))
    {
      PdfOutline pdfOutline = pdfDocument.Outlines.Add(key, page1, true, (PdfOutlineStyle) 2, XColors.DarkBlue);
      foreach (UploadFile uploadFile in (IEnumerable<UploadFile>) dictionary[key].OrderBy<UploadFile, string>((Func<UploadFile, string>) (file => file.Name), (IComparer<string>) StringComparer.OrdinalIgnoreCase))
      {
        UploadFileRevision uploadFileRevision = PXResultset<UploadFileRevision>.op_Implicit(PXSelectBase<UploadFileRevision, PXViewOf<UploadFileRevision>.BasedOn<SelectFromBase<UploadFileRevision, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<UploadFileRevision.fileID, Equal<P.AsGuid>>>>>.And<BqlOperand<UploadFileRevision.fileRevisionID, IBqlInt>.IsEqual<P.AsInt>>>>.ReadOnly.Config>.Select((PXGraph) this, new object[2]
        {
          (object) uploadFile.FileID,
          (object) uploadFile.LastRevisionID
        }));
        if (uploadFileRevision != null)
        {
          if (string.Equals(Path.GetExtension(uploadFile.Name), ".pdf", StringComparison.OrdinalIgnoreCase))
          {
            try
            {
              foreach (PdfPage page3 in PdfReader.Open((Stream) new MemoryStream(uploadFileRevision.Data), (PdfDocumentOpenMode) 1).Pages)
                page1 = pdfDocument.AddPage(page3);
            }
            catch
            {
              this.AddLink(pdfDocument.AddPage(), "The PDF file that is available by the following link has a specific structure and cannot be displayed in this report:", uploadFile.Name, str2 + uploadFile.FileID.ToString());
            }
          }
          else
          {
            page1 = pdfDocument.AddPage();
            this.AddImage(page1, uploadFileRevision.Data);
          }
          pdfOutline.Outlines.Add(uploadFile.Name ?? "", page1, true);
        }
      }
    }
    using (MemoryStream memoryStream = new MemoryStream())
    {
      pdfDocument.Save((Stream) memoryStream, false);
      return memoryStream.ToArray();
    }
  }

  private string GetDocumentName(PMTran pmTran, PMRegister pmRegister)
  {
    if (pmTran == null)
      return string.Empty;
    string str1 = pmTran.TranType == "PM" ? pmTran.RefNbr : pmTran.OrigRefNbr;
    if (pmTran.TranType == "GL" && string.IsNullOrEmpty(str1))
      return "GL " + pmTran.BatchNbr;
    string str2;
    if (this.OrigDocTypes.TryGetValue(pmRegister?.OrigDocType ?? string.Empty, out str2))
      return $"{str2} {str1}";
    if (pmTran.TranType == "CA")
    {
      int index = Array.IndexOf<string>(CATranType.Values, pmTran.OrigTranType ?? string.Empty);
      if (index >= 0)
        str2 = CATranType.Labels[index];
    }
    else
    {
      string str3;
      str2 = !(pmTran.TranType == "AP") || !this.APOrigDocTypes.TryGetValue(pmTran.OrigTranType ?? string.Empty, out str3) ? string.Empty : str3;
    }
    return !string.IsNullOrEmpty(str2) ? $"{str2} {str1}" : str1;
  }

  private void AddLink(PdfPage page, string message, string name, string url)
  {
    XGraphics xgraphics = XGraphics.FromPdfPage(page);
    XRect xrect;
    // ISSUE: explicit constructor call
    ((XRect) ref xrect).\u002Ector(new XPoint(30.0, 30.0), new XSize(600.0, 30.0));
    PdfRectangle pdfRectangle = new PdfRectangle(xgraphics.Transformer.WorldToDefaultPage(xrect));
    page.AddWebLink(pdfRectangle, url);
    xgraphics.DrawString(message, new XFont("Calibri", 10.0, (XFontStyle) 0), (XBrush) XBrushes.Red, xrect, XStringFormats.TopLeft);
    xgraphics.DrawString(name, new XFont("Calibri", 10.0, (XFontStyle) 4), (XBrush) XBrushes.Blue, xrect, XStringFormats.BottomLeft);
  }

  private void AddImage(PdfPage page, byte[] imageData)
  {
    XGraphics xgraphics = XGraphics.FromPdfPage(page);
    XImage ximage1 = XImage.FromStream((Stream) new MemoryStream(imageData));
    double num1 = Math.Min(XUnit.op_Implicit(page.Width) / (double) ximage1.PixelWidth, XUnit.op_Implicit(page.Height) / (double) ximage1.PixelHeight);
    XImage ximage2 = ximage1;
    double num2 = (double) ximage1.PixelWidth * num1;
    double num3 = (double) ximage1.PixelHeight * num1;
    xgraphics.DrawImage(ximage2, 0.0, 0.0, num2, num3);
  }

  public class ReflectionMethods
  {
    public static void SetPrivatePropertyValue<T>(T obj, string propertyName, object newValue)
    {
      foreach (FieldInfo field in obj.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic))
      {
        if (field.Name.ToLower().Contains(propertyName.ToLower()))
        {
          field.SetValue((object) obj, newValue);
          break;
        }
      }
    }
  }

  [PXHidden]
  [Serializable]
  public class Substantial : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    /// <summary>Project for which the report is displayed</summary>
    [PXDefault]
    [Project(typeof (Where<PMProject.baseType, Equal<CTPRType.project>, And<PMProject.nonProject, NotEqual<True>>>), WarnIfCompleted = false)]
    public virtual int? ProjectID { get; set; }

    /// <summary>Transaction selection mode</summary>
    [PXString(1, IsFixed = true)]
    [PXUIField(DisplayName = "Select Transactions By", Required = true)]
    [SubstantiatedBillingMode.StringList]
    [PXUnboundDefault("D")]
    public virtual string Mode { get; set; }

    /// <summary>Start of the period</summary>
    [PXDBDate]
    [PXUIField]
    public virtual DateTime? FromDate { get; set; }

    /// <summary>End of the period</summary>
    [PXDBDate]
    [PXDefault(typeof (AccessInfo.businessDate))]
    [PXUIField(DisplayName = "End Date")]
    public virtual DateTime? ToDate { get; set; }

    /// <summary>The reference number of the pro forma invoice</summary>
    [PXString(15)]
    [PXUIField]
    [PXSelector(typeof (FbqlSelect<SelectFromBase<PMProforma, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMProforma.projectID, Equal<BqlField<SubstantiatedBilling.Substantial.projectID, IBqlInt>.FromCurrent>>>>>.And<BqlOperand<PMProforma.corrected, IBqlBool>.IsNotEqual<True>>>.Order<By<BqlField<PMProforma.refNbr, IBqlString>.Desc>>, PMProforma>.SearchFor<PMProforma.refNbr>), DescriptionField = typeof (PMProforma.description))]
    public virtual string ProFormaRefNbr { get; set; }

    /// <summary>
    /// Whether non-billable transactions should be included in the report
    /// </summary>
    [PXBool]
    [PXUIField(DisplayName = "Include Non-Billable Transactions")]
    [PXUnboundDefault(true)]
    public virtual bool? IncludeNonBillable { get; set; }

    [PXBool]
    [PXUIField(DisplayName = "Copy Line Attachments to Report")]
    [PXUnboundDefault(true)]
    public virtual bool? IncludeLineAttachments { get; set; }

    public abstract class projectID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      SubstantiatedBilling.Substantial.projectID>
    {
    }

    public abstract class mode : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      SubstantiatedBilling.Substantial.mode>
    {
    }

    public abstract class fromDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      SubstantiatedBilling.Substantial.fromDate>
    {
    }

    public abstract class toDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      SubstantiatedBilling.Substantial.toDate>
    {
    }

    public abstract class proFormaRefNbr : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      SubstantiatedBilling.Substantial.proFormaRefNbr>
    {
    }

    public abstract class includeNonBillable : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      SubstantiatedBilling.Substantial.includeNonBillable>
    {
    }

    public abstract class includeLineAttachments : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      SubstantiatedBilling.Substantial.includeLineAttachments>
    {
    }
  }
}
