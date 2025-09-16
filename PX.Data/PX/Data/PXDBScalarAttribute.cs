// Decompiled with JetBrains decompiler
// Type: PX.Data.PXDBScalarAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data.PushNotifications;
using PX.Data.SQLTree;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data;

/// <summary>Defines the SQL subrequest that will be used to retrieve the
/// value for the DAC field.</summary>
/// <remarks>
/// <para>You place this attribute on the DAC field that is not bound to
/// any particular database column, but to multiple database columns.
/// Fields marked with this attribute are considered bound to the database.</para>
/// <para>The attribute will translate the provided BQL <tt>Search</tt>
/// command into the SQL subrequest and insert it into the SELECT
/// statement that retrieves data records of this DAC. In the BQL command,
/// you can reference any bound field of any DAC.</para>
/// <para>Note that you should also mark the field with an attribute
/// that usually indicates an unbound
/// field of a particular data type. Otherwise, the field may be
/// displayed incorrectly in the user interface.</para>
/// <para>You should not use fields marked with the <tt>PXDBScalar</tt>
/// attribute in BQL parameters (<tt>Current</tt>, <tt>Optional</tt>, and
/// <see cref="!:Required" />).</para>
/// </remarks>
/// <example>
/// The attribute below selects the <tt>AcctName</tt> value from the
/// <tt>Vendor</tt> table as the <tt>VendorName</tt> value.
/// <code>
/// [PXString(50, IsUnicode = true)]
/// [PXDBScalar(typeof(
/// Search&lt;Vendor.acctName,
/// Where&lt;Vendor.bAccountID, Equal&lt;RQRequestLine.vendorID&gt;&gt;&gt;))]
/// public virtual string VendorName { get; set; }</code>
/// </example>
[PXAttributeFamily(typeof (PXDBFieldAttribute))]
public class PXDBScalarAttribute : PXDBFieldAttribute
{
  protected BqlCommand _Search;
  protected Dictionary<PXDBScalarAttribute.CacheKey, SQLExpression> dictExpr = new Dictionary<PXDBScalarAttribute.CacheKey, SQLExpression>();
  protected System.Type typeOfProperty;
  private bool _recursiveCall;

  /// <summary>
  /// Initializes a new instance that uses the provided <tt>Search</tt> command
  /// to retrieve the value of the field the attribute is attached to.
  /// </summary>
  /// <param name="search">The BQL query based on the <tt>Search</tt> class or
  /// other class derived from <tt>IBqlSearch</tt>.</param>
  public PXDBScalarAttribute(System.Type search)
  {
    this._Search = BqlCommand.CreateInstance(search);
    this.BqlField = ((IBqlSearch) this._Search).GetField();
  }

  /// <summary>
  /// Returns or sets the value indicating that the DAC field is subquery and must be used as is
  /// </summary>
  public bool Forced { get; set; }

