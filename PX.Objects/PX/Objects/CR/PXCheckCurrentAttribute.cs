// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.PXCheckCurrentAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.CR;

public class PXCheckCurrentAttribute : PXViewExtensionAttribute
{
  private string _hostViewName;

  public virtual void ViewCreated(PXGraph graph, string viewName)
  {
    this._hostViewName = viewName;
    // ISSUE: method pointer
    graph.Initialized += new PXGraphInitializedDelegate((object) this, __methodptr(\u003CViewCreated\u003Eb__1_0));
  }

  private PXCache GetCache(PXGraph graph) => graph.Views[this._hostViewName].Cache;
}
