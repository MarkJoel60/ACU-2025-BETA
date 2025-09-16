// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.ProjectAccounting.IN.GraphExtensions.InReceiptEntryExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using PX.Objects.IN;
using PX.Objects.PM;
using System;

#nullable disable
namespace PX.Objects.CN.ProjectAccounting.IN.GraphExtensions;

public class InReceiptEntryExt : PXGraphExtension<INReceiptEntry>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.construction>();

  [PXMergeAttributes]
  [PXRestrictor(typeof (Where<PMTask.type, NotEqual<ProjectTaskType.revenue>>), "Task Type is not valid", new Type[] {})]
  [PXFormula(typeof (Validate<INTran.projectID>))]
  protected virtual void _(PX.Data.Events.CacheAttached<INTran.taskID> e)
  {
  }
}
