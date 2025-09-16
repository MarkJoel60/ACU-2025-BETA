// Decompiled with JetBrains decompiler
// Type: PX.Data.SQLTree.XMLPathQuery
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api;
using System;
using System.Collections.Generic;
using System.Globalization;

#nullable disable
namespace PX.Data.SQLTree;

public class XMLPathQuery : Query
{
  /// <summary>
  /// Specifies a name for the row element. Empty string value means skipping row tag.
  /// </summary>
  public string ElementName { get; set; } = "v";

  /// <summary>
  /// Specifies that an element that has an xsi:nil attribute set to True be created for NULL column values.
  /// </summary>
  public bool HasXsinil { get; set; }

  /// <summary>
  /// If the this option is specified, any binary data returned by the query is represented in base64-encoded format
  /// </summary>
  public bool HasBinaryBase64 { get; set; }

  /// <summary>
  /// Specifies that a single, top-level element be added to the resulting XML.
  /// </summary>
  public bool HasRoot { get; set; }

  /// <summary>
  /// Specifies the root element name to generate. The default value is "root"
  /// </summary>
  public string RootName { get; set; }

  public XMLPathQuery()
  {
  }

  public XMLPathQuery(Query q)
    : base(q)
  {
  }

  protected XMLPathQuery(XMLPathQuery other)
    : this((Query) other)
  {
    this.ElementName = other.ElementName;
    this.HasXsinil = other.HasXsinil;
    this.HasRoot = other.HasRoot;
    this.RootName = other.RootName;
    this.HasBinaryBase64 = other.HasBinaryBase64;
  }

  internal override Table Duplicate() => (Table) new XMLPathQuery(this);

  internal override T Accept<T>(ISQLQueryVisitor<T> visitor) => visitor.Visit(this);

  /// <summary>
  /// FOR XML needs every non-column expression to have an alias.
  /// During flattening simple column may be replaced with expression, so we need alias for every column
  /// </summary>
  internal void EnsureColumnAliases()
  {
    int num = 0;
    List<SQLExpression> selection = this.GetSelection();
    string a = string.Empty;
    foreach (SQLExpression sqlExpression in selection)
    {
      if (!string.IsNullOrEmpty(sqlExpression.Alias()) && !a.OrdinalEquals(sqlExpression.Alias()))
        a = sqlExpression.Alias();
      else if (sqlExpression is Column column && !a.OrdinalEquals(column.Name))
      {
        a = column.Name;
        sqlExpression.SetAlias(column.Name);
      }
      else
      {
        sqlExpression.SetAlias("UnknownColumn" + num.ToString((IFormatProvider) CultureInfo.InvariantCulture));
        ++num;
        a = sqlExpression.Alias();
      }
    }
  }
}
