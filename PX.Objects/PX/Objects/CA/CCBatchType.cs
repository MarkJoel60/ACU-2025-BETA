// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CCBatchType
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.CCProcessingBase.Interfaces.V2;
using PX.Data;
using PX.Data.BQL;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace PX.Objects.CA;

public static class CCBatchType
{
  public const 
  #nullable disable
  string CCSettlement = "CCS";
  public const string ACHSettlement = "ACS";
  public const string ACHReject = "ACR";
  public const string ACHVoid = "ACV";
  private static readonly Dictionary<BatchType, string> codes = new Dictionary<BatchType, string>()
  {
    {
      (BatchType) 0,
      "CCS"
    },
    {
      (BatchType) 1,
      "ACS"
    },
    {
      (BatchType) 2,
      "ACR"
    },
    {
      (BatchType) 3,
      "ACV"
    }
  };

  public static string GetCode(BatchType batchType)
  {
    string code;
    if (CCBatchType.codes.TryGetValue(batchType, out code))
      return code;
    throw new ArgumentException(nameof (batchType));
  }

  public static BatchType? GetType(string code)
  {
    return new BatchType?(CCBatchType.codes.Where<KeyValuePair<BatchType, string>>((Func<KeyValuePair<BatchType, string>, bool>) (x => x.Value == code)).FirstOrDefault<KeyValuePair<BatchType, string>>().Key);
  }

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new string[4]{ "CCS", "ACS", "ACR", "ACV" }, new string[4]
      {
        "Credit Card Settlement",
        "ACH Settlement",
        "ACH Reject",
        "ACH Void"
      })
    {
    }
  }

  public class ccSettlement : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CCBatchType.ccSettlement>
  {
    public ccSettlement()
      : base("CCS")
    {
    }
  }

  public class achSettlement : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CCBatchType.achSettlement>
  {
    public achSettlement()
      : base("ACS")
    {
    }
  }

  public class achReject : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CCBatchType.achReject>
  {
    public achReject()
      : base("ACR")
    {
    }
  }

  public class achVoid : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CCBatchType.achVoid>
  {
    public achVoid()
      : base("ACV")
    {
    }
  }
}
