// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.POShipAddressAttribute
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
/// Internaly, it expects POShipAddress as a POAddress type.
/// </summary>
/// <param name="SelectType">Must have type IBqlSelect. This select is used for both selecting <br />
/// a source Address record from which PO address is defaulted and for selecting default version of POAddress, <br />
/// created  from source Address (having  matching ContactID, revision and IsDefaultContact = true) <br />
/// if it exists - so it must include both records. See example above. <br />
/// </param>
public class POShipAddressAttribute(System.Type SelectType) : AddressAttribute(typeof (POShipAddress.addressID), typeof (POShipAddress.isDefaultAddress), SelectType)
{
  public override void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    PXGraph.FieldVerifyingEvents fieldVerifying = sender.Graph.FieldVerifying;
    POShipAddressAttribute addressAttribute = this;
    // ISSUE: virtual method pointer
    PXFieldVerifying pxFieldVerifying = new PXFieldVerifying((object) addressAttribute, __vmethodptr(addressAttribute, Record_Override_FieldVerifying));
    fieldVerifying.AddHandler<POShipAddress.overrideAddress>(pxFieldVerifying);
  }

  protected override void Record_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    base.Record_RowSelected(sender, e);
    if (e.Row == null)
      return;
    PXUIFieldAttribute.SetEnabled<POShipAddress.overrideAddress>(sender, e.Row, sender.AllowUpdate);
    PXUIFieldAttribute.SetEnabled<POShipAddress.isValidated>(sender, e.Row, false);
  }

  public override void DefaultRecord(PXCache sender, object DocumentRow, object Row)
  {
    this.DefaultAddress<POShipAddress, POShipAddress.addressID>(sender, DocumentRow, Row);
  }

  public override void CopyRecord(
    PXCache sender,
    object DocumentRow,
    object SourceRow,
    bool clone)
  {
    this.CopyAddress<POShipAddress, POShipAddress.addressID>(sender, DocumentRow, SourceRow, clone);
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
      this.Address_IsDefaultAddress_FieldVerifying<POShipAddress>(sender, e);
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

  public override void DefaultAddress<TAddress, TAddressID>(
    PXCache sender,
    object DocumentRow,
    object AddressRow)
  {
    int? nullable1 = (int?) sender.GetValue<POOrder.siteID>(DocumentRow);
    int? nullable2 = (int?) sender.GetValue<POOrder.projectID>(DocumentRow);
    string str = (string) sender.GetValue<POOrder.shipDestType>(DocumentRow);
    string shipDestination = (string) null;
    object[] objArray = (object[]) null;
    if (nullable1.HasValue && str == "S")
    {
      shipDestination = str;
      objArray = new object[1]{ (object) nullable1 };
    }
    if (nullable2.HasValue && str == "P")
    {
      shipDestination = str;
      objArray = new object[1]{ (object) nullable2 };
    }
    PXView addressView = this.CreateAddressView(sender, DocumentRow, shipDestination);
    int num1 = -1;
    int num2 = 0;
    bool flag = false;
    if (addressView.Select((object[]) null, objArray, (object[]) null, (string[]) null, (bool[]) null, (PXFilterRow[]) null, ref num1, 1, ref num2).Any<object>())
    {
      using (List<object>.Enumerator enumerator = addressView.Select((object[]) null, objArray, (object[]) null, (string[]) null, (bool[]) null, (PXFilterRow[]) null, ref num1, 1, ref num2).GetEnumerator())
      {
        if (enumerator.MoveNext())
        {
          PXResult current = (PXResult) enumerator.Current;
          if (shipDestination == "P")
          {
            PMSiteAddress source = PXResult.Unwrap<PMSiteAddress>((object) current);
            PX.Objects.CR.Address address = PropertyTransfer.Transfer<PMSiteAddress, PX.Objects.CR.Address>(source, new PX.Objects.CR.Address());
            address.AddressID = source.AddressID;
            flag = AddressAttribute.DefaultAddress<TAddress, TAddressID>(sender, this.FieldName, DocumentRow, AddressRow, (object) new PXResult<PX.Objects.CR.Address, POShipAddress>(address, new POShipAddress()));
          }
          else
            flag = AddressAttribute.DefaultAddress<TAddress, TAddressID>(sender, this.FieldName, DocumentRow, AddressRow, (object) current);
        }
      }
    }
    else if (shipDestination == "P")
      flag = AddressAttribute.DefaultAddress<TAddress, TAddressID>(sender, this.FieldName, DocumentRow, AddressRow, (object) new PXResult<PX.Objects.CR.Address, POShipAddress>(new PX.Objects.CR.Address()
      {
        RevisionID = new int?(0)
      }, new POShipAddress()));
    if (!flag && !this._Required)
      this.ClearRecord(sender, DocumentRow);
    if (!flag && this._Required && shipDestination == "S")
      throw new SharedRecordMissingException();
  }

  protected virtual PXView CreateAddressView(
    PXCache sender,
    object DocumentRow,
    string shipDestination)
  {
    switch (shipDestination)
    {
      case "S":
        BqlCommand instance1 = BqlCommand.CreateInstance(new System.Type[1]
        {
          typeof (SelectFromBase<PX.Objects.CR.Address, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<INSite>.On<KeysRelation<Field<INSite.addressID>.IsRelatedTo<PX.Objects.CR.Address.addressID>.AsSimpleKey.WithTablesOf<PX.Objects.CR.Address, INSite>, PX.Objects.CR.Address, INSite>.And<BqlOperand<INSite.siteID, IBqlInt>.IsEqual<P.AsInt>>>>, FbqlJoins.Left<POShipAddress>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<POShipAddress.bAccountID, Equal<PX.Objects.CR.Address.bAccountID>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<POShipAddress.bAccountAddressID, Equal<PX.Objects.CR.Address.addressID>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<POShipAddress.revisionID, Equal<PX.Objects.CR.Address.revisionID>>>>>.And<BqlOperand<POShipAddress.isDefaultAddress, IBqlBool>.IsEqual<boolTrue>>>>>>>.Where<BqlOperand<boolTrue, IBqlBool>.IsEqual<boolTrue>>)
        });
        return sender.Graph.TypedViews.GetView(instance1, false);
      case "P":
        BqlCommand instance2 = BqlCommand.CreateInstance(new System.Type[1]
        {
          typeof (SelectFromBase<PMSiteAddress, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PMProject>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMProject.siteAddressID, Equal<PMSiteAddress.addressID>>>>>.And<BqlOperand<PMProject.contractID, IBqlInt>.IsEqual<P.AsInt>>>>>.Where<BqlOperand<boolTrue, IBqlBool>.IsEqual<boolTrue>>)
        });
        return sender.Graph.TypedViews.GetView(instance2, false);
      default:
        return sender.Graph.TypedViews.GetView(this._Select, false);
    }
  }
}
