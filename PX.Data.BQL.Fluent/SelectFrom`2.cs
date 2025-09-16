// Decompiled with JetBrains decompiler
// Type: PX.Data.BQL.Fluent.SelectFrom`2
// Assembly: PX.Data.BQL.Fluent, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1086111-88AF-4F2E-BE39-D2C71848C2C0
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.BQL.Fluent.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.BQL.Fluent.xml

using PX.Common;

#nullable disable
namespace PX.Data.BQL.Fluent;

/// <exclude />
public class SelectFrom<TTable, TJoins> : SelectFromBase<TTable, TJoins>
  where TTable : class, IBqlTable, new()
  where TJoins : ITypeArrayOf<IFbqlJoin>
{
  public static class InnerJoin<TJoinTable> where TJoinTable : IBqlTable
  {
    public class On<TJoinCondition> : 
      SelectFromMirror<TTable, TypeArrayOf<IFbqlJoin>.Append<TJoins, FbqlJoins.Inner<TJoinTable>.On<TJoinCondition>>>
      where TJoinCondition : IBqlUnary, new()
    {
      public class SingleTableOnly : 
        SelectFromMirror<TTable, TypeArrayOf<IFbqlJoin>.Append<TJoins, FbqlJoins.Inner<TJoinTable>.On<TJoinCondition>.SingleTableOnly>>
      {
      }

      /// <inheritdoc cref="T:PX.Data.BQL.Fluent.FbqlJoins.Inner`1.On`1.Straight" />
      [PXInternalUseOnly]
      public class Straight : 
        SelectFromMirror<TTable, TypeArrayOf<IFbqlJoin>.Append<TJoins, FbqlJoins.Inner<TJoinTable>.On<TJoinCondition>.Straight>>
      {
      }
    }
  }

  public static class LeftJoin<TJoinTable> where TJoinTable : IBqlTable
  {
    public class On<TJoinCondition> : 
      SelectFromMirror<TTable, TypeArrayOf<IFbqlJoin>.Append<TJoins, FbqlJoins.Left<TJoinTable>.On<TJoinCondition>>>
      where TJoinCondition : IBqlUnary, new()
    {
      public class SingleTableOnly : 
        SelectFromMirror<TTable, TypeArrayOf<IFbqlJoin>.Append<TJoins, FbqlJoins.Left<TJoinTable>.On<TJoinCondition>.SingleTableOnly>>
      {
      }
    }
  }

  public static class RightJoin<TJoinTable> where TJoinTable : IBqlTable
  {
    public class On<TJoinCondition> : 
      SelectFromMirror<TTable, TypeArrayOf<IFbqlJoin>.Append<TJoins, FbqlJoins.Right<TJoinTable>.On<TJoinCondition>>>
      where TJoinCondition : IBqlUnary, new()
    {
      public class SingleTableOnly : 
        SelectFromMirror<TTable, TypeArrayOf<IFbqlJoin>.Append<TJoins, FbqlJoins.Right<TJoinTable>.On<TJoinCondition>.SingleTableOnly>>
      {
      }
    }
  }

  public static class FullJoin<TJoinTable> where TJoinTable : IBqlTable
  {
    public class On<TJoinCondition> : 
      SelectFromMirror<TTable, TypeArrayOf<IFbqlJoin>.Append<TJoins, FbqlJoins.Full<TJoinTable>.On<TJoinCondition>>>
      where TJoinCondition : IBqlUnary, new()
    {
      public class SingleTableOnly : 
        SelectFromMirror<TTable, TypeArrayOf<IFbqlJoin>.Append<TJoins, FbqlJoins.Full<TJoinTable>.On<TJoinCondition>.SingleTableOnly>>
      {
      }
    }
  }

  public class CrossJoin<TJoinTable> : 
    SelectFromMirror<TTable, TypeArrayOf<IFbqlJoin>.Append<TJoins, FbqlJoins.Cross<TJoinTable>>>
    where TJoinTable : IBqlTable
  {
    public class SingleTableOnly : 
      SelectFromMirror<TTable, TypeArrayOf<IFbqlJoin>.Append<TJoins, FbqlJoins.Cross<TJoinTable>.SingleTableOnly>>
    {
    }
  }

  public class PlaceholderJoin<TPlaceholder> : 
    SelectFromMirror<TTable, TypeArrayOf<IFbqlJoin>.Append<TJoins, FbqlJoins.Placeholder<TPlaceholder>>>
    where TPlaceholder : IBqlPlaceholder, IBqlJoin
  {
  }
}
