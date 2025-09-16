// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INCostCenter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CT;
using PX.Objects.PM;
using System;

#nullable enable
namespace PX.Objects.IN;

[PXCacheName("IN Cost Center")]
public class INCostCenter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBForeignIdentity(typeof (INCostSite), IsKey = true)]
  [PXReferentialIntegrityCheck]
  public virtual int? CostCenterID { get; set; }

  [PXDefault]
  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Cost Center ID", Visible = false)]
  public virtual 
  #nullable disable
  string CostCenterCD { get; set; }

  [PXDBString(1)]
  [PXDefault]
  [PX.Objects.IN.CostLayerType.List]
  public virtual string CostLayerType { get; set; }

  [PXDefault]
  [Site]
  [PXForeignReference(typeof (INCostCenter.FK.Site))]
  public virtual int? SiteID { get; set; }

  [Location(typeof (INCostCenter.siteID))]
  public virtual int? LocationID { get; set; }

  [Project(typeof (Where<PMProject.baseType, Equal<CTPRType.project>>))]
  [PXForeignReference(typeof (INCostCenter.FK.Project))]
  public virtual int? ProjectID { get; set; }

  [ProjectTask(typeof (INCostCenter.projectID))]
  [PXForeignReference(typeof (INCostCenter.FK.Task))]
  public virtual int? TaskID { get; set; }

  [PXDBString(2, IsFixed = true)]
  [PXUIField(DisplayName = "Sales Order Type", Enabled = false)]
  public virtual string SOOrderType { get; set; }

  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "Sales Order Nbr.", Enabled = false)]
  public virtual string SOOrderNbr { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Sales Order Line Nbr.", Enabled = false)]
  public virtual int? SOOrderLineNbr { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  public class PK : PrimaryKeyOf<INCostCenter>.By<INCostCenter.costCenterID>.Dirty
  {
    public static INCostCenter Find(PXGraph graph, int? costSiteID)
    {
      return (INCostCenter) PrimaryKeyOf<INCostCenter>.By<INCostCenter.costCenterID>.Dirty.FindBy(graph, (object) costSiteID, costSiteID.GetValueOrDefault() <= 0);
    }
  }

  public class UKProject : 
    PrimaryKeyOf<INCostCenter>.By<INCostCenter.siteID, INCostCenter.locationID, INCostCenter.projectID, INCostCenter.taskID>
  {
    public static INCostCenter Find(
      PXGraph graph,
      int? siteID,
      int? locationID,
      int? projectID,
      int? taskID,
      PKFindOptions options = 0)
    {
      return (INCostCenter) PrimaryKeyOf<INCostCenter>.By<INCostCenter.siteID, INCostCenter.locationID, INCostCenter.projectID, INCostCenter.taskID>.FindBy(graph, (object) siteID, (object) locationID, (object) projectID, (object) taskID, options);
    }
  }

  public static class FK
  {
    public class Site : 
      PrimaryKeyOf<INSite>.By<INSite.siteID>.ForeignKeyOf<INCostCenter>.By<INCostCenter.siteID>
    {
    }

    public class Location : 
      PrimaryKeyOf<INLocation>.By<INLocation.locationID>.ForeignKeyOf<INCostCenter>.By<INCostCenter.locationID>
    {
    }

    public class Project : 
      PrimaryKeyOf<PMProject>.By<PMProject.contractID>.ForeignKeyOf<INCostCenter>.By<INCostCenter.projectID>
    {
    }

    public class OrderLine : 
      PrimaryKeyOf<PX.Objects.SO.SOLine>.By<PX.Objects.SO.SOLine.orderType, PX.Objects.SO.SOLine.orderNbr, PX.Objects.SO.SOLine.lineNbr>.ForeignKeyOf<INCostCenter>.By<INCostCenter.sOOrderType, INCostCenter.sOOrderNbr, INCostCenter.sOOrderLineNbr>
    {
    }

    public class Task : 
      PrimaryKeyOf<PMTask>.By<PMTask.projectID, PMTask.taskID>.ForeignKeyOf<INCostCenter>.By<INCostCenter.projectID, INCostCenter.taskID>
    {
    }
  }

  public abstract class costCenterID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INCostCenter.costCenterID>
  {
  }

  public abstract class costCenterCD : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INCostCenter.costCenterCD>
  {
  }

  public abstract class costLayerType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INCostCenter.costLayerType>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INCostCenter.siteID>
  {
  }

  public abstract class locationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INCostCenter.locationID>
  {
  }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INCostCenter.projectID>
  {
  }

  public abstract class taskID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INCostCenter.taskID>
  {
  }

  public abstract class sOOrderType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INCostCenter.sOOrderType>
  {
  }

  public abstract class sOOrderNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INCostCenter.sOOrderNbr>
  {
  }

  public abstract class sOOrderLineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INCostCenter.sOOrderLineNbr>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  INCostCenter.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INCostCenter.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INCostCenter.createdDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  INCostCenter.Tstamp>
  {
  }
}
