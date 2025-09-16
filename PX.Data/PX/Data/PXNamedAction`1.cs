// Decompiled with JetBrains decompiler
// Type: PX.Data.PXNamedAction`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data;

/// <exclude />
public class PXNamedAction<TNode> : PXAction<TNode> where TNode : class, IBqlTable, new()
{
  public PXNamedAction(PXGraph graph, string name, PXButtonDelegate handler)
    : base(graph)
  {
    this._Handler = handler != null ? (Delegate) handler : throw new PXArgumentException(nameof (handler), "The argument cannot be null.");
    this.SetHandler((Delegate) handler, name);
  }

  public PXNamedAction(
    PXGraph graph,
    string name,
    PXButtonDelegate handler,
    params PXEventSubscriberAttribute[] additionalAttributes)
    : this(graph, name, handler)
  {
    if (additionalAttributes == null)
      return;
    this.AppendAttributes(name, (object[]) additionalAttributes);
  }

  public static PXAction<TNode> AddAction(
    PXGraph graph,
    string name,
    string displayName,
    PXButtonDelegate handler)
  {
    return PXNamedAction.AddAction(graph, typeof (TNode), name, displayName, handler) as PXAction<TNode>;
  }

  public static PXAction<TNode> AddAction(
    PXGraph graph,
    string name,
    string displayName,
    PXButtonDelegate handler,
    params PXEventSubscriberAttribute[] additionalAttributes)
  {
    return PXNamedAction.AddAction(graph, typeof (TNode), name, displayName, handler, additionalAttributes) as PXAction<TNode>;
  }
}
