// Decompiled with JetBrains decompiler
// Type: PX.SP.PortalDescriptorMock
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Collections.Generic;

#nullable disable
namespace PX.SP;

internal class PortalDescriptorMock : IPortalDescriptor
{
  public List<PortalInfo> GetAllPortals() => new List<PortalInfo>();

  public PortalInfo GetPortal(int? portalID) => (PortalInfo) null;

  public PortalInfo GetPortal(string portalName) => (PortalInfo) null;

  public List<PortalInfo> GetPortals() => new List<PortalInfo>();
}
