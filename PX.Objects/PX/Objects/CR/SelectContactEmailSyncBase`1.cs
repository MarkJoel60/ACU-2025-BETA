// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.SelectContactEmailSyncBase`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.SM;
using System;

#nullable disable
namespace PX.Objects.CR;

public class SelectContactEmailSyncBase<TCommand> : 
  PXSelectBase<Contact, SelectContactEmailSyncBase<TCommand>.Config>
  where TCommand : BqlCommand, new()
{
  public SelectContactEmailSyncBase(PXGraph graph, Delegate handler)
    : base(graph, handler)
  {
    this.AddFieldUpdatedHandlers();
  }

  public SelectContactEmailSyncBase(PXGraph graph)
    : base(graph)
  {
    this.AddFieldUpdatedHandlers();
  }

  private void AddFieldUpdatedHandlers()
  {
    PXGraph.FieldUpdatedEvents fieldUpdated1 = ((PXSelectBase) this)._Graph.FieldUpdated;
    System.Type type1 = typeof (Contact);
    string name1 = typeof (Contact.eMail).Name;
    SelectContactEmailSyncBase<TCommand> contactEmailSyncBase1 = this;
    // ISSUE: virtual method pointer
    PXFieldUpdated pxFieldUpdated1 = new PXFieldUpdated((object) contactEmailSyncBase1, __vmethodptr(contactEmailSyncBase1, FieldUpdated<Contact.eMail, Users.email>));
    fieldUpdated1.AddHandler(type1, name1, pxFieldUpdated1);
    PXGraph.FieldUpdatedEvents fieldUpdated2 = ((PXSelectBase) this)._Graph.FieldUpdated;
    System.Type type2 = typeof (Contact);
    string name2 = typeof (Contact.firstName).Name;
    SelectContactEmailSyncBase<TCommand> contactEmailSyncBase2 = this;
    // ISSUE: virtual method pointer
    PXFieldUpdated pxFieldUpdated2 = new PXFieldUpdated((object) contactEmailSyncBase2, __vmethodptr(contactEmailSyncBase2, FieldUpdated<Contact.firstName, Users.firstName>));
    fieldUpdated2.AddHandler(type2, name2, pxFieldUpdated2);
    PXGraph.FieldUpdatedEvents fieldUpdated3 = ((PXSelectBase) this)._Graph.FieldUpdated;
    System.Type type3 = typeof (Contact);
    string name3 = typeof (Contact.lastName).Name;
    SelectContactEmailSyncBase<TCommand> contactEmailSyncBase3 = this;
    // ISSUE: virtual method pointer
    PXFieldUpdated pxFieldUpdated3 = new PXFieldUpdated((object) contactEmailSyncBase3, __vmethodptr(contactEmailSyncBase3, FieldUpdated<Contact.lastName, Users.lastName>));
    fieldUpdated3.AddHandler(type3, name3, pxFieldUpdated3);
  }

  protected virtual void FieldUpdated<TSrcField, TDstField>(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
    where TSrcField : IBqlField
    where TDstField : IBqlField
  {
    Contact row = (Contact) e.Row;
    Users users = PXResultset<Users>.op_Implicit(PXSelectBase<Users, PXSelect<Users, Where<Users.pKID, Equal<Current<Contact.userID>>>>.Config>.SelectSingleBound(((PXSelectBase) this)._Graph, new object[1]
    {
      (object) row
    }, Array.Empty<object>()));
    if (users == null)
      return;
    PXCache cach = ((PXSelectBase) this)._Graph.Caches[typeof (Users)];
    cach.SetValue<TDstField>((object) users, sender.GetValue<TSrcField>((object) row));
    cach.Update((object) users);
  }

  public class Config : 
    PXSelectBase<Contact, SelectContactEmailSyncBase<TCommand>.Config>.IViewConfig,
    IViewConfigBase
  {
    public BqlCommand GetCommand() => (BqlCommand) new TCommand();

    public bool IsReadOnly => false;
  }
}
