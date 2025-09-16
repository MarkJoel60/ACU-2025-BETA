// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CRClassSeverityTime
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CS;
using System;

#nullable enable
namespace PX.Objects.CR;

[PXCacheName("Time Reaction By Severity")]
public class CRClassSeverityTime : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(10, IsUnicode = true, IsKey = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Case Class")]
  [PXSelector(typeof (CRCaseClass.caseClassID))]
  public virtual 
  #nullable disable
  string CaseClassID { get; set; }

  [PXDBString(1, IsKey = true, IsFixed = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Severity")]
  [CRCaseSeverity(BqlField = typeof (CRCase.severity))]
  public virtual string Severity { get; set; }

  /// <summary>
  /// This field provides an ability to set <see cref="P:PX.Objects.CR.CRClassSeverityTime.InitialResponseTimeTarget">Target Initial Response Time</see>
  /// and <see cref="P:PX.Objects.CR.CRClassSeverityTime.InitialResponseGracePeriod">Initial Response Extension</see>.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Enable")]
  public virtual bool? TrackInitialResponseTime { get; set; }

  /// <summary>
  /// The timeframe within which the service provider is obliged to provide a response to the initial communication on a particular service case by a service user.
  /// </summary>
  [WorkTime(typeof (FbqlSelect<SelectFromBase<CRCaseClass, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<CRCaseClass.caseClassID, IBqlString>.IsEqual<BqlField<CRClassSeverityTime.caseClassID, IBqlString>.FromCurrent>>, CRCaseClass>.SearchFor<CRCaseClass.calendarID>), AvailabilityField = typeof (CRClassSeverityTime.trackInitialResponseTime))]
  [PXUIField(DisplayName = "Target Initial Response Time")]
  public virtual int? InitialResponseTimeTarget { get; set; }

  /// <summary>
  /// This field defines the <see cref="P:PX.Objects.CR.CRClassSeverityTime.InitialResponseTimeTarget">Target Initial Response Time</see> when a severity was changed in a case
  /// and the target for the new severity is less than the actual time passed since the time counter started.
  /// The grace period gives the service provider enough time to react to an increased case severity.
  /// </summary>
  [WorkTime(typeof (FbqlSelect<SelectFromBase<CRCaseClass, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<CRCaseClass.caseClassID, IBqlString>.IsEqual<BqlField<CRClassSeverityTime.caseClassID, IBqlString>.FromCurrent>>, CRCaseClass>.SearchFor<CRCaseClass.calendarID>), AvailabilityField = typeof (CRClassSeverityTime.trackInitialResponseTime))]
  [PXUIField(DisplayName = "Initial Response Extension")]
  [PXFormula(typeof (IIf<Where<IsNull<Current<CRClassSeverityTime.initialResponseGracePeriod>, int0>, Equal<int0>>, CRClassSeverityTime.initialResponseTimeTarget, CRClassSeverityTime.initialResponseGracePeriod>))]
  public virtual int? InitialResponseGracePeriod { get; set; }

  /// <summary>
  /// This field provides an ability to set <see cref="P:PX.Objects.CR.CRClassSeverityTime.ResponseTimeTarget">Target Response Time</see> and <see cref="P:PX.Objects.CR.CRClassSeverityTime.ResponseGracePeriod">Response Extension</see>.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Enable")]
  public virtual bool? TrackResponseTime { get; set; }

  /// <summary>
  /// The timeframe within which the service provider is obliged to provide a response to the communication by a service user according.
  /// </summary>
  [WorkTime(typeof (FbqlSelect<SelectFromBase<CRCaseClass, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<CRCaseClass.caseClassID, IBqlString>.IsEqual<BqlField<CRClassSeverityTime.caseClassID, IBqlString>.FromCurrent>>, CRCaseClass>.SearchFor<CRCaseClass.calendarID>), AvailabilityField = typeof (CRClassSeverityTime.trackResponseTime))]
  [PXUIField(DisplayName = "Target Response Time")]
  public virtual int? ResponseTimeTarget { get; set; }

  /// <summary>
  /// These fields define the <see cref="P:PX.Objects.CR.CRClassSeverityTime.ResponseTimeTarget">Target Response Time</see> when a severity was changed in a case
  /// and the target for the new severity is less than the actual time passed since the time counter started.
  /// The grace period gives the service provider enough time to react to an increased case severity.
  /// </summary>
  [WorkTime(typeof (FbqlSelect<SelectFromBase<CRCaseClass, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<CRCaseClass.caseClassID, IBqlString>.IsEqual<BqlField<CRClassSeverityTime.caseClassID, IBqlString>.FromCurrent>>, CRCaseClass>.SearchFor<CRCaseClass.calendarID>), AvailabilityField = typeof (CRClassSeverityTime.trackResponseTime))]
  [PXUIField(DisplayName = "Response Extension")]
  [PXFormula(typeof (IIf<Where<IsNull<Current<CRClassSeverityTime.responseGracePeriod>, int0>, Equal<int0>>, CRClassSeverityTime.responseTimeTarget, CRClassSeverityTime.responseGracePeriod>))]
  public virtual int? ResponseGracePeriod { get; set; }

  /// <summary>
  /// This field provides an ability to set <see cref="P:PX.Objects.CR.CRClassSeverityTime.ResolutionTimeTarget">Target Resolution Time</see> and <see cref="P:PX.Objects.CR.CRClassSeverityTime.ResolutionGracePeriod">Resolution Extension</see>.
  /// </summary>
  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Enable")]
  public virtual bool? TrackResolutionTime { get; set; }

  /// <summary>
  /// The timeframe within which the service provider is obliged to provide a reply to the service user that contains the resolution for the inquiry.
  /// </summary>
  [WorkTime(typeof (FbqlSelect<SelectFromBase<CRCaseClass, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<CRCaseClass.caseClassID, IBqlString>.IsEqual<BqlField<CRClassSeverityTime.caseClassID, IBqlString>.FromCurrent>>, CRCaseClass>.SearchFor<CRCaseClass.calendarID>), AvailabilityField = typeof (CRClassSeverityTime.trackResolutionTime))]
  [PXUIField(DisplayName = "Target Resolution Time")]
  public virtual int? ResolutionTimeTarget { get; set; }

  /// <summary>
  /// This field defines the <see cref="P:PX.Objects.CR.CRClassSeverityTime.ResolutionTimeTarget">Target Resolution Time</see> when a severity was changed in a case
  /// and the target for the new severity is less than the actual time passed since the time counter started.
  /// The grace period gives the service provider enough time to react to an increased case severity.
  /// </summary>
  [WorkTime(typeof (FbqlSelect<SelectFromBase<CRCaseClass, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<CRCaseClass.caseClassID, IBqlString>.IsEqual<BqlField<CRClassSeverityTime.caseClassID, IBqlString>.FromCurrent>>, CRCaseClass>.SearchFor<CRCaseClass.calendarID>), AvailabilityField = typeof (CRClassSeverityTime.trackResolutionTime))]
  [PXUIField(DisplayName = "Resolution Extension")]
  [PXFormula(typeof (IIf<Where<IsNull<Current<CRClassSeverityTime.resolutionGracePeriod>, int0>, Equal<int0>>, CRClassSeverityTime.resolutionTimeTarget, CRClassSeverityTime.resolutionGracePeriod>))]
  public virtual int? ResolutionGracePeriod { get; set; }

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

  public class PK : 
    PrimaryKeyOf<CRClassSeverityTime>.By<CRClassSeverityTime.caseClassID, CRClassSeverityTime.severity>
  {
    public static CRClassSeverityTime Find(
      PXGraph graph,
      string caseClassID,
      string severity,
      PKFindOptions options = 0)
    {
      return (CRClassSeverityTime) PrimaryKeyOf<CRClassSeverityTime>.By<CRClassSeverityTime.caseClassID, CRClassSeverityTime.severity>.FindBy(graph, (object) caseClassID, (object) severity, options);
    }
  }

  public static class FK
  {
    public class CaseClass : 
      PrimaryKeyOf<CRCaseClass>.By<CRCaseClass.caseClassID>.ForeignKeyOf<CRClassSeverityTime>.By<CRClassSeverityTime.caseClassID>
    {
    }
  }

  public abstract class caseClassID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRClassSeverityTime.caseClassID>
  {
  }

  public abstract class severity : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CRClassSeverityTime.severity>
  {
  }

  public abstract class trackInitialResponseTime : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CRClassSeverityTime.trackInitialResponseTime>
  {
  }

  public abstract class initialResponseTimeTarget : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CRClassSeverityTime.initialResponseTimeTarget>
  {
  }

  public abstract class initialResponseGracePeriod : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CRClassSeverityTime.initialResponseGracePeriod>
  {
  }

  public abstract class trackResponseTime : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CRClassSeverityTime.trackResponseTime>
  {
  }

  public abstract class responseTimeTarget : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CRClassSeverityTime.responseTimeTarget>
  {
  }

  public abstract class responseGracePeriod : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CRClassSeverityTime.responseGracePeriod>
  {
  }

  public abstract class trackResolutionTime : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    CRClassSeverityTime.trackResolutionTime>
  {
  }

  public abstract class resolutionTimeTarget : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CRClassSeverityTime.resolutionTimeTarget>
  {
  }

  public abstract class resolutionGracePeriod : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CRClassSeverityTime.resolutionGracePeriod>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  CRClassSeverityTime.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CRClassSeverityTime.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRClassSeverityTime.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CRClassSeverityTime.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    CRClassSeverityTime.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CRClassSeverityTime.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CRClassSeverityTime.lastModifiedDateTime>
  {
  }
}
