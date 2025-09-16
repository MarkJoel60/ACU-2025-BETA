// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.Pdf.PXCurrentMargins
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Collections.Generic;

#nullable disable
namespace PX.Data.Wiki.Parser.Pdf;

public class PXCurrentMargins : List<PXMargin>
{
  public int GetWidth(int y)
  {
    int width = 0;
    foreach (PXMargin pxMargin in (List<PXMargin>) this)
    {
      if (pxMargin.Float != ElementFloat.None && pxMargin.Contains(y))
        width += pxMargin.Width;
    }
    return width;
  }

  public PXMargin this[ElementFloat floating, int y]
  {
    get
    {
      foreach (PXMargin pxMargin in (List<PXMargin>) this)
      {
        if (pxMargin.Float == floating && pxMargin.Contains(y))
          return pxMargin;
      }
      return (PXMargin) null;
    }
  }
}
