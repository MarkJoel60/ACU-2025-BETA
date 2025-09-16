// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Compliance.CL.Descriptor.Attributes.ComplianceDocumentRefNote.ComplianceDocumentEntityStrategies.PmRegisterStrategy
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.Common.Abstractions;
using PX.Objects.PM;
using System;
using System.Linq;

#nullable disable
namespace PX.Objects.CN.Compliance.CL.Descriptor.Attributes.ComplianceDocumentRefNote.ComplianceDocumentEntityStrategies;

public class PmRegisterStrategy : ComplianceDocumentEntityStrategy
{
  public PmRegisterStrategy()
  {
    this.EntityType = typeof (PMRegister);
    this.TypeField = typeof (PMRegister.module);
  }

  public override Guid? GetNoteId(PXGraph graph, string clDisplayName)
  {
    DocumentKey documentKey = ComplianceReferenceTypeHelper.ConvertToDocumentKey<PMRegister>(clDisplayName);
    return ((PXSelectBase<PMRegister>) new PXSelect<PMRegister, Where<PMRegister.module, Equal<Required<PMRegister.module>>, And<PMRegister.refNbr, Equal<Required<PMRegister.refNbr>>>>>(graph)).Select(new object[2]
    {
      (object) documentKey.DocType,
      (object) documentKey.RefNbr
    }).FirstTableItems.ToList<PMRegister>().SingleOrDefault<PMRegister>()?.NoteID;
  }
}
