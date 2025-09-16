// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.VerifyingHelper
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.Common;

public class VerifyingHelper
{
  public static object GetNewValueByIncoming(
    PXCache cache,
    object row,
    string fieldName,
    bool externalCall)
  {
    if (externalCall)
    {
      object valuePending = cache.GetValuePending(row, fieldName);
      if (valuePending != null)
        return valuePending;
    }
    try
    {
      return PXFieldState.UnwrapValue(cache.GetValueExt(row, fieldName));
    }
    catch
    {
    }
    return cache.GetValue(row, fieldName);
  }
}
