// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.AttributeValueAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.SQLTree;
using System;

#nullable disable
namespace PX.Objects.CS;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public sealed class AttributeValueAttribute : 
  PXEventSubscriberAttribute,
  IPXRowSelectingSubscriber,
  IPXCommandPreparingSubscriber,
  IPXFieldSelectingSubscriber,
  IPXRowSelectedSubscriber
{
  private readonly Type _attributeID;
  private readonly Type _refNoteId;

  public AttributeValueAttribute(Type attributeID, Type refNoteId)
  {
    if (attributeID == (Type) null)
      throw new ArgumentNullException(nameof (attributeID));
    if (!typeof (IBqlField).IsAssignableFrom(attributeID))
      throw new ArgumentException($"'{MainTools.GetLongName(attributeID)}' must implement '{MainTools.GetLongName(typeof (IBqlField))}' interface.", nameof (attributeID));
    if (refNoteId == (Type) null)
      throw new ArgumentNullException("entityNoteID");
    if (!typeof (IBqlField).IsAssignableFrom(refNoteId))
      throw new ArgumentException($"'{MainTools.GetLongName(refNoteId)}' must implement '{MainTools.GetLongName(typeof (IBqlField))}' interface.", nameof (refNoteId));
    this._attributeID = attributeID;
    this._refNoteId = refNoteId;
  }

  public void CommandPreparing(PXCache sender, PXCommandPreparingEventArgs e)
  {
    if ((e.Operation & 3) != null)
      return;
    PXCommandPreparingEventArgs preparingEventArgs = e;
    Query query = new Query().Select<CSAnswers.value>().From<CSAnswers>();
    SQLExpression sqlExpression1 = SQLExpressionExt.EQ((SQLExpression) new Column<CSAnswers.attributeID>((Table) null), (SQLExpression) new SQLConst(this.GetAttributeID(sender)));
    Column<CSAnswers.refNoteID> column1 = new Column<CSAnswers.refNoteID>((Table) null);
    Type refNoteId = this._refNoteId;
    Type type = e.Table;
    if ((object) type == null)
      type = this._BqlTable;
    SimpleTable simpleTable = new SimpleTable(type, (string) null);
    Column column2 = new Column(refNoteId, (Table) simpleTable);
    SQLExpression sqlExpression2 = SQLExpressionExt.EQ((SQLExpression) column1, (SQLExpression) column2);
    SQLExpression sqlExpression3 = sqlExpression1.And(sqlExpression2);
    SQLExpression sqlExpression4 = ((SQLExpression) new SubQuery(query.Where(sqlExpression3))).Embrace();
    preparingEventArgs.Expr = sqlExpression4;
  }

  private object GetAttributeID(PXCache sender)
  {
    PXCache cach = sender.Graph.Caches[BqlCommand.GetItemType(this._attributeID)];
    return cach.GetValue(cach.Current, this._attributeID.Name);
  }

  public void RowSelecting(PXCache sender, PXRowSelectingEventArgs e)
  {
    if (e.Row != null)
    {
      object obj = e.Record.GetValue(e.Position);
      sender.SetValue(e.Row, this._FieldOrdinal, obj == null ? (object) null : Convert.ChangeType(obj, typeof (string)));
    }
    ++e.Position;
  }

  public void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    if (this._AttributeLevel != 2 && !e.IsAltered)
      return;
    e.ReturnState = (object) PXFieldState.CreateInstance(e.ReturnState, typeof (string), new bool?(false), new bool?(true), new int?(), new int?(), new int?(), (object) null, this._FieldName, (string) null, (string) null, (string) null, (PXErrorLevel) 0, new bool?(), new bool?(), new bool?(), (PXUIVisibility) 0, (string) null, (string[]) null, (string[]) null);
  }

  public void RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (sender.GetStatus(e.Row) != 2)
      return;
    object obj1 = sender.GetValue(e.Row, this._refNoteId.Name);
    object obj2 = (object) null;
    if (obj1 != null)
      obj2 = (object) PXResultset<CSAnswers>.op_Implicit(PXSelectBase<CSAnswers, PXSelect<CSAnswers, Where<CSAnswers.attributeID, Equal<Required<CSAnswers.attributeID>>>>.Config>.Search<CSAnswers.refNoteID>(sender.Graph, obj1, new object[1]
      {
        this.GetAttributeID(sender)
      })).With<CSAnswers, string>((Func<CSAnswers, string>) (att => att.Value));
    sender.SetValue(e.Row, this._FieldName, obj2);
  }
}
