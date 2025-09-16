// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.ARCashSaleEntryDepositDateExtension
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AR;
using PX.Objects.AR.Standalone;

#nullable disable
namespace PX.Objects.CA;

public class ARCashSaleEntryDepositDateExtension : PXGraphExtension<ARCashSaleEntry>
{
  public static bool IsActive() => true;

  [PXMergeAttributes]
  [PXSelector(typeof (Search<CADeposit.refNbr, Where<CADeposit.tranType, Equal<Current<ARCashSale.depositType>>>>))]
  protected virtual void _(PX.Data.Events.CacheAttached<ARCashSale.depositNbr> e)
  {
  }

  [PXMergeAttributes]
  [PXFormula(typeof (Selector<ARCashSale.depositNbr, CADeposit.tranDate>))]
  protected virtual void _(PX.Data.Events.CacheAttached<ARCashSale.depositDate> e)
  {
  }
}
