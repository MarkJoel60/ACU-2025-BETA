// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.PMReportProject
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CT;
using PX.Objects.GL;
using PX.Objects.PM;
using System;

#nullable enable
namespace PX.Objects.CN;

[PXCacheName("PM Report Project")]
[PXProjection(typeof (Select<PMProject>))]
[PXPrimaryGraph(new Type[] {typeof (ProjectEntry)}, new Type[] {typeof (Select<PMProject, Where<PMProject.contractID, Equal<Current<PMReportProject.contractID>>, And<PMProject.baseType, Equal<CTPRType.project>, And<PMProject.nonProject, Equal<False>>>>>)})]
[Serializable]
public class PMReportProject : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBInt(BqlField = typeof (PMProject.contractID))]
  [PXSelector(typeof (Search<PMReportProject.contractID, Where<PMReportProject.baseType, Equal<CTPRType.project>>>), SubstituteKey = typeof (PMReportProject.contractCD))]
  public virtual int? ContractID { get; set; }

  [PXDBString(1, IsFixed = true, IsKey = true, BqlField = typeof (PMProject.baseType))]
  public virtual 
  #nullable disable
  string BaseType { get; set; }

  [PXDimensionSelector("PROJECT", typeof (Search<PMReportProject.contractCD, Where<PMReportProject.baseType, Equal<CTPRType.project>, And<PMReportProject.nonProject, Equal<False>, And<Match<Current<AccessInfo.userName>>>>>>))]
  [PXDBString(IsKey = true, InputMask = "", BqlField = typeof (PMProject.contractCD))]
  public virtual string ContractCD { get; set; }

  [PXDBString(60, BqlField = typeof (PMProject.description))]
  public virtual string Description { get; set; }

  [Branch(null, null, true, true, true, IsDetail = true, BqlField = typeof (PMProject.defaultBranchID))]
  public virtual int? DefaultBranchID { get; set; }

  [PXDBString(1, BqlField = typeof (PMProject.status))]
  [ProjectStatus.List]
  public virtual string Status { get; set; }

  [PXDBDate(BqlField = typeof (PMProject.startDate))]
  public virtual DateTime? StartDate { get; set; }

  [PXDBDate(BqlField = typeof (PMProject.expireDate))]
  public virtual DateTime? ExpireDate { get; set; }

  [PXDBBool(BqlField = typeof (PMProject.isActive))]
  public virtual bool? IsActive { get; set; }

  [PXDBBool(BqlField = typeof (PMProject.isCompleted))]
  public virtual bool? IsCompleted { get; set; }

  [PXDBBool(BqlField = typeof (Contract.isCancelled))]
  public virtual bool? IsCancelled { get; set; }

  [PXDBBool(BqlField = typeof (PMProject.nonProject))]
  public virtual bool? NonProject { get; set; }

  [PXDBString(BqlField = typeof (PMProject.curyID))]
  public virtual string CuryID { get; set; }

  public abstract class contractID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMReportProject.contractID>
  {
  }

  public abstract class baseType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMReportProject.baseType>
  {
  }

  public abstract class contractCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMReportProject.contractCD>
  {
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMReportProject.description>
  {
  }

  public abstract class defaultBranchID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    PMReportProject.defaultBranchID>
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMReportProject.status>
  {
  }

  public abstract class startDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  PMReportProject.startDate>
  {
  }

  public abstract class expireDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PMReportProject.expireDate>
  {
  }

  public abstract class isActive : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMReportProject.isActive>
  {
  }

  public abstract class isCompleted : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMReportProject.isCompleted>
  {
  }

  public abstract class isCancelled : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMReportProject.isCancelled>
  {
  }

  public abstract class nonProject : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMReportProject.nonProject>
  {
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMReportProject.curyID>
  {
  }
}
