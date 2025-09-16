// Decompiled with JetBrains decompiler
// Type: PX.Data.Search.EntitySearchBqlConstructor
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;

#nullable disable
namespace PX.Data.Search;

/// <summary>
/// A BQL queries constructor which composes dynamic BQL at runtime for search and user records engines to retrieve <see cref="T:PX.Data.IBqlTable" /> DACs by their NoteID.
/// It interoperates with <see cref="T:PX.Data.PXSearchableAttribute" /> and provides an ability to construct BQL queries with or without row level security access rights checks
/// specified in the <see cref="T:PX.Data.PXSearchableAttribute" />.
/// </summary>
[PXInternalUseOnly]
internal static class EntitySearchBqlConstructor
{
  /// <summary>
  /// Creates bql query for multiple entities without access rights checks.
  /// </summary>
  /// <param name="entityType">Type of the entity.</param>
  /// <param name="noteIdField">The NoteID field.</param>
  /// <returns>
  /// The new BQL query for multiple entities without access rights checks.
  /// </returns>
  public static BqlCommand CreateBqlQueryForMultipleEntitiesWithoutAccessChecks(
    System.Type entityType,
    System.Type noteIdField)
  {
    ExceptionExtensions.ThrowOnNull<System.Type>(entityType, nameof (entityType), (string) null);
    ExceptionExtensions.ThrowOnNull<System.Type>(noteIdField, nameof (noteIdField), (string) null);
    return BqlCommand.CreateInstance(typeof (Select<,>), entityType, typeof (Where<,>), noteIdField, typeof (In<>), typeof (Required<>), noteIdField);
  }

  /// <summary>
  /// Creates BQL query for one or multiple entities of <paramref name="entityType" /> type with row level security access checks.
  /// </summary>
  /// <param name="entityType">Type of the entity.</param>
  /// <param name="searchableAttribute">The <see cref="T:PX.Data.PXSearchableAttribute" /> attribute declared on entity type's NoteID DAC property.</param>
  /// <param name="noteIdField">The NoteID field.</param>
  /// <param name="isQueryForMultipleEntities">True if query shouold be created to obtain multiple entities, false if it is used to obtain only a single entity.</param>
  /// <returns>
  /// The new BQL query with row level security access checks.
  /// </returns>
  public static BqlCommand CreateBqlQueryWithAccessChecks(
    System.Type entityType,
    PXSearchableAttribute searchableAttribute,
    System.Type noteIdField,
    bool isQueryForMultipleEntities)
  {
    ExceptionExtensions.ThrowOnNull<PXSearchableAttribute>(searchableAttribute, nameof (searchableAttribute), (string) null);
    ExceptionExtensions.ThrowOnNull<System.Type>(noteIdField, nameof (noteIdField), (string) null);
    System.Type checkedForAccessDacType = ExceptionExtensions.CheckIfNull<System.Type>(entityType, nameof (entityType), (string) null);
    System.Type nullableField = (System.Type) null;
    if (searchableAttribute.MatchWithJoin != (System.Type) null)
    {
      System.Type[] typeArray = BqlCommand.Decompose(searchableAttribute.MatchWithJoin);
      if (typeArray.Length > 1)
      {
        checkedForAccessDacType = typeArray[1];
        nullableField = typeArray[typeArray.Length - 1];
      }
    }
    System.Type whereClause = EntitySearchBqlConstructor.GetWhereClause(noteIdField, checkedForAccessDacType, nullableField, isQueryForMultipleEntities);
    return searchableAttribute.MatchWithJoin != (System.Type) null ? BqlCommand.CreateInstance(typeof (Select2<,,>), entityType, searchableAttribute.MatchWithJoin, whereClause) : BqlCommand.CreateInstance(typeof (Select<,>), entityType, whereClause);
  }

  private static System.Type GetWhereClause(
    System.Type noteIdField,
    System.Type checkedForAccessDacType,
    System.Type nullableField,
    bool isQueryForMultipleEntities)
  {
    return BqlCommand.Compose(typeof (Where<,,>), noteIdField, EntitySearchBqlConstructor.GetNoteIdFilterClause(noteIdField, isQueryForMultipleEntities), EntitySearchBqlConstructor.GetAccessCheckFilterClause(checkedForAccessDacType, nullableField));
  }

  private static System.Type GetNoteIdFilterClause(
    System.Type noteIdField,
    bool isQueryForMultipleEntities)
  {
    return !isQueryForMultipleEntities ? BqlCommand.Compose(typeof (Equal<>), typeof (Required<>), noteIdField) : BqlCommand.Compose(typeof (In<>), typeof (Required<>), noteIdField);
  }

  private static System.Type GetAccessCheckFilterClause(
    System.Type checkedForAccessDacType,
    System.Type nullableField)
  {
    return nullableField == (System.Type) null ? BqlCommand.Compose(typeof (And<>), EntitySearchBqlConstructor.GetMatchUserFilterClause(checkedForAccessDacType)) : BqlCommand.Compose(typeof (And<>), typeof (Where<,,>), nullableField, typeof (IsNull), typeof (Or<>), EntitySearchBqlConstructor.GetMatchUserFilterClause(checkedForAccessDacType));
  }

  private static System.Type GetMatchUserFilterClause(System.Type checkedForAccessDacType)
  {
    return BqlCommand.Compose(typeof (Match<,>), checkedForAccessDacType, typeof (Optional<AccessInfo.userName>));
  }
}
