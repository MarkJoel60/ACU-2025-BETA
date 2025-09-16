// Decompiled with JetBrains decompiler
// Type: PX.Objects.CC.PayLinkPaymentStatus
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.CC;

public static class PayLinkPaymentStatus
{
  public const string None = "N";
  public const string Unpaid = "U";
  public const string Paid = "P";
  public const string Incomplete = "I";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(PayLinkPaymentStatus.ListAttribute.ValueLabelPairs())
    {
    }

    public static Tuple<string, string>[] ValueLabelPairs()
    {
      return new Tuple<string, string>[4]
      {
        new Tuple<string, string>("N", "None"),
        new Tuple<string, string>("U", "Unpaid"),
        new Tuple<string, string>("P", "Paid"),
        new Tuple<string, string>("I", "Incomplete")
      };
    }
  }
}
