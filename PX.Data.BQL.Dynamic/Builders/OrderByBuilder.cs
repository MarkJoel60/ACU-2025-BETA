// Decompiled with JetBrains decompiler
// Type: PX.Data.BQL.Dynamic.Builders.OrderByBuilder
// Assembly: PX.Data.BQL.Dynamic, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A48F85E4-C38C-40B3-9BFF-193A53C847AB
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.BQL.Dynamic.dll

using PX.Data.BQL.Dynamic.Statements;
using System;
using System.ComponentModel;

#nullable disable
namespace PX.Data.BQL.Dynamic.Builders;

[ImmutableObject(true)]
public class OrderByBuilder
{
  public OrderByStatement OrderBy(FieldSortStatement field, params FieldSortStatement[] fields)
  {
    return new OrderByStatement(field, fields);
  }

  public FieldSortStatement Asc<TField>() => this.Asc(typeof (TField));

  public FieldSortStatement Asc(Type field) => new FieldSortStatement(false, false, field);

  public FieldSortStatement Desc<TField>() => this.Desc(typeof (TField));

  public FieldSortStatement Desc(Type field) => new FieldSortStatement(false, true, field);
}
