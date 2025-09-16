// Decompiled with JetBrains decompiler
// Type: PX.Data.PXAppCodeTypeBinder
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common.Extensions;
using System.Runtime.Serialization;
using System.Web.Compilation;

#nullable disable
namespace PX.Data;

/// <exclude />
internal class PXAppCodeTypeBinder : SerializationBinder
{
  public override System.Type BindToType(string assemblyName, string typeName)
  {
    switch (typeName)
    {
      case "PX.Data.GridPreferences+ColumnPref":
        return typeof (GridPreferences.ColumnPref);
      case "System.UnitySerializationHolder":
        return typeof (UnitySerializationHolderProxy);
      default:
        string typeName1 = $"{typeName}, {assemblyName}";
        System.Type type = (System.Type) null;
        try
        {
          type = System.Type.GetType(typeName1, false);
        }
        catch
        {
        }
        if (type == (System.Type) null)
        {
          try
          {
            type = PXBuildManager.GetType(typeName, false);
          }
          catch
          {
          }
        }
        if (type == (System.Type) null)
          type = System.Type.GetType($"{typeName}, {StringExtensions.FirstSegment(assemblyName, ',')}", false);
        return type;
    }
  }

  public override void BindToName(
    System.Type serializedType,
    out string assemblyName,
    out string typeName)
  {
    base.BindToName(serializedType, out assemblyName, out typeName);
  }
}
