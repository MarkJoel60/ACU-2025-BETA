// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.SavePaymentProfileCode
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.AR;

internal static class SavePaymentProfileCode
{
  public const string Allow = "A";
  public const string Force = "F";
  public const string Prohibit = "P";

  public static Tuple<string, string>[] ValueLabelPairs()
  {
    return new Tuple<string, string>[3]
    {
      new Tuple<string, string>("A", "Upon Confirmation"),
      new Tuple<string, string>("F", "Always"),
      new Tuple<string, string>("P", "Never")
    };
  }

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(SavePaymentProfileCode.ValueLabelPairs())
    {
    }
  }
}
