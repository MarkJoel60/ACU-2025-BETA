// Decompiled with JetBrains decompiler
// Type: PX.Common.PXPageCacheUtils
// Assembly: PX.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 35AC74DC-4190-4222-F56C-8CF32A9F1C93
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Common.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Common.xml

using System;

#nullable enable
namespace PX.Common;

[Obsolete("This class will be moved out of PX.Common in the future versions")]
public static class PXPageCacheUtils
{
  [Obsolete("This method will be removed in the future versions. Use ICacheControl<PageCache>.InvalidateCache() whenever possible.")]
  public static Action InvalidateCachedPages = new Action(PXPageCacheUtils.\u0002.\u0002.\u0002);

  [Serializable]
  private sealed class \u0002
  {
    public static readonly 
    #nullable disable
    PXPageCacheUtils.\u0002 \u0002 = new PXPageCacheUtils.\u0002();

    internal void \u0002()
    {
    }
  }
}
