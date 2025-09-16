// Decompiled with JetBrains decompiler
// Type: PX.Metadata.IScreenInfoStorage
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data;

#nullable disable
namespace PX.Metadata;

/// <summary>
/// <para>Implementations of this interface provide a storage for ScreenInfo objects.</para>
/// <para>It is utilized in <see cref="T:PX.Metadata.ScreenInfoProvider" /> and derived classes.</para>
/// </summary>
/// <remarks>
/// <para>The implementation can be non-thread safe (thread-safety is handled inside <see cref="T:PX.Metadata.ScreenInfoProvider" />)</para>
/// <para>ScreenID passed to the method arguments is always in the upper case.</para>
/// </remarks>
[PXInternalUseOnly]
public interface IScreenInfoStorage
{
  void Store(string screenId, string locale, PXSiteMap.ScreenInfo value);

  PXSiteMap.ScreenInfo Get(string screenId, string locale);
}
