// Decompiled with JetBrains decompiler
// Type: PX.Data.PreventEditOf`8
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;

#nullable disable
namespace PX.Data;

public sealed class PreventEditOf<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8> : 
  EditPreventor<TypeArrayOf<IBqlField>.FilledWith<TField1, TField2, TField3, TField4, TField5, TField6, TField7, TField8>>
  where TField1 : class, IBqlField
  where TField2 : class, IBqlField
  where TField3 : class, IBqlField
  where TField4 : class, IBqlField
  where TField5 : class, IBqlField
  where TField6 : class, IBqlField
  where TField7 : class, IBqlField
  where TField8 : class, IBqlField
{
}
