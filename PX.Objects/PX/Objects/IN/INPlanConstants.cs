// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INPlanConstants
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.IN;

public class INPlanConstants
{
  /// <summary>IN Receipts</summary>
  public const 
  #nullable disable
  string Plan10 = "10";
  /// <summary>IN Issues</summary>
  public const string Plan20 = "20";
  /// <summary>IN One-step Transfer</summary>
  public const string Plan40 = "40";
  /// <summary>IN Two-step Transfer</summary>
  public const string Plan41 = "41";
  /// <summary>In-Transit - gets replaned  to <see cref="F:PX.Objects.IN.INPlanConstants.Plan43" /></summary>
  public const string Plan42 = "42";
  /// <summary>IN Transfer Receipt</summary>
  public const string Plan43 = "43";
  /// <summary>In-Transit to SO - gets replaned to <see cref="F:PX.Objects.IN.INPlanConstants.Plan45" /></summary>
  public const string Plan44 = "44";
  /// <summary>Transfer Receipt for SO</summary>
  public const string Plan45 = "45";
  /// <summary>IN Assembly Demand</summary>
  public const string Plan50 = "50";
  /// <summary>IN Assembly Supply</summary>
  public const string Plan51 = "51";
  /// <summary>SO Booked</summary>
  public const string Plan60 = "60";
  /// <summary>SO Allocated</summary>
  public const string Plan61 = "61";
  /// <summary>SO Shipped</summary>
  public const string Plan62 = "62";
  /// <summary>SO Allocated - Synonymous to <see cref="F:PX.Objects.IN.INPlanConstants.Plan61" /></summary>
  public const string Plan63 = "63";
  /// <summary>Pre-Allocated</summary>
  public const string Plan64 = "64";
  /// <summary>SO to Purchase Regular</summary>
  public const string Plan66 = "66";
  /// <summary>SO Back Ordered</summary>
  public const string Plan68 = "68";
  /// <summary>SO Prepared</summary>
  public const string Plan69 = "69";
  /// <summary>SO to Purchase Blanket</summary>
  public const string Plan6B = "6B";
  /// <summary>SO to Drop-Ship Regular</summary>
  public const string Plan6D = "6D";
  /// <summary>SO to Drop-Ship Blanket</summary>
  public const string Plan6E = "6E";
  /// <summary>SO to Drop-Ship Transfer</summary>
  public const string Plan6T = "6T";
  /// <summary>PO Order</summary>
  public const string Plan70 = "70";
  /// <summary>PO Receipt</summary>
  public const string Plan71 = "71";
  /// <summary>PO RC Return</summary>
  public const string Plan72 = "72";
  /// <summary>PO Prepare</summary>
  public const string Plan73 = "73";
  /// <summary>Drop-Ship for SO</summary>
  public const string Plan74 = "74";
  /// <summary>Drop-Ship for SO Receipts</summary>
  public const string Plan75 = "75";
  /// <summary>Purchase for SO</summary>
  public const string Plan76 = "76";
  /// <summary>Receipt for SO</summary>
  public const string Plan77 = "77";
  /// <summary>Purchase for SO Prepared</summary>
  public const string Plan78 = "78";
  /// <summary>Drop-Ship for SO Prepared</summary>
  public const string Plan79 = "79";
  /// <summary>PO Blanket</summary>
  public const string Plan7B = "7B";
  /// <summary>IN Replanned</summary>
  public const string Plan90 = "90";
  /// <summary>In-Transit to SO - gets replaned  to <see cref="F:PX.Objects.IN.INPlanConstants.Plan95" /></summary>
  public const string Plan93 = "93";
  /// <summary>In-Transit - gets replaned  to <see cref="F:PX.Objects.IN.INPlanConstants.Plan95" /></summary>
  public const string Plan94 = "94";
  /// <summary>PO Complete</summary>
  public const string Plan95 = "95";
  /// <summary>In Transit to Production</summary>
  public const string PlanM0 = "M0";
  /// <summary>Production Supply Prepared</summary>
  public const string PlanM1 = "M1";
  /// <summary>Production Supply</summary>
  public const string PlanM2 = "M2";
  /// <summary>Purchase for Production Prepared</summary>
  public const string PlanM3 = "M3";
  /// <summary>Purchase for Production</summary>
  public const string PlanM4 = "M4";
  /// <summary>Production Demand Prepared</summary>
  public const string PlanM5 = "M5";
  /// <summary>Production Demand</summary>
  public const string PlanM6 = "M6";
  /// <summary>Production Allocated</summary>
  public const string PlanM7 = "M7";
  /// <summary>SO to Production</summary>
  public const string PlanM8 = "M8";
  /// <summary>Production to Purchase</summary>
  public const string PlanM9 = "M9";
  /// <summary>Production to Production</summary>
  public const string PlanMA = "MA";
  /// <summary>Production for Prod. Prepared</summary>
  public const string PlanMB = "MB";
  /// <summary>Production for Production</summary>
  public const string PlanMC = "MC";
  /// <summary>Production for SO Prepared</summary>
  public const string PlanMD = "MD";
  /// <summary>Production for SO</summary>
  public const string PlanME = "ME";
  /// <summary>FS Prepared</summary>
  public const string PlanF0 = "F0";
  /// <summary>FS Booked</summary>
  public const string PlanF1 = "F1";
  /// <summary>FS Allocated</summary>
  public const string PlanF2 = "F2";
  /// <summary>FS Pre-Allocated</summary>
  public const string PlanF5 = "F5";
  /// <summary>FS to Purchase</summary>
  public const string PlanF6 = "F6";
  /// <summary>Purchase for FS</summary>
  public const string PlanF7 = "F7";
  /// <summary>Purchase for FS Prepared</summary>
  public const string PlanF8 = "F8";
  /// <summary>Receipts for FS</summary>
  public const string PlanF9 = "F9";

