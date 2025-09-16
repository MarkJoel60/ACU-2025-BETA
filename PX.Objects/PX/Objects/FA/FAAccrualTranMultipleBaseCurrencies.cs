// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.FAAccrualTranMultipleBaseCurrencies
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using System;

#nullable disable
namespace PX.Objects.FA;

public sealed class FAAccrualTranMultipleBaseCurrencies : PXCacheExtension<FAAccrualTran>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.multipleBaseCurrencies>();

  [PXMergeAttributes]
  [PXRestrictor(typeof (Where<BqlOperand<PX.Objects.GL.Branch.baseCuryID, IBqlString>.IsEqual<BqlField<GLTranFilter.branchBaseCuryID, IBqlString>.FromCurrent>>), "The {0} base currency of the {1} branch differs from the {2} base currency of the transaction's {3} branch.", new Type[] {typeof (PX.Objects.GL.Branch.baseCuryID), typeof (PX.Objects.GL.Branch.branchCD), typeof (GLTranFilter.branchBaseCuryID), typeof (GLTranFilter.branchID)})]
  public int? BranchID { get; set; }
}
