// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.ProformaTransactLineStatusAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.PM;

public class ProformaTransactLineStatusAttribute : PXStringListAttribute, IPXRowSelectedSubscriber
{
  public ProformaTransactLineStatusAttribute()
    : base(new string[0], new string[0])
  {
  }

  public void RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is PMProformaTransactLine row))
      return;
    KeyValuePair<List<string>, List<string>> validStatusOptions = ProformaTransactLineStatusAttribute.GetValidStatusOptions(row, row.Option);
    PXStringListAttribute.SetList<PMProformaTransactLine.option>(sender, (object) row, validStatusOptions.Key.ToArray(), validStatusOptions.Value.ToArray());
  }

  public static string GetDefaultProformaLineOption(
    PMProformaTransactLine row,
    string currentOption)
  {
    if (row == null)
      return (string) null;
    if (currentOption == "X" || currentOption == "N")
      return currentOption;
    Decimal? curyLineTotal = row.CuryLineTotal;
    Decimal? curyPrepaidAmount = row.CuryPrepaidAmount;
    Decimal? nullable1 = curyLineTotal.HasValue & curyPrepaidAmount.HasValue ? new Decimal?(curyLineTotal.GetValueOrDefault() + curyPrepaidAmount.GetValueOrDefault()) : new Decimal?();
    Decimal? nullable2 = row.CuryBillableAmount;
    if (nullable1.GetValueOrDefault() < nullable2.GetValueOrDefault() & nullable1.HasValue & nullable2.HasValue)
    {
      nullable2 = row.CuryLineTotal;
      Decimal num = 0M;
      if (nullable2.GetValueOrDefault() >= num & nullable2.HasValue)
        return "U";
    }
    return "N";
  }

  public static KeyValuePair<List<string>, List<string>> GetValidStatusOptions(
    PMProformaTransactLine line,
    string status)
  {
    KeyValuePair<List<string>, List<string>> validStatusOptions = new KeyValuePair<List<string>, List<string>>(new List<string>(), new List<string>());
    if (ProformaTransactLineStatusAttribute.GetDefaultProformaLineOption(line, status) == "U")
    {
      validStatusOptions.Key.Add("U");
      validStatusOptions.Value.Add(PXMessages.LocalizeNoPrefix("Hold Remainder"));
      validStatusOptions.Key.Add("C");
      validStatusOptions.Value.Add(PXMessages.LocalizeNoPrefix("Write Off Remainder"));
      validStatusOptions.Key.Add("X");
      validStatusOptions.Value.Add(PXMessages.LocalizeNoPrefix("Write Off"));
    }
    else
    {
      validStatusOptions.Key.Add("N");
      validStatusOptions.Value.Add(PXMessages.LocalizeNoPrefix("Bill"));
      validStatusOptions.Key.Add("X");
      validStatusOptions.Value.Add(PXMessages.LocalizeNoPrefix("Write Off"));
    }
    return validStatusOptions;
  }
}
