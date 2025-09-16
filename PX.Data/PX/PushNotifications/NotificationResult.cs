// Decompiled with JetBrains decompiler
// Type: PX.PushNotifications.NotificationResult
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.PushNotifications;

/// <exclude />
public class NotificationResult
{
  public NotificationResult(
    Dictionary<KeyWithAlias, object>[] inserted,
    Dictionary<KeyWithAlias, object>[] deleted,
    string query)
  {
    this.InitializeRecords(inserted, deleted);
    this.Query = query;
  }

  private void InitializeRecords(
    Dictionary<KeyWithAlias, object>[] inserted,
    Dictionary<KeyWithAlias, object>[] deleted)
  {
    NotificationResult.NotificationResultRow[] array1 = ((IEnumerable<Dictionary<KeyWithAlias, object>>) inserted).Select<Dictionary<KeyWithAlias, object>, NotificationResult.NotificationResultRow>((Func<Dictionary<KeyWithAlias, object>, int, NotificationResult.NotificationResultRow>) ((c, i) => new NotificationResult.NotificationResultRow(c, i))).OrderBy<NotificationResult.NotificationResultRow, NotificationResult.NotificationResultRow>((Func<NotificationResult.NotificationResultRow, NotificationResult.NotificationResultRow>) (c => c)).ToArray<NotificationResult.NotificationResultRow>();
    NotificationResult.NotificationResultRow[] array2 = ((IEnumerable<Dictionary<KeyWithAlias, object>>) deleted).Select<Dictionary<KeyWithAlias, object>, NotificationResult.NotificationResultRow>((Func<Dictionary<KeyWithAlias, object>, int, NotificationResult.NotificationResultRow>) ((c, i) => new NotificationResult.NotificationResultRow(c, i))).OrderBy<NotificationResult.NotificationResultRow, NotificationResult.NotificationResultRow>((Func<NotificationResult.NotificationResultRow, NotificationResult.NotificationResultRow>) (c => c)).ToArray<NotificationResult.NotificationResultRow>();
    int index = 0;
    foreach (NotificationResult.NotificationResultRow notificationResultRow1 in array2)
    {
      for (; !notificationResultRow1.Erased && index < array1.Length && notificationResultRow1.GetHashCode() >= array1[index].GetHashCode(); ++index)
      {
        NotificationResult.NotificationResultRow notificationResultRow2 = array1[index];
        if (notificationResultRow1.GetHashCode() == notificationResultRow2.GetHashCode() && notificationResultRow1.Equals((object) notificationResultRow2))
        {
          notificationResultRow1.Erased = true;
          notificationResultRow2.Erased = true;
        }
      }
    }
    this.Inserted = ((IEnumerable<NotificationResult.NotificationResultRow>) array1).Where<NotificationResult.NotificationResultRow>((Func<NotificationResult.NotificationResultRow, bool>) (c => !c.Erased)).OrderBy<NotificationResult.NotificationResultRow, int>((Func<NotificationResult.NotificationResultRow, int>) (c => c.InitialOrder)).Distinct<NotificationResult.NotificationResultRow>().Select<NotificationResult.NotificationResultRow, Dictionary<KeyWithAlias, object>>((Func<NotificationResult.NotificationResultRow, Dictionary<KeyWithAlias, object>>) (c => c.Row)).ToArray<Dictionary<KeyWithAlias, object>>();
    this.Deleted = ((IEnumerable<NotificationResult.NotificationResultRow>) array2).Where<NotificationResult.NotificationResultRow>((Func<NotificationResult.NotificationResultRow, bool>) (c => !c.Erased)).OrderBy<NotificationResult.NotificationResultRow, int>((Func<NotificationResult.NotificationResultRow, int>) (c => c.InitialOrder)).Distinct<NotificationResult.NotificationResultRow>().Select<NotificationResult.NotificationResultRow, Dictionary<KeyWithAlias, object>>((Func<NotificationResult.NotificationResultRow, Dictionary<KeyWithAlias, object>>) (c => c.Row)).ToArray<Dictionary<KeyWithAlias, object>>();
  }

  [Obsolete("For serialization purpose only")]
  public NotificationResult()
  {
  }

  public Dictionary<KeyWithAlias, object>[] Inserted { get; set; }

  public Dictionary<KeyWithAlias, object>[] Deleted { get; set; }

  public string Query { get; set; }

  public string CompanyId { get; set; }

  public Guid Id { get; set; }

  public long TimeStamp { get; set; }

  public Dictionary<string, object> AdditionalInfo { get; set; }

  public object QueryId { get; set; }

  private class NotificationResultRow : IComparable
  {
    private readonly int _hashCode;
    private readonly Dictionary<KeyWithAlias, object> _row;

    public NotificationResultRow(Dictionary<KeyWithAlias, object> row, int initialOrder)
    {
      this._row = row;
      this.InitialOrder = initialOrder;
      this._hashCode = NotificationResult.NotificationResultRow.CalculateHashCode(row);
    }

    public int InitialOrder { get; }

    public bool Erased { get; set; }

    public Dictionary<KeyWithAlias, object> Row => this._row;

    public override bool Equals(object other)
    {
      int? nullable = other is NotificationResult.NotificationResultRow notificationResultRow ? new int?(notificationResultRow._hashCode) : new int?();
      int hashCode = this._hashCode;
      return nullable.GetValueOrDefault() == hashCode & nullable.HasValue && NotificationResult.NotificationResultRow.DictionaryStructuralEquals(this.Row, notificationResultRow.Row);
    }

    public static bool DictionaryStructuralEquals(
      Dictionary<KeyWithAlias, object> x,
      Dictionary<KeyWithAlias, object> y)
    {
      if (x == null && y == null)
        return true;
      int? count1 = x?.Count;
      int? count2 = y?.Count;
      if (!(count1.GetValueOrDefault() == count2.GetValueOrDefault() & count1.HasValue == count2.HasValue))
        return false;
      foreach (KeyWithAlias key in x.Keys)
      {
        object objA;
        if (!y.TryGetValue(key, out objA))
          return false;
        if (objA is IStructuralEquatable structuralEquatable)
        {
          if (!structuralEquatable.Equals(x[key], (IEqualityComparer) EqualityComparer<object>.Default))
            return false;
        }
        else if (!object.Equals(objA, x[key]))
          return false;
      }
      return true;
    }

    public override int GetHashCode() => this._hashCode;

    public int CompareTo(object obj)
    {
      return obj is NotificationResult.NotificationResultRow notificationResultRow ? this._hashCode.CompareTo(notificationResultRow._hashCode) : throw new ArgumentException("Incorrect Type", nameof (obj));
    }

    private static int CalculateHashCode(Dictionary<KeyWithAlias, object> obj)
    {
      int h1 = 0;
      foreach (KeyValuePair<KeyWithAlias, object> keyValuePair in obj)
      {
        h1 = NotificationResult.NotificationResultRow.CombineHashCodes(h1, keyValuePair.Key.GetHashCode());
        object obj1 = keyValuePair.Value;
        int h2 = obj1 is IStructuralEquatable structuralEquatable ? structuralEquatable.GetHashCode((IEqualityComparer) EqualityComparer<object>.Default) : (obj1 != null ? obj1.GetHashCode() : 0);
        h1 = NotificationResult.NotificationResultRow.CombineHashCodes(h1, h2);
      }
      return h1;
    }

    private static int CombineHashCodes(int h1, int h2) => (h1 << 5) + h1 ^ h2;
  }
}
