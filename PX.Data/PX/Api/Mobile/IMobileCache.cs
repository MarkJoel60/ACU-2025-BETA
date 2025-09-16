// Decompiled with JetBrains decompiler
// Type: PX.Api.Mobile.IMobileCache
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;

#nullable disable
namespace PX.Api.Mobile;

[PXInternalUseOnly]
public interface IMobileCache
{
  /// <summary>
  /// Need this to subscribe mobile caches on Database changes (Roles and Localizations mostly)
  /// to keep cache in correct state, if nobody started mobile app after IIS reset.
  /// </summary>
  void Init();
}
