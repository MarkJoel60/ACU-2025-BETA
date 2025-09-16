// Decompiled with JetBrains decompiler
// Type: PX.Data.PXSiteMapDummyProvider
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using PX.Security;
using System;

#nullable disable
namespace PX.Data;

public class PXSiteMapDummyProvider : PXSiteMapProvider
{
  private readonly PXSiteMapDummyProvider.EmptyDefinition _definition;

  public PXSiteMapDummyProvider(IRoleManagementService roleManagementService)
    : base((IOptions<PXSiteMapOptions>) new OptionsWrapper<PXSiteMapOptions>(new PXSiteMapOptions()), (IHttpContextAccessor) new PXSiteMapDummyProvider.DummyHttpContextAccessor(), roleManagementService)
  {
    this._definition = new PXSiteMapDummyProvider.EmptyDefinition((PXSiteMapProvider) this);
  }

  protected void AddNode(Guid? nodeId = null, string screenId = null, System.Type graphType = null, string url = null, string title = null)
  {
    this._definition.AddNode(nodeId, screenId, graphType, url, title);
  }

  protected override PXSiteMapProvider.Definition GetSlot(string slotName)
  {
    return (PXSiteMapProvider.Definition) this._definition;
  }

  protected override void ResetSlot(string slotName)
  {
  }

  protected class EmptyDefinition : PXSiteMapProvider.Definition
  {
    private readonly PXSiteMapProvider _provider;

    public EmptyDefinition(PXSiteMapProvider provider)
    {
      this._provider = provider;
      this.AddNode(new Guid?(Guid.Empty), "00000000");
    }

    public void AddNode(
      Guid? nodeId = null,
      string screenId = null,
      System.Type graphType = null,
      string aspxUrl = null,
      string title = null,
      string tsUrl = null,
      string selectedUi = "E")
    {
      this.AddNode(new PXSiteMapNode(this._provider, nodeId ?? Guid.NewGuid(), aspxUrl ?? "~/Frames/Default.aspx", title, (PXRoleList) null, graphType?.FullName, screenId ?? "00000000"), Guid.Empty);
    }
  }

  internal class DummyHttpContextAccessor : IHttpContextAccessor
  {
    public HttpContext HttpContext { get; set; }
  }
}
