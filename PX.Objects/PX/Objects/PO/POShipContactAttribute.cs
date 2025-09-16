// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.POShipContactAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CS;
using PX.Objects.IN;
using PX.Objects.PM;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.PO;

/// <summary>
/// Ctor. Internaly, it expects POShipContact as a POContact type
/// </summary>
/// <param name="SelectType">Must have type IBqlSelect. This select is used for both selecting <br />
/// a source Contact record from which PO Contact is defaulted and for selecting version of POContact, <br />
/// created from source Contact (having  matching ContactID, revision and IsDefaultContact = true).<br />
/// - so it must include both records. See example above. <br />
/// </param>
public class POShipContactAttribute(System.Type SelectType) : ContactAttribute(typeof (POShipContact.contactID), typeof (POShipContact.isDefaultContact), SelectType)
{
  public override void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    PXGraph.FieldVerifyingEvents fieldVerifying = sender.Graph.FieldVerifying;
    POShipContactAttribute contactAttribute = this;
    // ISSUE: virtual method pointer
    PXFieldVerifying pxFieldVerifying = new PXFieldVerifying((object) contactAttribute, __vmethodptr(contactAttribute, Record_Override_FieldVerifying));
    fieldVerifying.AddHandler<POShipContact.overrideContact>(pxFieldVerifying);
  }

  public override void DefaultRecord(PXCache sender, object DocumentRow, object Row)
  {
    this.DefaultContact<POShipContact, POShipContact.contactID>(sender, DocumentRow, Row);
  }

  public override void CopyRecord(
    PXCache sender,
    object DocumentRow,
    object SourceRow,
    bool clone)
  {
    this.CopyContact<POShipContact, POShipContact.contactID>(sender, DocumentRow, SourceRow, clone);
  }

  protected override void Record_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    base.Record_RowSelected(sender, e);
    if (e.Row == null)
      return;
    PXUIFieldAttribute.SetEnabled<POShipContact.overrideContact>(sender, e.Row, sender.AllowUpdate);
  }

  public override void Record_IsDefault_FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
  }

  public virtual void Record_Override_FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    PXFieldVerifyingEventArgs verifyingEventArgs1 = e;
    object obj1;
    if (e.NewValue != null)
    {
      bool? newValue = (bool?) e.NewValue;
      bool flag = false;
      obj1 = (object) (newValue.GetValueOrDefault() == flag & newValue.HasValue);
    }
    else
      obj1 = e.NewValue;
    verifyingEventArgs1.NewValue = obj1;
    try
    {
      this.Contact_IsDefaultContact_FieldVerifying<POShipContact>(sender, e);
    }
    finally
    {
      PXFieldVerifyingEventArgs verifyingEventArgs2 = e;
      object obj2;
      if (e.NewValue != null)
      {
        bool? newValue = (bool?) e.NewValue;
        bool flag = false;
        obj2 = (object) (newValue.GetValueOrDefault() == flag & newValue.HasValue);
      }
      else
        obj2 = e.NewValue;
      verifyingEventArgs2.NewValue = obj2;
    }
  }

  public override void RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    if (e.Row != null && (string) sender.GetValue<POOrder.shipDestType>(e.Row) != "S")
    {
      Dictionary<string, string> errors = PXUIFieldAttribute.GetErrors(sender, e.Row, new PXErrorLevel[1]
      {
        (PXErrorLevel) 4
      });
      if (errors != null && errors.Count > 0)
        return;
    }
    base.RowPersisting(sender, e);
  }

  public override void DefaultContact<TContact, TContactID>(
    PXCache sender,
    object DocumentRow,
    object ContactRow)
  {
    int? nullable1 = (int?) sender.GetValue<POOrder.siteID>(DocumentRow);
    int? nullable2 = (int?) sender.GetValue<POOrder.projectID>(DocumentRow);
    string str = (string) sender.GetValue<POOrder.shipDestType>(DocumentRow);
    string shipDestination = (string) null;
    object[] objArray1 = (object[]) null;
    if (nullable1.HasValue && str == "S")
    {
      shipDestination = str;
      objArray1 = new object[1]{ (object) nullable1 };
    }
    if (nullable2.HasValue && str == "P")
    {
      shipDestination = str;
      objArray1 = new object[1]{ (object) nullable2 };
    }
    PXView contactView = this.CreateContactView(sender, DocumentRow, shipDestination);
    int num1 = -1;
    int num2 = 0;
    bool flag = false;
    object[] objArray2 = objArray1;
    ref int local1 = ref num1;
    ref int local2 = ref num2;
    List<object> source1 = contactView.Select((object[]) null, objArray2, (object[]) null, (string[]) null, (bool[]) null, (PXFilterRow[]) null, ref local1, 1, ref local2);
    if (source1.Any<object>())
    {
      using (List<object>.Enumerator enumerator = source1.GetEnumerator())
      {
        if (enumerator.MoveNext())
        {
          PXResult current = (PXResult) enumerator.Current;
          if (shipDestination == "P")
          {
            PMContact source2 = PXResult.Unwrap<PMContact>((object) current);
            PX.Objects.CR.Contact contact = PropertyTransfer.Transfer<PMContact, PX.Objects.CR.Contact>(source2, new PX.Objects.CR.Contact());
            contact.EMail = source2.Email;
            flag = ContactAttribute.DefaultContact<TContact, TContactID>(sender, this.FieldName, DocumentRow, ContactRow, (object) new PXResult<PX.Objects.CR.Contact, POShipContact>(contact, new POShipContact()));
          }
          else
            flag = ContactAttribute.DefaultContact<TContact, TContactID>(sender, this.FieldName, DocumentRow, ContactRow, (object) current);
        }
      }
    }
    else if (shipDestination == "P")
      flag = ContactAttribute.DefaultContact<TContact, TContactID>(sender, this.FieldName, DocumentRow, ContactRow, (object) new PXResult<PX.Objects.CR.Contact, POShipContact>(new PX.Objects.CR.Contact()
      {
        RevisionID = new int?(0)
      }, new POShipContact()));
    if (!flag && !this._Required)
      this.ClearRecord(sender, DocumentRow);
    if (!flag && this._Required && shipDestination == "S")
      throw new SharedRecordMissingException();
  }

  protected virtual PXView CreateContactView(
    PXCache sender,
    object DocumentRow,
    string shipDestination)
  {
    switch (shipDestination)
    {
      case "S":
        BqlCommand instance1 = BqlCommand.CreateInstance(new System.Type[1]
        {
          typeof (SelectFromBase<PX.Objects.CR.Contact, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<INSite>.On<KeysRelation<Field<INSite.contactID>.IsRelatedTo<PX.Objects.CR.Contact.contactID>.AsSimpleKey.WithTablesOf<PX.Objects.CR.Contact, INSite>, PX.Objects.CR.Contact, INSite>.And<BqlOperand<INSite.siteID, IBqlInt>.IsEqual<P.AsInt>>>>, FbqlJoins.Left<POShipContact>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<POShipContact.bAccountID, Equal<PX.Objects.CR.Contact.bAccountID>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<POShipContact.bAccountContactID, Equal<PX.Objects.CR.Contact.contactID>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<POShipContact.revisionID, Equal<PX.Objects.CR.Contact.revisionID>>>>>.And<BqlOperand<POShipContact.isDefaultContact, IBqlBool>.IsEqual<boolTrue>>>>>>>.Where<BqlOperand<boolTrue, IBqlBool>.IsEqual<boolTrue>>)
        });
        return sender.Graph.TypedViews.GetView(instance1, false);
      case "P":
        BqlCommand instance2 = BqlCommand.CreateInstance(new System.Type[1]
        {
          typeof (SelectFromBase<PMContact, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PMProject>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMProject.billContactID, Equal<PMContact.contactID>>>>>.And<BqlOperand<PMProject.contractID, IBqlInt>.IsEqual<P.AsInt>>>>>.Where<BqlOperand<boolTrue, IBqlBool>.IsEqual<boolTrue>>)
        });
        return sender.Graph.TypedViews.GetView(instance2, false);
      default:
        return sender.Graph.TypedViews.GetView(this._Select, false);
    }
  }
}