  public static bool IsFixed(string PlanType)
  {
    return PlanType == "64" || PlanType == "66" || PlanType == "6B" || PlanType == "6E" || PlanType == "6D" || PlanType == "93" || PlanType == "F5" || PlanType == "F6" || PlanType == "M8";
  }

  public static bool IsAllocated(string PlanType)
  {
    return PlanType == "61" || PlanType == "63" || PlanType == "M7" || PlanType == "F2";
  }

  public static string ToModuleField(string planType)
  {
    switch (planType)
    {
      case "10":
      case "20":
      case "40":
      case "41":
      case "42":
      case "43":
      case "50":
      case "51":
      case "90":
      case "94":
        return "IN";
      case "44":
      case "60":
      case "61":
      case "62":
      case "63":
      case "66":
      case "68":
      case "69":
      case "6B":
      case "6D":
      case "6E":
      case "6T":
      case "93":
        return "SO";
      case "45":
      case "70":
      case "71":
      case "72":
      case "73":
      case "74":
      case "75":
      case "76":
      case "77":
      case "78":
      case "79":
      case "F7":
      case "F8":
      case "F9":
        return "PO";
      case "F0":
      case "F1":
      case "F2":
      case "F6":
        return "FS";
      case "M0":
      case "M1":
      case "M2":
      case "M3":
      case "M4":
      case "M5":
      case "M6":
      case "M7":
      case "M8":
      case "M9":
      case "MA":
      case "MB":
      case "MC":
      case "MD":
      case "ME":
        return "AM";
      default:
        return (string) null;
    }
  }

