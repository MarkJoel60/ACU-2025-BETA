// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.CCTranTypeCode
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

public static class CCTranTypeCode
{
  public const 
  #nullable disable
  string Authorize = "AUT";
  public const string AuthorizeAndCapture = "AAC";
  public const string IncreaseAuthorizedAmount = "IAA";
  public const string PriorAuthorizedCapture = "PAC";
  public const string CaptureOnly = "CAP";
  public const string VoidTran = "VDG";
  public const string Credit = "CDT";
  public const string Unknown = "UKN";
  public const string Rejection = "REJ";
  public const string AUTLabel = "Authorize Only";
  public const string AACLabel = "Authorize and Capture";
  public const string IAALabel = "Increase Authorized Amount";
  public const string PACLabel = "Capture Authorized";
  public const string CAPLabel = "Capture Manualy Authorized";
  public const string VDGLabel = "Void";
  public const string CDTLabel = "Refund";
  public const string UKNLabel = "Unknown";
  public const string REJLabel = "Rejection";
  private static (CCTranType, string)[] typeCodes = new (CCTranType, string)[9]
  {
    (CCTranType.AuthorizeAndCapture, "AAC"),
    (CCTranType.AuthorizeOnly, "AUT"),
    (CCTranType.PriorAuthorizedCapture, "PAC"),
    (CCTranType.CaptureOnly, "CAP"),
    (CCTranType.Credit, "CDT"),
    (CCTranType.Void, "VDG"),
    (CCTranType.Unknown, "UKN"),
    (CCTranType.Reject, "REJ"),
    (CCTranType.IncreaseAuthorizedAmount, "IAA")
  };

  public static string GetTypeCode(CCTranType tranType)
  {
    if (!((IEnumerable<(CCTranType, string)>) CCTranTypeCode.typeCodes).Where<(CCTranType, string)>((Func<(CCTranType, string), bool>) (i => i.Item1 == tranType)).Any<(CCTranType, string)>())
      throw new InvalidOperationException();
    return ((IEnumerable<(CCTranType, string)>) CCTranTypeCode.typeCodes).Where<(CCTranType, string)>((Func<(CCTranType, string), bool>) (i => i.Item1 == tranType)).First<(CCTranType, string)>().Item2;
  }

  public static string GetTypeLabel(CCTranType tranType)
  {
    return PXMessages.LocalizeNoPrefix(new CCTranTypeCode.ListAttribute().ValueLabelDic[CCTranTypeCode.GetTypeCode(tranType)]);
  }

  public static CCTranType GetTranTypeByTranTypeStr(string tranTypeStr)
  {
    if (!((IEnumerable<(CCTranType, string)>) CCTranTypeCode.typeCodes).Where<(CCTranType, string)>((Func<(CCTranType, string), bool>) (i => i.Item2 == tranTypeStr)).Any<(CCTranType, string)>())
      throw new PXInvalidOperationException();
    return ((IEnumerable<(CCTranType, string)>) CCTranTypeCode.typeCodes).Where<(CCTranType, string)>((Func<(CCTranType, string), bool>) (i => i.Item2 == tranTypeStr)).First<(CCTranType, string)>().Item1;
  }

  public static bool IsCaptured(CCTranType tranType)
  {
    bool flag = false;
    if (tranType == CCTranType.AuthorizeAndCapture || tranType == CCTranType.CaptureOnly || tranType == CCTranType.PriorAuthorizedCapture)
      flag = true;
    return flag;
  }

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new string[9]
      {
        "AUT",
        "AAC",
        "PAC",
        "CAP",
        "VDG",
        "CDT",
        "UKN",
        "IAA",
        "REJ"
      }, new string[9]
      {
        "Authorize Only",
        "Authorize and Capture",
        "Capture Authorized",
        "Capture Manualy Authorized",
        "Void",
        "Refund",
        "Unknown",
        "Increase Authorized Amount",
        "Rejection"
      })
    {
    }
  }

  public class authorize : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CCTranTypeCode.authorize>
  {
    public authorize()
      : base("AUT")
    {
    }
  }

  public class priorAuthorizedCapture : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    CCTranTypeCode.priorAuthorizedCapture>
  {
    public priorAuthorizedCapture()
      : base("PAC")
    {
    }
  }

  public class authorizeAndCapture : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    CCTranTypeCode.authorizeAndCapture>
  {
    public authorizeAndCapture()
      : base("AAC")
    {
    }
  }

  public class captureOnly : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CCTranTypeCode.captureOnly>
  {
    public captureOnly()
      : base("CAP")
    {
    }
  }

  public class voidTran : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CCTranTypeCode.voidTran>
  {
    public voidTran()
      : base("VDG")
    {
    }
  }

  public class rejectTran : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CCTranTypeCode.rejectTran>
  {
    public rejectTran()
      : base("REJ")
    {
    }
  }

  public class credit : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CCTranTypeCode.credit>
  {
    public credit()
      : base("CDT")
    {
    }
  }
}
