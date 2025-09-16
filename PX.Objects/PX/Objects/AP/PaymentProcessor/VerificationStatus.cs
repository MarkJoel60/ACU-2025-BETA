// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.PaymentProcessor.VerificationStatus
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.PaymentProcessor.Data;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace PX.Objects.AP.PaymentProcessor;

public class VerificationStatus
{
  public const 
  #nullable disable
  string Unverified = "U";
  public const string Nominated = "N";
  public const string Verified = "V";
  public const string Undefined = "D";
  private static (VerificationStatus userVerificationStatus, string statusAsString)[] statusMapping = new (VerificationStatus, string)[4]
  {
    ((VerificationStatus) 0, "U"),
    ((VerificationStatus) 1, "N"),
    ((VerificationStatus) 2, "V"),
    ((VerificationStatus) 3, "D")
  };

  public static string GetStatusAsString(VerificationStatus status)
  {
    return ((IEnumerable<(VerificationStatus, string)>) VerificationStatus.statusMapping).Where<(VerificationStatus, string)>((Func<(VerificationStatus, string), bool>) (i => i.userVerificationStatus == status)).Select<(VerificationStatus, string), string>((Func<(VerificationStatus, string), string>) (i => i.statusAsString)).FirstOrDefault<string>() ?? "D";
  }

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(VerificationStatus.ListAttribute.GetStatuses)
    {
    }

    public static (string, string)[] GetStatuses
    {
      get
      {
        return new (string, string)[4]
        {
          ("U", "Unverified"),
          ("N", "Nominated"),
          ("V", "Verified"),
          ("D", "Undefined")
        };
      }
    }
  }

  public class undefined : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  VerificationStatus.undefined>
  {
    public undefined()
      : base("D")
    {
    }
  }
}
