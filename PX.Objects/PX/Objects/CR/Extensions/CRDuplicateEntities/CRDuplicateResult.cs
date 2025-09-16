// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Extensions.CRDuplicateEntities.CRDuplicateResult
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using System;

#nullable disable
namespace PX.Objects.CR.Extensions.CRDuplicateEntities;

[PXInternalUseOnly]
public class CRDuplicateResult : 
  PXResult<CRDuplicateRecord, DuplicateContact, BAccountR, PX.Objects.CR.CRLead, PX.Objects.CR.Address, CRActivityStatistics, PX.Objects.CR.Standalone.Location>
{
  [Obsolete]
  public CRDuplicateResult(
    CRDuplicateRecord p1,
    DuplicateContact p2,
    BAccountR p3,
    PX.Objects.CR.CRLead p4,
    PX.Objects.CR.Address p5,
    CRActivityStatistics p6)
    : base(p1, p2, p3, p4, p5, p6, (PX.Objects.CR.Standalone.Location) null)
  {
  }

  public CRDuplicateResult(
    CRDuplicateRecord p1,
    DuplicateContact p2,
    BAccountR p3,
    PX.Objects.CR.CRLead p4,
    PX.Objects.CR.Address p5,
    CRActivityStatistics p6,
    PX.Objects.CR.Standalone.Location p7)
    : base(p1, p2, p3, p4, p5, p6, p7)
  {
  }
}
