// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOQuickProcessParametersAvailabilityExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.SO;

public sealed class SOQuickProcessParametersAvailabilityExt : 
  PXCacheExtension<
  #nullable disable
  SOQuickProcessParameters>
{
  [PXString]
  public string AvailabilityStatus { get; set; }

  [PXBool]
  [PXDefault(true)]
  [PXUIField(Visible = false)]
  [PXUIVisible(typeof (Where<SOQuickProcessParametersAvailabilityExt.availabilityStatus, Equal<PX.Objects.SO.AvailabilityStatus.canShipAll>, And<SOQuickProcessParametersAvailabilityExt.skipByDateMsg, Equal<Empty>>>))]
  public bool? GreenStatus { get; set; }

  [PXBool]
  [PXDefault(true)]
  [PXUIField(Visible = false)]
  [PXUIVisible(typeof (Where<SOQuickProcessParametersAvailabilityExt.availabilityStatus, In3<PX.Objects.SO.AvailabilityStatus.canShipPartCancelRemainder, PX.Objects.SO.AvailabilityStatus.canShipPartBackOrder>, Or<SOQuickProcessParametersAvailabilityExt.availabilityStatus, Equal<PX.Objects.SO.AvailabilityStatus.canShipAll>, And<SOQuickProcessParametersAvailabilityExt.skipByDateMsg, NotEqual<Empty>>>>))]
  public bool? YellowStatus { get; set; }

  [PXBool]
  [PXDefault(true)]
  [PXUIField(Visible = false)]
  [PXUIVisible(typeof (Where<SOQuickProcessParametersAvailabilityExt.availabilityStatus, In3<PX.Objects.SO.AvailabilityStatus.nothingToShipAtAll, PX.Objects.SO.AvailabilityStatus.nothingToShip, PX.Objects.SO.AvailabilityStatus.noItemsAvailableToShip>>))]
  public bool? RedStatus { get; set; }

  [PXString(IsUnicode = true)]
  [PXUIField(IsReadOnly = true)]
  [PXUIVisible(typeof (Where<SOQuickProcessParametersAvailabilityExt.availabilityStatus, NotEqual<Empty>, And<SOQuickProcessParametersAvailabilityExt.availabilityStatus, IsNotNull>>))]
  [PXFormula(typeof (SmartJoin<Space, Switch<Case<Where<SOQuickProcessParametersAvailabilityExt.availabilityStatus, Equal<PX.Objects.SO.AvailabilityStatus.canShipAll>>, SOQuickProcessParametersAvailabilityExt.Msg.canShipAll, Case<Where<SOQuickProcessParametersAvailabilityExt.availabilityStatus, Equal<PX.Objects.SO.AvailabilityStatus.canShipPartCancelRemainder>>, SOQuickProcessParametersAvailabilityExt.Msg.canShipPartCancelRemainder, Case<Where<SOQuickProcessParametersAvailabilityExt.availabilityStatus, Equal<PX.Objects.SO.AvailabilityStatus.canShipPartBackOrder>>, SOQuickProcessParametersAvailabilityExt.Msg.canShipPartBackOrder, Case<Where<SOQuickProcessParametersAvailabilityExt.availabilityStatus, Equal<PX.Objects.SO.AvailabilityStatus.nothingToShip>>, SOQuickProcessParametersAvailabilityExt.Msg.nothingToShip, Case<Where<SOQuickProcessParametersAvailabilityExt.availabilityStatus, Equal<PX.Objects.SO.AvailabilityStatus.nothingToShipAtAll>>, SOQuickProcessParametersAvailabilityExt.Msg.nothingToShipAtAll, Case<Where<SOQuickProcessParametersAvailabilityExt.availabilityStatus, Equal<PX.Objects.SO.AvailabilityStatus.noItemsAvailableToShip>>, SOQuickProcessParametersAvailabilityExt.Msg.noItemsAvailableToShip>>>>>>, Empty>, SOQuickProcessParametersAvailabilityExt.skipByDateMsg>))]
  public string AvailabilityMessage { get; set; }

  /// <summary>
  /// The field is used to display the value from <see cref="P:PX.Objects.SO.SOQuickProcessParametersAvailabilityExt.AvailabilityMessage" /> in UI via <c>qp-info-box</c> component.
  /// </summary>
  /// <remarks>See <see cref="M:PX.Objects.SO.SOOrderEntry.SOQuickProcess.DisplayAvailabilityStatusMessage(PX.Objects.SO.SOQuickProcessParameters)" /> for the linking logic.</remarks>
  [PXString(IsUnicode = true)]
  [PXUIField(IsReadOnly = true, Visible = false)]
  public string AvailabilityStatusMessage { get; set; }

  [PXString]
  [PXDefault(typeof (Empty))]
  public string SkipByDateMsg { get; set; }

  [PXLocalizable]
  public static class Msg
  {
    public const string CanShipAll = "All items available on the selected date.";
    public const string CanShipPartCancelRemainder = "Several items are not available for shipment on the selected date. The system will cancel items that are not currently available and complete the order.";
    public const string CanShipPartBackOrder = "Several items are not available for shipment on the selected date. The system will back order items that are not currently available and will not complete the order.";
    public const string NothingToShip = "There are no items to ship on the selected date in the warehouse. Try changing the date or the warehouse.";
    public const string NothingToShipAtAll = "There are no items to ship for this order.";
    public const string NoItemsAvailableToShip = "Several items are not available and partial shipment is not allowed for the order.";

    public class canShipAll : ConstantMessage<SOQuickProcessParametersAvailabilityExt.Msg.canShipAll>
    {
      public canShipAll()
        : base("All items available on the selected date.")
      {
      }
    }

    public class canShipPartCancelRemainder : 
      ConstantMessage<SOQuickProcessParametersAvailabilityExt.Msg.canShipPartCancelRemainder>
    {
      public canShipPartCancelRemainder()
        : base("Several items are not available for shipment on the selected date. The system will cancel items that are not currently available and complete the order.")
      {
      }
    }

    public class canShipPartBackOrder : 
      ConstantMessage<SOQuickProcessParametersAvailabilityExt.Msg.canShipPartBackOrder>
    {
      public canShipPartBackOrder()
        : base("Several items are not available for shipment on the selected date. The system will back order items that are not currently available and will not complete the order.")
      {
      }
    }

    public class nothingToShip : 
      ConstantMessage<SOQuickProcessParametersAvailabilityExt.Msg.nothingToShip>
    {
      public nothingToShip()
        : base("There are no items to ship on the selected date in the warehouse. Try changing the date or the warehouse.")
      {
      }
    }

    public class nothingToShipAtAll : 
      ConstantMessage<SOQuickProcessParametersAvailabilityExt.Msg.nothingToShipAtAll>
    {
      public nothingToShipAtAll()
        : base("There are no items to ship for this order.")
      {
      }
    }

    public class noItemsAvailableToShip : 
      ConstantMessage<SOQuickProcessParametersAvailabilityExt.Msg.noItemsAvailableToShip>
    {
      public noItemsAvailableToShip()
        : base("Several items are not available and partial shipment is not allowed for the order.")
      {
      }
    }
  }

  public abstract class availabilityStatus : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOQuickProcessParametersAvailabilityExt.availabilityStatus>
  {
  }

  public abstract class greenStatus : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOQuickProcessParametersAvailabilityExt.greenStatus>
  {
  }

  public abstract class yellowStatus : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOQuickProcessParametersAvailabilityExt.yellowStatus>
  {
  }

  public abstract class redStatus : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOQuickProcessParametersAvailabilityExt.redStatus>
  {
  }

  public abstract class availabilityMessage : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOQuickProcessParametersAvailabilityExt.availabilityMessage>
  {
  }

  /// <inheritdoc cref="P:PX.Objects.SO.SOQuickProcessParametersAvailabilityExt.AvailabilityStatusMessage" />
  /// &gt;
  public abstract class availabilityStatusMessage : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOQuickProcessParametersAvailabilityExt.availabilityStatusMessage>
  {
  }

  public abstract class skipByDateMsg : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOQuickProcessParametersAvailabilityExt.skipByDateMsg>
  {
  }
}
