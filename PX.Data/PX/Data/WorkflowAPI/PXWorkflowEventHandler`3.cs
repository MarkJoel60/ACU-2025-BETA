// Decompiled with JetBrains decompiler
// Type: PX.Data.WorkflowAPI.PXWorkflowEventHandler`3
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.WorkflowAPI;

public class PXWorkflowEventHandler<TPrimaryEntity, TTargetEntity, TParams> : 
  PXWorkflowEventHandler<TPrimaryEntity, TTargetEntity>
  where TPrimaryEntity : class, IBqlTable, new()
  where TTargetEntity : class, IBqlTable, new()
  where TParams : class, IBqlTable, new()
{
}
