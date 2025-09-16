// Decompiled with JetBrains decompiler
// Type: PX.Data.Api.Services.ViewDataService
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Data.Api.Services;

public class ViewDataService
{
  public const string GIDesignKey = "DesignID";
  private static readonly string SESSION_KEY = typeof (ViewDataService.GraphStateByScreen).FullName;
  private Dictionary<string, ViewDataService.GraphStateByScreen> _states;

  private ViewDataService()
  {
    if (PXContext.SessionTyped<ViewDataService.SessionState>().GraphByScreen == null)
    {
      this._states = new Dictionary<string, ViewDataService.GraphStateByScreen>();
      PXContext.SessionTyped<ViewDataService.SessionState>().GraphByScreen = this._states;
    }
    else
      this._states = PXContext.SessionTyped<ViewDataService.SessionState>().GraphByScreen;
  }

  public static ViewDataService Instance => new ViewDataService();

  public void SetCache(string screenId, string key, string value)
  {
    this.GetCache(screenId).Data.SetData(key, value);
  }

  public void SetFilterCache(string screenId, string key, string value)
  {
    this.GetCache(screenId).FilterData.SetData(key, value);
  }

  public void SetInitFilterCache(string screenId, string key, string value)
  {
    this.GetCache(screenId).InitFilterData.SetData(key, value);
  }

  public string GetCache(string screenId, string key)
  {
    return this.GetCache(screenId, false)?.Data?.GetFromData(key);
  }

  public Dictionary<string, string> GetFilterCache(string screenId)
  {
    return this.GetCache(screenId, false)?.FilterData?.Data ?? new Dictionary<string, string>();
  }

  public Dictionary<string, string> GetInitFilterCache(string screenId)
  {
    return this.GetCache(screenId, false)?.InitFilterData?.Data ?? new Dictionary<string, string>();
  }

  public void ClearFilter(string screenId)
  {
    ViewDataService.GraphStateByScreen cache = this.GetCache(screenId, false);
    cache?.FilterData?.Clear();
    cache?.InitFilterData?.Clear();
  }

  public Dictionary<string, string> GetFilterKeys(string screenId)
  {
    return this.GetCache(screenId, false)?.FilterData?.Data;
  }

  private ViewDataService.GraphStateByScreen GetCache(string screenId, bool initNew = true)
  {
    if (this._states.ContainsKey(screenId))
      return this._states[screenId];
    if (!initNew)
      return (ViewDataService.GraphStateByScreen) null;
    ViewDataService.GraphStateByScreen graphStateByScreen = new ViewDataService.GraphStateByScreen();
    this._states.Add(screenId, graphStateByScreen);
    return this._states[screenId];
  }

  public void Clear(string screenId)
  {
    if (!this._states.ContainsKey(screenId))
      return;
    this._states.Remove(screenId);
  }

  private class SessionState : PXSessionState
  {
    public Dictionary<string, ViewDataService.GraphStateByScreen> GraphByScreen
    {
      get
      {
        return this[ViewDataService.SESSION_KEY] as Dictionary<string, ViewDataService.GraphStateByScreen>;
      }
      set => this[ViewDataService.SESSION_KEY] = (object) value;
    }
  }

  private class GraphStateByScreen
  {
    public ViewDataService.CachedData Data = new ViewDataService.CachedData();
    public ViewDataService.CachedData FilterData = new ViewDataService.CachedData();
    public ViewDataService.CachedData InitFilterData = new ViewDataService.CachedData();
  }

  private class CachedData
  {
    public Dictionary<string, string> Data = new Dictionary<string, string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);

    public void Clear() => this.Data.Clear();

    public void SetData(string key, string value)
    {
      if (this.Data.ContainsKey(key))
      {
        if (!(this.Data[key] != value))
          return;
        this.Data[key] = value;
      }
      else
        this.Data.Add(key, value);
    }

    public string GetFromData(string key)
    {
      return !this.Data.ContainsKey(key) ? (string) null : this.Data[key];
    }
  }
}
