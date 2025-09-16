// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.VendorAttribute
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
using System.Threading;

#nullable enable
namespace PX.Objects.AP;

[PXDBInt]
[PXUIField(DisplayName = "Vendor", Visibility = PXUIVisibility.Visible)]
[Serializable]
public class VendorAttribute : PXEntityAttribute
{
  public const 
  #nullable disable
  string DimensionName = "VENDOR";
  private readonly System.Type[] _fields;
  protected string[] _HeaderList;
  protected string[] _FieldList;

  public VendorAttribute()
    : this(typeof (Search<BAccountR.bAccountID>))
  {
  }

  public VendorAttribute(System.Type search)
    : this(search, typeof (Vendor.acctCD), typeof (Vendor.acctName), typeof (PX.Objects.CR.Address.addressLine1), typeof (PX.Objects.CR.Address.addressLine2), typeof (PX.Objects.CR.Address.postalCode), typeof (VendorAttribute.Contact.phone1), typeof (PX.Objects.CR.Address.city), typeof (PX.Objects.CR.Address.countryID), typeof (VendorAttribute.Location.taxRegistrationID), typeof (Vendor.curyID), typeof (VendorAttribute.Contact.attention), typeof (Vendor.vendorClassID), typeof (Vendor.vStatus))
  {
  }

