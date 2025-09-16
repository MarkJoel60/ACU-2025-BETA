// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.EquipmentHelper
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Objects.EP;
using PX.Objects.FA;
using System;
using System.Collections;
using System.Linq;

#nullable disable
namespace PX.Objects.FS;

public static class EquipmentHelper
{
  /// <summary>
  /// Set default values in FSEquipment from the Fixed Asset specified.
  /// </summary>
  public static void SetDefaultValuesFromFixedAsset(
    PXCache cacheFSEquipment,
    FSEquipment fsEquipmentRow,
    int? fixedAssetID)
  {
    if (!fixedAssetID.HasValue)
      return;
    FADetails faDetails = PXResultset<FADetails>.op_Implicit(PXSelectBase<FADetails, PXSelect<FADetails, Where<FADetails.assetID, Equal<Required<FADetails.assetID>>>>.Config>.Select(cacheFSEquipment.Graph, new object[1]
    {
      (object) fixedAssetID
    }));
    if (faDetails == null)
      return;
    cacheFSEquipment.SetValueExt<FSEquipment.purchDate>((object) fsEquipmentRow, (object) faDetails.ReceiptDate);
    cacheFSEquipment.SetValueExt<FSEquipment.registeredDate>((object) fsEquipmentRow, (object) faDetails.DepreciateFromDate);
    cacheFSEquipment.SetValueExt<FSEquipment.purchAmount>((object) fsEquipmentRow, (object) faDetails.AcquisitionCost);
    cacheFSEquipment.SetValueExt<FSEquipment.purchPONumber>((object) fsEquipmentRow, (object) faDetails.PONumber);
    cacheFSEquipment.SetValueExt<FSEquipment.propertyType>((object) fsEquipmentRow, (object) faDetails.PropertyType);
    cacheFSEquipment.SetValueExt<FSEquipment.serialNumber>((object) fsEquipmentRow, (object) faDetails.SerialNumber);
  }

  /// <summary>
  /// Update a FSEquipment record with the values in the EPEquipment record.
  /// </summary>
  /// <param name="cacheFSEquipment">The cache of the FSEquipment record.</param>
  /// <param name="fsEquipmentRow">The FSEquipment record.</param>
  /// <param name="cacheEPEquipment">The cache of the EPEquipment record.</param>
  /// <param name="epEquipmentRow">The EPEquipment record.</param>
  /// <returns>Returns true if some value changes, otherwise it returns false.</returns>
  public static bool UpdateFSEquipmentWithEPEquipment(
    PXCache cacheFSEquipment,
    FSEquipment fsEquipmentRow,
    PXCache cacheEPEquipment,
    EPEquipment epEquipmentRow)
  {
    return EquipmentHelper.CopyEPEquipmentFields(cacheFSEquipment, (IBqlTable) fsEquipmentRow, cacheEPEquipment, (IBqlTable) epEquipmentRow);
  }

  /// <summary>
  /// Update a EPEquipment record with the values in the FSEquipment record.
  /// </summary>
  /// <param name="cacheEPEquipment">The cache of the EPEquipment record.</param>
  /// <param name="epEquipmentRow">The EPEquipment record.</param>
  /// <param name="cacheFSEquipment">The cache of the FSEquipment record.</param>
  /// <param name="fsEquipmentRow">The FSEquipment record.</param>
  /// <returns>Returns true if some value changes, otherwise it returns false.</returns>
  public static bool UpdateEPEquipmentWithFSEquipment(
    PXCache cacheEPEquipment,
    EPEquipment epEquipmentRow,
    PXCache cacheFSEquipment,
    FSEquipment fsEquipmentRow)
  {
    return EquipmentHelper.CopyEPEquipmentFields(cacheEPEquipment, (IBqlTable) epEquipmentRow, cacheFSEquipment, (IBqlTable) fsEquipmentRow);
  }

