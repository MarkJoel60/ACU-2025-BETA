// Decompiled with JetBrains decompiler
// Type: PX.Data.ReferentialIntegrity.Attributes.CompositeKey`10
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;

#nullable disable
namespace PX.Data.ReferentialIntegrity.Attributes;

/// <inheritdoc cref="T:PX.Data.ReferentialIntegrity.Attributes.CompositeKey`2" />
public class CompositeKey<TFieldsRelation1, TFieldsRelation2, TFieldsRelation3, TFieldsRelation4, TFieldsRelation5, TFieldsRelation6, TFieldsRelation7, TFieldsRelation8, TFieldsRelation9, TFieldsRelation10> : 
  TypeArrayOf<IFieldsRelation>.IFilledWith<TFieldsRelation1, TFieldsRelation2, TFieldsRelation3, TFieldsRelation4, TFieldsRelation5, TFieldsRelation6, TFieldsRelation7, TFieldsRelation8, TFieldsRelation9, TFieldsRelation10>,
  ITypeArrayOf<IFieldsRelation>,
  TypeArray.IsNotEmpty
  where TFieldsRelation1 : IFieldsRelation
  where TFieldsRelation2 : IFieldsRelation
  where TFieldsRelation3 : IFieldsRelation
  where TFieldsRelation4 : IFieldsRelation
  where TFieldsRelation5 : IFieldsRelation
  where TFieldsRelation6 : IFieldsRelation
  where TFieldsRelation7 : IFieldsRelation
  where TFieldsRelation8 : IFieldsRelation
  where TFieldsRelation9 : IFieldsRelation
  where TFieldsRelation10 : IFieldsRelation
{
  /// <inheritdoc cref="T:PX.Data.ReferentialIntegrity.Attributes.CompositeKey`2.WithTablesOf`2" />
  public class WithTablesOf<TParentTable, TChildTable> : 
    KeysRelation<CompositeKey<TFieldsRelation1, TFieldsRelation2, TFieldsRelation3, TFieldsRelation4, TFieldsRelation5, TFieldsRelation6, TFieldsRelation7, TFieldsRelation8, TFieldsRelation9, TFieldsRelation10>.WithTablesOf<TParentTable, TChildTable>, TParentTable, TChildTable>,
    TypeArrayOf<IFieldsRelation>.IFilledWith<TFieldsRelation1, TFieldsRelation2, TFieldsRelation3, TFieldsRelation4, TFieldsRelation5, TFieldsRelation6, TFieldsRelation7, TFieldsRelation8, TFieldsRelation9, TFieldsRelation10>,
    ITypeArrayOf<IFieldsRelation>,
    TypeArray.IsNotEmpty
    where TParentTable : class, IBqlTable, new()
    where TChildTable : class, IBqlTable, new()
  {
  }
}
