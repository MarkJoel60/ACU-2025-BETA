// Decompiled with JetBrains decompiler
// Type: PX.Data.BQL.Validations.LineCounterStrongValidation`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Data.BQL.Validations;

internal class LineCounterStrongValidation<TDataType> : LineCounterValidation<TDataType> where TDataType : IComparable<TDataType>
{
  protected override IEnumerable<TDataType> GetLineNumbers() => base.GetLineNumbers();
}
