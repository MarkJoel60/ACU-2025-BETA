// Decompiled with JetBrains decompiler
// Type: PX.Data.Reports.UserReport
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Data.Reports;

[PXAutoSave]
[Serializable]
public class UserReport : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  private 
  #nullable disable
  string _ReportFileName;

  [PXDBString(50, IsKey = true, IsFixed = false, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "ReportFileName", Visibility = PXUIVisibility.SelectorVisible)]
  public string ReportFileName
  {
    get => this._ReportFileName;
    set => this._ReportFileName = value == null ? (string) null : value.ToLowerInvariant();
  }

  [PXDBInt(IsKey = true)]
  [PXUIField(DisplayName = "Version", IsReadOnly = true, Enabled = false)]
  public int? Version { get; set; }

  [PXDBString(50, IsKey = false, IsFixed = false, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Description")]
  public string Description { get; set; }

  [PXDBDate(IsKey = false)]
  [PXUIField(DisplayName = "Created", IsReadOnly = true, Enabled = false)]
  public System.DateTime? DateCreated { get; set; }

  [PXDBBool(IsKey = false)]
  [PXUIField(DisplayName = "Active")]
  public bool? IsActive { get; set; }

  [PXDBString(IsKey = false, IsFixed = false, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Xml")]
  public virtual string Xml { get; set; }

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

  public abstract class reportFileName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  UserReport.reportFileName>
  {
  }

  public abstract class version : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  UserReport.version>
  {
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  UserReport.description>
  {
  }

  public abstract class dateCreated : BqlType<
  #nullable enable
  IBqlDateTime, System.DateTime>.Field<
  #nullable disable
  UserReport.dateCreated>
  {
  }

  public abstract class isActive : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  UserReport.isActive>
  {
  }

  public abstract class xml : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  UserReport.xml>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  UserReport.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    UserReport.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    UserReport.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  UserReport.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    UserReport.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    UserReport.lastModifiedDateTime>
  {
  }
}
