// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.PXProviderTypeSelectorAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.CCProcessingBase.Attributes;
using PX.Data;
using PX.Data.BQL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

#nullable enable
namespace PX.Objects.CA;

public class PXProviderTypeSelectorAttribute : PXCustomSelectorAttribute
{
  private 
  #nullable disable
  Type[] _providerInterfaceType;

  public PXProviderTypeSelectorAttribute(params Type[] providerType)
    : base(typeof (PXProviderTypeSelectorAttribute.ProviderRec.typeName))
  {
    this._providerInterfaceType = providerType;
  }

  protected virtual IEnumerable GetRecords()
  {
    return (IEnumerable) PXProviderTypeSelectorAttribute.GetProviderRecs(this._providerInterfaceType);
  }

  public static IEnumerable<PXProviderTypeSelectorAttribute.ProviderRec> GetProviderRecs(
    params Type[] providerInterfaceTypes)
  {
    if (providerInterfaceTypes != null)
    {
      Assembly[] assemblyArray = AppDomain.CurrentDomain.GetAssemblies();
      for (int index = 0; index < assemblyArray.Length; ++index)
      {
        Assembly assembly = assemblyArray[index];
        if (PXSubstManager.IsSuitableTypeExportAssembly(assembly, false))
        {
          Type[] source = (Type[]) null;
          try
          {
            if (!assembly.IsDynamic)
              source = assembly.GetExportedTypes();
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
            foreach (Type element in ((IEnumerable<Type>) source).Where<Type>((Func<Type, bool>) (assemblyType => ((IEnumerable<Type>) providerInterfaceTypes).Any<Type>((Func<Type, bool>) (interfaceType => interfaceType.IsAssignableFrom(assemblyType) && assemblyType != interfaceType)))))
            {
              Attribute attribute = element.GetCustomAttributes().FirstOrDefault<Attribute>((Func<Attribute, bool>) (a => a is PXDisplayTypeNameAttribute));
              if (attribute != null)
              {
                int num = attribute is PXDisplayTypeNameAttribute typeNameAttribute1 ? (typeNameAttribute1.Skip ? 1 : 0) : 0;
                string dependsFromFeature = attribute is PXDisplayTypeNameAttribute typeNameAttribute2 ? typeNameAttribute2.DependsFromFeature : (string) null;
                if (num != 0 || dependsFromFeature != null && !PXAccess.FeatureInstalled(dependsFromFeature))
                  continue;
              }
              string str = (attribute is PXDisplayTypeNameAttribute typeNameAttribute ? typeNameAttribute.Name : (string) null) ?? element.FullName;
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
    [PXUIField]
    public virtual string TypeName { get; set; }

    [PXString(255 /*0xFF*/, InputMask = "")]
    [PXUIField]
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
