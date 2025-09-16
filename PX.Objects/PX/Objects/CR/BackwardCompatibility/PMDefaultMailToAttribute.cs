// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.BackwardCompatibility.PMDefaultMailToAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.CR.BackwardCompatibility;

/// <exclude />
[Obsolete]
public class PMDefaultMailToAttribute : CRDefaultMailToAttribute
{
  public PMDefaultMailToAttribute()
  {
  }

  public PMDefaultMailToAttribute(System.Type select)
    : base(select)
  {
  }

  protected override PXSelectBase GetSelectView(PXGraph graph)
  {
    return (PXSelectBase) graph.GetType().GetField(this._hostViewName).GetValue((object) graph);
  }
}