  /// <exclude />
  public override void CommandPreparing(PXCache sender, PXCommandPreparingEventArgs e)
  {
    if ((e.Operation & PXDBOperation.Delete) != PXDBOperation.Select || sender.BypassCalced && sender.BqlSelect == null || this._recursiveCall)
      return;
    if ((e.Operation & PXDBOperation.Option) == PXDBOperation.Select || (e.Operation & PXDBOperation.Option) == PXDBOperation.Internal || (e.Operation & PXDBOperation.Option) == PXDBOperation.External || (e.Operation & PXDBOperation.Option) == PXDBOperation.GroupBy && sender.BqlSelect == null && this.Forced)
    {
      e.IsForcedSubQuery = this.Forced;
      PXDBScalarAttribute.CacheKey key = new PXDBScalarAttribute.CacheKey(sender.Graph.GetType(), sender.GetItemType(), e.Table);
      if (!TableChangingScope.IsScoped)
      {
        lock (((ICollection) this.dictExpr).SyncRoot)
        {
          SQLExpression sqlExpression = (SQLExpression) null;
          if (this.dictExpr.TryGetValue(key, out sqlExpression))
          {
            e.BqlTable = this._BqlTable;
            e.Expr = sqlExpression.Duplicate();
            return;
          }
        }
      }
      Query q = (Query) null;
      List<System.Type> typeList1;
      if (sender.GetItemType().IsAssignableFrom(BqlCommand.GetItemType(((IBqlSearch) this._Search).GetField())))
        typeList1 = new List<System.Type>((IEnumerable<System.Type>) new System.Type[1]
        {
          sender.GetItemType()
        });
      else if (e.Table != (System.Type) null)
        typeList1 = new List<System.Type>((IEnumerable<System.Type>) new System.Type[1]
        {
          e.Table
        });
      else
        typeList1 = new List<System.Type>();
      bool flag1 = true;
      BqlCommand.Selection selection1 = new BqlCommand.Selection()
      {
        _Command = this._Search,
        Restrict = false,
        RestrictedFields = (RestrictedFieldsSet) null
      };
      bool flag2;
      try
      {
        PXMutableCollection mutableCollection = PXContext.GetSlot<PXMutableCollection>();
        if (flag1 = mutableCollection == null)
        {
          mutableCollection = new PXMutableCollection();
          PXContext.SetSlot<PXMutableCollection>(mutableCollection);
        }
        int count = mutableCollection.Count;
        List<System.Type> typeList2 = new List<System.Type>();
        typeList2.AddRange((IEnumerable<System.Type>) typeList1);
        this._recursiveCall = true;
        BqlCommand search = this._Search;
        PXGraph graph = sender.Graph;
        BqlCommandInfo info = new BqlCommandInfo(false);
        info.Tables = typeList2;
        BqlCommand.Selection selection2 = selection1;
        q = search.GetQueryInternal(graph, info, selection2);
        flag2 = mutableCollection.Count > count;
      }
      finally
      {
        this._recursiveCall = false;
        if (flag1)
          PXContext.SetSlot<PXMutableCollection>((PXMutableCollection) null);
      }
      System.Type field = ((IBqlSearch) this._Search).GetField();
      if (field.DeclaringType.IsAssignableFrom(sender.GetItemType()) && string.Equals(this._FieldName, field.Name, StringComparison.OrdinalIgnoreCase))
      {
        e.Expr = (SQLExpression) new Column(this._FieldName, typeList1.First<System.Type>());
      }
      else
      {
        if (this._Search is IBqlAggregate && ((IEnumerable<IBqlFunction>) ((IBqlAggregate) this._Search).GetAggregates()).All<IBqlFunction>((Func<IBqlFunction, bool>) (f => f.GetField() != field)))
        {
          q.ClearSelection();
          q.Field(selection1.ColExprs.Last<SQLExpression>());
        }
        else
        {
          q.ClearSelection();
          q.Field(BqlCommand.GetSingleExpression(field, sender.Graph, new List<System.Type>(), selection1, BqlCommand.FieldPlace.Select));
        }
        e.Expr = (SQLExpression) new SubQuery(q);
      }
      e.BqlTable = this._BqlTable;
      if (flag2 || TableChangingScope.IsScoped)
        return;
      lock (((ICollection) this.dictExpr).SyncRoot)
        this.dictExpr[key] = e.Expr.Duplicate();
    }
    else
    {
      if ((e.Operation & PXDBOperation.Option) != PXDBOperation.GroupBy)
        return;
      if (this.Forced && sender.BqlSelect != null)
      {
        e.Expr = (SQLExpression) new Column(this._FieldName, e.Table);
        e.BqlTable = sender.GetItemType();
      }
      else
        e.Expr = SQLExpression.Null();
    }
  }

  /// <exclude />
  public override void CacheAttached(PXCache sender)
  {
    this.typeOfProperty = sender.GetFieldType(this._FieldName);
  }

  /// <exclude />
  public override void RowSelecting(PXCache sender, PXRowSelectingEventArgs e)
  {
    if (e.Row != null)
    {
      object obj = e.Record.GetValue(e.Position, this.typeOfProperty);
      this.SetValue(sender, e.Row, obj);
    }
    ++e.Position;
  }

  protected virtual void SetValue(PXCache cache, object row, object value)
  {
    cache.SetValue(row, this._FieldOrdinal, value);
  }

  /// <exclude />
  protected sealed class CacheKey
  {
    private System.Type _GraphType;
    private System.Type _CacheType;
    private System.Type _TableType;
    private int? _HashCode;

    public CacheKey(System.Type graphType, System.Type cacheType, System.Type tableType)
    {
      this._GraphType = graphType;
      this._CacheType = cacheType;
      this._TableType = tableType;
    }

    public override bool Equals(object obj)
    {
      return obj is PXDBScalarAttribute.CacheKey cacheKey && this._GraphType == cacheKey._GraphType && this._CacheType == cacheKey._CacheType && this._TableType == cacheKey._TableType;
    }

    public override int GetHashCode()
    {
      if (!this._HashCode.HasValue)
      {
        int num = 37 * (37 * (37 * 13 + this._GraphType.GetHashCode()) + this._CacheType.GetHashCode());
        if (this._TableType != (System.Type) null)
          num += this._TableType.GetHashCode();
        this._HashCode = new int?(num);
      }
      return this._HashCode.Value;
    }
  }
}
