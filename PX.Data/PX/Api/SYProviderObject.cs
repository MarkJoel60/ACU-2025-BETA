// Decompiled with JetBrains decompiler
// Type: PX.Api.SYProviderObject
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.Api;

[PXCacheName("Provider Object")]
public class SYProviderObject : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBGuid(false, IsKey = true)]
  [PXDefault(typeof (SYProvider.providerID))]
  public virtual Guid? ProviderID { get; set; }

  [PXDBShort(IsKey = true)]
  [PXDefault]
  [PXLineNbr(typeof (SYProvider.objectCntr))]
  [PXParent(typeof (Select<SYProvider, Where<SYProvider.providerID, Equal<Current<SYProviderObject.providerID>>>>))]
  public virtual short? LineNbr { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Active")]
  public virtual bool? IsActive { get; set; }

  [PXDBString(128 /*0x80*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Object")]
  [PXDefault]
  public virtual 
  #nullable disable
  string Name { get; set; }

  [PXDBString(128 /*0x80*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Description", Enabled = false)]
  public virtual string DisplayName { get; set; }

  [PXDBString(4000, IsUnicode = true)]
  [PXUIField(DisplayName = "Command")]
  public virtual string Command { get; set; }

  [PXDBShort]
  [PXDefault(0)]
  public virtual short? FieldCntr { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  public virtual bool? IsCustom { get; set; }

  [PXNote]
  public virtual Guid? NoteID { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  public virtual System.DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  public virtual System.DateTime? LastModifiedDateTime { get; set; }

  [PXDBTimestamp]
  public virtual byte[] TStamp { get; set; }

  public class PK : 
    PrimaryKeyOf<SYProviderObject>.By<SYProviderObject.providerID, SYProviderObject.lineNbr>
  {
    public static SYProviderObject Find(
      PXGraph graph,
      Guid? providerID,
      short? lineNbr,
      PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<SYProviderObject>.By<SYProviderObject.providerID, SYProviderObject.lineNbr>.FindBy(graph, (object) providerID, (object) lineNbr, options);
    }
  }

  public static class FK
  {
    public class Provider : 
      PrimaryKeyOf<SYProvider>.By<SYProvider.providerID>.ForeignKeyOf<SYProviderObject>.By<SYProviderObject.providerID>
    {
    }
  }

  public abstract class providerID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SYProviderObject.providerID>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  SYProviderObject.lineNbr>
  {
  }

  public abstract class isActive : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SYProviderObject.isActive>
  {
  }

  public abstract class name : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SYProviderObject.name>
  {
  }

  public abstract class displayName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SYProviderObject.displayName>
  {
  }

  public abstract class command : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SYProviderObject.command>
  {
  }

  public abstract class fieldCntr : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  SYProviderObject.fieldCntr>
  {
  }

  public abstract class isCustom : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SYProviderObject.isCustom>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SYProviderObject.noteID>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SYProviderObject.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SYProviderObject.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    SYProviderObject.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    SYProviderObject.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SYProviderObject.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    SYProviderObject.lastModifiedDateTime>
  {
  }

  public abstract class tStamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  SYProviderObject.tStamp>
  {
  }
}
