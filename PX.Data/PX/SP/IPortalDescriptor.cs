// Decompiled with JetBrains decompiler
// Type: PX.SP.IPortalDescriptor
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System.Collections.Generic;

#nullable disable
namespace PX.SP;

[PXInternalUseOnly]
public interface IPortalDescriptor
{
  /// <summary>
  /// Returns collection of PortalInfo classes that contains infomration about portals configured within current tentant.
  /// </summary>
  List<PortalInfo> GetPortals();

  /// <summary>
  /// Returns collection of PortalInfo classes that contains infomration about portals configured across all tentant within current instance.
  /// </summary>
  List<PortalInfo> GetAllPortals();

  /// <summary>
  /// Returns PortalInfo class that contains infomration about single portal found by ID within current tenant.
  /// </summary>
  PortalInfo GetPortal(int? portalID);

  /// <summary>
  /// Returns PortalInfo class that contains infomration about single portal found by ID within current tenant.
  /// </summary>
  PortalInfo GetPortal(string portalName);
}
