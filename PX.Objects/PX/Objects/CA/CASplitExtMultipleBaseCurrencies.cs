// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CASplitExtMultipleBaseCurrencies
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.CR;
using System;

#nullable disable
namespace PX.Objects.CA;

[Obsolete("This item has been deprecated and will be removed in Acumatica ERP 2022 R2.")]
public sealed class CASplitExtMultipleBaseCurrencies : 
  PXCacheExtension<CASplitExtVisibilityRestiriction, CASplitExt>
{
  [Obsolete("This item has been deprecated and will be removed in Acumatica ERP 2022 R2.")]
  public static bool IsActive() => false;

  [Obsolete("This item has been deprecated and will be removed in Acumatica ERP 2022 R2.")]
  [PXMergeAttributes]
  [RestrictCustomerByBranch(typeof (BAccountR.cOrgBAccountID), typeof (PaymentReclassifyProcess.Filter.branchID))]
  [RestrictVendorByBranch(typeof (BAccountR.vOrgBAccountID), typeof (PaymentReclassifyProcess.Filter.branchID))]
  public int? ReferenceID { get; set; }
}
