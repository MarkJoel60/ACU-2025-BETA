// Decompiled with JetBrains decompiler
// Type: PX.Data.BQL.SetOfConstantsFluent`2
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;

#nullable disable
namespace PX.Data.BQL;

public abstract class SetOfConstantsFluent<TConstantType, TConstants> : 
  SetOfConstants<TConstantType, SetOfConstantsFluent<TConstantType, TConstants>.Provider>
  where TConstants : ITypeArrayOf<IConstant<TConstantType>>, TypeArray.IsNotEmpty
{
  public sealed class Provider : 
    SetOfConstants<TConstantType, Equal<IBqlOperand>, SetOfConstantsFluent<TConstantType, TConstants>.Provider>.ISetProvider
  {
    public IConstant<TConstantType>[] Constants
    {
      get
      {
        return TypeArrayOf<IConstant<TConstantType>>.CheckAndExtractInstances(typeof (TConstants), (string) null);
      }
    }
  }

  public abstract class AsIntegers : 
    SetOfConstantsFluent<TConstantType, TConstants>,
    IBqlConstantsOf<IImplement<IBqlInt>>,
    IBqlConstants
  {
    /// <summary>
    /// Indicates whether the value of the passed field is present in the current set of constants.
    /// </summary>
    public new class Contains<TOperand> : 
      SetOfConstants<TConstantType, Equal<IBqlOperand>, SetOfConstantsFluent<TConstantType, TConstants>.Provider>.Contains<TOperand>
      where TOperand : IBqlOperand, IImplement<IBqlCastableTo<IBqlInt>>, IImplement<IBqlEquitable>
    {
    }
  }

  public abstract class AsStrings : 
    SetOfConstantsFluent<TConstantType, TConstants>,
    IBqlConstantsOf<IImplement<IBqlString>>,
    IBqlConstants,
    IBqlConstantsOf<IBqlOperand>,
    IBqlConstantsOf<IBqlAggregatedOperand>
  {
    /// <summary>
    /// Indicates whether the value of the passed field is present in the current set of constants.
    /// </summary>
    public new class Contains<TOperand> : 
      SetOfConstants<TConstantType, Equal<IBqlOperand>, SetOfConstantsFluent<TConstantType, TConstants>.Provider>.Contains<TOperand>
      where TOperand : IBqlOperand, IImplement<IBqlCastableTo<IBqlString>>, IImplement<IBqlEquitable>
    {
    }
  }
}
