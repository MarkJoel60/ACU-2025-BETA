// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.Descriptor.GenericFinYearSelectorAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.Common.DAC;
using PX.Objects.Common.Extensions;
using PX.Objects.GL.FinPeriods;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.GL.Descriptor;

public class GenericFinYearSelectorAttribute : PXCustomSelectorAttribute
{
  public Type SearchType { get; set; }

  public Type SourceType { get; set; }

  public Type BranchSourceType { get; set; }

  public Type BranchSourceFormulaType { get; set; }

  public Type OrganizationSourceType { get; set; }

  public bool TakeBranchForSelectorFromQueryParams { get; set; }

  public bool TakeOrganizationForSelectorFromQueryParams { get; set; }

  public Type UseMasterCalendarSourceType { get; set; }

  public CalendarOrganizationIDProvider CalendarOrganizationIDProvider { get; protected set; }

  public GenericFinYearSelectorAttribute(
    Type searchType,
    Type sourceType,
    Type branchSourceType = null,
    Type branchSourceFormulaType = null,
    Type organizationSourceType = null,
    bool takeBranchForSelectorFromQueryParams = false,
    bool takeOrganizationForSelectorFromQueryParams = false,
    Type useMasterCalendarSourceType = null,
    bool useMasterOrganizationIDByDefault = false,
    Type[] fieldList = null)
    : base(GenericFinYearSelectorAttribute.GetSearchType(typeof (Search3<PX.Objects.GL.FinPeriods.TableDefinition.FinYear.year, OrderBy<Desc<PX.Objects.GL.FinPeriods.TableDefinition.FinYear.startDate>>>), takeBranchForSelectorFromQueryParams, takeOrganizationForSelectorFromQueryParams), new Type[1]
    {
      typeof (MasterFinYear.year)
    })
  {
    if (searchType == (Type) null)
      searchType = typeof (Search3<PX.Objects.GL.FinPeriods.TableDefinition.FinYear.year, OrderBy<Desc<PX.Objects.GL.FinPeriods.TableDefinition.FinYear.startDate>>>);
    this.SearchType = searchType;
    this.SourceType = sourceType;
    this.BranchSourceType = branchSourceType;
    this.BranchSourceFormulaType = branchSourceFormulaType;
    this.OrganizationSourceType = organizationSourceType;
    this.TakeBranchForSelectorFromQueryParams = takeBranchForSelectorFromQueryParams;
    this.TakeOrganizationForSelectorFromQueryParams = takeOrganizationForSelectorFromQueryParams;
    this.UseMasterCalendarSourceType = useMasterCalendarSourceType;
    PeriodKeyProviderBase.SourcesSpecificationCollection sourcesSpecification = new PeriodKeyProviderBase.SourcesSpecificationCollection();
    sourcesSpecification.SpecificationItems = new PeriodKeyProviderBase.SourceSpecificationItem()
    {
      BranchSourceType = branchSourceType,
      BranchSourceFormulaType = branchSourceFormulaType,
      OrganizationSourceType = organizationSourceType
    }.SingleToList<PeriodKeyProviderBase.SourceSpecificationItem>();
    this.CalendarOrganizationIDProvider = new CalendarOrganizationIDProvider(sourcesSpecification, useMasterCalendarSourceType: this.UseMasterCalendarSourceType, useMasterOrganizationIDByDefault: useMasterOrganizationIDByDefault);
    ((PXSelectorAttribute) this).Filterable = true;
  }

  public static Type GetSearchType(
    Type origSearchType,
    bool takeBranchForSelectorFromQueryParams,
    bool takeOrganizationForSelectorFromQueryParams)
  {
    if (!(takeBranchForSelectorFromQueryParams | takeOrganizationForSelectorFromQueryParams))
      return origSearchType;
    BqlCommand bqlCommand = BqlCommand.CreateInstance(new Type[1]
    {
      origSearchType
    });
    if (takeBranchForSelectorFromQueryParams)
      bqlCommand = bqlCommand.WhereAnd<Where<PX.Objects.GL.FinPeriods.TableDefinition.FinYear.organizationID, Equal<Optional2<QueryParameters.branchID>>>>();
    if (takeOrganizationForSelectorFromQueryParams)
      bqlCommand = bqlCommand.WhereAnd<Where<PX.Objects.GL.FinPeriods.TableDefinition.FinYear.organizationID, Equal<Optional2<QueryParameters.organizationID>>>>();
    return bqlCommand.GetType();
  }

  protected virtual IEnumerable GetRecords()
  {
    PXCache cach = this._Graph.Caches[((PXSelectorAttribute) this)._CacheType];
    object extRow = ((IEnumerable<object>) PXView.Currents).FirstOrDefault<object>((Func<object, bool>) (c => ((PXSelectorAttribute) this)._CacheType.IsAssignableFrom(c.GetType())));
    int? nullable = this.TakeBranchForSelectorFromQueryParams || this.TakeOrganizationForSelectorFromQueryParams ? this.CalendarOrganizationIDProvider.GetCalendarOrganizationID(this._Graph, PXView.Parameters, this.TakeBranchForSelectorFromQueryParams, this.TakeOrganizationForSelectorFromQueryParams) : this.CalendarOrganizationIDProvider.GetCalendarOrganizationID(cach.Graph, cach, extRow);
    int startRow = PXView.StartRow;
    int num1 = 0;
    List<object> objectList = new List<object>()
    {
      (object) nullable
    };
    BqlCommand bqlCommand1 = BqlCommand.CreateInstance(new Type[1]
    {
      this.SearchType
    }).WhereAnd<Where<PX.Objects.GL.FinPeriods.TableDefinition.FinYear.organizationID, Equal<Required<PX.Objects.GL.FinPeriods.TableDefinition.FinYear.organizationID>>>>();
    PXGraph graph = this._Graph;
    PXView view = PXView.View;
    int num2 = view != null ? (view.IsReadOnly ? 1 : 0) : 1;
    BqlCommand bqlCommand2 = bqlCommand1;
    PXView pxView = new PXView(graph, num2 != 0, bqlCommand2);
    try
    {
      return (IEnumerable) pxView.Select(PXView.Currents, objectList.ToArray(), PXView.Searches, PXView.SortColumns, PXView.Descendings, PXView.PXFilterRowCollection.op_Implicit(PXView.Filters), ref startRow, PXView.MaximumRows, ref num1);
    }
    finally
    {
      PXView.StartRow = 0;
    }
  }
}
