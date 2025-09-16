// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PostingOptions
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.PM;

public static class PostingOptions
{
  private static IDictionary<string, string> _allValues = (IDictionary<string, string>) new Dictionary<string, string>()
  {
    ["PL"] = "Detailed Projects, Detailed Accounts",
    ["PD"] = "Detailed Projects, Default Accounts",
    ["XL"] = "Non-Project, Detailed Accounts",
    ["XD"] = "Non-Project, Default Accounts",
    ["XT"] = "Non-Project, Total Adjustment"
  };
  public const string ProjectLineAccount = "PL";
  public const string ProjectDefaultAccount = "PD";
  public const string NonProjectLineAccount = "XL";
  public const string NonProjectDefaultAccount = "XD";
  public const string NonProjectTotal = "XT";

  public static string GetOptionDisplayName(string option)
  {
    string str;
    return PostingOptions._allValues.TryGetValue(option, out str) ? str : string.Empty;
  }

  public static void ParsePostingOptions(
    string option,
    out bool useLineProject,
    out bool useLineAccounts)
  {
    PostingOptions.ParsePostingOptions(option, out useLineProject, out useLineAccounts, out bool _);
  }

  public static void ParsePostingOptions(
    string option,
    out bool useLineProject,
    out bool useLineAccounts,
    out bool postTotalOnly)
  {
    useLineProject = false;
    useLineAccounts = false;
    postTotalOnly = false;
    if (string.IsNullOrWhiteSpace(option))
      return;
    switch (option)
    {
      case "PL":
        useLineProject = true;
        useLineAccounts = true;
        postTotalOnly = false;
        break;
      case "PD":
        useLineProject = true;
        useLineAccounts = false;
        postTotalOnly = false;
        break;
      case "XL":
        useLineProject = false;
        useLineAccounts = true;
        postTotalOnly = false;
        break;
      case "XD":
        useLineProject = false;
        useLineAccounts = false;
        postTotalOnly = false;
        break;
      case "XT":
        useLineProject = false;
        useLineAccounts = false;
        postTotalOnly = true;
        break;
    }
  }

  public class OverbillingUnderbillingOptionsListAttribute : PXStringListAttribute
  {
    public OverbillingUnderbillingOptionsListAttribute()
      : base(new Tuple<string, string>[5]
      {
        PXStringListAttribute.Pair("PL", "Detailed Projects, Detailed Accounts"),
        PXStringListAttribute.Pair("PD", "Detailed Projects, Default Accounts"),
        PXStringListAttribute.Pair("XL", "Non-Project, Detailed Accounts"),
        PXStringListAttribute.Pair("XD", "Non-Project, Default Accounts"),
        PXStringListAttribute.Pair("XT", "Non-Project, Total Adjustment")
      })
    {
    }
  }

  public class RevenueOptionsListAttribute : PXStringListAttribute
  {
    public RevenueOptionsListAttribute()
      : base(new Tuple<string, string>[4]
      {
        PXStringListAttribute.Pair("PL", "Detailed Projects, Detailed Accounts"),
        PXStringListAttribute.Pair("PD", "Detailed Projects, Default Account"),
        PXStringListAttribute.Pair("XL", "Non-Project, Detailed Accounts"),
        PXStringListAttribute.Pair("XD", "Non-Project, Default Account")
      })
    {
    }
  }
}
