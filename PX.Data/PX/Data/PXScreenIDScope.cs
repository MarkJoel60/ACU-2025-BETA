// Decompiled with JetBrains decompiler
// Type: PX.Data.PXScreenIDScope
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;
using System.Web;

#nullable disable
namespace PX.Data;

public class PXScreenIDScope : IDisposable
{
  private const string ScreenIDMask = ">CC.CC.CC.CC";
  private readonly string _screenId;
  private readonly string _oldScreenID;
  private readonly PXGraph _graph;

  public PXScreenIDScope(string screenID)
  {
    this._screenId = this.Unmask(screenID);
    this._oldScreenID = this.Unmask(PXContext.GetScreenID());
    PXContext.SetScreenID(this.Mask(screenID));
  }

  public PXScreenIDScope(PXGraph graph, string screenID)
    : this(screenID)
  {
    this._graph = graph;
    graph.Accessinfo.ScreenID = this.Mask(screenID);
  }

  private string Mask(string screenID) => PX.Common.Mask.Format(">CC.CC.CC.CC", screenID);

  private string Unmask(string screenID)
  {
    return string.IsNullOrEmpty(screenID) ? screenID : screenID.Replace(".", "");
  }

  public void Dispose()
  {
    PXContext.SetScreenID(this.Mask(this._oldScreenID));
    if (this._graph == null)
      return;
    this._graph.Accessinfo.ScreenID = this.Mask(this._oldScreenID);
  }

  public void AddToContext(HttpContext context)
  {
    PXContext.SetScreenID(context, this.Mask(this._screenId));
  }
}
