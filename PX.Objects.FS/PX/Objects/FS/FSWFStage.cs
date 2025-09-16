// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSWFStage
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.Objects.FS;

[PXCacheName("Order Stage")]
[PXPrimaryGraph(typeof (WFStageMaint))]
[Serializable]
public class FSWFStage : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBInt(IsKey = true)]
  [PXUIField(DisplayName = "Workflow ID")]
  public virtual int? WFID { get; set; }

  [PXDBInt(IsKey = true)]
  [PXDBDefault(typeof (FSWFStage.wFStageID))]
  [PXUIField(DisplayName = "Parent Workflow Stage ID")]
  public virtual int? ParentWFStageID { get; set; }

  [PXDBIdentity]
  [PXUIField(DisplayName = "Workflow Stage ID")]
  public virtual int? WFStageID { get; set; }

  [PXDBString(30, IsUnicode = true, IsKey = true, InputMask = ">CCCCCCCCCCCCCCCCCCCCCCCCCCCCCC")]
  [PXDefault]
  [PXUIField]
  public virtual 
  #nullable disable
  string WFStageCD { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Allow Cancel")]
  public virtual bool? AllowCancel { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Allow Delete")]
  public virtual bool? AllowDelete { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Allow Update")]
  public virtual bool? AllowModify { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Allow Post")]
  public virtual bool? AllowPost { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Allow Complete")]
  public virtual bool? AllowComplete { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Allow Reopen")]
  public virtual bool? AllowReopen { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Allow Close")]
  public virtual bool? AllowClose { get; set; }

  [PXDBLocalizableString(60, IsUnicode = true)]
  [PXUIField]
  public virtual string Descr { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Require Reason")]
  public virtual bool? RequireReason { get; set; }

  [PXDBInt]
  [PXDefault(0)]
  [PXUIField(DisplayName = "Sort Order")]
  public virtual int? SortOrder { get; set; }

  [PXUIField(DisplayName = "NoteID")]
  [PXNote]
  public virtual Guid? NoteID { get; set; }

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

  public class PK : PrimaryKeyOf<FSWFStage>.By<FSWFStage.wFStageID>
  {
    public static FSWFStage Find(PXGraph graph, int? wFStageID, PKFindOptions options = 0)
    {
      return (FSWFStage) PrimaryKeyOf<FSWFStage>.By<FSWFStage.wFStageID>.FindBy(graph, (object) wFStageID, options);
    }
  }

  public class UK : 
    PrimaryKeyOf<FSWFStage>.By<FSWFStage.wFID, FSWFStage.parentWFStageID, FSWFStage.wFStageCD>
  {
    public static FSWFStage Find(
      PXGraph graph,
      int? wFID,
      int? parentWFStageID,
      string wFStageCD,
      PKFindOptions options = 0)
    {
      return (FSWFStage) PrimaryKeyOf<FSWFStage>.By<FSWFStage.wFID, FSWFStage.parentWFStageID, FSWFStage.wFStageCD>.FindBy(graph, (object) wFID, (object) parentWFStageID, (object) wFStageCD, options);
    }
  }

  public static class FK
  {
    public class ParentWorkflowFStage : 
      PrimaryKeyOf<FSWFStage>.By<FSWFStage.wFStageID>.ForeignKeyOf<FSWFStage>.By<FSWFStage.parentWFStageID>
    {
    }
  }

  public abstract class wFID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSWFStage.wFID>
  {
  }

  public abstract class parentWFStageID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSWFStage.parentWFStageID>
  {
  }

  public abstract class wFStageID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSWFStage.wFStageID>
  {
  }

  public abstract class wFStageCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSWFStage.wFStageCD>
  {
  }

  public abstract class allowCancel : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSWFStage.allowCancel>
  {
  }

  public abstract class allowDelete : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSWFStage.allowDelete>
  {
  }

  public abstract class allowModify : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSWFStage.allowModify>
  {
  }

  public abstract class allowPost : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSWFStage.allowPost>
  {
  }

  public abstract class allowComplete : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSWFStage.allowComplete>
  {
  }

  public abstract class allowReopen : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSWFStage.allowReopen>
  {
  }

  public abstract class allowClose : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSWFStage.allowClose>
  {
  }

  public abstract class descr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSWFStage.descr>
  {
  }

  public abstract class requireReason : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSWFStage.requireReason>
  {
  }

  public abstract class sortOrder : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSWFStage.sortOrder>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FSWFStage.noteID>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FSWFStage.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSWFStage.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSWFStage.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FSWFStage.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSWFStage.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSWFStage.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  FSWFStage.Tstamp>
  {
  }
}
