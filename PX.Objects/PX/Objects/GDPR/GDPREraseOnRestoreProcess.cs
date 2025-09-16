// Decompiled with JetBrains decompiler
// Type: PX.Objects.GDPR.GDPREraseOnRestoreProcess
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.GDPR;

public class GDPREraseOnRestoreProcess : GDPRRestoreProcess
{
  protected override void ChildLevelProcessor(
    PXGraph processingGraph,
    Type childTable,
    IEnumerable<PXPersonalDataFieldAttribute> fields,
    IEnumerable<object> childs,
    Guid? topParentNoteID)
  {
    this.ErasePseudonymizedData(processingGraph, childTable, childs);
  }

  private void ErasePseudonymizedData(
    PXGraph processingGraph,
    Type childTable,
    IEnumerable<object> childs)
  {
    foreach (object child in childs)
    {
      List<PXDataFieldParam> first = new List<PXDataFieldParam>();
      first.Add((PXDataFieldParam) new PXDataFieldAssign("PseudonymizationStatus", (object) this.SetPseudonymizationStatus));
      List<PXDataFieldParam> second = new List<PXDataFieldParam>();
      foreach (string key in (IEnumerable<string>) processingGraph.Caches[childTable].Keys)
        second.Add((PXDataFieldParam) new PXDataFieldRestrict(key, processingGraph.Caches[childTable].GetValue(child, key)));
      PXDatabase.Update(childTable, first.Union<PXDataFieldParam>((IEnumerable<PXDataFieldParam>) second).ToArray<PXDataFieldParam>());
      Guid? nullable = processingGraph.Caches[childTable].GetValue(child, "NoteID") as Guid?;
      PXDatabase.Delete<SMPersonalData>(new PXDataFieldRestrict[2]
      {
        (PXDataFieldRestrict) new PXDataFieldRestrict<SMPersonalData.table>((object) childTable.FullName),
        (PXDataFieldRestrict) new PXDataFieldRestrict<SMPersonalData.entityID>((object) nullable)
      });
    }
  }
}
