// Decompiled with JetBrains decompiler
// Type: PX.Objects.TX.ECMPluginSelectorAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CA;
using PX.TaxProvider;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace PX.Objects.TX;

public class ECMPluginSelectorAttribute : PXCustomSelectorAttribute
{
  private static 
  #nullable disable
  Type[] _interfaces = new Type[1]
  {
    typeof (IExemptionCertificateProvider)
  };

  public ECMPluginSelectorAttribute()
    : base(typeof (ECMPluginSelectorAttribute.ECMPlugin.taxPluginID), new Type[1]
    {
      typeof (ECMPluginSelectorAttribute.ECMPlugin.description)
    })
  {
    ((PXSelectorAttribute) this).DescriptionField = typeof (ECMPluginSelectorAttribute.ECMPlugin.description);
  }

  protected IEnumerable GetRecords()
  {
    ECMPluginSelectorAttribute selectorAttribute = this;
    PXProviderTypeSelectorAttribute.ProviderRec[] array = PXProviderTypeSelectorAttribute.GetProviderRecs(ECMPluginSelectorAttribute._interfaces).ToArray<PXProviderTypeSelectorAttribute.ProviderRec>();
    PXResultset<TaxPlugin> pxResultset = PXSelectBase<TaxPlugin, PXSelect<TaxPlugin>.Config>.Select(selectorAttribute._Graph, Array.Empty<object>());
    List<ECMPluginSelectorAttribute.ECMPlugin> ecmPluginList = new List<ECMPluginSelectorAttribute.ECMPlugin>();
    if (pxResultset.Count > 0)
    {
      foreach (PXResult<TaxPlugin> pxResult in pxResultset)
      {
        TaxPlugin row = PXResult<TaxPlugin>.op_Implicit(pxResult);
        if ((array != null ? ((IEnumerable<PXProviderTypeSelectorAttribute.ProviderRec>) array).FirstOrDefault<PXProviderTypeSelectorAttribute.ProviderRec>((Func<PXProviderTypeSelectorAttribute.ProviderRec, bool>) (plugin => plugin.TypeName.Trim() == row.PluginTypeName.Trim())) : (PXProviderTypeSelectorAttribute.ProviderRec) null) != null)
          ecmPluginList.Add(new ECMPluginSelectorAttribute.ECMPlugin()
          {
            TaxPluginID = row.TaxPluginID,
            Description = row.Description
          });
      }
    }
    foreach (object record in ecmPluginList)
      yield return record;
  }

  [PXHidden]
  public class ECMPlugin : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXDBString(15, IsUnicode = true, IsKey = true)]
    [PXUIField(DisplayName = "Provider ID ")]
    public virtual string TaxPluginID { get; set; }

    [PXDBString(255 /*0xFF*/, IsUnicode = true)]
    [PXUIField(DisplayName = "Description")]
    public virtual string Description { get; set; }

    public abstract class taxPluginID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ECMPluginSelectorAttribute.ECMPlugin.taxPluginID>
    {
    }

    public abstract class description : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ECMPluginSelectorAttribute.ECMPlugin.description>
    {
    }
  }
}
