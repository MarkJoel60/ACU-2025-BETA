// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.InvoicingProcessStepGroupShared
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.FS;

public class InvoicingProcessStepGroupShared
{
  public IInvoiceProcessGraph ProcessGraph;
  public IInvoiceGraph InvoiceGraph;
  public PXCache<FSCreatedDoc> CacheFSCreatedDoc;
  public PostInfoEntry PostInfoEntryGraph;
  public PXCache<FSPostDet> CacheFSPostDet;
  public ServiceOrderEntry ServiceOrderGraph;

  public virtual void Initialize(string targetScreen, string billingBy)
  {
    if (this.ProcessGraph == null)
      this.ProcessGraph = this.CreateInvoiceProcessGraph(billingBy);
    else
      this.ProcessGraph.Clear((PXClearOption) 3);
    if (this.InvoiceGraph == null)
      this.InvoiceGraph = InvoiceHelper.CreateInvoiceGraph(targetScreen);
    else
      this.InvoiceGraph.Clear();
    if (this.ServiceOrderGraph == null)
      this.ServiceOrderGraph = PXGraph.CreateInstance<ServiceOrderEntry>();
    else
      ((PXGraph) this.ServiceOrderGraph).Clear();
    if (this.CacheFSCreatedDoc == null)
      this.CacheFSCreatedDoc = new PXCache<FSCreatedDoc>(this.ProcessGraph.GetGraph());
    else
      ((PXCache) this.CacheFSCreatedDoc).Clear();
    if (this.PostInfoEntryGraph == null)
      this.PostInfoEntryGraph = PXGraph.CreateInstance<PostInfoEntry>();
    else
      ((PXGraph) this.PostInfoEntryGraph).Clear((PXClearOption) 3);
    if (this.CacheFSPostDet == null)
      this.CacheFSPostDet = new PXCache<FSPostDet>((PXGraph) this.PostInfoEntryGraph);
    else
      ((PXCache) this.CacheFSPostDet).Clear();
  }

  public virtual IInvoiceProcessGraph CreateInvoiceProcessGraph(string billingBy)
  {
    switch (billingBy)
    {
      case "SO":
        return (IInvoiceProcessGraph) PXGraph.CreateInstance<CreateInvoiceByServiceOrderPost>();
      case "AP":
        return (IInvoiceProcessGraph) PXGraph.CreateInstance<CreateInvoiceByAppointmentPost>();
      default:
        throw new NotImplementedException();
    }
  }

  public virtual void Clear()
  {
    if (this.ProcessGraph != null)
      this.ProcessGraph.Clear((PXClearOption) 3);
    if (this.InvoiceGraph != null)
      this.InvoiceGraph.Clear();
    if (this.CacheFSCreatedDoc != null)
      ((PXCache) this.CacheFSCreatedDoc).Clear();
    if (this.PostInfoEntryGraph != null)
      ((PXGraph) this.PostInfoEntryGraph).Clear((PXClearOption) 3);
    if (this.CacheFSPostDet == null)
      return;
    ((PXCache) this.CacheFSPostDet).Clear();
  }

  public virtual void Dispose()
  {
    this.Clear();
    this.ProcessGraph = (IInvoiceProcessGraph) null;
    this.InvoiceGraph = (IInvoiceGraph) null;
    this.CacheFSCreatedDoc = (PXCache<FSCreatedDoc>) null;
    this.PostInfoEntryGraph = (PostInfoEntry) null;
    this.CacheFSPostDet = (PXCache<FSPostDet>) null;
  }
}
