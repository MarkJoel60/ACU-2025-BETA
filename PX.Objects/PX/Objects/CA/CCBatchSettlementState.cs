// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CCBatchSettlementState
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

public static class CCBatchSettlementState
{
  public const 
  #nullable disable
  string SettledSuccessfully = "SSC";
  public const string SettlementError = "SER";
  public const string PendingSettlement = "SPN";
  public const string Unknown = "UNK";
  public const string Rejected = "REJ";
  public const string Voided = "VOI";
  private static readonly Dictionary<CCBatchState, string> codes = new Dictionary<CCBatchState, string>()
  {
    {
      (CCBatchState) 0,
      "SSC"
    },
    {
      (CCBatchState) 1,
      "SER"
    },
    {
      (CCBatchState) 2,
      "SPN"
    },
    {
      (CCBatchState) 3,
      "UNK"
    },
    {
      (CCBatchState) 4,
      "REJ"
    },
    {
      (CCBatchState) 5,
      "VOI"
    }
  };

  public static string GetCode(CCBatchState batchState)
  {
    string code;
    if (CCBatchSettlementState.codes.TryGetValue(batchState, out code))
      return code;
    throw new ArgumentException(nameof (batchState));
  }

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new string[6]
      {
        "SSC",
        "SER",
        "SPN",
        "UNK",
        "REJ",
        "VOI"
      }, new string[6]
      {
        "Settled Successfully",
        "Settlement Error",
        "Pending Settlement",
        "Unknown",
        "Rejected",
        "Voided"
      })
    {
    }
  }

  public class settledSuccessfully : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    CCBatchSettlementState.settledSuccessfully>
  {
    public settledSuccessfully()
      : base("SSC")
    {
    }
  }

  public class settlementError : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    CCBatchSettlementState.settlementError>
  {
    public settlementError()
      : base("SER")
    {
    }
  }

  public class pendingSettlement : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    CCBatchSettlementState.pendingSettlement>
  {
    public pendingSettlement()
      : base("SPN")
    {
    }
  }

  public class unknown : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CCBatchSettlementState.unknown>
  {
    public unknown()
      : base("UNK")
    {
    }
  }
}
