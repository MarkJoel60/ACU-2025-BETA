// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.BAccountStaffMember
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Objects.AP;
using PX.Objects.CR;
using PX.Objects.CR.MassProcess;
using System;

#nullable enable
namespace PX.Objects.FS;

[Serializable]
public class BAccountStaffMember : BAccountSelectorBase
{
  [PXDimensionSelector("BIZACCT", typeof (PX.Objects.CR.BAccount.acctCD), typeof (PX.Objects.CR.BAccount.acctCD))]
  [PXDBString(30, IsUnicode = true, IsKey = true, InputMask = "")]
  [PXDefault]
  [PXUIField]
  [PXFieldDescription]
  public override 
  #nullable disable
  string AcctCD { get; set; }

  [PXDBString(60, IsUnicode = true)]
  [PXDefault]
  [PXFieldDescription]
  [PXMassMergableField]
  [PXUIField]
  public override string AcctName { get; set; }

  [PXDBInt]
  [PXUIField]
  [PXDimensionSelector("BIZACCT", typeof (Search2<BAccountR.bAccountID, LeftJoin<PX.Objects.CR.Contact, On<PX.Objects.CR.Contact.bAccountID, Equal<BAccountR.bAccountID>, And<PX.Objects.CR.Contact.contactID, Equal<BAccountR.defContactID>>>, LeftJoin<PX.Objects.CR.Address, On<PX.Objects.CR.Address.bAccountID, Equal<BAccountR.bAccountID>, And<PX.Objects.CR.Address.addressID, Equal<BAccountR.defAddressID>>>>>, Where<BAccountR.type, Equal<BAccountType.customerType>, Or<BAccountR.type, Equal<BAccountType.prospectType>, Or<BAccountR.type, Equal<BAccountType.combinedType>, Or<BAccountR.type, Equal<BAccountType.vendorType>>>>>>), typeof (BAccountR.acctCD), new System.Type[] {typeof (BAccountR.acctCD), typeof (BAccountR.acctName), typeof (BAccountR.type), typeof (BAccountR.classID), typeof (BAccountR.status), typeof (PX.Objects.CR.Contact.phone1), typeof (PX.Objects.CR.Address.city), typeof (PX.Objects.CR.Address.countryID), typeof (PX.Objects.CR.Contact.eMail)}, DescriptionField = typeof (BAccountR.acctName))]
  public override int? ParentBAccountID { get; set; }

  [PXBool]
  [PXUIField(DisplayName = "Selected")]
  public override bool? Selected { get; set; }

  /// <summary>
  /// <inheritdoc cref="P:PX.Objects.EP.EPEmployee.VStatus" />
  /// </summary>
  [PXDBString(1, IsFixed = true)]
  [PXUIField(DisplayName = "Employee Status")]
  [VendorStatus.List]
  public override string VStatus { get; set; }

  public new abstract class bAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  BAccountStaffMember.bAccountID>
  {
  }

  public new abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  BAccountStaffMember.selected>
  {
  }

  public new abstract class vStatus : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BAccountStaffMember.vStatus>
  {
  }
}
