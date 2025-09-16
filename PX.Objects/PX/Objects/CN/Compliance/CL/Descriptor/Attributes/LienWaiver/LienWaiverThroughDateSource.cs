// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Compliance.CL.Descriptor.Attributes.LienWaiver.LienWaiverThroughDateSource
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.CN.Compliance.CL.Descriptor.Attributes.LienWaiver;

public class LienWaiverThroughDateSource
{
  public const 
  #nullable disable
  string BillDate = "AP Bill Date";
  public const string PostingPeriodEndDate = "Posting Period End Date";
  public const string PaymentDate = "AP Check Date";
  private static readonly string[] ThroughDateSources = new string[3]
  {
    "AP Bill Date",
    "Posting Period End Date",
    "AP Check Date"
  };
  private static readonly string[] ThroughDateSourcesLabels = new string[3]
  {
    "Bill Date",
    "Posting Period End Date",
    "AP Payment Date"
  };

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(LienWaiverThroughDateSource.ThroughDateSources, LienWaiverThroughDateSource.ThroughDateSourcesLabels)
    {
    }
  }

  public sealed class billDate : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    LienWaiverThroughDateSource.billDate>
  {
    public billDate()
      : base("AP Bill Date")
    {
    }
  }

  public sealed class postingPeriodEndDate : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    LienWaiverThroughDateSource.postingPeriodEndDate>
  {
    public postingPeriodEndDate()
      : base("Posting Period End Date")
    {
    }
  }

  public sealed class paymentDate : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    LienWaiverThroughDateSource.paymentDate>
  {
    public paymentDate()
      : base("AP Check Date")
    {
    }
  }
}
