// Decompiled with JetBrains decompiler
// Type: PX.Data.GIDescription
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.Maintenance.GI;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data;

/// <exclude />
public class GIDescription : ICloneable
{
  public Guid DesignID { get; set; }

  public GIDesign Design { get; set; }

  public IEnumerable<GIFilter> Filters { get; set; } = Enumerable.Empty<GIFilter>();

  public IEnumerable<GIGroupBy> GroupBys { get; set; } = Enumerable.Empty<GIGroupBy>();

  public IEnumerable<GIMassAction> MassActions { get; set; } = Enumerable.Empty<GIMassAction>();

  public IEnumerable<GIMassUpdateField> MassUpdateFields { get; set; } = Enumerable.Empty<GIMassUpdateField>();

  public IEnumerable<GINavigationScreen> NavigationScreens { get; set; } = Enumerable.Empty<GINavigationScreen>();

  public IEnumerable<GINavigationParameter> NavigationParameters { get; set; } = Enumerable.Empty<GINavigationParameter>();

  public IEnumerable<GINavigationCondition> NavigationConditions { get; set; } = Enumerable.Empty<GINavigationCondition>();

  public IEnumerable<GIRecordDefault> RecordDefaults { get; set; } = Enumerable.Empty<GIRecordDefault>();

  public IEnumerable<GIRelation> Relations { get; set; } = Enumerable.Empty<GIRelation>();

  public IEnumerable<GIOn> Ons { get; set; } = Enumerable.Empty<GIOn>();

  public IEnumerable<GIResult> Results { get; set; } = Enumerable.Empty<GIResult>();

  public IEnumerable<GISort> Sorts { get; set; } = Enumerable.Empty<GISort>();

  public IEnumerable<GITable> Tables { get; set; } = Enumerable.Empty<GITable>();

  public IEnumerable<GIWhere> Wheres { get; set; } = Enumerable.Empty<GIWhere>();

  public bool RetrieveTotalsOnly { get; set; }

  public GIDescription(Guid designID) => this.DesignID = designID;

