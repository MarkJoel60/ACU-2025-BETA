// Decompiled with JetBrains decompiler
// Type: PX.SP.PortalServiceMock
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Collections.Generic;
using System.Web.Security;

#nullable disable
namespace PX.SP;

internal class PortalServiceMock : IPortalService
{
  public PortalInfo GetPortal() => (PortalInfo) null;

  public int? GetPortalID() => new int?();

  public List<PortalInfo> GetPortals() => new List<PortalInfo>();

  public bool IsPortalAccessAllowed(string UserName) => false;

  public bool IsPortalContext() => false;

  public bool IsPortalFeatureInstalled() => false;

  public bool IsPortalInstance() => false;

  public bool ValidateUser(MembershipUser user) => false;
}
