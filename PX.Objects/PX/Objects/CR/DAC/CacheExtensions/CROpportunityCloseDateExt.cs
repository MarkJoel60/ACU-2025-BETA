// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.DAC.CacheExtensions.CROpportunityCloseDateExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.CR.DAC.CacheExtensions;

public sealed class CROpportunityCloseDateExt : PXCacheExtension<
#nullable disable
CROpportunity>
{
  /// <summary>The estimated date of closing the deal (year part)</summary>
  /// <value>Year</value>
  [PXInt]
  [PXUIField]
  [PXDBCalced(typeof (DatePart<DatePart.year, CROpportunity.closeDate>), typeof (int))]
  public int? CloseDateYear { get; set; }

  /// <summary>The estimated date of closing the deal (quarter part)</summary>
  /// <value>Quarter</value>
  [PXInt]
  [PXUIField]
  [PXDBCalced(typeof (DatePart<DatePart.quarter, CROpportunity.closeDate>), typeof (int))]
  [Quarter.List]
  public int? CloseDateQuarter { get; set; }

  /// <summary>The estimated date of closing the deal (month part)</summary>
  /// <value>Month</value>
  [PXInt]
  [PXUIField]
  [PXDBCalced(typeof (DatePart<DatePart.month, CROpportunity.closeDate>), typeof (int))]
  [Month.List]
  public int? CloseDateMonth { get; set; }

  public abstract class closeDateYear : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CROpportunityCloseDateExt.closeDateYear>
  {
  }

  public abstract class closeDateQuarter : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CROpportunityCloseDateExt.closeDateQuarter>
  {
  }

  public abstract class closeDateMonth : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CROpportunityCloseDateExt.closeDateMonth>
  {
  }
}
