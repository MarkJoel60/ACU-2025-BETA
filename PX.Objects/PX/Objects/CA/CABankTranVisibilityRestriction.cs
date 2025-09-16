// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CABankTranVisibilityRestriction
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.GL;

#nullable disable
namespace PX.Objects.CA;

public sealed class CABankTranVisibilityRestriction : PXCacheExtension<CABankTran>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.visibilityRestriction>();

  [PXMergeAttributes]
  [RestrictCustomerByBranch(typeof (BAccountR.cOrgBAccountID), typeof (Or<Current<CABankTran.origModule>, Equal<BatchModule.moduleAP>>), typeof (AccessInfo.branchID))]
  [RestrictVendorByBranch(typeof (BAccountR.vOrgBAccountID), typeof (Or<Current<CABankTran.origModule>, Equal<BatchModule.moduleAR>>), typeof (AccessInfo.branchID))]
  public int? PayeeBAccountID { get; set; }
}
