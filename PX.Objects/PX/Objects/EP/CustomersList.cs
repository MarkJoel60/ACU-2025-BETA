// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.CustomersList
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.AR;
using System;

#nullable enable
namespace PX.Objects.EP;

[Serializable]
public class CustomersList : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected bool? _Selected = new bool?(false);
  protected 
  #nullable disable
  string _CustomerClassID;
  protected int? _CustomerID;
  protected int? _LocationID;

  [PXBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Selected")]
  public bool? Selected
  {
    get => this._Selected;
    set => this._Selected = value;
  }

  [PXDBString(10, IsUnicode = true, InputMask = ">aaaaaaaaaa")]
  [PXSelector(typeof (CustomerClass.customerClassID), DescriptionField = typeof (CustomerClass.descr), CacheGlobal = true)]
  [PXUIField(DisplayName = "Customer Class")]
  public virtual string CustomerClassID
  {
    get => this._CustomerClassID;
    set => this._CustomerClassID = value;
  }

  [Customer(DescriptionField = typeof (PX.Objects.AR.Customer.acctName), IsKey = true)]
  public virtual int? CustomerID
  {
    get => this._CustomerID;
    set => this._CustomerID = value;
  }

  [PX.Objects.CS.LocationID(typeof (Where<PX.Objects.CR.Location.bAccountID, Equal<Current<CustomersList.customerID>>>))]
  public virtual int? LocationID
  {
    get => this._LocationID;
    set => this._LocationID = value;
  }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CustomersList.selected>
  {
  }

  public abstract class customerClassID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CustomersList.customerClassID>
  {
  }

  public abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CustomersList.customerID>
  {
  }

  public abstract class locationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CustomersList.locationID>
  {
  }
}
