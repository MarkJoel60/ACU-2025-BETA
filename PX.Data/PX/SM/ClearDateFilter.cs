// Decompiled with JetBrains decompiler
// Type: PX.SM.ClearDateFilter
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.SM;

[Serializable]
public class ClearDateFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected System.DateTime? _Till;

  [PXDBDate]
  [PXUIField(DisplayName = "Clear before date")]
  public virtual System.DateTime? Till
  {
    get => this._Till;
    set => this._Till = value;
  }

  public abstract class till : BqlType<IBqlDateTime, System.DateTime>.Field<
  #nullable disable
  ClearDateFilter.till>
  {
  }
}
