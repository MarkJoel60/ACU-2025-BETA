// Decompiled with JetBrains decompiler
// Type: PX.Objects.DR.DeferredMethodType
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data.BQL;
using PX.Objects.Common;
using System.Collections.Generic;

#nullable enable
namespace PX.Objects.DR;

public class DeferredMethodType : ILabelProvider
{
  private static readonly 
  #nullable disable
  IEnumerable<ValueLabelPair> _valueLabelPairs = (IEnumerable<ValueLabelPair>) new ValueLabelList()
  {
    {
      "E",
      "Evenly by Periods"
    },
    {
      "P",
      "Evenly by Periods, Prorate by Days"
    },
    {
      "D",
      "Evenly by Days in Period"
    },
    {
      "F",
      "Flexible by Periods, Prorate by Days"
    },
    {
      "L",
      "Flexible by Days in Period"
    },
    {
      "C",
      "On Payment"
    }
  };
  public const string EvenPeriods = "E";
  public const string ProrateDays = "P";
  public const string ExactDays = "D";
  public const string FlexibleProrateDays = "F";
  public const string FlexibleExactDays = "L";
  public const string CashReceipt = "C";

  public IEnumerable<ValueLabelPair> ValueLabelPairs => DeferredMethodType._valueLabelPairs;

  public static bool RequiresTerms(string method) => method == "L" || method == "F";

  public static bool RequiresTerms(DRDeferredCode code)
  {
    return code != null && DeferredMethodType.RequiresTerms(code.Method);
  }

  public class EvenPeriodMethod : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    DeferredMethodType.EvenPeriodMethod>
  {
    public EvenPeriodMethod()
      : base("E")
    {
    }
  }

  public class ProrateDaysMethod : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    DeferredMethodType.ProrateDaysMethod>
  {
    public ProrateDaysMethod()
      : base("P")
    {
    }
  }

  public class ExactDaysMethod : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    DeferredMethodType.ExactDaysMethod>
  {
    public ExactDaysMethod()
      : base("D")
    {
    }
  }

  public class cashReceipt : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  DeferredMethodType.cashReceipt>
  {
    public cashReceipt()
      : base("C")
    {
    }
  }

  public class flexibleProrateDays : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    DeferredMethodType.flexibleProrateDays>
  {
    public flexibleProrateDays()
      : base("F")
    {
    }
  }

  public class flexibleExactDays : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    DeferredMethodType.flexibleExactDays>
  {
    public flexibleExactDays()
      : base("L")
    {
    }
  }
}
