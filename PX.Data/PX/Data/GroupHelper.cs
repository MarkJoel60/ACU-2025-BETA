// Decompiled with JetBrains decompiler
// Type: PX.Data.GroupHelper
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.SM;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Compilation;

#nullable disable
namespace PX.Data;

/// <exclude />
public static class GroupHelper
{
  public const string FieldName = "GroupMask";

  public static object GetReferencedValue(
    PXCache cache,
    object data,
    string field,
    object value,
    bool forceUnattended)
  {
    System.Type referencedType = GroupHelper.GetReferencedType(cache, field);
    return !forceUnattended && typeof (Users).IsAssignableFrom(referencedType) && HttpContext.Current == null && !cache.Graph.IsImportFromExcel ? GroupHelper.Definitions.fullaccess.Clone() : PXSelectorAttribute.GetField(cache, data, field, value, "GroupMask");
  }

  public static System.Type GetReferencedType(PXCache cache, string field)
  {
    return PXSelectorAttribute.GetItemType(cache, field);
  }

  private static GroupHelper.Definition Definitions
  {
    get
    {
      GroupHelper.Definition definitions = PXContext.GetSlot<GroupHelper.Definition>();
      if (definitions == null)
        definitions = PXContext.SetSlot<GroupHelper.Definition>(PXDatabase.GetSlot<GroupHelper.Definition>("Definition", typeof (RelationGroup), typeof (Neighbour)));
      return definitions;
    }
  }

  public static int Count => GroupHelper.Definitions.Count;

  public static void Clear()
  {
    PXDatabase.ResetSlot<GroupHelper.Definition>("Definition", typeof (RelationGroup), typeof (Neighbour));
    PXContext.SetSlot<GroupHelper.Definition>((GroupHelper.Definition) null);
  }

  public static GroupHelper.ParamsPair[] GetParams(System.Type restrition, System.Type verified, byte[] mask)
  {
    GroupHelper.ParamsPair[] paramsPairArray1 = new GroupHelper.ParamsPair[(GroupHelper.Definitions.Count + 31 /*0x1F*/) / 32 /*0x20*/];
    int[] numArray = new int[(GroupHelper.Definitions.Count + 31 /*0x1F*/) / 32 /*0x20*/];
    for (int index = 0; index < numArray.Length; ++index)
    {
      int num = 0;
      if (mask != null)
      {
        if (index * 4 < mask.Length)
          num = (int) mask[index * 4] << 8;
        num = (num + (index * 4 + 1 < mask.Length ? (int) mask[index * 4 + 1] : 0) << 8) + (index * 4 + 2 < mask.Length ? (int) mask[index * 4 + 2] : 0) << 8;
        if (index * 4 + 3 < mask.Length)
          num += (int) mask[index * 4 + 3];
      }
      numArray[index] = num;
    }
    if (restrition == (System.Type) null)
    {
      for (int index = 0; index < paramsPairArray1.Length; ++index)
        paramsPairArray1[index] = new GroupHelper.ParamsPair(-1, 0);
      return paramsPairArray1;
    }
    if (restrition == typeof (RelationGroup) || typeof (RelationGroup).IsAssignableFrom(restrition))
    {
      bool flag = false;
      for (int index = 0; index < paramsPairArray1.Length; ++index)
      {
        paramsPairArray1[index] = new GroupHelper.ParamsPair(index >= numArray.Length || numArray[index] == 0 ? 0 : -1, index < numArray.Length ? numArray[index] : 0);
        if (!flag && index < numArray.Length)
          flag = numArray[index] != 0;
      }
      if (!flag)
      {
        for (int index = 0; index < paramsPairArray1.Length; ++index)
          paramsPairArray1[index] = new GroupHelper.ParamsPair(-1, 0);
      }
      return paramsPairArray1;
    }
    Dictionary<System.Type, GroupHelper.ParamsFour[]> dictionary;
    if (verified != (System.Type) null && GroupHelper.Definitions.cross.TryGetValue(restrition, out dictionary))
    {
      GroupHelper.ParamsFour[] paramsFourArray = (GroupHelper.ParamsFour[]) null;
      bool flag = false;
      for (; verified != typeof (object); verified = verified.BaseType)
      {
        if (dictionary.TryGetValue(verified, out paramsFourArray))
        {
          flag = true;
          break;
        }
      }
      if (flag)
      {
        for (int index = 0; index < paramsPairArray1.Length; ++index)
        {
          int num1 = 0;
          if (index < numArray.Length)
            num1 = numArray[index];
          int num2 = 0;
          int num3 = 0;
          int num4 = 0;
          int num5 = 0;
          if (index < paramsFourArray.Length)
          {
            num2 = paramsFourArray[index].First;
            num3 = paramsFourArray[index].Second;
            num4 = paramsFourArray[index].Third;
            num5 = paramsFourArray[index].Fourth;
          }
          paramsPairArray1[index] = new GroupHelper.ParamsPair(~num1 & num2 | num1 & num3 | num4 | num5, num1 & num4 | ~num1 & num5);
        }
        return paramsPairArray1;
      }
    }
    GroupHelper.ParamsPair[] paramsPairArray2 = new GroupHelper.ParamsPair[numArray.Length];
    for (int index = 0; index < paramsPairArray2.Length; ++index)
      paramsPairArray2[index] = new GroupHelper.ParamsPair(0, -1);
    return paramsPairArray2;
  }

