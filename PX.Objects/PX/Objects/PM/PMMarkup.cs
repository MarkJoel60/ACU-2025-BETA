// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMMarkup
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.GL;
using PX.Objects.IN;
using System;
using System.Diagnostics.CodeAnalysis;

#nullable enable
namespace PX.Objects.PM;

[ExcludeFromCodeCoverage]
[PXCacheName("Markup")]
[Serializable]
public class PMMarkup : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected Guid? _NoteID;
  protected 
  #nullable disable
  byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  [PXDefault]
  [PXDBInt(IsKey = true)]
  public virtual int? ProjectID { get; set; }

  [PXDBInt(IsKey = true)]
  [PXLineNbr(typeof (PMProject))]
  [PXParent(typeof (Select<PMProject, Where<PMProject.contractID, Equal<Current<PMMarkup.projectID>>>>))]
  [PXUIField(DisplayName = "Line Nbr.", Visible = false)]
  public virtual int? LineNbr { get; set; }

  [PXUIField(DisplayName = "Type")]
  [PXDefault("P")]
  [PMMarkupLineType.List]
  [PXDBString(1)]
  public virtual string Type { get; set; }

  [PXDBString(256 /*0x0100*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Description")]
  [PXFieldDescription]
  public virtual string Description { get; set; }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Value")]
  public virtual Decimal? Value { get; set; }

  [PXParent(typeof (Select<PMTask, Where<PMTask.projectID, Equal<Current<PMMarkup.projectID>>, And<PMTask.taskID, Equal<Current<PMMarkup.taskID>>>>>))]
  [ProjectTask(typeof (PMMarkup.projectID), AlwaysEnabled = true, AllowNull = true, DirtyRead = true)]
  [PXForeignReference(typeof (CompositeKey<Field<PMMarkup.projectID>.IsRelatedTo<PMTask.projectID>, Field<PMMarkup.taskID>.IsRelatedTo<PMTask.taskID>>))]
  public virtual int? TaskID { get; set; }

  [PXRestrictor(typeof (Where<PMAccountGroup.isActive, Equal<True>>), "The {0} account group is inactive. You can activate it on the Account Groups (PM201000) form.", new System.Type[] {typeof (PMAccountGroup.groupCD)})]
  [AccountGroup(typeof (Where<PMAccountGroup.type, Equal<AccountType.income>>))]
  [PXForeignReference(typeof (Field<PMMarkup.accountGroupID>.IsRelatedTo<PMAccountGroup.groupID>))]
  public virtual int? AccountGroupID { get; set; }

  [CostCode(null, typeof (PMMarkup.taskID), "I", typeof (PMMarkup.accountGroupID), AllowNullValue = true)]
  public virtual int? CostCodeID { get; set; }

  [PXDBInt]
  [PXUIField(DisplayName = "Inventory ID")]
  [PMInventorySelector]
  public virtual int? InventoryID { get; set; }

  [PXNote]
  public virtual Guid? NoteID
  {
    get => this._NoteID;
    set => this._NoteID = value;
  }

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

  [PXUIField(DisplayName = "Created On", Enabled = false, IsReadOnly = true)]
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

  [PXUIField(DisplayName = "Last Modified On", Enabled = false, IsReadOnly = true)]
  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime
  {
    get => this._LastModifiedDateTime;
    set => this._LastModifiedDateTime = value;
  }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMMarkup.projectID>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMMarkup.lineNbr>
  {
  }

  public abstract class type : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMMarkup.type>
  {
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMMarkup.description>
  {
  }

  public abstract class value : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMMarkup.value>
  {
  }

  public abstract class taskID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMMarkup.taskID>
  {
  }

  public abstract class accountGroupID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMMarkup.accountGroupID>
  {
  }

  public abstract class costCodeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMMarkup.costCodeID>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMMarkup.inventoryID>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  PMMarkup.noteID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  PMMarkup.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  PMMarkup.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMMarkup.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PMMarkup.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  PMMarkup.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMMarkup.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PMMarkup.lastModifiedDateTime>
  {
  }
}
