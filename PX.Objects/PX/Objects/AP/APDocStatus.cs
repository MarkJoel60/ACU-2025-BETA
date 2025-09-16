// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APDocStatus
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.AP;

public class APDocStatus
{
  public static readonly 
  #nullable disable
  string[] Values = new string[16 /*0x10*/]
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
    "X",
    "W",
    "Y",
    "U"
  };
  public static readonly string[] Labels = new string[16 /*0x10*/]
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
    "Under Reclassification",
    "Pending Processing",
    "Pending Payment",
    nameof (Unapplied)
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
  public const string UnderReclassification = "X";
  public const string PendingProcessing = "W";
  public const string PendingPayment = "Y";
  public const string Unapplied = "U";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(APDocStatus.Values, APDocStatus.Labels)
    {
    }
  }

  public class hold : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  APDocStatus.hold>
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
  APDocStatus.balanced>
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
  APDocStatus.voided>
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
  APDocStatus.scheduled>
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
  APDocStatus.open>
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
  APDocStatus.closed>
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
  APDocStatus.printed>
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
  APDocStatus.prebooked>
  {
    public prebooked()
      : base("K")
    {
    }
  }

  public class pendingApproval : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  APDocStatus.pendingApproval>
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
  APDocStatus.rejected>
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
  APDocStatus.reserved>
  {
    public reserved()
      : base("Z")
    {
    }
  }

  public class pendingPrint : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  APDocStatus.pendingPrint>
  {
    public pendingPrint()
      : base("G")
    {
    }
  }

  public class underReclassification : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    APDocStatus.underReclassification>
  {
    public underReclassification()
      : base("X")
    {
    }
  }

  public class HoldToBalance : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  APDocStatus.HoldToBalance>
  {
    public HoldToBalance()
      : base("H->B")
    {
    }
  }

  public class pendingProcessing : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    APDocStatus.pendingProcessing>
  {
    public pendingProcessing()
      : base("W")
    {
    }
  }

  public class pendingPayment : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  APDocStatus.pendingPayment>
  {
    public pendingPayment()
      : base("Y")
    {
    }
  }

  public class unapplied : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  APDocStatus.unapplied>
  {
    public unapplied()
      : base("U")
    {
    }
  }
}
