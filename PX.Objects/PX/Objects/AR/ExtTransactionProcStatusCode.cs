// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ExtTransactionProcStatusCode
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

public static class ExtTransactionProcStatusCode
{
  public const 
  #nullable disable
  string AuthorizeFail = "AUF";
  public const string AuthorizeIncreaseFail = "AIF";
  public const string CaptureFail = "CAF";
  public const string VoidFail = "VDF";
  public const string CreditFail = "CDF";
  public const string AuthorizeSuccess = "AUS";
  public const string CaptureSuccess = "CAS";
  public const string VoidSuccess = "VDS";
  public const string CreditSuccess = "CDS";
  public const string RejectSuccess = "REJ";
  public const string AuthorizeExpired = "AUE";
  public const string CaptureExpired = "CAE";
  public const string AuthorizeHeldForReview = "AUH";
  public const string CaptureHeldForReview = "CAH";
  public const string VoidHeldForReview = "VDH";
  public const string CreditHeldForReview = "CDH";
  public const string AuthorizeDecline = "AUD";
  public const string CaptureDecline = "CAD";
  public const string VoidDecline = "VDD";
  public const string CreditDecline = "CDD";
  public const string Unknown = "UKN";
  private static (ProcessingStatus, string)[] mapping = new (ProcessingStatus, string)[21]
  {
    (ProcessingStatus.Unknown, "UKN"),
    (ProcessingStatus.AuthorizeFail, "AUF"),
    (ProcessingStatus.AuthorizeIncreaseFail, "AIF"),
    (ProcessingStatus.CaptureFail, "CAF"),
    (ProcessingStatus.VoidFail, "VDF"),
    (ProcessingStatus.CreditFail, "CDF"),
    (ProcessingStatus.AuthorizeExpired, "AUE"),
    (ProcessingStatus.CaptureExpired, "CAE"),
    (ProcessingStatus.AuthorizeSuccess, "AUS"),
    (ProcessingStatus.CaptureSuccess, "CAS"),
    (ProcessingStatus.VoidSuccess, "VDS"),
    (ProcessingStatus.CreditSuccess, "CDS"),
    (ProcessingStatus.AuthorizeHeldForReview, "AUH"),
    (ProcessingStatus.CaptureHeldForReview, "CAH"),
    (ProcessingStatus.VoidHeldForReview, "VDH"),
    (ProcessingStatus.CreditHeldForReview, "CDH"),
    (ProcessingStatus.AuthorizeDecline, "AUD"),
    (ProcessingStatus.CaptureDecline, "CAD"),
    (ProcessingStatus.VoidDecline, "VDD"),
    (ProcessingStatus.CreditDecline, "CDD"),
    (ProcessingStatus.RejectSuccess, "REJ")
  };

