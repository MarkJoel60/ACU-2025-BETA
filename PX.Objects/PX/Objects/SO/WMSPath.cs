// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.WMSPath
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Objects.IN;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.SO;

public class WMSPath : IEnumerable<INLocation>, IEnumerable, IEquatable<WMSPath>
{
  private readonly INLocation[] _locations;

  public string Name { get; }

  public int PathLength { get; }

  public bool IsEmpty { get; }

  public bool IsDot { get; }

  public bool IsFake { get; private set; }

  public override string ToString()
  {
    return $"{this.Name}:{(this.IsFake ? "IsFake" : this.PathLength.ToString())}";
  }

  public static WMSPath MakeFake(string name)
  {
    return new WMSPath(name, Enumerable.Empty<INLocation>())
    {
      IsFake = true
    };
  }

  public WMSPath(string name, IEnumerable<INLocation> locations)
  {
    this.Name = name;
    this._locations = locations.ToArray<INLocation>();
    this.IsEmpty = this._locations.Length == 0;
    this.IsDot = this._locations.Length == 1;
    this.PathLength = this.GetFullPathLength(this._locations);
  }

  public WMSPath MergeWith(WMSPath another, string newName)
  {
    if (this.IsEmpty && another.IsEmpty)
      return new WMSPath(newName, Enumerable.Empty<INLocation>());
    if (this.IsEmpty)
      return new WMSPath(newName, (IEnumerable<INLocation>) another);
    if (another.IsEmpty)
      return new WMSPath(newName, (IEnumerable<INLocation>) this);
    return EnumerableExtensions.Distinct<INLocation, int?>(new WMSPath[2]
    {
      this,
      another
    }.With<WMSPath[], IEnumerable<WMSPath>>((Func<WMSPath[], IEnumerable<WMSPath>>) (ps => WMSPath.SortPaths((IEnumerable<WMSPath>) ps))).Select<WMSPath, IEnumerable<INLocation>>((Func<WMSPath, IEnumerable<INLocation>>) (p => p.AsEnumerable<INLocation>())).Aggregate<IEnumerable<INLocation>>((Func<IEnumerable<INLocation>, IEnumerable<INLocation>, IEnumerable<INLocation>>) ((pA, pB) => pA.Concat<INLocation>(pB))), (Func<INLocation, int?>) (loc => loc.LocationID)).ToArray<INLocation>().With<INLocation[], WMSPath>((Func<INLocation[], WMSPath>) (a => new WMSPath(newName, (IEnumerable<INLocation>) a)));
  }

  public WMSPath GetIntersectionWith(WMSPath another, string newName)
  {
    return this.GetIntersectionWith(another.AsEnumerable<INLocation>(), newName);
  }

  public WMSPath GetIntersectionWith(IEnumerable<INLocation> locations, string newName)
  {
    return this.GetIntersectionWith(locations.Select<INLocation, int?>((Func<INLocation, int?>) (loc => loc.LocationID)), newName);
  }

  public WMSPath GetIntersectionWith(IEnumerable<int?> locationIDs, string newName)
  {
    int num1 = 0;
    int count = -1;
    int num2 = 0;
    HashSet<int?> hashSet = locationIDs.ToHashSet<int?>();
    foreach (INLocation location in this._locations)
    {
      if (count == -1 && hashSet.Contains(location.LocationID))
        count = num1;
      if (hashSet.Contains(location.LocationID))
        num2 = num1 - count;
      ++num1;
    }
    return count != -1 ? new WMSPath(newName, (IEnumerable<INLocation>) ((IEnumerable<INLocation>) this._locations).Skip<INLocation>(count).Take<INLocation>(num2 + 1).ToArray<INLocation>()) : new WMSPath(newName, Enumerable.Empty<INLocation>());
  }

  public bool Contains(WMSPath another)
  {
    return this.GetIntersectionWith(another, "temp").PathLength == another.PathLength;
  }

  public bool StartsWith(WMSPath another)
  {
    int? locationId1 = this.First<INLocation>().LocationID;
    int? locationId2 = another.First<INLocation>().LocationID;
    return locationId1.GetValueOrDefault() == locationId2.GetValueOrDefault() & locationId1.HasValue == locationId2.HasValue && this.Contains(another);
  }

  public bool EndsWith(WMSPath another)
  {
    int? locationId1 = this.Last<INLocation>().LocationID;
    int? locationId2 = another.Last<INLocation>().LocationID;
    return locationId1.GetValueOrDefault() == locationId2.GetValueOrDefault() & locationId1.HasValue == locationId2.HasValue && this.Contains(another);
  }

  public static IEnumerable<WMSPath> SortPaths(IEnumerable<WMSPath> paths, bool includePathLength = false)
  {
    return (IEnumerable<WMSPath>) paths.OrderByDescending<WMSPath, bool>((Func<WMSPath, bool>) (p => p.Any<INLocation>())).OrderBy<WMSPath, int>((Func<WMSPath, int>) (p => p.First<INLocation>().PathPriority.Value)).ThenBy<WMSPath, string>((Func<WMSPath, string>) (p => p.First<INLocation>().LocationCD)).ThenBy<WMSPath, int>((Func<WMSPath, int>) (p => !includePathLength ? 0 : p.PathLength));
  }

  public static WMSPath MergePaths(IEnumerable<WMSPath> paths, string newName)
  {
    return paths.ToArray<WMSPath>().With<WMSPath[], WMSPath>((Func<WMSPath[], WMSPath>) (a => a.Length != 1 ? ((IEnumerable<WMSPath>) a).Aggregate<WMSPath>((Func<WMSPath, WMSPath, WMSPath>) ((path, nextPath) => path.MergeWith(nextPath, newName))) : new WMSPath(newName, (IEnumerable<INLocation>) a[0])));
  }

  private int GetFullPathLength(INLocation[] locations)
  {
    return locations.Length != 0 ? locations[locations.Length - 1].PathPriority.Value - locations[0].PathPriority.Value : 0;
  }

  public IEnumerator<INLocation> GetEnumerator()
  {
    return ((IEnumerable<INLocation>) this._locations).AsEnumerable<INLocation>().GetEnumerator();
  }

  IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.GetEnumerator();

  public override bool Equals(object obj) => this.Equals(obj as WMSPath);

  public bool Equals(WMSPath other) => other != null && this.Name == other.Name;

  public override int GetHashCode()
  {
    return 539060726 + EqualityComparer<string>.Default.GetHashCode(this.Name);
  }
}
