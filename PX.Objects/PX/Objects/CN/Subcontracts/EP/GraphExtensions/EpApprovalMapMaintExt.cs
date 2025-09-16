// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Subcontracts.EP.GraphExtensions.EpApprovalMapMaintExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CN.Common.Extensions;
using PX.Objects.CN.Subcontracts.SC.Graphs;
using PX.Objects.CN.Subcontracts.SM.Extension;
using PX.Objects.CS;
using PX.Objects.EP;
using PX.SM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.CN.Subcontracts.EP.GraphExtensions;

public class EpApprovalMapMaintExt : PXGraphExtension<EPApprovalMapMaint>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.construction>();

  protected IEnumerable entityItems(string parent)
  {
    EpApprovalMapMaintExt approvalMapMaintExt = this;
    if (((PXSelectBase<EPAssignmentMap>) approvalMapMaintExt.Base.AssigmentMap).Current != null)
    {
      Type type = GraphHelper.GetType(((PXSelectBase<EPAssignmentMap>) approvalMapMaintExt.Base.AssigmentMap).Current.EntityType);
      Type primaryGraphType;
      if (((PXSelectBase<EPAssignmentMap>) approvalMapMaintExt.Base.AssigmentMap).Current.GraphType == null)
      {
        if (type == (Type) null && parent != null)
          yield break;
        primaryGraphType = EntityHelper.GetPrimaryGraphType((PXGraph) approvalMapMaintExt.Base, type);
      }
      else
        primaryGraphType = GraphHelper.GetType(((PXSelectBase<EPAssignmentMap>) approvalMapMaintExt.Base.AssigmentMap).Current.GraphType);
      foreach (CacheEntityItem cacheEntityItem in EMailSourceHelper.TemplateEntity((PXGraph) approvalMapMaintExt.Base, parent, type?.FullName, primaryGraphType?.FullName))
      {
        if (primaryGraphType == typeof (SubcontractEntry))
          cacheEntityItem.Name = cacheEntityItem.GetSubcontractViewName();
        yield return (object) cacheEntityItem;
      }
      primaryGraphType = (Type) null;
    }
  }

  protected virtual void EPAssignmentMap_GraphType_FieldSelecting(
    PXCache cache,
    PXFieldSelectingEventArgs args,
    PXFieldSelecting baseHandler)
  {
    baseHandler.Invoke(cache, args);
    PXStringListAttribute.AppendList<EPAssignmentMap.graphType>(cache, args.Row, typeof (SubcontractEntry).FullName.CreateArray<string>(), "Subcontracts".CreateArray<string>());
  }

  protected virtual void EPRuleCondition_Entity_FieldSelecting(
    PXCache cache,
    PXFieldSelectingEventArgs args,
    PXFieldSelecting baseHandler)
  {
    if (((PXSelectBase<EPAssignmentMap>) this.Base.AssigmentMap).Current != null && ((PXSelectBase<EPAssignmentMap>) this.Base.AssigmentMap).Current?.GraphType == typeof (SubcontractEntry).FullName)
      args.ReturnState = (object) this.CreateFieldStateForEntity(args.ReturnValue, ((PXSelectBase<EPAssignmentMap>) this.Base.AssigmentMap).Current.EntityType, ((PXSelectBase<EPAssignmentMap>) this.Base.AssigmentMap).Current.GraphType);
    else
      baseHandler.Invoke(cache, args);
  }

  private PXFieldState CreateFieldStateForEntity(
    object returnState,
    string entityTypeName,
    string graphTypeName)
  {
    List<string> stringList1 = new List<string>();
    List<string> stringList2 = new List<string>();
    Type type1 = GraphHelper.GetType(entityTypeName);
    if (type1 != (Type) null)
    {
      Type type2 = EntityHelper.GetPrimaryGraphType((PXGraph) this.Base, type1);
      if (!string.IsNullOrEmpty(graphTypeName))
        type2 = GraphHelper.GetType(graphTypeName);
      if (type2 == (Type) null)
      {
        PXCacheNameAttribute[] customAttributes = (PXCacheNameAttribute[]) type1.GetCustomAttributes(typeof (PXCacheNameAttribute), true);
        if (type1.IsSubclassOf(typeof (CSAnswers)))
        {
          stringList1.Add(type1.FullName);
          string str = customAttributes.Length != 0 ? ((PXNameAttribute) customAttributes[0]).Name : type1.Name;
          stringList2.Add(str);
        }
      }
      else
      {
        foreach (CacheEntityItem cacheEntityItem in Enumerable.Cast<CacheEntityItem>(EMailSourceHelper.TemplateEntity((PXGraph) this.Base, (string) null, type1.FullName, type2.FullName)).Where<CacheEntityItem>((Func<CacheEntityItem, bool>) (cacheEntityItem => cacheEntityItem.SubKey != typeof (CSAnswers).FullName)))
        {
          stringList1.Add(cacheEntityItem.SubKey);
          stringList2.Add(cacheEntityItem.GetSubcontractViewName());
        }
      }
    }
    return PXStringState.CreateInstance(returnState, new int?(60), new bool?(false), "Entity", new bool?(false), new int?(1), (string) null, stringList1.ToArray(), stringList2.ToArray(), new bool?(true), (string) null, (string[]) null);
  }
}
