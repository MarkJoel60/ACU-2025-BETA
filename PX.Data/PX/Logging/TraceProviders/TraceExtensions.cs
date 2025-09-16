// Decompiled with JetBrains decompiler
// Type: PX.Logging.TraceProviders.TraceExtensions
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Logging.TraceProviders;

public static class TraceExtensions
{
  internal static bool IsExcluded(
    this Exception exception,
    string stackTrace,
    (Type ExceptionType, string ExceptionMessage, string ExceptionStackTraceText, int? HResult)[] conditions)
  {
    Type exType = exception.GetType();
    return ((IEnumerable<(Type, string, string, int?)>) conditions).Any<(Type, string, string, int?)>((Func<(Type, string, string, int?), bool>) (et =>
    {
      if (et.ExceptionType.IsAssignableFrom(exType))
      {
        if (et.ExceptionMessage != null)
        {
          string message = exception.Message;
          if ((message != null ? (message.Contains(et.ExceptionMessage) ? 1 : 0) : 0) == 0)
            goto label_7;
        }
        if (et.ExceptionStackTraceText == null || stackTrace.Contains(et.ExceptionStackTraceText))
        {
          if (!et.HResult.HasValue)
            return true;
          int hresult1 = exception.HResult;
          int? hresult2 = et.HResult;
          int valueOrDefault = hresult2.GetValueOrDefault();
          return hresult1 == valueOrDefault & hresult2.HasValue;
        }
      }
label_7:
      return false;
    }));
  }

  [Obsolete("Use overload with HResult in conditions.")]
  public static bool IsExcluded(
    this Exception exception,
    string stackTrace,
    (Type ExceptionType, string ExceptionMessage, string ExceptionStackTraceText)[] conditions)
  {
    return exception.IsExcluded(stackTrace, ((IEnumerable<(Type, string, string)>) conditions).Select<(Type, string, string), (Type, string, string, int?)>((Func<(Type, string, string), (Type, string, string, int?)>) (condition => (condition.ExceptionType, condition.ExceptionMessage, condition.ExceptionStackTraceText, new int?()))).ToArray<(Type, string, string, int?)>());
  }
}
