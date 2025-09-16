// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CAAdjMultipleBaseCurrenciesRestriction
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using System;

#nullable disable
namespace PX.Objects.CA;

public sealed class CAAdjMultipleBaseCurrenciesRestriction : PXCacheExtension<CAAdj>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.multipleBaseCurrencies>();

  [PXMergeAttributes]
  [PXRestrictor(typeof (Where<CashAccount.baseCuryID, Equal<Current<AccessInfo.baseCuryID>>>), null, new Type[] {})]
  public int? CashAccountID { get; set; }
}