  public static bool IsRestricted(System.Type restrition, System.Type verified)
  {
    Dictionary<System.Type, GroupHelper.ParamsFour[]> dictionary;
    if (restrition != (System.Type) null && verified != (System.Type) null && verified != restrition && !verified.IsSubclassOf(restrition) && GroupHelper.Definitions.cross.TryGetValue(restrition, out dictionary))
    {
      for (; verified != typeof (object); verified = verified.BaseType)
      {
        if (dictionary.ContainsKey(verified))
          return true;
      }
    }
    return false;
  }

  public static System.Type FindRestricted(System.Type restriction, string verified)
  {
    Dictionary<System.Type, GroupHelper.ParamsFour[]> dictionary;
    if (restriction != (System.Type) null && !string.IsNullOrEmpty(verified) && GroupHelper.Definitions.cross.TryGetValue(restriction, out dictionary))
    {
      foreach (System.Type key in dictionary.Keys)
      {
        if (string.Equals(key.Name, verified, StringComparison.OrdinalIgnoreCase))
          return key;
      }
    }
    return (System.Type) null;
  }

  public static bool IsAccessibleToUser(PXCache sender, object data)
  {
    return GroupHelper.IsAccessibleToUser(sender, data, sender.Graph.Accessinfo.UserName, sender.Graph._ForceUnattended);
  }

  public static bool IsAccessibleToUser(
    PXCache sender,
    object data,
    string userName,
    bool forceUnattended)
  {
    byte[] mask = forceUnattended || HttpContext.Current != null || sender.Graph.IsImportFromExcel ? PXSelectorAttribute.GetField(sender.Graph.Caches[typeof (AccessInfo)], (object) sender.Graph.Accessinfo, nameof (userName), (object) userName, "GroupMask") as byte[] : (byte[]) GroupHelper.Definitions.fullaccess.Clone();
    if (!(PXFieldState.UnwrapValue(sender.GetValueExt(data, "GroupMask")) is byte[] numArray))
      return true;
    if (mask == null)
      return false;
    int num1 = 0;
    bool flag1 = true;
    bool flag2 = false;
    foreach (GroupHelper.ParamsPair paramsPair in GroupHelper.GetParams(typeof (Users), sender.GetItemType(), mask))
    {
      int num2 = 0;
      if (num1 * 4 < numArray.Length)
        num2 = (int) numArray[num1 * 4] << 8;
      int num3 = (num2 + (num1 * 4 + 1 < numArray.Length ? (int) numArray[num1 * 4 + 1] : 0) << 8) + (num1 * 4 + 2 < numArray.Length ? (int) numArray[num1 * 4 + 2] : 0) << 8;
      if (num1 * 4 + 3 < numArray.Length)
        num3 += (int) numArray[num1 * 4 + 3];
      if ((paramsPair.First & num3) != 0)
        flag1 = false;
      if ((paramsPair.Second & num3) != 0)
        flag2 = true;
      ++num1;
    }
    return flag1 | flag2;
  }

  /// <exclude />
  public class ParamsPair
  {
    public readonly int First;
    public readonly int Second;

    public ParamsPair(int first, int second)
    {
      this.First = first;
      this.Second = second;
    }
  }

