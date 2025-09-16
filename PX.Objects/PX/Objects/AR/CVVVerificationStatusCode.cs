// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.CVVVerificationStatusCode
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.AR.CCPaymentProcessing.Common;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace PX.Objects.AR;

public static class CVVVerificationStatusCode
{
  public const 
  #nullable disable
  string Matched = "MTH";
  public const string NotMatched = "NMH";
  public const string RequiredButNotVerified = "NOV";
  public const string RequiredButNotProvided = "SBP";
  public const string NotVerifiedDueToIssuer = "INV";
  public const string SkippedDueToPriorVerification = "RPV";
  public const string NotApplicable = "NOA";
  public const string Empty = "EMP";
  public const string Unknown = "UKN";
  private static (CcvVerificationStatus, string)[] _statuses = new (CcvVerificationStatus, string)[9]
  {
    (CcvVerificationStatus.Match, "MTH"),
    (CcvVerificationStatus.NotMatch, "NMH"),
    (CcvVerificationStatus.NotProcessed, "NOV"),
    (CcvVerificationStatus.ShouldHaveBeenPresent, "SBP"),
    (CcvVerificationStatus.IssuerUnableToProcessRequest, "INV"),
    (CcvVerificationStatus.RelyOnPreviousVerification, "RPV"),
    (CcvVerificationStatus.NotApplicable, "NOA"),
    (CcvVerificationStatus.Empty, "EMP"),
    (CcvVerificationStatus.Unknown, "UKN")
  };

  public static string GetCCVCode(CcvVerificationStatus sStatus)
  {
    if (!((IEnumerable<(CcvVerificationStatus, string)>) CVVVerificationStatusCode._statuses).Where<(CcvVerificationStatus, string)>((Func<(CcvVerificationStatus, string), bool>) (i => i.Item1 == sStatus)).Any<(CcvVerificationStatus, string)>())
      throw new InvalidOperationException();
    return ((IEnumerable<(CcvVerificationStatus, string)>) CVVVerificationStatusCode._statuses).Where<(CcvVerificationStatus, string)>((Func<(CcvVerificationStatus, string), bool>) (i => i.Item1 == sStatus)).First<(CcvVerificationStatus, string)>().Item2;
  }

  internal static CcvVerificationStatus GetCcvVerificationStatus(string CCVCode)
  {
    if (!((IEnumerable<(CcvVerificationStatus, string)>) CVVVerificationStatusCode._statuses).Where<(CcvVerificationStatus, string)>((Func<(CcvVerificationStatus, string), bool>) (i => i.Item2.Equals(CCVCode))).Any<(CcvVerificationStatus, string)>())
      throw new InvalidOperationException();
    return ((IEnumerable<(CcvVerificationStatus, string)>) CVVVerificationStatusCode._statuses).Where<(CcvVerificationStatus, string)>((Func<(CcvVerificationStatus, string), bool>) (i => i.Item2.Equals(CCVCode))).First<(CcvVerificationStatus, string)>().Item1;
  }

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new string[9]
      {
        "MTH",
        "NMH",
        "NOV",
        "SBP",
        "INV",
        "RPV",
        "NOA",
        "EMP",
        "UKN"
      }, new string[9]
      {
        "Matched",
        "Not Matched",
        "Required but Not Verified",
        "Required but Not Provided",
        "Not Verified Due to Issuer",
        "Skipped Due to Prior Verification",
        "Not applicable",
        "Processing center response is empty",
        "Unknown"
      })
    {
    }
  }

  public class match : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CVVVerificationStatusCode.match>
  {
    public match()
      : base("MTH")
    {
    }
  }
}
