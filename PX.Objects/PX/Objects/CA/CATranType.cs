// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CATranType
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data.BQL;
using PX.Objects.Common;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace PX.Objects.CA;

public class CATranType : ILabelProvider
{
  protected static readonly 
  #nullable disable
  ValueLabelList ValueLabelList = new ValueLabelList()
  {
    {
      "CTO",
      "Transfer Out"
    },
    {
      "CTI",
      "Transfer In"
    },
    {
      "CTE",
      "Expense Entry"
    },
    {
      "CAE",
      "Cash Entry"
    }
  };
  protected static readonly ValueLabelList DepositValueLabelList = new ValueLabelList()
  {
    {
      "CDT",
      "CA Deposit"
    },
    {
      "CVD",
      "CA Void Deposit"
    }
  };
  protected static readonly ValueLabelList FullValueLabelList = new ValueLabelList()
  {
    (IEnumerable<ValueLabelPair>) CATranType.ValueLabelList,
    (IEnumerable<ValueLabelPair>) CATranType.DepositValueLabelList,
    {
      "CDX",
      "CA Deposit"
    },
    {
      "CVX",
      "CA Void Deposit"
    },
    {
      "CT%",
      "Transfer"
    }
  };
  protected static readonly ValueLabelList FullValueLabelListUI = new ValueLabelList()
  {
    (IEnumerable<ValueLabelPair>) CATranType.ValueLabelList,
    (IEnumerable<ValueLabelPair>) CATranType.DepositValueLabelList
  };
  public static readonly string[] Values = CATranType.ValueLabelList.Select<ValueLabelPair, string>((Func<ValueLabelPair, string>) (pair => pair.Value)).ToArray<string>();
  public static readonly string[] Labels = CATranType.ValueLabelList.Select<ValueLabelPair, string>((Func<ValueLabelPair, string>) (pair => pair.Label)).ToArray<string>();
  public const string CATransferOut = "CTO";
  public const string CATransferIn = "CTI";
  public const string CATransferExp = "CTE";
  public const string CATransferRGOL = "CTG";
  public const string CAAdjustment = "CAE";
  public const string CAAdjustmentRGOL = "CAG";
  public const string CADeposit = "CDT";
  public const string CAVoidDeposit = "CVD";
  public const string CACashDropTransaction = "CDX";
  public const string CACashDropVoidTransaction = "CVX";
  public const string CABatch = "CBT";
  public const string CATransfer = "CT%";

  public virtual IEnumerable<ValueLabelPair> ValueLabelPairs
  {
    get => (IEnumerable<ValueLabelPair>) CATranType.ValueLabelList;
  }

  public IEnumerable<ValueLabelPair> FullValueLabelPairs
  {
    get => (IEnumerable<ValueLabelPair>) CATranType.FullValueLabelList;
  }

  public IEnumerable<ValueLabelPair> FullValueLabelPairsUI
  {
    get => (IEnumerable<ValueLabelPair>) CATranType.FullValueLabelListUI;
  }

  public static bool IsTransfer(string tranType) => tranType == "CTO" || tranType == "CTI";

  public class ListAttribute : LabelListAttribute
  {
    public ListAttribute()
      : base((IEnumerable<ValueLabelPair>) CATranType.ValueLabelList)
    {
    }
  }

  /// <summary>
  /// Selector. Defines a list of possible CADeposit types - namely <br />
  /// CADeposit and CAVoidDeposit <br />
  /// <example>
  /// [CATranType.DepositList()]
  /// </example>
  /// </summary>
  public class DepositListAttribute : LabelListAttribute
  {
    public DepositListAttribute()
      : base((IEnumerable<ValueLabelPair>) CATranType.DepositValueLabelList)
    {
    }
  }

  public class cATransferOut : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CATranType.cATransferOut>
  {
    public cATransferOut()
      : base("CTO")
    {
    }
  }

  public class cATransferIn : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CATranType.cATransferIn>
  {
    public cATransferIn()
      : base("CTI")
    {
    }
  }

  public class cATransferExp : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CATranType.cATransferExp>
  {
    public cATransferExp()
      : base("CTE")
    {
    }
  }

  public class cAAdjustment : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CATranType.cAAdjustment>
  {
    public cAAdjustment()
      : base("CAE")
    {
    }
  }

  public class cADeposit : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CATranType.cADeposit>
  {
    public cADeposit()
      : base("CDT")
    {
    }
  }

  public class cAVoidDeposit : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CATranType.cAVoidDeposit>
  {
    public cAVoidDeposit()
      : base("CVD")
    {
    }
  }

  public class cACashDropTransaction : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    CATranType.cACashDropTransaction>
  {
    public cACashDropTransaction()
      : base("CDX")
    {
    }
  }

  public class cACashDropVoidTransaction : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    CATranType.cACashDropVoidTransaction>
  {
    public cACashDropVoidTransaction()
      : base("CVX")
    {
    }
  }

  public class cABatch : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CATranType.cABatch>
  {
    public cABatch()
      : base("CBT")
    {
    }
  }

  public class cATransfer : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  CATranType.cATransfer>
  {
    public cATransfer()
      : base("CT%")
    {
    }
  }
}
