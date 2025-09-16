// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.MaterialManagement.GraphExtensions.ItemAvailability.Allocated.FSServiceOrderItemAvailabilityAllocatedProjectExtension
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Objects.FS;
using PX.Objects.IN;
using System;

#nullable disable
namespace PX.Objects.PM.MaterialManagement.GraphExtensions.ItemAvailability.Allocated;

[PXProtectedAccess(null)]
public abstract class FSServiceOrderItemAvailabilityAllocatedProjectExtension : 
  ItemAvailabilityAllocatedProjectExtension<ServiceOrderEntry, FSServiceOrderItemAvailabilityExtension, FSServiceOrderItemAvailabilityAllocatedExtension, FSServiceOrderItemAvailabilityProjectExtension, FSSODet, FSSODetSplit>
{
  public static bool IsActive()
  {
    return ItemAvailabilityAllocatedProjectExtension<ServiceOrderEntry, FSServiceOrderItemAvailabilityExtension, FSServiceOrderItemAvailabilityAllocatedExtension, FSServiceOrderItemAvailabilityProjectExtension, FSSODet, FSSODetSplit>.UseProjectAvailability;
  }

  protected override string GetStatusWithAllocatedProject(FSSODet line)
  {
    string allocatedProject = (string) null;
    bool excludeCurrent = line == null || !line.Completed.GetValueOrDefault();
    PMProject project;
    if (ProjectDefaultAttribute.IsProject((PXGraph) ((PXGraphExtension<ServiceOrderEntry>) this).Base, line.ProjectID, out project) && project.AccountingMode != "L")
    {
      IStatus availability = this.ItemAvailBase.FetchWithLineUOM(line, excludeCurrent, new int?(0));
      if (availability != null)
      {
        IStatus status = this.ItemAvailProjExt.FetchWithLineUOMProject(line, excludeCurrent, line.CostCenterID);
        if (status != null)
        {
          Decimal? allocated = new Decimal?(this.GetAllocatedQty(line));
          allocatedProject = this.FormatStatusAllocatedProject(availability, status, allocated, line.UOM);
          this.Check((ILSMaster) line, status);
        }
      }
    }
    return allocatedProject;
  }

  private string FormatStatusAllocatedProject(
    IStatus availability,
    IStatus availabilityProject,
    Decimal? allocated,
    string uom)
  {
    object[] objArray = new object[8]
    {
      (object) uom,
      (object) this.FormatQty(availabilityProject.QtyOnHand),
      (object) this.FormatQty(availabilityProject.QtyAvail),
      (object) this.FormatQty(availabilityProject.QtyHardAvail),
      (object) this.FormatQty(allocated),
      null,
      null,
      null
    };
    Decimal? nullable1 = availabilityProject.QtyOnHand;
    Decimal? nullable2 = availability.QtyOnHand;
    objArray[5] = (object) this.FormatQty(nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?());
    nullable2 = availabilityProject.QtyAvail;
    nullable1 = availability.QtyAvail;
    objArray[6] = (object) this.FormatQty(nullable2.HasValue & nullable1.HasValue ? new Decimal?(nullable2.GetValueOrDefault() + nullable1.GetValueOrDefault()) : new Decimal?());
    nullable1 = availabilityProject.QtyHardAvail;
    nullable2 = availability.QtyHardAvail;
    objArray[7] = (object) this.FormatQty(nullable1.HasValue & nullable2.HasValue ? new Decimal?(nullable1.GetValueOrDefault() + nullable2.GetValueOrDefault()) : new Decimal?());
    return PXMessages.LocalizeFormatNoPrefixNLA("Project/Total: On Hand {1}/{5} {0}, Available {2}/{6} {0}, Available for Shipping {3}/{7} {0}, Allocated {4} {0}", objArray);
  }

  /// Uses <see cref="M:PX.Objects.FS.FSServiceOrderItemAvailabilityAllocatedExtension.GetAllocatedQty(PX.Objects.FS.FSSODet)" />
  [PXProtectedAccess(typeof (FSServiceOrderItemAvailabilityAllocatedExtension))]
  protected abstract Decimal GetAllocatedQty(FSSODet line);

  /// Uses <see cref="M:PX.Objects.IN.GraphExtensions.ItemAvailabilityExtension`3.Check(PX.Objects.IN.ILSMaster,PX.Objects.IN.IStatus)" />
  [PXProtectedAccess(typeof (FSServiceOrderItemAvailabilityExtension))]
  protected abstract void Check(ILSMaster row, IStatus availability);

  /// Uses <see cref="M:PX.Objects.IN.GraphExtensions.ItemAvailabilityExtension`3.FormatQty(System.Nullable{System.Decimal})" />
  [PXProtectedAccess(typeof (FSServiceOrderItemAvailabilityExtension))]
  protected abstract string FormatQty(Decimal? value);
}
