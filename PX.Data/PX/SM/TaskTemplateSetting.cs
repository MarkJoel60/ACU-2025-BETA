// Decompiled with JetBrains decompiler
// Type: PX.SM.TaskTemplateSetting
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.SM;

[PXCacheName("Task Template Setting")]
[Serializable]
public class TaskTemplateSetting : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBInt(IsKey = true)]
  [PXDefault(typeof (TaskTemplate.taskTemplateID))]
  [PXParent(typeof (Select<TaskTemplate, Where<TaskTemplate.taskTemplateID, Equal<Current<TaskTemplateSetting.taskTemplateID>>>>))]
  [PXDBChildIdentity(typeof (TaskTemplate.taskTemplateID))]
  public virtual int? TaskTemplateID { get; set; }

  [PXDBShort(IsKey = true)]
  [PXDefault]
  [PXLineNbr(typeof (TaskTemplate.fieldCntr))]
  [PXParent(typeof (Select<TaskTemplate, Where<TaskTemplate.taskTemplateID, Equal<Current<TaskTemplateSetting.taskTemplateID>>>>))]
  public virtual short? LineNbr { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Active")]
  public virtual bool? IsActive { get; set; }

  [PXDefault]
  [PXDBString(128 /*0x80*/, InputMask = "", IsUnicode = true)]
  [PXUIField(DisplayName = "Field Name", Enabled = false)]
  public virtual 
  #nullable disable
  string FieldName { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "From Schema")]
  public virtual bool? FromSchema { get; set; }

  [PXFormulaEditor(DisplayName = "Value", IsDBField = false)]
  [PXFormulaEditor.AddOperators]
  [PXFormulaEditor.AddFunctions]
  [PXFormulaEditor_AddInternalFields]
  public virtual string Value { get; set; }

  [PXNote]
  public virtual Guid? NoteID { get; set; }

  [PXDBCreatedByID]
  [PXUIField(DisplayName = "Created By")]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  [PXUIField(DisplayName = "Created On", Enabled = false, IsReadOnly = true)]
  public virtual System.DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  [PXUIField(DisplayName = "Last Modified By")]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  [PXUIField(DisplayName = "Last Modified On", Enabled = false, IsReadOnly = true)]
  public virtual System.DateTime? LastModifiedDateTime { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  public abstract class taskTemplateID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    TaskTemplateSetting.taskTemplateID>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  TaskTemplateSetting.lineNbr>
  {
  }

  public abstract class isActive : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  TaskTemplateSetting.isActive>
  {
  }

  public abstract class fieldName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TaskTemplateSetting.fieldName>
  {
  }

  public abstract class fromSchema : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  TaskTemplateSetting.fromSchema>
  {
  }

  public abstract class value : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TaskTemplateSetting.value>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  TaskTemplateSetting.noteID>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  TaskTemplateSetting.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    TaskTemplateSetting.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    TaskTemplateSetting.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    TaskTemplateSetting.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    TaskTemplateSetting.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    TaskTemplateSetting.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  TaskTemplateSetting.Tstamp>
  {
  }
}
