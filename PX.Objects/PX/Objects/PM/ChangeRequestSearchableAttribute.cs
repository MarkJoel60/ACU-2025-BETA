// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.ChangeRequestSearchableAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.PM;

public class ChangeRequestSearchableAttribute : PXSearchableAttribute
{
  public ChangeRequestSearchableAttribute()
    : base(2048 /*0x0800*/, "Change Request: {0}", new Type[1]
    {
      typeof (PMChangeRequest.refNbr)
    }, new Type[6]
    {
      typeof (PMChangeRequest.description),
      typeof (PMChangeRequest.extRefNbr),
      typeof (PMChangeRequest.extRefNbr),
      typeof (PMChangeRequest.projectID),
      typeof (PMProject.contractCD),
      typeof (PMProject.description)
    })
  {
    this.NumberFields = new Type[1]
    {
      typeof (PMChangeRequest.refNbr)
    };
    this.Line1Format = "{0:d}{1}{2}";
    this.Line1Fields = new Type[3]
    {
      typeof (PMChangeRequest.date),
      typeof (PMChangeRequest.status),
      typeof (PMChangeRequest.projectID)
    };
    this.Line2Format = "{0}";
    this.Line2Fields = new Type[1]
    {
      typeof (PMChangeRequest.description)
    };
    this.SelectForFastIndexing = typeof (Select2<PMChangeRequest, InnerJoin<PMProject, On<PMChangeRequest.projectID, Equal<PMProject.contractID>>>>);
    this.MatchWithJoin = typeof (InnerJoin<PMProject, On<PMProject.contractID, Equal<PMChangeRequest.projectID>>>);
  }
}
