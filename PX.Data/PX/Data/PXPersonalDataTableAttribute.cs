// Decompiled with JetBrains decompiler
// Type: PX.Data.PXPersonalDataTableAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

#nullable disable
namespace PX.Data;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
public class PXPersonalDataTableAttribute : Attribute
{
  private static Dictionary<System.Type, Dictionary<System.Type, List<BqlCommand>>> Links;
  private BqlCommand primaryEntitySearch;
  private System.Type parentEntityType;

  public PXPersonalDataTableAttribute(System.Type primaryEntitySearchType)
  {
    if (!typeof (IBqlSelect).IsAssignableFrom(primaryEntitySearchType))
      return;
    this.primaryEntitySearch = BqlCommand.CreateInstance(primaryEntitySearchType);
    this.parentEntityType = ((IEnumerable<IBqlParameter>) this.primaryEntitySearch.GetParameters()).FirstOrDefault<IBqlParameter>()?.GetReferencedType()?.DeclaringType;
  }

  public static Dictionary<System.Type, List<BqlCommand>> GetEntitiesMapping(System.Type parentTable)
  {
    if (parentTable == (System.Type) null)
      return new Dictionary<System.Type, List<BqlCommand>>();
    DacMetadata.InitializationCompleted.Wait();
    Dictionary<System.Type, List<BqlCommand>> dictionary = new Dictionary<System.Type, List<BqlCommand>>();
    if (PXPersonalDataTableAttribute.Links != null)
      return PXPersonalDataTableAttribute.Links.TryGetValue(parentTable, out dictionary) ? dictionary : new Dictionary<System.Type, List<BqlCommand>>();
    PXGraph graph = new PXGraph();
    PXPersonalDataTableAttribute.Links = new Dictionary<System.Type, Dictionary<System.Type, List<BqlCommand>>>();
    foreach (System.Type type in PXPersonalDataFieldAttribute.TablesWithFieldsSpecified)
    {
      foreach (PXPersonalDataTableAttribute customAttribute in type.GetCustomAttributes(typeof (PXPersonalDataTableAttribute), false))
      {
        if (!(customAttribute.parentEntityType == (System.Type) null) && customAttribute.primaryEntitySearch != null)
        {
          if (!PXPersonalDataTableAttribute.Links.TryGetValue(customAttribute.parentEntityType, out Dictionary<System.Type, List<BqlCommand>> _))
            PXPersonalDataTableAttribute.Links[customAttribute.parentEntityType] = new Dictionary<System.Type, List<BqlCommand>>();
          if (!PXPersonalDataTableAttribute.Links[customAttribute.parentEntityType].TryGetValue(type, out List<BqlCommand> _))
            PXPersonalDataTableAttribute.Links[customAttribute.parentEntityType][type] = new List<BqlCommand>();
          PXPersonalDataTableAttribute.Links[customAttribute.parentEntityType][type].Add(customAttribute.primaryEntitySearch);
        }
      }
      graph.EnsureCachePersistence(type);
      foreach (MemberInfo memberInfo in ((IEnumerable<System.Type>) graph.Caches[type].GetExtensionTypes()).Where<System.Type>((Func<System.Type, bool>) (_ => _.GetCustomAttributes(typeof (PXPersonalDataTableAttribute), false).Length != 0)))
      {
        foreach (PXPersonalDataTableAttribute customAttribute in memberInfo.GetCustomAttributes(typeof (PXPersonalDataTableAttribute), false))
        {
          if (!(customAttribute.parentEntityType == (System.Type) null) && customAttribute.primaryEntitySearch != null)
          {
            if (!PXPersonalDataTableAttribute.Links.TryGetValue(customAttribute.parentEntityType, out Dictionary<System.Type, List<BqlCommand>> _))
              PXPersonalDataTableAttribute.Links[customAttribute.parentEntityType] = new Dictionary<System.Type, List<BqlCommand>>();
            if (!PXPersonalDataTableAttribute.Links[customAttribute.parentEntityType].TryGetValue(type, out List<BqlCommand> _))
              PXPersonalDataTableAttribute.Links[customAttribute.parentEntityType][type] = new List<BqlCommand>();
            PXPersonalDataTableAttribute.Links[customAttribute.parentEntityType][type].Add(customAttribute.primaryEntitySearch);
          }
        }
      }
    }
    return PXPersonalDataTableAttribute.Links.TryGetValue(parentTable, out dictionary) ? dictionary : new Dictionary<System.Type, List<BqlCommand>>();
  }
}