  /// <summary>Update a record with the values in another one.</summary>
  /// <param name="cacheTo">The cache of the record to be updated.</param>
  /// <param name="rowTo">The record to be updated.</param>
  /// <param name="cacheFrom">The cache of the record to be read.</param>
  /// <param name="rowFrom">The record to be read.</param>
  /// <returns>Returns true if some value changes, otherwise it returns false.</returns>
  private static bool CopyEPEquipmentFields(
    PXCache cacheTo,
    IBqlTable rowTo,
    PXCache cacheFrom,
    IBqlTable rowFrom)
  {
    bool flag1 = false;
    switch (rowFrom)
    {
      case EPEquipment epEquipment1 when rowTo is FSEquipment fsEquipment1:
        string str1 = epEquipment1.IsActive.GetValueOrDefault() ? "A" : "S";
        if (fsEquipment1.Status != str1)
        {
          cacheTo.SetValueExt<FSEquipment.status>((object) fsEquipment1, (object) str1);
          flag1 = true;
          break;
        }
        break;
      case FSEquipment fsEquipment2 when rowTo is EPEquipment epEquipment2:
        bool flag2 = fsEquipment2.Status == "A";
        bool? isActive = epEquipment2.IsActive;
        bool flag3 = flag2;
        if (!(isActive.GetValueOrDefault() == flag3 & isActive.HasValue))
        {
          cacheTo.SetValueExt<EPEquipment.isActive>((object) epEquipment2, (object) flag2);
          flag1 = true;
          break;
        }
        break;
    }
    string str2 = typeof (FSEquipment.descr).Name;
    string str3 = typeof (EPEquipment.description).Name;
    if (rowTo is EPEquipment)
    {
      string str4 = str2;
      str2 = str3;
      str3 = str4;
    }
    string objA = (string) cacheFrom.GetValue((object) rowFrom, str3);
    if (!object.Equals((object) objA, cacheTo.GetValue((object) rowTo, str2)))
    {
      cacheTo.SetValueExt((object) rowTo, str2, (object) objA);
      flag1 = true;
    }
    return flag1;
  }

  public static bool CheckReplaceComponentLines<TPartLine, TComponentLineRef>(
    PXCache cache,
    PXResultset<TPartLine> rows,
    IFSSODetBase currentRow)
    where TPartLine : class, IBqlTable, IFSSODetBase, new()
    where TComponentLineRef : IBqlField
  {
    if (currentRow == null || currentRow.EquipmentAction != "RC")
      return true;
    int? nullable1 = currentRow.SMEquipmentID;
    if (nullable1.HasValue)
    {
      nullable1 = currentRow.EquipmentLineRef;
      if (nullable1.HasValue)
      {
        bool flag = true;
        foreach (TPartLine partLine in GraphHelper.RowCast<TPartLine>((IEnumerable) rows).Where<TPartLine>((Func<TPartLine, bool>) (x => x.IsInventoryItem)))
        {
          nullable1 = partLine.LineID;
          int? nullable2 = currentRow.LineID;
          if (!(nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue) && partLine.EquipmentAction == currentRow.EquipmentAction)
          {
            nullable2 = partLine.SMEquipmentID;
            nullable1 = currentRow.SMEquipmentID;
            if (nullable2.GetValueOrDefault() == nullable1.GetValueOrDefault() & nullable2.HasValue == nullable1.HasValue)
            {
              nullable1 = partLine.EquipmentLineRef;
              nullable2 = currentRow.EquipmentLineRef;
              if (nullable1.GetValueOrDefault() == nullable2.GetValueOrDefault() & nullable1.HasValue == nullable2.HasValue)
              {
                string field = (string) PXSelectorAttribute.GetField(cache, (object) currentRow, typeof (TComponentLineRef).Name, (object) currentRow.EquipmentLineRef, typeof (FSEquipmentComponent.lineRef).Name);
                cache.RaiseExceptionHandling<TComponentLineRef>((object) currentRow, (object) field, (Exception) new PXSetPropertyException("The selected component has already been selected for replacement.", (PXErrorLevel) 4));
                flag = false;
              }
            }
          }
        }
        return flag;
      }
    }
    return true;
  }
}
