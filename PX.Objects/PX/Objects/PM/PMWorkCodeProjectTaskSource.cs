// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMWorkCodeProjectTaskSource
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CT;
using System;

#nullable enable
namespace PX.Objects.PM;

[PXCacheName("Workers' Compensation Project Task Source")]
[Serializable]
public class PMWorkCodeProjectTaskSource : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(15, IsKey = true)]
  [PXDBDefault(typeof (PMWorkCode.workCodeID))]
  [PXParent(typeof (PMWorkCodeProjectTaskSource.FK.WorkCode))]
  public 
  #nullable disable
  string WorkCodeID { get; set; }

  [PXDBInt(IsKey = true)]
  [PXLineNbr(typeof (PMWorkCode))]
  public int? LineNbr { get; set; }

  [Project(typeof (Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMProject.baseType, Equal<CTPRType.project>>>>>.And<BqlOperand<PMProject.nonProject, IBqlBool>.IsNotEqual<True>>>), DisplayName = "Project")]
  [PXDefault]
  [PXForeignReference(typeof (PMWorkCodeProjectTaskSource.FK.Project))]
  public int? ProjectID { get; set; }

  [ProjectTask(typeof (PMWorkCodeProjectTaskSource.projectID), DisplayName = "Project Task", AllowNull = true)]
  [PXCheckUnique(new Type[] {typeof (PMWorkCodeProjectTaskSource.projectID)}, IgnoreNulls = false, ErrorMessage = "One project/task combination cannot be associated with multiple workers' compensation codes.")]
  [PXForeignReference(typeof (PMWorkCodeProjectTaskSource.FK.ProjectTask))]
  public int? ProjectTaskID { get; set; }

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

  public class PK : 
    PrimaryKeyOf<PMWorkCodeProjectTaskSource>.By<PMWorkCodeProjectTaskSource.workCodeID, PMWorkCodeProjectTaskSource.lineNbr>
  {
    public static PMWorkCodeProjectTaskSource Find(
      PXGraph graph,
      string workCodeID,
      int? lineNbr,
      PKFindOptions options = 0)
    {
      return (PMWorkCodeProjectTaskSource) PrimaryKeyOf<PMWorkCodeProjectTaskSource>.By<PMWorkCodeProjectTaskSource.workCodeID, PMWorkCodeProjectTaskSource.lineNbr>.FindBy(graph, (object) workCodeID, (object) lineNbr, options);
    }
  }

  public static class FK
  {
    public class WorkCode : 
      PrimaryKeyOf<PMWorkCode>.By<PMWorkCode.workCodeID>.ForeignKeyOf<PMWorkCodeProjectTaskSource>.By<PMWorkCodeProjectTaskSource.workCodeID>
    {
    }

    public class Project : 
      PrimaryKeyOf<PMProject>.By<PMProject.contractID>.ForeignKeyOf<PMWorkCodeProjectTaskSource>.By<PMWorkCodeProjectTaskSource.projectID>
    {
    }

    public class ProjectTask : 
      PrimaryKeyOf<PMTask>.By<PMTask.projectID, PMTask.taskID>.ForeignKeyOf<PMWorkCodeProjectTaskSource>.By<PMWorkCodeProjectTaskSource.projectID, PMWorkCodeProjectTaskSource.projectTaskID>
    {
    }
  }

  public abstract class workCodeID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMWorkCodeProjectTaskSource.workCodeID>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMWorkCodeProjectTaskSource.lineNbr>
  {
  }

  public abstract class projectID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMWorkCodeProjectTaskSource.projectID>
  {
  }

  public abstract class projectTaskID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMWorkCodeProjectTaskSource.projectTaskID>
  {
  }

  public abstract class createdByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    PMWorkCodeProjectTaskSource.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMWorkCodeProjectTaskSource.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PMWorkCodeProjectTaskSource.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    PMWorkCodeProjectTaskSource.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMWorkCodeProjectTaskSource.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PMWorkCodeProjectTaskSource.lastModifiedDateTime>
  {
  }
}
