// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Subcontracts.SC.Loggers.SubcontractLogger
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.Common.Tools;
using PX.Objects.PO;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.CN.Subcontracts.SC.Loggers;

public class SubcontractLogger
{
  private readonly POOrder subcontract;
  private readonly List<POLine> subcontractLines;

  public SubcontractLogger(POOrder subcontract, List<POLine> subcontractLines)
  {
    this.subcontract = subcontract;
    this.subcontractLines = subcontractLines;
  }

  public void TraceFullInformation()
  {
    this.TraceDocumentInformation();
    this.TraceTransactionsInformation();
  }

  private void TraceDocumentInformation()
  {
    PXTrace.WriteInformation(((object) this.subcontract).Dump());
  }

  private void TraceTransactionsInformation()
  {
    foreach (object subcontractLine in this.subcontractLines)
      PXTrace.WriteInformation(subcontractLine.Dump());
  }
}
