// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.BAccountRawAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.AP;
using PX.Objects.AR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

#nullable disable
namespace PX.Objects.CR;

[PXDBInt]
[PXInt]
[PXUIField]
[PXAttributeFamily(typeof (PXEntityAttribute))]
public abstract class BAccountRawAttribute : PXEntityAttribute
{
  public const string DimensionName = "BIZACCT";
  protected System.Type EntityType = typeof (BAccount);
  protected System.Type[] BAccountTypes = new System.Type[8]
  {
    typeof (BAccountType.vendorType),
    typeof (BAccountType.customerType),
    typeof (BAccountType.combinedType),
    typeof (BAccountType.employeeType),
    typeof (BAccountType.empCombinedType),
    typeof (BAccountType.prospectType),
    typeof (BAccountType.branchType),
    typeof (BAccountType.organizationType)
  };

  public virtual PXSelectorMode SelectorMode { get; set; }

  public virtual bool HideInactiveVendors { get; set; } = true;

  public virtual bool HideInactiveCustomers { get; set; } = true;

  public bool ViewInCRM { get; set; }

  public BAccountRawAttribute(
    System.Type entityType,
    System.Type[] bAccountTypes = null,
    System.Type customSearchQuery = null,
    System.Type[] fieldList = null,
    string[] headerList = null)
  {
    System.Type type1 = entityType;
    if ((object) type1 == null)
      type1 = this.EntityType;
    this.EntityType = type1;
    this.BAccountTypes = bAccountTypes ?? this.BAccountTypes;
    System.Type type2 = customSearchQuery;
    if ((object) type2 == null)
      type2 = this.CreateSelect();
    System.Type type3 = this.Field<BAccount.acctCD>();
    System.Type[] typeArray1 = fieldList;
    if (typeArray1 == null)
      typeArray1 = new System.Type[10]
      {
        typeof (BAccount.acctCD),
        typeof (BAccount.acctName),
        typeof (BAccount.type),
        typeof (BAccount.classID),
        typeof (BAccount.status),
        typeof (Contact.phone1),
        typeof (Address.city),
        typeof (Address.state),
        typeof (Address.countryID),
        typeof (Contact.eMail)
      };
    PXDimensionSelectorAttribute selectorAttribute1 = new PXDimensionSelectorAttribute("BIZACCT", type2, type3, typeArray1);
    PXDimensionSelectorAttribute selectorAttribute2 = selectorAttribute1;
    string[] strArray;
    if (headerList == null && fieldList == null)
      strArray = new string[10]
      {
        "Account ID",
        "Account Name",
        "Type",
        "Class",
        "Customer Status",
        "Phone 1",
        "City",
        "State",
        "Country",
        "Email"
      };
    else
      strArray = headerList;
    selectorAttribute2.Headers = strArray;
    selectorAttribute1.DescriptionField = this.Field<BAccount.acctName>();
    selectorAttribute1.SelectorMode = this.SelectorMode;
    selectorAttribute1.Filterable = true;
    selectorAttribute1.DirtyRead = true;
    ((PXAggregateAttribute) this)._Attributes.Add((PXEventSubscriberAttribute) selectorAttribute1);
    this._SelAttrIndex = ((List<PXEventSubscriberAttribute>) ((PXAggregateAttribute) this)._Attributes).Count - 1;
    ((PXAggregateAttribute) this)._Attributes.Add((PXEventSubscriberAttribute) new PXRestrictorAttribute(this.GetBAccountTypeWhere(), "Business Account is {0}.", new System.Type[1]
    {
      typeof (BAccount.type)
    })
    {
      ShowWarning = true
    });
    if ((!this.HideInactiveCustomers || !((IEnumerable<System.Type>) this.BAccountTypes).Any<System.Type>((Func<System.Type, bool>) (type => EnumerableExtensions.IsIn<System.Type>(type, (IEnumerable<System.Type>) new System.Type[3]
    {
      typeof (BAccountType.prospectType),
      typeof (BAccountType.customerType),
      typeof (BAccountType.combinedType)
    })))) && (!this.HideInactiveVendors || !((IEnumerable<System.Type>) this.BAccountTypes).Any<System.Type>((Func<System.Type, bool>) (type => EnumerableExtensions.IsIn<System.Type>(type, (IEnumerable<System.Type>) new System.Type[2]
    {
      typeof (BAccountType.vendorType),
      typeof (BAccountType.combinedType)
    })))))
      return;
    PXAggregateAttribute.AggregatedAttributesCollection attributes = ((PXAggregateAttribute) this)._Attributes;
    System.Type type4 = typeof (Where<Not<Where<BAccount.status, IsNull, And<BAccount.vStatus, IsNull, Or<BAccount.status, IsNull, And<BAccount.vStatus, Equal<VendorStatus.inactive>, Or<BAccount.status, Equal<CustomerStatus.inactive>, And<BAccount.vStatus, IsNull, Or<BAccount.status, Equal<CustomerStatus.inactive>, And<BAccount.vStatus, Equal<VendorStatus.inactive>>>>>>>>>>>);
    System.Type[] typeArray2 = new System.Type[1];
    System.Type type5 = typeof (BAccount.status);
    if ((object) type5 == null)
      type5 = typeof (BAccount.vStatus);
    typeArray2[0] = type5;
    attributes.Add((PXEventSubscriberAttribute) new PXRestrictorAttribute(type4, "Business Account is {0}.", typeArray2)
    {
      ShowWarning = true
    });
  }

