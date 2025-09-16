// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.PaymentProcessor.UserStatus
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.AP.PaymentProcessor;

public class UserStatus
{
  public const 
  #nullable disable
  string Onboarded = "O";
  public const string OnboardRequired = "R";
  public const string Deactivated = "D";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(UserStatus.ListAttribute.GetStatuses)
    {
    }

    public static (string, string)[] GetStatuses
    {
      get
      {
        return new (string, string)[3]
        {
          ("O", "Active"),
          ("R", "Pending Onboarding"),
          ("D", "Inactive")
        };
      }
    }
  }

  public class onboarded : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  UserStatus.onboarded>
  {
    public onboarded()
      : base("O")
    {
    }
  }

  public class onboardRequired : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  UserStatus.onboardRequired>
  {
    public onboardRequired()
      : base("R")
    {
    }
  }

  public class deactivated : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  UserStatus.deactivated>
  {
    public deactivated()
      : base("D")
    {
    }
  }
}
