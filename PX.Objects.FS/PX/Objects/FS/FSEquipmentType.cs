// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSEquipmentType
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

[PXCacheName("Equipment Type")]
[PXPrimaryGraph(typeof (EquipmentTypeMaint))]
[Serializable]
public class FSEquipmentType : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBIdentity]
  [PXUIField(Enabled = false)]
  public virtual int? EquipmentTypeID { get; set; }

  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = ">CCCCCCCCCCCCCCC", IsFixed = true)]
  [PXDefault]
  [NormalizeWhiteSpace]
  [PXUIField]
  [PXSelector(typeof (Search<FSEquipmentType.equipmentTypeCD>), DescriptionField = typeof (FSEquipmentType.descr))]
  public virtual 
  #nullable disable
  string EquipmentTypeCD { get; set; }

  [PXDBLocalizableString(60, IsUnicode = true)]
  [PXDefault]
  [PXUIField]
  public virtual string Descr { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Require Branch Location")]
  public virtual bool? RequireBranchLocation { get; set; }

  [PXUIField(DisplayName = "NoteID")]
  [PXNote]
  public virtual Guid? NoteID { get; set; }

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

  public class PK : PrimaryKeyOf<FSEquipmentType>.By<FSEquipmentType.equipmentTypeCD>
  {
    public static FSEquipmentType Find(
      PXGraph graph,
      string equipmentTypeCD,
      PKFindOptions options = 0)
    {
      return (FSEquipmentType) PrimaryKeyOf<FSEquipmentType>.By<FSEquipmentType.equipmentTypeCD>.FindBy(graph, (object) equipmentTypeCD, options);
    }
  }

  public class UK : PrimaryKeyOf<FSEquipmentType>.By<FSEquipmentType.equipmentTypeID>
  {
    public static FSEquipmentType Find(PXGraph graph, int? equipmentTypeID, PKFindOptions options = 0)
    {
      return (FSEquipmentType) PrimaryKeyOf<FSEquipmentType>.By<FSEquipmentType.equipmentTypeID>.FindBy(graph, (object) equipmentTypeID, options);
    }
  }

  public abstract class equipmentTypeID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSEquipmentType.equipmentTypeID>
  {
  }

  public abstract class equipmentTypeCD : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSEquipmentType.equipmentTypeCD>
  {
  }

  public abstract class descr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSEquipmentType.descr>
  {
  }

  public abstract class requireBranchLocation : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSEquipmentType.requireBranchLocation>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FSEquipmentType.noteID>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FSEquipmentType.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSEquipmentType.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSEquipmentType.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    FSEquipmentType.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSEquipmentType.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSEquipmentType.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  FSEquipmentType.Tstamp>
  {
  }
}
