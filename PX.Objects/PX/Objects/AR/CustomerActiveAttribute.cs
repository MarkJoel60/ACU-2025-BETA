// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.CustomerActiveAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.AR;

/// <summary>
/// This is a specialized version of the Customer attribute.<br />
/// Displays only Active or OneTime customers<br />
/// See CustomerAttribute for detailed description. <br />
/// </summary>
[PXDBInt]
[PXUIField]
[PXRestrictor(typeof (Where<Customer.status, IsNull, Or<Customer.status, Equal<CustomerStatus.active>, Or<Customer.status, Equal<CustomerStatus.oneTime>>>>), "The customer status is '{0}'.", new Type[] {typeof (Customer.status)})]
public class CustomerActiveAttribute : CustomerAttribute
{
  public CustomerActiveAttribute(Type search, params Type[] fields)
    : base(search, fields)
  {
  }

  public CustomerActiveAttribute(Type search)
    : base(search)
  {
  }

  public CustomerActiveAttribute()
  {
  }
}
