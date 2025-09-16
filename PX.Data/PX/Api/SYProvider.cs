// Decompiled with JetBrains decompiler
// Type: PX.Api.SYProvider
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

[PXCacheName("Provider")]
[PXPrimaryGraph(typeof (SYProviderMaint))]
public class SYProvider : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBGuid(false)]
  [PXDefault]
  public virtual Guid? ProviderID { get; set; }

  [PXDBString(128 /*0x80*/, InputMask = "", IsUnicode = true, IsKey = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Name", Visibility = PXUIVisibility.SelectorVisible)]
  [PXSelector(typeof (SYProvider.name))]
  public virtual 
  #nullable disable
  string Name { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Active")]
  public virtual bool? IsActive { get; set; }

  [PXDBString(128 /*0x80*/)]
  [PXUIField(DisplayName = "Provider Type", Visibility = PXUIVisibility.SelectorVisible)]
  [PXDefault]
  [PXSYProviderSelector]
  public virtual string ProviderType { get; set; }

  [PXDBShort]
  [PXDefault(0)]
  public virtual short? ObjectCntr { get; set; }

  [PXDBShort]
  [PXDefault(0)]
  public virtual short? ParameterCntr { get; set; }

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

  public class PK : PrimaryKeyOf<SYProvider>.By<SYProvider.providerID>
  {
    public static SYProvider Find(PXGraph graph, Guid? providerID, PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<SYProvider>.By<SYProvider.providerID>.FindBy(graph, (object) providerID, options);
    }
  }

  public abstract class providerID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SYProvider.providerID>
  {
  }

  public abstract class name : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SYProvider.name>
  {
  }

  public abstract class isActive : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SYProvider.isActive>
  {
  }

  public abstract class providerType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SYProvider.providerType>
  {
  }

  public abstract class objectCntr : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  SYProvider.objectCntr>
  {
  }

  public abstract class parameterCntr : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  SYProvider.parameterCntr>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SYProvider.noteID>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SYProvider.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SYProvider.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    SYProvider.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SYProvider.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SYProvider.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    SYProvider.lastModifiedDateTime>
  {
  }

  public abstract class tStamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  SYProvider.tStamp>
  {
  }
}
