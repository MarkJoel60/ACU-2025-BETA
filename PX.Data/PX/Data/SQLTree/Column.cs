// Decompiled with JetBrains decompiler
// Type: PX.Data.SQLTree.Column
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api;
using PX.Data.BQL;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;

#nullable disable
namespace PX.Data.SQLTree;

[DebuggerDisplay("{table_?.Alias}.{Name}")]
public class Column : SQLExpression
{
  private PX.Data.SQLTree.Table table_;
  public string Name;
  private PXDbType type_ = PXDbType.Unspecified;

  protected Column(Column other)
    : base((SQLExpression) other)
  {
    this.table_ = other.table_?.Duplicate();
    this.Name = other.Name;
    this.type_ = other.type_;
    this.DoNotEnquote = other.DoNotEnquote;
    this.DoNotEnquoteWithTable = other.DoNotEnquoteWithTable;
    this.PadSpaced = other.PadSpaced;
  }

  public override SQLExpression Duplicate() => (SQLExpression) new Column(this);

  public bool DoNotEnquote { get; set; }

  internal bool DoNotEnquoteWithTable { get; set; }

  internal bool PadSpaced { get; set; }

  public void SetDBType(PXDbType type) => this.type_ = type;

  internal override PXDbType GetDBType()
  {
    SimpleTable simpleTable = this.table_ as SimpleTable;
    if (simpleTable == null)
      return this.type_;
    switch (this.type_)
    {
      case PXDbType.Char:
      case PXDbType.NChar:
        return SelectType(PXDbType.Char, PXDbType.NChar);
      case PXDbType.NText:
      case PXDbType.Text:
        return SelectType(PXDbType.Text, PXDbType.NText);
      case PXDbType.NVarChar:
      case PXDbType.VarChar:
        return SelectType(PXDbType.VarChar, PXDbType.NVarChar);
      default:
        return this.type_;
    }

    PXDbType SelectType(PXDbType latinType, PXDbType utf8Type)
    {
      bool? nullable = PXDatabase.Provider.IsColumnLatin(simpleTable.Name, this.Name);
      if (!nullable.HasValue)
        return this.type_;
      return !nullable.Value ? utf8Type : latinType;
    }
  }

  public PX.Data.SQLTree.Table Table() => this.table_;

  public override string AliasOrName() => this.Alias() ?? this.Name;

  internal override string getSurrogateAlias() => this.AliasOrName();

  public static Column SQLColumn(System.Type field) => new Column(field);

  public Column(string name, string tableName, PXDbType type = PXDbType.Unspecified)
    : this(name, (PX.Data.SQLTree.Table) new SimpleTable(tableName), type)
  {
  }

  public Column(string name, System.Type dac, PXDbType type = PXDbType.Unspecified)
    : this(name, (PX.Data.SQLTree.Table) new SimpleTable(dac), type)
  {
  }

  public Column(System.Type field, PX.Data.SQLTree.Table table = null)
    : base()
  {
    if (!typeof (IBqlField).IsAssignableFrom(field))
      throw new ArgumentException(field.FullName + " must implement IBqlField interface.");
    this.table_ = table != null ? table : (PX.Data.SQLTree.Table) new SimpleTable(field.DeclaringType);
    this.Name = field.Name;
    this.Name = char.ToUpper(this.Name[0]).ToString() + this.Name.Substring(1);
  }

  public Column(string name, PX.Data.SQLTree.Table t, PXDbType type = PXDbType.Unspecified)
    : base()
  {
    this.table_ = t;
    this.Name = name;
    this.type_ = type;
  }

  public Column(string name)
    : this(name, (PX.Data.SQLTree.Table) null)
  {
  }

  public static SQLExpression ExternalColumns(PXGraph graph, System.Type dac, string alias = null)
  {
    if (!typeof (IBqlTable).IsAssignableFrom(dac))
      return (SQLExpression) null;
    PXCache cach = graph?.Caches[dac];
    if (cach == null)
      throw new PXException("PXCache must be defined for ExternalColumns() call. ");
    PXDatabaseRecordStatusHelper.InsertDatabaseRecordStatusIfNeeded(dac, cach);
    PXDeletedDatabaseRecordHelper.InsertDeletedDatabaseRecordIfNeeded(dac, cach);
    SQLExpression sqlExpression = SQLExpression.None();
    foreach (string field in (List<string>) cach.Fields)
    {
      PXCommandPreparingEventArgs.FieldDescription description;
      cach.RaiseCommandPreparing(field, (object) null, (object) null, PXDBOperation.Select, dac, out description);
      if (description?.Expr != null)
      {
        string columnAlias = BqlCommand.GetColumnAlias(field, dac);
        sqlExpression = description.Expr.Oper() != SQLExpression.Operation.NULL ? sqlExpression.Seq(new Column(columnAlias, dac, description.Expr.GetDBTypeOrDefault()).SetColumnTableAliases(dac, alias)) : sqlExpression.Seq(SQLExpression.Null());
      }
    }
    return sqlExpression;
  }

