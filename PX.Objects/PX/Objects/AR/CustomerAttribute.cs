// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.CustomerAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.CR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

#nullable enable
namespace PX.Objects.AR;

[PXDBInt]
[PXUIField]
[Serializable]
public class CustomerAttribute : PXEntityAttribute
{
  public const 
  #nullable disable
  string DimensionName = "BIZACCT";
  public System.Type RestricionSource;
  private readonly System.Type[] _fields;
  protected string[] _HeaderList;
  protected string[] _FieldList;

  /// <summary>Default Ctor</summary>
  public CustomerAttribute()
    : this(typeof (Search<BAccountR.bAccountID>))
  {
  }

  /// <summary>Extended Ctor. User may define customised search.</summary>
  /// <param name="search">Must Be IBqlSearch, which returns BAccountID. Tables Customer,Contact, Address will be added automatically
  /// </param>
  public CustomerAttribute(System.Type search)
    : this(search, typeof (Customer.acctCD), typeof (Customer.acctName), typeof (PX.Objects.CR.Address.addressLine1), typeof (PX.Objects.CR.Address.addressLine2), typeof (PX.Objects.CR.Address.postalCode), typeof (CustomerAttribute.Contact.phone1), typeof (PX.Objects.CR.Address.city), typeof (PX.Objects.CR.Address.countryID), typeof (CustomerAttribute.Location.taxRegistrationID), typeof (Customer.curyID), typeof (CustomerAttribute.Contact.attention), typeof (Customer.customerClassID), typeof (Customer.status))
  {
  }

