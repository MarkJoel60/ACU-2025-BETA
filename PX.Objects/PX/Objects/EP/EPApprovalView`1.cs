// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EPApprovalView`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.Common.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

#nullable disable
namespace PX.Objects.EP;

public class EPApprovalView<Table>(PXGraph graph) : PXSetup<Table>(graph) where Table : class, IBqlTable, new()
{
  public static PXResultset<Table> Select(PXGraph graph, params object[] pars)
  {
    return EPApprovalView<Table>.SelectMultiBound(graph, (object[]) null, pars);
  }

  public static PXResultset<Table> SelectMultiBound(
    PXGraph graph,
    object[] currents,
    params object[] pars)
  {
    PXCache cach = graph.Caches[typeof (Table)];
    PXGraph.GetDefaultDelegate getDefaultDelegate;
    if (graph.Defaults.TryGetValue(typeof (Table), out getDefaultDelegate))
    {
      cach.Current = (object) null;
      PXResultset<Table> pxResultset = new PXResultset<Table>();
      pxResultset.Add(new PXResult<Table>(getDefaultDelegate.Invoke() as Table));
      return pxResultset;
    }
    if (cach.Keys.Count == 0)
      return PXSelectBase<Table, PXSelectReadonly<Table>.Config>.Select(graph, pars);
    string str1 = (string) null;
    lock (((ICollection) PXSetup<Table>._members).SyncRoot)
    {
      if (!PXSetup<Table>._members.TryGetValue(graph.GetType(), out str1))
      {
        foreach (PXViewInfo pxViewInfo in (IEnumerable<PXViewInfo>) GraphHelper.GetGraphViews(graph.GetType(), false).OrderByAccordanceTo<PXViewInfo>((Func<PXViewInfo, bool>) (v => !v.HasInferredDisplayName)))
        {
          if (((PXInfo) pxViewInfo.Cache).Name == typeof (Table).FullName)
          {
            str1 = ((PXInfo) pxViewInfo).Name;
            break;
          }
        }
        if (str1 == null)
        {
          foreach (string str2 in (IEnumerable<string>) ((Dictionary<string, PXView>) graph.Views).Keys.OrderBy<string, string>((Func<string, string>) (s => s), (IComparer<string>) new PXSetup<Table>.ViewNameComparer()))
          {
            PXView view = graph.Views[str2];
            if (!view.IsReadOnly && (view.GetItemType() == typeof (Table) || view.GetItemType().IsAssignableFrom(typeof (Table)) && !Attribute.IsDefined((MemberInfo) typeof (Table), typeof (PXBreakInheritanceAttribute), false)))
            {
              str1 = str2;
              break;
            }
          }
        }
        PXSetup<Table>._members[graph.GetType()] = str1;
      }
    }
    if (string.IsNullOrEmpty(str1))
      return (PXResultset<Table>) null;
    PXView view1 = graph.Views[str1];
    PXSelectBase<Table> viewExternalMember = PXSetup<Table>.GetViewExternalMember(graph, view1) as PXSelectBase<Table>;
    PXResultset<Table> pxResultset1 = new PXResultset<Table>();
    if (viewExternalMember != null)
    {
      foreach (object obj in ((PXSelectBase) viewExternalMember).View.SelectMultiBound(currents, pars))
      {
        if (!(obj is PXResult<Table> pxResult))
        {
          if (obj is Table able)
            pxResultset1.Add(new PXResult<Table>(able));
        }
        else
          pxResultset1.Add(pxResult);
      }
      return pxResultset1;
    }
    foreach (object obj in view1.SelectMultiBound(currents, pars))
    {
      if (!(obj is PXResult<Table> pxResult))
      {
        if (obj is Table able)
          pxResultset1.Add(new PXResult<Table>(able));
      }
      else
        pxResultset1.Add(pxResult);
    }
    return pxResultset1;
  }
}
