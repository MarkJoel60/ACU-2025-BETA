// Decompiled with JetBrains decompiler
// Type: PX.Objects.Extensions.PXUserSetup`5
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.Extensions;

public class PXUserSetup<TSelf, TGraph, THeader, TSetup, TUserIDField> : 
  PXSetupBase<TSelf, TGraph, THeader, TSetup, Where<TUserIDField, Equal<Current<AccessInfo.userID>>>>
  where TSelf : PXUserSetup<TSelf, TGraph, THeader, TSetup, TUserIDField>
  where TGraph : PXGraph
  where THeader : class, IBqlTable, new()
  where TSetup : class, IBqlTable, new()
  where TUserIDField : IBqlField
{
}
