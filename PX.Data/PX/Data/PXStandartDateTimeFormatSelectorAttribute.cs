// Decompiled with JetBrains decompiler
// Type: PX.Data.PXStandartDateTimeFormatSelectorAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.SM;
using System;
using System.Collections;
using System.Globalization;

#nullable disable
namespace PX.Data;

public class PXStandartDateTimeFormatSelectorAttribute : PXCustomSelectorAttribute
{
  protected PXCache<LocaleFormat> Cache;
  protected char Code;
  protected string ViewName;

  public PXStandartDateTimeFormatSelectorAttribute(char code)
    : base(typeof (StandartDateTimeFormat.pattern))
  {
    this.Code = code;
    this.ValidateValue = false;
    this.ViewName = $"_StandartDateTimeFormat_{((int) this.Code).ToString()}_";
    this._ViewName = this.ViewName;
  }

  public override void FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    if (e.NewValue == null || !e.ExternalCall)
      return;
    string newValue = (string) e.NewValue;
    if (string.IsNullOrEmpty(newValue))
      return;
    try
    {
      System.DateTime.Now.ToString(newValue);
    }
    catch (Exception ex)
    {
      throw new PXSetPropertyException(ex.Message);
    }
  }

  public override void CacheAttached(PXCache sender)
  {
    this.Cache = sender as PXCache<LocaleFormat>;
    base.CacheAttached(sender);
  }

  protected virtual IEnumerable GetRecords()
  {
    if (this.Cache != null && this.Cache.Current != null)
    {
      LocaleFormat current = this.Cache.Current as LocaleFormat;
      CultureInfo cultureInfo = (CultureInfo) null;
      if (current != null && !string.IsNullOrEmpty(current.TemplateLocale))
        cultureInfo = CultureInfo.GetCultureInfo(current.TemplateLocale);
      if (cultureInfo == null)
        cultureInfo = CultureInfo.CurrentCulture;
      string[] strArray = cultureInfo.DateTimeFormat.GetAllDateTimePatterns(this.Code);
      for (int index = 0; index < strArray.Length; ++index)
      {
        string str = strArray[index];
        yield return (object) new StandartDateTimeFormat()
        {
          Pattern = str
        };
      }
      strArray = (string[]) null;
    }
  }
}
