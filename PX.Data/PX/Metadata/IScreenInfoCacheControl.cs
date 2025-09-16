// Decompiled with JetBrains decompiler
// Type: PX.Metadata.IScreenInfoCacheControl
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Caching;
using PX.Common;
using PX.Data;

#nullable disable
namespace PX.Metadata;

/// <summary>
/// An abstraction used to granularly control cache invalidation for the <see cref="T:PX.Data.PXSiteMap.ScreenInfo" /> cache
/// <b>in the current tenant.</b>
/// </summary>
/// <remarks>
/// Changes in the following areas must (and do) invalidate the cache,
/// either directly (via <see cref="M:PX.Metadata.IScreenInfoCacheControl.InvalidateCache(System.String)" /> call) or indirectly (by being included in the caching key):
/// <list type="bullet">
/// <item> <description>Application Version (including ISV version)</description> </item>
/// <item> <description>Instance ID</description> </item>
/// <item> <description>Customizations (publish/unpublish)</description> </item>
/// <item> <description>Tenant ID</description> </item>
/// <item> <description>Installation Type (ERP or Portal)</description> </item>
/// <item> <description>Current Locale</description> </item>
/// <item> <description>Locales List</description> </item>
/// <item> <description>Localization</description> </item>
/// <item> <description>Enabled Features</description> </item>
/// <item> <description>Attribute Fields</description> </item>
/// <item> <description>User-Defined Fields (UDF)</description> </item>
/// <item> <description>Site Map / Portal Map</description> </item>
/// <item> <description>Generic Inquiries</description> </item>
/// <item> <description>Generic Inquiry anomalies ML processing data is imported or deleted</description> </item>
/// <item> <description>User Reports (stored in DB)</description> </item>
/// <item> <description>RPX Reports (stored on file system)</description> </item>
/// <item> <description>ARM Reports</description> </item>
/// <item> <description>Dashboards</description> </item>
/// <item> <description>Pivot Tables</description> </item>
/// <item> <description>Import/Export Scenarios</description> </item>
/// <item> <description>Power BI Reports</description> </item>
/// <item> <description>Business Scenarios (Wizards)</description> </item>
/// <item> <description>ASPX pages</description> </item>
/// <item> <description>Automation Definitions</description> </item>
/// <item> <description>Workflow Configuration (indirectly: it can be changed either by customizations or by changes in App_Runtime folder, and these cases are already handled separately)</description> </item>
/// <item> <description>Snapshot Restoration</description> </item>
/// </list>.
/// <paramref name="screenId" /> can contain along with the screen identifiers another unique identifiers of <see cref="T:PX.Data.PXSiteMap.ScreenInfo" /> such as Generic Inquiry DesignID. The parameter will be renamed in a next PR.
/// </remarks>
[PXInternalUseOnly]
public interface IScreenInfoCacheControl : ICacheControlledBy<PXSiteMap.ScreenInfo>, ICacheControl
{
  void InvalidateCache(string screenId);

  void InvalidateCache(string screenId, string locale);
}
