// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.ProgressWorksheetSearchableAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.PM;

public class ProgressWorksheetSearchableAttribute : PXSearchableAttribute
{
  public ProgressWorksheetSearchableAttribute()
    : base(2048 /*0x0800*/, "Progress Worksheet: {0}", new Type[1]
    {
      typeof (PMProgressWorksheet.refNbr)
    }, new Type[4]
    {
      typeof (PMProgressWorksheet.description),
      typeof (PMProgressWorksheet.projectID),
      typeof (PMProject.contractCD),
      typeof (PMProject.description)
    })
  {
    this.NumberFields = new Type[1]
    {
      typeof (PMProgressWorksheet.refNbr)
    };
    this.Line1Format = "{0:d}{1}{2}";
    this.Line1Fields = new Type[3]
    {
      typeof (PMProgressWorksheet.date),
      typeof (PMProgressWorksheet.status),
      typeof (PMProgressWorksheet.projectID)
    };
    this.Line2Format = "{0}";
    this.Line2Fields = new Type[1]
    {
      typeof (PMProgressWorksheet.description)
    };
    this.SelectForFastIndexing = typeof (Select2<PMProgressWorksheet, InnerJoin<PMProject, On<PMProgressWorksheet.projectID, Equal<PMProject.contractID>>>>);
    this.MatchWithJoin = typeof (InnerJoin<PMProject, On<PMProject.contractID, Equal<PMProgressWorksheet.projectID>>>);
  }
}
