// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.Descriptor.GenericFABookPeriodSelectorAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.FA.Descriptor;

public class GenericFABookPeriodSelectorAttribute : PXCustomSelectorAttribute
{
  protected ReportParametersFlag ReportParametersMask;

  public Type OrigSearchType { get; set; }

  public FABookPeriodKeyProvider BookPeriodKeyProvider { get; set; }

  public GenericFABookPeriodSelectorAttribute(
    Type searchType,
    FABookPeriodKeyProvider bookPeriodKeyProvider,
    ReportParametersFlag reportParametersMask = ReportParametersFlag.None,
    Type[] fieldList = null)
    : base(GenericFABookPeriodSelectorAttribute.GetSearchType(searchType, reportParametersMask), fieldList)
  {
    this.OrigSearchType = searchType;
    this.BookPeriodKeyProvider = bookPeriodKeyProvider;
    ((PXSelectorAttribute) this).SelectorMode = (PXSelectorMode) (((PXSelectorAttribute) this).SelectorMode | 1);
    this.ReportParametersMask = reportParametersMask;
  }

  protected virtual IEnumerable GetRecords()
  {
    PXCache cach = this._Graph.Caches[((PXSelectorAttribute) this)._CacheType];
    object extRow = ((IEnumerable<object>) PXView.Currents).FirstOrDefault<object>((Func<object, bool>) (c => ((PXSelectorAttribute) this)._CacheType.IsAssignableFrom(c.GetType())));
    FABookPeriod.Key periodKey = this.ReportParametersMask != ReportParametersFlag.None ? this.BookPeriodKeyProvider.GetKeyFromReportParameters(this._Graph, PXView.Parameters, this.ReportParametersMask) : this.BookPeriodKeyProvider.GetKey(this._Graph, cach, extRow);
    int startRow = PXView.StartRow;
    int num1 = 0;
    List<object> parameters = new List<object>();
    BqlCommand command = this.GetCommand(cach, extRow, parameters, periodKey);
    PXGraph graph = this._Graph;
    PXView view = PXView.View;
    int num2 = view != null ? (view.IsReadOnly ? 1 : 0) : 1;
    BqlCommand bqlCommand = command;
    PXView pxView = new PXView(graph, num2 != 0, bqlCommand);
    try
    {
      return (IEnumerable) pxView.Select(PXView.Currents, parameters.ToArray(), PXView.Searches, PXView.SortColumns, PXView.Descendings, PXView.PXFilterRowCollection.op_Implicit(PXView.Filters), ref startRow, PXView.MaximumRows, ref num1);
    }
    finally
    {
      PXView.StartRow = 0;
    }
  }

  public static Type GetSearchType(Type origSearchType, ReportParametersFlag reportParametersMask)
  {
    if (reportParametersMask == ReportParametersFlag.None)
      return origSearchType;
    BqlCommand bqlCommand = BqlCommand.CreateInstance(new Type[1]
    {
      origSearchType
    });
    if ((reportParametersMask & ReportParametersFlag.Organization) == ReportParametersFlag.Organization)
      bqlCommand = bqlCommand.WhereAnd<Where<FABookPeriod.organizationID, Equal<Optional2<FAQueryParameters.organizationID>>>>();
    if ((reportParametersMask & ReportParametersFlag.Branch) == ReportParametersFlag.Branch)
      bqlCommand = bqlCommand.WhereAnd<Where<FABookPeriod.organizationID, Equal<Optional2<FAQueryParameters.branchID>>>>();
    if ((reportParametersMask & ReportParametersFlag.BAccount) == ReportParametersFlag.BAccount)
      bqlCommand = bqlCommand.WhereAnd<Where<FABookPeriod.organizationID, Equal<Optional2<FAQueryParameters.orgBAccountID>>>>();
    if ((reportParametersMask & ReportParametersFlag.FixedAsset) == ReportParametersFlag.FixedAsset)
      bqlCommand = bqlCommand.WhereAnd<Where<FABookPeriod.organizationID, Equal<Optional2<FAQueryParameters.assetID>>>>();
    if ((reportParametersMask & ReportParametersFlag.Book) == ReportParametersFlag.Book)
      bqlCommand = bqlCommand.WhereAnd<Where<FABookPeriod.bookID, Equal<Optional2<FAQueryParameters.bookID>>>>();
    return bqlCommand.GetType();
  }

  protected virtual BqlCommand GetCommand(
    PXCache cache,
    object extRow,
    List<object> parameters,
    FABookPeriod.Key periodKey)
  {
    BqlCommand command = BqlCommand.CreateInstance(new Type[1]
    {
      this.OrigSearchType
    }).WhereAnd<Where<FABookPeriod.organizationID, Equal<Required<FABookPeriod.organizationID>>, And<FABookPeriod.bookID, Equal<Required<FABookPeriod.bookID>>>>>();
    parameters.Add((object) periodKey.OrganizationID);
    parameters.Add((object) periodKey.BookID);
    return command;
  }
}
