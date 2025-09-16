// Decompiled with JetBrains decompiler
// Type: PX.Data.ReferentialIntegrity.Attributes.IFieldsRelation
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.ReferentialIntegrity.Attributes;

/// <summary>
/// The interface for extraction of field pairs from the <see cref="T:PX.Data.ReferentialIntegrity.Attributes.Field`1.IsRelatedTo`1" /> class.
/// </summary>
public interface IFieldsRelation
{
  /// <exclude />
  System.Type FieldOfChildTable { get; }

  /// <exclude />
  System.Type FieldOfParentTable { get; }
}