  public static Type ToInclQtyField(string planType)
  {
    switch (planType)
    {
      case "10":
      case "43":
        return typeof (INPlanType.inclQtyINReceipts);
      case "20":
      case "40":
      case "41":
        return typeof (INPlanType.inclQtyINIssues);
      case "42":
      case "94":
        return typeof (INPlanType.inclQtyInTransit);
      case "44":
      case "93":
        return typeof (INPlanType.inclQtyInTransitToSO);
      case "45":
      case "77":
        return typeof (INPlanType.inclQtyPOFixedReceipts);
      case "50":
        return typeof (INPlanType.inclQtyINAssemblyDemand);
      case "51":
        return typeof (INPlanType.inclQtyINAssemblySupply);
      case "60":
        return typeof (INPlanType.inclQtySOBooked);
      case "61":
      case "63":
        return typeof (INPlanType.inclQtySOShipping);
      case "62":
        return typeof (INPlanType.inclQtySOShipped);
      case "66":
      case "6B":
        return typeof (INPlanType.inclQtySOFixed);
      case "68":
        return typeof (INPlanType.inclQtySOBackOrdered);
      case "69":
        return typeof (INPlanType.inclQtySOPrepared);
      case "6D":
      case "6E":
      case "6T":
        return typeof (INPlanType.inclQtySODropShip);
      case "70":
        return typeof (INPlanType.inclQtyPOOrders);
      case "71":
      case "72":
        return typeof (INPlanType.inclQtyPOReceipts);
      case "73":
        return typeof (INPlanType.inclQtyPOPrepared);
      case "74":
        return typeof (INPlanType.inclQtyPODropShipOrders);
      case "75":
        return typeof (INPlanType.inclQtyPODropShipReceipts);
      case "76":
        return typeof (INPlanType.inclQtyPOFixedOrders);
      case "78":
        return typeof (INPlanType.inclQtyPOFixedPrepared);
      case "79":
        return typeof (INPlanType.inclQtyPODropShipPrepared);
      case "90":
        return typeof (INPlanType.inclQtyINReplaned);
      case "F0":
        return typeof (INPlanType.inclQtyFSSrvOrdPrepared);
      case "F1":
        return typeof (INPlanType.inclQtyFSSrvOrdBooked);
      case "F2":
        return typeof (INPlanType.inclQtyFSSrvOrdAllocated);
      case "F6":
        return typeof (INPlanType.inclQtyFixedFSSrvOrd);
      case "F7":
        return typeof (INPlanType.inclQtyPOFixedFSSrvOrd);
      case "F8":
        return typeof (INPlanType.inclQtyPOFixedFSSrvOrdPrepared);
      case "F9":
        return typeof (INPlanType.inclQtyPOFixedFSSrvOrdReceipts);
      case "M0":
        return typeof (INPlanType.inclQtyInTransitToProduction);
      case "M1":
        return typeof (INPlanType.inclQtyProductionSupplyPrepared);
      case "M2":
        return typeof (INPlanType.inclQtyProductionSupply);
      case "M3":
        return typeof (INPlanType.inclQtyPOFixedProductionPrepared);
      case "M4":
        return typeof (INPlanType.inclQtyPOFixedProductionOrders);
      case "M5":
        return typeof (INPlanType.inclQtyProductionDemandPrepared);
      case "M6":
        return typeof (INPlanType.inclQtyProductionDemand);
      case "M7":
        return typeof (INPlanType.inclQtyProductionAllocated);
      case "M8":
        return typeof (INPlanType.inclQtySOFixedProduction);
      case "M9":
        return typeof (INPlanType.inclQtyProdFixedPurchase);
      case "MA":
        return typeof (INPlanType.inclQtyProdFixedProduction);
      case "MB":
        return typeof (INPlanType.inclQtyProdFixedProdOrdersPrepared);
      case "MC":
        return typeof (INPlanType.inclQtyProdFixedProdOrders);
      case "MD":
        return typeof (INPlanType.inclQtyProdFixedSalesOrdersPrepared);
      case "ME":
        return typeof (INPlanType.inclQtyProdFixedSalesOrders);
      default:
        return (Type) null;
    }
  }

  /// <inheritdoc cref="F:PX.Objects.IN.INPlanConstants.Plan10" />
  public class plan10 : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INPlanConstants.plan10>
  {
    public plan10()
      : base("10")
    {
    }
  }

  /// <inheritdoc cref="F:PX.Objects.IN.INPlanConstants.Plan20" />
  public class plan20 : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INPlanConstants.plan20>
  {
    public plan20()
      : base("20")
    {
    }
  }

  /// <inheritdoc cref="F:PX.Objects.IN.INPlanConstants.Plan40" />
  public class plan40 : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INPlanConstants.plan40>
  {
    public plan40()
      : base("40")
    {
    }
  }

  /// <inheritdoc cref="F:PX.Objects.IN.INPlanConstants.Plan41" />
  public class plan41 : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INPlanConstants.plan41>
  {
    public plan41()
      : base("41")
    {
    }
  }

