// Decompiled with JetBrains decompiler
// Type: PX.Data.PXBaseFormulaBasedAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <exclude />
public class PXBaseFormulaBasedAttribute : PXEventSubscriberAttribute
{
  protected IBqlCreator _Formula;

  public virtual System.Type Formula
  {
    get => this._Formula?.GetType();
    set => this._Formula = PXFormulaAttribute.InitFormula(value);
  }

  public PXBaseFormulaBasedAttribute()
  {
  }

  public PXBaseFormulaBasedAttribute(System.Type formulaType)
    : this()
  {
    this._Formula = PXFormulaAttribute.InitFormula(formulaType);
  }

  public override PXEventSubscriberAttribute Clone(PXAttributeLevel attributeLevel)
  {
    return attributeLevel == PXAttributeLevel.Item ? (PXEventSubscriberAttribute) this : base.Clone(attributeLevel);
  }

  protected internal static TResult GetFormulaResult<TResult>(
    PXCache sender,
    object row,
    System.Type formulaType)
  {
    IBqlCreator formula = PXFormulaAttribute.InitFormula(formulaType);
    bool? result = new bool?();
    object formulaResult = (object) null;
    BqlFormula.Verify(sender, row, formula, ref result, ref formulaResult);
    return (TResult) formulaResult;
  }
}
