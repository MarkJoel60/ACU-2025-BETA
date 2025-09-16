// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.VendorR
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CR;
using PX.Objects.EP;
using System;

#nullable enable
namespace PX.Objects.AP;

[PXPrimaryGraph(new System.Type[] {typeof (VendorMaint), typeof (EmployeeMaint)}, new System.Type[] {typeof (Select<VendorR, Where2<Where<Vendor.type, Equal<BAccountType.vendorType>, Or<Vendor.type, Equal<BAccountType.combinedType>>>, And<VendorR.bAccountID, Equal<Current<PX.Objects.CR.BAccount.bAccountID>>>>>), typeof (Select<PX.Objects.EP.EPEmployee, Where<Vendor.type, Equal<BAccountType.employeeType>, And<PX.Objects.EP.EPEmployee.bAccountID, Equal<Current<PX.Objects.CR.BAccount.bAccountID>>>>>)})]
[PXODataDocumentTypesRestriction(typeof (VendorMaint))]
[PXSubstitute(GraphType = typeof (VendorMaint))]
[PXCacheName("Vendor")]
[Serializable]
public class VendorR : Vendor
{
  [VendorRaw(typeof (Where<Vendor.type, Equal<BAccountType.vendorType>, Or<Vendor.type, Equal<BAccountType.combinedType>>>), DescriptionField = typeof (Vendor.acctName), IsKey = true, DisplayName = "Vendor ID")]
  [PXDefault]
  [PXFieldDescription]
  [PXPersonalDataWarning]
  public override 
  #nullable disable
  string AcctCD
  {
    get => this._AcctCD;
    set => this._AcctCD = value;
  }

  /// <summary>
  /// The VendorR account name, which is the same as the <see cref="P:PX.Objects.AP.Vendor.AcctName">Vendor.AcctName</see> field.
  /// </summary>
  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Vendor Name", Visibility = PXUIVisibility.SelectorVisible)]
  [PXFieldDescription]
  [PXPersonalDataField]
  public override string AcctName
  {
    get => this._AcctName;
    set => this._AcctName = value;
  }

  public new class PK : PrimaryKeyOf<VendorR>.By<VendorR.bAccountID>
  {
    public static VendorR Find(PXGraph graph, int? bAccountID, PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<VendorR>.By<VendorR.bAccountID>.FindBy(graph, (object) bAccountID, options);
    }
  }

  public new abstract class bAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  VendorR.bAccountID>
  {
  }

  public new abstract class defLocationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  VendorR.defLocationID>
  {
  }

  public new abstract class primaryContactID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  VendorR.primaryContactID>
  {
  }

  public new abstract class acctCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  VendorR.acctCD>
  {
  }

  public new abstract class acctName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  VendorR.acctName>
  {
  }
}
