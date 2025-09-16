// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMCustomerBillingAddressMapped
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data.BQL;
using PX.Objects.CR;

#nullable disable
namespace PX.Objects.PM;

public class PMCustomerBillingAddressMapped : 
  BqlFieldMapper<PMCustomerBillingAddress, PMBillingAddress>
{
  public PMCustomerBillingAddressMapped()
  {
    this.Map<BqlField<PMAddress.addressID, IBqlInt>.EqualTo<PMCustomerBillingAddress.projectAddressID>>();
    this.Map<BqlField<PMAddress.customerAddressID, IBqlInt>.EqualTo<Address.addressID>>();
    this.Map<BqlField<PMAddress.customerID, IBqlInt>.EqualTo<Address.bAccountID>>();
  }
}
