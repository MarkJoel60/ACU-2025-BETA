// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.SOPickerToShipmentLink
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.Common;
using PX.Objects.Common.Bql;
using PX.Objects.IN;
using System;

#nullable enable
namespace PX.Objects.SO;

[PXCacheName]
public class SOPickerToShipmentLink : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(15, IsKey = true, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXUIField]
  [PXDBDefault(typeof (SOPickingWorksheet.worksheetNbr))]
  [PXParent(typeof (SOPickerToShipmentLink.FK.Worksheet))]
  public virtual 
  #nullable disable
  string WorksheetNbr { get; set; }

  [PXDBInt(IsKey = true)]
  [PXDBDefault(typeof (SOPicker.pickerNbr))]
  [PXUIField(DisplayName = "Picker Nbr.")]
  public virtual int? PickerNbr { get; set; }

  [PXDBString(15, IsKey = true, IsUnicode = true, InputMask = "")]
  [PXDefault]
  [PXForeignReference(typeof (SOPickerToShipmentLink.FK.Shipment))]
  [PXUIField(DisplayName = "Shipment Nbr.", Enabled = false)]
  public virtual string ShipmentNbr { get; set; }

  [Site]
  [PXDefault]
  [PXForeignReference(typeof (SOPickerToShipmentLink.FK.Site))]
  [InterBranchRestrictor(typeof (Where<SameOrganizationBranch<PX.Objects.IN.INSite.branchID, Current<AccessInfo.branchID>>>))]
  public virtual int? SiteID { get; set; }

  [INTote.UnassignableTote(IsKey = true, Enabled = false)]
  [PXForeignReference(typeof (SOPickerToShipmentLink.FK.Tote))]
  public virtual int? ToteID { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  [PXUIField(DisplayName = "Created At", Enabled = false, IsReadOnly = true)]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  [PXUIField(DisplayName = "Created On", Enabled = false, IsReadOnly = true)]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  [PXUIField(DisplayName = "Last Modified By", Enabled = false, IsReadOnly = true)]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  [PXUIField(DisplayName = "Last Modified At", Enabled = false, IsReadOnly = true)]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  [PXUIField(DisplayName = "Last Modified On", Enabled = false, IsReadOnly = true)]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  public class PK : 
    PrimaryKeyOf<SOPickerToShipmentLink>.By<SOPickerToShipmentLink.worksheetNbr, SOPickerToShipmentLink.pickerNbr, SOPickerToShipmentLink.siteID, SOPickerToShipmentLink.toteID, SOPickerToShipmentLink.shipmentNbr>
  {
    public static SOPickerToShipmentLink Find(
      PXGraph graph,
      string worksheetNbr,
      int? pickerNbr,
      int? siteID,
      int? toteID,
      string shipmentNbr,
      PKFindOptions options = 0)
    {
      return (SOPickerToShipmentLink) PrimaryKeyOf<SOPickerToShipmentLink>.By<SOPickerToShipmentLink.worksheetNbr, SOPickerToShipmentLink.pickerNbr, SOPickerToShipmentLink.siteID, SOPickerToShipmentLink.toteID, SOPickerToShipmentLink.shipmentNbr>.FindBy(graph, (object) worksheetNbr, (object) pickerNbr, (object) siteID, (object) toteID, (object) shipmentNbr, options);
    }
  }

  public static class FK
  {
    public class Worksheet : 
      PrimaryKeyOf<SOPickingWorksheet>.By<SOPickingWorksheet.worksheetNbr>.ForeignKeyOf<SOPickerToShipmentLink>.By<SOPickerToShipmentLink.worksheetNbr>
    {
    }

    public class Picker : 
      PrimaryKeyOf<SOPicker>.By<SOPicker.worksheetNbr, SOPicker.pickerNbr>.ForeignKeyOf<SOPickerToShipmentLink>.By<SOPickerToShipmentLink.worksheetNbr, SOPickerToShipmentLink.pickerNbr>
    {
    }

    public class Shipment : 
      PrimaryKeyOf<SOShipment>.By<SOShipment.shipmentNbr>.ForeignKeyOf<SOPickerToShipmentLink>.By<SOPickerToShipmentLink.shipmentNbr>
    {
    }

    public class Site : 
      PrimaryKeyOf<PX.Objects.IN.INSite>.By<PX.Objects.IN.INSite.siteID>.ForeignKeyOf<SOPickerToShipmentLink>.By<SOPickerToShipmentLink.siteID>
    {
    }

    public class Tote : 
      PrimaryKeyOf<INTote>.By<INTote.siteID, INTote.toteID>.ForeignKeyOf<SOPickerToShipmentLink>.By<SOPickerToShipmentLink.siteID, SOPickerToShipmentLink.toteID>
    {
    }
  }

  public abstract class worksheetNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOPickerToShipmentLink.worksheetNbr>
  {
  }

  public abstract class pickerNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOPickerToShipmentLink.pickerNbr>
  {
  }

  public abstract class shipmentNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOPickerToShipmentLink.shipmentNbr>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOPickerToShipmentLink.siteID>
  {
  }

  public abstract class toteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SOPickerToShipmentLink.toteID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  SOPickerToShipmentLink.Tstamp>
  {
  }

  public abstract class createdByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    SOPickerToShipmentLink.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOPickerToShipmentLink.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SOPickerToShipmentLink.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    SOPickerToShipmentLink.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SOPickerToShipmentLink.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SOPickerToShipmentLink.lastModifiedDateTime>
  {
  }
}
