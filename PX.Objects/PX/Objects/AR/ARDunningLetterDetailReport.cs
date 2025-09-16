// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARDunningLetterDetailReport
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.AR;

[PXCacheName("Dunning Letter Detail")]
public class ARDunningLetterDetailReport : ARDunningLetterDetail
{
  [PXDate]
  [PXDBCalced(typeof (Switch<Case<Where<ARDunningLetterDetail.dueDate, IsNull>, ARDunningLetterDetail.docDate>, ARDunningLetterDetail.dueDate>), typeof (DateTime))]
  public virtual DateTime? SortDate { get; set; }

  /// <summary>
  /// Read-only field. Equals DueDate or DocDate if DueDate is null. Needed for sorting in reports.
  /// </summary>
  public abstract class sortDate : 
    BqlType<IBqlDateTime, DateTime>.Field<
    #nullable disable
    ARDunningLetterDetailReport.sortDate>
  {
  }
}
