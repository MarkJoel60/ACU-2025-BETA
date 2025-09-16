// Decompiled with JetBrains decompiler
// Type: PX.Data.Description.GI.PXTable
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.Maintenance.GI;
using System;
using System.Diagnostics;

#nullable disable
namespace PX.Data.Description.GI;

/// <exclude />
[DebuggerDisplay("{Alias,nq} ({BqlTable.FullName,nq})")]
public class PXTable : IEquatable<PXTable>, ICloneable
{
  private string alias;
  private System.Type bqlTable;
  private System.Type cacheType;
  private bool isInDbExist;

  public PXGenericInqGrph Graph { get; private set; }

  public PXTable(string alias, System.Type bqlTable, System.Type cacheType, bool isInDbExist)
  {
    this.alias = !string.IsNullOrEmpty(alias) && !(bqlTable == (System.Type) null) ? alias : throw new PXException("The following parameters are required: alias, cacheType.");
    this.bqlTable = bqlTable;
    this.cacheType = cacheType;
    this.isInDbExist = isInDbExist;
  }

  public PXTable(string alias, System.Type bqlTable, System.Type cacheType, PXGenericInqGrph graph)
    : this(alias, bqlTable, cacheType, false)
  {
    this.Graph = graph;
  }

  public string Alias => this.alias;

  public System.Type BqlTable => this.bqlTable;

  public System.Type CacheType => this.cacheType;

  public bool IsInDbExist => this.isInDbExist;

  public bool Equals(PXTable other)
  {
    if (other == null)
      return false;
    if (this == other)
      return true;
    if (!(this.Alias == other.Alias) || !(this.BqlTable == other.BqlTable) || !(this.CacheType == other.CacheType) || this.IsInDbExist != other.IsInDbExist)
      return false;
    Guid? designId1 = (Guid?) this.Graph?.Design?.DesignID;
    Guid? designId2 = (Guid?) other.Graph?.Design?.DesignID;
    if (designId1.HasValue != designId2.HasValue)
      return false;
    return !designId1.HasValue || designId1.GetValueOrDefault() == designId2.GetValueOrDefault();
  }

  public object Clone()
  {
    return (object) new PXTable(this.Alias, this.BqlTable, this.CacheType, this.IsInDbExist)
    {
      Graph = this.Graph
    };
  }

  public override bool Equals(object obj)
  {
    if (obj == null)
      return false;
    if (this == obj)
      return true;
    return !(obj.GetType() != this.GetType()) && this.Equals((PXTable) obj);
  }

  public override int GetHashCode()
  {
    int num = (((this.alias != null ? this.alias.GetHashCode() : 0) * 397 ^ (this.bqlTable != (System.Type) null ? this.bqlTable.GetHashCode() : 0)) * 397 ^ (this.cacheType != (System.Type) null ? this.cacheType.GetHashCode() : 0)) * 397;
    PXGenericInqGrph graph = this.Graph;
    int? nullable;
    if (graph == null)
    {
      nullable = new int?();
    }
    else
    {
      GIDesign design = graph.Design;
      if (design == null)
      {
        nullable = new int?();
      }
      else
      {
        Guid? designId = design.DesignID;
        ref Guid? local = ref designId;
        nullable = local.HasValue ? new int?(local.GetValueOrDefault().GetHashCode()) : new int?();
      }
    }
    int valueOrDefault = nullable.GetValueOrDefault();
    return (num ^ valueOrDefault) * 397 ^ this.isInDbExist.GetHashCode();
  }
}
