// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.CCPaymentProcessing.Common.DocDateWarningDisplay
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.AR.CCPaymentProcessing.Common;

[Obsolete("This class has been deprecated and will be removed in the later Acumatica versions.")]
public sealed class DocDateWarningDisplay : IPXCustomInfo
{
  private readonly DateTime _NewDate;

  public void Complete(PXLongRunStatus status, PXGraph graph)
  {
    if (status != 2 || !(graph is ARPaymentEntry))
      return;
    // ISSUE: method pointer
    graph.RowSelected.AddHandler<ARPayment>(new PXRowSelected((object) this, __methodptr(\u003CComplete\u003Eb__0_0)));
  }

  public DocDateWarningDisplay(DateTime newDate) => this._NewDate = newDate;
}
