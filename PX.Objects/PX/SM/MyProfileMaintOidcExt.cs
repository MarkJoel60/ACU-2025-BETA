// Decompiled with JetBrains decompiler
// Type: PX.SM.MyProfileMaintOidcExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.OidcClient;
using PX.OidcClient.GraphExtensions;

#nullable disable
namespace PX.SM;

[PXInternalUseOnly]
public class MyProfileMaintOidcExt : AccessOidc<MyProfileMaint>
{
  public static bool IsActive() => FeaturesHelper.OpenIDConnect;
}
