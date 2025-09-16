// Decompiled with JetBrains decompiler
// Type: PX.Data.PXWikiDummyProvider
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using PX.Security;

#nullable disable
namespace PX.Data;

public class PXWikiDummyProvider(IRoleManagementService roleManagementService) : PXWikiProvider((IOptions<PXWikiSiteMapOptions>) new OptionsWrapper<PXWikiSiteMapOptions>(new PXWikiSiteMapOptions()), (IHttpContextAccessor) new PXSiteMapDummyProvider.DummyHttpContextAccessor(), roleManagementService)
{
  private static readonly PXSiteMapProvider.Definition EmptyDefinitionInstance = (PXSiteMapProvider.Definition) new PXWikiDummyProvider.EmptyDefinition();

  protected override PXSiteMapProvider.Definition GetSlot(string slotName)
  {
    return PXWikiDummyProvider.EmptyDefinitionInstance;
  }

  protected override void ResetSlot(string slotName)
  {
  }

  private class EmptyDefinition : PXSiteMapProvider.Definition
  {
  }
}
