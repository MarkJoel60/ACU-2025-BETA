// Decompiled with JetBrains decompiler
// Type: PX.SM.PXProviderTypeSelectorAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

#nullable enable
namespace PX.SM;

public class PXProviderTypeSelectorAttribute : PXCustomSelectorAttribute
{
  private readonly 
  #nullable disable
  System.Type[] _providerInterfaceType;

  public PXProviderTypeSelectorAttribute(params System.Type[] providerType)
    : base(typeof (PXProviderTypeSelectorAttribute.ProviderRec.typeName))
  {
    this._providerInterfaceType = providerType;
  }

  protected virtual IEnumerable GetRecords()
  {
    return (IEnumerable) PXProviderTypeSelectorAttribute.GetProviderRecs(this._providerInterfaceType);
  }

  public static IEnumerable<PXProviderTypeSelectorAttribute.ProviderRec> GetProviderRecs(
    params System.Type[] providerInterfaceTypes)
  {
    if (providerInterfaceTypes != null)
    {
      Assembly[] assemblyArray = AppDomain.CurrentDomain.GetAssemblies();
      for (int index = 0; index < assemblyArray.Length; ++index)
      {
        Assembly a1 = assemblyArray[index];
        if (PXSubstManager.IsSuitableTypeExportAssembly(a1, false))
        {
          System.Type[] source = (System.Type[]) null;
          try
          {
            if (!a1.IsDynamic)
              source = a1.GetExportedTypes();
          }
          catch (ReflectionTypeLoadException ex)
          {
            source = ex.Types;
          }
          catch
          {
            continue;
          }
          if (source != null)
          {
            foreach (System.Type element in ((IEnumerable<System.Type>) source).Where<System.Type>((Func<System.Type, bool>) (assemblyType => ((IEnumerable<System.Type>) providerInterfaceTypes).Any<System.Type>((Func<System.Type, bool>) (interfaceType => interfaceType.IsAssignableFrom(assemblyType) && assemblyType != interfaceType)))))
            {
              Attribute attribute = element.GetCustomAttributes().FirstOrDefault<Attribute>((Func<Attribute, bool>) (a => a is PXDisplayTypeNameAttribute));
              if (attribute != null)
              {
                System.Type dependsFromFeature = attribute is PXDisplayTypeNameAttribute typeNameAttribute ? typeNameAttribute.DependsFromFeature : (System.Type) null;
                if (dependsFromFeature != (System.Type) null && !PXAccess.FeatureInstalled(dependsFromFeature.FullName))
                  continue;
              }
              string str = (attribute is PXDisplayTypeNameAttribute typeNameAttribute1 ? typeNameAttribute1.Name : (string) null) ?? element.FullName;
              yield return new PXProviderTypeSelectorAttribute.ProviderRec()
              {
                TypeName = element.FullName,
                DisplayTypeName = str
              };
            }
          }
        }
      }
      assemblyArray = (Assembly[]) null;
    }
  }

  [PXHidden]
  [Serializable]
  public class ProviderRec : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXString(255 /*0xFF*/, InputMask = "", IsKey = true)]
    [PXUIField(DisplayName = "Type Name", Visibility = PXUIVisibility.SelectorVisible)]
    public virtual string TypeName { get; set; }

    [PXString(255 /*0xFF*/, InputMask = "")]
    [PXUIField(DisplayName = "Description", Visibility = PXUIVisibility.SelectorVisible)]
    public virtual string DisplayTypeName { get; set; }

    public abstract class typeName : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      PXProviderTypeSelectorAttribute.ProviderRec.typeName>
    {
    }

    public abstract class displayTypeName : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      PXProviderTypeSelectorAttribute.ProviderRec.displayTypeName>
    {
    }
  }
}
