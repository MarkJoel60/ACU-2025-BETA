// Decompiled with JetBrains decompiler
// Type: PX.Data.GenericInquiry.GenericInquiryDescriptionProvider
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Data.GenericInquiry;

internal class GenericInquiryDescriptionProvider : IGenericInquiryDescriptionProvider
{
  GIDescription IGenericInquiryDescriptionProvider.Get(Guid designId)
  {
    return PXGenericInqGrph.Def[designId];
  }

  GIDescription IGenericInquiryDescriptionProvider.GetByName(string name)
  {
    return PXGenericInqGrph.Def[name];
  }

  IEnumerable<GIDescription> IGenericInquiryDescriptionProvider.GetAll()
  {
    return (IEnumerable<GIDescription>) PXGenericInqGrph.Def;
  }
}
