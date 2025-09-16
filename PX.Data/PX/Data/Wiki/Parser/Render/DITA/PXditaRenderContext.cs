// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.Render.DITA.PXditaRenderContext
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.Wiki.DITA;
using System.Collections.Generic;

#nullable disable
namespace PX.Data.Wiki.Parser.Render.DITA;

public class PXditaRenderContext
{
  public PXWikiParserContext Settings;

  public Topic CurrentTopic => (Topic) this.Settings.GetExchangedata()["topic"];

  public Stack<PXDitaElement> CurrentParent
  {
    get => (Stack<PXDitaElement>) this.Settings.GetExchangedata()[nameof (CurrentParent)];
  }
}
