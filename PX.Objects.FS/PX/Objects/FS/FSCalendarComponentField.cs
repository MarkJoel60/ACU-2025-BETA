// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSCalendarComponentField
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.SM;
using System;

#nullable enable
namespace PX.Objects.FS;

[Serializable]
public class FSCalendarComponentField : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage, ISortOrder
{
  protected int? _LineNbr;

  [PXDefault]
  [PXDBString(2, IsFixed = true, IsKey = true)]
  [PXUIField(DisplayName = "Component Type", Enabled = false)]
  [ListField_ComponentType.List]
  public virtual 
  #nullable disable
  string ComponentType { get; set; }

  [PXInt]
  [PXUIField(Visible = false, Enabled = false)]
  public virtual int? LineNbr
  {
    get
    {
      if (!this._LineNbr.HasValue)
        this._LineNbr = this.SortOrder;
      return this._LineNbr;
    }
    set => this._LineNbr = value;
  }

  [PXDBInt]
  [PXDefault(2147483647 /*0x7FFFFFFF*/)]
  [PXUIField(Visible = false, Enabled = false)]
  public virtual int? SortOrder { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Visible")]
  public bool? IsActive { get; set; }

  [PXDefault]
  [PXDBString(InputMask = "", IsUnicode = false, IsKey = true)]
  [PXUIField]
  [PXStringList(new string[] {null}, new string[] {""}, ExclusiveValues = false)]
  public virtual string ObjectName { get; set; }

  [PXDefault]
  [PXDBString(InputMask = "", IsUnicode = false, IsKey = true)]
  [PXUIField(DisplayName = "Field Name")]
  [PXStringList(new string[] {null}, new string[] {""}, ExclusiveValues = false)]
  public virtual string FieldName { get; set; }

  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Icon")]
  [PXIconsList]
  public virtual string ImageUrl { get; set; }

  [PXDBCreatedByID]
  [PXUIField(DisplayName = "CreatedByID")]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  [PXUIField(DisplayName = "CreatedByScreenID")]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  [PXUIField(DisplayName = "CreatedDateTime")]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  [PXUIField(DisplayName = "LastModifiedByID")]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  [PXUIField(DisplayName = "LastModifiedByScreenID")]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  [PXUIField(DisplayName = "LastModifiedDateTime")]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  [PXDBTimestamp]
  [PXUIField(DisplayName = "tstamp")]
  public virtual byte[] tstamp { get; set; }

  public abstract class componentType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSCalendarComponentField.componentType>
  {
    public abstract class Values : ListField_ComponentType
    {
    }
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSCalendarComponentField.lineNbr>
  {
  }

  public abstract class sortOrder : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSCalendarComponentField.sortOrder>
  {
  }

  public abstract class isActive : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSCalendarComponentField.isActive>
  {
  }

  public abstract class objectName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSCalendarComponentField.objectName>
  {
  }

  public abstract class fieldName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSCalendarComponentField.fieldName>
  {
  }

  public abstract class imageUrl : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSCalendarComponentField.imageUrl>
  {
  }

  public abstract class createdByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    FSCalendarComponentField.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSCalendarComponentField.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSCalendarComponentField.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    FSCalendarComponentField.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSCalendarComponentField.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSCalendarComponentField.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : 
    BqlType<
    #nullable enable
    IBqlByteArray, byte[]>.Field<
    #nullable disable
    FSCalendarComponentField.Tstamp>
  {
  }
}
