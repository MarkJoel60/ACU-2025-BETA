// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.AccountEntityType
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.Common;
using System.Collections.Generic;

#nullable enable
namespace PX.Objects.GL;

public class AccountEntityType : ILabelProvider
{
  /// <summary>GL Account, Financial Accounting</summary>
  public const 
  #nullable disable
  string GLAccount = "F";

  public IEnumerable<ValueLabelPair> ValueLabelPairs
  {
    get
    {
      return (IEnumerable<ValueLabelPair>) new ValueLabelList()
      {
        {
          "F",
          "GL Account"
        }
      };
    }
  }

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new string[1]{ "F" }, new string[1]
      {
        "GL Account"
      })
    {
    }
  }

  public class gLAccount : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  AccountEntityType.gLAccount>
  {
    public gLAccount()
      : base("F")
    {
    }
  }
}
