// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.CustomerRawAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.AR;

/// <summary>
/// Provides a UI Selector for Customers AcctCD as a string. <br />
/// Should be used where the definition of the AccountCD is needed - mostly, in a Customer DAC class.<br />
/// Properties of the selector - mask, length of the key, etc.<br />
/// are defined in the Dimension with predefined name "CUSTOMER".<br />
/// By default, search uses the following tables (linked) BAccount, Customer (strict), Contact, Address (optional).<br />
/// List of the Customers is filtered based on the user's access rights.<br />
/// Default column's list in the Selector - Customer.acctCD, Customer.acctName,<br />
/// Customer.customerClassID, Customer.status, Contact.phone1, Address.city, Address.countryID, Contact.EMail<br />
/// List of properties - inherited from PXEntityAttribute <br />
/// <example>
/// [CustomerRaw(IsKey = true)]
/// </example>
/// </summary>
[PXDBString(30, IsUnicode = true, InputMask = "", PadSpaced = true)]
[PXUIField]
public sealed class CustomerRawAttribute : PXEntityAttribute
{
  public const string DimensionName = "CUSTOMER";

  public CustomerRawAttribute()
    : this(typeof (Search2<Customer.acctCD, LeftJoin<PX.Objects.CR.Contact, On<PX.Objects.CR.Contact.bAccountID, Equal<Customer.bAccountID>, And<PX.Objects.CR.Contact.contactID, Equal<Customer.defContactID>>>, LeftJoin<PX.Objects.CR.Address, On<PX.Objects.CR.Address.bAccountID, Equal<Customer.bAccountID>, And<PX.Objects.CR.Address.addressID, Equal<Customer.defAddressID>>>>>, Where<Match<Current<AccessInfo.userName>>>>))
  {
  }

  public CustomerRawAttribute(System.Type SearchType)
  {
    ((PXAggregateAttribute) this)._Attributes.Add((PXEventSubscriberAttribute) new PXDimensionSelectorAttribute("CUSTOMER", SearchType, typeof (Customer.acctCD), new System.Type[8]
    {
      typeof (Customer.acctCD),
      typeof (Customer.acctName),
      typeof (Customer.customerClassID),
      typeof (Customer.status),
      typeof (PX.Objects.CR.Contact.phone1),
      typeof (PX.Objects.CR.Address.city),
      typeof (PX.Objects.CR.Address.countryID),
      typeof (PX.Objects.CR.Contact.eMail)
    }));
    this._SelAttrIndex = ((List<PXEventSubscriberAttribute>) ((PXAggregateAttribute) this)._Attributes).Count - 1;
    this.DescriptionField = typeof (Customer.acctName);
    ((PXDimensionSelectorAttribute) ((PXAggregateAttribute) this)._Attributes[this._SelAttrIndex]).CacheGlobal = true;
    this.Filterable = true;
  }
}
