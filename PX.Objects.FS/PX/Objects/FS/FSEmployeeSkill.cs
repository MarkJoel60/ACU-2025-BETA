// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSEmployeeSkill
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.EP;
using System;

#nullable enable
namespace PX.Objects.FS;

[Serializable]
public class FSEmployeeSkill : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBInt(IsKey = true)]
  [PXUIField(DisplayName = "Employee ID")]
  [PXParent(typeof (Select<EPEmployee, Where<EPEmployee.bAccountID, Equal<Current<FSEmployeeSkill.employeeID>>>>))]
  [PXDBDefault(typeof (EPEmployee.bAccountID))]
  public virtual int? EmployeeID { get; set; }

  [PXDBInt(IsKey = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Skill ID")]
  [PXSelector(typeof (FSSkill.skillID), SubstituteKey = typeof (FSSkill.skillCD), DescriptionField = typeof (FSSkill.descr))]
  public virtual int? SkillID { get; set; }

  [PXUIField(DisplayName = "CreatedByID")]
  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXUIField(DisplayName = "CreatedByScreenID")]
  [PXDBCreatedByScreenID]
  public virtual 
  #nullable disable
  string CreatedByScreenID { get; set; }

  [PXUIField(DisplayName = "CreatedDateTime")]
  [PXDBCreatedDateTime]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXUIField(DisplayName = "LastModifiedByID")]
  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXUIField(DisplayName = "LastModifiedByScreenID")]
  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXUIField(DisplayName = "LastModifiedDateTime")]
  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  public abstract class employeeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSEmployeeSkill.employeeID>
  {
  }

  public abstract class skillID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSEmployeeSkill.skillID>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FSEmployeeSkill.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSEmployeeSkill.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSEmployeeSkill.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    FSEmployeeSkill.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSEmployeeSkill.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSEmployeeSkill.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  FSEmployeeSkill.Tstamp>
  {
  }
}
