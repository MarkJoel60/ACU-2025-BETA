// Decompiled with JetBrains decompiler
// Type: PX.CS.RMFontNamesListAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.CS;

public class RMFontNamesListAttribute : PXStringListAttribute
{
  public override void CacheAttached(PXCache sender)
  {
    this._AllowedValues = ((IEnumerable<string>) PXSpecialResources.FontNames).ToArray<string>();
    this._AllowedLabels = ((IEnumerable<string>) this._AllowedValues).Select<string, string>((Func<string, string>) (font =>
    {
      string fontName = font;
      PXLocalizerRepository.SpecialLocalizer.LocalizeFontName(ref fontName);
      return fontName;
    })).ToArray<string>();
    base.CacheAttached(sender);
  }
}