  public CustomerAttribute(System.Type search, params System.Type[] fields)
  {
    System.Type genericTypeDefinition = search.GetGenericTypeDefinition();
    System.Type[] genericArguments = search.GetGenericArguments();
    System.Type type1;
    if (genericTypeDefinition == typeof (Search<>))
      type1 = BqlCommand.Compose(new System.Type[15]
      {
        typeof (Search2<,,>),
        typeof (BAccountR.bAccountID),
        typeof (LeftJoin<,,>),
        typeof (Customer),
        typeof (On<Customer.bAccountID, Equal<BAccountR.bAccountID>, And<Match<Customer, Current<AccessInfo.userName>>>>),
        typeof (LeftJoin<,,>),
        typeof (CustomerAttribute.Contact),
        typeof (On<CustomerAttribute.Contact.bAccountID, Equal<BAccountR.bAccountID>, And<CustomerAttribute.Contact.contactID, Equal<BAccountR.defContactID>>>),
        typeof (LeftJoin<,,>),
        typeof (PX.Objects.CR.Address),
        typeof (On<PX.Objects.CR.Address.bAccountID, Equal<BAccountR.bAccountID>, And<PX.Objects.CR.Address.addressID, Equal<BAccountR.defAddressID>>>),
        typeof (LeftJoin<,>),
        typeof (CustomerAttribute.Location),
        typeof (On<CustomerAttribute.Location.bAccountID, Equal<BAccountR.bAccountID>, And<CustomerAttribute.Location.locationID, Equal<BAccountR.defLocationID>>>),
        typeof (Where<Customer.bAccountID, IsNotNull>)
      });
    else if (genericTypeDefinition == typeof (Search<,>))
      type1 = BqlCommand.Compose(new System.Type[15]
      {
        typeof (Search2<,,>),
        typeof (BAccountR.bAccountID),
        typeof (LeftJoin<,,>),
        typeof (Customer),
        typeof (On<Customer.bAccountID, Equal<BAccountR.bAccountID>, And<Match<Customer, Current<AccessInfo.userName>>>>),
        typeof (LeftJoin<,,>),
        typeof (CustomerAttribute.Contact),
        typeof (On<CustomerAttribute.Contact.bAccountID, Equal<BAccountR.bAccountID>, And<CustomerAttribute.Contact.contactID, Equal<BAccountR.defContactID>>>),
        typeof (LeftJoin<,,>),
        typeof (PX.Objects.CR.Address),
        typeof (On<PX.Objects.CR.Address.bAccountID, Equal<BAccountR.bAccountID>, And<PX.Objects.CR.Address.addressID, Equal<BAccountR.defAddressID>>>),
        typeof (LeftJoin<,>),
        typeof (CustomerAttribute.Location),
        typeof (On<CustomerAttribute.Location.bAccountID, Equal<BAccountR.bAccountID>, And<CustomerAttribute.Location.locationID, Equal<BAccountR.defLocationID>>>),
        genericArguments[1]
      });
    else if (genericTypeDefinition == typeof (Search<,,>))
      type1 = BqlCommand.Compose(new System.Type[16 /*0x10*/]
      {
        typeof (Search2<,,>),
        typeof (BAccountR.bAccountID),
        typeof (LeftJoin<,,>),
        typeof (Customer),
        typeof (On<Customer.bAccountID, Equal<BAccountR.bAccountID>, And<Match<Customer, Current<AccessInfo.userName>>>>),
        typeof (LeftJoin<,,>),
        typeof (CustomerAttribute.Contact),
        typeof (On<CustomerAttribute.Contact.bAccountID, Equal<BAccountR.bAccountID>, And<CustomerAttribute.Contact.contactID, Equal<BAccountR.defContactID>>>),
        typeof (LeftJoin<,,>),
        typeof (PX.Objects.CR.Address),
        typeof (On<PX.Objects.CR.Address.bAccountID, Equal<BAccountR.bAccountID>, And<PX.Objects.CR.Address.addressID, Equal<BAccountR.defAddressID>>>),
        typeof (LeftJoin<,>),
        typeof (CustomerAttribute.Location),
        typeof (On<CustomerAttribute.Location.bAccountID, Equal<BAccountR.bAccountID>, And<CustomerAttribute.Location.locationID, Equal<BAccountR.defLocationID>>>),
        genericArguments[1],
        genericArguments[2]
      });
    else if (genericTypeDefinition == typeof (Search2<,>))
      type1 = BqlCommand.Compose(new System.Type[16 /*0x10*/]
      {
        typeof (Search2<,,>),
        typeof (BAccountR.bAccountID),
        typeof (LeftJoin<,,>),
        typeof (Customer),
        typeof (On<Customer.bAccountID, Equal<BAccountR.bAccountID>, And<Match<Customer, Current<AccessInfo.userName>>>>),
        typeof (LeftJoin<,,>),
        typeof (CustomerAttribute.Contact),
        typeof (On<CustomerAttribute.Contact.bAccountID, Equal<BAccountR.bAccountID>, And<CustomerAttribute.Contact.contactID, Equal<BAccountR.defContactID>>>),
        typeof (LeftJoin<,,>),
        typeof (PX.Objects.CR.Address),
        typeof (On<PX.Objects.CR.Address.bAccountID, Equal<BAccountR.bAccountID>, And<PX.Objects.CR.Address.addressID, Equal<BAccountR.defAddressID>>>),
        typeof (LeftJoin<,,>),
        typeof (CustomerAttribute.Location),
        typeof (On<CustomerAttribute.Location.bAccountID, Equal<BAccountR.bAccountID>, And<CustomerAttribute.Location.locationID, Equal<BAccountR.defLocationID>>>),
        genericArguments[1],
        typeof (Where<Customer.bAccountID, IsNotNull>)
      });
    else if (genericTypeDefinition == typeof (Search2<,,>))
    {
      type1 = BqlCommand.Compose(new System.Type[16 /*0x10*/]
      {
        typeof (Search2<,,>),
        typeof (BAccountR.bAccountID),
        typeof (LeftJoin<,,>),
        typeof (Customer),
        typeof (On<Customer.bAccountID, Equal<BAccountR.bAccountID>, And<Match<Customer, Current<AccessInfo.userName>>>>),
        typeof (LeftJoin<,,>),
        typeof (CustomerAttribute.Contact),
        typeof (On<CustomerAttribute.Contact.bAccountID, Equal<BAccountR.bAccountID>, And<CustomerAttribute.Contact.contactID, Equal<BAccountR.defContactID>>>),
        typeof (LeftJoin<,,>),
        typeof (PX.Objects.CR.Address),
        typeof (On<PX.Objects.CR.Address.bAccountID, Equal<BAccountR.bAccountID>, And<PX.Objects.CR.Address.addressID, Equal<BAccountR.defAddressID>>>),
        typeof (LeftJoin<,,>),
        typeof (CustomerAttribute.Location),
        typeof (On<CustomerAttribute.Location.bAccountID, Equal<BAccountR.bAccountID>, And<CustomerAttribute.Location.locationID, Equal<BAccountR.defLocationID>>>),
        genericArguments[1],
        genericArguments[2]
      });
    }
    else
    {
      if (!(genericTypeDefinition == typeof (Search2<,,,>)))
        throw new PXArgumentException(nameof (search), "An invalid argument has been specified.");
      type1 = BqlCommand.Compose(new System.Type[17]
      {
        typeof (Search2<,,>),
        typeof (BAccountR.bAccountID),
        typeof (LeftJoin<,,>),
        typeof (Customer),
        typeof (On<Customer.bAccountID, Equal<BAccountR.bAccountID>, And<Match<Customer, Current<AccessInfo.userName>>>>),
        typeof (LeftJoin<,,>),
        typeof (CustomerAttribute.Contact),
        typeof (On<CustomerAttribute.Contact.bAccountID, Equal<BAccountR.bAccountID>, And<CustomerAttribute.Contact.contactID, Equal<BAccountR.defContactID>>>),
        typeof (LeftJoin<,,>),
        typeof (PX.Objects.CR.Address),
        typeof (On<PX.Objects.CR.Address.bAccountID, Equal<BAccountR.bAccountID>, And<PX.Objects.CR.Address.addressID, Equal<BAccountR.defAddressID>>>),
        typeof (LeftJoin<,,>),
        typeof (CustomerAttribute.Location),
        typeof (On<CustomerAttribute.Location.bAccountID, Equal<BAccountR.bAccountID>, And<CustomerAttribute.Location.locationID, Equal<BAccountR.defLocationID>>>),
        genericArguments[1],
        genericArguments[2],
        genericArguments[3]
      });
    }
    PXAggregateAttribute.AggregatedAttributesCollection attributes = ((PXAggregateAttribute) this)._Attributes;
    System.Type type2 = type1;
    System.Type type3 = typeof (BAccountR.acctCD);
    System.Type[] typeArray = new System.Type[7]
    {
      typeof (BAccountR.acctCD),
      typeof (Customer.acctName),
      typeof (Customer.customerClassID),
      typeof (Customer.status),
      typeof (CustomerAttribute.Contact.phone1),
      typeof (PX.Objects.CR.Address.city),
      typeof (PX.Objects.CR.Address.countryID)
    };
    PXDimensionSelectorAttribute selectorAttribute1;
    PXDimensionSelectorAttribute selectorAttribute2 = selectorAttribute1 = new PXDimensionSelectorAttribute("BIZACCT", type2, type3, typeArray);
    attributes.Add((PXEventSubscriberAttribute) selectorAttribute1);
    selectorAttribute2.DescriptionField = typeof (Customer.acctName);
    selectorAttribute2.CacheGlobal = true;
    selectorAttribute2.FilterEntity = typeof (Customer);
    this._SelAttrIndex = ((List<PXEventSubscriberAttribute>) ((PXAggregateAttribute) this)._Attributes).Count - 1;
    this.Filterable = true;
    this._fields = fields;
  }

