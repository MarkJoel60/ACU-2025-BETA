// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INSiteStatusLookup`2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

#nullable enable
namespace PX.Objects.IN;

[Obsolete("This class is obsolete. Use AddItemLookupBaseExt instead.")]
public class INSiteStatusLookup<Status, StatusFilter> : PXSelectBase<
#nullable disable
Status>
  where Status : class, IBqlTable, new()
  where StatusFilter : class, IBqlTable, new()
{
  public const string Selected = "Selected";
  public const string QtySelected = "QtySelected";
  private PXView intView;

  public INSiteStatusLookup(PXGraph graph)
  {
    PXGraph pxGraph = graph;
    BqlCommand instance = BqlCommand.CreateInstance(new Type[1]
    {
      BqlCommand.Compose(new Type[2]
      {
        typeof (Select<>),
        typeof (Status)
      })
    });
    INSiteStatusLookup<Status, StatusFilter> siteStatusLookup = this;
    // ISSUE: virtual method pointer
    PXSelectDelegate pxSelectDelegate = new PXSelectDelegate((object) siteStatusLookup, __vmethodptr(siteStatusLookup, viewHandler));
    ((PXSelectBase) this).View = new PXView(pxGraph, false, instance, (Delegate) pxSelectDelegate);
    this.InitHandlers(graph);
  }

  public INSiteStatusLookup(PXGraph graph, Delegate handler)
  {
    ((PXSelectBase) this).View = new PXView(graph, false, BqlCommand.CreateInstance(new Type[2]
    {
      typeof (Select<>),
      typeof (Status)
    }), handler);
    this.InitHandlers(graph);
  }

  private void InitHandlers(PXGraph graph)
  {
    PXGraph.RowSelectedEvents rowSelected1 = graph.RowSelected;
    Type type1 = typeof (StatusFilter);
    INSiteStatusLookup<Status, StatusFilter> siteStatusLookup1 = this;
    // ISSUE: virtual method pointer
    PXRowSelected pxRowSelected1 = new PXRowSelected((object) siteStatusLookup1, __vmethodptr(siteStatusLookup1, OnFilterSelected));
    rowSelected1.AddHandler(type1, pxRowSelected1);
    PXGraph.RowSelectedEvents rowSelected2 = graph.RowSelected;
    Type type2 = typeof (Status);
    INSiteStatusLookup<Status, StatusFilter> siteStatusLookup2 = this;
    // ISSUE: virtual method pointer
    PXRowSelected pxRowSelected2 = new PXRowSelected((object) siteStatusLookup2, __vmethodptr(siteStatusLookup2, OnRowSelected));
    rowSelected2.AddHandler(type2, pxRowSelected2);
    PXGraph.FieldUpdatedEvents fieldUpdated = graph.FieldUpdated;
    Type type3 = typeof (Status);
    INSiteStatusLookup<Status, StatusFilter> siteStatusLookup3 = this;
    // ISSUE: virtual method pointer
    PXFieldUpdated pxFieldUpdated = new PXFieldUpdated((object) siteStatusLookup3, __vmethodptr(siteStatusLookup3, OnSelectedUpdated));
    fieldUpdated.AddHandler(type3, "Selected", pxFieldUpdated);
  }

  protected virtual void OnFilterSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is INSiteStatusFilter row))
      return;
    PXUIFieldAttribute.SetVisible(sender.Graph.Caches[typeof (Status)], typeof (INSiteStatusByCostCenter.siteID).Name, !row.SiteID.HasValue);
  }

  protected virtual void OnSelectedUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    bool? nullable1 = (bool?) sender.GetValue(e.Row, "Selected");
    Decimal? nullable2 = (Decimal?) sender.GetValue(e.Row, "QtySelected");
    if (nullable1.GetValueOrDefault())
    {
      if (nullable2.HasValue)
      {
        Decimal? nullable3 = nullable2;
        Decimal num = 0M;
        if (!(nullable3.GetValueOrDefault() == num & nullable3.HasValue))
          return;
      }
      sender.SetValue(e.Row, "QtySelected", (object) 1M);
    }
    else
      sender.SetValue(e.Row, "QtySelected", (object) 0M);
  }

  protected virtual void OnRowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    PXUIFieldAttribute.SetEnabled(sender, e.Row, false);
    PXUIFieldAttribute.SetEnabled(sender, e.Row, "Selected", true);
    PXUIFieldAttribute.SetEnabled(sender, e.Row, "QtySelected", true);
  }

  protected virtual PXView CreateIntView(PXGraph graph)
  {
    PXCache cach = graph.Caches[typeof (Status)];
    Type type = BqlCommand.Compose(new List<Type>()
    {
      typeof (Select<,>),
      typeof (Status),
      INSiteStatusLookup<Status, StatusFilter>.CreateWhere(graph)
    }.ToArray());
    return (PXView) new INSiteStatusLookup<Status, StatusFilter>.LookupView(graph, BqlCommand.CreateInstance(new Type[1]
    {
      type
    }));
  }

  protected virtual IEnumerable viewHandler()
  {
    if (this.intView == null)
      this.intView = this.CreateIntView(((PXSelectBase) this).View.Graph);
    int startRow = PXView.StartRow;
    int num = 0;
    PXDelegateResult pxDelegateResult = new PXDelegateResult();
    foreach (object obj in this.intView.Select((object[]) null, (object[]) null, PXView.Searches, PXView.SortColumns, PXView.Descendings, PXView.PXFilterRowCollection.op_Implicit(PXView.Filters), ref startRow, PXView.MaximumRows, ref num))
    {
      Status status1 = PXResult.Unwrap<Status>(obj);
      Status status2 = status1;
      if (((PXSelectBase) this).Cache.Locate((object) status1) is Status status3 && (((PXSelectBase) this).Cache.GetValue((object) status3, "Selected") as bool?).GetValueOrDefault())
      {
        Decimal? nullable = ((PXSelectBase) this).Cache.GetValue((object) status3, "QtySelected") as Decimal?;
        ((PXSelectBase) this).Cache.RestoreCopy((object) status3, (object) status1);
        ((PXSelectBase) this).Cache.SetValue((object) status3, "Selected", (object) true);
        ((PXSelectBase) this).Cache.SetValue((object) status3, "QtySelected", (object) nullable);
        status2 = status3;
      }
      ((List<object>) pxDelegateResult).Add((object) status2);
    }
    PXView.StartRow = 0;
    if (PXView.ReverseOrder)
      ((List<object>) pxDelegateResult).Reverse();
    pxDelegateResult.IsResultSorted = true;
    return (IEnumerable) pxDelegateResult;
  }

  protected static Type CreateWhere(PXGraph graph)
  {
    PXCache cach1 = graph.Caches[typeof (INSiteStatusFilter)];
    PXCache cach2 = graph.Caches[typeof (Status)];
    Type where = typeof (Where<boolTrue, Equal<boolTrue>>);
    foreach (string field in (List<string>) cach1.Fields)
    {
      if (cach2.Fields.Contains(field))
      {
        if (!cach1.Fields.Contains(field + "Wildcard") && (!field.Contains("SubItem") || PXAccess.FeatureInstalled<FeaturesSet.subItem>()) && (!field.Contains("Site") || PXAccess.FeatureInstalled<FeaturesSet.warehouse>()) && (!field.Contains("Location") || PXAccess.FeatureInstalled<FeaturesSet.warehouseLocation>()))
        {
          Type bqlField1 = cach1.GetBqlField(field);
          Type bqlField2 = cach2.GetBqlField(field);
          if (bqlField1 != (Type) null && bqlField2 != (Type) null)
            where = BqlCommand.Compose(new Type[12]
            {
              typeof (Where2<,>),
              typeof (Where<,,>),
              typeof (Current<>),
              bqlField1,
              typeof (IsNull),
              typeof (Or<,>),
              bqlField2,
              typeof (Equal<>),
              typeof (Current<>),
              bqlField1,
              typeof (And<>),
              where
            });
        }
        else
          continue;
      }
      string str;
      if (field.Length > 8 && field.EndsWith("Wildcard") && cach2.Fields.Contains(str = field.Substring(0, field.Length - 8)) && (!field.Contains("SubItem") || PXAccess.FeatureInstalled<FeaturesSet.subItem>()) && (!field.Contains("Site") || PXAccess.FeatureInstalled<FeaturesSet.warehouse>()) && (!field.Contains("Location") || PXAccess.FeatureInstalled<FeaturesSet.warehouseLocation>()))
      {
        Type bqlField3 = cach1.GetBqlField(field);
        Type bqlField4 = cach2.GetBqlField(str);
        where = BqlCommand.Compose(new Type[12]
        {
          typeof (Where2<,>),
          typeof (Where<,,>),
          typeof (Current<>),
          bqlField3,
          typeof (IsNull),
          typeof (Or<,>),
          bqlField4,
          typeof (Like<>),
          typeof (Current<>),
          bqlField3,
          typeof (And<>),
          where
        });
      }
    }
    return where;
  }

  protected static Type GetTypeField<Source>(string field)
  {
    Type type = typeof (Source);
    Type typeField;
    for (typeField = (Type) null; typeField == (Type) null && type != (Type) null; type = type.BaseType)
      typeField = type.GetNestedType(field, BindingFlags.Public);
    return typeField;
  }

  protected class LookupView : PXView
  {
    public LookupView(PXGraph graph, BqlCommand command)
      : base(graph, true, command)
    {
    }

    public LookupView(PXGraph graph, BqlCommand command, Delegate handler)
      : base(graph, true, command, handler)
    {
    }

    protected PXView.PXSearchColumn CorrectFieldName(PXView.PXSearchColumn orig, bool idFound)
    {
      switch (((PXView.PXSortColumn) orig).Column.ToLower())
      {
        case "inventoryid":
          return !idFound ? new PXView.PXSearchColumn("InventoryCD", ((PXView.PXSortColumn) orig).Descending, orig.OrigSearchValue ?? orig.SearchValue) : (PXView.PXSearchColumn) null;
        case "subitemid":
          return new PXView.PXSearchColumn("SubItemCD", ((PXView.PXSortColumn) orig).Descending, orig.OrigSearchValue ?? orig.SearchValue);
        case "siteid":
          return new PXView.PXSearchColumn("SiteCD", ((PXView.PXSortColumn) orig).Descending, orig.OrigSearchValue ?? orig.SearchValue);
        case "locationid":
          return new PXView.PXSearchColumn("LocationCD", ((PXView.PXSortColumn) orig).Descending, orig.OrigSearchValue ?? orig.SearchValue);
        default:
          return orig;
      }
    }

    protected virtual List<object> InvokeDelegate(object[] parameters)
    {
      if (PXView.MaximumRows == 1 && PXView.StartRow == 0)
      {
        int? length = PXView.Searches?.Length;
        int count1 = this.Cache.Keys.Count;
        if (length.GetValueOrDefault() == count1 & length.HasValue && ((IEnumerable<object>) PXView.Searches).All<object>((Func<object, bool>) (s => s != null)))
        {
          length = PXView.SortColumns?.Length;
          int count2 = this.Cache.Keys.Count;
          if (length.GetValueOrDefault() == count2 & length.HasValue && ((IEnumerable<string>) PXView.SortColumns).SequenceEqual<string>((IEnumerable<string>) this.Cache.Keys, (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase))
            return base.InvokeDelegate(parameters);
        }
      }
      PXView.Context context = PXView._Executing.Peek();
      PXView.PXSearchColumn[] sorts = context.Sorts;
      bool idFound = false;
      List<PXView.PXSearchColumn> pxSearchColumnList = new List<PXView.PXSearchColumn>();
      for (int index = 0; index < sorts.Length - this.Cache.Keys.Count; ++index)
      {
        pxSearchColumnList.Add(this.CorrectFieldName(sorts[index], false));
        if (((PXView.PXSortColumn) sorts[index]).Column == "InventoryCD")
          idFound = true;
      }
      for (int index = sorts.Length - this.Cache.Keys.Count; index < sorts.Length; ++index)
      {
        PXView.PXSearchColumn pxSearchColumn = this.CorrectFieldName(sorts[index], idFound);
        if (pxSearchColumn != null)
          pxSearchColumnList.Add(pxSearchColumn);
      }
      context.Sorts = pxSearchColumnList.ToArray();
      return base.InvokeDelegate(parameters);
    }
  }

  private class Zero : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Constant<
    #nullable disable
    INSiteStatusLookup<Status, StatusFilter>.Zero>
  {
    public Zero()
      : base(0M)
    {
    }
  }
}
