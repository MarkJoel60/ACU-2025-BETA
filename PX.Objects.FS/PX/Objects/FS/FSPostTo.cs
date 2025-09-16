// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSPostTo
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using System;
using System.Collections.Generic;

#nullable enable
namespace PX.Objects.FS;

public class FSPostTo
{
  public static 
  #nullable disable
  FSPostTo.CustomListAttribute GetDropDownList(
    bool showSOInvoice,
    bool showNone,
    bool showProjects,
    bool showAPAR)
  {
    List<Tuple<string, string>> tupleList = new List<Tuple<string, string>>();
    if (showAPAR)
      tupleList.Add(new Tuple<string, string>("AA", "AR Documents and/or AP Bills"));
    else
      tupleList.Add(new Tuple<string, string>("AR", "AR Documents"));
    if (PXAccess.FeatureInstalled<FeaturesSet.distributionModule>())
    {
      tupleList.Add(new Tuple<string, string>("SO", "Sales Orders"));
      if (PXAccess.FeatureInstalled<FeaturesSet.advancedSOInvoices>() & showSOInvoice)
        tupleList.Add(new Tuple<string, string>("SI", "SO Invoices"));
    }
    if (PXAccess.FeatureInstalled<FeaturesSet.projectAccounting>() & showProjects)
      tupleList.Add(new Tuple<string, string>("PM", "Project Transactions"));
    if (showNone)
      tupleList.Add(new Tuple<string, string>("NA", "None"));
    return new FSPostTo.CustomListAttribute(tupleList.ToArray());
  }

  public static void SetLineTypeList<LineTypeField>(
    PXCache cache,
    object row,
    bool showSOInvoice = false,
    bool showNone = false,
    bool showProjects = false,
    bool showAPAR = false)
    where LineTypeField : class, IBqlField
  {
    FSPostTo.CustomListAttribute dropDownList = FSPostTo.GetDropDownList(showSOInvoice, showNone, showProjects, showAPAR);
    PXStringListAttribute.SetList<LineTypeField>(cache, row, dropDownList.AllowedValues, dropDownList.AllowedLabels);
  }

  public class CustomListAttribute : PXStringListAttribute
  {
    public string[] AllowedValues => this._AllowedValues;

    public string[] AllowedLabels => this._AllowedLabels;

    public CustomListAttribute(string[] allowedValues, string[] allowedLabels)
      : base(allowedValues, allowedLabels)
    {
    }

    public CustomListAttribute(Tuple<string, string>[] valuesToLabels)
      : base(valuesToLabels)
    {
    }
  }

  public class ListAttribute : FSPostTo.CustomListAttribute
  {
    public ListAttribute()
      : base(new Tuple<string, string>[6]
      {
        PXStringListAttribute.Pair("AA", "AR Documents and/or AP Bills"),
        PXStringListAttribute.Pair("AR", "AR Documents"),
        PXStringListAttribute.Pair("SO", "Sales Orders"),
        PXStringListAttribute.Pair("SI", "SO Invoices"),
        PXStringListAttribute.Pair("PM", "Project Transactions"),
        PXStringListAttribute.Pair("NA", "None")
      })
    {
    }
  }

  public class None : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  FSPostTo.None>
  {
    public None()
      : base("NA")
    {
    }
  }

  public class Accounts_Receivable_Module : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    FSPostTo.Accounts_Receivable_Module>
  {
    public Accounts_Receivable_Module()
      : base("AR")
    {
    }
  }

  public class Sales_Order_Module : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  FSPostTo.Sales_Order_Module>
  {
    public Sales_Order_Module()
      : base("SO")
    {
    }
  }

  public class Sales_Order_Invoice : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    FSPostTo.Sales_Order_Invoice>
  {
    public Sales_Order_Invoice()
      : base("SI")
    {
    }
  }

  public class Projects : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  FSPostTo.Projects>
  {
    public Projects()
      : base("PM")
    {
    }
  }
}
