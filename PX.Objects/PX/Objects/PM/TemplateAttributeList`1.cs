// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.TemplateAttributeList`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CR;
using PX.Objects.CS;
using System.ComponentModel;

#nullable disable
namespace PX.Objects.PM;

public class TemplateAttributeList<T>(PXGraph graph) : CRAttributeList<T>(graph)
{
  protected override void ReferenceRowPersistingHandler(PXCache sender, PXRowPersistingEventArgs e)
  {
    if (e.Row == null)
      return;
    PXCache cach = ((PXSelectBase) this)._Graph.Caches[typeof (CSAnswers)];
    foreach (CSAnswers csAnswers in cach.Cached)
    {
      if (e.Operation == 3)
        cach.Delete((object) csAnswers);
    }
  }

  protected override void RowPersistingHandler(PXCache sender, PXRowPersistingEventArgs e)
  {
    if (e.Operation != 2 && e.Operation != 1 || !(e.Row is CSAnswers row) || row.RefNoteID.HasValue)
      return;
    ((CancelEventArgs) e).Cancel = true;
    this.RowPersistDeleted(sender, (object) row);
  }
}
