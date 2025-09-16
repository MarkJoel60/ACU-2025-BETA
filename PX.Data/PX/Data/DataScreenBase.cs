// Decompiled with JetBrains decompiler
// Type: PX.Data.DataScreenBase
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Data;

/// <exclude />
public abstract class DataScreenBase
{
  private PXGraph _graph;

  protected DataScreenBase(string screenID, PXGraph graph)
  {
    this.ScreenID = !string.IsNullOrEmpty(screenID) ? screenID : throw new ArgumentException("ScreenID cannot be empty.", nameof (screenID));
    this._graph = graph;
  }

  public virtual PXGraph DataGraph
  {
    get
    {
      if (this._graph == null)
        this._graph = this.InstantiateGraph();
      return this._graph;
    }
  }

  public abstract string ViewName { get; }

  public abstract string ParametersViewName { get; }

  public abstract IEnumerable<InqField> GetFields();

  public abstract IEnumerable<InqField> GetParameters();

  protected abstract PXGraph InstantiateGraph();

  public virtual PXView View => this.DataGraph.Views[this.ViewName];

  public virtual PXView ParametersView
  {
    get
    {
      return !string.IsNullOrEmpty(this.ParametersViewName) ? this.DataGraph.Views[this.ParametersViewName] : (PXView) null;
    }
  }

  public string ScreenID { get; }

  public abstract string DefaultAction { get; }

  public virtual string GetStyle(string fieldName, object row) => (string) null;
}
