// Decompiled with JetBrains decompiler
// Type: PX.Data.BQL.Fluent.FbqlJoins
// Assembly: PX.Data.BQL.Fluent, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1086111-88AF-4F2E-BE39-D2C71848C2C0
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.BQL.Fluent.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.BQL.Fluent.xml

using PX.Common;
using System;

#nullable disable
namespace PX.Data.BQL.Fluent;

/// <summary>Defines the classes for fluent BQL join.</summary>
public static class FbqlJoins
{
  /// <summary>
  /// Defines an inner join with a <typeparamref name="TJoinedTable" />.
  /// </summary>
  /// <typeparam name="TJoinedTable">Indicates the table that will be inner-joined</typeparam>
  public class Inner<TJoinedTable> where TJoinedTable : IBqlTable
  {
    /// <summary>Declares a joining condition</summary>
    public class On<TJoinCondition> : IFbqlJoin where TJoinCondition : IBqlUnary, new()
    {
      Type IFbqlJoin.ChainedJoin => typeof (InnerJoin<,,>);

      Type IFbqlJoin.SimpleJoin => typeof (InnerJoin<,>);

      Type IFbqlJoin.Table => typeof (TJoinedTable);

      Type IFbqlJoin.Condition => typeof (TJoinCondition);

      /// <summary>
      /// Indicates that only the most left table from a joined projection will be used
      /// </summary>
      public class SingleTableOnly : IFbqlJoin
      {
        Type IFbqlJoin.ChainedJoin => typeof (InnerJoinSingleTable<,,>);

        Type IFbqlJoin.SimpleJoin => typeof (InnerJoinSingleTable<,>);

        Type IFbqlJoin.Table => typeof (TJoinedTable);

        Type IFbqlJoin.Condition => typeof (TJoinCondition);
      }

      /// <summary>
      /// If possible, prevents the DBMS optimizer from reordering joined tables, which can impact performance in both directions, so use it wisely.
      /// </summary>
      [PXInternalUseOnly]
      public class Straight : IFbqlJoin
      {
        Type IFbqlJoin.ChainedJoin => typeof (InnerJoinStraight<,,>);

        Type IFbqlJoin.SimpleJoin => typeof (InnerJoinStraight<,>);

        Type IFbqlJoin.Table => typeof (TJoinedTable);

        Type IFbqlJoin.Condition => typeof (TJoinCondition);
      }
    }
  }

  /// <summary>
  /// Defines an left join with a <typeparamref name="TJoinedTable" />.
  /// </summary>
  /// <typeparam name="TJoinedTable">Indicates the table that will be left-joined</typeparam>
  public class Left<TJoinedTable> where TJoinedTable : IBqlTable
  {
    /// <summary>Declares a joining condition</summary>
    public class On<TJoinCondition> : IFbqlJoin where TJoinCondition : IBqlUnary, new()
    {
      Type IFbqlJoin.ChainedJoin => typeof (LeftJoin<,,>);

      Type IFbqlJoin.SimpleJoin => typeof (LeftJoin<,>);

      Type IFbqlJoin.Table => typeof (TJoinedTable);

      Type IFbqlJoin.Condition => typeof (TJoinCondition);

      /// <summary>
      /// Indicates that only the most left table from a joined projection will be used
      /// </summary>
      public class SingleTableOnly : IFbqlJoin
      {
        Type IFbqlJoin.ChainedJoin => typeof (LeftJoinSingleTable<,,>);

        Type IFbqlJoin.SimpleJoin => typeof (LeftJoinSingleTable<,>);

        Type IFbqlJoin.Table => typeof (TJoinedTable);

        Type IFbqlJoin.Condition => typeof (TJoinCondition);
      }
    }
  }

  /// <summary>
  /// Defines an right join with a <typeparamref name="TJoinedTable" />.
  /// </summary>
  /// <typeparam name="TJoinedTable">Indicates the table that will be right-joined</typeparam>
  public class Right<TJoinedTable> where TJoinedTable : IBqlTable
  {
    /// <summary>Declares a joining condition</summary>
    public class On<TJoinCondition> : IFbqlJoin where TJoinCondition : IBqlUnary, new()
    {
      Type IFbqlJoin.ChainedJoin => typeof (RightJoin<,,>);

