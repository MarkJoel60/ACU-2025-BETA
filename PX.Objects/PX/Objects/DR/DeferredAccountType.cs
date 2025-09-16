// Decompiled with JetBrains decompiler
// Type: PX.Objects.DR.DeferredAccountType
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data.BQL;
using PX.Objects.Common;
using System.Collections.Generic;

#nullable enable
namespace PX.Objects.DR;

public class DeferredAccountType : ILabelProvider
{
  private static readonly 
  #nullable disable
  IEnumerable<ValueLabelPair> _valueLabelPairs = (IEnumerable<ValueLabelPair>) new ValueLabelList()
  {
    {
      "I",
      "Revenue"
    },
    {
      "E",
      nameof (Expense)
    }
  };
  public const string Income = "I";
  public const string Expense = "E";

  public IEnumerable<ValueLabelPair> ValueLabelPairs => DeferredAccountType._valueLabelPairs;

  public class income : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  DeferredAccountType.income>
  {
    public income()
      : base("I")
    {
    }
  }

  public class expense : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  DeferredAccountType.expense>
  {
    public expense()
      : base("E")
    {
    }
  }
}
