// Decompiled with JetBrains decompiler
// Type: PX.Objects.CC.PayLinkProcessingAction
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.CC;

public static class PayLinkProcessingAction
{
  public const string Create = "C";
  public const string Sync = "S";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(PayLinkProcessingAction.ListAttribute.ValueLabelPairs())
    {
    }

    public static Tuple<string, string>[] ValueLabelPairs()
    {
      return new Tuple<string, string>[2]
      {
        new Tuple<string, string>("C", "Create Payment Link"),
        new Tuple<string, string>("S", "Sync Payment Link")
      };
    }
  }
}
