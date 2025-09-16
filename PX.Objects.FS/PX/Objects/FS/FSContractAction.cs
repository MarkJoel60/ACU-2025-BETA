// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSContractAction
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.FS;

[Serializable]
public class FSContractAction : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  byte[] _tstamp;

  [PXDBInt]
  [PXUIField(DisplayName = "Service Contract ID", Enabled = false)]
  [PXParent(typeof (Select<FSServiceContract, Where<FSServiceContract.serviceContractID, Equal<Current<FSContractAction.serviceContractID>>>>))]
  [PXDBDefault(typeof (FSServiceContract.serviceContractID))]
  public virtual int? ServiceContractID { get; set; }

  [PXDBIdentity(IsKey = true)]
  public virtual int? RecordID { get; set; }

  [ListField_RecordType_ContractAction.ListAtrribute]
  [PXDBString(1, IsUnicode = false)]
  [PXUIField(DisplayName = "Type", Enabled = false)]
  public virtual string Type { get; set; }

  [ListField_Action_ContractAction.ListAtrribute]
  [PXDBString(1, IsUnicode = false)]
  [PXUIField(DisplayName = "Action", Enabled = false)]
  public virtual string Action { get; set; }

  [PXDBDate]
  [PXUIField(DisplayName = "Date", Enabled = false)]
  public virtual DateTime? ActionBusinessDate { get; set; }

  [PXDBDate]
  [PXUIField(DisplayName = "Effective Date", Enabled = false)]
  public virtual DateTime? EffectiveDate { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Change Recurrence", Enabled = false)]
  public virtual bool? ScheduleChangeRecurrence { get; set; }

  [PXDBDate]
  [PXUIField(DisplayName = "Effective Recurrence Start Date", Enabled = false)]
  public virtual DateTime? ScheduleNextExecutionDate { get; set; }

  [PXDBString(2147483647 /*0x7FFFFFFF*/, IsUnicode = false)]
  [PXUIField(DisplayName = "Recurrence Description", Enabled = false)]
  public virtual string ScheduleRecurrenceDescr { get; set; }

  [PXDBString(15, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXUIField(DisplayName = "Schedule ID", Enabled = false)]
  public virtual string ScheduleRefNbr { get; set; }

  [PXDBString(15, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXSelector(typeof (Search<FSServiceContract.refNbr>), ValidateValue = false)]
  [PXUIField(DisplayName = "Orig. Service Contract ID", Enabled = false)]
  public virtual string OrigServiceContractRefNbr { get; set; }

  [PXDBString(15, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXSelector(typeof (Search<FSSchedule.refNbr>), ValidateValue = false)]
  [PXUIField(DisplayName = "Orig. Schedule ID", Enabled = false)]
  public virtual string OrigScheduleRefNbr { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Applied", Enabled = false)]
  public virtual bool? Applied { get; set; }

  [PXDBCreatedByID]
  [PXUIField(DisplayName = "Created By ID")]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  [PXUIField(DisplayName = "Created By Screen ID")]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  [PXUIField(DisplayName = "Created DateTime")]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  [PXUIField(DisplayName = "Last Modified By ID")]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  [PXUIField(DisplayName = "Last Modified By Screen ID")]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  [PXUIField(DisplayName = "Last Modified Date Time")]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  public abstract class serviceContractID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSContractAction.serviceContractID>
  {
  }

  public abstract class recordID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSContractAction.recordID>
  {
  }

  public abstract class type : ListField_RecordType_ContractAction
  {
  }

  public abstract class action : ListField_Action_ContractAction
  {
  }

  public abstract class actionBusinessDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSContractAction.actionBusinessDate>
  {
  }

  public abstract class effectiveDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSContractAction.effectiveDate>
  {
  }

  public abstract class scheduleChangeRecurrence : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSContractAction.scheduleChangeRecurrence>
  {
  }

  public abstract class scheduleNextExecutionDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSContractAction.scheduleNextExecutionDate>
  {
  }

  public abstract class scheduleRecurrenceDescr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSContractAction.scheduleRecurrenceDescr>
  {
  }

  public abstract class scheduleRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSContractAction.scheduleRefNbr>
  {
  }

  public abstract class origServiceContractRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSContractAction.origServiceContractRefNbr>
  {
  }

  public abstract class origScheduleRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSContractAction.origScheduleRefNbr>
  {
  }

  public abstract class applied : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSContractAction.applied>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FSContractAction.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSContractAction.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSContractAction.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    FSContractAction.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSContractAction.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSContractAction.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  FSContractAction.Tstamp>
  {
  }
}
