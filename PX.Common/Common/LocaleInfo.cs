// Decompiled with JetBrains decompiler
// Type: PX.Common.LocaleInfo
// Assembly: PX.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 35AC74DC-4190-4222-F56C-8CF32A9F1C93
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Common.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Common.xml

using System;
using System.Globalization;
using System.Reflection;
using System.Threading;
using System.Web;
using System.Web.Compilation;

#nullable disable
namespace PX.Common;

public sealed class LocaleInfo
{
  private static bool \u0002;
  private static MethodInfo \u000E;

  public static void SetCulture(CultureInfo culture)
  {
    Thread.CurrentThread.CurrentCulture = LocaleInfo.ApplyCustomFormat(culture);
  }

  public static void SetUICulture(CultureInfo culture)
  {
    Thread.CurrentThread.CurrentUICulture = LocaleInfo.ApplyCustomFormat(culture);
  }

  public static void SetAllCulture(CultureInfo culture)
  {
    LocaleInfo.SetCulture(culture);
    LocaleInfo.SetUICulture(culture);
  }

  [PXInternalUseOnly]
  public static void SetAllCulture(HttpContextBase httpContext)
  {
    LocaleInfo.\u0002(PXSessionContextFactory.GetTempSessionBucket(httpContext));
  }

  private static void \u0002(PXSessionContext _param0)
  {
    if (_param0 == null)
      _param0 = PXContext.PXIdentity;
    LocaleInfo.SetCulture(_param0.Culture);
    LocaleInfo.SetUICulture(_param0.UICulture);
  }

  public static void ReaplyAllCulture()
  {
    LocaleInfo.SetCulture(LocaleInfo.GetCulture());
    LocaleInfo.SetUICulture(LocaleInfo.GetUICulture());
  }

  public static CultureInfo GetCulture()
  {
    return PXContext.PXIdentity.Culture ?? Thread.CurrentThread.CurrentCulture;
  }

  public static CultureInfo GetUICulture()
  {
    return PXContext.PXIdentity.UICulture ?? Thread.CurrentThread.CurrentUICulture;
  }

  internal static CultureInfo ApplyCustomFormat(CultureInfo _param0)
  {
    if (!LocaleInfo.\u0002)
    {
      Type type = PXBuildManager.GetType("PX.Data.Localization.LocalesFormatProvider", false);
      if (type != (Type) null)
        LocaleInfo.\u000E = type.GetMethod("ApplyLocaleFormat", BindingFlags.Static | BindingFlags.Public);
      if (LocaleInfo.\u000E != (MethodInfo) null)
        LocaleInfo.\u0002 = true;
    }
    if (_param0.IsReadOnly)
      _param0 = (CultureInfo) _param0.Clone();
    if (LocaleInfo.\u000E != (MethodInfo) null)
      LocaleInfo.\u000E.Invoke((object) null, new object[1]
      {
        (object) _param0
      });
    return _param0;
  }

  public static void SetTimeZone(PXTimeZoneInfo zone) => PXContext.PXIdentity.TimeZone = zone;

  public static PXTimeZoneInfo GetTimeZone()
  {
    PXSessionContext pxIdentity = PXContext.PXIdentity;
    return !pxIdentity.IsAnonymous() && pxIdentity.TimeZone != null ? pxIdentity.TimeZone : PXTimeZoneInfo.Invariant;
  }
}
