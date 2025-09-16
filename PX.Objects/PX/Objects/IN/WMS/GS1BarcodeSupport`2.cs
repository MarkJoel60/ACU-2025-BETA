// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.WMS.GS1BarcodeSupport`2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.BarcodeProcessing;
using PX.Common;
using PX.Common.GS1;
using PX.Data;
using System;

#nullable disable
namespace PX.Objects.IN.WMS;

public abstract class GS1BarcodeSupport<TScanBasis, TScanGraph> : 
  CompositeBarcodeSupport<TScanBasis, TScanGraph, EAN128BarcodeParser>
  where TScanBasis : BarcodeDrivenStateMachine<TScanBasis, TScanGraph>
  where TScanGraph : PXGraph, new()
{
  public PXSetupOptional<GS1UOMSetup> gs1Setup;

  protected virtual string GetUOMOf(string code)
  {
    AI ai;
    return !Parser.TryGetAI(code, ref ai) ? (string) null : ((PXSelectBase<GS1UOMSetup>) this.gs1Setup).Current.GetUOMOf(ai);
  }

  protected virtual void ReportNoneWasApplied()
  {
    this.Basis.ReportError("The values in the GS1 barcode are not valid for the current step.", Array.Empty<object>());
  }

  [PXLocalizable]
  public abstract class Msg
  {
    public const string GS1BarcodeWasNotHandled = "The values in the GS1 barcode are not valid for the current step.";
  }
}