  public static string GetStatusByTranStatusTranType(string tranStatus, string tranType)
  {
    string tranStatusTranType = "UKN";
    if (tranStatus == "ERR" && tranType != null && tranType.Length == 3)
    {
      switch (tranType[0])
      {
        case 'A':
          switch (tranType)
          {
            case "AUT":
              tranStatusTranType = "AUF";
              goto label_13;
            case "AAC":
              break;
            default:
              goto label_13;
          }
          break;
        case 'C':
          switch (tranType)
          {
            case "CAP":
              break;
            case "CDT":
              tranStatusTranType = "CDF";
              goto label_13;
            default:
              goto label_13;
          }
          break;
        case 'I':
          if (tranType == "IAA")
          {
            tranStatusTranType = "AIF";
            goto label_13;
          }
          goto label_13;
        case 'P':
          if (tranType == "PAC")
            break;
          goto label_13;
        case 'R':
          if (tranType == "REJ")
            goto label_11;
          goto label_13;
        case 'V':
          if (tranType == "VDG")
            goto label_11;
          goto label_13;
        default:
          goto label_13;
      }
      tranStatusTranType = "CAF";
      goto label_13;
label_11:
      tranStatusTranType = "VDF";
    }
label_13:
    if (tranStatus == "HFR" && tranType != null && tranType.Length == 3)
    {
      switch (tranType[0])
      {
        case 'A':
          switch (tranType)
          {
            case "AUT":
              break;
            case "AAC":
              goto label_22;
            default:
              goto label_25;
          }
          break;
        case 'C':
          switch (tranType)
          {
            case "CAP":
              goto label_22;
            case "CDT":
              tranStatusTranType = "CDH";
              goto label_25;
            default:
              goto label_25;
          }
        case 'I':
          if (tranType == "IAA")
            break;
          goto label_25;
        case 'P':
          if (tranType == "PAC")
            goto label_22;
          goto label_25;
        case 'R':
          if (tranType == "REJ")
            goto label_23;
          goto label_25;
        case 'V':
          if (tranType == "VDG")
            goto label_23;
          goto label_25;
        default:
          goto label_25;
      }
      tranStatusTranType = "AUH";
      goto label_25;
label_22:
      tranStatusTranType = "CAH";
      goto label_25;
label_23:
      tranStatusTranType = "VDH";
    }
label_25:
    if (tranStatus == "APR" && tranType != null && tranType.Length == 3)
    {
      switch (tranType[0])
      {
        case 'A':
          switch (tranType)
          {
            case "AUT":
              break;
            case "AAC":
              goto label_34;
            default:
              goto label_38;
          }
          break;
        case 'C':
          switch (tranType)
          {
            case "CAP":
              goto label_34;
            case "CDT":
              tranStatusTranType = "CDS";
              goto label_38;
            default:
              goto label_38;
          }
        case 'I':
          if (tranType == "IAA")
            break;
          goto label_38;
        case 'P':
          if (tranType == "PAC")
            goto label_34;
          goto label_38;
        case 'R':
          if (tranType == "REJ")
          {
            tranStatusTranType = "REJ";
            goto label_38;
          }
          goto label_38;
        case 'V':
          if (tranType == "VDG")
          {
            tranStatusTranType = "VDS";
            goto label_38;
          }
          goto label_38;
        default:
          goto label_38;
      }
      tranStatusTranType = "AUS";
      goto label_38;
label_34:
      tranStatusTranType = "CAS";
    }
label_38:
    if (tranStatus == "DEC" && tranType != null && tranType.Length == 3)
    {
      switch (tranType[0])
      {
        case 'A':
          switch (tranType)
          {
            case "AUT":
              break;
            case "AAC":
              goto label_46;
            default:
              goto label_49;
          }
          break;
        case 'C':
          switch (tranType)
          {
            case "CAP":
              goto label_46;
            case "CDT":
              tranStatusTranType = "CDD";
              goto label_49;
            default:
              goto label_49;
          }
        case 'I':
          if (tranType == "IAA")
            break;
          goto label_49;
        case 'P':
          if (tranType == "PAC")
            goto label_46;
          goto label_49;
        case 'V':
          if (tranType == "VDG")
          {
            tranStatusTranType = "VDD";
            goto label_49;
          }
          goto label_49;
        default:
          goto label_49;
      }
      tranStatusTranType = "AUD";
      goto label_49;
label_46:
      tranStatusTranType = "CAD";
    }
label_49:
    if (tranStatus == "EXP")
    {
      switch (tranType)
      {
        case "IAA":
        case "AUT":
          tranStatusTranType = "AUE";
          break;
        case "AAC":
          tranStatusTranType = "CAE";
          break;
      }
    }
    return tranStatusTranType;
  }

  public static ProcessingStatus GetProcessingStatusByProcStatusStr(string procStatusCode)
  {
    if (!((IEnumerable<(ProcessingStatus, string)>) ExtTransactionProcStatusCode.mapping).Where<(ProcessingStatus, string)>((Func<(ProcessingStatus, string), bool>) (i => i.Item2 == procStatusCode)).Any<(ProcessingStatus, string)>())
      throw new PXInvalidOperationException();
    return ((IEnumerable<(ProcessingStatus, string)>) ExtTransactionProcStatusCode.mapping).Where<(ProcessingStatus, string)>((Func<(ProcessingStatus, string), bool>) (i => i.Item2 == procStatusCode)).Select<(ProcessingStatus, string), ProcessingStatus>((Func<(ProcessingStatus, string), ProcessingStatus>) (i => i.Item1)).First<ProcessingStatus>();
  }

