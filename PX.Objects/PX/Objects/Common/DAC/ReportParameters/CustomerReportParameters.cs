// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.DAC.ReportParameters.CustomerReportParameters
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.AR;
using PX.Objects.CR;
using System.Collections.Generic;

#nullable enable
namespace PX.Objects.Common.DAC.ReportParameters;

[PXHidden]
public class CustomerReportParameters : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [CustomerReportParameters.format.List]
  [PXDBString(2)]
  [PXUIField]
  public 
  #nullable disable
  string Format { get; set; }

  [PXDBString(10, IsUnicode = true)]
  [PXUIField]
  [PXSelector(typeof (Search<CustomerClass.customerClassID>))]
  public string CustomerClassID { get; set; }

  [Customer]
  [PXDefault]
  public int? CustomerID { get; set; }

  [PXDBInt]
  [PXDimensionSelector("BIZACCT", typeof (Search<Customer.bAccountID, Where2<Where<Customer.customerClassID, Equal<Optional<CustomerReportParameters.customerClassID>>, Or<Optional<CustomerReportParameters.customerClassID>, IsNull>>, And<Match<Customer, BqlField<AccessInfo.userID, IBqlGuid>.FromCurrent>>>>), typeof (BAccountR.acctCD), new System.Type[] {typeof (BAccountR.acctCD), typeof (Customer.acctName), typeof (Customer.customerClassID), typeof (Customer.status), typeof (PX.Objects.CR.Contact.phone1), typeof (PX.Objects.CR.Address.city), typeof (PX.Objects.CR.Address.countryID)})]
  public int? CustomerIDByCustomerClass { get; set; }

  public abstract class format : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CustomerReportParameters.format>
  {
    public class ListAttribute : PXStringListAttribute
    {
      public const string Summary = "S";
      public const string Details = "D";
      public const string DetailsAll = "A";

      public ListAttribute()
        : base(CustomerReportParameters.format.ListAttribute.GetAllowedValues(), CustomerReportParameters.format.ListAttribute.GetAllowedLabels())
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

  public abstract class customerClassID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CustomerReportParameters.customerClassID>
  {
  }

  public abstract class customerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CustomerReportParameters.customerID>
  {
  }

  public abstract class customerIDByCustomerClass : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CustomerReportParameters.customerIDByCustomerClass>
  {
  }
}
