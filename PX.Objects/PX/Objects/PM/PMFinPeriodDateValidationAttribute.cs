// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMFinPeriodDateValidationAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using CommonServiceLocator;
using PX.Data;
using PX.Objects.GL.FinPeriods;
using PX.Objects.GL.FinPeriods.TableDefinition;
using System;

#nullable disable
namespace PX.Objects.PM;

/// <summary>
/// Attribute for date fields that verifies if the selected date belongs to a valid financial period.
/// It checks if the financial period exists, is active, and is not closed.
/// If any of these conditions are not met, it sets an appropriate error or warning on the field.
/// </summary>
/// <remarks>
/// This attribute should be used in conjunction with [PXDBDate] for database fields
/// or [PXDate] for non-database fields, and [PXUIField] for UI representation.
/// 
/// Example usage:
/// [PMFinPeriodDateValidation]
/// [PXDBDate]
/// [PXUIField(DisplayName = "Date")]
/// public virtual DateTime? Date { get; set; }
/// </remarks>
public class PMFinPeriodDateValidationAttribute : 
  PXEventSubscriberAttribute,
  IPXFieldVerifyingSubscriber
{
  public virtual void FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    if (!(e.NewValue is DateTime newValue))
      return;
    this.CheckDate(sender, e.Row, newValue);
  }

  protected virtual void CheckDate(PXCache cache, object row, DateTime date)
  {
    IFinPeriodRepository periodRepository = ((Func<PXGraph, IFinPeriodRepository>) ((IServiceProvider) ServiceLocator.Current).GetService(typeof (Func<PXGraph, IFinPeriodRepository>)))(cache.Graph);
    string shortDateString = date.ToShortDateString();
    if (periodRepository == null)
      return;
    try
    {
      FinPeriod finPeriodByDate = periodRepository.GetFinPeriodByDate(new DateTime?(date), PXAccess.GetParentOrganizationID(cache.Graph.Accessinfo.BranchID));
      if (finPeriodByDate == null)
        PXUIFieldAttribute.SetError(cache, row, this.FieldName, $"The financial period for the selected {shortDateString} date is not defined in the system. The date must belong to an existing financial period of the master calendar.");
      else if (finPeriodByDate.Status == "Inactive")
      {
        PXUIFieldAttribute.SetWarning(cache, row, this.FieldName, $"The {shortDateString} financial period is inactive in the {PXAccess.GetParentOrganization(cache.Graph.Accessinfo.BranchID).OrganizationCD} company.");
      }
      else
      {
        if (!(finPeriodByDate.Status == "Closed"))
          return;
        PXUIFieldAttribute.SetWarning(cache, row, this.FieldName, $"The {shortDateString} financial period is closed in the {PXAccess.GetParentOrganization(cache.Graph.Accessinfo.BranchID).OrganizationCD} company.");
      }
    }
    catch (PXException ex)
    {
      PXUIFieldAttribute.SetError(cache, row, this.FieldName, $"The financial period for the selected {shortDateString} date is not defined in the system. The date must belong to an existing financial period of the master calendar.");
    }
  }
}
