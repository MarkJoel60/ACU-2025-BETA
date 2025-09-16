// Decompiled with JetBrains decompiler
// Type: PX.SM.TaskTemplate
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.SM;

[PXPrimaryGraph(typeof (TaskTemplateMaint))]
[PXCacheName("Task Template")]
[Serializable]
public class TaskTemplate : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage, ISentByEvent
{
  internal const 
  #nullable disable
  string EventSubscriberType = "TASK";

  public Guid? GetHandlerId() => this.NoteID;

  [PXDBIdentity(IsKey = true)]
  [PXUIField(DisplayName = "Template ID")]
  [PXSelector(typeof (TaskTemplate.taskTemplateID), new System.Type[] {typeof (TaskTemplate.taskTemplateID), typeof (TaskTemplate.summary), typeof (TaskTemplate.screenID)}, DescriptionField = typeof (TaskTemplate.nameForDescription))]
  public virtual int? TaskTemplateID { get; set; }

  [PXDefault]
  [PXDBString(255 /*0xFF*/, InputMask = "", IsUnicode = true)]
  [PXUIField(DisplayName = "Description", Visibility = PXUIVisibility.SelectorVisible)]
  public virtual string Name { get; set; }

  [PXString(255 /*0xFF*/, InputMask = "", IsUnicode = true)]
  public virtual string NameForDescription
  {
    get
    {
      int? taskTemplateId = this.TaskTemplateID;
      int num = 0;
      return !(taskTemplateId.GetValueOrDefault() < num & taskTemplateId.HasValue) ? this.Name : (string) null;
    }
  }

  [PXDefault]
  [PXDBString(256 /*0x0100*/, InputMask = "", IsUnicode = true)]
  [PXUIField(DisplayName = "Summary", Visibility = PXUIVisibility.SelectorVisible)]
  public virtual string Summary { get; set; }

  [PXDefault]
  [PXDBString(8, IsFixed = true, InputMask = "CC.CC.CC.CC")]
  [PXUIField(DisplayName = "Screen ID", Visibility = PXUIVisibility.SelectorVisible)]
  [PXSiteMapNodeSelector]
  public virtual string ScreenID { get; set; }

  [PXDBString(128 /*0x80*/, InputMask = "")]
  [PXUIField(DisplayName = "Owner")]
  public virtual string OwnerName { get; set; }

  [PXDBText(IsUnicode = true)]
  [PXUIField(DisplayName = "Body")]
  public virtual string Body { get; set; }

  [PXSelector(typeof (Search<Locale.localeName, Where<Locale.isActive, Equal<PX.Data.True>>>), DescriptionField = typeof (Locale.translatedName))]
  [PXDBString(10, IsUnicode = true)]
  [PXUIField(DisplayName = "Locale")]
  public virtual string LocaleName { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Attach Activity")]
  public bool? AttachActivity { get; set; }

  [PXDBString(255 /*0xFF*/)]
  [PXUIField(DisplayName = "Link-To Entity")]
  public string RefNoteID { get; set; }

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

  [PXDBShort]
  [PXDefault(0)]
  public virtual short? FieldCntr { get; set; }

  [PXInternalUseOnly]
  string ISentByEvent.SubscriberType
  {
    get => "TASK";
    set
    {
    }
  }

  /// <summary>
  /// A service field. It is used to hide "Created by Events" tab on SM204005 page.
  /// </summary>
  [PXBool]
  [PXUIField(Visible = false)]
  [PXDependsOnFields(new System.Type[] {typeof (TaskTemplate.taskTemplateID)})]
  public virtual bool ShowCreatedByEventsTabExpr
  {
    get
    {
      if (!this.TaskTemplateID.HasValue)
        return false;
      int? taskTemplateId = this.TaskTemplateID;
      int num = 0;
      return taskTemplateId.GetValueOrDefault() > num & taskTemplateId.HasValue;
    }
  }

  public abstract class taskTemplateID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  TaskTemplate.taskTemplateID>
  {
  }

  public abstract class name : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TaskTemplate.name>
  {
  }

  public abstract class nameForDescription : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TaskTemplate.name>
  {
  }

  public abstract class summary : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TaskTemplate.summary>
  {
  }

  public abstract class screenID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TaskTemplate.screenID>
  {
  }

  public abstract class ownerName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TaskTemplate.ownerName>
  {
  }

  public abstract class body : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TaskTemplate.body>
  {
  }

  public abstract class localeName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TaskTemplate.localeName>
  {
  }

  public abstract class attachActivity : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  TaskTemplate.attachActivity>
  {
  }

  public abstract class refNoteID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TaskTemplate.refNoteID>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  TaskTemplate.noteID>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  TaskTemplate.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    TaskTemplate.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    TaskTemplate.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    TaskTemplate.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    TaskTemplate.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    TaskTemplate.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  TaskTemplate.Tstamp>
  {
  }

  public abstract class fieldCntr : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  TaskTemplate.fieldCntr>
  {
  }
}
