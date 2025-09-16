// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Compliance.CL.Graphs.Extensions.ComplianceDocumentTypeExtension`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Objects.CN.Compliance.CL.DAC;

#nullable disable
namespace PX.Objects.CN.Compliance.CL.Graphs.Extensions;

public class ComplianceDocumentTypeExtension<TGraph> : PXGraphExtension<TGraph> where TGraph : PXGraph<TGraph>
{
  public virtual ComplianceAttributeType GetComplianceDocumentType(string type)
  {
    return ((PXSelectBase<ComplianceAttributeType>) new FbqlSelect<SelectFromBase<ComplianceAttributeType, TypeArrayOf<IFbqlJoin>.Empty>.Where<BqlOperand<ComplianceAttributeType.type, IBqlString>.IsEqual<P.AsString>>, ComplianceAttributeType>.View((PXGraph) (object) this.Base)).SelectSingle(new object[1]
    {
      (object) type
    });
  }

  public virtual ComplianceAttribute GetComplianceDocumentTypeValue(string type, string typeValue)
  {
    return ((PXSelectBase<ComplianceAttribute>) new FbqlSelect<SelectFromBase<ComplianceAttribute, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<ComplianceAttributeType>.On<BqlOperand<ComplianceAttributeType.complianceAttributeTypeID, IBqlInt>.IsEqual<ComplianceAttribute.type>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<ComplianceAttributeType.type, Equal<P.AsString>>>>>.And<BqlOperand<ComplianceAttribute.value, IBqlString>.IsEqual<P.AsString>>>, ComplianceAttribute>.View((PXGraph) (object) this.Base)).SelectSingle(new object[2]
    {
      (object) type,
      (object) typeValue
    });
  }

  public virtual ComplianceAttributeType GetLienWaiverDocumentType()
  {
    return this.GetComplianceDocumentType("Lien Waiver");
  }

  public virtual ComplianceAttribute GetLienWaiverDocumentTypeValue(string typeValue)
  {
    return this.GetComplianceDocumentTypeValue("Lien Waiver", typeValue);
  }

  public virtual ComplianceAttribute GetLienWaiverConditionalPartialType()
  {
    return this.GetComplianceDocumentTypeValue("Lien Waiver", "Conditional Partial");
  }

  public virtual ComplianceAttribute GetLienWaiverUnconditionalPartialType()
  {
    return this.GetComplianceDocumentTypeValue("Lien Waiver", "Unconditional Partial");
  }

  public virtual ComplianceAttribute GetLienWaiverConditionalFinalType()
  {
    return this.GetComplianceDocumentTypeValue("Lien Waiver", "Conditional Final");
  }

  public virtual ComplianceAttribute GetLienWaiverUnconditionalFinalType()
  {
    return this.GetComplianceDocumentTypeValue("Lien Waiver", "Unconditional Final");
  }
}
