// Decompiled with JetBrains decompiler
// Type: PX.Data.PXUnboundDefaultAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data.SQLTree;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Data;

/// <summary>Sets the default value to an unbound DAC field. The value is
/// assigned to the field when the data record is retrieved from the
/// database.</summary>
/// <remarks>This attributes is similar to the <tt>PXDefault</tt> attribute,
/// but, unlike the <tt>PXDefault</tt> attribute, it assigns the provided
/// default value to the field when a data record is retrieved from the
/// database (on the <tt>RowSelecting</tt> event). The <tt>PXDefault</tt>
/// attribute assigns the default value to the field when a data record is
/// inserted to the cache object.</remarks>
/// <example>
/// The examples below show the definitions of two DAC fields.
/// <code>
/// [PXDecimal(4)]
/// [PXUnboundDefault(TypeCode.Decimal, "0.0")]
/// public virtual Decimal? DocBal { get; set; }
/// </code>
/// <code>
/// [PXBool]
/// [PXUnboundDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
/// [PXUIField(DisplayName = "Included")]
/// public virtual bool? Included { get; set; }
/// </code>
/// </example>
public class PXUnboundDefaultAttribute : PXDefaultAttribute, IPXDependsOnFields
{
  private HashSet<System.Type> _dependencies;

  /// <summary>Initializes a new instance that calculates the default value
  /// using the provided BQL query.</summary>
  /// <param name="sourceType">The BQL query that is used to calculate the
  /// default value. Accepts the types derived from: <tt>IBqlSearch</tt>,
  /// <tt>IBqlSelect</tt>, <tt>IBqlField</tt>, <tt>IBqlTable</tt>.</param>
  public PXUnboundDefaultAttribute(System.Type sourceType)
    : base(sourceType)
  {
    this._PersistingCheck = PXPersistingCheck.Nothing;
  }

  /// <summary>Initializes a new instance that defines the default value as
  /// a constant value.</summary>
  /// <param name="constant">Constant value that is used as the default
  /// value.</param>
  /// <example>
  /// <code>
  /// [PXBool]
  /// [PXUnboundDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
  /// [PXUIField(DisplayName = "Included")]
  /// public virtual bool? Included { get; set; }
  /// </code>
  /// </example>
  public PXUnboundDefaultAttribute(object constant)
    : base(constant)
  {
    this._PersistingCheck = PXPersistingCheck.Nothing;
  }

  /// <summary>Initializes a new instance that calculates the default value
  /// using the provided BQL query and uses the constant value if the BQL
  /// query returns nothing. If the BQL query is of <tt>Select</tt> type,
  /// you should also explicitly set the <tt>SourceField</tt> property. If
  /// the BQL query is a DAC field, the attribute will take the value from
  /// the <tt>Current</tt> property of the cache object corresponding to the
  /// DAC.</summary>
  /// <param name="constant">Constant value that is used as the default
  /// value.</param>
  /// <param name="sourceType">The BQL query that is used to calculate the
  /// default value. Accepts the types derived from: <tt>IBqlSearch</tt>,
  /// <tt>IBqlSelect</tt>, <tt>IBqlField</tt>, <tt>IBqlTable</tt>.</param>
  public PXUnboundDefaultAttribute(object constant, System.Type sourceType)
    : base(constant, sourceType)
  {
    this._PersistingCheck = PXPersistingCheck.Nothing;
  }

  /// <summary>Initializes a new instance that does not provide the default
  /// value, but checks whether the field value is not null before saving to
  /// the database.</summary>
  public PXUnboundDefaultAttribute() => this._PersistingCheck = PXPersistingCheck.Nothing;

  /// <summary>Converts the provided string to a specific type and
  /// initializes a new instance that uses the conversion result as the
  /// default value.</summary>
  /// <param name="converter">The type code that specifies the type to
  /// covert the string to..</param>
  /// <param name="constant">The string representation of the constant used
  /// as the default value.</param>
  /// <example>
  /// <code>
  /// [PXDecimal(4)]
  /// [PXUnboundDefault(TypeCode.Decimal, "0.0")]
  /// public virtual Decimal? DocBal { get; set; }
  /// </code>
  /// </example>
  public PXUnboundDefaultAttribute(TypeCode converter, string constant)
    : base(converter, constant)
  {
    this._PersistingCheck = PXPersistingCheck.Nothing;
  }

  /// <summary>Initializes a new instance that determines the default value
  /// using either the provided BQL query or the constant if the BQL query
  /// returns nothing.</summary>
  /// <param name="converter">The type code that specifies the type to
  /// convert the string constant to.</param>
  /// <param name="constant">The string representation of the constant used
  /// as the default value if the BQL query returns nothing.</param>
  /// <param name="sourceType">The BQL command that is used to calculate the
  /// default value. Accepts the types derived from: <tt>IBqlSearch</tt>,
  /// <tt>IBqlSelect</tt>, <tt>IBqlField</tt>, <tt>IBqlTable</tt>.</param>
  public PXUnboundDefaultAttribute(TypeCode converter, string constant, System.Type sourceType)
    : base(converter, constant, sourceType)
  {
    this._PersistingCheck = PXPersistingCheck.Nothing;
  }

  /// <summary>Provides the default value</summary>
  /// <param name="sender">Cache</param>
  /// <param name="e">Event arguments with a row reading</param>
  /// <exclude />
  public virtual void RowSelecting(PXCache sender, PXRowSelectingEventArgs e)
  {
    if (e.Row == null)
      return;
    object newValue = (object) null;
    if (sender.RaiseFieldDefaulting(this._FieldName, e.Row, out newValue))
      sender.RaiseFieldUpdating(this._FieldName, e.Row, ref newValue);
    sender.SetValue(e.Row, this._FieldOrdinal, newValue);
  }

  public ISet<System.Type> GetDependencies(PXCache cache)
  {
    if (this._dependencies == null)
    {
      this._dependencies = new HashSet<System.Type>();
      if (this._Formula != null)
      {
        List<System.Type> typeList = new List<System.Type>();
        SQLExpression sqlExpression = SQLExpression.None();
        IBqlCreator formula = this._Formula;
        ref SQLExpression local = ref sqlExpression;
        PXGraph graph = cache.Graph;
        BqlCommandInfo info = new BqlCommandInfo(false);
        info.Fields = typeList;
        info.BuildExpression = false;
        BqlCommand.Selection selection = new BqlCommand.Selection();
        formula.AppendExpression(ref local, graph, info, selection);
        EnumerableExtensions.AddRange<System.Type>((ISet<System.Type>) this._dependencies, (IEnumerable<System.Type>) typeList);
      }
    }
    return (ISet<System.Type>) this._dependencies;
  }

  public override void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    sender.RowSelecting += new PXRowSelecting(this.RowSelecting);
  }
}
