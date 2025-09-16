// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.DAC.ReportParameters.VendorReportParameters
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.AP;
using PX.Objects.CR;
using PX.Objects.EP;
using PX.Objects.PM;
using System.Collections.Generic;

#nullable enable
namespace PX.Objects.Common.DAC.ReportParameters;

[PXHidden]
public class VendorReportParameters : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [VendorReportParameters.format.List]
  [PXDBString(2)]
  [PXUIField]
  public 
  #nullable disable
  string Format { get; set; }

  [PXDBString(10, IsUnicode = true)]
  [PXUIField]
  [PXSelector(typeof (Search2<VendorClass.vendorClassID, LeftJoin<EPEmployeeClass, On<EPEmployeeClass.vendorClassID, Equal<VendorClass.vendorClassID>>>, Where<EPEmployeeClass.vendorClassID, IsNull>>))]
  public string VendorClassID { get; set; }

  [Vendor]
  [PXRestrictor(typeof (Where<PX.Objects.AP.Vendor.vStatus, NotEqual<VendorStatus.inactive>>), "The vendor status is '{0}'.", new System.Type[] {typeof (PX.Objects.AP.Vendor.vStatus)})]
  public int? VendorID { get; set; }

  [VendorActive]
  public int? VendorActiveID { get; set; }

  [Vendor(typeof (Search<BAccountR.bAccountID, Where<True, Equal<True>>>))]
  [VerndorNonEmployeeOrOrganizationRestrictor(typeof (PX.Objects.PO.POReceipt.receiptType))]
  [PXRestrictor(typeof (Where<PX.Objects.AP.Vendor.vStatus, IsNull, Or<PX.Objects.AP.Vendor.vStatus, Equal<VendorStatus.active>, Or<PX.Objects.AP.Vendor.vStatus, Equal<VendorStatus.oneTime>>>>), "The vendor status is '{0}'.", new System.Type[] {typeof (PX.Objects.AP.Vendor.vStatus)})]
  public virtual int? VendorIDPOReceipt { get; set; }

  [VendorNonEmployeeActive]
  public virtual int? VendorIDNonEmployeeActive { get; set; }

  [APActiveProject]
  public int? ProjectID { get; set; }

  [APAnyStateProject(typeof (Where<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PMProject.isActive, Equal<True>>>>, And<BqlOperand<PMProject.isCompleted, IBqlBool>.IsNotEqual<True>>>>.Or<BqlOperand<Optional<PMProject.isActive>, IBqlBool>.IsNotEqual<True>>>))]
  public int? AnyStateProjectID { get; set; }

  public abstract class format : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  VendorReportParameters.format>
  {
    public class ListAttribute : PXStringListAttribute
    {
      public const string Summary = "S";
      public const string Details = "D";
      public const string DetailsAll = "A";

      public ListAttribute()
        : base(VendorReportParameters.format.ListAttribute.GetAllowedValues(), VendorReportParameters.format.ListAttribute.GetAllowedLabels())
      {
      }

      public static string[] GetAllowedValues()
      {
        return new List<string>() { "S", "D", "A" }.ToArray();
      }

      public static string[] GetAllowedLabels()
      {
        return new List<string>()
        {
          "Document Summary",
          "Open Documents",
          "Open and Closed Documents"
        }.ToArray();
      }
    }
  }

  public abstract class vendorClassID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    VendorReportParameters.vendorClassID>
  {
  }

  public abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  VendorReportParameters.vendorID>
  {
  }

  public abstract class vendorActiveID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    VendorReportParameters.vendorActiveID>
  {
  }

  public abstract class vendorIDPOReceipt : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    VendorReportParameters.vendorIDPOReceipt>
  {
  }

  public abstract class vendorIDNonEmployeeActive : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    VendorReportParameters.vendorIDNonEmployeeActive>
  {
  }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  VendorReportParameters.projectID>
  {
  }

  public abstract class anyStateProjectID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    VendorReportParameters.anyStateProjectID>
  {
  }
}
