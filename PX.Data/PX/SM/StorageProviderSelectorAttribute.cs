// Decompiled with JetBrains decompiler
// Type: PX.SM.StorageProviderSelectorAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.Update.Storage;
using System;
using System.Collections;
using System.Linq;

#nullable enable
namespace PX.SM;

internal class StorageProviderSelectorAttribute : PXCustomSelectorAttribute
{
  public StorageProviderSelectorAttribute()
    : base(typeof (StorageProviderSelectorAttribute.ProviderRec.typeName))
  {
  }

  protected 
  #nullable disable
  IEnumerable GetRecords()
  {
    return (IEnumerable) PXStorageHelper.Providers.Select<string, StorageProviderSelectorAttribute.ProviderRec>((Func<string, StorageProviderSelectorAttribute.ProviderRec>) (p => new StorageProviderSelectorAttribute.ProviderRec()
    {
      TypeName = p
    }));
  }

  [Serializable]
  public class ProviderRec : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXString(32 /*0x20*/, InputMask = "", IsKey = true)]
    [PXUIField(DisplayName = "Name", Visibility = PXUIVisibility.SelectorVisible)]
    public virtual string TypeName { get; set; }

    public abstract class typeName : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      StorageProviderSelectorAttribute.ProviderRec.typeName>
    {
    }
  }
}
