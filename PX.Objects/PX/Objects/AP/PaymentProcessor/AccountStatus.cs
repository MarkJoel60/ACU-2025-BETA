// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.PaymentProcessor.AccountStatus
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

public class AccountStatus
{
  public const string Pending = "P";
  public const string Active = "A";
  public const string Invalid = "I";
  public const string Expired = "E";
  public const string Disabled = "D";
  public const string Blocked = "B";
  public const string Unknown = "U";
  private static (BankAccountStatus bankAccountStatus, string statusAsString)[] statusMapping = new (BankAccountStatus, string)[7]
  {
    ((BankAccountStatus) 0, "P"),
    ((BankAccountStatus) 1, "A"),
    ((BankAccountStatus) 2, "I"),
    ((BankAccountStatus) 3, "E"),
    ((BankAccountStatus) 4, "D"),
    ((BankAccountStatus) 5, "B"),
    ((BankAccountStatus) 6, "U")
  };

  public static string GetStatusAsString(BankAccountStatus status)
  {
    return ((IEnumerable<(BankAccountStatus, string)>) AccountStatus.statusMapping).Where<(BankAccountStatus, string)>((Func<(BankAccountStatus, string), bool>) (i => i.bankAccountStatus == status)).Select<(BankAccountStatus, string), string>((Func<(BankAccountStatus, string), string>) (i => i.statusAsString)).FirstOrDefault<string>() ?? "U";
  }

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(AccountStatus.ListAttribute.GetStatuses)
    {
    }

    public static (string, string)[] GetStatuses
    {
      get
      {
        return new (string, string)[7]
        {
          ("P", "Pending"),
          ("A", "Active"),
          ("I", "Invalid"),
          ("E", "Expired"),
          ("D", "Disabled"),
          ("B", "Blocked"),
          ("U", "Unknown")
        };
      }
    }
  }

  public class pending : BqlType<IBqlString, string>.Constant<AccountStatus.pending>
  {
    public pending()
      : base("P")
    {
    }
  }

  public class active : BqlType<IBqlString, string>.Constant<AccountStatus.active>
  {
    public active()
      : base("A")
    {
    }
  }

  public class invalid : BqlType<IBqlString, string>.Constant<AccountStatus.invalid>
  {
    public invalid()
      : base("I")
    {
    }
  }

  public class expired : BqlType<IBqlString, string>.Constant<AccountStatus.expired>
  {
    public expired()
      : base("E")
    {
    }
  }

  public class disabled : BqlType<IBqlString, string>.Constant<AccountStatus.disabled>
  {
    public disabled()
      : base("D")
    {
    }
  }

  public class blocked : BqlType<IBqlString, string>.Constant<AccountStatus.blocked>
  {
    public blocked()
      : base("B")
    {
    }
  }
}
