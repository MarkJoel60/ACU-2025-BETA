// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EPEarningTypesSetup
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.PM;

#nullable disable
namespace PX.Objects.EP;

public class EPEarningTypesSetup : PXGraph<EPEarningTypesSetup>
{
  public PXSelect<EPEarningType> EarningTypes;
  public PXSetup<EPSetup> Setup;
  public PXSavePerRow<EPEarningType> Save;
  public PXCancel<EPEarningType> Cancel;
  public PXAction<EPEarningType> RedirectToPayrollScreen;

  /// <summary>
  /// There is a payroll module replacement for this screen. So if the payroll module
  /// is enabled/installed, then we should redirect users to the payroll version of this page.
  /// </summary>
  protected virtual void redirectToPayrollScreen()
  {
    if (!PXAccess.FeatureInstalled<FeaturesSet.payrollModule>())
      return;
    PXRedirectHelper.TryRedirect(PXGraph.CreateInstance(GraphHelper.GetType("PX.Objects.PR.PREarningTypeMaint")), (PXRedirectHelper.WindowMode) 0);
  }

  protected void EPEarningType_RowDeleting(PXCache sender, PXRowDeletingEventArgs e)
  {
    EPEarningType row = (EPEarningType) e.Row;
    if (row == null)
      return;
    if (PXResultset<EPSetup>.op_Implicit(PXSelectBase<EPSetup, PXSelect<EPSetup, Where<EPSetup.regularHoursType, Equal<Required<EPEarningType.typeCD>>, Or<EPSetup.holidaysType, Equal<Required<EPEarningType.typeCD>>, Or<EPSetup.vacationsType, Equal<Required<EPEarningType.typeCD>>>>>>.Config>.Select((PXGraph) this, new object[3]
    {
      (object) row.TypeCD,
      (object) row.TypeCD,
      (object) row.TypeCD
    })) != null)
      throw new PXException("Cannot delete because it is already in use.");
    if (PXResultset<CRCaseClassLaborMatrix>.op_Implicit(PXSelectBase<CRCaseClassLaborMatrix, PXSelect<CRCaseClassLaborMatrix, Where<CRCaseClassLaborMatrix.earningType, Equal<Required<EPEarningType.typeCD>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) row.TypeCD
    })) != null)
      throw new PXException("Cannot delete because it is already in use.");
    if (PXResultset<EPContractRate>.op_Implicit(PXSelectBase<EPContractRate, PXSelect<EPContractRate, Where<EPContractRate.earningType, Equal<Required<EPEarningType.typeCD>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) row.TypeCD
    })) != null)
      throw new PXException("Cannot delete because it is already in use.");
    if (PXResultset<EPEmployeeClassLaborMatrix>.op_Implicit(PXSelectBase<EPEmployeeClassLaborMatrix, PXSelect<EPEmployeeClassLaborMatrix, Where<EPEmployeeClassLaborMatrix.earningType, Equal<Required<EPEarningType.typeCD>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) row.TypeCD
    })) != null)
      throw new PXException("Cannot delete because it is already in use.");
    if (PXResultset<PMTimeActivity>.op_Implicit(PXSelectBase<PMTimeActivity, PXSelect<PMTimeActivity, Where<PMTimeActivity.earningTypeID, Equal<Required<EPEarningType.typeCD>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) row.TypeCD
    })) != null)
      throw new PXException("Cannot delete because it is already in use.");
    if (PXResultset<PMTran>.op_Implicit(PXSelectBase<PMTran, PXSelect<PMTran, Where<PMTran.earningType, Equal<Required<EPEarningType.typeCD>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) row.TypeCD
    })) != null)
      throw new PXException("Cannot delete because it is already in use.");
  }

  protected virtual void EPEarningType_IsActive_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is EPEarningType row))
      return;
    bool? isActive = row.IsActive;
    bool flag = false;
    if (!(isActive.GetValueOrDefault() == flag & isActive.HasValue))
      return;
    if (((PXSelectBase<EPSetup>) this.Setup).Current.RegularHoursType == row.TypeCD)
      throw new PXException($"Earning type {row.TypeCD} cannot be deactivated because it is used in the {PXUIFieldAttribute.GetDisplayName<EPSetup.regularHoursType>(((PXSelectBase) this.Setup).Cache)} setting on the Time & Expenses Preferences form EP101000");
    if (((PXSelectBase<EPSetup>) this.Setup).Current.HolidaysType == row.TypeCD)
      throw new PXException($"Earning type {row.TypeCD} cannot be deactivated because it is used in the {PXUIFieldAttribute.GetDisplayName<EPSetup.holidaysType>(((PXSelectBase) this.Setup).Cache)} setting on the Time & Expenses Preferences form EP101000");
    if (((PXSelectBase<EPSetup>) this.Setup).Current.VacationsType == row.TypeCD)
      throw new PXException($"Earning type {row.TypeCD} cannot be deactivated because it is used in the {PXUIFieldAttribute.GetDisplayName<EPSetup.vacationsType>(((PXSelectBase) this.Setup).Cache)} setting on the Time & Expenses Preferences form EP101000");
  }
}
