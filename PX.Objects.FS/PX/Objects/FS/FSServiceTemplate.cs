// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSServiceTemplate
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.SM;
using System;

#nullable enable
namespace PX.Objects.FS;

[PXPrimaryGraph(typeof (ServiceTemplateMaint))]
[Serializable]
public class FSServiceTemplate : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBIdentity]
  [PXUIField(Enabled = false)]
  public virtual int? ServiceTemplateID { get; set; }

  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = ">CCCCCCCCCCCCCCC", IsFixed = true)]
  [PXDefault]
  [NormalizeWhiteSpace]
  [PXUIField]
  [PXSelector(typeof (Search<FSServiceTemplate.serviceTemplateCD>), DescriptionField = typeof (FSServiceTemplate.descr))]
  public virtual 
  #nullable disable
  string ServiceTemplateCD { get; set; }

  [PXDBString(60, IsUnicode = true)]
  [PXDefault]
  [PXUIField]
  public virtual string Descr { get; set; }

  [PXUIField(DisplayName = "NoteID")]
  [PXNote]
  public virtual Guid? NoteID { get; set; }

  [PXDBString(4, IsFixed = true, InputMask = ">AAAA")]
  [PXDefault(typeof (Coalesce<Search<FSxUserPreferences.dfltSrvOrdType, Where<UserPreferences.userID, Equal<CurrentValue<AccessInfo.userID>>>>, Search<FSSetup.dfltSrvOrdType>>))]
  [PXUIField(DisplayName = "Service Order Type")]
  [FSSelectorActiveSrvOrdType]
  public virtual string SrvOrdType { get; set; }

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

  public class PK : PrimaryKeyOf<FSServiceTemplate>.By<FSServiceTemplate.serviceTemplateID>
  {
    public static FSServiceTemplate Find(
      PXGraph graph,
      int? serviceTemplateID,
      PKFindOptions options = 0)
    {
      return (FSServiceTemplate) PrimaryKeyOf<FSServiceTemplate>.By<FSServiceTemplate.serviceTemplateID>.FindBy(graph, (object) serviceTemplateID, options);
    }
  }

  public class UK : PrimaryKeyOf<FSServiceTemplate>.By<FSServiceTemplate.serviceTemplateCD>
  {
    public static FSServiceTemplate Find(
      PXGraph graph,
      string serviceTemplateCD,
      PKFindOptions options = 0)
    {
      return (FSServiceTemplate) PrimaryKeyOf<FSServiceTemplate>.By<FSServiceTemplate.serviceTemplateCD>.FindBy(graph, (object) serviceTemplateCD, options);
    }
  }

  public static class FK
  {
    public class ServiceOrderType : 
      PrimaryKeyOf<FSSrvOrdType>.By<FSSrvOrdType.srvOrdType>.ForeignKeyOf<FSServiceTemplate>.By<FSServiceTemplate.srvOrdType>
    {
    }
  }

  public abstract class serviceTemplateID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSServiceTemplate.serviceTemplateID>
  {
  }

  public abstract class serviceTemplateCD : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSServiceTemplate.serviceTemplateCD>
  {
  }

  public abstract class descr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSServiceTemplate.descr>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FSServiceTemplate.noteID>
  {
  }

  public abstract class srvOrdType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSServiceTemplate.srvOrdType>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FSServiceTemplate.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSServiceTemplate.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSServiceTemplate.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    FSServiceTemplate.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSServiceTemplate.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSServiceTemplate.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  FSServiceTemplate.Tstamp>
  {
  }
}
