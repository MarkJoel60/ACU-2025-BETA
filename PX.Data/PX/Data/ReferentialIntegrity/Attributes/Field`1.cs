// Decompiled with JetBrains decompiler
// Type: PX.Data.ReferentialIntegrity.Attributes.Field`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;

#nullable disable
namespace PX.Data.ReferentialIntegrity.Attributes;

/// <summary>
/// Begins a field relation declaration by defining a <see cref="T:PX.Data.IBqlField" /> of the child <see cref="T:PX.Data.IBqlTable" />.
/// </summary>
/// <typeparam name="TBqlFieldOfChildTable"><see cref="T:PX.Data.IBqlField" /> of child-<see cref="T:PX.Data.IBqlTable" /> of the relation</typeparam>
public class Field<TBqlFieldOfChildTable> where TBqlFieldOfChildTable : IBqlField
{
  /// <summary>
  /// Finishes the fields relation declaration by defining a <see cref="T:PX.Data.IBqlField" /> of parent-<see cref="T:PX.Data.IBqlTable" />.
  /// </summary>
  /// <typeparam name="TBqlFieldOfParentTable"><see cref="T:PX.Data.IBqlField" /> of parent-<see cref="T:PX.Data.IBqlTable" /> of the relation</typeparam>
  public class IsRelatedTo<TBqlFieldOfParentTable> : IFieldsRelation where TBqlFieldOfParentTable : IBqlField
  {
    System.Type IFieldsRelation.FieldOfChildTable => typeof (TBqlFieldOfChildTable);

    System.Type IFieldsRelation.FieldOfParentTable => typeof (TBqlFieldOfParentTable);

    /// <summary>
    /// Indicates that the field relation should be interpreted as a simple foreign key relation.
    /// </summary>
    public class AsSimpleKey : 
      TypeArrayOf<IFieldsRelation>.IFilledWith<Field<TBqlFieldOfChildTable>.IsRelatedTo<TBqlFieldOfParentTable>>,
      ITypeArrayOf<IFieldsRelation>,
      TypeArray.IsNotEmpty
    {
      /// <summary>
      /// A simple relation between <typeparamref name="TParentTable" /> and <typeparamref name="TChildTable" /> entities.
      /// </summary>
      /// <typeparam name="TParentTable">The parent table of the foreign key.</typeparam>
      /// <typeparam name="TChildTable">The child table of the foreign key.</typeparam>
      public class WithTablesOf<TParentTable, TChildTable> : 
        KeysRelation<Field<TBqlFieldOfChildTable>.IsRelatedTo<TBqlFieldOfParentTable>.AsSimpleKey.WithTablesOf<TParentTable, TChildTable>, TParentTable, TChildTable>,
        TypeArrayOf<IFieldsRelation>.IFilledWith<Field<TBqlFieldOfChildTable>.IsRelatedTo<TBqlFieldOfParentTable>>,
        ITypeArrayOf<IFieldsRelation>,
        TypeArray.IsNotEmpty
        where TParentTable : class, IBqlTable, new()
        where TChildTable : class, IBqlTable, new()
      {
      }
    }
  }
}