  /// <inheritdoc cref="F:PX.Objects.IN.INPlanConstants.Plan42" />
  public class plan42 : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INPlanConstants.plan42>
  {
    public plan42()
      : base("42")
    {
    }
  }

  /// <inheritdoc cref="F:PX.Objects.IN.INPlanConstants.Plan43" />
  public class plan43 : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INPlanConstants.plan43>
  {
    public plan43()
      : base("43")
    {
    }
  }

  /// <inheritdoc cref="F:PX.Objects.IN.INPlanConstants.Plan44" />
  public class plan44 : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INPlanConstants.plan44>
  {
    public plan44()
      : base("44")
    {
    }
  }

  /// <inheritdoc cref="F:PX.Objects.IN.INPlanConstants.Plan45" />
  public class plan45 : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INPlanConstants.plan45>
  {
    public plan45()
      : base("45")
    {
    }
  }

  /// <inheritdoc cref="F:PX.Objects.IN.INPlanConstants.Plan50" />
  public class plan50 : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INPlanConstants.plan50>
  {
    public plan50()
      : base("50")
    {
    }
  }

  /// <inheritdoc cref="F:PX.Objects.IN.INPlanConstants.Plan51" />
  public class plan51 : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INPlanConstants.plan51>
  {
    public plan51()
      : base("51")
    {
    }
  }

  /// <inheritdoc cref="F:PX.Objects.IN.INPlanConstants.Plan60" />
  public class plan60 : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INPlanConstants.plan60>
  {
    public plan60()
      : base("60")
    {
    }
  }

  /// <inheritdoc cref="F:PX.Objects.IN.INPlanConstants.Plan61" />
  public class plan61 : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INPlanConstants.plan61>
  {
    public plan61()
      : base("61")
    {
    }
  }

  /// <inheritdoc cref="F:PX.Objects.IN.INPlanConstants.Plan62" />
  public class plan62 : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INPlanConstants.plan62>
  {
    public plan62()
      : base("62")
    {
    }
  }

  /// <inheritdoc cref="F:PX.Objects.IN.INPlanConstants.Plan63" />
  public class plan63 : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INPlanConstants.plan63>
  {
    public plan63()
      : base("63")
    {
    }
  }

  /// <inheritdoc cref="F:PX.Objects.IN.INPlanConstants.Plan64" />
  public class plan64 : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INPlanConstants.plan64>
  {
    public plan64()
      : base("64")
    {
    }
  }

  /// <inheritdoc cref="F:PX.Objects.IN.INPlanConstants.Plan66" />
  public class plan66 : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INPlanConstants.plan66>
  {
    public plan66()
      : base("66")
    {
    }
  }

  /// <inheritdoc cref="F:PX.Objects.IN.INPlanConstants.Plan68" />
  public class plan68 : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INPlanConstants.plan68>
  {
    public plan68()
      : base("68")
    {
    }
  }

  /// <inheritdoc cref="F:PX.Objects.IN.INPlanConstants.Plan69" />
  public class plan69 : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INPlanConstants.plan69>
  {
    public plan69()
      : base("69")
    {
    }
  }

  /// <inheritdoc cref="F:PX.Objects.IN.INPlanConstants.Plan6B" />
  public class plan6B : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INPlanConstants.plan6B>
  {
    public plan6B()
      : base("6B")
    {
    }
  }

  /// <inheritdoc cref="F:PX.Objects.IN.INPlanConstants.Plan6D" />
  public class plan6D : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INPlanConstants.plan6D>
  {
    public plan6D()
      : base("6D")
    {
    }
  }

  /// <inheritdoc cref="F:PX.Objects.IN.INPlanConstants.Plan6E" />
  public class plan6E : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INPlanConstants.plan6E>
  {
    public plan6E()
      : base("6E")
    {
    }
  }

  /// <inheritdoc cref="F:PX.Objects.IN.INPlanConstants.Plan6T" />
  public class plan6T : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INPlanConstants.plan6T>
  {
    public plan6T()
      : base("6T")
    {
    }
  }

  /// <inheritdoc cref="F:PX.Objects.IN.INPlanConstants.Plan70" />
  public class plan70 : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INPlanConstants.plan70>
  {
    public plan70()
      : base("70")
    {
    }
  }

