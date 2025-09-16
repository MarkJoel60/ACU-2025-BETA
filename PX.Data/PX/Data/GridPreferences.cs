// Decompiled with JetBrains decompiler
// Type: PX.Data.GridPreferences
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

#nullable disable
namespace PX.Data;

[Serializable]
public class GridPreferences : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  public const string DefaultUserName = "________________";
  private const string SlotKey = "GridPreferences";

  public static int GetDbSlotHash()
  {
    return PXDatabase.GetSlot<GridPreferences.Definition>(nameof (GridPreferences), typeof (GridPreferences)).GetHashCode();
  }

  public static GridPreferences.ColumnPref[] Get(string screenID, string userName, string viewName)
  {
    GridPreferences.Definition slot = PXDatabase.GetSlot<GridPreferences.Definition>(nameof (GridPreferences), typeof (GridPreferences));
    GridPreferences.ColumnPref[] columnPrefArray;
    return slot != null && slot.Preferences.TryGetValue(screenID + userName + viewName, out columnPrefArray) ? columnPrefArray : (GridPreferences.ColumnPref[]) null;
  }

  public static IEnumerable<string> GetUsers(string screenID)
  {
    GridPreferences.Definition slot = PXDatabase.GetSlot<GridPreferences.Definition>(nameof (GridPreferences), typeof (GridPreferences));
    List<string> source;
    return slot != null && slot.PreferencedUsersByScreen.TryGetValue(screenID, out source) ? source.Where<string>((Func<string, bool>) (userName => userName != "________________")) : Enumerable.Empty<string>();
  }

  public static IEnumerable<string> GetDefaultConfiguredScreens()
  {
    return (IEnumerable<string>) PXDatabase.GetSlot<GridPreferences.Definition>(nameof (GridPreferences), typeof (GridPreferences))?.DefaultConfiguredScreens ?? Enumerable.Empty<string>();
  }

  public static bool Set(
    string screenID,
    string userName,
    string viewName,
    GridPreferences.ColumnPref[] preferences)
  {
    byte[] numArray = PXDatabase.Serialize((object[]) preferences);
    if (!PXDatabase.Ensure<GridPreferences>(new PXDataFieldAssign[4]
    {
      new PXDataFieldAssign("ScreenID", PXDbType.VarChar, new int?(8), (object) screenID),
      new PXDataFieldAssign("UserName", PXDbType.NVarChar, new int?(64 /*0x40*/), (object) userName),
      new PXDataFieldAssign("ViewName", PXDbType.VarChar, new int?(1000), (object) viewName),
      new PXDataFieldAssign("Columns", PXDbType.VarBinary, new int?(numArray != null ? numArray.Length : 0), (object) numArray)
    }, new PXDataField[3]
    {
      (PXDataField) new PXDataFieldValue("ScreenID", PXDbType.VarChar, new int?(8), (object) screenID),
      (PXDataField) new PXDataFieldValue("UserName", PXDbType.NVarChar, new int?(64 /*0x40*/), (object) userName),
      (PXDataField) new PXDataFieldValue("ViewName", PXDbType.VarChar, new int?(1000), (object) viewName)
    }))
      return PXDatabase.Update<GridPreferences>((PXDataFieldParam) new PXDataFieldAssign("Columns", PXDbType.VarBinary, new int?(numArray != null ? numArray.Length : 0), (object) numArray), (PXDataFieldParam) new PXDataFieldRestrict("ScreenID", PXDbType.VarChar, new int?(8), (object) screenID), (PXDataFieldParam) new PXDataFieldRestrict("UserName", PXDbType.NVarChar, new int?(64 /*0x40*/), (object) userName), (PXDataFieldParam) new PXDataFieldRestrict("ViewName", PXDbType.VarChar, new int?(1000), (object) viewName));
    PXDatabase.ResetSlot<GridPreferences.Definition>(nameof (GridPreferences), typeof (GridPreferences));
    return true;
  }

  public static bool Reset(string screenID, string userName, string viewName)
  {
    bool flag = PXDatabase.Delete<GridPreferences>(new PXDataFieldRestrict("ScreenID", PXDbType.VarChar, new int?(8), (object) screenID), new PXDataFieldRestrict("UserName", PXDbType.NVarChar, new int?(64 /*0x40*/), (object) userName), new PXDataFieldRestrict("ViewName", PXDbType.VarChar, new int?(1000), (object) viewName));
    if (flag)
      PXDatabase.ResetSlot<GridPreferences.Definition>(nameof (GridPreferences), typeof (GridPreferences));
    return flag;
  }

  public static bool ResetAll(string screenID, string viewName)
  {
    bool flag = PXDatabase.Delete<GridPreferences>(new PXDataFieldRestrict("ScreenID", PXDbType.VarChar, new int?(8), (object) screenID), new PXDataFieldRestrict("UserName", PXDbType.NVarChar, new int?(64 /*0x40*/), (object) "________________", PXComp.NE), new PXDataFieldRestrict("ViewName", PXDbType.VarChar, new int?(1000), (object) viewName));
    if (flag)
      PXDatabase.ResetSlot<GridPreferences.Definition>(nameof (GridPreferences), typeof (GridPreferences));
    return flag;
  }

  public static bool ResetDefault(string screenID, string viewName)
  {
    return GridPreferences.Reset(screenID, "________________", viewName);
  }

  [Serializable]
  public class ColumnPref
  {
    public string DataField;
    public bool? Visible;
    public int Order;
    public int? Width;
    public int SortOrder;
    public bool? SkipTab;

    public ColumnPref(string dataField)
    {
      this.DataField = dataField;
      this.Visible = new bool?();
    }

    public ColumnPref(string dataField, bool? visible, short order, short? width)
      : this(dataField)
    {
      this.Visible = visible;
      this.Order = (int) order;
      short? nullable = width;
      this.Width = nullable.HasValue ? new int?((int) nullable.GetValueOrDefault()) : new int?();
    }
  }

  private sealed class Definition : IPrefetchable, IPXCompanyDependent
  {
    public Dictionary<string, GridPreferences.ColumnPref[]> Preferences = new Dictionary<string, GridPreferences.ColumnPref[]>((IEqualityComparer<string>) StringComparer.InvariantCultureIgnoreCase);
    public Dictionary<string, List<string>> PreferencedUsersByScreen = new Dictionary<string, List<string>>();
    public readonly HashSet<string> DefaultConfiguredScreens = new HashSet<string>();

    void IPrefetchable.Prefetch()
    {
      IFormatter deserializationFormatter = PXDatabase.CreateDeserializationFormatter();
      foreach (PXDataRecord pxDataRecord in PXDatabase.SelectMulti<GridPreferences>(new PXDataField("Columns"), new PXDataField("ScreenID"), new PXDataField("UserName"), new PXDataField("ViewName")))
      {
        object[] objArray = (object[]) null;
        try
        {
          objArray = PXDatabase.Deserialize(pxDataRecord.GetBytes(0), deserializationFormatter);
        }
        catch
        {
        }
        if (objArray != null && objArray.Length != 0)
        {
          GridPreferences.ColumnPref[] columnPrefArray = new GridPreferences.ColumnPref[objArray.Length];
          for (int index = 0; index < objArray.Length; ++index)
            columnPrefArray[index] = objArray[index] as GridPreferences.ColumnPref;
          string key = pxDataRecord.GetString(1);
          string str = pxDataRecord.GetString(2);
          this.Preferences[key + str + pxDataRecord.GetString(3)] = columnPrefArray;
          List<string> stringList;
          if (!this.PreferencedUsersByScreen.TryGetValue(key, out stringList))
          {
            stringList = new List<string>();
            this.PreferencedUsersByScreen.Add(key, stringList);
          }
          stringList.Add(str);
          if (str == "________________")
            this.DefaultConfiguredScreens.Add(key);
        }
      }
    }
  }
}
