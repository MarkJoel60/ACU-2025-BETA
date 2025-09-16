// Decompiled with JetBrains decompiler
// Type: PX.Data.BQL.Fluent.By`6
// Assembly: PX.Data.BQL.Fluent, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1086111-88AF-4F2E-BE39-D2C71848C2C0
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.BQL.Fluent.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.BQL.Fluent.xml

using PX.Common;

#nullable disable
namespace PX.Data.BQL.Fluent;

/// <exclude />
public class By<TSortColumn1, TSortColumn2, TSortColumn3, TSortColumn4, TSortColumn5, TSortColumn6> : 
  TypeArrayOf<IBqlSortColumn>.FilledWith<TSortColumn1, TSortColumn2, TSortColumn3, TSortColumn4, TSortColumn5, TSortColumn6>
  where TSortColumn1 : IBqlSortColumn
  where TSortColumn2 : IBqlSortColumn
  where TSortColumn3 : IBqlSortColumn
  where TSortColumn4 : IBqlSortColumn
  where TSortColumn5 : IBqlSortColumn
  where TSortColumn6 : IBqlSortColumn
{
}
