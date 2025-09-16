// Decompiled with JetBrains decompiler
// Type: PX.Objects.CT.ContractTemplateAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;
using System.Collections.Generic;

#nullable enable
namespace PX.Objects.CT;

/// <summary>
/// Contract Template Selector. Displays all Contract Templates.
/// </summary>
[PXDBInt]
[PXUIField]
public class ContractTemplateAttribute : PXEntityAttribute
{
  public const 
  #nullable disable
  string DimensionName = "TMCONTRACT";

  public ContractTemplateAttribute()
  {
    ((PXAggregateAttribute) this)._Attributes.Add((PXEventSubscriberAttribute) new PXDimensionSelectorAttribute("TMCONTRACT", typeof (Search<ContractTemplate.contractID, Where<ContractTemplate.baseType, Equal<CTPRType.contractTemplate>>>), typeof (ContractTemplate.contractCD), new Type[3]
    {
      typeof (ContractTemplate.contractCD),
      typeof (ContractTemplate.description),
      typeof (ContractTemplate.status)
    })
    {
      DescriptionField = typeof (ContractTemplate.description)
    });
    this._SelAttrIndex = ((List<PXEventSubscriberAttribute>) ((PXAggregateAttribute) this)._Attributes).Count - 1;
  }

  public ContractTemplateAttribute(Type WhereType)
  {
    ((PXAggregateAttribute) this)._Attributes.Add((PXEventSubscriberAttribute) new PXDimensionSelectorAttribute("TMCONTRACT", BqlCommand.Compose(new Type[8]
    {
      typeof (Search<,>),
      typeof (ContractTemplate.contractID),
      typeof (Where<,,>),
      typeof (ContractTemplate.baseType),
      typeof (Equal<>),
      typeof (CTPRType.contractTemplate),
      typeof (And<>),
      WhereType
    }), typeof (ContractTemplate.contractCD), new Type[3]
    {
      typeof (ContractTemplate.contractCD),
      typeof (ContractTemplate.description),
      typeof (ContractTemplate.status)
    })
    {
      DescriptionField = typeof (ContractTemplate.description)
    });
    this._SelAttrIndex = ((List<PXEventSubscriberAttribute>) ((PXAggregateAttribute) this)._Attributes).Count - 1;
  }

  public class dimension : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ContractTemplateAttribute.dimension>
  {
    public dimension()
      : base("TMCONTRACT")
    {
    }
  }
}
