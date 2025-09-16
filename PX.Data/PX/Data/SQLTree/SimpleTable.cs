// Decompiled with JetBrains decompiler
// Type: PX.Data.SQLTree.SimpleTable
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;
using System.Text;

#nullable disable
namespace PX.Data.SQLTree;

public class SimpleTable : Table, IEquatable<SimpleTable>
{
  public string Name;

  internal override StringBuilder SQLTableName(BaseSQLWriterVisitor visitor)
  {
    return visitor.SQLTableName(this);
  }

  internal override StringBuilder SQLAlias(BaseSQLWriterVisitor visitor) => visitor.SQLAlias(this);

  public SimpleTable(string name, string alias = null)
  {
    this.Name = name;
    this.Alias = alias;
  }

  public SimpleTable(System.Type dac, string alias = null)
  {
    if (alias == null)
      this.Alias = dac.Name;
    else
      this.Alias = alias;
    this.Name = BqlCommand.GetTableName(dac);
  }

  internal override Table addExternalAliasToTableNames(
    string alias,
    Dictionary<string, SQRenamer> aliases,
    HashSet<string> excludes = null)
  {
    Dictionary<string, string> sub2External = aliases[alias].sub2External;
    string str1 = this.Alias ?? this.Name;
    string key = str1;
    string str2;
    ref string local = ref str2;
    // ISSUE: explicit non-virtual call
    if (sub2External.TryGetValue(key, out local) && (excludes != null ? (!__nonvirtual (excludes.Contains(str1)) ? 1 : 0) : 1) != 0)
    {
      this.Name = str2;
      this.Alias = (string) null;
    }
    return (Table) this;
  }

  internal override Table transformTableName(string from, string to)
  {
    if (string.Equals(this.Name, from, StringComparison.OrdinalIgnoreCase))
    {
      if (this.Alias == null)
        this.Alias = this.Name;
      this.Name = to;
    }
    return (Table) this;
  }

  internal override Table Duplicate() => (Table) new SimpleTable(this.Name, this.Alias);

  internal override Table substituteColumnAliases(Dictionary<string, string> dict)
  {
    string str;
    if (dict.TryGetValue(this.Alias ?? this.Name, out str))
      this.Alias = str;
    return (Table) this;
  }

  public override string ToString() => this.Name + (this.Alias == null ? "" : " " + this.Alias);

  public virtual bool Equals(SimpleTable other)
  {
    if (other == null || this.GetType() != other.GetType())
      return false;
    if (this == other)
      return true;
    string str1 = this.Alias ?? this.Name;
    string str2 = other.Alias ?? other.Name;
    string name = this.Name;
    return (name != null ? (name.Equals(other.Name, StringComparison.OrdinalIgnoreCase) ? 1 : 0) : 0) != 0 && str1 != null && str1.Equals(str2, StringComparison.OrdinalIgnoreCase);
  }

  public PX.Data.SQLTree.Column Column(System.Type column) => new PX.Data.SQLTree.Column(column, (Table) this);

  public PX.Data.SQLTree.Column Column(string column) => new PX.Data.SQLTree.Column(column, (Table) this);

  public PX.Data.SQLTree.Column Column<TField>() where TField : IBqlField
  {
    return (PX.Data.SQLTree.Column) new PX.Data.SQLTree.Column<TField>((Table) this);
  }

  internal override T Accept<T>(ISQLQueryVisitor<T> visitor) => visitor.Visit(this);

  public override string AliasOrName() => this.Alias ?? this.Name;
}
