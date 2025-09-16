// Decompiled with JetBrains decompiler
// Type: PX.Data.BQL.Fluent.FbqlCommand
// Assembly: PX.Data.BQL.Fluent, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B1086111-88AF-4F2E-BE39-D2C71848C2C0
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.BQL.Fluent.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.BQL.Fluent.xml

using System;
using System.Collections.Concurrent;

#nullable disable
namespace PX.Data.BQL.Fluent;

public class FbqlCommand : BqlCommandDecorator, IHasBqlWhere
{
  internal static readonly ConcurrentDictionary<Type, FbqlParseResult> FbqlToBqlMap = new ConcurrentDictionary<Type, FbqlParseResult>();

  public static Type UnwrapCommandType(Type bqlCommandType)
  {
    if (!typeof (FbqlCommand).IsAssignableFrom(bqlCommandType))
      return bqlCommandType;
    FbqlParseResult fbqlParseResult;
    return FbqlCommand.FbqlToBqlMap.TryGetValue(bqlCommandType, out fbqlParseResult) ? fbqlParseResult.BqlCommand : ((BqlCommandDecorator) Activator.CreateInstance(bqlCommandType)).GetOriginalType();
  }

  protected FbqlCommand(BqlCommand bqlCommand)
    : base(bqlCommand)
  {
  }

  public IBqlWhere GetWhere() => ((IHasBqlWhere) this.InnerBqlCommand).GetWhere();
}
