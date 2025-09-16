// Decompiled with JetBrains decompiler
// Type: PX.Data.BQL.SetOf
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;

#nullable disable
namespace PX.Data.BQL;

public static class SetOf
{
  public static class Integers
  {
    public abstract class ProvidedBy<TSetProvider> : 
      SetOfConstants<int, TSetProvider>,
      IBqlConstantsOf<IImplement<IBqlInt>>,
      IBqlConstants
      where TSetProvider : SetOfConstants<int, Equal<IBqlOperand>, TSetProvider>.ISetProvider, new()
    {
      /// <summary>
      /// Indicates whether the value of the passed field is present in the current set of constants.
      /// </summary>
      public new class Contains<TOperand> : 
        SetOfConstants<int, Equal<IBqlOperand>, TSetProvider>.Contains<TOperand>
        where TOperand : IBqlOperand, IImplement<IBqlCastableTo<IBqlInt>>, IImplement<IBqlEquitable>
      {
      }
    }

    public abstract class FilledWith<TConstants> : SetOfConstantsFluent<int, TConstants>.AsIntegers where TConstants : ITypeArrayOf<IConstant<int>>, TypeArray.IsNotEmpty
    {
    }

    public abstract class FilledWith<TConst1, TConst2> : 
      SetOfConstantsFluent<int, TypeArrayOf<IConstant<int>>.FilledWith<TConst1, TConst2>>.AsIntegers
      where TConst1 : IConstant<int>
      where TConst2 : IConstant<int>
    {
    }

    public abstract class FilledWith<TConst1, TConst2, TConst3> : 
      SetOfConstantsFluent<int, TypeArrayOf<IConstant<int>>.FilledWith<TConst1, TConst2, TConst3>>.AsIntegers
      where TConst1 : IConstant<int>
      where TConst2 : IConstant<int>
      where TConst3 : IConstant<int>
    {
    }

    public abstract class FilledWith<TConst1, TConst2, TConst3, TConst4> : 
      SetOfConstantsFluent<int, TypeArrayOf<IConstant<int>>.FilledWith<TConst1, TConst2, TConst3, TConst4>>.AsIntegers
      where TConst1 : IConstant<int>
      where TConst2 : IConstant<int>
      where TConst3 : IConstant<int>
      where TConst4 : IConstant<int>
    {
    }

    public abstract class FilledWith<TConst1, TConst2, TConst3, TConst4, TConst5> : 
      SetOfConstantsFluent<int, TypeArrayOf<IConstant<int>>.FilledWith<TConst1, TConst2, TConst3, TConst4, TConst5>>.AsIntegers
      where TConst1 : IConstant<int>
      where TConst2 : IConstant<int>
      where TConst3 : IConstant<int>
      where TConst4 : IConstant<int>
      where TConst5 : IConstant<int>
    {
    }

    public abstract class FilledWith<TConst1, TConst2, TConst3, TConst4, TConst5, TConst6> : 
      SetOfConstantsFluent<int, TypeArrayOf<IConstant<int>>.FilledWith<TConst1, TConst2, TConst3, TConst4, TConst5, TConst6>>.AsIntegers
      where TConst1 : IConstant<int>
      where TConst2 : IConstant<int>
      where TConst3 : IConstant<int>
      where TConst4 : IConstant<int>
      where TConst5 : IConstant<int>
      where TConst6 : IConstant<int>
    {
    }

    public abstract class FilledWith<TConst1, TConst2, TConst3, TConst4, TConst5, TConst6, TConst7> : 
      SetOfConstantsFluent<int, TypeArrayOf<IConstant<int>>.FilledWith<TConst1, TConst2, TConst3, TConst4, TConst5, TConst6, TConst7>>.AsIntegers
      where TConst1 : IConstant<int>
      where TConst2 : IConstant<int>
      where TConst3 : IConstant<int>
      where TConst4 : IConstant<int>
      where TConst5 : IConstant<int>
      where TConst6 : IConstant<int>
      where TConst7 : IConstant<int>
    {
    }

    public abstract class FilledWith<TConst1, TConst2, TConst3, TConst4, TConst5, TConst6, TConst7, TConst8> : 
      SetOfConstantsFluent<int, TypeArrayOf<IConstant<int>>.FilledWith<TConst1, TConst2, TConst3, TConst4, TConst5, TConst6, TConst7, TConst8>>.AsIntegers
      where TConst1 : IConstant<int>
      where TConst2 : IConstant<int>
      where TConst3 : IConstant<int>
      where TConst4 : IConstant<int>
      where TConst5 : IConstant<int>
      where TConst6 : IConstant<int>
      where TConst7 : IConstant<int>
      where TConst8 : IConstant<int>
    {
    }
  }

