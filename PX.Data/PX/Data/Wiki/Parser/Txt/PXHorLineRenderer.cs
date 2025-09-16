// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.Txt.PXHorLineRenderer
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data.Wiki.Parser.Txt;

/// <summary>
/// Represents a class for PXHorLineElement txt rendering.
/// </summary>
internal class PXHorLineRenderer : PXTxtRenderer
{
  protected override void Render(PXElement elem, PXTxtRenderContext resultTxt)
  {
    if (resultTxt.Result.Length != 0 && !resultTxt.Result.EndsWith(Environment.NewLine))
      resultTxt.NewLine();
    if (resultTxt.ColsCount == 0)
    {
      resultTxt.Append("----------------------------");
    }
    else
    {
      for (int index = 0; index < resultTxt.ColsCount; ++index)
        resultTxt.Append("-");
    }
    resultTxt.NewLine();
  }
}
