// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.INPIReviewExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using PX.Objects.IN;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.PM;

public class INPIReviewExt : PXGraphExtension<INPIReview>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.projectAccounting>();

  [PXOverride]
  public virtual INAdjustmentEntry CreateAdjustmentEntry(Func<INAdjustmentEntry> baseMethod)
  {
    INAdjustmentEntry adjustmentEntry = baseMethod();
    INAdjustmentEntryExt extension = ((PXGraph) adjustmentEntry).GetExtension<INAdjustmentEntryExt>();
    if (extension == null)
      return adjustmentEntry;
    extension.IsTaskErrorsHandlingIsEnabled = true;
    return adjustmentEntry;
  }

  [PXOverride]
  public virtual void HandleAdjustmentExceptions(
    INAdjustmentEntry je,
    List<PXException> exceptions,
    Action<INAdjustmentEntry, List<PXException>> baseMethod)
  {
    INAdjustmentEntryExt extension = ((PXGraph) je).GetExtension<INAdjustmentEntryExt>();
    if (extension != null)
      this.ThrowComplexTaskException(extension.TaskExceptions);
    baseMethod(je, exceptions);
  }

  private void ThrowComplexTaskException(List<PXTaskSetPropertyException> taskExceptions)
  {
    Dictionary<int, string> dictionary = new Dictionary<int, string>();
    Dictionary<int, HashSet<string>> source = new Dictionary<int, HashSet<string>>();
    foreach (PXTaskSetPropertyException taskException in taskExceptions)
    {
      int? taskId = taskException.TaskID;
      if (taskId.HasValue)
      {
        int? projectId = taskException.ProjectID;
        if (projectId.HasValue)
        {
          PMTask pmTask = PMTask.PK.Find((PXGraph) this.Base, projectId, taskId);
          if (pmTask != null)
          {
            if (dictionary.TryGetValue(projectId.Value, out string _))
            {
              HashSet<string> stringSet;
              if (source.TryGetValue(projectId.Value, out stringSet))
                stringSet.Add(pmTask.TaskCD);
              else
                source.Add(pmTask.ProjectID.Value, new HashSet<string>()
                {
                  pmTask.TaskCD
                });
            }
            else
            {
              PMProject pmProject = PMProject.PK.Find((PXGraph) this.Base, projectId);
              if (pmProject != null)
              {
                dictionary.Add(projectId.Value, pmProject.ContractCD);
                source.Add(projectId.Value, new HashSet<string>()
                {
                  pmTask.TaskCD
                });
              }
            }
          }
        }
      }
    }
    if (source.Count == 0)
      return;
    KeyValuePair<int, HashSet<string>> keyValuePair = source.Count == 1 ? source.First<KeyValuePair<int, HashSet<string>>>() : throw new PXException("The adjustment cannot be released because some inventory transactions have inactive project tasks associated. You can activate the project tasks on the Projects (PM301000) or Project Tasks (PM302000) forms. See the Trace for details.");
    string str1 = dictionary[keyValuePair.Key];
    string str2 = string.Join(", ", (IEnumerable<string>) keyValuePair.Value);
    if (keyValuePair.Value.Count == 1)
      throw new PXException("The adjustment cannot be released because at least one inventory transaction related to the {0} project has the inactive {1} project task associated. You can activate the project task on the Projects (PM301000) or Project Tasks (PM302000) forms.", new object[2]
      {
        (object) str1,
        (object) str2
      });
    throw new PXException("The adjustment cannot be released because some inventory transactions related to the {0} project have the following inactive project tasks associated: {1}. You can activate the tasks on the Projects (PM301000) or Project Tasks (PM302000) forms.", new object[2]
    {
      (object) str1,
      (object) str2
    });
  }
}
