// Decompiled with JetBrains decompiler
// Type: PX.Data.BQL.Fluent.FbqlSelect`7
// Assembly: PX.Data.BQL.Fluent, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1086111-88AF-4F2E-BE39-D2C71848C2C0
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.BQL.Fluent.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.BQL.Fluent.xml

using PX.Common;
using System;

#nullable disable
namespace PX.Data.BQL.Fluent;

public abstract class FbqlSelect<TSelf, TTable, TJoinArray, TCondition, TFunctionArray, TGroupCondition, TSortColumnArray> : 
  FbqlSelect<TSelf, TTable>
  where TSelf : FbqlSelect<TSelf, TTable, TJoinArray, TCondition, TFunctionArray, TGroupCondition, TSortColumnArray>, new()
  where TTable : class, IBqlTable, new()
  where TJoinArray : ITypeArrayOf<IFbqlJoin>
  where TCondition : IBqlUnary, new()
  where TFunctionArray : ITypeArrayOf<IBqlFunction>
  where TGroupCondition : IBqlHaving, new()
  where TSortColumnArray : ITypeArrayOf<IBqlSortColumn>
{
  protected FbqlSelect()
    : base(FbqlSelect<TSelf, TTable, TJoinArray, TCondition, TFunctionArray, TGroupCondition, TSortColumnArray>.ParseSelf())
  {
  }

  private static FbqlParseResult ParseSelf()
  {
    return FbqlCommand.FbqlToBqlMap.GetOrAdd(typeof (TSelf), (Func<Type, FbqlParseResult>) (t => FbqlCommandParser.CreateBqlCommand(t.GetGenericTypeDefinition(), typeof (TTable), typeof (TJoinArray), typeof (TCondition), typeof (TFunctionArray), typeof (TGroupCondition), typeof (TSortColumnArray))));
  }
}
