// Decompiled with JetBrains decompiler
// Type: PX.Caching.IScreenInfoVersion
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;

#nullable disable
namespace PX.Caching;

[PXInternalUseOnly]
public interface IScreenInfoVersion
{
  int GetCurrent(string screenInfoId);

  void Increment(string screenInfoId);
}
