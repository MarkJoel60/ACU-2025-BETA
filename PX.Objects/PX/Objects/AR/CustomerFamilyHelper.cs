// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.CustomerFamilyHelper
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AR.Override;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.AR;

/// <summary>
/// Contains helper methods to retrieve customer families.
/// </summary>
public static class CustomerFamilyHelper
{
  /// <summary>
  /// Gets the family of customers whose ID is either equal to the provided parent customer ID,
  /// or which contain that ID in the specified parent field.
  /// </summary>
  /// <param name="customerID">
  /// The parent customer ID. If <c>null</c>, the value will be taken from the Current customer used.
  /// </param>
  public static IEnumerable<ExtendedCustomer> GetCustomerFamily<ParentField>(
    PXGraph graph,
    int? customerID = null)
    where ParentField : IBqlField
  {
    return ((IEnumerable<PXResult<PX.Objects.AR.Override.Customer>>) PXSelectBase<PX.Objects.AR.Override.Customer, PXSelectReadonly2<PX.Objects.AR.Override.Customer, InnerJoin<PX.Objects.AR.Override.BAccount, On<PX.Objects.AR.Override.Customer.bAccountID, Equal<PX.Objects.AR.Override.BAccount.bAccountID>>>, Where<PX.Objects.AR.Override.BAccount.bAccountID, Equal<Optional<Customer.bAccountID>>, Or<ParentField, Equal<Optional<Customer.bAccountID>>>>>.Config>.Select(graph, new object[2]
    {
      (object) customerID,
      (object) customerID
    })).AsEnumerable<PXResult<PX.Objects.AR.Override.Customer>>().Cast<PXResult<PX.Objects.AR.Override.Customer, PX.Objects.AR.Override.BAccount>>().Select<PXResult<PX.Objects.AR.Override.Customer, PX.Objects.AR.Override.BAccount>, ExtendedCustomer>((Func<PXResult<PX.Objects.AR.Override.Customer, PX.Objects.AR.Override.BAccount>, ExtendedCustomer>) (result => new ExtendedCustomer(PXResult<PX.Objects.AR.Override.Customer, PX.Objects.AR.Override.BAccount>.op_Implicit(result), PXResult<PX.Objects.AR.Override.Customer, PX.Objects.AR.Override.BAccount>.op_Implicit(result))));
  }
}
