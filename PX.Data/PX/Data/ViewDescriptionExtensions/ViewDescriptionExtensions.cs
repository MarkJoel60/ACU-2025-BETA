// Decompiled with JetBrains decompiler
// Type: PX.Data.ViewDescriptionExtensions.ViewDescriptionExtensions
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api;
using PX.Data.Description;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data.ViewDescriptionExtensions;

internal static class ViewDescriptionExtensions
{
  internal static bool IsKeyField(this PXViewDescription viewDescription, string fieldName)
  {
    PX.Data.Description.FieldInfo fieldInfo = ((IEnumerable<PX.Data.Description.FieldInfo>) viewDescription.Fields).FirstOrDefault<PX.Data.Description.FieldInfo>((Func<PX.Data.Description.FieldInfo, bool>) (field => field.FieldName.OrdinalEquals(fieldName)));
    return fieldInfo != null && fieldInfo.IsKey;
  }
}
