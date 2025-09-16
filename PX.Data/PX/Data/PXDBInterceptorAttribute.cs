// Decompiled with JetBrains decompiler
// Type: PX.Data.PXDBInterceptorAttribute
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

public abstract class PXDBInterceptorAttribute : Attribute
{
  protected bool tableMeet(
    PXCommandPreparingEventArgs.FieldDescription description,
    System.Type table,
    ISqlDialect dialect)
  {
    return (description.Expr is Column expr ? expr.Table() : (Table) null) is SimpleTable simpleTable && simpleTable.AliasOrName() == table.Name;
  }

  protected bool fieldMeet(string databaseFieldName, string fieldName, ISqlDialect dialect)
  {
    return databaseFieldName.EndsWith("." + fieldName) || databaseFieldName.EndsWith("." + dialect.quoteDbIdentifier(fieldName));
  }

  protected static object[] getKeys(PXCache sender, object node)
  {
    object[] keys = new object[sender.Keys.Count];
    for (int index = 0; index < keys.Length; ++index)
      keys[index] = sender.GetValue(node, sender.Keys[index]);
    return keys;
  }

  public PXDBInterceptorAttribute Child { get; set; }

  internal virtual List<string> Keys => (List<string>) null;

  public abstract BqlCommand GetRowCommand();

  public virtual BqlCommand GetRowByNoteIdCommand()
  {
    throw new NotSupportedException("Table does not support select by noteid");
  }

  public abstract BqlCommand GetTableCommand();

  public virtual bool CanSelectByNoteId => false;

  internal virtual BqlCommand GetTableCommand(PXCache sender) => this.GetTableCommand();

  public virtual void CacheAttached(PXCache sender)
  {
  }

  public virtual System.Type[] GetTables()
  {
    BqlCommand tableCommand = this.GetTableCommand();
    return tableCommand != null ? tableCommand.GetTables() : new System.Type[0];
  }

  public virtual bool PersistInserted(PXCache sender, object row) => false;

  public virtual bool PersistUpdated(PXCache sender, object row) => false;

  public virtual bool PersistDeleted(PXCache sender, object row) => false;

  public virtual object Insert(PXCache sender, object row) => sender.Insert(row, true);

  public virtual object Update(PXCache sender, object row) => sender.Update(row, true);

  public virtual object Delete(PXCache sender, object row) => sender.Delete(row, true);

  public virtual bool CacheSelected => true;

  public virtual PXDBInterceptorAttribute Clone()
  {
    return (PXDBInterceptorAttribute) this.MemberwiseClone();
  }

  protected static PXLockViolationException GetLockViolationException(
    System.Type table,
    PXDataFieldParam[] parameters,
    PXDBOperation operation)
  {
    object[] keys;
    bool deletedDatabaseRecord = PXDatabase.IsDeletedDatabaseRecord(table, parameters, out keys);
    return new PXLockViolationException(table, operation, keys, deletedDatabaseRecord);
  }

  [PXInternalUseOnly]
  public virtual PersistOrder PersistOrder { get; set; }
}
