// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.JournalWithSubEntryMultipleBaseCurrencies
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CR;
using PX.Objects.CS;

#nullable disable
namespace PX.Objects.GL;

public sealed class JournalWithSubEntryMultipleBaseCurrencies : PXGraphExtension<JournalWithSubEntry>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.multipleBaseCurrencies>();

  [PXRestrictor(typeof (Where<BAccountR.baseCuryID, EqualBaseCuryID<BqlField<GLDocBatch.branchID, IBqlInt>.FromCurrent>>), "", new System.Type[] {}, SuppressVerify = false)]
  [PXMergeAttributes]
  public void _(PX.Data.Events.CacheAttached<GLTranDoc.bAccountID> e)
  {
  }
}
