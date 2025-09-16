// Decompiled with JetBrains decompiler
// Type: PX.Translation.MobileSiteMapRipperScope
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Translation;

public class MobileSiteMapRipperScope : IDisposable
{
  public static bool IsScoped { get; private set; }

  public MobileSiteMapRipperScope()
  {
    MobileSiteMapRipperScope.IsScoped = true;
    PXDatabase.SelectTimeStamp();
  }

  public void Dispose() => MobileSiteMapRipperScope.IsScoped = false;
}
