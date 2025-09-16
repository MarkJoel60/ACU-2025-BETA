// Decompiled with JetBrains decompiler
// Type: PX.Data.BQL.Fluent.SearchFor`1
// Assembly: PX.Data.BQL.Fluent, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1086111-88AF-4F2E-BE39-D2C71848C2C0
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.BQL.Fluent.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.BQL.Fluent.xml

#nullable disable
namespace PX.Data.BQL.Fluent;

/// <summary>Declares a search for a certain field.</summary>
/// <typeparam name="TField">Searched field.</typeparam>
public class SearchFor<TField> : FbqlSearch<TField> where TField : IBqlField
{
  public SearchFor()
    : base((BqlCommand) new Search<TField>())
  {
  }

  private static BqlCommand ConvertSelectToSearch<TSelect>() where TSelect : IFbqlSelect<IBqlTable>
  {
    return FbqlCommandParser.ConvertSelectToSearch<TField>(FbqlCommand.UnwrapCommandType(typeof (TSelect)));
  }

  public class Where<TCondition> : FbqlSearch<TField> where TCondition : IBqlUnary, new()
  {
    public Where()
      : base((BqlCommand) new Search<TField, Where<TCondition>>())
    {
    }
  }

  /// <summary>
  /// Declares a Select command in which results a certain field will be searched
  /// </summary>
  /// <typeparam name="TSelect">Select command in which results a certain field will be searched</typeparam>
  public class In<TSelect> : FbqlSearch<TField> where TSelect : IFbqlSelect<IBqlTable>
  {
    public In()
      : base(SearchFor<TField>.ConvertSelectToSearch<TSelect>())
    {
    }
  }
}