      Type IFbqlJoin.SimpleJoin => typeof (RightJoin<,>);

      Type IFbqlJoin.Table => typeof (TJoinedTable);

      Type IFbqlJoin.Condition => typeof (TJoinCondition);

      /// <summary>
      /// Indicates that only the most left table from a joined projection will be used
      /// </summary>
      public class SingleTableOnly : IFbqlJoin
      {
        Type IFbqlJoin.ChainedJoin => typeof (RightJoinSingleTable<,,>);

        Type IFbqlJoin.SimpleJoin => typeof (RightJoinSingleTable<,>);

        Type IFbqlJoin.Table => typeof (TJoinedTable);

        Type IFbqlJoin.Condition => typeof (TJoinCondition);
      }
    }
  }

  /// <summary>
  /// Defines an full join with a <typeparamref name="TJoinedTable" />.
  /// </summary>
  /// <typeparam name="TJoinedTable">Indicates the table that will be full-joined</typeparam>
  public class Full<TJoinedTable> where TJoinedTable : IBqlTable
  {
    /// <summary>Declares a joining condition</summary>
    public class On<TJoinCondition> : IFbqlJoin where TJoinCondition : IBqlUnary, new()
    {
      Type IFbqlJoin.ChainedJoin => typeof (FullJoin<,,>);

      Type IFbqlJoin.SimpleJoin => typeof (FullJoin<,>);

      Type IFbqlJoin.Table => typeof (TJoinedTable);

      Type IFbqlJoin.Condition => typeof (TJoinCondition);

      /// <summary>
      /// Indicates that only the most left table from a joined projection will be used
      /// </summary>
      public class SingleTableOnly : IFbqlJoin
      {
        Type IFbqlJoin.ChainedJoin => typeof (FullJoinSingleTable<,,>);

        Type IFbqlJoin.SimpleJoin => typeof (FullJoinSingleTable<,>);

        Type IFbqlJoin.Table => typeof (TJoinedTable);

        Type IFbqlJoin.Condition => typeof (TJoinCondition);
      }
    }
  }

  /// <summary>
  /// Defines an cross join with a <typeparamref name="TJoinedTable" />.
  /// </summary>
  /// <typeparam name="TJoinedTable">Indicates the table that will be cross-joined</typeparam>
  public class Cross<TJoinedTable> : IFbqlJoin where TJoinedTable : IBqlTable
  {
    Type IFbqlJoin.ChainedJoin => typeof (CrossJoin<,>);

    Type IFbqlJoin.SimpleJoin => typeof (CrossJoin<>);

    Type IFbqlJoin.Table => typeof (TJoinedTable);

    Type IFbqlJoin.Condition => (Type) null;

    /// <summary>
    /// Indicates that only the most left table from a joined projection will be used
    /// </summary>
    public class SingleTableOnly : IFbqlJoin
    {
      Type IFbqlJoin.ChainedJoin => typeof (CrossJoinSingleTable<,>);

      Type IFbqlJoin.SimpleJoin => typeof (CrossJoinSingleTable<>);

      Type IFbqlJoin.Table => typeof (TJoinedTable);

      Type IFbqlJoin.Condition => (Type) null;
    }
  }

  /// <summary>
  /// Defines a placeholder join, that should be replaced later with <see cref="T:PX.Data.BQL.BqlTemplate" /> mechanism.
  /// </summary>
  /// <typeparam name="TPlaceholder">The exact <see cref="T:PX.Data.BQL.BqlPlaceholder" /> that will be used instead of join</typeparam>
  public class Placeholder<TPlaceholder> : IFbqlJoin where TPlaceholder : IBqlPlaceholder, IBqlJoin
  {
    Type IFbqlJoin.ChainedJoin => throw new NotImplementedException();

    Type IFbqlJoin.SimpleJoin => typeof (TPlaceholder);

    Type IFbqlJoin.Table => (Type) null;

    Type IFbqlJoin.Condition => (Type) null;
  }
}
