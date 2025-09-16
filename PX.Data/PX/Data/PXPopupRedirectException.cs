// Decompiled with JetBrains decompiler
// Type: PX.Data.PXPopupRedirectException
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System.Runtime.Serialization;

#nullable disable
namespace PX.Data;

/// <summary>Opens the specified application page in a pop-up window.</summary>
public class PXPopupRedirectException : PXBaseRedirectException
{
  protected bool _Reload;
  protected bool _Persist;
  protected PXGraph _Graph;

  public bool Reload => this._Reload;

  public bool Persist => this._Persist;

  public PXGraph Graph
  {
    get => this._Graph;
    private set
    {
      this._Graph = value;
      PXBaseRedirectException.populateGraphTimeStamp(this._Graph);
      this._Graph.EnsureIfArchived();
      PXReusableGraphFactory.SetRedirect();
    }
  }

  public PXPopupRedirectException(PXGraph graph, string message)
    : this(graph, message, false)
  {
  }

  public PXPopupRedirectException(PXGraph graph, string message, bool Reload)
    : this(graph, message, Reload, true)
  {
  }

  public PXPopupRedirectException(PXGraph graph, string message, bool Reload, bool Persist)
    : base(message)
  {
    this._Persist = Reload || Persist;
    this._Reload = Reload;
    this.Graph = graph;
  }

  public static PXPopupRedirectException FromRedirectRequired(PXRedirectRequiredException e)
  {
    return new PXPopupRedirectException(e.Graph, e.Message, true);
  }

  public PXPopupRedirectException(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
    this.HResult = -2147024809;
    ReflectionSerializer.RestoreObjectProps<PXPopupRedirectException>(this, info);
  }

  public override void GetObjectData(SerializationInfo info, StreamingContext context)
  {
    ReflectionSerializer.GetObjectData<PXPopupRedirectException>(this, info);
    base.GetObjectData(info, context);
  }
}
