// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EPAssignmentMapSelectorAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections;
using System.Web.Compilation;

#nullable disable
namespace PX.Objects.EP;

public class EPAssignmentMapSelectorAttribute : PXCustomSelectorAttribute
{
  public virtual int MapType { get; set; }

  public EPAssignmentMapSelectorAttribute()
    : base(typeof (EPAssignmentMap.assignmentMapID), new Type[2]
    {
      typeof (EPAssignmentMap.name),
      typeof (EPAssignmentMap.entityType)
    })
  {
    ((PXSelectorAttribute) this).DescriptionField = typeof (EPAssignmentMap.name);
    ((PXSelectorAttribute) this).SelectorMode = (PXSelectorMode) 16 /*0x10*/;
  }

  public IEnumerable GetRecords()
  {
    EPAssignmentMapSelectorAttribute selectorAttribute = this;
    foreach (PXResult<EPAssignmentMap> pxResult in PXSelectBase<EPAssignmentMap, PXSelect<EPAssignmentMap>.Config>.Select(selectorAttribute._Graph, Array.Empty<object>()))
    {
      EPAssignmentMap record = PXResult<EPAssignmentMap>.op_Implicit(pxResult);
      int? nullable = record.AssignmentMapID;
      int num = 0;
      if (nullable.GetValueOrDefault() >= num & nullable.HasValue)
      {
        nullable = record.MapType;
        if (nullable.HasValue || selectorAttribute.MapType == 0)
        {
          nullable = record.MapType;
          if (nullable.HasValue)
          {
            nullable = record.MapType;
            int mapType = selectorAttribute.MapType;
            if (!(nullable.GetValueOrDefault() == mapType & nullable.HasValue))
              continue;
          }
        }
        else
          continue;
      }
      if (record.GraphType != null)
      {
        Type type = PXBuildManager.GetType(record.GraphType, false);
        if ((type != (Type) null ? PXSiteMapProviderExtensions.FindSiteMapNode(PXSiteMap.Provider, type) : (PXSiteMapNode) null) != null)
          yield return (object) record;
      }
      else if (record.EntityType != null)
      {
        Type type = PXBuildManager.GetType(record.EntityType, false);
        if (type != (Type) null)
        {
          Type primaryGraphType = EntityHelper.GetPrimaryGraphType(selectorAttribute._Graph, type);
          if (primaryGraphType != (Type) null && PXSiteMapProviderExtensions.FindSiteMapNode(PXSiteMap.Provider, primaryGraphType) != null)
            yield return (object) record;
        }
      }
    }
  }
}
