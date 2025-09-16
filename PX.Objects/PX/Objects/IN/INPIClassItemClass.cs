// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INPIClassItemClass
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CS;
using System;

#nullable enable
namespace PX.Objects.IN;

[Serializable]
public class INPIClassItemClass : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(30, IsUnicode = true, IsKey = true)]
  [PXDBDefault(typeof (INPIClass.pIClassID))]
  [PXParent(typeof (INPIClassItemClass.FK.PIClass))]
  public virtual 
  #nullable disable
  string PIClassID { get; set; }

  [PXDBInt(IsKey = true)]
  [PXUIField(DisplayName = "Item Class ID")]
  [PXParent(typeof (INPIClassItemClass.FK.ItemClass))]
  [PXDimensionSelector("INITEMCLASS", typeof (Search<INItemClass.itemClassID, Where<INItemClass.stkItem, Equal<boolTrue>>>), typeof (INItemClass.itemClassCD), DescriptionField = typeof (INItemClass.descr), ValidComboRequired = true)]
  public virtual int? ItemClassID { get; set; }

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
    PrimaryKeyOf<INPIClassItemClass>.By<INPIClassItemClass.pIClassID, INPIClassItemClass.itemClassID>
  {
    public static INPIClassItemClass Find(
      PXGraph graph,
      string pIClassID,
      int? itemClassID,
      PKFindOptions options = 0)
    {
      return (INPIClassItemClass) PrimaryKeyOf<INPIClassItemClass>.By<INPIClassItemClass.pIClassID, INPIClassItemClass.itemClassID>.FindBy(graph, (object) pIClassID, (object) itemClassID, options);
    }
  }

  public static class FK
  {
    public class PIClass : 
      PrimaryKeyOf<INPIClass>.By<INPIClass.pIClassID>.ForeignKeyOf<INPIClassItemClass>.By<INPIClassItemClass.pIClassID>
    {
    }

    public class ItemClass : 
      PrimaryKeyOf<INItemClass>.By<INItemClass.itemClassID>.ForeignKeyOf<INPIClassItemClass>.By<INPIClassItemClass.itemClassID>
    {
    }
  }

  public abstract class pIClassID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INPIClassItemClass.pIClassID>
  {
  }

  public abstract class itemClassID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INPIClassItemClass.itemClassID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  INPIClassItemClass.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  INPIClassItemClass.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INPIClassItemClass.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INPIClassItemClass.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    INPIClassItemClass.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INPIClassItemClass.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INPIClassItemClass.lastModifiedDateTime>
  {
  }
}
