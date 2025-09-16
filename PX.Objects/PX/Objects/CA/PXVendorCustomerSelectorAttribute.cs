// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.PXVendorCustomerSelectorAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.AP;
using PX.Objects.AR;
using PX.Objects.CR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace PX.Objects.CA;

/// <summary>
/// Selector. Allows to select either vendors or customers depending <br />
/// upon the value in the BatchModule field. Will return a list of Customers for AR<br />
/// and a list of Vendors for AP, other types are not supported. If a currency ID is provided, <br />
/// list of Vendors/Customers will be restricted by those, having this CuryID set <br />
/// or having AllowOverrideCury set on. For example this allows to select only Customers/Vendors <br />
/// which may pay to/from a specific cash account<br />
/// <example>
/// [PXVendorCustomerSelector(typeof(CABankTran.origModule))]
/// </example>
/// </summary>
[Serializable]
public class PXVendorCustomerSelectorAttribute : PXCustomSelectorAttribute
{
  protected 
  #nullable disable
  System.Type BatchModule;
  protected System.Type _CuryID;
  protected bool _onlyActive;

  /// <summary>Ctor</summary>
  /// <param name="batchModule">Must be IBqlField. Refers to the BatchModule field of the row.</param>
  public PXVendorCustomerSelectorAttribute(System.Type batchModule)
    : this(batchModule, (System.Type) null, false)
  {
  }

  /// <summary>Ctor</summary>
  /// <param name="batchModule">Must be IBqlField. Refers to the BatchModule field of the row.</param>
  /// <param name="curyID">Must be IBqlField.Refers to the CuryID field of the row.</param>
  public PXVendorCustomerSelectorAttribute(System.Type batchModule, System.Type curyID)
    : this(batchModule, curyID, false)
  {
  }

  /// <summary>Ctor</summary>
  /// <param name="batchModule">Must be IBqlField. Refers to the BatchModule field of the row.</param>
  /// <param name="onlyActive">Flag to display only active business accounts (Active, One-Time, Hold Payments).</param>
  public PXVendorCustomerSelectorAttribute(System.Type batchModule, bool onlyActive)
    : this(batchModule, (System.Type) null, onlyActive)
  {
  }

  /// <summary>Ctor</summary>
  /// <param name="batchModule">Must be IBqlField. Refers to the BatchModule field of the row.</param>
  /// <param name="curyID">Must be IBqlField.Refers to the CuryID field of the row.</param>
  /// <param name="onlyActive">Flag to display only active business accounts (Active, One-Time, Hold Payments).</param>
  public PXVendorCustomerSelectorAttribute(System.Type batchModule, System.Type curyID, bool onlyActive)
    : base(typeof (Search<BAccountR.bAccountID, Where<True, Equal<False>, And<Match<Current<AccessInfo.userName>>>>>), new System.Type[2]
    {
      typeof (BAccountR.acctCD),
      typeof (BAccountR.acctName)
    })
  {
    ((PXSelectorAttribute) this).SubstituteKey = typeof (BAccountR.acctCD);
    ((PXSelectorAttribute) this).DescriptionField = typeof (BAccountR.acctName);
    this.BatchModule = batchModule;
    this._CuryID = curyID;
    this._onlyActive = onlyActive;
  }

