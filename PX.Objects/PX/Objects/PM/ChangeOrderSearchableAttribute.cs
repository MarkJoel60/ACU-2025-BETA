// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.ChangeOrderSearchableAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.PM;

public class ChangeOrderSearchableAttribute : PXSearchableAttribute
{
  public ChangeOrderSearchableAttribute()
    : base(2048 /*0x0800*/, "Change Order: {0}", new Type[1]
    {
      typeof (PMChangeOrder.refNbr)
    }, new Type[5]
    {
      typeof (PMChangeOrder.description),
      typeof (PMChangeOrder.extRefNbr),
      typeof (PMChangeOrder.projectID),
      typeof (PMProject.contractCD),
      typeof (PMProject.description)
    })
  {
    this.NumberFields = new Type[1]
    {
      typeof (PMChangeOrder.refNbr)
    };
    this.Line1Format = "{0:d}{1}{2}";
    this.Line1Fields = new Type[3]
    {
      typeof (PMChangeOrder.date),
      typeof (PMChangeOrder.status),
      typeof (PMChangeOrder.projectID)
    };
    this.Line2Format = "{0}";
    this.Line2Fields = new Type[1]
    {
      typeof (PMChangeOrder.description)
    };
    this.SelectForFastIndexing = typeof (Select2<PMChangeOrder, InnerJoin<PMProject, On<PMChangeOrder.projectID, Equal<PMProject.contractID>>>>);
    this.MatchWithJoin = typeof (InnerJoin<PMProject, On<PMProject.contractID, Equal<PMChangeOrder.projectID>>>);
  }
}
