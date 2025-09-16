// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.DAC.ReportParameters.VendorReportParametersVisibilityRestriction
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AP;
using PX.Objects.CS;

#nullable disable
namespace PX.Objects.Common.DAC.ReportParameters;

public sealed class VendorReportParametersVisibilityRestriction : 
  PXCacheExtension<VendorReportParameters>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.visibilityRestriction>();

  [PXMergeAttributes]
  [RestrictVendorClassByUserBranches]
  public string VendorClassID { get; set; }

  [PXMergeAttributes]
  [RestrictVendorByUserBranches]
  public int? VendorID { get; set; }

  [PXMergeAttributes]
  [RestrictVendorByUserBranches]
  public int? VendorActiveID { get; set; }

  [PXMergeAttributes]
  [RestrictVendorByUserBranches]
  public int? VendorIDPOReceipt { get; set; }

  [PXMergeAttributes]
  [RestrictVendorByUserBranches]
  public int? VendorIDNonEmployeeActive { get; set; }
}
