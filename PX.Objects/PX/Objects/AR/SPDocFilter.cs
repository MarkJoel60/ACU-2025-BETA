// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.SPDocFilter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.AR;

[Serializable]
public class SPDocFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _SalesPersonID;
  protected 
  #nullable disable
  string _CommnPeriod;
  protected int? _CustomerID;
  protected int? _LocationID;

  [SalesPerson(DescriptionField = typeof (SalesPerson.descr))]
  public virtual int? SalesPersonID
  {
    get => this._SalesPersonID;
    set => this._SalesPersonID = value;
  }

  [PXDefault]
  [ARCommissionPeriodID]
  [PXUIField]
  public virtual string CommnPeriod
  {
    get => this._CommnPeriod;
    set => this._CommnPeriod = value;
  }

  [Customer(DescriptionField = typeof (Customer.acctName))]
  public virtual int? CustomerID
  {
    get => this._CustomerID;
    set => this._CustomerID = value;
  }

  [PX.Objects.CS.LocationID(typeof (Where<PX.Objects.CR.Location.bAccountID, Equal<Current<SPDocFilter.customerID>>>), DisplayName = "Location", DescriptionField = typeof (PX.Objects.CR.Location.descr))]
  public virtual int? LocationID
  {
    get => this._LocationID;
    set => this._LocationID = value;
  }

  public abstract class salesPersonID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SPDocFilter.salesPersonID>
  {
  }

  public abstract class commnPeriod : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SPDocFilter.commnPeriod>
  {
  }

  public abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SPDocFilter.customerID>
  {
  }

  public abstract class locationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SPDocFilter.locationID>
  {
  }
}
