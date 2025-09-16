// Decompiled with JetBrains decompiler
// Type: PX.Objects.GDPR.GDPREraseProcess
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.GDPR;

public class GDPREraseProcess : GDPRPseudonymizeProcess
{
  public GDPREraseProcess()
  {
    this.GetPseudonymizationStatus = typeof (PXPseudonymizationStatusListAttribute.notPseudonymized);
    this.SetPseudonymizationStatus = 3;
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    ((PXProcessingBase<GDPRPseudonymizeProcess.ObfuscateEntity>) this.SelectedItems).SetProcessDelegate(GDPREraseProcess.\u003C\u003Ec.\u003C\u003E9__0_0 ?? (GDPREraseProcess.\u003C\u003Ec.\u003C\u003E9__0_0 = new PXProcessingBase<GDPRPseudonymizeProcess.ObfuscateEntity>.ProcessListDelegate((object) GDPREraseProcess.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003C\u002Ector\u003Eb__0_0))));
    ((PXProcessing<GDPRPseudonymizeProcess.ObfuscateEntity>) this.SelectedItems).SetProcessCaption("Erase");
    ((PXProcessing<GDPRPseudonymizeProcess.ObfuscateEntity>) this.SelectedItems).SetProcessAllCaption("Erase All");
  }

  protected override void TopLevelProcessor(string combinedKey, Guid? topParentNoteID, string info)
  {
    this.DeleteSearchIndex(topParentNoteID);
  }

  protected override void ChildLevelProcessor(
    PXGraph processingGraph,
    Type childTable,
    IEnumerable<PXPersonalDataFieldAttribute> fields,
    IEnumerable<object> childs,
    Guid? topParentNoteID)
  {
    this.PseudonymizeChilds(processingGraph, childTable, fields, childs);
    this.WipeAudit(processingGraph, childTable, fields, childs);
  }
}
