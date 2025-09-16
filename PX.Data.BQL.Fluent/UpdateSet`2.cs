// Decompiled with JetBrains decompiler
// Type: PX.Data.BQL.Fluent.UpdateSet`2
// Assembly: PX.Data.BQL.Fluent, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1086111-88AF-4F2E-BE39-D2C71848C2C0
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.BQL.Fluent.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.BQL.Fluent.xml

using PX.Common;

#nullable disable
namespace PX.Data.BQL.Fluent;

/// <exclude />
public abstract class UpdateSet<TTable, TAssigns> : 
  PX.Data.BQL.Fluent.Update<TTable, TAssigns, TypeArrayOf<IFbqlJoin>.Empty>
  where TTable : class, IBqlTable, new()
  where TAssigns : ITypeArrayOf<IFbqlSet>
{
  /// <summary>Adds another field assignment.</summary>
  public class Set<TAssign> : 
    UpdateSetMirror<TTable, TypeArrayOf<IFbqlSet>.Append<TAssigns, TAssign>>
    where TAssign : IFbqlSet
  {
  }
}
