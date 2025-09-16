// Decompiled with JetBrains decompiler
// Type: PX.Data.BQL.Fluent.FbqlUpdateBase`6
// Assembly: PX.Data.BQL.Fluent, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1086111-88AF-4F2E-BE39-D2C71848C2C0
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.BQL.Fluent.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.BQL.Fluent.xml

using PX.Common;
using PX.Data.SQLTree;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data.BQL.Fluent;

/// <exclude />
public abstract class FbqlUpdateBase<TTable, TAssigns, TJoinArray, TCondition, TFunctionArray, TGroupCondition>
  where TTable : class, IBqlTable, new()
  where TAssigns : ITypeArrayOf<IFbqlSet>
  where TJoinArray : ITypeArrayOf<IFbqlJoin>
  where TCondition : IBqlUnary, new()
  where TFunctionArray : ITypeArrayOf<IBqlFunction>
  where TGroupCondition : IBqlHaving, new()
{
  /// <summary>Execute the UPDATE statement.</summary>
  /// <returns>The number of updated rows</returns>
  public static int Update(PXGraph graph, params object[] parameters)
  {
    IBqlUpdate command = FbqlUpdateBase<TTable, TAssigns, TJoinArray, TCondition, TFunctionArray, TGroupCondition>.GetCommand();
    return PXDatabase.Update(graph, command, PXUpdate.prepareParameters(graph, command, parameters));
  }

  /// <summary>
  /// Provides an instance of <see cref="T:PX.Data.IBqlUpdate" /> that corresponds to the current update statement.
  /// </summary>
  public static IBqlUpdate GetCommand()
  {
    return (IBqlUpdate) new PX.Data.Update<FbqlUpdateBase<TTable, TAssigns, TJoinArray, TCondition, TFunctionArray, TGroupCondition>.ChainedSet<TAssigns>, FbqlUpdateBase<TTable, TAssigns, TJoinArray, TCondition, TFunctionArray, TGroupCondition>.Select>();
  }

  private class ChainedSet<TSetArray> : IFbqlSet, IBqlSet where TSetArray : ITypeArrayOf<IFbqlSet>
  {
    public bool AppendExpression(
      PXGraph graph,
      BqlCommandInfo info,
      List<KeyValuePair<SQLExpression, SQLExpression>> assignments)
    {
      IFbqlSet[] instances = TypeArrayOf<IFbqlSet>.CheckAndExtractInstances(typeof (TSetArray), (string) null);
      bool flag = true;
      foreach (IFbqlSet ifbqlSet in instances)
        flag &= ((IBqlSet) ifbqlSet).AppendExpression(graph, info, assignments);
      return flag;
    }

    public Type GetFieldType()
    {
      return ((IBqlSet) ((IEnumerable<IFbqlSet>) TypeArrayOf<IFbqlSet>.CheckAndExtractInstances(typeof (TSetArray), (string) null)).First<IFbqlSet>()).GetFieldType();
    }
  }

  private class Select : 
    FbqlSelect<FbqlUpdateBase<TTable, TAssigns, TJoinArray, TCondition, TFunctionArray, TGroupCondition>.Select, TTable, TJoinArray, TCondition, TFunctionArray, TGroupCondition, TypeArrayOf<IBqlSortColumn>.Empty>
  {
  }
}
