// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CABankTranMultipleBaseCurrenciesRestriction
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CR;
using PX.Objects.CS;

#nullable disable
namespace PX.Objects.CA;

public sealed class CABankTranMultipleBaseCurrenciesRestriction : PXCacheExtension<CABankTran>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.multipleBaseCurrencies>();

  [PXMergeAttributes]
  [PXRestrictor(typeof (Where<BAccountR.baseCuryID, Equal<Current<CABankTransactionsMaint.Filter.baseCuryID>>, Or<BAccountR.baseCuryID, IsNull>>), "The {0} cash account is associated with the {1} branch whose base currency differs from the base currency of {2} associated with the {3} account.", new System.Type[] {typeof (CABankTransactionsMaint.Filter.cashAccountID), typeof (CashAccount.branchID), typeof (BAccountR.cOrgBAccountID), typeof (BAccountR.bAccountID)})]
  public int? PayeeBAccountID { get; set; }
}
