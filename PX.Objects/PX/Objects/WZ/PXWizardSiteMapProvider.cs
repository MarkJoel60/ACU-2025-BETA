// Decompiled with JetBrains decompiler
// Type: PX.Objects.WZ.PXWizardSiteMapProvider
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using PX.Common;
using PX.Data;
using PX.Security;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading;

#nullable disable
namespace PX.Objects.WZ;

public class PXWizardSiteMapProvider(
  IOptions<PXSiteMapOptions> options,
  IHttpContextAccessor httpContextAccessor,
  IRoleManagementService roleManagementService) : PXDatabaseSiteMapProvider(options, httpContextAccessor, roleManagementService)
{
  public const string WizardRootNode = "WZ000000";
  public const string OrganizationNode = "OG000000";
  protected static readonly Type[] Tables = EnumerableExtensions.Append<Type>(PXDatabaseSiteMapProvider.Tables, typeof (WZScenario));

  protected virtual PXSiteMapProvider.Definition GetSlot(string slotName)
  {
    return (PXSiteMapProvider.Definition) PXDatabase.GetSlot<PXWizardSiteMapProvider.WizardDefinition, PXWizardSiteMapProvider>(slotName + Thread.CurrentThread.CurrentUICulture.Name, this, PXWizardSiteMapProvider.Tables);
  }

  protected virtual void ResetSlot(string slotName)
  {
    PXDatabase.ResetSlot<PXWizardSiteMapProvider.WizardDefinition>(slotName + Thread.CurrentThread.CurrentUICulture.Name, PXWizardSiteMapProvider.Tables);
  }

  private class WizardDefinition : 
    PXDatabaseSiteMapProvider.DatabaseDefinition,
    IPrefetchable<PXWizardSiteMapProvider>,
    IPXCompanyDependent,
    IInternable
  {
    private readonly Dictionary<Guid, PXSiteMapNode> nodesByID = new Dictionary<Guid, PXSiteMapNode>();
    private bool isInterned;
    private object internObjectLock = new object();

    protected virtual void AddNode(PXSiteMapNode node, Guid parentID)
    {
      ((PXSiteMapProvider.Definition) this).AddNode(node, parentID);
      if (this.nodesByID.ContainsKey(node.NodeID))
        return;
      this.nodesByID.Add(node.NodeID, node);
    }

    void IPrefetchable<PXWizardSiteMapProvider>.Prefetch(PXWizardSiteMapProvider provider)
    {
      PXContext.SetSlot<bool>("PrefetchSiteMap", true);
      CultureInfo cultureInfo = (CultureInfo) null;
      if (Thread.CurrentThread.CurrentCulture.Name != Thread.CurrentThread.CurrentUICulture.Name)
      {
        cultureInfo = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture;
      }
      Dictionary<Guid, WZScenario> wizardScenarios = this.GetWizardScenarios();
      this.Prefetch((PXDatabaseSiteMapProvider) provider);
      if (!PXSiteMap.IsPortal)
      {
        foreach (Guid key in wizardScenarios.Keys)
        {
          List<string> stringList = new List<string>();
          if (!string.IsNullOrEmpty(wizardScenarios[key].Rolename))
            stringList.Add(wizardScenarios[key].Rolename);
          bool flag = wizardScenarios[key].Status == "AC";
          PXSiteMapNode pxSiteMapNode1 = !flag ? PXSiteMapProviderDefinitionExtensions.FindSiteMapNodesFromGraphType((PXSiteMapProvider.Definition) this, typeof (WizardNotActiveScenario).FullName, false).FirstOrDefault<PXSiteMapNode>() : PXSiteMapProviderDefinitionExtensions.FindSiteMapNodesFromGraphType((PXSiteMapProvider.Definition) this, typeof (WizardScenarioMaint).FullName, false).FirstOrDefault<PXSiteMapNode>();
          string url = "~/Frames/Default.aspx";
          if (pxSiteMapNode1 != null)
            url = $"{pxSiteMapNode1.Url}?ScenarioID={WebUtility.UrlEncode(key.ToString())}";
          PXSiteMapNode pxSiteMapNode2 = PXSiteMapProviderDefinitionExtensions.FindSiteMapNodesFromScreenID((PXSiteMapProvider.Definition) this, "WZ000000", false).FirstOrDefault<PXSiteMapNode>();
          PXSiteMapNode wizardNode = this.CreateWizardNode(provider, key, url, wizardScenarios[key].Name, new PXRoleList(stringList.Count == 0 ? (List<string>) null : stringList, (List<string>) null, (List<string>) null), (string) null, "WZ201500");
          Guid? nodeId1 = wizardScenarios[key].NodeID;
          Guid empty = Guid.Empty;
          if ((nodeId1.HasValue ? (nodeId1.GetValueOrDefault() != empty ? 1 : 0) : 1) != 0)
          {
            if (flag)
            {
              if (pxSiteMapNode2 != null)
              {
                Guid nodeId2 = pxSiteMapNode2.NodeID;
                nodeId1 = wizardScenarios[key].NodeID;
                Guid guid = nodeId1.Value;
                if (nodeId2 == guid)
                {
                  PXSiteMapNode pxSiteMapNode3 = PXSiteMapProviderDefinitionExtensions.FindSiteMapNodesFromScreenID((PXSiteMapProvider.Definition) this, "WZ000001", false).FirstOrDefault<PXSiteMapNode>();
                  if (pxSiteMapNode3 == null)
                  {
                    pxSiteMapNode3 = this.CreateWizardNode(provider, Guid.NewGuid(), "~/Frames/Default.aspx", "Work Area", (PXRoleList) null, (string) null, "WZ000001");
                    ((PXSiteMapProvider.Definition) this).AddNode(pxSiteMapNode3, pxSiteMapNode2.NodeID);
                  }
                  ((PXSiteMapProvider.Definition) this).AddNode(wizardNode, pxSiteMapNode3.NodeID);
                  continue;
                }
              }
              PXSiteMapNode pxSiteMapNode4 = wizardNode;
              nodeId1 = wizardScenarios[key].NodeID;
              Guid guid1 = nodeId1.Value;
              ((PXSiteMapProvider.Definition) this).AddNode(pxSiteMapNode4, guid1);
            }
            else
              ((PXSiteMapProvider.Definition) this).AddNode(wizardNode, Guid.Empty);
          }
          else if (flag)
          {
            if (pxSiteMapNode2 != null)
            {
              PXSiteMapNode pxSiteMapNode5 = PXSiteMapProviderDefinitionExtensions.FindSiteMapNodesFromScreenID((PXSiteMapProvider.Definition) this, "WZ000001", false).FirstOrDefault<PXSiteMapNode>();
              if (pxSiteMapNode5 == null)
              {
                pxSiteMapNode5 = this.CreateWizardNode(provider, Guid.NewGuid(), "~/Frames/Default.aspx", "Work Area", (PXRoleList) null, (string) null, "WZ000001");
                ((PXSiteMapProvider.Definition) this).AddNode(pxSiteMapNode5, pxSiteMapNode2.NodeID);
              }
              ((PXSiteMapProvider.Definition) this).AddNode(wizardNode, pxSiteMapNode5.NodeID);
            }
          }
          else
            ((PXSiteMapProvider.Definition) this).AddNode(wizardNode, Guid.Empty);
        }
      }
      if (cultureInfo != null)
        Thread.CurrentThread.CurrentCulture = cultureInfo;
      PXContext.SetSlot<bool>("PrefetchSiteMap", false);
    }

    public object Intern()
    {
      PXWizardSiteMapProvider.WizardDefinition wizardDefinition = this;
      PXWizardSiteMapProvider.WizardDefinition returnValue1;
      if (new PxObjectsIntern<PXWizardSiteMapProvider.WizardDefinition>().TryIntern(wizardDefinition, out returnValue1))
        wizardDefinition = returnValue1;
      if (wizardDefinition.isInterned)
        return (object) wizardDefinition;
      lock (wizardDefinition.internObjectLock)
      {
        if (!wizardDefinition.isInterned)
        {
          Dictionary<PXSiteMapNode, PXSiteMapNode> cache = new Dictionary<PXSiteMapNode, PXSiteMapNode>((IEqualityComparer<PXSiteMapNode>) new ReflectionSerializer.ObjectComparer<object>());
          PxObjectsIntern<PXSiteMapNode> pxObjectsIntern = new PxObjectsIntern<PXSiteMapNode>();
          PXSiteMapNode returnValue2;
          if (pxObjectsIntern.TryIntern(((PXSiteMapProvider.Definition) this).RootNode, out returnValue2, cache))
            ((PXSiteMapProvider.Definition) this).RootNode = returnValue2;
          PXSiteMapProvider.Definition.InternDictionary<Guid, PXSiteMapNode>(this.nodesByID, pxObjectsIntern, cache);
          PXSiteMapProvider.Definition.InternMultiDictionary(((PXSiteMapProvider.Definition) this).GraphTypeTable, pxObjectsIntern, cache);
          PXSiteMapProvider.Definition.InternMultiDictionary(((PXSiteMapProvider.Definition) this).ScreenIDTable, pxObjectsIntern, cache);
          PXSiteMapProvider.Definition.InternDictionary<string, PXSiteMapNode>(((PXSiteMapProvider.Definition) this).UrlTable, pxObjectsIntern, cache);
          PXSiteMapProvider.Definition.InternDictionary<Guid, PXSiteMapNode>(((PXSiteMapProvider.Definition) this).KeyTable, pxObjectsIntern, cache);
          foreach (Guid key in ((PXSiteMapProvider.Definition) this).ChildNodeCollectionTable.Keys.ToArray<Guid>())
          {
            foreach (PXSiteMapNode pxSiteMapNode in new List<PXSiteMapNode>((IEnumerable<PXSiteMapNode>) ((PXSiteMapProvider.Definition) this).ChildNodeCollectionTable[key]))
            {
              if (pxObjectsIntern.TryIntern(pxSiteMapNode, out returnValue2, cache))
              {
                ((PXSiteMapProvider.Definition) this).ChildNodeCollectionTable[key].Remove(pxSiteMapNode);
                ((PXSiteMapProvider.Definition) this).ChildNodeCollectionTable[key].Add(returnValue2);
              }
            }
          }
          wizardDefinition.isInterned = true;
        }
      }
      return (object) wizardDefinition;
    }

    private Dictionary<Guid, WZScenario> GetWizardScenarios()
    {
      Dictionary<Guid, WZScenario> source = new Dictionary<Guid, WZScenario>();
      foreach (PXDataRecord pxDataRecord in PXDatabase.SelectMulti(typeof (WZScenario), new PXDataField[6]
      {
        (PXDataField) new PXDataField<WZScenario.scenarioID>(),
        (PXDataField) new PXDataField<WZScenario.nodeID>(),
        (PXDataField) new PXDataField<WZScenario.name>(),
        (PXDataField) new PXDataField<WZScenario.rolename>(),
        (PXDataField) new PXDataField<WZScenario.status>(),
        (PXDataField) new PXDataField<WZScenario.scenarioOrder>()
      }))
      {
        WZScenario wzScenario1 = new WZScenario();
        Guid? nullable = pxDataRecord.GetGuid(0);
        wzScenario1.ScenarioID = new Guid?(nullable.Value);
        nullable = pxDataRecord.GetGuid(1);
        wzScenario1.NodeID = new Guid?(nullable.Value);
        wzScenario1.Name = pxDataRecord.GetString(2);
        wzScenario1.Rolename = pxDataRecord.GetString(3);
        wzScenario1.Status = pxDataRecord.GetString(4);
        wzScenario1.ScenarioOrder = pxDataRecord.GetInt32(5);
        WZScenario wzScenario2 = wzScenario1;
        Dictionary<Guid, WZScenario> dictionary = source;
        nullable = wzScenario2.ScenarioID;
        Guid key = nullable.Value;
        WZScenario wzScenario3 = wzScenario2;
        dictionary.Add(key, wzScenario3);
      }
      return source.OrderBy<KeyValuePair<Guid, WZScenario>, int?>((Func<KeyValuePair<Guid, WZScenario>, int?>) (x => x.Value.ScenarioOrder)).ToDictionary<KeyValuePair<Guid, WZScenario>, Guid, WZScenario>((Func<KeyValuePair<Guid, WZScenario>, Guid>) (x => x.Key), (Func<KeyValuePair<Guid, WZScenario>, WZScenario>) (x => x.Value));
    }

    private PXSiteMapNode CreateWizardNode(
      PXWizardSiteMapProvider provider,
      Guid nodeID,
      string url,
      string title,
      PXRoleList roles,
      string graphType,
      string screenID)
    {
      return (PXSiteMapNode) new PXOldUiSiteMapNode((PXSiteMapProvider) provider, nodeID, url, title, roles, graphType, screenID);
    }
  }
}
