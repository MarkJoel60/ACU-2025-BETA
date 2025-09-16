// Decompiled with JetBrains decompiler
// Type: PX.Data.IBqlFieldMappingResolver
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.SQLTree;
using System.Collections.Generic;

#nullable disable
namespace PX.Data;

/// <summary>The interface that reads the rules of the relationship between the fields of two DAC classes, but does not allows these rules to be modified.</summary>
public interface IBqlFieldMappingResolver
{
  Dictionary<string, SQLExpression> GetMapping(PXGraph graph);

  System.Type MappedFrom { get; }

  System.Type MappedTo { get; }
}
