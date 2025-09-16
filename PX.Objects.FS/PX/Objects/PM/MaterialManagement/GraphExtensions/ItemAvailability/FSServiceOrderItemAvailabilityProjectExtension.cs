// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.MaterialManagement.GraphExtensions.ItemAvailability.FSServiceOrderItemAvailabilityProjectExtension
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Objects.CS;
using PX.Objects.FS;
using PX.Objects.IN;

#nullable disable
namespace PX.Objects.PM.MaterialManagement.GraphExtensions.ItemAvailability;

[PXProtectedAccess(typeof (FSServiceOrderItemAvailabilityExtension))]
public abstract class FSServiceOrderItemAvailabilityProjectExtension : 
  ItemAvailabilityProjectExtension<ServiceOrderEntry, FSServiceOrderItemAvailabilityExtension, FSSODet, FSSODetSplit>
{
  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.materialManagement>();

  protected override string GetStatusProject(FSSODet line)
  {
    string statusProject = (string) null;
    bool excludeCurrent = line == null || !line.Completed.GetValueOrDefault();
    PMProject project;
    if (ProjectDefaultAttribute.IsProject((PXGraph) ((PXGraphExtension<ServiceOrderEntry>) this).Base, line.ProjectID, out project) && project.AccountingMode != "L")
    {
      IStatus availability = this.FetchWithLineUOM(line, excludeCurrent, line.CostCenterID);
      if (availability != null)
      {
        IStatus availabilityProject = this.FetchWithLineUOMProject(line, excludeCurrent, line.CostCenterID);
        if (availabilityProject != null)
        {
          statusProject = this.FormatStatusProject(availability, availabilityProject, line.UOM);
          this.Check((ILSMaster) line, line.CostCenterID);
        }
      }
    }
    return statusProject;
  }

  private string FormatStatusProject(IStatus availability, IStatus availabilityProject, string uom)
  {
    return PXMessages.LocalizeFormatNoPrefixNLA("Project/Total: On Hand {1}/{4} {0}, Available {2}/{5} {0}, Available for Shipping {3}/{6} {0}", new object[7]
    {
      (object) uom,
      (object) this.FormatQty(availabilityProject.QtyOnHand),
      (object) this.FormatQty(availabilityProject.QtyAvail),
      (object) this.FormatQty(availabilityProject.QtyHardAvail),
      (object) this.FormatQty(availability.QtyOnHand),
      (object) this.FormatQty(availability.QtyAvail),
      (object) this.FormatQty(availability.QtyHardAvail)
    });
  }
}
