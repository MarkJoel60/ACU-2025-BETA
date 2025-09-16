// Decompiled with JetBrains decompiler
// Type: ReconciliationTools.ARGLDiscrepancyByCustomerEnqFilter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data.BQL;
using PX.Objects.AR;
using System;

#nullable enable
namespace ReconciliationTools;

[Serializable]
public class ARGLDiscrepancyByCustomerEnqFilter : ARGLDiscrepancyEnqFilter
{
  [Customer(DescriptionField = typeof (Customer.acctName))]
  public virtual int? CustomerID { get; set; }

  public abstract class customerID : 
    BqlType<IBqlInt, int>.Field<
    #nullable disable
    ARGLDiscrepancyByCustomerEnqFilter.customerID>
  {
  }
}
