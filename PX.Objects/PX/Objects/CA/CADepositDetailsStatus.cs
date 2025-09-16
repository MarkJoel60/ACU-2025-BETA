// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CADepositDetailsStatus
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.CA;

public class CADepositDetailsStatus
{
  public static readonly 
  #nullable disable
  string[] Values = new string[13]
  {
    "H",
    "B",
    "V",
    "S",
    "N",
    "C",
    "P",
    "K",
    "E",
    "R",
    "Z",
    "G",
    "D"
  };
  public static readonly string[] Labels = new string[13]
  {
    "On Hold",
    nameof (Balanced),
    nameof (Voided),
    nameof (Scheduled),
    nameof (Open),
    nameof (Closed),
    nameof (Printed),
    "Pre-Released",
    "Pending Approval",
    nameof (Rejected),
    nameof (Reserved),
    "Pending Print",
    nameof (Released)
  };
  public const string Hold = "H";
  public const string Balanced = "B";
  public const string Voided = "V";
  public const string Scheduled = "S";
  public const string Open = "N";
  public const string Closed = "C";
  public const string Printed = "P";
  public const string Prebooked = "K";
  public const string PendingApproval = "E";
  public const string Rejected = "R";
  public const string Reserved = "Z";
  public const string PendingPrint = "G";
  public const string Released = "D";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(CADepositDetailsStatus.Values, CADepositDetailsStatus.Labels)
    {
    }
  }

  public class hold : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CADepositDetailsStatus.hold>
  {
    public hold()
      : base("H")
    {
    }
  }

  public class balanced : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CADepositDetailsStatus.balanced>
  {
    public balanced()
      : base("B")
    {
    }
  }

  public class voided : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CADepositDetailsStatus.voided>
  {
    public voided()
      : base("V")
    {
    }
  }

  public class scheduled : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CADepositDetailsStatus.scheduled>
  {
    public scheduled()
      : base("S")
    {
    }
  }

  public class open : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CADepositDetailsStatus.open>
  {
    public open()
      : base("N")
    {
    }
  }

  public class closed : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CADepositDetailsStatus.closed>
  {
    public closed()
      : base("C")
    {
    }
  }

  public class printed : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CADepositDetailsStatus.printed>
  {
    public printed()
      : base("P")
    {
    }
  }

  public class prebooked : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CADepositDetailsStatus.prebooked>
  {
    public prebooked()
      : base("K")
    {
    }
  }

  public class pendingApproval : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    CADepositDetailsStatus.pendingApproval>
  {
    public pendingApproval()
      : base("E")
    {
    }
  }

  public class rejected : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CADepositDetailsStatus.rejected>
  {
    public rejected()
      : base("R")
    {
    }
  }

  public class reserved : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CADepositDetailsStatus.reserved>
  {
    public reserved()
      : base("Z")
    {
    }
  }

  public class pendingPrint : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    CADepositDetailsStatus.pendingPrint>
  {
    public pendingPrint()
      : base("G")
    {
    }
  }

  public class released : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CADepositDetailsStatus.released>
  {
    public released()
      : base("D")
    {
    }
  }
}
