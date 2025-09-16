// Decompiled with JetBrains decompiler
// Type: PX.SM.Graph
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System.Diagnostics;

#nullable enable
namespace PX.SM;

[PXCacheName("Graph")]
[DebuggerDisplay("GraphName: {GraphName}")]
public sealed class Graph : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  private 
  #nullable disable
  string _GraphName;
  private string _Text;
  private string _Icon;
  private bool? _IsNamespace;
  private bool? _IsReport;

  [PXUnboundKey]
  [PXUIField(DisplayName = "Graph Name")]
  public string GraphName
  {
    get => this._GraphName;
    set => this._GraphName = value;
  }

  public string Path => this._GraphName;

  [PXString]
  [PXUIField(DisplayName = "Entity Name")]
  public string Text
  {
    [PXDependsOnFields(new System.Type[] {typeof (Graph.graphName)})] get
    {
      if (!string.IsNullOrEmpty(this._Text))
        return this._Text;
      if (!string.IsNullOrEmpty(this._GraphName))
      {
        string[] strArray = this._GraphName.Split('.');
        if (strArray.Length != 0)
        {
          this._Text = strArray[strArray.Length - 1];
          return this._Text;
        }
      }
      return string.Empty;
    }
    set => this._Text = value;
  }

  public string Icon
  {
    get => this._Icon;
    set => this._Icon = value;
  }

  public bool? IsNamespace
  {
    get => this._IsNamespace;
    set => this._IsNamespace = value;
  }

  public bool? IsReport
  {
    get => this._IsReport;
    set => this._IsReport = value;
  }

  public class PK : PrimaryKeyOf<Graph>.By<Graph.graphName>
  {
    public static Graph Find(PXGraph graph, string graphName, PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<Graph>.By<Graph.graphName>.FindBy(graph, (object) graphName, options);
    }
  }

  public abstract class graphName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Graph.graphName>
  {
  }

  public abstract class text : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Graph.text>
  {
  }
}
