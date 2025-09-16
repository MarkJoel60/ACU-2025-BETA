// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CATranEntryDepositDateExtension
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.CA;

public class CATranEntryDepositDateExtension : PXGraphExtension<CATranEntry>
{
  public static bool IsActive() => true;

  [PXMergeAttributes]
  [PXFormula(typeof (Selector<CAAdj.depositNbr, CADeposit.tranDate>))]
  protected virtual void _(Events.CacheAttached<CAAdj.depositDate> e)
  {
  }
}
