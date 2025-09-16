// Decompiled with JetBrains decompiler
// Type: PX.CloudServices.Models
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System.Collections.Generic;

#nullable disable
namespace PX.CloudServices;

[PXInternalUseOnly]
public static class Models
{
  public static string ReceiptModel = "afr-receipt-prebuilt-01";
  public static string ReceiptModel3 = "afr-receipt-prebuilt-03";
  public static string ApInvoicesModel = "afr-invoice-prebuilt-01";
  public static string ApInvoicesModel3 = "afr-invoice-prebuilt-03";
  public static string ApInvoicesModel4 = "afr-invoice-prebuilt-04";
  public const string APInvoiceDocumentType = "INV";
  public const string ReceiptDocumentType = "RCT";
  private const string APDocRecognitionFeature = "PX.Objects.CS.FeaturesSet+apDocumentRecognition";
  private const string ImageRecognitionFeature = "PX.Objects.CS.FeaturesSet+imageRecognition";
  public static readonly Dictionary<string, Model> KnownModels = new Dictionary<string, Model>()
  {
    {
      Models.ApInvoicesModel,
      new Model()
      {
        Feature = "PX.Objects.CS.FeaturesSet+apDocumentRecognition",
        Name = Models.ApInvoicesModel,
        DocumentType = "INV",
        DocumentTypeLabel = "AP Document"
      }
    },
    {
      Models.ApInvoicesModel3,
      new Model()
      {
        Feature = "PX.Objects.CS.FeaturesSet+apDocumentRecognition",
        Name = Models.ApInvoicesModel3,
        DocumentType = "INV",
        DocumentTypeLabel = "AP Document"
      }
    },
    {
      Models.ApInvoicesModel4,
      new Model()
      {
        Feature = "PX.Objects.CS.FeaturesSet+apDocumentRecognition",
        Name = Models.ApInvoicesModel4,
        DocumentType = "INV",
        DocumentTypeLabel = "AP Document"
      }
    },
    {
      Models.ReceiptModel,
      new Model()
      {
        Feature = "PX.Objects.CS.FeaturesSet+imageRecognition",
        Name = Models.ReceiptModel,
        DocumentType = "RCT",
        DocumentTypeLabel = "Expense Receipt"
      }
    },
    {
      Models.ReceiptModel3,
      new Model()
      {
        Feature = "PX.Objects.CS.FeaturesSet+imageRecognition",
        Name = Models.ReceiptModel3,
        DocumentType = "RCT",
        DocumentTypeLabel = "Expense Receipt"
      }
    }
  };
}