  public static string GetProcStatusStrByProcessingStatus(ProcessingStatus procStatus)
  {
    return ((IEnumerable<(ProcessingStatus, string)>) ExtTransactionProcStatusCode.mapping).Where<(ProcessingStatus, string)>((Func<(ProcessingStatus, string), bool>) (i => i.Item1 == procStatus)).Select<(ProcessingStatus, string), string>((Func<(ProcessingStatus, string), string>) (i => i.Item2)).First<string>();
  }

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new string[21]
      {
        "AUF",
        "CAF",
        "VDF",
        "CDF",
        "AIF",
        "AUS",
        "CAS",
        "VDS",
        "CDS",
        "REJ",
        "AUH",
        "CAH",
        "VDH",
        "CDH",
        "AUD",
        "CAD",
        "VDD",
        "CDD",
        "AUE",
        "CAE",
        "UKN"
      }, new string[21]
      {
        "Pre-Authorization Failed",
        "Capture Failed",
        "Voiding failed",
        "Refund Failed",
        "Pre-Authorization Increase Failed",
        "Pre-Authorized",
        "Captured",
        "Voided",
        "Refunded",
        "Rejected",
        "Held for Review (Authorization)",
        "Held for Review (Capture)",
        "Held for Review (Void)",
        "Held for Review (Refund)",
        "Pre-Authorization Declined",
        "Capture Declined",
        "Voiding Declined",
        "Refund Declined",
        "Pre-Authorization Expired",
        "Held for Review (Capture) Expired",
        "Unknown"
      })
    {
    }
  }

  public class captureSuccess : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ExtTransactionProcStatusCode.captureSuccess>
  {
    public captureSuccess()
      : base("CAS")
    {
    }
  }

  public class authorizeSuccess : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ExtTransactionProcStatusCode.authorizeSuccess>
  {
    public authorizeSuccess()
      : base("AUS")
    {
    }
  }

  public class authorizeHeldForReview : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ExtTransactionProcStatusCode.authorizeHeldForReview>
  {
    public authorizeHeldForReview()
      : base("AUH")
    {
    }
  }

  public class captureHeldForReview : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ExtTransactionProcStatusCode.captureHeldForReview>
  {
    public captureHeldForReview()
      : base("CAH")
    {
    }
  }

  public class creditHeldForReview : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ExtTransactionProcStatusCode.creditHeldForReview>
  {
    public creditHeldForReview()
      : base("CDH")
    {
    }
  }

  public class voidHeldForReview : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ExtTransactionProcStatusCode.voidHeldForReview>
  {
    public voidHeldForReview()
      : base("VDH")
    {
    }
  }

  public class voidFailed : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ExtTransactionProcStatusCode.voidFailed>
  {
    public voidFailed()
      : base("VDF")
    {
    }
  }

  public class captureFailed : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ExtTransactionProcStatusCode.captureFailed>
  {
    public captureFailed()
      : base("CAF")
    {
    }
  }

  public class voidDeclined : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ExtTransactionProcStatusCode.voidDeclined>
  {
    public voidDeclined()
      : base("VDD")
    {
    }
  }

  public class captureDeclined : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ExtTransactionProcStatusCode.captureDeclined>
  {
    public captureDeclined()
      : base("CAD")
    {
    }
  }

  public class unknown : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ExtTransactionProcStatusCode.unknown>
  {
    public unknown()
      : base("UKN")
    {
    }
  }

  public class rejectSuccess : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ExtTransactionProcStatusCode.rejectSuccess>
  {
    public rejectSuccess()
      : base("REJ")
    {
    }
  }
}
