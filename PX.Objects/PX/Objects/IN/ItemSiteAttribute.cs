// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.ItemSiteAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.SQLTree;
using System;
using System.ComponentModel;

#nullable disable
namespace PX.Objects.IN;

public class ItemSiteAttribute : PXSelectorAttribute
{
  public ItemSiteAttribute()
    : base(typeof (Search2<INItemSite.siteID, InnerJoin<INSite, On<INSite.siteID, Equal<INItemSite.siteID>, And<Where<CurrentMatch<INSite, AccessInfo.userName>>>>>, Where<INItemSite.inventoryID, Equal<Current<INItemSite.inventoryID>>>>))
  {
    this._SubstituteKey = typeof (INSite.siteCD);
    this._UnconditionalSelect = BqlCommand.CreateInstance(new Type[1]
    {
      typeof (Search<INSite.siteID, Where<INSite.siteID, Equal<Required<INSite.siteID>>>>)
    });
    this._NaturalSelect = BqlCommand.CreateInstance(new Type[1]
    {
      typeof (Search<INSite.siteCD, Where<INSite.siteCD, Equal<Required<INSite.siteCD>>>>)
    });
  }

  public virtual void SubstituteKeyCommandPreparing(PXCache sender, PXCommandPreparingEventArgs e)
  {
    if (!PXSelectorAttribute.ShouldPrepareCommandForSubstituteKey(e))
      return;
    ((CancelEventArgs) e).Cancel = true;
    foreach (PXEventSubscriberAttribute attribute in sender.GetAttributes(((PXEventSubscriberAttribute) this)._FieldName))
    {
      if (attribute is PXDBFieldAttribute)
      {
        SimpleTable simpleTable = (SimpleTable) new SimpleTable<INSite>(this._Type.Name + "Ext");
        Query query1 = new Query().Select(new SQLExpression[1]
        {
          (SQLExpression) simpleTable.Column(this._SubstituteKey)
        }).From((Table) simpleTable);
        Column column1 = simpleTable.Column<INSite.siteID>();
        string databaseFieldName = ((PXDBFieldAttribute) attribute).DatabaseFieldName;
        Type type = e.Table;
        if ((object) type == null)
          type = ((PXEventSubscriberAttribute) this)._BqlTable;
        Column column2 = new Column(databaseFieldName, type, (PXDbType) 100);
        SQLExpression sqlExpression = SQLExpressionExt.EQ((SQLExpression) column1, (SQLExpression) column2);
        Query query2 = query1.Where(sqlExpression);
        e.Expr = ((SQLExpression) new SubQuery(query2)).Embrace();
        if (e.Value == null)
          break;
        e.DataValue = e.Value;
        e.DataType = (PXDbType) 12;
        e.DataLength = new int?(((string) e.Value).Length);
        break;
      }
    }
  }

  public virtual void FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
  }
}
