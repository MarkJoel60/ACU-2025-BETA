// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.TransferFilterMultipleBaseCurrencies
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using System;

#nullable disable
namespace PX.Objects.FA;

public sealed class TransferFilterMultipleBaseCurrencies : 
  PXCacheExtension<TransferProcess.TransferFilter>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.multipleBaseCurrencies>();

  [PXMergeAttributes]
  [PXRestrictor(typeof (Where<PX.Objects.GL.Branch.baseCuryID, EqualBaseCuryID<Current2<TransferProcess.TransferFilter.branchFrom>>, Or<Current2<TransferProcess.TransferFilter.branchFrom>, IsNull>>), "The {1} branch cannot be selected. The {0} base currency of the {1} destination branch differs from the base currency of the {2} branch from which the assets are transferred.", new Type[] {typeof (PX.Objects.GL.Branch.baseCuryID), typeof (PX.Objects.GL.Branch.branchCD), typeof (TransferProcess.TransferFilter.branchFrom)})]
  public int? BranchTo { get; set; }

  [PXMergeAttributes]
  [PXRestrictor(typeof (Where<PX.Objects.GL.Branch.baseCuryID, EqualBaseCuryID<Current2<TransferProcess.TransferFilter.branchTo>>, Or<Current2<TransferProcess.TransferFilter.branchTo>, IsNull>>), "The {1} branch cannot be selected. The base currency of the {2} destination branch differs from the {0} base currency of the {1} branch from which the assets are transferred.", new Type[] {typeof (PX.Objects.GL.Branch.baseCuryID), typeof (PX.Objects.GL.Branch.branchCD), typeof (TransferProcess.TransferFilter.branchTo)})]
  public int? BranchFrom { get; set; }
}
