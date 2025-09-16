// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.GraphExtensions.KitAssemblyEntryExt.ShiftedPeriodsExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.Common.GraphExtensions.Abstract.DAC;
using PX.Objects.Common.GraphExtensions.Abstract.Mapping;
using System;

#nullable disable
namespace PX.Objects.IN.GraphExtensions.KitAssemblyEntryExt;

public class ShiftedPeriodsExt : 
  ShiftedPeriodsExt<KitAssemblyEntry, INKitRegister, INKitRegister.tranDate, INKitRegister.tranPeriodID, INComponentTran>
{
  public PXSelectExtension<DocumentLine> Overheads;

  public virtual void Initialize()
  {
    ((PXGraphExtension) this).Initialize();
    this.Documents = new PXSelectExtension<Document>((PXSelectBase) this.Base.Document);
    this.Lines = new PXSelectExtension<DocumentLine>((PXSelectBase) this.Base.Components);
    this.Overheads = new PXSelectExtension<DocumentLine>((PXSelectBase) this.Base.Overhead);
  }

  protected virtual DocumentLineMapping GetDocumentOverheadMapping()
  {
    return new DocumentLineMapping(typeof (INOverheadTran));
  }

  protected override void _(Events.RowUpdated<Document> e)
  {
    base._(e);
    if (!this.ShouldUpdateOverheadsOnDocumentUpdated(e))
      return;
    foreach (PXResult<DocumentLine> pxResult in ((PXSelectBase<DocumentLine>) this.Overheads).Select(Array.Empty<object>()))
    {
      DocumentLine documentLine = PXResult<DocumentLine>.op_Implicit(pxResult);
      ((PXSelectBase) this.Overheads).Cache.SetDefaultExt<DocumentLine.finPeriodID>((object) documentLine);
      GraphHelper.MarkUpdated(((PXSelectBase) this.Overheads).Cache, (object) documentLine, true);
    }
  }

  protected virtual bool ShouldUpdateOverheadsOnDocumentUpdated(Events.RowUpdated<Document> e)
  {
    return this.ShouldUpdateDetailsOnDocumentUpdated(e);
  }
}
