// Decompiled with JetBrains decompiler
// Type: PX.Data.PXViewIncludesArchivedRecordsAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data;

[AttributeUsage(AttributeTargets.Field)]
public class PXViewIncludesArchivedRecordsAttribute : PXViewExtensionAttribute
{
  public override void ViewCreated(PXGraph graph, string viewName)
  {
    graph.Views[viewName].ForceReadArchived = true;
  }
}
