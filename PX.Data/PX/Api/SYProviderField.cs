// Decompiled with JetBrains decompiler
// Type: PX.Api.SYProviderField
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Api;

[Serializable]
public class SYProviderField : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  private bool _isMapped = true;

  [PXDBGuid(false, IsKey = true)]
  [PXDefault(typeof (SYProvider.providerID))]
  public virtual Guid? ProviderID { get; set; }

  [PXDBString(128 /*0x80*/, IsUnicode = true, IsKey = true)]
  [PXDefault(typeof (SYProviderObject.name))]
  public virtual 
  #nullable disable
  string ObjectName { get; set; }

  [PXDBShort]
  [PXDefault]
  [PXLineNbr(typeof (SYProviderObject.fieldCntr))]
  [PXParent(typeof (Select<SYProviderObject, Where<SYProviderObject.providerID, Equal<Current<SYProviderField.providerID>>, And<SYProviderObject.name, Equal<Current<SYProviderField.objectName>>>>>))]
  public virtual short? LineNbr { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Active")]
  public virtual bool? IsActive { get; set; }

  [PXDBString(128 /*0x80*/, IsUnicode = true, IsKey = true, InputMask = "")]
  [PXUIField(DisplayName = "Field")]
  public virtual string Name { get; set; }

  [PXDBString(128 /*0x80*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Description", Enabled = false)]
  public virtual string DisplayName { get; set; }

  [PXDBString(4000, IsUnicode = true)]
  [PXUIField(DisplayName = "Command")]
  public virtual string Command { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Key")]
  public virtual bool? IsKey { get; set; }

  [PXDBString(128 /*0x80*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Data Type")]
  [PXStringList]
  public virtual string DataType { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Data Length")]
  public virtual int? DataLength { get; set; }

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

  public bool? IsMapped
  {
    get => new bool?(this._isMapped);
    set => this._isMapped = ((int) value ?? 1) != 0;
  }

  internal SYProviderField Clone() => (SYProviderField) this.MemberwiseClone();

  public abstract class providerID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SYProviderField.providerID>
  {
  }

  public abstract class objectName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SYProviderField.objectName>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  SYProviderField.lineNbr>
  {
  }

  public abstract class isActive : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SYProviderField.isActive>
  {
  }

  public abstract class name : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SYProviderField.name>
  {
  }

  public abstract class displayName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SYProviderField.displayName>
  {
  }

  public abstract class command : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SYProviderField.command>
  {
  }

  public abstract class isKey : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SYProviderField.isKey>
  {
  }

  public abstract class dataType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SYProviderField.dataType>
  {
  }

  public abstract class dataLength : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SYProviderField.dataLength>
  {
  }

  public abstract class isCustom : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SYProviderField.isCustom>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SYProviderField.noteID>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SYProviderField.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SYProviderField.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    SYProviderField.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    SYProviderField.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SYProviderField.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    SYProviderField.lastModifiedDateTime>
  {
  }

  public abstract class tStamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  SYProviderField.tStamp>
  {
  }
}