  /// <inheritdoc cref="F:PX.Objects.IN.INPlanConstants.Plan71" />
  public class plan71 : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INPlanConstants.plan71>
  {
    public plan71()
      : base("71")
    {
    }
  }

  /// <inheritdoc cref="F:PX.Objects.IN.INPlanConstants.Plan72" />
  public class plan72 : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INPlanConstants.plan72>
  {
    public plan72()
      : base("72")
    {
    }
  }

  /// <inheritdoc cref="F:PX.Objects.IN.INPlanConstants.Plan73" />
  public class plan73 : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INPlanConstants.plan73>
  {
    public plan73()
      : base("73")
    {
    }
  }

  /// <inheritdoc cref="F:PX.Objects.IN.INPlanConstants.Plan74" />
  public class plan74 : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INPlanConstants.plan74>
  {
    public plan74()
      : base("74")
    {
    }
  }

  /// <inheritdoc cref="F:PX.Objects.IN.INPlanConstants.Plan75" />
  public class plan75 : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INPlanConstants.plan75>
  {
    public plan75()
      : base("75")
    {
    }
  }

  /// <inheritdoc cref="F:PX.Objects.IN.INPlanConstants.Plan76" />
  public class plan76 : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INPlanConstants.plan76>
  {
    public plan76()
      : base("76")
    {
    }
  }

  /// <inheritdoc cref="F:PX.Objects.IN.INPlanConstants.Plan77" />
  public class plan77 : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INPlanConstants.plan77>
  {
    public plan77()
      : base("77")
    {
    }
  }

  /// <inheritdoc cref="F:PX.Objects.IN.INPlanConstants.Plan78" />
  public class plan78 : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INPlanConstants.plan78>
  {
    public plan78()
      : base("78")
    {
    }
  }

  /// <inheritdoc cref="F:PX.Objects.IN.INPlanConstants.Plan79" />
  public class plan79 : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INPlanConstants.plan79>
  {
    public plan79()
      : base("79")
    {
    }
  }

  /// <inheritdoc cref="F:PX.Objects.IN.INPlanConstants.Plan7B" />
  public class plan7B : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INPlanConstants.plan7B>
  {
    public plan7B()
      : base("7B")
    {
    }
  }

  /// <inheritdoc cref="F:PX.Objects.IN.INPlanConstants.Plan90" />
  public class plan90 : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INPlanConstants.plan90>
  {
    public plan90()
      : base("90")
    {
    }
  }

  /// <inheritdoc cref="F:PX.Objects.IN.INPlanConstants.Plan93" />
  public class plan93 : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INPlanConstants.plan93>
  {
    public plan93()
      : base("93")
    {
    }
  }

  /// <inheritdoc cref="F:PX.Objects.IN.INPlanConstants.Plan94" />
  public class plan94 : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INPlanConstants.plan94>
  {
    public plan94()
      : base("94")
    {
    }
  }

  /// <inheritdoc cref="F:PX.Objects.IN.INPlanConstants.Plan95" />
  public class plan95 : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INPlanConstants.plan95>
  {
    public plan95()
      : base("95")
    {
    }
  }

  /// <inheritdoc cref="F:PX.Objects.IN.INPlanConstants.PlanM0" />
  public class planM0 : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INPlanConstants.planM0>
  {
    public planM0()
      : base("M0")
    {
    }
  }

  /// <inheritdoc cref="F:PX.Objects.IN.INPlanConstants.PlanM1" />
  public class planM1 : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INPlanConstants.planM1>
  {
    public planM1()
      : base("M1")
    {
    }
  }

  /// <inheritdoc cref="F:PX.Objects.IN.INPlanConstants.PlanM2" />
  public class planM2 : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INPlanConstants.planM2>
  {
    public planM2()
      : base("M2")
    {
    }
  }

  /// <inheritdoc cref="F:PX.Objects.IN.INPlanConstants.PlanM3" />
  public class planM3 : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INPlanConstants.planM3>
  {
    public planM3()
      : base("M3")
    {
    }
  }

  /// <inheritdoc cref="F:PX.Objects.IN.INPlanConstants.PlanM4" />
  public class planM4 : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INPlanConstants.planM4>
  {
    public planM4()
      : base("M4")
    {
    }
  }

