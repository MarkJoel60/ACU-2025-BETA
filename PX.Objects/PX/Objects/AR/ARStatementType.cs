// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARStatementType
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data.BQL;
using PX.Objects.Common;
using System.Collections.Generic;

#nullable enable
namespace PX.Objects.AR;

public class ARStatementType : ILabelProvider
{
  public const 
  #nullable disable
  string OpenItem = "O";
  public const string BalanceBroughtForward = "B";
  public const string CS_OPEN_ITEM = "O";
  public const string CS_BALANCE_BROUGHT_FORWARD = "B";

  public IEnumerable<ValueLabelPair> ValueLabelPairs
  {
    get
    {
      return (IEnumerable<ValueLabelPair>) new ValueLabelList()
      {
        {
          "O",
          "Open Item"
        },
        {
          "B",
          "Balance Brought Forward"
        }
      };
    }
  }

  public class balanceBroughtForward : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ARStatementType.balanceBroughtForward>
  {
    public balanceBroughtForward()
      : base("B")
    {
    }
  }

  public class openItem : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ARStatementType.openItem>
  {
    public openItem()
      : base("O")
    {
    }
  }

  public class balanceFoward : ARStatementType.balanceBroughtForward
  {
  }
}
