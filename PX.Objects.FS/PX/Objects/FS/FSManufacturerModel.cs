// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSManufacturerModel
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

[PXCacheName("Manufacturer Model")]
[PXPrimaryGraph(typeof (ManufacturerModelMaint))]
[Serializable]
public class FSManufacturerModel : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBInt(IsKey = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Manufacturer ID")]
  [PXSelector(typeof (FSManufacturer.manufacturerID), SubstituteKey = typeof (FSManufacturer.manufacturerCD), DescriptionField = typeof (FSManufacturer.descr))]
  public virtual int? ManufacturerID { get; set; }

  [PXDBIdentity]
  [PXUIField(Enabled = false)]
  public virtual int? ManufacturerModelID { get; set; }

  [PXDBString(15, IsKey = true, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXDefault]
  [PXUIField]
  [PXSelector(typeof (Search<FSManufacturerModel.manufacturerModelCD, Where<FSManufacturerModel.manufacturerID, Equal<Current<FSManufacturerModel.manufacturerID>>>>), SubstituteKey = typeof (FSManufacturerModel.manufacturerModelCD), DescriptionField = typeof (FSManufacturerModel.descr))]
  public virtual 
  #nullable disable
  string ManufacturerModelCD { get; set; }

  [PXDBLocalizableString(60, IsUnicode = true)]
  [PXDefault]
  [PXUIField]
  public virtual string Descr { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Equipment Type")]
  [PXSelector(typeof (FSEquipmentType.equipmentTypeID), SubstituteKey = typeof (FSEquipmentType.equipmentTypeCD), DescriptionField = typeof (FSEquipmentType.descr))]
  public virtual int? EquipmentTypeID { get; set; }

  [PXUIField(DisplayName = "NoteID")]
  [PXNote]
  public virtual Guid? NoteID { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  [PXUIField(DisplayName = "Created On")]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  [PXUIField(DisplayName = "Last Modified On")]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  public class PK : 
    PrimaryKeyOf<FSManufacturerModel>.By<FSManufacturerModel.manufacturerID, FSManufacturerModel.manufacturerModelCD>
  {
    public static FSManufacturerModel Find(
      PXGraph graph,
      int? manufacturerID,
      string manufacturerModelCD,
      PKFindOptions options = 0)
    {
      return (FSManufacturerModel) PrimaryKeyOf<FSManufacturerModel>.By<FSManufacturerModel.manufacturerID, FSManufacturerModel.manufacturerModelCD>.FindBy(graph, (object) manufacturerID, (object) manufacturerModelCD, options);
    }
  }

  public static class FK
  {
    public class Manufacturer : 
      PrimaryKeyOf<FSManufacturer>.By<FSManufacturer.manufacturerID>.ForeignKeyOf<FSManufacturerModel>.By<FSManufacturerModel.manufacturerID>
    {
    }

    public class EquipmentType : 
      PrimaryKeyOf<FSEquipmentType>.By<FSEquipmentType.equipmentTypeCD>.ForeignKeyOf<FSManufacturerModel>.By<FSManufacturerModel.equipmentTypeID>
    {
    }
  }

  public abstract class manufacturerID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSManufacturerModel.manufacturerID>
  {
  }

  public abstract class manufacturerModelID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSManufacturerModel.manufacturerModelID>
  {
  }

  public abstract class manufacturerModelCD : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSManufacturerModel.manufacturerModelCD>
  {
  }

  public abstract class descr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSManufacturerModel.descr>
  {
  }

  public abstract class equipmentTypeID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSManufacturerModel.equipmentTypeID>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FSManufacturerModel.noteID>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FSManufacturerModel.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSManufacturerModel.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSManufacturerModel.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    FSManufacturerModel.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSManufacturerModel.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSManufacturerModel.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  FSManufacturerModel.Tstamp>
  {
  }
}
