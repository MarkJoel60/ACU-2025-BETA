// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.SkillGridFilter
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using System;
using System.Collections.Generic;

#nullable enable
namespace PX.Objects.FS;

[Serializable]
public class SkillGridFilter : FSSkill
{
  [PXDBString(15, IsKey = true, IsFixed = true)]
  [PXUIField(DisplayName = "Skill ID", Enabled = false)]
  public override 
  #nullable disable
  string SkillCD { get; set; }

  [PXDBString(60, IsUnicode = true)]
  [PXUIField(DisplayName = "Description", Enabled = false)]
  public override string Descr { get; set; }

  [PXBool]
  [PXUIField(DisplayName = "Selected")]
  public virtual bool? Mem_Selected { get; set; }

  [PXString(200, IsUnicode = true)]
  [PXUIField(DisplayName = "Service List", Enabled = false)]
  public virtual string Mem_ServicesList { get; set; }

  /// <summary>
  /// Gets the Inventory CDs of the given services associated with the given skill.
  /// </summary>
  /// <param name="graph">Context graph that will be used in the query execution.</param>
  /// <param name="serviceIDList">Service identifier list which their Inventory CDs will be retrieved.</param>
  /// <param name="skillID">Skill identifier associated with the services to which they Inventory CDs will be retrieved.</param>
  /// <returns>String with the concatenation of the Inventory CDs, separated by commas, of the resulting services.</returns>
  public static string GetServiceListField(PXGraph graph, List<int?> serviceIDList, int? skillID)
  {
    string serviceListField = string.Empty;
    if (serviceIDList.Count > 0 && skillID.HasValue)
    {
      PXGraph graph1 = graph;
      List<int?> fieldList = serviceIDList;
      object[] objArray = new object[1]{ (object) skillID };
      foreach (SharedClasses.ItemList itemWith in SharedFunctions.GetItemWithList<PX.Objects.IN.InventoryItem, InnerJoin<FSServiceSkill, On<FSServiceSkill.serviceID, Equal<PX.Objects.IN.InventoryItem.inventoryID>>>, PX.Objects.IN.InventoryItem.inventoryID, PX.Objects.IN.InventoryItem.inventoryCD, Where<FSServiceSkill.skillID, Equal<Required<FSServiceSkill.skillID>>>>(graph1, fieldList, objArray))
      {
        string str = (string) itemWith.list[0];
        serviceListField = !string.IsNullOrEmpty(serviceListField) ? $"{serviceListField}, {str.Trim()}" : str.Trim();
      }
    }
    return serviceListField;
  }

  public abstract class mem_Selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SkillGridFilter.mem_Selected>
  {
  }

  public abstract class mem_ServicesList : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SkillGridFilter.mem_ServicesList>
  {
  }
}
