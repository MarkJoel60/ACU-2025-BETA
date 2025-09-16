// Decompiled with JetBrains decompiler
// Type: PX.CloudServices.DAC.RecognizedRecordEntityTypeListAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace PX.CloudServices.DAC;

[PXInternalUseOnly]
public class RecognizedRecordEntityTypeListAttribute : PXStringListAttribute
{
  public RecognizedRecordEntityTypeListAttribute()
    : base(EnumerableExtensions.Distinct<KeyValuePair<string, Model>, string>((IEnumerable<KeyValuePair<string, Model>>) Models.KnownModels, (Func<KeyValuePair<string, Model>, string>) (_ => _.Value.DocumentType)).Select<KeyValuePair<string, Model>, (string, string)>((Func<KeyValuePair<string, Model>, (string, string)>) (c => (c.Value.DocumentType, c.Value.DocumentTypeLabel))).ToArray<(string, string)>())
  {
  }

  public sealed class aPDocument : 
    BqlType<IBqlString, string>.Constant<
    #nullable disable
    RecognizedRecordEntityTypeListAttribute.aPDocument>
  {
    public aPDocument()
      : base("INV")
    {
    }
  }
}
