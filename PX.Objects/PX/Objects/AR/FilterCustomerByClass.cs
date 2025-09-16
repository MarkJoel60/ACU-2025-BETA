// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.FilterCustomerByClass
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CR;

#nullable enable
namespace PX.Objects.AR;

public class FilterCustomerByClass : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _CustomerClassID;
  protected int? _CustomerID;

  [PXDBString(10, IsUnicode = true)]
  [PXUIField]
  [PXSelector(typeof (Search<CustomerClass.customerClassID>))]
  public virtual string CustomerClassID
  {
    get => this._CustomerClassID;
    set => this._CustomerClassID = value;
  }

  [PXDBInt]
  [PXDimensionSelector("BIZACCT", typeof (Search<Customer.bAccountID, Where<Customer.customerClassID, Equal<Optional<FilterCustomerByClass.customerClassID>>, Or<Optional<FilterCustomerByClass.customerClassID>, IsNull>>>), typeof (BAccountR.acctCD), new System.Type[] {typeof (BAccountR.acctCD), typeof (Customer.acctName), typeof (Customer.customerClassID), typeof (Customer.status), typeof (PX.Objects.CR.Contact.phone1), typeof (PX.Objects.CR.Address.city), typeof (PX.Objects.CR.Address.countryID)})]
  public virtual int? CustomerID
  {
    get => this._CustomerID;
    set => this._CustomerID = value;
  }

  public abstract class customerClassID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FilterCustomerByClass.customerClassID>
  {
  }

  public abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FilterCustomerByClass.customerID>
  {
  }
}
