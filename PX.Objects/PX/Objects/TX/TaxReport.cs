// Decompiled with JetBrains decompiler
// Type: PX.Objects.TX.TaxReport
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.TX.Descriptor;
using System;

#nullable enable
namespace PX.Objects.TX;

[PXCacheName("Tax Report")]
[Serializable]
public class TaxReport : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [TaxAgencyActive(IsKey = true)]
  [PXDefault]
  public virtual int? VendorID { get; set; }

  [PXDBInt(IsKey = true)]
  [PXUIField(DisplayName = "Report Version")]
  [PXDefault]
  [PXSelector(typeof (Search<TaxReport.revisionID, Where<TaxReport.vendorID, Equal<Current<TaxReport.vendorID>>>, OrderBy<Desc<TaxReport.revisionID>>>), new Type[] {typeof (TaxReport.revisionID), typeof (TaxReport.validFrom)})]
  public virtual int? RevisionID { get; set; }

  [PXDBDate]
  [PXUIField(DisplayName = "Valid From")]
  [PXDefault]
  public virtual DateTime? ValidFrom { get; set; }

  [PXDBDate]
  [PXUIField(DisplayName = "Valid To", Visible = false)]
  public virtual DateTime? ValidTo { get; set; }

  [PXBool]
  [PXUIField(DisplayName = "Show Tax Zones")]
  [PXUnboundDefault(false)]
  public virtual bool? ShowNoTemp { get; set; }

  [PXInt]
  [PXDefault(0)]
  [PXDBScalar(typeof (Search4<TaxReportLine.lineNbr, Where<TaxReportLine.vendorID, Equal<TaxReport.vendorID>, And<TaxReportLine.taxReportRevisionID, Equal<TaxReport.revisionID>>>, Aggregate<Max<TaxReportLine.lineNbr>>>))]
  public virtual int? LineCntr { get; set; }

  [PXNote(PopupTextEnabled = true)]
  public Guid? NoteID { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual 
  #nullable disable
  string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  public class PK : PrimaryKeyOf<TaxReport>.By<TaxReport.vendorID, TaxReport.revisionID>
  {
    public static TaxReport Find(
      PXGraph graph,
      int? vendorID,
      int? revisionID,
      PKFindOptions options = 0)
    {
      return (TaxReport) PrimaryKeyOf<TaxReport>.By<TaxReport.vendorID, TaxReport.revisionID>.FindBy(graph, (object) vendorID, (object) revisionID, options);
    }
  }

  public static class FK
  {
    public class Vendor : 
      PrimaryKeyOf<PX.Objects.AP.Vendor>.By<PX.Objects.AP.Vendor.bAccountID>.ForeignKeyOf<TaxReport>.By<TaxReport.vendorID>
    {
    }
  }

  public abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  TaxReport.vendorID>
  {
  }

  public abstract class revisionID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  TaxReport.revisionID>
  {
  }

  public abstract class validFrom : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  TaxReport.validFrom>
  {
  }

  public abstract class validTo : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  TaxReport.validTo>
  {
  }

  public abstract class showNoTemp : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  TaxReport.showNoTemp>
  {
  }

  public abstract class lineCntr : IBqlField, IBqlOperand
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  TaxReport.noteID>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  TaxReport.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    TaxReport.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    TaxReport.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  TaxReport.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    TaxReport.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    TaxReport.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  TaxReport.Tstamp>
  {
  }
}
