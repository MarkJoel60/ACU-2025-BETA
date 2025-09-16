// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMActiveLaborItemAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Linq;

#nullable disable
namespace PX.Objects.PM;

[LaborItemIsInactive]
public class PMActiveLaborItemAttribute(Type project, Type earningType, Type employeeSearch) : 
  PMLaborItemAttribute(project, earningType, employeeSearch)
{
  public static void VerifyLaborItem<Field>(PXCache sender, object record) where Field : IBqlField
  {
    object obj = sender.GetValue<Field>(record);
    sender.GetAttributes<Field>().OfType<PMActiveLaborItemAttribute>().FirstOrDefault<PMActiveLaborItemAttribute>()?.VerifyItemStatus<Field>(sender, new PXFieldVerifyingEventArgs(record, obj, true));
  }

  private void VerifyItemStatus<Field>(PXCache sender, PXFieldVerifyingEventArgs e) where Field : IBqlField
  {
    try
    {
      ((PXAggregateAttribute) this).GetAttribute<LaborItemIsInactiveAttribute>()?.Verify(sender, e, true);
    }
    catch (PXSetPropertyException ex)
    {
      PXFieldState stateExt = (PXFieldState) sender.GetStateExt<Field>(e.Row);
      PXUIFieldAttribute.SetError<Field>(sender, e.Row, ((Exception) ex).Message, (string) stateExt?.Value);
      throw;
    }
  }
}
