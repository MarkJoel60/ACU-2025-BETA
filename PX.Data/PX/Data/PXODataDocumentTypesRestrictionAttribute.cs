// Decompiled with JetBrains decompiler
// Type: PX.Data.PXODataDocumentTypesRestrictionAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;

#nullable disable
namespace PX.Data;

/// <exclude />
[PXInternalUseOnly]
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class PXODataDocumentTypesRestrictionAttribute : Attribute
{
  /// <summary>
  /// Graph type to check access rights.
  /// It have to be the same graph as listed in PXPrimaryGraphBaseAttribute
  /// </summary>
  public System.Type Graph { get; }

  /// <summary>DAC field type for restriction.</summary>
  public System.Type DocumentTypeField { get; set; }

  /// <summary>
  /// Values of <see cref="P:PX.Data.PXODataDocumentTypesRestrictionAttribute.DocumentTypeField" /> for restriction.
  /// </summary>
  public object[] RestrictRightsTo { get; set; }

  public PXODataDocumentTypesRestrictionAttribute(System.Type graph) => this.Graph = graph;
}
