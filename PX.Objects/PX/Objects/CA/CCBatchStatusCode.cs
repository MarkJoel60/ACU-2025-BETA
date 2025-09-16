// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CCBatchStatusCode
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.CA;

public static class CCBatchStatusCode
{
  public const 
  #nullable disable
  string PendingImport = "PIM";
  public const string PendingProcessing = "PPR";
  public const string Processing = "PRG";
  public const string PendingReview = "PRV";
  public const string Processed = "PRD";
  public const string Deposited = "DPD";
  public const string Error = "ERR";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new string[7]
      {
        "PIM",
        "PPR",
        "PRG",
        "PRV",
        "PRD",
        "DPD",
        "ERR"
      }, new string[7]
      {
        "Pending Import",
        "Pending Processing",
        "Processing",
        "Pending Review",
        "Processed",
        "Deposited",
        "Error"
      })
    {
    }
  }

  public class pendingImport : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CCBatchStatusCode.pendingImport>
  {
    public pendingImport()
      : base("PIM")
    {
    }
  }

  public class pendingProcessing : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    CCBatchStatusCode.pendingProcessing>
  {
    public pendingProcessing()
      : base("PPR")
    {
    }
  }

  public class processing : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CCBatchStatusCode.processing>
  {
    public processing()
      : base("PRG")
    {
    }
  }

  public class pendingReview : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CCBatchStatusCode.pendingReview>
  {
    public pendingReview()
      : base("PRV")
    {
    }
  }

  public class processed : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CCBatchStatusCode.processed>
  {
    public processed()
      : base("PRD")
    {
    }
  }

  public class deposited : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CCBatchStatusCode.deposited>
  {
    public deposited()
      : base("DPD")
    {
    }
  }

  public class error : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CCBatchStatusCode.error>
  {
    public error()
      : base("ERR")
    {
    }
  }
}
