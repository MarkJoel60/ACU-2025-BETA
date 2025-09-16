// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.ListField_FuelType_Equipment
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.FS;

public abstract class ListField_FuelType_Equipment : IBqlField, IBqlOperand
{
  public class ListAtrribute : PXStringListAttribute
  {
    public ListAtrribute()
      : base(new ID.FuelType_Equipment().ID_LIST, new ID.FuelType_Equipment().TX_LIST)
    {
    }
  }

  public class RegularUnleaded : 
    BqlType<IBqlString, string>.Constant<
    #nullable disable
    ListField_FuelType_Equipment.RegularUnleaded>
  {
    public RegularUnleaded()
      : base("R")
    {
    }
  }

  public class PremiumUnleaded : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ListField_FuelType_Equipment.PremiumUnleaded>
  {
    public PremiumUnleaded()
      : base("P")
    {
    }
  }

  public class Diesel : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ListField_FuelType_Equipment.Diesel>
  {
    public Diesel()
      : base("D")
    {
    }
  }

  public class Other : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ListField_FuelType_Equipment.Other>
  {
    public Other()
      : base("O")
    {
    }
  }
}
