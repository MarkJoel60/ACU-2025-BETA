// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CCBatchTranSettlementStatusCode
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.CCProcessingBase.Interfaces.V2;
using PX.Data;
using PX.Data.BQL;
using System;
using System.Collections.Generic;

#nullable enable
namespace PX.Objects.CA;

public static class CCBatchTranSettlementStatusCode
{
  public const 
  #nullable disable
  string SettledSuccessfully = "SSC";
  public const string SettlementError = "SER";
  public const string Voided = "VOI";
  public const string RefundVoided = "RVO";
  public const string Declined = "DEC";
  public const string RefundSettledSuccessfully = "RSS";
  public const string GeneralError = "ERR";
  public const string Expired = "EXP";
  public const string Unknown = "UNK";
  public const string Rejected = "REJ";
  public const string RefundRejected = "RRJ";
  private static readonly Dictionary<CCTranStatus, string> codes = new Dictionary<CCTranStatus, string>()
  {
    {
      (CCTranStatus) 6,
      "SSC"
    },
    {
      (CCTranStatus) 10,
      "SER"
    },
    {
      (CCTranStatus) 7,
      "VOI"
    },
    {
      (CCTranStatus) 8,
      "RVO"
    },
    {
      (CCTranStatus) 1,
      "DEC"
    },
    {
      (CCTranStatus) 9,
      "RSS"
    },
    {
      (CCTranStatus) 11,
      "ERR"
    },
    {
      (CCTranStatus) 4,
      "EXP"
    },
    {
      (CCTranStatus) 5,
      "UNK"
    },
    {
      (CCTranStatus) 12,
      "REJ"
    },
    {
      (CCTranStatus) 13,
      "RRJ"
    }
  };

  public static string GetCode(CCTranStatus tranType)
  {
    string code;
    if (CCBatchTranSettlementStatusCode.codes.TryGetValue(tranType, out code))
      return code;
    throw new ArgumentException(nameof (tranType));
  }

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new string[11]
      {
        "SSC",
        "SER",
        "VOI",
        "RVO",
        "DEC",
        "RSS",
        "ERR",
        "EXP",
        "UNK",
        "REJ",
        "RRJ"
      }, new string[11]
      {
        "Settled Successfully",
        "Settlement Error",
        "Voided",
        "Refund Voided",
        "Declined",
        "Refund Settled Successfully",
        "General Error",
        "Expired",
        "Unknown",
        "Rejected",
        "Refund Rejected"
      })
    {
    }
  }

  public class settledSuccessfully : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    CCBatchTranSettlementStatusCode.settledSuccessfully>
  {
    public settledSuccessfully()
      : base("SSC")
    {
    }
  }

  public class voided : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CCBatchTranSettlementStatusCode.voided>
  {
    public voided()
      : base("VOI")
    {
    }
  }

  public class declined : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    CCBatchTranSettlementStatusCode.declined>
  {
    public declined()
      : base("DEC")
    {
    }
  }

  public class refundSettledSuccessfully : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    CCBatchTranSettlementStatusCode.refundSettledSuccessfully>
  {
    public refundSettledSuccessfully()
      : base("RSS")
    {
    }
  }

  public class generalError : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    CCBatchTranSettlementStatusCode.generalError>
  {
    public generalError()
      : base("ERR")
    {
    }
  }

  public class expired : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    CCBatchTranSettlementStatusCode.expired>
  {
    public expired()
      : base("EXP")
    {
    }
  }

  public class unknown : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    CCBatchTranSettlementStatusCode.unknown>
  {
    public unknown()
      : base("UNK")
    {
    }
  }
}
