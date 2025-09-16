// Decompiled with JetBrains decompiler
// Type: PX.Data.Automation.IWorkflowFieldsService
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.SM;
using System.Collections.Generic;

#nullable disable
namespace PX.Data.Automation;

[PXInternalUseOnly]
public interface IWorkflowFieldsService
{
  void SetFormValues(
    PXGraph graph,
    string form,
    IDictionary<string, object> values,
    bool useMulti = false);

  Dictionary<string, AUWorkflowFormField[]> GetFormFields(string screenId);
}
