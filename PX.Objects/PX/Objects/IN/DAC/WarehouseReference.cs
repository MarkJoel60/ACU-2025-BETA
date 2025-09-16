// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.DAC.WarehouseReference
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.Objects.IN.DAC;

[Serializable]
public class WarehouseReference : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBInt(IsKey = true)]
  [PXUIField]
  public virtual int? SiteID { get; set; }

  [PXDBString(256 /*0x0100*/, IsKey = true)]
  [PXUIField]
  public virtual 
  #nullable disable
  string PortalSetupID { get; set; }

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

  public class PK : 
    PrimaryKeyOf<WarehouseReference>.By<WarehouseReference.siteID, WarehouseReference.portalSetupID>
  {
    public static WarehouseReference Find(
      PXGraph graph,
      int? siteID,
      string portalSetupID,
      PKFindOptions options = 0)
    {
      return (WarehouseReference) PrimaryKeyOf<WarehouseReference>.By<WarehouseReference.siteID, WarehouseReference.portalSetupID>.FindBy(graph, (object) siteID, (object) portalSetupID, options);
    }
  }

  public static class FK
  {
    public class Site : 
      PrimaryKeyOf<INSite>.By<INSite.siteID>.ForeignKeyOf<WarehouseReference>.By<WarehouseReference.siteID>
    {
    }
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  WarehouseReference.siteID>
  {
  }

  public abstract class portalSetupID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    WarehouseReference.portalSetupID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  WarehouseReference.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  WarehouseReference.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    WarehouseReference.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    WarehouseReference.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    WarehouseReference.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    WarehouseReference.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    WarehouseReference.lastModifiedDateTime>
  {
  }
}
