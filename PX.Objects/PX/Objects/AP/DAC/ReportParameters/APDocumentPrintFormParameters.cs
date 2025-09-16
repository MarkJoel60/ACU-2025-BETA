// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.DAC.ReportParameters.APDocumentPrintFormParameters
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System.Collections.Generic;

#nullable enable
namespace PX.Objects.AP.DAC.ReportParameters;

[PXHidden]
public class APDocumentPrintFormParameters : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [APDocumentPrintFormParameters.paymentDocType.List]
  [PXDBString(3)]
  [PXUIField(DisplayName = "Doc Type", Visibility = PXUIVisibility.SelectorVisible)]
  public 
  #nullable disable
  string PaymentDocType { get; set; }

  public abstract class paymentDocType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APDocumentPrintFormParameters.paymentDocType>
  {
    public class ListAttribute : PXStringListAttribute
    {
      public ListAttribute()
        : base(APDocumentPrintFormParameters.paymentDocType.ListAttribute.GetAllowedValues(), APDocumentPrintFormParameters.paymentDocType.ListAttribute.GetAllowedLabels())
      {
      }

      public static string[] GetAllowedValues()
      {
        return new List<string>()
        {
          "CHK",
          "VCK",
          "PPM",
          "REF",
          "VRF",
          "QCK",
          "VQC",
          "RQC"
        }.ToArray();
      }

      public static string[] GetAllowedLabels()
      {
        return new List<string>()
        {
          "Payment",
          "Voided Payment",
          "Prepayment",
          "Refund",
          "Voided Refund",
          "Cash Purchase",
          "Voided Cash Purchase",
          "Cash Return"
        }.ToArray();
      }
    }
  }
}
