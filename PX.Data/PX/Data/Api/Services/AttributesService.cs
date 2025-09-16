// Decompiled with JetBrains decompiler
// Type: PX.Data.Api.Services.AttributesService
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.CS;

#nullable disable
namespace PX.Data.Api.Services;

public class AttributesService : IAttributesService
{
  public KeyValueHelper.AttributeFieldInfo[] GetUserDefinedFields(string screenId)
  {
    return KeyValueHelper.GetAttributeFieldsExtended(screenId);
  }

  public System.DateTime LastChangedDate() => KeyValueHelper.LastChangedDateUTC;
}
