// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INItemClassCurySettings
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CM;
using System;

#nullable enable
namespace PX.Objects.IN;

[PXCacheName("Item Class Currency Settings", CacheGlobal = true)]
public class INItemClassCurySettings : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBInt(IsKey = true)]
  [PXDBDefault(typeof (INItemClass.itemClassID))]
  [PXUIField]
  [PXParent(typeof (INItemClassCurySettings.FK.ItemClass))]
  public virtual int? ItemClassID { get; set; }

  [PXDBString(IsUnicode = true, IsKey = true)]
  [PXUIField(DisplayName = "Currency", Enabled = true)]
  [PXSelector(typeof (Search<CurrencyList.curyID>))]
  [PXDefault(typeof (Current<AccessInfo.baseCuryID>))]
  public virtual 
  #nullable disable
  string CuryID { get; set; }

  /// <summary>
  /// The default <see cref="T:PX.Objects.IN.INSite">Warehouse</see> used to store the items of this kind.
  /// Applicable only for Stock Items (see <see cref="P:PX.Objects.IN.InventoryItem.StkItem" />) and when the <see cref="P:PX.Objects.CS.FeaturesSet.Warehouse">Warehouses</see> feature is enabled.
  /// </summary>
  /// <value>
  /// Corresponds to the <see cref="P:PX.Objects.IN.INSite.SiteID" /> field.
  /// Defaults to the <see cref="P:PX.Objects.IN.INItemClassCurySettings.DfltSiteID">Default Warehouse</see> specified for the <see cref="P:PX.Objects.IN.InventoryItem.ItemClassID">Class of the item</see>.
  /// </value>
  [Site(DisplayName = "Default Warehouse", DescriptionField = typeof (INSite.descr))]
  [PXForeignReference(typeof (INItemClassCurySettings.FK.DefaultSite))]
  public virtual int? DfltSiteID { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  [PXUIField(DisplayName = "Created On", Enabled = false, IsReadOnly = true)]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  [PXUIField(DisplayName = "Last Modified On", Enabled = false, IsReadOnly = true)]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  public class PK : 
    PrimaryKeyOf<INItemClassCurySettings>.By<INItemClassCurySettings.itemClassID, INItemClassCurySettings.curyID>
  {
    public static INItemClassCurySettings Find(
      PXGraph graph,
      int? itemClassID,
      string curyID,
      PKFindOptions options = 0)
    {
      return (INItemClassCurySettings) PrimaryKeyOf<INItemClassCurySettings>.By<INItemClassCurySettings.itemClassID, INItemClassCurySettings.curyID>.FindBy(graph, (object) itemClassID, (object) curyID, options);
    }
  }

  public static class FK
  {
    public class ItemClass : 
      PrimaryKeyOf<INItemClass>.By<INItemClass.itemClassID>.ForeignKeyOf<INItemClassCurySettings>.By<INItemClassCurySettings.itemClassID>
    {
    }

    public class Currency : 
      PrimaryKeyOf<PX.Objects.CM.Currency>.By<PX.Objects.CM.Currency.curyID>.ForeignKeyOf<InventoryItemCurySettings>.By<INItemClassCurySettings.curyID>
    {
    }

    public class DefaultSite : 
      PrimaryKeyOf<INSite>.By<INSite.siteID>.ForeignKeyOf<InventoryItemCurySettings>.By<INItemClassCurySettings.dfltSiteID>
    {
    }
  }

  public abstract class itemClassID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    INItemClassCurySettings.itemClassID>
  {
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INItemClassCurySettings.curyID>
  {
  }

  public abstract class dfltSiteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INItemClassCurySettings.dfltSiteID>
  {
  }

  public abstract class createdByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    INItemClassCurySettings.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INItemClassCurySettings.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INItemClassCurySettings.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    INItemClassCurySettings.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INItemClassCurySettings.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INItemClassCurySettings.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  INItemClassCurySettings.Tstamp>
  {
  }
}
