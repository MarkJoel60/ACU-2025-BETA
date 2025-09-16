// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INScanUserSetup
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
namespace PX.Objects.IN;

[PXCacheName]
public class INScanUserSetup : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBGuid(false, IsKey = true)]
  [PXDefault(typeof (Search<Users.pKID, Where<Users.pKID, Equal<Current<AccessInfo.userID>>>>))]
  [PXUIField(DisplayName = "User")]
  public virtual Guid? UserID { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Is Overridden", Enabled = false)]
  public virtual bool? IsOverridden { get; set; }

  [PXDBString(4, IsKey = true, IsFixed = true)]
  [PXUIField(DisplayName = "Mode", Enabled = false, Visible = false)]
  public virtual 
  #nullable disable
  string Mode { get; set; }

  [PXDBBool]
  [PXDefault(true, typeof (Search<INScanSetup.defaultWarehouse, Where<INScanSetup.branchID, Equal<Current<AccessInfo.branchID>>>>))]
  [PXUIField(DisplayName = "Default Warehouse from User Profile")]
  public virtual bool? DefaultWarehouse { get; set; }

  [PXDBBool]
  [PXDefault(false, typeof (Search<INScanSetup.printInventoryLabelsAutomatically, Where<INScanSetup.branchID, Equal<Current<AccessInfo.branchID>>>>))]
  [PXUIField(DisplayName = "Print Inventory Labels Automatically", FieldClass = "DeviceHub")]
  public virtual bool? PrintInventoryLabelsAutomatically { get; set; }

  [PXDefault("IN619200", typeof (Search<INScanSetup.inventoryLabelsReportID, Where<INScanSetup.branchID, Equal<Current<AccessInfo.branchID>>>>))]
  [PXDBString(8, InputMask = "CC.CC.CC.CC")]
  [PXUIField(DisplayName = "Inventory Labels Report ID", FieldClass = "DeviceHub")]
  [PXSelector(typeof (Search<SiteMap.screenID, Where<SiteMap.screenID, Like<PXModule.in_>, And<Where<SiteMap.url, Like<PX.Objects.Common.urlReports>, Or<SiteMap.url, Like<urlReportsInNewUi>>>>>, OrderBy<Asc<SiteMap.screenID>>>), new Type[] {typeof (SiteMap.screenID), typeof (SiteMap.title)}, Headers = new string[] {"Report ID", "Report Name"}, DescriptionField = typeof (SiteMap.title))]
  [PXUIEnabled(typeof (INScanUserSetup.printInventoryLabelsAutomatically))]
  [PXUIRequired(typeof (Where<INScanUserSetup.printInventoryLabelsAutomatically, Equal<True>, And<FeatureInstalled<FeaturesSet.deviceHub>>>))]
  public virtual string InventoryLabelsReportID { get; set; }

  [PXDBBool]
  [PXDefault(false, typeof (Search<INScanSetup.useDefaultLotSerialNbrInTransfer, Where<BqlField<INScanUserSetup.mode, IBqlString>.FromCurrent, Equal<INDocType.transfer>, And<INScanSetup.branchID, Equal<Current<AccessInfo.branchID>>>>>))]
  [PXUIField(DisplayName = "Use Default Lot/Serial Nbr.")]
  public virtual bool? UseDefaultLotSerialNbr { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Use Digital Scale", FieldClass = "DeviceHub", Visible = false)]
  public virtual bool? UseScale { get; set; }

  [PXScaleSelector]
  [PXUIEnabled(typeof (INScanUserSetup.useScale))]
  [PXUIField(DisplayName = "Scale", FieldClass = "DeviceHub", Visible = false)]
  [PXForeignReference(typeof (INScanUserSetup.FK.ScaleDevice))]
  public virtual Guid? ScaleDeviceID { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  public virtual bool SameAs(INScanSetup setup)
  {
    bool? defaultWarehouse1 = this.DefaultWarehouse;
    bool? defaultWarehouse2 = setup.DefaultWarehouse;
    if (defaultWarehouse1.GetValueOrDefault() == defaultWarehouse2.GetValueOrDefault() & defaultWarehouse1.HasValue == defaultWarehouse2.HasValue)
    {
      bool? labelsAutomatically1 = this.PrintInventoryLabelsAutomatically;
      bool? labelsAutomatically2 = setup.PrintInventoryLabelsAutomatically;
      if (labelsAutomatically1.GetValueOrDefault() == labelsAutomatically2.GetValueOrDefault() & labelsAutomatically1.HasValue == labelsAutomatically2.HasValue && this.InventoryLabelsReportID == setup.InventoryLabelsReportID && this.Mode == "T")
      {
        bool? defaultLotSerialNbr = this.UseDefaultLotSerialNbr;
        bool? serialNbrInTransfer = setup.UseDefaultLotSerialNbrInTransfer;
        return defaultLotSerialNbr.GetValueOrDefault() == serialNbrInTransfer.GetValueOrDefault() & defaultLotSerialNbr.HasValue == serialNbrInTransfer.HasValue;
      }
    }
    return false;
  }

  public virtual INScanUserSetup ApplyValuesFrom(INScanSetup setup)
  {
    this.DefaultWarehouse = setup.DefaultWarehouse;
    this.PrintInventoryLabelsAutomatically = setup.PrintInventoryLabelsAutomatically;
    this.InventoryLabelsReportID = setup.InventoryLabelsReportID;
    this.UseDefaultLotSerialNbr = !(this.Mode == "T") ? new bool?(false) : setup.UseDefaultLotSerialNbrInTransfer;
    return this;
  }

  public class PK : PrimaryKeyOf<INScanUserSetup>.By<INScanUserSetup.userID, INScanUserSetup.mode>
  {
    public static INScanUserSetup Find(
      PXGraph graph,
      Guid? userID,
      string mode,
      PKFindOptions options = 0)
    {
      return (INScanUserSetup) PrimaryKeyOf<INScanUserSetup>.By<INScanUserSetup.userID, INScanUserSetup.mode>.FindBy(graph, (object) userID, (object) mode, options);
    }
  }

  public static class FK
  {
    public class User : 
      PrimaryKeyOf<Users>.By<Users.pKID>.ForeignKeyOf<INScanUserSetup>.By<INScanUserSetup.userID>
    {
    }

    public class ScaleDevice : 
      PrimaryKeyOf<SMScale>.By<SMScale.scaleDeviceID>.ForeignKeyOf<INScanUserSetup>.By<INScanUserSetup.scaleDeviceID>
    {
    }

    public class InventoryLabelsReport : 
      PrimaryKeyOf<SiteMap>.By<SiteMap.nodeID>.ForeignKeyOf<INScanUserSetup>.By<INScanUserSetup.inventoryLabelsReportID>
    {
    }
  }

  public abstract class userID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  INScanUserSetup.userID>
  {
  }

  public abstract class isOverridden : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  INScanUserSetup.isOverridden>
  {
  }

  public abstract class mode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INScanUserSetup.mode>
  {
  }

  public abstract class defaultWarehouse : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INScanUserSetup.defaultWarehouse>
  {
  }

  public abstract class printInventoryLabelsAutomatically : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INScanUserSetup.printInventoryLabelsAutomatically>
  {
  }

  public abstract class inventoryLabelsReportID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INScanUserSetup.inventoryLabelsReportID>
  {
  }

  public abstract class useDefaultLotSerialNbr : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    INScanUserSetup.useDefaultLotSerialNbr>
  {
  }

  public abstract class useScale : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  INScanUserSetup.useScale>
  {
  }

  public abstract class scaleDeviceID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  INScanUserSetup.scaleDeviceID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  INScanUserSetup.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  INScanUserSetup.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INScanUserSetup.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INScanUserSetup.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    INScanUserSetup.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INScanUserSetup.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INScanUserSetup.lastModifiedDateTime>
  {
  }
}