  public static class Strings
  {
    public abstract class ProvidedBy<TSetProvider> : 
      SetOfConstants<string, TSetProvider>,
      IBqlConstantsOf<IImplement<IBqlString>>,
      IBqlConstants
      where TSetProvider : SetOfConstants<string, Equal<IBqlOperand>, TSetProvider>.ISetProvider, new()
    {
      /// <summary>
      /// Indicates whether the value of the passed field is present in the current set of constants.
      /// </summary>
      public new class Contains<TOperand> : 
        SetOfConstants<string, Equal<IBqlOperand>, TSetProvider>.Contains<TOperand>
        where TOperand : IBqlOperand, IImplement<IBqlCastableTo<IBqlString>>, IImplement<IBqlEquitable>
      {
      }
    }

    public abstract class FilledWith<TConstants> : SetOfConstantsFluent<string, TConstants>.AsStrings
      where TConstants : ITypeArrayOf<IConstant<string>>, TypeArray.IsNotEmpty
    {
    }

    public abstract class FilledWith<TConst1, TConst2> : 
      SetOfConstantsFluent<string, TypeArrayOf<IConstant<string>>.FilledWith<TConst1, TConst2>>.AsStrings
      where TConst1 : IConstant<string>
      where TConst2 : IConstant<string>
    {
    }

    public abstract class FilledWith<TConst1, TConst2, TConst3> : 
      SetOfConstantsFluent<string, TypeArrayOf<IConstant<string>>.FilledWith<TConst1, TConst2, TConst3>>.AsStrings
      where TConst1 : IConstant<string>
      where TConst2 : IConstant<string>
      where TConst3 : IConstant<string>
    {
    }

    public abstract class FilledWith<TConst1, TConst2, TConst3, TConst4> : 
      SetOfConstantsFluent<string, TypeArrayOf<IConstant<string>>.FilledWith<TConst1, TConst2, TConst3, TConst4>>.AsStrings
      where TConst1 : IConstant<string>
      where TConst2 : IConstant<string>
      where TConst3 : IConstant<string>
      where TConst4 : IConstant<string>
    {
    }

    public abstract class FilledWith<TConst1, TConst2, TConst3, TConst4, TConst5> : 
      SetOfConstantsFluent<string, TypeArrayOf<IConstant<string>>.FilledWith<TConst1, TConst2, TConst3, TConst4, TConst5>>.AsStrings
      where TConst1 : IConstant<string>
      where TConst2 : IConstant<string>
      where TConst3 : IConstant<string>
      where TConst4 : IConstant<string>
      where TConst5 : IConstant<string>
    {
    }

    public abstract class FilledWith<TConst1, TConst2, TConst3, TConst4, TConst5, TConst6> : 
      SetOfConstantsFluent<string, TypeArrayOf<IConstant<string>>.FilledWith<TConst1, TConst2, TConst3, TConst4, TConst5, TConst6>>.AsStrings
      where TConst1 : IConstant<string>
      where TConst2 : IConstant<string>
      where TConst3 : IConstant<string>
      where TConst4 : IConstant<string>
      where TConst5 : IConstant<string>
      where TConst6 : IConstant<string>
    {
    }

    public abstract class FilledWith<TConst1, TConst2, TConst3, TConst4, TConst5, TConst6, TConst7> : 
      SetOfConstantsFluent<string, TypeArrayOf<IConstant<string>>.FilledWith<TConst1, TConst2, TConst3, TConst4, TConst5, TConst6, TConst7>>.AsStrings
      where TConst1 : IConstant<string>
      where TConst2 : IConstant<string>
      where TConst3 : IConstant<string>
      where TConst4 : IConstant<string>
      where TConst5 : IConstant<string>
      where TConst6 : IConstant<string>
      where TConst7 : IConstant<string>
    {
    }

    public abstract class FilledWith<TConst1, TConst2, TConst3, TConst4, TConst5, TConst6, TConst7, TConst8> : 
      SetOfConstantsFluent<string, TypeArrayOf<IConstant<string>>.FilledWith<TConst1, TConst2, TConst3, TConst4, TConst5, TConst6, TConst7, TConst8>>.AsStrings
      where TConst1 : IConstant<string>
      where TConst2 : IConstant<string>
      where TConst3 : IConstant<string>
      where TConst4 : IConstant<string>
      where TConst5 : IConstant<string>
      where TConst6 : IConstant<string>
      where TConst7 : IConstant<string>
      where TConst8 : IConstant<string>
    {
    }
  }
}
