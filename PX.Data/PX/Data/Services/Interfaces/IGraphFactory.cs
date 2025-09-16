// Decompiled with JetBrains decompiler
// Type: PX.Data.Services.Interfaces.IGraphFactory
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;

#nullable disable
namespace PX.Data.Services.Interfaces;

[PXInternalUseOnly]
[Obsolete("This interface is obsolete and will be removed in the future versions")]
public interface IGraphFactory
{
  PXGraph Create(string screenId);

  PXSiteMap.ScreenInfo GetScreenInfo(string screenId);
}
