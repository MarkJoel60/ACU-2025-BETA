// Decompiled with JetBrains decompiler
// Type: PX.Data.BQL.Fluent.SelectFromBase`2
// Assembly: PX.Data.BQL.Fluent, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1086111-88AF-4F2E-BE39-D2C71848C2C0
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.BQL.Fluent.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.BQL.Fluent.xml

using PX.Common;

#nullable disable
namespace PX.Data.BQL.Fluent;

/// <exclude />
/// <exclude />
/// <exclude />
/// <exclude />
/// <exclude />
/// <exclude />
/// <exclude />
/// <exclude />
/// <exclude />
/// <exclude />
/// <exclude />
/// <exclude />
public class SelectFromBase<TTable, TJoins> : 
  FbqlSelect<SelectFromBase<TTable, TJoins>, TTable, TJoins, BqlNone, TypeArrayOf<IBqlFunction>.Empty, BqlNone, TypeArrayOf<IBqlSortColumn>.Empty>
  where TTable : class, IBqlTable, new()
  where TJoins : ITypeArrayOf<IFbqlJoin>
{
  /// <exclude />
  public class AggregateTo<TFunction> : SelectFromBase<TTable, TJoins>.Aggregate<To<TFunction>> where TFunction : IBqlFunction
  {
  }

  /// <exclude />
  public class AggregateTo<TFunction1, TFunction2> : 
    SelectFromBase<TTable, TJoins>.Aggregate<To<TFunction1, TFunction2>>
    where TFunction1 : IBqlFunction
    where TFunction2 : IBqlFunction
  {
  }

  /// <exclude />
  public class AggregateTo<TFunction1, TFunction2, TFunction3> : 
    SelectFromBase<TTable, TJoins>.Aggregate<To<TFunction1, TFunction2, TFunction3>>
    where TFunction1 : IBqlFunction
    where TFunction2 : IBqlFunction
    where TFunction3 : IBqlFunction
  {
  }

  /// <exclude />
  public class AggregateTo<TFunction1, TFunction2, TFunction3, TFunction4> : 
    SelectFromBase<TTable, TJoins>.Aggregate<To<TFunction1, TFunction2, TFunction3, TFunction4>>
    where TFunction1 : IBqlFunction
    where TFunction2 : IBqlFunction
    where TFunction3 : IBqlFunction
    where TFunction4 : IBqlFunction
  {
  }

  /// <exclude />
  public class AggregateTo<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5> : 
    SelectFromBase<TTable, TJoins>.Aggregate<To<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5>>
    where TFunction1 : IBqlFunction
    where TFunction2 : IBqlFunction
    where TFunction3 : IBqlFunction
    where TFunction4 : IBqlFunction
    where TFunction5 : IBqlFunction
  {
  }

  /// <exclude />
  public class AggregateTo<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6> : 
    SelectFromBase<TTable, TJoins>.Aggregate<To<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6>>
    where TFunction1 : IBqlFunction
    where TFunction2 : IBqlFunction
    where TFunction3 : IBqlFunction
    where TFunction4 : IBqlFunction
    where TFunction5 : IBqlFunction
    where TFunction6 : IBqlFunction
  {
  }

  /// <exclude />
  public class AggregateTo<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7> : 
    SelectFromBase<TTable, TJoins>.Aggregate<To<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7>>
    where TFunction1 : IBqlFunction
    where TFunction2 : IBqlFunction
    where TFunction3 : IBqlFunction
    where TFunction4 : IBqlFunction
    where TFunction5 : IBqlFunction
    where TFunction6 : IBqlFunction
    where TFunction7 : IBqlFunction
  {
  }

  /// <exclude />
  public class AggregateTo<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8> : 
    SelectFromBase<TTable, TJoins>.Aggregate<To<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8>>
    where TFunction1 : IBqlFunction
    where TFunction2 : IBqlFunction
    where TFunction3 : IBqlFunction
    where TFunction4 : IBqlFunction
    where TFunction5 : IBqlFunction
    where TFunction6 : IBqlFunction
    where TFunction7 : IBqlFunction
    where TFunction8 : IBqlFunction
  {
  }

  /// <exclude />
  public class AggregateTo<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8, TFunction9> : 
    SelectFromBase<TTable, TJoins>.Aggregate<To<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8, TFunction9>>
    where TFunction1 : IBqlFunction
    where TFunction2 : IBqlFunction
    where TFunction3 : IBqlFunction
    where TFunction4 : IBqlFunction
    where TFunction5 : IBqlFunction
    where TFunction6 : IBqlFunction
    where TFunction7 : IBqlFunction
    where TFunction8 : IBqlFunction
    where TFunction9 : IBqlFunction
  {
  }

  /// <exclude />
  public class AggregateTo<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8, TFunction9, TFunction10> : 
    SelectFromBase<TTable, TJoins>.Aggregate<To<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8, TFunction9, TFunction10>>
    where TFunction1 : IBqlFunction
    where TFunction2 : IBqlFunction
    where TFunction3 : IBqlFunction
    where TFunction4 : IBqlFunction
    where TFunction5 : IBqlFunction
    where TFunction6 : IBqlFunction
    where TFunction7 : IBqlFunction
    where TFunction8 : IBqlFunction
    where TFunction9 : IBqlFunction
    where TFunction10 : IBqlFunction
  {
  }

  /// <exclude />
  public class AggregateTo<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8, TFunction9, TFunction10, TFunction11> : 
    SelectFromBase<TTable, TJoins>.Aggregate<To<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8, TFunction9, TFunction10, TFunction11>>
    where TFunction1 : IBqlFunction
    where TFunction2 : IBqlFunction
    where TFunction3 : IBqlFunction
    where TFunction4 : IBqlFunction
    where TFunction5 : IBqlFunction
    where TFunction6 : IBqlFunction
    where TFunction7 : IBqlFunction
    where TFunction8 : IBqlFunction
    where TFunction9 : IBqlFunction
    where TFunction10 : IBqlFunction
    where TFunction11 : IBqlFunction
  {
  }

  /// <exclude />
  public class AggregateTo<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8, TFunction9, TFunction10, TFunction11, TFunction12> : 
    SelectFromBase<TTable, TJoins>.Aggregate<To<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8, TFunction9, TFunction10, TFunction11, TFunction12>>
    where TFunction1 : IBqlFunction
    where TFunction2 : IBqlFunction
    where TFunction3 : IBqlFunction
    where TFunction4 : IBqlFunction
    where TFunction5 : IBqlFunction
    where TFunction6 : IBqlFunction
    where TFunction7 : IBqlFunction
    where TFunction8 : IBqlFunction
    where TFunction9 : IBqlFunction
    where TFunction10 : IBqlFunction
    where TFunction11 : IBqlFunction
    where TFunction12 : IBqlFunction
  {
  }

  /// <exclude />
  public class AggregateTo<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8, TFunction9, TFunction10, TFunction11, TFunction12, TFunction13> : 
    SelectFromBase<TTable, TJoins>.Aggregate<To<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8, TFunction9, TFunction10, TFunction11, TFunction12, TFunction13>>
    where TFunction1 : IBqlFunction
    where TFunction2 : IBqlFunction
    where TFunction3 : IBqlFunction
    where TFunction4 : IBqlFunction
    where TFunction5 : IBqlFunction
    where TFunction6 : IBqlFunction
    where TFunction7 : IBqlFunction
    where TFunction8 : IBqlFunction
    where TFunction9 : IBqlFunction
    where TFunction10 : IBqlFunction
    where TFunction11 : IBqlFunction
    where TFunction12 : IBqlFunction
    where TFunction13 : IBqlFunction
  {
  }

  /// <exclude />
  public class AggregateTo<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8, TFunction9, TFunction10, TFunction11, TFunction12, TFunction13, TFunction14> : 
    SelectFromBase<TTable, TJoins>.Aggregate<To<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8, TFunction9, TFunction10, TFunction11, TFunction12, TFunction13, TFunction14>>
    where TFunction1 : IBqlFunction
    where TFunction2 : IBqlFunction
    where TFunction3 : IBqlFunction
    where TFunction4 : IBqlFunction
    where TFunction5 : IBqlFunction
    where TFunction6 : IBqlFunction
    where TFunction7 : IBqlFunction
    where TFunction8 : IBqlFunction
    where TFunction9 : IBqlFunction
    where TFunction10 : IBqlFunction
    where TFunction11 : IBqlFunction
    where TFunction12 : IBqlFunction
    where TFunction13 : IBqlFunction
    where TFunction14 : IBqlFunction
  {
  }

  /// <exclude />
  public class AggregateTo<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8, TFunction9, TFunction10, TFunction11, TFunction12, TFunction13, TFunction14, TFunction15> : 
    SelectFromBase<TTable, TJoins>.Aggregate<To<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8, TFunction9, TFunction10, TFunction11, TFunction12, TFunction13, TFunction14, TFunction15>>
    where TFunction1 : IBqlFunction
    where TFunction2 : IBqlFunction
    where TFunction3 : IBqlFunction
    where TFunction4 : IBqlFunction
    where TFunction5 : IBqlFunction
    where TFunction6 : IBqlFunction
    where TFunction7 : IBqlFunction
    where TFunction8 : IBqlFunction
    where TFunction9 : IBqlFunction
    where TFunction10 : IBqlFunction
    where TFunction11 : IBqlFunction
    where TFunction12 : IBqlFunction
    where TFunction13 : IBqlFunction
    where TFunction14 : IBqlFunction
    where TFunction15 : IBqlFunction
  {
  }

  /// <exclude />
  public class AggregateTo<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8, TFunction9, TFunction10, TFunction11, TFunction12, TFunction13, TFunction14, TFunction15, TFunction16> : 
    SelectFromBase<TTable, TJoins>.Aggregate<To<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8, TFunction9, TFunction10, TFunction11, TFunction12, TFunction13, TFunction14, TFunction15, TFunction16>>
    where TFunction1 : IBqlFunction
    where TFunction2 : IBqlFunction
    where TFunction3 : IBqlFunction
    where TFunction4 : IBqlFunction
    where TFunction5 : IBqlFunction
    where TFunction6 : IBqlFunction
    where TFunction7 : IBqlFunction
    where TFunction8 : IBqlFunction
    where TFunction9 : IBqlFunction
    where TFunction10 : IBqlFunction
    where TFunction11 : IBqlFunction
    where TFunction12 : IBqlFunction
    where TFunction13 : IBqlFunction
    where TFunction14 : IBqlFunction
    where TFunction15 : IBqlFunction
    where TFunction16 : IBqlFunction
  {
  }

  /// <exclude />
  public class AggregateTo<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8, TFunction9, TFunction10, TFunction11, TFunction12, TFunction13, TFunction14, TFunction15, TFunction16, TFunction17> : 
    SelectFromBase<TTable, TJoins>.Aggregate<To<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8, TFunction9, TFunction10, TFunction11, TFunction12, TFunction13, TFunction14, TFunction15, TFunction16, TFunction17>>
    where TFunction1 : IBqlFunction
    where TFunction2 : IBqlFunction
    where TFunction3 : IBqlFunction
    where TFunction4 : IBqlFunction
    where TFunction5 : IBqlFunction
    where TFunction6 : IBqlFunction
    where TFunction7 : IBqlFunction
    where TFunction8 : IBqlFunction
    where TFunction9 : IBqlFunction
    where TFunction10 : IBqlFunction
    where TFunction11 : IBqlFunction
    where TFunction12 : IBqlFunction
    where TFunction13 : IBqlFunction
    where TFunction14 : IBqlFunction
    where TFunction15 : IBqlFunction
    where TFunction16 : IBqlFunction
    where TFunction17 : IBqlFunction
  {
  }

  /// <exclude />
  public class AggregateTo<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8, TFunction9, TFunction10, TFunction11, TFunction12, TFunction13, TFunction14, TFunction15, TFunction16, TFunction17, TFunction18> : 
    SelectFromBase<TTable, TJoins>.Aggregate<To<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8, TFunction9, TFunction10, TFunction11, TFunction12, TFunction13, TFunction14, TFunction15, TFunction16, TFunction17, TFunction18>>
    where TFunction1 : IBqlFunction
    where TFunction2 : IBqlFunction
    where TFunction3 : IBqlFunction
    where TFunction4 : IBqlFunction
    where TFunction5 : IBqlFunction
    where TFunction6 : IBqlFunction
    where TFunction7 : IBqlFunction
    where TFunction8 : IBqlFunction
    where TFunction9 : IBqlFunction
    where TFunction10 : IBqlFunction
    where TFunction11 : IBqlFunction
    where TFunction12 : IBqlFunction
    where TFunction13 : IBqlFunction
    where TFunction14 : IBqlFunction
    where TFunction15 : IBqlFunction
    where TFunction16 : IBqlFunction
    where TFunction17 : IBqlFunction
    where TFunction18 : IBqlFunction
  {
  }

  /// <exclude />
  public class AggregateTo<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8, TFunction9, TFunction10, TFunction11, TFunction12, TFunction13, TFunction14, TFunction15, TFunction16, TFunction17, TFunction18, TFunction19> : 
    SelectFromBase<TTable, TJoins>.Aggregate<To<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8, TFunction9, TFunction10, TFunction11, TFunction12, TFunction13, TFunction14, TFunction15, TFunction16, TFunction17, TFunction18, TFunction19>>
    where TFunction1 : IBqlFunction
    where TFunction2 : IBqlFunction
    where TFunction3 : IBqlFunction
    where TFunction4 : IBqlFunction
    where TFunction5 : IBqlFunction
    where TFunction6 : IBqlFunction
    where TFunction7 : IBqlFunction
    where TFunction8 : IBqlFunction
    where TFunction9 : IBqlFunction
    where TFunction10 : IBqlFunction
    where TFunction11 : IBqlFunction
    where TFunction12 : IBqlFunction
    where TFunction13 : IBqlFunction
    where TFunction14 : IBqlFunction
    where TFunction15 : IBqlFunction
    where TFunction16 : IBqlFunction
    where TFunction17 : IBqlFunction
    where TFunction18 : IBqlFunction
    where TFunction19 : IBqlFunction
  {
  }

  /// <exclude />
  public class AggregateTo<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8, TFunction9, TFunction10, TFunction11, TFunction12, TFunction13, TFunction14, TFunction15, TFunction16, TFunction17, TFunction18, TFunction19, TFunction20> : 
    SelectFromBase<TTable, TJoins>.Aggregate<To<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8, TFunction9, TFunction10, TFunction11, TFunction12, TFunction13, TFunction14, TFunction15, TFunction16, TFunction17, TFunction18, TFunction19, TFunction20>>
    where TFunction1 : IBqlFunction
    where TFunction2 : IBqlFunction
    where TFunction3 : IBqlFunction
    where TFunction4 : IBqlFunction
    where TFunction5 : IBqlFunction
    where TFunction6 : IBqlFunction
    where TFunction7 : IBqlFunction
    where TFunction8 : IBqlFunction
    where TFunction9 : IBqlFunction
    where TFunction10 : IBqlFunction
    where TFunction11 : IBqlFunction
    where TFunction12 : IBqlFunction
    where TFunction13 : IBqlFunction
    where TFunction14 : IBqlFunction
    where TFunction15 : IBqlFunction
    where TFunction16 : IBqlFunction
    where TFunction17 : IBqlFunction
    where TFunction18 : IBqlFunction
    where TFunction19 : IBqlFunction
    where TFunction20 : IBqlFunction
  {
  }

  /// <exclude />
  public class AggregateTo<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8, TFunction9, TFunction10, TFunction11, TFunction12, TFunction13, TFunction14, TFunction15, TFunction16, TFunction17, TFunction18, TFunction19, TFunction20, TFunction21> : 
    SelectFromBase<TTable, TJoins>.Aggregate<To<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8, TFunction9, TFunction10, TFunction11, TFunction12, TFunction13, TFunction14, TFunction15, TFunction16, TFunction17, TFunction18, TFunction19, TFunction20, TFunction21>>
    where TFunction1 : IBqlFunction
    where TFunction2 : IBqlFunction
    where TFunction3 : IBqlFunction
    where TFunction4 : IBqlFunction
    where TFunction5 : IBqlFunction
    where TFunction6 : IBqlFunction
    where TFunction7 : IBqlFunction
    where TFunction8 : IBqlFunction
    where TFunction9 : IBqlFunction
    where TFunction10 : IBqlFunction
    where TFunction11 : IBqlFunction
    where TFunction12 : IBqlFunction
    where TFunction13 : IBqlFunction
    where TFunction14 : IBqlFunction
    where TFunction15 : IBqlFunction
    where TFunction16 : IBqlFunction
    where TFunction17 : IBqlFunction
    where TFunction18 : IBqlFunction
    where TFunction19 : IBqlFunction
    where TFunction20 : IBqlFunction
    where TFunction21 : IBqlFunction
  {
  }

  /// <exclude />
  public class AggregateTo<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8, TFunction9, TFunction10, TFunction11, TFunction12, TFunction13, TFunction14, TFunction15, TFunction16, TFunction17, TFunction18, TFunction19, TFunction20, TFunction21, TFunction22> : 
    SelectFromBase<TTable, TJoins>.Aggregate<To<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8, TFunction9, TFunction10, TFunction11, TFunction12, TFunction13, TFunction14, TFunction15, TFunction16, TFunction17, TFunction18, TFunction19, TFunction20, TFunction21, TFunction22>>
    where TFunction1 : IBqlFunction
    where TFunction2 : IBqlFunction
    where TFunction3 : IBqlFunction
    where TFunction4 : IBqlFunction
    where TFunction5 : IBqlFunction
    where TFunction6 : IBqlFunction
    where TFunction7 : IBqlFunction
    where TFunction8 : IBqlFunction
    where TFunction9 : IBqlFunction
    where TFunction10 : IBqlFunction
    where TFunction11 : IBqlFunction
    where TFunction12 : IBqlFunction
    where TFunction13 : IBqlFunction
    where TFunction14 : IBqlFunction
    where TFunction15 : IBqlFunction
    where TFunction16 : IBqlFunction
    where TFunction17 : IBqlFunction
    where TFunction18 : IBqlFunction
    where TFunction19 : IBqlFunction
    where TFunction20 : IBqlFunction
    where TFunction21 : IBqlFunction
    where TFunction22 : IBqlFunction
  {
  }

  /// <exclude />
  public class AggregateTo<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8, TFunction9, TFunction10, TFunction11, TFunction12, TFunction13, TFunction14, TFunction15, TFunction16, TFunction17, TFunction18, TFunction19, TFunction20, TFunction21, TFunction22, TFunction23> : 
    SelectFromBase<TTable, TJoins>.Aggregate<To<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8, TFunction9, TFunction10, TFunction11, TFunction12, TFunction13, TFunction14, TFunction15, TFunction16, TFunction17, TFunction18, TFunction19, TFunction20, TFunction21, TFunction22, TFunction23>>
    where TFunction1 : IBqlFunction
    where TFunction2 : IBqlFunction
    where TFunction3 : IBqlFunction
    where TFunction4 : IBqlFunction
    where TFunction5 : IBqlFunction
    where TFunction6 : IBqlFunction
    where TFunction7 : IBqlFunction
    where TFunction8 : IBqlFunction
    where TFunction9 : IBqlFunction
    where TFunction10 : IBqlFunction
    where TFunction11 : IBqlFunction
    where TFunction12 : IBqlFunction
    where TFunction13 : IBqlFunction
    where TFunction14 : IBqlFunction
    where TFunction15 : IBqlFunction
    where TFunction16 : IBqlFunction
    where TFunction17 : IBqlFunction
    where TFunction18 : IBqlFunction
    where TFunction19 : IBqlFunction
    where TFunction20 : IBqlFunction
    where TFunction21 : IBqlFunction
    where TFunction22 : IBqlFunction
    where TFunction23 : IBqlFunction
  {
  }

  /// <exclude />
  public class AggregateTo<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8, TFunction9, TFunction10, TFunction11, TFunction12, TFunction13, TFunction14, TFunction15, TFunction16, TFunction17, TFunction18, TFunction19, TFunction20, TFunction21, TFunction22, TFunction23, TFunction24> : 
    SelectFromBase<TTable, TJoins>.Aggregate<To<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8, TFunction9, TFunction10, TFunction11, TFunction12, TFunction13, TFunction14, TFunction15, TFunction16, TFunction17, TFunction18, TFunction19, TFunction20, TFunction21, TFunction22, TFunction23, TFunction24>>
    where TFunction1 : IBqlFunction
    where TFunction2 : IBqlFunction
    where TFunction3 : IBqlFunction
    where TFunction4 : IBqlFunction
    where TFunction5 : IBqlFunction
    where TFunction6 : IBqlFunction
    where TFunction7 : IBqlFunction
    where TFunction8 : IBqlFunction
    where TFunction9 : IBqlFunction
    where TFunction10 : IBqlFunction
    where TFunction11 : IBqlFunction
    where TFunction12 : IBqlFunction
    where TFunction13 : IBqlFunction
    where TFunction14 : IBqlFunction
    where TFunction15 : IBqlFunction
    where TFunction16 : IBqlFunction
    where TFunction17 : IBqlFunction
    where TFunction18 : IBqlFunction
    where TFunction19 : IBqlFunction
    where TFunction20 : IBqlFunction
    where TFunction21 : IBqlFunction
    where TFunction22 : IBqlFunction
    where TFunction23 : IBqlFunction
    where TFunction24 : IBqlFunction
  {
  }

  /// <exclude />
  public class AggregateTo<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8, TFunction9, TFunction10, TFunction11, TFunction12, TFunction13, TFunction14, TFunction15, TFunction16, TFunction17, TFunction18, TFunction19, TFunction20, TFunction21, TFunction22, TFunction23, TFunction24, TFunction25> : 
    SelectFromBase<TTable, TJoins>.Aggregate<To<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8, TFunction9, TFunction10, TFunction11, TFunction12, TFunction13, TFunction14, TFunction15, TFunction16, TFunction17, TFunction18, TFunction19, TFunction20, TFunction21, TFunction22, TFunction23, TFunction24, TFunction25>>
    where TFunction1 : IBqlFunction
    where TFunction2 : IBqlFunction
    where TFunction3 : IBqlFunction
    where TFunction4 : IBqlFunction
    where TFunction5 : IBqlFunction
    where TFunction6 : IBqlFunction
    where TFunction7 : IBqlFunction
    where TFunction8 : IBqlFunction
    where TFunction9 : IBqlFunction
    where TFunction10 : IBqlFunction
    where TFunction11 : IBqlFunction
    where TFunction12 : IBqlFunction
    where TFunction13 : IBqlFunction
    where TFunction14 : IBqlFunction
    where TFunction15 : IBqlFunction
    where TFunction16 : IBqlFunction
    where TFunction17 : IBqlFunction
    where TFunction18 : IBqlFunction
    where TFunction19 : IBqlFunction
    where TFunction20 : IBqlFunction
    where TFunction21 : IBqlFunction
    where TFunction22 : IBqlFunction
    where TFunction23 : IBqlFunction
    where TFunction24 : IBqlFunction
    where TFunction25 : IBqlFunction
  {
  }

  /// <exclude />
  public class AggregateTo<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8, TFunction9, TFunction10, TFunction11, TFunction12, TFunction13, TFunction14, TFunction15, TFunction16, TFunction17, TFunction18, TFunction19, TFunction20, TFunction21, TFunction22, TFunction23, TFunction24, TFunction25, TFunction26> : 
    SelectFromBase<TTable, TJoins>.Aggregate<To<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8, TFunction9, TFunction10, TFunction11, TFunction12, TFunction13, TFunction14, TFunction15, TFunction16, TFunction17, TFunction18, TFunction19, TFunction20, TFunction21, TFunction22, TFunction23, TFunction24, TFunction25, TFunction26>>
    where TFunction1 : IBqlFunction
    where TFunction2 : IBqlFunction
    where TFunction3 : IBqlFunction
    where TFunction4 : IBqlFunction
    where TFunction5 : IBqlFunction
    where TFunction6 : IBqlFunction
    where TFunction7 : IBqlFunction
    where TFunction8 : IBqlFunction
    where TFunction9 : IBqlFunction
    where TFunction10 : IBqlFunction
    where TFunction11 : IBqlFunction
    where TFunction12 : IBqlFunction
    where TFunction13 : IBqlFunction
    where TFunction14 : IBqlFunction
    where TFunction15 : IBqlFunction
    where TFunction16 : IBqlFunction
    where TFunction17 : IBqlFunction
    where TFunction18 : IBqlFunction
    where TFunction19 : IBqlFunction
    where TFunction20 : IBqlFunction
    where TFunction21 : IBqlFunction
    where TFunction22 : IBqlFunction
    where TFunction23 : IBqlFunction
    where TFunction24 : IBqlFunction
    where TFunction25 : IBqlFunction
    where TFunction26 : IBqlFunction
  {
  }

  /// <exclude />
  public class AggregateTo<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8, TFunction9, TFunction10, TFunction11, TFunction12, TFunction13, TFunction14, TFunction15, TFunction16, TFunction17, TFunction18, TFunction19, TFunction20, TFunction21, TFunction22, TFunction23, TFunction24, TFunction25, TFunction26, TFunction27> : 
    SelectFromBase<TTable, TJoins>.Aggregate<To<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8, TFunction9, TFunction10, TFunction11, TFunction12, TFunction13, TFunction14, TFunction15, TFunction16, TFunction17, TFunction18, TFunction19, TFunction20, TFunction21, TFunction22, TFunction23, TFunction24, TFunction25, TFunction26, TFunction27>>
    where TFunction1 : IBqlFunction
    where TFunction2 : IBqlFunction
    where TFunction3 : IBqlFunction
    where TFunction4 : IBqlFunction
    where TFunction5 : IBqlFunction
    where TFunction6 : IBqlFunction
    where TFunction7 : IBqlFunction
    where TFunction8 : IBqlFunction
    where TFunction9 : IBqlFunction
    where TFunction10 : IBqlFunction
    where TFunction11 : IBqlFunction
    where TFunction12 : IBqlFunction
    where TFunction13 : IBqlFunction
    where TFunction14 : IBqlFunction
    where TFunction15 : IBqlFunction
    where TFunction16 : IBqlFunction
    where TFunction17 : IBqlFunction
    where TFunction18 : IBqlFunction
    where TFunction19 : IBqlFunction
    where TFunction20 : IBqlFunction
    where TFunction21 : IBqlFunction
    where TFunction22 : IBqlFunction
    where TFunction23 : IBqlFunction
    where TFunction24 : IBqlFunction
    where TFunction25 : IBqlFunction
    where TFunction26 : IBqlFunction
    where TFunction27 : IBqlFunction
  {
  }

  /// <exclude />
  public class AggregateTo<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8, TFunction9, TFunction10, TFunction11, TFunction12, TFunction13, TFunction14, TFunction15, TFunction16, TFunction17, TFunction18, TFunction19, TFunction20, TFunction21, TFunction22, TFunction23, TFunction24, TFunction25, TFunction26, TFunction27, TFunction28> : 
    SelectFromBase<TTable, TJoins>.Aggregate<To<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8, TFunction9, TFunction10, TFunction11, TFunction12, TFunction13, TFunction14, TFunction15, TFunction16, TFunction17, TFunction18, TFunction19, TFunction20, TFunction21, TFunction22, TFunction23, TFunction24, TFunction25, TFunction26, TFunction27, TFunction28>>
    where TFunction1 : IBqlFunction
    where TFunction2 : IBqlFunction
    where TFunction3 : IBqlFunction
    where TFunction4 : IBqlFunction
    where TFunction5 : IBqlFunction
    where TFunction6 : IBqlFunction
    where TFunction7 : IBqlFunction
    where TFunction8 : IBqlFunction
    where TFunction9 : IBqlFunction
    where TFunction10 : IBqlFunction
    where TFunction11 : IBqlFunction
    where TFunction12 : IBqlFunction
    where TFunction13 : IBqlFunction
    where TFunction14 : IBqlFunction
    where TFunction15 : IBqlFunction
    where TFunction16 : IBqlFunction
    where TFunction17 : IBqlFunction
    where TFunction18 : IBqlFunction
    where TFunction19 : IBqlFunction
    where TFunction20 : IBqlFunction
    where TFunction21 : IBqlFunction
    where TFunction22 : IBqlFunction
    where TFunction23 : IBqlFunction
    where TFunction24 : IBqlFunction
    where TFunction25 : IBqlFunction
    where TFunction26 : IBqlFunction
    where TFunction27 : IBqlFunction
    where TFunction28 : IBqlFunction
  {
  }

  /// <exclude />
  public class AggregateTo<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8, TFunction9, TFunction10, TFunction11, TFunction12, TFunction13, TFunction14, TFunction15, TFunction16, TFunction17, TFunction18, TFunction19, TFunction20, TFunction21, TFunction22, TFunction23, TFunction24, TFunction25, TFunction26, TFunction27, TFunction28, TFunction29> : 
    SelectFromBase<TTable, TJoins>.Aggregate<To<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8, TFunction9, TFunction10, TFunction11, TFunction12, TFunction13, TFunction14, TFunction15, TFunction16, TFunction17, TFunction18, TFunction19, TFunction20, TFunction21, TFunction22, TFunction23, TFunction24, TFunction25, TFunction26, TFunction27, TFunction28, TFunction29>>
    where TFunction1 : IBqlFunction
    where TFunction2 : IBqlFunction
    where TFunction3 : IBqlFunction
    where TFunction4 : IBqlFunction
    where TFunction5 : IBqlFunction
    where TFunction6 : IBqlFunction
    where TFunction7 : IBqlFunction
    where TFunction8 : IBqlFunction
    where TFunction9 : IBqlFunction
    where TFunction10 : IBqlFunction
    where TFunction11 : IBqlFunction
    where TFunction12 : IBqlFunction
    where TFunction13 : IBqlFunction
    where TFunction14 : IBqlFunction
    where TFunction15 : IBqlFunction
    where TFunction16 : IBqlFunction
    where TFunction17 : IBqlFunction
    where TFunction18 : IBqlFunction
    where TFunction19 : IBqlFunction
    where TFunction20 : IBqlFunction
    where TFunction21 : IBqlFunction
    where TFunction22 : IBqlFunction
    where TFunction23 : IBqlFunction
    where TFunction24 : IBqlFunction
    where TFunction25 : IBqlFunction
    where TFunction26 : IBqlFunction
    where TFunction27 : IBqlFunction
    where TFunction28 : IBqlFunction
    where TFunction29 : IBqlFunction
  {
  }

  /// <exclude />
  public class AggregateTo<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8, TFunction9, TFunction10, TFunction11, TFunction12, TFunction13, TFunction14, TFunction15, TFunction16, TFunction17, TFunction18, TFunction19, TFunction20, TFunction21, TFunction22, TFunction23, TFunction24, TFunction25, TFunction26, TFunction27, TFunction28, TFunction29, TFunction30> : 
    SelectFromBase<TTable, TJoins>.Aggregate<To<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8, TFunction9, TFunction10, TFunction11, TFunction12, TFunction13, TFunction14, TFunction15, TFunction16, TFunction17, TFunction18, TFunction19, TFunction20, TFunction21, TFunction22, TFunction23, TFunction24, TFunction25, TFunction26, TFunction27, TFunction28, TFunction29, TFunction30>>
    where TFunction1 : IBqlFunction
    where TFunction2 : IBqlFunction
    where TFunction3 : IBqlFunction
    where TFunction4 : IBqlFunction
    where TFunction5 : IBqlFunction
    where TFunction6 : IBqlFunction
    where TFunction7 : IBqlFunction
    where TFunction8 : IBqlFunction
    where TFunction9 : IBqlFunction
    where TFunction10 : IBqlFunction
    where TFunction11 : IBqlFunction
    where TFunction12 : IBqlFunction
    where TFunction13 : IBqlFunction
    where TFunction14 : IBqlFunction
    where TFunction15 : IBqlFunction
    where TFunction16 : IBqlFunction
    where TFunction17 : IBqlFunction
    where TFunction18 : IBqlFunction
    where TFunction19 : IBqlFunction
    where TFunction20 : IBqlFunction
    where TFunction21 : IBqlFunction
    where TFunction22 : IBqlFunction
    where TFunction23 : IBqlFunction
    where TFunction24 : IBqlFunction
    where TFunction25 : IBqlFunction
    where TFunction26 : IBqlFunction
    where TFunction27 : IBqlFunction
    where TFunction28 : IBqlFunction
    where TFunction29 : IBqlFunction
    where TFunction30 : IBqlFunction
  {
  }

  /// <exclude />
  public class AggregateTo<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8, TFunction9, TFunction10, TFunction11, TFunction12, TFunction13, TFunction14, TFunction15, TFunction16, TFunction17, TFunction18, TFunction19, TFunction20, TFunction21, TFunction22, TFunction23, TFunction24, TFunction25, TFunction26, TFunction27, TFunction28, TFunction29, TFunction30, TFunction31> : 
    SelectFromBase<TTable, TJoins>.Aggregate<To<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8, TFunction9, TFunction10, TFunction11, TFunction12, TFunction13, TFunction14, TFunction15, TFunction16, TFunction17, TFunction18, TFunction19, TFunction20, TFunction21, TFunction22, TFunction23, TFunction24, TFunction25, TFunction26, TFunction27, TFunction28, TFunction29, TFunction30, TFunction31>>
    where TFunction1 : IBqlFunction
    where TFunction2 : IBqlFunction
    where TFunction3 : IBqlFunction
    where TFunction4 : IBqlFunction
    where TFunction5 : IBqlFunction
    where TFunction6 : IBqlFunction
    where TFunction7 : IBqlFunction
    where TFunction8 : IBqlFunction
    where TFunction9 : IBqlFunction
    where TFunction10 : IBqlFunction
    where TFunction11 : IBqlFunction
    where TFunction12 : IBqlFunction
    where TFunction13 : IBqlFunction
    where TFunction14 : IBqlFunction
    where TFunction15 : IBqlFunction
    where TFunction16 : IBqlFunction
    where TFunction17 : IBqlFunction
    where TFunction18 : IBqlFunction
    where TFunction19 : IBqlFunction
    where TFunction20 : IBqlFunction
    where TFunction21 : IBqlFunction
    where TFunction22 : IBqlFunction
    where TFunction23 : IBqlFunction
    where TFunction24 : IBqlFunction
    where TFunction25 : IBqlFunction
    where TFunction26 : IBqlFunction
    where TFunction27 : IBqlFunction
    where TFunction28 : IBqlFunction
    where TFunction29 : IBqlFunction
    where TFunction30 : IBqlFunction
    where TFunction31 : IBqlFunction
  {
  }

  /// <exclude />
  public class AggregateTo<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8, TFunction9, TFunction10, TFunction11, TFunction12, TFunction13, TFunction14, TFunction15, TFunction16, TFunction17, TFunction18, TFunction19, TFunction20, TFunction21, TFunction22, TFunction23, TFunction24, TFunction25, TFunction26, TFunction27, TFunction28, TFunction29, TFunction30, TFunction31, TFunction32> : 
    SelectFromBase<TTable, TJoins>.Aggregate<To<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8, TFunction9, TFunction10, TFunction11, TFunction12, TFunction13, TFunction14, TFunction15, TFunction16, TFunction17, TFunction18, TFunction19, TFunction20, TFunction21, TFunction22, TFunction23, TFunction24, TFunction25, TFunction26, TFunction27, TFunction28, TFunction29, TFunction30, TFunction31, TFunction32>>
    where TFunction1 : IBqlFunction
    where TFunction2 : IBqlFunction
    where TFunction3 : IBqlFunction
    where TFunction4 : IBqlFunction
    where TFunction5 : IBqlFunction
    where TFunction6 : IBqlFunction
    where TFunction7 : IBqlFunction
    where TFunction8 : IBqlFunction
    where TFunction9 : IBqlFunction
    where TFunction10 : IBqlFunction
    where TFunction11 : IBqlFunction
    where TFunction12 : IBqlFunction
    where TFunction13 : IBqlFunction
    where TFunction14 : IBqlFunction
    where TFunction15 : IBqlFunction
    where TFunction16 : IBqlFunction
    where TFunction17 : IBqlFunction
    where TFunction18 : IBqlFunction
    where TFunction19 : IBqlFunction
    where TFunction20 : IBqlFunction
    where TFunction21 : IBqlFunction
    where TFunction22 : IBqlFunction
    where TFunction23 : IBqlFunction
    where TFunction24 : IBqlFunction
    where TFunction25 : IBqlFunction
    where TFunction26 : IBqlFunction
    where TFunction27 : IBqlFunction
    where TFunction28 : IBqlFunction
    where TFunction29 : IBqlFunction
    where TFunction30 : IBqlFunction
    where TFunction31 : IBqlFunction
    where TFunction32 : IBqlFunction
  {
  }

  public class Where<TCondition> : 
    FbqlSelect<SelectFromBase<TTable, TJoins>.Where<TCondition>, TTable, TJoins, TCondition, TypeArrayOf<IBqlFunction>.Empty, BqlNone, TypeArrayOf<IBqlSortColumn>.Empty>
    where TCondition : IBqlUnary, new()
  {
    /// <exclude />
    public class AggregateTo<TFunction> : 
      SelectFromBase<TTable, TJoins>.Where<TCondition>.Aggregate<To<TFunction>>
      where TFunction : IBqlFunction
    {
    }

    /// <exclude />
    public class AggregateTo<TFunction1, TFunction2> : 
      SelectFromBase<TTable, TJoins>.Where<TCondition>.Aggregate<To<TFunction1, TFunction2>>
      where TFunction1 : IBqlFunction
      where TFunction2 : IBqlFunction
    {
    }

    /// <exclude />
    public class AggregateTo<TFunction1, TFunction2, TFunction3> : 
      SelectFromBase<TTable, TJoins>.Where<TCondition>.Aggregate<To<TFunction1, TFunction2, TFunction3>>
      where TFunction1 : IBqlFunction
      where TFunction2 : IBqlFunction
      where TFunction3 : IBqlFunction
    {
    }

    /// <exclude />
    public class AggregateTo<TFunction1, TFunction2, TFunction3, TFunction4> : 
      SelectFromBase<TTable, TJoins>.Where<TCondition>.Aggregate<To<TFunction1, TFunction2, TFunction3, TFunction4>>
      where TFunction1 : IBqlFunction
      where TFunction2 : IBqlFunction
      where TFunction3 : IBqlFunction
      where TFunction4 : IBqlFunction
    {
    }

    /// <exclude />
    public class AggregateTo<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5> : 
      SelectFromBase<TTable, TJoins>.Where<TCondition>.Aggregate<To<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5>>
      where TFunction1 : IBqlFunction
      where TFunction2 : IBqlFunction
      where TFunction3 : IBqlFunction
      where TFunction4 : IBqlFunction
      where TFunction5 : IBqlFunction
    {
    }

    /// <exclude />
    public class AggregateTo<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6> : 
      SelectFromBase<TTable, TJoins>.Where<TCondition>.Aggregate<To<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6>>
      where TFunction1 : IBqlFunction
      where TFunction2 : IBqlFunction
      where TFunction3 : IBqlFunction
      where TFunction4 : IBqlFunction
      where TFunction5 : IBqlFunction
      where TFunction6 : IBqlFunction
    {
    }

    /// <exclude />
    public class AggregateTo<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7> : 
      SelectFromBase<TTable, TJoins>.Where<TCondition>.Aggregate<To<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7>>
      where TFunction1 : IBqlFunction
      where TFunction2 : IBqlFunction
      where TFunction3 : IBqlFunction
      where TFunction4 : IBqlFunction
      where TFunction5 : IBqlFunction
      where TFunction6 : IBqlFunction
      where TFunction7 : IBqlFunction
    {
    }

    /// <exclude />
    public class AggregateTo<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8> : 
      SelectFromBase<TTable, TJoins>.Where<TCondition>.Aggregate<To<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8>>
      where TFunction1 : IBqlFunction
      where TFunction2 : IBqlFunction
      where TFunction3 : IBqlFunction
      where TFunction4 : IBqlFunction
      where TFunction5 : IBqlFunction
      where TFunction6 : IBqlFunction
      where TFunction7 : IBqlFunction
      where TFunction8 : IBqlFunction
    {
    }

    /// <exclude />
    public class AggregateTo<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8, TFunction9> : 
      SelectFromBase<TTable, TJoins>.Where<TCondition>.Aggregate<To<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8, TFunction9>>
      where TFunction1 : IBqlFunction
      where TFunction2 : IBqlFunction
      where TFunction3 : IBqlFunction
      where TFunction4 : IBqlFunction
      where TFunction5 : IBqlFunction
      where TFunction6 : IBqlFunction
      where TFunction7 : IBqlFunction
      where TFunction8 : IBqlFunction
      where TFunction9 : IBqlFunction
    {
    }

    /// <exclude />
    public class AggregateTo<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8, TFunction9, TFunction10> : 
      SelectFromBase<TTable, TJoins>.Where<TCondition>.Aggregate<To<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8, TFunction9, TFunction10>>
      where TFunction1 : IBqlFunction
      where TFunction2 : IBqlFunction
      where TFunction3 : IBqlFunction
      where TFunction4 : IBqlFunction
      where TFunction5 : IBqlFunction
      where TFunction6 : IBqlFunction
      where TFunction7 : IBqlFunction
      where TFunction8 : IBqlFunction
      where TFunction9 : IBqlFunction
      where TFunction10 : IBqlFunction
    {
    }

    /// <exclude />
    public class AggregateTo<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8, TFunction9, TFunction10, TFunction11> : 
      SelectFromBase<TTable, TJoins>.Where<TCondition>.Aggregate<To<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8, TFunction9, TFunction10, TFunction11>>
      where TFunction1 : IBqlFunction
      where TFunction2 : IBqlFunction
      where TFunction3 : IBqlFunction
      where TFunction4 : IBqlFunction
      where TFunction5 : IBqlFunction
      where TFunction6 : IBqlFunction
      where TFunction7 : IBqlFunction
      where TFunction8 : IBqlFunction
      where TFunction9 : IBqlFunction
      where TFunction10 : IBqlFunction
      where TFunction11 : IBqlFunction
    {
    }

    /// <exclude />
    public class AggregateTo<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8, TFunction9, TFunction10, TFunction11, TFunction12> : 
      SelectFromBase<TTable, TJoins>.Where<TCondition>.Aggregate<To<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8, TFunction9, TFunction10, TFunction11, TFunction12>>
      where TFunction1 : IBqlFunction
      where TFunction2 : IBqlFunction
      where TFunction3 : IBqlFunction
      where TFunction4 : IBqlFunction
      where TFunction5 : IBqlFunction
      where TFunction6 : IBqlFunction
      where TFunction7 : IBqlFunction
      where TFunction8 : IBqlFunction
      where TFunction9 : IBqlFunction
      where TFunction10 : IBqlFunction
      where TFunction11 : IBqlFunction
      where TFunction12 : IBqlFunction
    {
    }

    /// <exclude />
    public class AggregateTo<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8, TFunction9, TFunction10, TFunction11, TFunction12, TFunction13> : 
      SelectFromBase<TTable, TJoins>.Where<TCondition>.Aggregate<To<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8, TFunction9, TFunction10, TFunction11, TFunction12, TFunction13>>
      where TFunction1 : IBqlFunction
      where TFunction2 : IBqlFunction
      where TFunction3 : IBqlFunction
      where TFunction4 : IBqlFunction
      where TFunction5 : IBqlFunction
      where TFunction6 : IBqlFunction
      where TFunction7 : IBqlFunction
      where TFunction8 : IBqlFunction
      where TFunction9 : IBqlFunction
      where TFunction10 : IBqlFunction
      where TFunction11 : IBqlFunction
      where TFunction12 : IBqlFunction
      where TFunction13 : IBqlFunction
    {
    }

    /// <exclude />
    public class AggregateTo<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8, TFunction9, TFunction10, TFunction11, TFunction12, TFunction13, TFunction14> : 
      SelectFromBase<TTable, TJoins>.Where<TCondition>.Aggregate<To<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8, TFunction9, TFunction10, TFunction11, TFunction12, TFunction13, TFunction14>>
      where TFunction1 : IBqlFunction
      where TFunction2 : IBqlFunction
      where TFunction3 : IBqlFunction
      where TFunction4 : IBqlFunction
      where TFunction5 : IBqlFunction
      where TFunction6 : IBqlFunction
      where TFunction7 : IBqlFunction
      where TFunction8 : IBqlFunction
      where TFunction9 : IBqlFunction
      where TFunction10 : IBqlFunction
      where TFunction11 : IBqlFunction
      where TFunction12 : IBqlFunction
      where TFunction13 : IBqlFunction
      where TFunction14 : IBqlFunction
    {
    }

    /// <exclude />
    public class AggregateTo<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8, TFunction9, TFunction10, TFunction11, TFunction12, TFunction13, TFunction14, TFunction15> : 
      SelectFromBase<TTable, TJoins>.Where<TCondition>.Aggregate<To<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8, TFunction9, TFunction10, TFunction11, TFunction12, TFunction13, TFunction14, TFunction15>>
      where TFunction1 : IBqlFunction
      where TFunction2 : IBqlFunction
      where TFunction3 : IBqlFunction
      where TFunction4 : IBqlFunction
      where TFunction5 : IBqlFunction
      where TFunction6 : IBqlFunction
      where TFunction7 : IBqlFunction
      where TFunction8 : IBqlFunction
      where TFunction9 : IBqlFunction
      where TFunction10 : IBqlFunction
      where TFunction11 : IBqlFunction
      where TFunction12 : IBqlFunction
      where TFunction13 : IBqlFunction
      where TFunction14 : IBqlFunction
      where TFunction15 : IBqlFunction
    {
    }

    /// <exclude />
    public class AggregateTo<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8, TFunction9, TFunction10, TFunction11, TFunction12, TFunction13, TFunction14, TFunction15, TFunction16> : 
      SelectFromBase<TTable, TJoins>.Where<TCondition>.Aggregate<To<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8, TFunction9, TFunction10, TFunction11, TFunction12, TFunction13, TFunction14, TFunction15, TFunction16>>
      where TFunction1 : IBqlFunction
      where TFunction2 : IBqlFunction
      where TFunction3 : IBqlFunction
      where TFunction4 : IBqlFunction
      where TFunction5 : IBqlFunction
      where TFunction6 : IBqlFunction
      where TFunction7 : IBqlFunction
      where TFunction8 : IBqlFunction
      where TFunction9 : IBqlFunction
      where TFunction10 : IBqlFunction
      where TFunction11 : IBqlFunction
      where TFunction12 : IBqlFunction
      where TFunction13 : IBqlFunction
      where TFunction14 : IBqlFunction
      where TFunction15 : IBqlFunction
      where TFunction16 : IBqlFunction
    {
    }

    /// <exclude />
    public class AggregateTo<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8, TFunction9, TFunction10, TFunction11, TFunction12, TFunction13, TFunction14, TFunction15, TFunction16, TFunction17> : 
      SelectFromBase<TTable, TJoins>.Where<TCondition>.Aggregate<To<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8, TFunction9, TFunction10, TFunction11, TFunction12, TFunction13, TFunction14, TFunction15, TFunction16, TFunction17>>
      where TFunction1 : IBqlFunction
      where TFunction2 : IBqlFunction
      where TFunction3 : IBqlFunction
      where TFunction4 : IBqlFunction
      where TFunction5 : IBqlFunction
      where TFunction6 : IBqlFunction
      where TFunction7 : IBqlFunction
      where TFunction8 : IBqlFunction
      where TFunction9 : IBqlFunction
      where TFunction10 : IBqlFunction
      where TFunction11 : IBqlFunction
      where TFunction12 : IBqlFunction
      where TFunction13 : IBqlFunction
      where TFunction14 : IBqlFunction
      where TFunction15 : IBqlFunction
      where TFunction16 : IBqlFunction
      where TFunction17 : IBqlFunction
    {
    }

    /// <exclude />
    public class AggregateTo<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8, TFunction9, TFunction10, TFunction11, TFunction12, TFunction13, TFunction14, TFunction15, TFunction16, TFunction17, TFunction18> : 
      SelectFromBase<TTable, TJoins>.Where<TCondition>.Aggregate<To<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8, TFunction9, TFunction10, TFunction11, TFunction12, TFunction13, TFunction14, TFunction15, TFunction16, TFunction17, TFunction18>>
      where TFunction1 : IBqlFunction
      where TFunction2 : IBqlFunction
      where TFunction3 : IBqlFunction
      where TFunction4 : IBqlFunction
      where TFunction5 : IBqlFunction
      where TFunction6 : IBqlFunction
      where TFunction7 : IBqlFunction
      where TFunction8 : IBqlFunction
      where TFunction9 : IBqlFunction
      where TFunction10 : IBqlFunction
      where TFunction11 : IBqlFunction
      where TFunction12 : IBqlFunction
      where TFunction13 : IBqlFunction
      where TFunction14 : IBqlFunction
      where TFunction15 : IBqlFunction
      where TFunction16 : IBqlFunction
      where TFunction17 : IBqlFunction
      where TFunction18 : IBqlFunction
    {
    }

    /// <exclude />
    public class AggregateTo<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8, TFunction9, TFunction10, TFunction11, TFunction12, TFunction13, TFunction14, TFunction15, TFunction16, TFunction17, TFunction18, TFunction19> : 
      SelectFromBase<TTable, TJoins>.Where<TCondition>.Aggregate<To<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8, TFunction9, TFunction10, TFunction11, TFunction12, TFunction13, TFunction14, TFunction15, TFunction16, TFunction17, TFunction18, TFunction19>>
      where TFunction1 : IBqlFunction
      where TFunction2 : IBqlFunction
      where TFunction3 : IBqlFunction
      where TFunction4 : IBqlFunction
      where TFunction5 : IBqlFunction
      where TFunction6 : IBqlFunction
      where TFunction7 : IBqlFunction
      where TFunction8 : IBqlFunction
      where TFunction9 : IBqlFunction
      where TFunction10 : IBqlFunction
      where TFunction11 : IBqlFunction
      where TFunction12 : IBqlFunction
      where TFunction13 : IBqlFunction
      where TFunction14 : IBqlFunction
      where TFunction15 : IBqlFunction
      where TFunction16 : IBqlFunction
      where TFunction17 : IBqlFunction
      where TFunction18 : IBqlFunction
      where TFunction19 : IBqlFunction
    {
    }

    /// <exclude />
    public class AggregateTo<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8, TFunction9, TFunction10, TFunction11, TFunction12, TFunction13, TFunction14, TFunction15, TFunction16, TFunction17, TFunction18, TFunction19, TFunction20> : 
      SelectFromBase<TTable, TJoins>.Where<TCondition>.Aggregate<To<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8, TFunction9, TFunction10, TFunction11, TFunction12, TFunction13, TFunction14, TFunction15, TFunction16, TFunction17, TFunction18, TFunction19, TFunction20>>
      where TFunction1 : IBqlFunction
      where TFunction2 : IBqlFunction
      where TFunction3 : IBqlFunction
      where TFunction4 : IBqlFunction
      where TFunction5 : IBqlFunction
      where TFunction6 : IBqlFunction
      where TFunction7 : IBqlFunction
      where TFunction8 : IBqlFunction
      where TFunction9 : IBqlFunction
      where TFunction10 : IBqlFunction
      where TFunction11 : IBqlFunction
      where TFunction12 : IBqlFunction
      where TFunction13 : IBqlFunction
      where TFunction14 : IBqlFunction
      where TFunction15 : IBqlFunction
      where TFunction16 : IBqlFunction
      where TFunction17 : IBqlFunction
      where TFunction18 : IBqlFunction
      where TFunction19 : IBqlFunction
      where TFunction20 : IBqlFunction
    {
    }

    /// <exclude />
    public class AggregateTo<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8, TFunction9, TFunction10, TFunction11, TFunction12, TFunction13, TFunction14, TFunction15, TFunction16, TFunction17, TFunction18, TFunction19, TFunction20, TFunction21> : 
      SelectFromBase<TTable, TJoins>.Where<TCondition>.Aggregate<To<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8, TFunction9, TFunction10, TFunction11, TFunction12, TFunction13, TFunction14, TFunction15, TFunction16, TFunction17, TFunction18, TFunction19, TFunction20, TFunction21>>
      where TFunction1 : IBqlFunction
      where TFunction2 : IBqlFunction
      where TFunction3 : IBqlFunction
      where TFunction4 : IBqlFunction
      where TFunction5 : IBqlFunction
      where TFunction6 : IBqlFunction
      where TFunction7 : IBqlFunction
      where TFunction8 : IBqlFunction
      where TFunction9 : IBqlFunction
      where TFunction10 : IBqlFunction
      where TFunction11 : IBqlFunction
      where TFunction12 : IBqlFunction
      where TFunction13 : IBqlFunction
      where TFunction14 : IBqlFunction
      where TFunction15 : IBqlFunction
      where TFunction16 : IBqlFunction
      where TFunction17 : IBqlFunction
      where TFunction18 : IBqlFunction
      where TFunction19 : IBqlFunction
      where TFunction20 : IBqlFunction
      where TFunction21 : IBqlFunction
    {
    }

    /// <exclude />
    public class AggregateTo<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8, TFunction9, TFunction10, TFunction11, TFunction12, TFunction13, TFunction14, TFunction15, TFunction16, TFunction17, TFunction18, TFunction19, TFunction20, TFunction21, TFunction22> : 
      SelectFromBase<TTable, TJoins>.Where<TCondition>.Aggregate<To<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8, TFunction9, TFunction10, TFunction11, TFunction12, TFunction13, TFunction14, TFunction15, TFunction16, TFunction17, TFunction18, TFunction19, TFunction20, TFunction21, TFunction22>>
      where TFunction1 : IBqlFunction
      where TFunction2 : IBqlFunction
      where TFunction3 : IBqlFunction
      where TFunction4 : IBqlFunction
      where TFunction5 : IBqlFunction
      where TFunction6 : IBqlFunction
      where TFunction7 : IBqlFunction
      where TFunction8 : IBqlFunction
      where TFunction9 : IBqlFunction
      where TFunction10 : IBqlFunction
      where TFunction11 : IBqlFunction
      where TFunction12 : IBqlFunction
      where TFunction13 : IBqlFunction
      where TFunction14 : IBqlFunction
      where TFunction15 : IBqlFunction
      where TFunction16 : IBqlFunction
      where TFunction17 : IBqlFunction
      where TFunction18 : IBqlFunction
      where TFunction19 : IBqlFunction
      where TFunction20 : IBqlFunction
      where TFunction21 : IBqlFunction
      where TFunction22 : IBqlFunction
    {
    }

    /// <exclude />
    public class AggregateTo<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8, TFunction9, TFunction10, TFunction11, TFunction12, TFunction13, TFunction14, TFunction15, TFunction16, TFunction17, TFunction18, TFunction19, TFunction20, TFunction21, TFunction22, TFunction23> : 
      SelectFromBase<TTable, TJoins>.Where<TCondition>.Aggregate<To<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8, TFunction9, TFunction10, TFunction11, TFunction12, TFunction13, TFunction14, TFunction15, TFunction16, TFunction17, TFunction18, TFunction19, TFunction20, TFunction21, TFunction22, TFunction23>>
      where TFunction1 : IBqlFunction
      where TFunction2 : IBqlFunction
      where TFunction3 : IBqlFunction
      where TFunction4 : IBqlFunction
      where TFunction5 : IBqlFunction
      where TFunction6 : IBqlFunction
      where TFunction7 : IBqlFunction
      where TFunction8 : IBqlFunction
      where TFunction9 : IBqlFunction
      where TFunction10 : IBqlFunction
      where TFunction11 : IBqlFunction
      where TFunction12 : IBqlFunction
      where TFunction13 : IBqlFunction
      where TFunction14 : IBqlFunction
      where TFunction15 : IBqlFunction
      where TFunction16 : IBqlFunction
      where TFunction17 : IBqlFunction
      where TFunction18 : IBqlFunction
      where TFunction19 : IBqlFunction
      where TFunction20 : IBqlFunction
      where TFunction21 : IBqlFunction
      where TFunction22 : IBqlFunction
      where TFunction23 : IBqlFunction
    {
    }

    /// <exclude />
    public class AggregateTo<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8, TFunction9, TFunction10, TFunction11, TFunction12, TFunction13, TFunction14, TFunction15, TFunction16, TFunction17, TFunction18, TFunction19, TFunction20, TFunction21, TFunction22, TFunction23, TFunction24> : 
      SelectFromBase<TTable, TJoins>.Where<TCondition>.Aggregate<To<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8, TFunction9, TFunction10, TFunction11, TFunction12, TFunction13, TFunction14, TFunction15, TFunction16, TFunction17, TFunction18, TFunction19, TFunction20, TFunction21, TFunction22, TFunction23, TFunction24>>
      where TFunction1 : IBqlFunction
      where TFunction2 : IBqlFunction
      where TFunction3 : IBqlFunction
      where TFunction4 : IBqlFunction
      where TFunction5 : IBqlFunction
      where TFunction6 : IBqlFunction
      where TFunction7 : IBqlFunction
      where TFunction8 : IBqlFunction
      where TFunction9 : IBqlFunction
      where TFunction10 : IBqlFunction
      where TFunction11 : IBqlFunction
      where TFunction12 : IBqlFunction
      where TFunction13 : IBqlFunction
      where TFunction14 : IBqlFunction
      where TFunction15 : IBqlFunction
      where TFunction16 : IBqlFunction
      where TFunction17 : IBqlFunction
      where TFunction18 : IBqlFunction
      where TFunction19 : IBqlFunction
      where TFunction20 : IBqlFunction
      where TFunction21 : IBqlFunction
      where TFunction22 : IBqlFunction
      where TFunction23 : IBqlFunction
      where TFunction24 : IBqlFunction
    {
    }

    /// <exclude />
    public class AggregateTo<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8, TFunction9, TFunction10, TFunction11, TFunction12, TFunction13, TFunction14, TFunction15, TFunction16, TFunction17, TFunction18, TFunction19, TFunction20, TFunction21, TFunction22, TFunction23, TFunction24, TFunction25> : 
      SelectFromBase<TTable, TJoins>.Where<TCondition>.Aggregate<To<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8, TFunction9, TFunction10, TFunction11, TFunction12, TFunction13, TFunction14, TFunction15, TFunction16, TFunction17, TFunction18, TFunction19, TFunction20, TFunction21, TFunction22, TFunction23, TFunction24, TFunction25>>
      where TFunction1 : IBqlFunction
      where TFunction2 : IBqlFunction
      where TFunction3 : IBqlFunction
      where TFunction4 : IBqlFunction
      where TFunction5 : IBqlFunction
      where TFunction6 : IBqlFunction
      where TFunction7 : IBqlFunction
      where TFunction8 : IBqlFunction
      where TFunction9 : IBqlFunction
      where TFunction10 : IBqlFunction
      where TFunction11 : IBqlFunction
      where TFunction12 : IBqlFunction
      where TFunction13 : IBqlFunction
      where TFunction14 : IBqlFunction
      where TFunction15 : IBqlFunction
      where TFunction16 : IBqlFunction
      where TFunction17 : IBqlFunction
      where TFunction18 : IBqlFunction
      where TFunction19 : IBqlFunction
      where TFunction20 : IBqlFunction
      where TFunction21 : IBqlFunction
      where TFunction22 : IBqlFunction
      where TFunction23 : IBqlFunction
      where TFunction24 : IBqlFunction
      where TFunction25 : IBqlFunction
    {
    }

    /// <exclude />
    public class AggregateTo<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8, TFunction9, TFunction10, TFunction11, TFunction12, TFunction13, TFunction14, TFunction15, TFunction16, TFunction17, TFunction18, TFunction19, TFunction20, TFunction21, TFunction22, TFunction23, TFunction24, TFunction25, TFunction26> : 
      SelectFromBase<TTable, TJoins>.Where<TCondition>.Aggregate<To<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8, TFunction9, TFunction10, TFunction11, TFunction12, TFunction13, TFunction14, TFunction15, TFunction16, TFunction17, TFunction18, TFunction19, TFunction20, TFunction21, TFunction22, TFunction23, TFunction24, TFunction25, TFunction26>>
      where TFunction1 : IBqlFunction
      where TFunction2 : IBqlFunction
      where TFunction3 : IBqlFunction
      where TFunction4 : IBqlFunction
      where TFunction5 : IBqlFunction
      where TFunction6 : IBqlFunction
      where TFunction7 : IBqlFunction
      where TFunction8 : IBqlFunction
      where TFunction9 : IBqlFunction
      where TFunction10 : IBqlFunction
      where TFunction11 : IBqlFunction
      where TFunction12 : IBqlFunction
      where TFunction13 : IBqlFunction
      where TFunction14 : IBqlFunction
      where TFunction15 : IBqlFunction
      where TFunction16 : IBqlFunction
      where TFunction17 : IBqlFunction
      where TFunction18 : IBqlFunction
      where TFunction19 : IBqlFunction
      where TFunction20 : IBqlFunction
      where TFunction21 : IBqlFunction
      where TFunction22 : IBqlFunction
      where TFunction23 : IBqlFunction
      where TFunction24 : IBqlFunction
      where TFunction25 : IBqlFunction
      where TFunction26 : IBqlFunction
    {
    }

    /// <exclude />
    public class AggregateTo<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8, TFunction9, TFunction10, TFunction11, TFunction12, TFunction13, TFunction14, TFunction15, TFunction16, TFunction17, TFunction18, TFunction19, TFunction20, TFunction21, TFunction22, TFunction23, TFunction24, TFunction25, TFunction26, TFunction27> : 
      SelectFromBase<TTable, TJoins>.Where<TCondition>.Aggregate<To<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8, TFunction9, TFunction10, TFunction11, TFunction12, TFunction13, TFunction14, TFunction15, TFunction16, TFunction17, TFunction18, TFunction19, TFunction20, TFunction21, TFunction22, TFunction23, TFunction24, TFunction25, TFunction26, TFunction27>>
      where TFunction1 : IBqlFunction
      where TFunction2 : IBqlFunction
      where TFunction3 : IBqlFunction
      where TFunction4 : IBqlFunction
      where TFunction5 : IBqlFunction
      where TFunction6 : IBqlFunction
      where TFunction7 : IBqlFunction
      where TFunction8 : IBqlFunction
      where TFunction9 : IBqlFunction
      where TFunction10 : IBqlFunction
      where TFunction11 : IBqlFunction
      where TFunction12 : IBqlFunction
      where TFunction13 : IBqlFunction
      where TFunction14 : IBqlFunction
      where TFunction15 : IBqlFunction
      where TFunction16 : IBqlFunction
      where TFunction17 : IBqlFunction
      where TFunction18 : IBqlFunction
      where TFunction19 : IBqlFunction
      where TFunction20 : IBqlFunction
      where TFunction21 : IBqlFunction
      where TFunction22 : IBqlFunction
      where TFunction23 : IBqlFunction
      where TFunction24 : IBqlFunction
      where TFunction25 : IBqlFunction
      where TFunction26 : IBqlFunction
      where TFunction27 : IBqlFunction
    {
    }

    /// <exclude />
    public class AggregateTo<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8, TFunction9, TFunction10, TFunction11, TFunction12, TFunction13, TFunction14, TFunction15, TFunction16, TFunction17, TFunction18, TFunction19, TFunction20, TFunction21, TFunction22, TFunction23, TFunction24, TFunction25, TFunction26, TFunction27, TFunction28> : 
      SelectFromBase<TTable, TJoins>.Where<TCondition>.Aggregate<To<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8, TFunction9, TFunction10, TFunction11, TFunction12, TFunction13, TFunction14, TFunction15, TFunction16, TFunction17, TFunction18, TFunction19, TFunction20, TFunction21, TFunction22, TFunction23, TFunction24, TFunction25, TFunction26, TFunction27, TFunction28>>
      where TFunction1 : IBqlFunction
      where TFunction2 : IBqlFunction
      where TFunction3 : IBqlFunction
      where TFunction4 : IBqlFunction
      where TFunction5 : IBqlFunction
      where TFunction6 : IBqlFunction
      where TFunction7 : IBqlFunction
      where TFunction8 : IBqlFunction
      where TFunction9 : IBqlFunction
      where TFunction10 : IBqlFunction
      where TFunction11 : IBqlFunction
      where TFunction12 : IBqlFunction
      where TFunction13 : IBqlFunction
      where TFunction14 : IBqlFunction
      where TFunction15 : IBqlFunction
      where TFunction16 : IBqlFunction
      where TFunction17 : IBqlFunction
      where TFunction18 : IBqlFunction
      where TFunction19 : IBqlFunction
      where TFunction20 : IBqlFunction
      where TFunction21 : IBqlFunction
      where TFunction22 : IBqlFunction
      where TFunction23 : IBqlFunction
      where TFunction24 : IBqlFunction
      where TFunction25 : IBqlFunction
      where TFunction26 : IBqlFunction
      where TFunction27 : IBqlFunction
      where TFunction28 : IBqlFunction
    {
    }

    /// <exclude />
    public class AggregateTo<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8, TFunction9, TFunction10, TFunction11, TFunction12, TFunction13, TFunction14, TFunction15, TFunction16, TFunction17, TFunction18, TFunction19, TFunction20, TFunction21, TFunction22, TFunction23, TFunction24, TFunction25, TFunction26, TFunction27, TFunction28, TFunction29> : 
      SelectFromBase<TTable, TJoins>.Where<TCondition>.Aggregate<To<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8, TFunction9, TFunction10, TFunction11, TFunction12, TFunction13, TFunction14, TFunction15, TFunction16, TFunction17, TFunction18, TFunction19, TFunction20, TFunction21, TFunction22, TFunction23, TFunction24, TFunction25, TFunction26, TFunction27, TFunction28, TFunction29>>
      where TFunction1 : IBqlFunction
      where TFunction2 : IBqlFunction
      where TFunction3 : IBqlFunction
      where TFunction4 : IBqlFunction
      where TFunction5 : IBqlFunction
      where TFunction6 : IBqlFunction
      where TFunction7 : IBqlFunction
      where TFunction8 : IBqlFunction
      where TFunction9 : IBqlFunction
      where TFunction10 : IBqlFunction
      where TFunction11 : IBqlFunction
      where TFunction12 : IBqlFunction
      where TFunction13 : IBqlFunction
      where TFunction14 : IBqlFunction
      where TFunction15 : IBqlFunction
      where TFunction16 : IBqlFunction
      where TFunction17 : IBqlFunction
      where TFunction18 : IBqlFunction
      where TFunction19 : IBqlFunction
      where TFunction20 : IBqlFunction
      where TFunction21 : IBqlFunction
      where TFunction22 : IBqlFunction
      where TFunction23 : IBqlFunction
      where TFunction24 : IBqlFunction
      where TFunction25 : IBqlFunction
      where TFunction26 : IBqlFunction
      where TFunction27 : IBqlFunction
      where TFunction28 : IBqlFunction
      where TFunction29 : IBqlFunction
    {
    }

    /// <exclude />
    public class AggregateTo<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8, TFunction9, TFunction10, TFunction11, TFunction12, TFunction13, TFunction14, TFunction15, TFunction16, TFunction17, TFunction18, TFunction19, TFunction20, TFunction21, TFunction22, TFunction23, TFunction24, TFunction25, TFunction26, TFunction27, TFunction28, TFunction29, TFunction30> : 
      SelectFromBase<TTable, TJoins>.Where<TCondition>.Aggregate<To<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8, TFunction9, TFunction10, TFunction11, TFunction12, TFunction13, TFunction14, TFunction15, TFunction16, TFunction17, TFunction18, TFunction19, TFunction20, TFunction21, TFunction22, TFunction23, TFunction24, TFunction25, TFunction26, TFunction27, TFunction28, TFunction29, TFunction30>>
      where TFunction1 : IBqlFunction
      where TFunction2 : IBqlFunction
      where TFunction3 : IBqlFunction
      where TFunction4 : IBqlFunction
      where TFunction5 : IBqlFunction
      where TFunction6 : IBqlFunction
      where TFunction7 : IBqlFunction
      where TFunction8 : IBqlFunction
      where TFunction9 : IBqlFunction
      where TFunction10 : IBqlFunction
      where TFunction11 : IBqlFunction
      where TFunction12 : IBqlFunction
      where TFunction13 : IBqlFunction
      where TFunction14 : IBqlFunction
      where TFunction15 : IBqlFunction
      where TFunction16 : IBqlFunction
      where TFunction17 : IBqlFunction
      where TFunction18 : IBqlFunction
      where TFunction19 : IBqlFunction
      where TFunction20 : IBqlFunction
      where TFunction21 : IBqlFunction
      where TFunction22 : IBqlFunction
      where TFunction23 : IBqlFunction
      where TFunction24 : IBqlFunction
      where TFunction25 : IBqlFunction
      where TFunction26 : IBqlFunction
      where TFunction27 : IBqlFunction
      where TFunction28 : IBqlFunction
      where TFunction29 : IBqlFunction
      where TFunction30 : IBqlFunction
    {
    }

    /// <exclude />
    public class AggregateTo<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8, TFunction9, TFunction10, TFunction11, TFunction12, TFunction13, TFunction14, TFunction15, TFunction16, TFunction17, TFunction18, TFunction19, TFunction20, TFunction21, TFunction22, TFunction23, TFunction24, TFunction25, TFunction26, TFunction27, TFunction28, TFunction29, TFunction30, TFunction31> : 
      SelectFromBase<TTable, TJoins>.Where<TCondition>.Aggregate<To<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8, TFunction9, TFunction10, TFunction11, TFunction12, TFunction13, TFunction14, TFunction15, TFunction16, TFunction17, TFunction18, TFunction19, TFunction20, TFunction21, TFunction22, TFunction23, TFunction24, TFunction25, TFunction26, TFunction27, TFunction28, TFunction29, TFunction30, TFunction31>>
      where TFunction1 : IBqlFunction
      where TFunction2 : IBqlFunction
      where TFunction3 : IBqlFunction
      where TFunction4 : IBqlFunction
      where TFunction5 : IBqlFunction
      where TFunction6 : IBqlFunction
      where TFunction7 : IBqlFunction
      where TFunction8 : IBqlFunction
      where TFunction9 : IBqlFunction
      where TFunction10 : IBqlFunction
      where TFunction11 : IBqlFunction
      where TFunction12 : IBqlFunction
      where TFunction13 : IBqlFunction
      where TFunction14 : IBqlFunction
      where TFunction15 : IBqlFunction
      where TFunction16 : IBqlFunction
      where TFunction17 : IBqlFunction
      where TFunction18 : IBqlFunction
      where TFunction19 : IBqlFunction
      where TFunction20 : IBqlFunction
      where TFunction21 : IBqlFunction
      where TFunction22 : IBqlFunction
      where TFunction23 : IBqlFunction
      where TFunction24 : IBqlFunction
      where TFunction25 : IBqlFunction
      where TFunction26 : IBqlFunction
      where TFunction27 : IBqlFunction
      where TFunction28 : IBqlFunction
      where TFunction29 : IBqlFunction
      where TFunction30 : IBqlFunction
      where TFunction31 : IBqlFunction
    {
    }

    /// <exclude />
    public class AggregateTo<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8, TFunction9, TFunction10, TFunction11, TFunction12, TFunction13, TFunction14, TFunction15, TFunction16, TFunction17, TFunction18, TFunction19, TFunction20, TFunction21, TFunction22, TFunction23, TFunction24, TFunction25, TFunction26, TFunction27, TFunction28, TFunction29, TFunction30, TFunction31, TFunction32> : 
      SelectFromBase<TTable, TJoins>.Where<TCondition>.Aggregate<To<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8, TFunction9, TFunction10, TFunction11, TFunction12, TFunction13, TFunction14, TFunction15, TFunction16, TFunction17, TFunction18, TFunction19, TFunction20, TFunction21, TFunction22, TFunction23, TFunction24, TFunction25, TFunction26, TFunction27, TFunction28, TFunction29, TFunction30, TFunction31, TFunction32>>
      where TFunction1 : IBqlFunction
      where TFunction2 : IBqlFunction
      where TFunction3 : IBqlFunction
      where TFunction4 : IBqlFunction
      where TFunction5 : IBqlFunction
      where TFunction6 : IBqlFunction
      where TFunction7 : IBqlFunction
      where TFunction8 : IBqlFunction
      where TFunction9 : IBqlFunction
      where TFunction10 : IBqlFunction
      where TFunction11 : IBqlFunction
      where TFunction12 : IBqlFunction
      where TFunction13 : IBqlFunction
      where TFunction14 : IBqlFunction
      where TFunction15 : IBqlFunction
      where TFunction16 : IBqlFunction
      where TFunction17 : IBqlFunction
      where TFunction18 : IBqlFunction
      where TFunction19 : IBqlFunction
      where TFunction20 : IBqlFunction
      where TFunction21 : IBqlFunction
      where TFunction22 : IBqlFunction
      where TFunction23 : IBqlFunction
      where TFunction24 : IBqlFunction
      where TFunction25 : IBqlFunction
      where TFunction26 : IBqlFunction
      where TFunction27 : IBqlFunction
      where TFunction28 : IBqlFunction
      where TFunction29 : IBqlFunction
      where TFunction30 : IBqlFunction
      where TFunction31 : IBqlFunction
      where TFunction32 : IBqlFunction
    {
    }

    public class Aggregate<TFunctionArray> : 
      FbqlSelect<SelectFromBase<TTable, TJoins>.Where<TCondition>.Aggregate<TFunctionArray>, TTable, TJoins, TCondition, TFunctionArray, BqlNone, TypeArrayOf<IBqlSortColumn>.Empty>
      where TFunctionArray : ITypeArrayOf<IBqlFunction>, TypeArray.IsNotEmpty
    {
      /// <exclude />
      public class OrderBy<TField> : 
        SelectFromBase<TTable, TJoins>.Where<TCondition>.Aggregate<TFunctionArray>.Order<By<TField>>
        where TField : IBqlSortColumn
      {
      }

      /// <exclude />
      public class OrderBy<TField1, TField2> : 
        SelectFromBase<TTable, TJoins>.Where<TCondition>.Aggregate<TFunctionArray>.Order<By<TField1, TField2>>
        where TField1 : IBqlSortColumn
        where TField2 : IBqlSortColumn
      {
      }

      /// <exclude />
      public class OrderBy<TField1, TField2, TField3> : 
        SelectFromBase<TTable, TJoins>.Where<TCondition>.Aggregate<TFunctionArray>.Order<By<TField1, TField2, TField3>>
        where TField1 : IBqlSortColumn
        where TField2 : IBqlSortColumn
        where TField3 : IBqlSortColumn
      {
      }

      /// <exclude />
      public class OrderBy<TField1, TField2, TField3, TField4> : 
        SelectFromBase<TTable, TJoins>.Where<TCondition>.Aggregate<TFunctionArray>.Order<By<TField1, TField2, TField3, TField4>>
        where TField1 : IBqlSortColumn
        where TField2 : IBqlSortColumn
        where TField3 : IBqlSortColumn
        where TField4 : IBqlSortColumn
      {
      }

      /// <exclude />
      public class OrderBy<TField1, TField2, TField3, TField4, TField5> : 
        SelectFromBase<TTable, TJoins>.Where<TCondition>.Aggregate<TFunctionArray>.Order<By<TField1, TField2, TField3, TField4, TField5>>
        where TField1 : IBqlSortColumn
        where TField2 : IBqlSortColumn
        where TField3 : IBqlSortColumn
        where TField4 : IBqlSortColumn
        where TField5 : IBqlSortColumn
      {
      }

      /// <exclude />
      public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6> : 
        SelectFromBase<TTable, TJoins>.Where<TCondition>.Aggregate<TFunctionArray>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6>>
        where TField1 : IBqlSortColumn
        where TField2 : IBqlSortColumn
        where TField3 : IBqlSortColumn
        where TField4 : IBqlSortColumn
        where TField5 : IBqlSortColumn
        where TField6 : IBqlSortColumn
      {
      }

      /// <exclude />
      public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7> : 
        SelectFromBase<TTable, TJoins>.Where<TCondition>.Aggregate<TFunctionArray>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7>>
        where TField1 : IBqlSortColumn
        where TField2 : IBqlSortColumn
        where TField3 : IBqlSortColumn
        where TField4 : IBqlSortColumn
        where TField5 : IBqlSortColumn
        where TField6 : IBqlSortColumn
        where TField7 : IBqlSortColumn
      {
      }

      /// <exclude />
      public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8> : 
        SelectFromBase<TTable, TJoins>.Where<TCondition>.Aggregate<TFunctionArray>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8>>
        where TField1 : IBqlSortColumn
        where TField2 : IBqlSortColumn
        where TField3 : IBqlSortColumn
        where TField4 : IBqlSortColumn
        where TField5 : IBqlSortColumn
        where TField6 : IBqlSortColumn
        where TField7 : IBqlSortColumn
        where TField8 : IBqlSortColumn
      {
      }

      /// <exclude />
      public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9> : 
        SelectFromBase<TTable, TJoins>.Where<TCondition>.Aggregate<TFunctionArray>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9>>
        where TField1 : IBqlSortColumn
        where TField2 : IBqlSortColumn
        where TField3 : IBqlSortColumn
        where TField4 : IBqlSortColumn
        where TField5 : IBqlSortColumn
        where TField6 : IBqlSortColumn
        where TField7 : IBqlSortColumn
        where TField8 : IBqlSortColumn
        where TField9 : IBqlSortColumn
      {
      }

      /// <exclude />
      public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10> : 
        SelectFromBase<TTable, TJoins>.Where<TCondition>.Aggregate<TFunctionArray>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10>>
        where TField1 : IBqlSortColumn
        where TField2 : IBqlSortColumn
        where TField3 : IBqlSortColumn
        where TField4 : IBqlSortColumn
        where TField5 : IBqlSortColumn
        where TField6 : IBqlSortColumn
        where TField7 : IBqlSortColumn
        where TField8 : IBqlSortColumn
        where TField9 : IBqlSortColumn
        where TField10 : IBqlSortColumn
      {
      }

      /// <exclude />
      public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11> : 
        SelectFromBase<TTable, TJoins>.Where<TCondition>.Aggregate<TFunctionArray>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11>>
        where TField1 : IBqlSortColumn
        where TField2 : IBqlSortColumn
        where TField3 : IBqlSortColumn
        where TField4 : IBqlSortColumn
        where TField5 : IBqlSortColumn
        where TField6 : IBqlSortColumn
        where TField7 : IBqlSortColumn
        where TField8 : IBqlSortColumn
        where TField9 : IBqlSortColumn
        where TField10 : IBqlSortColumn
        where TField11 : IBqlSortColumn
      {
      }

      /// <exclude />
      public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12> : 
        SelectFromBase<TTable, TJoins>.Where<TCondition>.Aggregate<TFunctionArray>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12>>
        where TField1 : IBqlSortColumn
        where TField2 : IBqlSortColumn
        where TField3 : IBqlSortColumn
        where TField4 : IBqlSortColumn
        where TField5 : IBqlSortColumn
        where TField6 : IBqlSortColumn
        where TField7 : IBqlSortColumn
        where TField8 : IBqlSortColumn
        where TField9 : IBqlSortColumn
        where TField10 : IBqlSortColumn
        where TField11 : IBqlSortColumn
        where TField12 : IBqlSortColumn
      {
      }

      /// <exclude />
      public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13> : 
        SelectFromBase<TTable, TJoins>.Where<TCondition>.Aggregate<TFunctionArray>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13>>
        where TField1 : IBqlSortColumn
        where TField2 : IBqlSortColumn
        where TField3 : IBqlSortColumn
        where TField4 : IBqlSortColumn
        where TField5 : IBqlSortColumn
        where TField6 : IBqlSortColumn
        where TField7 : IBqlSortColumn
        where TField8 : IBqlSortColumn
        where TField9 : IBqlSortColumn
        where TField10 : IBqlSortColumn
        where TField11 : IBqlSortColumn
        where TField12 : IBqlSortColumn
        where TField13 : IBqlSortColumn
      {
      }

      /// <exclude />
      public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14> : 
        SelectFromBase<TTable, TJoins>.Where<TCondition>.Aggregate<TFunctionArray>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14>>
        where TField1 : IBqlSortColumn
        where TField2 : IBqlSortColumn
        where TField3 : IBqlSortColumn
        where TField4 : IBqlSortColumn
        where TField5 : IBqlSortColumn
        where TField6 : IBqlSortColumn
        where TField7 : IBqlSortColumn
        where TField8 : IBqlSortColumn
        where TField9 : IBqlSortColumn
        where TField10 : IBqlSortColumn
        where TField11 : IBqlSortColumn
        where TField12 : IBqlSortColumn
        where TField13 : IBqlSortColumn
        where TField14 : IBqlSortColumn
      {
      }

      /// <exclude />
      public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15> : 
        SelectFromBase<TTable, TJoins>.Where<TCondition>.Aggregate<TFunctionArray>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15>>
        where TField1 : IBqlSortColumn
        where TField2 : IBqlSortColumn
        where TField3 : IBqlSortColumn
        where TField4 : IBqlSortColumn
        where TField5 : IBqlSortColumn
        where TField6 : IBqlSortColumn
        where TField7 : IBqlSortColumn
        where TField8 : IBqlSortColumn
        where TField9 : IBqlSortColumn
        where TField10 : IBqlSortColumn
        where TField11 : IBqlSortColumn
        where TField12 : IBqlSortColumn
        where TField13 : IBqlSortColumn
        where TField14 : IBqlSortColumn
        where TField15 : IBqlSortColumn
      {
      }

      /// <exclude />
      public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16> : 
        SelectFromBase<TTable, TJoins>.Where<TCondition>.Aggregate<TFunctionArray>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16>>
        where TField1 : IBqlSortColumn
        where TField2 : IBqlSortColumn
        where TField3 : IBqlSortColumn
        where TField4 : IBqlSortColumn
        where TField5 : IBqlSortColumn
        where TField6 : IBqlSortColumn
        where TField7 : IBqlSortColumn
        where TField8 : IBqlSortColumn
        where TField9 : IBqlSortColumn
        where TField10 : IBqlSortColumn
        where TField11 : IBqlSortColumn
        where TField12 : IBqlSortColumn
        where TField13 : IBqlSortColumn
        where TField14 : IBqlSortColumn
        where TField15 : IBqlSortColumn
        where TField16 : IBqlSortColumn
      {
      }

      /// <exclude />
      public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17> : 
        SelectFromBase<TTable, TJoins>.Where<TCondition>.Aggregate<TFunctionArray>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17>>
        where TField1 : IBqlSortColumn
        where TField2 : IBqlSortColumn
        where TField3 : IBqlSortColumn
        where TField4 : IBqlSortColumn
        where TField5 : IBqlSortColumn
        where TField6 : IBqlSortColumn
        where TField7 : IBqlSortColumn
        where TField8 : IBqlSortColumn
        where TField9 : IBqlSortColumn
        where TField10 : IBqlSortColumn
        where TField11 : IBqlSortColumn
        where TField12 : IBqlSortColumn
        where TField13 : IBqlSortColumn
        where TField14 : IBqlSortColumn
        where TField15 : IBqlSortColumn
        where TField16 : IBqlSortColumn
        where TField17 : IBqlSortColumn
      {
      }

      /// <exclude />
      public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18> : 
        SelectFromBase<TTable, TJoins>.Where<TCondition>.Aggregate<TFunctionArray>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18>>
        where TField1 : IBqlSortColumn
        where TField2 : IBqlSortColumn
        where TField3 : IBqlSortColumn
        where TField4 : IBqlSortColumn
        where TField5 : IBqlSortColumn
        where TField6 : IBqlSortColumn
        where TField7 : IBqlSortColumn
        where TField8 : IBqlSortColumn
        where TField9 : IBqlSortColumn
        where TField10 : IBqlSortColumn
        where TField11 : IBqlSortColumn
        where TField12 : IBqlSortColumn
        where TField13 : IBqlSortColumn
        where TField14 : IBqlSortColumn
        where TField15 : IBqlSortColumn
        where TField16 : IBqlSortColumn
        where TField17 : IBqlSortColumn
        where TField18 : IBqlSortColumn
      {
      }

      /// <exclude />
      public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19> : 
        SelectFromBase<TTable, TJoins>.Where<TCondition>.Aggregate<TFunctionArray>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19>>
        where TField1 : IBqlSortColumn
        where TField2 : IBqlSortColumn
        where TField3 : IBqlSortColumn
        where TField4 : IBqlSortColumn
        where TField5 : IBqlSortColumn
        where TField6 : IBqlSortColumn
        where TField7 : IBqlSortColumn
        where TField8 : IBqlSortColumn
        where TField9 : IBqlSortColumn
        where TField10 : IBqlSortColumn
        where TField11 : IBqlSortColumn
        where TField12 : IBqlSortColumn
        where TField13 : IBqlSortColumn
        where TField14 : IBqlSortColumn
        where TField15 : IBqlSortColumn
        where TField16 : IBqlSortColumn
        where TField17 : IBqlSortColumn
        where TField18 : IBqlSortColumn
        where TField19 : IBqlSortColumn
      {
      }

      /// <exclude />
      public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20> : 
        SelectFromBase<TTable, TJoins>.Where<TCondition>.Aggregate<TFunctionArray>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20>>
        where TField1 : IBqlSortColumn
        where TField2 : IBqlSortColumn
        where TField3 : IBqlSortColumn
        where TField4 : IBqlSortColumn
        where TField5 : IBqlSortColumn
        where TField6 : IBqlSortColumn
        where TField7 : IBqlSortColumn
        where TField8 : IBqlSortColumn
        where TField9 : IBqlSortColumn
        where TField10 : IBqlSortColumn
        where TField11 : IBqlSortColumn
        where TField12 : IBqlSortColumn
        where TField13 : IBqlSortColumn
        where TField14 : IBqlSortColumn
        where TField15 : IBqlSortColumn
        where TField16 : IBqlSortColumn
        where TField17 : IBqlSortColumn
        where TField18 : IBqlSortColumn
        where TField19 : IBqlSortColumn
        where TField20 : IBqlSortColumn
      {
      }

      /// <exclude />
      public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20, TField21> : 
        SelectFromBase<TTable, TJoins>.Where<TCondition>.Aggregate<TFunctionArray>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20, TField21>>
        where TField1 : IBqlSortColumn
        where TField2 : IBqlSortColumn
        where TField3 : IBqlSortColumn
        where TField4 : IBqlSortColumn
        where TField5 : IBqlSortColumn
        where TField6 : IBqlSortColumn
        where TField7 : IBqlSortColumn
        where TField8 : IBqlSortColumn
        where TField9 : IBqlSortColumn
        where TField10 : IBqlSortColumn
        where TField11 : IBqlSortColumn
        where TField12 : IBqlSortColumn
        where TField13 : IBqlSortColumn
        where TField14 : IBqlSortColumn
        where TField15 : IBqlSortColumn
        where TField16 : IBqlSortColumn
        where TField17 : IBqlSortColumn
        where TField18 : IBqlSortColumn
        where TField19 : IBqlSortColumn
        where TField20 : IBqlSortColumn
        where TField21 : IBqlSortColumn
      {
      }

      /// <exclude />
      public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20, TField21, TField22> : 
        SelectFromBase<TTable, TJoins>.Where<TCondition>.Aggregate<TFunctionArray>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20, TField21, TField22>>
        where TField1 : IBqlSortColumn
        where TField2 : IBqlSortColumn
        where TField3 : IBqlSortColumn
        where TField4 : IBqlSortColumn
        where TField5 : IBqlSortColumn
        where TField6 : IBqlSortColumn
        where TField7 : IBqlSortColumn
        where TField8 : IBqlSortColumn
        where TField9 : IBqlSortColumn
        where TField10 : IBqlSortColumn
        where TField11 : IBqlSortColumn
        where TField12 : IBqlSortColumn
        where TField13 : IBqlSortColumn
        where TField14 : IBqlSortColumn
        where TField15 : IBqlSortColumn
        where TField16 : IBqlSortColumn
        where TField17 : IBqlSortColumn
        where TField18 : IBqlSortColumn
        where TField19 : IBqlSortColumn
        where TField20 : IBqlSortColumn
        where TField21 : IBqlSortColumn
        where TField22 : IBqlSortColumn
      {
      }

      /// <exclude />
      public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20, TField21, TField22, TField23> : 
        SelectFromBase<TTable, TJoins>.Where<TCondition>.Aggregate<TFunctionArray>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20, TField21, TField22, TField23>>
        where TField1 : IBqlSortColumn
        where TField2 : IBqlSortColumn
        where TField3 : IBqlSortColumn
        where TField4 : IBqlSortColumn
        where TField5 : IBqlSortColumn
        where TField6 : IBqlSortColumn
        where TField7 : IBqlSortColumn
        where TField8 : IBqlSortColumn
        where TField9 : IBqlSortColumn
        where TField10 : IBqlSortColumn
        where TField11 : IBqlSortColumn
        where TField12 : IBqlSortColumn
        where TField13 : IBqlSortColumn
        where TField14 : IBqlSortColumn
        where TField15 : IBqlSortColumn
        where TField16 : IBqlSortColumn
        where TField17 : IBqlSortColumn
        where TField18 : IBqlSortColumn
        where TField19 : IBqlSortColumn
        where TField20 : IBqlSortColumn
        where TField21 : IBqlSortColumn
        where TField22 : IBqlSortColumn
        where TField23 : IBqlSortColumn
      {
      }

      /// <exclude />
      public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20, TField21, TField22, TField23, TField24> : 
        SelectFromBase<TTable, TJoins>.Where<TCondition>.Aggregate<TFunctionArray>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20, TField21, TField22, TField23, TField24>>
        where TField1 : IBqlSortColumn
        where TField2 : IBqlSortColumn
        where TField3 : IBqlSortColumn
        where TField4 : IBqlSortColumn
        where TField5 : IBqlSortColumn
        where TField6 : IBqlSortColumn
        where TField7 : IBqlSortColumn
        where TField8 : IBqlSortColumn
        where TField9 : IBqlSortColumn
        where TField10 : IBqlSortColumn
        where TField11 : IBqlSortColumn
        where TField12 : IBqlSortColumn
        where TField13 : IBqlSortColumn
        where TField14 : IBqlSortColumn
        where TField15 : IBqlSortColumn
        where TField16 : IBqlSortColumn
        where TField17 : IBqlSortColumn
        where TField18 : IBqlSortColumn
        where TField19 : IBqlSortColumn
        where TField20 : IBqlSortColumn
        where TField21 : IBqlSortColumn
        where TField22 : IBqlSortColumn
        where TField23 : IBqlSortColumn
        where TField24 : IBqlSortColumn
      {
      }

      /// <exclude />
      public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20, TField21, TField22, TField23, TField24, TField25> : 
        SelectFromBase<TTable, TJoins>.Where<TCondition>.Aggregate<TFunctionArray>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20, TField21, TField22, TField23, TField24, TField25>>
        where TField1 : IBqlSortColumn
        where TField2 : IBqlSortColumn
        where TField3 : IBqlSortColumn
        where TField4 : IBqlSortColumn
        where TField5 : IBqlSortColumn
        where TField6 : IBqlSortColumn
        where TField7 : IBqlSortColumn
        where TField8 : IBqlSortColumn
        where TField9 : IBqlSortColumn
        where TField10 : IBqlSortColumn
        where TField11 : IBqlSortColumn
        where TField12 : IBqlSortColumn
        where TField13 : IBqlSortColumn
        where TField14 : IBqlSortColumn
        where TField15 : IBqlSortColumn
        where TField16 : IBqlSortColumn
        where TField17 : IBqlSortColumn
        where TField18 : IBqlSortColumn
        where TField19 : IBqlSortColumn
        where TField20 : IBqlSortColumn
        where TField21 : IBqlSortColumn
        where TField22 : IBqlSortColumn
        where TField23 : IBqlSortColumn
        where TField24 : IBqlSortColumn
        where TField25 : IBqlSortColumn
      {
      }

      /// <exclude />
      public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20, TField21, TField22, TField23, TField24, TField25, TField26> : 
        SelectFromBase<TTable, TJoins>.Where<TCondition>.Aggregate<TFunctionArray>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20, TField21, TField22, TField23, TField24, TField25, TField26>>
        where TField1 : IBqlSortColumn
        where TField2 : IBqlSortColumn
        where TField3 : IBqlSortColumn
        where TField4 : IBqlSortColumn
        where TField5 : IBqlSortColumn
        where TField6 : IBqlSortColumn
        where TField7 : IBqlSortColumn
        where TField8 : IBqlSortColumn
        where TField9 : IBqlSortColumn
        where TField10 : IBqlSortColumn
        where TField11 : IBqlSortColumn
        where TField12 : IBqlSortColumn
        where TField13 : IBqlSortColumn
        where TField14 : IBqlSortColumn
        where TField15 : IBqlSortColumn
        where TField16 : IBqlSortColumn
        where TField17 : IBqlSortColumn
        where TField18 : IBqlSortColumn
        where TField19 : IBqlSortColumn
        where TField20 : IBqlSortColumn
        where TField21 : IBqlSortColumn
        where TField22 : IBqlSortColumn
        where TField23 : IBqlSortColumn
        where TField24 : IBqlSortColumn
        where TField25 : IBqlSortColumn
        where TField26 : IBqlSortColumn
      {
      }

      /// <exclude />
      public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20, TField21, TField22, TField23, TField24, TField25, TField26, TField27> : 
        SelectFromBase<TTable, TJoins>.Where<TCondition>.Aggregate<TFunctionArray>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20, TField21, TField22, TField23, TField24, TField25, TField26, TField27>>
        where TField1 : IBqlSortColumn
        where TField2 : IBqlSortColumn
        where TField3 : IBqlSortColumn
        where TField4 : IBqlSortColumn
        where TField5 : IBqlSortColumn
        where TField6 : IBqlSortColumn
        where TField7 : IBqlSortColumn
        where TField8 : IBqlSortColumn
        where TField9 : IBqlSortColumn
        where TField10 : IBqlSortColumn
        where TField11 : IBqlSortColumn
        where TField12 : IBqlSortColumn
        where TField13 : IBqlSortColumn
        where TField14 : IBqlSortColumn
        where TField15 : IBqlSortColumn
        where TField16 : IBqlSortColumn
        where TField17 : IBqlSortColumn
        where TField18 : IBqlSortColumn
        where TField19 : IBqlSortColumn
        where TField20 : IBqlSortColumn
        where TField21 : IBqlSortColumn
        where TField22 : IBqlSortColumn
        where TField23 : IBqlSortColumn
        where TField24 : IBqlSortColumn
        where TField25 : IBqlSortColumn
        where TField26 : IBqlSortColumn
        where TField27 : IBqlSortColumn
      {
      }

      /// <exclude />
      public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20, TField21, TField22, TField23, TField24, TField25, TField26, TField27, TField28> : 
        SelectFromBase<TTable, TJoins>.Where<TCondition>.Aggregate<TFunctionArray>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20, TField21, TField22, TField23, TField24, TField25, TField26, TField27, TField28>>
        where TField1 : IBqlSortColumn
        where TField2 : IBqlSortColumn
        where TField3 : IBqlSortColumn
        where TField4 : IBqlSortColumn
        where TField5 : IBqlSortColumn
        where TField6 : IBqlSortColumn
        where TField7 : IBqlSortColumn
        where TField8 : IBqlSortColumn
        where TField9 : IBqlSortColumn
        where TField10 : IBqlSortColumn
        where TField11 : IBqlSortColumn
        where TField12 : IBqlSortColumn
        where TField13 : IBqlSortColumn
        where TField14 : IBqlSortColumn
        where TField15 : IBqlSortColumn
        where TField16 : IBqlSortColumn
        where TField17 : IBqlSortColumn
        where TField18 : IBqlSortColumn
        where TField19 : IBqlSortColumn
        where TField20 : IBqlSortColumn
        where TField21 : IBqlSortColumn
        where TField22 : IBqlSortColumn
        where TField23 : IBqlSortColumn
        where TField24 : IBqlSortColumn
        where TField25 : IBqlSortColumn
        where TField26 : IBqlSortColumn
        where TField27 : IBqlSortColumn
        where TField28 : IBqlSortColumn
      {
      }

      /// <exclude />
      public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20, TField21, TField22, TField23, TField24, TField25, TField26, TField27, TField28, TField29> : 
        SelectFromBase<TTable, TJoins>.Where<TCondition>.Aggregate<TFunctionArray>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20, TField21, TField22, TField23, TField24, TField25, TField26, TField27, TField28, TField29>>
        where TField1 : IBqlSortColumn
        where TField2 : IBqlSortColumn
        where TField3 : IBqlSortColumn
        where TField4 : IBqlSortColumn
        where TField5 : IBqlSortColumn
        where TField6 : IBqlSortColumn
        where TField7 : IBqlSortColumn
        where TField8 : IBqlSortColumn
        where TField9 : IBqlSortColumn
        where TField10 : IBqlSortColumn
        where TField11 : IBqlSortColumn
        where TField12 : IBqlSortColumn
        where TField13 : IBqlSortColumn
        where TField14 : IBqlSortColumn
        where TField15 : IBqlSortColumn
        where TField16 : IBqlSortColumn
        where TField17 : IBqlSortColumn
        where TField18 : IBqlSortColumn
        where TField19 : IBqlSortColumn
        where TField20 : IBqlSortColumn
        where TField21 : IBqlSortColumn
        where TField22 : IBqlSortColumn
        where TField23 : IBqlSortColumn
        where TField24 : IBqlSortColumn
        where TField25 : IBqlSortColumn
        where TField26 : IBqlSortColumn
        where TField27 : IBqlSortColumn
        where TField28 : IBqlSortColumn
        where TField29 : IBqlSortColumn
      {
      }

      /// <exclude />
      public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20, TField21, TField22, TField23, TField24, TField25, TField26, TField27, TField28, TField29, TField30> : 
        SelectFromBase<TTable, TJoins>.Where<TCondition>.Aggregate<TFunctionArray>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20, TField21, TField22, TField23, TField24, TField25, TField26, TField27, TField28, TField29, TField30>>
        where TField1 : IBqlSortColumn
        where TField2 : IBqlSortColumn
        where TField3 : IBqlSortColumn
        where TField4 : IBqlSortColumn
        where TField5 : IBqlSortColumn
        where TField6 : IBqlSortColumn
        where TField7 : IBqlSortColumn
        where TField8 : IBqlSortColumn
        where TField9 : IBqlSortColumn
        where TField10 : IBqlSortColumn
        where TField11 : IBqlSortColumn
        where TField12 : IBqlSortColumn
        where TField13 : IBqlSortColumn
        where TField14 : IBqlSortColumn
        where TField15 : IBqlSortColumn
        where TField16 : IBqlSortColumn
        where TField17 : IBqlSortColumn
        where TField18 : IBqlSortColumn
        where TField19 : IBqlSortColumn
        where TField20 : IBqlSortColumn
        where TField21 : IBqlSortColumn
        where TField22 : IBqlSortColumn
        where TField23 : IBqlSortColumn
        where TField24 : IBqlSortColumn
        where TField25 : IBqlSortColumn
        where TField26 : IBqlSortColumn
        where TField27 : IBqlSortColumn
        where TField28 : IBqlSortColumn
        where TField29 : IBqlSortColumn
        where TField30 : IBqlSortColumn
      {
      }

      /// <exclude />
      public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20, TField21, TField22, TField23, TField24, TField25, TField26, TField27, TField28, TField29, TField30, TField31> : 
        SelectFromBase<TTable, TJoins>.Where<TCondition>.Aggregate<TFunctionArray>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20, TField21, TField22, TField23, TField24, TField25, TField26, TField27, TField28, TField29, TField30, TField31>>
        where TField1 : IBqlSortColumn
        where TField2 : IBqlSortColumn
        where TField3 : IBqlSortColumn
        where TField4 : IBqlSortColumn
        where TField5 : IBqlSortColumn
        where TField6 : IBqlSortColumn
        where TField7 : IBqlSortColumn
        where TField8 : IBqlSortColumn
        where TField9 : IBqlSortColumn
        where TField10 : IBqlSortColumn
        where TField11 : IBqlSortColumn
        where TField12 : IBqlSortColumn
        where TField13 : IBqlSortColumn
        where TField14 : IBqlSortColumn
        where TField15 : IBqlSortColumn
        where TField16 : IBqlSortColumn
        where TField17 : IBqlSortColumn
        where TField18 : IBqlSortColumn
        where TField19 : IBqlSortColumn
        where TField20 : IBqlSortColumn
        where TField21 : IBqlSortColumn
        where TField22 : IBqlSortColumn
        where TField23 : IBqlSortColumn
        where TField24 : IBqlSortColumn
        where TField25 : IBqlSortColumn
        where TField26 : IBqlSortColumn
        where TField27 : IBqlSortColumn
        where TField28 : IBqlSortColumn
        where TField29 : IBqlSortColumn
        where TField30 : IBqlSortColumn
        where TField31 : IBqlSortColumn
      {
      }

      /// <exclude />
      public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20, TField21, TField22, TField23, TField24, TField25, TField26, TField27, TField28, TField29, TField30, TField31, TField32> : 
        SelectFromBase<TTable, TJoins>.Where<TCondition>.Aggregate<TFunctionArray>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20, TField21, TField22, TField23, TField24, TField25, TField26, TField27, TField28, TField29, TField30, TField31, TField32>>
        where TField1 : IBqlSortColumn
        where TField2 : IBqlSortColumn
        where TField3 : IBqlSortColumn
        where TField4 : IBqlSortColumn
        where TField5 : IBqlSortColumn
        where TField6 : IBqlSortColumn
        where TField7 : IBqlSortColumn
        where TField8 : IBqlSortColumn
        where TField9 : IBqlSortColumn
        where TField10 : IBqlSortColumn
        where TField11 : IBqlSortColumn
        where TField12 : IBqlSortColumn
        where TField13 : IBqlSortColumn
        where TField14 : IBqlSortColumn
        where TField15 : IBqlSortColumn
        where TField16 : IBqlSortColumn
        where TField17 : IBqlSortColumn
        where TField18 : IBqlSortColumn
        where TField19 : IBqlSortColumn
        where TField20 : IBqlSortColumn
        where TField21 : IBqlSortColumn
        where TField22 : IBqlSortColumn
        where TField23 : IBqlSortColumn
        where TField24 : IBqlSortColumn
        where TField25 : IBqlSortColumn
        where TField26 : IBqlSortColumn
        where TField27 : IBqlSortColumn
        where TField28 : IBqlSortColumn
        where TField29 : IBqlSortColumn
        where TField30 : IBqlSortColumn
        where TField31 : IBqlSortColumn
        where TField32 : IBqlSortColumn
      {
      }

      public class Having<TGroupCondition> : 
        FbqlSelect<SelectFromBase<TTable, TJoins>.Where<TCondition>.Aggregate<TFunctionArray>.Having<TGroupCondition>, TTable, TJoins, TCondition, TFunctionArray, Having<TGroupCondition>, TypeArrayOf<IBqlSortColumn>.Empty>
        where TGroupCondition : IBqlHavingCondition, new()
      {
        /// <exclude />
        public class OrderBy<TField> : 
          SelectFromBase<TTable, TJoins>.Where<TCondition>.Aggregate<TFunctionArray>.Having<TGroupCondition>.Order<By<TField>>
          where TField : IBqlSortColumn
        {
        }

        /// <exclude />
        public class OrderBy<TField1, TField2> : 
          SelectFromBase<TTable, TJoins>.Where<TCondition>.Aggregate<TFunctionArray>.Having<TGroupCondition>.Order<By<TField1, TField2>>
          where TField1 : IBqlSortColumn
          where TField2 : IBqlSortColumn
        {
        }

        /// <exclude />
        public class OrderBy<TField1, TField2, TField3> : 
          SelectFromBase<TTable, TJoins>.Where<TCondition>.Aggregate<TFunctionArray>.Having<TGroupCondition>.Order<By<TField1, TField2, TField3>>
          where TField1 : IBqlSortColumn
          where TField2 : IBqlSortColumn
          where TField3 : IBqlSortColumn
        {
        }

        /// <exclude />
        public class OrderBy<TField1, TField2, TField3, TField4> : 
          SelectFromBase<TTable, TJoins>.Where<TCondition>.Aggregate<TFunctionArray>.Having<TGroupCondition>.Order<By<TField1, TField2, TField3, TField4>>
          where TField1 : IBqlSortColumn
          where TField2 : IBqlSortColumn
          where TField3 : IBqlSortColumn
          where TField4 : IBqlSortColumn
        {
        }

        /// <exclude />
        public class OrderBy<TField1, TField2, TField3, TField4, TField5> : 
          SelectFromBase<TTable, TJoins>.Where<TCondition>.Aggregate<TFunctionArray>.Having<TGroupCondition>.Order<By<TField1, TField2, TField3, TField4, TField5>>
          where TField1 : IBqlSortColumn
          where TField2 : IBqlSortColumn
          where TField3 : IBqlSortColumn
          where TField4 : IBqlSortColumn
          where TField5 : IBqlSortColumn
        {
        }

        /// <exclude />
        public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6> : 
          SelectFromBase<TTable, TJoins>.Where<TCondition>.Aggregate<TFunctionArray>.Having<TGroupCondition>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6>>
          where TField1 : IBqlSortColumn
          where TField2 : IBqlSortColumn
          where TField3 : IBqlSortColumn
          where TField4 : IBqlSortColumn
          where TField5 : IBqlSortColumn
          where TField6 : IBqlSortColumn
        {
        }

        /// <exclude />
        public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7> : 
          SelectFromBase<TTable, TJoins>.Where<TCondition>.Aggregate<TFunctionArray>.Having<TGroupCondition>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7>>
          where TField1 : IBqlSortColumn
          where TField2 : IBqlSortColumn
          where TField3 : IBqlSortColumn
          where TField4 : IBqlSortColumn
          where TField5 : IBqlSortColumn
          where TField6 : IBqlSortColumn
          where TField7 : IBqlSortColumn
        {
        }

        /// <exclude />
        public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8> : 
          SelectFromBase<TTable, TJoins>.Where<TCondition>.Aggregate<TFunctionArray>.Having<TGroupCondition>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8>>
          where TField1 : IBqlSortColumn
          where TField2 : IBqlSortColumn
          where TField3 : IBqlSortColumn
          where TField4 : IBqlSortColumn
          where TField5 : IBqlSortColumn
          where TField6 : IBqlSortColumn
          where TField7 : IBqlSortColumn
          where TField8 : IBqlSortColumn
        {
        }

        /// <exclude />
        public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9> : 
          SelectFromBase<TTable, TJoins>.Where<TCondition>.Aggregate<TFunctionArray>.Having<TGroupCondition>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9>>
          where TField1 : IBqlSortColumn
          where TField2 : IBqlSortColumn
          where TField3 : IBqlSortColumn
          where TField4 : IBqlSortColumn
          where TField5 : IBqlSortColumn
          where TField6 : IBqlSortColumn
          where TField7 : IBqlSortColumn
          where TField8 : IBqlSortColumn
          where TField9 : IBqlSortColumn
        {
        }

        /// <exclude />
        public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10> : 
          SelectFromBase<TTable, TJoins>.Where<TCondition>.Aggregate<TFunctionArray>.Having<TGroupCondition>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10>>
          where TField1 : IBqlSortColumn
          where TField2 : IBqlSortColumn
          where TField3 : IBqlSortColumn
          where TField4 : IBqlSortColumn
          where TField5 : IBqlSortColumn
          where TField6 : IBqlSortColumn
          where TField7 : IBqlSortColumn
          where TField8 : IBqlSortColumn
          where TField9 : IBqlSortColumn
          where TField10 : IBqlSortColumn
        {
        }

        /// <exclude />
        public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11> : 
          SelectFromBase<TTable, TJoins>.Where<TCondition>.Aggregate<TFunctionArray>.Having<TGroupCondition>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11>>
          where TField1 : IBqlSortColumn
          where TField2 : IBqlSortColumn
          where TField3 : IBqlSortColumn
          where TField4 : IBqlSortColumn
          where TField5 : IBqlSortColumn
          where TField6 : IBqlSortColumn
          where TField7 : IBqlSortColumn
          where TField8 : IBqlSortColumn
          where TField9 : IBqlSortColumn
          where TField10 : IBqlSortColumn
          where TField11 : IBqlSortColumn
        {
        }

        /// <exclude />
        public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12> : 
          SelectFromBase<TTable, TJoins>.Where<TCondition>.Aggregate<TFunctionArray>.Having<TGroupCondition>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12>>
          where TField1 : IBqlSortColumn
          where TField2 : IBqlSortColumn
          where TField3 : IBqlSortColumn
          where TField4 : IBqlSortColumn
          where TField5 : IBqlSortColumn
          where TField6 : IBqlSortColumn
          where TField7 : IBqlSortColumn
          where TField8 : IBqlSortColumn
          where TField9 : IBqlSortColumn
          where TField10 : IBqlSortColumn
          where TField11 : IBqlSortColumn
          where TField12 : IBqlSortColumn
        {
        }

        /// <exclude />
        public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13> : 
          SelectFromBase<TTable, TJoins>.Where<TCondition>.Aggregate<TFunctionArray>.Having<TGroupCondition>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13>>
          where TField1 : IBqlSortColumn
          where TField2 : IBqlSortColumn
          where TField3 : IBqlSortColumn
          where TField4 : IBqlSortColumn
          where TField5 : IBqlSortColumn
          where TField6 : IBqlSortColumn
          where TField7 : IBqlSortColumn
          where TField8 : IBqlSortColumn
          where TField9 : IBqlSortColumn
          where TField10 : IBqlSortColumn
          where TField11 : IBqlSortColumn
          where TField12 : IBqlSortColumn
          where TField13 : IBqlSortColumn
        {
        }

        /// <exclude />
        public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14> : 
          SelectFromBase<TTable, TJoins>.Where<TCondition>.Aggregate<TFunctionArray>.Having<TGroupCondition>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14>>
          where TField1 : IBqlSortColumn
          where TField2 : IBqlSortColumn
          where TField3 : IBqlSortColumn
          where TField4 : IBqlSortColumn
          where TField5 : IBqlSortColumn
          where TField6 : IBqlSortColumn
          where TField7 : IBqlSortColumn
          where TField8 : IBqlSortColumn
          where TField9 : IBqlSortColumn
          where TField10 : IBqlSortColumn
          where TField11 : IBqlSortColumn
          where TField12 : IBqlSortColumn
          where TField13 : IBqlSortColumn
          where TField14 : IBqlSortColumn
        {
        }

        /// <exclude />
        public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15> : 
          SelectFromBase<TTable, TJoins>.Where<TCondition>.Aggregate<TFunctionArray>.Having<TGroupCondition>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15>>
          where TField1 : IBqlSortColumn
          where TField2 : IBqlSortColumn
          where TField3 : IBqlSortColumn
          where TField4 : IBqlSortColumn
          where TField5 : IBqlSortColumn
          where TField6 : IBqlSortColumn
          where TField7 : IBqlSortColumn
          where TField8 : IBqlSortColumn
          where TField9 : IBqlSortColumn
          where TField10 : IBqlSortColumn
          where TField11 : IBqlSortColumn
          where TField12 : IBqlSortColumn
          where TField13 : IBqlSortColumn
          where TField14 : IBqlSortColumn
          where TField15 : IBqlSortColumn
        {
        }

        /// <exclude />
        public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16> : 
          SelectFromBase<TTable, TJoins>.Where<TCondition>.Aggregate<TFunctionArray>.Having<TGroupCondition>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16>>
          where TField1 : IBqlSortColumn
          where TField2 : IBqlSortColumn
          where TField3 : IBqlSortColumn
          where TField4 : IBqlSortColumn
          where TField5 : IBqlSortColumn
          where TField6 : IBqlSortColumn
          where TField7 : IBqlSortColumn
          where TField8 : IBqlSortColumn
          where TField9 : IBqlSortColumn
          where TField10 : IBqlSortColumn
          where TField11 : IBqlSortColumn
          where TField12 : IBqlSortColumn
          where TField13 : IBqlSortColumn
          where TField14 : IBqlSortColumn
          where TField15 : IBqlSortColumn
          where TField16 : IBqlSortColumn
        {
        }

        /// <exclude />
        public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17> : 
          SelectFromBase<TTable, TJoins>.Where<TCondition>.Aggregate<TFunctionArray>.Having<TGroupCondition>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17>>
          where TField1 : IBqlSortColumn
          where TField2 : IBqlSortColumn
          where TField3 : IBqlSortColumn
          where TField4 : IBqlSortColumn
          where TField5 : IBqlSortColumn
          where TField6 : IBqlSortColumn
          where TField7 : IBqlSortColumn
          where TField8 : IBqlSortColumn
          where TField9 : IBqlSortColumn
          where TField10 : IBqlSortColumn
          where TField11 : IBqlSortColumn
          where TField12 : IBqlSortColumn
          where TField13 : IBqlSortColumn
          where TField14 : IBqlSortColumn
          where TField15 : IBqlSortColumn
          where TField16 : IBqlSortColumn
          where TField17 : IBqlSortColumn
        {
        }

        /// <exclude />
        public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18> : 
          SelectFromBase<TTable, TJoins>.Where<TCondition>.Aggregate<TFunctionArray>.Having<TGroupCondition>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18>>
          where TField1 : IBqlSortColumn
          where TField2 : IBqlSortColumn
          where TField3 : IBqlSortColumn
          where TField4 : IBqlSortColumn
          where TField5 : IBqlSortColumn
          where TField6 : IBqlSortColumn
          where TField7 : IBqlSortColumn
          where TField8 : IBqlSortColumn
          where TField9 : IBqlSortColumn
          where TField10 : IBqlSortColumn
          where TField11 : IBqlSortColumn
          where TField12 : IBqlSortColumn
          where TField13 : IBqlSortColumn
          where TField14 : IBqlSortColumn
          where TField15 : IBqlSortColumn
          where TField16 : IBqlSortColumn
          where TField17 : IBqlSortColumn
          where TField18 : IBqlSortColumn
        {
        }

        /// <exclude />
        public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19> : 
          SelectFromBase<TTable, TJoins>.Where<TCondition>.Aggregate<TFunctionArray>.Having<TGroupCondition>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19>>
          where TField1 : IBqlSortColumn
          where TField2 : IBqlSortColumn
          where TField3 : IBqlSortColumn
          where TField4 : IBqlSortColumn
          where TField5 : IBqlSortColumn
          where TField6 : IBqlSortColumn
          where TField7 : IBqlSortColumn
          where TField8 : IBqlSortColumn
          where TField9 : IBqlSortColumn
          where TField10 : IBqlSortColumn
          where TField11 : IBqlSortColumn
          where TField12 : IBqlSortColumn
          where TField13 : IBqlSortColumn
          where TField14 : IBqlSortColumn
          where TField15 : IBqlSortColumn
          where TField16 : IBqlSortColumn
          where TField17 : IBqlSortColumn
          where TField18 : IBqlSortColumn
          where TField19 : IBqlSortColumn
        {
        }

        /// <exclude />
        public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20> : 
          SelectFromBase<TTable, TJoins>.Where<TCondition>.Aggregate<TFunctionArray>.Having<TGroupCondition>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20>>
          where TField1 : IBqlSortColumn
          where TField2 : IBqlSortColumn
          where TField3 : IBqlSortColumn
          where TField4 : IBqlSortColumn
          where TField5 : IBqlSortColumn
          where TField6 : IBqlSortColumn
          where TField7 : IBqlSortColumn
          where TField8 : IBqlSortColumn
          where TField9 : IBqlSortColumn
          where TField10 : IBqlSortColumn
          where TField11 : IBqlSortColumn
          where TField12 : IBqlSortColumn
          where TField13 : IBqlSortColumn
          where TField14 : IBqlSortColumn
          where TField15 : IBqlSortColumn
          where TField16 : IBqlSortColumn
          where TField17 : IBqlSortColumn
          where TField18 : IBqlSortColumn
          where TField19 : IBqlSortColumn
          where TField20 : IBqlSortColumn
        {
        }

        /// <exclude />
        public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20, TField21> : 
          SelectFromBase<TTable, TJoins>.Where<TCondition>.Aggregate<TFunctionArray>.Having<TGroupCondition>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20, TField21>>
          where TField1 : IBqlSortColumn
          where TField2 : IBqlSortColumn
          where TField3 : IBqlSortColumn
          where TField4 : IBqlSortColumn
          where TField5 : IBqlSortColumn
          where TField6 : IBqlSortColumn
          where TField7 : IBqlSortColumn
          where TField8 : IBqlSortColumn
          where TField9 : IBqlSortColumn
          where TField10 : IBqlSortColumn
          where TField11 : IBqlSortColumn
          where TField12 : IBqlSortColumn
          where TField13 : IBqlSortColumn
          where TField14 : IBqlSortColumn
          where TField15 : IBqlSortColumn
          where TField16 : IBqlSortColumn
          where TField17 : IBqlSortColumn
          where TField18 : IBqlSortColumn
          where TField19 : IBqlSortColumn
          where TField20 : IBqlSortColumn
          where TField21 : IBqlSortColumn
        {
        }

        /// <exclude />
        public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20, TField21, TField22> : 
          SelectFromBase<TTable, TJoins>.Where<TCondition>.Aggregate<TFunctionArray>.Having<TGroupCondition>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20, TField21, TField22>>
          where TField1 : IBqlSortColumn
          where TField2 : IBqlSortColumn
          where TField3 : IBqlSortColumn
          where TField4 : IBqlSortColumn
          where TField5 : IBqlSortColumn
          where TField6 : IBqlSortColumn
          where TField7 : IBqlSortColumn
          where TField8 : IBqlSortColumn
          where TField9 : IBqlSortColumn
          where TField10 : IBqlSortColumn
          where TField11 : IBqlSortColumn
          where TField12 : IBqlSortColumn
          where TField13 : IBqlSortColumn
          where TField14 : IBqlSortColumn
          where TField15 : IBqlSortColumn
          where TField16 : IBqlSortColumn
          where TField17 : IBqlSortColumn
          where TField18 : IBqlSortColumn
          where TField19 : IBqlSortColumn
          where TField20 : IBqlSortColumn
          where TField21 : IBqlSortColumn
          where TField22 : IBqlSortColumn
        {
        }

        /// <exclude />
        public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20, TField21, TField22, TField23> : 
          SelectFromBase<TTable, TJoins>.Where<TCondition>.Aggregate<TFunctionArray>.Having<TGroupCondition>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20, TField21, TField22, TField23>>
          where TField1 : IBqlSortColumn
          where TField2 : IBqlSortColumn
          where TField3 : IBqlSortColumn
          where TField4 : IBqlSortColumn
          where TField5 : IBqlSortColumn
          where TField6 : IBqlSortColumn
          where TField7 : IBqlSortColumn
          where TField8 : IBqlSortColumn
          where TField9 : IBqlSortColumn
          where TField10 : IBqlSortColumn
          where TField11 : IBqlSortColumn
          where TField12 : IBqlSortColumn
          where TField13 : IBqlSortColumn
          where TField14 : IBqlSortColumn
          where TField15 : IBqlSortColumn
          where TField16 : IBqlSortColumn
          where TField17 : IBqlSortColumn
          where TField18 : IBqlSortColumn
          where TField19 : IBqlSortColumn
          where TField20 : IBqlSortColumn
          where TField21 : IBqlSortColumn
          where TField22 : IBqlSortColumn
          where TField23 : IBqlSortColumn
        {
        }

        /// <exclude />
        public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20, TField21, TField22, TField23, TField24> : 
          SelectFromBase<TTable, TJoins>.Where<TCondition>.Aggregate<TFunctionArray>.Having<TGroupCondition>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20, TField21, TField22, TField23, TField24>>
          where TField1 : IBqlSortColumn
          where TField2 : IBqlSortColumn
          where TField3 : IBqlSortColumn
          where TField4 : IBqlSortColumn
          where TField5 : IBqlSortColumn
          where TField6 : IBqlSortColumn
          where TField7 : IBqlSortColumn
          where TField8 : IBqlSortColumn
          where TField9 : IBqlSortColumn
          where TField10 : IBqlSortColumn
          where TField11 : IBqlSortColumn
          where TField12 : IBqlSortColumn
          where TField13 : IBqlSortColumn
          where TField14 : IBqlSortColumn
          where TField15 : IBqlSortColumn
          where TField16 : IBqlSortColumn
          where TField17 : IBqlSortColumn
          where TField18 : IBqlSortColumn
          where TField19 : IBqlSortColumn
          where TField20 : IBqlSortColumn
          where TField21 : IBqlSortColumn
          where TField22 : IBqlSortColumn
          where TField23 : IBqlSortColumn
          where TField24 : IBqlSortColumn
        {
        }

        /// <exclude />
        public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20, TField21, TField22, TField23, TField24, TField25> : 
          SelectFromBase<TTable, TJoins>.Where<TCondition>.Aggregate<TFunctionArray>.Having<TGroupCondition>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20, TField21, TField22, TField23, TField24, TField25>>
          where TField1 : IBqlSortColumn
          where TField2 : IBqlSortColumn
          where TField3 : IBqlSortColumn
          where TField4 : IBqlSortColumn
          where TField5 : IBqlSortColumn
          where TField6 : IBqlSortColumn
          where TField7 : IBqlSortColumn
          where TField8 : IBqlSortColumn
          where TField9 : IBqlSortColumn
          where TField10 : IBqlSortColumn
          where TField11 : IBqlSortColumn
          where TField12 : IBqlSortColumn
          where TField13 : IBqlSortColumn
          where TField14 : IBqlSortColumn
          where TField15 : IBqlSortColumn
          where TField16 : IBqlSortColumn
          where TField17 : IBqlSortColumn
          where TField18 : IBqlSortColumn
          where TField19 : IBqlSortColumn
          where TField20 : IBqlSortColumn
          where TField21 : IBqlSortColumn
          where TField22 : IBqlSortColumn
          where TField23 : IBqlSortColumn
          where TField24 : IBqlSortColumn
          where TField25 : IBqlSortColumn
        {
        }

        /// <exclude />
        public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20, TField21, TField22, TField23, TField24, TField25, TField26> : 
          SelectFromBase<TTable, TJoins>.Where<TCondition>.Aggregate<TFunctionArray>.Having<TGroupCondition>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20, TField21, TField22, TField23, TField24, TField25, TField26>>
          where TField1 : IBqlSortColumn
          where TField2 : IBqlSortColumn
          where TField3 : IBqlSortColumn
          where TField4 : IBqlSortColumn
          where TField5 : IBqlSortColumn
          where TField6 : IBqlSortColumn
          where TField7 : IBqlSortColumn
          where TField8 : IBqlSortColumn
          where TField9 : IBqlSortColumn
          where TField10 : IBqlSortColumn
          where TField11 : IBqlSortColumn
          where TField12 : IBqlSortColumn
          where TField13 : IBqlSortColumn
          where TField14 : IBqlSortColumn
          where TField15 : IBqlSortColumn
          where TField16 : IBqlSortColumn
          where TField17 : IBqlSortColumn
          where TField18 : IBqlSortColumn
          where TField19 : IBqlSortColumn
          where TField20 : IBqlSortColumn
          where TField21 : IBqlSortColumn
          where TField22 : IBqlSortColumn
          where TField23 : IBqlSortColumn
          where TField24 : IBqlSortColumn
          where TField25 : IBqlSortColumn
          where TField26 : IBqlSortColumn
        {
        }

        /// <exclude />
        public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20, TField21, TField22, TField23, TField24, TField25, TField26, TField27> : 
          SelectFromBase<TTable, TJoins>.Where<TCondition>.Aggregate<TFunctionArray>.Having<TGroupCondition>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20, TField21, TField22, TField23, TField24, TField25, TField26, TField27>>
          where TField1 : IBqlSortColumn
          where TField2 : IBqlSortColumn
          where TField3 : IBqlSortColumn
          where TField4 : IBqlSortColumn
          where TField5 : IBqlSortColumn
          where TField6 : IBqlSortColumn
          where TField7 : IBqlSortColumn
          where TField8 : IBqlSortColumn
          where TField9 : IBqlSortColumn
          where TField10 : IBqlSortColumn
          where TField11 : IBqlSortColumn
          where TField12 : IBqlSortColumn
          where TField13 : IBqlSortColumn
          where TField14 : IBqlSortColumn
          where TField15 : IBqlSortColumn
          where TField16 : IBqlSortColumn
          where TField17 : IBqlSortColumn
          where TField18 : IBqlSortColumn
          where TField19 : IBqlSortColumn
          where TField20 : IBqlSortColumn
          where TField21 : IBqlSortColumn
          where TField22 : IBqlSortColumn
          where TField23 : IBqlSortColumn
          where TField24 : IBqlSortColumn
          where TField25 : IBqlSortColumn
          where TField26 : IBqlSortColumn
          where TField27 : IBqlSortColumn
        {
        }

        /// <exclude />
        public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20, TField21, TField22, TField23, TField24, TField25, TField26, TField27, TField28> : 
          SelectFromBase<TTable, TJoins>.Where<TCondition>.Aggregate<TFunctionArray>.Having<TGroupCondition>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20, TField21, TField22, TField23, TField24, TField25, TField26, TField27, TField28>>
          where TField1 : IBqlSortColumn
          where TField2 : IBqlSortColumn
          where TField3 : IBqlSortColumn
          where TField4 : IBqlSortColumn
          where TField5 : IBqlSortColumn
          where TField6 : IBqlSortColumn
          where TField7 : IBqlSortColumn
          where TField8 : IBqlSortColumn
          where TField9 : IBqlSortColumn
          where TField10 : IBqlSortColumn
          where TField11 : IBqlSortColumn
          where TField12 : IBqlSortColumn
          where TField13 : IBqlSortColumn
          where TField14 : IBqlSortColumn
          where TField15 : IBqlSortColumn
          where TField16 : IBqlSortColumn
          where TField17 : IBqlSortColumn
          where TField18 : IBqlSortColumn
          where TField19 : IBqlSortColumn
          where TField20 : IBqlSortColumn
          where TField21 : IBqlSortColumn
          where TField22 : IBqlSortColumn
          where TField23 : IBqlSortColumn
          where TField24 : IBqlSortColumn
          where TField25 : IBqlSortColumn
          where TField26 : IBqlSortColumn
          where TField27 : IBqlSortColumn
          where TField28 : IBqlSortColumn
        {
        }

        /// <exclude />
        public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20, TField21, TField22, TField23, TField24, TField25, TField26, TField27, TField28, TField29> : 
          SelectFromBase<TTable, TJoins>.Where<TCondition>.Aggregate<TFunctionArray>.Having<TGroupCondition>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20, TField21, TField22, TField23, TField24, TField25, TField26, TField27, TField28, TField29>>
          where TField1 : IBqlSortColumn
          where TField2 : IBqlSortColumn
          where TField3 : IBqlSortColumn
          where TField4 : IBqlSortColumn
          where TField5 : IBqlSortColumn
          where TField6 : IBqlSortColumn
          where TField7 : IBqlSortColumn
          where TField8 : IBqlSortColumn
          where TField9 : IBqlSortColumn
          where TField10 : IBqlSortColumn
          where TField11 : IBqlSortColumn
          where TField12 : IBqlSortColumn
          where TField13 : IBqlSortColumn
          where TField14 : IBqlSortColumn
          where TField15 : IBqlSortColumn
          where TField16 : IBqlSortColumn
          where TField17 : IBqlSortColumn
          where TField18 : IBqlSortColumn
          where TField19 : IBqlSortColumn
          where TField20 : IBqlSortColumn
          where TField21 : IBqlSortColumn
          where TField22 : IBqlSortColumn
          where TField23 : IBqlSortColumn
          where TField24 : IBqlSortColumn
          where TField25 : IBqlSortColumn
          where TField26 : IBqlSortColumn
          where TField27 : IBqlSortColumn
          where TField28 : IBqlSortColumn
          where TField29 : IBqlSortColumn
        {
        }

        /// <exclude />
        public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20, TField21, TField22, TField23, TField24, TField25, TField26, TField27, TField28, TField29, TField30> : 
          SelectFromBase<TTable, TJoins>.Where<TCondition>.Aggregate<TFunctionArray>.Having<TGroupCondition>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20, TField21, TField22, TField23, TField24, TField25, TField26, TField27, TField28, TField29, TField30>>
          where TField1 : IBqlSortColumn
          where TField2 : IBqlSortColumn
          where TField3 : IBqlSortColumn
          where TField4 : IBqlSortColumn
          where TField5 : IBqlSortColumn
          where TField6 : IBqlSortColumn
          where TField7 : IBqlSortColumn
          where TField8 : IBqlSortColumn
          where TField9 : IBqlSortColumn
          where TField10 : IBqlSortColumn
          where TField11 : IBqlSortColumn
          where TField12 : IBqlSortColumn
          where TField13 : IBqlSortColumn
          where TField14 : IBqlSortColumn
          where TField15 : IBqlSortColumn
          where TField16 : IBqlSortColumn
          where TField17 : IBqlSortColumn
          where TField18 : IBqlSortColumn
          where TField19 : IBqlSortColumn
          where TField20 : IBqlSortColumn
          where TField21 : IBqlSortColumn
          where TField22 : IBqlSortColumn
          where TField23 : IBqlSortColumn
          where TField24 : IBqlSortColumn
          where TField25 : IBqlSortColumn
          where TField26 : IBqlSortColumn
          where TField27 : IBqlSortColumn
          where TField28 : IBqlSortColumn
          where TField29 : IBqlSortColumn
          where TField30 : IBqlSortColumn
        {
        }

        /// <exclude />
        public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20, TField21, TField22, TField23, TField24, TField25, TField26, TField27, TField28, TField29, TField30, TField31> : 
          SelectFromBase<TTable, TJoins>.Where<TCondition>.Aggregate<TFunctionArray>.Having<TGroupCondition>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20, TField21, TField22, TField23, TField24, TField25, TField26, TField27, TField28, TField29, TField30, TField31>>
          where TField1 : IBqlSortColumn
          where TField2 : IBqlSortColumn
          where TField3 : IBqlSortColumn
          where TField4 : IBqlSortColumn
          where TField5 : IBqlSortColumn
          where TField6 : IBqlSortColumn
          where TField7 : IBqlSortColumn
          where TField8 : IBqlSortColumn
          where TField9 : IBqlSortColumn
          where TField10 : IBqlSortColumn
          where TField11 : IBqlSortColumn
          where TField12 : IBqlSortColumn
          where TField13 : IBqlSortColumn
          where TField14 : IBqlSortColumn
          where TField15 : IBqlSortColumn
          where TField16 : IBqlSortColumn
          where TField17 : IBqlSortColumn
          where TField18 : IBqlSortColumn
          where TField19 : IBqlSortColumn
          where TField20 : IBqlSortColumn
          where TField21 : IBqlSortColumn
          where TField22 : IBqlSortColumn
          where TField23 : IBqlSortColumn
          where TField24 : IBqlSortColumn
          where TField25 : IBqlSortColumn
          where TField26 : IBqlSortColumn
          where TField27 : IBqlSortColumn
          where TField28 : IBqlSortColumn
          where TField29 : IBqlSortColumn
          where TField30 : IBqlSortColumn
          where TField31 : IBqlSortColumn
        {
        }

        /// <exclude />
        public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20, TField21, TField22, TField23, TField24, TField25, TField26, TField27, TField28, TField29, TField30, TField31, TField32> : 
          SelectFromBase<TTable, TJoins>.Where<TCondition>.Aggregate<TFunctionArray>.Having<TGroupCondition>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20, TField21, TField22, TField23, TField24, TField25, TField26, TField27, TField28, TField29, TField30, TField31, TField32>>
          where TField1 : IBqlSortColumn
          where TField2 : IBqlSortColumn
          where TField3 : IBqlSortColumn
          where TField4 : IBqlSortColumn
          where TField5 : IBqlSortColumn
          where TField6 : IBqlSortColumn
          where TField7 : IBqlSortColumn
          where TField8 : IBqlSortColumn
          where TField9 : IBqlSortColumn
          where TField10 : IBqlSortColumn
          where TField11 : IBqlSortColumn
          where TField12 : IBqlSortColumn
          where TField13 : IBqlSortColumn
          where TField14 : IBqlSortColumn
          where TField15 : IBqlSortColumn
          where TField16 : IBqlSortColumn
          where TField17 : IBqlSortColumn
          where TField18 : IBqlSortColumn
          where TField19 : IBqlSortColumn
          where TField20 : IBqlSortColumn
          where TField21 : IBqlSortColumn
          where TField22 : IBqlSortColumn
          where TField23 : IBqlSortColumn
          where TField24 : IBqlSortColumn
          where TField25 : IBqlSortColumn
          where TField26 : IBqlSortColumn
          where TField27 : IBqlSortColumn
          where TField28 : IBqlSortColumn
          where TField29 : IBqlSortColumn
          where TField30 : IBqlSortColumn
          where TField31 : IBqlSortColumn
          where TField32 : IBqlSortColumn
        {
        }

        public class Order<TSortColumnArray> : 
          FbqlSelect<SelectFromBase<TTable, TJoins>.Where<TCondition>.Aggregate<TFunctionArray>.Having<TGroupCondition>.Order<TSortColumnArray>, TTable, TJoins, TCondition, TFunctionArray, Having<TGroupCondition>, TSortColumnArray>
          where TSortColumnArray : ITypeArrayOf<IBqlSortColumn>, TypeArray.IsNotEmpty
        {
        }
      }

      public class Order<TSortColumnArray> : 
        FbqlSelect<SelectFromBase<TTable, TJoins>.Where<TCondition>.Aggregate<TFunctionArray>.Order<TSortColumnArray>, TTable, TJoins, TCondition, TFunctionArray, BqlNone, TSortColumnArray>
        where TSortColumnArray : ITypeArrayOf<IBqlSortColumn>, TypeArray.IsNotEmpty
      {
      }
    }

    /// <exclude />
    public class OrderBy<TField> : SelectFromBase<TTable, TJoins>.Where<TCondition>.Order<By<TField>>
      where TField : IBqlSortColumn
    {
    }

    /// <exclude />
    public class OrderBy<TField1, TField2> : 
      SelectFromBase<TTable, TJoins>.Where<TCondition>.Order<By<TField1, TField2>>
      where TField1 : IBqlSortColumn
      where TField2 : IBqlSortColumn
    {
    }

    /// <exclude />
    public class OrderBy<TField1, TField2, TField3> : 
      SelectFromBase<TTable, TJoins>.Where<TCondition>.Order<By<TField1, TField2, TField3>>
      where TField1 : IBqlSortColumn
      where TField2 : IBqlSortColumn
      where TField3 : IBqlSortColumn
    {
    }

    /// <exclude />
    public class OrderBy<TField1, TField2, TField3, TField4> : 
      SelectFromBase<TTable, TJoins>.Where<TCondition>.Order<By<TField1, TField2, TField3, TField4>>
      where TField1 : IBqlSortColumn
      where TField2 : IBqlSortColumn
      where TField3 : IBqlSortColumn
      where TField4 : IBqlSortColumn
    {
    }

    /// <exclude />
    public class OrderBy<TField1, TField2, TField3, TField4, TField5> : 
      SelectFromBase<TTable, TJoins>.Where<TCondition>.Order<By<TField1, TField2, TField3, TField4, TField5>>
      where TField1 : IBqlSortColumn
      where TField2 : IBqlSortColumn
      where TField3 : IBqlSortColumn
      where TField4 : IBqlSortColumn
      where TField5 : IBqlSortColumn
    {
    }

    /// <exclude />
    public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6> : 
      SelectFromBase<TTable, TJoins>.Where<TCondition>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6>>
      where TField1 : IBqlSortColumn
      where TField2 : IBqlSortColumn
      where TField3 : IBqlSortColumn
      where TField4 : IBqlSortColumn
      where TField5 : IBqlSortColumn
      where TField6 : IBqlSortColumn
    {
    }

    /// <exclude />
    public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7> : 
      SelectFromBase<TTable, TJoins>.Where<TCondition>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7>>
      where TField1 : IBqlSortColumn
      where TField2 : IBqlSortColumn
      where TField3 : IBqlSortColumn
      where TField4 : IBqlSortColumn
      where TField5 : IBqlSortColumn
      where TField6 : IBqlSortColumn
      where TField7 : IBqlSortColumn
    {
    }

    /// <exclude />
    public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8> : 
      SelectFromBase<TTable, TJoins>.Where<TCondition>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8>>
      where TField1 : IBqlSortColumn
      where TField2 : IBqlSortColumn
      where TField3 : IBqlSortColumn
      where TField4 : IBqlSortColumn
      where TField5 : IBqlSortColumn
      where TField6 : IBqlSortColumn
      where TField7 : IBqlSortColumn
      where TField8 : IBqlSortColumn
    {
    }

    /// <exclude />
    public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9> : 
      SelectFromBase<TTable, TJoins>.Where<TCondition>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9>>
      where TField1 : IBqlSortColumn
      where TField2 : IBqlSortColumn
      where TField3 : IBqlSortColumn
      where TField4 : IBqlSortColumn
      where TField5 : IBqlSortColumn
      where TField6 : IBqlSortColumn
      where TField7 : IBqlSortColumn
      where TField8 : IBqlSortColumn
      where TField9 : IBqlSortColumn
    {
    }

    /// <exclude />
    public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10> : 
      SelectFromBase<TTable, TJoins>.Where<TCondition>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10>>
      where TField1 : IBqlSortColumn
      where TField2 : IBqlSortColumn
      where TField3 : IBqlSortColumn
      where TField4 : IBqlSortColumn
      where TField5 : IBqlSortColumn
      where TField6 : IBqlSortColumn
      where TField7 : IBqlSortColumn
      where TField8 : IBqlSortColumn
      where TField9 : IBqlSortColumn
      where TField10 : IBqlSortColumn
    {
    }

    /// <exclude />
    public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11> : 
      SelectFromBase<TTable, TJoins>.Where<TCondition>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11>>
      where TField1 : IBqlSortColumn
      where TField2 : IBqlSortColumn
      where TField3 : IBqlSortColumn
      where TField4 : IBqlSortColumn
      where TField5 : IBqlSortColumn
      where TField6 : IBqlSortColumn
      where TField7 : IBqlSortColumn
      where TField8 : IBqlSortColumn
      where TField9 : IBqlSortColumn
      where TField10 : IBqlSortColumn
      where TField11 : IBqlSortColumn
    {
    }

    /// <exclude />
    public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12> : 
      SelectFromBase<TTable, TJoins>.Where<TCondition>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12>>
      where TField1 : IBqlSortColumn
      where TField2 : IBqlSortColumn
      where TField3 : IBqlSortColumn
      where TField4 : IBqlSortColumn
      where TField5 : IBqlSortColumn
      where TField6 : IBqlSortColumn
      where TField7 : IBqlSortColumn
      where TField8 : IBqlSortColumn
      where TField9 : IBqlSortColumn
      where TField10 : IBqlSortColumn
      where TField11 : IBqlSortColumn
      where TField12 : IBqlSortColumn
    {
    }

    /// <exclude />
    public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13> : 
      SelectFromBase<TTable, TJoins>.Where<TCondition>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13>>
      where TField1 : IBqlSortColumn
      where TField2 : IBqlSortColumn
      where TField3 : IBqlSortColumn
      where TField4 : IBqlSortColumn
      where TField5 : IBqlSortColumn
      where TField6 : IBqlSortColumn
      where TField7 : IBqlSortColumn
      where TField8 : IBqlSortColumn
      where TField9 : IBqlSortColumn
      where TField10 : IBqlSortColumn
      where TField11 : IBqlSortColumn
      where TField12 : IBqlSortColumn
      where TField13 : IBqlSortColumn
    {
    }

    /// <exclude />
    public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14> : 
      SelectFromBase<TTable, TJoins>.Where<TCondition>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14>>
      where TField1 : IBqlSortColumn
      where TField2 : IBqlSortColumn
      where TField3 : IBqlSortColumn
      where TField4 : IBqlSortColumn
      where TField5 : IBqlSortColumn
      where TField6 : IBqlSortColumn
      where TField7 : IBqlSortColumn
      where TField8 : IBqlSortColumn
      where TField9 : IBqlSortColumn
      where TField10 : IBqlSortColumn
      where TField11 : IBqlSortColumn
      where TField12 : IBqlSortColumn
      where TField13 : IBqlSortColumn
      where TField14 : IBqlSortColumn
    {
    }

    /// <exclude />
    public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15> : 
      SelectFromBase<TTable, TJoins>.Where<TCondition>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15>>
      where TField1 : IBqlSortColumn
      where TField2 : IBqlSortColumn
      where TField3 : IBqlSortColumn
      where TField4 : IBqlSortColumn
      where TField5 : IBqlSortColumn
      where TField6 : IBqlSortColumn
      where TField7 : IBqlSortColumn
      where TField8 : IBqlSortColumn
      where TField9 : IBqlSortColumn
      where TField10 : IBqlSortColumn
      where TField11 : IBqlSortColumn
      where TField12 : IBqlSortColumn
      where TField13 : IBqlSortColumn
      where TField14 : IBqlSortColumn
      where TField15 : IBqlSortColumn
    {
    }

    /// <exclude />
    public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16> : 
      SelectFromBase<TTable, TJoins>.Where<TCondition>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16>>
      where TField1 : IBqlSortColumn
      where TField2 : IBqlSortColumn
      where TField3 : IBqlSortColumn
      where TField4 : IBqlSortColumn
      where TField5 : IBqlSortColumn
      where TField6 : IBqlSortColumn
      where TField7 : IBqlSortColumn
      where TField8 : IBqlSortColumn
      where TField9 : IBqlSortColumn
      where TField10 : IBqlSortColumn
      where TField11 : IBqlSortColumn
      where TField12 : IBqlSortColumn
      where TField13 : IBqlSortColumn
      where TField14 : IBqlSortColumn
      where TField15 : IBqlSortColumn
      where TField16 : IBqlSortColumn
    {
    }

    /// <exclude />
    public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17> : 
      SelectFromBase<TTable, TJoins>.Where<TCondition>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17>>
      where TField1 : IBqlSortColumn
      where TField2 : IBqlSortColumn
      where TField3 : IBqlSortColumn
      where TField4 : IBqlSortColumn
      where TField5 : IBqlSortColumn
      where TField6 : IBqlSortColumn
      where TField7 : IBqlSortColumn
      where TField8 : IBqlSortColumn
      where TField9 : IBqlSortColumn
      where TField10 : IBqlSortColumn
      where TField11 : IBqlSortColumn
      where TField12 : IBqlSortColumn
      where TField13 : IBqlSortColumn
      where TField14 : IBqlSortColumn
      where TField15 : IBqlSortColumn
      where TField16 : IBqlSortColumn
      where TField17 : IBqlSortColumn
    {
    }

    /// <exclude />
    public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18> : 
      SelectFromBase<TTable, TJoins>.Where<TCondition>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18>>
      where TField1 : IBqlSortColumn
      where TField2 : IBqlSortColumn
      where TField3 : IBqlSortColumn
      where TField4 : IBqlSortColumn
      where TField5 : IBqlSortColumn
      where TField6 : IBqlSortColumn
      where TField7 : IBqlSortColumn
      where TField8 : IBqlSortColumn
      where TField9 : IBqlSortColumn
      where TField10 : IBqlSortColumn
      where TField11 : IBqlSortColumn
      where TField12 : IBqlSortColumn
      where TField13 : IBqlSortColumn
      where TField14 : IBqlSortColumn
      where TField15 : IBqlSortColumn
      where TField16 : IBqlSortColumn
      where TField17 : IBqlSortColumn
      where TField18 : IBqlSortColumn
    {
    }

    /// <exclude />
    public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19> : 
      SelectFromBase<TTable, TJoins>.Where<TCondition>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19>>
      where TField1 : IBqlSortColumn
      where TField2 : IBqlSortColumn
      where TField3 : IBqlSortColumn
      where TField4 : IBqlSortColumn
      where TField5 : IBqlSortColumn
      where TField6 : IBqlSortColumn
      where TField7 : IBqlSortColumn
      where TField8 : IBqlSortColumn
      where TField9 : IBqlSortColumn
      where TField10 : IBqlSortColumn
      where TField11 : IBqlSortColumn
      where TField12 : IBqlSortColumn
      where TField13 : IBqlSortColumn
      where TField14 : IBqlSortColumn
      where TField15 : IBqlSortColumn
      where TField16 : IBqlSortColumn
      where TField17 : IBqlSortColumn
      where TField18 : IBqlSortColumn
      where TField19 : IBqlSortColumn
    {
    }

    /// <exclude />
    public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20> : 
      SelectFromBase<TTable, TJoins>.Where<TCondition>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20>>
      where TField1 : IBqlSortColumn
      where TField2 : IBqlSortColumn
      where TField3 : IBqlSortColumn
      where TField4 : IBqlSortColumn
      where TField5 : IBqlSortColumn
      where TField6 : IBqlSortColumn
      where TField7 : IBqlSortColumn
      where TField8 : IBqlSortColumn
      where TField9 : IBqlSortColumn
      where TField10 : IBqlSortColumn
      where TField11 : IBqlSortColumn
      where TField12 : IBqlSortColumn
      where TField13 : IBqlSortColumn
      where TField14 : IBqlSortColumn
      where TField15 : IBqlSortColumn
      where TField16 : IBqlSortColumn
      where TField17 : IBqlSortColumn
      where TField18 : IBqlSortColumn
      where TField19 : IBqlSortColumn
      where TField20 : IBqlSortColumn
    {
    }

    /// <exclude />
    public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20, TField21> : 
      SelectFromBase<TTable, TJoins>.Where<TCondition>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20, TField21>>
      where TField1 : IBqlSortColumn
      where TField2 : IBqlSortColumn
      where TField3 : IBqlSortColumn
      where TField4 : IBqlSortColumn
      where TField5 : IBqlSortColumn
      where TField6 : IBqlSortColumn
      where TField7 : IBqlSortColumn
      where TField8 : IBqlSortColumn
      where TField9 : IBqlSortColumn
      where TField10 : IBqlSortColumn
      where TField11 : IBqlSortColumn
      where TField12 : IBqlSortColumn
      where TField13 : IBqlSortColumn
      where TField14 : IBqlSortColumn
      where TField15 : IBqlSortColumn
      where TField16 : IBqlSortColumn
      where TField17 : IBqlSortColumn
      where TField18 : IBqlSortColumn
      where TField19 : IBqlSortColumn
      where TField20 : IBqlSortColumn
      where TField21 : IBqlSortColumn
    {
    }

    /// <exclude />
    public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20, TField21, TField22> : 
      SelectFromBase<TTable, TJoins>.Where<TCondition>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20, TField21, TField22>>
      where TField1 : IBqlSortColumn
      where TField2 : IBqlSortColumn
      where TField3 : IBqlSortColumn
      where TField4 : IBqlSortColumn
      where TField5 : IBqlSortColumn
      where TField6 : IBqlSortColumn
      where TField7 : IBqlSortColumn
      where TField8 : IBqlSortColumn
      where TField9 : IBqlSortColumn
      where TField10 : IBqlSortColumn
      where TField11 : IBqlSortColumn
      where TField12 : IBqlSortColumn
      where TField13 : IBqlSortColumn
      where TField14 : IBqlSortColumn
      where TField15 : IBqlSortColumn
      where TField16 : IBqlSortColumn
      where TField17 : IBqlSortColumn
      where TField18 : IBqlSortColumn
      where TField19 : IBqlSortColumn
      where TField20 : IBqlSortColumn
      where TField21 : IBqlSortColumn
      where TField22 : IBqlSortColumn
    {
    }

    /// <exclude />
    public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20, TField21, TField22, TField23> : 
      SelectFromBase<TTable, TJoins>.Where<TCondition>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20, TField21, TField22, TField23>>
      where TField1 : IBqlSortColumn
      where TField2 : IBqlSortColumn
      where TField3 : IBqlSortColumn
      where TField4 : IBqlSortColumn
      where TField5 : IBqlSortColumn
      where TField6 : IBqlSortColumn
      where TField7 : IBqlSortColumn
      where TField8 : IBqlSortColumn
      where TField9 : IBqlSortColumn
      where TField10 : IBqlSortColumn
      where TField11 : IBqlSortColumn
      where TField12 : IBqlSortColumn
      where TField13 : IBqlSortColumn
      where TField14 : IBqlSortColumn
      where TField15 : IBqlSortColumn
      where TField16 : IBqlSortColumn
      where TField17 : IBqlSortColumn
      where TField18 : IBqlSortColumn
      where TField19 : IBqlSortColumn
      where TField20 : IBqlSortColumn
      where TField21 : IBqlSortColumn
      where TField22 : IBqlSortColumn
      where TField23 : IBqlSortColumn
    {
    }

    /// <exclude />
    public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20, TField21, TField22, TField23, TField24> : 
      SelectFromBase<TTable, TJoins>.Where<TCondition>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20, TField21, TField22, TField23, TField24>>
      where TField1 : IBqlSortColumn
      where TField2 : IBqlSortColumn
      where TField3 : IBqlSortColumn
      where TField4 : IBqlSortColumn
      where TField5 : IBqlSortColumn
      where TField6 : IBqlSortColumn
      where TField7 : IBqlSortColumn
      where TField8 : IBqlSortColumn
      where TField9 : IBqlSortColumn
      where TField10 : IBqlSortColumn
      where TField11 : IBqlSortColumn
      where TField12 : IBqlSortColumn
      where TField13 : IBqlSortColumn
      where TField14 : IBqlSortColumn
      where TField15 : IBqlSortColumn
      where TField16 : IBqlSortColumn
      where TField17 : IBqlSortColumn
      where TField18 : IBqlSortColumn
      where TField19 : IBqlSortColumn
      where TField20 : IBqlSortColumn
      where TField21 : IBqlSortColumn
      where TField22 : IBqlSortColumn
      where TField23 : IBqlSortColumn
      where TField24 : IBqlSortColumn
    {
    }

    /// <exclude />
    public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20, TField21, TField22, TField23, TField24, TField25> : 
      SelectFromBase<TTable, TJoins>.Where<TCondition>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20, TField21, TField22, TField23, TField24, TField25>>
      where TField1 : IBqlSortColumn
      where TField2 : IBqlSortColumn
      where TField3 : IBqlSortColumn
      where TField4 : IBqlSortColumn
      where TField5 : IBqlSortColumn
      where TField6 : IBqlSortColumn
      where TField7 : IBqlSortColumn
      where TField8 : IBqlSortColumn
      where TField9 : IBqlSortColumn
      where TField10 : IBqlSortColumn
      where TField11 : IBqlSortColumn
      where TField12 : IBqlSortColumn
      where TField13 : IBqlSortColumn
      where TField14 : IBqlSortColumn
      where TField15 : IBqlSortColumn
      where TField16 : IBqlSortColumn
      where TField17 : IBqlSortColumn
      where TField18 : IBqlSortColumn
      where TField19 : IBqlSortColumn
      where TField20 : IBqlSortColumn
      where TField21 : IBqlSortColumn
      where TField22 : IBqlSortColumn
      where TField23 : IBqlSortColumn
      where TField24 : IBqlSortColumn
      where TField25 : IBqlSortColumn
    {
    }

    /// <exclude />
    public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20, TField21, TField22, TField23, TField24, TField25, TField26> : 
      SelectFromBase<TTable, TJoins>.Where<TCondition>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20, TField21, TField22, TField23, TField24, TField25, TField26>>
      where TField1 : IBqlSortColumn
      where TField2 : IBqlSortColumn
      where TField3 : IBqlSortColumn
      where TField4 : IBqlSortColumn
      where TField5 : IBqlSortColumn
      where TField6 : IBqlSortColumn
      where TField7 : IBqlSortColumn
      where TField8 : IBqlSortColumn
      where TField9 : IBqlSortColumn
      where TField10 : IBqlSortColumn
      where TField11 : IBqlSortColumn
      where TField12 : IBqlSortColumn
      where TField13 : IBqlSortColumn
      where TField14 : IBqlSortColumn
      where TField15 : IBqlSortColumn
      where TField16 : IBqlSortColumn
      where TField17 : IBqlSortColumn
      where TField18 : IBqlSortColumn
      where TField19 : IBqlSortColumn
      where TField20 : IBqlSortColumn
      where TField21 : IBqlSortColumn
      where TField22 : IBqlSortColumn
      where TField23 : IBqlSortColumn
      where TField24 : IBqlSortColumn
      where TField25 : IBqlSortColumn
      where TField26 : IBqlSortColumn
    {
    }

    /// <exclude />
    public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20, TField21, TField22, TField23, TField24, TField25, TField26, TField27> : 
      SelectFromBase<TTable, TJoins>.Where<TCondition>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20, TField21, TField22, TField23, TField24, TField25, TField26, TField27>>
      where TField1 : IBqlSortColumn
      where TField2 : IBqlSortColumn
      where TField3 : IBqlSortColumn
      where TField4 : IBqlSortColumn
      where TField5 : IBqlSortColumn
      where TField6 : IBqlSortColumn
      where TField7 : IBqlSortColumn
      where TField8 : IBqlSortColumn
      where TField9 : IBqlSortColumn
      where TField10 : IBqlSortColumn
      where TField11 : IBqlSortColumn
      where TField12 : IBqlSortColumn
      where TField13 : IBqlSortColumn
      where TField14 : IBqlSortColumn
      where TField15 : IBqlSortColumn
      where TField16 : IBqlSortColumn
      where TField17 : IBqlSortColumn
      where TField18 : IBqlSortColumn
      where TField19 : IBqlSortColumn
      where TField20 : IBqlSortColumn
      where TField21 : IBqlSortColumn
      where TField22 : IBqlSortColumn
      where TField23 : IBqlSortColumn
      where TField24 : IBqlSortColumn
      where TField25 : IBqlSortColumn
      where TField26 : IBqlSortColumn
      where TField27 : IBqlSortColumn
    {
    }

    /// <exclude />
    public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20, TField21, TField22, TField23, TField24, TField25, TField26, TField27, TField28> : 
      SelectFromBase<TTable, TJoins>.Where<TCondition>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20, TField21, TField22, TField23, TField24, TField25, TField26, TField27, TField28>>
      where TField1 : IBqlSortColumn
      where TField2 : IBqlSortColumn
      where TField3 : IBqlSortColumn
      where TField4 : IBqlSortColumn
      where TField5 : IBqlSortColumn
      where TField6 : IBqlSortColumn
      where TField7 : IBqlSortColumn
      where TField8 : IBqlSortColumn
      where TField9 : IBqlSortColumn
      where TField10 : IBqlSortColumn
      where TField11 : IBqlSortColumn
      where TField12 : IBqlSortColumn
      where TField13 : IBqlSortColumn
      where TField14 : IBqlSortColumn
      where TField15 : IBqlSortColumn
      where TField16 : IBqlSortColumn
      where TField17 : IBqlSortColumn
      where TField18 : IBqlSortColumn
      where TField19 : IBqlSortColumn
      where TField20 : IBqlSortColumn
      where TField21 : IBqlSortColumn
      where TField22 : IBqlSortColumn
      where TField23 : IBqlSortColumn
      where TField24 : IBqlSortColumn
      where TField25 : IBqlSortColumn
      where TField26 : IBqlSortColumn
      where TField27 : IBqlSortColumn
      where TField28 : IBqlSortColumn
    {
    }

    /// <exclude />
    public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20, TField21, TField22, TField23, TField24, TField25, TField26, TField27, TField28, TField29> : 
      SelectFromBase<TTable, TJoins>.Where<TCondition>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20, TField21, TField22, TField23, TField24, TField25, TField26, TField27, TField28, TField29>>
      where TField1 : IBqlSortColumn
      where TField2 : IBqlSortColumn
      where TField3 : IBqlSortColumn
      where TField4 : IBqlSortColumn
      where TField5 : IBqlSortColumn
      where TField6 : IBqlSortColumn
      where TField7 : IBqlSortColumn
      where TField8 : IBqlSortColumn
      where TField9 : IBqlSortColumn
      where TField10 : IBqlSortColumn
      where TField11 : IBqlSortColumn
      where TField12 : IBqlSortColumn
      where TField13 : IBqlSortColumn
      where TField14 : IBqlSortColumn
      where TField15 : IBqlSortColumn
      where TField16 : IBqlSortColumn
      where TField17 : IBqlSortColumn
      where TField18 : IBqlSortColumn
      where TField19 : IBqlSortColumn
      where TField20 : IBqlSortColumn
      where TField21 : IBqlSortColumn
      where TField22 : IBqlSortColumn
      where TField23 : IBqlSortColumn
      where TField24 : IBqlSortColumn
      where TField25 : IBqlSortColumn
      where TField26 : IBqlSortColumn
      where TField27 : IBqlSortColumn
      where TField28 : IBqlSortColumn
      where TField29 : IBqlSortColumn
    {
    }

    /// <exclude />
    public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20, TField21, TField22, TField23, TField24, TField25, TField26, TField27, TField28, TField29, TField30> : 
      SelectFromBase<TTable, TJoins>.Where<TCondition>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20, TField21, TField22, TField23, TField24, TField25, TField26, TField27, TField28, TField29, TField30>>
      where TField1 : IBqlSortColumn
      where TField2 : IBqlSortColumn
      where TField3 : IBqlSortColumn
      where TField4 : IBqlSortColumn
      where TField5 : IBqlSortColumn
      where TField6 : IBqlSortColumn
      where TField7 : IBqlSortColumn
      where TField8 : IBqlSortColumn
      where TField9 : IBqlSortColumn
      where TField10 : IBqlSortColumn
      where TField11 : IBqlSortColumn
      where TField12 : IBqlSortColumn
      where TField13 : IBqlSortColumn
      where TField14 : IBqlSortColumn
      where TField15 : IBqlSortColumn
      where TField16 : IBqlSortColumn
      where TField17 : IBqlSortColumn
      where TField18 : IBqlSortColumn
      where TField19 : IBqlSortColumn
      where TField20 : IBqlSortColumn
      where TField21 : IBqlSortColumn
      where TField22 : IBqlSortColumn
      where TField23 : IBqlSortColumn
      where TField24 : IBqlSortColumn
      where TField25 : IBqlSortColumn
      where TField26 : IBqlSortColumn
      where TField27 : IBqlSortColumn
      where TField28 : IBqlSortColumn
      where TField29 : IBqlSortColumn
      where TField30 : IBqlSortColumn
    {
    }

    /// <exclude />
    public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20, TField21, TField22, TField23, TField24, TField25, TField26, TField27, TField28, TField29, TField30, TField31> : 
      SelectFromBase<TTable, TJoins>.Where<TCondition>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20, TField21, TField22, TField23, TField24, TField25, TField26, TField27, TField28, TField29, TField30, TField31>>
      where TField1 : IBqlSortColumn
      where TField2 : IBqlSortColumn
      where TField3 : IBqlSortColumn
      where TField4 : IBqlSortColumn
      where TField5 : IBqlSortColumn
      where TField6 : IBqlSortColumn
      where TField7 : IBqlSortColumn
      where TField8 : IBqlSortColumn
      where TField9 : IBqlSortColumn
      where TField10 : IBqlSortColumn
      where TField11 : IBqlSortColumn
      where TField12 : IBqlSortColumn
      where TField13 : IBqlSortColumn
      where TField14 : IBqlSortColumn
      where TField15 : IBqlSortColumn
      where TField16 : IBqlSortColumn
      where TField17 : IBqlSortColumn
      where TField18 : IBqlSortColumn
      where TField19 : IBqlSortColumn
      where TField20 : IBqlSortColumn
      where TField21 : IBqlSortColumn
      where TField22 : IBqlSortColumn
      where TField23 : IBqlSortColumn
      where TField24 : IBqlSortColumn
      where TField25 : IBqlSortColumn
      where TField26 : IBqlSortColumn
      where TField27 : IBqlSortColumn
      where TField28 : IBqlSortColumn
      where TField29 : IBqlSortColumn
      where TField30 : IBqlSortColumn
      where TField31 : IBqlSortColumn
    {
    }

    /// <exclude />
    public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20, TField21, TField22, TField23, TField24, TField25, TField26, TField27, TField28, TField29, TField30, TField31, TField32> : 
      SelectFromBase<TTable, TJoins>.Where<TCondition>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20, TField21, TField22, TField23, TField24, TField25, TField26, TField27, TField28, TField29, TField30, TField31, TField32>>
      where TField1 : IBqlSortColumn
      where TField2 : IBqlSortColumn
      where TField3 : IBqlSortColumn
      where TField4 : IBqlSortColumn
      where TField5 : IBqlSortColumn
      where TField6 : IBqlSortColumn
      where TField7 : IBqlSortColumn
      where TField8 : IBqlSortColumn
      where TField9 : IBqlSortColumn
      where TField10 : IBqlSortColumn
      where TField11 : IBqlSortColumn
      where TField12 : IBqlSortColumn
      where TField13 : IBqlSortColumn
      where TField14 : IBqlSortColumn
      where TField15 : IBqlSortColumn
      where TField16 : IBqlSortColumn
      where TField17 : IBqlSortColumn
      where TField18 : IBqlSortColumn
      where TField19 : IBqlSortColumn
      where TField20 : IBqlSortColumn
      where TField21 : IBqlSortColumn
      where TField22 : IBqlSortColumn
      where TField23 : IBqlSortColumn
      where TField24 : IBqlSortColumn
      where TField25 : IBqlSortColumn
      where TField26 : IBqlSortColumn
      where TField27 : IBqlSortColumn
      where TField28 : IBqlSortColumn
      where TField29 : IBqlSortColumn
      where TField30 : IBqlSortColumn
      where TField31 : IBqlSortColumn
      where TField32 : IBqlSortColumn
    {
    }

    public class Order<TSortColumnArray> : 
      FbqlSelect<SelectFromBase<TTable, TJoins>.Where<TCondition>.Order<TSortColumnArray>, TTable, TJoins, TCondition, TypeArrayOf<IBqlFunction>.Empty, BqlNone, TSortColumnArray>
      where TSortColumnArray : ITypeArrayOf<IBqlSortColumn>, TypeArray.IsNotEmpty
    {
    }
  }

  public class Aggregate<TFunctionArray> : 
    FbqlSelect<SelectFromBase<TTable, TJoins>.Aggregate<TFunctionArray>, TTable, TJoins, BqlNone, TFunctionArray, BqlNone, TypeArrayOf<IBqlSortColumn>.Empty>
    where TFunctionArray : ITypeArrayOf<IBqlFunction>, TypeArray.IsNotEmpty
  {
    /// <exclude />
    public class OrderBy<TField> : 
      SelectFromBase<TTable, TJoins>.Aggregate<TFunctionArray>.Order<By<TField>>
      where TField : IBqlSortColumn
    {
    }

    /// <exclude />
    public class OrderBy<TField1, TField2> : 
      SelectFromBase<TTable, TJoins>.Aggregate<TFunctionArray>.Order<By<TField1, TField2>>
      where TField1 : IBqlSortColumn
      where TField2 : IBqlSortColumn
    {
    }

    /// <exclude />
    public class OrderBy<TField1, TField2, TField3> : 
      SelectFromBase<TTable, TJoins>.Aggregate<TFunctionArray>.Order<By<TField1, TField2, TField3>>
      where TField1 : IBqlSortColumn
      where TField2 : IBqlSortColumn
      where TField3 : IBqlSortColumn
    {
    }

    /// <exclude />
    public class OrderBy<TField1, TField2, TField3, TField4> : 
      SelectFromBase<TTable, TJoins>.Aggregate<TFunctionArray>.Order<By<TField1, TField2, TField3, TField4>>
      where TField1 : IBqlSortColumn
      where TField2 : IBqlSortColumn
      where TField3 : IBqlSortColumn
      where TField4 : IBqlSortColumn
    {
    }

    /// <exclude />
    public class OrderBy<TField1, TField2, TField3, TField4, TField5> : 
      SelectFromBase<TTable, TJoins>.Aggregate<TFunctionArray>.Order<By<TField1, TField2, TField3, TField4, TField5>>
      where TField1 : IBqlSortColumn
      where TField2 : IBqlSortColumn
      where TField3 : IBqlSortColumn
      where TField4 : IBqlSortColumn
      where TField5 : IBqlSortColumn
    {
    }

    /// <exclude />
    public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6> : 
      SelectFromBase<TTable, TJoins>.Aggregate<TFunctionArray>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6>>
      where TField1 : IBqlSortColumn
      where TField2 : IBqlSortColumn
      where TField3 : IBqlSortColumn
      where TField4 : IBqlSortColumn
      where TField5 : IBqlSortColumn
      where TField6 : IBqlSortColumn
    {
    }

    /// <exclude />
    public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7> : 
      SelectFromBase<TTable, TJoins>.Aggregate<TFunctionArray>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7>>
      where TField1 : IBqlSortColumn
      where TField2 : IBqlSortColumn
      where TField3 : IBqlSortColumn
      where TField4 : IBqlSortColumn
      where TField5 : IBqlSortColumn
      where TField6 : IBqlSortColumn
      where TField7 : IBqlSortColumn
    {
    }

    /// <exclude />
    public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8> : 
      SelectFromBase<TTable, TJoins>.Aggregate<TFunctionArray>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8>>
      where TField1 : IBqlSortColumn
      where TField2 : IBqlSortColumn
      where TField3 : IBqlSortColumn
      where TField4 : IBqlSortColumn
      where TField5 : IBqlSortColumn
      where TField6 : IBqlSortColumn
      where TField7 : IBqlSortColumn
      where TField8 : IBqlSortColumn
    {
    }

    /// <exclude />
    public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9> : 
      SelectFromBase<TTable, TJoins>.Aggregate<TFunctionArray>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9>>
      where TField1 : IBqlSortColumn
      where TField2 : IBqlSortColumn
      where TField3 : IBqlSortColumn
      where TField4 : IBqlSortColumn
      where TField5 : IBqlSortColumn
      where TField6 : IBqlSortColumn
      where TField7 : IBqlSortColumn
      where TField8 : IBqlSortColumn
      where TField9 : IBqlSortColumn
    {
    }

    /// <exclude />
    public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10> : 
      SelectFromBase<TTable, TJoins>.Aggregate<TFunctionArray>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10>>
      where TField1 : IBqlSortColumn
      where TField2 : IBqlSortColumn
      where TField3 : IBqlSortColumn
      where TField4 : IBqlSortColumn
      where TField5 : IBqlSortColumn
      where TField6 : IBqlSortColumn
      where TField7 : IBqlSortColumn
      where TField8 : IBqlSortColumn
      where TField9 : IBqlSortColumn
      where TField10 : IBqlSortColumn
    {
    }

    /// <exclude />
    public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11> : 
      SelectFromBase<TTable, TJoins>.Aggregate<TFunctionArray>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11>>
      where TField1 : IBqlSortColumn
      where TField2 : IBqlSortColumn
      where TField3 : IBqlSortColumn
      where TField4 : IBqlSortColumn
      where TField5 : IBqlSortColumn
      where TField6 : IBqlSortColumn
      where TField7 : IBqlSortColumn
      where TField8 : IBqlSortColumn
      where TField9 : IBqlSortColumn
      where TField10 : IBqlSortColumn
      where TField11 : IBqlSortColumn
    {
    }

    /// <exclude />
    public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12> : 
      SelectFromBase<TTable, TJoins>.Aggregate<TFunctionArray>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12>>
      where TField1 : IBqlSortColumn
      where TField2 : IBqlSortColumn
      where TField3 : IBqlSortColumn
      where TField4 : IBqlSortColumn
      where TField5 : IBqlSortColumn
      where TField6 : IBqlSortColumn
      where TField7 : IBqlSortColumn
      where TField8 : IBqlSortColumn
      where TField9 : IBqlSortColumn
      where TField10 : IBqlSortColumn
      where TField11 : IBqlSortColumn
      where TField12 : IBqlSortColumn
    {
    }

    /// <exclude />
    public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13> : 
      SelectFromBase<TTable, TJoins>.Aggregate<TFunctionArray>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13>>
      where TField1 : IBqlSortColumn
      where TField2 : IBqlSortColumn
      where TField3 : IBqlSortColumn
      where TField4 : IBqlSortColumn
      where TField5 : IBqlSortColumn
      where TField6 : IBqlSortColumn
      where TField7 : IBqlSortColumn
      where TField8 : IBqlSortColumn
      where TField9 : IBqlSortColumn
      where TField10 : IBqlSortColumn
      where TField11 : IBqlSortColumn
      where TField12 : IBqlSortColumn
      where TField13 : IBqlSortColumn
    {
    }

    /// <exclude />
    public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14> : 
      SelectFromBase<TTable, TJoins>.Aggregate<TFunctionArray>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14>>
      where TField1 : IBqlSortColumn
      where TField2 : IBqlSortColumn
      where TField3 : IBqlSortColumn
      where TField4 : IBqlSortColumn
      where TField5 : IBqlSortColumn
      where TField6 : IBqlSortColumn
      where TField7 : IBqlSortColumn
      where TField8 : IBqlSortColumn
      where TField9 : IBqlSortColumn
      where TField10 : IBqlSortColumn
      where TField11 : IBqlSortColumn
      where TField12 : IBqlSortColumn
      where TField13 : IBqlSortColumn
      where TField14 : IBqlSortColumn
    {
    }

    /// <exclude />
    public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15> : 
      SelectFromBase<TTable, TJoins>.Aggregate<TFunctionArray>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15>>
      where TField1 : IBqlSortColumn
      where TField2 : IBqlSortColumn
      where TField3 : IBqlSortColumn
      where TField4 : IBqlSortColumn
      where TField5 : IBqlSortColumn
      where TField6 : IBqlSortColumn
      where TField7 : IBqlSortColumn
      where TField8 : IBqlSortColumn
      where TField9 : IBqlSortColumn
      where TField10 : IBqlSortColumn
      where TField11 : IBqlSortColumn
      where TField12 : IBqlSortColumn
      where TField13 : IBqlSortColumn
      where TField14 : IBqlSortColumn
      where TField15 : IBqlSortColumn
    {
    }

    /// <exclude />
    public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16> : 
      SelectFromBase<TTable, TJoins>.Aggregate<TFunctionArray>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16>>
      where TField1 : IBqlSortColumn
      where TField2 : IBqlSortColumn
      where TField3 : IBqlSortColumn
      where TField4 : IBqlSortColumn
      where TField5 : IBqlSortColumn
      where TField6 : IBqlSortColumn
      where TField7 : IBqlSortColumn
      where TField8 : IBqlSortColumn
      where TField9 : IBqlSortColumn
      where TField10 : IBqlSortColumn
      where TField11 : IBqlSortColumn
      where TField12 : IBqlSortColumn
      where TField13 : IBqlSortColumn
      where TField14 : IBqlSortColumn
      where TField15 : IBqlSortColumn
      where TField16 : IBqlSortColumn
    {
    }

    /// <exclude />
    public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17> : 
      SelectFromBase<TTable, TJoins>.Aggregate<TFunctionArray>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17>>
      where TField1 : IBqlSortColumn
      where TField2 : IBqlSortColumn
      where TField3 : IBqlSortColumn
      where TField4 : IBqlSortColumn
      where TField5 : IBqlSortColumn
      where TField6 : IBqlSortColumn
      where TField7 : IBqlSortColumn
      where TField8 : IBqlSortColumn
      where TField9 : IBqlSortColumn
      where TField10 : IBqlSortColumn
      where TField11 : IBqlSortColumn
      where TField12 : IBqlSortColumn
      where TField13 : IBqlSortColumn
      where TField14 : IBqlSortColumn
      where TField15 : IBqlSortColumn
      where TField16 : IBqlSortColumn
      where TField17 : IBqlSortColumn
    {
    }

    /// <exclude />
    public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18> : 
      SelectFromBase<TTable, TJoins>.Aggregate<TFunctionArray>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18>>
      where TField1 : IBqlSortColumn
      where TField2 : IBqlSortColumn
      where TField3 : IBqlSortColumn
      where TField4 : IBqlSortColumn
      where TField5 : IBqlSortColumn
      where TField6 : IBqlSortColumn
      where TField7 : IBqlSortColumn
      where TField8 : IBqlSortColumn
      where TField9 : IBqlSortColumn
      where TField10 : IBqlSortColumn
      where TField11 : IBqlSortColumn
      where TField12 : IBqlSortColumn
      where TField13 : IBqlSortColumn
      where TField14 : IBqlSortColumn
      where TField15 : IBqlSortColumn
      where TField16 : IBqlSortColumn
      where TField17 : IBqlSortColumn
      where TField18 : IBqlSortColumn
    {
    }

    /// <exclude />
    public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19> : 
      SelectFromBase<TTable, TJoins>.Aggregate<TFunctionArray>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19>>
      where TField1 : IBqlSortColumn
      where TField2 : IBqlSortColumn
      where TField3 : IBqlSortColumn
      where TField4 : IBqlSortColumn
      where TField5 : IBqlSortColumn
      where TField6 : IBqlSortColumn
      where TField7 : IBqlSortColumn
      where TField8 : IBqlSortColumn
      where TField9 : IBqlSortColumn
      where TField10 : IBqlSortColumn
      where TField11 : IBqlSortColumn
      where TField12 : IBqlSortColumn
      where TField13 : IBqlSortColumn
      where TField14 : IBqlSortColumn
      where TField15 : IBqlSortColumn
      where TField16 : IBqlSortColumn
      where TField17 : IBqlSortColumn
      where TField18 : IBqlSortColumn
      where TField19 : IBqlSortColumn
    {
    }

    /// <exclude />
    public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20> : 
      SelectFromBase<TTable, TJoins>.Aggregate<TFunctionArray>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20>>
      where TField1 : IBqlSortColumn
      where TField2 : IBqlSortColumn
      where TField3 : IBqlSortColumn
      where TField4 : IBqlSortColumn
      where TField5 : IBqlSortColumn
      where TField6 : IBqlSortColumn
      where TField7 : IBqlSortColumn
      where TField8 : IBqlSortColumn
      where TField9 : IBqlSortColumn
      where TField10 : IBqlSortColumn
      where TField11 : IBqlSortColumn
      where TField12 : IBqlSortColumn
      where TField13 : IBqlSortColumn
      where TField14 : IBqlSortColumn
      where TField15 : IBqlSortColumn
      where TField16 : IBqlSortColumn
      where TField17 : IBqlSortColumn
      where TField18 : IBqlSortColumn
      where TField19 : IBqlSortColumn
      where TField20 : IBqlSortColumn
    {
    }

    /// <exclude />
    public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20, TField21> : 
      SelectFromBase<TTable, TJoins>.Aggregate<TFunctionArray>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20, TField21>>
      where TField1 : IBqlSortColumn
      where TField2 : IBqlSortColumn
      where TField3 : IBqlSortColumn
      where TField4 : IBqlSortColumn
      where TField5 : IBqlSortColumn
      where TField6 : IBqlSortColumn
      where TField7 : IBqlSortColumn
      where TField8 : IBqlSortColumn
      where TField9 : IBqlSortColumn
      where TField10 : IBqlSortColumn
      where TField11 : IBqlSortColumn
      where TField12 : IBqlSortColumn
      where TField13 : IBqlSortColumn
      where TField14 : IBqlSortColumn
      where TField15 : IBqlSortColumn
      where TField16 : IBqlSortColumn
      where TField17 : IBqlSortColumn
      where TField18 : IBqlSortColumn
      where TField19 : IBqlSortColumn
      where TField20 : IBqlSortColumn
      where TField21 : IBqlSortColumn
    {
    }

    /// <exclude />
    public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20, TField21, TField22> : 
      SelectFromBase<TTable, TJoins>.Aggregate<TFunctionArray>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20, TField21, TField22>>
      where TField1 : IBqlSortColumn
      where TField2 : IBqlSortColumn
      where TField3 : IBqlSortColumn
      where TField4 : IBqlSortColumn
      where TField5 : IBqlSortColumn
      where TField6 : IBqlSortColumn
      where TField7 : IBqlSortColumn
      where TField8 : IBqlSortColumn
      where TField9 : IBqlSortColumn
      where TField10 : IBqlSortColumn
      where TField11 : IBqlSortColumn
      where TField12 : IBqlSortColumn
      where TField13 : IBqlSortColumn
      where TField14 : IBqlSortColumn
      where TField15 : IBqlSortColumn
      where TField16 : IBqlSortColumn
      where TField17 : IBqlSortColumn
      where TField18 : IBqlSortColumn
      where TField19 : IBqlSortColumn
      where TField20 : IBqlSortColumn
      where TField21 : IBqlSortColumn
      where TField22 : IBqlSortColumn
    {
    }

    /// <exclude />
    public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20, TField21, TField22, TField23> : 
      SelectFromBase<TTable, TJoins>.Aggregate<TFunctionArray>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20, TField21, TField22, TField23>>
      where TField1 : IBqlSortColumn
      where TField2 : IBqlSortColumn
      where TField3 : IBqlSortColumn
      where TField4 : IBqlSortColumn
      where TField5 : IBqlSortColumn
      where TField6 : IBqlSortColumn
      where TField7 : IBqlSortColumn
      where TField8 : IBqlSortColumn
      where TField9 : IBqlSortColumn
      where TField10 : IBqlSortColumn
      where TField11 : IBqlSortColumn
      where TField12 : IBqlSortColumn
      where TField13 : IBqlSortColumn
      where TField14 : IBqlSortColumn
      where TField15 : IBqlSortColumn
      where TField16 : IBqlSortColumn
      where TField17 : IBqlSortColumn
      where TField18 : IBqlSortColumn
      where TField19 : IBqlSortColumn
      where TField20 : IBqlSortColumn
      where TField21 : IBqlSortColumn
      where TField22 : IBqlSortColumn
      where TField23 : IBqlSortColumn
    {
    }

    /// <exclude />
    public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20, TField21, TField22, TField23, TField24> : 
      SelectFromBase<TTable, TJoins>.Aggregate<TFunctionArray>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20, TField21, TField22, TField23, TField24>>
      where TField1 : IBqlSortColumn
      where TField2 : IBqlSortColumn
      where TField3 : IBqlSortColumn
      where TField4 : IBqlSortColumn
      where TField5 : IBqlSortColumn
      where TField6 : IBqlSortColumn
      where TField7 : IBqlSortColumn
      where TField8 : IBqlSortColumn
      where TField9 : IBqlSortColumn
      where TField10 : IBqlSortColumn
      where TField11 : IBqlSortColumn
      where TField12 : IBqlSortColumn
      where TField13 : IBqlSortColumn
      where TField14 : IBqlSortColumn
      where TField15 : IBqlSortColumn
      where TField16 : IBqlSortColumn
      where TField17 : IBqlSortColumn
      where TField18 : IBqlSortColumn
      where TField19 : IBqlSortColumn
      where TField20 : IBqlSortColumn
      where TField21 : IBqlSortColumn
      where TField22 : IBqlSortColumn
      where TField23 : IBqlSortColumn
      where TField24 : IBqlSortColumn
    {
    }

    /// <exclude />
    public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20, TField21, TField22, TField23, TField24, TField25> : 
      SelectFromBase<TTable, TJoins>.Aggregate<TFunctionArray>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20, TField21, TField22, TField23, TField24, TField25>>
      where TField1 : IBqlSortColumn
      where TField2 : IBqlSortColumn
      where TField3 : IBqlSortColumn
      where TField4 : IBqlSortColumn
      where TField5 : IBqlSortColumn
      where TField6 : IBqlSortColumn
      where TField7 : IBqlSortColumn
      where TField8 : IBqlSortColumn
      where TField9 : IBqlSortColumn
      where TField10 : IBqlSortColumn
      where TField11 : IBqlSortColumn
      where TField12 : IBqlSortColumn
      where TField13 : IBqlSortColumn
      where TField14 : IBqlSortColumn
      where TField15 : IBqlSortColumn
      where TField16 : IBqlSortColumn
      where TField17 : IBqlSortColumn
      where TField18 : IBqlSortColumn
      where TField19 : IBqlSortColumn
      where TField20 : IBqlSortColumn
      where TField21 : IBqlSortColumn
      where TField22 : IBqlSortColumn
      where TField23 : IBqlSortColumn
      where TField24 : IBqlSortColumn
      where TField25 : IBqlSortColumn
    {
    }

    /// <exclude />
    public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20, TField21, TField22, TField23, TField24, TField25, TField26> : 
      SelectFromBase<TTable, TJoins>.Aggregate<TFunctionArray>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20, TField21, TField22, TField23, TField24, TField25, TField26>>
      where TField1 : IBqlSortColumn
      where TField2 : IBqlSortColumn
      where TField3 : IBqlSortColumn
      where TField4 : IBqlSortColumn
      where TField5 : IBqlSortColumn
      where TField6 : IBqlSortColumn
      where TField7 : IBqlSortColumn
      where TField8 : IBqlSortColumn
      where TField9 : IBqlSortColumn
      where TField10 : IBqlSortColumn
      where TField11 : IBqlSortColumn
      where TField12 : IBqlSortColumn
      where TField13 : IBqlSortColumn
      where TField14 : IBqlSortColumn
      where TField15 : IBqlSortColumn
      where TField16 : IBqlSortColumn
      where TField17 : IBqlSortColumn
      where TField18 : IBqlSortColumn
      where TField19 : IBqlSortColumn
      where TField20 : IBqlSortColumn
      where TField21 : IBqlSortColumn
      where TField22 : IBqlSortColumn
      where TField23 : IBqlSortColumn
      where TField24 : IBqlSortColumn
      where TField25 : IBqlSortColumn
      where TField26 : IBqlSortColumn
    {
    }

    /// <exclude />
    public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20, TField21, TField22, TField23, TField24, TField25, TField26, TField27> : 
      SelectFromBase<TTable, TJoins>.Aggregate<TFunctionArray>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20, TField21, TField22, TField23, TField24, TField25, TField26, TField27>>
      where TField1 : IBqlSortColumn
      where TField2 : IBqlSortColumn
      where TField3 : IBqlSortColumn
      where TField4 : IBqlSortColumn
      where TField5 : IBqlSortColumn
      where TField6 : IBqlSortColumn
      where TField7 : IBqlSortColumn
      where TField8 : IBqlSortColumn
      where TField9 : IBqlSortColumn
      where TField10 : IBqlSortColumn
      where TField11 : IBqlSortColumn
      where TField12 : IBqlSortColumn
      where TField13 : IBqlSortColumn
      where TField14 : IBqlSortColumn
      where TField15 : IBqlSortColumn
      where TField16 : IBqlSortColumn
      where TField17 : IBqlSortColumn
      where TField18 : IBqlSortColumn
      where TField19 : IBqlSortColumn
      where TField20 : IBqlSortColumn
      where TField21 : IBqlSortColumn
      where TField22 : IBqlSortColumn
      where TField23 : IBqlSortColumn
      where TField24 : IBqlSortColumn
      where TField25 : IBqlSortColumn
      where TField26 : IBqlSortColumn
      where TField27 : IBqlSortColumn
    {
    }

    /// <exclude />
    public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20, TField21, TField22, TField23, TField24, TField25, TField26, TField27, TField28> : 
      SelectFromBase<TTable, TJoins>.Aggregate<TFunctionArray>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20, TField21, TField22, TField23, TField24, TField25, TField26, TField27, TField28>>
      where TField1 : IBqlSortColumn
      where TField2 : IBqlSortColumn
      where TField3 : IBqlSortColumn
      where TField4 : IBqlSortColumn
      where TField5 : IBqlSortColumn
      where TField6 : IBqlSortColumn
      where TField7 : IBqlSortColumn
      where TField8 : IBqlSortColumn
      where TField9 : IBqlSortColumn
      where TField10 : IBqlSortColumn
      where TField11 : IBqlSortColumn
      where TField12 : IBqlSortColumn
      where TField13 : IBqlSortColumn
      where TField14 : IBqlSortColumn
      where TField15 : IBqlSortColumn
      where TField16 : IBqlSortColumn
      where TField17 : IBqlSortColumn
      where TField18 : IBqlSortColumn
      where TField19 : IBqlSortColumn
      where TField20 : IBqlSortColumn
      where TField21 : IBqlSortColumn
      where TField22 : IBqlSortColumn
      where TField23 : IBqlSortColumn
      where TField24 : IBqlSortColumn
      where TField25 : IBqlSortColumn
      where TField26 : IBqlSortColumn
      where TField27 : IBqlSortColumn
      where TField28 : IBqlSortColumn
    {
    }

    /// <exclude />
    public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20, TField21, TField22, TField23, TField24, TField25, TField26, TField27, TField28, TField29> : 
      SelectFromBase<TTable, TJoins>.Aggregate<TFunctionArray>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20, TField21, TField22, TField23, TField24, TField25, TField26, TField27, TField28, TField29>>
      where TField1 : IBqlSortColumn
      where TField2 : IBqlSortColumn
      where TField3 : IBqlSortColumn
      where TField4 : IBqlSortColumn
      where TField5 : IBqlSortColumn
      where TField6 : IBqlSortColumn
      where TField7 : IBqlSortColumn
      where TField8 : IBqlSortColumn
      where TField9 : IBqlSortColumn
      where TField10 : IBqlSortColumn
      where TField11 : IBqlSortColumn
      where TField12 : IBqlSortColumn
      where TField13 : IBqlSortColumn
      where TField14 : IBqlSortColumn
      where TField15 : IBqlSortColumn
      where TField16 : IBqlSortColumn
      where TField17 : IBqlSortColumn
      where TField18 : IBqlSortColumn
      where TField19 : IBqlSortColumn
      where TField20 : IBqlSortColumn
      where TField21 : IBqlSortColumn
      where TField22 : IBqlSortColumn
      where TField23 : IBqlSortColumn
      where TField24 : IBqlSortColumn
      where TField25 : IBqlSortColumn
      where TField26 : IBqlSortColumn
      where TField27 : IBqlSortColumn
      where TField28 : IBqlSortColumn
      where TField29 : IBqlSortColumn
    {
    }

    /// <exclude />
    public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20, TField21, TField22, TField23, TField24, TField25, TField26, TField27, TField28, TField29, TField30> : 
      SelectFromBase<TTable, TJoins>.Aggregate<TFunctionArray>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20, TField21, TField22, TField23, TField24, TField25, TField26, TField27, TField28, TField29, TField30>>
      where TField1 : IBqlSortColumn
      where TField2 : IBqlSortColumn
      where TField3 : IBqlSortColumn
      where TField4 : IBqlSortColumn
      where TField5 : IBqlSortColumn
      where TField6 : IBqlSortColumn
      where TField7 : IBqlSortColumn
      where TField8 : IBqlSortColumn
      where TField9 : IBqlSortColumn
      where TField10 : IBqlSortColumn
      where TField11 : IBqlSortColumn
      where TField12 : IBqlSortColumn
      where TField13 : IBqlSortColumn
      where TField14 : IBqlSortColumn
      where TField15 : IBqlSortColumn
      where TField16 : IBqlSortColumn
      where TField17 : IBqlSortColumn
      where TField18 : IBqlSortColumn
      where TField19 : IBqlSortColumn
      where TField20 : IBqlSortColumn
      where TField21 : IBqlSortColumn
      where TField22 : IBqlSortColumn
      where TField23 : IBqlSortColumn
      where TField24 : IBqlSortColumn
      where TField25 : IBqlSortColumn
      where TField26 : IBqlSortColumn
      where TField27 : IBqlSortColumn
      where TField28 : IBqlSortColumn
      where TField29 : IBqlSortColumn
      where TField30 : IBqlSortColumn
    {
    }

    /// <exclude />
    public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20, TField21, TField22, TField23, TField24, TField25, TField26, TField27, TField28, TField29, TField30, TField31> : 
      SelectFromBase<TTable, TJoins>.Aggregate<TFunctionArray>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20, TField21, TField22, TField23, TField24, TField25, TField26, TField27, TField28, TField29, TField30, TField31>>
      where TField1 : IBqlSortColumn
      where TField2 : IBqlSortColumn
      where TField3 : IBqlSortColumn
      where TField4 : IBqlSortColumn
      where TField5 : IBqlSortColumn
      where TField6 : IBqlSortColumn
      where TField7 : IBqlSortColumn
      where TField8 : IBqlSortColumn
      where TField9 : IBqlSortColumn
      where TField10 : IBqlSortColumn
      where TField11 : IBqlSortColumn
      where TField12 : IBqlSortColumn
      where TField13 : IBqlSortColumn
      where TField14 : IBqlSortColumn
      where TField15 : IBqlSortColumn
      where TField16 : IBqlSortColumn
      where TField17 : IBqlSortColumn
      where TField18 : IBqlSortColumn
      where TField19 : IBqlSortColumn
      where TField20 : IBqlSortColumn
      where TField21 : IBqlSortColumn
      where TField22 : IBqlSortColumn
      where TField23 : IBqlSortColumn
      where TField24 : IBqlSortColumn
      where TField25 : IBqlSortColumn
      where TField26 : IBqlSortColumn
      where TField27 : IBqlSortColumn
      where TField28 : IBqlSortColumn
      where TField29 : IBqlSortColumn
      where TField30 : IBqlSortColumn
      where TField31 : IBqlSortColumn
    {
    }

    /// <exclude />
    public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20, TField21, TField22, TField23, TField24, TField25, TField26, TField27, TField28, TField29, TField30, TField31, TField32> : 
      SelectFromBase<TTable, TJoins>.Aggregate<TFunctionArray>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20, TField21, TField22, TField23, TField24, TField25, TField26, TField27, TField28, TField29, TField30, TField31, TField32>>
      where TField1 : IBqlSortColumn
      where TField2 : IBqlSortColumn
      where TField3 : IBqlSortColumn
      where TField4 : IBqlSortColumn
      where TField5 : IBqlSortColumn
      where TField6 : IBqlSortColumn
      where TField7 : IBqlSortColumn
      where TField8 : IBqlSortColumn
      where TField9 : IBqlSortColumn
      where TField10 : IBqlSortColumn
      where TField11 : IBqlSortColumn
      where TField12 : IBqlSortColumn
      where TField13 : IBqlSortColumn
      where TField14 : IBqlSortColumn
      where TField15 : IBqlSortColumn
      where TField16 : IBqlSortColumn
      where TField17 : IBqlSortColumn
      where TField18 : IBqlSortColumn
      where TField19 : IBqlSortColumn
      where TField20 : IBqlSortColumn
      where TField21 : IBqlSortColumn
      where TField22 : IBqlSortColumn
      where TField23 : IBqlSortColumn
      where TField24 : IBqlSortColumn
      where TField25 : IBqlSortColumn
      where TField26 : IBqlSortColumn
      where TField27 : IBqlSortColumn
      where TField28 : IBqlSortColumn
      where TField29 : IBqlSortColumn
      where TField30 : IBqlSortColumn
      where TField31 : IBqlSortColumn
      where TField32 : IBqlSortColumn
    {
    }

    public class Having<TGroupCondition> : 
      FbqlSelect<SelectFromBase<TTable, TJoins>.Aggregate<TFunctionArray>.Having<TGroupCondition>, TTable, TJoins, BqlNone, TFunctionArray, Having<TGroupCondition>, TypeArrayOf<IBqlSortColumn>.Empty>
      where TGroupCondition : IBqlHavingCondition, new()
    {
      /// <exclude />
      public class OrderBy<TField> : 
        SelectFromBase<TTable, TJoins>.Aggregate<TFunctionArray>.Having<TGroupCondition>.Order<By<TField>>
        where TField : IBqlSortColumn
      {
      }

      /// <exclude />
      public class OrderBy<TField1, TField2> : 
        SelectFromBase<TTable, TJoins>.Aggregate<TFunctionArray>.Having<TGroupCondition>.Order<By<TField1, TField2>>
        where TField1 : IBqlSortColumn
        where TField2 : IBqlSortColumn
      {
      }

      /// <exclude />
      public class OrderBy<TField1, TField2, TField3> : 
        SelectFromBase<TTable, TJoins>.Aggregate<TFunctionArray>.Having<TGroupCondition>.Order<By<TField1, TField2, TField3>>
        where TField1 : IBqlSortColumn
        where TField2 : IBqlSortColumn
        where TField3 : IBqlSortColumn
      {
      }

      /// <exclude />
      public class OrderBy<TField1, TField2, TField3, TField4> : 
        SelectFromBase<TTable, TJoins>.Aggregate<TFunctionArray>.Having<TGroupCondition>.Order<By<TField1, TField2, TField3, TField4>>
        where TField1 : IBqlSortColumn
        where TField2 : IBqlSortColumn
        where TField3 : IBqlSortColumn
        where TField4 : IBqlSortColumn
      {
      }

      /// <exclude />
      public class OrderBy<TField1, TField2, TField3, TField4, TField5> : 
        SelectFromBase<TTable, TJoins>.Aggregate<TFunctionArray>.Having<TGroupCondition>.Order<By<TField1, TField2, TField3, TField4, TField5>>
        where TField1 : IBqlSortColumn
        where TField2 : IBqlSortColumn
        where TField3 : IBqlSortColumn
        where TField4 : IBqlSortColumn
        where TField5 : IBqlSortColumn
      {
      }

      /// <exclude />
      public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6> : 
        SelectFromBase<TTable, TJoins>.Aggregate<TFunctionArray>.Having<TGroupCondition>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6>>
        where TField1 : IBqlSortColumn
        where TField2 : IBqlSortColumn
        where TField3 : IBqlSortColumn
        where TField4 : IBqlSortColumn
        where TField5 : IBqlSortColumn
        where TField6 : IBqlSortColumn
      {
      }

      /// <exclude />
      public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7> : 
        SelectFromBase<TTable, TJoins>.Aggregate<TFunctionArray>.Having<TGroupCondition>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7>>
        where TField1 : IBqlSortColumn
        where TField2 : IBqlSortColumn
        where TField3 : IBqlSortColumn
        where TField4 : IBqlSortColumn
        where TField5 : IBqlSortColumn
        where TField6 : IBqlSortColumn
        where TField7 : IBqlSortColumn
      {
      }

      /// <exclude />
      public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8> : 
        SelectFromBase<TTable, TJoins>.Aggregate<TFunctionArray>.Having<TGroupCondition>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8>>
        where TField1 : IBqlSortColumn
        where TField2 : IBqlSortColumn
        where TField3 : IBqlSortColumn
        where TField4 : IBqlSortColumn
        where TField5 : IBqlSortColumn
        where TField6 : IBqlSortColumn
        where TField7 : IBqlSortColumn
        where TField8 : IBqlSortColumn
      {
      }

      /// <exclude />
      public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9> : 
        SelectFromBase<TTable, TJoins>.Aggregate<TFunctionArray>.Having<TGroupCondition>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9>>
        where TField1 : IBqlSortColumn
        where TField2 : IBqlSortColumn
        where TField3 : IBqlSortColumn
        where TField4 : IBqlSortColumn
        where TField5 : IBqlSortColumn
        where TField6 : IBqlSortColumn
        where TField7 : IBqlSortColumn
        where TField8 : IBqlSortColumn
        where TField9 : IBqlSortColumn
      {
      }

      /// <exclude />
      public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10> : 
        SelectFromBase<TTable, TJoins>.Aggregate<TFunctionArray>.Having<TGroupCondition>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10>>
        where TField1 : IBqlSortColumn
        where TField2 : IBqlSortColumn
        where TField3 : IBqlSortColumn
        where TField4 : IBqlSortColumn
        where TField5 : IBqlSortColumn
        where TField6 : IBqlSortColumn
        where TField7 : IBqlSortColumn
        where TField8 : IBqlSortColumn
        where TField9 : IBqlSortColumn
        where TField10 : IBqlSortColumn
      {
      }

      /// <exclude />
      public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11> : 
        SelectFromBase<TTable, TJoins>.Aggregate<TFunctionArray>.Having<TGroupCondition>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11>>
        where TField1 : IBqlSortColumn
        where TField2 : IBqlSortColumn
        where TField3 : IBqlSortColumn
        where TField4 : IBqlSortColumn
        where TField5 : IBqlSortColumn
        where TField6 : IBqlSortColumn
        where TField7 : IBqlSortColumn
        where TField8 : IBqlSortColumn
        where TField9 : IBqlSortColumn
        where TField10 : IBqlSortColumn
        where TField11 : IBqlSortColumn
      {
      }

      /// <exclude />
      public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12> : 
        SelectFromBase<TTable, TJoins>.Aggregate<TFunctionArray>.Having<TGroupCondition>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12>>
        where TField1 : IBqlSortColumn
        where TField2 : IBqlSortColumn
        where TField3 : IBqlSortColumn
        where TField4 : IBqlSortColumn
        where TField5 : IBqlSortColumn
        where TField6 : IBqlSortColumn
        where TField7 : IBqlSortColumn
        where TField8 : IBqlSortColumn
        where TField9 : IBqlSortColumn
        where TField10 : IBqlSortColumn
        where TField11 : IBqlSortColumn
        where TField12 : IBqlSortColumn
      {
      }

      /// <exclude />
      public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13> : 
        SelectFromBase<TTable, TJoins>.Aggregate<TFunctionArray>.Having<TGroupCondition>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13>>
        where TField1 : IBqlSortColumn
        where TField2 : IBqlSortColumn
        where TField3 : IBqlSortColumn
        where TField4 : IBqlSortColumn
        where TField5 : IBqlSortColumn
        where TField6 : IBqlSortColumn
        where TField7 : IBqlSortColumn
        where TField8 : IBqlSortColumn
        where TField9 : IBqlSortColumn
        where TField10 : IBqlSortColumn
        where TField11 : IBqlSortColumn
        where TField12 : IBqlSortColumn
        where TField13 : IBqlSortColumn
      {
      }

      /// <exclude />
      public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14> : 
        SelectFromBase<TTable, TJoins>.Aggregate<TFunctionArray>.Having<TGroupCondition>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14>>
        where TField1 : IBqlSortColumn
        where TField2 : IBqlSortColumn
        where TField3 : IBqlSortColumn
        where TField4 : IBqlSortColumn
        where TField5 : IBqlSortColumn
        where TField6 : IBqlSortColumn
        where TField7 : IBqlSortColumn
        where TField8 : IBqlSortColumn
        where TField9 : IBqlSortColumn
        where TField10 : IBqlSortColumn
        where TField11 : IBqlSortColumn
        where TField12 : IBqlSortColumn
        where TField13 : IBqlSortColumn
        where TField14 : IBqlSortColumn
      {
      }

      /// <exclude />
      public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15> : 
        SelectFromBase<TTable, TJoins>.Aggregate<TFunctionArray>.Having<TGroupCondition>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15>>
        where TField1 : IBqlSortColumn
        where TField2 : IBqlSortColumn
        where TField3 : IBqlSortColumn
        where TField4 : IBqlSortColumn
        where TField5 : IBqlSortColumn
        where TField6 : IBqlSortColumn
        where TField7 : IBqlSortColumn
        where TField8 : IBqlSortColumn
        where TField9 : IBqlSortColumn
        where TField10 : IBqlSortColumn
        where TField11 : IBqlSortColumn
        where TField12 : IBqlSortColumn
        where TField13 : IBqlSortColumn
        where TField14 : IBqlSortColumn
        where TField15 : IBqlSortColumn
      {
      }

      /// <exclude />
      public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16> : 
        SelectFromBase<TTable, TJoins>.Aggregate<TFunctionArray>.Having<TGroupCondition>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16>>
        where TField1 : IBqlSortColumn
        where TField2 : IBqlSortColumn
        where TField3 : IBqlSortColumn
        where TField4 : IBqlSortColumn
        where TField5 : IBqlSortColumn
        where TField6 : IBqlSortColumn
        where TField7 : IBqlSortColumn
        where TField8 : IBqlSortColumn
        where TField9 : IBqlSortColumn
        where TField10 : IBqlSortColumn
        where TField11 : IBqlSortColumn
        where TField12 : IBqlSortColumn
        where TField13 : IBqlSortColumn
        where TField14 : IBqlSortColumn
        where TField15 : IBqlSortColumn
        where TField16 : IBqlSortColumn
      {
      }

      /// <exclude />
      public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17> : 
        SelectFromBase<TTable, TJoins>.Aggregate<TFunctionArray>.Having<TGroupCondition>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17>>
        where TField1 : IBqlSortColumn
        where TField2 : IBqlSortColumn
        where TField3 : IBqlSortColumn
        where TField4 : IBqlSortColumn
        where TField5 : IBqlSortColumn
        where TField6 : IBqlSortColumn
        where TField7 : IBqlSortColumn
        where TField8 : IBqlSortColumn
        where TField9 : IBqlSortColumn
        where TField10 : IBqlSortColumn
        where TField11 : IBqlSortColumn
        where TField12 : IBqlSortColumn
        where TField13 : IBqlSortColumn
        where TField14 : IBqlSortColumn
        where TField15 : IBqlSortColumn
        where TField16 : IBqlSortColumn
        where TField17 : IBqlSortColumn
      {
      }

      /// <exclude />
      public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18> : 
        SelectFromBase<TTable, TJoins>.Aggregate<TFunctionArray>.Having<TGroupCondition>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18>>
        where TField1 : IBqlSortColumn
        where TField2 : IBqlSortColumn
        where TField3 : IBqlSortColumn
        where TField4 : IBqlSortColumn
        where TField5 : IBqlSortColumn
        where TField6 : IBqlSortColumn
        where TField7 : IBqlSortColumn
        where TField8 : IBqlSortColumn
        where TField9 : IBqlSortColumn
        where TField10 : IBqlSortColumn
        where TField11 : IBqlSortColumn
        where TField12 : IBqlSortColumn
        where TField13 : IBqlSortColumn
        where TField14 : IBqlSortColumn
        where TField15 : IBqlSortColumn
        where TField16 : IBqlSortColumn
        where TField17 : IBqlSortColumn
        where TField18 : IBqlSortColumn
      {
      }

      /// <exclude />
      public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19> : 
        SelectFromBase<TTable, TJoins>.Aggregate<TFunctionArray>.Having<TGroupCondition>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19>>
        where TField1 : IBqlSortColumn
        where TField2 : IBqlSortColumn
        where TField3 : IBqlSortColumn
        where TField4 : IBqlSortColumn
        where TField5 : IBqlSortColumn
        where TField6 : IBqlSortColumn
        where TField7 : IBqlSortColumn
        where TField8 : IBqlSortColumn
        where TField9 : IBqlSortColumn
        where TField10 : IBqlSortColumn
        where TField11 : IBqlSortColumn
        where TField12 : IBqlSortColumn
        where TField13 : IBqlSortColumn
        where TField14 : IBqlSortColumn
        where TField15 : IBqlSortColumn
        where TField16 : IBqlSortColumn
        where TField17 : IBqlSortColumn
        where TField18 : IBqlSortColumn
        where TField19 : IBqlSortColumn
      {
      }

      /// <exclude />
      public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20> : 
        SelectFromBase<TTable, TJoins>.Aggregate<TFunctionArray>.Having<TGroupCondition>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20>>
        where TField1 : IBqlSortColumn
        where TField2 : IBqlSortColumn
        where TField3 : IBqlSortColumn
        where TField4 : IBqlSortColumn
        where TField5 : IBqlSortColumn
        where TField6 : IBqlSortColumn
        where TField7 : IBqlSortColumn
        where TField8 : IBqlSortColumn
        where TField9 : IBqlSortColumn
        where TField10 : IBqlSortColumn
        where TField11 : IBqlSortColumn
        where TField12 : IBqlSortColumn
        where TField13 : IBqlSortColumn
        where TField14 : IBqlSortColumn
        where TField15 : IBqlSortColumn
        where TField16 : IBqlSortColumn
        where TField17 : IBqlSortColumn
        where TField18 : IBqlSortColumn
        where TField19 : IBqlSortColumn
        where TField20 : IBqlSortColumn
      {
      }

      /// <exclude />
      public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20, TField21> : 
        SelectFromBase<TTable, TJoins>.Aggregate<TFunctionArray>.Having<TGroupCondition>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20, TField21>>
        where TField1 : IBqlSortColumn
        where TField2 : IBqlSortColumn
        where TField3 : IBqlSortColumn
        where TField4 : IBqlSortColumn
        where TField5 : IBqlSortColumn
        where TField6 : IBqlSortColumn
        where TField7 : IBqlSortColumn
        where TField8 : IBqlSortColumn
        where TField9 : IBqlSortColumn
        where TField10 : IBqlSortColumn
        where TField11 : IBqlSortColumn
        where TField12 : IBqlSortColumn
        where TField13 : IBqlSortColumn
        where TField14 : IBqlSortColumn
        where TField15 : IBqlSortColumn
        where TField16 : IBqlSortColumn
        where TField17 : IBqlSortColumn
        where TField18 : IBqlSortColumn
        where TField19 : IBqlSortColumn
        where TField20 : IBqlSortColumn
        where TField21 : IBqlSortColumn
      {
      }

      /// <exclude />
      public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20, TField21, TField22> : 
        SelectFromBase<TTable, TJoins>.Aggregate<TFunctionArray>.Having<TGroupCondition>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20, TField21, TField22>>
        where TField1 : IBqlSortColumn
        where TField2 : IBqlSortColumn
        where TField3 : IBqlSortColumn
        where TField4 : IBqlSortColumn
        where TField5 : IBqlSortColumn
        where TField6 : IBqlSortColumn
        where TField7 : IBqlSortColumn
        where TField8 : IBqlSortColumn
        where TField9 : IBqlSortColumn
        where TField10 : IBqlSortColumn
        where TField11 : IBqlSortColumn
        where TField12 : IBqlSortColumn
        where TField13 : IBqlSortColumn
        where TField14 : IBqlSortColumn
        where TField15 : IBqlSortColumn
        where TField16 : IBqlSortColumn
        where TField17 : IBqlSortColumn
        where TField18 : IBqlSortColumn
        where TField19 : IBqlSortColumn
        where TField20 : IBqlSortColumn
        where TField21 : IBqlSortColumn
        where TField22 : IBqlSortColumn
      {
      }

      /// <exclude />
      public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20, TField21, TField22, TField23> : 
        SelectFromBase<TTable, TJoins>.Aggregate<TFunctionArray>.Having<TGroupCondition>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20, TField21, TField22, TField23>>
        where TField1 : IBqlSortColumn
        where TField2 : IBqlSortColumn
        where TField3 : IBqlSortColumn
        where TField4 : IBqlSortColumn
        where TField5 : IBqlSortColumn
        where TField6 : IBqlSortColumn
        where TField7 : IBqlSortColumn
        where TField8 : IBqlSortColumn
        where TField9 : IBqlSortColumn
        where TField10 : IBqlSortColumn
        where TField11 : IBqlSortColumn
        where TField12 : IBqlSortColumn
        where TField13 : IBqlSortColumn
        where TField14 : IBqlSortColumn
        where TField15 : IBqlSortColumn
        where TField16 : IBqlSortColumn
        where TField17 : IBqlSortColumn
        where TField18 : IBqlSortColumn
        where TField19 : IBqlSortColumn
        where TField20 : IBqlSortColumn
        where TField21 : IBqlSortColumn
        where TField22 : IBqlSortColumn
        where TField23 : IBqlSortColumn
      {
      }

      /// <exclude />
      public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20, TField21, TField22, TField23, TField24> : 
        SelectFromBase<TTable, TJoins>.Aggregate<TFunctionArray>.Having<TGroupCondition>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20, TField21, TField22, TField23, TField24>>
        where TField1 : IBqlSortColumn
        where TField2 : IBqlSortColumn
        where TField3 : IBqlSortColumn
        where TField4 : IBqlSortColumn
        where TField5 : IBqlSortColumn
        where TField6 : IBqlSortColumn
        where TField7 : IBqlSortColumn
        where TField8 : IBqlSortColumn
        where TField9 : IBqlSortColumn
        where TField10 : IBqlSortColumn
        where TField11 : IBqlSortColumn
        where TField12 : IBqlSortColumn
        where TField13 : IBqlSortColumn
        where TField14 : IBqlSortColumn
        where TField15 : IBqlSortColumn
        where TField16 : IBqlSortColumn
        where TField17 : IBqlSortColumn
        where TField18 : IBqlSortColumn
        where TField19 : IBqlSortColumn
        where TField20 : IBqlSortColumn
        where TField21 : IBqlSortColumn
        where TField22 : IBqlSortColumn
        where TField23 : IBqlSortColumn
        where TField24 : IBqlSortColumn
      {
      }

      /// <exclude />
      public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20, TField21, TField22, TField23, TField24, TField25> : 
        SelectFromBase<TTable, TJoins>.Aggregate<TFunctionArray>.Having<TGroupCondition>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20, TField21, TField22, TField23, TField24, TField25>>
        where TField1 : IBqlSortColumn
        where TField2 : IBqlSortColumn
        where TField3 : IBqlSortColumn
        where TField4 : IBqlSortColumn
        where TField5 : IBqlSortColumn
        where TField6 : IBqlSortColumn
        where TField7 : IBqlSortColumn
        where TField8 : IBqlSortColumn
        where TField9 : IBqlSortColumn
        where TField10 : IBqlSortColumn
        where TField11 : IBqlSortColumn
        where TField12 : IBqlSortColumn
        where TField13 : IBqlSortColumn
        where TField14 : IBqlSortColumn
        where TField15 : IBqlSortColumn
        where TField16 : IBqlSortColumn
        where TField17 : IBqlSortColumn
        where TField18 : IBqlSortColumn
        where TField19 : IBqlSortColumn
        where TField20 : IBqlSortColumn
        where TField21 : IBqlSortColumn
        where TField22 : IBqlSortColumn
        where TField23 : IBqlSortColumn
        where TField24 : IBqlSortColumn
        where TField25 : IBqlSortColumn
      {
      }

      /// <exclude />
      public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20, TField21, TField22, TField23, TField24, TField25, TField26> : 
        SelectFromBase<TTable, TJoins>.Aggregate<TFunctionArray>.Having<TGroupCondition>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20, TField21, TField22, TField23, TField24, TField25, TField26>>
        where TField1 : IBqlSortColumn
        where TField2 : IBqlSortColumn
        where TField3 : IBqlSortColumn
        where TField4 : IBqlSortColumn
        where TField5 : IBqlSortColumn
        where TField6 : IBqlSortColumn
        where TField7 : IBqlSortColumn
        where TField8 : IBqlSortColumn
        where TField9 : IBqlSortColumn
        where TField10 : IBqlSortColumn
        where TField11 : IBqlSortColumn
        where TField12 : IBqlSortColumn
        where TField13 : IBqlSortColumn
        where TField14 : IBqlSortColumn
        where TField15 : IBqlSortColumn
        where TField16 : IBqlSortColumn
        where TField17 : IBqlSortColumn
        where TField18 : IBqlSortColumn
        where TField19 : IBqlSortColumn
        where TField20 : IBqlSortColumn
        where TField21 : IBqlSortColumn
        where TField22 : IBqlSortColumn
        where TField23 : IBqlSortColumn
        where TField24 : IBqlSortColumn
        where TField25 : IBqlSortColumn
        where TField26 : IBqlSortColumn
      {
      }

      /// <exclude />
      public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20, TField21, TField22, TField23, TField24, TField25, TField26, TField27> : 
        SelectFromBase<TTable, TJoins>.Aggregate<TFunctionArray>.Having<TGroupCondition>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20, TField21, TField22, TField23, TField24, TField25, TField26, TField27>>
        where TField1 : IBqlSortColumn
        where TField2 : IBqlSortColumn
        where TField3 : IBqlSortColumn
        where TField4 : IBqlSortColumn
        where TField5 : IBqlSortColumn
        where TField6 : IBqlSortColumn
        where TField7 : IBqlSortColumn
        where TField8 : IBqlSortColumn
        where TField9 : IBqlSortColumn
        where TField10 : IBqlSortColumn
        where TField11 : IBqlSortColumn
        where TField12 : IBqlSortColumn
        where TField13 : IBqlSortColumn
        where TField14 : IBqlSortColumn
        where TField15 : IBqlSortColumn
        where TField16 : IBqlSortColumn
        where TField17 : IBqlSortColumn
        where TField18 : IBqlSortColumn
        where TField19 : IBqlSortColumn
        where TField20 : IBqlSortColumn
        where TField21 : IBqlSortColumn
        where TField22 : IBqlSortColumn
        where TField23 : IBqlSortColumn
        where TField24 : IBqlSortColumn
        where TField25 : IBqlSortColumn
        where TField26 : IBqlSortColumn
        where TField27 : IBqlSortColumn
      {
      }

      /// <exclude />
      public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20, TField21, TField22, TField23, TField24, TField25, TField26, TField27, TField28> : 
        SelectFromBase<TTable, TJoins>.Aggregate<TFunctionArray>.Having<TGroupCondition>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20, TField21, TField22, TField23, TField24, TField25, TField26, TField27, TField28>>
        where TField1 : IBqlSortColumn
        where TField2 : IBqlSortColumn
        where TField3 : IBqlSortColumn
        where TField4 : IBqlSortColumn
        where TField5 : IBqlSortColumn
        where TField6 : IBqlSortColumn
        where TField7 : IBqlSortColumn
        where TField8 : IBqlSortColumn
        where TField9 : IBqlSortColumn
        where TField10 : IBqlSortColumn
        where TField11 : IBqlSortColumn
        where TField12 : IBqlSortColumn
        where TField13 : IBqlSortColumn
        where TField14 : IBqlSortColumn
        where TField15 : IBqlSortColumn
        where TField16 : IBqlSortColumn
        where TField17 : IBqlSortColumn
        where TField18 : IBqlSortColumn
        where TField19 : IBqlSortColumn
        where TField20 : IBqlSortColumn
        where TField21 : IBqlSortColumn
        where TField22 : IBqlSortColumn
        where TField23 : IBqlSortColumn
        where TField24 : IBqlSortColumn
        where TField25 : IBqlSortColumn
        where TField26 : IBqlSortColumn
        where TField27 : IBqlSortColumn
        where TField28 : IBqlSortColumn
      {
      }

      /// <exclude />
      public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20, TField21, TField22, TField23, TField24, TField25, TField26, TField27, TField28, TField29> : 
        SelectFromBase<TTable, TJoins>.Aggregate<TFunctionArray>.Having<TGroupCondition>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20, TField21, TField22, TField23, TField24, TField25, TField26, TField27, TField28, TField29>>
        where TField1 : IBqlSortColumn
        where TField2 : IBqlSortColumn
        where TField3 : IBqlSortColumn
        where TField4 : IBqlSortColumn
        where TField5 : IBqlSortColumn
        where TField6 : IBqlSortColumn
        where TField7 : IBqlSortColumn
        where TField8 : IBqlSortColumn
        where TField9 : IBqlSortColumn
        where TField10 : IBqlSortColumn
        where TField11 : IBqlSortColumn
        where TField12 : IBqlSortColumn
        where TField13 : IBqlSortColumn
        where TField14 : IBqlSortColumn
        where TField15 : IBqlSortColumn
        where TField16 : IBqlSortColumn
        where TField17 : IBqlSortColumn
        where TField18 : IBqlSortColumn
        where TField19 : IBqlSortColumn
        where TField20 : IBqlSortColumn
        where TField21 : IBqlSortColumn
        where TField22 : IBqlSortColumn
        where TField23 : IBqlSortColumn
        where TField24 : IBqlSortColumn
        where TField25 : IBqlSortColumn
        where TField26 : IBqlSortColumn
        where TField27 : IBqlSortColumn
        where TField28 : IBqlSortColumn
        where TField29 : IBqlSortColumn
      {
      }

      /// <exclude />
      public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20, TField21, TField22, TField23, TField24, TField25, TField26, TField27, TField28, TField29, TField30> : 
        SelectFromBase<TTable, TJoins>.Aggregate<TFunctionArray>.Having<TGroupCondition>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20, TField21, TField22, TField23, TField24, TField25, TField26, TField27, TField28, TField29, TField30>>
        where TField1 : IBqlSortColumn
        where TField2 : IBqlSortColumn
        where TField3 : IBqlSortColumn
        where TField4 : IBqlSortColumn
        where TField5 : IBqlSortColumn
        where TField6 : IBqlSortColumn
        where TField7 : IBqlSortColumn
        where TField8 : IBqlSortColumn
        where TField9 : IBqlSortColumn
        where TField10 : IBqlSortColumn
        where TField11 : IBqlSortColumn
        where TField12 : IBqlSortColumn
        where TField13 : IBqlSortColumn
        where TField14 : IBqlSortColumn
        where TField15 : IBqlSortColumn
        where TField16 : IBqlSortColumn
        where TField17 : IBqlSortColumn
        where TField18 : IBqlSortColumn
        where TField19 : IBqlSortColumn
        where TField20 : IBqlSortColumn
        where TField21 : IBqlSortColumn
        where TField22 : IBqlSortColumn
        where TField23 : IBqlSortColumn
        where TField24 : IBqlSortColumn
        where TField25 : IBqlSortColumn
        where TField26 : IBqlSortColumn
        where TField27 : IBqlSortColumn
        where TField28 : IBqlSortColumn
        where TField29 : IBqlSortColumn
        where TField30 : IBqlSortColumn
      {
      }

      /// <exclude />
      public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20, TField21, TField22, TField23, TField24, TField25, TField26, TField27, TField28, TField29, TField30, TField31> : 
        SelectFromBase<TTable, TJoins>.Aggregate<TFunctionArray>.Having<TGroupCondition>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20, TField21, TField22, TField23, TField24, TField25, TField26, TField27, TField28, TField29, TField30, TField31>>
        where TField1 : IBqlSortColumn
        where TField2 : IBqlSortColumn
        where TField3 : IBqlSortColumn
        where TField4 : IBqlSortColumn
        where TField5 : IBqlSortColumn
        where TField6 : IBqlSortColumn
        where TField7 : IBqlSortColumn
        where TField8 : IBqlSortColumn
        where TField9 : IBqlSortColumn
        where TField10 : IBqlSortColumn
        where TField11 : IBqlSortColumn
        where TField12 : IBqlSortColumn
        where TField13 : IBqlSortColumn
        where TField14 : IBqlSortColumn
        where TField15 : IBqlSortColumn
        where TField16 : IBqlSortColumn
        where TField17 : IBqlSortColumn
        where TField18 : IBqlSortColumn
        where TField19 : IBqlSortColumn
        where TField20 : IBqlSortColumn
        where TField21 : IBqlSortColumn
        where TField22 : IBqlSortColumn
        where TField23 : IBqlSortColumn
        where TField24 : IBqlSortColumn
        where TField25 : IBqlSortColumn
        where TField26 : IBqlSortColumn
        where TField27 : IBqlSortColumn
        where TField28 : IBqlSortColumn
        where TField29 : IBqlSortColumn
        where TField30 : IBqlSortColumn
        where TField31 : IBqlSortColumn
      {
      }

      /// <exclude />
      public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20, TField21, TField22, TField23, TField24, TField25, TField26, TField27, TField28, TField29, TField30, TField31, TField32> : 
        SelectFromBase<TTable, TJoins>.Aggregate<TFunctionArray>.Having<TGroupCondition>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20, TField21, TField22, TField23, TField24, TField25, TField26, TField27, TField28, TField29, TField30, TField31, TField32>>
        where TField1 : IBqlSortColumn
        where TField2 : IBqlSortColumn
        where TField3 : IBqlSortColumn
        where TField4 : IBqlSortColumn
        where TField5 : IBqlSortColumn
        where TField6 : IBqlSortColumn
        where TField7 : IBqlSortColumn
        where TField8 : IBqlSortColumn
        where TField9 : IBqlSortColumn
        where TField10 : IBqlSortColumn
        where TField11 : IBqlSortColumn
        where TField12 : IBqlSortColumn
        where TField13 : IBqlSortColumn
        where TField14 : IBqlSortColumn
        where TField15 : IBqlSortColumn
        where TField16 : IBqlSortColumn
        where TField17 : IBqlSortColumn
        where TField18 : IBqlSortColumn
        where TField19 : IBqlSortColumn
        where TField20 : IBqlSortColumn
        where TField21 : IBqlSortColumn
        where TField22 : IBqlSortColumn
        where TField23 : IBqlSortColumn
        where TField24 : IBqlSortColumn
        where TField25 : IBqlSortColumn
        where TField26 : IBqlSortColumn
        where TField27 : IBqlSortColumn
        where TField28 : IBqlSortColumn
        where TField29 : IBqlSortColumn
        where TField30 : IBqlSortColumn
        where TField31 : IBqlSortColumn
        where TField32 : IBqlSortColumn
      {
      }

      public class Order<TSortColumnArray> : 
        FbqlSelect<SelectFromBase<TTable, TJoins>.Aggregate<TFunctionArray>.Having<TGroupCondition>.Order<TSortColumnArray>, TTable, TJoins, BqlNone, TFunctionArray, Having<TGroupCondition>, TSortColumnArray>
        where TSortColumnArray : ITypeArrayOf<IBqlSortColumn>, TypeArray.IsNotEmpty
      {
      }
    }

    public class Order<TSortColumnArray> : 
      FbqlSelect<SelectFromBase<TTable, TJoins>.Aggregate<TFunctionArray>.Order<TSortColumnArray>, TTable, TJoins, BqlNone, TFunctionArray, BqlNone, TSortColumnArray>
      where TSortColumnArray : ITypeArrayOf<IBqlSortColumn>, TypeArray.IsNotEmpty
    {
    }
  }

  /// <exclude />
  public class OrderBy<TField> : SelectFromBase<TTable, TJoins>.Order<By<TField>> where TField : IBqlSortColumn
  {
  }

  /// <exclude />
  public class OrderBy<TField1, TField2> : SelectFromBase<TTable, TJoins>.Order<By<TField1, TField2>>
    where TField1 : IBqlSortColumn
    where TField2 : IBqlSortColumn
  {
  }

  /// <exclude />
  public class OrderBy<TField1, TField2, TField3> : 
    SelectFromBase<TTable, TJoins>.Order<By<TField1, TField2, TField3>>
    where TField1 : IBqlSortColumn
    where TField2 : IBqlSortColumn
    where TField3 : IBqlSortColumn
  {
  }

  /// <exclude />
  public class OrderBy<TField1, TField2, TField3, TField4> : 
    SelectFromBase<TTable, TJoins>.Order<By<TField1, TField2, TField3, TField4>>
    where TField1 : IBqlSortColumn
    where TField2 : IBqlSortColumn
    where TField3 : IBqlSortColumn
    where TField4 : IBqlSortColumn
  {
  }

  /// <exclude />
  public class OrderBy<TField1, TField2, TField3, TField4, TField5> : 
    SelectFromBase<TTable, TJoins>.Order<By<TField1, TField2, TField3, TField4, TField5>>
    where TField1 : IBqlSortColumn
    where TField2 : IBqlSortColumn
    where TField3 : IBqlSortColumn
    where TField4 : IBqlSortColumn
    where TField5 : IBqlSortColumn
  {
  }

  /// <exclude />
  public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6> : 
    SelectFromBase<TTable, TJoins>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6>>
    where TField1 : IBqlSortColumn
    where TField2 : IBqlSortColumn
    where TField3 : IBqlSortColumn
    where TField4 : IBqlSortColumn
    where TField5 : IBqlSortColumn
    where TField6 : IBqlSortColumn
  {
  }

  /// <exclude />
  public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7> : 
    SelectFromBase<TTable, TJoins>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7>>
    where TField1 : IBqlSortColumn
    where TField2 : IBqlSortColumn
    where TField3 : IBqlSortColumn
    where TField4 : IBqlSortColumn
    where TField5 : IBqlSortColumn
    where TField6 : IBqlSortColumn
    where TField7 : IBqlSortColumn
  {
  }

  /// <exclude />
  public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8> : 
    SelectFromBase<TTable, TJoins>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8>>
    where TField1 : IBqlSortColumn
    where TField2 : IBqlSortColumn
    where TField3 : IBqlSortColumn
    where TField4 : IBqlSortColumn
    where TField5 : IBqlSortColumn
    where TField6 : IBqlSortColumn
    where TField7 : IBqlSortColumn
    where TField8 : IBqlSortColumn
  {
  }

  /// <exclude />
  public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9> : 
    SelectFromBase<TTable, TJoins>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9>>
    where TField1 : IBqlSortColumn
    where TField2 : IBqlSortColumn
    where TField3 : IBqlSortColumn
    where TField4 : IBqlSortColumn
    where TField5 : IBqlSortColumn
    where TField6 : IBqlSortColumn
    where TField7 : IBqlSortColumn
    where TField8 : IBqlSortColumn
    where TField9 : IBqlSortColumn
  {
  }

  /// <exclude />
  public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10> : 
    SelectFromBase<TTable, TJoins>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10>>
    where TField1 : IBqlSortColumn
    where TField2 : IBqlSortColumn
    where TField3 : IBqlSortColumn
    where TField4 : IBqlSortColumn
    where TField5 : IBqlSortColumn
    where TField6 : IBqlSortColumn
    where TField7 : IBqlSortColumn
    where TField8 : IBqlSortColumn
    where TField9 : IBqlSortColumn
    where TField10 : IBqlSortColumn
  {
  }

  /// <exclude />
  public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11> : 
    SelectFromBase<TTable, TJoins>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11>>
    where TField1 : IBqlSortColumn
    where TField2 : IBqlSortColumn
    where TField3 : IBqlSortColumn
    where TField4 : IBqlSortColumn
    where TField5 : IBqlSortColumn
    where TField6 : IBqlSortColumn
    where TField7 : IBqlSortColumn
    where TField8 : IBqlSortColumn
    where TField9 : IBqlSortColumn
    where TField10 : IBqlSortColumn
    where TField11 : IBqlSortColumn
  {
  }

  /// <exclude />
  public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12> : 
    SelectFromBase<TTable, TJoins>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12>>
    where TField1 : IBqlSortColumn
    where TField2 : IBqlSortColumn
    where TField3 : IBqlSortColumn
    where TField4 : IBqlSortColumn
    where TField5 : IBqlSortColumn
    where TField6 : IBqlSortColumn
    where TField7 : IBqlSortColumn
    where TField8 : IBqlSortColumn
    where TField9 : IBqlSortColumn
    where TField10 : IBqlSortColumn
    where TField11 : IBqlSortColumn
    where TField12 : IBqlSortColumn
  {
  }

  /// <exclude />
  public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13> : 
    SelectFromBase<TTable, TJoins>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13>>
    where TField1 : IBqlSortColumn
    where TField2 : IBqlSortColumn
    where TField3 : IBqlSortColumn
    where TField4 : IBqlSortColumn
    where TField5 : IBqlSortColumn
    where TField6 : IBqlSortColumn
    where TField7 : IBqlSortColumn
    where TField8 : IBqlSortColumn
    where TField9 : IBqlSortColumn
    where TField10 : IBqlSortColumn
    where TField11 : IBqlSortColumn
    where TField12 : IBqlSortColumn
    where TField13 : IBqlSortColumn
  {
  }

  /// <exclude />
  public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14> : 
    SelectFromBase<TTable, TJoins>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14>>
    where TField1 : IBqlSortColumn
    where TField2 : IBqlSortColumn
    where TField3 : IBqlSortColumn
    where TField4 : IBqlSortColumn
    where TField5 : IBqlSortColumn
    where TField6 : IBqlSortColumn
    where TField7 : IBqlSortColumn
    where TField8 : IBqlSortColumn
    where TField9 : IBqlSortColumn
    where TField10 : IBqlSortColumn
    where TField11 : IBqlSortColumn
    where TField12 : IBqlSortColumn
    where TField13 : IBqlSortColumn
    where TField14 : IBqlSortColumn
  {
  }

  /// <exclude />
  public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15> : 
    SelectFromBase<TTable, TJoins>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15>>
    where TField1 : IBqlSortColumn
    where TField2 : IBqlSortColumn
    where TField3 : IBqlSortColumn
    where TField4 : IBqlSortColumn
    where TField5 : IBqlSortColumn
    where TField6 : IBqlSortColumn
    where TField7 : IBqlSortColumn
    where TField8 : IBqlSortColumn
    where TField9 : IBqlSortColumn
    where TField10 : IBqlSortColumn
    where TField11 : IBqlSortColumn
    where TField12 : IBqlSortColumn
    where TField13 : IBqlSortColumn
    where TField14 : IBqlSortColumn
    where TField15 : IBqlSortColumn
  {
  }

  /// <exclude />
  public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16> : 
    SelectFromBase<TTable, TJoins>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16>>
    where TField1 : IBqlSortColumn
    where TField2 : IBqlSortColumn
    where TField3 : IBqlSortColumn
    where TField4 : IBqlSortColumn
    where TField5 : IBqlSortColumn
    where TField6 : IBqlSortColumn
    where TField7 : IBqlSortColumn
    where TField8 : IBqlSortColumn
    where TField9 : IBqlSortColumn
    where TField10 : IBqlSortColumn
    where TField11 : IBqlSortColumn
    where TField12 : IBqlSortColumn
    where TField13 : IBqlSortColumn
    where TField14 : IBqlSortColumn
    where TField15 : IBqlSortColumn
    where TField16 : IBqlSortColumn
  {
  }

  /// <exclude />
  public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17> : 
    SelectFromBase<TTable, TJoins>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17>>
    where TField1 : IBqlSortColumn
    where TField2 : IBqlSortColumn
    where TField3 : IBqlSortColumn
    where TField4 : IBqlSortColumn
    where TField5 : IBqlSortColumn
    where TField6 : IBqlSortColumn
    where TField7 : IBqlSortColumn
    where TField8 : IBqlSortColumn
    where TField9 : IBqlSortColumn
    where TField10 : IBqlSortColumn
    where TField11 : IBqlSortColumn
    where TField12 : IBqlSortColumn
    where TField13 : IBqlSortColumn
    where TField14 : IBqlSortColumn
    where TField15 : IBqlSortColumn
    where TField16 : IBqlSortColumn
    where TField17 : IBqlSortColumn
  {
  }

  /// <exclude />
  public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18> : 
    SelectFromBase<TTable, TJoins>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18>>
    where TField1 : IBqlSortColumn
    where TField2 : IBqlSortColumn
    where TField3 : IBqlSortColumn
    where TField4 : IBqlSortColumn
    where TField5 : IBqlSortColumn
    where TField6 : IBqlSortColumn
    where TField7 : IBqlSortColumn
    where TField8 : IBqlSortColumn
    where TField9 : IBqlSortColumn
    where TField10 : IBqlSortColumn
    where TField11 : IBqlSortColumn
    where TField12 : IBqlSortColumn
    where TField13 : IBqlSortColumn
    where TField14 : IBqlSortColumn
    where TField15 : IBqlSortColumn
    where TField16 : IBqlSortColumn
    where TField17 : IBqlSortColumn
    where TField18 : IBqlSortColumn
  {
  }

  /// <exclude />
  public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19> : 
    SelectFromBase<TTable, TJoins>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19>>
    where TField1 : IBqlSortColumn
    where TField2 : IBqlSortColumn
    where TField3 : IBqlSortColumn
    where TField4 : IBqlSortColumn
    where TField5 : IBqlSortColumn
    where TField6 : IBqlSortColumn
    where TField7 : IBqlSortColumn
    where TField8 : IBqlSortColumn
    where TField9 : IBqlSortColumn
    where TField10 : IBqlSortColumn
    where TField11 : IBqlSortColumn
    where TField12 : IBqlSortColumn
    where TField13 : IBqlSortColumn
    where TField14 : IBqlSortColumn
    where TField15 : IBqlSortColumn
    where TField16 : IBqlSortColumn
    where TField17 : IBqlSortColumn
    where TField18 : IBqlSortColumn
    where TField19 : IBqlSortColumn
  {
  }

  /// <exclude />
  public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20> : 
    SelectFromBase<TTable, TJoins>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20>>
    where TField1 : IBqlSortColumn
    where TField2 : IBqlSortColumn
    where TField3 : IBqlSortColumn
    where TField4 : IBqlSortColumn
    where TField5 : IBqlSortColumn
    where TField6 : IBqlSortColumn
    where TField7 : IBqlSortColumn
    where TField8 : IBqlSortColumn
    where TField9 : IBqlSortColumn
    where TField10 : IBqlSortColumn
    where TField11 : IBqlSortColumn
    where TField12 : IBqlSortColumn
    where TField13 : IBqlSortColumn
    where TField14 : IBqlSortColumn
    where TField15 : IBqlSortColumn
    where TField16 : IBqlSortColumn
    where TField17 : IBqlSortColumn
    where TField18 : IBqlSortColumn
    where TField19 : IBqlSortColumn
    where TField20 : IBqlSortColumn
  {
  }

  /// <exclude />
  public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20, TField21> : 
    SelectFromBase<TTable, TJoins>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20, TField21>>
    where TField1 : IBqlSortColumn
    where TField2 : IBqlSortColumn
    where TField3 : IBqlSortColumn
    where TField4 : IBqlSortColumn
    where TField5 : IBqlSortColumn
    where TField6 : IBqlSortColumn
    where TField7 : IBqlSortColumn
    where TField8 : IBqlSortColumn
    where TField9 : IBqlSortColumn
    where TField10 : IBqlSortColumn
    where TField11 : IBqlSortColumn
    where TField12 : IBqlSortColumn
    where TField13 : IBqlSortColumn
    where TField14 : IBqlSortColumn
    where TField15 : IBqlSortColumn
    where TField16 : IBqlSortColumn
    where TField17 : IBqlSortColumn
    where TField18 : IBqlSortColumn
    where TField19 : IBqlSortColumn
    where TField20 : IBqlSortColumn
    where TField21 : IBqlSortColumn
  {
  }

  /// <exclude />
  public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20, TField21, TField22> : 
    SelectFromBase<TTable, TJoins>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20, TField21, TField22>>
    where TField1 : IBqlSortColumn
    where TField2 : IBqlSortColumn
    where TField3 : IBqlSortColumn
    where TField4 : IBqlSortColumn
    where TField5 : IBqlSortColumn
    where TField6 : IBqlSortColumn
    where TField7 : IBqlSortColumn
    where TField8 : IBqlSortColumn
    where TField9 : IBqlSortColumn
    where TField10 : IBqlSortColumn
    where TField11 : IBqlSortColumn
    where TField12 : IBqlSortColumn
    where TField13 : IBqlSortColumn
    where TField14 : IBqlSortColumn
    where TField15 : IBqlSortColumn
    where TField16 : IBqlSortColumn
    where TField17 : IBqlSortColumn
    where TField18 : IBqlSortColumn
    where TField19 : IBqlSortColumn
    where TField20 : IBqlSortColumn
    where TField21 : IBqlSortColumn
    where TField22 : IBqlSortColumn
  {
  }

  /// <exclude />
  public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20, TField21, TField22, TField23> : 
    SelectFromBase<TTable, TJoins>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20, TField21, TField22, TField23>>
    where TField1 : IBqlSortColumn
    where TField2 : IBqlSortColumn
    where TField3 : IBqlSortColumn
    where TField4 : IBqlSortColumn
    where TField5 : IBqlSortColumn
    where TField6 : IBqlSortColumn
    where TField7 : IBqlSortColumn
    where TField8 : IBqlSortColumn
    where TField9 : IBqlSortColumn
    where TField10 : IBqlSortColumn
    where TField11 : IBqlSortColumn
    where TField12 : IBqlSortColumn
    where TField13 : IBqlSortColumn
    where TField14 : IBqlSortColumn
    where TField15 : IBqlSortColumn
    where TField16 : IBqlSortColumn
    where TField17 : IBqlSortColumn
    where TField18 : IBqlSortColumn
    where TField19 : IBqlSortColumn
    where TField20 : IBqlSortColumn
    where TField21 : IBqlSortColumn
    where TField22 : IBqlSortColumn
    where TField23 : IBqlSortColumn
  {
  }

  /// <exclude />
  public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20, TField21, TField22, TField23, TField24> : 
    SelectFromBase<TTable, TJoins>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20, TField21, TField22, TField23, TField24>>
    where TField1 : IBqlSortColumn
    where TField2 : IBqlSortColumn
    where TField3 : IBqlSortColumn
    where TField4 : IBqlSortColumn
    where TField5 : IBqlSortColumn
    where TField6 : IBqlSortColumn
    where TField7 : IBqlSortColumn
    where TField8 : IBqlSortColumn
    where TField9 : IBqlSortColumn
    where TField10 : IBqlSortColumn
    where TField11 : IBqlSortColumn
    where TField12 : IBqlSortColumn
    where TField13 : IBqlSortColumn
    where TField14 : IBqlSortColumn
    where TField15 : IBqlSortColumn
    where TField16 : IBqlSortColumn
    where TField17 : IBqlSortColumn
    where TField18 : IBqlSortColumn
    where TField19 : IBqlSortColumn
    where TField20 : IBqlSortColumn
    where TField21 : IBqlSortColumn
    where TField22 : IBqlSortColumn
    where TField23 : IBqlSortColumn
    where TField24 : IBqlSortColumn
  {
  }

  /// <exclude />
  public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20, TField21, TField22, TField23, TField24, TField25> : 
    SelectFromBase<TTable, TJoins>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20, TField21, TField22, TField23, TField24, TField25>>
    where TField1 : IBqlSortColumn
    where TField2 : IBqlSortColumn
    where TField3 : IBqlSortColumn
    where TField4 : IBqlSortColumn
    where TField5 : IBqlSortColumn
    where TField6 : IBqlSortColumn
    where TField7 : IBqlSortColumn
    where TField8 : IBqlSortColumn
    where TField9 : IBqlSortColumn
    where TField10 : IBqlSortColumn
    where TField11 : IBqlSortColumn
    where TField12 : IBqlSortColumn
    where TField13 : IBqlSortColumn
    where TField14 : IBqlSortColumn
    where TField15 : IBqlSortColumn
    where TField16 : IBqlSortColumn
    where TField17 : IBqlSortColumn
    where TField18 : IBqlSortColumn
    where TField19 : IBqlSortColumn
    where TField20 : IBqlSortColumn
    where TField21 : IBqlSortColumn
    where TField22 : IBqlSortColumn
    where TField23 : IBqlSortColumn
    where TField24 : IBqlSortColumn
    where TField25 : IBqlSortColumn
  {
  }

  /// <exclude />
  public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20, TField21, TField22, TField23, TField24, TField25, TField26> : 
    SelectFromBase<TTable, TJoins>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20, TField21, TField22, TField23, TField24, TField25, TField26>>
    where TField1 : IBqlSortColumn
    where TField2 : IBqlSortColumn
    where TField3 : IBqlSortColumn
    where TField4 : IBqlSortColumn
    where TField5 : IBqlSortColumn
    where TField6 : IBqlSortColumn
    where TField7 : IBqlSortColumn
    where TField8 : IBqlSortColumn
    where TField9 : IBqlSortColumn
    where TField10 : IBqlSortColumn
    where TField11 : IBqlSortColumn
    where TField12 : IBqlSortColumn
    where TField13 : IBqlSortColumn
    where TField14 : IBqlSortColumn
    where TField15 : IBqlSortColumn
    where TField16 : IBqlSortColumn
    where TField17 : IBqlSortColumn
    where TField18 : IBqlSortColumn
    where TField19 : IBqlSortColumn
    where TField20 : IBqlSortColumn
    where TField21 : IBqlSortColumn
    where TField22 : IBqlSortColumn
    where TField23 : IBqlSortColumn
    where TField24 : IBqlSortColumn
    where TField25 : IBqlSortColumn
    where TField26 : IBqlSortColumn
  {
  }

  /// <exclude />
  public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20, TField21, TField22, TField23, TField24, TField25, TField26, TField27> : 
    SelectFromBase<TTable, TJoins>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20, TField21, TField22, TField23, TField24, TField25, TField26, TField27>>
    where TField1 : IBqlSortColumn
    where TField2 : IBqlSortColumn
    where TField3 : IBqlSortColumn
    where TField4 : IBqlSortColumn
    where TField5 : IBqlSortColumn
    where TField6 : IBqlSortColumn
    where TField7 : IBqlSortColumn
    where TField8 : IBqlSortColumn
    where TField9 : IBqlSortColumn
    where TField10 : IBqlSortColumn
    where TField11 : IBqlSortColumn
    where TField12 : IBqlSortColumn
    where TField13 : IBqlSortColumn
    where TField14 : IBqlSortColumn
    where TField15 : IBqlSortColumn
    where TField16 : IBqlSortColumn
    where TField17 : IBqlSortColumn
    where TField18 : IBqlSortColumn
    where TField19 : IBqlSortColumn
    where TField20 : IBqlSortColumn
    where TField21 : IBqlSortColumn
    where TField22 : IBqlSortColumn
    where TField23 : IBqlSortColumn
    where TField24 : IBqlSortColumn
    where TField25 : IBqlSortColumn
    where TField26 : IBqlSortColumn
    where TField27 : IBqlSortColumn
  {
  }

  /// <exclude />
  public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20, TField21, TField22, TField23, TField24, TField25, TField26, TField27, TField28> : 
    SelectFromBase<TTable, TJoins>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20, TField21, TField22, TField23, TField24, TField25, TField26, TField27, TField28>>
    where TField1 : IBqlSortColumn
    where TField2 : IBqlSortColumn
    where TField3 : IBqlSortColumn
    where TField4 : IBqlSortColumn
    where TField5 : IBqlSortColumn
    where TField6 : IBqlSortColumn
    where TField7 : IBqlSortColumn
    where TField8 : IBqlSortColumn
    where TField9 : IBqlSortColumn
    where TField10 : IBqlSortColumn
    where TField11 : IBqlSortColumn
    where TField12 : IBqlSortColumn
    where TField13 : IBqlSortColumn
    where TField14 : IBqlSortColumn
    where TField15 : IBqlSortColumn
    where TField16 : IBqlSortColumn
    where TField17 : IBqlSortColumn
    where TField18 : IBqlSortColumn
    where TField19 : IBqlSortColumn
    where TField20 : IBqlSortColumn
    where TField21 : IBqlSortColumn
    where TField22 : IBqlSortColumn
    where TField23 : IBqlSortColumn
    where TField24 : IBqlSortColumn
    where TField25 : IBqlSortColumn
    where TField26 : IBqlSortColumn
    where TField27 : IBqlSortColumn
    where TField28 : IBqlSortColumn
  {
  }

  /// <exclude />
  public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20, TField21, TField22, TField23, TField24, TField25, TField26, TField27, TField28, TField29> : 
    SelectFromBase<TTable, TJoins>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20, TField21, TField22, TField23, TField24, TField25, TField26, TField27, TField28, TField29>>
    where TField1 : IBqlSortColumn
    where TField2 : IBqlSortColumn
    where TField3 : IBqlSortColumn
    where TField4 : IBqlSortColumn
    where TField5 : IBqlSortColumn
    where TField6 : IBqlSortColumn
    where TField7 : IBqlSortColumn
    where TField8 : IBqlSortColumn
    where TField9 : IBqlSortColumn
    where TField10 : IBqlSortColumn
    where TField11 : IBqlSortColumn
    where TField12 : IBqlSortColumn
    where TField13 : IBqlSortColumn
    where TField14 : IBqlSortColumn
    where TField15 : IBqlSortColumn
    where TField16 : IBqlSortColumn
    where TField17 : IBqlSortColumn
    where TField18 : IBqlSortColumn
    where TField19 : IBqlSortColumn
    where TField20 : IBqlSortColumn
    where TField21 : IBqlSortColumn
    where TField22 : IBqlSortColumn
    where TField23 : IBqlSortColumn
    where TField24 : IBqlSortColumn
    where TField25 : IBqlSortColumn
    where TField26 : IBqlSortColumn
    where TField27 : IBqlSortColumn
    where TField28 : IBqlSortColumn
    where TField29 : IBqlSortColumn
  {
  }

  /// <exclude />
  public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20, TField21, TField22, TField23, TField24, TField25, TField26, TField27, TField28, TField29, TField30> : 
    SelectFromBase<TTable, TJoins>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20, TField21, TField22, TField23, TField24, TField25, TField26, TField27, TField28, TField29, TField30>>
    where TField1 : IBqlSortColumn
    where TField2 : IBqlSortColumn
    where TField3 : IBqlSortColumn
    where TField4 : IBqlSortColumn
    where TField5 : IBqlSortColumn
    where TField6 : IBqlSortColumn
    where TField7 : IBqlSortColumn
    where TField8 : IBqlSortColumn
    where TField9 : IBqlSortColumn
    where TField10 : IBqlSortColumn
    where TField11 : IBqlSortColumn
    where TField12 : IBqlSortColumn
    where TField13 : IBqlSortColumn
    where TField14 : IBqlSortColumn
    where TField15 : IBqlSortColumn
    where TField16 : IBqlSortColumn
    where TField17 : IBqlSortColumn
    where TField18 : IBqlSortColumn
    where TField19 : IBqlSortColumn
    where TField20 : IBqlSortColumn
    where TField21 : IBqlSortColumn
    where TField22 : IBqlSortColumn
    where TField23 : IBqlSortColumn
    where TField24 : IBqlSortColumn
    where TField25 : IBqlSortColumn
    where TField26 : IBqlSortColumn
    where TField27 : IBqlSortColumn
    where TField28 : IBqlSortColumn
    where TField29 : IBqlSortColumn
    where TField30 : IBqlSortColumn
  {
  }

  /// <exclude />
  public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20, TField21, TField22, TField23, TField24, TField25, TField26, TField27, TField28, TField29, TField30, TField31> : 
    SelectFromBase<TTable, TJoins>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20, TField21, TField22, TField23, TField24, TField25, TField26, TField27, TField28, TField29, TField30, TField31>>
    where TField1 : IBqlSortColumn
    where TField2 : IBqlSortColumn
    where TField3 : IBqlSortColumn
    where TField4 : IBqlSortColumn
    where TField5 : IBqlSortColumn
    where TField6 : IBqlSortColumn
    where TField7 : IBqlSortColumn
    where TField8 : IBqlSortColumn
    where TField9 : IBqlSortColumn
    where TField10 : IBqlSortColumn
    where TField11 : IBqlSortColumn
    where TField12 : IBqlSortColumn
    where TField13 : IBqlSortColumn
    where TField14 : IBqlSortColumn
    where TField15 : IBqlSortColumn
    where TField16 : IBqlSortColumn
    where TField17 : IBqlSortColumn
    where TField18 : IBqlSortColumn
    where TField19 : IBqlSortColumn
    where TField20 : IBqlSortColumn
    where TField21 : IBqlSortColumn
    where TField22 : IBqlSortColumn
    where TField23 : IBqlSortColumn
    where TField24 : IBqlSortColumn
    where TField25 : IBqlSortColumn
    where TField26 : IBqlSortColumn
    where TField27 : IBqlSortColumn
    where TField28 : IBqlSortColumn
    where TField29 : IBqlSortColumn
    where TField30 : IBqlSortColumn
    where TField31 : IBqlSortColumn
  {
  }

  /// <exclude />
  public class OrderBy<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20, TField21, TField22, TField23, TField24, TField25, TField26, TField27, TField28, TField29, TField30, TField31, TField32> : 
    SelectFromBase<TTable, TJoins>.Order<By<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8, TField9, TField10, TField11, TField12, TField13, TField14, TField15, TField16, TField17, TField18, TField19, TField20, TField21, TField22, TField23, TField24, TField25, TField26, TField27, TField28, TField29, TField30, TField31, TField32>>
    where TField1 : IBqlSortColumn
    where TField2 : IBqlSortColumn
    where TField3 : IBqlSortColumn
    where TField4 : IBqlSortColumn
    where TField5 : IBqlSortColumn
    where TField6 : IBqlSortColumn
    where TField7 : IBqlSortColumn
    where TField8 : IBqlSortColumn
    where TField9 : IBqlSortColumn
    where TField10 : IBqlSortColumn
    where TField11 : IBqlSortColumn
    where TField12 : IBqlSortColumn
    where TField13 : IBqlSortColumn
    where TField14 : IBqlSortColumn
    where TField15 : IBqlSortColumn
    where TField16 : IBqlSortColumn
    where TField17 : IBqlSortColumn
    where TField18 : IBqlSortColumn
    where TField19 : IBqlSortColumn
    where TField20 : IBqlSortColumn
    where TField21 : IBqlSortColumn
    where TField22 : IBqlSortColumn
    where TField23 : IBqlSortColumn
    where TField24 : IBqlSortColumn
    where TField25 : IBqlSortColumn
    where TField26 : IBqlSortColumn
    where TField27 : IBqlSortColumn
    where TField28 : IBqlSortColumn
    where TField29 : IBqlSortColumn
    where TField30 : IBqlSortColumn
    where TField31 : IBqlSortColumn
    where TField32 : IBqlSortColumn
  {
  }

  public class Order<TSortColumnArray> : 
    FbqlSelect<SelectFromBase<TTable, TJoins>.Order<TSortColumnArray>, TTable, TJoins, BqlNone, TypeArrayOf<IBqlFunction>.Empty, BqlNone, TSortColumnArray>
    where TSortColumnArray : ITypeArrayOf<IBqlSortColumn>, TypeArray.IsNotEmpty
  {
  }
}