  protected virtual System.Type GetBAccountTypeWhere()
  {
    System.Type baccountTypeWhere = (System.Type) null;
    bool isBranchExpected = ((IEnumerable<System.Type>) this.BAccountTypes).Contains<System.Type>(typeof (BAccountType.branchType));
    System.Type type = this.Field<BAccount.type>();
    switch (this.BAccountTypes.Length)
    {
      case 1:
        baccountTypeWhere = this.AppendIsBranchIfNeeded(BqlCommand.Compose(new System.Type[4]
        {
          typeof (Where<,>),
          type,
          typeof (Equal<>),
          this.BAccountTypes[0]
        }), isBranchExpected);
        break;
      case 2:
        baccountTypeWhere = this.AppendIsBranchIfNeeded(BqlCommand.Compose(new System.Type[5]
        {
          typeof (Where<,>),
          type,
          typeof (In3<,>),
          this.BAccountTypes[0],
          this.BAccountTypes[1]
        }), isBranchExpected);
        break;
      case 3:
        baccountTypeWhere = this.AppendIsBranchIfNeeded(BqlCommand.Compose(new System.Type[6]
        {
          typeof (Where<,>),
          type,
          typeof (In3<,,>),
          this.BAccountTypes[0],
          this.BAccountTypes[1],
          this.BAccountTypes[2]
        }), isBranchExpected);
        break;
      case 4:
        baccountTypeWhere = this.AppendIsBranchIfNeeded(BqlCommand.Compose(new System.Type[7]
        {
          typeof (Where<,>),
          type,
          typeof (In3<,,,>),
          this.BAccountTypes[0],
          this.BAccountTypes[1],
          this.BAccountTypes[2],
          this.BAccountTypes[3]
        }), isBranchExpected);
        break;
      case 5:
        baccountTypeWhere = this.AppendIsBranchIfNeeded(BqlCommand.Compose(new System.Type[8]
        {
          typeof (Where<,>),
          type,
          typeof (In3<,,,,>),
          this.BAccountTypes[0],
          this.BAccountTypes[1],
          this.BAccountTypes[2],
          this.BAccountTypes[3],
          this.BAccountTypes[4]
        }), isBranchExpected);
        break;
      case 6:
        baccountTypeWhere = this.AppendIsBranchIfNeeded(BqlCommand.Compose(new System.Type[9]
        {
          typeof (Where<,>),
          type,
          typeof (In3<,,,,,>),
          this.BAccountTypes[0],
          this.BAccountTypes[1],
          this.BAccountTypes[2],
          this.BAccountTypes[3],
          this.BAccountTypes[4],
          this.BAccountTypes[5]
        }), isBranchExpected);
        break;
      case 7:
        baccountTypeWhere = this.AppendIsBranchIfNeeded(BqlCommand.Compose(new System.Type[10]
        {
          typeof (Where<,>),
          type,
          typeof (In3<,,,,,,>),
          this.BAccountTypes[0],
          this.BAccountTypes[1],
          this.BAccountTypes[2],
          this.BAccountTypes[3],
          this.BAccountTypes[4],
          this.BAccountTypes[5],
          this.BAccountTypes[6]
        }), isBranchExpected);
        break;
      case 8:
        baccountTypeWhere = this.AppendIsBranchIfNeeded(BqlCommand.Compose(new System.Type[11]
        {
          typeof (Where<,>),
          type,
          typeof (In3<,,,,,,,>),
          this.BAccountTypes[0],
          this.BAccountTypes[1],
          this.BAccountTypes[2],
          this.BAccountTypes[3],
          this.BAccountTypes[4],
          this.BAccountTypes[5],
          this.BAccountTypes[6],
          this.BAccountTypes[7]
        }), isBranchExpected);
        break;
    }
    return baccountTypeWhere;
  }

  protected virtual System.Type AppendIsBranchIfNeeded(
    System.Type originalCondition,
    bool isBranchExpected)
  {
    if (!isBranchExpected || originalCondition.GenericTypeArguments.Length != 2)
      return originalCondition;
    return BqlCommand.Compose(new System.Type[7]
    {
      typeof (Where<,,>),
      originalCondition.GenericTypeArguments[0],
      originalCondition.GenericTypeArguments[1],
      typeof (Or<,>),
      this.Field<BAccount.isBranch>(),
      typeof (Equal<>),
      typeof (True)
    });
  }

