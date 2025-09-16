// Decompiled with JetBrains decompiler
// Type: PX.SM.PXGeneralThemeVariableAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data;
using System;
using System.Linq;

#nullable disable
namespace PX.SM;

[PXInternalUseOnly]
public class PXGeneralThemeVariableAttribute : PXThemeVariableAttribute
{
  private readonly System.Type _themeField;

  public PXGeneralThemeVariableAttribute(System.Type themeField, string variableName)
    : base(variableName)
  {
    this._themeField = typeof (IBqlField).IsAssignableFrom(themeField) ? themeField : throw new ArgumentException($"The type {themeField.Name} must inherit the PX.Data.IBqlField interface.", nameof (themeField));
  }

  protected override string GetCurrentTheme(PXCache cache, object row)
  {
    string field = cache.GetField(this._themeField);
    return cache.GetValue(row ?? cache.Current, field) is string str ? str : PXThemeLoader.ThemeName;
  }

  protected override string GetDefaultVariableValue(PXCache cache, object row)
  {
    string currentTheme = this.GetCurrentTheme(cache, row);
    if (string.Equals(currentTheme, PXThemeLoader.ThemeName, StringComparison.OrdinalIgnoreCase))
      return base.GetDefaultVariableValue(cache, row);
    return PXThemeLoader.GetCssVariables(currentTheme).FirstOrDefault<PXThemeLoader.CssVariable>((Func<PXThemeLoader.CssVariable, bool>) (v => string.Equals(v.VariableName, this._variableName)))?.DefaultValue;
  }
}
