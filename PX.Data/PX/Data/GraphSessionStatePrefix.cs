// Decompiled with JetBrains decompiler
// Type: PX.Data.GraphSessionStatePrefix
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable enable
namespace PX.Data;

internal sealed class GraphSessionStatePrefix
{
  private const string EmptySessionPrefix = "";

  private GraphSessionStatePrefix(string statePrefix, System.Type type)
  {
    this.Value = statePrefix + type.FullName;
    this.SubKeyPrefix = GraphSessionStateSubKey.CreatePrefix(this);
    this.GraphInfoKey = this.Value;
  }

  private GraphSessionStatePrefix(string statePrefix, PXGraph graph)
    : this(statePrefix, graph.GetType())
  {
  }

  internal static GraphSessionStatePrefix Create(string statePrefix, System.Type type)
  {
    return new GraphSessionStatePrefix(statePrefix, type);
  }

  internal static GraphSessionStatePrefix WithoutStatePrefix(System.Type type)
  {
    return new GraphSessionStatePrefix("", type);
  }

  internal static GraphSessionStatePrefix For(PXGraph graph)
  {
    return new GraphSessionStatePrefix(graph.StatePrefix, graph);
  }

  internal static GraphSessionStatePrefix WithoutStatePrefixFor(PXGraph graph)
  {
    return new GraphSessionStatePrefix("", graph);
  }

  internal string Value { get; }

  internal string SubKeyPrefix { get; }

  internal string GraphInfoKey { get; }

  internal string GetSubKey(string key) => GraphSessionStateSubKey.Create(this, key);

  internal bool IsSubKey(string key) => GraphSessionStateSubKey.IsSubKey(this, key);
}
