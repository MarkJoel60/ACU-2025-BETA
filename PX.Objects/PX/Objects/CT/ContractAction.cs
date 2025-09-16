// Decompiled with JetBrains decompiler
// Type: PX.Objects.CT.ContractAction
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.CT;

public static class ContractAction
{
  public const 
  #nullable disable
  string Create = "N";
  public const string Activate = "A";
  public const string Bill = "B";
  public const string Renew = "R";
  public const string Terminate = "T";
  public const string Upgrade = "U";
  public const string Setup = "S";
  public const string SetupAndActivate = "M";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new string[8]
      {
        "N",
        "A",
        "B",
        "R",
        "T",
        "U",
        "S",
        "M"
      }, new string[8]
      {
        "Create",
        "Activate",
        "Bill",
        "Renew",
        "Terminate",
        "Upgrade",
        "Set Up",
        "Set Up and Activate"
      })
    {
    }
  }

  public class create : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ContractAction.create>
  {
    public create()
      : base("N")
    {
    }
  }

  public class activate : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ContractAction.activate>
  {
    public activate()
      : base("A")
    {
    }
  }

  public class bill : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ContractAction.bill>
  {
    public bill()
      : base("B")
    {
    }
  }

  public class renew : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ContractAction.renew>
  {
    public renew()
      : base("R")
    {
    }
  }

  public class terminate : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ContractAction.terminate>
  {
    public terminate()
      : base("T")
    {
    }
  }

  public class upgrade : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ContractAction.upgrade>
  {
    public upgrade()
      : base("U")
    {
    }
  }

  public class setup : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ContractAction.setup>
  {
    public setup()
      : base("S")
    {
    }
  }

  public class setupAndActivate : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ContractAction.setupAndActivate>
  {
    public setupAndActivate()
      : base("M")
    {
    }
  }
}
