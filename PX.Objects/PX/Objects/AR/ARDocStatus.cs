// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARDocStatus
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data.BQL;
using PX.Objects.Common;
using System.Collections.Generic;

#nullable enable
namespace PX.Objects.AR;

/// <exclude />
public class ARDocStatus : ILabelProvider
{
  private static readonly 
  #nullable disable
  IEnumerable<ValueLabelPair> _valueLabelPairs = (IEnumerable<ValueLabelPair>) new ValueLabelList()
  {
    {
      "I",
      nameof (Incomplete)
    },
    {
      "R",
      "Credit Hold"
    },
    {
      "W",
      "Pending Processing"
    },
    {
      "H",
      "On Hold"
    },
    {
      "B",
      nameof (Balanced)
    },
    {
      "V",
      nameof (Voided)
    },
    {
      "S",
      nameof (Scheduled)
    },
    {
      "N",
      nameof (Open)
    },
    {
      "C",
      nameof (Closed)
    },
    {
      "P",
      "Pending Print"
    },
    {
      "E",
      "Pending Email"
    },
    {
      "Z",
      nameof (Reserved)
    },
    {
      "D",
      "Pending Approval"
    },
    {
      "J",
      nameof (Rejected)
    },
    {
      "L",
      nameof (Canceled)
    },
    {
      "Y",
      "Pending Payment"
    },
    {
      "U",
      nameof (Unapplied)
    }
  };
  public static readonly string[] Values = new string[16 /*0x10*/]
  {
    "R",
    "W",
    "H",
    "B",
    "V",
    "S",
    "N",
    "C",
    "P",
    "E",
    "Z",
    "D",
    "J",
    "L",
    "Y",
    "U"
  };
  public static readonly string[] Labels = new string[16 /*0x10*/]
  {
    "Credit Hold",
    "Pending Processing",
    "On Hold",
    nameof (Balanced),
    nameof (Voided),
    nameof (Scheduled),
    nameof (Open),
    nameof (Closed),
    "Pending Print",
    "Pending Email",
    nameof (Reserved),
    "Pending Approval",
    nameof (Rejected),
    nameof (Canceled),
    "Pending Payment",
    nameof (Unapplied)
  };
  public const string Incomplete = "I";
  public const string Hold = "H";
  public const string Balanced = "B";
  public const string Voided = "V";
  public const string Scheduled = "S";
  public const string Open = "N";
  public const string Closed = "C";
  public const string PendingPrint = "P";
  public const string PendingEmail = "E";
  public const string CreditHold = "R";
  public const string CCHold = "W";
  public const string Reserved = "Z";
  public const string PendingApproval = "D";
  public const string Rejected = "J";
  public const string Canceled = "L";
  public const string PendingPayment = "Y";
  public const string Unapplied = "U";

  public IEnumerable<ValueLabelPair> ValueLabelPairs => ARDocStatus._valueLabelPairs;

  public class ListAttribute : LabelListAttribute
  {
    public ListAttribute()
      : base(ARDocStatus._valueLabelPairs)
    {
    }
  }

  public class incomplete : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ARDocStatus.incomplete>
  {
    public incomplete()
      : base("I")
    {
    }
  }

  public class hold : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ARDocStatus.hold>
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
  ARDocStatus.balanced>
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
  ARDocStatus.voided>
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
  ARDocStatus.scheduled>
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
  ARDocStatus.open>
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
  ARDocStatus.closed>
  {
    public closed()
      : base("C")
    {
    }
  }

  public class pendingPrint : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ARDocStatus.pendingPrint>
  {
    public pendingPrint()
      : base("P")
    {
    }
  }

  public class pendingEmail : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ARDocStatus.pendingEmail>
  {
    public pendingEmail()
      : base("E")
    {
    }
  }

  public class cCHold : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ARDocStatus.cCHold>
  {
    public cCHold()
      : base("W")
    {
    }
  }

  public class creditHold : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ARDocStatus.creditHold>
  {
    public creditHold()
      : base("R")
    {
    }
  }

  public class reserved : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ARDocStatus.reserved>
  {
    public reserved()
      : base("Z")
    {
    }
  }

  public class pendingApproval : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ARDocStatus.pendingApproval>
  {
    public pendingApproval()
      : base("D")
    {
    }
  }

  public class rejected : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ARDocStatus.rejected>
  {
    public rejected()
      : base("J")
    {
    }
  }

  public class canceled : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ARDocStatus.canceled>
  {
    public canceled()
      : base("L")
    {
    }
  }

  public class HoldToBalance : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ARDocStatus.HoldToBalance>
  {
    public HoldToBalance()
      : base("H->B")
    {
    }
  }

  public class pendingPayment : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ARDocStatus.pendingPayment>
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
  ARDocStatus.unapplied>
  {
    public unapplied()
      : base("U")
    {
    }
  }
}
