// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMCustomerBillingAddress
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using System;

#nullable enable
namespace PX.Objects.PM;

[PXHidden]
[PXBreakInheritance]
[PXProjection(typeof (SelectFromBase<PX.Objects.CR.Address, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.AR.Customer>.On<BqlOperand<PX.Objects.CR.Address.addressID, IBqlInt>.IsEqual<PX.Objects.AR.Customer.defBillAddressID>>>, FbqlJoins.Inner<PMAddress>.On<BqlOperand<PX.Objects.AR.Customer.bAccountID, IBqlInt>.IsEqual<PMAddress.customerID>>>>.Where<BqlOperand<PMAddress.isDefaultBillAddress, IBqlBool>.IsEqual<True>>))]
[Serializable]
public class PMCustomerBillingAddress : PX.Objects.CR.Address
{
  /// <inheritdoc cref="P:PX.Objects.PM.PMAddress.AddressID" />
  [PXDBInt(BqlField = typeof (PMAddress.addressID))]
  public virtual int? ProjectAddressID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PM.PMAddress.IsDefaultAddress" />
  [PXDBBool(BqlField = typeof (PMAddress.isDefaultBillAddress))]
  public virtual bool? IsDefaultBillAddress { get; set; }

  public abstract class projectAddressID : 
    BqlType<IBqlInt, int>.Field<
    #nullable disable
    PMCustomerBillingAddress.projectAddressID>
  {
  }

  public abstract class isDefaultBillAddress : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    PMCustomerBillingAddress.isDefaultBillAddress>
  {
  }
}
