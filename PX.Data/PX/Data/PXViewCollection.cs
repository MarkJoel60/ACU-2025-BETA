// Decompiled with JetBrains decompiler
// Type: PX.Data.PXViewCollection
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Web.Compilation;

#nullable disable
namespace PX.Data;

/// <exclude />
public class PXViewCollection : Dictionary<string, PXView>
{
  private PXGraph _Parent;
  private Dictionary<PXView, PXSelectBase> _Members = new Dictionary<PXView, PXSelectBase>();
  public readonly List<System.Type> Caches = new List<System.Type>();
  public readonly List<System.Type> RestorableCaches = new List<System.Type>();

  public PXViewCollection(PXGraph parent)
    : base((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase)
  {
    this._Parent = parent;
  }

  public PXViewCollection(PXGraph parent, int capacity)
    : base(capacity, (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase)
  {
    this._Parent = parent;
  }

  [PXInternalUseOnly]
  public bool TryGetOrCreateValue(string key, out PXView value)
  {
    if (this.ContainsKey(key))
    {
      value = base[key];
      return true;
    }
    if (key.StartsWith("_Cache#"))
    {
      try
      {
        string[] strArray = key.Split(new char[1]{ '_' }, StringSplitOptions.RemoveEmptyEntries);
        if (strArray.Length > 1)
        {
          System.Type type = PXBuildManager.GetType(strArray[0].Substring(6), false);
          if (type != (System.Type) null && this._Parent.Caches[type.Name] == null)
          {
            PXCache cach = this._Parent.Caches[type];
          }
          if (this.ContainsKey(key))
          {
            value = base[key];
            return true;
          }
        }
      }
      catch
      {
      }
    }
    value = (PXView) null;
    return false;
  }

  public new virtual PXView this[string key]
  {
    get
    {
      if (this._Parent.IsMobile)
        key = key == null || !key.Contains(":") ? key : StringExtensions.FirstSegment(key, ':').TrimEnd();
      PXView pxView;
      if (this.TryGetOrCreateValue(key, out pxView))
        return pxView;
      throw new PXViewDoesNotExistException("The view {0} doesn't exist.", new object[1]
      {
        (object) key
      });
    }
    set
    {
      PXView key1;
      if (this.TryGetValue(key, out key1) && key1 != null && !this._Members.ContainsKey(key1) && value != null && key1.GetType() == value.GetType())
        this._Parent.ViewNames.Remove(key1);
      base[key] = value;
      this._Parent.ViewNames[value] = key;
    }
  }

  public new virtual void Add(string key, PXView value) => this[key] = value;

  internal void Add(string key, PXSelectBase value)
  {
    this[key] = value.View;
    this._Members[value.View] = value;
  }

  internal PXSelectBase GetExternalMember(PXView view)
  {
    view = this._Parent.Views[this._Parent.ViewNames[view]];
    PXSelectBase externalMember;
    this._Members.TryGetValue(view, out externalMember);
    return externalMember;
  }
}
