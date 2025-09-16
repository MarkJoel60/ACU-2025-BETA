// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.LocationAPPaymentInfoMultipleBaseCurrenciesRestriction
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CR;

#nullable disable
namespace PX.Objects.AP;

public sealed class LocationAPPaymentInfoMultipleBaseCurrenciesRestriction : 
  PXCacheExtension<LocationAPPaymentInfo>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.multipleBaseCurrencies>();

  [PXMergeAttributes(Method = MergeMethod.Append)]
  [PXRestrictor(typeof (Where<PX.Objects.CA.CashAccount.baseCuryID, Equal<Current<BAccountR.baseCuryID>>>), "The cash account must have the same base currency as the vendor base currency.", new System.Type[] {}, SuppressVerify = true)]
  public int? VCashAccountID { get; set; }
}
