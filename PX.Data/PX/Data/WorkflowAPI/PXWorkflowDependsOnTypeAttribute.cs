// Decompiled with JetBrains decompiler
// Type: PX.Data.WorkflowAPI.PXWorkflowDependsOnTypeAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data.WorkflowAPI;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public class PXWorkflowDependsOnTypeAttribute : Attribute
{
  public System.Type[] Types { get; }

  public PXWorkflowDependsOnTypeAttribute(params System.Type[] types) => this.Types = types;
}
