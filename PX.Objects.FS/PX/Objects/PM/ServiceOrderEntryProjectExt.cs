// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.ServiceOrderEntryProjectExt
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Objects.CS;
using PX.Objects.FS;

#nullable disable
namespace PX.Objects.PM;

public class ServiceOrderEntryProjectExt : PXGraphExtension<ServiceOrderEntry>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.projectAccounting>();

  [PXOverride]
  public virtual void Persist(
    ServiceOrderEntryProjectExt.PersistDelegate baseMethod)
  {
    FSServiceOrder current = ((PXSelectBase<FSServiceOrder>) this.Base.ServiceOrderRecords).Current;
    if (current != null && current.ReopenActionRunning.GetValueOrDefault() || current != null && current.UnCloseActionRunning.GetValueOrDefault())
    {
      PMProject pmProject = PMProject.PK.Find((PXGraph) this.Base, current.ProjectID);
      if (pmProject?.Status == "L")
        throw new PXException(PXMessages.LocalizeFormat("The {0} project is closed.", new object[1]
        {
          (object) pmProject.ContractCD.Trim()
        }));
    }
    baseMethod();
  }

  public delegate void PersistDelegate();
}
