// Decompiled with JetBrains decompiler
// Type: PX.SM.ExchangeContactsFolderSelectorAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.Update.ExchangeService;
using PX.Data.Update.WebServices;
using System.Collections;

#nullable enable
namespace PX.SM;

public class ExchangeContactsFolderSelectorAttribute : PXCustomSelectorAttribute
{
  public ExchangeContactsFolderSelectorAttribute()
    : base(typeof (ExchangeContactsFolderSelectorAttribute.Folder.key))
  {
    this.CacheGlobal = true;
    this.DescriptionField = typeof (ExchangeContactsFolderSelectorAttribute.Folder.name);
  }

  protected virtual 
  #nullable disable
  IEnumerable GetRecords()
  {
    ExchangeContactsFolderSelectorAttribute selectorAttribute = this;
    if (selectorAttribute._Graph.Caches[selectorAttribute._CacheType].Current is EMailSyncServer current)
    {
      foreach (BaseFolderType publicFolder in PXExchangeServer.GetGate(current).FindPublicFolders((string) null, typeof (ContactsFolderType)))
        yield return (object) new ExchangeContactsFolderSelectorAttribute.Folder()
        {
          Key = $"{publicFolder.FolderId.Id}|{publicFolder.FolderId.ChangeKey}",
          Name = publicFolder.DisplayName
        };
    }
  }

  public class Folder : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXString(IsKey = true, IsFixed = false, IsUnicode = true, InputMask = "")]
    public string Key { get; set; }

    [PXString(IsUnicode = true, InputMask = "")]
    [PXUIField(DisplayName = "Folder", Visibility = PXUIVisibility.SelectorVisible)]
    public string Name { get; set; }

    public abstract class key : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ExchangeContactsFolderSelectorAttribute.Folder.key>
    {
    }

    public abstract class name : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ExchangeContactsFolderSelectorAttribute.Folder.name>
    {
    }
  }
}
