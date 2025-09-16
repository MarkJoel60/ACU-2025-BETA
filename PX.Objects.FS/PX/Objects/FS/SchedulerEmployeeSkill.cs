// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.SchedulerEmployeeSkill
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using System;

#nullable enable
namespace PX.Objects.FS;

/// <exclude />
[PXProjection(typeof (SelectFrom<FSSkill>))]
[PXCacheName("Employee Skill")]
[Serializable]
public class SchedulerEmployeeSkill : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBInt(BqlField = typeof (FSSkill.skillID))]
  [PXSelector(typeof (FSSkill.skillID), SubstituteKey = typeof (FSSkill.skillCD), DescriptionField = typeof (FSSkill.descr))]
  [PXUIField(DisplayName = "Employee Skill ID")]
  public virtual int? SkillID { get; set; }

  [PXDBString(BqlField = typeof (FSSkill.skillCD))]
  public virtual 
  #nullable disable
  string SkillCD { get; set; }

  [PXDBString(BqlField = typeof (FSSkill.descr))]
  [PXUIField]
  public virtual string Descr { get; set; }

  public abstract class skillID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SchedulerEmployeeSkill.skillID>
  {
  }

  public abstract class skillCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SchedulerEmployeeSkill.skillCD>
  {
  }

  public abstract class descr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SchedulerEmployeeSkill.descr>
  {
  }
}
