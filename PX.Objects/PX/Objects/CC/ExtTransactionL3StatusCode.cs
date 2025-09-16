// Decompiled with JetBrains decompiler
// Type: PX.Objects.CC.ExtTransactionL3StatusCode
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.CC;

public static class ExtTransactionL3StatusCode
{
  public const 
  #nullable disable
  string NotApplicable = "NA ";
  public const string Pending = "PEN";
  public const string Sent = "SNT";
  public const string Failed = "FLD";
  public const string Rejected = "REJ";
  public const string ResendRejected = "RRJ";
  public const string ResendFailed = "RFL";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new string[4]{ "PEN", "SNT", "FLD", "RFL" }, new string[4]
      {
        "Pending",
        "Sent",
        "Failed",
        "Resend Failed"
      })
    {
    }
  }

  public class notApplicable : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ExtTransactionL3StatusCode.notApplicable>
  {
    public notApplicable()
      : base("NA ")
    {
    }
  }

  public class pending : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ExtTransactionL3StatusCode.pending>
  {
    public pending()
      : base("PEN")
    {
    }
  }

  public class sent : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ExtTransactionL3StatusCode.sent>
  {
    public sent()
      : base("SNT")
    {
    }
  }

  public class failed : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ExtTransactionL3StatusCode.failed>
  {
    public failed()
      : base("FLD")
    {
    }
  }

  public class rejected : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ExtTransactionL3StatusCode.rejected>
  {
    public rejected()
      : base("REJ")
    {
    }
  }

  public class resendRejected : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ExtTransactionL3StatusCode.resendRejected>
  {
    public resendRejected()
      : base("RRJ")
    {
    }
  }

  public class resendFailed : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ExtTransactionL3StatusCode.resendFailed>
  {
    public resendFailed()
      : base("RFL")
    {
    }
  }
}
