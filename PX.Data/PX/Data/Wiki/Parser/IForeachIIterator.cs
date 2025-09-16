// Decompiled with JetBrains decompiler
// Type: PX.Data.Wiki.Parser.IForeachIIterator
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace PX.Data.Wiki.Parser;

internal interface IForeachIIterator : 
  IEnumerable<object>,
  IEnumerable,
  IEnumerator<object>,
  IDisposable,
  IEnumerator
{
  string GetValue(string fieldName);

  string GetValue(string fieldName, object row);
}
