// Decompiled with JetBrains decompiler
// Type: PX.Data.WebAppTypeExtensions
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data;

internal static class WebAppTypeExtensions
{
  [Obsolete("Don't rely on this flag; encapsulate your logic instead.")]
  public static bool IsPortal(this WebAppType webAppType)
  {
    if (webAppType == null)
      throw new ArgumentNullException(nameof (webAppType));
    return webAppType.AppTypeId == 1;
  }
}