  public VendorAttribute(System.Type search, params System.Type[] fields)
  {
    System.Type genericTypeDefinition = search.GetGenericTypeDefinition();
    System.Type[] genericArguments = search.GetGenericArguments();
    System.Type type;
    if (genericTypeDefinition == typeof (Search<>))
      type = BqlCommand.Compose(typeof (Search2<,,>), typeof (BAccountR.bAccountID), typeof (LeftJoin<,,>), typeof (Vendor), typeof (On<Vendor.bAccountID, Equal<BAccountR.bAccountID>, PX.Data.And<PX.Data.Match<Vendor, Current<AccessInfo.userName>>>>), typeof (LeftJoin<,,>), typeof (VendorAttribute.Contact), typeof (On<VendorAttribute.Contact.bAccountID, Equal<BAccountR.bAccountID>, And<VendorAttribute.Contact.contactID, Equal<BAccountR.defContactID>>>), typeof (LeftJoin<,,>), typeof (PX.Objects.CR.Address), typeof (On<PX.Objects.CR.Address.bAccountID, Equal<BAccountR.bAccountID>, And<PX.Objects.CR.Address.addressID, Equal<BAccountR.defAddressID>>>), typeof (LeftJoin<,>), typeof (VendorAttribute.Location), typeof (On<VendorAttribute.Location.bAccountID, Equal<BAccountR.bAccountID>, And<VendorAttribute.Location.locationID, Equal<BAccountR.defLocationID>>>), typeof (Where<Vendor.bAccountID, PX.Data.IsNotNull>));
    else if (genericTypeDefinition == typeof (Search<,>))
      type = BqlCommand.Compose(typeof (Search2<,,>), typeof (BAccountR.bAccountID), typeof (LeftJoin<,,>), typeof (Vendor), typeof (On<Vendor.bAccountID, Equal<BAccountR.bAccountID>, PX.Data.And<PX.Data.Match<Vendor, Current<AccessInfo.userName>>>>), typeof (LeftJoin<,,>), typeof (VendorAttribute.Contact), typeof (On<VendorAttribute.Contact.bAccountID, Equal<BAccountR.bAccountID>, And<VendorAttribute.Contact.contactID, Equal<BAccountR.defContactID>>>), typeof (LeftJoin<,,>), typeof (PX.Objects.CR.Address), typeof (On<PX.Objects.CR.Address.bAccountID, Equal<BAccountR.bAccountID>, And<PX.Objects.CR.Address.addressID, Equal<BAccountR.defAddressID>>>), typeof (LeftJoin<,>), typeof (VendorAttribute.Location), typeof (On<VendorAttribute.Location.bAccountID, Equal<BAccountR.bAccountID>, And<VendorAttribute.Location.locationID, Equal<BAccountR.defLocationID>>>), genericArguments[1]);
    else if (genericTypeDefinition == typeof (Search<,,>))
      type = BqlCommand.Compose(typeof (Search2<,,>), typeof (BAccountR.bAccountID), typeof (LeftJoin<,,>), typeof (Vendor), typeof (On<Vendor.bAccountID, Equal<BAccountR.bAccountID>, PX.Data.And<PX.Data.Match<Vendor, Current<AccessInfo.userName>>>>), typeof (LeftJoin<,,>), typeof (VendorAttribute.Contact), typeof (On<VendorAttribute.Contact.bAccountID, Equal<BAccountR.bAccountID>, And<VendorAttribute.Contact.contactID, Equal<BAccountR.defContactID>>>), typeof (LeftJoin<,,>), typeof (PX.Objects.CR.Address), typeof (On<PX.Objects.CR.Address.bAccountID, Equal<BAccountR.bAccountID>, And<PX.Objects.CR.Address.addressID, Equal<BAccountR.defAddressID>>>), typeof (LeftJoin<,>), typeof (VendorAttribute.Location), typeof (On<VendorAttribute.Location.bAccountID, Equal<BAccountR.bAccountID>, And<VendorAttribute.Location.locationID, Equal<BAccountR.defLocationID>>>), genericArguments[1], genericArguments[2]);
    else if (genericTypeDefinition == typeof (Search2<,>))
      type = BqlCommand.Compose(typeof (Search2<,,>), typeof (BAccountR.bAccountID), typeof (LeftJoin<,,>), typeof (Vendor), typeof (On<Vendor.bAccountID, Equal<BAccountR.bAccountID>, PX.Data.And<PX.Data.Match<Vendor, Current<AccessInfo.userName>>>>), typeof (LeftJoin<,,>), typeof (VendorAttribute.Contact), typeof (On<VendorAttribute.Contact.bAccountID, Equal<BAccountR.bAccountID>, And<VendorAttribute.Contact.contactID, Equal<BAccountR.defContactID>>>), typeof (LeftJoin<,,>), typeof (PX.Objects.CR.Address), typeof (On<PX.Objects.CR.Address.bAccountID, Equal<BAccountR.bAccountID>, And<PX.Objects.CR.Address.addressID, Equal<BAccountR.defAddressID>>>), typeof (LeftJoin<,,>), typeof (VendorAttribute.Location), typeof (On<VendorAttribute.Location.bAccountID, Equal<BAccountR.bAccountID>, And<VendorAttribute.Location.locationID, Equal<BAccountR.defLocationID>>>), genericArguments[1], typeof (Where<Vendor.bAccountID, PX.Data.IsNotNull>));
    else if (genericTypeDefinition == typeof (Search2<,,>))
    {
      type = BqlCommand.Compose(typeof (Search2<,,>), typeof (BAccountR.bAccountID), typeof (LeftJoin<,,>), typeof (Vendor), typeof (On<Vendor.bAccountID, Equal<BAccountR.bAccountID>, PX.Data.And<PX.Data.Match<Vendor, Current<AccessInfo.userName>>>>), typeof (LeftJoin<,,>), typeof (VendorAttribute.Contact), typeof (On<VendorAttribute.Contact.bAccountID, Equal<BAccountR.bAccountID>, And<VendorAttribute.Contact.contactID, Equal<BAccountR.defContactID>>>), typeof (LeftJoin<,,>), typeof (PX.Objects.CR.Address), typeof (On<PX.Objects.CR.Address.bAccountID, Equal<BAccountR.bAccountID>, And<PX.Objects.CR.Address.addressID, Equal<BAccountR.defAddressID>>>), typeof (LeftJoin<,,>), typeof (VendorAttribute.Location), typeof (On<VendorAttribute.Location.bAccountID, Equal<BAccountR.bAccountID>, And<VendorAttribute.Location.locationID, Equal<BAccountR.defLocationID>>>), genericArguments[1], genericArguments[2]);
    }
    else
    {
      if (!(genericTypeDefinition == typeof (Search2<,,,>)))
        throw new PXArgumentException(nameof (search), "An invalid argument has been specified.");
      type = BqlCommand.Compose(typeof (Search2<,,>), typeof (BAccountR.bAccountID), typeof (LeftJoin<,,>), typeof (Vendor), typeof (On<Vendor.bAccountID, Equal<BAccountR.bAccountID>, PX.Data.And<PX.Data.Match<Vendor, Current<AccessInfo.userName>>>>), typeof (LeftJoin<,,>), typeof (VendorAttribute.Contact), typeof (On<VendorAttribute.Contact.bAccountID, Equal<BAccountR.bAccountID>, And<VendorAttribute.Contact.contactID, Equal<BAccountR.defContactID>>>), typeof (LeftJoin<,,>), typeof (PX.Objects.CR.Address), typeof (On<PX.Objects.CR.Address.bAccountID, Equal<BAccountR.bAccountID>, And<PX.Objects.CR.Address.addressID, Equal<BAccountR.defAddressID>>>), typeof (LeftJoin<,,>), typeof (VendorAttribute.Location), typeof (On<VendorAttribute.Location.bAccountID, Equal<BAccountR.bAccountID>, And<VendorAttribute.Location.locationID, Equal<BAccountR.defLocationID>>>), genericArguments[1], genericArguments[2], genericArguments[3]);
    }
    this._fields = fields;
    PXDimensionSelectorAttribute selectorAttribute;
    this._Attributes.Add((PXEventSubscriberAttribute) (selectorAttribute = new PXDimensionSelectorAttribute("VENDOR", type, typeof (BAccountR.acctCD), fields)));
    selectorAttribute.DescriptionField = typeof (Vendor.acctName);
    selectorAttribute.CacheGlobal = true;
    selectorAttribute.FilterEntity = typeof (Vendor);
    this._SelAttrIndex = this._Attributes.Count - 1;
    this.Filterable = true;
  }

