// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INUnitMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.GL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

#nullable disable
namespace PX.Objects.IN;

[TableAndChartDashboardType]
public class INUnitMaint : PXGraph<INUnitMaint>
{
  public PXSelect<INUnit, Where<INUnit.unitType, Equal<INUnitType.global>>> Unit;
  public PXSavePerRow<INUnit> Save;
  public PXCancel<INUnit> Cancel;

  protected virtual void INUnit_FromUnit_FieldUpdating(PXCache sender, PXFieldUpdatingEventArgs e)
  {
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void INUnit_ToUnit_FieldUpdating(PXCache sender, PXFieldUpdatingEventArgs e)
  {
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void INUnit_RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    INUnit row = (INUnit) e.Row;
    if (row == null || e.Operation == 3 || !(row.FromUnit == row.ToUnit))
      return;
    Decimal? unitRate = row.UnitRate;
    Decimal num = 1M;
    if (!(unitRate.GetValueOrDefault() == num & unitRate.HasValue))
      throw new PXRowPersistingException(typeof (INUnit.unitRate).Name, (object) null, "The changes cannot be saved because the conversion factor for converting unit '{0}' to unit '{0}' differs from 1.", new object[1]
      {
        (object) row.FromUnit
      });
  }

  protected virtual void INUnit_UnitRate_FieldVerifying(PXCache cache, PXFieldVerifyingEventArgs e)
  {
    Decimal? newValue = (Decimal?) e.NewValue;
    Decimal num = 0M;
    if (newValue.GetValueOrDefault() <= num & newValue.HasValue)
      throw new PXSetPropertyException("Incorrect value. The value to be entered must be greater than {0}.", new object[1]
      {
        (object) "0"
      });
  }

  public virtual IEnumerable ExecuteSelect(
    string viewName,
    object[] parameters,
    object[] searches,
    string[] sortcolumns,
    bool[] descendings,
    PXFilterRow[] filters,
    ref int startRow,
    int maximumRows,
    ref int totalRows)
  {
    if (viewName == "Unit")
    {
      object[] source = searches;
      if ((source != null ? (((IEnumerable<object>) source).Any<object>() ? 1 : 0) : 0) != 0 && removeFromSearches("UnitType", (Func<object, bool>) (v =>
      {
        short? nullable1 = v as short?;
        int? nullable2 = nullable1.HasValue ? new int?((int) nullable1.GetValueOrDefault()) : new int?();
        int num = 1;
        return nullable2.GetValueOrDefault() == num & nullable2.HasValue;
      })))
      {
        removeFromSearches("InventoryID");
        removeFromSearches("ItemClassID");
        removeFromSearches("ToUnit");
      }
    }
    return ((PXGraph) this).ExecuteSelect(viewName, parameters, searches, sortcolumns, descendings, filters, ref startRow, maximumRows, ref totalRows);

    bool removeFromSearches(string field, Func<object, bool> condition = null)
    {
      int index = Array.IndexOf<string>(sortcolumns, field);
      if (index < 0 || condition != null && !condition(searches[index]))
        return false;
      searches[index] = (object) null;
      return true;
    }
  }
}
