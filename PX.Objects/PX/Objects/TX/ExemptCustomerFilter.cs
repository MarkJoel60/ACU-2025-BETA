// Decompiled with JetBrains decompiler
// Type: PX.Objects.TX.ExemptCustomerFilter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.TX;

/// <summary>
/// An unbound DAC that is used for the filtering parameters on the Manage Exempt Customers (TX505000) form.
/// </summary>
[PXHidden]
[Serializable]
public class ExemptCustomerFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  /// <summary>
  /// The action that should be performed on customer records.
  /// </summary>
  [PXString]
  [PXUIField]
  [PXStringList(new string[] {"Create Customer in ECM Provider", "Update Customer in ECM Provider"}, new string[] {"Create Customer in ECM Provider", "Update Customer in ECM Provider"})]
  public virtual 
  #nullable disable
  string Action { get; set; }

  /// <summary>
  /// The company codes for which customer records are processed in the exemption certificate management (ECM) system.
  /// </summary>
  [PXString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField(DisplayName = "Company Code")]
  [PXSelector(typeof (Search5<TaxPluginMapping.companyCode, InnerJoin<TaxPlugin, On<TaxPluginMapping.taxPluginID, Equal<TaxPlugin.taxPluginID>>, InnerJoin<TXSetup, On<TaxPlugin.taxPluginID, Equal<TXSetup.eCMProvider>>>>, Aggregate<GroupBy<TaxPluginMapping.companyCode>>>), ValidateValue = false)]
  public virtual string CompanyCode { get; set; }

  public abstract class action : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ExemptCustomerFilter.action>
  {
  }

  public abstract class companyCode : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ExemptCustomerFilter.companyCode>
  {
  }
}
