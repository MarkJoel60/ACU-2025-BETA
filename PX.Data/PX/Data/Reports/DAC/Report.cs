// Decompiled with JetBrains decompiler
// Type: PX.Data.Reports.DAC.Report
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.BQL;

#nullable enable
namespace PX.Data.Reports.DAC;

[PXHidden]
[PXPrimaryGraph(typeof (ReportMaint))]
public class Report : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXString]
  public 
  #nullable disable
  string ReportName { get; set; }

  [PXString]
  public string ScreenId { get; set; }

  [PXString]
  [PXStringList]
  [PXUIField(DisplayName = "Template")]
  public string Template { get; set; }

  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Default")]
  public bool? IsDefault { get; set; }

  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Shared")]
  public bool? Shared { get; set; }

  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Print in PDF format")]
  public bool? ViewPdf { get; set; }

  [PXBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Compress PDF file")]
  public bool? PdfCompressed { get; set; }

  [PXBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Embed fonts in PDF file")]
  public bool? PdfFontEmbedded { get; set; }

  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "MS Print PDF Engine")]
  public bool? AlternativeEngine { get; set; }

  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Print all pages")]
  public bool? PrintAllPages { get; set; }

  [PXString(1)]
  [PXStringList(new string[] {"H", "P", "O"}, new string[] {"Hide", "Print", "Only"})]
  [PXDefault("H")]
  [PXUIField(DisplayName = "Deleted Records")]
  public string DeletedRecords { get; set; }

  [PXString(1)]
  [PXStringList(new string[] {"H", "P", "O"}, new string[] {"Hide", "Print", "Only"})]
  [PXDefault("P")]
  [PXUIField(DisplayName = "Archived Records")]
  public string ArchivedRecords { get; set; }

  [PXString]
  [PXUIField(DisplayName = "Format")]
  [PXStringList(new string[] {"PDF", "HTML", "Excel"}, new string[] {"PDF", "HTML", "Excel"})]
  [PXDefault("PDF")]
  public string Format { get; set; }

  [PXDBEmail]
  [PXDefault("")]
  [PXUIField(DisplayName = "To", Required = false)]
  public string Email { get; set; }

  [PXDBEmail]
  [PXDefault("")]
  [PXUIField(DisplayName = "Cc", Required = false)]
  public string Cc { get; set; }

  [PXDBEmail]
  [PXDefault("")]
  [PXUIField(DisplayName = "Bcc", Required = false)]
  public string Bcc { get; set; }

  [PXString(255 /*0xFF*/)]
  [PXDefault("")]
  [PXUIField(DisplayName = "Subject", Required = false)]
  public string Subject { get; set; }

  [PXString]
  [PXDefault("")]
  [PXStringList]
  [PXUIField(DisplayName = "Locale")]
  public string Locale { get; set; }

  [PXString]
  [PXDefault("")]
  [PXStringList]
  [PXUIField(DisplayName = "Localization")]
  public string Localization { get; set; }

  [PXString]
  public string InstanceId { get; set; }

  [PXInt]
  [PXDefault(0)]
  public int? PageIndex { get; set; }

  public abstract class reportName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Report.reportName>
  {
  }

  public abstract class screenId : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Report.screenId>
  {
  }

  public abstract class template : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Report.template>
  {
  }

  public abstract class isDefault : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Report.isDefault>
  {
  }

  public abstract class shared : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Report.shared>
  {
  }

  public abstract class viewPdf : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Report.viewPdf>
  {
  }

  public abstract class pdfCompressed : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Report.pdfCompressed>
  {
  }

  public abstract class pdfFontEmbedded : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Report.pdfFontEmbedded>
  {
  }

  public abstract class alternativeEngine : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Report.alternativeEngine>
  {
  }

  public abstract class printAllPages : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Report.printAllPages>
  {
  }

  public abstract class deletedRecords : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Report.deletedRecords>
  {
  }

  public abstract class archivedRecords : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Report.archivedRecords>
  {
  }

  public abstract class format : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Report.format>
  {
  }

  public abstract class email : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Report.email>
  {
  }

  public abstract class cc : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Report.cc>
  {
  }

  public abstract class bcc : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Report.bcc>
  {
  }

  public abstract class subject : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Report.subject>
  {
  }

  public abstract class locale : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Report.locale>
  {
  }

  public abstract class localization : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Report.localization>
  {
  }

  public abstract class instanceId : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Report.instanceId>
  {
  }

  public abstract class pageIndex : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Report.pageIndex>
  {
  }
}
