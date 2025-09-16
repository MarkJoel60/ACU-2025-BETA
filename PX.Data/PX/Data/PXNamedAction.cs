// Decompiled with JetBrains decompiler
// Type: PX.Data.PXNamedAction
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data;

/// <exclude />
public static class PXNamedAction
{
  public static PXAction AddAction(
    PXGraph graph,
    System.Type primaryType,
    string name,
    string displayName,
    string fieldClass,
    PXButtonDelegate handler)
  {
    return PXNamedAction.AddAction(graph, primaryType, name, displayName, fieldClass, true, handler);
  }

  public static PXAction AddHiddenAction(
    PXGraph graph,
    System.Type primaryType,
    string name,
    string displayname,
    string fieldClass,
    PXButtonDelegate handler)
  {
    return PXNamedAction.AddAction(graph, primaryType, name, displayname, fieldClass, false, handler);
  }

  public static PXAction AddAction(
    PXGraph graph,
    System.Type primaryType,
    string name,
    string displayName,
    PXButtonDelegate handler)
  {
    return PXNamedAction.AddAction(graph, primaryType, name, displayName, (string) null, true, handler);
  }

  public static PXAction AddHiddenAction(
    PXGraph graph,
    System.Type primaryType,
    string name,
    PXButtonDelegate handler)
  {
    return PXNamedAction.AddAction(graph, primaryType, name, string.Empty, (string) null, false, handler);
  }

  public static PXAction AddHiddenAction(
    PXGraph graph,
    System.Type primaryType,
    string name,
    string displayname,
    PXButtonDelegate handler)
  {
    return PXNamedAction.AddAction(graph, primaryType, name, displayname, (string) null, false, handler);
  }

  public static PXAction AddAction(
    PXGraph graph,
    System.Type primaryType,
    string name,
    string displayName,
    PXButtonDelegate handler,
    params PXEventSubscriberAttribute[] additionalAttributes)
  {
    return PXNamedAction.AddAction(graph, primaryType, name, displayName, (string) null, true, handler, additionalAttributes);
  }

  public static PXAction AddAction(
    PXGraph graph,
    System.Type primaryType,
    string name,
    string displayName,
    bool visible,
    PXButtonDelegate handler)
  {
    return PXNamedAction.AddAction(graph, primaryType, name, displayName, (string) null, visible, handler);
  }

  public static PXAction AddAction(
    PXGraph graph,
    System.Type primaryType,
    string name,
    string displayName,
    string fieldClass,
    bool visible,
    PXButtonDelegate handler,
    params PXEventSubscriberAttribute[] additionalAttributes)
  {
    if (graph == null)
      throw new ArgumentNullException(nameof (graph));
    if (primaryType == (System.Type) null)
      throw new ArgumentNullException(nameof (primaryType));
    if (string.IsNullOrEmpty(name))
      throw new ArgumentNullException(nameof (name));
    if (string.IsNullOrEmpty(displayName))
      displayName = name;
    if (handler == null)
      handler = (PXButtonDelegate) (adapter => adapter.Get());
    bool flag = false;
    foreach (PXUIFieldAttribute pxuiFieldAttribute in additionalAttributes.OfType<PXUIFieldAttribute>())
    {
      flag = true;
      pxuiFieldAttribute.DisplayName = pxuiFieldAttribute.DisplayName ?? displayName;
      pxuiFieldAttribute.FieldClass = string.IsNullOrEmpty(pxuiFieldAttribute.FieldClass) ? fieldClass : pxuiFieldAttribute.FieldClass;
      pxuiFieldAttribute.Visible = visible;
    }
    if (!flag)
      additionalAttributes = ((IEnumerable<PXEventSubscriberAttribute>) additionalAttributes).Concat<PXEventSubscriberAttribute>((IEnumerable<PXEventSubscriberAttribute>) new PXUIFieldAttribute[1]
      {
        new PXUIFieldAttribute()
        {
          DisplayName = PXMessages.LocalizeNoPrefix(displayName),
          FieldClass = fieldClass,
          MapEnableRights = PXCacheRights.Select,
          Visible = visible
        }
      }).ToArray<PXEventSubscriberAttribute>();
    PXAction instance = (PXAction) Activator.CreateInstance(typeof (PXNamedAction<>).MakeGenericType(primaryType), (object) graph, (object) name, (object) handler, (object) additionalAttributes);
    graph.Actions[name] = instance;
    return instance;
  }
}
