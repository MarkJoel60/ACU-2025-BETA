// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARStatementScheduleType
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.Common;
using System;
using System.Collections.Generic;

#nullable enable
namespace PX.Objects.AR;

public class ARStatementScheduleType : ILabelProvider
{
  public const 
  #nullable disable
  string Weekly = "W";
  public const string TwiceAMonth = "C";
  public const string FixedDayOfMonth = "F";
  public const string EndOfMonth = "E";
  public const string EndOfPeriod = "P";
  [Obsolete("This constant is not used anymore and will be removed in Acumatica ERP 8.0. Please use TwiceAMonthinstead.")]
  public const string Custom = "C";

  public IEnumerable<ValueLabelPair> ValueLabelPairs
  {
    get
    {
      return (IEnumerable<ValueLabelPair>) new ValueLabelList()
      {
        {
          "W",
          "Weekly"
        },
        {
          "C",
          "Twice a Month"
        },
        {
          "F",
          "Fixed Day of Month"
        },
        {
          "E",
          "End of Month"
        },
        {
          "P",
          "End of Financial Period"
        }
      };
    }
  }

  public class weekly : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ARStatementScheduleType.weekly>
  {
    public weekly()
      : base("W")
    {
    }
  }

  public class twiceAMonth : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ARStatementScheduleType.twiceAMonth>
  {
    public twiceAMonth()
      : base("C")
    {
    }
  }

  public class fixedDayOfMonth : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ARStatementScheduleType.fixedDayOfMonth>
  {
    public fixedDayOfMonth()
      : base("F")
    {
    }
  }

  public class endOfMonth : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ARStatementScheduleType.endOfMonth>
  {
    public endOfMonth()
      : base("E")
    {
    }
  }

  public class endOfPeriod : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ARStatementScheduleType.endOfPeriod>
  {
    public endOfPeriod()
      : base("P")
    {
    }
  }

  [Obsolete("This attribute is not used anymore and will be removed in Acumatica ERP 8.0. Please use LabelListAttribute instead.")]
  public class ListAttribute : PXStringListAttribute
  {
  }
}
