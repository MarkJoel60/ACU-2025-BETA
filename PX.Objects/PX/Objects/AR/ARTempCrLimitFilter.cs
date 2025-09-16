// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARTempCrLimitFilter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.AR;

[Obsolete("This DAC is not used anymore and will be removed in Acumatica ERP 8.0.")]
[PXPrimaryGraph(typeof (ARTempCrLimitMaint))]
[PXHidden]
[Serializable]
public class ARTempCrLimitFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _CustomerID;

  [CustomerActive(DescriptionField = typeof (Customer.acctName))]
  [PXDefault]
  [PXUIField(DisplayName = "Customer")]
  public virtual int? CustomerID
  {
    get => this._CustomerID;
    set => this._CustomerID = value;
  }

  public abstract class customerID : BqlType<IBqlInt, int>.Field<
  #nullable disable
  ARTempCrLimitFilter.customerID>
  {
  }
}
