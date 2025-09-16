// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.ChangeProjectID
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections;
using System.Collections.Specialized;

#nullable disable
namespace PX.Objects.PM;

public class ChangeProjectID(PXGraph graph, string name) : 
  PXChangeID<PMProject, PMProject.contractCD>(graph, name)
{
  [PXUIField]
  [PXButton]
  protected virtual IEnumerable Handler(PXAdapter adapter)
  {
    if (adapter.View.Cache.Current != null && adapter.View.Cache.GetStatus(adapter.View.Cache.Current) != 2)
    {
      WebDialogResult webDialogResult = adapter.View.Cache.Graph.Views["ChangeIDDialog"].AskExt();
      string newCd;
      if ((webDialogResult == 1 || webDialogResult == 6 && ((PXAction) this).Graph.IsExport) && !string.IsNullOrWhiteSpace(newCd = PXChangeID<PMProject, PMProject.contractCD>.GetNewCD(adapter)))
      {
        this.ChangeCDProject(adapter.View.Cache, PXChangeID<PMProject, PMProject.contractCD>.GetOldCD(adapter), newCd, this.GetType(adapter));
        if (adapter.SortColumns != null && adapter.SortColumns.Length != 0 && string.Equals(adapter.SortColumns[0], typeof (PMProject.contractCD).Name, StringComparison.OrdinalIgnoreCase) && adapter.Searches != null && adapter.Searches.Length != 0)
          adapter.Searches[0] = (object) newCd;
      }
    }
    if (((PXAction) this).Graph.IsContractBasedAPI)
      ((PXAction) this).Graph.Actions.PressSave();
    return adapter.Get();
  }

  protected string GetType(PXAdapter adapter)
  {
    return (string) adapter.View.Cache.GetValue(adapter.View.Cache.Current, typeof (PMProject.baseType).Name);
  }

  public void ChangeCDProject(PXCache cache, string oldCD, string newCD, string type)
  {
    OrderedDictionary orderedDictionary1 = new OrderedDictionary((IEqualityComparer) StringComparer.OrdinalIgnoreCase)
    {
      {
        (object) typeof (PMProject.contractCD).Name,
        (object) oldCD
      },
      {
        (object) typeof (PMProject.baseType).Name,
        (object) type
      }
    };
    OrderedDictionary orderedDictionary2 = new OrderedDictionary((IEqualityComparer) StringComparer.OrdinalIgnoreCase)
    {
      {
        (object) typeof (PMProject.contractCD).Name,
        (object) newCD
      },
      {
        (object) typeof (PMProject.baseType).Name,
        (object) type
      }
    };
    cache.Update((IDictionary) orderedDictionary1, (IDictionary) orderedDictionary2);
  }

  protected virtual void FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    ProjectCDRestrictorAttribute.Verify(sender, e);
    base.FieldVerifying(sender, e);
  }
}
