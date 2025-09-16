// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOPickPackShipUserSetup
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.SM;
using System;

#nullable enable
namespace PX.Objects.SO;

[PXCacheName]
[Serializable]
public class SOPickPackShipUserSetup : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBGuid(false, IsKey = true)]
  [PXDefault(typeof (Search<Users.pKID, Where<Users.pKID, Equal<Current<AccessInfo.userID>>>>))]
  [PXUIField(DisplayName = "User")]
  public virtual Guid? UserID { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Is Overridden", Enabled = false)]
  public virtual bool? IsOverridden { get; set; }

  [PXDBBool]
  [PXDefault(false, typeof (Search<SOPickPackShipSetup.defaultLocationFromShipment, Where<SOPickPackShipSetup.branchID, Equal<Current<AccessInfo.branchID>>>>))]
  [PXUIField(DisplayName = "Use Default Location")]
  public virtual bool? DefaultLocationFromShipment { get; set; }

  [PXDBBool]
  [PXDefault(false, typeof (Search<SOPickPackShipSetup.defaultLotSerialFromShipment, Where<SOPickPackShipSetup.branchID, Equal<Current<AccessInfo.branchID>>>>))]
  [PXUIField(DisplayName = "Use Default Lot/Serial Nbr.")]
  public virtual bool? DefaultLotSerialFromShipment { get; set; }

  [PXDBBool]
  [PXDefault(false, typeof (Search<SOPickPackShipSetup.printShipmentConfirmation, Where<SOPickPackShipSetup.branchID, Equal<Current<AccessInfo.branchID>>>>))]
  [PXUIField(DisplayName = "Print Shipment Confirmation Automatically", FieldClass = "DeviceHub")]
  public virtual bool? PrintShipmentConfirmation { get; set; }

  [PXDBBool]
  [PXDefault(false, typeof (Search<SOPickPackShipSetup.printShipmentLabels, Where<SOPickPackShipSetup.branchID, Equal<Current<AccessInfo.branchID>>>>))]
  [PXUIField(DisplayName = "Print Shipment Labels Automatically", FieldClass = "DeviceHub")]
  public virtual bool? PrintShipmentLabels { get; set; }

  [PXDBBool]
  [PXDefault(false, typeof (Search<SOPickPackShipSetup.printCommercialInvoices, Where<SOPickPackShipSetup.branchID, Equal<Current<AccessInfo.branchID>>>>))]
  [PXUIField(DisplayName = "Print Commercial Invoices Automatically", FieldClass = "DeviceHub")]
  public virtual bool? PrintCommercialInvoices { get; set; }

  [PXDBBool]
  [PXDefault(false, typeof (Search<SOPickPackShipSetup.enterSizeForPackages, Where<SOPickPackShipSetup.branchID, Equal<Current<AccessInfo.branchID>>>>))]
  [PXUIField(DisplayName = "Enter Length/Width/Height for Packages", Visible = false)]
  public virtual bool? EnterSizeForPackages { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Use Digital Scale", FieldClass = "DeviceHub")]
  public virtual bool? UseScale { get; set; }

  [PXScaleSelector]
  [PXUIEnabled(typeof (SOPickPackShipUserSetup.useScale))]
  [PXUIField(DisplayName = "Scale", FieldClass = "DeviceHub")]
  [PXForeignReference(typeof (SOPickPackShipUserSetup.FK.ScaleDevice))]
  public virtual Guid? ScaleDeviceID { get; set; }

  public virtual bool SameAs(
  #nullable disable
  SOPickPackShipSetup setup)
  {
    bool? locationFromShipment1 = this.DefaultLocationFromShipment;
    bool? locationFromShipment2 = setup.DefaultLocationFromShipment;
    if (locationFromShipment1.GetValueOrDefault() == locationFromShipment2.GetValueOrDefault() & locationFromShipment1.HasValue == locationFromShipment2.HasValue)
    {
      bool? serialFromShipment1 = this.DefaultLotSerialFromShipment;
      bool? serialFromShipment2 = setup.DefaultLotSerialFromShipment;
      if (serialFromShipment1.GetValueOrDefault() == serialFromShipment2.GetValueOrDefault() & serialFromShipment1.HasValue == serialFromShipment2.HasValue)
      {
        bool? printShipmentLabels1 = this.PrintShipmentLabels;
        bool? printShipmentLabels2 = setup.PrintShipmentLabels;
        if (printShipmentLabels1.GetValueOrDefault() == printShipmentLabels2.GetValueOrDefault() & printShipmentLabels1.HasValue == printShipmentLabels2.HasValue)
        {
          bool? commercialInvoices1 = this.PrintCommercialInvoices;
          bool? commercialInvoices2 = setup.PrintCommercialInvoices;
          if (commercialInvoices1.GetValueOrDefault() == commercialInvoices2.GetValueOrDefault() & commercialInvoices1.HasValue == commercialInvoices2.HasValue)
          {
            bool? shipmentConfirmation1 = this.PrintShipmentConfirmation;
            bool? shipmentConfirmation2 = setup.PrintShipmentConfirmation;
            if (shipmentConfirmation1.GetValueOrDefault() == shipmentConfirmation2.GetValueOrDefault() & shipmentConfirmation1.HasValue == shipmentConfirmation2.HasValue)
            {
              bool? enterSizeForPackages1 = this.EnterSizeForPackages;
              bool? enterSizeForPackages2 = setup.EnterSizeForPackages;
              return enterSizeForPackages1.GetValueOrDefault() == enterSizeForPackages2.GetValueOrDefault() & enterSizeForPackages1.HasValue == enterSizeForPackages2.HasValue;
            }
          }
        }
      }
    }
    return false;
  }

  public virtual SOPickPackShipUserSetup ApplyValuesFrom(SOPickPackShipSetup setup)
  {
    this.DefaultLocationFromShipment = setup.DefaultLocationFromShipment;
    this.DefaultLotSerialFromShipment = setup.DefaultLotSerialFromShipment;
    this.PrintShipmentLabels = setup.PrintShipmentLabels;
    this.PrintCommercialInvoices = setup.PrintCommercialInvoices;
    this.PrintShipmentConfirmation = setup.PrintShipmentConfirmation;
    this.EnterSizeForPackages = setup.EnterSizeForPackages;
    return this;
  }

  public class PK : PrimaryKeyOf<SOPickPackShipUserSetup>.By<SOPickPackShipUserSetup.userID>
  {
    public static SOPickPackShipUserSetup Find(PXGraph graph, Guid? userID, PKFindOptions options = 0)
    {
      return (SOPickPackShipUserSetup) PrimaryKeyOf<SOPickPackShipUserSetup>.By<SOPickPackShipUserSetup.userID>.FindBy(graph, (object) userID, options);
    }
  }

  public static class FK
  {
    public class User : 
      PrimaryKeyOf<Users>.By<Users.pKID>.ForeignKeyOf<SOPickPackShipUserSetup>.By<SOPickPackShipUserSetup.userID>
    {
    }

    public class ScaleDevice : 
      PrimaryKeyOf<SMScale>.By<SMScale.scaleDeviceID>.ForeignKeyOf<SOPickPackShipUserSetup>.By<SOPickPackShipUserSetup.scaleDeviceID>
    {
    }
  }

  public abstract class userID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SOPickPackShipUserSetup.userID>
  {
  }

  public abstract class isOverridden : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOPickPackShipUserSetup.isOverridden>
  {
  }

  public abstract class defaultLocationFromShipment : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOPickPackShipUserSetup.defaultLocationFromShipment>
  {
  }

  public abstract class defaultLotSerialFromShipment : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOPickPackShipUserSetup.defaultLotSerialFromShipment>
  {
  }

  public abstract class printShipmentConfirmation : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOPickPackShipUserSetup.printShipmentConfirmation>
  {
  }

  public abstract class printShipmentLabels : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOPickPackShipUserSetup.printShipmentLabels>
  {
  }

  public abstract class printCommercialInvoices : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOPickPackShipUserSetup.printCommercialInvoices>
  {
  }

  public abstract class enterSizeForPackages : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SOPickPackShipUserSetup.enterSizeForPackages>
  {
  }

  public abstract class useScale : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SOPickPackShipUserSetup.useScale>
  {
  }

  public abstract class scaleDeviceID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    SOPickPackShipUserSetup.scaleDeviceID>
  {
  }
}
