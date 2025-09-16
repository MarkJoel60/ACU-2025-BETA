// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.AvailabilityStatus
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data.BQL;

#nullable enable
namespace PX.Objects.SO;

public static class AvailabilityStatus
{
  public const 
  #nullable disable
  string CanShipAll = "AL";
  public const string CanShipPartCancelRemainder = "PC";
  public const string CanShipPartBackOrder = "PB";
  public const string NothingToShip = "NT";
  public const string NothingToShipAtAll = "NA";
  public const string NoItemsAvailableToShip = "NI";

  public class canShipAll : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  AvailabilityStatus.canShipAll>
  {
    public canShipAll()
      : base("AL")
    {
    }
  }

  public class canShipPartCancelRemainder : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    AvailabilityStatus.canShipPartCancelRemainder>
  {
    public canShipPartCancelRemainder()
      : base("PC")
    {
    }
  }

  public class canShipPartBackOrder : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    AvailabilityStatus.canShipPartBackOrder>
  {
    public canShipPartBackOrder()
      : base("PB")
    {
    }
  }

  public class nothingToShip : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  AvailabilityStatus.nothingToShip>
  {
    public nothingToShip()
      : base("NT")
    {
    }
  }

  public class nothingToShipAtAll : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    AvailabilityStatus.nothingToShipAtAll>
  {
    public nothingToShipAtAll()
      : base("NA")
    {
    }
  }

  public class noItemsAvailableToShip : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    AvailabilityStatus.noItemsAvailableToShip>
  {
    public noItemsAvailableToShip()
      : base("NI")
    {
    }
  }
}