  protected virtual IEnumerable GetRecords()
  {
    PXVendorCustomerSelectorAttribute selectorAttribute = this;
    PXCache cach = selectorAttribute._Graph.Caches[BqlCommand.GetItemType(selectorAttribute.BatchModule)];
    object obj1 = (object) null;
    foreach (object current in PXView.Currents)
    {
      if (current != null && (current.GetType() == BqlCommand.GetItemType(selectorAttribute.BatchModule) || current.GetType().IsSubclassOf(BqlCommand.GetItemType(selectorAttribute.BatchModule))))
      {
        obj1 = current;
        break;
      }
    }
    if (obj1 == null)
      obj1 = cach.Current;
    PXView view = PXView.View;
    if (view != null)
    {
      if (selectorAttribute._CuryID != (System.Type) null)
      {
        object obj2 = cach.GetValue(obj1, selectorAttribute._CuryID.Name) ?? cach.GetValuePending(obj1, selectorAttribute._CuryID.Name);
        PXContext.SetSlot(selectorAttribute._CuryID.FullName, obj2);
      }
      switch ((string) (cach.GetValue(obj1, selectorAttribute.BatchModule.Name) ?? cach.GetValuePending(obj1, selectorAttribute.BatchModule.Name)))
      {
        case "AP":
          view.WhereNew<Where2<Match<PXVendorCustomerSelectorAttribute.VendorR, Current<AccessInfo.userName>>, And<Where<BAccountR.type, Equal<BAccountType.vendorType>, Or<BAccountR.type, Equal<BAccountType.employeeType>, Or<BAccountR.type, Equal<BAccountType.combinedType>, Or<BAccountR.type, Equal<BAccountType.empCombinedType>>>>>>>>();
          if (selectorAttribute._CuryID != (System.Type) null)
          {
            System.Type type = BqlCommand.Compose(new System.Type[19]
            {
              typeof (InnerJoin<,>),
              typeof (PXVendorCustomerSelectorAttribute.VendorR),
              typeof (On<,,>),
              typeof (PXVendorCustomerSelectorAttribute.VendorR.bAccountID),
              typeof (Equal<>),
              typeof (BAccountR.bAccountID),
              typeof (And<>),
              typeof (Where<,,>),
              typeof (PXVendorCustomerSelectorAttribute.VendorR.curyID),
              typeof (Equal<>),
              typeof (SavedStringValue<>),
              selectorAttribute._CuryID,
              typeof (Or<,,>),
              typeof (PXVendorCustomerSelectorAttribute.VendorR.allowOverrideCury),
              typeof (Equal<>),
              typeof (True),
              typeof (Or<,>),
              typeof (PXVendorCustomerSelectorAttribute.VendorR.curyID),
              typeof (IsNull)
            });
            view.JoinNew(type);
          }
          else
          {
            System.Type type = BqlCommand.Compose(new System.Type[6]
            {
              typeof (InnerJoin<,>),
              typeof (PXVendorCustomerSelectorAttribute.VendorR),
              typeof (On<,>),
              typeof (PXVendorCustomerSelectorAttribute.VendorR.bAccountID),
              typeof (Equal<>),
              typeof (BAccountR.bAccountID)
            });
            view.JoinNew(type);
          }
          if (selectorAttribute._onlyActive)
          {
            view.WhereAnd(typeof (Where<BAccountR.vStatus, In3<VendorStatus.active, VendorStatus.oneTime, VendorStatus.holdPayments>>));
            break;
          }
          break;
        case "AR":
          view.WhereNew<Where2<Match<PXVendorCustomerSelectorAttribute.CustomerR, Current<AccessInfo.userName>>, And<Where<BAccountR.type, Equal<BAccountType.customerType>, Or<BAccountR.type, Equal<BAccountType.combinedType>, Or<BAccountR.type, Equal<BAccountType.empCombinedType>>>>>>>();
          if (selectorAttribute._CuryID != (System.Type) null)
          {
            System.Type type = BqlCommand.Compose(new System.Type[19]
            {
              typeof (InnerJoin<,>),
              typeof (PXVendorCustomerSelectorAttribute.CustomerR),
              typeof (On<,,>),
              typeof (PXVendorCustomerSelectorAttribute.CustomerR.bAccountID),
              typeof (Equal<>),
              typeof (BAccountR.bAccountID),
              typeof (And<>),
              typeof (Where<,,>),
              typeof (PXVendorCustomerSelectorAttribute.CustomerR.curyID),
              typeof (Equal<>),
              typeof (SavedStringValue<>),
              selectorAttribute._CuryID,
              typeof (Or<,,>),
              typeof (PXVendorCustomerSelectorAttribute.CustomerR.allowOverrideCury),
              typeof (Equal<>),
              typeof (True),
              typeof (Or<,>),
              typeof (PXVendorCustomerSelectorAttribute.CustomerR.curyID),
              typeof (IsNull)
            });
            view.JoinNew(type);
            goto default;
          }
          System.Type type1 = BqlCommand.Compose(new System.Type[6]
          {
            typeof (InnerJoin<,>),
            typeof (PXVendorCustomerSelectorAttribute.CustomerR),
            typeof (On<,>),
            typeof (PXVendorCustomerSelectorAttribute.CustomerR.bAccountID),
            typeof (Equal<>),
            typeof (BAccountR.bAccountID)
          });
          view.JoinNew(type1);
          goto default;
        default:
          if (selectorAttribute._onlyActive)
          {
            view.WhereAnd(typeof (Where<BAccountR.status, In3<CustomerStatus.active, CustomerStatus.oneTime, CustomerStatus.creditHold>>));
            break;
          }
          break;
      }
      foreach (PXRestrictorAttribute restrictorAttribute in cach.GetAttributesReadonly(((PXEventSubscriberAttribute) selectorAttribute)._FieldName).OfType<PXRestrictorAttribute>())
        view.WhereAnd(restrictorAttribute.RestrictingCondition);
    }
    foreach (PXResult record in GraphHelper.QuickSelect(view))
      yield return (object) record;
  }

