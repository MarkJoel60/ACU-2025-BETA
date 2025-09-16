// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.AdvancedAuthenticationRestrictor
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using System;

#nullable disable
namespace PX.Objects.CS;

internal class AdvancedAuthenticationRestrictor : IAdvancedAuthenticationRestrictor
{
  private const string _allowedProviderName = "ExchangeIdentityToken";

  public bool IsAllowedProviderName(string name)
  {
    ExceptionExtensions.ThrowOnNull<string>(name, nameof (name), (string) null);
    return "ExchangeIdentityToken".Equals(name, StringComparison.Ordinal) || PXAccess.FeatureInstalled<FeaturesSet.activeDirectoryAndOtherExternalSSO>() || PXAccess.FeatureInstalled<FeaturesSet.openIDConnect>();
  }

  public bool IsDeprecatedProvider(string name)
  {
    return !"ExchangeIdentityToken".Equals(name, StringComparison.Ordinal);
  }
}
