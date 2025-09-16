// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.UIState
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.Common;

public static class UIState
{
  public static void RaiseOrHideError<T>(
    PXCache cache,
    object row,
    bool isIncorrect,
    string message,
    PXErrorLevel errorLevel,
    params object[] parameters)
    where T : IBqlField
  {
    if (isIncorrect)
      cache.RaiseExceptionHandling<T>(row, PXFieldState.UnwrapValue(cache.GetValueExt<T>(row)), (Exception) new PXSetPropertyException(message, errorLevel, parameters));
    else
      cache.RaiseExceptionHandling<T>(row, PXFieldState.UnwrapValue(cache.GetValueExt<T>(row)), (Exception) null);
  }

  public static void RaiseOrHideErrorByErrorLevelPriority<T>(
    PXCache cache,
    object row,
    bool isIncorrect,
    string message,
    PXErrorLevel errorLevel,
    params object[] parameters)
    where T : IBqlField
  {
    if (UIState.IsHigherErrorLevelExist<T>(cache, row, errorLevel))
      return;
    UIState.RaiseOrHideError<T>(cache, row, isIncorrect, message, errorLevel, parameters);
  }

  public static bool IsHigherErrorLevelExist<T>(PXCache cache, object row, PXErrorLevel errorLevel) where T : IBqlField
  {
    PXFieldState stateExt = (PXFieldState) cache.GetStateExt<T>(row);
    return (stateExt != null ? (stateExt.ErrorLevel > errorLevel ? 1 : 0) : 0) != 0;
  }
}
