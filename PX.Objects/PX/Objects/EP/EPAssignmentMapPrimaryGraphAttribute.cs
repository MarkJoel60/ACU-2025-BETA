// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EPAssignmentMapPrimaryGraphAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CR;
using System;

#nullable disable
namespace PX.Objects.EP;

public sealed class EPAssignmentMapPrimaryGraphAttribute : 
  CRCacheIndependentPrimaryGraphListAttribute
{
  public EPAssignmentMapPrimaryGraphAttribute()
    : base(new System.Type[4]
    {
      typeof (EPApprovalMapMaint),
      typeof (EPAssignmentMapMaint),
      typeof (EPAssignmentMaint),
      typeof (EPAssignmentAndApprovalMapEnq)
    }, new System.Type[4]
    {
      typeof (Select<EPAssignmentMap, Where<EPAssignmentMap.assignmentMapID, Equal<Current<EPAssignmentMap.assignmentMapID>>, And<EPAssignmentMap.mapType, Equal<EPMapType.approval>>>>),
      typeof (Select<EPAssignmentMap, Where<EPAssignmentMap.assignmentMapID, Equal<Current<EPAssignmentMap.assignmentMapID>>, And<EPAssignmentMap.mapType, Equal<EPMapType.assignment>>>>),
      typeof (Select<EPAssignmentMap, Where<EPAssignmentMap.assignmentMapID, Equal<Current<EPAssignmentMap.assignmentMapID>>, And<EPAssignmentMap.mapType, Equal<EPMapType.legacy>, And<EPAssignmentMap.assignmentMapID, Greater<Zero>>>>>),
      typeof (Select<EPAssignmentMap>)
    })
  {
  }

  protected override void OnAccessDenied(System.Type graphType)
  {
    throw new AccessViolationException(PX.Objects.CR.Messages.FormNoAccessRightsMessage(graphType));
  }
}