  public static SQLExpression Columns(PXGraph graph, System.Type dac, string alias = null)
  {
    PXCache cach = graph?.Caches[dac];
    SQLExpression sqlExpression = SQLExpression.None();
    if (cach != null)
    {
      PXDatabaseRecordStatusHelper.InsertDatabaseRecordStatusIfNeeded(dac, cach);
      PXDeletedDatabaseRecordHelper.InsertDeletedDatabaseRecordIfNeeded(dac, cach);
      foreach (string field in (List<string>) cach.Fields)
      {
        PXCommandPreparingEventArgs.FieldDescription description;
        cach.RaiseCommandPreparing(field, (object) null, (object) null, PXDBOperation.Select, dac, out description);
        if (description?.Expr != null)
        {
          description.Expr.SetColumnTableAliases(dac, alias);
          sqlExpression = sqlExpression.Seq(description.Expr);
        }
      }
    }
    else
    {
      SimpleTable t = new SimpleTable(dac, alias);
      foreach (PropertyInfo property in dac.GetProperties())
      {
        bool flag = false;
        foreach (object customAttribute in property.GetCustomAttributes(true))
        {
          if (typeof (PXDBFieldAttribute).IsAssignableFrom(customAttribute.GetType()) || typeof (PXDBCreatedByIDAttribute).IsAssignableFrom(customAttribute.GetType()))
          {
            flag = true;
            break;
          }
        }
        if (flag)
        {
          Column r = new Column(property.Name, (PX.Data.SQLTree.Table) t);
          sqlExpression = sqlExpression.Seq((SQLExpression) r);
        }
      }
    }
    return sqlExpression;
  }

  internal override SQLExpression substituteTableName(string from, string to)
  {
    if (!(this.table_ is SimpleTable table))
      return (SQLExpression) this;
    if (table.Alias != null && table.Alias.OrdinalIgnoreCaseEquals(from) || table.Name.OrdinalIgnoreCaseEquals(from))
      table.Alias = table.Name = to;
    return (SQLExpression) this;
  }

  public override bool Equals(SQLExpression other)
  {
    return other != null && !(this.GetType() != other.GetType()) && (this == other || !(this.Alias() != other.Alias()) && other is Column column && this.Name.Equals(column.Name, StringComparison.OrdinalIgnoreCase) && (this.table_ == null && column.table_ == null || this.table_ != null && column.table_ != null && (this.table_ == column.table_ || this.table_ is SimpleTable table && table.Equals(column.table_ as SimpleTable))));
  }

  internal override bool CanBeFlattened(
    Dictionary<string, SQRenamer> aliases,
    bool insideAggregate = false,
    bool isMaxAggregate = false,
    Func<Column, bool> isInsideGroupBy = null)
  {
    string key = this.table_?.AliasOrName();
    SQRenamer sqRenamer;
    SQLExpression sqlExpression;
    return key == null || !aliases.TryGetValue(key, out sqRenamer) || !sqRenamer.extCol2Int.TryGetValue(this.Name, out sqlExpression) || !(sqlExpression is SubQuery subQuery) || subQuery.CanBeFlattened(aliases, insideAggregate, isMaxAggregate, isInsideGroupBy);
  }

  internal override SQLExpression substituteColumnAliases(Dictionary<string, string> dict)
  {
    if (!(this.table_ is SimpleTable table))
      return (SQLExpression) this;
    string str;
    if (dict.TryGetValue(table.AliasOrName(), out str))
      table.Alias = str;
    return (SQLExpression) this;
  }

  internal override SQLExpression substituteExternalColumnAliases(
    Dictionary<string, SQRenamer> aliases,
    QueryPart queryPart,
    bool insideAggregate = false,
    HashSet<string> excludes = null,
    bool isMaxAggregate = false,
    Func<Column, bool> isInsideGroupBy = null)
  {
    string key = this.table_?.AliasOrName();
    if (key == null)
      return (SQLExpression) this;
    SQRenamer sqRenamer;
    SQLExpression internalField;
    // ISSUE: explicit non-virtual call
    return aliases.TryGetValue(key, out sqRenamer) && (excludes != null ? (!__nonvirtual (excludes.Contains(key)) ? 1 : 0) : 1) != 0 && sqRenamer.extCol2Int.TryGetValue(this.Name, out internalField) ? PXDatabase.Provider.SqlDialect.SubstituteExternalColumnAliases(internalField, aliases, queryPart, insideAggregate, excludes, isMaxAggregate, isInsideGroupBy) : (SQLExpression) this;
  }

  internal override SQLExpression addExternalAliasToTableNames(
    string alias,
    Dictionary<string, SQRenamer> aliases,
    HashSet<string> excludes = null)
  {
    if (!(this.table_ is SimpleTable))
      return (SQLExpression) this;
    this.table_.addExternalAliasToTableNames(alias, aliases, excludes);
    return (SQLExpression) this;
  }

  public override string ToString()
  {
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append($"{this.table_}.{this.Name}");
    if (this.Alias() != null)
      stringBuilder.Append(" AS " + this.Alias());
    return stringBuilder.ToString();
  }

  internal override T Accept<T>(ISQLExpressionVisitor<T> visitor) => visitor.Visit(this);
}
