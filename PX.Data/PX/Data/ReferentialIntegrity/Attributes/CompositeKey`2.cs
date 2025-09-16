// Decompiled with JetBrains decompiler
// Type: PX.Data.ReferentialIntegrity.Attributes.CompositeKey`2
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;

#nullable disable
namespace PX.Data.ReferentialIntegrity.Attributes;

/// <summary>
/// Represents a composite foreign key relation, made of a set of fields relations
/// (<see cref="T:PX.Data.ReferentialIntegrity.Attributes.Field`1.IsRelatedTo`1" />).
/// </summary>
public class CompositeKey<TFieldsRelation1, TFieldsRelation2> : 
  TypeArrayOf<IFieldsRelation>.IFilledWith<TFieldsRelation1, TFieldsRelation2>,
  ITypeArrayOf<IFieldsRelation>,
  TypeArray.IsNotEmpty
  where TFieldsRelation1 : IFieldsRelation
  where TFieldsRelation2 : IFieldsRelation
{
  /// <summary>
  /// A composite foreign key relation between <typeparamref name="TParentTable" /> and <typeparamref name="TChildTable" /> entities.
  /// </summary>
  /// <typeparam name="TParentTable">The parent table of the foreign key.</typeparam>
  /// <typeparam name="TChildTable">The child table of the foreign key.</typeparam>
  public class WithTablesOf<TParentTable, TChildTable> : 
    KeysRelation<CompositeKey<TFieldsRelation1, TFieldsRelation2>.WithTablesOf<TParentTable, TChildTable>, TParentTable, TChildTable>,
    TypeArrayOf<IFieldsRelation>.IFilledWith<TFieldsRelation1, TFieldsRelation2>,
    ITypeArrayOf<IFieldsRelation>,
    TypeArray.IsNotEmpty
    where TParentTable : class, IBqlTable, new()
    where TChildTable : class, IBqlTable, new()
  {
  }
}