  public virtual void CacheAttached(PXCache sender)
  {
    this.EmitColumnForCustomerField(sender);
    ((PXAggregateAttribute) this).CacheAttached(sender);
    string lower = ((PXEventSubscriberAttribute) this)._FieldName.ToLower();
    PXGraph.FieldSelectingEvents fieldSelecting1 = sender.Graph.FieldSelecting;
    System.Type itemType1 = sender.GetItemType();
    string str1 = lower;
    PXDimensionSelectorAttribute attribute = ((PXAggregateAttribute) this).GetAttribute<PXDimensionSelectorAttribute>();
    // ISSUE: virtual method pointer
    PXFieldSelecting pxFieldSelecting1 = new PXFieldSelecting((object) attribute, __vmethodptr(attribute, FieldSelecting));
    fieldSelecting1.RemoveHandler(itemType1, str1, pxFieldSelecting1);
    PXGraph.FieldSelectingEvents fieldSelecting2 = sender.Graph.FieldSelecting;
    System.Type itemType2 = sender.GetItemType();
    string str2 = lower;
    CustomerAttribute customerAttribute1 = this;
    // ISSUE: virtual method pointer
    PXFieldSelecting pxFieldSelecting2 = new PXFieldSelecting((object) customerAttribute1, __vmethodptr(customerAttribute1, FieldSelecting));
    fieldSelecting2.AddHandler(itemType2, str2, pxFieldSelecting2);
    PXGraph.RowSelectingEvents rowSelecting = sender.Graph.RowSelecting;
    System.Type type1 = typeof (BAccountR);
    CustomerAttribute customerAttribute2 = this;
    // ISSUE: virtual method pointer
    PXRowSelecting pxRowSelecting = new PXRowSelecting((object) customerAttribute2, __vmethodptr(customerAttribute2, BAccountR_Customer_RowSelecting));
    rowSelecting.AddHandler(type1, pxRowSelecting);
    PXGraph.FieldDefaultingEvents fieldDefaulting = sender.Graph.FieldDefaulting;
    System.Type type2 = typeof (BAccountR);
    CustomerAttribute customerAttribute3 = this;
    // ISSUE: virtual method pointer
    PXFieldDefaulting pxFieldDefaulting = new PXFieldDefaulting((object) customerAttribute3, __vmethodptr(customerAttribute3, BAccountR_viewInAr_FieldDefaulting));
    fieldDefaulting.AddHandler(type2, "viewInAr", pxFieldDefaulting);
  }

