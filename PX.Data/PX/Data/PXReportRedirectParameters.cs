// Decompiled with JetBrains decompiler
// Type: PX.Data.PXReportRedirectParameters
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace PX.Data;

/// <summary>The redirect parameters for <see cref="T:PX.Data.PXReportRequiredException">PXReportRequiredException</see>.</summary>
public sealed class PXReportRedirectParameters : IPXResultset, IPXResultsetBase, IEnumerable
{
  /// <summary>
  ///   <para>The dictionary with report parameters. <span id="f2efad77-0698-49f8-8c0e-3648b21cde84">The dictionary key is the parameter name as shown in the Report
  /// Designer. The dictionary value is the value you assign to this parameter.</span></para>
  /// </summary>
  public Dictionary<string, string> ReportParameters { get; set; }

  /// <summary>The data to be used for the report generation.</summary>
  public IPXResultset ResultSet { get; set; }

  /// <exclude />
  public IEnumerator GetEnumerator() => this.ResultSet.GetEnumerator();

  /// <exclude />
  public System.Type GetItemType(int i) => this.ResultSet.GetItemType(i);

  /// <exclude />
  public object GetItem(int rowNbr, int i) => this.ResultSet.GetItem(rowNbr, i);

  /// <exclude />
  public int GetTableCount() => this.ResultSet.GetTableCount();

  /// <exclude />
  public int GetRowCount() => this.ResultSet.GetRowCount();

  /// <exclude />
  public PXDelayedQuery GetDelayedQuery() => this.ResultSet.GetDelayedQuery();

  /// <exclude />
  public System.Type GetCollectionType() => this.ResultSet.GetCollectionType();

  /// <exclude />
  public object GetCollection() => this.ResultSet.GetCollection();

  /// <exclude />
  public static Dictionary<string, string> UnwrapParameters(object sessionReportData)
  {
    switch (sessionReportData)
    {
      case PXReportRedirectParameters redirectParameters:
        return redirectParameters.ReportParameters;
      case Dictionary<string, string> dictionary:
        return dictionary;
      default:
        return (Dictionary<string, string>) null;
    }
  }

  /// <exclude />
  public static IPXResultset UnwrapSet(object sessionReportData)
  {
    switch (sessionReportData)
    {
      case PXReportRedirectParameters redirectParameters:
        return redirectParameters.ResultSet;
      case IPXResultset pxResultset:
        return pxResultset;
      default:
        return (IPXResultset) null;
    }
  }
}
