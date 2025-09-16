// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.DocumentTypeField
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.CR;

public class DocumentTypeField
{
  public const string CashSales = "CS";
  public const string ChecksAndPayments = "CP";
  public const string InvoicesAndMemo = "IM";
  public const string Invoices = "IN";
  public const string Opportunities = "OP";
  public const string Projects = "PR";
  public const string ProjectsQuote = "PQ";
  public const string ProFormaInvoice = "PFI";
  public const string PurchaseOrder = "PO";
  public const string SalesOrder = "SO";
  public const string SalesQuote = "SQ";
  public const string Shipments = "SH";

  public enum SetOfValues
  {
    SORelatedDocumentTypes,
    PORelatedDocumentTypes,
    ARRelatedDocumentTypes,
    APRelatedDocumentTypes,
    CRRelatedDocumentTypes,
    PMRelatedDocumentTypes,
  }

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute(DocumentTypeField.SetOfValues setOfValues)
    {
      switch (setOfValues)
      {
        case DocumentTypeField.SetOfValues.SORelatedDocumentTypes:
          this._AllowedValues = new string[3]
          {
            "SO",
            "SH",
            "IN"
          };
          this._AllowedLabels = new string[3]
          {
            "Sales Orders",
            "Shipments",
            "SO Invoices"
          };
          break;
        case DocumentTypeField.SetOfValues.PORelatedDocumentTypes:
          this._AllowedValues = new string[1]{ "PO" };
          this._AllowedLabels = new string[1]
          {
            "Purchase Orders"
          };
          break;
        case DocumentTypeField.SetOfValues.ARRelatedDocumentTypes:
          this._AllowedValues = new string[2]{ "CS", "IM" };
          this._AllowedLabels = new string[2]
          {
            "Cash Sales",
            "Invoices and Memos"
          };
          break;
        case DocumentTypeField.SetOfValues.APRelatedDocumentTypes:
          this._AllowedValues = new string[1]{ "CP" };
          this._AllowedLabels = new string[1]
          {
            "Checks and Payments"
          };
          break;
        case DocumentTypeField.SetOfValues.CRRelatedDocumentTypes:
          this._AllowedValues = new string[3]
          {
            "OP",
            "SQ",
            "PQ"
          };
          this._AllowedLabels = new string[3]
          {
            "Opportunities",
            "Sales Quotes",
            "Project Quotes"
          };
          break;
        case DocumentTypeField.SetOfValues.PMRelatedDocumentTypes:
          this._AllowedValues = new string[3]
          {
            "PR",
            "PQ",
            "PFI"
          };
          this._AllowedLabels = new string[3]
          {
            "Projects",
            "Project Quotes",
            "Pro Forma Invoices"
          };
          break;
      }
    }
  }
}