  protected virtual void BAccountR_Customer_RowSelecting(PXCache sender, PXRowSelectingEventArgs e)
  {
    sender.SetValue(e.Row, "viewInAr", (object) true);
    sender.SetValue(e.Row, "viewInAp", (object) false);
  }

  protected virtual void BAccountR_viewInAr_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    e.NewValue = (object) true;
  }

  protected virtual void PopulateFields(PXCache sender)
  {
    if (this._FieldList == null)
    {
      this._FieldList = new string[this._fields.Length];
      System.Type[] typeArray = new System.Type[this._fields.Length];
      for (int index = 0; index < this._fields.Length; ++index)
      {
        System.Type itemType = BqlCommand.GetItemType(this._fields[index]);
        typeArray[index] = itemType;
        sender.Graph.Caches.AddCacheMapping(itemType, itemType);
        this._FieldList[index] = itemType.IsAssignableFrom(typeof (BAccountR)) || this._fields[index].Name == typeof (BAccountR.acctCD).Name || this._fields[index].Name == typeof (BAccountR.acctName).Name ? this._fields[index].Name : $"{itemType.Name}__{this._fields[index].Name}";
      }
      string str = nameof (PopulateFields);
      object[] objArray = new object[3]
      {
        (object) EnumerableExtensions.GetHashCodeOfSequence((IEnumerable) this._FieldList),
        (object) sender.GetItemType(),
        (object) Thread.CurrentThread.CurrentCulture.Name
      };
      if (!sender.Graph.Prototype.TryGetValue<string[]>(ref this._HeaderList, (object) str, objArray))
      {
        this._HeaderList = new string[this._fields.Length];
        for (int index = 0; index < this._FieldList.Length; ++index)
        {
          PXCache cach = sender.Graph.Caches[typeArray[index]];
          this._HeaderList[index] = PXUIFieldAttribute.GetDisplayName(cach, this._fields[index].Name);
        }
        sender.Graph.Prototype.SetValue<string[]>(this._HeaderList, (object) str, objArray);
      }
    }
    ((PXAggregateAttribute) ((PXAggregateAttribute) this).GetAttribute<PXDimensionSelectorAttribute>()).GetAttribute<PXSelectorAttribute>().SetColumns(this._FieldList, this._HeaderList);
  }

  public virtual void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    if (((PXEventSubscriberAttribute) this).AttributeLevel == 2 || e.IsAltered)
      this.PopulateFields(sender);
    PXDimensionSelectorAttribute attribute = ((PXAggregateAttribute) this).GetAttribute<PXDimensionSelectorAttribute>();
    // ISSUE: virtual method pointer
    new PXFieldSelecting((object) attribute, __vmethodptr(attribute, FieldSelecting)).Invoke(sender, e);
  }

  protected void EmitColumnForCustomerField(PXCache sender)
  {
    if (this.DescriptionField == (System.Type) null)
      return;
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    CustomerAttribute.\u003C\u003Ec__DisplayClass15_0 cDisplayClass150 = new CustomerAttribute.\u003C\u003Ec__DisplayClass15_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass150.\u003C\u003E4__this = this;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass150.alias = $"{((PXEventSubscriberAttribute) this)._FieldName}_{typeof (Customer).Name}_{this.DescriptionField.Name}";
    // ISSUE: reference to a compiler-generated field
    if (!sender.Fields.Contains(cDisplayClass150.alias))
    {
      // ISSUE: reference to a compiler-generated field
      sender.Fields.Add(cDisplayClass150.alias);
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method pointer
      sender.Graph.FieldSelecting.AddHandler(sender.GetItemType(), cDisplayClass150.alias, new PXFieldSelecting((object) cDisplayClass150, __methodptr(\u003CEmitColumnForCustomerField\u003Eb__0)));
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method pointer
      sender.Graph.CommandPreparing.AddHandler(sender.GetItemType(), cDisplayClass150.alias, new PXCommandPreparing((object) this, __methodptr(\u003CEmitColumnForCustomerField\u003Eb__15_1)));
    }
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    CustomerAttribute.\u003C\u003Ec__DisplayClass15_1 cDisplayClass151 = new CustomerAttribute.\u003C\u003Ec__DisplayClass15_1();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass151.\u003C\u003E4__this = this;
    // ISSUE: reference to a compiler-generated field
    cDisplayClass151.alias = ((PXEventSubscriberAttribute) this)._FieldName + "_description";
    // ISSUE: reference to a compiler-generated field
    if (sender.Fields.Contains(cDisplayClass151.alias))
      return;
    // ISSUE: reference to a compiler-generated field
    sender.Fields.Add(cDisplayClass151.alias);
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    sender.Graph.FieldSelecting.AddHandler(sender.GetItemType(), cDisplayClass151.alias, new PXFieldSelecting((object) cDisplayClass151, __methodptr(\u003CEmitColumnForCustomerField\u003Eb__2)));
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    sender.Graph.CommandPreparing.AddHandler(sender.GetItemType(), cDisplayClass151.alias, new PXCommandPreparing((object) this, __methodptr(\u003CEmitColumnForCustomerField\u003Eb__15_3)));
  }

  [PXHidden]
  [Serializable]
  public class Location : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected int? _BAccountID;
    protected int? _LocationID;
    protected string _LocationCD;
    protected string _TaxRegistrationID;
    protected short? _VLeadTime;
    protected string _VCarrierID;

    [PXDBInt(IsKey = true)]
    public virtual int? BAccountID
    {
      get => this._BAccountID;
      set => this._BAccountID = value;
    }

    [PXDBIdentity]
    public virtual int? LocationID
    {
      get => this._LocationID;
      set => this._LocationID = value;
    }

    [PXDBString(IsKey = true, IsUnicode = true)]
    public virtual string LocationCD
    {
      get => this._LocationCD;
      set => this._LocationCD = value;
    }

    [PXDBString(50, IsUnicode = true)]
    [PXUIField(DisplayName = "Tax Registration ID")]
    public virtual string TaxRegistrationID
    {
      get => this._TaxRegistrationID;
      set => this._TaxRegistrationID = value;
    }

    [PXDBString(10, IsUnicode = true)]
    [PXUIField(DisplayName = "Shipping Terms")]
    public virtual string VShipTermsID { get; set; }

    [PXDBShort(MinValue = 0, MaxValue = 100000)]
    [PXUIField(DisplayName = "Lead Time (Days)")]
    public virtual short? VLeadTime
    {
      get => this._VLeadTime;
      set => this._VLeadTime = value;
    }

    [PXDBString(15, IsUnicode = true, InputMask = ">aaaaaaaaaaaaaaa")]
    [PXUIField(DisplayName = "Ship Via")]
    public virtual string VCarrierID
    {
      get => this._VCarrierID;
      set => this._VCarrierID = value;
    }

    public abstract class bAccountID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      CustomerAttribute.Location.bAccountID>
    {
    }

    public abstract class locationID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      CustomerAttribute.Location.locationID>
    {
    }

    public abstract class locationCD : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      CustomerAttribute.Location.locationCD>
    {
    }

    public abstract class taxRegistrationID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      CustomerAttribute.Location.taxRegistrationID>
    {
    }

    public abstract class vShipTermsID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      CustomerAttribute.Location.vShipTermsID>
    {
    }

    public abstract class vLeadTime : 
      BqlType<
      #nullable enable
      IBqlShort, short>.Field<
      #nullable disable
      CustomerAttribute.Location.vLeadTime>
    {
    }

    public abstract class vCarrierID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      CustomerAttribute.Location.vCarrierID>
    {
    }
  }

  [PXHidden(ServiceVisible = true)]
  [Serializable]
  public class Contact : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXDBInt]
    [CRContactBAccountDefault]
    public virtual int? BAccountID { get; set; }

    [PXDBIdentity(IsKey = true)]
    [PXUIField]
    public virtual int? ContactID { get; set; }

    [PXDBInt]
    [PXDefault(0)]
    [AddressRevisionID]
    public virtual int? RevisionID { get; set; }

    [PXDBString(50, IsUnicode = true)]
    [Titles]
    [PXUIField(DisplayName = "Title")]
    public virtual string Title { get; set; }

    [PXDBString(50, IsUnicode = true)]
    [PXUIField(DisplayName = "First Name")]
    public virtual string FirstName { get; set; }

    [PXDBString(50, IsUnicode = true)]
    [PXUIField(DisplayName = "Middle Name")]
    public virtual string MidName { get; set; }

    [PXDBString(100, IsUnicode = true)]
    [PXUIField(DisplayName = "Last Name")]
    [CRLastNameDefault]
    public virtual string LastName { get; set; }

    [PXDBString(255 /*0xFF*/, IsUnicode = true)]
    [PXUIField(DisplayName = "Job Title")]
    [PXPersonalDataField]
    public virtual string Salutation { get; set; }

    [PXDBString(255 /*0xFF*/, IsUnicode = true)]
    [PXUIField]
    [PXPersonalDataField]
    public virtual string Attention { get; set; }

    [PXDBString(50)]
    [PXUIField]
    [PhoneValidation]
    public virtual string Phone1 { get; set; }

    [PXDBString(3)]
    [PXDefault("B1")]
    [PXUIField(DisplayName = "Phone 1")]
    [PhoneTypes]
    public virtual string Phone1Type { get; set; }

    [PXDBString(50)]
    [PXUIField(DisplayName = "Phone 2")]
    [PhoneValidation]
    public virtual string Phone2 { get; set; }

    [PXDBString(3)]
    [PXDefault("B2")]
    [PXUIField(DisplayName = "Phone 2")]
    [PhoneTypes]
    public virtual string Phone2Type { get; set; }

    [PXDBString(50)]
    [PXUIField(DisplayName = "Phone 3")]
    [PhoneValidation]
    public virtual string Phone3 { get; set; }

    [PXDBString(3)]
    [PXDefault("H1")]
    [PXUIField(DisplayName = "Phone 3")]
    [PhoneTypes]
    public virtual string Phone3Type { get; set; }

    [PXDBWeblink]
    [PXUIField(DisplayName = "Web")]
    public virtual string WebSite { get; set; }

    [PXDBString(50)]
    [PXUIField(DisplayName = "Fax")]
    [PhoneValidation]
    public virtual string Fax { get; set; }

    [PXDBEmail]
    [PXUIField]
    public virtual string EMail { get; set; }

    public abstract class bAccountID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      CustomerAttribute.Contact.bAccountID>
    {
    }

    public abstract class contactID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      CustomerAttribute.Contact.contactID>
    {
    }

    public abstract class revisionID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      CustomerAttribute.Contact.revisionID>
    {
    }

    public abstract class title : BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CustomerAttribute.Contact.title>
    {
    }

    public abstract class firstName : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      CustomerAttribute.Contact.firstName>
    {
    }

    public abstract class midName : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      CustomerAttribute.Contact.midName>
    {
    }

    public abstract class lastName : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      CustomerAttribute.Contact.lastName>
    {
    }

    public abstract class salutation : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      CustomerAttribute.Contact.salutation>
    {
    }

    public abstract class attention : IBqlField, IBqlOperand
    {
    }

    public abstract class phone1 : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      CustomerAttribute.Contact.phone1>
    {
    }

    public abstract class phone1Type : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      CustomerAttribute.Contact.phone1Type>
    {
    }

    public abstract class phone2 : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      CustomerAttribute.Contact.phone2>
    {
    }

    public abstract class phone2Type : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      CustomerAttribute.Contact.phone2Type>
    {
    }

    public abstract class phone3 : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      CustomerAttribute.Contact.phone3>
    {
    }

    public abstract class phone3Type : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      CustomerAttribute.Contact.phone3Type>
    {
    }

    public abstract class webSite : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      CustomerAttribute.Contact.webSite>
    {
    }

    public abstract class fax : BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CustomerAttribute.Contact.fax>
    {
    }

    public abstract class eMail : BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CustomerAttribute.Contact.eMail>
    {
    }
  }
}
