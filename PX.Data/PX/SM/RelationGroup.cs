// Decompiled with JetBrains decompiler
// Type: PX.SM.RelationGroup
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System.Collections;

#nullable enable
namespace PX.SM;

[PXCacheName("Relation Group")]
[PXPrimaryGraph(typeof (RelationGroups))]
public class RelationGroup : 
  PXBqlTable,
  IBqlTable,
  IBqlTableSystemDataStorage,
  IIncludable,
  IRestricted
{
  protected 
  #nullable disable
  string _GroupName;
  protected string _Description;
  protected string _SpecificType;
  protected string _SpecificModule;
  protected byte[] _GroupMask;
  private bool? _Active;
  private string _GroupType;
  protected bool? _Included;

  [PXDBString(128 /*0x80*/, IsKey = true, InputMask = "")]
  [PXDefault]
  [PXUIField(DisplayName = "Group Name", Visibility = PXUIVisibility.SelectorVisible)]
  [PXSelector(typeof (RelationGroup.groupName), Filterable = true)]
  public virtual string GroupName
  {
    get => this._GroupName;
    set => this._GroupName = value;
  }

  [PXDBString(255 /*0xFF*/)]
  [PXUIField(DisplayName = "Description", Visibility = PXUIVisibility.SelectorVisible)]
  public virtual string Description
  {
    get => this._Description;
    set => this._Description = value;
  }

  [PXDBString(255 /*0xFF*/, IsUnicode = false)]
  [MaskedTypeSelector]
  [PXUIField(DisplayName = "Specific Type")]
  public virtual string SpecificType
  {
    get => this._SpecificType;
    set => this._SpecificType = value;
  }

  [PXDBString(255 /*0xFF*/)]
  [RelationGroup.ModuleAll]
  [PXUIField(DisplayName = "Specific Module")]
  public virtual string SpecificModule
  {
    get => this._SpecificModule;
    set => this._SpecificModule = value;
  }

  [PXDBBinary]
  [PXDefault]
  public virtual byte[] GroupMask
  {
    get => this._GroupMask;
    set => this._GroupMask = value;
  }

  [PXDBBool]
  [PXUIField(DisplayName = "Active")]
  [PXDefault(true)]
  public bool? Active
  {
    get => this._Active;
    set => this._Active = value;
  }

  [PXDBString(2, IsFixed = true)]
  [PXUIField(DisplayName = "Group Type")]
  [PXDefault("IO")]
  [PXStringList(new string[] {"IE", "EE", "IO", "EO"}, new string[] {"B", "B Inverse", "A", "A Inverse"})]
  public string GroupType
  {
    get => this._GroupType;
    set => this._GroupType = value;
  }

  [PXUnboundDefault(false, PersistingCheck = PXPersistingCheck.Nothing)]
  [PXBool]
  [PXUIField(DisplayName = "Included")]
  public virtual bool? Included
  {
    get => this._Included;
    set => this._Included = value;
  }

  public class PK : PrimaryKeyOf<RelationGroup>.By<RelationGroup.groupName>
  {
    public static RelationGroup Find(PXGraph graph, string groupName, PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<RelationGroup>.By<RelationGroup.groupName>.FindBy(graph, (object) groupName, options);
    }
  }

  public static class FK
  {
    public class MaskedType : 
      PrimaryKeyOf<MaskedType>.By<MaskedType.entityTypeName>.ForeignKeyOf<RelationGroup>.By<RelationGroup.specificType>
    {
    }

    public class Graph : 
      PrimaryKeyOf<Graph>.By<Graph.graphName>.ForeignKeyOf<RelationGroup>.By<RelationGroup.specificModule>
    {
    }
  }

  /// <summary>Allow show list of application modules.</summary>
  /// <example>[ModuleAll]</example>
  public sealed class ModuleAllAttribute : PXCustomSelectorAttribute, IPXFieldUpdatingSubscriber
  {
    public ModuleAllAttribute()
      : base(typeof (Graph.graphName), typeof (Graph.text))
    {
    }

    internal IEnumerable GetRecords() => (IEnumerable) GraphHelper.GetModules(true);

    public override void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
    {
      string returnValue = e.ReturnValue as string;
      if (!string.IsNullOrEmpty(returnValue))
      {
        foreach (Graph module in GraphHelper.GetModules())
        {
          if (module.GraphName == returnValue)
          {
            e.ReturnValue = (object) module.Text;
            break;
          }
        }
      }
      base.FieldSelecting(sender, e);
    }

    public void FieldUpdating(PXCache sender, PXFieldUpdatingEventArgs e)
    {
      string newValue = e.NewValue as string;
      if (string.IsNullOrEmpty(newValue))
        return;
      foreach (Graph module in GraphHelper.GetModules())
      {
        if (module.Text == newValue)
        {
          e.NewValue = (object) module.GraphName;
          break;
        }
      }
    }
  }

  public abstract class groupName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RelationGroup.groupName>
  {
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RelationGroup.description>
  {
  }

  public abstract class specificType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RelationGroup.specificType>
  {
  }

  public abstract class specificModule : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    RelationGroup.specificModule>
  {
  }

  public abstract class groupMask : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  RelationGroup.groupMask>
  {
  }

  public abstract class active : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  RelationGroup.active>
  {
  }

  public abstract class groupType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  RelationGroup.groupType>
  {
  }

  public abstract class included : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  RelationGroup.included>
  {
  }
}
