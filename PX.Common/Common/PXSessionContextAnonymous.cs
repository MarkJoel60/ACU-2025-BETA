// Decompiled with JetBrains decompiler
// Type: PX.Common.PXSessionContextAnonymous
// Assembly: PX.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 35AC74DC-4190-4222-F56C-8CF32A9F1C93
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Common.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Common.xml

using System.Runtime.CompilerServices;

#nullable disable
namespace PX.Common;

[PXInternalUseOnly]
public static class PXSessionContextAnonymous
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool IsAnonymous(this PXSessionContext pxIdentity)
  {
    return Anonymous.IsAnonymous(pxIdentity?.User);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool IsAuthenticatedAndNotEmpty(this PXSessionContext pxIdentity)
  {
    return pxIdentity.Authenticated && !string.IsNullOrEmpty(pxIdentity.IdentityName) && !Anonymous.IsAnonymous(pxIdentity.IdentityName);
  }
}
