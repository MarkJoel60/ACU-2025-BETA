// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMCustomerBillingContact
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
[PXProjection(typeof (SelectFromBase<PX.Objects.CR.Contact, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.AR.Customer>.On<BqlOperand<PX.Objects.CR.Contact.contactID, IBqlInt>.IsEqual<PX.Objects.AR.Customer.defBillContactID>>>, FbqlJoins.Inner<PMContact>.On<BqlOperand<PX.Objects.AR.Customer.bAccountID, IBqlInt>.IsEqual<PMContact.customerID>>>>.Where<BqlOperand<PMContact.isDefaultContact, IBqlBool>.IsEqual<True>>))]
[Serializable]
public class PMCustomerBillingContact : PX.Objects.CR.Contact
{
  /// <inheritdoc cref="P:PX.Objects.PM.PMContact.ContactID" />
  [PXDBInt(BqlField = typeof (PMContact.contactID))]
  public virtual int? ProjectContactID { get; set; }

  /// <inheritdoc cref="P:PX.Objects.PM.PMContact.IsDefaultContact" />
  [PXDBBool(BqlField = typeof (PMContact.isDefaultContact))]
  public virtual bool? IsDefaultContact { get; set; }

  public abstract class projectContactID : 
    BqlType<IBqlInt, int>.Field<
    #nullable disable
    PMCustomerBillingContact.projectContactID>
  {
  }

  public abstract class isDefaultContact : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    PMCustomerBillingContact.isDefaultContact>
  {
  }
}
