// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSServiceLicenseType
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.Objects.FS;

[PXCacheName("Service - License Type")]
[Serializable]
public class FSServiceLicenseType : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBInt(IsKey = true)]
  [PXDBDefault(typeof (PX.Objects.IN.InventoryItem.inventoryID))]
  [PXParent(typeof (Select<PX.Objects.IN.InventoryItem, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Current<FSServiceLicenseType.serviceID>>>>))]
  public virtual int? ServiceID { get; set; }

  [PXDBInt(IsKey = true)]
  [PXDefault]
  [PXSelector(typeof (FSLicenseType.licenseTypeID), SubstituteKey = typeof (FSLicenseType.licenseTypeCD))]
  [PXUIField(DisplayName = "License Type ID")]
  public virtual int? LicenseTypeID { get; set; }

  [PXDBCreatedByID]
  [PXUIField(DisplayName = "CreatedByID")]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  [PXUIField(DisplayName = "CreatedByScreenID")]
  public virtual 
  #nullable disable
  string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  [PXUIField(DisplayName = "CreatedDateTime")]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  [PXUIField(DisplayName = "LastModifiedByID")]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  [PXUIField(DisplayName = "LastModifiedByScreenID")]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  [PXUIField(DisplayName = "LastModifiedDateTime")]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  public class PK : 
    PrimaryKeyOf<FSServiceLicenseType>.By<FSServiceLicenseType.serviceID, FSServiceLicenseType.licenseTypeID>
  {
    public static FSServiceLicenseType Find(
      PXGraph graph,
      int? serviceID,
      int? licenseTypeID,
      PKFindOptions options = 0)
    {
      return (FSServiceLicenseType) PrimaryKeyOf<FSServiceLicenseType>.By<FSServiceLicenseType.serviceID, FSServiceLicenseType.licenseTypeID>.FindBy(graph, (object) serviceID, (object) licenseTypeID, options);
    }
  }

  public static class FK
  {
    public class Service : 
      PrimaryKeyOf<PX.Objects.IN.InventoryItem>.By<PX.Objects.IN.InventoryItem.inventoryID>.ForeignKeyOf<FSServiceLicenseType>.By<FSServiceLicenseType.serviceID>
    {
    }
  }

  public abstract class serviceID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSServiceLicenseType.serviceID>
  {
  }

  public abstract class licenseTypeID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSServiceLicenseType.licenseTypeID>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FSServiceLicenseType.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSServiceLicenseType.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSServiceLicenseType.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    FSServiceLicenseType.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSServiceLicenseType.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSServiceLicenseType.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  FSServiceLicenseType.Tstamp>
  {
  }
}
