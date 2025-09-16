// Decompiled with JetBrains decompiler
// Type: PX.Objects.CT.ContractAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CR;
using System.Collections.Generic;

#nullable enable
namespace PX.Objects.CT;

/// <summary>Contract Selector. Dispalys all contracts.</summary>
[PXDBInt]
[PXUIField]
public class ContractAttribute : PXEntityAttribute
{
  public const 
  #nullable disable
  string DimensionName = "CONTRACT";

  public ContractAttribute()
  {
    ((PXAggregateAttribute) this)._Attributes.Add((PXEventSubscriberAttribute) new PXDimensionSelectorAttribute("CONTRACT", typeof (Search2<Contract.contractID, InnerJoin<ContractBillingSchedule, On<ContractBillingSchedule.contractID, Equal<Contract.contractID>>, LeftJoin<BAccountR, On<BAccountR.bAccountID, Equal<Contract.customerID>>>>, Where<Contract.baseType, Equal<CTPRType.contract>>>), typeof (Contract.contractCD), new System.Type[8]
    {
      typeof (Contract.contractCD),
      typeof (Contract.customerID),
      typeof (Contract.locationID),
      typeof (Contract.description),
      typeof (Contract.status),
      typeof (Contract.expireDate),
      typeof (ContractBillingSchedule.lastDate),
      typeof (ContractBillingSchedule.nextDate)
    })
    {
      DescriptionField = typeof (Contract.description)
    });
    this._SelAttrIndex = ((List<PXEventSubscriberAttribute>) ((PXAggregateAttribute) this)._Attributes).Count - 1;
  }

  public ContractAttribute(System.Type WhereType)
  {
    ((PXAggregateAttribute) this)._Attributes.Add((PXEventSubscriberAttribute) new PXDimensionSelectorAttribute("CONTRACT", BqlCommand.Compose(new System.Type[10]
    {
      typeof (Search2<,,,>),
      typeof (Contract.contractID),
      typeof (InnerJoin<ContractBillingSchedule, On<ContractBillingSchedule.contractID, Equal<Contract.contractID>>, LeftJoin<BAccountR, On<BAccountR.bAccountID, Equal<Contract.customerID>>>>),
      typeof (Where<,,>),
      typeof (Contract.baseType),
      typeof (Equal<>),
      typeof (CTPRType.contract),
      typeof (And<>),
      WhereType,
      typeof (OrderBy<Desc<Contract.contractCD>>)
    }), typeof (Contract.contractCD), new System.Type[8]
    {
      typeof (Contract.contractCD),
      typeof (Contract.customerID),
      typeof (Contract.locationID),
      typeof (Contract.description),
      typeof (Contract.status),
      typeof (Contract.expireDate),
      typeof (ContractBillingSchedule.lastDate),
      typeof (ContractBillingSchedule.nextDate)
    })
    {
      DescriptionField = typeof (Contract.description)
    });
    this._SelAttrIndex = ((List<PXEventSubscriberAttribute>) ((PXAggregateAttribute) this)._Attributes).Count - 1;
  }

  public class dimension : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ContractAttribute.dimension>
  {
    public dimension()
      : base("CONTRACT")
    {
    }
  }
}
