// Decompiled with JetBrains decompiler
// Type: PX.Data.BQL.Fluent.UpdateBase`3
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
public class UpdateBase<TTable, TAssigns, TJoins> : 
  FbqlUpdateBase<TTable, TAssigns, TJoins, BqlNone, TypeArrayOf<IBqlFunction>.Empty, BqlNone>
  where TTable : class, IBqlTable, new()
  where TAssigns : ITypeArrayOf<IFbqlSet>
  where TJoins : ITypeArrayOf<IFbqlJoin>
{
  /// <exclude />
  public class AggregateTo<TFunction> : UpdateBase<TTable, TAssigns, TJoins>.Aggregate<To<TFunction>>
    where TFunction : IBqlFunction
  {
  }

  /// <exclude />
  public class AggregateTo<TFunction1, TFunction2> : 
    UpdateBase<TTable, TAssigns, TJoins>.Aggregate<To<TFunction1, TFunction2>>
    where TFunction1 : IBqlFunction
    where TFunction2 : IBqlFunction
  {
  }

  /// <exclude />
  public class AggregateTo<TFunction1, TFunction2, TFunction3> : 
    UpdateBase<TTable, TAssigns, TJoins>.Aggregate<To<TFunction1, TFunction2, TFunction3>>
    where TFunction1 : IBqlFunction
    where TFunction2 : IBqlFunction
    where TFunction3 : IBqlFunction
  {
  }

  /// <exclude />
  public class AggregateTo<TFunction1, TFunction2, TFunction3, TFunction4> : 
    UpdateBase<TTable, TAssigns, TJoins>.Aggregate<To<TFunction1, TFunction2, TFunction3, TFunction4>>
    where TFunction1 : IBqlFunction
    where TFunction2 : IBqlFunction
    where TFunction3 : IBqlFunction
    where TFunction4 : IBqlFunction
  {
  }

  /// <exclude />
  public class AggregateTo<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5> : 
    UpdateBase<TTable, TAssigns, TJoins>.Aggregate<To<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5>>
    where TFunction1 : IBqlFunction
    where TFunction2 : IBqlFunction
    where TFunction3 : IBqlFunction
    where TFunction4 : IBqlFunction
    where TFunction5 : IBqlFunction
  {
  }

  /// <exclude />
  public class AggregateTo<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6> : 
    UpdateBase<TTable, TAssigns, TJoins>.Aggregate<To<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6>>
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
    UpdateBase<TTable, TAssigns, TJoins>.Aggregate<To<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7>>
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
    UpdateBase<TTable, TAssigns, TJoins>.Aggregate<To<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8>>
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
    UpdateBase<TTable, TAssigns, TJoins>.Aggregate<To<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8, TFunction9>>
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
    UpdateBase<TTable, TAssigns, TJoins>.Aggregate<To<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8, TFunction9, TFunction10>>
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
    UpdateBase<TTable, TAssigns, TJoins>.Aggregate<To<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8, TFunction9, TFunction10, TFunction11>>
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
    UpdateBase<TTable, TAssigns, TJoins>.Aggregate<To<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8, TFunction9, TFunction10, TFunction11, TFunction12>>
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
    UpdateBase<TTable, TAssigns, TJoins>.Aggregate<To<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8, TFunction9, TFunction10, TFunction11, TFunction12, TFunction13>>
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
    UpdateBase<TTable, TAssigns, TJoins>.Aggregate<To<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8, TFunction9, TFunction10, TFunction11, TFunction12, TFunction13, TFunction14>>
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
    UpdateBase<TTable, TAssigns, TJoins>.Aggregate<To<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8, TFunction9, TFunction10, TFunction11, TFunction12, TFunction13, TFunction14, TFunction15>>
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
    UpdateBase<TTable, TAssigns, TJoins>.Aggregate<To<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8, TFunction9, TFunction10, TFunction11, TFunction12, TFunction13, TFunction14, TFunction15, TFunction16>>
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
    UpdateBase<TTable, TAssigns, TJoins>.Aggregate<To<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8, TFunction9, TFunction10, TFunction11, TFunction12, TFunction13, TFunction14, TFunction15, TFunction16, TFunction17>>
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
    UpdateBase<TTable, TAssigns, TJoins>.Aggregate<To<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8, TFunction9, TFunction10, TFunction11, TFunction12, TFunction13, TFunction14, TFunction15, TFunction16, TFunction17, TFunction18>>
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
    UpdateBase<TTable, TAssigns, TJoins>.Aggregate<To<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8, TFunction9, TFunction10, TFunction11, TFunction12, TFunction13, TFunction14, TFunction15, TFunction16, TFunction17, TFunction18, TFunction19>>
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
    UpdateBase<TTable, TAssigns, TJoins>.Aggregate<To<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8, TFunction9, TFunction10, TFunction11, TFunction12, TFunction13, TFunction14, TFunction15, TFunction16, TFunction17, TFunction18, TFunction19, TFunction20>>
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
    UpdateBase<TTable, TAssigns, TJoins>.Aggregate<To<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8, TFunction9, TFunction10, TFunction11, TFunction12, TFunction13, TFunction14, TFunction15, TFunction16, TFunction17, TFunction18, TFunction19, TFunction20, TFunction21>>
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
    UpdateBase<TTable, TAssigns, TJoins>.Aggregate<To<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8, TFunction9, TFunction10, TFunction11, TFunction12, TFunction13, TFunction14, TFunction15, TFunction16, TFunction17, TFunction18, TFunction19, TFunction20, TFunction21, TFunction22>>
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
    UpdateBase<TTable, TAssigns, TJoins>.Aggregate<To<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8, TFunction9, TFunction10, TFunction11, TFunction12, TFunction13, TFunction14, TFunction15, TFunction16, TFunction17, TFunction18, TFunction19, TFunction20, TFunction21, TFunction22, TFunction23>>
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
    UpdateBase<TTable, TAssigns, TJoins>.Aggregate<To<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8, TFunction9, TFunction10, TFunction11, TFunction12, TFunction13, TFunction14, TFunction15, TFunction16, TFunction17, TFunction18, TFunction19, TFunction20, TFunction21, TFunction22, TFunction23, TFunction24>>
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
    UpdateBase<TTable, TAssigns, TJoins>.Aggregate<To<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8, TFunction9, TFunction10, TFunction11, TFunction12, TFunction13, TFunction14, TFunction15, TFunction16, TFunction17, TFunction18, TFunction19, TFunction20, TFunction21, TFunction22, TFunction23, TFunction24, TFunction25>>
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
    UpdateBase<TTable, TAssigns, TJoins>.Aggregate<To<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8, TFunction9, TFunction10, TFunction11, TFunction12, TFunction13, TFunction14, TFunction15, TFunction16, TFunction17, TFunction18, TFunction19, TFunction20, TFunction21, TFunction22, TFunction23, TFunction24, TFunction25, TFunction26>>
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
    UpdateBase<TTable, TAssigns, TJoins>.Aggregate<To<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8, TFunction9, TFunction10, TFunction11, TFunction12, TFunction13, TFunction14, TFunction15, TFunction16, TFunction17, TFunction18, TFunction19, TFunction20, TFunction21, TFunction22, TFunction23, TFunction24, TFunction25, TFunction26, TFunction27>>
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
    UpdateBase<TTable, TAssigns, TJoins>.Aggregate<To<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8, TFunction9, TFunction10, TFunction11, TFunction12, TFunction13, TFunction14, TFunction15, TFunction16, TFunction17, TFunction18, TFunction19, TFunction20, TFunction21, TFunction22, TFunction23, TFunction24, TFunction25, TFunction26, TFunction27, TFunction28>>
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
    UpdateBase<TTable, TAssigns, TJoins>.Aggregate<To<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8, TFunction9, TFunction10, TFunction11, TFunction12, TFunction13, TFunction14, TFunction15, TFunction16, TFunction17, TFunction18, TFunction19, TFunction20, TFunction21, TFunction22, TFunction23, TFunction24, TFunction25, TFunction26, TFunction27, TFunction28, TFunction29>>
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
    UpdateBase<TTable, TAssigns, TJoins>.Aggregate<To<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8, TFunction9, TFunction10, TFunction11, TFunction12, TFunction13, TFunction14, TFunction15, TFunction16, TFunction17, TFunction18, TFunction19, TFunction20, TFunction21, TFunction22, TFunction23, TFunction24, TFunction25, TFunction26, TFunction27, TFunction28, TFunction29, TFunction30>>
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
    UpdateBase<TTable, TAssigns, TJoins>.Aggregate<To<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8, TFunction9, TFunction10, TFunction11, TFunction12, TFunction13, TFunction14, TFunction15, TFunction16, TFunction17, TFunction18, TFunction19, TFunction20, TFunction21, TFunction22, TFunction23, TFunction24, TFunction25, TFunction26, TFunction27, TFunction28, TFunction29, TFunction30, TFunction31>>
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
    UpdateBase<TTable, TAssigns, TJoins>.Aggregate<To<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8, TFunction9, TFunction10, TFunction11, TFunction12, TFunction13, TFunction14, TFunction15, TFunction16, TFunction17, TFunction18, TFunction19, TFunction20, TFunction21, TFunction22, TFunction23, TFunction24, TFunction25, TFunction26, TFunction27, TFunction28, TFunction29, TFunction30, TFunction31, TFunction32>>
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
    FbqlUpdateBase<TTable, TAssigns, TJoins, TCondition, TypeArrayOf<IBqlFunction>.Empty, BqlNone>
    where TCondition : IBqlUnary, new()
  {
    /// <exclude />
    public class AggregateTo<TFunction> : 
      UpdateBase<TTable, TAssigns, TJoins>.Where<TCondition>.Aggregate<To<TFunction>>
      where TFunction : IBqlFunction
    {
    }

    /// <exclude />
    public class AggregateTo<TFunction1, TFunction2> : 
      UpdateBase<TTable, TAssigns, TJoins>.Where<TCondition>.Aggregate<To<TFunction1, TFunction2>>
      where TFunction1 : IBqlFunction
      where TFunction2 : IBqlFunction
    {
    }

    /// <exclude />
    public class AggregateTo<TFunction1, TFunction2, TFunction3> : 
      UpdateBase<TTable, TAssigns, TJoins>.Where<TCondition>.Aggregate<To<TFunction1, TFunction2, TFunction3>>
      where TFunction1 : IBqlFunction
      where TFunction2 : IBqlFunction
      where TFunction3 : IBqlFunction
    {
    }

    /// <exclude />
    public class AggregateTo<TFunction1, TFunction2, TFunction3, TFunction4> : 
      UpdateBase<TTable, TAssigns, TJoins>.Where<TCondition>.Aggregate<To<TFunction1, TFunction2, TFunction3, TFunction4>>
      where TFunction1 : IBqlFunction
      where TFunction2 : IBqlFunction
      where TFunction3 : IBqlFunction
      where TFunction4 : IBqlFunction
    {
    }

    /// <exclude />
    public class AggregateTo<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5> : 
      UpdateBase<TTable, TAssigns, TJoins>.Where<TCondition>.Aggregate<To<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5>>
      where TFunction1 : IBqlFunction
      where TFunction2 : IBqlFunction
      where TFunction3 : IBqlFunction
      where TFunction4 : IBqlFunction
      where TFunction5 : IBqlFunction
    {
    }

    /// <exclude />
    public class AggregateTo<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6> : 
      UpdateBase<TTable, TAssigns, TJoins>.Where<TCondition>.Aggregate<To<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6>>
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
      UpdateBase<TTable, TAssigns, TJoins>.Where<TCondition>.Aggregate<To<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7>>
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
      UpdateBase<TTable, TAssigns, TJoins>.Where<TCondition>.Aggregate<To<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8>>
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
      UpdateBase<TTable, TAssigns, TJoins>.Where<TCondition>.Aggregate<To<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8, TFunction9>>
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
      UpdateBase<TTable, TAssigns, TJoins>.Where<TCondition>.Aggregate<To<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8, TFunction9, TFunction10>>
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
      UpdateBase<TTable, TAssigns, TJoins>.Where<TCondition>.Aggregate<To<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8, TFunction9, TFunction10, TFunction11>>
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
      UpdateBase<TTable, TAssigns, TJoins>.Where<TCondition>.Aggregate<To<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8, TFunction9, TFunction10, TFunction11, TFunction12>>
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
      UpdateBase<TTable, TAssigns, TJoins>.Where<TCondition>.Aggregate<To<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8, TFunction9, TFunction10, TFunction11, TFunction12, TFunction13>>
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
      UpdateBase<TTable, TAssigns, TJoins>.Where<TCondition>.Aggregate<To<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8, TFunction9, TFunction10, TFunction11, TFunction12, TFunction13, TFunction14>>
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
      UpdateBase<TTable, TAssigns, TJoins>.Where<TCondition>.Aggregate<To<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8, TFunction9, TFunction10, TFunction11, TFunction12, TFunction13, TFunction14, TFunction15>>
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
      UpdateBase<TTable, TAssigns, TJoins>.Where<TCondition>.Aggregate<To<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8, TFunction9, TFunction10, TFunction11, TFunction12, TFunction13, TFunction14, TFunction15, TFunction16>>
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
      UpdateBase<TTable, TAssigns, TJoins>.Where<TCondition>.Aggregate<To<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8, TFunction9, TFunction10, TFunction11, TFunction12, TFunction13, TFunction14, TFunction15, TFunction16, TFunction17>>
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
      UpdateBase<TTable, TAssigns, TJoins>.Where<TCondition>.Aggregate<To<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8, TFunction9, TFunction10, TFunction11, TFunction12, TFunction13, TFunction14, TFunction15, TFunction16, TFunction17, TFunction18>>
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
      UpdateBase<TTable, TAssigns, TJoins>.Where<TCondition>.Aggregate<To<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8, TFunction9, TFunction10, TFunction11, TFunction12, TFunction13, TFunction14, TFunction15, TFunction16, TFunction17, TFunction18, TFunction19>>
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
      UpdateBase<TTable, TAssigns, TJoins>.Where<TCondition>.Aggregate<To<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8, TFunction9, TFunction10, TFunction11, TFunction12, TFunction13, TFunction14, TFunction15, TFunction16, TFunction17, TFunction18, TFunction19, TFunction20>>
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
      UpdateBase<TTable, TAssigns, TJoins>.Where<TCondition>.Aggregate<To<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8, TFunction9, TFunction10, TFunction11, TFunction12, TFunction13, TFunction14, TFunction15, TFunction16, TFunction17, TFunction18, TFunction19, TFunction20, TFunction21>>
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
      UpdateBase<TTable, TAssigns, TJoins>.Where<TCondition>.Aggregate<To<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8, TFunction9, TFunction10, TFunction11, TFunction12, TFunction13, TFunction14, TFunction15, TFunction16, TFunction17, TFunction18, TFunction19, TFunction20, TFunction21, TFunction22>>
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
      UpdateBase<TTable, TAssigns, TJoins>.Where<TCondition>.Aggregate<To<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8, TFunction9, TFunction10, TFunction11, TFunction12, TFunction13, TFunction14, TFunction15, TFunction16, TFunction17, TFunction18, TFunction19, TFunction20, TFunction21, TFunction22, TFunction23>>
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
      UpdateBase<TTable, TAssigns, TJoins>.Where<TCondition>.Aggregate<To<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8, TFunction9, TFunction10, TFunction11, TFunction12, TFunction13, TFunction14, TFunction15, TFunction16, TFunction17, TFunction18, TFunction19, TFunction20, TFunction21, TFunction22, TFunction23, TFunction24>>
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
      UpdateBase<TTable, TAssigns, TJoins>.Where<TCondition>.Aggregate<To<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8, TFunction9, TFunction10, TFunction11, TFunction12, TFunction13, TFunction14, TFunction15, TFunction16, TFunction17, TFunction18, TFunction19, TFunction20, TFunction21, TFunction22, TFunction23, TFunction24, TFunction25>>
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
      UpdateBase<TTable, TAssigns, TJoins>.Where<TCondition>.Aggregate<To<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8, TFunction9, TFunction10, TFunction11, TFunction12, TFunction13, TFunction14, TFunction15, TFunction16, TFunction17, TFunction18, TFunction19, TFunction20, TFunction21, TFunction22, TFunction23, TFunction24, TFunction25, TFunction26>>
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
      UpdateBase<TTable, TAssigns, TJoins>.Where<TCondition>.Aggregate<To<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8, TFunction9, TFunction10, TFunction11, TFunction12, TFunction13, TFunction14, TFunction15, TFunction16, TFunction17, TFunction18, TFunction19, TFunction20, TFunction21, TFunction22, TFunction23, TFunction24, TFunction25, TFunction26, TFunction27>>
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
      UpdateBase<TTable, TAssigns, TJoins>.Where<TCondition>.Aggregate<To<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8, TFunction9, TFunction10, TFunction11, TFunction12, TFunction13, TFunction14, TFunction15, TFunction16, TFunction17, TFunction18, TFunction19, TFunction20, TFunction21, TFunction22, TFunction23, TFunction24, TFunction25, TFunction26, TFunction27, TFunction28>>
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
      UpdateBase<TTable, TAssigns, TJoins>.Where<TCondition>.Aggregate<To<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8, TFunction9, TFunction10, TFunction11, TFunction12, TFunction13, TFunction14, TFunction15, TFunction16, TFunction17, TFunction18, TFunction19, TFunction20, TFunction21, TFunction22, TFunction23, TFunction24, TFunction25, TFunction26, TFunction27, TFunction28, TFunction29>>
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
      UpdateBase<TTable, TAssigns, TJoins>.Where<TCondition>.Aggregate<To<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8, TFunction9, TFunction10, TFunction11, TFunction12, TFunction13, TFunction14, TFunction15, TFunction16, TFunction17, TFunction18, TFunction19, TFunction20, TFunction21, TFunction22, TFunction23, TFunction24, TFunction25, TFunction26, TFunction27, TFunction28, TFunction29, TFunction30>>
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
      UpdateBase<TTable, TAssigns, TJoins>.Where<TCondition>.Aggregate<To<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8, TFunction9, TFunction10, TFunction11, TFunction12, TFunction13, TFunction14, TFunction15, TFunction16, TFunction17, TFunction18, TFunction19, TFunction20, TFunction21, TFunction22, TFunction23, TFunction24, TFunction25, TFunction26, TFunction27, TFunction28, TFunction29, TFunction30, TFunction31>>
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
      UpdateBase<TTable, TAssigns, TJoins>.Where<TCondition>.Aggregate<To<TFunction1, TFunction2, TFunction3, TFunction4, TFunction5, TFunction6, TFunction7, TFunction8, TFunction9, TFunction10, TFunction11, TFunction12, TFunction13, TFunction14, TFunction15, TFunction16, TFunction17, TFunction18, TFunction19, TFunction20, TFunction21, TFunction22, TFunction23, TFunction24, TFunction25, TFunction26, TFunction27, TFunction28, TFunction29, TFunction30, TFunction31, TFunction32>>
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
      FbqlUpdateBase<TTable, TAssigns, TJoins, TCondition, TFunctionArray, BqlNone>
      where TFunctionArray : ITypeArrayOf<IBqlFunction>, TypeArray.IsNotEmpty
    {
      public class Having<TGroupCondition> : 
        FbqlUpdateBase<TTable, TAssigns, TJoins, TCondition, TFunctionArray, Having<TGroupCondition>>
        where TGroupCondition : IBqlHavingCondition, new()
      {
      }
    }
  }

  public class Aggregate<TFunctionArray> : 
    FbqlUpdateBase<TTable, TAssigns, TJoins, BqlNone, TFunctionArray, BqlNone>
    where TFunctionArray : ITypeArrayOf<IBqlFunction>, TypeArray.IsNotEmpty
  {
    public class Having<TGroupCondition> : 
      FbqlUpdateBase<TTable, TAssigns, TJoins, BqlNone, TFunctionArray, Having<TGroupCondition>>
      where TGroupCondition : IBqlHavingCondition, new()
    {
    }
  }
}
