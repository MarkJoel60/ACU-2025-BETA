// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CoalesceCombinedDBStringListsAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.SQLTree;
using System.Linq;

#nullable disable
namespace PX.Objects.CR;

/// <summary>
/// Combines all <see cref="T:PX.Data.PXStringListAttribute" /> from specified fields and append SQL query during select by executing coalesce on each field by another in the specified order.
/// </summary>
public class CoalesceCombinedDBStringListsAttribute(System.Type table, params System.Type[] fields) : 
  CombinedDBStringListsAttribute(table, fields)
{
  protected override void PrepareFieldExpression(
    PXCache cache,
    PXCommandPreparingEventArgs e,
    int fieldIndex)
  {
    if (!(e.Expr is SQLSwitch sqlSwitch))
      e.Expr = (SQLExpression) (sqlSwitch = new SQLSwitch());
    System.Type field = this.Fields[fieldIndex];
    PXDBCalcedAttribute pxdbCalcedAttribute = cache.GetAttributes(field.Name).OfType<PXDBCalcedAttribute>().FirstOrDefault<PXDBCalcedAttribute>();
    SQLExpression sqlExpression;
    if (pxdbCalcedAttribute != null)
    {
      PXCommandPreparingEventArgs preparingEventArgs = new PXCommandPreparingEventArgs(e.Row, e.Value, e.Operation, e.Table, e.SqlDialect);
      pxdbCalcedAttribute.CommandPreparing(cache, preparingEventArgs);
      sqlExpression = preparingEventArgs.Expr ?? SQLExpression.IsTrue(true);
    }
    else
    {
      PXDBFieldAttribute pxdbFieldAttribute = cache.GetAttributes(field.Name).OfType<PXDBFieldAttribute>().FirstOrDefault<PXDBFieldAttribute>();
      if (pxdbFieldAttribute != null)
      {
        string databaseFieldName = pxdbFieldAttribute.DatabaseFieldName;
        System.Type type = ((PXEventSubscriberAttribute) pxdbFieldAttribute).BqlTable;
        if ((object) type == null)
          type = field.DeclaringType;
        sqlExpression = (SQLExpression) new Column(databaseFieldName, type, (PXDbType) 100);
      }
      else
        sqlExpression = (SQLExpression) new Column(field, (PX.Data.SQLTree.Table) null);
    }
    SQLConst sqlConst = new SQLConst((object) CombinedStringListsAttribute.GetPrefix(fieldIndex));
    sqlConst.SetDBType((PXDbType) 22);
    sqlSwitch.Case(sqlExpression.IsNotNull(), SQLExpressionExt.Concat((SQLExpression) sqlConst, sqlExpression));
  }
}
