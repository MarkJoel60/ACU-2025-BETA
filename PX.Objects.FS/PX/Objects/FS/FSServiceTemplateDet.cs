// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSServiceTemplateDet
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.IN;
using System;

#nullable enable
namespace PX.Objects.FS;

[PXCacheName("FSServiceTemplateDet")]
[Serializable]
public class FSServiceTemplateDet : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBInt(IsKey = true)]
  [PXParent(typeof (Select<FSServiceTemplate, Where<FSServiceTemplate.serviceTemplateID, Equal<Current<FSServiceTemplateDet.serviceTemplateID>>>>))]
  [PXDBDefault(typeof (FSServiceTemplate.serviceTemplateID))]
  [PXUIField(DisplayName = "Service Template ID")]
  public virtual int? ServiceTemplateID { get; set; }

  [PXDBIdentity(IsKey = true)]
  [PXUIField(Enabled = false)]
  [PXDefault]
  public virtual int? ServiceTemplateDetID { get; set; }

  [PXDBString(5, IsFixed = true)]
  [PXDefault("SERVI")]
  [PXUIField(DisplayName = "Line Type")]
  [ListField_LineType_UnifyTabs.ListAtrribute]
  public virtual 
  #nullable disable
  string LineType { get; set; }

  [PXDefault]
  [PXFormula(typeof (Default<FSServiceTemplateDet.lineType>))]
  [InventoryIDByLineType(typeof (FSServiceTemplateDet.lineType), null, Filterable = true)]
  [PXRestrictor(typeof (Where<PX.Objects.IN.InventoryItem.itemType, NotEqual<INItemTypes.serviceItem>, Or<FSxServiceClass.requireRoute, Equal<True>, Or<Current<FSSrvOrdType.requireRoute>, Equal<False>>>>), "Non-route service cannot be handled with current route Service Order Type.", new Type[] {})]
  [PXRestrictor(typeof (Where<PX.Objects.IN.InventoryItem.itemType, NotEqual<INItemTypes.serviceItem>, Or<FSxServiceClass.requireRoute, Equal<False>, Or<Current<FSSrvOrdType.requireRoute>, Equal<True>>>>), "Route service cannot be handled with current non-route Service Order Type.", new Type[] {})]
  public virtual int? InventoryID { get; set; }

  /// <summary>
  /// The <see cref="T:PX.Objects.IN.INUnit">Unit of Measure</see> used as the sales unit for the Inventory Item.
  /// </summary>
  /// <value>
  /// Defaults to the <see cref="P:PX.Objects.IN.InventoryItem.SalesUnit">Sales Unit</see> associated with the <see cref="P:PX.Objects.FS.FSServiceTemplateDet.InventoryID">Inventory Item</see>.
  /// Corresponds to the <see cref="P:PX.Objects.IN.INUnit.FromUnit" /> field.
  /// </value>
  [PXDefault(typeof (Search<PX.Objects.IN.InventoryItem.salesUnit, Where<PX.Objects.IN.InventoryItem.inventoryID, Equal<Current<FSServiceTemplateDet.inventoryID>>>>))]
  [INUnit(typeof (FSServiceTemplateDet.inventoryID), DisplayName = "UOM")]
  public virtual string UOM { get; set; }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "1.0")]
  [PXUIField(DisplayName = "Quantity")]
  public virtual Decimal? Qty { get; set; }

  [PXDBString(256 /*0x0100*/, IsUnicode = true)]
  [PXDefault]
  [PXFormula(typeof (Selector<FSServiceTemplateDet.inventoryID, PX.Objects.IN.InventoryItem.descr>))]
  [PXUIField(DisplayName = "Transaction Description")]
  public virtual string TranDesc { get; set; }

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

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  public class PK : 
    PrimaryKeyOf<FSServiceTemplateDet>.By<FSServiceTemplateDet.serviceTemplateID, FSServiceTemplateDet.serviceTemplateDetID>
  {
    public static FSServiceTemplateDet Find(
      PXGraph graph,
      int? serviceTemplateID,
      int? serviceTemplateDetID,
      PKFindOptions options = 0)
    {
      return (FSServiceTemplateDet) PrimaryKeyOf<FSServiceTemplateDet>.By<FSServiceTemplateDet.serviceTemplateID, FSServiceTemplateDet.serviceTemplateDetID>.FindBy(graph, (object) serviceTemplateID, (object) serviceTemplateDetID, options);
    }
  }

  public static class FK
  {
    public class ServiceTemplate : 
      PrimaryKeyOf<FSServiceTemplate>.By<FSServiceTemplate.serviceTemplateID>.ForeignKeyOf<FSServiceTemplateDet>.By<FSServiceTemplateDet.serviceTemplateID>
    {
    }

    public class InventoryItem : 
      PrimaryKeyOf<PX.Objects.IN.InventoryItem>.By<PX.Objects.IN.InventoryItem.inventoryID>.ForeignKeyOf<FSServiceTemplateDet>.By<FSServiceTemplateDet.inventoryID>
    {
    }
  }

  public abstract class serviceTemplateID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSServiceTemplateDet.serviceTemplateID>
  {
  }

  public abstract class serviceTemplateDetID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSServiceTemplateDet.serviceTemplateDetID>
  {
  }

  public abstract class lineType : ListField_LineType_UnifyTabs
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSServiceTemplateDet.inventoryID>
  {
  }

  public abstract class uOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSServiceTemplateDet.uOM>
  {
  }

  public abstract class qty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FSServiceTemplateDet.qty>
  {
  }

  public abstract class tranDesc : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSServiceTemplateDet.tranDesc>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FSServiceTemplateDet.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSServiceTemplateDet.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSServiceTemplateDet.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    FSServiceTemplateDet.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSServiceTemplateDet.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSServiceTemplateDet.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  FSServiceTemplateDet.Tstamp>
  {
  }
}