  public object Clone()
  {
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    return (object) new GIDescription(this.DesignID)
    {
      Design = PXCache<GIDesign>.CreateCopy(this.Design),
      Filters = (IEnumerable<GIFilter>) this.Filters.Select<GIFilter, GIFilter>(GIDescription.\u003C\u003EO.\u003C0\u003E__CreateCopy ?? (GIDescription.\u003C\u003EO.\u003C0\u003E__CreateCopy = new Func<GIFilter, GIFilter>(PXCache<GIFilter>.CreateCopy))).ToArray<GIFilter>(),
      GroupBys = (IEnumerable<GIGroupBy>) this.GroupBys.Select<GIGroupBy, GIGroupBy>(GIDescription.\u003C\u003EO.\u003C1\u003E__CreateCopy ?? (GIDescription.\u003C\u003EO.\u003C1\u003E__CreateCopy = new Func<GIGroupBy, GIGroupBy>(PXCache<GIGroupBy>.CreateCopy))).ToArray<GIGroupBy>(),
      MassActions = (IEnumerable<GIMassAction>) this.MassActions.Select<GIMassAction, GIMassAction>(GIDescription.\u003C\u003EO.\u003C2\u003E__CreateCopy ?? (GIDescription.\u003C\u003EO.\u003C2\u003E__CreateCopy = new Func<GIMassAction, GIMassAction>(PXCache<GIMassAction>.CreateCopy))).ToArray<GIMassAction>(),
      MassUpdateFields = (IEnumerable<GIMassUpdateField>) this.MassUpdateFields.Select<GIMassUpdateField, GIMassUpdateField>(GIDescription.\u003C\u003EO.\u003C3\u003E__CreateCopy ?? (GIDescription.\u003C\u003EO.\u003C3\u003E__CreateCopy = new Func<GIMassUpdateField, GIMassUpdateField>(PXCache<GIMassUpdateField>.CreateCopy))).ToArray<GIMassUpdateField>(),
      NavigationScreens = (IEnumerable<GINavigationScreen>) this.NavigationScreens.Select<GINavigationScreen, GINavigationScreen>(GIDescription.\u003C\u003EO.\u003C4\u003E__CreateCopy ?? (GIDescription.\u003C\u003EO.\u003C4\u003E__CreateCopy = new Func<GINavigationScreen, GINavigationScreen>(PXCache<GINavigationScreen>.CreateCopy))).ToArray<GINavigationScreen>(),
      NavigationParameters = (IEnumerable<GINavigationParameter>) this.NavigationParameters.Select<GINavigationParameter, GINavigationParameter>(GIDescription.\u003C\u003EO.\u003C5\u003E__CreateCopy ?? (GIDescription.\u003C\u003EO.\u003C5\u003E__CreateCopy = new Func<GINavigationParameter, GINavigationParameter>(PXCache<GINavigationParameter>.CreateCopy))).ToArray<GINavigationParameter>(),
      NavigationConditions = (IEnumerable<GINavigationCondition>) this.NavigationConditions.Select<GINavigationCondition, GINavigationCondition>(GIDescription.\u003C\u003EO.\u003C6\u003E__CreateCopy ?? (GIDescription.\u003C\u003EO.\u003C6\u003E__CreateCopy = new Func<GINavigationCondition, GINavigationCondition>(PXCache<GINavigationCondition>.CreateCopy))).ToArray<GINavigationCondition>(),
      RecordDefaults = (IEnumerable<GIRecordDefault>) this.RecordDefaults.Select<GIRecordDefault, GIRecordDefault>(GIDescription.\u003C\u003EO.\u003C7\u003E__CreateCopy ?? (GIDescription.\u003C\u003EO.\u003C7\u003E__CreateCopy = new Func<GIRecordDefault, GIRecordDefault>(PXCache<GIRecordDefault>.CreateCopy))).ToArray<GIRecordDefault>(),
      Relations = (IEnumerable<GIRelation>) this.Relations.Select<GIRelation, GIRelation>(GIDescription.\u003C\u003EO.\u003C8\u003E__CreateCopy ?? (GIDescription.\u003C\u003EO.\u003C8\u003E__CreateCopy = new Func<GIRelation, GIRelation>(PXCache<GIRelation>.CreateCopy))).ToArray<GIRelation>(),
      Ons = (IEnumerable<GIOn>) this.Ons.Select<GIOn, GIOn>(GIDescription.\u003C\u003EO.\u003C9\u003E__CreateCopy ?? (GIDescription.\u003C\u003EO.\u003C9\u003E__CreateCopy = new Func<GIOn, GIOn>(PXCache<GIOn>.CreateCopy))).ToArray<GIOn>(),
      Results = (IEnumerable<GIResult>) this.Results.Select<GIResult, GIResult>(GIDescription.\u003C\u003EO.\u003C10\u003E__CreateCopy ?? (GIDescription.\u003C\u003EO.\u003C10\u003E__CreateCopy = new Func<GIResult, GIResult>(PXCache<GIResult>.CreateCopy))).ToArray<GIResult>(),
      Sorts = (IEnumerable<GISort>) this.Sorts.Select<GISort, GISort>(GIDescription.\u003C\u003EO.\u003C11\u003E__CreateCopy ?? (GIDescription.\u003C\u003EO.\u003C11\u003E__CreateCopy = new Func<GISort, GISort>(PXCache<GISort>.CreateCopy))).ToArray<GISort>(),
      Tables = (IEnumerable<GITable>) this.Tables.Select<GITable, GITable>(GIDescription.\u003C\u003EO.\u003C12\u003E__CreateCopy ?? (GIDescription.\u003C\u003EO.\u003C12\u003E__CreateCopy = new Func<GITable, GITable>(PXCache<GITable>.CreateCopy))).ToArray<GITable>(),
      Wheres = (IEnumerable<GIWhere>) this.Wheres.Select<GIWhere, GIWhere>(GIDescription.\u003C\u003EO.\u003C13\u003E__CreateCopy ?? (GIDescription.\u003C\u003EO.\u003C13\u003E__CreateCopy = new Func<GIWhere, GIWhere>(PXCache<GIWhere>.CreateCopy))).ToArray<GIWhere>(),
      RetrieveTotalsOnly = this.RetrieveTotalsOnly
    };
  }
}
