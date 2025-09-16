// Decompiled with JetBrains decompiler
// Type: PX.Data.ReferentialIntegrity.Attributes.ExcludeWhen`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.ReferentialIntegrity.Attributes;

/// <summary>
/// Begins a foreign table excluding condition definition
/// by defining the foreign <see cref="T:PX.Data.IBqlTable" />.
/// </summary>
public abstract class ExcludeWhen<TBqlTable> where TBqlTable : IBqlTable
{
  /// <summary>
  /// Continues the foreign table excluding condition definition
  /// by defining <see cref="T:PX.Data.IBqlOn" />-condition, on which foreign <see cref="T:PX.Data.IBqlTable" />
  /// would be joined to the current table.
  /// </summary>
  public abstract class Joined<TBqlOn> where TBqlOn : IBqlOn
  {
    /// <summary>
    /// Finishes the foreign table excluding condition definition
    /// by defining the <see cref="T:PX.Data.IBqlWhere" />-condition, which foreign <see cref="T:PX.Data.IBqlTable" />
    /// should match so that a current table row would be excluded from referential integrity check.
    /// </summary>
    public class Satisfies<TBqlWhere> : IByForeignTableExcludingCondition where TBqlWhere : IBqlWhere
    {
      System.Type IByForeignTableExcludingCondition.ForeignTable => typeof (TBqlTable);

      System.Type IByForeignTableExcludingCondition.JoinOn => typeof (TBqlOn);

      System.Type IByForeignTableExcludingCondition.Condition => typeof (TBqlWhere);
    }

    /// <summary>
    /// Finishes the foreign table excluding condition definition
    /// by indicating that foreign table should only be joined
    /// without any excluding condition introducing and that
    /// real excluding condition for joined table will be found
    /// in further foreign table excluding condition definitions,
    /// therefore such excluding condition should never be neither
    /// the last element of <see cref="T:PX.Data.ReferentialIntegrity.Attributes.ExcludingConditionArray" />,
    /// nor the single condition.<para />
    /// Using of current technique allows developer to define
    /// complex excluding conditions, that includes fields of
    /// multiple tables.
    /// </summary>
    /// <example>
    /// Exclude row only if both SOOrder-parent and SOShipment-parent are released/completed
    /// [PXExcludeRowsFromReferentialIntegrityCheck(ForeignTableExcludingConditions =
    /// 	typeof(ExcludingConditionArray.FilledWith{
    /// 		ExcludeWhen{SOOrder}.JoinedAsParent.JoinOnly,
    /// 		ExcludeWhen{SOShipment}.JoinedAsParent.Satisfies{Where{SOShipment.released, Equal{True}, And{SOOrder.completed, Equal{True}}}}})
    /// )]
    /// </example>
    public class JoinOnly : IByForeignTableExcludingCondition
    {
      System.Type IByForeignTableExcludingCondition.ForeignTable => typeof (TBqlTable);

      System.Type IByForeignTableExcludingCondition.JoinOn => typeof (TBqlOn);

      System.Type IByForeignTableExcludingCondition.Condition => typeof (Where<True, Equal<False>>);
    }
  }

  /// <summary>
  /// Continues the foreign table excluding condition definition
  /// by indicating that <see cref="T:PX.Data.IBqlOn" />-condition, on which foreign <see cref="T:PX.Data.IBqlTable" />
  /// would be joined to the current table, should be searched in <see cref="T:PX.Data.PXParentAttribute" />s
  /// of current table.
  /// </summary>
  public abstract class JoinedAsParent
  {
    /// <summary>
    /// Finishes the foreign table excluding condition definition
    /// by defining the <see cref="T:PX.Data.IBqlWhere" />-condition, which foreign <see cref="T:PX.Data.IBqlTable" />
    /// should match so that a current table row would be excluded from referential integrity check.
    /// </summary>
    public class Satisfies<TBqlWhere> : IByForeignTableExcludingCondition where TBqlWhere : IBqlWhere
    {
      System.Type IByForeignTableExcludingCondition.ForeignTable => typeof (TBqlTable);

      System.Type IByForeignTableExcludingCondition.JoinOn => typeof (PXParentAttribute);

      System.Type IByForeignTableExcludingCondition.Condition => typeof (TBqlWhere);
    }

    /// <summary>
    /// Finishes the foreign table excluding condition definition
    /// by indicating that foreign table should only be joined
    /// without any excluding condition introducing and that
    /// real excluding condition for joined table will be found
    /// in further foreign table excluding condition definitions,
    /// therefore such excluding condition should never be neither
    /// the last element of <see cref="T:PX.Data.ReferentialIntegrity.Attributes.ExcludingConditionArray" />,
    /// nor the single condition.<para />
    /// Using of current technique allows developer to define
    /// complex excluding conditions, that includes fields of
    /// multiple tables.
    /// </summary>
    /// <example>
    /// Exclude row only if both SOOrder and SOShipment parents are released/completed
    /// [PXExcludeRowsFromReferentialIntegrityCheck(ForeignTableExcludingConditions =
    /// 	typeof(ExcludingConditionArray.FilledWith{
    /// 		ExcludeWhen{SOOrder}.JoinedAsParent.JoinOnly,
    /// 		ExcludeWhen{SOShipment}.JoinedAsParent.Satisfies{Where{SOShipment.released, Equal{True}, And{SOOrder.completed, Equal}True}}}}})
    /// )]
    /// </example>
    public class JoinOnly : IByForeignTableExcludingCondition
    {
      System.Type IByForeignTableExcludingCondition.ForeignTable => typeof (TBqlTable);

      System.Type IByForeignTableExcludingCondition.JoinOn => typeof (PXParentAttribute);

      System.Type IByForeignTableExcludingCondition.Condition => typeof (Where<True, Equal<False>>);
    }
  }
}
