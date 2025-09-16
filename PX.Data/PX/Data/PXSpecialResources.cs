// Decompiled with JetBrains decompiler
// Type: PX.Data.PXSpecialResources
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.SM;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

#nullable disable
namespace PX.Data;

/// <exclude />
public static class PXSpecialResources
{
  public static string[] ColorNames => PX.Common.Drawing.GetColorNames();

  public static string[] FontNames => FontFamilyEx.GetFontNames();

  public static string[] FontFamilyNames
  {
    get
    {
      return ((IEnumerable<FontFamily>) FontFamily.Families).Select<FontFamily, string>((Func<FontFamily, string>) (family => family.Name)).ToArray<string>();
    }
  }

  public static (string Theme, string VariableDisplayName)[] ThemeVariables
  {
    get
    {
      return PXThemeLoader.AvailableThemes.SelectMany<string, (string, string)>((Func<string, IEnumerable<(string, string)>>) (theme => PXThemeLoader.GetCssVariables(theme).Select<PXThemeLoader.CssVariable, (string, string)>((Func<PXThemeLoader.CssVariable, (string, string)>) (v => (theme, v.DisplayName))))).ToArray<(string, string)>();
    }
  }
}