  protected virtual System.Type CreateSelect()
  {
    return ((IBqlTemplate) BqlTemplate.OfCommand<Search2<BqlPlaceholder.I, LeftJoin<Contact, On<Contact.bAccountID, Equal<BqlPlaceholder.I>, And<Contact.contactID, Equal<BqlPlaceholder.C>>>, LeftJoin<Address, On<Address.bAccountID, Equal<BqlPlaceholder.I>, And<Address.addressID, Equal<BqlPlaceholder.A>>>>>, Where<Match<Current<AccessInfo.userName>>>>>.Replace<BqlPlaceholder.I>(this.Field<BAccount.bAccountID>()).Replace<BqlPlaceholder.C>(this.Field<BAccount.defContactID>()).Replace<BqlPlaceholder.A>(this.Field<BAccount.defAddressID>())).ToType();
  }

  public virtual void CacheAttached(PXCache sender)
  {
    PXCache cach = sender.Graph.Caches[typeof (BAccount)];
    ((PXAggregateAttribute) this).CacheAttached(sender);
    if (!this.ViewInCRM)
      return;
    PXGraph.RowSelectingEvents rowSelecting = sender.Graph.RowSelecting;
    System.Type entityType1 = this.EntityType;
    BAccountRawAttribute baccountRawAttribute1 = this;
    // ISSUE: virtual method pointer
    PXRowSelecting pxRowSelecting = new PXRowSelecting((object) baccountRawAttribute1, __vmethodptr(baccountRawAttribute1, BAccount_RowSelecting));
    rowSelecting.AddHandler(entityType1, pxRowSelecting);
    PXGraph.FieldDefaultingEvents fieldDefaulting = sender.Graph.FieldDefaulting;
    System.Type entityType2 = this.EntityType;
    BAccountRawAttribute baccountRawAttribute2 = this;
    // ISSUE: virtual method pointer
    PXFieldDefaulting pxFieldDefaulting = new PXFieldDefaulting((object) baccountRawAttribute2, __vmethodptr(baccountRawAttribute2, BAccount_ViewInCrm_FieldDefaulting));
    fieldDefaulting.AddHandler(entityType2, "ViewInCrm", pxFieldDefaulting);
  }

  protected virtual void BAccount_RowSelecting(PXCache sender, PXRowSelectingEventArgs e)
  {
    sender.SetValue(e.Row, "ViewInCrm", (object) true);
  }

  protected virtual void BAccount_ViewInCrm_FieldDefaulting(
    PXCache sender,
    PXFieldDefaultingEventArgs e)
  {
    e.NewValue = (object) true;
  }

  protected virtual (string[], string[]) MapBAccountTypesToStringList()
  {
    List<string> stringList1 = new List<string>();
    List<string> stringList2 = new List<string>();
    foreach (MemberInfo baccountType in this.BAccountTypes)
    {
      string name = baccountType.Name;
      if (name != null)
      {
        switch (name.Length)
        {
          case 10:
            switch (name[0])
            {
              case 'b':
                if (name == "branchType")
                {
                  stringList1.Add("CP");
                  stringList2.Add("Branch");
                  continue;
                }
                continue;
              case 'v':
                if (name == "vendorType")
                {
                  stringList1.Add("VE");
                  stringList2.Add("Vendor");
                  continue;
                }
                continue;
              default:
                continue;
            }
          case 12:
            switch (name[1])
            {
              case 'm':
                if (name == "employeeType")
                {
                  stringList1.Add("EP");
                  stringList2.Add("Employee");
                  continue;
                }
                continue;
              case 'o':
                if (name == "combinedType")
                {
                  stringList1.Add("VC");
                  stringList2.Add("Customer & Vendor");
                  continue;
                }
                continue;
              case 'r':
                if (name == "prospectType")
                {
                  stringList1.Add("PR");
                  stringList2.Add("Prospect");
                  continue;
                }
                continue;
              case 'u':
                if (name == "customerType")
                {
                  stringList1.Add("CU");
                  stringList2.Add("Customer");
                  continue;
                }
                continue;
              default:
                continue;
            }
          case 15:
            if (name == "empCombinedType")
            {
              stringList1.Add("EC");
              stringList2.Add("Customer & Employee");
              continue;
            }
            continue;
          case 16 /*0x10*/:
            if (name == "organizationType")
            {
              stringList1.Add("OR");
              stringList2.Add("Company");
              continue;
            }
            continue;
          default:
            continue;
        }
      }
    }
    return (stringList1.ToArray(), stringList2.ToArray());
  }

  protected virtual System.Type Field<TField>()
  {
    return this.EntityType.GetNestedType(typeof (TField).Name, BindingFlags.IgnoreCase | BindingFlags.Public, true);
  }
}
