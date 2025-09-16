// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.BAccountCRMCacheExtensionCRMFeatureOff
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.CS;
using System;

#nullable disable
namespace PX.Objects.CR;

/// <exclude />
[CRCacheIndependentPrimaryGraphList(new System.Type[] {typeof (CustomerMaint), typeof (CustomerMaint), typeof (CustomerMaint), typeof (VendorMaint), typeof (VendorMaint), typeof (VendorMaint), typeof (CustomerMaint), typeof (CustomerMaint)}, new System.Type[] {typeof (Select<BAccount, Where<BAccount.bAccountID, Equal<Current<BAccount.bAccountID>>, And<Current<BAccount.viewInCrm>, Equal<True>>>>), typeof (Select<Customer, Where<Customer.bAccountID, Equal<Current<BAccount.bAccountID>>>>), typeof (Select<Customer, Where<Customer.bAccountID, Equal<Current<BAccountR.bAccountID>>>>), typeof (Select<VendorR, Where<VendorR.bAccountID, Equal<Current<BAccount.bAccountID>>>>), typeof (Select<PX.Objects.AP.Vendor, Where<PX.Objects.AP.Vendor.bAccountID, Equal<Current<BAccountR.bAccountID>>>>), typeof (Where<BAccountR.bAccountID, Less<Zero>, And<BAccountR.type, Equal<BAccountType.vendorType>>>), typeof (Where<BAccountR.bAccountID, Less<Zero>, And<BAccountR.type, Equal<BAccountType.customerType>>>), typeof (Select<BAccount, Where2<Where<BAccount.type, Equal<BAccountType.prospectType>, Or<BAccount.type, Equal<BAccountType.customerType>, Or<BAccount.type, Equal<BAccountType.vendorType>, Or<BAccount.type, Equal<BAccountType.combinedType>>>>>, And<Where<BAccount.bAccountID, Equal<Current<BAccount.bAccountID>>, Or<Current<BAccount.bAccountID>, Less<Zero>>>>>>)})]
[PXHidden]
[Serializable]
public sealed class BAccountCRMCacheExtensionCRMFeatureOff : PXCacheExtension<BAccountCRM>
{
  public static bool IsActive() => !PXAccess.FeatureInstalled<FeaturesSet.customerModule>();
}
