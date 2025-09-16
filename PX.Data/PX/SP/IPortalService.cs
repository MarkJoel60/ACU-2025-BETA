// Decompiled with JetBrains decompiler
// Type: PX.SP.IPortalService
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System.Collections.Generic;
using System.Web.Security;

#nullable disable
namespace PX.SP;

[PXInternalUseOnly]
public interface IPortalService
{
  /// <summary>
  /// Returns <see langword="true" /> if the current code is running within Portal context;
  /// <see langword="false" /> otherwise.
  /// </summary>
  bool IsPortalContext();

  /// <summary>
  /// Returns ID of Portal as <see langword="int" /> if the current code is running within Portal context;
  /// <see langword="null" /> otherwise.
  /// </summary>
  int? GetPortalID();

  /// <summary>
  /// Returns PortalInfo if the current code is running within Portal context;
  /// <see langword="null" /> otherwise.
  /// </summary>
  PortalInfo GetPortal();

  /// <summary>
  /// Returns collection of PortalInfo classes that contains infomration about portals configured within current tentant.
  /// </summary>
  List<PortalInfo> GetPortals();

  /// <summary>
  /// Returns <see langword="true" /> if the poral feature is active;
  /// <see langword="false" /> otherwise.
  /// </summary>
  bool IsPortalFeatureInstalled();

  /// <summary>
  /// Returns <see langword="true" /> if the user has roles that matches portal access role configuration;
  /// <see langword="false" /> otherwise.
  /// </summary>
  bool IsPortalAccessAllowed(string UserName);

  /// <summary>
  /// Returns <see langword="true" /> if the user has roles that matches portal access role configuration;
  /// <see langword="false" /> otherwise.
  /// </summary>
  bool ValidateUser(MembershipUser user);
}
