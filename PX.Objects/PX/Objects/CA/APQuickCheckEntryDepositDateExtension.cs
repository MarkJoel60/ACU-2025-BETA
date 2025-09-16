// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.APQuickCheckEntryDepositDateExtension
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AP;
using PX.Objects.AP.Standalone;

#nullable disable
namespace PX.Objects.CA;

public class APQuickCheckEntryDepositDateExtension : PXGraphExtension<APQuickCheckEntry>
{
  public static bool IsActive() => true;

  [PXMergeAttributes]
  [PXSelector(typeof (Search<CADeposit.refNbr, Where<CADeposit.tranType, Equal<Current<APQuickCheck.depositType>>>>))]
  protected virtual void _(PX.Data.Events.CacheAttached<APQuickCheck.depositNbr> e)
  {
  }

  [PXMergeAttributes]
  [PXFormula(typeof (Selector<APQuickCheck.depositNbr, CADeposit.tranDate>))]
  protected virtual void _(PX.Data.Events.CacheAttached<APQuickCheck.depositDate> e)
  {
  }
}
