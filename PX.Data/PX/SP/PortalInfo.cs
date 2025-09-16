// Decompiled with JetBrains decompiler
// Type: PX.SP.PortalInfo
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;
using System.Collections.Generic;

#nullable enable
namespace PX.SP;

[PXInternalUseOnly]
public class PortalInfo
{
  /// <summary>Contains integer ID of the Portal</summary>
  public int? ID { get; set; }

  /// <summary>Contains Name of the Portal</summary>
  public 
  #nullable disable
  string Name { get; set; }

  /// <summary>
  /// Contains URL associated with the Portal. This field contains the full URL, as it is provided by the user.
  /// </summary>
  public string Url { get; set; }

  /// <summary>
  /// Contains <see langword="true" /> if portal is Active.
  /// </summary>
  public bool IsActive { get; set; }

  /// <summary>
  /// Contains Role Name that is associated with the portal. This role is used to validate user access to the portal.
  /// </summary>
  public string AccessRole { get; set; }

  /// <summary>
  /// Contains <see langword="guid" /> of SiteMap ID that will be used as a Home Page for the Portal users.
  /// </summary>
  public string DefaultScreenID { get; set; }

  /// <summary>
  /// Contains uploaded file info that will be used as a Logo for the Portal users.
  /// </summary>
  public Guid? LogoFileID { get; set; }

  /// <summary>
  /// Contains uploaded file info that will be used as the sign-in page image for the Portal users.
  /// </summary>
  public Guid? SignInImageFileID { get; set; }

  /// <summary>
  /// Contains the theme preference that should be used for this portal.
  /// </summary>
  public string Theme { get; set; }

  /// <summary>
  /// Contains the company name text to be shown in the login page this portal.
  /// </summary>
  public string CompanyName { get; set; }

  /// <summary>
  /// Contains <see langword="int" /> Organization record that will be used by Financial Module on Portal.
  /// </summary>
  public int? OrganizationID { get; set; }

  /// <summary>
  /// Contains <see langword="int" /> Branch record that will be used for creation of new documents on Portal.
  /// </summary>
  public int? BranchID { get; set; }

  /// <summary>
  /// Contains <see langword="int" /> OrgBAccountID, this respresents the Organization Group when Source of Financial Documents is selected
  /// </summary>
  public int? OrgBAccountID { get; set; }

  /// <summary>
  /// Contains list of IDs of Branch records that portal user can see and access on Portal.
  /// </summary>
  public HashSet<int> BranchRestrictions { get; set; }

  /// <summary>
  /// Contains name of the class that should be used as an Address Plugin for portal users.
  /// </summary>
  public 
  #nullable enable
  string? AddressLookupPluginID { get; set; }

  /// <summary>
  /// Contains <see langword="true" /> if the navigation panels (both the top and left ones) should be displayed in the portal.
  /// </summary>
  public bool ShowNavigation { get; set; }

  /// <summary>
  /// Contains <see langword="true" /> if a predefined message is to be shown on the portal's landing page.
  /// </summary>
  public bool ShowTopMessage { get; set; }

  /// <summary>
  /// Contains the text of the predefined message to be shown on the portal's landing page
  /// </summary>
  public 
  #nullable disable
  string TopMessageText { get; set; }

  /// <summary>
  /// Contains uploaded file info that will be used as the image within the top message.
  /// </summary>
  public Guid? TopMessageImageFileID { get; set; }

  /// <summary>
  /// Contains <see langword="true" /> if a "Browse Catalog" button is to be shown on the portal's landing page.
  /// </summary>
  public bool ShowGoToCartButton { get; set; }

  /// <summary>
  /// Contains the text to be displayed within "Browse Catalog" button on the landing page of the portal
  /// </summary>
  public string GoToCartText { get; set; }

  /// <summary>
  /// Contains uploaded file info that will be used as the image within "Browse Catalog" button.
  /// </summary>
  public Guid? GoToCartImageFileID { get; set; }
}
