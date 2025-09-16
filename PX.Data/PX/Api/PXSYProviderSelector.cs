// Decompiled with JetBrains decompiler
// Type: PX.Api.PXSYProviderSelector
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;
using System.Collections;
using System.Reflection;

#nullable enable
namespace PX.Api;

public class PXSYProviderSelector : PXCustomSelectorAttribute
{
  public PXSYProviderSelector()
    : base(typeof (PXSYProviderSelector.ProviderRec.typeName), typeof (PXSYProviderSelector.ProviderRec.typeName), typeof (PXSYProviderSelector.ProviderRec.description))
  {
    this.DescriptionField = typeof (PXSYProviderSelector.ProviderRec.description);
  }

  internal 
  #nullable disable
  IEnumerable GetRecords()
  {
    Assembly[] assemblyArray = AppDomain.CurrentDomain.GetAssemblies();
    for (int index1 = 0; index1 < assemblyArray.Length; ++index1)
    {
      Assembly a = assemblyArray[index1];
      if (PXSubstManager.IsSuitableTypeExportAssembly(a, true))
      {
        System.Type[] typeArray1 = (System.Type[]) null;
        try
        {
          if (!a.IsDynamic)
            typeArray1 = a.GetExportedTypes();
        }
        catch (ReflectionTypeLoadException ex)
        {
          typeArray1 = ex.Types;
        }
        catch
        {
          continue;
        }
        if (typeArray1 != null)
        {
          System.Type[] typeArray = typeArray1;
          for (int index2 = 0; index2 < typeArray.Length; ++index2)
          {
            System.Type type = typeArray[index2];
            if (type != (System.Type) null && typeof (IPXSYProvider).IsAssignableFrom(type) && !type.IsInterface)
            {
              string str1 = type.Name;
              string str2 = (string) null;
              try
              {
                IPXSYProvider instance = (IPXSYProvider) Activator.CreateInstance(type);
                str1 = instance.ProviderName;
                str2 = instance.DefaultFileExtension;
              }
              catch
              {
              }
              yield return (object) new PXSYProviderSelector.ProviderRec()
              {
                TypeName = type.FullName,
                Description = str1,
                DefaultFileExtension = str2
              };
            }
          }
          typeArray = (System.Type[]) null;
        }
      }
    }
    assemblyArray = (Assembly[]) null;
  }

  [Serializable]
  public class ProviderRec : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXString(128 /*0x80*/, InputMask = "", IsKey = true)]
    [PXUIField(DisplayName = "Type Name", Visibility = PXUIVisibility.SelectorVisible)]
    public virtual string TypeName { get; set; }

    [PXDBString(128 /*0x80*/, InputMask = "", IsUnicode = true)]
    [PXUIField(DisplayName = "Description", Visibility = PXUIVisibility.SelectorVisible)]
    public virtual string Description { get; set; }

    [PXString]
    [PXUIField(Visibility = PXUIVisibility.Invisible)]
    public virtual string DefaultFileExtension { get; set; }

    public abstract class typeName : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      PXSYProviderSelector.ProviderRec.typeName>
    {
    }

    public abstract class description : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      PXSYProviderSelector.ProviderRec.description>
    {
    }

    public abstract class defaultFileExtension : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      PXSYProviderSelector.ProviderRec.defaultFileExtension>
    {
    }
  }
}
