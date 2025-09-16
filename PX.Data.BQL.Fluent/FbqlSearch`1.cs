// Decompiled with JetBrains decompiler
// Type: PX.Data.BQL.Fluent.FbqlSearch`1
// Assembly: PX.Data.BQL.Fluent, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1086111-88AF-4F2E-BE39-D2C71848C2C0
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.BQL.Fluent.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.BQL.Fluent.xml

using PX.Data.SQLTree;
using System;

#nullable disable
namespace PX.Data.BQL.Fluent;

/// <summary>Provides classes for fluent BQL search commands.</summary>
/// <typeparam name="TField">The search field.</typeparam>
public abstract class FbqlSearch<TField>(BqlCommand bqlCommand) : FbqlCommand(bqlCommand), IBqlSearch
  where TField : IBqlField
{
  private IBqlSearch BqlSearch => (IBqlSearch) this.InnerBqlCommand;

  public Type GetField() => this.BqlSearch.GetField();

  public string GetFieldName(PXGraph graph) => this.BqlSearch.GetFieldName(graph);

  public SQLExpression GetFieldExpression(PXGraph graph)
  {
    return this.BqlSearch.GetFieldExpression(graph);
  }

  public SQLExpression GetWhereExpression(PXGraph graph)
  {
    return this.BqlSearch.GetWhereExpression(graph);
  }
}
