// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.TaxZoneExtension.ProjectRevenueTaxZoneExtension`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.PM.TaxZoneExtension;

public abstract class ProjectRevenueTaxZoneExtension<T> : PXGraphExtension<T> where T : PXGraph
{
  public PXSelectExtension<PX.Objects.PM.TaxZoneExtension.Document> Document;

  protected abstract DocumentMapping GetDocumentMapping();

  protected virtual void _(
    Events.FieldUpdated<PX.Objects.PM.TaxZoneExtension.Document, PX.Objects.PM.TaxZoneExtension.Document.projectID> e)
  {
    if (object.Equals(((Events.FieldUpdatedBase<Events.FieldUpdated<PX.Objects.PM.TaxZoneExtension.Document, PX.Objects.PM.TaxZoneExtension.Document.projectID>, PX.Objects.PM.TaxZoneExtension.Document, object>) e).OldValue, e.NewValue) && ProjectDefaultAttribute.IsNonProject((int?) e.NewValue))
      return;
    this.SetDefaultShipToAddress(((Events.Event<PXFieldUpdatedEventArgs, Events.FieldUpdated<PX.Objects.PM.TaxZoneExtension.Document, PX.Objects.PM.TaxZoneExtension.Document.projectID>>) e).Cache, e.Row);
  }

  protected abstract void SetDefaultShipToAddress(PXCache sender, PX.Objects.PM.TaxZoneExtension.Document row);
}
