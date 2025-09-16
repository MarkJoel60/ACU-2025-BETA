// Decompiled with JetBrains decompiler
// Type: PX.Translation.ResourceByScreenComparer
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Translation;

internal class ResourceByScreenComparer : IEqualityComparer<LocalizationResourceByScreen>
{
  bool IEqualityComparer<LocalizationResourceByScreen>.Equals(
    LocalizationResourceByScreen x,
    LocalizationResourceByScreen y)
  {
    if (x == y || x == null && y == null)
      return true;
    return x != null && y != null && (x.IdValue == null || y.IdValue != null) && (x.IdValue != null || y.IdValue == null) && (x.IdRes == null || y.IdRes != null) && (x.IdRes != null || y.IdRes == null) && (x.ScreenID == null || y.ScreenID != null) && (x.ScreenID != null || y.ScreenID == null) && string.Compare(x.IdValue, y.IdValue, StringComparison.OrdinalIgnoreCase) == 0 && string.Compare(x.IdRes, y.IdRes, StringComparison.OrdinalIgnoreCase) == 0 && string.Compare(x.ScreenID, y.ScreenID, StringComparison.OrdinalIgnoreCase) == 0;
  }

  int IEqualityComparer<LocalizationResourceByScreen>.GetHashCode(LocalizationResourceByScreen obj)
  {
    return obj == null ? 0 : $"{obj.ScreenID}_{obj.IdValue}_{obj.IdRes}".GetHashCode();
  }
}
