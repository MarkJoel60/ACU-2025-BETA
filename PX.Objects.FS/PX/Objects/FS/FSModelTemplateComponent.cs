// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSModelTemplateComponent
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

[PXCacheName("Model Template Component")]
[Serializable]
public class FSModelTemplateComponent : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBInt(IsKey = true)]
  [PXDBDefault(typeof (INItemClass.itemClassID))]
  [PXParent(typeof (Select<INItemClass, Where<INItemClass.itemClassID, Equal<Current<FSModelTemplateComponent.modelTemplateID>>>>))]
  public virtual int? ModelTemplateID { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Active")]
  public virtual bool? Active { get; set; }

  [PXDBIdentity]
  public virtual int? ComponentID { get; set; }

  [PXDBString(15, IsKey = true, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXDefault]
  [PXUIField(DisplayName = "Component ID")]
  public virtual 
  #nullable disable
  string ComponentCD { get; set; }

  [PXDBLocalizableString(250, IsUnicode = true)]
  [PXUIField(DisplayName = "Description")]
  public virtual string Descr { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Item Class ID")]
  [PXDefault]
  [PXSelector(typeof (Search<INItemClass.itemClassID, Where<FSxEquipmentModelTemplate.equipmentItemClass, Equal<ListField_EquipmentItemClass.Component>>>), SubstituteKey = typeof (INItemClass.itemClassCD), DescriptionField = typeof (INItemClass.descr))]
  public virtual int? ClassID { get; set; }

  [PXDBInt(MinValue = 1)]
  [PXDefault(TypeCode.Int32, "1")]
  [PXUIField(DisplayName = "Quantity")]
  public virtual int? Qty { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField]
  public virtual bool? Optional { get; set; }

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

  public class PK : PrimaryKeyOf<FSModelTemplateComponent>.By<FSModelTemplateComponent.componentID>
  {
    public static FSModelTemplateComponent Find(
      PXGraph graph,
      int? componentID,
      PKFindOptions options = 0)
    {
      return (FSModelTemplateComponent) PrimaryKeyOf<FSModelTemplateComponent>.By<FSModelTemplateComponent.componentID>.FindBy(graph, (object) componentID, options);
    }
  }

  public class UK : 
    PrimaryKeyOf<FSModelTemplateComponent>.By<FSModelTemplateComponent.modelTemplateID, FSModelTemplateComponent.componentCD>
  {
    public static FSModelTemplateComponent Find(
      PXGraph graph,
      int? modelTemplateID,
      string componentCD,
      PKFindOptions options = 0)
    {
      return (FSModelTemplateComponent) PrimaryKeyOf<FSModelTemplateComponent>.By<FSModelTemplateComponent.modelTemplateID, FSModelTemplateComponent.componentCD>.FindBy(graph, (object) modelTemplateID, (object) componentCD, options);
    }
  }

  public static class FK
  {
    public class ModelTemplate : 
      PrimaryKeyOf<INItemClass>.By<INItemClass.itemClassID>.ForeignKeyOf<FSModelTemplateComponent>.By<FSModelTemplateComponent.modelTemplateID>
    {
    }

    public class ItemClass : 
      PrimaryKeyOf<INItemClass>.By<INItemClass.itemClassID>.ForeignKeyOf<FSModelTemplateComponent>.By<FSModelTemplateComponent.classID>
    {
    }
  }

  public abstract class modelTemplateID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSModelTemplateComponent.modelTemplateID>
  {
  }

  public abstract class active : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSModelTemplateComponent.active>
  {
  }

  public abstract class componentID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSModelTemplateComponent.componentID>
  {
  }

  public abstract class componentCD : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSModelTemplateComponent.componentCD>
  {
  }

  public abstract class descr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSModelTemplateComponent.descr>
  {
  }

  public abstract class classID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSModelTemplateComponent.classID>
  {
  }

  public abstract class qty : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSModelTemplateComponent.qty>
  {
  }

  public abstract class optional : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSModelTemplateComponent.optional>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FSModelTemplateComponent.noteID>
  {
  }

  public abstract class createdByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    FSModelTemplateComponent.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSModelTemplateComponent.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSModelTemplateComponent.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    FSModelTemplateComponent.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSModelTemplateComponent.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSModelTemplateComponent.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : 
    BqlType<
    #nullable enable
    IBqlByteArray, byte[]>.Field<
    #nullable disable
    FSModelTemplateComponent.Tstamp>
  {
  }
}