  /// <inheritdoc cref="F:PX.Objects.IN.INPlanConstants.PlanM5" />
  public class planM5 : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INPlanConstants.planM5>
  {
    public planM5()
      : base("M5")
    {
    }
  }

  /// <inheritdoc cref="F:PX.Objects.IN.INPlanConstants.PlanM6" />
  public class planM6 : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INPlanConstants.planM6>
  {
    public planM6()
      : base("M6")
    {
    }
  }

  /// <inheritdoc cref="F:PX.Objects.IN.INPlanConstants.PlanM7" />
  public class planM7 : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INPlanConstants.planM7>
  {
    public planM7()
      : base("M7")
    {
    }
  }

  /// <inheritdoc cref="F:PX.Objects.IN.INPlanConstants.PlanM8" />
  public class planM8 : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INPlanConstants.planM8>
  {
    public planM8()
      : base("M8")
    {
    }
  }

  /// <inheritdoc cref="F:PX.Objects.IN.INPlanConstants.PlanM9" />
  public class planM9 : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INPlanConstants.planM9>
  {
    public planM9()
      : base("M9")
    {
    }
  }

  /// <inheritdoc cref="F:PX.Objects.IN.INPlanConstants.PlanMA" />
  public class planMA : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INPlanConstants.planMA>
  {
    public planMA()
      : base("MA")
    {
    }
  }

  /// <inheritdoc cref="F:PX.Objects.IN.INPlanConstants.PlanMB" />
  public class planMB : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INPlanConstants.planMB>
  {
    public planMB()
      : base("MB")
    {
    }
  }

  /// <inheritdoc cref="F:PX.Objects.IN.INPlanConstants.PlanMC" />
  public class planMC : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INPlanConstants.planMC>
  {
    public planMC()
      : base("MC")
    {
    }
  }

  /// <inheritdoc cref="F:PX.Objects.IN.INPlanConstants.PlanME" />
  public class planME : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INPlanConstants.planME>
  {
    public planME()
      : base("ME")
    {
    }
  }

  /// <inheritdoc cref="F:PX.Objects.IN.INPlanConstants.PlanMD" />
  public class planMD : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INPlanConstants.planMD>
  {
    public planMD()
      : base("MD")
    {
    }
  }

  /// <inheritdoc cref="F:PX.Objects.IN.INPlanConstants.PlanF0" />
  public class planF0 : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INPlanConstants.planF0>
  {
    public planF0()
      : base("F0")
    {
    }
  }

  /// <inheritdoc cref="F:PX.Objects.IN.INPlanConstants.PlanF1" />
  public class planF1 : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INPlanConstants.planF1>
  {
    public planF1()
      : base("F1")
    {
    }
  }

  /// <inheritdoc cref="F:PX.Objects.IN.INPlanConstants.PlanF2" />
  public class planF2 : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INPlanConstants.planF2>
  {
    public planF2()
      : base("F2")
    {
    }
  }

  /// <inheritdoc cref="F:PX.Objects.IN.INPlanConstants.PlanF5" />
  public class planF5 : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INPlanConstants.planF5>
  {
    public planF5()
      : base("F5")
    {
    }
  }

  /// <inheritdoc cref="F:PX.Objects.IN.INPlanConstants.PlanF6" />
  public class planF6 : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INPlanConstants.planF6>
  {
    public planF6()
      : base("F6")
    {
    }
  }

  /// <inheritdoc cref="F:PX.Objects.IN.INPlanConstants.PlanF7" />
  public class planF7 : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INPlanConstants.planF7>
  {
    public planF7()
      : base("F7")
    {
    }
  }

  /// <inheritdoc cref="F:PX.Objects.IN.INPlanConstants.PlanF8" />
  public class planF8 : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INPlanConstants.planF8>
  {
    public planF8()
      : base("F8")
    {
    }
  }

  /// <inheritdoc cref="F:PX.Objects.IN.INPlanConstants.PlanF9" />
  public class planF9 : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  INPlanConstants.planF9>
  {
    public planF9()
      : base("F9")
    {
    }
  }
}
