// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.PaymentProcessor.AccountUserStatus
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.AP.PaymentProcessor;

public class AccountUserStatus
{
  public const string Pending = "P";
  public const string Nominated = "N";
  public const string Verified = "V";
  public const string Disabled = "D";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(AccountUserStatus.ListAttribute.GetStatuses)
    {
    }

    public static (string, string)[] GetStatuses
    {
      get
      {
        return new (string, string)[4]
        {
          ("P", "Pending"),
          ("N", "Nominated"),
          ("V", "Verified"),
          ("D", "Disabled")
        };
      }
    }
  }
}