  public override void CacheAttached(PXCache sender)
  {
    this.EmitColumnForVendorField(sender);
    base.CacheAttached(sender);
    string lower = this._FieldName.ToLower();
    sender.Graph.FieldSelecting.RemoveHandler(sender.GetItemType(), lower, new PXFieldSelecting(this.GetAttribute<PXDimensionSelectorAttribute>().FieldSelecting));
    sender.Graph.FieldSelecting.AddHandler(sender.GetItemType(), lower, new PXFieldSelecting(this.FieldSelecting));
    sender.Graph.RowSelecting.AddHandler(typeof (BAccountR), new PXRowSelecting(this.BAccountR_Vendor_RowSelecting));
    sender.Graph.FieldDefaulting.AddHandler(typeof (BAccountR), "viewInAp", new PXFieldDefaulting(this.BAccountR_viewInAp_FieldDefaulting));
  }

  protected virtual void BAccountR_Vendor_RowSelecting(PXCache sender, PXRowSelectingEventArgs e)
  {
    sender.SetValue(e.Row, "viewInAp", (object) true);
    sender.SetValue(e.Row, "viewInAr", (object) false);
  }

  protected virtual void BAccountR_viewInAp_FieldDefaulting(
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
      string key = nameof (PopulateFields);
      object[] objArray = new object[3]
      {
        (object) EnumerableExtensions.GetHashCodeOfSequence((IEnumerable) this._FieldList),
        (object) sender.GetItemType(),
        (object) Thread.CurrentThread.CurrentCulture.Name
      };
      if (!sender.Graph.Prototype.TryGetValue<string[]>(out this._HeaderList, (object) key, objArray))
      {
        this._HeaderList = new string[this._fields.Length];
        for (int index = 0; index < this._FieldList.Length; ++index)
        {
          PXCache cach = sender.Graph.Caches[typeArray[index]];
          this._HeaderList[index] = PXUIFieldAttribute.GetDisplayName(cach, this._fields[index].Name);
        }
        sender.Graph.Prototype.SetValue<string[]>(this._HeaderList, (object) key, objArray);
      }
    }
    this.GetAttribute<PXDimensionSelectorAttribute>().GetAttribute<PXSelectorAttribute>().SetColumns(this._FieldList, this._HeaderList);
  }

  public virtual void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    if (this.AttributeLevel == PXAttributeLevel.Item || e.IsAltered)
      this.PopulateFields(sender);
    new PXFieldSelecting(this.GetAttribute<PXDimensionSelectorAttribute>().FieldSelecting)(sender, e);
  }

  protected void EmitColumnForVendorField(PXCache sender)
  {
    if (this.DescriptionField == (System.Type) null)
      return;
    string alias1 = $"{this._FieldName}_{typeof (Vendor).Name}_{this.DescriptionField.Name}";
    if (!sender.Fields.Contains(alias1))
    {
      sender.Fields.Add(alias1);
      sender.Graph.FieldSelecting.AddHandler(sender.GetItemType(), alias1, (PXFieldSelecting) ((s, e) =>
      {
        this.PopulateFields(sender);
        this.GetAttribute<PXDimensionSelectorAttribute>().GetAttribute<PXSelectorAttribute>().DescriptionFieldSelecting(s, e, alias1);
      }));
      sender.Graph.CommandPreparing.AddHandler(sender.GetItemType(), alias1, (PXCommandPreparing) ((s, e) => this.GetAttribute<PXDimensionSelectorAttribute>().GetAttribute<PXSelectorAttribute>().DescriptionFieldCommandPreparing(s, e)));
    }
    string alias2 = this._FieldName + "_description";
    if (sender.Fields.Contains(alias2))
      return;
    sender.Fields.Add(alias2);
    sender.Graph.FieldSelecting.AddHandler(sender.GetItemType(), alias2, (PXFieldSelecting) ((s, e) =>
    {
      this.PopulateFields(sender);
      this.GetAttribute<PXDimensionSelectorAttribute>().GetAttribute<PXSelectorAttribute>().DescriptionFieldSelecting(s, e, alias2);
    }));
    sender.Graph.CommandPreparing.AddHandler(sender.GetItemType(), alias2, (PXCommandPreparing) ((s, e) => this.GetAttribute<PXDimensionSelectorAttribute>().GetAttribute<PXSelectorAttribute>().DescriptionFieldCommandPreparing(s, e)));
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
      VendorAttribute.Location.bAccountID>
    {
    }

    public abstract class locationID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      VendorAttribute.Location.locationID>
    {
    }

    public abstract class locationCD : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      VendorAttribute.Location.locationCD>
    {
    }

    public abstract class taxRegistrationID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      VendorAttribute.Location.taxRegistrationID>
    {
    }

    public abstract class vShipTermsID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      VendorAttribute.Location.vShipTermsID>
    {
    }

    public abstract class vLeadTime : 
      BqlType<
      #nullable enable
      IBqlShort, short>.Field<
      #nullable disable
      VendorAttribute.Location.vLeadTime>
    {
    }

    public abstract class vCarrierID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      VendorAttribute.Location.vCarrierID>
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
    [PXUIField(DisplayName = "Contact ID", Visibility = PXUIVisibility.Invisible)]
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
    [PXUIField(DisplayName = "Attention", Visibility = PXUIVisibility.SelectorVisible)]
    [PXPersonalDataField]
    public virtual string Attention { get; set; }

    [PXDBString(50)]
    [PXUIField(DisplayName = "Phone 1", Visibility = PXUIVisibility.SelectorVisible)]
    [PhoneValidation]
    [PXPhone]
    public virtual string Phone1 { get; set; }

    [PXDBString(3)]
    [PXDefault("B1", PersistingCheck = PXPersistingCheck.Nothing)]
    [PXUIField(DisplayName = "Phone 1")]
    [PhoneTypes]
    public virtual string Phone1Type { get; set; }

    [PXDBString(50)]
    [PXUIField(DisplayName = "Phone 2")]
    [PhoneValidation]
    [PXPhone]
    public virtual string Phone2 { get; set; }

    [PXDBString(3)]
    [PXDefault("B2", PersistingCheck = PXPersistingCheck.Nothing)]
    [PXUIField(DisplayName = "Phone 2")]
    [PhoneTypes]
    public virtual string Phone2Type { get; set; }

    [PXDBString(50)]
    [PXUIField(DisplayName = "Phone 3")]
    [PhoneValidation]
    [PXPhone]
    public virtual string Phone3 { get; set; }

    [PXDBString(3)]
    [PXDefault("H1", PersistingCheck = PXPersistingCheck.Nothing)]
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
    [PXUIField(DisplayName = "Email", Visibility = PXUIVisibility.SelectorVisible)]
    public virtual string EMail { get; set; }

    public abstract class bAccountID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      VendorAttribute.Contact.bAccountID>
    {
    }

    public abstract class contactID : BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    VendorAttribute.Contact.contactID>
    {
    }

    public abstract class revisionID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      VendorAttribute.Contact.revisionID>
    {
    }

    public abstract class title : BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    VendorAttribute.Contact.title>
    {
    }

    public abstract class firstName : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      VendorAttribute.Contact.firstName>
    {
    }

    public abstract class midName : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      VendorAttribute.Contact.midName>
    {
    }

    public abstract class lastName : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      VendorAttribute.Contact.lastName>
    {
    }

    public abstract class salutation : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      VendorAttribute.Contact.salutation>
    {
    }

    public abstract class attention : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      VendorAttribute.Contact.attention>
    {
    }

    public abstract class phone1 : BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    VendorAttribute.Contact.phone1>
    {
    }

    public abstract class phone1Type : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      VendorAttribute.Contact.phone1Type>
    {
    }

    public abstract class phone2 : BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    VendorAttribute.Contact.phone2>
    {
    }

    public abstract class phone2Type : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      VendorAttribute.Contact.phone2Type>
    {
    }

    public abstract class phone3 : BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    VendorAttribute.Contact.phone3>
    {
    }

    public abstract class phone3Type : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      VendorAttribute.Contact.phone3Type>
    {
    }

    public abstract class webSite : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      VendorAttribute.Contact.webSite>
    {
    }

    public abstract class fax : BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    VendorAttribute.Contact.fax>
    {
    }

    public abstract class eMail : BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    VendorAttribute.Contact.eMail>
    {
    }
  }
}
