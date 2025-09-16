// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.Matrix.Attributes.TemplateUnitAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using System;

#nullable disable
namespace PX.Objects.IN.Matrix.Attributes;

public class TemplateUnitAttribute : INUnitAttribute
{
  public TemplateUnitAttribute(Type inventoryID, Type templateID)
    : base(INUnitAttribute.VerifyingMode.InventoryUnitConversion)
  {
    this.InventoryType = inventoryID;
    Type type = ((IBqlTemplate) BqlTemplate.OfCommand<SearchFor<INUnit.fromUnit>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INUnit.unitType, Equal<INUnitType.inventoryItem>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INUnit.inventoryID, Equal<BqlField<BqlPlaceholder.A, BqlPlaceholder.IBqlAny>.AsOptional.NoDefault>>>>>.Or<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<Optional2<BqlPlaceholder.A>, IsNull>>>>.And<BqlOperand<INUnit.inventoryID, IBqlInt>.IsEqual<BqlField<BqlPlaceholder.B, BqlPlaceholder.IBqlAny>.AsOptional.NoDefault>>>>>>.Replace<BqlPlaceholder.A>(inventoryID).Replace<BqlPlaceholder.B>(templateID)).ToType();
    this.Init(type, type);
  }
}
