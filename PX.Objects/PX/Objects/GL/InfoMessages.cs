// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.InfoMessages
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;

#nullable disable
namespace PX.Objects.GL;

[PXLocalizable]
public static class InfoMessages
{
  public const string SomeTransactionsCannotBeReclassified = "Some transactions that match the specified selection criteria cannot be reclassified. These transactions will not be loaded.";
  public const string NoReclassifiableTransactionsHaveBeenFoundToMatchTheCriteria = "No transactions, for which the reclassification can be performed, have been found to match the specified criteria.";
  public const string NoReclassifiableTransactionsHaveBeenSelected = "No transactions, for which the reclassification can be performed, have been selected.";
  public const string SomeTransactionsOfTheBatchCannotBeReclassified = "Some transactions of the batch cannot be reclassified. These transactions will not be loaded.";
  public const string NoReclassifiableTransactionsHaveBeenFoundInTheBatch = "No transactions, for which the reclassification can be performed, have been found in the batch.";
  public const string TransactionsListedOnTheFormIfAnyWillBeRemoved = "Transactions listed on the form (if any) will be removed. New transactions that match the selection criteria will be loaded. Do you want to continue?";
  public const string ReleasedDocCannotBeDeleted = "Released documents cannot be deleted.";
}
