// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.ExternalControlsHelper
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.FS;

public static class ExternalControlsHelper
{
  public static bool PressSave(
    this PXGraph graph,
    ExternalControls.DispatchBoardAppointmentMessages messages)
  {
    bool flag1 = true;
    bool flag2 = false;
    string str = (string) null;
    try
    {
      graph.GetSaveAction().Press();
    }
    catch (Exception ex)
    {
      flag2 = true;
      str = ex.Message;
      if (graph.GetType() == typeof (AppointmentEntry))
      {
        if (str.IndexOf("#106") > 0)
          str = "The appointment cannot be modified because its associated service order has been either closed or canceled.";
      }
    }
    foreach (PXCache cach in graph.Caches.Caches)
    {
      if (ExternalControlsHelper.GetRowMessages(cach, cach.Current, messages) > 0)
        flag1 = false;
      foreach (object row in cach.Deleted)
      {
        if (row != cach.Current && ExternalControlsHelper.GetRowMessages(cach, row, messages) > 0)
          flag1 = false;
      }
      foreach (object row in cach.Inserted)
      {
        if (row != cach.Current && ExternalControlsHelper.GetRowMessages(cach, row, messages) > 0)
          flag1 = false;
      }
      foreach (object row in cach.Updated)
      {
        if (row != cach.Current && ExternalControlsHelper.GetRowMessages(cach, row, messages) > 0)
          flag1 = false;
      }
    }
    if (flag1 && flag2)
    {
      messages.ErrorMessages.Add(str);
      flag1 = false;
    }
    return flag1;
  }

  public static bool PressDelete(
    this PXGraph graph,
    ExternalControls.DispatchBoardAppointmentMessages messages)
  {
    bool flag1 = true;
    bool flag2 = false;
    string str = (string) null;
    try
    {
      graph.GetDeleteAction().Press();
    }
    catch (Exception ex)
    {
      flag2 = true;
      str = ex.Message;
    }
    foreach (PXCache cach in graph.Caches.Caches)
    {
      if (ExternalControlsHelper.GetRowMessages(cach, cach.Current, messages) > 0)
        flag1 = false;
      foreach (object row in cach.Deleted)
      {
        if (row != cach.Current && ExternalControlsHelper.GetRowMessages(cach, row, messages) > 0)
          flag1 = false;
      }
      foreach (object row in cach.Inserted)
      {
        if (row != cach.Current && ExternalControlsHelper.GetRowMessages(cach, row, messages) > 0)
          flag1 = false;
      }
      foreach (object row in cach.Updated)
      {
        if (row != cach.Current && ExternalControlsHelper.GetRowMessages(cach, row, messages) > 0)
          flag1 = false;
      }
    }
    if (flag1 && flag2)
    {
      messages.ErrorMessages.Add(str);
      flag1 = false;
    }
    return flag1;
  }

  private static int GetRowMessages(
    PXCache cache,
    object row,
    ExternalControls.DispatchBoardAppointmentMessages messages)
  {
    return MessageHelper.GetRowMessages(cache, row, messages.ErrorMessages, messages.WarningMessages, true);
  }

  public static string GetLongAddressText(FSAddress row)
  {
    string str = row.AddressLine1?.Trim();
    if (!string.IsNullOrEmpty(row.AddressLine2))
    {
      if (!string.IsNullOrEmpty(str))
        str += " ";
      str += row.AddressLine2.Trim();
    }
    return $"{str}, {row.State}, {row.City} {row.PostalCode}, {row.CountryID}";
  }

  public static string GetShortAddressText(FSAddress row)
  {
    string shortAddressText = row.AddressLine1?.Trim();
    if (!string.IsNullOrEmpty(row.AddressLine2))
    {
      if (!string.IsNullOrEmpty(shortAddressText))
        shortAddressText += " ";
      shortAddressText += row.AddressLine2.Trim();
    }
    return shortAddressText;
  }
}
