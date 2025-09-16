// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.FAAveragingConvention
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace PX.Objects.FA;

public class FAAveragingConvention
{
  public static 
  #nullable disable
  Dictionary<object, string[]> DeprMethodDisabledValues = new Dictionary<object, string[]>()
  {
    {
      (object) "DB",
      new string[6]{ "HP", "NP", "MP", "M2", "FQ", "FY" }
    },
    {
      (object) "YD",
      new string[8]
      {
        "HP",
        "NP",
        "MP",
        "M2",
        "FQ",
        "HQ",
        "FY",
        "HY"
      }
    },
    {
      (object) "N1",
      new string[9]
      {
        "HP",
        "NP",
        "MP",
        "M2",
        "FQ",
        "HQ",
        "FY",
        "HY",
        "FD"
      }
    },
    {
      (object) "N2",
      new string[9]
      {
        "HP",
        "NP",
        "MP",
        "M2",
        "FQ",
        "HQ",
        "FY",
        "HY",
        "FD"
      }
    },
    {
      (object) "RD",
      new string[9]
      {
        "HP",
        "NP",
        "MP",
        "M2",
        "FQ",
        "HQ",
        "FY",
        "HY",
        "FP"
      }
    },
    {
      (object) "DP",
      new string[9]
      {
        "HP",
        "NP",
        "MP",
        "M2",
        "FQ",
        "HQ",
        "FY",
        "HY",
        "FP"
      }
    }
  };
  public static Dictionary<object, string[]> FixedLengthPeriodDisabledValues = new Dictionary<object, string[]>()
  {
    {
      (object) true,
      new string[4]{ "MP", "M2", "FQ", "HQ" }
    }
  };
  public static Dictionary<object, string[]> RecordTypeDisabledValues = new Dictionary<object, string[]>()
  {
    {
      (object) "B",
      new string[8]
      {
        "FP",
        "HP",
        "NP",
        "MP",
        "M2",
        "FQ",
        "HQ",
        "FD"
      }
    }
  };
  public const string FullPeriod = "FP";
  public const string HalfPeriod = "HP";
  public const string NextPeriod = "NP";
  public const string FullQuarter = "FQ";
  public const string HalfQuarter = "HQ";
  public const string FullYear = "FY";
  public const string HalfYear = "HY";
  public const string FullDay = "FD";
  public const string ModifiedPeriod = "MP";
  public const string ModifiedPeriod2 = "M2";

  public static void SetAveragingConventionsList<AveragingConventionField>(
    PXCache sender,
    object row,
    params KeyValuePair<object, Dictionary<object, string[]>>[] pars)
    where AveragingConventionField : IBqlField
  {
    Dictionary<string, string> valueLabelDic = new FAAveragingConvention.ListAttribute().ValueLabelDic;
    foreach (KeyValuePair<object, Dictionary<object, string[]>> par in pars)
    {
      object key1 = par.Key;
      Dictionary<object, string[]> dictionary = par.Value;
      string[] strArray;
      if (key1 != null && dictionary.TryGetValue(key1, out strArray))
      {
        foreach (string key2 in strArray)
          valueLabelDic.Remove(key2);
      }
    }
    PXStringListAttribute.SetList<AveragingConventionField>(sender, row, valueLabelDic.Keys.ToArray<string>(), valueLabelDic.Values.ToArray<string>());
  }

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new string[11]
      {
        null,
        "FP",
        "HP",
        "NP",
        "MP",
        "M2",
        "FQ",
        "HQ",
        "FY",
        "HY",
        "FD"
      }, new string[11]
      {
        string.Empty,
        "Full Period",
        "Mid Period",
        "Next Period",
        "Modified Half Period",
        "Modified Half Period 2",
        "Full Quarter",
        "Mid Quarter",
        "Full Year",
        "Mid Year",
        "Full Day"
      })
    {
    }
  }

  public class fullPeriod : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  FAAveragingConvention.fullPeriod>
  {
    public fullPeriod()
      : base("FP")
    {
    }
  }

  public class halfPeriod : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  FAAveragingConvention.halfPeriod>
  {
    public halfPeriod()
      : base("HP")
    {
    }
  }

  public class nextPeriod : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  FAAveragingConvention.nextPeriod>
  {
    public nextPeriod()
      : base("NP")
    {
    }
  }

  public class modifiedPeriod : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    FAAveragingConvention.modifiedPeriod>
  {
    public modifiedPeriod()
      : base("MP")
    {
    }
  }

  public class modifiedPeriod2 : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    FAAveragingConvention.modifiedPeriod2>
  {
    public modifiedPeriod2()
      : base("M2")
    {
    }
  }

  public class fullQuarter : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  FAAveragingConvention.fullQuarter>
  {
    public fullQuarter()
      : base("FQ")
    {
    }
  }

  public class halfQuarter : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  FAAveragingConvention.halfQuarter>
  {
    public halfQuarter()
      : base("HQ")
    {
    }
  }

  public class fullYear : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  FAAveragingConvention.fullYear>
  {
    public fullYear()
      : base("FY")
    {
    }
  }

  public class halfYear : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  FAAveragingConvention.halfYear>
  {
    public halfYear()
      : base("HY")
    {
    }
  }

  public class fullDay : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  FAAveragingConvention.fullDay>
  {
    public fullDay()
      : base("FD")
    {
    }
  }
}
