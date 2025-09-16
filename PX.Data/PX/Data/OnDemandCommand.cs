// Decompiled with JetBrains decompiler
// Type: PX.Data.OnDemandCommand
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.SQLTree;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data;

/// <exclude />
internal class OnDemandCommand : BqlCommand
{
  protected readonly System.Type _Table;
  protected readonly IBqlParameter[] _Parameters;
  protected readonly Query _Query;

  public OnDemandCommand(Query q, System.Type table, IBqlParameter[] parameters)
  {
    this._Table = table;
    this._Parameters = parameters;
    this._Query = q;
  }

  public override Query GetQueryInternal(
    PXGraph graph,
    BqlCommandInfo info,
    BqlCommand.Selection selection)
  {
    info.Tables?.Add(this._Table);
    info.Parameters?.AddRange((IEnumerable<IBqlParameter>) this._Parameters);
    return this._Query;
  }

  public override void Verify(
    PXCache cache,
    object item,
    List<object> pars,
    ref bool? result,
    ref object value)
  {
  }

  public override BqlCommand OrderByNew(System.Type newOrderBy) => (BqlCommand) this;

  public override BqlCommand OrderByNew<newOrderBy>() => (BqlCommand) this;

  public override BqlCommand WhereAnd(System.Type where) => (BqlCommand) this;

  public override BqlCommand WhereAnd<TWhere>() => (BqlCommand) this;

  public override BqlCommand WhereNew(System.Type newWhere) => (BqlCommand) this;

  public override BqlCommand WhereNew<newWhere>() => (BqlCommand) this;

  public override BqlCommand WhereNot() => (BqlCommand) this;

  public override BqlCommand WhereOr(System.Type where) => (BqlCommand) this;

  public override BqlCommand WhereOr<TWhere>() => (BqlCommand) this;

  public static bool GetKeyValues(
    PXCache sender,
    object row,
    System.Type bqlTable,
    Dictionary<string, int> indexes,
    out string[] alternatives)
  {
    if (sender.IsKeysFilled(row) && sender.Keys.All<string>((Func<string, bool>) (key =>
    {
      int? nullable1 = sender.GetValue(row, key) as int?;
      if (!nullable1.HasValue)
        return true;
      int? nullable2 = nullable1;
      int num = 0;
      return nullable2.GetValueOrDefault() >= num & nullable2.HasValue;
    })))
    {
      Query attributesJoined = BqlCommand.GetNoteAttributesJoined((System.Type) null, bqlTable, (System.Type) null, PXDBOperation.Select);
      Query q = new Query();
      q.Field((SQLExpression) new SubQuery(attributesJoined)).From((Table) new SimpleTable(bqlTable));
      ISqlDialect sqlDialect = sender.Graph.SqlDialect;
      List<PXDataValue> pxDataValueList = new List<PXDataValue>();
      OnDemandCommand command = (OnDemandCommand) null;
      if (bqlTable.IsAssignableFrom(sender.BqlTable) || OnDemandCommand.SenderHasProjectionFrom(sender, bqlTable) && OnDemandCommand.ProjectionContainsPrimaryKeyOf(sender, bqlTable))
      {
        foreach (string key in (IEnumerable<string>) sender.Keys)
        {
          object obj = sender.GetValue(row, key);
          PXCommandPreparingEventArgs.FieldDescription description;
          sender.RaiseCommandPreparing(key, row, obj, PXDBOperation.Update, bqlTable, out description);
          if (description?.Expr != null && description.IsRestriction)
          {
            q.Where((q.GetWhere() ?? SQLExpression.None()).And(SQLExpressionExt.EQ(description.Expr, (SQLExpression) Literal.NewParameter(pxDataValueList.Count))));
            pxDataValueList.Add(new PXDataValue(description.DataType, description.DataLength, description.DataValue));
          }
        }
        command = new OnDemandCommand(q, bqlTable, sender.BqlKeys.Select<System.Type, IBqlParameter>((Func<System.Type, IBqlParameter>) (_ => (IBqlParameter) Activator.CreateInstance(typeof (Required<>).MakeGenericType(_)))).ToArray<IBqlParameter>());
      }
      else if (sender._NoteIDName != null)
      {
        System.Type bqlField = sender.GetBqlField(sender._NoteIDName);
        if (bqlField != (System.Type) null)
        {
          object obj = sender.GetValue(row, sender._NoteIDName);
          PXCommandPreparingEventArgs.FieldDescription description;
          sender.RaiseCommandPreparing(sender._NoteIDName, row, obj, PXDBOperation.Update, bqlTable, out description);
          if (description?.Expr != null)
          {
            q.Where((q.GetWhere() ?? SQLExpression.None()).And(SQLExpressionExt.EQ(description.Expr, (SQLExpression) Literal.NewParameter(pxDataValueList.Count))));
            pxDataValueList.Add(new PXDataValue(description.DataType, description.DataLength, description.DataValue));
            command = new OnDemandCommand(q, bqlTable, new IBqlParameter[1]
            {
              (IBqlParameter) Activator.CreateInstance(typeof (Required<>).MakeGenericType(bqlField))
            });
          }
        }
      }
      if (command != null)
      {
        try
        {
          using (IEnumerator<PXDataRecord> enumerator = sender.Graph.ProviderSelect((BqlCommand) command, 1, pxDataValueList.ToArray()).GetEnumerator())
          {
            if (enumerator.MoveNext())
            {
              string firstColumn = enumerator.Current.GetString(0);
              if (sqlDialect.tryExtractAttributes(firstColumn, (IDictionary<string, int>) indexes, out alternatives))
                return true;
            }
          }
        }
        catch
        {
        }
      }
    }
    alternatives = (string[]) null;
    return false;
  }

  private static bool SenderHasProjectionFrom(PXCache sender, System.Type table)
  {
    if (sender.BqlSelect == null)
      return false;
    System.Type firstTable = sender.BqlSelect.GetFirstTable();
    return table.IsAssignableFrom(PXCache.GetBqlTable(firstTable));
  }

  private static bool ProjectionContainsPrimaryKeyOf(PXCache sender, System.Type table)
  {
    PXCache cach = sender.Graph.Caches[table];
    return sender == cach || sender.Keys.Intersect<string>((IEnumerable<string>) cach.Keys, (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase).Count<string>() == sender.Keys.Count;
  }
}
