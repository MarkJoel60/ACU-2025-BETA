// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.CCTranStatusCode
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

public static class CCTranStatusCode
{
  public const 
  #nullable disable
  string Approved = "APR";
  public const string Declined = "DEC";
  public const string HeldForReview = "HFR";
  public const string Error = "ERR";
  public const string Expired = "EXP";
  public const string Unknown = "UKN";
  private static (CCTranStatus, string)[] statusCodes = new (CCTranStatus, string)[6]
  {
    (CCTranStatus.Approved, "APR"),
    (CCTranStatus.Declined, "DEC"),
    (CCTranStatus.Error, "ERR"),
    (CCTranStatus.HeldForReview, "HFR"),
    (CCTranStatus.Expired, "EXP"),
    (CCTranStatus.Unknown, "UKN")
  };

  public static string GetCode(CCTranStatus status)
  {
    if (!((IEnumerable<(CCTranStatus, string)>) CCTranStatusCode.statusCodes).Where<(CCTranStatus, string)>((Func<(CCTranStatus, string), bool>) (i => i.Item1 == status)).Any<(CCTranStatus, string)>())
      throw new InvalidOperationException();
    return ((IEnumerable<(CCTranStatus, string)>) CCTranStatusCode.statusCodes).Where<(CCTranStatus, string)>((Func<(CCTranStatus, string), bool>) (i => i.Item1 == status)).First<(CCTranStatus, string)>().Item2;
  }

  internal static CCTranStatus GetCCTranStatus(string tranStatusCode)
  {
    if (!((IEnumerable<(CCTranStatus, string)>) CCTranStatusCode.statusCodes).Where<(CCTranStatus, string)>((Func<(CCTranStatus, string), bool>) (i => i.Item2.Equals(tranStatusCode))).Any<(CCTranStatus, string)>())
      throw new InvalidOperationException();
    return ((IEnumerable<(CCTranStatus, string)>) CCTranStatusCode.statusCodes).Where<(CCTranStatus, string)>((Func<(CCTranStatus, string), bool>) (i => i.Item2.Equals(tranStatusCode))).First<(CCTranStatus, string)>().Item1;
  }

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new string[6]
      {
        "APR",
        "DEC",
        "ERR",
        "HFR",
        "EXP",
        "UKN"
      }, new string[6]
      {
        "Approved",
        "Declined",
        "Error",
        "Held for Review",
        "Expired",
        "Unknown"
      })
    {
    }
  }

  public class approved : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CCTranStatusCode.approved>
  {
    public approved()
      : base("APR")
    {
    }
  }

  public class declined : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CCTranStatusCode.declined>
  {
    public declined()
      : base("DEC")
    {
    }
  }

  public class heldForReview : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CCTranStatusCode.heldForReview>
  {
    public heldForReview()
      : base("HFR")
    {
    }
  }

  public class error : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CCTranStatusCode.error>
  {
    public error()
      : base("ERR")
    {
    }
  }
}
