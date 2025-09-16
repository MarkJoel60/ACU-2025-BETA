// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.CSAttributeDetail
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.Objects.CS;

[PXCacheName("Attribute Detail")]
public class CSAttributeDetail : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  public const int ParameterIdLength = 10;
  protected 
  #nullable disable
  string _AttributeID;
  protected string _ValueID;
  protected string _Description;
  protected short? _SortOrder;
  protected bool? _Disabled;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  [PXDBString(10, IsKey = true)]
  [PXDBDefault(typeof (CSAttribute.attributeID))]
  [PXUIField(DisplayName = "Attribute ID")]
  [PXParent(typeof (Select<CSAttribute, Where<CSAttribute.attributeID, Equal<Current<CSAttributeDetail.attributeID>>>>))]
  public virtual string AttributeID
  {
    get => this._AttributeID;
    set => this._AttributeID = value;
  }

  [PXDBString(10, InputMask = "", IsUnicode = true, IsKey = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Value ID")]
  public virtual string ValueID
  {
    get => this._ValueID;
    set => this._ValueID = value;
  }

  [PXDBLocalizableString(60, IsUnicode = true)]
  [PXUIField(DisplayName = "Description")]
  public virtual string Description
  {
    get => this._Description;
    set => this._Description = value;
  }

  [PXDBShort]
  [PXUIField(DisplayName = "Sort Order")]
  public virtual short? SortOrder
  {
    get => this._SortOrder;
    set => this._SortOrder = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField]
  public virtual bool? Disabled
  {
    get => this._Disabled;
    set => this._Disabled = value;
  }

  [PXNote]
  public virtual Guid? NoteID { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID
  {
    get => this._CreatedByID;
    set => this._CreatedByID = value;
  }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID
  {
    get => this._CreatedByScreenID;
    set => this._CreatedByScreenID = value;
  }

  [PXDBCreatedDateTime]
  public virtual DateTime? CreatedDateTime
  {
    get => this._CreatedDateTime;
    set => this._CreatedDateTime = value;
  }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID
  {
    get => this._LastModifiedByID;
    set => this._LastModifiedByID = value;
  }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID
  {
    get => this._LastModifiedByScreenID;
    set => this._LastModifiedByScreenID = value;
  }

  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime
  {
    get => this._LastModifiedDateTime;
    set => this._LastModifiedDateTime = value;
  }

  public class PK : 
    PrimaryKeyOf<CSAttributeDetail>.By<CSAttributeDetail.attributeID, CSAttributeDetail.valueID>
  {
    public static CSAttributeDetail Find(
      PXGraph graph,
      string attributeID,
      string valueID,
      PKFindOptions options = 0)
    {
      return (CSAttributeDetail) PrimaryKeyOf<CSAttributeDetail>.By<CSAttributeDetail.attributeID, CSAttributeDetail.valueID>.FindBy(graph, (object) attributeID, (object) valueID, options);
    }
  }

  public static class FK
  {
    public class Attribute : 
      PrimaryKeyOf<CSAttribute>.By<CSAttribute.attributeID>.ForeignKeyOf<CSAttributeDetail>.By<CSAttributeDetail.attributeID>
    {
    }
  }

  public abstract class attributeID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CSAttributeDetail.attributeID>
  {
  }

  public abstract class valueID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CSAttributeDetail.valueID>
  {
  }

  public abstract class description : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CSAttributeDetail.description>
  {
  }

  public abstract class sortOrder : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  CSAttributeDetail.sortOrder>
  {
  }

  public abstract class disabled : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CSAttributeDetail.disabled>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CSAttributeDetail.noteID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  CSAttributeDetail.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CSAttributeDetail.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CSAttributeDetail.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CSAttributeDetail.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    CSAttributeDetail.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CSAttributeDetail.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CSAttributeDetail.lastModifiedDateTime>
  {
  }
}
