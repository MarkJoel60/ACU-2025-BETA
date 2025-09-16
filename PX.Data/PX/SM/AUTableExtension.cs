// Decompiled with JetBrains decompiler
// Type: PX.SM.AUTableExtension
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.SM;

[Serializable]
public class AUTableExtension : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _TableName;
  protected Guid? _ProjectID;
  protected string _FieldName;
  protected int? _StorageType;
  protected bool? _IsOverride;
  protected bool? _Inherit;
  protected int? _SortOrder;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected System.DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected System.DateTime? _LastModifiedDateTime;
  protected byte[] _TStamp;

  [PXDBString(128 /*0x80*/, IsKey = true)]
  [PXDefault(typeof (AUTableDefinition.tableName))]
  public virtual string TableName
  {
    get => this._TableName;
    set => this._TableName = value;
  }

  [PXDBGuid(false, IsKey = true)]
  [PXDefault(typeof (AUTableDefinition.projectID))]
  public virtual Guid? ProjectID
  {
    get => this._ProjectID;
    set => this._ProjectID = value;
  }

  [PXDBString(128 /*0x80*/, IsKey = true, IsUnicode = true, InputMask = "")]
  [PXDefault]
  [PXParent(typeof (Select<AUTableDefinition, Where<AUTableDefinition.tableName, Equal<Current<AUTableExtension.tableName>>, And<AUTableDefinition.projectID, Equal<Current<AUTableExtension.projectID>>>>>))]
  [PXUIField(DisplayName = "Field Name")]
  public virtual string FieldName
  {
    get => this._FieldName;
    set => this._FieldName = value;
  }

  [PXDBInt]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Storage Type")]
  [PXIntList(new int[] {0, 1, 2, 3, 4, 5}, new string[] {"Virtual", "Attribute", "Table", "Base", "Separate Table", "Projection"})]
  public virtual int? StorageType
  {
    get => this._StorageType;
    set => this._StorageType = value;
  }

  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Override", Enabled = false)]
  [PXDBCalced(typeof (Switch<Case<Where<AUTableExtension.projectID, Equal<CurrentValue<AUTableDefinition.projectID>>>, PX.Data.True>, False>), typeof (bool))]
  [PXFormula(typeof (Switch<Case<Where<AUTableExtension.projectID, Equal<CurrentValue<AUTableDefinition.projectID>>>, PX.Data.True>, False>))]
  public virtual bool? IsOverride
  {
    get => this._IsOverride;
    set => this._IsOverride = value;
  }

  [PXBool]
  [PXDefault(false)]
  public virtual bool? Inherit
  {
    get => this._Inherit;
    set => this._Inherit = value;
  }

  [PXInt]
  public virtual int? SortOrder
  {
    get => this._SortOrder;
    set => this._SortOrder = value;
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
  public virtual System.DateTime? CreatedDateTime
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
  public virtual System.DateTime? LastModifiedDateTime
  {
    get => this._LastModifiedDateTime;
    set => this._LastModifiedDateTime = value;
  }

  [PXDBTimestamp]
  public virtual byte[] TStamp
  {
    get => this._TStamp;
    set => this._TStamp = value;
  }

  public abstract class tableName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUTableExtension.tableName>
  {
  }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  AUTableExtension.projectID>
  {
  }

  public abstract class fieldName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUTableExtension.fieldName>
  {
  }

  public abstract class storageType : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  AUTableExtension.storageType>
  {
  }

  public abstract class isOverride : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  AUTableExtension.isOverride>
  {
  }

  public abstract class inherit : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  AUTableExtension.inherit>
  {
  }

  public abstract class sortOrder : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  AUTableExtension.sortOrder>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  AUTableExtension.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUTableExtension.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    AUTableExtension.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    AUTableExtension.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUTableExtension.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    AUTableExtension.lastModifiedDateTime>
  {
  }

  public abstract class tStamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  AUTableExtension.tStamp>
  {
  }
}
