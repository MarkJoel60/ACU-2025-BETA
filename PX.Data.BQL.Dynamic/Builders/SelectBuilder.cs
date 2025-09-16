// Decompiled with JetBrains decompiler
// Type: PX.Data.BQL.Dynamic.Builders.SelectBuilder
// Assembly: PX.Data.BQL.Dynamic, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A48F85E4-C38C-40B3-9BFF-193A53C847AB
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.BQL.Dynamic.dll

using PX.Data.BQL.Dynamic.Statements;
using System;
using System.ComponentModel;

#nullable disable
namespace PX.Data.BQL.Dynamic.Builders;

[ImmutableObject(true)]
public class SelectBuilder
{
  public SelectStatement SelectFrom<TTable>() where TTable : IBqlTable
  {
    return this.SelectFrom(typeof (TTable));
  }

  public SelectStatement SelectFrom(Type table) => new SelectStatement(table, false);

  public SelectStatement SearchFor<TField>() where TField : IBqlField
  {
    return this.SearchFor(typeof (TField));
  }

  public SelectStatement SearchFor(Type field) => new SelectStatement(field, true);
}
