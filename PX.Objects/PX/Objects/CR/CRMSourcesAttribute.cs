// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CRMSourcesAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.CR;

/// <summary>
/// Attribute to specify system values for <see cref="T:PX.Objects.CR.Contact.source" />, <see cref="T:PX.Objects.CR.CRLead.source" /> and <see cref="T:PX.Objects.CR.CROpportunity.source" />.
/// Values for those screen should be adjusted by adjusting System Workflow for those screens.
/// </summary>
public class CRMSourcesAttribute : PXStringListAttribute
{
  /// <exclude />
  [Obsolete("This source is used only for backward compatibility.")]
  public const string _WEB = "W";
  /// <exclude />
  [Obsolete("This source is used only for backward compatibility.")]
  public const string _PHONE_INQ = "H";
  /// <exclude />
  [Obsolete("This source is used only for backward compatibility.")]
  public const string _REFERRAL = "R";
  /// <exclude />
  [Obsolete("This source is used only for backward compatibility.")]
  public const string _PURCHASED_LIST = "L";
  /// <exclude />
  [Obsolete("This source is used only for backward compatibility.")]
  public const string _OTHER = "O";
  /// <exclude />
  public const string OrganicSearch = "S";
  /// <exclude />
  public const string Campaign = "C";
  /// <exclude />
  public const string Referral = "R";
  /// <exclude />
  public const string Other = "O";
  /// <exclude />
  public const string Web = "W";
  /// <exclude />
  public const string PhoneInquiry = "H";
  /// <exclude />
  public const string PurchasedList = "L";
  internal static readonly string[] Values = new string[7]
  {
    "S",
    "C",
    "R",
    "O",
    "W",
    "H",
    "L"
  };

  /// <exclude />
  public CRMSourcesAttribute()
    : base(CRMSourcesAttribute.Values, new string[7]
    {
      "Organic Search",
      nameof (Campaign),
      nameof (Referral),
      nameof (Other),
      nameof (Web),
      "Phone Inquiry",
      "Purchased List"
    })
  {
  }
}
