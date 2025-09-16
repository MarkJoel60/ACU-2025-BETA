// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EPEarningType
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CS;
using PX.Objects.CT;
using PX.Objects.PM;
using System;

#nullable enable
namespace PX.Objects.EP;

/// <summary>
/// A code that is used to categorize the type of work done by an employee. The information will be visible on the Earning Types (EP102000) form.
/// </summary>
[PXCacheName("Earning Type")]
[Serializable]
public class EPEarningType : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDefault]
  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXUIField]
  [PXReferentialIntegrityCheck]
  public virtual 
  #nullable disable
  string TypeCD { get; set; }

  [PXDBString(60, IsUnicode = true)]
  [PXDefault]
  [PXUIField]
  [PXFieldDescription]
  public virtual string Description { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Active")]
  public virtual bool? IsActive { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Overtime")]
  public virtual bool? IsOvertime { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Billable")]
  public virtual bool? isBillable { get; set; }

  [Project(typeof (Where<PMProject.baseType, Equal<CTPRType.project>>), DisplayName = "Default Project")]
  public virtual int? ProjectID { get; set; }

  [ProjectTask(typeof (EPEarningType.projectID), DisplayName = "Default Project Task", AllowNull = true)]
  public virtual int? TaskID { get; set; }

  [PXDBDecimal(2, MinValue = 0.01, MaxValue = 99.99)]
  [PXUIEnabled(typeof (EPEarningType.isOvertime))]
  [PXFormula(typeof (Switch<Case<Where<EPEarningType.isOvertime, NotEqual<True>>, decimal1>, EPEarningType.overtimeMultiplier>))]
  [PXUIField(DisplayName = "Multiplier")]
  public virtual Decimal? OvertimeMultiplier { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  /// <summary>Primary Key</summary>
  public class PK : PrimaryKeyOf<EPEarningType>.By<EPEarningType.typeCD>
  {
    public static EPEarningType Find(PXGraph graph, string typeCD, PKFindOptions options = 0)
    {
      return (EPEarningType) PrimaryKeyOf<EPEarningType>.By<EPEarningType.typeCD>.FindBy(graph, (object) typeCD, options);
    }
  }

  /// <summary>Foreign Keys</summary>
  public static class FK
  {
    /// <summary>Default Project</summary>
    public class DefaultProject : 
      PrimaryKeyOf<PMProject>.By<PMProject.contractID>.ForeignKeyOf<EPEarningType>.By<EPEarningType.projectID>
    {
    }

    /// <summary>Default Project Task</summary>
    public class DefaultProjectTask : 
      PrimaryKeyOf<PMTask>.By<PMTask.projectID, PMTask.taskID>.ForeignKeyOf<EPEarningType>.By<EPEarningType.projectID, EPEarningType.taskID>
    {
    }
  }

  public abstract class typeCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EPEarningType.typeCD>
  {
    public const int Length = 15;
    public const string InputMask = ">CCCCCCCCCCCCCCC";
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EPEarningType.description>
  {
  }

  public abstract class isActive : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  EPEarningType.isActive>
  {
  }

  public abstract class isOvertime : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  EPEarningType.isOvertime>
  {
  }

  public abstract class isbillable : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  EPEarningType.isbillable>
  {
  }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPEarningType.projectID>
  {
  }

  public abstract class taskID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPEarningType.taskID>
  {
  }

  public abstract class overtimeMultiplier : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    EPEarningType.overtimeMultiplier>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  EPEarningType.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  EPEarningType.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPEarningType.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    EPEarningType.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    EPEarningType.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPEarningType.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    EPEarningType.lastModifiedDateTime>
  {
  }
}
