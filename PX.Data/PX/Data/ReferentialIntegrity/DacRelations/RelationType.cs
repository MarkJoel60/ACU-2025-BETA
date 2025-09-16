// Decompiled with JetBrains decompiler
// Type: PX.Data.ReferentialIntegrity.DacRelations.RelationType
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;

#nullable disable
namespace PX.Data.ReferentialIntegrity.DacRelations;

[PXInternalUseOnly]
public enum RelationType
{
  /// <summary>
  /// Represents an explicitly declared relation between DACs (e.g., by using <a href="https://wiki.acumatica.com/display/TND/Primary+and+Foreign+Key+API">Foreign Key API</a>).
  /// </summary>
  Key,
  /// <summary>
  /// Represents a relation between DACs that is directly mentioned in a <see cref="T:PX.Data.PXParentAttribute" />, <see cref="T:PX.Data.ReferentialIntegrity.Attributes.PXForeignReferenceAttribute" />, <see cref="T:PX.Data.PXSelectorAttribute" /> or in a similar attribute.
  /// </summary>
  Direct,
  /// <summary>
  /// Represents a relation between DACs that is indirectly mentioned in a Join or Where clause in a data view inside one of the data graphs.
  /// </summary>
  Indirect,
}
