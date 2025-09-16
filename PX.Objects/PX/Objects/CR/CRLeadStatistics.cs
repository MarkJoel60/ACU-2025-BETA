// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CRLeadStatistics
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.CR;

[PXProjection(typeof (Select<CRLead>))]
[PXCacheName("Lead Statistics")]
[Serializable]
public class CRLeadStatistics : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXBool]
  [PXDefault(false)]
  [PXUIField]
  public virtual bool? Selected { get; set; }

  [PXDBIdentity(IsKey = true, BqlField = typeof (CRLead.contactID))]
  [PXUIField]
  [PXPersonalDataWarning]
  public virtual int? ContactID { get; set; }

  [CRTimeSpanCalced(typeof (DateDiff<CRLead.createdDateTime, CRLead.qualificationDate, DateDiff.minute>))]
  [PXTimeSpanLong]
  [PXUIField(DisplayName = "Lead Qualification Time")]
  public int? LeadQualificationTime { get; set; }

  [CRTimeSpanCalced(typeof (Minus1<Search<CRActivityStatistics.initialOutgoingActivityCompletedAtDate, Where<CRActivityStatistics.noteID, Equal<CRLead.noteID>>>, CRLead.createdDateTime>))]
  [PXUIField(DisplayName = "Lead Response Time")]
  [PXTimeSpanLong]
  public int? LeadResponseTime { get; set; }

  public abstract class selected : BqlType<IBqlBool, bool>.Field<
  #nullable disable
  CRLeadStatistics.selected>
  {
  }

  public abstract class contactID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CRLeadStatistics.contactID>
  {
  }

  public abstract class leadQualificationTime : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CRLeadStatistics.leadQualificationTime>
  {
  }

  public abstract class leadResponseTime : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CRLeadStatistics.leadResponseTime>
  {
  }
}
