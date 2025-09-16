// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.InvoiceRecognition.DAC.APPOLinkStatus
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.AP.InvoiceRecognition.DAC;

public class APPOLinkStatus
{
  public static readonly string[] Values = new string[4]
  {
    "N",
    "L",
    "M",
    "P"
  };
  public static readonly string[] Labels = new string[4]
  {
    "The line is not linked to a purchase order line. You can click the Link PO Line button to select a purchase order line.",
    nameof (Linked),
    "Multiple purchase order lines have been found. You can click the Link PO Line button to select a purchase order line.",
    "Multiple purchase receipt lines have been found. You can click the Link PO Line button to select a purchase receipt line."
  };
  public const string NotLinked = "N";
  public const string Linked = "L";
  public const string MultiplePOLinesFound = "M";
  public const string MultiplePRLinesFound = "P";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(APPOLinkStatus.Values, APPOLinkStatus.Labels)
    {
    }
  }
}