  /// <exclude />
  private sealed class ParamsFour : GroupHelper.ParamsPair
  {
    public readonly int Third;
    public readonly int Fourth;

    public ParamsFour(int first, int second, int third, int fourth)
      : base(first, second)
    {
      this.Third = third;
      this.Fourth = fourth;
    }
  }

  /// <exclude />
  private sealed class Definition : IPrefetchable, IPXCompanyDependent
  {
    public Dictionary<System.Type, Dictionary<System.Type, GroupHelper.ParamsFour[]>> cross;
    public byte[] active;
    public byte[] fullaccess;
    public int Count;

    public void Prefetch()
    {
      this.cross = new Dictionary<System.Type, Dictionary<System.Type, GroupHelper.ParamsFour[]>>();
      this.Count = 0;
      try
      {
        using (IEnumerator<PXDataRecord> enumerator = PXDatabase.SelectMulti<RelationGroup>(new PXDataField("GroupName"), new PXDataField("Description"), new PXDataField("GroupMask"), new PXDataField("Active"), new PXDataField("GroupType")).GetEnumerator())
        {
label_29:
          while (enumerator.MoveNext())
          {
            PXDataRecord current = enumerator.Current;
            byte[] bytes = current.GetBytes(2);
            if (bytes.Length != 0)
            {
              int num1 = 0;
              byte num2 = 0;
              for (int index = bytes.Length - 1; index >= 0; --index)
              {
                if (bytes[index] != (byte) 0)
                {
                  num1 = index * 8;
                  num2 = bytes[index];
                  break;
                }
              }
              for (int index = 0; index < 8 && num2 != (byte) 0; ++index)
              {
                ++num1;
                num2 <<= 1;
              }
              if (num1 > this.Count)
                this.Count = num1;
            }
            if (this.active == null)
            {
              this.active = new byte[bytes.Length];
              this.fullaccess = new byte[bytes.Length];
            }
            else if (this.active.Length < bytes.Length)
            {
              Array.Resize<byte>(ref this.active, bytes.Length);
              Array.Resize<byte>(ref this.fullaccess, bytes.Length);
            }
            bool? boolean = current.GetBoolean(3);
            bool flag = true;
            if (boolean.GetValueOrDefault() == flag & boolean.HasValue)
            {
              for (int index = 0; index < this.active.Length && index < bytes.Length; ++index)
                this.active[index] |= bytes[index];
            }
            string a = current.GetString(4);
            if (string.Equals(a, "EE", StringComparison.OrdinalIgnoreCase) || string.Equals(a, "EO", StringComparison.OrdinalIgnoreCase))
            {
              int index = 0;
              while (true)
              {
                if (index < this.fullaccess.Length && index < bytes.Length)
                {
                  this.fullaccess[index] &= ~bytes[index];
                  ++index;
                }
                else
                  goto label_29;
              }
            }
            else
            {
              int index = 0;
              while (true)
              {
                if (index < this.fullaccess.Length && index < bytes.Length)
                {
                  this.fullaccess[index] |= bytes[index];
                  ++index;
                }
                else
                  goto label_29;
              }
            }
          }
        }
        foreach (PXDataRecord pxDataRecord in PXDatabase.SelectMulti<Neighbour>(new PXDataField("LeftEntityType"), new PXDataField("RightEntityType"), new PXDataField("CoverageMask"), new PXDataField("InverseMask"), new PXDataField("WinCoverageMask"), new PXDataField("WinInverseMask")))
        {
          System.Type type1 = PXBuildManager.GetType(pxDataRecord.GetString(0), false);
          System.Type type2 = PXBuildManager.GetType(pxDataRecord.GetString(1), false);
          if (type1 != (System.Type) null && type2 != (System.Type) null)
          {
            Dictionary<System.Type, GroupHelper.ParamsFour[]> dictionary;
            if (!this.cross.TryGetValue(type1, out dictionary))
            {
              dictionary = new Dictionary<System.Type, GroupHelper.ParamsFour[]>();
              this.cross[type1] = dictionary;
            }
            byte[] bytes1 = pxDataRecord.GetBytes(2);
            for (int index = 0; index < this.active.Length && index < bytes1.Length; ++index)
              bytes1[index] &= this.active[index];
            byte[] bytes2 = pxDataRecord.GetBytes(3);
            for (int index = 0; index < this.active.Length && index < bytes2.Length; ++index)
              bytes2[index] &= this.active[index];
            byte[] bytes3 = pxDataRecord.GetBytes(4);
            for (int index = 0; index < this.active.Length && index < bytes3.Length; ++index)
              bytes3[index] &= this.active[index];
            byte[] bytes4 = pxDataRecord.GetBytes(5);
            for (int index = 0; index < this.active.Length && index < bytes4.Length; ++index)
              bytes4[index] &= this.active[index];
            if (bytes1.Length != 0)
            {
              int num3 = 0;
              byte num4 = 0;
              for (int index = bytes1.Length - 1; index >= 0; --index)
              {
                if (bytes1[index] != (byte) 0)
                {
                  num3 = index * 8;
                  num4 = bytes1[index];
                  break;
                }
              }
              for (int index = 0; index < 8 && num4 != (byte) 0; ++index)
              {
                ++num3;
                num4 <<= 1;
              }
              if (num3 > this.Count)
                this.Count = num3;
            }
            if (bytes3.Length != 0)
            {
              int num5 = 0;
              byte num6 = 0;
              for (int index = bytes3.Length - 1; index >= 0; --index)
              {
                if (bytes3[index] != (byte) 0)
                {
                  num5 = index * 8;
                  num6 = bytes3[index];
                  break;
                }
              }
              for (int index = 0; index < 8 && num6 != (byte) 0; ++index)
              {
                ++num5;
                num6 <<= 1;
              }
              if (num5 > this.Count)
                this.Count = num5;
            }
            GroupHelper.ParamsFour[] paramsFourArray = new GroupHelper.ParamsFour[(bytes1.Length * 8 + 31 /*0x1F*/) / 32 /*0x20*/];
            for (int index = 0; index < paramsFourArray.Length; ++index)
            {
              int num7 = 0;
              int num8 = 0;
              int num9 = 0;
              int num10 = 0;
              if (index * 4 < bytes1.Length)
                num7 = (int) bytes1[index * 4] << 8;
              int first = (num7 + (index * 4 + 1 < bytes1.Length ? (int) bytes1[index * 4 + 1] : 0) << 8) + (index * 4 + 2 < bytes1.Length ? (int) bytes1[index * 4 + 2] : 0) << 8;
              if (index * 4 + 3 < bytes1.Length)
                first += (int) bytes1[index * 4 + 3];
              if (index * 4 < bytes2.Length)
                num8 = (int) bytes2[index * 4] << 8;
              int second = (num8 + (index * 4 + 1 < bytes2.Length ? (int) bytes2[index * 4 + 1] : 0) << 8) + (index * 4 + 2 < bytes2.Length ? (int) bytes2[index * 4 + 2] : 0) << 8;
              if (index * 4 + 3 < bytes2.Length)
                second += (int) bytes2[index * 4 + 3];
              if (index * 4 < bytes3.Length)
                num9 = (int) bytes3[index * 4] << 8;
              int third = (num9 + (index * 4 + 1 < bytes3.Length ? (int) bytes3[index * 4 + 1] : 0) << 8) + (index * 4 + 2 < bytes3.Length ? (int) bytes3[index * 4 + 2] : 0) << 8;
              if (index * 4 + 3 < bytes3.Length)
                third += (int) bytes3[index * 4 + 3];
              if (index * 4 < bytes4.Length)
                num10 = (int) bytes4[index * 4] << 8;
              int fourth = (num10 + (index * 4 + 1 < bytes4.Length ? (int) bytes4[index * 4 + 1] : 0) << 8) + (index * 4 + 2 < bytes4.Length ? (int) bytes4[index * 4 + 2] : 0) << 8;
              if (index * 4 + 3 < bytes4.Length)
                fourth += (int) bytes4[index * 4 + 3];
              paramsFourArray[index] = new GroupHelper.ParamsFour(first, second, third, fourth);
            }
            dictionary[type2] = paramsFourArray;
          }
        }
      }
      catch
      {
        this.Count = 0;
        this.cross.Clear();
        this.active = new byte[0];
        throw;
      }
      finally
      {
        if (this.fullaccess == null)
          this.fullaccess = new byte[0];
      }
    }
  }
}