  private static object SelectSingleBound(PXView view, object[] currents, params object[] pars)
  {
    List<object> objectList = view.SelectMultiBound(currents, pars);
    if (objectList.Count <= 0)
      return (object) null;
    return objectList[0] is PXResult ? ((PXResult) objectList[0])[0] : objectList[0];
  }

  public virtual void FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    if (e.NewValue == null || !((PXSelectorAttribute) this).ValidateValue || sender.Keys.Count != 0 && !(((PXEventSubscriberAttribute) this)._FieldName != sender.Keys[sender.Keys.Count - 1]))
      return;
    PXSelectorAttribute.ViewWithParameters viewWithParameters = ((PXSelectorAttribute) this).GetViewWithParameters(sender, e.NewValue, false);
    object obj = (object) null;
    try
    {
      obj = ((PXSelectorAttribute.ViewWithParameters) ref viewWithParameters).SelectSingleBound(e.Row);
    }
    catch (FormatException ex)
    {
    }
    catch (InvalidCastException ex)
    {
    }
    if (obj != null)
      return;
    ((PXSelectorAttribute) this).throwNoItem(PXSelectorAttribute.hasRestrictedAccess(sender, ((PXSelectorAttribute) this)._PrimarySimpleSelect, e.Row), e.ExternalCall, e.NewValue);
  }

  [PXHidden]
  [Serializable]
  public class VendorR : PX.Objects.AP.Vendor
  {
    public new abstract class bAccountID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      PXVendorCustomerSelectorAttribute.VendorR.bAccountID>
    {
    }

    public new abstract class curyID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      PXVendorCustomerSelectorAttribute.VendorR.curyID>
    {
    }

    public new abstract class allowOverrideCury : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      PXVendorCustomerSelectorAttribute.VendorR.allowOverrideCury>
    {
    }
  }

  [PXHidden]
  [Serializable]
  public class CustomerR : PX.Objects.AR.Customer
  {
    public new abstract class bAccountID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      PXVendorCustomerSelectorAttribute.CustomerR.bAccountID>
    {
    }

    public new abstract class curyID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      PXVendorCustomerSelectorAttribute.CustomerR.curyID>
    {
    }

    public new abstract class allowOverrideCury : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      PXVendorCustomerSelectorAttribute.CustomerR.allowOverrideCury>
    {
    }
  }
}
