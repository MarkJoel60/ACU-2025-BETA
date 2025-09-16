// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.WMS.AllWMSRedirects
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.BarcodeProcessing;
using PX.Data;
using PX.Objects.PO.WMS;
using PX.Objects.SO.WMS;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.IN.WMS;

public static class AllWMSRedirects
{
  public static IEnumerable<ScanRedirect<TScanExt>> CreateFor<TScanExt>() where TScanExt : PXGraphExtension, IBarcodeDrivenStateMachine
  {
    return (IEnumerable<ScanRedirect<TScanExt>>) new ScanRedirect<TScanExt>[14]
    {
      (ScanRedirect<TScanExt>) new StoragePlaceLookup.RedirectFrom<TScanExt>(),
      (ScanRedirect<TScanExt>) new InventoryItemLookup.RedirectFrom<TScanExt>(),
      (ScanRedirect<TScanExt>) new INScanIssue.RedirectFrom<TScanExt>(),
      (ScanRedirect<TScanExt>) new INScanReceive.RedirectFrom<TScanExt>(),
      (ScanRedirect<TScanExt>) new INScanTransfer.RedirectFrom<TScanExt>(),
      (ScanRedirect<TScanExt>) new INScanCount.RedirectFrom<TScanExt>(),
      (ScanRedirect<TScanExt>) new ReceivePutAway.ReceiveMode.RedirectFrom<TScanExt>(),
      (ScanRedirect<TScanExt>) new ReceivePutAway.ReceiveTransferMode.RedirectFrom<TScanExt>(),
      (ScanRedirect<TScanExt>) new ReceivePutAway.ReturnMode.RedirectFrom<TScanExt>(),
      (ScanRedirect<TScanExt>) new ReceivePutAway.PutAwayMode.RedirectFrom<TScanExt>(),
      (ScanRedirect<TScanExt>) new PickPackShip.PickMode.RedirectFrom<TScanExt>(),
      (ScanRedirect<TScanExt>) new PickPackShip.PackMode.RedirectFrom<TScanExt>(),
      (ScanRedirect<TScanExt>) new PickPackShip.ShipMode.RedirectFrom<TScanExt>(),
      (ScanRedirect<TScanExt>) new PickPackShip.ReturnMode.RedirectFrom<TScanExt>()
    };
  }
}
