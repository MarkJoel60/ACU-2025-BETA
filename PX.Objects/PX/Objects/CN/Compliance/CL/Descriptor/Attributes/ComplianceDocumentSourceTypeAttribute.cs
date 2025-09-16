// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Compliance.CL.Descriptor.Attributes.ComplianceDocumentSourceTypeAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.CN.Compliance.CL.Descriptor.Attributes;

public class ComplianceDocumentSourceTypeAttribute : PXStringListAttribute
{
  public const string ApBill = "AP Bill";
  public const string PoSub = "PO/Sub";
  public const string Project = "Project";
  public const string Customer = "Customer";

  public ComplianceDocumentSourceTypeAttribute()
    : base(new string[4]
    {
      "AP Bill",
      "PO/Sub",
      nameof (Customer),
      nameof (Project)
    }, new string[4]
    {
      "AP Bill",
      "PO/Sub",
      nameof (Customer),
      nameof (Project)
    })
  {
  }
}
