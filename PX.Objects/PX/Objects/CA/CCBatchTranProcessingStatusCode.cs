// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CCBatchTranProcessingStatusCode
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.CA;

public static class CCBatchTranProcessingStatusCode
{
  public const 
  #nullable disable
  string PendingProcessing = "PPR";
  public const string Processed = "PRD";
  public const string Missing = "MIS";
  public const string Hidden = "HID";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new string[4]{ "PPR", "PRD", "MIS", "HID" }, new string[4]
      {
        "Pending Processing",
        "Processed",
        "Missing",
        "Hidden"
      })
    {
    }
  }

  public class pendingProcessing : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    CCBatchTranProcessingStatusCode.pendingProcessing>
  {
    public pendingProcessing()
      : base("PPR")
    {
    }
  }

  public class processed : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    CCBatchTranProcessingStatusCode.processed>
  {
    public processed()
      : base("PRD")
    {
    }
  }

  public class missing : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    CCBatchTranProcessingStatusCode.missing>
  {
    public missing()
      : base("MIS")
    {
    }
  }

  public class hidden : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CCBatchTranProcessingStatusCode.hidden>
  {
    public hidden()
      : base("HID")
    {
    }
  }
}
