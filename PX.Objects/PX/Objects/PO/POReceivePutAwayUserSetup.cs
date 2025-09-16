// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.POReceivePutAwayUserSetup
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CA;
using PX.Objects.Common;
using PX.Objects.CS;
using PX.SM;
using System;

#nullable enable
namespace PX.Objects.PO;

[PXCacheName]
public class POReceivePutAwayUserSetup : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
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
  [PXDefault(false, typeof (Search<POReceivePutAwaySetup.defaultLotSerialNumber, Where<POReceivePutAwaySetup.branchID, Equal<Current<AccessInfo.branchID>>>>))]
  [PXUIField(DisplayName = "Use Default Auto-Generated Lot/Serial Nbr.")]
  public virtual bool? DefaultLotSerialNumber { get; set; }

  [PXDBBool]
  [PXDefault(true, typeof (Search<POReceivePutAwaySetup.defaultExpireDate, Where<POReceivePutAwaySetup.branchID, Equal<Current<AccessInfo.branchID>>>>))]
  [PXUIField(DisplayName = "Use Default Expiration Date")]
  public virtual bool? DefaultExpireDate { get; set; }

  [PXDBBool]
  [PXDefault(false, typeof (Search<POReceivePutAwaySetup.singleLocation, Where<POReceivePutAwaySetup.branchID, Equal<Current<AccessInfo.branchID>>>>))]
  [PXUIField(DisplayName = "Use Single Receiving Location", FieldClass = "INLOCATION")]
  public virtual bool? SingleLocation { get; set; }

  [PXDBBool]
  [PXDefault(false, typeof (Search<POReceivePutAwaySetup.printInventoryLabelsAutomatically, Where<POReceivePutAwaySetup.branchID, Equal<Current<AccessInfo.branchID>>>>))]
  [PXUIField(DisplayName = "Print Inventory Labels Automatically", FieldClass = "DeviceHub")]
  public virtual bool? PrintInventoryLabelsAutomatically { get; set; }

  [PXDefault("IN619200", typeof (Search<POReceivePutAwaySetup.inventoryLabelsReportID, Where<POReceivePutAwaySetup.branchID, Equal<Current<AccessInfo.branchID>>>>))]
  [PXDBString(8, InputMask = "CC.CC.CC.CC")]
  [PXUIField(DisplayName = "Inventory Labels Report ID", FieldClass = "DeviceHub")]
  [PXSelector(typeof (Search<SiteMap.screenID, Where<SiteMap.screenID, Like<PXModule.in_>, And<Where<SiteMap.url, Like<PX.Objects.Common.urlReports>, Or<SiteMap.url, Like<urlReportsInNewUi>>>>>, OrderBy<Asc<SiteMap.screenID>>>), new Type[] {typeof (SiteMap.screenID), typeof (SiteMap.title)}, Headers = new string[] {"Report ID", "Report Name"}, DescriptionField = typeof (SiteMap.title))]
  [PXUIEnabled(typeof (POReceivePutAwayUserSetup.printInventoryLabelsAutomatically))]
  [PXUIRequired(typeof (Where<POReceivePutAwayUserSetup.printInventoryLabelsAutomatically, Equal<True>, And<FeatureInstalled<FeaturesSet.deviceHub>>>))]
  public virtual 
  #nullable disable
  string InventoryLabelsReportID { get; set; }

  [PXDBBool]
  [PXDefault(false, typeof (Search<POReceivePutAwaySetup.printPurchaseReceiptAutomatically, Where<POReceivePutAwaySetup.branchID, Equal<Current<AccessInfo.branchID>>>>))]
  [PXUIField(DisplayName = "Print Purchase Receipts Automatically", FieldClass = "DeviceHub")]
  public virtual bool? PrintPurchaseReceiptAutomatically { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Use Digital Scale", FieldClass = "DeviceHub", Visible = false)]
  public virtual bool? UseScale { get; set; }

  [PXScaleSelector]
  [PXUIEnabled(typeof (POReceivePutAwayUserSetup.useScale))]
  [PXUIField(DisplayName = "Scale", FieldClass = "DeviceHub", Visible = false)]
  [PXForeignReference(typeof (Field<POReceivePutAwayUserSetup.scaleDeviceID>.IsRelatedTo<SMScale.scaleDeviceID>))]
  public virtual Guid? ScaleDeviceID { get; set; }

  public virtual bool SameAs(POReceivePutAwaySetup setup)
  {
    bool? singleLocation1 = this.SingleLocation;
    bool? singleLocation2 = setup.SingleLocation;
    if (singleLocation1.GetValueOrDefault() == singleLocation2.GetValueOrDefault() & singleLocation1.HasValue == singleLocation2.HasValue)
    {
      bool? defaultLotSerialNumber1 = this.DefaultLotSerialNumber;
      bool? defaultLotSerialNumber2 = setup.DefaultLotSerialNumber;
      if (defaultLotSerialNumber1.GetValueOrDefault() == defaultLotSerialNumber2.GetValueOrDefault() & defaultLotSerialNumber1.HasValue == defaultLotSerialNumber2.HasValue)
      {
        bool? defaultExpireDate1 = this.DefaultExpireDate;
        bool? defaultExpireDate2 = setup.DefaultExpireDate;
        if (defaultExpireDate1.GetValueOrDefault() == defaultExpireDate2.GetValueOrDefault() & defaultExpireDate1.HasValue == defaultExpireDate2.HasValue)
        {
          bool? receiptAutomatically1 = this.PrintPurchaseReceiptAutomatically;
          bool? receiptAutomatically2 = setup.PrintPurchaseReceiptAutomatically;
          if (receiptAutomatically1.GetValueOrDefault() == receiptAutomatically2.GetValueOrDefault() & receiptAutomatically1.HasValue == receiptAutomatically2.HasValue)
          {
            bool? labelsAutomatically1 = this.PrintInventoryLabelsAutomatically;
            bool? labelsAutomatically2 = setup.PrintInventoryLabelsAutomatically;
            if (labelsAutomatically1.GetValueOrDefault() == labelsAutomatically2.GetValueOrDefault() & labelsAutomatically1.HasValue == labelsAutomatically2.HasValue)
              return this.InventoryLabelsReportID == setup.InventoryLabelsReportID;
          }
        }
      }
    }
    return false;
  }

  public virtual POReceivePutAwayUserSetup ApplyValuesFrom(POReceivePutAwaySetup setup)
  {
    this.SingleLocation = setup.SingleLocation;
    this.DefaultLotSerialNumber = setup.DefaultLotSerialNumber;
    this.DefaultExpireDate = setup.DefaultExpireDate;
    this.PrintPurchaseReceiptAutomatically = setup.PrintPurchaseReceiptAutomatically;
    this.PrintInventoryLabelsAutomatically = setup.PrintInventoryLabelsAutomatically;
    this.InventoryLabelsReportID = setup.InventoryLabelsReportID;
    return this;
  }

  public class PK : PrimaryKeyOf<POReceivePutAwayUserSetup>.By<POReceivePutAwayUserSetup.userID>
  {
    public static POReceivePutAwayUserSetup Find(
      PXGraph graph,
      Guid? userID,
      PKFindOptions options = 0)
    {
      return (POReceivePutAwayUserSetup) PrimaryKeyOf<POReceivePutAwayUserSetup>.By<POReceivePutAwayUserSetup.userID>.FindBy(graph, (object) userID, options);
    }
  }

  public static class FK
  {
    public class User : 
      PrimaryKeyOf<Users>.By<Users.pKID>.ForeignKeyOf<POReceivePutAwayUserSetup>.By<POReceivePutAwayUserSetup.userID>
    {
    }

    public class InventoryLabelsReport : 
      PrimaryKeyOf<SiteMap>.By<SiteMap.nodeID>.ForeignKeyOf<POReceivePutAwaySetup>.By<POReceivePutAwayUserSetup.inventoryLabelsReportID>
    {
    }

    public class ScaleDevice : 
      PrimaryKeyOf<SMScale>.By<SMScale.scaleDeviceID>.ForeignKeyOf<POReceivePutAwayUserSetup>.By<POReceivePutAwayUserSetup.scaleDeviceID>
    {
    }
  }

  public abstract class userID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  POReceivePutAwayUserSetup.userID>
  {
  }

  public abstract class isOverridden : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    POReceivePutAwayUserSetup.isOverridden>
  {
  }

  public abstract class defaultLotSerialNumber : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    POReceivePutAwayUserSetup.defaultLotSerialNumber>
  {
  }

  public abstract class defaultExpireDate : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    POReceivePutAwayUserSetup.defaultExpireDate>
  {
  }

  public abstract class singleLocation : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    POReceivePutAwayUserSetup.singleLocation>
  {
  }

  public abstract class printInventoryLabelsAutomatically : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    POReceivePutAwayUserSetup.printInventoryLabelsAutomatically>
  {
  }

  public abstract class inventoryLabelsReportID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    POReceivePutAwayUserSetup.inventoryLabelsReportID>
  {
  }

  public abstract class printPurchaseReceiptAutomatically : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    POReceivePutAwayUserSetup.printPurchaseReceiptAutomatically>
  {
  }

  public abstract class useScale : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  POReceivePutAwayUserSetup.useScale>
  {
  }

  public abstract class scaleDeviceID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    POReceivePutAwayUserSetup.scaleDeviceID>
  {
  }
}
