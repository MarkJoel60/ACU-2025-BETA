// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.Repositories.CustomerRepository
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.Common;
using PX.Objects.CR;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.AR.Repositories;

public class CustomerRepository(PXGraph graph) : RepositoryBase<Customer>(graph)
{
  public Customer FindByID(int? accountID)
  {
    using (IEnumerator<PXResult<PX.Objects.CR.BAccount>> enumerator = PXSelectBase<PX.Objects.CR.BAccount, PXSelectJoin<PX.Objects.CR.BAccount, InnerJoinSingleTable<Customer, On<PX.Objects.CR.BAccount.bAccountID, Equal<Customer.bAccountID>>>, Where2<Where<PX.Objects.CR.BAccount.type, Equal<BAccountType.customerType>, Or<PX.Objects.CR.BAccount.type, Equal<BAccountType.combinedType>>>, And<Customer.bAccountID, Equal<Required<Customer.bAccountID>>>>>.Config>.Select(this._graph, new object[1]
    {
      (object) accountID
    }).GetEnumerator())
    {
      if (enumerator.MoveNext())
      {
        PXResult<PX.Objects.CR.BAccount, Customer> current = (PXResult<PX.Objects.CR.BAccount, Customer>) enumerator.Current;
        PX.Objects.CR.BAccount baccount = PXResult<PX.Objects.CR.BAccount, Customer>.op_Implicit(current);
        Customer byId = PXResult<PX.Objects.CR.BAccount, Customer>.op_Implicit(current);
        PXCache<PX.Objects.CR.BAccount>.RestoreCopy((PX.Objects.CR.BAccount) byId, baccount);
        return byId;
      }
    }
    return (Customer) null;
  }

  public Customer FindByCD(string accountCD)
  {
    using (IEnumerator<PXResult<PX.Objects.CR.BAccount>> enumerator = PXSelectBase<PX.Objects.CR.BAccount, PXSelectJoin<PX.Objects.CR.BAccount, InnerJoinSingleTable<Customer, On<PX.Objects.CR.BAccount.bAccountID, Equal<Customer.bAccountID>>>, Where2<Where<PX.Objects.CR.BAccount.type, Equal<BAccountType.customerType>, Or<PX.Objects.CR.BAccount.type, Equal<BAccountType.combinedType>>>, And<PX.Objects.CR.BAccount.acctCD, Equal<Required<Customer.acctCD>>>>>.Config>.Select(this._graph, new object[1]
    {
      (object) accountCD
    }).GetEnumerator())
    {
      if (enumerator.MoveNext())
      {
        PXResult<PX.Objects.CR.BAccount, Customer> current = (PXResult<PX.Objects.CR.BAccount, Customer>) enumerator.Current;
        PX.Objects.CR.BAccount baccount = PXResult<PX.Objects.CR.BAccount, Customer>.op_Implicit(current);
        Customer byCd = PXResult<PX.Objects.CR.BAccount, Customer>.op_Implicit(current);
        PXCache<PX.Objects.CR.BAccount>.RestoreCopy((PX.Objects.CR.BAccount) byCd, baccount);
        return byCd;
      }
    }
    return (Customer) null;
  }

  public Customer GetByCD(string accountCD)
  {
    return this.ForceNotNull(this.FindByCD(accountCD), accountCD);
  }

  public Tuple<CustomerClass, Customer> GetCustomerAndClassById(int? customerId)
  {
    PXResult<CustomerClass, Customer> pxResult = (PXResult<CustomerClass, Customer>) PXResultset<CustomerClass>.op_Implicit(PXSelectBase<CustomerClass, PXSelectJoin<CustomerClass, InnerJoin<Customer, On<CustomerClass.customerClassID, Equal<Customer.customerClassID>>>, Where<Customer.bAccountID, Equal<Required<Customer.bAccountID>>>>.Config>.Select(this._graph, new object[1]
    {
      (object) customerId
    }));
    return pxResult != null ? new Tuple<CustomerClass, Customer>(PXResult<CustomerClass, Customer>.op_Implicit(pxResult), PXResult<CustomerClass, Customer>.op_Implicit(pxResult)) : (Tuple<CustomerClass, Customer>) null;
  }
}
