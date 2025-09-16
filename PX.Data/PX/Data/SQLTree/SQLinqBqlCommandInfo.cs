// Decompiled with JetBrains decompiler
// Type: PX.Data.SQLTree.SQLinqBqlCommandInfo
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Collections.Generic;

#nullable disable
namespace PX.Data.SQLTree;

internal class SQLinqBqlCommandInfo : BqlCommandInfo
{
  public SimpleTable CurrTable { get; set; }

  public Dictionary<System.Type, (string alias, bool isExternal)> CurrAliases { get; set; }

  public SQLinqBqlCommandInfo(bool initialize = true)
    : base(initialize)
  {
    this.Pars = new List<PXDataValue>();
    this.Arguments = new List<object>();
  }

  public List<PXDataValue> Pars { get; set; }

  public List<object> Arguments { get; set; }

  public BqlCommand BaseCommand { get; set; }

  public SQLinqBqlCommandInfo ParentInfo { get; set; }
}
