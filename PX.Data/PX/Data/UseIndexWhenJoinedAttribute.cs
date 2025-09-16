// Decompiled with JetBrains decompiler
// Type: PX.Data.UseIndexWhenJoinedAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Data;

/// <exclude />
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class UseIndexWhenJoinedAttribute : Attribute
{
  public readonly string IndexName;

  public UseIndexWhenJoinedAttribute(string indexName) => this.IndexName = indexName;

  public System.Type On { get; set; }

  public IBqlQueryHintTableOptions BuildTableOptions()
  {
    return (IBqlQueryHintTableOptions) new QueryHintUseIndexes((IEnumerable<string>) new string[1]
    {
      this.IndexName
    });
  }
}
