// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CRLeadFullNameAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using System;

#nullable disable
namespace PX.Objects.CR;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, AllowMultiple = false)]
[PXDBString(255 /*0xFF*/, IsUnicode = true)]
public class CRLeadFullNameAttribute : PXAggregateAttribute
{
  private readonly System.Type _accountIdBqlField;
  private int _accountIdfieldOrdinal;

  public CRLeadFullNameAttribute(System.Type accountIdBqlField)
  {
    this._accountIdBqlField = !(accountIdBqlField == (System.Type) null) ? accountIdBqlField : throw new ArgumentNullException(nameof (accountIdBqlField));
  }

  public virtual void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    System.Type itemType = sender.GetItemType();
    if (!typeof (Contact).IsAssignableFrom(itemType))
      throw new Exception($"Attribute '{((object) this).GetType().Name}' can be used only with DAC '{typeof (Contact).Name}' or its inheritors");
    this._accountIdfieldOrdinal = this.GetFieldOrdinal(sender, itemType, this._accountIdBqlField);
    // ISSUE: method pointer
    sender.Graph.RowSelected.AddHandler(itemType, new PXRowSelected((object) this, __methodptr(Handler)));
  }

  private int GetFieldOrdinal(PXCache sender, System.Type itemType, System.Type bqlField)
  {
    string field = sender.GetField(bqlField);
    return !string.IsNullOrEmpty(field) ? sender.GetFieldOrdinal(field) : throw new Exception($"Field '{bqlField.Name}' can not be not found in table '{itemType.Name}'");
  }

  private void Handler(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is Contact row) || !(row.ContactType == "PN") && !(row.ContactType == "LD"))
      return;
    object obj = sender.GetValue(e.Row, this._accountIdfieldOrdinal);
    if (obj == null)
      return;
    PXUIFieldAttribute.SetEnabled<Contact.fullName>(sender, e.Row, false);
    string str = PXResultset<BAccount>.op_Implicit(PXSelectBase<BAccount, PXSelect<BAccount, Where<BAccount.bAccountID, Equal<Required<BAccount.bAccountID>>>>.Config>.SelectSingleBound(sender.Graph, (object[]) null, new object[1]
    {
      obj
    })).With<BAccount, string>((Func<BAccount, string>) (_ => _.AcctName));
    if (str == null)
      return;
    sender.SetValue(e.Row, ((PXEventSubscriberAttribute) this)._FieldOrdinal, (object) str);
  }
}
