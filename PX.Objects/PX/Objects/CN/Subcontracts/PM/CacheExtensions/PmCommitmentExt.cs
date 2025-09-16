// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Subcontracts.PM.CacheExtensions.PmCommitmentExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CN.Subcontracts.PM.Descriptor.Attributes;
using PX.Objects.CS;
using PX.Objects.PM;
using System;

#nullable disable
namespace PX.Objects.CN.Subcontracts.PM.CacheExtensions;

public sealed class PmCommitmentExt : PXCacheExtension<PMCommitment>
{
  [PXString]
  [PXUIField]
  [PXStringList(new string[] {"POOrder", "SOOrder", "Subcontract"}, new string[] {"Purchase Order", "Sales Order", "Subcontract"})]
  public string RelatedDocumentType { get; set; }

  [PXRemoveBaseAttribute(typeof (PMCommitment.PXRefNoteAttribute))]
  [CommitmentRefNote]
  public Guid? RefNoteID { get; set; }

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.construction>();

  public abstract class relatedDocumentType : IBqlField, IBqlOperand
  {
  }
}
